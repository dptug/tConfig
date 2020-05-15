using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
	public class Interface
	{
		public static Main main;

		public static void Initialize(Main main)
		{
			Interface.main = main;
		}

		public static void chestFunc(int type, int func)
		{
			Item[] chest = null;
			if (type > -1)
			{
				chest = Main.chest[Main.player[Main.myPlayer].chest].item;
			}
			if (type == -2)
			{
				chest = Main.player[Main.myPlayer].bank;
			}
			if (type == -3)
			{
				chest = Main.player[Main.myPlayer].bank2;
			}
			bool flag = false;
			if (type > -1)
			{
				flag = true;
			}
			if (func == 0)
			{
				chestLoot(chest, flag);
			}
			if (func == 1)
			{
				chestDeposit(chest, flag);
			}
			if (func == 2)
			{
				chestStack(chest, flag);
			}
		}

		public static void chestStack(Item[] chest, bool isChest)
		{
			for (int i = 0; i < 20; i++)
			{
				if (chest[i].type <= 0 || chest[i].stack >= chest[i].maxStack)
				{
					continue;
				}
				for (int j = 0; j < 48; j++)
				{
					if (chest[i].IsTheSameAs(Main.player[Main.myPlayer].inventory[j]))
					{
						int num = Main.player[Main.myPlayer].inventory[j].stack;
						if (chest[i].stack + num > chest[i].maxStack)
						{
							num = chest[i].maxStack - chest[i].stack;
						}
						Main.PlaySound(7);
						chest[i].stack += num;
						Main.player[Main.myPlayer].inventory[j].stack -= num;
						if (isChest)
						{
							Main.ChestCoins();
						}
						else
						{
							Main.BankCoins();
						}
						if (Main.player[Main.myPlayer].inventory[j].stack == 0)
						{
							Main.player[Main.myPlayer].inventory[j].SetDefaults(0);
						}
						else if (chest[i].type == 0)
						{
							chest[i] = (Item)Main.player[Main.myPlayer].inventory[j].Clone();
							Main.player[Main.myPlayer].inventory[j].SetDefaults(0);
						}
						if (isChest && Main.netMode == 1)
						{
							NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, i);
						}
					}
				}
			}
		}

		public static void chestDeposit(Item[] chest, bool isChest)
		{
			for (int num = 40; num >= 10; num--)
			{
				if (Main.player[Main.myPlayer].inventory[num].stack > 0 && Main.player[Main.myPlayer].inventory[num].type > 0)
				{
					if (Main.player[Main.myPlayer].inventory[num].maxStack > 1)
					{
						for (int i = 0; i < 20; i++)
						{
							if (chest[i].stack >= chest[i].maxStack || !Main.player[Main.myPlayer].inventory[num].IsTheSameAs(chest[i]))
							{
								continue;
							}
							int num2 = Main.player[Main.myPlayer].inventory[num].stack;
							if (Main.player[Main.myPlayer].inventory[num].stack + chest[i].stack > chest[i].maxStack)
							{
								num2 = chest[i].maxStack - chest[i].stack;
							}
							Main.player[Main.myPlayer].inventory[num].stack -= num2;
							chest[i].stack += num2;
							if (isChest)
							{
								Main.ChestCoins();
							}
							else
							{
								Main.BankCoins();
							}
							Main.PlaySound(7);
							if (Main.player[Main.myPlayer].inventory[num].stack <= 0)
							{
								Main.player[Main.myPlayer].inventory[num].SetDefaults(0);
								if (isChest && Main.netMode == 1)
								{
									NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, i);
								}
								break;
							}
							if (chest[i].type == 0)
							{
								chest[i] = (Item)Main.player[Main.myPlayer].inventory[num].Clone();
								Main.player[Main.myPlayer].inventory[num].SetDefaults(0);
							}
							if (isChest && Main.netMode == 1)
							{
								NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, i);
							}
						}
					}
					if (Main.player[Main.myPlayer].inventory[num].stack > 0)
					{
						for (int j = 0; j < 20; j++)
						{
							if (chest[j].stack == 0)
							{
								Main.PlaySound(7);
								chest[j] = (Item)Main.player[Main.myPlayer].inventory[num].Clone();
								Main.player[Main.myPlayer].inventory[num].SetDefaults(0);
								if (isChest && Main.netMode == 1)
								{
									NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, j);
								}
								break;
							}
						}
					}
				}
			}
		}

		public static void chestLoot(Item[] chest, bool netUpdate = false)
		{
			for (int i = 0; i < 20; i++)
			{
				if (chest[i].type > 0)
				{
					chest[i] = Main.player[Main.myPlayer].GetItem(Main.myPlayer, chest[i]);
					if (netUpdate && Main.netMode == 1)
					{
						NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, i);
					}
				}
			}
		}

		public static void drawChest(int type, ref string mouseTip)
		{
			if (type != -1)
			{
				string text = null;
				if (type > -1)
				{
					text = Main.chestText;
				}
				if (type == -2)
				{
					text = Lang.inter[32];
				}
				if (type == -3)
				{
					text = Lang.inter[33];
				}
				Item[] array = null;
				if (type > -1 && Main.chest[Main.player[Main.myPlayer].chest] != null)
				{
					array = Main.chest[Main.player[Main.myPlayer].chest].item;
				}
				if (type == -2)
				{
					array = Main.player[Main.myPlayer].bank;
				}
				if (type == -3)
				{
					array = Main.player[Main.myPlayer].bank2;
				}
				bool netUpdate = false;
				if (type > -1)
				{
					netUpdate = true;
				}
				Texture2D texture2D = null;
				if (type > -1)
				{
					texture2D = Main.inventoryBack5Texture;
				}
				if (type == -2 || type == -3)
				{
					texture2D = Main.inventoryBack2Texture;
				}
				if (text != null && array != null && texture2D != null)
				{
					drawChest(text, array, texture2D, netUpdate, ref mouseTip);
				}
			}
		}

		public static void drawChest(string chestName, Item[] chest, Texture2D invTexture, bool netUpdate, ref string mouseTip)
		{
			SpriteBatch spriteBatch = main.spriteBatch;
			new Color((byte)Main.invAlpha, (byte)Main.invAlpha, (byte)Main.invAlpha, (byte)Main.invAlpha);
			SpriteFont fontMouseText = Main.fontMouseText;
			Vector2 position = new Vector2(284f, 210f);
			Color color = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
			float rotation = 0f;
			spriteBatch.DrawString(fontMouseText, chestName, position, color, rotation, default(Vector2), 1f, SpriteEffects.None, 0f);
			Main.inventoryScale = 0.75f;
			if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 280f * Main.inventoryScale) && Main.mouseY > 210 && Main.mouseY < (int)(210f + 224f * Main.inventoryScale))
			{
				Main.player[Main.myPlayer].mouseInterface = true;
			}
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int num = (int)(73f + (float)(i * 56) * Main.inventoryScale);
					int num2 = (int)(210f + (float)(j * 56) * Main.inventoryScale);
					int num3 = i + j * 5;
					new Color(100, 100, 100, 100);
					if (Main.mouseX >= num && (float)Main.mouseX <= (float)num + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num2 && (float)Main.mouseY <= (float)num2 + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
					{
						Main.player[Main.myPlayer].mouseInterface = true;
						if (Main.mouseLeftRelease && Main.mouseLeft)
						{
							if (Main.player[Main.myPlayer].selectedItem != num3 || Main.player[Main.myPlayer].itemAnimation <= 0)
							{
								Item mouseItem = Main.mouseItem;
								Main.mouseItem = chest[num3];
								chest[num3] = mouseItem;
								if (chest[num3].type == 0 || chest[num3].stack < 1)
								{
									chest[num3] = new Item();
								}
								if (Main.mouseItem.IsTheSameAs(chest[num3]) && chest[num3].stack != chest[num3].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
								{
									if (Main.mouseItem.stack + chest[num3].stack <= Main.mouseItem.maxStack)
									{
										chest[num3].stack += Main.mouseItem.stack;
										Main.mouseItem.stack = 0;
									}
									else
									{
										int num4 = Main.mouseItem.maxStack - chest[num3].stack;
										chest[num3].stack += num4;
										Main.mouseItem.stack -= num4;
									}
								}
								if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
								{
									Main.mouseItem = new Item();
								}
								if (Main.mouseItem.type > 0 || chest[num3].type > 0)
								{
									Recipe.FindRecipes();
									Main.PlaySound(7);
								}
								if (netUpdate && Main.netMode == 1)
								{
									NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, num3);
								}
							}
						}
						else if (Main.mouseRight && Main.mouseRightRelease && chest[num3].maxStack == 1)
						{
							chest[num3] = Main.armorSwap(chest[num3]);
							if (netUpdate && Main.netMode == 1)
							{
								NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, num3);
							}
						}
						else if (Main.stackSplit <= 1 && Main.mouseRight && chest[num3].maxStack > 1 && (Main.mouseItem.IsTheSameAs(chest[num3]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
						{
							if (Main.mouseItem.type == 0)
							{
								Main.mouseItem = (Item)chest[num3].Clone();
								Main.mouseItem.stack = 0;
							}
							Main.mouseItem.stack++;
							chest[num3].stack--;
							if (chest[num3].stack <= 0)
							{
								chest[num3] = new Item();
							}
							Recipe.FindRecipes();
							Main.soundInstanceMenuTick.Stop();
							Main.soundInstanceMenuTick = Main.soundMenuTick.CreateInstance();
							Main.PlaySound(12);
							if (Main.stackSplit == 0)
							{
								Main.stackSplit = 15;
							}
							else
							{
								Main.stackSplit = Main.stackDelay;
							}
							if (netUpdate && Main.netMode == 1)
							{
								NetMessage.SendData(32, -1, -1, "", Main.player[Main.myPlayer].chest, num3);
							}
						}
						mouseTip = chest[num3].name;
						Main.toolTip = (Item)chest[num3].ShallowClone();
						if (chest[num3].stack > 1)
						{
							object obj = mouseTip;
							mouseTip = string.Concat(obj, " (", chest[num3].stack, ")");
						}
					}
					DrawItem(invTexture, chest[num3], num, num2);
				}
			}
		}

		public static void DrawItem(Texture2D texture, Item item, int x, int y)
		{
			Color color = new Color((byte)Main.invAlpha, (byte)Main.invAlpha, (byte)Main.invAlpha, (byte)Main.invAlpha);
			SpriteBatch spriteBatch = main.spriteBatch;
			Vector2 vector = new Vector2(x, y);
			Rectangle? sourceRectangle = new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height);
			Color color2 = color;
			float rotation = 0f;
			spriteBatch.Draw(texture, vector, sourceRectangle, color2, rotation, default(Vector2), Main.inventoryScale, SpriteEffects.None, 0f);
			_ = Color.White;
			ItemSlotRender.DrawItemInSlot(spriteBatch, color2, item, vector);
		}
	}
}
