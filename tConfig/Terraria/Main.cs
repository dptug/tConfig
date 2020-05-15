using Gajatko.IniFiles;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SevenZip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Terraria
{
	public class Main : Game
	{
		public const int worldRelease = 301;

		public const int MF_BYPOSITION = 1024;

		public const int sectionWidth = 200;

		public const int sectionHeight = 150;

		public const int maxTileSets = 150;

		public const int maxWallTypes = 32;

		public const int maxBackgrounds = 32;

		public const int maxDust = 2000;

		public const int maxCombatText = 100;

		public const int maxItemText = 20;

		public const int maxPlayers = 255;

		public const int maxChests = 1000;

		public const int maxItemTypes = 604;

		public const int maxItems = 200;

		public const int maxBuffs = 41;

		public const int maxProjectileTypes = 112;

		public const int maxProjectiles = 1000;

		public const int maxNPCTypes = 147;

		public const int maxNPCs = 200;

		public const int maxGoreTypes = 160;

		public const int maxGore = 200;

		public const int maxInventory = 48;

		public const int maxItemSounds = 37;

		public const int maxNPCHitSounds = 11;

		public const int maxNPCKilledSounds = 15;

		public const int maxLiquidTypes = 2;

		public const int maxMusic = 14;

		public const int numArmorHead = 45;

		public const int numArmorBody = 26;

		public const int numArmorLegs = 25;

		public const double dayLength = 54000.0;

		public const double nightLength = 32400.0;

		public const int maxStars = 130;

		public const int maxStarTypes = 5;

		public const int maxClouds = 100;

		public const int maxCloudTypes = 4;

		public const int maxHair = 36;

		public static int curRelease = 1379;

		public static string versionNumber = "v1.1.2 tConfig " + Constants.version + Constants.subVersion;

		public static string versionNumber2 = versionNumber;

		public static int errorHandleChoice = 0;

		public static string SavePath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Storage";

		public static string WorldPath = SavePath + Path.DirectorySeparatorChar + "Worlds" + Path.DirectorySeparatorChar + "tConfig";

		public static string worldTempPath = WorldPath + Path.DirectorySeparatorChar + "temp";

		public static string PlayerPath = SavePath + Path.DirectorySeparatorChar + "Players" + Path.DirectorySeparatorChar + "tConfig";

		public static string loadFailedMessage = "";

		public static bool savePlayerSnapshot = false;

		public static string npcChatText;

		public RenderTarget2D globalTarget;

		public static Recipe[] recipe;

		public static int[] availableRecipe;

		public static float[] availableRecipeY;

		public static int numAvailableRecipes;

		public static int focusRecipe;

		public static float[] colorShopCopper = new float[3]
		{
			246f,
			138f,
			96f
		};

		public static float[] colorShopSilver = new float[3]
		{
			181f,
			192f,
			193f
		};

		public static float[] colorShopGold = new float[3]
		{
			224f,
			201f,
			92f
		};

		public static float[] colorShopPlatinum = new float[3]
		{
			220f,
			220f,
			198f
		};

		public static int colorRarityMin = -1;

		public static int colorRarityMax = 6;

		public static Dictionary<int, float[]> colorRarity = new Dictionary<int, float[]>
		{
			{
				-1,
				new float[3]
				{
					130f,
					130f,
					130f
				}
			},
			{
				0,
				new float[3]
				{
					255f,
					255f,
					255f
				}
			},
			{
				1,
				new float[3]
				{
					150f,
					150f,
					255f
				}
			},
			{
				2,
				new float[3]
				{
					150f,
					255f,
					150f
				}
			},
			{
				3,
				new float[3]
				{
					255f,
					200f,
					150f
				}
			},
			{
				4,
				new float[3]
				{
					255f,
					150f,
					150f
				}
			},
			{
				5,
				new float[3]
				{
					255f,
					150f,
					255f
				}
			},
			{
				6,
				new float[3]
				{
					210f,
					160f,
					255f
				}
			}
		};

		public static bool creatingChar = false;

		public static bool skipMenu = false;

		public static bool verboseNetplay = false;

		public static bool stopTimeOuts = false;

		public static bool showSpam = false;

		public static bool showItemOwner = false;

		public static int oldTempLightCount = 0;

		public static int musicBox = -1;

		public static int musicBox2 = -1;

		public static bool cEd = false;

		public static float upTimer;

		public static float upTimerMax;

		public static float upTimerMaxDelay;

		public static float[] drawTimer = new float[10];

		public static float[] drawTimerMax = new float[10];

		public static float[] drawTimerMaxDelay = new float[10];

		public static float[] renderTimer = new float[10];

		public static float[] lightTimer = new float[10];

		public static bool drawDiag = false;

		public static bool drawRelease = false;

		public static bool renderNow = false;

		public static bool drawToScreen = false;

		public static bool targetSet = false;

		public static int mouseX;

		public static int mouseY;

		public static bool mouseLeft;

		public static bool mouseRight;

		public static float essScale = 1f;

		public static int essDir = -1;

		public static string debugWords = "";

		public static bool gamePad = false;

		public static bool xMas = false;

		public static int snowDust = 0;

		public static bool chTitle = false;

		public static int keyCount = 0;

		public static string[] keyString = new string[10];

		public static int[] keyInt = new int[10];

		public static bool netDiag = false;

		public static int txData = 0;

		public static int rxData = 0;

		public static int txMsg = 0;

		public static int rxMsg = 0;

		public static int maxMsg = 104;

		public static int[] rxMsgType = new int[maxMsg];

		public static int[] rxDataType = new int[maxMsg];

		public static int[] txMsgType = new int[maxMsg];

		public static int[] txDataType = new int[maxMsg];

		public static float uCarry = 0f;

		public static bool drawSkip = false;

		public static int fpsCount = 0;

		public static Stopwatch fpsTimer = new Stopwatch();

		public static Stopwatch updateTimer = new Stopwatch();

		public bool gammaTest;

		public static bool showSplash = false;

		public static bool ignoreErrors = true;

		public static string defaultIP = "";

		public static int dayRate = 1;

		public static int maxScreenW = 1920;

		public static int minScreenW = 800;

		public static int maxScreenH = 1200;

		public static int minScreenH = 600;

		public static float iS = 1f;

		public static bool render = false;

		public static int qaStyle = 0;

		public static int zoneX = 99;

		public static int zoneY = 87;

		public static float harpNote = 0f;

		public static ArrayHandler<bool> projHostile;

		public static ArrayHandler<bool> pvpBuff;

		public static ArrayHandler<bool> debuff;

		public static ArrayHandler<string> buffName;

		public static ArrayHandler<string> buffTip;

		public static ArrayHandler<bool> buffDontDisplayTime;

		public static int maxMP = 10;

		public static string[] recentWorld = new string[maxMP];

		public static string[] recentIP = new string[maxMP];

		public static int[] recentPort = new int[maxMP];

		public static bool shortRender = true;

		public static bool owBack = true;

		public static int quickBG = 2;

		public static int bgDelay = 0;

		public static int bgStyle = 0;

		public static float[] bgAlpha = new float[10];

		public static float[] bgAlpha2 = new float[10];

		public bool showNPCs;

		public int mouseNPC = -1;

		public static int wof = -1;

		public static int wofT;

		public static int wofB;

		public static int wofF = 0;

		public static int offScreenRange = 200;

		public RenderTarget2D backWaterTarget;

		public RenderTarget2D waterTarget;

		public RenderTarget2D tileTarget;

		public RenderTarget2D blackTarget;

		public RenderTarget2D tile2Target;

		public RenderTarget2D wallTarget;

		public RenderTarget2D backgroundTarget;

		public int firstTileX;

		public int lastTileX;

		public int firstTileY;

		public int lastTileY;

		public double bgParrallax;

		public int bgStart;

		public int bgLoops;

		public int bgStartY;

		public int bgLoopsY;

		public int bgTop;

		public static int renderCount = 99;

		public GraphicsDeviceManager graphics;

		public SpriteBatch spriteBatch;

		public Process tServer = new Process();

		public static Stopwatch saveTime = new Stopwatch();

		public static MouseState mouseState = Mouse.GetState();

		public static MouseState oldMouseState = Mouse.GetState();

		public static KeyboardState keyState = Keyboard.GetState();

		public static Color mcColor = new Color(125, 125, 255);

		public static Color hcColor = new Color(200, 125, 255);

		public static Color bgColor;

		public static bool mouseHC = false;

		public static string chestText = "Chest";

		public static bool craftingHide = false;

		public static bool armorHide = false;

		public static float craftingAlpha = 1f;

		public static float armorAlpha = 1f;

		public static ArrayHandler<float> buffAlpha;

		public static Item trashItem = new Item();

		public static bool hardMode = false;

		public float chestLootScale = 1f;

		public bool chestLootHover;

		public float chestStackScale = 1f;

		public bool chestStackHover;

		public float chestDepositScale = 1f;

		public bool chestDepositHover;

		public static bool drawScene = false;

		public static Vector2 sceneWaterPos = default(Vector2);

		public static Vector2 sceneTilePos = default(Vector2);

		public static Vector2 sceneTile2Pos = default(Vector2);

		public static Vector2 sceneWallPos = default(Vector2);

		public static Vector2 sceneBackgroundPos = default(Vector2);

		public static bool maxQ = true;

		public static float gfxQuality = 1f;

		public static float gfxRate = 0.01f;

		public int DiscoStyle;

		public static int DiscoR = 255;

		public static int DiscoB = 0;

		public static int DiscoG = 0;

		public static int teamCooldown = 0;

		public static int teamCooldownLen = 300;

		public static bool gamePaused = false;

		public static bool modPause = false;

		public static int updateTime = 0;

		public static int drawTime = 0;

		public static int uCount = 0;

		public static int updateRate = 0;

		public static int frameRate = 0;

		public static bool RGBRelease = false;

		public static bool qRelease = false;

		public static bool netRelease = false;

		public static bool frameRelease = false;

		public static bool showFrameRate = false;

		public static int magmaBGFrame = 0;

		public static int magmaBGFrameCounter = 0;

		public static int saveTimer = 0;

		public static bool autoJoin = false;

		public static bool serverStarting = false;

		public static float leftWorld = 0f;

		public static float rightWorld = 134400f;

		public static float topWorld = 0f;

		public static float bottomWorld = 38400f;

		public static int maxTilesX = (int)rightWorld / 16 + 1;

		public static int maxTilesY = (int)bottomWorld / 16 + 1;

		public static int maxSectionsX = maxTilesX / 200;

		public static int maxSectionsY = maxTilesY / 150;

		public static int numDust = 2000;

		public static int maxNetPlayers = 255;

		public static ArrayHandler<string> chrName = new ArrayHandler<string>(147);

		public static int worldRate = 1;

		public static float caveParrallax = 1f;

		public static ArrayHandler<string> tileName = new ArrayHandler<string>(150);

		public static int dungeonX;

		public static int dungeonY;

		public static Liquid[] liquid = new Liquid[Liquid.resLiquid];

		public static LiquidBuffer[] liquidBuffer = new LiquidBuffer[10000];

		public static bool dedServ = false;

		public static int spamCount = 0;

		public static SoundHandler.Music curMusic = null;

		public SoundHandler.Music newMusic;

		public static bool showItemText = true;

		public static bool autoSave = true;

		public static string buffString = "";

		public static string libPath = "";

		public static int lo = 0;

		public static int LogoA = 255;

		public static int LogoB = 0;

		public static bool LogoT = false;

		public static string statusText = "";

		public static StringBuilder statusTextSB;

		public static string worldName = "";

		public static int worldID;

		public static int background = 0;

		public static Color tileColor;

		public static double worldSurface;

		public static double rockLayer;

		public static double hellLayer;

		public static Color[] teamColor = new Color[5];

		public static bool dayTime = true;

		public static double time = 13500.0;

		public static int moonPhase = 0;

		public static short sunModY = 0;

		public static short moonModY = 0;

		public static bool grabSky = false;

		public static bool bloodMoon = false;

		public static int checkForSpawns = 0;

		public static int helpText = 0;

		public static bool autoGen = false;

		public static bool autoPause = false;

		public static ArrayHandler<int> projFrames = new ArrayHandler<int>(112);

		public static float demonTorch = 1f;

		public static int demonTorchDir = 1;

		public static int numStars;

		public static int cloudLimit = 100;

		public static int numClouds = cloudLimit;

		public static float windSpeed = 0f;

		public static float windSpeedSpeed = 0f;

		public static Cloud[] cloud = new Cloud[100];

		public static bool resetClouds = true;

		public static int sandTiles;

		public static int evilTiles;

		public static int snowTiles;

		public static int holyTiles;

		public static int meteorTiles;

		public static int jungleTiles;

		public static int dungeonTiles;

		public static int[] tileCount = new int[1];

		public static bool ForceGuideMenu;

		public static int fadeCounter = 0;

		public static float invAlpha = 1f;

		public static float invDir = 1f;

		[ThreadStatic]
		public static Random rand;

		public static Texture2D[] bannerTexture = new Texture2D[3];

		public static TextureHandler npcHeadTexture = new TextureHandler(12);

		public static Texture2D[] destTexture = new Texture2D[3];

		public static Texture2D[] wingsTexture = new Texture2D[3];

		public static TextureHandler armorHeadTexture = new TextureHandler(45);

		public static TextureHandler armorBodyTexture = new TextureHandler(26);

		public static TextureHandler femaleBodyTexture = new TextureHandler(26);

		public static TextureHandler armorArmTexture = new TextureHandler(26);

		public static TextureHandler armorLegTexture = new TextureHandler(25);

		public static Texture2D timerTexture;

		public static Texture2D reforgeTexture;

		public static Texture2D wallOutlineTexture;

		public static Texture2D wireTexture;

		public static Texture2D gridTexture;

		public static Texture2D lightDiscTexture;

		public static Texture2D MusicBoxTexture;

		public static Texture2D EyeLaserTexture;

		public static Texture2D BoneEyesTexture;

		public static Texture2D BoneLaserTexture;

		public static Texture2D trashTexture;

		public static Texture2D chainTexture;

		public static Texture2D probeTexture;

		public static Texture2D confuseTexture;

		public static Texture2D chain2Texture;

		public static Texture2D chain3Texture;

		public static Texture2D chain4Texture;

		public static Texture2D chain5Texture;

		public static Texture2D chain6Texture;

		public static Texture2D chain7Texture;

		public static Texture2D chain8Texture;

		public static Texture2D chain9Texture;

		public static Texture2D chain10Texture;

		public static Texture2D chain11Texture;

		public static Texture2D chain12Texture;

		public static Texture2D chaosTexture;

		public static Texture2D cdTexture;

		public static Texture2D wofTexture;

		public static Texture2D boneArmTexture;

		public static Texture2D boneArm2Texture;

		public static Texture2D[] npcToggleTexture = new Texture2D[2];

		public static Texture2D[] HBLockTexture = new Texture2D[2];

		public static TextureHandler buffTexture = new TextureHandler(41);

		public static TextureHandler itemTexture;

		public static TextureHandler npcTexture;

		public static TextureHandler projectileTexture;

		public static TextureHandler goreTexture = new TextureHandler(160);

		public static Texture2D cursorTexture;

		public static Texture2D dustTexture;

		public static Texture2D sunTexture;

		public static Texture2D sun2Texture;

		public static Texture2D moonTexture;

		public static TextureHandler tileTexture;

		public static Texture2D blackTileTexture;

		public static TextureHandler wallTexture = new TextureHandler(32);

		public static Texture2D[] backgroundTexture = new Texture2D[32];

		public static Texture2D[] cloudTexture = new Texture2D[4];

		public static Texture2D[] starTexture = new Texture2D[5];

		public static Texture2D[] liquidTexture = new Texture2D[2];

		public static Texture2D heartTexture;

		public static Texture2D manaTexture;

		public static Texture2D bubbleTexture;

		public static Texture2D[] treeTopTexture = new Texture2D[5];

		public static Texture2D shroomCapTexture;

		public static Texture2D[] treeBranchTexture = new Texture2D[5];

		public static Texture2D inventoryBackTexture;

		public static Texture2D inventoryBack2Texture;

		public static Texture2D inventoryBack3Texture;

		public static Texture2D inventoryBack4Texture;

		public static Texture2D inventoryBack5Texture;

		public static Texture2D inventoryBack6Texture;

		public static Texture2D inventoryBack7Texture;

		public static Texture2D inventoryBack8Texture;

		public static Texture2D inventoryBack9Texture;

		public static Texture2D inventoryBack10Texture;

		public static Texture2D inventoryBack11Texture;

		public static Texture2D loTexture;

		public static Texture2D logoTexture;

		public static Texture2D logo2Texture;

		public static Texture2D logo3Texture;

		public static Texture2D textBackTexture;

		public static Texture2D chatTexture;

		public static Texture2D chat2Texture;

		public static Texture2D chatBackTexture;

		public static Texture2D teamTexture;

		public static Texture2D reTexture;

		public static Texture2D raTexture;

		public static Texture2D splashTexture;

		public static Texture2D fadeTexture;

		public static Texture2D ninjaTexture;

		public static Texture2D antLionTexture;

		public static Texture2D spikeBaseTexture;

		public static Texture2D ghostTexture;

		public static Texture2D evilCactusTexture;

		public static Texture2D goodCactusTexture;

		public static Texture2D wraithEyeTexture;

		public static Texture2D skinBodyTexture;

		public static Texture2D skinLegsTexture;

		public static Texture2D playerEyeWhitesTexture;

		public static Texture2D playerEyesTexture;

		public static Texture2D playerHandsTexture;

		public static Texture2D playerHands2Texture;

		public static Texture2D playerHeadTexture;

		public static Texture2D playerPantsTexture;

		public static Texture2D playerShirtTexture;

		public static Texture2D playerShoesTexture;

		public static Texture2D playerUnderShirtTexture;

		public static Texture2D playerUnderShirt2Texture;

		public static Texture2D femaleShirt2Texture;

		public static Texture2D femalePantsTexture;

		public static Texture2D femaleShirtTexture;

		public static Texture2D femaleShoesTexture;

		public static Texture2D femaleUnderShirtTexture;

		public static Texture2D femaleUnderShirt2Texture;

		public static Texture2D[] playerHairTexture = new Texture2D[36];

		public static Texture2D[] playerHairAltTexture = new Texture2D[36];

		public static SoundEffect[] soundMech = new SoundEffect[1];

		public static SoundEffectInstance[] soundInstanceMech = new SoundEffectInstance[1];

		public static SoundEffect[] soundDig = new SoundEffect[3];

		public static SoundEffectInstance[] soundInstanceDig = new SoundEffectInstance[3];

		public static SoundEffect[] soundTink = new SoundEffect[3];

		public static SoundEffectInstance[] soundInstanceTink = new SoundEffectInstance[3];

		public static SoundEffect[] soundPlayerHit = new SoundEffect[3];

		public static SoundEffectInstance[] soundInstancePlayerHit = new SoundEffectInstance[3];

		public static SoundEffect[] soundFemaleHit = new SoundEffect[3];

		public static SoundEffectInstance[] soundInstanceFemaleHit = new SoundEffectInstance[3];

		public static SoundEffect soundPlayerKilled;

		public static SoundEffectInstance soundInstancePlayerKilled;

		public static SoundEffect soundGrass;

		public static SoundEffectInstance soundInstanceGrass;

		public static SoundEffect soundGrab;

		public static SoundEffectInstance soundInstanceGrab;

		public static SoundEffect soundPixie;

		public static SoundEffectInstance soundInstancePixie;

		public static SoundHandler soundItem = new SoundHandler(38);

		public static soundInstanceHandler soundInstanceItem = new soundInstanceHandler(38);

		public static SoundHandler soundNPCHit = new SoundHandler(12);

		public static soundInstanceHandler soundInstanceNPCHit = new soundInstanceHandler(12);

		public static SoundHandler soundNPCKilled = new SoundHandler(16);

		public static soundInstanceHandler soundInstanceNPCKilled = new soundInstanceHandler(16);

		public static SoundEffect soundDoorOpen;

		public static SoundEffectInstance soundInstanceDoorOpen;

		public static SoundEffect soundDoorClosed;

		public static SoundEffectInstance soundInstanceDoorClosed;

		public static SoundEffect soundMenuOpen;

		public static SoundEffectInstance soundInstanceMenuOpen;

		public static SoundEffect soundMenuClose;

		public static SoundEffectInstance soundInstanceMenuClose;

		public static SoundEffect soundMenuTick;

		public static SoundEffectInstance soundInstanceMenuTick;

		public static SoundEffect soundShatter;

		public static SoundEffectInstance soundInstanceShatter;

		public static SoundEffect[] soundZombie = new SoundEffect[5];

		public static SoundEffectInstance[] soundInstanceZombie = new SoundEffectInstance[5];

		public static SoundEffect[] soundRoar = new SoundEffect[2];

		public static SoundEffectInstance[] soundInstanceRoar = new SoundEffectInstance[2];

		public static SoundEffect[] soundSplash = new SoundEffect[2];

		public static SoundEffectInstance[] soundInstanceSplash = new SoundEffectInstance[2];

		public static SoundEffect soundDoubleJump;

		public static SoundEffectInstance soundInstanceDoubleJump;

		public static SoundEffect soundRun;

		public static SoundEffectInstance soundInstanceRun;

		public static SoundEffect soundCoins;

		public static SoundEffectInstance soundInstanceCoins;

		public static SoundEffect soundUnlock;

		public static SoundEffectInstance soundInstanceUnlock;

		public static SoundEffect soundChat;

		public static SoundEffectInstance soundInstanceChat;

		public static SoundEffect soundMaxMana;

		public static SoundEffectInstance soundInstanceMaxMana;

		public static SoundEffect soundDrown;

		public static SoundEffectInstance soundInstanceDrown;

		public static AudioEngine engine;

		public static SoundBank soundBank;

		public static WaveBank waveBank;

		public static Cue[] music = new Cue[14];

		public static float[] musicFade = new float[14];

		public static float musicVolume = 0.75f;

		public static float soundVolume = 1f;

		public static SpriteFont fontItemStack;

		public static SpriteFont fontMouseText;

		public static SpriteFont fontDeathText;

		public static SpriteFont[] fontCombatText = new SpriteFont[2];

		public static ArrayHandler<bool> tileLighted = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileMergeDirt = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileStone = new ArrayHandler<bool>(150);

		public static bool[][] tileMerge = new bool[200][];

		public static ArrayHandler<bool> tileCut = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileAlch = new ArrayHandler<bool>(150);

		public static ArrayHandler<int> tileShine = new ArrayHandler<int>(150);

		public static ArrayHandler<bool> tileShine2 = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> wallHouse = new ArrayHandler<bool>(32);

		public static ArrayHandler<int> wallBlend = new ArrayHandler<int>(32);

		public static ArrayHandler<bool> tilePick = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileAxe = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileHammer = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileWaterDeath = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileLavaDeath = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileTable = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileBlockLight = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileNoSunLight = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileDungeon = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileSolidTop = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileSolid = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileNoAttach = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileNoFail = new ArrayHandler<bool>(150);

		public static ArrayHandler<bool> tileFrameImportant = new ArrayHandler<bool>(150);

		public static int[] backgroundWidth = new int[32];

		public static int[] backgroundHeight = new int[32];

		public static bool tilesLoaded = false;

		public static Tile[,] tile = new Tile[maxTilesX, maxTilesY];

		public static Dust[] dust = new Dust[2001];

		public static Star[] star = new Star[130];

		public static Item[] item = new Item[201];

		public static NPC[] npc = new NPC[201];

		public static Gore[] gore = new Gore[201];

		public static Projectile[] projectile = new Projectile[1001];

		public static CombatText[] combatText = new CombatText[100];

		public static ItemText[] itemText = new ItemText[20];

		public static Chest[] chest = new Chest[1000];

		public static Sign[] sign = new Sign[1000];

		public static Vector2 screenPosition;

		public static Vector2 screenLastPosition;

		public static int screenWidth = 800;

		public static int screenHeight = 600;

		public static int chatLength = 600;

		public static bool chatMode = false;

		public static bool chatRelease = false;

		public static int numChatLines = 7;

		public static string chatText = "";

		public static ChatLine[] chatLine = new ChatLine[numChatLines];

		public static bool inputTextEnter = false;

		public static float[] hotbarScale = new float[10]
		{
			1f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f,
			0.75f
		};

		public static byte mouseTextColor = 0;

		public static int mouseTextColorChange = 1;

		public static bool mouseLeftRelease = false;

		public static bool mouseRightRelease = false;

		public static bool playerInventory = false;

		public static int stackSplit;

		public static int stackCounter = 0;

		public static int stackDelay = 7;

		public static Item mouseItem = new Item();

		public static Item guideItem = new Item();

		public static Item reforgeItem = new Item();

		public static float inventoryScale = 0.75f;

		public static bool hasFocus = true;

		public static int myPlayer = 0;

		public static Player[] player = new Player[256];

		public static int spawnTileX;

		public static int spawnTileY;

		public static bool npcChatRelease = false;

		public static bool editSign = false;

		public static string signText = "";

		public static bool npcChatFocus1 = false;

		public static bool npcChatFocus2 = false;

		public static bool npcChatFocus3 = false;

		public static int npcShop = 0;

		public ArrayHandler<Chest> shop = new ArrayHandler<Chest>(10);

		public static bool craftGuide = false;

		public static bool reforge = false;

		public static Item toolTip = new Item();

		public static int backSpaceCount = 0;

		public static string motd = "";

		public bool toggleFullscreen;

		public int numDisplayModes;

		public int[] displayWidth = new int[99];

		public int[] displayHeight = new int[99];

		public static bool gameMenu = true;

		public static Player[] loadPlayer = new Player[999];

		public static string[] loadPlayerPath = new string[999];

		public static int numLoadPlayers = 0;

		public static string playerPathName;

		public static string[] loadWorld = new string[999];

		public static string[] loadWorldPath = new string[999];

		public static string[] loadPlayerName = new string[999];

		public static Texture2D[] loadPlayerTexture = new Texture2D[999];

		public static Player saveClone;

		public static int numLoadWorlds = 0;

		public static string worldPathName;

		public static ItemNameAccess itemName;

		public static string[] npcName = new string[147];

		public static KeyboardState inputText;

		public static KeyboardState oldInputText;

		public static int invasionType = 0;

		public static double invasionX = 0.0;

		public static int invasionSize = 0;

		public static int invasionDelay = 0;

		public static int invasionWarn = 0;

		public static ArrayHandler<int> npcFrameCount = new ArrayHandler<int>(new int[147]
		{
			1,
			2,
			2,
			3,
			6,
			2,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			16,
			14,
			16,
			14,
			15,
			16,
			2,
			10,
			1,
			16,
			16,
			16,
			3,
			1,
			15,
			3,
			1,
			3,
			1,
			1,
			16,
			16,
			1,
			1,
			1,
			3,
			3,
			15,
			3,
			7,
			7,
			4,
			5,
			5,
			5,
			3,
			3,
			16,
			6,
			3,
			6,
			6,
			2,
			5,
			3,
			2,
			7,
			7,
			4,
			2,
			8,
			1,
			5,
			1,
			2,
			4,
			16,
			5,
			4,
			4,
			15,
			15,
			15,
			15,
			2,
			4,
			6,
			6,
			18,
			16,
			1,
			1,
			1,
			1,
			1,
			1,
			4,
			3,
			1,
			1,
			1,
			1,
			1,
			1,
			5,
			6,
			7,
			16,
			1,
			1,
			16,
			16,
			12,
			20,
			21,
			1,
			2,
			2,
			3,
			6,
			1,
			1,
			1,
			15,
			4,
			11,
			1,
			14,
			6,
			6,
			3,
			1,
			2,
			2,
			1,
			3,
			4,
			1,
			2,
			1,
			4,
			2,
			1,
			15,
			3,
			16,
			4,
			5,
			7,
			3
		});

		public static bool mouseExit = false;

		public static float exitScale = 0.8f;

		public static bool mouseReforge = false;

		public static float reforgeScale = 0.8f;

		public static Player clientPlayer = new Player();

		public static string getIP = defaultIP;

		public static string getPort = Convert.ToString(Netplay.serverPort);

		public static bool menuMultiplayer = false;

		public static bool menuServer = false;

		public static int netMode = 0;

		public static int timeOut = 120;

		public static int netPlayCounter;

		public static int lastNPCUpdate;

		public static int lastItemUpdate;

		public static int maxNPCUpdates = 5;

		public static int maxItemUpdates = 5;

		public static string cUp = "W";

		public static string cLeft = "A";

		public static string cDown = "S";

		public static string cRight = "D";

		public static string cJump = "Space";

		public static string cThrowItem = "T";

		public static string cInv = "Escape";

		public static string cHeal = "H";

		public static string cMana = "M";

		public static string cBuff = "B";

		public static string cHook = "E";

		public static string cTorch = "LeftShift";

		public static Color mouseColor = new Color(255, 50, 95);

		public static Color cursorColor = Color.White;

		public static int cursorColorDirection = 1;

		public static float cursorAlpha = 0f;

		public static float cursorScale = 0f;

		public static bool signBubble = false;

		public static int signX = 0;

		public static int signY = 0;

		public static bool hideUI = false;

		public static bool releaseUI = false;

		public static bool fixedTiming = false;

		public int splashCounter;

		public static string oldStatusText = "";

		public static bool autoShutdown = false;

		public float logoRotation;

		public float logoRotationDirection = 1f;

		public float logoRotationSpeed = 1f;

		public float logoScale = 1f;

		public float logoScaleDirection = 1f;

		public float logoScaleSpeed = 1f;

		public static int maxMenuItems = 14;

		public float[] menuItemScale = new float[maxMenuItems];

		public int focusMenu = -1;

		public int selectedMenu = -1;

		public int selectedMenu2 = -1;

		public int selectedPlayer;

		public int selectedWorld;

		public static int menuMode = 0;

		public static Item cpItem = new Item();

		public int textBlinkerCount;

		public int textBlinkerState;

		public static string newWorldName = "";

		public static int accSlotCount = 0;

		public Color selColor = Color.White;

		public int focusColor;

		public int colorDelay;

		public int setKey = -1;

		public int bgScroll;

		public static bool autoPass = false;

		public static int menuFocus = 0;

		public static string MouseTextString = "";

		public static int MouseTextRare = 0;

		public static bool loadPlayersAfterScreenshot;

		public static Vector2 MouseWorld => new Vector2(screenPosition.X + (float)mouseX, screenPosition.Y + (float)mouseY);

		public static Color Disco => new Color(DiscoR, DiscoG, DiscoB);

		[DllImport("User32")]
		public static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

		[DllImport("User32")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("User32")]
		public static extern int GetMenuItemCount(IntPtr hWnd);

		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		public static void LoadWorlds()
		{
			Directory.CreateDirectory(WorldPath);
			string[] files = Directory.GetFiles(WorldPath, "*.wld");
			int num = files.Length;
			if (!dedServ && num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < num; i++)
			{
				loadWorldPath[i] = files[i];
				try
				{
					using (FileStream input = new FileStream(loadWorldPath[i], FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							binaryReader.ReadInt32();
							loadWorld[i] = binaryReader.ReadString();
							binaryReader.Close();
						}
					}
				}
				catch
				{
					loadWorld[i] = loadWorldPath[i];
				}
			}
			int num2 = num;
			files = Directory.GetFiles(WorldPath, "*.zip");
			for (int j = 0; j < files.Length; j++)
			{
				loadWorldPath[num2 + j] = files[j];
				loadWorld[num2 + j] = Path.GetFileNameWithoutExtension(files[j]);
				num++;
			}
			numLoadWorlds = num;
		}

		public static void LoadPlayers()
		{
			Directory.CreateDirectory(PlayerPath);
			string[] files = Directory.GetFiles(PlayerPath, "*.plr");
			int num = files.Length;
			if (num > 5)
			{
				num = 5;
			}
			for (int i = 0; i < 5; i++)
			{
				loadPlayer[i] = new Player(modsLoaded: true);
				if (i < num)
				{
					loadPlayerPath[i] = files[i];
					loadPlayer[i] = Player.LoadPlayer(loadPlayerPath[i]);
					loadPlayerName[i] = loadPlayer[i].name;
				}
			}
			int num2 = num;
			files = Directory.GetFiles(PlayerPath, "*.zip");
			for (int j = 0; j < files.Length; j++)
			{
				loadPlayerPath[num2 + j] = files[j];
				loadPlayerName[num2 + j] = Path.GetFileNameWithoutExtension(files[j]);
				loadPlayer[num2 + j] = Player.LoadPlayer(files[j]);
				loadPlayerTexture[num2 + j] = null;
				if (File.Exists(files[j] + ".png"))
				{
					using (FileStream stream = File.OpenRead(files[j] + ".png"))
					{
						loadPlayerTexture[num2 + j] = Texture2D.FromStream(Config.mainInstance.GraphicsDevice, stream);
					}
				}
				num++;
			}
			numLoadPlayers = num;
		}

		public void OpenRecent()
		{
			try
			{
				if (File.Exists(SavePath + Path.DirectorySeparatorChar + "servers.dat"))
				{
					using (FileStream input = new FileStream(SavePath + Path.DirectorySeparatorChar + "servers.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							binaryReader.ReadInt32();
							for (int i = 0; i < 10; i++)
							{
								recentWorld[i] = binaryReader.ReadString();
								recentIP[i] = binaryReader.ReadString();
								recentPort[i] = binaryReader.ReadInt32();
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void SaveRecent()
		{
			Directory.CreateDirectory(SavePath);
			try
			{
				File.SetAttributes(SavePath + Path.DirectorySeparatorChar + "servers.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream output = new FileStream(SavePath + Path.DirectorySeparatorChar + "servers.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(output))
					{
						binaryWriter.Write(curRelease);
						for (int i = 0; i < 10; i++)
						{
							binaryWriter.Write(recentWorld[i]);
							binaryWriter.Write(recentIP[i]);
							binaryWriter.Write(recentPort[i]);
						}
					}
				}
			}
			catch
			{
			}
		}

		public void SaveSettings()
		{
			Directory.CreateDirectory(SavePath);
			try
			{
				File.SetAttributes(SavePath + Path.DirectorySeparatorChar + "config.dat", FileAttributes.Normal);
			}
			catch
			{
			}
			try
			{
				using (FileStream output = new FileStream(SavePath + Path.DirectorySeparatorChar + "config.dat", FileMode.Create))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(output))
					{
						binaryWriter.Write(curRelease);
						binaryWriter.Write(graphics.IsFullScreen);
						binaryWriter.Write(mouseColor.R);
						binaryWriter.Write(mouseColor.G);
						binaryWriter.Write(mouseColor.B);
						binaryWriter.Write(soundVolume);
						binaryWriter.Write(musicVolume);
						binaryWriter.Write(cUp);
						binaryWriter.Write(cDown);
						binaryWriter.Write(cLeft);
						binaryWriter.Write(cRight);
						binaryWriter.Write(cJump);
						binaryWriter.Write(cThrowItem);
						binaryWriter.Write(cInv);
						binaryWriter.Write(cHeal);
						binaryWriter.Write(cMana);
						binaryWriter.Write(cBuff);
						binaryWriter.Write(cHook);
						binaryWriter.Write(caveParrallax);
						binaryWriter.Write(fixedTiming);
						binaryWriter.Write(graphics.PreferredBackBufferWidth);
						binaryWriter.Write(graphics.PreferredBackBufferHeight);
						binaryWriter.Write(autoSave);
						binaryWriter.Write(autoPause);
						binaryWriter.Write(showItemText);
						binaryWriter.Write(cTorch);
						binaryWriter.Write((byte)Lighting.lightMode);
						binaryWriter.Write((byte)qaStyle);
						binaryWriter.Write(owBack);
						binaryWriter.Write((byte)Lang.lang);
						binaryWriter.Close();
					}
				}
			}
			catch
			{
			}
		}

		public void CheckBunny()
		{
			try
			{
				RegistryKey currentUser = Registry.CurrentUser;
				currentUser = currentUser.CreateSubKey("Software\\Terraria");
				if (currentUser != null && currentUser.GetValue("Bunny") != null && currentUser.GetValue("Bunny").ToString() == "1")
				{
					cEd = true;
				}
			}
			catch
			{
				cEd = false;
			}
		}

		public void OpenSettings()
		{
			try
			{
				if (File.Exists(SavePath + Path.DirectorySeparatorChar + "config.dat"))
				{
					using (FileStream input = new FileStream(SavePath + Path.DirectorySeparatorChar + "config.dat", FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							int num = binaryReader.ReadInt32();
							bool flag = binaryReader.ReadBoolean();
							mouseColor.R = binaryReader.ReadByte();
							mouseColor.G = binaryReader.ReadByte();
							mouseColor.B = binaryReader.ReadByte();
							soundVolume = binaryReader.ReadSingle();
							musicVolume = binaryReader.ReadSingle();
							cUp = binaryReader.ReadString();
							cDown = binaryReader.ReadString();
							cLeft = binaryReader.ReadString();
							cRight = binaryReader.ReadString();
							cJump = binaryReader.ReadString();
							cThrowItem = binaryReader.ReadString();
							if (num >= 1)
							{
								cInv = binaryReader.ReadString();
							}
							if (num >= 12)
							{
								cHeal = binaryReader.ReadString();
								cMana = binaryReader.ReadString();
								cBuff = binaryReader.ReadString();
							}
							if (num >= 13)
							{
								cHook = binaryReader.ReadString();
							}
							caveParrallax = binaryReader.ReadSingle();
							if (num >= 2)
							{
								fixedTiming = binaryReader.ReadBoolean();
							}
							if (num >= 4)
							{
								graphics.PreferredBackBufferWidth = binaryReader.ReadInt32();
								graphics.PreferredBackBufferHeight = binaryReader.ReadInt32();
							}
							if (num >= 8)
							{
								autoSave = binaryReader.ReadBoolean();
							}
							if (num >= 9)
							{
								autoPause = binaryReader.ReadBoolean();
							}
							if (num >= 19)
							{
								showItemText = binaryReader.ReadBoolean();
							}
							if (num >= 30)
							{
								cTorch = binaryReader.ReadString();
								Lighting.lightMode = binaryReader.ReadByte();
								qaStyle = binaryReader.ReadByte();
							}
							if (num >= 37)
							{
								owBack = binaryReader.ReadBoolean();
							}
							if (num >= 39)
							{
								Lang.lang = binaryReader.ReadByte();
							}
							binaryReader.Close();
							if (flag && !graphics.IsFullScreen)
							{
								graphics.ToggleFullScreen();
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void ErasePlayer(int i)
		{
			try
			{
				File.Delete(loadPlayerPath[i]);
				File.Delete(loadPlayerPath[i] + ".bak");
				File.Delete(loadPlayerPath[i] + ".png");
				Config.DeletePlayer(loadPlayerPath[i]);
				LoadPlayers();
			}
			catch
			{
				NewText("hmm");
			}
		}

		public static void EraseWorld(int i)
		{
			try
			{
				File.Delete(loadWorldPath[i]);
				File.Delete(loadWorldPath[i] + ".bak");
				Config.DeleteWorld(loadWorldPath[i]);
				LoadWorlds();
			}
			catch
			{
			}
		}

		public static string nextLoadPlayer()
		{
			string text = Path.Combine(PlayerPath, loadPlayer[numLoadPlayers].name + ".zip");
			int num = 0;
			string name = loadPlayer[numLoadPlayers].name;
			while (File.Exists(text))
			{
				loadPlayer[numLoadPlayers].name = name + num;
				text = Path.Combine(PlayerPath, loadPlayer[numLoadPlayers].name + ".zip");
				num++;
			}
			return text;
		}

		public static string nextLoadWorld()
		{
			string text = Path.Combine(WorldPath, worldName + ".zip");
			int num = 0;
			string str = worldName;
			while (File.Exists(text))
			{
				worldName = str + num;
				text = Path.Combine(WorldPath, worldName + ".zip");
				num++;
			}
			return text;
		}

		public void autoCreate(string newOpt)
		{
			switch (newOpt)
			{
			case "0":
				autoGen = false;
				break;
			case "1":
				maxTilesX = 4200;
				maxTilesY = 1200;
				autoGen = true;
				break;
			case "2":
				maxTilesX = 6300;
				maxTilesY = 1800;
				autoGen = true;
				break;
			case "3":
				maxTilesX = 8400;
				maxTilesY = 2400;
				autoGen = true;
				break;
			}
		}

		public void NewMOTD(string newMOTD)
		{
			motd = newMOTD;
		}

		public void LoadDedConfig(string configPath)
		{
			if (File.Exists(configPath))
			{
				using (StreamReader streamReader = new StreamReader(configPath))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						try
						{
							if (text.Length > 6 && text.Substring(0, 6).ToLower() == "world=")
							{
								string text2 = worldPathName = text.Substring(6);
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "port=")
							{
								string value = text.Substring(5);
								try
								{
									int num = Netplay.serverPort = Convert.ToInt32(value);
								}
								catch
								{
								}
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "maxplayers=")
							{
								string value2 = text.Substring(11);
								try
								{
									int num2 = maxNetPlayers = Convert.ToInt32(value2);
								}
								catch
								{
								}
							}
							if (text.Length > 11 && text.Substring(0, 9).ToLower() == "priority=")
							{
								string value3 = text.Substring(9);
								try
								{
									int num3 = Convert.ToInt32(value3);
									if (num3 >= 0 && num3 <= 5)
									{
										Process currentProcess = Process.GetCurrentProcess();
										switch (num3)
										{
										case 0:
											currentProcess.PriorityClass = ProcessPriorityClass.RealTime;
											break;
										case 1:
											currentProcess.PriorityClass = ProcessPriorityClass.High;
											break;
										case 2:
											currentProcess.PriorityClass = ProcessPriorityClass.AboveNormal;
											break;
										case 3:
											currentProcess.PriorityClass = ProcessPriorityClass.Normal;
											break;
										case 4:
											currentProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
											break;
										case 5:
											currentProcess.PriorityClass = ProcessPriorityClass.Idle;
											break;
										}
									}
								}
								catch
								{
								}
							}
							if (text.Length > 9 && text.Substring(0, 9).ToLower() == "password=")
							{
								string text3 = Netplay.password = text.Substring(9);
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "motd=")
							{
								string text4 = motd = text.Substring(5);
							}
							if (text.Length > 5 && text.Substring(0, 5).ToLower() == "lang=")
							{
								string value4 = text.Substring(5);
								Lang.lang = Convert.ToInt32(value4);
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldpath=")
							{
								string text5 = WorldPath = text.Substring(10);
							}
							if (text.Length >= 10 && text.Substring(0, 10).ToLower() == "worldname=")
							{
								string text6 = worldName = text.Substring(10);
							}
							if (text.Length > 8 && text.Substring(0, 8).ToLower() == "banlist=")
							{
								string text7 = Netplay.banFile = text.Substring(8);
							}
							if (text.Length > 11 && text.Substring(0, 11).ToLower() == "autocreate=")
							{
								switch (text.Substring(11))
								{
								case "0":
									autoGen = false;
									break;
								case "1":
									maxTilesX = 4200;
									maxTilesY = 1200;
									autoGen = true;
									break;
								case "2":
									maxTilesX = 6300;
									maxTilesY = 1800;
									autoGen = true;
									break;
								case "3":
									maxTilesX = 8400;
									maxTilesY = 2400;
									autoGen = true;
									break;
								}
							}
							if (text.Length > 7 && text.Substring(0, 7).ToLower() == "secure=")
							{
								string a = text.Substring(7);
								if (a == "1")
								{
									Netplay.spamCheck = true;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		public void SetNetPlayers(int mPlayers)
		{
			maxNetPlayers = mPlayers;
		}

		public void SetWorld(string wrold)
		{
			worldPathName = wrold;
		}

		public void SetWorldName(string wrold)
		{
			worldName = wrold;
		}

		public void autoShut()
		{
			autoShutdown = true;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public void AutoPass()
		{
			autoPass = true;
		}

		public void AutoJoin(string IP)
		{
			defaultIP = IP;
			getIP = IP;
			Netplay.SetIP(defaultIP);
			autoJoin = true;
		}

		public void AutoHost()
		{
			menuMultiplayer = true;
			menuServer = true;
			menuMode = 1;
		}

		public void loadLib(string path)
		{
			libPath = path;
			LoadLibrary(libPath);
		}

		public void DedServ()
		{
			//IL_0737: Unknown result type (might be due to invalid IL or missing references)
			//IL_073e: Expected O, but got Unknown
			Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
			Debug.AutoFlush = true;
			rand = new Random();
			if (autoShutdown)
			{
				string lpWindowName = Console.Title = "terraria" + rand.Next(int.MaxValue);
				IntPtr intPtr = FindWindow(null, lpWindowName);
				if (intPtr != IntPtr.Zero)
				{
					ShowWindow(intPtr, 0);
				}
			}
			else
			{
				Console.Title = "Terraria Server " + versionNumber2;
			}
			dedServ = true;
			showSplash = false;
			Initialize();
			Lang.setLang();
			for (int i = 0; i < 147; i++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(i);
				npcName[i] = nPC.name;
			}
			Console.Clear();
			while (worldPathName == null || worldPathName == "")
			{
				LoadWorlds();
				bool flag = true;
				while (flag)
				{
					Console.WriteLine("Terraria Server " + versionNumber2);
					Console.WriteLine("");
					for (int j = 0; j < numLoadWorlds; j++)
					{
						Console.WriteLine(string.Concat(j + 1, '\t', '\t', loadWorld[j]));
					}
					Console.WriteLine("n" + '\t' + '\t' + "New World");
					Console.WriteLine("s\t\ttConfig Settings");
					Console.WriteLine("d <number>" + '\t' + "Delete World");
					Console.WriteLine("");
					Console.Write("Choose World: ");
					string text2 = Console.ReadLine();
					try
					{
						Console.Clear();
					}
					catch
					{
					}
					if (text2.Length >= 2 && text2.Substring(0, 2).ToLower() == "d ")
					{
						try
						{
							int num = Convert.ToInt32(text2.Substring(2)) - 1;
							if (num < numLoadWorlds)
							{
								Console.WriteLine("Terraria Server " + versionNumber2);
								Console.WriteLine("");
								Console.WriteLine("Really delete " + loadWorld[num] + "?");
								Console.Write("(y/n): ");
								string text3 = Console.ReadLine();
								if (text3.ToLower() == "y")
								{
									EraseWorld(num);
								}
							}
						}
						catch
						{
						}
						try
						{
							Console.Clear();
						}
						catch
						{
						}
						continue;
					}
					switch (text2)
					{
					case "s":
					case "S":
						try
						{
							IniFile settings;
							while (true)
							{
								int num5 = 1;
								settings = Config.settings;
								string text6 = "";
								if (settings["ModPacks"] != null)
								{
									ArrayList arrayList = new ArrayList(settings["ModPacks"].GetKeys());
									int tConfigMenuStart = Config.tConfigMenuStart;
									for (int k = tConfigMenuStart; k < arrayList.Count; k++)
									{
										string text7 = (string)arrayList[k];
										string text8 = num5++ + ". " + text7 + ": ";
										if (text8.Length > text6.Length)
										{
											text6 = text8;
										}
									}
								}
								num5 = 1;
								if (settings["ModPacks"] != null)
								{
									ArrayList arrayList2 = new ArrayList(settings["ModPacks"].GetKeys());
									int tConfigMenuStart2 = Config.tConfigMenuStart;
									for (int l = tConfigMenuStart2; l < arrayList2.Count; l++)
									{
										string text9 = (string)arrayList2[l];
										string key = text9;
										string str = "";
										if (settings["ModPacks"][text9] != "False")
										{
											if (settings["ModPacks"][text9] != "True")
											{
												new List<string>(Config.ModSetting[key]["Choice"].GetKeys());
												str += Config.ModSetting[key]["Choice"][settings["ModPacks"][text9]];
											}
											else
											{
												str += "On";
											}
										}
										else
										{
											str += "Off";
										}
										string text10 = num5++ + ". " + text9 + ": ";
										Console.Write(text10);
										for (int m = 0; m < text6.Length - text10.Length + 3; m++)
										{
											Console.Write(' ');
										}
										Console.WriteLine(str);
									}
								}
								Console.WriteLine();
								Console.WriteLine("Leave blank to go back");
								Console.WriteLine();
								Console.Write("Select: ");
								string text11 = Console.ReadLine();
								if (text11 == null || text11 == "")
								{
									break;
								}
								num5 = 1;
								int num6 = Convert.ToInt32(text11);
								if (settings["ModPacks"] != null)
								{
									ArrayList arrayList3 = new ArrayList(settings["ModPacks"].GetKeys());
									int tConfigMenuStart3 = Config.tConfigMenuStart;
									for (int n = tConfigMenuStart3; n < arrayList3.Count; n++)
									{
										string text12 = (string)arrayList3[n];
										string text13 = text12;
										if (num6 == num5)
										{
											Config.modSettingChanged = true;
											if (settings["ModPacks"][text12] != "False")
											{
												if (Config.tConfigModTypes.ContainsKey(text13) && Config.tConfigModTypes[text13].Count > 0)
												{
													Config.tConfigModChoices[text13]++;
													Config.tConfigModChoices[text13] = Config.tConfigModChoices[text13] % (Config.tConfigModTypes[text13].Count + 1);
													if (Config.tConfigModChoices[text13] == 0)
													{
														settings["ModPacks"][text12] = "False";
													}
													else
													{
														settings["ModPacks"][text12] = Config.tConfigModTypes[text13][Config.tConfigModChoices[text13] - 1];
													}
												}
												else
												{
													settings["ModPacks"][text12] = "False";
												}
											}
											else
											{
												if (!Config.tConfigModTypes.ContainsKey(text13))
												{
													try
													{
														string text14 = Path.Combine(Config.tConfigFolder, "ModPacks", text12 + ".obj");
														SevenZipExtractor val = (SevenZipExtractor)(object)new SevenZipExtractor(text14);
														MemoryStream memoryStream = new MemoryStream();
														val.ExtractFile("Config.ini", (Stream)memoryStream);
														val.Dispose();
														memoryStream.Position = 0L;
														IniFileReader reader = new IniFileReader(memoryStream);
														IniFile value2 = IniFile.FromStream(reader);
														Config.ModSetting[text13] = value2;
														Config.tConfigModTypes[text13] = new List<string>(Config.ModSetting[text13]["Choice"].GetKeys());
														int num7 = Config.tConfigModTypes[text13].IndexOf(Config.settings["ModPacks"][text12]);
														Config.tConfigModChoices[text13] = num7 + 1;
													}
													catch (Exception)
													{
														Config.tConfigModTypes[text13] = new List<string>();
													}
												}
												if (Config.tConfigModTypes.ContainsKey(text13) && Config.tConfigModTypes[text13].Count > 0)
												{
													Config.tConfigModChoices[text13]++;
													Config.tConfigModChoices[text13] = Config.tConfigModChoices[text13] % (Config.tConfigModTypes[text13].Count + 1);
													if (Config.tConfigModChoices[text13] == 0)
													{
														settings["ModPacks"][text12] = "False";
													}
													else
													{
														settings["ModPacks"][text12] = Config.tConfigModTypes[text13][Config.tConfigModChoices[text13] - 1];
													}
												}
												else
												{
													settings["ModPacks"][text12] = "True";
												}
											}
											break;
										}
										num5++;
									}
								}
								Console.Clear();
							}
							if (Config.modSettingChanged)
							{
								if (Config.loadedServerSettings)
								{
									settings.Save(SavePath + "\\Config Server Mod.ini");
								}
								else
								{
									settings.Save(SavePath + "\\Config Mod.ini");
								}
								Config.modSettingChanged = false;
								Console.Clear();
								Config.ReInitializeCallback(this);
							}
						}
						catch
						{
						}
						try
						{
							Console.Clear();
						}
						catch
						{
						}
						break;
					case "n":
					case "N":
					{
						bool flag3 = true;
						while (flag3)
						{
							Console.WriteLine("Terraria Server " + versionNumber2);
							Console.WriteLine("");
							Console.WriteLine("1" + '\t' + "Small");
							Console.WriteLine("2" + '\t' + "Medium");
							Console.WriteLine("3" + '\t' + "Large");
							Console.WriteLine("");
							Console.Write("Choose size: ");
							string value = Console.ReadLine();
							try
							{
								switch (Convert.ToInt32(value))
								{
								case 1:
									maxTilesX = 4200;
									maxTilesY = 1200;
									flag3 = false;
									break;
								case 2:
									maxTilesX = 6300;
									maxTilesY = 1800;
									flag3 = false;
									break;
								case 3:
									maxTilesX = 8400;
									maxTilesY = 2400;
									flag3 = false;
									break;
								}
							}
							catch
							{
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						flag3 = true;
						while (flag3)
						{
							Console.WriteLine("Terraria Server " + versionNumber2);
							Console.WriteLine("");
							Console.Write("Enter world name: ");
							newWorldName = Console.ReadLine();
							if (newWorldName != "" && newWorldName != " " && newWorldName != null)
							{
								flag3 = false;
							}
							try
							{
								Console.Clear();
							}
							catch
							{
							}
						}
						worldName = newWorldName;
						worldPathName = nextLoadWorld();
						menuMode = 10;
						WorldGen.CreateNewWorld();
						flag3 = false;
						while (menuMode == 10)
						{
							if (oldStatusText != statusText)
							{
								oldStatusText = statusText;
								Console.WriteLine(statusText);
							}
						}
						try
						{
							Console.Clear();
						}
						catch
						{
						}
						break;
					}
					default:
						try
						{
							int num2 = Convert.ToInt32(text2);
							num2--;
							if (num2 >= 0 && num2 < numLoadWorlds)
							{
								bool flag2 = true;
								while (flag2)
								{
									Console.WriteLine("Terraria Server " + versionNumber2);
									Console.WriteLine("");
									Console.Write("Max players (press enter for 8): ");
									string text4 = Console.ReadLine();
									try
									{
										if (text4 == "")
										{
											text4 = "8";
										}
										int num3 = Convert.ToInt32(text4);
										if (num3 <= 255 && num3 >= 1)
										{
											maxNetPlayers = num3;
											flag2 = false;
										}
										flag2 = false;
									}
									catch
									{
									}
									try
									{
										Console.Clear();
									}
									catch
									{
									}
								}
								flag2 = true;
								while (flag2)
								{
									Console.WriteLine("Terraria Server " + versionNumber2);
									Console.WriteLine("");
									Console.Write("Server port (press enter for 7777): ");
									string text5 = Console.ReadLine();
									try
									{
										if (text5 == "")
										{
											text5 = "7777";
										}
										int num4 = Convert.ToInt32(text5);
										if (num4 <= 65535)
										{
											Netplay.serverPort = num4;
											flag2 = false;
										}
									}
									catch
									{
									}
									try
									{
										Console.Clear();
									}
									catch
									{
									}
								}
								Console.WriteLine("Terraria Server " + versionNumber2);
								Console.WriteLine("");
								Console.Write("Server password (press enter for none): ");
								Netplay.password = Console.ReadLine();
								worldPathName = loadWorldPath[num2];
								flag = false;
								try
								{
									Console.Clear();
								}
								catch
								{
								}
							}
						}
						catch
						{
						}
						break;
					}
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			WorldGen.serverLoadWorld();
			Console.WriteLine("Terraria Server " + versionNumber);
			Console.WriteLine("");
			while (!Netplay.ServerUp)
			{
				if (oldStatusText != statusText)
				{
					oldStatusText = statusText;
					Console.WriteLine(statusText);
				}
			}
			try
			{
				Console.Clear();
			}
			catch
			{
			}
			Console.WriteLine("Terraria Server " + versionNumber);
			Console.WriteLine("");
			Console.WriteLine("Listening on port " + Netplay.serverPort);
			Console.WriteLine("Type 'help' for a list of commands.");
			Console.WriteLine("");
			Console.Title = "Terraria Server: " + worldName;
			Stopwatch stopwatch = new Stopwatch();
			if (!autoShutdown)
			{
				startDedInput();
			}
			stopwatch.Start();
			double num8 = 16.666666666666668;
			double num9 = 0.0;
			int num10 = 0;
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			while (!Netplay.disconnect)
			{
				double num11 = stopwatch.ElapsedMilliseconds;
				if (num11 + num9 >= num8)
				{
					num10++;
					num9 += num11 - num8;
					stopwatch.Reset();
					stopwatch.Start();
					if (oldStatusText != statusText)
					{
						oldStatusText = statusText;
						Console.WriteLine(statusText);
					}
					if (num9 > 1000.0)
					{
						num9 = 1000.0;
					}
					if (Netplay.anyClients)
					{
						Update(new GameTime());
					}
					double num12 = (double)stopwatch.ElapsedMilliseconds + num9;
					if (num12 < num8)
					{
						int num13 = (int)(num8 - num12) - 1;
						if (num13 > 1)
						{
							Thread.Sleep(num13);
							if (!Netplay.anyClients)
							{
								num9 = 0.0;
								Thread.Sleep(10);
							}
						}
					}
				}
				Thread.Sleep(0);
			}
		}

		public static void startDedInput()
		{
			ThreadPool.QueueUserWorkItem(startDedInputCallBack, 1);
		}

		public static void startDedInputCallBack(object threadContext)
		{
			while (!Netplay.disconnect)
			{
				Console.Write(": ");
				string text = Console.ReadLine();
				string text2 = text;
				Codable.RunGlobalMethod("ModGeneric", "ServerCommand", text);
				text = text.ToLower();
				try
				{
					switch (text)
					{
					case "help":
						Console.WriteLine("Available commands:");
						Console.WriteLine("");
						Console.WriteLine("help " + '\t' + '\t' + " Displays a list of commands.");
						Console.WriteLine("playing " + '\t' + " Shows the list of players");
						Console.WriteLine("clear " + '\t' + '\t' + " Clear the console window.");
						Console.WriteLine("exit " + '\t' + '\t' + " Shutdown the server and save.");
						Console.WriteLine("exit-nosave " + '\t' + " Shutdown the server without saving.");
						Console.WriteLine("save " + '\t' + '\t' + " Save the game world.");
						Console.WriteLine("kick <player> " + '\t' + " Kicks a player from the server.");
						Console.WriteLine("ban <player> " + '\t' + " Bans a player from the server.");
						Console.WriteLine("password" + '\t' + " Show password.");
						Console.WriteLine("password <pass>" + '\t' + " Change password.");
						Console.WriteLine("version" + '\t' + '\t' + " Print version number.");
						Console.WriteLine("time" + '\t' + '\t' + " Display game time.");
						Console.WriteLine("port" + '\t' + '\t' + " Print the listening port.");
						Console.WriteLine("maxplayers" + '\t' + " Print the max number of players.");
						Console.WriteLine("say <words>" + '\t' + " Send a message.");
						Console.WriteLine("motd" + '\t' + '\t' + " Print MOTD.");
						Console.WriteLine("motd <words>" + '\t' + " Change MOTD.");
						Console.WriteLine("dawn" + '\t' + '\t' + " Change time to dawn.");
						Console.WriteLine("noon" + '\t' + '\t' + " Change time to noon.");
						Console.WriteLine("dusk" + '\t' + '\t' + " Change time to dusk.");
						Console.WriteLine("midnight" + '\t' + " Change time to midnight.");
						Console.WriteLine("settle" + '\t' + '\t' + " Settle all water.");
						break;
					case "settle":
						if (!Liquid.panicMode)
						{
							Liquid.StartPanic();
						}
						else
						{
							Console.WriteLine("Water is already settling");
						}
						break;
					case "dawn":
						dayTime = true;
						time = 0.0;
						NetMessage.SendData(7);
						break;
					case "dusk":
						dayTime = false;
						time = 0.0;
						NetMessage.SendData(7);
						break;
					case "noon":
						dayTime = true;
						time = 27000.0;
						NetMessage.SendData(7);
						break;
					case "midnight":
						dayTime = false;
						time = 16200.0;
						NetMessage.SendData(7);
						break;
					case "exit-nosave":
						Netplay.disconnect = true;
						break;
					case "exit":
						WorldGen.saveWorld();
						Netplay.disconnect = true;
						break;
					case "save":
						WorldGen.saveWorld();
						break;
					case "time":
					{
						string text8 = "AM";
						double num2 = time;
						if (!dayTime)
						{
							num2 += 54000.0;
						}
						num2 = num2 / 86400.0 * 24.0;
						double num3 = 7.5;
						num2 = num2 - num3 - 12.0;
						if (num2 < 0.0)
						{
							num2 += 24.0;
						}
						if (num2 >= 12.0)
						{
							text8 = "PM";
						}
						int num4 = (int)num2;
						double num5 = num2 - (double)num4;
						num5 = (int)(num5 * 60.0);
						string text9 = string.Concat(num5);
						if (num5 < 10.0)
						{
							text9 = "0" + text9;
						}
						if (num4 > 12)
						{
							num4 -= 12;
						}
						if (num4 == 0)
						{
							num4 = 12;
						}
						Console.WriteLine("Time: " + num4 + ":" + text9 + " " + text8);
						break;
					}
					case "maxplayers":
						Console.WriteLine("Player limit: " + maxNetPlayers);
						break;
					case "port":
						Console.WriteLine("Port: " + Netplay.serverPort);
						break;
					case "version":
						Console.WriteLine("Terraria Server " + versionNumber);
						break;
					case "clear":
						try
						{
							Console.Clear();
						}
						catch
						{
						}
						break;
					case "playing":
					{
						int num = 0;
						for (int k = 0; k < 255; k++)
						{
							if (player[k].active)
							{
								num++;
								Console.WriteLine(string.Concat(player[k].name, " (", Netplay.serverSock[k].tcpClient.Client.RemoteEndPoint, ")"));
							}
						}
						switch (num)
						{
						case 0:
							Console.WriteLine("No players connected.");
							break;
						case 1:
							Console.WriteLine("1 player connected.");
							break;
						default:
							Console.WriteLine(num + " players connected.");
							break;
						}
						break;
					}
					default:
						if (!(text == ""))
						{
							if (text == "motd")
							{
								if (motd == "")
								{
									Console.WriteLine("Welcome to " + worldName + "!");
								}
								else
								{
									Console.WriteLine("MOTD: " + motd);
								}
							}
							else if (text.Length >= 5 && text.Substring(0, 5) == "motd ")
							{
								string text3 = motd = text2.Substring(5);
							}
							else if (text.Length == 8 && text.Substring(0, 8) == "password")
							{
								if (Netplay.password == "")
								{
									Console.WriteLine("No password set.");
								}
								else
								{
									Console.WriteLine("Password: " + Netplay.password);
								}
							}
							else if (text.Length >= 9 && text.Substring(0, 9) == "password ")
							{
								string text4 = text2.Substring(9);
								if (text4 == "")
								{
									Netplay.password = "";
									Console.WriteLine("Password disabled.");
								}
								else
								{
									Netplay.password = text4;
									Console.WriteLine("Password: " + Netplay.password);
								}
							}
							else if (text == "say")
							{
								Console.WriteLine("Usage: say <words>");
							}
							else if (text.Length >= 4 && text.Substring(0, 4) == "say ")
							{
								string text5 = text2.Substring(4);
								if (text5 == "")
								{
									Console.WriteLine("Usage: say <words>");
								}
								else
								{
									Console.WriteLine("<Server> " + text5);
									NetMessage.SendData(25, -1, -1, "<Server> " + text5, 255, 255f, 240f, 20f);
								}
							}
							else if (text.Length == 4 && text.Substring(0, 4) == "kick")
							{
								Console.WriteLine("Usage: kick <player>");
							}
							else if (text.Length >= 5 && text.Substring(0, 5) == "kick ")
							{
								string text6 = text.Substring(5);
								text6 = text6.ToLower();
								if (text6 == "")
								{
									Console.WriteLine("Usage: kick <player>");
								}
								else
								{
									for (int i = 0; i < 255; i++)
									{
										if (player[i].active && player[i].name.ToLower() == text6)
										{
											NetMessage.SendData(2, i, -1, "Kicked from server.");
										}
									}
								}
							}
							else if (text.Length == 3 && text.Substring(0, 3) == "ban")
							{
								Console.WriteLine("Usage: ban <player>");
							}
							else if (text.Length >= 4 && text.Substring(0, 4) == "ban ")
							{
								string text7 = text.Substring(4);
								text7 = text7.ToLower();
								if (text7 == "")
								{
									Console.WriteLine("Usage: ban <player>");
								}
								else
								{
									for (int j = 0; j < 255; j++)
									{
										if (player[j].active && player[j].name.ToLower() == text7)
										{
											Netplay.AddBan(j);
											NetMessage.SendData(2, j, -1, "Banned from server.");
										}
									}
								}
							}
							else
							{
								Console.WriteLine("'" + text + "' is an Invalid command.");
							}
						}
						break;
					}
				}
				catch
				{
					Console.WriteLine("Invalid command.");
				}
			}
		}

		public Main()
		{
			if (Constants.betaRelease)
			{
				versionNumber += " Beta";
				versionNumber2 += " Beta";
			}
			GraphicsProfile graphicsProfile = GraphicsProfile.Reach;
			if (GraphicsAdapter.DefaultAdapter.IsProfileSupported(GraphicsProfile.HiDef))
			{
				graphicsProfile = GraphicsProfile.HiDef;
			}
			graphics = new GraphicsDeviceManager(this)
			{
				GraphicsProfile = graphicsProfile
			};
			base.Content.RootDirectory = "Content";
			Config.mainInstance = this;
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
			{
				Assembly value = null;
				Config.assemblies.TryGetValue(args.Name, out value);
				return value;
			};
			Interface.Initialize(this);
		}

		public void SetTitle()
		{
			base.Window.Title = Lang.title();
		}

		public void PublicInit(bool loadMods = true, int netMode = 1, bool reloadPlayers = true)
		{
			int num = selectedPlayer;
			int num2 = myPlayer;
			InitData(loadMods);
			if (!dedServ)
			{
				LoadContent();
			}
			if (loadMods && reloadPlayers)
			{
				LoadPlayers();
				selectedPlayer = num;
				myPlayer = num2;
				playerPathName = loadPlayerPath[selectedPlayer];
				player[myPlayer] = Player.LoadPlayer(playerPathName);
			}
			Main.netMode = netMode;
		}

		public void InitData(bool loadMods = true)
		{
			itemTexture = new TextureHandler(604);
			npcTexture = new TextureHandler(147);
			projectileTexture = new TextureHandler(112);
			tileTexture = new TextureHandler(150);
			keyString = new string[10];
			keyInt = new int[10];
			if (rand == null)
			{
				rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			projHostile = new ArrayHandler<bool>(112);
			pvpBuff = new ArrayHandler<bool>(41);
			NPC.clrNames();
			NPC.setNames();
			bgAlpha[0] = 1f;
			bgAlpha2[0] = 1f;
			for (int i = 0; i < 112; i++)
			{
				projFrames[i] = 1;
			}
			projFrames[72] = 4;
			projFrames[86] = 4;
			projFrames[87] = 4;
			projFrames[102] = 2;
			projFrames[111] = 8;
			pvpBuff[20] = true;
			pvpBuff[24] = true;
			pvpBuff[31] = true;
			pvpBuff[39] = true;
			debuff = new ArrayHandler<bool>(41);
			buffAlpha = new ArrayHandler<float>(41);
			buffName = new ArrayHandler<string>(41);
			buffTip = new ArrayHandler<string>(41);
			buffDontDisplayTime = new ArrayHandler<bool>(41);
			debuff[20] = true;
			debuff[21] = true;
			debuff[22] = true;
			debuff[23] = true;
			debuff[24] = true;
			debuff[25] = true;
			debuff[28] = true;
			debuff[30] = true;
			debuff[31] = true;
			debuff[32] = true;
			debuff[33] = true;
			debuff[34] = true;
			debuff[35] = true;
			debuff[36] = true;
			debuff[37] = true;
			debuff[38] = true;
			debuff[39] = true;
			buffDontDisplayTime[28] = true;
			buffDontDisplayTime[34] = true;
			buffDontDisplayTime[37] = true;
			buffDontDisplayTime[38] = true;
			buffDontDisplayTime[40] = true;
			lo = rand.Next(6);
			tileShine2[6] = true;
			tileShine2[7] = true;
			tileShine2[8] = true;
			tileShine2[9] = true;
			tileShine2[12] = true;
			tileShine2[21] = true;
			tileShine2[22] = true;
			tileShine2[25] = true;
			tileShine2[45] = true;
			tileShine2[46] = true;
			tileShine2[47] = true;
			tileShine2[63] = true;
			tileShine2[64] = true;
			tileShine2[65] = true;
			tileShine2[66] = true;
			tileShine2[67] = true;
			tileShine2[68] = true;
			tileShine2[107] = true;
			tileShine2[108] = true;
			tileShine2[111] = true;
			tileShine2[121] = true;
			tileShine2[122] = true;
			tileShine2[117] = true;
			tileShine[129] = 300;
			tileHammer[141] = true;
			tileHammer[4] = true;
			tileHammer[10] = true;
			tileHammer[11] = true;
			tileHammer[12] = true;
			tileHammer[13] = true;
			tileHammer[14] = true;
			tileHammer[15] = true;
			tileHammer[16] = true;
			tileHammer[17] = true;
			tileHammer[18] = true;
			tileHammer[19] = true;
			tileHammer[21] = true;
			tileHammer[26] = true;
			tileHammer[28] = true;
			tileHammer[29] = true;
			tileHammer[31] = true;
			tileHammer[33] = true;
			tileHammer[34] = true;
			tileHammer[35] = true;
			tileHammer[36] = true;
			tileHammer[42] = true;
			tileHammer[48] = true;
			tileHammer[49] = true;
			tileHammer[50] = true;
			tileHammer[54] = true;
			tileHammer[55] = true;
			tileHammer[77] = true;
			tileHammer[78] = true;
			tileHammer[79] = true;
			tileHammer[81] = true;
			tileHammer[85] = true;
			tileHammer[86] = true;
			tileHammer[87] = true;
			tileHammer[88] = true;
			tileHammer[89] = true;
			tileHammer[90] = true;
			tileHammer[91] = true;
			tileHammer[92] = true;
			tileHammer[93] = true;
			tileHammer[94] = true;
			tileHammer[95] = true;
			tileHammer[96] = true;
			tileHammer[97] = true;
			tileHammer[98] = true;
			tileHammer[99] = true;
			tileHammer[100] = true;
			tileHammer[101] = true;
			tileHammer[102] = true;
			tileHammer[103] = true;
			tileHammer[104] = true;
			tileHammer[105] = true;
			tileHammer[106] = true;
			tileHammer[114] = true;
			tileHammer[125] = true;
			tileHammer[126] = true;
			tileHammer[128] = true;
			tileHammer[129] = true;
			tileHammer[132] = true;
			tileHammer[133] = true;
			tileHammer[134] = true;
			tileHammer[135] = true;
			tileHammer[136] = true;
			tileFrameImportant[139] = true;
			tileHammer[139] = true;
			tileLighted[149] = true;
			tileFrameImportant[149] = true;
			tileHammer[149] = true;
			tileFrameImportant[142] = true;
			tileHammer[142] = true;
			tileFrameImportant[143] = true;
			tileHammer[143] = true;
			tileFrameImportant[144] = true;
			tileHammer[144] = true;
			tileStone[131] = true;
			tileFrameImportant[136] = true;
			tileFrameImportant[137] = true;
			tileFrameImportant[138] = true;
			tileBlockLight[137] = true;
			tileSolid[137] = true;
			tileBlockLight[145] = true;
			tileSolid[145] = true;
			tileMergeDirt[145] = true;
			tileBlockLight[146] = true;
			tileSolid[146] = true;
			tileMergeDirt[146] = true;
			tileBlockLight[147] = true;
			tileSolid[147] = true;
			tileMergeDirt[147] = true;
			tileBlockLight[148] = true;
			tileSolid[148] = true;
			tileMergeDirt[148] = true;
			tileBlockLight[138] = true;
			tileSolid[138] = true;
			tileBlockLight[140] = true;
			tileSolid[140] = true;
			tileAxe[5] = true;
			tileAxe[30] = true;
			tileAxe[72] = true;
			tileAxe[80] = true;
			tileAxe[124] = true;
			tileShine[22] = 1150;
			tileShine[6] = 1150;
			tileShine[7] = 1100;
			tileShine[8] = 1000;
			tileShine[9] = 1050;
			tileShine[12] = 1000;
			tileShine[21] = 1200;
			tileShine[63] = 900;
			tileShine[64] = 900;
			tileShine[65] = 900;
			tileShine[66] = 900;
			tileShine[67] = 900;
			tileShine[68] = 900;
			tileShine[45] = 1900;
			tileShine[46] = 2000;
			tileShine[47] = 2100;
			tileShine[122] = 1800;
			tileShine[121] = 1850;
			tileShine[125] = 600;
			tileShine[109] = 9000;
			tileShine[110] = 9000;
			tileShine[116] = 9000;
			tileShine[117] = 9000;
			tileShine[118] = 8000;
			tileShine[107] = 950;
			tileShine[108] = 900;
			tileShine[111] = 850;
			tileLighted[4] = true;
			tileLighted[17] = true;
			tileLighted[133] = true;
			tileLighted[31] = true;
			tileLighted[33] = true;
			tileLighted[34] = true;
			tileLighted[35] = true;
			tileLighted[36] = true;
			tileLighted[37] = true;
			tileLighted[42] = true;
			tileLighted[49] = true;
			tileLighted[58] = true;
			tileLighted[61] = true;
			tileLighted[70] = true;
			tileLighted[71] = true;
			tileLighted[72] = true;
			tileLighted[76] = true;
			tileLighted[77] = true;
			tileLighted[19] = true;
			tileLighted[22] = true;
			tileLighted[26] = true;
			tileLighted[83] = true;
			tileLighted[84] = true;
			tileLighted[92] = true;
			tileLighted[93] = true;
			tileLighted[95] = true;
			tileLighted[98] = true;
			tileLighted[100] = true;
			tileLighted[109] = true;
			tileLighted[125] = true;
			tileLighted[126] = true;
			tileLighted[129] = true;
			tileLighted[140] = true;
			tileMergeDirt[1] = true;
			tileMergeDirt[6] = true;
			tileMergeDirt[7] = true;
			tileMergeDirt[8] = true;
			tileMergeDirt[9] = true;
			tileMergeDirt[22] = true;
			tileMergeDirt[25] = true;
			tileMergeDirt[30] = true;
			tileMergeDirt[37] = true;
			tileMergeDirt[38] = true;
			tileMergeDirt[40] = true;
			tileMergeDirt[53] = true;
			tileMergeDirt[56] = true;
			tileMergeDirt[107] = true;
			tileMergeDirt[108] = true;
			tileMergeDirt[111] = true;
			tileMergeDirt[112] = true;
			tileMergeDirt[116] = true;
			tileMergeDirt[117] = true;
			tileMergeDirt[123] = true;
			tileMergeDirt[140] = true;
			tileMergeDirt[39] = true;
			tileMergeDirt[122] = true;
			tileMergeDirt[121] = true;
			tileMergeDirt[120] = true;
			tileMergeDirt[119] = true;
			tileMergeDirt[118] = true;
			tileMergeDirt[47] = true;
			tileMergeDirt[46] = true;
			tileMergeDirt[45] = true;
			tileMergeDirt[44] = true;
			tileMergeDirt[43] = true;
			tileMergeDirt[41] = true;
			tileFrameImportant[3] = true;
			tileFrameImportant[4] = true;
			tileFrameImportant[5] = true;
			tileFrameImportant[10] = true;
			tileFrameImportant[11] = true;
			tileFrameImportant[12] = true;
			tileFrameImportant[13] = true;
			tileFrameImportant[14] = true;
			tileFrameImportant[15] = true;
			tileFrameImportant[16] = true;
			tileFrameImportant[17] = true;
			tileFrameImportant[18] = true;
			tileFrameImportant[20] = true;
			tileFrameImportant[21] = true;
			tileFrameImportant[24] = true;
			tileFrameImportant[26] = true;
			tileFrameImportant[27] = true;
			tileFrameImportant[28] = true;
			tileFrameImportant[29] = true;
			tileFrameImportant[31] = true;
			tileFrameImportant[33] = true;
			tileFrameImportant[34] = true;
			tileFrameImportant[35] = true;
			tileFrameImportant[36] = true;
			tileFrameImportant[42] = true;
			tileFrameImportant[50] = true;
			tileFrameImportant[55] = true;
			tileFrameImportant[61] = true;
			tileFrameImportant[71] = true;
			tileFrameImportant[72] = true;
			tileFrameImportant[73] = true;
			tileFrameImportant[74] = true;
			tileFrameImportant[77] = true;
			tileFrameImportant[78] = true;
			tileFrameImportant[79] = true;
			tileFrameImportant[81] = true;
			tileFrameImportant[82] = true;
			tileFrameImportant[83] = true;
			tileFrameImportant[84] = true;
			tileFrameImportant[85] = true;
			tileFrameImportant[86] = true;
			tileFrameImportant[87] = true;
			tileFrameImportant[88] = true;
			tileFrameImportant[89] = true;
			tileFrameImportant[90] = true;
			tileFrameImportant[91] = true;
			tileFrameImportant[92] = true;
			tileFrameImportant[93] = true;
			tileFrameImportant[94] = true;
			tileFrameImportant[95] = true;
			tileFrameImportant[96] = true;
			tileFrameImportant[97] = true;
			tileFrameImportant[98] = true;
			tileFrameImportant[99] = true;
			tileFrameImportant[101] = true;
			tileFrameImportant[102] = true;
			tileFrameImportant[103] = true;
			tileFrameImportant[104] = true;
			tileFrameImportant[105] = true;
			tileFrameImportant[100] = true;
			tileFrameImportant[106] = true;
			tileFrameImportant[110] = true;
			tileFrameImportant[113] = true;
			tileFrameImportant[114] = true;
			tileFrameImportant[125] = true;
			tileFrameImportant[126] = true;
			tileFrameImportant[128] = true;
			tileFrameImportant[129] = true;
			tileFrameImportant[132] = true;
			tileFrameImportant[133] = true;
			tileFrameImportant[134] = true;
			tileFrameImportant[135] = true;
			tileFrameImportant[141] = true;
			tileCut[3] = true;
			tileCut[24] = true;
			tileCut[28] = true;
			tileCut[32] = true;
			tileCut[51] = true;
			tileCut[52] = true;
			tileCut[61] = true;
			tileCut[62] = true;
			tileCut[69] = true;
			tileCut[71] = true;
			tileCut[73] = true;
			tileCut[74] = true;
			tileCut[82] = true;
			tileCut[83] = true;
			tileCut[84] = true;
			tileCut[110] = true;
			tileCut[113] = true;
			tileCut[115] = true;
			tileAlch[82] = true;
			tileAlch[83] = true;
			tileAlch[84] = true;
			tileLavaDeath[104] = true;
			tileLavaDeath[110] = true;
			tileLavaDeath[113] = true;
			tileLavaDeath[115] = true;
			tileSolid[127] = true;
			tileSolid[130] = true;
			tileBlockLight[130] = true;
			tileBlockLight[131] = true;
			tileSolid[107] = true;
			tileBlockLight[107] = true;
			tileSolid[108] = true;
			tileBlockLight[108] = true;
			tileSolid[111] = true;
			tileBlockLight[111] = true;
			tileSolid[109] = true;
			tileBlockLight[109] = true;
			tileSolid[110] = false;
			tileNoAttach[110] = true;
			tileNoFail[110] = true;
			tileSolid[112] = true;
			tileBlockLight[112] = true;
			tileSolid[116] = true;
			tileBlockLight[116] = true;
			tileSolid[117] = true;
			tileBlockLight[117] = true;
			tileSolid[123] = true;
			tileBlockLight[123] = true;
			tileSolid[118] = true;
			tileBlockLight[118] = true;
			tileSolid[119] = true;
			tileBlockLight[119] = true;
			tileSolid[120] = true;
			tileBlockLight[120] = true;
			tileSolid[121] = true;
			tileBlockLight[121] = true;
			tileSolid[122] = true;
			tileBlockLight[122] = true;
			tileBlockLight[115] = true;
			tileSolid[0] = true;
			tileBlockLight[0] = true;
			tileSolid[1] = true;
			tileBlockLight[1] = true;
			tileSolid[2] = true;
			tileBlockLight[2] = true;
			tileSolid[3] = false;
			tileNoAttach[3] = true;
			tileNoFail[3] = true;
			tileSolid[4] = false;
			tileNoAttach[4] = true;
			tileNoFail[4] = true;
			tileNoFail[24] = true;
			tileSolid[5] = false;
			tileSolid[6] = true;
			tileBlockLight[6] = true;
			tileSolid[7] = true;
			tileBlockLight[7] = true;
			tileSolid[8] = true;
			tileBlockLight[8] = true;
			tileSolid[9] = true;
			tileBlockLight[9] = true;
			tileBlockLight[10] = true;
			tileSolid[10] = true;
			tileNoAttach[10] = true;
			tileBlockLight[10] = true;
			tileSolid[11] = false;
			tileSolidTop[19] = true;
			tileSolid[19] = true;
			tileSolid[22] = true;
			tileSolid[23] = true;
			tileSolid[25] = true;
			tileSolid[30] = true;
			tileNoFail[32] = true;
			tileBlockLight[32] = true;
			tileSolid[37] = true;
			tileBlockLight[37] = true;
			tileSolid[38] = true;
			tileBlockLight[38] = true;
			tileSolid[39] = true;
			tileBlockLight[39] = true;
			tileSolid[40] = true;
			tileBlockLight[40] = true;
			tileSolid[41] = true;
			tileBlockLight[41] = true;
			tileSolid[43] = true;
			tileBlockLight[43] = true;
			tileSolid[44] = true;
			tileBlockLight[44] = true;
			tileSolid[45] = true;
			tileBlockLight[45] = true;
			tileSolid[46] = true;
			tileBlockLight[46] = true;
			tileSolid[47] = true;
			tileBlockLight[47] = true;
			tileSolid[48] = true;
			tileBlockLight[48] = true;
			tileSolid[53] = true;
			tileBlockLight[53] = true;
			tileSolid[54] = true;
			tileBlockLight[52] = true;
			tileSolid[56] = true;
			tileBlockLight[56] = true;
			tileSolid[57] = true;
			tileBlockLight[57] = true;
			tileSolid[58] = true;
			tileBlockLight[58] = true;
			tileSolid[59] = true;
			tileBlockLight[59] = true;
			tileSolid[60] = true;
			tileBlockLight[60] = true;
			tileSolid[63] = true;
			tileBlockLight[63] = true;
			tileStone[63] = true;
			tileStone[130] = true;
			tileSolid[64] = true;
			tileBlockLight[64] = true;
			tileStone[64] = true;
			tileSolid[65] = true;
			tileBlockLight[65] = true;
			tileStone[65] = true;
			tileSolid[66] = true;
			tileBlockLight[66] = true;
			tileStone[66] = true;
			tileSolid[67] = true;
			tileBlockLight[67] = true;
			tileStone[67] = true;
			tileSolid[68] = true;
			tileBlockLight[68] = true;
			tileStone[68] = true;
			tileSolid[75] = true;
			tileBlockLight[75] = true;
			tileSolid[76] = true;
			tileBlockLight[76] = true;
			tileSolid[70] = true;
			tileBlockLight[70] = true;
			tileNoFail[50] = true;
			tileNoAttach[50] = true;
			tileDungeon[41] = true;
			tileDungeon[43] = true;
			tileDungeon[44] = true;
			tileBlockLight[30] = true;
			tileBlockLight[25] = true;
			tileBlockLight[23] = true;
			tileBlockLight[22] = true;
			tileBlockLight[62] = true;
			tileSolidTop[18] = true;
			tileSolidTop[14] = true;
			tileSolidTop[16] = true;
			tileSolidTop[114] = true;
			tileNoAttach[20] = true;
			tileNoAttach[19] = true;
			tileNoAttach[13] = true;
			tileNoAttach[14] = true;
			tileNoAttach[15] = true;
			tileNoAttach[16] = true;
			tileNoAttach[17] = true;
			tileNoAttach[18] = true;
			tileNoAttach[19] = true;
			tileNoAttach[21] = true;
			tileNoAttach[27] = true;
			tileNoAttach[114] = true;
			tileTable[14] = true;
			tileTable[18] = true;
			tileTable[19] = true;
			tileTable[114] = true;
			tileNoAttach[86] = true;
			tileNoAttach[87] = true;
			tileNoAttach[88] = true;
			tileNoAttach[89] = true;
			tileNoAttach[90] = true;
			tileLavaDeath[86] = true;
			tileLavaDeath[87] = true;
			tileLavaDeath[88] = true;
			tileLavaDeath[89] = true;
			tileLavaDeath[125] = true;
			tileLavaDeath[126] = true;
			tileLavaDeath[101] = true;
			tileTable[101] = true;
			tileNoAttach[101] = true;
			tileLavaDeath[102] = true;
			tileNoAttach[102] = true;
			tileNoAttach[94] = true;
			tileNoAttach[95] = true;
			tileNoAttach[96] = true;
			tileNoAttach[97] = true;
			tileNoAttach[98] = true;
			tileNoAttach[99] = true;
			tileLavaDeath[94] = true;
			tileLavaDeath[95] = true;
			tileLavaDeath[96] = true;
			tileLavaDeath[97] = true;
			tileLavaDeath[98] = true;
			tileLavaDeath[99] = true;
			tileLavaDeath[100] = true;
			tileLavaDeath[103] = true;
			tileTable[87] = true;
			tileTable[88] = true;
			tileSolidTop[87] = true;
			tileSolidTop[88] = true;
			tileSolidTop[101] = true;
			tileNoAttach[91] = true;
			tileLavaDeath[91] = true;
			tileNoAttach[92] = true;
			tileLavaDeath[92] = true;
			tileNoAttach[93] = true;
			tileLavaDeath[93] = true;
			tileWaterDeath[4] = true;
			tileWaterDeath[51] = true;
			tileWaterDeath[93] = true;
			tileWaterDeath[98] = true;
			tileLavaDeath[3] = true;
			tileLavaDeath[5] = true;
			tileLavaDeath[10] = true;
			tileLavaDeath[11] = true;
			tileLavaDeath[12] = true;
			tileLavaDeath[13] = true;
			tileLavaDeath[14] = true;
			tileLavaDeath[15] = true;
			tileLavaDeath[16] = true;
			tileLavaDeath[17] = true;
			tileLavaDeath[18] = true;
			tileLavaDeath[19] = true;
			tileLavaDeath[20] = true;
			tileLavaDeath[27] = true;
			tileLavaDeath[28] = true;
			tileLavaDeath[29] = true;
			tileLavaDeath[32] = true;
			tileLavaDeath[33] = true;
			tileLavaDeath[34] = true;
			tileLavaDeath[35] = true;
			tileLavaDeath[36] = true;
			tileLavaDeath[42] = true;
			tileLavaDeath[49] = true;
			tileLavaDeath[50] = true;
			tileLavaDeath[52] = true;
			tileLavaDeath[55] = true;
			tileLavaDeath[61] = true;
			tileLavaDeath[62] = true;
			tileLavaDeath[69] = true;
			tileLavaDeath[71] = true;
			tileLavaDeath[72] = true;
			tileLavaDeath[73] = true;
			tileLavaDeath[74] = true;
			tileLavaDeath[79] = true;
			tileLavaDeath[80] = true;
			tileLavaDeath[81] = true;
			tileLavaDeath[106] = true;
			wallHouse[1] = true;
			wallHouse[4] = true;
			wallHouse[5] = true;
			wallHouse[6] = true;
			wallHouse[10] = true;
			wallHouse[11] = true;
			wallHouse[12] = true;
			wallHouse[16] = true;
			wallHouse[17] = true;
			wallHouse[18] = true;
			wallHouse[19] = true;
			wallHouse[20] = true;
			wallHouse[21] = true;
			wallHouse[22] = true;
			wallHouse[23] = true;
			wallHouse[24] = true;
			wallHouse[25] = true;
			wallHouse[26] = true;
			wallHouse[27] = true;
			wallHouse[29] = true;
			wallHouse[30] = true;
			wallHouse[31] = true;
			for (int j = 0; j < 32; j++)
			{
				switch (j)
				{
				case 20:
					wallBlend[j] = 14;
					break;
				case 19:
					wallBlend[j] = 9;
					break;
				case 18:
					wallBlend[j] = 8;
					break;
				case 17:
					wallBlend[j] = 7;
					break;
				case 16:
					wallBlend[j] = 2;
					break;
				default:
					wallBlend[j] = j;
					break;
				}
			}
			tileNoFail[32] = true;
			tileNoFail[61] = true;
			tileNoFail[69] = true;
			tileNoFail[73] = true;
			tileNoFail[74] = true;
			tileNoFail[82] = true;
			tileNoFail[83] = true;
			tileNoFail[84] = true;
			tileNoFail[110] = true;
			tileNoFail[113] = true;
			for (int k = 0; k < 150; k++)
			{
				tileName[k] = "";
				if (tileSolid[k])
				{
					tileNoSunLight[k] = true;
				}
			}
			tileNoSunLight[19] = false;
			tileNoSunLight[11] = true;
			for (int l = 0; l < maxMenuItems; l++)
			{
				menuItemScale[l] = 0.8f;
			}
			for (int m = 0; m < 2001; m++)
			{
				dust[m] = new Dust();
			}
			for (int n = 0; n < 201; n++)
			{
				Main.item[n] = new Item();
			}
			for (int num = 0; num < 201; num++)
			{
				npc[num] = new NPC();
				npc[num].whoAmI = num;
			}
			for (int num2 = 0; num2 < 1001; num2++)
			{
				Main.projectile[num2] = new Projectile();
			}
			for (int num3 = 0; num3 < 201; num3++)
			{
				gore[num3] = new Gore();
			}
			for (int num4 = 0; num4 < 100; num4++)
			{
				cloud[num4] = new Cloud();
			}
			for (int num5 = 0; num5 < 100; num5++)
			{
				combatText[num5] = new CombatText();
			}
			for (int num6 = 0; num6 < 20; num6++)
			{
				itemText[num6] = new ItemText();
			}
			clientPlayer = new Player();
			for (int num7 = 0; num7 < 256; num7++)
			{
				player[num7] = new Player();
			}
			Lang.setLang();
			Config.LoadData(loadMods);
			for (int num8 = 1; num8 < 112; num8++)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(num8);
				if (projectile.hostile)
				{
					projHostile[num8] = true;
				}
			}
			for (int num9 = 0; num9 < 604; num9++)
			{
				Item item = new Item();
				item.SetDefaults(num9);
				itemName[num9] = item.name;
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
			}
			Config.Initialize();
			clientPlayer = new Player(modsLoaded: true);
			for (int num10 = 0; num10 < 256; num10++)
			{
				player[num10] = new Player(modsLoaded: true);
			}
			for (int num11 = 0; num11 < numChatLines; num11++)
			{
				chatLine[num11] = new ChatLine();
			}
			for (int num12 = 0; num12 < Liquid.resLiquid; num12++)
			{
				liquid[num12] = new Liquid();
			}
			for (int num13 = 0; num13 < 10000; num13++)
			{
				liquidBuffer[num13] = new LiquidBuffer();
			}
			shop[0] = new Chest();
			shop[1] = new Chest();
			shop[1].SetupShop(1);
			shop[2] = new Chest();
			shop[2].SetupShop(2);
			shop[3] = new Chest();
			shop[3].SetupShop(3);
			shop[4] = new Chest();
			shop[4].SetupShop(4);
			shop[5] = new Chest();
			shop[5].SetupShop(5);
			shop[6] = new Chest();
			shop[6].SetupShop(6);
			shop[7] = new Chest();
			shop[7].SetupShop(7);
			shop[8] = new Chest();
			shop[8].SetupShop(8);
			shop[9] = new Chest();
			shop[9].SetupShop(9);
			teamColor[0] = Color.White;
			teamColor[1] = new Color(230, 40, 20);
			teamColor[2] = new Color(20, 200, 30);
			teamColor[3] = new Color(75, 90, 255);
			teamColor[4] = new Color(200, 180, 0);
			tileMerge = new bool[150 + Config.customTileAmt + 1][];
			for (int num14 = 0; num14 < tileMerge.Length; num14++)
			{
				tileMerge[num14] = new bool[150 + Config.customTileAmt + 1];
			}
			Biome.InitBiomes();
			if (dedServ)
			{
				Codable.RunGlobalMethod("ModGeneric", "OnLoad");
			}
		}

		protected override void Initialize()
		{
			for (int i = 0; i < 10; i++)
			{
				recentWorld[i] = "";
				recentIP[i] = "";
				recentPort[i] = 0;
			}
			if (rand == null)
			{
				rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			InitData(dedServ);
			if (menuMode == 1)
			{
				LoadPlayers();
			}
			Netplay.clientSock = new ClientSock();
			Netplay.Init();
			if (skipMenu)
			{
				WorldGen.clearWorld();
				gameMenu = false;
				LoadPlayers();
				player[myPlayer] = (Player)loadPlayer[0].Clone();
				PlayerPath = loadPlayerPath[0];
				LoadWorlds();
				WorldGen.generateWorld();
				WorldGen.EveryTileFrame();
				player[myPlayer].Spawn();
				OnScreenInterface.Setup();
			}
			else
			{
				IntPtr systemMenu = GetSystemMenu(base.Window.Handle, bRevert: false);
				int menuItemCount = GetMenuItemCount(systemMenu);
				RemoveMenu(systemMenu, menuItemCount - 1, 1024);
			}
			if (!dedServ)
			{
				keyBoardInput.ResetSubscriptions();
				keyBoardInput.newKeyEvent += delegate(char keyStroke)
				{
					if (keyCount < 10)
					{
						keyInt[keyCount] = keyStroke;
						keyString[keyCount] = string.Concat(keyStroke);
						keyCount++;
					}
				};
				graphics.PreferredBackBufferWidth = screenWidth;
				graphics.PreferredBackBufferHeight = screenHeight;
				graphics.ApplyChanges();
				base.Initialize();
				base.Window.AllowUserResizing = true;
				OpenSettings();
				CheckBunny();
				if (Lang.lang == 0)
				{
					menuMode = 1212;
				}
				SetTitle();
				OpenRecent();
				Star.SpawnStars();
				foreach (DisplayMode supportedDisplayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
				{
					if (supportedDisplayMode.Width >= minScreenW && supportedDisplayMode.Height >= minScreenH && supportedDisplayMode.Width <= maxScreenW && supportedDisplayMode.Height <= maxScreenH)
					{
						bool flag = true;
						for (int j = 0; j < numDisplayModes; j++)
						{
							if (supportedDisplayMode.Width == displayWidth[j] && supportedDisplayMode.Height == displayHeight[j])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							displayHeight[numDisplayModes] = supportedDisplayMode.Height;
							displayWidth[numDisplayModes] = supportedDisplayMode.Width;
							numDisplayModes++;
						}
					}
				}
				if (autoJoin)
				{
					LoadPlayers();
					menuMode = 1;
					menuMultiplayer = true;
				}
				fpsTimer.Start();
				updateTimer.Start();
				if (File.Exists(SavePath + Path.DirectorySeparatorChar + "tconfig-errorlog.txt"))
				{
					File.Delete(SavePath + Path.DirectorySeparatorChar + "tconfig-errorlog.txt");
				}
				Config.ReInitialize();
			}
		}

		protected override void LoadContent()
		{
			try
			{
				engine = new AudioEngine("Content" + Path.DirectorySeparatorChar + "TerrariaMusic.xgs");
				soundBank = new SoundBank(engine, "Content" + Path.DirectorySeparatorChar + "Sound Bank.xsb");
				waveBank = new WaveBank(engine, "Content" + Path.DirectorySeparatorChar + "Wave Bank.xwb");
				Audio.Player.Init();
				for (int i = 1; i < 14; i++)
				{
					music[i] = soundBank.GetCue("Music_" + i);
				}
				soundMech[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Mech_0");
				soundInstanceMech[0] = soundMech[0].CreateInstance();
				soundGrab = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Grab");
				soundInstanceGrab = soundGrab.CreateInstance();
				soundPixie = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Pixie");
				soundInstancePixie = soundGrab.CreateInstance();
				soundDig[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_0");
				soundInstanceDig[0] = soundDig[0].CreateInstance();
				soundDig[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_1");
				soundInstanceDig[1] = soundDig[1].CreateInstance();
				soundDig[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Dig_2");
				soundInstanceDig[2] = soundDig[2].CreateInstance();
				soundTink[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_0");
				soundInstanceTink[0] = soundTink[0].CreateInstance();
				soundTink[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_1");
				soundInstanceTink[1] = soundTink[1].CreateInstance();
				soundTink[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Tink_2");
				soundInstanceTink[2] = soundTink[2].CreateInstance();
				soundPlayerHit[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_0");
				soundInstancePlayerHit[0] = soundPlayerHit[0].CreateInstance();
				soundPlayerHit[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_1");
				soundInstancePlayerHit[1] = soundPlayerHit[1].CreateInstance();
				soundPlayerHit[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Hit_2");
				soundInstancePlayerHit[2] = soundPlayerHit[2].CreateInstance();
				soundFemaleHit[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_0");
				soundInstanceFemaleHit[0] = soundFemaleHit[0].CreateInstance();
				soundFemaleHit[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_1");
				soundInstanceFemaleHit[1] = soundFemaleHit[1].CreateInstance();
				soundFemaleHit[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Female_Hit_2");
				soundInstanceFemaleHit[2] = soundFemaleHit[2].CreateInstance();
				soundPlayerKilled = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Player_Killed");
				soundInstancePlayerKilled = soundPlayerKilled.CreateInstance();
				soundChat = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Chat");
				soundInstanceChat = soundChat.CreateInstance();
				soundGrass = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Grass");
				soundInstanceGrass = soundGrass.CreateInstance();
				soundDoorOpen = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Door_Opened");
				soundInstanceDoorOpen = soundDoorOpen.CreateInstance();
				soundDoorClosed = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Door_Closed");
				soundInstanceDoorClosed = soundDoorClosed.CreateInstance();
				soundMenuTick = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Tick");
				soundInstanceMenuTick = soundMenuTick.CreateInstance();
				soundMenuOpen = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Open");
				soundInstanceMenuOpen = soundMenuOpen.CreateInstance();
				soundMenuClose = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Menu_Close");
				soundInstanceMenuClose = soundMenuClose.CreateInstance();
				soundShatter = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Shatter");
				soundInstanceShatter = soundShatter.CreateInstance();
				soundZombie[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_0");
				soundInstanceZombie[0] = soundZombie[0].CreateInstance();
				soundZombie[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_1");
				soundInstanceZombie[1] = soundZombie[1].CreateInstance();
				soundZombie[2] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_2");
				soundInstanceZombie[2] = soundZombie[2].CreateInstance();
				soundZombie[3] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_3");
				soundInstanceZombie[3] = soundZombie[3].CreateInstance();
				soundZombie[4] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Zombie_4");
				soundInstanceZombie[4] = soundZombie[4].CreateInstance();
				soundRoar[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Roar_0");
				soundInstanceRoar[0] = soundRoar[0].CreateInstance();
				soundRoar[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Roar_1");
				soundInstanceRoar[1] = soundRoar[1].CreateInstance();
				soundSplash[0] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Splash_0");
				soundInstanceSplash[0] = soundRoar[0].CreateInstance();
				soundSplash[1] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Splash_1");
				soundInstanceSplash[1] = soundSplash[1].CreateInstance();
				soundDoubleJump = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Double_Jump");
				soundInstanceDoubleJump = soundRoar[0].CreateInstance();
				soundRun = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Run");
				soundInstanceRun = soundRun.CreateInstance();
				soundCoins = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Coins");
				soundInstanceCoins = soundCoins.CreateInstance();
				soundUnlock = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Unlock");
				soundInstanceUnlock = soundUnlock.CreateInstance();
				soundMaxMana = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "MaxMana");
				soundInstanceMaxMana = soundMaxMana.CreateInstance();
				soundDrown = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Drown");
				soundInstanceDrown = soundDrown.CreateInstance();
				for (int j = 1; j < 38; j++)
				{
					soundItem[j] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "Item_" + j);
					soundInstanceItem[j] = soundItem[j].CreateInstance();
				}
				for (int k = 1; k < 12; k++)
				{
					soundNPCHit[k] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "NPC_Hit_" + k);
					soundInstanceNPCHit[k] = soundNPCHit[k].CreateInstance();
				}
				for (int l = 1; l < 16; l++)
				{
					soundNPCKilled[l] = base.Content.Load<SoundEffect>("Sounds" + Path.DirectorySeparatorChar + "NPC_Killed_" + l);
					soundInstanceNPCKilled[l] = soundNPCKilled[l].CreateInstance();
				}
			}
			catch
			{
				musicVolume = 0f;
				soundVolume = 0f;
			}
			reforgeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Reforge");
			timerTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Timer");
			wofTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "WallOfFlesh");
			wallOutlineTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wall_Outline");
			raTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "ra-logo");
			reTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "re-logo");
			splashTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "splash");
			fadeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "fade-out");
			ghostTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Ghost");
			evilCactusTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Evil_Cactus");
			goodCactusTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Good_Cactus");
			wraithEyeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wraith_Eyes");
			MusicBoxTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Music_Box");
			wingsTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wings_1");
			wingsTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wings_2");
			destTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest1");
			destTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest2");
			destTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dest3");
			wireTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wires");
			loTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "logo_" + rand.Next(1, 7));
			if (spriteBatch == null)
			{
				spriteBatch = new SpriteBatch(base.GraphicsDevice);
			}
			for (int m = 1; m < 2; m++)
			{
				bannerTexture[m] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "House_Banner_" + m);
			}
			for (int n = 0; n < 12; n++)
			{
				npcHeadTexture[n] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "NPC_Head_" + n);
			}
			for (int num = 0; num < 150; num++)
			{
				tileTexture[num] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tiles_" + num);
			}
			for (int num2 = 1; num2 < 32; num2++)
			{
				wallTexture[num2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Wall_" + num2);
			}
			for (int num3 = 1; num3 < 41; num3++)
			{
				buffTexture[num3] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Buff_" + num3);
			}
			for (int num4 = 0; num4 < 32; num4++)
			{
				backgroundTexture[num4] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Background_" + num4);
				backgroundWidth[num4] = backgroundTexture[num4].Width;
				backgroundHeight[num4] = backgroundTexture[num4].Height;
			}
			for (int num5 = 0; num5 < 604; num5++)
			{
				itemTexture[num5] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Item_" + num5);
			}
			for (int num6 = 0; num6 < 147; num6++)
			{
				npcTexture[num6] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "NPC_" + num6);
			}
			for (int num7 = 0; num7 < 147; num7++)
			{
				NPC nPC = new NPC();
				nPC.SetDefaults(num7);
				npcName[num7] = nPC.name;
			}
			for (int num8 = 0; num8 < 112; num8++)
			{
				projectileTexture[num8] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Projectile_" + num8);
			}
			for (int num9 = 1; num9 < 160; num9++)
			{
				goreTexture[num9] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Gore_" + num9);
			}
			for (int num10 = 0; num10 < 4; num10++)
			{
				cloudTexture[num10] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Cloud_" + num10);
			}
			for (int num11 = 0; num11 < 5; num11++)
			{
				starTexture[num11] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Star_" + num11);
			}
			for (int num12 = 0; num12 < 2; num12++)
			{
				liquidTexture[num12] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Liquid_" + num12);
			}
			npcToggleTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "House_1");
			npcToggleTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "House_2");
			HBLockTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Lock_0");
			HBLockTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Lock_1");
			gridTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Grid");
			trashTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Trash");
			cdTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "CoolDown");
			logoTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo");
			logo2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo2");
			logo3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Logo3");
			dustTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Dust");
			sunTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Sun");
			sun2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Sun2");
			moonTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Moon");
			blackTileTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Black_Tile");
			heartTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Heart");
			bubbleTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bubble");
			manaTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Mana");
			cursorTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Cursor");
			ninjaTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Ninja");
			antLionTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "AntlionBody");
			spikeBaseTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Spike_Base");
			treeTopTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_0");
			treeBranchTexture[0] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_0");
			treeTopTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_1");
			treeBranchTexture[1] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_1");
			treeTopTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_2");
			treeBranchTexture[2] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_2");
			treeTopTexture[3] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_3");
			treeBranchTexture[3] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_3");
			treeTopTexture[4] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Tops_4");
			treeBranchTexture[4] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Tree_Branches_4");
			shroomCapTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Shroom_Tops");
			inventoryBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back");
			inventoryBack2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back2");
			inventoryBack3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back3");
			inventoryBack4Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back4");
			inventoryBack5Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back5");
			inventoryBack6Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back6");
			inventoryBack7Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back7");
			inventoryBack8Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back8");
			inventoryBack9Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back9");
			inventoryBack10Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back10");
			inventoryBack11Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Inventory_Back11");
			textBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Text_Back");
			chatTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat");
			chat2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat2");
			chatBackTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chat_Back");
			teamTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Team");
			for (int num13 = 1; num13 < 26; num13++)
			{
				femaleBodyTexture[num13] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Body_" + num13);
				armorBodyTexture[num13] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Armor_Body_" + num13);
				armorArmTexture[num13] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Armor_Arm_" + num13);
			}
			for (int num14 = 1; num14 < 45; num14++)
			{
				armorHeadTexture[num14] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Armor_Head_" + num14);
			}
			for (int num15 = 1; num15 < 25; num15++)
			{
				armorLegTexture[num15] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Armor_Legs_" + num15);
			}
			for (int num16 = 0; num16 < 36; num16++)
			{
				playerHairTexture[num16] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Hair_" + (num16 + 1));
				playerHairAltTexture[num16] = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_HairAlt_" + (num16 + 1));
			}
			skinBodyTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Skin_Body");
			skinLegsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Skin_Legs");
			playerEyeWhitesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Eye_Whites");
			playerEyesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Eyes");
			playerHandsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Hands");
			playerHands2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Hands2");
			playerHeadTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Head");
			playerPantsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Pants");
			playerShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Shirt");
			playerShoesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Shoes");
			playerUnderShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Undershirt");
			playerUnderShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Player_Undershirt2");
			femalePantsTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Pants");
			femaleShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shirt");
			femaleShoesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shoes");
			femaleUnderShirtTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Undershirt");
			femaleUnderShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Undershirt2");
			femaleShirt2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Female_Shirt2");
			chaosTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chaos");
			EyeLaserTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Eye_Laser");
			BoneEyesTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bone_eyes");
			BoneLaserTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Bone_Laser");
			lightDiscTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Light_Disc");
			confuseTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Confuse");
			probeTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Probe");
			chainTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain");
			chain2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain2");
			chain3Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain3");
			chain4Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain4");
			chain5Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain5");
			chain6Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain6");
			chain7Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain7");
			chain8Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain8");
			chain9Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain9");
			chain10Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain10");
			chain11Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain11");
			chain12Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Chain12");
			boneArmTexture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Arm_Bone");
			boneArm2Texture = base.Content.Load<Texture2D>("Images" + Path.DirectorySeparatorChar + "Arm_Bone_2");
			fontItemStack = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Item_Stack");
			fontMouseText = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Mouse_Text");
			fontDeathText = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Death_Text");
			fontCombatText[0] = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Combat_Text");
			fontCombatText[1] = base.Content.Load<SpriteFont>("Fonts" + Path.DirectorySeparatorChar + "Combat_Crit");
			Codable.RunGlobalMethod("ModGeneric", "OnLoad");
		}

		protected override void UnloadContent()
		{
			Codable.RunGlobalMethod("ModGeneric", "OnUnload");
			base.UnloadContent();
		}

		public void UpdateMusic()
		{
			if (dedServ || engine == null)
			{
				return;
			}
			if (!base.IsActive)
			{
				if (Audio.Player.Playing())
				{
					Audio.Player.Pause();
					Audio.Player.InactivePause = true;
				}
			}
			else if (Audio.Player.InactivePause)
			{
				Audio.Player.Resume();
				Audio.Player.InactivePause = false;
			}
			try
			{
				if (curMusic == null || (curMusic is SoundHandler.MusicVanilla && ((SoundHandler.MusicVanilla)curMusic).ID == 0))
				{
					goto IL_00ab;
				}
				if (base.IsActive)
				{
					if (curMusic.IsPaused())
					{
						curMusic.Resume();
					}
					goto IL_00ab;
				}
				if (!curMusic.IsPaused() && curMusic.IsPlaying())
				{
					try
					{
						curMusic.Pause();
					}
					catch
					{
					}
				}
				goto end_IL_003d;
				IL_00ab:
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				SoundHandler.Music music = null;
				SoundHandler.customMusic = null;
				Codable.RunGlobalMethod("ModGeneric", "PreUpdateMusic");
				Rectangle rectangle = new Rectangle((int)screenPosition.X, (int)screenPosition.Y, screenWidth, screenHeight);
				int num = 5000;
				for (int i = 0; i < 200; i++)
				{
					if (npc[i].active)
					{
						if (music == null && npc[i].musicName != null && npc[i].musicName != "")
						{
							Rectangle value = new Rectangle((int)(npc[i].position.X + (float)(npc[i].width / 2)) - num, (int)(npc[i].position.Y + (float)(npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value))
							{
								string mod = npc[i].musicName.Substring(0, npc[i].musicName.IndexOf(':'));
								string audio = npc[i].musicName.Substring(npc[i].musicName.IndexOf(':') + 1);
								music = new SoundHandler.MusicCustomBank(audio, mod);
								break;
							}
						}
						else if (music == null && npc[i].music > 0)
						{
							Rectangle value2 = new Rectangle((int)(npc[i].position.X + (float)(npc[i].width / 2)) - num, (int)(npc[i].position.Y + (float)(npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value2))
							{
								music = new SoundHandler.MusicVanilla(npc[i].music);
								break;
							}
						}
						else if (npc[i].type == 134 || npc[i].type == 143 || npc[i].type == 144 || npc[i].type == 145)
						{
							Rectangle value3 = new Rectangle((int)(npc[i].position.X + (float)(npc[i].width / 2)) - num, (int)(npc[i].position.Y + (float)(npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value3))
							{
								flag3 = true;
								break;
							}
						}
						else if (npc[i].type == 113 || npc[i].type == 114 || npc[i].type == 125 || npc[i].type == 126)
						{
							Rectangle value4 = new Rectangle((int)(npc[i].position.X + (float)(npc[i].width / 2)) - num, (int)(npc[i].position.Y + (float)(npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value4))
							{
								flag2 = true;
								break;
							}
						}
						else if (npc[i].boss || npc[i].type == 13 || npc[i].type == 14 || npc[i].type == 15 || npc[i].type == 134 || npc[i].type == 26 || npc[i].type == 27 || npc[i].type == 28 || npc[i].type == 29 || npc[i].type == 111)
						{
							Rectangle value5 = new Rectangle((int)(npc[i].position.X + (float)(npc[i].width / 2)) - num, (int)(npc[i].position.Y + (float)(npc[i].height / 2)) - num, num * 2, num * 2);
							if (rectangle.Intersects(value5))
							{
								flag = true;
								break;
							}
						}
					}
				}
				Biome biome = null;
				bool flag4 = false;
				if (musicVolume == 0f)
				{
					newMusic = new SoundHandler.MusicVanilla(0);
				}
				else if (gameMenu)
				{
					if (netMode != 2)
					{
						newMusic = new SoundHandler.MusicVanilla(6);
					}
					else
					{
						newMusic = new SoundHandler.MusicVanilla(0);
					}
				}
				else if (SoundHandler.customMusic != null)
				{
					newMusic = SoundHandler.customMusic;
					SoundHandler.customMusic = null;
				}
				else if (music != null)
				{
					newMusic = music;
				}
				else if (flag2)
				{
					newMusic = new SoundHandler.MusicVanilla(12);
				}
				else if (flag)
				{
					newMusic = new SoundHandler.MusicVanilla(5);
				}
				else if (flag3)
				{
					newMusic = new SoundHandler.MusicVanilla(13);
				}
				else if (Biome.GetBiomeForMusic(Biome.MusicPriority.High, ref biome))
				{
					newMusic = biome.GetMusic();
				}
				else if ((double)player[myPlayer].position.Y > hellLayer * 16.0)
				{
					newMusic = new SoundHandler.MusicVanilla(2);
				}
				else if (player[myPlayer].zoneEvil)
				{
					if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)screenHeight)
					{
						newMusic = new SoundHandler.MusicVanilla(10);
					}
					else
					{
						newMusic = new SoundHandler.MusicVanilla(8);
					}
				}
				else if (player[myPlayer].zoneMeteor || player[myPlayer].zoneDungeon)
				{
					newMusic = new SoundHandler.MusicVanilla(2);
				}
				else if (Biome.GetBiomeForMusic(Biome.MusicPriority.Med, ref biome))
				{
					newMusic = biome.GetMusic();
				}
				else if (player[myPlayer].zoneJungle)
				{
					newMusic = new SoundHandler.MusicVanilla(7);
				}
				else if ((double)player[myPlayer].position.Y > worldSurface * 16.0 + (double)screenHeight)
				{
					if (player[myPlayer].zoneHoly)
					{
						newMusic = new SoundHandler.MusicVanilla(11);
					}
					else
					{
						newMusic = new SoundHandler.MusicVanilla(4);
					}
				}
				else if (dayTime)
				{
					if (player[myPlayer].zoneHoly)
					{
						newMusic = new SoundHandler.MusicVanilla(9);
					}
					else
					{
						flag4 = true;
						newMusic = new SoundHandler.MusicVanilla(1);
					}
				}
				else if (!dayTime)
				{
					flag4 = true;
					if (bloodMoon)
					{
						newMusic = new SoundHandler.MusicVanilla(2);
					}
					else
					{
						newMusic = new SoundHandler.MusicVanilla(3);
					}
				}
				if ((newMusic == null || flag4) && Biome.GetBiomeForMusic(Biome.MusicPriority.Low, ref biome))
				{
					newMusic = biome.GetMusic();
				}
				if (gameMenu)
				{
					musicBox2 = -1;
					musicBox = -1;
				}
				if (musicBox2 >= 0)
				{
					musicBox = musicBox2;
				}
				if (musicBox >= 0)
				{
					if (musicBox == 0)
					{
						newMusic = new SoundHandler.MusicVanilla(1);
					}
					if (musicBox == 1)
					{
						newMusic = new SoundHandler.MusicVanilla(2);
					}
					if (musicBox == 2)
					{
						newMusic = new SoundHandler.MusicVanilla(3);
					}
					if (musicBox == 4)
					{
						newMusic = new SoundHandler.MusicVanilla(4);
					}
					if (musicBox == 5)
					{
						newMusic = new SoundHandler.MusicVanilla(5);
					}
					if (musicBox == 3)
					{
						newMusic = new SoundHandler.MusicVanilla(6);
					}
					if (musicBox == 6)
					{
						newMusic = new SoundHandler.MusicVanilla(7);
					}
					if (musicBox == 7)
					{
						newMusic = new SoundHandler.MusicVanilla(8);
					}
					if (musicBox == 9)
					{
						newMusic = new SoundHandler.MusicVanilla(9);
					}
					if (musicBox == 8)
					{
						newMusic = new SoundHandler.MusicVanilla(10);
					}
					if (musicBox == 11)
					{
						newMusic = new SoundHandler.MusicVanilla(11);
					}
					if (musicBox == 10)
					{
						newMusic = new SoundHandler.MusicVanilla(12);
					}
					if (musicBox == 12)
					{
						newMusic = new SoundHandler.MusicVanilla(13);
					}
				}
				curMusic = newMusic;
				List<SoundHandler.Music> list = new List<SoundHandler.Music>();
				for (int j = 1; j < 14; j++)
				{
					list.Add(new SoundHandler.MusicVanilla(j));
				}
				foreach (KeyValuePair<int, SoundEffectInstance> item2 in SoundHandler.customMusicInstance)
				{
					list.Add(new SoundHandler.MusicCustom(item2.Key));
				}
				foreach (KeyValuePair<int, Cue> item3 in SoundHandler.customCueInstance)
				{
					string text = SoundHandler.IDCue[item3.Key];
					string audio2 = text.Substring(text.LastIndexOf(':') + 1);
					string mod2 = text.Substring(0, text.LastIndexOf(':'));
					list.Add(new SoundHandler.MusicCustomBank(audio2, mod2));
				}
				if (curMusic is SoundHandler.MusicCustom)
				{
					int id = ((SoundHandler.MusicCustom)curMusic).id;
					bool flag5 = true;
					foreach (SoundHandler.Music item4 in list)
					{
						if (item4 is SoundHandler.MusicCustom && item4.Equals(curMusic))
						{
							flag5 = false;
							break;
						}
					}
					if (flag5)
					{
						if (curMusic is SoundHandler.MusicCustomBank)
						{
							SoundHandler.customCueFade[id] = 0f;
						}
						else
						{
							SoundHandler.customMusicInstance[id] = SoundHandler.customSound[id].CreateInstance();
							SoundHandler.customMusicInstance[id].IsLooped = true;
							SoundHandler.customMusicFade[id] = 0f;
						}
						list.Add(curMusic);
					}
				}
				foreach (SoundHandler.Music item5 in list)
				{
					if (item5.Equals(curMusic))
					{
						if (!item5.IsPlaying())
						{
							item5.Play();
							item5.SetVolume(0f);
						}
						else
						{
							item5.SetVolume(Math.Min(item5.GetVolume() + 0.005f, 1f));
						}
					}
					else if (item5.IsPlaying())
					{
						if (curMusic.GetVolume() > 0.25f)
						{
							item5.SetVolume(Math.Max(item5.GetVolume() - 0.005f, 0f));
						}
						else if (curMusic is SoundHandler.MusicVanilla && ((SoundHandler.MusicVanilla)curMusic).ID == 0)
						{
							item5.SetVolume(0f);
						}
						if (item5.GetVolume() <= 0f)
						{
							item5.Stop();
						}
					}
					else
					{
						item5.SetVolume(0f);
					}
				}
				end_IL_003d:;
			}
			catch
			{
				musicVolume = 0f;
			}
		}

		public static void snowing()
		{
			if (gamePaused || snowTiles <= 0 || !((double)player[myPlayer].position.Y < worldSurface * 16.0))
			{
				return;
			}
			int maxValue = 800 / snowTiles;
			float num = (float)screenWidth / 1920f;
			int num2 = (int)(500f * num);
			if ((float)snowDust < (float)num2 * (gfxQuality / 2f + 0.5f) + (float)num2 * 0.1f && rand.Next(maxValue) == 0)
			{
				int num3 = rand.Next(screenWidth + 1000) - 500;
				int num4 = (int)screenPosition.Y;
				if (rand.Next(5) == 0)
				{
					num3 = rand.Next(500) - 500;
				}
				else if (rand.Next(5) == 0)
				{
					num3 = rand.Next(500) + screenWidth;
				}
				if (num3 < 0 || num3 > screenWidth)
				{
					num4 += rand.Next((int)((double)screenHeight * 0.5)) + (int)((double)screenHeight * 0.1);
				}
				num3 += (int)screenPosition.X;
				int num5 = Dust.NewDust(new Vector2(num3, num4), 10, 10, 76);
				Main.dust[num5].velocity.Y = 3f + (float)rand.Next(30) * 0.1f;
				Dust dust = Main.dust[num5];
				dust.velocity.Y = dust.velocity.Y * Main.dust[num5].scale;
				Main.dust[num5].velocity.X = windSpeed + (float)rand.Next(-10, 10) * 0.1f;
			}
		}

		public static void checkXMas()
		{
			if (!Codable.RunGlobalMethod("ModGeneric", "checkXMas"))
			{
				DateTime now = DateTime.Now;
				if (now.Day >= 15 && now.Month == 12)
				{
					xMas = true;
				}
				else
				{
					xMas = false;
				}
			}
		}

		protected override void Update(GameTime gameTime)
		{
			TMod.RunMethod(TMod.WorldHooks.PreUpdate, gameTime);
			if (netMode != 2)
			{
				snowing();
			}
			if (chTitle)
			{
				chTitle = false;
				SetTitle();
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			WorldGen.destroyObject = false;
			if (!dedServ)
			{
				if (fixedTiming)
				{
					if (base.IsActive)
					{
						base.IsFixedTimeStep = false;
					}
					else
					{
						base.IsFixedTimeStep = true;
					}
				}
				else
				{
					base.IsFixedTimeStep = true;
				}
				graphics.SynchronizeWithVerticalRetrace = true;
				UpdateMusic();
				if (showSplash)
				{
					return;
				}
				if (!gameMenu && netMode == 1)
				{
					if (!saveTime.IsRunning)
					{
						saveTime.Start();
					}
					if (saveTime.ElapsedMilliseconds > 300000)
					{
						saveTime.Reset();
						WorldGen.saveToonWhilePlaying();
					}
				}
				else if (!gameMenu && autoSave)
				{
					if (!saveTime.IsRunning)
					{
						saveTime.Start();
					}
					if (saveTime.ElapsedMilliseconds > 600000)
					{
						saveTime.Reset();
						WorldGen.saveToonWhilePlaying();
						WorldGen.saveAndPlay();
					}
				}
				else if (saveTime.IsRunning)
				{
					saveTime.Stop();
				}
				if (teamCooldown > 0)
				{
					teamCooldown--;
				}
				updateTime++;
				if (fpsTimer.ElapsedMilliseconds >= 1000)
				{
					if ((float)fpsCount >= 30f + 30f * gfxQuality)
					{
						gfxQuality += gfxRate;
						gfxRate += 0.005f;
					}
					else if ((float)fpsCount < 29f + 30f * gfxQuality)
					{
						gfxRate = 0.01f;
						gfxQuality -= 0.1f;
					}
					if (gfxQuality < 0f)
					{
						gfxQuality = 0f;
					}
					if (gfxQuality > 1f)
					{
						gfxQuality = 1f;
					}
					if (maxQ && base.IsActive)
					{
						gfxQuality = 1f;
						maxQ = false;
					}
					updateRate = uCount;
					frameRate = fpsCount;
					fpsCount = 0;
					fpsTimer.Restart();
					updateTime = 0;
					drawTime = 0;
					uCount = 0;
					if (netMode == 2)
					{
						cloudLimit = 0;
					}
				}
				if (fixedTiming)
				{
					float num = 16f;
					float num2 = updateTimer.ElapsedMilliseconds;
					if (num2 + uCarry < num)
					{
						drawSkip = true;
						return;
					}
					uCarry += num2 - num;
					if (uCarry > 1000f)
					{
						uCarry = 1000f;
					}
					updateTimer.Restart();
				}
				uCount++;
				drawSkip = false;
				if (qaStyle == 1)
				{
					gfxQuality = 1f;
				}
				else if (qaStyle == 2)
				{
					gfxQuality = 0.5f;
				}
				else if (qaStyle == 3)
				{
					gfxQuality = 0f;
				}
				numDust = (int)(2000f * (gfxQuality * 0.75f + 0.25f));
				Gore.goreTime = (int)(600f * gfxQuality);
				cloudLimit = (int)(100f * gfxQuality);
				Liquid.maxLiquid = (int)(2500f + 2500f * gfxQuality);
				Liquid.cycles = (int)(17f - 10f * gfxQuality);
				if ((double)gfxQuality < 0.5)
				{
					graphics.SynchronizeWithVerticalRetrace = false;
				}
				else
				{
					graphics.SynchronizeWithVerticalRetrace = true;
				}
				if ((double)gfxQuality < 0.2)
				{
					Lighting.maxRenderCount = 8;
				}
				else if ((double)gfxQuality < 0.4)
				{
					Lighting.maxRenderCount = 7;
				}
				else if ((double)gfxQuality < 0.6)
				{
					Lighting.maxRenderCount = 6;
				}
				else if ((double)gfxQuality < 0.8)
				{
					Lighting.maxRenderCount = 5;
				}
				else
				{
					Lighting.maxRenderCount = 4;
				}
				if (Liquid.quickSettle)
				{
					Liquid.maxLiquid = Liquid.resLiquid;
					Liquid.cycles = 1;
				}
				if (!base.IsActive)
				{
					hasFocus = false;
				}
				else
				{
					hasFocus = true;
				}
				if (!base.IsActive && netMode == 0)
				{
					base.IsMouseVisible = true;
					if (netMode != 2 && myPlayer >= 0)
					{
						player[myPlayer].delayUseItem = true;
					}
					mouseLeftRelease = false;
					mouseRightRelease = false;
					if (gameMenu)
					{
						UpdateMenu();
					}
					gamePaused = true;
					return;
				}
				base.IsMouseVisible = false;
				demonTorch += (float)demonTorchDir * 0.01f;
				if (demonTorch > 1f)
				{
					demonTorch = 1f;
					demonTorchDir = -1;
				}
				if (demonTorch < 0f)
				{
					demonTorch = 0f;
					demonTorchDir = 1;
				}
				int num3 = 7;
				if (DiscoStyle == 0)
				{
					DiscoG += num3;
					if (DiscoG >= 255)
					{
						DiscoG = 255;
						DiscoStyle++;
					}
					DiscoR -= num3;
					if (DiscoR <= 0)
					{
						DiscoR = 0;
					}
				}
				else if (DiscoStyle == 1)
				{
					DiscoB += num3;
					if (DiscoB >= 255)
					{
						DiscoB = 255;
						DiscoStyle++;
					}
					DiscoG -= num3;
					if (DiscoG <= 0)
					{
						DiscoG = 0;
					}
				}
				else
				{
					DiscoR += num3;
					if (DiscoR >= 255)
					{
						DiscoR = 255;
						DiscoStyle = 0;
					}
					DiscoB -= num3;
					if (DiscoB <= 0)
					{
						DiscoB = 0;
					}
				}
				if (keyState.IsKeyDown(Keys.F10) && !chatMode && !editSign)
				{
					if (frameRelease)
					{
						PlaySound(12);
						if (showFrameRate)
						{
							showFrameRate = false;
						}
						else
						{
							showFrameRate = true;
						}
					}
					frameRelease = false;
				}
				else
				{
					frameRelease = true;
				}
				if (keyState.IsKeyDown(Keys.F9) && !chatMode && !editSign)
				{
					if (RGBRelease)
					{
						Lighting.lightCounter += 100;
						PlaySound(12);
						Lighting.lightMode++;
						if (Lighting.lightMode >= 4)
						{
							Lighting.lightMode = 0;
						}
						if (Lighting.lightMode == 2 || Lighting.lightMode == 0)
						{
							renderCount = 0;
							renderNow = true;
							int num4 = screenWidth / 16 + Lighting.offScreenTiles * 2;
							int num5 = screenHeight / 16 + Lighting.offScreenTiles * 2;
							for (int i = 0; i < num4; i++)
							{
								for (int j = 0; j < num5; j++)
								{
									Lighting.color[i, j] = 0f;
									Lighting.colorG[i, j] = 0f;
									Lighting.colorB[i, j] = 0f;
								}
							}
						}
					}
					RGBRelease = false;
				}
				else
				{
					RGBRelease = true;
				}
				if (keyState.IsKeyDown(Keys.F8) && !chatMode && !editSign)
				{
					if (netRelease)
					{
						PlaySound(12);
						if (netDiag)
						{
							netDiag = false;
						}
						else
						{
							netDiag = true;
						}
					}
					netRelease = false;
				}
				else
				{
					netRelease = true;
				}
				if (keyState.IsKeyDown(Keys.F7) && !chatMode && !editSign)
				{
					if (drawRelease)
					{
						PlaySound(12);
						if (drawDiag)
						{
							drawDiag = false;
						}
						else
						{
							drawDiag = true;
						}
					}
					drawRelease = false;
				}
				else
				{
					drawRelease = true;
				}
				if (keyState.IsKeyDown(Keys.F11))
				{
					if (releaseUI)
					{
						if (hideUI)
						{
							hideUI = false;
						}
						else
						{
							hideUI = true;
						}
					}
					releaseUI = false;
				}
				else
				{
					releaseUI = true;
				}
				if ((keyState.IsKeyDown(Keys.LeftAlt) || keyState.IsKeyDown(Keys.RightAlt)) && keyState.IsKeyDown(Keys.Enter))
				{
					if (toggleFullscreen)
					{
						graphics.ToggleFullScreen();
						chatRelease = false;
					}
					toggleFullscreen = false;
				}
				else
				{
					toggleFullscreen = true;
				}
				if (!gamePad || gameMenu)
				{
					oldMouseState = mouseState;
					mouseState = Mouse.GetState();
					mouseX = mouseState.X;
					mouseY = mouseState.Y;
					mouseLeft = false;
					mouseRight = false;
					if (mouseState.LeftButton == ButtonState.Pressed)
					{
						mouseLeft = true;
					}
					if (mouseState.RightButton == ButtonState.Pressed)
					{
						mouseRight = true;
					}
				}
				keyState = Keyboard.GetState();
				if (editSign)
				{
					chatMode = false;
				}
				if (chatMode)
				{
					if (keyState.IsKeyDown(Keys.Escape))
					{
						chatMode = false;
					}
					string a = chatText;
					chatText = GetInputText(chatText);
					while (fontMouseText.MeasureString(chatText).X > 470f)
					{
						chatText = chatText.Substring(0, chatText.Length - 1);
					}
					if (a != chatText)
					{
						PlaySound(12);
					}
					if (inputTextEnter && chatRelease)
					{
						if (chatText != "")
						{
							NetMessage.SendData(25, -1, -1, chatText, myPlayer);
						}
						chatText = "";
						chatMode = false;
						chatRelease = false;
						player[myPlayer].releaseHook = false;
						player[myPlayer].releaseThrow = false;
						PlaySound(11);
					}
				}
				if (keyState.IsKeyDown(Keys.Enter) && netMode == 1 && !keyState.IsKeyDown(Keys.LeftAlt) && !keyState.IsKeyDown(Keys.RightAlt))
				{
					if (chatRelease && !chatMode && !editSign && !keyState.IsKeyDown(Keys.Escape))
					{
						PlaySound(10);
						chatMode = true;
						clrInput();
						chatText = "";
					}
					chatRelease = false;
				}
				else
				{
					chatRelease = true;
				}
				if (gameMenu)
				{
					UpdateMenu();
					if (netMode != 2)
					{
						return;
					}
					gamePaused = false;
				}
			}
			if (netMode == 1)
			{
				for (int k = 0; k < 49; k++)
				{
					if (player[myPlayer].inventory[k].IsNotTheSameAs(clientPlayer.inventory[k]))
					{
						NetMessage.SendData(5, -1, -1, player[myPlayer].inventory[k].name, myPlayer, k, (int)player[myPlayer].inventory[k].prefix);
					}
				}
				if (player[myPlayer].armor[0].IsNotTheSameAs(clientPlayer.armor[0]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[0].name, myPlayer, 49f, (int)player[myPlayer].armor[0].prefix);
				}
				if (player[myPlayer].armor[1].IsNotTheSameAs(clientPlayer.armor[1]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[1].name, myPlayer, 50f, (int)player[myPlayer].armor[1].prefix);
				}
				if (player[myPlayer].armor[2].IsNotTheSameAs(clientPlayer.armor[2]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[2].name, myPlayer, 51f, (int)player[myPlayer].armor[2].prefix);
				}
				if (player[myPlayer].armor[3].IsNotTheSameAs(clientPlayer.armor[3]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[3].name, myPlayer, 52f, (int)player[myPlayer].armor[3].prefix);
				}
				if (player[myPlayer].armor[4].IsNotTheSameAs(clientPlayer.armor[4]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[4].name, myPlayer, 53f, (int)player[myPlayer].armor[4].prefix);
				}
				if (player[myPlayer].armor[5].IsNotTheSameAs(clientPlayer.armor[5]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[5].name, myPlayer, 54f, (int)player[myPlayer].armor[5].prefix);
				}
				if (player[myPlayer].armor[6].IsNotTheSameAs(clientPlayer.armor[6]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[6].name, myPlayer, 55f, (int)player[myPlayer].armor[6].prefix);
				}
				if (player[myPlayer].armor[7].IsNotTheSameAs(clientPlayer.armor[7]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[7].name, myPlayer, 56f, (int)player[myPlayer].armor[7].prefix);
				}
				if (player[myPlayer].armor[8].IsNotTheSameAs(clientPlayer.armor[8]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[8].name, myPlayer, 57f, (int)player[myPlayer].armor[8].prefix);
				}
				if (player[myPlayer].armor[9].IsNotTheSameAs(clientPlayer.armor[9]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[9].name, myPlayer, 58f, (int)player[myPlayer].armor[9].prefix);
				}
				if (player[myPlayer].armor[10].IsNotTheSameAs(clientPlayer.armor[10]))
				{
					NetMessage.SendData(5, -1, -1, player[myPlayer].armor[10].name, myPlayer, 59f, (int)player[myPlayer].armor[10].prefix);
				}
				if (player[myPlayer].chest != clientPlayer.chest)
				{
					NetMessage.SendData(33, -1, -1, "", player[myPlayer].chest);
				}
				if (player[myPlayer].talkNPC != clientPlayer.talkNPC)
				{
					NetMessage.SendData(40, -1, -1, "", myPlayer);
				}
				foreach (string key in player[myPlayer].zone.Keys)
				{
					if (player[myPlayer].zone[key] != clientPlayer.zone[key])
					{
						NetMessage.SendData(36, -1, -1, "", myPlayer);
						break;
					}
				}
				bool flag = false;
				for (int l = 0; l < player[myPlayer].buffType.Length; l++)
				{
					if (player[myPlayer].buffType[l] != clientPlayer.buffType[l])
					{
						flag = true;
					}
				}
				if (flag)
				{
					NetMessage.SendData(50, -1, -1, "", myPlayer);
					NetMessage.SendData(13, -1, -1, "", myPlayer);
				}
			}
			if (netMode == 1)
			{
				clientPlayer = (Player)player[myPlayer].clientClone();
			}
			if ((netMode == 0 && (playerInventory || npcChatText != "" || player[myPlayer].sign >= 0) && autoPause) || modPause)
			{
				Keys[] pressedKeys = keyState.GetPressedKeys();
				player[myPlayer].controlInv = false;
				for (int m = 0; m < pressedKeys.Length; m++)
				{
					string a2 = string.Concat(pressedKeys[m]);
					if (a2 == cInv)
					{
						player[myPlayer].controlInv = true;
					}
				}
				if (player[myPlayer].controlInv)
				{
					if (player[myPlayer].releaseInventory)
					{
						player[myPlayer].toggleInv();
					}
					player[myPlayer].releaseInventory = false;
				}
				else
				{
					player[myPlayer].releaseInventory = true;
				}
				if (playerInventory)
				{
					int num6 = (mouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue) / 120;
					focusRecipe += num6;
					if (focusRecipe > numAvailableRecipes - 1)
					{
						focusRecipe = numAvailableRecipes - 1;
					}
					if (focusRecipe < 0)
					{
						focusRecipe = 0;
					}
					player[myPlayer].dropItemCheck();
				}
				player[myPlayer].head = player[myPlayer].armor[0].headSlot;
				player[myPlayer].body = player[myPlayer].armor[1].bodySlot;
				player[myPlayer].legs = player[myPlayer].armor[2].legSlot;
				if (!player[myPlayer].hostile)
				{
					if (player[myPlayer].armor[8].headSlot >= 0)
					{
						player[myPlayer].head = player[myPlayer].armor[8].headSlot;
					}
					if (player[myPlayer].armor[9].bodySlot >= 0)
					{
						player[myPlayer].body = player[myPlayer].armor[9].bodySlot;
					}
					if (player[myPlayer].armor[10].legSlot >= 0)
					{
						player[myPlayer].legs = player[myPlayer].armor[10].legSlot;
					}
				}
				if (editSign)
				{
					if (player[myPlayer].sign == -1)
					{
						editSign = false;
					}
					else
					{
						npcChatText = GetInputText(npcChatText);
						if (inputTextEnter)
						{
							byte[] bytes = new byte[1]
							{
								10
							};
							npcChatText += Encoding.ASCII.GetString(bytes);
						}
					}
				}
				gamePaused = true;
				return;
			}
			gamePaused = false;
			if (!dedServ && (double)screenPosition.Y < worldSurface * 16.0 + 16.0 && netMode != 2)
			{
				Star.UpdateStars();
				Cloud.UpdateClouds();
			}
			for (int n = 0; n < 255; n++)
			{
				if (ignoreErrors)
				{
					try
					{
						player[n].UpdatePlayer(n);
					}
					catch
					{
					}
				}
				else
				{
					player[n].UpdatePlayer(n);
				}
			}
			if (netMode != 1)
			{
				NPC.SpawnNPC();
			}
			for (int num7 = 0; num7 < 255; num7++)
			{
				player[num7].activeNPCs = 0f;
				player[num7].townNPCs = 0f;
			}
			if (wof >= 0 && !npc[wof].active)
			{
				wof = -1;
			}
			for (int num8 = 0; num8 < 200; num8++)
			{
				if (ignoreErrors)
				{
					try
					{
						npc[num8].UpdateNPC(num8);
					}
					catch (Exception)
					{
						npc[num8] = new NPC();
					}
				}
				else
				{
					npc[num8].UpdateNPC(num8);
				}
			}
			for (int num9 = 0; num9 < 200; num9++)
			{
				if (ignoreErrors)
				{
					try
					{
						gore[num9].Update();
					}
					catch
					{
						gore[num9] = new Gore();
					}
				}
				else
				{
					gore[num9].Update();
				}
			}
			for (int num10 = 0; num10 < 1000; num10++)
			{
				if (ignoreErrors)
				{
					try
					{
						projectile[num10].Update(num10);
					}
					catch
					{
						projectile[num10] = new Projectile();
					}
				}
				else
				{
					projectile[num10].Update(num10);
				}
			}
			for (int num11 = 0; num11 < 200; num11++)
			{
				if (ignoreErrors)
				{
					try
					{
						item[num11].UpdateItem(num11);
					}
					catch
					{
						item[num11] = new Item();
					}
				}
				else
				{
					item[num11].UpdateItem(num11);
				}
			}
			if (ignoreErrors)
			{
				try
				{
					Dust.UpdateDust();
				}
				catch
				{
					for (int num12 = 0; num12 < 2000; num12++)
					{
						dust[num12] = new Dust();
					}
				}
			}
			else
			{
				Dust.UpdateDust();
			}
			if (netMode != 2)
			{
				CombatText.UpdateCombatText();
				ItemText.UpdateItemText();
			}
			if (ignoreErrors)
			{
				try
				{
					UpdateTime();
				}
				catch
				{
					checkForSpawns = 0;
				}
			}
			else
			{
				UpdateTime();
			}
			if (netMode != 1)
			{
				if (ignoreErrors)
				{
					try
					{
						WorldGen.UpdateWorld();
						UpdateInvasion();
					}
					catch
					{
					}
				}
				else
				{
					WorldGen.UpdateWorld();
					UpdateInvasion();
				}
			}
			if (ignoreErrors)
			{
				try
				{
					if (netMode == 2)
					{
						UpdateServer();
					}
					if (netMode == 1)
					{
						UpdateClient();
					}
				}
				catch
				{
					_ = netMode;
				}
			}
			else
			{
				if (netMode == 2)
				{
					UpdateServer();
				}
				if (netMode == 1)
				{
					UpdateClient();
				}
			}
			if (ignoreErrors)
			{
				try
				{
					for (int num13 = 0; num13 < numChatLines; num13++)
					{
						if (chatLine[num13].showTime > 0)
						{
							chatLine[num13].showTime--;
						}
					}
				}
				catch
				{
					for (int num14 = 0; num14 < numChatLines; num14++)
					{
						chatLine[num14] = new ChatLine();
					}
				}
			}
			else
			{
				for (int num15 = 0; num15 < numChatLines; num15++)
				{
					if (chatLine[num15].showTime > 0)
					{
						chatLine[num15].showTime--;
					}
				}
			}
			upTimer = stopwatch.ElapsedMilliseconds;
			if (upTimerMaxDelay > 0f)
			{
				upTimerMaxDelay -= 1f;
			}
			else
			{
				upTimerMax = 0f;
			}
			if (upTimer > upTimerMax)
			{
				upTimerMax = upTimer;
				upTimerMaxDelay = 400f;
			}
			Audio.Player.Update();
			TMod.RunMethod(TMod.WorldHooks.PostUpdate);
			base.Update(gameTime);
		}

		private static void UpdateMenu()
		{
			playerInventory = false;
			exitScale = 0.8f;
			if (netMode == 0)
			{
				if (grabSky)
				{
					return;
				}
				time += 86.4;
				if (!dayTime)
				{
					if (time > 32400.0)
					{
						bloodMoon = false;
						time = 0.0;
						dayTime = true;
						moonPhase++;
						if (moonPhase >= 8)
						{
							moonPhase = 0;
						}
					}
				}
				else if (time > 54000.0)
				{
					time = 0.0;
					dayTime = false;
				}
			}
			else if (netMode == 1)
			{
				UpdateTime();
			}
		}

		public static void clrInput()
		{
			keyCount = 0;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetKeyState(int keyCode);

		public static string GetInputText(string oldString)
		{
			if (!hasFocus)
			{
				return oldString;
			}
			inputTextEnter = false;
			string text = oldString;
			string text2 = "";
			if (text == null)
			{
				text = "";
			}
			bool flag = false;
			for (int i = 0; i < keyCount; i++)
			{
				int num = keyInt[i];
				string str = keyString[i];
				if (num == 13)
				{
					inputTextEnter = true;
				}
				else if (num >= 32 && num != 127)
				{
					text2 += str;
				}
			}
			keyCount = 0;
			text += text2;
			oldInputText = inputText;
			inputText = Keyboard.GetState();
			Keys[] pressedKeys = inputText.GetPressedKeys();
			Keys[] pressedKeys2 = oldInputText.GetPressedKeys();
			if (inputText.IsKeyDown(Keys.Back) && oldInputText.IsKeyDown(Keys.Back))
			{
				if (backSpaceCount == 0)
				{
					backSpaceCount = 7;
					flag = true;
				}
				backSpaceCount--;
			}
			else
			{
				backSpaceCount = 15;
			}
			for (int j = 0; j < pressedKeys.Length; j++)
			{
				bool flag2 = true;
				for (int k = 0; k < pressedKeys2.Length; k++)
				{
					if (pressedKeys[j] == pressedKeys2[k])
					{
						flag2 = false;
					}
				}
				string a = string.Concat(pressedKeys[j]);
				if (a == "Back" && (flag2 || flag) && text.Length > 0)
				{
					text = text.Substring(0, text.Length - 1);
				}
			}
			return text;
		}

		public void MouseText(string cursorText, int rare = 0, byte diff = 0)
		{
			if (mouseNPC > -1 || cursorText == null)
			{
				return;
			}
			int num = mouseX + 10;
			int num2 = mouseY + 10;
			Color color = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
			float num21;
			if (toolTip.type > 0)
			{
				if (player[myPlayer].kbGlove)
				{
					toolTip.knockBack *= 1.7f;
				}
				rare = toolTip.rare;
				int num3 = 30;
				int num4 = 1;
				string[] array = new string[num3];
				bool[] array2 = new bool[num3];
				bool[] array3 = new bool[num3];
				for (int i = 0; i < num3; i++)
				{
					array2[i] = false;
					array3[i] = false;
				}
				array[0] = toolTip.AffixName();
				if (toolTip.stack > 1)
				{
					string[] array4;
					string[] array5 = array4 = array;
					int num5 = 0;
					object obj = array4[0];
					array5[num5] = string.Concat(obj, " (", toolTip.stack, ")");
				}
				if (toolTip.social)
				{
					array[num4] = Lang.tip[0];
					num4++;
					array[num4] = Lang.tip[1];
					num4++;
				}
				else
				{
					if (toolTip.damage > 0)
					{
						int damage = toolTip.damage;
						if (toolTip.melee)
						{
							array[num4] = string.Concat((int)(player[myPlayer].meleeDamage * (float)damage));
							string[] array6;
							IntPtr value;
							(array6 = array)[(int)(value = (IntPtr)num4)] = array6[(int)value] + Lang.tip[2];
						}
						else if (toolTip.ranged)
						{
							array[num4] = string.Concat((int)(player[myPlayer].rangedDamage * (float)damage));
							string[] array7;
							IntPtr value2;
							(array7 = array)[(int)(value2 = (IntPtr)num4)] = array7[(int)value2] + Lang.tip[3];
						}
						else if (toolTip.magic)
						{
							array[num4] = string.Concat((int)(player[myPlayer].magicDamage * (float)damage));
							string[] array8;
							IntPtr value3;
							(array8 = array)[(int)(value3 = (IntPtr)num4)] = array8[(int)value3] + Lang.tip[4];
						}
						else
						{
							array[num4] = string.Concat(damage);
						}
						num4++;
						if (toolTip.melee)
						{
							int num6 = player[myPlayer].meleeCrit - player[myPlayer].inventory[player[myPlayer].selectedItem].crit + toolTip.crit;
							if (num6 < 0)
							{
								num6 = 0;
							}
							array[num4] = num6 + Lang.tip[5];
							num4++;
						}
						else if (toolTip.ranged)
						{
							int num7 = player[myPlayer].rangedCrit - player[myPlayer].inventory[player[myPlayer].selectedItem].crit + toolTip.crit;
							if (num7 < 0)
							{
								num7 = 0;
							}
							array[num4] = num7 + Lang.tip[5];
							num4++;
						}
						else if (toolTip.magic)
						{
							int num8 = player[myPlayer].magicCrit - player[myPlayer].inventory[player[myPlayer].selectedItem].crit + toolTip.crit;
							if (num8 < 0)
							{
								num8 = 0;
							}
							array[num4] = num8 + Lang.tip[5];
							num4++;
						}
						if (toolTip.useStyle > 0)
						{
							if (toolTip.useAnimation <= 8)
							{
								array[num4] = Lang.tip[6];
							}
							else if (toolTip.useAnimation <= 20)
							{
								array[num4] = Lang.tip[7];
							}
							else if (toolTip.useAnimation <= 25)
							{
								array[num4] = Lang.tip[8];
							}
							else if (toolTip.useAnimation <= 30)
							{
								array[num4] = Lang.tip[9];
							}
							else if (toolTip.useAnimation <= 35)
							{
								array[num4] = Lang.tip[10];
							}
							else if (toolTip.useAnimation <= 45)
							{
								array[num4] = Lang.tip[11];
							}
							else if (toolTip.useAnimation <= 55)
							{
								array[num4] = Lang.tip[12];
							}
							else
							{
								array[num4] = Lang.tip[13];
							}
							num4++;
						}
						if (toolTip.knockBack == 0f)
						{
							array[num4] = Lang.tip[14];
						}
						else if ((double)toolTip.knockBack <= 1.5)
						{
							array[num4] = Lang.tip[15];
						}
						else if (toolTip.knockBack <= 3f)
						{
							array[num4] = Lang.tip[16];
						}
						else if (toolTip.knockBack <= 4f)
						{
							array[num4] = Lang.tip[17];
						}
						else if (toolTip.knockBack <= 6f)
						{
							array[num4] = Lang.tip[18];
						}
						else if (toolTip.knockBack <= 7f)
						{
							array[num4] = Lang.tip[19];
						}
						else if (toolTip.knockBack <= 9f)
						{
							array[num4] = Lang.tip[20];
						}
						else if (toolTip.knockBack <= 11f)
						{
							array[num4] = Lang.tip[21];
						}
						else
						{
							array[num4] = Lang.tip[22];
						}
						num4++;
					}
					if (toolTip.headSlot > 0 || toolTip.bodySlot > 0 || toolTip.legSlot > 0 || toolTip.accessory)
					{
						array[num4] = Lang.tip[23];
						num4++;
					}
					if (toolTip.vanity)
					{
						array[num4] = Lang.tip[24];
						num4++;
					}
					if (toolTip.defense > 0)
					{
						array[num4] = toolTip.defense + Lang.tip[25];
						num4++;
					}
					if (toolTip.pick > 0)
					{
						array[num4] = toolTip.pick + Lang.tip[26];
						num4++;
					}
					if (toolTip.axe > 0)
					{
						array[num4] = toolTip.axe * 5 + Lang.tip[27];
						num4++;
					}
					if (toolTip.hammer > 0)
					{
						array[num4] = toolTip.hammer + Lang.tip[28];
						num4++;
					}
					if (toolTip.healLife > 0)
					{
						array[num4] = Lang.tip[29] + " " + toolTip.healLife + " " + Lang.tip[30];
						num4++;
					}
					if (toolTip.healMana > 0)
					{
						array[num4] = Lang.tip[29] + " " + toolTip.healMana + " " + Lang.tip[31];
						num4++;
					}
					if (toolTip.mana > 0 && (toolTip.type != 127 || !player[myPlayer].spaceGun))
					{
						array[num4] = Lang.tip[32] + " " + (int)((float)toolTip.mana * player[myPlayer].manaCost) + " " + Lang.tip[31];
						num4++;
					}
					if (toolTip.createWall > 0 || toolTip.createTile > -1)
					{
						if (toolTip.type != 213)
						{
							array[num4] = Lang.tip[33];
							num4++;
						}
					}
					else if (toolTip.ammo > 0)
					{
						array[num4] = Lang.tip[34];
						num4++;
					}
					else if (toolTip.consumable)
					{
						array[num4] = Lang.tip[35];
						num4++;
					}
					if (toolTip.material)
					{
						array[num4] = Lang.tip[36];
						num4++;
					}
					if (toolTip.toolTip != null)
					{
						array[num4] = toolTip.toolTip;
						num4++;
					}
					if (toolTip.toolTip2 != null)
					{
						array[num4] = toolTip.toolTip2;
						num4++;
					}
					if (toolTip.toolTip3 != null)
					{
						array[num4] = toolTip.toolTip3;
						num4++;
					}
					if (toolTip.toolTip4 != null)
					{
						array[num4] = toolTip.toolTip4;
						num4++;
					}
					if (toolTip.toolTip5 != null)
					{
						array[num4] = toolTip.toolTip5;
						num4++;
					}
					if (toolTip.toolTip6 != null)
					{
						array[num4] = toolTip.toolTip6;
						num4++;
					}
					if (toolTip.toolTip7 != null)
					{
						array[num4] = toolTip.toolTip7;
						num4++;
					}
					if (toolTip.buffTime > 0)
					{
						string text = "0 s";
						text = (array[num4] = ((toolTip.buffTime / 60 < 60) ? (Math.Round((double)toolTip.buffTime / 60.0) + Lang.tip[38]) : (Math.Round((double)(toolTip.buffTime / 60) / 60.0) + Lang.tip[37])));
						num4++;
					}
					if (cpItem == null || cpItem.netID != toolTip.netID)
					{
						cpItem = new Item();
						cpItem.SetDefaults(toolTip.name);
					}
					if (cpItem.damage != toolTip.damage)
					{
						double num9 = (float)toolTip.damage - (float)cpItem.damage;
						num9 = num9 / (double)(float)cpItem.damage * 100.0;
						num9 = Math.Round(num9);
						if (num9 > 0.0)
						{
							array[num4] = "+" + num9 + Lang.tip[39];
						}
						else
						{
							array[num4] = num9 + Lang.tip[39];
						}
						if (num9 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.useAnimation != toolTip.useAnimation)
					{
						double num10 = (float)toolTip.useAnimation - (float)cpItem.useAnimation;
						num10 = num10 / (double)(float)cpItem.useAnimation * 100.0;
						num10 = Math.Round(num10);
						num10 *= -1.0;
						if (num10 > 0.0)
						{
							array[num4] = "+" + num10 + Lang.tip[40];
						}
						else
						{
							array[num4] = num10 + Lang.tip[40];
						}
						if (num10 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.crit != toolTip.crit)
					{
						double num11 = (float)toolTip.crit - (float)cpItem.crit;
						if (num11 > 0.0)
						{
							array[num4] = "+" + num11 + Lang.tip[41];
						}
						else
						{
							array[num4] = num11 + Lang.tip[41];
						}
						if (num11 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.mana != toolTip.mana && cpItem.mana > 0)
					{
						double num12 = (float)toolTip.mana - (float)cpItem.mana;
						num12 = num12 / (double)(float)cpItem.mana * 100.0;
						num12 = Math.Round(num12);
						if (num12 > 0.0)
						{
							array[num4] = "+" + num12 + Lang.tip[42];
						}
						else
						{
							array[num4] = num12 + Lang.tip[42];
						}
						if (num12 > 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.scale != toolTip.scale)
					{
						double num13 = toolTip.scale - cpItem.scale;
						num13 = num13 / (double)cpItem.scale * 100.0;
						num13 = Math.Round(num13);
						if (num13 > 0.0)
						{
							array[num4] = "+" + num13 + Lang.tip[43];
						}
						else
						{
							array[num4] = num13 + Lang.tip[43];
						}
						if (num13 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.shootSpeed != toolTip.shootSpeed)
					{
						double num14 = toolTip.shootSpeed - cpItem.shootSpeed;
						num14 = num14 / (double)cpItem.shootSpeed * 100.0;
						num14 = Math.Round(num14);
						if (num14 > 0.0)
						{
							array[num4] = "+" + num14 + Lang.tip[44];
						}
						else
						{
							array[num4] = num14 + Lang.tip[44];
						}
						if (num14 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (cpItem.knockBack != toolTip.knockBack)
					{
						double num15 = toolTip.knockBack - cpItem.knockBack;
						num15 = num15 / (double)cpItem.knockBack * 100.0;
						num15 = Math.Round(num15);
						if (num15 > 0.0)
						{
							array[num4] = "+" + num15 + Lang.tip[45];
						}
						else
						{
							array[num4] = num15 + Lang.tip[45];
						}
						if (num15 < 0.0)
						{
							array3[num4] = true;
						}
						array2[num4] = true;
						num4++;
					}
					if (toolTip.prefix > 0)
					{
						MouseTip[] array9 = Prefix.prefixes[toolTip.prefix].UpdateTooltip();
						foreach (MouseTip mouseTip in array9)
						{
							array[num4] = mouseTip.text;
							array2[num4] = mouseTip.colored;
							array3[num4] = mouseTip.red;
							num4++;
						}
					}
					if (toolTip.RunMethodRef("UpdateTooltip"))
					{
						MouseTip[] array10 = (MouseTip[])Codable.customMethodReturn;
						MouseTip[] array9 = array10;
						foreach (MouseTip mouseTip2 in array9)
						{
							array[num4] = mouseTip2.text;
							array2[num4] = mouseTip2.colored;
							array3[num4] = mouseTip2.red;
							num4++;
						}
					}
					if (toolTip.wornArmor && player[myPlayer].setBonus != "")
					{
						array[num4] = Lang.tip[48] + " " + player[myPlayer].setBonus;
						num4++;
					}
				}
				if (npcShop > 0)
				{
					if (toolTip.value > 0)
					{
						string text2 = "";
						int num16 = 0;
						int num17 = 0;
						int num18 = 0;
						int num19 = 0;
						int num20 = toolTip.value * toolTip.stack;
						if (!toolTip.buy)
						{
							num20 = toolTip.value / 5 * toolTip.stack;
						}
						if (num20 < 1)
						{
							num20 = 1;
						}
						if (num20 >= 1000000)
						{
							num16 = num20 / 1000000;
							num20 -= num16 * 1000000;
						}
						if (num20 >= 10000)
						{
							num17 = num20 / 10000;
							num20 -= num17 * 10000;
						}
						if (num20 >= 100)
						{
							num18 = num20 / 100;
							num20 -= num18 * 100;
						}
						if (num20 >= 1)
						{
							num19 = num20;
						}
						if (num16 > 0)
						{
							object obj2 = text2;
							text2 = string.Concat(obj2, num16, " ", Lang.inter[15]);
						}
						if (num17 > 0)
						{
							object obj3 = text2;
							text2 = string.Concat(obj3, num17, " ", Lang.inter[16]);
						}
						if (num18 > 0)
						{
							object obj4 = text2;
							text2 = string.Concat(obj4, num18, " ", Lang.inter[17]);
						}
						if (num19 > 0)
						{
							object obj5 = text2;
							text2 = string.Concat(obj5, num19, " ", Lang.inter[18]);
						}
						if (!toolTip.buy)
						{
							array[num4] = Lang.tip[49] + " " + text2;
						}
						else
						{
							array[num4] = Lang.tip[50] + " " + text2;
						}
						num4++;
						num21 = (float)(int)mouseTextColor / 255f;
						if (num16 > 0)
						{
							color = new Color((byte)(colorShopPlatinum[0] * num21), (byte)(colorShopPlatinum[1] * num21), (byte)(colorShopPlatinum[2] * num21), mouseTextColor);
						}
						else if (num17 > 0)
						{
							color = new Color((byte)(colorShopGold[0] * num21), (byte)(colorShopGold[1] * num21), (byte)(colorShopGold[2] * num21), mouseTextColor);
						}
						else if (num18 > 0)
						{
							color = new Color((byte)(colorShopSilver[0] * num21), (byte)(colorShopSilver[1] * num21), (byte)(colorShopSilver[2] * num21), mouseTextColor);
						}
						else if (num19 > 0)
						{
							color = new Color((byte)(colorShopCopper[0] * num21), (byte)(colorShopCopper[1] * num21), (byte)(colorShopCopper[2] * num21), mouseTextColor);
						}
					}
					else
					{
						num21 = (float)(int)mouseTextColor / 255f;
						array[num4] = Lang.tip[51];
						num4++;
						color = new Color((byte)(120f * num21), (byte)(120f * num21), (byte)(120f * num21), mouseTextColor);
					}
				}
				Vector2 vector = default(Vector2);
				int num22 = 0;
				for (int k = 0; k < num4; k++)
				{
					Vector2 vector2 = fontMouseText.MeasureString(array[k]);
					if (vector2.X > vector.X)
					{
						vector.X = vector2.X;
					}
					vector.Y += vector2.Y + (float)num22;
				}
				if ((float)num + vector.X + 4f > (float)screenWidth)
				{
					num = (int)((float)screenWidth - vector.X - 4f);
				}
				if ((float)num2 + vector.Y + 4f > (float)screenHeight)
				{
					num2 = (int)((float)screenHeight - vector.Y - 4f);
				}
				int num23 = 0;
				num21 = (float)(int)mouseTextColor / 255f;
				if (Codable.RunGlobalMethod("ModGeneric", "ItemMouseText", this.spriteBatch, cursorText, rare, diff, array, array2, array3) && !(bool)Codable.customMethodReturn)
				{
					return;
				}
				for (int l = 0; l < num4; l++)
				{
					for (int m = 0; m < 5; m++)
					{
						int num24 = num;
						int num25 = num2 + num23;
						Color color2 = Color.Black;
						switch (m)
						{
						case 0:
							num24 -= 2;
							break;
						case 1:
							num24 += 2;
							break;
						case 2:
							num25 -= 2;
							break;
						case 3:
							num25 += 2;
							break;
						default:
							color2 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
							if (l == 0)
							{
								if (colorRarity.ContainsKey(rare))
								{
									color2 = new Color((byte)(colorRarity[rare][0] * num21), (byte)(colorRarity[rare][1] * num21), (byte)(colorRarity[rare][2] * num21), mouseTextColor);
								}
								if (diff == 1)
								{
									color2 = new Color((byte)((float)(int)mcColor.R * num21), (byte)((float)(int)mcColor.G * num21), (byte)((float)(int)mcColor.B * num21), mouseTextColor);
								}
								if (diff == 2)
								{
									color2 = new Color((byte)((float)(int)hcColor.R * num21), (byte)((float)(int)hcColor.G * num21), (byte)((float)(int)hcColor.B * num21), mouseTextColor);
								}
							}
							else if (array2[l])
							{
								color2 = ((!array3[l]) ? new Color((byte)(120f * num21), (byte)(190f * num21), (byte)(120f * num21), mouseTextColor) : new Color((byte)(190f * num21), (byte)(120f * num21), (byte)(120f * num21), mouseTextColor));
							}
							else if (l == num4 - 1)
							{
								color2 = color;
							}
							break;
						}
						SpriteBatch spriteBatch = this.spriteBatch;
						SpriteFont spriteFont = fontMouseText;
						string text3 = array[l];
						Vector2 position = new Vector2(num24, num25);
						Color color3 = color2;
						float rotation = 0f;
						spriteBatch.DrawString(spriteFont, text3, position, color3, rotation, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					num23 += (int)(fontMouseText.MeasureString(array[l]).Y + (float)num22);
				}
				return;
			}
			if (buffString != "" && buffString != null)
			{
				for (int n = 0; n < 5; n++)
				{
					int num26 = num;
					int num27 = num2 + (int)fontMouseText.MeasureString(buffString).Y;
					Color color4 = Color.Black;
					switch (n)
					{
					case 0:
						num26 -= 2;
						break;
					case 1:
						num26 += 2;
						break;
					case 2:
						num27 -= 2;
						break;
					case 3:
						num27 += 2;
						break;
					default:
						color4 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						break;
					}
					SpriteBatch spriteBatch2 = this.spriteBatch;
					SpriteFont spriteFont2 = fontMouseText;
					string text4 = buffString;
					Vector2 position2 = new Vector2(num26, num27);
					Color color5 = color4;
					float rotation2 = 0f;
					spriteBatch2.DrawString(spriteFont2, text4, position2, color5, rotation2, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			Vector2 vector3 = fontMouseText.MeasureString(cursorText);
			if ((float)num + vector3.X + 4f > (float)screenWidth)
			{
				num = (int)((float)screenWidth - vector3.X - 4f);
			}
			if ((float)num2 + vector3.Y + 4f > (float)screenHeight)
			{
				num2 = (int)((float)screenHeight - vector3.Y - 4f);
			}
			SpriteBatch spriteBatch3 = this.spriteBatch;
			SpriteFont spriteFont3 = fontMouseText;
			Vector2 position3 = new Vector2(num, num2 - 2);
			Color black = Color.Black;
			float rotation3 = 0f;
			spriteBatch3.DrawString(spriteFont3, cursorText, position3, black, rotation3, default(Vector2), 1f, SpriteEffects.None, 0f);
			SpriteBatch spriteBatch4 = this.spriteBatch;
			SpriteFont spriteFont4 = fontMouseText;
			Vector2 position4 = new Vector2(num, num2 + 2);
			Color black2 = Color.Black;
			float rotation4 = 0f;
			spriteBatch4.DrawString(spriteFont4, cursorText, position4, black2, rotation4, default(Vector2), 1f, SpriteEffects.None, 0f);
			SpriteBatch spriteBatch5 = this.spriteBatch;
			SpriteFont spriteFont5 = fontMouseText;
			Vector2 position5 = new Vector2(num - 2, num2);
			Color black3 = Color.Black;
			float rotation5 = 0f;
			spriteBatch5.DrawString(spriteFont5, cursorText, position5, black3, rotation5, default(Vector2), 1f, SpriteEffects.None, 0f);
			SpriteBatch spriteBatch6 = this.spriteBatch;
			SpriteFont spriteFont6 = fontMouseText;
			Vector2 position6 = new Vector2(num + 2, num2);
			Color black4 = Color.Black;
			float rotation6 = 0f;
			spriteBatch6.DrawString(spriteFont6, cursorText, position6, black4, rotation6, default(Vector2), 1f, SpriteEffects.None, 0f);
			num21 = (float)(int)mouseTextColor / 255f;
			Color color6 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
			if (colorRarity.ContainsKey(rare))
			{
				color6 = new Color((byte)(colorRarity[rare][0] * num21), (byte)(colorRarity[rare][1] * num21), (byte)(colorRarity[rare][2] * num21), mouseTextColor);
			}
			if (diff == 1)
			{
				color6 = new Color((byte)((float)(int)mcColor.R * num21), (byte)((float)(int)mcColor.G * num21), (byte)((float)(int)mcColor.B * num21), mouseTextColor);
			}
			if (diff == 2)
			{
				color6 = new Color((byte)((float)(int)hcColor.R * num21), (byte)((float)(int)hcColor.G * num21), (byte)((float)(int)hcColor.B * num21), mouseTextColor);
			}
			SpriteBatch spriteBatch7 = this.spriteBatch;
			SpriteFont spriteFont7 = fontMouseText;
			Vector2 position7 = new Vector2(num, num2);
			Color color7 = color6;
			float rotation7 = 0f;
			spriteBatch7.DrawString(spriteFont7, cursorText, position7, color7, rotation7, default(Vector2), 1f, SpriteEffects.None, 0f);
		}

		public void DrawFPS()
		{
			if (showFrameRate)
			{
				string text = string.Concat(frameRate);
				object obj = text;
				text = string.Concat(obj, " (", (int)(gfxQuality * 100f), "%)");
				int num = 4;
				if (!gameMenu)
				{
					num = screenHeight - 24;
				}
				spriteBatch.DrawString(fontMouseText, text + " " + debugWords, new Vector2(4f, num), new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
		}

		public static Color shine(Color newColor, int type)
		{
			int r = newColor.R;
			int r2 = newColor.R;
			int r3 = newColor.R;
			float num = 0.6f;
			switch (type)
			{
			case 25:
				r = (int)((float)(int)newColor.R * 0.95f);
				r2 = (int)((float)(int)newColor.G * 0.85f);
				r3 = (int)((double)(float)(int)newColor.B * 1.1);
				break;
			case 117:
				r = (int)((float)(int)newColor.R * 1.1f);
				r2 = (int)((float)(int)newColor.G * 1f);
				r3 = (int)((double)(float)(int)newColor.B * 1.2);
				break;
			default:
				r = (int)((float)(int)newColor.R * (1f + num));
				r2 = (int)((float)(int)newColor.G * (1f + num));
				r3 = (int)((float)(int)newColor.B * (1f + num));
				break;
			}
			if (r > 255)
			{
				r = 255;
			}
			if (r2 > 255)
			{
				r2 = 255;
			}
			if (r3 > 255)
			{
				r3 = 255;
			}
			newColor.R = (byte)r;
			newColor.G = (byte)r2;
			newColor.B = (byte)r3;
			return new Color((byte)r, (byte)r2, (byte)r3, newColor.A);
		}

		public void DrawTiles(bool solidOnly = true)
		{
			TMod.RunMethod(TMod.WorldHooks.PreDrawTiles, this.spriteBatch, solidOnly);
			Config.tilesPretend = false;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - gfxQuality) + 30f * gfxQuality);
			int num2 = (int)(50f * (1f - gfxQuality) + 2f * gfxQuality);
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			int num3 = 0;
			int[] array = new int[1000];
			int[] array2 = new int[1000];
			int num4 = (int)((screenPosition.X - value.X) / 16f - 1f);
			int num5 = (int)((screenPosition.X + (float)screenWidth + value.X) / 16f) + 2;
			int num6 = (int)((screenPosition.Y - value.Y) / 16f - 1f);
			int num7 = (int)((screenPosition.Y + (float)screenHeight + value.Y) / 16f) + 5;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > maxTilesX)
			{
				num5 = maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > maxTilesY)
			{
				num7 = maxTilesY;
			}
			int num8 = 16;
			int num9 = 16;
			for (int i = num6; i < num7 + 4; i++)
			{
				for (int j = num4 - 2; j < num5 + 2; j++)
				{
					if (tile[j, i] == null)
					{
						tile[j, i] = new Tile();
					}
					bool flag = tileSolid[tile[j, i].type];
					if (tile[j, i].type == 11)
					{
						flag = true;
					}
					if (!tile[j, i].active || flag != solidOnly)
					{
						continue;
					}
					Color color = Lighting.GetColor(j, i);
					int num10 = 0;
					if (tile[j, i].type == 78 || tile[j, i].type == 85)
					{
						num10 = 2;
					}
					if (tile[j, i].type == 33 || tile[j, i].type == 49)
					{
						num10 = -4;
					}
					if (tile[j, i].type == 3 || tile[j, i].type == 4 || tile[j, i].type == 5 || tile[j, i].type == 24 || tile[j, i].type == 33 || tile[j, i].type == 49 || tile[j, i].type == 61 || tile[j, i].type == 71 || tile[j, i].type == 110)
					{
						num8 = 20;
					}
					else if (tile[j, i].type == 15 || tile[j, i].type == 14 || tile[j, i].type == 16 || tile[j, i].type == 17 || tile[j, i].type == 18 || tile[j, i].type == 20 || tile[j, i].type == 21 || tile[j, i].type == 26 || tile[j, i].type == 27 || tile[j, i].type == 32 || tile[j, i].type == 69 || tile[j, i].type == 72 || tile[j, i].type == 77 || tile[j, i].type == 80)
					{
						num8 = 18;
					}
					else if (tile[j, i].type == 137)
					{
						num8 = 18;
					}
					else if (tile[j, i].type == 135)
					{
						num10 = 2;
						num8 = 18;
					}
					else if (tile[j, i].type == 132)
					{
						num10 = 2;
						num8 = 18;
					}
					else
					{
						num8 = 16;
					}
					num9 = ((tile[j, i].type != 4 && tile[j, i].type != 5) ? 16 : 20);
					if (tile[j, i].type == 73 || tile[j, i].type == 74 || tile[j, i].type == 113)
					{
						num10 -= 12;
						num8 = 32;
					}
					if (tile[j, i].type == 81)
					{
						num10 -= 8;
						num8 = 26;
						num9 = 24;
					}
					if (tile[j, i].type == 105)
					{
						num10 = 2;
					}
					if (tile[j, i].type == 124)
					{
						num8 = 18;
					}
					if (tile[j, i].type == 137)
					{
						num8 = 18;
					}
					if (tile[j, i].type == 138)
					{
						num8 = 18;
					}
					if (tile[j, i].type == 139 || tile[j, i].type == 142 || tile[j, i].type == 143)
					{
						num10 = 2;
					}
					if (player[myPlayer].findTreasure && (tile[j, i].type == 6 || tile[j, i].type == 7 || tile[j, i].type == 8 || tile[j, i].type == 9 || tile[j, i].type == 12 || tile[j, i].type == 21 || tile[j, i].type == 22 || tile[j, i].type == 28 || tile[j, i].type == 107 || tile[j, i].type == 108 || tile[j, i].type == 111 || (tile[j, i].type >= 63 && tile[j, i].type <= 68) || tileAlch[tile[j, i].type] || Config.tileDefs.treasure[tile[j, i].type]))
					{
						if (color.R < (int)mouseTextColor / 2)
						{
							color.R = (byte)((int)mouseTextColor / 2);
						}
						if (color.G < 70)
						{
							color.G = 70;
						}
						if (color.B < 210)
						{
							color.B = 210;
						}
						color.A = mouseTextColor;
						if (!gamePaused && base.IsActive && rand.Next(150) == 0)
						{
							Vector2 position = new Vector2(j * 16, i * 16);
							int width = 16;
							int height = 16;
							int type = 15;
							float speedX = 0f;
							float speedY = 0f;
							int alpha = 150;
							int num11 = Dust.NewDust(position, width, height, type, speedX, speedY, alpha, default(Color), 0.8f);
							Dust dust = Main.dust[num11];
							dust.velocity *= 0.1f;
							Main.dust[num11].noLight = true;
						}
					}
					if (!gamePaused && base.IsActive)
					{
						if (tile[j, i].type == 4 && rand.Next(40) == 0 && tile[j, i].frameX < 66)
						{
							int num12 = tile[j, i].frameY / 22;
							switch (num12)
							{
							case 0:
								num12 = 6;
								break;
							case 8:
								num12 = 75;
								break;
							default:
								num12 = 58 + num12;
								break;
							}
							if (tile[j, i].frameX == 22)
							{
								Vector2 position2 = new Vector2(j * 16 + 6, i * 16);
								int width2 = 4;
								int height2 = 4;
								int type2 = num12;
								float speedX2 = 0f;
								float speedY2 = 0f;
								int alpha2 = 100;
								Dust.NewDust(position2, width2, height2, type2, speedX2, speedY2, alpha2);
							}
							if (tile[j, i].frameX == 44)
							{
								Vector2 position3 = new Vector2(j * 16 + 2, i * 16);
								int width3 = 4;
								int height3 = 4;
								int type3 = num12;
								float speedX3 = 0f;
								float speedY3 = 0f;
								int alpha3 = 100;
								Dust.NewDust(position3, width3, height3, type3, speedX3, speedY3, alpha3);
							}
							else
							{
								Vector2 position4 = new Vector2(j * 16 + 4, i * 16);
								int width4 = 4;
								int height4 = 4;
								int type4 = num12;
								float speedX4 = 0f;
								float speedY4 = 0f;
								int alpha4 = 100;
								Dust.NewDust(position4, width4, height4, type4, speedX4, speedY4, alpha4);
							}
						}
						if (tile[j, i].type == 33 && rand.Next(40) == 0 && tile[j, i].frameX == 0)
						{
							Vector2 position5 = new Vector2(j * 16 + 4, i * 16 - 4);
							int width5 = 4;
							int height5 = 4;
							int type5 = 6;
							float speedX5 = 0f;
							float speedY5 = 0f;
							int alpha5 = 100;
							Dust.NewDust(position5, width5, height5, type5, speedX5, speedY5, alpha5);
						}
						if (tile[j, i].type == 93 && rand.Next(40) == 0 && tile[j, i].frameX == 0 && tile[j, i].frameY == 0)
						{
							Vector2 position6 = new Vector2(j * 16 + 4, i * 16 + 2);
							int width6 = 4;
							int height6 = 4;
							int type6 = 6;
							float speedX6 = 0f;
							float speedY6 = 0f;
							int alpha6 = 100;
							Dust.NewDust(position6, width6, height6, type6, speedX6, speedY6, alpha6);
						}
						if (tile[j, i].type == 100 && rand.Next(40) == 0 && tile[j, i].frameX < 36 && tile[j, i].frameY == 0)
						{
							if (tile[j, i].frameX == 0)
							{
								if (rand.Next(3) == 0)
								{
									Vector2 position7 = new Vector2(j * 16 + 4, i * 16 + 2);
									int width7 = 4;
									int height7 = 4;
									int type7 = 6;
									float speedX7 = 0f;
									float speedY7 = 0f;
									int alpha7 = 100;
									Dust.NewDust(position7, width7, height7, type7, speedX7, speedY7, alpha7);
								}
								else
								{
									Vector2 position8 = new Vector2(j * 16 + 14, i * 16 + 2);
									int width8 = 4;
									int height8 = 4;
									int type8 = 6;
									float speedX8 = 0f;
									float speedY8 = 0f;
									int alpha8 = 100;
									Dust.NewDust(position8, width8, height8, type8, speedX8, speedY8, alpha8);
								}
							}
							else if (rand.Next(3) == 0)
							{
								Vector2 position9 = new Vector2(j * 16 + 6, i * 16 + 2);
								int width9 = 4;
								int height9 = 4;
								int type9 = 6;
								float speedX9 = 0f;
								float speedY9 = 0f;
								int alpha9 = 100;
								Dust.NewDust(position9, width9, height9, type9, speedX9, speedY9, alpha9);
							}
							else
							{
								Vector2 position10 = new Vector2(j * 16, i * 16 + 2);
								int width10 = 4;
								int height10 = 4;
								int type10 = 6;
								float speedX10 = 0f;
								float speedY10 = 0f;
								int alpha10 = 100;
								Dust.NewDust(position10, width10, height10, type10, speedX10, speedY10, alpha10);
							}
						}
						if (tile[j, i].type == 98 && rand.Next(40) == 0 && tile[j, i].frameY == 0 && tile[j, i].frameX == 0)
						{
							Vector2 position11 = new Vector2(j * 16 + 12, i * 16 + 2);
							int width11 = 4;
							int height11 = 4;
							int type11 = 6;
							float speedX11 = 0f;
							float speedY11 = 0f;
							int alpha11 = 100;
							Dust.NewDust(position11, width11, height11, type11, speedX11, speedY11, alpha11);
						}
						if (tile[j, i].type == 49 && rand.Next(20) == 0)
						{
							Vector2 position12 = new Vector2(j * 16 + 4, i * 16 - 4);
							int width12 = 4;
							int height12 = 4;
							int type12 = 29;
							float speedX12 = 0f;
							float speedY12 = 0f;
							int alpha12 = 100;
							Dust.NewDust(position12, width12, height12, type12, speedX12, speedY12, alpha12);
						}
						if ((tile[j, i].type == 34 || tile[j, i].type == 35 || tile[j, i].type == 36) && rand.Next(40) == 0 && tile[j, i].frameX < 54 && tile[j, i].frameY == 18 && (tile[j, i].frameX == 0 || tile[j, i].frameX == 36))
						{
							Vector2 position13 = new Vector2(j * 16, i * 16 + 2);
							int width13 = 14;
							int height13 = 6;
							int type13 = 6;
							float speedX13 = 0f;
							float speedY13 = 0f;
							int alpha13 = 100;
							Dust.NewDust(position13, width13, height13, type13, speedX13, speedY13, alpha13);
						}
						if (tile[j, i].type == 22 && rand.Next(400) == 0)
						{
							Vector2 position14 = new Vector2(j * 16, i * 16);
							int width14 = 16;
							int height14 = 16;
							int type14 = 14;
							float speedX14 = 0f;
							float speedY14 = 0f;
							int alpha14 = 0;
							Dust.NewDust(position14, width14, height14, type14, speedX14, speedY14, alpha14);
						}
						else if ((tile[j, i].type == 23 || tile[j, i].type == 24 || tile[j, i].type == 32) && rand.Next(500) == 0)
						{
							Vector2 position15 = new Vector2(j * 16, i * 16);
							int width15 = 16;
							int height15 = 16;
							int type15 = 14;
							float speedX15 = 0f;
							float speedY15 = 0f;
							int alpha15 = 0;
							Dust.NewDust(position15, width15, height15, type15, speedX15, speedY15, alpha15);
						}
						else if (tile[j, i].type == 25 && rand.Next(700) == 0)
						{
							Vector2 position16 = new Vector2(j * 16, i * 16);
							int width16 = 16;
							int height16 = 16;
							int type16 = 14;
							float speedX16 = 0f;
							float speedY16 = 0f;
							int alpha16 = 0;
							Dust.NewDust(position16, width16, height16, type16, speedX16, speedY16, alpha16);
						}
						else if (tile[j, i].type == 112 && rand.Next(700) == 0)
						{
							Vector2 position17 = new Vector2(j * 16, i * 16);
							int width17 = 16;
							int height17 = 16;
							int type17 = 14;
							float speedX17 = 0f;
							float speedY17 = 0f;
							int alpha17 = 0;
							Dust.NewDust(position17, width17, height17, type17, speedX17, speedY17, alpha17);
						}
						else if (tile[j, i].type == 31 && rand.Next(20) == 0)
						{
							Vector2 position18 = new Vector2(j * 16, i * 16);
							int width18 = 16;
							int height18 = 16;
							int type18 = 14;
							float speedX18 = 0f;
							float speedY18 = 0f;
							int alpha18 = 100;
							Dust.NewDust(position18, width18, height18, type18, speedX18, speedY18, alpha18);
						}
						else if (tile[j, i].type == 26 && rand.Next(20) == 0)
						{
							Vector2 position19 = new Vector2(j * 16, i * 16);
							int width19 = 16;
							int height19 = 16;
							int type19 = 14;
							float speedX19 = 0f;
							float speedY19 = 0f;
							int alpha19 = 100;
							Dust.NewDust(position19, width19, height19, type19, speedX19, speedY19, alpha19);
						}
						else if ((tile[j, i].type == 71 || tile[j, i].type == 72) && rand.Next(500) == 0)
						{
							Vector2 position20 = new Vector2(j * 16, i * 16);
							int width20 = 16;
							int height20 = 16;
							int type20 = 41;
							float speedX20 = 0f;
							float speedY20 = 0f;
							int alpha20 = 250;
							Dust.NewDust(position20, width20, height20, type20, speedX20, speedY20, alpha20, default(Color), 0.8f);
						}
						else if ((tile[j, i].type == 17 || tile[j, i].type == 77 || tile[j, i].type == 133) && rand.Next(40) == 0)
						{
							if ((tile[j, i].frameX == 18) & (tile[j, i].frameY == 18))
							{
								Vector2 position21 = new Vector2(j * 16 + 2, i * 16);
								int width21 = 8;
								int height21 = 6;
								int type21 = 6;
								float speedX21 = 0f;
								float speedY21 = 0f;
								int alpha21 = 100;
								Dust.NewDust(position21, width21, height21, type21, speedX21, speedY21, alpha21);
							}
						}
						else if (tile[j, i].type == 37 && rand.Next(250) == 0)
						{
							Vector2 position22 = new Vector2(j * 16, i * 16);
							int width22 = 16;
							int height22 = 16;
							int type22 = 6;
							float speedX22 = 0f;
							float speedY22 = 0f;
							int alpha22 = 0;
							int num13 = Dust.NewDust(position22, width22, height22, type22, speedX22, speedY22, alpha22, default(Color), rand.Next(3));
							if (Main.dust[num13].scale > 1f)
							{
								Main.dust[num13].noGravity = true;
							}
						}
						else if ((tile[j, i].type == 58 || tile[j, i].type == 76) && rand.Next(250) == 0)
						{
							Vector2 position23 = new Vector2(j * 16, i * 16);
							int width23 = 16;
							int height23 = 16;
							int type23 = 6;
							float speedX23 = 0f;
							float speedY23 = 0f;
							int alpha23 = 0;
							int num14 = Dust.NewDust(position23, width23, height23, type23, speedX23, speedY23, alpha23, default(Color), rand.Next(3));
							if (Main.dust[num14].scale > 1f)
							{
								Main.dust[num14].noGravity = true;
							}
							Main.dust[num14].noLight = true;
						}
						else if (tile[j, i].type == 61)
						{
							if (tile[j, i].frameX == 144)
							{
								if (rand.Next(60) == 0)
								{
									Vector2 position24 = new Vector2(j * 16, i * 16);
									int width24 = 16;
									int height24 = 16;
									int type24 = 44;
									float speedX24 = 0f;
									float speedY24 = 0f;
									int alpha24 = 250;
									int num15 = Dust.NewDust(position24, width24, height24, type24, speedX24, speedY24, alpha24, default(Color), 0.4f);
									Main.dust[num15].fadeIn = 0.7f;
								}
								color.A = (byte)(245f - (float)(int)mouseTextColor * 1.5f);
								color.R = (byte)(245f - (float)(int)mouseTextColor * 1.5f);
								color.B = (byte)(245f - (float)(int)mouseTextColor * 1.5f);
								color.G = (byte)(245f - (float)(int)mouseTextColor * 1.5f);
							}
						}
						else if (tileShine[tile[j, i].type] > 0 && (color.R > 20 || color.B > 20 || color.G > 20))
						{
							int num16 = color.R;
							if (color.G > num16)
							{
								num16 = color.G;
							}
							if (color.B > num16)
							{
								num16 = color.B;
							}
							num16 /= 30;
							if (rand.Next(tileShine[tile[j, i].type]) < num16 && (tile[j, i].type != 21 || (tile[j, i].frameX >= 36 && tile[j, i].frameX < 180)))
							{
								Vector2 position25 = new Vector2(j * 16, i * 16);
								int width25 = 16;
								int height25 = 16;
								int type25 = 43;
								float speedX25 = 0f;
								float speedY25 = 0f;
								int alpha25 = 254;
								int num17 = Dust.NewDust(position25, width25, height25, type25, speedX25, speedY25, alpha25, default(Color), 0.5f);
								Dust dust2 = Main.dust[num17];
								dust2.velocity *= 0f;
							}
						}
					}
					if (tile[j, i].type == 128 && tile[j, i].frameX >= 100)
					{
						array[num3] = j;
						array2[num3] = i;
						num3++;
					}
					if (tile[j, i].type == 5 && tile[j, i].frameY >= 198 && tile[j, i].frameX >= 22)
					{
						array[num3] = j;
						array2[num3] = i;
						num3++;
					}
					if (tile[j, i].type == 72 && tile[j, i].frameX >= 36)
					{
						int num18 = 0;
						if (tile[j, i].frameY == 18)
						{
							num18 = 1;
						}
						else if (tile[j, i].frameY == 36)
						{
							num18 = 2;
						}
						SpriteBatch spriteBatch = this.spriteBatch;
						Texture2D texture = shroomCapTexture;
						Vector2 position26 = new Vector2(j * 16 - (int)screenPosition.X - 22, i * 16 - (int)screenPosition.Y - 26) + value;
						Rectangle? sourceRectangle = new Rectangle(num18 * 62, 0, 60, 42);
						Color color2 = Lighting.GetColor(j, i);
						float rotation = 0f;
						spriteBatch.Draw(texture, position26, sourceRectangle, color2, rotation, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					if (color.R <= 1 && color.G <= 1 && color.B <= 1)
					{
						continue;
					}
					if (tile[j - 1, i] == null)
					{
						tile[j - 1, i] = new Tile();
					}
					if (tile[j + 1, i] == null)
					{
						tile[j + 1, i] = new Tile();
					}
					if (tile[j, i - 1] == null)
					{
						tile[j, i - 1] = new Tile();
					}
					if (tile[j, i + 1] == null)
					{
						tile[j, i + 1] = new Tile();
					}
					if (solidOnly && flag && !tileSolidTop[tile[j, i].type] && (tile[j - 1, i].liquid > 0 || tile[j + 1, i].liquid > 0 || tile[j, i - 1].liquid > 0 || tile[j, i + 1].liquid > 0))
					{
						Color color3 = Lighting.GetColor(j, i);
						int num19 = 0;
						bool flag2 = false;
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						int num20 = 0;
						bool flag6 = false;
						if (tile[j - 1, i].liquid > num19)
						{
							num19 = tile[j - 1, i].liquid;
							flag2 = true;
						}
						else if (tile[j - 1, i].liquid > 0)
						{
							flag2 = true;
						}
						if (tile[j + 1, i].liquid > num19)
						{
							num19 = tile[j + 1, i].liquid;
							flag3 = true;
						}
						else if (tile[j + 1, i].liquid > 0)
						{
							num19 = tile[j + 1, i].liquid;
							flag3 = true;
						}
						if (tile[j, i - 1].liquid > 0)
						{
							flag4 = true;
						}
						if (tile[j, i + 1].liquid > 240)
						{
							flag5 = true;
						}
						if (tile[j - 1, i].liquid > 0)
						{
							if (tile[j - 1, i].lava)
							{
								num20 = 1;
							}
							else
							{
								flag6 = true;
							}
						}
						if (tile[j + 1, i].liquid > 0)
						{
							if (tile[j + 1, i].lava)
							{
								num20 = 1;
							}
							else
							{
								flag6 = true;
							}
						}
						if (tile[j, i - 1].liquid > 0)
						{
							if (tile[j, i - 1].lava)
							{
								num20 = 1;
							}
							else
							{
								flag6 = true;
							}
						}
						if (tile[j, i + 1].liquid > 0)
						{
							if (tile[j, i + 1].lava)
							{
								num20 = 1;
							}
							else
							{
								flag6 = true;
							}
						}
						if (!flag6 || num20 != 1)
						{
							Vector2 value2 = new Vector2(j * 16, i * 16);
							Rectangle value3 = new Rectangle(0, 4, 16, 16);
							if (flag5 && (flag2 || flag3))
							{
								flag2 = true;
								flag3 = true;
							}
							if ((!flag4 || (!flag2 && !flag3)) && (!flag5 || !flag4))
							{
								if (flag4)
								{
									value3 = new Rectangle(0, 4, 16, 4);
								}
								else if (flag5 && !flag2 && !flag3)
								{
									value2 = new Vector2(j * 16, i * 16 + 12);
									value3 = new Rectangle(0, 4, 16, 4);
								}
								else
								{
									float num21 = 256 - num19;
									num21 /= 32f;
									if (flag2 && flag3)
									{
										value2 = new Vector2(j * 16, i * 16 + (int)num21 * 2);
										value3 = new Rectangle(0, 4, 16, 16 - (int)num21 * 2);
									}
									else if (!flag2)
									{
										value2 = new Vector2(j * 16 + 12, i * 16 + (int)num21 * 2);
										value3 = new Rectangle(0, 4, 4, 16 - (int)num21 * 2);
									}
									else
									{
										value2 = new Vector2(j * 16, i * 16 + (int)num21 * 2);
										value3 = new Rectangle(0, 4, 4, 16 - (int)num21 * 2);
									}
								}
							}
							float num22 = 0.5f;
							if (num20 == 1)
							{
								num22 *= 1.6f;
							}
							if ((double)i < worldSurface || num22 > 1f)
							{
								num22 = 1f;
							}
							float num23 = (float)(int)color3.R * num22;
							float num24 = (float)(int)color3.G * num22;
							float num25 = (float)(int)color3.B * num22;
							float num26 = (float)(int)color3.A * num22;
							color3 = new Color((byte)num23, (byte)num24, (byte)num25, (byte)num26);
							SpriteBatch spriteBatch2 = this.spriteBatch;
							Texture2D texture2 = liquidTexture[num20];
							Vector2 position27 = value2 - screenPosition + value;
							Rectangle? sourceRectangle2 = value3;
							Color color4 = color3;
							float rotation2 = 0f;
							spriteBatch2.Draw(texture2, position27, sourceRectangle2, color4, rotation2, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
					if (tile[j, i].type == 51)
					{
						Color color5 = Lighting.GetColor(j, i);
						float num27 = 0.5f;
						float num28 = (float)(int)color5.R * num27;
						float num29 = (float)(int)color5.G * num27;
						float num30 = (float)(int)color5.B * num27;
						float num31 = (float)(int)color5.A * num27;
						color5 = new Color((byte)num28, (byte)num29, (byte)num30, (byte)num31);
						SpriteBatch spriteBatch3 = this.spriteBatch;
						Texture2D texture3 = tileTexture[tile[j, i].type];
						Vector2 position28 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle3 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
						Color color6 = color5;
						float rotation3 = 0f;
						spriteBatch3.Draw(texture3, position28, sourceRectangle3, color6, rotation3, default(Vector2), 1f, SpriteEffects.None, 0f);
						continue;
					}
					if (tile[j, i].type == 129)
					{
						SpriteBatch spriteBatch4 = this.spriteBatch;
						Texture2D texture4 = tileTexture[tile[j, i].type];
						Vector2 position29 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle4 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
						Color color7 = new Color(200, 200, 200, 0);
						float rotation4 = 0f;
						spriteBatch4.Draw(texture4, position29, sourceRectangle4, color7, rotation4, default(Vector2), 1f, SpriteEffects.None, 0f);
						continue;
					}
					if (tileAlch[tile[j, i].type])
					{
						num8 = 20;
						num10 = -1;
						int num32 = tile[j, i].type;
						int num33 = tile[j, i].frameX / 18;
						if (num32 > 82)
						{
							if (num33 == 0 && dayTime)
							{
								num32 = 84;
							}
							if (num33 == 1 && !dayTime)
							{
								num32 = 84;
							}
							if (num33 == 3 && bloodMoon)
							{
								num32 = 84;
							}
						}
						if (num32 == 84)
						{
							if (num33 == 0 && rand.Next(100) == 0)
							{
								Vector2 position30 = new Vector2(j * 16, i * 16 - 4);
								int width26 = 16;
								int height26 = 16;
								int type26 = 19;
								float speedX26 = 0f;
								float speedY26 = 0f;
								int alpha26 = 160;
								int num34 = Dust.NewDust(position30, width26, height26, type26, speedX26, speedY26, alpha26, default(Color), 0.1f);
								Dust dust3 = Main.dust[num34];
								dust3.velocity.X = dust3.velocity.X / 2f;
								Dust dust4 = Main.dust[num34];
								dust4.velocity.Y = dust4.velocity.Y / 2f;
								Main.dust[num34].noGravity = true;
								Main.dust[num34].fadeIn = 1f;
							}
							if (num33 == 1 && rand.Next(100) == 0)
							{
								Vector2 position31 = new Vector2(j * 16, i * 16);
								int width27 = 16;
								int height27 = 16;
								int type27 = 41;
								float speedX27 = 0f;
								float speedY27 = 0f;
								int alpha27 = 250;
								Dust.NewDust(position31, width27, height27, type27, speedX27, speedY27, alpha27, default(Color), 0.8f);
							}
							if (num33 == 3)
							{
								if (rand.Next(200) == 0)
								{
									Vector2 position32 = new Vector2(j * 16, i * 16);
									int width28 = 16;
									int height28 = 16;
									int type28 = 14;
									float speedX28 = 0f;
									float speedY28 = 0f;
									int alpha28 = 100;
									int num35 = Dust.NewDust(position32, width28, height28, type28, speedX28, speedY28, alpha28, default(Color), 0.2f);
									Main.dust[num35].fadeIn = 1.2f;
								}
								if (rand.Next(75) == 0)
								{
									Vector2 position33 = new Vector2(j * 16, i * 16);
									int width29 = 16;
									int height29 = 16;
									int type29 = 27;
									float speedX29 = 0f;
									float speedY29 = 0f;
									int alpha29 = 100;
									int num36 = Dust.NewDust(position33, width29, height29, type29, speedX29, speedY29, alpha29);
									Dust dust5 = Main.dust[num36];
									dust5.velocity.X = dust5.velocity.X / 2f;
									Dust dust6 = Main.dust[num36];
									dust6.velocity.Y = dust6.velocity.Y / 2f;
								}
							}
							if (num33 == 4 && rand.Next(150) == 0)
							{
								Vector2 position34 = new Vector2(j * 16, i * 16);
								int width30 = 16;
								int height30 = 8;
								int type30 = 16;
								float speedX30 = 0f;
								float speedY30 = 0f;
								int alpha30 = 0;
								int num37 = Dust.NewDust(position34, width30, height30, type30, speedX30, speedY30, alpha30);
								Dust dust7 = Main.dust[num37];
								dust7.velocity.X = dust7.velocity.X / 3f;
								Dust dust8 = Main.dust[num37];
								dust8.velocity.Y = dust8.velocity.Y / 3f;
								Dust dust9 = Main.dust[num37];
								dust9.velocity.Y = dust9.velocity.Y - 0.7f;
								Main.dust[num37].alpha = 50;
								Main.dust[num37].scale *= 0.1f;
								Main.dust[num37].fadeIn = 0.9f;
								Main.dust[num37].noGravity = true;
							}
							if (num33 == 5)
							{
								if (rand.Next(40) == 0)
								{
									Vector2 position35 = new Vector2(j * 16, i * 16 - 6);
									int width31 = 16;
									int height31 = 16;
									int type31 = 6;
									float speedX31 = 0f;
									float speedY31 = 0f;
									int alpha31 = 0;
									int num38 = Dust.NewDust(position35, width31, height31, type31, speedX31, speedY31, alpha31, default(Color), 1.5f);
									Dust dust10 = Main.dust[num38];
									dust10.velocity.Y = dust10.velocity.Y - 2f;
									Main.dust[num38].noGravity = true;
								}
								color.A = (byte)((int)mouseTextColor / 2);
								color.G = mouseTextColor;
								color.B = mouseTextColor;
							}
						}
						SpriteBatch spriteBatch5 = this.spriteBatch;
						Texture2D texture5 = tileTexture[num32];
						Vector2 position36 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle5 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
						Color color8 = color;
						float rotation5 = 0f;
						spriteBatch5.Draw(texture5, position36, sourceRectangle5, color8, rotation5, default(Vector2), 1f, SpriteEffects.None, 0f);
						continue;
					}
					if (tile[j, i].type == 80)
					{
						bool flag7 = false;
						bool flag8 = false;
						int num39 = j;
						if (tile[j, i].frameX == 36)
						{
							num39--;
						}
						if (tile[j, i].frameX == 54)
						{
							num39++;
						}
						if (tile[j, i].frameX == 108)
						{
							num39 = ((tile[j, i].frameY != 16) ? (num39 + 1) : (num39 - 1));
						}
						int num40 = i;
						bool flag9 = false;
						if (tile[num39, num40].type == 80 && tile[num39, num40].active)
						{
							flag9 = true;
						}
						while (!tile[num39, num40].active || !tileSolid[tile[num39, num40].type] || !flag9)
						{
							if (tile[num39, num40].type == 80 && tile[num39, num40].active)
							{
								flag9 = true;
							}
							num40++;
							if (num40 > i + 20)
							{
								break;
							}
						}
						if (tile[num39, num40].type == 112)
						{
							flag7 = true;
						}
						if (tile[num39, num40].type == 116)
						{
							flag8 = true;
						}
						if (flag7)
						{
							SpriteBatch spriteBatch6 = this.spriteBatch;
							Texture2D texture6 = evilCactusTexture;
							Vector2 position37 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
							Rectangle? sourceRectangle6 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
							Color color9 = color;
							float rotation6 = 0f;
							spriteBatch6.Draw(texture6, position37, sourceRectangle6, color9, rotation6, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						else if (flag8)
						{
							SpriteBatch spriteBatch7 = this.spriteBatch;
							Texture2D texture7 = goodCactusTexture;
							Vector2 position38 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
							Rectangle? sourceRectangle7 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
							Color color10 = color;
							float rotation7 = 0f;
							spriteBatch7.Draw(texture7, position38, sourceRectangle7, color10, rotation7, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						else
						{
							SpriteBatch spriteBatch8 = this.spriteBatch;
							Texture2D texture8 = tileTexture[tile[j, i].type];
							Vector2 position39 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
							Rectangle? sourceRectangle8 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
							Color color11 = color;
							float rotation8 = 0f;
							spriteBatch8.Draw(texture8, position39, sourceRectangle8, color11, rotation8, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						continue;
					}
					if (Lighting.lightMode < 2 && tileSolid[tile[j, i].type] && tile[j, i].type != 137)
					{
						if (color.R > num || (double)(int)color.G > (double)num * 1.1 || (double)(int)color.B > (double)num * 1.2)
						{
							for (int k = 0; k < 9; k++)
							{
								int num41 = 0;
								int num42 = 0;
								int width32 = 4;
								int height32 = 4;
								Color color12 = color;
								Color color13 = color;
								if (k == 0)
								{
									color13 = Lighting.GetColor(j - 1, i - 1);
								}
								if (k == 1)
								{
									width32 = 8;
									num41 = 4;
									color13 = Lighting.GetColor(j, i - 1);
								}
								if (k == 2)
								{
									color13 = Lighting.GetColor(j + 1, i - 1);
									num41 = 12;
								}
								if (k == 3)
								{
									color13 = Lighting.GetColor(j - 1, i);
									height32 = 8;
									num42 = 4;
								}
								if (k == 4)
								{
									width32 = 8;
									height32 = 8;
									num41 = 4;
									num42 = 4;
								}
								if (k == 5)
								{
									num41 = 12;
									num42 = 4;
									height32 = 8;
									color13 = Lighting.GetColor(j + 1, i);
								}
								if (k == 6)
								{
									color13 = Lighting.GetColor(j - 1, i + 1);
									num42 = 12;
								}
								if (k == 7)
								{
									width32 = 8;
									height32 = 4;
									num41 = 4;
									num42 = 12;
									color13 = Lighting.GetColor(j, i + 1);
								}
								if (k == 8)
								{
									color13 = Lighting.GetColor(j + 1, i + 1);
									num41 = 12;
									num42 = 12;
								}
								color12.R = (byte)((color.R + color13.R) / 2);
								color12.G = (byte)((color.G + color13.G) / 2);
								color12.B = (byte)((color.B + color13.B) / 2);
								if (tileShine2[tile[j, i].type])
								{
									color12 = shine(color12, tile[j, i].type);
								}
								SpriteBatch spriteBatch9 = this.spriteBatch;
								Texture2D texture9 = tileTexture[tile[j, i].type];
								Vector2 position40 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f + (float)num41, i * 16 - (int)screenPosition.Y + num10 + num42) + value;
								Rectangle? sourceRectangle9 = new Rectangle(tile[j, i].frameX + num41, tile[j, i].frameY + num42, width32, height32);
								Color color14 = color12;
								float rotation9 = 0f;
								spriteBatch9.Draw(texture9, position40, sourceRectangle9, color14, rotation9, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else if (color.R > num2 || (double)(int)color.G > (double)num2 * 1.1 || (double)(int)color.B > (double)num2 * 1.2)
						{
							for (int l = 0; l < 4; l++)
							{
								int num43 = 0;
								int num44 = 0;
								Color color15 = color;
								Color color16 = color;
								if (l == 0)
								{
									color16 = ((!Lighting.Brighter(j, i - 1, j - 1, i)) ? Lighting.GetColor(j, i - 1) : Lighting.GetColor(j - 1, i));
								}
								if (l == 1)
								{
									color16 = ((!Lighting.Brighter(j, i - 1, j + 1, i)) ? Lighting.GetColor(j, i - 1) : Lighting.GetColor(j + 1, i));
									num43 = 8;
								}
								if (l == 2)
								{
									color16 = ((!Lighting.Brighter(j, i + 1, j - 1, i)) ? Lighting.GetColor(j, i + 1) : Lighting.GetColor(j - 1, i));
									num44 = 8;
								}
								if (l == 3)
								{
									color16 = ((!Lighting.Brighter(j, i + 1, j + 1, i)) ? Lighting.GetColor(j, i + 1) : Lighting.GetColor(j + 1, i));
									num43 = 8;
									num44 = 8;
								}
								color15.R = (byte)((color.R + color16.R) / 2);
								color15.G = (byte)((color.G + color16.G) / 2);
								color15.B = (byte)((color.B + color16.B) / 2);
								if (tileShine2[tile[j, i].type])
								{
									color15 = shine(color15, tile[j, i].type);
								}
								SpriteBatch spriteBatch10 = this.spriteBatch;
								Texture2D texture10 = tileTexture[tile[j, i].type];
								Vector2 position41 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f + (float)num43, i * 16 - (int)screenPosition.Y + num10 + num44) + value;
								Rectangle? sourceRectangle10 = new Rectangle(tile[j, i].frameX + num43, tile[j, i].frameY + num44, 8, 8);
								Color color17 = color15;
								float rotation10 = 0f;
								spriteBatch10.Draw(texture10, position41, sourceRectangle10, color17, rotation10, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							if (tileShine2[tile[j, i].type])
							{
								color = shine(color, tile[j, i].type);
							}
							SpriteBatch spriteBatch11 = this.spriteBatch;
							Texture2D texture11 = tileTexture[tile[j, i].type];
							Vector2 position42 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
							Rectangle? sourceRectangle11 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
							Color color18 = color;
							float rotation11 = 0f;
							spriteBatch11.Draw(texture11, position42, sourceRectangle11, color18, rotation11, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						continue;
					}
					if (Lighting.lightMode < 2 && tileShine2[tile[j, i].type])
					{
						if (tile[j, i].type == 21)
						{
							if (tile[j, i].frameX >= 36 && tile[j, i].frameX < 178)
							{
								color = shine(color, tile[j, i].type);
							}
						}
						else
						{
							color = shine(color, tile[j, i].type);
						}
					}
					if (tile[j, i].type == 128)
					{
						int num45;
						for (num45 = tile[j, i].frameX; num45 >= 100; num45 -= 100)
						{
						}
						SpriteBatch spriteBatch12 = this.spriteBatch;
						Texture2D texture12 = tileTexture[tile[j, i].type];
						Vector2 position43 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle12 = new Rectangle(num45, tile[j, i].frameY, num9, num8);
						Color color19 = color;
						float rotation12 = 0f;
						spriteBatch12.Draw(texture12, position43, sourceRectangle12, color19, rotation12, default(Vector2), 1f, SpriteEffects.None, 0f);
						continue;
					}
					SpriteBatch spriteBatch13 = this.spriteBatch;
					Texture2D texture13 = tileTexture[tile[j, i].type];
					Vector2 position44 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
					Rectangle? sourceRectangle13 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
					Color color20 = color;
					float rotation13 = 0f;
					spriteBatch13.Draw(texture13, position44, sourceRectangle13, color20, rotation13, default(Vector2), 1f, SpriteEffects.None, 0f);
					if (tile[j, i].type == 139)
					{
						SpriteBatch spriteBatch14 = this.spriteBatch;
						Texture2D musicBoxTexture = MusicBoxTexture;
						Vector2 position45 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle14 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
						Color color21 = new Color(200, 200, 200, 0);
						float rotation14 = 0f;
						spriteBatch14.Draw(musicBoxTexture, position45, sourceRectangle14, color21, rotation14, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					if (tile[j, i].type == 144)
					{
						SpriteBatch spriteBatch15 = this.spriteBatch;
						Texture2D texture14 = timerTexture;
						Vector2 position46 = new Vector2((float)(j * 16 - (int)screenPosition.X) - ((float)num9 - 16f) / 2f, i * 16 - (int)screenPosition.Y + num10) + value;
						Rectangle? sourceRectangle15 = new Rectangle(tile[j, i].frameX, tile[j, i].frameY, num9, num8);
						Color color22 = new Color(200, 200, 200, 0);
						float rotation15 = 0f;
						spriteBatch15.Draw(texture14, position46, sourceRectangle15, color22, rotation15, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			for (int m = 0; m < num3; m++)
			{
				int num46 = array[m];
				int num47 = array2[m];
				int num48 = tile[num46, num47].frameY / 18;
				if (tile[num46, num47].type == 128)
				{
					if (num48 == 1 && tile[num46, num47 + 1].frameX >= 100)
					{
						num48++;
						num47++;
					}
					else if (num48 == 2 && tile[num46, num47 - 1].frameX >= 100)
					{
						num48--;
						num47--;
					}
				}
				if (tile[num46, num47].type == 128 && tile[num46, num47].frameX >= 100)
				{
					int num49 = tile[num46, num47].frameX;
					int num50 = 0;
					while (num49 >= 100)
					{
						num50++;
						num49 -= 100;
					}
					int num51 = -4;
					SpriteEffects effects = SpriteEffects.FlipHorizontally;
					if (num49 >= 36)
					{
						effects = SpriteEffects.None;
						num51 = -4;
					}
					if (num48 == 0 && armorHeadTexture[num50] != null)
					{
						SpriteBatch spriteBatch16 = this.spriteBatch;
						Texture2D texture15 = armorHeadTexture[num50];
						Vector2 position47 = new Vector2(num46 * 16 - (int)screenPosition.X + num51, num47 * 16 - (int)screenPosition.Y - 12) + value;
						Rectangle? sourceRectangle16 = new Rectangle(0, 0, 40, 36);
						Color color23 = Lighting.GetColor(num46, num47);
						float rotation16 = 0f;
						spriteBatch16.Draw(texture15, position47, sourceRectangle16, color23, rotation16, default(Vector2), 1f, effects, 0f);
					}
					else if (num48 == 1 && armorBodyTexture[num50] != null)
					{
						SpriteBatch spriteBatch17 = this.spriteBatch;
						Texture2D texture16 = armorBodyTexture[num50];
						Vector2 position48 = new Vector2(num46 * 16 - (int)screenPosition.X + num51, num47 * 16 - (int)screenPosition.Y - 28) + value;
						Rectangle? sourceRectangle17 = new Rectangle(0, 0, 40, 54);
						Color color24 = Lighting.GetColor(num46, num47);
						float rotation17 = 0f;
						spriteBatch17.Draw(texture16, position48, sourceRectangle17, color24, rotation17, default(Vector2), 1f, effects, 0f);
					}
					else if (num48 == 2 && armorLegTexture[num50] != null)
					{
						SpriteBatch spriteBatch18 = this.spriteBatch;
						Texture2D texture17 = armorLegTexture[num50];
						Vector2 position49 = new Vector2(num46 * 16 - (int)screenPosition.X + num51, num47 * 16 - (int)screenPosition.Y - 44) + value;
						Rectangle? sourceRectangle18 = new Rectangle(0, 0, 40, 54);
						Color color25 = Lighting.GetColor(num46, num47);
						float rotation18 = 0f;
						spriteBatch18.Draw(texture17, position49, sourceRectangle18, color25, rotation18, default(Vector2), 1f, effects, 0f);
					}
				}
				try
				{
					if (tile[num46, num47].type == 5 && tile[num46, num47].frameY >= 198 && tile[num46, num47].frameX >= 22)
					{
						int num52 = 0;
						if (tile[num46, num47].frameX == 22)
						{
							if (tile[num46, num47].frameY == 220)
							{
								num52 = 1;
							}
							else if (tile[num46, num47].frameY == 242)
							{
								num52 = 2;
							}
							int num53 = 0;
							int num54 = 80;
							int num55 = 80;
							int num56 = 32;
							int num57 = 0;
							for (int n = num47; n < num47 + 100; n++)
							{
								if (tile[num46, n].type == 2)
								{
									num53 = 0;
									break;
								}
								if (tile[num46, n].type == 23)
								{
									num53 = 1;
									break;
								}
								if (tile[num46, n].type == 60)
								{
									num53 = 2;
									num54 = 114;
									num55 = 96;
									num56 = 48;
									break;
								}
								if (tile[num46, n].type == 147)
								{
									num53 = 4;
									break;
								}
								if (tile[num46, n].type == 109)
								{
									num53 = 3;
									num55 = 140;
									if (num46 % 3 == 1)
									{
										num52 += 3;
									}
									else if (num46 % 3 == 2)
									{
										num52 += 6;
									}
									break;
								}
							}
							SpriteBatch spriteBatch19 = this.spriteBatch;
							Texture2D texture18 = treeTopTexture[num53];
							Vector2 position50 = new Vector2(num46 * 16 - (int)screenPosition.X - num56, num47 * 16 - (int)screenPosition.Y - num55 + 16 + num57) + value;
							Rectangle? sourceRectangle19 = new Rectangle(num52 * (num54 + 2), 0, num54, num55);
							Color color26 = Lighting.GetColor(num46, num47);
							float rotation19 = 0f;
							spriteBatch19.Draw(texture18, position50, sourceRectangle19, color26, rotation19, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						else if (tile[num46, num47].frameX == 44)
						{
							if (tile[num46, num47].frameY == 220)
							{
								num52 = 1;
							}
							else if (tile[num46, num47].frameY == 242)
							{
								num52 = 2;
							}
							int num58 = 0;
							for (int num59 = num47; num59 < num47 + 100; num59++)
							{
								if (tile[num46 + 1, num59].type == 2)
								{
									num58 = 0;
									break;
								}
								if (tile[num46 + 1, num59].type == 23)
								{
									num58 = 1;
									break;
								}
								if (tile[num46 + 1, num59].type == 60)
								{
									num58 = 2;
									break;
								}
								if (tile[num46 + 1, num59].type == 147)
								{
									num58 = 4;
									break;
								}
								if (tile[num46 + 1, num59].type == 109)
								{
									num58 = 3;
									if (num46 % 3 == 1)
									{
										num52 += 3;
									}
									else if (num46 % 3 == 2)
									{
										num52 += 6;
									}
									break;
								}
							}
							SpriteBatch spriteBatch20 = this.spriteBatch;
							Texture2D texture19 = treeBranchTexture[num58];
							Vector2 position51 = new Vector2(num46 * 16 - (int)screenPosition.X - 24, num47 * 16 - (int)screenPosition.Y - 12) + value;
							Rectangle? sourceRectangle20 = new Rectangle(0, num52 * 42, 40, 40);
							Color color27 = Lighting.GetColor(num46, num47);
							float rotation20 = 0f;
							spriteBatch20.Draw(texture19, position51, sourceRectangle20, color27, rotation20, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						else if (tile[num46, num47].frameX == 66)
						{
							if (tile[num46, num47].frameY == 220)
							{
								num52 = 1;
							}
							else if (tile[num46, num47].frameY == 242)
							{
								num52 = 2;
							}
							int num60 = 0;
							for (int num61 = num47; num61 < num47 + 100; num61++)
							{
								if (tile[num46 - 1, num61].type == 2)
								{
									num60 = 0;
									break;
								}
								if (tile[num46 - 1, num61].type == 23)
								{
									num60 = 1;
									break;
								}
								if (tile[num46 - 1, num61].type == 60)
								{
									num60 = 2;
									break;
								}
								if (tile[num46 - 1, num61].type == 147)
								{
									num60 = 4;
									break;
								}
								if (tile[num46 - 1, num61].type == 109)
								{
									num60 = 3;
									if (num46 % 3 == 1)
									{
										num52 += 3;
									}
									else if (num46 % 3 == 2)
									{
										num52 += 6;
									}
									break;
								}
							}
							SpriteBatch spriteBatch21 = this.spriteBatch;
							Texture2D texture20 = treeBranchTexture[num60];
							Vector2 position52 = new Vector2(num46 * 16 - (int)screenPosition.X, num47 * 16 - (int)screenPosition.Y - 12) + value;
							Rectangle? sourceRectangle21 = new Rectangle(42, num52 * 42, 40, 40);
							Color color28 = Lighting.GetColor(num46, num47);
							float rotation21 = 0f;
							spriteBatch21.Draw(texture20, position52, sourceRectangle21, color28, rotation21, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
				}
				catch
				{
				}
			}
			TMod.RunMethod(TMod.WorldHooks.PostDrawTiles, this.spriteBatch, solidOnly);
			if (solidOnly)
			{
				renderTimer[0] = stopwatch.ElapsedMilliseconds;
			}
			else
			{
				renderTimer[1] = stopwatch.ElapsedMilliseconds;
			}
		}

		public void DrawWater(bool bg = false)
		{
			TMod.RunMethod(TMod.WorldHooks.PreDrawWater, spriteBatch, bg);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			int num = (int)(255f * (1f - gfxQuality) + 40f * gfxQuality);
			int num2 = (int)(255f * (1f - gfxQuality) + 140f * gfxQuality);
			float num3 = (float)evilTiles / 350f;
			if (num3 > 1f)
			{
				num3 = 1f;
			}
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			float num4 = (255f - 100f * num3) / 255f;
			float num5 = (255f - 50f * num3) / 255f;
			int num6 = (int)((screenPosition.X - value.X) / 16f - 1f);
			int num7 = (int)((screenPosition.X + (float)screenWidth + value.X) / 16f) + 2;
			int num8 = (int)((screenPosition.Y - value.Y) / 16f - 1f);
			int num9 = (int)((screenPosition.Y + (float)screenHeight + value.Y) / 16f) + 5;
			if (num6 < 5)
			{
				num6 = 5;
			}
			if (num7 > maxTilesX - 5)
			{
				num7 = maxTilesX - 5;
			}
			if (num8 < 5)
			{
				num8 = 5;
			}
			if (num9 > maxTilesY - 5)
			{
				num9 = maxTilesY - 5;
			}
			for (int i = num8; i < num9 + 4; i++)
			{
				for (int j = num6 - 2; j < num7 + 2; j++)
				{
					if (tile[j, i] == null)
					{
						tile[j, i] = new Tile();
					}
					if (tile[j, i].liquid <= 0 || (tile[j, i].active && tileSolid[tile[j, i].type] && !tileSolidTop[tile[j, i].type]) || (!(Lighting.Brightness(j, i) > 0f) && !bg))
					{
						continue;
					}
					Color color = Lighting.GetColor(j, i);
					float num10 = 256 - tile[j, i].liquid;
					num10 /= 32f;
					int num11 = 0;
					if (tile[j, i].lava)
					{
						num11 = 1;
					}
					float num12 = 0.5f;
					if (bg)
					{
						num12 = 1f;
					}
					Vector2 value2 = new Vector2(j * 16, i * 16 + (int)num10 * 2);
					Rectangle value3 = new Rectangle(0, 0, 16, 16 - (int)num10 * 2);
					if (tile[j, i + 1].liquid < 245 && (!tile[j, i + 1].active || !tileSolid[tile[j, i + 1].type] || tileSolidTop[tile[j, i + 1].type]))
					{
						float num13 = 256 - tile[j, i + 1].liquid;
						num13 /= 32f;
						num12 = 0.5f * (8f - num10) / 4f;
						if ((double)num12 > 0.55)
						{
							num12 = 0.55f;
						}
						if ((double)num12 < 0.35)
						{
							num12 = 0.35f;
						}
						float num14 = num10 / 2f;
						if (tile[j, i + 1].liquid < 200)
						{
							if (bg)
							{
								continue;
							}
							if (tile[j, i - 1].liquid > 0 && tile[j, i - 1].liquid > 0)
							{
								value3 = new Rectangle(0, 4, 16, 16);
								num12 = 0.5f;
							}
							else if (tile[j, i - 1].liquid > 0)
							{
								value2 = new Vector2(j * 16, i * 16 + 4);
								value3 = new Rectangle(0, 4, 16, 12);
								num12 = 0.5f;
							}
							else if (tile[j, i + 1].liquid <= 0)
							{
								value2 = new Vector2(j * 16 + (int)num14, i * 16 + (int)num14 * 2 + (int)num13 * 2);
								value3 = new Rectangle(0, 4, 16 - (int)num14 * 2, 16 - (int)num14 * 2);
							}
							else
							{
								value2 = new Vector2(j * 16, i * 16 + (int)num10 * 2 + (int)num13 * 2);
								value3 = new Rectangle(0, 4, 16, 16 - (int)num10 * 2);
							}
						}
						else
						{
							num12 = 0.5f;
							value3 = new Rectangle(0, 4, 16, 16 - (int)num10 * 2 + (int)num13 * 2);
						}
					}
					else if (tile[j, i - 1].liquid > 32)
					{
						value3 = new Rectangle(0, 4, value3.Width, value3.Height);
					}
					else if (num10 < 1f && tile[j, i - 1].active && tileSolid[tile[j, i - 1].type] && !tileSolidTop[tile[j, i - 1].type])
					{
						value2 = new Vector2(j * 16, i * 16);
						value3 = new Rectangle(0, 4, 16, 16);
					}
					else
					{
						bool flag = true;
						for (int k = i + 1; k < i + 6 && (!tile[j, k].active || !tileSolid[tile[j, k].type] || tileSolidTop[tile[j, k].type]); k++)
						{
							if (tile[j, k].liquid < 200)
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							num12 = 0.5f;
							value3 = new Rectangle(0, 4, 16, 16);
						}
						else if (tile[j, i - 1].liquid > 0)
						{
							value3 = new Rectangle(0, 2, value3.Width, value3.Height);
						}
					}
					if (tile[j, i].lava)
					{
						num12 *= 1.8f;
						if (num12 > 1f)
						{
							num12 = 1f;
						}
						if (base.IsActive && !gamePaused && Dust.lavaBubbles < 200)
						{
							if (tile[j, i].liquid > 200 && rand.Next(700) == 0)
							{
								Dust.NewDust(new Vector2(j * 16, i * 16), 16, 16, 35);
							}
							if (value3.Y == 0 && rand.Next(350) == 0)
							{
								int num15 = Dust.NewDust(new Vector2(j * 16, (float)(i * 16) + num10 * 2f - 8f), 16, 8, 35, 0f, 0f, 50, default(Color), 1.5f);
								Dust dust = Main.dust[num15];
								dust.velocity *= 0.8f;
								Dust dust2 = Main.dust[num15];
								dust2.velocity.X = dust2.velocity.X * 2f;
								Dust dust3 = Main.dust[num15];
								dust3.velocity.Y = dust3.velocity.Y - (float)rand.Next(1, 7) * 0.1f;
								if (rand.Next(10) == 0)
								{
									Dust dust4 = Main.dust[num15];
									dust4.velocity.Y = dust4.velocity.Y * (float)rand.Next(2, 5);
								}
								Main.dust[num15].noGravity = true;
							}
						}
					}
					float num16 = (float)(int)color.R * num12;
					float num17 = (float)(int)color.G * num12;
					float num18 = (float)(int)color.B * num12;
					float num19 = (float)(int)color.A * num12;
					if (num11 == 0)
					{
						num18 *= num4;
					}
					else
					{
						num16 *= num5;
					}
					color = new Color((byte)num16, (byte)num17, (byte)num18, (byte)num19);
					if (Lighting.lightMode < 2 && !bg)
					{
						Color color2 = color;
						if ((num11 == 0 && (color2.R > num || (double)(int)color2.G > (double)num * 1.1 || (double)(int)color2.B > (double)num * 1.2)) || color2.R > num2 || (double)(int)color2.G > (double)num2 * 1.1 || (double)(int)color2.B > (double)num2 * 1.2)
						{
							for (int l = 0; l < 4; l++)
							{
								int num20 = 0;
								int num21 = 0;
								int width = 8;
								int height = 8;
								Color color3 = color2;
								Color color4 = Lighting.GetColor(j, i);
								if (l == 0)
								{
									if (Lighting.Brighter(j, i - 1, j - 1, i))
									{
										if (!tile[j - 1, i].active)
										{
											color4 = Lighting.GetColor(j - 1, i);
										}
										else if (!tile[j, i - 1].active)
										{
											color4 = Lighting.GetColor(j, i - 1);
										}
									}
									if (value3.Height < 8)
									{
										height = value3.Height;
									}
								}
								if (l == 1)
								{
									if (Lighting.Brighter(j, i - 1, j + 1, i))
									{
										if (!tile[j + 1, i].active)
										{
											color4 = Lighting.GetColor(j + 1, i);
										}
										else if (!tile[j, i - 1].active)
										{
											color4 = Lighting.GetColor(j, i - 1);
										}
									}
									num20 = 8;
									if (value3.Height < 8)
									{
										height = value3.Height;
									}
								}
								if (l == 2)
								{
									if (Lighting.Brighter(j, i + 1, j - 1, i))
									{
										if (!tile[j - 1, i].active)
										{
											color4 = Lighting.GetColor(j - 1, i);
										}
										else if (!tile[j, i + 1].active)
										{
											color4 = Lighting.GetColor(j, i + 1);
										}
									}
									num21 = 8;
									height = 8 - (16 - value3.Height);
								}
								if (l == 3)
								{
									if (Lighting.Brighter(j, i + 1, j + 1, i))
									{
										if (!tile[j + 1, i].active)
										{
											color4 = Lighting.GetColor(j + 1, i);
										}
										else if (!tile[j, i + 1].active)
										{
											color4 = Lighting.GetColor(j, i + 1);
										}
									}
									num20 = 8;
									num21 = 8;
									height = 8 - (16 - value3.Height);
								}
								num16 = (float)(int)color4.R * num12;
								num17 = (float)(int)color4.G * num12;
								num18 = (float)(int)color4.B * num12;
								num19 = (float)(int)color4.A * num12;
								color4 = new Color((byte)num16, (byte)num17, (byte)num18, (byte)num19);
								color3.R = (byte)((color2.R + color4.R) / 2);
								color3.G = (byte)((color2.G + color4.G) / 2);
								color3.B = (byte)((color2.B + color4.B) / 2);
								color3.A = (byte)((color2.A + color4.A) / 2);
								spriteBatch.Draw(liquidTexture[num11], value2 - screenPosition + new Vector2(num20, num21) + value, new Rectangle(value3.X + num20, value3.Y + num21, width, height), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							spriteBatch.Draw(liquidTexture[num11], value2 - screenPosition + value, value3, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
					else
					{
						spriteBatch.Draw(liquidTexture[num11], value2 - screenPosition + value, value3, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (!bg)
			{
				renderTimer[4] = stopwatch.ElapsedMilliseconds;
			}
			TMod.RunMethod(TMod.WorldHooks.PostDrawWater, spriteBatch, bg);
		}

		public void DrawGore()
		{
			for (int i = 0; i < 200; i++)
			{
				if (gore[i].active && gore[i].type > 0)
				{
					Color alpha = gore[i].GetAlpha(Lighting.GetColor((int)((double)gore[i].position.X + (double)goreTexture[gore[i].type].Width * 0.5) / 16, (int)(((double)gore[i].position.Y + (double)goreTexture[gore[i].type].Height * 0.5) / 16.0)));
					spriteBatch.Draw(goreTexture[gore[i].type], new Vector2(gore[i].position.X - screenPosition.X + (float)(goreTexture[gore[i].type].Width / 2), gore[i].position.Y - screenPosition.Y + (float)(goreTexture[gore[i].type].Height / 2)), new Rectangle(0, 0, goreTexture[gore[i].type].Width, goreTexture[gore[i].type].Height), alpha, gore[i].rotation, new Vector2(goreTexture[gore[i].type].Width / 2, goreTexture[gore[i].type].Height / 2), gore[i].scale, SpriteEffects.None, 0f);
					Color color = gore[i].GetColor(Lighting.GetColor((int)((double)gore[i].position.X + (double)goreTexture[gore[i].type].Width * 0.5) / 16, (int)(((double)gore[i].position.Y + (double)goreTexture[gore[i].type].Height * 0.5) / 16.0)));
					if (color != default(Color))
					{
						spriteBatch.Draw(goreTexture[gore[i].type], new Vector2(gore[i].position.X - screenPosition.X + (float)(goreTexture[gore[i].type].Width / 2), gore[i].position.Y - screenPosition.Y + (float)(goreTexture[gore[i].type].Height / 2)), new Rectangle(0, 0, goreTexture[gore[i].type].Width, goreTexture[gore[i].type].Height), color, gore[i].rotation, new Vector2(goreTexture[gore[i].type].Width / 2, goreTexture[gore[i].type].Height / 2), gore[i].scale, SpriteEffects.None, 0f);
					}
				}
			}
		}

		public void DrawNPCs(bool behindTiles = false)
		{
			bool flag = false;
			new Rectangle((int)screenPosition.X - 300, (int)screenPosition.Y - 300, screenWidth + 600, screenHeight + 600);
			for (int num = 199; num >= 0; num--)
			{
				if (npc[num].active && npc[num].type > 0 && npc[num].behindTiles == behindTiles)
				{
					if (!npc[num].PreDraw(spriteBatch))
					{
						npc[num].PostDraw(spriteBatch);
					}
					else
					{
						if ((npc[num].type == 125 || npc[num].type == 126) && !flag)
						{
							flag = true;
							for (int i = 0; i < 200; i++)
							{
								if (!npc[i].active || num == i || (npc[i].type != 125 && npc[i].type != 126))
								{
									continue;
								}
								float num2 = npc[i].position.X + (float)npc[i].width * 0.5f;
								float num3 = npc[i].position.Y + (float)npc[i].height * 0.5f;
								Vector2 vector = new Vector2(npc[num].position.X + (float)npc[num].width * 0.5f, npc[num].position.Y + (float)npc[num].height * 0.5f);
								float num4 = num2 - vector.X;
								float num5 = num3 - vector.Y;
								float rotation = (float)Math.Atan2(num5, num4) - 1.57f;
								bool flag2 = true;
								float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
								if (num6 > 2000f)
								{
									flag2 = false;
								}
								while (flag2)
								{
									num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
									if (num6 < 40f)
									{
										flag2 = false;
										continue;
									}
									num6 = (float)chain12Texture.Height / num6;
									num4 *= num6;
									num5 *= num6;
									vector.X += num4;
									vector.Y += num5;
									num4 = num2 - vector.X;
									num5 = num3 - vector.Y;
									Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
									spriteBatch.Draw(chain12Texture, new Vector2(vector.X - screenPosition.X, vector.Y - screenPosition.Y), new Rectangle(0, 0, chain12Texture.Width, chain12Texture.Height), color, rotation, new Vector2((float)chain12Texture.Width * 0.5f, (float)chain12Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
								}
							}
						}
						DrawNPCReal(num, behindTiles);
						npc[num].PostDraw(spriteBatch);
					}
				}
			}
		}

		public void DrawNPCReal(int i, bool behindTiles)
		{
			if (!new Rectangle((int)screenPosition.X - 300, (int)screenPosition.Y - 300, screenWidth + 600, screenHeight + 600).Intersects(new Rectangle((int)npc[i].position.X, (int)npc[i].position.Y, npc[i].width, npc[i].height)))
			{
				return;
			}
			if (npc[i].type == 101)
			{
				bool flag = true;
				Vector2 vector = new Vector2(npc[i].position.X + (float)(npc[i].width / 2), npc[i].position.Y + (float)(npc[i].height / 2));
				float num = npc[i].ai[0] * 16f + 8f - vector.X;
				float num2 = npc[i].ai[1] * 16f + 8f - vector.Y;
				float rotation = (float)Math.Atan2(num2, num) - 1.57f;
				bool flag2 = true;
				while (flag2)
				{
					float num3 = 0.75f;
					int height = 28;
					float num4 = (float)Math.Sqrt(num * num + num2 * num2);
					if (num4 < 28f * num3)
					{
						height = (int)num4 - 40 + 28;
						flag2 = false;
					}
					num4 = 20f * num3 / num4;
					num *= num4;
					num2 *= num4;
					vector.X += num;
					vector.Y += num2;
					num = npc[i].ai[0] * 16f + 8f - vector.X;
					num2 = npc[i].ai[1] * 16f + 8f - vector.Y;
					Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
					if (!flag)
					{
						flag = true;
						spriteBatch.Draw(chain10Texture, new Vector2(vector.X - screenPosition.X, vector.Y - screenPosition.Y), new Rectangle(0, 0, chain10Texture.Width, height), color, rotation, new Vector2((float)chain10Texture.Width * 0.5f, (float)chain10Texture.Height * 0.5f), num3, SpriteEffects.None, 0f);
					}
					else
					{
						flag = false;
						spriteBatch.Draw(chain11Texture, new Vector2(vector.X - screenPosition.X, vector.Y - screenPosition.Y), new Rectangle(0, 0, chain10Texture.Width, height), color, rotation, new Vector2((float)chain10Texture.Width * 0.5f, (float)chain10Texture.Height * 0.5f), num3, SpriteEffects.None, 0f);
					}
				}
			}
			else if (npc[i].aiStyle == 13)
			{
				Vector2 vector2 = new Vector2(npc[i].position.X + (float)(npc[i].width / 2), npc[i].position.Y + (float)(npc[i].height / 2));
				float num5 = npc[i].ai[0] * 16f + 8f - vector2.X;
				float num6 = npc[i].ai[1] * 16f + 8f - vector2.Y;
				float rotation2 = (float)Math.Atan2(num6, num5) - 1.57f;
				bool flag3 = true;
				while (flag3)
				{
					int height2 = 28;
					float num7 = (float)Math.Sqrt(num5 * num5 + num6 * num6);
					if (num7 < 40f)
					{
						height2 = (int)num7 - 40 + 28;
						flag3 = false;
					}
					num7 = 28f / num7;
					num5 *= num7;
					num6 *= num7;
					vector2.X += num5;
					vector2.Y += num6;
					num5 = npc[i].ai[0] * 16f + 8f - vector2.X;
					num6 = npc[i].ai[1] * 16f + 8f - vector2.Y;
					Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
					if (npc[i].type == 56)
					{
						spriteBatch.Draw(chain5Texture, new Vector2(vector2.X - screenPosition.X, vector2.Y - screenPosition.Y), new Rectangle(0, 0, chain4Texture.Width, height2), color2, rotation2, new Vector2((float)chain4Texture.Width * 0.5f, (float)chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
					else
					{
						spriteBatch.Draw(chain4Texture, new Vector2(vector2.X - screenPosition.X, vector2.Y - screenPosition.Y), new Rectangle(0, 0, chain4Texture.Width, height2), color2, rotation2, new Vector2((float)chain4Texture.Width * 0.5f, (float)chain4Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (npc[i].type == 36)
			{
				Vector2 vector3 = new Vector2(npc[i].position.X + (float)npc[i].width * 0.5f - 5f * npc[i].ai[0], npc[i].position.Y + 20f);
				for (int j = 0; j < 2; j++)
				{
					float num8 = npc[(int)npc[i].ai[1]].position.X + (float)(npc[(int)npc[i].ai[1]].width / 2) - vector3.X;
					float num9 = npc[(int)npc[i].ai[1]].position.Y + (float)(npc[(int)npc[i].ai[1]].height / 2) - vector3.Y;
					float num10 = 0f;
					if (j == 0)
					{
						num8 -= 200f * npc[i].ai[0];
						num9 += 130f;
						num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
						num10 = 92f / num10;
						vector3.X += num8 * num10;
						vector3.Y += num9 * num10;
					}
					else
					{
						num8 -= 50f * npc[i].ai[0];
						num9 += 80f;
						num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
						num10 = 60f / num10;
						vector3.X += num8 * num10;
						vector3.Y += num9 * num10;
					}
					float rotation3 = (float)Math.Atan2(num9, num8) - 1.57f;
					Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
					spriteBatch.Draw(boneArmTexture, new Vector2(vector3.X - screenPosition.X, vector3.Y - screenPosition.Y), new Rectangle(0, 0, boneArmTexture.Width, boneArmTexture.Height), color3, rotation3, new Vector2((float)boneArmTexture.Width * 0.5f, (float)boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					if (j == 0)
					{
						vector3.X += num8 * num10 / 2f;
						vector3.Y += num9 * num10 / 2f;
					}
					else if (base.IsActive)
					{
						vector3.X += num8 * num10 - 16f;
						vector3.Y += num9 * num10 - 6f;
						Vector2 position = new Vector2(vector3.X, vector3.Y);
						int width = 30;
						int height3 = 10;
						int type = 5;
						float speedX = num8 * 0.02f;
						float speedY = num9 * 0.02f;
						int alpha = 0;
						int num11 = Dust.NewDust(position, width, height3, type, speedX, speedY, alpha, default(Color), 2f);
						Main.dust[num11].noGravity = true;
					}
				}
			}
			if (npc[i].aiStyle >= 33 && npc[i].aiStyle <= 36)
			{
				Vector2 vector4 = new Vector2(npc[i].position.X + (float)npc[i].width * 0.5f - 5f * npc[i].ai[0], npc[i].position.Y + 20f);
				for (int k = 0; k < 2; k++)
				{
					float num12 = npc[(int)npc[i].ai[1]].position.X + (float)(npc[(int)npc[i].ai[1]].width / 2) - vector4.X;
					float num13 = npc[(int)npc[i].ai[1]].position.Y + (float)(npc[(int)npc[i].ai[1]].height / 2) - vector4.Y;
					float num14 = 0f;
					if (k == 0)
					{
						num12 -= 200f * npc[i].ai[0];
						num13 += 130f;
						num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
						num14 = 92f / num14;
						vector4.X += num12 * num14;
						vector4.Y += num13 * num14;
					}
					else
					{
						num12 -= 50f * npc[i].ai[0];
						num13 += 80f;
						num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
						num14 = 60f / num14;
						vector4.X += num12 * num14;
						vector4.Y += num13 * num14;
					}
					float rotation4 = (float)Math.Atan2(num13, num12) - 1.57f;
					Color color4 = Lighting.GetColor((int)vector4.X / 16, (int)(vector4.Y / 16f));
					spriteBatch.Draw(boneArm2Texture, new Vector2(vector4.X - screenPosition.X, vector4.Y - screenPosition.Y), new Rectangle(0, 0, boneArmTexture.Width, boneArmTexture.Height), color4, rotation4, new Vector2((float)boneArmTexture.Width * 0.5f, (float)boneArmTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					if (k == 0)
					{
						vector4.X += num12 * num14 / 2f;
						vector4.Y += num13 * num14 / 2f;
					}
					else if (base.IsActive)
					{
						vector4.X += num12 * num14 - 16f;
						vector4.Y += num13 * num14 - 6f;
						Vector2 position2 = new Vector2(vector4.X, vector4.Y);
						int width2 = 30;
						int height4 = 10;
						int type2 = 6;
						float speedX2 = num12 * 0.02f;
						float speedY2 = num13 * 0.02f;
						int alpha2 = 0;
						int num15 = Dust.NewDust(position2, width2, height4, type2, speedX2, speedY2, alpha2, default(Color), 2.5f);
						Main.dust[num15].noGravity = true;
					}
				}
			}
			if (npc[i].aiStyle == 20)
			{
				Vector2 vector5 = new Vector2(npc[i].position.X + (float)(npc[i].width / 2), npc[i].position.Y + (float)(npc[i].height / 2));
				float num16 = npc[i].ai[1] - vector5.X;
				float num17 = npc[i].ai[2] - vector5.Y;
				float num18 = (float)Math.Atan2(num17, num16) - 1.57f;
				npc[i].rotation = num18;
				bool flag4 = true;
				while (flag4)
				{
					int height5 = 12;
					float num19 = (float)Math.Sqrt(num16 * num16 + num17 * num17);
					if (num19 < 20f)
					{
						height5 = (int)num19 - 20 + 12;
						flag4 = false;
					}
					num19 = 12f / num19;
					num16 *= num19;
					num17 *= num19;
					vector5.X += num16;
					vector5.Y += num17;
					num16 = npc[i].ai[1] - vector5.X;
					num17 = npc[i].ai[2] - vector5.Y;
					Color color5 = Lighting.GetColor((int)vector5.X / 16, (int)(vector5.Y / 16f));
					spriteBatch.Draw(chainTexture, new Vector2(vector5.X - screenPosition.X, vector5.Y - screenPosition.Y), new Rectangle(0, 0, chainTexture.Width, height5), color5, num18, new Vector2((float)chainTexture.Width * 0.5f, (float)chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(spikeBaseTexture, new Vector2(npc[i].ai[1] - screenPosition.X, npc[i].ai[2] - screenPosition.Y), new Rectangle(0, 0, spikeBaseTexture.Width, spikeBaseTexture.Height), Lighting.GetColor((int)npc[i].ai[1] / 16, (int)(npc[i].ai[2] / 16f)), num18 - 0.75f, new Vector2((float)spikeBaseTexture.Width * 0.5f, (float)spikeBaseTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			Color color6 = Lighting.GetColor((int)((double)npc[i].position.X + (double)npc[i].width * 0.5) / 16, (int)(((double)npc[i].position.Y + (double)npc[i].height * 0.5) / 16.0));
			if (behindTiles && npc[i].type != 113 && npc[i].type != 114)
			{
				int num20 = (int)((npc[i].position.X - 8f) / 16f);
				int num21 = (int)((npc[i].position.X + (float)npc[i].width + 8f) / 16f);
				int num22 = (int)((npc[i].position.Y - 8f) / 16f);
				int num23 = (int)((npc[i].position.Y + (float)npc[i].height + 8f) / 16f);
				for (int l = num20; l <= num21; l++)
				{
					for (int m = num22; m <= num23; m++)
					{
						if (Lighting.Brightness(l, m) == 0f)
						{
							color6 = Color.Black;
						}
					}
				}
			}
			float num24 = 1f;
			float g = 1f;
			float num25 = 1f;
			float a = 1f;
			if (npc[i].poisoned)
			{
				if (rand.Next(30) == 0)
				{
					Vector2 position3 = npc[i].position;
					int width3 = npc[i].width;
					int height6 = npc[i].height;
					int type3 = 46;
					float speedX3 = 0f;
					float speedY3 = 0f;
					int alpha3 = 120;
					int num26 = Dust.NewDust(position3, width3, height6, type3, speedX3, speedY3, alpha3, default(Color), 0.2f);
					Main.dust[num26].noGravity = true;
					Main.dust[num26].fadeIn = 1.9f;
				}
				num24 *= 0.65f;
				num25 *= 0.75f;
				color6 = buffColor(color6, num24, g, num25, a);
			}
			if (npc[i].onFire)
			{
				if (rand.Next(4) < 3)
				{
					Vector2 position4 = new Vector2(npc[i].position.X - 2f, npc[i].position.Y - 2f);
					int width4 = npc[i].width + 4;
					int height7 = npc[i].height + 4;
					int type4 = 6;
					float speedX4 = npc[i].velocity.X * 0.4f;
					float speedY4 = npc[i].velocity.Y * 0.4f;
					int alpha4 = 100;
					int num27 = Dust.NewDust(position4, width4, height7, type4, speedX4, speedY4, alpha4, default(Color), 3.5f);
					Main.dust[num27].noGravity = true;
					Dust dust = Main.dust[num27];
					dust.velocity *= 1.8f;
					Dust dust2 = Main.dust[num27];
					dust2.velocity.Y = dust2.velocity.Y - 0.5f;
					if (rand.Next(4) == 0)
					{
						Main.dust[num27].noGravity = false;
						Main.dust[num27].scale *= 0.5f;
					}
				}
				Lighting.addLight((int)(npc[i].position.X / 16f), (int)(npc[i].position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
			}
			if (npc[i].onFire2)
			{
				if (rand.Next(4) < 3)
				{
					Vector2 position5 = new Vector2(npc[i].position.X - 2f, npc[i].position.Y - 2f);
					int width5 = npc[i].width + 4;
					int height8 = npc[i].height + 4;
					int type5 = 75;
					float speedX5 = npc[i].velocity.X * 0.4f;
					float speedY5 = npc[i].velocity.Y * 0.4f;
					int alpha5 = 100;
					int num28 = Dust.NewDust(position5, width5, height8, type5, speedX5, speedY5, alpha5, default(Color), 3.5f);
					Main.dust[num28].noGravity = true;
					Dust dust3 = Main.dust[num28];
					dust3.velocity *= 1.8f;
					Dust dust4 = Main.dust[num28];
					dust4.velocity.Y = dust4.velocity.Y - 0.5f;
					if (rand.Next(4) == 0)
					{
						Main.dust[num28].noGravity = false;
						Main.dust[num28].scale *= 0.5f;
					}
				}
				Lighting.addLight((int)(npc[i].position.X / 16f), (int)(npc[i].position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
			}
			if (player[myPlayer].detectCreature && npc[i].lifeMax > 1)
			{
				if (color6.R < 150)
				{
					color6.A = mouseTextColor;
				}
				if (color6.R < 50)
				{
					color6.R = 50;
				}
				if (color6.G < 200)
				{
					color6.G = 200;
				}
				if (color6.B < 100)
				{
					color6.B = 100;
				}
				if (!gamePaused && base.IsActive && rand.Next(50) == 0)
				{
					Vector2 position6 = new Vector2(npc[i].position.X, npc[i].position.Y);
					int width6 = npc[i].width;
					int height9 = npc[i].height;
					int type6 = 15;
					float speedX6 = 0f;
					float speedY6 = 0f;
					int alpha6 = 150;
					int num29 = Dust.NewDust(position6, width6, height9, type6, speedX6, speedY6, alpha6, default(Color), 0.8f);
					Dust dust5 = Main.dust[num29];
					dust5.velocity *= 0.1f;
					Main.dust[num29].noLight = true;
				}
			}
			if (npc[i].type == 50)
			{
				Vector2 vector6 = default(Vector2);
				float num30 = 0f;
				vector6.Y -= npc[i].velocity.Y;
				vector6.X -= npc[i].velocity.X * 2f;
				num30 += npc[i].velocity.X * 0.05f;
				if (npc[i].frame.Y == 120)
				{
					vector6.Y += 2f;
				}
				if (npc[i].frame.Y == 360)
				{
					vector6.Y -= 2f;
				}
				if (npc[i].frame.Y == 480)
				{
					vector6.Y -= 6f;
				}
				spriteBatch.Draw(ninjaTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) + vector6.X, npc[i].position.Y - screenPosition.Y + (float)(npc[i].height / 2) + vector6.Y), new Rectangle(0, 0, ninjaTexture.Width, ninjaTexture.Height), color6, num30, new Vector2(ninjaTexture.Width / 2, ninjaTexture.Height / 2), 1f, SpriteEffects.None, 0f);
			}
			if (npc[i].type == 71)
			{
				Vector2 vector7 = default(Vector2);
				float num31 = 0f;
				vector7.Y -= npc[i].velocity.Y * 0.3f;
				vector7.X -= npc[i].velocity.X * 0.6f;
				num31 += npc[i].velocity.X * 0.09f;
				if (npc[i].frame.Y == 120)
				{
					vector7.Y += 2f;
				}
				if (npc[i].frame.Y == 360)
				{
					vector7.Y -= 2f;
				}
				if (npc[i].frame.Y == 480)
				{
					vector7.Y -= 6f;
				}
				spriteBatch.Draw(itemTexture[327], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) + vector7.X, npc[i].position.Y - screenPosition.Y + (float)(npc[i].height / 2) + vector7.Y), new Rectangle(0, 0, itemTexture[327].Width, itemTexture[327].Height), color6, num31, new Vector2(itemTexture[327].Width / 2, itemTexture[327].Height / 2), 1f, SpriteEffects.None, 0f);
			}
			if (npc[i].type == 69)
			{
				spriteBatch.Draw(antLionTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2), npc[i].position.Y - screenPosition.Y + (float)npc[i].height + 14f), new Rectangle(0, 0, antLionTexture.Width, antLionTexture.Height), color6, (0f - npc[i].rotation) * 0.3f, new Vector2(antLionTexture.Width / 2, antLionTexture.Height / 2), 1f, SpriteEffects.None, 0f);
			}
			float num32 = 0f;
			float num33 = 0f;
			Vector2 origin = new Vector2(npcTexture[npc[i].type].Width / 2, npcTexture[npc[i].type].Height / npcFrameCount[npc[i].type] / 2);
			if (npc[i].type == 108 || npc[i].type == 124)
			{
				num32 = 2f;
			}
			if (npc[i].type == 4)
			{
				origin = new Vector2(55f, 107f);
			}
			else if (npc[i].type == 125)
			{
				origin = new Vector2(55f, 107f);
				num33 = 30f;
			}
			else if (npc[i].type == 126)
			{
				origin = new Vector2(55f, 107f);
				num33 = 30f;
			}
			else if (npc[i].type == 6)
			{
				num33 = 26f;
			}
			else if (npc[i].type == 94)
			{
				num33 = 14f;
			}
			else if (npc[i].type == 7 || npc[i].type == 8 || npc[i].type == 9)
			{
				num33 = 13f;
			}
			else if (npc[i].type == 98 || npc[i].type == 99 || npc[i].type == 100)
			{
				num33 = 13f;
			}
			else if (npc[i].type == 95 || npc[i].type == 96 || npc[i].type == 97)
			{
				num33 = 13f;
			}
			else if (npc[i].type == 10 || npc[i].type == 11 || npc[i].type == 12)
			{
				num33 = 8f;
			}
			else if (npc[i].type == 13 || npc[i].type == 14 || npc[i].type == 15)
			{
				num33 = 26f;
			}
			else if (npc[i].type == 48)
			{
				num33 = 32f;
			}
			else if (npc[i].type == 49 || npc[i].type == 51)
			{
				num33 = 4f;
			}
			else if (npc[i].type == 60)
			{
				num33 = 10f;
			}
			else if (npc[i].type == 62 || npc[i].type == 66)
			{
				num33 = 14f;
			}
			else if (npc[i].type == 63 || npc[i].type == 64 || npc[i].type == 103)
			{
				num33 = 4f;
				origin.Y += 4f;
			}
			else if (npc[i].type == 65)
			{
				num33 = 14f;
			}
			else if (npc[i].type == 69)
			{
				num33 = 4f;
				origin.Y += 8f;
			}
			else if (npc[i].type == 70)
			{
				num33 = -4f;
			}
			else if (npc[i].type == 72)
			{
				num33 = -2f;
			}
			else if (npc[i].type == 83 || npc[i].type == 84)
			{
				num33 = 20f;
			}
			else if (npc[i].type == 39 || npc[i].type == 40 || npc[i].type == 41)
			{
				num33 = 26f;
			}
			else if (npc[i].type >= 87 && npc[i].type <= 92)
			{
				num33 = 56f;
			}
			else if (npc[i].type >= 134 && npc[i].type <= 136)
			{
				num33 = 30f;
			}
			num33 *= npc[i].scale;
			if (npc[i].aiStyle == 10 || npc[i].type == 72)
			{
				color6 = Color.White;
			}
			SpriteEffects effects = SpriteEffects.None;
			if (npc[i].spriteDirection == 1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			if (npc[i].type == 83 || npc[i].type == 84)
			{
				spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, Color.White, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				return;
			}
			if (npc[i].type >= 87 && npc[i].type <= 92)
			{
				Color alpha7 = npc[i].GetAlpha(color6);
				byte b = (byte)((tileColor.R + tileColor.G + tileColor.B) / 3);
				if (alpha7.R < b)
				{
					alpha7.R = b;
				}
				if (alpha7.G < b)
				{
					alpha7.G = b;
				}
				if (alpha7.B < b)
				{
					alpha7.B = b;
				}
				spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, alpha7, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				return;
			}
			if (npc[i].type == 94)
			{
				for (int n = 1; n < 6; n += 2)
				{
					_ = npc[i].oldPos[n];
					Color alpha8 = npc[i].GetAlpha(color6);
					alpha8.R = (byte)(alpha8.R * (10 - n) / 15);
					alpha8.G = (byte)(alpha8.G * (10 - n) / 15);
					alpha8.B = (byte)(alpha8.B * (10 - n) / 15);
					alpha8.A = (byte)(alpha8.A * (10 - n) / 15);
					spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].oldPos[n].X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].oldPos[n].Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33), npc[i].frame, alpha8, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				}
			}
			if (npc[i].type == 125 || npc[i].type == 126 || npc[i].type == 127 || npc[i].type == 128 || npc[i].type == 129 || npc[i].type == 130 || npc[i].type == 131 || npc[i].type == 139 || npc[i].type == 140)
			{
				for (int num34 = 9; num34 >= 0; num34 -= 2)
				{
					_ = npc[i].oldPos[num34];
					Color alpha9 = npc[i].GetAlpha(color6);
					alpha9.R = (byte)(alpha9.R * (10 - num34) / 20);
					alpha9.G = (byte)(alpha9.G * (10 - num34) / 20);
					alpha9.B = (byte)(alpha9.B * (10 - num34) / 20);
					alpha9.A = (byte)(alpha9.A * (10 - num34) / 20);
					spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].oldPos[num34].X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].oldPos[num34].Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33), npc[i].frame, alpha9, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				}
			}
			spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, npc[i].GetAlpha(color6), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			Color color7 = npc[i].color;
			if (color7 != default(Color))
			{
				spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, npc[i].GetColor(color6), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			for (int num35 = 0; num35 < npc[i].buffType.Length; num35++)
			{
				if (npc[i].buffType[num35] == 31 && npc[i].buffTime[num35] > 0)
				{
					spriteBatch.Draw(confuseTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32 - (float)confuseTexture.Height - 20f), new Rectangle(0, 0, confuseTexture.Width, confuseTexture.Height), new Color(250, 250, 250, 70), npc[i].velocity.X * -0.05f, new Vector2(confuseTexture.Width / 2, confuseTexture.Height / 2), essScale + 0.2f, SpriteEffects.None, 0f);
					break;
				}
			}
			if (npc[i].type >= 134 && npc[i].type <= 136 && color6 != Color.Black)
			{
				spriteBatch.Draw(destTexture[npc[i].type - 134], new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, new Color(255, 255, 255, 0), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			if (npc[i].type == 125)
			{
				spriteBatch.Draw(EyeLaserTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, new Color(255, 255, 255, 0), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			if (npc[i].type == 139)
			{
				spriteBatch.Draw(probeTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, new Color(255, 255, 255, 0), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			if (npc[i].type == 127)
			{
				spriteBatch.Draw(BoneEyesTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, new Color(200, 200, 200, 0), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			if (npc[i].type == 131)
			{
				spriteBatch.Draw(BoneLaserTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33 + num32), npc[i].frame, new Color(200, 200, 200, 0), npc[i].rotation, origin, npc[i].scale, effects, 0f);
			}
			if (npc[i].type == 120)
			{
				for (int num36 = 1; num36 < npc[i].oldPos.Length; num36++)
				{
					_ = npc[i].oldPos[num36];
					Color color8 = default(Color);
					color8.R = (byte)(150 * (10 - num36) / 15);
					color8.G = (byte)(100 * (10 - num36) / 15);
					color8.B = (byte)(150 * (10 - num36) / 15);
					color8.A = (byte)(50 * (10 - num36) / 15);
					spriteBatch.Draw(chaosTexture, new Vector2(npc[i].oldPos[num36].X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].oldPos[num36].Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33), npc[i].frame, color8, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				}
			}
			else if (npc[i].type == 137 || npc[i].type == 138)
			{
				for (int num37 = 1; num37 < npc[i].oldPos.Length; num37++)
				{
					_ = npc[i].oldPos[num37];
					Color color9 = default(Color);
					color9.R = (byte)(150 * (10 - num37) / 15);
					color9.G = (byte)(100 * (10 - num37) / 15);
					color9.B = (byte)(150 * (10 - num37) / 15);
					color9.A = (byte)(50 * (10 - num37) / 15);
					spriteBatch.Draw(npcTexture[npc[i].type], new Vector2(npc[i].oldPos[num37].X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].oldPos[num37].Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33), npc[i].frame, color9, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				}
			}
			else if (npc[i].type == 82)
			{
				spriteBatch.Draw(wraithEyeTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33), npc[i].frame, Color.White, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				for (int num38 = 1; num38 < 10; num38++)
				{
					Color color10 = new Color(110 - num38 * 10, 110 - num38 * 10, 110 - num38 * 10, 110 - num38 * 10);
					spriteBatch.Draw(wraithEyeTexture, new Vector2(npc[i].position.X - screenPosition.X + (float)(npc[i].width / 2) - (float)npcTexture[npc[i].type].Width * npc[i].scale / 2f + origin.X * npc[i].scale, npc[i].position.Y - screenPosition.Y + (float)npc[i].height - (float)npcTexture[npc[i].type].Height * npc[i].scale / (float)npcFrameCount[npc[i].type] + 4f + origin.Y * npc[i].scale + num33) - npc[i].velocity * num38 * 0.5f, npc[i].frame, color10, npc[i].rotation, origin, npc[i].scale, effects, 0f);
				}
			}
		}

		public void DrawProj(int i)
		{
			bool flag = true;
			if (projectile[i].PreDraw != null)
			{
				flag = projectile[i].PreDraw(spriteBatch);
			}
			if (flag)
			{
				int type = projectile[i].type;
				int value = type;
				if (Config.projDefs.drawPretendType.TryGetValue(type, out value))
				{
					projectile[i].type = value;
				}
				DrawProjReal(i);
				projectile[i].type = type;
			}
			if (projectile[i].PostDraw != null)
			{
				projectile[i].PostDraw(spriteBatch);
			}
		}

		public void DrawProjReal(int i)
		{
			if (projectile[i].type == 32)
			{
				Vector2 vector = new Vector2(projectile[i].position.X + (float)projectile[i].width * 0.5f, projectile[i].position.Y + (float)projectile[i].height * 0.5f);
				float num = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector.X;
				float num2 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector.Y;
				float rotation = (float)Math.Atan2(num2, num) - 1.57f;
				bool flag = true;
				if (num == 0f && num2 == 0f)
				{
					flag = false;
				}
				else
				{
					float num3 = (float)Math.Sqrt(num * num + num2 * num2);
					num3 = 8f / num3;
					num *= num3;
					num2 *= num3;
					vector.X -= num;
					vector.Y -= num2;
					num = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector.X;
					num2 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector.Y;
				}
				while (flag)
				{
					float num4 = (float)Math.Sqrt(num * num + num2 * num2);
					if (num4 < 28f)
					{
						flag = false;
						continue;
					}
					num4 = 28f / num4;
					num *= num4;
					num2 *= num4;
					vector.X += num;
					vector.Y += num2;
					num = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector.X;
					num2 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector.Y;
					Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
					spriteBatch.Draw(chain5Texture, new Vector2(vector.X - screenPosition.X, vector.Y - screenPosition.Y), new Rectangle(0, 0, chain5Texture.Width, chain5Texture.Height), color, rotation, new Vector2((float)chain5Texture.Width * 0.5f, (float)chain5Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (projectile[i].type == 73)
			{
				Vector2 vector2 = new Vector2(projectile[i].position.X + (float)projectile[i].width * 0.5f, projectile[i].position.Y + (float)projectile[i].height * 0.5f);
				float num5 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector2.X;
				float num6 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector2.Y;
				float rotation2 = (float)Math.Atan2(num6, num5) - 1.57f;
				bool flag2 = true;
				while (flag2)
				{
					float num7 = (float)Math.Sqrt(num5 * num5 + num6 * num6);
					if (num7 < 25f)
					{
						flag2 = false;
						continue;
					}
					num7 = 12f / num7;
					num5 *= num7;
					num6 *= num7;
					vector2.X += num5;
					vector2.Y += num6;
					num5 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector2.X;
					num6 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector2.Y;
					Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
					spriteBatch.Draw(chain8Texture, new Vector2(vector2.X - screenPosition.X, vector2.Y - screenPosition.Y), new Rectangle(0, 0, chain8Texture.Width, chain8Texture.Height), color2, rotation2, new Vector2((float)chain8Texture.Width * 0.5f, (float)chain8Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (projectile[i].type == 74)
			{
				Vector2 vector3 = new Vector2(projectile[i].position.X + (float)projectile[i].width * 0.5f, projectile[i].position.Y + (float)projectile[i].height * 0.5f);
				float num8 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector3.X;
				float num9 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector3.Y;
				float rotation3 = (float)Math.Atan2(num9, num8) - 1.57f;
				bool flag3 = true;
				while (flag3)
				{
					float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
					if (num10 < 25f)
					{
						flag3 = false;
						continue;
					}
					num10 = 12f / num10;
					num8 *= num10;
					num9 *= num10;
					vector3.X += num8;
					vector3.Y += num9;
					num8 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector3.X;
					num9 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector3.Y;
					Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
					spriteBatch.Draw(chain9Texture, new Vector2(vector3.X - screenPosition.X, vector3.Y - screenPosition.Y), new Rectangle(0, 0, chain8Texture.Width, chain8Texture.Height), color3, rotation3, new Vector2((float)chain8Texture.Width * 0.5f, (float)chain8Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (projectile[i].aiStyle == 7)
			{
				Vector2 vector4 = new Vector2(projectile[i].position.X + (float)projectile[i].width * 0.5f, projectile[i].position.Y + (float)projectile[i].height * 0.5f);
				float num11 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector4.X;
				float num12 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector4.Y;
				float rotation4 = (float)Math.Atan2(num12, num11) - 1.57f;
				bool flag4 = true;
				while (flag4)
				{
					float num13 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
					if (num13 < 25f)
					{
						flag4 = false;
						continue;
					}
					num13 = 12f / num13;
					num11 *= num13;
					num12 *= num13;
					vector4.X += num11;
					vector4.Y += num12;
					num11 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector4.X;
					num12 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector4.Y;
					Color color4 = Lighting.GetColor((int)vector4.X / 16, (int)(vector4.Y / 16f));
					spriteBatch.Draw(chainTexture, new Vector2(vector4.X - screenPosition.X, vector4.Y - screenPosition.Y), new Rectangle(0, 0, chainTexture.Width, chainTexture.Height), color4, rotation4, new Vector2((float)chainTexture.Width * 0.5f, (float)chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (projectile[i].aiStyle == 13)
			{
				float num14 = projectile[i].position.X + 8f;
				float num15 = projectile[i].position.Y + 2f;
				float x = projectile[i].velocity.X;
				float y = projectile[i].velocity.Y;
				float num16 = (float)Math.Sqrt(x * x + y * y);
				num16 = 20f / num16;
				if (projectile[i].ai[0] == 0f)
				{
					num14 -= projectile[i].velocity.X * num16;
					num15 -= projectile[i].velocity.Y * num16;
				}
				else
				{
					num14 += projectile[i].velocity.X * num16;
					num15 += projectile[i].velocity.Y * num16;
				}
				Vector2 vector5 = new Vector2(num14, num15);
				x = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector5.X;
				y = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector5.Y;
				float rotation5 = (float)Math.Atan2(y, x) - 1.57f;
				if (projectile[i].alpha == 0)
				{
					int num17 = -1;
					if (projectile[i].position.X + (float)(projectile[i].width / 2) < player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2))
					{
						num17 = 1;
					}
					if (player[projectile[i].owner].direction == 1)
					{
						player[projectile[i].owner].itemRotation = (float)Math.Atan2(y * (float)num17, x * (float)num17);
					}
					else
					{
						player[projectile[i].owner].itemRotation = (float)Math.Atan2(y * (float)num17, x * (float)num17);
					}
				}
				bool flag5 = true;
				int num18 = 0;
				while (flag5 && num18 < 500)
				{
					num18++;
					float num19 = (float)Math.Sqrt(x * x + y * y);
					if (num19 < 25f)
					{
						flag5 = false;
						continue;
					}
					num19 = 12f / num19;
					x *= num19;
					y *= num19;
					vector5.X += x;
					vector5.Y += y;
					x = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector5.X;
					y = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector5.Y;
					Color color5 = Lighting.GetColor((int)vector5.X / 16, (int)(vector5.Y / 16f));
					spriteBatch.Draw(chainTexture, new Vector2(vector5.X - screenPosition.X, vector5.Y - screenPosition.Y), new Rectangle(0, 0, chainTexture.Width, chainTexture.Height), color5, rotation5, new Vector2((float)chainTexture.Width * 0.5f, (float)chainTexture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (projectile[i].aiStyle == 15)
			{
				Vector2 vector6 = new Vector2(projectile[i].position.X + (float)projectile[i].width * 0.5f, projectile[i].position.Y + (float)projectile[i].height * 0.5f);
				float num20 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector6.X;
				float num21 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector6.Y;
				float rotation6 = (float)Math.Atan2(num21, num20) - 1.57f;
				if (projectile[i].alpha == 0)
				{
					int num22 = -1;
					if (projectile[i].position.X + (float)(projectile[i].width / 2) < player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2))
					{
						num22 = 1;
					}
					if (player[projectile[i].owner].direction == 1)
					{
						player[projectile[i].owner].itemRotation = (float)Math.Atan2(num21 * (float)num22, num20 * (float)num22);
					}
					else
					{
						player[projectile[i].owner].itemRotation = (float)Math.Atan2(num21 * (float)num22, num20 * (float)num22);
					}
				}
				bool flag6 = true;
				while (flag6)
				{
					float num23 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
					if (num23 < 25f)
					{
						flag6 = false;
						continue;
					}
					num23 = 12f / num23;
					num20 *= num23;
					num21 *= num23;
					vector6.X += num20;
					vector6.Y += num21;
					num20 = player[projectile[i].owner].position.X + (float)(player[projectile[i].owner].width / 2) - vector6.X;
					num21 = player[projectile[i].owner].position.Y + (float)(player[projectile[i].owner].height / 2) - vector6.Y;
					Color color6 = Lighting.GetColor((int)vector6.X / 16, (int)(vector6.Y / 16f));
					if (projectile[i].type == 25)
					{
						spriteBatch.Draw(chain2Texture, new Vector2(vector6.X - screenPosition.X, vector6.Y - screenPosition.Y), new Rectangle(0, 0, chain2Texture.Width, chain2Texture.Height), color6, rotation6, new Vector2((float)chain2Texture.Width * 0.5f, (float)chain2Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
					else if (projectile[i].type == 35)
					{
						spriteBatch.Draw(chain6Texture, new Vector2(vector6.X - screenPosition.X, vector6.Y - screenPosition.Y), new Rectangle(0, 0, chain6Texture.Width, chain6Texture.Height), color6, rotation6, new Vector2((float)chain6Texture.Width * 0.5f, (float)chain6Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
					else if (projectile[i].type == 63)
					{
						spriteBatch.Draw(chain7Texture, new Vector2(vector6.X - screenPosition.X, vector6.Y - screenPosition.Y), new Rectangle(0, 0, chain7Texture.Width, chain7Texture.Height), color6, rotation6, new Vector2((float)chain7Texture.Width * 0.5f, (float)chain7Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
					else
					{
						spriteBatch.Draw(chain3Texture, new Vector2(vector6.X - screenPosition.X, vector6.Y - screenPosition.Y), new Rectangle(0, 0, chain3Texture.Width, chain3Texture.Height), color6, rotation6, new Vector2((float)chain3Texture.Width * 0.5f, (float)chain3Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			Color newColor = Lighting.GetColor((int)((double)projectile[i].position.X + (double)projectile[i].width * 0.5) / 16, (int)(((double)projectile[i].position.Y + (double)projectile[i].height * 0.5) / 16.0));
			if (projectile[i].hide)
			{
				newColor = Lighting.GetColor((int)((double)player[projectile[i].owner].position.X + (double)player[projectile[i].owner].width * 0.5) / 16, (int)(((double)player[projectile[i].owner].position.Y + (double)player[projectile[i].owner].height * 0.5) / 16.0));
			}
			if (projectile[i].type == 14)
			{
				newColor = Color.White;
			}
			int num24 = 0;
			int num25 = 0;
			if (projectile[i].type == 16)
			{
				num24 = 6;
			}
			if (projectile[i].type == 17 || projectile[i].type == 31)
			{
				num24 = 2;
			}
			if (projectile[i].type == 25 || projectile[i].type == 26 || projectile[i].type == 35 || projectile[i].type == 63)
			{
				num24 = 6;
				num25 -= 6;
			}
			if (projectile[i].type == 28 || projectile[i].type == 37 || projectile[i].type == 75)
			{
				num24 = 8;
			}
			if (projectile[i].type == 29)
			{
				num24 = 11;
			}
			if (projectile[i].type == 43)
			{
				num24 = 4;
			}
			if (projectile[i].type == 69 || projectile[i].type == 70)
			{
				num24 = 4;
				num25 = 4;
			}
			float num26 = (float)(projectileTexture[projectile[i].type].Width - projectile[i].width) * 0.5f + (float)projectile[i].width * 0.5f;
			if (projectile[i].type == 50 || projectile[i].type == 53)
			{
				num25 = -8;
			}
			if (projectile[i].type == 72 || projectile[i].type == 86 || projectile[i].type == 87)
			{
				num25 = -16;
				num24 = 8;
			}
			if (projectile[i].type == 74)
			{
				num25 = -6;
			}
			if (projectile[i].type == 99)
			{
				num24 = 1;
			}
			if (projectile[i].type == 111)
			{
				num24 = 18;
				num25 = -16;
			}
			int value = 0;
			if (Config.projDefs.frameOffsetX.TryGetValue(projectile[i].type, out value))
			{
				num24 = value;
			}
			int value2 = 0;
			if (Config.projDefs.frameOffsetY.TryGetValue(projectile[i].type, out value2))
			{
				num25 = value2;
			}
			SpriteEffects effects = SpriteEffects.None;
			if (projectile[i].spriteDirection == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}
			if (projFrames[projectile[i].type] > 1)
			{
				int num27 = projectileTexture[projectile[i].type].Height / projFrames[projectile[i].type];
				int y2 = num27 * projectile[i].frame;
				if (projectile[i].type == 111)
				{
					int r = player[projectile[i].owner].shirtColor.R;
					int g = player[projectile[i].owner].shirtColor.G;
					int b = player[projectile[i].owner].shirtColor.B;
					newColor = Lighting.GetColor(oldColor: new Color((byte)r, (byte)g, (byte)b), x: (int)((double)projectile[i].position.X + (double)projectile[i].width * 0.5) / 16, y: (int)(((double)projectile[i].position.Y + (double)projectile[i].height * 0.5) / 16.0));
					spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, y2, projectileTexture[projectile[i].type].Width, num27), projectile[i].GetAlpha(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
					if (projectile[i].color != default(Color))
					{
						spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, y2, projectileTexture[projectile[i].type].Width, num27), projectile[i].GetColor(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
					}
				}
				else
				{
					spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, y2, projectileTexture[projectile[i].type].Width, num27), projectile[i].GetAlpha(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
					if (projectile[i].color != default(Color))
					{
						spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, y2, projectileTexture[projectile[i].type].Width, num27), projectile[i].GetColor(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
					}
				}
				return;
			}
			if (projectile[i].aiStyle == 19)
			{
				Vector2 origin = new Vector2(0f, 0f);
				if (projectile[i].spriteDirection == -1)
				{
					origin.X = projectileTexture[projectile[i].type].Width;
				}
				spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + (float)(projectile[i].width / 2), projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), projectile[i].GetAlpha(newColor), projectile[i].rotation, origin, projectile[i].scale, effects, 0f);
				if (projectile[i].color != default(Color))
				{
					spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + (float)(projectile[i].width / 2), projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), projectile[i].GetColor(newColor), projectile[i].rotation, origin, projectile[i].scale, effects, 0f);
				}
				return;
			}
			if (projectile[i].type == 94 && projectile[i].ai[1] > 6f)
			{
				for (int j = 0; j < 10; j++)
				{
					Color alpha = projectile[i].GetAlpha(newColor);
					float num28 = (float)(9 - j) / 9f;
					alpha.R = (byte)((float)(int)alpha.R * num28);
					alpha.G = (byte)((float)(int)alpha.G * num28);
					alpha.B = (byte)((float)(int)alpha.B * num28);
					alpha.A = (byte)((float)(int)alpha.A * num28);
					float num29 = (float)(9 - j) / 9f;
					spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].oldPos[j].X - screenPosition.X + num26 + (float)num25, projectile[i].oldPos[j].Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), alpha, projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), num29 * projectile[i].scale, effects, 0f);
				}
			}
			spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), projectile[i].GetAlpha(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
			if (projectile[i].color != default(Color))
			{
				spriteBatch.Draw(projectileTexture[projectile[i].type], new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), projectile[i].GetColor(newColor), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
			}
			if (projectile[i].type == 106)
			{
				spriteBatch.Draw(lightDiscTexture, new Vector2(projectile[i].position.X - screenPosition.X + num26 + (float)num25, projectile[i].position.Y - screenPosition.Y + (float)(projectile[i].height / 2)), new Rectangle(0, 0, projectileTexture[projectile[i].type].Width, projectileTexture[projectile[i].type].Height), new Color(200, 200, 200, 0), projectile[i].rotation, new Vector2(num26, projectile[i].height / 2 + num24), projectile[i].scale, effects, 0f);
			}
		}

		public static Color buffColor(Color newColor, float R, float G, float B, float A)
		{
			newColor.R = (byte)((float)(int)newColor.R * R);
			newColor.G = (byte)((float)(int)newColor.G * G);
			newColor.B = (byte)((float)(int)newColor.B * B);
			newColor.A = (byte)((float)(int)newColor.A * A);
			return newColor;
		}

		public void DrawWoF()
		{
			if (wof < 0 || !player[myPlayer].gross)
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (!player[i].active || !player[i].tongued || player[i].dead)
				{
					continue;
				}
				float num = npc[wof].position.X + (float)(npc[wof].width / 2);
				float num2 = npc[wof].position.Y + (float)(npc[wof].height / 2);
				Vector2 vector = new Vector2(player[i].position.X + (float)player[i].width * 0.5f, player[i].position.Y + (float)player[i].height * 0.5f);
				float num3 = num - vector.X;
				float num4 = num2 - vector.Y;
				float rotation = (float)Math.Atan2(num4, num3) - 1.57f;
				bool flag = true;
				while (flag)
				{
					float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
					if (num5 < 40f)
					{
						flag = false;
						continue;
					}
					num5 = (float)chain12Texture.Height / num5;
					num3 *= num5;
					num4 *= num5;
					vector.X += num3;
					vector.Y += num4;
					num3 = num - vector.X;
					num4 = num2 - vector.Y;
					Color color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f));
					spriteBatch.Draw(chain12Texture, new Vector2(vector.X - screenPosition.X, vector.Y - screenPosition.Y), new Rectangle(0, 0, chain12Texture.Width, chain12Texture.Height), color, rotation, new Vector2((float)chain12Texture.Width * 0.5f, (float)chain12Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			for (int j = 0; j < 200; j++)
			{
				if (!npc[j].active || npc[j].aiStyle != 29)
				{
					continue;
				}
				float num6 = npc[wof].position.X + (float)(npc[wof].width / 2);
				float y = npc[wof].position.Y;
				float num7 = wofB - wofT;
				bool flag2 = false;
				if (npc[j].frameCounter > 7.0)
				{
					flag2 = true;
				}
				y = (float)wofT + num7 * npc[j].ai[0];
				Vector2 vector2 = new Vector2(npc[j].position.X + (float)(npc[j].width / 2), npc[j].position.Y + (float)(npc[j].height / 2));
				float num8 = num6 - vector2.X;
				float num9 = y - vector2.Y;
				float rotation2 = (float)Math.Atan2(num9, num8) - 1.57f;
				bool flag3 = true;
				while (flag3)
				{
					SpriteEffects effects = SpriteEffects.None;
					if (flag2)
					{
						effects = SpriteEffects.FlipHorizontally;
						flag2 = false;
					}
					else
					{
						flag2 = true;
					}
					int height = 28;
					float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
					if (num10 < 40f)
					{
						height = (int)num10 - 40 + 28;
						flag3 = false;
					}
					num10 = 28f / num10;
					num8 *= num10;
					num9 *= num10;
					vector2.X += num8;
					vector2.Y += num9;
					num8 = num6 - vector2.X;
					num9 = y - vector2.Y;
					Color color2 = Lighting.GetColor((int)vector2.X / 16, (int)(vector2.Y / 16f));
					spriteBatch.Draw(chain12Texture, new Vector2(vector2.X - screenPosition.X, vector2.Y - screenPosition.Y), new Rectangle(0, 0, chain4Texture.Width, height), color2, rotation2, new Vector2((float)chain4Texture.Width * 0.5f, (float)chain4Texture.Height * 0.5f), 1f, effects, 0f);
				}
			}
			int num11 = 140;
			float num12 = wofT;
			float num13 = wofB;
			num13 = screenPosition.Y + (float)screenHeight;
			float num14 = (int)((num12 - screenPosition.Y) / (float)num11) + 1;
			num14 *= (float)num11;
			if (num14 > 0f)
			{
				num12 -= num14;
			}
			float num15 = num12;
			float num16 = npc[wof].position.X;
			float num17 = num13 - num12;
			bool flag4 = true;
			SpriteEffects effects2 = SpriteEffects.None;
			if (npc[wof].spriteDirection == 1)
			{
				effects2 = SpriteEffects.FlipHorizontally;
			}
			if (npc[wof].direction > 0)
			{
				num16 -= 80f;
			}
			int num18 = 0;
			if (!gamePaused)
			{
				wofF++;
			}
			if (wofF > 12)
			{
				num18 = 280;
				if (wofF > 17)
				{
					wofF = 0;
				}
			}
			else if (wofF > 6)
			{
				num18 = 140;
			}
			while (flag4)
			{
				num17 = num13 - num15;
				if (num17 > (float)num11)
				{
					num17 = num11;
				}
				bool flag5 = true;
				int num19 = 0;
				while (flag5)
				{
					int x = (int)(num16 + (float)(wofTexture.Width / 2)) / 16;
					int y2 = (int)(num15 + (float)num19) / 16;
					spriteBatch.Draw(wofTexture, new Vector2(num16 - screenPosition.X, num15 + (float)num19 - screenPosition.Y), new Rectangle(0, num18 + num19, wofTexture.Width, 16), Lighting.GetColor(x, y2), 0f, default(Vector2), 1f, effects2, 0f);
					num19 += 16;
					if ((float)num19 >= num17)
					{
						flag5 = false;
					}
				}
				num15 += (float)num11;
				if (num15 >= num13)
				{
					flag4 = false;
				}
			}
		}

		public void DrawGhost(Player drawPlayer)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			spriteEffects = ((drawPlayer.direction != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			Color immuneAlpha = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, new Color((int)mouseTextColor / 2 + 100, (int)mouseTextColor / 2 + 100, (int)mouseTextColor / 2 + 100, (int)mouseTextColor / 2 + 100)));
			Rectangle value = new Rectangle(0, ghostTexture.Height / 4 * drawPlayer.ghostFrame, ghostTexture.Width, ghostTexture.Height / 4);
			Vector2 origin = new Vector2((float)value.Width * 0.5f, (float)value.Height * 0.5f);
			spriteBatch.Draw(ghostTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X + (float)(value.Width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)(value.Height / 2))), value, immuneAlpha, 0f, origin, 1f, spriteEffects, 0f);
		}

		public void SavePlayerScreenShot(Player Pr)
		{
			SpriteBatch spriteBatch = this.spriteBatch;
			RenderTarget2D renderTarget2D = null;
			this.spriteBatch = new SpriteBatch(base.GraphicsDevice);
			renderTarget2D = new RenderTarget2D(base.GraphicsDevice, screenWidth, screenHeight);
			Config.mainInstance.GraphicsDevice.SetRenderTarget(renderTarget2D);
			this.spriteBatch.Begin();
			base.GraphicsDevice.Clear(Color.Transparent);
			Player player = (Player)Pr.Clone();
			player.Center = new Vector2(screenWidth, screenHeight) / 2f;
			player.velocity = Vector2.Zero;
			player.itemAnimation = 0;
			player.itemAnimationMax = 0;
			player.direction = 1;
			player.gravDir = 1f;
			player.PlayerFrame();
			Vector2 vector = screenPosition;
			screenPosition = Vector2.Zero;
			Lighting.addLight(1, 1, 1f, (int)player.Center.X / 16, (int)player.Center.Y / 16);
			player.immuneAlpha = 0;
			DrawPlayer(player);
			this.spriteBatch.End();
			Config.mainInstance.GraphicsDevice.SetRenderTarget(null);
			Texture2D texture2D = renderTarget2D;
			Color[] array = new Color[texture2D.Width * texture2D.Height];
			texture2D.GetData(array);
			int num = texture2D.Width - 1;
			int num2 = 0;
			int num3 = texture2D.Height - 1;
			int num4 = 0;
			for (int i = 0; i < texture2D.Width; i++)
			{
				for (int j = 0; j < texture2D.Height; j++)
				{
					if (array[j * texture2D.Width + i] != Color.Transparent)
					{
						if (i < num)
						{
							num = i;
						}
						if (i > num2)
						{
							num2 = i;
						}
						if (j < num3)
						{
							num3 = j;
						}
						if (j > num4)
						{
							num4 = j;
						}
					}
				}
			}
			Color[] array2 = new Color[(num2 - num + 1) * (num4 - num3 + 1)];
			for (int k = 0; k < texture2D.Width; k++)
			{
				for (int l = 0; l < texture2D.Height; l++)
				{
					if (array[l * texture2D.Width + k] != Color.Transparent)
					{
						array2[(l - num3) * (num2 + 1 - num) + (k - num)] = array[l * texture2D.Width + k];
					}
				}
			}
			Texture2D texture2D2 = new Texture2D(base.GraphicsDevice, num2 + 1 - num, num4 + 1 - num3);
			texture2D2.SetData(array2);
			FileStream fileStream = new FileStream(playerPathName + ".png", FileMode.Create);
			texture2D2.SaveAsPng(fileStream, texture2D2.Width, texture2D2.Height);
			fileStream.Close();
			this.spriteBatch = spriteBatch;
			screenPosition = vector;
		}

		public void DrawPlayer(Player drawPlayer)
		{
			bool flag = true;
			if (Codable.RunPlayerMethodRef("PreDraw", false, drawPlayer, spriteBatch, flag) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn = Codable.customMethodRefReturn;
				flag = (bool)customMethodRefReturn[2];
			}
			if (!flag)
			{
				Codable.RunPlayerMethod("PostDraw", true, drawPlayer, spriteBatch);
				return;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			SpriteEffects spriteEffects2 = SpriteEffects.FlipHorizontally;
			Color color = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), Color.White));
			Color color2 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.eyeColor));
			Color color3 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.hairColor));
			Color color4 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16.0), drawPlayer.skinColor));
			Color color5 = drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.skinColor));
			drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.skinColor));
			Color color6 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.shirtColor));
			Color color7 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0), drawPlayer.underShirtColor));
			Color color8 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.pantsColor));
			Color color9 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16.0), drawPlayer.shoeColor));
			Color color10 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.25) / 16, Color.White));
			Color color11 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16, Color.White));
			Color color12 = drawPlayer.GetImmuneAlpha2(Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.75) / 16, Color.White));
			if (drawPlayer.shadow > 0f)
			{
				new Color(0, 0, 0, 0);
				color5 = new Color(0, 0, 0, 0);
				color4 = new Color(0, 0, 0, 0);
				color3 = new Color(0, 0, 0, 0);
				color2 = new Color(0, 0, 0, 0);
				color = new Color(0, 0, 0, 0);
			}
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			float num4 = 1f;
			if (drawPlayer.poisoned)
			{
				if (rand.Next(50) == 0)
				{
					int num5 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
					Main.dust[num5].noGravity = true;
					Main.dust[num5].fadeIn = 1.9f;
				}
				num *= 0.65f;
				num3 *= 0.75f;
			}
			if (drawPlayer.onFire)
			{
				if (rand.Next(4) == 0)
				{
					int num6 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 6, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[num6].noGravity = true;
					Dust dust = Main.dust[num6];
					dust.velocity *= 1.8f;
					Dust dust2 = Main.dust[num6];
					dust2.velocity.Y = dust2.velocity.Y - 0.5f;
				}
				num3 *= 0.6f;
				num2 *= 0.7f;
			}
			if (drawPlayer.onFire2)
			{
				if (rand.Next(4) == 0)
				{
					int num7 = Dust.NewDust(new Vector2(drawPlayer.position.X - 2f, drawPlayer.position.Y - 2f), drawPlayer.width + 4, drawPlayer.height + 4, 75, drawPlayer.velocity.X * 0.4f, drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[num7].noGravity = true;
					Dust dust3 = Main.dust[num7];
					dust3.velocity *= 1.8f;
					Dust dust4 = Main.dust[num7];
					dust4.velocity.Y = dust4.velocity.Y - 0.5f;
				}
				num3 *= 0.6f;
				num2 *= 0.7f;
			}
			if (drawPlayer.noItems)
			{
				num2 *= 0.8f;
				num *= 0.65f;
			}
			if (drawPlayer.blind)
			{
				num2 *= 0.65f;
				num *= 0.7f;
			}
			if (drawPlayer.bleed)
			{
				num2 *= 0.9f;
				num3 *= 0.9f;
				if (!drawPlayer.dead && rand.Next(30) == 0)
				{
					int num8 = Dust.NewDust(drawPlayer.position, drawPlayer.width, drawPlayer.height, 5);
					Dust dust5 = Main.dust[num8];
					dust5.velocity.Y = dust5.velocity.Y + 0.5f;
					Dust dust6 = Main.dust[num8];
					dust6.velocity *= 0.25f;
				}
			}
			bool flag2 = false;
			if (Codable.RunPlayerMethodRef("ModifyPlayerDrawColors", false, drawPlayer, flag2, num, num2, num3, num4) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn2 = Codable.customMethodRefReturn;
				flag2 = (bool)customMethodRefReturn2[1];
				num = (float)customMethodRefReturn2[2];
				num2 = (float)customMethodRefReturn2[3];
				num3 = (float)customMethodRefReturn2[4];
				num4 = (float)customMethodRefReturn2[5];
			}
			if (num != 1f || num2 != 1f || num3 != 1f || num4 != 1f)
			{
				if ((drawPlayer.onFire || drawPlayer.onFire2) && !flag2)
				{
					color = drawPlayer.GetImmuneAlpha(Color.White);
					color2 = drawPlayer.GetImmuneAlpha(drawPlayer.eyeColor);
					color3 = drawPlayer.GetImmuneAlpha(drawPlayer.hairColor);
					color4 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
					color5 = drawPlayer.GetImmuneAlpha(drawPlayer.skinColor);
					color6 = drawPlayer.GetImmuneAlpha(drawPlayer.shirtColor);
					color7 = drawPlayer.GetImmuneAlpha(drawPlayer.underShirtColor);
					color8 = drawPlayer.GetImmuneAlpha(drawPlayer.pantsColor);
					color9 = drawPlayer.GetImmuneAlpha(drawPlayer.shoeColor);
					color10 = drawPlayer.GetImmuneAlpha(Color.White);
					color11 = drawPlayer.GetImmuneAlpha(Color.White);
					color12 = drawPlayer.GetImmuneAlpha(Color.White);
				}
				else
				{
					color = buffColor(color, num, num2, num3, num4);
					color2 = buffColor(color2, num, num2, num3, num4);
					color3 = buffColor(color3, num, num2, num3, num4);
					color4 = buffColor(color4, num, num2, num3, num4);
					color5 = buffColor(color5, num, num2, num3, num4);
					color6 = buffColor(color6, num, num2, num3, num4);
					color7 = buffColor(color7, num, num2, num3, num4);
					color8 = buffColor(color8, num, num2, num3, num4);
					color9 = buffColor(color9, num, num2, num3, num4);
					color10 = buffColor(color10, num, num2, num3, num4);
					color11 = buffColor(color11, num, num2, num3, num4);
					color12 = buffColor(color12, num, num2, num3, num4);
				}
			}
			if (drawPlayer.gravDir == 1f)
			{
				if (drawPlayer.direction == 1)
				{
					spriteEffects = SpriteEffects.None;
					spriteEffects2 = SpriteEffects.None;
				}
				else
				{
					spriteEffects = SpriteEffects.FlipHorizontally;
					spriteEffects2 = SpriteEffects.FlipHorizontally;
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 0f;
					drawPlayer.headPosition.Y = 0f;
					drawPlayer.bodyPosition.Y = 0f;
				}
			}
			else
			{
				if (drawPlayer.direction == 1)
				{
					spriteEffects = SpriteEffects.FlipVertically;
					spriteEffects2 = SpriteEffects.FlipVertically;
				}
				else
				{
					spriteEffects = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
					spriteEffects2 = (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);
				}
				if (!drawPlayer.dead)
				{
					drawPlayer.legPosition.Y = 6f;
					drawPlayer.headPosition.Y = 6f;
					drawPlayer.bodyPosition.Y = 6f;
				}
			}
			Vector2 vector = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.75f);
			Vector2 origin = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.5f);
			Vector2 vector2 = new Vector2((float)drawPlayer.legFrame.Width * 0.5f, (float)drawPlayer.legFrame.Height * 0.4f);
			bool flag3 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeWings", false, drawPlayer, spriteBatch, flag3, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn3 = Codable.customMethodRefReturn;
				flag3 = (bool)customMethodRefReturn3[2];
				color11 = (Color)customMethodRefReturn3[3];
			}
			if (flag3 && drawPlayer.wings > 0)
			{
				spriteBatch.Draw(wingsTexture[drawPlayer.wings], new Vector2((int)(drawPlayer.position.X - screenPosition.X + (float)(drawPlayer.width / 2) - (float)(9 * drawPlayer.direction)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)(drawPlayer.height / 2) + 2f * drawPlayer.gravDir)), new Rectangle(0, wingsTexture[drawPlayer.wings].Height / 4 * drawPlayer.wingFrame, wingsTexture[drawPlayer.wings].Width, wingsTexture[drawPlayer.wings].Height / 4), color11, drawPlayer.bodyRotation, new Vector2(wingsTexture[drawPlayer.wings].Width / 2, wingsTexture[drawPlayer.wings].Height / 8), 1f, spriteEffects, 0f);
			}
			if (Codable.RunPlayerMethodRef("DrawAfterWings", false, drawPlayer, spriteBatch, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn4 = Codable.customMethodRefReturn;
				color11 = (Color)customMethodRefReturn4[2];
			}
			bool flag4 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeSkin", false, drawPlayer, spriteBatch, flag4, color5) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn5 = Codable.customMethodRefReturn;
				flag4 = (bool)customMethodRefReturn5[2];
				color5 = (Color)customMethodRefReturn5[3];
			}
			if (flag4 && !drawPlayer.invis)
			{
				spriteBatch.Draw(skinBodyTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color5, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
				spriteBatch.Draw(skinLegsTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.legFrame, color5, drawPlayer.legRotation, origin, 1f, spriteEffects, 0f);
			}
			if (Codable.RunPlayerMethodRef("DrawAfterSkin", false, drawPlayer, spriteBatch, color5) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn6 = Codable.customMethodRefReturn;
				color5 = (Color)customMethodRefReturn6[2];
			}
			bool flag5 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeLegs", false, drawPlayer, spriteBatch, flag5, color8, color9, color12) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn7 = Codable.customMethodRefReturn;
				flag5 = (bool)customMethodRefReturn7[2];
				color8 = (Color)customMethodRefReturn7[3];
				color9 = (Color)customMethodRefReturn7[4];
				color12 = (Color)customMethodRefReturn7[5];
			}
			if (flag5)
			{
				if (armorLegTexture[drawPlayer.legs] != null)
				{
					spriteBatch.Draw(armorLegTexture[drawPlayer.legs], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)) + drawPlayer.legPosition + vector, drawPlayer.legFrame, color12, drawPlayer.legRotation, vector, 1f, spriteEffects, 0f);
				}
				else if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						spriteBatch.Draw(femalePantsTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)) + drawPlayer.legPosition + vector, drawPlayer.legFrame, color8, drawPlayer.legRotation, vector, 1f, spriteEffects, 0f);
						spriteBatch.Draw(femaleShoesTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)) + drawPlayer.legPosition + vector, drawPlayer.legFrame, color9, drawPlayer.legRotation, vector, 1f, spriteEffects, 0f);
					}
					else
					{
						spriteBatch.Draw(playerPantsTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)) + drawPlayer.legPosition + vector, drawPlayer.legFrame, color8, drawPlayer.legRotation, vector, 1f, spriteEffects, 0f);
						spriteBatch.Draw(playerShoesTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.legFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.legFrame.Height + 4f)) + drawPlayer.legPosition + vector, drawPlayer.legFrame, color9, drawPlayer.legRotation, vector, 1f, spriteEffects, 0f);
					}
				}
			}
			if (Codable.RunPlayerMethodRef("DrawAfterLegs", false, drawPlayer, spriteBatch, color8, color9, color12) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn8 = Codable.customMethodRefReturn;
				color8 = (Color)customMethodRefReturn8[2];
				color9 = (Color)customMethodRefReturn8[3];
				color12 = (Color)customMethodRefReturn8[4];
			}
			bool flag6 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeBody", false, drawPlayer, spriteBatch, flag6, color5, color7, color6, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn9 = Codable.customMethodRefReturn;
				flag6 = (bool)customMethodRefReturn9[2];
				color5 = (Color)customMethodRefReturn9[3];
				color7 = (Color)customMethodRefReturn9[4];
				color6 = (Color)customMethodRefReturn9[5];
				color11 = (Color)customMethodRefReturn9[6];
			}
			if (flag6)
			{
				if (armorBodyTexture[drawPlayer.body] != null)
				{
					if (!drawPlayer.male)
					{
						spriteBatch.Draw(femaleBodyTexture[drawPlayer.body], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color11, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					else
					{
						spriteBatch.Draw(armorBodyTexture[drawPlayer.body], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color11, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					if ((Config.hasHands[drawPlayer.body] || drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
					{
						spriteBatch.Draw(playerHandsTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color5, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
				}
				else if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						spriteBatch.Draw(femaleUnderShirtTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color7, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
						spriteBatch.Draw(femaleShirtTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color6, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					else
					{
						spriteBatch.Draw(playerUnderShirtTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color7, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
						spriteBatch.Draw(playerShirtTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color6, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					spriteBatch.Draw(playerHandsTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color5, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
				}
			}
			if (Codable.RunPlayerMethodRef("DrawAfterBody", false, drawPlayer, spriteBatch, color5, color7, color6, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn10 = Codable.customMethodRefReturn;
				color5 = (Color)customMethodRefReturn10[2];
				color7 = (Color)customMethodRefReturn10[3];
				color6 = (Color)customMethodRefReturn10[4];
				color11 = (Color)customMethodRefReturn10[5];
			}
			bool flag7 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeHead", false, drawPlayer, spriteBatch, flag7, color4, color, color2, color3, color10) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn11 = Codable.customMethodRefReturn;
				flag7 = (bool)customMethodRefReturn11[2];
				color4 = (Color)customMethodRefReturn11[3];
				color = (Color)customMethodRefReturn11[4];
				color2 = (Color)customMethodRefReturn11[5];
				color3 = (Color)customMethodRefReturn11[6];
				color10 = (Color)customMethodRefReturn11[7];
			}
			if (flag7)
			{
				if (!drawPlayer.invis && drawPlayer.head != 38)
				{
					spriteBatch.Draw(playerHeadTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color4, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					spriteBatch.Draw(playerEyeWhitesTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					spriteBatch.Draw(playerEyesTexture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color2, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
				}
				if (drawPlayer.head == 10 || drawPlayer.head == 12 || drawPlayer.head == 28)
				{
					spriteBatch.Draw(armorHeadTexture[drawPlayer.head], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color10, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					if (!drawPlayer.invis)
					{
						Rectangle bodyFrame = drawPlayer.bodyFrame;
						bodyFrame.Y -= 336;
						if (bodyFrame.Y < 0)
						{
							bodyFrame.Y = 0;
						}
						spriteBatch.Draw(playerHairTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					}
				}
				if (drawPlayer.head == 14 || drawPlayer.head == 15 || drawPlayer.head == 16 || drawPlayer.head == 18 || drawPlayer.head == 21 || drawPlayer.head == 24 || drawPlayer.head == 25 || drawPlayer.head == 26 || drawPlayer.head == 40 || drawPlayer.head == 44)
				{
					Rectangle bodyFrame2 = drawPlayer.bodyFrame;
					bodyFrame2.Y -= 336;
					if (bodyFrame2.Y < 0)
					{
						bodyFrame2.Y = 0;
					}
					if (!drawPlayer.invis)
					{
						spriteBatch.Draw(playerHairAltTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame2, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					}
				}
				if (drawPlayer.head == 23)
				{
					Rectangle bodyFrame3 = drawPlayer.bodyFrame;
					bodyFrame3.Y -= 336;
					if (bodyFrame3.Y < 0)
					{
						bodyFrame3.Y = 0;
					}
					if (!drawPlayer.invis)
					{
						spriteBatch.Draw(playerHairTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame3, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					}
					spriteBatch.Draw(armorHeadTexture[drawPlayer.head], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color10, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
				}
				else if (drawPlayer.head == 14)
				{
					Rectangle bodyFrame4 = drawPlayer.bodyFrame;
					int num9 = 0;
					if (bodyFrame4.Y == bodyFrame4.Height * 6)
					{
						bodyFrame4.Height -= 2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 7)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 8)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 9)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 10)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 13)
					{
						bodyFrame4.Height -= 2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 14)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 15)
					{
						num9 = -2;
					}
					else if (bodyFrame4.Y == bodyFrame4.Height * 16)
					{
						num9 = -2;
					}
					bodyFrame4.Y += num9;
					spriteBatch.Draw(armorHeadTexture[drawPlayer.head], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f + (float)num9)) + drawPlayer.headPosition + vector2, bodyFrame4, color10, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
				}
				else if ((armorHeadTexture[drawPlayer.head] != null || (drawPlayer.head > 0 && drawPlayer.head < 45)) && drawPlayer.head != 28)
				{
					if (Config.drawHair[drawPlayer.head] == 2 && !drawPlayer.invis)
					{
						Rectangle bodyFrame5 = drawPlayer.bodyFrame;
						bodyFrame5.Y -= 336;
						if (bodyFrame5.Y < 0)
						{
							bodyFrame5.Y = 0;
						}
						if (Config.drawHairAlt[drawPlayer.head])
						{
							spriteBatch.Draw(playerHairAltTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame5, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
						}
						else
						{
							spriteBatch.Draw(playerHairTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame5, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
						}
					}
					spriteBatch.Draw(armorHeadTexture[drawPlayer.head], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, drawPlayer.bodyFrame, color10, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
					if (Config.drawHair[drawPlayer.head] == 1 && !drawPlayer.invis)
					{
						Rectangle bodyFrame6 = drawPlayer.bodyFrame;
						bodyFrame6.Y -= 336;
						if (bodyFrame6.Y < 0)
						{
							bodyFrame6.Y = 0;
						}
						if (Config.drawHairAlt[drawPlayer.head])
						{
							spriteBatch.Draw(playerHairAltTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame6, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
						}
						else
						{
							spriteBatch.Draw(playerHairTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame6, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
						}
					}
				}
				else if (!drawPlayer.invis)
				{
					Rectangle bodyFrame7 = drawPlayer.bodyFrame;
					bodyFrame7.Y -= 336;
					if (bodyFrame7.Y < 0)
					{
						bodyFrame7.Y = 0;
					}
					spriteBatch.Draw(playerHairTexture[drawPlayer.hair], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.headPosition + vector2, bodyFrame7, color3, drawPlayer.headRotation, vector2, 1f, spriteEffects, 0f);
				}
			}
			if (Codable.RunPlayerMethodRef("DrawAfterHead", false, drawPlayer, spriteBatch, color4, color, color2, color3, color10) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn12 = Codable.customMethodRefReturn;
				color4 = (Color)customMethodRefReturn12[2];
				color = (Color)customMethodRefReturn12[3];
				color2 = (Color)customMethodRefReturn12[4];
				color3 = (Color)customMethodRefReturn12[5];
				color10 = (Color)customMethodRefReturn12[6];
			}
			Color color13 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
			bool flag8 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeWeapon", false, drawPlayer, spriteBatch, flag8, color13) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn13 = Codable.customMethodRefReturn;
				flag8 = (bool)customMethodRefReturn13[2];
				color13 = (Color)customMethodRefReturn13[3];
			}
			if (flag8)
			{
				try
				{
					if (drawPlayer.heldProj >= 0)
					{
						DrawProj(drawPlayer.heldProj);
					}
				}
				catch (Exception)
				{
				}
				if ((drawPlayer.itemAnimation > 0 || drawPlayer.inventory[drawPlayer.selectedItem].holdStyle > 0) && drawPlayer.inventory[drawPlayer.selectedItem].type > 0 && !drawPlayer.dead && !drawPlayer.inventory[drawPlayer.selectedItem].noUseGraphic && (!drawPlayer.wet || !drawPlayer.inventory[drawPlayer.selectedItem].noWet))
				{
					int type = drawPlayer.inventory[drawPlayer.selectedItem].type;
					int value = type;
					try
					{
						if (Config.itemDefs.drawPretendType.TryGetValue(drawPlayer.inventory[drawPlayer.selectedItem].name, out value))
						{
							drawPlayer.inventory[drawPlayer.selectedItem].type = value;
						}
					}
					catch
					{
					}
					if (drawPlayer.inventory[drawPlayer.selectedItem].useStyle == 5)
					{
						int shootOff = 10;
						Vector2 ShootCenter = new Vector2(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width / 2, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2);
						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 95)
						{
							shootOff = 10;
							ShootCenter.Y += 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 96)
						{
							shootOff = -5;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 98)
						{
							shootOff = -5;
							ShootCenter.Y -= 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 534)
						{
							shootOff = -2;
							ShootCenter.Y += 1f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 533)
						{
							shootOff = -7;
							ShootCenter.Y -= 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 506)
						{
							shootOff = 0;
							ShootCenter.Y -= 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 494 || drawPlayer.inventory[drawPlayer.selectedItem].type == 508)
						{
							shootOff = -2;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 434)
						{
							shootOff = 0;
							ShootCenter.Y -= 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 514)
						{
							shootOff = 0;
							ShootCenter.Y += 3f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 435 || drawPlayer.inventory[drawPlayer.selectedItem].type == 436 || drawPlayer.inventory[drawPlayer.selectedItem].type == 481 || drawPlayer.inventory[drawPlayer.selectedItem].type == 578)
						{
							shootOff = -2;
							ShootCenter.Y -= 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 197)
						{
							shootOff = -5;
							ShootCenter.Y += 4f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 126)
						{
							shootOff = 4;
							ShootCenter.Y += 4f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 127)
						{
							shootOff = 4;
							ShootCenter.Y += 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 157)
						{
							shootOff = 6;
							ShootCenter.Y += 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 160)
						{
							shootOff = -8;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 164 || drawPlayer.inventory[drawPlayer.selectedItem].type == 219)
						{
							shootOff = 2;
							ShootCenter.Y += 4f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 165 || drawPlayer.inventory[drawPlayer.selectedItem].type == 272)
						{
							shootOff = 4;
							ShootCenter.Y += 4f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 266)
						{
							shootOff = 0;
							ShootCenter.Y += 2f * drawPlayer.gravDir;
						}
						else if (drawPlayer.inventory[drawPlayer.selectedItem].type == 281)
						{
							shootOff = 6;
							ShootCenter.Y -= 6f * drawPlayer.gravDir;
						}
						drawPlayer.inventory[drawPlayer.selectedItem].DrawBeforeShooting(drawPlayer, spriteBatch, ref shootOff, ref ShootCenter);
						Vector2 origin2 = new Vector2(0f - (float)shootOff, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2);
						if (drawPlayer.direction == -1)
						{
							origin2 = new Vector2(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width + shootOff, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height / 2);
						}
						spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X + ShootCenter.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y + ShootCenter.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X + ShootCenter.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y + ShootCenter.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, origin2, drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						}
					}
					else if (drawPlayer.gravDir == -1f)
					{
						spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, 0f), drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						}
					}
					else
					{
						if (drawPlayer.inventory[drawPlayer.selectedItem].type == 425 || drawPlayer.inventory[drawPlayer.selectedItem].type == 507)
						{
							spriteEffects2 = ((drawPlayer.gravDir == 1f) ? ((drawPlayer.direction != 1) ? (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically) : SpriteEffects.FlipVertically) : ((drawPlayer.direction != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None));
						}
						spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetAlpha(color13), drawPlayer.itemRotation, new Vector2((float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						if (drawPlayer.inventory[drawPlayer.selectedItem].color != default(Color))
						{
							spriteBatch.Draw(itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type], new Vector2((int)(drawPlayer.itemLocation.X - screenPosition.X), (int)(drawPlayer.itemLocation.Y - screenPosition.Y)), new Rectangle(0, 0, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].GetColor(color13), drawPlayer.itemRotation, new Vector2((float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f - (float)itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Width * 0.5f * (float)drawPlayer.direction, itemTexture[drawPlayer.inventory[drawPlayer.selectedItem].type].Height), drawPlayer.inventory[drawPlayer.selectedItem].scale, spriteEffects2, 0f);
						}
					}
					try
					{
						if (type != value)
						{
							drawPlayer.inventory[drawPlayer.selectedItem].type = type;
						}
					}
					catch
					{
					}
				}
			}
			if (Codable.RunPlayerMethodRef("DrawAfterWeapon", false, drawPlayer, spriteBatch, color13) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn14 = Codable.customMethodRefReturn;
				color13 = (Color)customMethodRefReturn14[2];
			}
			bool flag9 = true;
			if (Codable.RunPlayerMethodRef("DrawBeforeArms", false, drawPlayer, spriteBatch, flag9, color5, color7, color6, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn15 = Codable.customMethodRefReturn;
				flag9 = (bool)customMethodRefReturn15[2];
				color5 = (Color)customMethodRefReturn15[3];
				color7 = (Color)customMethodRefReturn15[4];
				color6 = (Color)customMethodRefReturn15[5];
				color11 = (Color)customMethodRefReturn15[6];
			}
			if (flag9)
			{
				if (armorArmTexture[drawPlayer.body] != null)
				{
					spriteBatch.Draw(armorArmTexture[drawPlayer.body], new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color11, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					if ((Config.hasHands[drawPlayer.body] || drawPlayer.body == 10 || drawPlayer.body == 11 || drawPlayer.body == 12 || drawPlayer.body == 13 || drawPlayer.body == 14 || drawPlayer.body == 15 || drawPlayer.body == 16 || drawPlayer.body == 20) && !drawPlayer.invis)
					{
						spriteBatch.Draw(playerHands2Texture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color5, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
				}
				else if (!drawPlayer.invis)
				{
					if (!drawPlayer.male)
					{
						spriteBatch.Draw(femaleUnderShirt2Texture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color7, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
						spriteBatch.Draw(femaleShirt2Texture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color6, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					else
					{
						spriteBatch.Draw(playerUnderShirt2Texture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color7, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
					}
					spriteBatch.Draw(playerHands2Texture, new Vector2((int)(drawPlayer.position.X - screenPosition.X - (float)(drawPlayer.bodyFrame.Width / 2) + (float)(drawPlayer.width / 2)), (int)(drawPlayer.position.Y - screenPosition.Y + (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height + 4f)) + drawPlayer.bodyPosition + new Vector2(drawPlayer.bodyFrame.Width / 2, drawPlayer.bodyFrame.Height / 2), drawPlayer.bodyFrame, color5, drawPlayer.bodyRotation, origin, 1f, spriteEffects, 0f);
				}
			}
			if (Codable.RunPlayerMethodRef("DrawAfterArms", false, drawPlayer, spriteBatch, color5, color7, color6, color11) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn16 = Codable.customMethodRefReturn;
				color5 = (Color)customMethodRefReturn16[2];
				color7 = (Color)customMethodRefReturn16[3];
				color6 = (Color)customMethodRefReturn16[4];
				color11 = (Color)customMethodRefReturn16[5];
			}
			Codable.RunPlayerMethod("PostDraw", true, drawPlayer, spriteBatch);
		}

		private static void HelpText()
		{
			bool flag = false;
			if (player[myPlayer].statLifeMax2 > 100)
			{
				flag = true;
			}
			bool flag2 = false;
			if (player[myPlayer].statManaMax > 0)
			{
				flag2 = true;
			}
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			for (int i = 0; i < 48; i++)
			{
				if (player[myPlayer].inventory[i].pick > 0 && player[myPlayer].inventory[i].name != "Copper Pickaxe")
				{
					flag3 = false;
				}
				if (player[myPlayer].inventory[i].axe > 0 && player[myPlayer].inventory[i].name != "Copper Axe")
				{
					flag3 = false;
				}
				if (player[myPlayer].inventory[i].hammer > 0)
				{
					flag3 = false;
				}
				if (player[myPlayer].inventory[i].type == 11 || player[myPlayer].inventory[i].type == 12 || player[myPlayer].inventory[i].type == 13 || player[myPlayer].inventory[i].type == 14)
				{
					flag4 = true;
				}
				if (player[myPlayer].inventory[i].type == 19 || player[myPlayer].inventory[i].type == 20 || player[myPlayer].inventory[i].type == 21 || player[myPlayer].inventory[i].type == 22)
				{
					flag5 = true;
				}
				if (player[myPlayer].inventory[i].type == 75)
				{
					flag6 = true;
				}
				if (player[myPlayer].inventory[i].type == 75)
				{
					flag7 = true;
				}
				if (player[myPlayer].inventory[i].type == 68 || player[myPlayer].inventory[i].type == 70)
				{
					flag8 = true;
				}
				if (player[myPlayer].inventory[i].type == 84)
				{
					flag9 = true;
				}
			}
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			bool flag16 = false;
			bool flag17 = false;
			bool flag18 = false;
			for (int j = 0; j < 200; j++)
			{
				if (npc[j].active)
				{
					if (npc[j].type == 17)
					{
						flag10 = true;
					}
					if (npc[j].type == 18)
					{
						flag11 = true;
					}
					if (npc[j].type == 19)
					{
						flag13 = true;
					}
					if (npc[j].type == 20)
					{
						flag12 = true;
					}
					if (npc[j].type == 54)
					{
						flag18 = true;
					}
					if (npc[j].type == 124)
					{
						flag15 = true;
					}
					if (npc[j].type == 107)
					{
						flag14 = true;
					}
					if (npc[j].type == 108)
					{
						flag16 = true;
					}
					if (npc[j].type == 38)
					{
						flag17 = true;
					}
				}
			}
			while (true)
			{
				helpText++;
				if (flag3)
				{
					if (helpText == 1)
					{
						npcChatText = Lang.dialog(177);
						return;
					}
					if (helpText == 2)
					{
						npcChatText = Lang.dialog(178);
						return;
					}
					if (helpText == 3)
					{
						npcChatText = Lang.dialog(179);
						return;
					}
					if (helpText == 4)
					{
						npcChatText = Lang.dialog(180);
						return;
					}
					if (helpText == 5)
					{
						npcChatText = Lang.dialog(181);
						return;
					}
					if (helpText == 6)
					{
						npcChatText = Lang.dialog(182);
						return;
					}
				}
				if (!flag3 || flag4 || flag5 || helpText != 11)
				{
					if (flag3 && flag4 && !flag5)
					{
						if (helpText == 21)
						{
							npcChatText = Lang.dialog(184);
							return;
						}
						if (helpText == 22)
						{
							npcChatText = Lang.dialog(185);
							return;
						}
					}
					if (flag3 && flag5)
					{
						if (helpText == 31)
						{
							npcChatText = Lang.dialog(186);
							return;
						}
						if (helpText == 32)
						{
							npcChatText = Lang.dialog(187);
							return;
						}
					}
					if (flag || helpText != 41)
					{
						if (flag2 || helpText != 42)
						{
							if (flag2 || flag6 || helpText != 43)
							{
								if (!flag10 && !flag11)
								{
									if (helpText == 51)
									{
										npcChatText = Lang.dialog(191);
										return;
									}
									if (helpText == 52)
									{
										npcChatText = Lang.dialog(192);
										return;
									}
									if (helpText == 53)
									{
										npcChatText = Lang.dialog(193);
										return;
									}
									if (helpText == 54)
									{
										npcChatText = Lang.dialog(194);
										return;
									}
								}
								if (flag10 || helpText != 61)
								{
									if (flag11 || helpText != 62)
									{
										if (flag13 || helpText != 63)
										{
											if (flag12 || helpText != 64)
											{
												if (flag15 || helpText != 65 || !NPC.downedBoss3)
												{
													if (flag18 || helpText != 66 || !NPC.downedBoss3)
													{
														if (flag14 || helpText != 67)
														{
															if (flag17 || !NPC.downedBoss2 || helpText != 68)
															{
																if (flag16 || !hardMode || helpText != 69)
																{
																	if (!flag7 || helpText != 71)
																	{
																		if (!flag8 || helpText != 72)
																		{
																			if ((!flag7 && !flag8) || helpText != 80)
																			{
																				if (flag9 || helpText != 201 || hardMode || NPC.downedBoss3 || NPC.downedBoss2)
																				{
																					if (helpText != 1000 || NPC.downedBoss1 || NPC.downedBoss2)
																					{
																						if (helpText != 1001 || NPC.downedBoss1 || NPC.downedBoss2)
																						{
																							if (helpText != 1002 || NPC.downedBoss3)
																							{
																								if (helpText != 1050 || NPC.downedBoss1 || player[myPlayer].statLifeMax2 >= 200)
																								{
																									if (helpText != 1051 || NPC.downedBoss1 || player[myPlayer].statDefense > 10)
																									{
																										if (helpText != 1052 || NPC.downedBoss1 || player[myPlayer].statLifeMax2 < 200 || player[myPlayer].statDefense <= 10)
																										{
																											if (helpText != 1053 || !NPC.downedBoss1 || NPC.downedBoss2 || player[myPlayer].statLifeMax2 >= 300)
																											{
																												if (helpText != 1054 || !NPC.downedBoss1 || NPC.downedBoss2 || player[myPlayer].statLifeMax2 < 300)
																												{
																													if (helpText != 1055 || !NPC.downedBoss1 || NPC.downedBoss2 || player[myPlayer].statLifeMax2 < 300)
																													{
																														if (helpText != 1056 || !NPC.downedBoss1 || !NPC.downedBoss2 || NPC.downedBoss3)
																														{
																															if (helpText != 1057 || !NPC.downedBoss1 || !NPC.downedBoss2 || !NPC.downedBoss3 || hardMode || player[myPlayer].statLifeMax2 >= 400)
																															{
																																if (helpText != 1058 || !NPC.downedBoss1 || !NPC.downedBoss2 || !NPC.downedBoss3 || hardMode || player[myPlayer].statLifeMax2 < 400)
																																{
																																	if (helpText != 1059 || !NPC.downedBoss1 || !NPC.downedBoss2 || !NPC.downedBoss3 || hardMode || player[myPlayer].statLifeMax2 < 400)
																																	{
																																		if (helpText != 1060 || !NPC.downedBoss1 || !NPC.downedBoss2 || !NPC.downedBoss3 || hardMode || player[myPlayer].statLifeMax2 < 400)
																																		{
																																			if (helpText != 1061 || !hardMode)
																																			{
																																				if (helpText == 1062 && hardMode)
																																				{
																																					break;
																																				}
																																				if (helpText > 1100)
																																				{
																																					helpText = 0;
																																				}
																																				continue;
																																			}
																																			npcChatText = Lang.dialog(222);
																																			return;
																																		}
																																		npcChatText = Lang.dialog(221);
																																		return;
																																	}
																																	npcChatText = Lang.dialog(220);
																																	return;
																																}
																																npcChatText = Lang.dialog(219);
																																return;
																															}
																															npcChatText = Lang.dialog(218);
																															return;
																														}
																														npcChatText = Lang.dialog(217);
																														return;
																													}
																													npcChatText = Lang.dialog(216);
																													return;
																												}
																												npcChatText = Lang.dialog(215);
																												return;
																											}
																											npcChatText = Lang.dialog(214);
																											return;
																										}
																										npcChatText = Lang.dialog(213);
																										return;
																									}
																									npcChatText = Lang.dialog(212);
																									return;
																								}
																								npcChatText = Lang.dialog(211);
																								return;
																							}
																							npcChatText = Lang.dialog(210);
																							return;
																						}
																						npcChatText = Lang.dialog(209);
																						return;
																					}
																					npcChatText = Lang.dialog(208);
																					return;
																				}
																				npcChatText = Lang.dialog(207);
																				return;
																			}
																			npcChatText = Lang.dialog(206);
																			return;
																		}
																		npcChatText = Lang.dialog(205);
																		return;
																	}
																	npcChatText = Lang.dialog(204);
																	return;
																}
																npcChatText = Lang.dialog(203);
																return;
															}
															npcChatText = Lang.dialog(202);
															return;
														}
														npcChatText = Lang.dialog(201);
														return;
													}
													npcChatText = Lang.dialog(200);
													return;
												}
												npcChatText = Lang.dialog(199);
												return;
											}
											npcChatText = Lang.dialog(198);
											return;
										}
										npcChatText = Lang.dialog(197);
										return;
									}
									npcChatText = Lang.dialog(196);
									return;
								}
								npcChatText = Lang.dialog(195);
								return;
							}
							npcChatText = Lang.dialog(190);
							return;
						}
						npcChatText = Lang.dialog(189);
						return;
					}
					npcChatText = Lang.dialog(188);
					return;
				}
				npcChatText = Lang.dialog(183);
				return;
			}
			npcChatText = Lang.dialog(223);
		}

		public void DrawChat()
		{
			if (player[myPlayer].talkNPC < 0 && player[myPlayer].sign == -1)
			{
				npcChatText = "";
				return;
			}
			if (netMode == 0 && autoPause && player[myPlayer].talkNPC >= 0)
			{
				if (npc[player[myPlayer].talkNPC].type == 105)
				{
					npc[player[myPlayer].talkNPC].Transform(107);
				}
				if (npc[player[myPlayer].talkNPC].type == 106)
				{
					npc[player[myPlayer].talkNPC].Transform(108);
				}
				if (npc[player[myPlayer].talkNPC].type == 123)
				{
					npc[player[myPlayer].talkNPC].Transform(124);
				}
			}
			Color color = new Color(200, 200, 200, 200);
			int num = (mouseTextColor * 2 + 255) / 3;
			Color color2 = new Color(num, num, num, num);
			int num2 = 10;
			int num3 = 0;
			string[] array = new string[num2];
			int num4 = 0;
			int num5 = 0;
			if (npcChatText == null)
			{
				npcChatText = "";
			}
			for (int i = 0; i < npcChatText.Length; i++)
			{
				byte[] bytes = Encoding.ASCII.GetBytes(npcChatText.Substring(i, 1));
				if (bytes[0] == 10)
				{
					array[num3] = npcChatText.Substring(num4, i - num4);
					num3++;
					num4 = i + 1;
					num5 = i + 1;
				}
				else if (npcChatText.Substring(i, 1) == " " || i == npcChatText.Length - 1)
				{
					if (fontMouseText.MeasureString(npcChatText.Substring(num4, i - num4)).X > 470f)
					{
						array[num3] = npcChatText.Substring(num4, num5 - num4);
						num3++;
						num4 = num5 + 1;
					}
					num5 = i;
				}
				if (num3 == 10)
				{
					npcChatText = npcChatText.Substring(0, i - 1);
					num4 = i - 1;
					num3 = 9;
					break;
				}
			}
			if (num3 < 10)
			{
				array[num3] = npcChatText.Substring(num4, npcChatText.Length - num4);
			}
			if (editSign)
			{
				textBlinkerCount++;
				if (textBlinkerCount >= 20)
				{
					if (textBlinkerState == 0)
					{
						textBlinkerState = 1;
					}
					else
					{
						textBlinkerState = 0;
					}
					textBlinkerCount = 0;
				}
				if (textBlinkerState == 1)
				{
					string[] array2;
					IntPtr value;
					(array2 = array)[(int)(value = (IntPtr)num3)] = array2[(int)value] + "|";
				}
			}
			num3++;
			spriteBatch.Draw(chatBackTexture, new Vector2(screenWidth / 2 - chatBackTexture.Width / 2, 100f), new Rectangle(0, 0, chatBackTexture.Width, (num3 + 1) * 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(chatBackTexture, new Vector2(screenWidth / 2 - chatBackTexture.Width / 2, 100 + (num3 + 1) * 30), new Rectangle(0, chatBackTexture.Height - 30, chatBackTexture.Width, 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
			for (int j = 0; j < num3; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					Color color3 = Color.Black;
					int num6 = 170 + (screenWidth - 800) / 2;
					int num7 = 120 + j * 30;
					if (k == 0)
					{
						num6 -= 2;
					}
					if (k == 1)
					{
						num6 += 2;
					}
					if (k == 2)
					{
						num7 -= 2;
					}
					if (k == 3)
					{
						num7 += 2;
					}
					if (k == 4)
					{
						color3 = color2;
					}
					spriteBatch.DrawString(fontMouseText, array[j], new Vector2(num6, num7), color3, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			num = mouseTextColor;
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			string text = "";
			string text2 = "";
			int num8 = player[myPlayer].statLifeMax2 - player[myPlayer].statLife;
			for (int l = 0; l < player[myPlayer].buffType.Length; l++)
			{
				int num9 = player[myPlayer].buffType[l];
				if (debuff[num9] && player[myPlayer].buffTime[l] > 0 && num9 != 28 && num9 != 34)
				{
					num8 += 1000;
				}
			}
			if (player[myPlayer].sign > -1)
			{
				text = ((!editSign) ? Lang.inter[48] : Lang.inter[47]);
			}
			else
			{
				if (npc[player[myPlayer].talkNPC].type == 20)
				{
					text = Lang.inter[28];
					text2 = Lang.inter[49];
				}
				else if (npc[player[myPlayer].talkNPC].type == 17 || npc[player[myPlayer].talkNPC].type == 19 || npc[player[myPlayer].talkNPC].type == 38 || npc[player[myPlayer].talkNPC].type == 54 || npc[player[myPlayer].talkNPC].type == 107 || npc[player[myPlayer].talkNPC].type == 108 || npc[player[myPlayer].talkNPC].type == 124 || npc[player[myPlayer].talkNPC].type == 142 || (npc[player[myPlayer].talkNPC].subclass != null && npc[player[myPlayer].talkNPC].subclass.GetType().GetMethod("SetupShop") != null))
				{
					text = Lang.inter[28];
					if (npc[player[myPlayer].talkNPC].type == 107)
					{
						text2 = Lang.inter[19];
					}
				}
				else if (npc[player[myPlayer].talkNPC].type == 37)
				{
					if (!dayTime)
					{
						text = Lang.inter[50];
					}
				}
				else if (npc[player[myPlayer].talkNPC].type == 22)
				{
					text = Lang.inter[51];
					text2 = Lang.inter[25];
				}
				else if (npc[player[myPlayer].talkNPC].type == 18)
				{
					string text3 = "";
					int num10 = 0;
					int num11 = 0;
					int num12 = 0;
					int num13 = 0;
					int num14 = num8;
					if (num14 > 0)
					{
						num14 = (int)((double)num14 * 0.75);
						if (num14 < 1)
						{
							num14 = 1;
						}
					}
					if (num14 < 0)
					{
						num14 = 0;
					}
					num8 = num14;
					if (num14 >= 1000000)
					{
						num10 = num14 / 1000000;
						num14 -= num10 * 1000000;
					}
					if (num14 >= 10000)
					{
						num11 = num14 / 10000;
						num14 -= num11 * 10000;
					}
					if (num14 >= 100)
					{
						num12 = num14 / 100;
						num14 -= num12 * 100;
					}
					if (num14 >= 1)
					{
						num13 = num14;
					}
					if (num10 > 0)
					{
						object obj = text3;
						text3 = string.Concat(obj, num10, " ", Lang.inter[15], " ");
					}
					if (num11 > 0)
					{
						object obj2 = text3;
						text3 = string.Concat(obj2, num11, " ", Lang.inter[16], " ");
					}
					if (num12 > 0)
					{
						object obj3 = text3;
						text3 = string.Concat(obj3, num12, " ", Lang.inter[17], " ");
					}
					if (num13 > 0)
					{
						object obj4 = text3;
						text3 = string.Concat(obj4, num13, " ", Lang.inter[18], " ");
					}
					float num15 = (float)(int)mouseTextColor / 255f;
					if (num10 > 0)
					{
						color2 = new Color((byte)(220f * num15), (byte)(220f * num15), (byte)(198f * num15), mouseTextColor);
					}
					else if (num11 > 0)
					{
						color2 = new Color((byte)(224f * num15), (byte)(201f * num15), (byte)(92f * num15), mouseTextColor);
					}
					else if (num12 > 0)
					{
						color2 = new Color((byte)(181f * num15), (byte)(192f * num15), (byte)(193f * num15), mouseTextColor);
					}
					else if (num13 > 0)
					{
						color2 = new Color((byte)(246f * num15), (byte)(138f * num15), (byte)(96f * num15), mouseTextColor);
					}
					text = Lang.inter[54] + " (" + text3 + ")";
					if (num14 == 0)
					{
						text = Lang.inter[54];
					}
				}
				string text4 = "";
				if (npc[player[myPlayer].talkNPC].ShopOverFuncName != null)
				{
					text4 = npc[player[myPlayer].talkNPC].ShopOverFuncName();
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text = text4;
				}
				if (npc[player[myPlayer].talkNPC].ChatFuncName != null)
				{
					text2 = npc[player[myPlayer].talkNPC].ChatFuncName();
				}
				if (npc[player[myPlayer].talkNPC].KindChatFuncName != null)
				{
					text4 = npc[player[myPlayer].talkNPC].KindChatFuncName();
				}
				if (!string.IsNullOrEmpty(text4))
				{
					text2 = text4;
				}
			}
			int num16 = 180 + (screenWidth - 800) / 2;
			int num17 = 130 + num3 * 30;
			float scale = 0.9f;
			if (mouseX > num16 && (float)mouseX < (float)num16 + fontMouseText.MeasureString(text).X && mouseY > num17 && (float)mouseY < (float)num17 + fontMouseText.MeasureString(text).Y)
			{
				player[myPlayer].mouseInterface = true;
				scale = 1.1f;
				if (!npcChatFocus2)
				{
					PlaySound(12);
				}
				npcChatFocus2 = true;
				player[myPlayer].releaseUseItem = false;
			}
			else
			{
				if (npcChatFocus2)
				{
					PlaySound(12);
				}
				npcChatFocus2 = false;
			}
			for (int m = 0; m < 5; m++)
			{
				int num18 = num16;
				int num19 = num17;
				Color color4 = Color.Black;
				if (m == 0)
				{
					num18 -= 2;
				}
				if (m == 1)
				{
					num18 += 2;
				}
				if (m == 2)
				{
					num19 -= 2;
				}
				if (m == 3)
				{
					num19 += 2;
				}
				if (m == 4)
				{
					color4 = color2;
				}
				Vector2 origin = fontMouseText.MeasureString(text);
				origin *= 0.5f;
				spriteBatch.DrawString(fontMouseText, text, new Vector2((float)num18 + origin.X, (float)num19 + origin.Y), color4, 0f, origin, scale, SpriteEffects.None, 0f);
			}
			string text5 = Lang.inter[52];
			color2 = new Color(num, (int)((double)num / 1.1), num / 2, num);
			num16 = num16 + (int)fontMouseText.MeasureString(text).X + 20;
			int num20 = num16 + (int)fontMouseText.MeasureString(text5).X;
			num17 = 130 + num3 * 30;
			scale = 0.9f;
			if (mouseX > num16 && (float)mouseX < (float)num16 + fontMouseText.MeasureString(text5).X && mouseY > num17 && (float)mouseY < (float)num17 + fontMouseText.MeasureString(text5).Y)
			{
				scale = 1.1f;
				if (!npcChatFocus1)
				{
					PlaySound(12);
				}
				npcChatFocus1 = true;
				player[myPlayer].releaseUseItem = false;
				player[myPlayer].controlUseItem = false;
			}
			else
			{
				if (npcChatFocus1)
				{
					PlaySound(12);
				}
				npcChatFocus1 = false;
			}
			for (int n = 0; n < 5; n++)
			{
				int num21 = num16;
				int num22 = num17;
				Color color5 = Color.Black;
				if (n == 0)
				{
					num21 -= 2;
				}
				if (n == 1)
				{
					num21 += 2;
				}
				if (n == 2)
				{
					num22 -= 2;
				}
				if (n == 3)
				{
					num22 += 2;
				}
				if (n == 4)
				{
					color5 = color2;
				}
				Vector2 origin2 = fontMouseText.MeasureString(text5);
				origin2 *= 0.5f;
				spriteBatch.DrawString(fontMouseText, text5, new Vector2((float)num21 + origin2.X, (float)num22 + origin2.Y), color5, 0f, origin2, scale, SpriteEffects.None, 0f);
			}
			if (text2 != "")
			{
				num16 = num20 + (int)fontMouseText.MeasureString(text2).X / 3;
				num17 = 130 + num3 * 30;
				scale = 0.9f;
				if (mouseX > num16 && (float)mouseX < (float)num16 + fontMouseText.MeasureString(text2).X && mouseY > num17 && (float)mouseY < (float)num17 + fontMouseText.MeasureString(text2).Y)
				{
					player[myPlayer].mouseInterface = true;
					scale = 1.1f;
					if (!npcChatFocus3)
					{
						PlaySound(12);
					}
					npcChatFocus3 = true;
					player[myPlayer].releaseUseItem = false;
				}
				else
				{
					if (npcChatFocus3)
					{
						PlaySound(12);
					}
					npcChatFocus3 = false;
				}
				for (int num23 = 0; num23 < 5; num23++)
				{
					int num24 = num16;
					int num25 = num17;
					Color color6 = Color.Black;
					if (num23 == 0)
					{
						num24 -= 2;
					}
					if (num23 == 1)
					{
						num24 += 2;
					}
					if (num23 == 2)
					{
						num25 -= 2;
					}
					if (num23 == 3)
					{
						num25 += 2;
					}
					if (num23 == 4)
					{
						color6 = color2;
					}
					Vector2 origin3 = fontMouseText.MeasureString(text);
					origin3 *= 0.5f;
					spriteBatch.DrawString(fontMouseText, text2, new Vector2((float)num24 + origin3.X, (float)num25 + origin3.Y), color6, 0f, origin3, scale, SpriteEffects.None, 0f);
				}
			}
			if (!mouseLeft || !mouseLeftRelease)
			{
				return;
			}
			mouseLeftRelease = false;
			player[myPlayer].releaseUseItem = false;
			player[myPlayer].mouseInterface = true;
			if (npcChatFocus1)
			{
				player[myPlayer].talkNPC = -1;
				player[myPlayer].sign = -1;
				editSign = false;
				npcChatText = "";
				PlaySound(11);
			}
			else if (npcChatFocus2)
			{
				if (player[myPlayer].sign != -1)
				{
					if (!editSign)
					{
						PlaySound(12);
						editSign = true;
						clrInput();
						return;
					}
					PlaySound(12);
					int num26 = player[myPlayer].sign;
					Sign.TextSign(num26, npcChatText);
					editSign = false;
					if (netMode == 1)
					{
						NetMessage.SendData(47, -1, -1, "", num26);
					}
					return;
				}
				Chest chest = new Chest();
				for (int num27 = 0; num27 < Chest.maxItems; num27++)
				{
					chest.item[num27] = new Item();
				}
				if (npc[player[myPlayer].talkNPC].ShopOverFunc())
				{
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].SetupShop != null)
				{
					npc[player[myPlayer].talkNPC].SetupShop(chest);
					shop[npc[player[myPlayer].talkNPC].type] = chest;
					playerInventory = true;
					npcChatText = "";
					npcShop = npc[player[myPlayer].talkNPC].type;
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 17)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 1;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 19)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 2;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 124)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 8;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 142)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 9;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 37)
				{
					if (netMode == 0)
					{
						NPC.SpawnSkeletron();
					}
					else
					{
						NetMessage.SendData(51, -1, -1, "", myPlayer, 1f);
					}
					npcChatText = "";
				}
				else if (npc[player[myPlayer].talkNPC].type == 20)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 3;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 38)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 4;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 54)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 5;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 107)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 6;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 108)
				{
					playerInventory = true;
					npcChatText = "";
					npcShop = 7;
					shop[npcShop].SetupShop(npcShop);
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 22)
				{
					PlaySound(12);
					HelpText();
				}
				else
				{
					if (npc[player[myPlayer].talkNPC].type != 18)
					{
						return;
					}
					PlaySound(12);
					if (num8 > 0)
					{
						if (player[myPlayer].BuyItem(num8))
						{
							PlaySound(2, -1, -1, 4);
							player[myPlayer].HealEffect(player[myPlayer].statLifeMax2 - player[myPlayer].statLife);
							if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.25)
							{
								npcChatText = Lang.dialog(227);
							}
							else if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.5)
							{
								npcChatText = Lang.dialog(228);
							}
							else if ((double)player[myPlayer].statLife < (double)player[myPlayer].statLifeMax2 * 0.75)
							{
								npcChatText = Lang.dialog(229);
							}
							else
							{
								npcChatText = Lang.dialog(230);
							}
							player[myPlayer].statLife = player[myPlayer].statLifeMax2;
							for (int num28 = 0; num28 < player[myPlayer].buffType.Length; num28++)
							{
								int num29 = player[myPlayer].buffType[num28];
								if (debuff[num29] && player[myPlayer].buffTime[num28] > 0 && num29 != 28 && num29 != 34)
								{
									player[myPlayer].DelBuff(num28);
								}
							}
						}
						else
						{
							int num30 = rand.Next(3);
							if (num30 == 0)
							{
								npcChatText = Lang.dialog(52);
							}
							if (num30 == 1)
							{
								npcChatText = Lang.dialog(53);
							}
							if (num30 == 2)
							{
								npcChatText = Lang.dialog(54);
							}
						}
					}
					else
					{
						int num31 = rand.Next(3);
						if (num31 == 0)
						{
							npcChatText = Lang.dialog(55);
						}
						if (num31 == 1)
						{
							npcChatText = Lang.dialog(56);
						}
						if (num31 == 2)
						{
							npcChatText = Lang.dialog(57);
						}
					}
				}
			}
			else if (npcChatFocus3 && player[myPlayer].talkNPC >= 0)
			{
				if (!npc[player[myPlayer].talkNPC].KindChatFunc())
				{
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].ChatFunc != null)
				{
					npc[player[myPlayer].talkNPC].ChatFunc();
					PlaySound(12);
				}
				else if (npc[player[myPlayer].talkNPC].type == 20)
				{
					PlaySound(12);
					npcChatText = Lang.evilGood();
				}
				else if (npc[player[myPlayer].talkNPC].type == 22)
				{
					playerInventory = true;
					npcChatText = "";
					PlaySound(12);
					craftGuide = true;
				}
				else if (npc[player[myPlayer].talkNPC].type == 107)
				{
					playerInventory = true;
					npcChatText = "";
					PlaySound(12);
					reforge = true;
				}
			}
		}

		public static bool AccCheck(Item newItem, int slot)
		{
			if (player[myPlayer].armor[slot].IsTheSameAs(newItem))
			{
				return false;
			}
			if (newItem.AccCheck != null)
			{
				return !newItem.AccCheck(player[myPlayer], slot);
			}
			for (int i = 0; i < player[myPlayer].armor.Length; i++)
			{
				if (newItem.IsTheSameAs(player[myPlayer].armor[i]))
				{
					return true;
				}
			}
			return false;
		}

		public static Item armorSwap(Item newItem)
		{
			for (int i = 0; i < player[myPlayer].armor.Length; i++)
			{
				if (newItem.IsTheSameAs(player[myPlayer].armor[i]))
				{
					accSlotCount = i;
				}
			}
			if (newItem.headSlot == -1 && newItem.bodySlot == -1 && newItem.legSlot == -1 && !newItem.accessory)
			{
				return newItem;
			}
			Item item = newItem;
			if (newItem.headSlot != -1)
			{
				if (!newItem.CanEquip(player[myPlayer], 0))
				{
					return newItem;
				}
				item = (Item)player[myPlayer].armor[0].Clone();
				item.OnUnequip(player[myPlayer], 0);
				player[myPlayer].armor[0] = (Item)newItem.Clone();
				player[myPlayer].armor[0].OnEquip(player[myPlayer], 0);
			}
			else if (newItem.bodySlot != -1)
			{
				if (!newItem.CanEquip(player[myPlayer], 1))
				{
					return newItem;
				}
				item = (Item)player[myPlayer].armor[1].Clone();
				item.OnUnequip(player[myPlayer], 1);
				player[myPlayer].armor[1] = (Item)newItem.Clone();
				player[myPlayer].armor[1].OnEquip(player[myPlayer], 1);
			}
			else if (newItem.legSlot != -1)
			{
				if (!newItem.CanEquip(player[myPlayer], 2))
				{
					return newItem;
				}
				item = (Item)player[myPlayer].armor[2].Clone();
				item.OnUnequip(player[myPlayer], 2);
				player[myPlayer].armor[2] = (Item)newItem.Clone();
				player[myPlayer].armor[2].OnEquip(player[myPlayer], 2);
			}
			else if (newItem.accessory)
			{
				for (int j = 3; j < 8; j++)
				{
					if (player[myPlayer].armor[j].type == 0)
					{
						accSlotCount = j - 3;
						break;
					}
				}
				for (int k = 0; k < player[myPlayer].armor.Length; k++)
				{
					if (newItem.IsTheSameAs(player[myPlayer].armor[k]))
					{
						accSlotCount = k - 3;
					}
				}
				if (accSlotCount >= 5)
				{
					accSlotCount = 0;
				}
				if (accSlotCount < 0)
				{
					accSlotCount = 4;
				}
				if (!newItem.CanEquip(player[myPlayer], 3 + accSlotCount))
				{
					return newItem;
				}
				item = (Item)player[myPlayer].armor[3 + accSlotCount].Clone();
				item.OnUnequip(player[myPlayer], 3 + accSlotCount);
				player[myPlayer].armor[3 + accSlotCount] = (Item)newItem.Clone();
				player[myPlayer].armor[3 + accSlotCount].OnEquip(player[myPlayer], 3 + accSlotCount);
				accSlotCount++;
				if (accSlotCount >= 5)
				{
					accSlotCount = 0;
				}
			}
			PlaySound(7);
			Recipe.FindRecipes();
			return item;
		}

		public static void BankCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (player[myPlayer].bank[i].type < 71 || player[myPlayer].bank[i].type > 73 || player[myPlayer].bank[i].stack != player[myPlayer].bank[i].maxStack)
				{
					continue;
				}
				player[myPlayer].bank[i].SetDefaults(player[myPlayer].bank[i].type + 1);
				for (int j = 0; j < 20; j++)
				{
					if (j != i && player[myPlayer].bank[j].type == player[myPlayer].bank[i].type && player[myPlayer].bank[j].stack < player[myPlayer].bank[j].maxStack)
					{
						player[myPlayer].bank[j].stack++;
						player[myPlayer].bank[i].SetDefaults(0);
						BankCoins();
					}
				}
			}
		}

		public static void ChestCoins()
		{
			for (int i = 0; i < 20; i++)
			{
				if (chest[player[myPlayer].chest].item[i].type < 71 || chest[player[myPlayer].chest].item[i].type > 73 || chest[player[myPlayer].chest].item[i].stack != chest[player[myPlayer].chest].item[i].maxStack)
				{
					continue;
				}
				chest[player[myPlayer].chest].item[i].SetDefaults(chest[player[myPlayer].chest].item[i].type + 1);
				for (int j = 0; j < 20; j++)
				{
					if (j != i && chest[player[myPlayer].chest].item[j].type == chest[player[myPlayer].chest].item[i].type && chest[player[myPlayer].chest].item[j].stack < chest[player[myPlayer].chest].item[j].maxStack)
					{
						if (netMode == 1)
						{
							NetMessage.SendData(32, -1, -1, "", player[myPlayer].chest, j);
						}
						chest[player[myPlayer].chest].item[j].stack++;
						chest[player[myPlayer].chest].item[i].SetDefaults(0);
						ChestCoins();
					}
				}
			}
		}

		public void DrawNPCHouse()
		{
			for (int i = 0; i < 200; i++)
			{
				if (!npc[i].active || !npc[i].townNPC || npc[i].homeless || npc[i].homeTileX <= 0 || npc[i].homeTileY <= 0 || npc[i].type == 37 || npc[i].dontDrawFace)
				{
					continue;
				}
				int num = 1;
				int homeTileX = npc[i].homeTileX;
				int num2 = npc[i].homeTileY - 1;
				if (tile[homeTileX, num2] == null)
				{
					continue;
				}
				bool flag = false;
				while (!tile[homeTileX, num2].active || !tileSolid[tile[homeTileX, num2].type])
				{
					num2--;
					if (num2 < 10)
					{
						break;
					}
					if (tile[homeTileX, num2] == null)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
				int num3 = 8;
				int num4 = 18;
				if (tile[homeTileX, num2].type == 19)
				{
					num4 -= 8;
				}
				num2++;
				spriteBatch.Draw(bannerTexture[num], new Vector2(homeTileX * 16 - (int)screenPosition.X + num3, num2 * 16 - (int)screenPosition.Y + num4), new Rectangle(0, 0, bannerTexture[num].Width, bannerTexture[num].Height), Lighting.GetColor(homeTileX, num2), 0f, new Vector2(bannerTexture[num].Width / 2, bannerTexture[num].Height / 2), 1f, SpriteEffects.None, 0f);
				int i2 = NPC.TypeToNum(npc[i].type);
				float scale = 1f;
				float num5 = 0f;
				num5 = ((npcHeadTexture[i2].Width <= npcHeadTexture[i2].Height) ? ((float)npcHeadTexture[i2].Height) : ((float)npcHeadTexture[i2].Width));
				if (num5 > 24f)
				{
					scale = 24f / num5;
				}
				spriteBatch.Draw(npcHeadTexture[i2], new Vector2(homeTileX * 16 - (int)screenPosition.X + num3, num2 * 16 - (int)screenPosition.Y + num4 + 2), new Rectangle(0, 0, npcHeadTexture[i2].Width, npcHeadTexture[i2].Height), Lighting.GetColor(homeTileX, num2), 0f, new Vector2(npcHeadTexture[i2].Width / 2, npcHeadTexture[i2].Height / 2), scale, SpriteEffects.None, 0f);
				homeTileX = homeTileX * 16 - (int)screenPosition.X + num3 - bannerTexture[num].Width / 2;
				num2 = num2 * 16 - (int)screenPosition.Y + num4 - bannerTexture[num].Height / 2;
				if (mouseX >= homeTileX && mouseX <= homeTileX + bannerTexture[num].Width && mouseY >= num2 && mouseY <= num2 + bannerTexture[num].Height)
				{
					MouseText(npc[i].displayName + " the " + npc[i].name, 0, 0);
					if (mouseRightRelease && mouseRight)
					{
						mouseRightRelease = false;
						WorldGen.kickOut(i);
						PlaySound(12);
					}
				}
			}
		}

		public void DrawInterface()
		{
			if (showNPCs)
			{
				DrawNPCHouse();
			}
			if (player[myPlayer].selectedItem == 48 && player[myPlayer].itemAnimation > 0)
			{
				mouseLeftRelease = false;
			}
			mouseHC = false;
			if (hideUI)
			{
				maxQ = true;
				return;
			}
			if (player[myPlayer].rulerAcc)
			{
				int num = (int)((float)((int)(screenPosition.X / 16f) * 16) - screenPosition.X);
				int num2 = (int)((float)((int)(screenPosition.Y / 16f) * 16) - screenPosition.Y);
				int num3 = screenWidth / gridTexture.Width;
				int num4 = screenHeight / gridTexture.Height;
				for (int i = 0; i <= num3 + 1; i++)
				{
					for (int j = 0; j <= num4 + 1; j++)
					{
						SpriteBatch spriteBatch = this.spriteBatch;
						Texture2D texture = gridTexture;
						Vector2 position = new Vector2(i * gridTexture.Width + num, j * gridTexture.Height + num2);
						Rectangle? sourceRectangle = new Rectangle(0, 0, gridTexture.Width, gridTexture.Height);
						Color color = new Color(100, 100, 100, 15);
						float rotation = 0f;
						spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if (netDiag)
			{
				for (int k = 0; k < 4; k++)
				{
					string text = "";
					int num5 = 20;
					int num6 = 220;
					switch (k)
					{
					case 0:
						text = "RX Msgs: " + $"{rxMsg:0,0}";
						num6 += k * 20;
						break;
					case 1:
						text = "RX Bytes: " + $"{rxData:0,0}";
						num6 += k * 20;
						break;
					case 2:
						text = "TX Msgs: " + $"{txMsg:0,0}";
						num6 += k * 20;
						break;
					case 3:
						text = "TX Bytes: " + $"{txData:0,0}";
						num6 += k * 20;
						break;
					}
					SpriteBatch spriteBatch2 = this.spriteBatch;
					SpriteFont spriteFont = fontMouseText;
					string text2 = text;
					Vector2 position2 = new Vector2(num5, num6);
					Color white = Color.White;
					float rotation2 = 0f;
					spriteBatch2.DrawString(spriteFont, text2, position2, white, rotation2, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int l = 0; l < maxMsg; l++)
				{
					int num7 = 200;
					int num8 = 120;
					num8 += l * 15;
					string text3 = l + ": ";
					SpriteBatch spriteBatch3 = this.spriteBatch;
					SpriteFont spriteFont2 = fontMouseText;
					string text4 = text3;
					Vector2 position3 = new Vector2(num7, num8);
					Color white2 = Color.White;
					float rotation3 = 0f;
					spriteBatch3.DrawString(spriteFont2, text4, position3, white2, rotation3, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 30;
					text3 = "rx:" + $"{rxMsgType[l]:0,0}";
					SpriteBatch spriteBatch4 = this.spriteBatch;
					SpriteFont spriteFont3 = fontMouseText;
					string text5 = text3;
					Vector2 position4 = new Vector2(num7, num8);
					Color white3 = Color.White;
					float rotation4 = 0f;
					spriteBatch4.DrawString(spriteFont3, text5, position4, white3, rotation4, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text3 = $"{rxDataType[l]:0,0}";
					SpriteBatch spriteBatch5 = this.spriteBatch;
					SpriteFont spriteFont4 = fontMouseText;
					string text6 = text3;
					Vector2 position5 = new Vector2(num7, num8);
					Color white4 = Color.White;
					float rotation5 = 0f;
					spriteBatch5.DrawString(spriteFont4, text6, position5, white4, rotation5, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text3 = l + ": ";
					SpriteBatch spriteBatch6 = this.spriteBatch;
					SpriteFont spriteFont5 = fontMouseText;
					string text7 = text3;
					Vector2 position6 = new Vector2(num7, num8);
					Color white5 = Color.White;
					float rotation6 = 0f;
					spriteBatch6.DrawString(spriteFont5, text7, position6, white5, rotation6, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 30;
					text3 = "tx:" + $"{txMsgType[l]:0,0}";
					SpriteBatch spriteBatch7 = this.spriteBatch;
					SpriteFont spriteFont6 = fontMouseText;
					string text8 = text3;
					Vector2 position7 = new Vector2(num7, num8);
					Color white6 = Color.White;
					float rotation7 = 0f;
					spriteBatch7.DrawString(spriteFont6, text8, position7, white6, rotation7, default(Vector2), 0.8f, SpriteEffects.None, 0f);
					num7 += 70;
					text3 = $"{txDataType[l]:0,0}";
					SpriteBatch spriteBatch8 = this.spriteBatch;
					SpriteFont spriteFont7 = fontMouseText;
					string text9 = text3;
					Vector2 position8 = new Vector2(num7, num8);
					Color white7 = Color.White;
					float rotation8 = 0f;
					spriteBatch8.DrawString(spriteFont7, text9, position8, white7, rotation8, default(Vector2), 0.8f, SpriteEffects.None, 0f);
				}
			}
			if (drawDiag)
			{
				for (int m = 0; m < 7; m++)
				{
					string text10 = "";
					int num9 = 20;
					int num10 = 220;
					num10 += m * 16;
					if (m == 0)
					{
						text10 = "Solid Tiles:";
					}
					if (m == 1)
					{
						text10 = "Misc. Tiles:";
					}
					if (m == 2)
					{
						text10 = "Walls Tiles:";
					}
					if (m == 3)
					{
						text10 = "Background Tiles:";
					}
					if (m == 4)
					{
						text10 = "Water Tiles:";
					}
					if (m == 5)
					{
						text10 = "Black Tiles:";
					}
					if (m == 6)
					{
						text10 = "Total Render:";
					}
					SpriteBatch spriteBatch9 = this.spriteBatch;
					SpriteFont spriteFont8 = fontMouseText;
					string text11 = text10;
					Vector2 position9 = new Vector2(num9, num10);
					Color white8 = Color.White;
					float rotation9 = 0f;
					spriteBatch9.DrawString(spriteFont8, text11, position9, white8, rotation9, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int n = 0; n < 7; n++)
				{
					string text12 = "";
					int num11 = 180;
					int num12 = 220;
					num12 += n * 16;
					if (n == 0)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 1)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 2)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 3)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 4)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 5)
					{
						text12 = renderTimer[n] + "ms";
					}
					if (n == 6)
					{
						text12 = renderTimer[0] + renderTimer[1] + renderTimer[2] + renderTimer[3] + renderTimer[4] + renderTimer[5] + "ms";
					}
					SpriteBatch spriteBatch10 = this.spriteBatch;
					SpriteFont spriteFont9 = fontMouseText;
					string text13 = text12;
					Vector2 position10 = new Vector2(num11, num12);
					Color white9 = Color.White;
					float rotation10 = 0f;
					spriteBatch10.DrawString(spriteFont9, text13, position10, white9, rotation10, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num13 = 0; num13 < 6; num13++)
				{
					string text14 = "";
					int num14 = 20;
					int num15 = 346;
					num15 += num13 * 16;
					if (num13 == 0)
					{
						text14 = "Lighting Init:";
					}
					if (num13 == 1)
					{
						text14 = "Lighting Phase #1:";
					}
					if (num13 == 2)
					{
						text14 = "Lighting Phase #2:";
					}
					if (num13 == 3)
					{
						text14 = "Lighting Phase #3";
					}
					if (num13 == 4)
					{
						text14 = "Lighting Phase #4";
					}
					if (num13 == 5)
					{
						text14 = "Total Lighting:";
					}
					SpriteBatch spriteBatch11 = this.spriteBatch;
					SpriteFont spriteFont10 = fontMouseText;
					string text15 = text14;
					Vector2 position11 = new Vector2(num14, num15);
					Color white10 = Color.White;
					float rotation11 = 0f;
					spriteBatch11.DrawString(spriteFont10, text15, position11, white10, rotation11, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num16 = 0; num16 < 6; num16++)
				{
					string text16 = "";
					int num17 = 180;
					int num18 = 346;
					num18 += num16 * 16;
					if (num16 == 0)
					{
						text16 = lightTimer[num16] + "ms";
					}
					if (num16 == 1)
					{
						text16 = lightTimer[num16] + "ms";
					}
					if (num16 == 2)
					{
						text16 = lightTimer[num16] + "ms";
					}
					if (num16 == 3)
					{
						text16 = lightTimer[num16] + "ms";
					}
					if (num16 == 4)
					{
						text16 = lightTimer[num16] + "ms";
					}
					if (num16 == 5)
					{
						text16 = lightTimer[0] + lightTimer[1] + lightTimer[2] + lightTimer[3] + lightTimer[4] + "ms";
					}
					SpriteBatch spriteBatch12 = this.spriteBatch;
					SpriteFont spriteFont11 = fontMouseText;
					string text17 = text16;
					Vector2 position12 = new Vector2(num17, num18);
					Color white11 = Color.White;
					float rotation12 = 0f;
					spriteBatch12.DrawString(spriteFont11, text17, position12, white11, rotation12, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				int num19 = 5;
				for (int num20 = 0; num20 < num19; num20++)
				{
					int num21 = 20;
					int num22 = 456;
					num22 += num20 * 16;
					string text18 = "Render #" + num20 + ":";
					SpriteBatch spriteBatch13 = this.spriteBatch;
					SpriteFont spriteFont12 = fontMouseText;
					string text19 = text18;
					Vector2 position13 = new Vector2(num21, num22);
					Color white12 = Color.White;
					float rotation13 = 0f;
					spriteBatch13.DrawString(spriteFont12, text19, position13, white12, rotation13, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num23 = 0; num23 < num19; num23++)
				{
					int num24 = 180;
					int num25 = 456;
					num25 += num23 * 16;
					string text20 = drawTimer[num23] + "ms";
					SpriteBatch spriteBatch14 = this.spriteBatch;
					SpriteFont spriteFont13 = fontMouseText;
					string text21 = text20;
					Vector2 position14 = new Vector2(num24, num25);
					Color white13 = Color.White;
					float rotation14 = 0f;
					spriteBatch14.DrawString(spriteFont13, text21, position14, white13, rotation14, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				for (int num26 = 0; num26 < num19; num26++)
				{
					int num27 = 230;
					int num28 = 456;
					num28 += num26 * 16;
					string text22 = drawTimerMax[num26] + "ms";
					SpriteBatch spriteBatch15 = this.spriteBatch;
					SpriteFont spriteFont14 = fontMouseText;
					string text23 = text22;
					Vector2 position15 = new Vector2(num27, num28);
					Color white14 = Color.White;
					float rotation15 = 0f;
					spriteBatch15.DrawString(spriteFont14, text23, position15, white14, rotation15, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				int num29 = 20;
				int num30 = 456 + 16 * num19 + 16;
				string text24 = "Update:";
				SpriteBatch spriteBatch16 = this.spriteBatch;
				SpriteFont spriteFont15 = fontMouseText;
				string text25 = text24;
				Vector2 position16 = new Vector2(num29, num30);
				Color white15 = Color.White;
				float rotation16 = 0f;
				spriteBatch16.DrawString(spriteFont15, text25, position16, white15, rotation16, default(Vector2), 1f, SpriteEffects.None, 0f);
				num29 = 180;
				text24 = upTimer + "ms";
				SpriteBatch spriteBatch17 = this.spriteBatch;
				SpriteFont spriteFont16 = fontMouseText;
				string text26 = text24;
				Vector2 position17 = new Vector2(num29, num30);
				Color white16 = Color.White;
				float rotation17 = 0f;
				spriteBatch17.DrawString(spriteFont16, text26, position17, white16, rotation17, default(Vector2), 1f, SpriteEffects.None, 0f);
				num29 = 230;
				text24 = upTimerMax + "ms";
				SpriteBatch spriteBatch18 = this.spriteBatch;
				SpriteFont spriteFont17 = fontMouseText;
				string text27 = text24;
				Vector2 position18 = new Vector2(num29, num30);
				Color white17 = Color.White;
				float rotation18 = 0f;
				spriteBatch18.DrawString(spriteFont17, text27, position18, white17, rotation18, default(Vector2), 1f, SpriteEffects.None, 0f);
			}
			if (signBubble)
			{
				int num31 = (int)((float)signX - screenPosition.X);
				int num32 = (int)((float)signY - screenPosition.Y);
				SpriteEffects effects = SpriteEffects.None;
				if ((float)signX > player[myPlayer].position.X + (float)player[myPlayer].width)
				{
					effects = SpriteEffects.FlipHorizontally;
					num31 += -8 - chat2Texture.Width;
				}
				else
				{
					num31 += 8;
				}
				num32 -= 22;
				SpriteBatch spriteBatch19 = this.spriteBatch;
				Texture2D texture2 = chat2Texture;
				Vector2 position19 = new Vector2(num31, num32);
				Rectangle? sourceRectangle2 = new Rectangle(0, 0, chat2Texture.Width, chat2Texture.Height);
				Color color2 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
				float rotation19 = 0f;
				spriteBatch19.Draw(texture2, position19, sourceRectangle2, color2, rotation19, default(Vector2), 1f, effects, 0f);
				signBubble = false;
			}
			for (int num33 = 0; num33 < 255; num33++)
			{
				if (!player[num33].active || myPlayer == num33 || player[num33].dead)
				{
					continue;
				}
				new Rectangle((int)((double)player[num33].position.X + (double)player[num33].width * 0.5 - 16.0), (int)(player[num33].position.Y + (float)player[num33].height - 48f), 32, 48);
				if (player[myPlayer].team > 0 && player[myPlayer].team == player[num33].team)
				{
					new Rectangle((int)screenPosition.X, (int)screenPosition.Y, screenWidth, screenHeight);
					string text28 = player[num33].name;
					if (player[num33].statLife < player[num33].statLifeMax2)
					{
						object obj = text28;
						text28 = string.Concat(obj, ": ", player[num33].statLife, "/", player[num33].statLifeMax2);
					}
					Vector2 vector = fontMouseText.MeasureString(text28);
					float num34 = 0f;
					if (player[num33].chatShowTime > 0)
					{
						num34 = 0f - vector.Y;
					}
					float num35 = 0f;
					float num36 = (float)(int)mouseTextColor / 255f;
					Color color3 = new Color((byte)((float)(int)teamColor[player[num33].team].R * num36), (byte)((float)(int)teamColor[player[num33].team].G * num36), (byte)((float)(int)teamColor[player[num33].team].B * num36), mouseTextColor);
					Vector2 vector2 = new Vector2((float)(screenWidth / 2) + screenPosition.X, (float)(screenHeight / 2) + screenPosition.Y);
					float num37 = player[num33].position.X + (float)(player[num33].width / 2) - vector2.X;
					float num38 = player[num33].position.Y - vector.Y - 2f + num34 - vector2.Y;
					float num39 = (float)Math.Sqrt(num37 * num37 + num38 * num38);
					int num40 = screenHeight;
					if (screenHeight > screenWidth)
					{
						num40 = screenWidth;
					}
					num40 = num40 / 2 - 30;
					if (num40 < 100)
					{
						num40 = 100;
					}
					if (num39 < (float)num40)
					{
						vector.X = player[num33].position.X + (float)(player[num33].width / 2) - vector.X / 2f - screenPosition.X;
						vector.Y = player[num33].position.Y - vector.Y - 2f + num34 - screenPosition.Y;
					}
					else
					{
						num35 = num39;
						num39 = (float)num40 / num39;
						vector.X = (float)(screenWidth / 2) + num37 * num39 - vector.X / 2f;
						vector.Y = (float)(screenHeight / 2) + num38 * num39;
					}
					if (num35 > 0f)
					{
						string text29 = "(" + (int)(num35 / 16f * 2f) + " ft)";
						Vector2 vector3 = fontMouseText.MeasureString(text29);
						vector3.X = vector.X + fontMouseText.MeasureString(text28).X / 2f - vector3.X / 2f;
						vector3.Y = vector.Y + fontMouseText.MeasureString(text28).Y / 2f - vector3.Y / 2f - 20f;
						SpriteBatch spriteBatch20 = this.spriteBatch;
						SpriteFont spriteFont18 = fontMouseText;
						string text30 = text29;
						Vector2 position20 = new Vector2(vector3.X - 2f, vector3.Y);
						Color black = Color.Black;
						float rotation20 = 0f;
						spriteBatch20.DrawString(spriteFont18, text30, position20, black, rotation20, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch21 = this.spriteBatch;
						SpriteFont spriteFont19 = fontMouseText;
						string text31 = text29;
						Vector2 position21 = new Vector2(vector3.X + 2f, vector3.Y);
						Color black2 = Color.Black;
						float rotation21 = 0f;
						spriteBatch21.DrawString(spriteFont19, text31, position21, black2, rotation21, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch22 = this.spriteBatch;
						SpriteFont spriteFont20 = fontMouseText;
						string text32 = text29;
						Vector2 position22 = new Vector2(vector3.X, vector3.Y - 2f);
						Color black3 = Color.Black;
						float rotation22 = 0f;
						spriteBatch22.DrawString(spriteFont20, text32, position22, black3, rotation22, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch23 = this.spriteBatch;
						SpriteFont spriteFont21 = fontMouseText;
						string text33 = text29;
						Vector2 position23 = new Vector2(vector3.X, vector3.Y + 2f);
						Color black4 = Color.Black;
						float rotation23 = 0f;
						spriteBatch23.DrawString(spriteFont21, text33, position23, black4, rotation23, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch24 = this.spriteBatch;
						SpriteFont spriteFont22 = fontMouseText;
						string text34 = text29;
						Vector2 position24 = vector3;
						Color color4 = color3;
						float rotation24 = 0f;
						spriteBatch24.DrawString(spriteFont22, text34, position24, color4, rotation24, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					SpriteBatch spriteBatch25 = this.spriteBatch;
					SpriteFont spriteFont23 = fontMouseText;
					string text35 = text28;
					Vector2 position25 = new Vector2(vector.X - 2f, vector.Y);
					Color black5 = Color.Black;
					float rotation25 = 0f;
					spriteBatch25.DrawString(spriteFont23, text35, position25, black5, rotation25, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch26 = this.spriteBatch;
					SpriteFont spriteFont24 = fontMouseText;
					string text36 = text28;
					Vector2 position26 = new Vector2(vector.X + 2f, vector.Y);
					Color black6 = Color.Black;
					float rotation26 = 0f;
					spriteBatch26.DrawString(spriteFont24, text36, position26, black6, rotation26, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch27 = this.spriteBatch;
					SpriteFont spriteFont25 = fontMouseText;
					string text37 = text28;
					Vector2 position27 = new Vector2(vector.X, vector.Y - 2f);
					Color black7 = Color.Black;
					float rotation27 = 0f;
					spriteBatch27.DrawString(spriteFont25, text37, position27, black7, rotation27, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch28 = this.spriteBatch;
					SpriteFont spriteFont26 = fontMouseText;
					string text38 = text28;
					Vector2 position28 = new Vector2(vector.X, vector.Y + 2f);
					Color black8 = Color.Black;
					float rotation28 = 0f;
					spriteBatch28.DrawString(spriteFont26, text38, position28, black8, rotation28, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch29 = this.spriteBatch;
					SpriteFont spriteFont27 = fontMouseText;
					string text39 = text28;
					Vector2 position29 = vector;
					Color color5 = color3;
					float rotation29 = 0f;
					spriteBatch29.DrawString(spriteFont27, text39, position29, color5, rotation29, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			if (playerInventory)
			{
				npcChatText = "";
				player[myPlayer].sign = -1;
			}
			if (ignoreErrors)
			{
				try
				{
					if (npcChatText != "" || player[myPlayer].sign != -1)
					{
						DrawChat();
					}
				}
				catch
				{
				}
			}
			else if (npcChatText != "" || player[myPlayer].sign != -1)
			{
				DrawChat();
			}
			Color color6 = new Color(220, 220, 220, 220);
			invAlpha += invDir * 0.2f;
			if (invAlpha > 240f)
			{
				invAlpha = 240f;
				invDir = -1f;
			}
			if (invAlpha < 180f)
			{
				invAlpha = 180f;
				invDir = 1f;
			}
			color6 = new Color((byte)invAlpha, (byte)invAlpha, (byte)invAlpha, (byte)invAlpha);
			bool flag = false;
			MouseTextRare = 0;
			int num41 = screenWidth - 800;
			int num42 = player[myPlayer].statLifeMax2 / 20;
			if (num42 >= 10)
			{
				num42 = 10;
			}
			string text40 = Lang.inter[0] + " " + player[myPlayer].statLifeMax2 + "/" + player[myPlayer].statLifeMax2;
			Vector2 vector4 = fontMouseText.MeasureString(text40);
			int num43 = 20;
			if (!TMod.RunMethod(TMod.WorldHooks.PreDrawLifeHearts, this.spriteBatch) || TMod.GetContinueMethod())
			{
				if (!player[myPlayer].ghost)
				{
					Vector2 position30 = new Vector2((float)(500 + 13 * num42) - vector4.X * 0.5f + (float)num41, 6f);
					Color color7 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					this.spriteBatch.DrawString(fontMouseText, Lang.inter[0], position30, color7, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					this.spriteBatch.DrawString(fontMouseText, player[myPlayer].statLife + "/" + player[myPlayer].statLifeMax2, new Vector2((float)(500 + 13 * num42) + vector4.X * 0.5f + (float)num41, 6f), new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor), 0f, new Vector2(fontMouseText.MeasureString(player[myPlayer].statLife + "/" + player[myPlayer].statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f);
				}
				for (int num44 = 1; num44 < player[myPlayer].statLifeMax2 / num43 + 1; num44++)
				{
					int num45 = 255;
					float num46 = 1f;
					bool flag2 = false;
					if (player[myPlayer].statLife >= num44 * num43)
					{
						num45 = 255;
						if (player[myPlayer].statLife == num44 * num43)
						{
							flag2 = true;
						}
					}
					else
					{
						float num47 = (float)(player[myPlayer].statLife - (num44 - 1) * num43) / (float)num43;
						num45 = (int)(30f + 225f * num47);
						if (num45 < 30)
						{
							num45 = 30;
						}
						num46 = num47 / 4f + 0.75f;
						if ((double)num46 < 0.75)
						{
							num46 = 0.75f;
						}
						if (num47 > 0f)
						{
							flag2 = true;
						}
					}
					if (flag2)
					{
						num46 += cursorScale - 1f;
					}
					int num48 = 0;
					int num49 = 0;
					if (num44 > 10)
					{
						num48 -= 260;
						num49 += 26;
					}
					int a = (int)((double)(float)num45 * 0.9);
					if (!player[myPlayer].ghost)
					{
						this.spriteBatch.Draw(heartTexture, new Vector2(500 + 26 * (num44 - 1) + num48 + num41 + heartTexture.Width / 2, 32f + ((float)heartTexture.Height - (float)heartTexture.Height * num46) / 2f + (float)num49 + (float)(heartTexture.Height / 2)), new Rectangle(0, 0, heartTexture.Width, heartTexture.Height), new Color(num45, num45, num45, a), 0f, new Vector2(heartTexture.Width / 2, heartTexture.Height / 2), num46, SpriteEffects.None, 0f);
					}
				}
			}
			int num50 = 20;
			if ((!TMod.RunMethod(TMod.WorldHooks.PreDrawManaStars, this.spriteBatch) || TMod.GetContinueMethod()) && player[myPlayer].statManaMax2 > 0)
			{
				_ = player[myPlayer].statManaMax2 / 20;
				SpriteBatch spriteBatch30 = this.spriteBatch;
				SpriteFont spriteFont28 = fontMouseText;
				string text41 = Lang.inter[2];
				Vector2 position31 = new Vector2(750 + num41, 6f);
				Color color8 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
				float rotation30 = 0f;
				spriteBatch30.DrawString(spriteFont28, text41, position31, color8, rotation30, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int num51 = 1; num51 < player[myPlayer].statManaMax2 / num50 + 1; num51++)
				{
					int num52 = 255;
					bool flag3 = false;
					float num53 = 1f;
					if (player[myPlayer].statMana >= num51 * num50)
					{
						num52 = 255;
						if (player[myPlayer].statMana == num51 * num50)
						{
							flag3 = true;
						}
					}
					else
					{
						float num54 = (float)(player[myPlayer].statMana - (num51 - 1) * num50) / (float)num50;
						num52 = (int)(30f + 225f * num54);
						if (num52 < 30)
						{
							num52 = 30;
						}
						num53 = num54 / 4f + 0.75f;
						if ((double)num53 < 0.75)
						{
							num53 = 0.75f;
						}
						if (num54 > 0f)
						{
							flag3 = true;
						}
					}
					if (flag3)
					{
						num53 += cursorScale - 1f;
					}
					int a2 = (int)((double)(float)num52 * 0.9);
					this.spriteBatch.Draw(manaTexture, new Vector2(775 + num41, (float)(30 + manaTexture.Height / 2) + ((float)manaTexture.Height - (float)manaTexture.Height * num53) / 2f + (float)(28 * (num51 - 1))), new Rectangle(0, 0, manaTexture.Width, manaTexture.Height), new Color(num52, num52, num52, a2), 0f, new Vector2(manaTexture.Width / 2, manaTexture.Height / 2), num53, SpriteEffects.None, 0f);
				}
			}
			if ((!TMod.RunMethod(TMod.WorldHooks.PreDrawBubbleBar, this.spriteBatch) || TMod.GetContinueMethod()) && player[myPlayer].breath < player[myPlayer].breathMax && !player[myPlayer].ghost)
			{
				int num55 = 76;
				Vector2 position32 = new Vector2((float)(500 + 13 * num42) - fontMouseText.MeasureString(Lang.inter[1]).X * 0.5f + (float)num41, 6 + num55);
				Color color9 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
				this.spriteBatch.DrawString(fontMouseText, Lang.inter[1], position32, color9);
				int num56 = 20;
				for (int num57 = 1; num57 < player[myPlayer].breathMax / num56 + 1; num57++)
				{
					int num58 = 255;
					float num59 = 1f;
					if (player[myPlayer].breath >= num57 * num56)
					{
						num58 = 255;
					}
					else
					{
						float num60 = (float)(player[myPlayer].breath - (num57 - 1) * num56) / (float)num56;
						num58 = (int)(30f + 225f * num60);
						if (num58 < 30)
						{
							num58 = 30;
						}
						num59 = num60 / 4f + 0.75f;
						if ((double)num59 < 0.75)
						{
							num59 = 0.75f;
						}
					}
					int num61 = 0;
					int num62 = 0;
					if (num57 > 10)
					{
						num61 -= 260;
						num62 += 26;
					}
					SpriteBatch spriteBatch31 = this.spriteBatch;
					Texture2D texture3 = bubbleTexture;
					Vector2 position33 = new Vector2(500 + 26 * (num57 - 1) + num61 + num41, 32f + ((float)bubbleTexture.Height - (float)bubbleTexture.Height * num59) / 2f + (float)num62 + (float)num55);
					Rectangle? sourceRectangle3 = new Rectangle(0, 0, bubbleTexture.Width, bubbleTexture.Height);
					Color color10 = new Color(num58, num58, num58, num58);
					float rotation31 = 0f;
					spriteBatch31.Draw(texture3, position33, sourceRectangle3, color10, rotation31, default(Vector2), num59, SpriteEffects.None, 0f);
				}
			}
			buffString = "";
			if ((!TMod.RunMethod(TMod.WorldHooks.PreDrawBuffsList, this.spriteBatch) || TMod.GetContinueMethod()) && !playerInventory)
			{
				int num63 = -1;
				for (int num64 = 0; num64 < player[myPlayer].buffType.Length; num64++)
				{
					if (player[myPlayer].buffType[num64] > 0)
					{
						int i2 = player[myPlayer].buffType[num64];
						int num65 = 32 + num64 * 38;
						int num66 = 76;
						Color color11 = new Color(buffAlpha[num64], buffAlpha[num64], buffAlpha[num64], buffAlpha[num64]);
						SpriteBatch spriteBatch32 = this.spriteBatch;
						Texture2D texture4 = buffTexture[i2];
						Vector2 position34 = new Vector2(num65, num66);
						Rectangle? sourceRectangle4 = new Rectangle(0, 0, buffTexture[i2].Width, buffTexture[i2].Height);
						Color color12 = color11;
						float rotation32 = 0f;
						spriteBatch32.Draw(texture4, position34, sourceRectangle4, color12, rotation32, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (!buffDontDisplayTime[i2])
						{
							string text42 = "0 s";
							text42 = ((player[myPlayer].buffTime[num64] / 60 < 60) ? (Math.Round((double)player[myPlayer].buffTime[num64] / 60.0) + " s") : (Math.Round((double)(player[myPlayer].buffTime[num64] / 60) / 60.0) + " m"));
							SpriteBatch spriteBatch33 = this.spriteBatch;
							SpriteFont spriteFont29 = fontItemStack;
							string text43 = text42;
							Vector2 position35 = new Vector2(num65, num66 + buffTexture[i2].Height);
							Color color13 = color11;
							float rotation33 = 0f;
							spriteBatch33.DrawString(spriteFont29, text43, position35, color13, rotation33, default(Vector2), 0.8f, SpriteEffects.None, 0f);
						}
						if (mouseX < num65 + buffTexture[i2].Width && mouseY < num66 + buffTexture[i2].Height && mouseX > num65 && mouseY > num66)
						{
							num63 = num64;
							buffAlpha[num64] += 0.1f;
							if (mouseRight && mouseRightRelease && !debuff[i2])
							{
								PlaySound(12);
								player[myPlayer].DelBuff(num64);
							}
						}
						else
						{
							buffAlpha[num64] -= 0.05f;
						}
						if (buffAlpha[num64] > 1f)
						{
							buffAlpha[num64] = 1f;
						}
						else if ((double)buffAlpha[num64] < 0.4)
						{
							buffAlpha[num64] = 0.4f;
						}
					}
					else
					{
						buffAlpha[num64] = 0.4f;
					}
				}
				if (num63 >= 0)
				{
					int num67 = player[myPlayer].buffType[num63];
					if (num67 > 0)
					{
						buffString = buffTip[num67];
						MouseText(buffName[num67], 0, 0);
					}
				}
			}
			if (player[myPlayer].dead)
			{
				playerInventory = false;
			}
			if (!playerInventory)
			{
				player[myPlayer].chest = -1;
				if (craftGuide)
				{
					craftGuide = false;
					Recipe.FindRecipes();
				}
				reforge = false;
				if (Config.tileInterface != null)
				{
					Config.tileInterface.DropAll();
					Config.tileInterface = null;
				}
			}
			MouseTextString = "";
			if (playerInventory)
			{
				if (netMode == 1)
				{
					int num68 = 675 + screenWidth - 800;
					int num69 = 114;
					if (player[myPlayer].hostile)
					{
						SpriteBatch spriteBatch34 = this.spriteBatch;
						Texture2D texture5 = itemTexture[4];
						Vector2 position36 = new Vector2(num68 - 2, num69);
						Rectangle? sourceRectangle5 = new Rectangle(0, 0, itemTexture[4].Width, itemTexture[4].Height);
						Color color14 = teamColor[player[myPlayer].team];
						float rotation34 = 0f;
						spriteBatch34.Draw(texture5, position36, sourceRectangle5, color14, rotation34, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch35 = this.spriteBatch;
						Texture2D texture6 = itemTexture[4];
						Vector2 position37 = new Vector2(num68 + 2, num69);
						Rectangle? sourceRectangle6 = new Rectangle(0, 0, itemTexture[4].Width, itemTexture[4].Height);
						Color color15 = teamColor[player[myPlayer].team];
						float rotation35 = 0f;
						spriteBatch35.Draw(texture6, position37, sourceRectangle6, color15, rotation35, default(Vector2), 1f, SpriteEffects.FlipHorizontally, 0f);
					}
					else
					{
						SpriteBatch spriteBatch36 = this.spriteBatch;
						Texture2D texture7 = itemTexture[4];
						Vector2 position38 = new Vector2(num68 - 16, num69 + 14);
						Rectangle? sourceRectangle7 = new Rectangle(0, 0, itemTexture[4].Width, itemTexture[4].Height);
						Color color16 = teamColor[player[myPlayer].team];
						float rotation36 = -0.785f;
						spriteBatch36.Draw(texture7, position38, sourceRectangle7, color16, rotation36, default(Vector2), 1f, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch37 = this.spriteBatch;
						Texture2D texture8 = itemTexture[4];
						Vector2 position39 = new Vector2(num68 + 2, num69 + 14);
						Rectangle? sourceRectangle8 = new Rectangle(0, 0, itemTexture[4].Width, itemTexture[4].Height);
						Color color17 = teamColor[player[myPlayer].team];
						float rotation37 = -0.785f;
						spriteBatch37.Draw(texture8, position39, sourceRectangle8, color17, rotation37, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					if (mouseX > num68 && mouseX < num68 + 34 && mouseY > num69 - 2 && mouseY < num69 + 34)
					{
						player[myPlayer].mouseInterface = true;
						if (mouseLeft && mouseLeftRelease && teamCooldown == 0)
						{
							teamCooldown = teamCooldownLen;
							PlaySound(12);
							if (player[myPlayer].hostile)
							{
								player[myPlayer].hostile = false;
							}
							else
							{
								player[myPlayer].hostile = true;
							}
							NetMessage.SendData(30, -1, -1, "", myPlayer);
						}
					}
					num68 -= 3;
					Rectangle value = new Rectangle(mouseX, mouseY, 1, 1);
					int width = teamTexture.Width;
					int height = teamTexture.Height;
					for (int num70 = 0; num70 < 5; num70++)
					{
						Rectangle rectangle = default(Rectangle);
						if (num70 == 0)
						{
							rectangle = new Rectangle(num68 + 50, num69 - 20, width, height);
						}
						if (num70 == 1)
						{
							rectangle = new Rectangle(num68 + 40, num69, width, height);
						}
						if (num70 == 2)
						{
							rectangle = new Rectangle(num68 + 60, num69, width, height);
						}
						if (num70 == 3)
						{
							rectangle = new Rectangle(num68 + 40, num69 + 20, width, height);
						}
						if (num70 == 4)
						{
							rectangle = new Rectangle(num68 + 60, num69 + 20, width, height);
						}
						if (rectangle.Intersects(value))
						{
							player[myPlayer].mouseInterface = true;
							if (mouseLeft && mouseLeftRelease && player[myPlayer].team != num70 && teamCooldown == 0)
							{
								teamCooldown = teamCooldownLen;
								PlaySound(12);
								player[myPlayer].team = num70;
								NetMessage.SendData(45, -1, -1, "", myPlayer);
							}
						}
					}
					SpriteBatch spriteBatch38 = this.spriteBatch;
					Texture2D texture9 = teamTexture;
					Vector2 position40 = new Vector2(num68 + 50, num69 - 20);
					Rectangle? sourceRectangle9 = new Rectangle(0, 0, teamTexture.Width, teamTexture.Height);
					Color color18 = teamColor[0];
					float rotation38 = 0f;
					spriteBatch38.Draw(texture9, position40, sourceRectangle9, color18, rotation38, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch39 = this.spriteBatch;
					Texture2D texture10 = teamTexture;
					Vector2 position41 = new Vector2(num68 + 40, num69);
					Rectangle? sourceRectangle10 = new Rectangle(0, 0, teamTexture.Width, teamTexture.Height);
					Color color19 = teamColor[1];
					float rotation39 = 0f;
					spriteBatch39.Draw(texture10, position41, sourceRectangle10, color19, rotation39, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch40 = this.spriteBatch;
					Texture2D texture11 = teamTexture;
					Vector2 position42 = new Vector2(num68 + 60, num69);
					Rectangle? sourceRectangle11 = new Rectangle(0, 0, teamTexture.Width, teamTexture.Height);
					Color color20 = teamColor[2];
					float rotation40 = 0f;
					spriteBatch40.Draw(texture11, position42, sourceRectangle11, color20, rotation40, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch41 = this.spriteBatch;
					Texture2D texture12 = teamTexture;
					Vector2 position43 = new Vector2(num68 + 40, num69 + 20);
					Rectangle? sourceRectangle12 = new Rectangle(0, 0, teamTexture.Width, teamTexture.Height);
					Color color21 = teamColor[3];
					float rotation41 = 0f;
					spriteBatch41.Draw(texture12, position43, sourceRectangle12, color21, rotation41, default(Vector2), 1f, SpriteEffects.None, 0f);
					SpriteBatch spriteBatch42 = this.spriteBatch;
					Texture2D texture13 = teamTexture;
					Vector2 position44 = new Vector2(num68 + 60, num69 + 20);
					Rectangle? sourceRectangle13 = new Rectangle(0, 0, teamTexture.Width, teamTexture.Height);
					Color color22 = teamColor[4];
					float rotation42 = 0f;
					spriteBatch42.Draw(texture13, position44, sourceRectangle13, color22, rotation42, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				bool flag4 = false;
				inventoryScale = 0.85f;
				int num71 = 448;
				int num72 = 210;
				Color color23 = new Color(150, 150, 150, 150);
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawTrashItem, this.spriteBatch) || TMod.GetContinueMethod())
				{
					if (mouseX >= num71 && (float)mouseX <= (float)num71 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num72 && (float)mouseY <= (float)num72 + (float)inventoryBackTexture.Height * inventoryScale)
					{
						player[myPlayer].mouseInterface = true;
						if (mouseLeftRelease && mouseLeft)
						{
							if (mouseItem.type != 0)
							{
								trashItem.SetDefaults(0);
							}
							Item item = mouseItem;
							mouseItem = trashItem;
							trashItem = item;
							if (trashItem.type == 0 || trashItem.stack < 1)
							{
								trashItem = new Item();
							}
							if (mouseItem.IsTheSameAs(trashItem) && trashItem.stack != trashItem.maxStack && mouseItem.stack != mouseItem.maxStack)
							{
								if (mouseItem.stack + trashItem.stack <= mouseItem.maxStack)
								{
									trashItem.stack += mouseItem.stack;
									mouseItem.stack = 0;
								}
								else
								{
									int num73 = mouseItem.maxStack - trashItem.stack;
									trashItem.stack += num73;
									mouseItem.stack -= num73;
								}
							}
							if (mouseItem.type == 0 || mouseItem.stack < 1)
							{
								mouseItem = new Item();
							}
							if (mouseItem.type > 0 || trashItem.type > 0)
							{
								PlaySound(7);
							}
						}
						if (!flag4)
						{
							MouseTextString = trashItem.name;
							if (trashItem.stack > 1)
							{
								object mouseTextString = MouseTextString;
								MouseTextString = string.Concat(mouseTextString, " (", trashItem.stack, ")");
							}
							toolTip = (Item)trashItem.ShallowClone();
							if (MouseTextString == null)
							{
								MouseTextString = Lang.inter[3];
							}
						}
						else
						{
							MouseTextString = Lang.inter[3];
						}
					}
					SpriteBatch spriteBatch43 = this.spriteBatch;
					Texture2D texture14 = inventoryBack7Texture;
					Vector2 vector5 = new Vector2(num71, num72);
					Rectangle? sourceRectangle14 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
					Color color24 = color6;
					float rotation43 = 0f;
					spriteBatch43.Draw(texture14, vector5, sourceRectangle14, color24, rotation43, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
					color23 = Color.White;
					if (trashItem.type == 0 || trashItem.stack == 0 || flag4)
					{
						color23 = new Color(100, 100, 100, 100);
						float num74 = inventoryScale;
						SpriteBatch spriteBatch44 = this.spriteBatch;
						Texture2D texture15 = trashTexture;
						Vector2 position45 = new Vector2((float)num71 + 26f * inventoryScale - (float)trashTexture.Width * 0.5f * num74, (float)num72 + 26f * inventoryScale - (float)trashTexture.Height * 0.5f * num74);
						Rectangle? sourceRectangle15 = new Rectangle(0, 0, trashTexture.Width, trashTexture.Height);
						Color color25 = color23;
						float rotation44 = 0f;
						spriteBatch44.Draw(texture15, position45, sourceRectangle15, color25, rotation44, default(Vector2), num74, SpriteEffects.None, 0f);
					}
					else
					{
						ItemSlotRender.DrawItemInSlot(spriteBatch43, color23, trashItem, vector5);
					}
				}
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawInventoryTitle, this.spriteBatch) || TMod.GetContinueMethod())
				{
					SpriteBatch spriteBatch45 = this.spriteBatch;
					SpriteFont spriteFont30 = fontMouseText;
					string text44 = Lang.inter[4];
					Vector2 position46 = new Vector2(40f, 0f);
					Color color26 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation45 = 0f;
					spriteBatch45.DrawString(spriteFont30, text44, position46, color26, rotation45, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				inventoryScale = 0.85f;
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawInventorySlots, this.spriteBatch) || TMod.GetContinueMethod())
				{
					if (mouseX > 20 && mouseX < (int)(20f + 560f * inventoryScale) && mouseY > 20 && mouseY < (int)(20f + 224f * inventoryScale))
					{
						player[myPlayer].mouseInterface = true;
					}
					for (int num75 = 0; num75 < 10; num75++)
					{
						for (int num76 = 0; num76 < 4; num76++)
						{
							int num77 = (int)(20f + (float)(num75 * 56) * inventoryScale);
							int num78 = (int)(20f + (float)(num76 * 56) * inventoryScale);
							int num79 = num75 + num76 * 10;
							new Color(100, 100, 100, 100);
							if (mouseX >= num77 && (float)mouseX <= (float)num77 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num78 && (float)mouseY <= (float)num78 + (float)inventoryBackTexture.Height * inventoryScale)
							{
								player[myPlayer].mouseInterface = true;
								if (mouseLeftRelease && mouseLeft)
								{
									if (keyState.IsKeyDown(Keys.LeftShift))
									{
										if (player[myPlayer].inventory[num79].type > 0)
										{
											if (npcShop > 0)
											{
												if (player[myPlayer].SellItem(player[myPlayer].inventory[num79].value, player[myPlayer].inventory[num79].stack))
												{
													shop[npcShop].AddShop(player[myPlayer].inventory[num79]);
													player[myPlayer].inventory[num79].SetDefaults(0);
													PlaySound(18);
												}
												else if (player[myPlayer].inventory[num79].value == 0)
												{
													shop[npcShop].AddShop(player[myPlayer].inventory[num79]);
													player[myPlayer].inventory[num79].SetDefaults(0);
													PlaySound(7);
												}
											}
											else
											{
												PlaySound(7);
												trashItem = (Item)player[myPlayer].inventory[num79].Clone();
												player[myPlayer].inventory[num79].SetDefaults(0);
												Recipe.FindRecipes();
											}
										}
									}
									else if (player[myPlayer].selectedItem != num79 || player[myPlayer].itemAnimation <= 0)
									{
										Item item2 = mouseItem;
										mouseItem = player[myPlayer].inventory[num79];
										player[myPlayer].inventory[num79] = item2;
										if (player[myPlayer].inventory[num79].type == 0 || player[myPlayer].inventory[num79].stack < 1)
										{
											player[myPlayer].inventory[num79] = new Item();
										}
										if (mouseItem.IsTheSameAs(player[myPlayer].inventory[num79]) && player[myPlayer].inventory[num79].stack != player[myPlayer].inventory[num79].maxStack && mouseItem.stack != mouseItem.maxStack)
										{
											if (mouseItem.stack + player[myPlayer].inventory[num79].stack <= mouseItem.maxStack)
											{
												player[myPlayer].inventory[num79].stack += mouseItem.stack;
												mouseItem.stack = 0;
											}
											else
											{
												int num80 = mouseItem.maxStack - player[myPlayer].inventory[num79].stack;
												player[myPlayer].inventory[num79].stack += num80;
												mouseItem.stack -= num80;
											}
										}
										if (mouseItem.type == 0 || mouseItem.stack < 1)
										{
											mouseItem = new Item();
										}
										if (mouseItem.type > 0 || player[myPlayer].inventory[num79].type > 0)
										{
											Recipe.FindRecipes();
											PlaySound(7);
										}
									}
								}
								else if (!mouseRight || !mouseRightRelease || player[myPlayer].inventory[num79].type <= 0 || player[myPlayer].inventory[num79].InvRightClicked(player[myPlayer], myPlayer, num79))
								{
									if (mouseRight && mouseRightRelease && (player[myPlayer].inventory[num79].type == 599 || player[myPlayer].inventory[num79].type == 600 || player[myPlayer].inventory[num79].type == 601))
									{
										PlaySound(7);
										stackSplit = 30;
										mouseRightRelease = false;
										int num81 = rand.Next(14);
										if (num81 == 0 && hardMode)
										{
											player[myPlayer].inventory[num79].SetDefaults(602);
										}
										else if (num81 <= 7)
										{
											player[myPlayer].inventory[num79].SetDefaults(586);
											player[myPlayer].inventory[num79].stack = rand.Next(20, 50);
										}
										else
										{
											player[myPlayer].inventory[num79].SetDefaults(591);
											player[myPlayer].inventory[num79].stack = rand.Next(20, 50);
										}
									}
									else if (mouseRight && mouseRightRelease && player[myPlayer].inventory[num79].maxStack == 1)
									{
										player[myPlayer].inventory[num79] = armorSwap(player[myPlayer].inventory[num79]);
									}
									else if (stackSplit <= 1 && mouseRight && player[myPlayer].inventory[num79].maxStack > 1 && player[myPlayer].inventory[num79].type > 0 && (mouseItem.IsTheSameAs(player[myPlayer].inventory[num79]) || mouseItem.type == 0) && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
									{
										if (mouseItem.type == 0)
										{
											mouseItem = (Item)player[myPlayer].inventory[num79].Clone();
											mouseItem.stack = 0;
										}
										mouseItem.stack++;
										player[myPlayer].inventory[num79].stack--;
										if (player[myPlayer].inventory[num79].stack <= 0)
										{
											player[myPlayer].inventory[num79] = new Item();
										}
										Recipe.FindRecipes();
										soundInstanceMenuTick.Stop();
										soundInstanceMenuTick = soundMenuTick.CreateInstance();
										PlaySound(12);
										if (stackSplit == 0)
										{
											stackSplit = 15;
										}
										else
										{
											stackSplit = stackDelay;
										}
									}
								}
								MouseTextString = player[myPlayer].inventory[num79].name;
								toolTip = (Item)player[myPlayer].inventory[num79].ShallowClone();
								if (player[myPlayer].inventory[num79].stack > 1)
								{
									object mouseTextString2 = MouseTextString;
									MouseTextString = string.Concat(mouseTextString2, " (", player[myPlayer].inventory[num79].stack, ")");
								}
							}
							if (num76 != 0)
							{
								SpriteBatch spriteBatch46 = this.spriteBatch;
								Texture2D texture16 = inventoryBackTexture;
								Vector2 position47 = new Vector2(num77, num78);
								Rectangle? sourceRectangle16 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
								Color color27 = color6;
								float rotation46 = 0f;
								spriteBatch46.Draw(texture16, position47, sourceRectangle16, color27, rotation46, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							}
							else
							{
								SpriteBatch spriteBatch47 = this.spriteBatch;
								Texture2D texture17 = inventoryBack9Texture;
								Vector2 position48 = new Vector2(num77, num78);
								Rectangle? sourceRectangle17 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
								Color color28 = color6;
								float rotation47 = 0f;
								spriteBatch47.Draw(texture17, position48, sourceRectangle17, color28, rotation47, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							}
							_ = Color.White;
							ItemSlotRender.DrawItemInSlot(this.spriteBatch, color6, player[myPlayer].inventory[num79], new Vector2(num77, num78));
							if (num76 == 0)
							{
								string text45 = string.Concat(num79 + 1);
								if (text45 == "10")
								{
									text45 = "0";
								}
								Color color29 = color6;
								if (player[myPlayer].selectedItem == num79)
								{
									color29.R = 0;
									color29.B = 0;
									color29.G = byte.MaxValue;
									color29.A = 50;
								}
								SpriteBatch spriteBatch48 = this.spriteBatch;
								SpriteFont spriteFont31 = fontItemStack;
								string text46 = text45;
								Vector2 position49 = new Vector2(num77 + 6, num78 + 4);
								Color color30 = color29;
								float rotation48 = 0f;
								spriteBatch48.DrawString(spriteFont31, text46, position49, color30, rotation48, default(Vector2), inventoryScale * 0.8f, SpriteEffects.None, 0f);
							}
						}
					}
				}
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawHotbarLock, this.spriteBatch) || TMod.GetContinueMethod())
				{
					int num82 = 0;
					int num83 = 2;
					int num84 = 32;
					if (!player[myPlayer].hbLocked)
					{
						num82 = 1;
					}
					SpriteBatch spriteBatch49 = this.spriteBatch;
					Texture2D texture18 = HBLockTexture[num82];
					Vector2 position50 = new Vector2(num83, num84);
					Rectangle? sourceRectangle18 = new Rectangle(0, 0, HBLockTexture[num82].Width, HBLockTexture[num82].Height);
					Color color31 = color6;
					float rotation49 = 0f;
					spriteBatch49.Draw(texture18, position50, sourceRectangle18, color31, rotation49, default(Vector2), 0.9f, SpriteEffects.None, 0f);
					if (mouseX > num83 && (float)mouseX < (float)num83 + (float)HBLockTexture[num82].Width * 0.9f && mouseY > num84 && (float)mouseY < (float)num84 + (float)HBLockTexture[num82].Height * 0.9f)
					{
						player[myPlayer].mouseInterface = true;
						if (!player[myPlayer].hbLocked)
						{
							MouseText(Lang.inter[5], 0, 0);
							flag = true;
						}
						else
						{
							MouseText(Lang.inter[6], 0, 0);
							flag = true;
						}
						if (mouseLeft && mouseLeftRelease)
						{
							PlaySound(22);
							if (!player[myPlayer].hbLocked)
							{
								player[myPlayer].hbLocked = true;
							}
							else
							{
								player[myPlayer].hbLocked = false;
							}
						}
					}
				}
				if (armorHide)
				{
					armorAlpha -= 0.1f;
					if (armorAlpha < 0f)
					{
						armorAlpha = 0f;
					}
				}
				else
				{
					armorAlpha += 0.025f;
					if (armorAlpha > 1f)
					{
						armorAlpha = 1f;
					}
				}
				Color color32 = new Color((int)((float)(int)mouseTextColor * armorAlpha), (int)((float)(int)mouseTextColor * armorAlpha), (int)((float)(int)mouseTextColor * armorAlpha), (int)((float)(int)mouseTextColor * armorAlpha));
				armorHide = false;
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawNPCHouseToggle, this.spriteBatch) || TMod.GetContinueMethod())
				{
					int num85 = 1;
					int num86 = screenWidth - 152;
					int num87 = 128;
					if (netMode == 0)
					{
						num86 += 72;
					}
					if (showNPCs)
					{
						num85 = 0;
					}
					SpriteBatch spriteBatch50 = this.spriteBatch;
					Texture2D texture19 = npcToggleTexture[num85];
					Vector2 position51 = new Vector2(num86, num87);
					Rectangle? sourceRectangle19 = new Rectangle(0, 0, npcToggleTexture[num85].Width, npcToggleTexture[num85].Height);
					Color white18 = Color.White;
					float rotation50 = 0f;
					spriteBatch50.Draw(texture19, position51, sourceRectangle19, white18, rotation50, default(Vector2), 0.9f, SpriteEffects.None, 0f);
					if (mouseX > num86 && (float)mouseX < (float)num86 + (float)npcToggleTexture[num85].Width * 0.9f && mouseY > num87 && (float)mouseY < (float)num87 + (float)npcToggleTexture[num85].Height * 0.9f)
					{
						player[myPlayer].mouseInterface = true;
						if (mouseLeft && mouseLeftRelease)
						{
							PlaySound(12);
							if (!showNPCs)
							{
								showNPCs = true;
							}
							else
							{
								showNPCs = false;
							}
						}
					}
				}
				if (showNPCs)
				{
					if (!TMod.RunMethod(TMod.WorldHooks.PreDrawNPCHousingMenu, this.spriteBatch) || TMod.GetContinueMethod())
					{
						SpriteBatch spriteBatch51 = this.spriteBatch;
						SpriteFont spriteFont32 = fontMouseText;
						string text47 = Lang.inter[7];
						Vector2 position52 = new Vector2(screenWidth - 64 - 28 - 3, 152f);
						Color color33 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						float rotation51 = 0f;
						spriteBatch51.DrawString(spriteFont32, text47, position52, color33, rotation51, default(Vector2), 0.8f, SpriteEffects.None, 0f);
						if (mouseX > screenWidth - 64 - 28 && mouseX < (int)((float)(screenWidth - 64 - 28) + 56f * inventoryScale) && mouseY > 174 && mouseY < (int)(174f + 448f * inventoryScale))
						{
							player[myPlayer].mouseInterface = true;
						}
						int num88 = 0;
						string text48 = "";
						int num89 = 0;
						for (int num90 = 0; num90 < 12 || num89 < Config.npcDefs.townNPCList.Count; num90++)
						{
							if (num90 >= 12)
							{
								num90 = (int)Config.npcDefs.townNPCList[num89];
								num88 = num89;
								num89++;
							}
							bool flag5 = false;
							int num91 = 0;
							if (num90 == 0)
							{
								flag5 = true;
							}
							else
							{
								for (int num92 = 0; num92 < 200; num92++)
								{
									if (npc[num92].active && NPC.TypeToNum(npc[num92].type) == num90 && !npc[num92].dontDrawFace)
									{
										flag5 = true;
										num91 = num92;
										break;
									}
								}
							}
							if (!flag5)
							{
								continue;
							}
							int num93 = screenWidth - 64 - 28;
							if (num90 >= 12)
							{
								num93 -= 96;
							}
							int num94 = (int)(174f + (float)(num88 * 56) * inventoryScale);
							Color color34 = new Color(100, 100, 100, 100);
							if (screenHeight < 768 && num88 > 5)
							{
								num94 -= (int)(280f * inventoryScale);
								num93 -= 48;
							}
							if (mouseX >= num93 && (float)mouseX <= (float)num93 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num94 && (float)mouseY <= (float)num94 + (float)inventoryBackTexture.Height * inventoryScale)
							{
								flag = true;
								switch (num90)
								{
								case 0:
									text48 = Lang.inter[8];
									break;
								case 11:
									text48 = npc[num91].displayName;
									break;
								default:
									text48 = npc[num91].displayName + " the " + npc[num91].name;
									break;
								}
								player[myPlayer].mouseInterface = true;
								if (mouseLeftRelease && mouseLeft && mouseItem.type == 0)
								{
									PlaySound(12);
									mouseNPC = num90;
									mouseLeftRelease = false;
								}
							}
							SpriteBatch spriteBatch52 = this.spriteBatch;
							Texture2D texture20 = inventoryBack11Texture;
							Vector2 position53 = new Vector2(num93, num94);
							Rectangle? sourceRectangle20 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
							Color color35 = color6;
							float rotation52 = 0f;
							spriteBatch52.Draw(texture20, position53, sourceRectangle20, color35, rotation52, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							color34 = Color.White;
							int i3 = num90;
							float scale = 1f;
							float num95 = 0f;
							num95 = ((npcHeadTexture[i3].Width <= npcHeadTexture[i3].Height) ? ((float)npcHeadTexture[i3].Height) : ((float)npcHeadTexture[i3].Width));
							if (num95 > 36f)
							{
								scale = 36f / num95;
							}
							this.spriteBatch.Draw(npcHeadTexture[i3], new Vector2((float)num93 + 26f * inventoryScale, (float)num94 + 26f * inventoryScale), new Rectangle(0, 0, npcHeadTexture[i3].Width, npcHeadTexture[i3].Height), color34, 0f, new Vector2(npcHeadTexture[i3].Width / 2, npcHeadTexture[i3].Height / 2), scale, SpriteEffects.None, 0f);
							num88++;
						}
						if (text48 != "" && mouseItem.type == 0)
						{
							MouseText(text48, 0, 0);
						}
					}
				}
				else if (!TMod.RunMethod(TMod.WorldHooks.PreDrawPlayerEquipment, this.spriteBatch) || TMod.GetContinueMethod())
				{
					Vector2 vector6 = fontMouseText.MeasureString("Equip");
					Vector2 vector7 = fontMouseText.MeasureString(Lang.inter[45]);
					float num96 = vector6.X / vector7.X;
					SpriteBatch spriteBatch53 = this.spriteBatch;
					SpriteFont spriteFont33 = fontMouseText;
					string text49 = Lang.inter[45];
					Vector2 position54 = new Vector2(screenWidth - 64 - 28 + 4, 152f + (vector6.Y - vector6.Y * num96) / 2f);
					Color color36 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation53 = 0f;
					spriteBatch53.DrawString(spriteFont33, text49, position54, color36, rotation53, default(Vector2), 0.8f * num96, SpriteEffects.None, 0f);
					if (mouseX > screenWidth - 64 - 28 && mouseX < (int)((float)(screenWidth - 64 - 28) + 56f * inventoryScale) && mouseY > 174 && mouseY < (int)(174f + 448f * inventoryScale))
					{
						player[myPlayer].mouseInterface = true;
					}
					for (int num97 = 0; num97 < 8; num97++)
					{
						int num98 = screenWidth - 64 - 28;
						int num99 = (int)(174f + (float)(num97 * 56) * inventoryScale);
						new Color(100, 100, 100, 100);
						string text50 = "";
						if (num97 == 3)
						{
							text50 = Lang.inter[9];
						}
						if (num97 == 7)
						{
							text50 = player[myPlayer].statDefense + " " + Lang.inter[10];
						}
						Vector2 vector8 = fontMouseText.MeasureString(text50);
						SpriteBatch spriteBatch54 = this.spriteBatch;
						SpriteFont spriteFont34 = fontMouseText;
						string text51 = text50;
						Vector2 position55 = new Vector2((float)num98 - vector8.X - 10f, (float)num99 + (float)inventoryBackTexture.Height * 0.5f - vector8.Y * 0.5f);
						Color color37 = color32;
						float rotation54 = 0f;
						spriteBatch54.DrawString(spriteFont34, text51, position55, color37, rotation54, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (mouseX >= num98 && (float)mouseX <= (float)num98 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num99 && (float)mouseY <= (float)num99 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							armorHide = true;
							player[myPlayer].mouseInterface = true;
							if (mouseLeftRelease && mouseLeft && (mouseItem.type == 0 || (mouseItem.headSlot > -1 && num97 == 0) || (mouseItem.bodySlot > -1 && num97 == 1) || (mouseItem.legSlot > -1 && num97 == 2) || (mouseItem.accessory && num97 > 2 && !AccCheck(mouseItem, num97))) && mouseItem.CanEquip(player[myPlayer], num97))
							{
								Item item3 = mouseItem;
								mouseItem = player[myPlayer].armor[num97];
								mouseItem.OnUnequip(player[myPlayer], num97);
								player[myPlayer].armor[num97] = item3;
								player[myPlayer].armor[num97].OnEquip(player[myPlayer], num97);
								if (player[myPlayer].armor[num97].type == 0 || player[myPlayer].armor[num97].stack < 1)
								{
									player[myPlayer].armor[num97] = new Item();
								}
								if (mouseItem.type == 0 || mouseItem.stack < 1)
								{
									mouseItem = new Item();
								}
								if (mouseItem.type > 0 || player[myPlayer].armor[num97].type > 0)
								{
									Recipe.FindRecipes();
									PlaySound(7);
								}
							}
							MouseTextString = player[myPlayer].armor[num97].name;
							toolTip = (Item)player[myPlayer].armor[num97].ShallowClone();
							if (num97 <= 2)
							{
								toolTip.wornArmor = true;
							}
							if (player[myPlayer].armor[num97].stack > 1)
							{
								object mouseTextString3 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString3, " (", player[myPlayer].armor[num97].stack, ")");
							}
						}
						SpriteBatch spriteBatch55 = this.spriteBatch;
						Texture2D texture21 = inventoryBack3Texture;
						Vector2 vector9 = new Vector2(num98, num99);
						Rectangle? sourceRectangle21 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
						Color color38 = color6;
						float rotation55 = 0f;
						spriteBatch55.Draw(texture21, vector9, sourceRectangle21, color38, rotation55, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
						_ = Color.White;
						ItemSlotRender.DrawItemInSlot(spriteBatch55, color38, player[myPlayer].armor[num97], vector9);
					}
					Vector2 vector10 = fontMouseText.MeasureString("Social");
					Vector2 vector11 = fontMouseText.MeasureString(Lang.inter[11]);
					float num100 = vector10.X / vector11.X;
					SpriteBatch spriteBatch56 = this.spriteBatch;
					SpriteFont spriteFont35 = fontMouseText;
					string text52 = Lang.inter[11];
					Vector2 position56 = new Vector2(screenWidth - 64 - 28 - 44, 152f + (vector10.Y - vector10.Y * num100) / 2f);
					Color color39 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation56 = 0f;
					spriteBatch56.DrawString(spriteFont35, text52, position56, color39, rotation56, default(Vector2), 0.8f * num100, SpriteEffects.None, 0f);
					if (mouseX > screenWidth - 64 - 28 - 47 && mouseX < (int)((float)(screenWidth - 64 - 20 - 47) + 56f * inventoryScale) && mouseY > 174 && mouseY < (int)(174f + 168f * inventoryScale))
					{
						player[myPlayer].mouseInterface = true;
					}
					for (int num101 = 8; num101 < 11; num101++)
					{
						int num102 = screenWidth - 64 - 28 - 47;
						int num103 = (int)(174f + (float)((num101 - 8) * 56) * inventoryScale);
						new Color(100, 100, 100, 100);
						string text53 = "";
						switch (num101)
						{
						case 8:
							text53 = Lang.inter[12];
							break;
						case 9:
							text53 = Lang.inter[13];
							break;
						case 10:
							text53 = Lang.inter[14];
							break;
						}
						Vector2 vector12 = fontMouseText.MeasureString(text53);
						SpriteBatch spriteBatch57 = this.spriteBatch;
						SpriteFont spriteFont36 = fontMouseText;
						string text54 = text53;
						Vector2 position57 = new Vector2((float)num102 - vector12.X - 10f, (float)num103 + (float)inventoryBackTexture.Height * 0.5f - vector12.Y * 0.5f);
						Color color40 = color32;
						float rotation57 = 0f;
						spriteBatch57.DrawString(spriteFont36, text54, position57, color40, rotation57, default(Vector2), 1f, SpriteEffects.None, 0f);
						if (mouseX >= num102 && (float)mouseX <= (float)num102 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num103 && (float)mouseY <= (float)num103 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							player[myPlayer].mouseInterface = true;
							armorHide = true;
							if (mouseLeftRelease && mouseLeft)
							{
								if (mouseItem.type == 0 || (mouseItem.headSlot > -1 && num101 == 8) || (mouseItem.bodySlot > -1 && num101 == 9) || (mouseItem.legSlot > -1 && num101 == 10))
								{
									Item item4 = mouseItem;
									mouseItem = player[myPlayer].armor[num101];
									player[myPlayer].armor[num101] = item4;
									if (player[myPlayer].armor[num101].type == 0 || player[myPlayer].armor[num101].stack < 1)
									{
										player[myPlayer].armor[num101] = new Item();
									}
									if (mouseItem.type == 0 || mouseItem.stack < 1)
									{
										mouseItem = new Item();
									}
									if (mouseItem.type > 0 || player[myPlayer].armor[num101].type > 0)
									{
										Recipe.FindRecipes();
										PlaySound(7);
									}
								}
							}
							else if (mouseRight && mouseRightRelease && player[myPlayer].armor[num101].maxStack == 1)
							{
								player[myPlayer].armor[num101] = armorSwap(player[myPlayer].armor[num101]);
							}
							MouseTextString = player[myPlayer].armor[num101].name;
							toolTip = (Item)player[myPlayer].armor[num101].ShallowClone();
							toolTip.social = true;
							if (num101 <= 2)
							{
								toolTip.wornArmor = true;
							}
							if (player[myPlayer].armor[num101].stack > 1)
							{
								object mouseTextString4 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString4, " (", player[myPlayer].armor[num101].stack, ")");
							}
						}
						SpriteBatch spriteBatch58 = this.spriteBatch;
						Texture2D texture22 = inventoryBack8Texture;
						Vector2 vector13 = new Vector2(num102, num103);
						Rectangle? sourceRectangle22 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
						Color color41 = color6;
						float rotation58 = 0f;
						spriteBatch58.Draw(texture22, vector13, sourceRectangle22, color41, rotation58, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
						_ = Color.White;
						ItemSlotRender.DrawItemInSlot(spriteBatch58, color41, player[myPlayer].armor[num101], vector13);
					}
				}
				int num104 = (screenHeight - 600) / 2;
				int num105 = (int)((float)screenHeight / 600f * 250f);
				if (craftingHide)
				{
					craftingAlpha -= 0.1f;
					if (craftingAlpha < 0f)
					{
						craftingAlpha = 0f;
					}
				}
				else
				{
					craftingAlpha += 0.025f;
					if (craftingAlpha > 1f)
					{
						craftingAlpha = 1f;
					}
				}
				Color color42 = new Color((byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha), (byte)((float)(int)mouseTextColor * craftingAlpha));
				craftingHide = false;
				if (Config.npcInterface != null)
				{
					try
					{
						if (player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC == -1 || craftGuide)
						{
							Config.npcInterface.DropAll();
							Config.npcInterface = null;
						}
						else
						{
							Config.npcInterface.Draw(ref MouseTextString);
						}
					}
					catch (Exception)
					{
					}
				}
				else if (Config.tileInterface != null)
				{
					try
					{
						bool flag6 = false;
						int num106 = (int)(((double)player[myPlayer].position.X + (double)player[myPlayer].width * 0.5) / 16.0);
						int num107 = (int)(((double)player[myPlayer].position.Y + (double)player[myPlayer].height * 0.5) / 16.0);
						if ((float)num106 < Config.tileInterface.sourceLocation.X - 5f || (float)num106 > Config.tileInterface.sourceLocation.X + 6f || (float)num107 < Config.tileInterface.sourceLocation.Y - 4f || (float)num107 > Config.tileInterface.sourceLocation.Y + 5f)
						{
							flag6 = true;
						}
						if (flag6 || player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC != -1 || craftGuide)
						{
							PlaySound(11);
							Config.tileInterface.DropAll();
							Config.tileInterface = null;
						}
						else
						{
							Config.tileInterface.Draw(ref MouseTextString);
						}
					}
					catch (Exception)
					{
					}
				}
				if (reforge)
				{
					if (mouseReforge)
					{
						if ((double)reforgeScale < 1.3)
						{
							reforgeScale += 0.02f;
						}
					}
					else if (reforgeScale > 1f)
					{
						reforgeScale -= 0.02f;
					}
					if (player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC == -1 || craftGuide)
					{
						reforge = false;
						player[myPlayer].dropItemCheck();
						Recipe.FindRecipes();
					}
					else
					{
						int num108 = 101;
						int num109 = 241;
						string text55 = Lang.inter[46] + ": ";
						if (reforgeItem.type > 0)
						{
							int value2 = reforgeItem.value;
							string text56 = "";
							int num110 = 0;
							int num111 = 0;
							int num112 = 0;
							int num113 = 0;
							int num114 = value2;
							if (num114 < 1)
							{
								num114 = 1;
							}
							if (num114 >= 1000000)
							{
								num110 = num114 / 1000000;
								num114 -= num110 * 1000000;
							}
							if (num114 >= 10000)
							{
								num111 = num114 / 10000;
								num114 -= num111 * 10000;
							}
							if (num114 >= 100)
							{
								num112 = num114 / 100;
								num114 -= num112 * 100;
							}
							if (num114 >= 1)
							{
								num113 = num114;
							}
							if (num110 > 0)
							{
								object obj3 = text56;
								text56 = string.Concat(obj3, num110, " ", Lang.inter[15], " ");
							}
							if (num111 > 0)
							{
								object obj4 = text56;
								text56 = string.Concat(obj4, num111, " ", Lang.inter[16], " ");
							}
							if (num112 > 0)
							{
								object obj5 = text56;
								text56 = string.Concat(obj5, num112, " ", Lang.inter[17], " ");
							}
							if (num113 > 0)
							{
								object obj6 = text56;
								text56 = string.Concat(obj6, num113, " ", Lang.inter[18], " ");
							}
							float num115 = (float)(int)mouseTextColor / 255f;
							Color color43 = Color.White;
							if (num110 > 0)
							{
								color43 = new Color((byte)(220f * num115), (byte)(220f * num115), (byte)(198f * num115), mouseTextColor);
							}
							else if (num111 > 0)
							{
								color43 = new Color((byte)(224f * num115), (byte)(201f * num115), (byte)(92f * num115), mouseTextColor);
							}
							else if (num112 > 0)
							{
								color43 = new Color((byte)(181f * num115), (byte)(192f * num115), (byte)(193f * num115), mouseTextColor);
							}
							else if (num113 > 0)
							{
								color43 = new Color((byte)(246f * num115), (byte)(138f * num115), (byte)(96f * num115), mouseTextColor);
							}
							SpriteBatch spriteBatch59 = this.spriteBatch;
							SpriteFont spriteFont37 = fontMouseText;
							string text57 = text56;
							Vector2 position58 = new Vector2((float)(num108 + 50) + fontMouseText.MeasureString(text55).X, num109);
							Color color44 = color43;
							float rotation59 = 0f;
							spriteBatch59.DrawString(spriteFont37, text57, position58, color44, rotation59, default(Vector2), 1f, SpriteEffects.None, 0f);
							int num116 = num108 + 70;
							int num117 = num109 + 40;
							this.spriteBatch.Draw(reforgeTexture, new Vector2(num116, num117), new Rectangle(0, 0, reforgeTexture.Width, reforgeTexture.Height), Color.White, 0f, new Vector2(reforgeTexture.Width / 2, reforgeTexture.Height / 2), reforgeScale, SpriteEffects.None, 0f);
							if (mouseX > num116 - reforgeTexture.Width / 2 && mouseX < num116 + reforgeTexture.Width / 2 && mouseY > num117 - reforgeTexture.Height / 2 && mouseY < num117 + reforgeTexture.Height / 2)
							{
								MouseTextString = Lang.inter[19];
								if (!mouseReforge)
								{
									PlaySound(12);
								}
								mouseReforge = true;
								player[myPlayer].mouseInterface = true;
								if (mouseLeftRelease && mouseLeft && player[myPlayer].BuyItem(value2))
								{
									reforgeItem.Prefix(-2);
									reforgeItem.position.X = player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(reforgeItem.width / 2);
									reforgeItem.position.Y = player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(reforgeItem.height / 2);
									ItemText.NewText(reforgeItem, reforgeItem.stack);
									PlaySound(2, -1, -1, 37);
								}
							}
							else
							{
								mouseReforge = false;
							}
						}
						else
						{
							text55 = Lang.inter[20];
						}
						SpriteBatch spriteBatch60 = this.spriteBatch;
						SpriteFont spriteFont38 = fontMouseText;
						string text58 = text55;
						Vector2 position59 = new Vector2(num108 + 50, num109);
						Color color45 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						float rotation60 = 0f;
						spriteBatch60.DrawString(spriteFont38, text58, position59, color45, rotation60, default(Vector2), 1f, SpriteEffects.None, 0f);
						new Color(100, 100, 100, 100);
						if (mouseX >= num108 && (float)mouseX <= (float)num108 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num109 && (float)mouseY <= (float)num109 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							player[myPlayer].mouseInterface = true;
							craftingHide = true;
							if (mouseItem.Prefix(-3) || mouseItem.type == 0)
							{
								if (mouseLeftRelease && mouseLeft)
								{
									Item item5 = mouseItem;
									mouseItem = reforgeItem;
									reforgeItem = item5;
									if (reforgeItem.type == 0 || reforgeItem.stack < 1)
									{
										reforgeItem = new Item();
									}
									if (mouseItem.IsTheSameAs(reforgeItem) && reforgeItem.stack != reforgeItem.maxStack && mouseItem.stack != mouseItem.maxStack)
									{
										if (mouseItem.stack + reforgeItem.stack <= mouseItem.maxStack)
										{
											reforgeItem.stack += mouseItem.stack;
											mouseItem.stack = 0;
										}
										else
										{
											int num118 = mouseItem.maxStack - reforgeItem.stack;
											reforgeItem.stack += num118;
											mouseItem.stack -= num118;
										}
									}
									if (mouseItem.type == 0 || mouseItem.stack < 1)
									{
										mouseItem = new Item();
									}
									if (mouseItem.type > 0 || reforgeItem.type > 0)
									{
										Recipe.FindRecipes();
										PlaySound(7);
									}
								}
								else if (stackSplit <= 1 && mouseRight && (mouseItem.IsTheSameAs(reforgeItem) || mouseItem.type == 0) && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
								{
									if (mouseItem.type == 0)
									{
										mouseItem = (Item)reforgeItem.Clone();
										mouseItem.stack = 0;
									}
									mouseItem.stack++;
									reforgeItem.stack--;
									if (reforgeItem.stack <= 0)
									{
										reforgeItem = new Item();
									}
									Recipe.FindRecipes();
									soundInstanceMenuTick.Stop();
									soundInstanceMenuTick = soundMenuTick.CreateInstance();
									PlaySound(12);
									if (stackSplit == 0)
									{
										stackSplit = 15;
									}
									else
									{
										stackSplit = stackDelay;
									}
								}
							}
							MouseTextString = reforgeItem.name;
							toolTip = (Item)reforgeItem.ShallowClone();
							if (reforgeItem.stack > 1)
							{
								object mouseTextString5 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString5, " (", reforgeItem.stack, ")");
							}
						}
						SpriteBatch spriteBatch61 = this.spriteBatch;
						Texture2D texture23 = inventoryBack4Texture;
						Vector2 vector14 = new Vector2(num108, num109);
						Rectangle? sourceRectangle23 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
						Color color46 = color6;
						float rotation61 = 0f;
						spriteBatch61.Draw(texture23, vector14, sourceRectangle23, color46, rotation61, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
						_ = Color.White;
						ItemSlotRender.DrawItemInSlot(spriteBatch61, color46, reforgeItem, vector14);
					}
				}
				else
				{
					bool forceGuideMenu = ForceGuideMenu;
					ForceGuideMenu = false;
					if (craftGuide || forceGuideMenu)
					{
						if (!forceGuideMenu && (player[myPlayer].chest != -1 || npcShop != 0 || player[myPlayer].talkNPC == -1 || reforge))
						{
							craftGuide = false;
							player[myPlayer].dropItemCheck();
							Recipe.FindRecipes();
						}
						else
						{
							int num119 = 73;
							int num120 = 331;
							num120 += num104;
							string text59;
							if (guideItem.type > 0)
							{
								text59 = Lang.inter[21] + " " + guideItem.name;
								SpriteBatch spriteBatch62 = this.spriteBatch;
								SpriteFont spriteFont39 = fontMouseText;
								string text60 = Lang.inter[22];
								Vector2 position60 = new Vector2(num119, num120 + 118);
								Color color47 = color42;
								float rotation62 = 0f;
								spriteBatch62.DrawString(spriteFont39, text60, position60, color47, rotation62, default(Vector2), 1f, SpriteEffects.None, 0f);
								int num121 = focusRecipe;
								int num122 = 0;
								for (int num123 = 0; num123 < Recipe.maxRequirements; num123++)
								{
									int num124 = (num123 + 1) * 26;
									if (recipe[availableRecipe[num121]].requiredTile[num123] == -1)
									{
										if (num123 == 0 && !recipe[availableRecipe[num121]].needWater && !recipe[availableRecipe[num121]].needLava)
										{
											SpriteBatch spriteBatch63 = this.spriteBatch;
											SpriteFont spriteFont40 = fontMouseText;
											string text61 = Lang.inter[23];
											Vector2 position61 = new Vector2(num119, num120 + 118 + num124);
											Color color48 = color42;
											float rotation63 = 0f;
											spriteBatch63.DrawString(spriteFont40, text61, position61, color48, rotation63, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
										break;
									}
									num122++;
									SpriteBatch spriteBatch64 = this.spriteBatch;
									SpriteFont spriteFont41 = fontMouseText;
									string text62 = tileName[recipe[availableRecipe[num121]].requiredTile[num123]];
									Vector2 position62 = new Vector2(num119, num120 + 118 + num124);
									Color color49 = color42;
									float rotation64 = 0f;
									spriteBatch64.DrawString(spriteFont41, text62, position62, color49, rotation64, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								if (recipe[availableRecipe[num121]].needWater)
								{
									int num125 = (num122 + 1) * 26;
									SpriteBatch spriteBatch65 = this.spriteBatch;
									SpriteFont spriteFont42 = fontMouseText;
									string text63 = Lang.inter[53];
									Vector2 position63 = new Vector2(num119, num120 + 118 + num125);
									Color color50 = color42;
									float rotation65 = 0f;
									spriteBatch65.DrawString(spriteFont42, text63, position63, color50, rotation65, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
								if (recipe[availableRecipe[num121]].needLava)
								{
									int num126 = (num122 + 1 + (recipe[availableRecipe[num121]].needWater ? 1 : 0)) * 26;
									SpriteBatch spriteBatch66 = this.spriteBatch;
									SpriteFont spriteFont43 = fontMouseText;
									string text64 = "Lava";
									Vector2 position64 = new Vector2(num119, num120 + 118 + num126);
									Color color51 = color42;
									float rotation66 = 0f;
									spriteBatch66.DrawString(spriteFont43, text64, position64, color51, rotation66, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
							else
							{
								text59 = Lang.inter[24];
							}
							SpriteBatch spriteBatch67 = this.spriteBatch;
							SpriteFont spriteFont44 = fontMouseText;
							string text65 = text59;
							Vector2 position65 = new Vector2(num119 + 50, num120 + 12);
							Color color52 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
							float rotation67 = 0f;
							spriteBatch67.DrawString(spriteFont44, text65, position65, color52, rotation67, default(Vector2), 1f, SpriteEffects.None, 0f);
							new Color(100, 100, 100, 100);
							if (mouseX >= num119 && (float)mouseX <= (float)num119 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num120 && (float)mouseY <= (float)num120 + (float)inventoryBackTexture.Height * inventoryScale)
							{
								player[myPlayer].mouseInterface = true;
								craftingHide = true;
								if (mouseItem.material || mouseItem.type == 0)
								{
									if (mouseLeftRelease && mouseLeft)
									{
										Item item6 = mouseItem;
										mouseItem = guideItem;
										guideItem = item6;
										if (guideItem.type == 0 || guideItem.stack < 1)
										{
											guideItem = new Item();
										}
										if (mouseItem.IsTheSameAs(guideItem) && guideItem.stack != guideItem.maxStack && mouseItem.stack != mouseItem.maxStack)
										{
											if (mouseItem.stack + guideItem.stack <= mouseItem.maxStack)
											{
												guideItem.stack += mouseItem.stack;
												mouseItem.stack = 0;
											}
											else
											{
												int num127 = mouseItem.maxStack - guideItem.stack;
												guideItem.stack += num127;
												mouseItem.stack -= num127;
											}
										}
										if (mouseItem.type == 0 || mouseItem.stack < 1)
										{
											mouseItem = new Item();
										}
										if (mouseItem.type > 0 || guideItem.type > 0)
										{
											Recipe.FindRecipes();
											PlaySound(7);
										}
									}
									else if (stackSplit <= 1 && mouseRight && (mouseItem.IsTheSameAs(guideItem) || mouseItem.type == 0) && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
									{
										if (mouseItem.type == 0)
										{
											mouseItem = (Item)guideItem.Clone();
											mouseItem.stack = 0;
										}
										mouseItem.stack++;
										guideItem.stack--;
										if (guideItem.stack <= 0)
										{
											guideItem = new Item();
										}
										Recipe.FindRecipes();
										soundInstanceMenuTick.Stop();
										soundInstanceMenuTick = soundMenuTick.CreateInstance();
										PlaySound(12);
										if (stackSplit == 0)
										{
											stackSplit = 15;
										}
										else
										{
											stackSplit = stackDelay;
										}
									}
								}
								MouseTextString = guideItem.name;
								toolTip = (Item)guideItem.ShallowClone();
								if (guideItem.stack > 1)
								{
									object mouseTextString6 = MouseTextString;
									MouseTextString = string.Concat(mouseTextString6, " (", guideItem.stack, ")");
								}
							}
							SpriteBatch spriteBatch68 = this.spriteBatch;
							Texture2D texture24 = inventoryBack4Texture;
							Vector2 vector15 = new Vector2(num119, num120);
							Rectangle? sourceRectangle24 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
							Color color53 = color6;
							float rotation68 = 0f;
							spriteBatch68.Draw(texture24, vector15, sourceRectangle24, color53, rotation68, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							_ = Color.White;
							ItemSlotRender.DrawItemInSlot(spriteBatch68, color53, guideItem, vector15);
						}
					}
				}
				if (!reforge && (!TMod.RunMethod(TMod.WorldHooks.PreDrawAvailableRecipes, this.spriteBatch) || TMod.GetContinueMethod()))
				{
					if (numAvailableRecipes > 0)
					{
						SpriteBatch spriteBatch69 = this.spriteBatch;
						SpriteFont spriteFont45 = fontMouseText;
						string text66 = Lang.inter[25];
						Vector2 position66 = new Vector2(76f, 414 + num104);
						Color color54 = color42;
						float rotation69 = 0f;
						spriteBatch69.DrawString(spriteFont45, text66, position66, color54, rotation69, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					for (int num128 = 0; num128 < Recipe.maxRecipes; num128++)
					{
						inventoryScale = 100f / (Math.Abs(availableRecipeY[num128]) + 100f);
						if ((double)inventoryScale < 0.75)
						{
							inventoryScale = 0.75f;
						}
						if (availableRecipeY[num128] < (float)((num128 - focusRecipe) * 65))
						{
							if (availableRecipeY[num128] == 0f)
							{
								PlaySound(12);
							}
							availableRecipeY[num128] += 6.5f;
						}
						else if (availableRecipeY[num128] > (float)((num128 - focusRecipe) * 65))
						{
							if (availableRecipeY[num128] == 0f)
							{
								PlaySound(12);
							}
							availableRecipeY[num128] -= 6.5f;
						}
						if (num128 >= numAvailableRecipes || !(Math.Abs(availableRecipeY[num128]) <= (float)num105))
						{
							continue;
						}
						int num129 = (int)(46f - 26f * inventoryScale);
						int num130 = (int)(410f + availableRecipeY[num128] * inventoryScale - 30f * inventoryScale + (float)num104);
						double num131 = color6.A + 50;
						double num132 = 255.0;
						if (Math.Abs(availableRecipeY[num128]) > (float)(num105 - 100))
						{
							num131 = (double)(150f * (100f - (Math.Abs(availableRecipeY[num128]) - (float)(num105 - 100)))) * 0.01;
							num132 = (double)(255f * (100f - (Math.Abs(availableRecipeY[num128]) - (float)(num105 - 100)))) * 0.01;
						}
						new Color((byte)num131, (byte)num131, (byte)num131, (byte)num131);
						new Color((byte)num132, (byte)num132, (byte)num132, (byte)num132);
						if (mouseX >= num129 && (float)mouseX <= (float)num129 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num130 && (float)mouseY <= (float)num130 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							player[myPlayer].mouseInterface = true;
							if (focusRecipe == num128 && guideItem.type == 0)
							{
								if (mouseItem.type == 0 || (mouseItem.IsTheSameAs(recipe[availableRecipe[num128]].createItem) && mouseItem.stack + recipe[availableRecipe[num128]].createItem.stack <= mouseItem.maxStack))
								{
									if (mouseLeftRelease && mouseLeft)
									{
										int stack = mouseItem.stack;
										mouseItem = (Item)recipe[availableRecipe[num128]].createItem.Clone();
										mouseItem.Prefix(-1);
										mouseItem.stack += stack;
										mouseItem.position.X = player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(mouseItem.width / 2);
										mouseItem.position.Y = player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(mouseItem.height / 2);
										ItemText.NewText(mouseItem, recipe[availableRecipe[num128]].createItem.stack);
										recipe[availableRecipe[num128]].Create(mouseItem);
										if (mouseItem.type > 0 || recipe[availableRecipe[num128]].createItem.type > 0)
										{
											PlaySound(7);
										}
									}
									else if (stackSplit <= 1 && mouseRight && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
									{
										if (stackSplit == 0)
										{
											stackSplit = 15;
										}
										else
										{
											stackSplit = stackDelay;
										}
										int stack2 = mouseItem.stack;
										mouseItem = (Item)recipe[availableRecipe[num128]].createItem.Clone();
										mouseItem.Prefix(-1);
										mouseItem.stack += stack2;
										mouseItem.position.X = player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(mouseItem.width / 2);
										mouseItem.position.Y = player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(mouseItem.height / 2);
										ItemText.NewText(mouseItem, recipe[availableRecipe[num128]].createItem.stack);
										recipe[availableRecipe[num128]].Create(mouseItem);
										if (mouseItem.type > 0 || recipe[availableRecipe[num128]].createItem.type > 0)
										{
											PlaySound(7);
										}
									}
								}
							}
							else if (mouseLeftRelease && mouseLeft)
							{
								focusRecipe = num128;
							}
							craftingHide = true;
							MouseTextString = recipe[availableRecipe[num128]].createItem.name;
							toolTip = (Item)recipe[availableRecipe[num128]].createItem.ShallowClone();
							if (recipe[availableRecipe[num128]].createItem.stack > 1)
							{
								object mouseTextString7 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString7, " (", recipe[availableRecipe[num128]].createItem.stack, ")");
							}
						}
						if (numAvailableRecipes > 0)
						{
							num131 -= 50.0;
							if (num131 < 0.0)
							{
								num131 = 0.0;
							}
							SpriteBatch spriteBatch70 = this.spriteBatch;
							Texture2D texture25 = inventoryBack4Texture;
							Vector2 vector16 = new Vector2(num129, num130);
							Rectangle? sourceRectangle25 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
							Color color55 = new Color((byte)num131, (byte)num131, (byte)num131, (byte)num131);
							float rotation70 = 0f;
							spriteBatch70.Draw(texture25, vector16, sourceRectangle25, color55, rotation70, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							ItemSlotRender.DrawItemInSlot(spriteBatch70, color55, recipe[availableRecipe[num128]].createItem, vector16);
						}
					}
					if (numAvailableRecipes > 0)
					{
						for (int num133 = 0; num133 < Recipe.maxRequirements && recipe[availableRecipe[focusRecipe]].requiredItem[num133].type != 0; num133++)
						{
							int num134 = 80 + num133 * 40;
							int num135 = 380 + num104;
							double num136 = color6.A + 50;
							double num137 = 255.0;
							Color white19 = Color.White;
							Color white20 = Color.White;
							num136 = (float)(color6.A + 50) - Math.Abs(availableRecipeY[focusRecipe]) * 2f;
							num137 = 255f - Math.Abs(availableRecipeY[focusRecipe]) * 2f;
							if (num136 < 0.0)
							{
								num136 = 0.0;
							}
							if (num137 < 0.0)
							{
								num137 = 0.0;
							}
							white19.R = (byte)num136;
							white19.G = (byte)num136;
							white19.B = (byte)num136;
							white19.A = (byte)num136;
							white20.R = (byte)num137;
							white20.G = (byte)num137;
							white20.B = (byte)num137;
							white20.A = (byte)num137;
							inventoryScale = 0.6f;
							if (num136 == 0.0)
							{
								break;
							}
							if (mouseX >= num134 && (float)mouseX <= (float)num134 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num135 && (float)mouseY <= (float)num135 + (float)inventoryBackTexture.Height * inventoryScale)
							{
								craftingHide = true;
								player[myPlayer].mouseInterface = true;
								MouseTextString = recipe[availableRecipe[focusRecipe]].requiredItem[num133].name;
								toolTip = (Item)recipe[availableRecipe[focusRecipe]].requiredItem[num133].ShallowClone();
								if (recipe[availableRecipe[focusRecipe]].requiredItem[num133].stack > 1)
								{
									object mouseTextString8 = MouseTextString;
									MouseTextString = string.Concat(mouseTextString8, " (", recipe[availableRecipe[focusRecipe]].requiredItem[num133].stack, ")");
								}
							}
							num136 -= 50.0;
							if (num136 < 0.0)
							{
								num136 = 0.0;
							}
							SpriteBatch spriteBatch71 = this.spriteBatch;
							Texture2D texture26 = inventoryBack4Texture;
							Vector2 vector17 = new Vector2(num134, num135);
							Rectangle? sourceRectangle26 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
							Color color56 = new Color((byte)num136, (byte)num136, (byte)num136, (byte)num136);
							float rotation71 = 0f;
							spriteBatch71.Draw(texture26, vector17, sourceRectangle26, color56, rotation71, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							ItemSlotRender.DrawItemInSlot(spriteBatch71, color56, recipe[availableRecipe[focusRecipe]].requiredItem[num133], vector17);
						}
					}
				}
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawInventoryCoins, this.spriteBatch) || TMod.GetContinueMethod())
				{
					Vector2 vector18 = fontMouseText.MeasureString("Coins");
					Vector2 vector19 = fontMouseText.MeasureString(Lang.inter[26]);
					float num138 = vector18.X / vector19.X;
					SpriteBatch spriteBatch72 = this.spriteBatch;
					SpriteFont spriteFont46 = fontMouseText;
					string text67 = Lang.inter[26];
					Vector2 position67 = new Vector2(496f, 84f + (vector18.Y - vector18.Y * num138) / 2f);
					Color color57 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation72 = 0f;
					spriteBatch72.DrawString(spriteFont46, text67, position67, color57, rotation72, default(Vector2), 0.75f * num138, SpriteEffects.None, 0f);
					inventoryScale = 0.6f;
					for (int num139 = 0; num139 < 4; num139++)
					{
						int num140 = 497;
						int num141 = (int)(85f + (float)(num139 * 56) * inventoryScale + 20f);
						int num142 = num139 + 40;
						new Color(100, 100, 100, 100);
						if (mouseX >= num140 && (float)mouseX <= (float)num140 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num141 && (float)mouseY <= (float)num141 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							player[myPlayer].mouseInterface = true;
							if (mouseLeftRelease && mouseLeft)
							{
								if (keyState.IsKeyDown(Keys.LeftShift))
								{
									if (player[myPlayer].inventory[num142].type > 0)
									{
										if (npcShop > 0)
										{
											if (player[myPlayer].SellItem(player[myPlayer].inventory[num142].value, player[myPlayer].inventory[num142].stack))
											{
												shop[npcShop].AddShop(player[myPlayer].inventory[num142]);
												player[myPlayer].inventory[num142].SetDefaults(0);
												PlaySound(18);
											}
											else if (player[myPlayer].inventory[num142].value == 0)
											{
												shop[npcShop].AddShop(player[myPlayer].inventory[num142]);
												player[myPlayer].inventory[num142].SetDefaults(0);
												PlaySound(7);
											}
										}
										else
										{
											PlaySound(7);
											trashItem = (Item)player[myPlayer].inventory[num142].Clone();
											player[myPlayer].inventory[num142].SetDefaults(0);
											Recipe.FindRecipes();
										}
									}
								}
								else if ((player[myPlayer].selectedItem != num142 || player[myPlayer].itemAnimation <= 0) && (mouseItem.type == 0 || mouseItem.type == 71 || mouseItem.type == 72 || mouseItem.type == 73 || mouseItem.type == 74))
								{
									Item item7 = mouseItem;
									mouseItem = player[myPlayer].inventory[num142];
									player[myPlayer].inventory[num142] = item7;
									if (player[myPlayer].inventory[num142].type == 0 || player[myPlayer].inventory[num142].stack < 1)
									{
										player[myPlayer].inventory[num142] = new Item();
									}
									if (mouseItem.IsTheSameAs(player[myPlayer].inventory[num142]) && player[myPlayer].inventory[num142].stack != player[myPlayer].inventory[num142].maxStack && mouseItem.stack != mouseItem.maxStack)
									{
										if (mouseItem.stack + player[myPlayer].inventory[num142].stack <= mouseItem.maxStack)
										{
											player[myPlayer].inventory[num142].stack += mouseItem.stack;
											mouseItem.stack = 0;
										}
										else
										{
											int num143 = mouseItem.maxStack - player[myPlayer].inventory[num142].stack;
											player[myPlayer].inventory[num142].stack += num143;
											mouseItem.stack -= num143;
										}
									}
									if (mouseItem.type == 0 || mouseItem.stack < 1)
									{
										mouseItem = new Item();
									}
									if (mouseItem.type > 0 || player[myPlayer].inventory[num142].type > 0)
									{
										PlaySound(7);
									}
									Recipe.FindRecipes();
								}
							}
							else if (stackSplit <= 1 && mouseRight && (mouseItem.IsTheSameAs(player[myPlayer].inventory[num142]) || mouseItem.type == 0) && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
							{
								if (mouseItem.type == 0)
								{
									mouseItem = (Item)player[myPlayer].inventory[num142].Clone();
									mouseItem.stack = 0;
								}
								mouseItem.stack++;
								player[myPlayer].inventory[num142].stack--;
								if (player[myPlayer].inventory[num142].stack <= 0)
								{
									player[myPlayer].inventory[num142] = new Item();
								}
								Recipe.FindRecipes();
								soundInstanceMenuTick.Stop();
								soundInstanceMenuTick = soundMenuTick.CreateInstance();
								PlaySound(12);
								if (stackSplit == 0)
								{
									stackSplit = 15;
								}
								else
								{
									stackSplit = stackDelay;
								}
							}
							MouseTextString = player[myPlayer].inventory[num142].name;
							toolTip = (Item)player[myPlayer].inventory[num142].ShallowClone();
							if (player[myPlayer].inventory[num142].stack > 1)
							{
								object mouseTextString9 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString9, " (", player[myPlayer].inventory[num142].stack, ")");
							}
						}
						SpriteBatch spriteBatch73 = this.spriteBatch;
						Texture2D texture27 = inventoryBackTexture;
						Vector2 vector20 = new Vector2(num140, num141);
						Rectangle? sourceRectangle27 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
						Color color58 = color6;
						float rotation73 = 0f;
						spriteBatch73.Draw(texture27, vector20, sourceRectangle27, color58, rotation73, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
						_ = Color.White;
						ItemSlotRender.DrawItemInSlot(spriteBatch73, color58, player[myPlayer].inventory[num142], vector20);
					}
				}
				if (!TMod.RunMethod(TMod.WorldHooks.PreDrawInventoryAmmo, this.spriteBatch) || TMod.GetContinueMethod())
				{
					Vector2 vector21 = fontMouseText.MeasureString("Ammo");
					Vector2 vector22 = fontMouseText.MeasureString(Lang.inter[27]);
					float num144 = vector21.X / vector22.X;
					SpriteBatch spriteBatch74 = this.spriteBatch;
					SpriteFont spriteFont47 = fontMouseText;
					string text68 = Lang.inter[27];
					Vector2 position68 = new Vector2(532f, 84f + (vector21.Y - vector21.Y * num144) / 2f);
					Color color59 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation74 = 0f;
					spriteBatch74.DrawString(spriteFont47, text68, position68, color59, rotation74, default(Vector2), 0.75f * num144, SpriteEffects.None, 0f);
					inventoryScale = 0.6f;
					for (int num145 = 0; num145 < 4; num145++)
					{
						int num146 = 534;
						int num147 = (int)(85f + (float)(num145 * 56) * inventoryScale + 20f);
						int num148 = 44 + num145;
						new Color(100, 100, 100, 100);
						if (mouseX >= num146 && (float)mouseX <= (float)num146 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num147 && (float)mouseY <= (float)num147 + (float)inventoryBackTexture.Height * inventoryScale)
						{
							player[myPlayer].mouseInterface = true;
							if (mouseLeftRelease && mouseLeft)
							{
								if (keyState.IsKeyDown(Keys.LeftShift))
								{
									if (player[myPlayer].inventory[num148].type > 0)
									{
										if (npcShop > 0)
										{
											if (player[myPlayer].SellItem(player[myPlayer].inventory[num148].value, player[myPlayer].inventory[num148].stack))
											{
												shop[npcShop].AddShop(player[myPlayer].inventory[num148]);
												player[myPlayer].inventory[num148].SetDefaults(0);
												PlaySound(18);
											}
											else if (player[myPlayer].inventory[num148].value == 0)
											{
												shop[npcShop].AddShop(player[myPlayer].inventory[num148]);
												player[myPlayer].inventory[num148].SetDefaults(0);
												PlaySound(7);
											}
										}
										else
										{
											PlaySound(7);
											trashItem = (Item)player[myPlayer].inventory[num148].Clone();
											player[myPlayer].inventory[num148].SetDefaults(0);
											Recipe.FindRecipes();
										}
									}
								}
								else if ((player[myPlayer].selectedItem != num148 || player[myPlayer].itemAnimation <= 0) && (mouseItem.type == 0 || mouseItem.ammo > 0 || mouseItem.type == 530))
								{
									Item item8 = mouseItem;
									mouseItem = player[myPlayer].inventory[num148];
									player[myPlayer].inventory[num148] = item8;
									if (player[myPlayer].inventory[num148].type == 0 || player[myPlayer].inventory[num148].stack < 1)
									{
										player[myPlayer].inventory[num148] = new Item();
									}
									if (mouseItem.IsTheSameAs(player[myPlayer].inventory[num148]) && player[myPlayer].inventory[num148].stack != player[myPlayer].inventory[num148].maxStack && mouseItem.stack != mouseItem.maxStack)
									{
										if (mouseItem.stack + player[myPlayer].inventory[num148].stack <= mouseItem.maxStack)
										{
											player[myPlayer].inventory[num148].stack += mouseItem.stack;
											mouseItem.stack = 0;
										}
										else
										{
											int num149 = mouseItem.maxStack - player[myPlayer].inventory[num148].stack;
											player[myPlayer].inventory[num148].stack += num149;
											mouseItem.stack -= num149;
										}
									}
									if (mouseItem.type == 0 || mouseItem.stack < 1)
									{
										mouseItem = new Item();
									}
									if (mouseItem.type > 0 || player[myPlayer].inventory[num148].type > 0)
									{
										PlaySound(7);
									}
									Recipe.FindRecipes();
								}
							}
							else if (stackSplit <= 1 && mouseRight && (mouseItem.IsTheSameAs(player[myPlayer].inventory[num148]) || mouseItem.type == 0) && (mouseItem.stack < mouseItem.maxStack || mouseItem.type == 0))
							{
								if (mouseItem.type == 0)
								{
									mouseItem = (Item)player[myPlayer].inventory[num148].Clone();
									mouseItem.stack = 0;
								}
								mouseItem.stack++;
								player[myPlayer].inventory[num148].stack--;
								if (player[myPlayer].inventory[num148].stack <= 0)
								{
									player[myPlayer].inventory[num148] = new Item();
								}
								Recipe.FindRecipes();
								soundInstanceMenuTick.Stop();
								soundInstanceMenuTick = soundMenuTick.CreateInstance();
								PlaySound(12);
								if (stackSplit == 0)
								{
									stackSplit = 15;
								}
								else
								{
									stackSplit = stackDelay;
								}
							}
							MouseTextString = player[myPlayer].inventory[num148].name;
							toolTip = (Item)player[myPlayer].inventory[num148].ShallowClone();
							if (player[myPlayer].inventory[num148].stack > 1)
							{
								object mouseTextString10 = MouseTextString;
								MouseTextString = string.Concat(mouseTextString10, " (", player[myPlayer].inventory[num148].stack, ")");
							}
						}
						SpriteBatch spriteBatch75 = this.spriteBatch;
						Texture2D texture28 = inventoryBackTexture;
						Vector2 vector23 = new Vector2(num146, num147);
						Rectangle? sourceRectangle28 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
						Color color60 = color6;
						float rotation75 = 0f;
						spriteBatch75.Draw(texture28, vector23, sourceRectangle28, color60, rotation75, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
						_ = Color.White;
						ItemSlotRender.DrawItemInSlot(spriteBatch75, color60, player[myPlayer].inventory[num148], vector23);
					}
				}
				if (npcShop > 0 && (!playerInventory || player[myPlayer].talkNPC == -1))
				{
					npcShop = 0;
				}
				if (npcShop > 0 && (!TMod.RunMethod(TMod.WorldHooks.PreDrawNPCShop, this.spriteBatch) || TMod.GetContinueMethod()))
				{
					SpriteBatch spriteBatch76 = this.spriteBatch;
					SpriteFont spriteFont48 = fontMouseText;
					string text69 = Lang.inter[28];
					Vector2 position69 = new Vector2(284f, 210f);
					Color color61 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation76 = 0f;
					spriteBatch76.DrawString(spriteFont48, text69, position69, color61, rotation76, default(Vector2), 1f, SpriteEffects.None, 0f);
					inventoryScale = 0.75f;
					if (mouseX > 73 && mouseX < (int)(73f + 280f * inventoryScale) && mouseY > 210 && mouseY < (int)(210f + 224f * inventoryScale))
					{
						player[myPlayer].mouseInterface = true;
					}
					for (int num150 = 0; num150 < 5; num150++)
					{
						for (int num151 = 0; num151 < 4; num151++)
						{
							int num152 = (int)(73f + (float)(num150 * 56) * inventoryScale);
							int num153 = (int)(210f + (float)(num151 * 56) * inventoryScale);
							int num154 = num150 + num151 * 5;
							new Color(100, 100, 100, 100);
							if (mouseX >= num152 && (float)mouseX <= (float)num152 + (float)inventoryBackTexture.Width * inventoryScale && mouseY >= num153 && (float)mouseY <= (float)num153 + (float)inventoryBackTexture.Height * inventoryScale)
							{
								player[myPlayer].mouseInterface = true;
								if (mouseLeftRelease && mouseLeft)
								{
									if (mouseItem.type == 0)
									{
										if ((player[myPlayer].selectedItem != num154 || player[myPlayer].itemAnimation <= 0) && player[myPlayer].BuyItem(shop[npcShop].item[num154].value * shop[npcShop].item[num154].stack))
										{
											if (shop[npcShop].item[num154].buyOnce)
											{
												int prefix = shop[npcShop].item[num154].prefix;
												mouseItem.netDefaults(shop[npcShop].item[num154].netID);
												mouseItem.Prefix(prefix);
											}
											else
											{
												mouseItem.netDefaults(shop[npcShop].item[num154].netID);
												mouseItem.Prefix(-1);
											}
											mouseItem.stack = shop[npcShop].item[num154].stack;
											mouseItem.position.X = player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(mouseItem.width / 2);
											mouseItem.position.Y = player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(mouseItem.height / 2);
											ItemText.NewText(mouseItem, mouseItem.stack);
											if (shop[npcShop].item[num154].buyOnce)
											{
												shop[npcShop].item[num154].stack = 0;
												shop[npcShop].item[num154].SetDefaults(0);
											}
											PlaySound(18);
										}
									}
									else if (shop[npcShop].item[num154].type == 0)
									{
										if (player[myPlayer].SellItem(mouseItem.value, mouseItem.stack))
										{
											shop[npcShop].AddShop(mouseItem);
											mouseItem.stack = 0;
											mouseItem.type = 0;
											PlaySound(18);
										}
										else if (mouseItem.value == 0)
										{
											shop[npcShop].AddShop(mouseItem);
											mouseItem.stack = 0;
											mouseItem.type = 0;
											PlaySound(7);
										}
									}
								}
								else if (stackSplit <= 1 && mouseRight && (mouseItem.IsTheSameAs(shop[npcShop].item[num154]) || mouseItem.type == 0) && (shop[npcShop].item[num154].stack + mouseItem.stack <= mouseItem.maxStack || mouseItem.type == 0) && player[myPlayer].BuyItem(shop[npcShop].item[num154].value * (shop[npcShop].item[num154].buyOnce ? 1 : shop[npcShop].item[num154].stack)))
								{
									PlaySound(18);
									if (mouseItem.type == 0)
									{
										mouseItem.netDefaults(shop[npcShop].item[num154].netID);
										mouseItem.stack = 0;
									}
									if (shop[npcShop].item[num154].buyOnce)
									{
										mouseItem.stack++;
									}
									else
									{
										mouseItem.stack += shop[npcShop].item[num154].stack;
									}
									if (stackSplit == 0)
									{
										stackSplit = 15;
									}
									else
									{
										stackSplit = stackDelay;
									}
									if (shop[npcShop].item[num154].buyOnce)
									{
										shop[npcShop].item[num154].stack--;
										if (shop[npcShop].item[num154].stack <= 0)
										{
											shop[npcShop].item[num154].SetDefaults(0);
										}
									}
								}
								MouseTextString = shop[npcShop].item[num154].name;
								toolTip = (Item)shop[npcShop].item[num154].ShallowClone();
								toolTip.buy = true;
								if (shop[npcShop].item[num154].stack > 1)
								{
									object mouseTextString11 = MouseTextString;
									MouseTextString = string.Concat(mouseTextString11, " (", shop[npcShop].item[num154].stack, ")");
								}
							}
							SpriteBatch spriteBatch77 = this.spriteBatch;
							Texture2D texture29 = inventoryBack6Texture;
							Vector2 vector24 = new Vector2(num152, num153);
							Rectangle? sourceRectangle29 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
							Color color62 = color6;
							float rotation77 = 0f;
							spriteBatch77.Draw(texture29, vector24, sourceRectangle29, color62, rotation77, default(Vector2), inventoryScale, SpriteEffects.None, 0f);
							_ = Color.White;
							ItemSlotRender.DrawItemInSlot(spriteBatch77, color62, shop[npcShop].item[num154], vector24);
						}
					}
				}
				if (player[myPlayer].chest != -1)
				{
					if (!TMod.RunMethod(TMod.WorldHooks.PreDrawChestButtons, this.spriteBatch) || TMod.GetContinueMethod())
					{
						inventoryScale = 0.75f;
						if (mouseX > 73 && mouseX < (int)(73f + 280f * inventoryScale) && mouseY > 210 && mouseY < (int)(210f + 224f * inventoryScale))
						{
							player[myPlayer].mouseInterface = true;
						}
						for (int num155 = 0; num155 < 3; num155++)
						{
							int num156 = 286;
							int num157 = 250;
							float num158 = chestLootScale;
							string text70 = Lang.inter[29];
							switch (num155)
							{
							case 1:
								num157 += 26;
								num158 = chestDepositScale;
								text70 = Lang.inter[30];
								break;
							case 2:
								num157 += 52;
								num158 = chestStackScale;
								text70 = Lang.inter[31];
								break;
							}
							Vector2 origin = fontMouseText.MeasureString(text70) / 2f;
							Color color63 = new Color((byte)((float)(int)mouseTextColor * num158), (byte)((float)(int)mouseTextColor * num158), (byte)((float)(int)mouseTextColor * num158), (byte)((float)(int)mouseTextColor * num158));
							num156 += (int)(origin.X * num158);
							this.spriteBatch.DrawString(fontMouseText, text70, new Vector2(num156, num157), color63, 0f, origin, num158, SpriteEffects.None, 0f);
							origin *= num158;
							if ((float)mouseX > (float)num156 - origin.X && (float)mouseX < (float)num156 + origin.X && (float)mouseY > (float)num157 - origin.Y && (float)mouseY < (float)num157 + origin.Y)
							{
								switch (num155)
								{
								case 0:
									if (!chestLootHover)
									{
										PlaySound(12);
									}
									chestLootHover = true;
									break;
								case 1:
									if (!chestDepositHover)
									{
										PlaySound(12);
									}
									chestDepositHover = true;
									break;
								default:
									if (!chestStackHover)
									{
										PlaySound(12);
									}
									chestStackHover = true;
									break;
								}
								player[myPlayer].mouseInterface = true;
								num158 += 0.05f;
								if (mouseLeft && mouseLeftRelease)
								{
									Interface.chestFunc(player[myPlayer].chest, num155);
									Recipe.FindRecipes();
								}
							}
							else
							{
								num158 -= 0.05f;
								switch (num155)
								{
								case 0:
									chestLootHover = false;
									break;
								case 1:
									chestDepositHover = false;
									break;
								default:
									chestStackHover = false;
									break;
								}
							}
							if ((double)num158 < 0.75)
							{
								num158 = 0.75f;
							}
							if (num158 > 1f)
							{
								num158 = 1f;
							}
							switch (num155)
							{
							case 0:
								chestLootScale = num158;
								break;
							case 1:
								chestDepositScale = num158;
								break;
							default:
								chestStackScale = num158;
								break;
							}
						}
					}
				}
				else
				{
					chestLootScale = 0.75f;
					chestDepositScale = 0.75f;
					chestStackScale = 0.75f;
					chestLootHover = false;
					chestDepositHover = false;
					chestStackHover = false;
				}
				if (player[myPlayer].chest != -1 && (!TMod.RunMethod(TMod.WorldHooks.PreDrawChestButtons, this.spriteBatch) || TMod.GetContinueMethod()))
				{
					Interface.drawChest(player[myPlayer].chest, ref MouseTextString);
				}
			}
			else if ((npcChatText == null || npcChatText == "") && (!TMod.RunMethod(TMod.WorldHooks.PreDrawInformationTexts, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				for (int num159 = 0; num159 < 3; num159++)
				{
					string text71 = "";
					if (player[myPlayer].accCompass > 0 && !flag9)
					{
						int num160 = (int)((player[myPlayer].position.X + (float)(player[myPlayer].width / 2)) * 2f / 16f - (float)maxTilesX);
						if (num160 > 0)
						{
							text71 = "Position: " + num160 + " feet east";
							if (num160 == 1)
							{
								text71 = "Position: " + num160 + " foot east";
							}
						}
						else if (num160 < 0)
						{
							num160 *= -1;
							text71 = "Position: " + num160 + " feet west";
							if (num160 == 1)
							{
								text71 = "Position: " + num160 + " foot west";
							}
						}
						else
						{
							text71 = "Position: center";
						}
						flag9 = true;
					}
					else if (player[myPlayer].accDepthMeter > 0 && !flag8)
					{
						int num161 = (int)((double)((player[myPlayer].position.Y + (float)player[myPlayer].height) * 2f / 16f) - worldSurface * 2.0);
						if (num161 > 0)
						{
							text71 = "Depth: " + num161 + " feet below";
							if (num161 == 1)
							{
								text71 = "Depth: " + num161 + " foot below";
							}
						}
						else if (num161 < 0)
						{
							num161 *= -1;
							text71 = "Depth: " + num161 + " feet above";
							if (num161 == 1)
							{
								text71 = "Depth: " + num161 + " foot above";
							}
						}
						else
						{
							text71 = "Depth: Level";
						}
						flag8 = true;
					}
					else if (player[myPlayer].accWatch > 0 && !flag7)
					{
						string text72 = "AM";
						double num162 = time;
						if (!dayTime)
						{
							num162 += 54000.0;
						}
						num162 = num162 / 86400.0 * 24.0;
						double num163 = 7.5;
						num162 = num162 - num163 - 12.0;
						if (num162 < 0.0)
						{
							num162 += 24.0;
						}
						if (num162 >= 12.0)
						{
							text72 = "PM";
						}
						int num164 = (int)num162;
						double num165 = num162 - (double)num164;
						num165 = (int)(num165 * 60.0);
						string text73 = string.Concat(num165);
						if (num165 < 10.0)
						{
							text73 = "0" + text73;
						}
						if (num164 > 12)
						{
							num164 -= 12;
						}
						if (num164 == 0)
						{
							num164 = 12;
						}
						if (player[myPlayer].accWatch == 1)
						{
							text73 = "00";
						}
						else if (player[myPlayer].accWatch == 2)
						{
							text73 = ((!(num165 < 30.0)) ? "30" : "00");
						}
						text71 = Lang.inter[34] + ": " + num164 + ":" + text73 + " " + text72;
						flag7 = true;
					}
					if (!(text71 != ""))
					{
						continue;
					}
					for (int num166 = 0; num166 < 5; num166++)
					{
						int num167 = 0;
						int num168 = 0;
						Color color64 = Color.Black;
						if (num166 == 0)
						{
							num167 = -2;
						}
						if (num166 == 1)
						{
							num167 = 2;
						}
						if (num166 == 2)
						{
							num168 = -2;
						}
						if (num166 == 3)
						{
							num168 = 2;
						}
						if (num166 == 4)
						{
							color64 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						}
						SpriteBatch spriteBatch78 = this.spriteBatch;
						SpriteFont spriteFont49 = fontMouseText;
						string text74 = text71;
						Vector2 position70 = new Vector2(22 + num167, 110 + 22 * num159 + num168 + 48);
						Color color65 = color64;
						float rotation78 = 0f;
						spriteBatch78.DrawString(spriteFont49, text74, position70, color65, rotation78, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			if ((playerInventory || player[myPlayer].ghost) && (!TMod.RunMethod(TMod.WorldHooks.PreDrawEscapeButtons, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				string text75 = Lang.inter[35];
				Vector2 vector25 = fontMouseText.MeasureString("Save & Exit");
				Vector2 vector26 = fontMouseText.MeasureString(Lang.inter[35]);
				if (netMode != 0)
				{
					text75 = Lang.inter[36];
					vector25 = fontMouseText.MeasureString("Disconnect");
					vector26 = fontMouseText.MeasureString(Lang.inter[36]);
				}
				Vector2 vector27 = fontDeathText.MeasureString(text75);
				int num169 = screenWidth - 110;
				int num170 = screenHeight - 20;
				float num171 = vector25.X / vector26.X;
				if (mouseExit)
				{
					if (exitScale < 1f)
					{
						exitScale += 0.02f;
					}
				}
				else if ((double)exitScale > 0.8)
				{
					exitScale -= 0.02f;
				}
				for (int num172 = 0; num172 < 5; num172++)
				{
					int num173 = 0;
					int num174 = 0;
					Color color66 = Color.Black;
					if (num172 == 0)
					{
						num173 = -2;
					}
					if (num172 == 1)
					{
						num173 = 2;
					}
					if (num172 == 2)
					{
						num174 = -2;
					}
					if (num172 == 3)
					{
						num174 = 2;
					}
					if (num172 == 4)
					{
						color66 = Color.White;
					}
					this.spriteBatch.DrawString(fontDeathText, text75, new Vector2(num169 + num173, num170 + num174), color66, 0f, new Vector2(vector27.X / 2f, vector27.Y / 2f), (exitScale - 0.2f) * num171, SpriteEffects.None, 0f);
				}
				if ((float)mouseX > (float)num169 - vector27.X / 2f && (float)mouseX < (float)num169 + vector27.X / 2f && (float)mouseY > (float)num170 - vector27.Y / 2f && (float)mouseY < (float)num170 + vector27.Y / 2f - 10f)
				{
					if (!mouseExit)
					{
						PlaySound(12);
					}
					mouseExit = true;
					player[myPlayer].mouseInterface = true;
					if (mouseLeftRelease && mouseLeft)
					{
						if (netMode == 1)
						{
							foreach (KeyValuePair<string, JsonData> item10 in Config.jsonCurrent.dict)
							{
								Config.SaveModSettings(item10.Key);
							}
						}
						menuMode = 10;
						WorldGen.SaveAndQuit();
					}
				}
				else
				{
					mouseExit = false;
				}
			}
			if (!playerInventory && !player[myPlayer].ghost && (!TMod.RunMethod(TMod.WorldHooks.PreDrawHotbarInventory, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				string text76 = Lang.inter[37];
				if (player[myPlayer].inventory[player[myPlayer].selectedItem].name != "" && player[myPlayer].inventory[player[myPlayer].selectedItem].name != null)
				{
					text76 = player[myPlayer].inventory[player[myPlayer].selectedItem].AffixName();
				}
				Vector2 vector28 = fontMouseText.MeasureString(text76) / 2f;
				SpriteBatch spriteBatch79 = this.spriteBatch;
				SpriteFont spriteFont50 = fontMouseText;
				string text77 = text76;
				Vector2 position71 = new Vector2(236f - vector28.X, 0f);
				Color color67 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
				float rotation79 = 0f;
				spriteBatch79.DrawString(spriteFont50, text77, position71, color67, rotation79, default(Vector2), 1f, SpriteEffects.None, 0f);
				int num175 = 20;
				float num176 = 1f;
				for (int num177 = 0; num177 < 10; num177++)
				{
					if (num177 == player[myPlayer].selectedItem)
					{
						if (hotbarScale[num177] < 1f)
						{
							hotbarScale[num177] += 0.05f;
						}
					}
					else if ((double)hotbarScale[num177] > 0.75)
					{
						hotbarScale[num177] -= 0.05f;
					}
					float num178 = hotbarScale[num177];
					int num179 = (int)(20f + 22f * (1f - num178));
					int a3 = (int)(75f + 150f * num178);
					Color color68 = new Color(255, 255, 255, a3);
					SpriteBatch spriteBatch80 = this.spriteBatch;
					Texture2D texture30 = inventoryBackTexture;
					Vector2 vector29 = new Vector2(num175, num179);
					Rectangle? sourceRectangle30 = new Rectangle(0, 0, inventoryBackTexture.Width, inventoryBackTexture.Height);
					Color color69 = new Color(100, 100, 100, 100);
					float rotation80 = 0f;
					spriteBatch80.Draw(texture30, vector29, sourceRectangle30, color69, rotation80, default(Vector2), num178, SpriteEffects.None, 0f);
					if (!player[myPlayer].hbLocked && mouseX >= num175 && (float)mouseX <= (float)num175 + (float)inventoryBackTexture.Width * hotbarScale[num177] && mouseY >= num179 && (float)mouseY <= (float)num179 + (float)inventoryBackTexture.Height * hotbarScale[num177] && !player[myPlayer].channel)
					{
						player[myPlayer].mouseInterface = true;
						if (mouseLeft && !player[myPlayer].hbLocked)
						{
							player[myPlayer].changeItem = num177;
						}
						player[myPlayer].showItemIcon = false;
						MouseTextString = player[myPlayer].inventory[num177].AffixName();
						if (player[myPlayer].inventory[num177].stack > 1)
						{
							object mouseTextString12 = MouseTextString;
							MouseTextString = string.Concat(mouseTextString12, " (", player[myPlayer].inventory[num177].stack, ")");
						}
						MouseTextRare = player[myPlayer].inventory[num177].rare;
					}
					ItemSlotRender.DrawItemInSlot(spriteBatch80, color68, player[myPlayer].inventory[num177], vector29, num178);
					if (player[myPlayer].inventory[num177].type > 0 && player[myPlayer].inventory[num177].stack > 0)
					{
						num176 = 1f;
						Item item9 = player[myPlayer].inventory[num177];
						if (itemTexture[item9.type].Width > 32 || itemTexture[item9.type].Height > 32)
						{
							num176 = ((itemTexture[player[myPlayer].inventory[num177].type].Width <= itemTexture[player[myPlayer].inventory[num177].type].Height) ? (32f / (float)itemTexture[player[myPlayer].inventory[num177].type].Height) : (32f / (float)itemTexture[player[myPlayer].inventory[num177].type].Width));
						}
						num176 *= num178;
						if (player[myPlayer].inventory[num177].useAmmo > 0)
						{
							int useAmmo = player[myPlayer].inventory[num177].useAmmo;
							int num180 = 0;
							for (int num181 = 0; num181 < 48; num181++)
							{
								if (player[myPlayer].inventory[num181].ammo == useAmmo)
								{
									num180 += player[myPlayer].inventory[num181].stack;
								}
							}
							SpriteBatch spriteBatch81 = this.spriteBatch;
							SpriteFont spriteFont51 = fontItemStack;
							string text78 = string.Concat(num180);
							Vector2 position72 = new Vector2((float)num175 + 8f * num178, (float)num179 + 30f * num178);
							Color color70 = color68;
							float rotation81 = 0f;
							spriteBatch81.DrawString(spriteFont51, text78, position72, color70, rotation81, default(Vector2), num178 * 0.8f, SpriteEffects.None, 0f);
						}
						else if (player[myPlayer].inventory[num177].type == 509)
						{
							int num182 = 0;
							for (int num183 = 0; num183 < 48; num183++)
							{
								if (player[myPlayer].inventory[num183].type == 530)
								{
									num182 += player[myPlayer].inventory[num183].stack;
								}
							}
							SpriteBatch spriteBatch82 = this.spriteBatch;
							SpriteFont spriteFont52 = fontItemStack;
							string text79 = string.Concat(num182);
							Vector2 position73 = new Vector2((float)num175 + 8f * num178, (float)num179 + 30f * num178);
							Color color71 = color68;
							float rotation82 = 0f;
							spriteBatch82.DrawString(spriteFont52, text79, position73, color71, rotation82, default(Vector2), num178 * 0.8f, SpriteEffects.None, 0f);
						}
						string text80 = string.Concat(num177 + 1);
						if (text80 == "10")
						{
							text80 = "0";
						}
						SpriteBatch spriteBatch83 = this.spriteBatch;
						SpriteFont spriteFont53 = fontItemStack;
						string text81 = text80;
						Vector2 position74 = new Vector2((float)num175 + 8f * hotbarScale[num177], (float)num179 + 4f * hotbarScale[num177]);
						Color color72 = new Color((int)color68.R / 2, (int)color68.G / 2, (int)color68.B / 2, (int)color68.A / 2);
						float rotation83 = 0f;
						spriteBatch83.DrawString(spriteFont53, text81, position74, color72, rotation83, default(Vector2), num176, SpriteEffects.None, 0f);
						if (player[myPlayer].inventory[num177].potion)
						{
							Color alpha = player[myPlayer].inventory[num177].GetAlpha(color68);
							float num184 = (float)player[myPlayer].potionDelay / (float)player[myPlayer].potionDelayTime;
							float num185 = (float)(int)alpha.R * num184;
							float num186 = (float)(int)alpha.G * num184;
							float num187 = (float)(int)alpha.B * num184;
							float num188 = (float)(int)alpha.A * num184;
							alpha = new Color((byte)num185, (byte)num186, (byte)num187, (byte)num188);
							SpriteBatch spriteBatch84 = this.spriteBatch;
							Texture2D texture31 = cdTexture;
							Vector2 position75 = new Vector2((float)num175 + 26f * hotbarScale[num177] - (float)cdTexture.Width * 0.5f * num176, (float)num179 + 26f * hotbarScale[num177] - (float)cdTexture.Height * 0.5f * num176);
							Rectangle? sourceRectangle31 = new Rectangle(0, 0, cdTexture.Width, cdTexture.Height);
							Color color73 = alpha;
							float rotation84 = 0f;
							spriteBatch84.Draw(texture31, position75, sourceRectangle31, color73, rotation84, default(Vector2), num176, SpriteEffects.None, 0f);
						}
					}
					num175 += (int)((float)inventoryBackTexture.Width * hotbarScale[num177]) + 4;
				}
			}
			if (mouseItem.stack <= 0)
			{
				mouseItem.type = 0;
			}
			if (MouseTextString != null && MouseTextString != "" && mouseItem.type == 0)
			{
				player[myPlayer].showItemIcon = false;
				MouseText(MouseTextString, MouseTextRare, 0);
				flag = true;
			}
			if (chatMode)
			{
				textBlinkerCount++;
				if (textBlinkerCount >= 20)
				{
					if (textBlinkerState == 0)
					{
						textBlinkerState = 1;
					}
					else
					{
						textBlinkerState = 0;
					}
					textBlinkerCount = 0;
				}
				string text82 = chatText;
				if (textBlinkerState == 1)
				{
					text82 += "|";
				}
				SpriteBatch spriteBatch85 = this.spriteBatch;
				Texture2D texture32 = textBackTexture;
				Vector2 position76 = new Vector2(78f, screenHeight - 36);
				Rectangle? sourceRectangle32 = new Rectangle(0, 0, textBackTexture.Width, textBackTexture.Height);
				Color color74 = new Color(100, 100, 100, 100);
				float rotation85 = 0f;
				spriteBatch85.Draw(texture32, position76, sourceRectangle32, color74, rotation85, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int num189 = 0; num189 < 5; num189++)
				{
					int num190 = 0;
					int num191 = 0;
					Color color75 = Color.Black;
					if (num189 == 0)
					{
						num190 = -2;
					}
					if (num189 == 1)
					{
						num190 = 2;
					}
					if (num189 == 2)
					{
						num191 = -2;
					}
					if (num189 == 3)
					{
						num191 = 2;
					}
					if (num189 == 4)
					{
						color75 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					}
					SpriteBatch spriteBatch86 = this.spriteBatch;
					SpriteFont spriteFont54 = fontMouseText;
					string text83 = text82;
					Vector2 position77 = new Vector2(88 + num190, screenHeight - 30 + num191);
					Color color76 = color75;
					float rotation86 = 0f;
					spriteBatch86.DrawString(spriteFont54, text83, position77, color76, rotation86, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			for (int num192 = 0; num192 < numChatLines; num192++)
			{
				if (!chatMode && chatLine[num192].showTime <= 0)
				{
					continue;
				}
				float num193 = (float)(int)mouseTextColor / 255f;
				for (int num194 = 0; num194 < 5; num194++)
				{
					int num195 = 0;
					int num196 = 0;
					Color color77 = Color.Black;
					if (num194 == 0)
					{
						num195 = -2;
					}
					if (num194 == 1)
					{
						num195 = 2;
					}
					if (num194 == 2)
					{
						num196 = -2;
					}
					if (num194 == 3)
					{
						num196 = 2;
					}
					if (num194 == 4)
					{
						color77 = new Color((byte)((float)(int)chatLine[num192].color.R * num193), (byte)((float)(int)chatLine[num192].color.G * num193), (byte)((float)(int)chatLine[num192].color.B * num193), mouseTextColor);
					}
					SpriteBatch spriteBatch87 = this.spriteBatch;
					SpriteFont spriteFont55 = fontMouseText;
					string text84 = chatLine[num192].text;
					Vector2 position78 = new Vector2(88 + num195, screenHeight - 30 + num196 - 28 - num192 * 21);
					Color color78 = color77;
					float rotation87 = 0f;
					spriteBatch87.DrawString(spriteFont55, text84, position78, color78, rotation87, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			if (player[myPlayer].dead && (!TMod.RunMethod(TMod.WorldHooks.PreDrawDeathText, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				Vector2 position79 = new Vector2(screenWidth / 2 - Lang.inter[38].Length * 10, screenHeight / 2 - 20);
				Color deathAlpha = player[myPlayer].GetDeathAlpha(Color.Transparent);
				this.spriteBatch.DrawString(fontDeathText, Lang.inter[38], position79, deathAlpha);
			}
			Color color79 = new Color((int)((float)(int)cursorColor.R * 0.2f), (int)((float)(int)cursorColor.G * 0.2f), (int)((float)(int)cursorColor.B * 0.2f), (int)((float)(int)cursorColor.A * 0.5f));
			this.spriteBatch.Draw(cursorTexture, new Vector2(mouseX + 1, mouseY + 1), color79);
			this.spriteBatch.Draw(cursorTexture, new Vector2(mouseX, mouseY), cursorColor);
			if (mouseItem.type > 0 && mouseItem.stack > 0)
			{
				mouseNPC = -1;
				player[myPlayer].showItemIcon = false;
				player[myPlayer].showItemIcon2 = 0;
				flag = true;
				ItemSlotRender.DrawItemInSlot(this.spriteBatch, mouseItem, new Vector2(mouseX, mouseY), cursorScale);
			}
			else if (mouseNPC > -1)
			{
				player[myPlayer].mouseInterface = true;
				flag = false;
				float num197 = 1f;
				num197 *= cursorScale;
				SpriteBatch spriteBatch88 = this.spriteBatch;
				Texture2D texture33 = npcHeadTexture[mouseNPC];
				Vector2 position80 = new Vector2((float)mouseX + 26f * num197 - (float)npcHeadTexture[mouseNPC].Width * 0.5f * num197, (float)mouseY + 26f * num197 - (float)npcHeadTexture[mouseNPC].Height * 0.5f * num197);
				Rectangle? sourceRectangle33 = new Rectangle(0, 0, npcHeadTexture[mouseNPC].Width, npcHeadTexture[mouseNPC].Height);
				Color white21 = Color.White;
				float rotation88 = 0f;
				spriteBatch88.Draw(texture33, position80, sourceRectangle33, white21, rotation88, default(Vector2), num197, SpriteEffects.None, 0f);
				if (mouseRight && mouseRightRelease)
				{
					PlaySound(12);
					mouseNPC = -1;
				}
				if (mouseLeft && mouseLeftRelease)
				{
					if (mouseNPC == 0)
					{
						int x = (int)(((float)mouseX + screenPosition.X) / 16f);
						int y = (int)(((float)mouseY + screenPosition.Y) / 16f);
						int n2 = -1;
						if (WorldGen.MoveNPC(x, y, n2))
						{
							NewText(Lang.inter[39], byte.MaxValue, 240, 20);
						}
					}
					else
					{
						int num198 = 0;
						for (int num199 = 0; num199 < 200; num199++)
						{
							if (npc[num199].active && npc[num199].type == NPC.NumToType(mouseNPC))
							{
								num198 = num199;
								break;
							}
						}
						if (num198 >= 0)
						{
							int x2 = (int)(((float)mouseX + screenPosition.X) / 16f);
							int y2 = (int)(((float)mouseY + screenPosition.Y) / 16f);
							if (WorldGen.MoveNPC(x2, y2, num198))
							{
								mouseNPC = -1;
								WorldGen.moveRoom(x2, y2, num198);
								PlaySound(12);
							}
						}
						else
						{
							mouseNPC = 0;
						}
					}
				}
			}
			Rectangle rectangle2 = new Rectangle((int)((float)mouseX + screenPosition.X), (int)((float)mouseY + screenPosition.Y), 1, 1);
			if (!flag && (!TMod.RunMethod(TMod.WorldHooks.PreDrawLifeText, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				int num200 = 26 * player[myPlayer].statLifeMax2 / num43;
				int num201 = 0;
				if (player[myPlayer].statLifeMax2 > 200)
				{
					num200 = 260;
					num201 += 26;
				}
				if (mouseX > 500 + num41 && mouseX < 500 + num200 + num41 && mouseY > 32 && mouseY < 32 + heartTexture.Height + num201)
				{
					player[myPlayer].showItemIcon = false;
					string cursorText = player[myPlayer].statLife + "/" + player[myPlayer].statLifeMax2;
					MouseText(cursorText, 0, 0);
					flag = true;
				}
			}
			if (!flag && (!TMod.RunMethod(TMod.WorldHooks.PreDrawManaText, this.spriteBatch) || TMod.GetContinueMethod()))
			{
				int num202 = 24;
				int num203 = 28 * player[myPlayer].statManaMax2 / num50;
				if (mouseX > 762 + num41 && mouseX < 762 + num202 + num41 && mouseY > 30 && mouseY < 30 + num203)
				{
					player[myPlayer].showItemIcon = false;
					string cursorText2 = player[myPlayer].statMana + "/" + player[myPlayer].statManaMax2;
					MouseText(cursorText2, 0, 0);
					flag = true;
				}
			}
			if (!flag)
			{
				for (int num204 = 0; num204 < 200; num204++)
				{
					if (!Main.item[num204].active)
					{
						continue;
					}
					Rectangle value3 = new Rectangle((int)(Main.item[num204].position.X + (float)(Main.item[num204].width - itemTexture[Main.item[num204].type].Width) * 0.5f), (int)(Main.item[num204].position.Y + (float)Main.item[num204].height - (float)itemTexture[Main.item[num204].type].Height), itemTexture[Main.item[num204].type].Width, itemTexture[Main.item[num204].type].Height);
					if (rectangle2.Intersects(value3))
					{
						player[myPlayer].showItemIcon = false;
						string text85 = Main.item[num204].AffixName();
						if (Main.item[num204].stack > 1)
						{
							object obj7 = text85;
							text85 = string.Concat(obj7, " (", Main.item[num204].stack, ")");
						}
						if (Main.item[num204].owner < 255 && showItemOwner)
						{
							text85 = text85 + " <" + player[Main.item[num204].owner].name + ">";
						}
						MouseTextRare = Main.item[num204].rare;
						MouseText(text85, MouseTextRare, 0);
						flag = true;
						break;
					}
				}
			}
			for (int num205 = 0; num205 < 255; num205++)
			{
				if (!player[num205].active || myPlayer == num205 || player[num205].dead)
				{
					continue;
				}
				Rectangle value4 = new Rectangle((int)((double)player[num205].position.X + (double)player[num205].width * 0.5 - 16.0), (int)(player[num205].position.Y + (float)player[num205].height - 48f), 32, 48);
				if (!flag && rectangle2.Intersects(value4))
				{
					player[myPlayer].showItemIcon = false;
					int num206 = player[num205].statLife;
					if (num206 < 0)
					{
						num206 = 0;
					}
					string text86 = player[num205].name + ": " + num206 + "/" + player[num205].statLifeMax2;
					if (player[num205].hostile)
					{
						text86 += " (PvP)";
					}
					MouseText(text86, 0, player[num205].difficulty);
				}
			}
			if (!flag)
			{
				for (int num207 = 0; num207 < 200; num207++)
				{
					if (!npc[num207].active)
					{
						continue;
					}
					Rectangle rectangle3 = new Rectangle((int)((double)npc[num207].position.X + (double)npc[num207].width * 0.5 - (double)npcTexture[npc[num207].type].Width * 0.5), (int)(npc[num207].position.Y + (float)npc[num207].height - (float)(npcTexture[npc[num207].type].Height / npcFrameCount[npc[num207].type])), npcTexture[npc[num207].type].Width, npcTexture[npc[num207].type].Height / npcFrameCount[npc[num207].type]);
					float scale2 = npc[num207].scale;
					float num208 = ((float)rectangle3.Width * scale2 - (float)rectangle3.Width) / 2f;
					float num209 = ((float)rectangle3.Height * scale2 - (float)rectangle3.Height) / 2f;
					int x3 = rectangle3.X - (int)num208;
					int y3 = rectangle3.Y - (int)(num209 * 2f);
					int width2 = rectangle3.Width + (int)(num209 * 2f);
					int height2 = rectangle3.Height + (int)(num209 * 2f);
					rectangle3 = new Rectangle(x3, y3, width2, height2);
					if (npc[num207].type >= 87 && npc[num207].type <= 92)
					{
						rectangle3 = new Rectangle((int)((double)npc[num207].position.X + (double)npc[num207].width * 0.5 - 32.0), (int)((double)npc[num207].position.Y + (double)npc[num207].height * 0.5 - 32.0), 64, 64);
					}
					if (!rectangle2.Intersects(rectangle3) || (npc[num207].type == 85 && npc[num207].ai[0] == 0f))
					{
						continue;
					}
					bool flag10 = false;
					if (npc[num207].townNPC || npc[num207].type == 105 || npc[num207].type == 106 || npc[num207].type == 123)
					{
						Rectangle rectangle4 = new Rectangle((int)(player[myPlayer].position.X + (float)(player[myPlayer].width / 2) - (float)(Player.tileRangeX * 16)), (int)(player[myPlayer].position.Y + (float)(player[myPlayer].height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
						Rectangle value5 = new Rectangle((int)npc[num207].position.X, (int)npc[num207].position.Y, npc[num207].width, npc[num207].height);
						if (rectangle4.Intersects(value5))
						{
							flag10 = true;
						}
					}
					if (flag10 && !player[myPlayer].dead)
					{
						int num210 = -(npc[num207].width / 2 + 8);
						SpriteEffects effects2 = SpriteEffects.None;
						if (npc[num207].spriteDirection == -1)
						{
							effects2 = SpriteEffects.FlipHorizontally;
							num210 = npc[num207].width / 2 + 8;
						}
						SpriteBatch spriteBatch89 = this.spriteBatch;
						Texture2D texture34 = chatTexture;
						Vector2 position81 = new Vector2(npc[num207].position.X + (float)(npc[num207].width / 2) - screenPosition.X - (float)(chatTexture.Width / 2) - (float)num210, npc[num207].position.Y - (float)chatTexture.Height - screenPosition.Y);
						Rectangle? sourceRectangle34 = new Rectangle(0, 0, chatTexture.Width, chatTexture.Height);
						Color color80 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						float rotation89 = 0f;
						spriteBatch89.Draw(texture34, position81, sourceRectangle34, color80, rotation89, default(Vector2), 1f, effects2, 0f);
						if (mouseRight && npcChatRelease)
						{
							npcChatRelease = false;
							if (player[myPlayer].talkNPC != num207)
							{
								npcShop = 0;
								craftGuide = false;
								player[myPlayer].dropItemCheck();
								Recipe.FindRecipes();
								player[myPlayer].sign = -1;
								editSign = false;
								player[myPlayer].talkNPC = num207;
								playerInventory = false;
								player[myPlayer].chest = -1;
								npcChatText = npc[num207].GetChat();
								PlaySound(24);
							}
						}
					}
					if (!npc[num207].dontDrawLifeText)
					{
						player[myPlayer].showItemIcon = false;
						string text87 = npc[num207].displayName;
						int num211 = num207;
						if (npc[num207].realLife >= 0)
						{
							num211 = npc[num207].realLife;
						}
						if (npc[num211].lifeMax > 1 && !npc[num211].dontTakeDamage)
						{
							object obj8 = text87;
							text87 = string.Concat(obj8, ": ", npc[num211].life, "/", npc[num211].lifeMax);
						}
						MouseText(text87, 0, 0);
						break;
					}
				}
			}
			if (mouseRight)
			{
				npcChatRelease = false;
			}
			else
			{
				npcChatRelease = true;
			}
			if (player[myPlayer].showItemIcon && (player[myPlayer].inventory[player[myPlayer].selectedItem].type > 0 || player[myPlayer].showItemIcon2 > 0))
			{
				int i4 = player[myPlayer].inventory[player[myPlayer].selectedItem].type;
				Color color81 = player[myPlayer].inventory[player[myPlayer].selectedItem].GetAlpha(Color.White);
				Color color82 = player[myPlayer].inventory[player[myPlayer].selectedItem].GetColor(Color.White);
				if (player[myPlayer].showItemIcon2 > 0)
				{
					i4 = player[myPlayer].showItemIcon2;
					color81 = Color.White;
					color82 = default(Color);
				}
				float scale3 = cursorScale;
				SpriteBatch spriteBatch90 = this.spriteBatch;
				Texture2D texture35 = itemTexture[i4];
				Vector2 position82 = new Vector2(mouseX + 10, mouseY + 10);
				Rectangle? sourceRectangle35 = new Rectangle(0, 0, itemTexture[i4].Width, itemTexture[i4].Height);
				Color color83 = color81;
				float rotation90 = 0f;
				spriteBatch90.Draw(texture35, position82, sourceRectangle35, color83, rotation90, default(Vector2), scale3, SpriteEffects.None, 0f);
				if (player[myPlayer].showItemIcon2 == 0)
				{
					Color color84 = player[myPlayer].inventory[player[myPlayer].selectedItem].color;
					if (color84 != default(Color))
					{
						SpriteBatch spriteBatch91 = this.spriteBatch;
						Texture2D texture36 = itemTexture[player[myPlayer].inventory[player[myPlayer].selectedItem].type];
						Vector2 position83 = new Vector2(mouseX + 10, mouseY + 10);
						Rectangle? sourceRectangle36 = new Rectangle(0, 0, itemTexture[player[myPlayer].inventory[player[myPlayer].selectedItem].type].Width, itemTexture[player[myPlayer].inventory[player[myPlayer].selectedItem].type].Height);
						Color color85 = color82;
						float rotation91 = 0f;
						spriteBatch91.Draw(texture36, position83, sourceRectangle36, color85, rotation91, default(Vector2), scale3, SpriteEffects.None, 0f);
					}
				}
			}
			player[myPlayer].showItemIcon = false;
			player[myPlayer].showItemIcon2 = 0;
		}

		public void QuitGame()
		{
			Music.DisposeAll();
			Audio.Player.Shutdown();
			Exit();
		}

		public Color randColor()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num + num3 + num2 <= 150)
			{
				num = rand.Next(256);
				num2 = rand.Next(256);
				num3 = rand.Next(256);
			}
			return new Color(num, num2, num3, 255);
		}

		public void DrawMenu()
		{
			Menu.DrawMenu(this);
		}

		public static void CursorColor()
		{
			cursorAlpha += (float)cursorColorDirection * 0.015f;
			if (cursorAlpha >= 1f)
			{
				cursorAlpha = 1f;
				cursorColorDirection = -1;
			}
			if ((double)cursorAlpha <= 0.6)
			{
				cursorAlpha = 0.6f;
				cursorColorDirection = 1;
			}
			float num = cursorAlpha * 0.3f + 0.7f;
			byte r = (byte)((float)(int)mouseColor.R * cursorAlpha);
			byte g = (byte)((float)(int)mouseColor.G * cursorAlpha);
			byte b = (byte)((float)(int)mouseColor.B * cursorAlpha);
			byte a = (byte)(255f * num);
			cursorColor = new Color(r, g, b, a);
			cursorScale = cursorAlpha * 0.3f + 0.7f + 0.1f;
		}

		public void DrawSplash(GameTime gameTime)
		{
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			spriteBatch.Begin();
			splashCounter++;
			Color white = Color.White;
			byte b = 0;
			if (splashCounter <= 75)
			{
				float num = (float)splashCounter / 75f * 255f;
				b = (byte)num;
			}
			else if (splashCounter <= 125)
			{
				b = byte.MaxValue;
			}
			else if (splashCounter <= 200)
			{
				int num2 = 125 - splashCounter;
				float num3 = (float)num2 / 75f * 255f;
				b = (byte)num3;
			}
			else
			{
				showSplash = false;
				fadeCounter = 75;
			}
			white = new Color(b, b, b, b);
			spriteBatch.Draw(loTexture, new Rectangle(0, 0, screenWidth, screenHeight), white);
			spriteBatch.End();
		}

		public void DrawBackground()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - gfxQuality) + 140f * gfxQuality);
			int num2 = (int)(200f * (1f - gfxQuality) + 40f * gfxQuality);
			int num3 = 96;
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			float num4 = 0.9f;
			float num5 = num4;
			float num6 = num4;
			float num7 = num4;
			float num8 = 0f;
			if (holyTiles > evilTiles)
			{
				num8 = (float)holyTiles / 800f;
			}
			else if (evilTiles > holyTiles)
			{
				num8 = (float)evilTiles / 800f;
			}
			if (num8 > 1f)
			{
				num8 = 1f;
			}
			if (num8 < 0f)
			{
				num8 = 0f;
			}
			float num9 = (float)((double)screenPosition.Y - worldSurface * 16.0) / 300f;
			if (num9 < 0f)
			{
				num9 = 0f;
			}
			else if (num9 > 1f)
			{
				num9 = 1f;
			}
			float num10 = 1f * (1f - num9) + num5 * num9;
			Lighting.brightness = Lighting.defBrightness * (1f - num9) + 1f * num9;
			float num11 = (float)((double)(screenPosition.Y - (float)(screenHeight / 2) + 200f) - rockLayer * 16.0) / 300f;
			if (num11 < 0f)
			{
				num11 = 0f;
			}
			else if (num11 > 1f)
			{
				num11 = 1f;
			}
			if (evilTiles > 0)
			{
				num5 = 0.8f * num8 + num5 * (1f - num8);
				num6 = 0.75f * num8 + num6 * (1f - num8);
				num7 = 1.1f * num8 + num7 * (1f - num8);
			}
			else if (holyTiles > 0)
			{
				num5 = 1f * num8 + num5 * (1f - num8);
				num6 = 0.7f * num8 + num6 * (1f - num8);
				num7 = 0.9f * num8 + num7 * (1f - num8);
			}
			num5 = 1f * (num10 - num11) + num5 * num11;
			num6 = 1f * (num10 - num11) + num6 * num11;
			num7 = 1f * (num10 - num11) + num7 * num11;
			Lighting.defBrightness = 1.2f * (1f - num11) + 1f * num11;
			bgParrallax = caveParrallax;
			bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
			bgLoops = screenWidth / num3 + 2;
			bgTop = (int)((float)((int)worldSurface * 16 - backgroundHeight[1]) - screenPosition.Y + 16f);
			for (int i = 0; i < bgLoops; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					float num12 = (float)bgStart + screenPosition.X;
					num12 = 0f - (float)Math.IEEERemainder(num12, 16.0);
					num12 = (float)Math.Round(num12);
					int num13 = (int)num12;
					if (num13 == -8)
					{
						num13 = 8;
					}
					float num14 = bgStart + num3 * i + j * 16 + 8;
					float num15 = bgTop;
					Color color = Lighting.GetColor((int)((num14 + screenPosition.X) / 16f), (int)((screenPosition.Y + num15) / 16f));
					color.R = (byte)((float)(int)color.R * num5);
					color.G = (byte)((float)(int)color.G * num6);
					color.B = (byte)((float)(int)color.B * num7);
					this.spriteBatch.Draw(backgroundTexture[1], new Vector2(bgStart + num3 * i + 16 * j + num13, bgTop) + value, new Rectangle(16 * j + num13 + 16, 0, 16, 16), color);
				}
			}
			double num16 = hellLayer - 30.0;
			double num17 = (int)((num16 - worldSurface) / 6.0) * 6;
			num16 = worldSurface + num17 - 5.0;
			bool flag = false;
			bool flag2 = false;
			bgTop = (int)((float)((int)worldSurface * 16) - screenPosition.Y + 16f);
			if (worldSurface * 16.0 <= (double)(screenPosition.Y + (float)screenHeight + (float)offScreenRange))
			{
				bgParrallax = caveParrallax;
				bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)value.X;
				bgLoops = (screenWidth + (int)value.X * 2) / num3 + 2;
				if (worldSurface * 16.0 < (double)(screenPosition.Y - 16f))
				{
					bgStartY = (int)(Math.IEEERemainder(bgTop, backgroundHeight[2]) - (double)backgroundHeight[2]);
					bgLoopsY = (screenHeight - bgStartY + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				else
				{
					bgStartY = bgTop;
					bgLoopsY = (screenHeight - bgTop + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				if (rockLayer * 16.0 < (double)(screenPosition.Y + 600f))
				{
					bgLoopsY = (int)(rockLayer * 16.0 - (double)screenPosition.Y + 600.0 - (double)bgStartY) / backgroundHeight[2];
					flag2 = true;
				}
				float num18 = (float)bgStart + screenPosition.X;
				num18 = 0f - (float)Math.IEEERemainder(num18, 16.0);
				num18 = (float)Math.Round(num18);
				int num19 = (int)num18;
				if (num19 == -8)
				{
					num19 = 8;
				}
				for (int k = 0; k < bgLoops; k++)
				{
					for (int l = 0; l < bgLoopsY; l++)
					{
						for (int m = 0; m < 6; m++)
						{
							for (int n = 0; n < 6; n++)
							{
								float num20 = bgStartY + l * 96 + n * 16 + 8;
								float num21 = bgStart + num3 * k + m * 16 + 8;
								int num22 = (int)((num21 + screenPosition.X) / 16f);
								int num23 = (int)((num20 + screenPosition.Y) / 16f);
								Color color2 = Lighting.GetColor(num22, num23);
								if (tile[num22, num23] == null)
								{
									tile[num22, num23] = new Tile();
								}
								if (color2.R > 0 || color2.G > 0 || color2.B > 0)
								{
									if ((color2.R > num || (double)(int)color2.G > (double)num * 1.1 || (double)(int)color2.B > (double)num * 1.2) && !tile[num22, num23].active && (tile[num22, num23].wall == 0 || tile[num22, num23].wall == 21))
									{
										try
										{
											for (int num24 = 0; num24 < 9; num24++)
											{
												int num25 = 0;
												int num26 = 0;
												int width = 4;
												int height = 4;
												Color color3 = color2;
												Color color4 = color2;
												if (num24 == 0 && !tile[num22 - 1, num23 - 1].active)
												{
													color4 = Lighting.GetColor(num22 - 1, num23 - 1);
												}
												if (num24 == 1)
												{
													width = 8;
													num25 = 4;
													if (!tile[num22, num23 - 1].active)
													{
														color4 = Lighting.GetColor(num22, num23 - 1);
													}
												}
												if (num24 == 2)
												{
													if (!tile[num22 + 1, num23 - 1].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23 - 1);
													}
													if (tile[num22 + 1, num23 - 1] == null)
													{
														tile[num22 + 1, num23 - 1] = new Tile();
													}
													num25 = 12;
												}
												if (num24 == 3)
												{
													if (!tile[num22 - 1, num23].active)
													{
														color4 = Lighting.GetColor(num22 - 1, num23);
													}
													height = 8;
													num26 = 4;
												}
												if (num24 == 4)
												{
													width = 8;
													height = 8;
													num25 = 4;
													num26 = 4;
												}
												if (num24 == 5)
												{
													num25 = 12;
													num26 = 4;
													height = 8;
													if (!tile[num22 + 1, num23].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23);
													}
												}
												if (num24 == 6)
												{
													if (!tile[num22 - 1, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22 - 1, num23 + 1);
													}
													num26 = 12;
												}
												if (num24 == 7)
												{
													width = 8;
													height = 4;
													num25 = 4;
													num26 = 12;
													if (!tile[num22, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22, num23 + 1);
													}
												}
												if (num24 == 8)
												{
													if (!tile[num22 + 1, num23 + 1].active)
													{
														color4 = Lighting.GetColor(num22 + 1, num23 + 1);
													}
													num25 = 12;
													num26 = 12;
												}
												color3.R = (byte)((color2.R + color4.R) / 2);
												color3.G = (byte)((color2.G + color4.G) / 2);
												color3.B = (byte)((color2.B + color4.B) / 2);
												color3.R = (byte)((float)(int)color3.R * num5);
												color3.G = (byte)((float)(int)color3.G * num6);
												color3.B = (byte)((float)(int)color3.B * num7);
												this.spriteBatch.Draw(backgroundTexture[2], new Vector2(bgStart + num3 * k + 16 * m + num25 + num19, bgStartY + backgroundHeight[2] * l + 16 * n + num26) + value, new Rectangle(16 * m + num25 + num19 + 16, 16 * n + num26, width, height), color3);
											}
										}
										catch
										{
											color2.R = (byte)((float)(int)color2.R * num5);
											color2.G = (byte)((float)(int)color2.G * num6);
											color2.B = (byte)((float)(int)color2.B * num7);
											this.spriteBatch.Draw(backgroundTexture[2], new Vector2(bgStart + num3 * k + 16 * m + num19, bgStartY + backgroundHeight[2] * l + 16 * n) + value, new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16), color2);
										}
									}
									else if (color2.R > num2 || (double)(int)color2.G > (double)num2 * 1.1 || (double)(int)color2.B > (double)num2 * 1.2)
									{
										for (int num27 = 0; num27 < 4; num27++)
										{
											int num28 = 0;
											int num29 = 0;
											Color color5 = color2;
											Color color6 = color2;
											if (num27 == 0)
											{
												color6 = ((!Lighting.Brighter(num22, num23 - 1, num22 - 1, num23)) ? Lighting.GetColor(num22, num23 - 1) : Lighting.GetColor(num22 - 1, num23));
											}
											if (num27 == 1)
											{
												color6 = ((!Lighting.Brighter(num22, num23 - 1, num22 + 1, num23)) ? Lighting.GetColor(num22, num23 - 1) : Lighting.GetColor(num22 + 1, num23));
												num28 = 8;
											}
											if (num27 == 2)
											{
												color6 = ((!Lighting.Brighter(num22, num23 + 1, num22 - 1, num23)) ? Lighting.GetColor(num22, num23 + 1) : Lighting.GetColor(num22 - 1, num23));
												num29 = 8;
											}
											if (num27 == 3)
											{
												color6 = ((!Lighting.Brighter(num22, num23 + 1, num22 + 1, num23)) ? Lighting.GetColor(num22, num23 + 1) : Lighting.GetColor(num22 + 1, num23));
												num28 = 8;
												num29 = 8;
											}
											color5.R = (byte)((color2.R + color6.R) / 2);
											color5.G = (byte)((color2.G + color6.G) / 2);
											color5.B = (byte)((color2.B + color6.B) / 2);
											color5.R = (byte)((float)(int)color5.R * num5);
											color5.G = (byte)((float)(int)color5.G * num6);
											color5.B = (byte)((float)(int)color5.B * num7);
											this.spriteBatch.Draw(backgroundTexture[2], new Vector2(bgStart + num3 * k + 16 * m + num28 + num19, bgStartY + backgroundHeight[2] * l + 16 * n + num29) + value, new Rectangle(16 * m + num28 + num19 + 16, 16 * n + num29, 8, 8), color5);
										}
									}
									else
									{
										color2.R = (byte)((float)(int)color2.R * num5);
										color2.G = (byte)((float)(int)color2.G * num6);
										color2.B = (byte)((float)(int)color2.B * num7);
										this.spriteBatch.Draw(backgroundTexture[2], new Vector2(bgStart + num3 * k + 16 * m + num19, bgStartY + backgroundHeight[2] * l + 16 * n) + value, new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16), color2);
									}
								}
								else
								{
									color2.R = (byte)((float)(int)color2.R * num5);
									color2.G = (byte)((float)(int)color2.G * num6);
									color2.B = (byte)((float)(int)color2.B * num7);
									this.spriteBatch.Draw(backgroundTexture[2], new Vector2(bgStart + num3 * k + 16 * m + num19, bgStartY + backgroundHeight[2] * l + 16 * n) + value, new Rectangle(16 * m + num19 + 16, 16 * n, 16, 16), color2);
								}
							}
						}
					}
				}
				if (flag2)
				{
					bgParrallax = caveParrallax;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
					bgLoops = (screenWidth + (int)value.X * 2) / num3 + 2;
					bgTop = bgStartY + bgLoopsY * backgroundHeight[2];
					if (bgTop > -32)
					{
						for (int num30 = 0; num30 < bgLoops; num30++)
						{
							for (int num31 = 0; num31 < 6; num31++)
							{
								float num32 = bgStart + num3 * num30 + num31 * 16 + 8;
								float num33 = bgTop;
								Color color7 = Lighting.GetColor((int)((num32 + screenPosition.X) / 16f), (int)((screenPosition.Y + num33) / 16f));
								color7.R = (byte)((float)(int)color7.R * num5);
								color7.G = (byte)((float)(int)color7.G * num6);
								color7.B = (byte)((float)(int)color7.B * num7);
								this.spriteBatch.Draw(backgroundTexture[4], new Vector2(bgStart + num3 * num30 + 16 * num31 + num19, bgTop) + value, new Rectangle(16 * num31 + num19 + 16, 0, 16, 16), color7);
							}
						}
					}
				}
			}
			bgTop = (int)((float)((int)rockLayer * 16) - screenPosition.Y + 16f + 600f - 8f);
			if (rockLayer * 16.0 <= (double)(screenPosition.Y + 600f))
			{
				bgParrallax = caveParrallax;
				bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)value.X;
				bgLoops = (screenWidth + (int)value.X * 2) / num3 + 2;
				if (rockLayer * 16.0 + (double)screenHeight < (double)(screenPosition.Y - 16f))
				{
					bgStartY = (int)(Math.IEEERemainder(bgTop, backgroundHeight[3]) - (double)backgroundHeight[3]);
					bgLoopsY = (screenHeight - bgStartY + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				else
				{
					bgStartY = bgTop;
					bgLoopsY = (screenHeight - bgTop + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				if (num16 * 16.0 < (double)(screenPosition.Y + 600f))
				{
					bgLoopsY = (int)(num16 * 16.0 - (double)screenPosition.Y + 600.0 - (double)bgStartY) / backgroundHeight[2];
					flag = true;
				}
				float num34 = (float)bgStart + screenPosition.X;
				num34 = 0f - (float)Math.IEEERemainder(num34, 16.0);
				num34 = (float)Math.Round(num34);
				int num35 = (int)num34;
				if (num35 == -8)
				{
					num35 = 8;
				}
				for (int num36 = 0; num36 < bgLoops; num36++)
				{
					for (int num37 = 0; num37 < bgLoopsY; num37++)
					{
						for (int num38 = 0; num38 < 6; num38++)
						{
							for (int num39 = 0; num39 < 6; num39++)
							{
								float num40 = bgStartY + num37 * 96 + num39 * 16 + 8;
								float num41 = bgStart + num3 * num36 + num38 * 16 + 8;
								int num42 = (int)((num41 + screenPosition.X) / 16f);
								int num43 = (int)((num40 + screenPosition.Y) / 16f);
								Color color8 = Lighting.GetColor(num42, num43);
								if (tile[num42, num43] == null)
								{
									tile[num42, num43] = new Tile();
								}
								bool flag3 = false;
								if (caveParrallax != 0f)
								{
									if (tile[num42 - 1, num43] == null)
									{
										tile[num42 - 1, num43] = new Tile();
									}
									if (tile[num42 + 1, num43] == null)
									{
										tile[num42 + 1, num43] = new Tile();
									}
									if (tile[num42, num43].wall == 0 || tile[num42, num43].wall == 21 || tile[num42 - 1, num43].wall == 0 || tile[num42 - 1, num43].wall == 21 || tile[num42 + 1, num43].wall == 0 || tile[num42 + 1, num43].wall == 21)
									{
										flag3 = true;
									}
								}
								else if (tile[num42, num43].wall == 0 || tile[num42, num43].wall == 21)
								{
									flag3 = true;
								}
								if ((!flag3 && color8.R != 0 && color8.G != 0 && color8.B != 0) || (color8.R <= 0 && color8.G <= 0 && color8.B <= 0) || (tile[num42, num43].wall != 0 && tile[num42, num43].wall != 21 && caveParrallax == 0f))
								{
									continue;
								}
								if (Lighting.lightMode < 2 && color8.R < 230 && color8.G < 230 && color8.B < 230)
								{
									if ((color8.R > num || (double)(int)color8.G > (double)num * 1.1 || (double)(int)color8.B > (double)num * 1.2) && !tile[num42, num43].active)
									{
										for (int num44 = 0; num44 < 9; num44++)
										{
											int num45 = 0;
											int num46 = 0;
											int width2 = 4;
											int height2 = 4;
											Color color9 = color8;
											Color color10 = color8;
											if (num44 == 0 && !tile[num42 - 1, num43 - 1].active)
											{
												color10 = Lighting.GetColor(num42 - 1, num43 - 1);
											}
											if (num44 == 1)
											{
												width2 = 8;
												num45 = 4;
												if (!tile[num42, num43 - 1].active)
												{
													color10 = Lighting.GetColor(num42, num43 - 1);
												}
											}
											if (num44 == 2)
											{
												if (!tile[num42 + 1, num43 - 1].active)
												{
													color10 = Lighting.GetColor(num42 + 1, num43 - 1);
												}
												num45 = 12;
											}
											if (num44 == 3)
											{
												if (!tile[num42 - 1, num43].active)
												{
													color10 = Lighting.GetColor(num42 - 1, num43);
												}
												height2 = 8;
												num46 = 4;
											}
											if (num44 == 4)
											{
												width2 = 8;
												height2 = 8;
												num45 = 4;
												num46 = 4;
											}
											if (num44 == 5)
											{
												num45 = 12;
												num46 = 4;
												height2 = 8;
												if (!tile[num42 + 1, num43].active)
												{
													color10 = Lighting.GetColor(num42 + 1, num43);
												}
											}
											if (num44 == 6)
											{
												if (!tile[num42 - 1, num43 + 1].active)
												{
													color10 = Lighting.GetColor(num42 - 1, num43 + 1);
												}
												num46 = 12;
											}
											if (num44 == 7)
											{
												width2 = 8;
												height2 = 4;
												num45 = 4;
												num46 = 12;
												if (!tile[num42, num43 + 1].active)
												{
													color10 = Lighting.GetColor(num42, num43 + 1);
												}
											}
											if (num44 == 8)
											{
												if (!tile[num42 + 1, num43 + 1].active)
												{
													color10 = Lighting.GetColor(num42 + 1, num43 + 1);
												}
												num45 = 12;
												num46 = 12;
											}
											color9.R = (byte)((color8.R + color10.R) / 2);
											color9.G = (byte)((color8.G + color10.G) / 2);
											color9.B = (byte)((color8.B + color10.B) / 2);
											color9.R = (byte)((float)(int)color9.R * num5);
											color9.G = (byte)((float)(int)color9.G * num6);
											color9.B = (byte)((float)(int)color9.B * num7);
											this.spriteBatch.Draw(backgroundTexture[3], new Vector2(bgStart + num3 * num36 + 16 * num38 + num45 + num35, bgStartY + backgroundHeight[2] * num37 + 16 * num39 + num46) + value, new Rectangle(16 * num38 + num45 + num35 + 16, 16 * num39 + num46, width2, height2), color9);
										}
									}
									else if (color8.R > num2 || (double)(int)color8.G > (double)num2 * 1.1 || (double)(int)color8.B > (double)num2 * 1.2)
									{
										for (int num47 = 0; num47 < 4; num47++)
										{
											int num48 = 0;
											int num49 = 0;
											Color color11 = color8;
											Color color12 = color8;
											if (num47 == 0)
											{
												color12 = ((!Lighting.Brighter(num42, num43 - 1, num42 - 1, num43)) ? Lighting.GetColor(num42, num43 - 1) : Lighting.GetColor(num42 - 1, num43));
											}
											if (num47 == 1)
											{
												color12 = ((!Lighting.Brighter(num42, num43 - 1, num42 + 1, num43)) ? Lighting.GetColor(num42, num43 - 1) : Lighting.GetColor(num42 + 1, num43));
												num48 = 8;
											}
											if (num47 == 2)
											{
												color12 = ((!Lighting.Brighter(num42, num43 + 1, num42 - 1, num43)) ? Lighting.GetColor(num42, num43 + 1) : Lighting.GetColor(num42 - 1, num43));
												num49 = 8;
											}
											if (num47 == 3)
											{
												color12 = ((!Lighting.Brighter(num42, num43 + 1, num42 + 1, num43)) ? Lighting.GetColor(num42, num43 + 1) : Lighting.GetColor(num42 + 1, num43));
												num48 = 8;
												num49 = 8;
											}
											color11.R = (byte)((color8.R + color12.R) / 2);
											color11.G = (byte)((color8.G + color12.G) / 2);
											color11.B = (byte)((color8.B + color12.B) / 2);
											color11.R = (byte)((float)(int)color11.R * num5);
											color11.G = (byte)((float)(int)color11.G * num6);
											color11.B = (byte)((float)(int)color11.B * num7);
											this.spriteBatch.Draw(backgroundTexture[3], new Vector2(bgStart + num3 * num36 + 16 * num38 + num48 + num35, bgStartY + backgroundHeight[2] * num37 + 16 * num39 + num49) + value, new Rectangle(16 * num38 + num48 + num35 + 16, 16 * num39 + num49, 8, 8), color11);
										}
									}
									else
									{
										color8.R = (byte)((float)(int)color8.R * num5);
										color8.G = (byte)((float)(int)color8.G * num6);
										color8.B = (byte)((float)(int)color8.B * num7);
										this.spriteBatch.Draw(backgroundTexture[3], new Vector2(bgStart + num3 * num36 + 16 * num38 + num35, bgStartY + backgroundHeight[2] * num37 + 16 * num39) + value, new Rectangle(16 * num38 + num35 + 16, 16 * num39, 16, 16), color8);
									}
								}
								else
								{
									color8.R = (byte)((float)(int)color8.R * num5);
									color8.G = (byte)((float)(int)color8.G * num6);
									color8.B = (byte)((float)(int)color8.B * num7);
									this.spriteBatch.Draw(backgroundTexture[3], new Vector2(bgStart + num3 * num36 + 16 * num38 + num35, bgStartY + backgroundHeight[2] * num37 + 16 * num39) + value, new Rectangle(16 * num38 + num35 + 16, 16 * num39, 16, 16), color8);
								}
							}
						}
					}
				}
				if (flag)
				{
					bgParrallax = caveParrallax;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2));
					bgLoops = screenWidth / num3 + 2;
					bgTop = bgStartY + bgLoopsY * backgroundHeight[2];
					for (int num50 = 0; num50 < bgLoops; num50++)
					{
						for (int num51 = 0; num51 < 6; num51++)
						{
							float num52 = bgStart + num3 * num50 + num51 * 16 + 8;
							float num53 = bgTop;
							Color color13 = Lighting.GetColor((int)((num52 + screenPosition.X) / 16f), (int)((screenPosition.Y + num53) / 16f));
							color13.R = (byte)((float)(int)color13.R * num5);
							color13.G = (byte)((float)(int)color13.G * num6);
							color13.B = (byte)((float)(int)color13.B * num7);
							this.spriteBatch.Draw(backgroundTexture[6], new Vector2(bgStart + num3 * num50 + 16 * num51 + num35, bgTop) + value, new Rectangle(16 * num51 + num35 + 16, magmaBGFrame * 16, 16, 16), color13);
						}
					}
				}
			}
			bgTop = (int)((float)((int)num16 * 16) - screenPosition.Y + 16f + 600f) - 8;
			if (num16 * 16.0 <= (double)(screenPosition.Y + 600f))
			{
				bgStart = (int)(0.0 - Math.IEEERemainder(96.0 + (double)screenPosition.X * bgParrallax, num3) - (double)(num3 / 2)) - (int)value.X;
				bgLoops = (screenWidth + (int)value.X * 2) / num3 + 2;
				if (num16 * 16.0 + (double)screenHeight < (double)(screenPosition.Y - 16f))
				{
					bgStartY = (int)(Math.IEEERemainder(bgTop, backgroundHeight[2]) - (double)backgroundHeight[2]);
					bgLoopsY = (screenHeight - bgStartY + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				else
				{
					bgStartY = bgTop;
					bgLoopsY = (screenHeight - bgTop + (int)value.Y * 2) / backgroundHeight[2] + 1;
				}
				num = (int)((double)num * 1.5);
				num2 = (int)((double)num2 * 1.5);
				float num54 = (float)bgStart + screenPosition.X;
				num54 = 0f - (float)Math.IEEERemainder(num54, 16.0);
				num54 = (float)Math.Round(num54);
				int num55 = (int)num54;
				if (num55 == -8)
				{
					num55 = 8;
				}
				for (int num56 = 0; num56 < bgLoops; num56++)
				{
					for (int num57 = 0; num57 < bgLoopsY; num57++)
					{
						for (int num58 = 0; num58 < 6; num58++)
						{
							for (int num59 = 0; num59 < 6; num59++)
							{
								float num60 = bgStartY + num57 * 96 + num59 * 16 + 8;
								float num61 = bgStart + num3 * num56 + num58 * 16 + 8;
								int num62 = (int)((num61 + screenPosition.X) / 16f);
								int num63 = (int)((num60 + screenPosition.Y) / 16f);
								Color color14 = Lighting.GetColor(num62, num63);
								if (tile[num62, num63] == null)
								{
									tile[num62, num63] = new Tile();
								}
								bool flag4 = false;
								if (caveParrallax != 0f)
								{
									if (tile[num62 - 1, num63] == null)
									{
										tile[num62 - 1, num63] = new Tile();
									}
									if (tile[num62 + 1, num63] == null)
									{
										tile[num62 + 1, num63] = new Tile();
									}
									if (tile[num62, num63].wall == 0 || tile[num62, num63].wall == 21 || tile[num62 - 1, num63].wall == 0 || tile[num62 - 1, num63].wall == 21 || tile[num62 + 1, num63].wall == 0 || tile[num62 + 1, num63].wall == 21)
									{
										flag4 = true;
									}
								}
								else if (tile[num62, num63].wall == 0 || tile[num62, num63].wall == 21)
								{
									flag4 = true;
								}
								if ((!flag4 && color14.R != 0 && color14.G != 0 && color14.B != 0) || (color14.R <= 0 && color14.G <= 0 && color14.B <= 0) || (tile[num62, num63].wall != 0 && tile[num62, num63].wall != 21 && caveParrallax == 0f))
								{
									continue;
								}
								if (Lighting.lightMode < 2 && color14.R < 230 && color14.G < 230 && color14.B < 230)
								{
									if ((color14.R > num || (double)(int)color14.G > (double)num * 1.1 || (double)(int)color14.B > (double)num * 1.2) && !tile[num62, num63].active)
									{
										for (int num64 = 0; num64 < 9; num64++)
										{
											int num65 = 0;
											int num66 = 0;
											int width3 = 4;
											int height3 = 4;
											Color color15 = color14;
											Color color16 = color14;
											if (num64 == 0 && !tile[num62 - 1, num63 - 1].active)
											{
												color16 = Lighting.GetColor(num62 - 1, num63 - 1);
											}
											if (num64 == 1)
											{
												width3 = 8;
												num65 = 4;
												if (!tile[num62, num63 - 1].active)
												{
													color16 = Lighting.GetColor(num62, num63 - 1);
												}
											}
											if (num64 == 2)
											{
												if (!tile[num62 + 1, num63 - 1].active)
												{
													color16 = Lighting.GetColor(num62 + 1, num63 - 1);
												}
												num65 = 12;
											}
											if (num64 == 3)
											{
												if (!tile[num62 - 1, num63].active)
												{
													color16 = Lighting.GetColor(num62 - 1, num63);
												}
												height3 = 8;
												num66 = 4;
											}
											if (num64 == 4)
											{
												width3 = 8;
												height3 = 8;
												num65 = 4;
												num66 = 4;
											}
											if (num64 == 5)
											{
												num65 = 12;
												num66 = 4;
												height3 = 8;
												if (!tile[num62 + 1, num63].active)
												{
													color16 = Lighting.GetColor(num62 + 1, num63);
												}
											}
											if (num64 == 6)
											{
												if (!tile[num62 - 1, num63 + 1].active)
												{
													color16 = Lighting.GetColor(num62 - 1, num63 + 1);
												}
												num66 = 12;
											}
											if (num64 == 7)
											{
												width3 = 8;
												height3 = 4;
												num65 = 4;
												num66 = 12;
												if (!tile[num62, num63 + 1].active)
												{
													color16 = Lighting.GetColor(num62, num63 + 1);
												}
											}
											if (num64 == 8)
											{
												if (!tile[num62 + 1, num63 + 1].active)
												{
													color16 = Lighting.GetColor(num62 + 1, num63 + 1);
												}
												num65 = 12;
												num66 = 12;
											}
											color15.R = (byte)((color14.R + color16.R) / 2);
											color15.G = (byte)((color14.G + color16.G) / 2);
											color15.B = (byte)((color14.B + color16.B) / 2);
											color15.R = (byte)((float)(int)color15.R * num5);
											color15.G = (byte)((float)(int)color15.G * num6);
											color15.B = (byte)((float)(int)color15.B * num7);
											SpriteBatch spriteBatch = this.spriteBatch;
											Texture2D texture = backgroundTexture[5];
											Vector2 position = new Vector2(bgStart + num3 * num56 + 16 * num58 + num65 + num55, bgStartY + backgroundHeight[2] * num57 + 16 * num59 + num66) + value;
											Rectangle? sourceRectangle = new Rectangle(16 * num58 + num65 + num55 + 16, 16 * num59 + backgroundHeight[2] * magmaBGFrame + num66, width3, height3);
											Color color17 = color15;
											float rotation = 0f;
											spriteBatch.Draw(texture, position, sourceRectangle, color17, rotation, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
									}
									else if (color14.R > num2 || (double)(int)color14.G > (double)num2 * 1.1 || (double)(int)color14.B > (double)num2 * 1.2)
									{
										for (int num67 = 0; num67 < 4; num67++)
										{
											int num68 = 0;
											int num69 = 0;
											Color color18 = color14;
											Color color19 = color14;
											if (num67 == 0)
											{
												color19 = ((!Lighting.Brighter(num62, num63 - 1, num62 - 1, num63)) ? Lighting.GetColor(num62, num63 - 1) : Lighting.GetColor(num62 - 1, num63));
											}
											if (num67 == 1)
											{
												color19 = ((!Lighting.Brighter(num62, num63 - 1, num62 + 1, num63)) ? Lighting.GetColor(num62, num63 - 1) : Lighting.GetColor(num62 + 1, num63));
												num68 = 8;
											}
											if (num67 == 2)
											{
												color19 = ((!Lighting.Brighter(num62, num63 + 1, num62 - 1, num63)) ? Lighting.GetColor(num62, num63 + 1) : Lighting.GetColor(num62 - 1, num63));
												num69 = 8;
											}
											if (num67 == 3)
											{
												color19 = ((!Lighting.Brighter(num62, num63 + 1, num62 + 1, num63)) ? Lighting.GetColor(num62, num63 + 1) : Lighting.GetColor(num62 + 1, num63));
												num68 = 8;
												num69 = 8;
											}
											color18.R = (byte)((color14.R + color19.R) / 2);
											color18.G = (byte)((color14.G + color19.G) / 2);
											color18.B = (byte)((color14.B + color19.B) / 2);
											color18.R = (byte)((float)(int)color18.R * num5);
											color18.G = (byte)((float)(int)color18.G * num6);
											color18.B = (byte)((float)(int)color18.B * num7);
											SpriteBatch spriteBatch2 = this.spriteBatch;
											Texture2D texture2 = backgroundTexture[5];
											Vector2 position2 = new Vector2(bgStart + num3 * num56 + 16 * num58 + num68 + num55, bgStartY + backgroundHeight[2] * num57 + 16 * num59 + num69) + value;
											Rectangle? sourceRectangle2 = new Rectangle(16 * num58 + num68 + num55 + 16, 16 * num59 + backgroundHeight[2] * magmaBGFrame + num69, 8, 8);
											Color color20 = color18;
											float rotation2 = 0f;
											spriteBatch2.Draw(texture2, position2, sourceRectangle2, color20, rotation2, default(Vector2), 1f, SpriteEffects.None, 0f);
										}
									}
									else
									{
										color14.R = (byte)((float)(int)color14.R * num5);
										color14.G = (byte)((float)(int)color14.G * num6);
										color14.B = (byte)((float)(int)color14.B * num7);
										SpriteBatch spriteBatch3 = this.spriteBatch;
										Texture2D texture3 = backgroundTexture[5];
										Vector2 position3 = new Vector2(bgStart + num3 * num56 + 16 * num58 + num55, bgStartY + backgroundHeight[2] * num57 + 16 * num59) + value;
										Rectangle? sourceRectangle3 = new Rectangle(16 * num58 + num55 + 16, 16 * num59 + backgroundHeight[2] * magmaBGFrame, 16, 16);
										Color color21 = color14;
										float rotation3 = 0f;
										spriteBatch3.Draw(texture3, position3, sourceRectangle3, color21, rotation3, default(Vector2), 1f, SpriteEffects.None, 0f);
									}
								}
								else
								{
									color14.R = (byte)((float)(int)color14.R * num5);
									color14.G = (byte)((float)(int)color14.G * num6);
									color14.B = (byte)((float)(int)color14.B * num7);
									SpriteBatch spriteBatch4 = this.spriteBatch;
									Texture2D texture4 = backgroundTexture[5];
									Vector2 position4 = new Vector2(bgStart + num3 * num56 + 16 * num58 + num55, bgStartY + backgroundHeight[2] * num57 + 16 * num59) + value;
									Rectangle? sourceRectangle4 = new Rectangle(16 * num58 + num55 + 16, 16 * num59 + backgroundHeight[2] * magmaBGFrame, 16, 16);
									Color color22 = color14;
									float rotation4 = 0f;
									spriteBatch4.Draw(texture4, position4, sourceRectangle4, color22, rotation4, default(Vector2), 1f, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
			}
			Lighting.brightness = Lighting.defBrightness;
			renderTimer[3] = stopwatch.ElapsedMilliseconds;
		}

		public void RenderBackground()
		{
			if (!drawToScreen)
			{
				base.GraphicsDevice.SetRenderTarget(backWaterTarget);
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				try
				{
					DrawWater(bg: true);
				}
				catch
				{
				}
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
				base.GraphicsDevice.SetRenderTarget(backgroundTarget);
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				DrawBackground();
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public void RenderTiles()
		{
			if (!drawToScreen)
			{
				RenderBlack();
				base.GraphicsDevice.SetRenderTarget(tileTarget);
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				DrawTiles();
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public void RenderTiles2()
		{
			if (!drawToScreen)
			{
				base.GraphicsDevice.SetRenderTarget(tile2Target);
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				DrawTiles(solidOnly: false);
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public void RenderWater()
		{
			if (!drawToScreen)
			{
				base.GraphicsDevice.SetRenderTarget(waterTarget);
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				try
				{
					DrawWater();
					if (player[myPlayer].inventory[player[myPlayer].selectedItem].mech)
					{
						DrawWires();
					}
				}
				catch
				{
				}
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public bool FullTile(int x, int y)
		{
			if (tile[x, y].active && tileSolid[tile[x, y].type] && !tileSolidTop[tile[x, y].type] && tile[x, y].type != 10 && tile[x, y].type != 54 && tile[x, y].type != 138)
			{
				int frameX = tile[x, y].frameX;
				int frameY = tile[x, y].frameY;
				if (frameY == 18)
				{
					if (frameX >= 18 && frameX <= 54)
					{
						return true;
					}
					if (frameX >= 108 && frameX <= 144)
					{
						return true;
					}
				}
				else if (frameY >= 90 && frameY <= 196)
				{
					if (frameX <= 70)
					{
						return true;
					}
					if (frameX >= 144 && frameX <= 232)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void DrawBlack()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			int num = (tileColor.R + tileColor.G + tileColor.B) / 3;
			float num2 = (float)((double)num * 0.4) / 255f;
			if (Lighting.lightMode == 2)
			{
				num2 = (float)(tileColor.R - 55) / 255f;
			}
			else if (Lighting.lightMode == 3)
			{
				num2 = (float)(num - 55) / 255f;
			}
			int num3 = (int)((screenPosition.X - value.X) / 16f - 1f);
			int num4 = (int)((screenPosition.X + (float)screenWidth + value.X) / 16f) + 2;
			int num5 = (int)((screenPosition.Y - value.Y) / 16f - 1f);
			int num6 = (int)((screenPosition.Y + (float)screenHeight + value.Y) / 16f) + 5;
			int num7 = offScreenRange / 16;
			int num8 = offScreenRange / 16;
			if (num3 - num7 < 0)
			{
				num3 = num7;
			}
			if (num4 + num7 > maxTilesX)
			{
				num4 = maxTilesX - num7;
			}
			if (num5 - num8 < 0)
			{
				num5 = num8;
			}
			if (num6 + num8 > maxTilesY)
			{
				num6 = maxTilesY - num8;
			}
			for (int i = num5 - num8; i < num6 + num8; i++)
			{
				if (!((double)i <= worldSurface))
				{
					continue;
				}
				for (int j = num3 - num7; j < num4 + num7; j++)
				{
					if (tile[j, i] == null)
					{
						tile[j, i] = new Tile();
					}
					if (!(Lighting.Brightness(j, i) < num2) || (tile[j, i].liquid >= 250 && !WorldGen.SolidTile(j, i) && (tile[j, i].liquid <= 250 || Lighting.Brightness(j, i) != 0f)))
					{
						continue;
					}
					int num9 = j;
					j++;
					while (tile[j, i] != null && Lighting.Brightness(j, i) < num2 && (tile[j, i].liquid < 250 || WorldGen.SolidTile(j, i) || (tile[j, i].liquid > 250 && Lighting.Brightness(j, i) == 0f)))
					{
						j++;
						if (j >= num4 + num7)
						{
							break;
						}
					}
					j--;
					int width = (j - num9 + 1) * 16;
					spriteBatch.Draw(blackTileTexture, new Vector2(num9 * 16 - (int)screenPosition.X, i * 16 - (int)screenPosition.Y) + value, new Rectangle(0, 0, width, 16), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
			}
			renderTimer[5] = stopwatch.ElapsedMilliseconds;
		}

		public void RenderBlack()
		{
			if (!drawToScreen)
			{
				base.GraphicsDevice.SetRenderTarget(blackTarget);
				base.GraphicsDevice.DepthStencilState = new DepthStencilState
				{
					DepthBufferEnable = true
				};
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				DrawBlack();
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public void DrawWalls()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(255f * (1f - gfxQuality) + 100f * gfxQuality);
			int num2 = (int)(120f * (1f - gfxQuality) + 40f * gfxQuality);
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			int num3 = (tileColor.R + tileColor.G + tileColor.B) / 3;
			float num4 = (float)((double)num3 * 0.53) / 255f;
			if (Lighting.lightMode == 2)
			{
				num4 = (float)(tileColor.R - 12) / 255f;
			}
			if (Lighting.lightMode == 3)
			{
				num4 = (float)(num3 - 12) / 255f;
			}
			int num5 = (int)((screenPosition.X - value.X) / 16f - 1f);
			int num6 = (int)((screenPosition.X + (float)screenWidth + value.X) / 16f) + 2;
			int num7 = (int)((screenPosition.Y - value.Y) / 16f - 1f);
			int num8 = (int)((screenPosition.Y + (float)screenHeight + value.Y) / 16f) + 5;
			int num9 = offScreenRange / 16;
			int num10 = offScreenRange / 16;
			if (num5 - num9 < 0)
			{
				num5 = num9;
			}
			if (num6 + num9 > maxTilesX)
			{
				num6 = maxTilesX - num9;
			}
			if (num7 - num10 < 0)
			{
				num7 = num10;
			}
			if (num8 + num10 > maxTilesY)
			{
				num8 = maxTilesY - num10;
			}
			for (int i = num7 - num10; i < num8 + num10; i++)
			{
				if (!((double)i <= worldSurface))
				{
					continue;
				}
				for (int j = num5 - num9; j < num6 + num9; j++)
				{
					if (tile[j, i] == null)
					{
						tile[j, i] = new Tile();
					}
					if (Lighting.Brightness(j, i) < num4 && (tile[j, i].liquid < 250 || WorldGen.SolidTile(j, i) || (tile[j, i].liquid > 250 && Lighting.Brightness(j, i) == 0f)))
					{
						spriteBatch.Draw(blackTileTexture, new Vector2(j * 16 - (int)screenPosition.X, i * 16 - (int)screenPosition.Y) + value, Lighting.GetBlackness(j, i));
					}
				}
			}
			for (int k = num7 - num10; k < num8 + num10; k++)
			{
				for (int l = num5 - num9; l < num6 + num9; l++)
				{
					if (tile[l, k] == null)
					{
						tile[l, k] = new Tile();
					}
					if (tile[l, k].wall <= 0 || !(Lighting.Brightness(l, k) > 0f) || FullTile(l, k))
					{
						continue;
					}
					Color color = Lighting.GetColor(l, k);
					if (Lighting.lightMode < 2 && tile[l, k].wall != 21 && !WorldGen.SolidTile(l, k))
					{
						if (color.R > num || (double)(int)color.G > (double)num * 1.1 || (double)(int)color.B > (double)num * 1.2)
						{
							for (int m = 0; m < 9; m++)
							{
								int num11 = 0;
								int num12 = 0;
								int width = 12;
								int height = 12;
								Color color2 = color;
								Color color3 = color;
								if (m == 0)
								{
									color3 = Lighting.GetColor(l - 1, k - 1);
								}
								if (m == 1)
								{
									width = 8;
									num11 = 12;
									color3 = Lighting.GetColor(l, k - 1);
								}
								if (m == 2)
								{
									color3 = Lighting.GetColor(l + 1, k - 1);
									num11 = 20;
								}
								if (m == 3)
								{
									color3 = Lighting.GetColor(l - 1, k);
									height = 8;
									num12 = 12;
								}
								if (m == 4)
								{
									width = 8;
									height = 8;
									num11 = 12;
									num12 = 12;
								}
								if (m == 5)
								{
									num11 = 20;
									num12 = 12;
									height = 8;
									color3 = Lighting.GetColor(l + 1, k);
								}
								if (m == 6)
								{
									color3 = Lighting.GetColor(l - 1, k + 1);
									num12 = 20;
								}
								if (m == 7)
								{
									width = 12;
									num11 = 12;
									num12 = 20;
									color3 = Lighting.GetColor(l, k + 1);
								}
								if (m == 8)
								{
									color3 = Lighting.GetColor(l + 1, k + 1);
									num11 = 20;
									num12 = 20;
								}
								color2.R = (byte)((color.R + color3.R) / 2);
								color2.G = (byte)((color.G + color3.G) / 2);
								color2.B = (byte)((color.B + color3.B) / 2);
								spriteBatch.Draw(wallTexture[tile[l, k].wall], new Vector2(l * 16 - (int)screenPosition.X - 8 + num11, k * 16 - (int)screenPosition.Y - 8 + num12) + value, new Rectangle(tile[l, k].wallFrameX * 2 + num11, tile[l, k].wallFrameY * 2 + num12, width, height), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else if (color.R > num2 || (double)(int)color.G > (double)num2 * 1.1 || (double)(int)color.B > (double)num2 * 1.2)
						{
							for (int n = 0; n < 4; n++)
							{
								int num13 = 0;
								int num14 = 0;
								Color color4 = color;
								Color color5 = color;
								if (n == 0)
								{
									color5 = ((!Lighting.Brighter(l, k - 1, l - 1, k)) ? Lighting.GetColor(l, k - 1) : Lighting.GetColor(l - 1, k));
								}
								if (n == 1)
								{
									color5 = ((!Lighting.Brighter(l, k - 1, l + 1, k)) ? Lighting.GetColor(l, k - 1) : Lighting.GetColor(l + 1, k));
									num13 = 16;
								}
								if (n == 2)
								{
									color5 = ((!Lighting.Brighter(l, k + 1, l - 1, k)) ? Lighting.GetColor(l, k + 1) : Lighting.GetColor(l - 1, k));
									num14 = 16;
								}
								if (n == 3)
								{
									color5 = ((!Lighting.Brighter(l, k + 1, l + 1, k)) ? Lighting.GetColor(l, k + 1) : Lighting.GetColor(l + 1, k));
									num13 = 16;
									num14 = 16;
								}
								color4.R = (byte)((color.R + color5.R) / 2);
								color4.G = (byte)((color.G + color5.G) / 2);
								color4.B = (byte)((color.B + color5.B) / 2);
								spriteBatch.Draw(wallTexture[tile[l, k].wall], new Vector2(l * 16 - (int)screenPosition.X - 8 + num13, k * 16 - (int)screenPosition.Y - 8 + num14) + value, new Rectangle(tile[l, k].wallFrameX * 2 + num13, tile[l, k].wallFrameY * 2 + num14, 16, 16), color4, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
							}
						}
						else
						{
							Rectangle value2 = new Rectangle(tile[l, k].wallFrameX * 2, tile[l, k].wallFrameY * 2, 32, 32);
							spriteBatch.Draw(wallTexture[tile[l, k].wall], new Vector2(l * 16 - (int)screenPosition.X - 8, k * 16 - (int)screenPosition.Y - 8) + value, value2, Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
					else
					{
						Rectangle value3 = new Rectangle(tile[l, k].wallFrameX * 2, tile[l, k].wallFrameY * 2, 32, 32);
						spriteBatch.Draw(wallTexture[tile[l, k].wall], new Vector2(l * 16 - (int)screenPosition.X - 8, k * 16 - (int)screenPosition.Y - 8) + value, value3, Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
					if ((double)(int)color.R > (double)num2 * 0.4 || (double)(int)color.G > (double)num2 * 0.35 || (double)(int)color.B > (double)num2 * 0.3)
					{
						bool flag = false;
						if (tile[l - 1, k].wall > 0 && wallBlend[tile[l - 1, k].wall] != wallBlend[tile[l, k].wall])
						{
							flag = true;
						}
						bool flag2 = false;
						if (tile[l + 1, k].wall > 0 && wallBlend[tile[l + 1, k].wall] != wallBlend[tile[l, k].wall])
						{
							flag2 = true;
						}
						bool flag3 = false;
						if (tile[l, k - 1].wall > 0 && wallBlend[tile[l, k - 1].wall] != wallBlend[tile[l, k].wall])
						{
							flag3 = true;
						}
						bool flag4 = false;
						if (tile[l, k + 1].wall > 0 && wallBlend[tile[l, k + 1].wall] != wallBlend[tile[l, k].wall])
						{
							flag4 = true;
						}
						if (flag)
						{
							spriteBatch.Draw(wallOutlineTexture, new Vector2(l * 16 - (int)screenPosition.X, k * 16 - (int)screenPosition.Y) + value, new Rectangle(0, 0, 2, 16), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						if (flag2)
						{
							spriteBatch.Draw(wallOutlineTexture, new Vector2(l * 16 - (int)screenPosition.X + 14, k * 16 - (int)screenPosition.Y) + value, new Rectangle(14, 0, 2, 16), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						if (flag3)
						{
							spriteBatch.Draw(wallOutlineTexture, new Vector2(l * 16 - (int)screenPosition.X, k * 16 - (int)screenPosition.Y) + value, new Rectangle(0, 0, 16, 2), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
						if (flag4)
						{
							spriteBatch.Draw(wallOutlineTexture, new Vector2(l * 16 - (int)screenPosition.X, k * 16 - (int)screenPosition.Y + 14) + value, new Rectangle(0, 14, 16, 2), Lighting.GetColor(l, k), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
			renderTimer[2] = stopwatch.ElapsedMilliseconds;
		}

		public void RenderWalls()
		{
			if (!drawToScreen)
			{
				base.GraphicsDevice.SetRenderTarget(wallTarget);
				base.GraphicsDevice.DepthStencilState = new DepthStencilState
				{
					DepthBufferEnable = true
				};
				base.GraphicsDevice.Clear(new Color(0, 0, 0, 0));
				spriteBatch.Begin();
				DrawWalls();
				spriteBatch.End();
				base.GraphicsDevice.SetRenderTarget(globalTarget);
			}
		}

		public void ReleaseTargets()
		{
			try
			{
				if (!dedServ)
				{
					offScreenRange = 0;
					targetSet = false;
					waterTarget.Dispose();
					backWaterTarget.Dispose();
					blackTarget.Dispose();
					tileTarget.Dispose();
					tile2Target.Dispose();
					wallTarget.Dispose();
					backgroundTarget.Dispose();
				}
			}
			catch
			{
			}
		}

		public void InitTargets()
		{
			try
			{
				if (!dedServ)
				{
					offScreenRange = 192;
					targetSet = true;
					if (base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2 > 2048)
					{
						offScreenRange = (2048 - base.GraphicsDevice.PresentationParameters.BackBufferWidth) / 2;
					}
					waterTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					backWaterTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					blackTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					tileTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					tile2Target = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					wallTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
					backgroundTarget = new RenderTarget2D(base.GraphicsDevice, base.GraphicsDevice.PresentationParameters.BackBufferWidth + offScreenRange * 2, base.GraphicsDevice.PresentationParameters.BackBufferHeight + offScreenRange * 2, mipMap: false, base.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
				}
			}
			catch
			{
				Lighting.lightMode = 2;
				try
				{
					ReleaseTargets();
				}
				catch
				{
				}
			}
		}

		public void DrawWires()
		{
			int num = (int)(50f * (1f - gfxQuality) + 2f * gfxQuality);
			Vector2 value = new Vector2(offScreenRange, offScreenRange);
			if (drawToScreen)
			{
				value = default(Vector2);
			}
			int num2 = (int)((screenPosition.X - value.X) / 16f - 1f);
			int num3 = (int)((screenPosition.X + (float)screenWidth + value.X) / 16f) + 2;
			int num4 = (int)((screenPosition.Y - value.Y) / 16f - 1f);
			int num5 = (int)((screenPosition.Y + (float)screenHeight + value.Y) / 16f) + 5;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num3 > maxTilesX)
			{
				num3 = maxTilesX;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > maxTilesY)
			{
				num5 = maxTilesY;
			}
			for (int i = num4; i < num5; i++)
			{
				for (int j = num2; j < num3; j++)
				{
					if (!tile[j, i].wire || !(Lighting.Brightness(j, i) > 0f))
					{
						continue;
					}
					Rectangle rectangle = new Rectangle(0, 0, 16, 16);
					bool wire = tile[j, i - 1].wire;
					bool wire2 = tile[j, i + 1].wire;
					bool wire3 = tile[j - 1, i].wire;
					bool wire4 = tile[j + 1, i].wire;
					rectangle = (wire ? (wire2 ? (wire3 ? (wire4 ? new Rectangle(18, 18, 16, 16) : new Rectangle(54, 0, 16, 16)) : (wire4 ? new Rectangle(36, 0, 16, 16) : new Rectangle(0, 0, 16, 16))) : (wire3 ? (wire4 ? new Rectangle(0, 18, 16, 16) : new Rectangle(54, 18, 16, 16)) : (wire4 ? new Rectangle(36, 18, 16, 16) : new Rectangle(36, 36, 16, 16)))) : (wire2 ? (wire3 ? (wire4 ? new Rectangle(72, 0, 16, 16) : new Rectangle(72, 18, 16, 16)) : (wire4 ? new Rectangle(0, 36, 16, 16) : new Rectangle(18, 36, 16, 16))) : (wire3 ? (wire4 ? new Rectangle(18, 0, 16, 16) : new Rectangle(54, 36, 16, 16)) : (wire4 ? new Rectangle(72, 36, 16, 16) : new Rectangle(0, 54, 16, 16)))));
					Color color = Lighting.GetColor(j, i);
					if (Lighting.lightMode < 2 && (color.R > num || (double)(int)color.G > (double)num * 1.1 || (double)(int)color.B > (double)num * 1.2))
					{
						for (int k = 0; k < 4; k++)
						{
							int num6 = 0;
							int num7 = 0;
							Color color2 = color;
							Color color3 = color;
							if (k == 0)
							{
								color3 = ((!Lighting.Brighter(j, i - 1, j - 1, i)) ? Lighting.GetColor(j, i - 1) : Lighting.GetColor(j - 1, i));
							}
							if (k == 1)
							{
								color3 = ((!Lighting.Brighter(j, i - 1, j + 1, i)) ? Lighting.GetColor(j, i - 1) : Lighting.GetColor(j + 1, i));
								num6 = 8;
							}
							if (k == 2)
							{
								color3 = ((!Lighting.Brighter(j, i + 1, j - 1, i)) ? Lighting.GetColor(j, i + 1) : Lighting.GetColor(j - 1, i));
								num7 = 8;
							}
							if (k == 3)
							{
								color3 = ((!Lighting.Brighter(j, i + 1, j + 1, i)) ? Lighting.GetColor(j, i + 1) : Lighting.GetColor(j + 1, i));
								num6 = 8;
								num7 = 8;
							}
							color2.R = (byte)((color.R + color3.R) / 2);
							color2.G = (byte)((color.G + color3.G) / 2);
							color2.B = (byte)((color.B + color3.B) / 2);
							spriteBatch.Draw(wireTexture, new Vector2(j * 16 - (int)screenPosition.X + num6, i * 16 - (int)screenPosition.Y + num7) + value, new Rectangle(rectangle.X + num6, rectangle.Y + num7, 8, 8), color2, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
						}
					}
					else
					{
						spriteBatch.Draw(wireTexture, new Vector2(j * 16 - (int)screenPosition.X, i * 16 - (int)screenPosition.Y) + value, rectangle, color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			if (Lighting.lightMode >= 2)
			{
				drawToScreen = true;
			}
			else
			{
				drawToScreen = false;
			}
			if (drawToScreen && targetSet)
			{
				ReleaseTargets();
			}
			if (!drawToScreen && !targetSet)
			{
				InitTargets();
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			fpsCount++;
			if (!base.IsActive)
			{
				maxQ = true;
			}
			bool flag;
			if (!dedServ)
			{
				flag = false;
				int num = screenWidth;
				if (num == base.GraphicsDevice.Viewport.Width)
				{
					int num2 = screenHeight;
					if (num2 == base.GraphicsDevice.Viewport.Height)
					{
						goto IL_00b9;
					}
				}
				flag = true;
				if (gamePaused)
				{
					renderNow = true;
				}
				goto IL_00b9;
			}
			goto IL_0184;
			IL_00b9:
			screenWidth = base.GraphicsDevice.Viewport.Width;
			screenHeight = base.GraphicsDevice.Viewport.Height;
			if (screenWidth > maxScreenW)
			{
				screenWidth = maxScreenW;
				flag = true;
			}
			if (screenHeight > maxScreenH)
			{
				screenHeight = maxScreenH;
				flag = true;
			}
			if (screenWidth < minScreenW)
			{
				screenWidth = minScreenW;
				flag = true;
			}
			if (screenHeight < minScreenH)
			{
				screenHeight = minScreenH;
				flag = true;
			}
			if (flag)
			{
				graphics.PreferredBackBufferWidth = screenWidth;
				graphics.PreferredBackBufferHeight = screenHeight;
				graphics.ApplyChanges();
				if (!drawToScreen)
				{
					InitTargets();
				}
			}
			goto IL_0184;
			IL_0184:
			CursorColor();
			drawTime++;
			screenLastPosition = screenPosition;
			if (stackSplit == 0)
			{
				stackCounter = 0;
				stackDelay = 7;
			}
			else
			{
				stackCounter++;
				if (stackCounter >= 30)
				{
					stackDelay--;
					if (stackDelay < 2)
					{
						stackDelay = 2;
					}
					stackCounter = 0;
				}
			}
			mouseTextColor += (byte)mouseTextColorChange;
			if (mouseTextColor >= 250)
			{
				mouseTextColorChange = -4;
			}
			if (mouseTextColor <= 175)
			{
				mouseTextColorChange = 4;
			}
			if (myPlayer >= 0)
			{
				player[myPlayer].mouseInterface = false;
			}
			toolTip = new Item();
			if (!gameMenu && netMode != 2)
			{
				screenPosition.X = player[myPlayer].position.X + (float)player[myPlayer].width * 0.5f - (float)screenWidth * 0.5f;
				screenPosition.Y = player[myPlayer].position.Y + (float)player[myPlayer].height * 0.5f - (float)screenHeight * 0.5f;
				screenPosition.X = (int)screenPosition.X;
				screenPosition.Y = (int)screenPosition.Y;
			}
			if (!gameMenu && netMode != 2)
			{
				if (screenPosition.X < leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
				{
					screenPosition.X = leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				}
				else if (screenPosition.X + (float)screenWidth > rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
				{
					screenPosition.X = rightWorld - (float)screenWidth - (float)(Lighting.offScreenTiles * 16) - 32f;
				}
				if (screenPosition.Y < topWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
				{
					screenPosition.Y = topWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				}
				else if (screenPosition.Y + (float)screenHeight > bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
				{
					screenPosition.Y = bottomWorld - (float)screenHeight - (float)(Lighting.offScreenTiles * 16) - 32f;
				}
			}
			if (showSplash)
			{
				DrawSplash(gameTime);
				return;
			}
			if (!gameMenu)
			{
				if (renderNow)
				{
					screenLastPosition = screenPosition;
					renderNow = false;
					renderCount = 99;
					int tempLightCount = Lighting.tempLightCount;
					Draw(gameTime);
					Lighting.tempLightCount = tempLightCount;
					Lighting.LightTiles(firstTileX, lastTileX, firstTileY, lastTileY);
					Lighting.LightTiles(firstTileX, lastTileX, firstTileY, lastTileY);
					RenderTiles();
					sceneTilePos.X = screenPosition.X - (float)offScreenRange;
					sceneTilePos.Y = screenPosition.Y - (float)offScreenRange;
					RenderBackground();
					sceneBackgroundPos.X = screenPosition.X - (float)offScreenRange;
					sceneBackgroundPos.Y = screenPosition.Y - (float)offScreenRange;
					RenderWalls();
					sceneWallPos.X = screenPosition.X - (float)offScreenRange;
					sceneWallPos.Y = screenPosition.Y - (float)offScreenRange;
					RenderTiles2();
					sceneTile2Pos.X = screenPosition.X - (float)offScreenRange;
					sceneTile2Pos.Y = screenPosition.Y - (float)offScreenRange;
					RenderWater();
					sceneWaterPos.X = screenPosition.X - (float)offScreenRange;
					sceneWaterPos.Y = screenPosition.Y - (float)offScreenRange;
					renderCount = 99;
				}
				else
				{
					if (renderCount == 3)
					{
						RenderTiles();
						sceneTilePos.X = screenPosition.X - (float)offScreenRange;
						sceneTilePos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (renderCount == 2)
					{
						RenderBackground();
						sceneBackgroundPos.X = screenPosition.X - (float)offScreenRange;
						sceneBackgroundPos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (renderCount == 2)
					{
						RenderWalls();
						sceneWallPos.X = screenPosition.X - (float)offScreenRange;
						sceneWallPos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (renderCount == 3)
					{
						RenderTiles2();
						sceneTile2Pos.X = screenPosition.X - (float)offScreenRange;
						sceneTile2Pos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (renderCount == 1)
					{
						RenderWater();
						sceneWaterPos.X = screenPosition.X - (float)offScreenRange;
						sceneWaterPos.Y = screenPosition.Y - (float)offScreenRange;
					}
				}
				if (render && !gameMenu)
				{
					if (Math.Abs(sceneTilePos.X - (screenPosition.X - (float)offScreenRange)) > (float)offScreenRange || Math.Abs(sceneTilePos.Y - (screenPosition.Y - (float)offScreenRange)) > (float)offScreenRange)
					{
						RenderTiles();
						sceneTilePos.X = screenPosition.X - (float)offScreenRange;
						sceneTilePos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (Math.Abs(sceneTile2Pos.X - (screenPosition.X - (float)offScreenRange)) > (float)offScreenRange || Math.Abs(sceneTile2Pos.Y - (screenPosition.Y - (float)offScreenRange)) > (float)offScreenRange)
					{
						RenderTiles2();
						sceneTile2Pos.X = screenPosition.X - (float)offScreenRange;
						sceneTile2Pos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (Math.Abs(sceneBackgroundPos.X - (screenPosition.X - (float)offScreenRange)) > (float)offScreenRange || Math.Abs(sceneBackgroundPos.Y - (screenPosition.Y - (float)offScreenRange)) > (float)offScreenRange)
					{
						RenderBackground();
						sceneBackgroundPos.X = screenPosition.X - (float)offScreenRange;
						sceneBackgroundPos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (Math.Abs(sceneWallPos.X - (screenPosition.X - (float)offScreenRange)) > (float)offScreenRange || Math.Abs(sceneWallPos.Y - (screenPosition.Y - (float)offScreenRange)) > (float)offScreenRange)
					{
						RenderWalls();
						sceneWallPos.X = screenPosition.X - (float)offScreenRange;
						sceneWallPos.Y = screenPosition.Y - (float)offScreenRange;
					}
					if (Math.Abs(sceneWaterPos.X - (screenPosition.X - (float)offScreenRange)) > (float)offScreenRange || Math.Abs(sceneWaterPos.Y - (screenPosition.Y - (float)offScreenRange)) > (float)offScreenRange)
					{
						RenderWater();
						sceneWaterPos.X = screenPosition.X - (float)offScreenRange;
						sceneWaterPos.Y = screenPosition.Y - (float)offScreenRange;
					}
				}
			}
			bgParrallax = 0.1;
			bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, backgroundWidth[background]) - (double)(backgroundWidth[background] / 2));
			bgLoops = screenWidth / backgroundWidth[background] + 2;
			bgStartY = 0;
			bgLoopsY = 0;
			bgTop = (int)((0.0 - (double)screenPosition.Y) / (worldSurface * 16.0 - 600.0) * 200.0);
			bgColor = Color.White;
			if (gameMenu || netMode == 2)
			{
				bgTop = -200;
			}
			int num3 = (int)(time / 54000.0 * (double)(screenWidth + sunTexture.Width * 2)) - sunTexture.Width;
			int num4 = 0;
			Color white = Color.White;
			float num5 = 1f;
			float rotation = (float)(time / 54000.0) * 2f - 7.3f;
			int num6 = (int)(time / 32400.0 * (double)(screenWidth + moonTexture.Width * 2)) - moonTexture.Width;
			int num7 = 0;
			Color white2 = Color.White;
			float num8 = 1f;
			float rotation2 = (float)(time / 32400.0) * 2f - 7.3f;
			if (dayTime)
			{
				double num9;
				if (time < 27000.0)
				{
					num9 = Math.Pow(1.0 - time / 54000.0 * 2.0, 2.0);
					num4 = (int)((double)bgTop + num9 * 250.0 + 180.0);
				}
				else
				{
					num9 = Math.Pow((time / 54000.0 - 0.5) * 2.0, 2.0);
					num4 = (int)((double)bgTop + num9 * 250.0 + 180.0);
				}
				num5 = (float)(1.2 - num9 * 0.4);
			}
			else
			{
				double num10;
				if (time < 16200.0)
				{
					num10 = Math.Pow(1.0 - time / 32400.0 * 2.0, 2.0);
					num7 = (int)((double)bgTop + num10 * 250.0 + 180.0);
				}
				else
				{
					num10 = Math.Pow((time / 32400.0 - 0.5) * 2.0, 2.0);
					num7 = (int)((double)bgTop + num10 * 250.0 + 180.0);
				}
				num8 = (float)(1.2 - num10 * 0.4);
			}
			if (dayTime)
			{
				if (time < 13500.0)
				{
					float num11 = (float)(time / 13500.0);
					white.R = (byte)(num11 * 200f + 55f);
					white.G = (byte)(num11 * 180f + 75f);
					white.B = (byte)(num11 * 250f + 5f);
					bgColor.R = (byte)(num11 * 230f + 25f);
					bgColor.G = (byte)(num11 * 220f + 35f);
					bgColor.B = (byte)(num11 * 220f + 35f);
				}
				if (time > 45900.0)
				{
					float num12 = (float)(1.0 - (time / 54000.0 - 0.85) * 6.666666666666667);
					white.R = (byte)(num12 * 120f + 55f);
					white.G = (byte)(num12 * 100f + 25f);
					white.B = (byte)(num12 * 120f + 55f);
					bgColor.R = (byte)(num12 * 200f + 35f);
					bgColor.G = (byte)(num12 * 85f + 35f);
					bgColor.B = (byte)(num12 * 135f + 35f);
				}
				else if (time > 37800.0)
				{
					float num13 = (float)(1.0 - (time / 54000.0 - 0.7) * 6.666666666666667);
					white.R = (byte)(num13 * 80f + 175f);
					white.G = (byte)(num13 * 130f + 125f);
					white.B = (byte)(num13 * 100f + 155f);
					bgColor.R = (byte)(num13 * 20f + 235f);
					bgColor.G = (byte)(num13 * 135f + 120f);
					bgColor.B = (byte)(num13 * 85f + 170f);
				}
			}
			if (!dayTime)
			{
				if (bloodMoon)
				{
					if (time < 16200.0)
					{
						float num14 = (float)(1.0 - time / 16200.0);
						white2.R = (byte)(num14 * 10f + 205f);
						white2.G = (byte)(num14 * 170f + 55f);
						white2.B = (byte)(num14 * 200f + 55f);
						bgColor.R = (byte)(40f - num14 * 40f + 35f);
						bgColor.G = (byte)(num14 * 20f + 15f);
						bgColor.B = (byte)(num14 * 20f + 15f);
					}
					else if (time >= 16200.0)
					{
						float num15 = (float)((time / 32400.0 - 0.5) * 2.0);
						white2.R = (byte)(num15 * 50f + 205f);
						white2.G = (byte)(num15 * 100f + 155f);
						white2.B = (byte)(num15 * 100f + 155f);
						white2.R = (byte)(num15 * 10f + 205f);
						white2.G = (byte)(num15 * 170f + 55f);
						white2.B = (byte)(num15 * 200f + 55f);
						bgColor.R = (byte)(40f - num15 * 40f + 35f);
						bgColor.G = (byte)(num15 * 20f + 15f);
						bgColor.B = (byte)(num15 * 20f + 15f);
					}
				}
				else if (time < 16200.0)
				{
					float num16 = (float)(1.0 - time / 16200.0);
					white2.R = (byte)(num16 * 10f + 205f);
					white2.G = (byte)(num16 * 70f + 155f);
					white2.B = (byte)(num16 * 100f + 155f);
					bgColor.R = (byte)(num16 * 20f + 15f);
					bgColor.G = (byte)(num16 * 20f + 15f);
					bgColor.B = (byte)(num16 * 20f + 15f);
				}
				else if (time >= 16200.0)
				{
					float num17 = (float)((time / 32400.0 - 0.5) * 2.0);
					white2.R = (byte)(num17 * 50f + 205f);
					white2.G = (byte)(num17 * 100f + 155f);
					white2.B = (byte)(num17 * 100f + 155f);
					bgColor.R = (byte)(num17 * 10f + 15f);
					bgColor.G = (byte)(num17 * 20f + 15f);
					bgColor.B = (byte)(num17 * 20f + 15f);
				}
			}
			if (gameMenu || netMode == 2)
			{
				bgTop = 0;
				if (!dayTime)
				{
					bgColor.R = 35;
					bgColor.G = 35;
					bgColor.B = 35;
				}
			}
			if (gameMenu)
			{
				bgDelay = 1000;
				evilTiles = (int)(bgAlpha[1] * 500f);
			}
			if (evilTiles > 0)
			{
				float num18 = (float)evilTiles / 500f;
				if (num18 > 1f)
				{
					num18 = 1f;
				}
				int r = bgColor.R;
				int g = bgColor.G;
				int b = bgColor.B;
				r -= (int)(100f * num18 * ((float)(int)bgColor.R / 255f));
				g -= (int)(140f * num18 * ((float)(int)bgColor.G / 255f));
				b -= (int)(80f * num18 * ((float)(int)bgColor.B / 255f));
				if (r < 15)
				{
					r = 15;
				}
				if (g < 15)
				{
					g = 15;
				}
				if (b < 15)
				{
					b = 15;
				}
				bgColor.R = (byte)r;
				bgColor.G = (byte)g;
				bgColor.B = (byte)b;
				r = white.R;
				g = white.G;
				b = white.B;
				r -= (int)(100f * num18 * ((float)(int)white.R / 255f));
				g -= (int)(100f * num18 * ((float)(int)white.G / 255f));
				b -= (int)(0f * num18 * ((float)(int)white.B / 255f));
				if (r < 15)
				{
					r = 15;
				}
				if (g < 15)
				{
					g = 15;
				}
				if (b < 15)
				{
					b = 15;
				}
				white.R = (byte)r;
				white.G = (byte)g;
				white.B = (byte)b;
				r = white2.R;
				g = white2.G;
				b = white2.B;
				r -= (int)(140f * num18 * ((float)(int)white2.R / 255f));
				g -= (int)(190f * num18 * ((float)(int)white2.G / 255f));
				b -= (int)(170f * num18 * ((float)(int)white2.B / 255f));
				if (r < 15)
				{
					r = 15;
				}
				if (g < 15)
				{
					g = 15;
				}
				if (b < 15)
				{
					b = 15;
				}
				white2.R = (byte)r;
				white2.G = (byte)g;
				white2.B = (byte)b;
			}
			if (jungleTiles > 0)
			{
				float num19 = (float)jungleTiles / 200f;
				if (num19 > 1f)
				{
					num19 = 1f;
				}
				int r2 = bgColor.R;
				int num20 = bgColor.G;
				int b2 = bgColor.B;
				r2 -= (int)(20f * num19 * ((float)(int)bgColor.R / 255f));
				b2 -= (int)(90f * num19 * ((float)(int)bgColor.B / 255f));
				if (num20 > 255)
				{
					num20 = 255;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (r2 > 255)
				{
					r2 = 255;
				}
				if (r2 < 15)
				{
					r2 = 15;
				}
				if (b2 < 15)
				{
					b2 = 15;
				}
				bgColor.R = (byte)r2;
				bgColor.G = (byte)num20;
				bgColor.B = (byte)b2;
				r2 = white.R;
				num20 = white.G;
				b2 = white.B;
				r2 -= (int)(30f * num19 * ((float)(int)white.R / 255f));
				b2 -= (int)(10f * num19 * ((float)(int)white.B / 255f));
				if (r2 < 15)
				{
					r2 = 15;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (b2 < 15)
				{
					b2 = 15;
				}
				white.R = (byte)r2;
				white.G = (byte)num20;
				white.B = (byte)b2;
				r2 = white2.R;
				num20 = white2.G;
				b2 = white2.B;
				num20 -= (int)(140f * num19 * ((float)(int)white2.R / 255f));
				r2 -= (int)(170f * num19 * ((float)(int)white2.G / 255f));
				b2 -= (int)(190f * num19 * ((float)(int)white2.B / 255f));
				if (r2 < 15)
				{
					r2 = 15;
				}
				if (num20 < 15)
				{
					num20 = 15;
				}
				if (b2 < 15)
				{
					b2 = 15;
				}
				white2.R = (byte)r2;
				white2.G = (byte)num20;
				white2.B = (byte)b2;
			}
			if (bgColor.R < 15)
			{
				bgColor.R = 15;
			}
			if (bgColor.G < 15)
			{
				bgColor.G = 15;
			}
			if (bgColor.B < 15)
			{
				bgColor.B = 15;
			}
			if (bloodMoon)
			{
				if (bgColor.R < 25)
				{
					bgColor.R = 25;
				}
				if (bgColor.G < 25)
				{
					bgColor.G = 25;
				}
				if (bgColor.B < 25)
				{
					bgColor.B = 25;
				}
			}
			tileColor.A = byte.MaxValue;
			tileColor.R = (byte)((bgColor.R + bgColor.B + bgColor.G) / 3);
			tileColor.G = (byte)((bgColor.R + bgColor.B + bgColor.G) / 3);
			tileColor.B = (byte)((bgColor.R + bgColor.B + bgColor.G) / 3);
			tileColor.R = (byte)((bgColor.R + bgColor.G + bgColor.B + bgColor.R * 7) / 10);
			tileColor.G = (byte)((bgColor.R + bgColor.G + bgColor.B + bgColor.G * 7) / 10);
			tileColor.B = (byte)((bgColor.R + bgColor.G + bgColor.B + bgColor.B * 7) / 10);
			if (tileColor.R >= byte.MaxValue && tileColor.G >= byte.MaxValue)
			{
				_ = tileColor.B;
			}
			float num21 = maxTilesX / 4200;
			num21 *= num21;
			float num22 = (float)((double)((screenPosition.Y + (float)(screenHeight / 2)) / 16f - (65f + 10f * num21)) / (worldSurface / 5.0));
			if (num22 < 0f)
			{
				num22 = 0f;
			}
			if (num22 > 1f)
			{
				num22 = 1f;
			}
			if (gameMenu)
			{
				num22 = 1f;
			}
			bgColor.R = (byte)((float)(int)bgColor.R * num22);
			bgColor.G = (byte)((float)(int)bgColor.G * num22);
			bgColor.B = (byte)((float)(int)bgColor.B * num22);
			base.GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
			if (savePlayerSnapshot)
			{
				savePlayerSnapshot = false;
				if (loadPlayersAfterScreenshot)
				{
					LoadPlayers();
					loadPlayersAfterScreenshot = false;
				}
			}
			this.spriteBatch.Begin();
			if (menuMode != 100)
			{
				Codable.RunGlobalMethod("ModWorld", "PreDrawALL", this.spriteBatch, gameMenu);
			}
			if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
			{
				for (int i = 0; i < bgLoops; i++)
				{
					this.spriteBatch.Draw(backgroundTexture[background], new Rectangle(bgStart + backgroundWidth[background] * i, bgTop, backgroundWidth[background], backgroundHeight[background]), bgColor);
				}
			}
			if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0 && 255 - bgColor.R - 100 > 0 && netMode != 2)
			{
				for (int j = 0; j < numStars; j++)
				{
					Color color = default(Color);
					float num23 = (float)evilTiles / 500f;
					if (num23 > 1f)
					{
						num23 = 1f;
					}
					num23 = 1f - num23 * 0.5f;
					if (evilTiles <= 0)
					{
						num23 = 1f;
					}
					int num24 = (int)((float)(255 - bgColor.R - 100) * star[j].twinkle * num23);
					int num25 = (int)((float)(255 - bgColor.G - 100) * star[j].twinkle * num23);
					int num26 = (int)((float)(255 - bgColor.B - 100) * star[j].twinkle * num23);
					if (num24 < 0)
					{
						num24 = 0;
					}
					if (num25 < 0)
					{
						num25 = 0;
					}
					if (num26 < 0)
					{
						num26 = 0;
					}
					color.R = (byte)num24;
					color.G = (byte)((float)num25 * num23);
					color.B = (byte)((float)num26 * num23);
					float num27 = star[j].position.X * ((float)screenWidth / 800f);
					float num28 = star[j].position.Y * ((float)screenHeight / 600f);
					this.spriteBatch.Draw(starTexture[star[j].type], new Vector2(num27 + (float)starTexture[star[j].type].Width * 0.5f, num28 + (float)starTexture[star[j].type].Height * 0.5f + (float)bgTop), new Rectangle(0, 0, starTexture[star[j].type].Width, starTexture[star[j].type].Height), color, star[j].rotation, new Vector2((float)starTexture[star[j].type].Width * 0.5f, (float)starTexture[star[j].type].Height * 0.5f), star[j].scale * star[j].twinkle, SpriteEffects.None, 0f);
				}
			}
			if ((double)(screenPosition.Y / 16f) < worldSurface + 2.0)
			{
				if (dayTime)
				{
					num5 *= 1.1f;
					if (!gameMenu && player[myPlayer].head == 12)
					{
						this.spriteBatch.Draw(sun2Texture, new Vector2(num3, num4 + sunModY), new Rectangle(0, 0, sunTexture.Width, sunTexture.Height), white, rotation, new Vector2(sunTexture.Width / 2, sunTexture.Height / 2), num5, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.Draw(sunTexture, new Vector2(num3, num4 + sunModY), new Rectangle(0, 0, sunTexture.Width, sunTexture.Height), white, rotation, new Vector2(sunTexture.Width / 2, sunTexture.Height / 2), num5, SpriteEffects.None, 0f);
					}
				}
				if (!dayTime)
				{
					this.spriteBatch.Draw(moonTexture, new Vector2(num6, num7 + moonModY), new Rectangle(0, moonTexture.Width * moonPhase, moonTexture.Width, moonTexture.Width), white2, rotation2, new Vector2(moonTexture.Width / 2, moonTexture.Width / 2), num8, SpriteEffects.None, 0f);
				}
			}
			Rectangle value = dayTime ? new Rectangle((int)((double)num3 - (double)sunTexture.Width * 0.5 * (double)num5), (int)((double)num4 - (double)sunTexture.Height * 0.5 * (double)num5 + (double)sunModY), (int)((float)sunTexture.Width * num5), (int)((float)sunTexture.Width * num5)) : new Rectangle((int)((double)num6 - (double)moonTexture.Width * 0.5 * (double)num8), (int)((double)num7 - (double)moonTexture.Width * 0.5 * (double)num8 + (double)moonModY), (int)((float)moonTexture.Width * num8), (int)((float)moonTexture.Width * num8));
			Rectangle rectangle = new Rectangle(mouseX, mouseY, 1, 1);
			sunModY = (short)((double)sunModY * 0.999);
			moonModY = (short)((double)moonModY * 0.999);
			if (gameMenu && netMode != 1)
			{
				if (mouseLeft && hasFocus)
				{
					if (rectangle.Intersects(value) || grabSky)
					{
						if (dayTime)
						{
							time = 54000.0 * (double)((float)(mouseX + sunTexture.Width) / ((float)screenWidth + (float)(sunTexture.Width * 2)));
							sunModY = (short)(mouseY - num4);
							if (time > 53990.0)
							{
								time = 53990.0;
							}
						}
						else
						{
							time = 32400.0 * (double)((float)(mouseX + moonTexture.Width) / ((float)screenWidth + (float)(moonTexture.Width * 2)));
							moonModY = (short)(mouseY - num7);
							if (time > 32390.0)
							{
								time = 32390.0;
							}
						}
						if (time < 10.0)
						{
							time = 10.0;
						}
						if (netMode != 0)
						{
							NetMessage.SendData(18);
						}
						grabSky = true;
					}
				}
				else
				{
					grabSky = false;
				}
			}
			float num29 = screenHeight - 600;
			bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1200.0 + 1190.0);
			float num30 = bgTop - 50;
			if (resetClouds)
			{
				Cloud.resetClouds();
				resetClouds = false;
			}
			if (base.IsActive || netMode != 0)
			{
				windSpeedSpeed += (float)rand.Next(-10, 11) * 0.0001f;
				if (!dayTime)
				{
					windSpeedSpeed += (float)rand.Next(-10, 11) * 0.0002f;
				}
				if ((double)windSpeedSpeed < -0.002)
				{
					windSpeedSpeed = -0.002f;
				}
				if ((double)windSpeedSpeed > 0.002)
				{
					windSpeedSpeed = 0.002f;
				}
				windSpeed += windSpeedSpeed;
				if ((double)windSpeed < -0.3)
				{
					windSpeed = -0.3f;
				}
				if ((double)windSpeed > 0.3)
				{
					windSpeed = 0.3f;
				}
				numClouds += rand.Next(-1, 2);
				if (numClouds < 0)
				{
					numClouds = 0;
				}
				if (numClouds > cloudLimit)
				{
					numClouds = cloudLimit;
				}
			}
			if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
			{
				for (int k = 0; k < 100; k++)
				{
					if (cloud[k].active && cloud[k].scale < 1f)
					{
						Color color2 = cloud[k].cloudColor(bgColor);
						if (num22 < 1f)
						{
							color2.R = (byte)((float)(int)color2.R * num22);
							color2.G = (byte)((float)(int)color2.G * num22);
							color2.B = (byte)((float)(int)color2.B * num22);
							color2.A = (byte)((float)(int)color2.A * num22);
						}
						float num31 = cloud[k].position.Y * ((float)screenHeight / 600f);
						float num32 = (float)((double)(screenPosition.Y / 16f - 24f) / worldSurface);
						if (num32 < 0f)
						{
							num32 = 0f;
						}
						if (num32 > 1f)
						{
							num32 = 1f;
						}
						if (gameMenu)
						{
							num32 = 1f;
						}
						this.spriteBatch.Draw(cloudTexture[cloud[k].type], new Vector2(cloud[k].position.X + (float)cloudTexture[cloud[k].type].Width * 0.5f, num31 + (float)cloudTexture[cloud[k].type].Height * 0.5f + num30), new Rectangle(0, 0, cloudTexture[cloud[k].type].Width, cloudTexture[cloud[k].type].Height), color2, cloud[k].rotation, new Vector2((float)cloudTexture[cloud[k].type].Width * 0.5f, (float)cloudTexture[cloud[k].type].Height * 0.5f), cloud[k].scale, SpriteEffects.None, 0f);
					}
				}
			}
			num22 = 1f;
			float num33 = 1f;
			num33 *= 2f;
			bgParrallax = 0.15;
			int num34 = (int)((float)backgroundWidth[7] * num33);
			Color color3 = bgColor;
			Color color4 = color3;
			if (num22 < 1f)
			{
				color3.R = (byte)((float)(int)color3.R * num22);
				color3.G = (byte)((float)(int)color3.G * num22);
				color3.B = (byte)((float)(int)color3.B * num22);
				color3.A = (byte)((float)(int)color3.A * num22);
			}
			bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1300.0 + 1090.0);
			if (owBack)
			{
				bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
				bgLoops = screenWidth / num34 + 2;
				if (gameMenu)
				{
					bgTop = 100;
				}
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[0]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[0]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[0]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[0]);
					if (bgAlpha2[0] > 0f)
					{
						for (int l = 0; l < bgLoops; l++)
						{
							SpriteBatch spriteBatch = this.spriteBatch;
							Texture2D texture = backgroundTexture[7];
							Vector2 position = new Vector2(bgStart + num34 * l, bgTop);
							Rectangle? sourceRectangle = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color5 = color3;
							float rotation3 = 0f;
							spriteBatch.Draw(texture, position, sourceRectangle, color5, rotation3, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[1]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[1]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[1]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[1]);
					if (bgAlpha2[1] > 0f)
					{
						for (int m = 0; m < bgLoops; m++)
						{
							SpriteBatch spriteBatch2 = this.spriteBatch;
							Texture2D texture2 = backgroundTexture[23];
							Vector2 position2 = new Vector2(bgStart + num34 * m, bgTop);
							Rectangle? sourceRectangle2 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color6 = color3;
							float rotation4 = 0f;
							spriteBatch2.Draw(texture2, position2, sourceRectangle2, color6, rotation4, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[2]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[2]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[2]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[2]);
					if (bgAlpha2[2] > 0f)
					{
						for (int n = 0; n < bgLoops; n++)
						{
							SpriteBatch spriteBatch3 = this.spriteBatch;
							Texture2D texture3 = backgroundTexture[24];
							Vector2 position3 = new Vector2(bgStart + num34 * n, bgTop);
							Rectangle? sourceRectangle3 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color7 = color3;
							float rotation5 = 0f;
							spriteBatch3.Draw(texture3, position3, sourceRectangle3, color7, rotation5, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
			}
			num30 = bgTop - 50;
			if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
			{
				for (int num35 = 0; num35 < 100; num35++)
				{
					if (cloud[num35].active && (double)cloud[num35].scale < 1.15 && cloud[num35].scale >= 1f)
					{
						Color color8 = cloud[num35].cloudColor(bgColor);
						if (num22 < 1f)
						{
							color8.R = (byte)((float)(int)color8.R * num22);
							color8.G = (byte)((float)(int)color8.G * num22);
							color8.B = (byte)((float)(int)color8.B * num22);
							color8.A = (byte)((float)(int)color8.A * num22);
						}
						float num36 = cloud[num35].position.Y * ((float)screenHeight / 600f);
						float num37 = (float)((double)(screenPosition.Y / 16f - 24f) / worldSurface);
						if (num37 < 0f)
						{
							num37 = 0f;
						}
						if (num37 > 1f)
						{
							num37 = 1f;
						}
						if (gameMenu)
						{
							num37 = 1f;
						}
						this.spriteBatch.Draw(cloudTexture[cloud[num35].type], new Vector2(cloud[num35].position.X + (float)cloudTexture[cloud[num35].type].Width * 0.5f, num36 + (float)cloudTexture[cloud[num35].type].Height * 0.5f + num30), new Rectangle(0, 0, cloudTexture[cloud[num35].type].Width, cloudTexture[cloud[num35].type].Height), color8, cloud[num35].rotation, new Vector2((float)cloudTexture[cloud[num35].type].Width * 0.5f, (float)cloudTexture[cloud[num35].type].Height * 0.5f), cloud[num35].scale, SpriteEffects.None, 0f);
					}
				}
			}
			if (holyTiles > 0 && owBack)
			{
				bgParrallax = 0.17;
				num33 = 1.1f;
				num33 *= 2f;
				num34 = (int)((double)(3500f * num33) * 1.05);
				bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
				bgLoops = screenWidth / num34 + 2;
				bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1400.0 + 900.0);
				if (gameMenu)
				{
					bgTop = 230;
					bgStart -= 500;
				}
				Color color9 = color4;
				float num38 = (float)holyTiles / 400f;
				if (num38 > 0.5f)
				{
					num38 = 0.5f;
				}
				color9.R = (byte)((float)(int)color9.R * num38);
				color9.G = (byte)((float)(int)color9.G * num38);
				color9.B = (byte)((float)(int)color9.B * num38);
				color9.A = (byte)((float)(int)color9.A * num38 * 0.8f);
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					for (int num39 = 0; num39 < bgLoops; num39++)
					{
						SpriteBatch spriteBatch4 = this.spriteBatch;
						Texture2D texture4 = backgroundTexture[18];
						Vector2 position4 = new Vector2(bgStart + num34 * num39, bgTop);
						Rectangle? sourceRectangle4 = new Rectangle(0, 0, backgroundWidth[18], backgroundHeight[18]);
						Color color10 = color9;
						float rotation6 = 0f;
						spriteBatch4.Draw(texture4, position4, sourceRectangle4, color10, rotation6, default(Vector2), num33, SpriteEffects.None, 0f);
						SpriteBatch spriteBatch5 = this.spriteBatch;
						Texture2D texture5 = backgroundTexture[19];
						Vector2 position5 = new Vector2(bgStart + num34 * num39 + 1700, bgTop + 100);
						Rectangle? sourceRectangle5 = new Rectangle(0, 0, backgroundWidth[19], backgroundHeight[19]);
						Color color11 = color9;
						float rotation7 = 0f;
						spriteBatch5.Draw(texture5, position5, sourceRectangle5, color11, rotation7, default(Vector2), num33 * 0.9f, SpriteEffects.None, 0f);
					}
				}
			}
			bgParrallax = 0.2;
			num33 = 1.15f;
			num33 *= 2f;
			num34 = (int)((float)backgroundWidth[7] * num33);
			bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
			bgLoops = screenWidth / num34 + 2;
			bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1400.0 + 1260.0);
			if (owBack)
			{
				if (gameMenu)
				{
					bgTop = 230;
					bgStart -= 500;
				}
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[0]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[0]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[0]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[0]);
					if (bgAlpha2[0] > 0f)
					{
						for (int num40 = 0; num40 < bgLoops; num40++)
						{
							SpriteBatch spriteBatch6 = this.spriteBatch;
							Texture2D texture6 = backgroundTexture[8];
							Vector2 position6 = new Vector2(bgStart + num34 * num40, bgTop);
							Rectangle? sourceRectangle6 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color12 = color3;
							float rotation8 = 0f;
							spriteBatch6.Draw(texture6, position6, sourceRectangle6, color12, rotation8, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[1]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[1]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[1]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[1]);
					if (bgAlpha2[1] > 0f)
					{
						for (int num41 = 0; num41 < bgLoops; num41++)
						{
							SpriteBatch spriteBatch7 = this.spriteBatch;
							Texture2D texture7 = backgroundTexture[22];
							Vector2 position7 = new Vector2(bgStart + num34 * num41, bgTop);
							Rectangle? sourceRectangle7 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color13 = color3;
							float rotation9 = 0f;
							spriteBatch7.Draw(texture7, position7, sourceRectangle7, color13, rotation9, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[2]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[2]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[2]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[2]);
					if (bgAlpha2[2] > 0f)
					{
						for (int num42 = 0; num42 < bgLoops; num42++)
						{
							SpriteBatch spriteBatch8 = this.spriteBatch;
							Texture2D texture8 = backgroundTexture[25];
							Vector2 position8 = new Vector2(bgStart + num34 * num42, bgTop);
							Rectangle? sourceRectangle8 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color14 = color3;
							float rotation10 = 0f;
							spriteBatch8.Draw(texture8, position8, sourceRectangle8, color14, rotation10, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					color3 = color4;
					color3.R = (byte)((float)(int)color3.R * bgAlpha2[3]);
					color3.G = (byte)((float)(int)color3.G * bgAlpha2[3]);
					color3.B = (byte)((float)(int)color3.B * bgAlpha2[3]);
					color3.A = (byte)((float)(int)color3.A * bgAlpha2[3]);
					if (bgAlpha2[3] > 0f)
					{
						for (int num43 = 0; num43 < bgLoops; num43++)
						{
							SpriteBatch spriteBatch9 = this.spriteBatch;
							Texture2D texture9 = backgroundTexture[28];
							Vector2 position9 = new Vector2(bgStart + num34 * num43, bgTop);
							Rectangle? sourceRectangle9 = new Rectangle(0, 0, backgroundWidth[7], backgroundHeight[7]);
							Color color15 = color3;
							float rotation11 = 0f;
							spriteBatch9.Draw(texture9, position9, sourceRectangle9, color15, rotation11, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
			}
			num30 = (float)bgTop * 1.01f - 150f;
			if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
			{
				for (int num44 = 0; num44 < 100; num44++)
				{
					if (cloud[num44].active && cloud[num44].scale > num33)
					{
						Color color16 = cloud[num44].cloudColor(bgColor);
						if (num22 < 1f)
						{
							color16.R = (byte)((float)(int)color16.R * num22);
							color16.G = (byte)((float)(int)color16.G * num22);
							color16.B = (byte)((float)(int)color16.B * num22);
							color16.A = (byte)((float)(int)color16.A * num22);
						}
						float num45 = cloud[num44].position.Y * ((float)screenHeight / 600f);
						float num46 = (float)((double)(screenPosition.Y / 16f - 24f) / worldSurface);
						if (num46 < 0f)
						{
							num46 = 0f;
						}
						if (num46 > 1f)
						{
							num46 = 1f;
						}
						if (gameMenu)
						{
							num46 = 1f;
						}
						this.spriteBatch.Draw(cloudTexture[cloud[num44].type], new Vector2(cloud[num44].position.X + (float)cloudTexture[cloud[num44].type].Width * 0.5f, num45 + (float)cloudTexture[cloud[num44].type].Height * 0.5f + num30), new Rectangle(0, 0, cloudTexture[cloud[num44].type].Width, cloudTexture[cloud[num44].type].Height), color16, cloud[num44].rotation, new Vector2((float)cloudTexture[cloud[num44].type].Width * 0.5f, (float)cloudTexture[cloud[num44].type].Height * 0.5f), cloud[num44].scale, SpriteEffects.None, 0f);
					}
				}
			}
			int num47 = bgStyle;
			int num48 = (int)((screenPosition.X + (float)(screenWidth / 2)) / 16f);
			num47 = ((num48 < 380 || num48 > maxTilesX - 380) ? 4 : ((sandTiles > 1000) ? (player[myPlayer].zoneEvil ? 5 : ((!player[myPlayer].zoneHoly) ? 2 : 5)) : (player[myPlayer].zoneHoly ? 6 : (player[myPlayer].zoneEvil ? 1 : (player[myPlayer].zoneJungle ? 3 : 0)))));
			float num49 = 0.05f;
			int num50 = 30;
			if (num47 == 0)
			{
				num50 = 120;
			}
			if (bgDelay < 0)
			{
				bgDelay++;
			}
			else if (num47 != bgStyle)
			{
				bgDelay++;
				if (bgDelay > num50)
				{
					bgDelay = -60;
					bgStyle = num47;
					if (num47 == 0)
					{
						bgDelay = 0;
					}
				}
			}
			else if (bgDelay > 0)
			{
				bgDelay--;
			}
			if (gameMenu)
			{
				num49 = 0.02f;
				if (!dayTime)
				{
					bgStyle = 1;
				}
				else
				{
					bgStyle = 0;
				}
				num47 = bgStyle;
			}
			if (quickBG > 0)
			{
				quickBG--;
				bgStyle = num47;
				num49 = 1f;
			}
			if (bgStyle == 2)
			{
				bgAlpha2[0] -= num49;
				if (bgAlpha2[0] < 0f)
				{
					bgAlpha2[0] = 0f;
				}
				bgAlpha2[1] += num49;
				if (bgAlpha2[1] > 1f)
				{
					bgAlpha2[1] = 1f;
				}
				bgAlpha2[2] -= num49;
				if (bgAlpha2[2] < 0f)
				{
					bgAlpha2[2] = 0f;
				}
				bgAlpha2[3] -= num49;
				if (bgAlpha2[3] < 0f)
				{
					bgAlpha2[3] = 0f;
				}
			}
			else if (bgStyle == 5 || bgStyle == 1 || bgStyle == 6)
			{
				bgAlpha2[0] -= num49;
				if (bgAlpha2[0] < 0f)
				{
					bgAlpha2[0] = 0f;
				}
				bgAlpha2[1] -= num49;
				if (bgAlpha2[1] < 0f)
				{
					bgAlpha2[1] = 0f;
				}
				bgAlpha2[2] += num49;
				if (bgAlpha2[2] > 1f)
				{
					bgAlpha2[2] = 1f;
				}
				bgAlpha2[3] -= num49;
				if (bgAlpha2[3] < 0f)
				{
					bgAlpha2[3] = 0f;
				}
			}
			else if (bgStyle == 4)
			{
				bgAlpha2[0] -= num49;
				if (bgAlpha2[0] < 0f)
				{
					bgAlpha2[0] = 0f;
				}
				bgAlpha2[1] -= num49;
				if (bgAlpha2[1] < 0f)
				{
					bgAlpha2[1] = 0f;
				}
				bgAlpha2[2] -= num49;
				if (bgAlpha2[2] < 0f)
				{
					bgAlpha2[2] = 0f;
				}
				bgAlpha2[3] += num49;
				if (bgAlpha2[3] > 1f)
				{
					bgAlpha2[3] = 1f;
				}
			}
			else
			{
				bgAlpha2[0] += num49;
				if (bgAlpha2[0] > 1f)
				{
					bgAlpha2[0] = 1f;
				}
				bgAlpha2[1] -= num49;
				if (bgAlpha2[1] < 0f)
				{
					bgAlpha2[1] = 0f;
				}
				bgAlpha2[2] -= num49;
				if (bgAlpha2[2] < 0f)
				{
					bgAlpha2[2] = 0f;
				}
				bgAlpha2[3] -= num49;
				if (bgAlpha2[3] < 0f)
				{
					bgAlpha2[3] = 0f;
				}
			}
			for (int num51 = 0; num51 < 7; num51++)
			{
				if (bgStyle == num51)
				{
					bgAlpha[num51] += num49;
					if (bgAlpha[num51] > 1f)
					{
						bgAlpha[num51] = 1f;
					}
				}
				else
				{
					bgAlpha[num51] -= num49;
					if (bgAlpha[num51] < 0f)
					{
						bgAlpha[num51] = 0f;
					}
				}
				if (!owBack)
				{
					continue;
				}
				color3 = color4;
				color3.R = (byte)((float)(int)color3.R * bgAlpha[num51]);
				color3.G = (byte)((float)(int)color3.G * bgAlpha[num51]);
				color3.B = (byte)((float)(int)color3.B * bgAlpha[num51]);
				color3.A = (byte)((float)(int)color3.A * bgAlpha[num51]);
				if (bgAlpha[num51] > 0f && num51 == 3)
				{
					num33 = 1.25f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.4;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1660.0);
					if (gameMenu)
					{
						bgTop = 320;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num52 = 0; num52 < bgLoops; num52++)
						{
							SpriteBatch spriteBatch10 = this.spriteBatch;
							Texture2D texture10 = backgroundTexture[15];
							Vector2 position10 = new Vector2(bgStart + num34 * num52, bgTop);
							Rectangle? sourceRectangle10 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color17 = color3;
							float rotation12 = 0f;
							spriteBatch10.Draw(texture10, position10, sourceRectangle10, color17, rotation12, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.31f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.43;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1950.0 + 1840.0);
					if (gameMenu)
					{
						bgTop = 400;
						bgStart -= 80;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num53 = 0; num53 < bgLoops; num53++)
						{
							SpriteBatch spriteBatch11 = this.spriteBatch;
							Texture2D texture11 = backgroundTexture[16];
							Vector2 position11 = new Vector2(bgStart + num34 * num53, bgTop);
							Rectangle? sourceRectangle11 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color18 = color3;
							float rotation13 = 0f;
							spriteBatch11.Draw(texture11, position11, sourceRectangle11, color18, rotation13, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.34f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.49;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2060.0);
					if (gameMenu)
					{
						bgTop = 480;
						bgStart -= 120;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num54 = 0; num54 < bgLoops; num54++)
						{
							SpriteBatch spriteBatch12 = this.spriteBatch;
							Texture2D texture12 = backgroundTexture[17];
							Vector2 position12 = new Vector2(bgStart + num34 * num54, bgTop);
							Rectangle? sourceRectangle12 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color19 = color3;
							float rotation14 = 0f;
							spriteBatch12.Draw(texture12, position12, sourceRectangle12, color19, rotation14, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
				if (bgAlpha[num51] > 0f && num51 == 2)
				{
					num33 = 1.25f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.37;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1750.0);
					if (gameMenu)
					{
						bgTop = 320;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num55 = 0; num55 < bgLoops; num55++)
						{
							SpriteBatch spriteBatch13 = this.spriteBatch;
							Texture2D texture13 = backgroundTexture[21];
							Vector2 position13 = new Vector2(bgStart + num34 * num55, bgTop);
							Rectangle? sourceRectangle13 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[20]);
							Color color20 = color3;
							float rotation15 = 0f;
							spriteBatch13.Draw(texture13, position13, sourceRectangle13, color20, rotation15, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.34f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.49;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2150.0);
					if (gameMenu)
					{
						bgTop = 480;
						bgStart -= 120;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num56 = 0; num56 < bgLoops; num56++)
						{
							SpriteBatch spriteBatch14 = this.spriteBatch;
							Texture2D texture14 = backgroundTexture[20];
							Vector2 position14 = new Vector2(bgStart + num34 * num56, bgTop);
							Rectangle? sourceRectangle14 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[20]);
							Color color21 = color3;
							float rotation16 = 0f;
							spriteBatch14.Draw(texture14, position14, sourceRectangle14, color21, rotation16, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
				if (bgAlpha[num51] > 0f && num51 == 5)
				{
					num33 = 1.25f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.37;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1750.0);
					if (gameMenu)
					{
						bgTop = 320;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num57 = 0; num57 < bgLoops; num57++)
						{
							SpriteBatch spriteBatch15 = this.spriteBatch;
							Texture2D texture15 = backgroundTexture[26];
							Vector2 position15 = new Vector2(bgStart + num34 * num57, bgTop);
							Rectangle? sourceRectangle15 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[20]);
							Color color22 = color3;
							float rotation17 = 0f;
							spriteBatch15.Draw(texture15, position15, sourceRectangle15, color22, rotation17, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.34f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.49;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2150.0);
					if (gameMenu)
					{
						bgTop = 480;
						bgStart -= 120;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num58 = 0; num58 < bgLoops; num58++)
						{
							SpriteBatch spriteBatch16 = this.spriteBatch;
							Texture2D texture16 = backgroundTexture[27];
							Vector2 position16 = new Vector2(bgStart + num34 * num58, bgTop);
							Rectangle? sourceRectangle16 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[20]);
							Color color23 = color3;
							float rotation18 = 0f;
							spriteBatch16.Draw(texture16, position16, sourceRectangle16, color23, rotation18, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
				if (bgAlpha[num51] > 0f && num51 == 1)
				{
					num33 = 1.25f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.4;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1500.0);
					if (gameMenu)
					{
						bgTop = 320;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num59 = 0; num59 < bgLoops; num59++)
						{
							SpriteBatch spriteBatch17 = this.spriteBatch;
							Texture2D texture17 = backgroundTexture[12];
							Vector2 position17 = new Vector2(bgStart + num34 * num59, bgTop);
							Rectangle? sourceRectangle17 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color24 = color3;
							float rotation19 = 0f;
							spriteBatch17.Draw(texture17, position17, sourceRectangle17, color24, rotation19, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.31f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.43;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1950.0 + 1750.0);
					if (gameMenu)
					{
						bgTop = 400;
						bgStart -= 80;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num60 = 0; num60 < bgLoops; num60++)
						{
							SpriteBatch spriteBatch18 = this.spriteBatch;
							Texture2D texture18 = backgroundTexture[13];
							Vector2 position18 = new Vector2(bgStart + num34 * num60, bgTop);
							Rectangle? sourceRectangle18 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color25 = color3;
							float rotation20 = 0f;
							spriteBatch18.Draw(texture18, position18, sourceRectangle18, color25, rotation20, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.34f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.49;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2000.0);
					if (gameMenu)
					{
						bgTop = 480;
						bgStart -= 120;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num61 = 0; num61 < bgLoops; num61++)
						{
							SpriteBatch spriteBatch19 = this.spriteBatch;
							Texture2D texture19 = backgroundTexture[14];
							Vector2 position19 = new Vector2(bgStart + num34 * num61, bgTop);
							Rectangle? sourceRectangle19 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color26 = color3;
							float rotation21 = 0f;
							spriteBatch19.Draw(texture19, position19, sourceRectangle19, color26, rotation21, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
				if (bgAlpha[num51] > 0f && num51 == 6)
				{
					num33 = 1.25f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.4;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1500.0);
					if (gameMenu)
					{
						bgTop = 320;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num62 = 0; num62 < bgLoops; num62++)
						{
							SpriteBatch spriteBatch20 = this.spriteBatch;
							Texture2D texture20 = backgroundTexture[29];
							Vector2 position20 = new Vector2(bgStart + num34 * num62, bgTop);
							Rectangle? sourceRectangle20 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color27 = color3;
							float rotation22 = 0f;
							spriteBatch20.Draw(texture20, position20, sourceRectangle20, color27, rotation22, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.31f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.43;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1950.0 + 1750.0);
					if (gameMenu)
					{
						bgTop = 400;
						bgStart -= 80;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num63 = 0; num63 < bgLoops; num63++)
						{
							SpriteBatch spriteBatch21 = this.spriteBatch;
							Texture2D texture21 = backgroundTexture[30];
							Vector2 position21 = new Vector2(bgStart + num34 * num63, bgTop);
							Rectangle? sourceRectangle21 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color28 = color3;
							float rotation23 = 0f;
							spriteBatch21.Draw(texture21, position21, sourceRectangle21, color28, rotation23, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
					num33 = 1.34f;
					num33 *= 2f;
					num34 = (int)((float)backgroundWidth[8] * num33);
					bgParrallax = 0.49;
					bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
					bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2000.0);
					if (gameMenu)
					{
						bgTop = 480;
						bgStart -= 120;
					}
					bgLoops = screenWidth / num34 + 2;
					if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
					{
						for (int num64 = 0; num64 < bgLoops; num64++)
						{
							SpriteBatch spriteBatch22 = this.spriteBatch;
							Texture2D texture22 = backgroundTexture[31];
							Vector2 position22 = new Vector2(bgStart + num34 * num64, bgTop);
							Rectangle? sourceRectangle22 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
							Color color29 = color3;
							float rotation24 = 0f;
							spriteBatch22.Draw(texture22, position22, sourceRectangle22, color29, rotation24, default(Vector2), num33, SpriteEffects.None, 0f);
						}
					}
				}
				if (!(bgAlpha[num51] > 0f) || num51 != 0)
				{
					continue;
				}
				num33 = 1.25f;
				num33 *= 2f;
				num34 = (int)((float)backgroundWidth[8] * num33);
				bgParrallax = 0.4;
				bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
				bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1800.0 + 1500.0);
				if (gameMenu)
				{
					bgTop = 320;
				}
				bgLoops = screenWidth / num34 + 2;
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					for (int num65 = 0; num65 < bgLoops; num65++)
					{
						SpriteBatch spriteBatch23 = this.spriteBatch;
						Texture2D texture23 = backgroundTexture[9];
						Vector2 position23 = new Vector2(bgStart + num34 * num65, bgTop);
						Rectangle? sourceRectangle23 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
						Color color30 = color3;
						float rotation25 = 0f;
						spriteBatch23.Draw(texture23, position23, sourceRectangle23, color30, rotation25, default(Vector2), num33, SpriteEffects.None, 0f);
					}
				}
				num33 = 1.31f;
				num33 *= 2f;
				num34 = (int)((float)backgroundWidth[8] * num33);
				bgParrallax = 0.43;
				bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
				bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 1950.0 + 1750.0);
				if (gameMenu)
				{
					bgTop = 400;
					bgStart -= 80;
				}
				bgLoops = screenWidth / num34 + 2;
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					for (int num66 = 0; num66 < bgLoops; num66++)
					{
						SpriteBatch spriteBatch24 = this.spriteBatch;
						Texture2D texture24 = backgroundTexture[10];
						Vector2 position24 = new Vector2(bgStart + num34 * num66, bgTop);
						Rectangle? sourceRectangle24 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
						Color color31 = color3;
						float rotation26 = 0f;
						spriteBatch24.Draw(texture24, position24, sourceRectangle24, color31, rotation26, default(Vector2), num33, SpriteEffects.None, 0f);
					}
				}
				num33 = 1.34f;
				num33 *= 2f;
				num34 = (int)((float)backgroundWidth[8] * num33);
				bgParrallax = 0.49;
				bgStart = (int)(0.0 - Math.IEEERemainder((double)screenPosition.X * bgParrallax, num34) - (double)(num34 / 2));
				bgTop = (int)((0.0 - (double)screenPosition.Y + (double)(num29 / 2f)) / (worldSurface * 16.0) * 2100.0 + 2000.0);
				if (gameMenu)
				{
					bgTop = 480;
					bgStart -= 120;
				}
				bgLoops = screenWidth / num34 + 2;
				if ((double)screenPosition.Y < worldSurface * 16.0 + 16.0)
				{
					for (int num67 = 0; num67 < bgLoops; num67++)
					{
						SpriteBatch spriteBatch25 = this.spriteBatch;
						Texture2D texture25 = backgroundTexture[11];
						Vector2 position25 = new Vector2(bgStart + num34 * num67, bgTop);
						Rectangle? sourceRectangle25 = new Rectangle(0, 0, backgroundWidth[8], backgroundHeight[8]);
						Color color32 = color3;
						float rotation27 = 0f;
						spriteBatch25.Draw(texture25, position25, sourceRectangle25, color32, rotation27, default(Vector2), num33, SpriteEffects.None, 0f);
					}
				}
			}
			if (gameMenu || netMode == 2)
			{
				DrawMenu();
				if (Config.displayConsole && (Config.console.Count > 0 || Config.consoleTrack.Count > 0))
				{
					this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
					DrawConsole(this.spriteBatch);
					this.spriteBatch.End();
				}
				return;
			}
			firstTileX = (int)(screenPosition.X / 16f) - 1;
			lastTileX = (int)(screenPosition.X + (float)screenWidth) / 16 + 2;
			firstTileY = (int)(screenPosition.Y / 16f) - 1;
			lastTileY = (int)(screenPosition.Y + (float)screenHeight) / 16 + 2;
			if (firstTileX < 0)
			{
				firstTileX = 0;
			}
			if (lastTileX > maxTilesX)
			{
				lastTileX = maxTilesX;
			}
			if (firstTileY < 0)
			{
				firstTileY = 0;
			}
			if (lastTileY > maxTilesY)
			{
				lastTileY = maxTilesY;
			}
			if (!drawSkip)
			{
				TMod.RunMethod(TMod.WorldHooks.PreLightTiles, this.spriteBatch);
				Lighting.LightTiles(firstTileX, lastTileX, firstTileY, lastTileY);
				TMod.RunMethod(TMod.WorldHooks.PostLightTiles, this.spriteBatch);
			}
			if (drawToScreen)
			{
				DrawWater(bg: true);
			}
			else
			{
				this.spriteBatch.Draw(backWaterTarget, sceneBackgroundPos - screenPosition, Color.White);
			}
			float x = (sceneBackgroundPos.X - screenPosition.X + (float)offScreenRange) * caveParrallax - (float)offScreenRange;
			if (drawToScreen)
			{
				DrawBackground();
			}
			else
			{
				TMod.RunMethod(TMod.WorldHooks.PreDrawBackground, this.spriteBatch);
				this.spriteBatch.Draw(backgroundTarget, new Vector2(x, sceneBackgroundPos.Y - screenPosition.Y), Color.White);
				TMod.RunMethod(TMod.WorldHooks.PostDrawBackground, this.spriteBatch);
			}
			magmaBGFrameCounter++;
			if (magmaBGFrameCounter >= 8)
			{
				magmaBGFrameCounter = 0;
				magmaBGFrame++;
				if (magmaBGFrame >= 3)
				{
					magmaBGFrame = 0;
				}
			}
			try
			{
				if (drawToScreen)
				{
					DrawBlack();
					TMod.RunMethod(TMod.WorldHooks.PreDrawWalls, this.spriteBatch);
					DrawWalls();
					TMod.RunMethod(TMod.WorldHooks.PostDrawWalls, this.spriteBatch);
				}
				else
				{
					this.spriteBatch.Draw(blackTarget, sceneTilePos - screenPosition, Color.White);
					this.spriteBatch.Draw(wallTarget, sceneWallPos - screenPosition, Color.White);
				}
				DrawWoF();
				Codable.RunGlobalMethod("ModWorld", "PreDrawTilesEachTick", this.spriteBatch);
				if (player[myPlayer].detectCreature)
				{
					if (drawToScreen)
					{
						DrawTiles(solidOnly: false);
						DrawTiles();
					}
					else
					{
						this.spriteBatch.Draw(tile2Target, sceneTile2Pos - screenPosition, Color.White);
						this.spriteBatch.Draw(tileTarget, sceneTilePos - screenPosition, Color.White);
					}
					DrawGore();
					DrawNPCs(behindTiles: true);
					DrawNPCs();
				}
				else
				{
					if (drawToScreen)
					{
						DrawTiles(solidOnly: false);
						DrawNPCs(behindTiles: true);
						DrawTiles();
					}
					else
					{
						this.spriteBatch.Draw(tile2Target, sceneTile2Pos - screenPosition, Color.White);
						DrawNPCs(behindTiles: true);
						this.spriteBatch.Draw(tileTarget, sceneTilePos - screenPosition, Color.White);
					}
					DrawGore();
					DrawNPCs();
				}
			}
			catch
			{
			}
			for (int num68 = 0; num68 < 1000; num68++)
			{
				if (projectile[num68] == null)
				{
					projectile[num68] = new Projectile();
				}
				if (projectile[num68].active && projectile[num68].type > 0 && !projectile[num68].hide)
				{
					try
					{
						DrawProj(num68);
					}
					catch (Exception)
					{
					}
				}
			}
			for (int num69 = 0; num69 < 255; num69++)
			{
				if (player[num69] == null)
				{
					player[num69] = new Player();
				}
				if (!player[num69].active)
				{
					continue;
				}
				if (player[num69].ghost)
				{
					Vector2 position26 = player[num69].position;
					player[num69].position = player[num69].shadowPos[0];
					player[num69].shadow = 0.5f;
					DrawGhost(player[num69]);
					player[num69].position = player[num69].shadowPos[1];
					player[num69].shadow = 0.7f;
					DrawGhost(player[num69]);
					player[num69].position = player[num69].shadowPos[2];
					player[num69].shadow = 0.9f;
					DrawGhost(player[num69]);
					player[num69].position = position26;
					player[num69].shadow = 0f;
					DrawGhost(player[num69]);
					continue;
				}
				bool flag2 = player[num69].ShadowTail;
				bool flag3 = player[num69].ShadowAura;
				if (player[num69].head == 5 && player[num69].body == 5 && player[num69].legs == 5)
				{
					flag2 = true;
				}
				if (player[num69].head == 7 && player[num69].body == 7 && player[num69].legs == 7)
				{
					flag2 = true;
				}
				if (player[num69].head == 22 && player[num69].body == 14 && player[num69].legs == 14)
				{
					flag2 = true;
				}
				if (player[num69].body == 17 && player[num69].legs == 16 && (player[num69].head == 29 || player[num69].head == 30 || player[num69].head == 31))
				{
					flag2 = true;
				}
				if (player[num69].body == 19 && player[num69].legs == 18 && (player[num69].head == 35 || player[num69].head == 36 || player[num69].head == 37))
				{
					flag3 = true;
				}
				if (player[num69].body == 24 && player[num69].legs == 23 && (player[num69].head == 41 || player[num69].head == 42 || player[num69].head == 43))
				{
					flag3 = true;
					flag2 = true;
				}
				if (flag3)
				{
					Vector2 position27 = player[num69].position;
					if (!gamePaused)
					{
						player[num69].ghostFade += player[num69].ghostDir * 0.075f;
					}
					if ((double)player[num69].ghostFade < 0.1)
					{
						player[num69].ghostDir = 1f;
						player[num69].ghostFade = 0.1f;
					}
					if ((double)player[num69].ghostFade > 0.9)
					{
						player[num69].ghostDir = -1f;
						player[num69].ghostFade = 0.9f;
					}
					player[num69].position.X = position27.X - player[num69].ghostFade * 5f;
					player[num69].shadow = player[num69].ghostFade;
					DrawPlayer(player[num69]);
					player[num69].position.X = position27.X + player[num69].ghostFade * 5f;
					player[num69].shadow = player[num69].ghostFade;
					DrawPlayer(player[num69]);
					player[num69].position = position27;
					player[num69].position.Y = position27.Y - player[num69].ghostFade * 5f;
					player[num69].shadow = player[num69].ghostFade;
					DrawPlayer(player[num69]);
					player[num69].position.Y = position27.Y + player[num69].ghostFade * 5f;
					player[num69].shadow = player[num69].ghostFade;
					DrawPlayer(player[num69]);
					player[num69].position = position27;
					player[num69].shadow = 0f;
				}
				if (flag2)
				{
					Vector2 position28 = player[num69].position;
					player[num69].position = player[num69].shadowPos[0];
					player[num69].shadow = 0.5f;
					DrawPlayer(player[num69]);
					player[num69].position = player[num69].shadowPos[1];
					player[num69].shadow = 0.7f;
					DrawPlayer(player[num69]);
					player[num69].position = player[num69].shadowPos[2];
					player[num69].shadow = 0.9f;
					DrawPlayer(player[num69]);
					player[num69].position = position28;
					player[num69].shadow = 0f;
				}
				DrawPlayer(player[num69]);
			}
			if (!gamePaused)
			{
				essScale += (float)essDir * 0.01f;
				if (essScale > 1f)
				{
					essDir = -1;
					essScale = 1f;
				}
				if ((double)essScale < 0.7)
				{
					essDir = 1;
					essScale = 0.7f;
				}
			}
			for (int num70 = 0; num70 < 200; num70++)
			{
				if (item[num70] == null)
				{
					item[num70] = new Item();
				}
				if (!item[num70].active || item[num70].type <= 0)
				{
					continue;
				}
				int type = item[num70].type;
				int value2 = type;
				if (item[num70].name != null && Config.itemDefs.drawPretendType.TryGetValue(item[num70].name, out value2))
				{
					item[num70].type = value2;
				}
				_ = (int)((double)item[num70].position.X + (double)item[num70].width * 0.5) / 16;
				_ = Lighting.offScreenTiles;
				_ = (int)((double)item[num70].position.Y + (double)item[num70].height * 0.5) / 16;
				_ = Lighting.offScreenTiles;
				Color color33 = Lighting.GetColor((int)((double)item[num70].position.X + (double)item[num70].width * 0.5) / 16, (int)((double)item[num70].position.Y + (double)item[num70].height * 0.5) / 16);
				if (!gamePaused && base.IsActive && ((item[num70].type >= 71 && item[num70].type <= 74) || item[num70].type == 58 || item[num70].type == 109) && color33.R > 60)
				{
					float num71 = (float)rand.Next(500) - (Math.Abs(item[num70].velocity.X) + Math.Abs(item[num70].velocity.Y)) * 10f;
					if (num71 < (float)((int)color33.R / 50))
					{
						Vector2 position29 = item[num70].position;
						int width = item[num70].width;
						int height = item[num70].height;
						int type2 = 43;
						float speedX = 0f;
						float speedY = 0f;
						int alpha = 254;
						int num72 = Dust.NewDust(position29, width, height, type2, speedX, speedY, alpha, default(Color), 0.5f);
						Dust dust = Main.dust[num72];
						dust.velocity *= 0f;
					}
				}
				float rotation28 = item[num70].velocity.X * 0.2f;
				float Scale = 1f;
				Color DrawAlpha = item[num70].GetAlpha(color33);
				if (item[num70].type == 520 || item[num70].type == 521 || item[num70].type == 547 || item[num70].type == 548 || item[num70].type == 549)
				{
					Scale = essScale;
					DrawAlpha.R = (byte)((float)(int)DrawAlpha.R * Scale);
					DrawAlpha.G = (byte)((float)(int)DrawAlpha.G * Scale);
					DrawAlpha.B = (byte)((float)(int)DrawAlpha.B * Scale);
					DrawAlpha.A = (byte)((float)(int)DrawAlpha.A * Scale);
				}
				else if (item[num70].type == 58 || item[num70].type == 184)
				{
					Scale = essScale * 0.25f + 0.75f;
					DrawAlpha.R = (byte)((float)(int)DrawAlpha.R * Scale);
					DrawAlpha.G = (byte)((float)(int)DrawAlpha.G * Scale);
					DrawAlpha.B = (byte)((float)(int)DrawAlpha.B * Scale);
					DrawAlpha.A = (byte)((float)(int)DrawAlpha.A * Scale);
				}
				item[num70].PreDrawItem(this.spriteBatch, item[num70], ref DrawAlpha, ref Scale);
				float num73 = item[num70].height - itemTexture[item[num70].type].Height;
				float num74 = item[num70].width / 2 - itemTexture[item[num70].type].Width / 2;
				this.spriteBatch.Draw(itemTexture[item[num70].type], new Vector2(item[num70].position.X - screenPosition.X + (float)(itemTexture[item[num70].type].Width / 2) + num74, item[num70].position.Y - screenPosition.Y + (float)(itemTexture[item[num70].type].Height / 2) + num73 + 2f), new Rectangle(0, 0, itemTexture[item[num70].type].Width, itemTexture[item[num70].type].Height), DrawAlpha, rotation28, new Vector2(itemTexture[item[num70].type].Width / 2, itemTexture[item[num70].type].Height / 2), Scale, SpriteEffects.None, 0f);
				Color color34 = item[num70].color;
				if (color34 != default(Color))
				{
					this.spriteBatch.Draw(itemTexture[item[num70].type], new Vector2(item[num70].position.X - screenPosition.X + (float)(itemTexture[item[num70].type].Width / 2) + num74, item[num70].position.Y - screenPosition.Y + (float)(itemTexture[item[num70].type].Height / 2) + num73 + 2f), new Rectangle(0, 0, itemTexture[item[num70].type].Width, itemTexture[item[num70].type].Height), item[num70].GetColor(color33), rotation28, new Vector2(itemTexture[item[num70].type].Width / 2, itemTexture[item[num70].type].Height / 2), Scale, SpriteEffects.None, 0f);
				}
				item[num70].type = type;
			}
			Rectangle value3 = new Rectangle((int)screenPosition.X - 500, (int)screenPosition.Y - 50, screenWidth + 1000, screenHeight + 100);
			for (int num75 = 0; num75 < numDust; num75++)
			{
				Dust dust2 = Main.dust[num75];
				if (!dust2.active)
				{
					continue;
				}
				if (new Rectangle((int)dust2.position.X, (int)dust2.position.Y, 4, 4).Intersects(value3))
				{
					Color newColor = Lighting.GetColor((int)((double)dust2.position.X + 4.0) / 16, (int)((double)dust2.position.Y + 4.0) / 16);
					if (dust2.type == 6 || dust2.type == 15 || dust2.noLight || (dust2.type >= 59 && dust2.type <= 64))
					{
						newColor = Color.White;
					}
					newColor = dust2.GetAlpha(newColor);
					if (dust2.OverrideTexture != null)
					{
						this.spriteBatch.Draw(dust2.OverrideTexture, dust2.position - screenPosition, dust2.frame, newColor, dust2.rotation, new Vector2(dust2.frame.Width, dust2.frame.Height) / 2f, dust2.scale, SpriteEffects.None, 0f);
					}
					else
					{
						this.spriteBatch.Draw(dustTexture, dust2.position - screenPosition, dust2.frame, newColor, dust2.rotation, new Vector2(4f, 4f), dust2.scale, SpriteEffects.None, 0f);
					}
					Color color35 = dust2.color;
					if (color35 != default(Color))
					{
						if (dust2.OverrideTexture != null)
						{
							this.spriteBatch.Draw(dust2.OverrideTexture, dust2.position - screenPosition, dust2.frame, dust2.GetColor(newColor), dust2.rotation, new Vector2(dust2.frame.Width, dust2.frame.Height) / 2f, dust2.scale, SpriteEffects.None, 0f);
						}
						else
						{
							this.spriteBatch.Draw(dustTexture, dust2.position - screenPosition, dust2.frame, dust2.GetColor(newColor), dust2.rotation, new Vector2(4f, 4f), dust2.scale, SpriteEffects.None, 0f);
						}
					}
					if (newColor == Color.Black)
					{
						dust2.active = false;
					}
				}
				else
				{
					dust2.active = false;
				}
			}
			if (drawToScreen)
			{
				DrawWater();
				if (player[myPlayer].inventory[player[myPlayer].selectedItem].mech)
				{
					DrawWires();
				}
			}
			else
			{
				this.spriteBatch.Draw(waterTarget, sceneWaterPos - screenPosition, Color.White);
			}
			OnScreenInterface.DrawOnScreen(this.spriteBatch);
			TMod.RunMethod(TMod.WorldHooks.PreDrawInterface, this.spriteBatch);
			base.GraphicsDevice.SetRenderTarget(null);
			if (!hideUI)
			{
				Vector2 vector2 = default(Vector2);
				for (int num76 = 0; num76 < 255; num76++)
				{
					if (!player[num76].active || player[num76].chatShowTime <= 0 || num76 == myPlayer || player[num76].dead)
					{
						continue;
					}
					Vector2 vector = fontMouseText.MeasureString(player[num76].chatText);
					vector2.X = player[num76].position.X + (float)(player[num76].width / 2) - vector.X / 2f;
					vector2.Y = player[num76].position.Y - vector.Y - 2f;
					for (int num77 = 0; num77 < 5; num77++)
					{
						int num78 = 0;
						int num79 = 0;
						Color color36 = Color.Black;
						if (num77 == 0)
						{
							num78 = -2;
						}
						if (num77 == 1)
						{
							num78 = 2;
						}
						if (num77 == 2)
						{
							num79 = -2;
						}
						if (num77 == 3)
						{
							num79 = 2;
						}
						if (num77 == 4)
						{
							color36 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
						}
						SpriteBatch spriteBatch26 = this.spriteBatch;
						SpriteFont spriteFont = fontMouseText;
						string text = player[num76].chatText;
						Vector2 position30 = new Vector2(vector2.X + (float)num78 - screenPosition.X, vector2.Y + (float)num79 - screenPosition.Y);
						Color color37 = color36;
						float rotation29 = 0f;
						spriteBatch26.DrawString(spriteFont, text, position30, color37, rotation29, default(Vector2), 1f, SpriteEffects.None, 0f);
					}
				}
				for (int num80 = 0; num80 < 100; num80++)
				{
					if (!combatText[num80].active)
					{
						continue;
					}
					int num81 = 0;
					if (combatText[num80].crit)
					{
						num81 = 1;
					}
					Vector2 vector3 = fontCombatText[num81].MeasureString(combatText[num80].text);
					Vector2 origin = new Vector2(vector3.X * 0.5f, vector3.Y * 0.5f);
					_ = combatText[num80].scale;
					float num82 = (int)combatText[num80].color.R;
					float num83 = (int)combatText[num80].color.G;
					float num84 = (int)combatText[num80].color.B;
					float num85 = (int)combatText[num80].color.A;
					num82 *= combatText[num80].scale * combatText[num80].alpha * 0.3f;
					num84 *= combatText[num80].scale * combatText[num80].alpha * 0.3f;
					num83 *= combatText[num80].scale * combatText[num80].alpha * 0.3f;
					num85 *= combatText[num80].scale * combatText[num80].alpha;
					Color color38 = new Color((int)num82, (int)num83, (int)num84, (int)num85);
					for (int num86 = 0; num86 < 5; num86++)
					{
						int num87 = 0;
						int num88 = 0;
						switch (num86)
						{
						case 0:
							num87--;
							break;
						case 1:
							num87++;
							break;
						case 2:
							num88--;
							break;
						case 3:
							num88++;
							break;
						default:
							num82 = (float)(int)combatText[num80].color.R * combatText[num80].scale * combatText[num80].alpha;
							num84 = (float)(int)combatText[num80].color.B * combatText[num80].scale * combatText[num80].alpha;
							num83 = (float)(int)combatText[num80].color.G * combatText[num80].scale * combatText[num80].alpha;
							num85 = (float)(int)combatText[num80].color.A * combatText[num80].scale * combatText[num80].alpha;
							color38 = new Color((int)num82, (int)num83, (int)num84, (int)num85);
							break;
						}
						this.spriteBatch.DrawString(fontCombatText[num81], combatText[num80].text, new Vector2(combatText[num80].position.X - screenPosition.X + (float)num87 + origin.X, combatText[num80].position.Y - screenPosition.Y + (float)num88 + origin.Y), color38, combatText[num80].rotation, origin, combatText[num80].scale, SpriteEffects.None, 0f);
					}
				}
				for (int num89 = 0; num89 < 20; num89++)
				{
					if (!itemText[num89].active)
					{
						continue;
					}
					string text2 = itemText[num89].name;
					if (itemText[num89].stack > 1)
					{
						text2 = text2 + " (" + itemText[num89].stack + ")";
					}
					Vector2 vector4 = fontMouseText.MeasureString(text2);
					Vector2 origin2 = new Vector2(vector4.X * 0.5f, vector4.Y * 0.5f);
					_ = itemText[num89].scale;
					float num90 = (int)itemText[num89].color.R;
					float num91 = (int)itemText[num89].color.G;
					float num92 = (int)itemText[num89].color.B;
					float num93 = (int)itemText[num89].color.A;
					num90 *= itemText[num89].scale * itemText[num89].alpha * 0.3f;
					num92 *= itemText[num89].scale * itemText[num89].alpha * 0.3f;
					num91 *= itemText[num89].scale * itemText[num89].alpha * 0.3f;
					num93 *= itemText[num89].scale * itemText[num89].alpha;
					Color color39 = new Color((int)num90, (int)num91, (int)num92, (int)num93);
					for (int num94 = 0; num94 < 5; num94++)
					{
						int num95 = 0;
						int num96 = 0;
						switch (num94)
						{
						case 0:
							num95 -= 2;
							break;
						case 1:
							num95 += 2;
							break;
						case 2:
							num96 -= 2;
							break;
						case 3:
							num96 += 2;
							break;
						default:
							num90 = (float)(int)itemText[num89].color.R * itemText[num89].scale * itemText[num89].alpha;
							num92 = (float)(int)itemText[num89].color.B * itemText[num89].scale * itemText[num89].alpha;
							num91 = (float)(int)itemText[num89].color.G * itemText[num89].scale * itemText[num89].alpha;
							num93 = (float)(int)itemText[num89].color.A * itemText[num89].scale * itemText[num89].alpha;
							color39 = new Color((int)num90, (int)num91, (int)num92, (int)num93);
							break;
						}
						if (num94 < 4)
						{
							num93 = (float)(int)itemText[num89].color.A * itemText[num89].scale * itemText[num89].alpha;
							color39 = new Color(0, 0, 0, (int)num93);
						}
						this.spriteBatch.DrawString(fontMouseText, text2, new Vector2(itemText[num89].position.X - screenPosition.X + (float)num95 + origin2.X, itemText[num89].position.Y - screenPosition.Y + (float)num96 + origin2.Y), color39, itemText[num89].rotation, origin2, itemText[num89].scale, SpriteEffects.None, 0f);
					}
				}
				if (netMode == 1 && Netplay.clientSock.statusText != "" && Netplay.clientSock.statusText != null)
				{
					string text3 = Netplay.clientSock.statusText + ": " + (int)((float)Netplay.clientSock.statusCount / (float)Netplay.clientSock.statusMax * 100f) + "%";
					SpriteBatch spriteBatch27 = this.spriteBatch;
					SpriteFont spriteFont2 = fontMouseText;
					string text4 = text3;
					Vector2 position31 = new Vector2(628f - fontMouseText.MeasureString(text3).X * 0.5f + (float)(screenWidth - 800), 84f);
					Color color40 = new Color(mouseTextColor, mouseTextColor, mouseTextColor, mouseTextColor);
					float rotation30 = 0f;
					spriteBatch27.DrawString(spriteFont2, text4, position31, color40, rotation30, default(Vector2), 1f, SpriteEffects.None, 0f);
				}
				DrawFPS();
				DrawInterface();
			}
			else
			{
				maxQ = true;
			}
			TMod.RunMethod(TMod.WorldHooks.PostDraw, this.spriteBatch);
			if (Config.displayConsole && (Config.console.Count > 0 || Config.consoleTrack.Count > 0))
			{
				this.spriteBatch.End();
				this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
				DrawConsole(this.spriteBatch);
			}
			this.spriteBatch.End();
			if (mouseLeft)
			{
				mouseLeftRelease = false;
			}
			else
			{
				mouseLeftRelease = true;
			}
			if (mouseRight)
			{
				mouseRightRelease = false;
			}
			else
			{
				mouseRightRelease = true;
			}
			if (mouseState.RightButton != ButtonState.Pressed)
			{
				stackSplit = 0;
			}
			if (stackSplit > 0)
			{
				stackSplit--;
			}
			if (renderCount < 10)
			{
				drawTimer[renderCount] = stopwatch.ElapsedMilliseconds;
				if (drawTimerMaxDelay[renderCount] > 0f)
				{
					drawTimerMaxDelay[renderCount] -= 1f;
				}
				else
				{
					drawTimerMax[renderCount] = 0f;
				}
				if (drawTimer[renderCount] > drawTimerMax[renderCount])
				{
					drawTimerMax[renderCount] = drawTimer[renderCount];
					drawTimerMaxDelay[renderCount] = 100f;
				}
			}
		}

		public static void DrawConsole(SpriteBatch sb)
		{
			for (int i = 0; i < Config.consoleTrack.Count; i++)
			{
				if (Config.consoleTrack[i].ticks <= 0)
				{
					Config.consoleTrack.RemoveAt(i--);
				}
			}
			int num = Math.Min(Math.Min((screenHeight - 4) / 18, Config.console.Count), Config.displayConsoleLines);
			int num2 = Math.Min((screenHeight - 4) / 18, Config.consoleTrack.Count);
			int num3 = Math.Max(num, num2);
			sb.Draw(blackTileTexture, new Rectangle(0, 0, screenWidth, 4 + 18 * num3), new Color(0f, 0f, 0f, 0.5f));
			for (int j = 0; j < num; j++)
			{
				int index = Config.console.Count - num + j;
				sb.DrawString(fontMouseText, Config.console[index], new Vector2(2f, 2 + j * 18), Config.consoleColor[index]);
			}
			for (int k = 0; k < num2; k++)
			{
				int index2 = Config.consoleTrack.Count - num2 + k;
				TrackConsoleLine trackConsoleLine = Config.consoleTrack[index2];
				sb.DrawString(fontMouseText, trackConsoleLine.print, new Vector2((float)(screenWidth - 2) - fontMouseText.MeasureString(trackConsoleLine.print).X, 2 + k * 18), trackConsoleLine.color);
				trackConsoleLine.ticks--;
			}
		}

		private static void UpdateInvasion()
		{
			if (invasionType <= 0)
			{
				return;
			}
			if (invasionSize <= 0)
			{
				if (invasionType == 1)
				{
					NPC.downedGoblins = true;
					if (netMode == 2)
					{
						NetMessage.SendData(7);
					}
				}
				else if (invasionType == 2)
				{
					NPC.downedFrost = true;
				}
				InvasionWarning();
				invasionType = 0;
				invasionDelay = 7;
			}
			if (invasionX == (double)spawnTileX)
			{
				return;
			}
			float num = 1f;
			if (invasionX > (double)spawnTileX)
			{
				invasionX -= num;
				if (invasionX <= (double)spawnTileX)
				{
					invasionX = spawnTileX;
					InvasionWarning();
				}
				else
				{
					invasionWarn--;
				}
			}
			else if (invasionX < (double)spawnTileX)
			{
				invasionX += num;
				if (invasionX >= (double)spawnTileX)
				{
					invasionX = spawnTileX;
					InvasionWarning();
				}
				else
				{
					invasionWarn--;
				}
			}
			if (invasionWarn <= 0)
			{
				invasionWarn = 3600;
				InvasionWarning();
			}
		}

		private static void InvasionWarning()
		{
			string text = "";
			text = ((invasionSize <= 0) ? ((invasionType != 2) ? (text = Lang.misc[0]) : Lang.misc[4]) : ((invasionX < (double)spawnTileX) ? ((invasionType != 2) ? (text = Lang.misc[1]) : Lang.misc[5]) : ((invasionX > (double)spawnTileX) ? ((invasionType != 2) ? (text = Lang.misc[2]) : Lang.misc[6]) : ((invasionType != 2) ? (text = Lang.misc[3]) : Lang.misc[7]))));
			if (netMode == 0)
			{
				NewText(text, 175, 75);
			}
			else if (netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, text, 255, 175f, 75f, 255f);
			}
		}

		public static void StartInvasion(int type = 1)
		{
			if (invasionType != 0 || invasionDelay != 0)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 255; i++)
			{
				if (player[i].active && player[i].statLifeMax2 >= 200)
				{
					num++;
				}
			}
			if (num > 0)
			{
				invasionType = type;
				invasionSize = 80 + 40 * num;
				invasionWarn = 0;
				if (rand.Next(2) == 0)
				{
					invasionX = 0.0;
				}
				else
				{
					invasionX = maxTilesX;
				}
			}
		}

		private static void UpdateClient()
		{
			if (myPlayer == 255)
			{
				Netplay.disconnect = true;
			}
			netPlayCounter++;
			if (netPlayCounter > 3600)
			{
				netPlayCounter = 0;
			}
			if (Math.IEEERemainder(netPlayCounter, 300.0) == 0.0)
			{
				NetMessage.SendData(13, -1, -1, "", myPlayer);
				NetMessage.SendData(36, -1, -1, "", myPlayer);
			}
			if (Math.IEEERemainder(netPlayCounter, 600.0) == 0.0)
			{
				NetMessage.SendData(16, -1, -1, "", myPlayer);
				NetMessage.SendData(40, -1, -1, "", myPlayer);
			}
			if (Netplay.clientSock.active)
			{
				Netplay.clientSock.timeOut++;
				if (!stopTimeOuts && Netplay.clientSock.timeOut > 60 * timeOut)
				{
					statusText = Lang.inter[43];
					Netplay.disconnect = true;
				}
			}
			for (int i = 0; i < 200; i++)
			{
				if (item[i].active && item[i].owner == myPlayer)
				{
					item[i].FindOwner(i);
				}
			}
		}

		private static void UpdateServer()
		{
			netPlayCounter++;
			if (netPlayCounter > 3600)
			{
				NetMessage.SendData(7);
				NetMessage.syncPlayers();
				netPlayCounter = 0;
			}
			for (int i = 0; i < maxNetPlayers; i++)
			{
				if (player[i].active && Netplay.serverSock[i].active)
				{
					Netplay.serverSock[i].SpamUpdate();
				}
			}
			if (Math.IEEERemainder(netPlayCounter, 900.0) == 0.0)
			{
				bool flag = true;
				int num = lastItemUpdate;
				int num2 = 0;
				while (flag)
				{
					num++;
					if (num >= 200)
					{
						num = 0;
					}
					num2++;
					if (!item[num].active || item[num].owner == 255)
					{
						NetMessage.SendData(21, -1, -1, "", num);
					}
					if (num2 >= maxItemUpdates || num == lastItemUpdate)
					{
						flag = false;
					}
				}
				lastItemUpdate = num;
			}
			for (int j = 0; j < 200; j++)
			{
				if (item[j].active && (item[j].owner == 255 || !player[item[j].owner].active))
				{
					item[j].FindOwner(j);
				}
			}
			for (int k = 0; k < 255; k++)
			{
				if (Netplay.serverSock[k].active)
				{
					Netplay.serverSock[k].timeOut++;
					if (!stopTimeOuts && Netplay.serverSock[k].timeOut > 60 * timeOut)
					{
						Netplay.serverSock[k].kill = true;
					}
				}
				if (!player[k].active)
				{
					continue;
				}
				int sectionX = Netplay.GetSectionX((int)(player[k].position.X / 16f));
				int sectionY = Netplay.GetSectionY((int)(player[k].position.Y / 16f));
				int num3 = 0;
				for (int l = sectionX - 1; l < sectionX + 2; l++)
				{
					for (int m = sectionY - 1; m < sectionY + 2; m++)
					{
						if (l >= 0 && l < maxSectionsX && m >= 0 && m < maxSectionsY && !Netplay.serverSock[k].tileSection[l, m])
						{
							num3++;
						}
					}
				}
				if (num3 <= 0)
				{
					continue;
				}
				int num4 = num3 * 150;
				NetMessage.SendData(9, k, -1, "Receiving tile data", num4);
				Netplay.serverSock[k].statusText2 = "is receiving tile data";
				Netplay.serverSock[k].statusMax += num4;
				for (int n = sectionX - 1; n < sectionX + 2; n++)
				{
					for (int num5 = sectionY - 1; num5 < sectionY + 2; num5++)
					{
						if (n >= 0 && n < maxSectionsX && num5 >= 0 && num5 < maxSectionsY && !Netplay.serverSock[k].tileSection[n, num5])
						{
							NetMessage.SendSection(k, n, num5);
							NetMessage.SendData(11, k, -1, "", n, num5, n, num5);
						}
					}
				}
			}
		}

		public static void NewText(string newText, byte R = byte.MaxValue, byte G = byte.MaxValue, byte B = byte.MaxValue)
		{
			for (int num = numChatLines - 1; num > 0; num--)
			{
				chatLine[num].text = chatLine[num - 1].text;
				chatLine[num].showTime = chatLine[num - 1].showTime;
				chatLine[num].color = chatLine[num - 1].color;
			}
			if (R == 0 && G == 0 && B == 0)
			{
				chatLine[0].color = Color.White;
			}
			else
			{
				chatLine[0].color = new Color(R, G, B);
			}
			chatLine[0].text = newText;
			chatLine[0].showTime = chatLength;
			PlaySound(12);
		}

		private static void UpdateTime()
		{
			time += dayRate;
			if (!dayTime)
			{
				if (WorldGen.spawnEye && netMode != 1 && time > 4860.0)
				{
					for (int i = 0; i < 255; i++)
					{
						if (player[i].active && !player[i].dead && (double)player[i].position.Y < worldSurface * 16.0)
						{
							NPC.SpawnOnPlayer(i, 4);
							WorldGen.spawnEye = false;
							break;
						}
					}
				}
				if (time > 32400.0)
				{
					checkXMas();
					if (invasionDelay > 0)
					{
						invasionDelay--;
					}
					WorldGen.spawnNPC = 0;
					checkForSpawns = 0;
					time = 0.0;
					bloodMoon = false;
					dayTime = true;
					moonPhase++;
					if (moonPhase >= 8)
					{
						moonPhase = 0;
					}
					if (netMode == 2)
					{
						NetMessage.SendData(7);
						WorldGen.saveAndPlay();
					}
					if (netMode != 1 && WorldGen.shadowOrbSmashed)
					{
						if (!NPC.downedGoblins)
						{
							if (rand.Next(3) == 0)
							{
								StartInvasion();
							}
						}
						else if (rand.Next(15) == 0)
						{
							StartInvasion();
						}
					}
				}
				if (time > 16200.0 && WorldGen.spawnMeteor)
				{
					WorldGen.spawnMeteor = false;
					WorldGen.dropMeteor();
				}
				return;
			}
			bloodMoon = false;
			if (time > 54000.0)
			{
				WorldGen.spawnNPC = 0;
				checkForSpawns = 0;
				if (rand.Next(50) == 0 && netMode != 1 && WorldGen.shadowOrbSmashed)
				{
					WorldGen.spawnMeteor = true;
				}
				if (!NPC.downedBoss1 && netMode != 1)
				{
					bool flag = false;
					for (int j = 0; j < 255; j++)
					{
						if (player[j].active && player[j].statLifeMax2 >= 200 && player[j].statDefense > 10)
						{
							flag = true;
							break;
						}
					}
					if (flag && rand.Next(3) == 0)
					{
						int num = 0;
						for (int k = 0; k < 200; k++)
						{
							if (npc[k].active && npc[k].townNPC)
							{
								num++;
							}
						}
						if (num >= 4)
						{
							WorldGen.spawnEye = true;
							if (netMode == 0)
							{
								NewText(Lang.misc[9], 50, byte.MaxValue, 130);
							}
							else if (netMode == 2)
							{
								NetMessage.SendData(25, -1, -1, Lang.misc[9], 255, 50f, 255f, 130f);
							}
						}
					}
				}
				if (!WorldGen.spawnEye && moonPhase != 4 && rand.Next(9) == 0 && netMode != 1)
				{
					for (int l = 0; l < 255; l++)
					{
						if (player[l].active && player[l].statLifeMax2 > 120)
						{
							bloodMoon = true;
							break;
						}
					}
					if (bloodMoon)
					{
						if (netMode == 0)
						{
							NewText(Lang.misc[8], 50, byte.MaxValue, 130);
						}
						else if (netMode == 2)
						{
							NetMessage.SendData(25, -1, -1, Lang.misc[8], 255, 50f, 255f, 130f);
						}
					}
				}
				time = 0.0;
				dayTime = false;
				if (netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
			if (netMode == 1)
			{
				return;
			}
			checkForSpawns++;
			if (checkForSpawns < 7200)
			{
				return;
			}
			int num2 = 0;
			for (int m = 0; m < 255; m++)
			{
				if (player[m].active)
				{
					num2++;
				}
			}
			checkForSpawns = 0;
			WorldGen.spawnNPC = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			int num13 = 0;
			int num14 = 0;
			int num15 = 0;
			DictionaryHandler<int, int> dictionaryHandler = new DictionaryHandler<int, int>();
			for (int n = 0; n < 200; n++)
			{
				if (npc[n].active && npc[n].townNPC)
				{
					if (npc[n].type != 37 && !npc[n].homeless)
					{
						WorldGen.QuickFindHome(n);
					}
					if (npc[n].townNPC && Config.npcDefs.assemblyByType[npc[n].type] != null)
					{
						dictionaryHandler[npc[n].type]++;
					}
					if (npc[n].type == 37)
					{
						num8++;
					}
					if (npc[n].type == 17)
					{
						num3++;
					}
					if (npc[n].type == 18)
					{
						num4++;
					}
					if (npc[n].type == 19)
					{
						num6++;
					}
					if (npc[n].type == 20)
					{
						num5++;
					}
					if (npc[n].type == 22)
					{
						num7++;
					}
					if (npc[n].type == 38)
					{
						num9++;
					}
					if (npc[n].type == 54)
					{
						num10++;
					}
					if (npc[n].type == 107)
					{
						num12++;
					}
					if (npc[n].type == 108)
					{
						num11++;
					}
					if (npc[n].type == 124)
					{
						num13++;
					}
					if (npc[n].type == 142)
					{
						num14++;
					}
					num15++;
				}
			}
			if (WorldGen.spawnNPC != 0)
			{
				return;
			}
			int num16 = 0;
			bool flag2 = false;
			int num17 = 0;
			bool flag3 = false;
			bool flag4 = false;
			for (int num18 = 0; num18 < 255; num18++)
			{
				if (!player[num18].active)
				{
					continue;
				}
				for (int num19 = 0; num19 < 48; num19++)
				{
					if ((player[num18].inventory[num19] != null) & (player[num18].inventory[num19].stack > 0))
					{
						if (player[num18].inventory[num19].type == 71)
						{
							num16 += player[num18].inventory[num19].stack;
						}
						if (player[num18].inventory[num19].type == 72)
						{
							num16 += player[num18].inventory[num19].stack * 100;
						}
						if (player[num18].inventory[num19].type == 73)
						{
							num16 += player[num18].inventory[num19].stack * 10000;
						}
						if (player[num18].inventory[num19].type == 74)
						{
							num16 += player[num18].inventory[num19].stack * 1000000;
						}
						if (player[num18].inventory[num19].ammo == 14 || player[num18].inventory[num19].useAmmo == 14)
						{
							flag3 = true;
						}
						if (player[num18].inventory[num19].type == 166 || player[num18].inventory[num19].type == 167 || player[num18].inventory[num19].type == 168 || player[num18].inventory[num19].type == 235)
						{
							flag4 = true;
						}
					}
				}
				int num20 = player[num18].statLifeMax2 / 20;
				if (num20 > 5)
				{
					flag2 = true;
				}
				num17 += num20;
			}
			if (!NPC.downedBoss3 && num8 == 0)
			{
				int num21 = NPC.NewNPC(dungeonX * 16 + 8, dungeonY * 16, 37);
				npc[num21].homeless = false;
				npc[num21].homeTileX = dungeonX;
				npc[num21].homeTileY = dungeonY;
			}
			if (WorldGen.spawnNPC == 0 && num7 < 1)
			{
				WorldGen.spawnNPC = 22;
			}
			if (WorldGen.spawnNPC == 0 && (double)num16 > 5000.0 && num3 < 1)
			{
				WorldGen.spawnNPC = 17;
			}
			if (WorldGen.spawnNPC == 0 && flag2 && num4 < 1)
			{
				WorldGen.spawnNPC = 18;
			}
			if (WorldGen.spawnNPC == 0 && flag3 && num6 < 1)
			{
				WorldGen.spawnNPC = 19;
			}
			if (WorldGen.spawnNPC == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3) && num5 < 1)
			{
				WorldGen.spawnNPC = 20;
			}
			if (WorldGen.spawnNPC == 0 && flag4 && num3 > 0 && num9 < 1)
			{
				WorldGen.spawnNPC = 38;
			}
			if (WorldGen.spawnNPC == 0 && NPC.downedBoss3 && num10 < 1)
			{
				WorldGen.spawnNPC = 54;
			}
			if (WorldGen.spawnNPC == 0 && NPC.savedGoblin && num12 < 1)
			{
				WorldGen.spawnNPC = 107;
			}
			if (WorldGen.spawnNPC == 0 && NPC.savedWizard && num11 < 1)
			{
				WorldGen.spawnNPC = 108;
			}
			if (WorldGen.spawnNPC == 0 && NPC.savedMech && num13 < 1)
			{
				WorldGen.spawnNPC = 124;
			}
			if (WorldGen.spawnNPC == 0 && NPC.downedFrost && num14 < 1 && xMas)
			{
				WorldGen.spawnNPC = 142;
			}
			if (WorldGen.spawnNPC != 0)
			{
				return;
			}
			int num22 = 147;
			object code;
			while (true)
			{
				if (num22 >= Config.npcDefs.GetSize())
				{
					return;
				}
				if (Config.npcDefs.assemblyByType[num22] != null)
				{
					Assembly assembly = Config.npcDefs.assemblyByType[num22];
					code = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Config.npcDefs[num22].name) + "NPC");
					if (dictionaryHandler[num22] == 0 && Codable.RunSpecifiedMethod("NPC " + Config.npcDefs[num22].name, code, "TownSpawn") && (bool)Codable.customMethodReturn)
					{
						break;
					}
				}
				num22++;
			}
			WorldGen.spawnNPC = num22;
			if (Codable.RunSpecifiedMethod("NPC " + Config.npcDefs[num22].name, code, "SetName"))
			{
				chrName[num22] = (string)Codable.customMethodReturn;
			}
			else
			{
				chrName[num22] = Config.npcDefs[num22].name;
			}
		}

		public static int DamageVar(float dmg)
		{
			float num = dmg * (1f + (float)rand.Next(-15, 16) * 0.01f);
			return (int)Math.Round(num);
		}

		public static double CalculateDamage(int Damage, int Defense)
		{
			double num = (double)Damage - (double)Defense * 0.5;
			if (num < 1.0)
			{
				num = 1.0;
			}
			return num;
		}

		public static void PlaySound(int type, int x = -1, int y = -1, int Style = 1)
		{
			int num = Style;
			try
			{
				bool flag;
				float num3;
				float num2;
				if (!dedServ && soundVolume != 0f)
				{
					flag = false;
					num2 = 1f;
					num3 = 0f;
					if (x == -1 || y == -1)
					{
						flag = true;
						goto IL_0142;
					}
					if (!WorldGen.gen && netMode != 2)
					{
						Rectangle value = new Rectangle((int)(screenPosition.X - (float)(screenWidth * 2)), (int)(screenPosition.Y - (float)(screenHeight * 2)), screenWidth * 5, screenHeight * 5);
						Rectangle rectangle = new Rectangle(x, y, 1, 1);
						Vector2 vector = new Vector2(screenPosition.X + (float)screenWidth * 0.5f, screenPosition.Y + (float)screenHeight * 0.5f);
						if (rectangle.Intersects(value))
						{
							flag = true;
						}
						if (flag)
						{
							num3 = ((float)x - vector.X) / ((float)screenWidth * 0.5f);
							float num4 = Math.Abs((float)x - vector.X);
							float num5 = Math.Abs((float)y - vector.Y);
							float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
							num2 = 1f - num6 / ((float)screenWidth * 1.5f);
						}
						goto IL_0142;
					}
				}
				goto end_IL_0002;
				IL_0142:
				if (num3 < -1f)
				{
					num3 = -1f;
				}
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				if (num2 > 0f && flag)
				{
					num2 *= soundVolume;
					switch (type)
					{
					case 0:
					{
						int num7 = rand.Next(3);
						soundInstanceDig[num7].Stop();
						soundInstanceDig[num7] = soundDig[num7].CreateInstance();
						soundInstanceDig[num7].Volume = num2;
						soundInstanceDig[num7].Pan = num3;
						soundInstanceDig[num7].Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceDig[num7].Play();
						break;
					}
					case 1:
					{
						int num8 = rand.Next(3);
						soundInstancePlayerHit[num8].Stop();
						soundInstancePlayerHit[num8] = soundPlayerHit[num8].CreateInstance();
						soundInstancePlayerHit[num8].Volume = num2;
						soundInstancePlayerHit[num8].Pan = num3;
						soundInstancePlayerHit[num8].Play();
						break;
					}
					case 2:
						if (num == 1)
						{
							int num12 = rand.Next(3);
							if (num12 == 1)
							{
								num = 18;
							}
							if (num12 == 2)
							{
								num = 19;
							}
						}
						if (num != 9 && num != 10 && num != 24 && num != 26 && num != 34)
						{
							soundInstanceItem[num].Stop();
						}
						soundInstanceItem[num] = soundItem[num].CreateInstance();
						soundInstanceItem[num].Volume = num2;
						soundInstanceItem[num].Pan = num3;
						soundInstanceItem[num].Pitch = (float)rand.Next(-6, 7) * 0.01f;
						if (num == 26 || num == 35)
						{
							soundInstanceItem[num].Volume = num2 * 0.75f;
							soundInstanceItem[num].Pitch = harpNote;
						}
						soundInstanceItem[num].Play();
						break;
					case 3:
						soundInstanceNPCHit[num].Stop();
						soundInstanceNPCHit[num] = soundNPCHit[num].CreateInstance();
						soundInstanceNPCHit[num].Volume = num2;
						soundInstanceNPCHit[num].Pan = num3;
						soundInstanceNPCHit[num].Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceNPCHit[num].Play();
						break;
					case 4:
						if (num != 10 || soundInstanceNPCKilled[num].State != 0)
						{
							soundInstanceNPCKilled[num] = soundNPCKilled[num].CreateInstance();
							soundInstanceNPCKilled[num].Volume = num2;
							soundInstanceNPCKilled[num].Pan = num3;
							soundInstanceNPCKilled[num].Pitch = (float)rand.Next(-10, 11) * 0.01f;
							soundInstanceNPCKilled[num].Play();
						}
						break;
					case 5:
						soundInstancePlayerKilled.Stop();
						soundInstancePlayerKilled = soundPlayerKilled.CreateInstance();
						soundInstancePlayerKilled.Volume = num2;
						soundInstancePlayerKilled.Pan = num3;
						soundInstancePlayerKilled.Play();
						break;
					case 6:
						soundInstanceGrass.Stop();
						soundInstanceGrass = soundGrass.CreateInstance();
						soundInstanceGrass.Volume = num2;
						soundInstanceGrass.Pan = num3;
						soundInstanceGrass.Pitch = (float)rand.Next(-30, 31) * 0.01f;
						soundInstanceGrass.Play();
						break;
					case 7:
						soundInstanceGrab.Stop();
						soundInstanceGrab = soundGrab.CreateInstance();
						soundInstanceGrab.Volume = num2;
						soundInstanceGrab.Pan = num3;
						soundInstanceGrab.Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceGrab.Play();
						break;
					case 8:
						soundInstanceDoorOpen.Stop();
						soundInstanceDoorOpen = soundDoorOpen.CreateInstance();
						soundInstanceDoorOpen.Volume = num2;
						soundInstanceDoorOpen.Pan = num3;
						soundInstanceDoorOpen.Pitch = (float)rand.Next(-20, 21) * 0.01f;
						soundInstanceDoorOpen.Play();
						break;
					case 9:
						soundInstanceDoorClosed.Stop();
						soundInstanceDoorClosed = soundDoorClosed.CreateInstance();
						soundInstanceDoorClosed.Volume = num2;
						soundInstanceDoorClosed.Pan = num3;
						soundInstanceDoorOpen.Pitch = (float)rand.Next(-20, 21) * 0.01f;
						soundInstanceDoorClosed.Play();
						break;
					case 10:
						soundInstanceMenuOpen.Stop();
						soundInstanceMenuOpen = soundMenuOpen.CreateInstance();
						soundInstanceMenuOpen.Volume = num2;
						soundInstanceMenuOpen.Pan = num3;
						soundInstanceMenuOpen.Play();
						break;
					case 11:
						soundInstanceMenuClose.Stop();
						soundInstanceMenuClose = soundMenuClose.CreateInstance();
						soundInstanceMenuClose.Volume = num2;
						soundInstanceMenuClose.Pan = num3;
						soundInstanceMenuClose.Play();
						break;
					case 12:
						soundInstanceMenuTick.Stop();
						soundInstanceMenuTick = soundMenuTick.CreateInstance();
						soundInstanceMenuTick.Volume = num2;
						soundInstanceMenuTick.Pan = num3;
						soundInstanceMenuTick.Play();
						break;
					case 13:
						soundInstanceShatter.Stop();
						soundInstanceShatter = soundShatter.CreateInstance();
						soundInstanceShatter.Volume = num2;
						soundInstanceShatter.Pan = num3;
						soundInstanceShatter.Play();
						break;
					case 14:
					{
						int num13 = rand.Next(3);
						soundInstanceZombie[num13] = soundZombie[num13].CreateInstance();
						soundInstanceZombie[num13].Volume = num2 * 0.4f;
						soundInstanceZombie[num13].Pan = num3;
						soundInstanceZombie[num13].Play();
						break;
					}
					case 15:
						if (soundInstanceRoar[num].State == SoundState.Stopped)
						{
							soundInstanceRoar[num] = soundRoar[num].CreateInstance();
							soundInstanceRoar[num].Volume = num2;
							soundInstanceRoar[num].Pan = num3;
							soundInstanceRoar[num].Play();
						}
						break;
					case 16:
						soundInstanceDoubleJump.Stop();
						soundInstanceDoubleJump = soundDoubleJump.CreateInstance();
						soundInstanceDoubleJump.Volume = num2;
						soundInstanceDoubleJump.Pan = num3;
						soundInstanceDoubleJump.Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceDoubleJump.Play();
						break;
					case 17:
						soundInstanceRun.Stop();
						soundInstanceRun = soundRun.CreateInstance();
						soundInstanceRun.Volume = num2;
						soundInstanceRun.Pan = num3;
						soundInstanceRun.Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceRun.Play();
						break;
					case 18:
						soundInstanceCoins = soundCoins.CreateInstance();
						soundInstanceCoins.Volume = num2;
						soundInstanceCoins.Pan = num3;
						soundInstanceCoins.Play();
						break;
					case 19:
						if (soundInstanceSplash[num].State == SoundState.Stopped)
						{
							soundInstanceSplash[num] = soundSplash[num].CreateInstance();
							soundInstanceSplash[num].Volume = num2;
							soundInstanceSplash[num].Pan = num3;
							soundInstanceSplash[num].Pitch = (float)rand.Next(-10, 11) * 0.01f;
							soundInstanceSplash[num].Play();
						}
						break;
					case 20:
					{
						int num11 = rand.Next(3);
						soundInstanceFemaleHit[num11].Stop();
						soundInstanceFemaleHit[num11] = soundFemaleHit[num11].CreateInstance();
						soundInstanceFemaleHit[num11].Volume = num2;
						soundInstanceFemaleHit[num11].Pan = num3;
						soundInstanceFemaleHit[num11].Play();
						break;
					}
					case 21:
					{
						int num10 = rand.Next(3);
						soundInstanceTink[num10].Stop();
						soundInstanceTink[num10] = soundTink[num10].CreateInstance();
						soundInstanceTink[num10].Volume = num2;
						soundInstanceTink[num10].Pan = num3;
						soundInstanceTink[num10].Play();
						break;
					}
					case 22:
						soundInstanceUnlock.Stop();
						soundInstanceUnlock = soundUnlock.CreateInstance();
						soundInstanceUnlock.Volume = num2;
						soundInstanceUnlock.Pan = num3;
						soundInstanceUnlock.Play();
						break;
					case 23:
						soundInstanceDrown.Stop();
						soundInstanceDrown = soundDrown.CreateInstance();
						soundInstanceDrown.Volume = num2;
						soundInstanceDrown.Pan = num3;
						soundInstanceDrown.Play();
						break;
					case 24:
						soundInstanceChat = soundChat.CreateInstance();
						soundInstanceChat.Volume = num2;
						soundInstanceChat.Pan = num3;
						soundInstanceChat.Play();
						break;
					case 25:
						soundInstanceMaxMana = soundMaxMana.CreateInstance();
						soundInstanceMaxMana.Volume = num2;
						soundInstanceMaxMana.Pan = num3;
						soundInstanceMaxMana.Play();
						break;
					case 26:
					{
						int num9 = rand.Next(3, 5);
						soundInstanceZombie[num9] = soundZombie[num9].CreateInstance();
						soundInstanceZombie[num9].Volume = num2 * 0.9f;
						soundInstanceZombie[num9].Pan = num3;
						soundInstanceSplash[num].Pitch = (float)rand.Next(-10, 11) * 0.01f;
						soundInstanceZombie[num9].Play();
						break;
					}
					case 27:
						if (soundInstancePixie.State == SoundState.Playing)
						{
							soundInstancePixie.Volume = num2;
							soundInstancePixie.Pan = num3;
							soundInstancePixie.Pitch = (float)rand.Next(-10, 11) * 0.01f;
						}
						else
						{
							soundInstancePixie.Stop();
							soundInstancePixie = soundPixie.CreateInstance();
							soundInstancePixie.Volume = num2;
							soundInstancePixie.Pan = num3;
							soundInstancePixie.Pitch = (float)rand.Next(-10, 11) * 0.01f;
							soundInstancePixie.Play();
						}
						break;
					case 28:
						if (soundInstanceMech[num].State != 0)
						{
							soundInstanceMech[num] = soundMech[num].CreateInstance();
							soundInstanceMech[num].Volume = num2;
							soundInstanceMech[num].Pan = num3;
							soundInstanceMech[num].Pitch = (float)rand.Next(-10, 11) * 0.01f;
							soundInstanceMech[num].Play();
						}
						break;
					}
					if (type == 100)
					{
						soundInstanceNPCHit[num].Stop();
						soundInstanceNPCHit[num] = soundNPCHit[num].CreateInstance();
						soundInstanceNPCHit[num].Volume = num2;
						soundInstanceNPCHit[num].Pan = num3;
						soundInstanceNPCHit[num].Play();
					}
				}
				end_IL_0002:;
			}
			catch
			{
			}
		}
	}
}
