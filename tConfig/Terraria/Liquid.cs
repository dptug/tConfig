using System;

namespace Terraria
{
	public class Liquid
	{
		public static int skipCount = 0;

		public static int stuckCount = 0;

		public static int stuckAmount = 0;

		public static int cycles = 10;

		public static int resLiquid = 5000;

		public static int maxLiquid = 5000;

		public static int numLiquid;

		public static bool stuck = false;

		public static bool quickFall = false;

		public static bool quickSettle = false;

		private static int wetCounter;

		public static int panicCounter = 0;

		public static bool panicMode = false;

		public static int panicY = 0;

		public int x;

		public int y;

		public int kill;

		public int delay;

		public static double QuickWater(int verbose = 0, int minY = -1, int maxY = -1)
		{
			int num = 0;
			if (minY == -1)
			{
				minY = 3;
			}
			if (maxY == -1)
			{
				maxY = Main.maxTilesY - 3;
			}
			for (int num2 = maxY; num2 >= minY; num2--)
			{
				if (verbose > 0)
				{
					float num3 = (float)(maxY - num2) / (float)(maxY - minY + 1) / (float)verbose * 100f + 1f;
					Main.statusText = "Settling liquids: " + (int)num3 + "%";
				}
				else if (verbose < 0)
				{
					float num4 = (float)(maxY - num2) / (float)(maxY - minY + 1) / (float)(-verbose) * 100f + 1f;
					Main.statusText = "Creating underworld: " + (int)num4 + "%";
				}
				for (int i = 0; i < 2; i++)
				{
					int num5 = 2;
					int num6 = Main.maxTilesX - 2;
					int num7 = 1;
					if (i == 1)
					{
						num5 = Main.maxTilesX - 2;
						num6 = 2;
						num7 = -1;
					}
					for (int j = num5; j != num6; j += num7)
					{
						if (Main.tile[j, num2].liquid <= 0)
						{
							continue;
						}
						int num8 = -num7;
						bool flag = false;
						int num9 = j;
						int num10 = num2;
						bool flag2 = Main.tile[j, num2].lava;
						byte b = Main.tile[j, num2].liquid;
						Main.tile[j, num2].liquid = 0;
						bool flag3 = true;
						int num11 = 0;
						while (flag3 && num9 > 3 && num9 < Main.maxTilesX - 3 && num10 < Main.maxTilesY - 3)
						{
							flag3 = false;
							while (Main.tile[num9, num10 + 1].liquid == 0 && num10 < Main.maxTilesY - 5 && (!Main.tile[num9, num10 + 1].active || !Main.tileSolid[Main.tile[num9, num10 + 1].type] || Main.tileSolidTop[Main.tile[num9, num10 + 1].type]))
							{
								flag = true;
								num8 = num7;
								num11 = 0;
								flag3 = true;
								num10++;
								if (num10 > WorldGen.waterLine)
								{
									flag2 = true;
								}
							}
							if (Main.tile[num9, num10 + 1].liquid > 0 && Main.tile[num9, num10 + 1].liquid < byte.MaxValue && Main.tile[num9, num10 + 1].lava == flag2)
							{
								int num12 = 255 - Main.tile[num9, num10 + 1].liquid;
								if (num12 > b)
								{
									num12 = b;
								}
								Main.tile[num9, num10 + 1].liquid += (byte)num12;
								b = (byte)(b - (byte)num12);
								if (b <= 0)
								{
									num++;
									break;
								}
							}
							if (num11 == 0)
							{
								if (Main.tile[num9 + num8, num10].liquid == 0 && (!Main.tile[num9 + num8, num10].active || !Main.tileSolid[Main.tile[num9 + num8, num10].type] || Main.tileSolidTop[Main.tile[num9 + num8, num10].type]))
								{
									num11 = num8;
								}
								else if (Main.tile[num9 - num8, num10].liquid == 0 && (!Main.tile[num9 - num8, num10].active || !Main.tileSolid[Main.tile[num9 - num8, num10].type] || Main.tileSolidTop[Main.tile[num9 - num8, num10].type]))
								{
									num11 = -num8;
								}
							}
							if (num11 != 0 && Main.tile[num9 + num11, num10].liquid == 0 && (!Main.tile[num9 + num11, num10].active || !Main.tileSolid[Main.tile[num9 + num11, num10].type] || Main.tileSolidTop[Main.tile[num9 + num11, num10].type]))
							{
								flag3 = true;
								num9 += num11;
							}
							if (flag && !flag3)
							{
								flag = false;
								flag3 = true;
								num8 = -num7;
								num11 = 0;
							}
						}
						if (j != num9 && num2 != num10)
						{
							num++;
						}
						Main.tile[num9, num10].liquid = b;
						Main.tile[num9, num10].lava = flag2;
						if (Main.tile[num9 - 1, num10].liquid > 0 && Main.tile[num9 - 1, num10].lava != flag2)
						{
							if (flag2)
							{
								LavaCheck(num9, num10);
							}
							else
							{
								LavaCheck(num9 - 1, num10);
							}
						}
						else if (Main.tile[num9 + 1, num10].liquid > 0 && Main.tile[num9 + 1, num10].lava != flag2)
						{
							if (flag2)
							{
								LavaCheck(num9, num10);
							}
							else
							{
								LavaCheck(num9 + 1, num10);
							}
						}
						else if (Main.tile[num9, num10 - 1].liquid > 0 && Main.tile[num9, num10 - 1].lava != flag2)
						{
							if (flag2)
							{
								LavaCheck(num9, num10);
							}
							else
							{
								LavaCheck(num9, num10 - 1);
							}
						}
						else if (Main.tile[num9, num10 + 1].liquid > 0 && Main.tile[num9, num10 + 1].lava != flag2)
						{
							if (flag2)
							{
								LavaCheck(num9, num10);
							}
							else
							{
								LavaCheck(num9, num10 + 1);
							}
						}
					}
				}
			}
			return num;
		}

		public void Update()
		{
			Tile tile = Main.tile[x, y];
			if (tile.active && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type])
			{
				kill = 9;
				return;
			}
			byte liquid = tile.liquid;
			float num = 0f;
			if ((double)y > Main.hellLayer && !tile.lava && tile.liquid > 0)
			{
				byte b = 2;
				if (tile.liquid < b)
				{
					b = tile.liquid;
				}
				tile.liquid -= b;
			}
			if (tile.liquid == 0)
			{
				kill = 9;
				return;
			}
			if (tile.lava)
			{
				LavaCheck(x, y);
				if (!quickFall)
				{
					if (delay < 5)
					{
						delay++;
						return;
					}
					delay = 0;
				}
			}
			else
			{
				if (Main.tile[x - 1, y].lava)
				{
					AddWater(x - 1, y);
				}
				if (Main.tile[x + 1, y].lava)
				{
					AddWater(x + 1, y);
				}
				if (Main.tile[x, y - 1].lava)
				{
					AddWater(x, y - 1);
				}
				if (Main.tile[x, y + 1].lava)
				{
					AddWater(x, y + 1);
				}
			}
			if ((!Main.tile[x, y + 1].active || !Main.tileSolid[Main.tile[x, y + 1].type] || Main.tileSolidTop[Main.tile[x, y + 1].type]) && (Main.tile[x, y + 1].liquid <= 0 || Main.tile[x, y + 1].lava == tile.lava) && Main.tile[x, y + 1].liquid < byte.MaxValue)
			{
				num = 255 - Main.tile[x, y + 1].liquid;
				if (num > (float)(int)tile.liquid)
				{
					num = (int)tile.liquid;
				}
				tile.liquid -= (byte)num;
				Main.tile[x, y + 1].liquid += (byte)num;
				Main.tile[x, y + 1].lava = tile.lava;
				AddWater(x, y + 1);
				Main.tile[x, y + 1].skipLiquid = true;
				tile.skipLiquid = true;
				if (tile.liquid > 250)
				{
					tile.liquid = byte.MaxValue;
				}
				else
				{
					AddWater(x - 1, y);
					AddWater(x + 1, y);
				}
			}
			if (tile.liquid > 0)
			{
				bool flag = true;
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				if (Main.tile[x - 1, y].active && Main.tileSolid[Main.tile[x - 1, y].type] && !Main.tileSolidTop[Main.tile[x - 1, y].type])
				{
					flag = false;
				}
				else if (Main.tile[x - 1, y].liquid > 0 && Main.tile[x - 1, y].lava != tile.lava)
				{
					flag = false;
				}
				else if (Main.tile[x - 2, y].active && Main.tileSolid[Main.tile[x - 2, y].type] && !Main.tileSolidTop[Main.tile[x - 2, y].type])
				{
					flag3 = false;
				}
				else if (Main.tile[x - 2, y].liquid == 0)
				{
					flag3 = false;
				}
				else if (Main.tile[x - 2, y].liquid > 0 && Main.tile[x - 2, y].lava != tile.lava)
				{
					flag3 = false;
				}
				if (Main.tile[x + 1, y].active && Main.tileSolid[Main.tile[x + 1, y].type] && !Main.tileSolidTop[Main.tile[x + 1, y].type])
				{
					flag2 = false;
				}
				else if (Main.tile[x + 1, y].liquid > 0 && Main.tile[x + 1, y].lava != tile.lava)
				{
					flag2 = false;
				}
				else if (Main.tile[x + 2, y].active && Main.tileSolid[Main.tile[x + 2, y].type] && !Main.tileSolidTop[Main.tile[x + 2, y].type])
				{
					flag4 = false;
				}
				else if (Main.tile[x + 2, y].liquid == 0)
				{
					flag4 = false;
				}
				else if (Main.tile[x + 2, y].liquid > 0 && Main.tile[x + 2, y].lava != tile.lava)
				{
					flag4 = false;
				}
				int num2 = 0;
				if (tile.liquid < 3)
				{
					num2 = -1;
				}
				if (flag && flag2)
				{
					if (flag3 && flag4)
					{
						bool flag5 = true;
						bool flag6 = true;
						if (Main.tile[x - 3, y].active && Main.tileSolid[Main.tile[x - 3, y].type] && !Main.tileSolidTop[Main.tile[x - 3, y].type])
						{
							flag5 = false;
						}
						else if (Main.tile[x - 3, y].liquid == 0)
						{
							flag5 = false;
						}
						else if (Main.tile[x - 3, y].lava != tile.lava)
						{
							flag5 = false;
						}
						if (Main.tile[x + 3, y].active && Main.tileSolid[Main.tile[x + 3, y].type] && !Main.tileSolidTop[Main.tile[x + 3, y].type])
						{
							flag6 = false;
						}
						else if (Main.tile[x + 3, y].liquid == 0)
						{
							flag6 = false;
						}
						else if (Main.tile[x + 3, y].lava != tile.lava)
						{
							flag6 = false;
						}
						if (flag5 && flag6)
						{
							num = Main.tile[x - 1, y].liquid + Main.tile[x + 1, y].liquid + Main.tile[x - 2, y].liquid + Main.tile[x + 2, y].liquid + Main.tile[x - 3, y].liquid + Main.tile[x + 3, y].liquid + tile.liquid + num2;
							num = (float)Math.Round(num / 7f);
							byte b2 = (byte)num;
							int num3 = 0;
							Main.tile[x - 1, y].lava = tile.lava;
							if (Main.tile[x - 1, y].liquid != b2)
							{
								AddWater(x - 1, y);
								Main.tile[x - 1, y].liquid = b2;
							}
							else
							{
								num3++;
							}
							Main.tile[x + 1, y].lava = tile.lava;
							if (Main.tile[x + 1, y].liquid != (byte)num)
							{
								AddWater(x + 1, y);
								Main.tile[x + 1, y].liquid = (byte)num;
							}
							else
							{
								num3++;
							}
							Main.tile[x - 2, y].lava = tile.lava;
							if (Main.tile[x - 2, y].liquid != (byte)num)
							{
								AddWater(x - 2, y);
								Main.tile[x - 2, y].liquid = (byte)num;
							}
							else
							{
								num3++;
							}
							Main.tile[x + 2, y].lava = tile.lava;
							if (Main.tile[x + 2, y].liquid != (byte)num)
							{
								AddWater(x + 2, y);
								Main.tile[x + 2, y].liquid = (byte)num;
							}
							else
							{
								num3++;
							}
							Main.tile[x - 3, y].lava = tile.lava;
							if (Main.tile[x - 3, y].liquid != (byte)num)
							{
								AddWater(x - 3, y);
								Main.tile[x - 3, y].liquid = (byte)num;
							}
							else
							{
								num3++;
							}
							Main.tile[x + 3, y].lava = tile.lava;
							if (Main.tile[x + 3, y].liquid != (byte)num)
							{
								AddWater(x + 3, y);
								Main.tile[x + 3, y].liquid = (byte)num;
							}
							else
							{
								num3++;
							}
							if (tile.liquid != b2)
							{
								AddWater(x - 3, y);
								AddWater(x - 2, y);
								AddWater(x - 1, y);
								AddWater(x + 1, y);
								AddWater(x + 2, y);
								AddWater(x + 3, y);
							}
							else
							{
								if (Main.tile[x - 3, y].liquid != b2)
								{
									AddWater(x - 3, y);
								}
								if (Main.tile[x - 2, y].liquid != b2)
								{
									AddWater(x - 2, y);
								}
								if (Main.tile[x - 1, y].liquid != b2)
								{
									AddWater(x - 1, y);
								}
								if (Main.tile[x + 1, y].liquid != b2)
								{
									AddWater(x + 1, y);
								}
								if (Main.tile[x + 2, y].liquid != b2)
								{
									AddWater(x + 2, y);
								}
								if (Main.tile[x + 3, y].liquid != b2)
								{
									AddWater(x + 3, y);
								}
							}
							if (num3 != 6 || Main.tile[x, y - 1].liquid <= 0)
							{
								tile.liquid = b2;
							}
						}
						else
						{
							int num4 = 0;
							num = Main.tile[x - 1, y].liquid + Main.tile[x + 1, y].liquid + Main.tile[x - 2, y].liquid + Main.tile[x + 2, y].liquid + tile.liquid + num2;
							num = (float)Math.Round(num * 0.2f);
							byte b3 = (byte)num;
							Main.tile[x - 1, y].lava = tile.lava;
							if (Main.tile[x - 1, y].liquid != b3)
							{
								AddWater(x - 1, y);
								Main.tile[x - 1, y].liquid = b3;
							}
							else
							{
								num4++;
							}
							Main.tile[x + 1, y].lava = tile.lava;
							if (Main.tile[x + 1, y].liquid != b3)
							{
								AddWater(x + 1, y);
								Main.tile[x + 1, y].liquid = b3;
							}
							else
							{
								num4++;
							}
							Main.tile[x - 2, y].lava = tile.lava;
							if (Main.tile[x - 2, y].liquid != b3)
							{
								AddWater(x - 2, y);
								Main.tile[x - 2, y].liquid = b3;
							}
							else
							{
								num4++;
							}
							Main.tile[x + 2, y].lava = tile.lava;
							if (Main.tile[x + 2, y].liquid != b3)
							{
								AddWater(x + 2, y);
								Main.tile[x + 2, y].liquid = b3;
							}
							else
							{
								num4++;
							}
							if (tile.liquid != b3)
							{
								AddWater(x - 2, y);
								AddWater(x - 1, y);
								AddWater(x + 1, y);
								AddWater(x + 2, y);
							}
							else
							{
								if (Main.tile[x - 1, y].liquid != b3)
								{
									AddWater(x - 1, y);
								}
								if (Main.tile[x + 1, y].liquid != b3)
								{
									AddWater(x + 1, y);
								}
								if (Main.tile[x - 2, y].liquid != b3)
								{
									AddWater(x - 2, y);
								}
								if (Main.tile[x + 2, y].liquid != b3)
								{
									AddWater(x + 2, y);
								}
							}
							if (num4 != 4 || Main.tile[x, y - 1].liquid <= 0)
							{
								tile.liquid = b3;
							}
						}
					}
					else if (flag3)
					{
						num = Main.tile[x - 1, y].liquid + Main.tile[x + 1, y].liquid + Main.tile[x - 2, y].liquid + tile.liquid + num2;
						num = (float)Math.Round(num * 0.25f + 0.001f);
						byte b4 = (byte)num;
						Main.tile[x - 2, y].lava = tile.lava;
						Main.tile[x - 1, y].lava = tile.lava;
						Main.tile[x + 1, y].lava = tile.lava;
						if (tile.liquid != b4)
						{
							Main.tile[x - 2, y].liquid = b4;
							AddWater(x - 2, y);
							AddWater(x - 1, y);
							Main.tile[x - 1, y].liquid = b4;
							AddWater(x + 1, y);
							Main.tile[x + 1, y].liquid = b4;
						}
						else
						{
							if (Main.tile[x - 2, y].liquid != b4)
							{
								Main.tile[x - 2, y].liquid = b4;
								AddWater(x - 2, y);
							}
							if (Main.tile[x - 1, y].liquid != b4)
							{
								AddWater(x - 1, y);
								Main.tile[x - 1, y].liquid = b4;
							}
							if (Main.tile[x + 1, y].liquid != b4)
							{
								AddWater(x + 1, y);
								Main.tile[x + 1, y].liquid = b4;
							}
						}
						tile.liquid = b4;
					}
					else if (flag4)
					{
						num = Main.tile[x - 1, y].liquid + Main.tile[x + 1, y].liquid + Main.tile[x + 2, y].liquid + tile.liquid + num2;
						num = (float)Math.Round(num * 0.25f + 0.001f);
						byte b5 = (byte)num;
						Main.tile[x - 1, y].lava = tile.lava;
						Main.tile[x + 1, y].lava = tile.lava;
						Main.tile[x + 2, y].lava = tile.lava;
						if (tile.liquid != b5)
						{
							AddWater(x - 1, y);
							Main.tile[x - 1, y].liquid = b5;
							AddWater(x + 1, y);
							Main.tile[x + 1, y].liquid = b5;
							Main.tile[x + 2, y].liquid = b5;
							AddWater(x + 2, y);
						}
						else
						{
							if (Main.tile[x - 1, y].liquid != b5)
							{
								AddWater(x - 1, y);
								Main.tile[x - 1, y].liquid = b5;
							}
							if (Main.tile[x + 1, y].liquid != b5)
							{
								AddWater(x + 1, y);
								Main.tile[x + 1, y].liquid = b5;
							}
							if (Main.tile[x + 2, y].liquid != b5)
							{
								Main.tile[x + 2, y].liquid = b5;
								AddWater(x + 2, y);
							}
						}
						tile.liquid = b5;
					}
					else
					{
						num = Main.tile[x - 1, y].liquid + Main.tile[x + 1, y].liquid + tile.liquid + num2;
						num = (float)Math.Round(num / 3f + 0.001f);
						byte b6 = (byte)num;
						Main.tile[x - 1, y].lava = tile.lava;
						Main.tile[x + 1, y].lava = tile.lava;
						if (Main.tile[x - 1, y].liquid != b6)
						{
							Main.tile[x - 1, y].liquid = b6;
						}
						if (tile.liquid != b6)
						{
							AddWater(x - 1, y);
							Main.tile[x + 1, y].liquid = b6;
							AddWater(x + 1, y);
						}
						else
						{
							if (Main.tile[x - 1, y].liquid != b6)
							{
								AddWater(x - 1, y);
							}
							Main.tile[x + 1, y].liquid = b6;
							if (Main.tile[x + 1, y].liquid != b6)
							{
								AddWater(x + 1, y);
							}
						}
						tile.liquid = b6;
					}
				}
				else if (flag)
				{
					num = Main.tile[x - 1, y].liquid + tile.liquid + num2;
					num = (float)Math.Round(num * 0.5f + 0.001f);
					byte b7 = (byte)num;
					Main.tile[x - 1, y].liquid = b7;
					Main.tile[x - 1, y].lava = tile.lava;
					if (tile.liquid != b7 || Main.tile[x - 1, y].liquid != b7)
					{
						AddWater(x - 1, y);
					}
					tile.liquid = b7;
				}
				else if (flag2)
				{
					num = Main.tile[x + 1, y].liquid + tile.liquid + num2;
					num = (float)Math.Round(num * 0.5f + 0.001f);
					byte b8 = (byte)num;
					Main.tile[x + 1, y].liquid = b8;
					Main.tile[x + 1, y].lava = tile.lava;
					if (tile.liquid != b8 || Main.tile[x + 1, y].liquid != b8)
					{
						AddWater(x + 1, y);
					}
					tile.liquid = b8;
				}
			}
			if (tile.liquid == liquid)
			{
				kill++;
			}
			else if (tile.liquid == 254 && liquid == byte.MaxValue)
			{
				tile.liquid = byte.MaxValue;
				kill++;
			}
			else
			{
				AddWater(x, y - 1);
				kill = 0;
			}
		}

		public static void StartPanic()
		{
			if (!panicMode)
			{
				WorldGen.waterLine = Main.maxTilesY;
				numLiquid = 0;
				LiquidBuffer.numLiquidBuffer = 0;
				panicCounter = 0;
				panicMode = true;
				panicY = Main.maxTilesY - 3;
				if (Main.dedServ)
				{
					Console.WriteLine("Forcing water to settle.");
				}
			}
		}

		public static void UpdateLiquid()
		{
			if (Main.netMode == 2)
			{
				cycles = 30;
				maxLiquid = 6000;
			}
			if (!WorldGen.gen)
			{
				if (!panicMode)
				{
					if (numLiquid + LiquidBuffer.numLiquidBuffer > 4000)
					{
						panicCounter++;
						if (panicCounter > 1800 || numLiquid + LiquidBuffer.numLiquidBuffer > 13500)
						{
							StartPanic();
						}
					}
					else
					{
						panicCounter = 0;
					}
				}
				if (panicMode)
				{
					int num = 0;
					while (panicY >= 3 && num < 5)
					{
						num++;
						QuickWater(0, panicY, panicY);
						panicY--;
						if (panicY >= 3)
						{
							continue;
						}
						Console.WriteLine("Water has been settled.");
						panicCounter = 0;
						panicMode = false;
						WorldGen.WaterCheck();
						if (Main.netMode != 2)
						{
							continue;
						}
						for (int i = 0; i < 255; i++)
						{
							for (int j = 0; j < Main.maxSectionsX; j++)
							{
								for (int k = 0; k < Main.maxSectionsY; k++)
								{
									Netplay.serverSock[i].tileSection[j, k] = false;
								}
							}
						}
					}
					return;
				}
			}
			if (quickSettle || numLiquid > 2000)
			{
				quickFall = true;
			}
			else
			{
				quickFall = false;
			}
			wetCounter++;
			int num2 = maxLiquid / cycles;
			int num3 = num2 * (wetCounter - 1);
			int num4 = num2 * wetCounter;
			if (wetCounter == cycles)
			{
				num4 = numLiquid;
			}
			if (num4 > numLiquid)
			{
				num4 = numLiquid;
				wetCounter = cycles;
			}
			if (quickFall)
			{
				for (int l = num3; l < num4; l++)
				{
					Main.liquid[l].delay = 10;
					Main.liquid[l].Update();
					Main.tile[Main.liquid[l].x, Main.liquid[l].y].skipLiquid = false;
				}
			}
			else
			{
				for (int m = num3; m < num4; m++)
				{
					if (!Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid)
					{
						Main.liquid[m].Update();
					}
					else
					{
						Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid = false;
					}
				}
			}
			if (wetCounter < cycles)
			{
				return;
			}
			wetCounter = 0;
			for (int num5 = numLiquid - 1; num5 >= 0; num5--)
			{
				if (Main.liquid[num5].kill > 3)
				{
					DelWater(num5);
				}
			}
			int num6 = maxLiquid - (maxLiquid - numLiquid);
			if (num6 > LiquidBuffer.numLiquidBuffer)
			{
				num6 = LiquidBuffer.numLiquidBuffer;
			}
			for (int n = 0; n < num6; n++)
			{
				Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid = false;
				AddWater(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
				LiquidBuffer.DelBuffer(0);
			}
			if (numLiquid > 0 && numLiquid > stuckAmount - 50 && numLiquid < stuckAmount + 50)
			{
				stuckCount++;
				if (stuckCount >= 10000)
				{
					stuck = true;
					for (int num7 = numLiquid - 1; num7 >= 0; num7--)
					{
						DelWater(num7);
					}
					stuck = false;
					stuckCount = 0;
				}
			}
			else
			{
				stuckCount = 0;
				stuckAmount = numLiquid;
			}
		}

		public static void AddWater(int x, int y)
		{
			if (Main.tile[x, y].checkingLiquid || x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5 || x < 5 || y < 5 || Main.tile[x, y] == null || Main.tile[x, y].liquid == 0)
			{
				return;
			}
			if (numLiquid >= maxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}
			Main.tile[x, y].checkingLiquid = true;
			Main.liquid[numLiquid].kill = 0;
			Main.liquid[numLiquid].x = x;
			Main.liquid[numLiquid].y = y;
			Main.liquid[numLiquid].delay = 0;
			Main.tile[x, y].skipLiquid = false;
			numLiquid++;
			if (Main.netMode == 2 && numLiquid < maxLiquid / 3)
			{
				NetMessage.sendWater(x, y);
			}
			if (!Main.tile[x, y].active || (!Main.tileWaterDeath[Main.tile[x, y].type] && (!Main.tile[x, y].lava || !Main.tileLavaDeath[Main.tile[x, y].type])) || (Main.tile[x, y].type == 4 && Main.tile[x, y].frameY == 176))
			{
				return;
			}
			if (WorldGen.gen)
			{
				Main.tile[x, y].active = false;
				return;
			}
			WorldGen.KillTile(x, y);
			if (Main.netMode == 2)
			{
				NetMessage.SendData(17, -1, -1, "", 0, x, y);
			}
		}

		public static void LavaCheck(int x, int y)
		{
			if ((Main.tile[x - 1, y].liquid > 0 && !Main.tile[x - 1, y].lava) || (Main.tile[x + 1, y].liquid > 0 && !Main.tile[x + 1, y].lava) || (Main.tile[x, y - 1].liquid > 0 && !Main.tile[x, y - 1].lava))
			{
				int num = 0;
				if (!Main.tile[x - 1, y].lava)
				{
					num += Main.tile[x - 1, y].liquid;
					Main.tile[x - 1, y].liquid = 0;
				}
				if (!Main.tile[x + 1, y].lava)
				{
					num += Main.tile[x + 1, y].liquid;
					Main.tile[x + 1, y].liquid = 0;
				}
				if (!Main.tile[x, y - 1].lava)
				{
					num += Main.tile[x, y - 1].liquid;
					Main.tile[x, y - 1].liquid = 0;
				}
				if (num >= 32 && !Main.tile[x, y].active)
				{
					Main.tile[x, y].liquid = 0;
					Main.tile[x, y].lava = false;
					WorldGen.PlaceTile(x, y, 56, mute: true, forced: true);
					WorldGen.SquareTileFrame(x, y);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, x - 1, y - 1, 3);
					}
				}
			}
			else if (Main.tile[x, y + 1].liquid > 0 && !Main.tile[x, y + 1].lava && !Main.tile[x, y + 1].active)
			{
				Main.tile[x, y].liquid = 0;
				Main.tile[x, y].lava = false;
				Main.tile[x, y + 1].liquid = 0;
				WorldGen.PlaceTile(x, y + 1, 56, mute: true, forced: true);
				WorldGen.SquareTileFrame(x, y + 1);
				if (Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, x - 1, y, 3);
				}
			}
		}

		public static void NetAddWater(int x, int y)
		{
			if (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5 || x < 5 || y < 5 || Main.tile[x, y] == null || Main.tile[x, y].liquid == 0)
			{
				return;
			}
			for (int i = 0; i < numLiquid; i++)
			{
				if (Main.liquid[i].x == x && Main.liquid[i].y == y)
				{
					Main.liquid[i].kill = 0;
					Main.tile[x, y].skipLiquid = true;
					return;
				}
			}
			if (numLiquid >= maxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}
			Main.tile[x, y].checkingLiquid = true;
			Main.tile[x, y].skipLiquid = true;
			Main.liquid[numLiquid].kill = 0;
			Main.liquid[numLiquid].x = x;
			Main.liquid[numLiquid].y = y;
			numLiquid++;
			_ = Main.netMode;
			if (Main.tile[x, y].active && (Main.tileWaterDeath[Main.tile[x, y].type] || (Main.tile[x, y].lava && Main.tileLavaDeath[Main.tile[x, y].type])))
			{
				WorldGen.KillTile(x, y);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(17, -1, -1, "", 0, x, y);
				}
			}
		}

		public static void DelWater(int l)
		{
			int num = Main.liquid[l].x;
			int num2 = Main.liquid[l].y;
			if (Main.tile[num, num2].liquid < 2)
			{
				Main.tile[num, num2].liquid = 0;
				if (Main.tile[num - 1, num2].liquid < 2)
				{
					Main.tile[num - 1, num2].liquid = 0;
				}
				if (Main.tile[num + 1, num2].liquid < 2)
				{
					Main.tile[num + 1, num2].liquid = 0;
				}
			}
			else if (Main.tile[num, num2].liquid < 20)
			{
				if ((Main.tile[num - 1, num2].liquid < Main.tile[num, num2].liquid && (!Main.tile[num - 1, num2].active || !Main.tileSolid[Main.tile[num - 1, num2].type] || Main.tileSolidTop[Main.tile[num - 1, num2].type])) || (Main.tile[num + 1, num2].liquid < Main.tile[num, num2].liquid && (!Main.tile[num + 1, num2].active || !Main.tileSolid[Main.tile[num + 1, num2].type] || Main.tileSolidTop[Main.tile[num + 1, num2].type])) || (Main.tile[num, num2 + 1].liquid < byte.MaxValue && (!Main.tile[num, num2 + 1].active || !Main.tileSolid[Main.tile[num, num2 + 1].type] || Main.tileSolidTop[Main.tile[num, num2 + 1].type])))
				{
					Main.tile[num, num2].liquid = 0;
				}
			}
			else if (Main.tile[num, num2 + 1].liquid < byte.MaxValue && (!Main.tile[num, num2 + 1].active || !Main.tileSolid[Main.tile[num, num2 + 1].type] || Main.tileSolidTop[Main.tile[num, num2 + 1].type]) && !stuck)
			{
				Main.liquid[l].kill = 0;
				return;
			}
			if (Main.tile[num, num2].liquid < 250 && Main.tile[num, num2 - 1].liquid > 0)
			{
				AddWater(num, num2 - 1);
			}
			if (Main.tile[num, num2].liquid == 0)
			{
				Main.tile[num, num2].lava = false;
			}
			else
			{
				if ((Main.tile[num + 1, num2].liquid > 0 && Main.tile[num + 1, num2 + 1].liquid < 250 && !Main.tile[num + 1, num2 + 1].active) || (Main.tile[num - 1, num2].liquid > 0 && Main.tile[num - 1, num2 + 1].liquid < 250 && !Main.tile[num - 1, num2 + 1].active))
				{
					AddWater(num - 1, num2);
					AddWater(num + 1, num2);
				}
				if (Main.tile[num, num2].lava)
				{
					LavaCheck(num, num2);
					for (int i = num - 1; i <= num + 1; i++)
					{
						for (int j = num2 - 1; j <= num2 + 1; j++)
						{
							if (!Main.tile[i, j].active)
							{
								continue;
							}
							if (Main.tile[i, j].type == 2 || Main.tile[i, j].type == 23 || Main.tile[i, j].type == 109)
							{
								Main.tile[i, j].type = 0;
								WorldGen.SquareTileFrame(i, j);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num, num2, 3);
								}
							}
							else if (Main.tile[i, j].type == 60 || Main.tile[i, j].type == 70)
							{
								Main.tile[i, j].type = 59;
								WorldGen.SquareTileFrame(i, j);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num, num2, 3);
								}
							}
						}
					}
				}
			}
			if (Main.netMode == 2)
			{
				NetMessage.sendWater(num, num2);
			}
			numLiquid--;
			Main.tile[Main.liquid[l].x, Main.liquid[l].y].checkingLiquid = false;
			Main.liquid[l].x = Main.liquid[numLiquid].x;
			Main.liquid[l].y = Main.liquid[numLiquid].y;
			Main.liquid[l].kill = Main.liquid[numLiquid].kill;
			if (Main.tileAlch[Main.tile[num, num2].type])
			{
				WorldGen.CheckAlch(num, num2);
			}
		}
	}
}
