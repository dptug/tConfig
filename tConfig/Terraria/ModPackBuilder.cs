using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Gajatko.IniFiles;
using Ionic.Zip;
using Microsoft.CSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CompilerParameters = System.CodeDom.Compiler.CompilerParameters;

namespace Terraria
{
	public class ModPackBuilder
	{
		public ModPackBuilder(string objFile)
		{
			ModPackBuilder.modpack = Path.GetFileNameWithoutExtension(objFile);
			string text = Config.Get7Zip();
			if (File.Exists(text))
			{
				if (!Directory.Exists(Path.Combine(Constants.tConfigFolder, "ModPacks")))
				{
					Directory.CreateDirectory(Path.Combine(Constants.tConfigFolder, "ModPacks"));
				}
				string text2 = Path.Combine(Constants.tConfigFolder, "ModPacks_temp");
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				File.Copy(objFile, Path.Combine(text2, ModPackBuilder.modpack + ".obj"), true);
				if (Directory.Exists(Path.Combine(text2, ModPackBuilder.modpack)))
				{
					Directory.Delete(Path.Combine(text2, ModPackBuilder.modpack), true);
				}
				Console.WriteLine("Using 7-zip!");
				Main.statusText = "Extracting " + ModPackBuilder.modpack;
				Process process = new Process
				{
					StartInfo = 
					{
						UseShellExecute = false,
						FileName = text,
						Arguments = string.Format("x -t7z \"{0}\" \"{1}\" -y -o\"{2}\"", objFile, ModPackBuilder.modpack, text2)
					}
				};
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.Start();
				process.BeginOutputReadLine();
				process.WaitForExit();
				if (!Directory.Exists(Path.Combine(text2, ModPackBuilder.modpack)))
				{
					Console.WriteLine("Failed to extract archive; build failed");
					return;
				}
				this.Initialize(ModPackBuilder.modpack, "", Path.Combine(text2, ModPackBuilder.modpack), "");
			}
		}

		public ModPackBuilder(string m, string Over = "", string directory = "", string output = "")
		{
			this.Initialize(m, Over, directory, output);
		}

		public void Initialize(string m, string Over = "", string directory = "", string outputDir = "")
		{
			if (Over != "")
			{
				Console.WriteLine("Building {0} with Override={1}", m, Over);
			}
			ModPackBuilder.modpack = m;
			ModPackBuilder.Override = Over;
			if (string.IsNullOrEmpty(directory))
			{
				ModPackBuilder.modpackFolder = Constants.tConfigFolder + "\\ModPacks\\" + ModPackBuilder.modpack;
			}
			else
			{
				ModPackBuilder.modpackFolder = directory;
			}
			if (string.IsNullOrEmpty(outputDir))
			{
				ModPackBuilder.outputFolder = Path.Combine(Constants.tConfigFolder, "ModPacks");
			}
			else
			{
				ModPackBuilder.outputFolder = outputDir;
			}
			ModPackBuilder.OverridePath = string.Concat(new object[]
			{
				ModPackBuilder.modpack,
				Path.DirectorySeparatorChar,
				"Override",
				Path.DirectorySeparatorChar,
				ModPackBuilder.Override
			});
			string path = ModPackBuilder.modpackFolder + Path.DirectorySeparatorChar + "Config.ini";
			IniFile iniFile = IniFile.FromFile(path);
			string path2 = Constants.tConfigFolder + Path.DirectorySeparatorChar + "Config Mod.ini";
			IniFile iniFile2 = IniFile.FromFile(path2);
			if (!string.IsNullOrEmpty(iniFile["Settings"]["cscheck"]) && iniFile["Settings"]["cscheck"] == "False")
			{
				ModPackBuilder.csCheck = false;
			}
			else if (!string.IsNullOrEmpty(iniFile2["Builder"]["cscheck"]) && iniFile2["Builder"]["cscheck"] == "False")
			{
				ModPackBuilder.csCheck = false;
			}
			else
			{
				ModPackBuilder.csCheck = true;
			}
			ModPackBuilder.customCompile = false;
			if (!string.IsNullOrEmpty(iniFile["Settings"]["customCompile"]) && iniFile["Settings"]["customCompile"] == "True")
			{
				ModPackBuilder.customCompile = true;
				ModPackBuilder.csCheck = false;
			}
			if (!string.IsNullOrEmpty(iniFile["Settings"]["requires"]))
			{
				Config.mods = new List<string>();
				string text = iniFile["Settings"]["requires"];
				ModPackBuilder.requiredMods = text.Split(new char[]
				{
					','
				});
				for (int i = 0; i < ModPackBuilder.requiredMods.Length; i++)
				{
					ModPackBuilder.requiredMods[i] = ModPackBuilder.requiredMods[i].Trim();
					Config.mods.Add(ModPackBuilder.requiredMods[i]);
					Console.WriteLine("Added {0} as requirement", ModPackBuilder.requiredMods[i]);
				}
				for (int j = 0; j < Config.mods.Count; j++)
				{
					if (!Config.LoadCustomObj(j))
					{
						Console.WriteLine("Failed to load required mod!");
						Config.mainInstance.PublicInit(false, 1, true);
						return;
					}
				}
			}
			else
			{
				ModPackBuilder.requiredMods = null;
			}
			ModPackBuilder.fileList = new Dictionary<string, ArrayList>();
			ModPackBuilder.itemFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Item")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Item"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Item"] = ModPackBuilder.itemFiles;
			ModPackBuilder.projFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Projectile")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Projectile"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Projectile"] = ModPackBuilder.projFiles;
			ModPackBuilder.npcFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "NPC")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "NPC"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["NPC"] = ModPackBuilder.npcFiles;
			ModPackBuilder.buffFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Buff")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Buff"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Buff"] = ModPackBuilder.buffFiles;
			ModPackBuilder.tileFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Tile")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Tile"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Tile"] = ModPackBuilder.tileFiles;
			ModPackBuilder.wallFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Wall")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Wall"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Wall"] = ModPackBuilder.wallFiles;
			ModPackBuilder.fileList["HoldStyle"] = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Item", "HoldStyle")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Item", "HoldStyle"), "*.cs")) : new ArrayList());
			ModPackBuilder.fileList["UseStyle"] = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Item", "UseStyle")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Item", "UseStyle"), "*.cs")) : new ArrayList());
			ModPackBuilder.prefixFiles = (Directory.Exists(Path.Combine(ModPackBuilder.modpackFolder, "Prefix")) ? new ArrayList(Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Prefix"), "*.ini", SearchOption.AllDirectories)) : new ArrayList());
			ModPackBuilder.fileList["Prefix"] = ModPackBuilder.prefixFiles;
		}

		public static bool ItemExists(string itemname)
		{
			Console.WriteLine("Checking existence of Item {0}", itemname);
			return Config.itemDefs.byName.ContainsKey(itemname) || Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Item"), itemname + ".ini", SearchOption.AllDirectories).Length > 0;
		}

		public static bool VanillaNPCExists(string itemname)
		{
			Console.WriteLine("Checking existence of NPC {0}", itemname);
			return Config.npcDefs.byName.ContainsKey(itemname);
		}

		public static bool HoldStyleExists(string itemname)
		{
			Console.WriteLine("Checking existence of HoldStyle {0}", itemname);
			return ModPackBuilder.fileList["HoldStyle"].Contains(ModPackBuilder.modpackFolder + "\\Item\\HoldStyle\\" + itemname + ".cs");
		}

		public static bool UseStyleExists(string itemname)
		{
			Console.WriteLine("Checking existence of UseStyle {0}", itemname);
			return ModPackBuilder.fileList["UseStyle"].Contains(ModPackBuilder.modpackFolder + "\\Item\\UseStyle\\" + itemname + ".cs");
		}

		public static bool TileExists(string itemname)
		{
			if (itemname == "Workbench")
			{
				itemname = "Work Bench";
			}
			Console.WriteLine("Checking existence of Tile {0}", itemname);
			return Config.tileDefs.ID.ContainsKey(itemname) || Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Tile"), itemname + ".ini", SearchOption.AllDirectories).Length > 0;
		}

		public static bool WallExists(string itemname)
		{
			Console.WriteLine("Checking existence of Wall {0}", itemname);
			return Config.wallDefs.ID.ContainsKey(itemname) || Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Wall"), itemname + ".ini", SearchOption.AllDirectories).Length > 0;
		}

		public static bool ProjExists(string itemname)
		{
			Console.WriteLine("Checking existence of Projectile {0}", itemname);
			return Config.projDefs.byName.ContainsKey(itemname) || Directory.GetFiles(Path.Combine(ModPackBuilder.modpackFolder, "Projectile"), itemname + ".ini", SearchOption.AllDirectories).Length > 0;
		}

		public static void AddSpecifiedTexture(string fullpath, BinaryWriter writer)
		{
			Stream stream = File.OpenRead(fullpath);
			MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			writer.Write(memoryStream.ToArray().Length);
			writer.Write(memoryStream.ToArray());
			memoryStream.Close();
			stream.Close();
		}

		public static void AddTexture(IniFile ini, string dir, string name, string itemtype, BinaryWriter writer)
		{
			int num = Convert.ToInt32(ini["Stats"]["type"]);
			if (num == -1)
			{
				if (!string.IsNullOrEmpty(ini["Stats"]["texture"]))
				{
					name = ini["Stats"]["texture"];
				}
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + ".png"), writer);
			}
		}

		public static void AddItemTexture(IniFile ini, string dir, string name, BinaryWriter writer)
		{
			int num = Convert.ToInt32(ini["Stats"]["type"]);
			if (num == -1)
			{
				if (!string.IsNullOrEmpty(ini["Stats"]["texture"]))
				{
					name = ini["Stats"]["texture"];
				}
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + ".png"), writer);
			}
			if (ini["Stats"]["bodySlot"] == "-2")
			{
				if (string.IsNullOrEmpty(ini["Stats"]["hasHands"]) || ini["Stats"]["hasHands"] == "False")
				{
					writer.Write(false);
				}
				else
				{
					writer.Write(true);
				}
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Body.png"), writer);
			}
			if (ini["Stats"]["bodySlot"] == "-3")
			{
				if (string.IsNullOrEmpty(ini["Stats"]["hasHands"]) || ini["Stats"]["hasHands"] == "False")
				{
					writer.Write(false);
				}
				else
				{
					writer.Write(true);
				}
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Body.png"), writer);
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Female Body.png"), writer);
			}
			if (ini["Stats"]["bodySlot"] == "-2" || ini["Stats"]["bodySlot"] == "-3")
			{
				if (ini["Stats"]["hasArms"] == "True")
				{
					writer.Write(true);
					ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Arms.png"), writer);
				}
				else
				{
					writer.Write(false);
				}
			}
			if (ini["Stats"]["legSlot"] == "-2")
			{
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Legs.png"), writer);
			}
			if (ini["Stats"]["headSlot"] == "-2")
			{
				ModPackBuilder.AddSpecifiedTexture(Path.Combine(dir, name + " Head.png"), writer);
			}
		}

		public static bool CheckProjINI(IniFile ini, string itemname)
		{
			Console.WriteLine("Checking projectile {0}", itemname);
			try
			{
				if (!ini.GetSectionNames().Contains("Stats"))
				{
					Console.WriteLine("Error: There is no Stats section defined!");
					return false;
				}
				int num = -1;
				if (!string.IsNullOrEmpty(ini["Stats"]["pretendName"]))
				{
					if (!Config.projDefs.byName.ContainsKey(ini["Stats"]["pretendName"]))
					{
						Console.WriteLine("'pretendName' refers to a non-existant item!");
						return false;
					}
					num = Config.projDefs.byName[ini["Stats"]["pretendName"]].type;
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["pretendType"]))
				{
					num = Convert.ToInt32(ini["Stats"]["pretendType"]);
				}
				if (num == 0 || num >= 604)
				{
					Console.WriteLine("'pretendType' refers to a non-existant item!");
					return false;
				}
				int num2 = -1;
				if (!string.IsNullOrEmpty(ini["Stats"]["aiPretendName"]))
				{
					if (!Config.projDefs.byName.ContainsKey(ini["Stats"]["aiPretendName"]))
					{
						Console.WriteLine("'aiPretendName' refers to a non-existant item!");
						return false;
					}
					num2 = Config.projDefs.byName[ini["Stats"]["aiPretendName"]].type;
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["aiPretendType"]))
				{
					num2 = Convert.ToInt32(ini["Stats"]["aiPretendType"]);
				}
				if (num2 == 0 || num2 >= 604)
				{
					Console.WriteLine("'aiPretendType' refers to a non-existant item!");
					return false;
				}
				int num3 = -1;
				if (!string.IsNullOrEmpty(ini["Stats"]["killPretendName"]))
				{
					if (!Config.projDefs.byName.ContainsKey(ini["Stats"]["killPretendName"]))
					{
						Console.WriteLine("'killPretendName' refers to a non-existant item!");
						return false;
					}
					num3 = Config.projDefs.byName[ini["Stats"]["killPretendName"]].type;
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["killPretendType"]))
				{
					num3 = Convert.ToInt32(ini["Stats"]["killPretendType"]);
				}
				if (num3 == 0 || num3 >= 604)
				{
					Console.WriteLine("'killPretendType' refers to a non-existant item!");
					return false;
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Exception:\n" + arg);
				return false;
			}
			return true;
		}

		public static bool CheckNPCINI(IniFile ini, string itemname)
		{
			Console.WriteLine("Checking NPC {0}", itemname);
			try
			{
				if (ini.GetSectionNames().Contains("Stats") && ini["Stats"]["type"] != "-1" && !ModPackBuilder.VanillaNPCExists(itemname))
				{
					Console.WriteLine("Error: NPC {0} doesn't exist!", itemname);
					return false;
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Exception:\n" + arg);
				return false;
			}
			return true;
		}

		public static bool BuffTextureExists(IniFile ini, string itemname)
		{
			int num = Convert.ToInt32(ini["Stats"]["id"]);
			return (num >= 0 && num < 41) || File.Exists(ModPackBuilder.modpackFolder + "\\Buff\\" + itemname + ".png");
		}

		public static bool CheckINI(IniFile ini, string[] items, string[] projs, string[] tiles, string[] walls)
		{
			if (!ini.GetSectionNames().Contains("Stats"))
			{
				return true;
			}
			foreach (string text in items)
			{
				string text2 = ini["Stats"][text];
				if (!string.IsNullOrEmpty(text2) && !ModPackBuilder.NameExists(text2, "Item", Data.ItemNames))
				{
					Console.WriteLine("Invalid value for {0}: item {1} does not exist!", text, text2);
					return false;
				}
			}
			foreach (string text3 in projs)
			{
				string text4 = ini["Stats"][text3];
				if (!string.IsNullOrEmpty(text4) && !ModPackBuilder.NameExists(text4, "Projectile", Data.ProjectileNames))
				{
					Console.WriteLine("Invalid value for {0}: projectile {1} does not exist!", text3, text4);
					return false;
				}
			}
			foreach (string text5 in tiles)
			{
				string text6 = ini["Stats"][text5];
				if (!string.IsNullOrEmpty(text6) && !ModPackBuilder.TileExists(text6))
				{
					Console.WriteLine("Invalid value for {0}: tile {1} does not exist!", text5, text6);
					return false;
				}
			}
			foreach (string text7 in walls)
			{
				string text8 = ini["Stats"][text7];
				if (!string.IsNullOrEmpty(text8) && !ModPackBuilder.WallExists(text8))
				{
					Console.WriteLine("Invalid value for {0}: wall {1} does not exist!", text7, text8);
					return false;
				}
			}
			return true;
		}

		public static bool CheckBuffINI(IniFile ini, string itemname)
		{
			try
			{
				if (!ini.GetSectionNames().Contains("Stats"))
				{
					Console.WriteLine("Error: There is no Stats section defined!");
					return false;
				}
				if (!ModPackBuilder.BuffTextureExists(ini, itemname))
				{
					Console.WriteLine("Required texture {0}.png doesn't exist!", itemname);
					return false;
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Exception:\n" + arg);
				return false;
			}
			return true;
		}

		public static bool CheckItemINI(IniFile ini, string itemname, string dir)
		{
			Console.WriteLine("Checking {0}", itemname);
			try
			{
				if (!ini.GetSectionNames().Contains("Stats"))
				{
					Console.WriteLine("Error: There is no Stats section defined!");
					return false;
				}
				int num = Convert.ToInt32(ini["Stats"]["createTile"]);
				if (num < -1 || num >= 150)
				{
					Console.WriteLine("createTile value {0} is invalid!", num);
					return false;
				}
				int num2 = Convert.ToInt32(ini["Stats"]["createWall"]);
				if (num2 < -1 || num2 >= 32)
				{
					Console.WriteLine("createWall value {0} is invalid!", num2);
					return false;
				}
				int num3 = Convert.ToInt32(ini["Stats"]["headSlot"]);
				int num4 = Convert.ToInt32(ini["Stats"]["bodySlot"]);
				int num5 = Convert.ToInt32(ini["Stats"]["legSlot"]);
				bool flag = Convert.ToBoolean(ini["Stats"]["armSlot"]);
				if (num3 == -2 && !File.Exists(Path.Combine(dir, itemname + " Head.png")))
				{
					Console.WriteLine("Required texture {0} does not exist!", Path.Combine(dir, itemname + " Head.png"));
					return false;
				}
				if (num4 == -2)
				{
					if (!File.Exists(Path.Combine(dir, itemname + " Body.png")))
					{
						Console.WriteLine("Required texture {0} does not exist!", Path.Combine(dir, itemname + " Body.png"));
						return false;
					}
					if (File.Exists(Path.Combine(dir, itemname + " Female Body.png")))
					{
						ini["Stats"]["bodySlot"] = "-3";
					}
				}
				if (num5 == -2 && !File.Exists(Path.Combine(dir, itemname + " Legs.png")))
				{
					Console.WriteLine("Required texture {0} does not exist!", Path.Combine(dir, itemname + " Legs.png"));
					return false;
				}
				if (flag && !File.Exists(Path.Combine(dir, itemname + " Arms.png")))
				{
					Console.WriteLine("Required texture {0} does not exist!", Path.Combine(dir, itemname + " Arms.png"));
					return false;
				}
				if (!string.IsNullOrEmpty(ini["Recipe"]["Items"]))
				{
					string[] array = ini["Recipe"]["Items"].Split(new char[]
					{
						','
					});
					foreach (string text in array)
					{
						string name = text.Substring(text.IndexOf(' ') + 1);
						if (!ModPackBuilder.NameExists(name, "Item", Data.ItemNames))
						{
							Console.WriteLine("Recipe Error!");
							return false;
						}
					}
				}
				if (!string.IsNullOrEmpty(ini["Recipe"]["Tiles"]))
				{
					string[] array3 = ini["Recipe"]["Tiles"].Split(new char[]
					{
						','
					});
					foreach (string text2 in array3)
					{
						if (!ModPackBuilder.TileExists(text2))
						{
							Console.WriteLine("Recipe Error: Tile {0} does not exist!", text2);
							return false;
						}
					}
				}
				string text3 = ini["Stats"]["projectile"];
				if (!string.IsNullOrEmpty(text3))
				{
					Console.WriteLine("Checking 'projectile'");
					if (!ModPackBuilder.ProjExists(text3))
					{
						return false;
					}
				}
				int num6 = -1;
				if (!string.IsNullOrEmpty(ini["Stats"]["pretendName"]))
				{
					if (!Config.itemDefs.byName.ContainsKey(ini["Stats"]["pretendName"]))
					{
						Console.WriteLine("'pretendName' refers to a non-existant item!");
						return false;
					}
					num6 = Config.itemDefs.byName[ini["Stats"]["pretendName"]].type;
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["pretendType"]))
				{
					num6 = Convert.ToInt32(ini["Stats"]["pretendType"]);
				}
				if (num6 == 0 || num6 >= 604)
				{
					Console.WriteLine("'pretendType' refers to a non-existant item!");
					return false;
				}
				if (!string.IsNullOrEmpty(ini["Stats"]["prefixType"]))
				{
					num6 = Convert.ToInt32(ini["Stats"]["prefixType"]);
					if (num6 <= 0 || num6 >= 604)
					{
						Console.WriteLine("'prefixType' refers to a non-existant item!");
						return false;
					}
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Exception:\n" + arg);
				return false;
			}
			return true;
		}

		public static bool NameExists(string name, string Type, byte[] data)
		{
			Console.WriteLine("Checking {0} {1} for existance", Type, name);
			string path = Path.Combine(ModPackBuilder.modpackFolder, Type);
			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
				foreach (string path2 in files)
				{
					if (Path.GetFileNameWithoutExtension(path2) == name)
					{
						return true;
					}
				}
			}
			if (Array.Exists<string>(Directory.GetFiles(ModPackBuilder.modpackFolder + "\\" + Type), (string s) => s == Path.GetFullPath(name + ".ini")))
			{
				return true;
			}
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						string a = binaryReader.ReadString();
						while (!(a == name))
						{
							a = binaryReader.ReadString();
						}
						binaryReader.Close();
						memoryStream.Close();
						return true;
					}
					catch (EndOfStreamException)
					{
						binaryReader.Close();
						memoryStream.Close();
					}
				}
			}
			if (Type == "Item" && Config.itemDefs.byName.ContainsKey(name))
			{
				return true;
			}
			if (Type == "NPC" && Config.npcDefs.byName.ContainsKey(name))
			{
				return true;
			}
			if (Type == "Projectile" && Config.projDefs.byName.ContainsKey(name))
			{
				return true;
			}
			Console.WriteLine("{0} {1} does not exist!", Type, name);
			return false;
		}

		public bool GenericIniToObj(string itemname, IniFile ini, List<FieldInfo> fields, object defaults, BinaryWriter binaryWriter)
		{
			Type type = defaults.GetType();
			if (!new ArrayList(ini.GetSectionNames()).Contains("Stats"))
			{
				Console.WriteLine("Error: {0}.ini does not have a [Stats] section.", itemname);
				return false;
			}
			ini["Stats"]["name"] = itemname;
			foreach (FieldInfo fieldInfo in fields)
			{
				if (!fieldInfo.IsStatic && !(fieldInfo.Name == "unloadedPrefix") && !(fieldInfo.Name == "useCode"))
				{
					string[] array = fieldInfo.ToString().Split(new char[]
					{
						' '
					});
					string text = array[0];
					string text2 = array[1];
					string text3 = ini["Stats"][text2];
					if (string.IsNullOrEmpty(text3) && text2 != "color")
					{
						object value = type.GetField(text2).GetValue(defaults);
						if (value != null)
						{
							text3 = value.ToString();
						}
						else
						{
							text3 = "";
						}
					}
					try
					{
						if (text == "Double")
						{
							binaryWriter.Write(Convert.ToDouble(text3));
						}
						else if (text == "Int32")
						{
							binaryWriter.Write(Convert.ToInt32(text3));
						}
						else if (text == "Single")
						{
							binaryWriter.Write(Convert.ToSingle(text3, CultureInfo.InvariantCulture.NumberFormat));
						}
						else if (text == "Boolean")
						{
							binaryWriter.Write(Convert.ToBoolean(text3));
						}
						else if (text == "Microsoft.Xna.Framework.Color")
						{
							if (string.IsNullOrEmpty(text3))
							{
								text3 = "0,0,0,0";
							}
							string[] array2 = text3.Split(new char[]
							{
								','
							});
							binaryWriter.Write(Convert.ToByte(array2[0]));
							binaryWriter.Write(Convert.ToByte(array2[1]));
							binaryWriter.Write(Convert.ToByte(array2[2]));
							binaryWriter.Write(Convert.ToByte(array2[3]));
						}
						else if (text == "System.String")
						{
							if (text3 == null)
							{
								binaryWriter.Write("");
							}
							else
							{
								binaryWriter.Write(text3);
							}
						}
					}
					catch (Exception arg)
					{
						Console.WriteLine("Error:");
						Console.WriteLine(string.Concat(new string[]
						{
							text,
							" ",
							text2,
							": ",
							text3
						}));
						Console.WriteLine("Exception: " + arg);
						return false;
					}
				}
			}
			return true;
		}

		public static bool CompileCSCheck(string className, string codePath, string type)
		{
			if (ModPackBuilder.csCheck)
			{
				Console.WriteLine("Test Compiling " + codePath);
				try
				{
					if (File.Exists(codePath))
					{
						ModPackBuilder.compilePerm(className, codePath, type, false);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Concat(new object[]
					{
						"Error compiling ",
						codePath,
						"\n",
						ex
					}));
					return false;
				}
				return true;
			}
			return true;
		}

		public static bool CompileBooCheck(string className, string codePath, string type)
		{
			try
			{
				if (File.Exists(codePath))
				{
					ModPackBuilder.compilePermBoo(className, codePath, type, false);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"Error compiling ",
					codePath,
					"\n",
					ex
				}));
				return false;
			}
			return true;
		}

		public static bool CompileNormalCSToObj(string source, BinaryWriter binaryWriter)
		{
			try
			{
				binaryWriter.Write(true);
				string path = ModPackBuilder.compileNormal(source);
				byte[] array = File.ReadAllBytes(path);
				binaryWriter.Write(array.Length);
				binaryWriter.Write(array);
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error compiling code\n" + arg);
				return false;
			}
			return true;
		}

		public static bool CompileNormalCSToObj(string[] sourceFiles, BinaryWriter binaryWriter)
		{
			try
			{
				binaryWriter.Write(true);
				string path = ModPackBuilder.compileNormal(sourceFiles);
				byte[] array = File.ReadAllBytes(path);
				binaryWriter.Write(array.Length);
				binaryWriter.Write(array);
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error compiling code\n" + arg);
				return false;
			}
			return true;
		}

		public static bool CompileBooToObj(string source, BinaryWriter binaryWriter)
		{
			try
			{
				binaryWriter.Write(true);
				string path = ModPackBuilder.CompileNormalBoo(source);
				byte[] array = File.ReadAllBytes(path);
				binaryWriter.Write(array.Length);
				binaryWriter.Write(array);
				File.Delete(path);
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error compiling code\n" + arg);
				return false;
			}
			return true;
		}

		public bool PrefixIniToObj(BinaryWriter binaryWriter)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Prefix");
			binaryWriter.Write(Prefix.saveVersion);
			if (!Directory.Exists(path))
			{
				binaryWriter.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			binaryWriter.Write(files.Length);
			foreach (string text in files)
			{
				Console.WriteLine("Processing " + text);
				try
				{
					Prefix prefix = new Prefix(IniFile.FromFile(text), Path.GetFileNameWithoutExtension(text));
					prefix.Save(binaryWriter);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Prefix load error on file " + text + "\n" + ex.Message);
					return false;
				}
			}
			return true;
		}

		public bool ItemIniToObj(BinaryWriter binaryWriter)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Item");
			if (!Directory.Exists(path))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.ItemDefaults itemDefaults = new Config.ItemDefaults();
			Type type = itemDefaults.GetType();
			int num = 0;
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
			binaryWriter.Write(files.Length);
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string directoryName = Path.GetDirectoryName(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				bool result;
				if (!ModPackBuilder.CheckItemINI(iniFile, fileNameWithoutExtension, directoryName))
				{
					result = false;
				}
				else if (!ModPackBuilder.CheckINI(iniFile, new string[]
				{
					"pretendName"
				}, new string[]
				{
					"projectile"
				}, new string[]
				{
					"mechToggle",
					"doorToggle"
				}, new string[0]))
				{
					result = false;
				}
				else if (!this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, itemDefaults, binaryWriter))
				{
					result = false;
				}
				else
				{
					ModPackBuilder.AddItemTexture(iniFile, directoryName, fileNameWithoutExtension, binaryWriter);
					if (new ItemBuilder
					{
						useStyleExists = new Builder.Checker(ModPackBuilder.UseStyleExists),
						itemExists = new Builder.Checker(ModPackBuilder.ItemExists),
						holdStyleExists = new Builder.Checker(ModPackBuilder.HoldStyleExists),
						wallExists = new Builder.Checker(ModPackBuilder.WallExists),
						tileExists = new Builder.Checker(ModPackBuilder.TileExists)
					}.ReadINI(binaryWriter, iniFile, ModPackBuilder.modpackFolder))
					{
						int num2 = 1;
						while (!string.IsNullOrEmpty(iniFile["Recipe" + ((num2 == 1) ? "" : string.Concat(num2))]["Items"]))
						{
							string sectionName = "Recipe" + ((num2 == 1) ? "" : string.Concat(num2));
							num++;
							binaryWriter2.Write(fileNameWithoutExtension);
							int value = 1;
							if (!string.IsNullOrEmpty(iniFile[sectionName]["Amount"]))
							{
								value = Convert.ToInt32(iniFile[sectionName]["Amount"]);
							}
							binaryWriter2.Write(value);
							bool value2 = false;
							if (!string.IsNullOrEmpty(iniFile[sectionName]["needWater"]))
							{
								value2 = Convert.ToBoolean(iniFile[sectionName]["needWater"]);
							}
							binaryWriter2.Write(value2);
							bool value3 = false;
							if (!string.IsNullOrEmpty(iniFile[sectionName]["needLava"]))
							{
								value3 = Convert.ToBoolean(iniFile[sectionName]["needLava"]);
							}
							binaryWriter2.Write(value3);
							string[] array2 = iniFile[sectionName]["Items"].Split(new char[]
							{
								','
							});
							binaryWriter2.Write(array2.Length);
							foreach (string text2 in array2)
							{
								int value4 = Convert.ToInt32(text2.Split(new char[]
								{
									' '
								})[0]);
								string text3 = text2.Substring(text2.IndexOf(' ') + 1);
								if (!ModPackBuilder.ItemExists(text3))
								{
									Console.WriteLine(string.Concat(new string[]
									{
										"Error in ",
										fileNameWithoutExtension,
										" recipe: Item ",
										text3,
										" does not exist!"
									}));
									return false;
								}
								binaryWriter2.Write(text3);
								binaryWriter2.Write(value4);
							}
							if (!string.IsNullOrEmpty(iniFile[sectionName]["Tiles"]))
							{
								array2 = iniFile[sectionName]["Tiles"].Split(new char[]
								{
									','
								});
							}
							else
							{
								array2 = new string[0];
							}
							binaryWriter2.Write(array2.Length);
							foreach (string text4 in array2)
							{
								if (!ModPackBuilder.TileExists(text4))
								{
									Console.WriteLine(string.Concat(new string[]
									{
										"Error in ",
										fileNameWithoutExtension,
										" recipe: Tile ",
										text4,
										" does not exist!"
									}));
									return false;
								}
								binaryWriter2.Write(text4);
							}
							num2++;
						}
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			binaryWriter2.Close();
			binaryWriter.Write(num);
			binaryWriter.Write(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
			return true;
		}

		public bool BuffIniToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Buff");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.BuffDefaults buffDefaults = new Config.BuffDefaults();
			Type type = buffDefaults.GetType();
			writer.Write(files.Length);
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				bool result;
				if (!ModPackBuilder.CheckBuffINI(iniFile, fileNameWithoutExtension))
				{
					result = false;
				}
				else
				{
					if (this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, buffDefaults, writer))
					{
						int num = Convert.ToInt32(iniFile["Stats"]["id"]);
						if (num == -1)
						{
							ModPackBuilder.AddSpecifiedTexture(ModPackBuilder.modpackFolder + "\\Buff\\" + fileNameWithoutExtension + ".png", writer);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["dontDisplayTime"]))
						{
							writer.Write(Convert.ToBoolean(iniFile["Stats"]["dontDisplayTime"]));
						}
						else
						{
							writer.Write(false);
						}
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		public bool ProjIniToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Projectile");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.ProjDefaults projDefaults = new Config.ProjDefaults();
			Type type = projDefaults.GetType();
			writer.Write(files.Length);
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string directoryName = Path.GetDirectoryName(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				bool result;
				if (!ModPackBuilder.CheckProjINI(iniFile, fileNameWithoutExtension))
				{
					result = false;
				}
				else
				{
					if (this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, projDefaults, writer))
					{
						ModPackBuilder.AddTexture(iniFile, directoryName, fileNameWithoutExtension, "Projectile", writer);
						if (!string.IsNullOrEmpty(iniFile["Stats"]["pretendName"]))
						{
							if (!ModPackBuilder.NameExists(iniFile["Stats"]["pretendName"], "Projectile", Data.ProjectileNames))
							{
								Console.WriteLine("pretendName " + iniFile["Stats"]["pretendName"] + " does not exist!");
								return false;
							}
							writer.Write(true);
							writer.Write(Config.projDefs.byName[iniFile["Stats"]["pretendName"]].type);
						}
						else if (!string.IsNullOrEmpty(iniFile["Stats"]["pretendType"]) && iniFile["Stats"]["pretendType"] != "-1")
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["pretendType"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["aiPretendName"]))
						{
							writer.Write(true);
							writer.Write(Config.projDefs.byName[iniFile["Stats"]["aiPretendName"]].type);
						}
						else if (!string.IsNullOrEmpty(iniFile["Stats"]["aiPretendType"]) && iniFile["Stats"]["aiPretendType"] != "-1")
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["aiPretendType"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["killPretendName"]))
						{
							writer.Write(true);
							writer.Write(Config.projDefs.byName[iniFile["Stats"]["killPretendName"]].type);
						}
						else if (!string.IsNullOrEmpty(iniFile["Stats"]["killPretendType"]) && iniFile["Stats"]["killPretendType"] != "-1")
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["killPretendType"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["frameCount"]))
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["frameCount"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["frameOffsetX"]))
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["frameOffsetX"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["frameOffsetY"]))
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["frameOffsetY"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["drawPretendType"]))
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["drawPretendType"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["displayName"]))
						{
							writer.Write(true);
							writer.Write(iniFile["Stats"]["displayName"]);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["color"]))
						{
							writer.Write(true);
							string[] array2 = iniFile["Stats"]["color"].Split(new char[]
							{
								','
							});
							writer.Write(Convert.ToByte(array2[0]));
							writer.Write(Convert.ToByte(array2[1]));
							writer.Write(Convert.ToByte(array2[2]));
							writer.Write(Convert.ToByte(array2[3]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["hurtsTiles"]))
						{
							writer.Write(true);
							if (iniFile["Stats"]["hurtsTiles"] == "True")
							{
								writer.Write(true);
							}
							else
							{
								writer.Write(false);
							}
						}
						else
						{
							writer.Write(false);
						}
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		public bool HoldStyleToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Item", "HoldStyle");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.cs");
			writer.Write(files.Length);
			foreach (string path2 in files)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
				writer.Write(fileNameWithoutExtension);
			}
			return true;
		}

		public bool UseStyleToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Item", "UseStyle");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.cs");
			writer.Write(files.Length);
			foreach (string path2 in files)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
				writer.Write(fileNameWithoutExtension);
			}
			return true;
		}

		public bool NPCIniToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "NPC");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.NPCDefaults npcdefaults = new Config.NPCDefaults();
			Type type = npcdefaults.GetType();
			writer.Write(files.Length);
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string directoryName = Path.GetDirectoryName(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				bool result;
				if (!ModPackBuilder.CheckNPCINI(iniFile, fileNameWithoutExtension))
				{
					result = false;
				}
				else
				{
					bool flag = false;
					if (ModPackBuilder.VanillaNPCExists(fileNameWithoutExtension) || iniFile.GetSectionNames().Contains("Stats"))
					{
						if (iniFile.GetSectionNames().Contains("Stats"))
						{
							if (iniFile["Stats"]["type"] == "-1")
							{
								flag = true;
							}
							writer.Write(true);
							if (!this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, npcdefaults, writer))
							{
								return false;
							}
						}
						else
						{
							flag = false;
							writer.Write(false);
							writer.Write(fileNameWithoutExtension);
						}
						for (int j = 1; j < 41; j++)
						{
							bool value = false;
							if (iniFile["Buff Immunities"][Main.buffName[j]] == "True")
							{
								value = true;
							}
							writer.Write(value);
						}
						ModPackBuilder.AddTexture(iniFile, directoryName, fileNameWithoutExtension, "NPC", writer);
						if (flag)
						{
							if (string.IsNullOrEmpty(iniFile["Stats"]["frameCount"]))
							{
								Console.WriteLine("Required attribute 'frameCount' isn't specified!");
								return false;
							}
							writer.Write(Convert.ToInt32(iniFile["Stats"]["frameCount"]));
							if (!string.IsNullOrEmpty(iniFile["Stats"]["animationType"]))
							{
								writer.Write(true);
								writer.Write(Convert.ToInt32(iniFile["Stats"]["animationType"]));
							}
							else
							{
								writer.Write(false);
							}
						}
						if (iniFile["Drops"].GetKeys().Count > 0)
						{
							writer.Write(true);
							DropTable dropTable = new DropTable(iniFile["Drops"]);
							foreach (object obj in dropTable.drops)
							{
								Drop drop = (Drop)obj;
								if (!ModPackBuilder.NameExists(drop.name, "Item", Data.ItemNames))
								{
									Console.WriteLine("Drop item " + drop.name + " does not exist!");
									return false;
								}
							}
							dropTable.Save(writer);
						}
						else
						{
							writer.Write(false);
						}
						if (flag && iniFile["Stats"]["townNPC"] == "True")
						{
							string text2 = Path.Combine(directoryName, fileNameWithoutExtension + " Head.png");
							if (!File.Exists(text2))
							{
								Console.WriteLine("Error: Required Texture " + text2 + " is missing!");
								return false;
							}
							ModPackBuilder.AddSpecifiedTexture(text2, writer);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["soundHitName"]))
						{
							string text3 = Path.Combine(ModPackBuilder.modpackFolder, "Sound", iniFile["Stats"]["soundHitName"] + ".wav");
							if (!File.Exists(text3))
							{
								Console.WriteLine("Sound file " + text3 + " does not exist!");
								return false;
							}
							writer.Write(true);
							writer.Write(iniFile["Stats"]["soundHitName"]);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["soundKilledName"]))
						{
							string text4 = Path.Combine(ModPackBuilder.modpackFolder, "Sound", iniFile["Stats"]["soundKilledName"] + ".wav");
							if (!File.Exists(text4))
							{
								Console.WriteLine("Sound file " + text4 + " does not exist!");
								return false;
							}
							writer.Write(true);
							writer.Write(iniFile["Stats"]["soundKilledName"]);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["aiPretendType"]))
						{
							int num = Convert.ToInt32(iniFile["Stats"]["aiPretendType"]);
							if (num < 0 || num > 147)
							{
								Console.WriteLine("Invalid aiPretendType value.");
								return false;
							}
							writer.Write(true);
							writer.Write(num);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["music"]))
						{
							writer.Write(true);
							writer.Write(Convert.ToInt32(iniFile["Stats"]["music"]));
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["musicName"]))
						{
							writer.Write(true);
							writer.Write(iniFile["Stats"]["musicName"]);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["SpawnBiomes"]))
						{
							writer.Write(true);
							writer.Write(iniFile["Stats"]["SpawnBiomes"]);
						}
						else
						{
							writer.Write(false);
						}
						if (!string.IsNullOrEmpty(iniFile["Stats"]["dontDrawLifeText"]))
						{
							writer.Write(true);
							if (iniFile["Stats"]["dontdrawLifeText"] == "True")
							{
								writer.Write(true);
							}
							else
							{
								writer.Write(false);
							}
						}
						else
						{
							writer.Write(false);
						}
						i++;
						continue;
					}
					Console.WriteLine("Custom NPC " + fileNameWithoutExtension + " must have a Stats section");
					result = false;
				}
				return result;
			}
			return true;
		}

		public bool IniToObjSingle(string objFile, IniFile ini)
		{
			using (FileStream fileStream = new FileStream(objFile, FileMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(Constants.version);
					if (!string.IsNullOrEmpty(ini["Settings"]["version"]))
					{
						binaryWriter.Write(Convert.ToInt32(ini["Settings"]["version"]));
					}
					else
					{
						binaryWriter.Write(0);
					}
					if (!string.IsNullOrEmpty(ini["Settings"]["DLversion"]) && !string.IsNullOrEmpty(ini["Settings"]["url"]))
					{
						binaryWriter.Write(true);
						binaryWriter.Write(ini["Settings"]["DLversion"]);
						binaryWriter.Write(ini["Settings"]["url"]);
					}
					else
					{
						binaryWriter.Write(false);
					}
					try
					{
						bool flag = false;
						ArrayList arrayList = new ArrayList(Directory.GetFiles(ModPackBuilder.modpackFolder, "*.boo", SearchOption.AllDirectories));
						if (arrayList.Count > 0)
						{
							flag = true;
						}
						if (flag)
						{
							if (!ModPackBuilder.BooToObj(binaryWriter))
							{
								binaryWriter.Close();
								fileStream.Close();
								if (File.Exists(objFile))
								{
									File.Delete(objFile);
								}
								return false;
							}
						}
						else if (!ModPackBuilder.DLLToObj(binaryWriter))
						{
							binaryWriter.Close();
							fileStream.Close();
							if (File.Exists(objFile))
							{
								File.Delete(objFile);
							}
							return false;
						}
						ModPackBuilder.SoundToObj(binaryWriter);
						if (!this.TileIniToObj(binaryWriter) || !this.WallIniToObj(binaryWriter) || !this.ProjIniToObj(binaryWriter) || !this.HoldStyleToObj(binaryWriter) || !this.UseStyleToObj(binaryWriter) || !this.PrefixIniToObj(binaryWriter) || !this.ItemIniToObj(binaryWriter) || !this.BuffIniToObj(binaryWriter) || !this.NPCIniToObj(binaryWriter))
						{
							binaryWriter.Close();
							fileStream.Close();
							if (File.Exists(objFile))
							{
								File.Delete(objFile);
							}
							return false;
						}
						ModPackBuilder.GoreToObj(binaryWriter);
						fileStream.Close();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						binaryWriter.Close();
						fileStream.Close();
						if (File.Exists(objFile))
						{
							File.Delete(objFile);
						}
						if (File.Exists(objFile + ".temp"))
						{
							File.Delete(objFile + ".temp");
						}
						if (File.Exists(objFile + ".counts"))
						{
							File.Delete(objFile + ".counts");
						}
						return false;
					}
					binaryWriter.Close();
					fileStream.Close();
				}
			}
			return true;
		}

		public bool IniToObj()
		{
			if (!Directory.Exists(ModPackBuilder.modpackFolder))
			{
				Console.WriteLine("Source directory " + ModPackBuilder.modpackFolder + " doesn't exist!");
				return false;
			}
			if (!Directory.Exists(ModPackBuilder.outputFolder))
			{
				Console.WriteLine("Output directory " + ModPackBuilder.outputFolder + " doesn't exist!");
				return false;
			}
			string text = ModPackBuilder.modpackFolder + Path.DirectorySeparatorChar + "Config.ini";
			string text2 = ModPackBuilder.modpackFolder + Path.DirectorySeparatorChar + "Settings.json";
			string text3 = Path.Combine(Constants.tConfigFolder, "ModPack_Objs");
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			IniFile iniFile = IniFile.FromFile(text);
			if (File.Exists(text2))
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(text2))
					{
						JsonData jsonData = JsonMapper.ToObject(streamReader);
						jsonData.ToString();
					}
				}
				catch (Exception)
				{
					Console.WriteLine("Error: Invalid JSON file: Settings.json");
					return false;
				}
			}
			string text4 = Path.Combine(ModPackBuilder.outputFolder, (ModPackBuilder.modpack + " " + ModPackBuilder.Override).Trim() + ".obj");
			ArrayList arrayList = new ArrayList();
			if (string.IsNullOrEmpty(ModPackBuilder.Override) && iniFile["Settings"]["type"] == "choice")
			{
				using (IEnumerator<string> enumerator = iniFile["Choice"].GetKeys().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text5 = enumerator.Current;
						if (text5 == "default")
						{
							ModPackBuilder.Override = "";
							ModPackBuilder.OverridePath = "";
						}
						else
						{
							ModPackBuilder.Override = text5;
							ModPackBuilder.OverridePath = string.Concat(new object[]
							{
								ModPackBuilder.modpack,
								Path.DirectorySeparatorChar,
								"Override",
								Path.DirectorySeparatorChar,
								ModPackBuilder.Override
							});
						}
						string text6 = Path.Combine(text3, (ModPackBuilder.modpack + " " + ModPackBuilder.Override).Trim() + ".obj");
						if (!this.IniToObjSingle(text6, iniFile))
						{
							return false;
						}
						arrayList.Add(text6);
					}
					goto IL_294;
				}
			}
			string text7 = Path.Combine(text3, (ModPackBuilder.modpack + " " + ModPackBuilder.Override).Trim() + ".obj");
			if (!this.IniToObjSingle(text7, iniFile))
			{
				return false;
			}
			arrayList.Add(text7);
			IL_294:
			Console.WriteLine("Compressing data...");
			string text8 = Config.Get7Zip();
			if (File.Exists(text8))
			{
				string text9 = "";
				foreach (object obj in arrayList)
				{
					string str = (string)obj;
					text9 = text9 + "\"" + str + "\" ";
				}
				if (File.Exists(text))
				{
					text9 = text9 + "\"" + text + "\" ";
				}
				if (File.Exists(text2))
				{
					text9 = text9 + "\"" + text2 + "\" ";
				}
				Console.WriteLine("Using 7-zip at " + text8);
				Process process = new Process();
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.FileName = text8;
				process.StartInfo.Arguments = string.Concat(new string[]
				{
					"a -t7z \"",
					text4,
					".temp\" \"",
					ModPackBuilder.modpackFolder,
					"\" ",
					text9
				});
				process.StartInfo.CreateNoWindow = true;
				process.Start();
				process.WaitForExit();
			}
			else
			{
				Console.WriteLine("Failed to find 7-zip, using plain zip format...");
				ZipFile zipFile = new ZipFile();
				zipFile.AddDirectory(ModPackBuilder.modpackFolder, Path.GetFileName(ModPackBuilder.modpackFolder));
				foreach (object obj2 in arrayList)
				{
					string text10 = (string)obj2;
					zipFile.AddFile(text10, "");
				}
				if (File.Exists(text))
				{
					zipFile.AddFile(text, "");
				}
				if (File.Exists(text2))
				{
					zipFile.AddFile(text2, "");
				}
				zipFile.Save(text4 + ".temp");
				zipFile.Dispose();
			}
			foreach (object obj3 in arrayList)
			{
				string path = (string)obj3;
				File.Delete(path);
			}
			if (File.Exists(text4))
			{
				File.Delete(text4);
			}
			File.Move(text4 + ".temp", text4);
			return true;
		}

		public bool WallIniToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Wall");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.WallDefaults wallDefaults = new Config.WallDefaults();
			Type type = wallDefaults.GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			writer.Write(files.Length);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string directoryName = Path.GetDirectoryName(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				iniFile["Stats"]["name"] = fileNameWithoutExtension;
				Console.WriteLine("Checking Wall " + fileNameWithoutExtension);
				bool result;
				if (!iniFile.GetSectionNames().Contains("Stats"))
				{
					Console.WriteLine("Error: There is no Stats section defined!");
					result = false;
				}
				else if (!string.IsNullOrEmpty(iniFile["Stats"]["DropName"]) && !ModPackBuilder.ItemExists(iniFile["Stats"]["DropName"]))
				{
					result = false;
				}
				else
				{
					if (string.IsNullOrEmpty(iniFile["Stats"]["id"]))
					{
						iniFile["Stats"]["id"] = "-1";
					}
					if (this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, wallDefaults, writer))
					{
						if (iniFile["Stats"]["id"] == "-1")
						{
							ModPackBuilder.AddSpecifiedTexture(Path.Combine(directoryName, fileNameWithoutExtension + ".png"), writer);
							int[] array2 = Config.CalculateAverageColor((Bitmap)Image.FromFile(Path.Combine(directoryName, fileNameWithoutExtension + ".png")));
							writer.Write((byte)array2[0]);
							writer.Write((byte)array2[1]);
							writer.Write((byte)array2[2]);
						}
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		public bool TileIniToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Tile");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return true;
			}
			string[] files = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);
			Config.TileDefaults tileDefaults = new Config.TileDefaults();
			Type type = tileDefaults.GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			writer.Write(files.Length);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				string directoryName = Path.GetDirectoryName(text);
				IniFile iniFile = ModPackBuilder.LoadINI(text, fileNameWithoutExtension);
				iniFile["Stats"]["name"] = fileNameWithoutExtension;
				Console.WriteLine("Checking Tile " + fileNameWithoutExtension);
				bool result;
				if (!iniFile.GetSectionNames().Contains("Stats"))
				{
					Console.WriteLine("Error: There is no Stats section defined!");
					result = false;
				}
				else if (!ModPackBuilder.CheckINI(iniFile, new string[]
				{
					"DropName"
				}, new string[0], new string[]
				{
					"mechToggle",
					"doorToggle"
				}, new string[0]))
				{
					result = false;
				}
				else
				{
					if (!string.IsNullOrEmpty(iniFile["Stats"]["furniture"]))
					{
						string a = iniFile["Stats"]["furniture"];
						if (a == "chair")
						{
							iniFile["Stats"]["furniture"] = "1";
						}
						else if (a == "table")
						{
							iniFile["Stats"]["furniture"] = "2";
						}
						else if (a == "torch")
						{
							iniFile["Stats"]["furniture"] = "3";
						}
						else
						{
							if (!(a == "door"))
							{
								Console.WriteLine("'furniture' stat is invalid! Must be 'chair' or 'table'");
								return false;
							}
							iniFile["Stats"]["furniture"] = "4";
						}
					}
					else
					{
						iniFile["Stats"]["furniture"] = "0";
					}
					if (!this.GenericIniToObj(fileNameWithoutExtension, iniFile, list, tileDefaults, writer))
					{
						result = false;
					}
					else
					{
						if (iniFile["Stats"]["id"] == "-1")
						{
							string str = fileNameWithoutExtension;
							if (!string.IsNullOrEmpty(iniFile["Stats"]["texture"]))
							{
								str = iniFile["Stats"]["texture"];
							}
							ModPackBuilder.AddSpecifiedTexture(Path.Combine(directoryName, str + ".png"), writer);
							int[] array2 = Config.CalculateAverageColor((Bitmap)Image.FromFile(Path.Combine(directoryName, str + ".png")));
							writer.Write((byte)array2[0]);
							writer.Write((byte)array2[1]);
							writer.Write((byte)array2[2]);
						}
						if (new TileBuilder
						{
							useStyleExists = new Builder.Checker(ModPackBuilder.UseStyleExists),
							itemExists = new Builder.Checker(ModPackBuilder.ItemExists),
							holdStyleExists = new Builder.Checker(ModPackBuilder.HoldStyleExists),
							wallExists = new Builder.Checker(ModPackBuilder.WallExists),
							tileExists = new Builder.Checker(ModPackBuilder.TileExists)
						}.ReadINI(writer, iniFile, ModPackBuilder.modpackFolder))
						{
							i++;
							continue;
						}
						result = false;
					}
				}
				return result;
			}
			return true;
		}

		public static void GoreToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Gore");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return;
			}
			string[] files = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
			writer.Write(files.Length);
			foreach (string text in files)
			{
				writer.Write(Path.GetFileNameWithoutExtension(text));
				ModPackBuilder.AddSpecifiedTexture(text, writer);
			}
		}

		public static void SoundToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "Sound");
			if (!Directory.Exists(path))
			{
				writer.Write(0);
				return;
			}
			string[] files = Directory.GetFiles(path, "*.wav", SearchOption.AllDirectories);
			writer.Write(files.Length);
			foreach (string path2 in files)
			{
				byte[] array2 = File.ReadAllBytes(path2);
				writer.Write(Path.GetFileNameWithoutExtension(path2));
				writer.Write(array2.Length);
				writer.Write(array2);
			}
		}

		public static bool BooToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "code.dll");
			bool flag = false;
			if (!File.Exists(path))
			{
				string text = "namespace Terraria\n" + ModPackBuilder.CodeUsingBoo();
				bool flag2 = false;
				foreach (string text2 in ModPackBuilder.fileList.Keys)
				{
					foreach (object obj in ModPackBuilder.fileList[text2])
					{
						string path2 = (string)obj;
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
						string text3 = Path.Combine(Path.GetDirectoryName(path2), fileNameWithoutExtension + ".boo");
						if (File.Exists(text3))
						{
							Console.WriteLine("Compiling " + text3);
							if (!ModPackBuilder.CompileBooCheck(fileNameWithoutExtension, text3, text2))
							{
								return false;
							}
							text += ModPackBuilder.HandleCodeBoo(fileNameWithoutExtension, text3, text2);
							flag2 = true;
						}
						string text4 = Path.Combine(Path.GetDirectoryName(path2), fileNameWithoutExtension + ".ini");
						if (File.Exists(text4))
						{
							IniFile iniFile = ModPackBuilder.LoadINI(text4, fileNameWithoutExtension);
							string text5 = iniFile["Stats"]["code"];
							if (!string.IsNullOrEmpty(text5) && File.Exists(Path.Combine(Path.GetDirectoryName(path2), text5 + ".boo")))
							{
								string text6 = Path.Combine(Path.GetDirectoryName(path2), text5 + ".boo");
								Console.WriteLine("Compiling " + text6);
								if (!ModPackBuilder.CompileBooCheck(fileNameWithoutExtension, text6, text2))
								{
									return false;
								}
								text += ModPackBuilder.HandleCodeBoo(fileNameWithoutExtension, text6, text2);
								flag2 = true;
							}
						}
					}
					if (text2 == "Item" || text2 == "NPC" || text2 == "Projectile" || text2 == "Tile" || text2 == "Buff")
					{
						string text7 = Path.Combine(ModPackBuilder.modpackFolder, text2, text2 + ".boo");
						if (File.Exists(text7))
						{
							Console.WriteLine("Compiling " + text7);
							if (!ModPackBuilder.CompileBooCheck("Global", text7, text2))
							{
								return false;
							}
							text += ModPackBuilder.HandleCodeBoo("Global", text7, text2);
							flag2 = true;
						}
					}
				}
				string text8 = string.Concat(new object[]
				{
					ModPackBuilder.modpackFolder,
					Path.DirectorySeparatorChar,
					"Global",
					Path.DirectorySeparatorChar,
					"World.boo"
				});
				string text9 = string.Concat(new object[]
				{
					ModPackBuilder.modpackFolder,
					Path.DirectorySeparatorChar,
					"Global",
					Path.DirectorySeparatorChar,
					"Player.boo"
				});
				if (File.Exists(text8))
				{
					Console.WriteLine("Compiling " + text8);
					if (!ModPackBuilder.CompileBooCheck("ModWorld", text8, ""))
					{
						return false;
					}
					text += ModPackBuilder.HandleCodeBoo("ModWorld", text8, "");
					flag2 = true;
				}
				if (File.Exists(text9))
				{
					Console.WriteLine("Compiling " + text9);
					if (!ModPackBuilder.CompileBooCheck("ModPlayer", text9, ""))
					{
						return false;
					}
					text += ModPackBuilder.HandleCodeBoo("ModPlayer", text9, "");
					flag2 = true;
				}
				text += "\n";
				if (!flag2)
				{
					goto IL_3EE;
				}
				if (!ModPackBuilder.CompileBooToObj(text, writer))
				{
					return false;
				}
				flag = true;
				goto IL_3EE;
			}
			writer.Write(true);
			byte[] array = File.ReadAllBytes(path);
			writer.Write(array.Length);
			writer.Write(array);
			flag = true;
			IL_3EE:
			if (!flag)
			{
				writer.Write(false);
			}
			return true;
		}

		public static bool DLLToObj(BinaryWriter writer)
		{
			string path = Path.Combine(ModPackBuilder.modpackFolder, "code.dll");
			bool flag = false;
			if (!File.Exists(path))
			{
				List<string> list = new List<string>();
				Console.WriteLine("Compiling all CS files...");
				string text = ModPackBuilder.CodeUsing();
				text += "\n namespace Terraria {\n";
				bool flag2 = false;
				foreach (string text2 in ModPackBuilder.fileList.Keys)
				{
					foreach (object obj in ModPackBuilder.fileList[text2])
					{
						string path2 = (string)obj;
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path2);
						string text3 = Path.Combine(Path.GetDirectoryName(path2), fileNameWithoutExtension + ".cs");
						string text4 = "";
						string text5 = Path.Combine(Path.GetDirectoryName(path2), fileNameWithoutExtension + ".ini");
						if (File.Exists(text5))
						{
							IniFile iniFile = ModPackBuilder.LoadINI(text5, fileNameWithoutExtension);
							text4 = iniFile["Stats"]["code"];
						}
						string text6 = text3;
						if (!string.IsNullOrEmpty(text4) && File.Exists(Path.Combine(Path.GetDirectoryName(text6), text4 + ".cs")))
						{
							text6 = Path.Combine(Path.GetDirectoryName(text6), text4 + ".cs");
						}
						string text7 = text6.Replace(ModPackBuilder.modpack, ModPackBuilder.OverridePath);
						if (!string.IsNullOrEmpty(ModPackBuilder.Override) && File.Exists(text7))
						{
							text6 = text7;
						}
						if (!string.IsNullOrEmpty(text6) && File.Exists(text6))
						{
							if (!ModPackBuilder.CompileCSCheck(fileNameWithoutExtension, text6, text2))
							{
								return false;
							}
							text += ModPackBuilder.HandleCode(fileNameWithoutExtension, text6, text2);
							flag2 = true;
							list.Add(text6);
						}
					}
					if (text2 == "Item" || text2 == "NPC" || text2 == "Projectile" || text2 == "Tile" || text2 == "Buff" || text2 == "Prefix")
					{
						string text8 = Path.Combine(ModPackBuilder.modpackFolder, text2, text2 + ".cs");
						string text9 = text8.Replace(ModPackBuilder.modpack, string.Concat(new object[]
						{
							ModPackBuilder.modpack,
							Path.DirectorySeparatorChar,
							"Override",
							Path.DirectorySeparatorChar,
							ModPackBuilder.Override
						}));
						if (!string.IsNullOrEmpty(ModPackBuilder.Override) && File.Exists(text9))
						{
							text8 = text9;
						}
						if (File.Exists(text8))
						{
							if (!ModPackBuilder.CompileCSCheck("Global", text8, text2))
							{
								return false;
							}
							text += ModPackBuilder.HandleCode("Global", text8, text2);
							flag2 = true;
							list.Add(text8);
						}
					}
				}
				string[] array;
				if (ModPackBuilder.customCompile)
				{
					array = Directory.GetFiles(ModPackBuilder.modpackFolder + Path.DirectorySeparatorChar + "Global", "*.cs", SearchOption.AllDirectories);
				}
				else
				{
					array = new string[]
					{
						string.Concat(new object[]
						{
							ModPackBuilder.modpackFolder,
							Path.DirectorySeparatorChar,
							"Global",
							Path.DirectorySeparatorChar,
							"World.cs"
						}),
						string.Concat(new object[]
						{
							ModPackBuilder.modpackFolder,
							Path.DirectorySeparatorChar,
							"Global",
							Path.DirectorySeparatorChar,
							"Player.cs"
						}),
						string.Concat(new object[]
						{
							ModPackBuilder.modpackFolder,
							Path.DirectorySeparatorChar,
							"Global",
							Path.DirectorySeparatorChar,
							"Generic.cs"
						})
					};
				}
				string[] array2 = new string[]
				{
					"ModWorld",
					"ModPlayer",
					"ModGeneric"
				};
				for (int i = 0; i < array.Length; i++)
				{
					string text10 = array[i];
					string text11 = text10.Replace(ModPackBuilder.modpack, ModPackBuilder.OverridePath);
					if (!string.IsNullOrEmpty(ModPackBuilder.Override) && File.Exists(text11))
					{
						text10 = text11;
					}
					if (File.Exists(text10))
					{
						if (!ModPackBuilder.customCompile)
						{
							if (!ModPackBuilder.CompileCSCheck(array2[i], text10, ""))
							{
								return false;
							}
							text += ModPackBuilder.HandleCode(array2[i], text10, "");
						}
						flag2 = true;
						list.Add(text10);
					}
				}
				text += "\n}\n";
				if (flag2)
				{
					if (!ModPackBuilder.customCompile)
					{
						if (!ModPackBuilder.CompileNormalCSToObj(text, writer))
						{
							return false;
						}
					}
					else if (!ModPackBuilder.CompileNormalCSToObj(list.ToArray(), writer))
					{
						return false;
					}
					flag = true;
				}
				goto IL_547;
			}
			writer.Write(true);
			byte[] array3 = File.ReadAllBytes(path);
			writer.Write(array3.Length);
			writer.Write(array3);
			flag = true;
			IL_547:
			if (!flag)
			{
				writer.Write(false);
			}
			return true;
		}

		public static string findIncludes(string source, string dir)
		{
			for (int i = source.IndexOf("#INCLUDE "); i > -1; i = source.IndexOf("#INCLUDE "))
			{
				int num = source.IndexOf('"', i + 1);
				int num2 = source.IndexOf('"', num + 1);
				string text = source.Substring(num + 1, num2 - num - 1);
				source = source.Replace("#INCLUDE \"" + text + "\"", "\n" + File.ReadAllText(Path.Combine(dir, text)) + "\n");
			}
			return source;
		}

		public static string compileNormal(string source)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(providerOptions);
			CompilerParameters compilerParameters = new CompilerParameters
			{
				GenerateInMemory = false,
				GenerateExecutable = false,
				CompilerOptions = "/optimize"
			};
			compilerParameters.ReferencedAssemblies.Add("tConfig.exe");
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Xact.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Game.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Graphics.dll");
			ModPackBuilder.LoadRequiredAssemblies(compilerParameters.ReferencedAssemblies);
			compilerParameters.OutputAssembly = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				ModPackBuilder.modpack,
				string.IsNullOrEmpty(ModPackBuilder.Override) ? "" : ("- " + ModPackBuilder.Override),
				".dll"
			});
			Console.WriteLine("Compiling to " + compilerParameters.OutputAssembly);
			CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[]
			{
				source
			});
			if (compilerResults.Errors.Count > 0)
			{
				ModPackBuilder.HandleErrors(compilerResults, source);
			}
			return compilerResults.PathToAssembly;
		}

		public static void LoadRequiredAssemblies(StringCollection referencedAssemblies)
		{
			if (ModPackBuilder.requiredMods != null && ModPackBuilder.requiredMods.Length > 0)
			{
				foreach (string text in ModPackBuilder.requiredMods)
				{
					string text2 = text;
					if (text2.IndexOf("- ") > -1)
					{
						string[] array2 = text2.Split(new char[]
						{
							'-'
						});
						array2[1].Trim();
						text2 = array2[0].Trim();
					}
					string text3 = Config.assemblyPath[text2];
					if (File.Exists(text3))
					{
						referencedAssemblies.Add(text3);
						Console.WriteLine("Added reference to " + text3);
					}
					else
					{
						Console.WriteLine("DLL " + text3 + " doesn't exist");
					}
				}
			}
		}

		public static void HandleErrors(CompilerResults results, string source)
		{
			string[] array = Regex.Split(source, "\\r?\\n|\\r");
			string text = "";
			foreach (object obj in results.Errors)
			{
				System.CodeDom.Compiler.CompilerError compilerError = (System.CodeDom.Compiler.CompilerError)obj;
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" ",
					compilerError.ErrorText,
					":",
					Environment.NewLine
				});
				text = text + "   " + array[compilerError.Line - 1] + Environment.NewLine;
				text = text + "   " + "^".PadLeft(compilerError.Column) + Environment.NewLine;
			}
			throw new Exception(text);
		}

		public static string compileNormal(string[] files)
		{
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(providerOptions);
			CompilerParameters compilerParameters = new CompilerParameters
			{
				GenerateInMemory = false,
				GenerateExecutable = false,
				CompilerOptions = "/optimize"
			};
			compilerParameters.ReferencedAssemblies.Add("tConfig.exe");
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Xact.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Game.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Graphics.dll");
			ModPackBuilder.LoadRequiredAssemblies(compilerParameters.ReferencedAssemblies);
			compilerParameters.OutputAssembly = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				ModPackBuilder.modpack,
				string.IsNullOrEmpty(ModPackBuilder.Override) ? "" : ("- " + ModPackBuilder.Override),
				".dll"
			});
			Console.WriteLine("Compiling to " + compilerParameters.OutputAssembly);
			CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromFile(compilerParameters, files);
			if (compilerResults.Errors.Count > 0)
			{
				string text = "";
				text = text + "Errors building source code into " + compilerResults.PathToAssembly;
				foreach (object obj in compilerResults.Errors)
				{
					System.CodeDom.Compiler.CompilerError compilerError = (System.CodeDom.Compiler.CompilerError)obj;
					text = text + "  " + compilerError.ToString();
				}
				throw new Exception(text);
			}
			return compilerResults.PathToAssembly;
		}

		public static string HandleCode(string className, string sourceFile, string type)
		{
			if (ModPackBuilder.customCompile)
			{
				return " \n" + File.ReadAllText(sourceFile) + "\n";
			}
			string text = Codable.ParseName(className) + type;
			string text2 = "public Item item; public " + text + "(Item item) \n{ this.item=item; }\n";
			string text3 = "public Projectile projectile; public " + text + "(Projectile projectile) \n{ this.projectile=projectile; }\n";
			string text4 = string.Concat(new string[]
			{
				"public NPC npc; public ",
				text,
				"() {} public ",
				text,
				"(NPC npc) \n{ this.npc = npc; }\n"
			});
			string text5 = "";
			string text6 = "";
			if (type == "Item")
			{
				text5 = text2;
			}
			if (type == "Projectile")
			{
				text5 = text3;
			}
			if (type == "NPC")
			{
				text5 = text4;
			}
			if (type == "Global")
			{
				text5 = "";
			}
			if (type == "HoldStyle")
			{
				text6 = " : IHoldStyle";
			}
			if (type == "UseStyle")
			{
				text6 = " : IUseStyle";
			}
			if (type == "Prefix")
			{
				text6 = " : IPrefixDefiner";
			}
			if (className == "ModWorld" || className == "ModPlayer" || className == "ModGeneric")
			{
				text = className;
			}
			string source = string.Concat(new string[]
			{
				"\npublic class ",
				text,
				text6,
				" { \n",
				text5,
				" \n",
				File.ReadAllText(sourceFile),
				" \n}\n"
			});
			return ModPackBuilder.findIncludes(source, Path.GetDirectoryName(sourceFile));
		}

		public static string HandleCodeBoo(string className, string sourceFile, string type)
		{
			string text = Codable.ParseName(className) + type;
			string text2 = "\titem as Item\n\tdef constructor(item as Item):\n\t\tself.item=item\n";
			string text3 = "\tprojectile as Projectile\n\tdef constructor(projectile as Projectile):\n\t\tself.projectile=projectile\n";
			string text4 = "\tnpc as NPC\n\tdef constructor()\n\tdef constructor(npc as NPC)\n\t\tself.npc = npc\n";
			string text5 = "";
			string text6 = "";
			if (type == "Item")
			{
				text5 = text2;
			}
			if (type == "Projectile")
			{
				text5 = text3;
			}
			if (type == "NPC")
			{
				text5 = text4;
			}
			if (type == "Global")
			{
				text5 = "";
			}
			if (type == "HoldStyle")
			{
				text6 = " : IHoldStyle";
			}
			if (type == "UseStyle")
			{
				text6 = " : IUseStyle";
			}
			if (className == "ModWorld" || className == "ModPlayer")
			{
				text = className;
			}
			string str = string.Concat(new string[]
			{
				"\nclass ",
				text,
				text6,
				":\n",
				text5,
				"\n"
			});
			string[] array = File.ReadAllText(sourceFile).Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "\t" + array[i];
			}
			return str + string.Join("\n", array) + "\n";
		}

		public static string CodeUsing()
		{
			return "using System.IO; using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using System; using System.Collections; using System.Collections.Generic;";
		}

		public static string CodeUsingBoo()
		{
			return "import System.IO\nimport Microsoft.Xna.Framework\nimport Microsoft.Xna.Framework.Graphics\nimport System\nimport System.Collections\nimport System.Collections.Generic\n\n";
		}

		public static string compilePerm(string className, string sourceFile, string type, bool save = true)
		{
			if (!File.Exists(sourceFile))
			{
				return "";
			}
			string text = "";
			if (!ModPackBuilder.customCompile)
			{
				text = ModPackBuilder.CodeUsing() + " namespace Terraria \n{";
			}
			string text2 = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				"Global",
				Path.DirectorySeparatorChar,
				"World.cs"
			});
			string text3 = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				"Global",
				Path.DirectorySeparatorChar,
				"Player.cs"
			});
			string text4 = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				"Global",
				Path.DirectorySeparatorChar,
				"Generic.cs"
			});
			if (File.Exists(text2) && className != "ModWorld")
			{
				text += ModPackBuilder.HandleCode("ModWorld", text2, "");
			}
			if (File.Exists(text3) && className != "ModPlayer")
			{
				text += ModPackBuilder.HandleCode("ModPlayer", text3, "");
			}
			if (File.Exists(text4) && className != "ModGeneric")
			{
				text += ModPackBuilder.HandleCode("ModGeneric", text4, "");
			}
			text = text + ModPackBuilder.HandleCode(className, sourceFile, type) + "}";
			Dictionary<string, string> providerOptions = new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"v4.0"
				}
			};
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(providerOptions);
			CompilerParameters compilerParameters = new CompilerParameters
			{
				GenerateInMemory = !save,
				GenerateExecutable = false,
				CompilerOptions = "/optimize"
			};
			compilerParameters.ReferencedAssemblies.Add("tConfig.exe");
			compilerParameters.ReferencedAssemblies.Add("System.dll");
			compilerParameters.ReferencedAssemblies.Add("System.Core.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Xact.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Game.dll");
			compilerParameters.ReferencedAssemblies.Add("Microsoft.Xna.Framework.Graphics.dll");
			if (save)
			{
				compilerParameters.OutputAssembly = "Temp.dll";
			}
			CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[]
			{
				text
			});
			if (compilerResults.Errors.Count > 0)
			{
				ModPackBuilder.HandleErrors(compilerResults, text);
			}
			return compilerResults.PathToAssembly;
		}

		public static string compilePermBoo(string className, string sourceFile, string type, bool save = true)
		{
			if (!File.Exists(sourceFile))
			{
				return null;
			}
			string text = "namespace Terraria\n" + ModPackBuilder.CodeUsingBoo();
			string text2 = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				"Global",
				Path.DirectorySeparatorChar,
				"World.boo"
			});
			string text3 = string.Concat(new object[]
			{
				ModPackBuilder.modpackFolder,
				Path.DirectorySeparatorChar,
				"Global",
				Path.DirectorySeparatorChar,
				"Player.boo"
			});
			if (File.Exists(text2) && className != "ModWorld")
			{
				text += ModPackBuilder.HandleCodeBoo("ModWorld", text2, "");
			}
			if (File.Exists(text3) && className != "ModPlayer")
			{
				text += ModPackBuilder.HandleCodeBoo("ModPlayer", text3, "");
			}
			text = text + ModPackBuilder.HandleCodeBoo(className, sourceFile, type) + "\n";
			return ModPackBuilder.CompileNormalBoo(text);
		}

		public static string CompileNormalBoo(string source)
		{
			BooCompiler booCompiler = new BooCompiler();
			booCompiler.Parameters.Input.Add(new StringInput("Boo", source));
			booCompiler.Parameters.Ducky = true;
			booCompiler.Parameters.Pipeline = new CompileToFile();
			booCompiler.Parameters.References.Add(Assembly.LoadFile(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "tConfig.exe"));
			booCompiler.Parameters.References.Add(Assembly.GetAssembly(typeof(Microsoft.Xna.Framework.Rectangle)));
			booCompiler.Parameters.References.Add(Assembly.GetAssembly(typeof(SpriteBatch)));
			booCompiler.Parameters.References.Add(Assembly.GetAssembly(typeof(Game)));
			CompilerContext compilerContext = booCompiler.Run();
			if (string.IsNullOrEmpty(compilerContext.GeneratedAssemblyFileName))
			{
				string text = "Error building boo source\n";
				IList list = compilerContext.Errors;
				for (int i = 0; i < list.Count; i++)
				{
					System.CodeDom.Compiler.CompilerError compilerError = (System.CodeDom.Compiler.CompilerError)list[i];
					text = text + compilerError.ToString() + "\n";
				}
				throw new Exception(text);
			}
			return compilerContext.GeneratedAssemblyFileName;
		}

		public static IniFile LoadINI(string fullpath, string itemname)
		{
			if (!File.Exists(fullpath))
			{
				return IniFile.FromFile(fullpath);
			}
			string s = File.ReadAllText(fullpath).Replace("[" + itemname + "]", "[Stats]");
			IniFileSettings.CaseSensitive = false;
			IniFile iniFile = IniFile.FromStream(new IniFileReader(new MemoryStream(Encoding.ASCII.GetBytes(s))));
			if (iniFile["Items"].GetKeys().Count > 0)
			{
				string text = "";
				foreach (string text2 in iniFile["Items"].GetKeys())
				{
					string text3 = text;
					text = string.Concat(new string[]
					{
						text3,
						iniFile["Items"][text2],
						" ",
						text2,
						","
					});
				}
				if (text != "")
				{
					iniFile["Recipe"]["Items"] = text.Substring(0, text.Length - 1);
				}
			}
			if (iniFile["Tiles"].GetKeys().Count > 0)
			{
				string text4 = "";
				foreach (string text5 in iniFile["Tiles"].GetKeys())
				{
					if (iniFile["Tiles"][text5] == "True")
					{
						text4 = text4 + text5 + ",";
					}
				}
				if (text4 != "")
				{
					iniFile["Recipe"]["Tiles"] = text4.Substring(0, text4.Length - 1);
				}
			}
			if (!string.IsNullOrEmpty(iniFile["needWater"]["value"]))
			{
				iniFile["Recipe"]["needWater"] = iniFile["needWater"]["value"];
			}
			if (!string.IsNullOrEmpty(iniFile["Amount"]["value"]))
			{
				iniFile["Recipe"]["Amount"] = iniFile["Amount"]["value"];
			}
			iniFile.DeleteSection("Items");
			iniFile.DeleteSection("Tiles");
			iniFile.DeleteSection("needWater");
			iniFile.DeleteSection("Amount");
			if (iniFile.GetSectionNames().Contains("Stats") && !string.IsNullOrEmpty(iniFile["Stats"]["parent"]))
			{
				string text6 = iniFile["Stats"]["parent"];
				string text7 = fullpath.Replace(itemname, text6);
				if (!File.Exists(text7))
				{
					throw new Exception(string.Concat(new string[]
					{
						"Error with ",
						fullpath,
						"\n'parent' file ",
						text7,
						" does not exist!"
					}));
				}
				IniFile iniFile2 = ModPackBuilder.LoadINI(text7, text6);
				IniFile iniFile3 = new IniFile();
				string sectionName = "Stats";
				foreach (string key in iniFile2[sectionName].GetKeys())
				{
					iniFile3[sectionName][key] = iniFile2[sectionName][key];
				}
				foreach (string sectionName2 in iniFile.GetSectionNames())
				{
					foreach (string key2 in iniFile[sectionName2].GetKeys())
					{
						iniFile3[sectionName2][key2] = iniFile[sectionName2][key2];
					}
				}
				iniFile = iniFile3;
			}
			if (!string.IsNullOrEmpty(ModPackBuilder.Override))
			{
				string text8 = fullpath.Replace(ModPackBuilder.modpack, ModPackBuilder.OverridePath);
				if (File.Exists(text8))
				{
					IniFile iniFile4 = ModPackBuilder.LoadINI(text8, itemname);
					IniFile iniFile5 = new IniFile();
					foreach (string sectionName3 in iniFile.GetSectionNames())
					{
						foreach (string key3 in iniFile[sectionName3].GetKeys())
						{
							iniFile5[sectionName3][key3] = iniFile[sectionName3][key3];
						}
					}
					foreach (string sectionName4 in iniFile4.GetSectionNames())
					{
						foreach (string key4 in iniFile4[sectionName4].GetKeys())
						{
							iniFile5[sectionName4][key4] = iniFile4[sectionName4][key4];
						}
					}
					iniFile = iniFile5;
				}
			}
			return iniFile;
		}

		private const SearchOption search = SearchOption.AllDirectories;

		private static string modpack;

		private static string modpackFolder;

		private static string outputFolder;

		private static string Override;

		private static string OverridePath;

		private static Dictionary<string, ArrayList> fileList;

		private static ArrayList itemFiles;

		private static ArrayList projFiles;

		private static ArrayList npcFiles;

		private static ArrayList buffFiles;

		private static ArrayList tileFiles;

		private static ArrayList wallFiles;

		private static ArrayList prefixFiles;

		private static bool csCheck = true;

		private static bool customCompile = false;

		private static string[] requiredMods;
	}
}
