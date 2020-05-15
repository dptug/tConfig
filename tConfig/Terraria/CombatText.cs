using Microsoft.Xna.Framework;

namespace Terraria
{
	public class CombatText
	{
		public static Color PlayerHurtColor = new Color(255, 80, 90, 255);

		public static Color CritHurtColor = new Color(255, 100, 30, 255);

		public static Color NPCHurtColor = new Color(255, 160, 80, 255);

		public Vector2 position;

		public Vector2 velocity;

		public float alpha;

		public int alphaDir = 1;

		public string text;

		public float scale = 1f;

		public float rotation;

		public Color color;

		public bool active;

		public int lifeTime;

		public bool crit;

		public static void NewText(Rectangle location, Color color, string text, bool Crit = false)
		{
			if (Main.netMode == 2)
			{
				return;
			}
			int num = 0;
			while (true)
			{
				if (num < 100)
				{
					if (!Main.combatText[num].active)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			int num2 = 0;
			if (Crit)
			{
				num2 = 1;
			}
			Vector2 vector = Main.fontCombatText[num2].MeasureString(text);
			Main.combatText[num].alpha = 1f;
			Main.combatText[num].alphaDir = -1;
			Main.combatText[num].active = true;
			Main.combatText[num].scale = 0f;
			Main.combatText[num].rotation = 0f;
			Main.combatText[num].position.X = (float)location.X + (float)location.Width * 0.5f - vector.X * 0.5f;
			Main.combatText[num].position.Y = (float)location.Y + (float)location.Height * 0.25f - vector.Y * 0.5f;
			CombatText combatText = Main.combatText[num];
			combatText.position.X = combatText.position.X + (float)Main.rand.Next(-(int)((double)location.Width * 0.5), (int)((double)location.Width * 0.5) + 1);
			CombatText combatText2 = Main.combatText[num];
			combatText2.position.Y = combatText2.position.Y + (float)Main.rand.Next(-(int)((double)location.Height * 0.5), (int)((double)location.Height * 0.5) + 1);
			Main.combatText[num].color = color;
			Main.combatText[num].text = text;
			Main.combatText[num].velocity.Y = -7f;
			Main.combatText[num].lifeTime = 60;
			Main.combatText[num].crit = Crit;
			if (Crit)
			{
				Main.combatText[num].text = text;
				Main.combatText[num].color = CritHurtColor;
				Main.combatText[num].lifeTime *= 2;
				CombatText combatText3 = Main.combatText[num];
				combatText3.velocity.Y = combatText3.velocity.Y * 2f;
				Main.combatText[num].velocity.X = (float)Main.rand.Next(-25, 26) * 0.05f;
				Main.combatText[num].rotation = (float)(Main.combatText[num].lifeTime / 2) * 0.002f;
				if (Main.combatText[num].velocity.X < 0f)
				{
					Main.combatText[num].rotation *= -1f;
				}
			}
		}

		public void Update()
		{
			if (!active)
			{
				return;
			}
			alpha += (float)alphaDir * 0.05f;
			if ((double)alpha <= 0.6)
			{
				alphaDir = 1;
			}
			if (alpha >= 1f)
			{
				alpha = 1f;
				alphaDir = -1;
			}
			velocity.Y *= 0.92f;
			if (crit)
			{
				velocity.Y *= 0.92f;
			}
			velocity.X *= 0.93f;
			position += velocity;
			lifeTime--;
			if (lifeTime <= 0)
			{
				scale -= 0.1f;
				if ((double)scale < 0.1)
				{
					active = false;
				}
				lifeTime = 0;
				if (crit)
				{
					alphaDir = -1;
					scale += 0.07f;
				}
				return;
			}
			if (crit)
			{
				if (velocity.X < 0f)
				{
					rotation += 0.001f;
				}
				else
				{
					rotation -= 0.001f;
				}
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

		public static void UpdateCombatText()
		{
			for (int i = 0; i < 100; i++)
			{
				if (Main.combatText[i].active)
				{
					Main.combatText[i].Update();
				}
			}
		}
	}
}
