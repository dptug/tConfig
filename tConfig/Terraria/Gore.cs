using Microsoft.Xna.Framework;
using System;

namespace Terraria
{
	public class Gore
	{
		public static int goreTime = 600;

		public Vector2 position;

		public Vector2 velocity;

		public float rotation;

		public float scale;

		public int alpha;

		public int type;

		public float light;

		public bool active;

		public bool sticky = true;

		public int timeLeft = goreTime;

		public Color color;

		public void Update()
		{
			if (Main.netMode == 2 || !active)
			{
				return;
			}
			if (type == 11 || type == 12 || type == 13 || type == 61 || type == 62 || type == 63 || type == 99)
			{
				velocity.Y *= 0.98f;
				velocity.X *= 0.98f;
				scale -= 0.007f;
				if ((double)scale < 0.1)
				{
					scale = 0.1f;
					alpha = 255;
				}
			}
			else if (type == 16 || type == 17)
			{
				velocity.Y *= 0.98f;
				velocity.X *= 0.98f;
				scale -= 0.01f;
				if ((double)scale < 0.1)
				{
					scale = 0.1f;
					alpha = 255;
				}
			}
			else
			{
				velocity.Y += 0.2f;
			}
			rotation += velocity.X * 0.1f;
			if (sticky)
			{
				int num = Main.goreTexture[type].Width;
				if (Main.goreTexture[type].Height < num)
				{
					num = Main.goreTexture[type].Height;
				}
				num = (int)((float)num * 0.9f);
				velocity = Collision.TileCollision(position, velocity, (int)((float)num * scale), (int)((float)num * scale));
				if (velocity.Y == 0f)
				{
					velocity.X *= 0.97f;
					if ((double)velocity.X > -0.01 && (double)velocity.X < 0.01)
					{
						velocity.X = 0f;
					}
				}
				if (timeLeft > 0)
				{
					timeLeft--;
				}
				else
				{
					alpha++;
				}
			}
			else
			{
				alpha += 2;
			}
			position += velocity;
			if (alpha >= 255)
			{
				active = false;
			}
			if (light > 0f)
			{
				float num2 = light * scale;
				float num3 = light * scale;
				float num4 = light * scale;
				if (type == 16)
				{
					num4 *= 0.3f;
					num3 *= 0.8f;
				}
				else if (type == 17)
				{
					num3 *= 0.6f;
					num2 *= 0.3f;
				}
				Lighting.addLight((int)((position.X + (float)Main.goreTexture[type].Width * scale / 2f) / 16f), (int)((position.Y + (float)Main.goreTexture[type].Height * scale / 2f) / 16f), num2, num3, num4);
			}
		}

		public static int NewGore(Vector2 Position, Vector2 Velocity, string name, float Scale = 1f, int Type = -1)
		{
			if (Type != -1)
			{
				int num = NewGore(Position, Velocity, Type, Scale);
				Main.gore[num].type = Config.goreID[name];
				return num;
			}
			return NewGore(Position, Velocity, Config.goreID[name], Scale);
		}

		public static int NewGore(Vector2 Position, Vector2 Velocity, int Type, float Scale = 1f)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random();
			}
			if (Main.netMode == 2)
			{
				return 0;
			}
			int num = 200;
			for (int i = 0; i < 200; i++)
			{
				if (!Main.gore[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == 200)
			{
				return num;
			}
			Main.gore[num].color = default(Color);
			Main.gore[num].light = 0f;
			Main.gore[num].position = Position;
			Main.gore[num].velocity = Velocity;
			Gore gore = Main.gore[num];
			gore.velocity.Y = gore.velocity.Y - (float)Main.rand.Next(10, 31) * 0.1f;
			Gore gore2 = Main.gore[num];
			gore2.velocity.X = gore2.velocity.X + (float)Main.rand.Next(-20, 21) * 0.1f;
			Main.gore[num].type = Type;
			Main.gore[num].active = true;
			Main.gore[num].alpha = 0;
			Main.gore[num].rotation = 0f;
			Main.gore[num].scale = Scale;
			if (goreTime == 0 || Type == 11 || Type == 12 || Type == 13 || Type == 16 || Type == 17 || Type == 61 || Type == 62 || Type == 63 || Type == 99)
			{
				Main.gore[num].sticky = false;
			}
			else
			{
				Main.gore[num].sticky = true;
				Main.gore[num].timeLeft = goreTime;
			}
			if (Type == 16 || Type == 17)
			{
				Main.gore[num].alpha = 100;
				Main.gore[num].scale = 0.7f;
				Main.gore[num].light = 1f;
			}
			return num;
		}

		public Color GetAlpha(Color newColor)
		{
			float num = (float)(255 - alpha) / 255f;
			int r;
			int g;
			int b;
			if (type == 16 || type == 17)
			{
				r = newColor.R;
				g = newColor.G;
				b = newColor.B;
			}
			else
			{
				r = (int)((float)(int)newColor.R * num);
				g = (int)((float)(int)newColor.G * num);
				b = (int)((float)(int)newColor.B * num);
			}
			int num2 = newColor.A - alpha;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			return new Color(r, g, b, num2);
		}

		public Color GetColor(Color newColor)
		{
			float value = color.R - (255 - newColor.R);
			float value2 = color.G - (255 - newColor.G);
			float value3 = color.B - (255 - newColor.B);
			float value4 = color.A - (255 - newColor.A);
			return new Color(MathHelper.Clamp(value, 0f, 255f), MathHelper.Clamp(value2, 0f, 255f), MathHelper.Clamp(value3, 0f, 255f), MathHelper.Clamp(value4, 0f, 255f));
		}
	}
}
