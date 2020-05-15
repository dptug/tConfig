using Microsoft.Xna.Framework;

namespace Terraria
{
	public class ItemText
	{
		public Vector2 position;

		public Vector2 velocity;

		public float alpha;

		public int alphaDir = 1;

		public string name;

		public int stack;

		public float scale = 1f;

		public float rotation;

		public Color color;

		public bool active;

		public int lifeTime;

		public static int activeTime = 60;

		public static int numActive;

		public static void NewText(Item newItem, int stack)
		{
			if (!Main.showItemText || newItem.name == null || !newItem.active || Main.netMode == 2)
			{
				return;
			}
			for (int i = 0; i < 20; i++)
			{
				if (Main.itemText[i].active && Main.itemText[i].name == newItem.name)
				{
					string text = newItem.name + " (" + (Main.itemText[i].stack + stack) + ")";
					string text2 = newItem.name;
					if (Main.itemText[i].stack > 1)
					{
						object obj = text2;
						text2 = string.Concat(obj, " (", Main.itemText[i].stack, ")");
					}
					Vector2 vector = Main.fontMouseText.MeasureString(text2);
					vector = Main.fontMouseText.MeasureString(text);
					if (Main.itemText[i].lifeTime < 0)
					{
						Main.itemText[i].scale = 1f;
					}
					Main.itemText[i].lifeTime = 60;
					Main.itemText[i].stack += stack;
					Main.itemText[i].scale = 0f;
					Main.itemText[i].rotation = 0f;
					Main.itemText[i].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector.X * 0.5f;
					Main.itemText[i].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector.Y * 0.5f;
					Main.itemText[i].velocity.Y = -7f;
					return;
				}
			}
			int num = -1;
			for (int j = 0; j < 20; j++)
			{
				if (!Main.itemText[j].active)
				{
					num = j;
					break;
				}
			}
			if (num == -1)
			{
				double num2 = Main.bottomWorld;
				for (int k = 0; k < 20; k++)
				{
					if (num2 > (double)Main.itemText[k].position.Y)
					{
						num = k;
						num2 = Main.itemText[k].position.Y;
					}
				}
			}
			if (num >= 0)
			{
				string text3 = newItem.AffixName();
				if (stack > 1)
				{
					object obj2 = text3;
					text3 = string.Concat(obj2, " (", stack, ")");
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text3);
				Main.itemText[num].alpha = 1f;
				Main.itemText[num].alphaDir = -1;
				Main.itemText[num].active = true;
				Main.itemText[num].scale = 0f;
				Main.itemText[num].rotation = 0f;
				Main.itemText[num].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
				Main.itemText[num].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
				Main.itemText[num].color = Color.White;
				if (Main.colorRarity.ContainsKey(newItem.rare))
				{
					Main.itemText[num].color = new Color((int)Main.colorRarity[newItem.rare][0], (int)Main.colorRarity[newItem.rare][1], (int)Main.colorRarity[newItem.rare][2]);
				}
				Main.itemText[num].name = newItem.AffixName();
				Main.itemText[num].stack = stack;
				Main.itemText[num].velocity.Y = -7f;
				Main.itemText[num].lifeTime = 60;
			}
		}

		public void Update(int whoAmI)
		{
			if (!active)
			{
				return;
			}
			alpha += (float)alphaDir * 0.01f;
			if ((double)alpha <= 0.7)
			{
				alpha = 0.7f;
				alphaDir = 1;
			}
			if (alpha >= 1f)
			{
				alpha = 1f;
				alphaDir = -1;
			}
			bool flag = false;
			string text = name;
			if (stack > 1)
			{
				object obj = text;
				text = string.Concat(obj, " (", stack, ")");
			}
			Vector2 vector = Main.fontMouseText.MeasureString(text);
			vector *= scale;
			vector.Y *= 0.8f;
			Rectangle rectangle = new Rectangle((int)(position.X - vector.X / 2f), (int)(position.Y - vector.Y / 2f), (int)vector.X, (int)vector.Y);
			for (int i = 0; i < 20; i++)
			{
				if (!Main.itemText[i].active || i == whoAmI)
				{
					continue;
				}
				string text2 = Main.itemText[i].name;
				if (Main.itemText[i].stack > 1)
				{
					object obj2 = text2;
					text2 = string.Concat(obj2, " (", Main.itemText[i].stack, ")");
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
				vector2 *= Main.itemText[i].scale;
				vector2.Y *= 0.8f;
				Rectangle value = new Rectangle((int)(Main.itemText[i].position.X - vector2.X / 2f), (int)(Main.itemText[i].position.Y - vector2.Y / 2f), (int)vector2.X, (int)vector2.Y);
				if (rectangle.Intersects(value) && (position.Y < Main.itemText[i].position.Y || (position.Y == Main.itemText[i].position.Y && whoAmI < i)))
				{
					flag = true;
					int num = numActive;
					if (num > 3)
					{
						num = 3;
					}
					Main.itemText[i].lifeTime = activeTime + 15 * num;
					lifeTime = activeTime + 15 * num;
				}
			}
			if (!flag)
			{
				velocity.Y *= 0.86f;
				if (scale == 1f)
				{
					velocity.Y *= 0.4f;
				}
			}
			else if (velocity.Y > -6f)
			{
				velocity.Y -= 0.2f;
			}
			else
			{
				velocity.Y *= 0.86f;
			}
			velocity.X *= 0.93f;
			position += velocity;
			lifeTime--;
			if (lifeTime <= 0)
			{
				scale -= 0.03f;
				if ((double)scale < 0.1)
				{
					active = false;
				}
				lifeTime = 0;
				return;
			}
			if (scale < 1f)
			{
				scale += 0.1f;
			}
			if (scale > 1f)
			{
				scale = 1f;
			}
		}

		public static void UpdateItemText()
		{
			int num = 0;
			for (int i = 0; i < 20; i++)
			{
				if (Main.itemText[i].active)
				{
					num++;
					Main.itemText[i].Update(i);
				}
			}
			numActive = num;
		}
	}
}
