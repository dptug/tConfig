using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class NPC : Codable
	{
		public delegate void DamagePlayer_Del(Player player, ref int damage);

		public const int maxBuffs = 5;

		public Func<SpriteBatch, bool> PreDraw;

		public Action<SpriteBatch> PostDraw;

		public Func<string> ShopOverFuncName;

		public Func<string> KindChatFuncName;

		public Func<string> ChatFuncName;

		public Func<string> DominantChat;

		public Func<bool> ShopOverFunc;

		public Action<Chest> SetupShop;

		public Func<bool> KindChatFunc;

		public Action ChatFunc;

		public DamagePlayer_Del DamagePlayer;

		public static string[] NurseNames = new string[23]
		{
			"Molly",
			"Amy",
			"Claire",
			"Emily",
			"Katie",
			"Madeline",
			"Katelyn",
			"Emma",
			"Abigail",
			"Carly",
			"Jenna",
			"Heather",
			"Katherine",
			"Caitlin",
			"Kaitlin",
			"Holly",
			"Kaitlyn",
			"Hannah",
			"Kathryn",
			"Lorraine",
			"Helen",
			"Kayla",
			"Allison"
		};

		public static string[] MechanicNames = new string[24]
		{
			"Shayna",
			"Korrie",
			"Ginger",
			"Brooke",
			"Jenny",
			"Autumn",
			"Nancy",
			"Ella",
			"Kayla",
			"Beth",
			"Sophia",
			"Marshanna",
			"Lauren",
			"Trisha",
			"Shirlena",
			"Sheena",
			"Ellen",
			"Amy",
			"Dawn",
			"Susana",
			"Meredith",
			"Selene",
			"Terra",
			"Sally"
		};

		public static string[] DryadNames = new string[21]
		{
			"Alalia",
			"Alura",
			"Ariella",
			"Caelia",
			"Calista",
			"Chryseis",
			"Emerenta",
			"Elysia",
			"Evvie",
			"Faye",
			"Felicitae",
			"Lunette",
			"Nata",
			"Nissa",
			"Tatiana",
			"Rosalva",
			"Shea",
			"Tania",
			"Isis",
			"Celestia",
			"Xylia"
		};

		public static string[] GoblinTinkererNames = new string[25]
		{
			"Grodax",
			"Sarx",
			"Xon",
			"Mrunok",
			"Nuxatk",
			"Tgerd",
			"Darz",
			"Smador",
			"Stazen",
			"Mobart",
			"Knogs",
			"Tkanus",
			"Negurk",
			"Nort",
			"Durnok",
			"Trogem",
			"Stezom",
			"Gnudar",
			"Ragz",
			"Fahd",
			"Xanos",
			"Arback",
			"Fjell",
			"Dalek",
			"Knub"
		};

		public static string[] GuideNames = new string[35]
		{
			"Jake",
			"Connor",
			"Tanner",
			"Wyatt",
			"Cody",
			"Dustin",
			"Luke",
			"Jack",
			"Scott",
			"Logan",
			"Cole",
			"Lucas",
			"Bradley",
			"Jacob",
			"Garrett",
			"Dylan",
			"Maxwell",
			"Steve",
			"Brett",
			"Andrew",
			"Harley",
			"Kyle",
			"Jake",
			"Ryan",
			"Jeffrey",
			"Seth",
			"Marty",
			"Brandon",
			"Zach",
			"Jeff",
			"Daniel",
			"Trent",
			"Kevin",
			"Brian",
			"Colin"
		};

		public static string[] ArmsDealerNames = new string[23]
		{
			"DeShawn",
			"DeAndre",
			"Marquis",
			"Darnell",
			"Terrell",
			"Malik",
			"Trevon",
			"Tyrone",
			"Willie",
			"Dominique",
			"Demetrius",
			"Reginald",
			"Jamal",
			"Maurice",
			"Jalen",
			"Darius",
			"Xavier",
			"Terrance",
			"Andre",
			"Dante",
			"Brimst",
			"Bronson",
			"Darryl"
		};

		public static string[] ClothierNames = new string[24]
		{
			"Sebastian",
			"Rupert",
			"Clive",
			"Nigel",
			"Mervyn",
			"Cedric",
			"Pip",
			"Cyril",
			"Fitz",
			"Lloyd",
			"Arthur",
			"Rodney",
			"Graham",
			"Edward",
			"Alfred",
			"Edmund",
			"Henry",
			"Herald",
			"Roland",
			"Lincoln",
			"Lloyd",
			"Edgar",
			"Eustace",
			"Rodrick"
		};

		public static string[] DemolitionistNames = new string[22]
		{
			"Dolbere",
			"Bazdin",
			"Durim",
			"Tordak",
			"Garval",
			"Morthal",
			"Oten",
			"Dolgen",
			"Gimli",
			"Gimut",
			"Duerthen",
			"Beldin",
			"Jarut",
			"Ovbere",
			"Norkas",
			"Dolgrim",
			"Boften",
			"Norsun",
			"Dias",
			"Fikod",
			"Urist",
			"Darur"
		};

		public static string[] WizardNames = new string[21]
		{
			"Dalamar",
			"Dulais",
			"Elric",
			"Arddun",
			"Maelor",
			"Leomund",
			"Hirael",
			"Gwentor",
			"Greum",
			"Gearroid",
			"Fizban",
			"Ningauble",
			"Seonag",
			"Sargon",
			"Merlyn",
			"Magius",
			"Berwyn",
			"Arwyn",
			"Alasdair",
			"Tagar",
			"Xanadu"
		};

		public static string[] MerchantNames = new string[23]
		{
			"Alfred",
			"Barney",
			"Calvin",
			"Edmund",
			"Edwin",
			"Eugene",
			"Frank",
			"Frederick",
			"Gilbert",
			"Gus",
			"Wilbur",
			"Seymour",
			"Louis",
			"Humphrey",
			"Harold",
			"Milton",
			"Mortimer",
			"Howard",
			"Walter",
			"Finn",
			"Isacc",
			"Joseph",
			"Ralph"
		};

		public object[] buffCode = new object[5];

		public static int immuneTime = 20;

		public static int maxAI = 4;

		public int netSpam;

		public static int spawnSpaceX = 3;

		public static int spawnSpaceY = 3;

		public static int maxAttack = 20;

		public static int[] attackNPC = new int[maxAttack];

		public Vector2[] oldPos = new Vector2[10];

		public int netSkip;

		public bool netAlways;

		public int realLife = -1;

		public static int sWidth = 1920;

		public static int sHeight = 1080;

		public static int spawnRangeX = (int)((double)(sWidth / 16) * 0.7);

		public static int spawnRangeY = (int)((double)(sHeight / 16) * 0.7);

		public static int safeRangeX = (int)((double)(sWidth / 16) * 0.52);

		public static int safeRangeY = (int)((double)(sHeight / 16) * 0.52);

		public static int activeRangeX = (int)((double)sWidth * 1.7);

		public static int activeRangeY = (int)((double)sHeight * 1.7);

		public static int townRangeX = sWidth;

		public static int townRangeY = sHeight;

		public float npcSlots = 1f;

		public static bool noSpawnCycle = false;

		public static int activeTime = 750;

		public static int defaultSpawnRate = 600;

		public static int defaultMaxSpawns = 5;

		public int[] buffType = new int[5];

		public int[] buffTime = new int[5];

		public ArrayHandler<bool> buffImmune = new ArrayHandler<bool>(41);

		public bool onFire;

		public bool onFire2;

		public bool poisoned;

		public int lifeRegen;

		public int lifeRegenCount;

		public bool confused;

		public static bool downedBoss1 = false;

		public static bool downedBoss2 = false;

		public static bool downedBoss3 = false;

		public static bool savedGoblin = false;

		public static bool savedWizard = false;

		public static bool savedMech = false;

		public static bool downedGoblins = false;

		public static bool downedFrost = false;

		public static bool downedClown = false;

		public static int spawnRate = defaultSpawnRate;

		public static int maxSpawns = defaultMaxSpawns;

		public int soundDelay;

		public Vector2 oldPosition;

		public Vector2 oldVelocity;

		public int[] immune = new int[256];

		public int type;

		public float[] ai = new float[maxAI];

		public float[] localAI = new float[maxAI];

		public int aiAction;

		public int aiStyle;

		public bool justHit;

		public int timeLeft;

		public int target = -1;

		public int damage;

		public int defense;

		public int defDamage;

		public int defDefense;

		public int soundHit;

		public int soundKilled;

		public int life;

		public int lifeMax;

		public Rectangle targetRect;

		public double frameCounter;

		public Rectangle frame;

		public string displayName;

		public Color color;

		public int alpha;

		public float scale = 1f;

		public float knockBackResist = 1f;

		public int oldDirection;

		public int oldDirectionY;

		public int oldTarget;

		public int whoAmI;

		public float rotation;

		public bool noGravity;

		public bool noTileCollide;

		public bool netUpdate;

		public bool netUpdate2;

		public bool collideX;

		public bool collideY;

		public bool boss;

		public int spriteDirection = -1;

		public bool behindTiles;

		public bool lavaImmune;

		public float value;

		public bool dontTakeDamage;

		public int netID;

		public bool townNPC;

		public bool homeless;

		public int homeTileX = -1;

		public int homeTileY = -1;

		public bool oldHomeless;

		public int oldHomeTileX = -1;

		public int oldHomeTileY = -1;

		public bool friendly;

		public bool closeDoor;

		public int doorX;

		public int doorY;

		public int friendlyRegen;

		public float baseGravity;

		public float maxGravity;

		public bool dontDrawFace;

		public bool dontRelocate;

		public int music;

		public string musicName;

		public int CritChance;

		public float CritMult = 2f;

		public bool dontDrawLifeText;

		public static bool SpawnSky = false;

		public static bool SpawnInvasion = false;

		public static bool SpawnNearTown = false;

		public static bool SpawnWater = false;

		public static bool SpawnTownReduction = false;

		public override void ResetEvents()
		{
			PreDraw = null;
			PostDraw = null;
			ShopOverFuncName = null;
			KindChatFuncName = null;
			ShopOverFunc = null;
			SetupShop = null;
			KindChatFunc = null;
			ChatFunc = null;
			ChatFuncName = null;
			DamagePlayer = null;
		}

		public override void SetDefaultEvents()
		{
			if (PreDraw == null)
			{
				PreDraw = ((SpriteBatch s) => true);
			}
			if (PostDraw == null)
			{
				PostDraw = delegate
				{
				};
			}
			if (ShopOverFunc == null)
			{
				ShopOverFunc = (() => false);
			}
			if (KindChatFunc == null)
			{
				KindChatFunc = (() => true);
			}
		}

		public override void RegisterEvents(object code)
		{
			if (code != null)
			{
				Register(ref PreDraw, code, "PreDraw");
				Register(ref PostDraw, code, "PostDraw");
				Register(ref ShopOverFuncName, code, "ShopOverFuncName");
				Register(ref KindChatFuncName, code, "KindChatFuncName");
				Register(ref ShopOverFunc, code, "ShopOverFunc");
				Register(ref SetupShop, code, "SetupShop");
				Register(ref KindChatFunc, code, "KindChatFunc");
				Register(ref ChatFunc, code, "ChatFunc");
				Register(ref ChatFuncName, code, "ChatFuncName");
				Register(ref DamagePlayer, code, "DamagePlayer");
			}
		}

		public override string ToString()
		{
			return "NPC " + whoAmI + " " + name + " [" + displayName + "]";
		}

		public new void Init(int type = -1)
		{
			className = "NPC";
			base.Init(type);
		}

		public NPC()
		{
			className = "NPC";
			SetDefaultEvents();
		}

		public static void clrNames()
		{
			for (int i = 0; i < 147; i++)
			{
				Main.chrName[i] = "";
			}
		}

		public static void setNames()
		{
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random();
			}
			if (Main.chrName[17] == "")
			{
				Main.chrName[17] = MerchantNames[WorldGen.genRand.Next(MerchantNames.Length)];
			}
			if (Main.chrName[18] == "")
			{
				Main.chrName[18] = NurseNames[WorldGen.genRand.Next(NurseNames.Length)];
			}
			if (Main.chrName[19] == "")
			{
				Main.chrName[19] = ArmsDealerNames[WorldGen.genRand.Next(ArmsDealerNames.Length)];
			}
			if (Main.chrName[20] == "")
			{
				Main.chrName[20] = DryadNames[WorldGen.genRand.Next(DryadNames.Length)];
			}
			if (Main.chrName[22] == "")
			{
				Main.chrName[22] = GuideNames[WorldGen.genRand.Next(GuideNames.Length)];
			}
			if (Main.chrName[38] == "")
			{
				Main.chrName[38] = DemolitionistNames[WorldGen.genRand.Next(DemolitionistNames.Length)];
			}
			if (Main.chrName[54] == "")
			{
				Main.chrName[54] = ClothierNames[WorldGen.genRand.Next(ClothierNames.Length)];
			}
			if (Main.chrName[107] == "")
			{
				Main.chrName[107] = GoblinTinkererNames[WorldGen.genRand.Next(GoblinTinkererNames.Length)];
			}
			if (Main.chrName[108] == "")
			{
				Main.chrName[108] = WizardNames[WorldGen.genRand.Next(WizardNames.Length)];
			}
			if (Main.chrName[124] == "")
			{
				Main.chrName[124] = MechanicNames[WorldGen.genRand.Next(MechanicNames.Length)];
			}
		}

		public void netDefaults(int type)
		{
			if (type < 0)
			{
				switch (type)
				{
				case -1:
					SetDefaults("Slimeling");
					break;
				case -2:
					SetDefaults("Slimer2");
					break;
				case -3:
					SetDefaults("Green Slime");
					break;
				case -4:
					SetDefaults("Pinky");
					break;
				case -5:
					SetDefaults("Baby Slime");
					break;
				case -6:
					SetDefaults("Black Slime");
					break;
				case -7:
					SetDefaults("Purple Slime");
					break;
				case -8:
					SetDefaults("Red Slime");
					break;
				case -9:
					SetDefaults("Yellow Slime");
					break;
				case -10:
					SetDefaults("Jungle Slime");
					break;
				case -11:
					SetDefaults("Little Eater");
					break;
				case -12:
					SetDefaults("Big Eater");
					break;
				case -13:
					SetDefaults("Short Bones");
					break;
				case -14:
					SetDefaults("Big Boned");
					break;
				case -15:
					SetDefaults("Heavy Skeleton");
					break;
				case -16:
					SetDefaults("Little Stinger");
					break;
				case -17:
					SetDefaults("Big Stinger");
					break;
				}
			}
			else
			{
				SetDefaults(type);
			}
		}

		public void SetDefaults(string Name)
		{
			SetDefaults(0);
			if (Config.generateItemObj || Config.generateINI || !Config.initialized)
			{
				switch (Name)
				{
				case "Slimeling":
					SetDefaults(81, 0.6f);
					name = Name;
					damage = 45;
					defense = 10;
					life = 90;
					knockBackResist = 1.2f;
					value = 100f;
					netID = -1;
					break;
				case "Slimer2":
					SetDefaults(81, 0.9f);
					displayName = "Slimer";
					name = Name;
					damage = 45;
					defense = 20;
					life = 90;
					knockBackResist = 1.2f;
					value = 100f;
					netID = -2;
					break;
				case "Green Slime":
					SetDefaults(1, 0.9f);
					name = Name;
					damage = 6;
					defense = 0;
					life = 14;
					knockBackResist = 1.2f;
					color = new Color(0, 220, 40, 100);
					value = 3f;
					netID = -3;
					break;
				case "Pinky":
					SetDefaults(1, 0.6f);
					name = Name;
					damage = 5;
					defense = 5;
					life = 150;
					knockBackResist = 1.4f;
					color = new Color(250, 30, 90, 90);
					value = 10000f;
					netID = -4;
					break;
				case "Baby Slime":
					SetDefaults(1, 0.9f);
					name = Name;
					damage = 13;
					defense = 4;
					life = 30;
					knockBackResist = 0.95f;
					alpha = 120;
					color = new Color(0, 0, 0, 50);
					value = 10f;
					netID = -5;
					break;
				case "Black Slime":
					SetDefaults(1);
					name = Name;
					damage = 15;
					defense = 4;
					life = 45;
					color = new Color(0, 0, 0, 50);
					value = 20f;
					netID = -6;
					break;
				case "Purple Slime":
					SetDefaults(1, 1.2f);
					name = Name;
					damage = 12;
					defense = 6;
					life = 40;
					knockBackResist = 0.9f;
					color = new Color(200, 0, 255, 150);
					value = 10f;
					netID = -7;
					break;
				case "Red Slime":
					SetDefaults(1);
					name = Name;
					damage = 12;
					defense = 4;
					life = 35;
					color = new Color(255, 30, 0, 100);
					value = 8f;
					netID = -8;
					break;
				case "Yellow Slime":
					SetDefaults(1, 1.2f);
					name = Name;
					damage = 15;
					defense = 7;
					life = 45;
					color = new Color(255, 255, 0, 100);
					value = 10f;
					netID = -9;
					break;
				case "Jungle Slime":
					SetDefaults(1, 1.1f);
					name = Name;
					damage = 18;
					defense = 6;
					life = 60;
					color = new Color(143, 215, 93, 100);
					value = 500f;
					netID = -10;
					break;
				case "Little Eater":
					SetDefaults(6, 0.85f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((float)damage * scale);
					life = (int)((float)life * scale);
					value = (int)(value * scale);
					npcSlots *= scale;
					knockBackResist *= 2f - scale;
					netID = -11;
					break;
				case "Big Eater":
					SetDefaults(6, 1.15f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((float)damage * scale);
					life = (int)((float)life * scale);
					value = (int)(value * scale);
					npcSlots *= scale;
					knockBackResist *= 2f - scale;
					netID = -12;
					break;
				case "Short Bones":
					SetDefaults(31, 0.9f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((float)damage * scale);
					life = (int)((float)life * scale);
					value = (int)(value * scale);
					netID = -13;
					break;
				case "Big Boned":
					SetDefaults(31, 1.15f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((double)((float)damage * scale) * 1.1);
					life = (int)((double)((float)life * scale) * 1.1);
					value = (int)(value * scale);
					npcSlots = 2f;
					knockBackResist *= 2f - scale;
					netID = -14;
					break;
				case "Heavy Skeleton":
					SetDefaults(77, 1.15f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((double)((float)damage * scale) * 1.1);
					life = 400;
					value = (int)(value * scale);
					npcSlots = 2f;
					knockBackResist *= 2f - scale;
					height = 44;
					netID = -15;
					break;
				case "Little Stinger":
					SetDefaults(42, 0.85f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((float)damage * scale);
					life = (int)((float)life * scale);
					value = (int)(value * scale);
					npcSlots *= scale;
					knockBackResist *= 2f - scale;
					netID = -16;
					break;
				case "Big Stinger":
					SetDefaults(42, 1.2f);
					name = Name;
					defense = (int)((float)defense * scale);
					damage = (int)((float)damage * scale);
					life = (int)((float)life * scale);
					value = (int)(value * scale);
					npcSlots *= scale;
					knockBackResist *= 2f - scale;
					netID = -17;
					break;
				default:
					if (Name != "")
					{
						for (int i = 1; i < 147; i++)
						{
							if (Main.npcName[i] == Name)
							{
								SetDefaults(i);
								return;
							}
						}
						SetDefaults(0);
						active = false;
					}
					else
					{
						active = false;
					}
					break;
				}
				if (type != 0)
				{
					if (displayName == null || displayName == "")
					{
						displayName = Name;
					}
					lifeMax = life;
					defDamage = damage;
					defDefense = defense;
					if ((Config.generateINI || Config.generateItemObj) && Config.initialized)
					{
						Config.ProcessINI(this, "NPC", Name, "Defaults");
					}
				}
			}
			else
			{
				if (Name != null && Config.npcDefs.byName.TryGetValue(Name, out NPC original))
				{
					Config.CopyAttributes(this, original, "NPC");
					Init();
					active = true;
					defDamage = damage;
					defDefense = defense;
					life = lifeMax;
					RunMethod("Initialize");
				}
				else
				{
					Reset();
					if (!WorldGen.loadSuccess)
					{
						Config.CopyAttributes(this, Config.npcDefs.byName["Guide"], "NPC");
						type = Config.npcDefs.GetSize();
						displayName = "Unidentified Man";
						name = Name;
						if (!Main.dedServ)
						{
							Main.npcTexture[type] = Main.npcTexture[Config.npcDefs.byName["Guide"].type];
							Main.npcHeadTexture[type] = Main.npcHeadTexture[0];
						}
						Main.npcFrameCount[type] = Main.npcFrameCount[Config.npcDefs.byName["Guide"].type];
						Config.npcDefs[type] = this;
						Config.npcDefs.animationType[name] = Config.npcDefs.byName["Guide"].type;
						friendly = false;
						damage = 0;
						life = 0;
					}
				}
				if (Main.dedServ)
				{
					frame = default(Rectangle);
				}
				else
				{
					frame = new Rectangle(0, 0, Main.npcTexture[type].Width, Main.npcTexture[type].Height / Main.npcFrameCount[type]);
				}
			}
			if (displayName == null || displayName == "")
			{
				displayName = Name;
			}
			lifeMax = life;
			defDamage = damage;
			defDefense = defense;
		}

		public static bool MechSpawn(float x, float y, int type)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == type)
				{
					num++;
					Vector2 vector = new Vector2(x, y);
					float num4 = Main.npc[i].position.X - vector.X;
					float num5 = Main.npc[i].position.Y - vector.Y;
					float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
					if (num6 < 200f)
					{
						num2++;
					}
					if (num6 < 600f)
					{
						num3++;
					}
				}
			}
			if (num2 < 3 && num3 < 6)
			{
				return num < 10;
			}
			return false;
		}

		public static int TypeToNum(int type)
		{
			switch (type)
			{
			case 17:
				return 2;
			case 18:
				return 3;
			case 19:
				return 6;
			case 20:
				return 5;
			case 22:
				return 1;
			case 38:
				return 4;
			case 54:
				return 7;
			case 107:
				return 9;
			case 108:
				return 10;
			case 124:
				return 8;
			case 142:
				return 11;
			default:
				if (Config.npcDefs.townNPCList.Contains(type))
				{
					return type;
				}
				return -1;
			}
		}

		public static int NumToType(int type)
		{
			switch (type)
			{
			case 2:
				return 17;
			case 3:
				return 18;
			case 6:
				return 19;
			case 5:
				return 20;
			case 1:
				return 22;
			case 4:
				return 38;
			case 7:
				return 54;
			case 9:
				return 107;
			case 10:
				return 108;
			case 8:
				return 124;
			case 11:
				return 142;
			default:
				if (Config.npcDefs.townNPCList.Contains(type))
				{
					return type;
				}
				return -1;
			}
		}

		public void SetDefaults(int Type, float scaleOverride = -1f, bool saveData = false)
		{
			Reset();
			className = "NPC";
			netID = 0;
			netAlways = false;
			netSpam = 0;
			for (int i = 0; i < oldPos.Length; i++)
			{
				oldPos[i].X = 0f;
				oldPos[i].Y = 0f;
			}
			for (int j = 0; j < 5; j++)
			{
				buffTime[j] = 0;
				buffType[j] = 0;
			}
			for (int k = 0; k < 41; k++)
			{
				buffImmune[k] = false;
			}
			for (int l = 0; l < maxAI; l++)
			{
				ai[l] = 0f;
			}
			for (int m = 0; m < maxAI; m++)
			{
				localAI[m] = 0f;
			}
			buffImmune[31] = true;
			netSkip = -2;
			realLife = -1;
			lifeRegen = 0;
			lifeRegenCount = 0;
			poisoned = false;
			onFire = false;
			confused = false;
			onFire2 = false;
			justHit = false;
			dontTakeDamage = false;
			npcSlots = 1f;
			lavaImmune = false;
			lavaWet = false;
			wetCount = 0;
			wet = false;
			townNPC = false;
			homeless = false;
			homeTileX = -1;
			homeTileY = -1;
			friendly = false;
			behindTiles = false;
			boss = false;
			noTileCollide = false;
			rotation = 0f;
			active = true;
			alpha = 0;
			color = default(Color);
			collideX = false;
			collideY = false;
			direction = 0;
			oldDirection = direction;
			frameCounter = 0.0;
			netUpdate = true;
			netUpdate2 = false;
			knockBackResist = 1f;
			name = "";
			displayName = "";
			noGravity = false;
			scale = 1f;
			soundHit = 0;
			soundKilled = 0;
			spriteDirection = -1;
			target = 255;
			oldTarget = target;
			targetRect = default(Rectangle);
			timeLeft = activeTime;
			type = Type;
			value = 0f;
			dontDrawLifeText = false;
			if (Config.generateItemObj || Config.generateINI || !Config.initialized)
			{
				if (type == 1)
				{
					name = "Blue Slime";
					width = 24;
					height = 18;
					aiStyle = 1;
					damage = 7;
					defense = 2;
					lifeMax = 25;
					soundHit = 1;
					soundKilled = 1;
					alpha = 175;
					color = new Color(0, 80, 255, 100);
					value = 25f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 2)
				{
					name = "Demon Eye";
					width = 30;
					height = 32;
					aiStyle = 2;
					damage = 18;
					defense = 2;
					lifeMax = 60;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 75f;
					buffImmune[31] = false;
				}
				else if (type == 3)
				{
					name = "Zombie";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 14;
					defense = 6;
					lifeMax = 45;
					soundHit = 1;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 60f;
					buffImmune[31] = false;
				}
				else if (type == 4)
				{
					name = "Eye of Cthulhu";
					width = 100;
					height = 110;
					aiStyle = 4;
					damage = 15;
					defense = 12;
					lifeMax = 2800;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0f;
					noGravity = true;
					noTileCollide = true;
					timeLeft = activeTime * 30;
					boss = true;
					value = 30000f;
					npcSlots = 5f;
				}
				else if (type == 5)
				{
					name = "Servant of Cthulhu";
					width = 20;
					height = 20;
					aiStyle = 5;
					damage = 12;
					defense = 0;
					lifeMax = 8;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
				}
				else if (type == 6)
				{
					npcSlots = 1f;
					name = "Eater of Souls";
					width = 30;
					height = 30;
					aiStyle = 5;
					damage = 22;
					defense = 8;
					lifeMax = 40;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					knockBackResist = 0.5f;
					value = 90f;
				}
				else if (type == 7)
				{
					displayName = "Devourer";
					npcSlots = 3.5f;
					name = "Devourer Head";
					width = 22;
					height = 22;
					aiStyle = 6;
					damage = 31;
					defense = 2;
					lifeMax = 100;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 140f;
					netAlways = true;
				}
				else if (type == 8)
				{
					displayName = "Devourer";
					name = "Devourer Body";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 16;
					defense = 6;
					lifeMax = 100;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 140f;
				}
				else if (type == 9)
				{
					displayName = "Devourer";
					name = "Devourer Tail";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 13;
					defense = 10;
					lifeMax = 100;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 140f;
				}
				else if (type == 10)
				{
					displayName = "Giant Worm";
					name = "Giant Worm Head";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 8;
					defense = 0;
					lifeMax = 30;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 40f;
				}
				else if (type == 11)
				{
					displayName = "Giant Worm";
					name = "Giant Worm Body";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 4;
					defense = 4;
					lifeMax = 30;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 40f;
				}
				else if (type == 12)
				{
					displayName = "Giant Worm";
					name = "Giant Worm Tail";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 4;
					defense = 6;
					lifeMax = 30;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 40f;
				}
				else if (type == 13)
				{
					displayName = "Eater of Worlds";
					npcSlots = 5f;
					name = "Eater of Worlds Head";
					width = 38;
					height = 38;
					aiStyle = 6;
					netAlways = true;
					damage = 22;
					defense = 2;
					lifeMax = 65;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 300f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 14)
				{
					displayName = "Eater of Worlds";
					name = "Eater of Worlds Body";
					width = 38;
					height = 38;
					aiStyle = 6;
					netAlways = true;
					damage = 13;
					defense = 4;
					lifeMax = 150;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 300f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 15)
				{
					displayName = "Eater of Worlds";
					name = "Eater of Worlds Tail";
					width = 38;
					height = 38;
					aiStyle = 6;
					netAlways = true;
					damage = 11;
					defense = 8;
					lifeMax = 220;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 300f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 16)
				{
					npcSlots = 2f;
					name = "Mother Slime";
					width = 36;
					height = 24;
					aiStyle = 1;
					damage = 20;
					defense = 7;
					lifeMax = 90;
					soundHit = 1;
					soundKilled = 1;
					alpha = 120;
					color = new Color(0, 0, 0, 50);
					value = 75f;
					scale = 1.25f;
					knockBackResist = 0.6f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 17)
				{
					townNPC = true;
					friendly = true;
					name = "Merchant";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 18)
				{
					townNPC = true;
					friendly = true;
					name = "Nurse";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 19)
				{
					townNPC = true;
					friendly = true;
					name = "Arms Dealer";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 20)
				{
					townNPC = true;
					friendly = true;
					name = "Dryad";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 21)
				{
					name = "Skeleton";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 20;
					defense = 8;
					lifeMax = 60;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 100f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 22)
				{
					townNPC = true;
					friendly = true;
					name = "Guide";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 23)
				{
					name = "Meteor Head";
					width = 22;
					height = 22;
					aiStyle = 5;
					damage = 40;
					defense = 6;
					lifeMax = 26;
					soundHit = 3;
					soundKilled = 3;
					noGravity = true;
					noTileCollide = true;
					value = 80f;
					knockBackResist = 0.4f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 24)
				{
					npcSlots = 3f;
					name = "Fire Imp";
					width = 18;
					height = 40;
					aiStyle = 8;
					damage = 30;
					defense = 16;
					lifeMax = 70;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
					lavaImmune = true;
					value = 350f;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 25)
				{
					name = "Burning Sphere";
					width = 16;
					height = 16;
					aiStyle = 9;
					damage = 30;
					defense = 0;
					lifeMax = 1;
					soundHit = 3;
					soundKilled = 3;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					alpha = 100;
				}
				else if (type == 26)
				{
					name = "Goblin Peon";
					scale = 0.9f;
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 12;
					defense = 4;
					lifeMax = 60;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.8f;
					value = 100f;
					buffImmune[31] = false;
				}
				else if (type == 27)
				{
					name = "Goblin Thief";
					scale = 0.95f;
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 20;
					defense = 6;
					lifeMax = 80;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.7f;
					value = 200f;
					buffImmune[31] = false;
				}
				else if (type == 28)
				{
					name = "Goblin Warrior";
					scale = 1.1f;
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 25;
					defense = 8;
					lifeMax = 110;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
					value = 150f;
					buffImmune[31] = false;
				}
				else if (type == 29)
				{
					name = "Goblin Sorcerer";
					width = 18;
					height = 40;
					aiStyle = 8;
					damage = 20;
					defense = 2;
					lifeMax = 40;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.6f;
					value = 200f;
				}
				else if (type == 30)
				{
					name = "Chaos Ball";
					width = 16;
					height = 16;
					aiStyle = 9;
					damage = 20;
					defense = 0;
					lifeMax = 1;
					soundHit = 3;
					soundKilled = 3;
					noGravity = true;
					noTileCollide = true;
					alpha = 100;
					knockBackResist = 0f;
				}
				else if (type == 31)
				{
					name = "Angry Bones";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 26;
					defense = 8;
					lifeMax = 80;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.8f;
					value = 130f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 32)
				{
					name = "Dark Caster";
					width = 18;
					height = 40;
					aiStyle = 8;
					damage = 20;
					defense = 2;
					lifeMax = 50;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.6f;
					value = 140f;
					npcSlots = 2f;
					buffImmune[20] = true;
				}
				else if (type == 33)
				{
					name = "Water Sphere";
					width = 16;
					height = 16;
					aiStyle = 9;
					damage = 20;
					defense = 0;
					lifeMax = 1;
					soundHit = 3;
					soundKilled = 3;
					noGravity = true;
					noTileCollide = true;
					alpha = 100;
					knockBackResist = 0f;
				}
				else if (type == 34)
				{
					name = "Cursed Skull";
					width = 26;
					height = 28;
					aiStyle = 10;
					damage = 35;
					defense = 6;
					lifeMax = 40;
					soundHit = 2;
					soundKilled = 2;
					noGravity = true;
					noTileCollide = true;
					value = 150f;
					knockBackResist = 0.2f;
					npcSlots = 0.75f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 35)
				{
					displayName = "Skeletron";
					name = "Skeletron Head";
					width = 80;
					height = 102;
					aiStyle = 11;
					damage = 32;
					defense = 10;
					lifeMax = 4400;
					soundHit = 2;
					soundKilled = 2;
					noGravity = true;
					noTileCollide = true;
					value = 50000f;
					knockBackResist = 0f;
					boss = true;
					npcSlots = 6f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 36)
				{
					displayName = "Skeletron";
					name = "Skeletron Hand";
					width = 52;
					height = 52;
					aiStyle = 12;
					damage = 20;
					defense = 14;
					lifeMax = 600;
					soundHit = 2;
					soundKilled = 2;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 37)
				{
					townNPC = true;
					friendly = true;
					name = "Old Man";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 38)
				{
					townNPC = true;
					friendly = true;
					name = "Demolitionist";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 39)
				{
					npcSlots = 6f;
					name = "Bone Serpent Head";
					displayName = "Bone Serpent";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 30;
					defense = 10;
					lifeMax = 250;
					soundHit = 2;
					soundKilled = 5;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 1200f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 40)
				{
					name = "Bone Serpent Body";
					displayName = "Bone Serpent";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 15;
					defense = 12;
					lifeMax = 250;
					soundHit = 2;
					soundKilled = 5;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 1200f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 41)
				{
					name = "Bone Serpent Tail";
					displayName = "Bone Serpent";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 10;
					defense = 18;
					lifeMax = 250;
					soundHit = 2;
					soundKilled = 5;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 1200f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 42)
				{
					name = "Hornet";
					width = 34;
					height = 32;
					aiStyle = 5;
					damage = 34;
					defense = 12;
					lifeMax = 50;
					soundHit = 1;
					knockBackResist = 0.5f;
					soundKilled = 1;
					value = 200f;
					noGravity = true;
					buffImmune[20] = true;
				}
				else if (type == 43)
				{
					noGravity = true;
					noTileCollide = true;
					name = "Man Eater";
					width = 30;
					height = 30;
					aiStyle = 13;
					damage = 42;
					defense = 14;
					lifeMax = 130;
					soundHit = 1;
					knockBackResist = 0f;
					soundKilled = 1;
					value = 350f;
					buffImmune[20] = true;
				}
				else if (type == 44)
				{
					name = "Undead Miner";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 22;
					defense = 9;
					lifeMax = 70;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 250f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 45)
				{
					name = "Tim";
					width = 18;
					height = 40;
					aiStyle = 8;
					damage = 20;
					defense = 4;
					lifeMax = 200;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.6f;
					value = 5000f;
					buffImmune[20] = true;
				}
				else if (type == 46)
				{
					name = "Bunny";
					width = 18;
					height = 20;
					aiStyle = 7;
					damage = 0;
					defense = 0;
					lifeMax = 5;
					soundHit = 1;
					soundKilled = 1;
				}
				else if (type == 47)
				{
					name = "Corrupt Bunny";
					width = 18;
					height = 20;
					aiStyle = 3;
					damage = 20;
					defense = 4;
					lifeMax = 70;
					soundHit = 1;
					soundKilled = 1;
					value = 500f;
					buffImmune[31] = false;
				}
				else if (type == 48)
				{
					name = "Harpy";
					width = 24;
					height = 34;
					aiStyle = 14;
					damage = 25;
					defense = 8;
					lifeMax = 100;
					soundHit = 1;
					knockBackResist = 0.6f;
					soundKilled = 1;
					value = 300f;
				}
				else if (type == 49)
				{
					npcSlots = 0.5f;
					name = "Cave Bat";
					width = 22;
					height = 18;
					aiStyle = 14;
					damage = 13;
					defense = 2;
					lifeMax = 16;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 4;
					value = 90f;
					buffImmune[31] = false;
				}
				else if (type == 50)
				{
					boss = true;
					name = "King Slime";
					width = 98;
					height = 92;
					aiStyle = 15;
					damage = 40;
					defense = 10;
					lifeMax = 2000;
					knockBackResist = 0f;
					soundHit = 1;
					soundKilled = 1;
					alpha = 30;
					value = 10000f;
					scale = 1.25f;
					buffImmune[20] = true;
				}
				else if (type == 51)
				{
					npcSlots = 0.5f;
					name = "Jungle Bat";
					width = 22;
					height = 18;
					aiStyle = 14;
					damage = 20;
					defense = 4;
					lifeMax = 34;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 4;
					value = 80f;
					buffImmune[31] = false;
				}
				else if (type == 52)
				{
					name = "Doctor Bones";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 20;
					defense = 10;
					lifeMax = 500;
					soundHit = 1;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 1000f;
					buffImmune[31] = false;
				}
				else if (type == 53)
				{
					name = "The Groom";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 14;
					defense = 8;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 1000f;
					buffImmune[31] = false;
				}
				else if (type == 54)
				{
					townNPC = true;
					friendly = true;
					name = "Clothier";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 55)
				{
					noGravity = true;
					name = "Goldfish";
					width = 20;
					height = 18;
					aiStyle = 16;
					damage = 0;
					defense = 0;
					lifeMax = 5;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 56)
				{
					noTileCollide = true;
					noGravity = true;
					name = "Snatcher";
					width = 30;
					height = 30;
					aiStyle = 13;
					damage = 25;
					defense = 10;
					lifeMax = 60;
					soundHit = 1;
					knockBackResist = 0f;
					soundKilled = 1;
					value = 90f;
					buffImmune[20] = true;
				}
				else if (type == 57)
				{
					noGravity = true;
					name = "Corrupt Goldfish";
					width = 18;
					height = 20;
					aiStyle = 16;
					damage = 30;
					defense = 6;
					lifeMax = 100;
					soundHit = 1;
					soundKilled = 1;
					value = 500f;
				}
				else if (type == 58)
				{
					npcSlots = 0.5f;
					noGravity = true;
					name = "Piranha";
					width = 18;
					height = 20;
					aiStyle = 16;
					damage = 25;
					defense = 2;
					lifeMax = 30;
					soundHit = 1;
					soundKilled = 1;
					value = 50f;
				}
				else if (type == 59)
				{
					name = "Lava Slime";
					width = 24;
					height = 18;
					aiStyle = 1;
					damage = 15;
					defense = 10;
					lifeMax = 50;
					soundHit = 1;
					soundKilled = 1;
					scale = 1.1f;
					alpha = 50;
					lavaImmune = true;
					value = 120f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					buffImmune[31] = false;
				}
				else if (type == 60)
				{
					npcSlots = 0.5f;
					name = "Hellbat";
					width = 22;
					height = 18;
					aiStyle = 14;
					damage = 35;
					defense = 8;
					lifeMax = 46;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 4;
					value = 120f;
					scale = 1.1f;
					lavaImmune = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					buffImmune[31] = false;
				}
				else if (type == 61)
				{
					name = "Vulture";
					width = 36;
					height = 36;
					aiStyle = 17;
					damage = 15;
					defense = 4;
					lifeMax = 40;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 60f;
				}
				else if (type == 62)
				{
					npcSlots = 2f;
					name = "Demon";
					width = 28;
					height = 48;
					aiStyle = 14;
					damage = 32;
					defense = 8;
					lifeMax = 120;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 300f;
					lavaImmune = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 63)
				{
					noGravity = true;
					name = "Blue Jellyfish";
					width = 26;
					height = 26;
					aiStyle = 18;
					damage = 20;
					defense = 2;
					lifeMax = 30;
					soundHit = 1;
					soundKilled = 1;
					value = 100f;
					alpha = 20;
				}
				else if (type == 64)
				{
					noGravity = true;
					name = "Pink Jellyfish";
					width = 26;
					height = 26;
					aiStyle = 18;
					damage = 30;
					defense = 6;
					lifeMax = 70;
					soundHit = 1;
					soundKilled = 1;
					value = 100f;
					alpha = 20;
				}
				else if (type == 65)
				{
					noGravity = true;
					name = "Shark";
					width = 100;
					height = 24;
					aiStyle = 16;
					damage = 40;
					defense = 2;
					lifeMax = 300;
					soundHit = 1;
					soundKilled = 1;
					value = 400f;
					knockBackResist = 0.7f;
				}
				else if (type == 66)
				{
					npcSlots = 2f;
					name = "Voodoo Demon";
					width = 28;
					height = 48;
					aiStyle = 14;
					damage = 32;
					defense = 8;
					lifeMax = 140;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 1000f;
					lavaImmune = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 67)
				{
					name = "Crab";
					width = 28;
					height = 20;
					aiStyle = 3;
					damage = 20;
					defense = 10;
					lifeMax = 40;
					soundHit = 1;
					soundKilled = 1;
					value = 60f;
				}
				else if (type == 68)
				{
					name = "Dungeon Guardian";
					width = 80;
					height = 102;
					aiStyle = 11;
					damage = 9000;
					defense = 9000;
					lifeMax = 9999;
					soundHit = 2;
					soundKilled = 2;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 69)
				{
					name = "Antlion";
					width = 24;
					height = 24;
					aiStyle = 19;
					damage = 10;
					defense = 6;
					lifeMax = 45;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0f;
					value = 60f;
					behindTiles = true;
				}
				else if (type == 70)
				{
					npcSlots = 0.3f;
					name = "Spike Ball";
					width = 34;
					height = 34;
					aiStyle = 20;
					damage = 32;
					defense = 100;
					lifeMax = 100;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0f;
					noGravity = true;
					noTileCollide = true;
					dontTakeDamage = true;
					scale = 1.5f;
				}
				else if (type == 71)
				{
					npcSlots = 2f;
					name = "Dungeon Slime";
					width = 36;
					height = 24;
					aiStyle = 1;
					damage = 30;
					defense = 7;
					lifeMax = 150;
					soundHit = 1;
					soundKilled = 1;
					alpha = 60;
					value = 150f;
					scale = 1.25f;
					knockBackResist = 0.6f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 72)
				{
					npcSlots = 0.3f;
					name = "Blazing Wheel";
					width = 34;
					height = 34;
					aiStyle = 21;
					damage = 24;
					defense = 100;
					lifeMax = 100;
					alpha = 100;
					behindTiles = true;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0f;
					noGravity = true;
					dontTakeDamage = true;
					scale = 1.2f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 73)
				{
					name = "Goblin Scout";
					scale = 0.95f;
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 20;
					defense = 6;
					lifeMax = 80;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.7f;
					value = 200f;
					buffImmune[31] = false;
				}
				else if (type == 74)
				{
					name = "Bird";
					width = 14;
					height = 14;
					aiStyle = 24;
					damage = 0;
					defense = 0;
					lifeMax = 5;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
				}
				else if (type == 75)
				{
					noGravity = true;
					name = "Pixie";
					width = 20;
					height = 20;
					aiStyle = 22;
					damage = 55;
					defense = 20;
					lifeMax = 150;
					soundHit = 5;
					knockBackResist = 0.6f;
					soundKilled = 7;
					value = 350f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					buffImmune[31] = false;
				}
				else if (type == 77)
				{
					name = "Armored Skeleton";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 60;
					defense = 36;
					lifeMax = 340;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.4f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 78)
				{
					name = "Mummy";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 50;
					defense = 16;
					lifeMax = 130;
					soundHit = 1;
					soundKilled = 6;
					knockBackResist = 0.6f;
					value = 600f;
					buffImmune[31] = false;
				}
				else if (type == 79)
				{
					name = "Dark Mummy";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 60;
					defense = 18;
					lifeMax = 180;
					soundHit = 1;
					soundKilled = 6;
					knockBackResist = 0.5f;
					value = 700f;
					buffImmune[31] = false;
				}
				else if (type == 80)
				{
					name = "Light Mummy";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 55;
					defense = 18;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 6;
					knockBackResist = 0.55f;
					value = 700f;
					buffImmune[31] = false;
				}
				else if (type == 81)
				{
					name = "Corrupt Slime";
					width = 40;
					height = 30;
					aiStyle = 1;
					damage = 55;
					defense = 20;
					lifeMax = 170;
					soundHit = 1;
					soundKilled = 1;
					alpha = 55;
					value = 400f;
					scale = 1.1f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 82)
				{
					noGravity = true;
					noTileCollide = true;
					name = "Wraith";
					width = 24;
					height = 44;
					aiStyle = 22;
					damage = 75;
					defense = 18;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 6;
					alpha = 100;
					value = 500f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					knockBackResist = 0.7f;
				}
				else if (type == 83)
				{
					name = "Cursed Hammer";
					width = 40;
					height = 40;
					aiStyle = 23;
					damage = 80;
					defense = 18;
					lifeMax = 200;
					soundHit = 4;
					soundKilled = 6;
					value = 1000f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					knockBackResist = 0.4f;
				}
				else if (type == 84)
				{
					name = "Enchanted Sword";
					width = 40;
					height = 40;
					aiStyle = 23;
					damage = 80;
					defense = 18;
					lifeMax = 200;
					soundHit = 4;
					soundKilled = 6;
					value = 1000f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					knockBackResist = 0.4f;
				}
				else if (type == 85)
				{
					name = "Mimic";
					width = 24;
					height = 24;
					aiStyle = 25;
					damage = 80;
					defense = 30;
					lifeMax = 500;
					soundHit = 4;
					soundKilled = 6;
					value = 100000f;
					knockBackResist = 0.3f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 86)
				{
					name = "Unicorn";
					width = 46;
					height = 42;
					aiStyle = 26;
					damage = 65;
					defense = 30;
					lifeMax = 400;
					soundHit = 10;
					soundKilled = 1;
					knockBackResist = 0.3f;
					value = 1000f;
					buffImmune[31] = false;
				}
				else if (type == 87)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					npcSlots = 5f;
					name = "Wyvern Head";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 80;
					defense = 10;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 10000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 88)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					name = "Wyvern Legs";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 20;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 10000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 89)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					name = "Wyvern Body";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 20;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 2000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 90)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					name = "Wyvern Body 2";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 20;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 10000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 91)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					name = "Wyvern Body 3";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 20;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 10000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 92)
				{
					displayName = "Wyvern";
					noTileCollide = true;
					name = "Wyvern Tail";
					width = 32;
					height = 32;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 20;
					lifeMax = 4000;
					soundHit = 7;
					soundKilled = 8;
					noGravity = true;
					knockBackResist = 0f;
					value = 10000f;
					scale = 1f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 93)
				{
					npcSlots = 0.5f;
					name = "Giant Bat";
					width = 26;
					height = 20;
					aiStyle = 14;
					damage = 70;
					defense = 20;
					lifeMax = 160;
					soundHit = 1;
					knockBackResist = 0.75f;
					soundKilled = 4;
					value = 400f;
					buffImmune[31] = false;
				}
				else if (type == 94)
				{
					npcSlots = 1f;
					name = "Corruptor";
					width = 44;
					height = 44;
					aiStyle = 5;
					damage = 60;
					defense = 32;
					lifeMax = 230;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					knockBackResist = 0.55f;
					value = 500f;
				}
				else if (type == 95)
				{
					displayName = "Digger";
					name = "Digger Head";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 45;
					defense = 10;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					scale = 0.9f;
					value = 300f;
				}
				else if (type == 96)
				{
					displayName = "Digger";
					name = "Digger Body";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 28;
					defense = 20;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					scale = 0.9f;
					value = 300f;
				}
				else if (type == 97)
				{
					displayName = "Digger";
					name = "Digger Tail";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 26;
					defense = 30;
					lifeMax = 200;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					scale = 0.9f;
					value = 300f;
				}
				else if (type == 98)
				{
					displayName = "World Feeder";
					npcSlots = 3.5f;
					name = "Seeker Head";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 70;
					defense = 36;
					lifeMax = 500;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 700f;
				}
				else if (type == 99)
				{
					displayName = "World Feeder";
					name = "Seeker Body";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 55;
					defense = 40;
					lifeMax = 500;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 700f;
				}
				else if (type == 100)
				{
					displayName = "World Feeder";
					name = "Seeker Tail";
					width = 22;
					height = 22;
					aiStyle = 6;
					netAlways = true;
					damage = 40;
					defense = 44;
					lifeMax = 500;
					soundHit = 1;
					soundKilled = 1;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 700f;
				}
				else if (type == 101)
				{
					noGravity = true;
					noTileCollide = true;
					behindTiles = true;
					name = "Clinger";
					width = 30;
					height = 30;
					aiStyle = 13;
					damage = 70;
					defense = 30;
					lifeMax = 320;
					soundHit = 1;
					knockBackResist = 0.2f;
					soundKilled = 1;
					value = 600f;
				}
				else if (type == 102)
				{
					npcSlots = 0.5f;
					noGravity = true;
					name = "Angler Fish";
					width = 18;
					height = 20;
					aiStyle = 16;
					damage = 80;
					defense = 22;
					lifeMax = 90;
					soundHit = 1;
					soundKilled = 1;
					value = 500f;
				}
				else if (type == 103)
				{
					noGravity = true;
					name = "Green Jellyfish";
					width = 26;
					height = 26;
					aiStyle = 18;
					damage = 80;
					defense = 30;
					lifeMax = 120;
					soundHit = 1;
					soundKilled = 1;
					value = 800f;
					alpha = 20;
				}
				else if (type == 104)
				{
					name = "Werewolf";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 70;
					defense = 40;
					lifeMax = 400;
					soundHit = 6;
					soundKilled = 1;
					knockBackResist = 0.4f;
					value = 1000f;
					buffImmune[31] = false;
				}
				else if (type == 105)
				{
					friendly = true;
					name = "Bound Goblin";
					width = 18;
					height = 34;
					aiStyle = 0;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
					scale = 0.9f;
				}
				else if (type == 106)
				{
					friendly = true;
					name = "Bound Wizard";
					width = 18;
					height = 40;
					aiStyle = 0;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 107)
				{
					townNPC = true;
					friendly = true;
					name = "Goblin Tinkerer";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
					scale = 0.9f;
				}
				else if (type == 108)
				{
					townNPC = true;
					friendly = true;
					name = "Wizard";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 109)
				{
					name = "Clown";
					width = 34;
					height = 78;
					aiStyle = 3;
					damage = 50;
					defense = 20;
					lifeMax = 400;
					soundHit = 1;
					soundKilled = 2;
					knockBackResist = 0.4f;
					value = 8000f;
				}
				else if (type == 110)
				{
					name = "Skeleton Archer";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 55;
					defense = 28;
					lifeMax = 260;
					soundHit = 2;
					soundKilled = 2;
					knockBackResist = 0.55f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 111)
				{
					name = "Goblin Archer";
					scale = 0.95f;
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 20;
					defense = 6;
					lifeMax = 80;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.7f;
					value = 200f;
					buffImmune[31] = false;
				}
				else if (type == 112)
				{
					name = "Vile Spit";
					width = 16;
					height = 16;
					aiStyle = 9;
					damage = 65;
					defense = 0;
					lifeMax = 1;
					soundHit = 0;
					soundKilled = 9;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					scale = 0.9f;
					alpha = 80;
				}
				else if (type == 113)
				{
					npcSlots = 10f;
					name = "Wall of Flesh";
					width = 100;
					height = 100;
					aiStyle = 27;
					damage = 50;
					defense = 12;
					lifeMax = 8000;
					soundHit = 8;
					soundKilled = 10;
					noGravity = true;
					noTileCollide = true;
					behindTiles = true;
					knockBackResist = 0f;
					scale = 1.2f;
					boss = true;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					value = 80000f;
				}
				else if (type == 114)
				{
					name = "Wall of Flesh Eye";
					displayName = "Wall of Flesh";
					width = 100;
					height = 100;
					aiStyle = 28;
					damage = 50;
					defense = 0;
					lifeMax = 8000;
					soundHit = 8;
					soundKilled = 10;
					noGravity = true;
					noTileCollide = true;
					behindTiles = true;
					knockBackResist = 0f;
					scale = 1.2f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					value = 80000f;
				}
				else if (type == 115)
				{
					name = "The Hungry";
					width = 30;
					height = 30;
					aiStyle = 29;
					damage = 30;
					defense = 10;
					lifeMax = 240;
					soundHit = 9;
					soundKilled = 11;
					noGravity = true;
					behindTiles = true;
					noTileCollide = true;
					knockBackResist = 1.1f;
				}
				else if (type == 116)
				{
					name = "The Hungry II";
					displayName = "The Hungry";
					width = 30;
					height = 32;
					aiStyle = 2;
					damage = 30;
					defense = 6;
					lifeMax = 80;
					soundHit = 9;
					knockBackResist = 0.8f;
					soundKilled = 12;
				}
				else if (type == 117)
				{
					displayName = "Leech";
					name = "Leech Head";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 26;
					defense = 2;
					lifeMax = 60;
					soundHit = 9;
					soundKilled = 12;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
				}
				else if (type == 118)
				{
					displayName = "Leech";
					name = "Leech Body";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 22;
					defense = 6;
					lifeMax = 60;
					soundHit = 9;
					soundKilled = 12;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
				}
				else if (type == 119)
				{
					displayName = "Leech";
					name = "Leech Tail";
					width = 14;
					height = 14;
					aiStyle = 6;
					netAlways = true;
					damage = 18;
					defense = 10;
					lifeMax = 60;
					soundHit = 9;
					soundKilled = 12;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
				}
				else if (type == 120)
				{
					name = "Chaos Elemental";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 40;
					defense = 30;
					lifeMax = 370;
					soundHit = 1;
					soundKilled = 6;
					knockBackResist = 0.4f;
					value = 600f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					buffImmune[31] = false;
				}
				else if (type == 121)
				{
					name = "Slimer";
					width = 40;
					height = 30;
					aiStyle = 14;
					damage = 45;
					defense = 20;
					lifeMax = 60;
					soundHit = 1;
					alpha = 55;
					knockBackResist = 0.8f;
					scale = 1.1f;
					buffImmune[20] = true;
					buffImmune[31] = false;
				}
				else if (type == 122)
				{
					noGravity = true;
					name = "Gastropod";
					width = 20;
					height = 20;
					aiStyle = 22;
					damage = 60;
					defense = 22;
					lifeMax = 220;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 600f;
					buffImmune[20] = true;
				}
				else if (type == 123)
				{
					friendly = true;
					name = "Bound Mechanic";
					width = 18;
					height = 34;
					aiStyle = 0;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
					scale = 0.9f;
				}
				else if (type == 124)
				{
					townNPC = true;
					friendly = true;
					name = "Mechanic";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 125)
				{
					name = "Retinazer";
					width = 100;
					height = 110;
					aiStyle = 30;
					damage = 50;
					defense = 10;
					lifeMax = 24000;
					soundHit = 1;
					soundKilled = 14;
					knockBackResist = 0f;
					noGravity = true;
					noTileCollide = true;
					timeLeft = activeTime * 30;
					boss = true;
					value = 120000f;
					npcSlots = 5f;
					boss = true;
				}
				else if (type == 126)
				{
					name = "Spazmatism";
					width = 100;
					height = 110;
					aiStyle = 31;
					damage = 50;
					defense = 10;
					lifeMax = 24000;
					soundHit = 1;
					soundKilled = 14;
					knockBackResist = 0f;
					noGravity = true;
					noTileCollide = true;
					timeLeft = activeTime * 30;
					boss = true;
					value = 120000f;
					npcSlots = 5f;
					boss = true;
				}
				else if (type == 127)
				{
					name = "Skeletron Prime";
					width = 80;
					height = 102;
					aiStyle = 32;
					damage = 50;
					defense = 25;
					lifeMax = 30000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					value = 120000f;
					knockBackResist = 0f;
					boss = true;
					npcSlots = 6f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					boss = true;
				}
				else if (type == 128)
				{
					name = "Prime Cannon";
					width = 52;
					height = 52;
					aiStyle = 35;
					damage = 30;
					defense = 25;
					lifeMax = 7000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					netAlways = true;
				}
				else if (type == 129)
				{
					name = "Prime Saw";
					width = 52;
					height = 52;
					aiStyle = 33;
					damage = 52;
					defense = 40;
					lifeMax = 10000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					netAlways = true;
				}
				else if (type == 130)
				{
					name = "Prime Vice";
					width = 52;
					height = 52;
					aiStyle = 34;
					damage = 45;
					defense = 35;
					lifeMax = 10000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					netAlways = true;
				}
				else if (type == 131)
				{
					name = "Prime Laser";
					width = 52;
					height = 52;
					aiStyle = 36;
					damage = 29;
					defense = 20;
					lifeMax = 6000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					netAlways = true;
				}
				else if (type == 132)
				{
					displayName = "Zombie";
					name = "Bald Zombie";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 14;
					defense = 6;
					lifeMax = 45;
					soundHit = 1;
					soundKilled = 2;
					knockBackResist = 0.5f;
					value = 60f;
					buffImmune[31] = false;
				}
				else if (type == 133)
				{
					name = "Wandering Eye";
					width = 30;
					height = 32;
					aiStyle = 2;
					damage = 40;
					defense = 20;
					lifeMax = 300;
					soundHit = 1;
					knockBackResist = 0.8f;
					soundKilled = 1;
					value = 500f;
					buffImmune[31] = false;
				}
				else if (type == 134)
				{
					displayName = "The Destroyer";
					npcSlots = 5f;
					name = "The Destroyer";
					width = 38;
					height = 38;
					aiStyle = 37;
					damage = 60;
					defense = 0;
					lifeMax = 80000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					value = 120000f;
					scale = 1.25f;
					boss = true;
					netAlways = true;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 135)
				{
					displayName = "The Destroyer";
					npcSlots = 5f;
					name = "The Destroyer Body";
					width = 38;
					height = 38;
					aiStyle = 37;
					damage = 40;
					defense = 30;
					lifeMax = 80000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					netAlways = true;
					scale = 1.25f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 136)
				{
					displayName = "The Destroyer";
					npcSlots = 5f;
					name = "The Destroyer Tail";
					width = 38;
					height = 38;
					aiStyle = 37;
					damage = 20;
					defense = 35;
					lifeMax = 80000;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					noTileCollide = true;
					knockBackResist = 0f;
					behindTiles = true;
					scale = 1.25f;
					netAlways = true;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 137)
				{
					name = "Illuminant Bat";
					width = 26;
					height = 20;
					aiStyle = 14;
					damage = 75;
					defense = 30;
					lifeMax = 200;
					soundHit = 1;
					knockBackResist = 0.75f;
					soundKilled = 6;
					value = 500f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					buffImmune[31] = false;
				}
				else if (type == 138)
				{
					name = "Illuminant Slime";
					width = 24;
					height = 18;
					aiStyle = 1;
					damage = 70;
					defense = 30;
					lifeMax = 180;
					soundHit = 1;
					soundKilled = 6;
					alpha = 100;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[24] = true;
					buffImmune[39] = true;
					knockBackResist = 0.85f;
					scale = 1.05f;
					buffImmune[31] = false;
				}
				else if (type == 139)
				{
					npcSlots = 1f;
					name = "Probe";
					width = 30;
					height = 30;
					aiStyle = 5;
					damage = 50;
					defense = 20;
					lifeMax = 200;
					soundHit = 4;
					soundKilled = 14;
					noGravity = true;
					knockBackResist = 0.8f;
					noTileCollide = true;
				}
				else if (type == 140)
				{
					name = "Possessed Armor";
					width = 18;
					height = 40;
					aiStyle = 3;
					damage = 55;
					defense = 28;
					lifeMax = 260;
					soundHit = 4;
					soundKilled = 6;
					knockBackResist = 0.4f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
					buffImmune[24] = true;
				}
				else if (type == 141)
				{
					name = "Toxic Sludge";
					width = 34;
					height = 28;
					aiStyle = 1;
					damage = 50;
					defense = 18;
					lifeMax = 150;
					soundHit = 1;
					soundKilled = 1;
					alpha = 55;
					value = 400f;
					scale = 1.1f;
					buffImmune[20] = true;
					buffImmune[31] = false;
					knockBackResist = 0.8f;
				}
				else if (type == 142)
				{
					townNPC = true;
					friendly = true;
					name = "Santa Claus";
					width = 18;
					height = 40;
					aiStyle = 7;
					damage = 10;
					defense = 15;
					lifeMax = 250;
					soundHit = 1;
					soundKilled = 1;
					knockBackResist = 0.5f;
				}
				else if (type == 143)
				{
					name = "Snowman Gangsta";
					width = 26;
					height = 40;
					aiStyle = 38;
					damage = 50;
					defense = 20;
					lifeMax = 200;
					soundHit = 11;
					soundKilled = 15;
					knockBackResist = 0.6f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 144)
				{
					name = "Mister Stabby";
					width = 26;
					height = 40;
					aiStyle = 38;
					damage = 65;
					defense = 26;
					lifeMax = 240;
					soundHit = 11;
					soundKilled = 15;
					knockBackResist = 0.6f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				else if (type == 145)
				{
					name = "Snow Balla";
					width = 26;
					height = 40;
					aiStyle = 38;
					damage = 55;
					defense = 22;
					lifeMax = 220;
					soundHit = 11;
					soundKilled = 15;
					knockBackResist = 0.6f;
					value = 400f;
					buffImmune[20] = true;
					buffImmune[31] = false;
					buffImmune[24] = true;
					buffImmune[39] = true;
				}
				life = lifeMax;
				defDamage = damage;
				defDefense = defense;
				netID = type;
				if (saveData && type != 0 && (Config.generateINI || Config.generateItemObj))
				{
					Config.ProcessINI(this, "NPC", name, "Defaults");
				}
			}
			else if (Type > 0)
			{
				if (Config.npcDefs[Type] == null)
				{
					type = 0;
					active = false;
					return;
				}
				Config.CopyAttributes(this, Config.npcDefs[Type], "NPC");
				defDamage = damage;
				defDefense = defense;
				active = true;
				Init(type);
				RunMethod("Initialize");
			}
			else
			{
				Reset();
				buffImmune[31] = true;
				netSkip = -2;
				realLife = -1;
				lifeRegen = 0;
				lifeRegenCount = 0;
				poisoned = false;
				onFire = false;
				confused = false;
				onFire2 = false;
				justHit = false;
				dontTakeDamage = false;
				npcSlots = 1f;
				lavaImmune = false;
				lavaWet = false;
				wetCount = 0;
				wet = false;
				townNPC = false;
				homeless = false;
				homeTileX = -1;
				homeTileY = -1;
				friendly = false;
				behindTiles = false;
				boss = false;
				noTileCollide = false;
				rotation = 0f;
				active = true;
				alpha = 0;
				color = default(Color);
				collideX = false;
				collideY = false;
				direction = 0;
				oldDirection = direction;
				frameCounter = 0.0;
				netUpdate = true;
				netUpdate2 = false;
				knockBackResist = 1f;
				name = "";
				displayName = "";
				noGravity = false;
				scale = 1f;
				soundHit = 0;
				soundKilled = 0;
				spriteDirection = -1;
				target = 255;
				oldTarget = target;
				targetRect = default(Rectangle);
				timeLeft = activeTime;
				type = Type;
				value = 0f;
				for (int n = 0; n < maxAI; n++)
				{
					ai[n] = 0f;
				}
				for (int num = 0; num < maxAI; num++)
				{
					localAI[num] = 0f;
				}
			}
			if (Main.dedServ || Main.npcTexture[type] == null)
			{
				frame = default(Rectangle);
			}
			else
			{
				frame = new Rectangle(0, 0, Main.npcTexture[type].Width, Main.npcTexture[type].Height / Main.npcFrameCount[type]);
			}
			if (scaleOverride > 0f)
			{
				int num2 = (int)((float)width * scale);
				int num3 = (int)((float)height * scale);
				position.X += (float)(num2 / 2);
				position.Y += (float)num3;
				scale = scaleOverride;
				width = (int)((float)width * scale);
				height = (int)((float)height * scale);
				if (height == 16 || height == 32)
				{
					height++;
				}
				position.X -= (float)(width / 2);
				position.Y -= (float)height;
			}
			else
			{
				width = (int)((float)width * scale);
				height = (int)((float)height * scale);
			}
			life = lifeMax;
			defDamage = damage;
			defDefense = defense;
			netID = type;
		}

		public void AI(bool ignoreCustomCode = false)
		{
			int num = type;
			int num2 = type;
			if (name != null && Config.npcDefs.aiPretendType.TryGetValue(name, out num2))
			{
				type = num2;
			}
			AIReal(ignoreCustomCode);
			if (type == num2)
			{
				type = num;
			}
		}

		public void AIReal(bool ignoreCustomCode = false)
		{
			if (!ignoreCustomCode && RunMethod("AI"))
			{
				return;
			}
			if (aiStyle == 0)
			{
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].talkNPC == whoAmI)
					{
						if (type == 105)
						{
							Transform(107);
							return;
						}
						if (type == 106)
						{
							Transform(108);
							return;
						}
						if (type == 123)
						{
							Transform(124);
							return;
						}
					}
				}
				velocity.X *= 0.93f;
				if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
				{
					velocity.X = 0f;
				}
				TargetClosest();
				spriteDirection = direction;
			}
			else if (aiStyle == 1)
			{
				bool flag = false;
				if (!Main.dayTime || life != lifeMax || (double)base.position.Y > Main.worldSurface * 16.0)
				{
					flag = true;
				}
				if (type == 81)
				{
					flag = true;
					if (Config.syncedRand.Next(30) == 0)
					{
						int num = Dust.NewDust(base.position, base.width, base.height, 14, 0f, 0f, alpha, color);
						Dust dust = Main.dust[num];
						dust.velocity *= 0.3f;
					}
				}
				if (type == 59)
				{
					Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 1f, 0.3f, 0.1f);
					Vector2 position = new Vector2(base.position.X, base.position.Y);
					int width = base.width;
					int height = base.height;
					int num2 = 6;
					float speedX = velocity.X * 0.2f;
					float speedY = velocity.Y * 0.2f;
					int num3 = 100;
					int num4 = Dust.NewDust(position, width, height, num2, speedX, speedY, num3, default(Color), 1.7f);
					Main.dust[num4].noGravity = true;
				}
				if (ai[2] > 1f)
				{
					ai[2] -= 1f;
				}
				if (wet)
				{
					if (collideY)
					{
						velocity.Y = -2f;
					}
					if (velocity.Y < 0f && ai[3] == base.position.X)
					{
						direction *= -1;
						ai[2] = 200f;
					}
					if (velocity.Y > 0f)
					{
						ai[3] = base.position.X;
					}
					if (type == 59)
					{
						if (velocity.Y > 2f)
						{
							velocity.Y *= 0.9f;
						}
						else if (base.directionY < 0)
						{
							velocity.Y -= 0.8f;
						}
						velocity.Y -= 0.5f;
						if (velocity.Y < -10f)
						{
							velocity.Y = -10f;
						}
					}
					else
					{
						if (velocity.Y > 2f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y -= 0.5f;
						if (velocity.Y < -4f)
						{
							velocity.Y = -4f;
						}
					}
					if (ai[2] == 1f && flag)
					{
						TargetClosest();
					}
				}
				aiAction = 0;
				if (ai[2] == 0f)
				{
					ai[0] = -100f;
					ai[2] = 1f;
					TargetClosest();
				}
				if (velocity.Y == 0f)
				{
					if (ai[3] == base.position.X)
					{
						direction *= -1;
						ai[2] = 200f;
					}
					ai[3] = 0f;
					velocity.X *= 0.8f;
					if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
					{
						velocity.X = 0f;
					}
					if (flag)
					{
						ai[0] += 1f;
					}
					ai[0] += 1f;
					if (type == 59)
					{
						ai[0] += 2f;
					}
					if (type == 71)
					{
						ai[0] += 3f;
					}
					if (type == 138)
					{
						ai[0] += 2f;
					}
					if (type == 81)
					{
						if (scale >= 0f)
						{
							ai[0] += 4f;
						}
						else
						{
							ai[0] += 1f;
						}
					}
					if (ai[0] >= 0f)
					{
						netUpdate = true;
						if (flag && ai[2] == 1f)
						{
							TargetClosest();
						}
						if (ai[1] == 2f)
						{
							velocity.Y = -8f;
							if (type == 59)
							{
								velocity.Y -= 2f;
							}
							velocity.X += (float)(3 * direction);
							if (type == 59)
							{
								velocity.X += 0.5f * (float)direction;
							}
							ai[0] = -200f;
							ai[1] = 0f;
							ai[3] = base.position.X;
						}
						else
						{
							velocity.Y = -6f;
							velocity.X += (float)(2 * direction);
							if (type == 59)
							{
								velocity.X += (float)(2 * direction);
							}
							ai[0] = -120f;
							ai[1] += 1f;
						}
						if (type == 141)
						{
							velocity.Y *= 1.3f;
							velocity.X *= 1.2f;
						}
					}
					else if (ai[0] >= -30f)
					{
						aiAction = 1;
					}
				}
				else if (target < 255 && ((direction == 1 && velocity.X < 3f) || (direction == -1 && velocity.X > -3f)))
				{
					if ((direction == -1 && (double)velocity.X < 0.1) || (direction == 1 && (double)velocity.X > -0.1))
					{
						velocity.X += 0.2f * (float)direction;
					}
					else
					{
						velocity.X *= 0.93f;
					}
				}
			}
			else if (aiStyle == 2)
			{
				noGravity = true;
				if (collideX)
				{
					velocity.X = oldVelocity.X * -0.5f;
					if (direction == -1 && velocity.X > 0f && velocity.X < 2f)
					{
						velocity.X = 2f;
					}
					if (direction == 1 && velocity.X < 0f && velocity.X > -2f)
					{
						velocity.X = -2f;
					}
				}
				if (collideY)
				{
					velocity.Y = oldVelocity.Y * -0.5f;
					if (velocity.Y > 0f && velocity.Y < 1f)
					{
						velocity.Y = 1f;
					}
					if (velocity.Y < 0f && velocity.Y > -1f)
					{
						velocity.Y = -1f;
					}
				}
				if (Main.dayTime && (double)base.position.Y <= Main.worldSurface * 16.0 && (type == 2 || type == 133))
				{
					if (timeLeft > 10)
					{
						timeLeft = 10;
					}
					base.directionY = -1;
					if (velocity.Y > 0f)
					{
						direction = 1;
					}
					direction = -1;
					if (velocity.X > 0f)
					{
						direction = 1;
					}
				}
				else
				{
					TargetClosest();
				}
				if (type == 116)
				{
					TargetClosest();
					Lighting.addLight((int)(base.position.X + (float)(base.width / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.3f, 0.2f, 0.1f);
					if (direction == -1 && velocity.X > -6f)
					{
						velocity.X -= 0.1f;
						if (velocity.X > 6f)
						{
							velocity.X -= 0.1f;
						}
						else if (velocity.X > 0f)
						{
							velocity.X -= 0.2f;
						}
						if (velocity.X < -6f)
						{
							velocity.X = -6f;
						}
					}
					else if (direction == 1 && velocity.X < 6f)
					{
						velocity.X += 0.1f;
						if (velocity.X < -6f)
						{
							velocity.X += 0.1f;
						}
						else if (velocity.X < 0f)
						{
							velocity.X += 0.2f;
						}
						if (velocity.X > 6f)
						{
							velocity.X = 6f;
						}
					}
					if (base.directionY == -1 && (double)velocity.Y > -2.5)
					{
						velocity.Y -= 0.04f;
						if ((double)velocity.Y > 2.5)
						{
							velocity.Y -= 0.05f;
						}
						else if (velocity.Y > 0f)
						{
							velocity.Y -= 0.15f;
						}
						if ((double)velocity.Y < -2.5)
						{
							velocity.Y = -2.5f;
						}
					}
					else if (base.directionY == 1 && (double)velocity.Y < 1.5)
					{
						velocity.Y += 0.04f;
						if ((double)velocity.Y < -2.5)
						{
							velocity.Y += 0.05f;
						}
						else if (velocity.Y < 0f)
						{
							velocity.Y += 0.15f;
						}
						if ((double)velocity.Y > 2.5)
						{
							velocity.Y = 2.5f;
						}
					}
					if (Config.syncedRand.Next(40) == 0)
					{
						Vector2 position2 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
						int width2 = base.width;
						int height2 = (int)((float)base.height * 0.5f);
						int num5 = 5;
						float x = velocity.X;
						float speedY2 = 2f;
						int num6 = 0;
						int num7 = Dust.NewDust(position2, width2, height2, num5, x, speedY2, num6);
						Dust dust2 = Main.dust[num7];
						dust2.velocity.X = dust2.velocity.X * 0.5f;
						Dust dust3 = Main.dust[num7];
						dust3.velocity.Y = dust3.velocity.Y * 0.1f;
					}
				}
				else if (type == 133)
				{
					if ((double)life < (double)lifeMax * 0.5)
					{
						if (direction == -1 && velocity.X > -6f)
						{
							velocity.X -= 0.1f;
							if (velocity.X > 6f)
							{
								velocity.X -= 0.1f;
							}
							else if (velocity.X > 0f)
							{
								velocity.X += 0.05f;
							}
							if (velocity.X < -6f)
							{
								velocity.X = -6f;
							}
						}
						else if (direction == 1 && velocity.X < 6f)
						{
							velocity.X += 0.1f;
							if (velocity.X < -6f)
							{
								velocity.X += 0.1f;
							}
							else if (velocity.X < 0f)
							{
								velocity.X -= 0.05f;
							}
							if (velocity.X > 6f)
							{
								velocity.X = 6f;
							}
						}
						if (base.directionY == -1 && velocity.Y > -4f)
						{
							velocity.Y -= 0.1f;
							if (velocity.Y > 4f)
							{
								velocity.Y -= 0.1f;
							}
							else if (velocity.Y > 0f)
							{
								velocity.Y += 0.05f;
							}
							if (velocity.Y < -4f)
							{
								velocity.Y = -4f;
							}
						}
						else if (base.directionY == 1 && velocity.Y < 4f)
						{
							velocity.Y += 0.1f;
							if (velocity.Y < -4f)
							{
								velocity.Y += 0.1f;
							}
							else if (velocity.Y < 0f)
							{
								velocity.Y -= 0.05f;
							}
							if (velocity.Y > 4f)
							{
								velocity.Y = 4f;
							}
						}
					}
					else
					{
						if (direction == -1 && velocity.X > -4f)
						{
							velocity.X -= 0.1f;
							if (velocity.X > 4f)
							{
								velocity.X -= 0.1f;
							}
							else if (velocity.X > 0f)
							{
								velocity.X += 0.05f;
							}
							if (velocity.X < -4f)
							{
								velocity.X = -4f;
							}
						}
						else if (direction == 1 && velocity.X < 4f)
						{
							velocity.X += 0.1f;
							if (velocity.X < -4f)
							{
								velocity.X += 0.1f;
							}
							else if (velocity.X < 0f)
							{
								velocity.X -= 0.05f;
							}
							if (velocity.X > 4f)
							{
								velocity.X = 4f;
							}
						}
						if (base.directionY == -1 && (double)velocity.Y > -1.5)
						{
							velocity.Y -= 0.04f;
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y -= 0.05f;
							}
							else if (velocity.Y > 0f)
							{
								velocity.Y += 0.03f;
							}
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y = -1.5f;
							}
						}
						else if (base.directionY == 1 && (double)velocity.Y < 1.5)
						{
							velocity.Y += 0.04f;
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y += 0.05f;
							}
							else if (velocity.Y < 0f)
							{
								velocity.Y -= 0.03f;
							}
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y = 1.5f;
							}
						}
					}
				}
				else
				{
					if (direction == -1 && velocity.X > -4f)
					{
						velocity.X -= 0.1f;
						if (velocity.X > 4f)
						{
							velocity.X -= 0.1f;
						}
						else if (velocity.X > 0f)
						{
							velocity.X += 0.05f;
						}
						if (velocity.X < -4f)
						{
							velocity.X = -4f;
						}
					}
					else if (direction == 1 && velocity.X < 4f)
					{
						velocity.X += 0.1f;
						if (velocity.X < -4f)
						{
							velocity.X += 0.1f;
						}
						else if (velocity.X < 0f)
						{
							velocity.X -= 0.05f;
						}
						if (velocity.X > 4f)
						{
							velocity.X = 4f;
						}
					}
					if (base.directionY == -1 && (double)velocity.Y > -1.5)
					{
						velocity.Y -= 0.04f;
						if ((double)velocity.Y > 1.5)
						{
							velocity.Y -= 0.05f;
						}
						else if (velocity.Y > 0f)
						{
							velocity.Y += 0.03f;
						}
						if ((double)velocity.Y < -1.5)
						{
							velocity.Y = -1.5f;
						}
					}
					else if (base.directionY == 1 && (double)velocity.Y < 1.5)
					{
						velocity.Y += 0.04f;
						if ((double)velocity.Y < -1.5)
						{
							velocity.Y += 0.05f;
						}
						else if (velocity.Y < 0f)
						{
							velocity.Y -= 0.03f;
						}
						if ((double)velocity.Y > 1.5)
						{
							velocity.Y = 1.5f;
						}
					}
				}
				if ((type == 2 || type == 133) && Config.syncedRand.Next(40) == 0)
				{
					Vector2 position3 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
					int width3 = base.width;
					int height3 = (int)((float)base.height * 0.5f);
					int num8 = 5;
					float x2 = velocity.X;
					float speedY3 = 2f;
					int num9 = 0;
					int num10 = Dust.NewDust(position3, width3, height3, num8, x2, speedY3, num9);
					Dust dust4 = Main.dust[num10];
					dust4.velocity.X = dust4.velocity.X * 0.5f;
					Dust dust5 = Main.dust[num10];
					dust5.velocity.Y = dust5.velocity.Y * 0.1f;
				}
				if (wet)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.Y -= 0.5f;
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
					TargetClosest();
				}
			}
			else if (aiStyle == 3)
			{
				int num11 = 60;
				if (type == 120)
				{
					num11 = 20;
					if (ai[3] == -120f)
					{
						velocity *= 0f;
						ai[3] = 0f;
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
						Vector2 vector = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num12 = oldPos[2].X + (float)base.width * 0.5f - vector.X;
						float num13 = oldPos[2].Y + (float)base.height * 0.5f - vector.Y;
						float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
						num14 = 2f / num14;
						num12 *= num14;
						num13 *= num14;
						for (int j = 0; j < 20; j++)
						{
							Vector2 position4 = base.position;
							int width4 = base.width;
							int height4 = base.height;
							int num15 = 71;
							float speedX2 = num12;
							float speedY4 = num13;
							int num16 = 200;
							int num17 = Dust.NewDust(position4, width4, height4, num15, speedX2, speedY4, num16, default(Color), 2f);
							Main.dust[num17].noGravity = true;
							Dust dust6 = Main.dust[num17];
							dust6.velocity.X = dust6.velocity.X * 2f;
						}
						for (int k = 0; k < 20; k++)
						{
							Vector2 position5 = oldPos[2];
							int width5 = base.width;
							int height5 = base.height;
							int num18 = 71;
							float speedX3 = 0f - num12;
							float speedY5 = 0f - num13;
							int num19 = 200;
							int num20 = Dust.NewDust(position5, width5, height5, num18, speedX3, speedY5, num19, default(Color), 2f);
							Main.dust[num20].noGravity = true;
							Dust dust7 = Main.dust[num20];
							dust7.velocity.X = dust7.velocity.X * 2f;
						}
					}
				}
				bool flag2 = false;
				bool flag3 = true;
				if (type == 47 || type == 67 || type == 109 || type == 110 || type == 111 || type == 120)
				{
					flag3 = false;
				}
				if ((type != 110 && type != 111) || ai[2] <= 0f)
				{
					if (velocity.Y == 0f && ((velocity.X > 0f && direction < 0) || (velocity.X < 0f && direction > 0)))
					{
						flag2 = true;
					}
					if (base.position.X == oldPosition.X || ai[3] >= (float)num11 || flag2)
					{
						ai[3] += 1f;
					}
					else if ((double)Math.Abs(velocity.X) > 0.9 && ai[3] > 0f)
					{
						ai[3] -= 1f;
					}
					if (ai[3] > (float)(num11 * 10))
					{
						ai[3] = 0f;
					}
					if (justHit)
					{
						ai[3] = 0f;
					}
					if (ai[3] == (float)num11)
					{
						netUpdate = true;
					}
				}
				if ((!Main.dayTime || (double)base.position.Y > Main.worldSurface * 16.0 || type == 26 || type == 27 || type == 28 || type == 31 || type == 47 || type == 67 || type == 73 || type == 77 || type == 78 || type == 79 || type == 80 || type == 110 || type == 111 || type == 120) && ai[3] < (float)num11)
				{
					if ((type == 3 || type == 21 || type == 31 || type == 77 || type == 110 || type == 132) && Config.syncedRand.Next(1000) == 0)
					{
						Main.PlaySound(14, (int)base.position.X, (int)base.position.Y);
					}
					if ((type == 78 || type == 79 || type == 80) && Config.syncedRand.Next(500) == 0)
					{
						Main.PlaySound(26, (int)base.position.X, (int)base.position.Y);
					}
					TargetClosest();
				}
				else if ((type != 110 && type != 111) || ai[2] <= 0f)
				{
					if (Main.dayTime && (double)(base.position.Y / 16f) < Main.worldSurface && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (velocity.X == 0f)
					{
						if (velocity.Y == 0f)
						{
							ai[0] += 1f;
							if (ai[0] >= 2f)
							{
								direction *= -1;
								spriteDirection = direction;
								ai[0] = 0f;
							}
						}
					}
					else
					{
						ai[0] = 0f;
					}
					if (direction == 0)
					{
						direction = 1;
					}
				}
				if (type == 120)
				{
					if (velocity.X < -3f || velocity.X > 3f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < 3f && direction == 1)
					{
						if (velocity.Y == 0f && velocity.X < 0f)
						{
							velocity.X *= 0.99f;
						}
						velocity.X += 0.07f;
						if (velocity.X > 3f)
						{
							velocity.X = 3f;
						}
					}
					else if (velocity.X > -3f && direction == -1)
					{
						if (velocity.Y == 0f && velocity.X > 0f)
						{
							velocity.X *= 0.99f;
						}
						velocity.X -= 0.07f;
						if (velocity.X < -3f)
						{
							velocity.X = -3f;
						}
					}
				}
				else if (type == 27 || type == 77 || type == 104)
				{
					if (velocity.X < -2f || velocity.X > 2f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < 2f && direction == 1)
					{
						velocity.X += 0.07f;
						if (velocity.X > 2f)
						{
							velocity.X = 2f;
						}
					}
					else if (velocity.X > -2f && direction == -1)
					{
						velocity.X -= 0.07f;
						if (velocity.X < -2f)
						{
							velocity.X = -2f;
						}
					}
				}
				else if (type == 109)
				{
					if (velocity.X < -2f || velocity.X > 2f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < 2f && direction == 1)
					{
						velocity.X += 0.04f;
						if (velocity.X > 2f)
						{
							velocity.X = 2f;
						}
					}
					else if (velocity.X > -2f && direction == -1)
					{
						velocity.X -= 0.04f;
						if (velocity.X < -2f)
						{
							velocity.X = -2f;
						}
					}
				}
				else if (type == 21 || type == 26 || type == 31 || type == 47 || type == 73 || type == 140)
				{
					if (velocity.X < -1.5f || velocity.X > 1.5f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < 1.5f && direction == 1)
					{
						velocity.X += 0.07f;
						if (velocity.X > 1.5f)
						{
							velocity.X = 1.5f;
						}
					}
					else if (velocity.X > -1.5f && direction == -1)
					{
						velocity.X -= 0.07f;
						if (velocity.X < -1.5f)
						{
							velocity.X = -1.5f;
						}
					}
				}
				else if (type == 67)
				{
					if (velocity.X < -0.5f || velocity.X > 0.5f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.7f;
						}
					}
					else if (velocity.X < 0.5f && direction == 1)
					{
						velocity.X += 0.03f;
						if (velocity.X > 0.5f)
						{
							velocity.X = 0.5f;
						}
					}
					else if (velocity.X > -0.5f && direction == -1)
					{
						velocity.X -= 0.03f;
						if (velocity.X < -0.5f)
						{
							velocity.X = -0.5f;
						}
					}
				}
				else if (type == 78 || type == 79 || type == 80)
				{
					float num21 = 1f;
					float num22 = 0.05f;
					if (life < lifeMax / 2)
					{
						num21 = 2f;
						num22 = 0.1f;
					}
					if (type == 79)
					{
						num21 *= 1.5f;
					}
					if (velocity.X < 0f - num21 || velocity.X > num21)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.7f;
						}
					}
					else if (velocity.X < num21 && direction == 1)
					{
						velocity.X += num22;
						if (velocity.X > num21)
						{
							velocity.X = num21;
						}
					}
					else if (velocity.X > 0f - num21 && direction == -1)
					{
						velocity.X -= num22;
						if (velocity.X < 0f - num21)
						{
							velocity.X = 0f - num21;
						}
					}
				}
				else if (type != 110 && type != 111)
				{
					if (velocity.X < -1f || velocity.X > 1f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < 1f && direction == 1)
					{
						velocity.X += 0.07f;
						if (velocity.X > 1f)
						{
							velocity.X = 1f;
						}
					}
					else if (velocity.X > -1f && direction == -1)
					{
						velocity.X -= 0.07f;
						if (velocity.X < -1f)
						{
							velocity.X = -1f;
						}
					}
				}
				if (type == 110 || type == 111)
				{
					if (confused)
					{
						ai[2] = 0f;
					}
					else
					{
						if (ai[1] > 0f)
						{
							ai[1] -= 1f;
						}
						if (justHit)
						{
							ai[1] = 30f;
							ai[2] = 0f;
						}
						int num23 = 70;
						if (type == 111)
						{
							num23 = 180;
						}
						if (ai[2] > 0f)
						{
							TargetClosest();
							if (ai[1] == (float)(num23 / 2))
							{
								float num24 = 11f;
								if (type == 111)
								{
									num24 = 9f;
								}
								Vector2 vector2 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
								float num25 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector2.X;
								float num26 = Math.Abs(num25) * 0.1f;
								float num27 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector2.Y - num26;
								num25 += (float)Config.syncedRand.Next(-40, 41);
								num27 += (float)Config.syncedRand.Next(-40, 41);
								float num28 = (float)Math.Sqrt(num25 * num25 + num27 * num27);
								netUpdate = true;
								num28 = num24 / num28;
								num25 *= num28;
								num27 *= num28;
								int num29 = 35;
								if (type == 111)
								{
									num29 = 11;
								}
								int num30 = 82;
								if (type == 111)
								{
									num30 = 81;
								}
								vector2.X += num25;
								vector2.Y += num27;
								if (Main.netMode != 1)
								{
									Projectile.NewProjectile(vector2.X, vector2.Y, num25, num27, num30, num29, 0f, Main.myPlayer);
								}
								if (Math.Abs(num27) > Math.Abs(num25) * 2f)
								{
									if (num27 > 0f)
									{
										ai[2] = 1f;
									}
									else
									{
										ai[2] = 5f;
									}
								}
								else if (Math.Abs(num25) > Math.Abs(num27) * 2f)
								{
									ai[2] = 3f;
								}
								else if (num27 > 0f)
								{
									ai[2] = 2f;
								}
								else
								{
									ai[2] = 4f;
								}
							}
							if (velocity.Y != 0f || ai[1] <= 0f)
							{
								ai[2] = 0f;
								ai[1] = 0f;
							}
							else
							{
								velocity.X *= 0.9f;
								spriteDirection = direction;
							}
						}
						if (ai[2] <= 0f && velocity.Y == 0f && ai[1] <= 0f && !Main.player[target].dead && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							float num31 = 10f;
							Vector2 vector3 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
							float num32 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector3.X;
							float num33 = Math.Abs(num32) * 0.1f;
							float num34 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector3.Y - num33;
							num32 += (float)Config.syncedRand.Next(-40, 41);
							num34 += (float)Config.syncedRand.Next(-40, 41);
							float num35 = (float)Math.Sqrt(num32 * num32 + num34 * num34);
							if (num35 < 700f)
							{
								netUpdate = true;
								velocity.X *= 0.5f;
								num35 = num31 / num35;
								num32 *= num35;
								num34 *= num35;
								ai[2] = 3f;
								ai[1] = num23;
								if (Math.Abs(num34) > Math.Abs(num32) * 2f)
								{
									if (num34 > 0f)
									{
										ai[2] = 1f;
									}
									else
									{
										ai[2] = 5f;
									}
								}
								else if (Math.Abs(num32) > Math.Abs(num34) * 2f)
								{
									ai[2] = 3f;
								}
								else if (num34 > 0f)
								{
									ai[2] = 2f;
								}
								else
								{
									ai[2] = 4f;
								}
							}
						}
						if (ai[2] <= 0f)
						{
							if (velocity.X < -1f || velocity.X > 1f)
							{
								if (velocity.Y == 0f)
								{
									velocity *= 0.8f;
								}
							}
							else if (velocity.X < 1f && direction == 1)
							{
								velocity.X += 0.07f;
								if (velocity.X > 1f)
								{
									velocity.X = 1f;
								}
							}
							else if (velocity.X > -1f && direction == -1)
							{
								velocity.X -= 0.07f;
								if (velocity.X < -1f)
								{
									velocity.X = -1f;
								}
							}
						}
					}
				}
				if (type == 109 && Main.netMode != 1 && !Main.player[target].dead)
				{
					if (justHit)
					{
						ai[2] = 0f;
					}
					ai[2] += 1f;
					if (ai[2] > 450f)
					{
						Vector2 vector4 = new Vector2(base.position.X + (float)base.width * 0.5f - (float)(direction * 24), base.position.Y + 4f);
						int num36 = 3 * direction;
						int num37 = -5;
						int num38 = Projectile.NewProjectile(vector4.X, vector4.Y, num36, num37, 75, 0, 0f, Main.myPlayer);
						Main.projectile[num38].timeLeft = 300;
						ai[2] = 0f;
					}
				}
				bool flag4 = false;
				if (velocity.Y == 0f)
				{
					int num39 = (int)(base.position.Y + (float)base.height + 8f) / 16;
					int num40 = (int)base.position.X / 16;
					int num41 = (int)(base.position.X + (float)base.width) / 16;
					for (int l = num40; l <= num41; l++)
					{
						if (Main.tile[l, num39] == null)
						{
							return;
						}
						if (Main.tile[l, num39].active && Main.tileSolid[Main.tile[l, num39].type])
						{
							flag4 = true;
							break;
						}
					}
				}
				if (flag4)
				{
					int num42 = (int)((base.position.X + (float)(base.width / 2) + (float)((base.width / 2 + 6) * direction)) / 16f);
					int num43 = (int)((base.position.Y + (float)base.height - 15f) / 16f);
					if (type == 109)
					{
						num42 = (int)((base.position.X + (float)(base.width / 2) + (float)((base.width / 2 + 16) * direction)) / 16f);
					}
					if (Main.tile[num42, num43] == null)
					{
						Main.tile[num42, num43] = new Tile();
					}
					if (Main.tile[num42, num43 - 1] == null)
					{
						Main.tile[num42, num43 - 1] = new Tile();
					}
					if (Main.tile[num42, num43 - 2] == null)
					{
						Main.tile[num42, num43 - 2] = new Tile();
					}
					if (Main.tile[num42, num43 - 3] == null)
					{
						Main.tile[num42, num43 - 3] = new Tile();
					}
					if (Main.tile[num42, num43 + 1] == null)
					{
						Main.tile[num42, num43 + 1] = new Tile();
					}
					if (Main.tile[num42 + direction, num43 - 1] == null)
					{
						Main.tile[num42 + direction, num43 - 1] = new Tile();
					}
					if (Main.tile[num42 + direction, num43 + 1] == null)
					{
						Main.tile[num42 + direction, num43 + 1] = new Tile();
					}
					if (Main.tile[num42, num43 - 1].active && Main.tile[num42, num43 - 1].type == 10 && flag3)
					{
						ai[2] += 1f;
						ai[3] = 0f;
						if (ai[2] >= 60f)
						{
							if (!Main.bloodMoon && (type == 3 || type == 132))
							{
								ai[1] = 0f;
							}
							velocity.X = 0.5f * (0f - (float)direction);
							ai[1] += 1f;
							if (type == 27)
							{
								ai[1] += 1f;
							}
							if (type == 31)
							{
								ai[1] += 6f;
							}
							ai[2] = 0f;
							bool flag5 = false;
							if (ai[1] >= 10f)
							{
								flag5 = true;
								ai[1] = 10f;
							}
							WorldGen.KillTile(num42, num43 - 1, fail: true);
							if ((Main.netMode != 1 || !flag5) && flag5 && Main.netMode != 1)
							{
								if (type == 26)
								{
									WorldGen.KillTile(num42, num43 - 1);
									if (Main.netMode == 2)
									{
										NetMessage.SendData(17, -1, -1, "", 0, num42, num43 - 1);
									}
								}
								else
								{
									bool flag6 = WorldGen.OpenDoor(num42, num43, direction);
									if (!flag6)
									{
										ai[3] = num11;
										netUpdate = true;
									}
									if (Main.netMode == 2 && flag6)
									{
										NetMessage.SendData(19, -1, -1, "", 0, num42, num43, direction);
									}
								}
							}
						}
					}
					else
					{
						if ((velocity.X < 0f && spriteDirection == -1) || (velocity.X > 0f && spriteDirection == 1))
						{
							if (Main.tile[num42, num43 - 2].active && Main.tileSolid[Main.tile[num42, num43 - 2].type])
							{
								if (Main.tile[num42, num43 - 3].active && Main.tileSolid[Main.tile[num42, num43 - 3].type])
								{
									velocity.Y = -8f;
									netUpdate = true;
								}
								else
								{
									velocity.Y = -7f;
									netUpdate = true;
								}
							}
							else if (Main.tile[num42, num43 - 1].active && Main.tileSolid[Main.tile[num42, num43 - 1].type])
							{
								velocity.Y = -6f;
								netUpdate = true;
							}
							else if (Main.tile[num42, num43].active && Main.tileSolid[Main.tile[num42, num43].type])
							{
								velocity.Y = -5f;
								netUpdate = true;
							}
							else if (base.directionY < 0 && type != 67 && (!Main.tile[num42, num43 + 1].active || !Main.tileSolid[Main.tile[num42, num43 + 1].type]) && (!Main.tile[num42 + direction, num43 + 1].active || !Main.tileSolid[Main.tile[num42 + direction, num43 + 1].type]))
							{
								velocity.Y = -8f;
								velocity.X *= 1.5f;
								netUpdate = true;
							}
							else if (flag3)
							{
								ai[1] = 0f;
								ai[2] = 0f;
							}
						}
						if ((type == 31 || type == 47 || type == 77 || type == 104) && velocity.Y == 0f && Math.Abs(base.position.X + (float)(base.width / 2) - (Main.player[target].position.X + (float)(Main.player[target].width / 2))) < 100f && Math.Abs(base.position.Y + (float)(base.height / 2) - (Main.player[target].position.Y + (float)(Main.player[target].height / 2))) < 50f && ((direction > 0 && velocity.X >= 1f) || (direction < 0 && velocity.X <= -1f)))
						{
							velocity.X *= 2f;
							if (velocity.X > 3f)
							{
								velocity.X = 3f;
							}
							if (velocity.X < -3f)
							{
								velocity.X = -3f;
							}
							velocity.Y = -4f;
							netUpdate = true;
						}
						if (type == 120 && velocity.Y < 0f)
						{
							velocity.Y *= 1.1f;
						}
					}
				}
				else if (flag3)
				{
					ai[1] = 0f;
					ai[2] = 0f;
				}
				if (Main.netMode == 1 || type != 120 || !(ai[3] >= (float)num11))
				{
					return;
				}
				int num44 = (int)Main.player[target].position.X / 16;
				int num45 = (int)Main.player[target].position.Y / 16;
				int num46 = (int)base.position.X / 16;
				int num47 = (int)base.position.Y / 16;
				int num48 = 20;
				int num49 = 0;
				bool flag7 = false;
				if (Math.Abs(base.position.X - Main.player[target].position.X) + Math.Abs(base.position.Y - Main.player[target].position.Y) > 2000f)
				{
					num49 = 100;
					flag7 = true;
				}
				while (!flag7 && num49 < 100)
				{
					num49++;
					int num50 = Config.syncedRand.Next(num44 - num48, num44 + num48);
					int num51 = Config.syncedRand.Next(num45 - num48, num45 + num48);
					for (int m = num51; m < num45 + num48; m++)
					{
						if ((m < num45 - 4 || m > num45 + 4 || num50 < num44 - 4 || num50 > num44 + 4) && (m < num47 - 1 || m > num47 + 1 || num50 < num46 - 1 || num50 > num46 + 1) && Main.tile[num50, m].active)
						{
							bool flag8 = true;
							if (type == 32 && Main.tile[num50, m - 1].wall == 0)
							{
								flag8 = false;
							}
							else if (Main.tile[num50, m - 1].lava)
							{
								flag8 = false;
							}
							if (flag8 && Main.tileSolid[Main.tile[num50, m].type] && !Collision.SolidTiles(num50 - 1, num50 + 1, m - 4, m - 1))
							{
								base.position.X = num50 * 16 - base.width / 2;
								base.position.Y = m * 16 - base.height;
								netUpdate = true;
								ai[3] = -120f;
							}
						}
					}
				}
			}
			else if (aiStyle == 4)
			{
				if (target < 0 || target == 255 || Main.player[target].dead || !Main.player[target].active)
				{
					TargetClosest();
				}
				bool dead = Main.player[target].dead;
				float num52 = base.position.X + (float)(base.width / 2) - Main.player[target].position.X - (float)(Main.player[target].width / 2);
				float num53 = base.position.Y + (float)base.height - 59f - Main.player[target].position.Y - (float)(Main.player[target].height / 2);
				float num54 = (float)Math.Atan2(num53, num52) + 1.57f;
				if (num54 < 0f)
				{
					num54 += 6.283f;
				}
				else if ((double)num54 > 6.283)
				{
					num54 -= 6.283f;
				}
				float num55 = 0f;
				if (ai[0] == 0f && ai[1] == 0f)
				{
					num55 = 0.02f;
				}
				if (ai[0] == 0f && ai[1] == 2f && ai[2] > 40f)
				{
					num55 = 0.05f;
				}
				if (ai[0] == 3f && ai[1] == 0f)
				{
					num55 = 0.05f;
				}
				if (ai[0] == 3f && ai[1] == 2f && ai[2] > 40f)
				{
					num55 = 0.08f;
				}
				if (rotation < num54)
				{
					if ((double)(num54 - rotation) > 3.1415)
					{
						rotation -= num55;
					}
					else
					{
						rotation += num55;
					}
				}
				else if (rotation > num54)
				{
					if ((double)(rotation - num54) > 3.1415)
					{
						rotation += num55;
					}
					else
					{
						rotation -= num55;
					}
				}
				if (rotation > num54 - num55 && rotation < num54 + num55)
				{
					rotation = num54;
				}
				if (rotation < 0f)
				{
					rotation += 6.283f;
				}
				else if ((double)rotation > 6.283)
				{
					rotation -= 6.283f;
				}
				if (rotation > num54 - num55 && rotation < num54 + num55)
				{
					rotation = num54;
				}
				if (Config.syncedRand.Next(5) == 0)
				{
					Vector2 position6 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
					int width6 = base.width;
					int height6 = (int)((float)base.height * 0.5f);
					int num56 = 5;
					float x3 = velocity.X;
					float speedY6 = 2f;
					int num57 = 0;
					int num58 = Dust.NewDust(position6, width6, height6, num56, x3, speedY6, num57);
					Dust dust8 = Main.dust[num58];
					dust8.velocity.X = dust8.velocity.X * 0.5f;
					Dust dust9 = Main.dust[num58];
					dust9.velocity.Y = dust9.velocity.Y * 0.1f;
				}
				if (Main.dayTime || dead)
				{
					velocity.Y -= 0.04f;
					if (timeLeft > 10)
					{
						timeLeft = 10;
					}
					return;
				}
				if (ai[0] == 0f)
				{
					if (ai[1] == 0f)
					{
						float num59 = 5f;
						float num60 = 0.04f;
						Vector2 vector5 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num61 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector5.X;
						float num62 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 200f - vector5.Y;
						float num63 = (float)Math.Sqrt(num61 * num61 + num62 * num62);
						float num64 = num63;
						num63 = num59 / num63;
						num61 *= num63;
						num62 *= num63;
						if (velocity.X < num61)
						{
							velocity.X += num60;
							if (velocity.X < 0f && num61 > 0f)
							{
								velocity.X += num60;
							}
						}
						else if (velocity.X > num61)
						{
							velocity.X -= num60;
							if (velocity.X > 0f && num61 < 0f)
							{
								velocity.X -= num60;
							}
						}
						if (velocity.Y < num62)
						{
							velocity.Y += num60;
							if (velocity.Y < 0f && num62 > 0f)
							{
								velocity.Y += num60;
							}
						}
						else if (velocity.Y > num62)
						{
							velocity.Y -= num60;
							if (velocity.Y > 0f && num62 < 0f)
							{
								velocity.Y -= num60;
							}
						}
						ai[2] += 1f;
						if (ai[2] >= 600f)
						{
							ai[1] = 1f;
							ai[2] = 0f;
							ai[3] = 0f;
							target = 255;
							netUpdate = true;
						}
						else if (base.position.Y + (float)base.height < Main.player[target].position.Y && num64 < 500f)
						{
							if (!Main.player[target].dead)
							{
								ai[3] += 1f;
							}
							if (ai[3] >= 110f)
							{
								ai[3] = 0f;
								rotation = num54;
								float num65 = 5f;
								float num66 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector5.X;
								float num67 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector5.Y;
								float num68 = (float)Math.Sqrt(num66 * num66 + num67 * num67);
								num68 = num65 / num68;
								Vector2 vector6 = vector5;
								Vector2 vector7 = default(Vector2);
								vector7.X = num66 * num68;
								vector7.Y = num67 * num68;
								vector6.X += vector7.X * 10f;
								vector6.Y += vector7.Y * 10f;
								if (Main.netMode != 1)
								{
									int num69 = NewNPC((int)vector6.X, (int)vector6.Y, 5);
									Main.npc[num69].velocity.X = vector7.X;
									Main.npc[num69].velocity.Y = vector7.Y;
									if (Main.netMode == 2 && num69 < 200)
									{
										NetMessage.SendData(23, -1, -1, "", num69);
									}
								}
								Main.PlaySound(3, (int)vector6.X, (int)vector6.Y);
								for (int n = 0; n < 10; n++)
								{
									Vector2 position7 = vector6;
									int width7 = 20;
									int height7 = 20;
									int num70 = 5;
									float speedX4 = vector7.X * 0.4f;
									float speedY7 = vector7.Y * 0.4f;
									int num71 = 0;
									Dust.NewDust(position7, width7, height7, num70, speedX4, speedY7, num71);
								}
							}
						}
					}
					else if (ai[1] == 1f)
					{
						rotation = num54;
						float num72 = 6f;
						Vector2 vector8 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num73 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector8.X;
						float num74 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector8.Y;
						float num75 = (float)Math.Sqrt(num73 * num73 + num74 * num74);
						num75 = num72 / num75;
						velocity.X = num73 * num75;
						velocity.Y = num74 * num75;
						ai[1] = 2f;
					}
					else if (ai[1] == 2f)
					{
						ai[2] += 1f;
						if (ai[2] >= 40f)
						{
							velocity.X *= 0.98f;
							velocity.Y *= 0.98f;
							if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
							{
								velocity.X = 0f;
							}
							if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
							{
								velocity.Y = 0f;
							}
						}
						else
						{
							rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
						}
						if (ai[2] >= 150f)
						{
							ai[3] += 1f;
							ai[2] = 0f;
							target = 255;
							rotation = num54;
							if (ai[3] >= 3f)
							{
								ai[1] = 0f;
								ai[3] = 0f;
							}
							else
							{
								ai[1] = 1f;
							}
						}
					}
					if ((double)life < (double)lifeMax * 0.5)
					{
						ai[0] = 1f;
						ai[1] = 0f;
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					return;
				}
				if (ai[0] == 1f || ai[0] == 2f)
				{
					if (ai[0] == 1f)
					{
						ai[2] += 0.005f;
						if ((double)ai[2] > 0.5)
						{
							ai[2] = 0.5f;
						}
					}
					else
					{
						ai[2] -= 0.005f;
						if (ai[2] < 0f)
						{
							ai[2] = 0f;
						}
					}
					rotation += ai[2];
					ai[1] += 1f;
					if (ai[1] == 100f)
					{
						ai[0] += 1f;
						ai[1] = 0f;
						if (ai[0] == 3f)
						{
							ai[2] = 0f;
						}
						else
						{
							Main.PlaySound(3, (int)base.position.X, (int)base.position.Y);
							for (int num76 = 0; num76 < 2; num76++)
							{
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 8);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 6);
							}
							for (int num77 = 0; num77 < 20; num77++)
							{
								Vector2 position8 = base.position;
								int width8 = base.width;
								int height8 = base.height;
								int num78 = 5;
								float speedX5 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								float speedY8 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								int num79 = 0;
								Dust.NewDust(position8, width8, height8, num78, speedX5, speedY8, num79);
							}
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						}
					}
					Vector2 position9 = base.position;
					int width9 = base.width;
					int height9 = base.height;
					int num80 = 5;
					float speedX6 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					float speedY9 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					int num81 = 0;
					Dust.NewDust(position9, width9, height9, num80, speedX6, speedY9, num81);
					velocity.X *= 0.98f;
					velocity.Y *= 0.98f;
					if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
					{
						velocity.X = 0f;
					}
					if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
					{
						velocity.Y = 0f;
					}
					return;
				}
				damage = 23;
				defense = 0;
				if (ai[1] == 0f)
				{
					float num82 = 6f;
					float num83 = 0.07f;
					Vector2 vector9 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num84 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector9.X;
					float num85 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 120f - vector9.Y;
					float num86 = (float)Math.Sqrt(num84 * num84 + num85 * num85);
					num86 = num82 / num86;
					num84 *= num86;
					num85 *= num86;
					if (velocity.X < num84)
					{
						velocity.X += num83;
						if (velocity.X < 0f && num84 > 0f)
						{
							velocity.X += num83;
						}
					}
					else if (velocity.X > num84)
					{
						velocity.X -= num83;
						if (velocity.X > 0f && num84 < 0f)
						{
							velocity.X -= num83;
						}
					}
					if (velocity.Y < num85)
					{
						velocity.Y += num83;
						if (velocity.Y < 0f && num85 > 0f)
						{
							velocity.Y += num83;
						}
					}
					else if (velocity.Y > num85)
					{
						velocity.Y -= num83;
						if (velocity.Y > 0f && num85 < 0f)
						{
							velocity.Y -= num83;
						}
					}
					ai[2] += 1f;
					if (ai[2] >= 200f)
					{
						ai[1] = 1f;
						ai[2] = 0f;
						ai[3] = 0f;
						target = 255;
						netUpdate = true;
					}
				}
				else if (ai[1] == 1f)
				{
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					rotation = num54;
					float num87 = 6.8f;
					Vector2 vector10 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num88 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector10.X;
					float num89 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector10.Y;
					float num90 = (float)Math.Sqrt(num88 * num88 + num89 * num89);
					num90 = num87 / num90;
					velocity.X = num88 * num90;
					velocity.Y = num89 * num90;
					ai[1] = 2f;
				}
				else
				{
					if (ai[1] != 2f)
					{
						return;
					}
					ai[2] += 1f;
					if (ai[2] >= 40f)
					{
						velocity.X *= 0.97f;
						velocity.Y *= 0.97f;
						if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
						{
							velocity.X = 0f;
						}
						if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
						{
							velocity.Y = 0f;
						}
					}
					else
					{
						rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
					}
					if (ai[2] >= 130f)
					{
						ai[3] += 1f;
						ai[2] = 0f;
						target = 255;
						rotation = num54;
						if (ai[3] >= 3f)
						{
							ai[1] = 0f;
							ai[3] = 0f;
						}
						else
						{
							ai[1] = 1f;
						}
					}
				}
			}
			else if (aiStyle == 5)
			{
				if (target < 0 || target == 255 || Main.player[target].dead)
				{
					TargetClosest();
				}
				float num91 = 6f;
				float num92 = 0.05f;
				if (type == 6)
				{
					num91 = 4f;
					num92 = 0.02f;
				}
				else if (type == 94)
				{
					num91 = 4.2f;
					num92 = 0.022f;
				}
				else if (type == 42)
				{
					num91 = 3.5f;
					num92 = 0.021f;
				}
				else if (type == 23)
				{
					num91 = 1f;
					num92 = 0.03f;
				}
				else if (type == 5)
				{
					num91 = 5f;
					num92 = 0.03f;
				}
				Vector2 vector11 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num93 = Main.player[target].position.X + (float)(Main.player[target].width / 2);
				float num94 = Main.player[target].position.Y + (float)(Main.player[target].height / 2);
				num93 = (int)(num93 / 8f) * 8;
				num94 = (int)(num94 / 8f) * 8;
				vector11.X = (int)(vector11.X / 8f) * 8;
				vector11.Y = (int)(vector11.Y / 8f) * 8;
				num93 -= vector11.X;
				num94 -= vector11.Y;
				float num95 = (float)Math.Sqrt(num93 * num93 + num94 * num94);
				float num96 = num95;
				bool flag9 = false;
				if (num95 > 600f)
				{
					flag9 = true;
				}
				if (num95 == 0f)
				{
					num93 = velocity.X;
					num94 = velocity.Y;
				}
				else
				{
					num95 = num91 / num95;
					num93 *= num95;
					num94 *= num95;
				}
				if (type == 6 || type == 42 || type == 94 || type == 139)
				{
					if (num96 > 100f || type == 42 || type == 94)
					{
						ai[0] += 1f;
						if (ai[0] > 0f)
						{
							velocity.Y += 0.023f;
						}
						else
						{
							velocity.Y -= 0.023f;
						}
						if (ai[0] < -100f || ai[0] > 100f)
						{
							velocity.X += 0.023f;
						}
						else
						{
							velocity.X -= 0.023f;
						}
						if (ai[0] > 200f)
						{
							ai[0] = -200f;
						}
					}
					if (num96 < 150f && (type == 6 || type == 94))
					{
						velocity.X += num93 * 0.007f;
						velocity.Y += num94 * 0.007f;
					}
				}
				if (Main.player[target].dead)
				{
					num93 = (float)direction * num91 / 2f;
					num94 = (0f - num91) / 2f;
				}
				if (velocity.X < num93)
				{
					velocity.X += num92;
					if (type != 6 && type != 42 && type != 94 && type != 139 && velocity.X < 0f && num93 > 0f)
					{
						velocity.X += num92;
					}
				}
				else if (velocity.X > num93)
				{
					velocity.X -= num92;
					if (type != 6 && type != 42 && type != 94 && type != 139 && velocity.X > 0f && num93 < 0f)
					{
						velocity.X -= num92;
					}
				}
				if (velocity.Y < num94)
				{
					velocity.Y += num92;
					if (type != 6 && type != 42 && type != 94 && type != 139 && velocity.Y < 0f && num94 > 0f)
					{
						velocity.Y += num92;
					}
				}
				else if (velocity.Y > num94)
				{
					velocity.Y -= num92;
					if (type != 6 && type != 42 && type != 94 && type != 139 && velocity.Y > 0f && num94 < 0f)
					{
						velocity.Y -= num92;
					}
				}
				if (type == 23)
				{
					if (num93 > 0f)
					{
						spriteDirection = 1;
						rotation = (float)Math.Atan2(num94, num93);
					}
					else if (num93 < 0f)
					{
						spriteDirection = -1;
						rotation = (float)Math.Atan2(num94, num93) + 3.14f;
					}
				}
				else if (type == 139)
				{
					localAI[0] += 1f;
					if (justHit)
					{
						localAI[0] = 0f;
					}
					if (Main.netMode != 1 && localAI[0] >= 120f)
					{
						localAI[0] = 0f;
						if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							int num97 = 25;
							int num98 = 84;
							Projectile.NewProjectile(vector11.X, vector11.Y, num93, num94, num98, num97, 0f, Main.myPlayer);
						}
					}
					int num99 = (int)base.position.X + base.width / 2;
					int num100 = (int)base.position.Y + base.height / 2;
					num99 /= 16;
					num100 /= 16;
					if (!WorldGen.SolidTile(num99, num100))
					{
						Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 0.3f, 0.1f, 0.05f);
					}
					if (num93 > 0f)
					{
						spriteDirection = 1;
						rotation = (float)Math.Atan2(num94, num93);
					}
					if (num93 < 0f)
					{
						spriteDirection = -1;
						rotation = (float)Math.Atan2(num94, num93) + 3.14f;
					}
				}
				else if (type == 6 || type == 94)
				{
					rotation = (float)Math.Atan2(num94, num93) - 1.57f;
				}
				else if (type == 42)
				{
					if (num93 > 0f)
					{
						spriteDirection = 1;
					}
					if (num93 < 0f)
					{
						spriteDirection = -1;
					}
					rotation = velocity.X * 0.1f;
				}
				else
				{
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
				}
				if (type == 6 || type == 23 || type == 42 || type == 94 || type == 139)
				{
					float num101 = 0.7f;
					if (type == 6)
					{
						num101 = 0.4f;
					}
					if (collideX)
					{
						netUpdate = true;
						velocity.X = oldVelocity.X * (0f - num101);
						if (direction == -1 && velocity.X > 0f && velocity.X < 2f)
						{
							velocity.X = 2f;
						}
						if (direction == 1 && velocity.X < 0f && velocity.X > -2f)
						{
							velocity.X = -2f;
						}
					}
					if (collideY)
					{
						netUpdate = true;
						velocity.Y = oldVelocity.Y * (0f - num101);
						if (velocity.Y > 0f && (double)velocity.Y < 1.5)
						{
							velocity.Y = 2f;
						}
						if (velocity.Y < 0f && (double)velocity.Y > -1.5)
						{
							velocity.Y = -2f;
						}
					}
					if (type == 23)
					{
						Vector2 position10 = new Vector2(base.position.X - velocity.X, base.position.Y - velocity.Y);
						int width10 = base.width;
						int height10 = base.height;
						int num102 = 6;
						float speedX7 = velocity.X * 0.2f;
						float speedY10 = velocity.Y * 0.2f;
						int num103 = 100;
						int num104 = Dust.NewDust(position10, width10, height10, num102, speedX7, speedY10, num103, default(Color), 2f);
						Main.dust[num104].noGravity = true;
						Dust dust10 = Main.dust[num104];
						dust10.velocity.X = dust10.velocity.X * 0.3f;
						Dust dust11 = Main.dust[num104];
						dust11.velocity.Y = dust11.velocity.Y * 0.3f;
					}
					else if (type != 42 && type != 139 && Config.syncedRand.Next(20) == 0)
					{
						int num105 = Dust.NewDust(new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f), base.width, (int)((float)base.height * 0.5f), 18, velocity.X, 2f, 75, color, scale);
						Dust dust12 = Main.dust[num105];
						dust12.velocity.X = dust12.velocity.X * 0.5f;
						Dust dust13 = Main.dust[num105];
						dust13.velocity.Y = dust13.velocity.Y * 0.1f;
					}
				}
				else if (Config.syncedRand.Next(40) == 0)
				{
					Vector2 position11 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
					int width11 = base.width;
					int height11 = (int)((float)base.height * 0.5f);
					int num106 = 5;
					float x4 = velocity.X;
					float speedY11 = 2f;
					int num107 = 0;
					int num108 = Dust.NewDust(position11, width11, height11, num106, x4, speedY11, num107);
					Dust dust14 = Main.dust[num108];
					dust14.velocity.X = dust14.velocity.X * 0.5f;
					Dust dust15 = Main.dust[num108];
					dust15.velocity.Y = dust15.velocity.Y * 0.1f;
				}
				if ((type == 6 || type == 94) && wet)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.Y -= 0.3f;
					if (velocity.Y < -2f)
					{
						velocity.Y = -2f;
					}
				}
				if (type == 42)
				{
					if (wet)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.95f;
						}
						velocity.Y -= 0.5f;
						if (velocity.Y < -4f)
						{
							velocity.Y = -4f;
						}
						TargetClosest();
					}
					if (ai[1] == 101f)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 17);
						ai[1] = 0f;
					}
					if (Main.netMode != 1)
					{
						ai[1] += (float)Config.syncedRand.Next(5, 20) * 0.1f * scale;
						if (ai[1] >= 130f)
						{
							if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
							{
								float num109 = 8f;
								Vector2 vector12 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)(base.height / 2));
								float num110 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector12.X + (float)Config.syncedRand.Next(-20, 21);
								float num111 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector12.Y + (float)Config.syncedRand.Next(-20, 21);
								if ((num110 < 0f && velocity.X < 0f) || (num110 > 0f && velocity.X > 0f))
								{
									float num112 = (float)Math.Sqrt(num110 * num110 + num111 * num111);
									num112 = num109 / num112;
									num110 *= num112;
									num111 *= num112;
									int num113 = (int)(13f * scale);
									int num114 = 55;
									int num115 = Projectile.NewProjectile(vector12.X, vector12.Y, num110, num111, num114, num113, 0f, Main.myPlayer);
									Main.projectile[num115].timeLeft = 300;
									ai[1] = 101f;
									netUpdate = true;
								}
								else
								{
									ai[1] = 0f;
								}
							}
							else
							{
								ai[1] = 0f;
							}
						}
					}
				}
				if (type == 139 && flag9)
				{
					if ((velocity.X > 0f && num93 > 0f) || (velocity.X < 0f && num93 < 0f))
					{
						if (Math.Abs(velocity.X) < 12f)
						{
							velocity.X *= 1.05f;
						}
					}
					else
					{
						velocity.X *= 0.9f;
					}
				}
				if (Main.netMode != 1 && type == 94 && !Main.player[target].dead)
				{
					if (justHit)
					{
						localAI[0] = 0f;
					}
					localAI[0] += 1f;
					if (localAI[0] == 180f)
					{
						if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							NewNPC((int)(base.position.X + (float)(base.width / 2) + velocity.X), (int)(base.position.Y + (float)(base.height / 2) + velocity.Y), 112);
						}
						localAI[0] = 0f;
					}
				}
				if ((Main.dayTime && type != 6 && type != 23 && type != 42 && type != 94) || Main.player[target].dead)
				{
					velocity.Y -= num92 * 2f;
					if (timeLeft > 10)
					{
						timeLeft = 10;
					}
				}
				if (((velocity.X > 0f && oldVelocity.X < 0f) || (velocity.X < 0f && oldVelocity.X > 0f) || (velocity.Y > 0f && oldVelocity.Y < 0f) || (velocity.Y < 0f && oldVelocity.Y > 0f)) && !justHit)
				{
					netUpdate = true;
				}
			}
			else if (aiStyle == 6)
			{
				if (type == 117 && localAI[1] == 0f)
				{
					localAI[1] = 1f;
					Main.PlaySound(4, (int)base.position.X, (int)base.position.Y, 13);
					int num116 = 1;
					if (velocity.X < 0f)
					{
						num116 = -1;
					}
					for (int num117 = 0; num117 < 20; num117++)
					{
						Vector2 position12 = new Vector2(base.position.X - 20f, base.position.Y - 20f);
						int width12 = base.width + 40;
						int height12 = base.height + 40;
						int num118 = 5;
						float speedX8 = num116 * 8;
						float speedY12 = -1f;
						int num119 = 0;
						Dust.NewDust(position12, width12, height12, num118, speedX8, speedY12, num119);
					}
				}
				if (type >= 13 && type <= 15)
				{
					realLife = -1;
				}
				else if (ai[3] > 0f)
				{
					realLife = (int)ai[3];
				}
				if (target < 0 || target == 255 || Main.player[target].dead)
				{
					TargetClosest();
				}
				if (Main.player[target].dead && timeLeft > 300)
				{
					timeLeft = 300;
				}
				if (Main.netMode != 1)
				{
					if (type == 87 && ai[0] == 0f)
					{
						ai[3] = whoAmI;
						realLife = whoAmI;
						int num120 = whoAmI;
						for (int num121 = 0; num121 < 14; num121++)
						{
							int num122 = 89;
							switch (num121)
							{
							case 1:
							case 8:
								num122 = 88;
								break;
							case 11:
								num122 = 90;
								break;
							case 12:
								num122 = 91;
								break;
							case 13:
								num122 = 92;
								break;
							}
							int num123 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), num122, whoAmI);
							Main.npc[num123].ai[3] = whoAmI;
							Main.npc[num123].realLife = whoAmI;
							Main.npc[num123].ai[1] = num120;
							Main.npc[num120].ai[0] = num123;
							NetMessage.SendData(23, -1, -1, "", num123);
							num120 = num123;
						}
					}
					if ((type == 7 || type == 8 || type == 10 || type == 11 || type == 13 || type == 14 || type == 39 || type == 40 || type == 95 || type == 96 || type == 98 || type == 99 || type == 117 || type == 118) && ai[0] == 0f)
					{
						if (type == 7 || type == 10 || type == 13 || type == 39 || type == 95 || type == 98 || type == 117)
						{
							if (type < 13 || type > 15)
							{
								ai[3] = whoAmI;
								realLife = whoAmI;
							}
							ai[2] = Config.syncedRand.Next(8, 13);
							if (type == 10)
							{
								ai[2] = Config.syncedRand.Next(4, 7);
							}
							if (type == 13)
							{
								ai[2] = Config.syncedRand.Next(45, 56);
							}
							if (type == 39)
							{
								ai[2] = Config.syncedRand.Next(12, 19);
							}
							if (type == 95)
							{
								ai[2] = Config.syncedRand.Next(6, 12);
							}
							if (type == 98)
							{
								ai[2] = Config.syncedRand.Next(20, 26);
							}
							if (type == 117)
							{
								ai[2] = Config.syncedRand.Next(3, 6);
							}
							ai[0] = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), type + 1, whoAmI);
						}
						else if ((type == 8 || type == 11 || type == 14 || type == 40 || type == 96 || type == 99 || type == 118) && ai[2] > 0f)
						{
							ai[0] = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), type, whoAmI);
						}
						else
						{
							ai[0] = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), type + 1, whoAmI);
						}
						if (type < 13 || type > 15)
						{
							Main.npc[(int)ai[0]].ai[3] = ai[3];
							Main.npc[(int)ai[0]].realLife = realLife;
						}
						Main.npc[(int)ai[0]].ai[1] = whoAmI;
						Main.npc[(int)ai[0]].ai[2] = ai[2] - 1f;
						netUpdate = true;
					}
					if ((type == 8 || type == 9 || type == 11 || type == 12 || type == 40 || type == 41 || type == 96 || type == 97 || type == 99 || type == 100 || (type > 87 && type <= 92) || type == 118 || type == 119) && (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != aiStyle))
					{
						life = 0;
						HitEffect();
						active = false;
					}
					if ((type == 7 || type == 8 || type == 10 || type == 11 || type == 39 || type == 40 || type == 95 || type == 96 || type == 98 || type == 99 || (type >= 87 && type < 92) || type == 117 || type == 118) && (!Main.npc[(int)ai[0]].active || Main.npc[(int)ai[0]].aiStyle != aiStyle))
					{
						life = 0;
						HitEffect();
						active = false;
					}
					if (type == 13 || type == 14 || type == 15)
					{
						if (!Main.npc[(int)ai[1]].active && !Main.npc[(int)ai[0]].active)
						{
							life = 0;
							HitEffect();
							active = false;
						}
						if (type == 13 && !Main.npc[(int)ai[0]].active)
						{
							life = 0;
							HitEffect();
							active = false;
						}
						if (type == 15 && !Main.npc[(int)ai[1]].active)
						{
							life = 0;
							HitEffect();
							active = false;
						}
						if (type == 14 && (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != aiStyle))
						{
							type = 13;
							int num124 = whoAmI;
							float num125 = (float)life / (float)lifeMax;
							float num126 = ai[0];
							SetDefaults(type);
							life = (int)((float)lifeMax * num125);
							ai[0] = num126;
							TargetClosest();
							netUpdate = true;
							whoAmI = num124;
						}
						if (type == 14 && (!Main.npc[(int)ai[0]].active || Main.npc[(int)ai[0]].aiStyle != aiStyle))
						{
							int num127 = whoAmI;
							float num128 = (float)life / (float)lifeMax;
							float num129 = ai[1];
							SetDefaults(type);
							life = (int)((float)lifeMax * num128);
							ai[1] = num129;
							TargetClosest();
							netUpdate = true;
							whoAmI = num127;
						}
						if (life == 0)
						{
							bool flag10 = true;
							for (int num130 = 0; num130 < 200; num130++)
							{
								if (Main.npc[num130].active && (Main.npc[num130].type == 13 || Main.npc[num130].type == 14 || Main.npc[num130].type == 15))
								{
									flag10 = false;
									break;
								}
							}
							if (flag10)
							{
								boss = true;
								NPCLoot();
							}
						}
					}
					if (!active && Main.netMode == 2)
					{
						NetMessage.SendData(28, -1, -1, "", whoAmI, 1f, 0f, 0f, -1);
					}
				}
				int num131 = (int)(base.position.X / 16f) - 1;
				int num132 = (int)((base.position.X + (float)base.width) / 16f) + 2;
				int num133 = (int)(base.position.Y / 16f) - 1;
				int num134 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
				if (num131 < 0)
				{
					num131 = 0;
				}
				if (num132 > Main.maxTilesX)
				{
					num132 = Main.maxTilesX;
				}
				if (num133 < 0)
				{
					num133 = 0;
				}
				if (num134 > Main.maxTilesY)
				{
					num134 = Main.maxTilesY;
				}
				bool flag11 = false;
				if (type >= 87 && type <= 92)
				{
					flag11 = true;
				}
				if (!flag11)
				{
					Vector2 vector13 = default(Vector2);
					for (int num135 = num131; num135 < num132; num135++)
					{
						for (int num136 = num133; num136 < num134; num136++)
						{
							if (Main.tile[num135, num136] == null || ((!Main.tile[num135, num136].active || (!Main.tileSolid[Main.tile[num135, num136].type] && (!Main.tileSolidTop[Main.tile[num135, num136].type] || Main.tile[num135, num136].frameY != 0))) && Main.tile[num135, num136].liquid <= 64))
							{
								continue;
							}
							vector13.X = num135 * 16;
							vector13.Y = num136 * 16;
							if (base.position.X + (float)base.width > vector13.X && base.position.X < vector13.X + 16f && base.position.Y + (float)base.height > vector13.Y && base.position.Y < vector13.Y + 16f)
							{
								flag11 = true;
								if (Config.syncedRand.Next(100) == 0 && type != 117 && Main.tile[num135, num136].active)
								{
									WorldGen.KillTile(num135, num136, fail: true, effectOnly: true);
								}
							}
						}
					}
				}
				if (!flag11 && (type == 7 || type == 10 || type == 13 || type == 39 || type == 95 || type == 98 || type == 117))
				{
					Rectangle rectangle = new Rectangle((int)base.position.X, (int)base.position.Y, base.width, base.height);
					int num137 = 1000;
					bool flag12 = true;
					for (int num138 = 0; num138 < 255; num138++)
					{
						if (Main.player[num138].active)
						{
							Rectangle rectangle2 = new Rectangle((int)Main.player[num138].position.X - num137, (int)Main.player[num138].position.Y - num137, num137 * 2, num137 * 2);
							if (rectangle.Intersects(rectangle2))
							{
								flag12 = false;
								break;
							}
						}
					}
					if (flag12)
					{
						flag11 = true;
					}
				}
				if (type >= 87 && type <= 92)
				{
					if (velocity.X < 0f)
					{
						spriteDirection = 1;
					}
					else if (velocity.X > 0f)
					{
						spriteDirection = -1;
					}
				}
				float num139 = 8f;
				float num140 = 0.07f;
				if (type == 95)
				{
					num139 = 5.5f;
					num140 = 0.045f;
				}
				if (type == 10)
				{
					num139 = 6f;
					num140 = 0.05f;
				}
				if (type == 13)
				{
					num139 = 10f;
					num140 = 0.07f;
				}
				if (type == 87)
				{
					num139 = 11f;
					num140 = 0.25f;
				}
				if (type == 117 && Main.wof >= 0)
				{
					float num141 = (float)Main.npc[Main.wof].life / (float)Main.npc[Main.wof].lifeMax;
					if ((double)num141 < 0.5)
					{
						num139 += 1f;
						num140 += 0.1f;
					}
					if ((double)num141 < 0.25)
					{
						num139 += 1f;
						num140 += 0.1f;
					}
					if ((double)num141 < 0.1)
					{
						num139 += 2f;
						num140 += 0.1f;
					}
				}
				Vector2 vector14 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num142 = Main.player[target].position.X + (float)(Main.player[target].width / 2);
				float num143 = Main.player[target].position.Y + (float)(Main.player[target].height / 2);
				num142 = (int)(num142 / 16f) * 16;
				num143 = (int)(num143 / 16f) * 16;
				vector14.X = (int)(vector14.X / 16f) * 16;
				vector14.Y = (int)(vector14.Y / 16f) * 16;
				num142 -= vector14.X;
				num143 -= vector14.Y;
				float num144 = (float)Math.Sqrt(num142 * num142 + num143 * num143);
				if (ai[1] > 0f && ai[1] < (float)Main.npc.Length)
				{
					try
					{
						vector14 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num142 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - vector14.X;
						num143 = Main.npc[(int)ai[1]].position.Y + (float)(Main.npc[(int)ai[1]].height / 2) - vector14.Y;
					}
					catch
					{
					}
					rotation = (float)Math.Atan2(num143, num142) + 1.57f;
					num144 = (float)Math.Sqrt(num142 * num142 + num143 * num143);
					int num145 = base.width;
					if (type >= 87 && type <= 92)
					{
						num145 = 42;
					}
					num144 = (num144 - (float)num145) / num144;
					num142 *= num144;
					num143 *= num144;
					velocity = default(Vector2);
					base.position.X = base.position.X + num142;
					base.position.Y = base.position.Y + num143;
					if (type >= 87 && type <= 92)
					{
						if (num142 < 0f)
						{
							spriteDirection = 1;
						}
						else if (num142 > 0f)
						{
							spriteDirection = -1;
						}
					}
					return;
				}
				if (!flag11)
				{
					TargetClosest();
					velocity.Y += 0.11f;
					if (velocity.Y > num139)
					{
						velocity.Y = num139;
					}
					if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num139 * 0.4)
					{
						if (velocity.X < 0f)
						{
							velocity.X -= num140 * 1.1f;
						}
						else
						{
							velocity.X += num140 * 1.1f;
						}
					}
					else if (velocity.Y == num139)
					{
						if (velocity.X < num142)
						{
							velocity.X += num140;
						}
						else if (velocity.X > num142)
						{
							velocity.X -= num140;
						}
					}
					else if (velocity.Y > 4f)
					{
						if (velocity.X < 0f)
						{
							velocity.X += num140 * 0.9f;
						}
						else
						{
							velocity.X -= num140 * 0.9f;
						}
					}
				}
				else
				{
					if (type != 87 && type != 117 && soundDelay == 0)
					{
						float num146 = num144 / 40f;
						if (num146 < 10f)
						{
							num146 = 10f;
						}
						if (num146 > 20f)
						{
							num146 = 20f;
						}
						soundDelay = (int)num146;
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y);
					}
					num144 = (float)Math.Sqrt(num142 * num142 + num143 * num143);
					float num147 = Math.Abs(num142);
					float num148 = Math.Abs(num143);
					float num149 = num139 / num144;
					num142 *= num149;
					num143 *= num149;
					if ((type == 13 || type == 7) && !Main.player[target].zoneEvil)
					{
						bool flag13 = true;
						for (int num150 = 0; num150 < 255; num150++)
						{
							if (Main.player[num150].active && !Main.player[num150].dead && Main.player[num150].zoneEvil)
							{
								flag13 = false;
							}
						}
						if (flag13)
						{
							if (Main.netMode != 1 && (double)(base.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
							{
								active = false;
								int num151 = (int)ai[0];
								while (num151 > 0 && num151 < 200 && Main.npc[num151].active && Main.npc[num151].aiStyle == aiStyle)
								{
									int num152 = (int)Main.npc[num151].ai[0];
									Main.npc[num151].active = false;
									life = 0;
									if (Main.netMode == 2)
									{
										NetMessage.SendData(23, -1, -1, "", num151);
									}
									num151 = num152;
								}
								if (Main.netMode == 2)
								{
									NetMessage.SendData(23, -1, -1, "", whoAmI);
								}
							}
							num142 = 0f;
							num143 = num139;
						}
					}
					bool flag14 = false;
					if (type == 87)
					{
						if (((velocity.X > 0f && num142 < 0f) || (velocity.X < 0f && num142 > 0f) || (velocity.Y > 0f && num143 < 0f) || (velocity.Y < 0f && num143 > 0f)) && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > num140 / 2f && num144 < 300f)
						{
							flag14 = true;
							if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < num139)
							{
								velocity *= 1.1f;
							}
						}
						if (base.position.Y > Main.player[target].position.Y || (double)(Main.player[target].position.Y / 16f) > Main.worldSurface || Main.player[target].dead)
						{
							flag14 = true;
							if (Math.Abs(velocity.X) < num139 / 2f)
							{
								if (velocity.X == 0f)
								{
									velocity.X -= (float)direction;
								}
								velocity.X *= 1.1f;
							}
							else if (velocity.Y > 0f - num139)
							{
								velocity.Y -= num140;
							}
						}
					}
					if (!flag14)
					{
						if ((velocity.X > 0f && num142 > 0f) || (velocity.X < 0f && num142 < 0f) || (velocity.Y > 0f && num143 > 0f) || (velocity.Y < 0f && num143 < 0f))
						{
							if (velocity.X < num142)
							{
								velocity.X += num140;
							}
							else if (velocity.X > num142)
							{
								velocity.X -= num140;
							}
							if (velocity.Y < num143)
							{
								velocity.Y += num140;
							}
							else if (velocity.Y > num143)
							{
								velocity.Y -= num140;
							}
							if ((double)Math.Abs(num143) < (double)num139 * 0.2 && ((velocity.X > 0f && num142 < 0f) || (velocity.X < 0f && num142 > 0f)))
							{
								if (velocity.Y > 0f)
								{
									velocity.Y += num140 * 2f;
								}
								else
								{
									velocity.Y -= num140 * 2f;
								}
							}
							if ((double)Math.Abs(num142) < (double)num139 * 0.2 && ((velocity.Y > 0f && num143 < 0f) || (velocity.Y < 0f && num143 > 0f)))
							{
								if (velocity.X > 0f)
								{
									velocity.X += num140 * 2f;
								}
								else
								{
									velocity.X -= num140 * 2f;
								}
							}
						}
						else if (num147 > num148)
						{
							if (velocity.X < num142)
							{
								velocity.X += num140 * 1.1f;
							}
							else if (velocity.X > num142)
							{
								velocity.X -= num140 * 1.1f;
							}
							if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num139 * 0.5)
							{
								if (velocity.Y > 0f)
								{
									velocity.Y += num140;
								}
								else
								{
									velocity.Y -= num140;
								}
							}
						}
						else
						{
							if (velocity.Y < num143)
							{
								velocity.Y += num140 * 1.1f;
							}
							else if (velocity.Y > num143)
							{
								velocity.Y -= num140 * 1.1f;
							}
							if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num139 * 0.5)
							{
								if (velocity.X > 0f)
								{
									velocity.X += num140;
								}
								else
								{
									velocity.X -= num140;
								}
							}
						}
					}
				}
				rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
				if (type != 7 && type != 10 && type != 13 && type != 39 && type != 95 && type != 98 && type != 117)
				{
					return;
				}
				if (flag11)
				{
					if (localAI[0] != 1f)
					{
						netUpdate = true;
					}
					localAI[0] = 1f;
				}
				else
				{
					if (localAI[0] != 0f)
					{
						netUpdate = true;
					}
					localAI[0] = 0f;
				}
				if (((velocity.X > 0f && oldVelocity.X < 0f) || (velocity.X < 0f && oldVelocity.X > 0f) || (velocity.Y > 0f && oldVelocity.Y < 0f) || (velocity.Y < 0f && oldVelocity.Y > 0f)) && !justHit)
				{
					netUpdate = true;
				}
			}
			else if (aiStyle == 7)
			{
				if (type == 142 && Main.netMode != 1 && !Main.xMas)
				{
					StrikeNPC(9999, 0f, 0);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(28, -1, -1, "", whoAmI, 1f, 0f, 0f, 9999);
					}
				}
				int num153 = (int)(base.position.X + (float)(base.width / 2)) / 16;
				int num154 = (int)(base.position.Y + (float)base.height + 1f) / 16;
				if (type == 107)
				{
					savedGoblin = true;
				}
				if (type == 108)
				{
					savedWizard = true;
				}
				if (type == 124)
				{
					savedMech = true;
				}
				if (type == 46 && target == 255)
				{
					TargetClosest();
				}
				bool flag15 = false;
				base.directionY = -1;
				if (direction == 0)
				{
					direction = 1;
				}
				for (int num155 = 0; num155 < 255; num155++)
				{
					if (Main.player[num155].active && Main.player[num155].talkNPC == whoAmI)
					{
						flag15 = true;
						if (ai[0] != 0f)
						{
							netUpdate = true;
						}
						ai[0] = 0f;
						ai[1] = 300f;
						ai[2] = 100f;
						if (Main.player[num155].position.X + (float)(Main.player[num155].width / 2) < base.position.X + (float)(base.width / 2))
						{
							direction = -1;
						}
						else
						{
							direction = 1;
						}
					}
				}
				if (ai[3] > 0f)
				{
					life = -1;
					HitEffect();
					active = false;
					if (type == 37)
					{
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					}
				}
				if (dontRelocate)
				{
					homeless = false;
				}
				if (type == 37 && Main.netMode != 1)
				{
					homeless = false;
					homeTileX = Main.dungeonX;
					homeTileY = Main.dungeonY;
					if (downedBoss3)
					{
						ai[3] = 1f;
						netUpdate = true;
					}
				}
				int num156 = homeTileY;
				if (Main.netMode != 1 && homeTileY > 0)
				{
					for (; !WorldGen.SolidTile(homeTileX, num156) && num156 < Main.maxTilesY - 20; num156++)
					{
					}
				}
				if (Main.netMode != 1 && townNPC && (!Main.dayTime || Main.tileDungeon[Main.tile[num153, num154].type]) && (num153 != homeTileX || num154 != num156) && !homeless)
				{
					bool flag16 = true;
					for (int num157 = 0; num157 < 2; num157++)
					{
						Rectangle rectangle3 = new Rectangle((int)(base.position.X + (float)(base.width / 2) - (float)(sWidth / 2) - (float)safeRangeX), (int)(base.position.Y + (float)(base.height / 2) - (float)(sHeight / 2) - (float)safeRangeY), sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
						if (num157 == 1)
						{
							rectangle3 = new Rectangle(homeTileX * 16 + 8 - sWidth / 2 - safeRangeX, num156 * 16 + 8 - sHeight / 2 - safeRangeY, sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
						}
						for (int num158 = 0; num158 < 255; num158++)
						{
							if (Main.player[num158].active && new Rectangle((int)Main.player[num158].position.X, (int)Main.player[num158].position.Y, Main.player[num158].width, Main.player[num158].height).Intersects(rectangle3))
							{
								flag16 = false;
								break;
							}
							if (!flag16)
							{
								break;
							}
						}
					}
					if (flag16)
					{
						if (type == 37 || !Collision.SolidTiles(homeTileX - 1, homeTileX + 1, num156 - 3, num156 - 1) || dontRelocate)
						{
							velocity.X = 0f;
							velocity.Y = 0f;
							base.position.X = homeTileX * 16 + 8 - base.width / 2;
							base.position.Y = (float)(num156 * 16 - base.height) - 0.1f;
							netUpdate = true;
						}
						else
						{
							homeless = true;
							WorldGen.QuickFindHome(whoAmI);
						}
					}
				}
				if (ai[0] == 0f)
				{
					if (ai[2] > 0f)
					{
						ai[2] -= 1f;
					}
					if (!Main.dayTime && !flag15 && type != 46)
					{
						if (Main.netMode != 1)
						{
							if (num153 == homeTileX && num154 == num156)
							{
								if (velocity.X != 0f)
								{
									netUpdate = true;
								}
								if ((double)velocity.X > 0.1)
								{
									velocity.X -= 0.1f;
								}
								else if ((double)velocity.X < -0.1)
								{
									velocity.X += 0.1f;
								}
								else
								{
									velocity.X = 0f;
								}
							}
							else if (!flag15)
							{
								if (num153 > homeTileX)
								{
									direction = -1;
								}
								else
								{
									direction = 1;
								}
								ai[0] = 1f;
								ai[1] = 200 + Config.syncedRand.Next(200);
								ai[2] = 0f;
								netUpdate = true;
							}
						}
					}
					else
					{
						if ((double)velocity.X > 0.1)
						{
							velocity.X -= 0.1f;
						}
						else if ((double)velocity.X < -0.1)
						{
							velocity.X += 0.1f;
						}
						else
						{
							velocity.X = 0f;
						}
						if (Main.netMode != 1)
						{
							if (ai[1] > 0f)
							{
								ai[1] -= 1f;
							}
							if (ai[1] <= 0f)
							{
								ai[0] = 1f;
								ai[1] = 200 + Config.syncedRand.Next(200);
								if (type == 46)
								{
									ai[1] += Config.syncedRand.Next(200, 400);
								}
								ai[2] = 0f;
								netUpdate = true;
							}
						}
					}
					if (Main.netMode == 1 || (!Main.dayTime && (num153 != homeTileX || num154 != num156)))
					{
						return;
					}
					if (num153 < homeTileX - 25 || num153 > homeTileX + 25)
					{
						if (ai[2] == 0f)
						{
							if (num153 < homeTileX - 50 && direction == -1)
							{
								direction = 1;
								netUpdate = true;
							}
							else if (num153 > homeTileX + 50 && direction == 1)
							{
								direction = -1;
								netUpdate = true;
							}
						}
					}
					else if (Config.syncedRand.Next(80) == 0 && ai[2] == 0f)
					{
						ai[2] = 200f;
						direction *= -1;
						netUpdate = true;
					}
				}
				else
				{
					if (ai[0] != 1f)
					{
						return;
					}
					if (Main.netMode != 1 && !Main.dayTime && num153 == homeTileX && num154 == homeTileY && type != 46)
					{
						ai[0] = 0f;
						ai[1] = 200 + Config.syncedRand.Next(200);
						ai[2] = 60f;
						netUpdate = true;
						return;
					}
					if (Main.netMode != 1 && !homeless && !Main.tileDungeon[Main.tile[num153, num154].type] && (num153 < homeTileX - 35 || num153 > homeTileX + 35))
					{
						if (base.position.X < (float)(homeTileX * 16) && direction == -1)
						{
							ai[1] -= 5f;
						}
						else if (base.position.X > (float)(homeTileX * 16) && direction == 1)
						{
							ai[1] -= 5f;
						}
					}
					ai[1] -= 1f;
					if (ai[1] <= 0f)
					{
						ai[0] = 0f;
						ai[1] = 300 + Config.syncedRand.Next(300);
						if (type == 46)
						{
							ai[1] -= Config.syncedRand.Next(100);
						}
						ai[2] = 60f;
						netUpdate = true;
					}
					if (closeDoor && ((base.position.X + (float)(base.width / 2)) / 16f > (float)(doorX + 2) || (base.position.X + (float)(base.width / 2)) / 16f < (float)(doorX - 2)))
					{
						if (WorldGen.CloseDoor(doorX, doorY))
						{
							closeDoor = false;
							NetMessage.SendData(19, -1, -1, "", 1, doorX, doorY, direction);
						}
						if ((base.position.X + (float)(base.width / 2)) / 16f > (float)(doorX + 4) || (base.position.X + (float)(base.width / 2)) / 16f < (float)(doorX - 4) || (base.position.Y + (float)(base.height / 2)) / 16f > (float)(doorY + 4) || (base.position.Y + (float)(base.height / 2)) / 16f < (float)(doorY - 4))
						{
							closeDoor = false;
						}
					}
					if (velocity.X < -1f || velocity.X > 1f)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if ((double)velocity.X < 1.15 && direction == 1)
					{
						velocity.X += 0.07f;
						if (velocity.X > 1f)
						{
							velocity.X = 1f;
						}
					}
					else if (velocity.X > -1f && direction == -1)
					{
						velocity.X -= 0.07f;
						if (velocity.X > 1f)
						{
							velocity.X = 1f;
						}
					}
					if (velocity.Y != 0f)
					{
						return;
					}
					if (base.position.X == ai[2])
					{
						direction *= -1;
					}
					ai[2] = -1f;
					int num159 = (int)((base.position.X + (float)(base.width / 2) + (float)((base.width / 2 + 6) * direction)) / 16f);
					int num160 = (int)((base.position.Y + (float)base.height - 16f) / 16f);
					if (Main.tile[num159, num160] == null)
					{
						Main.tile[num159, num160] = new Tile();
					}
					if (Main.tile[num159, num160 - 1] == null)
					{
						Main.tile[num159, num160 - 1] = new Tile();
					}
					if (Main.tile[num159, num160 - 2] == null)
					{
						Main.tile[num159, num160 - 2] = new Tile();
					}
					if (Main.tile[num159, num160 - 3] == null)
					{
						Main.tile[num159, num160 - 3] = new Tile();
					}
					if (Main.tile[num159, num160 + 1] == null)
					{
						Main.tile[num159, num160 + 1] = new Tile();
					}
					if (Main.tile[num159 + direction, num160 - 1] == null)
					{
						Main.tile[num159 + direction, num160 - 1] = new Tile();
					}
					if (Main.tile[num159 + direction, num160 + 1] == null)
					{
						Main.tile[num159 + direction, num160 + 1] = new Tile();
					}
					if (townNPC && Main.tile[num159, num160 - 2].active && Main.tile[num159, num160 - 2].type == 10 && (Config.syncedRand.Next(10) == 0 || !Main.dayTime))
					{
						if (Main.netMode != 1)
						{
							if (WorldGen.OpenDoor(num159, num160 - 2, direction))
							{
								closeDoor = true;
								doorX = num159;
								doorY = num160 - 2;
								NetMessage.SendData(19, -1, -1, "", 0, num159, num160 - 2, direction);
								netUpdate = true;
								ai[1] += 80f;
							}
							else if (WorldGen.OpenDoor(num159, num160 - 2, -direction))
							{
								closeDoor = true;
								doorX = num159;
								doorY = num160 - 2;
								NetMessage.SendData(19, -1, -1, "", 0, num159, num160 - 2, 0f - (float)direction);
								netUpdate = true;
								ai[1] += 80f;
							}
							else
							{
								direction *= -1;
								netUpdate = true;
							}
						}
						return;
					}
					if ((velocity.X < 0f && spriteDirection == -1) || (velocity.X > 0f && spriteDirection == 1))
					{
						if (Main.tile[num159, num160 - 2].active && Main.tileSolid[Main.tile[num159, num160 - 2].type] && !Main.tileSolidTop[Main.tile[num159, num160 - 2].type])
						{
							if ((direction == 1 && !Collision.SolidTiles(num159 - 2, num159 - 1, num160 - 5, num160 - 1)) || (direction == -1 && !Collision.SolidTiles(num159 + 1, num159 + 2, num160 - 5, num160 - 1)))
							{
								if (!Collision.SolidTiles(num159, num159, num160 - 5, num160 - 3))
								{
									velocity.Y = -6f;
									netUpdate = true;
								}
								else
								{
									direction *= -1;
									netUpdate = true;
								}
							}
							else
							{
								direction *= -1;
								netUpdate = true;
							}
						}
						else if (Main.tile[num159, num160 - 1].active && Main.tileSolid[Main.tile[num159, num160 - 1].type] && !Main.tileSolidTop[Main.tile[num159, num160 - 1].type])
						{
							if ((direction == 1 && !Collision.SolidTiles(num159 - 2, num159 - 1, num160 - 4, num160 - 1)) || (direction == -1 && !Collision.SolidTiles(num159 + 1, num159 + 2, num160 - 4, num160 - 1)))
							{
								if (!Collision.SolidTiles(num159, num159, num160 - 4, num160 - 2))
								{
									velocity.Y = -5f;
									netUpdate = true;
								}
								else
								{
									direction *= -1;
									netUpdate = true;
								}
							}
							else
							{
								direction *= -1;
								netUpdate = true;
							}
						}
						else if (Main.tile[num159, num160].active && Main.tileSolid[Main.tile[num159, num160].type] && !Main.tileSolidTop[Main.tile[num159, num160].type])
						{
							if ((direction == 1 && !Collision.SolidTiles(num159 - 2, num159, num160 - 3, num160 - 1)) || (direction == -1 && !Collision.SolidTiles(num159, num159 + 2, num160 - 3, num160 - 1)))
							{
								velocity.Y = -3.6f;
								netUpdate = true;
							}
							else
							{
								direction *= -1;
								netUpdate = true;
							}
						}
						try
						{
							if (Main.tile[num159, num160 + 1] == null)
							{
								Main.tile[num159, num160 + 1] = new Tile();
							}
							if (Main.tile[num159 - direction, num160 + 1] == null)
							{
								Main.tile[num159 - direction, num160 + 1] = new Tile();
							}
							if (Main.tile[num159, num160 + 2] == null)
							{
								Main.tile[num159, num160 + 2] = new Tile();
							}
							if (Main.tile[num159 - direction, num160 + 2] == null)
							{
								Main.tile[num159 - direction, num160 + 2] = new Tile();
							}
							if (Main.tile[num159, num160 + 3] == null)
							{
								Main.tile[num159, num160 + 3] = new Tile();
							}
							if (Main.tile[num159 - direction, num160 + 3] == null)
							{
								Main.tile[num159 - direction, num160 + 3] = new Tile();
							}
							if (Main.tile[num159, num160 + 4] == null)
							{
								Main.tile[num159, num160 + 4] = new Tile();
							}
							if (Main.tile[num159 - direction, num160 + 4] == null)
							{
								Main.tile[num159 - direction, num160 + 4] = new Tile();
							}
							else if (num153 >= homeTileX - 35 && num153 <= homeTileX + 35 && (!Main.tile[num159, num160 + 1].active || !Main.tileSolid[Main.tile[num159, num160 + 1].type]) && (!Main.tile[num159 - direction, num160 + 1].active || !Main.tileSolid[Main.tile[num159 - direction, num160 + 1].type]) && (!Main.tile[num159, num160 + 2].active || !Main.tileSolid[Main.tile[num159, num160 + 2].type]) && (!Main.tile[num159 - direction, num160 + 2].active || !Main.tileSolid[Main.tile[num159 - direction, num160 + 2].type]) && (!Main.tile[num159, num160 + 3].active || !Main.tileSolid[Main.tile[num159, num160 + 3].type]) && (!Main.tile[num159 - direction, num160 + 3].active || !Main.tileSolid[Main.tile[num159 - direction, num160 + 3].type]) && (!Main.tile[num159, num160 + 4].active || !Main.tileSolid[Main.tile[num159, num160 + 4].type]) && (!Main.tile[num159 - direction, num160 + 4].active || !Main.tileSolid[Main.tile[num159 - direction, num160 + 4].type]) && type != 46)
							{
								direction *= -1;
								velocity.X *= -1f;
								netUpdate = true;
							}
						}
						catch
						{
						}
						if (velocity.Y < 0f)
						{
							ai[2] = base.position.X;
						}
					}
					if (velocity.Y < 0f && wet)
					{
						velocity.Y *= 1.2f;
					}
					if (velocity.Y < 0f && type == 46)
					{
						velocity.Y *= 1.2f;
					}
				}
			}
			else if (aiStyle == 8)
			{
				TargetClosest();
				velocity.X *= 0.93f;
				if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
				{
					velocity.X = 0f;
				}
				if (ai[0] == 0f)
				{
					ai[0] = 500f;
				}
				if (ai[2] != 0f && ai[3] != 0f)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
					for (int num161 = 0; num161 < 50; num161++)
					{
						if (type == 29 || type == 45)
						{
							Vector2 position13 = new Vector2(base.position.X, base.position.Y);
							int width13 = base.width;
							int height13 = base.height;
							int num162 = 27;
							float speedX9 = 0f;
							float speedY13 = 0f;
							int num163 = 100;
							int num164 = Dust.NewDust(position13, width13, height13, num162, speedX9, speedY13, num163, default(Color), Config.syncedRand.Next(1, 3));
							Dust dust16 = Main.dust[num164];
							dust16.velocity *= 3f;
							if (Main.dust[num164].scale > 1f)
							{
								Main.dust[num164].noGravity = true;
							}
						}
						else if (type == 32)
						{
							Vector2 position14 = new Vector2(base.position.X, base.position.Y);
							int width14 = base.width;
							int height14 = base.height;
							int num165 = 29;
							float speedX10 = 0f;
							float speedY14 = 0f;
							int num166 = 100;
							int num167 = Dust.NewDust(position14, width14, height14, num165, speedX10, speedY14, num166, default(Color), 2.5f);
							Dust dust17 = Main.dust[num167];
							dust17.velocity *= 3f;
							Main.dust[num167].noGravity = true;
						}
						else
						{
							Vector2 position15 = new Vector2(base.position.X, base.position.Y);
							int width15 = base.width;
							int height15 = base.height;
							int num168 = 6;
							float speedX11 = 0f;
							float speedY15 = 0f;
							int num169 = 100;
							int num170 = Dust.NewDust(position15, width15, height15, num168, speedX11, speedY15, num169, default(Color), 2.5f);
							Dust dust18 = Main.dust[num170];
							dust18.velocity *= 3f;
							Main.dust[num170].noGravity = true;
						}
					}
					base.position.X = ai[2] * 16f - (float)(base.width / 2) + 8f;
					base.position.Y = ai[3] * 16f - (float)base.height;
					velocity.X = 0f;
					velocity.Y = 0f;
					ai[2] = 0f;
					ai[3] = 0f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
					for (int num171 = 0; num171 < 50; num171++)
					{
						if (type == 29 || type == 45)
						{
							Vector2 position16 = new Vector2(base.position.X, base.position.Y);
							int width16 = base.width;
							int height16 = base.height;
							int num172 = 27;
							float speedX12 = 0f;
							float speedY16 = 0f;
							int num173 = 100;
							int num174 = Dust.NewDust(position16, width16, height16, num172, speedX12, speedY16, num173, default(Color), Config.syncedRand.Next(1, 3));
							Dust dust19 = Main.dust[num174];
							dust19.velocity *= 3f;
							if (Main.dust[num174].scale > 1f)
							{
								Main.dust[num174].noGravity = true;
							}
						}
						else if (type == 32)
						{
							Vector2 position17 = new Vector2(base.position.X, base.position.Y);
							int width17 = base.width;
							int height17 = base.height;
							int num175 = 29;
							float speedX13 = 0f;
							float speedY17 = 0f;
							int num176 = 100;
							int num177 = Dust.NewDust(position17, width17, height17, num175, speedX13, speedY17, num176, default(Color), 2.5f);
							Dust dust20 = Main.dust[num177];
							dust20.velocity *= 3f;
							Main.dust[num177].noGravity = true;
						}
						else
						{
							Vector2 position18 = new Vector2(base.position.X, base.position.Y);
							int width18 = base.width;
							int height18 = base.height;
							int num178 = 6;
							float speedX14 = 0f;
							float speedY18 = 0f;
							int num179 = 100;
							int num180 = Dust.NewDust(position18, width18, height18, num178, speedX14, speedY18, num179, default(Color), 2.5f);
							Dust dust21 = Main.dust[num180];
							dust21.velocity *= 3f;
							Main.dust[num180].noGravity = true;
						}
					}
				}
				ai[0] += 1f;
				if (ai[0] == 100f || ai[0] == 200f || ai[0] == 300f)
				{
					ai[1] = 30f;
					netUpdate = true;
				}
				else if (ai[0] >= 650f && Main.netMode != 1)
				{
					ai[0] = 1f;
					int num181 = (int)Main.player[target].position.X / 16;
					int num182 = (int)Main.player[target].position.Y / 16;
					int num183 = (int)base.position.X / 16;
					int num184 = (int)base.position.Y / 16;
					int num185 = 20;
					int num186 = 0;
					bool flag17 = false;
					if (Math.Abs(base.position.X - Main.player[target].position.X) + Math.Abs(base.position.Y - Main.player[target].position.Y) > 2000f)
					{
						num186 = 100;
						flag17 = true;
					}
					while (!flag17 && num186 < 100)
					{
						num186++;
						int num187 = Config.syncedRand.Next(num181 - num185, num181 + num185);
						int num188 = Config.syncedRand.Next(num182 - num185, num182 + num185);
						for (int num189 = num188; num189 < num182 + num185; num189++)
						{
							if ((num189 < num182 - 4 || num189 > num182 + 4 || num187 < num181 - 4 || num187 > num181 + 4) && (num189 < num184 - 1 || num189 > num184 + 1 || num187 < num183 - 1 || num187 > num183 + 1) && Main.tile[num187, num189].active)
							{
								bool flag18 = true;
								if (type == 32 && Main.tile[num187, num189 - 1].wall == 0)
								{
									flag18 = false;
								}
								else if (Main.tile[num187, num189 - 1].lava)
								{
									flag18 = false;
								}
								if (flag18 && Main.tileSolid[Main.tile[num187, num189].type] && !Collision.SolidTiles(num187 - 1, num187 + 1, num189 - 4, num189 - 1))
								{
									ai[1] = 20f;
									ai[2] = num187;
									ai[3] = num189;
									flag17 = true;
									break;
								}
							}
						}
					}
					netUpdate = true;
				}
				if (ai[1] > 0f)
				{
					ai[1] -= 1f;
					if (ai[1] == 25f)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
						if (Main.netMode != 1)
						{
							if (type == 29 || type == 45)
							{
								NewNPC((int)base.position.X + base.width / 2, (int)base.position.Y - 8, 30);
							}
							else if (type == 32)
							{
								NewNPC((int)base.position.X + base.width / 2, (int)base.position.Y - 8, 33);
							}
							else
							{
								NewNPC((int)base.position.X + base.width / 2 + direction * 8, (int)base.position.Y + 20, 25);
							}
						}
					}
				}
				if (type == 29 || type == 45)
				{
					if (Config.syncedRand.Next(5) == 0)
					{
						Vector2 position19 = new Vector2(base.position.X, base.position.Y + 2f);
						int width19 = base.width;
						int height19 = base.height;
						int num190 = 27;
						float speedX15 = velocity.X * 0.2f;
						float speedY19 = velocity.Y * 0.2f;
						int num191 = 100;
						int num192 = Dust.NewDust(position19, width19, height19, num190, speedX15, speedY19, num191, default(Color), 1.5f);
						Main.dust[num192].noGravity = true;
						Dust dust22 = Main.dust[num192];
						dust22.velocity.X = dust22.velocity.X * 0.5f;
						Main.dust[num192].velocity.Y = -2f;
					}
				}
				else if (type == 32)
				{
					if (Config.syncedRand.Next(2) == 0)
					{
						Vector2 position20 = new Vector2(base.position.X, base.position.Y + 2f);
						int width20 = base.width;
						int height20 = base.height;
						int num193 = 29;
						float speedX16 = velocity.X * 0.2f;
						float speedY20 = velocity.Y * 0.2f;
						int num194 = 100;
						int num195 = Dust.NewDust(position20, width20, height20, num193, speedX16, speedY20, num194, default(Color), 2f);
						Main.dust[num195].noGravity = true;
						Dust dust23 = Main.dust[num195];
						dust23.velocity.X = dust23.velocity.X * 1f;
						Dust dust24 = Main.dust[num195];
						dust24.velocity.Y = dust24.velocity.Y * 1f;
					}
				}
				else if (Config.syncedRand.Next(2) == 0)
				{
					Vector2 position21 = new Vector2(base.position.X, base.position.Y + 2f);
					int width21 = base.width;
					int height21 = base.height;
					int num196 = 6;
					float speedX17 = velocity.X * 0.2f;
					float speedY21 = velocity.Y * 0.2f;
					int num197 = 100;
					int num198 = Dust.NewDust(position21, width21, height21, num196, speedX17, speedY21, num197, default(Color), 2f);
					Main.dust[num198].noGravity = true;
					Dust dust25 = Main.dust[num198];
					dust25.velocity.X = dust25.velocity.X * 1f;
					Dust dust26 = Main.dust[num198];
					dust26.velocity.Y = dust26.velocity.Y * 1f;
				}
			}
			else if (aiStyle == 9)
			{
				if (target == 255)
				{
					TargetClosest();
					float num199 = 6f;
					if (type == 25)
					{
						num199 = 5f;
					}
					if (type == 112)
					{
						num199 = 7f;
					}
					Vector2 vector15 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num200 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector15.X;
					float num201 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector15.Y;
					float num202 = (float)Math.Sqrt(num200 * num200 + num201 * num201);
					num202 = num199 / num202;
					velocity.X = num200 * num202;
					velocity.Y = num201 * num202;
				}
				if (type == 112)
				{
					ai[0] += 1f;
					if (ai[0] > 3f)
					{
						ai[0] = 3f;
					}
					if (ai[0] == 2f)
					{
						base.position += velocity;
						Main.PlaySound(4, (int)base.position.X, (int)base.position.Y, 9);
						for (int num203 = 0; num203 < 20; num203++)
						{
							Vector2 position22 = new Vector2(base.position.X, base.position.Y + 2f);
							int width22 = base.width;
							int height22 = base.height;
							int num204 = 18;
							float speedX18 = 0f;
							float speedY22 = 0f;
							int num205 = 100;
							int num206 = Dust.NewDust(position22, width22, height22, num204, speedX18, speedY22, num205, default(Color), 1.8f);
							Dust dust27 = Main.dust[num206];
							dust27.velocity *= 1.3f;
							Dust dust28 = Main.dust[num206];
							dust28.velocity += velocity;
							Main.dust[num206].noGravity = true;
						}
					}
				}
				if (type == 112 && Collision.SolidCollision(base.position, base.width, base.height))
				{
					if (Main.netMode != 1)
					{
						int num207 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int num208 = (int)(base.position.Y + (float)(base.height / 2)) / 16;
						int num209 = 8;
						for (int num210 = num207 - num209; num210 <= num207 + num209; num210++)
						{
							for (int num211 = num208 - num209; num211 < num208 + num209; num211++)
							{
								if (!((double)(Math.Abs(num210 - num207) + Math.Abs(num211 - num208)) < (double)num209 * 0.5))
								{
									continue;
								}
								if (Main.tile[num210, num211].type == 2)
								{
									Main.tile[num210, num211].type = 23;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
								else if (Main.tile[num210, num211].type == 1)
								{
									Main.tile[num210, num211].type = 25;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
								else if (Main.tile[num210, num211].type == 53)
								{
									Main.tile[num210, num211].type = 112;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
								else if (Main.tile[num210, num211].type == 109)
								{
									Main.tile[num210, num211].type = 23;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
								else if (Main.tile[num210, num211].type == 117)
								{
									Main.tile[num210, num211].type = 25;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
								else if (Main.tile[num210, num211].type == 116)
								{
									Main.tile[num210, num211].type = 112;
									WorldGen.SquareTileFrame(num210, num211);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num210, num211, 1);
									}
								}
							}
						}
					}
					StrikeNPC(999, 0f, 0);
				}
				if (timeLeft > 100)
				{
					timeLeft = 100;
				}
				for (int num212 = 0; num212 < 2; num212++)
				{
					if (type == 30)
					{
						Vector2 position23 = new Vector2(base.position.X, base.position.Y + 2f);
						int width23 = base.width;
						int height23 = base.height;
						int num213 = 27;
						float speedX19 = velocity.X * 0.2f;
						float speedY23 = velocity.Y * 0.2f;
						int num214 = 100;
						int num215 = Dust.NewDust(position23, width23, height23, num213, speedX19, speedY23, num214, default(Color), 2f);
						Main.dust[num215].noGravity = true;
						Dust dust29 = Main.dust[num215];
						dust29.velocity *= 0.3f;
						Dust dust30 = Main.dust[num215];
						dust30.velocity.X = dust30.velocity.X - velocity.X * 0.2f;
						Dust dust31 = Main.dust[num215];
						dust31.velocity.Y = dust31.velocity.Y - velocity.Y * 0.2f;
					}
					else if (type == 33)
					{
						Vector2 position24 = new Vector2(base.position.X, base.position.Y + 2f);
						int width24 = base.width;
						int height24 = base.height;
						int num216 = 29;
						float speedX20 = velocity.X * 0.2f;
						float speedY24 = velocity.Y * 0.2f;
						int num217 = 100;
						int num218 = Dust.NewDust(position24, width24, height24, num216, speedX20, speedY24, num217, default(Color), 2f);
						Main.dust[num218].noGravity = true;
						Dust dust32 = Main.dust[num218];
						dust32.velocity.X = dust32.velocity.X * 0.3f;
						Dust dust33 = Main.dust[num218];
						dust33.velocity.Y = dust33.velocity.Y * 0.3f;
					}
					else if (type == 112)
					{
						Vector2 position25 = new Vector2(base.position.X, base.position.Y + 2f);
						int width25 = base.width;
						int height25 = base.height;
						int num219 = 18;
						float speedX21 = velocity.X * 0.1f;
						float speedY25 = velocity.Y * 0.1f;
						int num220 = 80;
						int num221 = Dust.NewDust(position25, width25, height25, num219, speedX21, speedY25, num220, default(Color), 1.3f);
						Dust dust34 = Main.dust[num221];
						dust34.velocity *= 0.3f;
						Main.dust[num221].noGravity = true;
					}
					else
					{
						Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 1f, 0.3f, 0.1f);
						Vector2 position26 = new Vector2(base.position.X, base.position.Y + 2f);
						int width26 = base.width;
						int height26 = base.height;
						int num222 = 6;
						float speedX22 = velocity.X * 0.2f;
						float speedY26 = velocity.Y * 0.2f;
						int num223 = 100;
						int num224 = Dust.NewDust(position26, width26, height26, num222, speedX22, speedY26, num223, default(Color), 2f);
						Main.dust[num224].noGravity = true;
						Dust dust35 = Main.dust[num224];
						dust35.velocity.X = dust35.velocity.X * 0.3f;
						Dust dust36 = Main.dust[num224];
						dust36.velocity.Y = dust36.velocity.Y * 0.3f;
					}
				}
				rotation += 0.4f * (float)direction;
			}
			else if (aiStyle == 10)
			{
				float num225 = 1f;
				float num226 = 0.011f;
				TargetClosest();
				Vector2 vector16 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num227 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector16.X;
				float num228 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector16.Y;
				float num229 = (float)Math.Sqrt(num227 * num227 + num228 * num228);
				float num230 = num229;
				ai[1] += 1f;
				if (ai[1] > 600f)
				{
					num226 *= 8f;
					num225 = 4f;
					if (ai[1] > 650f)
					{
						ai[1] = 0f;
					}
				}
				else if (num230 < 250f)
				{
					ai[0] += 0.9f;
					if (ai[0] > 0f)
					{
						velocity.Y += 0.019f;
					}
					else
					{
						velocity.Y -= 0.019f;
					}
					if (ai[0] < -100f || ai[0] > 100f)
					{
						velocity.X += 0.019f;
					}
					else
					{
						velocity.X -= 0.019f;
					}
					if (ai[0] > 200f)
					{
						ai[0] = -200f;
					}
				}
				if (num230 > 350f)
				{
					num225 = 5f;
					num226 = 0.3f;
				}
				else if (num230 > 300f)
				{
					num225 = 3f;
					num226 = 0.2f;
				}
				else if (num230 > 250f)
				{
					num225 = 1.5f;
					num226 = 0.1f;
				}
				num229 = num225 / num229;
				num227 *= num229;
				num228 *= num229;
				if (Main.player[target].dead)
				{
					num227 = (float)direction * num225 / 2f;
					num228 = (0f - num225) / 2f;
				}
				if (velocity.X < num227)
				{
					velocity.X += num226;
				}
				else if (velocity.X > num227)
				{
					velocity.X -= num226;
				}
				if (velocity.Y < num228)
				{
					velocity.Y += num226;
				}
				else if (velocity.Y > num228)
				{
					velocity.Y -= num226;
				}
				if (num227 > 0f)
				{
					spriteDirection = -1;
					rotation = (float)Math.Atan2(num228, num227);
				}
				if (num227 < 0f)
				{
					spriteDirection = 1;
					rotation = (float)Math.Atan2(num228, num227) + 3.14f;
				}
			}
			else if (aiStyle == 11)
			{
				if (ai[0] == 0f && Main.netMode != 1)
				{
					TargetClosest();
					ai[0] = 1f;
					if (type != 68)
					{
						int num231 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 36, whoAmI);
						Main.npc[num231].ai[0] = -1f;
						Main.npc[num231].ai[1] = whoAmI;
						Main.npc[num231].target = target;
						Main.npc[num231].netUpdate = true;
						num231 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 36, whoAmI);
						Main.npc[num231].ai[0] = 1f;
						Main.npc[num231].ai[1] = whoAmI;
						Main.npc[num231].ai[3] = 150f;
						Main.npc[num231].target = target;
						Main.npc[num231].netUpdate = true;
					}
				}
				if (type == 68 && ai[1] != 3f && ai[1] != 2f)
				{
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					ai[1] = 2f;
				}
				if (Main.player[target].dead || Math.Abs(base.position.X - Main.player[target].position.X) > 2000f || Math.Abs(base.position.Y - Main.player[target].position.Y) > 2000f)
				{
					TargetClosest();
					if (Main.player[target].dead || Math.Abs(base.position.X - Main.player[target].position.X) > 2000f || Math.Abs(base.position.Y - Main.player[target].position.Y) > 2000f)
					{
						ai[1] = 3f;
					}
				}
				if (Main.dayTime && ai[1] != 3f && ai[1] != 2f)
				{
					ai[1] = 2f;
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
				}
				if (ai[1] == 0f)
				{
					defense = 10;
					ai[2] += 1f;
					if (ai[2] >= 800f)
					{
						ai[2] = 0f;
						ai[1] = 1f;
						TargetClosest();
						netUpdate = true;
					}
					rotation = velocity.X / 15f;
					if (base.position.Y > Main.player[target].position.Y - 250f)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.98f;
						}
						velocity.Y -= 0.02f;
						if (velocity.Y > 2f)
						{
							velocity.Y = 2f;
						}
					}
					else if (base.position.Y < Main.player[target].position.Y - 250f)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.98f;
						}
						velocity.Y += 0.02f;
						if (velocity.Y < -2f)
						{
							velocity.Y = -2f;
						}
					}
					if (base.position.X + (float)(base.width / 2) > Main.player[target].position.X + (float)(Main.player[target].width / 2))
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.98f;
						}
						velocity.X -= 0.05f;
						if (velocity.X > 8f)
						{
							velocity.X = 8f;
						}
					}
					if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)(Main.player[target].width / 2))
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.98f;
						}
						velocity.X += 0.05f;
						if (velocity.X < -8f)
						{
							velocity.X = -8f;
						}
					}
				}
				else if (ai[1] == 1f)
				{
					defense = 0;
					ai[2] += 1f;
					if (ai[2] == 2f)
					{
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					}
					if (ai[2] >= 400f)
					{
						ai[2] = 0f;
						ai[1] = 0f;
					}
					rotation += (float)direction * 0.3f;
					Vector2 vector17 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num232 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector17.X;
					float num233 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector17.Y;
					float num234 = (float)Math.Sqrt(num232 * num232 + num233 * num233);
					num234 = 1.5f / num234;
					velocity.X = num232 * num234;
					velocity.Y = num233 * num234;
				}
				else if (ai[1] == 2f)
				{
					damage = 9999;
					defense = 9999;
					rotation += (float)direction * 0.3f;
					Vector2 vector18 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num235 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector18.X;
					float num236 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector18.Y;
					float num237 = (float)Math.Sqrt(num235 * num235 + num236 * num236);
					num237 = 8f / num237;
					velocity.X = num235 * num237;
					velocity.Y = num236 * num237;
				}
				else if (ai[1] == 3f)
				{
					velocity.Y += 0.1f;
					if (velocity.Y < 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.X *= 0.95f;
					if (timeLeft > 500)
					{
						timeLeft = 500;
					}
				}
				if (ai[1] != 2f && ai[1] != 3f && type != 68)
				{
					Vector2 position27 = new Vector2(base.position.X + (float)(base.width / 2) - 15f - velocity.X * 5f, base.position.Y + (float)base.height - 2f);
					int width27 = 30;
					int height27 = 10;
					int num238 = 5;
					float speedX23 = (0f - velocity.X) * 0.2f;
					float speedY27 = 3f;
					int num239 = 0;
					int num240 = Dust.NewDust(position27, width27, height27, num238, speedX23, speedY27, num239, default(Color), 2f);
					Main.dust[num240].noGravity = true;
					Dust dust37 = Main.dust[num240];
					dust37.velocity.X = dust37.velocity.X * 1.3f;
					Dust dust38 = Main.dust[num240];
					dust38.velocity.X = dust38.velocity.X + velocity.X * 0.4f;
					Dust dust39 = Main.dust[num240];
					dust39.velocity.Y = dust39.velocity.Y + (2f + velocity.Y);
					for (int num241 = 0; num241 < 2; num241++)
					{
						Vector2 position28 = new Vector2(base.position.X, base.position.Y + 120f);
						int width28 = base.width;
						int height28 = 60;
						int num242 = 5;
						float x5 = velocity.X;
						float y = velocity.Y;
						int num243 = 0;
						num240 = Dust.NewDust(position28, width28, height28, num242, x5, y, num243, default(Color), 2f);
						Main.dust[num240].noGravity = true;
						Dust dust40 = Main.dust[num240];
						dust40.velocity -= velocity;
						Dust dust41 = Main.dust[num240];
						dust41.velocity.Y = dust41.velocity.Y + 5f;
					}
				}
			}
			else if (aiStyle == 12)
			{
				spriteDirection = -(int)ai[0];
				if (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != 11)
				{
					ai[2] += 10f;
					if (ai[2] > 50f || Main.netMode != 2)
					{
						life = -1;
						HitEffect();
						active = false;
					}
				}
				if (ai[2] == 0f || ai[2] == 3f)
				{
					if (Main.npc[(int)ai[1]].ai[1] == 3f && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (Main.npc[(int)ai[1]].ai[1] != 0f)
					{
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.07f;
							if (velocity.Y > 6f)
							{
								velocity.Y = 6f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.07f;
							if (velocity.Y < -6f)
							{
								velocity.Y = -6f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.1f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.1f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					else
					{
						ai[3] += 1f;
						if (ai[3] >= 300f)
						{
							ai[2] += 1f;
							ai[3] = 0f;
							netUpdate = true;
						}
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y + 230f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.04f;
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y + 230f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.04f;
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0])
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.07f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0])
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.07f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					Vector2 vector19 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num244 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector19.X;
					float num245 = Main.npc[(int)ai[1]].position.Y + 230f - vector19.Y;
					Math.Sqrt(num244 * num244 + num245 * num245);
					rotation = (float)Math.Atan2(num245, num244) + 1.57f;
				}
				else if (ai[2] == 1f)
				{
					Vector2 vector20 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num246 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector20.X;
					float num247 = Main.npc[(int)ai[1]].position.Y + 230f - vector20.Y;
					float num248 = (float)Math.Sqrt(num246 * num246 + num247 * num247);
					rotation = (float)Math.Atan2(num247, num246) + 1.57f;
					velocity.X *= 0.95f;
					velocity.Y -= 0.1f;
					if (velocity.Y < -8f)
					{
						velocity.Y = -8f;
					}
					if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 200f)
					{
						TargetClosest();
						ai[2] = 2f;
						vector20 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num246 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector20.X;
						num247 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector20.Y;
						num248 = (float)Math.Sqrt(num246 * num246 + num247 * num247);
						num248 = 18f / num248;
						velocity.X = num246 * num248;
						velocity.Y = num247 * num248;
						netUpdate = true;
					}
				}
				else if (ai[2] == 2f)
				{
					if (base.position.Y > Main.player[target].position.Y || velocity.Y < 0f)
					{
						ai[2] = 3f;
					}
				}
				else if (ai[2] == 4f)
				{
					Vector2 vector21 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num249 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector21.X;
					float num250 = Main.npc[(int)ai[1]].position.Y + 230f - vector21.Y;
					float num251 = (float)Math.Sqrt(num249 * num249 + num250 * num250);
					rotation = (float)Math.Atan2(num250, num249) + 1.57f;
					velocity.Y *= 0.95f;
					velocity.X += 0.1f * (0f - ai[0]);
					if (velocity.X < -8f)
					{
						velocity.X = -8f;
					}
					if (velocity.X > 8f)
					{
						velocity.X = 8f;
					}
					if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 500f || base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) + 500f)
					{
						TargetClosest();
						ai[2] = 5f;
						vector21 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num249 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector21.X;
						num250 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector21.Y;
						num251 = (float)Math.Sqrt(num249 * num249 + num250 * num250);
						num251 = 17f / num251;
						velocity.X = num249 * num251;
						velocity.Y = num250 * num251;
						netUpdate = true;
					}
				}
				else if (ai[2] == 5f && ((velocity.X > 0f && base.position.X + (float)(base.width / 2) > Main.player[target].position.X + (float)(Main.player[target].width / 2)) || (velocity.X < 0f && base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)(Main.player[target].width / 2))))
				{
					ai[2] = 0f;
				}
			}
			else if (aiStyle == 13)
			{
				if (Main.tile[(int)ai[0], (int)ai[1]] == null)
				{
					Main.tile[(int)ai[0], (int)ai[1]] = new Tile();
				}
				if (!Main.tile[(int)ai[0], (int)ai[1]].active)
				{
					life = -1;
					HitEffect();
					active = false;
					return;
				}
				TargetClosest();
				float num252 = 0.035f;
				float num253 = 150f;
				if (type == 43)
				{
					num253 = 250f;
				}
				if (type == 101)
				{
					num253 = 175f;
				}
				ai[2] += 1f;
				if (ai[2] > 300f)
				{
					num253 = (int)((double)num253 * 1.3);
					if (ai[2] > 450f)
					{
						ai[2] = 0f;
					}
				}
				Vector2 vector22 = new Vector2(ai[0] * 16f + 8f, ai[1] * 16f + 8f);
				float num254 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - (float)(base.width / 2) - vector22.X;
				float num255 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - (float)(base.height / 2) - vector22.Y;
				float num256 = (float)Math.Sqrt(num254 * num254 + num255 * num255);
				if (num256 > num253)
				{
					num256 = num253 / num256;
					num254 *= num256;
					num255 *= num256;
				}
				if (base.position.X < ai[0] * 16f + 8f + num254)
				{
					velocity.X += num252;
					if (velocity.X < 0f && num254 > 0f)
					{
						velocity.X += num252 * 1.5f;
					}
				}
				else if (base.position.X > ai[0] * 16f + 8f + num254)
				{
					velocity.X -= num252;
					if (velocity.X > 0f && num254 < 0f)
					{
						velocity.X -= num252 * 1.5f;
					}
				}
				if (base.position.Y < ai[1] * 16f + 8f + num255)
				{
					velocity.Y += num252;
					if (velocity.Y < 0f && num255 > 0f)
					{
						velocity.Y += num252 * 1.5f;
					}
				}
				else if (base.position.Y > ai[1] * 16f + 8f + num255)
				{
					velocity.Y -= num252;
					if (velocity.Y > 0f && num255 < 0f)
					{
						velocity.Y -= num252 * 1.5f;
					}
				}
				if (type == 43)
				{
					if (velocity.X > 3f)
					{
						velocity.X = 3f;
					}
					if (velocity.X < -3f)
					{
						velocity.X = -3f;
					}
					if (velocity.Y > 3f)
					{
						velocity.Y = 3f;
					}
					if (velocity.Y < -3f)
					{
						velocity.Y = -3f;
					}
				}
				else
				{
					if (velocity.X > 2f)
					{
						velocity.X = 2f;
					}
					if (velocity.X < -2f)
					{
						velocity.X = -2f;
					}
					if (velocity.Y > 2f)
					{
						velocity.Y = 2f;
					}
					if (velocity.Y < -2f)
					{
						velocity.Y = -2f;
					}
				}
				if (num254 > 0f)
				{
					spriteDirection = 1;
					rotation = (float)Math.Atan2(num255, num254);
				}
				if (num254 < 0f)
				{
					spriteDirection = -1;
					rotation = (float)Math.Atan2(num255, num254) + 3.14f;
				}
				if (collideX)
				{
					netUpdate = true;
					velocity.X = oldVelocity.X * -0.7f;
					if (velocity.X > 0f && velocity.X < 2f)
					{
						velocity.X = 2f;
					}
					if (velocity.X < 0f && velocity.X > -2f)
					{
						velocity.X = -2f;
					}
				}
				if (collideY)
				{
					netUpdate = true;
					velocity.Y = oldVelocity.Y * -0.7f;
					if (velocity.Y > 0f && velocity.Y < 2f)
					{
						velocity.Y = 2f;
					}
					if (velocity.Y < 0f && velocity.Y > -2f)
					{
						velocity.Y = -2f;
					}
				}
				if (Main.netMode == 1 || type != 101 || Main.player[target].dead)
				{
					return;
				}
				if (justHit)
				{
					localAI[0] = 0f;
				}
				localAI[0] += 1f;
				if (localAI[0] >= 120f)
				{
					if (!Collision.SolidCollision(base.position, base.width, base.height) && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						float num257 = 10f;
						vector22 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num254 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector22.X + (float)Config.syncedRand.Next(-10, 11);
						num255 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector22.Y + (float)Config.syncedRand.Next(-10, 11);
						num256 = (float)Math.Sqrt(num254 * num254 + num255 * num255);
						num256 = num257 / num256;
						num254 *= num256;
						num255 *= num256;
						int num258 = 22;
						int num259 = 96;
						int num260 = Projectile.NewProjectile(vector22.X, vector22.Y, num254, num255, num259, num258, 0f, Main.myPlayer);
						Main.projectile[num260].timeLeft = 300;
						localAI[0] = 0f;
					}
					else
					{
						localAI[0] = 100f;
					}
				}
			}
			else if (aiStyle == 14)
			{
				if (type == 60)
				{
					Vector2 position29 = new Vector2(base.position.X, base.position.Y);
					int width29 = base.width;
					int height29 = base.height;
					int num261 = 6;
					float speedX24 = velocity.X * 0.2f;
					float speedY28 = velocity.Y * 0.2f;
					int num262 = 100;
					int num263 = Dust.NewDust(position29, width29, height29, num261, speedX24, speedY28, num262, default(Color), 2f);
					Main.dust[num263].noGravity = true;
				}
				noGravity = true;
				if (collideX)
				{
					velocity.X = oldVelocity.X * -0.5f;
					if (direction == -1 && velocity.X > 0f && velocity.X < 2f)
					{
						velocity.X = 2f;
					}
					if (direction == 1 && velocity.X < 0f && velocity.X > -2f)
					{
						velocity.X = -2f;
					}
				}
				if (collideY)
				{
					velocity.Y = oldVelocity.Y * -0.5f;
					if (velocity.Y > 0f && velocity.Y < 1f)
					{
						velocity.Y = 1f;
					}
					if (velocity.Y < 0f && velocity.Y > -1f)
					{
						velocity.Y = -1f;
					}
				}
				TargetClosest();
				if (direction == -1 && velocity.X > -4f)
				{
					velocity.X -= 0.1f;
					if (velocity.X > 4f)
					{
						velocity.X -= 0.1f;
					}
					else if (velocity.X > 0f)
					{
						velocity.X += 0.05f;
					}
					if (velocity.X < -4f)
					{
						velocity.X = -4f;
					}
				}
				else if (direction == 1 && velocity.X < 4f)
				{
					velocity.X += 0.1f;
					if (velocity.X < -4f)
					{
						velocity.X += 0.1f;
					}
					else if (velocity.X < 0f)
					{
						velocity.X -= 0.05f;
					}
					if (velocity.X > 4f)
					{
						velocity.X = 4f;
					}
				}
				if (base.directionY == -1 && (double)velocity.Y > -1.5)
				{
					velocity.Y -= 0.04f;
					if ((double)velocity.Y > 1.5)
					{
						velocity.Y -= 0.05f;
					}
					else if (velocity.Y > 0f)
					{
						velocity.Y += 0.03f;
					}
					if ((double)velocity.Y < -1.5)
					{
						velocity.Y = -1.5f;
					}
				}
				else if (base.directionY == 1 && (double)velocity.Y < 1.5)
				{
					velocity.Y += 0.04f;
					if ((double)velocity.Y < -1.5)
					{
						velocity.Y += 0.05f;
					}
					else if (velocity.Y < 0f)
					{
						velocity.Y -= 0.03f;
					}
					if ((double)velocity.Y > 1.5)
					{
						velocity.Y = 1.5f;
					}
				}
				if (type == 49 || type == 51 || type == 60 || type == 62 || type == 66 || type == 93 || type == 137)
				{
					if (wet)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.95f;
						}
						velocity.Y -= 0.5f;
						if (velocity.Y < -4f)
						{
							velocity.Y = -4f;
						}
						TargetClosest();
					}
					if (type == 60)
					{
						if (direction == -1 && velocity.X > -4f)
						{
							velocity.X -= 0.1f;
							if (velocity.X > 4f)
							{
								velocity.X -= 0.07f;
							}
							else if (velocity.X > 0f)
							{
								velocity.X += 0.03f;
							}
							if (velocity.X < -4f)
							{
								velocity.X = -4f;
							}
						}
						else if (direction == 1 && velocity.X < 4f)
						{
							velocity.X += 0.1f;
							if (velocity.X < -4f)
							{
								velocity.X += 0.07f;
							}
							else if (velocity.X < 0f)
							{
								velocity.X -= 0.03f;
							}
							if (velocity.X > 4f)
							{
								velocity.X = 4f;
							}
						}
						if (base.directionY == -1 && (double)velocity.Y > -1.5)
						{
							velocity.Y -= 0.04f;
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y -= 0.03f;
							}
							else if (velocity.Y > 0f)
							{
								velocity.Y += 0.02f;
							}
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y = -1.5f;
							}
						}
						else if (base.directionY == 1 && (double)velocity.Y < 1.5)
						{
							velocity.Y += 0.04f;
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y += 0.03f;
							}
							else if (velocity.Y < 0f)
							{
								velocity.Y -= 0.02f;
							}
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y = 1.5f;
							}
						}
					}
					else
					{
						if (direction == -1 && velocity.X > -4f)
						{
							velocity.X -= 0.1f;
							if (velocity.X > 4f)
							{
								velocity.X -= 0.1f;
							}
							else if (velocity.X > 0f)
							{
								velocity.X += 0.05f;
							}
							if (velocity.X < -4f)
							{
								velocity.X = -4f;
							}
						}
						else if (direction == 1 && velocity.X < 4f)
						{
							velocity.X += 0.1f;
							if (velocity.X < -4f)
							{
								velocity.X += 0.1f;
							}
							else if (velocity.X < 0f)
							{
								velocity.X -= 0.05f;
							}
							if (velocity.X > 4f)
							{
								velocity.X = 4f;
							}
						}
						if (base.directionY == -1 && (double)velocity.Y > -1.5)
						{
							velocity.Y -= 0.04f;
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y -= 0.05f;
							}
							else if (velocity.Y > 0f)
							{
								velocity.Y += 0.03f;
							}
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y = -1.5f;
							}
						}
						else if (base.directionY == 1 && (double)velocity.Y < 1.5)
						{
							velocity.Y += 0.04f;
							if ((double)velocity.Y < -1.5)
							{
								velocity.Y += 0.05f;
							}
							else if (velocity.Y < 0f)
							{
								velocity.Y -= 0.03f;
							}
							if ((double)velocity.Y > 1.5)
							{
								velocity.Y = 1.5f;
							}
						}
					}
				}
				ai[1] += 1f;
				if (ai[1] > 200f)
				{
					if (!Main.player[target].wet && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						ai[1] = 0f;
					}
					float num264 = 0.2f;
					float num265 = 0.1f;
					float num266 = 4f;
					float num267 = 1.5f;
					if (type == 48 || type == 62 || type == 66)
					{
						num264 = 0.12f;
						num265 = 0.07f;
						num266 = 3f;
						num267 = 1.25f;
					}
					if (ai[1] > 1000f)
					{
						ai[1] = 0f;
					}
					ai[2] += 1f;
					if (ai[2] > 0f)
					{
						if (velocity.Y < num267)
						{
							velocity.Y += num265;
						}
					}
					else if (velocity.Y > 0f - num267)
					{
						velocity.Y -= num265;
					}
					if (ai[2] < -150f || ai[2] > 150f)
					{
						if (velocity.X < num266)
						{
							velocity.X += num264;
						}
					}
					else if (velocity.X > 0f - num266)
					{
						velocity.X -= num264;
					}
					if (ai[2] > 300f)
					{
						ai[2] = -300f;
					}
				}
				if (Main.netMode == 1)
				{
					return;
				}
				if (type == 48)
				{
					ai[0] += 1f;
					if (ai[0] == 30f || ai[0] == 60f || ai[0] == 90f)
					{
						if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							float num268 = 6f;
							Vector2 vector23 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
							float num269 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector23.X + (float)Config.syncedRand.Next(-100, 101);
							float num270 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector23.Y + (float)Config.syncedRand.Next(-100, 101);
							float num271 = (float)Math.Sqrt(num269 * num269 + num270 * num270);
							num271 = num268 / num271;
							num269 *= num271;
							num270 *= num271;
							int num272 = 15;
							int num273 = 38;
							int num274 = Projectile.NewProjectile(vector23.X, vector23.Y, num269, num270, num273, num272, 0f, Main.myPlayer);
							Main.projectile[num274].timeLeft = 300;
						}
					}
					else if (ai[0] >= (float)(400 + Config.syncedRand.Next(400)))
					{
						ai[0] = 0f;
					}
				}
				if (type != 62 && type != 66)
				{
					return;
				}
				ai[0] += 1f;
				if (ai[0] == 20f || ai[0] == 40f || ai[0] == 60f || ai[0] == 80f)
				{
					if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						float num275 = 0.2f;
						Vector2 vector24 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num276 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector24.X + (float)Config.syncedRand.Next(-100, 101);
						float num277 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector24.Y + (float)Config.syncedRand.Next(-100, 101);
						float num278 = (float)Math.Sqrt(num276 * num276 + num277 * num277);
						num278 = num275 / num278;
						num276 *= num278;
						num277 *= num278;
						int num279 = 21;
						int num280 = 44;
						int num281 = Projectile.NewProjectile(vector24.X, vector24.Y, num276, num277, num280, num279, 0f, Main.myPlayer);
						Main.projectile[num281].timeLeft = 300;
					}
				}
				else if (ai[0] >= (float)(300 + Config.syncedRand.Next(300)))
				{
					ai[0] = 0f;
				}
			}
			else if (aiStyle == 15)
			{
				aiAction = 0;
				if (ai[3] == 0f && life > 0)
				{
					ai[3] = lifeMax;
				}
				if (ai[2] == 0f)
				{
					ai[0] = -100f;
					ai[2] = 1f;
					TargetClosest();
				}
				if (velocity.Y == 0f)
				{
					velocity.X *= 0.8f;
					if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
					{
						velocity.X = 0f;
					}
					ai[0] += 2f;
					if ((double)life < (double)lifeMax * 0.8)
					{
						ai[0] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.6)
					{
						ai[0] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.4)
					{
						ai[0] += 2f;
					}
					if ((double)life < (double)lifeMax * 0.2)
					{
						ai[0] += 3f;
					}
					if ((double)life < (double)lifeMax * 0.1)
					{
						ai[0] += 4f;
					}
					if (ai[0] >= 0f)
					{
						netUpdate = true;
						TargetClosest();
						if (ai[1] == 3f)
						{
							velocity.Y = -13f;
							velocity.X += 3.5f * (float)direction;
							ai[0] = -200f;
							ai[1] = 0f;
						}
						else if (ai[1] == 2f)
						{
							velocity.Y = -6f;
							velocity.X += 4.5f * (float)direction;
							ai[0] = -120f;
							ai[1] += 1f;
						}
						else
						{
							velocity.Y = -8f;
							velocity.X += 4f * (float)direction;
							ai[0] = -120f;
							ai[1] += 1f;
						}
					}
					else if (ai[0] >= -30f)
					{
						aiAction = 1;
					}
				}
				else if (target < 255 && ((direction == 1 && velocity.X < 3f) || (direction == -1 && velocity.X > -3f)))
				{
					if ((direction == -1 && (double)velocity.X < 0.1) || (direction == 1 && (double)velocity.X > -0.1))
					{
						velocity.X += 0.2f * (float)direction;
					}
					else
					{
						velocity.X *= 0.93f;
					}
				}
				int num282 = Dust.NewDust(base.position, base.width, base.height, 4, velocity.X, velocity.Y, 255, new Color(0, 80, 255, 80), scale * 1.2f);
				Main.dust[num282].noGravity = true;
				Dust dust42 = Main.dust[num282];
				dust42.velocity *= 0.5f;
				if (life <= 0)
				{
					return;
				}
				float num283 = (float)life / (float)lifeMax;
				num283 = num283 * 0.5f + 0.75f;
				if (num283 != scale)
				{
					base.position.X = base.position.X + (float)(base.width / 2);
					base.position.Y = base.position.Y + (float)base.height;
					scale = num283;
					base.width = (int)(98f * scale);
					base.height = (int)(92f * scale);
					base.position.X = base.position.X - (float)(base.width / 2);
					base.position.Y = base.position.Y - (float)base.height;
				}
				if (Main.netMode == 1)
				{
					return;
				}
				int num284 = (int)((double)lifeMax * 0.05);
				if (!((float)(life + num284) < ai[3]))
				{
					return;
				}
				ai[3] = life;
				int num285 = Config.syncedRand.Next(1, 4);
				for (int num286 = 0; num286 < num285; num286++)
				{
					int x6 = (int)(base.position.X + (float)Config.syncedRand.Next(base.width - 32));
					int y2 = (int)(base.position.Y + (float)Config.syncedRand.Next(base.height - 32));
					int num287 = NewNPC(x6, y2, 1);
					Main.npc[num287].SetDefaults(1);
					Main.npc[num287].velocity.X = (float)Config.syncedRand.Next(-15, 16) * 0.1f;
					Main.npc[num287].velocity.Y = (float)Config.syncedRand.Next(-30, 1) * 0.1f;
					Main.npc[num287].ai[1] = Config.syncedRand.Next(3);
					if (Main.netMode == 2 && num287 < 200)
					{
						NetMessage.SendData(23, -1, -1, "", num287);
					}
				}
			}
			else if (aiStyle == 16)
			{
				if (direction == 0)
				{
					TargetClosest();
				}
				if (wet)
				{
					bool flag19 = false;
					if (type != 55)
					{
						TargetClosest(faceTarget: false);
						if (Main.player[target].wet && !Main.player[target].dead)
						{
							flag19 = true;
						}
					}
					if (!flag19)
					{
						if (collideX)
						{
							velocity.X *= -1f;
							direction *= -1;
							netUpdate = true;
						}
						if (collideY)
						{
							netUpdate = true;
							if (velocity.Y > 0f)
							{
								velocity.Y = Math.Abs(velocity.Y) * -1f;
								base.directionY = -1;
								ai[0] = -1f;
							}
							else if (velocity.Y < 0f)
							{
								velocity.Y = Math.Abs(velocity.Y);
								base.directionY = 1;
								ai[0] = 1f;
							}
						}
					}
					if (type == 102)
					{
						Lighting.addLight((int)(base.position.X + (float)(base.width / 2) + (float)(direction * (base.width + 8))) / 16, (int)(base.position.Y + 2f) / 16, 0.07f, 0.04f, 0.025f);
					}
					if (flag19)
					{
						TargetClosest();
						if (type == 65 || type == 102)
						{
							velocity.X += (float)direction * 0.15f;
							velocity.Y += (float)base.directionY * 0.15f;
							if (velocity.X > 5f)
							{
								velocity.X = 5f;
							}
							if (velocity.X < -5f)
							{
								velocity.X = -5f;
							}
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						else
						{
							velocity.X += (float)direction * 0.1f;
							velocity.Y += (float)base.directionY * 0.1f;
							if (velocity.X > 3f)
							{
								velocity.X = 3f;
							}
							if (velocity.X < -3f)
							{
								velocity.X = -3f;
							}
							if (velocity.Y > 2f)
							{
								velocity.Y = 2f;
							}
							if (velocity.Y < -2f)
							{
								velocity.Y = -2f;
							}
						}
					}
					else
					{
						velocity.X += (float)direction * 0.1f;
						if (velocity.X < -1f || velocity.X > 1f)
						{
							velocity.X *= 0.95f;
						}
						if (ai[0] == -1f)
						{
							velocity.Y -= 0.01f;
							if ((double)velocity.Y < -0.3)
							{
								ai[0] = 1f;
							}
						}
						else
						{
							velocity.Y += 0.01f;
							if ((double)velocity.Y > 0.3)
							{
								ai[0] = -1f;
							}
						}
						int num288 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int num289 = (int)(base.position.Y + (float)(base.height / 2)) / 16;
						if (Main.tile[num288, num289 - 1] == null)
						{
							Main.tile[num288, num289 - 1] = new Tile();
						}
						if (Main.tile[num288, num289 + 1] == null)
						{
							Main.tile[num288, num289 + 1] = new Tile();
						}
						if (Main.tile[num288, num289 + 2] == null)
						{
							Main.tile[num288, num289 + 2] = new Tile();
						}
						if (Main.tile[num288, num289 - 1].liquid > 128)
						{
							if (Main.tile[num288, num289 + 1].active)
							{
								ai[0] = -1f;
							}
							else if (Main.tile[num288, num289 + 2].active)
							{
								ai[0] = -1f;
							}
						}
						if ((double)velocity.Y > 0.4 || (double)velocity.Y < -0.4)
						{
							velocity.Y *= 0.95f;
						}
					}
				}
				else
				{
					if (velocity.Y == 0f)
					{
						if (type == 65)
						{
							velocity.X *= 0.94f;
							if ((double)velocity.X > -0.2 && (double)velocity.X < 0.2)
							{
								velocity.X = 0f;
							}
						}
						else if (Main.netMode != 1)
						{
							velocity.Y = (float)Config.syncedRand.Next(-50, -20) * 0.1f;
							velocity.X = (float)Config.syncedRand.Next(-20, 20) * 0.1f;
							netUpdate = true;
						}
					}
					velocity.Y += 0.3f;
					if (velocity.Y > 10f)
					{
						velocity.Y = 10f;
					}
					ai[0] = 1f;
				}
				rotation = velocity.Y * (float)direction * 0.1f;
				if ((double)rotation < -0.2)
				{
					rotation = -0.2f;
				}
				if ((double)rotation > 0.2)
				{
					rotation = 0.2f;
				}
			}
			else if (aiStyle == 17)
			{
				noGravity = true;
				if (ai[0] == 0f)
				{
					noGravity = false;
					TargetClosest();
					if (Main.netMode != 1)
					{
						if (velocity.X != 0f || velocity.Y < 0f || (double)velocity.Y > 0.3)
						{
							ai[0] = 1f;
							netUpdate = true;
						}
						else
						{
							Rectangle rectangle4 = new Rectangle((int)Main.player[target].position.X, (int)Main.player[target].position.Y, Main.player[target].width, Main.player[target].height);
							if (new Rectangle((int)base.position.X - 100, (int)base.position.Y - 100, base.width + 200, base.height + 200).Intersects(rectangle4) || life < lifeMax)
							{
								ai[0] = 1f;
								velocity.Y -= 6f;
								netUpdate = true;
							}
						}
					}
				}
				else if (!Main.player[target].dead)
				{
					if (collideX)
					{
						velocity.X = oldVelocity.X * -0.5f;
						if (direction == -1 && velocity.X > 0f && velocity.X < 2f)
						{
							velocity.X = 2f;
						}
						if (direction == 1 && velocity.X < 0f && velocity.X > -2f)
						{
							velocity.X = -2f;
						}
					}
					if (collideY)
					{
						velocity.Y = oldVelocity.Y * -0.5f;
						if (velocity.Y > 0f && velocity.Y < 1f)
						{
							velocity.Y = 1f;
						}
						if (velocity.Y < 0f && velocity.Y > -1f)
						{
							velocity.Y = -1f;
						}
					}
					TargetClosest();
					if (direction == -1 && velocity.X > -3f)
					{
						velocity.X -= 0.1f;
						if (velocity.X > 3f)
						{
							velocity.X -= 0.1f;
						}
						else if (velocity.X > 0f)
						{
							velocity.X -= 0.05f;
						}
						if (velocity.X < -3f)
						{
							velocity.X = -3f;
						}
					}
					else if (direction == 1 && velocity.X < 3f)
					{
						velocity.X += 0.1f;
						if (velocity.X < -3f)
						{
							velocity.X += 0.1f;
						}
						else if (velocity.X < 0f)
						{
							velocity.X += 0.05f;
						}
						if (velocity.X > 3f)
						{
							velocity.X = 3f;
						}
					}
					float num290 = Math.Abs(base.position.X + (float)(base.width / 2) - (Main.player[target].position.X + (float)(Main.player[target].width / 2)));
					float num291 = Main.player[target].position.Y - (float)(base.height / 2);
					if (num290 > 50f)
					{
						num291 -= 100f;
					}
					if (base.position.Y < num291)
					{
						velocity.Y += 0.05f;
						if (velocity.Y < 0f)
						{
							velocity.Y += 0.01f;
						}
					}
					else
					{
						velocity.Y -= 0.05f;
						if (velocity.Y > 0f)
						{
							velocity.Y -= 0.01f;
						}
					}
					if (velocity.Y < -3f)
					{
						velocity.Y = -3f;
					}
					if (velocity.Y > 3f)
					{
						velocity.Y = 3f;
					}
				}
				if (wet)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.Y -= 0.5f;
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
					TargetClosest();
				}
			}
			else if (aiStyle == 18)
			{
				if (type == 63)
				{
					Lighting.addLight((int)(base.position.X + (float)(base.height / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.05f, 0.15f, 0.4f);
				}
				else if (type == 103)
				{
					Lighting.addLight((int)(base.position.X + (float)(base.height / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.05f, 0.45f, 0.1f);
				}
				else
				{
					Lighting.addLight((int)(base.position.X + (float)(base.height / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.35f, 0.05f, 0.2f);
				}
				if (direction == 0)
				{
					TargetClosest();
				}
				if (!wet)
				{
					rotation += velocity.X * 0.1f;
					if (velocity.Y == 0f)
					{
						velocity.X *= 0.98f;
						if ((double)velocity.X > -0.01 && (double)velocity.X < 0.01)
						{
							velocity.X = 0f;
						}
					}
					velocity.Y += 0.2f;
					if (velocity.Y > 10f)
					{
						velocity.Y = 10f;
					}
					ai[0] = 1f;
					return;
				}
				if (collideX)
				{
					velocity.X *= -1f;
					direction *= -1;
				}
				if (collideY)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y = Math.Abs(velocity.Y) * -1f;
						base.directionY = -1;
						ai[0] = -1f;
					}
					else if (velocity.Y < 0f)
					{
						velocity.Y = Math.Abs(velocity.Y);
						base.directionY = 1;
						ai[0] = 1f;
					}
				}
				bool flag20 = false;
				if (!friendly)
				{
					TargetClosest(faceTarget: false);
					if (Main.player[target].wet && !Main.player[target].dead)
					{
						flag20 = true;
					}
				}
				if (flag20)
				{
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
					velocity *= 0.98f;
					float num292 = 0.2f;
					if (type == 103)
					{
						velocity *= 0.98f;
						num292 = 0.6f;
					}
					if (velocity.X > 0f - num292 && velocity.X < num292 && velocity.Y > 0f - num292 && velocity.Y < num292)
					{
						TargetClosest();
						float num293 = 7f;
						if (type == 103)
						{
							num293 = 9f;
						}
						Vector2 vector25 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num294 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector25.X;
						float num295 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector25.Y;
						float num296 = (float)Math.Sqrt(num294 * num294 + num295 * num295);
						num296 = num293 / num296;
						num294 *= num296;
						num295 *= num296;
						velocity.X = num294;
						velocity.Y = num295;
					}
					return;
				}
				velocity.X += (float)direction * 0.02f;
				rotation = velocity.X * 0.4f;
				if (velocity.X < -1f || velocity.X > 1f)
				{
					velocity.X *= 0.95f;
				}
				if (ai[0] == -1f)
				{
					velocity.Y -= 0.01f;
					if (velocity.Y < -1f)
					{
						ai[0] = 1f;
					}
				}
				else
				{
					velocity.Y += 0.01f;
					if (velocity.Y > 1f)
					{
						ai[0] = -1f;
					}
				}
				int num297 = (int)(base.position.X + (float)(base.width / 2)) / 16;
				int num298 = (int)(base.position.Y + (float)(base.height / 2)) / 16;
				if (Main.tile[num297, num298 - 1] == null)
				{
					Main.tile[num297, num298 - 1] = new Tile();
				}
				if (Main.tile[num297, num298 + 1] == null)
				{
					Main.tile[num297, num298 + 1] = new Tile();
				}
				if (Main.tile[num297, num298 + 2] == null)
				{
					Main.tile[num297, num298 + 2] = new Tile();
				}
				if (Main.tile[num297, num298 - 1].liquid > 128)
				{
					if (Main.tile[num297, num298 + 1].active)
					{
						ai[0] = -1f;
					}
					else if (Main.tile[num297, num298 + 2].active)
					{
						ai[0] = -1f;
					}
				}
				else
				{
					ai[0] = 1f;
				}
				if ((double)velocity.Y > 1.2 || (double)velocity.Y < -1.2)
				{
					velocity.Y *= 0.99f;
				}
			}
			else if (aiStyle == 19)
			{
				TargetClosest();
				float num299 = 12f;
				Vector2 vector26 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num300 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector26.X;
				float num301 = Main.player[target].position.Y - vector26.Y;
				float num302 = (float)Math.Sqrt(num300 * num300 + num301 * num301);
				num302 = num299 / num302;
				num300 *= num302;
				num301 *= num302;
				bool flag21 = false;
				if (base.directionY < 0)
				{
					rotation = (float)(Math.Atan2(num301, num300) + 1.57);
					flag21 = ((double)rotation >= -1.2 && (double)rotation <= 1.2);
					if ((double)rotation < -0.8)
					{
						rotation = -0.8f;
					}
					else if ((double)rotation > 0.8)
					{
						rotation = 0.8f;
					}
					if (velocity.X != 0f)
					{
						velocity.X *= 0.9f;
						if ((double)velocity.X > -0.1 || (double)velocity.X < 0.1)
						{
							netUpdate = true;
							velocity.X = 0f;
						}
					}
				}
				if (ai[0] > 0f)
				{
					if (ai[0] == 200f)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 5);
					}
					ai[0] -= 1f;
				}
				if (Main.netMode != 1 && flag21 && ai[0] == 0f && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
				{
					ai[0] = 200f;
					int num303 = 10;
					int num304 = 31;
					int num305 = Projectile.NewProjectile(vector26.X, vector26.Y, num300, num301, num304, num303, 0f, Main.myPlayer);
					Main.projectile[num305].ai[0] = 2f;
					Main.projectile[num305].timeLeft = 300;
					Main.projectile[num305].friendly = false;
					NetMessage.SendData(27, -1, -1, "", num305);
					netUpdate = true;
				}
				try
				{
					int num306 = (int)base.position.X / 16;
					int num307 = (int)(base.position.X + (float)(base.width / 2)) / 16;
					int num308 = (int)(base.position.X + (float)base.width) / 16;
					int num309 = (int)(base.position.Y + (float)base.height) / 16;
					bool flag22 = false;
					if (Main.tile[num306, num309] == null)
					{
						Main.tile[num306, num309] = new Tile();
					}
					if (Main.tile[num307, num309] == null)
					{
						Main.tile[num306, num309] = new Tile();
					}
					if (Main.tile[num308, num309] == null)
					{
						Main.tile[num306, num309] = new Tile();
					}
					if ((Main.tile[num306, num309].active && Main.tileSolid[Main.tile[num306, num309].type]) || (Main.tile[num307, num309].active && Main.tileSolid[Main.tile[num307, num309].type]) || (Main.tile[num308, num309].active && Main.tileSolid[Main.tile[num308, num309].type]))
					{
						flag22 = true;
					}
					if (flag22)
					{
						noGravity = true;
						noTileCollide = true;
						velocity.Y = -0.2f;
					}
					else
					{
						noGravity = false;
						noTileCollide = false;
						if (Config.syncedRand.Next(2) == 0)
						{
							Vector2 position30 = new Vector2(base.position.X - 4f, base.position.Y + (float)base.height - 8f);
							int width30 = base.width + 8;
							int height30 = 24;
							int num310 = 32;
							float speedX25 = 0f;
							float speedY29 = velocity.Y / 2f;
							int num311 = 0;
							int num312 = Dust.NewDust(position30, width30, height30, num310, speedX25, speedY29, num311);
							Dust dust43 = Main.dust[num312];
							dust43.velocity.X = dust43.velocity.X * 0.4f;
							Dust dust44 = Main.dust[num312];
							dust44.velocity.Y = dust44.velocity.Y * -1f;
							if (Config.syncedRand.Next(2) == 0)
							{
								Main.dust[num312].noGravity = true;
								Main.dust[num312].scale += 0.2f;
							}
						}
					}
				}
				catch
				{
				}
			}
			else if (aiStyle == 20)
			{
				if (ai[0] == 0f)
				{
					if (Main.netMode != 1)
					{
						TargetClosest();
						direction *= -1;
						base.directionY *= -1;
						base.position.Y = base.position.Y + (float)(base.height / 2 + 8);
						ai[1] = base.position.X + (float)(base.width / 2);
						ai[2] = base.position.Y + (float)(base.height / 2);
						if (direction == 0)
						{
							direction = 1;
						}
						if (base.directionY == 0)
						{
							base.directionY = 1;
						}
						ai[3] = 1f + (float)Config.syncedRand.Next(15) * 0.1f;
						velocity.Y = (float)(base.directionY * 6) * ai[3];
						ai[0] += 1f;
						netUpdate = true;
					}
					else
					{
						ai[1] = base.position.X + (float)(base.width / 2);
						ai[2] = base.position.Y + (float)(base.height / 2);
					}
					return;
				}
				float num313 = 6f * ai[3];
				float num314 = 0.2f * ai[3];
				float num315 = num313 / num314 / 2f;
				if (ai[0] >= 1f && ai[0] < (float)(int)num315)
				{
					velocity.Y = (float)base.directionY * num313;
					ai[0] += 1f;
					return;
				}
				if (ai[0] >= (float)(int)num315)
				{
					netUpdate = true;
					velocity.Y = 0f;
					base.directionY *= -1;
					velocity.X = num313 * (float)direction;
					ai[0] = -1f;
					return;
				}
				if (base.directionY > 0)
				{
					if (velocity.Y >= num313)
					{
						netUpdate = true;
						base.directionY *= -1;
						velocity.Y = num313;
					}
				}
				else if (base.directionY < 0 && velocity.Y <= 0f - num313)
				{
					base.directionY *= -1;
					velocity.Y = 0f - num313;
				}
				if (direction > 0)
				{
					if (velocity.X >= num313)
					{
						direction *= -1;
						velocity.X = num313;
					}
				}
				else if (direction < 0 && velocity.X <= 0f - num313)
				{
					direction *= -1;
					velocity.X = 0f - num313;
				}
				velocity.X += num314 * (float)direction;
				velocity.Y += num314 * (float)base.directionY;
			}
			else if (aiStyle == 21)
			{
				if (ai[0] == 0f)
				{
					TargetClosest();
					base.directionY = 1;
					ai[0] = 1f;
				}
				int num316 = 6;
				if (ai[1] == 0f)
				{
					rotation += (float)(direction * base.directionY) * 0.13f;
					if (collideY)
					{
						ai[0] = 2f;
					}
					if (!collideY && ai[0] == 2f)
					{
						direction = -direction;
						ai[1] = 1f;
						ai[0] = 1f;
					}
					if (collideX)
					{
						base.directionY = -base.directionY;
						ai[1] = 1f;
					}
				}
				else
				{
					rotation -= (float)(direction * base.directionY) * 0.13f;
					if (collideX)
					{
						ai[0] = 2f;
					}
					if (!collideX && ai[0] == 2f)
					{
						base.directionY = -base.directionY;
						ai[1] = 0f;
						ai[0] = 1f;
					}
					if (collideY)
					{
						direction = -direction;
						ai[1] = 0f;
					}
				}
				velocity.X = num316 * direction;
				velocity.Y = num316 * base.directionY;
				float num317 = (float)(270 - Main.mouseTextColor) / 400f;
				Lighting.addLight((int)(base.position.X + (float)(base.width / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.9f, 0.3f + num317, 0.2f);
			}
			else if (aiStyle == 22)
			{
				bool flag23 = false;
				if (justHit)
				{
					ai[2] = 0f;
				}
				if (ai[2] >= 0f)
				{
					int num318 = 16;
					bool flag24 = false;
					bool flag25 = false;
					if (base.position.X > ai[0] - (float)num318 && base.position.X < ai[0] + (float)num318)
					{
						flag24 = true;
					}
					else if ((velocity.X < 0f && direction > 0) || (velocity.X > 0f && direction < 0))
					{
						flag24 = true;
					}
					num318 += 24;
					if (base.position.Y > ai[1] - (float)num318 && base.position.Y < ai[1] + (float)num318)
					{
						flag25 = true;
					}
					if (flag24 && flag25)
					{
						ai[2] += 1f;
						if (ai[2] >= 30f && num318 == 16)
						{
							flag23 = true;
						}
						if (ai[2] >= 60f)
						{
							ai[2] = -200f;
							direction *= -1;
							velocity.X *= -1f;
							collideX = false;
						}
					}
					else
					{
						ai[0] = base.position.X;
						ai[1] = base.position.Y;
						ai[2] = 0f;
					}
					TargetClosest();
				}
				else
				{
					ai[2] += 1f;
					if (Main.player[target].position.X + (float)(Main.player[target].width / 2) > base.position.X + (float)(base.width / 2))
					{
						direction = -1;
					}
					else
					{
						direction = 1;
					}
				}
				int num319 = (int)((base.position.X + (float)(base.width / 2)) / 16f) + direction * 2;
				int num320 = (int)((base.position.Y + (float)base.height) / 16f);
				bool flag26 = true;
				bool flag27 = false;
				int num321 = 3;
				if (type == 122)
				{
					if (justHit)
					{
						ai[3] = 0f;
						localAI[1] = 0f;
					}
					float num322 = 7f;
					Vector2 vector27 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num323 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector27.X;
					float num324 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector27.Y;
					float num325 = (float)Math.Sqrt(num323 * num323 + num324 * num324);
					num325 = num322 / num325;
					num323 *= num325;
					num324 *= num325;
					if (Main.netMode != 1 && ai[3] == 32f)
					{
						int num326 = 25;
						int num327 = 84;
						Projectile.NewProjectile(vector27.X, vector27.Y, num323, num324, num327, num326, 0f, Main.myPlayer);
					}
					num321 = 8;
					if (ai[3] > 0f)
					{
						ai[3] += 1f;
						if (ai[3] >= 64f)
						{
							ai[3] = 0f;
						}
					}
					if (Main.netMode != 1 && ai[3] == 0f)
					{
						localAI[1] += 1f;
						if (localAI[1] > 120f && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							localAI[1] = 0f;
							ai[3] = 1f;
							netUpdate = true;
						}
					}
				}
				else if (type == 75)
				{
					num321 = 4;
					if (Config.syncedRand.Next(6) == 0)
					{
						int num328 = Dust.NewDust(base.position, base.width, base.height, 55, 0f, 0f, 200, color);
						Dust dust45 = Main.dust[num328];
						dust45.velocity *= 0.3f;
					}
					if (Config.syncedRand.Next(40) == 0)
					{
						Main.PlaySound(27, (int)base.position.X, (int)base.position.Y);
					}
				}
				for (int num329 = num320; num329 < num320 + num321; num329++)
				{
					if (Main.tile[num319, num329] == null)
					{
						Main.tile[num319, num329] = new Tile();
					}
					if ((Main.tile[num319, num329].active && Main.tileSolid[Main.tile[num319, num329].type]) || Main.tile[num319, num329].liquid > 0)
					{
						if (num329 <= num320 + 1)
						{
							flag27 = true;
						}
						flag26 = false;
						break;
					}
				}
				if (flag23)
				{
					flag27 = false;
					flag26 = true;
				}
				if (flag26)
				{
					if (type == 75)
					{
						velocity.Y += 0.2f;
						if (velocity.Y > 2f)
						{
							velocity.Y = 2f;
						}
					}
					else
					{
						velocity.Y += 0.1f;
						if (velocity.Y > 3f)
						{
							velocity.Y = 3f;
						}
					}
				}
				else
				{
					if (type == 75)
					{
						if ((base.directionY < 0 && velocity.Y > 0f) || flag27)
						{
							velocity.Y -= 0.2f;
						}
					}
					else if (base.directionY < 0 && velocity.Y > 0f)
					{
						velocity.Y -= 0.1f;
					}
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
				}
				if (type == 75 && wet)
				{
					velocity.Y -= 0.2f;
					if (velocity.Y < -2f)
					{
						velocity.Y = -2f;
					}
				}
				if (collideX)
				{
					velocity.X = oldVelocity.X * -0.4f;
					if (direction == -1 && velocity.X > 0f && velocity.X < 1f)
					{
						velocity.X = 1f;
					}
					if (direction == 1 && velocity.X < 0f && velocity.X > -1f)
					{
						velocity.X = -1f;
					}
				}
				if (collideY)
				{
					velocity.Y = oldVelocity.Y * -0.25f;
					if (velocity.Y > 0f && velocity.Y < 1f)
					{
						velocity.Y = 1f;
					}
					if (velocity.Y < 0f && velocity.Y > -1f)
					{
						velocity.Y = -1f;
					}
				}
				float num330 = 2f;
				if (type == 75)
				{
					num330 = 3f;
				}
				if (direction == -1 && velocity.X > 0f - num330)
				{
					velocity.X -= 0.1f;
					if (velocity.X > num330)
					{
						velocity.X -= 0.1f;
					}
					else if (velocity.X > 0f)
					{
						velocity.X += 0.05f;
					}
					if (velocity.X < 0f - num330)
					{
						velocity.X = 0f - num330;
					}
				}
				else if (direction == 1 && velocity.X < num330)
				{
					velocity.X += 0.1f;
					if (velocity.X < 0f - num330)
					{
						velocity.X += 0.1f;
					}
					else if (velocity.X < 0f)
					{
						velocity.X -= 0.05f;
					}
					if (velocity.X > num330)
					{
						velocity.X = num330;
					}
				}
				if (base.directionY == -1 && (double)velocity.Y > -1.5)
				{
					velocity.Y -= 0.04f;
					if ((double)velocity.Y > 1.5)
					{
						velocity.Y -= 0.05f;
					}
					else if (velocity.Y > 0f)
					{
						velocity.Y += 0.03f;
					}
					if ((double)velocity.Y < -1.5)
					{
						velocity.Y = -1.5f;
					}
				}
				else if (base.directionY == 1 && (double)velocity.Y < 1.5)
				{
					velocity.Y += 0.04f;
					if ((double)velocity.Y < -1.5)
					{
						velocity.Y += 0.05f;
					}
					else if (velocity.Y < 0f)
					{
						velocity.Y -= 0.03f;
					}
					if ((double)velocity.Y > 1.5)
					{
						velocity.Y = 1.5f;
					}
				}
				if (type == 122)
				{
					Lighting.addLight((int)base.position.X / 16, (int)base.position.Y / 16, 0.4f, 0f, 0.25f);
				}
			}
			else if (aiStyle == 23)
			{
				noGravity = true;
				noTileCollide = true;
				if (type == 83)
				{
					Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 0.2f, 0.05f, 0.3f);
				}
				else
				{
					Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 0.05f, 0.2f, 0.3f);
				}
				if (target < 0 || target == 255 || Main.player[target].dead)
				{
					TargetClosest();
				}
				if (ai[0] == 0f)
				{
					float num331 = 9f;
					Vector2 vector28 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num332 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector28.X;
					float num333 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector28.Y;
					float num334 = (float)Math.Sqrt(num332 * num332 + num333 * num333);
					num334 = num331 / num334;
					num332 *= num334;
					num333 *= num334;
					velocity.X = num332;
					velocity.Y = num333;
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 0.785f;
					ai[0] = 1f;
					ai[1] = 0f;
					return;
				}
				if (ai[0] == 1f)
				{
					if (justHit)
					{
						ai[0] = 2f;
						ai[1] = 0f;
					}
					velocity *= 0.99f;
					ai[1] += 1f;
					if (ai[1] >= 100f)
					{
						ai[0] = 2f;
						ai[1] = 0f;
						velocity.X = 0f;
						velocity.Y = 0f;
					}
					return;
				}
				if (justHit)
				{
					ai[0] = 2f;
					ai[1] = 0f;
				}
				velocity *= 0.96f;
				ai[1] += 1f;
				float num335 = ai[1] / 120f;
				num335 = 0.1f + num335 * 0.4f;
				rotation += num335 * (float)direction;
				if (ai[1] >= 120f)
				{
					netUpdate = true;
					ai[0] = 0f;
					ai[1] = 0f;
				}
			}
			else if (aiStyle == 24)
			{
				noGravity = true;
				if (ai[0] == 0f)
				{
					noGravity = false;
					TargetClosest();
					if (Main.netMode != 1)
					{
						if (velocity.X != 0f || velocity.Y < 0f || (double)velocity.Y > 0.3)
						{
							ai[0] = 1f;
							netUpdate = true;
							direction = -direction;
						}
						else
						{
							Rectangle rectangle5 = new Rectangle((int)Main.player[target].position.X, (int)Main.player[target].position.Y, Main.player[target].width, Main.player[target].height);
							if (new Rectangle((int)base.position.X - 100, (int)base.position.Y - 100, base.width + 200, base.height + 200).Intersects(rectangle5) || life < lifeMax)
							{
								ai[0] = 1f;
								velocity.Y -= 6f;
								netUpdate = true;
								direction = -direction;
							}
						}
					}
				}
				else if (!Main.player[target].dead)
				{
					if (collideX)
					{
						direction *= -1;
						velocity.X = oldVelocity.X * -0.5f;
						if (direction == -1 && velocity.X > 0f && velocity.X < 2f)
						{
							velocity.X = 2f;
						}
						if (direction == 1 && velocity.X < 0f && velocity.X > -2f)
						{
							velocity.X = -2f;
						}
					}
					if (collideY)
					{
						velocity.Y = oldVelocity.Y * -0.5f;
						if (velocity.Y > 0f && velocity.Y < 1f)
						{
							velocity.Y = 1f;
						}
						if (velocity.Y < 0f && velocity.Y > -1f)
						{
							velocity.Y = -1f;
						}
					}
					if (direction == -1 && velocity.X > -3f)
					{
						velocity.X -= 0.1f;
						if (velocity.X > 3f)
						{
							velocity.X -= 0.1f;
						}
						else if (velocity.X > 0f)
						{
							velocity.X -= 0.05f;
						}
						if (velocity.X < -3f)
						{
							velocity.X = -3f;
						}
					}
					else if (direction == 1 && velocity.X < 3f)
					{
						velocity.X += 0.1f;
						if (velocity.X < -3f)
						{
							velocity.X += 0.1f;
						}
						else if (velocity.X < 0f)
						{
							velocity.X += 0.05f;
						}
						if (velocity.X > 3f)
						{
							velocity.X = 3f;
						}
					}
					int num336 = (int)((base.position.X + (float)(base.width / 2)) / 16f) + direction;
					int num337 = (int)((base.position.Y + (float)base.height) / 16f);
					bool flag28 = true;
					int num338 = 15;
					bool flag29 = false;
					for (int num339 = num337; num339 < num337 + num338; num339++)
					{
						if (Main.tile[num336, num339] == null)
						{
							Main.tile[num336, num339] = new Tile();
						}
						if ((Main.tile[num336, num339].active && Main.tileSolid[Main.tile[num336, num339].type]) || Main.tile[num336, num339].liquid > 0)
						{
							if (num339 < num337 + 5)
							{
								flag29 = true;
							}
							flag28 = false;
							break;
						}
					}
					if (flag28)
					{
						velocity.Y += 0.1f;
					}
					else
					{
						velocity.Y -= 0.1f;
					}
					if (flag29)
					{
						velocity.Y -= 0.2f;
					}
					if (velocity.Y > 3f)
					{
						velocity.Y = 3f;
					}
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
				}
				if (wet)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.Y -= 0.5f;
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
					TargetClosest();
				}
			}
			else if (aiStyle == 25)
			{
				if (ai[3] == 0f)
				{
					base.position.X = base.position.X + 8f;
					if (base.position.Y / 16f > (float)Main.hellLayer)
					{
						ai[3] = 3f;
					}
					else if ((double)(base.position.Y / 16f) > Main.worldSurface)
					{
						ai[3] = 2f;
					}
					else
					{
						ai[3] = 1f;
					}
				}
				if (ai[0] == 0f)
				{
					TargetClosest();
					if (Main.netMode == 1)
					{
						return;
					}
					if (velocity.X != 0f || velocity.Y < 0f || (double)velocity.Y > 0.3)
					{
						ai[0] = 1f;
						netUpdate = true;
						return;
					}
					Rectangle rectangle6 = new Rectangle((int)Main.player[target].position.X, (int)Main.player[target].position.Y, Main.player[target].width, Main.player[target].height);
					if (new Rectangle((int)base.position.X - 100, (int)base.position.Y - 100, base.width + 200, base.height + 200).Intersects(rectangle6) || life < lifeMax)
					{
						ai[0] = 1f;
						netUpdate = true;
					}
				}
				else if (velocity.Y == 0f)
				{
					ai[2] += 1f;
					int num340 = 20;
					if (ai[1] == 0f)
					{
						num340 = 12;
					}
					if (ai[2] < (float)num340)
					{
						velocity.X *= 0.9f;
						return;
					}
					ai[2] = 0f;
					TargetClosest();
					spriteDirection = direction;
					ai[1] += 1f;
					if (ai[1] == 2f)
					{
						velocity.X = (float)direction * 2.5f;
						velocity.Y = -8f;
						ai[1] = 0f;
					}
					else
					{
						velocity.X = (float)direction * 3.5f;
						velocity.Y = -4f;
					}
					netUpdate = true;
				}
				else if (direction == 1 && velocity.X < 1f)
				{
					velocity.X += 0.1f;
				}
				else if (direction == -1 && velocity.X > -1f)
				{
					velocity.X -= 0.1f;
				}
			}
			else if (aiStyle == 26)
			{
				int num341 = 30;
				bool flag30 = false;
				if (velocity.Y == 0f && ((velocity.X > 0f && direction < 0) || (velocity.X < 0f && direction > 0)))
				{
					flag30 = true;
					ai[3] += 1f;
				}
				if (base.position.X == oldPosition.X || ai[3] >= (float)num341 || flag30)
				{
					ai[3] += 1f;
				}
				else if (ai[3] > 0f)
				{
					ai[3] -= 1f;
				}
				if (ai[3] > (float)(num341 * 10))
				{
					ai[3] = 0f;
				}
				if (justHit)
				{
					ai[3] = 0f;
				}
				if (ai[3] == (float)num341)
				{
					netUpdate = true;
				}
				if (ai[3] < (float)num341)
				{
					TargetClosest();
				}
				else
				{
					if (velocity.X == 0f)
					{
						if (velocity.Y == 0f)
						{
							ai[0] += 1f;
							if (ai[0] >= 2f)
							{
								direction *= -1;
								spriteDirection = direction;
								ai[0] = 0f;
							}
						}
					}
					else
					{
						ai[0] = 0f;
					}
					base.directionY = -1;
					if (direction == 0)
					{
						direction = 1;
					}
				}
				float num342 = 6f;
				if (velocity.Y == 0f || wet || (velocity.X <= 0f && direction < 0) || (velocity.X >= 0f && direction > 0))
				{
					if (velocity.X < 0f - num342 || velocity.X > num342)
					{
						if (velocity.Y == 0f)
						{
							velocity *= 0.8f;
						}
					}
					else if (velocity.X < num342 && direction == 1)
					{
						velocity.X += 0.07f;
						if (velocity.X > num342)
						{
							velocity.X = num342;
						}
					}
					else if (velocity.X > 0f - num342 && direction == -1)
					{
						velocity.X -= 0.07f;
						if (velocity.X < 0f - num342)
						{
							velocity.X = 0f - num342;
						}
					}
				}
				if (velocity.Y != 0f)
				{
					return;
				}
				int num343 = (int)((base.position.X + (float)(base.width / 2) + (float)((base.width / 2 + 2) * direction) + velocity.X * 5f) / 16f);
				int num344 = (int)((base.position.Y + (float)base.height - 15f) / 16f);
				if (Main.tile[num343, num344] == null)
				{
					Main.tile[num343, num344] = new Tile();
				}
				if (Main.tile[num343, num344 - 1] == null)
				{
					Main.tile[num343, num344 - 1] = new Tile();
				}
				if (Main.tile[num343, num344 - 2] == null)
				{
					Main.tile[num343, num344 - 2] = new Tile();
				}
				if (Main.tile[num343, num344 - 3] == null)
				{
					Main.tile[num343, num344 - 3] = new Tile();
				}
				if (Main.tile[num343, num344 + 1] == null)
				{
					Main.tile[num343, num344 + 1] = new Tile();
				}
				if (Main.tile[num343 + direction, num344 - 1] == null)
				{
					Main.tile[num343 + direction, num344 - 1] = new Tile();
				}
				if (Main.tile[num343 + direction, num344 + 1] == null)
				{
					Main.tile[num343 + direction, num344 + 1] = new Tile();
				}
				if ((!(velocity.X < 0f) || spriteDirection != -1) && (!(velocity.X > 0f) || spriteDirection != 1))
				{
					return;
				}
				if (Main.tile[num343, num344 - 2].active && Main.tileSolid[Main.tile[num343, num344 - 2].type])
				{
					if (Main.tile[num343, num344 - 3].active && Main.tileSolid[Main.tile[num343, num344 - 3].type])
					{
						velocity.Y = -8.5f;
						netUpdate = true;
					}
					else
					{
						velocity.Y = -7.5f;
						netUpdate = true;
					}
				}
				else if (Main.tile[num343, num344 - 1].active && Main.tileSolid[Main.tile[num343, num344 - 1].type])
				{
					velocity.Y = -7f;
					netUpdate = true;
				}
				else if (Main.tile[num343, num344].active && Main.tileSolid[Main.tile[num343, num344].type])
				{
					velocity.Y = -6f;
					netUpdate = true;
				}
				else if ((base.directionY < 0 || Math.Abs(velocity.X) > 3f) && (!Main.tile[num343, num344 + 1].active || !Main.tileSolid[Main.tile[num343, num344 + 1].type]) && (!Main.tile[num343 + direction, num344 + 1].active || !Main.tileSolid[Main.tile[num343 + direction, num344 + 1].type]))
				{
					velocity.Y = -8f;
					netUpdate = true;
				}
			}
			else if (aiStyle == 27)
			{
				if (base.position.X < 160f || base.position.X > (float)((Main.maxTilesX - 10) * 16))
				{
					active = false;
				}
				if (localAI[0] == 0f)
				{
					localAI[0] = 1f;
					Main.wofB = -1;
					Main.wofT = -1;
				}
				ai[1] += 1f;
				if (ai[2] == 0f)
				{
					if ((double)life < (double)lifeMax * 0.5)
					{
						ai[1] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.2)
					{
						ai[1] += 1f;
					}
					if (ai[1] > 2700f)
					{
						ai[2] = 1f;
					}
				}
				if (ai[2] > 0f && ai[1] > 60f)
				{
					int num345 = 3;
					if ((double)life < (double)lifeMax * 0.3)
					{
						num345++;
					}
					ai[2] += 1f;
					ai[1] = 0f;
					if (ai[2] > (float)num345)
					{
						ai[2] = 0f;
					}
					if (Main.netMode != 1)
					{
						int num346 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)(base.height / 2) + 20f), 117, 1);
						Main.npc[num346].velocity.X = direction * 8;
					}
				}
				localAI[3] += 1f;
				if (localAI[3] >= (float)(600 + Config.syncedRand.Next(1000)))
				{
					localAI[3] = 0f - (float)Config.syncedRand.Next(200);
					Main.PlaySound(4, (int)base.position.X, (int)base.position.Y, 10);
				}
				Main.wof = whoAmI;
				int num347 = (int)(base.position.X / 16f);
				int num348 = (int)((base.position.X + (float)base.width) / 16f);
				int num349 = (int)((base.position.Y + (float)(base.height / 2)) / 16f);
				int num350 = 0;
				int num351 = num349 + 7;
				while (num350 < 15 && (double)num351 > Main.hellLayer)
				{
					num351++;
					for (int num352 = num347; num352 <= num348; num352++)
					{
						try
						{
							if (WorldGen.SolidTile(num352, num351) || Main.tile[num352, num351].liquid > 0)
							{
								num350++;
							}
						}
						catch
						{
							num350 += 15;
						}
					}
				}
				num351 += 4;
				if (Main.wofB == -1)
				{
					Main.wofB = num351 * 16;
				}
				else if (Main.wofB > num351 * 16)
				{
					Main.wofB--;
					if (Main.wofB < num351 * 16)
					{
						Main.wofB = num351 * 16;
					}
				}
				else if (Main.wofB < num351 * 16)
				{
					Main.wofB++;
					if (Main.wofB > num351 * 16)
					{
						Main.wofB = num351 * 16;
					}
				}
				num350 = 0;
				num351 = num349 - 7;
				while (num350 < 15 && num351 < Main.maxTilesY - 10)
				{
					num351--;
					for (int num353 = num347; num353 <= num348; num353++)
					{
						try
						{
							if (WorldGen.SolidTile(num353, num351) || Main.tile[num353, num351].liquid > 0)
							{
								num350++;
							}
						}
						catch
						{
							num350 += 15;
						}
					}
				}
				num351 -= 4;
				if (Main.wofT == -1)
				{
					Main.wofT = num351 * 16;
				}
				else if (Main.wofT > num351 * 16)
				{
					Main.wofT--;
					if (Main.wofT < num351 * 16)
					{
						Main.wofT = num351 * 16;
					}
				}
				else if (Main.wofT < num351 * 16)
				{
					Main.wofT++;
					if (Main.wofT > num351 * 16)
					{
						Main.wofT = num351 * 16;
					}
				}
				float num354 = (Main.wofB + Main.wofT) / 2 - base.height / 2;
				if (base.position.Y > num354 + 1f)
				{
					velocity.Y = -1f;
				}
				else if (base.position.Y < num354 - 1f)
				{
					velocity.Y = 1f;
				}
				velocity.Y = 0f;
				base.position.Y = num354;
				float num355 = 1.5f;
				if ((double)life < (double)lifeMax * 0.75)
				{
					num355 += 0.25f;
				}
				if ((double)life < (double)lifeMax * 0.5)
				{
					num355 += 0.4f;
				}
				if ((double)life < (double)lifeMax * 0.25)
				{
					num355 += 0.5f;
				}
				if ((double)life < (double)lifeMax * 0.1)
				{
					num355 += 0.6f;
				}
				if (velocity.X == 0f)
				{
					TargetClosest();
					velocity.X = direction;
				}
				if (velocity.X < 0f)
				{
					velocity.X = 0f - num355;
					direction = -1;
				}
				else
				{
					velocity.X = num355;
					direction = 1;
				}
				spriteDirection = direction;
				Vector2 vector29 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num356 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector29.X;
				float num357 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector29.Y;
				float num358 = (float)Math.Sqrt(num356 * num356 + num357 * num357);
				num356 *= num358;
				num357 *= num358;
				if (direction > 0)
				{
					if (Main.player[target].position.X + (float)(Main.player[target].width / 2) > base.position.X + (float)(base.width / 2))
					{
						rotation = (float)Math.Atan2(0.0 - (double)num357, 0.0 - (double)num356) + 3.14f;
					}
					else
					{
						rotation = 0f;
					}
				}
				else if (Main.player[target].position.X + (float)(Main.player[target].width / 2) < base.position.X + (float)(base.width / 2))
				{
					rotation = (float)Math.Atan2(num357, num356) + 3.14f;
				}
				else
				{
					rotation = 0f;
				}
				if (localAI[0] == 1f && Main.netMode != 1)
				{
					localAI[0] = 2f;
					num354 = (Main.wofB + Main.wofT) / 2;
					num354 = (num354 + (float)Main.wofT) / 2f;
					int num359 = NewNPC((int)base.position.X, (int)num354, 114, whoAmI);
					Main.npc[num359].ai[0] = 1f;
					num354 = (Main.wofB + Main.wofT) / 2;
					num354 = (num354 + (float)Main.wofB) / 2f;
					num359 = NewNPC((int)base.position.X, (int)num354, 114, whoAmI);
					Main.npc[num359].ai[0] = -1f;
					num354 = (Main.wofB + Main.wofT) / 2;
					num354 = (num354 + (float)Main.wofB) / 2f;
					for (int num360 = 0; num360 < 11; num360++)
					{
						num359 = NewNPC((int)base.position.X, (int)num354, 115, whoAmI);
						Main.npc[num359].ai[0] = (float)num360 * 0.1f - 0.05f;
					}
				}
			}
			else if (aiStyle == 28)
			{
				if (Main.wof < 0)
				{
					active = false;
					return;
				}
				realLife = Main.wof;
				TargetClosest();
				base.position.X = Main.npc[Main.wof].position.X;
				direction = Main.npc[Main.wof].direction;
				spriteDirection = direction;
				float num361 = (Main.wofB + Main.wofT) / 2;
				num361 = ((!(ai[0] > 0f)) ? ((num361 + (float)Main.wofB) / 2f) : ((num361 + (float)Main.wofT) / 2f));
				num361 -= (float)(base.height / 2);
				if (base.position.Y > num361 + 1f)
				{
					velocity.Y = -1f;
				}
				else if (base.position.Y < num361 - 1f)
				{
					velocity.Y = 1f;
				}
				else
				{
					velocity.Y = 0f;
					base.position.Y = num361;
				}
				if (velocity.Y > 5f)
				{
					velocity.Y = 5f;
				}
				if (velocity.Y < -5f)
				{
					velocity.Y = -5f;
				}
				Vector2 vector30 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num362 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector30.X;
				float num363 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector30.Y;
				float num364 = (float)Math.Sqrt(num362 * num362 + num363 * num363);
				num362 *= num364;
				num363 *= num364;
				bool flag31 = true;
				if (direction > 0)
				{
					if (Main.player[target].position.X + (float)(Main.player[target].width / 2) > base.position.X + (float)(base.width / 2))
					{
						rotation = (float)Math.Atan2(0.0 - (double)num363, 0.0 - (double)num362) + 3.14f;
					}
					else
					{
						rotation = 0f;
						flag31 = false;
					}
				}
				else if (Main.player[target].position.X + (float)(Main.player[target].width / 2) < base.position.X + (float)(base.width / 2))
				{
					rotation = (float)Math.Atan2(num363, num362) + 3.14f;
				}
				else
				{
					rotation = 0f;
					flag31 = false;
				}
				if (Main.netMode == 1)
				{
					return;
				}
				int num365 = 4;
				localAI[1] += 1f;
				if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.75)
				{
					localAI[1] += 1f;
					num365++;
				}
				if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.5)
				{
					localAI[1] += 1f;
					num365++;
				}
				if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.25)
				{
					localAI[1] += 1f;
					num365 += 2;
				}
				if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.1)
				{
					localAI[1] += 2f;
					num365 += 3;
				}
				if (localAI[2] == 0f)
				{
					if (localAI[1] > 600f)
					{
						localAI[2] = 1f;
						localAI[1] = 0f;
					}
				}
				else
				{
					if (!(localAI[1] > 45f) || !Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						return;
					}
					localAI[1] = 0f;
					localAI[2] += 1f;
					if (localAI[2] >= (float)num365)
					{
						localAI[2] = 0f;
					}
					if (flag31)
					{
						float num366 = 9f;
						int num367 = 11;
						int num368 = 83;
						if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.5)
						{
							num367++;
							num366 += 1f;
						}
						if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.25)
						{
							num367++;
							num366 += 1f;
						}
						if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.1)
						{
							num367 += 2;
							num366 += 2f;
						}
						vector30 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num362 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector30.X;
						num363 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector30.Y;
						num364 = (float)Math.Sqrt(num362 * num362 + num363 * num363);
						num364 = num366 / num364;
						num362 *= num364;
						num363 *= num364;
						vector30.X += num362;
						vector30.Y += num363;
						Projectile.NewProjectile(vector30.X, vector30.Y, num362, num363, num368, num367, 0f, Main.myPlayer);
					}
				}
			}
			else if (aiStyle == 29)
			{
				if (justHit)
				{
					ai[1] = 10f;
				}
				if (Main.wof < 0)
				{
					active = false;
					return;
				}
				TargetClosest();
				float num369 = 0.1f;
				float num370 = 300f;
				if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.25)
				{
					damage = 75;
					defense = 40;
					num370 = 900f;
				}
				else if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.5)
				{
					damage = 60;
					defense = 30;
					num370 = 700f;
				}
				else if ((double)Main.npc[Main.wof].life < (double)Main.npc[Main.wof].lifeMax * 0.75)
				{
					damage = 45;
					defense = 20;
					num370 = 500f;
				}
				float num371 = Main.npc[Main.wof].position.X + (float)(Main.npc[Main.wof].width / 2);
				float y3 = Main.npc[Main.wof].position.Y;
				float num372 = Main.wofB - Main.wofT;
				y3 = (float)Main.wofT + num372 * ai[0];
				ai[2] += 1f;
				if (ai[2] > 100f)
				{
					num370 = (int)(num370 * 1.3f);
					if (ai[2] > 200f)
					{
						ai[2] = 0f;
					}
				}
				Vector2 vector31 = new Vector2(num371, y3);
				float num373 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - (float)(base.width / 2) - vector31.X;
				float num374 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - (float)(base.height / 2) - vector31.Y;
				float num375 = (float)Math.Sqrt(num373 * num373 + num374 * num374);
				if (ai[1] == 0f)
				{
					if (num375 > num370)
					{
						num375 = num370 / num375;
						num373 *= num375;
						num374 *= num375;
					}
					if (base.position.X < num371 + num373)
					{
						velocity.X += num369;
						if (velocity.X < 0f && num373 > 0f)
						{
							velocity.X += num369 * 2.5f;
						}
					}
					else if (base.position.X > num371 + num373)
					{
						velocity.X -= num369;
						if (velocity.X > 0f && num373 < 0f)
						{
							velocity.X -= num369 * 2.5f;
						}
					}
					if (base.position.Y < y3 + num374)
					{
						velocity.Y += num369;
						if (velocity.Y < 0f && num374 > 0f)
						{
							velocity.Y += num369 * 2.5f;
						}
					}
					else if (base.position.Y > y3 + num374)
					{
						velocity.Y -= num369;
						if (velocity.Y > 0f && num374 < 0f)
						{
							velocity.Y -= num369 * 2.5f;
						}
					}
					if (velocity.X > 4f)
					{
						velocity.X = 4f;
					}
					if (velocity.X < -4f)
					{
						velocity.X = -4f;
					}
					if (velocity.Y > 4f)
					{
						velocity.Y = 4f;
					}
					if (velocity.Y < -4f)
					{
						velocity.Y = -4f;
					}
				}
				else if (ai[1] > 0f)
				{
					ai[1] -= 1f;
				}
				else
				{
					ai[1] = 0f;
				}
				if (num373 > 0f)
				{
					spriteDirection = 1;
					rotation = (float)Math.Atan2(num374, num373);
				}
				if (num373 < 0f)
				{
					spriteDirection = -1;
					rotation = (float)Math.Atan2(num374, num373) + 3.14f;
				}
				Lighting.addLight((int)(base.position.X + (float)(base.width / 2)) / 16, (int)(base.position.Y + (float)(base.height / 2)) / 16, 0.3f, 0.2f, 0.1f);
			}
			else if (aiStyle == 30)
			{
				if (target < 0 || target == 255 || Main.player[target].dead || !Main.player[target].active)
				{
					TargetClosest();
				}
				bool dead2 = Main.player[target].dead;
				float num376 = base.position.X + (float)(base.width / 2) - Main.player[target].position.X - (float)(Main.player[target].width / 2);
				float num377 = base.position.Y + (float)base.height - 59f - Main.player[target].position.Y - (float)(Main.player[target].height / 2);
				float num378 = (float)Math.Atan2(num377, num376) + 1.57f;
				if (num378 < 0f)
				{
					num378 += 6.283f;
				}
				else if ((double)num378 > 6.283)
				{
					num378 -= 6.283f;
				}
				float num379 = 0.1f;
				if (rotation < num378)
				{
					if ((double)(num378 - rotation) > 3.1415)
					{
						rotation -= num379;
					}
					else
					{
						rotation += num379;
					}
				}
				else if (rotation > num378)
				{
					if ((double)(rotation - num378) > 3.1415)
					{
						rotation += num379;
					}
					else
					{
						rotation -= num379;
					}
				}
				if (rotation > num378 - num379 && rotation < num378 + num379)
				{
					rotation = num378;
				}
				if (rotation < 0f)
				{
					rotation += 6.283f;
				}
				else if ((double)rotation > 6.283)
				{
					rotation -= 6.283f;
				}
				if (rotation > num378 - num379 && rotation < num378 + num379)
				{
					rotation = num378;
				}
				if (Config.syncedRand.Next(5) == 0)
				{
					Vector2 position31 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
					int width31 = base.width;
					int height31 = (int)((float)base.height * 0.5f);
					int num380 = 5;
					float x7 = velocity.X;
					float speedY30 = 2f;
					int num381 = 0;
					int num382 = Dust.NewDust(position31, width31, height31, num380, x7, speedY30, num381);
					Dust dust46 = Main.dust[num382];
					dust46.velocity.X = dust46.velocity.X * 0.5f;
					Dust dust47 = Main.dust[num382];
					dust47.velocity.Y = dust47.velocity.Y * 0.1f;
				}
				if (Main.dayTime || dead2)
				{
					velocity.Y -= 0.04f;
					if (timeLeft > 10)
					{
						timeLeft = 10;
					}
					return;
				}
				if (ai[0] == 0f)
				{
					if (ai[1] == 0f)
					{
						float num383 = 7f;
						float num384 = 0.1f;
						int num385 = 1;
						if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)Main.player[target].width)
						{
							num385 = -1;
						}
						Vector2 vector32 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num386 = Main.player[target].position.X + (float)(Main.player[target].width / 2) + (float)(num385 * 300) - vector32.X;
						float num387 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 300f - vector32.Y;
						float num388 = (float)Math.Sqrt(num386 * num386 + num387 * num387);
						float num389 = num388;
						num388 = num383 / num388;
						num386 *= num388;
						num387 *= num388;
						if (velocity.X < num386)
						{
							velocity.X += num384;
							if (velocity.X < 0f && num386 > 0f)
							{
								velocity.X += num384;
							}
						}
						else if (velocity.X > num386)
						{
							velocity.X -= num384;
							if (velocity.X > 0f && num386 < 0f)
							{
								velocity.X -= num384;
							}
						}
						if (velocity.Y < num387)
						{
							velocity.Y += num384;
							if (velocity.Y < 0f && num387 > 0f)
							{
								velocity.Y += num384;
							}
						}
						else if (velocity.Y > num387)
						{
							velocity.Y -= num384;
							if (velocity.Y > 0f && num387 < 0f)
							{
								velocity.Y -= num384;
							}
						}
						ai[2] += 1f;
						if (ai[2] >= 600f)
						{
							ai[1] = 1f;
							ai[2] = 0f;
							ai[3] = 0f;
							target = 255;
							netUpdate = true;
						}
						else if (base.position.Y + (float)base.height < Main.player[target].position.Y && num389 < 400f)
						{
							if (!Main.player[target].dead)
							{
								ai[3] += 1f;
							}
							if (ai[3] >= 60f)
							{
								ai[3] = 0f;
								vector32 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
								num386 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector32.X;
								num387 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector32.Y;
								if (Main.netMode != 1)
								{
									float num390 = 9f;
									int num391 = 20;
									int num392 = 83;
									num388 = (float)Math.Sqrt(num386 * num386 + num387 * num387);
									num388 = num390 / num388;
									num386 *= num388;
									num387 *= num388;
									num386 += (float)Config.syncedRand.Next(-40, 41) * 0.08f;
									num387 += (float)Config.syncedRand.Next(-40, 41) * 0.08f;
									vector32.X += num386 * 15f;
									vector32.Y += num387 * 15f;
									Projectile.NewProjectile(vector32.X, vector32.Y, num386, num387, num392, num391, 0f, Main.myPlayer);
								}
							}
						}
					}
					else if (ai[1] == 1f)
					{
						rotation = num378;
						float num393 = 12f;
						Vector2 vector33 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num394 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector33.X;
						float num395 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector33.Y;
						float num396 = (float)Math.Sqrt(num394 * num394 + num395 * num395);
						num396 = num393 / num396;
						velocity.X = num394 * num396;
						velocity.Y = num395 * num396;
						ai[1] = 2f;
					}
					else if (ai[1] == 2f)
					{
						ai[2] += 1f;
						if (ai[2] >= 25f)
						{
							velocity.X *= 0.96f;
							velocity.Y *= 0.96f;
							if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
							{
								velocity.X = 0f;
							}
							if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
							{
								velocity.Y = 0f;
							}
						}
						else
						{
							rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
						}
						if (ai[2] >= 70f)
						{
							ai[3] += 1f;
							ai[2] = 0f;
							target = 255;
							rotation = num378;
							if (ai[3] >= 4f)
							{
								ai[1] = 0f;
								ai[3] = 0f;
							}
							else
							{
								ai[1] = 1f;
							}
						}
					}
					if ((double)life < (double)lifeMax * 0.5)
					{
						ai[0] = 1f;
						ai[1] = 0f;
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					return;
				}
				if (ai[0] == 1f || ai[0] == 2f)
				{
					if (ai[0] == 1f)
					{
						ai[2] += 0.005f;
						if ((double)ai[2] > 0.5)
						{
							ai[2] = 0.5f;
						}
					}
					else
					{
						ai[2] -= 0.005f;
						if (ai[2] < 0f)
						{
							ai[2] = 0f;
						}
					}
					rotation += ai[2];
					ai[1] += 1f;
					if (ai[1] == 100f)
					{
						ai[0] += 1f;
						ai[1] = 0f;
						if (ai[0] == 3f)
						{
							ai[2] = 0f;
						}
						else
						{
							Main.PlaySound(3, (int)base.position.X, (int)base.position.Y);
							for (int num397 = 0; num397 < 2; num397++)
							{
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 143);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 6);
							}
							for (int num398 = 0; num398 < 20; num398++)
							{
								Vector2 position32 = base.position;
								int width32 = base.width;
								int height32 = base.height;
								int num399 = 5;
								float speedX26 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								float speedY31 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								int num400 = 0;
								Dust.NewDust(position32, width32, height32, num399, speedX26, speedY31, num400);
							}
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						}
					}
					Vector2 position33 = base.position;
					int width33 = base.width;
					int height33 = base.height;
					int num401 = 5;
					float speedX27 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					float speedY32 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					int num402 = 0;
					Dust.NewDust(position33, width33, height33, num401, speedX27, speedY32, num402);
					velocity.X *= 0.98f;
					velocity.Y *= 0.98f;
					if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
					{
						velocity.X = 0f;
					}
					if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
					{
						velocity.Y = 0f;
					}
					return;
				}
				damage = (int)((double)defDamage * 1.5);
				defense = defDefense + 15;
				soundHit = 4;
				if (ai[1] == 0f)
				{
					float num403 = 8f;
					float num404 = 0.15f;
					Vector2 vector34 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num405 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector34.X;
					float num406 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 300f - vector34.Y;
					float num407 = (float)Math.Sqrt(num405 * num405 + num406 * num406);
					num407 = num403 / num407;
					num405 *= num407;
					num406 *= num407;
					if (velocity.X < num405)
					{
						velocity.X += num404;
						if (velocity.X < 0f && num405 > 0f)
						{
							velocity.X += num404;
						}
					}
					else if (velocity.X > num405)
					{
						velocity.X -= num404;
						if (velocity.X > 0f && num405 < 0f)
						{
							velocity.X -= num404;
						}
					}
					if (velocity.Y < num406)
					{
						velocity.Y += num404;
						if (velocity.Y < 0f && num406 > 0f)
						{
							velocity.Y += num404;
						}
					}
					else if (velocity.Y > num406)
					{
						velocity.Y -= num404;
						if (velocity.Y > 0f && num406 < 0f)
						{
							velocity.Y -= num404;
						}
					}
					ai[2] += 1f;
					if (ai[2] >= 300f)
					{
						ai[1] = 1f;
						ai[2] = 0f;
						ai[3] = 0f;
						TargetClosest();
						netUpdate = true;
					}
					vector34 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					num405 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector34.X;
					num406 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector34.Y;
					rotation = (float)Math.Atan2(num406, num405) - 1.57f;
					if (Main.netMode != 1)
					{
						localAI[1] += 1f;
						if ((double)life < (double)lifeMax * 0.75)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.5)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.25)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.1)
						{
							localAI[1] += 2f;
						}
						if (localAI[1] > 140f && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
						{
							localAI[1] = 0f;
							float num408 = 9f;
							int num409 = 25;
							int num410 = 100;
							num407 = (float)Math.Sqrt(num405 * num405 + num406 * num406);
							num407 = num408 / num407;
							num405 *= num407;
							num406 *= num407;
							vector34.X += num405 * 15f;
							vector34.Y += num406 * 15f;
							Projectile.NewProjectile(vector34.X, vector34.Y, num405, num406, num410, num409, 0f, Main.myPlayer);
						}
					}
					return;
				}
				int num411 = 1;
				if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)Main.player[target].width)
				{
					num411 = -1;
				}
				float num412 = 8f;
				float num413 = 0.2f;
				Vector2 vector35 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num414 = Main.player[target].position.X + (float)(Main.player[target].width / 2) + (float)(num411 * 340) - vector35.X;
				float num415 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector35.Y;
				float num416 = (float)Math.Sqrt(num414 * num414 + num415 * num415);
				num416 = num412 / num416;
				num414 *= num416;
				num415 *= num416;
				if (velocity.X < num414)
				{
					velocity.X += num413;
					if (velocity.X < 0f && num414 > 0f)
					{
						velocity.X += num413;
					}
				}
				else if (velocity.X > num414)
				{
					velocity.X -= num413;
					if (velocity.X > 0f && num414 < 0f)
					{
						velocity.X -= num413;
					}
				}
				if (velocity.Y < num415)
				{
					velocity.Y += num413;
					if (velocity.Y < 0f && num415 > 0f)
					{
						velocity.Y += num413;
					}
				}
				else if (velocity.Y > num415)
				{
					velocity.Y -= num413;
					if (velocity.Y > 0f && num415 < 0f)
					{
						velocity.Y -= num413;
					}
				}
				vector35 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				num414 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector35.X;
				num415 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector35.Y;
				rotation = (float)Math.Atan2(num415, num414) - 1.57f;
				if (Main.netMode != 1)
				{
					localAI[1] += 1f;
					if ((double)life < (double)lifeMax * 0.75)
					{
						localAI[1] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.5)
					{
						localAI[1] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.25)
					{
						localAI[1] += 1f;
					}
					if ((double)life < (double)lifeMax * 0.1)
					{
						localAI[1] += 2f;
					}
					if (localAI[1] > 45f && Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						localAI[1] = 0f;
						float num417 = 9f;
						int num418 = 20;
						int num419 = 100;
						num416 = (float)Math.Sqrt(num414 * num414 + num415 * num415);
						num416 = num417 / num416;
						num414 *= num416;
						num415 *= num416;
						vector35.X += num414 * 15f;
						vector35.Y += num415 * 15f;
						Projectile.NewProjectile(vector35.X, vector35.Y, num414, num415, num419, num418, 0f, Main.myPlayer);
					}
				}
				ai[2] += 1f;
				if (ai[2] >= 200f)
				{
					ai[1] = 0f;
					ai[2] = 0f;
					ai[3] = 0f;
					TargetClosest();
					netUpdate = true;
				}
			}
			else if (aiStyle == 31)
			{
				if (target < 0 || target == 255 || Main.player[target].dead || !Main.player[target].active)
				{
					TargetClosest();
				}
				bool dead3 = Main.player[target].dead;
				float num420 = base.position.X + (float)(base.width / 2) - Main.player[target].position.X - (float)(Main.player[target].width / 2);
				float num421 = base.position.Y + (float)base.height - 59f - Main.player[target].position.Y - (float)(Main.player[target].height / 2);
				float num422 = (float)Math.Atan2(num421, num420) + 1.57f;
				if (num422 < 0f)
				{
					num422 += 6.283f;
				}
				else if ((double)num422 > 6.283)
				{
					num422 -= 6.283f;
				}
				float num423 = 0.15f;
				if (rotation < num422)
				{
					if ((double)(num422 - rotation) > 3.1415)
					{
						rotation -= num423;
					}
					else
					{
						rotation += num423;
					}
				}
				else if (rotation > num422)
				{
					if ((double)(rotation - num422) > 3.1415)
					{
						rotation += num423;
					}
					else
					{
						rotation -= num423;
					}
				}
				if (rotation > num422 - num423 && rotation < num422 + num423)
				{
					rotation = num422;
				}
				if (rotation < 0f)
				{
					rotation += 6.283f;
				}
				else if ((double)rotation > 6.283)
				{
					rotation -= 6.283f;
				}
				if (rotation > num422 - num423 && rotation < num422 + num423)
				{
					rotation = num422;
				}
				if (Config.syncedRand.Next(5) == 0)
				{
					Vector2 position34 = new Vector2(base.position.X, base.position.Y + (float)base.height * 0.25f);
					int width34 = base.width;
					int height34 = (int)((float)base.height * 0.5f);
					int num424 = 5;
					float x8 = velocity.X;
					float speedY33 = 2f;
					int num425 = 0;
					int num426 = Dust.NewDust(position34, width34, height34, num424, x8, speedY33, num425);
					Dust dust48 = Main.dust[num426];
					dust48.velocity.X = dust48.velocity.X * 0.5f;
					Dust dust49 = Main.dust[num426];
					dust49.velocity.Y = dust49.velocity.Y * 0.1f;
				}
				if (Main.dayTime || dead3)
				{
					velocity.Y -= 0.04f;
					if (timeLeft > 10)
					{
						timeLeft = 10;
					}
					return;
				}
				if (ai[0] == 0f)
				{
					if (ai[1] == 0f)
					{
						TargetClosest();
						float num427 = 12f;
						float num428 = 0.4f;
						int num429 = 1;
						if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)Main.player[target].width)
						{
							num429 = -1;
						}
						Vector2 vector36 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num430 = Main.player[target].position.X + (float)(Main.player[target].width / 2) + (float)(num429 * 400) - vector36.X;
						float num431 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector36.Y;
						float num432 = (float)Math.Sqrt(num430 * num430 + num431 * num431);
						num432 = num427 / num432;
						num430 *= num432;
						num431 *= num432;
						if (velocity.X < num430)
						{
							velocity.X += num428;
							if (velocity.X < 0f && num430 > 0f)
							{
								velocity.X += num428;
							}
						}
						else if (velocity.X > num430)
						{
							velocity.X -= num428;
							if (velocity.X > 0f && num430 < 0f)
							{
								velocity.X -= num428;
							}
						}
						if (velocity.Y < num431)
						{
							velocity.Y += num428;
							if (velocity.Y < 0f && num431 > 0f)
							{
								velocity.Y += num428;
							}
						}
						else if (velocity.Y > num431)
						{
							velocity.Y -= num428;
							if (velocity.Y > 0f && num431 < 0f)
							{
								velocity.Y -= num428;
							}
						}
						ai[2] += 1f;
						if (ai[2] >= 600f)
						{
							ai[1] = 1f;
							ai[2] = 0f;
							ai[3] = 0f;
							target = 255;
							netUpdate = true;
						}
						else
						{
							if (!Main.player[target].dead)
							{
								ai[3] += 1f;
							}
							if (ai[3] >= 60f)
							{
								ai[3] = 0f;
								vector36 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
								num430 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector36.X;
								num431 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector36.Y;
								if (Main.netMode != 1)
								{
									float num433 = 12f;
									int num434 = 25;
									int num435 = 96;
									num432 = (float)Math.Sqrt(num430 * num430 + num431 * num431);
									num432 = num433 / num432;
									num430 *= num432;
									num431 *= num432;
									num430 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
									num431 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
									vector36.X += num430 * 4f;
									vector36.Y += num431 * 4f;
									Projectile.NewProjectile(vector36.X, vector36.Y, num430, num431, num435, num434, 0f, Main.myPlayer);
								}
							}
						}
					}
					else if (ai[1] == 1f)
					{
						rotation = num422;
						float num436 = 13f;
						Vector2 vector37 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num437 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector37.X;
						float num438 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector37.Y;
						float num439 = (float)Math.Sqrt(num437 * num437 + num438 * num438);
						num439 = num436 / num439;
						velocity.X = num437 * num439;
						velocity.Y = num438 * num439;
						ai[1] = 2f;
					}
					else if (ai[1] == 2f)
					{
						ai[2] += 1f;
						if (ai[2] >= 8f)
						{
							velocity.X *= 0.9f;
							velocity.Y *= 0.9f;
							if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
							{
								velocity.X = 0f;
							}
							if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
							{
								velocity.Y = 0f;
							}
						}
						else
						{
							rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
						}
						if (ai[2] >= 42f)
						{
							ai[3] += 1f;
							ai[2] = 0f;
							target = 255;
							rotation = num422;
							if (ai[3] >= 10f)
							{
								ai[1] = 0f;
								ai[3] = 0f;
							}
							else
							{
								ai[1] = 1f;
							}
						}
					}
					if ((double)life < (double)lifeMax * 0.5)
					{
						ai[0] = 1f;
						ai[1] = 0f;
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					return;
				}
				if (ai[0] == 1f || ai[0] == 2f)
				{
					if (ai[0] == 1f)
					{
						ai[2] += 0.005f;
						if ((double)ai[2] > 0.5)
						{
							ai[2] = 0.5f;
						}
					}
					else
					{
						ai[2] -= 0.005f;
						if (ai[2] < 0f)
						{
							ai[2] = 0f;
						}
					}
					rotation += ai[2];
					ai[1] += 1f;
					if (ai[1] == 100f)
					{
						ai[0] += 1f;
						ai[1] = 0f;
						if (ai[0] == 3f)
						{
							ai[2] = 0f;
						}
						else
						{
							Main.PlaySound(3, (int)base.position.X, (int)base.position.Y);
							for (int num440 = 0; num440 < 2; num440++)
							{
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 144);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(base.position, new Vector2((float)Config.syncedRand.Next(-30, 31) * 0.2f, (float)Config.syncedRand.Next(-30, 31) * 0.2f), 6);
							}
							for (int num441 = 0; num441 < 20; num441++)
							{
								Vector2 position35 = base.position;
								int width35 = base.width;
								int height35 = base.height;
								int num442 = 5;
								float speedX28 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								float speedY34 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
								int num443 = 0;
								Dust.NewDust(position35, width35, height35, num442, speedX28, speedY34, num443);
							}
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						}
					}
					Vector2 position36 = base.position;
					int width36 = base.width;
					int height36 = base.height;
					int num444 = 5;
					float speedX29 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					float speedY35 = (float)Config.syncedRand.Next(-30, 31) * 0.2f;
					int num445 = 0;
					Dust.NewDust(position36, width36, height36, num444, speedX29, speedY35, num445);
					velocity.X *= 0.98f;
					velocity.Y *= 0.98f;
					if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
					{
						velocity.X = 0f;
					}
					if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
					{
						velocity.Y = 0f;
					}
					return;
				}
				soundHit = 4;
				damage = (int)((double)defDamage * 1.5);
				defense = defDefense + 25;
				if (ai[1] == 0f)
				{
					float num446 = 4f;
					float num447 = 0.1f;
					int num448 = 1;
					if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)Main.player[target].width)
					{
						num448 = -1;
					}
					Vector2 vector38 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num449 = Main.player[target].position.X + (float)(Main.player[target].width / 2) + (float)(num448 * 180) - vector38.X;
					float num450 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector38.Y;
					float num451 = (float)Math.Sqrt(num449 * num449 + num450 * num450);
					num451 = num446 / num451;
					num449 *= num451;
					num450 *= num451;
					if (velocity.X < num449)
					{
						velocity.X += num447;
						if (velocity.X < 0f && num449 > 0f)
						{
							velocity.X += num447;
						}
					}
					else if (velocity.X > num449)
					{
						velocity.X -= num447;
						if (velocity.X > 0f && num449 < 0f)
						{
							velocity.X -= num447;
						}
					}
					if (velocity.Y < num450)
					{
						velocity.Y += num447;
						if (velocity.Y < 0f && num450 > 0f)
						{
							velocity.Y += num447;
						}
					}
					else if (velocity.Y > num450)
					{
						velocity.Y -= num447;
						if (velocity.Y > 0f && num450 < 0f)
						{
							velocity.Y -= num447;
						}
					}
					ai[2] += 1f;
					if (ai[2] >= 400f)
					{
						ai[1] = 1f;
						ai[2] = 0f;
						ai[3] = 0f;
						target = 255;
						netUpdate = true;
					}
					if (!Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
					{
						return;
					}
					localAI[2] += 1f;
					if (localAI[2] > 22f)
					{
						localAI[2] = 0f;
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 34);
					}
					if (Main.netMode != 1)
					{
						localAI[1] += 1f;
						if ((double)life < (double)lifeMax * 0.75)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.5)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.25)
						{
							localAI[1] += 1f;
						}
						if ((double)life < (double)lifeMax * 0.1)
						{
							localAI[1] += 2f;
						}
						if (localAI[1] > 8f)
						{
							localAI[1] = 0f;
							float num452 = 6f;
							int num453 = 30;
							int num454 = 101;
							vector38 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
							num449 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector38.X;
							num450 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector38.Y;
							num451 = (float)Math.Sqrt(num449 * num449 + num450 * num450);
							num451 = num452 / num451;
							num449 *= num451;
							num450 *= num451;
							num450 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							num449 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							num450 += velocity.Y * 0.5f;
							num449 += velocity.X * 0.5f;
							vector38.X -= num449 * 1f;
							vector38.Y -= num450 * 1f;
							Projectile.NewProjectile(vector38.X, vector38.Y, num449, num450, num454, num453, 0f, Main.myPlayer);
						}
					}
				}
				else if (ai[1] == 1f)
				{
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					rotation = num422;
					float num455 = 14f;
					Vector2 vector39 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num456 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector39.X;
					float num457 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector39.Y;
					float num458 = (float)Math.Sqrt(num456 * num456 + num457 * num457);
					num458 = num455 / num458;
					velocity.X = num456 * num458;
					velocity.Y = num457 * num458;
					ai[1] = 2f;
				}
				else
				{
					if (ai[1] != 2f)
					{
						return;
					}
					ai[2] += 1f;
					if (ai[2] >= 50f)
					{
						velocity.X *= 0.93f;
						velocity.Y *= 0.93f;
						if ((double)velocity.X > -0.1 && (double)velocity.X < 0.1)
						{
							velocity.X = 0f;
						}
						if ((double)velocity.Y > -0.1 && (double)velocity.Y < 0.1)
						{
							velocity.Y = 0f;
						}
					}
					else
					{
						rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 1.57f;
					}
					if (ai[2] >= 80f)
					{
						ai[3] += 1f;
						ai[2] = 0f;
						target = 255;
						rotation = num422;
						if (ai[3] >= 6f)
						{
							ai[1] = 0f;
							ai[3] = 0f;
						}
						else
						{
							ai[1] = 1f;
						}
					}
				}
			}
			else if (aiStyle == 32)
			{
				damage = defDamage;
				defense = defDefense;
				if (ai[0] == 0f && Main.netMode != 1)
				{
					TargetClosest();
					ai[0] = 1f;
					if (type != 68)
					{
						int num459 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 128, whoAmI);
						Main.npc[num459].ai[0] = -1f;
						Main.npc[num459].ai[1] = whoAmI;
						Main.npc[num459].target = target;
						Main.npc[num459].netUpdate = true;
						num459 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 129, whoAmI);
						Main.npc[num459].ai[0] = 1f;
						Main.npc[num459].ai[1] = whoAmI;
						Main.npc[num459].target = target;
						Main.npc[num459].netUpdate = true;
						num459 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 130, whoAmI);
						Main.npc[num459].ai[0] = -1f;
						Main.npc[num459].ai[1] = whoAmI;
						Main.npc[num459].target = target;
						Main.npc[num459].ai[3] = 150f;
						Main.npc[num459].netUpdate = true;
						num459 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)base.position.Y + base.height / 2, 131, whoAmI);
						Main.npc[num459].ai[0] = 1f;
						Main.npc[num459].ai[1] = whoAmI;
						Main.npc[num459].target = target;
						Main.npc[num459].netUpdate = true;
						Main.npc[num459].ai[3] = 150f;
					}
				}
				if (type == 68 && ai[1] != 3f && ai[1] != 2f)
				{
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					ai[1] = 2f;
				}
				if (Main.player[target].dead || Math.Abs(base.position.X - Main.player[target].position.X) > 6000f || Math.Abs(base.position.Y - Main.player[target].position.Y) > 6000f)
				{
					TargetClosest();
					if (Main.player[target].dead || Math.Abs(base.position.X - Main.player[target].position.X) > 6000f || Math.Abs(base.position.Y - Main.player[target].position.Y) > 6000f)
					{
						ai[1] = 3f;
					}
				}
				if (Main.dayTime && ai[1] != 3f && ai[1] != 2f)
				{
					ai[1] = 2f;
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
				}
				if (ai[1] == 0f)
				{
					ai[2] += 1f;
					if (ai[2] >= 600f)
					{
						ai[2] = 0f;
						ai[1] = 1f;
						TargetClosest();
						netUpdate = true;
					}
					rotation = velocity.X / 15f;
					if (base.position.Y > Main.player[target].position.Y - 200f)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.98f;
						}
						velocity.Y -= 0.1f;
						if (velocity.Y > 2f)
						{
							velocity.Y = 2f;
						}
					}
					else if (base.position.Y < Main.player[target].position.Y - 500f)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.98f;
						}
						velocity.Y += 0.1f;
						if (velocity.Y < -2f)
						{
							velocity.Y = -2f;
						}
					}
					if (base.position.X + (float)(base.width / 2) > Main.player[target].position.X + (float)(Main.player[target].width / 2) + 100f)
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.98f;
						}
						velocity.X -= 0.1f;
						if (velocity.X > 8f)
						{
							velocity.X = 8f;
						}
					}
					if (base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)(Main.player[target].width / 2) - 100f)
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.98f;
						}
						velocity.X += 0.1f;
						if (velocity.X < -8f)
						{
							velocity.X = -8f;
						}
					}
				}
				else if (ai[1] == 1f)
				{
					defense *= 2;
					damage *= 2;
					ai[2] += 1f;
					if (ai[2] == 2f)
					{
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					}
					if (ai[2] >= 400f)
					{
						ai[2] = 0f;
						ai[1] = 0f;
					}
					rotation += (float)direction * 0.3f;
					Vector2 vector40 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num460 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector40.X;
					float num461 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector40.Y;
					float num462 = (float)Math.Sqrt(num460 * num460 + num461 * num461);
					num462 = 2f / num462;
					velocity.X = num460 * num462;
					velocity.Y = num461 * num462;
				}
				else if (ai[1] == 2f)
				{
					damage = 9999;
					defense = 9999;
					rotation += (float)direction * 0.3f;
					Vector2 vector41 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num463 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector41.X;
					float num464 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector41.Y;
					float num465 = (float)Math.Sqrt(num463 * num463 + num464 * num464);
					num465 = 8f / num465;
					velocity.X = num463 * num465;
					velocity.Y = num464 * num465;
				}
				else if (ai[1] == 3f)
				{
					velocity.Y += 0.1f;
					if (velocity.Y < 0f)
					{
						velocity.Y *= 0.95f;
					}
					velocity.X *= 0.95f;
					if (timeLeft > 500)
					{
						timeLeft = 500;
					}
				}
			}
			else if (aiStyle == 33)
			{
				Vector2 vector42 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num466 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector42.X;
				float num467 = Main.npc[(int)ai[1]].position.Y + 230f - vector42.Y;
				float num468 = (float)Math.Sqrt(num466 * num466 + num467 * num467);
				if (ai[2] != 99f)
				{
					if (num468 > 800f)
					{
						ai[2] = 99f;
					}
				}
				else if (num468 < 400f)
				{
					ai[2] = 0f;
				}
				spriteDirection = -(int)ai[0];
				if (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != 32)
				{
					ai[2] += 10f;
					if (ai[2] > 50f || Main.netMode != 2)
					{
						life = -1;
						HitEffect();
						active = false;
					}
				}
				if (ai[2] == 99f)
				{
					if (base.position.Y > Main.npc[(int)ai[1]].position.Y)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.96f;
						}
						velocity.Y -= 0.1f;
						if (velocity.Y > 8f)
						{
							velocity.Y = 8f;
						}
					}
					else if (base.position.Y < Main.npc[(int)ai[1]].position.Y)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.96f;
						}
						velocity.Y += 0.1f;
						if (velocity.Y < -8f)
						{
							velocity.Y = -8f;
						}
					}
					if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.96f;
						}
						velocity.X -= 0.5f;
						if (velocity.X > 12f)
						{
							velocity.X = 12f;
						}
					}
					if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.96f;
						}
						velocity.X += 0.5f;
						if (velocity.X < -12f)
						{
							velocity.X = -12f;
						}
					}
				}
				else if (ai[2] == 0f || ai[2] == 3f)
				{
					if (Main.npc[(int)ai[1]].ai[1] == 3f && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (Main.npc[(int)ai[1]].ai[1] != 0f)
					{
						TargetClosest();
						if (Main.player[target].dead)
						{
							velocity.Y += 0.1f;
							if (velocity.Y > 16f)
							{
								velocity.Y = 16f;
							}
						}
						else
						{
							Vector2 vector43 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
							float num469 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector43.X;
							float num470 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector43.Y;
							float num471 = (float)Math.Sqrt(num469 * num469 + num470 * num470);
							num471 = 7f / num471;
							num469 *= num471;
							num470 *= num471;
							rotation = (float)Math.Atan2(num470, num469) - 1.57f;
							if (velocity.X > num469)
							{
								if (velocity.X > 0f)
								{
									velocity.X *= 0.97f;
								}
								velocity.X -= 0.05f;
							}
							if (velocity.X < num469)
							{
								if (velocity.X < 0f)
								{
									velocity.X *= 0.97f;
								}
								velocity.X += 0.05f;
							}
							if (velocity.Y > num470)
							{
								if (velocity.Y > 0f)
								{
									velocity.Y *= 0.97f;
								}
								velocity.Y -= 0.05f;
							}
							if (velocity.Y < num470)
							{
								if (velocity.Y < 0f)
								{
									velocity.Y *= 0.97f;
								}
								velocity.Y += 0.05f;
							}
						}
						ai[3] += 1f;
						if (ai[3] >= 600f)
						{
							ai[2] = 0f;
							ai[3] = 0f;
							netUpdate = true;
						}
					}
					else
					{
						ai[3] += 1f;
						if (ai[3] >= 300f)
						{
							ai[2] += 1f;
							ai[3] = 0f;
							netUpdate = true;
						}
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y + 320f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.04f;
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y + 260f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.04f;
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.3f;
							if (velocity.X > 12f)
							{
								velocity.X = 12f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 250f)
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.3f;
							if (velocity.X < -12f)
							{
								velocity.X = -12f;
							}
						}
					}
					Vector2 vector44 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num472 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector44.X;
					float num473 = Main.npc[(int)ai[1]].position.Y + 230f - vector44.Y;
					Math.Sqrt(num472 * num472 + num473 * num473);
					rotation = (float)Math.Atan2(num473, num472) + 1.57f;
				}
				else if (ai[2] == 1f)
				{
					Vector2 vector45 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num474 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector45.X;
					float num475 = Main.npc[(int)ai[1]].position.Y + 230f - vector45.Y;
					float num476 = (float)Math.Sqrt(num474 * num474 + num475 * num475);
					rotation = (float)Math.Atan2(num475, num474) + 1.57f;
					velocity.X *= 0.95f;
					velocity.Y -= 0.1f;
					if (velocity.Y < -8f)
					{
						velocity.Y = -8f;
					}
					if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 200f)
					{
						TargetClosest();
						ai[2] = 2f;
						vector45 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num474 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector45.X;
						num475 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector45.Y;
						num476 = (float)Math.Sqrt(num474 * num474 + num475 * num475);
						num476 = 22f / num476;
						velocity.X = num474 * num476;
						velocity.Y = num475 * num476;
						netUpdate = true;
					}
				}
				else if (ai[2] == 2f)
				{
					if (base.position.Y > Main.player[target].position.Y || velocity.Y < 0f)
					{
						ai[2] = 3f;
					}
				}
				else if (ai[2] == 4f)
				{
					TargetClosest();
					Vector2 vector46 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num477 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector46.X;
					float num478 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector46.Y;
					float num479 = (float)Math.Sqrt(num477 * num477 + num478 * num478);
					num479 = 7f / num479;
					num477 *= num479;
					num478 *= num479;
					if (velocity.X > num477)
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.97f;
						}
						velocity.X -= 0.05f;
					}
					if (velocity.X < num477)
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.97f;
						}
						velocity.X += 0.05f;
					}
					if (velocity.Y > num478)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.97f;
						}
						velocity.Y -= 0.05f;
					}
					if (velocity.Y < num478)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.97f;
						}
						velocity.Y += 0.05f;
					}
					ai[3] += 1f;
					if (ai[3] >= 600f)
					{
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					vector46 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					num477 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector46.X;
					num478 = Main.npc[(int)ai[1]].position.Y + 230f - vector46.Y;
					num479 = (float)Math.Sqrt(num477 * num477 + num478 * num478);
					rotation = (float)Math.Atan2(num478, num477) + 1.57f;
				}
				else if (ai[2] == 5f && ((velocity.X > 0f && base.position.X + (float)(base.width / 2) > Main.player[target].position.X + (float)(Main.player[target].width / 2)) || (velocity.X < 0f && base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)(Main.player[target].width / 2))))
				{
					ai[2] = 0f;
				}
			}
			else if (aiStyle == 34)
			{
				spriteDirection = -(int)ai[0];
				Vector2 vector47 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num480 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector47.X;
				float num481 = Main.npc[(int)ai[1]].position.Y + 230f - vector47.Y;
				float num482 = (float)Math.Sqrt(num480 * num480 + num481 * num481);
				if (ai[2] != 99f)
				{
					if (num482 > 800f)
					{
						ai[2] = 99f;
					}
				}
				else if (num482 < 400f)
				{
					ai[2] = 0f;
				}
				if (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != 32)
				{
					ai[2] += 10f;
					if (ai[2] > 50f || Main.netMode != 2)
					{
						life = -1;
						HitEffect();
						active = false;
					}
				}
				if (ai[2] == 99f)
				{
					if (base.position.Y > Main.npc[(int)ai[1]].position.Y)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.96f;
						}
						velocity.Y -= 0.1f;
						if (velocity.Y > 8f)
						{
							velocity.Y = 8f;
						}
					}
					else if (base.position.Y < Main.npc[(int)ai[1]].position.Y)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.96f;
						}
						velocity.Y += 0.1f;
						if (velocity.Y < -8f)
						{
							velocity.Y = -8f;
						}
					}
					if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.96f;
						}
						velocity.X -= 0.5f;
						if (velocity.X > 12f)
						{
							velocity.X = 12f;
						}
					}
					if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.96f;
						}
						velocity.X += 0.5f;
						if (velocity.X < -12f)
						{
							velocity.X = -12f;
						}
					}
				}
				else if (ai[2] == 0f || ai[2] == 3f)
				{
					if (Main.npc[(int)ai[1]].ai[1] == 3f && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (Main.npc[(int)ai[1]].ai[1] != 0f)
					{
						TargetClosest();
						TargetClosest();
						if (Main.player[target].dead)
						{
							velocity.Y += 0.1f;
							if (velocity.Y > 16f)
							{
								velocity.Y = 16f;
							}
						}
						else
						{
							Vector2 vector48 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
							float num483 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector48.X;
							float num484 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector48.Y;
							float num485 = (float)Math.Sqrt(num483 * num483 + num484 * num484);
							num485 = 12f / num485;
							num483 *= num485;
							num484 *= num485;
							rotation = (float)Math.Atan2(num484, num483) - 1.57f;
							if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < 2f)
							{
								rotation = (float)Math.Atan2(num484, num483) - 1.57f;
								velocity.X = num483;
								velocity.Y = num484;
								netUpdate = true;
							}
							else
							{
								velocity *= 0.97f;
							}
							ai[3] += 1f;
							if (ai[3] >= 600f)
							{
								ai[2] = 0f;
								ai[3] = 0f;
								netUpdate = true;
							}
						}
					}
					else
					{
						ai[3] += 1f;
						if (ai[3] >= 600f)
						{
							ai[2] += 1f;
							ai[3] = 0f;
							netUpdate = true;
						}
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y + 300f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.1f;
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y + 230f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.1f;
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) + 250f)
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.94f;
							}
							velocity.X -= 0.3f;
							if (velocity.X > 9f)
							{
								velocity.X = 9f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2))
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.94f;
							}
							velocity.X += 0.2f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					Vector2 vector49 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num486 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector49.X;
					float num487 = Main.npc[(int)ai[1]].position.Y + 230f - vector49.Y;
					Math.Sqrt(num486 * num486 + num487 * num487);
					rotation = (float)Math.Atan2(num487, num486) + 1.57f;
				}
				else if (ai[2] == 1f)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y *= 0.9f;
					}
					Vector2 vector50 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num488 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 280f * ai[0] - vector50.X;
					float num489 = Main.npc[(int)ai[1]].position.Y + 230f - vector50.Y;
					float num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
					rotation = (float)Math.Atan2(num489, num488) + 1.57f;
					velocity.X = (velocity.X * 5f + Main.npc[(int)ai[1]].velocity.X) / 6f;
					velocity.X += 0.5f;
					velocity.Y -= 0.5f;
					if (velocity.Y < -9f)
					{
						velocity.Y = -9f;
					}
					if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 280f)
					{
						TargetClosest();
						ai[2] = 2f;
						vector50 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num488 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector50.X;
						num489 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector50.Y;
						num490 = (float)Math.Sqrt(num488 * num488 + num489 * num489);
						num490 = 20f / num490;
						velocity.X = num488 * num490;
						velocity.Y = num489 * num490;
						netUpdate = true;
					}
				}
				else if (ai[2] == 2f)
				{
					if (base.position.Y > Main.player[target].position.Y || velocity.Y < 0f)
					{
						if (ai[3] >= 4f)
						{
							ai[2] = 3f;
							ai[3] = 0f;
						}
						else
						{
							ai[2] = 1f;
							ai[3] += 1f;
						}
					}
				}
				else if (ai[2] == 4f)
				{
					Vector2 vector51 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num491 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector51.X;
					float num492 = Main.npc[(int)ai[1]].position.Y + 230f - vector51.Y;
					float num493 = (float)Math.Sqrt(num491 * num491 + num492 * num492);
					rotation = (float)Math.Atan2(num492, num491) + 1.57f;
					velocity.Y = (velocity.Y * 5f + Main.npc[(int)ai[1]].velocity.Y) / 6f;
					velocity.X += 0.5f;
					if (velocity.X > 12f)
					{
						velocity.X = 12f;
					}
					if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 500f || base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) + 500f)
					{
						TargetClosest();
						ai[2] = 5f;
						vector51 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num491 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector51.X;
						num492 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector51.Y;
						num493 = (float)Math.Sqrt(num491 * num491 + num492 * num492);
						num493 = 17f / num493;
						velocity.X = num491 * num493;
						velocity.Y = num492 * num493;
						netUpdate = true;
					}
				}
				else if (ai[2] == 5f && base.position.X + (float)(base.width / 2) < Main.player[target].position.X + (float)(Main.player[target].width / 2) - 100f)
				{
					if (ai[3] >= 4f)
					{
						ai[2] = 0f;
						ai[3] = 0f;
					}
					else
					{
						ai[2] = 4f;
						ai[3] += 1f;
					}
				}
			}
			else if (aiStyle == 35)
			{
				spriteDirection = -(int)ai[0];
				if (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != 32)
				{
					ai[2] += 10f;
					if (ai[2] > 50f || Main.netMode != 2)
					{
						life = -1;
						HitEffect();
						active = false;
					}
				}
				if (ai[2] == 0f)
				{
					if (Main.npc[(int)ai[1]].ai[1] == 3f && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (Main.npc[(int)ai[1]].ai[1] != 0f)
					{
						localAI[0] += 2f;
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.07f;
							if (velocity.Y > 6f)
							{
								velocity.Y = 6f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.07f;
							if (velocity.Y < -6f)
							{
								velocity.Y = -6f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.1f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.1f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					else
					{
						ai[3] += 1f;
						if (ai[3] >= 1100f)
						{
							localAI[0] = 0f;
							ai[2] = 1f;
							ai[3] = 0f;
							netUpdate = true;
						}
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y - 150f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.04f;
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 150f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.04f;
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) + 200f)
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.2f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) + 160f)
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.2f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					Vector2 vector52 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num494 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 200f * ai[0] - vector52.X;
					float num495 = Main.npc[(int)ai[1]].position.Y + 230f - vector52.Y;
					float num496 = (float)Math.Sqrt(num494 * num494 + num495 * num495);
					rotation = (float)Math.Atan2(num495, num494) + 1.57f;
					if (Main.netMode != 1)
					{
						localAI[0] += 1f;
						if (localAI[0] > 140f)
						{
							localAI[0] = 0f;
							float num497 = 12f;
							int num498 = 0;
							int num499 = 102;
							num496 = num497 / num496;
							num494 = (0f - num494) * num496;
							num495 = (0f - num495) * num496;
							num494 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							num495 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							vector52.X += num494 * 4f;
							vector52.Y += num495 * 4f;
							Projectile.NewProjectile(vector52.X, vector52.Y, num494, num495, num499, num498, 0f, Main.myPlayer);
						}
					}
				}
				else
				{
					if (ai[2] != 1f)
					{
						return;
					}
					ai[3] += 1f;
					if (ai[3] >= 300f)
					{
						localAI[0] = 0f;
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					Vector2 vector53 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num500 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - vector53.X;
					float num501 = Main.npc[(int)ai[1]].position.Y - vector53.Y;
					num501 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 80f - vector53.Y;
					float num502 = (float)Math.Sqrt(num500 * num500 + num501 * num501);
					num502 = 6f / num502;
					num500 *= num502;
					num501 *= num502;
					if (velocity.X > num500)
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.9f;
						}
						velocity.X -= 0.04f;
					}
					if (velocity.X < num500)
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.9f;
						}
						velocity.X += 0.04f;
					}
					if (velocity.Y > num501)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y -= 0.08f;
					}
					if (velocity.Y < num501)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y += 0.08f;
					}
					TargetClosest();
					vector53 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					num500 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector53.X;
					num501 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector53.Y;
					num502 = (float)Math.Sqrt(num500 * num500 + num501 * num501);
					rotation = (float)Math.Atan2(num501, num500) - 1.57f;
					if (Main.netMode != 1)
					{
						localAI[0] += 1f;
						if (localAI[0] > 40f)
						{
							localAI[0] = 0f;
							float num503 = 10f;
							int num504 = 0;
							int num505 = 102;
							num502 = num503 / num502;
							num500 *= num502;
							num501 *= num502;
							num500 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							num501 += (float)Config.syncedRand.Next(-40, 41) * 0.01f;
							vector53.X += num500 * 4f;
							vector53.Y += num501 * 4f;
							Projectile.NewProjectile(vector53.X, vector53.Y, num500, num501, num505, num504, 0f, Main.myPlayer);
						}
					}
				}
			}
			else if (aiStyle == 36)
			{
				spriteDirection = -(int)ai[0];
				if (!Main.npc[(int)ai[1]].active || Main.npc[(int)ai[1]].aiStyle != 32)
				{
					ai[2] += 10f;
					if (ai[2] > 50f || Main.netMode != 2)
					{
						life = -1;
						HitEffect();
						active = false;
					}
				}
				if (ai[2] == 0f || ai[2] == 3f)
				{
					if (Main.npc[(int)ai[1]].ai[1] == 3f && timeLeft > 10)
					{
						timeLeft = 10;
					}
					if (Main.npc[(int)ai[1]].ai[1] != 0f)
					{
						localAI[0] += 3f;
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.07f;
							if (velocity.Y > 6f)
							{
								velocity.Y = 6f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.07f;
							if (velocity.Y < -6f)
							{
								velocity.Y = -6f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.1f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 120f * ai[0])
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.1f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					else
					{
						ai[3] += 1f;
						if (ai[3] >= 800f)
						{
							ai[2] += 1f;
							ai[3] = 0f;
							netUpdate = true;
						}
						if (base.position.Y > Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y -= 0.1f;
							if (velocity.Y > 3f)
							{
								velocity.Y = 3f;
							}
						}
						else if (base.position.Y < Main.npc[(int)ai[1]].position.Y - 100f)
						{
							if (velocity.Y < 0f)
							{
								velocity.Y *= 0.96f;
							}
							velocity.Y += 0.1f;
							if (velocity.Y < -3f)
							{
								velocity.Y = -3f;
							}
						}
						if (base.position.X + (float)(base.width / 2) > Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 180f * ai[0])
						{
							if (velocity.X > 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X -= 0.14f;
							if (velocity.X > 8f)
							{
								velocity.X = 8f;
							}
						}
						if (base.position.X + (float)(base.width / 2) < Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - 180f * ai[0])
						{
							if (velocity.X < 0f)
							{
								velocity.X *= 0.96f;
							}
							velocity.X += 0.14f;
							if (velocity.X < -8f)
							{
								velocity.X = -8f;
							}
						}
					}
					TargetClosest();
					Vector2 vector54 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num506 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector54.X;
					float num507 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector54.Y;
					float num508 = (float)Math.Sqrt(num506 * num506 + num507 * num507);
					rotation = (float)Math.Atan2(num507, num506) - 1.57f;
					if (Main.netMode != 1)
					{
						localAI[0] += 1f;
						if (localAI[0] > 200f)
						{
							localAI[0] = 0f;
							float num509 = 8f;
							int num510 = 25;
							int num511 = 100;
							num508 = num509 / num508;
							num506 *= num508;
							num507 *= num508;
							num506 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
							num507 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
							vector54.X += num506 * 8f;
							vector54.Y += num507 * 8f;
							Projectile.NewProjectile(vector54.X, vector54.Y, num506, num507, num511, num510, 0f, Main.myPlayer);
						}
					}
				}
				else
				{
					if (ai[2] != 1f)
					{
						return;
					}
					ai[3] += 1f;
					if (ai[3] >= 200f)
					{
						localAI[0] = 0f;
						ai[2] = 0f;
						ai[3] = 0f;
						netUpdate = true;
					}
					Vector2 vector55 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num512 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - 350f - vector55.X;
					float num513 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - 20f - vector55.Y;
					float num514 = (float)Math.Sqrt(num512 * num512 + num513 * num513);
					num514 = 7f / num514;
					num512 *= num514;
					num513 *= num514;
					if (velocity.X > num512)
					{
						if (velocity.X > 0f)
						{
							velocity.X *= 0.9f;
						}
						velocity.X -= 0.1f;
					}
					if (velocity.X < num512)
					{
						if (velocity.X < 0f)
						{
							velocity.X *= 0.9f;
						}
						velocity.X += 0.1f;
					}
					if (velocity.Y > num513)
					{
						if (velocity.Y > 0f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y -= 0.03f;
					}
					if (velocity.Y < num513)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y += 0.03f;
					}
					TargetClosest();
					vector55 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					num512 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector55.X;
					num513 = Main.player[target].position.Y + (float)(Main.player[target].height / 2) - vector55.Y;
					num514 = (float)Math.Sqrt(num512 * num512 + num513 * num513);
					rotation = (float)Math.Atan2(num513, num512) - 1.57f;
					if (Main.netMode == 1)
					{
						localAI[0] += 1f;
						if (localAI[0] > 80f)
						{
							localAI[0] = 0f;
							float num515 = 10f;
							int num516 = 25;
							int num517 = 100;
							num514 = num515 / num514;
							num512 *= num514;
							num513 *= num514;
							num512 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
							num513 += (float)Config.syncedRand.Next(-40, 41) * 0.05f;
							vector55.X += num512 * 8f;
							vector55.Y += num513 * 8f;
							Projectile.NewProjectile(vector55.X, vector55.Y, num512, num513, num517, num516, 0f, Main.myPlayer);
						}
					}
				}
			}
			else if (aiStyle == 37)
			{
				if (ai[3] > 0f)
				{
					realLife = (int)ai[3];
				}
				if (target < 0 || target == 255 || Main.player[target].dead)
				{
					TargetClosest();
				}
				if (type > 134)
				{
					bool flag32 = false;
					if (ai[1] <= 0f)
					{
						flag32 = true;
					}
					else if (Main.npc[(int)ai[1]].life <= 0)
					{
						flag32 = true;
					}
					if (flag32)
					{
						life = 0;
						HitEffect();
						checkDead();
					}
				}
				if (Main.netMode != 1)
				{
					if (ai[0] == 0f && type == 134)
					{
						ai[3] = whoAmI;
						realLife = whoAmI;
						int num518 = whoAmI;
						int num519 = 80;
						for (int num520 = 0; num520 <= num519; num520++)
						{
							int num521 = 135;
							if (num520 == num519)
							{
								num521 = 136;
							}
							int num522 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), num521, whoAmI);
							Main.npc[num522].ai[3] = whoAmI;
							Main.npc[num522].realLife = whoAmI;
							Main.npc[num522].ai[1] = num518;
							Main.npc[num518].ai[0] = num522;
							NetMessage.SendData(23, -1, -1, "", num522);
							num518 = num522;
						}
					}
					if (type == 135)
					{
						localAI[0] += Config.syncedRand.Next(4);
						if (localAI[0] >= (float)Config.syncedRand.Next(1400, 26000))
						{
							localAI[0] = 0f;
							TargetClosest();
							if (Collision.CanHit(base.position, base.width, base.height, Main.player[target].position, Main.player[target].width, Main.player[target].height))
							{
								float num523 = 8f;
								Vector2 vector56 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)(base.height / 2));
								float num524 = Main.player[target].position.X + (float)Main.player[target].width * 0.5f - vector56.X + (float)Config.syncedRand.Next(-20, 21);
								float num525 = Main.player[target].position.Y + (float)Main.player[target].height * 0.5f - vector56.Y + (float)Config.syncedRand.Next(-20, 21);
								float num526 = (float)Math.Sqrt(num524 * num524 + num525 * num525);
								num526 = num523 / num526;
								num524 *= num526;
								num525 *= num526;
								num524 += (float)Config.syncedRand.Next(-20, 21) * 0.05f;
								num525 += (float)Config.syncedRand.Next(-20, 21) * 0.05f;
								int num527 = 22;
								int num528 = 100;
								vector56.X += num524 * 5f;
								vector56.Y += num525 * 5f;
								int num529 = Projectile.NewProjectile(vector56.X, vector56.Y, num524, num525, num528, num527, 0f, Main.myPlayer);
								Main.projectile[num529].timeLeft = 300;
								netUpdate = true;
							}
						}
					}
				}
				int num530 = (int)(base.position.X / 16f) - 1;
				int num531 = (int)((base.position.X + (float)base.width) / 16f) + 2;
				int num532 = (int)(base.position.Y / 16f) - 1;
				int num533 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
				if (num530 < 0)
				{
					num530 = 0;
				}
				if (num531 > Main.maxTilesX)
				{
					num531 = Main.maxTilesX;
				}
				if (num532 < 0)
				{
					num532 = 0;
				}
				if (num533 > Main.maxTilesY)
				{
					num533 = Main.maxTilesY;
				}
				bool flag33 = false;
				if (!flag33)
				{
					Vector2 vector57 = default(Vector2);
					for (int num534 = num530; num534 < num531; num534++)
					{
						for (int num535 = num532; num535 < num533; num535++)
						{
							if (Main.tile[num534, num535] != null && ((Main.tile[num534, num535].active && (Main.tileSolid[Main.tile[num534, num535].type] || (Main.tileSolidTop[Main.tile[num534, num535].type] && Main.tile[num534, num535].frameY == 0))) || Main.tile[num534, num535].liquid > 64))
							{
								vector57.X = num534 * 16;
								vector57.Y = num535 * 16;
								if (base.position.X + (float)base.width > vector57.X && base.position.X < vector57.X + 16f && base.position.Y + (float)base.height > vector57.Y && base.position.Y < vector57.Y + 16f)
								{
									flag33 = true;
									break;
								}
							}
						}
					}
				}
				if (!flag33)
				{
					if (type != 135 || ai[2] != 1f)
					{
						Lighting.addLight((int)((base.position.X + (float)(base.width / 2)) / 16f), (int)((base.position.Y + (float)(base.height / 2)) / 16f), 0.3f, 0.1f, 0.05f);
					}
					localAI[1] = 1f;
					if (type == 134)
					{
						Rectangle rectangle7 = new Rectangle((int)base.position.X, (int)base.position.Y, base.width, base.height);
						int num536 = 1000;
						bool flag34 = true;
						if (base.position.Y > Main.player[target].position.Y)
						{
							for (int num537 = 0; num537 < 255; num537++)
							{
								if (Main.player[num537].active)
								{
									Rectangle rectangle8 = new Rectangle((int)Main.player[num537].position.X - num536, (int)Main.player[num537].position.Y - num536, num536 * 2, num536 * 2);
									if (rectangle7.Intersects(rectangle8))
									{
										flag34 = false;
										break;
									}
								}
							}
							if (flag34)
							{
								flag33 = true;
							}
						}
					}
				}
				else
				{
					localAI[1] = 0f;
				}
				float num538 = 16f;
				if (Main.dayTime || Main.player[target].dead)
				{
					flag33 = false;
					velocity.Y += 1f;
					if ((double)base.position.Y > Main.worldSurface * 16.0)
					{
						velocity.Y += 1f;
						num538 = 32f;
					}
					if ((double)base.position.Y > Main.rockLayer * 16.0)
					{
						for (int num539 = 0; num539 < 200; num539++)
						{
							if (Main.npc[num539].aiStyle == aiStyle)
							{
								Main.npc[num539].active = false;
							}
						}
					}
				}
				float num540 = 0.1f;
				float num541 = 0.15f;
				Vector2 vector58 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num542 = Main.player[target].position.X + (float)(Main.player[target].width / 2);
				float num543 = Main.player[target].position.Y + (float)(Main.player[target].height / 2);
				num542 = (int)(num542 / 16f) * 16;
				num543 = (int)(num543 / 16f) * 16;
				vector58.X = (int)(vector58.X / 16f) * 16;
				vector58.Y = (int)(vector58.Y / 16f) * 16;
				num542 -= vector58.X;
				num543 -= vector58.Y;
				float num544 = (float)Math.Sqrt(num542 * num542 + num543 * num543);
				if (ai[1] > 0f && ai[1] < (float)Main.npc.Length)
				{
					try
					{
						vector58 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						num542 = Main.npc[(int)ai[1]].position.X + (float)(Main.npc[(int)ai[1]].width / 2) - vector58.X;
						num543 = Main.npc[(int)ai[1]].position.Y + (float)(Main.npc[(int)ai[1]].height / 2) - vector58.Y;
					}
					catch
					{
					}
					rotation = (float)Math.Atan2(num543, num542) + 1.57f;
					num544 = (float)Math.Sqrt(num542 * num542 + num543 * num543);
					int num545 = (int)(44f * scale);
					num544 = (num544 - (float)num545) / num544;
					num542 *= num544;
					num543 *= num544;
					velocity = default(Vector2);
					base.position.X = base.position.X + num542;
					base.position.Y = base.position.Y + num543;
					return;
				}
				if (!flag33)
				{
					TargetClosest();
					velocity.Y += 0.15f;
					if (velocity.Y > num538)
					{
						velocity.Y = num538;
					}
					if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num538 * 0.4)
					{
						if (velocity.X < 0f)
						{
							velocity.X -= num540 * 1.1f;
						}
						else
						{
							velocity.X += num540 * 1.1f;
						}
					}
					else if (velocity.Y == num538)
					{
						if (velocity.X < num542)
						{
							velocity.X += num540;
						}
						else if (velocity.X > num542)
						{
							velocity.X -= num540;
						}
					}
					else if (velocity.Y > 4f)
					{
						if (velocity.X < 0f)
						{
							velocity.X += num540 * 0.9f;
						}
						else
						{
							velocity.X -= num540 * 0.9f;
						}
					}
				}
				else
				{
					if (soundDelay == 0)
					{
						float num546 = num544 / 40f;
						if (num546 < 10f)
						{
							num546 = 10f;
						}
						if (num546 > 20f)
						{
							num546 = 20f;
						}
						soundDelay = (int)num546;
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y);
					}
					num544 = (float)Math.Sqrt(num542 * num542 + num543 * num543);
					float num547 = Math.Abs(num542);
					float num548 = Math.Abs(num543);
					float num549 = num538 / num544;
					num542 *= num549;
					num543 *= num549;
					if (((velocity.X > 0f && num542 > 0f) || (velocity.X < 0f && num542 < 0f)) && ((velocity.Y > 0f && num543 > 0f) || (velocity.Y < 0f && num543 < 0f)))
					{
						if (velocity.X < num542)
						{
							velocity.X += num541;
						}
						else if (velocity.X > num542)
						{
							velocity.X -= num541;
						}
						if (velocity.Y < num543)
						{
							velocity.Y += num541;
						}
						else if (velocity.Y > num543)
						{
							velocity.Y -= num541;
						}
					}
					if ((velocity.X > 0f && num542 > 0f) || (velocity.X < 0f && num542 < 0f) || (velocity.Y > 0f && num543 > 0f) || (velocity.Y < 0f && num543 < 0f))
					{
						if (velocity.X < num542)
						{
							velocity.X += num540;
						}
						else if (velocity.X > num542)
						{
							velocity.X -= num540;
						}
						if (velocity.Y < num543)
						{
							velocity.Y += num540;
						}
						else if (velocity.Y > num543)
						{
							velocity.Y -= num540;
						}
						if ((double)Math.Abs(num543) < (double)num538 * 0.2 && ((velocity.X > 0f && num542 < 0f) || (velocity.X < 0f && num542 > 0f)))
						{
							if (velocity.Y > 0f)
							{
								velocity.Y += num540 * 2f;
							}
							else
							{
								velocity.Y -= num540 * 2f;
							}
						}
						if ((double)Math.Abs(num542) < (double)num538 * 0.2 && ((velocity.Y > 0f && num543 < 0f) || (velocity.Y < 0f && num543 > 0f)))
						{
							if (velocity.X > 0f)
							{
								velocity.X += num540 * 2f;
							}
							else
							{
								velocity.X -= num540 * 2f;
							}
						}
					}
					else if (num547 > num548)
					{
						if (velocity.X < num542)
						{
							velocity.X += num540 * 1.1f;
						}
						else if (velocity.X > num542)
						{
							velocity.X -= num540 * 1.1f;
						}
						if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num538 * 0.5)
						{
							if (velocity.Y > 0f)
							{
								velocity.Y += num540;
							}
							else
							{
								velocity.Y -= num540;
							}
						}
					}
					else
					{
						if (velocity.Y < num543)
						{
							velocity.Y += num540 * 1.1f;
						}
						else if (velocity.Y > num543)
						{
							velocity.Y -= num540 * 1.1f;
						}
						if ((double)(Math.Abs(velocity.X) + Math.Abs(velocity.Y)) < (double)num538 * 0.5)
						{
							if (velocity.X > 0f)
							{
								velocity.X += num540;
							}
							else
							{
								velocity.X -= num540;
							}
						}
					}
				}
				rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
				if (type != 134)
				{
					return;
				}
				if (flag33)
				{
					if (localAI[0] != 1f)
					{
						netUpdate = true;
					}
					localAI[0] = 1f;
				}
				else
				{
					if (localAI[0] != 0f)
					{
						netUpdate = true;
					}
					localAI[0] = 0f;
				}
				if (((velocity.X > 0f && oldVelocity.X < 0f) || (velocity.X < 0f && oldVelocity.X > 0f) || (velocity.Y > 0f && oldVelocity.Y < 0f) || (velocity.Y < 0f && oldVelocity.Y > 0f)) && !justHit)
				{
					netUpdate = true;
				}
			}
			else
			{
				if (aiStyle != 38)
				{
					return;
				}
				float num550 = 4f;
				float num551 = 1f;
				if (type == 143)
				{
					num550 = 3f;
					num551 = 0.7f;
				}
				if (type == 145)
				{
					num550 = 3.5f;
					num551 = 0.8f;
				}
				if (type == 143)
				{
					ai[2] += 1f;
					if (ai[2] >= 120f)
					{
						ai[2] = 0f;
						if (Main.netMode != 1)
						{
							Vector2 vector59 = new Vector2(base.position.X + (float)base.width * 0.5f - (float)(direction * 12), base.position.Y + (float)base.height * 0.5f);
							float speedX30 = 12 * spriteDirection;
							float speedY36 = 0f;
							if (Main.netMode != 1)
							{
								int num552 = 25;
								int num553 = 110;
								int num554 = Projectile.NewProjectile(vector59.X, vector59.Y, speedX30, speedY36, num553, num552, 0f, Main.myPlayer);
								Main.projectile[num554].ai[0] = 2f;
								Main.projectile[num554].timeLeft = 300;
								Main.projectile[num554].friendly = false;
								NetMessage.SendData(27, -1, -1, "", num554);
								netUpdate = true;
							}
						}
					}
				}
				if (type == 144 && ai[1] >= 3f)
				{
					TargetClosest();
					spriteDirection = direction;
					if (velocity.Y == 0f)
					{
						velocity.X *= 0.9f;
						ai[2] += 1f;
						if ((double)velocity.X > -0.3 && (double)velocity.X < 0.3)
						{
							velocity.X = 0f;
						}
						if (ai[2] >= 200f)
						{
							ai[2] = 0f;
							ai[1] = 0f;
						}
					}
				}
				else if (type == 145 && ai[1] >= 3f)
				{
					TargetClosest();
					if (velocity.Y == 0f)
					{
						velocity.X *= 0.9f;
						ai[2] += 1f;
						if ((double)velocity.X > -0.3 && (double)velocity.X < 0.3)
						{
							velocity.X = 0f;
						}
						if (ai[2] >= 16f)
						{
							ai[2] = 0f;
							ai[1] = 0f;
						}
					}
					if (velocity.X == 0f && velocity.Y == 0f && ai[2] == 8f)
					{
						float num555 = 10f;
						Vector2 vector60 = new Vector2(base.position.X + (float)base.width * 0.5f - (float)(direction * 12), base.position.Y + (float)base.height * 0.25f);
						float num556 = Main.player[target].position.X + (float)(Main.player[target].width / 2) - vector60.X;
						float num557 = Main.player[target].position.Y - vector60.Y;
						float num558 = (float)Math.Sqrt(num556 * num556 + num557 * num557);
						num558 = num555 / num558;
						num556 *= num558;
						num557 *= num558;
						if (Main.netMode != 1)
						{
							int num559 = 35;
							int num560 = 109;
							int num561 = Projectile.NewProjectile(vector60.X, vector60.Y, num556, num557, num560, num559, 0f, Main.myPlayer);
							Main.projectile[num561].ai[0] = 2f;
							Main.projectile[num561].timeLeft = 300;
							Main.projectile[num561].friendly = false;
							NetMessage.SendData(27, -1, -1, "", num561);
							netUpdate = true;
						}
					}
				}
				else
				{
					if (velocity.Y == 0f)
					{
						if (localAI[2] == base.position.X)
						{
							direction *= -1;
							ai[3] = 60f;
						}
						localAI[2] = base.position.X;
						if (ai[3] == 0f)
						{
							TargetClosest();
						}
						ai[0] += 1f;
						if (ai[0] > 2f)
						{
							ai[0] = 0f;
							ai[1] += 1f;
							velocity.Y = -8.2f;
							velocity.X += (float)direction * num551 * 1.1f;
						}
						else
						{
							velocity.Y = -6f;
							velocity.X += (float)direction * num551 * 0.9f;
						}
						spriteDirection = direction;
					}
					velocity.X += (float)direction * num551 * 0.01f;
				}
				if (ai[3] > 0f)
				{
					ai[3] -= 1f;
				}
				if (velocity.X > num550 && direction > 0)
				{
					velocity.X = 4f;
				}
				if (velocity.X < 0f - num550 && direction < 0)
				{
					velocity.X = -4f;
				}
			}
		}

		public int getFrame()
		{
			int num = Main.npcFrameCount[type];
			int num2 = Main.npcTexture[type].Height / num;
			return frame.Y / num2;
		}

		public void FindFrame()
		{
			int num = 1;
			if (!Main.dedServ)
			{
				if (RunMethod("FindFrame", getFrame()))
				{
					return;
				}
				num = Main.npcTexture[type].Height / Main.npcFrameCount[type];
			}
			int num2 = type;
			int num3 = type;
			if (name != null && Config.npcDefs.animationType.TryGetValue(name, out num3))
			{
				type = num3;
				FindFrameReal(num);
				type = num2;
			}
			else
			{
				FindFrameReal(num);
			}
		}

		public void FindFrameReal(int num)
		{
			int num2 = 0;
			if (aiAction == 0)
			{
				num2 = ((velocity.Y < 0f) ? 2 : ((velocity.Y > 0f) ? 3 : ((velocity.X != 0f) ? 1 : 0)));
			}
			else if (aiAction == 1)
			{
				num2 = 4;
			}
			if (type == 1 || type == 16 || type == 59 || type == 71 || type == 81 || type == 138)
			{
				frameCounter += 1.0;
				if (num2 > 0)
				{
					frameCounter += 1.0;
				}
				if (num2 == 4)
				{
					frameCounter += 1.0;
				}
				if (frameCounter >= 8.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			if (type == 141)
			{
				spriteDirection = direction;
				if (velocity.Y != 0f)
				{
					frame.Y = num * 2;
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter >= 8.0)
					{
						frame.Y += num;
						frameCounter = 0.0;
					}
					if (frame.Y > num)
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 143)
			{
				if (velocity.Y > 0f)
				{
					frameCounter += 1.0;
				}
				else if (velocity.Y < 0f)
				{
					frameCounter -= 1.0;
				}
				if (frameCounter < 6.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num * 2;
				}
				else if (frameCounter < 18.0)
				{
					frame.Y = num * 3;
				}
				if (frameCounter < 0.0)
				{
					frameCounter = 0.0;
				}
				if (frameCounter > 17.0)
				{
					frameCounter = 17.0;
				}
			}
			if (type == 144)
			{
				if (velocity.X == 0f && velocity.Y == 0f)
				{
					localAI[3] += 1f;
					if (localAI[3] < 6f)
					{
						frame.Y = 0;
					}
					else if (localAI[3] < 12f)
					{
						frame.Y = num;
					}
					if (localAI[3] >= 11f)
					{
						localAI[3] = 0f;
					}
				}
				else
				{
					if (velocity.Y > 0f)
					{
						frameCounter += 1.0;
					}
					else if (velocity.Y < 0f)
					{
						frameCounter -= 1.0;
					}
					if (frameCounter < 6.0)
					{
						frame.Y = num * 2;
					}
					else if (frameCounter < 12.0)
					{
						frame.Y = num * 3;
					}
					else if (frameCounter < 18.0)
					{
						frame.Y = num * 4;
					}
					if (frameCounter < 0.0)
					{
						frameCounter = 0.0;
					}
					if (frameCounter > 17.0)
					{
						frameCounter = 17.0;
					}
				}
			}
			if (type == 145)
			{
				if (velocity.X == 0f && velocity.Y == 0f)
				{
					if (ai[2] < 4f)
					{
						frame.Y = 0;
					}
					else if (ai[2] < 8f)
					{
						frame.Y = num;
					}
					else if (ai[2] < 12f)
					{
						frame.Y = num * 2;
					}
					else if (ai[2] < 16f)
					{
						frame.Y = num * 3;
					}
				}
				else
				{
					if (velocity.Y > 0f)
					{
						frameCounter += 1.0;
					}
					else if (velocity.Y < 0f)
					{
						frameCounter -= 1.0;
					}
					if (frameCounter < 6.0)
					{
						frame.Y = num * 4;
					}
					else if (frameCounter < 12.0)
					{
						frame.Y = num * 5;
					}
					else if (frameCounter < 18.0)
					{
						frame.Y = num * 6;
					}
					if (frameCounter < 0.0)
					{
						frameCounter = 0.0;
					}
					if (frameCounter > 17.0)
					{
						frameCounter = 17.0;
					}
				}
			}
			if (type == 50)
			{
				if (velocity.Y != 0f)
				{
					frame.Y = num * 4;
				}
				else
				{
					frameCounter += 1.0;
					if (num2 > 0)
					{
						frameCounter += 1.0;
					}
					if (num2 == 4)
					{
						frameCounter += 1.0;
					}
					if (frameCounter >= 8.0)
					{
						frame.Y += num;
						frameCounter = 0.0;
					}
					if (frame.Y >= num * 4)
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 135)
			{
				if (ai[2] == 0f)
				{
					frame.Y = 0;
				}
				else
				{
					frame.Y = num;
				}
			}
			if (type == 85)
			{
				if (ai[0] == 0f)
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
				else
				{
					int num3 = 3;
					if (velocity.Y == 0f)
					{
						frameCounter -= 1.0;
					}
					else
					{
						frameCounter += 1.0;
					}
					if (frameCounter < 0.0)
					{
						frameCounter = 0.0;
					}
					if (frameCounter > (double)(num3 * 4))
					{
						frameCounter = num3 * 4;
					}
					if (frameCounter < (double)num3)
					{
						frame.Y = num;
					}
					else if (frameCounter < (double)(num3 * 2))
					{
						frame.Y = num * 2;
					}
					else if (frameCounter < (double)(num3 * 3))
					{
						frame.Y = num * 3;
					}
					else if (frameCounter < (double)(num3 * 4))
					{
						frame.Y = num * 4;
					}
					else if (frameCounter < (double)(num3 * 5))
					{
						frame.Y = num * 5;
					}
					else if (frameCounter < (double)(num3 * 6))
					{
						frame.Y = num * 4;
					}
					else if (frameCounter < (double)(num3 * 7))
					{
						frame.Y = num * 3;
					}
					else
					{
						frame.Y = num * 2;
						if (frameCounter >= (double)(num3 * 8))
						{
							frameCounter = num3;
						}
					}
				}
				if (ai[3] == 2f)
				{
					frame.Y += num * 6;
				}
				else if (ai[3] == 3f)
				{
					frame.Y += num * 12;
				}
			}
			if (type == 113 || type == 114)
			{
				if (ai[2] == 0f)
				{
					frameCounter += 1.0;
					if (frameCounter >= 12.0)
					{
						frame.Y += num;
						frameCounter = 0.0;
					}
					if (frame.Y >= num * Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
				else
				{
					frame.Y = 0;
					frameCounter = -60.0;
				}
			}
			if (type == 61)
			{
				spriteDirection = direction;
				rotation = velocity.X * 0.1f;
				if (velocity.X == 0f && velocity.Y == 0f)
				{
					frame.Y = 0;
					frameCounter = 0.0;
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter < 4.0)
					{
						frame.Y = num;
					}
					else
					{
						frame.Y = num * 2;
						if (frameCounter >= 7.0)
						{
							frameCounter = 0.0;
						}
					}
				}
			}
			if (type == 122)
			{
				spriteDirection = direction;
				rotation = velocity.X * 0.05f;
				if (ai[3] > 0f)
				{
					int num4 = (int)(ai[3] / 8f);
					frameCounter = 0.0;
					frame.Y = (num4 + 3) * num;
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter >= 8.0)
					{
						frame.Y += num;
						frameCounter = 0.0;
					}
					if (frame.Y >= num * 3)
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 74)
			{
				spriteDirection = direction;
				rotation = velocity.X * 0.1f;
				if (velocity.X == 0f && velocity.Y == 0f)
				{
					frame.Y = num * 4;
					frameCounter = 0.0;
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter >= 4.0)
					{
						frame.Y += num;
						frameCounter = 0.0;
					}
					if (frame.Y >= num * Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 62 || type == 66)
			{
				spriteDirection = direction;
				rotation = velocity.X * 0.1f;
				frameCounter += 1.0;
				if (frameCounter < 6.0)
				{
					frame.Y = 0;
				}
				else
				{
					frame.Y = num;
					if (frameCounter >= 11.0)
					{
						frameCounter = 0.0;
					}
				}
			}
			if (type == 63 || type == 64 || type == 103)
			{
				frameCounter += 1.0;
				if (frameCounter < 6.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 18.0)
				{
					frame.Y = num * 2;
				}
				else
				{
					frame.Y = num * 3;
					if (frameCounter >= 23.0)
					{
						frameCounter = 0.0;
					}
				}
			}
			if (type == 2 || type == 23 || type == 121)
			{
				if (type == 2)
				{
					if (velocity.X > 0f)
					{
						spriteDirection = 1;
						rotation = (float)Math.Atan2(velocity.Y, velocity.X);
					}
					if (velocity.X < 0f)
					{
						spriteDirection = -1;
						rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 3.14f;
					}
				}
				else if (type == 2 || type == 121)
				{
					if (velocity.X > 0f)
					{
						spriteDirection = 1;
					}
					if (velocity.X < 0f)
					{
						spriteDirection = -1;
					}
					rotation = velocity.X * 0.1f;
				}
				frameCounter += 1.0;
				if (frameCounter >= 8.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			if (type == 133)
			{
				if (velocity.X > 0f)
				{
					spriteDirection = 1;
					rotation = (float)Math.Atan2(velocity.Y, velocity.X);
				}
				if (velocity.X < 0f)
				{
					spriteDirection = -1;
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 3.14f;
				}
				frameCounter += 1.0;
				if (frameCounter >= 8.0)
				{
					frame.Y = num;
				}
				else
				{
					frame.Y = 0;
				}
				if (frameCounter >= 16.0)
				{
					frame.Y = 0;
					frameCounter = 0.0;
				}
				if ((double)life < (double)lifeMax * 0.5)
				{
					frame.Y += num * 2;
				}
			}
			if (type == 116)
			{
				if (velocity.X > 0f)
				{
					spriteDirection = 1;
					rotation = (float)Math.Atan2(velocity.Y, velocity.X);
				}
				if (velocity.X < 0f)
				{
					spriteDirection = -1;
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 3.14f;
				}
				frameCounter += 1.0;
				if (frameCounter >= 5.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			if (type == 75)
			{
				if (velocity.X > 0f)
				{
					spriteDirection = 1;
				}
				else
				{
					spriteDirection = -1;
				}
				rotation = velocity.X * 0.1f;
				frameCounter += 1.0;
				if (frameCounter >= 4.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			if (type == 55 || type == 57 || type == 58 || type == 102)
			{
				spriteDirection = direction;
				frameCounter += 1.0;
				if (wet)
				{
					if (frameCounter < 6.0)
					{
						frame.Y = 0;
					}
					else if (frameCounter < 12.0)
					{
						frame.Y = num;
					}
					else if (frameCounter < 18.0)
					{
						frame.Y = num * 2;
					}
					else if (frameCounter < 24.0)
					{
						frame.Y = num * 3;
					}
					else
					{
						frameCounter = 0.0;
					}
				}
				else if (frameCounter < 6.0)
				{
					frame.Y = num * 4;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num * 5;
				}
				else
				{
					frameCounter = 0.0;
				}
			}
			if (type == 69)
			{
				if (ai[0] < 190f)
				{
					frameCounter += 1.0;
					if (frameCounter >= 6.0)
					{
						frameCounter = 0.0;
						frame.Y += num;
						if (frame.Y / num >= Main.npcFrameCount[type] - 1)
						{
							frame.Y = 0;
						}
					}
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = num * (Main.npcFrameCount[type] - 1);
				}
			}
			if (type == 86)
			{
				if (velocity.Y == 0f || wet)
				{
					if (velocity.X < -2f)
					{
						spriteDirection = -1;
					}
					else if (velocity.X > 2f)
					{
						spriteDirection = 1;
					}
					else
					{
						spriteDirection = direction;
					}
				}
				if (velocity.Y != 0f)
				{
					frame.Y = num * 15;
					frameCounter = 0.0;
				}
				else if (velocity.X == 0f)
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
				else if (Math.Abs(velocity.X) < 3f)
				{
					frameCounter += Math.Abs(velocity.X);
					if (frameCounter >= 6.0)
					{
						frameCounter = 0.0;
						frame.Y += num;
						if (frame.Y / num >= 9)
						{
							frame.Y = num;
						}
						if (frame.Y / num <= 0)
						{
							frame.Y = num;
						}
					}
				}
				else
				{
					frameCounter += Math.Abs(velocity.X);
					if (frameCounter >= 10.0)
					{
						frameCounter = 0.0;
						frame.Y += num;
						if (frame.Y / num >= 15)
						{
							frame.Y = num * 9;
						}
						if (frame.Y / num <= 8)
						{
							frame.Y = num * 9;
						}
					}
				}
			}
			if (type == 127)
			{
				if (ai[1] == 0f)
				{
					frameCounter += 1.0;
					if (frameCounter >= 12.0)
					{
						frameCounter = 0.0;
						frame.Y += num;
						if (frame.Y / num >= 2)
						{
							frame.Y = 0;
						}
					}
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = num * 2;
				}
			}
			if (type == 129)
			{
				if (velocity.Y == 0f)
				{
					spriteDirection = direction;
				}
				frameCounter += 1.0;
				if (frameCounter >= 2.0)
				{
					frameCounter = 0.0;
					frame.Y += num;
					if (frame.Y / num >= Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 130)
			{
				if (velocity.Y == 0f)
				{
					spriteDirection = direction;
				}
				frameCounter += 1.0;
				if (frameCounter >= 8.0)
				{
					frameCounter = 0.0;
					frame.Y += num;
					if (frame.Y / num >= Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 67)
			{
				if (velocity.Y == 0f)
				{
					spriteDirection = direction;
				}
				frameCounter += 1.0;
				if (frameCounter >= 6.0)
				{
					frameCounter = 0.0;
					frame.Y += num;
					if (frame.Y / num >= Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 109)
			{
				if (velocity.Y == 0f && ((velocity.X <= 0f && direction < 0) || (velocity.X >= 0f && direction > 0)))
				{
					spriteDirection = direction;
				}
				frameCounter += Math.Abs(velocity.X);
				if (frameCounter >= 7.0)
				{
					frameCounter -= 7.0;
					frame.Y += num;
					if (frame.Y / num >= Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 83 || type == 84)
			{
				if (ai[0] == 2f)
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter >= 4.0)
					{
						frameCounter = 0.0;
						frame.Y += num;
						if (frame.Y / num >= Main.npcFrameCount[type])
						{
							frame.Y = 0;
						}
					}
				}
			}
			if (type == 72)
			{
				frameCounter += 1.0;
				if (frameCounter >= 3.0)
				{
					frameCounter = 0.0;
					frame.Y += num;
					if (frame.Y / num >= Main.npcFrameCount[type])
					{
						frame.Y = 0;
					}
				}
			}
			if (type == 65)
			{
				spriteDirection = direction;
				frameCounter += 1.0;
				if (wet)
				{
					if (frameCounter < 6.0)
					{
						frame.Y = 0;
					}
					else if (frameCounter < 12.0)
					{
						frame.Y = num;
					}
					else if (frameCounter < 18.0)
					{
						frame.Y = num * 2;
					}
					else if (frameCounter < 24.0)
					{
						frame.Y = num * 3;
					}
					else
					{
						frameCounter = 0.0;
					}
				}
			}
			if (type == 48 || type == 49 || type == 51 || type == 60 || type == 82 || type == 93 || type == 137)
			{
				if (velocity.X > 0f)
				{
					spriteDirection = 1;
				}
				if (velocity.X < 0f)
				{
					spriteDirection = -1;
				}
				rotation = velocity.X * 0.1f;
				frameCounter += 1.0;
				if (frameCounter >= 6.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * 4)
				{
					frame.Y = 0;
				}
			}
			if (type == 42)
			{
				frameCounter += 1.0;
				if (frameCounter < 2.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 4.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 6.0)
				{
					frame.Y = num * 2;
				}
				else if (frameCounter < 8.0)
				{
					frame.Y = num;
				}
				else
				{
					frameCounter = 0.0;
				}
			}
			if (type == 43 || type == 56)
			{
				frameCounter += 1.0;
				if (frameCounter < 6.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 18.0)
				{
					frame.Y = num * 2;
				}
				else if (frameCounter < 24.0)
				{
					frame.Y = num;
				}
				if (frameCounter == 23.0)
				{
					frameCounter = 0.0;
				}
			}
			if (type == 115)
			{
				frameCounter += 1.0;
				if (frameCounter < 3.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 6.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num * 2;
				}
				else if (frameCounter < 15.0)
				{
					frame.Y = num;
				}
				if (frameCounter == 15.0)
				{
					frameCounter = 0.0;
				}
			}
			if (type == 101)
			{
				frameCounter += 1.0;
				if (frameCounter > 6.0)
				{
					frame.Y += num * 2;
					frameCounter = 0.0;
				}
				if (frame.Y > num * 2)
				{
					frame.Y = 0;
				}
			}
			if (type == 17 || type == 18 || type == 19 || type == 20 || type == 22 || type == 142 || type == 38 || type == 26 || type == 27 || type == 28 || type == 31 || type == 21 || type == 44 || type == 54 || type == 37 || type == 73 || type == 77 || type == 78 || type == 79 || type == 80 || type == 104 || type == 107 || type == 108 || type == 120 || type == 124 || type == 140)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
					if (velocity.X == 0f)
					{
						if (type == 140)
						{
							frame.Y = num;
							frameCounter = 0.0;
						}
						else
						{
							frame.Y = 0;
							frameCounter = 0.0;
						}
					}
					else
					{
						frameCounter += Math.Abs(velocity.X) * 2f;
						frameCounter += 1.0;
						if (frameCounter > 6.0)
						{
							frame.Y += num;
							frameCounter = 0.0;
						}
						if (frame.Y / num >= Main.npcFrameCount[type])
						{
							frame.Y = num * 2;
						}
					}
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = num;
					if (type == 21 || type == 31 || type == 44 || type == 77 || type == 78 || type == 79 || type == 80 || type == 120 || type == 140)
					{
						frame.Y = 0;
					}
				}
			}
			else if (type == 110)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
					if (ai[2] > 0f)
					{
						spriteDirection = direction;
						frame.Y = num * (int)ai[2];
						frameCounter = 0.0;
					}
					else
					{
						if (frame.Y < num * 6)
						{
							frame.Y = num * 6;
						}
						frameCounter += Math.Abs(velocity.X) * 2f;
						frameCounter += velocity.X;
						if (frameCounter > 6.0)
						{
							frame.Y += num;
							frameCounter = 0.0;
						}
						if (frame.Y / num >= Main.npcFrameCount[type])
						{
							frame.Y = num * 6;
						}
					}
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
			}
			if (type == 111)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
					if (ai[2] > 0f)
					{
						spriteDirection = direction;
						frame.Y = num * ((int)ai[2] - 1);
						frameCounter = 0.0;
					}
					else
					{
						if (frame.Y < num * 7)
						{
							frame.Y = num * 7;
						}
						frameCounter += Math.Abs(velocity.X) * 2f;
						frameCounter += velocity.X * 1.3f;
						if (frameCounter > 6.0)
						{
							frame.Y += num;
							frameCounter = 0.0;
						}
						if (frame.Y / num >= Main.npcFrameCount[type])
						{
							frame.Y = num * 7;
						}
					}
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = num * 6;
				}
			}
			else if (type == 3 || type == 52 || type == 53 || type == 132)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
				}
				if (velocity.Y != 0f || (direction == -1 && velocity.X > 0f) || (direction == 1 && velocity.X < 0f))
				{
					frameCounter = 0.0;
					frame.Y = num * 2;
				}
				else if (velocity.X == 0f)
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
				else
				{
					frameCounter += Math.Abs(velocity.X);
					if (frameCounter < 8.0)
					{
						frame.Y = 0;
					}
					else if (frameCounter < 16.0)
					{
						frame.Y = num;
					}
					else if (frameCounter < 24.0)
					{
						frame.Y = num * 2;
					}
					else if (frameCounter < 32.0)
					{
						frame.Y = num;
					}
					else
					{
						frameCounter = 0.0;
					}
				}
			}
			else if (type == 46 || type == 47)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
					if (velocity.X == 0f)
					{
						frame.Y = 0;
						frameCounter = 0.0;
					}
					else
					{
						frameCounter += Math.Abs(velocity.X) * 1f;
						frameCounter += 1.0;
						if (frameCounter > 6.0)
						{
							frame.Y += num;
							frameCounter = 0.0;
						}
						if (frame.Y / num >= Main.npcFrameCount[type])
						{
							frame.Y = 0;
						}
					}
				}
				else if (velocity.Y < 0f)
				{
					frameCounter = 0.0;
					frame.Y = num * 4;
				}
				else if (velocity.Y > 0f)
				{
					frameCounter = 0.0;
					frame.Y = num * 6;
				}
			}
			else if (type == 4 || type == 125 || type == 126)
			{
				frameCounter += 1.0;
				if (frameCounter < 7.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 14.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 21.0)
				{
					frame.Y = num * 2;
				}
				else
				{
					frameCounter = 0.0;
					frame.Y = 0;
				}
				if (ai[0] > 1f)
				{
					frame.Y += num * 3;
				}
			}
			else if (type == 5)
			{
				frameCounter += 1.0;
				if (frameCounter >= 8.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			else if (type == 94)
			{
				frameCounter += 1.0;
				if (frameCounter < 6.0)
				{
					frame.Y = 0;
				}
				else if (frameCounter < 12.0)
				{
					frame.Y = num;
				}
				else if (frameCounter < 18.0)
				{
					frame.Y = num * 2;
				}
				else
				{
					frame.Y = num;
					if (frameCounter >= 23.0)
					{
						frameCounter = 0.0;
					}
				}
			}
			else if (type == 6)
			{
				frameCounter += 1.0;
				if (frameCounter >= 8.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
			else if (type == 24)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
				}
				if (ai[1] > 0f)
				{
					if (frame.Y < 4)
					{
						frameCounter = 0.0;
					}
					frameCounter += 1.0;
					if (frameCounter <= 4.0)
					{
						frame.Y = num * 4;
					}
					else if (frameCounter <= 8.0)
					{
						frame.Y = num * 5;
					}
					else if (frameCounter <= 12.0)
					{
						frame.Y = num * 6;
					}
					else if (frameCounter <= 16.0)
					{
						frame.Y = num * 7;
					}
					else if (frameCounter <= 20.0)
					{
						frame.Y = num * 8;
					}
					else
					{
						frame.Y = num * 9;
						frameCounter = 100.0;
					}
				}
				else
				{
					frameCounter += 1.0;
					if (frameCounter <= 4.0)
					{
						frame.Y = 0;
					}
					else if (frameCounter <= 8.0)
					{
						frame.Y = num;
					}
					else if (frameCounter <= 12.0)
					{
						frame.Y = num * 2;
					}
					else
					{
						frame.Y = num * 3;
						if (frameCounter >= 16.0)
						{
							frameCounter = 0.0;
						}
					}
				}
			}
			else if (type == 29 || type == 32 || type == 45)
			{
				if (velocity.Y == 0f)
				{
					if (direction == 1)
					{
						spriteDirection = 1;
					}
					if (direction == -1)
					{
						spriteDirection = -1;
					}
				}
				frame.Y = 0;
				if (velocity.Y != 0f)
				{
					frame.Y += num;
				}
				else if (ai[1] > 0f)
				{
					frame.Y += num * 2;
				}
			}
			if (type == 34)
			{
				frameCounter += 1.0;
				if (frameCounter >= 4.0)
				{
					frame.Y += num;
					frameCounter = 0.0;
				}
				if (frame.Y >= num * Main.npcFrameCount[type])
				{
					frame.Y = 0;
				}
			}
		}

		public void TargetClosest(bool faceTarget = true)
		{
			float num = -1f;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active && !Main.player[i].dead && (num == -1f || Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - position.X + (float)(width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - position.Y + (float)(height / 2)) < num))
				{
					num = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - position.X + (float)(width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - position.Y + (float)(height / 2));
					target = i;
				}
			}
			if (target < 0 || target >= 255)
			{
				target = 0;
			}
			targetRect = new Rectangle((int)Main.player[target].position.X, (int)Main.player[target].position.Y, Main.player[target].width, Main.player[target].height);
			if (Main.player[target].dead)
			{
				faceTarget = false;
			}
			if (faceTarget)
			{
				direction = 1;
				if ((float)(targetRect.X + targetRect.Width / 2) < position.X + (float)(width / 2))
				{
					direction = -1;
				}
				base.directionY = 1;
				if ((float)(targetRect.Y + targetRect.Height / 2) < position.Y + (float)(height / 2))
				{
					base.directionY = -1;
				}
			}
			if (confused)
			{
				direction *= -1;
			}
			if ((direction != oldDirection || base.directionY != oldDirectionY || target != oldTarget) && !collideX && !collideY)
			{
				netUpdate = true;
			}
		}

		public void CheckActive()
		{
			if (!active || type == 8 || type == 9 || type == 11 || type == 12 || type == 14 || type == 15 || type == 40 || type == 41 || type == 96 || type == 97 || type == 99 || type == 100 || (type > 87 && type <= 92) || type == 118 || type == 119 || type == 113 || type == 114 || type == 115 || (type >= 134 && type <= 136))
			{
				return;
			}
			if (townNPC)
			{
				Rectangle rectangle = new Rectangle((int)(position.X + (float)(width / 2) - (float)townRangeX), (int)(position.Y + (float)(height / 2) - (float)townRangeY), townRangeX * 2, townRangeY * 2);
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && rectangle.Intersects(new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
					{
						Main.player[i].townNPCs += npcSlots;
					}
				}
				return;
			}
			bool flag = false;
			Rectangle rectangle2 = new Rectangle((int)(position.X + (float)(width / 2) - (float)activeRangeX), (int)(position.Y + (float)(height / 2) - (float)activeRangeY), activeRangeX * 2, activeRangeY * 2);
			Rectangle rectangle3 = new Rectangle((int)((double)(position.X + (float)(width / 2)) - (double)sWidth * 0.5 - (double)width), (int)((double)(position.Y + (float)(height / 2)) - (double)sHeight * 0.5 - (double)height), sWidth + width * 2, sHeight + height * 2);
			for (int j = 0; j < 255; j++)
			{
				if (!Main.player[j].active)
				{
					continue;
				}
				if (rectangle2.Intersects(new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
				{
					flag = true;
					if (type != 25 && type != 30 && type != 33 && lifeMax > 0)
					{
						Main.player[j].activeNPCs += npcSlots;
					}
				}
				if (rectangle3.Intersects(new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
				{
					timeLeft = activeTime;
				}
				if (type == 7 || type == 10 || type == 13 || type == 39 || type == 87)
				{
					flag = true;
				}
				if (boss || type == 35 || type == 36 || type == 127 || type == 128 || type == 129 || type == 130 || type == 131)
				{
					flag = true;
				}
			}
			timeLeft--;
			if (timeLeft <= 0)
			{
				flag = false;
			}
			if (flag || Main.netMode == 1)
			{
				return;
			}
			noSpawnCycle = true;
			active = false;
			if (Main.netMode == 2)
			{
				netSkip = -1;
				life = 0;
				NetMessage.SendData(23, -1, -1, "", whoAmI);
			}
			if (aiStyle != 6)
			{
				return;
			}
			for (int num = (int)ai[0]; num > 0; num = (int)Main.npc[num].ai[0])
			{
				if (Main.npc[num].active)
				{
					Main.npc[num].active = false;
					if (Main.netMode == 2)
					{
						Main.npc[num].life = 0;
						Main.npc[num].netSkip = -1;
						NetMessage.SendData(23, -1, -1, "", num);
					}
				}
			}
		}

		public static void SpawnNPC()
		{
			if (noSpawnCycle)
			{
				noSpawnCycle = false;
				return;
			}
			SpawnSky = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num3++;
				}
			}
			int num4 = 0;
			int d;
			List<int> list;
			while (true)
			{
				if (num4 >= 255)
				{
					return;
				}
				Player player = Main.player[num4];
				if (player.active && !player.dead)
				{
					SpawnInvasion = false;
					SpawnWater = false;
					SpawnNearTown = false;
					SpawnTownReduction = false;
					SpawnCheckInvasion(player);
					bool flag = false;
					SetSpawnRates(player);
					SetTownSpawnInfo(player);
					Codable.RunGlobalMethod("ModGeneric", "UpdateSpawn", player);
					if (player.activeNPCs < (float)maxSpawns && Main.rand.Next(spawnRate) == 0)
					{
						int num5 = (int)(player.position.X / 16f) - spawnRangeX;
						int num6 = (int)(player.position.X / 16f) + spawnRangeX;
						int num7 = (int)(player.position.Y / 16f) - spawnRangeY;
						int num8 = (int)(player.position.Y / 16f) + spawnRangeY;
						int num9 = (int)(player.position.X / 16f) - safeRangeX;
						int num10 = (int)(player.position.X / 16f) + safeRangeX;
						int num11 = (int)(player.position.Y / 16f) - safeRangeY;
						int num12 = (int)(player.position.Y / 16f) + safeRangeY;
						if (num5 < 0)
						{
							num5 = 0;
						}
						if (num6 > Main.maxTilesX)
						{
							num6 = Main.maxTilesX;
						}
						if (num7 < 0)
						{
							num7 = 0;
						}
						if (num8 > Main.maxTilesY)
						{
							num8 = Main.maxTilesY;
						}
						int num13 = 50;
						while (num13 > 0)
						{
							int num14 = Main.rand.Next(num5, num6);
							int num15 = Main.rand.Next(num7, num8);
							if (Main.tile[num14, num15].active && Main.tileSolid[Main.tile[num14, num15].type])
							{
								if (flag)
								{
									break;
								}
								num13--;
							}
							else if (!Main.wallHouse[Main.tile[num14, num15].wall])
							{
								if (!SpawnInvasion && (double)num15 < Main.worldSurface * 0.34999999403953552 && !SpawnTownReduction && ((float)num14 < (float)Main.maxTilesX * 0.45f || (float)num14 > (float)Main.maxTilesX * 0.55f || Main.hardMode))
								{
									num = num14;
									num2 = num15;
									flag = true;
									SpawnSky = true;
								}
								else if (!SpawnInvasion && (double)num15 < Main.worldSurface * 0.44999998807907104 && !SpawnTownReduction && Main.hardMode && Main.rand.Next(10) == 0)
								{
									num = num14;
									num2 = num15;
									flag = true;
									SpawnSky = true;
								}
								else
								{
									for (int j = num15; j < Main.maxTilesY; j++)
									{
										if (Main.tile[num14, j].active && Main.tileSolid[Main.tile[num14, j].type])
										{
											if (num14 < num9 || num14 > num10 || j < num11 || j > num12)
											{
												num = num14;
												num2 = j;
												flag = true;
											}
											break;
										}
									}
								}
								if (!flag)
								{
									num13--;
									continue;
								}
								int num16 = num - spawnSpaceX / 2;
								int num17 = num + spawnSpaceX / 2;
								int num18 = num2 - spawnSpaceY;
								int num19 = num2;
								if (num16 < 0)
								{
									flag = false;
								}
								if (num17 > Main.maxTilesX)
								{
									flag = false;
								}
								if (num18 < 0)
								{
									flag = false;
								}
								if (num19 > Main.maxTilesY)
								{
									flag = false;
								}
								if (flag)
								{
									for (int k = num16; k < num17; k++)
									{
										for (int l = num18; l < num19; l++)
										{
											if (Main.tile[k, l].active && Main.tileSolid[Main.tile[k, l].type])
											{
												flag = false;
												break;
											}
											if (Main.tile[k, l].lava)
											{
												flag = false;
												break;
											}
										}
									}
								}
								if (flag)
								{
									break;
								}
								num13--;
							}
							else
							{
								num13--;
							}
						}
					}
					if (flag)
					{
						Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
						for (int m = 0; m < 255; m++)
						{
							Player player2 = Main.player[m];
							Rectangle rectangle2 = new Rectangle((int)(player2.position.X + (float)player2.width / 2f - (float)sWidth / 2f - (float)safeRangeX), (int)(player2.position.Y + (float)player2.height / 2f - (float)sHeight / 2f - (float)safeRangeY), sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
							if (rectangle.Intersects(rectangle2))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						if (player.zoneDungeon && (!Main.tileDungeon[Main.tile[num, num2].type] || Main.tile[num, num2 - 1].wall == 0))
						{
							flag = false;
						}
						if (Main.tile[num, num2 - 1].liquid > 0 && Main.tile[num, num2 - 2].liquid > 0 && !Main.tile[num, num2 - 1].lava)
						{
							SpawnWater = true;
						}
					}
					if (flag)
					{
						flag = false;
						_ = Main.tile[num, num2].type;
						d = 200;
						list = new List<int>();
						for (int n = 1; n < Config.npcDefs.GetSize(); n++)
						{
							if (Config.npcDefs.assemblyByType[n] != null)
							{
								Assembly assembly = Config.npcDefs.assemblyByType[n];
								object code = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Config.npcDefs[n].name) + "NPC");
								if (Codable.RunSpecifiedMethod("NPC " + Config.npcDefs[n].name, code, "SpawnNPC", num, num2, num4) && (bool)Codable.customMethodReturn)
								{
									list.Add(n);
								}
							}
						}
						AddSpawnVanilla(list, "", num, num2, num4);
						Codable.RunGlobalMethod("ModGeneric", "ModifySpawnList", list);
						if (list.Count != 0)
						{
							break;
						}
					}
				}
				num4++;
			}
			int index = 0;
			if (list.Count > 1)
			{
				index = Main.rand.Next(list.Count);
			}
			if (list[index] < 0)
			{
				SpawnVanillaSpecial(ref d, -list[index], num, num2, num4);
			}
			else
			{
				d = NewNPC(num * 16 + 8, num2 * 16, list[index]);
			}
			if (Main.npc[d].type == 1 && Main.rand.Next(250) == 0)
			{
				Main.npc[d].SetDefaults("Pinky");
			}
			if (Main.netMode == 2 && d < 200)
			{
				NetMessage.SendData(23, -1, -1, "", d);
			}
		}

		public static void SpawnVanillaSpecial(ref int d, int i, int x, int y, int pid)
		{
			if (i == 1)
			{
				d = NewNPC(x * 16 + 8, y * 16, 31);
				if (Main.rand.Next(4) == 0)
				{
					Main.npc[d].SetDefaults("Big Boned");
				}
				else if (Main.rand.Next(5) == 0)
				{
					Main.npc[d].SetDefaults("Short Bones");
				}
			}
			if (i == 2)
			{
				d = NewNPC(x * 16 + 8, y * 16, 43);
				Main.npc[d].ai[0] = x;
				Main.npc[d].ai[1] = y;
				Main.npc[d].netUpdate = true;
			}
			if (i == 3)
			{
				d = NewNPC(x * 16 + 8, y * 16, 42);
				if (Main.rand.Next(4) == 0)
				{
					Main.npc[d].SetDefaults("Little Stinger");
				}
				else if (Main.rand.Next(4) == 0)
				{
					Main.npc[d].SetDefaults("Big Stinger");
				}
			}
			if (i == 4)
			{
				d = NewNPC(x * 16 + 8, y * 16, 56);
				Main.npc[d].ai[0] = x;
				Main.npc[d].ai[1] = y;
				Main.npc[d].netUpdate = true;
			}
			if (i == 5)
			{
				d = NewNPC(x * 16 + 8, y * 16, 101);
				Main.npc[d].ai[0] = x;
				Main.npc[d].ai[1] = y;
				Main.npc[d].netUpdate = true;
			}
			if (i == 6)
			{
				d = NewNPC(x * 16 + 8, y * 16, 6);
				if (Main.rand.Next(3) == 0)
				{
					Main.npc[d].SetDefaults("Little Eater");
				}
				else if (Main.rand.Next(3) == 0)
				{
					Main.npc[d].SetDefaults("Big Eater");
				}
			}
			if (i == 7)
			{
				int num = Math.Abs(x - Main.spawnTileX);
				d = NewNPC(x * 16 + 8, y * 16, 1);
				if (Main.tile[x, y].type == 60)
				{
					Main.npc[d].SetDefaults("Jungle Slime");
				}
				else if (Main.rand.Next(3) == 0 || num < 200)
				{
					Main.npc[d].SetDefaults("Green Slime");
				}
				else if (Main.rand.Next(10) == 0 && num > 400)
				{
					Main.npc[d].SetDefaults("Purple Slime");
				}
			}
			if (i == 8)
			{
				d = NewNPC(x * 16 + 8, y * 16, 1);
				if (Main.rand.Next(5) == 0)
				{
					Main.npc[d].SetDefaults("Yellow Slime");
				}
				else if (Main.rand.Next(2) == 0)
				{
					Main.npc[d].SetDefaults("Blue Slime");
				}
				else
				{
					Main.npc[d].SetDefaults("Red Slime");
				}
			}
			if (i == 9)
			{
				d = NewNPC(x * 16 + 8, y * 16, 1);
				if (Main.player[pid].zoneJungle)
				{
					Main.npc[d].SetDefaults("Jungle Slime");
				}
				else
				{
					Main.npc[d].SetDefaults("Black Slime");
				}
			}
			if (i == 10)
			{
				d = NewNPC(x * 16 + 8, y * 16, 77);
				if ((double)y > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(5) == 0)
				{
					Main.npc[d].SetDefaults("Heavy Skeleton");
				}
			}
		}

		public static void AddSpawnVanilla(List<int> suc, string biome, int x, int y, int pid)
		{
			Player player = Main.player[pid];
			int num = Main.tile[x, y].type;
			if (SpawnSky)
			{
				if (Main.hardMode && Main.rand.Next(10) == 0 && !AnyNPCs(87))
				{
					suc.Add(87);
				}
				else
				{
					suc.Add(48);
				}
			}
			else if (SpawnInvasion)
			{
				if (Main.invasionType == 1)
				{
					if (Main.rand.Next(9) == 0)
					{
						suc.Add(29);
					}
					else if (Main.rand.Next(5) == 0)
					{
						suc.Add(26);
					}
					else if (Main.rand.Next(3) == 0)
					{
						suc.Add(111);
					}
					else if (Main.rand.Next(3) == 0)
					{
						suc.Add(27);
					}
					else
					{
						suc.Add(28);
					}
				}
				else if (Main.invasionType == 2)
				{
					if (Main.rand.Next(7) == 0)
					{
						suc.Add(145);
					}
					else if (Main.rand.Next(3) == 0)
					{
						suc.Add(143);
					}
					else
					{
						suc.Add(144);
					}
				}
			}
			else if (Biome.Biomes["Ocean"].TileValid(x, y, pid))
			{
				if (Main.rand.Next(8) == 0)
				{
					suc.Add(65);
				}
				if (Main.rand.Next(3) == 0)
				{
					suc.Add(67);
				}
				else
				{
					suc.Add(64);
				}
			}
			else if (SpawnWater && (((double)y > Main.rockLayer && Main.rand.Next(2) == 0) || num == 60))
			{
				if (Main.hardMode && Main.rand.Next(3) != 0)
				{
					suc.Add(102);
				}
				else
				{
					suc.Add(58);
				}
			}
			else if (SpawnWater && (double)y > Main.worldSurface && Main.rand.Next(3) == 0)
			{
				if (Main.hardMode)
				{
					suc.Add(103);
				}
				else
				{
					suc.Add(63);
				}
			}
			else if (SpawnWater && Main.rand.Next(4) == 0)
			{
				if (player.zoneEvil)
				{
					suc.Add(57);
				}
				else
				{
					suc.Add(55);
				}
			}
			else if (downedGoblins && Main.rand.Next(20) == 0 && !SpawnWater && (double)y >= Main.rockLayer && (double)y < Main.hellLayer - 10.0 && !savedGoblin && !AnyNPCs(105))
			{
				suc.Add(105);
			}
			else if (Main.hardMode && Main.rand.Next(20) == 0 && !SpawnWater && (double)y >= Main.rockLayer && (double)y < Main.hellLayer - 10.0 && !savedWizard && !AnyNPCs(106))
			{
				suc.Add(106);
			}
			else if (SpawnTownReduction)
			{
				if (SpawnWater)
				{
					suc.Add(55);
				}
				else if (num == 2 || num == 109 || num == 147 || !((double)y < Main.worldSurface))
				{
					if (Main.rand.Next(2) == 0 && (double)y <= Main.worldSurface)
					{
						suc.Add(74);
					}
					else
					{
						suc.Add(46);
					}
				}
			}
			else if (player.zoneDungeon)
			{
				if (!downedBoss3)
				{
					NewNPC(x * 16 + 8, y * 16, 68);
				}
				else if (!savedMech && Main.rand.Next(5) == 0 && !SpawnWater && !AnyNPCs(123) && (double)y > Main.rockLayer)
				{
					suc.Add(123);
				}
				else if (Main.rand.Next(37) == 0)
				{
					suc.Add(71);
				}
				else if (Main.rand.Next(4) == 0 && !NearSpikeBall(x, y))
				{
					suc.Add(70);
				}
				else if (Main.rand.Next(15) == 0)
				{
					suc.Add(72);
				}
				else if (Main.rand.Next(9) == 0)
				{
					suc.Add(34);
				}
				else if (Main.rand.Next(7) == 0)
				{
					suc.Add(32);
				}
				else
				{
					suc.Add(-1);
				}
			}
			else if (player.zoneMeteor)
			{
				suc.Add(23);
			}
			else if (player.zoneEvil && Main.rand.Next(65) == 0)
			{
				if (Main.hardMode && Main.rand.Next(4) != 0)
				{
					suc.Add(98);
				}
				else
				{
					suc.Add(7);
				}
			}
			else if (Main.hardMode && (double)y > Main.worldSurface && Main.rand.Next(75) == 0)
			{
				suc.Add(85);
			}
			else if (Main.hardMode && Main.tile[x, y - 1].wall == 2 && Main.rand.Next(20) == 0)
			{
				suc.Add(85);
			}
			else if (Main.hardMode && (double)y <= Main.worldSurface && !Main.dayTime && (Main.rand.Next(20) == 0 || (Main.rand.Next(5) == 0 && Main.moonPhase == 4)))
			{
				suc.Add(82);
			}
			else if (num == 60 && Main.rand.Next(500) == 0 && !Main.dayTime)
			{
				suc.Add(52);
			}
			else if (num == 60 && (double)y > (Main.worldSurface + Main.rockLayer) / 2.0)
			{
				if (Main.rand.Next(3) == 0)
				{
					suc.Add(-2);
				}
				else
				{
					suc.Add(-3);
				}
			}
			else if (num == 60 && Main.rand.Next(4) == 0)
			{
				suc.Add(51);
			}
			else if (num == 60 && Main.rand.Next(8) == 0)
			{
				suc.Add(-4);
			}
			else if (Main.hardMode && num == 53 && Main.rand.Next(3) == 0)
			{
				suc.Add(78);
			}
			else if (Main.hardMode && num == 112 && Main.rand.Next(2) == 0)
			{
				suc.Add(79);
			}
			else if (Main.hardMode && num == 116 && Main.rand.Next(2) == 0)
			{
				suc.Add(80);
			}
			else if (Main.hardMode && !SpawnWater && (double)y < Main.rockLayer && (num == 116 || num == 117 || num == 109))
			{
				if (!Main.dayTime && Main.rand.Next(2) == 0)
				{
					suc.Add(122);
				}
				else if (Main.rand.Next(10) == 0)
				{
					suc.Add(86);
				}
				else
				{
					suc.Add(75);
				}
			}
			else if (!SpawnNearTown && Main.hardMode && Main.rand.Next(50) == 0 && SpawnWater && (double)y >= Main.rockLayer && (num == 116 || num == 117 || num == 109))
			{
				suc.Add(84);
			}
			else if ((num == 22 && player.zoneEvil) || num == 23 || num == 25 || num == 112)
			{
				if (Main.hardMode && (double)y >= Main.rockLayer && Main.rand.Next(3) == 0)
				{
					suc.Add(-5);
				}
				else if (Main.hardMode && Main.rand.Next(3) == 0)
				{
					if (Main.rand.Next(3) == 0)
					{
						suc.Add(121);
					}
					else
					{
						suc.Add(81);
					}
				}
				else if (Main.hardMode && (double)y >= Main.rockLayer && Main.rand.Next(40) == 0)
				{
					suc.Add(83);
				}
				else if (Main.hardMode && (Main.rand.Next(2) == 0 || (double)y > Main.rockLayer))
				{
					suc.Add(94);
				}
				else
				{
					suc.Add(-6);
				}
			}
			else if ((double)y <= Main.worldSurface)
			{
				if (Main.dayTime)
				{
					int num2 = Math.Abs(x - Main.spawnTileX);
					if (num2 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num == 2 || num == 109 || num == 147))
					{
						suc.Add(46);
					}
					else if (num2 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num == 2 || num == 109 || num == 147))
					{
						suc.Add(74);
					}
					else if (num2 > Main.maxTilesX / 3 && num == 2 && Main.rand.Next(300) == 0 && !AnyNPCs(50))
					{
						suc.Add(50);
					}
					else if (num == 53 && Main.rand.Next(5) == 0 && !SpawnWater)
					{
						suc.Add(69);
					}
					else if (num == 53 && !SpawnWater)
					{
						suc.Add(61);
					}
					else if (num2 > Main.maxTilesX / 3 && Main.rand.Next(15) == 0)
					{
						suc.Add(73);
					}
					else
					{
						suc.Add(-7);
					}
				}
				else if (Main.rand.Next(6) == 0 || (Main.moonPhase == 4 && Main.rand.Next(2) == 0))
				{
					if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						suc.Add(133);
					}
					else
					{
						suc.Add(2);
					}
				}
				else if (Main.hardMode && Main.rand.Next(50) == 0 && Main.bloodMoon && !AnyNPCs(109))
				{
					suc.Add(109);
				}
				else if (Main.rand.Next(250) == 0 && Main.bloodMoon)
				{
					suc.Add(53);
				}
				else if (Main.moonPhase == 0 && Main.hardMode && Main.rand.Next(3) != 0)
				{
					suc.Add(104);
				}
				else if (Main.hardMode && Main.rand.Next(3) == 0)
				{
					suc.Add(140);
				}
				else if (Main.rand.Next(3) == 0)
				{
					suc.Add(132);
				}
				else
				{
					suc.Add(3);
				}
			}
			else if ((double)y <= Main.rockLayer)
			{
				if (!SpawnNearTown && Main.rand.Next(50) == 0)
				{
					if (Main.hardMode)
					{
						suc.Add(95);
					}
					else
					{
						suc.Add(10);
					}
				}
				else if (Main.hardMode && Main.rand.Next(3) == 0)
				{
					suc.Add(140);
				}
				else if (Main.hardMode && Main.rand.Next(4) != 0)
				{
					suc.Add(141);
				}
				else
				{
					suc.Add(-8);
				}
			}
			else if ((double)y > Main.hellLayer + 10.0)
			{
				if (Main.rand.Next(40) == 0 && !AnyNPCs(39))
				{
					suc.Add(39);
				}
				else if (Main.rand.Next(14) == 0)
				{
					suc.Add(24);
				}
				else if (Main.rand.Next(8) == 0)
				{
					if (Main.rand.Next(7) == 0)
					{
						suc.Add(66);
					}
					else
					{
						suc.Add(62);
					}
				}
				else if (Main.rand.Next(3) == 0)
				{
					suc.Add(59);
				}
				else
				{
					suc.Add(60);
				}
			}
			else if ((num == 116 || num == 117) && !SpawnNearTown && Main.rand.Next(8) == 0)
			{
				suc.Add(120);
			}
			else if (!SpawnNearTown && Main.rand.Next(75) == 0 && !player.zoneHoly)
			{
				if (Main.hardMode)
				{
					suc.Add(95);
				}
				else
				{
					suc.Add(10);
				}
			}
			else if (!Main.hardMode && Main.rand.Next(10) == 0)
			{
				suc.Add(16);
			}
			else if (!Main.hardMode && Main.rand.Next(4) == 0)
			{
				suc.Add(-9);
			}
			else if (Main.rand.Next(2) == 0)
			{
				if ((double)y > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(700) == 0)
				{
					suc.Add(45);
				}
				else if (Main.hardMode && Main.rand.Next(10) != 0)
				{
					if (Main.rand.Next(2) == 0)
					{
						suc.Add(-10);
					}
					else
					{
						suc.Add(110);
					}
				}
				else if (Main.rand.Next(15) == 0)
				{
					suc.Add(44);
				}
				else
				{
					suc.Add(21);
				}
			}
			else if (Main.hardMode && player.zoneHoly && Main.rand.Next(2) == 0)
			{
				suc.Add(138);
			}
			else if (player.zoneJungle)
			{
				suc.Add(51);
			}
			else if (Main.hardMode && player.zoneHoly)
			{
				suc.Add(137);
			}
			else if (Main.hardMode && Main.rand.Next(6) != 0)
			{
				suc.Add(93);
			}
			else
			{
				suc.Add(49);
			}
		}

		public static void SetTownSpawnInfo(Player P)
		{
			if (SpawnInvasion || (Main.bloodMoon && !Main.dayTime) || P.zoneDungeon || P.zoneEvil || P.zoneMeteor)
			{
				return;
			}
			if (P.townNPCs == 1f)
			{
				SpawnNearTown = true;
				if (Main.rand.Next(3) <= 1)
				{
					SpawnTownReduction = true;
					maxSpawns = (int)((double)(float)maxSpawns * 0.6);
				}
				else
				{
					spawnRate = (int)((float)spawnRate * 2f);
				}
			}
			else if (P.townNPCs == 2f)
			{
				SpawnNearTown = true;
				if (Main.rand.Next(3) == 0)
				{
					SpawnTownReduction = true;
					maxSpawns = (int)((double)(float)maxSpawns * 0.6);
				}
				else
				{
					spawnRate = (int)((float)spawnRate * 3f);
				}
			}
			else if (P.townNPCs >= 3f)
			{
				SpawnNearTown = true;
				SpawnTownReduction = true;
				maxSpawns = (int)((double)(float)maxSpawns * 0.6);
			}
		}

		public static void SetSpawnRates(Player P)
		{
			spawnRate = defaultSpawnRate;
			maxSpawns = defaultMaxSpawns;
			if (Main.hardMode)
			{
				spawnRate = (int)((float)defaultSpawnRate * 0.9f);
				maxSpawns = defaultMaxSpawns + 1;
			}
			if ((double)P.position.Y > Main.hellLayer * 16.0)
			{
				maxSpawns = (int)((float)maxSpawns * 2f);
			}
			else if ((double)P.position.Y > Main.rockLayer * 16.0 + (double)sHeight)
			{
				spawnRate = (int)((double)spawnRate * 0.4);
				maxSpawns = (int)((float)maxSpawns * 1.9f);
			}
			else if ((double)P.position.Y > Main.worldSurface * 16.0 + (double)sHeight)
			{
				if (Main.hardMode)
				{
					spawnRate = (int)((double)spawnRate * 0.45);
					maxSpawns = (int)((float)maxSpawns * 1.8f);
				}
				else
				{
					spawnRate = (int)((double)spawnRate * 0.5);
					maxSpawns = (int)((float)maxSpawns * 1.7f);
				}
			}
			else if (!Main.dayTime)
			{
				spawnRate = (int)((double)spawnRate * 0.6);
				maxSpawns = (int)((float)maxSpawns * 1.3f);
				if (Main.bloodMoon)
				{
					spawnRate = (int)((double)spawnRate * 0.3);
					maxSpawns = (int)((float)maxSpawns * 1.8f);
				}
			}
			if (P.zoneDungeon)
			{
				spawnRate = (int)((double)spawnRate * 0.4);
				maxSpawns = (int)((float)maxSpawns * 1.7f);
			}
			else if (P.zoneJungle)
			{
				spawnRate = (int)((double)spawnRate * 0.4);
				maxSpawns = (int)((float)maxSpawns * 1.5f);
			}
			else if (P.zoneEvil)
			{
				spawnRate = (int)((double)spawnRate * 0.65);
				maxSpawns = (int)((float)maxSpawns * 1.3f);
			}
			else if (P.zoneMeteor)
			{
				spawnRate = (int)((double)spawnRate * 0.4);
				maxSpawns = (int)((float)maxSpawns * 1.1f);
			}
			if (P.zoneHoly && (double)P.position.Y > Main.rockLayer * 16.0 + (double)sHeight)
			{
				spawnRate = (int)((double)spawnRate * 0.65);
				maxSpawns = (int)((float)maxSpawns * 1.3f);
			}
			if (Main.wof >= 0 && (double)P.position.Y > Main.hellLayer * 16.0)
			{
				maxSpawns = (int)((float)maxSpawns * 0.3f);
				spawnRate *= 3;
			}
			if ((double)P.activeNPCs < (double)maxSpawns * 0.2)
			{
				spawnRate = (int)((float)spawnRate * 0.6f);
			}
			else if ((double)P.activeNPCs < (double)maxSpawns * 0.4)
			{
				spawnRate = (int)((float)spawnRate * 0.7f);
			}
			else if ((double)P.activeNPCs < (double)maxSpawns * 0.6)
			{
				spawnRate = (int)((float)spawnRate * 0.8f);
			}
			else if ((double)P.activeNPCs < (double)maxSpawns * 0.8)
			{
				spawnRate = (int)((float)spawnRate * 0.9f);
			}
			if ((double)(P.position.Y * 16f) > (Main.worldSurface + Main.rockLayer) / 2.0 || P.zoneEvil)
			{
				if ((double)P.activeNPCs < (double)maxSpawns * 0.2)
				{
					spawnRate = (int)((float)spawnRate * 0.7f);
				}
				else if ((double)P.activeNPCs < (double)maxSpawns * 0.4)
				{
					spawnRate = (int)((float)spawnRate * 0.9f);
				}
			}
			if (P.inventory[P.selectedItem].type == 148)
			{
				spawnRate = (int)((double)spawnRate * 0.75);
				maxSpawns = (int)((float)maxSpawns * 1.5f);
			}
			if (P.enemySpawns)
			{
				spawnRate = (int)((double)spawnRate * 0.5);
				maxSpawns = (int)((float)maxSpawns * 2f);
			}
			if ((double)spawnRate < (double)defaultSpawnRate * 0.1)
			{
				spawnRate = (int)((double)defaultSpawnRate * 0.1);
			}
			if (maxSpawns > defaultMaxSpawns * 3)
			{
				maxSpawns = defaultMaxSpawns * 3;
			}
			int num = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num++;
				}
			}
			if (SpawnInvasion)
			{
				maxSpawns = (int)((double)defaultMaxSpawns * (2.0 + 0.3 * (double)num));
				spawnRate = 20;
			}
			if (P.zoneDungeon && !downedBoss3)
			{
				spawnRate = 10;
			}
		}

		public static void SpawnCheckInvasion(Player P)
		{
			if (Main.invasionType >= 1 && Main.invasionDelay == 0 && Main.invasionSize >= 1 && !((double)P.position.Y >= Main.worldSurface * 16.0 + (double)sHeight) && (double)P.position.X > Main.invasionX * 16.0 - 3200.0 && (double)P.position.X < Main.invasionX * 16.0 + 3200.0)
			{
				SpawnInvasion = true;
			}
		}

		public static void SpawnNPC2()
		{
			if (noSpawnCycle)
			{
				noSpawnCycle = false;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num3++;
				}
			}
			int num4 = 0;
			bool flag3;
			bool flag4;
			bool flag5;
			bool flag6;
			while (true)
			{
				if (num4 >= 255)
				{
					return;
				}
				if (Main.player[num4].active && !Main.player[num4].dead)
				{
					flag3 = false;
					flag4 = false;
					flag5 = false;
					if (Main.player[num4].active && Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0 && (double)Main.player[num4].position.Y < Main.worldSurface * 16.0 + (double)sHeight)
					{
						int num5 = 3000;
						if ((double)Main.player[num4].position.X > Main.invasionX * 16.0 - (double)num5 && (double)Main.player[num4].position.X < Main.invasionX * 16.0 + (double)num5)
						{
							flag4 = true;
						}
					}
					flag = false;
					spawnRate = defaultSpawnRate;
					maxSpawns = defaultMaxSpawns;
					if (Main.hardMode)
					{
						spawnRate = (int)((double)defaultSpawnRate * 0.9);
						maxSpawns = defaultMaxSpawns + 1;
					}
					if (Main.player[num4].position.Y > (float)(Main.hellLayer * 16.0))
					{
						maxSpawns = (int)((float)maxSpawns * 2f);
					}
					else if ((double)Main.player[num4].position.Y > Main.rockLayer * 16.0 + (double)sHeight)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.9f);
					}
					else if ((double)Main.player[num4].position.Y > Main.worldSurface * 16.0 + (double)sHeight)
					{
						if (Main.hardMode)
						{
							spawnRate = (int)((double)spawnRate * 0.45);
							maxSpawns = (int)((float)maxSpawns * 1.8f);
						}
						else
						{
							spawnRate = (int)((double)spawnRate * 0.5);
							maxSpawns = (int)((float)maxSpawns * 1.7f);
						}
					}
					else if (!Main.dayTime)
					{
						spawnRate = (int)((double)spawnRate * 0.6);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
						if (Main.bloodMoon)
						{
							spawnRate = (int)((double)spawnRate * 0.3);
							maxSpawns = (int)((float)maxSpawns * 1.8f);
						}
					}
					if (Main.player[num4].zoneDungeon)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.7f);
					}
					else if (Main.player[num4].zoneJungle)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.5f);
					}
					else if (Main.player[num4].zoneEvil)
					{
						spawnRate = (int)((double)spawnRate * 0.65);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
					}
					else if (Main.player[num4].zoneMeteor)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.1f);
					}
					if (Main.player[num4].zoneHoly && (double)Main.player[num4].position.Y > Main.rockLayer * 16.0 + (double)sHeight)
					{
						spawnRate = (int)((double)spawnRate * 0.65);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
					}
					if (Main.wof >= 0 && Main.player[num4].position.Y > (float)(Main.hellLayer * 16.0))
					{
						maxSpawns = (int)((float)maxSpawns * 0.3f);
						spawnRate *= 3;
					}
					if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.2)
					{
						spawnRate = (int)((float)spawnRate * 0.6f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.4)
					{
						spawnRate = (int)((float)spawnRate * 0.7f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.6)
					{
						spawnRate = (int)((float)spawnRate * 0.8f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.8)
					{
						spawnRate = (int)((float)spawnRate * 0.9f);
					}
					if ((double)(Main.player[num4].position.Y * 16f) > (Main.worldSurface + Main.rockLayer) / 2.0 || Main.player[num4].zoneEvil)
					{
						if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.2)
						{
							spawnRate = (int)((float)spawnRate * 0.7f);
						}
						else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.4)
						{
							spawnRate = (int)((float)spawnRate * 0.9f);
						}
					}
					if (Main.player[num4].inventory[Main.player[num4].selectedItem].type == 148)
					{
						spawnRate = (int)((double)spawnRate * 0.75);
						maxSpawns = (int)((float)maxSpawns * 1.5f);
					}
					if (Main.player[num4].enemySpawns)
					{
						spawnRate = (int)((double)spawnRate * 0.5);
						maxSpawns = (int)((float)maxSpawns * 2f);
					}
					if ((double)spawnRate < (double)defaultSpawnRate * 0.1)
					{
						spawnRate = (int)((double)defaultSpawnRate * 0.1);
					}
					if (maxSpawns > defaultMaxSpawns * 3)
					{
						maxSpawns = defaultMaxSpawns * 3;
					}
					if (flag4)
					{
						maxSpawns = (int)((double)defaultMaxSpawns * (2.0 + 0.3 * (double)num3));
						spawnRate = 20;
					}
					if (Main.player[num4].zoneDungeon && !downedBoss3)
					{
						spawnRate = 10;
					}
					flag6 = false;
					if (!flag4 && (!Main.bloodMoon || Main.dayTime) && !Main.player[num4].zoneDungeon && !Main.player[num4].zoneEvil && !Main.player[num4].zoneMeteor)
					{
						if (Main.player[num4].townNPCs == 1f)
						{
							flag3 = true;
							if (Main.rand.Next(3) <= 1)
							{
								flag6 = true;
								maxSpawns = (int)((double)(float)maxSpawns * 0.6);
							}
							else
							{
								spawnRate = (int)((float)spawnRate * 2f);
							}
						}
						else if (Main.player[num4].townNPCs == 2f)
						{
							flag3 = true;
							if (Main.rand.Next(3) == 0)
							{
								flag6 = true;
								maxSpawns = (int)((double)(float)maxSpawns * 0.6);
							}
							else
							{
								spawnRate = (int)((float)spawnRate * 3f);
							}
						}
						else if (Main.player[num4].townNPCs >= 3f)
						{
							flag3 = true;
							flag6 = true;
							maxSpawns = (int)((double)(float)maxSpawns * 0.6);
						}
					}
					TMod.RunMethod(TMod.GenericHooks.UpdateSpawn);
					if (Main.player[num4].active && !Main.player[num4].dead && Main.player[num4].activeNPCs < (float)maxSpawns && Main.rand.Next(spawnRate) == 0)
					{
						int num6 = (int)(Main.player[num4].position.X / 16f) - spawnRangeX;
						int num7 = (int)(Main.player[num4].position.X / 16f) + spawnRangeX;
						int num8 = (int)(Main.player[num4].position.Y / 16f) - spawnRangeY;
						int num9 = (int)(Main.player[num4].position.Y / 16f) + spawnRangeY;
						int num10 = (int)(Main.player[num4].position.X / 16f) - safeRangeX;
						int num11 = (int)(Main.player[num4].position.X / 16f) + safeRangeX;
						int num12 = (int)(Main.player[num4].position.Y / 16f) - safeRangeY;
						int num13 = (int)(Main.player[num4].position.Y / 16f) + safeRangeY;
						if (num6 < 0)
						{
							num6 = 0;
						}
						if (num7 > Main.maxTilesX)
						{
							num7 = Main.maxTilesX;
						}
						if (num8 < 0)
						{
							num8 = 0;
						}
						if (num9 > Main.maxTilesY)
						{
							num9 = Main.maxTilesY;
						}
						for (int j = 0; j < 50; j++)
						{
							int num14 = Main.rand.Next(num6, num7);
							int num15 = Main.rand.Next(num8, num9);
							if (!Main.tile[num14, num15].active || !Main.tileSolid[Main.tile[num14, num15].type])
							{
								if (Main.wallHouse[Main.tile[num14, num15].wall])
								{
									continue;
								}
								if (!flag4 && (double)num15 < Main.worldSurface * 0.34999999403953552 && !flag6 && ((double)num14 < (double)Main.maxTilesX * 0.45 || (double)num14 > (double)Main.maxTilesX * 0.55 || Main.hardMode))
								{
									num = num14;
									num2 = num15;
									flag = true;
									flag2 = true;
								}
								else if (!flag4 && (double)num15 < Main.worldSurface * 0.44999998807907104 && !flag6 && Main.hardMode && Main.rand.Next(10) == 0)
								{
									num = num14;
									num2 = num15;
									flag = true;
									flag2 = true;
								}
								else
								{
									for (int k = num15; k < Main.maxTilesY; k++)
									{
										if (Main.tile[num14, k].active && Main.tileSolid[Main.tile[num14, k].type])
										{
											if (num14 < num10 || num14 > num11 || k < num12 || k > num13)
											{
												num = num14;
												num2 = k;
												flag = true;
											}
											break;
										}
									}
								}
								if (flag)
								{
									int num16 = num - spawnSpaceX / 2;
									int num17 = num + spawnSpaceX / 2;
									int num18 = num2 - spawnSpaceY;
									int num19 = num2;
									if (num16 < 0)
									{
										flag = false;
									}
									if (num17 > Main.maxTilesX)
									{
										flag = false;
									}
									if (num18 < 0)
									{
										flag = false;
									}
									if (num19 > Main.maxTilesY)
									{
										flag = false;
									}
									if (flag)
									{
										for (int l = num16; l < num17; l++)
										{
											for (int m = num18; m < num19; m++)
											{
												if (Main.tile[l, m].active && Main.tileSolid[Main.tile[l, m].type])
												{
													flag = false;
													break;
												}
												if (Main.tile[l, m].lava)
												{
													flag = false;
													break;
												}
											}
										}
									}
								}
							}
							if (flag || flag)
							{
								break;
							}
						}
					}
					if (flag)
					{
						Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
						for (int n = 0; n < 255; n++)
						{
							if (Main.player[n].active)
							{
								Rectangle rectangle2 = new Rectangle((int)(Main.player[n].position.X + (float)(Main.player[n].width / 2) - (float)(sWidth / 2) - (float)safeRangeX), (int)(Main.player[n].position.Y + (float)(Main.player[n].height / 2) - (float)(sHeight / 2) - (float)safeRangeY), sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
								if (rectangle.Intersects(rectangle2))
								{
									flag = false;
								}
							}
						}
					}
					if (flag)
					{
						if (Main.player[num4].zoneDungeon && (!Main.tileDungeon[Main.tile[num, num2].type] || Main.tile[num, num2 - 1].wall == 0))
						{
							flag = false;
						}
						if (Main.tile[num, num2 - 1].liquid > 0 && Main.tile[num, num2 - 2].liquid > 0 && !Main.tile[num, num2 - 1].lava)
						{
							flag5 = true;
						}
					}
					if (flag)
					{
						break;
					}
				}
				num4++;
			}
			flag = false;
			int num20 = Main.tile[num, num2].type;
			int num21 = 200;
			bool flag7 = false;
			ArrayList arrayList = new ArrayList();
			for (int num22 = 1; num22 < Config.npcDefs.GetSize(); num22++)
			{
				if (Config.npcDefs.assemblyByType[num22] != null)
				{
					Assembly assembly = Config.npcDefs.assemblyByType[num22];
					object code = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Config.npcDefs[num22].name) + "NPC");
					if (Codable.RunSpecifiedMethod("NPC " + Config.npcDefs[num22].name, code, "SpawnNPC", num, num2, num4) && (bool)Codable.customMethodReturn)
					{
						arrayList.Add(num22);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				int index = Main.rand.Next(arrayList.Count);
				flag7 = true;
				num21 = NewNPC(num * 16 + 8, num2 * 16, (int)arrayList[index]);
			}
			if (!flag7)
			{
				if (flag2)
				{
					if (Main.hardMode && Main.rand.Next(10) == 0 && !AnyNPCs(87))
					{
						NewNPC(num * 16 + 8, num2 * 16, 87, 1);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 48);
					}
				}
				else if (flag4)
				{
					if (Main.invasionType == 1)
					{
						if (Main.rand.Next(9) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 29);
						}
						else if (Main.rand.Next(5) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 26);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 111);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 27);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 28);
						}
					}
					else if (Main.invasionType == 2)
					{
						if (Main.rand.Next(7) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 145);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 143);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 144);
						}
					}
				}
				else if (flag5 && (num < 250 || num > Main.maxTilesX - 250) && num20 == 53 && (double)num2 < Main.rockLayer)
				{
					if (Main.rand.Next(8) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 65);
					}
					if (Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 67);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 64);
					}
				}
				else if (flag5 && (((double)num2 > Main.rockLayer && Main.rand.Next(2) == 0) || num20 == 60))
				{
					if (Main.hardMode && Main.rand.Next(3) > 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 102);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 58);
					}
				}
				else if (flag5 && (double)num2 > Main.worldSurface && Main.rand.Next(3) == 0)
				{
					if (Main.hardMode)
					{
						NewNPC(num * 16 + 8, num2 * 16, 103);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 63);
					}
				}
				else if (flag5 && Main.rand.Next(4) == 0)
				{
					if (Main.player[num4].zoneEvil)
					{
						NewNPC(num * 16 + 8, num2 * 16, 57);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 55);
					}
				}
				else if (downedGoblins && Main.rand.Next(20) == 0 && !flag5 && (double)num2 >= Main.rockLayer && num2 < Main.maxTilesY - 210 && !savedGoblin && !AnyNPCs(105))
				{
					NewNPC(num * 16 + 8, num2 * 16, 105);
				}
				else if (Main.hardMode && Main.rand.Next(20) == 0 && !flag5 && (double)num2 >= Main.rockLayer && num2 < Main.maxTilesY - 210 && !savedWizard && !AnyNPCs(106))
				{
					NewNPC(num * 16 + 8, num2 * 16, 106);
				}
				else if (flag6)
				{
					if (flag5)
					{
						NewNPC(num * 16 + 8, num2 * 16, 55);
					}
					else
					{
						if (num20 != 2 && num20 != 109 && num20 != 147 && (double)num2 <= Main.worldSurface)
						{
							return;
						}
						if (Main.rand.Next(2) == 0 && (double)num2 <= Main.worldSurface)
						{
							NewNPC(num * 16 + 8, num2 * 16, 74);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 46);
						}
					}
				}
				else if (Main.player[num4].zoneDungeon)
				{
					if (!downedBoss3)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 68);
					}
					else if (!savedMech && Main.rand.Next(5) == 0 && !flag5 && !AnyNPCs(123) && (double)num2 > Main.rockLayer)
					{
						NewNPC(num * 16 + 8, num2 * 16, 123);
					}
					else if (Main.rand.Next(37) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 71);
					}
					else if (Main.rand.Next(4) == 0 && !NearSpikeBall(num, num2))
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 70);
					}
					else if (Main.rand.Next(15) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 72);
					}
					else if (Main.rand.Next(9) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 34);
					}
					else if (Main.rand.Next(7) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 32);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 31);
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Big Boned");
						}
						else if (Main.rand.Next(5) == 0)
						{
							Main.npc[num21].SetDefaults("Short Bones");
						}
					}
				}
				else if (Main.player[num4].zoneMeteor)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 23);
				}
				else if (Main.player[num4].zoneEvil && Main.rand.Next(65) == 0)
				{
					num21 = ((!Main.hardMode || Main.rand.Next(4) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 7, 1) : NewNPC(num * 16 + 8, num2 * 16, 98, 1));
				}
				else if (Main.hardMode && (double)num2 > Main.worldSurface && Main.rand.Next(75) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 85);
				}
				else if (Main.hardMode && Main.tile[num, num2 - 1].wall == 2 && Main.rand.Next(20) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 85);
				}
				else if (Main.hardMode && (double)num2 <= Main.worldSurface && !Main.dayTime && (Main.rand.Next(20) == 0 || (Main.rand.Next(5) == 0 && Main.moonPhase == 4)))
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 82);
				}
				else if (num20 == 60 && Main.rand.Next(500) == 0 && !Main.dayTime)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 52);
				}
				else if (num20 == 60 && (double)num2 > (Main.worldSurface + Main.rockLayer) / 2.0)
				{
					if (Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 43);
						Main.npc[num21].ai[0] = num;
						Main.npc[num21].ai[1] = num2;
						Main.npc[num21].netUpdate = true;
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 42);
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Little Stinger");
						}
						else if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Big Stinger");
						}
					}
				}
				else if (num20 == 60 && Main.rand.Next(4) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 51);
				}
				else if (num20 == 60 && Main.rand.Next(8) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 56);
					Main.npc[num21].ai[0] = num;
					Main.npc[num21].ai[1] = num2;
					Main.npc[num21].netUpdate = true;
				}
				else if (Main.hardMode && num20 == 53 && Main.rand.Next(3) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 78);
				}
				else if (Main.hardMode && num20 == 112 && Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 79);
				}
				else if (Main.hardMode && num20 == 116 && Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 80);
				}
				else if (Main.hardMode && !flag5 && (double)num2 < Main.rockLayer && (num20 == 116 || num20 == 117 || num20 == 109))
				{
					num21 = ((!Main.dayTime && Main.rand.Next(2) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 122) : ((Main.rand.Next(10) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 75) : NewNPC(num * 16 + 8, num2 * 16, 86)));
				}
				else if (!flag3 && Main.hardMode && Main.rand.Next(50) == 0 && !flag5 && (double)num2 >= Main.rockLayer && (num20 == 116 || num20 == 117 || num20 == 109))
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 84);
				}
				else if ((num20 == 22 && Main.player[num4].zoneEvil) || num20 == 23 || num20 == 25 || num20 == 112)
				{
					if (Main.hardMode && (double)num2 >= Main.rockLayer && Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 101);
						Main.npc[num21].ai[0] = num;
						Main.npc[num21].ai[1] = num2;
						Main.npc[num21].netUpdate = true;
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						num21 = ((Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 81) : NewNPC(num * 16 + 8, num2 * 16, 121));
					}
					else if (Main.hardMode && (double)num2 >= Main.rockLayer && Main.rand.Next(40) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 83);
					}
					else if (Main.hardMode && (Main.rand.Next(2) == 0 || (double)num2 > Main.rockLayer))
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 94);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 6);
						if (Main.rand.Next(3) == 0)
						{
							Main.npc[num21].SetDefaults("Little Eater");
						}
						else if (Main.rand.Next(3) == 0)
						{
							Main.npc[num21].SetDefaults("Big Eater");
						}
					}
				}
				else if ((double)num2 <= Main.worldSurface)
				{
					if (Main.dayTime)
					{
						int num23 = Math.Abs(num - Main.spawnTileX);
						if (num23 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num20 == 2 || num20 == 109 || num20 == 147))
						{
							NewNPC(num * 16 + 8, num2 * 16, 46);
						}
						else if (num23 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num20 == 2 || num20 == 109 || num20 == 147))
						{
							NewNPC(num * 16 + 8, num2 * 16, 74);
						}
						else if (num23 > Main.maxTilesX / 3 && num20 == 2 && Main.rand.Next(300) == 0 && !AnyNPCs(50))
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 50);
						}
						else if (num20 == 53 && Main.rand.Next(5) == 0 && !flag5)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 69);
						}
						else if (num20 == 53 && !flag5)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 61);
						}
						else if (num23 > Main.maxTilesX / 3 && Main.rand.Next(15) == 0)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 73);
						}
						else
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
							if (num20 == 60)
							{
								Main.npc[num21].SetDefaults("Jungle Slime");
							}
							else if (Main.rand.Next(3) == 0 || num23 < 200)
							{
								Main.npc[num21].SetDefaults("Green Slime");
							}
							else if (Main.rand.Next(10) == 0 && num23 > 400)
							{
								Main.npc[num21].SetDefaults("Purple Slime");
							}
						}
					}
					else if (Main.rand.Next(6) == 0 || (Main.moonPhase == 4 && Main.rand.Next(2) == 0))
					{
						num21 = ((!Main.hardMode || Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 2) : NewNPC(num * 16 + 8, num2 * 16, 133));
					}
					else if (Main.hardMode && Main.rand.Next(50) == 0 && Main.bloodMoon && !AnyNPCs(109))
					{
						NewNPC(num * 16 + 8, num2 * 16, 109);
					}
					else if (Main.rand.Next(250) == 0 && Main.bloodMoon)
					{
						NewNPC(num * 16 + 8, num2 * 16, 53);
					}
					else if (Main.moonPhase == 0 && Main.hardMode && Main.rand.Next(3) != 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 104);
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 140);
					}
					else if (Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 132);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 3);
					}
				}
				else if ((double)num2 <= Main.rockLayer)
				{
					if (!flag3 && Main.rand.Next(50) == 0)
					{
						num21 = ((!Main.hardMode) ? NewNPC(num * 16 + 8, num2 * 16, 10, 1) : NewNPC(num * 16 + 8, num2 * 16, 95, 1));
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 140);
					}
					else if (Main.hardMode && Main.rand.Next(4) != 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 141);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
						if (Main.rand.Next(5) == 0)
						{
							Main.npc[num21].SetDefaults("Yellow Slime");
						}
						else if (Main.rand.Next(2) == 0)
						{
							Main.npc[num21].SetDefaults("Blue Slime");
						}
						else
						{
							Main.npc[num21].SetDefaults("Red Slime");
						}
					}
				}
				else if (num2 > Main.maxTilesY - 190)
				{
					num21 = ((Main.rand.Next(40) == 0 && !AnyNPCs(39)) ? NewNPC(num * 16 + 8, num2 * 16, 39, 1) : ((Main.rand.Next(14) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 24) : ((Main.rand.Next(8) == 0) ? ((Main.rand.Next(7) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 62) : NewNPC(num * 16 + 8, num2 * 16, 66)) : ((Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 60) : NewNPC(num * 16 + 8, num2 * 16, 59)))));
				}
				else if ((num20 == 116 || num20 == 117) && !flag3 && Main.rand.Next(8) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 120);
				}
				else if (!flag3 && Main.rand.Next(75) == 0 && !Main.player[num4].zoneHoly)
				{
					num21 = ((!Main.hardMode) ? NewNPC(num * 16 + 8, num2 * 16, 10, 1) : NewNPC(num * 16 + 8, num2 * 16, 95, 1));
				}
				else if (!Main.hardMode && Main.rand.Next(10) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 16);
				}
				else if (!Main.hardMode && Main.rand.Next(4) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
					if (Main.player[num4].zoneJungle)
					{
						Main.npc[num21].SetDefaults("Jungle Slime");
					}
					else
					{
						Main.npc[num21].SetDefaults("Black Slime");
					}
				}
				else if (Main.rand.Next(2) != 0)
				{
					num21 = ((Main.hardMode && (Main.player[num4].zoneHoly & (Main.rand.Next(2) == 0))) ? NewNPC(num * 16 + 8, num2 * 16, 138) : (Main.player[num4].zoneJungle ? NewNPC(num * 16 + 8, num2 * 16, 51) : ((Main.hardMode && Main.player[num4].zoneHoly) ? NewNPC(num * 16 + 8, num2 * 16, 137) : ((!Main.hardMode || Main.rand.Next(6) <= 0) ? NewNPC(num * 16 + 8, num2 * 16, 49) : NewNPC(num * 16 + 8, num2 * 16, 93)))));
				}
				else if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(700) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 45);
				}
				else if (!Main.hardMode || Main.rand.Next(10) == 0)
				{
					num21 = ((Main.rand.Next(15) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 21) : NewNPC(num * 16 + 8, num2 * 16, 44));
				}
				else if (Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 77);
					if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(5) == 0)
					{
						Main.npc[num21].SetDefaults("Heavy Skeleton");
					}
				}
				else
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 110);
				}
			}
			if (Main.npc[num21].type == 1 && Main.rand.Next(250) == 0)
			{
				Main.npc[num21].SetDefaults("Pinky");
			}
			if (Main.netMode == 2 && num21 < 200)
			{
				NetMessage.SendData(23, -1, -1, "", num21);
			}
		}

		public static void SpawnWOF(Vector2 pos)
		{
			if (pos.Y / 16f < (float)(Main.hellLayer - 5.0) || Main.wof >= 0 || Main.netMode == 1)
			{
				return;
			}
			Player.FindClosest(pos, 16, 16);
			int num = 1;
			if (pos.X / 16f > (float)(Main.maxTilesX / 2))
			{
				num = -1;
			}
			bool flag = false;
			int num2 = (int)pos.X;
			while (!flag)
			{
				flag = true;
				for (int i = 0; i < 255; i++)
				{
					if (Main.player[i].active && Main.player[i].position.X > (float)(num2 - 1200) && Main.player[i].position.X < (float)(num2 + 1200))
					{
						num2 -= num * 16;
						flag = false;
					}
				}
				if (num2 / 16 < 20 || num2 / 16 > Main.maxTilesX - 20)
				{
					flag = true;
				}
			}
			int num3 = (int)pos.Y;
			int num4 = num2 / 16;
			int num5 = num3 / 16;
			int num6 = 0;
			try
			{
				while (true)
				{
					if (!WorldGen.SolidTile(num4, num5 - num6) && Main.tile[num4, num5 - num6].liquid < 100)
					{
						num5 -= num6;
						break;
					}
					if (!WorldGen.SolidTile(num4, num5 + num6) && Main.tile[num4, num5 + num6].liquid < 100)
					{
						num5 += num6;
						break;
					}
					num6++;
				}
			}
			catch
			{
			}
			num3 = num5 * 16;
			int num7 = NewNPC(num2, num3, 113);
			if (Main.npc[num7].displayName == "")
			{
				Main.npc[num7].displayName = Main.npc[num7].name;
			}
			if (Main.netMode == 0)
			{
				Main.NewText(Main.npc[num7].displayName + " " + Lang.misc[16], 175, 75);
			}
			else if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, Main.npc[num7].displayName + " " + Lang.misc[16], 255, 175f, 75f, 255f);
			}
		}

		public static void SpawnOnPlayer(int plr, string name)
		{
			SpawnOnPlayer(plr, Config.npcDefs.byName[name].type);
		}

		public static void SpawnOnPlayer(int plr, int Type)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = (int)(Main.player[plr].position.X / 16f) - spawnRangeX * 2;
			int num4 = (int)(Main.player[plr].position.X / 16f) + spawnRangeX * 2;
			int num5 = (int)(Main.player[plr].position.Y / 16f) - spawnRangeY * 2;
			int num6 = (int)(Main.player[plr].position.Y / 16f) + spawnRangeY * 2;
			int num7 = (int)(Main.player[plr].position.X / 16f) - safeRangeX;
			int num8 = (int)(Main.player[plr].position.X / 16f) + safeRangeX;
			int num9 = (int)(Main.player[plr].position.Y / 16f) - safeRangeY;
			int num10 = (int)(Main.player[plr].position.Y / 16f) + safeRangeY;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesX)
			{
				num4 = Main.maxTilesX;
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesY)
			{
				num6 = Main.maxTilesY;
			}
			for (int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					int num11 = Main.rand.Next(num3, num4);
					int num12 = Main.rand.Next(num5, num6);
					if (!Main.tile[num11, num12].active || !Main.tileSolid[Main.tile[num11, num12].type])
					{
						if (Main.wallHouse[Main.tile[num11, num12].wall] && i < 999)
						{
							continue;
						}
						for (int k = num12; k < Main.maxTilesY; k++)
						{
							if (Main.tile[num11, k].active && Main.tileSolid[Main.tile[num11, k].type])
							{
								if (num11 < num7 || num11 > num8 || k < num9 || k > num10 || i == 999)
								{
									num = num11;
									num2 = k;
									flag = true;
								}
								break;
							}
						}
						if (flag && i < 999)
						{
							int num13 = num - spawnSpaceX / 2;
							int num14 = num + spawnSpaceX / 2;
							int num15 = num2 - spawnSpaceY;
							int num16 = num2;
							if (num13 < 0)
							{
								flag = false;
							}
							if (num14 > Main.maxTilesX)
							{
								flag = false;
							}
							if (num15 < 0)
							{
								flag = false;
							}
							if (num16 > Main.maxTilesY)
							{
								flag = false;
							}
							if (flag)
							{
								for (int l = num13; l < num14; l++)
								{
									for (int m = num15; m < num16; m++)
									{
										if (Main.tile[l, m].active && Main.tileSolid[Main.tile[l, m].type])
										{
											flag = false;
											break;
										}
									}
								}
							}
						}
					}
					if (flag || flag)
					{
						break;
					}
				}
				if (flag && i < 999)
				{
					Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
					for (int n = 0; n < 255; n++)
					{
						if (Main.player[n].active)
						{
							Rectangle rectangle2 = new Rectangle((int)(Main.player[n].position.X + (float)(Main.player[n].width / 2) - (float)(sWidth / 2) - (float)safeRangeX), (int)(Main.player[n].position.Y + (float)(Main.player[n].height / 2) - (float)(sHeight / 2) - (float)safeRangeY), sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
							if (rectangle.Intersects(rectangle2))
							{
								flag = false;
							}
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			int num17 = NewNPC(num * 16 + 8, num2 * 16, Type, 1);
			if (num17 == 200)
			{
				return;
			}
			Main.npc[num17].target = plr;
			Main.npc[num17].timeLeft *= 20;
			string name = Main.npc[num17].name;
			if (Main.npc[num17].displayName != "")
			{
				name = Main.npc[num17].displayName;
			}
			if (Main.netMode == 2 && num17 < 200)
			{
				NetMessage.SendData(23, -1, -1, "", num17);
			}
			switch (Type)
			{
			case 50:
			case 82:
			case 126:
				break;
			case 125:
				if (Main.netMode == 0)
				{
					Main.NewText("The Twins " + Lang.misc[16], 175, 75);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, "The Twins " + Lang.misc[16], 255, 175f, 75f, 255f);
				}
				break;
			default:
				if (Main.netMode == 0)
				{
					Main.NewText(name + " " + Lang.misc[16], 175, 75);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, name + " " + Lang.misc[16], 255, 175f, 75f, 255f);
				}
				break;
			}
		}

		public static int NewNPC(int X, int Y, string name, int Start = 0)
		{
			return NewNPC(X, Y, Config.npcDefs.byName[name].type, Start);
		}

		public static int NewNPC(int X, int Y, int Type, int Start = 0)
		{
			int num = -1;
			for (int i = Start; i < 200; i++)
			{
				if (!Main.npc[i].active)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				Main.npc[num] = new NPC();
				Main.npc[num].SetDefaults(Type);
				Main.npc[num].position.X = X - Main.npc[num].width / 2;
				Main.npc[num].position.Y = Y - Main.npc[num].height;
				Main.npc[num].active = true;
				Main.npc[num].timeLeft = (int)((double)activeTime * 1.25);
				Main.npc[num].wet = Collision.WetCollision(Main.npc[num].position, Main.npc[num].width, Main.npc[num].height);
				Main.npc[num].whoAmI = num;
				if (Type == 50)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Main.npc[num].name + " " + Lang.misc[16], 175, 75);
					}
					else if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, Main.npc[num].name + " " + Lang.misc[16], 255, 175f, 75f, 255f);
					}
				}
				Main.npc[num].oldPos = new Vector2[10];
				return num;
			}
			return 200;
		}

		public void Transform(int newType)
		{
			if (Main.netMode != 1)
			{
				Vector2 velocity = base.velocity;
				position.Y += (float)height;
				int num = spriteDirection;
				SetDefaults(newType);
				spriteDirection = num;
				TargetClosest();
				base.velocity = velocity;
				position.Y -= (float)height;
				if (newType == 107 || newType == 108)
				{
					homeTileX = (int)(position.X + (float)(width / 2)) / 16;
					homeTileY = (int)(position.Y + (float)height) / 16;
					homeless = true;
				}
				if (Main.netMode == 2)
				{
					netUpdate = true;
					NetMessage.SendData(23, -1, -1, "", whoAmI);
				}
			}
		}

		public double StrikeNPC(int Damage, float knockBack, int hitDirection, bool crit = false, bool noEffect = false, float CritMultiplier = 2f)
		{
			if (!active || life <= 0)
			{
				return 0.0;
			}
			double num = Damage;
			num = Main.CalculateDamage((int)num, defense);
			if (crit)
			{
				num *= (double)CritMultiplier;
			}
			if (Damage != 9999 && lifeMax > 1)
			{
				if (friendly)
				{
					CombatText.NewText(new Rectangle((int)position.X, (int)position.Y, width, height), CombatText.PlayerHurtColor, string.Concat((int)num), crit);
				}
				else
				{
					CombatText.NewText(new Rectangle((int)position.X, (int)position.Y, width, height), CombatText.NPCHurtColor, string.Concat((int)num), crit);
				}
			}
			if (num >= 1.0)
			{
				justHit = true;
				if (townNPC)
				{
					ai[0] = 1f;
					ai[1] = 300 + Main.rand.Next(300);
					ai[2] = 0f;
					direction = hitDirection;
					netUpdate = true;
				}
				if (aiStyle == 8 && Main.netMode != 1)
				{
					ai[0] = 400f;
					TargetClosest();
				}
				if (realLife >= 0)
				{
					Main.npc[realLife].life -= (int)num;
					life = Main.npc[realLife].life;
					lifeMax = Main.npc[realLife].lifeMax;
				}
				else
				{
					life -= (int)num;
				}
				if (knockBack > 0f && knockBackResist > 0f)
				{
					float num2 = knockBack * knockBackResist;
					if (num2 > 8f)
					{
						num2 = 8f;
					}
					if (crit)
					{
						num2 *= 1.4f;
					}
					if (num * 10.0 < (double)lifeMax)
					{
						if (hitDirection < 0 && velocity.X > 0f - num2)
						{
							if (velocity.X > 0f)
							{
								velocity.X -= num2;
							}
							velocity.X -= num2;
							if (velocity.X < 0f - num2)
							{
								velocity.X = 0f - num2;
							}
						}
						else if (hitDirection > 0 && velocity.X < num2)
						{
							if (velocity.X < 0f)
							{
								velocity.X += num2;
							}
							velocity.X += num2;
							if (velocity.X > num2)
							{
								velocity.X = num2;
							}
						}
						num2 = (noGravity ? (num2 * -0.5f) : (num2 * -0.75f));
						if (velocity.Y > num2)
						{
							velocity.Y += num2;
							if (velocity.Y < num2)
							{
								velocity.Y = num2;
							}
						}
					}
					else
					{
						if (!noGravity)
						{
							velocity.Y = (0f - num2) * 0.75f * knockBackResist;
						}
						else
						{
							velocity.Y = (0f - num2) * 0.5f * knockBackResist;
						}
						velocity.X = num2 * (float)hitDirection * knockBackResist;
					}
				}
				if ((type == 113 || type == 114) && life <= 0)
				{
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].active && (Main.npc[i].type == 113 || Main.npc[i].type == 114))
						{
							Main.npc[i].HitEffect(hitDirection, num);
						}
					}
				}
				else
				{
					HitEffect(hitDirection, num);
				}
				if (soundHit > 0)
				{
					Main.PlaySound(3, (int)position.X, (int)position.Y, soundHit);
				}
				if (realLife >= 0)
				{
					Main.npc[realLife].checkDead();
				}
				else
				{
					checkDead();
				}
				return num;
			}
			return 0.0;
		}

		public void checkDead()
		{
			if (!active || (realLife >= 0 && realLife != whoAmI) || life > 0)
			{
				return;
			}
			noSpawnCycle = true;
			if (townNPC && type != 37 && !dontDrawFace)
			{
				string name = base.name;
				if (displayName != "")
				{
					name = displayName;
				}
				if (Main.netMode == 0)
				{
					Main.NewText(name + Lang.misc[19], byte.MaxValue, 25, 25);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, name + Lang.misc[19], 255, 255f, 25f, 25f);
				}
				if (Main.netMode != 1)
				{
					Main.chrName[type] = "";
					setNames();
					NetMessage.SendData(56, -1, -1, "", type);
				}
			}
			if (townNPC && Main.netMode != 1 && homeless && WorldGen.spawnNPC == type)
			{
				WorldGen.spawnNPC = 0;
			}
			if (soundKilled > 0)
			{
				Main.PlaySound(4, (int)position.X, (int)position.Y, soundKilled);
			}
			active = false;
			NPCLoot();
			if (!active && (type == 26 || type == 27 || type == 28 || type == 29 || type == 111 || type == 143 || type == 144 || type == 145))
			{
				Main.invasionSize--;
			}
		}

		public void NPCLoot()
		{
			if (Main.hardMode && lifeMax > 1 && damage > 0 && !friendly && (double)position.Y > Main.rockLayer * 16.0 && Main.rand.Next(7) == 0 && type != 121 && value > 0f)
			{
				if (Main.player[Player.FindClosest(position, width, height)].zoneEvil)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 521);
				}
				if (Main.player[Player.FindClosest(position, width, height)].zoneHoly)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 520);
				}
			}
			if (Main.xMas && lifeMax > 1 && damage > 0 && !friendly && type != 121 && value > 0f && Main.rand.Next(13) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, Main.rand.Next(599, 602));
			}
			if (type == 109 && !downedClown)
			{
				downedClown = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			RunMethod("NPCLoot");
			if (Config.npcDefs.dropTables.ContainsKey(netID))
			{
				foreach (DropTable item2 in Config.npcDefs.dropTables[netID])
				{
					foreach (Drop drop in item2.drops)
					{
						Item item = drop.TryDrop();
						if (item != null)
						{
							Item.NewItem((int)position.X, (int)position.Y, width, height, item.type, item.stack, noBroadcast: false, -1);
						}
					}
				}
			}
			if (type == 85 && value > 0f)
			{
				int num = Main.rand.Next(7);
				if (num == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 437, 1, noBroadcast: false, -1);
				}
				if (num == 1)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 517, 1, noBroadcast: false, -1);
				}
				if (num == 2)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 535, 1, noBroadcast: false, -1);
				}
				if (num == 3)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 536, 1, noBroadcast: false, -1);
				}
				if (num == 4)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 532, 1, noBroadcast: false, -1);
				}
				if (num == 5)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 393, 1, noBroadcast: false, -1);
				}
				if (num == 6)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 554, 1, noBroadcast: false, -1);
				}
			}
			if (type == 87)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 575, Main.rand.Next(5, 11));
			}
			if (type == 143 || type == 144 || type == 145)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 593, Main.rand.Next(5, 11));
			}
			if (type == 79)
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 527);
				}
			}
			else if (type == 80 && Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 528);
			}
			if (type == 101 || type == 98)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 522, Main.rand.Next(2, 6));
			}
			if (type == 86)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 526);
			}
			if (type == 113)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 367, 1, noBroadcast: false, -1);
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, Main.rand.Next(489, 492), 1, noBroadcast: false, -1);
				}
				else
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						Item.NewItem((int)position.X, (int)position.Y, width, height, 514, 1, noBroadcast: false, -1);
						break;
					case 1:
						Item.NewItem((int)position.X, (int)position.Y, width, height, 426, 1, noBroadcast: false, -1);
						break;
					case 2:
						Item.NewItem((int)position.X, (int)position.Y, width, height, 434, 1, noBroadcast: false, -1);
						break;
					}
				}
				if (Main.netMode != 1)
				{
					int num2 = (int)(position.X + (float)(width / 2)) / 16;
					int num3 = (int)(position.Y + (float)(height / 2)) / 16;
					int num4 = width / 2 / 16 + 1;
					for (int i = num2 - num4; i <= num2 + num4; i++)
					{
						for (int j = num3 - num4; j <= num3 + num4; j++)
						{
							if ((i == num2 - num4 || i == num2 + num4 || j == num3 - num4 || j == num3 + num4) && !Main.tile[i, j].active)
							{
								Main.tile[i, j].type = 140;
								Main.tile[i, j].active = true;
							}
							Main.tile[i, j].lava = false;
							Main.tile[i, j].liquid = 0;
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, i, j, 1);
							}
							else
							{
								WorldGen.SquareTileFrame(i, j);
							}
						}
					}
				}
			}
			if (type == 1 || type == 16 || type == 138 || type == 141)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 23, Main.rand.Next(1, 3));
			}
			if (type == 75)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 501, Main.rand.Next(1, 4));
			}
			if (type == 81)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 23, Main.rand.Next(2, 5));
			}
			if (type == 122)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 23, Main.rand.Next(5, 11));
			}
			if (type == 71)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 327);
			}
			if (type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 38);
				}
				else if (Main.rand.Next(100) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 236);
				}
			}
			if (type == 104 && Main.rand.Next(60) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 485, 1, noBroadcast: false, -1);
			}
			if (type == 58)
			{
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 263);
				}
				else if (Main.rand.Next(40) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 118);
				}
			}
			if (type == 102 && Main.rand.Next(500) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 263);
			}
			if ((type == 3 || type == 132) && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 216, 1, noBroadcast: false, -1);
			}
			if (type == 66)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 267);
			}
			if (type == 62 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 272, 1, noBroadcast: false, -1);
			}
			if (type == 52)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 251);
			}
			if (type == 53)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 239);
			}
			if (type == 54)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 260);
			}
			if (type == 55)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 261);
			}
			if (type == 69 && Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 323);
			}
			if (type == 73)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 362, Main.rand.Next(1, 3));
			}
			if (type == 4)
			{
				int stack = Main.rand.Next(30) + 20;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 47, stack);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack);
				stack = Main.rand.Next(3) + 1;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 59, stack);
			}
			if ((type == 6 || type == 94) && Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 68);
			}
			if (type == 7 || type == 8 || type == 9)
			{
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 68, Main.rand.Next(1, 3));
				}
				Item.NewItem((int)position.X, (int)position.Y, width, height, 69, Main.rand.Next(3, 9));
			}
			if ((type == 10 || type == 11 || type == 12 || type == 95 || type == 96 || type == 97) && Main.rand.Next(500) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 215);
			}
			if (type == 47 && Main.rand.Next(75) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 243);
			}
			if (type == 13 || type == 14 || type == 15)
			{
				int stack2 = Main.rand.Next(1, 3);
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 86, stack2);
				}
				if (Main.rand.Next(2) == 0)
				{
					stack2 = Main.rand.Next(2, 6);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack2);
				}
				if (boss)
				{
					stack2 = Main.rand.Next(10, 30);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack2);
					stack2 = Main.rand.Next(10, 31);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 56, stack2);
				}
				if (Main.rand.Next(3) == 0 && Main.player[Player.FindClosest(position, width, height)].statLife < Main.player[Player.FindClosest(position, width, height)].statLifeMax2)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 58);
				}
			}
			if (type == 116 || type == 117 || type == 118 || type == 119 || type == 139)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 58);
			}
			if (type == 63 || type == 64 || type == 103)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 282, Main.rand.Next(1, 5));
			}
			if (type == 21 || type == 44)
			{
				if (Main.rand.Next(25) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 118);
				}
				else if (type == 44)
				{
					if (Main.rand.Next(20) == 0)
					{
						Item.NewItem((int)position.X, (int)position.Y, width, height, Main.rand.Next(410, 412));
					}
					else
					{
						Item.NewItem((int)position.X, (int)position.Y, width, height, 166, Main.rand.Next(1, 4));
					}
				}
			}
			if (type == 45)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 238);
			}
			if (type == 50)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, Main.rand.Next(256, 259));
			}
			if (type == 23 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 116);
			}
			if (type == 24 && Main.rand.Next(300) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 244);
			}
			if (type == 31 || type == 32 || type == 34)
			{
				if (Main.rand.Next(65) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 327);
				}
				else
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 154, Main.rand.Next(1, 4));
				}
			}
			if (type == 26 || type == 27 || type == 28 || type == 29 || type == 111)
			{
				if (Main.rand.Next(200) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 160);
				}
				else if (Main.rand.Next(2) == 0)
				{
					int stack3 = Main.rand.Next(1, 6);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 161, stack3);
				}
			}
			if (type == 42 && Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 209);
			}
			if (type == 43 && Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 210);
			}
			if (type == 65)
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 268);
				}
				else
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 319);
				}
			}
			if (type == 48 && Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 320);
			}
			if (type == 125 || type == 126)
			{
				int num5 = 125;
				if (type == 125)
				{
					num5 = 126;
				}
				if (!AnyNPCs(num5))
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 549, Main.rand.Next(20, 31));
				}
				else
				{
					value = 0f;
					boss = false;
				}
			}
			else if (type == 127)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 547, Main.rand.Next(20, 31));
			}
			else if (type == 134)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 548, Main.rand.Next(20, 31));
			}
			if (boss)
			{
				if (type == 4)
				{
					downedBoss1 = true;
				}
				else if (type == 13 || type == 14 || type == 15)
				{
					downedBoss2 = true;
					base.name = "Eater of Worlds";
				}
				else if (type == 35)
				{
					downedBoss3 = true;
					base.name = "Skeletron";
				}
				else
				{
					base.name = displayName;
				}
				string name = base.name;
				if (displayName != "")
				{
					name = displayName;
				}
				int stack4 = Main.rand.Next(5, 16);
				int num6 = 28;
				if (type >= 113 && type <= 147)
				{
					num6 = ((type != 113) ? 499 : 188);
				}
				if (type <= 147)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, num6, stack4);
				}
				int num7 = Main.rand.Next(5) + 5;
				for (int k = 0; k < num7; k++)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 58);
				}
				if (type == 125 || type == 126)
				{
					if (Main.netMode == 0)
					{
						Main.NewText("The Twins " + Lang.misc[17], 175, 75);
					}
					else if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, "The Twins " + Lang.misc[17], 255, 175f, 75f, 255f);
					}
				}
				else if (Main.netMode == 0)
				{
					Main.NewText(name + " " + Lang.misc[17], 175, 75);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, name + " " + Lang.misc[17], 255, 175f, 75f, 255f);
				}
				if (type == 113 && Main.netMode != 1)
				{
					WorldGen.StartHardmode();
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			if (Main.rand.Next(6) == 0 && lifeMax > 1 && damage > 0)
			{
				if (Main.rand.Next(2) == 0 && Main.player[Player.FindClosest(position, width, height)].statMana < Main.player[Player.FindClosest(position, width, height)].statManaMax)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 184);
				}
				else if (Main.rand.Next(2) == 0 && Main.player[Player.FindClosest(position, width, height)].statLife < Main.player[Player.FindClosest(position, width, height)].statLifeMax2)
				{
					Item.NewItem((int)position.X, (int)position.Y, width, height, 58);
				}
			}
			if (Main.rand.Next(2) == 0 && lifeMax > 1 && damage > 0 && Main.player[Player.FindClosest(position, width, height)].statMana < Main.player[Player.FindClosest(position, width, height)].statManaMax)
			{
				Item.NewItem((int)position.X, (int)position.Y, width, height, 184);
			}
			float num8 = value;
			num8 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
			if (Main.rand.Next(5) == 0)
			{
				num8 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
			}
			if (Main.rand.Next(10) == 0)
			{
				num8 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
			}
			if (Main.rand.Next(15) == 0)
			{
				num8 *= 1f + (float)Main.rand.Next(15, 31) * 0.01f;
			}
			if (Main.rand.Next(20) == 0)
			{
				num8 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
			}
			while ((int)num8 > 0)
			{
				if (num8 > 1000000f)
				{
					int num9 = (int)(num8 / 1000000f);
					if (num9 > 50 && Main.rand.Next(5) == 0)
					{
						num9 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(5) == 0)
					{
						num9 /= Main.rand.Next(3) + 1;
					}
					num8 -= (float)(1000000 * num9);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 74, num9);
					continue;
				}
				if (num8 > 10000f)
				{
					int num10 = (int)(num8 / 10000f);
					if (num10 > 50 && Main.rand.Next(5) == 0)
					{
						num10 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(5) == 0)
					{
						num10 /= Main.rand.Next(3) + 1;
					}
					num8 -= (float)(10000 * num10);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 73, num10);
					continue;
				}
				if (num8 > 100f)
				{
					int num11 = (int)(num8 / 100f);
					if (num11 > 50 && Main.rand.Next(5) == 0)
					{
						num11 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(5) == 0)
					{
						num11 /= Main.rand.Next(3) + 1;
					}
					num8 -= (float)(100 * num11);
					Item.NewItem((int)position.X, (int)position.Y, width, height, 72, num11);
					continue;
				}
				int num12 = (int)num8;
				if (num12 > 50 && Main.rand.Next(5) == 0)
				{
					num12 /= Main.rand.Next(3) + 1;
				}
				if (Main.rand.Next(5) == 0)
				{
					num12 /= Main.rand.Next(4) + 1;
				}
				if (num12 < 1)
				{
					num12 = 1;
				}
				num8 -= (float)num12;
				Item.NewItem((int)position.X, (int)position.Y, width, height, 71, num12);
			}
		}

		public void HitEffect(int hitDirection = 0, double dmg = 10.0)
		{
			if (!active || RunMethod("HitEffect", hitDirection, dmg))
			{
				return;
			}
			if (type == 1 || type == 16 || type == 71)
			{
				if (life > 0)
				{
					for (int i = 0; (double)i < dmg / (double)lifeMax * 100.0; i++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, hitDirection, -1f, alpha, color);
					}
				}
				else
				{
					for (int j = 0; j < 50; j++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, 2 * hitDirection, -2f, alpha, color);
					}
					if (Main.netMode != 1 && type == 16)
					{
						int num = Main.rand.Next(2) + 2;
						for (int k = 0; k < num; k++)
						{
							int num2 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), 1);
							Main.npc[num2].SetDefaults("Baby Slime");
							Main.npc[num2].velocity.X = base.velocity.X * 2f;
							Main.npc[num2].velocity.Y = base.velocity.Y;
							NPC nPC = Main.npc[num2];
							nPC.velocity.X = nPC.velocity.X + ((float)Main.rand.Next(-20, 20) * 0.1f + (float)(k * direction) * 0.3f);
							NPC nPC2 = Main.npc[num2];
							nPC2.velocity.Y = nPC2.velocity.Y - ((float)Main.rand.Next(0, 10) * 0.1f + (float)k);
							Main.npc[num2].ai[1] = k;
							if (Main.netMode == 2 && num2 < 200)
							{
								NetMessage.SendData(23, -1, -1, "", num2);
							}
						}
					}
				}
			}
			if (type == 143 || type == 144 || type == 145)
			{
				if (life > 0)
				{
					for (int l = 0; (double)l < dmg / (double)lifeMax * 100.0; l++)
					{
						Vector2 position = base.position;
						int width = base.width;
						int height = base.height;
						int num3 = 76;
						float speedX = hitDirection;
						float speedY = -1f;
						int num4 = 0;
						int num5 = Dust.NewDust(position, width, height, num3, speedX, speedY, num4);
						Main.dust[num5].noGravity = true;
					}
				}
				else
				{
					for (int m = 0; m < 50; m++)
					{
						Vector2 position2 = base.position;
						int width2 = base.width;
						int height2 = base.height;
						int num6 = 76;
						float speedX2 = hitDirection;
						float speedY2 = -1f;
						int num7 = 0;
						int num8 = Dust.NewDust(position2, width2, height2, num6, speedX2, speedY2, num7);
						Main.dust[num8].noGravity = true;
						Main.dust[num8].scale *= 1.2f;
					}
				}
			}
			if (type == 141)
			{
				if (life > 0)
				{
					for (int n = 0; (double)n < dmg / (double)lifeMax * 100.0; n++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, hitDirection, -1f, alpha, new Color(210, 230, 140));
					}
				}
				else
				{
					for (int num9 = 0; num9 < 50; num9++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, 2 * hitDirection, -2f, alpha, new Color(210, 230, 140));
					}
				}
			}
			if (type == 112)
			{
				for (int num10 = 0; num10 < 20; num10++)
				{
					Vector2 position3 = new Vector2(base.position.X, base.position.Y + 2f);
					int width3 = base.width;
					int height3 = base.height;
					int num11 = 18;
					float speedX3 = 0f;
					float speedY3 = 0f;
					int num12 = 100;
					int num13 = Dust.NewDust(position3, width3, height3, num11, speedX3, speedY3, num12, default(Color), 2f);
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num13].scale *= 0.6f;
						continue;
					}
					Dust dust = Main.dust[num13];
					dust.velocity *= 1.4f;
					Main.dust[num13].noGravity = true;
				}
			}
			if (type == 81 || type == 121)
			{
				if (life > 0)
				{
					for (int num14 = 0; (double)num14 < dmg / (double)lifeMax * 100.0; num14++)
					{
						Dust.NewDust(base.position, base.width, base.height, 14, 0f, 0f, alpha, color);
					}
				}
				else
				{
					for (int num15 = 0; num15 < 50; num15++)
					{
						int num16 = Dust.NewDust(base.position, base.width, base.height, 14, hitDirection, 0f, alpha, color);
						Dust dust2 = Main.dust[num16];
						dust2.velocity *= 2f;
					}
					if (Main.netMode != 1)
					{
						if (type == 121)
						{
							int num17 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), 81);
							Main.npc[num17].SetDefaults("Slimer2");
							Main.npc[num17].velocity.X = base.velocity.X;
							Main.npc[num17].velocity.Y = base.velocity.Y;
							Gore.NewGore(base.position, base.velocity, 94, scale);
							if (Main.netMode == 2 && num17 < 200)
							{
								NetMessage.SendData(23, -1, -1, "", num17);
							}
						}
						else if (scale >= 1f)
						{
							int num18 = Main.rand.Next(2) + 2;
							for (int num19 = 0; num19 < num18; num19++)
							{
								int num20 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), 1);
								Main.npc[num20].SetDefaults("Slimeling");
								Main.npc[num20].velocity.X = base.velocity.X * 3f;
								Main.npc[num20].velocity.Y = base.velocity.Y;
								NPC nPC3 = Main.npc[num20];
								nPC3.velocity.X = nPC3.velocity.X + ((float)Main.rand.Next(-10, 10) * 0.1f + (float)(num19 * direction) * 0.3f);
								NPC nPC4 = Main.npc[num20];
								nPC4.velocity.Y = nPC4.velocity.Y - ((float)Main.rand.Next(0, 10) * 0.1f + (float)num19);
								Main.npc[num20].ai[1] = num19;
								if (Main.netMode == 2 && num20 < 200)
								{
									NetMessage.SendData(23, -1, -1, "", num20);
								}
							}
						}
					}
				}
			}
			if (type == 120 || type == 137 || type == 138)
			{
				if (life > 0)
				{
					for (int num21 = 0; (double)num21 < dmg / (double)lifeMax * 50.0; num21++)
					{
						Vector2 position4 = base.position;
						int width4 = base.width;
						int height4 = base.height;
						int num22 = 71;
						float speedX4 = 0f;
						float speedY4 = 0f;
						int num23 = 200;
						int num24 = Dust.NewDust(position4, width4, height4, num22, speedX4, speedY4, num23);
						Dust dust3 = Main.dust[num24];
						dust3.velocity *= 1.5f;
					}
				}
				else
				{
					for (int num25 = 0; num25 < 50; num25++)
					{
						Vector2 position5 = base.position;
						int width5 = base.width;
						int height5 = base.height;
						int num26 = 71;
						float speedX5 = hitDirection;
						float speedY5 = 0f;
						int num27 = 200;
						int num28 = Dust.NewDust(position5, width5, height5, num26, speedX5, speedY5, num27);
						Dust dust4 = Main.dust[num28];
						dust4.velocity *= 1.5f;
					}
				}
			}
			if (type == 122)
			{
				if (life > 0)
				{
					for (int num29 = 0; (double)num29 < dmg / (double)lifeMax * 50.0; num29++)
					{
						Vector2 position6 = base.position;
						int width6 = base.width;
						int height6 = base.height;
						int num30 = 72;
						float speedX6 = 0f;
						float speedY6 = 0f;
						int num31 = 200;
						int num32 = Dust.NewDust(position6, width6, height6, num30, speedX6, speedY6, num31);
						Dust dust5 = Main.dust[num32];
						dust5.velocity *= 1.5f;
					}
				}
				else
				{
					for (int num33 = 0; num33 < 50; num33++)
					{
						Vector2 position7 = base.position;
						int width7 = base.width;
						int height7 = base.height;
						int num34 = 72;
						float speedX7 = hitDirection;
						float speedY7 = 0f;
						int num35 = 200;
						int num36 = Dust.NewDust(position7, width7, height7, num34, speedX7, speedY7, num35);
						Dust dust6 = Main.dust[num36];
						dust6.velocity *= 1.5f;
					}
				}
			}
			if (type == 75)
			{
				if (life > 0)
				{
					for (int num37 = 0; (double)num37 < dmg / (double)lifeMax * 50.0; num37++)
					{
						Dust.NewDust(base.position, base.width, base.height, 55, 0f, 0f, 200, color);
					}
				}
				else
				{
					for (int num38 = 0; num38 < 50; num38++)
					{
						int num39 = Dust.NewDust(base.position, base.width, base.height, 55, hitDirection, 0f, 200, color);
						Dust dust7 = Main.dust[num39];
						dust7.velocity *= 2f;
					}
				}
			}
			if (type == 63 || type == 64 || type == 103)
			{
				Color newColor = new Color(50, 120, 255, 100);
				if (type == 64)
				{
					newColor = new Color(225, 70, 140, 100);
				}
				if (type == 103)
				{
					newColor = new Color(70, 225, 140, 100);
				}
				if (life > 0)
				{
					for (int num40 = 0; (double)num40 < dmg / (double)lifeMax * 50.0; num40++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, hitDirection, -1f, 0, newColor);
					}
				}
				else
				{
					for (int num41 = 0; num41 < 25; num41++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, 2 * hitDirection, -2f, 0, newColor);
					}
				}
			}
			else if (type != 59 && type != 60)
			{
				if (type == 50)
				{
					if (life > 0)
					{
						for (int num42 = 0; (double)num42 < dmg / (double)lifeMax * 300.0; num42++)
						{
							Dust.NewDust(base.position, base.width, base.height, 4, hitDirection, -1f, 175, new Color(0, 80, 255, 100));
						}
						return;
					}
					for (int num43 = 0; num43 < 200; num43++)
					{
						Dust.NewDust(base.position, base.width, base.height, 4, 2 * hitDirection, -2f, 175, new Color(0, 80, 255, 100));
					}
					if (Main.netMode == 1)
					{
						return;
					}
					int num44 = Main.rand.Next(4) + 4;
					for (int num45 = 0; num45 < num44; num45++)
					{
						int x = (int)(base.position.X + (float)Main.rand.Next(base.width - 32));
						int y = (int)(base.position.Y + (float)Main.rand.Next(base.height - 32));
						int num46 = NewNPC(x, y, 1);
						Main.npc[num46].SetDefaults(1);
						Main.npc[num46].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
						Main.npc[num46].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
						Main.npc[num46].ai[1] = Main.rand.Next(3);
						if (Main.netMode == 2 && num46 < 200)
						{
							NetMessage.SendData(23, -1, -1, "", num46);
						}
					}
				}
				else if (type == 49 || type == 51 || type == 93)
				{
					if (life > 0)
					{
						for (int num47 = 0; (double)num47 < dmg / (double)lifeMax * 30.0; num47++)
						{
							Vector2 position8 = base.position;
							int width8 = base.width;
							int height8 = base.height;
							int num48 = 5;
							float speedX8 = hitDirection;
							float speedY8 = -1f;
							int num49 = 0;
							Dust.NewDust(position8, width8, height8, num48, speedX8, speedY8, num49);
						}
						return;
					}
					for (int num50 = 0; num50 < 15; num50++)
					{
						Vector2 position9 = base.position;
						int width9 = base.width;
						int height9 = base.height;
						int num51 = 5;
						float speedX9 = 2 * hitDirection;
						float speedY9 = -2f;
						int num52 = 0;
						Dust.NewDust(position9, width9, height9, num51, speedX9, speedY9, num52);
					}
					if (type == 51)
					{
						Gore.NewGore(base.position, base.velocity, 83);
					}
					else if (type == 93)
					{
						Gore.NewGore(base.position, base.velocity, 107);
					}
					else
					{
						Gore.NewGore(base.position, base.velocity, 82);
					}
				}
				else if (type == 46 || type == 55 || type == 67 || type == 74 || type == 102)
				{
					if (life > 0)
					{
						for (int num53 = 0; (double)num53 < dmg / (double)lifeMax * 20.0; num53++)
						{
							Vector2 position10 = base.position;
							int width10 = base.width;
							int height10 = base.height;
							int num54 = 5;
							float speedX10 = hitDirection;
							float speedY10 = -1f;
							int num55 = 0;
							Dust.NewDust(position10, width10, height10, num54, speedX10, speedY10, num55);
						}
						return;
					}
					for (int num56 = 0; num56 < 10; num56++)
					{
						Vector2 position11 = base.position;
						int width11 = base.width;
						int height11 = base.height;
						int num57 = 5;
						float speedX11 = 2 * hitDirection;
						float speedY11 = -2f;
						int num58 = 0;
						Dust.NewDust(position11, width11, height11, num57, speedX11, speedY11, num58);
					}
					if (type == 46)
					{
						Gore.NewGore(base.position, base.velocity, 76);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 77);
					}
					else if (type == 67)
					{
						Gore.NewGore(base.position, base.velocity, 95);
						Gore.NewGore(base.position, base.velocity, 95);
						Gore.NewGore(base.position, base.velocity, 96);
					}
					else if (type == 74)
					{
						Gore.NewGore(base.position, base.velocity, 100);
					}
					else if (type == 102)
					{
						Gore.NewGore(base.position, base.velocity, 116);
					}
				}
				else if (type == 47 || type == 57 || type == 58)
				{
					if (life > 0)
					{
						for (int num59 = 0; (double)num59 < dmg / (double)lifeMax * 20.0; num59++)
						{
							Vector2 position12 = base.position;
							int width12 = base.width;
							int height12 = base.height;
							int num60 = 5;
							float speedX12 = hitDirection;
							float speedY12 = -1f;
							int num61 = 0;
							Dust.NewDust(position12, width12, height12, num60, speedX12, speedY12, num61);
						}
						return;
					}
					for (int num62 = 0; num62 < 10; num62++)
					{
						Vector2 position13 = base.position;
						int width13 = base.width;
						int height13 = base.height;
						int num63 = 5;
						float speedX13 = 2 * hitDirection;
						float speedY13 = -2f;
						int num64 = 0;
						Dust.NewDust(position13, width13, height13, num63, speedX13, speedY13, num64);
					}
					if (type == 57)
					{
						Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 84);
						return;
					}
					if (type == 58)
					{
						Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 85);
						return;
					}
					Gore.NewGore(base.position, base.velocity, 78);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 79);
				}
				else if (type == 2)
				{
					if (life > 0)
					{
						for (int num65 = 0; (double)num65 < dmg / (double)lifeMax * 100.0; num65++)
						{
							Vector2 position14 = base.position;
							int width14 = base.width;
							int height14 = base.height;
							int num66 = 5;
							float speedX14 = hitDirection;
							float speedY14 = -1f;
							int num67 = 0;
							Dust.NewDust(position14, width14, height14, num66, speedX14, speedY14, num67);
						}
						return;
					}
					for (int num68 = 0; num68 < 50; num68++)
					{
						Vector2 position15 = base.position;
						int width15 = base.width;
						int height15 = base.height;
						int num69 = 5;
						float speedX15 = 2 * hitDirection;
						float speedY15 = -2f;
						int num70 = 0;
						Dust.NewDust(position15, width15, height15, num69, speedX15, speedY15, num70);
					}
					Gore.NewGore(base.position, base.velocity, 1);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity, 2);
				}
				else if (type == 133)
				{
					if (life <= 0)
					{
						for (int num71 = 0; num71 < 50; num71++)
						{
							Vector2 position16 = base.position;
							int width16 = base.width;
							int height16 = base.height;
							int num72 = 5;
							float speedX16 = 2 * hitDirection;
							float speedY16 = -2f;
							int num73 = 0;
							Dust.NewDust(position16, width16, height16, num72, speedX16, speedY16, num73);
						}
						Gore.NewGore(base.position, base.velocity, 155);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 14f), base.velocity, 155);
						return;
					}
					for (int num74 = 0; (double)num74 < dmg / (double)lifeMax * 100.0; num74++)
					{
						Vector2 position17 = base.position;
						int width17 = base.width;
						int height17 = base.height;
						int num75 = 5;
						float speedX17 = hitDirection;
						float speedY17 = -1f;
						int num76 = 0;
						Dust.NewDust(position17, width17, height17, num75, speedX17, speedY17, num76);
					}
					if ((float)life < (float)lifeMax * 0.5f && localAI[0] == 0f)
					{
						localAI[0] = 1f;
						Gore.NewGore(base.position, base.velocity, 1);
					}
				}
				else if (type == 69)
				{
					if (life > 0)
					{
						for (int num77 = 0; (double)num77 < dmg / (double)lifeMax * 100.0; num77++)
						{
							Vector2 position18 = base.position;
							int width18 = base.width;
							int height18 = base.height;
							int num78 = 5;
							float speedX18 = hitDirection;
							float speedY18 = -1f;
							int num79 = 0;
							Dust.NewDust(position18, width18, height18, num78, speedX18, speedY18, num79);
						}
						return;
					}
					for (int num80 = 0; num80 < 50; num80++)
					{
						Vector2 position19 = base.position;
						int width19 = base.width;
						int height19 = base.height;
						int num81 = 5;
						float speedX19 = 2 * hitDirection;
						float speedY19 = -2f;
						int num82 = 0;
						Dust.NewDust(position19, width19, height19, num81, speedX19, speedY19, num82);
					}
					Gore.NewGore(base.position, base.velocity, 97);
					Gore.NewGore(base.position, base.velocity, 98);
				}
				else if (type == 61)
				{
					if (life > 0)
					{
						for (int num83 = 0; (double)num83 < dmg / (double)lifeMax * 100.0; num83++)
						{
							Vector2 position20 = base.position;
							int width20 = base.width;
							int height20 = base.height;
							int num84 = 5;
							float speedX20 = hitDirection;
							float speedY20 = -1f;
							int num85 = 0;
							Dust.NewDust(position20, width20, height20, num84, speedX20, speedY20, num85);
						}
						return;
					}
					for (int num86 = 0; num86 < 50; num86++)
					{
						Vector2 position21 = base.position;
						int width21 = base.width;
						int height21 = base.height;
						int num87 = 5;
						float speedX21 = 2 * hitDirection;
						float speedY21 = -2f;
						int num88 = 0;
						Dust.NewDust(position21, width21, height21, num87, speedX21, speedY21, num88);
					}
					Gore.NewGore(base.position, base.velocity, 86);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity, 87);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity, 88);
				}
				else if (type == 65)
				{
					if (life > 0)
					{
						for (int num89 = 0; (double)num89 < dmg / (double)lifeMax * 150.0; num89++)
						{
							Vector2 position22 = base.position;
							int width22 = base.width;
							int height22 = base.height;
							int num90 = 5;
							float speedX22 = hitDirection;
							float speedY22 = -1f;
							int num91 = 0;
							Dust.NewDust(position22, width22, height22, num90, speedX22, speedY22, num91);
						}
						return;
					}
					for (int num92 = 0; num92 < 75; num92++)
					{
						Vector2 position23 = base.position;
						int width23 = base.width;
						int height23 = base.height;
						int num93 = 5;
						float speedX23 = 2 * hitDirection;
						float speedY23 = -2f;
						int num94 = 0;
						Dust.NewDust(position23, width23, height23, num93, speedX23, speedY23, num94);
					}
					Gore.NewGore(base.position, base.velocity * 0.8f, 89);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity * 0.8f, 90);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity * 0.8f, 91);
					Gore.NewGore(new Vector2(base.position.X + 14f, base.position.Y), base.velocity * 0.8f, 92);
				}
				else if (type == 3 || type == 52 || type == 53 || type == 104 || type == 109 || type == 132)
				{
					if (life > 0)
					{
						for (int num95 = 0; (double)num95 < dmg / (double)lifeMax * 100.0; num95++)
						{
							Vector2 position24 = base.position;
							int width24 = base.width;
							int height24 = base.height;
							int num96 = 5;
							float speedX24 = hitDirection;
							float speedY24 = -1f;
							int num97 = 0;
							Dust.NewDust(position24, width24, height24, num96, speedX24, speedY24, num97);
						}
						return;
					}
					for (int num98 = 0; num98 < 50; num98++)
					{
						Vector2 position25 = base.position;
						int width25 = base.width;
						int height25 = base.height;
						int num99 = 5;
						float speedX25 = 2.5f * (float)hitDirection;
						float speedY25 = -2.5f;
						int num100 = 0;
						Dust.NewDust(position25, width25, height25, num99, speedX25, speedY25, num100);
					}
					if (type == 104)
					{
						Gore.NewGore(base.position, base.velocity, 117);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 118);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 118);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 119);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 119);
						return;
					}
					if (type == 109)
					{
						Gore.NewGore(base.position, base.velocity, 121);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 122);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 122);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 123);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 123);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 46f), base.velocity, 120);
						return;
					}
					if (type == 132)
					{
						Gore.NewGore(base.position, base.velocity, 154);
					}
					else
					{
						Gore.NewGore(base.position, base.velocity, 3);
					}
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 4);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 4);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 5);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 5);
				}
				else if (type == 83 || type == 84)
				{
					if (life > 0)
					{
						for (int num101 = 0; (double)num101 < dmg / (double)lifeMax * 50.0; num101++)
						{
							Vector2 position26 = base.position;
							int width26 = base.width;
							int height26 = base.height;
							int num102 = 31;
							float speedX26 = 0f;
							float speedY26 = 0f;
							int num103 = 0;
							int num104 = Dust.NewDust(position26, width26, height26, num102, speedX26, speedY26, num103, default(Color), 1.5f);
							Main.dust[num104].noGravity = true;
						}
						return;
					}
					for (int num105 = 0; num105 < 20; num105++)
					{
						Vector2 position27 = base.position;
						int width27 = base.width;
						int height27 = base.height;
						int num106 = 31;
						float speedX27 = 0f;
						float speedY27 = 0f;
						int num107 = 0;
						int num108 = Dust.NewDust(position27, width27, height27, num106, speedX27, speedY27, num107, default(Color), 1.5f);
						Dust dust8 = Main.dust[num108];
						dust8.velocity *= 2f;
						Main.dust[num108].noGravity = true;
					}
					int num109 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, scale);
					Gore gore = Main.gore[num109];
					gore.velocity *= 0.5f;
					num109 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, scale);
					Gore gore2 = Main.gore[num109];
					gore2.velocity *= 0.5f;
					num109 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3)), 61, scale);
					Gore gore3 = Main.gore[num109];
					gore3.velocity *= 0.5f;
				}
				else if (type == 4 || type == 126 || type == 125)
				{
					if (life > 0)
					{
						for (int num110 = 0; (double)num110 < dmg / (double)lifeMax * 100.0; num110++)
						{
							Vector2 position28 = base.position;
							int width28 = base.width;
							int height28 = base.height;
							int num111 = 5;
							float speedX28 = hitDirection;
							float speedY28 = -1f;
							int num112 = 0;
							Dust.NewDust(position28, width28, height28, num111, speedX28, speedY28, num112);
						}
						return;
					}
					for (int num113 = 0; num113 < 150; num113++)
					{
						Vector2 position29 = base.position;
						int width29 = base.width;
						int height29 = base.height;
						int num114 = 5;
						float speedX29 = 2 * hitDirection;
						float speedY29 = -2f;
						int num115 = 0;
						Dust.NewDust(position29, width29, height29, num114, speedX29, speedY29, num115);
					}
					for (int num116 = 0; num116 < 2; num116++)
					{
						Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 2);
						Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
						Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 9);
						if (type == 4)
						{
							Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 10);
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						}
						else if (type == 125)
						{
							Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 146);
						}
						else if (type == 126)
						{
							Gore.NewGore(base.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 145);
						}
					}
					if (type == 125 || type == 126)
					{
						for (int num117 = 0; num117 < 10; num117++)
						{
							Vector2 position30 = new Vector2(base.position.X, base.position.Y);
							int width30 = base.width;
							int height30 = base.height;
							int num118 = 31;
							float speedX30 = 0f;
							float speedY30 = 0f;
							int num119 = 100;
							int num120 = Dust.NewDust(position30, width30, height30, num118, speedX30, speedY30, num119, default(Color), 1.5f);
							Dust dust9 = Main.dust[num120];
							dust9.velocity *= 1.4f;
						}
						for (int num121 = 0; num121 < 5; num121++)
						{
							Vector2 position31 = new Vector2(base.position.X, base.position.Y);
							int width31 = base.width;
							int height31 = base.height;
							int num122 = 6;
							float speedX31 = 0f;
							float speedY31 = 0f;
							int num123 = 100;
							int num124 = Dust.NewDust(position31, width31, height31, num122, speedX31, speedY31, num123, default(Color), 2.5f);
							Main.dust[num124].noGravity = true;
							Dust dust10 = Main.dust[num124];
							dust10.velocity *= 5f;
							Vector2 position32 = new Vector2(base.position.X, base.position.Y);
							int width32 = base.width;
							int height32 = base.height;
							int num125 = 6;
							float speedX32 = 0f;
							float speedY32 = 0f;
							int num126 = 100;
							num124 = Dust.NewDust(position32, width32, height32, num125, speedX32, speedY32, num126, default(Color), 1.5f);
							Dust dust11 = Main.dust[num124];
							dust11.velocity *= 3f;
						}
						Vector2 position33 = new Vector2(base.position.X, base.position.Y);
						int num127 = Gore.NewGore(position33, default(Vector2), Main.rand.Next(61, 64));
						Gore gore4 = Main.gore[num127];
						gore4.velocity *= 0.4f;
						Gore gore5 = Main.gore[num127];
						gore5.velocity.X = gore5.velocity.X + 1f;
						Gore gore6 = Main.gore[num127];
						gore6.velocity.Y = gore6.velocity.Y + 1f;
						Vector2 position34 = new Vector2(base.position.X, base.position.Y);
						num127 = Gore.NewGore(position34, default(Vector2), Main.rand.Next(61, 64));
						Gore gore7 = Main.gore[num127];
						gore7.velocity *= 0.4f;
						Gore gore8 = Main.gore[num127];
						gore8.velocity.X = gore8.velocity.X - 1f;
						Gore gore9 = Main.gore[num127];
						gore9.velocity.Y = gore9.velocity.Y + 1f;
						Vector2 position35 = new Vector2(base.position.X, base.position.Y);
						num127 = Gore.NewGore(position35, default(Vector2), Main.rand.Next(61, 64));
						Gore gore10 = Main.gore[num127];
						gore10.velocity *= 0.4f;
						Gore gore11 = Main.gore[num127];
						gore11.velocity.X = gore11.velocity.X + 1f;
						Gore gore12 = Main.gore[num127];
						gore12.velocity.Y = gore12.velocity.Y - 1f;
						Vector2 position36 = new Vector2(base.position.X, base.position.Y);
						num127 = Gore.NewGore(position36, default(Vector2), Main.rand.Next(61, 64));
						Gore gore13 = Main.gore[num127];
						gore13.velocity *= 0.4f;
						Gore gore14 = Main.gore[num127];
						gore14.velocity.X = gore14.velocity.X - 1f;
						Gore gore15 = Main.gore[num127];
						gore15.velocity.Y = gore15.velocity.Y - 1f;
					}
				}
				else if (type == 5)
				{
					if (life > 0)
					{
						for (int num128 = 0; (double)num128 < dmg / (double)lifeMax * 50.0; num128++)
						{
							Vector2 position37 = base.position;
							int width33 = base.width;
							int height33 = base.height;
							int num129 = 5;
							float speedX33 = hitDirection;
							float speedY33 = -1f;
							int num130 = 0;
							Dust.NewDust(position37, width33, height33, num129, speedX33, speedY33, num130);
						}
						return;
					}
					for (int num131 = 0; num131 < 20; num131++)
					{
						Vector2 position38 = base.position;
						int width34 = base.width;
						int height34 = base.height;
						int num132 = 5;
						float speedX34 = 2 * hitDirection;
						float speedY34 = -2f;
						int num133 = 0;
						Dust.NewDust(position38, width34, height34, num132, speedX34, speedY34, num133);
					}
					Gore.NewGore(base.position, base.velocity, 6);
					Gore.NewGore(base.position, base.velocity, 7);
				}
				else if (type == 113 || type == 114)
				{
					if (life > 0)
					{
						for (int num134 = 0; num134 < 20; num134++)
						{
							Vector2 position39 = base.position;
							int width35 = base.width;
							int height35 = base.height;
							int num135 = 5;
							float speedX35 = hitDirection;
							float speedY35 = -1f;
							int num136 = 0;
							Dust.NewDust(position39, width35, height35, num135, speedX35, speedY35, num136);
						}
						return;
					}
					for (int num137 = 0; num137 < 50; num137++)
					{
						Vector2 position40 = base.position;
						int width36 = base.width;
						int height36 = base.height;
						int num138 = 5;
						float speedX36 = 2 * hitDirection;
						float speedY36 = -1f;
						int num139 = 0;
						Dust.NewDust(position40, width36, height36, num138, speedX36, speedY36, num139);
					}
					if (type == 114)
					{
						Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 137, scale);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2)), base.velocity, 139, scale);
						Gore.NewGore(new Vector2(base.position.X + (float)(base.width / 2), base.position.Y), base.velocity, 139, scale);
						Gore.NewGore(new Vector2(base.position.X + (float)(base.width / 2), base.position.Y + (float)(base.height / 2)), base.velocity, 137, scale);
						return;
					}
					Gore.NewGore(new Vector2(base.position.X, base.position.Y), base.velocity, 137, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2)), base.velocity, 138, scale);
					Gore.NewGore(new Vector2(base.position.X + (float)(base.width / 2), base.position.Y), base.velocity, 138, scale);
					Gore.NewGore(new Vector2(base.position.X + (float)(base.width / 2), base.position.Y + (float)(base.height / 2)), base.velocity, 137, scale);
					if (!(Main.player[Main.myPlayer].position.Y / 16f > (float)(Main.hellLayer - 50.0)))
					{
						return;
					}
					int num140 = (int)Main.screenPosition.Y;
					int num141 = num140 + Main.screenWidth;
					int num142 = (int)base.position.X;
					if (direction > 0)
					{
						num142 -= 80;
					}
					int num143 = num142 + 140;
					int num144 = num142;
					for (int num145 = num140; num145 < num141; num145 += 50)
					{
						for (; num144 < num143; num144 += 46)
						{
							for (int num146 = 0; num146 < 5; num146++)
							{
								Vector2 position41 = new Vector2(num144, num145);
								int width37 = 32;
								int height37 = 32;
								int num147 = 5;
								float speedX37 = (float)Main.rand.Next(-60, 61) * 0.1f;
								float speedY37 = (float)Main.rand.Next(-60, 61) * 0.1f;
								int num148 = 0;
								Dust.NewDust(position41, width37, height37, num147, speedX37, speedY37, num148);
							}
							Gore.NewGore(Velocity: new Vector2((float)Main.rand.Next(-80, 81) * 0.1f, (float)Main.rand.Next(-60, 21) * 0.1f), Position: new Vector2(num144, num145), Type: Main.rand.Next(140, 143));
						}
						num144 = num142;
					}
				}
				else if (type == 115 || type == 116)
				{
					if (life > 0)
					{
						for (int num149 = 0; num149 < 5; num149++)
						{
							Vector2 position42 = base.position;
							int width38 = base.width;
							int height38 = base.height;
							int num150 = 5;
							float speedX38 = hitDirection;
							float speedY38 = -1f;
							int num151 = 0;
							Dust.NewDust(position42, width38, height38, num150, speedX38, speedY38, num151);
						}
						return;
					}
					if (type == 115 && Main.netMode != 1)
					{
						NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), 116);
						for (int num152 = 0; num152 < 10; num152++)
						{
							Vector2 position43 = base.position;
							int width39 = base.width;
							int height39 = base.height;
							int num153 = 5;
							float speedX39 = hitDirection;
							float speedY39 = -1f;
							int num154 = 0;
							Dust.NewDust(position43, width39, height39, num153, speedX39, speedY39, num154);
						}
						return;
					}
					for (int num155 = 0; num155 < 20; num155++)
					{
						Vector2 position44 = base.position;
						int width40 = base.width;
						int height40 = base.height;
						int num156 = 5;
						float speedX40 = hitDirection;
						float speedY40 = -1f;
						int num157 = 0;
						Dust.NewDust(position44, width40, height40, num156, speedX40, speedY40, num157);
					}
					Gore.NewGore(base.position, base.velocity, 132, scale);
					Gore.NewGore(base.position, base.velocity, 133, scale);
				}
				else if (type >= 117 && type <= 119)
				{
					if (life > 0)
					{
						for (int num158 = 0; num158 < 5; num158++)
						{
							Vector2 position45 = base.position;
							int width41 = base.width;
							int height41 = base.height;
							int num159 = 5;
							float speedX41 = hitDirection;
							float speedY41 = -1f;
							int num160 = 0;
							Dust.NewDust(position45, width41, height41, num159, speedX41, speedY41, num160);
						}
						return;
					}
					for (int num161 = 0; num161 < 10; num161++)
					{
						Vector2 position46 = base.position;
						int width42 = base.width;
						int height42 = base.height;
						int num162 = 5;
						float speedX42 = hitDirection;
						float speedY42 = -1f;
						int num163 = 0;
						Dust.NewDust(position46, width42, height42, num162, speedX42, speedY42, num163);
					}
					Gore.NewGore(base.position, base.velocity, 134 + type - 117, scale);
				}
				else if (type == 6 || type == 94)
				{
					if (life > 0)
					{
						for (int num164 = 0; (double)num164 < dmg / (double)lifeMax * 100.0; num164++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num165 = 0; num165 < 50; num165++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					if (type == 94)
					{
						int num166 = Gore.NewGore(base.position, base.velocity, 108, scale);
						num166 = Gore.NewGore(base.position, base.velocity, 108, scale);
						num166 = Gore.NewGore(base.position, base.velocity, 109, scale);
						num166 = Gore.NewGore(base.position, base.velocity, 110, scale);
					}
					else
					{
						int num166 = Gore.NewGore(base.position, base.velocity, 14, scale);
						Main.gore[num166].alpha = alpha;
						num166 = Gore.NewGore(base.position, base.velocity, 15, scale);
						Main.gore[num166].alpha = alpha;
					}
				}
				else if (type == 101)
				{
					if (life > 0)
					{
						for (int num167 = 0; (double)num167 < dmg / (double)lifeMax * 100.0; num167++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num168 = 0; num168 < 50; num168++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					Gore.NewGore(base.position, base.velocity, 110, scale);
					Gore.NewGore(base.position, base.velocity, 114, scale);
					Gore.NewGore(base.position, base.velocity, 114, scale);
					Gore.NewGore(base.position, base.velocity, 115, scale);
				}
				else if (type == 7 || type == 8 || type == 9)
				{
					if (life > 0)
					{
						for (int num169 = 0; (double)num169 < dmg / (double)lifeMax * 100.0; num169++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num170 = 0; num170 < 50; num170++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					int num171 = Gore.NewGore(base.position, base.velocity, type - 7 + 18);
					Main.gore[num171].alpha = alpha;
				}
				else if (type == 98 || type == 99 || type == 100)
				{
					if (life > 0)
					{
						for (int num172 = 0; (double)num172 < dmg / (double)lifeMax * 100.0; num172++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num173 = 0; num173 < 50; num173++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					int num174 = Gore.NewGore(base.position, base.velocity, 110);
					Main.gore[num174].alpha = alpha;
				}
				else if (type == 10 || type == 11 || type == 12)
				{
					if (life > 0)
					{
						for (int num175 = 0; (double)num175 < dmg / (double)lifeMax * 50.0; num175++)
						{
							Vector2 position47 = base.position;
							int width43 = base.width;
							int height43 = base.height;
							int num176 = 5;
							float speedX43 = hitDirection;
							float speedY43 = -1f;
							int num177 = 0;
							Dust.NewDust(position47, width43, height43, num176, speedX43, speedY43, num177);
						}
						return;
					}
					for (int num178 = 0; num178 < 10; num178++)
					{
						Vector2 position48 = base.position;
						int width44 = base.width;
						int height44 = base.height;
						int num179 = 5;
						float speedX44 = 2.5f * (float)hitDirection;
						float speedY44 = -2.5f;
						int num180 = 0;
						Dust.NewDust(position48, width44, height44, num179, speedX44, speedY44, num180);
					}
					Gore.NewGore(base.position, base.velocity, type - 7 + 18);
				}
				else if (type == 95 || type == 96 || type == 97)
				{
					if (life > 0)
					{
						for (int num181 = 0; (double)num181 < dmg / (double)lifeMax * 50.0; num181++)
						{
							Vector2 position49 = base.position;
							int width45 = base.width;
							int height45 = base.height;
							int num182 = 5;
							float speedX45 = hitDirection;
							float speedY45 = -1f;
							int num183 = 0;
							Dust.NewDust(position49, width45, height45, num182, speedX45, speedY45, num183);
						}
						return;
					}
					for (int num184 = 0; num184 < 10; num184++)
					{
						Vector2 position50 = base.position;
						int width46 = base.width;
						int height46 = base.height;
						int num185 = 5;
						float speedX46 = 2.5f * (float)hitDirection;
						float speedY46 = -2.5f;
						int num186 = 0;
						Dust.NewDust(position50, width46, height46, num185, speedX46, speedY46, num186);
					}
					Gore.NewGore(base.position, base.velocity, type - 95 + 111);
				}
				else if (type == 13 || type == 14 || type == 15)
				{
					if (life > 0)
					{
						for (int num187 = 0; (double)num187 < dmg / (double)lifeMax * 100.0; num187++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num188 = 0; num188 < 50; num188++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					if (type == 13)
					{
						Gore.NewGore(base.position, base.velocity, 24);
						Gore.NewGore(base.position, base.velocity, 25);
					}
					else if (type == 14)
					{
						Gore.NewGore(base.position, base.velocity, 26);
						Gore.NewGore(base.position, base.velocity, 27);
					}
					else
					{
						Gore.NewGore(base.position, base.velocity, 28);
						Gore.NewGore(base.position, base.velocity, 29);
					}
				}
				else if (type == 17)
				{
					if (life > 0)
					{
						for (int num189 = 0; (double)num189 < dmg / (double)lifeMax * 100.0; num189++)
						{
							Vector2 position51 = base.position;
							int width47 = base.width;
							int height47 = base.height;
							int num190 = 5;
							float speedX47 = hitDirection;
							float speedY47 = -1f;
							int num191 = 0;
							Dust.NewDust(position51, width47, height47, num190, speedX47, speedY47, num191);
						}
						return;
					}
					for (int num192 = 0; num192 < 50; num192++)
					{
						Vector2 position52 = base.position;
						int width48 = base.width;
						int height48 = base.height;
						int num193 = 5;
						float speedX48 = 2.5f * (float)hitDirection;
						float speedY48 = -2.5f;
						int num194 = 0;
						Dust.NewDust(position52, width48, height48, num193, speedX48, speedY48, num194);
					}
					Gore.NewGore(base.position, base.velocity, 30);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 31);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 31);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 32);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 32);
				}
				else if (type == 86)
				{
					if (life > 0)
					{
						for (int num195 = 0; (double)num195 < dmg / (double)lifeMax * 100.0; num195++)
						{
							Vector2 position53 = base.position;
							int width49 = base.width;
							int height49 = base.height;
							int num196 = 5;
							float speedX49 = hitDirection;
							float speedY49 = -1f;
							int num197 = 0;
							Dust.NewDust(position53, width49, height49, num196, speedX49, speedY49, num197);
						}
						return;
					}
					for (int num198 = 0; num198 < 50; num198++)
					{
						Vector2 position54 = base.position;
						int width50 = base.width;
						int height50 = base.height;
						int num199 = 5;
						float speedX50 = 2.5f * (float)hitDirection;
						float speedY50 = -2.5f;
						int num200 = 0;
						Dust.NewDust(position54, width50, height50, num199, speedX50, speedY50, num200);
					}
					Gore.NewGore(base.position, base.velocity, 101);
					Gore.NewGore(base.position, base.velocity, 102);
					Gore.NewGore(base.position, base.velocity, 103);
					Gore.NewGore(base.position, base.velocity, 103);
					Gore.NewGore(base.position, base.velocity, 104);
					Gore.NewGore(base.position, base.velocity, 104);
					Gore.NewGore(base.position, base.velocity, 105);
				}
				else if (type >= 105 && type <= 108)
				{
					if (life > 0)
					{
						for (int num201 = 0; (double)num201 < dmg / (double)lifeMax * 100.0; num201++)
						{
							Vector2 position55 = base.position;
							int width51 = base.width;
							int height51 = base.height;
							int num202 = 5;
							float speedX51 = hitDirection;
							float speedY51 = -1f;
							int num203 = 0;
							Dust.NewDust(position55, width51, height51, num202, speedX51, speedY51, num203);
						}
						return;
					}
					for (int num204 = 0; num204 < 50; num204++)
					{
						Vector2 position56 = base.position;
						int width52 = base.width;
						int height52 = base.height;
						int num205 = 5;
						float speedX52 = 2.5f * (float)hitDirection;
						float speedY52 = -2.5f;
						int num206 = 0;
						Dust.NewDust(position56, width52, height52, num205, speedX52, speedY52, num206);
					}
					if (type == 105 || type == 107)
					{
						Gore.NewGore(base.position, base.velocity, 124);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 125);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 125);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 126);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 126);
					}
					else
					{
						Gore.NewGore(base.position, base.velocity, 127);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 128);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 128);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 129);
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 129);
					}
				}
				else if (type == 123 || type == 124)
				{
					if (life > 0)
					{
						for (int num207 = 0; (double)num207 < dmg / (double)lifeMax * 100.0; num207++)
						{
							Vector2 position57 = base.position;
							int width53 = base.width;
							int height53 = base.height;
							int num208 = 5;
							float speedX53 = hitDirection;
							float speedY53 = -1f;
							int num209 = 0;
							Dust.NewDust(position57, width53, height53, num208, speedX53, speedY53, num209);
						}
						return;
					}
					for (int num210 = 0; num210 < 50; num210++)
					{
						Vector2 position58 = base.position;
						int width54 = base.width;
						int height54 = base.height;
						int num211 = 5;
						float speedX54 = 2.5f * (float)hitDirection;
						float speedY54 = -2.5f;
						int num212 = 0;
						Dust.NewDust(position58, width54, height54, num211, speedX54, speedY54, num212);
					}
					Gore.NewGore(base.position, base.velocity, 151);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 152);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 152);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 153);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 153);
				}
				else if (type == 22)
				{
					if (life > 0)
					{
						for (int num213 = 0; (double)num213 < dmg / (double)lifeMax * 100.0; num213++)
						{
							Vector2 position59 = base.position;
							int width55 = base.width;
							int height55 = base.height;
							int num214 = 5;
							float speedX55 = hitDirection;
							float speedY55 = -1f;
							int num215 = 0;
							Dust.NewDust(position59, width55, height55, num214, speedX55, speedY55, num215);
						}
						return;
					}
					for (int num216 = 0; num216 < 50; num216++)
					{
						Vector2 position60 = base.position;
						int width56 = base.width;
						int height56 = base.height;
						int num217 = 5;
						float speedX56 = 2.5f * (float)hitDirection;
						float speedY56 = -2.5f;
						int num218 = 0;
						Dust.NewDust(position60, width56, height56, num217, speedX56, speedY56, num218);
					}
					Gore.NewGore(base.position, base.velocity, 73);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 74);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 74);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 75);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 75);
				}
				else if (type == 142)
				{
					if (life > 0)
					{
						for (int num219 = 0; (double)num219 < dmg / (double)lifeMax * 100.0; num219++)
						{
							Vector2 position61 = base.position;
							int width57 = base.width;
							int height57 = base.height;
							int num220 = 5;
							float speedX57 = hitDirection;
							float speedY57 = -1f;
							int num221 = 0;
							Dust.NewDust(position61, width57, height57, num220, speedX57, speedY57, num221);
						}
						return;
					}
					for (int num222 = 0; num222 < 50; num222++)
					{
						Vector2 position62 = base.position;
						int width58 = base.width;
						int height58 = base.height;
						int num223 = 5;
						float speedX58 = 2.5f * (float)hitDirection;
						float speedY58 = -2.5f;
						int num224 = 0;
						Dust.NewDust(position62, width58, height58, num223, speedX58, speedY58, num224);
					}
					Gore.NewGore(base.position, base.velocity, 157);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 158);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 158);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 159);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 159);
				}
				else if (type == 37 || type == 54)
				{
					if (life > 0)
					{
						for (int num225 = 0; (double)num225 < dmg / (double)lifeMax * 100.0; num225++)
						{
							Vector2 position63 = base.position;
							int width59 = base.width;
							int height59 = base.height;
							int num226 = 5;
							float speedX59 = hitDirection;
							float speedY59 = -1f;
							int num227 = 0;
							Dust.NewDust(position63, width59, height59, num226, speedX59, speedY59, num227);
						}
						return;
					}
					for (int num228 = 0; num228 < 50; num228++)
					{
						Vector2 position64 = base.position;
						int width60 = base.width;
						int height60 = base.height;
						int num229 = 5;
						float speedX60 = 2.5f * (float)hitDirection;
						float speedY60 = -2.5f;
						int num230 = 0;
						Dust.NewDust(position64, width60, height60, num229, speedX60, speedY60, num230);
					}
					Gore.NewGore(base.position, base.velocity, 58);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 59);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 59);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 60);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 60);
				}
				else if (type == 18)
				{
					if (life > 0)
					{
						for (int num231 = 0; (double)num231 < dmg / (double)lifeMax * 100.0; num231++)
						{
							Vector2 position65 = base.position;
							int width61 = base.width;
							int height61 = base.height;
							int num232 = 5;
							float speedX61 = hitDirection;
							float speedY61 = -1f;
							int num233 = 0;
							Dust.NewDust(position65, width61, height61, num232, speedX61, speedY61, num233);
						}
						return;
					}
					for (int num234 = 0; num234 < 50; num234++)
					{
						Vector2 position66 = base.position;
						int width62 = base.width;
						int height62 = base.height;
						int num235 = 5;
						float speedX62 = 2.5f * (float)hitDirection;
						float speedY62 = -2.5f;
						int num236 = 0;
						Dust.NewDust(position66, width62, height62, num235, speedX62, speedY62, num236);
					}
					Gore.NewGore(base.position, base.velocity, 33);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 34);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 34);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 35);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 35);
				}
				else if (type == 19)
				{
					if (life > 0)
					{
						for (int num237 = 0; (double)num237 < dmg / (double)lifeMax * 100.0; num237++)
						{
							Vector2 position67 = base.position;
							int width63 = base.width;
							int height63 = base.height;
							int num238 = 5;
							float speedX63 = hitDirection;
							float speedY63 = -1f;
							int num239 = 0;
							Dust.NewDust(position67, width63, height63, num238, speedX63, speedY63, num239);
						}
						return;
					}
					for (int num240 = 0; num240 < 50; num240++)
					{
						Vector2 position68 = base.position;
						int width64 = base.width;
						int height64 = base.height;
						int num241 = 5;
						float speedX64 = 2.5f * (float)hitDirection;
						float speedY64 = -2.5f;
						int num242 = 0;
						Dust.NewDust(position68, width64, height64, num241, speedX64, speedY64, num242);
					}
					Gore.NewGore(base.position, base.velocity, 36);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 37);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 37);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 38);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 38);
				}
				else if (type == 38)
				{
					if (life > 0)
					{
						for (int num243 = 0; (double)num243 < dmg / (double)lifeMax * 100.0; num243++)
						{
							Vector2 position69 = base.position;
							int width65 = base.width;
							int height65 = base.height;
							int num244 = 5;
							float speedX65 = hitDirection;
							float speedY65 = -1f;
							int num245 = 0;
							Dust.NewDust(position69, width65, height65, num244, speedX65, speedY65, num245);
						}
						return;
					}
					for (int num246 = 0; num246 < 50; num246++)
					{
						Vector2 position70 = base.position;
						int width66 = base.width;
						int height66 = base.height;
						int num247 = 5;
						float speedX66 = 2.5f * (float)hitDirection;
						float speedY66 = -2.5f;
						int num248 = 0;
						Dust.NewDust(position70, width66, height66, num247, speedX66, speedY66, num248);
					}
					Gore.NewGore(base.position, base.velocity, 64);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 65);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 65);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 66);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 66);
				}
				else if (type == 20)
				{
					if (life > 0)
					{
						for (int num249 = 0; (double)num249 < dmg / (double)lifeMax * 100.0; num249++)
						{
							Vector2 position71 = base.position;
							int width67 = base.width;
							int height67 = base.height;
							int num250 = 5;
							float speedX67 = hitDirection;
							float speedY67 = -1f;
							int num251 = 0;
							Dust.NewDust(position71, width67, height67, num250, speedX67, speedY67, num251);
						}
						return;
					}
					for (int num252 = 0; num252 < 50; num252++)
					{
						Vector2 position72 = base.position;
						int width68 = base.width;
						int height68 = base.height;
						int num253 = 5;
						float speedX68 = 2.5f * (float)hitDirection;
						float speedY68 = -2.5f;
						int num254 = 0;
						Dust.NewDust(position72, width68, height68, num253, speedX68, speedY68, num254);
					}
					Gore.NewGore(base.position, base.velocity, 39);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 40);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 40);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 41);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 41);
				}
				else if (type == 21 || type == 31 || type == 32 || type == 44 || type == 45 || type == 77 || type == 110)
				{
					if (life > 0)
					{
						for (int num255 = 0; (double)num255 < dmg / (double)lifeMax * 50.0; num255++)
						{
							Vector2 position73 = base.position;
							int width69 = base.width;
							int height69 = base.height;
							int num256 = 26;
							float speedX69 = hitDirection;
							float speedY69 = -1f;
							int num257 = 0;
							Dust.NewDust(position73, width69, height69, num256, speedX69, speedY69, num257);
						}
						return;
					}
					for (int num258 = 0; num258 < 20; num258++)
					{
						Vector2 position74 = base.position;
						int width70 = base.width;
						int height70 = base.height;
						int num259 = 26;
						float speedX70 = 2.5f * (float)hitDirection;
						float speedY70 = -2.5f;
						int num260 = 0;
						Dust.NewDust(position74, width70, height70, num259, speedX70, speedY70, num260);
					}
					Gore.NewGore(base.position, base.velocity, 42, scale);
					if (type == 77)
					{
						Gore.NewGore(base.position, base.velocity, 106, scale);
					}
					if (type == 110)
					{
						Gore.NewGore(base.position, base.velocity, 130, scale);
					}
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 43, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 43, scale);
					if (type == 110)
					{
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 131, scale);
					}
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 44, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 44, scale);
				}
				else if (type == 85)
				{
					int num261 = 7;
					if (ai[3] == 2f)
					{
						num261 = 10;
					}
					if (ai[3] == 3f)
					{
						num261 = 37;
					}
					if (life > 0)
					{
						for (int num262 = 0; (double)num262 < dmg / (double)lifeMax * 50.0; num262++)
						{
							Vector2 position75 = base.position;
							int width71 = base.width;
							int height71 = base.height;
							int num263 = num261;
							float speedX71 = 0f;
							float speedY71 = 0f;
							int num264 = 0;
							Dust.NewDust(position75, width71, height71, num263, speedX71, speedY71, num264);
						}
						return;
					}
					for (int num265 = 0; num265 < 20; num265++)
					{
						Vector2 position76 = base.position;
						int width72 = base.width;
						int height72 = base.height;
						int num266 = num261;
						float speedX72 = 0f;
						float speedY72 = 0f;
						int num267 = 0;
						Dust.NewDust(position76, width72, height72, num266, speedX72, speedY72, num267);
					}
					int num268 = Gore.NewGore(new Vector2(base.position.X, base.position.Y - 10f), new Vector2(hitDirection, 0f), 61, scale);
					Gore gore16 = Main.gore[num268];
					gore16.velocity *= 0.3f;
					num268 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(hitDirection, 0f), 62, scale);
					Gore gore17 = Main.gore[num268];
					gore17.velocity *= 0.3f;
					num268 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)base.height - 10f), new Vector2(hitDirection, 0f), 63, scale);
					Gore gore18 = Main.gore[num268];
					gore18.velocity *= 0.3f;
				}
				else if (type >= 87 && type <= 92)
				{
					if (life > 0)
					{
						for (int num269 = 0; (double)num269 < dmg / (double)lifeMax * 50.0; num269++)
						{
							Vector2 position77 = base.position;
							int width73 = base.width;
							int height73 = base.height;
							int num270 = 16;
							float speedX73 = 0f;
							float speedY73 = 0f;
							int num271 = 0;
							int num272 = Dust.NewDust(position77, width73, height73, num270, speedX73, speedY73, num271, default(Color), 1.5f);
							Dust dust12 = Main.dust[num272];
							dust12.velocity *= 1.5f;
							Main.dust[num272].noGravity = true;
						}
						return;
					}
					for (int num273 = 0; num273 < 10; num273++)
					{
						Vector2 position78 = base.position;
						int width74 = base.width;
						int height74 = base.height;
						int num274 = 16;
						float speedX74 = 0f;
						float speedY74 = 0f;
						int num275 = 0;
						int num276 = Dust.NewDust(position78, width74, height74, num274, speedX74, speedY74, num275, default(Color), 1.5f);
						Dust dust13 = Main.dust[num276];
						dust13.velocity *= 2f;
						Main.dust[num276].noGravity = true;
					}
					int num277 = Main.rand.Next(1, 4);
					for (int num278 = 0; num278 < num277; num278++)
					{
						int num279 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(hitDirection, 0f), Main.rand.Next(11, 14), scale);
						Gore gore19 = Main.gore[num279];
						gore19.velocity *= 0.8f;
					}
				}
				else if (type == 78 || type == 79 || type == 80)
				{
					if (life > 0)
					{
						for (int num280 = 0; (double)num280 < dmg / (double)lifeMax * 50.0; num280++)
						{
							Vector2 position79 = base.position;
							int width75 = base.width;
							int height75 = base.height;
							int num281 = 31;
							float speedX75 = 0f;
							float speedY75 = 0f;
							int num282 = 0;
							int num283 = Dust.NewDust(position79, width75, height75, num281, speedX75, speedY75, num282, default(Color), 1.5f);
							Dust dust14 = Main.dust[num283];
							dust14.velocity *= 2f;
							Main.dust[num283].noGravity = true;
						}
						return;
					}
					for (int num284 = 0; num284 < 20; num284++)
					{
						Vector2 position80 = base.position;
						int width76 = base.width;
						int height76 = base.height;
						int num285 = 31;
						float speedX76 = 0f;
						float speedY76 = 0f;
						int num286 = 0;
						int num287 = Dust.NewDust(position80, width76, height76, num285, speedX76, speedY76, num286, default(Color), 1.5f);
						Dust dust15 = Main.dust[num287];
						dust15.velocity *= 2f;
						Main.dust[num287].noGravity = true;
					}
					int num288 = Gore.NewGore(new Vector2(base.position.X, base.position.Y - 10f), new Vector2(hitDirection, 0f), 61, scale);
					Gore gore20 = Main.gore[num288];
					gore20.velocity *= 0.3f;
					num288 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 10f), new Vector2(hitDirection, 0f), 62, scale);
					Gore gore21 = Main.gore[num288];
					gore21.velocity *= 0.3f;
					num288 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)base.height - 10f), new Vector2(hitDirection, 0f), 63, scale);
					Gore gore22 = Main.gore[num288];
					gore22.velocity *= 0.3f;
				}
				else if (type == 82)
				{
					if (life > 0)
					{
						for (int num289 = 0; (double)num289 < dmg / (double)lifeMax * 50.0; num289++)
						{
							Vector2 position81 = base.position;
							int width77 = base.width;
							int height77 = base.height;
							int num290 = 54;
							float speedX77 = 0f;
							float speedY77 = 0f;
							int num291 = 50;
							int num292 = Dust.NewDust(position81, width77, height77, num290, speedX77, speedY77, num291, default(Color), 1.5f);
							Dust dust16 = Main.dust[num292];
							dust16.velocity *= 2f;
							Main.dust[num292].noGravity = true;
						}
						return;
					}
					for (int num293 = 0; num293 < 20; num293++)
					{
						Vector2 position82 = base.position;
						int width78 = base.width;
						int height78 = base.height;
						int num294 = 54;
						float speedX78 = 0f;
						float speedY78 = 0f;
						int num295 = 50;
						int num296 = Dust.NewDust(position82, width78, height78, num294, speedX78, speedY78, num295, default(Color), 1.5f);
						Dust dust17 = Main.dust[num296];
						dust17.velocity *= 2f;
						Main.dust[num296].noGravity = true;
					}
					int num297 = Gore.NewGore(new Vector2(base.position.X, base.position.Y - 10f), new Vector2(hitDirection, 0f), 99, scale);
					Gore gore23 = Main.gore[num297];
					gore23.velocity *= 0.3f;
					num297 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 15f), new Vector2(hitDirection, 0f), 99, scale);
					Gore gore24 = Main.gore[num297];
					gore24.velocity *= 0.3f;
					num297 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)base.height - 20f), new Vector2(hitDirection, 0f), 99, scale);
					Gore gore25 = Main.gore[num297];
					gore25.velocity *= 0.3f;
				}
				else if (type == 140)
				{
					if (life <= 0)
					{
						for (int num298 = 0; num298 < 20; num298++)
						{
							Vector2 position83 = base.position;
							int width79 = base.width;
							int height79 = base.height;
							int num299 = 54;
							float speedX79 = 0f;
							float speedY79 = 0f;
							int num300 = 50;
							int num301 = Dust.NewDust(position83, width79, height79, num299, speedX79, speedY79, num300, default(Color), 1.5f);
							Dust dust18 = Main.dust[num301];
							dust18.velocity *= 2f;
							Main.dust[num301].noGravity = true;
						}
						int num302 = Gore.NewGore(new Vector2(base.position.X, base.position.Y - 10f), new Vector2(hitDirection, 0f), 99, scale);
						Gore gore26 = Main.gore[num302];
						gore26.velocity *= 0.3f;
						num302 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)(base.height / 2) - 15f), new Vector2(hitDirection, 0f), 99, scale);
						Gore gore27 = Main.gore[num302];
						gore27.velocity *= 0.3f;
						num302 = Gore.NewGore(new Vector2(base.position.X, base.position.Y + (float)base.height - 20f), new Vector2(hitDirection, 0f), 99, scale);
						Gore gore28 = Main.gore[num302];
						gore28.velocity *= 0.3f;
					}
				}
				else if (type == 39 || type == 40 || type == 41)
				{
					if (life > 0)
					{
						for (int num303 = 0; (double)num303 < dmg / (double)lifeMax * 50.0; num303++)
						{
							Vector2 position84 = base.position;
							int width80 = base.width;
							int height80 = base.height;
							int num304 = 26;
							float speedX80 = hitDirection;
							float speedY80 = -1f;
							int num305 = 0;
							Dust.NewDust(position84, width80, height80, num304, speedX80, speedY80, num305);
						}
						return;
					}
					for (int num306 = 0; num306 < 20; num306++)
					{
						Vector2 position85 = base.position;
						int width81 = base.width;
						int height81 = base.height;
						int num307 = 26;
						float speedX81 = 2.5f * (float)hitDirection;
						float speedY81 = -2.5f;
						int num308 = 0;
						Dust.NewDust(position85, width81, height81, num307, speedX81, speedY81, num308);
					}
					Gore.NewGore(base.position, base.velocity, type - 39 + 67);
				}
				else if (type == 34)
				{
					if (life > 0)
					{
						for (int num309 = 0; (double)num309 < dmg / (double)lifeMax * 30.0; num309++)
						{
							Vector2 position86 = new Vector2(base.position.X, base.position.Y);
							int width82 = base.width;
							int height82 = base.height;
							int num310 = 15;
							float speedX82 = (0f - base.velocity.X) * 0.2f;
							float speedY82 = (0f - base.velocity.Y) * 0.2f;
							int num311 = 100;
							int num312 = Dust.NewDust(position86, width82, height82, num310, speedX82, speedY82, num311, default(Color), 1.8f);
							Main.dust[num312].noLight = true;
							Main.dust[num312].noGravity = true;
							Dust dust19 = Main.dust[num312];
							dust19.velocity *= 1.3f;
							Vector2 position87 = new Vector2(base.position.X, base.position.Y);
							int width83 = base.width;
							int height83 = base.height;
							int num313 = 26;
							float speedX83 = (0f - base.velocity.X) * 0.2f;
							float speedY83 = (0f - base.velocity.Y) * 0.2f;
							int num314 = 0;
							num312 = Dust.NewDust(position87, width83, height83, num313, speedX83, speedY83, num314, default(Color), 0.9f);
							Main.dust[num312].noLight = true;
							Dust dust20 = Main.dust[num312];
							dust20.velocity *= 1.3f;
						}
					}
					else
					{
						for (int num315 = 0; num315 < 15; num315++)
						{
							Vector2 position88 = new Vector2(base.position.X, base.position.Y);
							int width84 = base.width;
							int height84 = base.height;
							int num316 = 15;
							float speedX84 = (0f - base.velocity.X) * 0.2f;
							float speedY84 = (0f - base.velocity.Y) * 0.2f;
							int num317 = 100;
							int num318 = Dust.NewDust(position88, width84, height84, num316, speedX84, speedY84, num317, default(Color), 1.8f);
							Main.dust[num318].noLight = true;
							Main.dust[num318].noGravity = true;
							Dust dust21 = Main.dust[num318];
							dust21.velocity *= 1.3f;
							Vector2 position89 = new Vector2(base.position.X, base.position.Y);
							int width85 = base.width;
							int height85 = base.height;
							int num319 = 26;
							float speedX85 = (0f - base.velocity.X) * 0.2f;
							float speedY85 = (0f - base.velocity.Y) * 0.2f;
							int num320 = 0;
							num318 = Dust.NewDust(position89, width85, height85, num319, speedX85, speedY85, num320, default(Color), 0.9f);
							Main.dust[num318].noLight = true;
							Dust dust22 = Main.dust[num318];
							dust22.velocity *= 1.3f;
						}
					}
				}
				else if (type == 35 || type == 36)
				{
					if (life > 0)
					{
						for (int num321 = 0; (double)num321 < dmg / (double)lifeMax * 100.0; num321++)
						{
							Vector2 position90 = base.position;
							int width86 = base.width;
							int height86 = base.height;
							int num322 = 26;
							float speedX86 = hitDirection;
							float speedY86 = -1f;
							int num323 = 0;
							Dust.NewDust(position90, width86, height86, num322, speedX86, speedY86, num323);
						}
						return;
					}
					for (int num324 = 0; num324 < 150; num324++)
					{
						Vector2 position91 = base.position;
						int width87 = base.width;
						int height87 = base.height;
						int num325 = 26;
						float speedX87 = 2.5f * (float)hitDirection;
						float speedY87 = -2.5f;
						int num326 = 0;
						Dust.NewDust(position91, width87, height87, num325, speedX87, speedY87, num326);
					}
					if (type == 35)
					{
						Gore.NewGore(base.position, base.velocity, 54);
						Gore.NewGore(base.position, base.velocity, 55);
						return;
					}
					Gore.NewGore(base.position, base.velocity, 56);
					Gore.NewGore(base.position, base.velocity, 57);
					Gore.NewGore(base.position, base.velocity, 57);
					Gore.NewGore(base.position, base.velocity, 57);
				}
				else if (type == 139)
				{
					if (life <= 0)
					{
						for (int num327 = 0; num327 < 10; num327++)
						{
							Vector2 position92 = new Vector2(base.position.X, base.position.Y);
							int width88 = base.width;
							int height88 = base.height;
							int num328 = 31;
							float speedX88 = 0f;
							float speedY88 = 0f;
							int num329 = 100;
							int num330 = Dust.NewDust(position92, width88, height88, num328, speedX88, speedY88, num329, default(Color), 1.5f);
							Dust dust23 = Main.dust[num330];
							dust23.velocity *= 1.4f;
						}
						for (int num331 = 0; num331 < 5; num331++)
						{
							Vector2 position93 = new Vector2(base.position.X, base.position.Y);
							int width89 = base.width;
							int height89 = base.height;
							int num332 = 6;
							float speedX89 = 0f;
							float speedY89 = 0f;
							int num333 = 100;
							int num334 = Dust.NewDust(position93, width89, height89, num332, speedX89, speedY89, num333, default(Color), 2.5f);
							Main.dust[num334].noGravity = true;
							Dust dust24 = Main.dust[num334];
							dust24.velocity *= 5f;
							Vector2 position94 = new Vector2(base.position.X, base.position.Y);
							int width90 = base.width;
							int height90 = base.height;
							int num335 = 6;
							float speedX90 = 0f;
							float speedY90 = 0f;
							int num336 = 100;
							num334 = Dust.NewDust(position94, width90, height90, num335, speedX90, speedY90, num336, default(Color), 1.5f);
							Dust dust25 = Main.dust[num334];
							dust25.velocity *= 3f;
						}
						Vector2 position95 = new Vector2(base.position.X, base.position.Y);
						int num337 = Gore.NewGore(position95, default(Vector2), Main.rand.Next(61, 64));
						Gore gore29 = Main.gore[num337];
						gore29.velocity *= 0.4f;
						Gore gore30 = Main.gore[num337];
						gore30.velocity.X = gore30.velocity.X + 1f;
						Gore gore31 = Main.gore[num337];
						gore31.velocity.Y = gore31.velocity.Y + 1f;
						Vector2 position96 = new Vector2(base.position.X, base.position.Y);
						num337 = Gore.NewGore(position96, default(Vector2), Main.rand.Next(61, 64));
						Gore gore32 = Main.gore[num337];
						gore32.velocity *= 0.4f;
						Gore gore33 = Main.gore[num337];
						gore33.velocity.X = gore33.velocity.X - 1f;
						Gore gore34 = Main.gore[num337];
						gore34.velocity.Y = gore34.velocity.Y + 1f;
						Vector2 position97 = new Vector2(base.position.X, base.position.Y);
						num337 = Gore.NewGore(position97, default(Vector2), Main.rand.Next(61, 64));
						Gore gore35 = Main.gore[num337];
						gore35.velocity *= 0.4f;
						Gore gore36 = Main.gore[num337];
						gore36.velocity.X = gore36.velocity.X + 1f;
						Gore gore37 = Main.gore[num337];
						gore37.velocity.Y = gore37.velocity.Y - 1f;
						Vector2 position98 = new Vector2(base.position.X, base.position.Y);
						num337 = Gore.NewGore(position98, default(Vector2), Main.rand.Next(61, 64));
						Gore gore38 = Main.gore[num337];
						gore38.velocity *= 0.4f;
						Gore gore39 = Main.gore[num337];
						gore39.velocity.X = gore39.velocity.X - 1f;
						Gore gore40 = Main.gore[num337];
						gore40.velocity.Y = gore40.velocity.Y - 1f;
					}
				}
				else if (type >= 134 && type <= 136)
				{
					if (type == 135 && life > 0 && Main.netMode != 1 && ai[2] == 0f && Main.rand.Next(25) == 0)
					{
						ai[2] = 1f;
						int num338 = NewNPC((int)(base.position.X + (float)(base.width / 2)), (int)(base.position.Y + (float)base.height), 139);
						if (Main.netMode == 2 && num338 < 200)
						{
							NetMessage.SendData(23, -1, -1, "", num338);
						}
						netUpdate = true;
					}
					if (life > 0)
					{
						return;
					}
					Gore.NewGore(base.position, base.velocity, 156);
					if (Main.rand.Next(2) == 0)
					{
						for (int num339 = 0; num339 < 10; num339++)
						{
							Vector2 position99 = new Vector2(base.position.X, base.position.Y);
							int width91 = base.width;
							int height91 = base.height;
							int num340 = 31;
							float speedX91 = 0f;
							float speedY91 = 0f;
							int num341 = 100;
							int num342 = Dust.NewDust(position99, width91, height91, num340, speedX91, speedY91, num341, default(Color), 1.5f);
							Dust dust26 = Main.dust[num342];
							dust26.velocity *= 1.4f;
						}
						for (int num343 = 0; num343 < 5; num343++)
						{
							Vector2 position100 = new Vector2(base.position.X, base.position.Y);
							int width92 = base.width;
							int height92 = base.height;
							int num344 = 6;
							float speedX92 = 0f;
							float speedY92 = 0f;
							int num345 = 100;
							int num346 = Dust.NewDust(position100, width92, height92, num344, speedX92, speedY92, num345, default(Color), 2.5f);
							Main.dust[num346].noGravity = true;
							Dust dust27 = Main.dust[num346];
							dust27.velocity *= 5f;
							Vector2 position101 = new Vector2(base.position.X, base.position.Y);
							int width93 = base.width;
							int height93 = base.height;
							int num347 = 6;
							float speedX93 = 0f;
							float speedY93 = 0f;
							int num348 = 100;
							num346 = Dust.NewDust(position101, width93, height93, num347, speedX93, speedY93, num348, default(Color), 1.5f);
							Dust dust28 = Main.dust[num346];
							dust28.velocity *= 3f;
						}
						Vector2 position102 = new Vector2(base.position.X, base.position.Y);
						int num349 = Gore.NewGore(position102, default(Vector2), Main.rand.Next(61, 64));
						Gore gore41 = Main.gore[num349];
						gore41.velocity *= 0.4f;
						Gore gore42 = Main.gore[num349];
						gore42.velocity.X = gore42.velocity.X + 1f;
						Gore gore43 = Main.gore[num349];
						gore43.velocity.Y = gore43.velocity.Y + 1f;
						Vector2 position103 = new Vector2(base.position.X, base.position.Y);
						num349 = Gore.NewGore(position103, default(Vector2), Main.rand.Next(61, 64));
						Gore gore44 = Main.gore[num349];
						gore44.velocity *= 0.4f;
						Gore gore45 = Main.gore[num349];
						gore45.velocity.X = gore45.velocity.X - 1f;
						Gore gore46 = Main.gore[num349];
						gore46.velocity.Y = gore46.velocity.Y + 1f;
						Vector2 position104 = new Vector2(base.position.X, base.position.Y);
						num349 = Gore.NewGore(position104, default(Vector2), Main.rand.Next(61, 64));
						Gore gore47 = Main.gore[num349];
						gore47.velocity *= 0.4f;
						Gore gore48 = Main.gore[num349];
						gore48.velocity.X = gore48.velocity.X + 1f;
						Gore gore49 = Main.gore[num349];
						gore49.velocity.Y = gore49.velocity.Y - 1f;
						Vector2 position105 = new Vector2(base.position.X, base.position.Y);
						num349 = Gore.NewGore(position105, default(Vector2), Main.rand.Next(61, 64));
						Gore gore50 = Main.gore[num349];
						gore50.velocity *= 0.4f;
						Gore gore51 = Main.gore[num349];
						gore51.velocity.X = gore51.velocity.X - 1f;
						Gore gore52 = Main.gore[num349];
						gore52.velocity.Y = gore52.velocity.Y - 1f;
					}
				}
				else if (type == 127)
				{
					if (life <= 0)
					{
						Gore.NewGore(base.position, base.velocity, 149);
						Gore.NewGore(base.position, base.velocity, 150);
						for (int num350 = 0; num350 < 10; num350++)
						{
							Vector2 position106 = new Vector2(base.position.X, base.position.Y);
							int width94 = base.width;
							int height94 = base.height;
							int num351 = 31;
							float speedX94 = 0f;
							float speedY94 = 0f;
							int num352 = 100;
							int num353 = Dust.NewDust(position106, width94, height94, num351, speedX94, speedY94, num352, default(Color), 1.5f);
							Dust dust29 = Main.dust[num353];
							dust29.velocity *= 1.4f;
						}
						for (int num354 = 0; num354 < 5; num354++)
						{
							Vector2 position107 = new Vector2(base.position.X, base.position.Y);
							int width95 = base.width;
							int height95 = base.height;
							int num355 = 6;
							float speedX95 = 0f;
							float speedY95 = 0f;
							int num356 = 100;
							int num357 = Dust.NewDust(position107, width95, height95, num355, speedX95, speedY95, num356, default(Color), 2.5f);
							Main.dust[num357].noGravity = true;
							Dust dust30 = Main.dust[num357];
							dust30.velocity *= 5f;
							Vector2 position108 = new Vector2(base.position.X, base.position.Y);
							int width96 = base.width;
							int height96 = base.height;
							int num358 = 6;
							float speedX96 = 0f;
							float speedY96 = 0f;
							int num359 = 100;
							num357 = Dust.NewDust(position108, width96, height96, num358, speedX96, speedY96, num359, default(Color), 1.5f);
							Dust dust31 = Main.dust[num357];
							dust31.velocity *= 3f;
						}
						Vector2 position109 = new Vector2(base.position.X, base.position.Y);
						int num360 = Gore.NewGore(position109, default(Vector2), Main.rand.Next(61, 64));
						Gore gore53 = Main.gore[num360];
						gore53.velocity *= 0.4f;
						Gore gore54 = Main.gore[num360];
						gore54.velocity.X = gore54.velocity.X + 1f;
						Gore gore55 = Main.gore[num360];
						gore55.velocity.Y = gore55.velocity.Y + 1f;
						Vector2 position110 = new Vector2(base.position.X, base.position.Y);
						num360 = Gore.NewGore(position110, default(Vector2), Main.rand.Next(61, 64));
						Gore gore56 = Main.gore[num360];
						gore56.velocity *= 0.4f;
						Gore gore57 = Main.gore[num360];
						gore57.velocity.X = gore57.velocity.X - 1f;
						Gore gore58 = Main.gore[num360];
						gore58.velocity.Y = gore58.velocity.Y + 1f;
						Vector2 position111 = new Vector2(base.position.X, base.position.Y);
						num360 = Gore.NewGore(position111, default(Vector2), Main.rand.Next(61, 64));
						Gore gore59 = Main.gore[num360];
						gore59.velocity *= 0.4f;
						Gore gore60 = Main.gore[num360];
						gore60.velocity.X = gore60.velocity.X + 1f;
						Gore gore61 = Main.gore[num360];
						gore61.velocity.Y = gore61.velocity.Y - 1f;
						Vector2 position112 = new Vector2(base.position.X, base.position.Y);
						num360 = Gore.NewGore(position112, default(Vector2), Main.rand.Next(61, 64));
						Gore gore62 = Main.gore[num360];
						gore62.velocity *= 0.4f;
						Gore gore63 = Main.gore[num360];
						gore63.velocity.X = gore63.velocity.X - 1f;
						Gore gore64 = Main.gore[num360];
						gore64.velocity.Y = gore64.velocity.Y - 1f;
					}
				}
				else if (type >= 128 && type <= 131)
				{
					if (life <= 0)
					{
						Gore.NewGore(base.position, base.velocity, 147);
						Gore.NewGore(base.position, base.velocity, 148);
						for (int num361 = 0; num361 < 10; num361++)
						{
							Vector2 position113 = new Vector2(base.position.X, base.position.Y);
							int width97 = base.width;
							int height97 = base.height;
							int num362 = 31;
							float speedX97 = 0f;
							float speedY97 = 0f;
							int num363 = 100;
							int num364 = Dust.NewDust(position113, width97, height97, num362, speedX97, speedY97, num363, default(Color), 1.5f);
							Dust dust32 = Main.dust[num364];
							dust32.velocity *= 1.4f;
						}
						for (int num365 = 0; num365 < 5; num365++)
						{
							Vector2 position114 = new Vector2(base.position.X, base.position.Y);
							int width98 = base.width;
							int height98 = base.height;
							int num366 = 6;
							float speedX98 = 0f;
							float speedY98 = 0f;
							int num367 = 100;
							int num368 = Dust.NewDust(position114, width98, height98, num366, speedX98, speedY98, num367, default(Color), 2.5f);
							Main.dust[num368].noGravity = true;
							Dust dust33 = Main.dust[num368];
							dust33.velocity *= 5f;
							Vector2 position115 = new Vector2(base.position.X, base.position.Y);
							int width99 = base.width;
							int height99 = base.height;
							int num369 = 6;
							float speedX99 = 0f;
							float speedY99 = 0f;
							int num370 = 100;
							num368 = Dust.NewDust(position115, width99, height99, num369, speedX99, speedY99, num370, default(Color), 1.5f);
							Dust dust34 = Main.dust[num368];
							dust34.velocity *= 3f;
						}
						Vector2 position116 = new Vector2(base.position.X, base.position.Y);
						int num371 = Gore.NewGore(position116, default(Vector2), Main.rand.Next(61, 64));
						Gore gore65 = Main.gore[num371];
						gore65.velocity *= 0.4f;
						Gore gore66 = Main.gore[num371];
						gore66.velocity.X = gore66.velocity.X + 1f;
						Gore gore67 = Main.gore[num371];
						gore67.velocity.Y = gore67.velocity.Y + 1f;
						Vector2 position117 = new Vector2(base.position.X, base.position.Y);
						num371 = Gore.NewGore(position117, default(Vector2), Main.rand.Next(61, 64));
						Gore gore68 = Main.gore[num371];
						gore68.velocity *= 0.4f;
						Gore gore69 = Main.gore[num371];
						gore69.velocity.X = gore69.velocity.X - 1f;
						Gore gore70 = Main.gore[num371];
						gore70.velocity.Y = gore70.velocity.Y + 1f;
						Vector2 position118 = new Vector2(base.position.X, base.position.Y);
						num371 = Gore.NewGore(position118, default(Vector2), Main.rand.Next(61, 64));
						Gore gore71 = Main.gore[num371];
						gore71.velocity *= 0.4f;
						Gore gore72 = Main.gore[num371];
						gore72.velocity.X = gore72.velocity.X + 1f;
						Gore gore73 = Main.gore[num371];
						gore73.velocity.Y = gore73.velocity.Y - 1f;
						Vector2 position119 = new Vector2(base.position.X, base.position.Y);
						num371 = Gore.NewGore(position119, default(Vector2), Main.rand.Next(61, 64));
						Gore gore74 = Main.gore[num371];
						gore74.velocity *= 0.4f;
						Gore gore75 = Main.gore[num371];
						gore75.velocity.X = gore75.velocity.X - 1f;
						Gore gore76 = Main.gore[num371];
						gore76.velocity.Y = gore76.velocity.Y - 1f;
					}
				}
				else if (type == 23)
				{
					if (life > 0)
					{
						for (int num372 = 0; (double)num372 < dmg / (double)lifeMax * 100.0; num372++)
						{
							int num373 = 25;
							if (Main.rand.Next(2) == 0)
							{
								num373 = 6;
							}
							Vector2 position120 = base.position;
							int width100 = base.width;
							int height100 = base.height;
							int num374 = num373;
							float speedX100 = hitDirection;
							float speedY100 = -1f;
							int num375 = 0;
							Dust.NewDust(position120, width100, height100, num374, speedX100, speedY100, num375);
							Vector2 position121 = new Vector2(base.position.X, base.position.Y);
							int width101 = base.width;
							int height101 = base.height;
							int num376 = 6;
							float speedX101 = base.velocity.X * 0.2f;
							float speedY101 = base.velocity.Y * 0.2f;
							int num377 = 100;
							int num378 = Dust.NewDust(position121, width101, height101, num376, speedX101, speedY101, num377, default(Color), 2f);
							Main.dust[num378].noGravity = true;
						}
						return;
					}
					for (int num379 = 0; num379 < 50; num379++)
					{
						int num380 = 25;
						if (Main.rand.Next(2) == 0)
						{
							num380 = 6;
						}
						Vector2 position122 = base.position;
						int width102 = base.width;
						int height102 = base.height;
						int num381 = num380;
						float speedX102 = 2 * hitDirection;
						float speedY102 = -2f;
						int num382 = 0;
						Dust.NewDust(position122, width102, height102, num381, speedX102, speedY102, num382);
					}
					for (int num383 = 0; num383 < 50; num383++)
					{
						Vector2 position123 = new Vector2(base.position.X, base.position.Y);
						int width103 = base.width;
						int height103 = base.height;
						int num384 = 6;
						float speedX103 = base.velocity.X * 0.2f;
						float speedY103 = base.velocity.Y * 0.2f;
						int num385 = 100;
						int num386 = Dust.NewDust(position123, width103, height103, num384, speedX103, speedY103, num385, default(Color), 2.5f);
						Dust dust35 = Main.dust[num386];
						dust35.velocity *= 6f;
						Main.dust[num386].noGravity = true;
					}
				}
				else if (type == 24)
				{
					if (life > 0)
					{
						for (int num387 = 0; (double)num387 < dmg / (double)lifeMax * 100.0; num387++)
						{
							Vector2 position124 = new Vector2(base.position.X, base.position.Y);
							int width104 = base.width;
							int height104 = base.height;
							int num388 = 6;
							float x2 = base.velocity.X;
							float y2 = base.velocity.Y;
							int num389 = 100;
							int num390 = Dust.NewDust(position124, width104, height104, num388, x2, y2, num389, default(Color), 2.5f);
							Main.dust[num390].noGravity = true;
						}
						return;
					}
					for (int num391 = 0; num391 < 50; num391++)
					{
						Vector2 position125 = new Vector2(base.position.X, base.position.Y);
						int width105 = base.width;
						int height105 = base.height;
						int num392 = 6;
						float x3 = base.velocity.X;
						float y3 = base.velocity.Y;
						int num393 = 100;
						int num394 = Dust.NewDust(position125, width105, height105, num392, x3, y3, num393, default(Color), 2.5f);
						Main.dust[num394].noGravity = true;
						Dust dust36 = Main.dust[num394];
						dust36.velocity *= 2f;
					}
					Gore.NewGore(base.position, base.velocity, 45);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 46);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 46);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 47);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 47);
				}
				else if (type == 25)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num395 = 0; num395 < 20; num395++)
					{
						Vector2 position126 = new Vector2(base.position.X, base.position.Y);
						int width106 = base.width;
						int height106 = base.height;
						int num396 = 6;
						float speedX104 = (0f - base.velocity.X) * 0.2f;
						float speedY104 = (0f - base.velocity.Y) * 0.2f;
						int num397 = 100;
						int num398 = Dust.NewDust(position126, width106, height106, num396, speedX104, speedY104, num397, default(Color), 2f);
						Main.dust[num398].noGravity = true;
						Dust dust37 = Main.dust[num398];
						dust37.velocity *= 2f;
						Vector2 position127 = new Vector2(base.position.X, base.position.Y);
						int width107 = base.width;
						int height107 = base.height;
						int num399 = 6;
						float speedX105 = (0f - base.velocity.X) * 0.2f;
						float speedY105 = (0f - base.velocity.Y) * 0.2f;
						int num400 = 100;
						num398 = Dust.NewDust(position127, width107, height107, num399, speedX105, speedY105, num400);
						Dust dust38 = Main.dust[num398];
						dust38.velocity *= 2f;
					}
				}
				else if (type == 33)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num401 = 0; num401 < 20; num401++)
					{
						Vector2 position128 = new Vector2(base.position.X, base.position.Y);
						int width108 = base.width;
						int height108 = base.height;
						int num402 = 29;
						float speedX106 = (0f - base.velocity.X) * 0.2f;
						float speedY106 = (0f - base.velocity.Y) * 0.2f;
						int num403 = 100;
						int num404 = Dust.NewDust(position128, width108, height108, num402, speedX106, speedY106, num403, default(Color), 2f);
						Main.dust[num404].noGravity = true;
						Dust dust39 = Main.dust[num404];
						dust39.velocity *= 2f;
						Vector2 position129 = new Vector2(base.position.X, base.position.Y);
						int width109 = base.width;
						int height109 = base.height;
						int num405 = 29;
						float speedX107 = (0f - base.velocity.X) * 0.2f;
						float speedY107 = (0f - base.velocity.Y) * 0.2f;
						int num406 = 100;
						num404 = Dust.NewDust(position129, width109, height109, num405, speedX107, speedY107, num406);
						Dust dust40 = Main.dust[num404];
						dust40.velocity *= 2f;
					}
				}
				else if (type == 26 || type == 27 || type == 28 || type == 29 || type == 73 || type == 111)
				{
					if (life > 0)
					{
						for (int num407 = 0; (double)num407 < dmg / (double)lifeMax * 100.0; num407++)
						{
							Vector2 position130 = base.position;
							int width110 = base.width;
							int height110 = base.height;
							int num408 = 5;
							float speedX108 = hitDirection;
							float speedY108 = -1f;
							int num409 = 0;
							Dust.NewDust(position130, width110, height110, num408, speedX108, speedY108, num409);
						}
						return;
					}
					for (int num410 = 0; num410 < 50; num410++)
					{
						Vector2 position131 = base.position;
						int width111 = base.width;
						int height111 = base.height;
						int num411 = 5;
						float speedX109 = 2.5f * (float)hitDirection;
						float speedY109 = -2.5f;
						int num412 = 0;
						Dust.NewDust(position131, width111, height111, num411, speedX109, speedY109, num412);
					}
					Gore.NewGore(base.position, base.velocity, 48, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 49, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 20f), base.velocity, 49, scale);
					if (type == 111)
					{
						Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 131, scale);
					}
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 50, scale);
					Gore.NewGore(new Vector2(base.position.X, base.position.Y + 34f), base.velocity, 50, scale);
				}
				else if (type == 30)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num413 = 0; num413 < 20; num413++)
					{
						Vector2 position132 = new Vector2(base.position.X, base.position.Y);
						int width112 = base.width;
						int height112 = base.height;
						int num414 = 27;
						float speedX110 = (0f - base.velocity.X) * 0.2f;
						float speedY110 = (0f - base.velocity.Y) * 0.2f;
						int num415 = 100;
						int num416 = Dust.NewDust(position132, width112, height112, num414, speedX110, speedY110, num415, default(Color), 2f);
						Main.dust[num416].noGravity = true;
						Dust dust41 = Main.dust[num416];
						dust41.velocity *= 2f;
						Vector2 position133 = new Vector2(base.position.X, base.position.Y);
						int width113 = base.width;
						int height113 = base.height;
						int num417 = 27;
						float speedX111 = (0f - base.velocity.X) * 0.2f;
						float speedY111 = (0f - base.velocity.Y) * 0.2f;
						int num418 = 100;
						num416 = Dust.NewDust(position133, width113, height113, num417, speedX111, speedY111, num418);
						Dust dust42 = Main.dust[num416];
						dust42.velocity *= 2f;
					}
				}
				else if (type == 42)
				{
					if (life > 0)
					{
						for (int num419 = 0; (double)num419 < dmg / (double)lifeMax * 100.0; num419++)
						{
							Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -1f, alpha, color, scale);
						}
						return;
					}
					for (int num420 = 0; num420 < 50; num420++)
					{
						Dust.NewDust(base.position, base.width, base.height, 18, hitDirection, -2f, alpha, color, scale);
					}
					Gore.NewGore(base.position, base.velocity, 70, scale);
					Gore.NewGore(base.position, base.velocity, 71, scale);
				}
				else if (type == 43 || type == 56)
				{
					if (life > 0)
					{
						for (int num421 = 0; (double)num421 < dmg / (double)lifeMax * 100.0; num421++)
						{
							Dust.NewDust(base.position, base.width, base.height, 40, hitDirection, -1f, alpha, color, 1.2f);
						}
						return;
					}
					for (int num422 = 0; num422 < 50; num422++)
					{
						Dust.NewDust(base.position, base.width, base.height, 40, hitDirection, -2f, alpha, color, 1.2f);
					}
					Gore.NewGore(base.position, base.velocity, 72);
					Gore.NewGore(base.position, base.velocity, 72);
				}
				else if (type == 48)
				{
					if (life > 0)
					{
						for (int num423 = 0; (double)num423 < dmg / (double)lifeMax * 100.0; num423++)
						{
							Vector2 position134 = base.position;
							int width114 = base.width;
							int height114 = base.height;
							int num424 = 5;
							float speedX112 = hitDirection;
							float speedY112 = -1f;
							int num425 = 0;
							Dust.NewDust(position134, width114, height114, num424, speedX112, speedY112, num425);
						}
						return;
					}
					for (int num426 = 0; num426 < 50; num426++)
					{
						Vector2 position135 = base.position;
						int width115 = base.width;
						int height115 = base.height;
						int num427 = 5;
						float speedX113 = 2 * hitDirection;
						float speedY113 = -2f;
						int num428 = 0;
						Dust.NewDust(position135, width115, height115, num427, speedX113, speedY113, num428);
					}
					Gore.NewGore(base.position, base.velocity, 80);
					Gore.NewGore(base.position, base.velocity, 81);
				}
				else
				{
					if (type != 62 && type != 66)
					{
						return;
					}
					if (life > 0)
					{
						for (int num429 = 0; (double)num429 < dmg / (double)lifeMax * 100.0; num429++)
						{
							Vector2 position136 = base.position;
							int width116 = base.width;
							int height116 = base.height;
							int num430 = 5;
							float speedX114 = hitDirection;
							float speedY114 = -1f;
							int num431 = 0;
							Dust.NewDust(position136, width116, height116, num430, speedX114, speedY114, num431);
						}
						return;
					}
					for (int num432 = 0; num432 < 50; num432++)
					{
						Vector2 position137 = base.position;
						int width117 = base.width;
						int height117 = base.height;
						int num433 = 5;
						float speedX115 = 2 * hitDirection;
						float speedY115 = -2f;
						int num434 = 0;
						Dust.NewDust(position137, width117, height117, num433, speedX115, speedY115, num434);
					}
					Gore.NewGore(base.position, base.velocity, 93);
					Gore.NewGore(base.position, base.velocity, 94);
					Gore.NewGore(base.position, base.velocity, 94);
				}
			}
			else if (life > 0)
			{
				for (int num435 = 0; (double)num435 < dmg / (double)lifeMax * 80.0; num435++)
				{
					Vector2 position138 = base.position;
					int width118 = base.width;
					int height118 = base.height;
					int num436 = 6;
					float speedX116 = hitDirection * 2;
					float speedY116 = -1f;
					int num437 = alpha;
					Dust.NewDust(position138, width118, height118, num436, speedX116, speedY116, num437, default(Color), 1.5f);
				}
			}
			else
			{
				for (int num438 = 0; num438 < 40; num438++)
				{
					Vector2 position139 = base.position;
					int width119 = base.width;
					int height119 = base.height;
					int num439 = 6;
					float speedX117 = hitDirection * 2;
					float speedY117 = -1f;
					int num440 = alpha;
					Dust.NewDust(position139, width119, height119, num439, speedX117, speedY117, num440, default(Color), 1.5f);
				}
			}
		}

		public static bool AnyNPCs(int Type)
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == Type)
				{
					return true;
				}
			}
			return false;
		}

		public static bool AnyNPCs(string name)
		{
			if (!Config.npcDefs.byName.ContainsKey(name))
			{
				return false;
			}
			int num = Config.npcDefs.byName[name].type;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == num)
				{
					return true;
				}
			}
			return false;
		}

		public static void SpawnSkeletron()
		{
			bool flag = true;
			bool flag2 = false;
			Vector2 vector = default(Vector2);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 35)
				{
					flag = false;
					break;
				}
			}
			for (int j = 0; j < 200; j++)
			{
				if (Main.npc[j].active && Main.npc[j].type == 37)
				{
					flag2 = true;
					Main.npc[j].ai[3] = 1f;
					vector = Main.npc[j].position;
					num = Main.npc[j].width;
					num2 = Main.npc[j].height;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, "", j);
					}
				}
			}
			if (flag && flag2)
			{
				int num3 = NewNPC((int)vector.X + num / 2, (int)vector.Y + num2 / 2, 35);
				Main.npc[num3].netUpdate = true;
				string str = "Skeletron";
				if (Main.netMode == 0)
				{
					Main.NewText(str + " " + Lang.misc[16], 175, 75);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, str + " " + Lang.misc[16], 255, 175f, 75f, 255f);
				}
			}
		}

		public static bool NearSpikeBall(int x, int y)
		{
			Rectangle rectangle = new Rectangle(x * 16 - 300, y * 16 - 300, 600, 600);
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].aiStyle == 20)
				{
					Rectangle rectangle2 = new Rectangle((int)Main.npc[i].ai[1], (int)Main.npc[i].ai[2], 20, 20);
					if (rectangle.Intersects(rectangle2))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void AddBuff(string buffName, int time, bool quiet = false)
		{
			AddBuff(Config.buffDefs.ID[buffName], time, quiet);
		}

		public int HasBuff(string buffName)
		{
			int num = Config.buffDefs.ID[buffName];
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] == num && buffTime[i] > 0)
				{
					return i;
				}
			}
			return -1;
		}

		public void AddBuff(int type, int time, bool quiet = false)
		{
			if (buffImmune[type])
			{
				return;
			}
			if (!quiet)
			{
				if (Main.netMode == 1)
				{
					NetMessage.SendData(53, -1, -1, "", whoAmI, type, time);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(54, -1, -1, "", whoAmI);
				}
			}
			int num = -1;
			for (int i = 0; i < 5; i++)
			{
				if (buffType[i] == type)
				{
					if (buffTime[i] < time)
					{
						buffTime[i] = time;
					}
					return;
				}
			}
			while (num == -1)
			{
				int num2 = -1;
				for (int j = 0; j < 5; j++)
				{
					if (!Main.debuff[buffType[j]])
					{
						num2 = j;
						break;
					}
				}
				if (num2 == -1)
				{
					return;
				}
				for (int k = num2; k < 5; k++)
				{
					if (buffType[k] == 0)
					{
						num = k;
						break;
					}
				}
				if (num == -1)
				{
					DelBuff(num2);
				}
			}
			buffType[num] = type;
			buffTime[num] = time;
			if (Config.buffDefs.assemblyByType[type] != null)
			{
				Assembly assembly = Config.buffDefs.assemblyByType[type];
				buffCode[num] = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Main.buffName[type]) + "Buff");
			}
			Codable.RunSpecifiedMethod("Buff " + Main.buffName[buffType[num]], buffCode[num], "NPCEffectsStart", this, num, type, buffTime[num]);
		}

		public void DelBuff(int b)
		{
			int num = buffType[b];
			Codable.RunSpecifiedMethod("Buff " + Main.buffName[num], buffCode[b], "NPCEffectsEnd", this, b, num, buffTime[b]);
			buffCode[b] = null;
			buffTime[b] = 0;
			buffType[b] = 0;
			for (int i = 0; i < 4; i++)
			{
				if (buffTime[i] == 0 || buffType[i] == 0)
				{
					for (int j = i + 1; j < 5; j++)
					{
						buffTime[j - 1] = buffTime[j];
						buffType[j - 1] = buffType[j];
						buffCode[j - 1] = buffCode[j];
						buffTime[j] = 0;
						buffType[j] = 0;
						buffCode[j] = null;
					}
				}
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(54, -1, -1, "", whoAmI);
			}
		}

		public void UpdateNPC(int i)
		{
			whoAmI = i;
			if (!active)
			{
				return;
			}
			if (displayName == "")
			{
				displayName = name;
			}
			if (townNPC && Main.chrName[type] != "")
			{
				displayName = Main.chrName[type];
			}
			lifeRegen = 0;
			poisoned = false;
			onFire = false;
			onFire2 = false;
			confused = false;
			RunMethod("UpdateNPC");
			for (int j = 0; j < 5; j++)
			{
				if (buffType[j] <= 0 || buffTime[j] <= 0)
				{
					continue;
				}
				buffTime[j]--;
				if (!Codable.RunSpecifiedMethod("Buff " + Main.buffName[buffType[j]], buffCode[j], "NPCEffects", this, j, buffType[j], buffTime[j]))
				{
					if (buffType[j] == 20)
					{
						poisoned = true;
					}
					else if (buffType[j] == 24)
					{
						onFire = true;
					}
					else if (buffType[j] == 31)
					{
						confused = true;
					}
					else if (buffType[j] == 39)
					{
						onFire2 = true;
					}
				}
			}
			if (Main.netMode != 1)
			{
				for (int k = 0; k < 5; k++)
				{
					if (buffType[k] > 0 && buffTime[k] <= 0)
					{
						DelBuff(k);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(54, -1, -1, "", whoAmI);
						}
					}
				}
			}
			if (!dontTakeDamage)
			{
				if (poisoned)
				{
					lifeRegen = -4;
				}
				if (onFire)
				{
					lifeRegen = -8;
				}
				if (onFire2)
				{
					lifeRegen = -12;
				}
				lifeRegenCount += lifeRegen;
				while (lifeRegenCount >= 120)
				{
					lifeRegenCount -= 120;
					if (life < lifeMax)
					{
						life++;
					}
					if (life > lifeMax)
					{
						life = lifeMax;
					}
				}
				while (lifeRegenCount <= -120)
				{
					lifeRegenCount += 120;
					int num = whoAmI;
					if (realLife >= 0)
					{
						num = realLife;
					}
					Main.npc[num].life--;
					if (Main.npc[num].life > 0)
					{
						continue;
					}
					Main.npc[num].life = 1;
					if (Main.netMode != 1)
					{
						Main.npc[num].StrikeNPC(9999, 0f, 0);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(28, -1, -1, "", num, 1f, 0f, 0f, 9999);
						}
					}
				}
			}
			if (Main.netMode != 1 && Main.bloodMoon)
			{
				if (type == 46)
				{
					Transform(47);
				}
				else if (type == 55)
				{
					Transform(57);
				}
			}
			maxGravity = 10f;
			baseGravity = 0.3f;
			float num2 = Main.maxTilesX / 4200;
			num2 *= num2;
			float num3 = (float)((double)(base.position.Y / 16f - (60f + 10f * num2)) / (Main.worldSurface / 6.0));
			if ((double)num3 < 0.25)
			{
				num3 = 0.25f;
			}
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			baseGravity *= num3;
			if (wet)
			{
				baseGravity = 0.2f;
				maxGravity = 7f;
			}
			if (soundDelay > 0)
			{
				soundDelay--;
			}
			if (life <= 0)
			{
				active = false;
			}
			oldTarget = target;
			oldDirection = direction;
			oldDirectionY = base.directionY;
			if (!RunMethod("PreAI") || (bool)Codable.customMethodReturn)
			{
				AI();
			}
			RunMethod("PostAI");
			if (type == 44)
			{
				Lighting.addLight((int)(base.position.X + (float)(width / 2)) / 16, (int)(base.position.Y + 4f) / 16, 0.9f, 0.75f, 0.5f);
			}
			for (int l = 0; l < 256; l++)
			{
				if (immune[l] > 0)
				{
					immune[l]--;
				}
			}
			if (!noGravity && !noTileCollide)
			{
				int num4 = (int)(base.position.X + (float)(width / 2)) / 16;
				int num5 = (int)(base.position.Y + (float)(height / 2)) / 16;
				if (Main.tile[num4, num5] == null)
				{
					baseGravity = 0f;
					base.velocity.X = 0f;
					base.velocity.Y = 0f;
				}
			}
			if (!noGravity)
			{
				base.velocity.Y = base.velocity.Y + baseGravity;
				if (base.velocity.Y > maxGravity)
				{
					base.velocity.Y = maxGravity;
				}
			}
			if ((double)base.velocity.X < 0.005 && (double)base.velocity.X > -0.005)
			{
				base.velocity.X = 0f;
			}
			if (Main.netMode != 1 && type != 37 && (friendly || type == 46 || type == 55 || type == 74))
			{
				if (life < lifeMax)
				{
					friendlyRegen++;
					if (friendlyRegen > 300)
					{
						friendlyRegen = 0;
						life++;
						netUpdate = true;
					}
				}
				if (immune[255] == 0)
				{
					Rectangle rectangle = new Rectangle((int)base.position.X, (int)base.position.Y, width, height);
					for (int m = 0; m < 200; m++)
					{
						if (!Main.npc[m].active || Main.npc[m].friendly || Main.npc[m].damage <= 0)
						{
							continue;
						}
						Rectangle rectangle2 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
						if (!rectangle.Intersects(rectangle2))
						{
							continue;
						}
						int number = Main.npc[m].damage;
						int num6 = 6;
						int num7 = 1;
						if (Main.npc[m].position.X + (float)(Main.npc[m].width / 2) > base.position.X + (float)(width / 2))
						{
							num7 = -1;
						}
						bool flag = Main.rand.Next(1, 101) <= CritChance;
						Main.npc[i].StrikeNPC(number, num6, num7, crit: false, noEffect: false, CritMult);
						if (Main.netMode != 0)
						{
							if (flag)
							{
								NetMessage.SendData(28, -1, -1, "", i, CritMult, num6, num7, number);
							}
							else
							{
								NetMessage.SendData(28, -1, -1, "", i, 1f, num6, num7, number);
							}
						}
						netUpdate = true;
						immune[255] = 30;
					}
				}
			}
			if (!noTileCollide)
			{
				bool flag2 = Collision.LavaCollision(base.position, width, height);
				if (flag2)
				{
					lavaWet = true;
					if (!lavaImmune && !dontTakeDamage && Main.netMode != 1 && immune[255] == 0)
					{
						AddBuff(24, 420);
						immune[255] = 30;
						StrikeNPC(50, 0f, 0);
						if (Main.netMode == 2 && Main.netMode != 0)
						{
							NetMessage.SendData(28, -1, -1, "", whoAmI, 1f, 0f, 0f, 50);
						}
					}
				}
				bool flag3 = false;
				if (type == 72)
				{
					flag3 = false;
					wetCount = 0;
					flag2 = false;
				}
				else
				{
					flag3 = Collision.WetCollision(base.position, width, height);
				}
				if (flag3)
				{
					if (onFire && !lavaWet && Main.netMode != 1)
					{
						for (int n = 0; n < 5; n++)
						{
							if (buffType[n] == 24)
							{
								DelBuff(n);
							}
						}
					}
					if (!wet && wetCount == 0)
					{
						wetCount = 10;
						if (!flag2)
						{
							for (int num8 = 0; num8 < 30; num8++)
							{
								int num9 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 33);
								Dust dust = Main.dust[num9];
								dust.velocity.Y = dust.velocity.Y - 4f;
								Dust dust2 = Main.dust[num9];
								dust2.velocity.X = dust2.velocity.X * 2.5f;
								Main.dust[num9].scale = 1.3f;
								Main.dust[num9].alpha = 100;
								Main.dust[num9].noGravity = true;
							}
							if (type != 1 && type != 59 && !noGravity)
							{
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y, 0);
							}
						}
						else
						{
							for (int num10 = 0; num10 < 10; num10++)
							{
								int num11 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 35);
								Dust dust3 = Main.dust[num11];
								dust3.velocity.Y = dust3.velocity.Y - 1.5f;
								Dust dust4 = Main.dust[num11];
								dust4.velocity.X = dust4.velocity.X * 2.5f;
								Main.dust[num11].scale = 1.3f;
								Main.dust[num11].alpha = 100;
								Main.dust[num11].noGravity = true;
							}
							if (type != 1 && type != 59 && !noGravity)
							{
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
							}
						}
					}
					wet = true;
				}
				else if (wet)
				{
					base.velocity.X = base.velocity.X * 0.5f;
					wet = false;
					if (wetCount == 0)
					{
						wetCount = 10;
						if (!lavaWet)
						{
							for (int num12 = 0; num12 < 30; num12++)
							{
								int num13 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 33);
								Dust dust5 = Main.dust[num13];
								dust5.velocity.Y = dust5.velocity.Y - 4f;
								Dust dust6 = Main.dust[num13];
								dust6.velocity.X = dust6.velocity.X * 2.5f;
								Main.dust[num13].scale = 1.3f;
								Main.dust[num13].alpha = 100;
								Main.dust[num13].noGravity = true;
							}
							if (type != 1 && type != 59 && !noGravity)
							{
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y, 0);
							}
						}
						else
						{
							for (int num14 = 0; num14 < 10; num14++)
							{
								int num15 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 35);
								Dust dust7 = Main.dust[num15];
								dust7.velocity.Y = dust7.velocity.Y - 1.5f;
								Dust dust8 = Main.dust[num15];
								dust8.velocity.X = dust8.velocity.X * 2.5f;
								Main.dust[num15].scale = 1.3f;
								Main.dust[num15].alpha = 100;
								Main.dust[num15].noGravity = true;
							}
							if (type != 1 && type != 59 && !noGravity)
							{
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
							}
						}
					}
				}
				if (!wet)
				{
					lavaWet = false;
				}
				if (wetCount > 0)
				{
					wetCount--;
				}
				bool flag4 = false;
				if (aiStyle == 10)
				{
					flag4 = true;
				}
				if (aiStyle == 14)
				{
					flag4 = true;
				}
				if (aiStyle == 3 && base.directionY == 1)
				{
					flag4 = true;
				}
				oldVelocity = base.velocity;
				collideX = false;
				collideY = false;
				if (wet)
				{
					Vector2 velocity = base.velocity;
					base.velocity = Collision.TileCollision(base.position, base.velocity, width, height, flag4, flag4);
					if (Collision.up)
					{
						base.velocity.Y = 0.01f;
					}
					Vector2 vector = base.velocity * 0.5f;
					if (base.velocity.X != velocity.X)
					{
						vector.X = base.velocity.X;
						collideX = true;
					}
					if (base.velocity.Y != velocity.Y)
					{
						vector.Y = base.velocity.Y;
						collideY = true;
					}
					oldPosition = base.position;
					base.position += vector;
				}
				else
				{
					if (type == 72)
					{
						Vector2 position = new Vector2(base.position.X + (float)(width / 2), base.position.Y + (float)(height / 2));
						int num16 = 12;
						int num17 = 12;
						position.X -= num16 / 2;
						position.Y -= num17 / 2;
						base.velocity = Collision.TileCollision(position, base.velocity, num16, num17, fallThrough: true, fall2: true);
					}
					else
					{
						base.velocity = Collision.TileCollision(base.position, base.velocity, width, height, flag4, flag4);
					}
					if (Collision.up)
					{
						base.velocity.Y = 0.01f;
					}
					if (oldVelocity.X != base.velocity.X)
					{
						collideX = true;
					}
					if (oldVelocity.Y != base.velocity.Y)
					{
						collideY = true;
					}
					oldPosition = base.position;
					base.position += base.velocity;
				}
			}
			else
			{
				oldPosition = base.position;
				base.position += base.velocity;
			}
			if (Main.netMode != 1 && !noTileCollide && lifeMax > 1 && Collision.SwitchTiles(base.position, width, height, oldPosition) && type == 46)
			{
				ai[0] = 1f;
				ai[1] = 400f;
				ai[2] = 0f;
			}
			if (!active)
			{
				netUpdate = true;
			}
			if (Main.netMode == 2)
			{
				if (townNPC)
				{
					netSpam = 0;
				}
				if (netUpdate2)
				{
					netUpdate = true;
				}
				if (!active)
				{
					netSpam = 0;
				}
				if (netUpdate)
				{
					if (netSpam <= 180)
					{
						netSpam += 60;
						NetMessage.SendData(23, -1, -1, "", i);
						netUpdate2 = false;
					}
					else
					{
						netUpdate2 = true;
					}
				}
				if (netSpam > 0)
				{
					netSpam--;
				}
				if (active && townNPC && TypeToNum(type) != -1)
				{
					if (homeless != oldHomeless || homeTileX != oldHomeTileX || homeTileY != oldHomeTileY)
					{
						int num18 = 0;
						if (homeless)
						{
							num18 = 1;
						}
						NetMessage.SendData(60, -1, -1, "", i, Main.npc[i].homeTileX, Main.npc[i].homeTileY, num18);
					}
					oldHomeless = homeless;
					oldHomeTileX = homeTileX;
					oldHomeTileY = homeTileY;
				}
			}
			FindFrame();
			CheckActive();
			netUpdate = false;
			justHit = false;
			if (type == 120 || type == 137 || type == 138)
			{
				for (int num19 = oldPos.Length - 1; num19 > 0; num19--)
				{
					oldPos[num19] = oldPos[num19 - 1];
					Lighting.addLight((int)base.position.X / 16, (int)base.position.Y / 16, 0.3f, 0f, 0.2f);
				}
				oldPos[0] = base.position;
			}
			else if (type == 94)
			{
				for (int num20 = oldPos.Length - 1; num20 > 0; num20--)
				{
					oldPos[num20] = oldPos[num20 - 1];
				}
				oldPos[0] = base.position;
			}
			else if (type == 125 || type == 126 || type == 127 || type == 128 || type == 129 || type == 130 || type == 131 || type == 139 || type == 140)
			{
				for (int num21 = oldPos.Length - 1; num21 > 0; num21--)
				{
					oldPos[num21] = oldPos[num21 - 1];
				}
				oldPos[0] = base.position;
			}
		}

		public Color GetAlpha(Color newColor)
		{
			float num = (float)(255 - alpha) / 255f;
			int num2 = (int)((float)(int)newColor.R * num);
			int num3 = (int)((float)(int)newColor.G * num);
			int num4 = (int)((float)(int)newColor.B * num);
			int num5 = newColor.A - alpha;
			if (type == 25 || type == 30 || type == 33 || type == 59 || type == 60)
			{
				return new Color(200, 200, 200, 0);
			}
			if (type == 72)
			{
				num2 = newColor.R;
				num3 = newColor.G;
				num4 = newColor.B;
			}
			else if (type == 64 || type == 63 || type == 75 || type == 103)
			{
				num2 = (int)((double)(int)newColor.R * 1.5);
				num3 = (int)((double)(int)newColor.G * 1.5);
				num4 = (int)((double)(int)newColor.B * 1.5);
				if (num2 > 255)
				{
					num2 = 255;
				}
				if (num3 > 255)
				{
					num3 = 255;
				}
				if (num4 > 255)
				{
					num4 = 255;
				}
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			return new Color(num2, num3, num4, num5);
		}

		public Color GetColor(Color newColor)
		{
			int num = color.R - (255 - newColor.R);
			int num2 = color.G - (255 - newColor.G);
			int num3 = color.B - (255 - newColor.B);
			int num4 = color.A - (255 - newColor.A);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			return new Color(num, num2, num3, num4);
		}

		public string GetChat()
		{
			Recipe.FindRecipes();
			_ = Main.chrName[18];
			_ = Main.chrName[17];
			_ = Main.chrName[19];
			_ = Main.chrName[20];
			_ = Main.chrName[38];
			_ = Main.chrName[54];
			_ = Main.chrName[22];
			_ = Main.chrName[108];
			_ = Main.chrName[107];
			_ = Main.chrName[124];
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active)
				{
					if (Main.npc[i].type == 17)
					{
						flag = true;
					}
					else if (Main.npc[i].type == 18)
					{
						flag2 = true;
					}
					else if (Main.npc[i].type == 19)
					{
						flag3 = true;
					}
					else if (Main.npc[i].type == 20)
					{
						flag4 = true;
					}
					else if (Main.npc[i].type == 37)
					{
						flag5 = true;
					}
					else if (Main.npc[i].type == 38)
					{
						flag6 = true;
					}
					else if (Main.npc[i].type == 124)
					{
						flag7 = true;
					}
					else if (Main.npc[i].type == 107)
					{
						flag8 = true;
					}
					else if (Main.npc[i].type == 2)
					{
						flag9 = true;
					}
				}
			}
			string result = "";
			if (RunMethod("DominantChat"))
			{
				string result2 = (string)Codable.customMethodReturn;
				if (!string.IsNullOrEmpty(result2))
				{
					return result2;
				}
			}
			if (RunMethod("Chat"))
			{
				string result3 = (string)Codable.customMethodReturn;
				if (!string.IsNullOrEmpty(result3))
				{
					return result3;
				}
			}
			if (type == 17)
			{
				if (!downedBoss1 && Main.rand.Next(3) == 0)
				{
					result = ((Main.player[Main.myPlayer].statLifeMax2 < 200) ? Lang.dialog(1) : ((Main.player[Main.myPlayer].statDefense > 10) ? Lang.dialog(3) : Lang.dialog(2)));
				}
				else if (Main.dayTime)
				{
					if (Main.time < 16200.0)
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							result = Lang.dialog(4);
							break;
						case 1:
							result = Lang.dialog(5);
							break;
						default:
							result = Lang.dialog(6);
							break;
						}
					}
					else if (Main.time > 37800.0)
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							result = Lang.dialog(7);
							break;
						case 1:
							result = Lang.dialog(8);
							break;
						default:
							result = Lang.dialog(9);
							break;
						}
					}
					else
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							result = Lang.dialog(10);
							break;
						case 1:
							result = Lang.dialog(11);
							break;
						default:
							result = Lang.dialog(12);
							break;
						}
					}
				}
				else if (Main.bloodMoon)
				{
					if (flag2 && flag7 && Main.rand.Next(3) == 0)
					{
						result = Lang.dialog(13);
					}
					else
					{
						switch (Main.rand.Next(4))
						{
						case 0:
							result = Lang.dialog(14);
							break;
						case 1:
							result = Lang.dialog(15);
							break;
						case 2:
							result = Lang.dialog(16);
							break;
						default:
							result = Lang.dialog(17);
							break;
						}
					}
				}
				else if (Main.time < 9720.0)
				{
					result = ((Main.rand.Next(2) != 0) ? Lang.dialog(19) : Lang.dialog(18));
				}
				else if (Main.time > 22680.0)
				{
					result = ((Main.rand.Next(2) != 0) ? Lang.dialog(21) : Lang.dialog(20));
				}
				else
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(22);
						break;
					case 1:
						result = Lang.dialog(23);
						break;
					default:
						result = Lang.dialog(24);
						break;
					}
				}
			}
			else if (type == 18)
			{
				if (Main.bloodMoon)
				{
					if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax2 * 0.66)
					{
						switch (Main.rand.Next(3))
						{
						case 0:
							result = Lang.dialog(25);
							break;
						case 1:
							result = Lang.dialog(26);
							break;
						default:
							result = Lang.dialog(27);
							break;
						}
					}
					else
					{
						switch (Main.rand.Next(4))
						{
						case 0:
							result = Lang.dialog(28);
							break;
						case 1:
							result = Lang.dialog(29);
							break;
						case 2:
							result = Lang.dialog(30);
							break;
						default:
							result = Lang.dialog(31);
							break;
						}
					}
				}
				else if (Main.rand.Next(3) == 0 && !downedBoss3)
				{
					result = Lang.dialog(32);
				}
				else if (flag6 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(33);
				}
				else if (flag3 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(34);
				}
				else if (flag9 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(35);
				}
				else if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax2 * 0.33)
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(36);
						break;
					case 1:
						result = Lang.dialog(37);
						break;
					case 2:
						result = Lang.dialog(38);
						break;
					case 3:
						result = Lang.dialog(39);
						break;
					default:
						result = Lang.dialog(40);
						break;
					}
				}
				else if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax2 * 0.66)
				{
					switch (Main.rand.Next(7))
					{
					case 0:
						result = Lang.dialog(41);
						break;
					case 1:
						result = Lang.dialog(42);
						break;
					case 2:
						result = Lang.dialog(43);
						break;
					case 3:
						result = Lang.dialog(44);
						break;
					case 4:
						result = Lang.dialog(45);
						break;
					case 5:
						result = Lang.dialog(46);
						break;
					default:
						result = Lang.dialog(47);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(48);
						break;
					case 1:
						result = Lang.dialog(49);
						break;
					case 2:
						result = Lang.dialog(50);
						break;
					default:
						result = Lang.dialog(51);
						break;
					}
				}
			}
			else if (type == 19)
			{
				if (downedBoss3 && !Main.hardMode)
				{
					result = Lang.dialog(58);
				}
				else if (flag2 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(59);
				}
				else if (flag2 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(60);
				}
				else if (flag4 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(61);
				}
				else if (flag6 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(62);
				}
				else if (flag6 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(63);
				}
				else if (Main.bloodMoon)
				{
					result = ((Main.rand.Next(2) != 0) ? Lang.dialog(65) : Lang.dialog(64));
				}
				else
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(66);
						break;
					case 1:
						result = Lang.dialog(67);
						break;
					default:
						result = Lang.dialog(68);
						break;
					}
				}
			}
			else if (type == 20)
			{
				if (!downedBoss2 && Main.rand.Next(3) == 0)
				{
					result = Lang.dialog(69);
				}
				else if (flag3 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(70);
				}
				else if (flag && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(71);
				}
				else if (flag5 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(72);
				}
				else if (Main.bloodMoon)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(73);
						break;
					case 1:
						result = Lang.dialog(74);
						break;
					case 2:
						result = Lang.dialog(75);
						break;
					default:
						result = Lang.dialog(76);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(77);
						break;
					case 1:
						result = Lang.dialog(78);
						break;
					case 2:
						result = Lang.dialog(79);
						break;
					case 3:
						result = Lang.dialog(80);
						break;
					default:
						result = Lang.dialog(81);
						break;
					}
				}
			}
			else if (type == 37)
			{
				if (Main.dayTime)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(82);
						break;
					case 1:
						result = Lang.dialog(83);
						break;
					default:
						result = Lang.dialog(84);
						break;
					}
				}
				else if (Main.player[Main.myPlayer].statLifeMax2 < 300 || Main.player[Main.myPlayer].statDefense < 10)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(85);
						break;
					case 1:
						result = Lang.dialog(86);
						break;
					case 2:
						result = Lang.dialog(87);
						break;
					default:
						result = Lang.dialog(88);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(89);
						break;
					case 1:
						result = Lang.dialog(90);
						break;
					case 2:
						result = Lang.dialog(91);
						break;
					default:
						result = Lang.dialog(92);
						break;
					}
				}
			}
			else if (type == 38)
			{
				if (!downedBoss2 && Main.rand.Next(3) == 0)
				{
					result = Lang.dialog(93);
				}
				if (Main.bloodMoon)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(94);
						break;
					case 1:
						result = Lang.dialog(95);
						break;
					default:
						result = Lang.dialog(96);
						break;
					}
				}
				else if (flag3 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(97);
				}
				else if (flag3 && Main.rand.Next(5) == 0)
				{
					result = Lang.dialog(98);
				}
				else if (flag2 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(99);
				}
				else if (flag4 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(100);
				}
				else if (!Main.dayTime)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(101);
						break;
					case 1:
						result = Lang.dialog(102);
						break;
					case 2:
						result = Lang.dialog(103);
						break;
					default:
						result = Lang.dialog(104);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(105);
						break;
					case 1:
						result = Lang.dialog(106);
						break;
					case 2:
						result = Lang.dialog(107);
						break;
					case 3:
						result = Lang.dialog(108);
						break;
					default:
						result = Lang.dialog(109);
						break;
					}
				}
			}
			else if (type == 54)
			{
				if (!flag7 && Main.rand.Next(2) == 0)
				{
					result = Lang.dialog(110);
				}
				else if (Main.bloodMoon)
				{
					result = Lang.dialog(111);
				}
				else if (flag2 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(112);
				}
				else if (Main.player[Main.myPlayer].head == 24)
				{
					result = Lang.dialog(113);
				}
				else
				{
					switch (Main.rand.Next(6))
					{
					case 0:
						result = Lang.dialog(114);
						break;
					case 1:
						result = Lang.dialog(115);
						break;
					case 2:
						result = Lang.dialog(116);
						break;
					case 3:
						result = Lang.dialog(117);
						break;
					case 4:
						result = Lang.dialog(118);
						break;
					default:
						result = Lang.dialog(119);
						break;
					}
				}
			}
			else if (type == 105)
			{
				result = Lang.dialog(120);
			}
			else if (type == 107)
			{
				if (homeless)
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(121);
						break;
					case 1:
						result = Lang.dialog(122);
						break;
					case 2:
						result = Lang.dialog(123);
						break;
					case 3:
						result = Lang.dialog(124);
						break;
					default:
						result = Lang.dialog(125);
						break;
					}
				}
				else if (flag7 && Main.rand.Next(4) == 0)
				{
					result = Lang.dialog(126);
				}
				else if (!Main.dayTime)
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(127);
						break;
					case 1:
						result = Lang.dialog(128);
						break;
					case 2:
						result = Lang.dialog(129);
						break;
					case 3:
						result = Lang.dialog(130);
						break;
					default:
						result = Lang.dialog(131);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(132);
						break;
					case 1:
						result = Lang.dialog(133);
						break;
					case 2:
						result = Lang.dialog(134);
						break;
					case 3:
						result = Lang.dialog(135);
						break;
					default:
						result = Lang.dialog(136);
						break;
					}
				}
			}
			else if (type == 106)
			{
				result = Lang.dialog(137);
			}
			else if (type == 108)
			{
				if (homeless)
				{
					int num = Main.rand.Next(3);
					if (num == 0)
					{
						result = Lang.dialog(138);
					}
					else if (num == 1 && !Main.player[Main.myPlayer].male)
					{
						result = Lang.dialog(139);
					}
					else
					{
						switch (num)
						{
						case 1:
							result = Lang.dialog(140);
							break;
						case 2:
							result = Lang.dialog(141);
							break;
						}
					}
				}
				else if (Main.player[Main.myPlayer].male && flag9 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(142);
				}
				else if (Main.player[Main.myPlayer].male && flag6 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(143);
				}
				else if (Main.player[Main.myPlayer].male && flag8 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(144);
				}
				else if (!Main.player[Main.myPlayer].male && flag2 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(145);
				}
				else if (!Main.player[Main.myPlayer].male && flag7 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(146);
				}
				else if (!Main.player[Main.myPlayer].male && flag4 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(147);
				}
				else if (!Main.dayTime)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(148);
						break;
					case 1:
						result = Lang.dialog(149);
						break;
					case 2:
						result = Lang.dialog(150);
						break;
					}
				}
				else
				{
					switch (Main.rand.Next(5))
					{
					case 0:
						result = Lang.dialog(151);
						break;
					case 1:
						result = Lang.dialog(152);
						break;
					case 2:
						result = Lang.dialog(153);
						break;
					case 3:
						result = Lang.dialog(154);
						break;
					default:
						result = Lang.dialog(155);
						break;
					}
				}
			}
			else if (type == 123)
			{
				result = Lang.dialog(156);
			}
			else if (type == 124)
			{
				if (homeless)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(157);
						break;
					case 1:
						result = Lang.dialog(158);
						break;
					case 2:
						result = Lang.dialog(159);
						break;
					default:
						result = Lang.dialog(160);
						break;
					}
				}
				else if (Main.bloodMoon)
				{
					switch (Main.rand.Next(4))
					{
					case 0:
						result = Lang.dialog(161);
						break;
					case 1:
						result = Lang.dialog(162);
						break;
					case 2:
						result = Lang.dialog(163);
						break;
					default:
						result = Lang.dialog(164);
						break;
					}
				}
				else if (flag8 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(165);
				}
				else if (flag3 && Main.rand.Next(6) == 0)
				{
					result = Lang.dialog(166);
				}
				else
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(167);
						break;
					case 1:
						result = Lang.dialog(168);
						break;
					default:
						result = Lang.dialog(169);
						break;
					}
				}
			}
			else if (type == 22)
			{
				if (Main.bloodMoon)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(170);
						break;
					case 1:
						result = Lang.dialog(171);
						break;
					default:
						result = Lang.dialog(172);
						break;
					}
				}
				else if (!Main.dayTime)
				{
					result = Lang.dialog(173);
				}
				else
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						result = Lang.dialog(174);
						break;
					case 1:
						result = Lang.dialog(175);
						break;
					default:
						result = Lang.dialog(176);
						break;
					}
				}
			}
			else if (type == 142)
			{
				switch (Main.rand.Next(3))
				{
				case 0:
					result = Lang.dialog(224);
					break;
				case 1:
					result = Lang.dialog(225);
					break;
				case 2:
					result = Lang.dialog(226);
					break;
				}
			}
			return result;
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static void OldSpawnNPC()
		{
			if (noSpawnCycle)
			{
				noSpawnCycle = false;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num3++;
				}
			}
			int num4 = 0;
			bool flag3;
			bool flag4;
			bool flag5;
			bool flag6;
			while (true)
			{
				if (num4 >= 255)
				{
					return;
				}
				if (Main.player[num4].active && !Main.player[num4].dead)
				{
					flag3 = false;
					flag4 = false;
					flag5 = false;
					if (Main.player[num4].active && Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0 && (double)Main.player[num4].position.Y < Main.worldSurface * 16.0 + (double)sHeight)
					{
						int num5 = 3000;
						if ((double)Main.player[num4].position.X > Main.invasionX * 16.0 - (double)num5 && (double)Main.player[num4].position.X < Main.invasionX * 16.0 + (double)num5)
						{
							flag4 = true;
						}
					}
					flag = false;
					spawnRate = defaultSpawnRate;
					maxSpawns = defaultMaxSpawns;
					if (Main.hardMode)
					{
						spawnRate = (int)((double)defaultSpawnRate * 0.9);
						maxSpawns = defaultMaxSpawns + 1;
					}
					if (Main.player[num4].position.Y > (float)((Main.maxTilesY - 200) * 16))
					{
						maxSpawns = (int)((float)maxSpawns * 2f);
					}
					else if ((double)Main.player[num4].position.Y > Main.rockLayer * 16.0 + (double)sHeight)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.9f);
					}
					else if ((double)Main.player[num4].position.Y > Main.worldSurface * 16.0 + (double)sHeight)
					{
						if (Main.hardMode)
						{
							spawnRate = (int)((double)spawnRate * 0.45);
							maxSpawns = (int)((float)maxSpawns * 1.8f);
						}
						else
						{
							spawnRate = (int)((double)spawnRate * 0.5);
							maxSpawns = (int)((float)maxSpawns * 1.7f);
						}
					}
					else if (!Main.dayTime)
					{
						spawnRate = (int)((double)spawnRate * 0.6);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
						if (Main.bloodMoon)
						{
							spawnRate = (int)((double)spawnRate * 0.3);
							maxSpawns = (int)((float)maxSpawns * 1.8f);
						}
					}
					if (Main.player[num4].zoneDungeon)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.7f);
					}
					else if (Main.player[num4].zoneJungle)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.5f);
					}
					else if (Main.player[num4].zoneEvil)
					{
						spawnRate = (int)((double)spawnRate * 0.65);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
					}
					else if (Main.player[num4].zoneMeteor)
					{
						spawnRate = (int)((double)spawnRate * 0.4);
						maxSpawns = (int)((float)maxSpawns * 1.1f);
					}
					if (Main.player[num4].zoneHoly && (double)Main.player[num4].position.Y > Main.rockLayer * 16.0 + (double)sHeight)
					{
						spawnRate = (int)((double)spawnRate * 0.65);
						maxSpawns = (int)((float)maxSpawns * 1.3f);
					}
					if (Main.wof >= 0 && Main.player[num4].position.Y > (float)((Main.maxTilesY - 200) * 16))
					{
						maxSpawns = (int)((float)maxSpawns * 0.3f);
						spawnRate *= 3;
					}
					if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.2)
					{
						spawnRate = (int)((float)spawnRate * 0.6f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.4)
					{
						spawnRate = (int)((float)spawnRate * 0.7f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.6)
					{
						spawnRate = (int)((float)spawnRate * 0.8f);
					}
					else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.8)
					{
						spawnRate = (int)((float)spawnRate * 0.9f);
					}
					if ((double)(Main.player[num4].position.Y * 16f) > (Main.worldSurface + Main.rockLayer) / 2.0 || Main.player[num4].zoneEvil)
					{
						if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.2)
						{
							spawnRate = (int)((float)spawnRate * 0.7f);
						}
						else if ((double)Main.player[num4].activeNPCs < (double)maxSpawns * 0.4)
						{
							spawnRate = (int)((float)spawnRate * 0.9f);
						}
					}
					if (Main.player[num4].inventory[Main.player[num4].selectedItem].type == 148)
					{
						spawnRate = (int)((double)spawnRate * 0.75);
						maxSpawns = (int)((float)maxSpawns * 1.5f);
					}
					if (Main.player[num4].enemySpawns)
					{
						spawnRate = (int)((double)spawnRate * 0.5);
						maxSpawns = (int)((float)maxSpawns * 2f);
					}
					if ((double)spawnRate < (double)defaultSpawnRate * 0.1)
					{
						spawnRate = (int)((double)defaultSpawnRate * 0.1);
					}
					if (maxSpawns > defaultMaxSpawns * 3)
					{
						maxSpawns = defaultMaxSpawns * 3;
					}
					if (flag4)
					{
						maxSpawns = (int)((double)defaultMaxSpawns * (2.0 + 0.3 * (double)num3));
						spawnRate = 20;
					}
					if (Main.player[num4].zoneDungeon && !downedBoss3)
					{
						spawnRate = 10;
					}
					flag6 = false;
					if (!flag4 && (!Main.bloodMoon || Main.dayTime) && !Main.player[num4].zoneDungeon && !Main.player[num4].zoneEvil && !Main.player[num4].zoneMeteor)
					{
						if (Main.player[num4].townNPCs == 1f)
						{
							flag3 = true;
							if (Main.rand.Next(3) <= 1)
							{
								flag6 = true;
								maxSpawns = (int)((double)(float)maxSpawns * 0.6);
							}
							else
							{
								spawnRate = (int)((float)spawnRate * 2f);
							}
						}
						else if (Main.player[num4].townNPCs == 2f)
						{
							flag3 = true;
							if (Main.rand.Next(3) == 0)
							{
								flag6 = true;
								maxSpawns = (int)((double)(float)maxSpawns * 0.6);
							}
							else
							{
								spawnRate = (int)((float)spawnRate * 3f);
							}
						}
						else if (Main.player[num4].townNPCs >= 3f)
						{
							flag3 = true;
							flag6 = true;
							maxSpawns = (int)((double)(float)maxSpawns * 0.6);
						}
					}
					TMod.RunMethod(TMod.GenericHooks.UpdateSpawn);
					if (Main.player[num4].active && !Main.player[num4].dead && Main.player[num4].activeNPCs < (float)maxSpawns && Main.rand.Next(spawnRate) == 0)
					{
						int num6 = (int)(Main.player[num4].position.X / 16f) - spawnRangeX;
						int num7 = (int)(Main.player[num4].position.X / 16f) + spawnRangeX;
						int num8 = (int)(Main.player[num4].position.Y / 16f) - spawnRangeY;
						int num9 = (int)(Main.player[num4].position.Y / 16f) + spawnRangeY;
						int num10 = (int)(Main.player[num4].position.X / 16f) - safeRangeX;
						int num11 = (int)(Main.player[num4].position.X / 16f) + safeRangeX;
						int num12 = (int)(Main.player[num4].position.Y / 16f) - safeRangeY;
						int num13 = (int)(Main.player[num4].position.Y / 16f) + safeRangeY;
						if (num6 < 0)
						{
							num6 = 0;
						}
						if (num7 > Main.maxTilesX)
						{
							num7 = Main.maxTilesX;
						}
						if (num8 < 0)
						{
							num8 = 0;
						}
						if (num9 > Main.maxTilesY)
						{
							num9 = Main.maxTilesY;
						}
						for (int j = 0; j < 50; j++)
						{
							int num14 = Main.rand.Next(num6, num7);
							int num15 = Main.rand.Next(num8, num9);
							if (!Main.tile[num14, num15].active || !Main.tileSolid[Main.tile[num14, num15].type])
							{
								if (Main.wallHouse[Main.tile[num14, num15].wall])
								{
									continue;
								}
								if (!flag4 && (double)num15 < Main.worldSurface * 0.34999999403953552 && !flag6 && ((double)num14 < (double)Main.maxTilesX * 0.45 || (double)num14 > (double)Main.maxTilesX * 0.55 || Main.hardMode))
								{
									num = num14;
									num2 = num15;
									flag = true;
									flag2 = true;
								}
								else if (!flag4 && (double)num15 < Main.worldSurface * 0.44999998807907104 && !flag6 && Main.hardMode && Main.rand.Next(10) == 0)
								{
									num = num14;
									num2 = num15;
									flag = true;
									flag2 = true;
								}
								else
								{
									for (int k = num15; k < Main.maxTilesY; k++)
									{
										if (Main.tile[num14, k].active && Main.tileSolid[Main.tile[num14, k].type])
										{
											if (num14 < num10 || num14 > num11 || k < num12 || k > num13)
											{
												num = num14;
												num2 = k;
												flag = true;
											}
											break;
										}
									}
								}
								if (flag)
								{
									int num16 = num - spawnSpaceX / 2;
									int num17 = num + spawnSpaceX / 2;
									int num18 = num2 - spawnSpaceY;
									int num19 = num2;
									if (num16 < 0)
									{
										flag = false;
									}
									if (num17 > Main.maxTilesX)
									{
										flag = false;
									}
									if (num18 < 0)
									{
										flag = false;
									}
									if (num19 > Main.maxTilesY)
									{
										flag = false;
									}
									if (flag)
									{
										for (int l = num16; l < num17; l++)
										{
											for (int m = num18; m < num19; m++)
											{
												if (Main.tile[l, m].active && Main.tileSolid[Main.tile[l, m].type])
												{
													flag = false;
													break;
												}
												if (Main.tile[l, m].lava)
												{
													flag = false;
													break;
												}
											}
										}
									}
								}
							}
							if (flag || flag)
							{
								break;
							}
						}
					}
					if (flag)
					{
						Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
						for (int n = 0; n < 255; n++)
						{
							if (Main.player[n].active)
							{
								Rectangle rectangle2 = new Rectangle((int)(Main.player[n].position.X + (float)(Main.player[n].width / 2) - (float)(sWidth / 2) - (float)safeRangeX), (int)(Main.player[n].position.Y + (float)(Main.player[n].height / 2) - (float)(sHeight / 2) - (float)safeRangeY), sWidth + safeRangeX * 2, sHeight + safeRangeY * 2);
								if (rectangle.Intersects(rectangle2))
								{
									flag = false;
								}
							}
						}
					}
					if (flag)
					{
						if (Main.player[num4].zoneDungeon && (!Main.tileDungeon[Main.tile[num, num2].type] || Main.tile[num, num2 - 1].wall == 0))
						{
							flag = false;
						}
						if (Main.tile[num, num2 - 1].liquid > 0 && Main.tile[num, num2 - 2].liquid > 0 && !Main.tile[num, num2 - 1].lava)
						{
							flag5 = true;
						}
					}
					if (flag)
					{
						break;
					}
				}
				num4++;
			}
			flag = false;
			int num20 = Main.tile[num, num2].type;
			int num21 = 200;
			bool flag7 = false;
			ArrayList arrayList = new ArrayList();
			for (int num22 = 1; num22 < Config.npcDefs.GetSize(); num22++)
			{
				if (Config.npcDefs.assemblyByType[num22] != null)
				{
					Assembly assembly = Config.npcDefs.assemblyByType[num22];
					object code = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Config.npcDefs[num22].name) + "NPC");
					if (Codable.RunSpecifiedMethod("NPC " + Config.npcDefs[num22].name, code, "SpawnNPC", num, num2, num4) && (bool)Codable.customMethodReturn)
					{
						arrayList.Add(num22);
					}
				}
			}
			if (arrayList.Count > 0)
			{
				int index = Main.rand.Next(arrayList.Count);
				flag7 = true;
				num21 = NewNPC(num * 16 + 8, num2 * 16, (int)arrayList[index]);
			}
			if (!flag7)
			{
				if (flag2)
				{
					if (Main.hardMode && Main.rand.Next(10) == 0 && !AnyNPCs(87))
					{
						NewNPC(num * 16 + 8, num2 * 16, 87, 1);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 48);
					}
				}
				else if (flag4)
				{
					if (Main.invasionType == 1)
					{
						if (Main.rand.Next(9) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 29);
						}
						else if (Main.rand.Next(5) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 26);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 111);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 27);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 28);
						}
					}
					else if (Main.invasionType == 2)
					{
						if (Main.rand.Next(7) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 145);
						}
						else if (Main.rand.Next(3) == 0)
						{
							NewNPC(num * 16 + 8, num2 * 16, 143);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 144);
						}
					}
				}
				else if (flag5 && (num < 250 || num > Main.maxTilesX - 250) && num20 == 53 && (double)num2 < Main.rockLayer)
				{
					if (Main.rand.Next(8) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 65);
					}
					if (Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 67);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 64);
					}
				}
				else if (flag5 && (((double)num2 > Main.rockLayer && Main.rand.Next(2) == 0) || num20 == 60))
				{
					if (Main.hardMode && Main.rand.Next(3) > 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 102);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 58);
					}
				}
				else if (flag5 && (double)num2 > Main.worldSurface && Main.rand.Next(3) == 0)
				{
					if (Main.hardMode)
					{
						NewNPC(num * 16 + 8, num2 * 16, 103);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 63);
					}
				}
				else if (flag5 && Main.rand.Next(4) == 0)
				{
					if (Main.player[num4].zoneEvil)
					{
						NewNPC(num * 16 + 8, num2 * 16, 57);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 55);
					}
				}
				else if (downedGoblins && Main.rand.Next(20) == 0 && !flag5 && (double)num2 >= Main.rockLayer && num2 < Main.maxTilesY - 210 && !savedGoblin && !AnyNPCs(105))
				{
					NewNPC(num * 16 + 8, num2 * 16, 105);
				}
				else if (Main.hardMode && Main.rand.Next(20) == 0 && !flag5 && (double)num2 >= Main.rockLayer && num2 < Main.maxTilesY - 210 && !savedWizard && !AnyNPCs(106))
				{
					NewNPC(num * 16 + 8, num2 * 16, 106);
				}
				else if (flag6)
				{
					if (flag5)
					{
						NewNPC(num * 16 + 8, num2 * 16, 55);
					}
					else
					{
						if (num20 != 2 && num20 != 109 && num20 != 147 && (double)num2 <= Main.worldSurface)
						{
							return;
						}
						if (Main.rand.Next(2) == 0 && (double)num2 <= Main.worldSurface)
						{
							NewNPC(num * 16 + 8, num2 * 16, 74);
						}
						else
						{
							NewNPC(num * 16 + 8, num2 * 16, 46);
						}
					}
				}
				else if (Main.player[num4].zoneDungeon)
				{
					if (!downedBoss3)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 68);
					}
					else if (!savedMech && Main.rand.Next(5) == 0 && !flag5 && !AnyNPCs(123) && (double)num2 > Main.rockLayer)
					{
						NewNPC(num * 16 + 8, num2 * 16, 123);
					}
					else if (Main.rand.Next(37) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 71);
					}
					else if (Main.rand.Next(4) == 0 && !NearSpikeBall(num, num2))
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 70);
					}
					else if (Main.rand.Next(15) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 72);
					}
					else if (Main.rand.Next(9) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 34);
					}
					else if (Main.rand.Next(7) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 32);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 31);
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Big Boned");
						}
						else if (Main.rand.Next(5) == 0)
						{
							Main.npc[num21].SetDefaults("Short Bones");
						}
					}
				}
				else if (Main.player[num4].zoneMeteor)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 23);
				}
				else if (Main.player[num4].zoneEvil && Main.rand.Next(65) == 0)
				{
					num21 = ((!Main.hardMode || Main.rand.Next(4) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 7, 1) : NewNPC(num * 16 + 8, num2 * 16, 98, 1));
				}
				else if (Main.hardMode && (double)num2 > Main.worldSurface && Main.rand.Next(75) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 85);
				}
				else if (Main.hardMode && Main.tile[num, num2 - 1].wall == 2 && Main.rand.Next(20) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 85);
				}
				else if (Main.hardMode && (double)num2 <= Main.worldSurface && !Main.dayTime && (Main.rand.Next(20) == 0 || (Main.rand.Next(5) == 0 && Main.moonPhase == 4)))
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 82);
				}
				else if (num20 == 60 && Main.rand.Next(500) == 0 && !Main.dayTime)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 52);
				}
				else if (num20 == 60 && (double)num2 > (Main.worldSurface + Main.rockLayer) / 2.0)
				{
					if (Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 43);
						Main.npc[num21].ai[0] = num;
						Main.npc[num21].ai[1] = num2;
						Main.npc[num21].netUpdate = true;
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 42);
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Little Stinger");
						}
						else if (Main.rand.Next(4) == 0)
						{
							Main.npc[num21].SetDefaults("Big Stinger");
						}
					}
				}
				else if (num20 == 60 && Main.rand.Next(4) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 51);
				}
				else if (num20 == 60 && Main.rand.Next(8) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 56);
					Main.npc[num21].ai[0] = num;
					Main.npc[num21].ai[1] = num2;
					Main.npc[num21].netUpdate = true;
				}
				else if (Main.hardMode && num20 == 53 && Main.rand.Next(3) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 78);
				}
				else if (Main.hardMode && num20 == 112 && Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 79);
				}
				else if (Main.hardMode && num20 == 116 && Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 80);
				}
				else if (Main.hardMode && !flag5 && (double)num2 < Main.rockLayer && (num20 == 116 || num20 == 117 || num20 == 109))
				{
					num21 = ((!Main.dayTime && Main.rand.Next(2) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 122) : ((Main.rand.Next(10) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 75) : NewNPC(num * 16 + 8, num2 * 16, 86)));
				}
				else if (!flag3 && Main.hardMode && Main.rand.Next(50) == 0 && !flag5 && (double)num2 >= Main.rockLayer && (num20 == 116 || num20 == 117 || num20 == 109))
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 84);
				}
				else if ((num20 == 22 && Main.player[num4].zoneEvil) || num20 == 23 || num20 == 25 || num20 == 112)
				{
					if (Main.hardMode && (double)num2 >= Main.rockLayer && Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 101);
						Main.npc[num21].ai[0] = num;
						Main.npc[num21].ai[1] = num2;
						Main.npc[num21].netUpdate = true;
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						num21 = ((Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 81) : NewNPC(num * 16 + 8, num2 * 16, 121));
					}
					else if (Main.hardMode && (double)num2 >= Main.rockLayer && Main.rand.Next(40) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 83);
					}
					else if (Main.hardMode && (Main.rand.Next(2) == 0 || (double)num2 > Main.rockLayer))
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 94);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 6);
						if (Main.rand.Next(3) == 0)
						{
							Main.npc[num21].SetDefaults("Little Eater");
						}
						else if (Main.rand.Next(3) == 0)
						{
							Main.npc[num21].SetDefaults("Big Eater");
						}
					}
				}
				else if ((double)num2 <= Main.worldSurface)
				{
					if (Main.dayTime)
					{
						int num23 = Math.Abs(num - Main.spawnTileX);
						if (num23 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num20 == 2 || num20 == 109 || num20 == 147))
						{
							NewNPC(num * 16 + 8, num2 * 16, 46);
						}
						else if (num23 < Main.maxTilesX / 3 && Main.rand.Next(15) == 0 && (num20 == 2 || num20 == 109 || num20 == 147))
						{
							NewNPC(num * 16 + 8, num2 * 16, 74);
						}
						else if (num23 > Main.maxTilesX / 3 && num20 == 2 && Main.rand.Next(300) == 0 && !AnyNPCs(50))
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 50);
						}
						else if (num20 == 53 && Main.rand.Next(5) == 0 && !flag5)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 69);
						}
						else if (num20 == 53 && !flag5)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 61);
						}
						else if (num23 > Main.maxTilesX / 3 && Main.rand.Next(15) == 0)
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 73);
						}
						else
						{
							num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
							if (num20 == 60)
							{
								Main.npc[num21].SetDefaults("Jungle Slime");
							}
							else if (Main.rand.Next(3) == 0 || num23 < 200)
							{
								Main.npc[num21].SetDefaults("Green Slime");
							}
							else if (Main.rand.Next(10) == 0 && num23 > 400)
							{
								Main.npc[num21].SetDefaults("Purple Slime");
							}
						}
					}
					else if (Main.rand.Next(6) == 0 || (Main.moonPhase == 4 && Main.rand.Next(2) == 0))
					{
						num21 = ((!Main.hardMode || Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 2) : NewNPC(num * 16 + 8, num2 * 16, 133));
					}
					else if (Main.hardMode && Main.rand.Next(50) == 0 && Main.bloodMoon && !AnyNPCs(109))
					{
						NewNPC(num * 16 + 8, num2 * 16, 109);
					}
					else if (Main.rand.Next(250) == 0 && Main.bloodMoon)
					{
						NewNPC(num * 16 + 8, num2 * 16, 53);
					}
					else if (Main.moonPhase == 0 && Main.hardMode && Main.rand.Next(3) != 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 104);
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 140);
					}
					else if (Main.rand.Next(3) == 0)
					{
						NewNPC(num * 16 + 8, num2 * 16, 132);
					}
					else
					{
						NewNPC(num * 16 + 8, num2 * 16, 3);
					}
				}
				else if ((double)num2 <= Main.rockLayer)
				{
					if (!flag3 && Main.rand.Next(50) == 0)
					{
						num21 = ((!Main.hardMode) ? NewNPC(num * 16 + 8, num2 * 16, 10, 1) : NewNPC(num * 16 + 8, num2 * 16, 95, 1));
					}
					else if (Main.hardMode && Main.rand.Next(3) == 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 140);
					}
					else if (Main.hardMode && Main.rand.Next(4) != 0)
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 141);
					}
					else
					{
						num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
						if (Main.rand.Next(5) == 0)
						{
							Main.npc[num21].SetDefaults("Yellow Slime");
						}
						else if (Main.rand.Next(2) == 0)
						{
							Main.npc[num21].SetDefaults("Blue Slime");
						}
						else
						{
							Main.npc[num21].SetDefaults("Red Slime");
						}
					}
				}
				else if (num2 > Main.maxTilesY - 190)
				{
					num21 = ((Main.rand.Next(40) == 0 && !AnyNPCs(39)) ? NewNPC(num * 16 + 8, num2 * 16, 39, 1) : ((Main.rand.Next(14) == 0) ? NewNPC(num * 16 + 8, num2 * 16, 24) : ((Main.rand.Next(8) == 0) ? ((Main.rand.Next(7) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 62) : NewNPC(num * 16 + 8, num2 * 16, 66)) : ((Main.rand.Next(3) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 60) : NewNPC(num * 16 + 8, num2 * 16, 59)))));
				}
				else if ((num20 == 116 || num20 == 117) && !flag3 && Main.rand.Next(8) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 120);
				}
				else if (!flag3 && Main.rand.Next(75) == 0 && !Main.player[num4].zoneHoly)
				{
					num21 = ((!Main.hardMode) ? NewNPC(num * 16 + 8, num2 * 16, 10, 1) : NewNPC(num * 16 + 8, num2 * 16, 95, 1));
				}
				else if (!Main.hardMode && Main.rand.Next(10) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 16);
				}
				else if (!Main.hardMode && Main.rand.Next(4) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 1);
					if (Main.player[num4].zoneJungle)
					{
						Main.npc[num21].SetDefaults("Jungle Slime");
					}
					else
					{
						Main.npc[num21].SetDefaults("Black Slime");
					}
				}
				else if (Main.rand.Next(2) != 0)
				{
					num21 = ((Main.hardMode && (Main.player[num4].zoneHoly & (Main.rand.Next(2) == 0))) ? NewNPC(num * 16 + 8, num2 * 16, 138) : (Main.player[num4].zoneJungle ? NewNPC(num * 16 + 8, num2 * 16, 51) : ((Main.hardMode && Main.player[num4].zoneHoly) ? NewNPC(num * 16 + 8, num2 * 16, 137) : ((!Main.hardMode || Main.rand.Next(6) <= 0) ? NewNPC(num * 16 + 8, num2 * 16, 49) : NewNPC(num * 16 + 8, num2 * 16, 93)))));
				}
				else if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(700) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 45);
				}
				else if (!Main.hardMode || Main.rand.Next(10) == 0)
				{
					num21 = ((Main.rand.Next(15) != 0) ? NewNPC(num * 16 + 8, num2 * 16, 21) : NewNPC(num * 16 + 8, num2 * 16, 44));
				}
				else if (Main.rand.Next(2) == 0)
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 77);
					if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(5) == 0)
					{
						Main.npc[num21].SetDefaults("Heavy Skeleton");
					}
				}
				else
				{
					num21 = NewNPC(num * 16 + 8, num2 * 16, 110);
				}
			}
			if (Main.npc[num21].type == 1 && Main.rand.Next(250) == 0)
			{
				Main.npc[num21].SetDefaults("Pinky");
			}
			if (Main.netMode == 2 && num21 < 200)
			{
				NetMessage.SendData(23, -1, -1, "", num21);
			}
		}
	}
}
