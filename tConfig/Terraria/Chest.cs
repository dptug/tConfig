using Microsoft.Xna.Framework;

namespace Terraria
{
	public class Chest
	{
		public static int maxItems = 20;

		public Item[] item = new Item[maxItems];

		public int x;

		public int y;

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static void Unlock(int X, int Y)
		{
			Main.PlaySound(22, X * 16, Y * 16);
			for (int i = X; i <= X + 1; i++)
			{
				for (int j = Y; j <= Y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if ((Main.tile[i, j].frameX >= 72 && Main.tile[i, j].frameX <= 106) || (Main.tile[i, j].frameX >= 144 && Main.tile[i, j].frameX <= 178))
					{
						Tile tile = Main.tile[i, j];
						tile.frameX -= 36;
						for (int k = 0; k < 4; k++)
						{
							Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, 11);
						}
					}
				}
			}
		}

		public static int UsingChest(int i)
		{
			if (Main.chest[i] != null)
			{
				for (int j = 0; j < 255; j++)
				{
					if (Main.player[j].active && Main.player[j].chest == i)
					{
						return j;
					}
				}
			}
			return -1;
		}

		public static int FindChest(int X, int Y)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
				{
					return i;
				}
			}
			return -1;
		}

		public static int CreateChest(int X, int Y)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
				{
					return -1;
				}
			}
			for (int j = 0; j < 1000; j++)
			{
				if (Main.chest[j] == null)
				{
					Main.chest[j] = new Chest();
					Main.chest[j].x = X;
					Main.chest[j].y = Y;
					for (int k = 0; k < maxItems; k++)
					{
						Main.chest[j].item[k] = new Item();
					}
					return j;
				}
			}
			return -1;
		}

		public static bool DestroyChest(int X, int Y)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.chest[i] == null || Main.chest[i].x != X || Main.chest[i].y != Y)
				{
					continue;
				}
				for (int j = 0; j < maxItems; j++)
				{
					if (Main.chest[i].item[j].type > 0 && Main.chest[i].item[j].stack > 0)
					{
						return false;
					}
				}
				Main.chest[i] = null;
				return true;
			}
			return true;
		}

		public void AddShop(Item newItem)
		{
			int num = 0;
			while (true)
			{
				if (num < 19)
				{
					if (item[num] == null || item[num].type == 0)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			item[num] = (Item)newItem.Clone();
			item[num].buyOnce = true;
			if (item[num].value > 0)
			{
				item[num].value = item[num].value / 5;
				if (item[num].value < 1)
				{
					item[num].value = 1;
				}
			}
		}

		public void SetupShop(int type)
		{
			for (int i = 0; i < maxItems; i++)
			{
				item[i] = new Item();
			}
			int num = 0;
			if (type == 1)
			{
				int num2 = 0;
				item[num2].SetDefaults("Mining Helmet");
				num2++;
				item[num2].SetDefaults("Piggy Bank");
				num2++;
				item[num2].SetDefaults("Iron Anvil");
				num2++;
				item[num2].SetDefaults("Copper Pickaxe");
				num2++;
				item[num2].SetDefaults("Copper Axe");
				num2++;
				item[num2].SetDefaults("Torch");
				num2++;
				item[num2].SetDefaults("Lesser Healing Potion");
				num2++;
				if (Main.player[Main.myPlayer].statManaMax >= 200)
				{
					item[num2].SetDefaults("Lesser Mana Potion");
					num2++;
				}
				item[num2].SetDefaults("Wooden Arrow");
				num2++;
				item[num2].SetDefaults("Shuriken");
				num2++;
				if (Main.bloodMoon)
				{
					item[num2].SetDefaults("Throwing Knife");
					num2++;
				}
				if (!Main.dayTime)
				{
					item[num2].SetDefaults("Glowstick");
					num2++;
				}
				if (NPC.downedBoss3)
				{
					item[num2].SetDefaults("Safe");
					num2++;
				}
				if (Main.hardMode)
				{
					item[num2].SetDefaults(488);
					num2++;
				}
				num = num2;
			}
			else
			{
				if (type == 2)
				{
					int num3 = 0;
					item[num3].SetDefaults("Musket Ball");
					num3++;
					if (Main.bloodMoon || Main.hardMode)
					{
						item[num3].SetDefaults("Silver Bullet");
						num3++;
					}
					if ((NPC.downedBoss2 && !Main.dayTime) || Main.hardMode)
					{
						item[num3].SetDefaults(47);
						num3++;
					}
					item[num3].SetDefaults("Flintlock Pistol");
					num3++;
					item[num3].SetDefaults("Minishark");
					num3++;
					if (!Main.dayTime)
					{
						item[num3].SetDefaults(324);
						num3++;
					}
					if (Main.hardMode)
					{
						item[num3].SetDefaults(534);
					}
					num3++;
					num = num3;
				}
				if (type == 3)
				{
					int num4 = 0;
					if (Main.bloodMoon)
					{
						item[num4].SetDefaults(67);
						num4++;
						item[num4].SetDefaults(59);
						num4++;
					}
					else
					{
						item[num4].SetDefaults("Purification Powder");
						num4++;
						item[num4].SetDefaults("Grass Seeds");
						num4++;
						item[num4].SetDefaults("Sunflower");
						num4++;
					}
					item[num4].SetDefaults("Acorn");
					num4++;
					item[num4].SetDefaults(114);
					num4++;
					if (Main.hardMode)
					{
						item[num4].SetDefaults(369);
					}
					num4++;
					num = num4;
				}
				if (type == 4)
				{
					int num5 = 0;
					item[num5].SetDefaults("Grenade");
					num5++;
					item[num5].SetDefaults("Bomb");
					num5++;
					item[num5].SetDefaults("Dynamite");
					num5++;
					if (Main.hardMode)
					{
						item[num5].SetDefaults("Hellfire Arrow");
					}
					num5++;
					num = num5;
				}
				if (type == 5)
				{
					int num6 = 0;
					item[num6].SetDefaults(254);
					num6++;
					if (Main.dayTime)
					{
						item[num6].SetDefaults(242);
						num6++;
					}
					if (Main.moonPhase == 0)
					{
						item[num6].SetDefaults(245);
						num6++;
						item[num6].SetDefaults(246);
						num6++;
					}
					else if (Main.moonPhase == 1)
					{
						item[num6].SetDefaults(325);
						num6++;
						item[num6].SetDefaults(326);
						num6++;
					}
					item[num6].SetDefaults(269);
					num6++;
					item[num6].SetDefaults(270);
					num6++;
					item[num6].SetDefaults(271);
					num6++;
					if (NPC.downedClown)
					{
						item[num6].SetDefaults(503);
						num6++;
						item[num6].SetDefaults(504);
						num6++;
						item[num6].SetDefaults(505);
						num6++;
					}
					if (Main.bloodMoon)
					{
						item[num6].SetDefaults(322);
						num6++;
					}
					num = num6;
				}
				else
				{
					if (type == 6)
					{
						int num7 = 0;
						item[num7].SetDefaults(128);
						num7++;
						item[num7].SetDefaults(486);
						num7++;
						item[num7].SetDefaults(398);
						num7++;
						item[num7].SetDefaults(84);
						num7++;
						item[num7].SetDefaults(407);
						num7++;
						item[num7].SetDefaults(161);
						num7++;
						num = num7;
					}
					if (type == 7)
					{
						int num8 = 0;
						item[num8].SetDefaults(487);
						num8++;
						item[num8].SetDefaults(496);
						num8++;
						item[num8].SetDefaults(500);
						num8++;
						item[num8].SetDefaults(507);
						num8++;
						item[num8].SetDefaults(508);
						num8++;
						item[num8].SetDefaults(531);
						num8++;
						item[num8].SetDefaults(576);
						num8++;
						num = num8;
					}
					if (type == 8)
					{
						int num9 = 0;
						item[num9].SetDefaults(509);
						num9++;
						item[num9].SetDefaults(510);
						num9++;
						item[num9].SetDefaults(530);
						num9++;
						item[num9].SetDefaults(513);
						num9++;
						item[num9].SetDefaults(538);
						num9++;
						item[num9].SetDefaults(529);
						num9++;
						item[num9].SetDefaults(541);
						num9++;
						item[num9].SetDefaults(542);
						num9++;
						item[num9].SetDefaults(543);
						num9++;
						num = num9;
					}
					if (type == 9)
					{
						int num10 = 0;
						item[num10].SetDefaults(588);
						num10++;
						item[num10].SetDefaults(589);
						num10++;
						item[num10].SetDefaults(590);
						num10++;
						item[num10].SetDefaults(597);
						num10++;
						item[num10].SetDefaults(598);
						num10++;
						item[num10].SetDefaults(596);
						num10++;
						num = num10;
					}
				}
			}
			if (Main.player[Main.myPlayer].talkNPC > -1)
			{
				Main.npc[Main.player[Main.myPlayer].talkNPC].RunMethod("UpdateShop", this, num);
			}
		}
	}
}
