using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Terraria
{
	public static class TMod
	{
		public enum HookOwners
		{
			ModWorld,
			ModPlayer,
			ModGeneric,
			NPC,
			Item,
			Projectile,
			Count
		}

		public enum WorldHooks
		{
			Initialize,
			TileCollision,
			RemoveOldItemInWorld,
			ModifyLightVision,
			PreDrawTiles,
			PostDrawTiles,
			PreDrawLifeHearts,
			PreDrawManaStars,
			PreDrawBubbleBar,
			PreDrawBuffsList,
			PreDrawTrashItem,
			PreDrawInventoryTitle,
			PreDrawInventorySlots,
			PreDrawHotbarLock,
			PreDrawNPCHouseToggle,
			PreDrawNPCHousingMenu,
			PreDrawPlayerEquipment,
			PreDrawAvailableRecipes,
			PreDrawInventoryCoins,
			PreDrawInventoryAmmo,
			PreDrawNPCShop,
			PreDrawChestButtons,
			PreDrawChestSlots,
			PreDrawInformationTexts,
			PreDrawEscapeButtons,
			PreDrawHotbarInventory,
			PreDrawDeathText,
			PreDrawLifeText,
			PreDrawManaText,
			PreLightTiles,
			PostLightTiles,
			PreDrawWater,
			PostDrawWater,
			PreDrawBackground,
			PostDrawBackground,
			PreDrawWalls,
			PostDrawWalls,
			PreDrawInterface,
			PostDraw,
			CraftItem,
			PlayerConnected,
			SyncPlayers,
			MoveNPC,
			MoveRoom,
			KickOut,
			SpawnNPC,
			RoomNeeds,
			QuickFindHome,
			ScoreRoom,
			StartRoomCheck,
			CheckRoom,
			DropMeteor,
			Meteor,
			SetWorldSize,
			CreateNewWorld,
			SaveAndQuit,
			PlayWorld,
			SaveAndPlay,
			SaveToonWhilePlaying,
			ServerLoadWorld,
			ClearWorld,
			SaveWorld,
			LoadWorld,
			ResetGen,
			PlaceTrap,
			GenerateWorld,
			GrowEpicTree,
			GrowTree,
			GrowShroom,
			AddTrees,
			EmptyTileCheck,
			StartHardMode,
			PlaceDoor,
			CloseDoor,
			AddLifeCrystal,
			AddShadowOrb,
			AddHellHouses,
			HellHouse,
			HellRoom,
			MakeDungeon,
			DungeonStairs,
			DungeonHalls,
			DungeonRoom,
			DungeonEnt,
			AddBuriedChest,
			OpenDoor,
			GrowAlch,
			PlantAlch,
			OreRunner,
			SmashAltar,
			PreHitWire,
			HardUpdateWorld,
			MineHouse,
			PreUpdateWorld,
			UpdateWorld,
			AddPlants,
			SpreadGrass,
			ChasmRunnerSideways,
			ChasmRunner,
			JungleRunner,
			GERunner,
			TileRunner,
			MudWallRunner,
			FloatingIsland,
			Caverer,
			IslandHouse,
			Mountinater,
			Lakinater,
			ShroomPatch,
			Cavinator,
			CaveOpenator,
			PostUpdate,
			PreUpdate,
			Count
		}

		public enum PlayerHooks
		{
			Initialize,
			FrameEffect,
			PreDraw,
			ModifyPlayerDrawColors,
			OverrideWingDraw,
			DrawBeforeLegs,
			DrawAfterLegs,
			DrawBeforeBody,
			DrawAfterBody,
			DrawBeforeWeapon,
			DrawAfterWeapon,
			DrawBeforeArms,
			DrawAfterArms,
			PostDraw,
			DamagePlayer,
			DealtPlayer,
			OnSpawn,
			PreQuickHeal,
			PreQuickMana,
			PreQuickBuff,
			PreUpdatePlayer,
			UpdatePlayer,
			AdjTiles,
			PreKill,
			PostKill,
			DealtNPC,
			DamageNPC,
			DamagePVP,
			PreQuickGrapple,
			CreatePlayer,
			PostLoad,
			Count
		}

		public enum GenericHooks
		{
			ServerCommand,
			OnLoad,
			OnUnload,
			PreUpdateMusic,
			CheckXMas,
			ItemMouseText,
			UpdateSpawn,
			Count
		}

		public enum NPCHooks
		{
			Count
		}

		public enum ItemHooks
		{
			Count
		}

		public enum ProjectileHooks
		{
			Count
		}

		private static Func<object[], object>[][][] HookLists = new Func<object[], object>[6][][]
		{
			new Func<object[], object>[113][],
			new Func<object[], object>[31][],
			new Func<object[], object>[7][],
			new Func<object[], object>[0][],
			new Func<object[], object>[0][],
			new Func<object[], object>[0][]
		};

		private static Action<object>[] InitMod = new Action<object>[6]
		{
			InitModWorld,
			InitModPlayer,
			InitModGeneric,
			InitModNPC,
			InitModItem,
			InitModProjectile
		};

		internal static readonly Assembly Entry = Assembly.GetEntryAssembly();

		private static int CurrentMod = 0;

		private static Enum LastMethod;

		private static string LoadingMod = "Unknown Mod";

		public static object[] ReturnValue = null;

		public static object[][] RefReturnValues = null;

		private static ContentManager Manager = null;

		private static BlendState PreBlend;

		private static DepthStencilState PreDepth;

		private static SamplerState PreSampler;

		public static void ResetAll()
		{
			for (HookOwners hookOwners = HookOwners.ModWorld; hookOwners < HookOwners.Count; hookOwners++)
			{
				Reset(hookOwners);
			}
		}

		public static void ResetWorld()
		{
			Reset(HookOwners.ModWorld);
		}

		public static void ResetPlayer()
		{
			Reset(HookOwners.ModPlayer);
		}

		public static void ResetGeneric()
		{
			Reset(HookOwners.ModGeneric);
		}

		public static void ResetNPC()
		{
			Reset(HookOwners.NPC);
		}

		public static void ResetItem()
		{
			Reset(HookOwners.Item);
		}

		public static void ResetProjectile()
		{
			Reset(HookOwners.Projectile);
		}

		private static void Reset(HookOwners Owner)
		{
			for (int i = 0; i < HookLists[(int)Owner].Length; i++)
			{
				for (int j = 0; j < HookLists[(int)Owner][i].Length; j++)
				{
					HookLists[(int)Owner][i][j] = null;
				}
			}
		}

		public static void Init()
		{
			ContentManager contentManager = new ContentManager(Config.mainInstance.Services);
			contentManager.RootDirectory = Config.tempModAssembly;
			Manager = contentManager;
			for (int i = 0; i < HookLists.Length; i++)
			{
				for (int j = 0; j < HookLists[i].Length; j++)
				{
					HookLists[i][j] = new Func<object[], object>[Config.mods.Count];
				}
			}
			ReturnValue = new object[Config.mods.Count];
			RefReturnValues = new object[Config.mods.Count][];
		}

		public static void Init(object ModObj, HookOwners Owner, int ModIndex)
		{
			LoadingMod = Config.mods[ModIndex];
			CurrentMod = ModIndex;
			InitMod[(int)Owner](ModObj);
		}

		public static void InitModWorld(object ModObj)
		{
			Type type = ModObj.GetType();
			Action<int> ModFunc = FuncFromMethod<Action<int>>(ModObj, type, WorldHooks.Initialize);
			if (ModFunc != null)
			{
				AddFunc(WorldHooks.Initialize, delegate(object[] Params)
				{
					ModFunc((int)Params[0]);
					return null;
				});
			}
			Func<Vector2, Vector2, Vector2, int, int, bool, bool, Vector2> ModFunc2 = FuncFromMethod<Func<Vector2, Vector2, Vector2, int, int, bool, bool, Vector2>>(ModObj, type, WorldHooks.TileCollision);
			if (ModFunc2 != null)
			{
				AddFunc(WorldHooks.TileCollision, (object[] Params) => ModFunc2((Vector2)Params[0], (Vector2)Params[1], (Vector2)Params[2], (int)Params[3], (int)Params[4], (bool)Params[5], (bool)Params[6]));
			}
			Action<Item, int> ModFunc3 = FuncFromMethod<Action<Item, int>>(ModObj, type, WorldHooks.RemoveOldItemInWorld);
			if (ModFunc3 != null)
			{
				AddFunc(WorldHooks.RemoveOldItemInWorld, delegate(object[] Params)
				{
					ModFunc3((Item)Params[0], (int)Params[1]);
					return null;
				});
			}
			Action<float[]> ModFunc4 = FuncFromMethod<Action<float[]>>(ModObj, type, WorldHooks.ModifyLightVision);
			if (ModFunc4 != null)
			{
				AddFunc(WorldHooks.ModifyLightVision, delegate(object[] Params)
				{
					ModFunc4((float[])Params[0]);
					return null;
				});
			}
			Action<SpriteBatch, bool> ModFunc5 = FuncFromMethod<Action<SpriteBatch, bool>>(ModObj, type, WorldHooks.PreDrawTiles);
			if (ModFunc5 != null)
			{
				AddFunc(WorldHooks.PreDrawTiles, delegate(object[] Params)
				{
					ModFunc5((SpriteBatch)Params[0], (bool)Params[1]);
					return null;
				});
			}
			Action<SpriteBatch, bool> ModFunc6 = FuncFromMethod<Action<SpriteBatch, bool>>(ModObj, type, WorldHooks.PostDrawTiles);
			if (ModFunc6 != null)
			{
				AddFunc(WorldHooks.PostDrawTiles, delegate(object[] Params)
				{
					ModFunc6((SpriteBatch)Params[0], (bool)Params[1]);
					return null;
				});
			}
			for (WorldHooks worldHooks = WorldHooks.PreDrawLifeHearts; worldHooks <= WorldHooks.PreDrawManaText; worldHooks++)
			{
				Func<SpriteBatch, bool> ModFunc7 = FuncFromMethod<Func<SpriteBatch, bool>>(ModObj, type, worldHooks);
				if (ModFunc7 != null)
				{
					AddFunc(worldHooks, (object[] Params) => ModFunc7((SpriteBatch)Params[0]));
				}
			}
			for (WorldHooks worldHooks2 = WorldHooks.PreLightTiles; worldHooks2 <= WorldHooks.PostDraw; worldHooks2++)
			{
				Action<SpriteBatch> ModFunc8 = FuncFromMethod<Action<SpriteBatch>>(ModObj, type, worldHooks2);
				if (ModFunc8 != null)
				{
					AddFunc(worldHooks2, delegate(object[] Params)
					{
						ModFunc8((SpriteBatch)Params[0]);
						return null;
					});
				}
			}
			Action<Recipe, Item, bool> ModFunc9 = FuncFromMethod<Action<Recipe, Item, bool>>(ModObj, type, WorldHooks.CraftItem);
			if (ModFunc9 != null)
			{
				AddFunc(WorldHooks.CraftItem, delegate(object[] Params)
				{
					ModFunc9((Recipe)Params[0], (Item)Params[1], (bool)Params[2]);
					return null;
				});
			}
			Action<int> ModFunc10 = FuncFromMethod<Action<int>>(ModObj, type, WorldHooks.PlayerConnected);
			if (ModFunc10 != null)
			{
				AddFunc(WorldHooks.PlayerConnected, delegate(object[] Params)
				{
					ModFunc10((int)Params[0]);
					return null;
				});
			}
			Action ModFunc11 = FuncFromMethod<Action>(ModObj, type, WorldHooks.SyncPlayers);
			if (ModFunc11 != null)
			{
				AddFunc(WorldHooks.SyncPlayers, delegate
				{
					ModFunc11();
					return null;
				});
			}
			Func<int, int, int, bool[]> ModFunc12 = FuncFromMethod<Func<int, int, int, bool[]>>(ModObj, type, WorldHooks.MoveNPC);
			if (ModFunc12 != null)
			{
				AddFunc(WorldHooks.MoveNPC, (object[] Params) => ModFunc12((int)Params[0], (int)Params[1], (int)Params[2]));
			}
			Func<bool> ModFunc13 = FuncFromMethod<Func<bool>>(ModObj, type, WorldHooks.SaveAndQuit);
			if (ModFunc13 != null)
			{
				AddFunc(WorldHooks.SaveAndQuit, (object[] Params) => ModFunc13());
			}
			Func<bool> ModFunc14 = FuncFromMethod<Func<bool>>(ModObj, type, WorldHooks.SaveToonWhilePlaying);
			if (ModFunc14 != null)
			{
				AddFunc(WorldHooks.SaveToonWhilePlaying, (object[] Params) => ModFunc14());
			}
			Func<int, int, bool[]> ModFunc15 = FuncFromMethod<Func<int, int, bool[]>>(ModObj, type, WorldHooks.GrowEpicTree);
			if (ModFunc15 != null)
			{
				AddFunc(WorldHooks.GrowEpicTree, (object[] Params) => ModFunc15((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc16 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.GrowTree);
			if (ModFunc16 != null)
			{
				AddFunc(WorldHooks.GrowTree, (object[] Params) => ModFunc16((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc17 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.GrowShroom);
			if (ModFunc17 != null)
			{
				AddFunc(WorldHooks.GrowShroom, (object[] Params) => ModFunc17((int)Params[0], (int)Params[1]));
			}
			Func<int, int, int, int, int, bool[]> ModFunc18 = FuncFromMethod<Func<int, int, int, int, int, bool[]>>(ModObj, type, WorldHooks.EmptyTileCheck);
			if (ModFunc18 != null)
			{
				AddFunc(WorldHooks.EmptyTileCheck, (object[] Params) => ModFunc18((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3], (int)Params[4]));
			}
			Func<int, int, byte, byte, bool> ModFunc19 = FuncFromMethod<Func<int, int, byte, byte, bool>>(ModObj, type, WorldHooks.HellHouse);
			if (ModFunc19 != null)
			{
				AddFunc(WorldHooks.HellHouse, (object[] Params) => ModFunc19((int)Params[0], (int)Params[1], (byte)Params[2], (byte)Params[3]));
			}
			Func<int, int, int, int, byte, byte, bool> ModFunc20 = FuncFromMethod<Func<int, int, int, int, byte, byte, bool>>(ModObj, type, WorldHooks.HellRoom);
			if (ModFunc20 != null)
			{
				AddFunc(WorldHooks.HellRoom, (object[] Params) => ModFunc20((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3], (byte)Params[4], (byte)Params[5]));
			}
			Func<int, int, int, int, bool> ModFunc21 = FuncFromMethod<Func<int, int, int, int, bool>>(ModObj, type, WorldHooks.DungeonStairs);
			if (ModFunc21 != null)
			{
				AddFunc(WorldHooks.DungeonStairs, (object[] Params) => ModFunc21((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3]));
			}
			Func<int, int, int, int, bool, bool> ModFunc22 = FuncFromMethod<Func<int, int, int, int, bool, bool>>(ModObj, type, WorldHooks.DungeonHalls);
			if (ModFunc22 != null)
			{
				AddFunc(WorldHooks.DungeonHalls, (object[] Params) => ModFunc22((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3], (bool)Params[4]));
			}
			Func<int, int, int, int, bool> ModFunc23 = FuncFromMethod<Func<int, int, int, int, bool>>(ModObj, type, WorldHooks.DungeonRoom);
			if (ModFunc23 != null)
			{
				AddFunc(WorldHooks.DungeonRoom, (object[] Params) => ModFunc23((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3]));
			}
			Func<int, int, int, bool, int, bool[]> ModFunc24 = FuncFromMethod<Func<int, int, int, bool, int, bool[]>>(ModObj, type, WorldHooks.AddBuriedChest);
			if (ModFunc24 != null)
			{
				AddFunc(WorldHooks.AddBuriedChest, (object[] Params) => ModFunc24((int)Params[0], (int)Params[1], (int)Params[2], (bool)Params[3], (int)Params[4]));
			}
			Func<bool> ModFunc25 = FuncFromMethod<Func<bool>>(ModObj, type, WorldHooks.PlantAlch);
			if (ModFunc25 != null)
			{
				AddFunc(WorldHooks.PlantAlch, (object[] Params) => ModFunc25());
			}
			Func<int, int, bool> ModFunc26 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.HardUpdateWorld);
			if (ModFunc26 != null)
			{
				AddFunc(WorldHooks.HardUpdateWorld, (object[] Params) => ModFunc26((int)Params[0], (int)Params[1]));
			}
			Func<bool> ModFunc27 = FuncFromMethod<Func<bool>>(ModObj, type, WorldHooks.PreUpdateWorld);
			if (ModFunc27 != null)
			{
				AddFunc(WorldHooks.PreUpdateWorld, (object[] Params) => ModFunc27());
			}
			Action ModFunc28 = FuncFromMethod<Action>(ModObj, type, WorldHooks.UpdateWorld);
			if (ModFunc28 != null)
			{
				AddFunc(WorldHooks.UpdateWorld, delegate
				{
					ModFunc28();
					return null;
				});
			}
			Func<int, int, int, int, bool, bool> ModFunc29 = FuncFromMethod<Func<int, int, int, int, bool, bool>>(ModObj, type, WorldHooks.SpreadGrass);
			if (ModFunc29 != null)
			{
				AddFunc(WorldHooks.SpreadGrass, (object[] Params) => ModFunc29((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3], (bool)Params[5]));
			}
			Func<int, int, int, int, bool> ModFunc30 = FuncFromMethod<Func<int, int, int, int, bool>>(ModObj, type, WorldHooks.ChasmRunnerSideways);
			if (ModFunc30 != null)
			{
				AddFunc(WorldHooks.ChasmRunnerSideways, (object[] Params) => ModFunc30((int)Params[0], (int)Params[1], (int)Params[2], (int)Params[3]));
			}
			Func<int, int, int, bool, bool> ModFunc31 = FuncFromMethod<Func<int, int, int, bool, bool>>(ModObj, type, WorldHooks.ChasmRunner);
			if (ModFunc31 != null)
			{
				AddFunc(WorldHooks.ChasmRunner, (object[] Params) => ModFunc31((int)Params[0], (int)Params[1], (int)Params[2], (bool)Params[3]));
			}
			Func<int, int, float, int, int, bool, float, float, bool, bool, bool> ModFunc32 = FuncFromMethod<Func<int, int, float, int, int, bool, float, float, bool, bool, bool>>(ModObj, type, WorldHooks.TileRunner);
			if (ModFunc32 != null)
			{
				AddFunc(WorldHooks.TileRunner, (object[] Params) => ModFunc32((int)Params[0], (int)Params[1], (float)Params[2], (int)Params[3], (int)Params[4], (bool)Params[5], (float)Params[6], (float)Params[7], (bool)Params[8], (bool)Params[9]));
			}
			Func<int, int, bool> ModFunc33 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.MudWallRunner);
			if (ModFunc33 != null)
			{
				AddFunc(WorldHooks.MudWallRunner, (object[] Params) => ModFunc33((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc34 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.FloatingIsland);
			if (ModFunc34 != null)
			{
				AddFunc(WorldHooks.FloatingIsland, (object[] Params) => ModFunc34((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc35 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.Caverer);
			if (ModFunc35 != null)
			{
				AddFunc(WorldHooks.Caverer, (object[] Params) => ModFunc35((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc36 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.IslandHouse);
			if (ModFunc36 != null)
			{
				AddFunc(WorldHooks.IslandHouse, (object[] Params) => ModFunc36((int)Params[0], (int)Params[1]));
			}
			Func<int, int, bool> ModFunc37 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.Mountinater);
			if (ModFunc37 != null)
			{
				AddFunc(WorldHooks.Mountinater, (object[] Params) => ModFunc37((int)Params[0], (int)Params[1]));
			}
			Func<int, int, int, bool> ModFunc38 = FuncFromMethod<Func<int, int, int, bool>>(ModObj, type, WorldHooks.Cavinator);
			if (ModFunc38 != null)
			{
				AddFunc(WorldHooks.Cavinator, (object[] Params) => ModFunc38((int)Params[0], (int)Params[1], (int)Params[2]));
			}
			Func<int, int, bool> ModFunc39 = FuncFromMethod<Func<int, int, bool>>(ModObj, type, WorldHooks.CaveOpenator);
			if (ModFunc39 != null)
			{
				AddFunc(WorldHooks.CaveOpenator, (object[] Params) => ModFunc39((int)Params[0], (int)Params[1]));
			}
			Action ModFunc40 = FuncFromMethod<Action>(ModObj, type, WorldHooks.PostUpdate);
			if (ModFunc40 != null)
			{
				AddFunc(WorldHooks.PostUpdate, delegate
				{
					ModFunc40();
					return null;
				});
			}
			Action<GameTime> ModFunc41 = FuncFromMethod<Action<GameTime>>(ModObj, type, WorldHooks.PreUpdate);
			if (ModFunc41 != null)
			{
				AddFunc(WorldHooks.PreUpdate, delegate(object[] Params)
				{
					ModFunc41((GameTime)Params[0]);
					return null;
				});
			}
		}

		public static void InitModPlayer(object ModObj)
		{
			Type type = ModObj.GetType();
			Action<int> ModFunc = FuncFromMethod<Action<int>>(ModObj, type, PlayerHooks.Initialize);
			if (ModFunc != null)
			{
				AddFunc(PlayerHooks.Initialize, delegate(object[] Params)
				{
					ModFunc((int)Params[0]);
					return null;
				});
			}
		}

		public static void InitModGeneric(object ModObj)
		{
			Type type = ModObj.GetType();
			Action ModFunc = FuncFromMethod<Action>(ModObj, type, GenericHooks.PreUpdateMusic);
			if (ModFunc != null)
			{
				AddFunc(GenericHooks.PreUpdateMusic, delegate
				{
					ModFunc();
					return null;
				});
			}
			Func<SpriteBatch, string, int, byte, string[], bool[], bool[], bool> ModFunc2 = FuncFromMethod<Func<SpriteBatch, string, int, byte, string[], bool[], bool[], bool>>(ModObj, type, GenericHooks.ItemMouseText);
			if (ModFunc2 != null)
			{
				AddFunc(GenericHooks.ItemMouseText, (object[] Params) => ModFunc2((SpriteBatch)Params[0], (string)Params[1], (int)Params[2], (byte)Params[3], (string[])Params[4], (bool[])Params[5], (bool[])Params[6]));
			}
			Action ModFunc3 = FuncFromMethod<Action>(ModObj, type, GenericHooks.UpdateSpawn);
			if (ModFunc3 != null)
			{
				AddFunc(GenericHooks.UpdateSpawn, delegate
				{
					ModFunc3();
					return null;
				});
			}
		}

		public static void InitModNPC(object ModObj)
		{
			ModObj.GetType();
		}

		public static void InitModItem(object ModObj)
		{
			ModObj.GetType();
		}

		public static void InitModProjectile(object ModObj)
		{
			ModObj.GetType();
		}

		private static void AddFunc(WorldHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[0][(int)Hook][CurrentMod] = Implementation;
		}

		private static void AddFunc(PlayerHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[1][(int)Hook][CurrentMod] = Implementation;
		}

		private static void AddFunc(GenericHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[2][(int)Hook][CurrentMod] = Implementation;
		}

		private static void AddFunc(NPCHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[3][(int)Hook][CurrentMod] = Implementation;
		}

		private static void AddFunc(ItemHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[4][(int)Hook][CurrentMod] = Implementation;
		}

		private static void AddFunc(ProjectileHooks Hook, Func<object[], object> Implementation)
		{
			HookLists[5][(int)Hook][CurrentMod] = Implementation;
		}

		private static T FuncFromMethod<T>(object ModObj, Type ModObjType, object Hook) where T : class
		{
			if (ModObjType == null)
			{
				return null;
			}
			MethodInfo method = ModObjType.GetMethod(Hook.ToString(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (method == null)
			{
				return null;
			}
			Type typeFromHandle = typeof(T);
			try
			{
				if (method.IsStatic)
				{
					return Delegate.CreateDelegate(typeFromHandle, method) as T;
				}
				return Delegate.CreateDelegate(typeFromHandle, ModObj, method) as T;
			}
			catch
			{
				return null;
			}
		}

		private static void ShowError(Type FuncType, Type ModObjType, MethodInfo MI)
		{
			Type[] genericArguments = MI.GetGenericArguments();
			string str = "Error:\n" + ModObjType.Name + "." + MI.Name + " in " + LoadingMod + " implemented as\n" + MI.Name + "(";
			for (int i = 0; i < genericArguments.Length; i++)
			{
				str = ((i >= genericArguments.Length - 1) ? (str + genericArguments[i].Name) : (str + genericArguments[i].Name + ", "));
			}
			str = str + ").\nShould be implemented as\n" + MI.Name + "(";
			genericArguments = FuncType.GetGenericArguments();
			for (int j = 0; j < genericArguments.Length; j++)
			{
				str = ((j >= genericArguments.Length - 1) ? (str + genericArguments[j].Name) : (str + genericArguments[j].Name + ", "));
			}
			str += ").";
			MessageBox.Show(str);
		}

		public static bool RunMethod(WorldHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.ModWorld, Hook, Params);
		}

		public static bool RunMethod(PlayerHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.ModPlayer, Hook, Params);
		}

		public static bool RunMethod(GenericHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.ModGeneric, Hook, Params);
		}

		public static bool RunMethod(NPCHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.NPC, Hook, Params);
		}

		public static bool RunMethod(ItemHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.Item, Hook, Params);
		}

		public static bool RunMethod(ProjectileHooks Hook, params object[] Params)
		{
			return RunMethod(HookOwners.Projectile, Hook, Params);
		}

		private static bool RunMethod(HookOwners Owner, Enum Hook, params object[] Params)
		{
			int num = (int)(object)Hook;
			bool result = false;
			LastMethod = Hook;
			if (HookLists[(int)Owner][num] != null)
			{
				for (int i = 0; i < HookLists[(int)Owner][num].Length; i++)
				{
					if (HookLists[(int)Owner][num][i] != null)
					{
						ReturnValue[i] = HookLists[(int)Owner][num][i](Params);
						RefReturnValues[i] = Params;
						Codable.customMethodReturn = ReturnValue[i];
						Codable.customMethodRefReturn = Params;
						result = true;
					}
					else
					{
						ReturnValue[i] = null;
						RefReturnValues[i] = null;
					}
				}
			}
			return result;
		}

		internal static bool GetContinueMethod()
		{
			for (int i = 0; i < ReturnValue.Length; i++)
			{
				if (ReturnValue[i] != null)
				{
					if (ReturnValue[i] is bool[] && !((bool[])ReturnValue[i])[0])
					{
						return false;
					}
					if (ReturnValue[i] is bool && !(bool)ReturnValue[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		internal static bool GetMethodReturn()
		{
			for (int i = 0; i < ReturnValue.Length; i++)
			{
				if (ReturnValue[i] != null && !((bool[])ReturnValue[i])[1])
				{
					return false;
				}
			}
			return true;
		}

		public static string GetCurrentMod()
		{
			StackFrame[] frames = new StackTrace().GetFrames();
			string path = "tConfig";
			for (int i = 0; i < frames.Length; i++)
			{
				Module module = frames[i].GetMethod().Module;
				if (module != Entry.ManifestModule)
				{
					path = module.Name;
					break;
				}
			}
			return Path.GetFileNameWithoutExtension(path);
		}

		public static string GetMod(Codable Instance)
		{
			if (Instance.defs.assemblyByName.TryGetValue(Instance.name, out Assembly val))
			{
				return Path.GetFileNameWithoutExtension(val.ManifestModule.Name);
			}
			return "tConfig";
		}

		public static MethodInfo GetMethodInfo(string ModName, string Owner, string MethodName, string ObjectName = "")
		{
			Assembly assembly = Config.modDLL[ModName];
			if (assembly == null)
			{
				return null;
			}
			Type type;
			switch (Owner)
			{
			case "ModWorld":
			case "ModPlayer":
			case "ModGeneric":
				type = assembly.GetType("Terraria." + Owner);
				break;
			default:
				type = assembly.GetType("Terraria." + ObjectName + "_" + Owner);
				break;
			}
			if (type == null)
			{
				return null;
			}
			return type.GetMethod(MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		public static T GetMethod<T>(string ModName, string Owner, string MethodName, string ObjectName = "", object Instance = null) where T : class
		{
			MethodInfo methodInfo = GetMethodInfo(ModName, Owner, MethodName, ObjectName);
			if (methodInfo == null)
			{
				return null;
			}
			if (methodInfo.IsStatic)
			{
				return Delegate.CreateDelegate(typeof(T), methodInfo) as T;
			}
			return Delegate.CreateDelegate(typeof(T), Instance, methodInfo) as T;
		}

		public static T Load<T>(string FileName) where T : class
		{
			T result = null;
			try
			{
				result = Manager.Load<T>(Path.Combine(GetCurrentMod(), FileName));
				return result;
			}
			catch
			{
				return result;
			}
		}

		public static void StartShader(Effect FX)
		{
			PreBlend = Config.mainInstance.GraphicsDevice.BlendState;
			PreDepth = Config.mainInstance.GraphicsDevice.DepthStencilState;
			PreSampler = Config.mainInstance.GraphicsDevice.SamplerStates[0];
			Config.mainInstance.spriteBatch.End();
			Config.mainInstance.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, FX);
		}

		public static void EndShader()
		{
			Config.mainInstance.spriteBatch.End();
			Config.mainInstance.GraphicsDevice.BlendState = PreBlend;
			Config.mainInstance.GraphicsDevice.DepthStencilState = PreDepth;
			Config.mainInstance.GraphicsDevice.SamplerStates[0] = PreSampler;
			Config.mainInstance.spriteBatch.Begin();
		}
	}
}
