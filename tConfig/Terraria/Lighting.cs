using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Terraria
{
	public static class Lighting
	{
		public static int maxRenderCount = 4;

		public static int dirX;

		public static int dirY;

		public static float brightness = 1f;

		public static float defBrightness = 1f;

		public static int lightMode = 0;

		public static bool RGB = true;

		public static float oldSkyColor = 0f;

		public static float skyColor = 0f;

		public static float lightColor = 0f;

		public static float lightColorG = 0f;

		public static float lightColorB = 0f;

		public static int lightCounter = 0;

		public static int offScreenTiles = 45;

		public static int offScreenTiles2 = 35;

		public static int firstTileX;

		public static int lastTileX;

		public static int firstTileY;

		public static int lastTileY;

		public static float[,] color = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static float[,] colorG = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static float[,] colorB = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static float[,] color2 = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static float[,] colorG2 = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static float[,] colorB2 = new float[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static bool[,] stopLight = new bool[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static bool[,] wetLight = new bool[Main.screenWidth + offScreenTiles * 2 + 10, Main.screenHeight + offScreenTiles * 2 + 10];

		public static int scrX;

		public static int scrY;

		public static int minX;

		public static int maxX;

		public static int minY;

		public static int maxY;

		public static int maxTempLights = 2000;

		public static int[] tempLightX = new int[maxTempLights];

		public static int[] tempLightY = new int[maxTempLights];

		public static float[] tempLight = new float[maxTempLights];

		public static float[] tempLightG = new float[maxTempLights];

		public static float[] tempLightB = new float[maxTempLights];

		public static int tempLightCount;

		public static int firstToLightX;

		public static int firstToLightY;

		public static int lastToLightX;

		public static int lastToLightY;

		public static bool resize = false;

		public static float negLight = 0.04f;

		public static float negLight2 = 0.16f;

		public static float wetLightR = 0.16f;

		public static float wetLightG = 0.16f;

		public static float blueWave = 1f;

		public static int blueDir = 1;

		public static int minX7;

		public static int maxX7;

		public static int minY7;

		public static int maxY7;

		public static int firstTileX7;

		public static int lastTileX7;

		public static int lastTileY7;

		public static int firstTileY7;

		public static int firstToLightX7;

		public static int lastToLightX7;

		public static int firstToLightY7;

		public static int lastToLightY7;

		public static int firstToLightX27;

		public static int lastToLightX27;

		public static int firstToLightY27;

		public static int lastToLightY27;

		public static float[] hookModifyLightVision = new float[2];

		public static void LightTiles(int firstX, int lastX, int firstY, int lastY)
		{
			Main.render = true;
			oldSkyColor = skyColor;
			skyColor = (float)((Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3) / 255f;
			if (lightMode < 2)
			{
				brightness = 1.2f;
				offScreenTiles2 = 34;
				offScreenTiles = 40;
			}
			else
			{
				brightness = 1f;
				offScreenTiles2 = 18;
				offScreenTiles = 23;
			}
			if (Main.player[Main.myPlayer].blind)
			{
				brightness = 1f;
			}
			defBrightness = brightness;
			firstTileX = firstX;
			lastTileX = lastX;
			firstTileY = firstY;
			lastTileY = lastY;
			firstToLightX = firstTileX - offScreenTiles;
			firstToLightY = firstTileY - offScreenTiles;
			lastToLightX = lastTileX + offScreenTiles;
			lastToLightY = lastTileY + offScreenTiles;
			if (firstToLightX < 0)
			{
				firstToLightX = 0;
			}
			if (lastToLightX >= Main.maxTilesX)
			{
				lastToLightX = Main.maxTilesX - 1;
			}
			if (firstToLightY < 0)
			{
				firstToLightY = 0;
			}
			if (lastToLightY >= Main.maxTilesY)
			{
				lastToLightY = Main.maxTilesY - 1;
			}
			int num = firstTileX - offScreenTiles2;
			int num2 = firstTileY - offScreenTiles2;
			int num3 = lastTileX + offScreenTiles2;
			int num4 = lastTileY + offScreenTiles2;
			if (num < 0)
			{
				num = 0;
			}
			if (num3 >= Main.maxTilesX)
			{
				num3 = Main.maxTilesX - 1;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num4 >= Main.maxTilesY)
			{
				num4 = Main.maxTilesY - 1;
			}
			lightCounter++;
			Main.renderCount++;
			int num5 = Main.screenWidth / 16 + offScreenTiles * 2;
			int num6 = Main.screenHeight / 16 + offScreenTiles * 2;
			Vector2 vector = Main.screenLastPosition;
			doColors();
			if (Main.renderCount == 2)
			{
				vector = Main.screenPosition;
				int num7 = (int)(Main.screenPosition.X / 16f) - scrX;
				int num8 = (int)(Main.screenPosition.Y / 16f) - scrY;
				if (num7 > 4)
				{
					num7 = 0;
				}
				if (num8 > 4)
				{
					num8 = 0;
				}
				if (RGB)
				{
					for (int i = 0; i < num5; i++)
					{
						if (i + num7 < 0)
						{
							continue;
						}
						for (int j = 0; j < num6; j++)
						{
							if (j + num8 >= 0)
							{
								color[i, j] = color2[i + num7, j + num8];
								colorG[i, j] = colorG2[i + num7, j + num8];
								colorB[i, j] = colorB2[i + num7, j + num8];
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < num5; k++)
					{
						if (k + num7 < 0)
						{
							continue;
						}
						for (int l = 0; l < num6; l++)
						{
							if (l + num8 >= 0)
							{
								color[k, l] = color2[k + num7, l + num8];
								colorG[k, l] = color2[k + num7, l + num8];
								colorB[k, l] = color2[k + num7, l + num8];
							}
						}
					}
				}
			}
			if (Main.renderCount != 2 && !resize && !Main.renderNow)
			{
				if (Math.Abs(Main.screenPosition.X / 16f - vector.X / 16f) < 5f)
				{
					while ((int)(Main.screenPosition.X / 16f) < (int)(vector.X / 16f))
					{
						vector.X -= 16f;
						for (int num9 = num5 - 1; num9 > 1; num9--)
						{
							for (int m = 0; m < num6; m++)
							{
								color[num9, m] = color[num9 - 1, m];
								colorG[num9, m] = colorG[num9 - 1, m];
								colorB[num9, m] = colorB[num9 - 1, m];
							}
						}
					}
					while ((int)(Main.screenPosition.X / 16f) > (int)(vector.X / 16f))
					{
						vector.X += 16f;
						for (int n = 0; n < num5 - 1; n++)
						{
							for (int num10 = 0; num10 < num6; num10++)
							{
								color[n, num10] = color[n + 1, num10];
								colorG[n, num10] = colorG[n + 1, num10];
								colorB[n, num10] = colorB[n + 1, num10];
							}
						}
					}
				}
				if (Math.Abs(Main.screenPosition.Y / 16f - vector.Y / 16f) < 5f)
				{
					while ((int)(Main.screenPosition.Y / 16f) < (int)(vector.Y / 16f))
					{
						vector.Y -= 16f;
						for (int num11 = num6 - 1; num11 > 1; num11--)
						{
							for (int num12 = 0; num12 < num5; num12++)
							{
								color[num12, num11] = color[num12, num11 - 1];
								colorG[num12, num11] = colorG[num12, num11 - 1];
								colorB[num12, num11] = colorB[num12, num11 - 1];
							}
						}
					}
					while ((int)(Main.screenPosition.Y / 16f) > (int)(vector.Y / 16f))
					{
						vector.Y += 16f;
						for (int num13 = 0; num13 < num6 - 1; num13++)
						{
							for (int num14 = 0; num14 < num5 - 1; num14++)
							{
								color[num14, num13] = color[num14, num13 + 1];
								colorG[num14, num13] = colorG[num14, num13 + 1];
								colorB[num14, num13] = colorB[num14, num13 + 1];
							}
						}
					}
				}
				if (oldSkyColor != skyColor)
				{
					for (int num15 = firstToLightX; num15 < lastToLightX; num15++)
					{
						for (int num16 = firstToLightY; num16 < lastToLightY; num16++)
						{
							if (Main.tile[num15, num16] == null)
							{
								Main.tile[num15, num16] = new Tile();
							}
							if ((!Main.tile[num15, num16].active || !Main.tileNoSunLight[Main.tile[num15, num16].type]) && color[num15 - firstToLightX, num16 - firstToLightY] < skyColor && (Main.tile[num15, num16].wall == 0 || Main.tile[num15, num16].wall == 21) && (double)num16 < Main.worldSurface && Main.tile[num15, num16].liquid < 200)
							{
								if (color[num15 - firstToLightX, num16 - firstToLightY] < skyColor)
								{
									color[num15 - firstToLightX, num16 - firstToLightY] = (float)(int)Main.tileColor.R / 255f;
								}
								if (colorG[num15 - firstToLightX, num16 - firstToLightY] < skyColor)
								{
									colorG[num15 - firstToLightX, num16 - firstToLightY] = (float)(int)Main.tileColor.G / 255f;
								}
								if (colorB[num15 - firstToLightX, num16 - firstToLightY] < skyColor)
								{
									colorB[num15 - firstToLightX, num16 - firstToLightY] = (float)(int)Main.tileColor.B / 255f;
								}
							}
						}
					}
				}
			}
			else
			{
				lightCounter = 0;
			}
			if (Main.renderCount <= maxRenderCount)
			{
				return;
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			_ = stopwatch.ElapsedMilliseconds;
			resize = false;
			Main.drawScene = true;
			ResetRange();
			if (lightMode == 0 || lightMode == 3)
			{
				RGB = true;
			}
			else
			{
				RGB = false;
			}
			int num17 = 0;
			int num18 = Main.screenWidth / 16 + offScreenTiles * 2 + 10;
			int num19 = 0;
			int num20 = Main.screenHeight / 16 + offScreenTiles * 2 + 10;
			for (int num21 = num17; num21 < num18; num21++)
			{
				for (int num22 = num19; num22 < num20; num22++)
				{
					color2[num21, num22] = 0f;
					colorG2[num21, num22] = 0f;
					colorB2[num21, num22] = 0f;
					stopLight[num21, num22] = false;
					wetLight[num21, num22] = false;
				}
			}
			for (int num23 = 0; num23 < tempLightCount; num23++)
			{
				if (tempLightX[num23] - firstTileX + offScreenTiles >= 0 && tempLightX[num23] - firstTileX + offScreenTiles < Main.screenWidth / 16 + offScreenTiles * 2 + 10 && tempLightY[num23] - firstTileY + offScreenTiles >= 0 && tempLightY[num23] - firstTileY + offScreenTiles < Main.screenHeight / 16 + offScreenTiles * 2 + 10)
				{
					int num24 = tempLightX[num23] - firstTileX + offScreenTiles;
					int num25 = tempLightY[num23] - firstTileY + offScreenTiles;
					if (color2[num24, num25] < tempLight[num23])
					{
						color2[num24, num25] = tempLight[num23];
					}
					if (colorG2[num24, num25] < tempLightG[num23])
					{
						colorG2[num24, num25] = tempLightG[num23];
					}
					if (colorB2[num24, num25] < tempLightB[num23])
					{
						colorB2[num24, num25] = tempLightB[num23];
					}
				}
			}
			if (Main.wof >= 0 && Main.player[Main.myPlayer].gross)
			{
				try
				{
					int num26 = (int)Main.screenPosition.Y / 16 - 10;
					int num27 = (int)(Main.screenPosition.Y + (float)Main.screenHeight) / 16 + 10;
					int num28 = (int)Main.npc[Main.wof].position.X / 16;
					num28 = ((Main.npc[Main.wof].direction <= 0) ? (num28 + 2) : (num28 - 3));
					int num29 = num28 + 8;
					float num30 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
					float num31 = 0.3f;
					float num32 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
					num30 *= 0.2f;
					num31 *= 0.1f;
					num32 *= 0.3f;
					for (int num33 = num28; num33 <= num29; num33++)
					{
						for (int num34 = num26; num34 <= num27; num34++)
						{
							if (color2[num33 - firstToLightX, num34 - firstToLightY] < num30)
							{
								color2[num33 - firstToLightX, num34 - firstToLightY] = num30;
							}
							if (colorG2[num33 - firstToLightX, num34 - firstToLightY] < num31)
							{
								colorG2[num33 - firstToLightX, num34 - firstToLightY] = num31;
							}
							if (colorB2[num33 - firstToLightX, num34 - firstToLightY] < num32)
							{
								colorB2[num33 - firstToLightX, num34 - firstToLightY] = num32;
							}
						}
					}
				}
				catch
				{
				}
			}
			if (!Main.renderNow)
			{
				Main.oldTempLightCount = tempLightCount;
				tempLightCount = 0;
			}
			if (Main.gamePaused)
			{
				tempLightCount = Main.oldTempLightCount;
			}
			Main.sandTiles = 0;
			Main.evilTiles = 0;
			Main.snowTiles = 0;
			Main.holyTiles = 0;
			Main.meteorTiles = 0;
			Main.jungleTiles = 0;
			Main.dungeonTiles = 0;
			Main.tileCount = new int[Config.customTileAmt + 150 + 1];
			Main.musicBox = -1;
			num17 = firstToLightX;
			num18 = lastToLightX;
			num19 = firstToLightY;
			num20 = lastToLightY;
			for (int num35 = num17; num35 < num18; num35++)
			{
				for (int num36 = num19; num36 < num20; num36++)
				{
					if (Main.tile[num35, num36] == null)
					{
						Main.tile[num35, num36] = new Tile();
					}
					if ((!Main.tile[num35, num36].active || !Main.tileNoSunLight[Main.tile[num35, num36].type]) && color2[num35 - firstToLightX, num36 - firstToLightY] < skyColor && (Main.tile[num35, num36].wall == 0 || Main.tile[num35, num36].wall == 21) && (double)num36 < Main.worldSurface && Main.tile[num35, num36].liquid < 200)
					{
						if (color2[num35 - firstToLightX, num36 - firstToLightY] < skyColor)
						{
							color2[num35 - firstToLightX, num36 - firstToLightY] = (float)(int)Main.tileColor.R / 255f;
						}
						if (colorG2[num35 - firstToLightX, num36 - firstToLightY] < skyColor)
						{
							colorG2[num35 - firstToLightX, num36 - firstToLightY] = (float)(int)Main.tileColor.G / 255f;
						}
						if (colorB2[num35 - firstToLightX, num36 - firstToLightY] < skyColor)
						{
							colorB2[num35 - firstToLightX, num36 - firstToLightY] = (float)(int)Main.tileColor.B / 255f;
						}
					}
				}
			}
			for (int num37 = num17; num37 < num18; num37++)
			{
				for (int num38 = num19; num38 < num20; num38++)
				{
					int num39 = num37 - firstToLightX;
					int num40 = num38 - firstToLightY;
					if (Main.tile[num37, num38] == null)
					{
						Main.tile[num37, num38] = new Tile();
					}
					int zoneX = Main.zoneX;
					int zoneY = Main.zoneY;
					int num41 = (num18 - num17 - zoneX) / 2;
					int num42 = (num20 - num19 - zoneY) / 2;
					ushort type2;
					ushort type3;
					if (Main.tile[num37, num38].active)
					{
						if (num37 > num17 + num41 && num37 < num18 - num41 && num38 > num19 + num42 && num38 < num20 - num42)
						{
							ushort type = Main.tile[num37, num38].type;
							Main.tileCount[type]++;
							switch (type)
							{
							case 27:
								Main.evilTiles -= 5;
								break;
							case 23:
							case 24:
							case 25:
							case 32:
								Main.evilTiles++;
								break;
							case 41:
							case 43:
							case 44:
								Main.dungeonTiles++;
								break;
							case 53:
								Main.sandTiles++;
								break;
							case 37:
								Main.meteorTiles++;
								break;
							case 60:
							case 61:
							case 62:
							case 84:
								Main.jungleTiles++;
								break;
							case 109:
							case 110:
							case 113:
							case 117:
								Main.holyTiles++;
								break;
							case 112:
								Main.sandTiles++;
								Main.evilTiles++;
								break;
							case 116:
								Main.sandTiles++;
								Main.holyTiles++;
								break;
							case 147:
							case 148:
								Main.snowTiles++;
								break;
							case 139:
								if (Main.tile[num37, num38].frameX >= 36)
								{
									int num43 = 0;
									for (int num44 = Main.tile[num37, num38].frameY / 18; num44 >= 2; num44 -= 2)
									{
										num43++;
									}
									Main.musicBox = num43;
								}
								break;
							}
						}
						if (Main.tileBlockLight[Main.tile[num37, num38].type] && Main.tile[num37, num38].type != 131)
						{
							stopLight[num39, num40] = true;
						}
						if (Main.tileLighted[Main.tile[num37, num38].type])
						{
							if (RGB)
							{
								type2 = Main.tile[num37, num38].type;
								float num45 = 0f;
								float num46 = 0f;
								float num47 = 0f;
								if (Codable.RunTileMethodRef(false, new Vector2(num37, num38), type2, "AddLight", num37, num38, num45, num46, num47))
								{
									if (Codable.customMethodRefReturn == null)
									{
										goto IL_14bd;
									}
									object[] customMethodRefReturn = Codable.customMethodRefReturn;
									num45 = (float)customMethodRefReturn[2];
									num46 = (float)customMethodRefReturn[3];
									num47 = (float)customMethodRefReturn[4];
									if (color2[num39, num40] < num45)
									{
										color2[num39, num40] = num45;
									}
									if (colorG2[num39, num40] < num46)
									{
										colorG2[num39, num40] = num46;
									}
									if (colorB2[num39, num40] < num47)
									{
										colorB2[num39, num40] = num47;
									}
									Codable.customMethodRefReturn = null;
								}
								else
								{
									if (!Config.tileDefs.light.ContainsKey(type2))
									{
										goto IL_14bd;
									}
									float[] array = Config.tileDefs.light[type2];
									num45 = array[0];
									num46 = array[1];
									num47 = array[2];
									if (color2[num39, num40] < num45)
									{
										color2[num39, num40] = num45;
									}
									if (colorG2[num39, num40] < num46)
									{
										colorG2[num39, num40] = num46;
									}
									if (colorB2[num39, num40] < num47)
									{
										colorB2[num39, num40] = num47;
									}
								}
							}
							else
							{
								type3 = Main.tile[num37, num38].type;
								float num48 = 0f;
								float num49 = 0f;
								float num50 = 0f;
								if (Codable.RunTileMethodRef(false, new Vector2(num37, num38), type3, "AddLight", num37, num38, num48, num49, num50))
								{
									if (Codable.customMethodRefReturn == null)
									{
										goto IL_2af4;
									}
									object[] customMethodRefReturn2 = Codable.customMethodRefReturn;
									num48 = (float)customMethodRefReturn2[2];
									num49 = (float)customMethodRefReturn2[3];
									num50 = (float)customMethodRefReturn2[4];
									float num51 = (num48 + num49 + num50) / 3f;
									if (color2[num39, num40] < num51)
									{
										color2[num39, num40] = num51;
									}
									if (colorG2[num39, num40] < num51)
									{
										colorG2[num39, num40] = num51;
									}
									if (colorB2[num39, num40] < num51)
									{
										colorB2[num39, num40] = num51;
									}
									Codable.customMethodRefReturn = null;
								}
								else
								{
									if (!Config.tileDefs.light.ContainsKey(type3))
									{
										goto IL_2af4;
									}
									float[] array2 = Config.tileDefs.light[type3];
									num48 = array2[0];
									num49 = array2[1];
									num50 = array2[2];
									float num52 = (num48 + num49 + num50) / 3f;
									if (color2[num39, num40] < num52)
									{
										color2[num39, num40] = num52;
									}
									if (colorG2[num39, num40] < num52)
									{
										colorG2[num39, num40] = num52;
									}
									if (colorB2[num39, num40] < num52)
									{
										colorB2[num39, num40] = num52;
									}
								}
							}
						}
					}
					goto IL_31ef;
					IL_14bd:
					switch (type2)
					{
					case 4:
					{
						float num65 = 1f;
						float num66 = 0.95f;
						float num67 = 0.8f;
						if (Main.tile[num37, num38].frameX < 66)
						{
							switch (Main.tile[num37, num38].frameY / 22)
							{
							case 1:
								num65 = 0f;
								num66 = 0.1f;
								num67 = 1.3f;
								break;
							case 2:
								num65 = 1f;
								num66 = 0.1f;
								num67 = 0.1f;
								break;
							case 3:
								num65 = 0f;
								num66 = 1f;
								num67 = 0.1f;
								break;
							case 4:
								num65 = 0.9f;
								num66 = 0f;
								num67 = 0.9f;
								break;
							case 5:
								num65 = 1.3f;
								num66 = 1.3f;
								num67 = 1.3f;
								break;
							case 6:
								num65 = 0.9f;
								num66 = 0.9f;
								num67 = 0f;
								break;
							case 7:
								num65 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
								num66 = 0.3f;
								num67 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
								break;
							case 8:
								num67 = 0.7f;
								num65 = 0.85f;
								num66 = 1f;
								break;
							}
							if (color2[num39, num40] < num65)
							{
								color2[num39, num40] = num65;
							}
							if (colorG2[num39, num40] < num66)
							{
								colorG2[num39, num40] = num66;
							}
							if (colorB2[num39, num40] < num67)
							{
								colorB2[num39, num40] = num67;
							}
						}
						break;
					}
					case 26:
					case 31:
					{
						float num56 = (float)Main.rand.Next(-5, 6) * 0.0025f;
						if (color2[num39, num40] < 0.31f + num56)
						{
							color2[num39, num40] = 0.31f + num56;
						}
						if (colorG2[num39, num40] < 0.1f + num56)
						{
							colorG2[num39, num40] = 0.1f;
						}
						if (colorB2[num39, num40] < 0.44f + num56 * 2f)
						{
							colorB2[num39, num40] = 0.44f + num56 * 2f;
						}
						break;
					}
					case 33:
						if (Main.tile[num37, num38].frameX == 0)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.95)
							{
								colorG2[num39, num40] = 0.95f;
							}
							if ((double)colorB2[num39, num40] < 0.65)
							{
								colorB2[num39, num40] = 0.65f;
							}
						}
						break;
					case 34:
					case 35:
						if (Main.tile[num37, num38].frameX < 54)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.95)
							{
								colorG2[num39, num40] = 0.95f;
							}
							if ((double)colorB2[num39, num40] < 0.8)
							{
								colorB2[num39, num40] = 0.8f;
							}
						}
						break;
					case 36:
						if (Main.tile[num37, num38].frameX < 54)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.95)
							{
								colorG2[num39, num40] = 0.95f;
							}
							if ((double)colorB2[num39, num40] < 0.65)
							{
								colorB2[num39, num40] = 0.65f;
							}
						}
						break;
					case 37:
						if ((double)color2[num39, num40] < 0.56)
						{
							color2[num39, num40] = 0.56f;
						}
						if ((double)colorG2[num39, num40] < 0.43)
						{
							colorG2[num39, num40] = 0.43f;
						}
						if ((double)colorB2[num39, num40] < 0.15)
						{
							colorB2[num39, num40] = 0.15f;
						}
						break;
					case 42:
						if (Main.tile[num37, num38].frameX == 0)
						{
							if (color2[num39, num40] < 0.65f)
							{
								color2[num39, num40] = 0.65f;
							}
							if (colorG2[num39, num40] < 0.8f)
							{
								colorG2[num39, num40] = 0.8f;
							}
							if (colorB2[num39, num40] < 0.54f)
							{
								colorB2[num39, num40] = 0.54f;
							}
						}
						break;
					case 61:
						if (Main.tile[num37, num38].frameX == 144)
						{
							if (color2[num39, num40] < 0.42f)
							{
								color2[num39, num40] = 0.42f;
							}
							if (colorG2[num39, num40] < 0.81f)
							{
								colorG2[num39, num40] = 0.81f;
							}
							if (colorB2[num39, num40] < 0.52f)
							{
								colorB2[num39, num40] = 0.52f;
							}
						}
						break;
					case 49:
						if (color2[num39, num40] < 0.3f)
						{
							color2[num39, num40] = 0.3f;
						}
						if (colorG2[num39, num40] < 0.3f)
						{
							colorG2[num39, num40] = 0.3f;
						}
						if (colorB2[num39, num40] < 0.75f)
						{
							colorB2[num39, num40] = 0.75f;
						}
						break;
					case 70:
					case 71:
					case 72:
					{
						float num64 = (float)Main.rand.Next(28, 42) * 0.005f;
						num64 += (float)(270 - Main.mouseTextColor) / 500f;
						if (color2[num39, num40] < 0.1f + num64)
						{
							color2[num39, num40] = 0.1f;
						}
						if (colorG2[num39, num40] < 0.3f + num64 / 2f)
						{
							colorG2[num39, num40] = 0.3f + num64 / 2f;
						}
						if (colorB2[num39, num40] < 0.6f + num64)
						{
							colorB2[num39, num40] = 0.6f + num64;
						}
						break;
					}
					case 77:
						if (color2[num39, num40] < 0.75f)
						{
							color2[num39, num40] = 0.75f;
						}
						if (colorG2[num39, num40] < 0.45f)
						{
							colorG2[num39, num40] = 0.45f;
						}
						if (colorB2[num39, num40] < 0.25f)
						{
							colorB2[num39, num40] = 0.25f;
						}
						break;
					case 83:
						if (Main.tile[num37, num38].frameX == 18 && !Main.dayTime)
						{
							if ((double)color2[num39, num40] < 0.1)
							{
								color2[num39, num40] = 0.1f;
							}
							if ((double)colorG2[num39, num40] < 0.4)
							{
								colorG2[num39, num40] = 0.4f;
							}
							if ((double)colorB2[num39, num40] < 0.6)
							{
								colorB2[num39, num40] = 0.6f;
							}
						}
						break;
					case 84:
						switch (Main.tile[num37, num38].frameX / 18)
						{
						case 2:
						{
							float num62 = 270 - Main.mouseTextColor;
							num62 /= 800f;
							if (num62 > 1f)
							{
								num62 = 1f;
							}
							if (num62 < 0f)
							{
								num62 = 0f;
							}
							float num63 = num62;
							if (color2[num39, num40] < num63 * 0.7f)
							{
								color2[num39, num40] = num63 * 0.7f;
							}
							if (colorG2[num39, num40] < num63)
							{
								colorG2[num39, num40] = num63;
							}
							if (colorB2[num39, num40] < num63 * 0.1f)
							{
								colorB2[num39, num40] = num63 * 0.1f;
							}
							break;
						}
						case 5:
						{
							float num61 = 0.9f;
							if (color2[num39, num40] < num61)
							{
								color2[num39, num40] = num61;
							}
							if (colorG2[num39, num40] < num61 * 0.8f)
							{
								colorG2[num39, num40] = num61 * 0.8f;
							}
							if (colorB2[num39, num40] < num61 * 0.2f)
							{
								colorB2[num39, num40] = num61 * 0.2f;
							}
							break;
						}
						}
						break;
					case 92:
						if (Main.tile[num37, num38].frameY <= 18 && Main.tile[num37, num38].frameX == 0)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if (colorG2[num39, num40] < 1f)
							{
								colorG2[num39, num40] = 1f;
							}
							if (colorB2[num39, num40] < 1f)
							{
								colorB2[num39, num40] = 1f;
							}
						}
						break;
					case 93:
						if (Main.tile[num37, num38].frameY == 0 && Main.tile[num37, num38].frameX == 0)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.97)
							{
								colorG2[num39, num40] = 0.97f;
							}
							if ((double)colorB2[num39, num40] < 0.85)
							{
								colorB2[num39, num40] = 0.85f;
							}
						}
						break;
					case 95:
						if (Main.tile[num37, num38].frameX < 36)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.95)
							{
								colorG2[num39, num40] = 0.95f;
							}
							if ((double)colorB2[num39, num40] < 0.8)
							{
								colorB2[num39, num40] = 0.8f;
							}
						}
						break;
					case 98:
						if (Main.tile[num37, num38].frameY == 0)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.97)
							{
								colorG2[num39, num40] = 0.97f;
							}
							if ((double)colorB2[num39, num40] < 0.85)
							{
								colorB2[num39, num40] = 0.85f;
							}
						}
						break;
					case 100:
						if (Main.tile[num37, num38].frameX < 36)
						{
							if (color2[num39, num40] < 1f)
							{
								color2[num39, num40] = 1f;
							}
							if ((double)colorG2[num39, num40] < 0.95)
							{
								colorG2[num39, num40] = 0.95f;
							}
							if ((double)colorB2[num39, num40] < 0.65)
							{
								colorB2[num39, num40] = 0.65f;
							}
						}
						break;
					case 125:
					{
						float num57 = (float)Main.rand.Next(28, 42) * 0.01f;
						num57 += (float)(270 - Main.mouseTextColor) / 800f;
						if ((double)colorG2[num39, num40] < 0.1 * (double)num57)
						{
							colorG2[num39, num40] = 0.3f * num57;
						}
						if ((double)colorB2[num39, num40] < 0.3 * (double)num57)
						{
							colorB2[num39, num40] = 0.6f * num57;
						}
						break;
					}
					case 126:
						if (Main.tile[num37, num38].frameX < 36)
						{
							if (color2[num39, num40] < (float)Main.DiscoR / 255f)
							{
								color2[num39, num40] = (float)Main.DiscoR / 255f;
							}
							if (colorG2[num39, num40] < (float)Main.DiscoG / 255f)
							{
								colorG2[num39, num40] = (float)Main.DiscoG / 255f;
							}
							if (colorB2[num39, num40] < (float)Main.DiscoB / 255f)
							{
								colorB2[num39, num40] = (float)Main.DiscoB / 255f;
							}
						}
						break;
					case 129:
					{
						float num53;
						float num54;
						float num55;
						if (Main.tile[num37, num38].frameX == 0 || Main.tile[num37, num38].frameX == 54 || Main.tile[num37, num38].frameX == 108)
						{
							num53 = 0.05f;
							num54 = 0.25f;
							num55 = 0f;
						}
						else if (Main.tile[num37, num38].frameX == 18 || Main.tile[num37, num38].frameX == 72 || Main.tile[num37, num38].frameX == 126)
						{
							num55 = 0.2f;
							num54 = 0.15f;
							num53 = 0f;
						}
						else
						{
							num54 = 0.2f;
							num55 = 0.1f;
							num53 = 0f;
						}
						if (color2[num39, num40] < num55)
						{
							color2[num39, num40] = num55 * (float)Main.rand.Next(970, 1031) * 0.001f;
						}
						if (colorG2[num39, num40] < num53)
						{
							colorG2[num39, num40] = num53 * (float)Main.rand.Next(970, 1031) * 0.001f;
						}
						if (colorB2[num39, num40] < num54)
						{
							colorB2[num39, num40] = num54 * (float)Main.rand.Next(970, 1031) * 0.001f;
						}
						break;
					}
					case 149:
					{
						float num58;
						float num59;
						float num60;
						if (Main.tile[num37, num38].frameX == 0)
						{
							num58 = 0.2f;
							num59 = 0.5f;
							num60 = 0.1f;
						}
						else if (Main.tile[num37, num38].frameX == 18)
						{
							num60 = 0.5f;
							num59 = 0.1f;
							num58 = 0.1f;
						}
						else
						{
							num59 = 0.1f;
							num60 = 0.2f;
							num58 = 0.5f;
						}
						if (Main.tile[num37, num38].frameX <= 36)
						{
							if (color2[num39, num40] < num60)
							{
								color2[num39, num40] = num60 * (float)Main.rand.Next(970, 1031) * 0.001f;
							}
							if (colorG2[num39, num40] < num58)
							{
								colorG2[num39, num40] = num58 * (float)Main.rand.Next(970, 1031) * 0.001f;
							}
							if (colorB2[num39, num40] < num59)
							{
								colorB2[num39, num40] = num59 * (float)Main.rand.Next(970, 1031) * 0.001f;
							}
						}
						break;
					}
					case 17:
					case 133:
						if (color2[num39, num40] < 0.83f)
						{
							color2[num39, num40] = 0.83f;
						}
						if (colorG2[num39, num40] < 0.6f)
						{
							colorG2[num39, num40] = 0.6f;
						}
						if (colorB2[num39, num40] < 0.5f)
						{
							colorB2[num39, num40] = 0.5f;
						}
						break;
					case 22:
					case 140:
						if ((double)color2[num39, num40] < 0.12)
						{
							color2[num39, num40] = 0.12f;
						}
						if ((double)colorG2[num39, num40] < 0.07)
						{
							colorG2[num39, num40] = 0.07f;
						}
						if ((double)colorB2[num39, num40] < 0.32)
						{
							colorB2[num39, num40] = 0.32f;
						}
						break;
					}
					goto IL_31ef;
					IL_2af4:
					switch (type3)
					{
					case 22:
						if (color2[num39, num40] < 0.2f)
						{
							color2[num39, num40] = 0.2f;
						}
						break;
					case 4:
						if (Main.tile[num37, num38].frameX < 66)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 26:
					case 31:
					{
						float num75 = (float)Main.rand.Next(-5, 6) * 0.01f;
						if (color2[num39, num40] < 0.4f + num75)
						{
							color2[num39, num40] = 0.4f + num75;
						}
						break;
					}
					case 33:
						if (Main.tile[num37, num38].frameX == 0)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 34:
					case 35:
					case 36:
						if (Main.tile[num37, num38].frameX < 54)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 37:
						if (color2[num39, num40] < 0.5f)
						{
							color2[num39, num40] = 0.5f;
						}
						break;
					case 42:
						if (Main.tile[num37, num38].frameX == 0)
						{
							color2[num39, num40] = 0.75f;
						}
						break;
					case 61:
						if (Main.tile[num37, num38].frameX == 144 && color2[num39, num40] < 0.75f)
						{
							color2[num39, num40] = 0.75f;
						}
						break;
					case 49:
						if (color2[num39, num40] < 0.1f)
						{
							color2[num39, num40] = 0.7f;
						}
						break;
					case 70:
					case 71:
					case 72:
					{
						float num74 = (float)Main.rand.Next(38, 43) * 0.01f;
						if (color2[num39, num40] < num74)
						{
							color2[num39, num40] = num74;
						}
						break;
					}
					case 84:
					{
						int num70 = Main.tile[num37, num38].frameX / 18;
						float num71 = 0f;
						switch (num70)
						{
						case 2:
						{
							float num72 = 270 - Main.mouseTextColor;
							num72 /= 500f;
							if (num72 > 1f)
							{
								num72 = 1f;
							}
							if (num72 < 0f)
							{
								num72 = 0f;
							}
							num71 = num72;
							break;
						}
						case 5:
							num71 = 0.7f;
							break;
						}
						if (color2[num39, num40] < num71)
						{
							color2[num39, num40] = num71;
						}
						break;
					}
					case 77:
						if (color2[num39, num40] < 0.6f)
						{
							color2[num39, num40] = 0.6f;
						}
						break;
					case 92:
						if (Main.tile[num37, num38].frameY <= 18 && Main.tile[num37, num38].frameX == 0)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 93:
						if (Main.tile[num37, num38].frameY == 0 && Main.tile[num37, num38].frameX == 0)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 95:
						if (Main.tile[num37, num38].frameX < 36 && color2[num39, num40] < 0.85f)
						{
							color2[num39, num40] = 0.9f;
						}
						break;
					case 98:
						if (Main.tile[num37, num38].frameY == 0)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 100:
						if (Main.tile[num37, num38].frameX < 36)
						{
							color2[num39, num40] = 1f;
						}
						break;
					case 125:
					{
						float num73 = (float)Main.rand.Next(28, 42) * 0.007f;
						num73 += (float)(270 - Main.mouseTextColor) / 900f;
						if ((double)color2[num39, num40] < 0.5 * (double)num73)
						{
							color2[num39, num40] = 0.3f * num73;
						}
						break;
					}
					case 126:
						if (Main.tile[num37, num38].frameX < 36 && color2[num39, num40] < 0.3f)
						{
							color2[num39, num40] = 0.3f;
						}
						break;
					case 129:
					{
						float num69 = 0.08f;
						if (color2[num39, num40] < num69)
						{
							color2[num39, num40] = num69 * (float)Main.rand.Next(970, 1031) * 0.001f;
						}
						break;
					}
					case 149:
						if (Main.tile[num37, num38].frameX <= 36)
						{
							float num68 = 0.4f;
							if (color2[num39, num40] < num68)
							{
								color2[num39, num40] = num68 * (float)Main.rand.Next(970, 1031) * 0.001f;
							}
						}
						break;
					case 17:
					case 133:
						if (color2[num39, num40] < 0.75f)
						{
							color2[num39, num40] = 0.75f;
						}
						break;
					}
					goto IL_31ef;
					IL_31ef:
					if (Main.tile[num37, num38].lava && Main.tile[num37, num38].liquid > 0)
					{
						if (RGB)
						{
							float num76 = (float)((int)Main.tile[num37, num38].liquid / 255) * 0.41f + 0.14f;
							num76 = 0.55f;
							num76 += (float)(270 - Main.mouseTextColor) / 900f;
							if (color2[num39, num40] < num76)
							{
								color2[num39, num40] = num76;
							}
							if (colorG2[num39, num40] < num76)
							{
								colorG2[num39, num40] = num76 * 0.6f;
							}
							if (colorB2[num39, num40] < num76)
							{
								colorB2[num39, num40] = num76 * 0.2f;
							}
						}
						else
						{
							float num77 = (float)((int)Main.tile[num37, num38].liquid / 255) * 0.38f + 0.08f;
							num77 += (float)(270 - Main.mouseTextColor) / 2000f;
							if (color2[num39, num40] < num77)
							{
								color2[num39, num40] = num77;
							}
						}
					}
					else if (Main.tile[num37, num38].liquid > 128)
					{
						wetLight[num39, num40] = true;
					}
					if (RGB)
					{
						if (color2[num39, num40] > 0f || colorG2[num39, num40] > 0f || colorB2[num39, num40] > 0f)
						{
							if (minX > num39)
							{
								minX = num39;
							}
							if (maxX < num39 + 1)
							{
								maxX = num39 + 1;
							}
							if (minY > num40)
							{
								minY = num40;
							}
							if (maxY < num40 + 1)
							{
								maxY = num40 + 1;
							}
						}
					}
					else if (color2[num39, num40] > 0f)
					{
						if (minX > num39)
						{
							minX = num39;
						}
						if (maxX < num39 + 1)
						{
							maxX = num39 + 1;
						}
						if (minY > num40)
						{
							minY = num40;
						}
						if (maxY < num40 + 1)
						{
							maxY = num40 + 1;
						}
					}
				}
			}
			int num78 = Biome.Biomes["Hallow"].CountNum();
			Main.holyTiles = num78 - Biome.Biomes["Corruption"].CountNum();
			Main.evilTiles = Biome.Biomes["Corruption"].CountNum() - num78;
			if (Main.holyTiles < 0)
			{
				Main.holyTiles = 0;
			}
			if (Main.evilTiles < 0)
			{
				Main.evilTiles = 0;
			}
			Main.sandTiles = Biome.Biomes["Desert"].CountNum();
			Main.snowTiles = Biome.Biomes["Snow"].CountNum();
			Main.meteorTiles = Biome.Biomes["Meteor"].CountNum();
			Main.jungleTiles = Biome.Biomes["Jungle"].CountNum();
			Main.dungeonTiles = Biome.Biomes["Dungeon"].CountNum();
			minX += firstToLightX;
			maxX += firstToLightX;
			minY += firstToLightY;
			maxY += firstToLightY;
			minX7 = minX;
			maxX7 = maxX;
			minY7 = minY;
			maxY7 = maxY;
			firstTileX7 = firstTileX;
			lastTileX7 = lastTileX;
			lastTileY7 = lastTileY;
			firstTileY7 = firstTileY;
			firstToLightX7 = firstToLightX;
			lastToLightX7 = lastToLightX;
			firstToLightY7 = firstToLightY;
			lastToLightY7 = lastToLightY;
			firstToLightX27 = num;
			lastToLightX27 = num3;
			firstToLightY27 = num2;
			lastToLightY27 = num4;
			scrX = (int)Main.screenPosition.X / 16;
			scrY = (int)Main.screenPosition.Y / 16;
			Main.renderCount = 0;
			Main.lightTimer[0] = stopwatch.ElapsedMilliseconds;
			doColors();
		}

		public static void doColors()
		{
			Stopwatch stopwatch = new Stopwatch();
			if (lightMode < 2)
			{
				blueWave += (float)blueDir * 0.0005f;
				if (blueWave > 1f)
				{
					blueWave = 1f;
					blueDir = -1;
				}
				else if (blueWave < 0.97f)
				{
					blueWave = 0.97f;
					blueDir = 1;
				}
				if (RGB)
				{
					negLight = 0.91f;
					negLight2 = 0.56f;
					wetLightG = 0.97f * negLight * blueWave;
					wetLightR = 0.88f * negLight * blueWave;
				}
				else
				{
					negLight = 0.9f;
					negLight2 = 0.54f;
					wetLightR = 0.95f * negLight * blueWave;
				}
				if (Main.player[Main.myPlayer].nightVision)
				{
					negLight *= 1.03f;
					negLight2 *= 1.03f;
				}
				if (Main.player[Main.myPlayer].blind)
				{
					negLight *= 0.95f;
					negLight2 *= 0.95f;
				}
				hookModifyLightVision[0] = negLight;
				hookModifyLightVision[1] = negLight2;
				if (TMod.RunMethod(TMod.WorldHooks.ModifyLightVision, hookModifyLightVision))
				{
					negLight = hookModifyLightVision[0];
					negLight2 = hookModifyLightVision[1];
				}
				if (RGB)
				{
					if (Main.renderCount == 0)
					{
						stopwatch.Restart();
						for (int i = minX7; i < maxX7; i++)
						{
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = 0;
							dirY = 1;
							for (int j = minY7; j < lastToLightY27 + maxRenderCount; j++)
							{
								LightColor(i, j);
								LightColorG(i, j);
								LightColorB(i, j);
							}
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = 0;
							dirY = -1;
							for (int num = maxY7; num >= firstTileY7 - maxRenderCount; num--)
							{
								LightColor(i, num);
								LightColorG(i, num);
								LightColorB(i, num);
							}
						}
						Main.lightTimer[1] = stopwatch.ElapsedMilliseconds;
					}
					if (Main.renderCount == 1)
					{
						stopwatch.Restart();
						for (int k = firstToLightY7; k < lastToLightY7; k++)
						{
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = 1;
							dirY = 0;
							for (int l = minX7; l < lastTileX7 + maxRenderCount; l++)
							{
								LightColor(l, k);
								LightColorG(l, k);
								LightColorB(l, k);
							}
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = -1;
							dirY = 0;
							for (int num2 = maxX7; num2 >= firstTileX7 - maxRenderCount; num2--)
							{
								LightColor(num2, k);
								LightColorG(num2, k);
								LightColorB(num2, k);
							}
						}
						Main.lightTimer[2] = stopwatch.ElapsedMilliseconds;
					}
					if (Main.renderCount == 1)
					{
						stopwatch.Restart();
						for (int m = firstToLightX27; m < lastToLightX27; m++)
						{
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = 0;
							dirY = 1;
							for (int n = firstToLightY27; n < lastTileY7 + maxRenderCount; n++)
							{
								LightColor(m, n);
								LightColorG(m, n);
								LightColorB(m, n);
							}
							lightColor = 0f;
							lightColorG = 0f;
							lightColorB = 0f;
							dirX = 0;
							dirY = -1;
							for (int num3 = lastToLightY27; num3 >= firstTileY7 - maxRenderCount; num3--)
							{
								LightColor(m, num3);
								LightColorG(m, num3);
								LightColorB(m, num3);
							}
						}
						Main.lightTimer[3] = stopwatch.ElapsedMilliseconds;
					}
					if (Main.renderCount != 2)
					{
						return;
					}
					stopwatch.Restart();
					for (int num4 = firstToLightY27; num4 < lastToLightY27; num4++)
					{
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 1;
						dirY = 0;
						for (int num5 = firstToLightX27; num5 < lastTileX7 + maxRenderCount; num5++)
						{
							LightColor(num5, num4);
							LightColorG(num5, num4);
							LightColorB(num5, num4);
						}
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = -1;
						dirY = 0;
						for (int num6 = lastToLightX27; num6 >= firstTileX7 - maxRenderCount; num6--)
						{
							LightColor(num6, num4);
							LightColorG(num6, num4);
							LightColorB(num6, num4);
						}
					}
					Main.lightTimer[4] = stopwatch.ElapsedMilliseconds;
					return;
				}
				if (Main.renderCount == 0)
				{
					stopwatch.Restart();
					for (int num7 = minX7; num7 < maxX7; num7++)
					{
						lightColor = 0f;
						dirX = 0;
						dirY = 1;
						for (int num8 = minY7; num8 < lastToLightY27 + maxRenderCount; num8++)
						{
							LightColor(num7, num8);
						}
						lightColor = 0f;
						dirX = 0;
						dirY = -1;
						for (int num9 = maxY7; num9 >= firstTileY7 - maxRenderCount; num9--)
						{
							LightColor(num7, num9);
						}
					}
					Main.lightTimer[1] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount == 1)
				{
					stopwatch.Restart();
					for (int num10 = firstToLightY7; num10 < lastToLightY7; num10++)
					{
						lightColor = 0f;
						dirX = 1;
						dirY = 0;
						for (int num11 = minX7; num11 < lastTileX7 + maxRenderCount; num11++)
						{
							LightColor(num11, num10);
						}
						lightColor = 0f;
						dirX = -1;
						dirY = 0;
						for (int num12 = maxX7; num12 >= firstTileX7 - maxRenderCount; num12--)
						{
							LightColor(num12, num10);
						}
					}
					Main.lightTimer[2] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount == 1)
				{
					stopwatch.Restart();
					for (int num13 = firstToLightX27; num13 < lastToLightX27; num13++)
					{
						lightColor = 0f;
						dirX = 0;
						dirY = 1;
						for (int num14 = firstToLightY27; num14 < lastTileY7 + maxRenderCount; num14++)
						{
							LightColor(num13, num14);
						}
						lightColor = 0f;
						dirX = 0;
						dirY = -1;
						for (int num15 = lastToLightY27; num15 >= firstTileY7 - maxRenderCount; num15--)
						{
							LightColor(num13, num15);
						}
					}
					Main.lightTimer[3] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount != 2)
				{
					return;
				}
				stopwatch.Restart();
				for (int num16 = firstToLightY27; num16 < lastToLightY27; num16++)
				{
					lightColor = 0f;
					dirX = 1;
					dirY = 0;
					for (int num17 = firstToLightX27; num17 < lastTileX7 + maxRenderCount; num17++)
					{
						LightColor(num17, num16);
					}
					lightColor = 0f;
					dirX = -1;
					dirY = 0;
					for (int num18 = lastToLightX27; num18 >= firstTileX7 - maxRenderCount; num18--)
					{
						LightColor(num18, num16);
					}
				}
				Main.lightTimer[4] = stopwatch.ElapsedMilliseconds;
				return;
			}
			negLight = 0.04f;
			negLight2 = 0.16f;
			if (Main.player[Main.myPlayer].nightVision)
			{
				negLight -= 0.013f;
				negLight2 -= 0.04f;
			}
			if (Main.player[Main.myPlayer].blind)
			{
				negLight += 0.03f;
				negLight2 += 0.06f;
			}
			hookModifyLightVision[0] = negLight;
			hookModifyLightVision[1] = negLight2;
			if (TMod.RunMethod(TMod.WorldHooks.ModifyLightVision, hookModifyLightVision))
			{
				negLight = hookModifyLightVision[0];
				negLight2 = hookModifyLightVision[1];
			}
			wetLightR = negLight * 1.2f;
			wetLightG = negLight * 1.1f;
			if (RGB)
			{
				if (Main.renderCount == 0)
				{
					stopwatch.Restart();
					for (int num19 = minX7; num19 < maxX7; num19++)
					{
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 0;
						dirY = 1;
						for (int num20 = minY7; num20 < lastToLightY27 + maxRenderCount; num20++)
						{
							LightColor2(num19, num20);
							LightColorG2(num19, num20);
							LightColorB2(num19, num20);
						}
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 0;
						dirY = -1;
						for (int num21 = maxY7; num21 >= firstTileY7 - maxRenderCount; num21--)
						{
							LightColor2(num19, num21);
							LightColorG2(num19, num21);
							LightColorB2(num19, num21);
						}
					}
					Main.lightTimer[1] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount == 1)
				{
					stopwatch.Restart();
					for (int num22 = firstToLightY7; num22 < lastToLightY7; num22++)
					{
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 1;
						dirY = 0;
						for (int num23 = minX7; num23 < lastTileX7 + maxRenderCount; num23++)
						{
							LightColor2(num23, num22);
							LightColorG2(num23, num22);
							LightColorB2(num23, num22);
						}
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = -1;
						dirY = 0;
						for (int num24 = maxX7; num24 >= firstTileX7 - maxRenderCount; num24--)
						{
							LightColor2(num24, num22);
							LightColorG2(num24, num22);
							LightColorB2(num24, num22);
						}
					}
					Main.lightTimer[2] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount == 1)
				{
					stopwatch.Restart();
					for (int num25 = firstToLightX27; num25 < lastToLightX27; num25++)
					{
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 0;
						dirY = 1;
						for (int num26 = firstToLightY27; num26 < lastTileY7 + maxRenderCount; num26++)
						{
							LightColor2(num25, num26);
							LightColorG2(num25, num26);
							LightColorB2(num25, num26);
						}
						lightColor = 0f;
						lightColorG = 0f;
						lightColorB = 0f;
						dirX = 0;
						dirY = -1;
						for (int num27 = lastToLightY27; num27 >= firstTileY7 - maxRenderCount; num27--)
						{
							LightColor2(num25, num27);
							LightColorG2(num25, num27);
							LightColorB2(num25, num27);
						}
					}
					Main.lightTimer[3] = stopwatch.ElapsedMilliseconds;
				}
				if (Main.renderCount != 2)
				{
					return;
				}
				stopwatch.Restart();
				for (int num28 = firstToLightY27; num28 < lastToLightY27; num28++)
				{
					lightColor = 0f;
					lightColorG = 0f;
					lightColorB = 0f;
					dirX = 1;
					dirY = 0;
					for (int num29 = firstToLightX27; num29 < lastTileX7 + maxRenderCount; num29++)
					{
						LightColor2(num29, num28);
						LightColorG2(num29, num28);
						LightColorB2(num29, num28);
					}
					lightColor = 0f;
					lightColorG = 0f;
					lightColorB = 0f;
					dirX = -1;
					dirY = 0;
					for (int num30 = lastToLightX27; num30 >= firstTileX7 - maxRenderCount; num30--)
					{
						LightColor2(num30, num28);
						LightColorG2(num30, num28);
						LightColorB2(num30, num28);
					}
				}
				Main.lightTimer[4] = stopwatch.ElapsedMilliseconds;
				return;
			}
			if (Main.renderCount == 0)
			{
				stopwatch.Restart();
				for (int num31 = minX7; num31 < maxX7; num31++)
				{
					lightColor = 0f;
					dirX = 0;
					dirY = 1;
					for (int num32 = minY7; num32 < lastToLightY27 + maxRenderCount; num32++)
					{
						LightColor2(num31, num32);
					}
					lightColor = 0f;
					dirX = 0;
					dirY = -1;
					for (int num33 = maxY7; num33 >= firstTileY7 - maxRenderCount; num33--)
					{
						LightColor2(num31, num33);
					}
				}
				Main.lightTimer[1] = stopwatch.ElapsedMilliseconds;
			}
			if (Main.renderCount == 1)
			{
				stopwatch.Restart();
				for (int num34 = firstToLightY7; num34 < lastToLightY7; num34++)
				{
					lightColor = 0f;
					dirX = 1;
					dirY = 0;
					for (int num35 = minX7; num35 < lastTileX7 + maxRenderCount; num35++)
					{
						LightColor2(num35, num34);
					}
					lightColor = 0f;
					dirX = -1;
					dirY = 0;
					for (int num36 = maxX7; num36 >= firstTileX7 - maxRenderCount; num36--)
					{
						LightColor2(num36, num34);
					}
				}
				Main.lightTimer[2] = stopwatch.ElapsedMilliseconds;
			}
			if (Main.renderCount == 1)
			{
				stopwatch.Restart();
				for (int num37 = firstToLightX27; num37 < lastToLightX27; num37++)
				{
					lightColor = 0f;
					dirX = 0;
					dirY = 1;
					for (int num38 = firstToLightY27; num38 < lastTileY7 + maxRenderCount; num38++)
					{
						LightColor2(num37, num38);
					}
					lightColor = 0f;
					dirX = 0;
					dirY = -1;
					for (int num39 = lastToLightY27; num39 >= firstTileY7 - maxRenderCount; num39--)
					{
						LightColor2(num37, num39);
					}
				}
				Main.lightTimer[3] = stopwatch.ElapsedMilliseconds;
			}
			if (Main.renderCount != 2)
			{
				return;
			}
			stopwatch.Restart();
			for (int num40 = firstToLightY27; num40 < lastToLightY27; num40++)
			{
				lightColor = 0f;
				dirX = 1;
				dirY = 0;
				for (int num41 = firstToLightX27; num41 < lastTileX7 + maxRenderCount; num41++)
				{
					LightColor2(num41, num40);
				}
				lightColor = 0f;
				dirX = -1;
				dirY = 0;
				for (int num42 = lastToLightX27; num42 >= firstTileX7 - maxRenderCount; num42--)
				{
					LightColor2(num42, num40);
				}
			}
			Main.lightTimer[4] = stopwatch.ElapsedMilliseconds;
		}

		public static void addLight(int i, int j, float Lightness)
		{
			if (Main.netMode == 2 || i - firstTileX + offScreenTiles < 0 || i - firstTileX + offScreenTiles >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || j - firstTileY + offScreenTiles < 0 || j - firstTileY + offScreenTiles >= Main.screenHeight / 16 + offScreenTiles * 2 + 10 || tempLightCount == maxTempLights)
			{
				return;
			}
			if (!RGB)
			{
				for (int k = 0; k < tempLightCount; k++)
				{
					if (tempLightX[k] == i && tempLightY[k] == j && Lightness <= tempLight[k])
					{
						return;
					}
				}
				tempLightX[tempLightCount] = i;
				tempLightY[tempLightCount] = j;
				tempLight[tempLightCount] = Lightness;
				tempLightG[tempLightCount] = Lightness;
				tempLightB[tempLightCount] = Lightness;
				tempLightCount++;
			}
			else
			{
				tempLight[tempLightCount] = Lightness;
				tempLightG[tempLightCount] = Lightness;
				tempLightB[tempLightCount] = Lightness;
				tempLightX[tempLightCount] = i;
				tempLightY[tempLightCount] = j;
				tempLightCount++;
			}
		}

		public static void addLight(int i, int j, float R, float G, float B)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			if (!RGB)
			{
				addLight(i, j, (R + G + B) / 3f);
			}
			if (i - firstTileX + offScreenTiles < 0 || i - firstTileX + offScreenTiles >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || j - firstTileY + offScreenTiles < 0 || j - firstTileY + offScreenTiles >= Main.screenHeight / 16 + offScreenTiles * 2 + 10 || tempLightCount == maxTempLights)
			{
				return;
			}
			for (int k = 0; k < tempLightCount; k++)
			{
				if (tempLightX[k] == i && tempLightY[k] == j)
				{
					if (tempLight[k] < R)
					{
						tempLight[k] = R;
					}
					if (tempLightG[k] < G)
					{
						tempLightG[k] = G;
					}
					if (tempLightB[k] < B)
					{
						tempLightB[k] = B;
					}
					return;
				}
			}
			tempLight[tempLightCount] = R;
			tempLightG[tempLightCount] = G;
			tempLightB[tempLightCount] = B;
			tempLightX[tempLightCount] = i;
			tempLightY[tempLightCount] = j;
			tempLightCount++;
		}

		public static void ResetRange()
		{
			minX = Main.screenWidth + offScreenTiles * 2 + 10;
			maxX = 0;
			minY = Main.screenHeight + offScreenTiles * 2 + 10;
			maxY = 0;
		}

		public static void LightColor(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			if (color2[num, num2] > lightColor)
			{
				lightColor = color2[num, num2];
			}
			else
			{
				if ((double)lightColor <= 0.0185)
				{
					return;
				}
				if (color2[num, num2] < lightColor)
				{
					color2[num, num2] = lightColor;
				}
			}
			if (!(color2[num + dirX, num2 + dirY] > lightColor))
			{
				if (stopLight[num, num2])
				{
					lightColor *= negLight2;
				}
				else if (wetLight[num, num2])
				{
					lightColor *= wetLightR * (float)Main.rand.Next(98, 100) * 0.01f;
				}
				else
				{
					lightColor *= negLight;
				}
			}
		}

		public static void LightColorG(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			if (colorG2[num, num2] > lightColorG)
			{
				lightColorG = colorG2[num, num2];
			}
			else
			{
				if ((double)lightColorG <= 0.0185)
				{
					return;
				}
				colorG2[num, num2] = lightColorG;
			}
			if (!(colorG2[num + dirX, num2 + dirY] > lightColorG))
			{
				if (stopLight[num, num2])
				{
					lightColorG *= negLight2;
				}
				else if (wetLight[num, num2])
				{
					lightColorG *= wetLightG * (float)Main.rand.Next(97, 100) * 0.01f;
				}
				else
				{
					lightColorG *= negLight;
				}
			}
		}

		public static void LightColorB(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			if (colorB2[num, num2] > lightColorB)
			{
				lightColorB = colorB2[num, num2];
			}
			else
			{
				if ((double)lightColorB <= 0.0185)
				{
					return;
				}
				colorB2[num, num2] = lightColorB;
			}
			if (!(colorB2[num + dirX, num2 + dirY] >= lightColorB))
			{
				if (stopLight[num, num2])
				{
					lightColorB *= negLight2;
				}
				else
				{
					lightColorB *= negLight;
				}
			}
		}

		public static void LightColor2(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			try
			{
				if (color2[num, num2] > lightColor)
				{
					lightColor = color2[num, num2];
					goto IL_0058;
				}
				if (!(lightColor <= 0f))
				{
					color2[num, num2] = lightColor;
					goto IL_0058;
				}
				goto end_IL_0010;
				IL_0058:
				if (Main.tile[i, j].active && Main.tileBlockLight[Main.tile[i, j].type])
				{
					lightColor -= negLight2;
				}
				else if (wetLight[num, num2])
				{
					lightColor -= wetLightR;
				}
				else
				{
					lightColor -= negLight;
				}
				end_IL_0010:;
			}
			catch
			{
			}
		}

		public static void LightColorG2(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			try
			{
				if (colorG2[num, num2] > lightColorG)
				{
					lightColorG = colorG2[num, num2];
					goto IL_0058;
				}
				if (!(lightColorG <= 0f))
				{
					colorG2[num, num2] = lightColorG;
					goto IL_0058;
				}
				goto end_IL_0010;
				IL_0058:
				if (Main.tile[i, j].active && Main.tileBlockLight[Main.tile[i, j].type])
				{
					lightColorG -= negLight2;
				}
				else if (wetLight[num, num2])
				{
					lightColorG -= wetLightG;
				}
				else
				{
					lightColorG -= negLight;
				}
				end_IL_0010:;
			}
			catch
			{
			}
		}

		public static void LightColorB2(int i, int j)
		{
			int num = i - firstToLightX7;
			int num2 = j - firstToLightY7;
			try
			{
				if (colorB2[num, num2] > lightColorB)
				{
					lightColorB = colorB2[num, num2];
					goto IL_0055;
				}
				if (!(lightColorB <= 0f))
				{
					colorB2[num, num2] = lightColorB;
					goto IL_0055;
				}
				goto end_IL_0010;
				IL_0055:
				if (Main.tile[i, j].active && Main.tileBlockLight[Main.tile[i, j].type])
				{
					lightColorB -= negLight2;
				}
				else
				{
					lightColorB -= negLight;
				}
				end_IL_0010:;
			}
			catch
			{
			}
		}

		public static int LightingX(int lightX)
		{
			if (lightX < 0)
			{
				return 0;
			}
			if (lightX >= Main.screenWidth / 16 + offScreenTiles * 2 + 10)
			{
				return Main.screenWidth / 16 + offScreenTiles * 2 + 10 - 1;
			}
			return lightX;
		}

		public static int LightingY(int lightY)
		{
			if (lightY < 0)
			{
				return 0;
			}
			if (lightY >= Main.screenHeight / 16 + offScreenTiles * 2 + 10)
			{
				return Main.screenHeight / 16 + offScreenTiles * 2 + 10 - 1;
			}
			return lightY;
		}

		public static Color GetColor(int x, int y, Color oldColor)
		{
			int num = x - firstTileX + offScreenTiles;
			int num2 = y - firstTileY + offScreenTiles;
			if (Main.gameMenu)
			{
				return oldColor;
			}
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + offScreenTiles * 2 + 10)
			{
				return Color.Black;
			}
			Color white = Color.White;
			int num3 = (int)((float)(int)oldColor.R * color[num, num2] * brightness);
			int num4 = (int)((float)(int)oldColor.G * colorG[num, num2] * brightness);
			int num5 = (int)((float)(int)oldColor.B * colorB[num, num2] * brightness);
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			white.R = (byte)num3;
			white.G = (byte)num4;
			white.B = (byte)num5;
			return white;
		}

		public static Color GetColor(int x, int y)
		{
			int num = x - firstTileX + offScreenTiles;
			int num2 = y - firstTileY + offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + offScreenTiles * 2)
			{
				return Color.Black;
			}
			int num3 = (int)(255f * color[num, num2] * brightness);
			int num4 = (int)(255f * colorG[num, num2] * brightness);
			int num5 = (int)(255f * colorB[num, num2] * brightness);
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			if (num5 > 255)
			{
				num5 = 255;
			}
			return new Color((byte)num3, (byte)num4, (byte)num5, 255);
		}

		public static Color GetBlackness(int x, int y)
		{
			int num = x - firstTileX + offScreenTiles;
			int num2 = y - firstTileY + offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + offScreenTiles * 2 + 10)
			{
				return Color.Black;
			}
			return new Color(0, 0, 0, (byte)(255f - 255f * color[num, num2]));
		}

		public static float Brightness(int x, int y)
		{
			int num = x - firstTileX + offScreenTiles;
			int num2 = y - firstTileY + offScreenTiles;
			if (num < 0 || num2 < 0 || num >= Main.screenWidth / 16 + offScreenTiles * 2 + 10 || num2 >= Main.screenHeight / 16 + offScreenTiles * 2 + 10)
			{
				return 0f;
			}
			return (color[num, num2] + colorG[num, num2] + colorB[num, num2]) / 3f;
		}

		public static bool Brighter(int x, int y, int x2, int y2)
		{
			int num = x - firstTileX + offScreenTiles;
			int num2 = y - firstTileY + offScreenTiles;
			int num3 = x2 - firstTileX + offScreenTiles;
			int num4 = y2 - firstTileY + offScreenTiles;
			try
			{
				if (color[num, num2] + colorG[num, num2] + colorB[num, num2] >= color[num3, num4] + colorG[num3, num4] + colorB[num3, num4])
				{
					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
	}
}
