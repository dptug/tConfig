using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
	public class InterfaceObj
	{
		public Vector2[] slotLocation;

		public Item[] itemSlots;

		public string[] buttonText;

		public bool[] buttonClickable;

		public Vector2[] buttonLocation;

		public float[] buttonScale;

		public Color[] buttonColor;

		public Vector2 sourceLocation;

		public bool display;

		public int buttonCount;

		public int slotCount;

		public Interfaceable code;

		public string name;

		public InterfaceObj(Interfaceable item, int slotCount, int buttonCount)
		{
			code = item;
			display = true;
			this.slotCount = 0;
			this.buttonCount = 0;
			slotLocation = new Vector2[slotCount];
			itemSlots = new Item[slotCount];
			buttonText = new string[buttonCount];
			buttonClickable = new bool[buttonCount];
			buttonLocation = new Vector2[buttonCount];
			buttonScale = new float[buttonCount];
			buttonColor = new Color[buttonCount];
		}

		public void SetLocation(Vector2 vect)
		{
			sourceLocation = vect;
		}

		public void DropAll()
		{
			for (int i = 0; i < slotCount; i++)
			{
				if (itemSlots[i] != null && itemSlots[i].type > 0 && itemSlots[i].stack > 0 && code.DropSlot(i))
				{
					float x = Main.player[Main.myPlayer].position.X;
					float y = Main.player[Main.myPlayer].position.Y;
					int num = Item.NewItem((int)x, (int)y, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, itemSlots[i]);
					if (Main.netMode == 0)
					{
						Main.item[num].noGrabDelay = 10;
					}
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num);
					}
				}
			}
		}

		public int AddText(string text, int x, int y, bool clickable, float scale = 1f, Color color = default(Color))
		{
			if (color == default(Color))
			{
				color = Color.White;
			}
			buttonText[buttonCount] = text;
			buttonLocation[buttonCount] = new Vector2(x, y);
			buttonClickable[buttonCount] = clickable;
			buttonScale[buttonCount] = scale;
			buttonColor[buttonCount] = color;
			buttonCount++;
			return buttonCount - 1;
		}

		public int AddItemSlot(int x, int y)
		{
			slotLocation[slotCount] = new Vector2(x, y);
			itemSlots[slotCount] = new Item();
			slotCount++;
			return slotCount - 1;
		}

		public void Draw(ref string mouseTip)
		{
			if (!display || (Codable.RunSpecifiedMethod("Predraw interface " + name, code, "PreDraw", Interface.main.spriteBatch) && !(bool)Codable.customMethodReturn))
			{
				return;
			}
			for (int i = 0; i < buttonCount; i++)
			{
				DrawText(i);
			}
			for (int j = 0; j < slotCount; j++)
			{
				float inventoryScale = Main.inventoryScale;
				if (Codable.RunSpecifiedMethod("Predraw interface slot " + name, code, "PreDrawSlot", Interface.main.spriteBatch, j))
				{
					if ((bool)Codable.customMethodReturn)
					{
						DrawItemSlot((int)slotLocation[j].X, (int)slotLocation[j].Y, j, ref mouseTip);
					}
				}
				else
				{
					DrawItemSlot((int)slotLocation[j].X, (int)slotLocation[j].Y, j, ref mouseTip);
				}
				Codable.RunSpecifiedMethod("Postdraw interface slot " + name, code, "PostDrawSlot", Interface.main.spriteBatch, j);
				Main.inventoryScale = inventoryScale;
			}
			Codable.RunSpecifiedMethod("Postdraw interface " + name, code, "PostDraw", Interface.main.spriteBatch);
		}

		public void DrawText(int button)
		{
			int num = (int)buttonLocation[button].X;
			int num2 = (int)buttonLocation[button].Y;
			string text = buttonText[button];
			bool flag = buttonClickable[button];
			Color color = buttonColor[button];
			Vector2 origin = Main.fontMouseText.MeasureString(text);
			float num3 = buttonScale[button];
			if (flag && Main.mouseX > num && (float)Main.mouseX < (float)num + origin.X && Main.mouseY > num2 && (float)Main.mouseY < (float)num2 + origin.Y)
			{
				num3 += 0.2f;
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeftRelease && Main.mouseLeft)
				{
					code.ButtonClicked(button);
				}
			}
			origin *= 0.5f;
			Interface.main.spriteBatch.DrawString(Main.fontMouseText, text, new Vector2((float)num + origin.X, (float)num2 + origin.Y), color, 0f, origin, num3, SpriteEffects.None, 0f);
		}

		public void DrawItemSlot(int x, int y, int slot, ref string mouseTip)
		{
			Texture2D inventoryBack5Texture = Main.inventoryBack5Texture;
			Item[] array = itemSlots;
			if (array[slot].stack == 0)
			{
				array[slot] = new Item();
			}
			new Color(100, 100, 100, 100);
			if (Main.mouseX >= x && (float)Main.mouseX <= (float)x + (float)Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= y && (float)Main.mouseY <= (float)y + (float)Main.inventoryBackTexture.Height * Main.inventoryScale)
			{
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeftRelease && Main.mouseLeft)
				{
					if (Main.player[Main.myPlayer].itemAnimation <= 0 && code.CanPlaceSlot(slot, Main.mouseItem))
					{
						Item mouseItem = Main.mouseItem;
						Main.mouseItem = array[slot];
						array[slot] = mouseItem;
						if (array[slot].type == 0 || array[slot].stack < 1)
						{
							array[slot] = new Item();
						}
						if (Main.mouseItem.IsTheSameAs(array[slot]) && array[slot].stack != array[slot].maxStack && Main.mouseItem.stack != Main.mouseItem.maxStack)
						{
							if (Main.mouseItem.stack + array[slot].stack <= Main.mouseItem.maxStack)
							{
								array[slot].stack += Main.mouseItem.stack;
								Main.mouseItem.stack = 0;
							}
							else
							{
								int num = Main.mouseItem.maxStack - array[slot].stack;
								array[slot].stack += num;
								Main.mouseItem.stack -= num;
							}
						}
						if (Main.mouseItem.type == 0 || Main.mouseItem.stack < 1)
						{
							Main.mouseItem = new Item();
						}
						if (Main.mouseItem.type > 0 || array[slot].type > 0)
						{
							Recipe.FindRecipes();
							Main.PlaySound(7);
						}
						code.PlaceSlot(slot);
					}
				}
				else if (!Main.mouseRight || !Codable.RunSpecifiedMethod("Interface right-click", code, "SlotRightClicked", slot))
				{
					if (Main.mouseRight && Main.mouseRightRelease && array[slot].maxStack == 1)
					{
						array[slot] = Main.armorSwap(array[slot]);
					}
					else if (Main.stackSplit <= 1 && Main.mouseRight && array[slot].maxStack > 1 && (Main.mouseItem.IsTheSameAs(array[slot]) || Main.mouseItem.type == 0) && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == 0))
					{
						if (Main.mouseItem.type == 0)
						{
							Main.mouseItem = (Item)array[slot].Clone();
							Main.mouseItem.stack = 0;
						}
						Main.mouseItem.stack++;
						array[slot].stack--;
						if (array[slot].stack <= 0)
						{
							array[slot] = new Item();
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
					}
				}
				mouseTip = array[slot].name;
				Main.toolTip = (Item)array[slot].ShallowClone();
				if (array[slot].stack > 1)
				{
					object obj = mouseTip;
					mouseTip = string.Concat(obj, " (", array[slot].stack, ")");
				}
			}
			Interface.DrawItem(inventoryBack5Texture, array[slot], x, y);
		}
	}
}
