using Microsoft.Xna.Framework;
using System;

namespace Terraria
{
	public class Cloud
	{
		public Vector2 position;

		public float scale;

		public float rotation;

		public float rSpeed;

		public float sSpeed;

		public bool active;

		public int type;

		public int width;

		public int height;

		private static Random rand = new Random();

		public static void resetClouds()
		{
			if (Main.cloudLimit < 10)
			{
				return;
			}
			Main.numClouds = rand.Next(10, Main.cloudLimit);
			Main.windSpeed = 0f;
			while (Main.windSpeed == 0f)
			{
				Main.windSpeed = (float)rand.Next(-100, 101) * 0.01f;
			}
			for (int i = 0; i < 100; i++)
			{
				Main.cloud[i].active = false;
			}
			for (int j = 0; j < Main.numClouds; j++)
			{
				addCloud();
			}
			for (int k = 0; k < Main.numClouds; k++)
			{
				if (Main.windSpeed < 0f)
				{
					Cloud cloud = Main.cloud[k];
					cloud.position.X = cloud.position.X - (float)(Main.screenWidth * 2);
				}
				else
				{
					Cloud cloud2 = Main.cloud[k];
					cloud2.position.X = cloud2.position.X + (float)(Main.screenWidth * 2);
				}
			}
		}

		public static void addCloud()
		{
			int num = -1;
			for (int i = 0; i < 100; i++)
			{
				if (!Main.cloud[i].active)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			Main.cloud[num].rSpeed = 0f;
			Main.cloud[num].sSpeed = 0f;
			Main.cloud[num].type = rand.Next(4);
			Main.cloud[num].scale = (float)rand.Next(70, 131) * 0.01f;
			Main.cloud[num].rotation = (float)rand.Next(-10, 11) * 0.01f;
			Main.cloud[num].width = (int)((float)Main.cloudTexture[Main.cloud[num].type].Width * Main.cloud[num].scale);
			Main.cloud[num].height = (int)((float)Main.cloudTexture[Main.cloud[num].type].Height * Main.cloud[num].scale);
			float num2 = Main.windSpeed;
			if (!Main.gameMenu)
			{
				num2 = Main.windSpeed - Main.player[Main.myPlayer].velocity.X * 0.1f;
			}
			if (num2 > 0f)
			{
				Main.cloud[num].position.X = 0f - (float)Main.cloud[num].width - (float)Main.cloudTexture[Main.cloud[num].type].Width - (float)rand.Next(Main.screenWidth * 2);
			}
			else
			{
				Main.cloud[num].position.X = Main.screenWidth + Main.cloudTexture[Main.cloud[num].type].Width + rand.Next(Main.screenWidth * 2);
			}
			Main.cloud[num].position.Y = rand.Next((int)((0f - (float)Main.screenHeight) * 0.25f), (int)((float)Main.screenHeight * 0.25f));
			Cloud cloud = Main.cloud[num];
			cloud.position.Y = cloud.position.Y - (float)rand.Next((int)((float)Main.screenHeight * 0.15f));
			Cloud cloud2 = Main.cloud[num];
			cloud2.position.Y = cloud2.position.Y - (float)rand.Next((int)((float)Main.screenHeight * 0.15f));
			if ((double)Main.cloud[num].scale > 1.3)
			{
				Main.cloud[num].scale = 1.3f;
			}
			if ((double)Main.cloud[num].scale < 0.7)
			{
				Main.cloud[num].scale = 0.7f;
			}
			Main.cloud[num].active = true;
			Rectangle rectangle = new Rectangle((int)Main.cloud[num].position.X, (int)Main.cloud[num].position.Y, Main.cloud[num].width, Main.cloud[num].height);
			for (int j = 0; j < 100; j++)
			{
				if (num != j && Main.cloud[j].active)
				{
					Rectangle value = new Rectangle((int)Main.cloud[j].position.X, (int)Main.cloud[j].position.Y, Main.cloud[j].width, Main.cloud[j].height);
					if (rectangle.Intersects(value))
					{
						Main.cloud[num].active = false;
					}
				}
			}
		}

		public Color cloudColor(Color bgColor)
		{
			float num = (scale - 0.4f) * 0.9f;
			float num2 = 1.1f;
			float num3 = 255f - (float)(255 - bgColor.R) * num2;
			float num4 = 255f - (float)(255 - bgColor.G) * num2;
			float num5 = 255f - (float)(255 - bgColor.B) * num2;
			float num6 = 255f;
			num3 *= num;
			num4 *= num;
			num5 *= num;
			num6 *= num;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			return new Color((byte)num3, (byte)num4, (byte)num5, (byte)num6);
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static void UpdateClouds()
		{
			int num = 0;
			for (int i = 0; i < 100; i++)
			{
				if (Main.cloud[i].active)
				{
					Main.cloud[i].rotation = 0f;
					Main.cloud[i].Update();
					num++;
				}
			}
			for (int j = 0; j < 100; j++)
			{
				if (Main.cloud[j].active)
				{
					if (j > 1 && (!Main.cloud[j - 1].active || (double)Main.cloud[j - 1].scale > (double)Main.cloud[j].scale + 0.02))
					{
						Cloud cloud = (Cloud)Main.cloud[j - 1].Clone();
						Main.cloud[j - 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud;
					}
					if (j < 99 && (!Main.cloud[j].active || (double)Main.cloud[j + 1].scale < (double)Main.cloud[j].scale - 0.02))
					{
						Cloud cloud2 = (Cloud)Main.cloud[j + 1].Clone();
						Main.cloud[j + 1] = (Cloud)Main.cloud[j].Clone();
						Main.cloud[j] = cloud2;
					}
				}
			}
			if (num < Main.numClouds)
			{
				addCloud();
			}
		}

		public void Update()
		{
			if (Main.gameMenu)
			{
				position.X += Main.windSpeed * scale * 3f;
			}
			else
			{
				float num = Main.player[Main.myPlayer].velocity.X * 0.18f;
				num = (Main.screenPosition.X - Main.screenLastPosition.X) * 0.18f;
				if (Main.player[Main.myPlayer].dead)
				{
					num = 0f;
				}
				position.X += (Main.windSpeed - num) * scale;
			}
			if (Main.windSpeed > 0f)
			{
				if (position.X - (float)Main.cloudTexture[type].Width > (float)(Main.screenWidth + 200))
				{
					active = false;
				}
			}
			else if (position.X + (float)width + (float)Main.cloudTexture[type].Width < -200f)
			{
				active = false;
			}
			rSpeed += (float)rand.Next(-10, 11) * 2E-05f;
			if ((double)rSpeed > 0.0007)
			{
				rSpeed = 0.0007f;
			}
			if ((double)rSpeed < -0.0007)
			{
				rSpeed = -0.0007f;
			}
			if ((double)rotation > 0.05)
			{
				rotation = 0.05f;
			}
			if ((double)rotation < -0.05)
			{
				rotation = -0.05f;
			}
			rotation += rSpeed;
			width = (int)((float)Main.cloudTexture[type].Width * scale);
			height = (int)((float)Main.cloudTexture[type].Height * scale);
		}
	}
}
