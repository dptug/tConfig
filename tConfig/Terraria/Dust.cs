using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria
{
	public class Dust
	{
		public Vector2 position;

		public Vector2 velocity;

		public static int lavaBubbles;

		public float fadeIn;

		public bool noGravity;

		public float scale;

		public float rotation;

		public bool noLight;

		public bool active;

		public int type;

		public Color color;

		public int alpha;

		public Rectangle frame;

		public Texture2D OverrideTexture;

		public Action OverrideUpdate;

		public static int NewDust(Vector2 Position, int Width, int Height, int Type, float SpeedX = 0f, float SpeedY = 0f, int Alpha = 0, Color newColor = default(Color), float Scale = 1f)
		{
			if (Main.gameMenu)
			{
				return 0;
			}
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			if (Main.gamePaused)
			{
				return 0;
			}
			if (WorldGen.gen)
			{
				return 0;
			}
			if (Main.netMode == 2)
			{
				return 0;
			}
			Rectangle rectangle = new Rectangle((int)(Main.player[Main.myPlayer].position.X - (float)(Main.screenWidth / 2) - 100f), (int)(Main.player[Main.myPlayer].position.Y - (float)(Main.screenHeight / 2) - 100f), Main.screenWidth + 200, Main.screenHeight + 200);
			Rectangle value = new Rectangle((int)Position.X, (int)Position.Y, 10, 10);
			if (!rectangle.Intersects(value))
			{
				return 2000;
			}
			int result = 2000;
			for (int i = 0; i < 2000; i++)
			{
				if (Main.dust[i].active)
				{
					continue;
				}
				int num = Width;
				int num2 = Height;
				if (num < 5)
				{
					num = 5;
				}
				if (num2 < 5)
				{
					num2 = 5;
				}
				result = i;
				Main.dust[i].fadeIn = 0f;
				Main.dust[i].active = true;
				Main.dust[i].type = Type;
				Main.dust[i].noGravity = false;
				Main.dust[i].color = newColor;
				Main.dust[i].alpha = Alpha;
				Main.dust[i].position.X = Position.X + (float)Main.rand.Next(num - 4) + 4f;
				Main.dust[i].position.Y = Position.Y + (float)Main.rand.Next(num2 - 4) + 4f;
				Main.dust[i].velocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedX;
				Main.dust[i].velocity.Y = (float)Main.rand.Next(-20, 21) * 0.1f + SpeedY;
				Main.dust[i].frame.X = 10 * Type;
				Main.dust[i].frame.Y = 10 * Main.rand.Next(3);
				Main.dust[i].frame.Width = 8;
				Main.dust[i].frame.Height = 8;
				Main.dust[i].rotation = 0f;
				Main.dust[i].scale = 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
				Main.dust[i].scale *= Scale;
				Main.dust[i].noLight = false;
				Main.dust[i].OverrideTexture = null;
				Main.dust[i].OverrideUpdate = null;
				if (Main.dust[i].type == 6 || Main.dust[i].type == 75 || Main.dust[i].type == 29 || (Main.dust[i].type >= 59 && Main.dust[i].type <= 65))
				{
					Main.dust[i].velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
					Dust dust = Main.dust[i];
					dust.velocity.X = dust.velocity.X * 0.3f;
					Main.dust[i].scale *= 0.7f;
				}
				if (Main.dust[i].type == 33 || Main.dust[i].type == 52)
				{
					Main.dust[i].alpha = 170;
					Dust dust2 = Main.dust[i];
					dust2.velocity *= 0.5f;
					Dust dust3 = Main.dust[i];
					dust3.velocity.Y = dust3.velocity.Y + 1f;
				}
				if (Main.dust[i].type == 41)
				{
					Dust dust4 = Main.dust[i];
					dust4.velocity *= 0f;
				}
				if (Main.dust[i].type == 34 || Main.dust[i].type == 35)
				{
					Dust dust5 = Main.dust[i];
					dust5.velocity *= 0.1f;
					Main.dust[i].velocity.Y = -0.5f;
					if (Main.dust[i].type == 34 && !Collision.WetCollision(new Vector2(Main.dust[i].position.X, Main.dust[i].position.Y - 8f), 4, 4))
					{
						Main.dust[i].active = false;
					}
				}
				break;
			}
			return result;
		}

		public static void UpdateDust()
		{
			lavaBubbles = 0;
			Main.snowDust = 0;
			for (int i = 0; i < 2000; i++)
			{
				if (i < Main.numDust)
				{
					if (!Main.dust[i].active)
					{
						continue;
					}
					if (Main.dust[i].type == 35)
					{
						lavaBubbles++;
					}
					if (Main.dust[i].OverrideUpdate != null)
					{
						Main.dust[i].OverrideUpdate();
						continue;
					}
					Dust dust = Main.dust[i];
					dust.position += Main.dust[i].velocity;
					if (Main.dust[i].type == 6 || Main.dust[i].type == 75 || Main.dust[i].type == 29 || (Main.dust[i].type >= 59 && Main.dust[i].type <= 65))
					{
						if (!Main.dust[i].noGravity)
						{
							Dust dust2 = Main.dust[i];
							dust2.velocity.Y = dust2.velocity.Y + 0.05f;
						}
						if (!Main.dust[i].noLight)
						{
							float num = Main.dust[i].scale * 1.4f;
							if (Main.dust[i].type == 29)
							{
								if (num > 1f)
								{
									num = 1f;
								}
								Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num * 0.1f, num * 0.4f, num);
							}
							if (Main.dust[i].type == 75)
							{
								if (num > 1f)
								{
									num = 1f;
								}
								Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num * 0.7f, num, num * 0.2f);
							}
							else if (Main.dust[i].type >= 59 && Main.dust[i].type <= 65)
							{
								if (num > 0.8f)
								{
									num = 0.8f;
								}
								int num2 = Main.dust[i].type - 58;
								float num3 = 1f;
								float num4 = 1f;
								float num5 = 1f;
								switch (num2)
								{
								case 1:
									num3 = 0f;
									num4 = 0.1f;
									num5 = 1.3f;
									break;
								case 2:
									num3 = 1f;
									num4 = 0.1f;
									num5 = 0.1f;
									break;
								case 3:
									num3 = 0f;
									num4 = 1f;
									num5 = 0.1f;
									break;
								case 4:
									num3 = 0.9f;
									num4 = 0f;
									num5 = 0.9f;
									break;
								case 5:
									num3 = 1.3f;
									num4 = 1.3f;
									num5 = 1.3f;
									break;
								case 6:
									num3 = 0.9f;
									num4 = 0.9f;
									num5 = 0f;
									break;
								case 7:
									num3 = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
									num4 = 0.3f;
									num5 = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
									break;
								}
								Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num * num3, num * num4, num * num5);
							}
							else
							{
								if (num > 0.6f)
								{
									num = 0.6f;
								}
								Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num, num * 0.65f, num * 0.4f);
							}
						}
					}
					else if (Main.dust[i].type == 14 || Main.dust[i].type == 16 || Main.dust[i].type == 31 || Main.dust[i].type == 46)
					{
						Dust dust3 = Main.dust[i];
						dust3.velocity.Y = dust3.velocity.Y * 0.98f;
						Dust dust4 = Main.dust[i];
						dust4.velocity.X = dust4.velocity.X * 0.98f;
						if (Main.dust[i].type == 31 && Main.dust[i].noGravity)
						{
							Dust dust5 = Main.dust[i];
							dust5.velocity *= 1.02f;
							Main.dust[i].scale += 0.02f;
							Main.dust[i].alpha += 4;
							if (Main.dust[i].alpha > 255)
							{
								Main.dust[i].scale = 0.0001f;
								Main.dust[i].alpha = 255;
							}
						}
					}
					else if (Main.dust[i].type == 32)
					{
						Main.dust[i].scale -= 0.01f;
						Dust dust6 = Main.dust[i];
						dust6.velocity.X = dust6.velocity.X * 0.96f;
						Dust dust7 = Main.dust[i];
						dust7.velocity.Y = dust7.velocity.Y + 0.1f;
					}
					else if (Main.dust[i].type == 43)
					{
						Main.dust[i].rotation += 0.1f * Main.dust[i].scale;
						Color color = Lighting.GetColor((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f));
						float num6 = (float)(int)color.R / 270f;
						float num7 = (float)(int)color.G / 270f;
						float num8 = (float)(int)color.B / 270f;
						num6 *= Main.dust[i].scale * 1.07f;
						num7 *= Main.dust[i].scale * 1.07f;
						num8 *= Main.dust[i].scale * 1.07f;
						if (Main.dust[i].alpha < 255)
						{
							Main.dust[i].scale += 0.09f;
							if (Main.dust[i].scale >= 1f)
							{
								Main.dust[i].scale = 1f;
								Main.dust[i].alpha = 255;
							}
						}
						else
						{
							if ((double)Main.dust[i].scale < 0.8)
							{
								Main.dust[i].scale -= 0.01f;
							}
							if ((double)Main.dust[i].scale < 0.5)
							{
								Main.dust[i].scale -= 0.01f;
							}
						}
						if ((double)num6 < 0.05 && (double)num7 < 0.05 && (double)num8 < 0.05)
						{
							Main.dust[i].active = false;
						}
						else
						{
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num6, num7, num8);
						}
					}
					else if (Main.dust[i].type == 15 || Main.dust[i].type == 57 || Main.dust[i].type == 58)
					{
						Dust dust8 = Main.dust[i];
						dust8.velocity.Y = dust8.velocity.Y * 0.98f;
						Dust dust9 = Main.dust[i];
						dust9.velocity.X = dust9.velocity.X * 0.98f;
						float num9 = Main.dust[i].scale;
						if (Main.dust[i].type != 15)
						{
							num9 = Main.dust[i].scale * 0.8f;
						}
						if (Main.dust[i].noLight)
						{
							Dust dust10 = Main.dust[i];
							dust10.velocity *= 0.95f;
						}
						if (num9 > 1f)
						{
							num9 = 1f;
						}
						if (Main.dust[i].type == 15)
						{
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num9 * 0.45f, num9 * 0.55f, num9);
						}
						else if (Main.dust[i].type == 57)
						{
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num9 * 0.95f, num9 * 0.95f, num9 * 0.45f);
						}
						else if (Main.dust[i].type == 58)
						{
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num9, num9 * 0.55f, num9 * 0.75f);
						}
					}
					else if (Main.dust[i].type == 66)
					{
						if (Main.dust[i].velocity.X < 0f)
						{
							Main.dust[i].rotation -= 1f;
						}
						else
						{
							Main.dust[i].rotation += 1f;
						}
						Dust dust11 = Main.dust[i];
						dust11.velocity.Y = dust11.velocity.Y * 0.98f;
						Dust dust12 = Main.dust[i];
						dust12.velocity.X = dust12.velocity.X * 0.98f;
						Main.dust[i].scale += 0.02f;
						float num10 = Main.dust[i].scale;
						if (Main.dust[i].type != 15)
						{
							num10 = Main.dust[i].scale * 0.8f;
						}
						if (num10 > 1f)
						{
							num10 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num10 * ((float)(int)Main.dust[i].color.R / 255f), num10 * ((float)(int)Main.dust[i].color.G / 255f), num10 * ((float)(int)Main.dust[i].color.B / 255f));
					}
					else if (Main.dust[i].type == 20 || Main.dust[i].type == 21)
					{
						Main.dust[i].scale += 0.005f;
						Dust dust13 = Main.dust[i];
						dust13.velocity.Y = dust13.velocity.Y * 0.94f;
						Dust dust14 = Main.dust[i];
						dust14.velocity.X = dust14.velocity.X * 0.94f;
						float num11 = Main.dust[i].scale * 0.8f;
						if (num11 > 1f)
						{
							num11 = 1f;
						}
						if (Main.dust[i].type == 21)
						{
							num11 = Main.dust[i].scale * 0.4f;
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num11 * 0.8f, num11 * 0.3f, num11);
						}
						else
						{
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num11 * 0.3f, num11 * 0.6f, num11);
						}
					}
					else if (Main.dust[i].type == 27 || Main.dust[i].type == 45)
					{
						Dust dust15 = Main.dust[i];
						dust15.velocity *= 0.94f;
						Main.dust[i].scale += 0.002f;
						float num12 = Main.dust[i].scale;
						if (Main.dust[i].noLight)
						{
							num12 *= 0.1f;
							Main.dust[i].scale -= 0.06f;
							if (Main.dust[i].scale < 1f)
							{
								Main.dust[i].scale -= 0.06f;
							}
							if (Main.player[Main.myPlayer].wet)
							{
								Dust dust16 = Main.dust[i];
								dust16.position += Main.player[Main.myPlayer].velocity * 0.5f;
							}
							else
							{
								Dust dust17 = Main.dust[i];
								dust17.position += Main.player[Main.myPlayer].velocity;
							}
						}
						if (num12 > 1f)
						{
							num12 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num12 * 0.6f, num12 * 0.2f, num12);
					}
					else if (Main.dust[i].type == 55 || Main.dust[i].type == 56 || Main.dust[i].type == 73 || Main.dust[i].type == 74)
					{
						Dust dust18 = Main.dust[i];
						dust18.velocity *= 0.98f;
						float num13 = Main.dust[i].scale * 0.8f;
						if (Main.dust[i].type == 55)
						{
							if (num13 > 1f)
							{
								num13 = 1f;
							}
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num13, num13, num13 * 0.6f);
						}
						else if (Main.dust[i].type == 73)
						{
							if (num13 > 1f)
							{
								num13 = 1f;
							}
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num13, num13 * 0.35f, num13 * 0.5f);
						}
						else if (Main.dust[i].type == 74)
						{
							if (num13 > 1f)
							{
								num13 = 1f;
							}
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num13 * 0.35f, num13, num13 * 0.5f);
						}
						else
						{
							num13 = Main.dust[i].scale * 1.2f;
							if (num13 > 1f)
							{
								num13 = 1f;
							}
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num13 * 0.35f, num13 * 0.5f, num13);
						}
					}
					else if (Main.dust[i].type == 71 || Main.dust[i].type == 72)
					{
						Dust dust19 = Main.dust[i];
						dust19.velocity *= 0.98f;
						float num14 = Main.dust[i].scale;
						if (num14 > 1f)
						{
							num14 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num14 * 0.2f, 0f, num14 * 0.1f);
					}
					else if (Main.dust[i].type == 76)
					{
						Main.snowDust++;
						Main.dust[i].scale += 0.009f;
						Dust dust20 = Main.dust[i];
						dust20.position += Main.player[Main.myPlayer].velocity * 0.2f;
					}
					else if (!Main.dust[i].noGravity && Main.dust[i].type != 41 && Main.dust[i].type != 44)
					{
						Dust dust21 = Main.dust[i];
						dust21.velocity.Y = dust21.velocity.Y + 0.1f;
					}
					if (Main.dust[i].type == 5 && Main.dust[i].noGravity)
					{
						Main.dust[i].scale -= 0.04f;
					}
					if (Main.dust[i].type == 33 || Main.dust[i].type == 52)
					{
						if (Main.dust[i].velocity.X == 0f)
						{
							if (Collision.SolidCollision(Main.dust[i].position, 2, 2))
							{
								Main.dust[i].scale = 0f;
							}
							Main.dust[i].rotation += 0.5f;
							Main.dust[i].scale -= 0.01f;
						}
						if (Collision.WetCollision(new Vector2(Main.dust[i].position.X, Main.dust[i].position.Y), 4, 4))
						{
							Main.dust[i].alpha += 20;
							Main.dust[i].scale -= 0.1f;
						}
						Main.dust[i].alpha += 2;
						Main.dust[i].scale -= 0.005f;
						if (Main.dust[i].alpha > 255)
						{
							Main.dust[i].scale = 0f;
						}
						Dust dust22 = Main.dust[i];
						dust22.velocity.X = dust22.velocity.X * 0.93f;
						if (Main.dust[i].velocity.Y > 4f)
						{
							Main.dust[i].velocity.Y = 4f;
						}
						if (Main.dust[i].noGravity)
						{
							if (Main.dust[i].velocity.X < 0f)
							{
								Main.dust[i].rotation -= 0.2f;
							}
							else
							{
								Main.dust[i].rotation += 0.2f;
							}
							Main.dust[i].scale += 0.03f;
							Dust dust23 = Main.dust[i];
							dust23.velocity.X = dust23.velocity.X * 1.05f;
							Dust dust24 = Main.dust[i];
							dust24.velocity.Y = dust24.velocity.Y + 0.15f;
						}
					}
					if (Main.dust[i].type == 35 && Main.dust[i].noGravity)
					{
						Main.dust[i].scale += 0.03f;
						if (Main.dust[i].scale < 1f)
						{
							Dust dust25 = Main.dust[i];
							dust25.velocity.Y = dust25.velocity.Y + 0.075f;
						}
						Dust dust26 = Main.dust[i];
						dust26.velocity.X = dust26.velocity.X * 1.08f;
						if (Main.dust[i].velocity.X > 0f)
						{
							Main.dust[i].rotation += 0.01f;
						}
						else
						{
							Main.dust[i].rotation -= 0.01f;
						}
						float num15 = Main.dust[i].scale * 0.6f;
						if (num15 > 1f)
						{
							num15 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f + 1f), num15, num15 * 0.3f, num15 * 0.1f);
					}
					else if (Main.dust[i].type == 67)
					{
						float num16 = Main.dust[i].scale;
						if (num16 > 1f)
						{
							num16 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), 0f, num16 * 0.8f, num16);
					}
					else if (Main.dust[i].type == 34 || Main.dust[i].type == 35)
					{
						if (!Collision.WetCollision(new Vector2(Main.dust[i].position.X, Main.dust[i].position.Y - 8f), 4, 4))
						{
							Main.dust[i].scale = 0f;
						}
						else
						{
							Main.dust[i].alpha += Main.rand.Next(2);
							if (Main.dust[i].alpha > 255)
							{
								Main.dust[i].scale = 0f;
							}
							Main.dust[i].velocity.Y = -0.5f;
							if (Main.dust[i].type == 34)
							{
								Main.dust[i].scale += 0.005f;
							}
							else
							{
								Main.dust[i].alpha++;
								Main.dust[i].scale -= 0.01f;
								Main.dust[i].velocity.Y = -0.2f;
							}
							Dust dust27 = Main.dust[i];
							dust27.velocity.X = dust27.velocity.X + (float)Main.rand.Next(-10, 10) * 0.002f;
							if ((double)Main.dust[i].velocity.X < -0.25)
							{
								Main.dust[i].velocity.X = -0.25f;
							}
							if ((double)Main.dust[i].velocity.X > 0.25)
							{
								Main.dust[i].velocity.X = 0.25f;
							}
						}
						if (Main.dust[i].type == 35)
						{
							float num17 = Main.dust[i].scale * 0.3f + 0.4f;
							if (num17 > 1f)
							{
								num17 = 1f;
							}
							Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num17, num17 * 0.5f, num17 * 0.3f);
						}
					}
					if (Main.dust[i].type == 68)
					{
						float num18 = Main.dust[i].scale * 0.3f;
						if (num18 > 1f)
						{
							num18 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num18 * 0.1f, num18 * 0.2f, num18);
					}
					if (Main.dust[i].type == 70)
					{
						float num19 = Main.dust[i].scale * 0.3f;
						if (num19 > 1f)
						{
							num19 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num19 * 0.5f, 0f, num19);
					}
					if (Main.dust[i].type == 41)
					{
						Dust dust28 = Main.dust[i];
						dust28.velocity.X = dust28.velocity.X + (float)Main.rand.Next(-10, 11) * 0.01f;
						Dust dust29 = Main.dust[i];
						dust29.velocity.Y = dust29.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.01f;
						if ((double)Main.dust[i].velocity.X > 0.75)
						{
							Main.dust[i].velocity.X = 0.75f;
						}
						if ((double)Main.dust[i].velocity.X < -0.75)
						{
							Main.dust[i].velocity.X = -0.75f;
						}
						if ((double)Main.dust[i].velocity.Y > 0.75)
						{
							Main.dust[i].velocity.Y = 0.75f;
						}
						if ((double)Main.dust[i].velocity.Y < -0.75)
						{
							Main.dust[i].velocity.Y = -0.75f;
						}
						Main.dust[i].scale += 0.007f;
						float num20 = Main.dust[i].scale * 0.7f;
						if (num20 > 1f)
						{
							num20 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num20 * 0.4f, num20 * 0.9f, num20);
					}
					else if (Main.dust[i].type == 44)
					{
						Dust dust30 = Main.dust[i];
						dust30.velocity.X = dust30.velocity.X + (float)Main.rand.Next(-10, 11) * 0.003f;
						Dust dust31 = Main.dust[i];
						dust31.velocity.Y = dust31.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.003f;
						if ((double)Main.dust[i].velocity.X > 0.35)
						{
							Main.dust[i].velocity.X = 0.35f;
						}
						if ((double)Main.dust[i].velocity.X < -0.35)
						{
							Main.dust[i].velocity.X = -0.35f;
						}
						if ((double)Main.dust[i].velocity.Y > 0.35)
						{
							Main.dust[i].velocity.Y = 0.35f;
						}
						if ((double)Main.dust[i].velocity.Y < -0.35)
						{
							Main.dust[i].velocity.Y = -0.35f;
						}
						Main.dust[i].scale += 0.0085f;
						float num21 = Main.dust[i].scale * 0.7f;
						if (num21 > 1f)
						{
							num21 = 1f;
						}
						Lighting.addLight((int)(Main.dust[i].position.X / 16f), (int)(Main.dust[i].position.Y / 16f), num21 * 0.7f, num21, num21 * 0.8f);
					}
					else
					{
						Dust dust32 = Main.dust[i];
						dust32.velocity.X = dust32.velocity.X * 0.99f;
					}
					if (Main.dust[i].type != 79)
					{
						Main.dust[i].rotation += Main.dust[i].velocity.X * 0.5f;
					}
					if (Main.dust[i].fadeIn > 0f)
					{
						if (Main.dust[i].type == 46)
						{
							Main.dust[i].scale += 0.1f;
						}
						else
						{
							Main.dust[i].scale += 0.03f;
						}
						if (Main.dust[i].scale > Main.dust[i].fadeIn)
						{
							Main.dust[i].fadeIn = 0f;
						}
					}
					else
					{
						Main.dust[i].scale -= 0.01f;
					}
					if (Main.dust[i].noGravity)
					{
						Dust dust33 = Main.dust[i];
						dust33.velocity *= 0.92f;
						if (Main.dust[i].fadeIn == 0f)
						{
							Main.dust[i].scale -= 0.04f;
						}
					}
					if (Main.dust[i].position.Y > Main.screenPosition.Y + (float)Main.screenHeight)
					{
						Main.dust[i].active = false;
					}
					if ((double)Main.dust[i].scale < 0.1)
					{
						Main.dust[i].active = false;
					}
				}
				else
				{
					Main.dust[i].active = false;
				}
			}
		}

		public Color GetAlpha(Color newColor)
		{
			float num = (float)(255 - alpha) / 255f;
			if (type == 6 || type == 75 || type == 20 || type == 21)
			{
				return new Color(newColor.R, newColor.G, newColor.B, 25);
			}
			if ((type == 68 || type == 70) && noGravity)
			{
				return new Color(255, 255, 255, 0);
			}
			if (type == 15 || type == 20 || type == 21 || type == 29 || type == 35 || type == 41 || type == 44 || type == 27 || type == 45 || type == 55 || type == 56 || type == 57 || type == 58 || type == 73 || type == 74)
			{
				num = (num + 3f) / 4f;
			}
			else if (type == 43)
			{
				num = (num + 9f) / 10f;
			}
			else
			{
				if (type == 66)
				{
					return new Color(newColor.R, newColor.G, newColor.B, 0);
				}
				if (type == 71)
				{
					return new Color(200, 200, 200, 0);
				}
				if (type == 72)
				{
					return new Color(200, 200, 200, 200);
				}
			}
			int r = (int)((float)(int)newColor.R * num);
			int g = (int)((float)(int)newColor.G * num);
			int b = (int)((float)(int)newColor.B * num);
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
			int num = color.R - (255 - newColor.R);
			int num2 = color.G - (255 - newColor.G);
			int num3 = color.B - (255 - newColor.B);
			int num4 = color.A - (255 - newColor.A);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			return new Color(num, num2, num3, num4);
		}
	}
}
