using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Terraria
{
	public class Events
	{
		public class Event
		{
			public Dictionary<string, Delegate> delegates;

			public Event()
			{
				delegates = new Dictionary<string, Delegate>();
			}

			public virtual void ResetEvents()
			{
			}

			public virtual void RegisterEvents(object code)
			{
			}

			public void Register<T>(ref T method, object subclass, string methodName) where T : class
			{
				T del = Codable.GetDel<T>(subclass, methodName);
				if (del != null)
				{
					Delegate @delegate = Delegate.Combine(method as Delegate, del as Delegate);
					delegates[methodName] = @delegate;
					method = (@delegate as T);
				}
				else
				{
					delegates[methodName] = (method as Delegate);
				}
			}
		}

		public class Generic : Event
		{
			public Action<string> ServerCommand;

			public Action<Main, SpriteBatch> PostDrawMenu;

			public Action OnLoad;

			public Action OnUnload;

			public Action<Player> UpdateSpawn;

			public Action<List<int>> ModifySpawnList;

			public Action PreUpdateMusic;

			public Action checkXMas;

			public Func<SpriteBatch, string, int, byte, string[], bool[], bool[], bool> ItemMouseText;

			public override void ResetEvents()
			{
				ServerCommand = null;
				PostDrawMenu = null;
				OnLoad = null;
				OnUnload = null;
				UpdateSpawn = null;
				ModifySpawnList = null;
				PreUpdateMusic = null;
				checkXMas = null;
				ItemMouseText = null;
			}

			public override void RegisterEvents(object code)
			{
				if (code != null)
				{
					Register(ref ServerCommand, code, "ServerCommand");
					Register(ref PostDrawMenu, code, "PostDrawMenu");
					Register(ref OnLoad, code, "OnLoad");
					Register(ref OnUnload, code, "OnUnload");
					Register(ref UpdateSpawn, code, "UpdateSpawn");
					Register(ref ModifySpawnList, code, "ModifySpawnList");
					Register(ref PreUpdateMusic, code, "PreUpdateMusic");
					Register(ref checkXMas, code, "checkXMas");
					Register(ref ItemMouseText, code, "ItemMouseText");
				}
			}
		}

		public class World : Event
		{
			public delegate void NetReceiveIntercept_Del(messageBuffer msgBuffer, int b, int start, int length, ref int num);

			public delegate void NetSendIntercept_Del(int num, ref int num2, ref int num3, int msgType, int remoteClient, int ignoreClient, string text, int number, float number2, float number3, float number4, int number5);

			public delegate bool PreNetReceiveIntercept_Del(messageBuffer msgBuffer, int b, int start, int length, ref int num);

			public delegate bool PreNetSendIntercept_Del(int num, ref int num2, ref int num3, int msgType, int remoteClient, int ignoreClient, string text, int number, float number2, float number3, float number4, int number5);

			public delegate bool GrowShroom_Del(int i, int y);

			public NetReceiveIntercept_Del NetReceiveIntercept;

			public NetSendIntercept_Del NetSendIntercept;

			public PreNetReceiveIntercept_Del PreNetReceiveIntercept;

			public PreNetSendIntercept_Del PreNetSendIntercept;

			public Action<int> Initialize;

			public Func<Vector2, Vector2, int, int, bool, bool, Vector2> TileCollision;

			public Action<Item, int> RemoveOldItemInWorld;

			public Action<SpriteBatch, bool> PreDrawTiles;

			public Action<SpriteBatch, bool> PostDrawTiles;

			public Action<int> PlayerConnected;

			public Action SyncPlayers;

			public Func<int, int, int, bool[]> MoveNPC;

			public Func<int, int, bool[]> GrowEpicTree;

			public Func<int, int, bool> GrowTree;

			public Func<int, int, bool> GrowShroom;

			public Func<int, int, int, int, int, bool[]> EmptyTileCheck;

			public Func<int, int, byte, byte, bool> HellHouse;

			public Func<int, int, int, int, byte, byte, bool> HellRoom;

			public Func<int, int, int, int, bool> DungeonStairs;

			public Func<int, int, int, int, bool> DungeonHalls;

			public Func<int, int, int, int, bool> DungeonRoom;

			public Func<int, int, int, bool, int, bool[]> AddBuriedChest;

			public Func<bool> PlantAlch;

			public Func<int, int, int, int, bool> SpreadGrass;

			public Func<int, int, int, int, bool> ChasmRunnerSideways;

			public Func<int, int, int, bool, bool> ChasmRunner;

			public Func<int, int, double, int, int, bool, float, float, bool, bool, bool> TileRunner;

			public Func<int, int, bool> MudWallRunner;

			public Func<int, int, bool> FloatingIsland;

			public Func<int, int, bool> Caverer;

			public Func<int, int, bool> IslandHouse;

			public Func<int, int, bool> Mountinater;

			public Func<int, int, int, bool> Cavinator;

			public Func<int, int, bool> CaveOpenator;

			public override void ResetEvents()
			{
				NetReceiveIntercept = null;
				NetSendIntercept = null;
				PreNetReceiveIntercept = null;
				PreNetSendIntercept = null;
				Initialize = null;
				TileCollision = null;
				RemoveOldItemInWorld = null;
				PreDrawTiles = null;
				PostDrawTiles = null;
				PlayerConnected = null;
				SyncPlayers = null;
				MoveNPC = null;
				GrowEpicTree = null;
				GrowTree = null;
				GrowShroom = null;
				EmptyTileCheck = null;
				HellHouse = null;
				HellRoom = null;
				DungeonStairs = null;
				DungeonHalls = null;
				DungeonRoom = null;
				AddBuriedChest = null;
				PlantAlch = null;
				SpreadGrass = null;
				ChasmRunnerSideways = null;
				ChasmRunner = null;
				TileRunner = null;
				MudWallRunner = null;
				FloatingIsland = null;
				Caverer = null;
				IslandHouse = null;
				Mountinater = null;
				Cavinator = null;
				CaveOpenator = null;
			}

			public override void RegisterEvents(object code)
			{
				if (code != null)
				{
					Register(ref NetReceiveIntercept, code, "NetReceiveIntercept");
					Register(ref NetSendIntercept, code, "NetSendIntercept");
					Register(ref PreNetSendIntercept, code, "PreNetSendIntercept");
					Register(ref PreNetReceiveIntercept, code, "PreNetReceiveIntercept");
					Register(ref Initialize, code, "Initialize");
					Register(ref TileCollision, code, "TileCollision");
					Register(ref RemoveOldItemInWorld, code, "RemoveOldItemInWorld");
					Register(ref PreDrawTiles, code, "PreDrawTiles");
					Register(ref PostDrawTiles, code, "PostDrawTiles");
					Register(ref PlayerConnected, code, "PlayerConnected");
					Register(ref SyncPlayers, code, "SyncPlayers");
					Register(ref MoveNPC, code, "MoveNPC");
					Register(ref GrowEpicTree, code, "GrowEpicTree");
					Register(ref GrowTree, code, "GrowTree");
					Register(ref GrowShroom, code, "GrowShroom");
					Register(ref EmptyTileCheck, code, "EmptyTileCheck");
					Register(ref HellHouse, code, "HellHouse");
					Register(ref HellRoom, code, "HellRoom");
					Register(ref DungeonStairs, code, "DungeonStairs");
					Register(ref DungeonHalls, code, "DungeonHalls");
					Register(ref DungeonRoom, code, "DungeonRoom");
					Register(ref AddBuriedChest, code, "AddBuriedChest");
					Register(ref PlantAlch, code, "PlantAlch");
					Register(ref SpreadGrass, code, "SpreadGrass");
					Register(ref ChasmRunnerSideways, code, "ChasmRunnerSideways");
					Register(ref ChasmRunner, code, "ChasmRunner");
					Register(ref TileRunner, code, "TileRunner");
					Register(ref MudWallRunner, code, "MudWallRunner");
					Register(ref FloatingIsland, code, "FloatingIsland");
					Register(ref Caverer, code, "Caverer");
					Register(ref IslandHouse, code, "IslandHouse");
					Register(ref Mountinater, code, "Mountinater");
					Register(ref Cavinator, code, "Cavinator");
					Register(ref CaveOpenator, code, "CaveOpenator");
				}
			}
		}

		public class Player : Event
		{
			public delegate void DamagePlayer_Del(Terraria.Player player, ref int damage, NPC npc);

			public delegate void DamageNPC_Del(Terraria.Player myPlayer, NPC npc, ref int damage, ref float knockback);

			public delegate void DrawBefore_Del1(Terraria.Player P, SpriteBatch SP, ref bool LetDraw, ref Color Color);

			public delegate void DrawBefore_Del3(Terraria.Player P, SpriteBatch SP, ref bool LetDraw, ref Color Color, ref Color Color2, ref Color Color3);

			public delegate void DrawBefore_Del4(Terraria.Player P, SpriteBatch SP, ref bool LetDraw, ref Color Color, ref Color Color2, ref Color Color3, ref Color Color4);

			public delegate void DrawBefore_Del5(Terraria.Player P, SpriteBatch SP, ref bool LetDraw, ref Color Color, ref Color Color2, ref Color Color3, ref Color Color4, ref Color Color5);

			public delegate void DrawAfter_Del1(Terraria.Player P, SpriteBatch SP, ref Color Color);

			public delegate void DrawAfter_Del3(Terraria.Player P, SpriteBatch SP, ref Color Color, ref Color Color2, ref Color Color3);

			public delegate void DrawAfter_Del4(Terraria.Player P, SpriteBatch SP, ref Color Color, ref Color Color2, ref Color Color3, ref Color Color4);

			public delegate void DrawAfter_Del5(Terraria.Player P, SpriteBatch SP, ref Color Color, ref Color Color2, ref Color Color3, ref Color Color4, ref Color Color5);

			public DamagePlayer_Del DamagePlayer;

			public DamageNPC_Del DamageNPC;

			public DrawBefore_Del5 DrawBeforeHead;

			public DrawAfter_Del5 DrawAfterHead;

			public DrawBefore_Del4 DrawBeforeBody;

			public DrawAfter_Del4 DrawAfterBody;

			public DrawBefore_Del3 DrawBeforeLegs;

			public DrawAfter_Del3 DrawAfterLegs;

			public DrawBefore_Del1 DrawBeforeWeapon;

			public DrawAfter_Del1 DrawAfterWeapon;

			public DrawBefore_Del4 DrawBeforeArms;

			public DrawAfter_Del4 DrawAfterArms;

			public Player()
			{
				ResetEvents();
			}

			public override void ResetEvents()
			{
				DamagePlayer = null;
				DrawBeforeHead = null;
				DrawAfterHead = null;
				DrawBeforeBody = null;
				DrawAfterBody = null;
				DrawBeforeLegs = null;
				DrawAfterLegs = null;
				DrawBeforeWeapon = null;
				DrawAfterWeapon = null;
				DrawBeforeArms = null;
				DrawAfterArms = null;
				DamageNPC = null;
			}

			public override void RegisterEvents(object code)
			{
				Register(ref DamagePlayer, code, "DamagePlayer");
				Register(ref DamageNPC, code, "DamageNPC");
				Register(ref DrawBeforeHead, code, "DrawBeforeHead");
				Register(ref DrawAfterHead, code, "DrawAfterHead");
				Register(ref DrawBeforeBody, code, "DrawBeforeBody");
				Register(ref DrawAfterBody, code, "DrawAfterBody");
				Register(ref DrawBeforeLegs, code, "DrawBeforeLegs");
				Register(ref DrawAfterLegs, code, "DrawAfterLegs");
				Register(ref DrawBeforeWeapon, code, "DrawBeforeWeapon");
				Register(ref DrawAfterWeapon, code, "DrawAfterWeapon");
				Register(ref DrawBeforeArms, code, "DrawBeforeArms");
				Register(ref DrawAfterArms, code, "DrawAfterArms");
			}
		}

		public static Generic generic;

		public static World world;

		public static Player player;

		public static void Initialize()
		{
			generic = new Generic();
			generic.ResetEvents();
			world = new World();
			world.ResetEvents();
			player = new Player();
			player.ResetEvents();
		}
	}
}
