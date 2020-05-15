using System;
using System.IO;

namespace Terraria
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string text = "tConfig Mod-Pack Builder " + Constants.version;
			if (Constants.betaRelease)
			{
				text += " Beta";
			}
			Console.WriteLine(text);
			try
			{
				Main main = new Main();
				Terraria.Main.dedServ = true;
				main.PublicInit(false, 1, true);
				Config.mainInstance = main;
				Config.tempRun = true;
				if (args.Length > 0)
				{
					if (args.Length < 2)
					{
						Console.WriteLine("Incorrect number of arguments. Usage: modpackDirectory modName (outputDir) (autoclose - true/false)");
						Console.WriteLine("Press Enter to exit");
						Console.ReadLine();
					}
					else
					{
						string text2 = args[0];
						Console.WriteLine("Using path: " + text2);
						string text3 = args[1];
						Console.WriteLine("Modname: " + text3);
						string text4 = Path.Combine(Constants.tConfigFolder, "ModPacks");
						if (args.Length > 2)
						{
							text4 = args[2];
						}
						string a = "false";
						if (args.Length > 3)
						{
							a = args[3];
						}
						if (!Directory.Exists(text2))
						{
							Console.WriteLine("'" + text2 + "' modpack folder doesn't exist! Press Enter to exit.");
							Console.ReadLine();
						}
						else
						{
							ModPackBuilder modPackBuilder = new ModPackBuilder(text3, "", text2, text4);
							if (modPackBuilder.IniToObj())
							{
								Console.WriteLine("Finished successfully!");
								Installer_Builder.Build(text3, new string[]
								{
									Path.Combine(text4, text3 + ".obj")
								}, text4);
								if (a == "true")
								{
									return;
								}
							}
							else
							{
								Console.WriteLine("Build failed!");
							}
							Console.WriteLine("Press Enter to exit");
							Console.ReadLine();
						}
					}
				}
				else
				{
					string[] directories = Directory.GetDirectories(Constants.tConfigFolder + "\\ModPacks");
					int num = 0;
					string over = "";
					do
					{
						directories = Directory.GetDirectories(Constants.tConfigFolder + "\\ModPacks");
						Console.WriteLine("Choose a Mod Pack to build:");
						for (int i = 0; i < directories.Length; i++)
						{
							string[] array = directories[i].Split(new char[]
							{
								'\\'
							});
							Console.WriteLine(i + 1 + ": " + array[array.Length - 1]);
						}
						num = -1;
						while (num == -1 || num >= directories.Length)
						{
							try
							{
								string text5 = Console.ReadLine();
								if (text5.ToLower().StartsWith("do "))
								{
									text5 = text5.Substring(3);
									string[] array2 = text5.Split(new char[]
									{
										','
									});
									for (int j = 0; j < array2.Length; j++)
									{
										Program.GetModParams(array2[j], ref num, ref over);
										if (!Program.TryBuilding(directories, num, over))
										{
											break;
										}
									}
									num = -2;
									break;
								}
								Program.GetModParams(text5, ref num, ref over);
							}
							catch (Exception)
							{
								num = -1;
							}
						}
						if (num != -2)
						{
							Program.TryBuilding(directories, num, over);
						}
						else
						{
							num = 0;
						}
						Console.WriteLine("Press Enter to continue");
						Console.ReadLine();
					}
					while (num >= 0);
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error:\n " + arg);
				Console.WriteLine("Press Enter to exit");
				Console.ReadLine();
			}
		}

		public static void GetModParams(string inp, ref int choice, ref string Over)
		{
			choice = -1;
			Over = "";
			if (inp.IndexOf(':') > -1)
			{
				string[] array = inp.Split(new char[]
				{
					':'
				});
				choice = Convert.ToInt32(array[0]) - 1;
				Over = array[1];
				return;
			}
			choice = Convert.ToInt32(inp) - 1;
		}

		public static bool TryBuilding(string[] dirs, int choice, string Over)
		{
			string[] array = dirs[choice].Split(new char[]
			{
				'\\'
			});
			string text = array[array.Length - 1];
			string text2 = dirs[choice];
			ModPackBuilder modPackBuilder = new ModPackBuilder(text, Over, "", "");
			if (modPackBuilder.IniToObj())
			{
				Console.WriteLine("Finished successfully!");
				Console.WriteLine("Building Installer...");
				string text3 = Constants.tConfigFolder + "\\ModPacks\\" + (text + " " + Over).Trim() + ".obj";
				Installer_Builder.Build((text + " " + Over).Trim(), new string[]
				{
					text3
				}, Constants.tConfigFolder + "\\ModPacks");
				return true;
			}
			Console.WriteLine("Build failed!");
			return false;
		}
	}
}
