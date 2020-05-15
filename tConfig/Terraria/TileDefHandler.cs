using Microsoft.Xna.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class TileDefHandler : DefHandler
	{
		public Dictionary<string, string> modName;

		public Dictionary<string, int> ID;

		public DictionaryHandler<int, int> pretendType;

		public DictionaryHandler<int, string> dropName;

		public DictionaryHandler<int, int> width;

		public DictionaryHandler<int, int> height;

		public DictionaryHandler<int, string> load;

		public DictionaryHandler<int, int> furniture;

		public Dictionary<int, float> axe;

		public Dictionary<int, float> pick;

		public Dictionary<int, float> hammer;

		public Dictionary<int, int> minAxe;

		public Dictionary<int, int> minPick;

		public Dictionary<int, int> minHammer;

		public DictionaryHandler<int, string> mechToggle;

		public DictionaryHandler<int, DropTable> dropTables;

		public DictionaryHandler<int, bool> special;

		public DictionaryHandler<int, bool> spawnPoint;

		public DictionaryHandler<int, byte> doorType;

		public DictionaryHandler<int, string> doorToggle;

		public DictionaryHandler<int, bool> directional;

		public DictionaryHandler<int, string> placeOn;

		public DictionaryHandler<int, bool> treasure;

		public DictionaryHandler<int, int> dustType;

		public Dictionary<Vector2, Codable> code;

		public Dictionary<int, string> loadModname;

		public Dictionary<int, int[]> color;

		public Dictionary<int, int> hitSound;

		public Dictionary<int, int> hitSoundList;

		public Dictionary<int, float[]> light;

		public ArrayList globalCode;

		public ArrayList globalName;

		public TileDefHandler(int size)
		{
			width = new DictionaryHandler<int, int>(size);
			height = new DictionaryHandler<int, int>(size);
			furniture = new DictionaryHandler<int, int>(size);
			placeOn = new DictionaryHandler<int, string>(size);
			directional = new DictionaryHandler<int, bool>(size);
			doorToggle = new DictionaryHandler<int, string>();
			doorType = new DictionaryHandler<int, byte>(size);
			hitSound = new Dictionary<int, int>();
			hitSoundList = new Dictionary<int, int>();
			color = new Dictionary<int, int[]>();
			loadModname = new Dictionary<int, string>();
			spawnPoint = new DictionaryHandler<int, bool>(size);
			globalName = new ArrayList();
			globalCode = new ArrayList();
			code = new Dictionary<Vector2, Codable>();
			special = new DictionaryHandler<int, bool>(size);
			dropTables = new DictionaryHandler<int, DropTable>(size);
			mechToggle = new DictionaryHandler<int, string>(size);
			minHammer = new Dictionary<int, int>(size);
			minAxe = new Dictionary<int, int>(size);
			minPick = new Dictionary<int, int>(size);
			axe = new Dictionary<int, float>();
			pick = new Dictionary<int, float>();
			hammer = new Dictionary<int, float>();
			load = new DictionaryHandler<int, string>();
			ID = new Dictionary<string, int>();
			modName = new Dictionary<string, string>();
			assemblyByName = new DictionaryHandler<string, Assembly>();
			assemblyByType = new DictionaryHandler<int, Assembly>();
			pretendType = new DictionaryHandler<int, int>();
			globalAssembly = new ArrayList();
			globalModname = new ArrayList();
			dropName = new DictionaryHandler<int, string>(size);
			light = new Dictionary<int, float[]>();
			treasure = new DictionaryHandler<int, bool>();
			dustType = new DictionaryHandler<int, int>();
		}

		public void AddDefaultTile(int id, string name, int width, int height)
		{
			ID.Add(name, id);
			this.width[id] = width;
			this.height[id] = height;
		}

		public static void AddDefaultTiles()
		{
			Config.tileDefs.AddDefaultTile(0, "Dirt", 1, 1);
			Config.tileDefs.AddDefaultTile(1, "Stone", 1, 1);
			Config.tileDefs.AddDefaultTile(2, "Normal Grass", 1, 1);
			Config.tileDefs.AddDefaultTile(3, "Normal Plant", 1, 1);
			Config.tileDefs.AddDefaultTile(4, "Torch", 1, 1);
			Config.tileDefs.AddDefaultTile(5, "Tree Trunk", 1, 1);
			Config.tileDefs.AddDefaultTile(6, "Iron", 1, 1);
			Config.tileDefs.AddDefaultTile(7, "Copper", 1, 1);
			Config.tileDefs.AddDefaultTile(8, "Gold", 1, 1);
			Config.tileDefs.AddDefaultTile(9, "Silver", 1, 1);
			Config.tileDefs.AddDefaultTile(10, "Closed Door", 1, 3);
			Config.tileDefs.AddDefaultTile(11, "Open Door", 2, 3);
			Config.tileDefs.AddDefaultTile(12, "Heart Stone", 2, 2);
			Config.tileDefs.AddDefaultTile(13, "Bottle", 1, 1);
			Config.tileDefs.AddDefaultTile(14, "Table", 3, 2);
			Config.tileDefs.AddDefaultTile(15, "Chair", 1, 2);
			Config.tileDefs.AddDefaultTile(16, "Anvil", 2, 1);
			Config.tileDefs.AddDefaultTile(17, "Furnace", 3, 2);
			Config.tileDefs.AddDefaultTile(18, "Workbench", 2, 1);
			Config.tileDefs.AddDefaultTile(19, "Wood Platform", 1, 1);
			Config.tileDefs.AddDefaultTile(20, "Sapling", 1, 2);
			Config.tileDefs.AddDefaultTile(21, "Chest", 2, 2);
			Config.tileDefs.AddDefaultTile(22, "Demonite", 1, 1);
			Config.tileDefs.AddDefaultTile(23, "Corrupt Grass", 1, 1);
			Config.tileDefs.AddDefaultTile(24, "Corrupt Plant", 1, 1);
			Config.tileDefs.AddDefaultTile(25, "Ebonstone", 1, 1);
			Config.tileDefs.AddDefaultTile(26, "Demon Altar", 3, 2);
			Config.tileDefs.AddDefaultTile(27, "Sunflower", 2, 4);
			Config.tileDefs.AddDefaultTile(28, "Pot", 2, 2);
			Config.tileDefs.AddDefaultTile(29, "Piggy Bank", 2, 1);
			Config.tileDefs.AddDefaultTile(30, "Wood", 1, 1);
			Config.tileDefs.AddDefaultTile(31, "Shadow Orb", 2, 2);
			Config.tileDefs.AddDefaultTile(32, "Corrupt Thorns", 1, 1);
			Config.tileDefs.AddDefaultTile(33, "Candle", 1, 1);
			Config.tileDefs.AddDefaultTile(34, "Copper Chandelier", 3, 2);
			Config.tileDefs.AddDefaultTile(35, "Silver Chandelier", 3, 2);
			Config.tileDefs.AddDefaultTile(36, "Gold Chandelier", 3, 2);
			Config.tileDefs.AddDefaultTile(37, "Meteorite", 1, 1);
			Config.tileDefs.AddDefaultTile(38, "Grey Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(39, "Red Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(40, "Clay", 1, 1);
			Config.tileDefs.AddDefaultTile(41, "Blue Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(42, "Chain Lantern", 1, 2);
			Config.tileDefs.AddDefaultTile(43, "Green Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(44, "Pink Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(45, "Gold Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(46, "Silver Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(47, "Copper Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(48, "Spike", 1, 1);
			Config.tileDefs.AddDefaultTile(49, "Water Candle", 1, 1);
			Config.tileDefs.AddDefaultTile(50, "Books", 1, 1);
			Config.tileDefs.AddDefaultTile(51, "Cobweb", 1, 1);
			Config.tileDefs.AddDefaultTile(52, "Normal Vine", 1, 1);
			Config.tileDefs.AddDefaultTile(53, "Sand", 1, 1);
			Config.tileDefs.AddDefaultTile(54, "Glass", 1, 1);
			Config.tileDefs.AddDefaultTile(55, "Sign", 2, 2);
			Config.tileDefs.AddDefaultTile(56, "Obsidian", 1, 1);
			Config.tileDefs.AddDefaultTile(57, "Ash", 1, 1);
			Config.tileDefs.AddDefaultTile(58, "Hellstone", 1, 1);
			Config.tileDefs.AddDefaultTile(59, "Mud", 1, 1);
			Config.tileDefs.AddDefaultTile(60, "Jungle Grass", 1, 1);
			Config.tileDefs.AddDefaultTile(61, "Jungle Plant", 1, 1);
			Config.tileDefs.AddDefaultTile(62, "Jungle Vine", 1, 1);
			Config.tileDefs.AddDefaultTile(63, "Sapphire", 1, 1);
			Config.tileDefs.AddDefaultTile(64, "Ruby", 1, 1);
			Config.tileDefs.AddDefaultTile(65, "Emerald", 1, 1);
			Config.tileDefs.AddDefaultTile(66, "Topaz", 1, 1);
			Config.tileDefs.AddDefaultTile(67, "Amethyst", 1, 1);
			Config.tileDefs.AddDefaultTile(68, "Diamond", 1, 1);
			Config.tileDefs.AddDefaultTile(69, "Jungle Thorns", 1, 1);
			Config.tileDefs.AddDefaultTile(70, "Mushroom Grass", 1, 1);
			Config.tileDefs.AddDefaultTile(71, "Glowing Mushroom", 1, 1);
			Config.tileDefs.AddDefaultTile(72, "Mushroom Stalk", 1, 1);
			Config.tileDefs.AddDefaultTile(73, "Normal Tall Plant", 1, 2);
			Config.tileDefs.AddDefaultTile(74, "Jungle Tall Plant", 1, 2);
			Config.tileDefs.AddDefaultTile(75, "Obsidian Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(76, "Hellstone Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(77, "Hellforge", 3, 2);
			Config.tileDefs.AddDefaultTile(78, "Clay Pot", 1, 1);
			Config.tileDefs.AddDefaultTile(79, "Bed", 4, 2);
			Config.tileDefs.AddDefaultTile(80, "Cactus", 1, 1);
			Config.tileDefs.AddDefaultTile(81, "Coral", 1, 1);
			Config.tileDefs.AddDefaultTile(82, "New Herb", 1, 1);
			Config.tileDefs.AddDefaultTile(83, "Grown Herb", 1, 1);
			Config.tileDefs.AddDefaultTile(84, "Bloomed Herb", 1, 1);
			Config.tileDefs.AddDefaultTile(85, "Tombstone", 2, 2);
			Config.tileDefs.AddDefaultTile(86, "Loom", 3, 2);
			Config.tileDefs.AddDefaultTile(87, "Piano", 3, 2);
			Config.tileDefs.AddDefaultTile(88, "Drawer", 3, 2);
			Config.tileDefs.AddDefaultTile(89, "Bench", 3, 2);
			Config.tileDefs.AddDefaultTile(90, "Bathtub", 4, 2);
			Config.tileDefs.AddDefaultTile(91, "Banner", 1, 3);
			Config.tileDefs.AddDefaultTile(92, "Lamp Post", 1, 6);
			Config.tileDefs.AddDefaultTile(93, "Tiki Torch", 1, 3);
			Config.tileDefs.AddDefaultTile(94, "Keg", 2, 2);
			Config.tileDefs.AddDefaultTile(95, "Chinese Lantern", 2, 2);
			Config.tileDefs.AddDefaultTile(96, "Cooking Pot", 2, 2);
			Config.tileDefs.AddDefaultTile(97, "Safe", 2, 2);
			Config.tileDefs.AddDefaultTile(98, "Skull Lantern", 2, 2);
			Config.tileDefs.AddDefaultTile(99, "Trash Can", 2, 2);
			Config.tileDefs.AddDefaultTile(100, "Candlebra", 2, 2);
			Config.tileDefs.AddDefaultTile(101, "Bookcase", 3, 4);
			Config.tileDefs.AddDefaultTile(102, "Throne", 3, 4);
			Config.tileDefs.AddDefaultTile(103, "Bowl", 2, 1);
			Config.tileDefs.AddDefaultTile(104, "Grandfather Clock", 2, 5);
			Config.tileDefs.AddDefaultTile(105, "Statue", 2, 3);
			Config.tileDefs.AddDefaultTile(106, "Sawmill", 3, 3);
			Config.tileDefs.AddDefaultTile(107, "Cobalt", 1, 1);
			Config.tileDefs.AddDefaultTile(108, "Mythril", 1, 1);
			Config.tileDefs.AddDefaultTile(109, "Hallow Grass", 1, 1);
			Config.tileDefs.AddDefaultTile(110, "Hallow Plant", 1, 1);
			Config.tileDefs.AddDefaultTile(111, "Adamantite", 1, 1);
			Config.tileDefs.AddDefaultTile(112, "Ebonsand", 1, 1);
			Config.tileDefs.AddDefaultTile(113, "Hallow Tall Plant", 1, 2);
			Config.tileDefs.AddDefaultTile(114, "Tinkerer's Workshop", 3, 2);
			Config.tileDefs.AddDefaultTile(115, "Hallow Vine", 1, 1);
			Config.tileDefs.AddDefaultTile(116, "Pearlsand", 1, 1);
			Config.tileDefs.AddDefaultTile(117, "Pearlstone", 1, 1);
			Config.tileDefs.AddDefaultTile(118, "Pearlstone Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(119, "Iridescent Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(120, "Mudstone Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(121, "Cobalt Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(122, "Mythril Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(123, "Silt", 1, 1);
			Config.tileDefs.AddDefaultTile(124, "Wooden Beam", 1, 1);
			Config.tileDefs.AddDefaultTile(125, "Crystal Ball", 2, 2);
			Config.tileDefs.AddDefaultTile(126, "Disco Ball", 2, 2);
			Config.tileDefs.AddDefaultTile(127, "Glass 2", 1, 1);
			Config.tileDefs.AddDefaultTile(128, "Mannequin", 2, 3);
			Config.tileDefs.AddDefaultTile(129, "Crystal Shard", 1, 1);
			Config.tileDefs.AddDefaultTile(130, "Active Stone", 1, 1);
			Config.tileDefs.AddDefaultTile(131, "Inactive Stone", 1, 1);
			Config.tileDefs.AddDefaultTile(132, "Lever", 2, 2);
			Config.tileDefs.AddDefaultTile(133, "Adamantite Forge", 3, 2);
			Config.tileDefs.AddDefaultTile(134, "Mythril Anvil", 2, 1);
			Config.tileDefs.AddDefaultTile(135, "Pressure Plate", 1, 1);
			Config.tileDefs.AddDefaultTile(136, "Switch", 1, 1);
			Config.tileDefs.AddDefaultTile(137, "Dart Trap", 1, 1);
			Config.tileDefs.AddDefaultTile(138, "Boulder", 2, 2);
			Config.tileDefs.AddDefaultTile(139, "Music Box", 2, 2);
			Config.tileDefs.AddDefaultTile(140, "Demonite Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(141, "Explosives", 1, 1);
			Config.tileDefs.AddDefaultTile(142, "Inlet Pump", 2, 2);
			Config.tileDefs.AddDefaultTile(143, "Outlet Pump", 2, 2);
			Config.tileDefs.AddDefaultTile(144, "Timer", 1, 1);
			Config.tileDefs.AddDefaultTile(145, "Candy Cane", 1, 1);
			Config.tileDefs.AddDefaultTile(146, "Green Candy Cane", 1, 1);
			Config.tileDefs.AddDefaultTile(147, "Snow", 1, 1);
			Config.tileDefs.AddDefaultTile(148, "Snow Brick", 1, 1);
			Config.tileDefs.AddDefaultTile(149, "Christmas Light", 1, 1);
		}
	}
}
