using Gajatko.IniFiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SevenZip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Terraria
{
	public class Config
	{
		public class ItemDefaults
		{
			public int width;

			public int height;

			public string name = "Unnamed";

			public int crit;

			public bool mech;

			public int reuseDelay;

			public bool melee;

			public bool magic;

			public bool ranged;

			public int placeStyle;

			public int buffTime;

			public int buffType;

			public bool noWet;

			public bool vanity;

			public int mana;

			public bool channel;

			public int manaIncrease;

			public bool noMelee;

			public bool noUseGraphic;

			public int lifeRegen;

			public float shootSpeed;

			public int alpha;

			public int ammo;

			public int useAmmo;

			public bool autoReuse;

			public bool accessory;

			public int axe;

			public int healMana;

			public int bodySlot = -1;

			public int legSlot = -1;

			public int headSlot = -1;

			public bool potion;

			public Microsoft.Xna.Framework.Color color = default(Microsoft.Xna.Framework.Color);

			public bool consumable;

			public int createTile = -1;

			public int createWall = -1;

			public int damage = -1;

			public int defense;

			public int hammer;

			public int healLife;

			public int holdStyle;

			public float knockBack;

			public int maxStack = 1;

			public int pick;

			public int rare;

			public float scale = 1f;

			public int shoot;

			public int stack = 1;

			public string toolTip = "";

			public string toolTip2 = "";

			public string toolTip3 = "";

			public string toolTip4 = "";

			public string toolTip5 = "";

			public string toolTip6 = "";

			public string toolTip7 = "";

			public int tileBoost;

			public int type = -1;

			public int useStyle;

			public int useSound;

			public int useTime = 100;

			public int useAnimation = 100;

			public int value;

			public bool useTurn;
		}

		public class ProjDefaults
		{
			public int width;

			public int height;

			public int soundDelay;

			public bool melee;

			public bool ranged;

			public bool magic;

			public bool ownerHitCheck;

			public bool hide;

			public bool ignoreWater;

			public bool hostile;

			public float light;

			public int penetrate = 1;

			public bool tileCollide = true;

			public int aiStyle;

			public int alpha;

			public int type = -1;

			public float scale = 1f;

			public int timeLeft = 3600;

			public string name = "Unnamed";

			public bool friendly;

			public int damage;

			public float knockBack;
		}

		public class BuffDefaults
		{
			public int id = -1;

			public string tip = "";

			public bool debuff;

			public float alpha;

			public string name = "Unnamed";
		}

		public class NPCDefaults
		{
			public bool netAlways;

			public bool[] buffImmune;

			public int lifeRegen;

			public bool dontTakeDamage;

			public float npcSlots = 1f;

			public bool lavaImmune;

			public bool townNPC;

			public bool friendly;

			public bool behindTiles;

			public bool boss;

			public bool noTileCollide;

			public int alpha;

			public Microsoft.Xna.Framework.Color color = default(Microsoft.Xna.Framework.Color);

			public float knockBackResist = 1f;

			public string name = "Unnamed";

			public string displayName = "";

			public bool noGravity;

			public float scale = 1f;

			public int soundHit;

			public int soundKilled;

			public int timeLeft = NPC.activeTime;

			public int type = -1;

			public float value;

			public int height;

			public int width;

			public int aiStyle;

			public int damage;

			public int defense;

			public int lifeMax;

			public NPCDefaults()
			{
				buffImmune = new bool[40];
				buffImmune[31] = true;
			}
		}

		public class WallDefaults
		{
			public string name;

			public int id = -1;

			public string DropName;

			public bool House;

			public int Blend;
		}

		public class TileDefaults
		{
			public string name;

			public int id;

			public string DropName;

			public int Width = 1;

			public int Height = 1;

			public int PretendType;

			public int furniture;

			public float pick = 1f;

			public float axe;

			public float hammer;

			public int minPick;

			public int minAxe;

			public int minHammer;

			public bool Lighted;

			public bool MergeDirt;

			public bool Cut;

			public bool Alch;

			public int Shine;

			public bool Shine2;

			public bool Stone;

			public bool WaterDeath;

			public bool LavaDeath;

			public bool Table;

			public bool BlockLight;

			public bool NoSunLight;

			public bool Dungeon;

			public bool SolidTop;

			public bool Solid;

			public bool NoAttach;

			public bool NoFail;

			public bool FrameImportant;
		}

		public const int TILEMAX = 65534;

		public static string tConfigFolder = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Storage";

		public static string tempModAssembly = tConfigFolder + Path.DirectorySeparatorChar + "ModPacks_temp_runtime";

		public static string tempModAssemblyOrigin = tConfigFolder + Path.DirectorySeparatorChar + "ModPacks_temp_runtime";

		public static bool tempRun = false;

		public static Dictionary<string, Assembly> assemblies;

		public static Dictionary<string, string> namespaces;

		public static Dictionary<string, string> assemblyName;

		public static Dictionary<string, string> assemblyPath;

		public static int saveRelease = 5;

		public static Main mainInstance;

		public static IniFile settings;

		public static bool loadedServerSettings;

		public static List<string> mods = new List<string>();

		public static DictionaryHandler<string, bool> modLoaded;

		public static Dictionary<string, int> modVersion;

		public static DictionaryHandler<string, int> loadedVersion;

		public static Dictionary<string, string> modDLVersion;

		public static Dictionary<string, string> modURL;

		public static Dictionary<string, string> modObjURL;

		public static Dictionary<string, string> modNewVersion;

		public static Dictionary<string, bool> downloadModUpdate;

		public static bool updatesAvailable = false;

		public static Dictionary<string, FileStream> modLoad;

		public static string tConfigHash;

		public static bool modSettingChanged = false;

		public static int tConfigMenuStart = 0;

		public static DictionaryHandler<string, int> tConfigModChoices = new DictionaryHandler<string, int>();

		public static DictionaryHandler<string, List<string>> tConfigModTypes = new DictionaryHandler<string, List<string>>();

		public static Dictionary<string, IniFile> ModSetting = new Dictionary<string, IniFile>();

		public static int tConfigWorldMenuStart = 0;

		public static int tConfigUpdateMenuStart = 0;

		public static int tConfigPlayerMenuStart = 0;

		public static InterfaceObj npcInterface;

		public static InterfaceObj tileInterface;

		public static List<InterfaceObj> playerInterface;

		public static bool initialized = false;

		public static ArrayList messages = new ArrayList();

		public static Random rand = new Random();

		public static int randSeed = 0;

		public static int[] modPartsTransferred = new int[256];

		public static int modPartsTotal = 0;

		public static int modsTotal = 0;

		public static bool modTransferStarted = false;

		public static MemoryStream modStream = null;

		public static int[] modStreamPos = new int[256];

		public static BinaryReader modReader = null;

		public static BinaryWriter modWriter;

		public static string modPartsHash;

		public static bool[] modConfirmed = new bool[256];

		public static bool generateINI = false;

		public static bool generateItemObj = false;

		private static readonly string[] skipAttrStuff = new string[19]
		{
			"className",
			"active",
			"stack",
			"material",
			"owner",
			"name",
			"directionY",
			"direction",
			"oldDirection",
			"oldDirectionY",
			"target",
			"oldTarget",
			"netID",
			"netUpdate",
			"life",
			"spriteDirection",
			"defDamage",
			"defDefense",
			"whoAmI"
		};

		private static readonly ArrayList skipAttr = new ArrayList(skipAttrStuff);

		public static Dictionary<string, Assembly> modDLL;

		public static Dictionary<string, Dictionary<string, object>> globalMod;

		public static Dictionary<string, byte[]> unloadedModData;

		public static ItemDefHandler<Item> itemDefs;

		public static ProjDefHandler<Projectile> projDefs;

		public static BuffDefHandler buffDefs;

		public static npcDefHandler npcDefs;

		public static Dictionary<string, int> goreID;

		public static TileDefHandler tileDefs;

		public static WallDefHandler wallDefs;

		public static HoldStyleHandler holdStyleDefs;

		public static UseStyleHandler useStyleDefs;

		public static bool tilesPretend = false;

		public static ProjDefIDAccessor projectileID = new ProjDefIDAccessor();

		public static BuffDefIDAccessor buffID = new BuffDefIDAccessor();

		private static int itemDefsCount = 1;

		private static int projDefsCount = 1;

		private static int npcDefsCount = 1;

		public static int customItemsAmt = 0;

		public static int modifiedItemsAmt = 0;

		private static int customProjAmt = 0;

		public static int customNPCAmt = 0;

		private static int customGoreAmt = 0;

		public static int customTileAmt = 0;

		public static int customWallAmt = 0;

		public static int interfaceAmt = 0;

		public static int holdStyleAmt = 0;

		public static int useStyleAmt = 0;

		public static int armorBodyAmt = 0;

		public static int armorFBodyAmt = 0;

		public static int armorLegAmt = 0;

		public static int armorHeadAmt = 0;

		private static bool wroteItems = false;

		private static bool wroteRecipes = false;

		private static bool wroteProjs = false;

		private static bool wroteNPCs = false;

		public static Dictionary<string, Recipe> recipeByName;

		public static DictionaryHandler<int, bool> hasHands;

		public static DictionaryHandler<string, string> armorSets;

		public static DictionaryHandler<int, int> drawHair;

		public static DictionaryHandler<int, bool> drawHairAlt;

		public static Dictionary<string, int> ammoDef;

		public static bool codeExists;

		public static Random syncedRand;

		public static bool modsLoading = true;

		public static bool displayConsole = true;

		public static int displayConsoleLines = 8;

		public static List<string> console = new List<string>();

		public static List<Microsoft.Xna.Framework.Color> consoleColor = new List<Microsoft.Xna.Framework.Color>();

		public static List<TrackConsoleLine> consoleTrack = new List<TrackConsoleLine>();

		public static DictionaryHandler<string, JsonData> jsonDefault = new DictionaryHandler<string, JsonData>();

		public static DictionaryHandler<string, JsonData> jsonCurrent = new DictionaryHandler<string, JsonData>();

		public Dictionary<string, object> globalWorldMod
		{
			get
			{
				return globalMod["ModWorld"];
			}
			set
			{
				globalMod["ModWorld"] = value;
			}
		}

		public Dictionary<string, object> globalPlayerMod
		{
			get
			{
				return globalMod["ModPlayer"];
			}
			set
			{
				globalMod["ModPlayer"] = value;
			}
		}

		public static string GetFile(string modpack, string itemname, string itemtype)
		{
			return tConfigFolder + "\\ModPacks\\" + modpack + "\\" + itemtype + "\\" + itemname + ".ini";
		}

		public static void Initialize()
		{
			if (generateINI || generateItemObj)
			{
				Main.recipe = new Recipe[Recipe.maxRecipes];
				Main.availableRecipe = new int[Recipe.maxRecipes];
				Main.availableRecipeY = new float[Recipe.maxRecipes];
				Recipe.numRecipes = 0;
				for (int i = 0; i < Recipe.maxRecipes; i++)
				{
					Main.recipe[i] = new Recipe();
					Main.availableRecipeY[i] = 65 * i;
				}
				recipeByName = new Dictionary<string, Recipe>();
				for (int j = 1; j < 112; j++)
				{
					Projectile projectile = new Projectile();
					projectile.SetDefaults(j);
				}
				for (int k = 1; k < 147; k++)
				{
					NPC nPC = new NPC();
					nPC.SetDefaults(k, -1f, saveData: true);
				}
				string[] array = new string[17]
				{
					"Slimeling",
					"Slimer2",
					"Green Slime",
					"Pinky",
					"Baby Slime",
					"Black Slime",
					"Purple Slime",
					"Red Slime",
					"Yellow Slime",
					"Jungle Slime",
					"Little Eater",
					"Big Eater",
					"Short Bones",
					"Big Boned",
					"Heavy Skeleton",
					"Little Stinger",
					"Big Stinger"
				};
				foreach (string defaults in array)
				{
					NPC nPC2 = new NPC();
					nPC2.SetDefaults(defaults);
				}
			}
			if (generateINI)
			{
				Recipe.SetupRecipes();
				SaveRecipesINI();
				SaveBuffsINI();
				SaveTilesINI();
				SaveWallsINI();
			}
			if (generateItemObj)
			{
				Recipe.SetupRecipes();
				SaveRecipesObj();
			}
		}

		public static void LoadData(bool loadMods = true)
		{
			string path = tempModAssemblyOrigin;
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string[] directories = Directory.GetDirectories(path);
			string[] array = directories;
			foreach (string path2 in array)
			{
				try
				{
					Directory.Delete(path2, recursive: true);
				}
				catch
				{
				}
			}
			int num = 0;
			do
			{
				path = tempModAssemblyOrigin + Path.DirectorySeparatorChar + (Main.dedServ ? 1 : 0) + num++;
				if (Directory.Exists(path))
				{
					try
					{
						Directory.Delete(path, recursive: true);
					}
					catch
					{
					}
				}
			}
			while (Directory.Exists(path));
			tempModAssembly = path;
			namespaces = new Dictionary<string, string>();
			modLoaded = new DictionaryHandler<string, bool>();
			assemblyName = new Dictionary<string, string>();
			modsLoading = true;
			if (!Main.dedServ)
			{
				syncedRand = Main.rand;
			}
			else
			{
				randSeed = Main.rand.Next(int.MaxValue);
				syncedRand = new Random(randSeed);
			}
			assemblyPath = new Dictionary<string, string>();
			assemblies = new Dictionary<string, Assembly>();
			SevenZipBase.SetLibraryPath(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "7z.dll");
			WorldGen.ResetEvents();
			Prefix.DefineDefaults();
			Codable.customVars = new Dictionary<string, object>();
			Codable.noMethod = new HashSet<string>();
			Codable.customMethodReturnDict = new Dictionary<int, object>();
			Codable.customMethodRefReturnDict = new Dictionary<int, object[]>();
			globalMod = new Dictionary<string, Dictionary<string, object>>();
			globalMod["ModPlayer"] = new Dictionary<string, object>();
			globalMod["ModWorld"] = new Dictionary<string, object>();
			globalMod["ModGeneric"] = new Dictionary<string, object>();
			Events.Initialize();
			npcInterface = null;
			tileInterface = null;
			playerInterface = new List<InterfaceObj>();
			modDLVersion = new Dictionary<string, string>();
			modURL = new Dictionary<string, string>();
			modObjURL = new Dictionary<string, string>();
			modNewVersion = new Dictionary<string, string>();
			downloadModUpdate = new Dictionary<string, bool>();
			unloadedModData = new Dictionary<string, byte[]>();
			modDLL = new Dictionary<string, Assembly>();
			modLoad = new Dictionary<string, FileStream>();
			modVersion = new Dictionary<string, int>();
			loadedVersion = new DictionaryHandler<string, int>();
			Main.itemName = new ItemNameAccess();
			goreID = new Dictionary<string, int>();
			drawHair = new DictionaryHandler<int, int>();
			drawHairAlt = new DictionaryHandler<int, bool>();
			ammoDef = new Dictionary<string, int>();
			SoundHandler.Init();
			npcDefsCount = 1;
			projDefsCount = 1;
			itemDefsCount = 1;
			initialized = true;
			LoadSettings(loadMods);
			int custom = 0;
			int custom2 = 0;
			int num2 = 0;
			int num3 = 0;
			int custom3 = 0;
			int custom4 = 0;
			itemDefs = new ItemDefHandler<Item>(604, custom);
			npcDefs = new npcDefHandler(147, custom2);
			wallDefs = new WallDefHandler(32 + num2);
			tileDefs = new TileDefHandler(150 + num3);
			projDefs = new ProjDefHandler<Projectile>(112, custom3);
			buffDefs = new BuffDefHandler(custom4);
			holdStyleDefs = new HoldStyleHandler();
			useStyleDefs = new UseStyleHandler();
			customItemsAmt = 0;
			modifiedItemsAmt = 0;
			customProjAmt = 0;
			customNPCAmt = 0;
			customGoreAmt = 0;
			customTileAmt = 0;
			customWallAmt = 0;
			interfaceAmt = 0;
			useStyleAmt = UseStyleHandler.numStart + 1;
			holdStyleAmt = HoldStyleHandler.numStart + 1;
			TileDefHandler.AddDefaultTiles();
			recipeByName = new Dictionary<string, Recipe>();
			hasHands = new DictionaryHandler<int, bool>();
			armorSets = new DictionaryHandler<string, string>();
			for (int j = 0; j < 150; j++)
			{
				if (!string.IsNullOrEmpty(Main.tileName[j]))
				{
					tileDefs.ID[Main.tileName[j]] = j;
				}
			}
			if (!generateItemObj)
			{
				LoadItemObj();
				LoadRecipesObj();
				LoadProjObj();
				LoadNPCObj();
				if (loadMods || Main.dedServ)
				{
					if (!Main.dedServ)
					{
						Music.Init();
					}
					jsonDefault = new DictionaryHandler<string, JsonData>();
					jsonCurrent = new DictionaryHandler<string, JsonData>();
					for (int k = 0; k < mods.Count; k++)
					{
						if (!LoadCustomObj(k))
						{
							return;
						}
					}
					Main.statusText = "Loading Built-In Mod";
					Item item = new Item();
					CopyAttributes(item, itemDefs.byName["Angel Statue"]);
					item.createTile = -1;
					item.toolTip = "Mysterious forces prevent you from seeing the true nature of this item.";
					item.name = "Unloaded Item";
					item.type = 604 + customItemsAmt;
					customItemsAmt++;
					item.netID = item.type;
					itemDefs[item.type] = item;
					itemDefs.byName[item.name] = item;
					if (mods.IndexOf("Built-In") == -1)
					{
						byte[] built_In = ModPacks.Built_In;
						LoadCustomObjBuiltin("Built-In", built_In);
					}
				}
				Main.npcTexture.SetArray();
				Main.itemTexture.SetArray();
				Main.projectileTexture.SetArray();
				Main.tileTexture.SetArray();
				Main.statusText = "Initializing ModGenerics";
				foreach (string key in modDLL.Keys)
				{
					object obj3 = modDLL[key].CreateInstance(namespaces[key] + ".ModGeneric");
					if (obj3 != null)
					{
						globalMod["ModGeneric"][key] = obj3;
					}
				}
				Assembly[] array2 = AppDomain.CurrentDomain.GetAssemblies();
				for (int l = 0; l < array2.Length; l++)
				{
					_ = array2[l];
				}
				Codable.RunGlobalMethod("ModGeneric", "LoadCustomRecipes");
				Main.statusText = "Loading Recipes";
				Recipe.maxRecipes = recipeByName.Values.Count + 2;
				Main.recipe = new Recipe[Recipe.maxRecipes];
				Main.availableRecipe = new int[Recipe.maxRecipes];
				Main.availableRecipeY = new float[Recipe.maxRecipes];
				Recipe.numRecipes = 0;
				for (int m = 0; m < Recipe.maxRecipes; m++)
				{
					Main.recipe[m] = new Recipe();
					Main.availableRecipeY[m] = 65 * m;
				}
				foreach (Recipe value in recipeByName.Values)
				{
					if (value.createItem.type != 0)
					{
						Recipe.newRecipe = value;
						Recipe.addRecipe();
					}
				}
				Recipe.newRecipe = new Recipe();
				Main.statusText = "";
			}
			else
			{
				Main.recipe = new Recipe[Recipe.maxRecipes];
				Main.availableRecipe = new int[Recipe.maxRecipes];
				Main.availableRecipeY = new float[Recipe.maxRecipes];
				Recipe.numRecipes = 0;
				for (int n = 0; n < Recipe.maxRecipes; n++)
				{
					Main.recipe[n] = new Recipe();
					Main.availableRecipeY[n] = 65 * n;
				}
			}
			modsLoading = false;
		}

		public static void NetMsg(int modIndex, int msgID)
		{
			NetMessage.SendData(100, -1, -1, "", modIndex, msgID);
		}

		public static void LoadCustomImage(BinaryReader binaryReader, TextureHandler handler, int type)
		{
			int count = binaryReader.ReadInt32();
			MemoryStream stream = new MemoryStream(binaryReader.ReadBytes(count));
			if (!Main.dedServ)
			{
				handler[type] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
			}
		}

		public static Texture2D ConvertToPreMultipliedAlpha(Texture2D texture)
		{
			Microsoft.Xna.Framework.Color[] array = new Microsoft.Xna.Framework.Color[texture.Width * texture.Height];
			texture.GetData(array, 0, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Microsoft.Xna.Framework.Color(new Vector4(array[i].ToVector3() * ((float)(int)array[i].A / 255f), (float)(int)array[i].A / 255f));
			}
			texture.SetData(array, 0, array.Length);
			return texture;
		}

		public static void ProcessINI(object obj, string itemtype, string itemname, string modpack, bool saveAllValues = false)
		{
			if (itemname == "")
			{
				return;
			}
			if (!generateINI)
			{
				switch (itemtype)
				{
				case "Item":
					SaveItemObj((Item)obj, itemname, modpack);
					break;
				case "Projectile":
					SaveProjectileObj((Projectile)obj, itemname, modpack);
					break;
				case "NPC":
					SaveNPCObj((NPC)obj, itemname, modpack);
					break;
				}
				return;
			}
			Type type = obj.GetType();
			FieldInfo[] fields = type.GetFields();
			string file = GetFile(modpack, itemname, itemtype);
			if (!Directory.Exists(file))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(file));
			}
			IniFile iniFile = IniFile.FromFile(file);
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				string[] array2 = fieldInfo.ToString().Split(' ');
				string text = array2[0];
				string text2 = array2[1];
				if (fieldInfo.IsStatic)
				{
					continue;
				}
				if (!(text2 == "buffImmune"))
				{
					switch (text)
					{
					case "Double":
					case "Int32":
					case "System.String":
					case "Single":
					case "Boolean":
					case "Microsoft.Xna.Framework.Color":
						break;
					default:
						continue;
					}
				}
				IniFileSection iniFileSection = iniFile["Stats"];
				if (skipAttr.Contains(text2))
				{
					continue;
				}
				if (itemtype == "Tile")
				{
					switch (text2)
					{
					case "PretendType":
					case "furniture":
					case "DropName":
					case "minPick":
					case "minAxe":
					case "minHammer":
						continue;
					}
				}
				if (itemtype == "Wall" && text2 == "DropName")
				{
					continue;
				}
				if (!saveAllValues)
				{
					switch (text)
					{
					case "Int32":
					case "Double":
					case "Single":
						if (Convert.ToInt32(fieldInfo.GetValue(obj)) <= 0)
						{
							continue;
						}
						break;
					}
					if ((text == "Boolean" && !Convert.ToBoolean(fieldInfo.GetValue(obj))) || (text == "System.String" && Convert.ToString(fieldInfo.GetValue(obj)) == ""))
					{
						continue;
					}
				}
				try
				{
					if (fieldInfo.GetValue(obj) != null)
					{
						if (text == "Microsoft.Xna.Framework.Color")
						{
							fieldInfo.SetValue(obj, fieldInfo.GetValue(obj));
							Microsoft.Xna.Framework.Color color = (Microsoft.Xna.Framework.Color)fieldInfo.GetValue(obj);
							if (color.R != 0 || color.G != 0 || color.B != 0 || color.A != 0 || saveAllValues)
							{
								iniFileSection[text2] = color.R + "," + color.G + "," + color.B + "," + color.A;
							}
						}
						else if (itemtype == "NPC" && text2 == "buffImmune")
						{
							bool[] array3 = ((ArrayHandler<bool>)fieldInfo.GetValue(obj)).array;
							for (int j = 0; j < array3.Length; j++)
							{
								if (array3[j])
								{
									iniFile["Buff Immunities"][Main.buffName[j]] = array3[j].ToString();
								}
							}
						}
						else
						{
							fieldInfo.SetValue(obj, fieldInfo.GetValue(obj));
							if (iniFileSection[text2] != fieldInfo.GetValue(obj).ToString())
							{
								iniFileSection[text2] = fieldInfo.GetValue(obj).ToString();
							}
						}
					}
					else
					{
						iniFileSection[text2] = "";
					}
				}
				catch (Exception)
				{
				}
			}
			iniFile.Save(file);
		}

		public static void CopyAttributes(Item obj, Item orig)
		{
			obj.toolTip3 = orig.toolTip3;
			obj.toolTip4 = orig.toolTip4;
			obj.toolTip5 = orig.toolTip5;
			obj.toolTip6 = orig.toolTip6;
			obj.toolTip7 = orig.toolTip7;
			obj.mech = orig.mech;
			obj.width = orig.width;
			obj.height = orig.height;
			obj.noGrabDelay = orig.noGrabDelay;
			obj.spawnTime = orig.spawnTime;
			obj.wornArmor = orig.wornArmor;
			obj.ownIgnore = orig.ownIgnore;
			obj.ownTime = orig.ownTime;
			obj.keepTime = orig.keepTime;
			obj.type = orig.type;
			obj.name = orig.name;
			obj.holdStyle = orig.holdStyle;
			obj.useStyle = orig.useStyle;
			obj.channel = orig.channel;
			obj.accessory = orig.accessory;
			obj.useAnimation = orig.useAnimation;
			obj.useTime = orig.useTime;
			obj.stack = orig.stack;
			obj.maxStack = orig.maxStack;
			obj.pick = orig.pick;
			obj.axe = orig.axe;
			obj.hammer = orig.hammer;
			obj.tileBoost = orig.tileBoost;
			obj.createTile = orig.createTile;
			obj.createWall = orig.createWall;
			obj.placeStyle = orig.placeStyle;
			obj.damage = orig.damage;
			obj.knockBack = orig.knockBack;
			obj.healLife = orig.healLife;
			obj.healMana = orig.healMana;
			obj.potion = orig.potion;
			obj.consumable = orig.consumable;
			obj.autoReuse = orig.autoReuse;
			obj.useTurn = orig.useTurn;
			obj.color = orig.color;
			obj.alpha = orig.alpha;
			obj.scale = orig.scale;
			obj.useSound = orig.useSound;
			obj.defense = orig.defense;
			obj.headSlot = orig.headSlot;
			obj.bodySlot = orig.bodySlot;
			obj.legSlot = orig.legSlot;
			obj.toolTip = orig.toolTip;
			obj.toolTip2 = orig.toolTip2;
			obj.rare = orig.rare;
			obj.shoot = orig.shoot;
			obj.shootSpeed = orig.shootSpeed;
			obj.ammo = orig.ammo;
			obj.useAmmo = orig.useAmmo;
			obj.lifeRegen = orig.lifeRegen;
			obj.manaIncrease = orig.manaIncrease;
			obj.buyOnce = orig.buyOnce;
			obj.mana = orig.mana;
			obj.noUseGraphic = orig.noUseGraphic;
			obj.noMelee = orig.noMelee;
			obj.release = orig.release;
			obj.value = orig.value;
			obj.buy = orig.buy;
			obj.social = orig.social;
			obj.vanity = orig.vanity;
			obj.material = orig.material;
			obj.noWet = orig.noWet;
			obj.buffType = orig.buffType;
			obj.buffTime = orig.buffTime;
			obj.netID = orig.netID;
			obj.crit = orig.crit;
			obj.prefix = orig.prefix;
			obj.melee = orig.melee;
			obj.magic = orig.magic;
			obj.ranged = orig.ranged;
			obj.reuseDelay = orig.reuseDelay;
		}

		public static void CopyAttributes(object obj, object original, string type = "")
		{
			Type type2 = obj.GetType();
			FieldInfo[] fields = type2.GetFields();
			ArrayList arrayList = new ArrayList(new string[16]
			{
				"ai",
				"localAI",
				"owner",
				"subclass",
				"customMethodReturnDict",
				"customMethodRefReturnDict",
				"customVars",
				"delegates",
				"globsubclass",
				"globsavedata",
				"unloadedSavedata",
				"savedata",
				"className",
				"HookList",
				"defs",
				"oldPos"
			});
			ArrayList arrayList2 = new ArrayList(new string[19]
			{
				"velocity",
				"oldVelocity",
				"position",
				"oldPosition",
				"directionY",
				"direction",
				"oldDirection",
				"oldDirectionY",
				"target",
				"oldTarget",
				"netUpdate",
				"spriteDirection",
				"defDamage",
				"defDefense",
				"whoAmI",
				"buffTime",
				"buffType",
				"subclass",
				"immune"
			});
			ArrayList arrayList3 = new ArrayList(new string[2]
			{
				"position",
				"velocity"
			});
			ArrayList arrayList4 = new ArrayList(new string[1]
			{
				"playerImmune"
			});
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				if (!fieldInfo.IsStatic && !arrayList.Contains(fieldInfo.Name) && (!(type == "Item") || !arrayList3.Contains(fieldInfo.Name)) && (!(type == "NPC") || !arrayList2.Contains(fieldInfo.Name)) && (!(type == "Projectile") || !arrayList4.Contains(fieldInfo.Name)))
				{
					fieldInfo.SetValue(obj, fieldInfo.GetValue(original));
				}
			}
		}

		public static void SaveRecipesINI()
		{
			string str = tConfigFolder;
			string text = str + "\\ModPacks\\Defaults\\Item";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];
				_ = recipe.createItem.type;
				string path = text + "\\" + recipe.createItem.name + ".ini";
				if (recipe.createItem.type == 0)
				{
					continue;
				}
				IniFile iniFile = IniFile.FromFile(path);
				iniFile["Recipe"]["Amount"] = recipe.createItem.stack.ToString();
				iniFile["Recipe"]["needWater"] = recipe.needWater.ToString();
				string text2 = "";
				Item[] requiredItem = recipe.requiredItem;
				foreach (Item item in requiredItem)
				{
					if (item != null && item.type != 0)
					{
						string text3 = text2;
						text2 = text3 + item.stack + " " + item.name + ",";
					}
				}
				if (text2 != "")
				{
					iniFile["Recipe"]["Items"] = text2.Substring(0, text2.Length - 1);
				}
				text2 = "";
				int[] requiredTile = recipe.requiredTile;
				foreach (int num in requiredTile)
				{
					if (num != -1)
					{
						text2 = text2 + Main.tileName[num] + ",";
					}
				}
				if (text2 != "")
				{
					iniFile["Recipe"]["Tiles"] = text2.Substring(0, text2.Length - 1);
				}
				iniFile.Save(path);
			}
		}

		public static void SaveTilesINI()
		{
			string str = tConfigFolder;
			string path = str + "\\ModPacks\\Defaults\\Tile";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			for (int i = 0; i < 150; i++)
			{
				int num = i;
				string text = Main.tileName[i];
				if (string.IsNullOrEmpty(text))
				{
					text = i.ToString();
				}
				TileDefaults tileDefaults = new TileDefaults();
				tileDefaults.pick = 0f;
				tileDefaults.id = num;
				if (Main.tileAxe[num])
				{
					tileDefaults.axe = -1f;
				}
				if (Main.tileHammer[num])
				{
					tileDefaults.hammer = -1f;
				}
				if (Main.tilePick[num])
				{
					tileDefaults.pick = -1f;
				}
				tileDefaults.Alch = Main.tileAlch[num];
				tileDefaults.BlockLight = Main.tileBlockLight[num];
				tileDefaults.Cut = Main.tileCut[num];
				tileDefaults.Dungeon = Main.tileDungeon[num];
				tileDefaults.FrameImportant = Main.tileFrameImportant[num];
				tileDefaults.LavaDeath = Main.tileLavaDeath[num];
				tileDefaults.Lighted = Main.tileLighted[num];
				tileDefaults.MergeDirt = Main.tileMergeDirt[num];
				tileDefaults.name = Main.tileName[num];
				tileDefaults.NoAttach = Main.tileNoAttach[num];
				tileDefaults.NoFail = Main.tileNoFail[num];
				tileDefaults.NoSunLight = Main.tileNoSunLight[num];
				tileDefaults.Shine = Main.tileShine[num];
				tileDefaults.Shine2 = Main.tileShine2[num];
				tileDefaults.Solid = Main.tileSolid[num];
				tileDefaults.SolidTop = Main.tileSolidTop[num];
				tileDefaults.Stone = Main.tileStone[num];
				tileDefaults.Table = Main.tileTable[num];
				tileDefaults.WaterDeath = Main.tileWaterDeath[num];
				ProcessINI(tileDefaults, "Tile", text, "Defaults", saveAllValues: true);
			}
		}

		public static void SaveWallsINI()
		{
			string str = tConfigFolder;
			string path = str + "\\ModPacks\\Defaults\\Wall";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			for (int i = 0; i < 150; i++)
			{
				int num = i;
				string text = wallDefs.name[i];
				if (string.IsNullOrEmpty(text))
				{
					text = i.ToString();
				}
				WallDefaults wallDefaults = new WallDefaults();
				wallDefaults.name = text;
				wallDefaults.id = num;
				wallDefaults.House = Main.wallHouse[num];
				wallDefaults.Blend = Main.wallBlend[num];
				ProcessINI(wallDefaults, "Wall", text, "Defaults", saveAllValues: true);
			}
		}

		public static void SaveBuffsINI()
		{
			string str = tConfigFolder;
			string text = str + "\\ModPacks\\Defaults\\Buff";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			for (int i = 1; i < 41; i++)
			{
				string path = text + "\\" + Main.buffName[i] + ".ini";
				IniFile iniFile = IniFile.FromFile(path);
				IniFileSection iniFileSection = iniFile["Stats"];
				iniFileSection["id"] = i.ToString();
				iniFileSection["tip"] = Main.buffTip[i];
				iniFileSection["debuff"] = Main.debuff[i].ToString();
				iniFile.Save(path);
			}
		}

		public static void LoadSettings(bool loadMods = true)
		{
			tConfigFolder = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Storage";
			settings = IniFile.FromFile(tConfigFolder + "\\Config Mod.ini");
			if (!Directory.Exists(tConfigFolder + "\\ModPacks"))
			{
				Directory.CreateDirectory(tConfigFolder + "\\ModPacks");
			}
			List<ModDef> list = new List<ModDef>();
			List<ModDef> list2 = new List<ModDef>();
			mods = new List<string>();
			if (generateINI || generateItemObj || !loadMods)
			{
				return;
			}
			List<string> list3 = new List<string>(Directory.GetFiles(tConfigFolder + "\\ModPacks\\", "*.obj"));
			foreach (string item in list3)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
				if (settings["ModPacks"] == null || settings["ModPacks"][fileNameWithoutExtension] == null)
				{
					settings["ModPacks"][fileNameWithoutExtension] = "False";
				}
			}
			if (settings["ModPacks"] != null)
			{
				int num = 0;
				foreach (string key in settings["ModPacks"].GetKeys())
				{
					if (File.Exists(tConfigFolder + "\\ModPacks\\" + key + ".obj"))
					{
						if (settings["ModPacks"][key] != "False")
						{
							mods.Add(key);
							list.Add(new ModDef(key, settings["ModPacks"][key], num));
							num++;
						}
						else
						{
							list2.Add(new ModDef(key, settings["ModPacks"][key], -1));
						}
					}
					settings["ModPacks"].DeleteKey(key);
				}
				TMod.Init();
			}
			foreach (ModDef item2 in list)
			{
				settings["ModPacks"][item2.name] = item2.setting;
			}
			foreach (ModDef item3 in list2)
			{
				settings["ModPacks"][item3.name] = item3.setting;
			}
			if (settings["Config"] != null)
			{
				if (!string.IsNullOrEmpty(settings["Config"]["ignoreErrors"]))
				{
					Main.ignoreErrors = Convert.ToBoolean(settings["Config"]["ignoreErrors"]);
				}
				else
				{
					settings["Config"]["ignoreErrors"] = "True";
				}
			}
			settings.Save(tConfigFolder + "\\Config Mod.ini");
			ComputeHash();
		}

		public static void ComputeHash()
		{
			string str = tConfigFolder;
			string text = "";
			SHA256 sHA = new SHA256Managed();
			foreach (string mod in mods)
			{
				string path = str + "\\ModPacks\\" + mod + ".obj";
				byte[] buffer = File.ReadAllBytes(path);
				byte[] array = sHA.ComputeHash(buffer);
				string str2 = string.Concat(Array.ConvertAll(array, (byte x) => x.ToString("X2")));
				text += str2;
				text += settings["ModPacks"][mod];
			}
			byte[] bytes = Encoding.Default.GetBytes(text);
			SHA256 sHA2 = new SHA256Managed();
			byte[] array2 = sHA2.ComputeHash(bytes);
			string text2 = tConfigHash = string.Concat(Array.ConvertAll(array2, (byte x) => x.ToString("X2")));
		}

		public static void ArmorVisualEffects(Player player)
		{
			if (player.armor[0].type > 0 && player.armor[1].type > 0 && player.armor[2].type > 0 && armorSets[player.armor[0].name] == armorSets[player.armor[1].name] && armorSets[player.armor[1].name] == armorSets[player.armor[2].name])
			{
				player.armor[1].RunMethod("PlayerFrame", player);
			}
			Codable.RunPlayerMethod("FrameEffect", true, player);
		}

		public static void ArmorSetBonusEffects(Player player)
		{
			if (player.armor[0].type > 0 && player.armor[1].type > 0 && player.armor[2].type > 0 && armorSets[player.armor[0].name] == armorSets[player.armor[1].name] && armorSets[player.armor[1].name] == armorSets[player.armor[2].name])
			{
				player.armor[1].RunMethod("SetBonus", player);
			}
		}

		public static void LoadItemObj()
		{
			byte[] item = Data.Item;
			using (MemoryStream memoryStream = new MemoryStream(item, writable: false))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					Type type = new Item().GetType();
					FieldInfo[] fields = type.GetFields();
					for (int i = 0; i < 627; i++)
					{
						Item item2 = new Item();
						LoadGenericObj(new List<FieldInfo>(fields), item2, binaryReader);
						if (!(item2.name == ""))
						{
							itemDefs.byName.Add(item2.name, item2);
							if (item2.netID >= 0)
							{
								itemDefs[itemDefsCount] = item2;
								itemDefsCount++;
							}
						}
					}
					binaryReader.Close();
					memoryStream.Close();
				}
			}
		}

		public static void LoadNPCObj()
		{
			byte[] nPC = Data.NPC;
			using (MemoryStream memoryStream = new MemoryStream(nPC))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					Type type = new NPC().GetType();
					FieldInfo[] fields = type.GetFields();
					for (int i = 1; i < 163; i++)
					{
						if (i == 76)
						{
							npcDefsCount++;
						}
						else
						{
							NPC nPC2 = new NPC();
							LoadGenericObj(new List<FieldInfo>(fields), nPC2, binaryReader);
							nPC2.active = true;
							npcDefs.byName.Add(nPC2.name, nPC2);
							if (nPC2.netID >= 0)
							{
								npcDefs[npcDefsCount] = nPC2;
								npcDefsCount++;
							}
						}
					}
					binaryReader.Close();
					memoryStream.Close();
				}
			}
		}

		public static void LoadProjObj()
		{
			byte[] projectile = Data.Projectile;
			using (MemoryStream memoryStream = new MemoryStream(projectile))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					Type type = new Projectile().GetType();
					FieldInfo[] fields = type.GetFields();
					for (int i = 0; i < 111; i++)
					{
						Projectile projectile2 = new Projectile();
						LoadGenericObj(new List<FieldInfo>(fields), projectile2, binaryReader);
						if (!projDefs.byName.ContainsKey(projectile2.name))
						{
							projDefs.byName.Add(projectile2.name, projectile2);
						}
						projDefs[projDefsCount] = projectile2;
						projDefsCount++;
					}
					binaryReader.Close();
					memoryStream.Close();
				}
			}
		}

		public static void SaveItemObj(Item obj, string itemname, string modpack)
		{
			if (!wroteItems)
			{
				if (File.Exists(tConfigFolder + "\\ModPacks\\Defaults\\Item.obj"))
				{
					File.Delete(tConfigFolder + "\\ModPacks\\Defaults\\Item.obj");
				}
				if (File.Exists(tConfigFolder + "\\ModPacks\\Defaults\\ItemNames.obj"))
				{
					File.Delete(tConfigFolder + "\\ModPacks\\Defaults\\ItemNames.obj");
				}
				wroteItems = true;
			}
			if (!itemDefs.byName.ContainsKey(obj.name) && (obj.netID < 0 || itemDefs[obj.type] == null))
			{
				FileStream fileStream = new FileStream(tConfigFolder + "\\ModPacks\\Defaults\\ItemNames.obj", FileMode.Append);
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(itemname);
				binaryWriter.Close();
				fileStream.Close();
				using (FileStream fileStream2 = new FileStream(tConfigFolder + "\\ModPacks\\Defaults\\Item.obj", FileMode.Append))
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream2))
					{
						SaveGenericObj(obj, binaryWriter2);
						binaryWriter2.Close();
						fileStream2.Close();
					}
				}
				if (obj.netID < 0)
				{
					itemDefs.byName.Add(obj.name, obj);
					return;
				}
				itemDefs[itemDefsCount] = obj;
				itemDefsCount++;
			}
		}

		public static void SaveProjectileObj(Projectile obj, string itemname, string modpack)
		{
			string path = tConfigFolder + "\\ModPacks\\Defaults\\Projectile.obj";
			string path2 = tConfigFolder + "\\ModPacks\\Defaults\\ProjectileNames.obj";
			if (!wroteProjs)
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				if (File.Exists(path2))
				{
					File.Delete(path2);
				}
				wroteProjs = true;
			}
			if (projDefs[obj.type] == null)
			{
				FileStream fileStream = new FileStream(path2, FileMode.Append);
				BinaryWriter binaryWriter = new BinaryWriter(fileStream);
				binaryWriter.Write(itemname);
				binaryWriter.Close();
				fileStream.Close();
				using (FileStream fileStream2 = new FileStream(path, FileMode.Append))
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream2))
					{
						SaveGenericObj(obj, binaryWriter2);
						binaryWriter2.Close();
						fileStream2.Close();
					}
				}
				projDefs[projDefsCount] = obj;
				projDefsCount++;
			}
		}

		public static void SaveNPCObj(NPC obj, string itemname, string modpack)
		{
			string path = tConfigFolder + "\\ModPacks\\Defaults\\NPC.obj";
			if (!wroteNPCs)
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				wroteNPCs = true;
			}
			if (!npcDefs.byName.ContainsKey(obj.name) && (obj.netID < 0 || npcDefs[obj.type] == null) && obj.type != 0)
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Append))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
					{
						SaveGenericObj(obj, binaryWriter);
						binaryWriter.Close();
						fileStream.Close();
					}
				}
				if (obj.netID < 0)
				{
					npcDefs.byName.Add(obj.name, obj);
					return;
				}
				npcDefs[itemDefsCount] = obj;
				npcDefsCount++;
			}
		}

		public static void SaveGenericObj(object obj, BinaryWriter binaryWriter)
		{
			Type type = obj.GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			foreach (FieldInfo item in list)
			{
				if (!item.IsStatic)
				{
					string[] array = item.ToString().Split(' ');
					string text = array[0];
					_ = array[1];
					object value = item.GetValue(obj);
					switch (text)
					{
					case "Double":
						binaryWriter.Write(Convert.ToDouble(value));
						break;
					case "Int32":
						binaryWriter.Write(Convert.ToInt32(value));
						break;
					case "Single":
						binaryWriter.Write(Convert.ToSingle(value, CultureInfo.InvariantCulture.NumberFormat));
						break;
					case "Boolean":
						binaryWriter.Write(Convert.ToBoolean(value));
						break;
					case "Microsoft.Xna.Framework.Color":
					{
						Microsoft.Xna.Framework.Color color = (Microsoft.Xna.Framework.Color)value;
						binaryWriter.Write(color.R);
						binaryWriter.Write(color.G);
						binaryWriter.Write(color.B);
						binaryWriter.Write(color.A);
						break;
					}
					case "Byte":
						binaryWriter.Write((byte)value);
						break;
					case "System.String":
						if (value == null)
						{
							binaryWriter.Write("");
						}
						else
						{
							binaryWriter.Write((string)value);
						}
						break;
					}
				}
			}
		}

		public static void SaveRecipesObj()
		{
			if (!wroteRecipes)
			{
				if (File.Exists(tConfigFolder + "\\ModPacks\\Defaults\\Recipes.obj"))
				{
					File.Delete(tConfigFolder + "\\ModPacks\\Defaults\\Recipes.obj");
				}
				wroteRecipes = true;
			}
			string str = tConfigFolder;
			string text = str + "\\ModPacks\\Defaults";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = text + "\\Recipes.obj";
			using (FileStream fileStream = new FileStream(path, FileMode.Append))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					for (int i = 0; i < Recipe.numRecipes; i++)
					{
						Recipe recipe = Main.recipe[i];
						_ = recipe.createItem.type;
						if (recipe.createItem.type != 0 && recipe.createItem.name != "")
						{
							binaryWriter.Write(recipe.createItem.name);
							binaryWriter.Write(recipe.createItem.stack);
							binaryWriter.Write(recipe.needWater);
							binaryWriter.Write(recipe.needLava);
							string[] array = new string[15];
							int[] array2 = new int[15];
							int num = 0;
							Item[] requiredItem = recipe.requiredItem;
							foreach (Item item in requiredItem)
							{
								if (item != null && item.type > 0)
								{
									array[num] = item.name;
									array2[num] = item.stack;
									num++;
								}
							}
							binaryWriter.Write(num);
							for (int k = 0; k < num; k++)
							{
								binaryWriter.Write(array[k]);
								binaryWriter.Write(array2[k]);
							}
							array = new string[15];
							num = 0;
							int[] requiredTile = recipe.requiredTile;
							foreach (int num2 in requiredTile)
							{
								if (num2 != -1)
								{
									array[num] = Main.tileName[num2];
									num++;
								}
							}
							binaryWriter.Write(num);
							for (int m = 0; m < num; m++)
							{
								binaryWriter.Write(array[m]);
							}
						}
					}
					binaryWriter.Close();
					fileStream.Close();
				}
			}
		}

		public static void LoadRecipesObj()
		{
			byte[] recipes = Data.Recipes;
			using (MemoryStream memoryStream = new MemoryStream(recipes))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					string text = binaryReader.ReadString();
					while (true)
					{
						if (!(text != ""))
						{
							return;
						}
						Recipe recipe = new Recipe();
						recipe.createItem.SetDefaults(text);
						recipe.createItem.stack = binaryReader.ReadInt32();
						recipe.needWater = binaryReader.ReadBoolean();
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							recipe.requiredItem[i].SetDefaults(binaryReader.ReadString());
							recipe.requiredItem[i].stack = binaryReader.ReadInt32();
							if (!generateINI)
							{
								recipe.requiredItem[i].material = true;
								itemDefs.byName[recipe.requiredItem[i].name].material = true;
								itemDefs[recipe.requiredItem[i].type].material = true;
							}
						}
						int num2 = binaryReader.ReadInt32();
						for (int j = 0; j < num2; j++)
						{
							string key = binaryReader.ReadString();
							recipe.requiredTile[j] = tileDefs.ID[key];
						}
						if (text == "Gold Coin")
						{
							Recipe recipe2 = new Recipe();
							recipe2.createItem.SetDefaults(73);
							recipe2.createItem.stack = 1;
							recipe2.requiredItem[0].SetDefaults(72);
							recipe2.requiredItem[0].stack = 100;
							recipeByName[recipe2.createItem.name + " 2"] = recipe2;
						}
						if (text == "Silver Coin")
						{
							Recipe recipe3 = new Recipe();
							recipe3 = new Recipe();
							recipe3.createItem.SetDefaults(72);
							recipe3.createItem.stack = 1;
							recipe3.requiredItem[0].SetDefaults(71);
							recipe3.requiredItem[0].stack = 100;
							recipeByName[recipe3.createItem.name + " 2"] = recipe3;
						}
						recipeByName[text] = recipe;
						if (binaryReader.PeekChar() == -1)
						{
							break;
						}
						text = binaryReader.ReadString();
					}
					binaryReader.Close();
					memoryStream.Close();
				}
			}
		}

		public static void LoadGenericObj(List<FieldInfo> fields, object defaults, object item, BinaryReader binaryReader, string version)
		{
			Type type = defaults.GetType();
			foreach (FieldInfo field in fields)
			{
				if (!field.IsStatic && !(field.Name == "useCode") && !(field.Name == "unloadedPrefix") && !(field.Name == "dontDrawFace") && !(field.Name == "dontRelocate") && !(field.Name == "baseGravity") && !(field.Name == "maxGravity") && !(field.Name == "SpawnBiomes"))
				{
					string[] array = field.ToString().Split(' ');
					string text = array[0];
					string text2 = array[1];
					if (!(type.GetField(text2) == null))
					{
						switch (text2)
						{
						case "toolTip3":
						case "toolTip4":
						case "toolTip5":
						case "toolTip6":
						case "toolTip7":
							if (!CheckVersion(version, "0.17"))
							{
								continue;
							}
							break;
						}
						switch (text)
						{
						case "Double":
							field.SetValue(item, binaryReader.ReadDouble());
							break;
						case "Int32":
							field.SetValue(item, binaryReader.ReadInt32());
							break;
						case "Single":
							field.SetValue(item, binaryReader.ReadSingle());
							break;
						case "Boolean":
							field.SetValue(item, binaryReader.ReadBoolean());
							break;
						case "Microsoft.Xna.Framework.Color":
						{
							Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte());
							field.SetValue(item, color);
							break;
						}
						case "System.String":
							field.SetValue(item, binaryReader.ReadString());
							break;
						}
					}
				}
			}
		}

		public static void LoadGenericObj(List<FieldInfo> fields, object item, BinaryReader binaryReader)
		{
			string text = "";
			IComparer<FieldInfo> comparer = new SortFields();
			fields.Sort(comparer);
			foreach (FieldInfo field in fields)
			{
				if (!field.IsStatic && !(field.Name == "useCode") && !(field.Name == "unloadedPrefix") && !(field.Name == "dontDrawFace") && !(field.Name == "dontRelocate") && !(field.Name == "baseGravity") && !(field.Name == "maxGravity") && !(field.Name == "music") && !(field.Name == "musicName") && (!(item is Item) || !(field.Name == "direction")) && (item is NPC || !(field.Name == "directionY")) && (item is Player || !(field.Name == "gravDir")) && (item is NPC || item is Player || !(field.Name == "direction2")) && !(field.Name == "CritMult") && !(field.Name == "CritChance") && ((!(item is Projectile) && !(item is Item)) || !(field.Name == "displayName")) && (!(item is Projectile) || !(field.Name == "color")) && !(field.Name == "dontDrawLifeText") && !(field.Name == "hurtsTiles"))
				{
					string[] array = field.ToString().Split(' ');
					string text2 = array[0];
					string text3 = array[1];
					switch (text2)
					{
					case "Double":
						field.SetValue(item, binaryReader.ReadDouble());
						break;
					case "Int32":
						field.SetValue(item, binaryReader.ReadInt32());
						break;
					case "Single":
						field.SetValue(item, binaryReader.ReadSingle());
						break;
					case "Boolean":
						field.SetValue(item, binaryReader.ReadBoolean());
						break;
					case "Microsoft.Xna.Framework.Color":
					{
						Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte());
						field.SetValue(item, color);
						break;
					}
					case "Byte":
						field.SetValue(item, binaryReader.ReadByte());
						break;
					case "System.String":
						try
						{
							field.SetValue(item, binaryReader.ReadString());
						}
						catch (Exception ex)
						{
							throw new Exception(string.Concat("Error loading built-in items.\nStopped at: ", text2, " ", text3, "\nCompleted:\n", ex, "\n", text));
						}
						break;
					}
					object obj = text;
					text = string.Concat(obj, text2, " ", text3, "=", field.GetValue(item), "\n");
				}
			}
		}

		public static void LoadCustomItemObj(string modpack, BinaryReader binaryReader, string version)
		{
			if (!CheckVersion(version, "0.24"))
			{
				binaryReader.ReadBoolean();
			}
			Type type = new Item().GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "item", i, num);
				Item item = new Item();
				LoadGenericObj(list, new ItemDefaults(), item, binaryReader, version);
				if (item.type == -1)
				{
					item.type = 604 + customItemsAmt;
					item.netID = item.type;
					int count = binaryReader.ReadInt32();
					MemoryStream stream = new MemoryStream(binaryReader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.itemTexture[item.type] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
					customItemsAmt++;
				}
				if (item.bodySlot == -2 || item.bodySlot == -3)
				{
					if (binaryReader.ReadBoolean())
					{
						hasHands[26 + armorBodyAmt] = true;
					}
					int num2 = 26 + armorBodyAmt;
					LoadCustomImage(binaryReader, Main.armorBodyTexture, num2);
					armorBodyAmt++;
					if (item.bodySlot == -3)
					{
						LoadCustomImage(binaryReader, Main.femaleBodyTexture, num2);
					}
					else
					{
						Main.femaleBodyTexture[num2] = Main.armorBodyTexture[num2];
					}
					if (binaryReader.ReadBoolean())
					{
						LoadCustomImage(binaryReader, Main.armorArmTexture, num2);
					}
					item.bodySlot = num2;
				}
				if (item.legSlot == -2)
				{
					int num3 = 25 + armorLegAmt;
					LoadCustomImage(binaryReader, Main.armorLegTexture, num3);
					item.legSlot = num3;
					armorLegAmt++;
				}
				if (item.headSlot == -2)
				{
					int num4 = 45 + armorHeadAmt;
					LoadCustomImage(binaryReader, Main.armorHeadTexture, num4);
					item.headSlot = num4;
					armorHeadAmt++;
				}
				if (codeExists)
				{
					itemDefs.assemblyByName[item.name] = modDLL[modpack];
					if (item.type >= 604 || itemDefs[item.type].name == item.name)
					{
						itemDefs.assemblyByType[item.type] = modDLL[modpack];
					}
				}
				ItemBuilder itemBuilder = new ItemBuilder(item);
				itemBuilder.ReadObj(modpack, binaryReader, version);
				if (item.type < 604)
				{
					item.netID = itemDefs[item.type].netID;
					Item item2 = new Item();
					item2.SetDefaults(item.name);
					string name = item2.name;
					item.netID = item2.netID;
					if (name != item.name)
					{
						modifiedItemsAmt++;
						item.netID = -24 - modifiedItemsAmt;
					}
				}
				if (item.headSlot > 0)
				{
					Item.headType[item.headSlot] = item.type;
				}
				if (item.bodySlot > 0)
				{
					Item.bodyType[item.bodySlot] = item.type;
				}
				if (item.legSlot > 0)
				{
					Item.legType[item.legSlot] = item.type;
				}
				item.active = true;
				itemDefs.byName[item.name] = item;
				if (item.type >= 604 || itemDefs[item.type].name == item.name)
				{
					itemDefs[item.type] = item;
				}
				itemDefs.modName[item.name] = modpack;
			}
			int num5 = binaryReader.ReadInt32();
			for (int j = 0; j < num5; j++)
			{
				Recipe recipe = new Recipe();
				string text = binaryReader.ReadString();
				recipe.createItem.SetDefaults(text);
				recipe.createItem.stack = binaryReader.ReadInt32();
				recipe.needWater = binaryReader.ReadBoolean();
				recipe.needLava = (CheckVersion(version, "0.28.8") && binaryReader.ReadBoolean());
				int num6 = binaryReader.ReadInt32();
				for (int k = 0; k < num6; k++)
				{
					recipe.requiredItem[k].SetDefaults(binaryReader.ReadString());
					recipe.requiredItem[k].stack = binaryReader.ReadInt32();
					recipe.requiredItem[k].material = true;
					itemDefs[recipe.requiredItem[k].type].material = true;
				}
				int num7 = binaryReader.ReadInt32();
				for (int l = 0; l < num7; l++)
				{
					string text2 = binaryReader.ReadString();
					if (text2 == "Workbench")
					{
						text2 = "Work Bench";
					}
					recipe.requiredTile[l] = tileDefs.ID[text2];
				}
				Dictionary<string, Recipe>.Enumerator enumerator = recipeByName.GetEnumerator();
				if (recipe.createItem.type <= 603)
				{
					List<string> list2 = new List<string>();
					while (enumerator.MoveNext())
					{
						Item createItem = enumerator.Current.Value.createItem;
						Item createItem2 = recipe.createItem;
						if (createItem.netID == createItem2.netID)
						{
							list2.Add(enumerator.Current.Key);
						}
					}
					foreach (string item3 in list2)
					{
						recipeByName.Remove(item3);
					}
				}
				else
				{
					string text3 = null;
					while (enumerator.MoveNext())
					{
						if (IsRecipeTheSame(enumerator.Current.Value, recipe))
						{
							text3 = enumerator.Current.Key;
							break;
						}
					}
					if (text3 != null)
					{
						recipeByName.Remove(text3);
					}
				}
				int m;
				for (m = 0; recipeByName.ContainsKey(text + ((m == 0) ? "" : string.Concat(m))); m++)
				{
				}
				recipeByName[text + ((m == 0) ? "" : string.Concat(m))] = recipe;
			}
		}

		public static bool IsRecipeTheSame(Recipe r1, Recipe r2)
		{
			if ((r1 == null) ^ (r2 == null))
			{
				return false;
			}
			if (r1 == null && r2 == null)
			{
				return true;
			}
			if (r1.needWater != r2.needWater)
			{
				return false;
			}
			if (r1.needLava != r2.needLava)
			{
				return false;
			}
			if (!r1.createItem.IsTheSameAs(r2.createItem))
			{
				return false;
			}
			for (int i = 0; i < r1.requiredTile.Length; i++)
			{
				if (r1.requiredTile[i] != r2.requiredTile[i])
				{
					return false;
				}
			}
			for (int j = 0; j < r1.requiredItem.Length; j++)
			{
				if (!r1.requiredItem[j].IsTheSameAs(r2.requiredItem[j]))
				{
					return false;
				}
				if (r1.requiredItem[j].stack != r2.requiredItem[j].stack)
				{
					return false;
				}
			}
			return true;
		}

		public static void LoadCustomProjObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.24"))
			{
				reader.ReadBoolean();
			}
			Type type = new Projectile().GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			int num = reader.ReadInt32();
			if (num == 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "projectile", i, num);
				Projectile projectile = new Projectile();
				LoadGenericObj(list, new ProjDefaults(), projectile, reader, version);
				if (projectile.type == -1)
				{
					projectile.type = 112 + customProjAmt;
					int count = reader.ReadInt32();
					MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.projectileTexture[projectile.type] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
					customProjAmt++;
				}
				if (!CheckVersion(version, "0.24"))
				{
					reader.ReadBoolean();
				}
				if (codeExists)
				{
					projDefs.assemblyByName[projectile.name] = modDLL[modpack];
					if (projectile.type >= 112 || projDefs[projectile.type].name == projectile.name)
					{
						projDefs.assemblyByType[projectile.type] = modDLL[modpack];
					}
				}
				if (CheckVersion(version, "0.13.1"))
				{
					if (reader.ReadBoolean())
					{
						int value = reader.ReadInt32();
						projDefs.pretendType[projectile.name] = value;
					}
					if (reader.ReadBoolean())
					{
						int value2 = reader.ReadInt32();
						projDefs.aiPretendType[projectile.name] = value2;
					}
					if (reader.ReadBoolean())
					{
						int value3 = reader.ReadInt32();
						projDefs.killPretendType[projectile.name] = value3;
					}
				}
				if (CheckVersion(version, "0.20") && reader.ReadBoolean())
				{
					Main.projFrames[projectile.type] = reader.ReadInt32();
				}
				if (CheckVersion(version, "0.20.1"))
				{
					if (reader.ReadBoolean())
					{
						projDefs.frameOffsetX[projectile.type] = reader.ReadInt32();
					}
					if (reader.ReadBoolean())
					{
						projDefs.frameOffsetY[projectile.type] = reader.ReadInt32();
					}
				}
				if (CheckVersion(version, "0.21.9") && reader.ReadBoolean())
				{
					projDefs.drawPretendType[projectile.type] = reader.ReadInt32();
				}
				if (CheckVersion(version, "0.29.7") && reader.ReadBoolean())
				{
					projectile.displayName = reader.ReadString();
				}
				if (CheckVersion(version, "0.29.9") && reader.ReadBoolean())
				{
					projectile.color = new Microsoft.Xna.Framework.Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
				}
				if (CheckVersion(version, "0.33.0") && reader.ReadBoolean())
				{
					projectile.hurtsTiles = reader.ReadBoolean();
				}
				projectile.active = true;
				projDefs.byName[projectile.name] = projectile;
				if (projectile.type >= 112 || projDefs[projectile.type].name == projectile.name)
				{
					projDefs[projectile.type] = projectile;
				}
				projDefs.modName[projectile.name] = modpack;
				if (projectile.hostile)
				{
					Main.projHostile[projectile.type] = true;
				}
			}
		}

		public static void LoadCustomNPCObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.24"))
			{
				reader.ReadBoolean();
			}
			Type typeFromHandle = typeof(NPC);
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(typeFromHandle.GetFields());
			list.Sort(comparer);
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "NPC", i, num);
				bool flag = false;
				NPC nPC = new NPC();
				bool flag2 = true;
				if (CheckVersion(version, "0.28.5"))
				{
					flag2 = reader.ReadBoolean();
				}
				if (flag2)
				{
					LoadGenericObj(list, new NPCDefaults(), nPC, reader, version);
				}
				else
				{
					Console.WriteLine("Loading modified NPC");
					string key = reader.ReadString();
					nPC = npcDefs.byName[key];
				}
				for (int j = 1; j < 41; j++)
				{
					nPC.buffImmune[j] = reader.ReadBoolean();
				}
				if (nPC.type == -1)
				{
					flag = true;
					nPC.type = 147 + customNPCAmt;
					nPC.netID = nPC.type;
					int count = reader.ReadInt32();
					MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.npcTexture[nPC.type] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
					customNPCAmt++;
				}
				if (!CheckVersion(version, "0.24"))
				{
					reader.ReadBoolean();
				}
				if (codeExists)
				{
					npcDefs.assemblyByName[nPC.name] = modDLL[modpack];
					if (nPC.type >= 147 || nPC.name == npcDefs[nPC.type].name)
					{
						npcDefs.assemblyByType[nPC.type] = modDLL[modpack];
					}
				}
				if (nPC.type >= 147)
				{
					int value = reader.ReadInt32();
					Main.npcFrameCount[nPC.type] = value;
					if (reader.ReadBoolean())
					{
						npcDefs.animationType[nPC.name] = reader.ReadInt32();
					}
				}
				if (nPC.type < 147)
				{
					nPC.netID = npcDefs.byName[nPC.name].netID;
				}
				if (reader.ReadBoolean())
				{
					if (npcDefs.dropTables.ContainsKey(nPC.netID))
					{
						npcDefs.dropTables[nPC.netID].Add(new DropTable(reader, version));
					}
					else
					{
						npcDefs.dropTables.Add(nPC.netID, new List<DropTable>(new DropTable[1]
						{
							new DropTable(reader, version)
						}));
					}
				}
				if (flag && nPC.townNPC)
				{
					int count2 = reader.ReadInt32();
					MemoryStream stream2 = new MemoryStream(reader.ReadBytes(count2));
					if (!Main.dedServ)
					{
						Main.npcHeadTexture[nPC.type] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream2));
					}
					npcDefs.townNPCList.Add(nPC.type);
				}
				if (CheckVersion(version, "0.17") && reader.ReadBoolean())
				{
					nPC.soundHit = SoundHandler.soundID[reader.ReadString()];
				}
				if (CheckVersion(version, "0.17") && reader.ReadBoolean())
				{
					nPC.soundKilled = SoundHandler.soundID[reader.ReadString()];
				}
				if (CheckVersion(version, "0.21.4") && reader.ReadBoolean())
				{
					npcDefs.aiPretendType[nPC.name] = reader.ReadInt32();
				}
				if (CheckVersion(version, "0.28.3") && reader.ReadBoolean())
				{
					nPC.music = reader.ReadInt32();
				}
				if (CheckVersion(version, "0.28.3") && reader.ReadBoolean())
				{
					nPC.musicName = modpack + ":" + reader.ReadString();
				}
				if (CheckVersion(version, "0.28.3") && reader.ReadBoolean())
				{
					string text = reader.ReadString();
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = text.Split(',');
						string[] array2 = array;
						foreach (string key2 in array2)
						{
							if (!Biome.SpawnList.ContainsKey(key2))
							{
								Biome.SpawnList.Add(key2, new List<int>
								{
									nPC.netID
								});
							}
							else
							{
								Biome.SpawnList[key2].Add(nPC.netID);
							}
						}
					}
				}
				if (CheckVersion(version, "0.30.0") && reader.ReadBoolean())
				{
					nPC.dontDrawLifeText = reader.ReadBoolean();
				}
				npcDefs.byName[nPC.name] = nPC;
				if (nPC.type >= 147 || nPC.name == npcDefs[nPC.type].name)
				{
					_ = nPC.type;
					_ = 147;
					npcDefs[nPC.type] = nPC;
				}
				npcDefs.modName[nPC.name] = modpack;
			}
		}

		public static bool LoadCustomObj(int modIndex)
		{
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Expected O, but got Unknown
			string text = mods[modIndex];
			if (modLoaded[text])
			{
				return true;
			}
			string text2 = Path.Combine(tConfigFolder, "ModPacks", text + ".obj");
			string text3 = settings["ModPacks"][text];
			Main.statusText = "Loading mod " + text;
			if (Main.dedServ)
			{
				Console.WriteLine("Loading mod " + text + "...");
			}
			codeExists = false;
			tConfigModTypes[text] = new List<string>();
			jsonDefault[text] = null;
			jsonCurrent[text] = null;
			Stream stream;
			using (stream = new MemoryStream())
			{
				try
				{
					SevenZipExtractor val = (SevenZipExtractor)(object)new SevenZipExtractor(text2);
					try
					{
						try
						{
							MemoryStream memoryStream = new MemoryStream();
							val.ExtractFile("Config.ini", (Stream)memoryStream);
							memoryStream.Position = 0L;
							IniFileReader reader = new IniFileReader(memoryStream);
							IniFile iniFile = IniFile.FromStream(reader);
							ModSetting[text] = iniFile;
							tConfigModTypes[text] = new List<string>(ModSetting[text]["Choice"].GetKeys());
							int num = tConfigModTypes[text].IndexOf(settings["ModPacks"][text]);
							tConfigModChoices[text] = num + 1;
							if (!string.IsNullOrEmpty(iniFile["Settings"]["requires"]))
							{
								string[] array = iniFile["Settings"]["requires"].Split(',');
								for (int i = 0; i < array.Length; i++)
								{
									array[i] = array[i].Trim();
									string text4 = array[i];
									if (!modLoaded[text4])
									{
										int num2 = mods.IndexOf(text4);
										if (num2 == -1 || !LoadCustomObj(num2))
										{
											if (!Main.dedServ)
											{
												messages.Add("ModPack '" + text + "' requires mod " + text4);
											}
											else
											{
												Console.WriteLine();
												Console.WriteLine("Error: ModPack '" + text + "' requires mod " + text4);
											}
											stream.Close();
											if (!tempRun)
											{
												mods.RemoveAt(modIndex);
												settings["ModPacks"][text] = "False";
												settings.Save(Path.Combine(tConfigFolder, "Config Mod.ini"));
												mainInstance.PublicInit();
												return false;
											}
											return false;
										}
									}
								}
							}
							Assembly assembly = null;
							if (assemblies.ContainsKey(text))
							{
								assembly = assemblies[text];
							}
							else
							{
								string text5 = tempModAssembly + Path.DirectorySeparatorChar;
								string str = text + Path.DirectorySeparatorChar + text + ((num > 0) ? ("- " + tConfigModTypes[text][num]) : "") + ".dll";
								List<string> list = new List<string>();
								foreach (string archiveFileName in val.ArchiveFileNames)
								{
									if (archiveFileName.EndsWith(".dll") || archiveFileName.EndsWith(".xnb"))
									{
										list.Add(archiveFileName);
									}
								}
								if (list.Count > 0)
								{
									val.ExtractFiles(text5, list.ToArray());
								}
								str = text5 + str;
								if (File.Exists(str))
								{
									assembly = Assembly.LoadFile(str);
									assemblyPath[text] = str;
								}
							}
							if (assembly != null)
							{
								Console.WriteLine("Added assembly " + assembly.FullName);
								assemblies[assembly.FullName] = assembly;
								assemblies[text] = assembly;
								assemblyName[text] = assembly.FullName;
								if (!Main.dedServ)
								{
									Music.ExtractBanks(val, tempModAssembly, text);
									Audio.ExtractMusic(val, tempModAssembly, text);
								}
								if (iniFile != null && !string.IsNullOrEmpty(iniFile["Settings"]["namespace"]))
								{
									namespaces[assembly.FullName] = iniFile["Settings"]["namespace"];
								}
								else
								{
									namespaces[assembly.FullName] = "Terraria";
								}
								namespaces[text] = namespaces[assembly.FullName];
								modDLL[text] = assembly;
								npcDefs.globalAssembly.Add(assembly);
								npcDefs.globalModname.Add(text);
								itemDefs.globalAssembly.Add(assembly);
								itemDefs.globalModname.Add(text);
								projDefs.globalAssembly.Add(assembly);
								projDefs.globalModname.Add(text);
								codeExists = true;
							}
							memoryStream.Close();
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.ToString());
						}
						switch (text3)
						{
						case "True":
						case "default":
						case "False":
							val.ExtractFile(text + ".obj", stream);
							break;
						default:
							val.ExtractFile((text + " " + text3).Trim() + ".obj", stream);
							break;
						}
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							try
							{
								val.ExtractFile("Settings.json", (Stream)memoryStream2);
								memoryStream2.Position = 0L;
								StreamReader streamReader = new StreamReader(memoryStream2);
								string json = streamReader.ReadToEnd();
								JsonData value = JsonMapper.ToObject(json);
								jsonDefault[text] = value;
								jsonCurrent[text] = JsonMapper.ToObject(json);
								LoadModSettings(text);
								value = jsonCurrent[text];
								foreach (KeyValuePair<string, JsonData> item in (IEnumerable)value["settings"])
								{
									if (!item.Value.Has("value"))
									{
										item.Value["value"] = item.Value["default"];
									}
								}
								SaveModSettings(text);
							}
							catch (Exception)
							{
							}
						}
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
				}
				catch (Exception)
				{
					stream = new FileStream(text2, FileMode.Open);
				}
				stream.Position = 0L;
				BinaryReader binaryReader;
				using (binaryReader = new BinaryReader(stream))
				{
					stream.Position = 0L;
					try
					{
						string text6 = binaryReader.ReadString();
						if (!CheckVersion(text6, Constants.minRequiredVersion))
						{
							if (!Main.dedServ)
							{
								messages.Add("ModPack '" + text + "' is an outdated version (" + text6 + ")");
								messages.Add("    Rebuild it to use it with version " + Constants.version);
							}
							else
							{
								Console.WriteLine();
								Console.WriteLine("Error: ModPack '" + text + "' is an outdated version (" + text6 + ")");
								Console.WriteLine("    Rebuild it to use it with version " + Constants.version);
								Console.WriteLine("    " + text + " has been disabled");
								Console.WriteLine();
							}
							binaryReader.Close();
							stream.Close();
							if (!tempRun)
							{
								mods.RemoveAt(modIndex);
								settings["ModPacks"][text] = "False";
								settings.Save(Path.Combine(tConfigFolder, "Config Mod.ini"));
								mainInstance.PublicInit();
								return false;
							}
							return false;
						}
						int value2 = 0;
						if (CheckVersion(text6, "0.20.5"))
						{
							value2 = binaryReader.ReadInt32();
						}
						modVersion[text] = value2;
						if (CheckVersion(text6, "0.22.8") && binaryReader.ReadBoolean())
						{
							modDLVersion[text] = binaryReader.ReadString();
							modURL[text] = binaryReader.ReadString();
						}
						LoadCustomObjBinary(text, binaryReader, text6);
						modLoaded[text] = true;
						binaryReader.Close();
						stream.Close();
					}
					catch (Exception ex4)
					{
						messages.Add("Failed to load " + text);
						messages.Add(ex4.Message);
						messages.Add("");
						Console.WriteLine("");
						Console.WriteLine("Error: Failed to load " + text);
						Console.WriteLine(ex4.Message);
						Console.WriteLine(text + " has been disabled");
						Console.WriteLine("");
						binaryReader.Close();
						stream.Close();
						if (!tempRun)
						{
							mods.RemoveAt(modIndex);
							settings["ModPacks"][text] = "False";
							settings.Save(Path.Combine(tConfigFolder, "Config Mod.ini"));
							mainInstance.PublicInit();
							return false;
						}
					}
				}
			}
			return true;
		}

		public static void LoadCustomObjBinary(string modpack, BinaryReader reader, string version)
		{
			LoadCustomDLL(modpack, reader, version);
			LoadCustomSounds(modpack, reader, version);
			LoadCustomTileObj(modpack, reader, version);
			LoadCustomWallObj(modpack, reader, version);
			LoadCustomProjObj(modpack, reader, version);
			LoadCustomHoldStyleObj(modpack, reader, version);
			LoadCustomUseStyleObj(modpack, reader, version);
			Prefix.LoadPrefixes(modpack, reader, version);
			LoadCustomItemObj(modpack, reader, version);
			LoadCustomBuffObj(modpack, reader, version);
			LoadCustomNPCObj(modpack, reader, version);
			LoadCustomGoreObj(modpack, reader, version);
			Main.statusText = "";
		}

		public static bool LoadCustomObjBuiltin(string modpack, byte[] binary)
		{
			mods.Add(modpack);
			MemoryStream memoryStream;
			using (memoryStream = new MemoryStream(binary))
			{
				BinaryReader binaryReader;
				using (binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						string text = binaryReader.ReadString();
						if (!CheckVersion(text, "0.18.3"))
						{
							if (!Main.dedServ)
							{
								messages.Add("ModPack '" + modpack + "' is an outdated version (" + text + ")");
								messages.Add("    Rebuild it to use it with version " + Constants.version);
								Console.WriteLine("ModPack '" + modpack + "' is an outdated version (" + text + ")");
								Console.WriteLine("    Rebuild it to use it with version " + Constants.version);
							}
							binaryReader.Close();
							memoryStream.Close();
							return true;
						}
						if (CheckVersion(text, "0.20.5"))
						{
							binaryReader.ReadInt32();
						}
						LoadCustomObjBinary(modpack, binaryReader, text);
						binaryReader.Close();
						memoryStream.Close();
					}
					catch (Exception)
					{
						messages.Add("Failed to load " + modpack);
						messages.Add("");
						Console.WriteLine("Failed to load " + modpack);
						Console.WriteLine("");
						binaryReader.Close();
						memoryStream.Close();
						mainInstance.PublicInit();
						return false;
					}
				}
			}
			return true;
		}

		public static void SetLoadStatus(string modpack, string type, int num, int total)
		{
			if (modpack.Length > 20)
			{
				modpack = modpack.Substring(0, 20);
			}
			Main.statusText = "Loading " + modpack + " " + type + " " + (num + 1) + "/" + total;
		}

		public static void LoadCustomHoldStyleObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.20"))
			{
				return;
			}
			int num = reader.ReadInt32();
			int num2 = 0;
			string text;
			while (true)
			{
				if (num2 < num)
				{
					SetLoadStatus(modpack, "holdstyle", num2, num);
					text = reader.ReadString();
					Assembly assembly = modDLL[modpack];
					IHoldStyle holdStyle = (IHoldStyle)assembly.CreateInstance(namespaces[assembly.FullName] + "." + Codable.ParseName(text) + "HoldStyle");
					if (holdStyle == null)
					{
						break;
					}
					holdStyleDefs.style[holdStyleAmt] = holdStyle;
					holdStyleDefs.ID[text] = holdStyleAmt;
					holdStyleAmt++;
					num2++;
					continue;
				}
				return;
			}
			throw new Exception("Error: HoldStyle " + text + " issue...");
		}

		public static void LoadCustomUseStyleObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.20.1"))
			{
				return;
			}
			int num = reader.ReadInt32();
			int num2 = 0;
			string text;
			while (true)
			{
				if (num2 < num)
				{
					SetLoadStatus(modpack, "usestyle", num2, num);
					text = reader.ReadString();
					Assembly assembly = modDLL[modpack];
					IUseStyle useStyle = (IUseStyle)assembly.CreateInstance(namespaces[assembly.FullName] + "." + Codable.ParseName(text) + "UseStyle");
					if (useStyle == null)
					{
						break;
					}
					useStyleDefs.style[useStyleAmt] = useStyle;
					useStyleDefs.ID[text] = useStyleAmt;
					useStyleAmt++;
					num2++;
					continue;
				}
				return;
			}
			throw new Exception("Error: UseStyle " + text + " issue...");
		}

		public static void LoadCustomWallObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.18.2"))
			{
				return;
			}
			Type type = new WallDefaults().GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "wall", i, num);
				WallDefaults wallDefaults = new WallDefaults();
				LoadGenericObj(list, new WallDefaults(), wallDefaults, reader, version);
				if (wallDefaults.id == -1)
				{
					wallDefaults.id = 32 + customWallAmt;
					int count = reader.ReadInt32();
					MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.wallTexture[wallDefaults.id] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
					customWallAmt++;
					if (CheckVersion(version, "0.22.7"))
					{
						wallDefs.color[wallDefaults.id] = new int[3]
						{
							reader.ReadByte(),
							reader.ReadByte(),
							reader.ReadByte()
						};
					}
				}
				int id = wallDefaults.id;
				wallDefs.ID[wallDefaults.name] = id;
				wallDefs.name[id] = wallDefaults.name;
				if (!string.IsNullOrEmpty(wallDefaults.DropName))
				{
					wallDefs.dropName[id] = wallDefaults.DropName;
				}
				Main.wallHouse[id] = wallDefaults.House;
				Main.wallBlend[id] = wallDefaults.Blend;
				wallDefs.modName[wallDefaults.name] = modpack;
			}
		}

		public static void LoadCustomTileObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.17.4"))
			{
				return;
			}
			if (!CheckVersion(version, "0.24"))
			{
				reader.ReadBoolean();
			}
			Type type = new TileDefaults().GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			int num = reader.ReadInt32();
			if (150 + customTileAmt + num > 65534)
			{
				throw new Exception("Error: Too many tiles loaded! Try using fewer mods at a time.");
			}
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "tile", i, num);
				TileDefaults tileDefaults = new TileDefaults();
				LoadGenericObj(list, new TileDefaults(), tileDefaults, reader, version);
				if (tileDefaults.id == -1)
				{
					tileDefaults.id = 150 + customTileAmt;
					int count = reader.ReadInt32();
					MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.tileTexture[tileDefaults.id] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
					customTileAmt++;
					if (CheckVersion(version, "0.22.5"))
					{
						tileDefs.color[tileDefaults.id] = new int[3]
						{
							reader.ReadByte(),
							reader.ReadByte(),
							reader.ReadByte()
						};
					}
				}
				if (codeExists)
				{
					tileDefs.assemblyByName[tileDefaults.name] = modDLL[modpack];
					tileDefs.assemblyByType[tileDefaults.id] = modDLL[modpack];
				}
				int id = tileDefaults.id;
				Main.tileAxe[id] = (tileDefaults.axe > 0f);
				Main.tileHammer[id] = (tileDefaults.hammer > 0f);
				Main.tilePick[id] = (tileDefaults.pick > 0f);
				if (tileDefaults.pick >= 0f)
				{
					tileDefs.pick[id] = tileDefaults.pick;
				}
				if (tileDefaults.hammer >= 0f)
				{
					tileDefs.hammer[id] = tileDefaults.hammer;
				}
				if (tileDefaults.axe >= 0f)
				{
					tileDefs.axe[id] = tileDefaults.axe;
				}
				tileDefs.minPick[id] = tileDefaults.minPick;
				tileDefs.minAxe[id] = tileDefaults.minAxe;
				tileDefs.minHammer[id] = tileDefaults.minHammer;
				Main.tileAlch[id] = tileDefaults.Alch;
				Main.tileBlockLight[id] = tileDefaults.BlockLight;
				Main.tileCut[id] = tileDefaults.Cut;
				Main.tileDungeon[id] = tileDefaults.Dungeon;
				Main.tileFrameImportant[id] = tileDefaults.FrameImportant;
				Main.tileLavaDeath[id] = tileDefaults.LavaDeath;
				Main.tileLighted[id] = tileDefaults.Lighted;
				Main.tileMergeDirt[id] = tileDefaults.MergeDirt;
				Main.tileName[id] = tileDefaults.name;
				Main.tileNoAttach[id] = tileDefaults.NoAttach;
				Main.tileNoFail[id] = tileDefaults.NoFail;
				Main.tileNoSunLight[id] = tileDefaults.NoSunLight;
				Main.tileShine[id] = tileDefaults.Shine;
				Main.tileShine2[id] = tileDefaults.Shine2;
				Main.tileSolid[id] = tileDefaults.Solid;
				Main.tileSolidTop[id] = tileDefaults.SolidTop;
				Main.tileStone[id] = tileDefaults.Stone;
				Main.tileTable[id] = tileDefaults.Table;
				Main.tileWaterDeath[id] = tileDefaults.WaterDeath;
				if (!string.IsNullOrEmpty(tileDefaults.DropName))
				{
					tileDefs.dropName[id] = tileDefaults.DropName;
				}
				if (tileDefaults.PretendType > 0)
				{
					tileDefs.pretendType[id] = tileDefaults.PretendType;
				}
				tileDefs.height[id] = tileDefaults.Height;
				tileDefs.width[id] = tileDefaults.Width;
				if (tileDefaults.furniture > -1)
				{
					tileDefs.furniture[id] = tileDefaults.furniture;
				}
				tileDefs.ID[tileDefaults.name] = id;
				tileDefs.modName[tileDefaults.name] = modpack;
				TileBuilder tileBuilder = new TileBuilder(tileDefaults);
				tileBuilder.ReadObj(modpack, reader, version);
			}
			Main.tileCount = new int[customTileAmt + 150 + 1];
		}

		public static void LoadCustomGoreObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.17.1"))
			{
				return;
			}
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "gore", i, num);
				string text = reader.ReadString();
				int count = reader.ReadInt32();
				MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
				int num2 = 160 + customGoreAmt;
				customGoreAmt++;
				if (!Main.dedServ)
				{
					if (text.StartsWith("NPMA"))
					{
						Main.goreTexture[num2] = Texture2D.FromStream(mainInstance.GraphicsDevice, stream);
					}
					else
					{
						Main.goreTexture[num2] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
				}
				goreID[text] = num2;
			}
		}

		public static void LoadCustomSounds(string modpack, BinaryReader reader, string version)
		{
			if (CheckVersion(version, "0.17"))
			{
				int num = reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					SetLoadStatus(modpack, "sound", i, num);
					string name = reader.ReadString();
					int count = reader.ReadInt32();
					byte[] bytes = reader.ReadBytes(count);
					SoundHandler.AddSound(name, bytes);
				}
			}
		}

		public static void LoadCustomDLL(string modpack, BinaryReader reader, string version)
		{
			if (CheckVersion(version, "0.16.9") && reader.ReadBoolean())
			{
				int count = reader.ReadInt32();
				byte[] rawAssembly = reader.ReadBytes(count);
				if (modpack == "Built-In")
				{
					Assembly assembly = Assembly.Load(rawAssembly);
					namespaces[assembly.FullName] = "Terraria";
					namespaces[modpack] = namespaces[assembly.FullName];
					modDLL[modpack] = assembly;
					npcDefs.globalAssembly.Add(assembly);
					npcDefs.globalModname.Add(modpack);
					itemDefs.globalAssembly.Add(assembly);
					itemDefs.globalModname.Add(modpack);
					projDefs.globalAssembly.Add(assembly);
					projDefs.globalModname.Add(modpack);
					codeExists = true;
				}
			}
		}

		public static void LoadCustomBuffObj(string modpack, BinaryReader reader, string version)
		{
			if (!CheckVersion(version, "0.24"))
			{
				reader.ReadBoolean();
			}
			Type type = new BuffDefaults().GetType();
			IComparer<FieldInfo> comparer = new SortFields();
			List<FieldInfo> list = new List<FieldInfo>(type.GetFields());
			list.Sort(comparer);
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				SetLoadStatus(modpack, "buff", i, num);
				BuffDefaults buffDefaults = new BuffDefaults();
				LoadGenericObj(list, new BuffDefaults(), buffDefaults, reader, version);
				int num2 = buffDefaults.id;
				if (num2 == -1)
				{
					num2 = (buffDefaults.id = Main.buffName.GetSize());
					int count = reader.ReadInt32();
					MemoryStream stream = new MemoryStream(reader.ReadBytes(count));
					if (!Main.dedServ)
					{
						Main.buffTexture[buffDefaults.id] = ConvertToPreMultipliedAlpha(Texture2D.FromStream(mainInstance.GraphicsDevice, stream));
					}
				}
				Main.buffName[num2] = buffDefaults.name;
				Main.buffAlpha[num2] = buffDefaults.alpha;
				Main.buffTip[num2] = buffDefaults.tip;
				Main.debuff[num2] = buffDefaults.debuff;
				buffDefs.ID[buffDefaults.name] = num2;
				buffDefs.modName[num2] = modpack;
				if (!CheckVersion(version, "0.24"))
				{
					reader.ReadBoolean();
				}
				if (codeExists)
				{
					buffDefs.assemblyByName[buffDefaults.name] = modDLL[modpack];
					buffDefs.assemblyByType[buffDefaults.id] = modDLL[modpack];
				}
				if (CheckVersion(version, "0.28.3"))
				{
					Main.buffDontDisplayTime[num2] = reader.ReadBoolean();
				}
			}
		}

		public static void SaveBuffSaveData(Player player, string playerPath)
		{
			string path = playerPath + ".buff.moddata";
			MemoryStream memoryStream = new MemoryStream();
			SaveBuffSaveData(player, memoryStream);
			File.WriteAllBytes(path, memoryStream.ToArray());
			memoryStream.Close();
		}

		public static void SaveBuffSaveData(Player player, MemoryStream stream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			for (int i = 0; i < player.buffType.Length; i++)
			{
				if (player.buffCode[i] == null)
				{
					binaryWriter.Write(0);
					continue;
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
				try
				{
					Codable.RunSpecifiedMethod("Buff " + Main.buffName[player.buffType[i]], player.buffCode[i], "Save", binaryWriter2);
				}
				catch (Exception)
				{
					memoryStream = new MemoryStream();
				}
				binaryWriter.Write(memoryStream.ToArray().Length);
				binaryWriter.Write(memoryStream.ToArray());
				binaryWriter2.Close();
				memoryStream.Close();
			}
		}

		public static void LoadItem(string playerPath, BinaryReader reader, BinaryReader sepReader, int version, Item item, bool loadStack = true)
		{
			LoadItem(reader, sepReader, version, item, loadStack);
		}

		public static void LoadItem(BinaryReader reader, BinaryReader sepReader, int version, Item item, bool loadStack = true)
		{
			int num = reader.ReadInt32();
			bool forceItemLoad = false;
			bool flag = false;
			if (version >= 2 && (num >= 604 || num < -24))
			{
				string itemName = "";
				forceItemLoad = true;
				try
				{
					itemName = itemDefs.playerLoad[num];
				}
				catch
				{
					flag = true;
				}
				if (!flag)
				{
					item.SetDefaults(itemName);
				}
			}
			else
			{
				item.netDefaults(num);
			}
			if (loadStack)
			{
				item.stack = reader.ReadInt32();
			}
			Prefix.LoadPrefix(reader, item, "player");
			if (version >= 4)
			{
				try
				{
					Codable.LoadCustomDataNew(item, sepReader, version, GetModVersion(item));
				}
				catch (Exception)
				{
				}
			}
			else if (version >= 1)
			{
				Codable.LoadCustomData(item, reader, version, forceItemLoad);
			}
		}

		public static int GetCurrentVersion(Codable item)
		{
			if (item.className == "Item" && itemDefs.modName.ContainsKey(item.name))
			{
				return modVersion[itemDefs.modName[item.name]];
			}
			return 0;
		}

		public static int GetModVersion(Codable item)
		{
			if (string.IsNullOrEmpty(item.name))
			{
				return 0;
			}
			if (item.className == "Item")
			{
				if (itemDefs.modName.ContainsKey(item.name))
				{
					return loadedVersion[itemDefs.modName[item.name]];
				}
			}
			else if (item.className == "NPC" && npcDefs.modName.ContainsKey(item.name))
			{
				return loadedVersion[npcDefs.modName[item.name]];
			}
			return 0;
		}

		public static void DeletePlayer(string path)
		{
			string[] array = new string[5]
			{
				".buff.moddata",
				".global.moddata",
				".items.ini",
				".items.moddata",
				".version.moddata"
			};
			string[] array2 = array;
			foreach (string str in array2)
			{
				string text = path + str;
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				if (File.Exists(text + ".bak"))
				{
					File.Delete(text + ".bak");
				}
			}
		}

		public static void DeleteWorld(string path)
		{
			if (path.IndexOf(".wld") <= -1)
			{
				return;
			}
			string[] array = new string[7]
			{
				".IDs.ini",
				".global.moddata",
				".NPC.moddata",
				".Tile.moddata",
				".version.moddata",
				".items.moddata",
				".omnitool.dat"
			};
			string[] array2 = array;
			foreach (string str in array2)
			{
				string text = path + str;
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				if (File.Exists(text + ".bak"))
				{
					File.Delete(text + ".bak");
				}
			}
		}

		public static void SaveCustomTileData(MemoryStream stream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(stream))
			{
				binaryWriter.Write(customTileAmt);
				for (int i = 150; i < 150 + customTileAmt; i++)
				{
					if (tileDefs.assemblyByType[i] != null)
					{
						Codable codable = new Codable();
						codable.name = Main.tileName[i];
						codable.className = "Tile";
						codable.Init();
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
						Codable.RunSpecifiedMethod(codable.className + " " + codable.name, codable.subclass, "SaveGlobal", binaryWriter2);
						binaryWriter.Write(memoryStream.ToArray().Length);
						binaryWriter.Write(memoryStream.ToArray());
					}
					else
					{
						binaryWriter.Write(0);
					}
				}
				int num = 0;
				MemoryStream memoryStream2 = new MemoryStream();
				BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2);
				foreach (Vector2 key in tileDefs.code.Keys)
				{
					Codable codable2 = tileDefs.code[key];
					MemoryStream memoryStream3 = new MemoryStream();
					BinaryWriter binaryWriter4 = new BinaryWriter(memoryStream3);
					Codable.RunSpecifiedMethod(codable2.className + " " + codable2.name, codable2.subclass, "Save", binaryWriter4);
					if (memoryStream3.ToArray().Length > 0)
					{
						num++;
						binaryWriter3.Write((int)key.X);
						binaryWriter3.Write((int)key.Y);
						binaryWriter3.Write(memoryStream3.ToArray().Length);
						binaryWriter3.Write(memoryStream3.ToArray());
					}
				}
				binaryWriter.Write(num);
				binaryWriter.Write(memoryStream2.ToArray());
				binaryWriter3.Close();
				memoryStream2.Close();
			}
		}

		public static void LoadCustomTileData(string path, int version)
		{
			if (File.Exists(path))
			{
				LoadCustomTileData(new MemoryStream(File.ReadAllBytes(path)), version);
			}
		}

		public static void LoadCustomTileData(MemoryStream stream, int version)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				int num = binaryReader.ReadInt32();
				for (int i = 150; i < 150 + num; i++)
				{
					int num2 = binaryReader.ReadInt32();
					if (num2 > 0)
					{
						MemoryStream memoryStream = new MemoryStream();
						BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
						binaryWriter.Write(binaryReader.ReadBytes(num2));
						BinaryReader binaryReader2 = new BinaryReader(memoryStream);
						memoryStream.Position = 0L;
						try
						{
							string text = tileDefs.load[i];
							if (!string.IsNullOrEmpty(text))
							{
								int i2 = tileDefs.ID[text];
								Codable codable = new Codable();
								codable.name = Main.tileName[i2];
								codable.className = "Tile";
								codable.Init();
								if (codable != null && codable.subclass != null)
								{
									Codable.RunSpecifiedMethod(codable.className + " " + codable.name, codable.subclass, "LoadGlobal", binaryReader2, loadedVersion[tileDefs.modName[text]]);
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
				if (version < 5)
				{
					binaryReader.Close();
					stream.Close();
				}
				else
				{
					num = binaryReader.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						int num3 = binaryReader.ReadInt32();
						int num4 = binaryReader.ReadInt32();
						int num5 = binaryReader.ReadInt32();
						if (num5 > 0)
						{
							int type = Main.tile[num3, num4].type;
							Vector2 pos = new Vector2(num3, num4);
							MemoryStream memoryStream2 = new MemoryStream();
							BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
							binaryWriter2.Write(binaryReader.ReadBytes(num5));
							BinaryReader binaryReader3 = new BinaryReader(memoryStream2);
							memoryStream2.Position = 0L;
							try
							{
								Codable.RunTileMethod(false, pos, type, "Load", binaryReader3, loadedVersion[Main.tileName[type]]);
							}
							catch (Exception)
							{
							}
						}
					}
					binaryReader.Close();
					stream.Close();
				}
			}
		}

		public static void LoadModVersion(string path)
		{
			path += ".version.moddata";
			if (File.Exists(path))
			{
				LoadModVersion(new MemoryStream(File.ReadAllBytes(path)));
			}
		}

		public static void LoadModVersion(MemoryStream stream)
		{
			try
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string key = binaryReader.ReadString();
					int value = binaryReader.ReadInt32();
					loadedVersion[key] = value;
				}
				binaryReader.Close();
				stream.Close();
			}
			catch (Exception)
			{
			}
		}

		public static void SaveModVersion(string path)
		{
			string path2 = path + ".version.moddata";
			MemoryStream memoryStream = new MemoryStream();
			SaveModVersion(memoryStream);
			File.WriteAllBytes(path2, memoryStream.ToArray());
			memoryStream.Close();
		}

		public static void SaveModVersion(MemoryStream stream)
		{
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				binaryWriter.Write(modVersion.Keys.Count);
				foreach (string key in modVersion.Keys)
				{
					binaryWriter.Write(key);
					binaryWriter.Write(modVersion[key]);
				}
				binaryWriter.Close();
			}
			catch (Exception)
			{
			}
		}

		public static void InitializeGlobalMod(string type)
		{
			TMod.HookOwners hookOwners = TMod.HookOwners.ModWorld;
			if (type == "ModWorld")
			{
				Events.world.ResetEvents();
				TMod.ResetWorld();
			}
			else if (type == "ModPlayer")
			{
				Events.player.ResetEvents();
				TMod.ResetPlayer();
				hookOwners = TMod.HookOwners.ModPlayer;
			}
			int num = 0;
			foreach (KeyValuePair<string, Assembly> item in modDLL)
			{
				object obj = item.Value.CreateInstance(namespaces[item.Key] + "." + type);
				if (obj != null)
				{
					globalMod[type][item.Key] = obj;
					switch (hookOwners)
					{
					case TMod.HookOwners.ModWorld:
						Events.world.RegisterEvents(obj);
						break;
					case TMod.HookOwners.ModPlayer:
						Events.player.RegisterEvents(obj);
						break;
					}
				}
				num++;
			}
			Dictionary<string, object> dictionary = globalMod[type];
			num = 0;
			foreach (KeyValuePair<string, object> item2 in dictionary)
			{
				object value = item2.Value;
				TMod.Init(value, hookOwners, mods.IndexOf(item2.Key));
				if (value != null)
				{
					Codable.RunSpecifiedMethod(item2.Key + " " + type, value, "Initialize", mods.IndexOf(item2.Key));
				}
				num++;
			}
		}

		public static void LoadGlobalModData(string path, string type)
		{
			path += ".global.moddata";
			if (File.Exists(path))
			{
				LoadGlobalModData(new MemoryStream(File.ReadAllBytes(path)), type);
			}
		}

		public static void LoadGlobalModData(MemoryStream stream, string type)
		{
			InitializeGlobalMod(type);
			Dictionary<string, object> dictionary = globalMod[type];
			try
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				int num = binaryReader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string text = binaryReader.ReadString();
					int count = binaryReader.ReadInt32();
					MemoryStream memoryStream = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
					binaryWriter.Write(binaryReader.ReadBytes(count));
					if (memoryStream.ToArray().Length > 0)
					{
						if (!dictionary.ContainsKey(text))
						{
							unloadedModData[type + ":" + text] = memoryStream.ToArray();
							continue;
						}
						memoryStream.Position = 0L;
						BinaryReader binaryReader2 = new BinaryReader(memoryStream);
						object obj = dictionary[text];
						if (obj != null)
						{
							Codable.RunSpecifiedMethod(text + " " + type, obj, "Load", binaryReader2, loadedVersion[text]);
						}
						binaryReader2.Close();
					}
					binaryWriter.Close();
					memoryStream.Close();
				}
				binaryReader.Close();
				stream.Close();
			}
			catch (Exception)
			{
			}
		}

		public static void SaveGlobalModData(string path, string type)
		{
			string path2 = path + ".global.moddata";
			Stream stream = new FileStream(path2, FileMode.Create);
			SaveGlobalModData(stream, type);
			stream.Close();
		}

		public static void SaveGlobalModData(Stream stream, string type)
		{
			Dictionary<string, object> dictionary = globalMod[type];
			try
			{
				BinaryWriter binaryWriter = new BinaryWriter(stream);
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
				int num = 0;
				foreach (string key in dictionary.Keys)
				{
					string text = key;
					MemoryStream memoryStream2 = new MemoryStream();
					BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2);
					object obj = dictionary[text];
					if (obj != null)
					{
						Codable.RunSpecifiedMethod(text + " " + type, obj, "Save", binaryWriter3);
					}
					if (memoryStream2.ToArray().Length > 0)
					{
						num++;
						binaryWriter2.Write(text);
						binaryWriter2.Write(memoryStream2.ToArray().Length);
						binaryWriter2.Write(memoryStream2.ToArray());
					}
					binaryWriter3.Close();
					memoryStream2.Close();
				}
				foreach (string key2 in unloadedModData.Keys)
				{
					string a = key2.Split(':')[0];
					string value = key2.Split(':')[1];
					if (a == type)
					{
						num++;
						binaryWriter2.Write(value);
						binaryWriter2.Write(unloadedModData[key2].Length);
						binaryWriter2.Write(unloadedModData[key2]);
					}
				}
				binaryWriter.Write(num);
				binaryWriter.Write(memoryStream.ToArray());
				binaryWriter2.Close();
				memoryStream.Close();
			}
			catch (Exception)
			{
			}
		}

		public static void LoadBuffSaveData(Player player, string path, int version)
		{
			path += ".buff.moddata";
			if (File.Exists(path))
			{
				LoadBuffSaveData(player, new MemoryStream(File.ReadAllBytes(path)), version);
			}
		}

		public static void LoadBuffSaveData(Player player, MemoryStream stream, int version)
		{
			if (version >= 4 && stream.Length != 0)
			{
				try
				{
					BinaryReader binaryReader = new BinaryReader(stream);
					for (int i = 0; i < player.buffType.Length; i++)
					{
						int num = binaryReader.ReadInt32();
						if (num != 0)
						{
							MemoryStream memoryStream = new MemoryStream();
							BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
							binaryWriter.Write(binaryReader.ReadBytes(num));
							memoryStream.Position = 0L;
							BinaryReader binaryReader2 = new BinaryReader(memoryStream);
							if (!(buffDefs.assemblyByType[player.buffType[i]] == null))
							{
								Assembly assembly = buffDefs.assemblyByType[player.buffType[i]];
								player.buffCode[i] = assembly.CreateInstance(namespaces[assembly.FullName] + "." + Codable.ParseName(Main.buffName[player.buffType[i]]) + "Buff");
								string key = buffDefs.modName[player.buffType[i]];
								Codable.RunSpecifiedMethod("Buff " + Main.buffName[player.buffType[i]], player.buffCode[i], "Load", binaryReader2, loadedVersion[key]);
							}
						}
					}
					binaryReader.Close();
					stream.Close();
				}
				catch (Exception)
				{
				}
			}
		}

		public static bool CheckVersion(string version, string required)
		{
			string[] array = version.Split('.');
			string[] array2 = required.Split('.');
			if (array.Length == 2)
			{
				array = (version + ".0").Split('.');
			}
			if (array2.Length == 2)
			{
				array2 = (required + ".0").Split('.');
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num = Convert.ToInt32(array[i]);
				int num2 = Convert.ToInt32(array2[i]);
				if (num < num2)
				{
					return false;
				}
				if (num > num2)
				{
					return true;
				}
			}
			return true;
		}

		public static void CheckObjUpdates()
		{
			Main.statusText = "Checking for updates";
			modObjURL = new Dictionary<string, string>();
			modNewVersion = new Dictionary<string, string>();
			downloadModUpdate = new Dictionary<string, bool>();
			updatesAvailable = false;
			WebClient webClient = new WebClient();
			try
			{
				string text = webClient.DownloadString("https://content.wuala.com/contents/Surfpup/Documents/Projects/Terraria/Release/ModPacks/tConfig%20Update/version.txt");
				if (CheckGreaterVersion(text, Constants.version))
				{
					modNewVersion["tConfig"] = text;
					modObjURL["tConfig"] = "https://content.wuala.com/contents/Surfpup/Documents/Projects/Terraria/Release/ModPacks/tConfig%20Update/TerrariaConfigMod.patch?dl=1";
					downloadModUpdate["tConfig"] = true;
					updatesAvailable = true;
				}
				foreach (string key in modURL.Keys)
				{
					string address = modURL[key];
					string required = modDLVersion[key];
					string text2 = webClient.DownloadString(address);
					string[] array = text2.Split('\n');
					string text3 = array[0];
					string value = array[1];
					if (CheckGreaterVersion(text3, required))
					{
						modNewVersion[key] = text3;
						modObjURL[key] = value;
						downloadModUpdate[key] = true;
						updatesAvailable = true;
					}
				}
			}
			catch (Exception)
			{
			}
			Main.statusText = "";
		}

		public static bool CheckGreaterVersion(string version, string required)
		{
			string[] array = version.Split('.');
			string[] array2 = required.Split('.');
			if (array.Length == 2)
			{
				array = (version + ".0").Split('.');
			}
			if (array2.Length == 2)
			{
				array2 = (required + ".0").Split('.');
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num = Convert.ToInt32(array[i]);
				int num2 = Convert.ToInt32(array2[i]);
				if (num < num2)
				{
					return false;
				}
				if (num > num2)
				{
					return true;
				}
			}
			return false;
		}

		public static void SetupModTransfer(int id)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(mods.Count - 1);
			Console.WriteLine("Setting up transfer with " + id);
			if (modStream == null)
			{
				foreach (string mod in mods)
				{
					if (!(mod == "Built-In"))
					{
						Console.WriteLine("Adding mod " + mod);
						string path = Path.Combine(tConfigFolder, "ModPacks", mod + ".obj");
						byte[] array = File.ReadAllBytes(path);
						binaryWriter.Write(mod);
						binaryWriter.Write(settings["ModPacks"][mod]);
						binaryWriter.Write(array.Length);
						binaryWriter.Write(array);
					}
				}
				modStream = new MemoryStream(memoryStream.ToArray());
				modReader = new BinaryReader(modStream);
				modsTotal = mods.Count;
				int num = 65535;
				num -= 24;
				int num2 = modPartsTotal = (int)(memoryStream.Length / num) + 1;
			}
			modPartsTransferred[id] = 0;
			modStreamPos[id] = 0;
		}

		public static byte[] TransferModPart(int id)
		{
			modStream.Position = modStreamPos[id];
			int num = 65535;
			num -= 24;
			int num2 = (int)(modReader.BaseStream.Length - modReader.BaseStream.Position);
			if (num2 < num)
			{
				num = num2;
			}
			modPartsTransferred[id]++;
			Console.WriteLine("Transferring mod part to " + id + " " + modPartsTransferred[id] + "/" + modPartsTotal);
			byte[] array = modReader.ReadBytes(num);
			modStreamPos[id] = (int)modStream.Position;
			Console.WriteLine("Mod part is " + array.Length + " bytes");
			if (modPartsTransferred[id] == 1)
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(modPartsTotal);
				SHA256 sHA = new SHA256Managed();
				byte[] array2 = sHA.ComputeHash(modStream.ToArray());
				string.Concat(Array.ConvertAll(array2, (byte x) => x.ToString("X2")));
				binaryWriter.Write(array);
				if (modPartsTransferred[id] == modPartsTotal)
				{
					modPartsTransferred[id] = 0;
					modStreamPos[id] = 0;
				}
				return memoryStream.ToArray();
			}
			if (modPartsTransferred[id] == modPartsTotal)
			{
				modPartsTransferred[id] = 0;
				modStreamPos[id] = 0;
			}
			return array;
		}

		public static bool ReceiveModPart(int bsize, BinaryReader recvreader)
		{
			if (!modTransferStarted)
			{
				modPartsTransferred[0] = 0;
				modPartsTotal = recvreader.ReadInt32();
				modStream = new MemoryStream();
				modWriter = new BinaryWriter(modStream);
				modTransferStarted = true;
			}
			modPartsTransferred[0]++;
			int num = 65535;
			num -= 24;
			if (bsize < num)
			{
				num = bsize;
			}
			modWriter.Write(recvreader.ReadBytes(num));
			if (modPartsTransferred[0] == modPartsTotal)
			{
				Main.statusText = "Re-initializing & loading mods";
				SHA256 sHA = new SHA256Managed();
				foreach (string key in settings["ModPacks"].GetKeys())
				{
					if (File.Exists(tConfigFolder + "\\ModPacks\\" + key + ".obj"))
					{
						settings["ModPacks"].DeleteKey(key);
					}
				}
				modStream.Position = 0L;
				BinaryReader binaryReader = new BinaryReader(modStream);
				int num2 = binaryReader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					string text = binaryReader.ReadString();
					string value = binaryReader.ReadString();
					int count = binaryReader.ReadInt32();
					byte[] array = binaryReader.ReadBytes(count);
					if (File.Exists(Path.Combine(tConfigFolder, "ModPacks", text + ".obj")))
					{
						sHA = new SHA256Managed();
						byte[] array2 = sHA.ComputeHash(array);
						string a = string.Concat(Array.ConvertAll(array2, (byte x) => x.ToString("X2")));
						array2 = sHA.ComputeHash(File.ReadAllBytes(Path.Combine(tConfigFolder, "ModPacks", text + ".obj")));
						string b = string.Concat(Array.ConvertAll(array2, (byte x) => x.ToString("X2")));
						if (a != b)
						{
							File.WriteAllBytes(Path.Combine(tConfigFolder, "ModPacks", text + ".obj"), array);
						}
					}
					else
					{
						File.WriteAllBytes(Path.Combine(tConfigFolder, "ModPacks", text + ".obj"), array);
					}
					settings["ModPacks"][text] = value;
				}
				settings.Save(Main.SavePath + "\\Config Mod.ini");
				mainInstance.PublicInit();
				foreach (KeyValuePair<string, JsonData> item in jsonCurrent.dict)
				{
					SaveModSettings(item.Key);
				}
				return true;
			}
			return false;
		}

		public static void PerformModTransfer(int whoAmI)
		{
			if (modConfirmed[whoAmI])
			{
				NetMessage.SendData(103, whoAmI);
			}
			else
			{
				NetMessage.SendData(3, whoAmI);
			}
		}

		public static void SaveTileNames()
		{
			string text = Main.worldPathName + ".IDs.ini";
			string destFileName = text + ".bak";
			if (File.Exists(text))
			{
				File.Copy(text, destFileName, overwrite: true);
			}
			IniFile iniFile = IniFile.FromFile(text);
			for (int i = 150; i < 150 + customTileAmt; i++)
			{
				iniFile["Tiles"][i.ToString()] = Main.tileName[i];
			}
			for (int j = 32; j < 32 + customWallAmt; j++)
			{
				iniFile["Walls"][j.ToString()] = wallDefs.name[j];
			}
			iniFile.Save(text);
		}

		public static void SaveOmnitoolData()
		{
			string path = Main.worldPathName + ".omnitool.dat";
			string str = "{\"Tiles\": {";
			for (int i = 150; i < 150 + customTileAmt; i++)
			{
				str = str + "\"" + i + "\" : ";
				string text = str;
				str = text + "[\"" + Main.tileName[i] + "\", " + Main.tileFrameImportant[i].ToString().ToLower() + "]";
				if (i < 150 + customTileAmt - 1)
				{
					str += ",";
				}
			}
			str += "}}";
			File.WriteAllText(path, str);
		}

		public static void LoadTileNames()
		{
			try
			{
				tileDefs.load = new DictionaryHandler<int, string>();
				string path = Main.worldPathName + ".IDs.ini";
				if (File.Exists(path))
				{
					IniFile iniFile = IniFile.FromFile(path);
					ReadOnlyCollection<string> keys = iniFile["Tiles"].GetKeys();
					foreach (string item in keys)
					{
						int key = Convert.ToInt32(item);
						string value = iniFile["Tiles"][item];
						tileDefs.load[key] = value;
					}
					keys = iniFile["Walls"].GetKeys();
					foreach (string item2 in keys)
					{
						int key2 = Convert.ToInt32(item2);
						string value2 = iniFile["Walls"][item2];
						wallDefs.load[key2] = value2;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static void SavePlayerItemNames(string path)
		{
			string path2 = path + ".items.ini";
			MemoryStream memoryStream = new MemoryStream();
			SavePlayerItemNames(memoryStream);
			File.WriteAllBytes(path2, memoryStream.ToArray());
			memoryStream.Close();
		}

		public static void SavePlayerItemNames(MemoryStream stream)
		{
			IniFileWriter iniFileWriter = new IniFileWriter(stream);
			IniFile iniFile = new IniFile();
			foreach (string key in itemDefs.byName.Keys)
			{
				int netID = itemDefs.byName[key].netID;
				if (netID < -24 || netID >= 604)
				{
					iniFile["Items"][netID.ToString()] = key;
				}
			}
			iniFileWriter.WriteIniFile(iniFile);
			iniFileWriter.Flush();
		}

		public static void LoadPlayerItemNames(string path)
		{
			path += ".items.ini";
			if (File.Exists(path))
			{
				LoadPlayerItemNames(new MemoryStream(File.ReadAllBytes(path)));
			}
		}

		public static void LoadPlayerItemNames(MemoryStream stream)
		{
			try
			{
				itemDefs.playerLoad = new Dictionary<int, string>();
				IniFileReader reader = new IniFileReader(stream);
				IniFile iniFile = IniFile.FromStream(reader);
				ReadOnlyCollection<string> keys = iniFile["Items"].GetKeys();
				foreach (string item in keys)
				{
					int key = Convert.ToInt32(item);
					string value = iniFile["Items"][item];
					itemDefs.playerLoad[key] = value;
				}
			}
			catch (Exception)
			{
			}
		}

		public static void SaveWorldItemNames()
		{
			string path = Main.worldPathName + ".IDs.ini";
			IniFile iniFile = IniFile.FromFile(path);
			for (int i = 604; i < 604 + customItemsAmt; i++)
			{
				iniFile["Items"][i.ToString()] = Main.itemName[i];
			}
			iniFile.Save(path);
		}

		public static void LoadWorldItemNames()
		{
			try
			{
				itemDefs.worldLoad = new Dictionary<int, string>();
				string path = Main.worldPathName + ".IDs.ini";
				if (File.Exists(path))
				{
					IniFile iniFile = IniFile.FromFile(path);
					ReadOnlyCollection<string> keys = iniFile["Items"].GetKeys();
					foreach (string item in keys)
					{
						int key = Convert.ToInt32(item);
						string value = iniFile["Items"][item];
						itemDefs.worldLoad[key] = value;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		public static bool CheckTilePlacement(int x, int y, int width, int height, int type)
		{
			string text = "solid ";
			if (Main.tileSolid[type])
			{
				text = "solid wall ceiling side ";
			}
			if (tileDefs.placeOn.ContainsKey(type))
			{
				text = tileDefs.placeOn[type];
			}
			int num = y - (height - 1);
			int num2 = x + width - 1;
			if (text.Contains("solid "))
			{
				bool flag = true;
				for (int i = x; i <= num2; i++)
				{
					if (Main.tile[i, y + 1] == null || !Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
					{
						flag = false;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			if (text.Contains("nonsolid "))
			{
				bool flag2 = true;
				for (int j = x; j <= num2; j++)
				{
					if (Main.tile[j, y + 1] == null || !Main.tile[j, y + 1].active || Main.tileSolid[Main.tile[j, y + 1].type])
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					return true;
				}
			}
			if (text.Contains("solidTop "))
			{
				bool flag3 = true;
				for (int k = x; k <= num2; k++)
				{
					if (Main.tile[k, y + 1] == null || !Main.tile[k, y + 1].active || !Main.tileSolidTop[Main.tile[k, y + 1].type])
					{
						flag3 = false;
					}
				}
				if (flag3)
				{
					return true;
				}
			}
			if (text.Contains("ceiling "))
			{
				bool flag4 = true;
				for (int l = x; l <= num2; l++)
				{
					if (Main.tile[l, num - 1] == null || !Main.tile[l, num - 1].active || !Main.tileSolid[Main.tile[l, num - 1].type])
					{
						flag4 = false;
					}
				}
				if (flag4)
				{
					return true;
				}
			}
			if (text.Contains("wall "))
			{
				bool flag5 = true;
				for (int m = x; m <= num2; m++)
				{
					for (int n = num; n <= y; n++)
					{
						if (Main.tile[m, n] == null || Main.tile[m, n].wall <= 0)
						{
							flag5 = false;
						}
					}
				}
				if (flag5)
				{
					return true;
				}
			}
			if (text.Contains("side "))
			{
				bool flag6 = true;
				for (int num3 = num; num3 <= y; num3++)
				{
					if ((Main.tile[x - 1, num3] == null || !Main.tile[x - 1, num3].active || !Main.tileSolid[Main.tile[x - 1, num3].type]) && (Main.tile[num2 + 1, num3] == null || !Main.tile[num2 + 1, num3].active || !Main.tileSolid[Main.tile[num2 + 1, num3].type]))
					{
						flag6 = false;
					}
				}
				if (flag6)
				{
					return true;
				}
			}
			if (text.Contains("air "))
			{
				return true;
			}
			return false;
		}

		public static bool CheckPlaceTile(int x, int y, int width, int height, int type)
		{
			foreach (string key in modDLL.Keys)
			{
				object obj = modDLL[key].CreateInstance(namespaces[key] + "." + Codable.ParseName(Main.tileName[type]) + "Tile");
				if (obj != null)
				{
					if (!Codable.RunSpecifiedMethod("Tile CheckPlaceTile", obj, "CheckPlaceTile", x, y))
					{
						break;
					}
					return (bool)Codable.customMethodReturn;
				}
			}
			int startY = y - (height - 1);
			int endX = x + width - 1;
			if (!WorldGen.EmptyTileCheck(x, endX, startY, y))
			{
				return false;
			}
			return CheckTilePlacement(x, y, width, height, type);
		}

		public static bool PlaceTile(int x, int y, int width, int height, int type, int plr = -1)
		{
			int num = 1;
			if (plr > -1)
			{
				num = Main.player[plr].direction;
			}
			Vector2 pos = new Vector2(x, y);
			tilesPretend = false;
			if (tileDefs.doorType[type] == 1)
			{
				bool flag = false;
				flag = PlaceDoor(x, y, type);
				if (flag)
				{
					Codable.InitTile(pos, type);
					Codable.RunTileMethod(true, pos, type, "PlaceTile", x, y, plr);
				}
				return flag;
			}
			if (!CheckPlaceTile(x, y, width, height, type))
			{
				return false;
			}
			int num2 = 0;
			if (tileDefs.directional[type] && num == 1)
			{
				num2 = 1;
			}
			else if (plr > -1)
			{
				Player player = Main.player[plr];
				num2 = itemDefs.placeFrame[player.inventory[player.selectedItem].name];
			}
			if (width > 1 || height > 1)
			{
				short num3 = (short)(num2 * 18 * width);
				int num4 = y - (height - 1);
				int num5 = x + width - 1;
				for (int i = x; i <= num5; i++)
				{
					short num6 = 0;
					for (int j = num4; j <= y; j++)
					{
						Main.tile[i, j].active = true;
						Main.tile[i, j].frameY = num6;
						Main.tile[i, j].frameX = num3;
						Main.tile[i, j].type = (ushort)type;
						Main.tile[i, j].frameNumber = (byte)num2;
						num6 = (short)(num6 + 18);
					}
					num3 = (short)(num3 + 18);
				}
				for (int k = x; k <= num5; k++)
				{
					for (int l = num4; l <= y; l++)
					{
						WorldGen.SquareTileFrame(k, l);
					}
				}
			}
			else
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x, y].frameX = (short)(num2 * 18);
				Main.tile[x, y].frameNumber = (byte)num2;
				WorldGen.SquareTileFrame(x, y);
			}
			Codable.InitTile(pos, type);
			Codable.RunTileMethod(true, pos, type, "PlaceTile", x, y, plr);
			return true;
		}

		public static bool PlaceDoor(int i, int j, int type)
		{
			int num = tileDefs.width[type];
			int num2 = tileDefs.height[type];
			for (int k = i; k < i + num; k++)
			{
				for (int l = j - num2 - 1; l < j + num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
				}
			}
			bool flag = true;
			bool flag2 = true;
			for (int m = i; m < i + num; m++)
			{
				for (int num3 = j - 1; num3 > j - num2; num3--)
				{
					if (Main.tile[m, num3].active)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				for (int n = i; n < i + num; n++)
				{
					if (!Main.tile[n, j - num2].active || !Main.tileSolid[Main.tile[n, j - num2].type] || !Main.tile[n, j + 1].active || !Main.tileSolid[Main.tile[n, j + 1].type])
					{
						flag = false;
					}
				}
			}
			for (int num4 = i; num4 < i + num; num4++)
			{
				for (int num5 = j + 1; num5 < j + num2; num5++)
				{
					if (Main.tile[num4, num5].active)
					{
						flag2 = false;
					}
				}
			}
			if (flag2)
			{
				for (int num6 = i; num6 < i + num; num6++)
				{
					if (!Main.tile[num6, j + num2].active || !Main.tileSolid[Main.tile[num6, j + num2].type] || !Main.tile[num6, j - 1].active || !Main.tileSolid[Main.tile[num6, j - 1].type])
					{
						flag2 = false;
					}
				}
			}
			bool flag3 = false;
			if (flag)
			{
				try
				{
					for (int num7 = i; num7 < i + num; num7++)
					{
						for (int num8 = j - num2 + 1; num8 < j + 1; num8++)
						{
							Main.tile[num7, num8].active = true;
							Main.tile[num7, num8].type = (ushort)type;
							Main.tile[num7, num8].frameY = (short)((num8 - (j - num2 + 1)) * 18);
							Main.tile[num7, num8].frameX = (short)((num7 - i) * 18);
						}
					}
				}
				catch
				{
					return false;
				}
				flag3 = true;
			}
			else if (flag2)
			{
				try
				{
					for (int num9 = i; num9 < i + num; num9++)
					{
						for (int num10 = j; num10 < j + num2; num10++)
						{
							Main.tile[num9, num10].active = true;
							Main.tile[num9, num10].type = (ushort)type;
							Main.tile[num9, num10].frameY = (short)((num10 - j) * 18);
							Main.tile[num9, num10].frameX = (short)((num9 - i) * 18);
						}
					}
				}
				catch
				{
					return false;
				}
				flag3 = true;
			}
			if (flag3)
			{
				if (flag)
				{
					for (int num11 = i - 1; num11 < i + num + 1; num11++)
					{
						for (int num12 = j - num2; num12 < j + 2; num12++)
						{
							WorldGen.TileFrame(num11, num12);
						}
					}
				}
				else
				{
					for (int num13 = i - 1; num13 < i + num + 1; num13++)
					{
						for (int num14 = j - 1; num14 < j + num2 + 1; num14++)
						{
							WorldGen.TileFrame(num13, num14);
						}
					}
				}
				return true;
			}
			return false;
		}

		public static bool PlaceDoorOld(int i, int j, int type)
		{
			try
			{
				if (Main.tile[i, j - 2] == null)
				{
					Main.tile[i, j - 2] = new Tile();
				}
				if (Main.tile[i, j - 1] == null)
				{
					Main.tile[i, j - 1] = new Tile();
				}
				if (Main.tile[i, j] == null)
				{
					Main.tile[i, j] = new Tile();
				}
				if (Main.tile[i, j + 1] == null)
				{
					Main.tile[i, j - 1] = new Tile();
				}
				if (Main.tile[i, j - 2].active && Main.tileSolid[Main.tile[i, j - 2].type] && Main.tile[i, j + 2].active && Main.tileSolid[Main.tile[i, j + 2].type] && (Main.tile[i, j - 1] == null || !Main.tile[i, j - 1].active) && (Main.tile[i, j] == null || !Main.tile[i, j].active) && (Main.tile[i, j + 1] == null || !Main.tile[i, j + 1].active))
				{
					Main.tile[i, j - 1].active = true;
					Main.tile[i, j - 1].type = (ushort)type;
					Main.tile[i, j - 1].frameY = 0;
					Main.tile[i, j - 1].frameX = 0;
					Main.tile[i, j].active = true;
					Main.tile[i, j].type = (ushort)type;
					Main.tile[i, j].frameY = 18;
					Main.tile[i, j].frameX = 0;
					Main.tile[i, j + 1].active = true;
					Main.tile[i, j + 1].type = (ushort)type;
					Main.tile[i, j + 1].frameY = 36;
					Main.tile[i, j + 1].frameX = 0;
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static void CheckTile(int i, int j, int width, int height, int type, bool noBreak = false)
		{
			Codable.RunTileMethod(false, new Vector2(i, j), type, "UpdateFrame", i, j);
			bool flag = false;
			bool flag2 = false;
			if (Codable.RunTileMethod(false, new Vector2(i, j), type, "CheckTile", i, j))
			{
				flag = !(bool)Codable.customMethodReturn;
			}
			else if (width > 1 || height > 1 || tileDefs.special[type])
			{
				if (tileDefs.doorType[type] == 1)
				{
					if (!WorldGen.destroyObject)
					{
						int num = i - Main.tile[i, j].frameX / 18;
						int num2 = j - Main.tile[i, j].frameY / 18;
						for (int k = num; k < num + width; k++)
						{
							for (int l = num2 - 1; l < num2 + height + 1; l++)
							{
								if (Main.tile[k, l] == null)
								{
									Main.tile[k, l] = new Tile();
								}
							}
						}
						bool flag3 = false;
						for (int m = num; m < num + width; m++)
						{
							if (!Main.tile[m, num2 - 1].active || !Main.tile[m, num2 + height].active || !Main.tileSolid[Main.tile[m, num2 - 1].type] || !Main.tileSolid[Main.tile[m, num2 + height].type])
							{
								flag3 = true;
							}
						}
						for (int n = num; n < num + width; n++)
						{
							for (int num3 = num2; num3 < num2 + height; num3++)
							{
								if (!Main.tile[n, num3].active || Main.tile[n, num3].type != type)
								{
									flag3 = true;
								}
							}
						}
						flag = flag3;
						if (flag)
						{
							WorldGen.destroyObject = true;
							for (int num4 = num; num4 < num + width; num4++)
							{
								for (int num5 = num2; num5 < num2 + height; num5++)
								{
									WorldGen.KillTile(num4, num5);
								}
							}
							flag = false;
							flag2 = true;
						}
						WorldGen.destroyObject = false;
					}
				}
				else if (tileDefs.doorType[type] == 2)
				{
					if (!WorldGen.destroyObject)
					{
						int num6 = i - Main.tile[i, j].frameX / 18;
						int num7 = j - Main.tile[i, j].frameY / 18;
						num6 += Main.tile[i, j].frameNumber * tileDefs.width[type];
						int num8 = 1;
						if (Main.tile[i, j].frameNumber == 1)
						{
							num8 = -1;
						}
						int key = tileDefs.ID[tileDefs.doorToggle[type]];
						int num9 = tileDefs.width[key];
						int num10 = tileDefs.height[key];
						bool flag4 = false;
						int num11 = num6;
						if (num8 == -1)
						{
							num11 += tileDefs.width[type] - num9;
						}
						for (int num12 = num11; num12 < num11 + num9; num12++)
						{
							if (!Main.tile[num12, num7 - 1].active || !Main.tile[num12, num7 + num10].active || !Main.tileSolid[Main.tile[num12, num7 - 1].type] || !Main.tileSolid[Main.tile[num12, num7 + num10].type])
							{
								flag4 = true;
								WorldGen.destroyObject = true;
								flag2 = true;
							}
						}
						for (int num13 = num6; num13 < num6 + tileDefs.width[type]; num13++)
						{
							for (int num14 = num7; num14 < num7 + tileDefs.height[type]; num14++)
							{
								if (!flag4 && (Main.tile[num13, num14].type != type || !Main.tile[num13, num14].active))
								{
									WorldGen.destroyObject = true;
									flag2 = true;
									flag4 = true;
									num13 = num6;
									num14 = num7;
								}
								if (flag4)
								{
									WorldGen.KillTile(num13, num14);
								}
							}
						}
						WorldGen.destroyObject = false;
					}
				}
				else if (!WorldGen.destroyObject)
				{
					flag = false;
					Vector2 pos = Codable.GetPos(new Vector2(i, j));
					int num15 = (int)pos.X;
					int num16 = (int)pos.Y;
					flag = !CheckTilePlacement(num15, num16 + (height - 1), width, height, type);
					if (!flag)
					{
						int num17 = num15;
						int num18 = num16;
						int num19 = num15 + width;
						int num20 = num16 + height;
						for (int num21 = num17; num21 < num19; num21++)
						{
							for (int num22 = num18; num22 < num20; num22++)
							{
								if (Main.tile[num21, num22] == null || !Main.tile[num21, num22].active || Main.tile[num21, num22].type != type)
								{
									flag = true;
									break;
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				Vector2 pos2 = Codable.GetPos(new Vector2(i, j));
				int num23 = (int)pos2.X;
				int num24 = (int)pos2.Y;
				int num25 = num23;
				int num26 = num24;
				int num27 = num23 + width;
				int num28 = num24 + height;
				WorldGen.destroyObject = true;
				for (int num29 = num25; num29 < num27; num29++)
				{
					for (int num30 = num26; num30 < num28; num30++)
					{
						if (Main.tile[num29, num30].type == type && Main.tile[num29, num30].active)
						{
							WorldGen.KillTile(num29, num30, fail: false, effectOnly: false, noItem: true);
						}
					}
				}
				WorldGen.destroyObject = false;
				for (int num31 = num25 - 1; num31 < num27 + 1; num31++)
				{
					for (int num32 = num26 - 1; num32 < num28 + 1; num32++)
					{
						WorldGen.TileFrame(num31, num32, resetFrame: false, noBreak: true);
					}
				}
				Codable.DestroyTile(new Vector2(i, j));
				flag2 = true;
			}
			if (flag2)
			{
				if (tileDefs.dropName[type] != null)
				{
					int type2 = itemDefs.byName[tileDefs.dropName[type]].type;
					Item.NewItem(i * 16, j * 16, 16, 16, type2);
				}
				DropTable val = null;
				if (tileDefs.dropTables.TryGetValue(type, out val))
				{
					foreach (Drop drop in val.drops)
					{
						Item item = drop.TryDrop();
						if (item != null)
						{
							Item.NewItem(i * 16, j * 16, 16, 16, item.type, item.stack);
						}
					}
				}
			}
		}

		public static bool OpenCustomDoor(int i, int j, int direction, string openDoorType)
		{
			if (!tileDefs.ID.ContainsKey(openDoorType))
			{
				return false;
			}
			int openDoorType2 = tileDefs.ID[openDoorType];
			return OpenCustomDoor(i, j, direction, openDoorType2);
		}

		public static bool OpenCustomDoor(int i, int j, int direction, int openDoorType)
		{
			int num = 0;
			Vector2 pos = Codable.GetPos(new Vector2(i, j));
			int num2 = tileDefs.width[Main.tile[i, j].type];
			int num3 = tileDefs.height[Main.tile[i, j].type];
			int num4 = tileDefs.width[openDoorType];
			int num5 = tileDefs.height[openDoorType];
			int num6 = i - Main.tile[i, j].frameX / 18;
			int num7 = j - Main.tile[i, j].frameY / 18;
			for (int k = num6; k < num6 + num2; k++)
			{
				for (int l = num7 - num3 + 1; l < num7 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
				}
			}
			int num8 = num6 + num2;
			if (direction == -1)
			{
				num++;
				num8 -= num4;
			}
			for (int m = num8; m < num8 + num4 - num2; m++)
			{
				for (int n = num7; n < num7 + num5; n++)
				{
					if (Main.tile[m, n] == null)
					{
						Main.tile[m, n] = new Tile();
					}
					if (Main.tile[m, n].active)
					{
						int type = Main.tile[m, n].type;
						if (type != 3 && type != 24 && type != 52 && type != 61 && type != 62 && type != 69 && type != 71 && type != 73 && type != 74)
						{
							return false;
						}
						WorldGen.KillTile(m, n);
					}
				}
			}
			if (direction != -1)
			{
				num8 -= num2;
			}
			Main.PlaySound(8, i * 16, j * 16);
			for (int num9 = num8; num9 < num8 + num4; num9++)
			{
				for (int num10 = num7; num10 < num7 + num5; num10++)
				{
					Main.tile[num9, num10].active = true;
					Main.tile[num9, num10].type = (ushort)openDoorType;
					Main.tile[num9, num10].frameY = (short)((num10 - num7) * 18);
					Main.tile[num9, num10].frameX = (short)((num9 - num8 + num * num4) * 18);
					Main.tile[num9, num10].frameNumber = (byte)num;
				}
			}
			for (int num11 = num8 - 1; num11 < num8 + num4 + 1; num11++)
			{
				for (int num12 = num7 - 1; num12 < num7 + num5 + 1; num12++)
				{
					WorldGen.TileFrame(num11, num12);
				}
			}
			if (tileDefs.code.ContainsKey(pos))
			{
				Codable value = tileDefs.code[pos];
				Codable.DestroyTile(pos);
				Vector2 pos2 = Codable.GetPos(new Vector2(i, j));
				tileDefs.code[pos2] = value;
			}
			return true;
		}

		public static bool CloseCustomDoor(int i, int j, string closedDoorType, bool forced = false)
		{
			if (!tileDefs.ID.ContainsKey(closedDoorType))
			{
				return false;
			}
			int closedDoorType2 = tileDefs.ID[closedDoorType];
			return CloseCustomDoor(i, j, closedDoorType2, forced);
		}

		public static bool CloseCustomDoor(int i, int j, int closedDoorType, bool forced = false)
		{
			Vector2 pos = Codable.GetPos(new Vector2(i, j));
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			int num = i - Main.tile[i, j].frameX / 18;
			int num2 = j - Main.tile[i, j].frameY / 18;
			int type = Main.tile[i, j].type;
			num += Main.tile[i, j].frameNumber * tileDefs.width[type];
			int num3 = 1;
			if (Main.tile[i, j].frameNumber == 1)
			{
				num3 = -1;
			}
			int key = tileDefs.ID[tileDefs.doorToggle[type]];
			int num4 = tileDefs.width[key];
			int num5 = tileDefs.height[key];
			int num6 = num;
			if (num3 == -1)
			{
				num6 += tileDefs.width[type] - num4;
			}
			for (int k = num6; k < num6 + num4; k++)
			{
				for (int l = num2; l < num2 + num5; l++)
				{
					if (!Collision.EmptyTile(k, l, ignoreTiles: true))
					{
						return false;
					}
				}
			}
			for (int m = num; m < num + tileDefs.width[type]; m++)
			{
				for (int n = num2; n < num2 + num5; n++)
				{
					if (Main.tile[m, n] == null)
					{
						Main.tile[m, n] = new Tile();
					}
					if (m >= num6 && m < num6 + num4)
					{
						Main.tile[m, n].type = (ushort)closedDoorType;
						Main.tile[m, n].frameX = (short)((m - num6) * 18);
						Main.tile[m, n].frameY = (short)((n - num2) * 18);
						Main.tile[m, n].frameNumber = 0;
					}
					else
					{
						Main.tile[m, n].active = false;
					}
				}
			}
			if (Main.netMode != 1)
			{
				for (int num7 = num6; num7 < num6 + num4; num7++)
				{
					for (int num8 = num2; num8 < num2 + num5; num8++)
					{
						if (WorldGen.numNoWire < WorldGen.maxWire - 1)
						{
							WorldGen.noWireX[WorldGen.numNoWire] = num7;
							WorldGen.noWireY[WorldGen.numNoWire] = num8;
							WorldGen.numNoWire++;
						}
					}
				}
			}
			for (int num9 = num6 = 1; num9 < num6 + num4 + 1; num9++)
			{
				for (int num10 = num2 - 1; num10 < num2 + num5 + 1; num10++)
				{
					WorldGen.TileFrame(num9, num10);
				}
			}
			if (tileDefs.code.ContainsKey(pos))
			{
				Codable value = tileDefs.code[pos];
				Codable.DestroyTile(pos);
				Vector2 pos2 = Codable.GetPos(new Vector2(i, j));
				tileDefs.code[pos2] = value;
			}
			Main.PlaySound(9, i * 16, j * 16);
			return true;
		}

		public static int[] CalculateAverageColor(Texture2D texture)
		{
			MemoryStream memoryStream = new MemoryStream();
			texture.SaveAsPng(memoryStream, texture.Width, texture.Height);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			Bitmap bm = (Bitmap)Image.FromStream(memoryStream);
			memoryStream.Close();
			return CalculateAverageColor(bm);
		}

		public unsafe static int[] CalculateAverageColor(Bitmap bm)
		{
			int width = bm.Width;
			int height = bm.Height;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 15;
			int num5 = 0;
			long[] array = new long[3];
			long[] array2 = array;
			int num6 = (bm.PixelFormat == PixelFormat.Format24bppRgb) ? 3 : 4;
			BitmapData bitmapData = bm.LockBits(new System.Drawing.Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
			int stride = bitmapData.Stride;
			IntPtr scan = bitmapData.Scan0;
			byte* ptr = (byte*)(void*)scan;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num7 = i * stride + j * num6;
					num = ptr[num7 + 2];
					num2 = ptr[num7 + 1];
					num3 = ptr[num7];
					if (Math.Abs(num - num2) > num4 || Math.Abs(num - num3) > num4 || Math.Abs(num2 - num3) > num4)
					{
						array2[2] += num;
						array2[1] += num2;
						array2[0] += num3;
					}
					else
					{
						num5++;
					}
				}
			}
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			try
			{
				num8 = width * height - num5;
				num9 = (int)(array2[2] / num8);
				num10 = (int)(array2[1] / num8);
				num11 = (int)(array2[0] / num8);
			}
			catch
			{
				return new int[3];
			}
			return new int[3]
			{
				num9,
				num10,
				num11
			};
		}

		public static void ReInitialize()
		{
			ThreadPool.QueueUserWorkItem(ReInitializeCallback, 1);
		}

		public static void ReInitializeCallback(object threadContext)
		{
			Main.menuMode = 100;
			mainInstance.PublicInit(loadMods: true, 0, reloadPlayers: false);
			Main.menuMode = 0;
			Main.statusText = "";
		}

		public static void Rebuild(string modpack)
		{
			ThreadPool.QueueUserWorkItem(RebuildCallback, modpack);
		}

		public static void RebuildCallback(object m)
		{
			string text = (string)m;
			Main.statusText = "Rebuilding " + text;
			string objFile = Path.Combine(Constants.tConfigFolder, "ModPacks", text + ".obj");
			Main.statusTextSB = new StringBuilder(24000);
			StringWriter @out = new StringWriter(Main.statusTextSB);
			Console.SetOut(@out);
			Main.menuMode = 10000;
			Main.statusTextSB.AppendLine("Extracting archive...");
			ModPackBuilder modPackBuilder = new ModPackBuilder(objFile);
			if (modPackBuilder.IniToObj())
			{
				messages.Add(text + " has been rebuilt successfully");
			}
			else
			{
				messages.Add("Failed to rebuild " + text);
				string text2 = Main.statusTextSB.ToString();
				string[] collection = text2.Split('\n');
				List<string> list = new List<string>(collection);
				for (int num = Math.Min(list.Count, 4); num > 0; num--)
				{
					messages.Add("   " + list[list.Count - num]);
				}
				settings["ModPacks"][text] = "False";
				settings.Save(Path.Combine(tConfigFolder, "Config Mod.ini"));
			}
			Main.menuMode = 1600;
			Main.statusText = "";
		}

		public static string Get7Zip()
		{
			return Path.Combine(Directory.GetCurrentDirectory(), "7za.exe");
		}

		public static void PrintConsole(string print)
		{
			PrintConsole(print, Microsoft.Xna.Framework.Color.White);
		}

		public static void PrintConsole(string print, Microsoft.Xna.Framework.Color color)
		{
			while (console.Count >= 500)
			{
				console.RemoveAt(0);
				consoleColor.RemoveAt(0);
			}
			console.Add(print);
			consoleColor.Add(color);
		}

		public static void TrackConsole(string tag, string print, int ticks = 1)
		{
			TrackConsole(tag, print, Microsoft.Xna.Framework.Color.White, ticks);
		}

		public static void TrackConsole(string tag, string print, Microsoft.Xna.Framework.Color color, int ticks = 1)
		{
			if (!string.IsNullOrEmpty(tag))
			{
				for (int i = 0; i < consoleTrack.Count; i++)
				{
					TrackConsoleLine trackConsoleLine = consoleTrack[i];
					if (trackConsoleLine.tag == tag)
					{
						trackConsoleLine.print = print;
						trackConsoleLine.color = color;
						trackConsoleLine.ticks = ticks;
						return;
					}
				}
			}
			consoleTrack.Add(new TrackConsoleLine(tag, print, color, ticks));
		}

		public static void LoadModSettings(string modpack)
		{
			string text = tConfigFolder + "\\ModSettings";
			if (Directory.Exists(text))
			{
				string path = text + "\\" + modpack + ".json";
				if (File.Exists(path))
				{
					using (new MemoryStream())
					{
						using (StreamReader streamReader = new StreamReader(path))
						{
							JsonData jsonData = JsonMapper.ToObject(streamReader.ReadToEnd());
							JsonData jsonData2 = jsonCurrent[modpack]["settings"];
							foreach (KeyValuePair<string, JsonData> item in (IEnumerable)jsonData)
							{
								if (jsonData2.Has(item.Key))
								{
									jsonData2[item.Key]["value"] = item.Value;
								}
							}
						}
					}
				}
			}
		}

		public static void SaveModSettings(string modpack)
		{
			string text = tConfigFolder + "\\ModSettings";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = text + "\\" + modpack + ".json";
			if (jsonCurrent[modpack] != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				JsonWriter jsonWriter = new JsonWriter(stringBuilder);
				jsonWriter.WriteObjectStart();
				foreach (KeyValuePair<string, JsonData> item in (IEnumerable)jsonCurrent[modpack]["settings"])
				{
					jsonWriter.WritePropertyName(item.Key);
					JsonData jsonData = item.Value["value"];
					if (jsonData.IsBoolean)
					{
						jsonWriter.Write((bool)jsonData);
					}
					if (jsonData.IsInt)
					{
						jsonWriter.Write((int)jsonData);
					}
					if (jsonData.IsString)
					{
						jsonWriter.Write((string)jsonData);
					}
				}
				jsonWriter.WriteObjectEnd();
				File.WriteAllText(path, stringBuilder.ToString());
			}
		}
	}
}
