using Ionic.Zip;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Terraria
{
	public static class WorldGen
	{
		public static int c = 0;

		public static int m = 0;

		public static int a = 0;

		public static int co = 0;

		public static int ir = 0;

		public static int si = 0;

		public static int go = 0;

		public static int maxMech = 1000;

		public static int[] mechX = new int[maxMech];

		public static int[] mechY = new int[maxMech];

		public static int numMechs = 0;

		public static int[] mechTime = new int[maxMech];

		public static int maxWire = 1000;

		public static int[] wireX = new int[maxWire];

		public static int[] wireY = new int[maxWire];

		public static int numWire = 0;

		public static int[] noWireX = new int[maxWire];

		public static int[] noWireY = new int[maxWire];

		public static int numNoWire = 0;

		public static int maxPump = 20;

		public static int[] inPumpX = new int[maxPump];

		public static int[] inPumpY = new int[maxPump];

		public static int numInPump = 0;

		public static int[] outPumpX = new int[maxPump];

		public static int[] outPumpY = new int[maxPump];

		public static int numOutPump = 0;

		public static int totalEvil = 0;

		public static int totalGood = 0;

		public static int totalSolid = 0;

		public static int totalEvil2 = 0;

		public static int totalGood2 = 0;

		public static int totalSolid2 = 0;

		public static byte tEvil = 0;

		public static byte tGood = 0;

		public static int totalX = 0;

		public static int totalD = 0;

		public static bool hardLock = false;

		private static object padlock = new object();

		public static int lavaLine;

		public static int waterLine;

		public static bool noTileActions = false;

		public static bool spawnEye = false;

		public static bool gen = false;

		public static bool shadowOrbSmashed = false;

		public static int shadowOrbCount = 0;

		public static int altarCount = 0;

		public static bool spawnMeteor = false;

		public static bool loadFailed = false;

		public static bool loadSuccess = false;

		public static bool worldCleared = false;

		public static bool worldBackup = false;

		public static bool loadBackup = false;

		public static int lastMaxTilesX = 0;

		public static int lastMaxTilesY = 0;

		public static bool saveLock = false;

		public static bool mergeUp = false;

		public static bool mergeDown = false;

		public static bool mergeLeft = false;

		public static bool mergeRight = false;

		public static int tempMoonPhase = Main.moonPhase;

		public static bool tempDayTime = Main.dayTime;

		public static bool tempBloodMoon = Main.bloodMoon;

		public static double tempTime = Main.time;

		public static bool stopDrops = false;

		public static bool mudWall = false;

		public static int grassSpread = 0;

		public static bool noLiquidCheck = false;

		[ThreadStatic]
		public static Random genRand = new Random();

		public static string statusText = "";

		public static bool destroyObject = false;

		public static int spawnDelay = 0;

		public static int spawnNPC = 0;

		public static int maxRoomTiles = 1900;

		public static int numRoomTiles;

		public static int[] roomX = new int[maxRoomTiles];

		public static int[] roomY = new int[maxRoomTiles];

		public static int roomX1;

		public static int roomX2;

		public static int roomY1;

		public static int roomY2;

		public static bool canSpawn;

		public static ArrayHandler<bool> houseTile = new ArrayHandler<bool>(150);

		public static int bestX = 0;

		public static int bestY = 0;

		public static int hiScore = 0;

		public static int dungeonX;

		public static int dungeonY;

		public static Vector2 lastDungeonHall = Vector2.Zero;

		public static int maxDRooms = 100;

		public static int numDRooms = 0;

		public static int[] dRoomX = new int[maxDRooms];

		public static int[] dRoomY = new int[maxDRooms];

		public static int[] dRoomSize = new int[maxDRooms];

		public static bool[] dRoomTreasure = new bool[maxDRooms];

		public static int[] dRoomL = new int[maxDRooms];

		public static int[] dRoomR = new int[maxDRooms];

		public static int[] dRoomT = new int[maxDRooms];

		public static int[] dRoomB = new int[maxDRooms];

		public static int numDDoors;

		public static int[] DDoorX = new int[300];

		public static int[] DDoorY = new int[300];

		public static int[] DDoorPos = new int[300];

		public static int numDPlats;

		public static int[] DPlatX = new int[300];

		public static int[] DPlatY = new int[300];

		public static int[] JChestX = new int[100];

		public static int[] JChestY = new int[100];

		public static int numJChests = 0;

		public static int dEnteranceX = 0;

		public static bool dSurface = false;

		public static int dxStrength1;

		public static int dyStrength1;

		public static int dxStrength2;

		public static int dyStrength2;

		public static int dMinX;

		public static int dMaxX;

		public static int dMinY;

		public static int dMaxY;

		public static int numIslandHouses = 0;

		public static int houseCount = 0;

		public static int[] fihX = new int[300];

		public static int[] fihY = new int[300];

		public static int numMCaves = 0;

		public static int[] mCaveX = new int[300];

		public static int[] mCaveY = new int[300];

		public static int JungleX = 0;

		public static int hellChest = 0;

		public static bool roomTorch;

		public static bool roomDoor;

		public static bool roomChair;

		public static bool roomTable;

		public static bool roomOccupied;

		public static bool roomEvil;

		public static ArrayHandler<bool> loadFrameImportant;

		public static event Action TileUpdate;

		public static void ResetEvents()
		{
			WorldGen.TileUpdate = null;
		}

		public static bool MoveNPC(int x, int y, int n)
		{
			if (Codable.RunGlobalMethod("ModWorld", "MoveNPC", x, y, n) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			if (!StartRoomCheck(x, y))
			{
				Main.NewText(Lang.inter[40], byte.MaxValue, 240, 20);
				return false;
			}
			if (!RoomNeeds(spawnNPC))
			{
				if (Lang.lang <= 1)
				{
					int num = 0;
					string[] array = new string[4];
					if (!roomTorch)
					{
						array[num] = "a light source";
						num++;
					}
					if (!roomDoor)
					{
						array[num] = "a door";
						num++;
					}
					if (!roomTable)
					{
						array[num] = "a table";
						num++;
					}
					if (!roomChair)
					{
						array[num] = "a chair";
						num++;
					}
					string text = "";
					for (int i = 0; i < num; i++)
					{
						if (num == 2 && i == 1)
						{
							text += " and ";
						}
						else if (i > 0 && i != num - 1)
						{
							text += ", and ";
						}
						else if (i > 0)
						{
							text += ", ";
						}
						text += array[i];
					}
					Main.NewText("This housing is missing " + text + ".", byte.MaxValue, 240, 20);
				}
				else
				{
					Main.NewText(Lang.inter[39], byte.MaxValue, 240, 20);
				}
				return false;
			}
			ScoreRoom();
			if (hiScore <= 0)
			{
				if (roomOccupied)
				{
					Main.NewText(Lang.inter[41], byte.MaxValue, 240, 20);
				}
				else if (roomEvil)
				{
					Main.NewText(Lang.inter[42], byte.MaxValue, 240, 20);
				}
				else
				{
					Main.NewText(Lang.inter[39], byte.MaxValue, 240, 20);
				}
				return false;
			}
			return true;
		}

		public static void moveRoom(int x, int y, int n)
		{
			if (!Codable.RunGlobalMethod("ModWorld", "moveRoom", x, y, n) || (bool)Codable.customMethodReturn)
			{
				if (Main.netMode == 1)
				{
					NetMessage.SendData(60, -1, -1, "", n, x, y, 1f);
					return;
				}
				spawnNPC = Main.npc[n].type;
				Main.npc[n].homeless = true;
				SpawnNPC(x, y);
			}
		}

		public static void kickOut(int n)
		{
			if (!Codable.RunGlobalMethod("ModWorld", "kickOut", n) || (bool)Codable.customMethodReturn)
			{
				if (Main.netMode == 1)
				{
					NetMessage.SendData(60, -1, -1, "", n);
				}
				else
				{
					Main.npc[n].homeless = true;
				}
			}
		}

		public static void SpawnNPC(int x, int y)
		{
			if (Codable.RunGlobalMethod("ModWorld", "SpawnNPC", x, y) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			if (Main.wallHouse[Main.tile[x, y].wall])
			{
				canSpawn = true;
			}
			if (!canSpawn || !StartRoomCheck(x, y) || !RoomNeeds(spawnNPC))
			{
				return;
			}
			ScoreRoom();
			if (hiScore <= 0)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].active && Main.npc[i].homeless && Main.npc[i].type == spawnNPC)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				int num2 = bestX;
				int num3 = bestY;
				bool flag = true;
				Rectangle value = new Rectangle(num2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
				for (int j = 0; j < Main.player.Length; j++)
				{
					if (Main.player[j].active)
					{
						Main.player[j].UpdatePlayerRect();
						if (Main.player[j].PlayerRect.Intersects(value))
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag && (double)num3 <= Main.worldSurface)
				{
					for (int k = 1; k < 500; k++)
					{
						for (int l = 0; l < 2; l++)
						{
							num2 = ((l != 0) ? (bestX - k) : (bestX + k));
							if (num2 > 10 && num2 < Main.maxTilesX - 10)
							{
								int num4 = bestY - k;
								double num5 = bestY + k;
								if (num4 < 10)
								{
									num4 = 10;
								}
								if (num5 > Main.worldSurface)
								{
									num5 = Main.worldSurface;
								}
								for (int m = num4; (double)m < num5; m++)
								{
									num3 = m;
									if (!Main.tile[num2, num3].active || !Main.tileSolid[Main.tile[num2, num3].type])
									{
										continue;
									}
									if (Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1))
									{
										break;
									}
									flag = true;
									Rectangle value2 = new Rectangle(num2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
									for (int n = 0; n < 255; n++)
									{
										if (Main.player[n].active && new Rectangle((int)Main.player[n].position.X, (int)Main.player[n].position.Y, Main.player[n].width, Main.player[n].height).Intersects(value2))
										{
											flag = false;
											break;
										}
									}
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
				int num6 = NPC.NewNPC(num2 * 16, num3 * 16, spawnNPC, 1);
				Main.npc[num6].homeTileX = bestX;
				Main.npc[num6].homeTileY = bestY;
				if (num2 < bestX)
				{
					Main.npc[num6].direction = 1;
				}
				else if (num2 > bestX)
				{
					Main.npc[num6].direction = -1;
				}
				Main.npc[num6].netUpdate = true;
				string str = Main.npc[num6].name;
				if (Main.chrName[Main.npc[num6].type] != "")
				{
					str = ((Lang.lang > 1) ? Main.chrName[Main.npc[num6].type] : (Main.chrName[Main.npc[num6].type] + " " + Lang.the + Main.npc[num6].name));
				}
				if (Main.netMode == 0)
				{
					Main.NewText(str + " " + Lang.misc[18], 50, 125);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, str + " " + Lang.misc[18], 255, 50f, 125f, 255f);
				}
			}
			else
			{
				spawnNPC = 0;
				Main.npc[num].homeTileX = bestX;
				Main.npc[num].homeTileY = bestY;
				Main.npc[num].homeless = false;
			}
			spawnNPC = 0;
		}

		public static bool RoomNeeds(int npcType)
		{
			if (Codable.RunGlobalMethod("ModWorld", "RoomNeeds", npcType) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			roomChair = false;
			roomDoor = false;
			roomTable = false;
			roomTorch = false;
			for (int i = 150; i < 150 + Config.customTileAmt; i++)
			{
				if (houseTile[i])
				{
					if (Config.tileDefs.furniture[i] == 1)
					{
						roomChair = true;
					}
					else if (Config.tileDefs.furniture[i] == 2)
					{
						roomTable = true;
					}
					else if (Config.tileDefs.furniture[i] == 3)
					{
						roomTorch = true;
					}
					else if (Config.tileDefs.furniture[i] == 4)
					{
						roomDoor = true;
					}
				}
			}
			if (houseTile[15] || houseTile[79] || houseTile[89] || houseTile[102])
			{
				roomChair = true;
			}
			if (houseTile[14] || houseTile[18] || houseTile[87] || houseTile[88] || houseTile[90] || houseTile[101])
			{
				roomTable = true;
			}
			if (houseTile[4] || houseTile[33] || houseTile[34] || houseTile[35] || houseTile[36] || houseTile[42] || houseTile[49] || houseTile[93] || houseTile[95] || houseTile[98] || houseTile[100] || houseTile[149])
			{
				roomTorch = true;
			}
			if (houseTile[10] || houseTile[11] || houseTile[19])
			{
				roomDoor = true;
			}
			if (roomChair && roomTable && roomDoor && roomTorch)
			{
				canSpawn = true;
			}
			else
			{
				canSpawn = false;
			}
			return canSpawn;
		}

		public static void QuickFindHome(int npc)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "QuickFindHome", npc) && !(bool)Codable.customMethodReturn) || Main.npc[npc].homeTileX <= 10 || Main.npc[npc].homeTileY <= 10 || Main.npc[npc].homeTileX >= Main.maxTilesX - 10 || Main.npc[npc].homeTileY >= Main.maxTilesY)
			{
				return;
			}
			canSpawn = false;
			StartRoomCheck(Main.npc[npc].homeTileX, Main.npc[npc].homeTileY - 1);
			if (!canSpawn)
			{
				for (int i = Main.npc[npc].homeTileX - 1; i < Main.npc[npc].homeTileX + 2; i++)
				{
					for (int j = Main.npc[npc].homeTileY - 1; j < Main.npc[npc].homeTileY + 2 && !StartRoomCheck(i, j); j++)
					{
					}
				}
				int num = 10;
				for (int k = Main.npc[npc].homeTileX - num; k <= Main.npc[npc].homeTileX + num; k += 2)
				{
					for (int l = Main.npc[npc].homeTileY - num; l <= Main.npc[npc].homeTileY + num && !StartRoomCheck(k, l); l += 2)
					{
					}
				}
			}
			if (canSpawn)
			{
				RoomNeeds(Main.npc[npc].type);
				if (canSpawn)
				{
					ScoreRoom(npc);
				}
				if (canSpawn && hiScore > 0)
				{
					Main.npc[npc].homeTileX = bestX;
					Main.npc[npc].homeTileY = bestY;
					Main.npc[npc].homeless = false;
					canSpawn = false;
				}
				else
				{
					Main.npc[npc].homeless = true;
				}
			}
			else
			{
				Main.npc[npc].homeless = true;
			}
		}

		public static void ScoreRoom(int ignoreNPC = -1)
		{
			if (Codable.RunGlobalMethod("ModWorld", "ScoreRoom", ignoreNPC) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			roomOccupied = false;
			roomEvil = false;
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (!Main.npc[i].active || !Main.npc[i].townNPC || ignoreNPC == i || Main.npc[i].homeless)
				{
					continue;
				}
				for (int j = 0; j < numRoomTiles; j++)
				{
					if (Main.npc[i].homeTileX != roomX[j] || Main.npc[i].homeTileY != roomY[j])
					{
						continue;
					}
					bool flag = false;
					for (int k = 0; k < numRoomTiles; k++)
					{
						if (Main.npc[i].homeTileX == roomX[k] && Main.npc[i].homeTileY - 1 == roomY[k])
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						roomOccupied = true;
						hiScore = -1;
						return;
					}
				}
			}
			hiScore = 0;
			int num = 0;
			int num2 = 0;
			int num3 = roomX1 - Main.zoneX / 2 / 16 - 1 - Lighting.offScreenTiles;
			int num4 = roomX2 + Main.zoneX / 2 / 16 + 1 + Lighting.offScreenTiles;
			int num5 = roomY1 - Main.zoneY / 2 / 16 - 1 - Lighting.offScreenTiles;
			int num6 = roomY2 + Main.zoneY / 2 / 16 + 1 + Lighting.offScreenTiles;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 >= Main.maxTilesX)
			{
				num4 = Main.maxTilesX - 1;
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			for (int l = num3 + 1; l < num4; l++)
			{
				for (int m = num5 + 2; m < num6 + 2; m++)
				{
					if (Main.tile[l, m].active)
					{
						if (Main.tile[l, m].type == 23 || Main.tile[l, m].type == 24 || Main.tile[l, m].type == 25 || Main.tile[l, m].type == 32 || Main.tile[l, m].type == 112)
						{
							num2++;
						}
						else if (Main.tile[l, m].type == 27)
						{
							num2 -= 5;
						}
						else if (Main.tile[l, m].type == 109 || Main.tile[l, m].type == 110 || Main.tile[l, m].type == 113 || Main.tile[l, m].type == 116)
						{
							num2--;
						}
					}
				}
			}
			if (num2 < 50)
			{
				num2 = 0;
			}
			int num7 = -num2;
			if (num7 <= -250)
			{
				hiScore = num7;
				roomEvil = true;
				return;
			}
			num3 = roomX1;
			num4 = roomX2;
			num5 = roomY1;
			num6 = roomY2;
			for (int n = num3 + 1; n < num4; n++)
			{
				for (int num8 = num5 + 2; num8 < num6 + 2; num8++)
				{
					if (!Main.tile[n, num8].active)
					{
						continue;
					}
					num = num7;
					if (!Main.tileSolid[Main.tile[n, num8].type] || Main.tileSolidTop[Main.tile[n, num8].type] || Collision.SolidTiles(n - 1, n + 1, num8 - 3, num8 - 1) || !Main.tile[n - 1, num8].active || !Main.tileSolid[Main.tile[n - 1, num8].type] || !Main.tile[n + 1, num8].active || !Main.tileSolid[Main.tile[n + 1, num8].type])
					{
						continue;
					}
					for (int num9 = n - 2; num9 < n + 3; num9++)
					{
						for (int num10 = num8 - 4; num10 < num8; num10++)
						{
							if (Main.tile[num9, num10].active)
							{
								num = ((num9 != n) ? ((Main.tile[num9, num10].type != 10 && Main.tile[num9, num10].type != 11) ? ((!Main.tileSolid[Main.tile[num9, num10].type]) ? (num + 5) : (num - 5)) : (num - 20)) : (num - 15));
							}
						}
					}
					if (num <= hiScore)
					{
						continue;
					}
					bool flag2 = false;
					for (int num11 = 0; num11 < numRoomTiles; num11++)
					{
						if (roomX[num11] == n && roomY[num11] == num8)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						hiScore = num;
						bestX = n;
						bestY = num8;
					}
				}
			}
		}

		public static bool StartRoomCheck(int x, int y)
		{
			if (Codable.RunGlobalMethod("ModWorld", "StartRoomCheck", x, y) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			roomX1 = x;
			roomX2 = x;
			roomY1 = y;
			roomY2 = y;
			numRoomTiles = 0;
			for (int i = 0; i < 150 + Config.customTileAmt; i++)
			{
				houseTile[i] = false;
			}
			canSpawn = true;
			if (Main.tile[x, y].active && Main.tileSolid[Main.tile[x, y].type])
			{
				canSpawn = false;
			}
			CheckRoom(x, y);
			if (numRoomTiles < 60)
			{
				canSpawn = false;
			}
			return canSpawn;
		}

		public static void CheckRoom(int x, int y)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "CheckRoom", x, y) && !(bool)Codable.customMethodReturn) || !canSpawn)
			{
				return;
			}
			if (x < 10 || y < 10 || x >= Main.maxTilesX - 10 || y >= lastMaxTilesY - 10)
			{
				canSpawn = false;
				return;
			}
			for (int i = 0; i < numRoomTiles; i++)
			{
				if (roomX[i] == x && roomY[i] == y)
				{
					return;
				}
			}
			roomX[numRoomTiles] = x;
			roomY[numRoomTiles] = y;
			numRoomTiles++;
			if (numRoomTiles >= maxRoomTiles)
			{
				canSpawn = false;
				return;
			}
			if (Main.tile[x, y].active)
			{
				houseTile[Main.tile[x, y].type] = true;
				if (Main.tileSolid[Main.tile[x, y].type] || Main.tile[x, y].type == 11 || Config.tileDefs.furniture[Main.tile[x, y].type] == 4)
				{
					return;
				}
			}
			if (x < roomX1)
			{
				roomX1 = x;
			}
			else if (x > roomX2)
			{
				roomX2 = x;
			}
			if (y < roomY1)
			{
				roomY1 = y;
			}
			else if (y > roomY2)
			{
				roomY2 = y;
			}
			bool flag = false;
			bool flag2 = false;
			for (int j = -2; j < 3; j++)
			{
				if (Main.wallHouse[Main.tile[x + j, y].wall])
				{
					flag = true;
				}
				else if (Main.tile[x + j, y].active && (Main.tileSolid[Main.tile[x + j, y].type] || Main.tile[x + j, y].type == 11 || Config.tileDefs.furniture[Main.tile[x, y].type] == 4))
				{
					flag = true;
				}
				if (Main.wallHouse[Main.tile[x, y + j].wall])
				{
					flag2 = true;
				}
				else if (Main.tile[x, y + j].active && (Main.tileSolid[Main.tile[x, y + j].type] || Main.tile[x, y + j].type == 11 || Config.tileDefs.furniture[Main.tile[x, y].type] == 4))
				{
					flag2 = true;
				}
			}
			if (!flag || !flag2)
			{
				canSpawn = false;
				return;
			}
			for (int k = x - 1; k < x + 2; k++)
			{
				for (int l = y - 1; l < y + 2; l++)
				{
					if ((k != x || l != y) && canSpawn)
					{
						CheckRoom(k, l);
					}
				}
			}
		}

		public static void dropMeteor()
		{
			if (Codable.RunGlobalMethod("ModWorld", "dropMeteor") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			bool flag = true;
			int num = 0;
			if (Main.netMode == 1)
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					flag = false;
					break;
				}
			}
			int num2 = 0;
			int num3 = (int)(400f * ((float)Main.maxTilesX / 4200f));
			for (int j = 5; j < Main.maxTilesX - 5; j++)
			{
				for (int k = 5; (double)k < Main.worldSurface; k++)
				{
					if (Main.tile[j, k].active && Main.tile[j, k].type == 37)
					{
						num2++;
						if (num2 > num3)
						{
							return;
						}
					}
				}
			}
			while (!flag)
			{
				float num4 = (float)Main.maxTilesX * 0.08f;
				int num5 = Main.rand.Next(50, Main.maxTilesX - 50);
				while ((float)num5 > (float)Main.spawnTileX - num4 && (float)num5 < (float)Main.spawnTileX + num4)
				{
					num5 = Main.rand.Next(50, Main.maxTilesX - 50);
				}
				for (int l = Main.rand.Next(100); l < Main.maxTilesY; l++)
				{
					if (Main.tile[num5, l].active && Main.tileSolid[Main.tile[num5, l].type])
					{
						flag = meteor(num5, l);
						break;
					}
				}
				num++;
				if (num >= 100)
				{
					break;
				}
			}
		}

		public static bool meteor(int i, int j)
		{
			if (Codable.RunGlobalMethod("ModWorld", "meteor", i, j) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			if (i < 50 || i > Main.maxTilesX - 50)
			{
				return false;
			}
			if (j < 50 || j > Main.maxTilesY - 50)
			{
				return false;
			}
			int num = 25;
			Rectangle rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
			Rectangle value = default(Rectangle);
			for (int k = 0; k < Main.player.Length; k++)
			{
				if (Main.player[k].active)
				{
					value.X = (int)(Main.player[k].position.X + (float)(Main.player[k].width - NPC.sWidth) * 0.5f - (float)NPC.safeRangeX);
					value.Y = (int)(Main.player[k].position.Y + (float)(Main.player[k].height - NPC.sHeight) * 0.5f - (float)NPC.safeRangeY);
					value.Width = NPC.sWidth + NPC.safeRangeX * 2;
					value.Height = NPC.sHeight + NPC.safeRangeY * 2;
					if (rectangle.Intersects(value))
					{
						return false;
					}
				}
			}
			Rectangle value2 = default(Rectangle);
			for (int l = 0; l < Main.npc.Length; l++)
			{
				if (Main.npc[l].active)
				{
					value2.X = (int)Main.npc[l].position.X;
					value2.Y = (int)Main.npc[l].position.Y;
					value2.Width = Main.npc[l].width;
					value2.Height = Main.npc[l].height;
					if (rectangle.Intersects(value2))
					{
						return false;
					}
				}
			}
			for (int m = i - num; m < i + num; m++)
			{
				for (int n = j - num; n < j + num; n++)
				{
					if (Main.tile[m, n].active && Main.tile[m, n].type == 21)
					{
						return false;
					}
				}
			}
			stopDrops = true;
			num = 15;
			for (int num2 = i - num; num2 < i + num; num2++)
			{
				for (int num3 = j - num; num3 < j + num; num3++)
				{
					if (num3 > j + Main.rand.Next(-2, 3) - 5 && (float)(Math.Abs(i - num2) + Math.Abs(j - num3)) < (float)num * 1.5f + (float)Main.rand.Next(-5, 5))
					{
						if (!Main.tileSolid[Main.tile[num2, num3].type])
						{
							Main.tile[num2, num3].active = false;
						}
						Main.tile[num2, num3].type = 37;
					}
				}
			}
			num = 10;
			for (int num4 = i - num; num4 < i + num; num4++)
			{
				for (int num5 = j - num; num5 < j + num; num5++)
				{
					if (num5 > j + Main.rand.Next(-2, 3) - 5 && Math.Abs(i - num4) + Math.Abs(j - num5) < num + Main.rand.Next(-3, 4))
					{
						Main.tile[num4, num5].active = false;
					}
				}
			}
			num = 16;
			for (int num6 = i - num; num6 < i + num; num6++)
			{
				for (int num7 = j - num; num7 < j + num; num7++)
				{
					if (Main.tile[num6, num7].type == 5 || Main.tile[num6, num7].type == 32)
					{
						KillTile(num6, num7);
					}
					SquareTileFrame(num6, num7);
					SquareWallFrame(num6, num7);
				}
			}
			num = 23;
			for (int num8 = i - num; num8 < i + num; num8++)
			{
				for (int num9 = j - num; num9 < j + num; num9++)
				{
					if (Main.tile[num8, num9].active && Main.rand.Next(10) == 0 && (float)(Math.Abs(i - num8) + Math.Abs(j - num9)) < (float)num * 1.3f)
					{
						if (Main.tile[num8, num9].type == 5 || Main.tile[num8, num9].type == 32)
						{
							KillTile(num8, num9);
						}
						Main.tile[num8, num9].type = 37;
						SquareTileFrame(num8, num9);
					}
				}
			}
			stopDrops = false;
			if (Main.netMode == 0)
			{
				Main.NewText(Lang.gen[59], 50, byte.MaxValue, 130);
			}
			else if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, Lang.gen[59], 255, 50f, 255f, 130f);
			}
			if (Main.netMode != 1)
			{
				NetMessage.SendTileSquare(-1, i, j, 30);
			}
			return true;
		}

		public static void setWorldSize()
		{
			if (!Codable.RunGlobalMethod("ModWorld", "setWorldSize") || (bool)Codable.customMethodReturn)
			{
				Main.bottomWorld = Main.maxTilesY * 16;
				Main.rightWorld = Main.maxTilesX * 16;
				Main.maxSectionsX = Main.maxTilesX / 200;
				Main.maxSectionsY = Main.maxTilesY / 150;
			}
		}

		public static void worldGenCallBack(object threadContext)
		{
			if (!Codable.RunGlobalMethod("ModWorld", "CreateNewWorld") || (bool)Codable.customMethodReturn)
			{
				Main.PlaySound(10);
				clearWorld();
				generateWorld();
				saveWorld(resetTime: true);
				Main.LoadWorlds();
				if (Main.menuMode == 10)
				{
					Main.menuMode = 6;
				}
				Main.PlaySound(10);
			}
		}

		public static void CreateNewWorld()
		{
			ThreadPool.QueueUserWorkItem(worldGenCallBack, 1);
		}

		public static void SaveAndQuitCallBack(object threadContext)
		{
			if (!TMod.RunMethod(TMod.WorldHooks.SaveAndQuit) || TMod.GetContinueMethod())
			{
				Main.menuMode = 10;
				Main.gameMenu = true;
				Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
				if (Main.netMode == 0)
				{
					saveWorld();
					Main.PlaySound(10);
				}
				else
				{
					Netplay.disconnect = true;
					Main.netMode = 0;
				}
				Audio.Player.Stop(Audio.Player.Priorities.Override);
				Main.menuMode = 0;
			}
		}

		public static void SaveAndQuit()
		{
			Main.PlaySound(11);
			Music.Reset();
			ThreadPool.QueueUserWorkItem(SaveAndQuitCallBack, 1);
			Main.savePlayerSnapshot = true;
			Main.saveClone = Main.player[Main.myPlayer];
		}

		public static void playWorldCallBack(object threadContext)
		{
			if (Codable.RunGlobalMethod("ModWorld", "playWorld") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			for (int i = 0; i < 255; i++)
			{
				if (i != Main.myPlayer)
				{
					Main.player[i].active = false;
				}
			}
			loadWorld();
			if ((loadFailed || !loadSuccess) && (loadFailed || !loadSuccess))
			{
				worldBackup = false;
				if (!Main.dedServ)
				{
					if (worldBackup)
					{
						Main.menuMode = 200;
					}
					else
					{
						Main.menuMode = 201;
					}
					return;
				}
				if (!worldBackup)
				{
					Console.WriteLine("Load failed!  No backup found.");
					return;
				}
				File.Copy(Main.worldPathName + ".bak", Main.worldPathName, overwrite: true);
				File.Delete(Main.worldPathName + ".bak");
				loadWorld();
				if (loadFailed || !loadSuccess)
				{
					loadWorld();
					if (loadFailed || !loadSuccess)
					{
						Console.WriteLine("Load failed!");
						return;
					}
				}
			}
			EveryTileFrame();
			if (Main.gameMenu)
			{
				Main.gameMenu = false;
				OnScreenInterface.Setup();
			}
			Main.player[Main.myPlayer].Spawn();
			Main.player[Main.myPlayer].UpdatePlayer(Main.myPlayer);
			Main.dayTime = tempDayTime;
			Main.time = tempTime;
			Main.moonPhase = tempMoonPhase;
			Main.bloodMoon = tempBloodMoon;
			Main.PlaySound(11);
			Main.resetClouds = true;
		}

		public static void playWorld()
		{
			ThreadPool.QueueUserWorkItem(playWorldCallBack, 1);
		}

		public static void saveAndPlayCallBack(object threadContext)
		{
			if (!Codable.RunGlobalMethod("ModWorld", "saveAndPlay") || (bool)Codable.customMethodReturn)
			{
				saveWorld();
			}
		}

		public static void saveAndPlay()
		{
			ThreadPool.QueueUserWorkItem(saveAndPlayCallBack, 1);
			Main.savePlayerSnapshot = true;
			Main.saveClone = Main.player[Main.myPlayer];
		}

		public static void saveToonWhilePlayingCallBack(object threadContext)
		{
			if (TMod.RunMethod(TMod.WorldHooks.SaveToonWhilePlaying) && !TMod.GetContinueMethod())
			{
				Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
			}
		}

		public static void saveToonWhilePlaying()
		{
			ThreadPool.QueueUserWorkItem(saveToonWhilePlayingCallBack, 1);
		}

		public static void serverLoadWorldCallBack(object threadContext)
		{
			if (Codable.RunGlobalMethod("ModWorld", "serverLoadWorld") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			loadWorld();
			if ((loadFailed || !loadSuccess) && (loadFailed || !loadSuccess))
			{
				if (File.Exists(Main.worldPathName + ".bak"))
				{
					worldBackup = true;
				}
				else
				{
					worldBackup = false;
				}
				if (!Main.dedServ)
				{
					if (worldBackup)
					{
						Main.menuMode = 200;
					}
					else
					{
						Main.menuMode = 201;
					}
					return;
				}
				if (!worldBackup)
				{
					Console.WriteLine("Load failed!  No backup found.");
					return;
				}
				File.Copy(Main.worldPathName + ".bak", Main.worldPathName, overwrite: true);
				File.Delete(Main.worldPathName + ".bak");
				loadWorld();
				if (loadFailed || !loadSuccess)
				{
					loadWorld();
					if (loadFailed || !loadSuccess)
					{
						Console.WriteLine("Load failed!");
						return;
					}
				}
			}
			Main.PlaySound(10);
			Netplay.StartServer();
			Main.dayTime = tempDayTime;
			Main.time = tempTime;
			Main.moonPhase = tempMoonPhase;
			Main.bloodMoon = tempBloodMoon;
		}

		public static void serverLoadWorld()
		{
			ThreadPool.QueueUserWorkItem(serverLoadWorldCallBack, 1);
		}

		public static void clearWorld()
		{
			if (Codable.RunGlobalMethod("ModWorld", "clearWorld") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			totalSolid2 = 0;
			totalGood2 = 0;
			totalEvil2 = 0;
			totalSolid = 0;
			totalGood = 0;
			totalEvil = 0;
			totalX = 0;
			totalD = 0;
			tEvil = 0;
			tGood = 0;
			NPC.clrNames();
			Main.trashItem = new Item();
			spawnEye = false;
			spawnNPC = 0;
			shadowOrbCount = 0;
			altarCount = 0;
			Main.hardMode = false;
			Main.helpText = 0;
			Main.dungeonX = 0;
			Main.dungeonY = 0;
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			NPC.downedBoss3 = false;
			NPC.savedGoblin = false;
			NPC.savedWizard = false;
			NPC.savedMech = false;
			NPC.downedGoblins = false;
			NPC.downedClown = false;
			NPC.downedFrost = false;
			shadowOrbSmashed = false;
			spawnMeteor = false;
			stopDrops = false;
			Main.invasionDelay = 0;
			Main.invasionType = 0;
			Main.invasionSize = 0;
			Main.invasionWarn = 0;
			Main.invasionX = 0.0;
			noLiquidCheck = false;
			Liquid.numLiquid = 0;
			LiquidBuffer.numLiquidBuffer = 0;
			if (Main.netMode == 1 || lastMaxTilesX > Main.maxTilesX || lastMaxTilesY > Main.maxTilesY)
			{
				for (int i = 0; i < lastMaxTilesX; i++)
				{
					float num = (float)i / (float)lastMaxTilesX;
					Main.statusText = Lang.gen[46] + " " + (int)(num * 100f + 1f) + "%";
					for (int j = 0; j < lastMaxTilesY; j++)
					{
						Main.tile[i, j] = null;
					}
				}
			}
			lastMaxTilesX = Main.maxTilesX;
			lastMaxTilesY = Main.maxTilesY;
			if (Main.netMode != 1)
			{
				for (int k = 0; k < Main.maxTilesX; k++)
				{
					float num2 = (float)k / (float)Main.maxTilesX;
					Main.statusText = Lang.gen[47] + " " + (int)(num2 * 100f + 1f) + "%";
					for (int l = 0; l < Main.maxTilesY; l++)
					{
						Main.tile[k, l] = new Tile();
					}
				}
			}
			for (int m = 0; m < Main.dust.Length; m++)
			{
				Main.dust[m] = new Dust();
			}
			for (int n = 0; n < Main.gore.Length; n++)
			{
				Main.gore[n] = new Gore();
			}
			for (int num3 = 0; num3 < Main.item.Length; num3++)
			{
				Main.item[num3] = new Item();
			}
			for (int num4 = 0; num4 < Main.npc.Length; num4++)
			{
				Main.npc[num4] = new NPC();
			}
			for (int num5 = 0; num5 < Main.projectile.Length; num5++)
			{
				Main.projectile[num5] = new Projectile();
			}
			for (int num6 = 0; num6 < Main.chest.Length; num6++)
			{
				Main.chest[num6] = null;
			}
			for (int num7 = 0; num7 < Main.sign.Length; num7++)
			{
				Main.sign[num7] = null;
			}
			for (int num8 = 0; num8 < Liquid.resLiquid; num8++)
			{
				Main.liquid[num8] = new Liquid();
			}
			for (int num9 = 0; num9 < Main.liquidBuffer.Length; num9++)
			{
				Main.liquidBuffer[num9] = new LiquidBuffer();
			}
			setWorldSize();
			worldCleared = true;
		}

		public static int[] loadWorldData(MemoryStream stream)
		{
			StreamReader streamReader = new StreamReader(stream);
			string text = streamReader.ReadToEnd();
			Dictionary<string, object> dictionary = JSONHandler.LoadDict(text);
			int num = (int)dictionary["tConfig save version"];
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary["header"];
			int num2 = (int)dictionary2["baseversion"];
			Main.worldName = dictionary2["name"].ToString();
			Main.worldID = (int)dictionary2["ID"];
			ArrayList arrayList = (ArrayList)dictionary2["worldrect"];
			Main.leftWorld = (int)arrayList[0];
			Main.rightWorld = (int)arrayList[1];
			Main.topWorld = (int)arrayList[2];
			Main.bottomWorld = (int)arrayList[3];
			Main.maxTilesY = (int)dictionary2["height"];
			Main.maxTilesX = (int)dictionary2["width"];
			clearWorld();
			ArrayList arrayList2 = (ArrayList)dictionary2["spawn"];
			Main.spawnTileX = (int)arrayList2[0];
			Main.spawnTileY = (int)arrayList2[1];
			Main.worldSurface = (int)dictionary2["groundlevel"];
			Main.rockLayer = (int)dictionary2["rocklevel"];
			if (dictionary2.ContainsKey("helllayer"))
			{
				Main.hellLayer = (int)double.Parse(dictionary2["helllayer"].ToString());
			}
			else
			{
				Main.hellLayer = Main.maxTilesY - 200;
			}
			tempTime = Convert.ToSingle(dictionary2["time"], CultureInfo.InvariantCulture.NumberFormat);
			tempDayTime = Convert.ToBoolean((int)dictionary2["is_day"]);
			tempMoonPhase = (int)dictionary2["moonphase"];
			tempBloodMoon = Convert.ToBoolean((int)dictionary2["is_bloodmoon"]);
			ArrayList arrayList3 = (ArrayList)dictionary2["dungeon_xy"];
			Main.dungeonX = (int)arrayList3[0];
			Main.dungeonY = (int)arrayList3[1];
			ArrayList arrayList4 = (ArrayList)dictionary2["bosses_slain"];
			NPC.downedBoss1 = Convert.ToBoolean((int)arrayList4[0]);
			NPC.downedBoss2 = Convert.ToBoolean((int)arrayList4[1]);
			NPC.downedBoss3 = Convert.ToBoolean((int)arrayList4[2]);
			ArrayList arrayList5 = (ArrayList)dictionary2["npcs_saved"];
			NPC.savedGoblin = Convert.ToBoolean((int)arrayList5[0]);
			NPC.savedWizard = Convert.ToBoolean((int)arrayList5[1]);
			NPC.savedMech = Convert.ToBoolean((int)arrayList5[2]);
			ArrayList arrayList6 = (ArrayList)dictionary2["special_slain"];
			NPC.downedGoblins = Convert.ToBoolean((int)arrayList6[0]);
			NPC.downedClown = Convert.ToBoolean((int)arrayList6[1]);
			NPC.downedFrost = Convert.ToBoolean((int)arrayList6[2]);
			shadowOrbSmashed = Convert.ToBoolean((int)dictionary2["is_a_shadow_orb_broken"]);
			spawnMeteor = Convert.ToBoolean((int)dictionary2["is_meteor_spawned"]);
			shadowOrbCount = (int)dictionary2["shadow_orbs_broken"];
			altarCount = (int)dictionary2["altars_broken"];
			Main.hardMode = Convert.ToBoolean((int)dictionary2["hardmode"]);
			Main.invasionDelay = (int)dictionary2["gob_inv_time"];
			Main.invasionSize = (int)dictionary2["gob_inv_size"];
			Main.invasionType = (int)dictionary2["gob_inv_type"];
			Main.invasionX = Convert.ToSingle(dictionary2["gob_inv_x"], CultureInfo.InvariantCulture.NumberFormat);
			ArrayList arrayList7 = (ArrayList)dictionary["tiles"];
			loadFrameImportant = new ArrayHandler<bool>(150);
			for (int i = 0; i < arrayList7.Count; i++)
			{
				ArrayList arrayList8 = (ArrayList)arrayList7[i];
				int num3 = (int)arrayList8[0];
				string value = arrayList8[1].ToString();
				loadFrameImportant[num3] = (((int)arrayList8[2] == 1) ? true : false);
				Config.tileDefs.load[num3] = value;
			}
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)dictionary["modtiles"];
			foreach (string key6 in dictionary3.Keys)
			{
				ArrayList arrayList9 = (ArrayList)dictionary3[key6];
				foreach (int item in arrayList9)
				{
					Config.tileDefs.loadModname[item] = key6;
				}
			}
			Dictionary<string, object> dictionary4 = (Dictionary<string, object>)dictionary["modwalls"];
			foreach (string key7 in dictionary4.Keys)
			{
				ArrayList arrayList10 = (ArrayList)dictionary4[key7];
				foreach (int item2 in arrayList10)
				{
					Config.wallDefs.loadModname[item2] = key7;
				}
			}
			ArrayList arrayList11 = (ArrayList)dictionary["walls"];
			for (int j = 0; j < arrayList11.Count; j++)
			{
				ArrayList arrayList12 = (ArrayList)arrayList11[j];
				int key3 = (int)arrayList12[0];
				string value2 = arrayList12[1].ToString();
				Config.wallDefs.load[key3] = value2;
			}
			ArrayList arrayList13 = (ArrayList)dictionary["mods"];
			for (int k = 0; k < arrayList13.Count; k++)
			{
				ArrayList arrayList14 = (ArrayList)arrayList13[k];
				string key4 = arrayList14[0].ToString();
				int value3 = (int)arrayList14[1];
				Config.loadedVersion[key4] = value3;
			}
			ArrayList arrayList15 = (ArrayList)dictionary["items"];
			for (int l = 0; l < arrayList15.Count; l++)
			{
				ArrayList arrayList16 = (ArrayList)arrayList15[l];
				int key5 = (int)arrayList16[0];
				string value4 = arrayList16[1].ToString();
				Config.itemDefs.worldLoad[key5] = value4;
			}
			Dictionary<string, object> dictionary5 = (Dictionary<string, object>)dictionary["npcnames"];
			foreach (string key8 in dictionary5.Keys)
			{
				string value5 = dictionary5[key8].ToString();
				if (Config.npcDefs.byName.ContainsKey(key8))
				{
					Main.chrName[Config.npcDefs.byName[key8].type] = value5;
				}
			}
			return new int[2]
			{
				num2,
				num
			};
		}

		public static void saveWorldData(bool isDay, MemoryStream stream)
		{
			JSONDict jSONDict = new JSONDict();
			jSONDict.Add("tConfig version", Constants.version);
			jSONDict.Add("tConfig save version", Config.saveRelease);
			JSONDict jSONDict2 = new JSONDict();
			jSONDict2.Add("baseversion", 301);
			jSONDict2.Add("name", Main.worldName);
			jSONDict2.Add("ID", Main.worldID);
			jSONDict2.Add("worldrect", new JSONArray((int)Main.leftWorld, (int)Main.rightWorld, (int)Main.topWorld, (int)Main.bottomWorld));
			jSONDict2.Add("height", Main.maxTilesY);
			jSONDict2.Add("width", Main.maxTilesX);
			jSONDict2.Add("spawn", new JSONArray(Main.spawnTileX, Main.spawnTileY));
			jSONDict2.Add("groundlevel", Main.worldSurface);
			jSONDict2.Add("rocklevel", Main.rockLayer);
			jSONDict2.Add("helllayer", Main.hellLayer);
			jSONDict2.Add("time", tempTime.ToString().Replace(',', '.'), addQuotes: false);
			jSONDict2.Add("is_day", isDay);
			jSONDict2.Add("moonphase", tempMoonPhase);
			jSONDict2.Add("is_bloodmoon", tempBloodMoon);
			jSONDict2.Add("dungeon_xy", new JSONArray(Main.dungeonX, Main.dungeonY));
			jSONDict2.Add("bosses_slain", new JSONArray(NPC.downedBoss1, NPC.downedBoss2, NPC.downedBoss3));
			jSONDict2.Add("npcs_saved", new JSONArray(NPC.savedGoblin, NPC.savedWizard, NPC.savedMech));
			jSONDict2.Add("special_slain", new JSONArray(NPC.downedGoblins, NPC.downedClown, NPC.downedFrost));
			jSONDict2.Add("is_a_shadow_orb_broken", shadowOrbSmashed);
			jSONDict2.Add("is_meteor_spawned", spawnMeteor);
			jSONDict2.Add("shadow_orbs_broken", shadowOrbCount);
			jSONDict2.Add("altars_broken", altarCount);
			jSONDict2.Add("hardmode", Main.hardMode);
			jSONDict2.Add("gob_inv_time", Main.invasionDelay);
			jSONDict2.Add("gob_inv_type", Main.invasionType);
			jSONDict2.Add("gob_inv_size", Main.invasionSize);
			jSONDict2.Add("gob_inv_x", Main.invasionX.ToString().Replace(',', '.'), addQuotes: false);
			jSONDict.Add("header", jSONDict2);
			jSONDict.Add("version", 1);
			JSONArray jSONArray = new JSONArray();
			foreach (string key3 in Config.modVersion.Keys)
			{
				jSONArray.Add(new JSONArray(key3, Config.modVersion[key3]));
			}
			jSONDict.Add("mods", jSONArray);
			Dictionary<string, JSONArray> dictionary = new Dictionary<string, JSONArray>();
			Dictionary<string, JSONArray> dictionary2 = new Dictionary<string, JSONArray>();
			JSONArray jSONArray2 = new JSONArray();
			for (int i = 150; i < 150 + Config.customTileAmt; i++)
			{
				string text = Main.tileName[i];
				string key = Config.tileDefs.modName[text];
				int[] value = new int[3];
				if (!Config.tileDefs.color.TryGetValue(i, out value))
				{
					value = new int[3]
					{
						0,
						0,
						0
					};
				}
				jSONArray2.Add(new JSONArray(i, text, Main.tileFrameImportant[i], Main.tileSolid[i], new JSONArray(value[0], value[1], value[2])));
				if (!dictionary.ContainsKey(key))
				{
					dictionary[key] = new JSONArray();
				}
				dictionary[key].Add(i);
			}
			jSONDict.Add("tiles", jSONArray2);
			JSONArray jSONArray3 = new JSONArray();
			for (int j = 32; j < 32 + Config.customWallAmt; j++)
			{
				string text2 = Config.wallDefs.name[j];
				string key2 = Config.wallDefs.modName[text2];
				int[] value2 = new int[3];
				if (!Config.wallDefs.color.TryGetValue(j, out value2))
				{
					value2 = new int[3]
					{
						0,
						0,
						0
					};
				}
				jSONArray3.Add(new JSONArray(j, text2, new JSONArray(value2[0], value2[1], value2[2])));
				if (!dictionary2.ContainsKey(key2))
				{
					dictionary2[key2] = new JSONArray();
				}
				dictionary2[key2].Add(j);
			}
			jSONDict.Add("walls", jSONArray3);
			JSONDict jSONDict3 = new JSONDict();
			foreach (string key4 in dictionary.Keys)
			{
				jSONDict3.Add(key4, dictionary[key4]);
			}
			jSONDict.Add("modtiles", jSONDict3);
			JSONDict jSONDict4 = new JSONDict();
			foreach (string key5 in dictionary2.Keys)
			{
				jSONDict4.Add(key5, dictionary2[key5]);
			}
			jSONDict.Add("modwalls", jSONDict4);
			Dictionary<string, JSONArray> dictionary3 = new Dictionary<string, JSONArray>();
			JSONArray jSONArray4 = new JSONArray();
			for (int k = 604; k < 604 + Config.customItemsAmt; k++)
			{
				string name = Config.itemDefs[k].name;
				string text3 = "";
				text3 = ((!(name == "Unloaded Item")) ? Config.itemDefs.modName[name] : "Built-In");
				jSONArray4.Add(new JSONArray(k, name));
				if (!dictionary3.ContainsKey(text3))
				{
					dictionary3[text3] = new JSONArray();
				}
				dictionary3[text3].Add(k);
			}
			jSONDict.Add("items", jSONArray4);
			JSONDict jSONDict5 = new JSONDict();
			foreach (string key6 in dictionary3.Keys)
			{
				jSONDict5.Add(key6, dictionary3[key6]);
			}
			jSONDict.Add("moditems", jSONDict5);
			JSONDict jSONDict6 = new JSONDict();
			for (int l = 147; l < 147 + Config.customNPCAmt; l++)
			{
				if (!string.IsNullOrEmpty(Main.chrName[l]))
				{
					jSONDict6.Add(Config.npcDefs[l].name, Main.chrName[l]);
				}
			}
			jSONDict.Add("npcnames", jSONDict6);
			StreamWriter writer = new StreamWriter(stream);
			TextWriter.Synchronized(writer);
			string s = jSONDict.ToString();
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			byte[] bytes = uTF8Encoding.GetBytes(s);
			stream.Write(bytes, 0, bytes.Length);
		}

		public static void saveWorldZip(bool resetTime = false)
		{
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Expected O, but got Unknown
			if (!saveLock)
			{
				saveLock = true;
				while (hardLock)
				{
					Main.statusText = Lang.gen[48];
				}
				lock (padlock)
				{
					if (!Main.skipMenu)
					{
						bool isDay = Main.dayTime;
						tempTime = Main.time;
						tempMoonPhase = Main.moonPhase;
						tempBloodMoon = Main.bloodMoon;
						if (resetTime)
						{
							isDay = true;
							tempTime = 13500.0;
							tempMoonPhase = 0;
							tempBloodMoon = false;
						}
						if (Main.worldPathName != null)
						{
							Stopwatch stopwatch = new Stopwatch();
							stopwatch.Start();
							MemoryStream memoryStream = new MemoryStream();
							MemoryStream memoryStream2 = new MemoryStream();
							MemoryStream memoryStream3 = new MemoryStream();
							MemoryStream memoryStream4 = new MemoryStream();
							MemoryStream memoryStream5 = new MemoryStream();
							MemoryStream memoryStream6 = new MemoryStream();
							MemoryStream memoryStream7 = new MemoryStream();
							MemoryStream memoryStream8 = new MemoryStream();
							MemoryStream memoryStream9 = new MemoryStream();
							MemoryStream memoryStream10 = new MemoryStream();
							ZipFile val = (ZipFile)(object)new ZipFile();
							Config.SaveGlobalModData(memoryStream7, "ModWorld");
							saveWorldData(isDay, memoryStream);
							Prefix.SavePrefixNames(memoryStream10);
							saveWorldTiles(memoryStream2);
							saveWorldChests(memoryStream8, memoryStream9);
							saveWorldNPCs(memoryStream3);
							saveWorldNPCNames(memoryStream4);
							saveWorldSigns(memoryStream5);
							Config.SaveCustomTileData(memoryStream6);
							val.AddEntry("world.global.moddata", memoryStream7.ToArray());
							val.AddEntry("world.txt", memoryStream.ToArray());
							val.AddEntry("world.tiles", memoryStream2.ToArray());
							val.AddEntry("world.chests", memoryStream8.ToArray());
							val.AddEntry("world.chests.moddata", memoryStream9.ToArray());
							val.AddEntry("world.NPCs", memoryStream3.ToArray());
							val.AddEntry("world.npcnames", memoryStream4.ToArray());
							val.AddEntry("world.signs", memoryStream5.ToArray());
							val.AddEntry("world.tiles.moddata", memoryStream6.ToArray());
							val.AddEntry("world.prefixes.ini", memoryStream10.ToArray());
							memoryStream7.Dispose();
							memoryStream.Dispose();
							memoryStream2.Dispose();
							memoryStream8.Dispose();
							memoryStream9.Dispose();
							memoryStream3.Dispose();
							memoryStream4.Dispose();
							memoryStream5.Dispose();
							memoryStream6.Dispose();
							memoryStream10.Dispose();
							if (Main.worldPathName.IndexOf(".wld") > -1)
							{
								Directory.CreateDirectory(Path.Combine(Main.WorldPath, "wldbackups"));
								string[] files = Directory.GetFiles(Main.WorldPath, Path.GetFileName(Main.worldPathName) + "*");
								string[] array = files;
								foreach (string text in array)
								{
									string destFileName = Path.Combine(Main.WorldPath, "wldbackups", Path.GetFileName(text));
									File.Copy(text, destFileName, overwrite: true);
								}
								File.Delete(Main.worldPathName);
								if (File.Exists(Main.worldPathName + ".bak"))
								{
									File.Delete(Main.worldPathName + ".bak");
								}
								Config.DeleteWorld(Main.worldPathName);
								Main.LoadWorlds();
								Main.worldPathName = Main.worldName + ".zip";
							}
							else if (File.Exists(Main.worldPathName))
							{
								Main.statusText = Lang.gen[50];
								string destFileName2 = Main.worldPathName + ".bak";
								File.Copy(Main.worldPathName, destFileName2, overwrite: true);
							}
							string text2 = Main.worldName + ".zip";
							char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
							foreach (char oldChar in invalidFileNameChars)
							{
								text2 = text2.Replace(oldChar, '_');
							}
							val.Save(Path.Combine(Main.WorldPath, text2));
							val.Dispose();
						}
					}
					saveLock = false;
				}
			}
		}

		public static bool saveWorldTiles(MemoryStream fileStream)
		{
			Path.Combine(Main.worldTempPath, "world.tiles");
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				for (int i = 0; i < Main.maxTilesX; i++)
				{
					float num = (float)i / (float)Main.maxTilesX;
					Main.statusText = Lang.gen[49] + " " + (int)(num * 100f + 1f) + "%";
					int num2;
					for (num2 = 0; num2 < Main.maxTilesY; num2++)
					{
						if (Main.tile[i, num2].type == 127 && Main.tile[i, num2].active)
						{
							KillTile(i, num2);
							KillTile(i, num2);
							if (!Main.tile[i, num2].active && Main.netMode != 0)
							{
								NetMessage.SendData(17, -1, -1, "", 0, i, num2);
							}
						}
						Tile tile = (Tile)Main.tile[i, num2].Clone();
						if (!tile.active)
						{
							binaryWriter.Write((ushort)0);
						}
						else
						{
							binaryWriter.Write((ushort)(tile.type + 1));
							if (Main.tileFrameImportant[tile.type])
							{
								binaryWriter.Write(tile.frameX);
								binaryWriter.Write(tile.frameY);
								if (tile.type >= 150)
								{
									binaryWriter.Write(tile.frameNumber);
								}
							}
						}
						binaryWriter.Write(Main.tile[i, num2].wall);
						binaryWriter.Write(tile.liquid);
						if (tile.liquid > 0)
						{
							binaryWriter.Write(tile.lava);
						}
						binaryWriter.Write(tile.wire);
						int j;
						for (j = 1; num2 + j < Main.maxTilesY && tile.isTheSameAs(Main.tile[i, num2 + j]); j++)
						{
						}
						j--;
						binaryWriter.Write((short)j);
						num2 += j;
					}
				}
			}
			return true;
		}

		public static bool saveWorldChests(MemoryStream fileStream, MemoryStream modStream)
		{
			using (BinaryWriter binaryWriter2 = new BinaryWriter(fileStream))
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				ushort num = 0;
				for (int i = 0; i < Main.chest.Length; i++)
				{
					if (Main.chest[i] == null)
					{
						binaryWriter2.Write(value: false);
					}
					else
					{
						Chest chest = (Chest)Main.chest[i].Clone();
						binaryWriter2.Write(value: true);
						binaryWriter2.Write(chest.x);
						binaryWriter2.Write(chest.y);
						for (int j = 0; j < Chest.maxItems; j++)
						{
							if (chest.item[j].type == 0)
							{
								chest.item[j].stack = 0;
							}
							binaryWriter2.Write(chest.item[j].stack);
							if (chest.item[j].stack > 0)
							{
								binaryWriter2.Write(chest.item[j].netID);
								Prefix.SavePrefix(binaryWriter2, chest.item[j]);
								MemoryStream memoryStream2 = new MemoryStream();
								BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream2);
								if (Codable.SaveCustomData(chest.item[j], binaryWriter3))
								{
									num = (ushort)(num + 1);
									binaryWriter2.Write(num);
									memoryStream2.Position = 0L;
									binaryWriter.Write(memoryStream2.ToArray().Length);
									binaryWriter.Write(memoryStream2.ToArray());
								}
								else
								{
									binaryWriter2.Write((ushort)0);
								}
								binaryWriter3.Close();
								memoryStream2.Close();
							}
						}
					}
				}
				BinaryWriter binaryWriter4 = new BinaryWriter(modStream);
				memoryStream.Position = 0L;
				binaryWriter4.Write(num);
				binaryWriter4.Write(memoryStream.ToArray());
			}
			return true;
		}

		public static bool saveWorldSigns(MemoryStream fileStream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				for (int i = 0; i < Main.sign.Length; i++)
				{
					if (Main.sign[i] == null || Main.sign[i].text == null)
					{
						binaryWriter.Write(value: false);
					}
					else
					{
						Sign sign = (Sign)Main.sign[i].Clone();
						binaryWriter.Write(value: true);
						binaryWriter.Write(sign.text);
						binaryWriter.Write(sign.x);
						binaryWriter.Write(sign.y);
					}
				}
			}
			return true;
		}

		public static bool saveWorldNPCs(MemoryStream fileStream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				for (int i = 0; i < Main.npc.Length; i++)
				{
					NPC nPC = (NPC)Main.npc[i].Clone();
					if (nPC.active && nPC.townNPC)
					{
						binaryWriter.Write(value: true);
						binaryWriter.Write(nPC.name);
						binaryWriter.Write(nPC.position.X);
						binaryWriter.Write(nPC.position.Y);
						binaryWriter.Write(nPC.homeless);
						binaryWriter.Write(nPC.homeTileX);
						binaryWriter.Write(nPC.homeTileY);
					}
				}
				binaryWriter.Write(value: false);
			}
			return true;
		}

		public static bool saveWorldNPCNames(MemoryStream fileStream)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
			{
				binaryWriter.Write(Main.chrName[17]);
				binaryWriter.Write(Main.chrName[18]);
				binaryWriter.Write(Main.chrName[19]);
				binaryWriter.Write(Main.chrName[20]);
				binaryWriter.Write(Main.chrName[22]);
				binaryWriter.Write(Main.chrName[54]);
				binaryWriter.Write(Main.chrName[38]);
				binaryWriter.Write(Main.chrName[107]);
				binaryWriter.Write(Main.chrName[108]);
				binaryWriter.Write(Main.chrName[124]);
			}
			return true;
		}

		public static void saveWorld(bool resetTime = false)
		{
			if (!Codable.RunGlobalMethod("ModWorld", "saveWorld", resetTime) || (bool)Codable.customMethodReturn)
			{
				saveWorldZip(resetTime);
			}
		}

		public static void loadWorldZip()
		{
			Main.checkXMas();
			if (!File.Exists(Main.worldPathName) && Main.autoGen)
			{
				for (int num = Main.worldPathName.Length - 1; num >= 0; num--)
				{
					if (Main.worldPathName.Substring(num, 1) == string.Concat(Path.DirectorySeparatorChar))
					{
						string path = Main.worldPathName.Substring(0, num);
						Directory.CreateDirectory(path);
						break;
					}
				}
				clearWorld();
				generateWorld();
				saveWorld();
			}
			if (genRand == null)
			{
				genRand = new Random((int)DateTime.Now.Ticks);
			}
			loadFailed = false;
			loadSuccess = false;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				MemoryStream memoryStream3 = new MemoryStream();
				MemoryStream memoryStream4 = new MemoryStream();
				MemoryStream memoryStream5 = new MemoryStream();
				MemoryStream memoryStream6 = new MemoryStream();
				MemoryStream memoryStream7 = new MemoryStream();
				MemoryStream memoryStream8 = new MemoryStream();
				MemoryStream memoryStream9 = new MemoryStream();
				MemoryStream memoryStream10 = new MemoryStream();
				ZipFile val = ZipFile.Read(Main.worldPathName);
				try
				{
					foreach (ZipEntry item in val)
					{
						switch (item.FileName.ToLower())
						{
						case "world.txt":
							item.Extract((Stream)memoryStream);
							break;
						case "world.tiles":
							item.Extract((Stream)memoryStream2);
							break;
						case "world.npcs":
							item.Extract((Stream)memoryStream3);
							break;
						case "world.npcnames":
							item.Extract((Stream)memoryStream4);
							break;
						case "world.signs":
							item.Extract((Stream)memoryStream5);
							break;
						case "world.tiles.moddata":
							item.Extract((Stream)memoryStream6);
							break;
						case "world.global.moddata":
							item.Extract((Stream)memoryStream7);
							break;
						case "world.chests":
							item.Extract((Stream)memoryStream8);
							break;
						case "world.chests.moddata":
							item.Extract((Stream)memoryStream9);
							break;
						case "world.prefixes.ini":
							item.Extract((Stream)memoryStream10);
							break;
						}
					}
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
				memoryStream.Position = 0L;
				memoryStream2.Position = 0L;
				memoryStream3.Position = 0L;
				memoryStream4.Position = 0L;
				memoryStream5.Position = 0L;
				memoryStream6.Position = 0L;
				memoryStream7.Position = 0L;
				memoryStream8.Position = 0L;
				memoryStream9.Position = 0L;
				memoryStream10.Position = 0L;
				int num2 = 39;
				int num3 = 4;
				int[] array = loadWorldData(memoryStream);
				Prefix.LoadPrefixNames("world", memoryStream10);
				num2 = array[0];
				num3 = array[1];
				Config.LoadGlobalModData(memoryStream7, "ModWorld");
				loadWorldTiles(num2, memoryStream2);
				loadWorldChests(num2, memoryStream8, memoryStream9);
				loadWorldNPCs(num2, memoryStream3);
				loadWorldNPCNames(num2, memoryStream4);
				loadWorldSigns(num2, memoryStream5);
				Config.LoadCustomTileData(memoryStream6, num3);
				loadFailed = false;
				loadSuccess = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Load exception:\n" + ex);
				loadFailed = true;
				loadSuccess = false;
				Main.loadFailedMessage = ex.Message;
				return;
			}
			if (loadFailed || !loadSuccess)
			{
				return;
			}
			gen = true;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num4 = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[52] + " " + (int)(num4 * 100f + 1f) + "%";
				CountTiles(i);
			}
			waterLine = Main.maxTilesY;
			NPC.setNames();
			Liquid.QuickWater(2);
			WaterCheck();
			int num5 = 0;
			Liquid.quickSettle = true;
			int num6 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
			float num7 = 0f;
			while (Liquid.numLiquid > 0 && num5 < 100000)
			{
				num5++;
				float num8 = (float)(num6 - Liquid.numLiquid + LiquidBuffer.numLiquidBuffer) / (float)num6;
				if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num6)
				{
					num6 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				}
				if (num8 > num7)
				{
					num7 = num8;
				}
				else
				{
					num8 = num7;
				}
				Main.statusText = Lang.gen[27] + " " + (int)(num8 * 100f / 2f + 50f) + "%";
				Liquid.UpdateLiquid();
			}
			Liquid.quickSettle = false;
			WaterCheck();
			gen = false;
		}

		public static bool loadWorldTiles(int numX, MemoryStream fileStream)
		{
			Config.tileDefs.code = new Dictionary<Vector2, Codable>();
			using (BinaryReader binaryReader = new BinaryReader(fileStream))
			{
				try
				{
					Dictionary<Vector2, int> dictionary = new Dictionary<Vector2, int>();
					HashSet<int> hashSet = new HashSet<int>();
					for (int i = 0; i < Main.maxTilesX; i++)
					{
						float num = (float)i / (float)Main.maxTilesX;
						Main.statusText = Lang.gen[51] + " " + (int)(num * 100f + 1f) + "%";
						for (int j = 0; j < Main.maxTilesY; j++)
						{
							int num2 = 0;
							num2 = ((numX < 100) ? binaryReader.ReadByte() : ((ushort)binaryReader.ReadInt16()));
							if (num2 == 0)
							{
								Main.tile[i, j].active = false;
							}
							else
							{
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)(num2 - 1);
								if (hashSet.Contains(Main.tile[i, j].type))
								{
									dictionary.Add(new Vector2(i, j), Main.tile[i, j].type);
									Main.tile[i, j].type = 0;
								}
								else if (Main.tile[i, j].type >= 150)
								{
									string text = Config.tileDefs.load[Main.tile[i, j].type];
									int value = 0;
									if (!string.IsNullOrEmpty(text) && Config.tileDefs.ID.TryGetValue(text, out value))
									{
										Main.tile[i, j].type = (ushort)value;
									}
									else
									{
										string val = Main.tile[i, j].type.ToString();
										Config.tileDefs.load.TryGetValue(Main.tile[i, j].type, out val);
										string text2 = "Tile '" + val + "' does not exist";
										string value2 = "";
										if (Config.tileDefs.loadModname.TryGetValue(Main.tile[i, j].type, out value2))
										{
											text2 = text2 + "\nMod '" + value2 + "' needs to be loaded";
										}
										Main.menuMode = 203;
										Main.errorHandleChoice = 0;
										Main.loadFailedMessage = text2;
										if (string.IsNullOrEmpty(val))
										{
											dictionary.Add(new Vector2(i, j), Main.tile[i, j].type);
											Main.tile[i, j].type = 0;
										}
										else
										{
											while (Main.errorHandleChoice == 0)
											{
												if (Main.dedServ)
												{
													Console.WriteLine();
													Console.WriteLine(Main.loadFailedMessage);
													Console.WriteLine("Type 1 or 2 to select an action");
													Console.WriteLine("1 : Lose it & load anyways");
													Console.WriteLine("2 : Stop loading world");
													Console.WriteLine();
													int num3 = 0;
													while (num3 == 0)
													{
														if (int.TryParse(Console.ReadLine(), out int result) && result > 0 && result < 3)
														{
															num3 = result;
														}
													}
													Main.errorHandleChoice = num3;
												}
												else
												{
													Thread.Sleep(1);
												}
											}
										}
										if (Main.errorHandleChoice == 1)
										{
											dictionary.Add(new Vector2(i, j), Main.tile[i, j].type);
											hashSet.Add(Main.tile[i, j].type);
											Main.tile[i, j].type = 0;
											Main.menuMode = 10;
										}
										if (Main.errorHandleChoice == 2)
										{
											binaryReader.Close();
											fileStream.Close();
											throw new Exception(text2);
										}
									}
								}
								if (Main.tile[i, j].type == 127)
								{
									Main.tile[i, j].active = false;
								}
								if (dictionary.ContainsKey(new Vector2(i, j)) && loadFrameImportant[dictionary[new Vector2(i, j)]])
								{
									binaryReader.ReadInt16();
									binaryReader.ReadInt16();
									binaryReader.ReadByte();
								}
								else if (Main.tileFrameImportant[Main.tile[i, j].type])
								{
									if (numX < 28 && Main.tile[i, j].type == 4)
									{
										Main.tile[i, j].frameX = 0;
										Main.tile[i, j].frameY = 0;
									}
									else
									{
										Main.tile[i, j].frameX = binaryReader.ReadInt16();
										Main.tile[i, j].frameY = binaryReader.ReadInt16();
										if (Main.tile[i, j].type == 144)
										{
											Main.tile[i, j].frameY = 0;
										}
										if (numX >= 101 && Main.tile[i, j].type >= 150)
										{
											Main.tile[i, j].frameNumber = binaryReader.ReadByte();
										}
									}
								}
								else
								{
									Main.tile[i, j].frameX = -1;
									Main.tile[i, j].frameY = -1;
								}
								if (Config.tileDefs.assemblyByType[Main.tile[i, j].type] != null)
								{
									Codable.InitTile(new Vector2(i, j), Main.tile[i, j].type);
								}
							}
							if (numX >= 200)
							{
								Main.tile[i, j].wall = (ushort)binaryReader.ReadInt16();
							}
							else
							{
								Main.tile[i, j].wall = binaryReader.ReadByte();
							}
							if (Main.tile[i, j].wall >= 32)
							{
								string text3 = Config.wallDefs.load[Main.tile[i, j].wall];
								int value3 = 0;
								if (!string.IsNullOrEmpty(text3) && Config.wallDefs.ID.TryGetValue(text3, out value3))
								{
									Main.tile[i, j].wall = (ushort)value3;
								}
								else if (Main.tile[i, j].wall >= 32 + Config.customWallAmt || string.IsNullOrWhiteSpace(Config.wallDefs.name[Main.tile[i, j].wall]))
								{
									string val2 = Main.tile[i, j].wall.ToString();
									Config.wallDefs.load.TryGetValue(Main.tile[i, j].wall, out val2);
									string text4 = "Wall " + val2 + " does not exist";
									string value4 = "";
									if (Config.wallDefs.loadModname.TryGetValue(Main.tile[i, j].wall, out value4))
									{
										text4 = text4 + "\nMod " + value4 + " needs to be loaded";
									}
									Main.menuMode = 203;
									Main.errorHandleChoice = 0;
									Main.loadFailedMessage = text4;
									while (Main.errorHandleChoice == 0)
									{
										if (Main.dedServ)
										{
											Console.WriteLine();
											Console.WriteLine(Main.loadFailedMessage);
											Console.WriteLine("Type 1 or 2 to select an action");
											Console.WriteLine("1 : Lose it & load anyways");
											Console.WriteLine("2 : Stop loading world");
											Console.WriteLine();
											int num4 = 0;
											while (num4 == 0)
											{
												if (int.TryParse(Console.ReadLine(), out int result2) && result2 > 0 && result2 < 3)
												{
													num4 = result2;
												}
											}
											Main.errorHandleChoice = num4;
										}
										else
										{
											Thread.Sleep(1);
										}
									}
									if (Main.errorHandleChoice == 1)
									{
										Config.wallDefs.ID.Add(text3, 0);
										Main.tile[i, j].wall = 0;
										Main.menuMode = 10;
									}
									if (Main.errorHandleChoice == 2)
									{
										binaryReader.Close();
										fileStream.Close();
										throw new Exception(text4);
									}
								}
							}
							Main.tile[i, j].liquid = binaryReader.ReadByte();
							if (Main.tile[i, j].liquid > 0)
							{
								Main.tile[i, j].lava = binaryReader.ReadBoolean();
							}
							if (numX >= 33)
							{
								Main.tile[i, j].wire = binaryReader.ReadBoolean();
							}
							if (numX >= 25)
							{
								int num5 = binaryReader.ReadInt16();
								if (num5 > 0)
								{
									for (int k = j + 1; k < j + num5 + 1; k++)
									{
										Main.tile[i, k].active = Main.tile[i, j].active;
										Main.tile[i, k].type = Main.tile[i, j].type;
										Main.tile[i, k].wall = Main.tile[i, j].wall;
										Main.tile[i, k].frameX = Main.tile[i, j].frameX;
										Main.tile[i, k].frameY = Main.tile[i, j].frameY;
										if (numX >= 101)
										{
											Main.tile[i, k].frameNumber = Main.tile[i, j].frameNumber;
										}
										Main.tile[i, k].liquid = Main.tile[i, j].liquid;
										Main.tile[i, k].lava = Main.tile[i, j].lava;
										Main.tile[i, k].wire = Main.tile[i, j].wire;
									}
									j += num5;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					binaryReader.Close();
					fileStream.Close();
					throw new Exception("Error loading tiles\n" + ex.Message);
				}
				binaryReader.Close();
				fileStream.Close();
			}
			return true;
		}

		public static bool loadWorldChests(int numX, MemoryStream fileStream, MemoryStream modStream)
		{
			using (BinaryReader binaryReader2 = new BinaryReader(fileStream))
			{
				BinaryReader binaryReader = new BinaryReader(modStream);
				try
				{
					Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>();
					ushort num = binaryReader.ReadUInt16();
					for (int i = 1; i < num + 1; i++)
					{
						dictionary[i] = binaryReader.ReadBytes(binaryReader.ReadInt32());
					}
					for (int j = 0; j < Main.chest.Length; j++)
					{
						if (binaryReader2.ReadBoolean())
						{
							Main.chest[j] = new Chest();
							Main.chest[j].x = binaryReader2.ReadInt32();
							Main.chest[j].y = binaryReader2.ReadInt32();
							for (int k = 0; k < Chest.maxItems; k++)
							{
								Main.chest[j].item[k] = new Item();
								int num2 = 0;
								num2 = ((numX <= 300) ? binaryReader2.ReadByte() : binaryReader2.ReadInt32());
								if (num2 > 0)
								{
									if (numX >= 38)
									{
										int num3 = binaryReader2.ReadInt32();
										if (num3 >= 604 || num3 < -24)
										{
											string text = "";
											try
											{
												text = Config.itemDefs.worldLoad[num3];
											}
											catch (Exception)
											{
												text = "Unloaded Item";
											}
											Main.chest[j].item[k].SetDefaults(text);
										}
										else
										{
											Main.chest[j].item[k].netDefaults(num3);
										}
									}
									else
									{
										string oldName = binaryReader2.ReadString();
										string itemName = Item.VersionName(oldName, numX);
										Main.chest[j].item[k].SetDefaults(itemName);
									}
									Main.chest[j].item[k].stack = num2;
									if (numX >= 36)
									{
										Prefix.LoadPrefix(binaryReader2, Main.chest[j].item[k], "world");
									}
									int num4 = binaryReader2.ReadUInt16();
									if (num4 > 0)
									{
										MemoryStream memoryStream = new MemoryStream(dictionary[num4]);
										BinaryReader binaryReader3 = new BinaryReader(memoryStream);
										Codable.LoadCustomDataNew(Main.chest[j].item[k], binaryReader3, 5, Config.GetModVersion(Main.chest[j].item[k]));
										binaryReader3.Close();
										memoryStream.Close();
									}
								}
							}
						}
					}
				}
				catch (Exception arg)
				{
					binaryReader.Close();
					modStream.Close();
					binaryReader2.Close();
					fileStream.Close();
					throw new Exception("Error loading chests\n" + arg);
				}
				binaryReader.Close();
				modStream.Close();
				binaryReader2.Close();
				fileStream.Close();
			}
			return true;
		}

		public static bool loadWorldSigns(int numX, MemoryStream fileStream)
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream))
			{
				try
				{
					for (int i = 0; i < Main.sign.Length; i++)
					{
						if (binaryReader.ReadBoolean())
						{
							string text = binaryReader.ReadString();
							int num = binaryReader.ReadInt32();
							int num2 = binaryReader.ReadInt32();
							if (Main.tile[num, num2].active)
							{
								Main.sign[i] = new Sign();
								Main.sign[i].x = num;
								Main.sign[i].y = num2;
								Main.sign[i].text = text;
							}
						}
					}
				}
				catch (Exception arg)
				{
					binaryReader.Close();
					fileStream.Close();
					throw new Exception("Error loading signs\n" + arg);
				}
				binaryReader.Close();
				fileStream.Close();
			}
			return true;
		}

		public static bool loadWorldNPCs(int numX, MemoryStream fileStream)
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream))
			{
				try
				{
					bool flag = binaryReader.ReadBoolean();
					int num = 0;
					while (flag)
					{
						Main.npc[num].SetDefaults(binaryReader.ReadString());
						Main.npc[num].position.X = binaryReader.ReadSingle();
						Main.npc[num].position.Y = binaryReader.ReadSingle();
						Main.npc[num].homeless = binaryReader.ReadBoolean();
						Main.npc[num].homeTileX = binaryReader.ReadInt32();
						Main.npc[num].homeTileY = binaryReader.ReadInt32();
						flag = binaryReader.ReadBoolean();
						num++;
					}
				}
				catch (Exception arg)
				{
					binaryReader.Close();
					fileStream.Close();
					throw new Exception("Error loading NPCs\n" + arg);
				}
				binaryReader.Close();
				fileStream.Close();
			}
			return true;
		}

		public static bool loadWorldNPCNames(int numX, MemoryStream fileStream)
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream))
			{
				try
				{
					if (numX >= 31)
					{
						Main.chrName[17] = binaryReader.ReadString();
						Main.chrName[18] = binaryReader.ReadString();
						Main.chrName[19] = binaryReader.ReadString();
						Main.chrName[20] = binaryReader.ReadString();
						Main.chrName[22] = binaryReader.ReadString();
						Main.chrName[54] = binaryReader.ReadString();
						Main.chrName[38] = binaryReader.ReadString();
						Main.chrName[107] = binaryReader.ReadString();
						Main.chrName[108] = binaryReader.ReadString();
						if (numX >= 35)
						{
							Main.chrName[124] = binaryReader.ReadString();
						}
					}
				}
				catch (Exception arg)
				{
					binaryReader.Close();
					fileStream.Close();
					throw new Exception("Error loading NPC names\n" + arg);
				}
				binaryReader.Close();
				fileStream.Close();
			}
			return true;
		}

		public static void loadWorld()
		{
			if (Codable.RunGlobalMethod("ModWorld", "loadWorld") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			if (Main.worldPathName.IndexOf(".wld") == -1)
			{
				loadWorldZip();
				return;
			}
			Main.checkXMas();
			if (!File.Exists(Main.worldPathName) && Main.autoGen)
			{
				for (int num = Main.worldPathName.Length - 1; num >= 0; num--)
				{
					if (Main.worldPathName.Substring(num, 1) == string.Concat(Path.DirectorySeparatorChar))
					{
						string path = Main.worldPathName.Substring(0, num);
						Directory.CreateDirectory(path);
						break;
					}
				}
				clearWorld();
				generateWorld();
				saveWorld();
			}
			if (genRand == null)
			{
				genRand = new Random((int)DateTime.Now.Ticks);
			}
			using (FileStream fileStream = new FileStream(Main.worldPathName, FileMode.Open))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					try
					{
						loadFailed = false;
						loadSuccess = false;
						int num2 = binaryReader.ReadInt32();
						int num3;
						if (num2 > Main.curRelease)
						{
							loadFailed = true;
							loadSuccess = false;
							try
							{
								binaryReader.Close();
								fileStream.Close();
							}
							catch
							{
							}
						}
						else
						{
							num3 = 0;
							if (num2 >= 1376)
							{
								num3 = num2 - 1337 - 39 + 2;
								num2 = 39;
							}
							else
							{
								switch (num2)
								{
								case -1:
									num2 = 37;
									num3 = 2;
									if (File.Exists(Main.worldPathName + ".tiles.ini"))
									{
										File.Move(Main.worldPathName + ".tiles.ini", Main.worldPathName + ".IDs.ini");
									}
									break;
								case -2:
									num2 = 37;
									num3 = 1;
									if (File.Exists(Main.worldPathName + ".tiles.ini"))
									{
										File.Move(Main.worldPathName + ".tiles.ini", Main.worldPathName + ".IDs.ini");
									}
									break;
								}
							}
							Config.LoadWorldItemNames();
							Config.LoadTileNames();
							Config.LoadModVersion(Main.worldPathName);
							Config.LoadGlobalModData(Main.worldPathName, "ModWorld");
							Main.worldName = binaryReader.ReadString();
							Main.worldID = binaryReader.ReadInt32();
							Main.leftWorld = binaryReader.ReadInt32();
							Main.rightWorld = binaryReader.ReadInt32();
							Main.topWorld = binaryReader.ReadInt32();
							Main.bottomWorld = binaryReader.ReadInt32();
							Main.maxTilesY = binaryReader.ReadInt32();
							Main.maxTilesX = binaryReader.ReadInt32();
							clearWorld();
							Main.spawnTileX = binaryReader.ReadInt32();
							Main.spawnTileY = binaryReader.ReadInt32();
							Main.worldSurface = binaryReader.ReadDouble();
							Main.rockLayer = binaryReader.ReadDouble();
							Main.hellLayer = Main.maxTilesY - 200;
							tempTime = binaryReader.ReadDouble();
							tempDayTime = binaryReader.ReadBoolean();
							tempMoonPhase = binaryReader.ReadInt32();
							tempBloodMoon = binaryReader.ReadBoolean();
							Main.dungeonX = binaryReader.ReadInt32();
							Main.dungeonY = binaryReader.ReadInt32();
							NPC.downedBoss1 = binaryReader.ReadBoolean();
							NPC.downedBoss2 = binaryReader.ReadBoolean();
							NPC.downedBoss3 = binaryReader.ReadBoolean();
							if (num2 >= 29)
							{
								NPC.savedGoblin = binaryReader.ReadBoolean();
								NPC.savedWizard = binaryReader.ReadBoolean();
								if (num2 >= 34)
								{
									NPC.savedMech = binaryReader.ReadBoolean();
								}
								NPC.downedGoblins = binaryReader.ReadBoolean();
							}
							if (num2 >= 32)
							{
								NPC.downedClown = binaryReader.ReadBoolean();
							}
							if (num2 >= 37)
							{
								NPC.downedFrost = binaryReader.ReadBoolean();
							}
							shadowOrbSmashed = binaryReader.ReadBoolean();
							spawnMeteor = binaryReader.ReadBoolean();
							shadowOrbCount = binaryReader.ReadByte();
							if (num2 >= 23)
							{
								altarCount = binaryReader.ReadInt32();
								Main.hardMode = binaryReader.ReadBoolean();
							}
							Main.invasionDelay = binaryReader.ReadInt32();
							Main.invasionSize = binaryReader.ReadInt32();
							Main.invasionType = binaryReader.ReadInt32();
							Main.invasionX = binaryReader.ReadDouble();
							for (int i = 0; i < Main.maxTilesX; i++)
							{
								float num4 = (float)i / (float)Main.maxTilesX;
								Main.statusText = Lang.gen[51] + " " + (int)(num4 * 100f + 1f) + "%";
								for (int j = 0; j < Main.maxTilesY; j++)
								{
									Main.tile[i, j].active = binaryReader.ReadBoolean();
									if (Main.tile[i, j].active)
									{
										Main.tile[i, j].type = binaryReader.ReadByte();
										if (Main.tile[i, j].type >= 150)
										{
											string text = Config.tileDefs.load[Main.tile[i, j].type];
											int value = 0;
											if (!string.IsNullOrEmpty(text) && Config.tileDefs.ID.TryGetValue(text, out value))
											{
												Main.tile[i, j].type = (byte)value;
											}
											else if (Main.tile[i, j].type >= 150)
											{
												loadSuccess = false;
												loadFailed = true;
												binaryReader.Close();
												fileStream.Close();
												return;
											}
										}
										if (Main.tile[i, j].type == 127)
										{
											Main.tile[i, j].active = false;
										}
										if (Main.tileFrameImportant[Main.tile[i, j].type])
										{
											if (num2 < 28 && Main.tile[i, j].type == 4)
											{
												Main.tile[i, j].frameX = 0;
												Main.tile[i, j].frameY = 0;
											}
											else
											{
												Main.tile[i, j].frameX = binaryReader.ReadInt16();
												Main.tile[i, j].frameY = binaryReader.ReadInt16();
												if (Main.tile[i, j].type == 144)
												{
													Main.tile[i, j].frameY = 0;
												}
											}
										}
										else
										{
											Main.tile[i, j].frameX = -1;
											Main.tile[i, j].frameY = -1;
										}
										if (Config.tileDefs.assemblyByType[Main.tile[i, j].type] != null)
										{
											Codable.InitTile(new Vector2(i, j), Main.tile[i, j].type);
										}
									}
									if (num2 <= 25)
									{
										binaryReader.ReadBoolean();
									}
									if (binaryReader.ReadBoolean())
									{
										Main.tile[i, j].wall = binaryReader.ReadByte();
										if (Main.tile[i, j].wall >= 32)
										{
											string text2 = Config.wallDefs.load[Main.tile[i, j].wall];
											int value2 = 0;
											if (!string.IsNullOrEmpty(text2) && Config.wallDefs.ID.TryGetValue(text2, out value2))
											{
												Main.tile[i, j].wall = (byte)value2;
											}
											else if (Main.tile[i, j].wall >= 32 + Config.customWallAmt || string.IsNullOrWhiteSpace(Config.wallDefs.name[Main.tile[i, j].wall]))
											{
												loadSuccess = false;
												loadFailed = true;
												binaryReader.Close();
												fileStream.Close();
												return;
											}
										}
									}
									if (binaryReader.ReadBoolean())
									{
										Main.tile[i, j].liquid = binaryReader.ReadByte();
										Main.tile[i, j].lava = binaryReader.ReadBoolean();
									}
									if (num2 >= 33)
									{
										Main.tile[i, j].wire = binaryReader.ReadBoolean();
									}
									if (num2 >= 25)
									{
										int num5 = binaryReader.ReadInt16();
										if (num5 > 0)
										{
											for (int k = j + 1; k < j + num5 + 1; k++)
											{
												Main.tile[i, k].active = Main.tile[i, j].active;
												Main.tile[i, k].type = Main.tile[i, j].type;
												Main.tile[i, k].wall = Main.tile[i, j].wall;
												Main.tile[i, k].frameX = Main.tile[i, j].frameX;
												Main.tile[i, k].frameY = Main.tile[i, j].frameY;
												Main.tile[i, k].liquid = Main.tile[i, j].liquid;
												Main.tile[i, k].lava = Main.tile[i, j].lava;
												Main.tile[i, k].wire = Main.tile[i, j].wire;
											}
											j += num5;
										}
									}
								}
							}
							string path2 = Main.worldPathName + ".items.moddata";
							BinaryReader binaryReader2 = null;
							if (num3 >= 4 && File.Exists(path2))
							{
								binaryReader2 = new BinaryReader(new FileStream(path2, FileMode.Open));
							}
							for (int l = 0; l < Main.chest.Length; l++)
							{
								if (binaryReader.ReadBoolean())
								{
									Main.chest[l] = new Chest();
									Main.chest[l].x = binaryReader.ReadInt32();
									Main.chest[l].y = binaryReader.ReadInt32();
									for (int m = 0; m < Chest.maxItems; m++)
									{
										Main.chest[l].item[m] = new Item();
										byte b = binaryReader.ReadByte();
										if (b > 0)
										{
											if (num2 >= 38)
											{
												int num6 = binaryReader.ReadInt32();
												if (num3 >= 1 && (num6 >= 604 || num6 < -24))
												{
													Main.chest[l].item[m].netDefaults(Config.itemDefs.byName[Config.itemDefs.worldLoad[num6]].netID);
												}
												else
												{
													Main.chest[l].item[m].netDefaults(num6);
												}
											}
											else
											{
												string oldName = binaryReader.ReadString();
												string itemName = Item.VersionName(oldName, num2);
												Main.chest[l].item[m].SetDefaults(itemName);
											}
											Main.chest[l].item[m].stack = b;
											if (num2 >= 36)
											{
												Main.chest[l].item[m].Prefix(binaryReader.ReadByte());
											}
											if (num3 >= 4)
											{
												Codable.LoadCustomDataNew(Main.chest[l].item[m], binaryReader2, num3, Config.GetModVersion(Main.chest[l].item[m]));
											}
											else if (num3 >= 1)
											{
												Codable.LoadCustomData(Main.chest[l].item[m], binaryReader, num3);
											}
										}
									}
								}
							}
							if (num3 >= 4)
							{
								binaryReader2?.Close();
							}
							for (int n = 0; n < Main.sign.Length; n++)
							{
								if (binaryReader.ReadBoolean())
								{
									string text3 = binaryReader.ReadString();
									int num7 = binaryReader.ReadInt32();
									int num8 = binaryReader.ReadInt32();
									if (Main.tile[num7, num8].active && (Main.tile[num7, num8].type == 55 || Main.tile[num7, num8].type == 85))
									{
										Main.sign[n] = new Sign();
										Main.sign[n].x = num7;
										Main.sign[n].y = num8;
										Main.sign[n].text = text3;
									}
								}
							}
							bool flag = binaryReader.ReadBoolean();
							int num9 = 0;
							path2 = Main.worldPathName + ".NPC.moddata";
							binaryReader2 = null;
							if (num3 >= 4 && File.Exists(path2))
							{
								binaryReader2 = new BinaryReader(new FileStream(path2, FileMode.Open));
							}
							while (flag)
							{
								Main.npc[num9].SetDefaults(binaryReader.ReadString());
								Main.npc[num9].position.X = binaryReader.ReadSingle();
								Main.npc[num9].position.Y = binaryReader.ReadSingle();
								Main.npc[num9].homeless = binaryReader.ReadBoolean();
								Main.npc[num9].homeTileX = binaryReader.ReadInt32();
								Main.npc[num9].homeTileY = binaryReader.ReadInt32();
								if (Main.npc[num9].type >= 147)
								{
									if (num3 >= 4 && binaryReader2 != null)
									{
										Main.chrName[Main.npc[num9].type] = binaryReader2.ReadString();
									}
									else
									{
										Main.chrName[Main.npc[num9].type] = binaryReader.ReadString();
									}
								}
								flag = binaryReader.ReadBoolean();
								num9++;
							}
							if (num3 >= 4)
							{
								binaryReader2?.Close();
							}
							if (num2 >= 31)
							{
								Main.chrName[17] = binaryReader.ReadString();
								Main.chrName[18] = binaryReader.ReadString();
								Main.chrName[19] = binaryReader.ReadString();
								Main.chrName[20] = binaryReader.ReadString();
								Main.chrName[22] = binaryReader.ReadString();
								Main.chrName[54] = binaryReader.ReadString();
								Main.chrName[38] = binaryReader.ReadString();
								Main.chrName[107] = binaryReader.ReadString();
								Main.chrName[108] = binaryReader.ReadString();
								if (num2 >= 35)
								{
									Main.chrName[124] = binaryReader.ReadString();
								}
							}
							if (num2 < 7)
							{
								loadSuccess = true;
								goto IL_0eb1;
							}
							bool flag2 = binaryReader.ReadBoolean();
							string text4 = binaryReader.ReadString();
							int num10 = binaryReader.ReadInt32();
							if (flag2 && text4 == Main.worldName && num10 == Main.worldID)
							{
								loadSuccess = true;
								goto IL_0eb1;
							}
							loadSuccess = false;
							loadFailed = true;
							binaryReader.Close();
							fileStream.Close();
						}
						goto end_IL_00e7;
						IL_0eb1:
						binaryReader.Close();
						fileStream.Close();
						if (!loadFailed && loadSuccess)
						{
							gen = true;
							for (int num11 = 0; num11 < Main.maxTilesX; num11++)
							{
								float num12 = (float)num11 / (float)Main.maxTilesX;
								Main.statusText = Lang.gen[52] + " " + (int)(num12 * 100f + 1f) + "%";
								CountTiles(num11);
							}
							waterLine = Main.maxTilesY;
							NPC.setNames();
							Liquid.QuickWater(2);
							WaterCheck();
							int num13 = 0;
							Liquid.quickSettle = true;
							int num14 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
							float num15 = 0f;
							while (Liquid.numLiquid > 0 && num13 < 100000)
							{
								num13++;
								float num16 = (float)(num14 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num14;
								if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num14)
								{
									num14 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
								}
								if (num16 > num15)
								{
									num15 = num16;
								}
								else
								{
									num16 = num15;
								}
								Main.statusText = Lang.gen[27] + " " + (int)(num16 * 100f / 2f + 50f) + "%";
								Liquid.UpdateLiquid();
							}
							Liquid.quickSettle = false;
							WaterCheck();
							gen = false;
						}
						Config.LoadCustomTileData(Main.worldPathName + ".Tile.moddata", num3);
						end_IL_00e7:;
					}
					catch
					{
						loadFailed = true;
						loadSuccess = false;
						try
						{
							binaryReader.Close();
							fileStream.Close();
						}
						catch
						{
						}
					}
				}
			}
		}

		private static void resetGen()
		{
			if (!Codable.RunGlobalMethod("ModWorld", "resetGen") || (bool)Codable.customMethodReturn)
			{
				mudWall = false;
				hellChest = 0;
				JungleX = 0;
				numMCaves = 0;
				numIslandHouses = 0;
				houseCount = 0;
				dEnteranceX = 0;
				numDRooms = 0;
				numDDoors = 0;
				numDPlats = 0;
				numJChests = 0;
			}
		}

		public static bool placeTrap(int x2, int y2, int type = -1)
		{
			if (Codable.RunGlobalMethod("ModWorld", "placeTrap", x2, y2, type) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			int num = y2;
			while (!SolidTile(x2, num))
			{
				num++;
				if ((double)num >= Main.hellLayer - 100.0)
				{
					return false;
				}
			}
			num--;
			if (Main.tile[x2, num].liquid > 0 && Main.tile[x2, num].lava)
			{
				return false;
			}
			if (type == -1 && Main.rand.Next(20) == 0)
			{
				type = 2;
			}
			else if (type == -1)
			{
				type = Main.rand.Next(2);
			}
			if (Main.tile[x2, num].active || Main.tile[x2 - 1, num].active || Main.tile[x2 + 1, num].active || Main.tile[x2, num - 1].active || Main.tile[x2 - 1, num - 1].active || Main.tile[x2 + 1, num - 1].active || Main.tile[x2, num - 2].active || Main.tile[x2 - 1, num - 2].active || Main.tile[x2 + 1, num - 2].active)
			{
				return false;
			}
			if (Main.tile[x2, num + 1].type == 48)
			{
				return false;
			}
			if (type == 0)
			{
				int num2 = x2;
				int num3 = num;
				num3 -= genRand.Next(3);
				while (!SolidTile(num2, num3))
				{
					num2--;
				}
				int num4 = num2;
				for (num2 = x2; !SolidTile(num2, num3); num2++)
				{
				}
				int num5 = num2;
				int num6 = x2 - num4;
				int num7 = num5 - x2;
				bool flag = false;
				bool flag2 = false;
				if (num6 > 5 && num6 < 50)
				{
					flag = true;
				}
				if (num7 > 5 && num7 < 50)
				{
					flag2 = true;
				}
				if (flag && !SolidTile(num4, num3 + 1))
				{
					flag = false;
				}
				if (flag2 && !SolidTile(num5, num3 + 1))
				{
					flag2 = false;
				}
				if (flag && (Main.tile[num4, num3].type == 10 || Main.tile[num4, num3].type == 48 || Main.tile[num4, num3 + 1].type == 10 || Main.tile[num4, num3 + 1].type == 48))
				{
					flag = false;
				}
				if (flag2 && (Main.tile[num5, num3].type == 10 || Main.tile[num5, num3].type == 48 || Main.tile[num5, num3 + 1].type == 10 || Main.tile[num5, num3 + 1].type == 48))
				{
					flag2 = false;
				}
				int num8 = 0;
				if (flag && flag2)
				{
					num8 = 1;
					num2 = num4;
					if (genRand.Next(2) == 0)
					{
						num2 = num5;
						num8 = -1;
					}
				}
				else if (flag2)
				{
					num2 = num5;
					num8 = -1;
				}
				else
				{
					if (!flag)
					{
						return false;
					}
					num2 = num4;
					num8 = 1;
				}
				if (Main.tile[x2, num].wall > 0)
				{
					PlaceTile(x2, num, 135, mute: true, forced: true, -1, 2);
				}
				else
				{
					PlaceTile(x2, num, 135, mute: true, forced: true, -1, genRand.Next(2, 4));
				}
				KillTile(num2, num3);
				PlaceTile(num2, num3, 137, mute: true, forced: true, -1, num8);
				int num9 = x2;
				int num10 = num;
				while (num9 != num2 || num10 != num3)
				{
					Main.tile[num9, num10].wire = true;
					if (num9 > num2)
					{
						num9--;
					}
					else if (num9 < num2)
					{
						num9++;
					}
					Main.tile[num9, num10].wire = true;
					if (num10 > num3)
					{
						num10--;
					}
					else if (num10 < num3)
					{
						num10++;
					}
					Main.tile[num9, num10].wire = true;
				}
				return true;
			}
			if (type != 1)
			{
				if (type == 2)
				{
					int num11 = Main.rand.Next(4, 7);
					int num12 = x2 + Main.rand.Next(-1, 2);
					int num13 = num;
					for (int i = 0; i < num11; i++)
					{
						num13++;
						if (!SolidTile(num12, num13))
						{
							return false;
						}
					}
					for (int j = num12 - 2; j <= num12 + 2; j++)
					{
						for (int k = num13 - 2; k <= num13 + 2; k++)
						{
							if (!SolidTile(j, k))
							{
								return false;
							}
						}
					}
					KillTile(num12, num13);
					Main.tile[num12, num13].active = true;
					Main.tile[num12, num13].type = 141;
					Main.tile[num12, num13].frameX = 0;
					Main.tile[num12, num13].frameY = (short)(18 * Main.rand.Next(2));
					PlaceTile(x2, num, 135, mute: true, forced: true, -1, genRand.Next(2, 4));
					int num14 = x2;
					int num15 = num;
					while (num14 != num12 || num15 != num13)
					{
						Main.tile[num14, num15].wire = true;
						if (num14 > num12)
						{
							num14--;
						}
						else if (num14 < num12)
						{
							num14++;
						}
						Main.tile[num14, num15].wire = true;
						if (num15 > num13)
						{
							num15--;
						}
						else if (num15 < num13)
						{
							num15++;
						}
						Main.tile[num14, num15].wire = true;
					}
				}
				return false;
			}
			int num16 = num - 8;
			int num17 = x2 + genRand.Next(-1, 2);
			bool flag3 = true;
			while (flag3)
			{
				bool flag4 = true;
				int num18 = 0;
				for (int l = num17 - 2; l <= num17 + 3; l++)
				{
					for (int m = num16; m <= num16 + 3; m++)
					{
						if (!SolidTile(l, m))
						{
							flag4 = false;
						}
						if (Main.tile[l, m].active && (Main.tile[l, m].type == 0 || Main.tile[l, m].type == 1 || Main.tile[l, m].type == 59))
						{
							num18++;
						}
					}
				}
				num16--;
				if ((double)num16 < Main.worldSurface)
				{
					return false;
				}
				if (flag4 && num18 > 2)
				{
					flag3 = false;
				}
			}
			if (num - num16 <= 5 || num - num16 >= 40)
			{
				return false;
			}
			for (int n = num17; n <= num17 + 1; n++)
			{
				for (int num19 = num16; num19 <= num; num19++)
				{
					if (SolidTile(n, num19))
					{
						KillTile(n, num19);
					}
				}
			}
			for (int num20 = num17 - 2; num20 <= num17 + 3; num20++)
			{
				for (int num21 = num16 - 2; num21 <= num16 + 3; num21++)
				{
					if (SolidTile(num20, num21))
					{
						Main.tile[num20, num21].type = 1;
					}
				}
			}
			PlaceTile(x2, num, 135, mute: true, forced: true, -1, genRand.Next(2, 4));
			PlaceTile(num17, num16 + 2, 130, mute: true);
			PlaceTile(num17 + 1, num16 + 2, 130, mute: true);
			PlaceTile(num17 + 1, num16 + 1, 138, mute: true);
			num16 += 2;
			Main.tile[num17, num16].wire = true;
			Main.tile[num17 + 1, num16].wire = true;
			num16++;
			PlaceTile(num17, num16, 130, mute: true);
			PlaceTile(num17 + 1, num16, 130, mute: true);
			Main.tile[num17, num16].wire = true;
			Main.tile[num17 + 1, num16].wire = true;
			PlaceTile(num17, num16 + 1, 130, mute: true);
			PlaceTile(num17 + 1, num16 + 1, 130, mute: true);
			Main.tile[num17, num16 + 1].wire = true;
			Main.tile[num17 + 1, num16 + 1].wire = true;
			int num22 = x2;
			int num23 = num;
			while (num22 != num17 || num23 != num16)
			{
				Main.tile[num22, num23].wire = true;
				if (num22 > num17)
				{
					num22--;
				}
				else if (num22 < num17)
				{
					num22++;
				}
				Main.tile[num22, num23].wire = true;
				if (num23 > num16)
				{
					num23--;
				}
				else if (num23 < num16)
				{
					num23++;
				}
				Main.tile[num22, num23].wire = true;
			}
			return true;
		}

		public static void generateWorld(int seed = -1)
		{
			Main.checkXMas();
			NPC.clrNames();
			NPC.setNames();
			gen = true;
			resetGen();
			if (seed > 0)
			{
				genRand = new Random(seed);
			}
			else
			{
				genRand = new Random((int)DateTime.Now.Ticks);
			}
			Main.worldID = genRand.Next(int.MaxValue);
			Config.InitializeGlobalMod("ModWorld");
			if (Codable.RunGlobalMethod("ModWorld", "GenerateWorld"))
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			float num3 = (float)Main.maxTilesY * 0.3f * (float)genRand.Next(90, 110) * 0.005f;
			float num4 = num3 + (float)Main.maxTilesY * 0.2f * (float)genRand.Next(90, 110) * 0.01f;
			float num5 = num3;
			float num6 = num3;
			float num7 = num4;
			float num8 = num4;
			int num9 = 0;
			num9 = ((genRand.Next(2) != 0) ? 1 : (-1));
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num10 = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[0] + " " + (int)(num10 * 100f + 1f) + "%";
				if (num3 < num5)
				{
					num5 = num3;
				}
				if (num3 > num6)
				{
					num6 = num3;
				}
				if (num4 < num7)
				{
					num7 = num4;
				}
				if (num4 > num8)
				{
					num8 = num4;
				}
				if (num2 <= 0)
				{
					num = genRand.Next(0, 5);
					num2 = genRand.Next(5, 40);
					if (num == 0)
					{
						num2 *= (int)((float)genRand.Next(5, 30) * 0.2f);
					}
				}
				num2--;
				if (num == 0)
				{
					while (genRand.Next(0, 7) == 0)
					{
						num3 += (float)genRand.Next(-1, 2);
					}
				}
				else if (num == 1)
				{
					while (genRand.Next(0, 4) == 0)
					{
						num3 -= 1f;
					}
					while (genRand.Next(0, 10) == 0)
					{
						num3 += 1f;
					}
				}
				else if (num == 2)
				{
					while (genRand.Next(0, 4) == 0)
					{
						num3 += 1f;
					}
					while (genRand.Next(0, 10) == 0)
					{
						num3 -= 1f;
					}
				}
				else if (num == 3)
				{
					while (genRand.Next(0, 2) == 0)
					{
						num3 -= 1f;
					}
					while (genRand.Next(0, 6) == 0)
					{
						num3 += 1f;
					}
				}
				else if (num == 4)
				{
					while (genRand.Next(0, 2) == 0)
					{
						num3 += 1f;
					}
					while (genRand.Next(0, 5) == 0)
					{
						num3 -= 1f;
					}
				}
				if (num3 < (float)Main.maxTilesY * 0.17f)
				{
					num3 = (float)Main.maxTilesY * 0.17f;
					num2 = 0;
				}
				else if (num3 > (float)Main.maxTilesY * 0.3f)
				{
					num3 = (float)Main.maxTilesY * 0.3f;
					num2 = 0;
				}
				if ((i < 275 || i > Main.maxTilesX - 275) && num3 > (float)Main.maxTilesY * 0.25f)
				{
					num3 = (float)Main.maxTilesY * 0.25f;
					num2 = 1;
				}
				while (genRand.Next(0, 3) == 0)
				{
					num4 += (float)genRand.Next(-2, 3);
				}
				if (num4 < num3 + (float)Main.maxTilesY * 0.05f)
				{
					num4 += 1f;
				}
				if (num4 > num3 + (float)Main.maxTilesY * 0.35f)
				{
					num4 -= 1f;
				}
				for (int j = 0; (float)j < num3; j++)
				{
					Main.tile[i, j].active = false;
					Main.tile[i, j].frameX = -1;
					Main.tile[i, j].frameY = -1;
				}
				for (int k = (int)num3; k < Main.maxTilesY; k++)
				{
					if ((float)k < num4)
					{
						Main.tile[i, k].active = true;
						Main.tile[i, k].type = 0;
						Main.tile[i, k].frameX = -1;
						Main.tile[i, k].frameY = -1;
					}
					else
					{
						Main.tile[i, k].active = true;
						Main.tile[i, k].type = 1;
						Main.tile[i, k].frameX = -1;
						Main.tile[i, k].frameY = -1;
					}
				}
			}
			Main.worldSurface = num6 + 25f;
			Main.rockLayer = Main.worldSurface + (double)((int)(((double)num8 - Main.worldSurface) / 6.0) * 6);
			Main.hellLayer = Main.maxTilesY - 200;
			waterLine = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2;
			waterLine += genRand.Next(-100, 20);
			lavaLine = waterLine + genRand.Next(50, 80);
			int num11 = 0;
			int num12 = (int)((float)Main.maxTilesX * 0.0015f);
			for (int l = 0; l < num12; l++)
			{
				int[] array = new int[10];
				int[] array2 = new int[10];
				int num13 = genRand.Next(450, Main.maxTilesX - 450);
				int m = 0;
				for (int n = 0; n < 10; n++)
				{
					for (; !Main.tile[num13, m].active; m++)
					{
					}
					array[n] = num13;
					array2[n] = m - genRand.Next(11, 16);
					num13 += genRand.Next(5, 11);
				}
				for (int num14 = 0; num14 < 10; num14++)
				{
					TileRunner(array[num14], array2[num14], genRand.Next(5, 8), genRand.Next(6, 9), 0, addTile: true, -2f, -0.3f);
					TileRunner(array[num14], array2[num14], genRand.Next(5, 8), genRand.Next(6, 9), 0, addTile: true, 2f, -0.3f);
				}
			}
			Main.statusText = Lang.gen[1];
			int num15 = genRand.Next((int)((float)Main.maxTilesX * 0.0008f), (int)((float)Main.maxTilesX * 0.0025f));
			num15 += 2;
			for (int num16 = 0; num16 < num15; num16++)
			{
				int num17 = genRand.Next(Main.maxTilesX);
				while ((float)num17 > (float)Main.maxTilesX * 0.4f && (float)num17 < (float)Main.maxTilesX * 0.6f)
				{
					num17 = genRand.Next(Main.maxTilesX);
				}
				int num18 = genRand.Next(35, 90);
				if (num16 == 1)
				{
					num18 += genRand.Next(20, 40) * Main.maxTilesX / 4200;
				}
				if (genRand.Next(3) == 0)
				{
					num18 *= 2;
				}
				if (num16 == 1)
				{
					num18 *= 2;
				}
				int num19 = num17 - num18;
				num18 = genRand.Next(35, 90);
				if (genRand.Next(3) == 0)
				{
					num18 *= 2;
				}
				if (num16 == 1)
				{
					num18 *= 2;
				}
				int num20 = num17 + num18;
				if (num19 < 0)
				{
					num19 = 0;
				}
				if (num20 > Main.maxTilesX)
				{
					num20 = Main.maxTilesX;
				}
				switch (num16)
				{
				case 0:
					num19 = 0;
					num20 = genRand.Next(260, 300);
					if (num9 == 1)
					{
						num20 += 40;
					}
					break;
				case 2:
					num19 = Main.maxTilesX - genRand.Next(260, 300);
					num20 = Main.maxTilesX;
					if (num9 == -1)
					{
						num19 -= 40;
					}
					break;
				}
				int num21 = genRand.Next(50, 100);
				for (int num22 = num19; num22 < num20; num22++)
				{
					if (genRand.Next(2) == 0)
					{
						num21 += genRand.Next(-1, 2);
						if (num21 < 50)
						{
							num21 = 50;
						}
						if (num21 > 100)
						{
							num21 = 100;
						}
					}
					for (int num23 = 0; (double)num23 < Main.worldSurface; num23++)
					{
						if (!Main.tile[num22, num23].active)
						{
							continue;
						}
						int num24 = num21;
						if (num22 - num19 < num24)
						{
							num24 = num22 - num19;
						}
						if (num20 - num22 < num24)
						{
							num24 = num20 - num22;
						}
						num24 += genRand.Next(5);
						for (int num25 = num23; num25 < num23 + num24; num25++)
						{
							if (num22 > num19 + genRand.Next(5) && num22 < num20 - genRand.Next(5))
							{
								Main.tile[num22, num25].type = 53;
							}
						}
						break;
					}
				}
			}
			for (int num26 = 0; num26 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 8E-06f); num26++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)Main.worldSurface, (int)Main.rockLayer), genRand.Next(15, 70), genRand.Next(20, 130), 53);
			}
			numMCaves = 0;
			Main.statusText = Lang.gen[2];
			for (int num27 = 0; num27 < (int)((float)Main.maxTilesX * 0.0008f); num27++)
			{
				int num28 = 0;
				bool flag = false;
				bool flag2 = false;
				int num29 = genRand.Next((int)((float)Main.maxTilesX * 0.25f), (int)((float)Main.maxTilesX * 0.75f));
				while (!flag2)
				{
					flag2 = true;
					while (num29 > Main.maxTilesX / 2 - 100 && num29 < Main.maxTilesX / 2 + 100)
					{
						num29 = genRand.Next((int)((float)Main.maxTilesX * 0.25f), (int)((float)Main.maxTilesX * 0.75f));
					}
					for (int num30 = 0; num30 < numMCaves; num30++)
					{
						if (num29 > mCaveX[num30] - 50 && num29 < mCaveX[num30] + 50)
						{
							num28++;
							flag2 = false;
							break;
						}
					}
					if (num28 >= 200)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					continue;
				}
				for (int num31 = 0; (double)num31 < Main.worldSurface; num31++)
				{
					if (Main.tile[num29, num31].active)
					{
						Mountinater(num29, num31);
						mCaveX[numMCaves] = num29;
						mCaveY[numMCaves] = num31;
						numMCaves++;
						break;
					}
				}
			}
			bool flag3 = false;
			if (Main.xMas)
			{
				flag3 = true;
			}
			else if (genRand.Next(3) == 0)
			{
				flag3 = true;
			}
			if (flag3)
			{
				Main.statusText = Lang.gen[56];
				int num32 = genRand.Next(Main.maxTilesX);
				while ((float)num32 < (float)Main.maxTilesX * 0.35f || (float)num32 > (float)Main.maxTilesX * 0.65f)
				{
					num32 = genRand.Next(Main.maxTilesX);
				}
				int num33 = genRand.Next(35, 90);
				float num34 = Main.maxTilesX / 4200;
				num33 += (int)((float)genRand.Next(20, 40) * num34);
				num33 += (int)((float)genRand.Next(20, 40) * num34);
				int num35 = num32 - num33;
				num33 = genRand.Next(35, 90);
				num33 += (int)((float)genRand.Next(20, 40) * num34);
				num33 += (int)((float)genRand.Next(20, 40) * num34);
				int num36 = num32 + num33;
				if (num35 < 0)
				{
					num35 = 0;
				}
				if (num36 > Main.maxTilesX)
				{
					num36 = Main.maxTilesX;
				}
				int num37 = genRand.Next(50, 100);
				for (int num38 = num35; num38 < num36; num38++)
				{
					if (genRand.Next(2) == 0)
					{
						num37 += genRand.Next(-1, 2);
						if (num37 < 50)
						{
							num37 = 50;
						}
						if (num37 > 100)
						{
							num37 = 100;
						}
					}
					for (int num39 = 0; (double)num39 < Main.worldSurface; num39++)
					{
						if (!Main.tile[num38, num39].active)
						{
							continue;
						}
						int num40 = num37;
						if (num38 - num35 < num40)
						{
							num40 = num38 - num35;
						}
						if (num36 - num38 < num40)
						{
							num40 = num36 - num38;
						}
						num40 += genRand.Next(5);
						for (int num41 = num39; num41 < num39 + num40; num41++)
						{
							if (num38 > num35 + genRand.Next(5) && num38 < num36 - genRand.Next(5))
							{
								Main.tile[num38, num41].type = 147;
							}
						}
						break;
					}
				}
			}
			for (int num42 = 1; num42 < Main.maxTilesX - 1; num42++)
			{
				float num43 = (float)num42 / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[3] + " " + (int)(num43 * 100f + 1f) + "%";
				bool flag4 = false;
				num11 += genRand.Next(-1, 2);
				if (num11 < 0)
				{
					num11 = 0;
				}
				else if (num11 > 10)
				{
					num11 = 10;
				}
				for (int num44 = 0; (double)num44 < Main.worldSurface + 10.0 && (double)num44 <= Main.worldSurface + (double)num11; num44++)
				{
					if (flag4)
					{
						Main.tile[num42, num44].wall = 2;
					}
					if (Main.tile[num42, num44].active && Main.tile[num42 - 1, num44].active && Main.tile[num42 + 1, num44].active && Main.tile[num42, num44 + 1].active && Main.tile[num42 - 1, num44 + 1].active && Main.tile[num42 + 1, num44 + 1].active)
					{
						flag4 = true;
					}
				}
			}
			Main.statusText = Lang.gen[4];
			for (int num45 = 0; num45 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2E-05f); num45++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next(0, (int)num5 + 1), genRand.Next(4, 15), genRand.Next(5, 40), 1);
			}
			for (int num46 = 0; num46 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2E-05f); num46++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num5, (int)num6 + 1), genRand.Next(4, 10), genRand.Next(5, 30), 1);
			}
			for (int num47 = 0; num47 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 4.5E-05f); num47++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, (int)num8 + 1), genRand.Next(2, 7), genRand.Next(2, 23), 1);
			}
			Main.statusText = Lang.gen[5];
			for (int num48 = 0; num48 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 5E-05f); num48++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(2, 6), genRand.Next(2, 40), 0);
			}
			Main.statusText = Lang.gen[6];
			for (int num49 = 0; num49 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2E-05f); num49++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next(0, (int)num5), genRand.Next(4, 14), genRand.Next(10, 50), 40);
			}
			for (int num50 = 0; num50 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 5E-05f); num50++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num5, (int)num6 + 1), genRand.Next(8, 14), genRand.Next(15, 45), 40);
			}
			for (int num51 = 0; num51 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2E-05f); num51++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, (int)num8 + 1), genRand.Next(8, 15), genRand.Next(5, 50), 40);
			}
			for (int num52 = 5; num52 < Main.maxTilesX - 5; num52++)
			{
				for (int num53 = 1; (double)num53 < Main.worldSurface - 1.0; num53++)
				{
					if (!Main.tile[num52, num53].active)
					{
						continue;
					}
					for (int num54 = num53; num54 < num53 + 5; num54++)
					{
						if (Main.tile[num52, num54].type == 40)
						{
							Main.tile[num52, num54].type = 0;
						}
					}
					break;
				}
			}
			int num55 = 0;
			for (int num56 = 0; num56 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0015f); num56++)
			{
				float num57 = (float)num56 / ((float)(Main.maxTilesX * Main.maxTilesY) * 0.0015f);
				Main.statusText = Lang.gen[7] + " " + (int)(num57 * 100f + 1f) + "%";
				int type = -1;
				if (genRand.Next(5) == 0)
				{
					type = -2;
				}
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, Main.maxTilesY), genRand.Next(2, 5), genRand.Next(2, 20), type);
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, Main.maxTilesY), genRand.Next(8, 15), genRand.Next(7, 30), type);
			}
			for (int num58 = 0; num58 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 3E-05f); num58++)
			{
				float num59 = (float)num58 / ((float)(Main.maxTilesX * Main.maxTilesY) * 3E-05f);
				Main.statusText = Lang.gen[8] + " " + (int)(num59 * 100f + 1f) + "%";
				if (num8 <= (float)Main.maxTilesY)
				{
					int type2 = -1;
					if (genRand.Next(6) == 0)
					{
						type2 = -2;
					}
					TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num5, (int)num8 + 1), genRand.Next(5, 15), genRand.Next(30, 200), type2);
				}
			}
			for (int num60 = 0; num60 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00013f); num60++)
			{
				float num61 = (float)num60 / ((float)(Main.maxTilesX * Main.maxTilesY) * 0.00013f);
				Main.statusText = Lang.gen[9] + " " + (int)(num61 * 100f + 1f) + "%";
				if (num8 <= (float)Main.maxTilesY)
				{
					int type3 = -1;
					if (genRand.Next(10) == 0)
					{
						type3 = -2;
					}
					TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num8, Main.maxTilesY), genRand.Next(6, 20), genRand.Next(50, 300), type3);
				}
			}
			Main.statusText = Lang.gen[10];
			for (int num62 = 0; num62 < (int)((float)Main.maxTilesX * 0.0025f); num62++)
			{
				num55 = genRand.Next(0, Main.maxTilesX);
				for (int num63 = 0; (float)num63 < num6; num63++)
				{
					if (Main.tile[num55, num63].active)
					{
						TileRunner(num55, num63, genRand.Next(3, 6), genRand.Next(5, 50), -1, addTile: false, (float)genRand.Next(-10, 11) * 0.1f, 1f);
						break;
					}
				}
			}
			for (int num64 = 0; num64 < (int)((float)Main.maxTilesX * 0.0007f); num64++)
			{
				num55 = genRand.Next(0, Main.maxTilesX);
				for (int num65 = 0; (float)num65 < num6; num65++)
				{
					if (Main.tile[num55, num65].active)
					{
						TileRunner(num55, num65, genRand.Next(10, 15), genRand.Next(50, 130), -1, addTile: false, (float)genRand.Next(-10, 11) * 0.1f, 2f);
						break;
					}
				}
			}
			for (int num66 = 0; num66 < (int)((float)Main.maxTilesX * 0.0003f); num66++)
			{
				num55 = genRand.Next(0, Main.maxTilesX);
				for (int num67 = 0; (float)num67 < num6; num67++)
				{
					if (Main.tile[num55, num67].active)
					{
						TileRunner(num55, num67, genRand.Next(12, 25), genRand.Next(150, 500), -1, addTile: false, (float)genRand.Next(-10, 11) * 0.1f, 4f);
						TileRunner(num55, num67, genRand.Next(8, 17), genRand.Next(60, 200), -1, addTile: false, (float)genRand.Next(-10, 11) * 0.1f, 2f);
						TileRunner(num55, num67, genRand.Next(5, 13), genRand.Next(40, 170), -1, addTile: false, (float)genRand.Next(-10, 11) * 0.1f, 2f);
						break;
					}
				}
			}
			for (int num68 = 0; num68 < (int)((float)Main.maxTilesX * 0.0004f); num68++)
			{
				num55 = genRand.Next(0, Main.maxTilesX);
				for (int num69 = 0; (float)num69 < num6; num69++)
				{
					if (Main.tile[num55, num69].active)
					{
						TileRunner(num55, num69, genRand.Next(7, 12), genRand.Next(150, 250), -1, addTile: false, 0f, 1f, noYChange: true);
						break;
					}
				}
			}
			float num70 = Main.maxTilesX / 4200;
			for (int num71 = 0; (float)num71 < 5f * num70; num71++)
			{
				try
				{
					Caverer(genRand.Next(100, Main.maxTilesX - 100), genRand.Next((int)Main.rockLayer, (int)Main.hellLayer - 200));
				}
				catch
				{
				}
			}
			for (int num72 = 0; num72 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.002f); num72++)
			{
				int num73 = genRand.Next(1, Main.maxTilesX - 1);
				int num74 = genRand.Next((int)num5, (int)num6);
				if (num74 >= Main.maxTilesY)
				{
					num74 = Main.maxTilesY - 2;
				}
				if (Main.tile[num73 - 1, num74].active && Main.tile[num73 - 1, num74].type == 0 && Main.tile[num73 + 1, num74].active && Main.tile[num73 + 1, num74].type == 0 && Main.tile[num73, num74 - 1].active && Main.tile[num73, num74 - 1].type == 0 && Main.tile[num73, num74 + 1].active && Main.tile[num73, num74 + 1].type == 0)
				{
					Main.tile[num73, num74].active = true;
					Main.tile[num73, num74].type = 2;
				}
				num73 = genRand.Next(1, Main.maxTilesX - 1);
				num74 = genRand.Next(0, (int)num5);
				if (num74 >= Main.maxTilesY)
				{
					num74 = Main.maxTilesY - 2;
				}
				if (Main.tile[num73 - 1, num74].active && Main.tile[num73 - 1, num74].type == 0 && Main.tile[num73 + 1, num74].active && Main.tile[num73 + 1, num74].type == 0 && Main.tile[num73, num74 - 1].active && Main.tile[num73, num74 - 1].type == 0 && Main.tile[num73, num74 + 1].active && Main.tile[num73, num74 + 1].type == 0)
				{
					Main.tile[num73, num74].active = true;
					Main.tile[num73, num74].type = 2;
				}
			}
			Main.statusText = Lang.gen[11] + " 0%";
			float num75 = Main.maxTilesX / 2800;
			int num76 = 0;
			float num77 = (float)genRand.Next(15, 30) * 0.01f;
			if (num9 == -1)
			{
				num77 = 1f - num77;
				num76 = (int)((float)Main.maxTilesX * num77);
			}
			else
			{
				num76 = (int)((float)Main.maxTilesX * num77);
			}
			int num78 = (int)(((double)Main.maxTilesY + Main.rockLayer) * 0.5) + genRand.Next((int)(-100f * num75), (int)(101f * num75));
			num76 += genRand.Next((int)(-100f * num75), (int)(101f * num75));
			int num79 = num76;
			int num80 = num78;
			TileRunner(num76, num78, genRand.Next((int)(250f * num75), (int)(500f * num75)), genRand.Next(50, 150), 59, addTile: false, num9 * 3);
			for (int num81 = 0; (float)num81 < 6f * num75; num81++)
			{
				TileRunner(num76 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), num78 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), genRand.Next(3, 7), genRand.Next(3, 8), genRand.Next(63, 65));
			}
			mudWall = true;
			Main.statusText = Lang.gen[11] + " 15%";
			num76 += genRand.Next((int)(-250f * num75), (int)(251f * num75));
			num78 += genRand.Next((int)(-150f * num75), (int)(151f * num75));
			int num82 = num76;
			int num83 = num78;
			int num84 = num76;
			int num85 = num78;
			TileRunner(num76, num78, genRand.Next((int)(250f * num75), (int)(500f * num75)), genRand.Next(50, 150), 59);
			mudWall = false;
			for (int num86 = 0; (float)num86 < 6f * num75; num86++)
			{
				TileRunner(num76 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), num78 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), genRand.Next(3, 7), genRand.Next(3, 8), genRand.Next(65, 67));
			}
			mudWall = true;
			Main.statusText = Lang.gen[11] + " 30%";
			num76 += genRand.Next((int)(-400f * num75), (int)(401f * num75));
			num78 += genRand.Next((int)(-150f * num75), (int)(151f * num75));
			int num87 = num76;
			int num88 = num78;
			TileRunner(num76, num78, genRand.Next((int)(250f * num75), (int)(500f * num75)), genRand.Next(50, 150), 59, addTile: false, num9 * -3);
			mudWall = false;
			for (int num89 = 0; (float)num89 < 6f * num75; num89++)
			{
				TileRunner(num76 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), num78 + genRand.Next(-(int)(125f * num75), (int)(125f * num75)), genRand.Next(3, 7), genRand.Next(3, 8), genRand.Next(67, 69));
			}
			mudWall = true;
			Main.statusText = Lang.gen[11] + " 45%";
			num76 = (num79 + num82 + num87) / 3;
			num78 = (num80 + num83 + num88) / 3;
			TileRunner(num76, num78, genRand.Next((int)(400f * num75), (int)(600f * num75)), 10000, 59, addTile: false, 0f, -20f, noYChange: true);
			JungleRunner(num76, num78);
			Main.statusText = Lang.gen[11] + " 60%";
			mudWall = false;
			for (int num90 = 0; num90 < Main.maxTilesX / 10; num90++)
			{
				num76 = genRand.Next(20, Main.maxTilesX - 20);
				num78 = genRand.Next((int)Main.rockLayer, (int)Main.hellLayer);
				while (Main.tile[num76, num78].wall != 15)
				{
					num76 = genRand.Next(20, Main.maxTilesX - 20);
					num78 = genRand.Next((int)Main.rockLayer, (int)Main.hellLayer);
				}
				MudWallRunner(num76, num78);
			}
			num76 = num84;
			num78 = num85;
			for (int num91 = 0; (float)num91 <= 20f * num75; num91++)
			{
				Main.statusText = Lang.gen[11] + " " + (int)(60f + (float)num91 / num75) + "%";
				num76 += genRand.Next((int)(-5f * num75), (int)(6f * num75));
				num78 += genRand.Next((int)(-5f * num75), (int)(6f * num75));
				TileRunner(num76, num78, genRand.Next(40, 100), genRand.Next(300, 500), 59);
			}
			for (int num92 = 0; (float)num92 <= 10f * num75; num92++)
			{
				Main.statusText = Lang.gen[11] + " " + (int)(80f + (float)num92 / num75 * 2f) + "%";
				num76 = num84 + genRand.Next((int)(-600f * num75), (int)(600f * num75));
				num78 = num85 + genRand.Next((int)(-200f * num75), (int)(200f * num75));
				while (num76 < 1 || num76 >= Main.maxTilesX - 1 || num78 < 1 || num78 >= Main.maxTilesY - 1 || Main.tile[num76, num78].type != 59)
				{
					num76 = num84 + genRand.Next((int)(-600f * num75), (int)(600f * num75));
					num78 = num85 + genRand.Next((int)(-200f * num75), (int)(200f * num75));
				}
				for (int num93 = 0; (float)num93 < 8f * num75; num93++)
				{
					num76 += genRand.Next(-30, 31);
					num78 += genRand.Next(-30, 31);
					int type4 = -1;
					if (genRand.Next(7) == 0)
					{
						type4 = -2;
					}
					TileRunner(num76, num78, genRand.Next(10, 20), genRand.Next(30, 70), type4);
				}
			}
			for (int num94 = 0; (float)num94 <= 300f * num75; num94++)
			{
				num76 = num84 + genRand.Next((int)(-600f * num75), (int)(600f * num75));
				num78 = num85 + genRand.Next((int)(-200f * num75), (int)(200f * num75));
				while (num76 < 1 || num76 >= Main.maxTilesX - 1 || num78 < 1 || num78 >= Main.maxTilesY - 1 || Main.tile[num76, num78].type != 59)
				{
					num76 = num84 + genRand.Next((int)(-600f * num75), (int)(600f * num75));
					num78 = num85 + genRand.Next((int)(-200f * num75), (int)(200f * num75));
				}
				TileRunner(num76, num78, genRand.Next(4, 10), genRand.Next(5, 30), 1);
				if (genRand.Next(4) == 0)
				{
					int type5 = genRand.Next(63, 69);
					TileRunner(num76 + genRand.Next(-1, 2), num78 + genRand.Next(-1, 2), genRand.Next(3, 7), genRand.Next(4, 8), type5);
				}
			}
			num76 = num84;
			num78 = num85;
			float num95 = genRand.Next(6, 10) * Main.maxTilesX / 4200;
			for (int num96 = 0; (float)num96 < num95; num96++)
			{
				bool flag5 = true;
				while (flag5)
				{
					num76 = genRand.Next(20, Main.maxTilesX - 20);
					num78 = genRand.Next((int)((Main.worldSurface + Main.rockLayer) * 0.5), (int)Main.hellLayer - 100);
					if (Main.tile[num76, num78].type != 59)
					{
						continue;
					}
					flag5 = false;
					int num97 = genRand.Next(2, 4);
					int num98 = genRand.Next(2, 4);
					for (int num99 = num76 - num97 - 1; num99 <= num76 + num97 + 1; num99++)
					{
						for (int num100 = num78 - num98 - 1; num100 <= num78 + num98 + 1; num100++)
						{
							Main.tile[num99, num100].active = true;
							Main.tile[num99, num100].type = 45;
							Main.tile[num99, num100].liquid = 0;
							Main.tile[num99, num100].lava = false;
						}
					}
					for (int num101 = num76 - num97; num101 <= num76 + num97; num101++)
					{
						for (int num102 = num78 - num98; num102 <= num78 + num98; num102++)
						{
							Main.tile[num101, num102].active = false;
						}
					}
					bool flag6 = false;
					int num103 = 0;
					while (!flag6 && num103 < 100)
					{
						num103++;
						int num104 = genRand.Next(num76 - num97, num76 + num97 + 1);
						int num105 = genRand.Next(num78 - num98, num78 + num98 - 2);
						PlaceTile(num104, num105, 4, mute: true);
						if (Main.tile[num104, num105].type == 4)
						{
							flag6 = true;
						}
					}
					for (int num106 = num76 - num97 - 1; num106 <= num76 + num97 + 1; num106++)
					{
						for (int num107 = num78 + num98 - 2; num107 <= num78 + num98; num107++)
						{
							Main.tile[num106, num107].active = false;
						}
					}
					for (int num108 = num76 - num97 - 1; num108 <= num76 + num97 + 1; num108++)
					{
						for (int num109 = num78 + num98 - 2; num109 <= num78 + num98 - 1; num109++)
						{
							Main.tile[num108, num109].active = false;
						}
					}
					for (int num110 = num76 - num97 - 1; num110 <= num76 + num97 + 1; num110++)
					{
						int num111 = 4;
						int num112 = num78 + num98 + 2;
						while (!Main.tile[num110, num112].active && num112 < Main.maxTilesY && num111 > 0)
						{
							Main.tile[num110, num112].active = true;
							Main.tile[num110, num112].type = 59;
							num112++;
							num111--;
						}
					}
					num97 -= genRand.Next(1, 3);
					int num113 = num78 - num98 - 2;
					while (num97 > -1)
					{
						for (int num114 = num76 - num97 - 1; num114 <= num76 + num97 + 1; num114++)
						{
							Main.tile[num114, num113].active = true;
							Main.tile[num114, num113].type = 45;
						}
						num97 -= genRand.Next(1, 3);
						num113--;
					}
					JChestX[numJChests] = num76;
					JChestY[numJChests] = num78;
					numJChests++;
				}
			}
			for (int num115 = 0; num115 < Main.maxTilesX; num115++)
			{
				for (int num116 = 0; num116 < Main.maxTilesY; num116++)
				{
					if (Main.tile[num115, num116].active)
					{
						try
						{
							grassSpread = 0;
							SpreadGrass(num115, num116, 59, 60);
						}
						catch
						{
							grassSpread = 0;
							SpreadGrass(num115, num116, 59, 60, repeat: false);
						}
					}
				}
			}
			numIslandHouses = 0;
			houseCount = 0;
			Main.statusText = Lang.gen[12];
			for (int num117 = 0; num117 < (int)((float)Main.maxTilesX * 0.0008f); num117++)
			{
				int num118 = 0;
				bool flag7 = false;
				int num119 = genRand.Next((int)((float)Main.maxTilesX * 0.1f), (int)((float)Main.maxTilesX * 0.9f));
				bool flag8 = false;
				while (!flag8)
				{
					flag8 = true;
					while (num119 > Main.maxTilesX / 2 - 80 && num119 < Main.maxTilesX / 2 + 80)
					{
						num119 = genRand.Next((int)((float)Main.maxTilesX * 0.1f), (int)((float)Main.maxTilesX * 0.9f));
					}
					for (int num120 = 0; num120 < numIslandHouses; num120++)
					{
						if (num119 > fihX[num120] - 80 && num119 < fihX[num120] + 80)
						{
							num118++;
							flag8 = false;
							break;
						}
					}
					if (num118 >= 200)
					{
						flag7 = true;
						break;
					}
				}
				if (flag7)
				{
					continue;
				}
				for (int num121 = 200; (double)num121 < Main.worldSurface; num121++)
				{
					if (Main.tile[num119, num121].active)
					{
						int num122 = num119;
						int num123 = genRand.Next(90, num121 - 100);
						while ((float)num123 > num5 - 50f)
						{
							num123--;
						}
						FloatingIsland(num122, num123);
						fihX[numIslandHouses] = num122;
						fihY[numIslandHouses] = num123;
						numIslandHouses++;
						break;
					}
				}
			}
			Main.statusText = Lang.gen[13];
			for (int num124 = 0; num124 < Main.maxTilesX / 500; num124++)
			{
				int i2 = genRand.Next((int)((float)Main.maxTilesX * 0.3f), (int)((float)Main.maxTilesX * 0.7f));
				int j2 = genRand.Next((int)Main.rockLayer, (int)Main.hellLayer - 150);
				ShroomPatch(i2, j2);
			}
			for (int num125 = 0; num125 < Main.maxTilesX; num125++)
			{
				for (int num126 = (int)Main.worldSurface; num126 < Main.maxTilesY; num126++)
				{
					if (Main.tile[num125, num126].active)
					{
						grassSpread = 0;
						SpreadGrass(num125, num126, 59, 70, repeat: false);
					}
				}
			}
			Main.statusText = Lang.gen[14];
			for (int num127 = 0; num127 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.001f); num127++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(2, 6), genRand.Next(2, 40), 59);
			}
			Main.statusText = Lang.gen[15];
			for (int num128 = 0; num128 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0001f); num128++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num8, Main.maxTilesY), genRand.Next(5, 12), genRand.Next(15, 50), 123);
			}
			for (int num129 = 0; num129 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0005f); num129++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num8, Main.maxTilesY), genRand.Next(2, 5), genRand.Next(2, 5), 123);
			}
			Main.statusText = Lang.gen[16];
			for (int num130 = 0; num130 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 6E-05f); num130++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num5, (int)num6), genRand.Next(3, 6), genRand.Next(2, 6), 7);
			}
			for (int num131 = 0; num131 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 8E-05f); num131++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, (int)num8), genRand.Next(3, 7), genRand.Next(3, 7), 7);
			}
			for (int num132 = 0; num132 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0002f); num132++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(4, 9), genRand.Next(4, 8), 7);
			}
			for (int num133 = 0; num133 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 3E-05f); num133++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num5, (int)num6), genRand.Next(3, 7), genRand.Next(2, 5), 6);
			}
			for (int num134 = 0; num134 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 8E-05f); num134++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, (int)num8), genRand.Next(3, 6), genRand.Next(3, 6), 6);
			}
			for (int num135 = 0; num135 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0002f); num135++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(4, 9), genRand.Next(4, 8), 6);
			}
			for (int num136 = 0; num136 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2.6E-05f); num136++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num6, (int)num8), genRand.Next(3, 6), genRand.Next(3, 6), 9);
			}
			for (int num137 = 0; num137 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00015f); num137++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(4, 9), genRand.Next(4, 8), 9);
			}
			for (int num138 = 0; num138 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00017f); num138++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next(0, (int)num5), genRand.Next(4, 9), genRand.Next(4, 8), 9);
			}
			for (int num139 = 0; num139 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00012f); num139++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(4, 8), genRand.Next(4, 8), 8);
			}
			for (int num140 = 0; num140 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.00012f); num140++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next(0, (int)num5 - 20), genRand.Next(4, 8), genRand.Next(4, 8), 8);
			}
			for (int num141 = 0; num141 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 2E-05f); num141++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)num7, Main.maxTilesY), genRand.Next(2, 4), genRand.Next(3, 6), 22);
			}
			Main.statusText = Lang.gen[17];
			for (int num142 = 0; num142 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0006f); num142++)
			{
				int num143 = genRand.Next(20, Main.maxTilesX - 20);
				int num144 = genRand.Next((int)num5, Main.maxTilesY - 20);
				if (num142 < numMCaves)
				{
					num143 = mCaveX[num142];
					num144 = mCaveY[num142];
				}
				if (!Main.tile[num143, num144].active && (!((double)num144 <= Main.worldSurface) || Main.tile[num143, num144].wall > 0))
				{
					while (!Main.tile[num143, num144].active && num144 > (int)num5)
					{
						num144--;
					}
					num144++;
					int num145 = 1;
					if (genRand.Next(2) == 0)
					{
						num145 = -1;
					}
					for (; !Main.tile[num143, num144].active && num143 > 10 && num143 < Main.maxTilesX - 10; num143 += num145)
					{
					}
					num143 -= num145;
					if ((double)num144 > Main.worldSurface || Main.tile[num143, num144].wall > 0)
					{
						TileRunner(num143, num144, genRand.Next(4, 11), genRand.Next(2, 4), 51, addTile: true, num145, -1f, noYChange: false, overRide: false);
					}
				}
			}
			Main.statusText = Lang.gen[18] + " 0%";
			int num146 = (int)Main.hellLayer + genRand.Next(11, 51);
			for (int num147 = 0; num147 < Main.maxTilesX; num147++)
			{
				num146 += genRand.Next(-3, 4);
				if ((double)num146 < Main.hellLayer + 10.0)
				{
					num146 = (int)Main.hellLayer + 10;
				}
				if ((double)num146 > Main.hellLayer + 40.0)
				{
					num146 = (int)Main.hellLayer + 40;
				}
				for (int num148 = num146 - 20 - genRand.Next(3); num148 < Main.maxTilesY; num148++)
				{
					if (num148 >= num146)
					{
						Main.tile[num147, num148].active = false;
						Main.tile[num147, num148].lava = false;
						Main.tile[num147, num148].liquid = 0;
					}
					else
					{
						Main.tile[num147, num148].type = 57;
					}
				}
			}
			int num149 = Main.maxTilesY - genRand.Next(40, 70);
			for (int num150 = 10; num150 < Main.maxTilesX - 10; num150++)
			{
				num149 += genRand.Next(-10, 11);
				if (num149 > Main.maxTilesY - 60)
				{
					num149 = Main.maxTilesY - 60;
				}
				if (num149 < Main.maxTilesY - 100)
				{
					num149 = Main.maxTilesY - 120;
				}
				for (int num151 = num149; num151 < Main.maxTilesY - 10; num151++)
				{
					if (!Main.tile[num150, num151].active)
					{
						Main.tile[num150, num151].lava = true;
						Main.tile[num150, num151].liquid = byte.MaxValue;
					}
				}
			}
			for (int num152 = 0; num152 < Main.maxTilesX; num152++)
			{
				if (genRand.Next(50) == 0)
				{
					int num153 = Main.maxTilesY - 65;
					while (!Main.tile[num152, num153].active && num153 > Main.maxTilesY - 135)
					{
						num153--;
					}
					TileRunner(genRand.Next(0, Main.maxTilesX), num153 + genRand.Next(20, 50), genRand.Next(15, 20), 1000, 57, addTile: true, 0f, genRand.Next(1, 3), noYChange: true);
				}
			}
			Liquid.QuickWater(-2);
			for (int num154 = 0; num154 < Main.maxTilesX; num154++)
			{
				float num155 = (float)num154 / (float)(Main.maxTilesX - 1);
				Main.statusText = Lang.gen[18] + " " + (int)(num155 * 100f / 2f + 50f) + "%";
				if (genRand.Next(13) == 0)
				{
					int num156 = Main.maxTilesY - 65;
					while ((Main.tile[num154, num156].liquid > 0 || Main.tile[num154, num156].active) && (double)num156 > Main.hellLayer + 60.0)
					{
						num156--;
					}
					TileRunner(num154, num156 - genRand.Next(2, 5), genRand.Next(5, 30), 1000, 57, addTile: true, 0f, genRand.Next(1, 3), noYChange: true);
					float num157 = genRand.Next(1, 3);
					if (genRand.Next(3) == 0)
					{
						num157 *= 0.5f;
					}
					if (genRand.Next(2) == 0)
					{
						TileRunner(num154, num156 - genRand.Next(2, 5), (int)((float)genRand.Next(5, 15) * num157), (int)((float)genRand.Next(10, 15) * num157), 57, addTile: true, 1f, 0.3f);
					}
					if (genRand.Next(2) == 0)
					{
						TileRunner(num154, num156 - genRand.Next(2, 5), (int)((float)genRand.Next(5, 15) * num157), genRand.Next(10, 15) * genRand.Next(1, 3), 57, addTile: true, -1f, 0.3f);
					}
					TileRunner(num154 + genRand.Next(-10, 10), num156 + genRand.Next(-10, 10), genRand.Next(5, 15), genRand.Next(5, 10), -2, addTile: false, genRand.Next(-1, 3), genRand.Next(-1, 3));
					if (genRand.Next(3) == 0)
					{
						TileRunner(num154 + genRand.Next(-10, 10), num156 + genRand.Next(-10, 10), genRand.Next(10, 30), genRand.Next(10, 20), -2, addTile: false, genRand.Next(-1, 3), genRand.Next(-1, 3));
					}
					if (genRand.Next(5) == 0)
					{
						TileRunner(num154 + genRand.Next(-15, 15), num156 + genRand.Next(-15, 10), genRand.Next(15, 30), genRand.Next(5, 20), -2, addTile: false, genRand.Next(-1, 3), genRand.Next(-1, 3));
					}
				}
			}
			for (int num158 = 0; num158 < Main.maxTilesX; num158++)
			{
				TileRunner(genRand.Next(20, Main.maxTilesX - 20), genRand.Next((int)Main.hellLayer + 20, Main.maxTilesY - 10), genRand.Next(2, 7), genRand.Next(2, 7), -2);
			}
			for (int num159 = 0; num159 < Main.maxTilesX; num159++)
			{
				if (!Main.tile[num159, (int)Main.hellLayer + 55].active)
				{
					Main.tile[num159, (int)Main.hellLayer + 55].liquid = byte.MaxValue;
					Main.tile[num159, (int)Main.hellLayer + 55].lava = true;
				}
				if (!Main.tile[num159, (int)Main.hellLayer + 56].active)
				{
					Main.tile[num159, (int)Main.hellLayer + 56].liquid = byte.MaxValue;
					Main.tile[num159, (int)Main.hellLayer + 56].lava = true;
				}
			}
			for (int num160 = 0; num160 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0008f); num160++)
			{
				TileRunner(genRand.Next(0, Main.maxTilesX), genRand.Next((int)Main.hellLayer + 60, Main.maxTilesY), genRand.Next(2, 7), genRand.Next(3, 7), 58);
			}
			AddHellHouses();
			int num161 = genRand.Next(2, (int)((float)Main.maxTilesX * 0.005f));
			for (int num162 = 0; num162 < num161; num162++)
			{
				float num163 = (float)num162 / (float)num161;
				Main.statusText = Lang.gen[19] + " " + (int)(num163 * 100f) + "%";
				int num164 = genRand.Next(300, Main.maxTilesX - 300);
				while (num164 > Main.maxTilesX / 2 - 50 && num164 < Main.maxTilesX / 2 + 50)
				{
					num164 = genRand.Next(300, Main.maxTilesX - 300);
				}
				int num165;
				for (num165 = (int)num5 - 20; !Main.tile[num164, num165].active; num165++)
				{
				}
				Lakinater(num164, num165);
			}
			int num166 = 0;
			if (num9 == -1)
			{
				num166 = genRand.Next((int)((float)Main.maxTilesX * 0.05f), (int)((float)Main.maxTilesX * 0.2f));
				num9 = -1;
			}
			else
			{
				num166 = genRand.Next((int)((float)Main.maxTilesX * 0.8f), (int)((float)Main.maxTilesX * 0.95f));
				num9 = 1;
			}
			int y = (int)((Main.rockLayer + (double)Main.maxTilesY) * 0.5) + genRand.Next(-200, 200);
			MakeDungeon(num166, y);
			for (int num167 = 0; (float)num167 < (float)Main.maxTilesX * 0.00045f; num167++)
			{
				float num168 = (float)num167 / ((float)Main.maxTilesX * 0.00045f);
				Main.statusText = Lang.gen[20] + " " + (int)(num168 * 100f) + "%";
				bool flag9 = false;
				int num169 = 0;
				int num170 = 0;
				int num171 = 0;
				while (!flag9)
				{
					int num172 = 0;
					flag9 = true;
					int num173 = Main.maxTilesX / 2;
					int num174 = 200;
					num169 = genRand.Next(320, Main.maxTilesX - 320);
					num170 = num169 - genRand.Next(200) - 100;
					num171 = num169 + genRand.Next(200) + 100;
					if (num170 < 285)
					{
						num170 = 285;
					}
					if (num171 > Main.maxTilesX - 285)
					{
						num171 = Main.maxTilesX - 285;
					}
					if (num169 > num173 - num174 && num169 < num173 + num174)
					{
						flag9 = false;
					}
					if (num170 > num173 - num174 && num170 < num173 + num174)
					{
						flag9 = false;
					}
					if (num171 > num173 - num174 && num171 < num173 + num174)
					{
						flag9 = false;
					}
					for (int num175 = num170; num175 < num171; num175++)
					{
						for (int num176 = 0; num176 < (int)Main.worldSurface; num176 += 5)
						{
							if (Main.tile[num175, num176].active && Main.tileDungeon[Main.tile[num175, num176].type])
							{
								flag9 = false;
								break;
							}
							if (!flag9)
							{
								break;
							}
						}
					}
					if (num172 < 200 && JungleX > num170 && JungleX < num171)
					{
						num172++;
						flag9 = false;
					}
				}
				int num177 = 0;
				for (int num178 = num170; num178 < num171; num178++)
				{
					if (num177 > 0)
					{
						num177--;
					}
					if (num178 == num169 || num177 == 0)
					{
						for (int num179 = (int)num5; (double)num179 < Main.worldSurface - 1.0; num179++)
						{
							if (Main.tile[num178, num179].active || Main.tile[num178, num179].wall > 0)
							{
								if (num178 == num169)
								{
									num177 = 20;
									ChasmRunner(num178, num179, genRand.Next(150) + 150, makeOrb: true);
								}
								else if (genRand.Next(35) == 0 && num177 == 0)
								{
									num177 = 30;
									ChasmRunner(num178, num179, genRand.Next(50) + 50, makeOrb: true);
								}
								break;
							}
						}
					}
					for (int num180 = (int)num5; (double)num180 < Main.worldSurface - 1.0; num180++)
					{
						if (!Main.tile[num178, num180].active)
						{
							continue;
						}
						int num181 = num180 + genRand.Next(10, 14);
						for (int num182 = num180; num182 < num181; num182++)
						{
							if ((Main.tile[num178, num182].type == 59 || Main.tile[num178, num182].type == 60) && num178 >= num170 + genRand.Next(5) && num178 < num171 - genRand.Next(5))
							{
								Main.tile[num178, num182].type = 0;
							}
						}
						break;
					}
				}
				double num183 = Main.worldSurface + 40.0;
				for (int num184 = num170; num184 < num171; num184++)
				{
					num183 += (double)genRand.Next(-2, 3);
					if (num183 < Main.worldSurface + 30.0)
					{
						num183 = Main.worldSurface + 30.0;
					}
					else if (num183 > Main.worldSurface + 50.0)
					{
						num183 = Main.worldSurface + 50.0;
					}
					num55 = num184;
					bool flag10 = false;
					for (int num185 = (int)num5; (double)num185 < num183; num185++)
					{
						if (Main.tile[num55, num185].active)
						{
							if (Main.tile[num55, num185].type == 53 && num55 >= num170 + genRand.Next(5) && num55 <= num171 - genRand.Next(5))
							{
								Main.tile[num55, num185].type = 0;
							}
							else if (Main.tile[num55, num185].type == 0 && (double)num185 < Main.worldSurface - 1.0 && !flag10)
							{
								grassSpread = 0;
								SpreadGrass(num55, num185, 0, 23);
							}
							flag10 = true;
							if (Main.tile[num55, num185].type == 1 && num55 >= num170 + genRand.Next(5) && num55 <= num171 - genRand.Next(5))
							{
								Main.tile[num55, num185].type = 25;
							}
							else if (Main.tile[num55, num185].type == 2)
							{
								Main.tile[num55, num185].type = 23;
							}
						}
					}
				}
				for (int num186 = num170; num186 < num171; num186++)
				{
					for (int num187 = 0; num187 < Main.maxTilesY - 50; num187++)
					{
						if (!Main.tile[num186, num187].active || Main.tile[num186, num187].type != 31)
						{
							continue;
						}
						int num188 = num186 - 13;
						int num189 = num186 + 13;
						int num190 = num187 - 13;
						int num191 = num187 + 13;
						for (int num192 = num188; num192 < num189; num192++)
						{
							if (num192 <= 10 || num192 >= Main.maxTilesX - 10)
							{
								continue;
							}
							for (int num193 = num190; num193 < num191; num193++)
							{
								if (Math.Abs(num192 - num186) + Math.Abs(num193 - num187) < 9 + genRand.Next(11) && genRand.Next(3) != 0 && Main.tile[num192, num193].type != 31)
								{
									Main.tile[num192, num193].active = true;
									Main.tile[num192, num193].type = 25;
									if (Math.Abs(num192 - num186) <= 1 && Math.Abs(num193 - num187) <= 1)
									{
										Main.tile[num192, num193].active = false;
									}
								}
								if (Main.tile[num192, num193].type != 31 && Math.Abs(num192 - num186) <= 2 + genRand.Next(3) && Math.Abs(num193 - num187) <= 2 + genRand.Next(3))
								{
									Main.tile[num192, num193].active = false;
								}
							}
						}
					}
				}
			}
			Main.statusText = Lang.gen[21];
			for (int num194 = 0; num194 < numMCaves; num194++)
			{
				int i3 = mCaveX[num194];
				int j3 = mCaveY[num194];
				CaveOpenater(i3, j3);
				Cavinator(i3, j3, genRand.Next(40, 50));
			}
			int num195 = 0;
			int num196 = 0;
			int num197 = 20;
			int num198 = Main.maxTilesX - 20;
			Main.statusText = Lang.gen[22];
			for (int num199 = 0; num199 < 2; num199++)
			{
				int num200 = 0;
				int num201 = 0;
				if (num199 == 0)
				{
					num200 = 0;
					num201 = genRand.Next(125, 200) + 50;
					if (num9 == 1)
					{
						num201 = 275;
					}
					int num202 = 0;
					float num203 = 1f;
					int num204;
					for (num204 = 0; !Main.tile[num201 - 1, num204].active; num204++)
					{
					}
					num195 = num204;
					num204 += genRand.Next(1, 5);
					for (int num205 = num201 - 1; num205 >= num200; num205--)
					{
						num202++;
						if (num202 < 3)
						{
							num203 += (float)genRand.Next(10, 20) * 0.2f;
						}
						else if (num202 < 6)
						{
							num203 += (float)genRand.Next(10, 20) * 0.15f;
						}
						else if (num202 < 9)
						{
							num203 += (float)genRand.Next(10, 20) * 0.1f;
						}
						else if (num202 < 15)
						{
							num203 += (float)genRand.Next(10, 20) * 0.07f;
						}
						else if (num202 < 50)
						{
							num203 += (float)genRand.Next(10, 20) * 0.05f;
						}
						else if (num202 < 75)
						{
							num203 += (float)genRand.Next(10, 20) * 0.04f;
						}
						else if (num202 < 100)
						{
							num203 += (float)genRand.Next(10, 20) * 0.03f;
						}
						else if (num202 < 125)
						{
							num203 += (float)genRand.Next(10, 20) * 0.02f;
						}
						else if (num202 < 150)
						{
							num203 += (float)genRand.Next(10, 20) * 0.01f;
						}
						else if (num202 < 175)
						{
							num203 += (float)genRand.Next(10, 20) * 0.005f;
						}
						else if (num202 < 200)
						{
							num203 += (float)genRand.Next(10, 20) * 0.001f;
						}
						else if (num202 < 230)
						{
							num203 += (float)genRand.Next(10, 20) * 0.01f;
						}
						else if (num202 < 235)
						{
							num203 += (float)genRand.Next(10, 20) * 0.05f;
						}
						else if (num202 < 240)
						{
							num203 += (float)genRand.Next(10, 20) * 0.1f;
						}
						else if (num202 < 245)
						{
							num203 += (float)genRand.Next(10, 20) * 0.05f;
						}
						else if (num202 < 255)
						{
							num203 += (float)genRand.Next(10, 20) * 0.01f;
						}
						if (num202 == 235)
						{
							num198 = num205;
						}
						if (num202 == 235)
						{
							num197 = num205;
						}
						int num206 = genRand.Next(15, 20);
						for (int num207 = 0; (float)num207 < (float)num204 + num203 + (float)num206; num207++)
						{
							if ((float)num207 < (float)num204 + num203 * 0.75f - 3f)
							{
								Main.tile[num205, num207].active = false;
								if (num207 > num204)
								{
									Main.tile[num205, num207].liquid = byte.MaxValue;
								}
								else if (num207 == num204)
								{
									Main.tile[num205, num207].liquid = 127;
								}
							}
							else if (num207 > num204)
							{
								Main.tile[num205, num207].type = 53;
								Main.tile[num205, num207].active = true;
							}
							Main.tile[num205, num207].wall = 0;
						}
					}
					continue;
				}
				num200 = Main.maxTilesX - genRand.Next(125, 200) - 50;
				num201 = Main.maxTilesX;
				if (num9 == -1)
				{
					num200 = Main.maxTilesX - 275;
				}
				float num208 = 1f;
				int num209 = 0;
				int num210;
				for (num210 = 0; !Main.tile[num200, num210].active; num210++)
				{
				}
				num196 = num210;
				num210 += genRand.Next(1, 5);
				for (int num211 = num200; num211 < num201; num211++)
				{
					num209++;
					if (num209 < 3)
					{
						num208 += (float)genRand.Next(10, 20) * 0.2f;
					}
					else if (num209 < 6)
					{
						num208 += (float)genRand.Next(10, 20) * 0.15f;
					}
					else if (num209 < 9)
					{
						num208 += (float)genRand.Next(10, 20) * 0.1f;
					}
					else if (num209 < 15)
					{
						num208 += (float)genRand.Next(10, 20) * 0.07f;
					}
					else if (num209 < 50)
					{
						num208 += (float)genRand.Next(10, 20) * 0.05f;
					}
					else if (num209 < 75)
					{
						num208 += (float)genRand.Next(10, 20) * 0.04f;
					}
					else if (num209 < 100)
					{
						num208 += (float)genRand.Next(10, 20) * 0.03f;
					}
					else if (num209 < 125)
					{
						num208 += (float)genRand.Next(10, 20) * 0.02f;
					}
					else if (num209 < 150)
					{
						num208 += (float)genRand.Next(10, 20) * 0.01f;
					}
					else if (num209 < 175)
					{
						num208 += (float)genRand.Next(10, 20) * 0.005f;
					}
					else if (num209 < 200)
					{
						num208 += (float)genRand.Next(10, 20) * 0.001f;
					}
					else if (num209 < 230)
					{
						num208 += (float)genRand.Next(10, 20) * 0.01f;
					}
					else if (num209 < 235)
					{
						num208 += (float)genRand.Next(10, 20) * 0.05f;
					}
					else if (num209 < 240)
					{
						num208 += (float)genRand.Next(10, 20) * 0.1f;
					}
					else if (num209 < 245)
					{
						num208 += (float)genRand.Next(10, 20) * 0.05f;
					}
					else if (num209 < 255)
					{
						num208 += (float)genRand.Next(10, 20) * 0.01f;
					}
					if (num209 == 235)
					{
						num198 = num211;
					}
					int num212 = genRand.Next(15, 20);
					for (int num213 = 0; (float)num213 < (float)num210 + num208 + (float)num212; num213++)
					{
						if ((float)num213 < (float)num210 + num208 * 0.75f - 3f && (double)num213 < Main.worldSurface - 2.0)
						{
							Main.tile[num211, num213].active = false;
							if (num213 > num210)
							{
								Main.tile[num211, num213].liquid = byte.MaxValue;
							}
							else if (num213 == num210)
							{
								Main.tile[num211, num213].liquid = 127;
							}
						}
						else if (num213 > num210)
						{
							Main.tile[num211, num213].type = 53;
							Main.tile[num211, num213].active = true;
						}
						Main.tile[num211, num213].wall = 0;
					}
				}
			}
			for (; !Main.tile[num197, num195].active; num195++)
			{
			}
			num195++;
			for (; !Main.tile[num198, num196].active; num196++)
			{
			}
			num196++;
			Main.statusText = Lang.gen[23];
			for (int num214 = 63; num214 <= 68; num214++)
			{
				float num215 = 0f;
				switch (num214)
				{
				case 67:
					num215 = (float)Main.maxTilesX * 0.1f;
					break;
				case 66:
					num215 = (float)Main.maxTilesX * 0.081f;
					break;
				case 63:
					num215 = (float)Main.maxTilesX * 0.06f;
					break;
				case 65:
					num215 = (float)Main.maxTilesX * 0.05f;
					break;
				case 64:
					num215 = (float)Main.maxTilesX * 0.02f;
					break;
				case 68:
					num215 = (float)Main.maxTilesX * 0.01f;
					break;
				}
				for (int num216 = 0; (float)num216 < num215; num216++)
				{
					int num217 = genRand.Next(0, Main.maxTilesX);
					int num218 = genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					while (Main.tile[num217, num218].type != 1)
					{
						num217 = genRand.Next(0, Main.maxTilesX);
						num218 = genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					}
					TileRunner(num217, num218, genRand.Next(2, 6), genRand.Next(3, 7), num214);
				}
			}
			for (int num219 = 0; num219 < 2; num219++)
			{
				int num220 = 1;
				int num221 = 5;
				int num222 = Main.maxTilesX - 5;
				if (num219 == 1)
				{
					num220 = -1;
					num221 = Main.maxTilesX - 5;
					num222 = 5;
				}
				for (int num223 = num221; num223 != num222; num223 += num220)
				{
					for (int num224 = 10; num224 < Main.maxTilesY - 10; num224++)
					{
						if (!Main.tile[num223, num224].active || Main.tile[num223, num224].type != 53 || !Main.tile[num223, num224 + 1].active || Main.tile[num223, num224 + 1].type != 53)
						{
							continue;
						}
						int num225 = num223 + num220;
						int num226 = num224 + 1;
						if (!Main.tile[num225, num224].active && !Main.tile[num225, num224 + 1].active)
						{
							for (; !Main.tile[num225, num226].active; num226++)
							{
							}
							num226--;
							Main.tile[num223, num224].active = false;
							Main.tile[num225, num226].active = true;
							Main.tile[num225, num226].type = 53;
						}
					}
				}
			}
			for (int num227 = 0; num227 < Main.maxTilesX; num227++)
			{
				float num228 = (float)num227 / (float)(Main.maxTilesX - 1);
				Main.statusText = Lang.gen[24] + " " + (int)(num228 * 100f) + "%";
				for (int num229 = Main.maxTilesY - 5; num229 > 0; num229--)
				{
					if (Main.tile[num227, num229].active)
					{
						if (Main.tile[num227, num229].type == 53)
						{
							for (int num230 = num229; !Main.tile[num227, num230 + 1].active && num230 < Main.maxTilesY - 5; num230++)
							{
								Main.tile[num227, num230 + 1].active = true;
								Main.tile[num227, num230 + 1].type = 53;
							}
						}
						else if (Main.tile[num227, num229].type == 123)
						{
							for (int num231 = num229; !Main.tile[num227, num231 + 1].active && num231 < Main.maxTilesY - 5; num231++)
							{
								Main.tile[num227, num231 + 1].active = true;
								Main.tile[num227, num231 + 1].type = 123;
								Main.tile[num227, num231].active = false;
							}
						}
					}
				}
			}
			for (int num232 = 3; num232 < Main.maxTilesX - 3; num232++)
			{
				float num233 = (float)num232 / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[25] + " " + (int)(num233 * 100f + 1f) + "%";
				bool flag11 = true;
				for (int num234 = 0; (double)num234 < Main.worldSurface; num234++)
				{
					if (flag11)
					{
						if (Main.tile[num232, num234].wall == 2)
						{
							Main.tile[num232, num234].wall = 0;
						}
						if (Main.tile[num232, num234].type != 53)
						{
							if (Main.tile[num232 - 1, num234].wall == 2)
							{
								Main.tile[num232 - 1, num234].wall = 0;
							}
							if (Main.tile[num232 - 2, num234].wall == 2 && genRand.Next(2) == 0)
							{
								Main.tile[num232 - 2, num234].wall = 0;
							}
							if (Main.tile[num232 - 3, num234].wall == 2 && genRand.Next(2) == 0)
							{
								Main.tile[num232 - 3, num234].wall = 0;
							}
							if (Main.tile[num232 + 1, num234].wall == 2)
							{
								Main.tile[num232 + 1, num234].wall = 0;
							}
							if (Main.tile[num232 + 2, num234].wall == 2 && genRand.Next(2) == 0)
							{
								Main.tile[num232 + 2, num234].wall = 0;
							}
							if (Main.tile[num232 + 3, num234].wall == 2 && genRand.Next(2) == 0)
							{
								Main.tile[num232 + 3, num234].wall = 0;
							}
							if (Main.tile[num232, num234].active)
							{
								flag11 = false;
							}
						}
					}
					else if (Main.tile[num232, num234].wall == 0 && Main.tile[num232, num234 + 1].wall == 0 && Main.tile[num232, num234 + 2].wall == 0 && Main.tile[num232, num234 + 3].wall == 0 && Main.tile[num232, num234 + 4].wall == 0 && Main.tile[num232 - 1, num234].wall == 0 && Main.tile[num232 + 1, num234].wall == 0 && Main.tile[num232 - 2, num234].wall == 0 && Main.tile[num232 + 2, num234].wall == 0 && !Main.tile[num232, num234].active && !Main.tile[num232, num234 + 1].active && !Main.tile[num232, num234 + 2].active && !Main.tile[num232, num234 + 3].active)
					{
						flag11 = true;
					}
				}
			}
			for (int num235 = 0; num235 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num235++)
			{
				float num236 = (float)((double)num235 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = Lang.gen[26] + " " + (int)(num236 * 100f + 1f) + "%";
				bool flag12 = false;
				int num237 = 0;
				while (!flag12)
				{
					int num238 = genRand.Next(1, Main.maxTilesX);
					int num239 = (int)(num6 + 20f);
					Place3x2(num238, num239, 26);
					if (Main.tile[num238, num239].type == 26)
					{
						flag12 = true;
						continue;
					}
					num237++;
					if (num237 >= 10000)
					{
						flag12 = true;
					}
				}
			}
			for (int num240 = 0; num240 < Main.maxTilesX; num240++)
			{
				num55 = num240;
				for (int num241 = (int)num5; (double)num241 < Main.worldSurface - 1.0; num241++)
				{
					if (Main.tile[num55, num241].active)
					{
						if (Main.tile[num55, num241].type == 60)
						{
							Main.tile[num55, num241 - 1].liquid = byte.MaxValue;
							Main.tile[num55, num241 - 2].liquid = byte.MaxValue;
						}
						break;
					}
				}
			}
			for (int num242 = 400; num242 < Main.maxTilesX - 400; num242++)
			{
				num55 = num242;
				for (int num243 = (int)num5; (double)num243 < Main.worldSurface - 1.0; num243++)
				{
					if (!Main.tile[num55, num243].active)
					{
						continue;
					}
					if (Main.tile[num55, num243].type == 53)
					{
						int num244 = num243;
						while ((float)num244 > num5)
						{
							num244--;
							Main.tile[num55, num244].liquid = 0;
						}
					}
					break;
				}
			}
			Liquid.QuickWater(3);
			WaterCheck();
			int num245 = 0;
			Liquid.quickSettle = true;
			while (num245 < 10)
			{
				int num246 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				num245++;
				float num247 = 0f;
				while (Liquid.numLiquid > 0)
				{
					float num248 = (float)(num246 - Liquid.numLiquid - LiquidBuffer.numLiquidBuffer) / (float)num246;
					if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num246)
					{
						num246 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
					}
					if (num248 > num247)
					{
						num247 = num248;
					}
					else
					{
						num248 = num247;
					}
					if (num245 == 1)
					{
						Main.statusText = Lang.gen[27] + " " + (int)(num248 * 100f / 3f + 33f) + "%";
					}
					Liquid.UpdateLiquid();
				}
				WaterCheck();
				Main.statusText = Lang.gen[27] + " " + (int)((float)num245 * 10f / 3f + 66f) + "%";
			}
			Liquid.quickSettle = false;
			float num249 = Main.maxTilesX / 4200;
			for (int num250 = 0; num250 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num250++)
			{
				float num251 = (float)((double)num250 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = Lang.gen[28] + " " + (int)(num251 * 100f + 1f) + "%";
				bool flag13 = false;
				int num252 = 0;
				while (!flag13)
				{
					if (AddLifeCrystal(genRand.Next(1, Main.maxTilesX), genRand.Next((int)((double)num6 + 20.0), Main.maxTilesY)))
					{
						flag13 = true;
						continue;
					}
					num252++;
					if (num252 >= 10000)
					{
						flag13 = true;
					}
				}
			}
			int num253 = 0;
			for (int num254 = 0; (float)num254 < 82f * num249; num254++)
			{
				if (num253 > 41)
				{
					num253 = 0;
				}
				float num255 = (float)num254 / (200f * num249);
				Main.statusText = Lang.gen[29] + " " + (int)(num255 * 100f + 1f) + "%";
				bool flag14 = false;
				int num256 = 0;
				while (!flag14)
				{
					int num257 = genRand.Next(20, Main.maxTilesX - 20);
					int num258;
					for (num258 = genRand.Next((int)((double)num6 + 20.0), (int)Main.hellLayer - 100); !Main.tile[num257, num258].active; num258++)
					{
					}
					num258--;
					PlaceTile(num257, num258, 105, mute: true, forced: true, -1, num253);
					if (Main.tile[num257, num258].active && Main.tile[num257, num258].type == 105)
					{
						flag14 = true;
						num253++;
						continue;
					}
					num256++;
					if (num256 >= 10000)
					{
						flag14 = true;
					}
				}
			}
			for (int num259 = 0; num259 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05); num259++)
			{
				float num260 = (float)((double)num259 / ((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05));
				Main.statusText = Lang.gen[30] + " " + (int)(num260 * 100f + 1f) + "%";
				bool flag15 = false;
				int num261 = 0;
				while (!flag15)
				{
					int num262 = genRand.Next(20, Main.maxTilesX - 20);
					int num263 = genRand.Next((int)((double)num6 + 20.0), (int)Main.hellLayer - 30);
					if ((float)num259 <= 3f * num249)
					{
						num263 = genRand.Next((int)Main.hellLayer, Main.maxTilesY - 50);
					}
					while (Main.tile[num262, num263].wall == 7 || Main.tile[num262, num263].wall == 8 || Main.tile[num262, num263].wall == 9)
					{
						num262 = genRand.Next(1, Main.maxTilesX);
						num263 = genRand.Next((int)((double)num6 + 20.0), (int)Main.hellLayer - 30);
						if (num259 <= 3)
						{
							num263 = genRand.Next((int)Main.hellLayer, Main.maxTilesY - 50);
						}
					}
					if (AddBuriedChest(num262, num263))
					{
						flag15 = true;
						if (genRand.Next(2) == 0)
						{
							int num264;
							for (num264 = num263; Main.tile[num262, num264].type != 21 && num264 < (int)Main.hellLayer - 100; num264++)
							{
							}
							if (num263 < (int)Main.hellLayer - 100)
							{
								MineHouse(num262, num264);
							}
						}
					}
					else
					{
						num261++;
						if (num261 >= 5000)
						{
							flag15 = true;
						}
					}
				}
			}
			for (int num265 = 0; num265 < (int)((float)Main.maxTilesX * 0.005f); num265++)
			{
				float num266 = (float)num265 / ((float)Main.maxTilesX * 0.005f);
				Main.statusText = Lang.gen[31] + " " + (int)(num266 * 100f + 1f) + "%";
				bool flag16 = false;
				int num267 = 0;
				while (!flag16)
				{
					int num268 = genRand.Next(300, Main.maxTilesX - 300);
					int num269 = genRand.Next((int)num5, (int)Main.worldSurface);
					bool flag17 = false;
					if (Main.tile[num268, num269].wall == 2 && !Main.tile[num268, num269].active)
					{
						flag17 = true;
					}
					if (flag17 && AddBuriedChest(num268, num269, 0, notNearOtherChests: true))
					{
						flag16 = true;
						continue;
					}
					num267++;
					if (num267 >= 2000)
					{
						flag16 = true;
					}
				}
			}
			int num270 = 0;
			for (int num271 = 0; num271 < numJChests; num271++)
			{
				float num272 = num271 / numJChests;
				Main.statusText = Lang.gen[32] + " " + (int)(num272 * 100f + 1f) + "%";
				num270++;
				int contain = 211;
				switch (num270)
				{
				case 1:
					contain = 211;
					break;
				case 2:
					contain = 212;
					break;
				case 3:
					contain = 213;
					break;
				}
				if (num270 > 3)
				{
					num270 = 0;
				}
				if (AddBuriedChest(JChestX[num271] + genRand.Next(2), JChestY[num271], contain))
				{
					continue;
				}
				for (int num273 = JChestX[num271]; num273 <= JChestX[num271] + 1; num273++)
				{
					for (int num274 = JChestY[num271]; num274 <= JChestY[num271] + 1; num274++)
					{
						KillTile(num273, num274);
					}
				}
				AddBuriedChest(JChestX[num271], JChestY[num271], contain);
			}
			int num275 = 0;
			for (int num276 = 0; (float)num276 < 9f * num249; num276++)
			{
				float num277 = (float)num276 / (9f * num249);
				Main.statusText = Lang.gen[33] + " " + (int)(num277 * 100f + 1f) + "%";
				int num278 = 0;
				num275++;
				switch (num275)
				{
				case 1:
					num278 = 186;
					break;
				case 2:
					num278 = 277;
					break;
				default:
					num278 = 187;
					num275 = 0;
					break;
				}
				bool flag18 = false;
				while (!flag18)
				{
					int num279 = genRand.Next(1, Main.maxTilesX);
					int num280 = genRand.Next(1, (int)Main.hellLayer);
					while (Main.tile[num279, num280].liquid < 200 || Main.tile[num279, num280].lava)
					{
						num279 = genRand.Next(1, Main.maxTilesX);
						num280 = genRand.Next(1, (int)Main.hellLayer);
					}
					flag18 = AddBuriedChest(num279, num280, num278);
				}
			}
			for (int num281 = 0; num281 < numIslandHouses; num281++)
			{
				IslandHouse(fihX[num281], fihY[num281]);
			}
			for (int num282 = 0; num282 < (int)((float)Main.maxTilesX * 0.05f); num282++)
			{
				float num283 = (float)num282 / ((float)Main.maxTilesX * 0.05f);
				Main.statusText = Lang.gen[34] + " " + (int)(num283 * 100f + 1f) + "%";
				for (int num284 = 0; num284 < 1000; num284++)
				{
					int num285 = Main.rand.Next(200, Main.maxTilesX - 200);
					int num286 = Main.rand.Next((int)Main.worldSurface, (int)Main.hellLayer - 100);
					if (Main.tile[num285, num286].wall == 0 && placeTrap(num285, num286))
					{
						break;
					}
				}
			}
			for (int num287 = 0; num287 < (int)((float)(Main.maxTilesX * Main.maxTilesY) * 0.0008f); num287++)
			{
				float num288 = (float)num287 / ((float)(Main.maxTilesX * Main.maxTilesY) * 0.0008f);
				Main.statusText = Lang.gen[35] + " " + (int)(num288 * 100f + 1f) + "%";
				bool flag19 = false;
				int num289 = 0;
				while (!flag19)
				{
					int num290 = genRand.Next((int)num6, Main.maxTilesY - 10);
					if (num288 > 0.93f)
					{
						num290 = (int)Main.hellLayer + 50;
					}
					else if (num288 > 0.75f)
					{
						num290 = (int)num5;
					}
					int num291 = genRand.Next(1, Main.maxTilesX);
					bool flag20 = false;
					for (int num292 = num290; num292 < Main.maxTilesY; num292++)
					{
						if (!flag20)
						{
							if (Main.tile[num291, num292].active && Main.tileSolid[Main.tile[num291, num292].type] && !Main.tile[num291, num292 - 1].lava)
							{
								flag20 = true;
							}
							continue;
						}
						if (PlacePot(num291, num292))
						{
							flag19 = true;
							break;
						}
						num289++;
						if (num289 >= 10000)
						{
							flag19 = true;
							break;
						}
					}
				}
			}
			for (int num293 = 0; num293 < Main.maxTilesX / 200; num293++)
			{
				float num294 = num293 / (Main.maxTilesX / 200);
				Main.statusText = Lang.gen[36] + " " + (int)(num294 * 100f + 1f) + "%";
				bool flag21 = false;
				int num295 = 0;
				while (!flag21)
				{
					int num296 = genRand.Next(1, Main.maxTilesX);
					int num297 = genRand.Next((int)Main.hellLayer - 50, Main.maxTilesY - 5);
					try
					{
						if (Main.tile[num296, num297].wall == 13 || Main.tile[num296, num297].wall == 14)
						{
							for (; !Main.tile[num296, num297].active; num297++)
							{
							}
							num297--;
							PlaceTile(num296, num297, 77);
							if (Main.tile[num296, num297].type == 77)
							{
								flag21 = true;
							}
							else
							{
								num295++;
								if (num295 >= 10000)
								{
									flag21 = true;
								}
							}
						}
					}
					catch
					{
					}
				}
			}
			Main.statusText = Lang.gen[37];
			for (int num298 = 0; num298 < Main.maxTilesX; num298++)
			{
				num55 = num298;
				bool flag22 = true;
				for (int num299 = 0; (double)num299 < Main.worldSurface - 1.0; num299++)
				{
					if (Main.tile[num55, num299].active)
					{
						if (flag22 && Main.tile[num55, num299].type == 0)
						{
							try
							{
								grassSpread = 0;
								SpreadGrass(num55, num299);
							}
							catch
							{
								grassSpread = 0;
								SpreadGrass(num55, num299, 0, 2, repeat: false);
							}
						}
						if ((float)num299 > num6)
						{
							break;
						}
						flag22 = false;
					}
					else if (Main.tile[num55, num299].wall == 0)
					{
						flag22 = true;
					}
				}
			}
			Main.statusText = Lang.gen[38];
			for (int num300 = 5; num300 < Main.maxTilesX - 5; num300++)
			{
				if (genRand.Next(8) != 0)
				{
					continue;
				}
				for (int num301 = 0; (double)num301 < Main.worldSurface - 1.0; num301++)
				{
					if (!Main.tile[num300, num301].active || Main.tile[num300, num301].type != 53 || Main.tile[num300, num301 - 1].active || Main.tile[num300, num301 - 1].wall != 0)
					{
						continue;
					}
					if (num300 < 250 || num300 > Main.maxTilesX - 250)
					{
						if (Main.tile[num300, num301 - 2].liquid == byte.MaxValue && Main.tile[num300, num301 - 3].liquid == byte.MaxValue && Main.tile[num300, num301 - 4].liquid == byte.MaxValue)
						{
							PlaceTile(num300, num301 - 1, 81, mute: true);
						}
					}
					else if (num300 > 400 && num300 < Main.maxTilesX - 400)
					{
						PlantCactus(num300, num301);
					}
				}
			}
			int num302 = 5;
			bool flag23 = true;
			while (flag23)
			{
				int num303 = Main.maxTilesX / 2 + genRand.Next(-num302, num302 + 1);
				for (int num304 = 0; num304 < Main.maxTilesY; num304++)
				{
					if (Main.tile[num303, num304].active)
					{
						Main.spawnTileX = num303;
						Main.spawnTileY = num304;
						break;
					}
				}
				flag23 = false;
				num302++;
				if ((double)Main.spawnTileY > Main.worldSurface)
				{
					flag23 = true;
				}
				if (Main.tile[Main.spawnTileX, Main.spawnTileY - 1].liquid > 0)
				{
					flag23 = true;
				}
			}
			int num305 = 10;
			while ((double)Main.spawnTileY > Main.worldSurface)
			{
				int num306 = genRand.Next(Main.maxTilesX / 2 - num305, Main.maxTilesX / 2 + num305);
				for (int num307 = 0; num307 < Main.maxTilesY; num307++)
				{
					if (Main.tile[num306, num307].active)
					{
						Main.spawnTileX = num306;
						Main.spawnTileY = num307;
						break;
					}
				}
				num305++;
			}
			int num308 = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, 22);
			Main.npc[num308].homeTileX = Main.spawnTileX;
			Main.npc[num308].homeTileY = Main.spawnTileY;
			Main.npc[num308].direction = 1;
			Main.npc[num308].homeless = true;
			Main.statusText = Lang.gen[39];
			for (int num309 = 0; (float)num309 < (float)Main.maxTilesX * 0.002f; num309++)
			{
				int num310 = 0;
				int num311 = 0;
				_ = Main.maxTilesX / 2;
				int num312 = genRand.Next(Main.maxTilesX);
				num310 = num312 - genRand.Next(10) - 7;
				num311 = num312 + genRand.Next(10) + 7;
				if (num310 < 0)
				{
					num310 = 0;
				}
				if (num311 > Main.maxTilesX - 1)
				{
					num311 = Main.maxTilesX - 1;
				}
				for (int num313 = num310; num313 < num311; num313++)
				{
					for (int num314 = 1; (double)num314 < Main.worldSurface - 1.0; num314++)
					{
						if (Main.tile[num313, num314].type == 2 && Main.tile[num313, num314].active && !Main.tile[num313, num314 - 1].active)
						{
							PlaceTile(num313, num314 - 1, 27, mute: true);
						}
						if (Main.tile[num313, num314].active)
						{
							break;
						}
					}
				}
			}
			Main.statusText = Lang.gen[40];
			for (int num315 = 0; (float)num315 < (float)Main.maxTilesX * 0.003f; num315++)
			{
				int num316 = genRand.Next(50, Main.maxTilesX - 50);
				int num317 = genRand.Next(25, 50);
				for (int num318 = num316 - num317; num318 < num316 + num317; num318++)
				{
					for (int num319 = 20; (double)num319 < Main.worldSurface; num319++)
					{
						GrowEpicTree(num318, num319);
					}
				}
			}
			AddTrees();
			Main.statusText = Lang.gen[41];
			for (int num320 = 0; (float)num320 < (float)Main.maxTilesX * 1.7f; num320++)
			{
				PlantAlch();
			}
			Main.statusText = Lang.gen[42];
			AddPlants();
			for (int num321 = 0; num321 < Main.maxTilesX; num321++)
			{
				for (int num322 = 0; num322 < Main.maxTilesY; num322++)
				{
					if (!Main.tile[num321, num322].active)
					{
						continue;
					}
					if (num322 >= (int)Main.worldSurface && Main.tile[num321, num322].type == 70 && !Main.tile[num321, num322 - 1].active)
					{
						GrowShroom(num321, num322);
						if (!Main.tile[num321, num322 - 1].active)
						{
							PlaceTile(num321, num322 - 1, 71, mute: true);
						}
					}
					if (Main.tile[num321, num322].type == 60 && !Main.tile[num321, num322 - 1].active)
					{
						PlaceTile(num321, num322 - 1, 61, mute: true);
					}
				}
			}
			Main.statusText = Lang.gen[43];
			for (int num323 = 0; num323 < Main.maxTilesX; num323++)
			{
				int num324 = 0;
				for (int num325 = 0; (double)num325 < Main.worldSurface; num325++)
				{
					if (num324 > 0 && !Main.tile[num323, num325].active)
					{
						Main.tile[num323, num325].active = true;
						Main.tile[num323, num325].type = 52;
						num324--;
					}
					else
					{
						num324 = 0;
					}
					if (Main.tile[num323, num325].active && Main.tile[num323, num325].type == 2 && genRand.Next(5) < 3)
					{
						num324 = genRand.Next(1, 10);
					}
				}
				num324 = 0;
				for (int num326 = 0; num326 < Main.maxTilesY; num326++)
				{
					if (num324 > 0 && !Main.tile[num323, num326].active)
					{
						Main.tile[num323, num326].active = true;
						Main.tile[num323, num326].type = 62;
						num324--;
					}
					else
					{
						num324 = 0;
					}
					if (Main.tile[num323, num326].active && Main.tile[num323, num326].type == 60 && genRand.Next(5) < 3)
					{
						num324 = genRand.Next(1, 10);
					}
				}
			}
			Main.statusText = Lang.gen[44];
			for (int num327 = 0; (float)num327 < (float)Main.maxTilesX * 0.005f; num327++)
			{
				int num328 = genRand.Next(20, Main.maxTilesX - 20);
				int num329 = genRand.Next(5, 15);
				int num330 = genRand.Next(15, 30);
				for (int num331 = 1; (double)num331 < Main.worldSurface - 1.0; num331++)
				{
					if (!Main.tile[num328, num331].active)
					{
						continue;
					}
					for (int num332 = num328 - num329; num332 < num328 + num329; num332++)
					{
						for (int num333 = num331 - num330; num333 < num331 + num330; num333++)
						{
							if (Main.tile[num332, num333].type == 3 || Main.tile[num332, num333].type == 24)
							{
								Main.tile[num332, num333].frameX = (short)(genRand.Next(6, 8) * 18);
							}
						}
					}
					break;
				}
			}
			Main.statusText = Lang.gen[45];
			for (int num334 = 0; (float)num334 < (float)Main.maxTilesX * 0.002f; num334++)
			{
				int num335 = genRand.Next(20, Main.maxTilesX - 20);
				int num336 = genRand.Next(4, 10);
				int num337 = genRand.Next(15, 30);
				for (int num338 = 1; (double)num338 < Main.worldSurface - 1.0; num338++)
				{
					if (!Main.tile[num335, num338].active)
					{
						continue;
					}
					for (int num339 = num335 - num336; num339 < num335 + num336; num339++)
					{
						for (int num340 = num338 - num337; num340 < num338 + num337; num340++)
						{
							if (Main.tile[num339, num340].type == 3 || Main.tile[num339, num340].type == 24)
							{
								Main.tile[num339, num340].frameX = 144;
							}
						}
					}
					break;
				}
			}
			if (Config.modDLL.Keys.Count > 0)
			{
				foreach (string key in Config.modDLL.Keys)
				{
					object obj5 = Config.modDLL[key].CreateInstance(Config.namespaces[key] + ".Global_Tile");
					if (obj5 != null)
					{
						Main.statusText = "World being processed by mod " + key;
						Codable.globalRunKnowledge = true;
						Codable.RunSpecifiedMethod("Global Tile " + key, obj5, "ModifyWorld");
						Codable.globalRunKnowledge = false;
					}
				}
			}
			gen = false;
		}

		public static bool GrowEpicTree(int i, int y)
		{
			if (TMod.RunMethod(TMod.WorldHooks.GrowEpicTree, i, y) && !TMod.GetContinueMethod())
			{
				return TMod.GetMethodReturn();
			}
			int j;
			for (j = y; Main.tile[i, j].type == 20; j++)
			{
			}
			if (Main.tile[i, j].active && Main.tile[i, j].type == 2 && Main.tile[i, j - 1].wall == 0 && Main.tile[i, j - 1].liquid == 0 && ((Main.tile[i - 1, j].active && (Main.tile[i - 1, j].type == 2 || Main.tile[i - 1, j].type == 23 || Main.tile[i - 1, j].type == 60 || Main.tile[i - 1, j].type == 109)) || (Main.tile[i + 1, j].active && (Main.tile[i + 1, j].type == 2 || Main.tile[i + 1, j].type == 23 || Main.tile[i + 1, j].type == 60 || Main.tile[i + 1, j].type == 109))))
			{
				int num = 1;
				if (EmptyTileCheck(i - num, i + num, j - 55, j - 1, 20))
				{
					bool flag = false;
					bool flag2 = false;
					int num2 = genRand.Next(20, 30);
					int num3;
					for (int k = j - num2; k < j; k++)
					{
						Main.tile[i, k].frameNumber = (byte)genRand.Next(3);
						Main.tile[i, k].active = true;
						Main.tile[i, k].type = 5;
						num3 = genRand.Next(3);
						int num4 = genRand.Next(10);
						if (k == j - 1 || k == j - num2)
						{
							num4 = 0;
						}
						while (((num4 == 5 || num4 == 7) && flag) || ((num4 == 6 || num4 == 7) && flag2))
						{
							num4 = genRand.Next(10);
						}
						flag = false;
						flag2 = false;
						if (num4 == 5 || num4 == 7)
						{
							flag = true;
						}
						if (num4 == 6 || num4 == 7)
						{
							flag2 = true;
						}
						switch (num4)
						{
						case 1:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 66;
								break;
							case 1:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 88;
								break;
							case 2:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 110;
								break;
							}
							break;
						case 2:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 0;
								break;
							case 1:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 22;
								break;
							case 2:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 44;
								break;
							}
							break;
						case 3:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 44;
								Main.tile[i, k].frameY = 66;
								break;
							case 1:
								Main.tile[i, k].frameX = 44;
								Main.tile[i, k].frameY = 88;
								break;
							case 2:
								Main.tile[i, k].frameX = 44;
								Main.tile[i, k].frameY = 110;
								break;
							}
							break;
						case 4:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 66;
								break;
							case 1:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 88;
								break;
							case 2:
								Main.tile[i, k].frameX = 22;
								Main.tile[i, k].frameY = 110;
								break;
							}
							break;
						case 5:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 88;
								Main.tile[i, k].frameY = 0;
								break;
							case 1:
								Main.tile[i, k].frameX = 88;
								Main.tile[i, k].frameY = 22;
								break;
							case 2:
								Main.tile[i, k].frameX = 88;
								Main.tile[i, k].frameY = 44;
								break;
							}
							break;
						case 6:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 66;
								Main.tile[i, k].frameY = 66;
								break;
							case 1:
								Main.tile[i, k].frameX = 66;
								Main.tile[i, k].frameY = 88;
								break;
							case 2:
								Main.tile[i, k].frameX = 66;
								Main.tile[i, k].frameY = 110;
								break;
							}
							break;
						case 7:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 110;
								Main.tile[i, k].frameY = 66;
								break;
							case 1:
								Main.tile[i, k].frameX = 110;
								Main.tile[i, k].frameY = 88;
								break;
							case 2:
								Main.tile[i, k].frameX = 110;
								Main.tile[i, k].frameY = 110;
								break;
							}
							break;
						default:
							switch (num3)
							{
							case 0:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 0;
								break;
							case 1:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 22;
								break;
							case 2:
								Main.tile[i, k].frameX = 0;
								Main.tile[i, k].frameY = 44;
								break;
							}
							break;
						}
						if (num4 == 5 || num4 == 7)
						{
							Main.tile[i - 1, k].active = true;
							Main.tile[i - 1, k].type = 5;
							num3 = genRand.Next(3);
							if (genRand.Next(3) < 2)
							{
								switch (num3)
								{
								case 0:
									Main.tile[i - 1, k].frameX = 44;
									Main.tile[i - 1, k].frameY = 198;
									break;
								case 1:
									Main.tile[i - 1, k].frameX = 44;
									Main.tile[i - 1, k].frameY = 220;
									break;
								case 2:
									Main.tile[i - 1, k].frameX = 44;
									Main.tile[i - 1, k].frameY = 242;
									break;
								}
							}
							else
							{
								switch (num3)
								{
								case 0:
									Main.tile[i - 1, k].frameX = 66;
									Main.tile[i - 1, k].frameY = 0;
									break;
								case 1:
									Main.tile[i - 1, k].frameX = 66;
									Main.tile[i - 1, k].frameY = 22;
									break;
								case 2:
									Main.tile[i - 1, k].frameX = 66;
									Main.tile[i - 1, k].frameY = 44;
									break;
								}
							}
						}
						if (num4 != 6 && num4 != 7)
						{
							continue;
						}
						Main.tile[i + 1, k].active = true;
						Main.tile[i + 1, k].type = 5;
						num3 = genRand.Next(3);
						if (genRand.Next(3) < 2)
						{
							switch (num3)
							{
							case 0:
								Main.tile[i + 1, k].frameX = 66;
								Main.tile[i + 1, k].frameY = 198;
								break;
							case 1:
								Main.tile[i + 1, k].frameX = 66;
								Main.tile[i + 1, k].frameY = 220;
								break;
							case 2:
								Main.tile[i + 1, k].frameX = 66;
								Main.tile[i + 1, k].frameY = 242;
								break;
							}
						}
						else
						{
							switch (num3)
							{
							case 0:
								Main.tile[i + 1, k].frameX = 88;
								Main.tile[i + 1, k].frameY = 66;
								break;
							case 1:
								Main.tile[i + 1, k].frameX = 88;
								Main.tile[i + 1, k].frameY = 88;
								break;
							case 2:
								Main.tile[i + 1, k].frameX = 88;
								Main.tile[i + 1, k].frameY = 110;
								break;
							}
						}
					}
					int num5 = genRand.Next(3);
					bool flag3 = false;
					bool flag4 = false;
					if (Main.tile[i - 1, j].active && (Main.tile[i - 1, j].type == 2 || Main.tile[i - 1, j].type == 23 || Main.tile[i - 1, j].type == 60 || Main.tile[i - 1, j].type == 109))
					{
						flag3 = true;
					}
					if (Main.tile[i + 1, j].active && (Main.tile[i + 1, j].type == 2 || Main.tile[i + 1, j].type == 23 || Main.tile[i + 1, j].type == 60 || Main.tile[i + 1, j].type == 109))
					{
						flag4 = true;
					}
					if (flag4 && !flag3)
					{
						num5 = 2;
					}
					else if (flag3 && !flag4)
					{
						num5 = 1;
					}
					else if (!flag4)
					{
						switch (num5)
						{
						case 0:
							num5 = 1;
							break;
						case 2:
							num5 = 3;
							break;
						}
					}
					else if (!flag3)
					{
						switch (num5)
						{
						case 0:
							num5 = 2;
							break;
						case 1:
							num5 = 3;
							break;
						}
					}
					if (num5 == 0 || num5 == 1)
					{
						Main.tile[i + 1, j - 1].active = true;
						Main.tile[i + 1, j - 1].type = 5;
						switch (genRand.Next(3))
						{
						case 0:
							Main.tile[i + 1, j - 1].frameX = 22;
							Main.tile[i + 1, j - 1].frameY = 132;
							break;
						case 1:
							Main.tile[i + 1, j - 1].frameX = 22;
							Main.tile[i + 1, j - 1].frameY = 154;
							break;
						case 2:
							Main.tile[i + 1, j - 1].frameX = 22;
							Main.tile[i + 1, j - 1].frameY = 176;
							break;
						}
					}
					if (num5 == 0 || num5 == 2)
					{
						Main.tile[i - 1, j - 1].active = true;
						Main.tile[i - 1, j - 1].type = 5;
						switch (genRand.Next(3))
						{
						case 0:
							Main.tile[i - 1, j - 1].frameX = 44;
							Main.tile[i - 1, j - 1].frameY = 132;
							break;
						case 1:
							Main.tile[i - 1, j - 1].frameX = 44;
							Main.tile[i - 1, j - 1].frameY = 154;
							break;
						case 2:
							Main.tile[i - 1, j - 1].frameX = 44;
							Main.tile[i - 1, j - 1].frameY = 176;
							break;
						}
					}
					num3 = genRand.Next(3);
					switch (num5)
					{
					case 0:
						switch (num3)
						{
						case 0:
							Main.tile[i, j - 1].frameX = 88;
							Main.tile[i, j - 1].frameY = 132;
							break;
						case 1:
							Main.tile[i, j - 1].frameX = 88;
							Main.tile[i, j - 1].frameY = 154;
							break;
						case 2:
							Main.tile[i, j - 1].frameX = 88;
							Main.tile[i, j - 1].frameY = 176;
							break;
						}
						break;
					case 1:
						switch (num3)
						{
						case 0:
							Main.tile[i, j - 1].frameX = 0;
							Main.tile[i, j - 1].frameY = 132;
							break;
						case 1:
							Main.tile[i, j - 1].frameX = 0;
							Main.tile[i, j - 1].frameY = 154;
							break;
						case 2:
							Main.tile[i, j - 1].frameX = 0;
							Main.tile[i, j - 1].frameY = 176;
							break;
						}
						break;
					case 2:
						switch (num3)
						{
						case 0:
							Main.tile[i, j - 1].frameX = 66;
							Main.tile[i, j - 1].frameY = 132;
							break;
						case 1:
							Main.tile[i, j - 1].frameX = 66;
							Main.tile[i, j - 1].frameY = 154;
							break;
						case 2:
							Main.tile[i, j - 1].frameX = 66;
							Main.tile[i, j - 1].frameY = 176;
							break;
						}
						break;
					}
					if (genRand.Next(3) < 2)
					{
						switch (genRand.Next(3))
						{
						case 0:
							Main.tile[i, j - num2].frameX = 22;
							Main.tile[i, j - num2].frameY = 198;
							break;
						case 1:
							Main.tile[i, j - num2].frameX = 22;
							Main.tile[i, j - num2].frameY = 220;
							break;
						case 2:
							Main.tile[i, j - num2].frameX = 22;
							Main.tile[i, j - num2].frameY = 242;
							break;
						}
					}
					else
					{
						switch (genRand.Next(3))
						{
						case 0:
							Main.tile[i, j - num2].frameX = 0;
							Main.tile[i, j - num2].frameY = 198;
							break;
						case 1:
							Main.tile[i, j - num2].frameX = 0;
							Main.tile[i, j - num2].frameY = 220;
							break;
						case 2:
							Main.tile[i, j - num2].frameX = 0;
							Main.tile[i, j - num2].frameY = 242;
							break;
						}
					}
					RangeFrame(i - 2, j - num2 - 1, i + 2, j + 1);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, (int)((float)j - (float)num2 * 0.5f), num2 + 1);
					}
					return true;
				}
			}
			return false;
		}

		public static void GrowTree(int i, int y)
		{
			if (TMod.RunMethod(TMod.WorldHooks.GrowTree, i, y) && !TMod.GetContinueMethod())
			{
				return;
			}
			int j;
			for (j = y; Main.tile[i, j].type == 20; j++)
			{
			}
			if (((Main.tile[i - 1, j - 1].liquid != 0 || Main.tile[i + 1, j - 1].liquid != 0) && Main.tile[i, j].type != 60) || !Main.tile[i, j].active || (Main.tile[i, j].type != 2 && Main.tile[i, j].type != 23 && Main.tile[i, j].type != 60 && Main.tile[i, j].type != 109 && Main.tile[i, j].type != 147) || Main.tile[i, j - 1].wall != 0 || ((!Main.tile[i - 1, j].active || (Main.tile[i - 1, j].type != 2 && Main.tile[i - 1, j].type != 23 && Main.tile[i - 1, j].type != 60 && Main.tile[i - 1, j].type != 109 && Main.tile[i - 1, j].type != 147)) && (!Main.tile[i + 1, j].active || (Main.tile[i + 1, j].type != 2 && Main.tile[i + 1, j].type != 23 && Main.tile[i + 1, j].type != 60 && Main.tile[i + 1, j].type != 109 && Main.tile[i + 1, j].type != 147))))
			{
				return;
			}
			int num = 1;
			int num2 = 16;
			if (Main.tile[i, j].type == 60)
			{
				num2 += 5;
			}
			if (!EmptyTileCheck(i - num, i + num, j - num2, j - 1, 20))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num3 = genRand.Next(5, num2 + 1);
			int num4;
			for (int k = j - num3; k < j; k++)
			{
				Main.tile[i, k].frameNumber = (byte)genRand.Next(3);
				Main.tile[i, k].active = true;
				Main.tile[i, k].type = 5;
				num4 = genRand.Next(3);
				int num5 = genRand.Next(10);
				if (k == j - 1 || k == j - num3)
				{
					num5 = 0;
				}
				while (((num5 == 5 || num5 == 7) && flag) || ((num5 == 6 || num5 == 7) && flag2))
				{
					num5 = genRand.Next(10);
				}
				flag = false;
				flag2 = false;
				if (num5 == 5 || num5 == 7)
				{
					flag = true;
				}
				if (num5 == 6 || num5 == 7)
				{
					flag2 = true;
				}
				switch (num5)
				{
				case 1:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 66;
						break;
					case 1:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 88;
						break;
					case 2:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 110;
						break;
					}
					break;
				case 2:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 0;
						break;
					case 1:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 22;
						break;
					case 2:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 44;
						break;
					}
					break;
				case 3:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 44;
						Main.tile[i, k].frameY = 66;
						break;
					case 1:
						Main.tile[i, k].frameX = 44;
						Main.tile[i, k].frameY = 88;
						break;
					case 2:
						Main.tile[i, k].frameX = 44;
						Main.tile[i, k].frameY = 110;
						break;
					}
					break;
				case 4:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 66;
						break;
					case 1:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 88;
						break;
					case 2:
						Main.tile[i, k].frameX = 22;
						Main.tile[i, k].frameY = 110;
						break;
					}
					break;
				case 5:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 88;
						Main.tile[i, k].frameY = 0;
						break;
					case 1:
						Main.tile[i, k].frameX = 88;
						Main.tile[i, k].frameY = 22;
						break;
					case 2:
						Main.tile[i, k].frameX = 88;
						Main.tile[i, k].frameY = 44;
						break;
					}
					break;
				case 6:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 66;
						Main.tile[i, k].frameY = 66;
						break;
					case 1:
						Main.tile[i, k].frameX = 66;
						Main.tile[i, k].frameY = 88;
						break;
					case 2:
						Main.tile[i, k].frameX = 66;
						Main.tile[i, k].frameY = 110;
						break;
					}
					break;
				case 7:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 110;
						Main.tile[i, k].frameY = 66;
						break;
					case 1:
						Main.tile[i, k].frameX = 110;
						Main.tile[i, k].frameY = 88;
						break;
					case 2:
						Main.tile[i, k].frameX = 110;
						Main.tile[i, k].frameY = 110;
						break;
					}
					break;
				default:
					switch (num4)
					{
					case 0:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 0;
						break;
					case 1:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 22;
						break;
					case 2:
						Main.tile[i, k].frameX = 0;
						Main.tile[i, k].frameY = 44;
						break;
					}
					break;
				}
				if (num5 == 5 || num5 == 7)
				{
					Main.tile[i - 1, k].active = true;
					Main.tile[i - 1, k].type = 5;
					num4 = genRand.Next(3);
					if (genRand.Next(3) < 2)
					{
						switch (num4)
						{
						case 0:
							Main.tile[i - 1, k].frameX = 44;
							Main.tile[i - 1, k].frameY = 198;
							break;
						case 1:
							Main.tile[i - 1, k].frameX = 44;
							Main.tile[i - 1, k].frameY = 220;
							break;
						case 2:
							Main.tile[i - 1, k].frameX = 44;
							Main.tile[i - 1, k].frameY = 242;
							break;
						}
					}
					else
					{
						switch (num4)
						{
						case 0:
							Main.tile[i - 1, k].frameX = 66;
							Main.tile[i - 1, k].frameY = 0;
							break;
						case 1:
							Main.tile[i - 1, k].frameX = 66;
							Main.tile[i - 1, k].frameY = 22;
							break;
						case 2:
							Main.tile[i - 1, k].frameX = 66;
							Main.tile[i - 1, k].frameY = 44;
							break;
						}
					}
				}
				if (num5 != 6 && num5 != 7)
				{
					continue;
				}
				Main.tile[i + 1, k].active = true;
				Main.tile[i + 1, k].type = 5;
				num4 = genRand.Next(3);
				if (genRand.Next(3) < 2)
				{
					switch (num4)
					{
					case 0:
						Main.tile[i + 1, k].frameX = 66;
						Main.tile[i + 1, k].frameY = 198;
						break;
					case 1:
						Main.tile[i + 1, k].frameX = 66;
						Main.tile[i + 1, k].frameY = 220;
						break;
					case 2:
						Main.tile[i + 1, k].frameX = 66;
						Main.tile[i + 1, k].frameY = 242;
						break;
					}
				}
				else
				{
					switch (num4)
					{
					case 0:
						Main.tile[i + 1, k].frameX = 88;
						Main.tile[i + 1, k].frameY = 66;
						break;
					case 1:
						Main.tile[i + 1, k].frameX = 88;
						Main.tile[i + 1, k].frameY = 88;
						break;
					case 2:
						Main.tile[i + 1, k].frameX = 88;
						Main.tile[i + 1, k].frameY = 110;
						break;
					}
				}
			}
			int num6 = genRand.Next(3);
			bool flag3 = false;
			bool flag4 = false;
			if (Main.tile[i - 1, j].active && (Main.tile[i - 1, j].type == 2 || Main.tile[i - 1, j].type == 23 || Main.tile[i - 1, j].type == 60 || Main.tile[i - 1, j].type == 109 || Main.tile[i - 1, j].type == 147))
			{
				flag3 = true;
			}
			if (Main.tile[i + 1, j].active && (Main.tile[i + 1, j].type == 2 || Main.tile[i + 1, j].type == 23 || Main.tile[i + 1, j].type == 60 || Main.tile[i + 1, j].type == 109 || Main.tile[i + 1, j].type == 147))
			{
				flag4 = true;
			}
			if (flag4 && !flag3)
			{
				num6 = 2;
			}
			else if (flag3 && !flag4)
			{
				num6 = 1;
			}
			else if (!flag4)
			{
				if (num6 == 0)
				{
					num6 = 1;
				}
				if (num6 == 2)
				{
					num6 = 3;
				}
			}
			else if (!flag3)
			{
				if (num6 == 0)
				{
					num6 = 2;
				}
				if (num6 == 1)
				{
					num6 = 3;
				}
			}
			if (num6 == 0 || num6 == 1)
			{
				Main.tile[i + 1, j - 1].active = true;
				Main.tile[i + 1, j - 1].type = 5;
				switch (genRand.Next(3))
				{
				case 0:
					Main.tile[i + 1, j - 1].frameX = 22;
					Main.tile[i + 1, j - 1].frameY = 132;
					break;
				case 1:
					Main.tile[i + 1, j - 1].frameX = 22;
					Main.tile[i + 1, j - 1].frameY = 154;
					break;
				case 2:
					Main.tile[i + 1, j - 1].frameX = 22;
					Main.tile[i + 1, j - 1].frameY = 176;
					break;
				}
			}
			if (num6 == 0 || num6 == 2)
			{
				Main.tile[i - 1, j - 1].active = true;
				Main.tile[i - 1, j - 1].type = 5;
				switch (genRand.Next(3))
				{
				case 0:
					Main.tile[i - 1, j - 1].frameX = 44;
					Main.tile[i - 1, j - 1].frameY = 132;
					break;
				case 1:
					Main.tile[i - 1, j - 1].frameX = 44;
					Main.tile[i - 1, j - 1].frameY = 154;
					break;
				case 2:
					Main.tile[i - 1, j - 1].frameX = 44;
					Main.tile[i - 1, j - 1].frameY = 176;
					break;
				}
			}
			num4 = genRand.Next(3);
			switch (num6)
			{
			case 0:
				switch (num4)
				{
				case 0:
					Main.tile[i, j - 1].frameX = 88;
					Main.tile[i, j - 1].frameY = 132;
					break;
				case 1:
					Main.tile[i, j - 1].frameX = 88;
					Main.tile[i, j - 1].frameY = 154;
					break;
				case 2:
					Main.tile[i, j - 1].frameX = 88;
					Main.tile[i, j - 1].frameY = 176;
					break;
				}
				break;
			case 1:
				switch (num4)
				{
				case 0:
					Main.tile[i, j - 1].frameX = 0;
					Main.tile[i, j - 1].frameY = 132;
					break;
				case 1:
					Main.tile[i, j - 1].frameX = 0;
					Main.tile[i, j - 1].frameY = 154;
					break;
				case 2:
					Main.tile[i, j - 1].frameX = 0;
					Main.tile[i, j - 1].frameY = 176;
					break;
				}
				break;
			case 2:
				switch (num4)
				{
				case 0:
					Main.tile[i, j - 1].frameX = 66;
					Main.tile[i, j - 1].frameY = 132;
					break;
				case 1:
					Main.tile[i, j - 1].frameX = 66;
					Main.tile[i, j - 1].frameY = 154;
					break;
				case 2:
					Main.tile[i, j - 1].frameX = 66;
					Main.tile[i, j - 1].frameY = 176;
					break;
				}
				break;
			}
			if (genRand.Next(4) < 3)
			{
				switch (genRand.Next(3))
				{
				case 0:
					Main.tile[i, j - num3].frameX = 22;
					Main.tile[i, j - num3].frameY = 198;
					break;
				case 1:
					Main.tile[i, j - num3].frameX = 22;
					Main.tile[i, j - num3].frameY = 220;
					break;
				case 2:
					Main.tile[i, j - num3].frameX = 22;
					Main.tile[i, j - num3].frameY = 242;
					break;
				}
			}
			else
			{
				switch (genRand.Next(3))
				{
				case 0:
					Main.tile[i, j - num3].frameX = 0;
					Main.tile[i, j - num3].frameY = 198;
					break;
				case 1:
					Main.tile[i, j - num3].frameX = 0;
					Main.tile[i, j - num3].frameY = 220;
					break;
				case 2:
					Main.tile[i, j - num3].frameX = 0;
					Main.tile[i, j - num3].frameY = 242;
					break;
				}
			}
			RangeFrame(i - 2, j - num3 - 1, i + 2, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, (int)((float)j - (float)num3 * 0.5f), num3 + 1);
			}
		}

		public static void GrowShroom(int i, int y)
		{
			if ((TMod.RunMethod(TMod.WorldHooks.GrowShroom, i, y) && !TMod.GetContinueMethod()) || Main.tile[i - 1, y - 1].lava || Main.tile[i - 1, y - 1].lava || Main.tile[i + 1, y - 1].lava || !Main.tile[i, y].active || Main.tile[i, y].type != 70 || Main.tile[i, y - 1].wall != 0 || !Main.tile[i - 1, y].active || Main.tile[i - 1, y].type != 70 || !Main.tile[i + 1, y].active || Main.tile[i + 1, y].type != 70 || !EmptyTileCheck(i - 2, i + 2, y - 13, y - 1, 71))
			{
				return;
			}
			int num = genRand.Next(4, 11);
			for (int j = y - num; j < y; j++)
			{
				Main.tile[i, j].frameNumber = (byte)genRand.Next(3);
				Main.tile[i, j].active = true;
				Main.tile[i, j].type = 72;
				switch (genRand.Next(3))
				{
				case 0:
					Main.tile[i, j].frameX = 0;
					Main.tile[i, j].frameY = 0;
					break;
				case 1:
					Main.tile[i, j].frameX = 0;
					Main.tile[i, j].frameY = 18;
					break;
				case 2:
					Main.tile[i, j].frameX = 0;
					Main.tile[i, j].frameY = 36;
					break;
				}
			}
			switch (genRand.Next(3))
			{
			case 0:
				Main.tile[i, y - num].frameX = 36;
				Main.tile[i, y - num].frameY = 0;
				break;
			case 1:
				Main.tile[i, y - num].frameX = 36;
				Main.tile[i, y - num].frameY = 18;
				break;
			case 2:
				Main.tile[i, y - num].frameX = 36;
				Main.tile[i, y - num].frameY = 36;
				break;
			}
			RangeFrame(i - 2, y - num - 1, i + 2, y + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, (int)((float)y - (float)num * 0.5f), num + 1);
			}
		}

		public static void AddTrees()
		{
			if (Codable.RunGlobalMethod("ModWorld", "AddTrees") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			for (int i = 1; i < Main.maxTilesX - 1; i++)
			{
				for (int j = 20; (double)j < Main.worldSurface; j++)
				{
					GrowTree(i, j);
				}
				if (genRand.Next(3) == 0)
				{
					i++;
				}
				if (genRand.Next(4) == 0)
				{
					i++;
				}
			}
		}

		public static bool EmptyTileCheck(int startX, int endX, int startY, int endY, int ignoreStyle = -1)
		{
			if (TMod.RunMethod(TMod.WorldHooks.EmptyTileCheck, startX, endX, startY, endY, ignoreStyle) && !TMod.GetContinueMethod())
			{
				return TMod.GetMethodReturn();
			}
			if (startX < 0)
			{
				return false;
			}
			if (endX >= Main.maxTilesX)
			{
				return false;
			}
			if (startY < 0)
			{
				return false;
			}
			if (endY >= Main.maxTilesY)
			{
				return false;
			}
			for (int i = startX; i < endX + 1; i++)
			{
				for (int j = startY; j < endY + 1; j++)
				{
					if (!Main.tile[i, j].active)
					{
						continue;
					}
					switch (ignoreStyle)
					{
					case -1:
						return false;
					case 11:
						if (Main.tile[i, j].type != 11)
						{
							return false;
						}
						break;
					}
					if (ignoreStyle == 20 && Main.tile[i, j].type != 20 && Main.tile[i, j].type != 3 && Main.tile[i, j].type != 24 && Main.tile[i, j].type != 61 && Main.tile[i, j].type != 32 && Main.tile[i, j].type != 69 && Main.tile[i, j].type != 73 && Main.tile[i, j].type != 74 && Main.tile[i, j].type != 110 && Main.tile[i, j].type != 113)
					{
						return false;
					}
					if (ignoreStyle == 71 && Main.tile[i, j].type != 71)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static void smCallBack(object threadContext)
		{
			if ((!Codable.RunGlobalMethod("ModWorld", "StartHardMode") || (bool)Codable.customMethodReturn) && !Main.hardMode)
			{
				hardLock = true;
				Main.hardMode = true;
				if (genRand == null)
				{
					genRand = new Random((int)DateTime.Now.Ticks);
				}
				float num = (float)genRand.Next(300, 400) * 0.001f;
				int i = (int)((float)Main.maxTilesX * num);
				int i2 = (int)((float)Main.maxTilesX * (1f - num));
				int num2 = 1;
				if (genRand.Next(2) == 0)
				{
					i2 = (int)((float)Main.maxTilesX * num);
					i = (int)((float)Main.maxTilesX * (1f - num));
					num2 = -1;
				}
				GERunner(i, 0, 3 * num2, 5f);
				GERunner(i2, 0, 3 * -num2, 5f, good: false);
				if (Main.netMode == 0)
				{
					Main.NewText(Lang.misc[15], 50, byte.MaxValue, 130);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Lang.misc[15], 255, 50f, 255f, 130f);
					Netplay.ResetSections();
				}
				hardLock = false;
			}
		}

		public static void StartHardmode()
		{
			if (Main.netMode != 1)
			{
				ThreadPool.QueueUserWorkItem(smCallBack, 1);
			}
		}

		public static bool PlaceDoor(int i, int j, int type)
		{
			if (Codable.RunGlobalMethod("ModWorld", "PlaceDoor", i, j, type) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			try
			{
				if (Main.tile[i, j - 2].active && Main.tileSolid[Main.tile[i, j - 2].type] && Main.tile[i, j + 2].active && Main.tileSolid[Main.tile[i, j + 2].type])
				{
					Main.tile[i, j - 1].active = true;
					Main.tile[i, j - 1].type = (ushort)type;
					Main.tile[i, j - 1].frameY = 0;
					Main.tile[i, j - 1].frameX = (short)(genRand.Next(3) * 18);
					Main.tile[i, j].active = true;
					Main.tile[i, j].type = (ushort)type;
					Main.tile[i, j].frameY = 18;
					Main.tile[i, j].frameX = (short)(genRand.Next(3) * 18);
					Main.tile[i, j + 1].active = true;
					Main.tile[i, j + 1].type = (ushort)type;
					Main.tile[i, j + 1].frameY = 36;
					Main.tile[i, j + 1].frameX = (short)(genRand.Next(3) * 18);
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static bool CloseDoor(int i, int j, bool forced = false)
		{
			if (Codable.RunGlobalMethod("ModWorld", "CloseDoor", i, j, forced) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			int type = Main.tile[i, j].type;
			if (Config.tileDefs.doorToggle.ContainsKey(type))
			{
				if (Config.tileDefs.doorType[type] == 2)
				{
					return Config.CloseCustomDoor(i, j, Config.tileDefs.doorToggle[type], forced);
				}
				return false;
			}
			int num = 0;
			int num2 = i;
			int num3 = j;
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			int frameX = Main.tile[i, j].frameX;
			int frameY = Main.tile[i, j].frameY;
			switch (frameX)
			{
			case 0:
				num2 = i;
				num = 1;
				break;
			case 18:
				num2 = i - 1;
				num = 1;
				break;
			case 36:
				num2 = i + 1;
				num = -1;
				break;
			case 54:
				num2 = i;
				num = -1;
				break;
			}
			switch (frameY)
			{
			case 0:
				num3 = j;
				break;
			case 18:
				num3 = j - 1;
				break;
			case 36:
				num3 = j - 2;
				break;
			}
			int num4 = num2;
			if (num == -1)
			{
				num4 = num2 - 1;
			}
			if (!forced)
			{
				for (int k = num3; k < num3 + 3; k++)
				{
					if (!Collision.EmptyTile(num2, k, ignoreTiles: true))
					{
						return false;
					}
				}
			}
			for (int l = num4; l < num4 + 2; l++)
			{
				for (int m = num3; m < num3 + 3; m++)
				{
					if (l == num2)
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						Main.tile[l, m].type = 10;
						Main.tile[l, m].frameX = (short)(genRand.Next(3) * 18);
					}
					else
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						Main.tile[l, m].active = false;
					}
				}
			}
			if (Main.netMode != 1)
			{
				int num5 = num2;
				for (int n = num3; n <= num3 + 2; n++)
				{
					if (numNoWire < maxWire - 1)
					{
						noWireX[numNoWire] = num5;
						noWireY[numNoWire] = n;
						numNoWire++;
					}
				}
			}
			for (int num6 = num2 - 1; num6 <= num2 + 1; num6++)
			{
				for (int num7 = num3 - 1; num7 <= num3 + 2; num7++)
				{
					TileFrame(num6, num7);
				}
			}
			Main.PlaySound(9, i * 16, j * 16);
			return true;
		}

		public static bool AddLifeCrystal(int i, int j)
		{
			if (Codable.RunGlobalMethod("ModWorld", "AddLifeCrystal", i, j) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			for (int k = j; k < Main.maxTilesY; k++)
			{
				if (Main.tile[i, k].active && Main.tileSolid[Main.tile[i, k].type])
				{
					int num = k - 1;
					if (Main.tile[i, num - 1].lava || Main.tile[i - 1, num - 1].lava)
					{
						return false;
					}
					if (!EmptyTileCheck(i - 1, i, num - 1, num))
					{
						return false;
					}
					Main.tile[i - 1, num - 1].active = true;
					Main.tile[i - 1, num - 1].type = 12;
					Main.tile[i - 1, num - 1].frameX = 0;
					Main.tile[i - 1, num - 1].frameY = 0;
					Main.tile[i, num - 1].active = true;
					Main.tile[i, num - 1].type = 12;
					Main.tile[i, num - 1].frameX = 18;
					Main.tile[i, num - 1].frameY = 0;
					Main.tile[i - 1, num].active = true;
					Main.tile[i - 1, num].type = 12;
					Main.tile[i - 1, num].frameX = 0;
					Main.tile[i - 1, num].frameY = 18;
					Main.tile[i, num].active = true;
					Main.tile[i, num].type = 12;
					Main.tile[i, num].frameX = 18;
					Main.tile[i, num].frameY = 18;
					return true;
				}
			}
			return false;
		}

		public static void AddShadowOrb(int x, int y)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "AddShadowOrb", x, y) && !(bool)Codable.customMethodReturn) || x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesY - 10)
			{
				return;
			}
			for (int i = x - 1; i < x + 1; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j].active && Main.tile[i, j].type == 31)
					{
						return;
					}
				}
			}
			Main.tile[x - 1, y - 1].active = true;
			Main.tile[x - 1, y - 1].type = 31;
			Main.tile[x - 1, y - 1].frameX = 0;
			Main.tile[x - 1, y - 1].frameY = 0;
			Main.tile[x, y - 1].active = true;
			Main.tile[x, y - 1].type = 31;
			Main.tile[x, y - 1].frameX = 18;
			Main.tile[x, y - 1].frameY = 0;
			Main.tile[x - 1, y].active = true;
			Main.tile[x - 1, y].type = 31;
			Main.tile[x - 1, y].frameX = 0;
			Main.tile[x - 1, y].frameY = 18;
			Main.tile[x, y].active = true;
			Main.tile[x, y].type = 31;
			Main.tile[x, y].frameX = 18;
			Main.tile[x, y].frameY = 18;
		}

		public static void AddHellHouses()
		{
			if (Codable.RunGlobalMethod("ModWorld", "AddHellHouses") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			int num = (int)((float)Main.maxTilesX * 0.25f);
			for (int i = num; i < Main.maxTilesX - num; i++)
			{
				int num2 = Main.maxTilesY - 40;
				while (Main.tile[i, num2].active || Main.tile[i, num2].liquid > 0)
				{
					num2--;
				}
				if (Main.tile[i, num2 + 1].active)
				{
					byte b = (byte)genRand.Next(75, 77);
					byte wall = 13;
					if (genRand.Next(5) > 0)
					{
						b = 75;
					}
					if (b == 75)
					{
						wall = 14;
					}
					HellHouse(i, num2, b, wall);
					i += genRand.Next(15, 80);
				}
			}
			float num3 = Main.maxTilesX / 4200;
			for (int j = 0; (float)j < 200f * num3; j++)
			{
				int num4 = 0;
				bool flag = false;
				while (!flag)
				{
					num4++;
					int num5 = genRand.Next((int)((float)Main.maxTilesX * 0.2f), (int)((float)Main.maxTilesX * 0.8f));
					int num6 = genRand.Next((int)Main.hellLayer - 100, Main.maxTilesY - 20);
					if (Main.tile[num5, num6].active && (Main.tile[num5, num6].type == 75 || Main.tile[num5, num6].type == 76))
					{
						int num7 = 0;
						if (Main.tile[num5 - 1, num6].wall > 0)
						{
							num7 = -1;
						}
						else if (Main.tile[num5 + 1, num6].wall > 0)
						{
							num7 = 1;
						}
						if (!Main.tile[num5 + num7, num6].active && !Main.tile[num5 + num7, num6 + 1].active)
						{
							bool flag2 = false;
							for (int k = num5 - 8; k < num5 + 8; k++)
							{
								for (int l = num6 - 8; l < num6 + 8; l++)
								{
									if (Main.tile[k, l].active && Main.tile[k, l].type == 4)
									{
										flag2 = true;
										break;
									}
								}
							}
							if (!flag2)
							{
								PlaceTile(num5 + num7, num6, 4, mute: true, forced: true, -1, 7);
								flag = true;
							}
						}
					}
					if (num4 > 1000)
					{
						flag = true;
					}
				}
			}
		}

		public static void HellHouse(int i, int j, byte type = 76, byte wall = 13)
		{
			if (TMod.RunMethod(TMod.WorldHooks.HellHouse, i, j, type, wall) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(8, 20);
			int num2 = genRand.Next(1, 3);
			int num3 = genRand.Next(4, 13);
			int num4 = j;
			for (int k = 0; k < num2; k++)
			{
				int num5 = genRand.Next(5, 9);
				HellRoom(i, num4, num, num5, type, wall);
				num4 -= num5;
			}
			num4 = j;
			for (int l = 0; l < num3; l++)
			{
				int num6 = genRand.Next(5, 9);
				num4 += num6;
				HellRoom(i, num4, num, num6, type, wall);
			}
			for (int m = i - num / 2; m <= i + num / 2; m++)
			{
				for (num4 = j; num4 < Main.maxTilesY && ((Main.tile[m, num4].active && (Main.tile[m, num4].type == 76 || Main.tile[m, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14); num4++)
				{
				}
				int num7 = 6 + genRand.Next(3);
				while (num4 < Main.maxTilesY && !Main.tile[m, num4].active)
				{
					num7--;
					Main.tile[m, num4].active = true;
					Main.tile[m, num4].type = 57;
					num4++;
					if (num7 <= 0)
					{
						break;
					}
				}
			}
			int num8 = 0;
			int num9 = 0;
			for (num4 = j; num4 < Main.maxTilesY && ((Main.tile[i, num4].active && (Main.tile[i, num4].type == 76 || Main.tile[i, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14); num4++)
			{
			}
			num4--;
			num9 = num4;
			while ((Main.tile[i, num4].active && (Main.tile[i, num4].type == 76 || Main.tile[i, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14)
			{
				num4--;
				if (!Main.tile[i, num4].active || (Main.tile[i, num4].type != 76 && Main.tile[i, num4].type != 75))
				{
					continue;
				}
				int num10 = genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
				int num11 = genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
				if (num10 > num11)
				{
					int num12 = num10;
					num10 = num11;
					num11 = num12;
				}
				if (num10 == num11)
				{
					if (num10 < i)
					{
						num11++;
					}
					else
					{
						num10--;
					}
				}
				for (int n = num10; n <= num11; n++)
				{
					if (Main.tile[n, num4 - 1].wall == 13)
					{
						Main.tile[n, num4].wall = 13;
					}
					else if (Main.tile[n, num4 - 1].wall == 14)
					{
						Main.tile[n, num4].wall = 14;
					}
					Main.tile[n, num4].type = 19;
					Main.tile[n, num4].active = true;
				}
				num4--;
			}
			num8 = num4;
			float num13 = (num9 - num8) * num;
			float num14 = num13 * 0.02f;
			for (int num15 = 0; (float)num15 < num14; num15++)
			{
				int num16 = genRand.Next(i - num / 2, i + num / 2 + 1);
				int num17 = genRand.Next(num8, num9);
				int num18 = genRand.Next(3, 8);
				for (int num19 = num16 - num18; num19 <= num16 + num18; num19++)
				{
					for (int num20 = num17 - num18; num20 <= num17 + num18; num20++)
					{
						float num21 = Math.Abs(num19 - num16);
						float num22 = Math.Abs(num20 - num17);
						float num23 = (float)Math.Sqrt(num21 * num21 + num22 * num22);
						if (num23 < (float)num18 * 0.4f)
						{
							try
							{
								if (Main.tile[num19, num20].type == 76 || Main.tile[num19, num20].type == 19)
								{
									Main.tile[num19, num20].active = false;
								}
								Main.tile[num19, num20].wall = 0;
							}
							catch
							{
							}
						}
					}
				}
			}
		}

		public static void HellRoom(int i, int j, int width, int height, byte type = 76, byte wall = 13)
		{
			if ((TMod.RunMethod(TMod.WorldHooks.HellRoom, i, j, width, height, type, wall) && !TMod.GetContinueMethod()) || j > Main.maxTilesY - 40)
			{
				return;
			}
			for (int k = i - width / 2; k <= i + width / 2; k++)
			{
				for (int l = j - height; l <= j; l++)
				{
					try
					{
						Main.tile[k, l].active = true;
						Main.tile[k, l].type = type;
						Main.tile[k, l].liquid = 0;
						Main.tile[k, l].lava = false;
					}
					catch
					{
					}
				}
			}
			for (int m = i - width / 2 + 1; m <= i + width / 2 - 1; m++)
			{
				for (int n = j - height + 1; n <= j - 1; n++)
				{
					try
					{
						Main.tile[m, n].active = false;
						Main.tile[m, n].wall = wall;
						Main.tile[m, n].liquid = 0;
						Main.tile[m, n].lava = false;
					}
					catch
					{
					}
				}
			}
		}

		public static void MakeDungeon(int x, int y, int tileType = 41, int wallType = 7)
		{
			if (Codable.RunGlobalMethod("ModWorld", "MakeDungeon", x, y, tileType, wallType) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			int num = genRand.Next(3);
			int num2 = genRand.Next(3);
			switch (num)
			{
			case 1:
				tileType = 43;
				break;
			case 2:
				tileType = 44;
				break;
			}
			switch (num2)
			{
			case 1:
				wallType = 8;
				break;
			case 2:
				wallType = 9;
				break;
			}
			numDDoors = 0;
			numDPlats = 0;
			numDRooms = 0;
			dungeonX = x;
			dungeonY = y;
			dMinX = x;
			dMaxX = x;
			dMinY = y;
			dMaxY = y;
			dxStrength1 = genRand.Next(25, 30);
			dyStrength1 = genRand.Next(20, 25);
			dxStrength2 = genRand.Next(35, 50);
			dyStrength2 = genRand.Next(10, 15);
			float num3 = Main.maxTilesX / 60;
			num3 += (float)genRand.Next(0, (int)(num3 / 3f));
			float num4 = num3;
			int num5 = 5;
			DungeonRoom(dungeonX, dungeonY, tileType, wallType);
			while (num3 > 0f)
			{
				if (dungeonX < dMinX)
				{
					dMinX = dungeonX;
				}
				else if (dungeonX > dMaxX)
				{
					dMaxX = dungeonX;
				}
				if (dungeonY > dMaxY)
				{
					dMaxY = dungeonY;
				}
				num3 -= 1f;
				Main.statusText = Lang.gen[58] + " " + (int)((num4 - num3) / num4 * 60f) + "%";
				if (num5 > 0)
				{
					num5--;
				}
				if ((num5 == 0) & (genRand.Next(3) == 0))
				{
					num5 = 5;
					if (genRand.Next(2) == 0)
					{
						int num6 = dungeonX;
						int num7 = dungeonY;
						DungeonHalls(dungeonX, dungeonY, tileType, wallType);
						if (genRand.Next(2) == 0)
						{
							DungeonHalls(dungeonX, dungeonY, tileType, wallType);
						}
						DungeonRoom(dungeonX, dungeonY, tileType, wallType);
						dungeonX = num6;
						dungeonY = num7;
					}
					else
					{
						DungeonRoom(dungeonX, dungeonY, tileType, wallType);
					}
				}
				else
				{
					DungeonHalls(dungeonX, dungeonY, tileType, wallType);
				}
			}
			DungeonRoom(dungeonX, dungeonY, tileType, wallType);
			int num8 = dRoomX[0];
			int num9 = dRoomY[0];
			for (int i = 0; i < numDRooms; i++)
			{
				if (dRoomY[i] < num9)
				{
					num8 = dRoomX[i];
					num9 = dRoomY[i];
				}
			}
			dungeonX = num8;
			dungeonY = num9;
			dEnteranceX = num8;
			dSurface = false;
			num5 = 5;
			while (!dSurface)
			{
				if (num5 > 0)
				{
					num5--;
				}
				if (num5 == 0 && (double)dungeonY > Main.worldSurface + 50.0)
				{
					num5 = 10;
					int num10 = dungeonX;
					int num11 = dungeonY;
					DungeonHalls(dungeonX, dungeonY, tileType, wallType, forceX: true);
					DungeonRoom(dungeonX, dungeonY, tileType, wallType);
					dungeonX = num10;
					dungeonY = num11;
				}
				DungeonStairs(dungeonX, dungeonY, tileType, wallType);
			}
			DungeonEnt(dungeonX, dungeonY, tileType, wallType);
			Main.statusText = Lang.gen[58] + " 65%";
			for (int j = 0; j < numDRooms; j++)
			{
				for (int k = dRoomL[j]; k <= dRoomR[j]; k++)
				{
					if (!Main.tile[k, dRoomT[j] - 1].active)
					{
						DPlatX[numDPlats] = k;
						DPlatY[numDPlats] = dRoomT[j] - 1;
						numDPlats++;
						break;
					}
				}
				for (int l = dRoomL[j]; l <= dRoomR[j]; l++)
				{
					if (!Main.tile[l, dRoomB[j] + 1].active)
					{
						DPlatX[numDPlats] = l;
						DPlatY[numDPlats] = dRoomB[j] + 1;
						numDPlats++;
						break;
					}
				}
				for (int m = dRoomT[j]; m <= dRoomB[j]; m++)
				{
					if (!Main.tile[dRoomL[j] - 1, m].active)
					{
						DDoorX[numDDoors] = dRoomL[j] - 1;
						DDoorY[numDDoors] = m;
						DDoorPos[numDDoors] = -1;
						numDDoors++;
						break;
					}
				}
				for (int n = dRoomT[j]; n <= dRoomB[j]; n++)
				{
					if (!Main.tile[dRoomR[j] + 1, n].active)
					{
						DDoorX[numDDoors] = dRoomR[j] + 1;
						DDoorY[numDDoors] = n;
						DDoorPos[numDDoors] = 1;
						numDDoors++;
						break;
					}
				}
			}
			Main.statusText = Lang.gen[58] + " 70%";
			int num12 = 0;
			int num13 = 1000;
			int num14 = 0;
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num15 = genRand.Next(dMinX, dMaxX);
				int num16 = genRand.Next((int)Main.worldSurface + 25, dMaxY);
				int num17 = num15;
				if (Main.tile[num15, num16].wall == wallType && !Main.tile[num15, num16].active)
				{
					int num18 = 1;
					if (genRand.Next(2) == 0)
					{
						num18 = -1;
					}
					for (; !Main.tile[num15, num16].active; num16 += num18)
					{
					}
					if (Main.tile[num15 - 1, num16].active && Main.tile[num15 + 1, num16].active && !Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
					{
						num14++;
						int num19 = genRand.Next(5, 13);
						while (Main.tile[num15 - 1, num16].active && Main.tile[num15, num16 + num18].active && Main.tile[num15, num16].active && !Main.tile[num15, num16 - num18].active && num19 > 0)
						{
							Main.tile[num15, num16].type = 48;
							if (!Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
							{
								Main.tile[num15, num16 - num18].type = 48;
								Main.tile[num15, num16 - num18].active = true;
							}
							num15--;
							num19--;
						}
						num19 = genRand.Next(5, 13);
						num15 = num17 + 1;
						while (Main.tile[num15 + 1, num16].active && Main.tile[num15, num16 + num18].active && Main.tile[num15, num16].active && !Main.tile[num15, num16 - num18].active && num19 > 0)
						{
							Main.tile[num15, num16].type = 48;
							if (!Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
							{
								Main.tile[num15, num16 - num18].type = 48;
								Main.tile[num15, num16 - num18].active = true;
							}
							num15++;
							num19--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			Main.statusText = Lang.gen[58] + " 75%";
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num20 = genRand.Next(dMinX, dMaxX);
				int num21 = genRand.Next((int)Main.worldSurface + 25, dMaxY);
				int num22 = num21;
				if (Main.tile[num20, num21].wall == wallType && !Main.tile[num20, num21].active)
				{
					int num23 = 1;
					if (genRand.Next(2) == 0)
					{
						num23 = -1;
					}
					for (; num20 > 5 && num20 < Main.maxTilesX - 5 && !Main.tile[num20, num21].active; num20 += num23)
					{
					}
					if (Main.tile[num20, num21 - 1].active && Main.tile[num20, num21 + 1].active && !Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
					{
						num14++;
						int num24 = genRand.Next(5, 13);
						while (Main.tile[num20, num21 - 1].active && Main.tile[num20 + num23, num21].active && Main.tile[num20, num21].active && !Main.tile[num20 - num23, num21].active && num24 > 0)
						{
							Main.tile[num20, num21].type = 48;
							if (!Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
							{
								Main.tile[num20 - num23, num21].type = 48;
								Main.tile[num20 - num23, num21].active = true;
							}
							num21--;
							num24--;
						}
						num24 = genRand.Next(5, 13);
						num21 = num22 + 1;
						while (Main.tile[num20, num21 + 1].active && Main.tile[num20 + num23, num21].active && Main.tile[num20, num21].active && !Main.tile[num20 - num23, num21].active && num24 > 0)
						{
							Main.tile[num20, num21].type = 48;
							if (!Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
							{
								Main.tile[num20 - num23, num21].type = 48;
								Main.tile[num20 - num23, num21].active = true;
							}
							num21++;
							num24--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = Lang.gen[58] + " 80%";
			for (int num25 = 0; num25 < numDDoors; num25++)
			{
				int num26 = DDoorX[num25] - 10;
				int num27 = DDoorX[num25] + 10;
				int num28 = 100;
				int num29 = 0;
				for (int num30 = num26; num30 < num27; num30++)
				{
					bool flag = true;
					int num31 = DDoorY[num25];
					while (!Main.tile[num30, num31].active)
					{
						num31--;
					}
					if (!Main.tileDungeon[Main.tile[num30, num31].type])
					{
						flag = false;
					}
					int num32 = num31;
					for (num31 = DDoorY[num25]; !Main.tile[num30, num31].active; num31++)
					{
					}
					if (!Main.tileDungeon[Main.tile[num30, num31].type])
					{
						flag = false;
					}
					int num33 = num31;
					if (num33 - num32 < 3)
					{
						continue;
					}
					int num34 = num30 - 20;
					int num35 = num30 + 20;
					int num36 = num33 - 10;
					int num37 = num33 + 10;
					for (int num38 = num34; num38 < num35; num38++)
					{
						for (int num39 = num36; num39 < num37; num39++)
						{
							if (Main.tile[num38, num39].active && Main.tile[num38, num39].type == 10)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						for (int num40 = num33 - 3; num40 < num33; num40++)
						{
							for (int num41 = num30 - 3; num41 <= num30 + 3; num41++)
							{
								if (Main.tile[num41, num40].active)
								{
									flag = false;
									break;
								}
							}
						}
					}
					if (flag && num33 - num32 < 20)
					{
						bool flag2 = false;
						if (DDoorPos[num25] == 0 && num33 - num32 < num28)
						{
							flag2 = true;
						}
						else if (DDoorPos[num25] == -1 && num30 > num29)
						{
							flag2 = true;
						}
						else if (DDoorPos[num25] == 1 && (num30 < num29 || num29 == 0))
						{
							flag2 = true;
						}
						if (flag2)
						{
							num29 = num30;
							num28 = num33 - num32;
						}
					}
				}
				if (num28 >= 20)
				{
					continue;
				}
				int num42 = num29;
				int num43 = DDoorY[num25];
				int num44 = num43;
				for (; !Main.tile[num42, num43].active; num43++)
				{
					Main.tile[num42, num43].active = false;
				}
				while (!Main.tile[num42, num44].active)
				{
					num44--;
				}
				num43--;
				num44++;
				for (int num45 = num44; num45 < num43 - 2; num45++)
				{
					Main.tile[num42, num45].active = true;
					Main.tile[num42, num45].type = (ushort)tileType;
				}
				PlaceTile(num42, num43, 10, mute: true);
				num42--;
				int num46 = num43 - 3;
				while (!Main.tile[num42, num46].active)
				{
					num46--;
				}
				if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[Main.tile[num42, num46].type])
				{
					for (int num47 = num43 - 4 - genRand.Next(3); num47 > num46; num47--)
					{
						Main.tile[num42, num47].active = true;
						Main.tile[num42, num47].type = (ushort)tileType;
					}
				}
				num42 += 2;
				num46 = num43 - 3;
				while (!Main.tile[num42, num46].active)
				{
					num46--;
				}
				if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[Main.tile[num42, num46].type])
				{
					for (int num48 = num43 - 4 - genRand.Next(3); num48 > num46; num48--)
					{
						Main.tile[num42, num48].active = true;
						Main.tile[num42, num48].type = (ushort)tileType;
					}
				}
				num43++;
				num42--;
				Main.tile[num42 - 1, num43].active = true;
				Main.tile[num42 - 1, num43].type = (ushort)tileType;
				Main.tile[num42 + 1, num43].active = true;
				Main.tile[num42 + 1, num43].type = (ushort)tileType;
			}
			Main.statusText = Lang.gen[58] + " 85%";
			for (int num49 = 0; num49 < numDPlats; num49++)
			{
				int num50 = DPlatX[num49];
				int num51 = DPlatY[num49];
				int num52 = Main.maxTilesX;
				int num53 = 10;
				for (int num54 = num51 - 5; num54 <= num51 + 5; num54++)
				{
					int num55 = num50;
					int num56 = num50;
					bool flag3 = false;
					if (Main.tile[num55, num54].active)
					{
						flag3 = true;
					}
					else
					{
						while (!Main.tile[num55, num54].active)
						{
							num55--;
							if (!Main.tileDungeon[Main.tile[num55, num54].type])
							{
								flag3 = true;
							}
						}
						while (!Main.tile[num56, num54].active)
						{
							num56++;
							if (!Main.tileDungeon[Main.tile[num56, num54].type])
							{
								flag3 = true;
							}
						}
					}
					if (flag3 || num56 - num55 > num53)
					{
						continue;
					}
					bool flag4 = true;
					int num57 = num50 - num53 / 2 - 2;
					int num58 = num50 + num53 / 2 + 2;
					int num59 = num54 - 5;
					int num60 = num54 + 5;
					for (int num61 = num57; num61 <= num58; num61++)
					{
						for (int num62 = num59; num62 <= num60; num62++)
						{
							if (Main.tile[num61, num62].active && Main.tile[num61, num62].type == 19)
							{
								flag4 = false;
								break;
							}
						}
					}
					for (int num63 = num54 + 3; num63 >= num54 - 5; num63--)
					{
						if (Main.tile[num50, num63].active)
						{
							flag4 = false;
							break;
						}
					}
					if (flag4)
					{
						num52 = num54;
						break;
					}
				}
				if (num52 > num51 - 10 && num52 < num51 + 10)
				{
					int num64 = num50;
					int num65 = num52;
					int num66 = num50 + 1;
					while (!Main.tile[num64, num65].active)
					{
						Main.tile[num64, num65].active = true;
						Main.tile[num64, num65].type = 19;
						num64--;
					}
					for (; !Main.tile[num66, num65].active; num66++)
					{
						Main.tile[num66, num65].active = true;
						Main.tile[num66, num65].type = 19;
					}
				}
			}
			Main.statusText = Lang.gen[58] + " 90%";
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 20)
			{
				num12++;
				int num67 = genRand.Next(dMinX, dMaxX);
				int num68 = genRand.Next(dMinY, dMaxY);
				bool flag5 = true;
				if (Main.tile[num67, num68].wall == wallType && !Main.tile[num67, num68].active)
				{
					int num69 = 1;
					if (genRand.Next(2) == 0)
					{
						num69 = -1;
					}
					while (flag5 && !Main.tile[num67, num68].active)
					{
						num67 -= num69;
						if (num67 < 5 || num67 > Main.maxTilesX - 5)
						{
							flag5 = false;
						}
						else if (Main.tile[num67, num68].active && !Main.tileDungeon[Main.tile[num67, num68].type])
						{
							flag5 = false;
						}
					}
					if (flag5 && Main.tile[num67, num68].active && Main.tileDungeon[Main.tile[num67, num68].type] && Main.tile[num67, num68 - 1].active && Main.tileDungeon[Main.tile[num67, num68 - 1].type] && Main.tile[num67, num68 + 1].active && Main.tileDungeon[Main.tile[num67, num68 + 1].type])
					{
						num67 += num69;
						for (int num70 = num67 - 3; num70 <= num67 + 3; num70++)
						{
							for (int num71 = num68 - 3; num71 <= num68 + 3; num71++)
							{
								if (Main.tile[num70, num71].active && Main.tile[num70, num71].type == 19)
								{
									flag5 = false;
									break;
								}
							}
						}
						if (flag5 && !Main.tile[num67, num68 - 1].active && !Main.tile[num67, num68 - 2].active && !Main.tile[num67, num68 - 3].active)
						{
							int num72 = num67;
							int num73 = num67;
							for (; num72 > dMinX && num72 < dMaxX && !Main.tile[num72, num68].active && !Main.tile[num72, num68 - 1].active && !Main.tile[num72, num68 + 1].active; num72 += num69)
							{
							}
							num72 = Math.Abs(num67 - num72);
							bool flag6 = false;
							if (genRand.Next(2) == 0)
							{
								flag6 = true;
							}
							if (num72 > 5)
							{
								for (int num74 = genRand.Next(1, 4); num74 > 0; num74--)
								{
									Main.tile[num67, num68].active = true;
									Main.tile[num67, num68].type = 19;
									if (flag6)
									{
										PlaceTile(num67, num68 - 1, 50, mute: true);
										if (genRand.Next(50) == 0 && Main.tile[num67, num68 - 1].type == 50)
										{
											Main.tile[num67, num68 - 1].frameX = 90;
										}
									}
									num67 += num69;
								}
								num12 = 0;
								num14++;
								if (!flag6 && genRand.Next(2) == 0)
								{
									num67 = num73;
									num68--;
									int num75 = 0;
									if (genRand.Next(4) == 0)
									{
										num75 = 1;
									}
									switch (num75)
									{
									case 0:
										num75 = 13;
										break;
									case 1:
										num75 = 49;
										break;
									}
									PlaceTile(num67, num68, num75, mute: true);
									if (Main.tile[num67, num68].type == 13)
									{
										if (genRand.Next(2) == 0)
										{
											Main.tile[num67, num68].frameX = 18;
										}
										else
										{
											Main.tile[num67, num68].frameX = 36;
										}
									}
								}
							}
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = Lang.gen[58] + " 95%";
			int num76 = 0;
			for (int num77 = 0; num77 < numDRooms; num77++)
			{
				int num78 = 0;
				while (num78 < 1000)
				{
					int num79 = (int)((double)dRoomSize[num77] * 0.4);
					int i2 = dRoomX[num77] + genRand.Next(-num79, num79 + 1);
					int num80 = dRoomY[num77] + genRand.Next(-num79, num79 + 1);
					int num81 = 0;
					num76++;
					int style = 2;
					switch (num76)
					{
					case 1:
						num81 = 329;
						break;
					case 2:
						num81 = 155;
						break;
					case 3:
						num81 = 156;
						break;
					case 4:
						num81 = 157;
						break;
					case 5:
						num81 = 163;
						break;
					case 6:
						num81 = 113;
						break;
					case 7:
						num81 = 327;
						style = 0;
						break;
					default:
						num81 = 164;
						num76 = 0;
						break;
					}
					if ((double)num80 < Main.worldSurface + 50.0)
					{
						num81 = 327;
						style = 0;
					}
					if (num81 == 0 && genRand.Next(2) == 0)
					{
						num78 = 1000;
						continue;
					}
					if (AddBuriedChest(i2, num80, num81, notNearOtherChests: false, style))
					{
						num78 += 1000;
					}
					num78++;
				}
			}
			dMinX -= 25;
			dMaxX += 25;
			dMinY -= 25;
			dMaxY += 25;
			if (dMinX < 0)
			{
				dMinX = 0;
			}
			if (dMaxX > Main.maxTilesX)
			{
				dMaxX = Main.maxTilesX;
			}
			if (dMinY < 0)
			{
				dMinY = 0;
			}
			if (dMaxY > Main.maxTilesY)
			{
				dMaxY = Main.maxTilesY;
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			Vector2 position = default(Vector2);
			Vector2 position2 = default(Vector2);
			while (num14 < Main.maxTilesX / 150)
			{
				num12++;
				int num82 = genRand.Next(dMinX, dMaxX);
				int num83 = genRand.Next(dMinY, dMaxY);
				if (Main.tile[num82, num83].wall == wallType)
				{
					for (int num84 = num83; num84 > dMinY; num84--)
					{
						if (Main.tile[num82, num84 - 1].active && Main.tile[num82, num84 - 1].type == tileType)
						{
							bool flag7 = false;
							for (int num85 = num82 - 15; num85 < num82 + 15; num85++)
							{
								for (int num86 = num84 - 15; num86 < num84 + 15; num86++)
								{
									if (num85 > 0 && num85 < Main.maxTilesX && num86 > 0 && num86 < Main.maxTilesY && Main.tile[num85, num86].type == 42)
									{
										flag7 = true;
										break;
									}
								}
							}
							if (Main.tile[num82 - 1, num84].active || Main.tile[num82 + 1, num84].active || Main.tile[num82 - 1, num84 + 1].active || Main.tile[num82 + 1, num84 + 1].active || Main.tile[num82, num84 + 2].active)
							{
								flag7 = true;
							}
							if (flag7)
							{
								break;
							}
							Place1x2Top(num82, num84, 42);
							if (Main.tile[num82, num84].type != 42)
							{
								break;
							}
							num12 = 0;
							num14++;
							for (int num87 = 0; num87 < 1000; num87++)
							{
								int num88 = num82 + genRand.Next(-12, 13);
								int num89 = num84 + genRand.Next(3, 21);
								position.X = num88 * 16;
								position.Y = num89 * 16;
								position2.X = num82 * 16;
								position2.Y = num84 * 16 + 1;
								if (Main.tile[num88, num89].active || Main.tile[num88, num89 + 1].active || Main.tile[num88 - 1, num89].type == 48 || Main.tile[num88 + 1, num89].type == 48 || !Collision.CanHit(position, 16, 16, position2, 16, 16))
								{
									continue;
								}
								PlaceTile(num88, num89, 136, mute: true);
								if (!Main.tile[num88, num89].active)
								{
									continue;
								}
								while (num88 != num82 || num89 != num84)
								{
									Main.tile[num88, num89].wire = true;
									if (num88 > num82)
									{
										num88--;
									}
									else if (num88 < num82)
									{
										num88++;
									}
									Main.tile[num88, num89].wire = true;
									if (num89 > num84)
									{
										num89--;
									}
									else if (num89 < num84)
									{
										num89++;
									}
									Main.tile[num88, num89].wire = true;
								}
								if (Main.rand.Next(3) > 0)
								{
									Main.tile[num82, num84].frameX = 18;
									Main.tile[num82, num84 + 1].frameX = 18;
								}
								break;
							}
							break;
						}
					}
				}
				if (num12 > num13)
				{
					num14++;
					num12 = 0;
				}
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while ((float)num14 < (float)Main.maxTilesX * 0.002f)
			{
				num12++;
				int num90 = genRand.Next(dMinX, dMaxX);
				int num91 = genRand.Next(dMinY, dMaxY);
				if (Main.tile[num90, num91].wall == wallType && placeTrap(num90, num91, 0))
				{
					num12 = num13;
				}
				if (num12 > num13)
				{
					num14++;
					num12 = 0;
				}
			}
		}

		public static void DungeonStairs(int i, int j, int tileType, int wallType)
		{
			if (TMod.RunMethod(TMod.WorldHooks.DungeonStairs, i, j, tileType, wallType) && !TMod.GetContinueMethod())
			{
				return;
			}
			Vector2 zero = Vector2.Zero;
			int num = genRand.Next(5, 9);
			int num2 = 1;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num3 = genRand.Next(10, 30);
			num2 = ((i <= dEnteranceX) ? 1 : (-1));
			if (i > Main.maxTilesX - 400)
			{
				num2 = -1;
			}
			else if (i < 400)
			{
				num2 = 1;
			}
			zero.Y = -1f;
			zero.X = num2;
			if (genRand.Next(3) == 0)
			{
				zero.X *= 0.5f;
			}
			else if (genRand.Next(3) == 0)
			{
				zero.Y *= 2f;
			}
			while (num3 > 0)
			{
				num3--;
				int num4 = (int)(vector.X - (float)num - 4f - (float)genRand.Next(6));
				int num5 = (int)(vector.X + (float)num + 4f + (float)genRand.Next(6));
				int num6 = (int)(vector.Y - (float)num - 4f);
				int num7 = (int)(vector.Y + (float)num + 4f + (float)genRand.Next(6));
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				int num8 = 1;
				if (vector.X > (float)(Main.maxTilesX / 2))
				{
					num8 = -1;
				}
				int num9 = (int)(vector.X + (float)dxStrength1 * 0.6f * (float)num8 + (float)(dxStrength2 * num8));
				int num10 = (int)((float)dyStrength2 * 0.5f);
				if ((double)vector.Y < Main.worldSurface - 5.0 && Main.tile[num9, (int)(vector.Y - (float)num - 6f + (float)num10)].wall == 0 && Main.tile[num9, (int)((double)vector.Y - (double)num - 7.0 + (double)num10)].wall == 0 && Main.tile[num9, (int)((double)vector.Y - (double)num - 8.0 + (double)num10)].wall == 0)
				{
					dSurface = true;
					TileRunner(num9, (int)(vector.Y - (float)num - 6f + (float)num10), genRand.Next(25, 35), genRand.Next(10, 20), -1, addTile: false, 0f, -1f);
				}
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						Main.tile[k, l].liquid = 0;
						if (Main.tile[k, l].wall != wallType)
						{
							Main.tile[k, l].wall = 0;
							Main.tile[k, l].active = true;
							Main.tile[k, l].type = (ushort)tileType;
						}
					}
				}
				for (int m = num4 + 1; m < num5 - 1; m++)
				{
					for (int n = num6 + 1; n < num7 - 1; n++)
					{
						PlaceWall(m, n, wallType, mute: true);
					}
				}
				int num11 = 0;
				if (genRand.Next(num) == 0)
				{
					num11 = genRand.Next(1, 3);
				}
				num4 = (int)(vector.X - (float)num * 0.5f - (float)num11);
				num5 = (int)(vector.X + (float)num * 0.5f + (float)num11);
				num6 = (int)(vector.Y - (float)num * 0.5f - (float)num11);
				num7 = (int)(vector.Y + (float)num * 0.5f + (float)num11);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int num12 = num4; num12 < num5; num12++)
				{
					for (int num13 = num6; num13 < num7; num13++)
					{
						Main.tile[num12, num13].active = false;
						PlaceWall(num12, num13, wallType, mute: true);
					}
				}
				if (dSurface)
				{
					num3 = 0;
				}
				vector += zero;
			}
			dungeonX = (int)vector.X;
			dungeonY = (int)vector.Y;
		}

		public static void DungeonHalls(int i, int j, int tileType, int wallType, bool forceX = false)
		{
			if (TMod.RunMethod(TMod.WorldHooks.DungeonHalls, i, j, tileType, wallType, forceX) && !TMod.GetContinueMethod())
			{
				return;
			}
			Vector2 zero = Vector2.Zero;
			int num = genRand.Next(4, 6);
			Vector2 zero2 = Vector2.Zero;
			Vector2 zero3 = Vector2.Zero;
			int num2 = 1;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num3 = genRand.Next(35, 80);
			if (forceX)
			{
				num3 += 20;
				lastDungeonHall = Vector2.Zero;
			}
			else if (genRand.Next(5) == 0)
			{
				num *= 2;
				num3 /= 2;
			}
			bool flag = false;
			while (!flag)
			{
				num2 = ((genRand.Next(2) != 0) ? 1 : (-1));
				bool flag2 = genRand.Next(2) == 0;
				if (forceX)
				{
					flag2 = true;
				}
				if (flag2)
				{
					zero2.X = num2;
					zero2.Y = 0f;
					zero3.X = -num2;
					zero3.Y = 0f;
					zero.X = num2;
					zero.Y = 0f;
					if (genRand.Next(3) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.Y = -0.2f;
						}
						else
						{
							zero.Y = 0.2f;
						}
					}
				}
				else
				{
					num++;
					zero.X = 0f;
					zero.Y = num2;
					zero2.X = 0f;
					zero2.Y = num2;
					zero3.X = 0f;
					zero3.Y = -num2;
					if (genRand.Next(2) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.X = 0.3f;
						}
						else
						{
							zero.X = -0.3f;
						}
					}
					else
					{
						num3 /= 2;
					}
				}
				if (lastDungeonHall != zero3)
				{
					flag = true;
				}
			}
			if (!forceX)
			{
				if (vector.X > (float)(lastMaxTilesX - 200))
				{
					num2 = -1;
					zero2.X = num2;
					zero2.Y = 0f;
					zero.X = num2;
					zero.Y = 0f;
					if (genRand.Next(3) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.Y = -0.2f;
						}
						else
						{
							zero.Y = 0.2f;
						}
					}
				}
				else if (vector.X < 200f)
				{
					num2 = 1;
					zero2.X = num2;
					zero2.Y = 0f;
					zero.X = num2;
					zero.Y = 0f;
					if (genRand.Next(3) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.Y = -0.2f;
						}
						else
						{
							zero.Y = 0.2f;
						}
					}
				}
				else if (vector.Y > (float)(lastMaxTilesY - 300))
				{
					num2 = -1;
					num++;
					zero.X = 0f;
					zero.Y = num2;
					zero2.X = 0f;
					zero2.Y = num2;
					if (genRand.Next(2) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.X = 0.3f;
						}
						else
						{
							zero.X = -0.3f;
						}
					}
				}
				else if ((double)vector.Y < Main.rockLayer)
				{
					num2 = 1;
					num++;
					zero.X = 0f;
					zero.Y = num2;
					zero2.X = 0f;
					zero2.Y = num2;
					if (genRand.Next(2) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.X = 0.3f;
						}
						else
						{
							zero.X = -0.3f;
						}
					}
				}
				else if (vector.X < (float)Main.maxTilesX * 0.5f && vector.X > (float)Main.maxTilesX * 0.25f)
				{
					num2 = -1;
					zero2.X = num2;
					zero2.Y = 0f;
					zero.X = num2;
					zero.Y = 0f;
					if (genRand.Next(3) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.Y = -0.2f;
						}
						else
						{
							zero.Y = 0.2f;
						}
					}
				}
				else if (vector.X > (float)Main.maxTilesX * 0.5f && vector.X < (float)Main.maxTilesX * 0.75f)
				{
					num2 = 1;
					zero2.X = num2;
					zero2.Y = 0f;
					zero.X = num2;
					zero.Y = 0f;
					if (genRand.Next(3) == 0)
					{
						if (genRand.Next(2) == 0)
						{
							zero.Y = -0.2f;
						}
						else
						{
							zero.Y = 0.2f;
						}
					}
				}
			}
			if (zero2.Y == 0f)
			{
				DDoorX[numDDoors] = (int)vector.X;
				DDoorY[numDDoors] = (int)vector.Y;
				DDoorPos[numDDoors] = 0;
				numDDoors++;
			}
			else
			{
				DPlatX[numDPlats] = (int)vector.X;
				DPlatY[numDPlats] = (int)vector.Y;
				numDPlats++;
			}
			lastDungeonHall = zero2;
			while (num3 > 0)
			{
				if (zero2.X > 0f && vector.X > (float)(Main.maxTilesX - 100))
				{
					num3 = 0;
				}
				else if (zero2.X < 0f && vector.X < 100f)
				{
					num3 = 0;
				}
				else if (zero2.Y > 0f && vector.Y > (float)((int)Main.hellLayer + 100))
				{
					num3 = 0;
				}
				else if (zero2.Y < 0f && (double)vector.Y < Main.rockLayer + 50.0)
				{
					num3 = 0;
				}
				num3--;
				int num4 = (int)(vector.X - (float)num - 4f - (float)genRand.Next(6));
				int num5 = (int)(vector.X + (float)num + 4f + (float)genRand.Next(6));
				int num6 = (int)(vector.Y - (float)num - 4f - (float)genRand.Next(6));
				int num7 = (int)(vector.Y + (float)num + 4f + (float)genRand.Next(6));
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						Main.tile[k, l].liquid = 0;
						if (Main.tile[k, l].wall == 0)
						{
							Main.tile[k, l].active = true;
							Main.tile[k, l].type = (ushort)tileType;
						}
					}
				}
				for (int m = num4 + 1; m < num5 - 1; m++)
				{
					for (int n = num6 + 1; n < num7 - 1; n++)
					{
						PlaceWall(m, n, wallType, mute: true);
					}
				}
				int num8 = 0;
				if (zero.Y == 0f && genRand.Next(num + 1) == 0)
				{
					num8 = genRand.Next(1, 3);
				}
				else if (zero.X == 0f && genRand.Next(num - 1) == 0)
				{
					num8 = genRand.Next(1, 3);
				}
				else if (genRand.Next(num * 3) == 0)
				{
					num8 = genRand.Next(1, 3);
				}
				num4 = (int)(vector.X - (float)num * 0.5f - (float)num8);
				num5 = (int)(vector.X + (float)num * 0.5f + (float)num8);
				num6 = (int)(vector.Y - (float)num * 0.5f - (float)num8);
				num7 = (int)(vector.Y + (float)num * 0.5f + (float)num8);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int num9 = num4; num9 < num5; num9++)
				{
					for (int num10 = num6; num10 < num7; num10++)
					{
						Main.tile[num9, num10].active = false;
						Main.tile[num9, num10].wall = (byte)wallType;
					}
				}
				vector += zero;
			}
			dungeonX = (int)vector.X;
			dungeonY = (int)vector.Y;
			if (zero2.Y == 0f)
			{
				DDoorX[numDDoors] = (int)vector.X;
				DDoorY[numDDoors] = (int)vector.Y;
				DDoorPos[numDDoors] = 0;
				numDDoors++;
			}
			else
			{
				DPlatX[numDPlats] = (int)vector.X;
				DPlatY[numDPlats] = (int)vector.Y;
				numDPlats++;
			}
		}

		public static void DungeonRoom(int i, int j, int tileType, int wallType)
		{
			if (TMod.RunMethod(TMod.WorldHooks.DungeonRoom, i, j, tileType, wallType) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(15, 30);
			Vector2 vector = default(Vector2);
			vector.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector.Y = (float)genRand.Next(-10, 11) * 0.1f;
			Vector2 vector2 = default(Vector2);
			vector2.X = i;
			vector2.Y = (float)j - (float)num * 0.5f;
			int num2 = genRand.Next(10, 20);
			float num3 = vector2.X;
			float num4 = vector2.X;
			float num5 = vector2.Y;
			float num6 = vector2.Y;
			while (num2 > 0)
			{
				num2--;
				int num7 = (int)(vector2.X - (float)num * 0.8f - 5f);
				int num8 = (int)(vector2.X + (float)num * 0.8f + 5f);
				int num9 = (int)(vector2.Y - (float)num * 0.8f - 5f);
				int num10 = (int)(vector2.Y + (float)num * 0.8f + 5f);
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesX)
				{
					num8 = Main.maxTilesX;
				}
				if (num9 < 0)
				{
					num9 = 0;
				}
				if (num10 > Main.maxTilesY)
				{
					num10 = Main.maxTilesY;
				}
				for (int k = num7; k < num8; k++)
				{
					for (int l = num9; l < num10; l++)
					{
						Main.tile[k, l].liquid = 0;
						if (Main.tile[k, l].wall == 0)
						{
							Main.tile[k, l].active = true;
							Main.tile[k, l].type = (ushort)tileType;
						}
					}
				}
				for (int m = num7 + 1; m < num8 - 1; m++)
				{
					for (int n = num9 + 1; n < num10 - 1; n++)
					{
						PlaceWall(m, n, wallType, mute: true);
					}
				}
				num7 = (int)(vector2.X - (float)num * 0.5f);
				num8 = (int)(vector2.X + (float)num * 0.5f);
				num9 = (int)(vector2.Y - (float)num * 0.5f);
				num10 = (int)(vector2.Y + (float)num * 0.5f);
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesX)
				{
					num8 = Main.maxTilesX;
				}
				if (num9 < 0)
				{
					num9 = 0;
				}
				if (num10 > Main.maxTilesY)
				{
					num10 = Main.maxTilesY;
				}
				if ((float)num7 < num3)
				{
					num3 = num7;
				}
				if ((float)num8 > num4)
				{
					num4 = num8;
				}
				if ((float)num9 < num5)
				{
					num5 = num9;
				}
				if ((float)num10 > num6)
				{
					num6 = num10;
				}
				for (int num11 = num7; num11 < num8; num11++)
				{
					for (int num12 = num9; num12 < num10; num12++)
					{
						Main.tile[num11, num12].active = false;
						Main.tile[num11, num12].wall = (byte)wallType;
					}
				}
				vector2 += vector;
				vector.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector.X > 1f)
				{
					vector.X = 1f;
				}
				else if (vector.X < -1f)
				{
					vector.X = -1f;
				}
				if (vector.Y > 1f)
				{
					vector.Y = 1f;
				}
				else if (vector.Y < -1f)
				{
					vector.Y = -1f;
				}
			}
			dRoomX[numDRooms] = (int)vector2.X;
			dRoomY[numDRooms] = (int)vector2.Y;
			dRoomSize[numDRooms] = num;
			dRoomL[numDRooms] = (int)num3;
			dRoomR[numDRooms] = (int)num4;
			dRoomT[numDRooms] = (int)num5;
			dRoomB[numDRooms] = (int)num6;
			dRoomTreasure[numDRooms] = false;
			numDRooms++;
		}

		public static void DungeonEnt(int i, int j, int tileType, int wallType)
		{
			if (Codable.RunGlobalMethod("ModWorld", "DungeonEnt", i, j, tileType, wallType) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			int num = 60;
			for (int k = i - num; k < i + num; k++)
			{
				for (int l = j - num; l < j + num; l++)
				{
					Main.tile[k, l].liquid = 0;
					Main.tile[k, l].lava = false;
				}
			}
			int num2 = dxStrength1;
			int num3 = dyStrength1;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j - (float)num3 * 0.5f;
			dMinY = (int)vector.Y;
			int num4 = 1;
			if (i > Main.maxTilesX / 2)
			{
				num4 = -1;
			}
			int num5 = (int)(vector.X - (float)num2 * 0.6f - (float)genRand.Next(2, 5));
			int num6 = (int)(vector.X + (float)num2 * 0.6f + (float)genRand.Next(2, 5));
			int num7 = (int)(vector.Y - (float)num3 * 0.6f - (float)genRand.Next(2, 5));
			int num8 = (int)(vector.Y + (float)num3 * 0.6f + (float)genRand.Next(8, 16));
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
			for (int m = num5; m < num6; m++)
			{
				for (int n = num7; n < num8; n++)
				{
					Main.tile[m, n].liquid = 0;
					if (Main.tile[m, n].wall != wallType)
					{
						Main.tile[m, n].wall = 0;
						if (m > num5 + 1 && m < num6 - 2 && n > num7 + 1 && n < num8 - 2)
						{
							PlaceWall(m, n, wallType, mute: true);
						}
						Main.tile[m, n].active = true;
						Main.tile[m, n].type = (ushort)tileType;
					}
				}
			}
			int num9 = num5;
			int num10 = num5 + 5 + genRand.Next(4);
			int num11 = num7 - 3 - genRand.Next(3);
			int num12 = num7;
			for (int num13 = num9; num13 < num10; num13++)
			{
				for (int num14 = num11; num14 < num12; num14++)
				{
					if (Main.tile[num13, num14].wall != wallType)
					{
						Main.tile[num13, num14].active = true;
						Main.tile[num13, num14].type = (ushort)tileType;
					}
				}
			}
			num9 = num6 - 5 - genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - genRand.Next(3);
			num12 = num7;
			for (int num15 = num9; num15 < num10; num15++)
			{
				for (int num16 = num11; num16 < num12; num16++)
				{
					if (Main.tile[num15, num16].wall != wallType)
					{
						Main.tile[num15, num16].active = true;
						Main.tile[num15, num16].type = (ushort)tileType;
					}
				}
			}
			int num17 = 1 + genRand.Next(2);
			int num18 = 2 + genRand.Next(4);
			int num19 = 0;
			for (int num20 = num5; num20 < num6; num20++)
			{
				for (int num21 = num7 - num17; num21 < num7; num21++)
				{
					if (Main.tile[num20, num21].wall != wallType)
					{
						Main.tile[num20, num21].active = true;
						Main.tile[num20, num21].type = (ushort)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num20 += num18;
					num19 = 0;
				}
			}
			for (int num22 = num5; num22 < num6; num22++)
			{
				for (int num23 = num8; num23 < num8 + 100; num23++)
				{
					PlaceWall(num22, num23, 2, mute: true);
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.6f);
			num6 = (int)(vector.X + (float)num2 * 0.6f);
			num7 = (int)(vector.Y - (float)num3 * 0.6f);
			num8 = (int)(vector.Y + (float)num3 * 0.6f);
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
			for (int num24 = num5; num24 < num6; num24++)
			{
				for (int num25 = num7; num25 < num8; num25++)
				{
					PlaceWall(num24, num25, wallType, mute: true);
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.6f - 1f);
			num6 = (int)(vector.X + (float)num2 * 0.6f + 1f);
			num7 = (int)(vector.Y - (float)num3 * 0.6f - 1f);
			num8 = (int)(vector.Y + (float)num3 * 0.6f + 1f);
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
			for (int num26 = num5; num26 < num6; num26++)
			{
				for (int num27 = num7; num27 < num8; num27++)
				{
					Main.tile[num26, num27].wall = (ushort)wallType;
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.5f);
			num6 = (int)(vector.X + (float)num2 * 0.5f);
			num7 = (int)(vector.Y - (float)num3 * 0.5f);
			num8 = (int)(vector.Y + (float)num3 * 0.5f);
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
			for (int num28 = num5; num28 < num6; num28++)
			{
				for (int num29 = num7; num29 < num8; num29++)
				{
					Main.tile[num28, num29].active = false;
					Main.tile[num28, num29].wall = (ushort)wallType;
				}
			}
			DPlatX[numDPlats] = (int)vector.X;
			DPlatY[numDPlats] = num8;
			numDPlats++;
			vector.X += (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = dxStrength2;
			num3 = dyStrength2;
			vector.X += (float)num2 * 0.55f * (float)num4;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)(vector.X - (float)num2 * 0.6f - (float)genRand.Next(1, 3));
			num6 = (int)(vector.X + (float)num2 * 0.6f + (float)genRand.Next(1, 3));
			num7 = (int)(vector.Y - (float)num3 * 0.6f - (float)genRand.Next(1, 3));
			num8 = (int)(vector.Y + (float)num3 * 0.6f + (float)genRand.Next(6, 16));
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
			for (int num30 = num5; num30 < num6; num30++)
			{
				for (int num31 = num7; num31 < num8; num31++)
				{
					if (Main.tile[num30, num31].wall == wallType)
					{
						continue;
					}
					bool flag = true;
					if (num4 < 0)
					{
						if ((float)num30 < vector.X - (float)num2 * 0.5f)
						{
							flag = false;
						}
					}
					else if ((float)num30 > vector.X + (float)num2 * 0.5f - 1f)
					{
						flag = false;
					}
					if (flag)
					{
						Main.tile[num30, num31].wall = 0;
						Main.tile[num30, num31].active = true;
						Main.tile[num30, num31].type = (ushort)tileType;
					}
				}
			}
			for (int num32 = num5; num32 < num6; num32++)
			{
				for (int num33 = num8; num33 < num8 + 100; num33++)
				{
					PlaceWall(num32, num33, 2, mute: true);
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.5f);
			num6 = (int)(vector.X + (float)num2 * 0.5f);
			num9 = num5;
			if (num4 < 0)
			{
				num9++;
			}
			num10 = num9 + 5 + genRand.Next(4);
			num11 = num7 - 3 - genRand.Next(3);
			num12 = num7;
			for (int num34 = num9; num34 < num10; num34++)
			{
				for (int num35 = num11; num35 < num12; num35++)
				{
					if (Main.tile[num34, num35].wall != wallType)
					{
						Main.tile[num34, num35].active = true;
						Main.tile[num34, num35].type = (ushort)tileType;
					}
				}
			}
			num9 = num6 - 5 - genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - genRand.Next(3);
			num12 = num7;
			for (int num36 = num9; num36 < num10; num36++)
			{
				for (int num37 = num11; num37 < num12; num37++)
				{
					if (Main.tile[num36, num37].wall != wallType)
					{
						Main.tile[num36, num37].active = true;
						Main.tile[num36, num37].type = (ushort)tileType;
					}
				}
			}
			num17 = 1 + genRand.Next(2);
			num18 = 2 + genRand.Next(4);
			num19 = 0;
			if (num4 < 0)
			{
				num6++;
			}
			for (int num38 = num5 + 1; num38 < num6 - 1; num38++)
			{
				for (int num39 = num7 - num17; num39 < num7; num39++)
				{
					if (Main.tile[num38, num39].wall != wallType)
					{
						Main.tile[num38, num39].active = true;
						Main.tile[num38, num39].type = (ushort)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num38 += num18;
					num19 = 0;
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.6f);
			num6 = (int)(vector.X + (float)num2 * 0.6f);
			num7 = (int)(vector.Y - (float)num3 * 0.6f);
			num8 = (int)(vector.Y + (float)num3 * 0.6f);
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
			for (int num40 = num5; num40 < num6; num40++)
			{
				for (int num41 = num7; num41 < num8; num41++)
				{
					Main.tile[num40, num41].wall = 0;
				}
			}
			num5 = (int)(vector.X - (float)num2 * 0.5f);
			num6 = (int)(vector.X + (float)num2 * 0.5f);
			num7 = (int)(vector.Y - (float)num3 * 0.5f);
			num8 = (int)(vector.Y + (float)num3 * 0.5f);
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
			for (int num42 = num5; num42 < num6; num42++)
			{
				for (int num43 = num7; num43 < num8; num43++)
				{
					Main.tile[num42, num43].active = false;
					Main.tile[num42, num43].wall = 0;
				}
			}
			for (int num44 = num5; num44 < num6; num44++)
			{
				if (!Main.tile[num44, num8].active)
				{
					Main.tile[num44, num8].active = true;
					Main.tile[num44, num8].type = 19;
				}
			}
			Main.dungeonX = (int)vector.X;
			Main.dungeonY = num8;
			int num45 = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37);
			Main.npc[num45].homeless = false;
			Main.npc[num45].homeTileX = Main.dungeonX;
			Main.npc[num45].homeTileY = Main.dungeonY;
			if (num4 == 1)
			{
				int num46 = 0;
				for (int num47 = num6; num47 < num6 + 25; num47++)
				{
					num46++;
					for (int num48 = num8 + num46; num48 < num8 + 25; num48++)
					{
						Main.tile[num47, num48].active = true;
						Main.tile[num47, num48].type = (ushort)tileType;
					}
				}
			}
			else
			{
				int num49 = 0;
				for (int num50 = num5; num50 > num5 - 25; num50--)
				{
					num49++;
					for (int num51 = num8 + num49; num51 < num8 + 25; num51++)
					{
						Main.tile[num50, num51].active = true;
						Main.tile[num50, num51].type = (ushort)tileType;
					}
				}
			}
			num17 = 1 + genRand.Next(2);
			num18 = 2 + genRand.Next(4);
			num19 = 0;
			num5 = (int)(vector.X - (float)num2 * 0.5f);
			num6 = (int)(vector.X + (float)num2 * 0.5f);
			num5 += 2;
			num6 -= 2;
			for (int num52 = num5; num52 < num6; num52++)
			{
				for (int num53 = num7; num53 < num8; num53++)
				{
					PlaceWall(num52, num53, wallType, mute: true);
				}
				num19++;
				if (num19 >= num18)
				{
					num52 += num18 * 2;
					num19 = 0;
				}
			}
			vector.X -= (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = 15;
			num3 = 3;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)(vector.X - (float)num2 * 0.5f);
			num6 = (int)(vector.X + (float)num2 * 0.5f);
			num7 = (int)(vector.Y - (float)num3 * 0.5f);
			num8 = (int)(vector.Y + (float)num3 * 0.5f);
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
			for (int num54 = num5; num54 < num6; num54++)
			{
				for (int num55 = num7; num55 < num8; num55++)
				{
					Main.tile[num54, num55].active = false;
				}
			}
			if (num4 < 0)
			{
				vector.X -= 1f;
			}
			PlaceTile((int)vector.X, (int)vector.Y + 1, 10);
		}

		public static bool AddBuriedChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
		{
			if (TMod.RunMethod(TMod.WorldHooks.AddBuriedChest, i, j, contain, notNearOtherChests, Style) && !TMod.GetContinueMethod())
			{
				return TMod.GetMethodReturn();
			}
			if (genRand == null)
			{
				genRand = new Random((int)DateTime.Now.Ticks);
			}
			for (int k = j; k < Main.maxTilesY; k++)
			{
				if (!Main.tile[i, k].active || !Main.tileSolid[Main.tile[i, k].type])
				{
					continue;
				}
				bool flag = false;
				int num = k;
				int style = 0;
				if ((double)num >= Main.worldSurface + 25.0 || contain > 0)
				{
					style = 1;
				}
				if (Style >= 0)
				{
					style = Style;
				}
				if ((double)num > Main.hellLayer - 5.0 && contain == 0)
				{
					if (hellChest == 0)
					{
						contain = 274;
						style = 4;
						flag = true;
					}
					else if (hellChest == 1)
					{
						contain = 220;
						style = 4;
						flag = true;
					}
					else if (hellChest == 2)
					{
						contain = 112;
						style = 4;
						flag = true;
					}
					else if (hellChest == 3)
					{
						contain = 218;
						style = 4;
						flag = true;
						hellChest = 0;
					}
				}
				int num2 = PlaceChest(i - 1, num - 1, 21, notNearOtherChests, style);
				if (num2 >= 0)
				{
					if (flag)
					{
						hellChest++;
					}
					int num3 = 0;
					while (num3 == 0)
					{
						if ((double)num < Main.worldSurface + 25.0)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								switch (genRand.Next(6))
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(280);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(281);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(284);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(282);
									Main.chest[num2].item[num3].stack = genRand.Next(50, 75);
									break;
								case 4:
									Main.chest[num2].item[num3].SetDefaults(279);
									Main.chest[num2].item[num3].stack = genRand.Next(25, 50);
									break;
								case 5:
									Main.chest[num2].item[num3].SetDefaults(285);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								}
								num3++;
							}
							if (genRand.Next(3) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(168);
								Main.chest[num2].item[num3].stack = genRand.Next(3, 6);
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num4 = genRand.Next(2);
								int stack = genRand.Next(8) + 3;
								switch (num4)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(20);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(22);
									break;
								}
								Main.chest[num2].item[num3].stack = stack;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num5 = genRand.Next(2);
								int stack2 = genRand.Next(26) + 25;
								switch (num5)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(40);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(42);
									break;
								}
								Main.chest[num2].item[num3].stack = stack2;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num6 = genRand.Next(1);
								int stack3 = genRand.Next(3) + 3;
								if (num6 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(28);
								}
								Main.chest[num2].item[num3].stack = stack3;
								num3++;
							}
							if (genRand.Next(3) > 0)
							{
								int num7 = genRand.Next(4);
								int stack4 = genRand.Next(1, 3);
								switch (num7)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(292);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(298);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(299);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(290);
									break;
								}
								Main.chest[num2].item[num3].stack = stack4;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num8 = genRand.Next(2);
								int stack5 = genRand.Next(11) + 10;
								switch (num8)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(8);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(31);
									break;
								}
								Main.chest[num2].item[num3].stack = stack5;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(72);
								Main.chest[num2].item[num3].stack = genRand.Next(10, 30);
								num3++;
							}
							continue;
						}
						if ((double)num < Main.rockLayer)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								switch (genRand.Next(7))
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(49);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(50);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(52);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(53);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 4:
									Main.chest[num2].item[num3].SetDefaults(54);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 5:
									Main.chest[num2].item[num3].SetDefaults(55);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 6:
									Main.chest[num2].item[num3].SetDefaults(51);
									Main.chest[num2].item[num3].stack = genRand.Next(26) + 25;
									break;
								}
								num3++;
							}
							if (genRand.Next(3) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(166);
								Main.chest[num2].item[num3].stack = genRand.Next(10, 20);
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num9 = genRand.Next(2);
								int stack6 = genRand.Next(10) + 5;
								if (num9 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(22);
								}
								if (num9 == 1)
								{
									Main.chest[num2].item[num3].SetDefaults(21);
								}
								Main.chest[num2].item[num3].stack = stack6;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num10 = genRand.Next(2);
								int stack7 = genRand.Next(25) + 25;
								switch (num10)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(40);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(42);
									break;
								}
								Main.chest[num2].item[num3].stack = stack7;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num11 = genRand.Next(1);
								int stack8 = genRand.Next(3) + 3;
								if (num11 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(28);
								}
								Main.chest[num2].item[num3].stack = stack8;
								num3++;
							}
							if (genRand.Next(3) > 0)
							{
								int num12 = genRand.Next(7);
								int stack9 = genRand.Next(1, 3);
								switch (num12)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(289);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(298);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(299);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(290);
									break;
								case 4:
									Main.chest[num2].item[num3].SetDefaults(303);
									break;
								case 5:
									Main.chest[num2].item[num3].SetDefaults(291);
									break;
								case 6:
									Main.chest[num2].item[num3].SetDefaults(304);
									break;
								}
								Main.chest[num2].item[num3].stack = stack9;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int stack10 = genRand.Next(11) + 10;
								Main.chest[num2].item[num3].SetDefaults(8);
								Main.chest[num2].item[num3].stack = stack10;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(72);
								Main.chest[num2].item[num3].stack = genRand.Next(50, 90);
								num3++;
							}
							continue;
						}
						if ((double)num < Main.hellLayer - 50.0)
						{
							if (contain > 0)
							{
								Main.chest[num2].item[num3].SetDefaults(contain);
								Main.chest[num2].item[num3].Prefix(-1);
								num3++;
							}
							else
							{
								int num13 = genRand.Next(7);
								if (num13 == 2 && genRand.Next(2) == 0)
								{
									num13 = genRand.Next(7);
								}
								switch (num13)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(49);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(50);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(52);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(53);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 4:
									Main.chest[num2].item[num3].SetDefaults(54);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 5:
									Main.chest[num2].item[num3].SetDefaults(55);
									Main.chest[num2].item[num3].Prefix(-1);
									break;
								case 6:
									Main.chest[num2].item[num3].SetDefaults(51);
									Main.chest[num2].item[num3].stack = genRand.Next(26) + 25;
									break;
								}
								num3++;
							}
							if (genRand.Next(5) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(43);
								num3++;
							}
							if (genRand.Next(3) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(167);
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num14 = genRand.Next(2);
								int stack11 = genRand.Next(8) + 3;
								switch (num14)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(19);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(21);
									break;
								}
								Main.chest[num2].item[num3].stack = stack11;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num15 = genRand.Next(2);
								int stack12 = genRand.Next(26) + 25;
								switch (num15)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(41);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(279);
									break;
								}
								Main.chest[num2].item[num3].stack = stack12;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num16 = genRand.Next(1);
								int stack13 = genRand.Next(3) + 3;
								if (num16 == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(188);
								}
								Main.chest[num2].item[num3].stack = stack13;
								num3++;
							}
							if (genRand.Next(3) > 0)
							{
								int num17 = genRand.Next(6);
								int stack14 = genRand.Next(1, 3);
								switch (num17)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(296);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(295);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(299);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(302);
									break;
								case 4:
									Main.chest[num2].item[num3].SetDefaults(303);
									break;
								case 5:
									Main.chest[num2].item[num3].SetDefaults(305);
									break;
								}
								Main.chest[num2].item[num3].stack = stack14;
								num3++;
							}
							if (genRand.Next(3) > 1)
							{
								int num18 = genRand.Next(4);
								int stack15 = genRand.Next(1, 3);
								switch (num18)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(301);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(302);
									break;
								case 2:
									Main.chest[num2].item[num3].SetDefaults(297);
									break;
								case 3:
									Main.chest[num2].item[num3].SetDefaults(304);
									break;
								}
								Main.chest[num2].item[num3].stack = stack15;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								int num19 = genRand.Next(2);
								int stack16 = genRand.Next(15) + 15;
								switch (num19)
								{
								case 0:
									Main.chest[num2].item[num3].SetDefaults(8);
									break;
								case 1:
									Main.chest[num2].item[num3].SetDefaults(282);
									break;
								}
								Main.chest[num2].item[num3].stack = stack16;
								num3++;
							}
							if (genRand.Next(2) == 0)
							{
								Main.chest[num2].item[num3].SetDefaults(73);
								Main.chest[num2].item[num3].stack = genRand.Next(1, 3);
								num3++;
							}
							continue;
						}
						if (contain > 0)
						{
							Main.chest[num2].item[num3].SetDefaults(contain);
							Main.chest[num2].item[num3].Prefix(-1);
							num3++;
						}
						else
						{
							switch (genRand.Next(4))
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(49);
								Main.chest[num2].item[num3].Prefix(-1);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(50);
								Main.chest[num2].item[num3].Prefix(-1);
								break;
							case 2:
								Main.chest[num2].item[num3].SetDefaults(53);
								Main.chest[num2].item[num3].Prefix(-1);
								break;
							case 3:
								Main.chest[num2].item[num3].SetDefaults(54);
								Main.chest[num2].item[num3].Prefix(-1);
								break;
							}
							num3++;
						}
						if (genRand.Next(3) == 0)
						{
							Main.chest[num2].item[num3].SetDefaults(167);
							num3++;
						}
						if (genRand.Next(2) == 0)
						{
							int num20 = genRand.Next(2);
							int stack17 = genRand.Next(15) + 15;
							switch (num20)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(117);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(19);
								break;
							}
							Main.chest[num2].item[num3].stack = stack17;
							num3++;
						}
						if (genRand.Next(2) == 0)
						{
							int num21 = genRand.Next(2);
							int stack18 = genRand.Next(25) + 50;
							switch (num21)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(265);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(278);
								break;
							}
							Main.chest[num2].item[num3].stack = stack18;
							num3++;
						}
						if (genRand.Next(2) == 0)
						{
							int num22 = genRand.Next(2);
							int stack19 = genRand.Next(15) + 15;
							switch (num22)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(226);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(227);
								break;
							}
							Main.chest[num2].item[num3].stack = stack19;
							num3++;
						}
						if (genRand.Next(4) > 0)
						{
							int num23 = genRand.Next(7);
							int stack20 = genRand.Next(1, 3);
							switch (num23)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(296);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(295);
								break;
							case 2:
								Main.chest[num2].item[num3].SetDefaults(293);
								break;
							case 3:
								Main.chest[num2].item[num3].SetDefaults(288);
								break;
							case 4:
								Main.chest[num2].item[num3].SetDefaults(294);
								break;
							case 5:
								Main.chest[num2].item[num3].SetDefaults(297);
								break;
							case 6:
								Main.chest[num2].item[num3].SetDefaults(304);
								break;
							}
							Main.chest[num2].item[num3].stack = stack20;
							num3++;
						}
						if (genRand.Next(3) > 0)
						{
							int num24 = genRand.Next(5);
							int stack21 = genRand.Next(1, 3);
							switch (num24)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(305);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(301);
								break;
							case 2:
								Main.chest[num2].item[num3].SetDefaults(302);
								break;
							case 3:
								Main.chest[num2].item[num3].SetDefaults(288);
								break;
							case 4:
								Main.chest[num2].item[num3].SetDefaults(300);
								break;
							}
							Main.chest[num2].item[num3].stack = stack21;
							num3++;
						}
						if (genRand.Next(2) == 0)
						{
							int num25 = genRand.Next(2);
							int stack22 = genRand.Next(15) + 15;
							switch (num25)
							{
							case 0:
								Main.chest[num2].item[num3].SetDefaults(8);
								break;
							case 1:
								Main.chest[num2].item[num3].SetDefaults(282);
								break;
							}
							Main.chest[num2].item[num3].stack = stack22;
							num3++;
						}
						if (genRand.Next(2) == 0)
						{
							Main.chest[num2].item[num3].SetDefaults(73);
							Main.chest[num2].item[num3].stack = genRand.Next(2, 5);
							num3++;
						}
					}
					return true;
				}
				return false;
			}
			return false;
		}

		public static bool OpenDoor(int i, int j, int direction)
		{
			if (Codable.RunGlobalMethod("ModWorld", "OpenDoor", i, j, direction) && !((bool[])Codable.customMethodReturn)[0])
			{
				return ((bool[])Codable.customMethodReturn)[1];
			}
			int type = Main.tile[i, j].type;
			if (Config.tileDefs.doorToggle.ContainsKey(type))
			{
				if (Config.tileDefs.doorType[type] == 1)
				{
					return Config.OpenCustomDoor(i, j, direction, Config.tileDefs.doorToggle[type]);
				}
				return false;
			}
			int num = 0;
			if (Main.tile[i, j - 1] == null)
			{
				Main.tile[i, j - 1] = new Tile();
			}
			if (Main.tile[i, j - 2] == null)
			{
				Main.tile[i, j - 2] = new Tile();
			}
			if (Main.tile[i, j + 1] == null)
			{
				Main.tile[i, j + 1] = new Tile();
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			num = ((Main.tile[i, j - 1].frameY == 0 && Main.tile[i, j - 1].type == Main.tile[i, j].type) ? (j - 1) : ((Main.tile[i, j - 2].frameY == 0 && Main.tile[i, j - 2].type == Main.tile[i, j].type) ? (j - 2) : ((Main.tile[i, j + 1].frameY != 0 || Main.tile[i, j + 1].type != Main.tile[i, j].type) ? j : (j + 1))));
			int num2 = i;
			short num3 = 0;
			int num4;
			if (direction == -1)
			{
				num2 = i - 1;
				num3 = 36;
				num4 = i - 1;
			}
			else
			{
				num2 = i;
				num4 = i + 1;
			}
			bool flag = true;
			for (int k = num; k < num + 3; k++)
			{
				if (Main.tile[num4, k] == null)
				{
					Main.tile[num4, k] = new Tile();
				}
				if (Main.tile[num4, k].active)
				{
					if (!Main.tileCut[Main.tile[num4, k].type] && Main.tile[num4, k].type != 3 && Main.tile[num4, k].type != 24 && Main.tile[num4, k].type != 52 && Main.tile[num4, k].type != 61 && Main.tile[num4, k].type != 62 && Main.tile[num4, k].type != 69 && Main.tile[num4, k].type != 71 && Main.tile[num4, k].type != 73 && Main.tile[num4, k].type != 74 && Main.tile[num4, k].type != 110 && Main.tile[num4, k].type != 113 && Main.tile[num4, k].type != 115)
					{
						flag = false;
						break;
					}
					KillTile(num4, k);
				}
			}
			if (flag)
			{
				if (Main.netMode != 1)
				{
					for (int l = num2; l <= num2 + 1; l++)
					{
						for (int m = num; m <= num + 2; m++)
						{
							if (numNoWire < maxWire - 1)
							{
								noWireX[numNoWire] = l;
								noWireY[numNoWire] = m;
								numNoWire++;
							}
						}
					}
				}
				Main.PlaySound(8, i * 16, j * 16);
				Main.tile[num2, num].active = true;
				Main.tile[num2, num].type = 11;
				Main.tile[num2, num].frameY = 0;
				Main.tile[num2, num].frameX = num3;
				if (Main.tile[num2 + 1, num] == null)
				{
					Main.tile[num2 + 1, num] = new Tile();
				}
				Main.tile[num2 + 1, num].active = true;
				Main.tile[num2 + 1, num].type = 11;
				Main.tile[num2 + 1, num].frameY = 0;
				Main.tile[num2 + 1, num].frameX = (short)(num3 + 18);
				if (Main.tile[num2, num + 1] == null)
				{
					Main.tile[num2, num + 1] = new Tile();
				}
				Main.tile[num2, num + 1].active = true;
				Main.tile[num2, num + 1].type = 11;
				Main.tile[num2, num + 1].frameY = 18;
				Main.tile[num2, num + 1].frameX = num3;
				if (Main.tile[num2 + 1, num + 1] == null)
				{
					Main.tile[num2 + 1, num + 1] = new Tile();
				}
				Main.tile[num2 + 1, num + 1].active = true;
				Main.tile[num2 + 1, num + 1].type = 11;
				Main.tile[num2 + 1, num + 1].frameY = 18;
				Main.tile[num2 + 1, num + 1].frameX = (short)(num3 + 18);
				if (Main.tile[num2, num + 2] == null)
				{
					Main.tile[num2, num + 2] = new Tile();
				}
				Main.tile[num2, num + 2].active = true;
				Main.tile[num2, num + 2].type = 11;
				Main.tile[num2, num + 2].frameY = 36;
				Main.tile[num2, num + 2].frameX = num3;
				if (Main.tile[num2 + 1, num + 2] == null)
				{
					Main.tile[num2 + 1, num + 2] = new Tile();
				}
				Main.tile[num2 + 1, num + 2].active = true;
				Main.tile[num2 + 1, num + 2].type = 11;
				Main.tile[num2 + 1, num + 2].frameY = 36;
				Main.tile[num2 + 1, num + 2].frameX = (short)(num3 + 18);
				for (int n = num2 - 1; n <= num2 + 2; n++)
				{
					for (int num5 = num - 1; num5 <= num + 2; num5++)
					{
						TileFrame(n, num5);
					}
				}
			}
			return flag;
		}

		public static void Check1xX(int x, int j, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = j - Main.tile[x, j].frameY / 18;
			int frameX = Main.tile[x, j].frameX;
			int num2 = 3;
			if (type == 92)
			{
				num2 = 6;
			}
			bool flag = false;
			for (int i = 0; i < num2; i++)
			{
				if (Main.tile[x, num + i] == null)
				{
					Main.tile[x, num + i] = new Tile();
				}
				if (!Main.tile[x, num + i].active)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].type != type)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].frameY != i * 18)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].frameX != frameX)
				{
					flag = true;
				}
			}
			if (Main.tile[x, num + num2] == null)
			{
				Main.tile[x, num + num2] = new Tile();
			}
			if (!Main.tile[x, num + num2].active)
			{
				flag = true;
			}
			if (!Main.tileSolid[Main.tile[x, num + num2].type])
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int k = 0; k < num2; k++)
			{
				if (Main.tile[x, num + k].type == type)
				{
					KillTile(x, num + k);
				}
			}
			switch (type)
			{
			case 92:
				Item.NewItem(x * 16, j * 16, 32, 32, 341);
				break;
			case 93:
				Item.NewItem(x * 16, j * 16, 32, 32, 342);
				break;
			}
			destroyObject = false;
		}

		public static void Check2xX(int i, int j, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = i;
			if (Main.tile[i, j].frameX % 36 == 18)
			{
				num--;
			}
			if (Main.tile[num, j] == null)
			{
				Main.tile[num, j] = new Tile();
			}
			int num2 = j - Main.tile[num, j].frameY / 18;
			if (Main.tile[num, num2] == null)
			{
				Main.tile[num, num2] = new Tile();
			}
			int frameX = Main.tile[num, j].frameX;
			int num3 = 3;
			if (type == 104)
			{
				num3 = 5;
			}
			bool flag = false;
			for (int k = 0; k < num3; k++)
			{
				if (Main.tile[num, num2 + k] == null)
				{
					Main.tile[num, num2 + k] = new Tile();
				}
				if (!Main.tile[num, num2 + k].active)
				{
					flag = true;
				}
				else if (Main.tile[num, num2 + k].type != type)
				{
					flag = true;
				}
				else if (Main.tile[num, num2 + k].frameY != k * 18)
				{
					flag = true;
				}
				else if (Main.tile[num, num2 + k].frameX != frameX)
				{
					flag = true;
				}
				if (Main.tile[num + 1, num2 + k] == null)
				{
					Main.tile[num + 1, num2 + k] = new Tile();
				}
				if (!Main.tile[num + 1, num2 + k].active)
				{
					flag = true;
				}
				else if (Main.tile[num + 1, num2 + k].type != type)
				{
					flag = true;
				}
				else if (Main.tile[num + 1, num2 + k].frameY != k * 18)
				{
					flag = true;
				}
				else if (Main.tile[num + 1, num2 + k].frameX != frameX + 18)
				{
					flag = true;
				}
			}
			if (Main.tile[num, num2 + num3] == null)
			{
				Main.tile[num, num2 + num3] = new Tile();
			}
			if (!Main.tile[num, num2 + num3].active)
			{
				flag = true;
			}
			else if (!Main.tileSolid[Main.tile[num, num2 + num3].type])
			{
				flag = true;
			}
			if (Main.tile[num + 1, num2 + num3] == null)
			{
				Main.tile[num + 1, num2 + num3] = new Tile();
			}
			if (!Main.tile[num + 1, num2 + num3].active)
			{
				flag = true;
			}
			else if (!Main.tileSolid[Main.tile[num + 1, num2 + num3].type])
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int l = 0; l < num3; l++)
			{
				if (Main.tile[num, num2 + l].type == type)
				{
					KillTile(num, num2 + l);
				}
				if (Main.tile[num + 1, num2 + l].type == type)
				{
					KillTile(num + 1, num2 + l);
				}
			}
			switch (type)
			{
			case 104:
				Item.NewItem(num * 16, j * 16, 32, 32, 359);
				break;
			case 105:
			{
				int num4 = frameX / 36;
				switch (num4)
				{
				case 0:
					num4 = 360;
					break;
				case 1:
					num4 = 52;
					break;
				default:
					num4 = 438 + num4 - 2;
					break;
				}
				Item.NewItem(num * 16, j * 16, 32, 32, num4);
				break;
			}
			}
			destroyObject = false;
		}

		public static void Place1xX(int x, int y, int type, int style = 0)
		{
			int num = style * 18;
			int num2 = 3;
			if (type == 92)
			{
				num2 = 6;
			}
			bool flag = true;
			for (int i = y - num2 + 1; i < y + 1; i++)
			{
				if (Main.tile[x, i] == null)
				{
					Main.tile[x, i] = new Tile();
				}
				if (Main.tile[x, i].active)
				{
					flag = false;
				}
				if (type == 93 && Main.tile[x, i].liquid > 0)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[x, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type])
			{
				for (int j = 0; j < num2; j++)
				{
					Main.tile[x, y - num2 + 1 + j].active = true;
					Main.tile[x, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x, y - num2 + 1 + j].frameX = (short)num;
					Main.tile[x, y - num2 + 1 + j].type = (ushort)type;
				}
			}
		}

		public static void Place2xX(int x, int y, int type, int style = 0)
		{
			int num = style * 36;
			int num2 = 3;
			if (type == 104)
			{
				num2 = 5;
			}
			bool flag = true;
			for (int i = y - num2 + 1; i < y + 1; i++)
			{
				if (Main.tile[x, i] == null)
				{
					Main.tile[x, i] = new Tile();
				}
				if (Main.tile[x + 1, i] == null)
				{
					Main.tile[x + 1, i] = new Tile();
				}
				if (Main.tile[x, i].active)
				{
					flag = false;
				}
				else if (Main.tile[x + 1, i].active)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[x, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type] && Main.tile[x + 1, y + 1].active && Main.tileSolid[Main.tile[x + 1, y + 1].type])
			{
				for (int j = 0; j < num2; j++)
				{
					Main.tile[x, y - num2 + 1 + j].active = true;
					Main.tile[x, y - num2 + 1 + j].frameX = (short)num;
					Main.tile[x, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x, y - num2 + 1 + j].type = (ushort)type;
					Main.tile[x + 1, y - num2 + 1 + j].active = true;
					Main.tile[x + 1, y - num2 + 1 + j].frameX = (short)(num + 18);
					Main.tile[x + 1, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x + 1, y - num2 + 1 + j].type = (ushort)type;
				}
			}
		}

		public static void Check1x2(int x, int j, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = j;
			bool flag = true;
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num + 1] == null)
			{
				Main.tile[x, num + 1] = new Tile();
			}
			int num2 = Main.tile[x, num].frameY;
			int num3 = 0;
			while (num2 >= 40)
			{
				num2 -= 40;
				num3++;
			}
			if (num2 == 18)
			{
				num--;
			}
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num].frameY == 40 * num3 && Main.tile[x, num + 1].frameY == 40 * num3 + 18 && Main.tile[x, num].type == type && Main.tile[x, num + 1].type == type)
			{
				flag = false;
			}
			if (Main.tile[x, num + 2] == null)
			{
				Main.tile[x, num + 2] = new Tile();
			}
			if (!Main.tile[x, num + 2].active || !Main.tileSolid[Main.tile[x, num + 2].type])
			{
				flag = true;
			}
			else if (Main.tile[x, num + 2].type != 2 && Main.tile[x, num + 2].type != 109 && Main.tile[x, num + 2].type != 147 && Main.tile[x, num].type == 20)
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			if (Main.tile[x, num].type == type)
			{
				KillTile(x, num);
			}
			if (Main.tile[x, num + 1].type == type)
			{
				KillTile(x, num + 1);
			}
			switch (type)
			{
			case 15:
				if (num3 == 1)
				{
					Item.NewItem(x * 16, num * 16, 32, 32, 358);
				}
				else
				{
					Item.NewItem(x * 16, num * 16, 32, 32, 34);
				}
				break;
			case 134:
				Item.NewItem(x * 16, num * 16, 32, 32, 525);
				break;
			}
			destroyObject = false;
		}

		public static void CheckOnTable1x1(int x, int y, int type)
		{
			if (Main.tile[x, y + 1] == null || (Main.tile[x, y + 1].active && Main.tileTable[Main.tile[x, y + 1].type]))
			{
				return;
			}
			if (type == 78)
			{
				if (!Main.tile[x, y + 1].active || !Main.tileSolid[Main.tile[x, y + 1].type])
				{
					KillTile(x, y);
				}
			}
			else
			{
				KillTile(x, y);
			}
		}

		public static void CheckSign(int x, int y, int type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = x - 2;
			int num2 = x + 3;
			int num3 = y - 2;
			int num4 = y + 3;
			if (num < 0 || num2 > Main.maxTilesX || num3 < 0 || num4 > Main.maxTilesY)
			{
				return;
			}
			bool flag = false;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
				}
			}
			int num5 = Main.tile[x, y].frameX / 18;
			int num6 = Main.tile[x, y].frameY / 18;
			while (num5 > 1)
			{
				num5 -= 2;
			}
			int num7 = x - num5;
			int num8 = y - num6;
			int num9 = Main.tile[num7, num8].frameX / 36;
			num = num7;
			num2 = num7 + 2;
			num3 = num8;
			num4 = num8 + 2;
			num5 = 0;
			for (int k = num; k < num2; k++)
			{
				num6 = 0;
				for (int l = num3; l < num4; l++)
				{
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type)
					{
						flag = true;
						break;
					}
					if (Main.tile[k, l].frameX / 18 != num5 + num9 * 2 || Main.tile[k, l].frameY / 18 != num6)
					{
						flag = true;
						break;
					}
					num6++;
				}
				num5++;
			}
			if (!flag)
			{
				if (type == 85)
				{
					if (Main.tile[num7, num8 + 2].active && Main.tileSolid[Main.tile[num7, num8 + 2].type] && Main.tile[num7 + 1, num8 + 2].active && Main.tileSolid[Main.tile[num7 + 1, num8 + 2].type])
					{
						num9 = 0;
					}
					else
					{
						flag = true;
					}
				}
				else if (Main.tile[num7, num8 + 2].active && Main.tileSolid[Main.tile[num7, num8 + 2].type] && Main.tile[num7 + 1, num8 + 2].active && Main.tileSolid[Main.tile[num7 + 1, num8 + 2].type])
				{
					num9 = 0;
				}
				else if (Main.tile[num7, num8 - 1].active && Main.tileSolid[Main.tile[num7, num8 - 1].type] && !Main.tileSolidTop[Main.tile[num7, num8 - 1].type] && Main.tile[num7 + 1, num8 - 1].active && Main.tileSolid[Main.tile[num7 + 1, num8 - 1].type] && !Main.tileSolidTop[Main.tile[num7 + 1, num8 - 1].type])
				{
					num9 = 1;
				}
				else if (Main.tile[num7 - 1, num8].active && Main.tileSolid[Main.tile[num7 - 1, num8].type] && !Main.tileSolidTop[Main.tile[num7 - 1, num8].type] && Main.tile[num7 - 1, num8 + 1].active && Main.tileSolid[Main.tile[num7 - 1, num8 + 1].type] && !Main.tileSolidTop[Main.tile[num7 - 1, num8 + 1].type])
				{
					num9 = 2;
				}
				else if (Main.tile[num7 + 2, num8].active && Main.tileSolid[Main.tile[num7 + 2, num8].type] && !Main.tileSolidTop[Main.tile[num7 + 2, num8].type] && Main.tile[num7 + 2, num8 + 1].active && Main.tileSolid[Main.tile[num7 + 2, num8 + 1].type] && !Main.tileSolidTop[Main.tile[num7 + 2, num8 + 1].type])
				{
					num9 = 3;
				}
				else
				{
					flag = true;
				}
			}
			if (flag)
			{
				destroyObject = true;
				for (int m = num; m < num2; m++)
				{
					for (int n = num3; n < num4; n++)
					{
						if (Main.tile[m, n].type == type)
						{
							KillTile(m, n);
						}
					}
				}
				Sign.KillSign(num7, num8);
				if (type == 85)
				{
					Item.NewItem(x * 16, y * 16, 32, 32, 321);
				}
				else
				{
					Item.NewItem(x * 16, y * 16, 32, 32, 171);
				}
				destroyObject = false;
				return;
			}
			int num10 = 36 * num9;
			for (int num11 = 0; num11 < 2; num11++)
			{
				for (int num12 = 0; num12 < 2; num12++)
				{
					Main.tile[num7 + num11, num8 + num12].active = true;
					Main.tile[num7 + num11, num8 + num12].type = (ushort)type;
					Main.tile[num7 + num11, num8 + num12].frameX = (short)(num10 + 18 * num11);
					Main.tile[num7 + num11, num8 + num12].frameY = (short)(18 * num12);
				}
			}
		}

		public static bool PlaceSign(int x, int y, int type)
		{
			int num = x - 2;
			int num2 = x + 3;
			int num3 = y - 2;
			int num4 = y + 3;
			if (num < 0)
			{
				return false;
			}
			if (num2 > Main.maxTilesX)
			{
				return false;
			}
			if (num3 < 0)
			{
				return false;
			}
			if (num4 > Main.maxTilesY)
			{
				return false;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
				}
			}
			int num5 = x;
			int num6 = y;
			int num7 = 0;
			switch (type)
			{
			case 55:
				if (Main.tile[x, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type] && Main.tile[x + 1, y + 1].active && Main.tileSolid[Main.tile[x + 1, y + 1].type])
				{
					num6--;
					num7 = 0;
					break;
				}
				if (Main.tile[x, y - 1].active && Main.tileSolid[Main.tile[x, y - 1].type] && !Main.tileSolidTop[Main.tile[x, y - 1].type] && Main.tile[x + 1, y - 1].active && Main.tileSolid[Main.tile[x + 1, y - 1].type] && !Main.tileSolidTop[Main.tile[x + 1, y - 1].type])
				{
					num7 = 1;
					break;
				}
				if (Main.tile[x - 1, y].active && Main.tileSolid[Main.tile[x - 1, y].type] && !Main.tileSolidTop[Main.tile[x - 1, y].type] && !Main.tileNoAttach[Main.tile[x - 1, y].type] && Main.tile[x - 1, y + 1].active && Main.tileSolid[Main.tile[x - 1, y + 1].type] && !Main.tileSolidTop[Main.tile[x - 1, y + 1].type] && !Main.tileNoAttach[Main.tile[x - 1, y + 1].type])
				{
					num7 = 2;
					break;
				}
				if (!Main.tile[x + 1, y].active || !Main.tileSolid[Main.tile[x + 1, y].type] || Main.tileSolidTop[Main.tile[x + 1, y].type] || Main.tileNoAttach[Main.tile[x + 1, y].type] || !Main.tile[x + 1, y + 1].active || !Main.tileSolid[Main.tile[x + 1, y + 1].type] || Main.tileSolidTop[Main.tile[x + 1, y + 1].type] || Main.tileNoAttach[Main.tile[x + 1, y + 1].type])
				{
					return false;
				}
				num5--;
				num7 = 3;
				break;
			case 85:
				if (!Main.tile[x, y + 1].active || !Main.tileSolid[Main.tile[x, y + 1].type] || !Main.tile[x + 1, y + 1].active || !Main.tileSolid[Main.tile[x + 1, y + 1].type])
				{
					return false;
				}
				num6--;
				num7 = 0;
				break;
			}
			if (Main.tile[num5, num6].active || Main.tile[num5 + 1, num6].active || Main.tile[num5, num6 + 1].active || Main.tile[num5 + 1, num6 + 1].active)
			{
				return false;
			}
			int num8 = 36 * num7;
			for (int k = 0; k < 2; k++)
			{
				for (int l = 0; l < 2; l++)
				{
					Main.tile[num5 + k, num6 + l].active = true;
					Main.tile[num5 + k, num6 + l].type = (ushort)type;
					Main.tile[num5 + k, num6 + l].frameX = (short)(num8 + 18 * k);
					Main.tile[num5 + k, num6 + l].frameY = (short)(18 * l);
				}
			}
			return true;
		}

		public static void Place1x1(int x, int y, int type, int style = 0)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (SolidTile(x, y + 1) && !Main.tile[x, y].active)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].type = (ushort)type;
				if (type == 144)
				{
					Main.tile[x, y].frameX = (short)(style * 18);
					Main.tile[x, y].frameY = 0;
				}
				else
				{
					Main.tile[x, y].frameY = (short)(style * 18);
				}
			}
		}

		public static void Check1x1(int x, int y, int type)
		{
			if (Main.tile[x, y + 1] != null && (!Main.tile[x, y + 1].active || !Main.tileSolid[Main.tile[x, y + 1].type]))
			{
				KillTile(x, y);
			}
		}

		public static void PlaceOnTable1x1(int x, int y, int type, int style = 0)
		{
			bool flag = false;
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (!Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileTable[Main.tile[x, y + 1].type])
			{
				flag = true;
			}
			else if (type == 78 && !Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameX = (short)(style * 18);
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].type = (ushort)type;
				if (type == 50)
				{
					Main.tile[x, y].frameX = (short)(18 * genRand.Next(5));
				}
			}
		}

		public static bool PlaceAlch(int x, int y, int style)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (!Main.tile[x, y].active && Main.tile[x, y + 1].active)
			{
				bool flag = false;
				switch (style)
				{
				case 0:
					if (Main.tile[x, y + 1].type != 2 && Main.tile[x, y + 1].type != 78 && Main.tile[x, y + 1].type != 109)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0)
					{
						flag = true;
					}
					break;
				case 1:
					if (Main.tile[x, y + 1].type != 60 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0)
					{
						flag = true;
					}
					break;
				case 2:
					if (Main.tile[x, y + 1].type != 0 && Main.tile[x, y + 1].type != 59 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0)
					{
						flag = true;
					}
					break;
				case 3:
					if (Main.tile[x, y + 1].type != 23 && Main.tile[x, y + 1].type != 25 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0)
					{
						flag = true;
					}
					break;
				case 4:
					if (Main.tile[x, y + 1].type != 53 && Main.tile[x, y + 1].type != 78 && Main.tile[x, y + 1].type != 116)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				case 5:
					if (Main.tile[x, y + 1].type != 57 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && !Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				}
				if (!flag)
				{
					Main.tile[x, y].active = true;
					Main.tile[x, y].type = 82;
					Main.tile[x, y].frameX = (short)(18 * style);
					Main.tile[x, y].frameY = 0;
					return true;
				}
			}
			return false;
		}

		public static void GrowAlch(int x, int y)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "GrowAlch", x, y) && !(bool)Codable.customMethodReturn) || !Main.tile[x, y].active)
			{
				return;
			}
			if (Main.tile[x, y].type == 82 && genRand.Next(50) == 0)
			{
				Main.tile[x, y].type = 83;
				if (Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
				SquareTileFrame(x, y);
			}
			else if (Main.tile[x, y].frameX == 36)
			{
				if (Main.tile[x, y].type == 83)
				{
					Main.tile[x, y].type = 84;
				}
				else
				{
					Main.tile[x, y].type = 83;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, x, y, 1);
				}
			}
		}

		public static void PlantAlch()
		{
			if (TMod.RunMethod(TMod.WorldHooks.PlantAlch) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(20, Main.maxTilesX - 20);
			int num2 = 0;
			for (num2 = ((genRand.Next(40) == 0) ? genRand.Next((int)(Main.rockLayer + (double)Main.maxTilesY) / 2, Main.maxTilesY - 20) : ((genRand.Next(10) != 0) ? genRand.Next((int)Main.worldSurface, Main.maxTilesY - 20) : genRand.Next(0, Main.maxTilesY - 20))); num2 < Main.maxTilesY - 20 && !Main.tile[num, num2].active; num2++)
			{
			}
			if (Main.tile[num, num2].active && !Main.tile[num, num2 - 1].active && Main.tile[num, num2 - 1].liquid == 0)
			{
				if (Main.tile[num, num2].type == 2 || Main.tile[num, num2].type == 109)
				{
					PlaceAlch(num, num2 - 1, 0);
				}
				if (Main.tile[num, num2].type == 60)
				{
					PlaceAlch(num, num2 - 1, 1);
				}
				if (Main.tile[num, num2].type == 0 || Main.tile[num, num2].type == 59)
				{
					PlaceAlch(num, num2 - 1, 2);
				}
				if (Main.tile[num, num2].type == 23 || Main.tile[num, num2].type == 25)
				{
					PlaceAlch(num, num2 - 1, 3);
				}
				if (Main.tile[num, num2].type == 53 || Main.tile[num, num2].type == 116)
				{
					PlaceAlch(num, num2 - 1, 4);
				}
				if (Main.tile[num, num2].type == 57)
				{
					PlaceAlch(num, num2 - 1, 5);
				}
				if (Main.tile[num, num2 - 1].active && Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, num, num2 - 1, 1);
				}
			}
		}

		public static void CheckAlch(int x, int y)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			bool flag = false;
			if (!Main.tile[x, y + 1].active)
			{
				flag = true;
			}
			int num = Main.tile[x, y].frameX / 18;
			Main.tile[x, y].frameY = 0;
			if (!flag)
			{
				switch (num)
				{
				case 0:
					if (Main.tile[x, y + 1].type != 109 && Main.tile[x, y + 1].type != 2 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				case 1:
					if (Main.tile[x, y + 1].type != 60 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				case 2:
					if (Main.tile[x, y + 1].type != 0 && Main.tile[x, y + 1].type != 59 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				case 3:
					if (Main.tile[x, y + 1].type != 23 && Main.tile[x, y + 1].type != 25 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					break;
				case 4:
					if (Main.tile[x, y + 1].type != 53 && Main.tile[x, y + 1].type != 78 && Main.tile[x, y + 1].type != 116)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
					if (Main.tile[x, y].type == 82 || Main.tile[x, y].lava || Main.netMode == 1)
					{
						break;
					}
					if (Main.tile[x, y].liquid > 16)
					{
						if (Main.tile[x, y].type == 83)
						{
							Main.tile[x, y].type = 84;
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, x, y, 1);
							}
						}
					}
					else if (Main.tile[x, y].type == 84)
					{
						Main.tile[x, y].type = 83;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x, y, 1);
						}
					}
					break;
				case 5:
					if (Main.tile[x, y + 1].type != 57 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					else if (Main.tile[x, y].liquid > 0 && !Main.tile[x, y].lava)
					{
						flag = true;
					}
					if (Main.tile[x, y].type == 82 || !Main.tile[x, y].lava || Main.tile[x, y].type == 82 || !Main.tile[x, y].lava || Main.netMode == 1)
					{
						break;
					}
					if (Main.tile[x, y].liquid > 16)
					{
						if (Main.tile[x, y].type == 83)
						{
							Main.tile[x, y].type = 84;
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, x, y, 1);
							}
						}
					}
					else if (Main.tile[x, y].type == 84)
					{
						Main.tile[x, y].type = 83;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x, y, 1);
						}
					}
					break;
				}
			}
			if (flag)
			{
				KillTile(x, y);
			}
		}

		public static void CheckBanner(int x, int j, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = j - Main.tile[x, j].frameY / 18;
			int frameX = Main.tile[x, j].frameX;
			bool flag = false;
			for (int i = 0; i < 3; i++)
			{
				if (Main.tile[x, num + i] == null)
				{
					Main.tile[x, num + i] = new Tile();
				}
				if (!Main.tile[x, num + i].active)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].type != type)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].frameY != i * 18)
				{
					flag = true;
				}
				else if (Main.tile[x, num + i].frameX != frameX)
				{
					flag = true;
				}
			}
			if (Main.tile[x, num - 1] == null)
			{
				Main.tile[x, num - 1] = new Tile();
			}
			if (!Main.tile[x, num - 1].active)
			{
				flag = true;
			}
			else if (!Main.tileSolid[Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			else if (Main.tileSolidTop[Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int k = 0; k < 3; k++)
			{
				if (Main.tile[x, num + k].type == type)
				{
					KillTile(x, num + k);
				}
			}
			if (type == 91)
			{
				Item.NewItem(x * 16, (num + 1) * 16, 32, 32, 337 + frameX / 18);
			}
			destroyObject = false;
		}

		public static void PlaceBanner(int x, int y, int type, int style = 0)
		{
			int num = style * 18;
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y + 2] == null)
			{
				Main.tile[x, y + 2] = new Tile();
			}
			if (Main.tile[x, y - 1].active && Main.tileSolid[Main.tile[x, y - 1].type] && !Main.tileSolidTop[Main.tile[x, y - 1].type] && !Main.tile[x, y].active && !Main.tile[x, y + 1].active && !Main.tile[x, y + 2].active)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameX = (short)num;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x, y + 1].active = true;
				Main.tile[x, y + 1].frameX = (short)num;
				Main.tile[x, y + 1].frameY = 18;
				Main.tile[x, y + 1].type = (ushort)type;
				Main.tile[x, y + 2].active = true;
				Main.tile[x, y + 2].frameX = (short)num;
				Main.tile[x, y + 2].frameY = 36;
				Main.tile[x, y + 2].type = (ushort)type;
			}
		}

		public static void PlaceMan(int i, int j, int dir)
		{
			for (int k = i; k <= i + 1; k++)
			{
				for (int l = j - 2; l <= j; l++)
				{
					if (Main.tile[k, l].active)
					{
						return;
					}
				}
			}
			if (SolidTile(i, j + 1) && SolidTile(i + 1, j + 1))
			{
				byte b = 0;
				if (dir == 1)
				{
					b = 36;
				}
				Main.tile[i, j - 2].active = true;
				Main.tile[i, j - 2].frameY = 0;
				Main.tile[i, j - 2].frameX = b;
				Main.tile[i, j - 2].type = 128;
				Main.tile[i, j - 1].active = true;
				Main.tile[i, j - 1].frameY = 18;
				Main.tile[i, j - 1].frameX = b;
				Main.tile[i, j - 1].type = 128;
				Main.tile[i, j].active = true;
				Main.tile[i, j].frameY = 36;
				Main.tile[i, j].frameX = b;
				Main.tile[i, j].type = 128;
				Main.tile[i + 1, j - 2].active = true;
				Main.tile[i + 1, j - 2].frameY = 0;
				Main.tile[i + 1, j - 2].frameX = (short)(18 + b);
				Main.tile[i + 1, j - 2].type = 128;
				Main.tile[i + 1, j - 1].active = true;
				Main.tile[i + 1, j - 1].frameY = 18;
				Main.tile[i + 1, j - 1].frameX = (short)(18 + b);
				Main.tile[i + 1, j - 1].type = 128;
				Main.tile[i + 1, j].active = true;
				Main.tile[i + 1, j].frameY = 36;
				Main.tile[i + 1, j].frameX = (short)(18 + b);
				Main.tile[i + 1, j].type = 128;
			}
		}

		public static void CheckMan(int i, int j)
		{
			if (destroyObject)
			{
				return;
			}
			int num = j - Main.tile[i, j].frameY / 18;
			int num2 = i - Main.tile[i, j].frameX % 100 % 36 / 18;
			bool flag = false;
			for (int k = 0; k <= 1; k++)
			{
				for (int l = 0; l <= 2; l++)
				{
					int num3 = num2 + k;
					int num4 = num + l;
					if (!Main.tile[num3, num4].active || Main.tile[num3, num4].type != 128 || Main.tile[num3, num4].frameY != l * 18 || Main.tile[num3, num4].frameX % 100 % 36 != k * 18)
					{
						flag = true;
					}
				}
			}
			if (!SolidTile(num2, num + 3) || !SolidTile(num2 + 1, num + 3))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			Item.NewItem(i * 16, j * 16, 32, 32, 498);
			for (int m = 0; m <= 1; m++)
			{
				for (int n = 0; n <= 2; n++)
				{
					int num5 = num2 + m;
					int num6 = num + n;
					if (Main.tile[num5, num6].active && Main.tile[num5, num6].type == 128)
					{
						KillTile(num5, num6);
					}
				}
			}
			destroyObject = false;
		}

		public static void Place1x2(int x, int y, int type, int style)
		{
			short frameX = 0;
			if (type == 20)
			{
				frameX = (short)(genRand.Next(3) * 18);
			}
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type] && !Main.tile[x, y - 1].active)
			{
				short num = (short)(style * 40);
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = num;
				Main.tile[x, y - 1].frameX = frameX;
				Main.tile[x, y - 1].type = (ushort)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = (short)(num + 18);
				Main.tile[x, y].frameX = frameX;
				Main.tile[x, y].type = (ushort)type;
			}
		}

		public static void Place1x2Top(int x, int y, int type)
		{
			short frameX = 0;
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y - 1].active && Main.tileSolid[Main.tile[x, y - 1].type] && !Main.tileSolidTop[Main.tile[x, y - 1].type] && !Main.tile[x, y + 1].active)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].frameX = frameX;
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x, y + 1].active = true;
				Main.tile[x, y + 1].frameY = 18;
				Main.tile[x, y + 1].frameX = frameX;
				Main.tile[x, y + 1].type = (ushort)type;
			}
		}

		public static void Check1x2Top(int x, int j, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = j;
			bool flag = true;
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num + 1] == null)
			{
				Main.tile[x, num + 1] = new Tile();
			}
			if (Main.tile[x, num].frameY == 18)
			{
				num--;
			}
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num].frameY == 0 && Main.tile[x, num + 1].frameY == 18 && Main.tile[x, num].type == type && Main.tile[x, num + 1].type == type)
			{
				flag = false;
			}
			if (Main.tile[x, num - 1] == null)
			{
				Main.tile[x, num - 1] = new Tile();
			}
			if (!Main.tile[x, num - 1].active || !Main.tileSolid[Main.tile[x, num - 1].type] || Main.tileSolidTop[Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				destroyObject = true;
				if (Main.tile[x, num].type == type)
				{
					KillTile(x, num);
				}
				if (Main.tile[x, num + 1].type == type)
				{
					KillTile(x, num + 1);
				}
				if (type == 42)
				{
					Item.NewItem(x * 16, num * 16, 32, 32, 136);
				}
				destroyObject = false;
			}
		}

		public static void Check2x1(int i, int y, byte type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = i;
			bool flag = true;
			if (Main.tile[num, y] == null)
			{
				Main.tile[num, y] = new Tile();
			}
			if (Main.tile[num + 1, y] == null)
			{
				Main.tile[num + 1, y] = new Tile();
			}
			if (Main.tile[num, y + 1] == null)
			{
				Main.tile[num, y + 1] = new Tile();
			}
			if (Main.tile[num + 1, y + 1] == null)
			{
				Main.tile[num + 1, y + 1] = new Tile();
			}
			if (Main.tile[num, y].frameX == 18)
			{
				num--;
			}
			if (Main.tile[num, y].frameX == 0 && Main.tile[num + 1, y].frameX == 18 && Main.tile[num, y].type == type && Main.tile[num + 1, y].type == type)
			{
				flag = false;
			}
			if (type == 29 || type == 103)
			{
				if (!Main.tile[num, y + 1].active || !Main.tileTable[Main.tile[num, y + 1].type])
				{
					flag = true;
				}
				else if (!Main.tile[num + 1, y + 1].active || !Main.tileTable[Main.tile[num + 1, y + 1].type])
				{
					flag = true;
				}
			}
			else if (!Main.tile[num, y + 1].active || !Main.tileSolid[Main.tile[num, y + 1].type])
			{
				flag = true;
			}
			else if (!Main.tile[num + 1, y + 1].active || !Main.tileSolid[Main.tile[num + 1, y + 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				destroyObject = true;
				if (Main.tile[num, y].type == type)
				{
					KillTile(num, y);
				}
				if (Main.tile[num + 1, y].type == type)
				{
					KillTile(num + 1, y);
				}
				switch (type)
				{
				case 16:
					Item.NewItem(num * 16, y * 16, 32, 32, 35);
					break;
				case 18:
					Item.NewItem(num * 16, y * 16, 32, 32, 36);
					break;
				case 29:
					Item.NewItem(num * 16, y * 16, 32, 32, 87);
					Main.PlaySound(13, i * 16, y * 16);
					break;
				case 103:
					Item.NewItem(num * 16, y * 16, 32, 32, 356);
					Main.PlaySound(13, i * 16, y * 16);
					break;
				case 134:
					Item.NewItem(num * 16, y * 16, 32, 32, 525);
					break;
				}
				destroyObject = false;
				SquareTileFrame(num, y);
				SquareTileFrame(num + 1, y);
			}
		}

		public static void Place2x1(int x, int y, int type)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x + 1, y] == null)
			{
				Main.tile[x + 1, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x + 1, y + 1] == null)
			{
				Main.tile[x + 1, y + 1] = new Tile();
			}
			bool flag = false;
			if (type != 29 && type != 103 && Main.tile[x, y + 1].active && Main.tile[x + 1, y + 1].active && Main.tileSolid[Main.tile[x, y + 1].type] && Main.tileSolid[Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active && !Main.tile[x + 1, y].active)
			{
				flag = true;
			}
			else if ((type == 29 || type == 103) && Main.tile[x, y + 1].active && Main.tile[x + 1, y + 1].active && Main.tileTable[Main.tile[x, y + 1].type] && Main.tileTable[Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active && !Main.tile[x + 1, y].active)
			{
				flag = true;
			}
			if (flag)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].frameX = 0;
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 0;
				Main.tile[x + 1, y].frameX = 18;
				Main.tile[x + 1, y].type = (ushort)type;
			}
		}

		public static void Check4x2(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + Main.tile[i, j].frameX / -18;
			if ((type == 79 || type == 90) && Main.tile[i, j].frameX >= 72)
			{
				num += 4;
			}
			int num2 = j + Main.tile[i, j].frameY / -18;
			for (int k = num; k < num + 4; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					int num3 = (k - num) * 18;
					if ((type == 79 || type == 90) && Main.tile[i, j].frameX >= 72)
					{
						num3 = (k - num + 4) * 18;
					}
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != num3 || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num; m < num + 4; m++)
			{
				for (int n = num2; n < num2 + 3; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			switch (type)
			{
			case 79:
				Item.NewItem(i * 16, j * 16, 32, 32, 224);
				break;
			case 90:
				Item.NewItem(i * 16, j * 16, 32, 32, 336);
				break;
			}
			destroyObject = false;
			for (int num4 = num - 1; num4 < num + 4; num4++)
			{
				for (int num5 = num2 - 1; num5 < num2 + 4; num5++)
				{
					TileFrame(num4, num5);
				}
			}
		}

		public static void Check2x2(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = 0;
			int num2 = Main.tile[i, j].frameX / -18;
			int num3 = Main.tile[i, j].frameY / -18;
			if (num2 < -1)
			{
				num2 += 2;
				num = 36;
			}
			num2 += i;
			num3 += j;
			for (int k = num2; k < num2 + 2; k++)
			{
				for (int l = num3; l < num3 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != (k - num2) * 18 + num || Main.tile[k, l].frameY != (l - num3) * 18)
					{
						flag = true;
					}
				}
				switch (type)
				{
				case 95:
				case 126:
					if (Main.tile[k, num3 - 1] == null)
					{
						Main.tile[k, num3 - 1] = new Tile();
					}
					if (!Main.tile[k, num3 - 1].active || !Main.tileSolid[Main.tile[k, num3 - 1].type] || Main.tileSolidTop[Main.tile[k, num3 - 1].type])
					{
						flag = true;
					}
					break;
				default:
					if (Main.tile[k, num3 + 2] == null)
					{
						Main.tile[k, num3 + 2] = new Tile();
					}
					if (!Main.tile[k, num3 + 2].active || (!Main.tileSolid[Main.tile[k, num3 + 2].type] && !Main.tileTable[Main.tile[k, num3 + 2].type]))
					{
						flag = true;
					}
					break;
				case 138:
					break;
				}
			}
			if (type == 138 && !SolidTile(num2, num3 + 2) && !SolidTile(num2 + 1, num3 + 2))
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num2; m < num2 + 2; m++)
			{
				for (int n = num3; n < num3 + 2; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			switch (type)
			{
			case 85:
				Item.NewItem(i * 16, j * 16, 32, 32, 321);
				break;
			case 94:
				Item.NewItem(i * 16, j * 16, 32, 32, 352);
				break;
			case 95:
				Item.NewItem(i * 16, j * 16, 32, 32, 344);
				break;
			case 96:
				Item.NewItem(i * 16, j * 16, 32, 32, 345);
				break;
			case 97:
				Item.NewItem(i * 16, j * 16, 32, 32, 346);
				break;
			case 98:
				Item.NewItem(i * 16, j * 16, 32, 32, 347);
				break;
			case 99:
				Item.NewItem(i * 16, j * 16, 32, 32, 348);
				break;
			case 100:
				Item.NewItem(i * 16, j * 16, 32, 32, 349);
				break;
			case 125:
				Item.NewItem(i * 16, j * 16, 32, 32, 487);
				break;
			case 126:
				Item.NewItem(i * 16, j * 16, 32, 32, 488);
				break;
			case 132:
				Item.NewItem(i * 16, j * 16, 32, 32, 513);
				break;
			case 142:
				Item.NewItem(i * 16, j * 16, 32, 32, 581);
				break;
			case 143:
				Item.NewItem(i * 16, j * 16, 32, 32, 582);
				break;
			case 138:
				if (!gen && Main.netMode != 1)
				{
					Projectile.NewProjectile((float)(num2 * 16) + 15.5f, num3 * 16 + 16, 0f, 0f, 99, 70, 10f, Main.myPlayer);
				}
				break;
			}
			destroyObject = false;
			for (int num4 = num2 - 1; num4 < num2 + 3; num4++)
			{
				for (int num5 = num3 - 1; num5 < num3 + 3; num5++)
				{
					TileFrame(num4, num5);
				}
			}
		}

		public static void OreRunner(int i, int j, double strength, int steps, int type)
		{
			if (Codable.RunGlobalMethod("ModWorld", "OreRunner", i, j, strength, steps, type) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			double num = strength;
			int num2 = steps;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-10, 11) * 0.1f;
			while (num > 0.0 && (float)num2 > 0f)
			{
				if (vector.Y < 0f && (float)num2 > 0f && type == 59)
				{
					num2 = 0;
				}
				num = strength * (double)num2 / (double)steps;
				num2--;
				int num3 = (int)((double)vector.X - num * 0.5);
				int num4 = (int)((double)vector.X + num * 0.5);
				int num5 = (int)((double)vector.Y - num * 0.5);
				int num6 = (int)((double)vector.Y + num * 0.5);
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
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if ((double)(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y)) < strength * 0.5 * (double)(1f + (float)genRand.Next(-10, 11) * 0.015f) && Main.tile[k, l].active && (Main.tile[k, l].type == 0 || Main.tile[k, l].type == 1 || Main.tile[k, l].type == 23 || Main.tile[k, l].type == 25 || Main.tile[k, l].type == 40 || Main.tile[k, l].type == 53 || Main.tile[k, l].type == 57 || Main.tile[k, l].type == 59 || Main.tile[k, l].type == 60 || Main.tile[k, l].type == 70 || Main.tile[k, l].type == 109 || Main.tile[k, l].type == 112 || Main.tile[k, l].type == 116 || Main.tile[k, l].type == 117))
						{
							Main.tile[k, l].type = (ushort)type;
							SquareTileFrame(k, l);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, k, l, 1);
							}
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 1f)
				{
					vector2.X = 1f;
				}
				else if (vector2.X < -1f)
				{
					vector2.X = -1f;
				}
			}
		}

		public static void SmashAltar(int i, int j)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "SmashAltar", i, j) && !(bool)Codable.customMethodReturn) || Main.netMode == 1 || !Main.hardMode || noTileActions || gen)
			{
				return;
			}
			int num = altarCount % 3;
			int num2 = altarCount / 3 + 1;
			float num3 = (float)(Main.maxTilesX / 4200 * 310 - 85 * num) * 0.85f / (float)num2;
			int num4 = 1 - num;
			switch (num)
			{
			case 0:
				if (Main.netMode == 0)
				{
					Main.NewText(Lang.misc[12], 50, byte.MaxValue, 130);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Lang.misc[12], 255, 50f, 255f, 130f);
				}
				num = 107;
				num3 *= 1.05f;
				break;
			case 1:
				if (Main.netMode == 0)
				{
					Main.NewText(Lang.misc[13], 50, byte.MaxValue, 130);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Lang.misc[13], 255, 50f, 255f, 130f);
				}
				num = 108;
				break;
			default:
				if (Main.netMode == 0)
				{
					Main.NewText(Lang.misc[14], 50, byte.MaxValue, 130);
				}
				else if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, Lang.misc[14], 255, 50f, 255f, 130f);
				}
				num = 111;
				break;
			}
			for (int k = 0; (float)k < num3; k++)
			{
				int i2 = genRand.Next(100, Main.maxTilesX - 100);
				double num5 = Main.worldSurface;
				if (num == 108)
				{
					num5 = Main.rockLayer;
				}
				if (num == 111)
				{
					num5 = (Main.rockLayer + Main.rockLayer + (double)Main.maxTilesY) / 3.0;
				}
				int j2 = genRand.Next((int)num5, (int)Main.hellLayer + 50);
				OreRunner(i2, j2, genRand.Next(5, 9 + num4), genRand.Next(5, 9 + num4), num);
			}
			int num6 = genRand.Next(3);
			while (num6 != 2)
			{
				int num7 = genRand.Next(100, Main.maxTilesX - 100);
				int num8 = genRand.Next((int)Main.rockLayer + 50, (int)Main.hellLayer - 100);
				if (Main.tile[num7, num8].active && Main.tile[num7, num8].type == 1)
				{
					if (num6 == 0)
					{
						Main.tile[num7, num8].type = 25;
					}
					else
					{
						Main.tile[num7, num8].type = 117;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, num7, num8, 1);
					}
					break;
				}
			}
			if (Main.netMode != 1)
			{
				int num9 = Main.rand.Next(2) + 1;
				for (int l = 0; l < num9; l++)
				{
					NPC.SpawnOnPlayer(Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16), 82);
				}
			}
			altarCount++;
		}

		public static void Check3x2(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + Main.tile[i, j].frameX / -18;
			int num2 = j + Main.tile[i, j].frameY / -18;
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != (k - num) * 18 || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num; m < num + 3; m++)
			{
				for (int n = num2; n < num2 + 3; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			switch (type)
			{
			case 14:
				Item.NewItem(i * 16, j * 16, 32, 32, 32);
				break;
			case 114:
				Item.NewItem(i * 16, j * 16, 32, 32, 398);
				break;
			case 26:
				if (!noTileActions)
				{
					SmashAltar(i, j);
				}
				break;
			case 17:
				Item.NewItem(i * 16, j * 16, 32, 32, 33);
				break;
			case 77:
				Item.NewItem(i * 16, j * 16, 32, 32, 221);
				break;
			case 86:
				Item.NewItem(i * 16, j * 16, 32, 32, 332);
				break;
			case 87:
				Item.NewItem(i * 16, j * 16, 32, 32, 333);
				break;
			case 88:
				Item.NewItem(i * 16, j * 16, 32, 32, 334);
				break;
			case 89:
				Item.NewItem(i * 16, j * 16, 32, 32, 335);
				break;
			case 133:
				Item.NewItem(i * 16, j * 16, 32, 32, 524);
				break;
			}
			destroyObject = false;
			for (int num3 = num - 1; num3 < num + 4; num3++)
			{
				for (int num4 = num2 - 1; num4 < num2 + 4; num4++)
				{
					TileFrame(num3, num4);
				}
			}
		}

		public static void Check3x4(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + Main.tile[i, j].frameX / 18 * -1;
			int num2 = j + Main.tile[i, j].frameY / 18 * -1;
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 4; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != (k - num) * 18 || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 4] == null)
				{
					Main.tile[k, num2 + 4] = new Tile();
				}
				if (!Main.tile[k, num2 + 4].active || !Main.tileSolid[Main.tile[k, num2 + 4].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num; m < num + 3; m++)
			{
				for (int n = num2; n < num2 + 4; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			switch (type)
			{
			case 101:
				Item.NewItem(i * 16, j * 16, 32, 32, 354);
				break;
			case 102:
				Item.NewItem(i * 16, j * 16, 32, 32, 355);
				break;
			}
			destroyObject = false;
			for (int num3 = num - 1; num3 < num + 4; num3++)
			{
				for (int num4 = num2 - 1; num4 < num2 + 4; num4++)
				{
					TileFrame(num3, num4);
				}
			}
		}

		public static void Place4x2(int x, int y, int type, int direction = -1)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 3; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			short num = 0;
			if (direction == 1)
			{
				num = 72;
			}
			if (flag)
			{
				Main.tile[x - 1, y - 1].active = true;
				Main.tile[x - 1, y - 1].frameY = 0;
				Main.tile[x - 1, y - 1].frameX = num;
				Main.tile[x - 1, y - 1].type = (ushort)type;
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
				Main.tile[x, y - 1].frameX = (short)(18 + num);
				Main.tile[x, y - 1].type = (ushort)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
				Main.tile[x + 1, y - 1].frameX = (short)(36 + num);
				Main.tile[x + 1, y - 1].type = (ushort)type;
				Main.tile[x + 2, y - 1].active = true;
				Main.tile[x + 2, y - 1].frameY = 0;
				Main.tile[x + 2, y - 1].frameX = (short)(54 + num);
				Main.tile[x + 2, y - 1].type = (ushort)type;
				Main.tile[x - 1, y].active = true;
				Main.tile[x - 1, y].frameY = 18;
				Main.tile[x - 1, y].frameX = num;
				Main.tile[x - 1, y].type = (ushort)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
				Main.tile[x, y].frameX = (short)(18 + num);
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
				Main.tile[x + 1, y].frameX = (short)(36 + num);
				Main.tile[x + 1, y].type = (ushort)type;
				Main.tile[x + 2, y].active = true;
				Main.tile[x + 2, y].frameY = 18;
				Main.tile[x + 2, y].frameX = (short)(54 + num);
				Main.tile[x + 2, y].type = (ushort)type;
			}
		}

		public static void SwitchMB(int i, int j)
		{
			int num = Main.tile[i, j].frameX / 18;
			if (num >= 2)
			{
				num -= 2;
			}
			int num2 = i - num;
			int num3 = j - Main.tile[i, j].frameY / 18 % 2;
			for (int k = num2; k < num2 + 2; k++)
			{
				for (int l = num3; l < num3 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (Main.tile[k, l].active && Main.tile[k, l].type == 139)
					{
						if (Main.tile[k, l].frameX < 36)
						{
							Main.tile[k, l].frameX += 36;
						}
						else
						{
							Main.tile[k, l].frameX -= 36;
						}
						noWireX[numNoWire] = k;
						noWireY[numNoWire] = l;
						numNoWire++;
					}
				}
			}
			NetMessage.SendTileSquare(-1, num2, num3, 3);
		}

		public static void CheckMB(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = Main.tile[i, j].frameY / 18 / 2;
			int num2 = Main.tile[i, j].frameX / 18;
			int num3 = 0;
			if (num2 >= 2)
			{
				num2 -= 2;
				num3++;
			}
			int num4 = i - num2;
			int num5 = j - Main.tile[i, j].frameY / 18 % 2;
			for (int k = num4; k < num4 + 2; k++)
			{
				for (int l = num5; l < num5 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != (k - num4) * 18 + num3 * 36 || Main.tile[k, l].frameY != (l - num5) * 18 + num * 36)
					{
						flag = true;
					}
				}
				if (!Main.tileSolid[Main.tile[k, num5 + 2].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num4; m < num4 + 2; m++)
			{
				for (int n = num5; n < num5 + 3; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			Item.NewItem(i * 16, j * 16, 32, 32, 562 + num);
			for (int num6 = num4 - 1; num6 < num4 + 3; num6++)
			{
				for (int num7 = num5 - 1; num7 < num5 + 3; num7++)
				{
					TileFrame(num6, num7);
				}
			}
			destroyObject = false;
		}

		public static void PlaceMB(int X, int y, int type, int style)
		{
			int num = X + 1;
			if (num < 5 || num > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = num - 1; i < num + 1; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || (!Main.tileSolid[Main.tile[i, y + 1].type] && !Main.tileTable[Main.tile[i, y + 1].type]))
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[num - 1, y - 1].active = true;
				Main.tile[num - 1, y - 1].frameY = (short)(style * 36);
				Main.tile[num - 1, y - 1].frameX = 0;
				Main.tile[num - 1, y - 1].type = (ushort)type;
				Main.tile[num, y - 1].active = true;
				Main.tile[num, y - 1].frameY = (short)(style * 36);
				Main.tile[num, y - 1].frameX = 18;
				Main.tile[num, y - 1].type = (ushort)type;
				Main.tile[num - 1, y].active = true;
				Main.tile[num - 1, y].frameY = (short)(style * 36 + 18);
				Main.tile[num - 1, y].frameX = 0;
				Main.tile[num - 1, y].type = (ushort)type;
				Main.tile[num, y].active = true;
				Main.tile[num, y].frameY = (short)(style * 36 + 18);
				Main.tile[num, y].frameX = 18;
				Main.tile[num, y].type = (ushort)type;
			}
		}

		public static void Place2x2(int x, int superY, int type)
		{
			int num = superY;
			if (type == 95 || type == 126)
			{
				num++;
			}
			if (x < 5 || x > Main.maxTilesX - 5 || num < 5 || num > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 1; i++)
			{
				for (int j = num - 1; j < num + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
					else if (type == 98 && Main.tile[i, j].liquid > 0)
					{
						flag = false;
					}
				}
				if (type == 95 || type == 126)
				{
					if (Main.tile[i, num - 2] == null)
					{
						Main.tile[i, num - 2] = new Tile();
					}
					if (!Main.tile[i, num - 2].active || !Main.tileSolid[Main.tile[i, num - 2].type] || Main.tileSolidTop[Main.tile[i, num - 2].type])
					{
						flag = false;
					}
				}
				else
				{
					if (Main.tile[i, num + 1] == null)
					{
						Main.tile[i, num + 1] = new Tile();
					}
					if (!Main.tile[i, num + 1].active || (!Main.tileSolid[Main.tile[i, num + 1].type] && !Main.tileTable[Main.tile[i, num + 1].type]))
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				Main.tile[x - 1, num - 1].active = true;
				Main.tile[x - 1, num - 1].frameY = 0;
				Main.tile[x - 1, num - 1].frameX = 0;
				Main.tile[x - 1, num - 1].type = (ushort)type;
				Main.tile[x, num - 1].active = true;
				Main.tile[x, num - 1].frameY = 0;
				Main.tile[x, num - 1].frameX = 18;
				Main.tile[x, num - 1].type = (ushort)type;
				Main.tile[x - 1, num].active = true;
				Main.tile[x - 1, num].frameY = 18;
				Main.tile[x - 1, num].frameX = 0;
				Main.tile[x - 1, num].type = (ushort)type;
				Main.tile[x, num].active = true;
				Main.tile[x, num].frameY = 18;
				Main.tile[x, num].frameX = 18;
				Main.tile[x, num].type = (ushort)type;
			}
		}

		public static void Place3x4(int x, int y, int type)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 2; i++)
			{
				for (int j = y - 3; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = -3; k <= 0; k++)
				{
					short frameY = (short)((3 + k) * 18);
					Main.tile[x - 1, y + k].active = true;
					Main.tile[x - 1, y + k].frameY = frameY;
					Main.tile[x - 1, y + k].frameX = 0;
					Main.tile[x - 1, y + k].type = (ushort)type;
					Main.tile[x, y + k].active = true;
					Main.tile[x, y + k].frameY = frameY;
					Main.tile[x, y + k].frameX = 18;
					Main.tile[x, y + k].type = (ushort)type;
					Main.tile[x + 1, y + k].active = true;
					Main.tile[x + 1, y + k].frameY = frameY;
					Main.tile[x + 1, y + k].frameX = 36;
					Main.tile[x + 1, y + k].type = (ushort)type;
				}
			}
		}

		public static void Place3x2(int x, int y, int type)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x - 1, y - 1].active = true;
				Main.tile[x - 1, y - 1].frameY = 0;
				Main.tile[x - 1, y - 1].frameX = 0;
				Main.tile[x - 1, y - 1].type = (ushort)type;
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
				Main.tile[x, y - 1].frameX = 18;
				Main.tile[x, y - 1].type = (ushort)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
				Main.tile[x + 1, y - 1].frameX = 36;
				Main.tile[x + 1, y - 1].type = (ushort)type;
				Main.tile[x - 1, y].active = true;
				Main.tile[x - 1, y].frameY = 18;
				Main.tile[x - 1, y].frameX = 0;
				Main.tile[x - 1, y].type = (ushort)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
				Main.tile[x, y].frameX = 18;
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
				Main.tile[x + 1, y].frameX = 36;
				Main.tile[x + 1, y].type = (ushort)type;
			}
		}

		public static void Check3x3(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i;
			num = Main.tile[i, j].frameX / 18;
			int num2 = i - num;
			if (num >= 3)
			{
				num -= 3;
			}
			num = i - num;
			int num3 = j + Main.tile[i, j].frameY / -18;
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num3; l < num3 + 3; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX != (k - num2) * 18 || Main.tile[k, l].frameY != (l - num3) * 18)
					{
						flag = true;
					}
				}
			}
			if (type == 106)
			{
				for (int m = num; m < num + 3; m++)
				{
					if (Main.tile[m, num3 + 3] == null)
					{
						Main.tile[m, num3 + 3] = new Tile();
					}
					if (!Main.tile[m, num3 + 3].active || !Main.tileSolid[Main.tile[m, num3 + 3].type])
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				if (Main.tile[num + 1, num3 - 1] == null)
				{
					Main.tile[num + 1, num3 - 1] = new Tile();
				}
				if (!Main.tile[num + 1, num3 - 1].active || !Main.tileSolid[Main.tile[num + 1, num3 - 1].type] || Main.tileSolidTop[Main.tile[num + 1, num3 - 1].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int n = num; n < num + 3; n++)
			{
				for (int num4 = num3; num4 < num3 + 3; num4++)
				{
					if (Main.tile[n, num4].type == type && Main.tile[n, num4].active)
					{
						KillTile(n, num4);
					}
				}
			}
			switch (type)
			{
			case 34:
				Item.NewItem(i * 16, j * 16, 32, 32, 106);
				break;
			case 35:
				Item.NewItem(i * 16, j * 16, 32, 32, 107);
				break;
			case 36:
				Item.NewItem(i * 16, j * 16, 32, 32, 108);
				break;
			case 106:
				Item.NewItem(i * 16, j * 16, 32, 32, 363);
				break;
			}
			destroyObject = false;
			for (int num5 = num - 1; num5 < num + 4; num5++)
			{
				for (int num6 = num3 - 1; num6 < num3 + 4; num6++)
				{
					TileFrame(num5, num6);
				}
			}
		}

		public static void Place3x3(int x, int y, int type)
		{
			bool flag = true;
			int num = 0;
			if (type == 106)
			{
				num = -2;
				for (int i = x - 1; i < x + 2; i++)
				{
					for (int j = y - 2; j < y + 1; j++)
					{
						if (Main.tile[i, j] == null)
						{
							Main.tile[i, j] = new Tile();
						}
						if (Main.tile[i, j].active)
						{
							flag = false;
						}
					}
				}
				for (int k = x - 1; k < x + 2; k++)
				{
					if (Main.tile[k, y + 1] == null)
					{
						Main.tile[k, y + 1] = new Tile();
					}
					if (!Main.tile[k, y + 1].active || !Main.tileSolid[Main.tile[k, y + 1].type])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				for (int l = x - 1; l < x + 2; l++)
				{
					for (int m = y; m < y + 3; m++)
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						if (Main.tile[l, m].active)
						{
							flag = false;
						}
					}
				}
				if (Main.tile[x, y - 1] == null)
				{
					Main.tile[x, y - 1] = new Tile();
				}
				if (!Main.tile[x, y - 1].active || !Main.tileSolid[Main.tile[x, y - 1].type] || Main.tileSolidTop[Main.tile[x, y - 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x - 1, y + num].active = true;
				Main.tile[x - 1, y + num].frameY = 0;
				Main.tile[x - 1, y + num].frameX = 0;
				Main.tile[x - 1, y + num].type = (ushort)type;
				Main.tile[x, y + num].active = true;
				Main.tile[x, y + num].frameY = 0;
				Main.tile[x, y + num].frameX = 18;
				Main.tile[x, y + num].type = (ushort)type;
				Main.tile[x + 1, y + num].active = true;
				Main.tile[x + 1, y + num].frameY = 0;
				Main.tile[x + 1, y + num].frameX = 36;
				Main.tile[x + 1, y + num].type = (ushort)type;
				Main.tile[x - 1, y + 1 + num].active = true;
				Main.tile[x - 1, y + 1 + num].frameY = 18;
				Main.tile[x - 1, y + 1 + num].frameX = 0;
				Main.tile[x - 1, y + 1 + num].type = (ushort)type;
				Main.tile[x, y + 1 + num].active = true;
				Main.tile[x, y + 1 + num].frameY = 18;
				Main.tile[x, y + 1 + num].frameX = 18;
				Main.tile[x, y + 1 + num].type = (ushort)type;
				Main.tile[x + 1, y + 1 + num].active = true;
				Main.tile[x + 1, y + 1 + num].frameY = 18;
				Main.tile[x + 1, y + 1 + num].frameX = 36;
				Main.tile[x + 1, y + 1 + num].type = (ushort)type;
				Main.tile[x - 1, y + 2 + num].active = true;
				Main.tile[x - 1, y + 2 + num].frameY = 36;
				Main.tile[x - 1, y + 2 + num].frameX = 0;
				Main.tile[x - 1, y + 2 + num].type = (ushort)type;
				Main.tile[x, y + 2 + num].active = true;
				Main.tile[x, y + 2 + num].frameY = 36;
				Main.tile[x, y + 2 + num].frameX = 18;
				Main.tile[x, y + 2 + num].type = (ushort)type;
				Main.tile[x + 1, y + 2 + num].active = true;
				Main.tile[x + 1, y + 2 + num].frameY = 36;
				Main.tile[x + 1, y + 2 + num].frameX = 36;
				Main.tile[x + 1, y + 2 + num].type = (ushort)type;
			}
		}

		public static void PlaceSunflower(int x, int y, int type = 27)
		{
			if ((double)y > Main.worldSurface - 1.0)
			{
				return;
			}
			bool flag = true;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 3; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active || Main.tile[i, j].wall > 0)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || (Main.tile[i, y + 1].type != 2 && Main.tile[i, y + 1].type != 109))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				return;
			}
			for (int k = 0; k < 2; k++)
			{
				for (int l = -3; l < 1; l++)
				{
					int num = k * 18 + genRand.Next(3) * 36;
					int num2 = (l + 3) * 18;
					Main.tile[x + k, y + l].active = true;
					Main.tile[x + k, y + l].frameX = (short)num;
					Main.tile[x + k, y + l].frameY = (short)num2;
					Main.tile[x + k, y + l].type = (ushort)type;
				}
			}
		}

		public static void CheckSunflower(int i, int j, int type = 27)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = Main.tile[i, j].frameX / 18;
			int num2 = j + Main.tile[i, j].frameY / -18;
			while (num > 1)
			{
				num -= 2;
			}
			num *= -1;
			num += i;
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 4; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX / 18 % 2 != k - num || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 4] == null)
				{
					Main.tile[k, num2 + 4] = new Tile();
				}
				if (!Main.tile[k, num2 + 4].active || (Main.tile[k, num2 + 4].type != 2 && Main.tile[k, num2 + 4].type != 109))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			for (int m = num; m < num + 2; m++)
			{
				for (int n = num2; n < num2 + 4; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			Item.NewItem(i * 16, j * 16, 32, 32, 63);
			destroyObject = false;
		}

		public static bool PlacePot(int x, int y, int type = 28)
		{
			bool flag = true;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = -1; l < 1; l++)
					{
						int num = k * 18 + genRand.Next(3) * 36;
						int num2 = (l + 1) * 18;
						Main.tile[x + k, y + l].active = true;
						Main.tile[x + k, y + l].frameX = (short)num;
						Main.tile[x + k, y + l].frameY = (short)num2;
						Main.tile[x + k, y + l].type = (ushort)type;
					}
				}
				return true;
			}
			return false;
		}

		public static bool CheckCactus(int i, int j)
		{
			int num = j;
			int num2 = i;
			while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
			{
				num++;
				if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
				{
					if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
					{
						num2--;
					}
					if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
					{
						num2++;
					}
				}
			}
			if (!Main.tile[num2, num].active || (Main.tile[num2, num].type != 53 && Main.tile[num2, num].type != 112 && Main.tile[num2, num].type != 116))
			{
				KillTile(i, j);
				return true;
			}
			if (i != num2)
			{
				if ((!Main.tile[i, j + 1].active || Main.tile[i, j + 1].type != 80) && (!Main.tile[i - 1, j].active || Main.tile[i - 1, j].type != 80) && (!Main.tile[i + 1, j].active || Main.tile[i + 1, j].type != 80))
				{
					KillTile(i, j);
					return true;
				}
			}
			else if (i == num2 && (!Main.tile[i, j + 1].active || (Main.tile[i, j + 1].type != 80 && Main.tile[i, j + 1].type != 53 && Main.tile[i, j + 1].type != 112 && Main.tile[i, j + 1].type != 116)))
			{
				KillTile(i, j);
				return true;
			}
			return false;
		}

		public static void PlantCactus(int i, int j)
		{
			GrowCactus(i, j);
			for (int k = 0; k < 150; k++)
			{
				GrowCactus(genRand.Next(i - 1, i + 2), genRand.Next(j - 10, j + 2));
			}
		}

		public static void CheckOrb(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			int num = i;
			int num2 = j;
			num = ((Main.tile[i, j].frameX != 0) ? (i - 1) : i);
			num2 = ((Main.tile[i, j].frameY != 0) ? (j - 1) : j);
			if (Main.tile[num, num2] == null || Main.tile[num + 1, num2] == null || Main.tile[num, num2 + 1] == null || Main.tile[num + 1, num2 + 1] == null || (Main.tile[num, num2].active && Main.tile[num, num2].type == type && Main.tile[num + 1, num2].active && Main.tile[num + 1, num2].type == type && Main.tile[num, num2 + 1].active && Main.tile[num, num2 + 1].type == type && Main.tile[num + 1, num2 + 1].active && Main.tile[num + 1, num2 + 1].type == type))
			{
				return;
			}
			destroyObject = true;
			if (Main.tile[num, num2].type == type)
			{
				KillTile(num, num2);
			}
			if (Main.tile[num + 1, num2].type == type)
			{
				KillTile(num + 1, num2);
			}
			if (Main.tile[num, num2 + 1].type == type)
			{
				KillTile(num, num2 + 1);
			}
			if (Main.tile[num + 1, num2 + 1].type == type)
			{
				KillTile(num + 1, num2 + 1);
			}
			if (Main.netMode != 1 && !noTileActions)
			{
				switch (type)
				{
				case 12:
					Item.NewItem(num * 16, num2 * 16, 32, 32, 29);
					break;
				case 31:
				{
					if (genRand.Next(2) == 0)
					{
						spawnMeteor = true;
					}
					int num3 = Main.rand.Next(5);
					if (!shadowOrbSmashed)
					{
						num3 = 0;
					}
					switch (num3)
					{
					case 0:
						Item.NewItem(num * 16, num2 * 16, 32, 32, 96, 1, noBroadcast: false, -1);
						Item.NewItem(num * 16, num2 * 16, 32, 32, 97, genRand.Next(25, 51));
						break;
					case 1:
						Item.NewItem(num * 16, num2 * 16, 32, 32, 64, 1, noBroadcast: false, -1);
						break;
					case 2:
						Item.NewItem(num * 16, num2 * 16, 32, 32, 162, 1, noBroadcast: false, -1);
						break;
					case 3:
						Item.NewItem(num * 16, num2 * 16, 32, 32, 115, 1, noBroadcast: false, -1);
						break;
					case 4:
						Item.NewItem(num * 16, num2 * 16, 32, 32, 111, 1, noBroadcast: false, -1);
						break;
					}
					shadowOrbSmashed = true;
					shadowOrbCount++;
					if (shadowOrbCount >= 3)
					{
						shadowOrbCount = 0;
						float num4 = num * 16;
						float num5 = num2 * 16;
						float num6 = -1f;
						int plr = 0;
						for (int k = 0; k < 255; k++)
						{
							float num7 = Math.Abs(Main.player[k].position.X - num4) + Math.Abs(Main.player[k].position.Y - num5);
							if (num7 < num6 || num6 == -1f)
							{
								plr = k;
								num6 = num7;
							}
						}
						NPC.SpawnOnPlayer(plr, 13);
					}
					else
					{
						string text = Lang.misc[10];
						if (shadowOrbCount == 2)
						{
							text = Lang.misc[11];
						}
						if (Main.netMode == 0)
						{
							Main.NewText(text, 50, byte.MaxValue, 130);
						}
						else if (Main.netMode == 2)
						{
							NetMessage.SendData(25, -1, -1, text, 255, 50f, 255f, 130f);
						}
					}
					break;
				}
				}
			}
			Main.PlaySound(13, i * 16, j * 16);
			destroyObject = false;
		}

		public static void CheckTree(int i, int j)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			Tile tile = Main.tile[i, j];
			int type = tile.type;
			int frameX = tile.frameX;
			int frameY = tile.frameY;
			if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
			{
				num4 = Main.tile[i - 1, j].type;
			}
			if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
			{
				num5 = Main.tile[i + 1, j].type;
			}
			if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
			{
				num2 = Main.tile[i, j - 1].type;
			}
			if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
			{
				num7 = Main.tile[i, j + 1].type;
			}
			if (Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
			{
				num = Main.tile[i - 1, j - 1].type;
			}
			if (Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
			{
				num3 = Main.tile[i + 1, j - 1].type;
			}
			if (Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
			{
				num6 = Main.tile[i - 1, j + 1].type;
			}
			if (Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
			{
				num8 = Main.tile[i + 1, j + 1].type;
			}
			if (num4 >= 0 && Main.tileStone[num4])
			{
				num4 = 1;
			}
			if (num5 >= 0 && Main.tileStone[num5])
			{
				num5 = 1;
			}
			if (num2 >= 0 && Main.tileStone[num2])
			{
				num2 = 1;
			}
			if (num7 >= 0 && Main.tileStone[num7])
			{
				num7 = 1;
			}
			if (num >= 0 && Main.tileStone[num])
			{
				num = 1;
			}
			if (num3 >= 0 && Main.tileStone[num3])
			{
				num3 = 1;
			}
			if (num6 >= 0 && Main.tileStone[num6])
			{
				num6 = 1;
			}
			if (num8 >= 0 && Main.tileStone[num8])
			{
				num8 = 1;
			}
			if (num7 == 23 || num7 == 60 || num7 == 109 || num7 == 147)
			{
				num7 = 2;
			}
			if (tile.frameX >= 22 && tile.frameX <= 44 && tile.frameY >= 132 && tile.frameY <= 176)
			{
				if (num7 != 2)
				{
					KillTile(i, j);
				}
				else if ((tile.frameX != 22 || num4 != type) && (tile.frameX != 44 || num5 != type))
				{
					KillTile(i, j);
				}
			}
			else if ((tile.frameX == 88 && tile.frameY >= 0 && tile.frameY <= 44) || (tile.frameX == 66 && tile.frameY >= 66 && tile.frameY <= 130) || (tile.frameX == 110 && tile.frameY >= 66 && tile.frameY <= 110) || (tile.frameX == 132 && tile.frameY >= 0 && tile.frameY <= 176))
			{
				if (num4 == type && num5 == type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 110;
						tile.frameY = 66;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 110;
						tile.frameY = 88;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 110;
						tile.frameY = 110;
					}
				}
				else if (num4 == type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 88;
						tile.frameY = 0;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 88;
						tile.frameY = 22;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 88;
						tile.frameY = 44;
					}
				}
				else if (num5 == type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 66;
						tile.frameY = 66;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 66;
						tile.frameY = 88;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 66;
						tile.frameY = 110;
					}
				}
				else if (tile.frameNumber == 0)
				{
					tile.frameX = 0;
					tile.frameY = 0;
				}
				else if (tile.frameNumber == 1)
				{
					tile.frameX = 0;
					tile.frameY = 22;
				}
				else if (tile.frameNumber == 2)
				{
					tile.frameX = 0;
					tile.frameY = 44;
				}
			}
			if (tile.frameY >= 132 && tile.frameY <= 176 && (tile.frameX == 0 || tile.frameX == 66 || tile.frameX == 88))
			{
				if (num7 != 2)
				{
					KillTile(i, j);
				}
				if (num4 != type && num5 != type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 0;
						tile.frameY = 0;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 0;
						tile.frameY = 22;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 0;
						tile.frameY = 44;
					}
				}
				else if (num4 != type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 0;
						tile.frameY = 132;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 0;
						tile.frameY = 154;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 0;
						tile.frameY = 176;
					}
				}
				else if (num5 != type)
				{
					if (tile.frameNumber == 0)
					{
						tile.frameX = 66;
						tile.frameY = 132;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 66;
						tile.frameY = 154;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 66;
						tile.frameY = 176;
					}
				}
				else if (tile.frameNumber == 0)
				{
					tile.frameX = 88;
					tile.frameY = 132;
				}
				else if (tile.frameNumber == 1)
				{
					tile.frameX = 88;
					tile.frameY = 154;
				}
				else if (tile.frameNumber == 2)
				{
					tile.frameX = 88;
					tile.frameY = 176;
				}
			}
			if ((tile.frameX == 66 && (tile.frameY == 0 || tile.frameY == 22 || tile.frameY == 44)) || (tile.frameX == 44 && (tile.frameY == 198 || tile.frameY == 220 || tile.frameY == 242)))
			{
				if (num5 != type)
				{
					KillTile(i, j);
				}
			}
			else if ((tile.frameX == 88 && (tile.frameY == 66 || tile.frameY == 88 || tile.frameY == 110)) || (tile.frameX == 66 && (tile.frameY == 198 || tile.frameY == 220 || tile.frameY == 242)))
			{
				if (num4 != type)
				{
					KillTile(i, j);
				}
			}
			else if (num7 == -1 || num7 == 23)
			{
				KillTile(i, j);
			}
			else if (num2 != type && tile.frameY < 198 && ((tile.frameX != 22 && tile.frameX != 44) || tile.frameY < 132))
			{
				if (num4 == type || num5 == type)
				{
					if (num7 == type)
					{
						if (num4 == type && num5 == type)
						{
							if (tile.frameNumber == 0)
							{
								tile.frameX = 132;
								tile.frameY = 132;
							}
							else if (tile.frameNumber == 1)
							{
								tile.frameX = 132;
								tile.frameY = 154;
							}
							else if (tile.frameNumber == 2)
							{
								tile.frameX = 132;
								tile.frameY = 176;
							}
						}
						else if (num4 == type)
						{
							if (tile.frameNumber == 0)
							{
								tile.frameX = 132;
								tile.frameY = 0;
							}
							else if (tile.frameNumber == 1)
							{
								tile.frameX = 132;
								tile.frameY = 22;
							}
							else if (tile.frameNumber == 2)
							{
								tile.frameX = 132;
								tile.frameY = 44;
							}
						}
						else if (tile.frameNumber == 0)
						{
							tile.frameX = 132;
							tile.frameY = 66;
						}
						else if (tile.frameNumber == 1)
						{
							tile.frameX = 132;
							tile.frameY = 88;
						}
						else if (tile.frameNumber == 2)
						{
							tile.frameX = 132;
							tile.frameY = 110;
						}
					}
					else if (num4 == type && num5 == type)
					{
						if (tile.frameNumber == 0)
						{
							tile.frameX = 154;
							tile.frameY = 132;
						}
						else if (tile.frameNumber == 1)
						{
							tile.frameX = 154;
							tile.frameY = 154;
						}
						else if (tile.frameNumber == 2)
						{
							tile.frameX = 154;
							tile.frameY = 176;
						}
					}
					else if (num4 == type)
					{
						if (tile.frameNumber == 0)
						{
							tile.frameX = 154;
							tile.frameY = 0;
						}
						else if (tile.frameNumber == 1)
						{
							tile.frameX = 154;
							tile.frameY = 22;
						}
						else if (tile.frameNumber == 2)
						{
							tile.frameX = 154;
							tile.frameY = 44;
						}
					}
					else if (tile.frameNumber == 0)
					{
						tile.frameX = 154;
						tile.frameY = 66;
					}
					else if (tile.frameNumber == 1)
					{
						tile.frameX = 154;
						tile.frameY = 88;
					}
					else if (tile.frameNumber == 2)
					{
						tile.frameX = 154;
						tile.frameY = 110;
					}
				}
				else if (tile.frameNumber == 0)
				{
					tile.frameX = 110;
					tile.frameY = 0;
				}
				else if (tile.frameNumber == 1)
				{
					tile.frameX = 110;
					tile.frameY = 22;
				}
				else if (tile.frameNumber == 2)
				{
					tile.frameX = 110;
					tile.frameY = 44;
				}
			}
			if (tile.frameX != frameX && tile.frameY != frameY && frameX >= 0 && frameY >= 0)
			{
				TileFrame(i - 1, j);
				TileFrame(i + 1, j);
				TileFrame(i, j - 1);
				TileFrame(i, j + 1);
			}
		}

		public static void CactusFrame(int i, int j)
		{
			try
			{
				int num = j;
				int num2 = i;
				if (!CheckCactus(i, j))
				{
					while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
					{
						num++;
						if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
						{
							if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
							{
								num2--;
							}
							if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
							{
								num2++;
							}
						}
					}
					num--;
					int num3 = i - num2;
					num2 = i;
					num = j;
					int type = Main.tile[i - 2, j].type;
					int num4 = Main.tile[i - 1, j].type;
					int num5 = Main.tile[i + 1, j].type;
					int num6 = Main.tile[i, j - 1].type;
					int num7 = Main.tile[i, j + 1].type;
					int num8 = Main.tile[i - 1, j + 1].type;
					int num9 = Main.tile[i + 1, j + 1].type;
					if (!Main.tile[i - 1, j].active)
					{
						num4 = -1;
					}
					if (!Main.tile[i + 1, j].active)
					{
						num5 = -1;
					}
					if (!Main.tile[i, j - 1].active)
					{
						num6 = -1;
					}
					if (!Main.tile[i, j + 1].active)
					{
						num7 = -1;
					}
					if (!Main.tile[i - 1, j + 1].active)
					{
						num8 = -1;
					}
					if (!Main.tile[i + 1, j + 1].active)
					{
						num9 = -1;
					}
					short num10 = Main.tile[i, j].frameX;
					short num11 = Main.tile[i, j].frameY;
					switch (num3)
					{
					case 0:
						if (num6 != 80)
						{
							if (num4 == 80 && num5 == 80 && num8 != 80 && num9 != 80 && type != 80)
							{
								num10 = 90;
								num11 = 0;
							}
							else if (num4 == 80 && num8 != 80 && type != 80)
							{
								num10 = 72;
								num11 = 0;
							}
							else if (num5 == 80 && num9 != 80)
							{
								num10 = 18;
								num11 = 0;
							}
							else
							{
								num10 = 0;
								num11 = 0;
							}
						}
						else if (num4 == 80 && num5 == 80 && num8 != 80 && num9 != 80 && type != 80)
						{
							num10 = 90;
							num11 = 36;
						}
						else if (num4 == 80 && num8 != 80 && type != 80)
						{
							num10 = 72;
							num11 = 36;
						}
						else if (num5 == 80 && num9 != 80)
						{
							num10 = 18;
							num11 = 36;
						}
						else if (num7 >= 0 && Main.tileSolid[num7])
						{
							num10 = 0;
							num11 = 36;
						}
						else
						{
							num10 = 0;
							num11 = 18;
						}
						break;
					case -1:
						if (num5 == 80)
						{
							if (num6 != 80 && num7 != 80)
							{
								num10 = 108;
								num11 = 36;
							}
							else if (num7 != 80)
							{
								num10 = 54;
								num11 = 36;
							}
							else if (num6 != 80)
							{
								num10 = 54;
								num11 = 0;
							}
							else
							{
								num10 = 54;
								num11 = 18;
							}
						}
						else if (num6 != 80)
						{
							num10 = 54;
							num11 = 0;
						}
						else
						{
							num10 = 54;
							num11 = 18;
						}
						break;
					case 1:
						if (num4 == 80)
						{
							if (num6 != 80 && num7 != 80)
							{
								num10 = 108;
								num11 = 16;
							}
							else if (num7 != 80)
							{
								num10 = 36;
								num11 = 36;
							}
							else if (num6 != 80)
							{
								num10 = 36;
								num11 = 0;
							}
							else
							{
								num10 = 36;
								num11 = 18;
							}
						}
						else if (num6 != 80)
						{
							num10 = 36;
							num11 = 0;
						}
						else
						{
							num10 = 36;
							num11 = 18;
						}
						break;
					}
					if (num10 != Main.tile[i, j].frameX || num11 != Main.tile[i, j].frameY)
					{
						Main.tile[i, j].frameX = num10;
						Main.tile[i, j].frameY = num11;
						SquareTileFrame(i, j);
					}
				}
			}
			catch
			{
				Main.tile[i, j].frameX = 0;
				Main.tile[i, j].frameY = 0;
			}
		}

		public static void GrowCactus(int i, int j)
		{
			int num = j;
			int num2 = i;
			if (!Main.tile[i, j].active || Main.tile[i, j - 1].liquid > 0 || (Main.tile[i, j].type != 53 && Main.tile[i, j].type != 80 && Main.tile[i, j].type != 112 && Main.tile[i, j].type != 116))
			{
				return;
			}
			if (Main.tile[i, j].type == 53 || Main.tile[i, j].type == 112 || Main.tile[i, j].type == 116)
			{
				if (Main.tile[i, j - 1].active || Main.tile[i - 1, j - 1].active || Main.tile[i + 1, j - 1].active)
				{
					return;
				}
				int num3 = 0;
				int num4 = 0;
				for (int k = i - 6; k <= i + 6; k++)
				{
					for (int l = j - 3; l <= j + 1; l++)
					{
						try
						{
							if (Main.tile[k, l].active)
							{
								if (Main.tile[k, l].type == 80)
								{
									num3++;
									if (num3 >= 4)
									{
										return;
									}
								}
								if (Main.tile[k, l].type == 53 || Main.tile[k, l].type == 112 || Main.tile[k, l].type == 116)
								{
									num4++;
								}
							}
						}
						catch
						{
						}
					}
				}
				if (num4 > 10)
				{
					Main.tile[i, j - 1].active = true;
					Main.tile[i, j - 1].type = 80;
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, j - 1, 1);
					}
					SquareTileFrame(num2, num - 1);
				}
			}
			else
			{
				if (Main.tile[i, j].type != 80)
				{
					return;
				}
				while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
				{
					num++;
					if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
					{
						if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
						{
							num2--;
						}
						if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
						{
							num2++;
						}
					}
				}
				num--;
				int num5 = num - j;
				int num6 = i - num2;
				num2 = i - num6;
				num = j;
				int num7 = 11 - num5;
				int num8 = 0;
				for (int m = num2 - 2; m <= num2 + 2; m++)
				{
					for (int n = num - num7; n <= num + num5; n++)
					{
						if (Main.tile[m, n].active && Main.tile[m, n].type == 80)
						{
							num8++;
						}
					}
				}
				if (num8 >= genRand.Next(11, 13))
				{
					return;
				}
				num2 = i;
				num = j;
				if (num6 == 0)
				{
					if (num5 == 0)
					{
						if (!Main.tile[num2, num - 1].active)
						{
							Main.tile[num2, num - 1].active = true;
							Main.tile[num2, num - 1].type = 80;
							SquareTileFrame(num2, num - 1);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num2, num - 1, 1);
							}
						}
						return;
					}
					bool flag = false;
					bool flag2 = false;
					if (Main.tile[num2, num - 1].active && Main.tile[num2, num - 1].type == 80)
					{
						if (!Main.tile[num2 - 1, num].active && !Main.tile[num2 - 2, num + 1].active && !Main.tile[num2 - 1, num - 1].active && !Main.tile[num2 - 1, num + 1].active && !Main.tile[num2 - 2, num].active)
						{
							flag = true;
						}
						if (!Main.tile[num2 + 1, num].active && !Main.tile[num2 + 2, num + 1].active && !Main.tile[num2 + 1, num - 1].active && !Main.tile[num2 + 1, num + 1].active && !Main.tile[num2 + 2, num].active)
						{
							flag2 = true;
						}
					}
					int num9 = genRand.Next(3);
					if (num9 == 0 && flag)
					{
						Main.tile[num2 - 1, num].active = true;
						Main.tile[num2 - 1, num].type = 80;
						SquareTileFrame(num2 - 1, num);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num2 - 1, num, 1);
						}
					}
					else if (num9 == 1 && flag2)
					{
						Main.tile[num2 + 1, num].active = true;
						Main.tile[num2 + 1, num].type = 80;
						SquareTileFrame(num2 + 1, num);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num2 + 1, num, 1);
						}
					}
					else if (num5 < genRand.Next(2, 8) && (!Main.tile[num2 + 1, num - 1].active || Main.tile[num2 + 1, num - 1].type != 80) && !Main.tile[num2, num - 1].active)
					{
						Main.tile[num2, num - 1].active = true;
						Main.tile[num2, num - 1].type = 80;
						SquareTileFrame(num2, num - 1);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num2, num - 1, 1);
						}
					}
				}
				else if (!Main.tile[num2, num - 1].active && !Main.tile[num2, num - 2].active && !Main.tile[num2 + num6, num - 1].active && Main.tile[num2 - num6, num - 1].active && Main.tile[num2 - num6, num - 1].type == 80)
				{
					Main.tile[num2, num - 1].active = true;
					Main.tile[num2, num - 1].type = 80;
					SquareTileFrame(num2, num - 1);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, num2, num - 1, 1);
					}
				}
			}
		}

		public static void CheckPot(int i, int j, int type = 28)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = -(Main.tile[i, j].frameX / 18 % 2) + i;
			int num2 = j + Main.tile[i, j].frameY / -18;
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX / 18 % 2 != k - num || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			destroyObject = true;
			Main.PlaySound(13, i * 16, j * 16);
			for (int m = num; m < num + 2; m++)
			{
				for (int n = num2; n < num2 + 2; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						KillTile(m, n);
					}
				}
			}
			Vector2 position = default(Vector2);
			position.X = i * 16;
			position.Y = j * 16;
			Gore.NewGore(position, Vector2.Zero, 51);
			Gore.NewGore(position, Vector2.Zero, 52);
			Gore.NewGore(position, Vector2.Zero, 53);
			if (genRand.Next(40) == 0 && (Main.tile[num, num2].wall == 7 || Main.tile[num, num2].wall == 8 || Main.tile[num, num2].wall == 9))
			{
				Item.NewItem(i * 16, j * 16, 16, 16, 327);
			}
			else if (genRand.Next(45) == 0)
			{
				if ((double)j < Main.worldSurface)
				{
					switch (genRand.Next(4))
					{
					case 0:
						Item.NewItem(i * 16, j * 16, 16, 16, 292);
						break;
					case 1:
						Item.NewItem(i * 16, j * 16, 16, 16, 298);
						break;
					case 2:
						Item.NewItem(i * 16, j * 16, 16, 16, 299);
						break;
					case 3:
						Item.NewItem(i * 16, j * 16, 16, 16, 290);
						break;
					}
				}
				else if ((double)j < Main.rockLayer)
				{
					switch (genRand.Next(7))
					{
					case 0:
						Item.NewItem(i * 16, j * 16, 16, 16, 289);
						break;
					case 1:
						Item.NewItem(i * 16, j * 16, 16, 16, 298);
						break;
					case 2:
						Item.NewItem(i * 16, j * 16, 16, 16, 299);
						break;
					case 3:
						Item.NewItem(i * 16, j * 16, 16, 16, 290);
						break;
					case 4:
						Item.NewItem(i * 16, j * 16, 16, 16, 303);
						break;
					case 5:
						Item.NewItem(i * 16, j * 16, 16, 16, 291);
						break;
					case 6:
						Item.NewItem(i * 16, j * 16, 16, 16, 304);
						break;
					}
				}
				else if ((double)j < Main.hellLayer)
				{
					switch (genRand.Next(10))
					{
					case 0:
						Item.NewItem(i * 16, j * 16, 16, 16, 296);
						break;
					case 1:
						Item.NewItem(i * 16, j * 16, 16, 16, 295);
						break;
					case 2:
						Item.NewItem(i * 16, j * 16, 16, 16, 299);
						break;
					case 3:
						Item.NewItem(i * 16, j * 16, 16, 16, 302);
						break;
					case 4:
						Item.NewItem(i * 16, j * 16, 16, 16, 303);
						break;
					case 5:
						Item.NewItem(i * 16, j * 16, 16, 16, 305);
						break;
					case 6:
						Item.NewItem(i * 16, j * 16, 16, 16, 301);
						break;
					case 7:
						Item.NewItem(i * 16, j * 16, 16, 16, 302);
						break;
					case 8:
						Item.NewItem(i * 16, j * 16, 16, 16, 297);
						break;
					case 9:
						Item.NewItem(i * 16, j * 16, 16, 16, 304);
						break;
					}
				}
				else
				{
					switch (genRand.Next(12))
					{
					case 0:
						Item.NewItem(i * 16, j * 16, 16, 16, 296);
						break;
					case 1:
						Item.NewItem(i * 16, j * 16, 16, 16, 295);
						break;
					case 2:
						Item.NewItem(i * 16, j * 16, 16, 16, 293);
						break;
					case 3:
						Item.NewItem(i * 16, j * 16, 16, 16, 288);
						break;
					case 4:
						Item.NewItem(i * 16, j * 16, 16, 16, 294);
						break;
					case 5:
						Item.NewItem(i * 16, j * 16, 16, 16, 297);
						break;
					case 6:
						Item.NewItem(i * 16, j * 16, 16, 16, 304);
						break;
					case 7:
						Item.NewItem(i * 16, j * 16, 16, 16, 305);
						break;
					case 8:
						Item.NewItem(i * 16, j * 16, 16, 16, 301);
						break;
					case 9:
						Item.NewItem(i * 16, j * 16, 16, 16, 302);
						break;
					case 10:
						Item.NewItem(i * 16, j * 16, 16, 16, 288);
						break;
					case 11:
						Item.NewItem(i * 16, j * 16, 16, 16, 300);
						break;
					}
				}
			}
			else
			{
				int num3 = Main.rand.Next(8);
				int num4 = Player.FindClosest(position, 16, 16);
				if (num3 == 0 && Main.player[num4].statLife < Main.player[num4].statLifeMax2)
				{
					Item.NewItem(i * 16, j * 16, 16, 16, 58);
				}
				else if (num3 == 1 && Main.player[num4].statMana < Main.player[num4].statManaMax)
				{
					Item.NewItem(i * 16, j * 16, 16, 16, 184);
				}
				else if (num3 == 2)
				{
					int stack = Main.rand.Next(1, 6);
					if (Main.tile[i, j].liquid > 0)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 282, stack);
					}
					else
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 8, stack);
					}
				}
				else if (num3 == 3)
				{
					int stack2 = Main.rand.Next(8) + 3;
					int type2 = 40;
					if ((double)j < Main.rockLayer && genRand.Next(2) == 0)
					{
						type2 = ((!Main.hardMode) ? 42 : 168);
					}
					if ((double)j > Main.hellLayer)
					{
						type2 = 265;
					}
					else if (Main.hardMode)
					{
						type2 = ((Main.rand.Next(2) != 0) ? 47 : 278);
					}
					Item.NewItem(i * 16, j * 16, 16, 16, type2, stack2);
				}
				else if (num3 == 4)
				{
					int type3 = 28;
					if ((double)j > Main.hellLayer || Main.hardMode)
					{
						type3 = 188;
					}
					Item.NewItem(i * 16, j * 16, 16, 16, type3);
				}
				else if (num3 == 5 && (double)j > Main.rockLayer)
				{
					int stack3 = Main.rand.Next(4) + 1;
					Item.NewItem(i * 16, j * 16, 16, 16, 166, stack3);
				}
				else
				{
					float num5 = 200 + genRand.Next(-100, 101);
					if ((double)j < Main.worldSurface)
					{
						num5 *= 0.5f;
					}
					else if ((double)j < Main.rockLayer)
					{
						num5 *= 0.75f;
					}
					else if ((double)j > Main.hellLayer - 50.0)
					{
						num5 *= 1.25f;
					}
					num5 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
					if (Main.rand.Next(5) == 0)
					{
						num5 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
					}
					if (Main.rand.Next(10) == 0)
					{
						num5 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
					}
					if (Main.rand.Next(15) == 0)
					{
						num5 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
					}
					if (Main.rand.Next(20) == 0)
					{
						num5 *= 1f + (float)Main.rand.Next(40, 81) * 0.01f;
					}
					if (Main.rand.Next(25) == 0)
					{
						num5 *= 1f + (float)Main.rand.Next(50, 101) * 0.01f;
					}
					while ((int)num5 > 0)
					{
						if (num5 > 1000000f)
						{
							int num6 = (int)(num5 / 1000000f);
							if (num6 > 50 && Main.rand.Next(2) == 0)
							{
								num6 /= Main.rand.Next(3) + 1;
							}
							if (Main.rand.Next(2) == 0)
							{
								num6 /= Main.rand.Next(3) + 1;
							}
							num5 -= (float)(1000000 * num6);
							Item.NewItem(i * 16, j * 16, 16, 16, 74, num6);
							continue;
						}
						if (num5 > 10000f)
						{
							int num7 = (int)(num5 / 10000f);
							if (num7 > 50 && Main.rand.Next(2) == 0)
							{
								num7 /= Main.rand.Next(3) + 1;
							}
							if (Main.rand.Next(2) == 0)
							{
								num7 /= Main.rand.Next(3) + 1;
							}
							num5 -= (float)(10000 * num7);
							Item.NewItem(i * 16, j * 16, 16, 16, 73, num7);
							continue;
						}
						if (num5 > 100f)
						{
							int num8 = (int)(num5 / 100f);
							if (num8 > 50 && Main.rand.Next(2) == 0)
							{
								num8 /= Main.rand.Next(3) + 1;
							}
							if (Main.rand.Next(2) == 0)
							{
								num8 /= Main.rand.Next(3) + 1;
							}
							num5 -= (float)(100 * num8);
							Item.NewItem(i * 16, j * 16, 16, 16, 72, num8);
							continue;
						}
						int num9 = (int)num5;
						if (num9 > 50 && Main.rand.Next(2) == 0)
						{
							num9 /= Main.rand.Next(3) + 1;
						}
						if (Main.rand.Next(2) == 0)
						{
							num9 /= Main.rand.Next(4) + 1;
						}
						if (num9 < 1)
						{
							num9 = 1;
						}
						num5 -= (float)num9;
						Item.NewItem(i * 16, j * 16, 16, 16, 71, num9);
					}
				}
			}
			destroyObject = false;
		}

		public static int PlaceChest(int x, int y, int type = 21, bool notNearOtherChests = false, int style = 0)
		{
			bool flag = true;
			int num = -1;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
					if (Main.tile[i, j].lava)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag && notNearOtherChests)
			{
				for (int k = x - 25; k < x + 25; k++)
				{
					for (int l = y - 8; l < y + 8; l++)
					{
						try
						{
							if (Main.tile[k, l].active && Main.tile[k, l].type == 21)
							{
								flag = false;
								return -1;
							}
						}
						catch
						{
						}
					}
				}
			}
			if (flag)
			{
				num = Chest.CreateChest(x, y - 1);
				if (num == -1)
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
				Main.tile[x, y - 1].frameX = (short)(36 * style);
				Main.tile[x, y - 1].type = (ushort)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
				Main.tile[x + 1, y - 1].frameX = (short)(18 + 36 * style);
				Main.tile[x + 1, y - 1].type = (ushort)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
				Main.tile[x, y].frameX = (short)(36 * style);
				Main.tile[x, y].type = (ushort)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
				Main.tile[x + 1, y].frameX = (short)(18 + 36 * style);
				Main.tile[x + 1, y].type = (ushort)type;
			}
			return num;
		}

		public static void CheckChest(int i, int j, int type)
		{
			if (destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = -(Main.tile[i, j].frameX / 18 % 2) + i;
			int num2 = j + Main.tile[i, j].frameY / -18;
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || Main.tile[k, l].type != type || Main.tile[k, l].frameX / 18 % 2 != k - num || Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			int type2 = 48;
			if (Main.tile[i, j].frameX >= 216)
			{
				type2 = 348;
			}
			else if (Main.tile[i, j].frameX >= 180)
			{
				type2 = 343;
			}
			else if (Main.tile[i, j].frameX >= 108)
			{
				type2 = 328;
			}
			else if (Main.tile[i, j].frameX >= 36)
			{
				type2 = 306;
			}
			destroyObject = true;
			for (int m = num; m < num + 2; m++)
			{
				for (int n = num2; n < num2 + 3; n++)
				{
					if (Main.tile[m, n].type == type && Main.tile[m, n].active)
					{
						Chest.DestroyChest(m, n);
						KillTile(m, n);
					}
				}
			}
			Item.NewItem(i * 16, j * 16, 32, 32, type2);
			destroyObject = false;
		}

		public static bool PlaceWire(int i, int j)
		{
			if (!Main.tile[i, j].wire)
			{
				Main.PlaySound(0, i * 16, j * 16);
				Main.tile[i, j].wire = true;
				return true;
			}
			return false;
		}

		public static bool KillWire(int i, int j)
		{
			if (Main.tile[i, j].wire)
			{
				Main.PlaySound(0, i * 16, j * 16);
				Main.tile[i, j].wire = false;
				if (Main.netMode != 1)
				{
					Item.NewItem(i * 16, j * 16, 16, 16, 530);
				}
				for (int k = 0; k < 5; k++)
				{
					Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 50, 0f, 0f, 0, Color.Transparent);
				}
				return true;
			}
			return false;
		}

		public static bool PlaceTile(int i, int j, int type, bool mute = false, bool forced = false, int plr = -1, int style = 0)
		{
			bool result = false;
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
			{
				if (Main.tile[i, j] == null)
				{
					Main.tile[i, j] = new Tile();
				}
				Config.tilesPretend = true;
				if (forced || Collision.EmptyTile(i, j) || !Main.tileSolid[type] || (type == 23 && Main.tile[i, j].type == 0 && Main.tile[i, j].active) || (type == 2 && Main.tile[i, j].type == 0 && Main.tile[i, j].active) || (type == 109 && Main.tile[i, j].type == 0 && Main.tile[i, j].active) || (type == 60 && Main.tile[i, j].type == 59 && Main.tile[i, j].active) || (type == 70 && Main.tile[i, j].type == 59 && Main.tile[i, j].active))
				{
					if (type == 23 && (Main.tile[i, j].type != 0 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 2 && (Main.tile[i, j].type != 0 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 109 && (Main.tile[i, j].type != 0 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 60 && (Main.tile[i, j].type != 59 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 81)
					{
						if (Main.tile[i - 1, j] == null)
						{
							Main.tile[i - 1, j] = new Tile();
						}
						if (Main.tile[i + 1, j] == null)
						{
							Main.tile[i + 1, j] = new Tile();
						}
						if (Main.tile[i, j - 1] == null)
						{
							Main.tile[i, j - 1] = new Tile();
						}
						if (Main.tile[i, j + 1] == null)
						{
							Main.tile[i, j + 1] = new Tile();
						}
						if (Main.tile[i - 1, j].active || Main.tile[i + 1, j].active || Main.tile[i, j - 1].active)
						{
							return false;
						}
						if (!Main.tile[i, j + 1].active || !Main.tileSolid[Main.tile[i, j + 1].type])
						{
							return false;
						}
					}
					if (Main.tile[i, j].liquid > 0)
					{
						if (type == 4)
						{
							if (style != 8)
							{
								return false;
							}
						}
						else if (type == 3 || type == 4 || type == 20 || type == 24 || type == 27 || type == 32 || type == 51 || type == 69 || type == 72)
						{
							return false;
						}
					}
					Main.tile[i, j].frameY = 0;
					Main.tile[i, j].frameX = 0;
					switch (type)
					{
					case 3:
					case 24:
					case 110:
						if (j + 1 >= Main.maxTilesY || !Main.tile[i, j + 1].active || ((Main.tile[i, j + 1].type != 2 || type != 3) && (Main.tile[i, j + 1].type != 23 || type != 24) && (Main.tile[i, j + 1].type != 78 || type != 3) && (Main.tile[i, j + 1].type != 109 || type != 110)))
						{
							break;
						}
						Config.tilesPretend = false;
						if (type == 24 && genRand.Next(13) == 0)
						{
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = 32;
							SquareTileFrame(i, j);
						}
						else if (Main.tile[i, j + 1].type == 78)
						{
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							Main.tile[i, j].frameX = (short)(genRand.Next(2) * 18 + 108);
						}
						else if (Main.tile[i, j].wall == 0 && Main.tile[i, j + 1].wall == 0)
						{
							if (genRand.Next(50) == 0 || (type == 24 && genRand.Next(40) == 0))
							{
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)type;
								Main.tile[i, j].frameX = 144;
							}
							else if (genRand.Next(35) == 0)
							{
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)type;
								Main.tile[i, j].frameX = (short)(genRand.Next(2) * 18 + 108);
							}
							else
							{
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)type;
								Main.tile[i, j].frameX = (short)(genRand.Next(6) * 18);
							}
						}
						break;
					case 61:
						Config.tilesPretend = true;
						if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && Main.tile[i, j + 1].type == 60)
						{
							Config.tilesPretend = false;
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							if (genRand.Next(16) == 0 && (double)j > Main.worldSurface)
							{
								Main.tile[i, j].type = 69;
								SquareTileFrame(i, j);
							}
							else if (genRand.Next(60) == 0 && (double)j > Main.rockLayer)
							{
								Main.tile[i, j].frameX = 144;
							}
							else if (genRand.Next(1000) == 0 && (double)j > Main.rockLayer)
							{
								Main.tile[i, j].frameX = 162;
							}
							else if (genRand.Next(15) == 0)
							{
								Main.tile[i, j].frameX = (short)(genRand.Next(2) * 18 + 108);
							}
							else
							{
								Main.tile[i, j].frameX = (short)(genRand.Next(6) * 18);
							}
						}
						break;
					case 71:
						Config.tilesPretend = true;
						if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && Main.tile[i, j + 1].type == 70)
						{
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							Main.tile[i, j].frameX = (short)(genRand.Next(5) * 18);
						}
						Config.tilesPretend = false;
						break;
					case 129:
						if (SolidTile(i - 1, j) || SolidTile(i + 1, j) || SolidTile(i, j - 1) || SolidTile(i, j + 1))
						{
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							Main.tile[i, j].frameX = (short)(genRand.Next(8) * 18);
							SquareTileFrame(i, j);
						}
						break;
					case 132:
					case 138:
					case 142:
					case 143:
						Place2x2(i, j, type);
						break;
					case 137:
						Main.tile[i, j].active = true;
						Main.tile[i, j].type = (ushort)type;
						if (style == 1)
						{
							Main.tile[i, j].frameX = 18;
						}
						break;
					case 136:
						if (Main.tile[i - 1, j] == null)
						{
							Main.tile[i - 1, j] = new Tile();
						}
						if (Main.tile[i + 1, j] == null)
						{
							Main.tile[i + 1, j] = new Tile();
						}
						if (Main.tile[i, j + 1] == null)
						{
							Main.tile[i, j + 1] = new Tile();
						}
						Config.tilesPretend = true;
						if ((Main.tile[i - 1, j].active && (Main.tileSolid[Main.tile[i - 1, j].type] || Main.tile[i - 1, j].type == 124 || (Main.tile[i - 1, j].type == 5 && Main.tile[i - 1, j - 1].type == 5 && Main.tile[i - 1, j + 1].type == 5))) || (Main.tile[i + 1, j].active && (Main.tileSolid[Main.tile[i + 1, j].type] || Main.tile[i + 1, j].type == 124 || (Main.tile[i + 1, j].type == 5 && Main.tile[i + 1, j - 1].type == 5 && Main.tile[i + 1, j + 1].type == 5))) || (Main.tile[i, j + 1].active && Main.tileSolid[Main.tile[i, j + 1].type]))
						{
							Config.tilesPretend = false;
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							SquareTileFrame(i, j);
						}
						Config.tilesPretend = false;
						break;
					case 4:
						if (Main.tile[i - 1, j] == null)
						{
							Main.tile[i - 1, j] = new Tile();
						}
						if (Main.tile[i + 1, j] == null)
						{
							Main.tile[i + 1, j] = new Tile();
						}
						if (Main.tile[i, j + 1] == null)
						{
							Main.tile[i, j + 1] = new Tile();
						}
						Config.tilesPretend = true;
						if ((Main.tile[i - 1, j].active && (Main.tileSolid[Main.tile[i - 1, j].type] || Main.tile[i - 1, j].type == 124 || (Main.tile[i - 1, j].type == 5 && Main.tile[i - 1, j - 1].type == 5 && Main.tile[i - 1, j + 1].type == 5))) || (Main.tile[i + 1, j].active && (Main.tileSolid[Main.tile[i + 1, j].type] || Main.tile[i + 1, j].type == 124 || (Main.tile[i + 1, j].type == 5 && Main.tile[i + 1, j - 1].type == 5 && Main.tile[i + 1, j + 1].type == 5))) || (Main.tile[i, j + 1].active && Main.tileSolid[Main.tile[i, j + 1].type]))
						{
							Config.tilesPretend = false;
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							Main.tile[i, j].frameY = (short)(22 * style);
							SquareTileFrame(i, j);
						}
						Config.tilesPretend = false;
						break;
					case 10:
						if (Main.tile[i, j - 1] == null)
						{
							Main.tile[i, j - 1] = new Tile();
						}
						if (Main.tile[i, j - 2] == null)
						{
							Main.tile[i, j - 2] = new Tile();
						}
						if (Main.tile[i, j - 3] == null)
						{
							Main.tile[i, j - 3] = new Tile();
						}
						if (Main.tile[i, j + 1] == null)
						{
							Main.tile[i, j + 1] = new Tile();
						}
						if (Main.tile[i, j + 2] == null)
						{
							Main.tile[i, j + 2] = new Tile();
						}
						if (Main.tile[i, j + 3] == null)
						{
							Main.tile[i, j + 3] = new Tile();
						}
						Config.tilesPretend = true;
						if (!Main.tile[i, j - 1].active && !Main.tile[i, j - 2].active && Main.tile[i, j - 3].active && Main.tileSolid[Main.tile[i, j - 3].type])
						{
							Config.tilesPretend = false;
							PlaceDoor(i, j - 1, type);
							SquareTileFrame(i, j);
						}
						else
						{
							if (Main.tile[i, j + 1].active || Main.tile[i, j + 2].active || !Main.tile[i, j + 3].active || !Main.tileSolid[Main.tile[i, j + 3].type])
							{
								Config.tilesPretend = false;
								return false;
							}
							Config.tilesPretend = false;
							PlaceDoor(i, j + 1, type);
							SquareTileFrame(i, j);
						}
						Config.tilesPretend = false;
						break;
					case 128:
						PlaceMan(i, j, style);
						SquareTileFrame(i, j);
						break;
					case 149:
						if (SolidTile(i - 1, j) || SolidTile(i + 1, j) || SolidTile(i, j - 1) || SolidTile(i, j + 1))
						{
							Main.tile[i, j].frameX = (short)(18 * style);
							Main.tile[i, j].active = true;
							Main.tile[i, j].type = (ushort)type;
							SquareTileFrame(i, j);
						}
						break;
					case 139:
						PlaceMB(i, j, type, style);
						SquareTileFrame(i, j);
						break;
					case 34:
					case 35:
					case 36:
					case 106:
						Place3x3(i, j, type);
						SquareTileFrame(i, j);
						break;
					case 13:
					case 33:
					case 49:
					case 50:
					case 78:
						PlaceOnTable1x1(i, j, type, style);
						SquareTileFrame(i, j);
						break;
					case 14:
					case 26:
					case 86:
					case 87:
					case 88:
					case 89:
					case 114:
						Place3x2(i, j, type);
						SquareTileFrame(i, j);
						break;
					default:
						Config.tilesPretend = false;
						switch (type)
						{
						case 20:
							if (Main.tile[i, j + 1] == null)
							{
								Main.tile[i, j + 1] = new Tile();
							}
							Config.tilesPretend = true;
							if (Main.tile[i, j + 1].active && (Main.tile[i, j + 1].type == 2 || Main.tile[i, j + 1].type == 109 || Main.tile[i, j + 1].type == 147))
							{
								Config.tilesPretend = false;
								Place1x2(i, j, type, style);
								SquareTileFrame(i, j);
							}
							Config.tilesPretend = false;
							break;
						case 15:
							if (Main.tile[i, j - 1] == null)
							{
								Main.tile[i, j - 1] = new Tile();
							}
							if (Main.tile[i, j] == null)
							{
								Main.tile[i, j] = new Tile();
							}
							Place1x2(i, j, type, style);
							SquareTileFrame(i, j);
							break;
						case 16:
						case 18:
						case 29:
						case 103:
						case 134:
							Place2x1(i, j, type);
							SquareTileFrame(i, j);
							break;
						case 92:
						case 93:
							Place1xX(i, j, type);
							SquareTileFrame(i, j);
							break;
						case 104:
						case 105:
							Place2xX(i, j, type, style);
							SquareTileFrame(i, j);
							break;
						case 17:
						case 77:
						case 133:
							Place3x2(i, j, type);
							SquareTileFrame(i, j);
							break;
						case 21:
							PlaceChest(i, j, type, notNearOtherChests: false, style);
							SquareTileFrame(i, j);
							break;
						case 91:
							PlaceBanner(i, j, type, style);
							SquareTileFrame(i, j);
							break;
						case 135:
						case 141:
						case 144:
							Place1x1(i, j, type, style);
							SquareTileFrame(i, j);
							break;
						case 101:
						case 102:
							Place3x4(i, j, type);
							SquareTileFrame(i, j);
							break;
						case 27:
							PlaceSunflower(i, j);
							SquareTileFrame(i, j);
							break;
						case 28:
							PlacePot(i, j);
							SquareTileFrame(i, j);
							break;
						case 42:
							Place1x2Top(i, j, type);
							SquareTileFrame(i, j);
							break;
						case 55:
						case 85:
							PlaceSign(i, j, type);
							break;
						default:
							if (Main.tileAlch[type])
							{
								PlaceAlch(i, j, style);
								break;
							}
							switch (type)
							{
							case 94:
							case 95:
							case 96:
							case 97:
							case 98:
							case 99:
							case 100:
							case 125:
							case 126:
								Place2x2(i, j, type);
								break;
							case 79:
							case 90:
							{
								int direction = 1;
								if (plr > -1)
								{
									direction = Main.player[plr].direction;
								}
								Place4x2(i, j, type, direction);
								break;
							}
							case 81:
								Main.tile[i, j].frameX = (short)(26 * genRand.Next(6));
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)type;
								break;
							default:
								if (type >= 150)
								{
									result = Config.PlaceTile(i, j, Config.tileDefs.width[type], Config.tileDefs.height[type], type, plr);
									break;
								}
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = (ushort)type;
								break;
							}
							break;
						}
						break;
					}
					if (Main.tile[i, j].active && !mute)
					{
						Vector2 vector = default(Vector2);
						vector.X = i;
						vector.Y = j;
						if (type < 150)
						{
							Codable.RunTileMethod(false, vector, Main.tile[i, j].type, "PlaceTile", i, j, plr);
						}
						SquareTileFrame(i, j);
						result = true;
						if (type == 127)
						{
							Main.PlaySound(2, i * 16, j * 16, 30);
						}
						else
						{
							Main.PlaySound(0, i * 16, j * 16);
						}
						if (type == 22 || type == 140)
						{
							vector *= 16f;
							for (int k = 0; k < 3; k++)
							{
								Dust.NewDust(vector, 16, 16, 14, 0f, 0f, 0, Color.Transparent);
							}
						}
					}
				}
			}
			Config.tilesPretend = false;
			return result;
		}

		public static void UpdateMech()
		{
			for (int num = numMechs - 1; num >= 0; num--)
			{
				mechTime[num]--;
				if (Main.tile[mechX[num], mechY[num]].active && Main.tile[mechX[num], mechY[num]].type == 144)
				{
					if (Main.tile[mechX[num], mechY[num]].frameY == 0)
					{
						mechTime[num] = 0;
					}
					else
					{
						int num2 = Main.tile[mechX[num], mechY[num]].frameX / 18;
						switch (num2)
						{
						case 0:
							num2 = 60;
							break;
						case 1:
							num2 = 180;
							break;
						case 2:
							num2 = 300;
							break;
						}
						if (Math.IEEERemainder(mechTime[num], num2) == 0.0)
						{
							mechTime[num] = 18000;
							TripWire(mechX[num], mechY[num]);
						}
					}
				}
				if (mechTime[num] <= 0)
				{
					if (Main.tile[mechX[num], mechY[num]].active && Main.tile[mechX[num], mechY[num]].type == 144)
					{
						Main.tile[mechX[num], mechY[num]].frameY = 0;
						NetMessage.SendTileSquare(-1, mechX[num], mechY[num], 1);
					}
					for (int i = num; i < numMechs; i++)
					{
						mechX[i] = mechX[i + 1];
						mechY[i] = mechY[i + 1];
						mechTime[i] = mechTime[i + 1];
					}
					numMechs--;
				}
			}
		}

		public static bool checkMech(int i, int j, int time)
		{
			for (int k = 0; k < numMechs; k++)
			{
				if (mechX[k] == i && mechY[k] == j)
				{
					return false;
				}
			}
			if (numMechs < maxMech - 1)
			{
				mechX[numMechs] = i;
				mechY[numMechs] = j;
				mechTime[numMechs] = time;
				numMechs++;
				return true;
			}
			return false;
		}

		public static void hitSwitch(int i, int j)
		{
			if (Main.tile[i, j] == null)
			{
				return;
			}
			if (Main.tile[i, j].type == 135)
			{
				Main.PlaySound(28, i * 16, j * 16, 0);
				TripWire(i, j);
			}
			else if (Main.tile[i, j].type == 136)
			{
				if (Main.tile[i, j].frameY == 0)
				{
					Main.tile[i, j].frameY = 18;
				}
				else
				{
					Main.tile[i, j].frameY = 0;
				}
				Main.PlaySound(28, i * 16, j * 16, 0);
				TripWire(i, j);
			}
			else if (Main.tile[i, j].type == 144)
			{
				if (Main.tile[i, j].frameY == 0)
				{
					Main.tile[i, j].frameY = 18;
					if (Main.netMode != 1)
					{
						checkMech(i, j, 18000);
					}
				}
				else
				{
					Main.tile[i, j].frameY = 0;
				}
				Main.PlaySound(28, i * 16, j * 16, 0);
			}
			else
			{
				if (Main.tile[i, j].type != 132)
				{
					return;
				}
				int num = Main.tile[i, j].frameX / -18;
				short num2 = 36;
				int num3 = Main.tile[i, j].frameY / -18;
				if (num < -1)
				{
					num += 2;
					num2 = -36;
				}
				num += i;
				num3 += j;
				for (int k = num; k < num + 2; k++)
				{
					for (int l = num3; l < num3 + 2; l++)
					{
						if (Main.tile[k, l].type == 132)
						{
							Main.tile[k, l].frameX += num2;
						}
					}
				}
				TileFrame(num, num3);
				Main.PlaySound(28, i * 16, j * 16, 0);
				for (int m = num; m < num + 2; m++)
				{
					for (int n = num3; n < num3 + 2; n++)
					{
						if (Main.tile[m, n].type == 132 && Main.tile[m, n].active && Main.tile[m, n].wire)
						{
							TripWire(m, n);
							return;
						}
					}
				}
			}
		}

		public static void TripWire(int i, int j)
		{
			if (Main.netMode != 1)
			{
				numWire = 0;
				numNoWire = 0;
				numInPump = 0;
				numOutPump = 0;
				noWire(i, j);
				hitWire(i, j);
				if (numInPump > 0 && numOutPump > 0)
				{
					xferWater();
				}
			}
		}

		public static void xferWater()
		{
			for (int i = 0; i < numInPump; i++)
			{
				int num = inPumpX[i];
				int num2 = inPumpY[i];
				int liquid = Main.tile[num, num2].liquid;
				if (liquid <= 0)
				{
					continue;
				}
				bool lava = Main.tile[num, num2].lava;
				for (int j = 0; j < numOutPump; j++)
				{
					int num3 = outPumpX[j];
					int num4 = outPumpY[j];
					int liquid2 = Main.tile[num3, num4].liquid;
					if (liquid2 >= 255)
					{
						continue;
					}
					bool flag = Main.tile[num3, num4].lava;
					if (liquid2 == 0)
					{
						flag = lava;
					}
					if (lava == flag)
					{
						int num5 = liquid;
						if (num5 + liquid2 > 255)
						{
							num5 = 255 - liquid2;
						}
						Main.tile[num3, num4].liquid += (byte)num5;
						Main.tile[num, num2].liquid -= (byte)num5;
						liquid = Main.tile[num, num2].liquid;
						Main.tile[num3, num4].lava = lava;
						SquareTileFrame(num3, num4);
						if (Main.tile[num, num2].liquid == 0)
						{
							Main.tile[num, num2].lava = false;
							SquareTileFrame(num, num2);
							break;
						}
					}
				}
				SquareTileFrame(num, num2);
			}
		}

		public static void noWire(int i, int j)
		{
			if (numNoWire < maxWire - 1)
			{
				noWireX[numNoWire] = i;
				noWireY[numNoWire] = j;
				numNoWire++;
			}
		}

		public static void hitWire(int i, int j)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "PreHitWire", i, j) && !(bool)Codable.customMethodReturn) || numWire >= maxWire - 1 || !Main.tile[i, j].wire)
			{
				return;
			}
			for (int k = 0; k < numWire; k++)
			{
				if (wireX[k] == i && wireY[k] == j)
				{
					return;
				}
			}
			wireX[numWire] = i;
			wireY[numWire] = j;
			numWire++;
			Config.tilesPretend = true;
			int type = Main.tile[i, j].type;
			Config.tilesPretend = false;
			int type2 = Main.tile[i, j].type;
			bool flag = true;
			for (int l = 0; l < numNoWire; l++)
			{
				if (noWireX[l] == i && noWireY[l] == j)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[i, j].active)
			{
				Vector2 pos = default(Vector2);
				pos.X = i;
				pos.Y = j;
				if (!Codable.RunTileMethod(false, pos, type2, "hitWire", i, j))
				{
					if (Config.tileDefs.mechToggle.ContainsKey(type2))
					{
						Main.tile[i, j].type = (ushort)Config.tileDefs.ID[Config.tileDefs.mechToggle[type2]];
						SquareTileFrame(i, j);
						NetMessage.SendTileSquare(-1, i, j, 1);
					}
					else if (Config.tileDefs.doorToggle.ContainsKey(type2))
					{
						int num = 1;
						if (Main.rand.Next(2) == 0)
						{
							num = -1;
						}
						if (Config.tileDefs.doorType[type2] == 1)
						{
							if (!Config.OpenCustomDoor(i, j, num, Config.tileDefs.doorToggle[type]))
							{
								num = -num;
								if (Config.OpenCustomDoor(i, j, num, Config.tileDefs.doorToggle[type]))
								{
									NetMessage.SendData(19, -1, -1, "", 0, i, j, num);
								}
							}
							else
							{
								NetMessage.SendData(19, -1, -1, "", 0, i, j, num);
							}
						}
						else if (Config.tileDefs.doorType[type2] == 2 && Config.CloseCustomDoor(i, j, Config.tileDefs.doorToggle[type], forced: true))
						{
							NetMessage.SendData(19, -1, -1, "", 1, i, j);
						}
					}
					else
					{
						switch (type)
						{
						case 144:
							hitSwitch(i, j);
							SquareTileFrame(i, j);
							NetMessage.SendTileSquare(-1, i, j, 1);
							break;
						case 130:
							Main.tile[i, j].type = 131;
							SquareTileFrame(i, j);
							NetMessage.SendTileSquare(-1, i, j, 1);
							break;
						case 131:
							Main.tile[i, j].type = 130;
							SquareTileFrame(i, j);
							NetMessage.SendTileSquare(-1, i, j, 1);
							break;
						case 11:
							if (CloseDoor(i, j, forced: true))
							{
								NetMessage.SendData(19, -1, -1, "", 1, i, j);
							}
							break;
						case 10:
						{
							int num31 = 1;
							if (Main.rand.Next(2) == 0)
							{
								num31 = -1;
							}
							if (!OpenDoor(i, j, num31))
							{
								if (OpenDoor(i, j, -num31))
								{
									NetMessage.SendData(19, -1, -1, "", 0, i, j, 0f - (float)num31);
								}
							}
							else
							{
								NetMessage.SendData(19, -1, -1, "", 0, i, j, num31);
							}
							break;
						}
						case 4:
							if (Main.tile[i, j].frameX < 66)
							{
								Main.tile[i, j].frameX += 66;
							}
							else
							{
								Main.tile[i, j].frameX -= 66;
							}
							NetMessage.SendTileSquare(-1, i, j, 1);
							break;
						case 149:
							if (Main.tile[i, j].frameX < 54)
							{
								Main.tile[i, j].frameX += 54;
							}
							else
							{
								Main.tile[i, j].frameX -= 54;
							}
							NetMessage.SendTileSquare(-1, i, j, 1);
							break;
						case 42:
						{
							int num35 = j - Main.tile[i, j].frameY / 18;
							short num36 = 18;
							if (Main.tile[i, j].frameX > 0)
							{
								num36 = -18;
							}
							Main.tile[i, num35].frameX += num36;
							Main.tile[i, num35 + 1].frameX += num36;
							noWire(i, num35);
							noWire(i, num35 + 1);
							NetMessage.SendTileSquare(-1, i, j, 2);
							break;
						}
						case 93:
						{
							int num32 = j - Main.tile[i, j].frameY / 18;
							short num33 = 18;
							if (Main.tile[i, j].frameX > 0)
							{
								num33 = -18;
							}
							Main.tile[i, num32].frameX += num33;
							Main.tile[i, num32 + 1].frameX += num33;
							Main.tile[i, num32 + 2].frameX += num33;
							noWire(i, num32);
							noWire(i, num32 + 1);
							noWire(i, num32 + 2);
							NetMessage.SendTileSquare(-1, i, num32 + 1, 3);
							break;
						}
						case 95:
						case 100:
						case 126:
						{
							int num14 = j - Main.tile[i, j].frameY / 18;
							int num15 = Main.tile[i, j].frameX / 18;
							if (num15 > 1)
							{
								num15 -= 2;
							}
							num15 = i - num15;
							short num16 = 36;
							if (Main.tile[num15, num14].frameX > 0)
							{
								num16 = -36;
							}
							Main.tile[num15, num14].frameX += num16;
							Main.tile[num15, num14 + 1].frameX += num16;
							Main.tile[num15 + 1, num14].frameX += num16;
							Main.tile[num15 + 1, num14 + 1].frameX += num16;
							noWire(num15, num14);
							noWire(num15, num14 + 1);
							noWire(num15 + 1, num14);
							noWire(num15 + 1, num14 + 1);
							NetMessage.SendTileSquare(-1, num15, num14, 3);
							break;
						}
						case 34:
						case 35:
						case 36:
						{
							int num26 = j - Main.tile[i, j].frameY / 18;
							int num27 = Main.tile[i, j].frameX / 18;
							if (num27 > 2)
							{
								num27 -= 3;
							}
							num27 = i - num27;
							short num28 = 54;
							if (Main.tile[num27, num26].frameX > 0)
							{
								num28 = -54;
							}
							for (int num29 = num27; num29 < num27 + 3; num29++)
							{
								for (int num30 = num26; num30 < num26 + 3; num30++)
								{
									Main.tile[num29, num30].frameX += num28;
									noWire(num29, num30);
								}
							}
							NetMessage.SendTileSquare(-1, num27 + 1, num26 + 1, 3);
							break;
						}
						case 33:
						{
							short num17 = 18;
							if (Main.tile[i, j].frameX > 0)
							{
								num17 = -18;
							}
							Main.tile[i, j].frameX += num17;
							NetMessage.SendTileSquare(-1, i, j, 3);
							break;
						}
						case 92:
						{
							int num12 = j - Main.tile[i, j].frameY / 18;
							short num13 = 18;
							if (Main.tile[i, j].frameX > 0)
							{
								num13 = -18;
							}
							Main.tile[i, num12].frameX += num13;
							Main.tile[i, num12 + 1].frameX += num13;
							Main.tile[i, num12 + 2].frameX += num13;
							Main.tile[i, num12 + 3].frameX += num13;
							Main.tile[i, num12 + 4].frameX += num13;
							Main.tile[i, num12 + 5].frameX += num13;
							noWire(i, num12);
							noWire(i, num12 + 1);
							noWire(i, num12 + 2);
							noWire(i, num12 + 3);
							noWire(i, num12 + 4);
							noWire(i, num12 + 5);
							NetMessage.SendTileSquare(-1, i, num12 + 3, 7);
							break;
						}
						case 137:
							if (checkMech(i, j, 180))
							{
								int num34 = -1;
								if (Main.tile[i, j].frameX != 0)
								{
									num34 = 1;
								}
								float speedX = 12 * num34;
								int damage = 20;
								int type3 = 98;
								pos.X = i * 16 + 8 + 10 * num34;
								pos.Y = j * 16 + 9;
								Projectile.NewProjectile((int)pos.X, (int)pos.Y, speedX, 0f, type3, damage, 2f, Main.myPlayer);
							}
							break;
						case 139:
							SwitchMB(i, j);
							break;
						case 141:
							KillTile(i, j, fail: false, effectOnly: false, noItem: true);
							NetMessage.SendTileSquare(-1, i, j, 1);
							Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 0f, 108, 250, 10f, Main.myPlayer);
							break;
						case 142:
						case 143:
						{
							int num18 = j - Main.tile[i, j].frameY / 18;
							int num19 = Main.tile[i, j].frameX / 18;
							if (num19 > 1)
							{
								num19 -= 2;
							}
							num19 = i - num19;
							noWire(num19, num18);
							noWire(num19, num18 + 1);
							noWire(num19 + 1, num18);
							noWire(num19 + 1, num18 + 1);
							if (type == 142)
							{
								int num20 = num19;
								int num21 = num18;
								for (int num22 = 0; num22 < 4; num22++)
								{
									if (numInPump >= maxPump - 1)
									{
										break;
									}
									switch (num22)
									{
									case 0:
										num20 = num19;
										num21 = num18 + 1;
										break;
									case 1:
										num20 = num19 + 1;
										num21 = num18 + 1;
										break;
									case 2:
										num20 = num19;
										num21 = num18;
										break;
									default:
										num20 = num19 + 1;
										num21 = num18;
										break;
									}
									inPumpX[numInPump] = num20;
									inPumpY[numInPump] = num21;
									numInPump++;
								}
								break;
							}
							int num23 = num19;
							int num24 = num18;
							for (int num25 = 0; num25 < 4; num25++)
							{
								if (numOutPump >= maxPump - 1)
								{
									break;
								}
								switch (num25)
								{
								case 0:
									num23 = num19;
									num24 = num18 + 1;
									break;
								case 1:
									num23 = num19 + 1;
									num24 = num18 + 1;
									break;
								case 2:
									num23 = num19;
									num24 = num18;
									break;
								default:
									num23 = num19 + 1;
									num24 = num18;
									break;
								}
								outPumpX[numOutPump] = num23;
								outPumpY[numOutPump] = num24;
								numOutPump++;
							}
							break;
						}
						case 105:
						{
							int num2 = j - Main.tile[i, j].frameY / 18;
							int num3 = Main.tile[i, j].frameX / 18;
							int num4 = 0;
							while (num3 >= 2)
							{
								num3 -= 2;
								num4++;
							}
							num3 = i - num3;
							noWire(num3, num2);
							noWire(num3, num2 + 1);
							noWire(num3, num2 + 2);
							noWire(num3 + 1, num2);
							noWire(num3 + 1, num2 + 1);
							noWire(num3 + 1, num2 + 2);
							int num5 = num3 * 16 + 16;
							int num6 = (num2 + 3) * 16;
							int num7 = -1;
							switch (num4)
							{
							case 4:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 1))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 1);
								}
								break;
							case 7:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 49))
								{
									num7 = NPC.NewNPC(num5 - 4, num6 - 6, 49);
								}
								break;
							case 8:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 55))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 55);
								}
								break;
							case 9:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 46))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 46);
								}
								break;
							case 10:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 21))
								{
									num7 = NPC.NewNPC(num5, num6, 21);
								}
								break;
							case 18:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 67))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 67);
								}
								break;
							case 23:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 63))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 63);
								}
								break;
							case 27:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 85))
								{
									num7 = NPC.NewNPC(num5 - 9, num6, 85);
								}
								break;
							case 28:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 74))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 74);
								}
								break;
							case 42:
								if (checkMech(i, j, 30) && NPC.MechSpawn(num5, num6, 58))
								{
									num7 = NPC.NewNPC(num5, num6 - 12, 58);
								}
								break;
							case 37:
								if (checkMech(i, j, 600) && Item.MechSpawn(num5, num6, 58))
								{
									Item.NewItem(num5, num6 - 16, 0, 0, 58);
								}
								break;
							case 2:
								if (checkMech(i, j, 600) && Item.MechSpawn(num5, num6, 184))
								{
									Item.NewItem(num5, num6 - 16, 0, 0, 184);
								}
								break;
							case 17:
								if (checkMech(i, j, 600) && Item.MechSpawn(num5, num6, 166))
								{
									Item.NewItem(num5, num6 - 20, 0, 0, 166);
								}
								break;
							case 40:
							{
								if (!checkMech(i, j, 300))
								{
									break;
								}
								int[] array2 = new int[10];
								int num10 = 0;
								for (int n = 0; n < 200; n++)
								{
									if (Main.npc[n].active && (Main.npc[n].type == 17 || Main.npc[n].type == 19 || Main.npc[n].type == 22 || Main.npc[n].type == 38 || Main.npc[n].type == 54 || Main.npc[n].type == 107 || Main.npc[n].type == 108))
									{
										array2[num10] = n;
										num10++;
										if (num10 >= 9)
										{
											break;
										}
									}
								}
								if (num10 > 0)
								{
									int num11 = array2[Main.rand.Next(num10)];
									Main.npc[num11].position.X = (float)num5 - (float)Main.npc[num11].width * 0.5f;
									Main.npc[num11].position.Y = num6 - Main.npc[num11].height - 1;
									NetMessage.SendData(23, -1, -1, "", num11);
								}
								break;
							}
							case 41:
							{
								if (!checkMech(i, j, 300))
								{
									break;
								}
								int[] array = new int[10];
								int num8 = 0;
								for (int m = 0; m < 200; m++)
								{
									if (Main.npc[m].active && (Main.npc[m].type == 18 || Main.npc[m].type == 20 || Main.npc[m].type == 124))
									{
										array[num8] = m;
										num8++;
										if (num8 >= 9)
										{
											break;
										}
									}
								}
								if (num8 > 0)
								{
									int num9 = array[Main.rand.Next(num8)];
									Main.npc[num9].position.X = num5 - Main.npc[num9].width / 2;
									Main.npc[num9].position.Y = num6 - Main.npc[num9].height - 1;
									NetMessage.SendData(23, -1, -1, "", num9);
								}
								break;
							}
							}
							if (num7 >= 0)
							{
								Main.npc[num7].value = 0f;
								Main.npc[num7].npcSlots = 0f;
							}
							break;
						}
						}
					}
				}
			}
			hitWire(i - 1, j);
			hitWire(i + 1, j);
			hitWire(i, j - 1);
			hitWire(i, j + 1);
		}

		public static void KillWall(int i, int j, bool fail = false)
		{
			if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
			{
				return;
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			ushort wall = Main.tile[i, j].wall;
			if (wall <= 0)
			{
				return;
			}
			if (wall == 21)
			{
				Main.PlaySound(13, i * 16, j * 16);
			}
			else
			{
				Main.PlaySound(0, i * 16, j * 16);
			}
			int num = 10;
			if (fail)
			{
				num = 3;
			}
			Vector2 position = default(Vector2);
			position.X = i * 16;
			position.Y = j * 16;
			for (int k = 0; k < num; k++)
			{
				int type = 0;
				if (wall == 1 || wall == 5 || wall == 6 || wall == 7 || wall == 8 || wall == 9)
				{
					type = 1;
				}
				switch (wall)
				{
				case 3:
					type = ((genRand.Next(2) != 0) ? 1 : 14);
					break;
				case 4:
					type = 7;
					break;
				case 10:
					type = 10;
					break;
				case 11:
					type = 11;
					break;
				case 12:
					type = 9;
					break;
				case 21:
					type = 13;
					break;
				case 22:
				case 28:
					type = 51;
					break;
				case 23:
					type = 38;
					break;
				case 24:
					type = 36;
					break;
				case 25:
					type = 48;
					break;
				case 26:
				case 30:
					type = 49;
					break;
				case 27:
					type = ((genRand.Next(2) != 0) ? 1 : 7);
					break;
				case 29:
					type = 50;
					break;
				case 31:
					type = 51;
					break;
				}
				Dust.NewDust(position, 16, 16, type, 0f, 0f, 0, Color.Transparent);
			}
			if (fail)
			{
				SquareWallFrame(i, j);
				return;
			}
			int num2 = 0;
			if (Config.wallDefs.dropName.TryGetValue(wall, out string val))
			{
				num2 = Config.itemDefs.byName[val].type;
			}
			else
			{
				switch (wall)
				{
				case 1:
					num2 = 26;
					break;
				case 4:
					num2 = 93;
					break;
				case 5:
					num2 = 130;
					break;
				case 6:
					num2 = 132;
					break;
				case 7:
					num2 = 135;
					break;
				case 8:
					num2 = 138;
					break;
				case 9:
					num2 = 140;
					break;
				case 10:
					num2 = 142;
					break;
				case 11:
					num2 = 144;
					break;
				case 12:
					num2 = 146;
					break;
				case 14:
					num2 = 330;
					break;
				case 16:
					num2 = 30;
					break;
				case 17:
					num2 = 135;
					break;
				case 18:
					num2 = 138;
					break;
				case 19:
					num2 = 140;
					break;
				case 20:
					num2 = 330;
					break;
				case 21:
					num2 = 392;
					break;
				case 22:
					num2 = 417;
					break;
				case 23:
					num2 = 418;
					break;
				case 24:
					num2 = 419;
					break;
				case 25:
					num2 = 420;
					break;
				case 26:
					num2 = 421;
					break;
				case 27:
					num2 = 479;
					break;
				case 29:
					num2 = 587;
					break;
				case 30:
					num2 = 592;
					break;
				case 31:
					num2 = 595;
					break;
				}
			}
			if (num2 > 0)
			{
				Item.NewItem(i * 16, j * 16, 16, 16, num2);
			}
			Main.tile[i, j].wall = 0;
			SquareWallFrame(i, j);
		}

		public static void KillTile(int i, int j, bool fail = false, bool effectOnly = false, bool noItem = false, Player player = null)
		{
			if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
			{
				return;
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			if (!Main.tile[i, j].active)
			{
				return;
			}
			Tile tile = Main.tile[i, j];
			if (j >= 1 && Main.tile[i, j - 1] == null)
			{
				Main.tile[i, j - 1] = new Tile();
			}
			Tile tile2 = Main.tile[i, j - 1];
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			bool flag = true;
			if (Codable.RunTileMethodRef(false, new Vector2(i, j), tile.type, "PreKillTile", i, j, flag, fail, effectOnly, noItem, player) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn = Codable.customMethodRefReturn;
				i = (int)customMethodRefReturn[0];
				j = (int)customMethodRefReturn[1];
				flag = (bool)customMethodRefReturn[2];
				fail = (bool)customMethodRefReturn[3];
				effectOnly = (bool)customMethodRefReturn[4];
				noItem = (bool)customMethodRefReturn[5];
			}
			if (!flag)
			{
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "PreKillTile", player);
				return;
			}
			if (j >= 1 && tile2.active && ((tile2.type == 5 && tile.type != 5) || (tile2.type == 21 && tile.type != 21) || (tile2.type == 26 && tile.type != 26) || (tile2.type == 72 && tile.type != 72) || (tile2.type == 12 && tile.type != 12)) && (tile2.type != 5 || ((tile2.frameX != 66 || tile2.frameY < 0 || tile2.frameY > 44) && (tile2.frameX != 88 || tile2.frameY < 66 || tile2.frameY > 110) && tile2.frameY < 198)))
			{
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "Immune Top", player);
				return;
			}
			List<Vector2> list = new List<Vector2>();
			Vector2 vector2 = new Vector2(i, j);
			Vector2 vector3 = vector2;
			vector3 = ((Config.tileDefs.height[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1 && Config.tileDefs.width[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1) ? vector2 : Codable.GetPos(vector2));
			if (!list.Contains(vector3))
			{
				if (Codable.RunTileMethod(false, vector2, tile.type, "CanDestroyTile", (int)vector2.X, (int)vector2.Y) && !(bool)Codable.customMethodReturn)
				{
					Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "CanDestroyTile", player);
					return;
				}
				list.Add(vector3);
			}
			vector2 = new Vector2(i, j - 1);
			if (Main.tile[(int)vector2.X, (int)vector2.Y] == null)
			{
				Main.tile[(int)vector2.X, (int)vector2.Y] = new Tile();
			}
			vector3 = ((Config.tileDefs.height[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1 && Config.tileDefs.width[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1) ? vector2 : Codable.GetPos(vector2));
			if (!list.Contains(vector3))
			{
				if (Codable.RunTileMethod(false, vector2, Main.tile[(int)vector2.X, (int)vector2.Y].type, "CanDestroyAround", (int)vector2.X, (int)vector2.Y, "Bottom") && !(bool)Codable.customMethodReturn)
				{
					Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "CanDestroyAround", player);
					return;
				}
				list.Add(vector3);
			}
			vector2 = new Vector2(i, j + 1);
			if (Main.tile[(int)vector2.X, (int)vector2.Y] == null)
			{
				Main.tile[(int)vector2.X, (int)vector2.Y] = new Tile();
			}
			vector3 = ((Config.tileDefs.height[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1 && Config.tileDefs.width[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1) ? vector2 : Codable.GetPos(vector2));
			if (!list.Contains(vector3))
			{
				if (Codable.RunTileMethod(false, vector2, Main.tile[(int)vector2.X, (int)vector2.Y].type, "CanDestroyAround", (int)vector2.X, (int)vector2.Y, "Top") && !(bool)Codable.customMethodReturn)
				{
					Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "CanDestroyAround", player);
					return;
				}
				list.Add(vector3);
			}
			vector2 = new Vector2(i - 1, j);
			if (Main.tile[(int)vector2.X, (int)vector2.Y] == null)
			{
				Main.tile[(int)vector2.X, (int)vector2.Y] = new Tile();
			}
			vector3 = ((Config.tileDefs.height[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1 && Config.tileDefs.width[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1) ? vector2 : Codable.GetPos(vector2));
			if (!list.Contains(vector3))
			{
				if (Codable.RunTileMethod(false, vector2, Main.tile[(int)vector2.X, (int)vector2.Y].type, "CanDestroyAround", (int)vector2.X, (int)vector2.Y, "Right") && !(bool)Codable.customMethodReturn)
				{
					Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "CanDestroyAround", player);
					return;
				}
				list.Add(vector3);
			}
			vector2 = new Vector2(i + 1, j);
			if (Main.tile[(int)vector2.X, (int)vector2.Y] == null)
			{
				Main.tile[(int)vector2.X, (int)vector2.Y] = new Tile();
			}
			vector3 = ((Config.tileDefs.height[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1 && Config.tileDefs.width[Main.tile[(int)vector2.X, (int)vector2.Y].type] <= 1) ? vector2 : Codable.GetPos(vector2));
			if (!list.Contains(vector3))
			{
				if (Codable.RunTileMethod(false, vector2, Main.tile[(int)vector2.X, (int)vector2.Y].type, "CanDestroyAround", (int)vector2.X, (int)vector2.Y, "Left") && !(bool)Codable.customMethodReturn)
				{
					Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "CanDestroyAround", player);
					return;
				}
				list.Add(vector3);
			}
			if (!effectOnly && !stopDrops)
			{
				int value = -1;
				if (Config.tileDefs.hitSound.TryGetValue(tile.type, out value))
				{
					int value2 = 2;
					Config.tileDefs.hitSoundList.TryGetValue(tile.type, out value2);
					Main.PlaySound(value2, i * 16, j * 16, value);
				}
				else if (tile.type == 127)
				{
					Main.PlaySound(2, i * 16, j * 16, 27);
				}
				else if (tile.type == 3 || tile.type == 110)
				{
					Main.PlaySound(6, i * 16, j * 16);
					if (tile.frameX == 144)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 5);
					}
				}
				else if (tile.type == 24)
				{
					Main.PlaySound(6, i * 16, j * 16);
					if (tile.frameX == 144)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 60);
					}
				}
				else if (Main.tileAlch[tile.type] || tile.type == 32 || tile.type == 51 || tile.type == 52 || tile.type == 61 || tile.type == 62 || tile.type == 69 || tile.type == 71 || tile.type == 73 || tile.type == 74 || tile.type == 113 || tile.type == 115)
				{
					Main.PlaySound(6, i * 16, j * 16);
				}
				else if (tile.type == 1 || tile.type == 6 || tile.type == 7 || tile.type == 8 || tile.type == 9 || tile.type == 22 || tile.type == 140 || tile.type == 25 || tile.type == 37 || tile.type == 38 || tile.type == 39 || tile.type == 41 || tile.type == 43 || tile.type == 44 || tile.type == 45 || tile.type == 46 || tile.type == 47 || tile.type == 48 || tile.type == 56 || tile.type == 58 || tile.type == 63 || tile.type == 64 || tile.type == 65 || tile.type == 66 || tile.type == 67 || tile.type == 68 || tile.type == 75 || tile.type == 76 || tile.type == 107 || tile.type == 108 || tile.type == 111 || tile.type == 117 || tile.type == 118 || tile.type == 119 || tile.type == 120 || tile.type == 121 || tile.type == 122)
				{
					Main.PlaySound(21, i * 16, j * 16);
				}
				else if (tile.type != 138)
				{
					Main.PlaySound(0, i * 16, j * 16);
				}
				if (tile.type == 129 && !fail)
				{
					Main.PlaySound(2, i * 16, j * 16, 27);
				}
			}
			int num = 10;
			if (tile.type == 128)
			{
				int num2 = i;
				int frameX = tile.frameX;
				if (tile.frameX % 100 % 36 == 18)
				{
					frameX = Main.tile[i - 1, j].frameX;
					num2--;
				}
				if (frameX >= 100)
				{
					int i2 = frameX / 100;
					switch (Main.tile[num2, j].frameY / 18)
					{
					case 0:
						Item.NewItem(i * 16, j * 16, 16, 16, Item.headType[i2]);
						break;
					case 1:
						Item.NewItem(i * 16, j * 16, 16, 16, Item.bodyType[i2]);
						break;
					case 2:
						Item.NewItem(i * 16, j * 16, 16, 16, Item.legType[i2]);
						break;
					}
					Main.tile[num2, j].frameX %= 100;
				}
			}
			if (fail)
			{
				num = 3;
			}
			if (tile.type == 138)
			{
				num = 0;
			}
			for (int k = 0; k < num; k++)
			{
				int num3 = 0;
				if (tile.type == 0)
				{
					num3 = 0;
				}
				else if (tile.type == 1 || tile.type == 16 || tile.type == 17 || tile.type == 38 || tile.type == 39 || tile.type == 41 || tile.type == 43 || tile.type == 44 || tile.type == 48 || Main.tileStone[tile.type] || tile.type == 85 || tile.type == 90 || tile.type == 92 || tile.type == 96 || tile.type == 97 || tile.type == 99 || tile.type == 105 || tile.type == 117 || tile.type == 130 || tile.type == 131 || tile.type == 132 || tile.type == 135 || tile.type == 135 || tile.type == 137 || tile.type == 142 || tile.type == 143 || tile.type == 144)
				{
					num3 = 1;
				}
				else if (tile.type == 33 || tile.type == 95 || tile.type == 98 || tile.type == 100)
				{
					num3 = 6;
				}
				else if (tile.type == 5 || tile.type == 10 || tile.type == 11 || tile.type == 14 || tile.type == 15 || tile.type == 19 || tile.type == 30 || tile.type == 86 || tile.type == 87 || tile.type == 88 || tile.type == 89 || tile.type == 93 || tile.type == 94 || tile.type == 104 || tile.type == 106 || tile.type == 114 || tile.type == 124 || tile.type == 128 || tile.type == 139)
				{
					num3 = 7;
				}
				else if (tile.type == 21)
				{
					num3 = ((tile.frameX >= 108) ? 37 : ((tile.frameX < 36) ? 7 : 10));
				}
				else if (tile.type == 2)
				{
					num3 = ((genRand.Next(2) != 0) ? 2 : 0);
				}
				else if (tile.type == 127)
				{
					num3 = 67;
				}
				else if (tile.type == 91)
				{
					num3 = -1;
				}
				else if (tile.type == 6 || tile.type == 26)
				{
					num3 = 8;
				}
				else if (tile.type == 7 || tile.type == 34 || tile.type == 47)
				{
					num3 = 9;
				}
				else if (tile.type == 8 || tile.type == 36 || tile.type == 45 || tile.type == 102)
				{
					num3 = 10;
				}
				else if (tile.type == 9 || tile.type == 35 || tile.type == 42 || tile.type == 46 || tile.type == 126 || tile.type == 136)
				{
					num3 = 11;
				}
				else if (tile.type == 12)
				{
					num3 = 12;
				}
				else if (tile.type == 3 || tile.type == 73)
				{
					num3 = 3;
				}
				else if (tile.type == 13 || tile.type == 54)
				{
					num3 = 13;
				}
				else if (tile.type == 22 || tile.type == 140)
				{
					num3 = 14;
				}
				else if (tile.type == 28 || tile.type == 78)
				{
					num3 = 22;
				}
				else if (tile.type == 29)
				{
					num3 = 23;
				}
				else if (tile.type == 40 || tile.type == 103)
				{
					num3 = 28;
				}
				else if (tile.type == 49)
				{
					num3 = 29;
				}
				else if (tile.type == 50)
				{
					num3 = 22;
				}
				else if (tile.type == 51)
				{
					num3 = 30;
				}
				else if (tile.type == 52)
				{
					num3 = 3;
				}
				else if (tile.type == 53 || tile.type == 81)
				{
					num3 = 32;
				}
				else if (tile.type == 56 || tile.type == 75)
				{
					num3 = 37;
				}
				else if (tile.type == 57 || tile.type == 119 || tile.type == 141)
				{
					num3 = 36;
				}
				else if (tile.type == 59 || tile.type == 120)
				{
					num3 = 38;
				}
				else if (tile.type == 61 || tile.type == 62 || tile.type == 74 || tile.type == 80)
				{
					num3 = 40;
				}
				else if (tile.type == 69)
				{
					num3 = 7;
				}
				else if (tile.type == 71 || tile.type == 72)
				{
					num3 = 26;
				}
				else if (tile.type == 70)
				{
					num3 = 17;
				}
				else if (tile.type == 112)
				{
					num3 = 14;
				}
				else if (tile.type == 123)
				{
					num3 = 53;
				}
				else if (tile.type == 116 || tile.type == 118 || tile.type == 147 || tile.type == 148)
				{
					num3 = 51;
				}
				else if (tile.type == 109)
				{
					num3 = ((genRand.Next(2) != 0) ? 47 : 0);
				}
				else if (tile.type == 110 || tile.type == 113 || tile.type == 115)
				{
					num3 = 47;
				}
				else if (tile.type == 107 || tile.type == 121)
				{
					num3 = 48;
				}
				else if (tile.type == 108 || tile.type == 122 || tile.type == 134 || tile.type == 146)
				{
					num3 = 49;
				}
				else if (tile.type == 111 || tile.type == 133 || tile.type == 145)
				{
					num3 = 50;
				}
				else if (tile.type == 149)
				{
					num3 = 49;
				}
				if (Main.tileAlch[tile.type])
				{
					int num4 = tile.frameX / 18;
					if (num4 == 0)
					{
						num3 = 3;
					}
					if (num4 == 1)
					{
						num3 = 3;
					}
					if (num4 == 2)
					{
						num3 = 7;
					}
					if (num4 == 3)
					{
						num3 = 17;
					}
					if (num4 == 4)
					{
						num3 = 3;
					}
					if (num4 == 5)
					{
						num3 = 6;
					}
				}
				if (tile.type == 61)
				{
					num3 = ((genRand.Next(2) != 0) ? 39 : 38);
				}
				else if (tile.type == 58 || tile.type == 76 || tile.type == 77)
				{
					num3 = ((genRand.Next(2) != 0) ? 25 : 6);
				}
				else if (tile.type == 37)
				{
					num3 = ((genRand.Next(2) != 0) ? 23 : 6);
				}
				else if (tile.type == 32)
				{
					num3 = ((genRand.Next(2) != 0) ? 24 : 14);
				}
				else if (tile.type == 23 || tile.type == 24)
				{
					num3 = ((genRand.Next(2) != 0) ? 17 : 14);
				}
				else if (tile.type == 25 || tile.type == 31)
				{
					num3 = ((genRand.Next(2) != 0) ? 1 : 14);
				}
				else if (tile.type == 20)
				{
					num3 = ((genRand.Next(2) != 0) ? 2 : 7);
				}
				else if (tile.type == 27)
				{
					num3 = ((genRand.Next(2) != 0) ? 19 : 3);
				}
				else if (tile.type == 129)
				{
					num3 = ((tile.frameX != 0 && tile.frameX != 54 && tile.frameX != 108) ? ((tile.frameX != 18 && tile.frameX != 72 && tile.frameX != 126) ? 70 : 69) : 68);
				}
				else if (tile.type == 4)
				{
					int num5 = tile.frameY / 22;
					switch (num5)
					{
					case 0:
						num3 = 6;
						break;
					case 8:
						num3 = 75;
						break;
					default:
						num3 = 58 + num5;
						break;
					}
				}
				if ((tile.type == 34 || tile.type == 35 || tile.type == 36 || tile.type == 42) && Main.rand.Next(2) == 0)
				{
					num3 = 6;
				}
				if (Config.tileDefs.dustType[tile.type] != 0)
				{
					num3 = Config.tileDefs.dustType[tile.type];
				}
				if (num3 >= 0)
				{
					vector.X = i * 16;
					vector.Y = j * 16;
					Dust.NewDust(vector, 16, 16, num3, 0f, 0f, 0, Color.Transparent);
				}
			}
			if (effectOnly)
			{
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "EffectOnly", player);
				return;
			}
			if (fail)
			{
				if (tile.type == 2 || tile.type == 23 || tile.type == 109)
				{
					tile.type = 0;
				}
				if (tile.type == 60 || tile.type == 70)
				{
					tile.type = 59;
				}
				SquareTileFrame(i, j);
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "Fail", player);
				return;
			}
			if (tile.type == 21 && Main.netMode != 1 && !Chest.DestroyChest(i - tile.frameX / 18 % 2, j - tile.frameY / 18))
			{
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "Unempty Chest", player);
				return;
			}
			if (Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "StopKillTile", i, j) && (bool)Codable.customMethodReturn)
			{
				Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "StopKill", player);
				return;
			}
			if (!noItem && !stopDrops && Main.netMode != 1)
			{
				int num6 = 0;
				Config.tilesPretend = false;
				if (Config.tileDefs.dropName.ContainsKey(tile.type) && Config.tileDefs.width[tile.type] == 1 && Config.tileDefs.height[tile.type] == 1)
				{
					num6 = Config.itemDefs.byName[Config.tileDefs.dropName[tile.type]].type;
				}
				else if (tile.type == 0 || tile.type == 2 || tile.type == 109)
				{
					num6 = 2;
				}
				else if (tile.type == 1)
				{
					num6 = 3;
				}
				else if (tile.type == 3 || tile.type == 73)
				{
					vector.X = i * 16;
					vector.Y = j * 16;
					if (Main.rand.Next(2) == 0 && Main.player[Player.FindClosest(vector, 16, 16)].HasItem(281))
					{
						num6 = 283;
					}
				}
				else if (tile.type == 4)
				{
					int num7 = tile.frameY / 22;
					switch (num7)
					{
					case 0:
						num6 = 8;
						break;
					case 8:
						num6 = 523;
						break;
					default:
						num6 = 426 + num7;
						break;
					}
				}
				else if (tile.type == 5)
				{
					if (tile.frameX >= 22 && tile.frameY >= 198)
					{
						if (Main.netMode != 1)
						{
							if (genRand.Next(2) == 0)
							{
								int l;
								for (l = j; Main.tile[i, l] != null && (!Main.tile[i, l].active || !Main.tileSolid[Main.tile[i, l].type] || Main.tileSolidTop[Main.tile[i, l].type]); l++)
								{
								}
								if (Main.tile[i, l] != null)
								{
									num6 = ((Main.tile[i, l].type != 2 && Main.tile[i, l].type != 109) ? 9 : 27);
								}
							}
							else
							{
								num6 = 9;
							}
						}
					}
					else
					{
						num6 = 9;
					}
				}
				else if (tile.type == 6)
				{
					num6 = 11;
				}
				else if (tile.type == 7)
				{
					num6 = 12;
				}
				else if (tile.type == 8)
				{
					num6 = 13;
				}
				else if (tile.type == 9)
				{
					num6 = 14;
				}
				else if (tile.type == 123)
				{
					num6 = 424;
				}
				else if (tile.type == 124)
				{
					num6 = 480;
				}
				else if (tile.type == 149)
				{
					if (tile.frameX == 0 || tile.frameX == 54)
					{
						num6 = 596;
					}
					else if (tile.frameX == 18 || tile.frameX == 72)
					{
						num6 = 597;
					}
					else if (tile.frameX == 36 || tile.frameX == 90)
					{
						num6 = 598;
					}
				}
				else if (tile.type == 13)
				{
					Main.PlaySound(13, i * 16, j * 16);
					num6 = ((tile.frameX == 18) ? 28 : ((tile.frameX == 36) ? 110 : ((tile.frameX == 54) ? 350 : ((tile.frameX != 72) ? 31 : 351))));
				}
				else if (tile.type == 19)
				{
					num6 = 94;
				}
				else if (tile.type == 22)
				{
					num6 = 56;
				}
				else if (tile.type == 140)
				{
					num6 = 577;
				}
				else if (tile.type == 23)
				{
					num6 = 2;
				}
				else if (tile.type == 25)
				{
					num6 = 61;
				}
				else if (tile.type == 30)
				{
					num6 = 9;
				}
				else if (tile.type == 33)
				{
					num6 = 105;
				}
				else if (tile.type == 37)
				{
					num6 = 116;
				}
				else if (tile.type == 38)
				{
					num6 = 129;
				}
				else if (tile.type == 39)
				{
					num6 = 131;
				}
				else if (tile.type == 40)
				{
					num6 = 133;
				}
				else if (tile.type == 41)
				{
					num6 = 134;
				}
				else if (tile.type == 43)
				{
					num6 = 137;
				}
				else if (tile.type == 44)
				{
					num6 = 139;
				}
				else if (tile.type == 45)
				{
					num6 = 141;
				}
				else if (tile.type == 46)
				{
					num6 = 143;
				}
				else if (tile.type == 47)
				{
					num6 = 145;
				}
				else if (tile.type == 48)
				{
					num6 = 147;
				}
				else if (tile.type == 49)
				{
					num6 = 148;
				}
				else if (tile.type == 51)
				{
					num6 = 150;
				}
				else if (tile.type == 53)
				{
					num6 = 169;
				}
				else if (tile.type == 54)
				{
					num6 = 170;
					Main.PlaySound(13, i * 16, j * 16);
				}
				else if (tile.type == 56)
				{
					num6 = 173;
				}
				else if (tile.type == 57)
				{
					num6 = 172;
				}
				else if (tile.type == 58)
				{
					num6 = 174;
				}
				else if (tile.type == 60)
				{
					num6 = 176;
				}
				else if (tile.type == 70)
				{
					num6 = 176;
				}
				else if (tile.type == 75)
				{
					num6 = 192;
				}
				else if (tile.type == 76)
				{
					num6 = 214;
				}
				else if (tile.type == 78)
				{
					num6 = 222;
				}
				else if (tile.type == 81)
				{
					num6 = 275;
				}
				else if (tile.type == 80)
				{
					num6 = 276;
				}
				else if (tile.type == 107)
				{
					num6 = 364;
				}
				else if (tile.type == 108)
				{
					num6 = 365;
				}
				else if (tile.type == 111)
				{
					num6 = 366;
				}
				else if (tile.type == 112)
				{
					num6 = 370;
				}
				else if (tile.type == 116)
				{
					num6 = 408;
				}
				else if (tile.type == 117)
				{
					num6 = 409;
				}
				else if (tile.type == 129)
				{
					num6 = 502;
				}
				else if (tile.type == 118)
				{
					num6 = 412;
				}
				else if (tile.type == 119)
				{
					num6 = 413;
				}
				else if (tile.type == 120)
				{
					num6 = 414;
				}
				else if (tile.type == 121)
				{
					num6 = 415;
				}
				else if (tile.type == 122)
				{
					num6 = 416;
				}
				else if (tile.type == 136)
				{
					num6 = 538;
				}
				else if (tile.type == 137)
				{
					num6 = 539;
				}
				else if (tile.type == 141)
				{
					num6 = 580;
				}
				else if (tile.type == 145)
				{
					num6 = 586;
				}
				else if (tile.type == 146)
				{
					num6 = 591;
				}
				else if (tile.type == 147)
				{
					num6 = 593;
				}
				else if (tile.type == 148)
				{
					num6 = 594;
				}
				else if (tile.type == 135)
				{
					if (tile.frameY == 0)
					{
						num6 = 529;
					}
					else if (tile.frameY == 18)
					{
						num6 = 541;
					}
					else if (tile.frameY == 36)
					{
						num6 = 542;
					}
					else if (tile.frameY == 54)
					{
						num6 = 543;
					}
				}
				else if (tile.type == 144)
				{
					if (tile.frameX == 0)
					{
						num6 = 583;
					}
					else if (tile.frameX == 18)
					{
						num6 = 584;
					}
					else if (tile.frameX == 36)
					{
						num6 = 585;
					}
				}
				else if (tile.type == 130)
				{
					num6 = 511;
				}
				else if (tile.type == 131)
				{
					num6 = 512;
				}
				else if (tile.type == 61 || tile.type == 74)
				{
					if (tile.frameX == 144)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 331, genRand.Next(2, 4));
					}
					else if (tile.frameX == 162)
					{
						num6 = 223;
					}
					else if (tile.frameX >= 108 && tile.frameX <= 126 && genRand.Next(100) == 0)
					{
						num6 = 208;
					}
					else if (genRand.Next(100) == 0)
					{
						num6 = 195;
					}
				}
				else if (tile.type == 59 || tile.type == 60)
				{
					num6 = 176;
				}
				else if (tile.type == 71 || tile.type == 72)
				{
					if (genRand.Next(50) == 0)
					{
						num6 = 194;
					}
					else if (genRand.Next(2) == 0)
					{
						num6 = 183;
					}
				}
				else if (tile.type >= 63 && tile.type <= 68)
				{
					num6 = tile.type - 63 + 177;
				}
				else if (tile.type == 50)
				{
					num6 = ((tile.frameX != 90) ? 149 : 165);
				}
				else if (Main.tileAlch[tile.type] && tile.type > 82)
				{
					int num8 = tile.frameX / 18;
					bool flag2 = false;
					if (tile.type == 84)
					{
						flag2 = true;
					}
					else if (num8 == 0 && Main.dayTime)
					{
						flag2 = true;
					}
					else if (num8 == 1 && !Main.dayTime)
					{
						flag2 = true;
					}
					else if (num8 == 3 && Main.bloodMoon)
					{
						flag2 = true;
					}
					num6 = 313 + num8;
					if (flag2)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, 307 + num8, genRand.Next(1, 4));
					}
				}
				if (num6 > 0)
				{
					Item.NewItem(i * 16, j * 16, 16, 16, num6, 1, noBroadcast: false, -1);
				}
			}
			vector.X = i;
			vector.Y = j;
			Codable.RunTileMethod(false, vector, tile.type, "KillTile", i, j, player);
			Codable.RunTileMethod(false, new Vector2(i, j), tile.type, "PostKillTile", i, j, "Success", player);
			Codable.DestroyTile(vector);
			tile.active = false;
			tile.frameX = -1;
			tile.frameY = -1;
			tile.frameNumber = 0;
			if (tile.type == 58 && (double)j > Main.hellLayer)
			{
				tile.lava = true;
				tile.liquid = 128;
			}
			tile.type = 0;
			SquareTileFrame(i, j);
		}

		public static bool PlayerLOS(int x, int y)
		{
			Rectangle rectangle = new Rectangle(x * 16, y * 16, 16, 16);
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					Rectangle value = new Rectangle((int)(Main.player[i].position.X + (float)Main.player[i].width * 0.5f - (float)NPC.sWidth * 0.6f), (int)(Main.player[i].position.Y + (float)Main.player[i].height * 0.5f - (float)NPC.sHeight * 0.6f), (int)((float)NPC.sWidth * 1.2f), (int)((float)NPC.sHeight * 1.2f));
					if (rectangle.Intersects(value))
					{
						return true;
					}
				}
			}
			return false;
		}

		public static void hardUpdateWorld(int i, int j)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "hardUpdateWorld", i, j) && !(bool)Codable.customMethodReturn) || !Main.hardMode)
			{
				return;
			}
			int type = Main.tile[i, j].type;
			if (type == 117 && (double)j > Main.rockLayer && Main.rand.Next(110) == 0)
			{
				int num = genRand.Next(4);
				int num2 = 0;
				int num3 = 0;
				switch (num)
				{
				case 0:
					num2 = -1;
					num3 = -1;
					break;
				case 1:
					num2 = 1;
					break;
				default:
					num3 = 1;
					break;
				}
				if (Main.tile[i + num2, j + num3].active)
				{
					return;
				}
				int num4 = 0;
				int num5 = 6;
				for (int k = i - num5; k <= i + num5; k++)
				{
					for (int l = j - num5; l <= j + num5; l++)
					{
						if (Main.tile[k, l].active && Main.tile[k, l].type == 129)
						{
							num4++;
						}
					}
				}
				if (num4 < 2)
				{
					PlaceTile(i + num2, j + num3, 129, mute: true);
					NetMessage.SendTileSquare(-1, i + num2, j + num3, 1);
				}
				return;
			}
			switch (type)
			{
			case 23:
			case 25:
			case 32:
			case 112:
			{
				bool flag2 = true;
				while (flag2)
				{
					flag2 = false;
					int num8 = i + genRand.Next(-3, 4);
					int num9 = j + genRand.Next(-3, 4);
					if (Main.tile[num8, num9].type == 2)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 23;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
					else if (Main.tile[num8, num9].type == 1)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 25;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
					else if (Main.tile[num8, num9].type == 53)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 112;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
					else if (Main.tile[num8, num9].type == 59)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 0;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
					else if (Main.tile[num8, num9].type == 60)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 23;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
					else if (Main.tile[num8, num9].type == 69)
					{
						if (genRand.Next(2) == 0)
						{
							flag2 = true;
						}
						Main.tile[num8, num9].type = 32;
						SquareTileFrame(num8, num9);
						NetMessage.SendTileSquare(-1, num8, num9, 1);
					}
				}
				break;
			}
			case 109:
			case 110:
			case 113:
			case 115:
			case 116:
			case 117:
			case 118:
			{
				bool flag = true;
				while (flag)
				{
					flag = false;
					int num6 = i + genRand.Next(-3, 4);
					int num7 = j + genRand.Next(-3, 4);
					if (Main.tile[num6, num7].type == 2)
					{
						if (genRand.Next(2) == 0)
						{
							flag = true;
						}
						Main.tile[num6, num7].type = 109;
						SquareTileFrame(num6, num7);
						NetMessage.SendTileSquare(-1, num6, num7, 1);
					}
					else if (Main.tile[num6, num7].type == 1)
					{
						if (genRand.Next(2) == 0)
						{
							flag = true;
						}
						Main.tile[num6, num7].type = 117;
						SquareTileFrame(num6, num7);
						NetMessage.SendTileSquare(-1, num6, num7, 1);
					}
					else if (Main.tile[num6, num7].type == 53)
					{
						if (genRand.Next(2) == 0)
						{
							flag = true;
						}
						Main.tile[num6, num7].type = 116;
						SquareTileFrame(num6, num7);
						NetMessage.SendTileSquare(-1, num6, num7, 1);
					}
				}
				break;
			}
			}
		}

		public static bool SolidTile(int i, int j)
		{
			try
			{
				if (Main.tile[i, j] == null)
				{
					return true;
				}
				if (Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type] && !Main.tileSolidTop[Main.tile[i, j].type])
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		public static void MineHouse(int i, int j)
		{
			if ((Codable.RunGlobalMethod("ModWorld", "MineHouse", i, j) && !(bool)Codable.customMethodReturn) || i < 50 || i > Main.maxTilesX - 50 || j < 50 || j > Main.maxTilesY - 50)
			{
				return;
			}
			int num = genRand.Next(6, 12);
			int num2 = genRand.Next(3, 6);
			int num3 = genRand.Next(15, 30);
			int num4 = genRand.Next(15, 30);
			if (SolidTile(i, j) || Main.tile[i, j].wall > 0)
			{
				return;
			}
			int num5 = j - num;
			int num6 = j + num2;
			for (int k = 0; k < 2; k++)
			{
				bool flag = true;
				int num7 = i;
				int num8 = j;
				int num9 = -1;
				int num10 = num3;
				if (k == 1)
				{
					num9 = 1;
					num10 = num4;
					num7++;
				}
				while (flag)
				{
					if (num8 - num < num5)
					{
						num5 = num8 - num;
					}
					if (num8 + num2 > num6)
					{
						num6 = num8 + num2;
					}
					for (int l = 0; l < 2; l++)
					{
						int num11 = num8;
						bool flag2 = true;
						int num12 = num;
						int num13 = -1;
						if (l == 1)
						{
							num11++;
							num12 = num2;
							num13 = 1;
						}
						while (flag2)
						{
							if (num7 != i && Main.tile[num7 - num9, num11].wall != 27 && (SolidTile(num7 - num9, num11) || !Main.tile[num7 - num9, num11].active))
							{
								Main.tile[num7 - num9, num11].active = true;
								Main.tile[num7 - num9, num11].type = 30;
							}
							if (SolidTile(num7 - 1, num11))
							{
								Main.tile[num7 - 1, num11].type = 30;
							}
							if (SolidTile(num7 + 1, num11))
							{
								Main.tile[num7 + 1, num11].type = 30;
							}
							if (SolidTile(num7, num11))
							{
								int num14 = 0;
								if (SolidTile(num7 - 1, num11))
								{
									num14++;
								}
								if (SolidTile(num7 + 1, num11))
								{
									num14++;
								}
								if (SolidTile(num7, num11 - 1))
								{
									num14++;
								}
								if (SolidTile(num7, num11 + 1))
								{
									num14++;
								}
								if (num14 < 2)
								{
									Main.tile[num7, num11].active = false;
								}
								else
								{
									flag2 = false;
									Main.tile[num7, num11].type = 30;
								}
							}
							else
							{
								Main.tile[num7, num11].wall = 27;
								Main.tile[num7, num11].liquid = 0;
								Main.tile[num7, num11].lava = false;
							}
							num11 += num13;
							num12--;
							if (num12 <= 0)
							{
								if (!Main.tile[num7, num11].active)
								{
									Main.tile[num7, num11].active = true;
									Main.tile[num7, num11].type = 30;
								}
								flag2 = false;
							}
						}
					}
					num10--;
					num7 += num9;
					if (SolidTile(num7, num8))
					{
						int num15 = 0;
						int num16 = 0;
						int num17 = num8;
						bool flag3 = true;
						while (flag3)
						{
							num17--;
							num15++;
							if (SolidTile(num7 - num9, num17))
							{
								num15 = 999;
								flag3 = false;
							}
							else if (!SolidTile(num7, num17))
							{
								flag3 = false;
							}
						}
						num17 = num8;
						flag3 = true;
						while (flag3)
						{
							num17++;
							num16++;
							if (SolidTile(num7 - num9, num17))
							{
								num16 = 999;
								flag3 = false;
							}
							else if (!SolidTile(num7, num17))
							{
								flag3 = false;
							}
						}
						if (num16 <= num15)
						{
							if (num16 > num2)
							{
								num10 = 0;
							}
							else
							{
								num8 += num16 + 1;
							}
						}
						else if (num15 > num)
						{
							num10 = 0;
						}
						else
						{
							num8 -= num15 + 1;
						}
					}
					if (num10 <= 0)
					{
						flag = false;
					}
				}
			}
			int num18 = i - num3 - 1;
			int num19 = i + num4 + 2;
			int num20 = num5 - 1;
			int num21 = num6 + 2;
			for (int m = num18; m < num19; m++)
			{
				for (int n = num20; n < num21; n++)
				{
					if (Main.tile[m, n].wall == 27 && !Main.tile[m, n].active)
					{
						if (Main.tile[m - 1, n].wall != 27 && m < i && !SolidTile(m - 1, n))
						{
							PlaceTile(m, n, 30, mute: true);
							Main.tile[m, n].wall = 0;
						}
						if (Main.tile[m + 1, n].wall != 27 && m > i && !SolidTile(m + 1, n))
						{
							PlaceTile(m, n, 30, mute: true);
							Main.tile[m, n].wall = 0;
						}
						for (int num22 = m - 1; num22 <= m + 1; num22++)
						{
							for (int num23 = n - 1; num23 <= n + 1; num23++)
							{
								if (SolidTile(num22, num23))
								{
									Main.tile[num22, num23].type = 30;
								}
							}
						}
					}
					if (Main.tile[m, n].type == 30 && Main.tile[m - 1, n].wall == 27 && Main.tile[m + 1, n].wall == 27 && (Main.tile[m, n - 1].wall == 27 || Main.tile[m, n - 1].active) && (Main.tile[m, n + 1].wall == 27 || Main.tile[m, n + 1].active))
					{
						Main.tile[m, n].active = false;
						Main.tile[m, n].wall = 27;
					}
				}
			}
			for (int num24 = num18; num24 < num19; num24++)
			{
				for (int num25 = num20; num25 < num21; num25++)
				{
					if (Main.tile[num24, num25].type == 30)
					{
						if (Main.tile[num24 - 1, num25].wall == 27 && Main.tile[num24 + 1, num25].wall == 27 && !Main.tile[num24 - 1, num25].active && !Main.tile[num24 + 1, num25].active)
						{
							Main.tile[num24, num25].active = false;
							Main.tile[num24, num25].wall = 27;
						}
						if (Main.tile[num24, num25 - 1].type != 21 && Main.tile[num24 - 1, num25].wall == 27 && Main.tile[num24 + 1, num25].type == 30 && Main.tile[num24 + 2, num25].wall == 27 && !Main.tile[num24 - 1, num25].active && !Main.tile[num24 + 2, num25].active)
						{
							Main.tile[num24, num25].active = false;
							Main.tile[num24, num25].wall = 27;
							Main.tile[num24 + 1, num25].active = false;
							Main.tile[num24 + 1, num25].wall = 27;
						}
						if (Main.tile[num24, num25 - 1].wall == 27 && Main.tile[num24, num25 + 1].wall == 27 && !Main.tile[num24, num25 - 1].active && !Main.tile[num24, num25 + 1].active)
						{
							Main.tile[num24, num25].active = false;
							Main.tile[num24, num25].wall = 27;
						}
					}
				}
			}
			for (int num26 = num18; num26 < num19; num26++)
			{
				for (int num27 = num21; num27 > num20; num27--)
				{
					bool flag4 = false;
					if (Main.tile[num26, num27].active && Main.tile[num26, num27].type == 30)
					{
						int num28 = -1;
						for (int num29 = 0; num29 < 2; num29++)
						{
							if (!SolidTile(num26 + num28, num27) && Main.tile[num26 + num28, num27].wall == 0)
							{
								int num30 = 0;
								int num31 = num27;
								int num32 = num27;
								while (Main.tile[num26, num31].active && Main.tile[num26, num31].type == 30 && !SolidTile(num26 + num28, num31) && Main.tile[num26 + num28, num31].wall == 0)
								{
									num31--;
									num30++;
								}
								num31++;
								int num33 = num31 + 1;
								if (num30 > 4)
								{
									if (genRand.Next(2) == 0)
									{
										num31 = num32 - 1;
										bool flag5 = true;
										for (int num34 = num26 - 2; num34 <= num26 + 2; num34++)
										{
											for (int num35 = num31 - 2; num35 <= num31; num35++)
											{
												if (num34 != num26 && Main.tile[num34, num35].active)
												{
													flag5 = false;
												}
											}
										}
										if (flag5)
										{
											Main.tile[num26, num31].active = false;
											Main.tile[num26, num31 - 1].active = false;
											Main.tile[num26, num31 - 2].active = false;
											PlaceTile(num26, num31, 10, mute: true);
											flag4 = true;
										}
									}
									if (!flag4)
									{
										for (int num36 = num33; num36 < num32; num36++)
										{
											Main.tile[num26, num36].type = 124;
										}
									}
								}
							}
							num28 = 1;
						}
					}
					if (flag4)
					{
						break;
					}
				}
			}
			int num37;
			for (num37 = num18; num37 < num19; num37++)
			{
				bool flag6 = true;
				for (int num38 = num20; num38 < num21; num38++)
				{
					for (int num39 = num37 - 2; num39 <= num37 + 2; num39++)
					{
						if (Main.tile[num39, num38].active && (!SolidTile(num39, num38) || Main.tile[num39, num38].type == 10))
						{
							flag6 = false;
						}
					}
				}
				if (flag6)
				{
					for (int num40 = num20; num40 < num21; num40++)
					{
						if (Main.tile[num37, num40].wall == 27 && !Main.tile[num37, num40].active)
						{
							PlaceTile(num37, num40, 124, mute: true);
						}
					}
				}
				num37 += genRand.Next(3);
			}
			for (int num41 = 0; num41 < 4; num41++)
			{
				int num42 = genRand.Next(num18 + 2, num19 - 1);
				int num43 = genRand.Next(num20 + 2, num21 - 1);
				while (Main.tile[num42, num43].wall != 27)
				{
					num42 = genRand.Next(num18 + 2, num19 - 1);
					num43 = genRand.Next(num20 + 2, num21 - 1);
				}
				while (Main.tile[num42, num43].active)
				{
					num43--;
				}
				for (; !Main.tile[num42, num43].active; num43++)
				{
				}
				num43--;
				if (Main.tile[num42, num43].wall != 27)
				{
					continue;
				}
				if (genRand.Next(3) == 0)
				{
					int num44 = genRand.Next(9);
					switch (num44)
					{
					case 0:
						num44 = 14;
						break;
					case 1:
						num44 = 16;
						break;
					case 2:
						num44 = 18;
						break;
					case 3:
						num44 = 86;
						break;
					case 4:
						num44 = 87;
						break;
					case 5:
						num44 = 94;
						break;
					case 6:
						num44 = 101;
						break;
					case 7:
						num44 = 104;
						break;
					case 8:
						num44 = 106;
						break;
					}
					PlaceTile(num42, num43, num44, mute: true);
				}
				else
				{
					PlaceTile(num42, num43, 105, mute: true, forced: true, -1, genRand.Next(2, 43));
				}
			}
		}

		public static void CountTiles(int X)
		{
			if (X == 0)
			{
				totalEvil = totalEvil2;
				totalSolid = totalSolid2;
				totalGood = totalGood2;
				float num = (float)Math.Round((float)totalGood / (float)totalSolid * 100f);
				float num2 = (float)Math.Round((float)totalEvil / (float)totalSolid * 100f);
				tGood = (byte)num;
				tEvil = (byte)num2;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(57);
				}
				totalEvil2 = 0;
				totalSolid2 = 0;
				totalGood2 = 0;
			}
			for (int i = 0; i < Main.maxTilesY; i++)
			{
				int num3 = 1;
				if ((double)i <= Main.worldSurface)
				{
					num3 *= 5;
				}
				if (SolidTile(X, i))
				{
					if (Main.tile[X, i].type == 109 || Main.tile[X, i].type == 116 || Main.tile[X, i].type == 117)
					{
						totalGood2 += num3;
					}
					else if (Main.tile[X, i].type == 23 || Main.tile[X, i].type == 25 || Main.tile[X, i].type == 112)
					{
						totalEvil2 += num3;
					}
					totalSolid2 += num3;
				}
			}
		}

		public static void UpdateWorld()
		{
			if (TMod.RunMethod(TMod.WorldHooks.PreUpdateWorld) && !TMod.GetContinueMethod())
			{
				return;
			}
			UpdateMech();
			totalD++;
			if (totalD >= 10)
			{
				totalD = 0;
				CountTiles(totalX);
				totalX++;
				if (totalX >= Main.maxTilesX)
				{
					totalX = 0;
				}
			}
			Liquid.skipCount++;
			if (Liquid.skipCount > 1)
			{
				Liquid.UpdateLiquid();
				Liquid.skipCount = 0;
			}
			float num = 3E-05f * (float)Main.worldRate;
			float num2 = 1.5E-05f * (float)Main.worldRate;
			bool flag = false;
			spawnDelay++;
			if (Main.invasionType > 0)
			{
				spawnDelay = 0;
			}
			if (spawnDelay >= 20)
			{
				flag = true;
				spawnDelay = 0;
				if (spawnNPC != 37)
				{
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].active && Main.npc[i].homeless && Main.npc[i].townNPC)
						{
							spawnNPC = Main.npc[i].type;
							break;
						}
					}
				}
			}
			for (int j = 0; (float)j < (float)(Main.maxTilesX * Main.maxTilesY) * num; j++)
			{
				int num3 = genRand.Next(10, Main.maxTilesX - 10);
				int num4 = genRand.Next(10, (int)Main.worldSurface - 1);
				int num5 = num3 - 1;
				int num6 = num3 + 2;
				int num7 = num4 - 1;
				int num8 = num4 + 2;
				if (num5 < 10)
				{
					num5 = 10;
				}
				if (num6 > Main.maxTilesX - 10)
				{
					num6 = Main.maxTilesX - 10;
				}
				if (num7 < 10)
				{
					num7 = 10;
				}
				if (num8 > Main.maxTilesY - 10)
				{
					num8 = Main.maxTilesY - 10;
				}
				if (Main.tile[num3, num4] == null)
				{
					continue;
				}
				if (Main.tileAlch[Main.tile[num3, num4].type])
				{
					GrowAlch(num3, num4);
				}
				if (Main.tile[num3, num4].liquid > 32)
				{
					if (Main.tile[num3, num4].active && (Main.tile[num3, num4].type == 3 || Main.tile[num3, num4].type == 20 || Main.tile[num3, num4].type == 24 || Main.tile[num3, num4].type == 27 || Main.tile[num3, num4].type == 73))
					{
						KillTile(num3, num4);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, "", 0, num3, num4);
						}
					}
				}
				else if (Main.tile[num3, num4].active)
				{
					hardUpdateWorld(num3, num4);
					if (Main.tile[num3, num4].type == 80)
					{
						if (genRand.Next(15) == 0)
						{
							GrowCactus(num3, num4);
						}
					}
					else if (Main.tile[num3, num4].type == 53)
					{
						if (!Main.tile[num3, num7].active)
						{
							if (num3 < 250 || num3 > Main.maxTilesX - 250)
							{
								if (genRand.Next(500) == 0 && Main.tile[num3, num7].liquid == byte.MaxValue && Main.tile[num3, num7 - 1].liquid == byte.MaxValue && Main.tile[num3, num7 - 2].liquid == byte.MaxValue && Main.tile[num3, num7 - 3].liquid == byte.MaxValue && Main.tile[num3, num7 - 4].liquid == byte.MaxValue)
								{
									PlaceTile(num3, num7, 81, mute: true);
									if (Main.netMode == 2 && Main.tile[num3, num7].active)
									{
										NetMessage.SendTileSquare(-1, num3, num7, 1);
									}
								}
							}
							else if (num3 > 400 && num3 < Main.maxTilesX - 400 && genRand.Next(300) == 0)
							{
								GrowCactus(num3, num4);
							}
						}
					}
					else if (Main.tile[num3, num4].type == 116 || Main.tile[num3, num4].type == 112)
					{
						if (!Main.tile[num3, num7].active && num3 > 400 && num3 < Main.maxTilesX - 400 && genRand.Next(300) == 0)
						{
							GrowCactus(num3, num4);
						}
					}
					else if (Main.tile[num3, num4].type == 78)
					{
						if (!Main.tile[num3, num7].active)
						{
							PlaceTile(num3, num7, 3, mute: true);
							if (Main.netMode == 2 && Main.tile[num3, num7].active)
							{
								NetMessage.SendTileSquare(-1, num3, num7, 1);
							}
						}
					}
					else if (Main.tile[num3, num4].type == 2 || Main.tile[num3, num4].type == 23 || Main.tile[num3, num4].type == 32 || Main.tile[num3, num4].type == 109)
					{
						int num9 = Main.tile[num3, num4].type;
						if (!Main.tile[num3, num7].active)
						{
							if (genRand.Next(12) == 0 && num9 == 2)
							{
								PlaceTile(num3, num7, 3, mute: true);
								if (Main.netMode == 2 && Main.tile[num3, num7].active)
								{
									NetMessage.SendTileSquare(-1, num3, num7, 1);
								}
							}
							if (genRand.Next(10) == 0 && num9 == 23)
							{
								PlaceTile(num3, num7, 24, mute: true);
								if (Main.netMode == 2 && Main.tile[num3, num7].active)
								{
									NetMessage.SendTileSquare(-1, num3, num7, 1);
								}
							}
							if (genRand.Next(10) == 0 && num9 == 109)
							{
								PlaceTile(num3, num7, 110, mute: true);
								if (Main.netMode == 2 && Main.tile[num3, num7].active)
								{
									NetMessage.SendTileSquare(-1, num3, num7, 1);
								}
							}
						}
						bool flag2 = false;
						for (int k = num5; k < num6; k++)
						{
							for (int l = num7; l < num8; l++)
							{
								if ((num3 == k && num4 == l) || !Main.tile[k, l].active)
								{
									continue;
								}
								if (num9 == 32)
								{
									num9 = 23;
								}
								if (Main.tile[k, l].type == 0 || (num9 == 23 && Main.tile[k, l].type == 2) || (num9 == 23 && Main.tile[k, l].type == 109))
								{
									SpreadGrass(k, l, 0, num9, repeat: false);
									if (num9 == 23)
									{
										SpreadGrass(k, l, 2, num9, repeat: false);
										SpreadGrass(k, l, 109, num9, repeat: false);
									}
									if (Main.tile[k, l].type == num9)
									{
										SquareTileFrame(k, l);
										flag2 = true;
									}
								}
								if (Main.tile[k, l].type == 0 || (num9 == 109 && Main.tile[k, l].type == 2) || (num9 == 109 && Main.tile[k, l].type == 23))
								{
									SpreadGrass(k, l, 0, num9, repeat: false);
									if (num9 == 109)
									{
										SpreadGrass(k, l, 2, num9, repeat: false);
										SpreadGrass(k, l, 23, num9, repeat: false);
									}
									if (Main.tile[k, l].type == num9)
									{
										SquareTileFrame(k, l);
										flag2 = true;
									}
								}
							}
						}
						if (Main.netMode == 2 && flag2)
						{
							NetMessage.SendTileSquare(-1, num3, num4, 3);
						}
					}
					else if (Main.tile[num3, num4].type == 20 && genRand.Next(20) == 0 && !PlayerLOS(num3, num4))
					{
						GrowTree(num3, num4);
					}
					else if (Main.tile[num3, num4].type == 3 && genRand.Next(20) == 0 && Main.tile[num3, num4].frameX < 144)
					{
						Main.tile[num3, num4].type = 73;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num3, num4, 3);
						}
					}
					else if (Main.tile[num3, num4].type == 110 && genRand.Next(20) == 0 && Main.tile[num3, num4].frameX < 144)
					{
						Main.tile[num3, num4].type = 113;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num3, num4, 3);
						}
					}
					else if (Main.tile[num3, num4].type == 32 && genRand.Next(3) == 0)
					{
						int num10 = num3;
						int num11 = num4;
						int num12 = 0;
						if (Main.tile[num10 + 1, num11].active && Main.tile[num10 + 1, num11].type == 32)
						{
							num12++;
						}
						if (Main.tile[num10 - 1, num11].active && Main.tile[num10 - 1, num11].type == 32)
						{
							num12++;
						}
						if (Main.tile[num10, num11 + 1].active && Main.tile[num10, num11 + 1].type == 32)
						{
							num12++;
						}
						if (Main.tile[num10, num11 - 1].active && Main.tile[num10, num11 - 1].type == 32)
						{
							num12++;
						}
						if (num12 < 3 || Main.tile[num3, num4].type == 23)
						{
							switch (genRand.Next(4))
							{
							case 0:
								num11--;
								break;
							case 1:
								num11++;
								break;
							case 2:
								num10--;
								break;
							case 3:
								num10++;
								break;
							}
							if (!Main.tile[num10, num11].active)
							{
								num12 = 0;
								if (Main.tile[num10 + 1, num11].active && Main.tile[num10 + 1, num11].type == 32)
								{
									num12++;
								}
								if (Main.tile[num10 - 1, num11].active && Main.tile[num10 - 1, num11].type == 32)
								{
									num12++;
								}
								if (Main.tile[num10, num11 + 1].active && Main.tile[num10, num11 + 1].type == 32)
								{
									num12++;
								}
								if (Main.tile[num10, num11 - 1].active && Main.tile[num10, num11 - 1].type == 32)
								{
									num12++;
								}
								if (num12 < 2)
								{
									int num13 = 7;
									int num14 = num10 - num13;
									int num15 = num10 + num13;
									int num16 = num11 - num13;
									int num17 = num11 + num13;
									bool flag3 = false;
									for (int m = num14; m < num15; m++)
									{
										for (int n = num16; n < num17; n++)
										{
											if (Math.Abs(m - num10) * 2 + Math.Abs(n - num11) < 9 && Main.tile[m, n].active && Main.tile[m, n].type == 23 && Main.tile[m, n - 1].active && Main.tile[m, n - 1].type == 32 && Main.tile[m, n - 1].liquid == 0)
											{
												flag3 = true;
												break;
											}
										}
									}
									if (flag3)
									{
										Main.tile[num10, num11].type = 32;
										Main.tile[num10, num11].active = true;
										SquareTileFrame(num10, num11);
										if (Main.netMode == 2)
										{
											NetMessage.SendTileSquare(-1, num10, num11, 3);
										}
									}
								}
							}
						}
					}
					else if (flag && spawnNPC > 0)
					{
						SpawnNPC(num3, num4);
					}
				}
				if (!Main.tile[num3, num4].active)
				{
					continue;
				}
				if ((Main.tile[num3, num4].type == 2 || Main.tile[num3, num4].type == 52) && genRand.Next(40) == 0 && !Main.tile[num3, num4 + 1].active && !Main.tile[num3, num4 + 1].lava)
				{
					bool flag4 = false;
					for (int num18 = num4; num18 > num4 - 10; num18--)
					{
						if (Main.tile[num3, num18].active && Main.tile[num3, num18].type == 2)
						{
							flag4 = true;
							break;
						}
					}
					if (flag4)
					{
						int num19 = num3;
						int num20 = num4 + 1;
						Main.tile[num19, num20].type = 52;
						Main.tile[num19, num20].active = true;
						SquareTileFrame(num19, num20);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num19, num20, 3);
						}
					}
				}
				else if (Main.tile[num3, num4].type == 60)
				{
					int type = Main.tile[num3, num4].type;
					if (!Main.tile[num3, num7].active && genRand.Next(7) == 0)
					{
						PlaceTile(num3, num7, 61, mute: true);
						if (Main.netMode == 2 && Main.tile[num3, num7].active)
						{
							NetMessage.SendTileSquare(-1, num3, num7, 1);
						}
					}
					else if (genRand.Next(500) == 0 && (!Main.tile[num3, num7].active || Main.tile[num3, num7].type == 61 || Main.tile[num3, num7].type == 74 || Main.tile[num3, num7].type == 69) && !PlayerLOS(num3, num4))
					{
						GrowTree(num3, num4);
					}
					bool flag5 = false;
					for (int num21 = num5; num21 < num6; num21++)
					{
						for (int num22 = num7; num22 < num8; num22++)
						{
							if ((num3 != num21 || num4 != num22) && Main.tile[num21, num22].active && Main.tile[num21, num22].type == 59)
							{
								SpreadGrass(num21, num22, 59, type, repeat: false);
								if (Main.tile[num21, num22].type == type)
								{
									SquareTileFrame(num21, num22);
									flag5 = true;
								}
							}
						}
					}
					if (Main.netMode == 2 && flag5)
					{
						NetMessage.SendTileSquare(-1, num3, num4, 3);
					}
				}
				else if (Main.tile[num3, num4].type == 61 && genRand.Next(3) == 0 && Main.tile[num3, num4].frameX < 144)
				{
					Main.tile[num3, num4].type = 74;
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, num3, num4, 3);
					}
				}
				else if ((Main.tile[num3, num4].type == 60 || Main.tile[num3, num4].type == 62) && genRand.Next(15) == 0 && !Main.tile[num3, num4 + 1].active && !Main.tile[num3, num4 + 1].lava)
				{
					bool flag6 = false;
					for (int num23 = num4; num23 > num4 - 10; num23--)
					{
						if (Main.tile[num3, num23].active && Main.tile[num3, num23].type == 60)
						{
							flag6 = true;
							break;
						}
					}
					if (flag6)
					{
						int num24 = num3;
						int num25 = num4 + 1;
						Main.tile[num24, num25].type = 62;
						Main.tile[num24, num25].active = true;
						SquareTileFrame(num24, num25);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num24, num25, 3);
						}
					}
				}
				else
				{
					if ((Main.tile[num3, num4].type != 109 && Main.tile[num3, num4].type != 115) || genRand.Next(15) != 0 || Main.tile[num3, num4 + 1].active || Main.tile[num3, num4 + 1].lava)
					{
						continue;
					}
					bool flag7 = false;
					for (int num26 = num4; num26 > num4 - 10; num26--)
					{
						if (Main.tile[num3, num26].active && Main.tile[num3, num26].type == 109)
						{
							flag7 = true;
							break;
						}
					}
					if (flag7)
					{
						int num27 = num3;
						int num28 = num4 + 1;
						Main.tile[num27, num28].type = 115;
						Main.tile[num27, num28].active = true;
						SquareTileFrame(num27, num28);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num27, num28, 3);
						}
					}
				}
			}
			for (int num29 = 0; (float)num29 < (float)(Main.maxTilesX * Main.maxTilesY) * num2; num29++)
			{
				int num30 = genRand.Next(10, Main.maxTilesX - 10);
				int num31 = genRand.Next((int)Main.worldSurface - 1, Main.maxTilesY - 20);
				int num32 = num30 - 1;
				int num33 = num30 + 2;
				int num34 = num31 - 1;
				int num35 = num31 + 2;
				if (num32 < 10)
				{
					num32 = 10;
				}
				if (num33 > Main.maxTilesX - 10)
				{
					num33 = Main.maxTilesX - 10;
				}
				if (num34 < 10)
				{
					num34 = 10;
				}
				if (num35 > Main.maxTilesY - 10)
				{
					num35 = Main.maxTilesY - 10;
				}
				if (Main.tile[num30, num31] == null)
				{
					continue;
				}
				if (Main.tileAlch[Main.tile[num30, num31].type])
				{
					GrowAlch(num30, num31);
				}
				if (Main.tile[num30, num31].liquid > 32)
				{
					continue;
				}
				if (Main.tile[num30, num31].active)
				{
					hardUpdateWorld(num30, num31);
					if (Main.tile[num30, num31].type == 23 && !Main.tile[num30, num34].active && genRand.Next(1) == 0)
					{
						PlaceTile(num30, num34, 24, mute: true);
						if (Main.netMode == 2 && Main.tile[num30, num34].active)
						{
							NetMessage.SendTileSquare(-1, num30, num34, 1);
						}
					}
					if (Main.tile[num30, num31].type == 32 && genRand.Next(3) == 0)
					{
						int num36 = num30;
						int num37 = num31;
						int num38 = 0;
						if (Main.tile[num36 + 1, num37].active && Main.tile[num36 + 1, num37].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36 - 1, num37].active && Main.tile[num36 - 1, num37].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36, num37 + 1].active && Main.tile[num36, num37 + 1].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36, num37 - 1].active && Main.tile[num36, num37 - 1].type == 32)
						{
							num38++;
						}
						if (num38 >= 3 && Main.tile[num30, num31].type != 23)
						{
							continue;
						}
						switch (genRand.Next(4))
						{
						case 0:
							num37--;
							break;
						case 1:
							num37++;
							break;
						case 2:
							num36--;
							break;
						case 3:
							num36++;
							break;
						}
						if (Main.tile[num36, num37].active)
						{
							continue;
						}
						num38 = 0;
						if (Main.tile[num36 + 1, num37].active && Main.tile[num36 + 1, num37].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36 - 1, num37].active && Main.tile[num36 - 1, num37].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36, num37 + 1].active && Main.tile[num36, num37 + 1].type == 32)
						{
							num38++;
						}
						if (Main.tile[num36, num37 - 1].active && Main.tile[num36, num37 - 1].type == 32)
						{
							num38++;
						}
						if (num38 >= 2)
						{
							continue;
						}
						int num39 = 7;
						int num40 = num36 - num39;
						int num41 = num36 + num39;
						int num42 = num37 - num39;
						int num43 = num37 + num39;
						bool flag8 = false;
						for (int num44 = num40; num44 < num41; num44++)
						{
							for (int num45 = num42; num45 < num43; num45++)
							{
								if (Math.Abs(num44 - num36) * 2 + Math.Abs(num45 - num37) < 9 && Main.tile[num44, num45].active && Main.tile[num44, num45].type == 23 && Main.tile[num44, num45 - 1].active && Main.tile[num44, num45 - 1].type == 32 && Main.tile[num44, num45 - 1].liquid == 0)
								{
									flag8 = true;
									break;
								}
							}
						}
						if (flag8)
						{
							Main.tile[num36, num37].type = 32;
							Main.tile[num36, num37].active = true;
							SquareTileFrame(num36, num37);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num36, num37, 3);
							}
						}
					}
					else if (Main.tile[num30, num31].type == 60)
					{
						if (!Main.tile[num30, num34].active && genRand.Next(10) == 0)
						{
							PlaceTile(num30, num34, 61, mute: true);
							if (Main.netMode == 2 && Main.tile[num30, num34].active)
							{
								NetMessage.SendTileSquare(-1, num30, num34, 1);
							}
						}
						bool flag9 = false;
						for (int num46 = num32; num46 < num33; num46++)
						{
							for (int num47 = num34; num47 < num35; num47++)
							{
								if ((num30 != num46 || num31 != num47) && Main.tile[num46, num47].active && Main.tile[num46, num47].type == 59)
								{
									SpreadGrass(num46, num47, 59, Main.tile[num30, num31].type, repeat: false);
									if (Main.tile[num46, num47].type == Main.tile[num30, num31].type)
									{
										SquareTileFrame(num46, num47);
										flag9 = true;
									}
								}
							}
						}
						if (Main.netMode == 2 && flag9)
						{
							NetMessage.SendTileSquare(-1, num30, num31, 3);
						}
					}
					else if (Main.tile[num30, num31].type == 61 && genRand.Next(3) == 0 && Main.tile[num30, num31].frameX < 144)
					{
						Main.tile[num30, num31].type = 74;
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num30, num31, 3);
						}
					}
					else if ((Main.tile[num30, num31].type == 60 || Main.tile[num30, num31].type == 62) && genRand.Next(5) == 0 && !Main.tile[num30, num31 + 1].active && !Main.tile[num30, num31 + 1].lava)
					{
						bool flag10 = false;
						for (int num48 = num31; num48 > num31 - 10; num48--)
						{
							if (Main.tile[num30, num48].active && Main.tile[num30, num48].type == 60)
							{
								flag10 = true;
								break;
							}
						}
						if (flag10)
						{
							int num49 = num30;
							int num50 = num31 + 1;
							Main.tile[num49, num50].type = 62;
							Main.tile[num49, num50].active = true;
							SquareTileFrame(num49, num50);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num49, num50, 3);
							}
						}
					}
					else if (Main.tile[num30, num31].type == 69 && genRand.Next(3) == 0)
					{
						int num51 = num30;
						int num52 = num31;
						int num53 = 0;
						if (Main.tile[num51 + 1, num52].active && Main.tile[num51 + 1, num52].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51 - 1, num52].active && Main.tile[num51 - 1, num52].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51, num52 + 1].active && Main.tile[num51, num52 + 1].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51, num52 - 1].active && Main.tile[num51, num52 - 1].type == 69)
						{
							num53++;
						}
						if (num53 >= 3 && Main.tile[num30, num31].type != 60)
						{
							continue;
						}
						switch (genRand.Next(4))
						{
						case 0:
							num52--;
							break;
						case 1:
							num52++;
							break;
						case 2:
							num51--;
							break;
						case 3:
							num51++;
							break;
						}
						if (Main.tile[num51, num52].active)
						{
							continue;
						}
						num53 = 0;
						if (Main.tile[num51 + 1, num52].active && Main.tile[num51 + 1, num52].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51 - 1, num52].active && Main.tile[num51 - 1, num52].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51, num52 + 1].active && Main.tile[num51, num52 + 1].type == 69)
						{
							num53++;
						}
						if (Main.tile[num51, num52 - 1].active && Main.tile[num51, num52 - 1].type == 69)
						{
							num53++;
						}
						if (num53 >= 2)
						{
							continue;
						}
						int num54 = 7;
						int num55 = num51 - num54;
						int num56 = num51 + num54;
						int num57 = num52 - num54;
						int num58 = num52 + num54;
						bool flag11 = false;
						for (int num59 = num55; num59 < num56; num59++)
						{
							for (int num60 = num57; num60 < num58; num60++)
							{
								if (Math.Abs(num59 - num51) * 2 + Math.Abs(num60 - num52) < 9 && Main.tile[num59, num60].active && Main.tile[num59, num60].type == 60 && Main.tile[num59, num60 - 1].active && Main.tile[num59, num60 - 1].type == 69 && Main.tile[num59, num60 - 1].liquid == 0)
								{
									flag11 = true;
									break;
								}
							}
						}
						if (flag11)
						{
							Main.tile[num51, num52].type = 69;
							Main.tile[num51, num52].active = true;
							SquareTileFrame(num51, num52);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num51, num52, 3);
							}
						}
					}
					else
					{
						if (Main.tile[num30, num31].type != 70)
						{
							continue;
						}
						if (!Main.tile[num30, num34].active && genRand.Next(10) == 0)
						{
							PlaceTile(num30, num34, 71, mute: true);
							if (Main.netMode == 2 && Main.tile[num30, num34].active)
							{
								NetMessage.SendTileSquare(-1, num30, num34, 1);
							}
						}
						if (genRand.Next(200) == 0 && !PlayerLOS(num30, num31))
						{
							GrowShroom(num30, num31);
						}
						bool flag12 = false;
						for (int num61 = num32; num61 < num33; num61++)
						{
							for (int num62 = num34; num62 < num35; num62++)
							{
								if ((num30 != num61 || num31 != num62) && Main.tile[num61, num62].active && Main.tile[num61, num62].type == 59)
								{
									SpreadGrass(num61, num62, 59, Main.tile[num30, num31].type, repeat: false);
									if (Main.tile[num61, num62].type == Main.tile[num30, num31].type)
									{
										SquareTileFrame(num61, num62);
										flag12 = true;
									}
								}
							}
						}
						if (Main.netMode == 2 && flag12)
						{
							NetMessage.SendTileSquare(-1, num30, num31, 3);
						}
					}
				}
				else if (flag && spawnNPC > 0)
				{
					SpawnNPC(num30, num31);
				}
			}
			if (Main.rand.Next(100) == 0)
			{
				PlantAlch();
			}
			if (!Main.dayTime && Main.rand.Next(8000) < 10 * Main.maxTilesX / 4200)
			{
				Vector2 vector = new Vector2((Main.rand.Next(Main.maxTilesX - 50) + 100) * 16, Main.rand.Next((int)((float)Main.maxTilesY * 0.05f)) * 16);
				float num63 = Main.rand.Next(-100, 101);
				float num64 = Main.rand.Next(200) + 100;
				float num65 = 12f / (float)Math.Sqrt(num63 * num63 + num64 * num64);
				num63 *= num65;
				num64 *= num65;
				Projectile.NewProjectile(vector.X, vector.Y, num63, num64, 12, 1000, 10f, Main.myPlayer);
			}
			TMod.RunMethod(TMod.WorldHooks.UpdateWorld);
			if (WorldGen.TileUpdate != null)
			{
				WorldGen.TileUpdate();
			}
		}

		public static void PlaceWall(int i, int j, int type, bool mute = false)
		{
			if (i <= 1 || j <= 1 || i >= Main.maxTilesX - 2 || j >= Main.maxTilesY - 2)
			{
				return;
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			if (Main.tile[i, j].wall == 0)
			{
				Main.tile[i, j].wall = (ushort)type;
				SquareWallFrame(i, j);
				if (!mute)
				{
					Main.PlaySound(0, i * 16, j * 16);
				}
			}
		}

		public static void AddPlants()
		{
			if (Codable.RunGlobalMethod("ModWorld", "AddPlants") && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 1; j < Main.maxTilesY; j++)
				{
					if (Main.tile[i, j].type == 2 && Main.tile[i, j].active)
					{
						if (!Main.tile[i, j - 1].active)
						{
							PlaceTile(i, j - 1, 3, mute: true);
						}
					}
					else if (Main.tile[i, j].type == 23 && Main.tile[i, j].active && !Main.tile[i, j - 1].active)
					{
						PlaceTile(i, j - 1, 24, mute: true);
					}
				}
			}
		}

		public static void SpreadGrass(int i, int j, int dirt = 0, int grass = 2, bool repeat = true)
		{
			if (!TMod.RunMethod(TMod.WorldHooks.SpreadGrass, i, j, dirt, grass, repeat) || TMod.GetContinueMethod())
			{
				try
				{
					if (Main.tile[i, j].type == dirt && Main.tile[i, j].active && ((double)j >= Main.worldSurface || grass != 70) && ((double)j < Main.worldSurface || dirt != 0))
					{
						int num = i - 1;
						int num2 = i + 2;
						int num3 = j - 1;
						int num4 = j + 2;
						if (num < 0)
						{
							num = 0;
						}
						if (num2 > Main.maxTilesX)
						{
							num2 = Main.maxTilesX;
						}
						if (num3 < 0)
						{
							num3 = 0;
						}
						if (num4 > Main.maxTilesY)
						{
							num4 = Main.maxTilesY;
						}
						bool flag = true;
						for (int k = num; k < num2; k++)
						{
							for (int l = num3; l < num4; l++)
							{
								if (!Main.tile[k, l].active || !Main.tileSolid[Main.tile[k, l].type])
								{
									flag = false;
								}
								if (Main.tile[k, l].lava && Main.tile[k, l].liquid > 0)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag && (grass != 23 || Main.tile[i, j - 1].type != 27))
						{
							Main.tile[i, j].type = (ushort)grass;
							for (int m = num; m < num2; m++)
							{
								for (int n = num3; n < num4; n++)
								{
									if (Main.tile[m, n].active && Main.tile[m, n].type == dirt)
									{
										try
										{
											if (repeat && grassSpread < 1000)
											{
												grassSpread++;
												SpreadGrass(m, n, dirt, grass);
												grassSpread--;
											}
										}
										catch
										{
										}
									}
								}
							}
						}
					}
				}
				catch
				{
				}
			}
		}

		public static void ChasmRunnerSideways(int i, int j, int direction, int steps)
		{
			if (TMod.RunMethod(TMod.WorldHooks.ChasmRunnerSideways, i, j, direction, steps) && !TMod.GetContinueMethod())
			{
				return;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(10, 21) * 0.1f * (float)direction;
			vector2.Y = (float)genRand.Next(-10, 10) * 0.01f;
			int num = steps;
			int num2 = genRand.Next(5) + 7;
			while (num2 > 0)
			{
				if (num > 0)
				{
					num2 += genRand.Next(3);
					num2 -= genRand.Next(3);
					if (num2 < 7)
					{
						num2 = 7;
					}
					else if (num2 > 20)
					{
						num2 = 20;
					}
					if (num == 1 && num2 < 10)
					{
						num2 = 10;
					}
				}
				else
				{
					num2 -= genRand.Next(4);
				}
				if ((double)vector.Y > Main.rockLayer && num > 0)
				{
					num = 0;
				}
				num--;
				int num3 = (int)(vector.X - (float)num2 * 0.5f);
				int num4 = (int)(vector.X + (float)num2 * 0.5f);
				int num5 = (int)(vector.Y - (float)num2 * 0.5f);
				int num6 = (int)(vector.Y + (float)num2 * 0.5f);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if (Main.tile[k, l].type != 31 && Main.tile[k, l].type != 22 && Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < (float)num2 * 0.5f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							Main.tile[k, l].active = false;
						}
					}
				}
				vector += vector2;
				vector2.Y += (float)genRand.Next(-10, 10) * 0.1f;
				if (vector.Y < (float)(j - 20))
				{
					vector2.Y += (float)genRand.Next(20) * 0.01f;
				}
				else if (vector.Y > (float)(j + 20))
				{
					vector2.Y -= (float)genRand.Next(20) * 0.01f;
				}
				if (vector2.Y < -0.5f)
				{
					vector2.Y = -0.5f;
				}
				else if (vector2.Y > 0.5f)
				{
					vector2.Y = 0.5f;
				}
				vector2.X += (float)genRand.Next(-10, 11) * 0.01f;
				switch (direction)
				{
				case -1:
					if (vector2.X > -0.5f)
					{
						vector2.X = -0.5f;
					}
					else if (vector2.X < -2f)
					{
						vector2.X = -2f;
					}
					break;
				case 1:
					if (vector2.X < 0.5f)
					{
						vector2.X = 0.5f;
					}
					else if (vector2.X > 2f)
					{
						vector2.X = 2f;
					}
					break;
				}
				num3 = (int)(vector.X - (float)num2 * 1.1f);
				num4 = (int)(vector.X + (float)num2 * 1.1f);
				num5 = (int)(vector.Y - (float)num2 * 1.1f);
				num6 = (int)(vector.Y + (float)num2 * 1.1f);
				if (num3 < 1)
				{
					num3 = 1;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if (Main.tile[m, n].wall != 3 && Math.Abs((float)m - vector.X) + Math.Abs((float)n - vector.Y) < (float)num2 * 1.1f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							Main.tile[m, n].active = true;
							if (Main.tile[m, n].type != 31 && Main.tile[m, n].type != 22)
							{
								Main.tile[m, n].type = 25;
							}
							if (Main.tile[m, n].wall == 2)
							{
								Main.tile[m, n].wall = 0;
							}
							PlaceWall(m, n, 3, mute: true);
						}
					}
				}
			}
			if (genRand.Next(3) == 0)
			{
				int num7 = (int)vector.X;
				int num8;
				for (num8 = (int)vector.Y; !Main.tile[num7, num8].active; num8++)
				{
				}
				TileRunner(num7, num8, genRand.Next(2, 6), genRand.Next(3, 7), 22);
			}
		}

		public static void ChasmRunner(int i, int j, int steps, bool makeOrb = false)
		{
			if (TMod.RunMethod(TMod.WorldHooks.ChasmRunner, i, j, steps, makeOrb) && !TMod.GetContinueMethod())
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (!makeOrb)
			{
				flag2 = true;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(11) * 0.2f + 0.5f;
			int num = 5;
			int num2 = genRand.Next(5) + 7;
			while (num2 > 0)
			{
				if (steps > 0)
				{
					num2 += genRand.Next(3);
					num2 -= genRand.Next(3);
					if (num2 < 7)
					{
						num2 = 7;
					}
					else if (num2 > 20)
					{
						num2 = 20;
					}
					if (steps == 1 && num2 < 10)
					{
						num2 = 10;
					}
				}
				else if ((double)vector.Y > Main.worldSurface + 45.0)
				{
					num2 -= genRand.Next(4);
				}
				if ((double)vector.Y > Main.rockLayer && steps > 0)
				{
					steps = 0;
				}
				steps--;
				if (!flag && (double)vector.Y > Main.worldSurface + 20.0)
				{
					flag = true;
					ChasmRunnerSideways((int)vector.X, (int)vector.Y, -1, genRand.Next(20, 40));
					ChasmRunnerSideways((int)vector.X, (int)vector.Y, 1, genRand.Next(20, 40));
				}
				int num3;
				int num4;
				int num5;
				int num6;
				if (steps > num)
				{
					num3 = (int)(vector.X - (float)num2 * 0.5f);
					num4 = (int)(vector.X + (float)num2 * 0.5f);
					num5 = (int)(vector.Y - (float)num2 * 0.5f);
					num6 = (int)(vector.Y + (float)num2 * 0.5f);
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 > Main.maxTilesX - 1)
					{
						num4 = Main.maxTilesX - 1;
					}
					if (num5 < 0)
					{
						num5 = 0;
					}
					if (num6 > Main.maxTilesY)
					{
						num6 = Main.maxTilesY;
					}
					for (int k = num3; k < num4; k++)
					{
						for (int l = num5; l < num6; l++)
						{
							if (Main.tile[k, l].type != 31 && Main.tile[k, l].type != 22 && Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < (float)num2 * 0.5f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
							{
								Main.tile[k, l].active = false;
							}
						}
					}
				}
				if (steps <= 2 && (double)vector.Y < Main.worldSurface + 45.0)
				{
					steps = 2;
				}
				if (steps <= 0)
				{
					if (!flag2)
					{
						flag2 = true;
						AddShadowOrb((int)vector.X, (int)vector.Y);
					}
					else if (!flag3)
					{
						flag3 = false;
						bool flag4 = false;
						int num7 = 0;
						while (!flag4)
						{
							int num8 = genRand.Next((int)vector.X - 25, (int)vector.X + 25);
							int num9 = genRand.Next((int)vector.Y - 50, (int)vector.Y);
							if (num8 < 5)
							{
								num8 = 5;
							}
							else if (num8 > Main.maxTilesX - 5)
							{
								num8 = Main.maxTilesX - 5;
							}
							if (num9 < 5)
							{
								num9 = 5;
							}
							else if (num9 > Main.maxTilesY - 5)
							{
								num9 = Main.maxTilesY - 5;
							}
							if ((double)num9 > Main.worldSurface)
							{
								Place3x2(num8, num9, 26);
								if (Main.tile[num8, num9].type == 26)
								{
									flag4 = true;
									continue;
								}
								num7++;
								if (num7 >= 10000)
								{
									flag4 = true;
								}
							}
							else
							{
								flag4 = true;
							}
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.01f;
				if (vector2.X > 0.3f)
				{
					vector2.X = 0.3f;
				}
				else if (vector2.X < -0.3f)
				{
					vector2.X = -0.3f;
				}
				num3 = (int)(vector.X - (float)num2 * 1.1f);
				num4 = (int)(vector.X + (float)num2 * 1.1f);
				num5 = (int)(vector.Y - (float)num2 * 1.1f);
				num6 = (int)(vector.Y + (float)num2 * 1.1f);
				if (num3 < 1)
				{
					num3 = 1;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if (Math.Abs((float)m - vector.X) + Math.Abs((float)n - vector.Y) < (float)num2 * 1.1f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							if (Main.tile[m, n].type != 25 && n > j + genRand.Next(3, 20))
							{
								Main.tile[m, n].active = true;
							}
							if (steps <= num)
							{
								Main.tile[m, n].active = true;
							}
							if (Main.tile[m, n].type != 31)
							{
								Main.tile[m, n].type = 25;
							}
							if (Main.tile[m, n].wall == 2)
							{
								Main.tile[m, n].wall = 0;
							}
						}
					}
				}
				for (int num10 = num3; num10 < num4; num10++)
				{
					for (int num11 = num5; num11 < num6; num11++)
					{
						if (Math.Abs((float)num10 - vector.X) + Math.Abs((float)num11 - vector.Y) < (float)num2 * 1.1f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							if (Main.tile[num10, num11].type != 31)
							{
								Main.tile[num10, num11].type = 25;
							}
							if (steps <= num)
							{
								Main.tile[num10, num11].active = true;
							}
							if (num11 > j + genRand.Next(3, 20))
							{
								PlaceWall(num10, num11, 3, mute: true);
							}
						}
					}
				}
			}
		}

		public static void JungleRunner(int i, int j)
		{
			if (Codable.RunGlobalMethod("ModWorld", "JungleRunner", i, j) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			float num = genRand.Next(5, 11);
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(10, 20) * 0.1f;
			int num2 = 0;
			bool flag = true;
			while (flag)
			{
				if ((double)vector.Y < Main.worldSurface)
				{
					int num3 = (int)vector.X;
					int num4 = (int)vector.Y;
					if (Main.tile[num3, num4].wall == 0 && !Main.tile[num3, num4].active && Main.tile[num3, num4 - 3].wall == 0 && !Main.tile[num3, num4 - 3].active && Main.tile[num3, num4 - 1].wall == 0 && !Main.tile[num3, num4 - 1].active && Main.tile[num3, num4 - 4].wall == 0 && !Main.tile[num3, num4 - 4].active && Main.tile[num3, num4 - 2].wall == 0 && !Main.tile[num3, num4 - 2].active && Main.tile[num3, num4 - 5].wall == 0 && !Main.tile[num3, num4 - 5].active)
					{
						flag = false;
					}
				}
				JungleX = (int)vector.X;
				num += (float)genRand.Next(-20, 21) * 0.1f;
				if (num < 5f)
				{
					num = 5f;
				}
				else if (num > 10f)
				{
					num = 10f;
				}
				int num5 = (int)(vector.X - num * 0.5f);
				int num6 = (int)(vector.X + num * 0.5f);
				int num7 = (int)(vector.Y - num * 0.5f);
				int num8 = (int)(vector.Y + num * 0.5f);
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
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if (Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < num * 0.5f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							KillTile(k, l);
						}
					}
				}
				num2++;
				if (num2 > 10 && genRand.Next(50) < num2)
				{
					num2 = 0;
					int num9 = -2;
					if (genRand.Next(2) == 0)
					{
						num9 = 2;
					}
					TileRunner((int)vector.X, (int)vector.Y, genRand.Next(3, 20), genRand.Next(10, 100), -1, addTile: false, num9);
				}
				vector += vector2;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.01f;
				if (vector2.Y > 0f)
				{
					vector2.Y = 0f;
				}
				else if (vector2.Y < -2f)
				{
					vector2.Y = -2f;
				}
				vector2.X += (float)genRand.Next(-10, 11) * 0.1f;
				if (vector.X < (float)(i - 200))
				{
					vector2.X += (float)genRand.Next(5, 21) * 0.1f;
				}
				else if (vector.X > (float)(i + 200))
				{
					vector2.X -= (float)genRand.Next(5, 21) * 0.1f;
				}
				if (vector2.X > 1.5f)
				{
					vector2.X = 1.5f;
				}
				else if (vector2.X < -1.5f)
				{
					vector2.X = -1.5f;
				}
			}
		}

		public static void GERunner(int i, int j, float speedX = 0f, float speedY = 0f, bool good = true)
		{
			if (Codable.RunGlobalMethod("ModWorld", "GERunner", i, j, speedX, speedY, good) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			int num = genRand.Next(200, 250) * Main.maxTilesX / 4200;
			int num2 = num;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-10, 11) * 0.1f;
			if (speedX != 0f || speedY != 0f)
			{
				vector2.X = speedX;
				vector2.Y = speedY;
			}
			bool flag = true;
			while (flag)
			{
				int num3 = (int)(vector.X - (float)num2 * 0.5f);
				int num4 = (int)(vector.X + (float)num2 * 0.5f);
				int num5 = (int)(vector.Y - (float)num2 * 0.5f);
				int num6 = (int)(vector.Y + (float)num2 * 0.5f);
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
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if (!(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < (float)num * 0.5f * (1f + (float)genRand.Next(-10, 11) * 0.015f)))
						{
							continue;
						}
						if (good)
						{
							if (Main.tile[k, l].wall == 3)
							{
								Main.tile[k, l].wall = 28;
							}
							if (Main.tile[k, l].type == 2)
							{
								Main.tile[k, l].type = 109;
								SquareTileFrame(k, l);
							}
							else if (Main.tile[k, l].type == 1)
							{
								Main.tile[k, l].type = 117;
								SquareTileFrame(k, l);
							}
							else if (Main.tile[k, l].type == 53 || Main.tile[k, l].type == 123)
							{
								Main.tile[k, l].type = 116;
								SquareTileFrame(k, l);
							}
							else if (Main.tile[k, l].type == 23)
							{
								Main.tile[k, l].type = 109;
								SquareTileFrame(k, l);
							}
							else if (Main.tile[k, l].type == 25)
							{
								Main.tile[k, l].type = 117;
								SquareTileFrame(k, l);
							}
							else if (Main.tile[k, l].type == 112)
							{
								Main.tile[k, l].type = 116;
								SquareTileFrame(k, l);
							}
						}
						else if (Main.tile[k, l].type == 2)
						{
							Main.tile[k, l].type = 23;
							SquareTileFrame(k, l);
						}
						else if (Main.tile[k, l].type == 1)
						{
							Main.tile[k, l].type = 25;
							SquareTileFrame(k, l);
						}
						else if (Main.tile[k, l].type == 53 || Main.tile[k, l].type == 123)
						{
							Main.tile[k, l].type = 112;
							SquareTileFrame(k, l);
						}
						else if (Main.tile[k, l].type == 109)
						{
							Main.tile[k, l].type = 23;
							SquareTileFrame(k, l);
						}
						else if (Main.tile[k, l].type == 117)
						{
							Main.tile[k, l].type = 25;
							SquareTileFrame(k, l);
						}
						else if (Main.tile[k, l].type == 116)
						{
							Main.tile[k, l].type = 112;
							SquareTileFrame(k, l);
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > speedX + 1f)
				{
					vector2.X = speedX + 1f;
				}
				else if (vector2.X < speedX - 1f)
				{
					vector2.X = speedX - 1f;
				}
				if (vector.X < (float)(-num) || vector.Y < (float)(-num) || vector.X > (float)(Main.maxTilesX + num) || vector.Y > (float)(Main.maxTilesX + num))
				{
					flag = false;
				}
			}
		}

		public static void TileRunner(int i, int j, double DStr, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool overRide = true)
		{
			if (TMod.RunMethod(TMod.WorldHooks.TileRunner, i, j, DStr, steps, type, addTile, speedX, speedY, noYChange, overRide) && !TMod.GetContinueMethod())
			{
				return;
			}
			float num = (float)DStr;
			float num2 = num;
			int num3 = steps;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			if (speedX != 0f || speedY != 0f)
			{
				vector2.X = speedX;
				vector2.Y = speedY;
			}
			else
			{
				vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
				vector2.Y = (float)genRand.Next(-10, 11) * 0.1f;
			}
			float num4 = 1f / (float)steps;
			float num5 = num * 0.5f;
			Tile tile = null;
			while (num2 > 0f && num3 > 0)
			{
				if (vector.Y < 0f && num3 > 0 && type == 59)
				{
					num3 = 0;
				}
				num2 = num * (float)num3 * num4;
				num3--;
				int num6 = (int)(vector.X - num2 * 0.5f);
				int num7 = num6 + (int)num2;
				int num8 = (int)(vector.Y - num2 * 0.5f);
				int num9 = num8 + (int)num2;
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
				for (int k = num6; k < num7; k++)
				{
					for (int l = num8; l < num9; l++)
					{
						if (!(Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < num5 * (1f + (float)genRand.Next(-10, 11) * 0.015f)))
						{
							continue;
						}
						if (mudWall && (double)l > Main.worldSurface && (double)l < Main.hellLayer - 10.0 - (double)genRand.Next(3))
						{
							PlaceWall(k, l, 15, mute: true);
						}
						tile = Main.tile[k, l];
						if (type < 0)
						{
							if (type == -2 && tile.active && (l < waterLine || l > lavaLine))
							{
								tile.liquid = byte.MaxValue;
								if (l > lavaLine)
								{
									tile.lava = true;
								}
							}
							tile.active = false;
						}
						else if ((overRide || !tile.active) && (type != 40 || tile.type != 53) && (!Main.tileStone[type] || tile.type == 1) && tile.type != 45 && tile.type != 147 && (tile.type != 1 || type != 59 || (double)l >= Main.worldSurface + (double)genRand.Next(-50, 50)))
						{
							if (tile.type != 53 || (double)l >= Main.worldSurface)
							{
								tile.type = (ushort)type;
							}
							else if (type == 59)
							{
								tile.type = (ushort)type;
							}
						}
						if (addTile)
						{
							tile.active = true;
							tile.liquid = 0;
							tile.lava = false;
						}
						if (noYChange && (double)l < Main.worldSurface && type != 59)
						{
							tile.wall = 2;
						}
						if (type == 59 && l > waterLine && tile.liquid > 0)
						{
							tile.lava = false;
							tile.liquid = 0;
						}
					}
				}
				vector += vector2;
				if (num2 > 50f)
				{
					int num10 = (int)Math.Min(num2, 300f) / 50;
					num3 -= num10;
					for (int m = 0; m < num10; m++)
					{
						vector += vector2;
						vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
						vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
					}
					if (num2 > 400f)
					{
						num10 = (int)((Math.Min(num2, 900f) - 300f) / 100f);
						num3 -= num10;
						for (int n = 0; n < num10; n++)
						{
							vector += vector2;
							vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
							vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
						}
					}
				}
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 1f)
				{
					vector2.X = 1f;
				}
				else if (vector2.X < -1f)
				{
					vector2.X = -1f;
				}
				if (!noYChange)
				{
					vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
					if (vector2.Y > 1f)
					{
						vector2.Y = 1f;
					}
					else if (vector2.Y < -1f)
					{
						vector2.Y = -1f;
					}
					if (type == 59)
					{
						if ((double)vector.Y < Main.rockLayer + 100.0)
						{
							vector2.Y = 1f;
						}
						else if ((double)vector.Y > Main.hellLayer - 100.0)
						{
							vector2.Y = -1f;
						}
						else if (vector2.Y > 0.5f)
						{
							vector2.Y = 0.5f;
						}
						else if (vector2.Y < -0.5f)
						{
							vector2.Y = -0.5f;
						}
					}
				}
				else if (type != 59 && num2 < 3f)
				{
					if (vector2.Y > 1f)
					{
						vector2.Y = 1f;
					}
					else if (vector2.Y < -1f)
					{
						vector2.Y = -1f;
					}
				}
			}
		}

		public static void MudWallRunner(int i, int j)
		{
			if (TMod.RunMethod(TMod.WorldHooks.MudWallRunner, i, j) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(5, 15);
			int num2 = genRand.Next(5, 20);
			int num3 = num2;
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-10, 11) * 0.1f;
			while (num > 0 && num3 > 0)
			{
				int num4 = num * num3 / num2;
				num3--;
				int num5 = (int)(vector.X - (float)num4 * 0.5f);
				int num6 = (int)(vector.X + (float)num4 * 0.5f);
				int num7 = (int)(vector.Y - (float)num4 * 0.5f);
				int num8 = (int)(vector.Y + (float)num4 * 0.5f);
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
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if (Math.Abs((float)k - vector.X) + Math.Abs((float)l - vector.Y) < (float)num * 0.5f * (1f + (float)genRand.Next(-10, 11) * 0.015f))
						{
							Main.tile[k, l].wall = 0;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 1f)
				{
					vector2.X = 1f;
				}
				else if (vector2.X < -1f)
				{
					vector2.X = -1f;
				}
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.Y > 1f)
				{
					vector2.Y = 1f;
				}
				else if (vector2.Y < -1f)
				{
					vector2.Y = -1f;
				}
			}
		}

		public static void FloatingIsland(int i, int j)
		{
			if (TMod.RunMethod(TMod.WorldHooks.FloatingIsland, i, j) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(80, 120);
			int num2 = genRand.Next(20, 25);
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-20, 21) * 0.2f;
			while (vector2.X > -2f && vector2.X < 2f)
			{
				vector2.X = (float)genRand.Next(-20, 21) * 0.2f;
			}
			vector2.Y = (float)genRand.Next(-20, -10) * 0.02f;
			while (num > 0 && num2 > 0)
			{
				num -= genRand.Next(4);
				num2--;
				int num3 = (int)(vector.X - (float)num * 0.5f);
				int num4 = (int)(vector.X + (float)num * 0.5f);
				int num5 = (int)(vector.Y - (float)num * 0.5f);
				int num6 = (int)(vector.Y + (float)num * 0.5f);
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
				float num7 = (float)(num * genRand.Next(80, 120)) * 0.01f;
				float num8 = vector.Y + 1f;
				for (int k = num3; k < num4; k++)
				{
					if (genRand.Next(2) == 0)
					{
						num8 += (float)genRand.Next(-1, 2);
					}
					if (num8 < vector.Y)
					{
						num8 = vector.Y;
					}
					if (num8 > vector.Y + 2f)
					{
						num8 = vector.Y + 2f;
					}
					for (int l = num5; l < num6; l++)
					{
						if (!((float)l > num8))
						{
							continue;
						}
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y) * 2f;
						float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
						if (num11 < num7 * 0.4f)
						{
							Main.tile[k, l].active = true;
							if (Main.tile[k, l].type == 59)
							{
								Main.tile[k, l].type = 0;
							}
						}
					}
				}
				TileRunner(genRand.Next(num3 + 10, num4 - 10), (int)(vector.Y + num7 * 0.1f + 5f), genRand.Next(5, 10), genRand.Next(10, 15), 0, addTile: true, 0f, 2f, noYChange: true);
				num3 = (int)(vector.X - (float)num * 0.4f);
				num4 = (int)(vector.X + (float)num * 0.4f);
				num5 = (int)(vector.Y - (float)num * 0.4f);
				num6 = (int)(vector.Y + (float)num * 0.4f);
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
				num7 = (float)(num * genRand.Next(80, 120)) * 0.01f;
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if ((float)n > vector.Y + 2f)
						{
							float num12 = Math.Abs((float)m - vector.X);
							float num13 = Math.Abs((float)n - vector.Y) * 2f;
							float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
							if (num14 < num7 * 0.4f)
							{
								Main.tile[m, n].wall = 2;
							}
						}
					}
				}
				vector += vector2;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 1f)
				{
					vector2.X = 1f;
				}
				else if (vector2.X < -1f)
				{
					vector2.X = -1f;
				}
				if (vector2.Y > 0.2f)
				{
					vector2.Y = -0.2f;
				}
				else if (vector2.Y < -0.2f)
				{
					vector2.Y = -0.2f;
				}
			}
		}

		public static void Caverer(int X, int Y)
		{
			if (TMod.RunMethod(TMod.WorldHooks.Caverer, X, Y) && !TMod.GetContinueMethod())
			{
				return;
			}
			switch (genRand.Next(2))
			{
			case 0:
			{
				int num4 = genRand.Next(7, 9);
				float num5 = (float)genRand.Next(100) * 0.01f;
				float num6 = 1f - num5;
				if (genRand.Next(2) == 0)
				{
					num5 = 0f - num5;
				}
				if (genRand.Next(2) == 0)
				{
					num6 = 0f - num6;
				}
				Vector2 vector2 = new Vector2(X, Y);
				for (int j = 0; j < num4; j++)
				{
					vector2 = digTunnel(vector2.X, vector2.Y, num5, num6, genRand.Next(6, 20), genRand.Next(4, 9));
					num5 += (float)genRand.Next(-20, 21) * 0.1f;
					num6 += (float)genRand.Next(-20, 21) * 0.1f;
					if (num5 < -1.5f)
					{
						num5 = -1.5f;
					}
					else if ((double)num5 > 1.5)
					{
						num5 = 1.5f;
					}
					if (num6 < -1.5f)
					{
						num6 = -1.5f;
					}
					else if (num6 > 1.5f)
					{
						num6 = 1.5f;
					}
					float num7 = (float)genRand.Next(100) * 0.01f;
					float num8 = 1f - num7;
					if (genRand.Next(2) == 0)
					{
						num7 = 0f - num7;
					}
					if (genRand.Next(2) == 0)
					{
						num8 = 0f - num8;
					}
					Vector2 vector3 = digTunnel(vector2.X, vector2.Y, num7, num8, genRand.Next(30, 50), genRand.Next(3, 6));
					TileRunner((int)vector3.X, (int)vector3.Y, genRand.Next(10, 20), genRand.Next(5, 10), -1);
				}
				break;
			}
			case 1:
			{
				int num = genRand.Next(15, 30);
				float num2 = (float)genRand.Next(100) * 0.01f;
				float num3 = 1f - num2;
				if (genRand.Next(2) == 0)
				{
					num2 = 0f - num2;
				}
				if (genRand.Next(2) == 0)
				{
					num3 = 0f - num3;
				}
				Vector2 vector = new Vector2(X, Y);
				for (int i = 0; i < num; i++)
				{
					vector = digTunnel(vector.X, vector.Y, num2, num3, genRand.Next(5, 15), genRand.Next(2, 6), Wet: true);
					num2 += (float)genRand.Next(-20, 21) * 0.1f;
					num3 += (float)genRand.Next(-20, 21) * 0.1f;
					if (num2 < -1.5f)
					{
						num2 = -1.5f;
					}
					else if (num2 > 1.5f)
					{
						num2 = 1.5f;
					}
					if (num3 < -1.5f)
					{
						num3 = -1.5f;
					}
					else if (num3 > 1.5f)
					{
						num3 = 1.5f;
					}
				}
				break;
			}
			}
		}

		public static Vector2 digTunnel(float X, float Y, float xDir, float yDir, int Steps, int Size, bool Wet = false)
		{
			float num = X;
			float num2 = Y;
			try
			{
				float num3 = 0f;
				float num4 = 0f;
				float num5 = Size;
				for (int i = 0; i < Steps; i++)
				{
					for (int j = (int)(num - num5); (float)j <= num + num5; j++)
					{
						for (int k = (int)(num2 - num5); (float)k <= num2 + num5; k++)
						{
							if (Math.Abs((float)j - num) + Math.Abs((float)k - num2) < num5 * (1f + (float)genRand.Next(-10, 11) * 0.005f))
							{
								Main.tile[j, k].active = false;
								if (Wet)
								{
									Main.tile[j, k].liquid = byte.MaxValue;
								}
							}
						}
					}
					num5 += (float)genRand.Next(-50, 51) * 0.03f;
					if (num5 < (float)Size * 0.6f)
					{
						num5 = (float)Size * 0.6f;
					}
					else if (num5 > (float)(Size * 2))
					{
						num5 = Size * 2;
					}
					num3 += (float)genRand.Next(-20, 21) * 0.01f;
					num4 += (float)genRand.Next(-20, 21) * 0.01f;
					if (num3 < -1f)
					{
						num3 = -1f;
					}
					else if (num3 > 1f)
					{
						num3 = 1f;
					}
					if (num4 < -1f)
					{
						num4 = -1f;
					}
					else if (num4 > 1f)
					{
						num4 = 1f;
					}
					num += (xDir + num3) * 0.6f;
					num2 += (yDir + num4) * 0.6f;
				}
			}
			catch
			{
			}
			return new Vector2(num, num2);
		}

		public static void IslandHouse(int i, int j)
		{
			if (TMod.RunMethod(TMod.WorldHooks.IslandHouse, i, j) && !TMod.GetContinueMethod())
			{
				return;
			}
			byte type = (byte)genRand.Next(45, 48);
			byte wall = (byte)genRand.Next(10, 13);
			Vector2 vector = new Vector2(i, j);
			int num = 1;
			if (genRand.Next(2) == 0)
			{
				num = -1;
			}
			int num2 = genRand.Next(7, 12);
			int num3 = genRand.Next(5, 7);
			vector.X = i + (num2 + 2) * num;
			for (int k = j - 15; k < j + 30; k++)
			{
				if (Main.tile[(int)vector.X, k].active)
				{
					vector.Y = k - 1;
					break;
				}
			}
			vector.X = i;
			int num4 = (int)(vector.X - (float)num2 - 2f);
			int num5 = (int)(vector.X + (float)num2 + 2f);
			int num6 = (int)(vector.Y - (float)num3 - 2f);
			int num7 = (int)(vector.Y + 2f + (float)genRand.Next(3, 5));
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int l = num4; l <= num5; l++)
			{
				for (int m = num6; m < num7; m++)
				{
					Main.tile[l, m].active = true;
					Main.tile[l, m].type = type;
					Main.tile[l, m].wall = 0;
				}
			}
			num4 = (int)(vector.X - (float)num2);
			num5 = (int)(vector.X + (float)num2);
			num6 = (int)(vector.Y - (float)num3);
			num7 = (int)(vector.Y + 1f);
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int n = num4; n <= num5; n++)
			{
				for (int num8 = num6; num8 < num7; num8++)
				{
					if (Main.tile[n, num8].wall == 0)
					{
						Main.tile[n, num8].active = false;
						Main.tile[n, num8].wall = wall;
					}
				}
			}
			int num9 = i + (num2 + 1) * num;
			int num10 = (int)vector.Y;
			for (int num11 = num9 - 2; num11 <= num9 + 2; num11++)
			{
				Main.tile[num11, num10].active = false;
				Main.tile[num11, num10 - 1].active = false;
				Main.tile[num11, num10 - 2].active = false;
			}
			PlaceTile(num9, num10, 10, mute: true);
			int contain = 0;
			int num12 = houseCount;
			if (num12 > 2)
			{
				num12 = genRand.Next(3);
			}
			switch (num12)
			{
			case 0:
				contain = 159;
				break;
			case 1:
				contain = 65;
				break;
			case 2:
				contain = 158;
				break;
			}
			AddBuriedChest(i, num10 - 3, contain, notNearOtherChests: false, 2);
			houseCount++;
		}

		public static void Mountinater(int i, int j)
		{
			if (TMod.RunMethod(TMod.WorldHooks.Mountinater, i, j) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(80, 120);
			int num2 = genRand.Next(40, 55);
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j + (float)num2 * 0.5f;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-20, -10) * 0.1f;
			while (num > 0 && num2 > 0)
			{
				num -= genRand.Next(4);
				num2--;
				int num3 = (int)(vector.X - (float)num * 0.5f);
				int num4 = (int)(vector.X + (float)num * 0.5f);
				int num5 = (int)(vector.Y - (float)num * 0.5f);
				int num6 = (int)(vector.Y + (float)num * 0.5f);
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
				float num7 = (float)(num * genRand.Next(80, 120)) * 0.01f;
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						float num8 = Math.Abs((float)k - vector.X);
						float num9 = Math.Abs((float)l - vector.Y);
						float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
						if (num10 < num7 * 0.4f && !Main.tile[k, l].active)
						{
							Main.tile[k, l].active = true;
							Main.tile[k, l].type = 0;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 0.5f)
				{
					vector2.X = 0.5f;
				}
				else if (vector2.X < -0.5f)
				{
					vector2.X = -0.5f;
				}
				if (vector2.Y > -0.5f)
				{
					vector2.Y = -0.5f;
				}
				else if (vector2.Y < -1.5f)
				{
					vector2.Y = -1.5f;
				}
			}
		}

		public static void Lakinater(int i, int j)
		{
			if (Codable.RunGlobalMethod("ModWorld", "Lakinater", i, j) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			float num = genRand.Next(25, 50);
			float num2 = num;
			float num3 = genRand.Next(30, 80);
			if (genRand.Next(5) == 0)
			{
				num *= 1.5f;
				num2 *= 1.5f;
				num3 *= 1.2f;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j - num3 * 0.3f;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-20, -10) * 0.1f;
			while (num > 0f && num3 > 0f)
			{
				if ((double)(vector.Y + num2 * 0.5f) > Main.worldSurface)
				{
					num3 = 0f;
				}
				num -= (float)genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)(vector.X - num * 0.5f);
				int num5 = (int)(vector.X + num * 0.5f);
				int num6 = (int)(vector.Y - num * 0.5f);
				int num7 = (int)(vector.Y + num * 0.5f);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (float)genRand.Next(80, 120) * 0.01f;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - vector.X);
						float num9 = Math.Abs((float)l - vector.Y);
						float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
						if (num10 < num2 * 0.4f)
						{
							if (Main.tile[k, l].active)
							{
								Main.tile[k, l].liquid = byte.MaxValue;
							}
							Main.tile[k, l].active = false;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > 0.5f)
				{
					vector2.X = 0.5f;
				}
				else if (vector2.X < -0.5f)
				{
					vector2.X = -0.5f;
				}
				if (vector2.Y > 1.5f)
				{
					vector2.Y = 1.5f;
				}
				else if (vector2.Y < 0.5f)
				{
					vector2.Y = 0.5f;
				}
			}
		}

		public static void ShroomPatch(int i, int j)
		{
			if (Codable.RunGlobalMethod("ModWorld", "ShroomPatch", i, j) && !(bool)Codable.customMethodReturn)
			{
				return;
			}
			float num = genRand.Next(40, 70);
			float num2 = num;
			float num3 = genRand.Next(20, 30);
			if (genRand.Next(5) == 0)
			{
				num *= 1.5f;
				num2 *= 1.5f;
				num3 *= 1.2f;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = (float)j - num3 * 0.3f;
			Vector2 vector2 = default(Vector2);
			vector2.X = (float)genRand.Next(-10, 11) * 0.1f;
			vector2.Y = (float)genRand.Next(-20, -10) * 0.1f;
			while ((double)num > 0.0 && num3 > 0f)
			{
				num -= (float)genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)(vector.X - num * 0.5f);
				int num5 = num4 + (int)num;
				int num6 = (int)(vector.Y - num * 0.5f);
				int num7 = num6 + (int)num;
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (float)genRand.Next(80, 120) * 0.01f;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - vector.X);
						float num9 = Math.Abs(((float)l - vector.Y) * 2.3f);
						float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
						if (!(num10 < num2 * 0.4f))
						{
							continue;
						}
						if ((float)l < vector.Y + num2 * 0.02f)
						{
							if (Main.tile[k, l].type != 59)
							{
								Main.tile[k, l].active = false;
							}
						}
						else
						{
							Main.tile[k, l].type = 59;
						}
						Main.tile[k, l].liquid = 0;
						Main.tile[k, l].lava = false;
					}
				}
				vector += vector2;
				vector.X += vector2.X;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector2.Y -= (float)genRand.Next(11) * 0.05f;
				if (vector2.X > -0.5f && vector2.X < 0.5f)
				{
					if (vector2.X < 0f)
					{
						vector2.X = -0.5f;
					}
					else
					{
						vector2.X = 0.5f;
					}
				}
				if (vector2.X > 2f)
				{
					vector2.X = 1f;
				}
				else if (vector2.X < -2f)
				{
					vector2.X = -1f;
				}
				if (vector2.Y > 1f)
				{
					vector2.Y = 1f;
				}
				else if (vector2.Y < -1f)
				{
					vector2.Y = -1f;
				}
				for (int m = 0; m < 2; m++)
				{
					int num11 = (int)vector.X + genRand.Next(-20, 20);
					int num12 = (int)vector.Y + genRand.Next(0, 20);
					while (!Main.tile[num11, num12].active && Main.tile[num11, num12].type != 59)
					{
						num11 = (int)vector.X + genRand.Next(-20, 20);
						num12 = (int)vector.Y + genRand.Next(0, 20);
					}
					int num13 = genRand.Next(7, 10);
					int num14 = genRand.Next(7, 10);
					TileRunner(num11, num12, num13, num14, 59, addTile: false, 0f, 2f, noYChange: true);
					if (genRand.Next(3) == 0)
					{
						TileRunner(num11, num12, num13 - 3, num14 - 3, -1, addTile: false, 0f, 2f, noYChange: true);
					}
				}
			}
		}

		public static void Cavinator(int i, int j, int steps)
		{
			if (TMod.RunMethod(TMod.WorldHooks.Cavinator, i, j, steps) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(7, 15);
			int num2 = 1;
			if (genRand.Next(2) == 0)
			{
				num2 = -1;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num3 = genRand.Next(20, 40);
			Vector2 vector2 = default(Vector2);
			vector2.Y = (float)genRand.Next(10, 20) * 0.01f;
			vector2.X = num2;
			while (num3 > 0)
			{
				num3--;
				int num4 = (int)(vector.X - (float)num * 0.5f);
				int num5 = (int)(vector.X + (float)num * 0.5f);
				int num6 = (int)(vector.Y - (float)num * 0.5f);
				int num7 = (int)(vector.Y + (float)num * 0.5f);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				float num8 = (float)(num * genRand.Next(80, 120)) * 0.01f;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y);
						float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
						if ((double)num11 < (double)num8 * 0.4)
						{
							Main.tile[k, l].active = false;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > (float)num2 + 0.5f)
				{
					vector2.X = (float)num2 + 0.5f;
				}
				if (vector2.X < (float)num2 - 0.5f)
				{
					vector2.X = (float)num2 - 0.5f;
				}
				if (vector2.Y > 2f)
				{
					vector2.Y = 2f;
				}
				else if (vector2.Y < 0f)
				{
					vector2.Y = 0f;
				}
			}
			if (steps > 0 && (double)(int)vector.Y < Main.rockLayer + 50.0)
			{
				Cavinator((int)vector.X, (int)vector.Y, steps - 1);
			}
		}

		public static void CaveOpenater(int i, int j)
		{
			if (TMod.RunMethod(TMod.WorldHooks.CaveOpenator, i, j) && !TMod.GetContinueMethod())
			{
				return;
			}
			int num = genRand.Next(7, 12);
			int num2 = 1;
			if (genRand.Next(2) == 0)
			{
				num2 = -1;
			}
			Vector2 vector = default(Vector2);
			vector.X = i;
			vector.Y = j;
			int num3 = 100;
			Vector2 vector2 = default(Vector2);
			vector2.X = num2;
			vector2.Y = 0f;
			while (num3 > 0)
			{
				if (Main.tile[(int)vector.X, (int)vector.Y].wall == 0)
				{
					num3 = 0;
				}
				num3--;
				int num4 = (int)(vector.X - (float)num * 0.5f);
				int num5 = (int)(vector.X + (float)num * 0.5f);
				int num6 = (int)(vector.Y - (float)num * 0.5f);
				int num7 = (int)(vector.Y + (float)num * 0.5f);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				float num8 = (float)(num * genRand.Next(80, 120)) * 0.01f;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num9 = Math.Abs((float)k - vector.X);
						float num10 = Math.Abs((float)l - vector.Y);
						float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
						if (num11 < num8 * 0.4f)
						{
							Main.tile[k, l].active = false;
						}
					}
				}
				vector += vector2;
				vector2.X += (float)genRand.Next(-10, 11) * 0.05f;
				vector2.Y += (float)genRand.Next(-10, 11) * 0.05f;
				if (vector2.X > (float)num2 + 0.5f)
				{
					vector2.X = (float)num2 + 0.5f;
				}
				else if (vector2.X < (float)num2 - 0.5f)
				{
					vector2.X = (float)num2 - 0.5f;
				}
				if (vector2.Y > 0f)
				{
					vector2.Y = 0f;
				}
				else if (vector2.Y < -0.5f)
				{
					vector2.Y = -0.5f;
				}
			}
		}

		public static void SquareTileFrame(int i, int j, bool resetFrame = true)
		{
			TileFrame(i - 1, j - 1);
			TileFrame(i - 1, j);
			TileFrame(i - 1, j + 1);
			TileFrame(i, j - 1);
			TileFrame(i, j, resetFrame);
			TileFrame(i, j + 1);
			TileFrame(i + 1, j - 1);
			TileFrame(i + 1, j);
			TileFrame(i + 1, j + 1);
		}

		public static void SquareWallFrame(int i, int j, bool resetFrame = true)
		{
			WallFrame(i - 1, j - 1);
			WallFrame(i - 1, j);
			WallFrame(i - 1, j + 1);
			WallFrame(i, j - 1);
			WallFrame(i, j, resetFrame);
			WallFrame(i, j + 1);
			WallFrame(i + 1, j - 1);
			WallFrame(i + 1, j);
			WallFrame(i + 1, j + 1);
		}

		public static void SectionTileFrame(int startX, int startY, int endX, int endY)
		{
			int num = startX * 200;
			int num2 = (endX + 1) * 200;
			int num3 = startY * 150;
			int num4 = (endY + 1) * 150;
			if (num < 1)
			{
				num = 1;
			}
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num > Main.maxTilesX - 2)
			{
				num = Main.maxTilesX - 2;
			}
			if (num3 > Main.maxTilesY - 2)
			{
				num3 = Main.maxTilesY - 2;
			}
			for (int i = num - 1; i < num2 + 1; i++)
			{
				for (int j = num3 - 1; j < num4 + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					TileFrame(i, j, resetFrame: true, noBreak: true);
					WallFrame(i, j, resetFrame: true);
				}
			}
		}

		public static void RangeFrame(int startX, int startY, int endX, int endY)
		{
			int num = endX + 1;
			int num2 = endY + 1;
			for (int i = startX - 1; i < num + 1; i++)
			{
				for (int j = startY - 1; j < num2 + 1; j++)
				{
					TileFrame(i, j);
					WallFrame(i, j);
				}
			}
		}

		public static void WaterCheck()
		{
			Liquid.numLiquid = 0;
			LiquidBuffer.numLiquidBuffer = 0;
			for (int i = 1; i < Main.maxTilesX - 1; i++)
			{
				for (int num = Main.maxTilesY - 2; num > 0; num--)
				{
					Main.tile[i, num].checkingLiquid = false;
					if (Main.tile[i, num].liquid > 0 && Main.tile[i, num].active && Main.tileSolid[Main.tile[i, num].type] && !Main.tileSolidTop[Main.tile[i, num].type])
					{
						Main.tile[i, num].liquid = 0;
					}
					else if (Main.tile[i, num].liquid > 0)
					{
						if (Main.tile[i, num].active)
						{
							if (Main.tileWaterDeath[Main.tile[i, num].type] && (Main.tile[i, num].type != 4 || Main.tile[i, num].frameY != 176))
							{
								KillTile(i, num);
							}
							if (Main.tile[i, num].lava && Main.tileLavaDeath[Main.tile[i, num].type])
							{
								KillTile(i, num);
							}
						}
						if ((!Main.tile[i, num + 1].active || !Main.tileSolid[Main.tile[i, num + 1].type] || Main.tileSolidTop[Main.tile[i, num + 1].type]) && Main.tile[i, num + 1].liquid < byte.MaxValue)
						{
							if (Main.tile[i, num + 1].liquid > 250)
							{
								Main.tile[i, num + 1].liquid = byte.MaxValue;
							}
							else
							{
								Liquid.AddWater(i, num);
							}
						}
						if ((!Main.tile[i - 1, num].active || !Main.tileSolid[Main.tile[i - 1, num].type] || Main.tileSolidTop[Main.tile[i - 1, num].type]) && Main.tile[i - 1, num].liquid != Main.tile[i, num].liquid)
						{
							Liquid.AddWater(i, num);
						}
						else if ((!Main.tile[i + 1, num].active || !Main.tileSolid[Main.tile[i + 1, num].type] || Main.tileSolidTop[Main.tile[i + 1, num].type]) && Main.tile[i + 1, num].liquid != Main.tile[i, num].liquid)
						{
							Liquid.AddWater(i, num);
						}
						if (Main.tile[i, num].lava)
						{
							if (Main.tile[i - 1, num].liquid > 0 && !Main.tile[i - 1, num].lava)
							{
								Liquid.AddWater(i, num);
							}
							else if (Main.tile[i + 1, num].liquid > 0 && !Main.tile[i + 1, num].lava)
							{
								Liquid.AddWater(i, num);
							}
							else if (Main.tile[i, num - 1].liquid > 0 && !Main.tile[i, num - 1].lava)
							{
								Liquid.AddWater(i, num);
							}
							else if (Main.tile[i, num + 1].liquid > 0 && !Main.tile[i, num + 1].lava)
							{
								Liquid.AddWater(i, num);
							}
						}
					}
				}
			}
		}

		public static void EveryTileFrame()
		{
			noLiquidCheck = true;
			noTileActions = true;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[55] + " " + (int)(num * 100f + 1f) + "%";
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					if (Main.tile[i, j].active)
					{
						TileFrame(i, j, resetFrame: true);
					}
					if (Main.tile[i, j].wall > 0)
					{
						WallFrame(i, j, resetFrame: true);
					}
				}
			}
			noLiquidCheck = false;
			noTileActions = false;
		}

		public static void PlantCheck(int i, int j)
		{
			int num = -1;
			int num2 = Main.tile[i, j].type;
			if (j + 1 >= Main.maxTilesY)
			{
				num = num2;
			}
			if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
			{
				num = Main.tile[i, j + 1].type;
			}
			if ((num2 != 3 || num == 2 || num == 78) && (num2 != 24 || num == 23) && (num2 != 61 || num == 60) && (num2 != 71 || num == 70) && (num2 != 73 || num == 2 || num == 78) && (num2 != 74 || num == 60) && (num2 != 110 || num == 109) && (num2 != 113 || num == 109))
			{
				return;
			}
			switch (num)
			{
			case 23:
				num2 = 24;
				if (Main.tile[i, j].frameX >= 162)
				{
					Main.tile[i, j].frameX = 126;
				}
				break;
			case 2:
				num2 = ((num2 != 113) ? 3 : 73);
				break;
			case 109:
				num2 = ((num2 != 73) ? 110 : 113);
				break;
			}
			if (num2 != Main.tile[i, j].type)
			{
				Main.tile[i, j].type = (ushort)num2;
			}
			else
			{
				KillTile(i, j);
			}
		}

		public static void WallFrame(int i, int j, bool resetFrame = false)
		{
			if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY || Main.tile[i, j] == null || Main.tile[i, j].wall <= 0)
			{
				return;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			int wall = Main.tile[i, j].wall;
			if (wall == 0)
			{
				return;
			}
			Rectangle rectangle = default(Rectangle);
			rectangle.X = -1;
			rectangle.Y = -1;
			if (i - 1 < 0)
			{
				num = wall;
				num4 = wall;
				num6 = wall;
			}
			if (i + 1 >= Main.maxTilesX)
			{
				num3 = wall;
				num5 = wall;
				num8 = wall;
			}
			if (j - 1 < 0)
			{
				num = wall;
				num2 = wall;
				num3 = wall;
			}
			if (j + 1 >= Main.maxTilesY)
			{
				num6 = wall;
				num7 = wall;
				num8 = wall;
			}
			if (i - 1 >= 0 && Main.tile[i - 1, j] != null)
			{
				num4 = Main.tile[i - 1, j].wall;
			}
			if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null)
			{
				num5 = Main.tile[i + 1, j].wall;
			}
			if (j - 1 >= 0 && Main.tile[i, j - 1] != null)
			{
				num2 = Main.tile[i, j - 1].wall;
			}
			if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null)
			{
				num7 = Main.tile[i, j + 1].wall;
			}
			if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null)
			{
				num = Main.tile[i - 1, j - 1].wall;
			}
			if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null)
			{
				num3 = Main.tile[i + 1, j - 1].wall;
			}
			if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null)
			{
				num6 = Main.tile[i - 1, j + 1].wall;
			}
			if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null)
			{
				num8 = Main.tile[i + 1, j + 1].wall;
			}
			if (wall == 2)
			{
				if (j == (int)Main.worldSurface)
				{
					num7 = wall;
					num6 = wall;
					num8 = wall;
				}
				else if (j >= (int)Main.worldSurface)
				{
					num7 = wall;
					num6 = wall;
					num8 = wall;
					num2 = wall;
					num = wall;
					num3 = wall;
					num4 = wall;
					num5 = wall;
				}
			}
			if (num7 > 0)
			{
				num7 = wall;
			}
			if (num6 > 0)
			{
				num6 = wall;
			}
			if (num8 > 0)
			{
				num8 = wall;
			}
			if (num2 > 0)
			{
				num2 = wall;
			}
			if (num > 0)
			{
				num = wall;
			}
			if (num3 > 0)
			{
				num3 = wall;
			}
			if (num4 > 0)
			{
				num4 = wall;
			}
			if (num5 > 0)
			{
				num5 = wall;
			}
			int num9 = 0;
			if (resetFrame)
			{
				num9 = genRand.Next(0, 3);
				Main.tile[i, j].wallFrameNumber = (byte)num9;
			}
			else
			{
				num9 = Main.tile[i, j].wallFrameNumber;
			}
			if (rectangle.X < 0 || rectangle.Y < 0)
			{
				if (num2 == wall && num7 == wall && num4 == wall && num5 == wall)
				{
					if (num != wall && num3 != wall)
					{
						switch (num9)
						{
						case 0:
							rectangle.X = 108;
							rectangle.Y = 18;
							break;
						case 1:
							rectangle.X = 126;
							rectangle.Y = 18;
							break;
						case 2:
							rectangle.X = 144;
							rectangle.Y = 18;
							break;
						}
					}
					else if (num6 != wall && num8 != wall)
					{
						switch (num9)
						{
						case 0:
							rectangle.X = 108;
							rectangle.Y = 36;
							break;
						case 1:
							rectangle.X = 126;
							rectangle.Y = 36;
							break;
						case 2:
							rectangle.X = 144;
							rectangle.Y = 36;
							break;
						}
					}
					else if (num != wall && num6 != wall)
					{
						switch (num9)
						{
						case 0:
							rectangle.X = 180;
							rectangle.Y = 0;
							break;
						case 1:
							rectangle.X = 180;
							rectangle.Y = 18;
							break;
						case 2:
							rectangle.X = 180;
							rectangle.Y = 36;
							break;
						}
					}
					else if (num3 != wall && num8 != wall)
					{
						switch (num9)
						{
						case 0:
							rectangle.X = 198;
							rectangle.Y = 0;
							break;
						case 1:
							rectangle.X = 198;
							rectangle.Y = 18;
							break;
						case 2:
							rectangle.X = 198;
							rectangle.Y = 36;
							break;
						}
					}
					else
					{
						switch (num9)
						{
						case 0:
							rectangle.X = 18;
							rectangle.Y = 18;
							break;
						case 1:
							rectangle.X = 36;
							rectangle.Y = 18;
							break;
						case 2:
							rectangle.X = 54;
							rectangle.Y = 18;
							break;
						}
					}
				}
				else if (num2 != wall && num7 == wall && num4 == wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 18;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 36;
						rectangle.Y = 0;
						break;
					case 2:
						rectangle.X = 54;
						rectangle.Y = 0;
						break;
					}
				}
				else if (num2 == wall && num7 != wall && num4 == wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 18;
						rectangle.Y = 36;
						break;
					case 1:
						rectangle.X = 36;
						rectangle.Y = 36;
						break;
					case 2:
						rectangle.X = 54;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 == wall && num7 == wall && num4 != wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 0;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 0;
						rectangle.Y = 18;
						break;
					case 2:
						rectangle.X = 0;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 == wall && num7 == wall && num4 == wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 72;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 72;
						rectangle.Y = 18;
						break;
					case 2:
						rectangle.X = 72;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 != wall && num7 == wall && num4 != wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 0;
						rectangle.Y = 54;
						break;
					case 1:
						rectangle.X = 36;
						rectangle.Y = 54;
						break;
					case 2:
						rectangle.X = 72;
						rectangle.Y = 54;
						break;
					}
				}
				else if (num2 != wall && num7 == wall && num4 == wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 18;
						rectangle.Y = 54;
						break;
					case 1:
						rectangle.X = 54;
						rectangle.Y = 54;
						break;
					case 2:
						rectangle.X = 90;
						rectangle.Y = 54;
						break;
					}
				}
				else if (num2 == wall && num7 != wall && num4 != wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 0;
						rectangle.Y = 72;
						break;
					case 1:
						rectangle.X = 36;
						rectangle.Y = 72;
						break;
					case 2:
						rectangle.X = 72;
						rectangle.Y = 72;
						break;
					}
				}
				else if (num2 == wall && num7 != wall && num4 == wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 18;
						rectangle.Y = 72;
						break;
					case 1:
						rectangle.X = 54;
						rectangle.Y = 72;
						break;
					case 2:
						rectangle.X = 90;
						rectangle.Y = 72;
						break;
					}
				}
				else if (num2 == wall && num7 == wall && num4 != wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 90;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 90;
						rectangle.Y = 18;
						break;
					case 2:
						rectangle.X = 90;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 != wall && num7 != wall && num4 == wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 108;
						rectangle.Y = 72;
						break;
					case 1:
						rectangle.X = 126;
						rectangle.Y = 72;
						break;
					case 2:
						rectangle.X = 144;
						rectangle.Y = 72;
						break;
					}
				}
				else if (num2 != wall && num7 == wall && num4 != wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 108;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 126;
						rectangle.Y = 0;
						break;
					case 2:
						rectangle.X = 144;
						rectangle.Y = 0;
						break;
					}
				}
				else if (num2 == wall && num7 != wall && num4 != wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 108;
						rectangle.Y = 54;
						break;
					case 1:
						rectangle.X = 126;
						rectangle.Y = 54;
						break;
					case 2:
						rectangle.X = 144;
						rectangle.Y = 54;
						break;
					}
				}
				else if (num2 != wall && num7 != wall && num4 != wall && num5 == wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 162;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 162;
						rectangle.Y = 18;
						break;
					case 2:
						rectangle.X = 162;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 != wall && num7 != wall && num4 == wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 216;
						rectangle.Y = 0;
						break;
					case 1:
						rectangle.X = 216;
						rectangle.Y = 18;
						break;
					case 2:
						rectangle.X = 216;
						rectangle.Y = 36;
						break;
					}
				}
				else if (num2 != wall && num7 != wall && num4 != wall && num5 != wall)
				{
					switch (num9)
					{
					case 0:
						rectangle.X = 162;
						rectangle.Y = 54;
						break;
					case 1:
						rectangle.X = 180;
						rectangle.Y = 54;
						break;
					case 2:
						rectangle.X = 198;
						rectangle.Y = 54;
						break;
					}
				}
			}
			if (rectangle.X <= -1 || rectangle.Y <= -1)
			{
				if (num9 <= 0)
				{
					rectangle.X = 18;
					rectangle.Y = 18;
				}
				else if (num9 == 1)
				{
					rectangle.X = 36;
					rectangle.Y = 18;
				}
				else if (num9 >= 2)
				{
					rectangle.X = 54;
					rectangle.Y = 18;
				}
			}
			Main.tile[i, j].wallFrameX = (byte)rectangle.X;
			Main.tile[i, j].wallFrameY = (byte)rectangle.Y;
		}

		public static void TileFrame(int i, int j, bool resetFrame = false, bool noBreak = false)
		{
			try
			{
				Config.tilesPretend = false;
				Tile tile = Main.tile[i, j];
				int num;
				int frameX;
				int frameY;
				Rectangle rectangle;
				if (i > 5 && j > 5 && i < Main.maxTilesX - 5 && j < Main.maxTilesY - 5 && tile != null)
				{
					if (tile.liquid > 0 && Main.netMode != 1 && !noLiquidCheck)
					{
						Liquid.AddWater(i, j);
					}
					if (tile.active)
					{
						int type = tile.type;
						if (!noBreak && type >= 150 && (Config.tileDefs.width[type] > 1 || Config.tileDefs.height[type] > 1 || Config.tileDefs.special[type]))
						{
							Config.CheckTile(i, j, Config.tileDefs.width[type], Config.tileDefs.height[type], type, noBreak);
						}
						else
						{
							Config.tilesPretend = true;
							if (!noBreak || !Main.tileFrameImportant[tile.type] || tile.type == 4)
							{
								num = tile.type;
								if (Main.tileStone[num])
								{
									num = 1;
								}
								frameX = tile.frameX;
								frameY = tile.frameY;
								rectangle = new Rectangle(-1, -1, 0, 0);
								if (Main.tileFrameImportant[tile.type])
								{
									switch (num)
									{
									case 4:
									{
										short num20 = 0;
										if (tile.frameX >= 66)
										{
											num20 = 66;
										}
										int num21 = -1;
										int num22 = -1;
										int num23 = -1;
										int num24 = -1;
										int num25 = -1;
										int num26 = -1;
										int num27 = -1;
										if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
										{
											num21 = Main.tile[i, j + 1].type;
										}
										if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
										{
											num22 = Main.tile[i - 1, j].type;
										}
										if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
										{
											num23 = Main.tile[i + 1, j].type;
										}
										if (Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
										{
											num24 = Main.tile[i - 1, j + 1].type;
										}
										if (Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
										{
											num25 = Main.tile[i + 1, j + 1].type;
										}
										if (Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
										{
											num26 = Main.tile[i - 1, j - 1].type;
										}
										if (Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
										{
											num27 = Main.tile[i + 1, j - 1].type;
										}
										Config.tilesPretend = false;
										if (num21 >= 0 && Main.tileSolid[num21] && !Main.tileNoAttach[num21])
										{
											tile.frameX = num20;
										}
										else if ((num22 >= 0 && Main.tileSolid[num22] && !Main.tileNoAttach[num22]) || num22 == 124 || (num22 == 5 && num26 == 5 && num24 == 5))
										{
											tile.frameX = (short)(22 + num20);
										}
										else if ((num23 >= 0 && Main.tileSolid[num23] && !Main.tileNoAttach[num23]) || num23 == 124 || (num23 == 5 && num27 == 5 && num25 == 5))
										{
											tile.frameX = (short)(44 + num20);
										}
										else
										{
											KillTile(i, j);
										}
										break;
									}
									case 136:
									{
										int num12 = -1;
										int num13 = -1;
										int num14 = -1;
										if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
										{
											num12 = Main.tile[i, j + 1].type;
										}
										if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
										{
											num13 = Main.tile[i - 1, j].type;
										}
										if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
										{
											num14 = Main.tile[i + 1, j].type;
										}
										Config.tilesPretend = false;
										if (num12 >= 0 && Main.tileSolid[num12] && !Main.tileNoAttach[num12])
										{
											tile.frameX = 0;
										}
										else if ((num13 >= 0 && Main.tileSolid[num13] && !Main.tileNoAttach[num13]) || num13 == 124 || num13 == 5)
										{
											tile.frameX = 18;
										}
										else if ((num14 >= 0 && Main.tileSolid[num14] && !Main.tileNoAttach[num14]) || num14 == 124 || num14 == 5)
										{
											tile.frameX = 36;
										}
										else
										{
											KillTile(i, j);
										}
										break;
									}
									case 129:
									case 149:
									{
										int num16 = -1;
										int num17 = -1;
										int num18 = -1;
										int num19 = -1;
										if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
										{
											num17 = Main.tile[i, j - 1].type;
										}
										if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
										{
											num16 = Main.tile[i, j + 1].type;
										}
										if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
										{
											num18 = Main.tile[i - 1, j].type;
										}
										if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
										{
											num19 = Main.tile[i + 1, j].type;
										}
										Config.tilesPretend = false;
										if (num16 >= 0 && Main.tileSolid[num16] && !Main.tileSolidTop[num16])
										{
											tile.frameY = 0;
										}
										else if (num18 >= 0 && Main.tileSolid[num18] && !Main.tileSolidTop[num18])
										{
											tile.frameY = 54;
										}
										else if (num19 >= 0 && Main.tileSolid[num19] && !Main.tileSolidTop[num19])
										{
											tile.frameY = 36;
										}
										else if (num17 >= 0 && Main.tileSolid[num17] && !Main.tileSolidTop[num17])
										{
											tile.frameY = 18;
										}
										else
										{
											KillTile(i, j);
										}
										break;
									}
									case 3:
									case 24:
									case 61:
									case 71:
									case 73:
									case 74:
									case 110:
									case 113:
										Config.tilesPretend = false;
										PlantCheck(i, j);
										break;
									case 12:
									case 31:
										Config.tilesPretend = false;
										CheckOrb(i, j, num);
										break;
									case 10:
										if (!destroyObject)
										{
											int num15 = j;
											bool flag2 = false;
											if (tile.frameY == 18)
											{
												num15 = j - 1;
											}
											else if (tile.frameY == 36)
											{
												num15 = j - 2;
											}
											if (Main.tile[i, num15 - 1] == null)
											{
												Main.tile[i, num15 - 1] = new Tile();
											}
											if (Main.tile[i, num15 + 3] == null)
											{
												Main.tile[i, num15 + 3] = new Tile();
											}
											if (Main.tile[i, num15 + 2] == null)
											{
												Main.tile[i, num15 + 2] = new Tile();
											}
											if (Main.tile[i, num15 + 1] == null)
											{
												Main.tile[i, num15 + 1] = new Tile();
											}
											if (Main.tile[i, num15] == null)
											{
												Main.tile[i, num15] = new Tile();
											}
											if (!Main.tile[i, num15 - 1].active || !Main.tileSolid[Main.tile[i, num15 - 1].type])
											{
												flag2 = true;
											}
											else if (!Main.tile[i, num15 + 3].active || !Main.tileSolid[Main.tile[i, num15 + 3].type])
											{
												flag2 = true;
											}
											else if (!Main.tile[i, num15].active || Main.tile[i, num15].type != num)
											{
												flag2 = true;
											}
											else if (!Main.tile[i, num15 + 1].active || Main.tile[i, num15 + 1].type != num)
											{
												flag2 = true;
											}
											else if (!Main.tile[i, num15 + 2].active || Main.tile[i, num15 + 2].type != num)
											{
												flag2 = true;
											}
											Config.tilesPretend = false;
											if (flag2)
											{
												destroyObject = true;
												KillTile(i, num15);
												KillTile(i, num15 + 1);
												KillTile(i, num15 + 2);
												Item.NewItem(i * 16, j * 16, 16, 16, 25);
											}
											destroyObject = false;
										}
										break;
									case 11:
										if (!destroyObject)
										{
											int num8 = 1;
											int num9 = i;
											int num10 = j;
											bool flag = false;
											if (tile.frameX == 18)
											{
												num9 = i - 1;
											}
											else if (tile.frameX == 36)
											{
												num9 = i + 1;
												num8 = -1;
											}
											else if (tile.frameX == 54)
											{
												num8 = -1;
											}
											if (tile.frameY == 18)
											{
												num10 = j - 1;
											}
											else if (tile.frameY == 36)
											{
												num10 = j - 2;
											}
											if (Main.tile[num9, num10 + 3] == null)
											{
												Main.tile[num9, num10 + 3] = new Tile();
											}
											if (Main.tile[num9, num10 - 1] == null)
											{
												Main.tile[num9, num10 - 1] = new Tile();
											}
											if (!Main.tile[num9, num10 - 1].active || !Main.tileSolid[Main.tile[num9, num10 - 1].type] || !Main.tile[num9, num10 + 3].active || !Main.tileSolid[Main.tile[num9, num10 + 3].type])
											{
												flag = true;
												destroyObject = true;
												Item.NewItem(i * 16, j * 16, 16, 16, 25);
											}
											int num11 = num9;
											if (num8 == -1)
											{
												num11 = num9 - 1;
											}
											for (int k = num11; k < num11 + 2; k++)
											{
												for (int l = num10; l < num10 + 3; l++)
												{
													if (!flag && (Main.tile[k, l].type != 11 || !Main.tile[k, l].active))
													{
														destroyObject = true;
														Item.NewItem(i * 16, j * 16, 16, 16, 25);
														flag = true;
														k = num11;
														l = num10;
													}
													if (flag)
													{
														Config.tilesPretend = false;
														KillTile(k, l);
													}
												}
											}
											destroyObject = false;
										}
										break;
									default:
										Config.tilesPretend = false;
										switch (num)
										{
										case 34:
										case 35:
										case 36:
										case 106:
											Check3x3(i, j, num);
											break;
										case 15:
										case 20:
											Check1x2(i, j, (byte)num);
											break;
										case 14:
										case 17:
										case 26:
										case 77:
										case 86:
										case 87:
										case 88:
										case 89:
										case 114:
										case 133:
											Check3x2(i, j, num);
											break;
										case 135:
										case 141:
										case 144:
											Check1x1(i, j, num);
											break;
										case 16:
										case 18:
										case 29:
										case 103:
										case 134:
											Check2x1(i, j, (byte)num);
											break;
										case 13:
										case 33:
										case 50:
										case 78:
											CheckOnTable1x1(i, j, num);
											break;
										case 21:
											CheckChest(i, j, num);
											break;
										case 128:
											CheckMan(i, j);
											break;
										case 27:
											CheckSunflower(i, j);
											break;
										case 28:
											CheckPot(i, j);
											break;
										case 132:
										case 138:
										case 142:
										case 143:
											Check2x2(i, j, num);
											break;
										case 91:
											CheckBanner(i, j, (byte)num);
											break;
										case 139:
											CheckMB(i, j, num);
											break;
										case 92:
										case 93:
											Check1xX(i, j, (byte)num);
											break;
										case 104:
										case 105:
											Check2xX(i, j, (byte)num);
											break;
										case 101:
										case 102:
											Check3x4(i, j, num);
											break;
										case 42:
											Check1x2Top(i, j, (byte)num);
											break;
										case 55:
										case 85:
											CheckSign(i, j, num);
											break;
										case 79:
										case 90:
											Check4x2(i, j, num);
											break;
										default:
											switch (num)
											{
											case 85:
											case 94:
											case 95:
											case 96:
											case 97:
											case 98:
											case 99:
											case 100:
											case 125:
											case 126:
												Check2x2(i, j, num);
												break;
											case 81:
											{
												Config.tilesPretend = true;
												int num4 = -1;
												int num5 = -1;
												int num6 = -1;
												int num7 = -1;
												if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
												{
													num5 = Main.tile[i, j - 1].type;
												}
												if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
												{
													num4 = Main.tile[i, j + 1].type;
												}
												if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
												{
													num6 = Main.tile[i - 1, j].type;
												}
												if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
												{
													num7 = Main.tile[i + 1, j].type;
												}
												Config.tilesPretend = false;
												if (num6 != -1 || num5 != -1 || num7 != -1)
												{
													KillTile(i, j);
												}
												else if (num4 < 0 || !Main.tileSolid[num4])
												{
													KillTile(i, j);
												}
												break;
											}
											default:
												if (Main.tileAlch[num])
												{
													CheckAlch(i, j);
												}
												else
												{
													switch (num)
													{
													case 72:
													{
														Config.tilesPretend = true;
														int num2 = -1;
														int num3 = -1;
														if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
														{
															num3 = Main.tile[i, j - 1].type;
														}
														if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
														{
															num2 = Main.tile[i, j + 1].type;
														}
														Config.tilesPretend = false;
														if (num2 != num && num2 != 70)
														{
															KillTile(i, j);
														}
														else if (num3 != num && tile.frameX == 0)
														{
															tile.frameNumber = (byte)genRand.Next(3);
															if (tile.frameNumber == 0)
															{
																tile.frameX = 18;
																tile.frameY = 0;
															}
															else if (tile.frameNumber == 1)
															{
																tile.frameX = 18;
																tile.frameY = 18;
															}
															else if (tile.frameNumber == 2)
															{
																tile.frameX = 18;
																tile.frameY = 36;
															}
														}
														break;
													}
													case 5:
														CheckTree(i, j);
														break;
													}
												}
												break;
											}
											break;
										}
										break;
									}
								}
								else
								{
									int num28 = -1;
									int num29 = -1;
									int num30 = -1;
									int num31 = -1;
									int num32 = -1;
									int num33 = -1;
									int num34 = -1;
									int num35 = -1;
									Config.tilesPretend = true;
									if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
									{
										num31 = (Main.tileStone[Main.tile[i - 1, j].type] ? 1 : Main.tile[i - 1, j].type);
									}
									if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
									{
										num32 = (Main.tileStone[Main.tile[i + 1, j].type] ? 1 : Main.tile[i + 1, j].type);
									}
									if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
									{
										num29 = (Main.tileStone[Main.tile[i, j - 1].type] ? 1 : Main.tile[i, j - 1].type);
									}
									if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
									{
										num34 = (Main.tileStone[Main.tile[i, j + 1].type] ? 1 : Main.tile[i, j + 1].type);
									}
									if (Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
									{
										num28 = (Main.tileStone[Main.tile[i - 1, j - 1].type] ? 1 : Main.tile[i - 1, j - 1].type);
									}
									if (Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
									{
										num30 = (Main.tileStone[Main.tile[i + 1, j - 1].type] ? 1 : Main.tile[i + 1, j - 1].type);
									}
									if (Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
									{
										num33 = (Main.tileStone[Main.tile[i - 1, j + 1].type] ? 1 : Main.tile[i - 1, j + 1].type);
									}
									if (Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
									{
										num35 = (Main.tileStone[Main.tile[i + 1, j + 1].type] ? 1 : Main.tile[i + 1, j + 1].type);
									}
									Config.tilesPretend = false;
									if (!Main.tileSolid[num])
									{
										switch (num)
										{
										case 49:
											CheckOnTable1x1(i, j, num);
											return;
										case 80:
											CactusFrame(i, j);
											return;
										}
									}
									else if (num == 19)
									{
										if (num32 >= 0 && !Main.tileSolid[num32])
										{
											num32 = -1;
										}
										if (num31 >= 0 && !Main.tileSolid[num31])
										{
											num31 = -1;
										}
										if (num31 == num && num32 == num)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 0;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 0;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 0;
												rectangle.Y = 36;
											}
										}
										else if (num31 == num && num32 == -1)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 18;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 18;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 18;
												rectangle.Y = 36;
											}
										}
										else if (num31 == -1 && num32 == num)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 36;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 36;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 36;
												rectangle.Y = 36;
											}
										}
										else if (num31 != num && num32 == num)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 54;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 54;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 54;
												rectangle.Y = 36;
											}
										}
										else if (num31 == num && num32 != num)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 72;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 72;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 72;
												rectangle.Y = 36;
											}
										}
										else if (num31 != num && num31 != -1 && num32 == -1)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 108;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 108;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 108;
												rectangle.Y = 36;
											}
										}
										else if (num31 == -1 && num32 != num && num32 != -1)
										{
											if (tile.frameNumber == 0)
											{
												rectangle.X = 126;
												rectangle.Y = 0;
											}
											else if (tile.frameNumber == 1)
											{
												rectangle.X = 126;
												rectangle.Y = 18;
											}
											else
											{
												rectangle.X = 126;
												rectangle.Y = 36;
											}
										}
										else if (tile.frameNumber == 0)
										{
											rectangle.X = 90;
											rectangle.Y = 0;
										}
										else if (tile.frameNumber == 1)
										{
											rectangle.X = 90;
											rectangle.Y = 18;
										}
										else
										{
											rectangle.X = 90;
											rectangle.Y = 36;
										}
									}
									mergeUp = false;
									mergeDown = false;
									mergeLeft = false;
									mergeRight = false;
									int num36 = 0;
									if (resetFrame)
									{
										num36 = genRand.Next(3);
										tile.frameNumber = (byte)num36;
									}
									else
									{
										num36 = tile.frameNumber;
									}
									if (num == 0)
									{
										if (num29 >= 0 && Main.tileMergeDirt[num29])
										{
											TileFrame(i, j - 1);
											if (mergeDown)
											{
												num29 = num;
											}
										}
										if (num34 >= 0 && Main.tileMergeDirt[num34])
										{
											TileFrame(i, j + 1);
											if (mergeUp)
											{
												num34 = num;
											}
										}
										if (num31 >= 0 && Main.tileMergeDirt[num31])
										{
											TileFrame(i - 1, j);
											if (mergeRight)
											{
												num31 = num;
											}
										}
										if (num32 >= 0 && Main.tileMergeDirt[num32])
										{
											TileFrame(i + 1, j);
											if (mergeLeft)
											{
												num32 = num;
											}
										}
										if (num29 == 2 || num29 == 23 || num29 == 109)
										{
											num29 = num;
										}
										if (num34 == 2 || num34 == 23 || num34 == 109)
										{
											num34 = num;
										}
										if (num31 == 2 || num31 == 23 || num31 == 109)
										{
											num31 = num;
										}
										if (num32 == 2 || num32 == 23 || num32 == 109)
										{
											num32 = num;
										}
										if (num28 >= 0 && Main.tileMergeDirt[num28])
										{
											num28 = num;
										}
										else if (num28 == 2 || num28 == 23 || num28 == 109)
										{
											num28 = num;
										}
										if (num30 >= 0 && Main.tileMergeDirt[num30])
										{
											num30 = num;
										}
										else if (num30 == 2 || num30 == 23 || num30 == 109)
										{
											num30 = num;
										}
										if (num33 >= 0 && Main.tileMergeDirt[num33])
										{
											num33 = num;
										}
										else if (num33 == 2 || num33 == 23 || num30 == 109)
										{
											num33 = num;
										}
										if (num35 >= 0 && Main.tileMergeDirt[num35])
										{
											num35 = num;
										}
										else if (num35 == 2 || num35 == 23 || num35 == 109)
										{
											num35 = num;
										}
										if ((double)j < Main.rockLayer)
										{
											if (num29 == 59)
											{
												num29 = -2;
											}
											if (num34 == 59)
											{
												num34 = -2;
											}
											if (num31 == 59)
											{
												num31 = -2;
											}
											if (num32 == 59)
											{
												num32 = -2;
											}
											if (num28 == 59)
											{
												num28 = -2;
											}
											if (num30 == 59)
											{
												num30 = -2;
											}
											if (num33 == 59)
											{
												num33 = -2;
											}
											if (num35 == 59)
											{
												num35 = -2;
											}
										}
									}
									else if (Main.tileMergeDirt[num])
									{
										if (num29 == 0)
										{
											num29 = -2;
										}
										if (num34 == 0)
										{
											num34 = -2;
										}
										if (num31 == 0)
										{
											num31 = -2;
										}
										if (num32 == 0)
										{
											num32 = -2;
										}
										if (num28 == 0)
										{
											num28 = -2;
										}
										if (num30 == 0)
										{
											num30 = -2;
										}
										if (num33 == 0)
										{
											num33 = -2;
										}
										if (num35 == 0)
										{
											num35 = -2;
										}
										if (num == 1)
										{
											if ((double)j > Main.rockLayer)
											{
												if (num29 == 59)
												{
													TileFrame(i, j - 1);
													if (mergeDown)
													{
														num29 = num;
													}
												}
												if (num34 == 59)
												{
													TileFrame(i, j + 1);
													if (mergeUp)
													{
														num34 = num;
													}
												}
												if (num31 == 59)
												{
													TileFrame(i - 1, j);
													if (mergeRight)
													{
														num31 = num;
													}
												}
												if (num32 == 59)
												{
													TileFrame(i + 1, j);
													if (mergeLeft)
													{
														num32 = num;
													}
												}
												if (num28 == 59)
												{
													num28 = num;
												}
												if (num30 == 59)
												{
													num30 = num;
												}
												if (num33 == 59)
												{
													num33 = num;
												}
												if (num35 == 59)
												{
													num35 = num;
												}
											}
											if (num29 == 57)
											{
												TileFrame(i, j - 1);
												if (mergeDown)
												{
													num29 = num;
												}
											}
											if (num34 == 57)
											{
												TileFrame(i, j + 1);
												if (mergeUp)
												{
													num34 = num;
												}
											}
											if (num31 == 57)
											{
												TileFrame(i - 1, j);
												if (mergeRight)
												{
													num31 = num;
												}
											}
											if (num32 == 57)
											{
												TileFrame(i + 1, j);
												if (mergeLeft)
												{
													num32 = num;
												}
											}
											if (num28 == 57)
											{
												num28 = num;
											}
											if (num30 == 57)
											{
												num30 = num;
											}
											if (num33 == 57)
											{
												num33 = num;
											}
											if (num35 == 57)
											{
												num35 = num;
											}
										}
									}
									else
									{
										switch (num)
										{
										case 58:
										case 75:
										case 76:
											if (num29 == 57)
											{
												num29 = -2;
											}
											if (num34 == 57)
											{
												num34 = -2;
											}
											if (num31 == 57)
											{
												num31 = -2;
											}
											if (num32 == 57)
											{
												num32 = -2;
											}
											if (num28 == 57)
											{
												num28 = -2;
											}
											if (num30 == 57)
											{
												num30 = -2;
											}
											if (num33 == 57)
											{
												num33 = -2;
											}
											if (num35 == 57)
											{
												num35 = -2;
											}
											break;
										case 59:
											if ((double)j > Main.rockLayer)
											{
												if (num29 == 1)
												{
													num29 = -2;
												}
												if (num34 == 1)
												{
													num34 = -2;
												}
												if (num31 == 1)
												{
													num31 = -2;
												}
												if (num32 == 1)
												{
													num32 = -2;
												}
												if (num28 == 1)
												{
													num28 = -2;
												}
												if (num30 == 1)
												{
													num30 = -2;
												}
												if (num33 == 1)
												{
													num33 = -2;
												}
												if (num35 == 1)
												{
													num35 = -2;
												}
											}
											if (num29 == 60)
											{
												num29 = num;
											}
											if (num34 == 60)
											{
												num34 = num;
											}
											if (num31 == 60)
											{
												num31 = num;
											}
											if (num32 == 60)
											{
												num32 = num;
											}
											if (num28 == 60)
											{
												num28 = num;
											}
											if (num30 == 60)
											{
												num30 = num;
											}
											if (num33 == 60)
											{
												num33 = num;
											}
											if (num35 == 60)
											{
												num35 = num;
											}
											if (num29 == 70)
											{
												num29 = num;
											}
											if (num34 == 70)
											{
												num34 = num;
											}
											if (num31 == 70)
											{
												num31 = num;
											}
											if (num32 == 70)
											{
												num32 = num;
											}
											if (num28 == 70)
											{
												num28 = num;
											}
											if (num30 == 70)
											{
												num30 = num;
											}
											if (num33 == 70)
											{
												num33 = num;
											}
											if (num35 == 70)
											{
												num35 = num;
											}
											if ((double)j < Main.rockLayer)
											{
												if (num29 == 0)
												{
													TileFrame(i, j - 1);
													if (mergeDown)
													{
														num29 = num;
													}
												}
												if (num34 == 0)
												{
													TileFrame(i, j + 1);
													if (mergeUp)
													{
														num34 = num;
													}
												}
												if (num31 == 0)
												{
													TileFrame(i - 1, j);
													if (mergeRight)
													{
														num31 = num;
													}
												}
												if (num32 == 0)
												{
													TileFrame(i + 1, j);
													if (mergeLeft)
													{
														num32 = num;
													}
												}
												if (num28 == 0)
												{
													num28 = num;
												}
												if (num30 == 0)
												{
													num30 = num;
												}
												if (num33 == 0)
												{
													num33 = num;
												}
												if (num35 == 0)
												{
													num35 = num;
												}
											}
											break;
										case 57:
											if (num29 == 1)
											{
												num29 = -2;
											}
											if (num34 == 1)
											{
												num34 = -2;
											}
											if (num31 == 1)
											{
												num31 = -2;
											}
											if (num32 == 1)
											{
												num32 = -2;
											}
											if (num28 == 1)
											{
												num28 = -2;
											}
											if (num30 == 1)
											{
												num30 = -2;
											}
											if (num33 == 1)
											{
												num33 = -2;
											}
											if (num35 == 1)
											{
												num35 = -2;
											}
											if (num29 == 58 || num29 == 76 || num29 == 75)
											{
												TileFrame(i, j - 1);
												if (mergeDown)
												{
													num29 = num;
												}
											}
											if (num34 == 58 || num34 == 76 || num34 == 75)
											{
												TileFrame(i, j + 1);
												if (mergeUp)
												{
													num34 = num;
												}
											}
											if (num31 == 58 || num31 == 76 || num31 == 75)
											{
												TileFrame(i - 1, j);
												if (mergeRight)
												{
													num31 = num;
												}
											}
											if (num32 == 58 || num32 == 76 || num32 == 75)
											{
												TileFrame(i + 1, j);
												if (mergeLeft)
												{
													num32 = num;
												}
											}
											if (num28 == 58 || num28 == 76 || num28 == 75)
											{
												num28 = num;
											}
											if (num30 == 58 || num30 == 76 || num30 == 75)
											{
												num30 = num;
											}
											if (num33 == 58 || num33 == 76 || num33 == 75)
											{
												num33 = num;
											}
											if (num35 == 58 || num35 == 76 || num35 == 75)
											{
												num35 = num;
											}
											break;
										case 32:
											if (num34 == 23)
											{
												num34 = num;
											}
											break;
										case 69:
											if (num34 == 60)
											{
												num34 = num;
											}
											break;
										case 51:
											if (num29 > -1 && !Main.tileNoAttach[num29])
											{
												num29 = num;
											}
											if (num34 > -1 && !Main.tileNoAttach[num34])
											{
												num34 = num;
											}
											if (num31 > -1 && !Main.tileNoAttach[num31])
											{
												num31 = num;
											}
											if (num32 > -1 && !Main.tileNoAttach[num32])
											{
												num32 = num;
											}
											if (num28 > -1 && !Main.tileNoAttach[num28])
											{
												num28 = num;
											}
											if (num30 > -1 && !Main.tileNoAttach[num30])
											{
												num30 = num;
											}
											if (num33 > -1 && !Main.tileNoAttach[num33])
											{
												num33 = num;
											}
											if (num35 > -1 && !Main.tileNoAttach[num35])
											{
												num35 = num;
											}
											break;
										}
									}
									bool flag3 = false;
									if (num == 2 || num == 23 || num == 60 || num == 70 || num == 109)
									{
										flag3 = true;
										if (num29 > -1 && !Main.tileSolid[num29] && num29 != num)
										{
											num29 = -1;
										}
										if (num34 > -1 && !Main.tileSolid[num34] && num34 != num)
										{
											num34 = -1;
										}
										if (num31 > -1 && !Main.tileSolid[num31] && num31 != num)
										{
											num31 = -1;
										}
										if (num32 > -1 && !Main.tileSolid[num32] && num32 != num)
										{
											num32 = -1;
										}
										if (num28 > -1 && !Main.tileSolid[num28] && num28 != num)
										{
											num28 = -1;
										}
										if (num30 > -1 && !Main.tileSolid[num30] && num30 != num)
										{
											num30 = -1;
										}
										if (num33 > -1 && !Main.tileSolid[num33] && num33 != num)
										{
											num33 = -1;
										}
										if (num35 > -1 && !Main.tileSolid[num35] && num35 != num)
										{
											num35 = -1;
										}
										int num37 = 0;
										switch (num)
										{
										case 60:
										case 70:
											num37 = 59;
											break;
										case 2:
											if (num29 == 23)
											{
												num29 = num37;
											}
											if (num34 == 23)
											{
												num34 = num37;
											}
											if (num31 == 23)
											{
												num31 = num37;
											}
											if (num32 == 23)
											{
												num32 = num37;
											}
											if (num28 == 23)
											{
												num28 = num37;
											}
											if (num30 == 23)
											{
												num30 = num37;
											}
											if (num33 == 23)
											{
												num33 = num37;
											}
											if (num35 == 23)
											{
												num35 = num37;
											}
											break;
										case 23:
											if (num29 == 2)
											{
												num29 = num37;
											}
											if (num34 == 2)
											{
												num34 = num37;
											}
											if (num31 == 2)
											{
												num31 = num37;
											}
											if (num32 == 2)
											{
												num32 = num37;
											}
											if (num28 == 2)
											{
												num28 = num37;
											}
											if (num30 == 2)
											{
												num30 = num37;
											}
											if (num33 == 2)
											{
												num33 = num37;
											}
											if (num35 == 2)
											{
												num35 = num37;
											}
											break;
										}
										if (num29 != num && num29 != num37 && (num34 == num || num34 == num37))
										{
											if (num31 == num37 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 198;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 198;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 198;
													break;
												}
											}
											else if (num31 == num && num32 == num37)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 198;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 198;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 198;
													break;
												}
											}
										}
										else if (num34 != num && num34 != num37 && (num29 == num || num29 == num37))
										{
											if (num31 == num37 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 216;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 216;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 216;
													break;
												}
											}
											else if (num31 == num && num32 == num37)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 216;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 216;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 216;
													break;
												}
											}
										}
										else if (num31 != num && num31 != num37 && (num32 == num || num32 == num37))
										{
											if (num29 == num37 && num34 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 72;
													rectangle.Y = 144;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 162;
													break;
												default:
													rectangle.X = 72;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num34 == num && num32 == num29)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 72;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 72;
													rectangle.Y = 126;
													break;
												}
											}
										}
										else if (num32 != num && num32 != num37 && (num31 == num || num31 == num37))
										{
											if (num29 == num37 && num34 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 90;
													rectangle.Y = 144;
													break;
												case 1:
													rectangle.X = 90;
													rectangle.Y = 162;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num34 == num && num32 == num29)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 90;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 90;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 126;
													break;
												}
											}
										}
										else if (num29 == num && num34 == num && num31 == num && num32 == num)
										{
											if (num28 != num && num30 != num && num33 != num && num35 != num)
											{
												if (num35 == num37)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 108;
														rectangle.Y = 324;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 324;
														break;
													default:
														rectangle.X = 144;
														rectangle.Y = 324;
														break;
													}
												}
												else if (num30 == num37)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 108;
														rectangle.Y = 342;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 342;
														break;
													default:
														rectangle.X = 144;
														rectangle.Y = 342;
														break;
													}
												}
												else if (num33 == num37)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 108;
														rectangle.Y = 360;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 360;
														break;
													default:
														rectangle.X = 144;
														rectangle.Y = 360;
														break;
													}
												}
												else if (num28 == num37)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 108;
														rectangle.Y = 378;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 378;
														break;
													default:
														rectangle.X = 144;
														rectangle.Y = 378;
														break;
													}
												}
												else
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 144;
														rectangle.Y = 234;
														break;
													case 1:
														rectangle.X = 198;
														rectangle.Y = 234;
														break;
													default:
														rectangle.X = 252;
														rectangle.Y = 234;
														break;
													}
												}
											}
											else if (num28 != num && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 306;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 306;
													break;
												default:
													rectangle.X = 72;
													rectangle.Y = 306;
													break;
												}
											}
											else if (num30 != num && num33 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 90;
													rectangle.Y = 306;
													break;
												case 1:
													rectangle.X = 108;
													rectangle.Y = 306;
													break;
												default:
													rectangle.X = 126;
													rectangle.Y = 306;
													break;
												}
											}
											else if (num28 != num && num30 == num && num33 == num && num35 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num28 == num && num30 != num && num33 == num && num35 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num28 == num && num30 == num && num33 != num && num35 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 162;
													break;
												}
											}
											else if (num28 == num && num30 == num && num33 == num && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 162;
													break;
												}
											}
										}
										else if (num29 == num && num34 == num37 && num31 == num && num32 == num && num28 == -1 && num30 == -1)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 108;
												rectangle.Y = 18;
												break;
											case 1:
												rectangle.X = 126;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 144;
												rectangle.Y = 18;
												break;
											}
										}
										else if (num29 == num37 && num34 == num && num31 == num && num32 == num && num33 == -1 && num35 == -1)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 108;
												rectangle.Y = 36;
												break;
											case 1:
												rectangle.X = 126;
												rectangle.Y = 36;
												break;
											default:
												rectangle.X = 144;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 == num && num34 == num && num31 == num37 && num32 == num && num30 == -1 && num35 == -1)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 198;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 198;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 198;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 == num && num34 == num && num31 == num && num32 == num37 && num28 == -1 && num33 == -1)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 180;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 180;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 180;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 == num && num34 == num37 && num31 == num && num32 == num)
										{
											if (num30 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num28 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 180;
													break;
												}
											}
										}
										else if (num29 == num37 && num34 == num && num31 == num && num32 == num)
										{
											if (num35 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 162;
													break;
												}
											}
											else if (num33 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 162;
													break;
												}
											}
										}
										else if (num29 == num && num34 == num && num31 == num && num32 == num37)
										{
											if (num28 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 162;
													break;
												}
											}
											else if (num33 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 180;
													break;
												}
											}
										}
										else if (num29 == num && num34 == num && num31 == num37 && num32 == num)
										{
											if (num30 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 162;
													break;
												}
											}
											else if (num35 != -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 180;
													break;
												}
											}
										}
										else if ((num29 == num37 && num34 == num && num31 == num && num32 == num) || (num29 == num && num34 == num37 && num31 == num && num32 == num) || (num29 == num && num34 == num && num31 == num37 && num32 == num) || (num29 == num && num34 == num && num31 == num && num32 == num37))
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 18;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 54;
												rectangle.Y = 18;
												break;
											}
										}
										if ((num29 == num || num29 == num37) && (num34 == num || num34 == num37) && (num31 == num || num31 == num37) && (num32 == num || num32 == num37))
										{
											if (num28 != num && num28 != num37 && (num30 == num || num30 == num37) && (num33 == num || num33 == num37) && (num35 == num || num35 == num37))
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num30 != num && num30 != num37 && (num28 == num || num28 == num37) && (num33 == num || num33 == num37) && (num35 == num || num35 == num37))
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 180;
													break;
												}
											}
											else if (num33 != num && num33 != num37 && (num28 == num || num28 == num37) && (num30 == num || num30 == num37) && (num35 == num || num35 == num37))
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 162;
													break;
												}
											}
											else if (num35 != num && num35 != num37 && (num28 == num || num28 == num37) && (num33 == num || num33 == num37) && (num30 == num || num30 == num37))
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 162;
													break;
												}
											}
										}
										if (num29 != num37 && num29 != num && num34 == num && num31 != num37 && num31 != num && num32 == num && num35 != num37 && num35 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 90;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 108;
												rectangle.Y = 270;
												break;
											default:
												rectangle.X = 126;
												rectangle.Y = 270;
												break;
											}
										}
										else if (num29 != num37 && num29 != num && num34 == num && num31 == num && num32 != num37 && num32 != num && num33 != num37 && num33 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 144;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 162;
												rectangle.Y = 270;
												break;
											default:
												rectangle.X = 180;
												rectangle.Y = 270;
												break;
											}
										}
										else if (num34 != num37 && num34 != num && num29 == num && num31 != num37 && num31 != num && num32 == num && num30 != num37 && num30 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 90;
												rectangle.Y = 288;
												break;
											case 1:
												rectangle.X = 108;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 126;
												rectangle.Y = 288;
												break;
											}
										}
										else if (num34 != num37 && num34 != num && num29 == num && num31 == num && num32 != num37 && num32 != num && num28 != num37 && num28 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 144;
												rectangle.Y = 288;
												break;
											case 1:
												rectangle.X = 162;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 180;
												rectangle.Y = 288;
												break;
											}
										}
										else if (num29 != num && num29 != num37 && num34 == num && num31 == num && num32 == num && num33 != num && num33 != num37 && num35 != num && num35 != num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 144;
												rectangle.Y = 216;
												break;
											case 1:
												rectangle.X = 198;
												rectangle.Y = 216;
												break;
											default:
												rectangle.X = 252;
												rectangle.Y = 216;
												break;
											}
										}
										else if (num34 != num && num34 != num37 && num29 == num && num31 == num && num32 == num && num28 != num && num28 != num37 && num30 != num && num30 != num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 144;
												rectangle.Y = 252;
												break;
											case 1:
												rectangle.X = 198;
												rectangle.Y = 252;
												break;
											default:
												rectangle.X = 252;
												rectangle.Y = 252;
												break;
											}
										}
										else if (num31 != num && num31 != num37 && num34 == num && num29 == num && num32 == num && num30 != num && num30 != num37 && num35 != num && num35 != num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 126;
												rectangle.Y = 234;
												break;
											case 1:
												rectangle.X = 180;
												rectangle.Y = 234;
												break;
											default:
												rectangle.X = 234;
												rectangle.Y = 234;
												break;
											}
										}
										else if (num32 != num && num32 != num37 && num34 == num && num29 == num && num31 == num && num28 != num && num28 != num37 && num33 != num && num33 != num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 162;
												rectangle.Y = 234;
												break;
											case 1:
												rectangle.X = 216;
												rectangle.Y = 234;
												break;
											default:
												rectangle.X = 270;
												rectangle.Y = 234;
												break;
											}
										}
										else if (num29 != num37 && num29 != num && (num34 == num37 || num34 == num) && num31 == num37 && num32 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 36;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 54;
												rectangle.Y = 270;
												break;
											default:
												rectangle.X = 72;
												rectangle.Y = 270;
												break;
											}
										}
										else if (num34 != num37 && num34 != num && (num29 == num37 || num29 == num) && num31 == num37 && num32 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 36;
												rectangle.Y = 288;
												break;
											case 1:
												rectangle.X = 54;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 72;
												rectangle.Y = 288;
												break;
											}
										}
										else if (num31 != num37 && num31 != num && (num32 == num37 || num32 == num) && num29 == num37 && num34 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 0;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 0;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 0;
												rectangle.Y = 306;
												break;
											}
										}
										else if (num32 != num37 && num32 != num && (num31 == num37 || num31 == num) && num29 == num37 && num34 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 18;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 18;
												rectangle.Y = 306;
												break;
											}
										}
										else if (num29 == num && num34 == num37 && num31 == num37 && num32 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 198;
												rectangle.Y = 288;
												break;
											case 1:
												rectangle.X = 216;
												rectangle.Y = 288;
												break;
											default:
												rectangle.X = 234;
												rectangle.Y = 288;
												break;
											}
										}
										else if (num29 == num37 && num34 == num && num31 == num37 && num32 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 198;
												rectangle.Y = 270;
												break;
											case 1:
												rectangle.X = 216;
												rectangle.Y = 270;
												break;
											default:
												rectangle.X = 234;
												rectangle.Y = 270;
												break;
											}
										}
										else if (num29 == num37 && num34 == num37 && num31 == num && num32 == num37)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 198;
												rectangle.Y = 306;
												break;
											case 1:
												rectangle.X = 216;
												rectangle.Y = 306;
												break;
											default:
												rectangle.X = 234;
												rectangle.Y = 306;
												break;
											}
										}
										else if (num29 == num37 && num34 == num37 && num31 == num37 && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 144;
												rectangle.Y = 306;
												break;
											case 1:
												rectangle.X = 162;
												rectangle.Y = 306;
												break;
											default:
												rectangle.X = 180;
												rectangle.Y = 306;
												break;
											}
										}
										if (num29 != num && num29 != num37 && num34 == num && num31 == num && num32 == num)
										{
											if ((num33 == num37 || num33 == num) && num35 != num37 && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 324;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 324;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 324;
													break;
												}
											}
											else if ((num35 == num37 || num35 == num) && num33 != num37 && num33 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 324;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 324;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 324;
													break;
												}
											}
										}
										else if (num34 != num && num34 != num37 && num29 == num && num31 == num && num32 == num)
										{
											if ((num28 == num37 || num28 == num) && num30 != num37 && num30 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 342;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 342;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 342;
													break;
												}
											}
											else if ((num30 == num37 || num30 == num) && num28 != num37 && num28 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 342;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 342;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 342;
													break;
												}
											}
										}
										else if (num31 != num && num31 != num37 && num29 == num && num34 == num && num32 == num)
										{
											if ((num30 == num37 || num30 == num) && num35 != num37 && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 360;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 360;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 360;
													break;
												}
											}
											else if ((num35 == num37 || num35 == num) && num30 != num37 && num30 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 360;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 360;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 360;
													break;
												}
											}
											else if (num32 != num && num32 != num37 && num29 == num && num34 == num && num31 == num)
											{
												if ((num28 == num37 || num28 == num) && num33 != num37 && num33 != num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 378;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 378;
														break;
													default:
														rectangle.X = 36;
														rectangle.Y = 378;
														break;
													}
												}
												else if ((num33 == num37 || num33 == num) && num28 != num37 && num28 != num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 54;
														rectangle.Y = 378;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 378;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 378;
														break;
													}
												}
											}
										}
										if ((num29 == num || num29 == num37) && (num34 == num || num34 == num37) && (num31 == num || num31 == num37) && (num32 == num || num32 == num37) && num28 != -1 && num30 != -1 && num33 != -1 && num35 != -1)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 18;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 54;
												rectangle.Y = 18;
												break;
											}
										}
										if (num29 == num37)
										{
											num29 = -2;
										}
										if (num34 == num37)
										{
											num34 = -2;
										}
										if (num31 == num37)
										{
											num31 = -2;
										}
										if (num32 == num37)
										{
											num32 = -2;
										}
										if (num28 == num37)
										{
											num28 = -2;
										}
										if (num30 == num37)
										{
											num30 = -2;
										}
										if (num33 == num37)
										{
											num33 = -2;
										}
										if (num35 == num37)
										{
											num35 = -2;
										}
									}
									if (num28 > -1 && Main.tileMerge[num][num28])
									{
										num28 = num;
									}
									if (num29 > -1 && Main.tileMerge[num][num29])
									{
										num29 = num;
									}
									if (num30 > -1 && Main.tileMerge[num][num30])
									{
										num30 = num;
									}
									if (num31 > -1 && Main.tileMerge[num][num31])
									{
										num31 = num;
									}
									if (num32 > -1 && Main.tileMerge[num][num32])
									{
										num32 = num;
									}
									if (num34 > -1 && Main.tileMerge[num][num34])
									{
										num34 = num;
									}
									if (num35 > -1 && Main.tileMerge[num][num35])
									{
										num35 = num;
									}
									if (rectangle.X == -1 && rectangle.Y == -1 && (Main.tileMergeDirt[num] || num == 0 || num == 2 || num == 57 || num == 58 || num == 59 || num == 60 || num == 70 || num == 109 || num == 76 || num == 75))
									{
										if (!flag3)
										{
											flag3 = true;
											if (num29 > -1 && !Main.tileSolid[num29] && num29 != num)
											{
												num29 = -1;
											}
											if (num34 > -1 && !Main.tileSolid[num34] && num34 != num)
											{
												num34 = -1;
											}
											if (num31 > -1 && !Main.tileSolid[num31] && num31 != num)
											{
												num31 = -1;
											}
											if (num32 > -1 && !Main.tileSolid[num32] && num32 != num)
											{
												num32 = -1;
											}
											if (num28 > -1 && !Main.tileSolid[num28] && num28 != num)
											{
												num28 = -1;
											}
											if (num30 > -1 && !Main.tileSolid[num30] && num30 != num)
											{
												num30 = -1;
											}
											if (num33 > -1 && !Main.tileSolid[num33] && num33 != num)
											{
												num33 = -1;
											}
											if (num35 > -1 && !Main.tileSolid[num35] && num35 != num)
											{
												num35 = -1;
											}
										}
										if (num29 >= 0 && num29 != num)
										{
											num29 = -1;
										}
										if (num34 >= 0 && num34 != num)
										{
											num34 = -1;
										}
										if (num31 >= 0 && num31 != num)
										{
											num31 = -1;
										}
										if (num32 >= 0 && num32 != num)
										{
											num32 = -1;
										}
										if (num29 != -1 && num34 != -1 && num31 != -1 && num32 != -1)
										{
											if (num29 == -2 && num34 == num && num31 == num && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 144;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 162;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 180;
													rectangle.Y = 108;
													break;
												}
												mergeUp = true;
											}
											else if (num29 == num && num34 == -2 && num31 == num && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 144;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 162;
													rectangle.Y = 90;
													break;
												default:
													rectangle.X = 180;
													rectangle.Y = 90;
													break;
												}
												mergeDown = true;
											}
											else if (num29 == num && num34 == num && num31 == -2 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 162;
													rectangle.Y = 126;
													break;
												case 1:
													rectangle.X = 162;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 162;
													rectangle.Y = 162;
													break;
												}
												mergeLeft = true;
											}
											else if (num29 == num && num34 == num && num31 == num && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 144;
													rectangle.Y = 126;
													break;
												case 1:
													rectangle.X = 144;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 144;
													rectangle.Y = 162;
													break;
												}
												mergeRight = true;
											}
											else if (num29 == -2 && num34 == num && num31 == -2 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 162;
													break;
												}
												mergeUp = true;
												mergeLeft = true;
											}
											else if (num29 == -2 && num34 == num && num31 == num && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 126;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 162;
													break;
												}
												mergeUp = true;
												mergeRight = true;
											}
											else if (num29 == num && num34 == -2 && num31 == -2 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 36;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 180;
													break;
												}
												mergeDown = true;
												mergeLeft = true;
											}
											else if (num29 == num && num34 == -2 && num31 == num && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 108;
													break;
												case 1:
													rectangle.X = 54;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 180;
													break;
												}
												mergeDown = true;
												mergeRight = true;
											}
											else if (num29 == num && num34 == num && num31 == -2 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 180;
													rectangle.Y = 126;
													break;
												case 1:
													rectangle.X = 180;
													rectangle.Y = 144;
													break;
												default:
													rectangle.X = 180;
													rectangle.Y = 162;
													break;
												}
												mergeLeft = true;
												mergeRight = true;
											}
											else if (num29 == -2 && num34 == -2 && num31 == num && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 144;
													rectangle.Y = 180;
													break;
												case 1:
													rectangle.X = 162;
													rectangle.Y = 180;
													break;
												default:
													rectangle.X = 180;
													rectangle.Y = 180;
													break;
												}
												mergeUp = true;
												mergeDown = true;
											}
											else if (num29 == -2 && num34 == num && num31 == -2 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 198;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 198;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 198;
													rectangle.Y = 126;
													break;
												}
												mergeUp = true;
												mergeLeft = true;
												mergeRight = true;
											}
											else if (num29 == num && num34 == -2 && num31 == -2 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 198;
													rectangle.Y = 144;
													break;
												case 1:
													rectangle.X = 198;
													rectangle.Y = 162;
													break;
												default:
													rectangle.X = 198;
													rectangle.Y = 180;
													break;
												}
												mergeDown = true;
												mergeLeft = true;
												mergeRight = true;
											}
											else if (num29 == -2 && num34 == -2 && num31 == num && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 216;
													rectangle.Y = 144;
													break;
												case 1:
													rectangle.X = 216;
													rectangle.Y = 162;
													break;
												default:
													rectangle.X = 216;
													rectangle.Y = 180;
													break;
												}
												mergeUp = true;
												mergeDown = true;
												mergeRight = true;
											}
											else if (num29 == -2 && num34 == -2 && num31 == -2 && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 216;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 216;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 216;
													rectangle.Y = 126;
													break;
												}
												mergeUp = true;
												mergeDown = true;
												mergeLeft = true;
											}
											else if (num29 == -2 && num34 == -2 && num31 == -2 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 108;
													rectangle.Y = 198;
													break;
												case 1:
													rectangle.X = 126;
													rectangle.Y = 198;
													break;
												default:
													rectangle.X = 144;
													rectangle.Y = 198;
													break;
												}
												mergeUp = true;
												mergeDown = true;
												mergeLeft = true;
												mergeRight = true;
											}
											else if (num29 == num && num34 == num && num31 == num && num32 == num)
											{
												if (num28 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 18;
														rectangle.Y = 108;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 144;
														break;
													default:
														rectangle.X = 18;
														rectangle.Y = 180;
														break;
													}
												}
												if (num30 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 108;
														break;
													case 1:
														rectangle.X = 0;
														rectangle.Y = 144;
														break;
													default:
														rectangle.X = 0;
														rectangle.Y = 180;
														break;
													}
												}
												if (num33 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 18;
														rectangle.Y = 90;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 126;
														break;
													default:
														rectangle.X = 18;
														rectangle.Y = 162;
														break;
													}
												}
												if (num35 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 90;
														break;
													case 1:
														rectangle.X = 0;
														rectangle.Y = 126;
														break;
													default:
														rectangle.X = 0;
														rectangle.Y = 162;
														break;
													}
												}
											}
										}
										else if (num != 2 && num != 23 && num != 60 && num != 70 && num != 109)
										{
											if (num29 == -1 && num34 == -2 && num31 == num && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 234;
													rectangle.Y = 0;
													break;
												case 1:
													rectangle.X = 252;
													rectangle.Y = 0;
													break;
												default:
													rectangle.X = 270;
													rectangle.Y = 0;
													break;
												}
												mergeDown = true;
											}
											else if (num29 == -2 && num34 == -1 && num31 == num && num32 == num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 234;
													rectangle.Y = 18;
													break;
												case 1:
													rectangle.X = 252;
													rectangle.Y = 18;
													break;
												default:
													rectangle.X = 270;
													rectangle.Y = 18;
													break;
												}
												mergeUp = true;
											}
											else if (num29 == num && num34 == num && num31 == -1 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 234;
													rectangle.Y = 36;
													break;
												case 1:
													rectangle.X = 252;
													rectangle.Y = 36;
													break;
												default:
													rectangle.X = 270;
													rectangle.Y = 36;
													break;
												}
												mergeRight = true;
											}
											else if (num29 == num && num34 == num && num31 == -2 && num32 == -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 234;
													rectangle.Y = 54;
													break;
												case 1:
													rectangle.X = 252;
													rectangle.Y = 54;
													break;
												default:
													rectangle.X = 270;
													rectangle.Y = 54;
													break;
												}
												mergeLeft = true;
											}
											if (num29 != -1 && num34 != -1 && num31 == -1 && num32 == num)
											{
												if (num29 == -2 && num34 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 72;
														rectangle.Y = 144;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 162;
														break;
													default:
														rectangle.X = 72;
														rectangle.Y = 180;
														break;
													}
													mergeUp = true;
												}
												else if (num34 == -2 && num29 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 72;
														rectangle.Y = 90;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 108;
														break;
													default:
														rectangle.X = 72;
														rectangle.Y = 126;
														break;
													}
													mergeDown = true;
												}
											}
											else if (num29 != -1 && num34 != -1 && num31 == num && num32 == -1)
											{
												if (num29 == -2 && num34 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 90;
														rectangle.Y = 144;
														break;
													case 1:
														rectangle.X = 90;
														rectangle.Y = 162;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 180;
														break;
													}
													mergeUp = true;
												}
												else if (num34 == -2 && num29 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 90;
														rectangle.Y = 90;
														break;
													case 1:
														rectangle.X = 90;
														rectangle.Y = 108;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 126;
														break;
													}
													mergeDown = true;
												}
											}
											else if (num29 == -1 && num34 == num && num31 != -1 && num32 != -1)
											{
												if (num31 == -2 && num32 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 198;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 198;
														break;
													default:
														rectangle.X = 36;
														rectangle.Y = 198;
														break;
													}
													mergeLeft = true;
												}
												else if (num32 == -2 && num31 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 54;
														rectangle.Y = 198;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 198;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 198;
														break;
													}
													mergeRight = true;
												}
											}
											else if (num29 == num && num34 == -1 && num31 != -1 && num32 != -1)
											{
												if (num31 == -2 && num32 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 216;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 216;
														break;
													default:
														rectangle.X = 36;
														rectangle.Y = 216;
														break;
													}
													mergeLeft = true;
												}
												else if (num32 == -2 && num31 == num)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 54;
														rectangle.Y = 216;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 216;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 216;
														break;
													}
													mergeRight = true;
												}
											}
											else if (num29 != -1 && num34 != -1 && num31 == -1 && num32 == -1)
											{
												if (num29 == -2 && num34 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 108;
														rectangle.Y = 216;
														break;
													case 1:
														rectangle.X = 108;
														rectangle.Y = 234;
														break;
													default:
														rectangle.X = 108;
														rectangle.Y = 252;
														break;
													}
													mergeUp = true;
													mergeDown = true;
												}
												else if (num29 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 126;
														rectangle.Y = 144;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 162;
														break;
													default:
														rectangle.X = 126;
														rectangle.Y = 180;
														break;
													}
													mergeUp = true;
												}
												else if (num34 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 126;
														rectangle.Y = 90;
														break;
													case 1:
														rectangle.X = 126;
														rectangle.Y = 108;
														break;
													default:
														rectangle.X = 126;
														rectangle.Y = 126;
														break;
													}
													mergeDown = true;
												}
											}
											else if (num29 == -1 && num34 == -1 && num31 != -1 && num32 != -1)
											{
												if (num31 == -2 && num32 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 162;
														rectangle.Y = 198;
														break;
													case 1:
														rectangle.X = 180;
														rectangle.Y = 198;
														break;
													default:
														rectangle.X = 198;
														rectangle.Y = 198;
														break;
													}
													mergeLeft = true;
													mergeRight = true;
												}
												else if (num31 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 0;
														rectangle.Y = 252;
														break;
													case 1:
														rectangle.X = 18;
														rectangle.Y = 252;
														break;
													default:
														rectangle.X = 36;
														rectangle.Y = 252;
														break;
													}
													mergeLeft = true;
												}
												else if (num32 == -2)
												{
													switch (num36)
													{
													case 0:
														rectangle.X = 54;
														rectangle.Y = 252;
														break;
													case 1:
														rectangle.X = 72;
														rectangle.Y = 252;
														break;
													default:
														rectangle.X = 90;
														rectangle.Y = 252;
														break;
													}
													mergeRight = true;
												}
											}
											else if (num29 == -2 && num34 == -1 && num31 == -1 && num32 == -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 108;
													rectangle.Y = 144;
													break;
												case 1:
													rectangle.X = 108;
													rectangle.Y = 162;
													break;
												default:
													rectangle.X = 108;
													rectangle.Y = 180;
													break;
												}
												mergeUp = true;
											}
											else if (num29 == -1 && num34 == -2 && num31 == -1 && num32 == -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 108;
													rectangle.Y = 90;
													break;
												case 1:
													rectangle.X = 108;
													rectangle.Y = 108;
													break;
												default:
													rectangle.X = 108;
													rectangle.Y = 126;
													break;
												}
												mergeDown = true;
											}
											else if (num29 == -1 && num34 == -1 && num31 == -2 && num32 == -1)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 0;
													rectangle.Y = 234;
													break;
												case 1:
													rectangle.X = 18;
													rectangle.Y = 234;
													break;
												default:
													rectangle.X = 36;
													rectangle.Y = 234;
													break;
												}
												mergeLeft = true;
											}
											else if (num29 == -1 && num34 == -1 && num31 == -1 && num32 == -2)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 54;
													rectangle.Y = 234;
													break;
												case 1:
													rectangle.X = 72;
													rectangle.Y = 234;
													break;
												default:
													rectangle.X = 90;
													rectangle.Y = 234;
													break;
												}
												mergeRight = true;
											}
										}
									}
									if (rectangle.X < 0 || rectangle.Y < 0)
									{
										if (!flag3)
										{
											flag3 = true;
											if (num29 > -1 && !Main.tileSolid[num29] && num29 != num)
											{
												num29 = -1;
											}
											if (num34 > -1 && !Main.tileSolid[num34] && num34 != num)
											{
												num34 = -1;
											}
											if (num31 > -1 && !Main.tileSolid[num31] && num31 != num)
											{
												num31 = -1;
											}
											if (num32 > -1 && !Main.tileSolid[num32] && num32 != num)
											{
												num32 = -1;
											}
											if (num28 > -1 && !Main.tileSolid[num28] && num28 != num)
											{
												num28 = -1;
											}
											if (num30 > -1 && !Main.tileSolid[num30] && num30 != num)
											{
												num30 = -1;
											}
											if (num33 > -1 && !Main.tileSolid[num33] && num33 != num)
											{
												num33 = -1;
											}
											if (num35 > -1 && !Main.tileSolid[num35] && num35 != num)
											{
												num35 = -1;
											}
										}
										if (num == 2 || num == 23 || num == 60 || num == 70 || num == 109)
										{
											if (num29 == -2)
											{
												num29 = num;
											}
											if (num34 == -2)
											{
												num34 = num;
											}
											if (num31 == -2)
											{
												num31 = num;
											}
											if (num32 == -2)
											{
												num32 = num;
											}
											if (num28 == -2)
											{
												num28 = num;
											}
											if (num30 == -2)
											{
												num30 = num;
											}
											if (num33 == -2)
											{
												num33 = num;
											}
											if (num35 == -2)
											{
												num35 = num;
											}
										}
										if (num29 == num && num34 == num && num31 == num && num32 == num)
										{
											if (num28 != num && num30 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 108;
													rectangle.Y = 18;
													break;
												case 1:
													rectangle.X = 126;
													rectangle.Y = 18;
													break;
												default:
													rectangle.X = 144;
													rectangle.Y = 18;
													break;
												}
											}
											else if (num33 != num && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 108;
													rectangle.Y = 36;
													break;
												case 1:
													rectangle.X = 126;
													rectangle.Y = 36;
													break;
												default:
													rectangle.X = 144;
													rectangle.Y = 36;
													break;
												}
											}
											else if (num28 != num && num33 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 180;
													rectangle.Y = 0;
													break;
												case 1:
													rectangle.X = 180;
													rectangle.Y = 18;
													break;
												default:
													rectangle.X = 180;
													rectangle.Y = 36;
													break;
												}
											}
											else if (num30 != num && num35 != num)
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 198;
													rectangle.Y = 0;
													break;
												case 1:
													rectangle.X = 198;
													rectangle.Y = 18;
													break;
												default:
													rectangle.X = 198;
													rectangle.Y = 36;
													break;
												}
											}
											else
											{
												switch (num36)
												{
												case 0:
													rectangle.X = 18;
													rectangle.Y = 18;
													break;
												case 1:
													rectangle.X = 36;
													rectangle.Y = 18;
													break;
												default:
													rectangle.X = 54;
													rectangle.Y = 18;
													break;
												}
											}
										}
										else if (num29 != num && num34 == num && num31 == num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 0;
												break;
											default:
												rectangle.X = 54;
												rectangle.Y = 0;
												break;
											}
										}
										else if (num29 == num && num34 != num && num31 == num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 36;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 36;
												break;
											default:
												rectangle.X = 54;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 == num && num34 == num && num31 != num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 0;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 0;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 0;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 == num && num34 == num && num31 == num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 72;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 72;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 72;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 != num && num34 == num && num31 != num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 0;
												rectangle.Y = 54;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 54;
												break;
											default:
												rectangle.X = 72;
												rectangle.Y = 54;
												break;
											}
										}
										else if (num29 != num && num34 == num && num31 == num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 54;
												break;
											case 1:
												rectangle.X = 54;
												rectangle.Y = 54;
												break;
											default:
												rectangle.X = 90;
												rectangle.Y = 54;
												break;
											}
										}
										else if (num29 == num && num34 != num && num31 != num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 0;
												rectangle.Y = 72;
												break;
											case 1:
												rectangle.X = 36;
												rectangle.Y = 72;
												break;
											default:
												rectangle.X = 72;
												rectangle.Y = 72;
												break;
											}
										}
										else if (num29 == num && num34 != num && num31 == num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 18;
												rectangle.Y = 72;
												break;
											case 1:
												rectangle.X = 54;
												rectangle.Y = 72;
												break;
											default:
												rectangle.X = 90;
												rectangle.Y = 72;
												break;
											}
										}
										else if (num29 == num && num34 == num && num31 != num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 90;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 90;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 90;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 != num && num34 != num && num31 == num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 108;
												rectangle.Y = 72;
												break;
											case 1:
												rectangle.X = 126;
												rectangle.Y = 72;
												break;
											default:
												rectangle.X = 144;
												rectangle.Y = 72;
												break;
											}
										}
										else if (num29 != num && num34 == num && num31 != num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 108;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 126;
												rectangle.Y = 0;
												break;
											default:
												rectangle.X = 144;
												rectangle.Y = 0;
												break;
											}
										}
										else if (num29 == num && num34 != num && num31 != num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 108;
												rectangle.Y = 54;
												break;
											case 1:
												rectangle.X = 126;
												rectangle.Y = 54;
												break;
											default:
												rectangle.X = 144;
												rectangle.Y = 54;
												break;
											}
										}
										else if (num29 != num && num34 != num && num31 != num && num32 == num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 162;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 162;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 162;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 != num && num34 != num && num31 == num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 216;
												rectangle.Y = 0;
												break;
											case 1:
												rectangle.X = 216;
												rectangle.Y = 18;
												break;
											default:
												rectangle.X = 216;
												rectangle.Y = 36;
												break;
											}
										}
										else if (num29 != num && num34 != num && num31 != num && num32 != num)
										{
											switch (num36)
											{
											case 0:
												rectangle.X = 162;
												rectangle.Y = 54;
												break;
											case 1:
												rectangle.X = 180;
												rectangle.Y = 54;
												break;
											default:
												rectangle.X = 198;
												rectangle.Y = 54;
												break;
											}
										}
									}
									if (rectangle.X <= -1 || rectangle.Y <= -1)
									{
										if (num36 <= 0)
										{
											rectangle.X = 18;
											rectangle.Y = 18;
										}
										else if (num36 == 1)
										{
											rectangle.X = 36;
											rectangle.Y = 18;
										}
										else if (num36 >= 2)
										{
											rectangle.X = 54;
											rectangle.Y = 18;
										}
									}
									tile.frameX = (short)rectangle.X;
									tile.frameY = (short)rectangle.Y;
									if (num != 52 && num != 62 && num != 115)
									{
										goto IL_5cae;
									}
									Config.tilesPretend = true;
									num29 = ((Main.tile[i, j - 1] == null) ? num : (Main.tile[i, j - 1].active ? Main.tile[i, j - 1].type : (-1)));
									Config.tilesPretend = false;
									if (num == 52 && (num29 == 109 || num29 == 115))
									{
										tile.type = 115;
										SquareTileFrame(i, j);
									}
									else
									{
										if (num != 115 || (num29 != 2 && num29 != 52))
										{
											if (num29 != num)
											{
												bool flag4 = false;
												if (num29 == -1)
												{
													flag4 = true;
												}
												else if (num == 52 && num29 != 2)
												{
													flag4 = true;
												}
												else if (num == 62 && num29 != 60)
												{
													flag4 = true;
												}
												else if (num == 115 && num29 != 109)
												{
													flag4 = true;
												}
												if (flag4)
												{
													KillTile(i, j);
												}
											}
											goto IL_5cae;
										}
										tile.type = 52;
										SquareTileFrame(i, j);
									}
								}
							}
						}
					}
				}
				goto end_IL_0000;
				IL_5cae:
				if (!noTileActions && (num == 53 || num == 112 || num == 116 || num == 123))
				{
					if (Main.netMode == 0)
					{
						Config.tilesPretend = true;
						if (Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
						{
							bool flag5 = true;
							if (Main.tile[i, j - 1].active && Main.tile[i, j - 1].type == 21)
							{
								flag5 = false;
							}
							Config.tilesPretend = false;
							if (flag5)
							{
								int type2 = 31;
								switch (num)
								{
								case 59:
									type2 = 39;
									break;
								case 57:
									type2 = 40;
									break;
								case 112:
									type2 = 56;
									break;
								case 116:
									type2 = 67;
									break;
								case 123:
									type2 = 71;
									break;
								}
								tile.active = false;
								int num38 = Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 0.41f, type2, 10, 0f, Main.myPlayer);
								Main.projectile[num38].ai[0] = 1f;
								SquareTileFrame(i, j);
							}
						}
						Config.tilesPretend = false;
					}
					else
					{
						Config.tilesPretend = true;
						if (Main.netMode == 2 && Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
						{
							bool flag6 = true;
							if (Main.tile[i, j - 1].active && Main.tile[i, j - 1].type == 21)
							{
								flag6 = false;
							}
							Config.tilesPretend = false;
							if (flag6)
							{
								int type3 = 31;
								switch (num)
								{
								case 59:
									type3 = 39;
									break;
								case 57:
									type3 = 40;
									break;
								case 112:
									type3 = 56;
									break;
								case 116:
									type3 = 67;
									break;
								case 123:
									type3 = 71;
									break;
								}
								tile.active = false;
								int num39 = Projectile.NewProjectile(i * 16 + 8, j * 16 + 10, 0f, 0.5f, type3, 10, 0f, Main.myPlayer);
								Main.projectile[num39].netUpdate = true;
								NetMessage.SendTileSquare(-1, i, j, 1);
								SquareTileFrame(i, j);
							}
						}
					}
					Config.tilesPretend = false;
				}
				if (rectangle.X != frameX && rectangle.Y != frameY && frameX >= 0 && frameY >= 0)
				{
					bool flag7 = mergeUp;
					bool flag8 = mergeDown;
					bool flag9 = mergeLeft;
					bool flag10 = mergeRight;
					TileFrame(i - 1, j);
					TileFrame(i + 1, j);
					TileFrame(i, j - 1);
					TileFrame(i, j + 1);
					mergeUp = flag7;
					mergeDown = flag8;
					mergeLeft = flag9;
					mergeRight = flag10;
				}
				end_IL_0000:;
			}
			catch
			{
			}
		}
	}
}
