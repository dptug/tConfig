using Gajatko.IniFiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SevenZip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace Terraria
{
	public class Menu
	{
		public static int MenuPosX;

		public static int MenuPosY;

		public static bool[] MenuActive;

		public static void DrawMenu(Main main)
		{
			//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
			//IL_1d5c: Expected O, but got Unknown
			if (Config.messages.Count > 0 && !Config.modsLoading)
			{
				int num = 0;
				foreach (string message in Config.messages)
				{
					main.spriteBatch.DrawString(Main.fontDeathText, message, new Vector2(30f, 100 + num * 30), new Color(255, 255, 255), 0f, default(Vector2), 0.5f, SpriteEffects.None, 1f);
					num++;
				}
				main.spriteBatch.DrawString(Main.fontDeathText, "Press any key to continue", new Vector2(250f, 100 + num * 30 + 60), new Color(255, 255, 255), 0f, default(Vector2), 0.6f, SpriteEffects.None, 1f);
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				if (pressedKeys.Length > 0)
				{
					Config.messages.Clear();
				}
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Config.messages.Clear();
				}
				if (Main.mouseRight && Main.mouseRightRelease)
				{
					Config.messages.Clear();
				}
				main.spriteBatch.End();
				return;
			}
			Main.render = false;
			Star.UpdateStars();
			Cloud.UpdateClouds();
			Main.holyTiles = 0;
			Main.evilTiles = 0;
			Main.jungleTiles = 0;
			Main.chatMode = false;
			for (int i = 0; i < Main.numChatLines; i++)
			{
				Main.chatLine[i] = new ChatLine();
			}
			main.DrawFPS();
			Main.screenLastPosition = Main.screenPosition;
			Main.screenPosition.Y = (float)(Main.worldSurface * 16.0 - (double)Main.screenHeight);
			if (Main.grabSky)
			{
				Main.screenPosition.X += (float)(Main.mouseX - Main.screenWidth / 2) * 0.02f;
			}
			else
			{
				Main.screenPosition.X += 2f;
			}
			if (Main.screenPosition.X > 2.14748352E+09f)
			{
				Main.screenPosition.X = 0f;
			}
			if (Main.screenPosition.X < -2.14748352E+09f)
			{
				Main.screenPosition.X = 0f;
			}
			Main.background = 0;
			byte b = (byte)((255 + Main.tileColor.R * 2) / 3);
			Color color = new Color(b, b, b, 255);
			main.logoRotation += main.logoRotationSpeed * 3E-05f;
			if ((double)main.logoRotation > 0.1)
			{
				main.logoRotationDirection = -1f;
			}
			else if ((double)main.logoRotation < -0.1)
			{
				main.logoRotationDirection = 1f;
			}
			if ((main.logoRotationSpeed < 20f) & (main.logoRotationDirection == 1f))
			{
				main.logoRotationSpeed += 1f;
			}
			else if ((main.logoRotationSpeed > -20f) & (main.logoRotationDirection == -1f))
			{
				main.logoRotationSpeed -= 1f;
			}
			main.logoScale += main.logoScaleSpeed * 1E-05f;
			if ((double)main.logoScale > 1.1)
			{
				main.logoScaleDirection = -1f;
			}
			else if ((double)main.logoScale < 0.9)
			{
				main.logoScaleDirection = 1f;
			}
			if ((main.logoScaleSpeed < 50f) & (main.logoScaleDirection == 1f))
			{
				main.logoScaleSpeed += 1f;
			}
			else if ((main.logoScaleSpeed > -50f) & (main.logoScaleDirection == -1f))
			{
				main.logoScaleSpeed -= 1f;
			}
			Color color2 = new Color((byte)((float)(int)color.R * ((float)Main.LogoA / 255f)), (byte)((float)(int)color.G * ((float)Main.LogoA / 255f)), (byte)((float)(int)color.B * ((float)Main.LogoA / 255f)), (byte)((float)(int)color.A * ((float)Main.LogoA / 255f)));
			Color color3 = new Color((byte)((float)(int)color.R * ((float)Main.LogoB / 255f)), (byte)((float)(int)color.G * ((float)Main.LogoB / 255f)), (byte)((float)(int)color.B * ((float)Main.LogoB / 255f)), (byte)((float)(int)color.A * ((float)Main.LogoB / 255f)));
			Main.LogoT = false;
			if (!Main.LogoT)
			{
				main.spriteBatch.Draw(Main.logoTexture, new Vector2(Main.screenWidth / 2, 100f), new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height), color2, main.logoRotation, new Vector2(Main.logoTexture.Width / 2, Main.logoTexture.Height / 2), main.logoScale, SpriteEffects.None, 0f);
			}
			else
			{
				main.spriteBatch.Draw(Main.logo3Texture, new Vector2(Main.screenWidth / 2, 100f), new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height), color2, main.logoRotation, new Vector2(Main.logoTexture.Width / 2, Main.logoTexture.Height / 2), main.logoScale, SpriteEffects.None, 0f);
			}
			main.spriteBatch.Draw(Main.logo2Texture, new Vector2(Main.screenWidth / 2, 100f), new Rectangle(0, 0, Main.logoTexture.Width, Main.logoTexture.Height), color3, main.logoRotation, new Vector2(Main.logoTexture.Width / 2, Main.logoTexture.Height / 2), main.logoScale, SpriteEffects.None, 0f);
			if (Main.dayTime)
			{
				Main.LogoA += 2;
				if (Main.LogoA > 255)
				{
					Main.LogoA = 255;
				}
				Main.LogoB--;
				if (Main.LogoB < 0)
				{
					Main.LogoB = 0;
				}
			}
			else
			{
				Main.LogoB += 2;
				if (Main.LogoB > 255)
				{
					Main.LogoB = 255;
				}
				Main.LogoA--;
				if (Main.LogoA < 0)
				{
					Main.LogoA = 0;
					Main.LogoT = true;
				}
			}
			MenuPosY = 200;
			MenuPosX = Main.screenWidth / 2;
			int num2 = 80;
			int num3 = 0;
			int menuMode = Main.menuMode;
			int num4 = -1;
			int num5 = 0;
			int num6 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num7 = 0;
			MenuActive = new bool[Main.maxMenuItems];
			bool[] array = new bool[Main.maxMenuItems];
			int[] array2 = new int[Main.maxMenuItems];
			int[] array3 = new int[Main.maxMenuItems];
			byte[] array4 = new byte[Main.maxMenuItems];
			float[] array5 = new float[Main.maxMenuItems];
			bool[] array6 = new bool[Main.maxMenuItems];
			for (int j = 0; j < Main.maxMenuItems; j++)
			{
				MenuActive[j] = false;
				array[j] = false;
				array2[j] = 0;
				array3[j] = 0;
				array5[j] = 1f;
			}
			string[] array7 = new string[Main.maxMenuItems];
			if (Main.menuMode == -1)
			{
				Main.menuMode = 0;
			}
			if (Main.menuMode == 1212)
			{
				if (main.focusMenu == 2)
				{
					array7[0] = "Wählen Sie die Sprache";
				}
				else if (main.focusMenu == 3)
				{
					array7[0] = "Selezionare la lingua";
				}
				else if (main.focusMenu == 4)
				{
					array7[0] = "Sélectionnez la langue";
				}
				else if (main.focusMenu == 5)
				{
					array7[0] = "Seleccione el idioma";
				}
				else
				{
					array7[0] = "Select language";
				}
				num2 = 50;
				MenuPosY = 200;
				array2[1] = 25;
				array2[2] = 25;
				array2[3] = 25;
				array2[4] = 25;
				array2[5] = 25;
				MenuActive[0] = true;
				array7[1] = "English";
				array7[2] = "Deutsch";
				array7[3] = "Italiano";
				array7[4] = "Française";
				array7[5] = "Español";
				num3 = 6;
				if (main.selectedMenu >= 1)
				{
					Lang.lang = main.selectedMenu;
					Lang.setLang();
					Main.menuMode = 0;
					Main.PlaySound(10);
					main.SaveSettings();
				}
			}
			else if (Main.menuMode == 1213)
			{
				if (main.focusMenu == 1)
				{
					array7[0] = "Select language";
				}
				else if (main.focusMenu == 2)
				{
					array7[0] = "Wählen Sie die Sprache";
				}
				else if (main.focusMenu == 3)
				{
					array7[0] = "Selezionare la lingua";
				}
				else if (main.focusMenu == 4)
				{
					array7[0] = "Sélectionnez la langue";
				}
				else if (main.focusMenu == 5)
				{
					array7[0] = "Seleccione el idioma";
				}
				else
				{
					array7[0] = Lang.menu[102];
				}
				num2 = 48;
				MenuPosY = 180;
				array2[1] = 25;
				array2[2] = 25;
				array2[3] = 25;
				array2[4] = 25;
				array2[5] = 25;
				array2[6] = 50;
				MenuActive[0] = true;
				array7[1] = "English";
				array7[2] = "Deutsch";
				array7[3] = "Italiano";
				array7[4] = "Française";
				array7[5] = "Español";
				array7[6] = Lang.menu[5];
				num3 = 7;
				if (main.selectedMenu == 6)
				{
					Main.menuMode = 11;
					Main.PlaySound(11);
				}
				else if (main.selectedMenu >= 1)
				{
					Lang.lang = main.selectedMenu;
					Lang.setLang();
					Main.PlaySound(12);
					main.SaveSettings();
				}
			}
			else if (Main.netMode == 2)
			{
				bool flag4 = true;
				for (int k = 0; k < 8; k++)
				{
					if (k < 255)
					{
						try
						{
							array7[k] = Netplay.serverSock[k].statusText;
							if (Netplay.serverSock[k].active && Main.showSpam)
							{
								string[] array8;
								string[] array9 = array8 = array7;
								IntPtr value;
								int num8 = (int)(value = (IntPtr)k);
								object obj = array8[(int)value];
								array9[num8] = string.Concat(obj, " (", NetMessage.buffer[k].spamCount, ")");
							}
						}
						catch
						{
							array7[k] = "";
						}
						MenuActive[k] = true;
						if (array7[k] != "" && array7[k] != null)
						{
							flag4 = false;
						}
					}
				}
				if (flag4)
				{
					array7[0] = Lang.menu[0];
					array7[1] = Lang.menu[1] + Netplay.serverPort + ".";
				}
				num3 = 11;
				array7[9] = Main.statusText;
				MenuActive[9] = true;
				MenuPosY = 170;
				num2 = 30;
				array2[10] = 20;
				array2[10] = 40;
				array7[10] = Lang.menu[2];
				if (main.selectedMenu == 10)
				{
					Netplay.disconnect = true;
					Main.PlaySound(11);
				}
			}
			else if (Main.menuMode == 31)
			{
				string password = Netplay.password;
				Netplay.password = Main.GetInputText(Netplay.password);
				if (password != Netplay.password)
				{
					Main.PlaySound(12);
				}
				array7[0] = Lang.menu[3];
				main.textBlinkerCount++;
				if (main.textBlinkerCount >= 20)
				{
					main.textBlinkerState = ((main.textBlinkerState == 0) ? 1 : 0);
					main.textBlinkerCount = 0;
				}
				array7[1] = Netplay.password;
				if (main.textBlinkerState == 1)
				{
					string[] array10;
					(array10 = array7)[1] = array10[1] + "|";
					array3[1] = 1;
				}
				else
				{
					string[] array11;
					(array11 = array7)[1] = array11[1] + " ";
				}
				MenuActive[0] = true;
				MenuActive[1] = true;
				array2[1] = -20;
				array2[2] = 20;
				array7[2] = Lang.menu[4];
				array7[3] = Lang.menu[5];
				num3 = 4;
				if (main.selectedMenu == 3)
				{
					Main.PlaySound(11);
					Main.menuMode = 0;
					Netplay.disconnect = true;
					Netplay.password = "";
				}
				else if (main.selectedMenu == 2 || Main.inputTextEnter)
				{
					NetMessage.SendData(38, -1, -1, Netplay.password);
					Main.menuMode = 14;
				}
			}
			else if (Main.netMode == 1 || Main.menuMode == 14)
			{
				num3 = 2;
				array7[0] = Main.statusText;
				MenuActive[0] = true;
				MenuPosY = 300;
				array7[1] = Lang.menu[6];
				if (main.selectedMenu == 1)
				{
					Netplay.disconnect = true;
					Netplay.clientSock.tcpClient.Close();
					Main.PlaySound(11);
					Main.menuMode = 0;
					Main.netMode = 0;
					try
					{
						main.tServer.Kill();
					}
					catch
					{
					}
				}
			}
			else
			{
				switch (Main.menuMode)
				{
				case 30:
				{
					string password2 = Netplay.password;
					Netplay.password = Main.GetInputText(Netplay.password);
					if (password2 != Netplay.password)
					{
						Main.PlaySound(12);
					}
					array7[0] = Lang.menu[7];
					main.textBlinkerCount++;
					if (main.textBlinkerCount >= 20)
					{
						if (main.textBlinkerState == 0)
						{
							main.textBlinkerState = 1;
						}
						else
						{
							main.textBlinkerState = 0;
						}
						main.textBlinkerCount = 0;
					}
					array7[1] = Netplay.password;
					if (main.textBlinkerState == 1)
					{
						string[] array34;
						(array34 = array7)[1] = array34[1] + "|";
						array3[1] = 1;
					}
					else
					{
						string[] array35;
						(array35 = array7)[1] = array35[1] + " ";
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[1] = -20;
					array2[2] = 20;
					array7[2] = Lang.menu[4];
					array7[3] = Lang.menu[5];
					num3 = 4;
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 6;
						Netplay.password = "";
					}
					else if (main.selectedMenu == 2 || Main.inputTextEnter || Main.autoPass)
					{
						main.tServer.StartInfo.FileName = "tConfigServer.exe";
						main.tServer.StartInfo.Arguments = "-autoshutdown -world \"" + Main.worldPathName + "\" -password \"" + Netplay.password + "\" -lang " + Lang.lang;
						if (Main.libPath != "")
						{
							ProcessStartInfo startInfo = main.tServer.StartInfo;
							startInfo.Arguments = startInfo.Arguments + " -loadlib " + Main.libPath;
						}
						main.tServer.StartInfo.UseShellExecute = false;
						main.tServer.StartInfo.CreateNoWindow = true;
						main.tServer.Start();
						Netplay.SetIP("127.0.0.1");
						Main.autoPass = true;
						Main.statusText = Lang.menu[8];
						Netplay.StartClient();
						Main.menuMode = 10;
					}
					break;
				}
				case 15:
					num3 = 2;
					array7[0] = Main.statusText;
					MenuActive[0] = true;
					MenuPosY = 80;
					num2 = 400;
					array7[1] = Lang.menu[5];
					if (main.selectedMenu == 1)
					{
						Netplay.disconnect = true;
						Main.PlaySound(11);
						Main.menuMode = 0;
						Main.netMode = 0;
					}
					break;
				case 200:
				case 201:
					num3 = 2;
					array7[0] = Main.loadFailedMessage;
					MenuActive[0] = true;
					array5[0] = 0.7f;
					MenuPosY -= 30;
					array2[1] = 50;
					array7[1] = Lang.menu[5];
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
						Main.netMode = 0;
					}
					break;
				case 203:
					num3 = 3;
					array7[0] = Main.loadFailedMessage;
					MenuActive[0] = true;
					array5[0] = 0.7f;
					MenuPosY -= 30;
					array2[1] = 40;
					array7[1] = "Lose it & load anyways";
					array2[2] = 20;
					array7[2] = "Stop loading world";
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(11);
						Main.errorHandleChoice = 1;
					}
					if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.errorHandleChoice = 2;
					}
					break;
				case 10:
					num3 = 1;
					array7[0] = Main.statusText;
					MenuActive[0] = true;
					MenuPosY = 300;
					break;
				case 100:
					num3 = 1;
					array7[0] = Main.statusText;
					MenuActive[0] = true;
					MenuPosY = 300;
					break;
				case 10000:
				{
					num2 = 40;
					MenuPosY = 180;
					array2[7] = 10;
					if (Main.statusTextSB.Length > 2000)
					{
						Main.statusTextSB.Remove(0, Main.statusTextSB.Length - 1500);
					}
					string text7 = Main.statusTextSB.ToString();
					if (text7 != "")
					{
						string[] collection = text7.Split('\n');
						List<string> list = new List<string>(collection);
						num3 = 10;
						for (int num21 = 1; num21 < num3 && num21 < list.Count; num21++)
						{
							array7[num3 - num21] = list[list.Count - num21];
							MenuActive[num3 - num21] = true;
							array5[num3 - num21] = 0.8f;
						}
					}
					else
					{
						array7[0] = "Loading...";
						MenuActive[0] = true;
					}
					break;
				}
				case 0:
					Main.menuMultiplayer = false;
					Main.menuServer = false;
					Main.netMode = 0;
					array7[0] = Lang.menu[12];
					array7[1] = "tConfig " + Lang.menu[13];
					array7[2] = Lang.menu[14];
					array7[3] = "tConfig " + Lang.menu[14];
					array7[4] = "Mod Config";
					array7[5] = "Rebuild Mods";
					array7[6] = "Check for Updates";
					if (Config.updatesAvailable)
					{
						array7[6] = "Updates Available!";
					}
					array7[7] = Lang.menu[15];
					num2 = 50;
					num3 = 8;
					if (main.selectedMenu == 6)
					{
						if (!Config.updatesAvailable)
						{
							Config.CheckObjUpdates();
						}
						Main.PlaySound(10);
						Main.menuMode = 1400;
					}
					if (main.selectedMenu == 7)
					{
						main.QuitGame();
					}
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(10);
						Main.menuMode = 12;
					}
					if (main.selectedMenu == 2)
					{
						Main.PlaySound(10);
						Main.menuMode = 11;
					}
					if (main.selectedMenu == 0)
					{
						Main.PlaySound(10);
						Main.menuMode = 1;
						Main.LoadPlayers();
					}
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(10);
						Main.menuMode = 1337;
					}
					if (main.selectedMenu == 4)
					{
						Main.PlaySound(10);
						Main.menuMode = 1500;
						Config.tConfigMenuStart = 0;
					}
					if (main.selectedMenu == 5)
					{
						Main.PlaySound(10);
						Main.menuMode = 1600;
					}
					break;
				case 1500:
				{
					MenuPosY = 180;
					num2 = 35;
					num3 = 8;
					string[] array32 = array7;
					float[] array33 = array5;
					int num31 = 0;
					int num32 = 8;
					int num33 = Config.tConfigMenuStart * num32;
					int num34 = num33 + num32 - 1;
					int num35 = 0;
					int num36 = 0;
					bool flag9 = true;
					foreach (string mod in Config.mods)
					{
						if (Config.jsonCurrent[mod] != null)
						{
							num36++;
							if (flag9)
							{
								array32[num31] = mod;
								array33[num31] = 0.7f;
								num31++;
								num35++;
								if (num35 == num32)
								{
									flag9 = false;
								}
							}
						}
					}
					if (num33 > 0)
					{
						array32[num31] = "<- Previous";
						array33[num31] = 0.7f;
						num31++;
					}
					if (num34 < num36 - 1)
					{
						array32[num31] = "Next ->";
						array33[num31] = 0.7f;
						num31++;
					}
					array32[num31] = "Back";
					array33[num31] = 0.85f;
					num31++;
					if (main.selectedMenu >= 0)
					{
						if (array32[main.selectedMenu] == "Back")
						{
							Main.PlaySound(11);
							Main.menuMode = 0;
						}
						else
						{
							Main.PlaySound(10);
							Main.menuMode = 1501;
							Settings.current = array32[main.selectedMenu];
						}
					}
					break;
				}
				case 1501:
				{
					int num51 = 700;
					int x = (Main.screenWidth - num51) / 2;
					int num52 = 200;
					int num53 = 60;
					float num54 = 0.5f;
					Config.mainInstance.spriteBatch.End();
					Config.mainInstance.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
					int num55 = 0;
					{
						IEnumerator enumerator = ((IEnumerable)Config.jsonCurrent[Settings.current]["settings"]).GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								Settings.draw(setting: ((KeyValuePair<string, JsonData>)enumerator.Current).Value, x: x, y: (int)((float)num52 + (float)(num53 * num55++) * num54), w: num51, scale: num54);
							}
						}
						finally
						{
							IDisposable disposable = enumerator as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
					bool flag10 = Settings.drawBackButton(x, (int)((float)(Main.screenHeight - 60) - (float)num53 * num54), num51, num54 * 1.5f);
					Config.mainInstance.spriteBatch.End();
					Config.mainInstance.spriteBatch.Begin();
					if (flag10)
					{
						Main.PlaySound(11);
						Main.menuMode = 1500;
						Config.SaveModSettings(Settings.current);
					}
					break;
				}
				case 1502:
				case 1503:
				case 1504:
				{
					if (Settings.currentInput == null)
					{
						if (Main.menuMode == 1502)
						{
							Settings.currentInput = string.Concat((int)Settings.currentSetting["value"]);
						}
						else if (Main.menuMode == 1503 || Main.menuMode == 1504)
						{
							Settings.currentInput = (string)Settings.currentSetting["value"];
						}
					}
					string text2 = Settings.currentInput;
					if (Main.menuMode == 1504)
					{
						array7[1] = text2;
					}
					else
					{
						text2 = Main.GetInputText(text2).Trim();
						if (text2 != Settings.currentInput)
						{
							Main.PlaySound(12);
						}
						Settings.currentInput = text2;
						main.textBlinkerCount++;
						if (main.textBlinkerCount >= 20)
						{
							main.textBlinkerState = ((main.textBlinkerState == 0) ? 1 : 0);
							main.textBlinkerCount = 0;
						}
						array7[1] = text2;
						if (main.textBlinkerState == 1)
						{
							string[] array12;
							(array12 = array7)[1] = array12[1] + "|";
							array3[1] = 1;
						}
						else
						{
							string[] array13;
							(array13 = array7)[1] = array13[1] + " ";
						}
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[1] = -20;
					array2[2] = 20;
					if (Main.menuMode == 1502)
					{
						array7[0] = (string)Settings.currentSetting["display"] + " (" + (int)Settings.currentSetting["rangeMin"] + " - " + (int)Settings.currentSetting["rangeMax"] + "):";
					}
					else if (Main.menuMode == 1503 || Main.menuMode == 1504)
					{
						array7[0] = (string)Settings.currentSetting["display"] + ":";
					}
					array7[3] = Lang.menu[5];
					if (Main.menuMode != 1504)
					{
						array7[2] = Lang.menu[4];
					}
					num3 = 4;
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 1501;
					}
					if (Main.menuMode == 1504)
					{
						Keys[] pressedKeys2 = Main.keyState.GetPressedKeys();
						if (pressedKeys2.Length <= 0)
						{
							break;
						}
						string text3 = string.Concat(pressedKeys2[0]);
						if (text3 != "None")
						{
							if (text3 == "Escape")
							{
								Main.PlaySound(10);
								Main.menuMode = 1501;
								break;
							}
							Settings.currentSetting["value"] = text3;
							Main.PlaySound(10);
							Main.menuMode = 1501;
							Config.TrackConsole("asdf", text3, 120);
						}
					}
					else if (main.selectedMenu == 2 || (!array[2] && Main.inputTextEnter))
					{
						if (Main.menuMode == 1502)
						{
							try
							{
								int val = int.Parse(text2);
								val = Math.Min(Math.Max(val, (int)Settings.currentSetting["rangeMin"]), (int)Settings.currentSetting["rangeMax"]);
								Settings.currentSetting["value"] = val;
							}
							catch (Exception)
							{
							}
						}
						else if (Main.menuMode == 1503)
						{
							Settings.currentSetting["value"] = text2;
						}
						Main.PlaySound(10);
						Main.menuMode = 1501;
					}
					break;
				}
				case 1337:
				{
					int[] array25 = array2;
					int[] array26 = array3;
					float[] array27 = array5;
					MenuPosY = 180;
					num2 = 35;
					array25[7] = 10;
					num3 = 8;
					int num22 = 0;
					IniFile settings2 = Config.settings;
					float num23 = 0.7f;
					int num24 = 8;
					if (settings2["ModPacks"] != null)
					{
						ArrayList arrayList2 = new ArrayList(settings2["ModPacks"].GetKeys());
						int tConfigMenuStart2 = Config.tConfigMenuStart;
						for (int num25 = tConfigMenuStart2; num25 < arrayList2.Count; num25++)
						{
							string text8 = (string)arrayList2[num25];
							string text9 = text8;
							if (main.selectedMenu == num22)
							{
								if (settings2["ModPacks"][text8] != "False")
								{
									if (Config.tConfigModTypes.ContainsKey(text9) && Config.tConfigModTypes[text9].Count > 0)
									{
										Config.tConfigModChoices[text9]++;
										Config.tConfigModChoices[text9] = Config.tConfigModChoices[text9] % (Config.tConfigModTypes[text9].Count + 1);
										if (Config.tConfigModChoices[text9] == 0)
										{
											settings2["ModPacks"][text8] = "False";
										}
										else
										{
											settings2["ModPacks"][text8] = Config.tConfigModTypes[text9][Config.tConfigModChoices[text9] - 1];
										}
									}
									else
									{
										settings2["ModPacks"][text8] = "False";
									}
								}
								else
								{
									if (!Config.tConfigModTypes.ContainsKey(text9))
									{
										try
										{
											string text10 = Path.Combine(Config.tConfigFolder, "ModPacks", text8 + ".obj");
											SevenZipExtractor val2 = (SevenZipExtractor)(object)new SevenZipExtractor(text10);
											MemoryStream memoryStream = new MemoryStream();
											val2.ExtractFile("Config.ini", (Stream)memoryStream);
											val2.Dispose();
											memoryStream.Position = 0L;
											IniFileReader reader = new IniFileReader(memoryStream);
											IniFile value2 = IniFile.FromStream(reader);
											Config.ModSetting[text9] = value2;
											Config.tConfigModTypes[text9] = new List<string>(Config.ModSetting[text9]["Choice"].GetKeys());
											int num26 = Config.tConfigModTypes[text9].IndexOf(Config.settings["ModPacks"][text8]);
											Config.tConfigModChoices[text9] = num26 + 1;
										}
										catch (Exception)
										{
											Config.tConfigModTypes[text9] = new List<string>();
										}
									}
									if (Config.tConfigModTypes.ContainsKey(text9) && Config.tConfigModTypes[text9].Count > 0)
									{
										Config.tConfigModChoices[text9]++;
										Config.tConfigModChoices[text9] = Config.tConfigModChoices[text9] % (Config.tConfigModTypes[text9].Count + 1);
										if (Config.tConfigModChoices[text9] == 0)
										{
											settings2["ModPacks"][text8] = "False";
										}
										else
										{
											settings2["ModPacks"][text8] = Config.tConfigModTypes[text9][Config.tConfigModChoices[text9] - 1];
										}
									}
									else
									{
										settings2["ModPacks"][text8] = "True";
									}
								}
								Config.modSettingChanged = true;
							}
							if (settings2["ModPacks"][text8] != "False")
							{
								array7[num22] = text8 + ": ";
								if (settings2["ModPacks"][text8] != "True")
								{
									new List<string>(Config.ModSetting[text9]["Choice"].GetKeys());
									string[] array28;
									string[] array29 = array28 = array7;
									int num27 = num22;
									IntPtr intPtr = (IntPtr)num27;
									array29[num27] = array28[(long)intPtr] + Config.ModSetting[text9]["Choice"][settings2["ModPacks"][text8]];
								}
								else
								{
									string[] array28;
									string[] array30 = array28 = array7;
									int num28 = num22;
									IntPtr intPtr = (IntPtr)num28;
									array30[num28] = array28[(long)intPtr] + "On";
								}
							}
							else
							{
								array7[num22] = text8 + ": Off";
							}
							array27[num22] = num23;
							array26[num22] = 0;
							array25[num22] = 0;
							num22++;
							if (Config.tConfigMenuStart > 0 && (num25 + 1 == arrayList2.Count || num22 >= num24))
							{
								for (; num22 < num24; num22++)
								{
								}
								array7[num22] = "<- Previous";
								array27[num22] = num23;
								if (main.selectedMenu == num22)
								{
									Config.tConfigMenuStart -= num24;
								}
								num22++;
							}
							if (num22 >= num24 && num25 + 1 < arrayList2.Count)
							{
								for (; num22 < num24 + 1; num22++)
								{
								}
								array27[num22] = num23;
								array7[num22] = "Next ->";
								if (main.selectedMenu == num22)
								{
									Config.tConfigMenuStart += num24;
								}
								num22++;
								break;
							}
						}
						if (tConfigMenuStart2 > 0 && tConfigMenuStart2 == arrayList2.Count)
						{
							Config.tConfigMenuStart -= num24;
						}
					}
					for (; num22 < num24 + 2; num22++)
					{
					}
					num3 = num22 + 1;
					array7[num22] = "Back";
					array27[num22] = num23;
					num3++;
					array7[num22] = "Reload Mods";
					if (Config.modSettingChanged)
					{
						string[] array28;
						string[] array31 = array28 = array7;
						int num29 = num22;
						IntPtr intPtr = (IntPtr)num29;
						array31[num29] = array28[(long)intPtr] + " *";
					}
					array27[num22] = num23;
					array7[num22 + 1] = "Cancel";
					array27[num22 + 1] = num23;
					if (main.selectedMenu == num22)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
						if (Config.loadedServerSettings)
						{
							settings2.Save(Main.SavePath + "\\Config Server Mod.ini");
						}
						else
						{
							settings2.Save(Main.SavePath + "\\Config Mod.ini");
						}
						Config.modSettingChanged = false;
						Config.ReInitialize();
					}
					if (main.selectedMenu == num22 + 1)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
						Config.modSettingChanged = false;
						Config.settings = null;
						Config.loadedServerSettings = false;
						if (Main.dedServ && File.Exists(Main.SavePath + "\\Config Server Mod.ini"))
						{
							Config.settings = IniFile.FromFile(Main.SavePath + "\\Config Server Mod.ini");
							Config.loadedServerSettings = true;
						}
						if (Config.settings == null)
						{
							Config.settings = IniFile.FromFile(Main.SavePath + "\\Config Mod.ini");
						}
						List<string> list2 = new List<string>(Config.tConfigModChoices.Keys);
						foreach (string item in list2)
						{
							int num30 = Config.tConfigModTypes[item].IndexOf(Config.settings["ModPacks"][item]);
							Config.tConfigModChoices[item] = num30 + 1;
						}
					}
					break;
				}
				case 1600:
				{
					int[] array22 = array2;
					int[] array23 = array3;
					float[] array24 = array5;
					MenuPosY = 180;
					num2 = 35;
					array22[7] = 10;
					num3 = 8;
					int num16 = 0;
					IniFile settings = Config.settings;
					float num17 = 0.7f;
					int num18 = 8;
					if (settings["ModPacks"] != null)
					{
						ArrayList arrayList = new ArrayList(settings["ModPacks"].GetKeys());
						int tConfigMenuStart = Config.tConfigMenuStart;
						for (int num19 = tConfigMenuStart; num19 < arrayList.Count; num19++)
						{
							string text6 = (string)arrayList[num19];
							if (main.selectedMenu == num16)
							{
								Config.Rebuild(text6);
							}
							array7[num16] = text6;
							array24[num16] = num17;
							array23[num16] = 0;
							array22[num16] = 0;
							num16++;
							if (Config.tConfigMenuStart > 0 && (num19 + 1 == arrayList.Count || num16 >= num18))
							{
								for (; num16 < num18; num16++)
								{
								}
								array7[num16] = "<- Previous";
								array24[num16] = num17;
								if (main.selectedMenu == num16)
								{
									Config.tConfigMenuStart -= num18;
								}
								num16++;
							}
							if (num16 >= num18 && num19 + 1 < arrayList.Count)
							{
								for (; num16 < num18 + 1; num16++)
								{
								}
								array24[num16] = num17;
								array7[num16] = "Next ->";
								if (main.selectedMenu == num16)
								{
									Config.tConfigMenuStart += num18;
								}
								num16++;
								break;
							}
						}
					}
					for (; num16 < num18 + 2; num16++)
					{
					}
					num3 = num16 + 1;
					array7[num16] = "Back";
					array24[num16] = num17;
					if (main.selectedMenu == num16)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
					}
					break;
				}
				case 1400:
				{
					int[] array14 = array2;
					int[] array15 = array3;
					float[] array16 = array5;
					MenuPosY = 180;
					num2 = 35;
					array14[7] = 10;
					num3 = 8;
					int l = 0;
					float num9 = 0.7f;
					int num10 = 8;
					WebClient webClient = new WebClient();
					int num11 = 0;
					foreach (string key in Config.modObjURL.Keys)
					{
						_ = Config.modObjURL[key];
						string str = Config.modNewVersion[key];
						string text4 = key;
						if (main.selectedMenu == l)
						{
							Config.downloadModUpdate[text4] = !Config.downloadModUpdate[text4];
						}
						if (Config.downloadModUpdate[text4])
						{
							array7[l] = "Download " + text4 + " v" + str;
						}
						else
						{
							array7[l] = "Don't Download " + text4 + " v" + str;
						}
						array16[l] = num9;
						array15[l] = 0;
						array14[l] = 0;
						l++;
						if (Config.tConfigUpdateMenuStart > 0 && (num11 + 1 == Config.modObjURL.Count || l >= num10))
						{
							for (; l < num10; l++)
							{
							}
							array7[l] = "<- Previous";
							array16[l] = num9;
							if (main.selectedMenu == l)
							{
								Config.tConfigUpdateMenuStart -= num10;
							}
							l++;
						}
						if (l >= num10 && num11 + 1 < Config.modObjURL.Count)
						{
							for (; l < num10 + 1; l++)
							{
							}
							array16[l] = num9;
							array7[l] = "Next ->";
							if (main.selectedMenu == l)
							{
								Config.tConfigUpdateMenuStart += num10;
							}
							l++;
							break;
						}
						num11++;
					}
					for (; l < num10 + 2; l++)
					{
					}
					num3 = l + 1;
					num3++;
					array7[l] = "Download Updates & Reload Mods";
					array16[l] = num9;
					array7[l + 1] = "Cancel";
					array16[l + 1] = num9;
					if (main.selectedMenu == l)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
						bool flag5 = false;
						bool flag6 = false;
						foreach (string key2 in Config.downloadModUpdate.Keys)
						{
							if (key2 == "tConfig" && Config.downloadModUpdate[key2])
							{
								string address = Config.modObjURL[key2];
								string str2 = "";
								try
								{
									str2 = key2 + ".patch";
									webClient.DownloadFile(address, key2 + ".patch");
									str2 = "ModPack Builder.exe";
									webClient.DownloadFile("http://content.wuala.com/contents/Surfpup/Documents/Projects/Terraria/Release/ModPacks/tConfig%20Update/ModPack%20Builder.exe?dl=1", "tempModPack Builder.exe");
									str2 = "tConfig.gli";
									webClient.DownloadFile("http://content.wuala.com/contents/Surfpup/Documents/Projects/Terraria/Release/ModPacks/tConfig%20Update/tConfig.gli?dl=1", "temptConfig.gli");
									flag6 = true;
								}
								catch (Exception ex2)
								{
									Config.messages.Add("Error downloading update for " + key2);
									Config.messages.Add("    File failed: " + str2);
									Config.messages.Add("   " + ex2.Message);
									flag6 = false;
								}
							}
							else if (Config.downloadModUpdate[key2])
							{
								string address2 = Config.modObjURL[key2];
								bool flag7 = false;
								try
								{
									webClient.DownloadFile(address2, Path.Combine(Config.tConfigFolder, "ModPacks", key2 + ".obj"));
									flag7 = true;
								}
								catch (Exception ex3)
								{
									Config.messages.Add("Error downloading update for " + key2);
									Config.messages.Add("   " + ex3.Message);
									flag7 = false;
								}
								flag5 = (flag5 || flag7);
							}
						}
						if (flag6)
						{
							File.Copy("tempModPack Builder.exe", "ModPack Builder.exe", overwrite: true);
							File.Copy("temptConfig.gli", "tConfig.gli", overwrite: true);
							File.Delete("tempModPack Builder.exe");
							File.Delete("temptConfig.gli");
							if (File.Exists("tConfig_old.exe"))
							{
								File.Delete("tConfig_old.exe");
							}
							File.Move("tConfig.exe", "tConfig_old.exe");
							Process process = new Process();
							process.StartInfo.FileName = "bspatch.exe";
							process.StartInfo.CreateNoWindow = true;
							process.StartInfo.Arguments = "TerrariaOriginalBackup.exe tConfig.exe tConfig.patch";
							process.Start();
							process.WaitForExit();
							Main.menuMode = 0;
							main.QuitGame();
							process = new Process();
							process.StartInfo.FileName = "tConfig.exe";
							process.Start();
						}
						else if (flag5)
						{
							Config.ReInitialize();
						}
					}
					if (main.selectedMenu == l + 1)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
					}
					break;
				}
				case 1:
				case 4:
				{
					int[] array17 = array2;
					int[] array18 = array3;
					float[] array19 = array5;
					MenuPosY = 180;
					num2 = 35;
					array17[7] = 10;
					num3 = 8;
					int m = 0;
					float num12 = 0.7f;
					int num13 = 7;
					if (Main.numLoadPlayers == 0)
					{
						num13--;
					}
					int tConfigPlayerMenuStart = Config.tConfigPlayerMenuStart;
					for (int n = tConfigPlayerMenuStart; n < Main.numLoadPlayers; n++)
					{
						array7[m] = Main.loadPlayerName[n];
						array4[m] = Main.loadPlayer[n].difficulty;
						array19[m] = num12;
						array18[m] = 0;
						array17[m] = 0;
						if (main.selectedMenu == m)
						{
							if (Main.menuMode == 1)
							{
								if (Main.menuMultiplayer)
								{
									main.selectedPlayer = n;
									Main.playerPathName = Main.loadPlayerPath[main.selectedPlayer];
									Main.player[Main.myPlayer] = Player.LoadPlayer(Main.playerPathName);
									Main.PlaySound(10);
									if (Main.autoJoin)
									{
										if (Netplay.SetIP(Main.getIP))
										{
											Main.menuMode = 10;
											Netplay.StartClient();
										}
										else if (Netplay.SetIP2(Main.getIP))
										{
											Main.menuMode = 10;
											Netplay.StartClient();
										}
										Main.autoJoin = false;
									}
									else if (Main.menuServer)
									{
										Main.LoadWorlds();
										Main.menuMode = 6;
									}
									else
									{
										Main.menuMode = 13;
										Main.clrInput();
									}
								}
								else
								{
									Main.myPlayer = 0;
									main.selectedPlayer = n;
									Main.playerPathName = Main.loadPlayerPath[main.selectedPlayer];
									Main.player[Main.myPlayer] = Player.LoadPlayer(Main.playerPathName);
									Main.LoadWorlds();
									Main.PlaySound(10);
									Main.menuMode = 6;
								}
							}
							else if (Main.menuMode == 4)
							{
								main.selectedPlayer = n;
								Main.PlaySound(10);
								Main.menuMode = 5;
							}
						}
						m++;
						if (Config.tConfigPlayerMenuStart > 0 && (n + 1 == Main.numLoadPlayers || m >= num13))
						{
							for (; m < num13; m++)
							{
							}
							array7[m] = "<- Previous";
							array19[m] = num12;
							if (main.selectedMenu == m)
							{
								Config.tConfigPlayerMenuStart -= num13;
							}
							m++;
						}
						if (m >= num13 && n + 1 < Main.numLoadPlayers)
						{
							for (; m < num13 + 1; m++)
							{
							}
							array19[m] = num12;
							array7[m] = "Next ->";
							if (main.selectedMenu == m)
							{
								Config.tConfigPlayerMenuStart += num13;
							}
							m++;
							break;
						}
					}
					if (tConfigPlayerMenuStart == Main.numLoadPlayers && Config.tConfigPlayerMenuStart > 0)
					{
						Config.tConfigPlayerMenuStart -= num13;
					}
					for (; m < num13 + 2; m++)
					{
					}
					num3 = m + 1;
					if (Main.menuMode == 1)
					{
						array7[m] = Lang.menu[16];
						array7[m + 1] = Lang.menu[17];
						array19[m] = num12;
						array19[m + 1] = num12;
						if (main.selectedMenu == m)
						{
							Main.creatingChar = true;
							Main.loadPlayer[Main.numLoadPlayers] = new Player(modsLoaded: true);
							Main.loadPlayer[Main.numLoadPlayers].inventory[0].SetDefaults("Copper Shortsword");
							Main.loadPlayer[Main.numLoadPlayers].inventory[0].Prefix(-1);
							Main.loadPlayer[Main.numLoadPlayers].inventory[1].SetDefaults("Copper Pickaxe");
							Main.loadPlayer[Main.numLoadPlayers].inventory[1].Prefix(-1);
							Main.loadPlayer[Main.numLoadPlayers].inventory[2].SetDefaults("Copper Axe");
							Main.loadPlayer[Main.numLoadPlayers].inventory[2].Prefix(-1);
							Config.InitializeGlobalMod("ModPlayer");
							Codable.RunGlobalMethod("ModPlayer", "CreatePlayer", Main.loadPlayer[Main.numLoadPlayers]);
							Main.creatingChar = false;
							Main.PlaySound(10);
							Main.menuMode = 2;
						}
						else if (main.selectedMenu == m + 1)
						{
							Main.PlaySound(10);
							Main.menuMode = 4;
						}
						if (Main.numLoadPlayers == 0)
						{
							array7[m + 2] = "Copy Over Save Files";
							array19[m + 2] = num12;
							if (main.selectedMenu == m + 2)
							{
								Main.PlaySound(10);
								string path = Main.SavePath + Path.DirectorySeparatorChar + "Players";
								string[] files = Directory.GetFiles(path, "*.plr");
								foreach (string text5 in files)
								{
									try
									{
										File.Copy(text5, Main.PlayerPath + Path.DirectorySeparatorChar + Path.GetFileName(text5), overwrite: false);
									}
									catch
									{
									}
								}
								Main.LoadPlayers();
							}
							m++;
							num3++;
						}
						m += 2;
						num3 += 2;
					}
					array7[m] = Lang.menu[5];
					array19[m] = num12;
					if (main.selectedMenu == m)
					{
						Main.autoJoin = false;
						Main.autoPass = false;
						Main.PlaySound(11);
						if (Main.menuMultiplayer)
						{
							Main.menuMode = 12;
							Main.menuMultiplayer = false;
							Main.menuServer = false;
						}
						else
						{
							Main.menuMode = 0;
						}
					}
					break;
				}
				case 2:
				{
					if (main.selectedMenu == 0)
					{
						Main.menuMode = 17;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].hairColor;
					}
					if (main.selectedMenu == 1)
					{
						Main.menuMode = 18;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].eyeColor;
					}
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 19;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].skinColor;
					}
					if (main.selectedMenu == 3)
					{
						Main.menuMode = 20;
						Main.PlaySound(10);
					}
					array7[0] = Lang.menu[18];
					array7[1] = Lang.menu[19];
					array7[2] = Lang.menu[20];
					array7[3] = Lang.menu[21];
					MenuPosY = 220;
					for (int num39 = 0; num39 < 9; num39++)
					{
						if (num39 < 6)
						{
							array5[num39] = 0.75f;
						}
						else
						{
							array5[num39] = 0.9f;
						}
					}
					num2 = 38;
					array2[6] = 6;
					array2[7] = 12;
					array2[8] = 18;
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 176;
					if (Main.loadPlayer[num4].male)
					{
						array7[4] = Lang.menu[22];
					}
					else
					{
						array7[4] = Lang.menu[23];
					}
					if (main.selectedMenu == 4)
					{
						if (Main.loadPlayer[num4].male)
						{
							Main.PlaySound(20);
							Main.loadPlayer[num4].male = false;
						}
						else
						{
							Main.PlaySound(1);
							Main.loadPlayer[num4].male = true;
						}
					}
					if (Main.loadPlayer[num4].difficulty == 2)
					{
						array7[5] = Lang.menu[24];
						array4[5] = Main.loadPlayer[num4].difficulty;
					}
					else if (Main.loadPlayer[num4].difficulty == 1)
					{
						array7[5] = Lang.menu[25];
						array4[5] = Main.loadPlayer[num4].difficulty;
					}
					else
					{
						array7[5] = Lang.menu[26];
					}
					if (main.selectedMenu == 5)
					{
						Main.PlaySound(10);
						Main.menuMode = 222;
					}
					if (main.selectedMenu == 7)
					{
						Main.PlaySound(12);
						Main.loadPlayer[num4].hair = Main.rand.Next(36);
						Main.loadPlayer[num4].eyeColor = main.randColor();
						while (Main.loadPlayer[num4].eyeColor.R + Main.loadPlayer[num4].eyeColor.G + Main.loadPlayer[num4].eyeColor.B > 300)
						{
							Main.loadPlayer[num4].eyeColor = main.randColor();
						}
						Main.loadPlayer[num4].hairColor = main.randColor();
						Main.loadPlayer[num4].pantsColor = main.randColor();
						Main.loadPlayer[num4].shirtColor = main.randColor();
						Main.loadPlayer[num4].shoeColor = main.randColor();
						Main.loadPlayer[num4].skinColor = main.randColor();
						float num40 = (float)Main.rand.Next(60, 120) * 0.01f;
						if (num40 > 1f)
						{
							num40 = 1f;
						}
						Main.loadPlayer[num4].skinColor.R = (byte)((float)Main.rand.Next(240, 255) * num40);
						Main.loadPlayer[num4].skinColor.G = (byte)((float)Main.rand.Next(110, 140) * num40);
						Main.loadPlayer[num4].skinColor.B = (byte)((float)Main.rand.Next(75, 110) * num40);
						Main.loadPlayer[num4].underShirtColor = main.randColor();
						int num41 = Main.loadPlayer[num4].hair + 1;
						if (num41 == 5 || num41 == 6 || num41 == 7 || num41 == 10 || num41 == 12 || num41 == 19 || num41 == 22 || num41 == 23 || num41 == 26 || num41 == 27 || num41 == 30 || num41 == 33)
						{
							Main.loadPlayer[num4].male = false;
						}
						else
						{
							Main.loadPlayer[num4].male = true;
						}
					}
					array7[7] = Lang.menu[27];
					array7[6] = Lang.menu[28];
					array7[8] = Lang.menu[5];
					num3 = 9;
					if (main.selectedMenu == 8)
					{
						Main.PlaySound(11);
						Main.menuMode = 1;
					}
					else if (main.selectedMenu == 6)
					{
						Main.PlaySound(10);
						Main.loadPlayer[Main.numLoadPlayers].name = "";
						Main.menuMode = 3;
						Main.clrInput();
					}
					break;
				}
				case 222:
					if (main.focusMenu == 3)
					{
						array7[0] = Lang.menu[29];
					}
					else if (main.focusMenu == 2)
					{
						array7[0] = Lang.menu[30];
					}
					else if (main.focusMenu == 1)
					{
						array7[0] = Lang.menu[31];
					}
					else
					{
						array7[0] = Lang.menu[32];
					}
					num2 = 50;
					array2[1] = 25;
					array2[2] = 25;
					array2[3] = 25;
					MenuActive[0] = true;
					array7[1] = Lang.menu[26];
					array7[2] = Lang.menu[25];
					array4[2] = 1;
					array7[3] = Lang.menu[24];
					array4[3] = 2;
					num3 = 4;
					if (main.selectedMenu == 1)
					{
						Main.loadPlayer[Main.numLoadPlayers].difficulty = 0;
						Main.menuMode = 2;
					}
					else if (main.selectedMenu == 2)
					{
						Main.menuMode = 2;
						Main.loadPlayer[Main.numLoadPlayers].difficulty = 1;
					}
					else if (main.selectedMenu == 3)
					{
						Main.loadPlayer[Main.numLoadPlayers].difficulty = 2;
						Main.menuMode = 2;
					}
					break;
				case 20:
					if (main.selectedMenu == 0)
					{
						Main.menuMode = 21;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].shirtColor;
					}
					if (main.selectedMenu == 1)
					{
						Main.menuMode = 22;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].underShirtColor;
					}
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 23;
						Main.PlaySound(10);
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].pantsColor;
					}
					if (main.selectedMenu == 3)
					{
						main.selColor = Main.loadPlayer[Main.numLoadPlayers].shoeColor;
						Main.menuMode = 24;
						Main.PlaySound(10);
					}
					array7[0] = Lang.menu[33];
					array7[1] = Lang.menu[34];
					array7[2] = Lang.menu[35];
					array7[3] = Lang.menu[36];
					MenuPosY = 260;
					num2 = 50;
					array2[5] = 20;
					array7[5] = Lang.menu[5];
					num3 = 6;
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					if (main.selectedMenu == 5)
					{
						Main.PlaySound(11);
						Main.menuMode = 2;
					}
					break;
				case 17:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 390;
					MenuPosY = 260;
					num2 = 60;
					Main.loadPlayer[num4].hairColor = main.selColor;
					num3 = 3;
					array7[0] = Lang.menu[37] + " " + (Main.loadPlayer[num4].hair + 1);
					array7[1] = Lang.menu[38];
					MenuActive[1] = true;
					array2[2] = 150;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 0)
					{
						Main.PlaySound(12);
						Main.loadPlayer[num4].hair++;
						if (Main.loadPlayer[num4].hair >= 36)
						{
							Main.loadPlayer[num4].hair = 0;
						}
					}
					else if (main.selectedMenu2 == 0)
					{
						Main.PlaySound(12);
						Main.loadPlayer[num4].hair--;
						if (Main.loadPlayer[num4].hair < 0)
						{
							Main.loadPlayer[num4].hair = 35;
						}
					}
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 2;
						Main.PlaySound(11);
					}
					break;
				case 18:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].eyeColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[39];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 2;
						Main.PlaySound(11);
					}
					break;
				case 19:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].skinColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[40];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 2;
						Main.PlaySound(11);
					}
					break;
				case 21:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].shirtColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[41];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 20;
						Main.PlaySound(11);
					}
					break;
				case 22:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].underShirtColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[42];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 20;
						Main.PlaySound(11);
					}
					break;
				case 23:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].pantsColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[43];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 20;
						Main.PlaySound(11);
					}
					break;
				case 24:
					num4 = Main.numLoadPlayers;
					num5 = Main.screenWidth / 2 - 16;
					num6 = 210;
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.loadPlayer[num4].shoeColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[44];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 20;
						Main.PlaySound(11);
					}
					break;
				case 3:
				{
					string name = Main.loadPlayer[Main.numLoadPlayers].name;
					Main.loadPlayer[Main.numLoadPlayers].name = Main.GetInputText(Main.loadPlayer[Main.numLoadPlayers].name);
					if (Main.loadPlayer[Main.numLoadPlayers].name.Length > Player.nameLen)
					{
						Main.loadPlayer[Main.numLoadPlayers].name = Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, Player.nameLen);
					}
					if (name != Main.loadPlayer[Main.numLoadPlayers].name)
					{
						Main.PlaySound(12);
					}
					array7[0] = Lang.menu[45];
					array[2] = true;
					if (Main.loadPlayer[Main.numLoadPlayers].name != "")
					{
						if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(0, 1) == " ")
						{
							Main.loadPlayer[Main.numLoadPlayers].name = "";
						}
						for (int num38 = 0; num38 < Main.loadPlayer[Main.numLoadPlayers].name.Length; num38++)
						{
							if (Main.loadPlayer[Main.numLoadPlayers].name.Substring(num38, 1) != " ")
							{
								array[2] = false;
							}
						}
					}
					main.textBlinkerCount++;
					if (main.textBlinkerCount >= 20)
					{
						if (main.textBlinkerState == 0)
						{
							main.textBlinkerState = 1;
						}
						else
						{
							main.textBlinkerState = 0;
						}
						main.textBlinkerCount = 0;
					}
					array7[1] = Main.loadPlayer[Main.numLoadPlayers].name;
					if (main.textBlinkerState == 1)
					{
						string[] array36;
						(array36 = array7)[1] = array36[1] + "|";
						array3[1] = 1;
					}
					else
					{
						string[] array37;
						(array37 = array7)[1] = array37[1] + " ";
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[1] = -20;
					array2[2] = 20;
					array7[2] = Lang.menu[4];
					array7[3] = Lang.menu[5];
					num3 = 4;
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 2;
					}
					if (main.selectedMenu == 2 || (!array[2] && Main.inputTextEnter))
					{
						Main.loadPlayer[Main.numLoadPlayers].name.Trim();
						Main.loadPlayerPath[Main.numLoadPlayers] = Main.nextLoadPlayer();
						Player.SavePlayer(Main.loadPlayer[Main.numLoadPlayers], Main.loadPlayerPath[Main.numLoadPlayers]);
						Main.savePlayerSnapshot = true;
						Main.playerPathName = Main.loadPlayerPath[Main.numLoadPlayers];
						Main.saveClone = Main.loadPlayer[Main.numLoadPlayers];
						Main.loadPlayersAfterScreenshot = true;
						Main.PlaySound(10);
						Main.menuMode = 1;
					}
					break;
				}
				case 5:
					array7[0] = Lang.menu[46] + " " + Main.loadPlayerName[main.selectedPlayer] + "?";
					MenuActive[0] = true;
					array7[1] = Lang.menu[104];
					array7[2] = Lang.menu[105];
					num3 = 3;
					if (main.selectedMenu == 1)
					{
						Main.ErasePlayer(main.selectedPlayer);
						Main.PlaySound(10);
						Main.menuMode = 1;
					}
					else if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.menuMode = 1;
					}
					break;
				case 6:
				case 8:
				{
					int[] array39 = array2;
					int[] array40 = array3;
					float[] array41 = array5;
					MenuPosY = 180;
					num2 = 35;
					array39[7] = 10;
					num3 = 8;
					int num43 = 0;
					float num44 = 0.7f;
					int num45 = 7;
					if (Main.numLoadWorlds == 0)
					{
						num45--;
					}
					int tConfigWorldMenuStart = Config.tConfigWorldMenuStart;
					for (int num46 = tConfigWorldMenuStart; num46 < Main.numLoadWorlds; num46++)
					{
						string text12 = array7[num43] = Main.loadWorld[num46];
						array41[num43] = num44;
						array40[num43] = 0;
						array39[num43] = 0;
						if (main.selectedMenu == num43)
						{
							if (Main.menuMode == 6)
							{
								if (Main.menuMultiplayer)
								{
									Main.PlaySound(10);
									Main.worldPathName = Main.loadWorldPath[num46];
									Main.menuMode = 30;
								}
								else
								{
									Main.PlaySound(10);
									Main.worldPathName = Main.loadWorldPath[num46];
									WorldGen.playWorld();
									Main.menuMode = 10;
								}
							}
							else if (Main.menuMode == 8)
							{
								main.selectedWorld = num46;
								Main.PlaySound(10);
								Main.menuMode = 9;
							}
						}
						num43++;
						if (Config.tConfigWorldMenuStart > 0 && (num46 + 1 == Main.numLoadWorlds || num43 >= num45))
						{
							for (; num43 < num45; num43++)
							{
							}
							array7[num43] = "<- Previous";
							array41[num43] = num44;
							if (main.selectedMenu == num43)
							{
								Config.tConfigWorldMenuStart -= num45;
							}
							num43++;
						}
						if (num43 >= num45 && num46 + 1 < Main.numLoadWorlds)
						{
							for (; num43 < num45 + 1; num43++)
							{
							}
							array41[num43] = num44;
							array7[num43] = "Next ->";
							if (main.selectedMenu == num43)
							{
								Config.tConfigWorldMenuStart += num45;
							}
							num43++;
							break;
						}
					}
					if (tConfigWorldMenuStart > 0 && tConfigWorldMenuStart == Main.numLoadWorlds)
					{
						Config.tConfigWorldMenuStart -= num45;
					}
					for (; num43 < num45 + 2; num43++)
					{
					}
					num3 = num43 + 1;
					if (Main.menuMode == 6)
					{
						array7[num43] = Lang.menu[47];
						array7[num43 + 1] = Lang.menu[17];
						array41[num43] = num44;
						array41[num43 + 1] = num44;
						if (main.selectedMenu == num43)
						{
							Main.PlaySound(10);
							Main.menuMode = 16;
							Main.newWorldName = Lang.gen[57] + " " + (Main.numLoadWorlds + 1);
						}
						if (main.selectedMenu == num43 + 1)
						{
							Main.PlaySound(10);
							Main.menuMode = 8;
						}
						if (Main.numLoadWorlds == 0)
						{
							array7[num43 + 2] = "Copy Over Save Files";
							array41[num43 + 2] = num44;
							if (main.selectedMenu == num43 + 2)
							{
								Main.PlaySound(10);
								string path2 = Main.SavePath + Path.DirectorySeparatorChar + "Worlds";
								string[] files = Directory.GetFiles(path2, "*.wld");
								foreach (string text13 in files)
								{
									try
									{
										File.Copy(text13, Main.WorldPath + Path.DirectorySeparatorChar + Path.GetFileName(text13), overwrite: false);
									}
									catch
									{
									}
								}
								Main.LoadWorlds();
							}
							num43++;
							num3++;
						}
						num43 += 2;
						num3 += 2;
					}
					array7[num43] = Lang.menu[5];
					array41[num43] = num44;
					if (main.selectedMenu == num43)
					{
						if (Main.menuMultiplayer)
						{
							Main.menuMode = 12;
						}
						else
						{
							Main.menuMode = 1;
						}
						Main.PlaySound(11);
					}
					break;
				}
				case 7:
				{
					string newWorldName = Main.newWorldName;
					Main.newWorldName = Main.GetInputText(Main.newWorldName);
					if (Main.newWorldName.Length > 20)
					{
						Main.newWorldName = Main.newWorldName.Substring(0, 20);
					}
					if (newWorldName != Main.newWorldName)
					{
						Main.PlaySound(12);
					}
					array7[0] = Lang.menu[48];
					array[2] = true;
					if (Main.newWorldName != "")
					{
						if (Main.newWorldName.Substring(0, 1) == " ")
						{
							Main.newWorldName = "";
						}
						for (int num56 = 0; num56 < Main.newWorldName.Length; num56++)
						{
							if (Main.newWorldName != " ")
							{
								array[2] = false;
							}
						}
					}
					main.textBlinkerCount++;
					if (main.textBlinkerCount >= 20)
					{
						if (main.textBlinkerState == 0)
						{
							main.textBlinkerState = 1;
						}
						else
						{
							main.textBlinkerState = 0;
						}
						main.textBlinkerCount = 0;
					}
					array7[1] = Main.newWorldName;
					if (main.textBlinkerState == 1)
					{
						string[] array44;
						(array44 = array7)[1] = array44[1] + "|";
						array3[1] = 1;
					}
					else
					{
						string[] array45;
						(array45 = array7)[1] = array45[1] + " ";
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[1] = -20;
					array2[2] = 20;
					array7[2] = Lang.menu[4];
					array7[3] = Lang.menu[5];
					num3 = 4;
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 16;
					}
					if (main.selectedMenu == 2 || (!array[2] && Main.inputTextEnter))
					{
						Main.menuMode = 10;
						Main.worldName = Main.newWorldName;
						Main.worldPathName = Main.nextLoadWorld();
						WorldGen.CreateNewWorld();
					}
					break;
				}
				case 9:
					array7[0] = Lang.menu[46] + " " + Main.loadWorld[main.selectedWorld] + "?";
					MenuActive[0] = true;
					array7[1] = Lang.menu[104];
					array7[2] = Lang.menu[105];
					num3 = 3;
					if (main.selectedMenu == 1)
					{
						Main.EraseWorld(main.selectedWorld);
						Main.PlaySound(10);
						Main.menuMode = 6;
					}
					else if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.menuMode = 6;
					}
					break;
				case 1111:
				{
					MenuPosY = 210;
					num2 = 46;
					for (int num37 = 0; num37 < 7; num37++)
					{
						array5[num37] = 0.9f;
					}
					array2[7] = 10;
					num3 = 8;
					if (main.graphics.IsFullScreen)
					{
						array7[0] = Lang.menu[49];
					}
					else
					{
						array7[0] = Lang.menu[50];
					}
					main.bgScroll = (int)Math.Round((1f - Main.caveParrallax) * 500f);
					array7[1] = Lang.menu[51];
					array7[2] = Lang.menu[52];
					if (Main.fixedTiming)
					{
						array7[3] = Lang.menu[53];
					}
					else
					{
						array7[3] = Lang.menu[54];
					}
					if (Lighting.lightMode == 0)
					{
						array7[4] = Lang.menu[55];
					}
					else if (Lighting.lightMode == 1)
					{
						array7[4] = Lang.menu[56];
					}
					else if (Lighting.lightMode == 2)
					{
						array7[4] = Lang.menu[57];
					}
					else if (Lighting.lightMode == 3)
					{
						array7[4] = Lang.menu[58];
					}
					if (Main.qaStyle == 0)
					{
						array7[5] = Lang.menu[59];
					}
					else if (Main.qaStyle == 1)
					{
						array7[5] = Lang.menu[60];
					}
					else if (Main.qaStyle == 2)
					{
						array7[5] = Lang.menu[61];
					}
					else
					{
						array7[5] = Lang.menu[62];
					}
					if (Main.owBack)
					{
						array7[6] = Lang.menu[100];
					}
					else
					{
						array7[6] = Lang.menu[101];
					}
					if (main.selectedMenu == 6)
					{
						Main.PlaySound(12);
						if (Main.owBack)
						{
							Main.owBack = false;
						}
						else
						{
							Main.owBack = true;
						}
					}
					array7[7] = Lang.menu[5];
					if (main.selectedMenu == 7)
					{
						Main.PlaySound(11);
						main.SaveSettings();
						Main.menuMode = 11;
					}
					if (main.selectedMenu == 5)
					{
						Main.PlaySound(12);
						Main.qaStyle++;
						if (Main.qaStyle > 3)
						{
							Main.qaStyle = 0;
						}
					}
					if (main.selectedMenu == 4)
					{
						Main.PlaySound(12);
						Lighting.lightMode++;
						if (Lighting.lightMode >= 4)
						{
							Lighting.lightMode = 0;
						}
					}
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(12);
						if (Main.fixedTiming)
						{
							Main.fixedTiming = false;
						}
						else
						{
							Main.fixedTiming = true;
						}
					}
					if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.menuMode = 28;
					}
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(10);
						Main.menuMode = 111;
					}
					if (main.selectedMenu == 0)
					{
						main.graphics.ToggleFullScreen();
					}
					break;
				}
				case 11:
				{
					MenuPosY = 180;
					num2 = 44;
					array2[8] = 10;
					num3 = 9;
					for (int num20 = 0; num20 < 9; num20++)
					{
						array5[num20] = 0.9f;
					}
					array7[0] = Lang.menu[63];
					array7[1] = Lang.menu[64];
					array7[2] = Lang.menu[65];
					array7[3] = Lang.menu[66];
					if (Main.autoSave)
					{
						array7[4] = Lang.menu[67];
					}
					else
					{
						array7[4] = Lang.menu[68];
					}
					if (Main.autoPause)
					{
						array7[5] = Lang.menu[69];
					}
					else
					{
						array7[5] = Lang.menu[70];
					}
					if (Main.showItemText)
					{
						array7[6] = Lang.menu[71];
					}
					else
					{
						array7[6] = Lang.menu[72];
					}
					array7[8] = Lang.menu[5];
					array7[7] = Lang.menu[103];
					if (main.selectedMenu == 7)
					{
						Main.PlaySound(10);
						Main.menuMode = 1213;
					}
					if (main.selectedMenu == 8)
					{
						Main.PlaySound(11);
						main.SaveSettings();
						Main.menuMode = 0;
					}
					if (main.selectedMenu == 6)
					{
						Main.PlaySound(12);
						if (Main.showItemText)
						{
							Main.showItemText = false;
						}
						else
						{
							Main.showItemText = true;
						}
					}
					if (main.selectedMenu == 5)
					{
						Main.PlaySound(12);
						if (Main.autoPause)
						{
							Main.autoPause = false;
						}
						else
						{
							Main.autoPause = true;
						}
					}
					if (main.selectedMenu == 4)
					{
						Main.PlaySound(12);
						if (Main.autoSave)
						{
							Main.autoSave = false;
						}
						else
						{
							Main.autoSave = true;
						}
					}
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 27;
					}
					if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.menuMode = 26;
					}
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(10);
						main.selColor = Main.mouseColor;
						Main.menuMode = 25;
					}
					if (main.selectedMenu == 0)
					{
						Main.PlaySound(10);
						Main.menuMode = 1111;
					}
					break;
				}
				case 111:
					MenuPosY = 240;
					num2 = 60;
					num3 = 3;
					array7[0] = Lang.menu[73];
					array7[1] = main.graphics.PreferredBackBufferWidth + "x" + main.graphics.PreferredBackBufferHeight;
					MenuActive[0] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 1)
					{
						Main.PlaySound(12);
						int num57 = 0;
						for (int num58 = 0; num58 < main.numDisplayModes; num58++)
						{
							if (main.displayWidth[num58] == main.graphics.PreferredBackBufferWidth && main.displayHeight[num58] == main.graphics.PreferredBackBufferHeight)
							{
								num57 = num58;
								break;
							}
						}
						num57++;
						if (num57 >= main.numDisplayModes)
						{
							num57 = 0;
						}
						main.graphics.PreferredBackBufferWidth = main.displayWidth[num57];
						main.graphics.PreferredBackBufferHeight = main.displayHeight[num57];
					}
					if (main.selectedMenu == 2)
					{
						if (main.graphics.IsFullScreen)
						{
							main.graphics.ApplyChanges();
						}
						Main.menuMode = 1111;
						Main.PlaySound(11);
					}
					break;
				case 25:
					flag = true;
					num7 = 370;
					MenuPosY = 240;
					num2 = 60;
					Main.mouseColor = main.selColor;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[64];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 11;
						Main.PlaySound(11);
					}
					break;
				case 26:
					flag2 = true;
					MenuPosY = 240;
					num2 = 60;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[65];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 11;
						Main.PlaySound(11);
					}
					break;
				case 28:
					Main.caveParrallax = 1f - (float)main.bgScroll / 500f;
					flag3 = true;
					MenuPosY = 240;
					num2 = 60;
					num3 = 3;
					array7[0] = "";
					array7[1] = Lang.menu[52];
					MenuActive[1] = true;
					array2[2] = 170;
					array2[1] = 10;
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 2)
					{
						Main.menuMode = 1111;
						Main.PlaySound(11);
					}
					break;
				case 27:
				{
					MenuPosY = 176;
					num2 = 28;
					num3 = 14;
					string[] array38 = new string[12]
					{
						Main.cUp,
						Main.cDown,
						Main.cLeft,
						Main.cRight,
						Main.cJump,
						Main.cThrowItem,
						Main.cInv,
						Main.cHeal,
						Main.cMana,
						Main.cBuff,
						Main.cHook,
						Main.cTorch
					};
					if (main.setKey >= 0)
					{
						array38[main.setKey] = "_";
					}
					array7[0] = Lang.menu[74] + array38[0];
					array7[1] = Lang.menu[75] + array38[1];
					array7[2] = Lang.menu[76] + array38[2];
					array7[3] = Lang.menu[77] + array38[3];
					array7[4] = Lang.menu[78] + array38[4];
					array7[5] = Lang.menu[79] + array38[5];
					array7[6] = Lang.menu[80] + array38[6];
					array7[7] = Lang.menu[81] + array38[7];
					array7[8] = Lang.menu[82] + array38[8];
					array7[9] = Lang.menu[83] + array38[9];
					array7[10] = Lang.menu[84] + array38[10];
					array7[11] = Lang.menu[85] + array38[11];
					for (int num42 = 0; num42 < 12; num42++)
					{
						array6[num42] = true;
						array5[num42] = 0.55f;
						array3[num42] = -80;
					}
					array5[12] = 0.8f;
					array5[13] = 0.8f;
					array2[12] = 6;
					array7[12] = Lang.menu[86];
					array2[13] = 16;
					array7[13] = Lang.menu[5];
					if (main.selectedMenu == 13)
					{
						Main.menuMode = 11;
						Main.PlaySound(11);
					}
					else if (main.selectedMenu == 12)
					{
						Main.cUp = "W";
						Main.cDown = "S";
						Main.cLeft = "A";
						Main.cRight = "D";
						Main.cJump = "Space";
						Main.cThrowItem = "T";
						Main.cInv = "Escape";
						Main.cHeal = "H";
						Main.cMana = "M";
						Main.cBuff = "B";
						Main.cHook = "E";
						Main.cTorch = "LeftShift";
						main.setKey = -1;
						Main.PlaySound(11);
					}
					else if (main.selectedMenu >= 0)
					{
						main.setKey = main.selectedMenu;
					}
					if (main.setKey < 0)
					{
						break;
					}
					Keys[] pressedKeys3 = Main.keyState.GetPressedKeys();
					if (pressedKeys3.Length <= 0)
					{
						break;
					}
					string text11 = string.Concat(pressedKeys3[0]);
					if (text11 != "None")
					{
						if (main.setKey == 0)
						{
							Main.cUp = text11;
						}
						if (main.setKey == 1)
						{
							Main.cDown = text11;
						}
						if (main.setKey == 2)
						{
							Main.cLeft = text11;
						}
						if (main.setKey == 3)
						{
							Main.cRight = text11;
						}
						if (main.setKey == 4)
						{
							Main.cJump = text11;
						}
						if (main.setKey == 5)
						{
							Main.cThrowItem = text11;
						}
						if (main.setKey == 6)
						{
							Main.cInv = text11;
						}
						if (main.setKey == 7)
						{
							Main.cHeal = text11;
						}
						if (main.setKey == 8)
						{
							Main.cMana = text11;
						}
						if (main.setKey == 9)
						{
							Main.cBuff = text11;
						}
						if (main.setKey == 10)
						{
							Main.cHook = text11;
						}
						if (main.setKey == 11)
						{
							Main.cTorch = text11;
						}
						main.setKey = -1;
					}
					break;
				}
				case 12:
					Main.menuServer = false;
					array7[0] = Lang.menu[87];
					array7[1] = Lang.menu[88];
					array7[2] = Lang.menu[5];
					if (main.selectedMenu == 0)
					{
						Main.LoadPlayers();
						Main.menuMultiplayer = true;
						Main.PlaySound(10);
						Main.menuMode = 1;
					}
					else if (main.selectedMenu == 1)
					{
						Main.LoadPlayers();
						Main.PlaySound(10);
						Main.menuMode = 1;
						Main.menuMultiplayer = true;
						Main.menuServer = true;
					}
					if (main.selectedMenu == 2)
					{
						Main.PlaySound(11);
						Main.menuMode = 0;
					}
					num3 = 3;
					break;
				case 13:
				{
					string getIP = Main.getIP;
					Main.getIP = Main.GetInputText(Main.getIP);
					if (getIP != Main.getIP)
					{
						Main.PlaySound(12);
					}
					array7[0] = Lang.menu[89];
					array[9] = true;
					if (Main.getIP != "")
					{
						if (Main.getIP.Substring(0, 1) == " ")
						{
							Main.getIP = "";
						}
						for (int num47 = 0; num47 < Main.getIP.Length; num47++)
						{
							if (Main.getIP != " ")
							{
								array[9] = false;
							}
						}
					}
					main.textBlinkerCount++;
					if (main.textBlinkerCount >= 20)
					{
						if (main.textBlinkerState == 0)
						{
							main.textBlinkerState = 1;
						}
						else
						{
							main.textBlinkerState = 0;
						}
						main.textBlinkerCount = 0;
					}
					array7[1] = Main.getIP;
					if (main.textBlinkerState == 1)
					{
						string[] array42;
						(array42 = array7)[1] = array42[1] + "|";
						array3[1] = 1;
					}
					else
					{
						string[] array43;
						(array43 = array7)[1] = array43[1] + " ";
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[9] = 44;
					array2[10] = 64;
					array7[9] = Lang.menu[4];
					array7[10] = Lang.menu[5];
					num3 = 11;
					MenuPosY = 180;
					num2 = 30;
					array2[1] = 19;
					for (int num48 = 2; num48 < 9; num48++)
					{
						int num49 = num48 - 2;
						if (Main.recentWorld[num49] != null && Main.recentWorld[num49] != "")
						{
							array7[num48] = Main.recentWorld[num49] + " (" + Main.recentIP[num49] + ":" + Main.recentPort[num49] + ")";
						}
						else
						{
							array7[num48] = "";
							MenuActive[num48] = true;
						}
						array5[num48] = 0.6f;
						array2[num48] = 40;
					}
					if (main.selectedMenu >= 2 && main.selectedMenu < 9)
					{
						Main.autoPass = false;
						int num50 = main.selectedMenu - 2;
						Netplay.serverPort = Main.recentPort[num50];
						Main.getIP = Main.recentIP[num50];
						if (Netplay.SetIP(Main.getIP))
						{
							Main.menuMode = 10;
							Netplay.StartClient();
						}
						else if (Netplay.SetIP2(Main.getIP))
						{
							Main.menuMode = 10;
							Netplay.StartClient();
						}
					}
					if (main.selectedMenu == 10)
					{
						Main.PlaySound(11);
						Main.menuMode = 1;
					}
					if (main.selectedMenu == 9 || (!array[2] && Main.inputTextEnter))
					{
						Main.PlaySound(12);
						Main.menuMode = 131;
						Main.clrInput();
					}
					break;
				}
				case 131:
				{
					int num15 = 7777;
					string getPort = Main.getPort;
					Main.getPort = Main.GetInputText(Main.getPort);
					if (getPort != Main.getPort)
					{
						Main.PlaySound(12);
					}
					array7[0] = Lang.menu[90];
					array[2] = true;
					if (Main.getPort != "")
					{
						bool flag8 = false;
						try
						{
							num15 = Convert.ToInt32(Main.getPort);
							if (num15 > 0 && num15 <= 65535)
							{
								flag8 = true;
							}
						}
						catch
						{
						}
						if (flag8)
						{
							array[2] = false;
						}
					}
					main.textBlinkerCount++;
					if (main.textBlinkerCount >= 20)
					{
						if (main.textBlinkerState == 0)
						{
							main.textBlinkerState = 1;
						}
						else
						{
							main.textBlinkerState = 0;
						}
						main.textBlinkerCount = 0;
					}
					array7[1] = Main.getPort;
					if (main.textBlinkerState == 1)
					{
						string[] array20;
						(array20 = array7)[1] = array20[1] + "|";
						array3[1] = 1;
					}
					else
					{
						string[] array21;
						(array21 = array7)[1] = array21[1] + " ";
					}
					MenuActive[0] = true;
					MenuActive[1] = true;
					array2[1] = -20;
					array2[2] = 20;
					array7[2] = Lang.menu[4];
					array7[3] = Lang.menu[5];
					num3 = 4;
					if (main.selectedMenu == 3)
					{
						Main.PlaySound(11);
						Main.menuMode = 1;
					}
					if (main.selectedMenu == 2 || (!array[2] && Main.inputTextEnter))
					{
						Netplay.serverPort = num15;
						Main.autoPass = false;
						if (Netplay.SetIP(Main.getIP))
						{
							Main.menuMode = 10;
							Netplay.StartClient();
						}
						else if (Netplay.SetIP2(Main.getIP))
						{
							Main.menuMode = 10;
							Netplay.StartClient();
						}
					}
					break;
				}
				case 16:
					MenuPosY = 200;
					num2 = 60;
					array2[1] = 30;
					array2[2] = 30;
					array2[3] = 30;
					array2[4] = 70;
					array7[0] = Lang.menu[91];
					MenuActive[0] = true;
					array7[1] = Lang.menu[92];
					array7[2] = Lang.menu[93];
					array7[3] = Lang.menu[94];
					array7[4] = Lang.menu[5];
					num3 = 5;
					if (main.selectedMenu == 4)
					{
						Main.menuMode = 6;
						Main.PlaySound(11);
					}
					else if (main.selectedMenu > 0)
					{
						if (main.selectedMenu == 1)
						{
							Main.maxTilesX = 4200;
							Main.maxTilesY = 1200;
						}
						else if (main.selectedMenu == 2)
						{
							Main.maxTilesX = 6400;
							Main.maxTilesY = 1800;
						}
						else
						{
							Main.maxTilesX = 8400;
							Main.maxTilesY = 2400;
						}
						Main.clrInput();
						Main.menuMode = 7;
						Main.PlaySound(10);
						WorldGen.setWorldSize();
					}
					break;
				}
			}
			if (Main.menuMode != menuMode)
			{
				num3 = 0;
				for (int num59 = 0; num59 < Main.maxMenuItems; num59++)
				{
					main.menuItemScale[num59] = 0.8f;
				}
			}
			int focusMenu = main.focusMenu;
			main.selectedMenu = -1;
			main.selectedMenu2 = -1;
			main.focusMenu = -1;
			bool flag11 = false;
			if (Codable.RunGlobalMethod("ModGeneric", "OverrideMenuSelection", focusMenu, num3, main) && !(bool)Codable.customMethodReturn)
			{
				flag11 = true;
			}
			for (int num60 = 0; num60 < num3; num60++)
			{
				if (array7[num60] == null)
				{
					continue;
				}
				if (flag)
				{
					string text14 = "";
					for (int num61 = 0; num61 < 6; num61++)
					{
						int num62 = num7;
						int num63 = 370 + Main.screenWidth / 2 - 400;
						if (num61 == 0)
						{
							text14 = Lang.menu[95];
						}
						if (num61 == 1)
						{
							text14 = Lang.menu[96];
							num62 += 30;
						}
						if (num61 == 2)
						{
							text14 = Lang.menu[97];
							num62 += 60;
						}
						if (num61 == 3)
						{
							text14 = string.Concat(main.selColor.R);
							num63 += 90;
						}
						if (num61 == 4)
						{
							text14 = string.Concat(main.selColor.G);
							num63 += 90;
							num62 += 30;
						}
						if (num61 == 5)
						{
							text14 = string.Concat(main.selColor.B);
							num63 += 90;
							num62 += 60;
						}
						for (int num64 = 0; num64 < 5; num64++)
						{
							Color color4 = Color.Black;
							if (num64 == 4)
							{
								color4 = color;
								color4.R = (byte)((255 + color4.R) / 2);
								color4.G = (byte)((255 + color4.R) / 2);
								color4.B = (byte)((255 + color4.R) / 2);
							}
							int num65 = 255;
							int num66 = color4.R - (255 - num65);
							if (num66 < 0)
							{
								num66 = 0;
							}
							color4 = new Color((byte)num66, (byte)num66, (byte)num66, (byte)num65);
							int num67 = 0;
							int num68 = 0;
							if (num64 == 0)
							{
								num67 = -2;
							}
							if (num64 == 1)
							{
								num67 = 2;
							}
							if (num64 == 2)
							{
								num68 = -2;
							}
							if (num64 == 3)
							{
								num68 = 2;
							}
							SpriteBatch spriteBatch = main.spriteBatch;
							SpriteFont fontDeathText = Main.fontDeathText;
							string text15 = text14;
							Vector2 position = new Vector2(num63 + num67, num62 + num68);
							Color color5 = color4;
							float rotation = 0f;
							spriteBatch.DrawString(fontDeathText, text15, position, color5, rotation, default(Vector2), 0.5f, SpriteEffects.None, 0f);
						}
					}
					bool flag12 = false;
					for (int num69 = 0; num69 < 2; num69++)
					{
						for (int num70 = 0; num70 < 3; num70++)
						{
							int num71 = num7 + num70 * 30 - 12;
							int num72 = 360 + Main.screenWidth / 2 - 400;
							float scale = 0.9f;
							if (num69 == 0)
							{
								num72 -= 70;
								num71 += 2;
							}
							else
							{
								num72 -= 40;
							}
							text14 = "-";
							if (num69 == 1)
							{
								text14 = "+";
							}
							Vector2 vector = new Vector2(24f, 24f);
							int num73 = 142;
							if (Main.mouseX > num72 && (float)Main.mouseX < (float)num72 + vector.X && Main.mouseY > num71 + 13 && (float)Main.mouseY < (float)(num71 + 13) + vector.Y)
							{
								if (main.focusColor != (num69 + 1) * (num70 + 10))
								{
									Main.PlaySound(12);
								}
								main.focusColor = (num69 + 1) * (num70 + 10);
								flag12 = true;
								num73 = 255;
								if (Main.mouseLeft)
								{
									if (main.colorDelay <= 1)
									{
										if (main.colorDelay == 0)
										{
											main.colorDelay = 40;
										}
										else
										{
											main.colorDelay = 3;
										}
										int num74 = num69;
										if (num69 == 0)
										{
											num74 = -1;
											if (main.selColor.R + main.selColor.G + main.selColor.B <= 150)
											{
												num74 = 0;
											}
										}
										if (num70 == 0 && main.selColor.R + num74 >= 0 && main.selColor.R + num74 <= 255)
										{
											main.selColor.R = (byte)(main.selColor.R + num74);
										}
										if (num70 == 1 && main.selColor.G + num74 >= 0 && main.selColor.G + num74 <= 255)
										{
											main.selColor.G = (byte)(main.selColor.G + num74);
										}
										if (num70 == 2 && main.selColor.B + num74 >= 0 && main.selColor.B + num74 <= 255)
										{
											main.selColor.B = (byte)(main.selColor.B + num74);
										}
									}
									main.colorDelay--;
								}
								else
								{
									main.colorDelay = 0;
								}
							}
							for (int num75 = 0; num75 < 5; num75++)
							{
								Color color6 = Color.Black;
								if (num75 == 4)
								{
									color6 = color;
									color6.R = (byte)((255 + color6.R) / 2);
									color6.G = (byte)((255 + color6.R) / 2);
									color6.B = (byte)((255 + color6.R) / 2);
								}
								int num76 = color6.R - (255 - num73);
								if (num76 < 0)
								{
									num76 = 0;
								}
								color6 = new Color((byte)num76, (byte)num76, (byte)num76, (byte)num73);
								int num77 = 0;
								int num78 = 0;
								if (num75 == 0)
								{
									num77 = -2;
								}
								if (num75 == 1)
								{
									num77 = 2;
								}
								if (num75 == 2)
								{
									num78 = -2;
								}
								if (num75 == 3)
								{
									num78 = 2;
								}
								SpriteBatch spriteBatch2 = main.spriteBatch;
								SpriteFont fontDeathText2 = Main.fontDeathText;
								string text16 = text14;
								Vector2 position2 = new Vector2(num72 + num77, num71 + num78);
								Color color7 = color6;
								float rotation2 = 0f;
								spriteBatch2.DrawString(fontDeathText2, text16, position2, color7, rotation2, default(Vector2), scale, SpriteEffects.None, 0f);
							}
						}
					}
					if (!flag12)
					{
						main.focusColor = 0;
						main.colorDelay = 0;
					}
				}
				if (flag3)
				{
					int num79 = 400;
					string text17 = "";
					for (int num80 = 0; num80 < 4; num80++)
					{
						int num81 = num79;
						int num82 = 370 + Main.screenWidth / 2 - 400;
						if (num80 == 0)
						{
							text17 = Lang.menu[52] + ": " + main.bgScroll;
						}
						for (int num83 = 0; num83 < 5; num83++)
						{
							Color color8 = Color.Black;
							if (num83 == 4)
							{
								color8 = color;
								color8.R = (byte)((255 + color8.R) / 2);
								color8.G = (byte)((255 + color8.R) / 2);
								color8.B = (byte)((255 + color8.R) / 2);
							}
							int num84 = 255;
							int num85 = color8.R - (255 - num84);
							if (num85 < 0)
							{
								num85 = 0;
							}
							color8 = new Color((byte)num85, (byte)num85, (byte)num85, (byte)num84);
							int num86 = 0;
							int num87 = 0;
							if (num83 == 0)
							{
								num86 = -2;
							}
							if (num83 == 1)
							{
								num86 = 2;
							}
							if (num83 == 2)
							{
								num87 = -2;
							}
							if (num83 == 3)
							{
								num87 = 2;
							}
							SpriteBatch spriteBatch3 = main.spriteBatch;
							SpriteFont fontDeathText3 = Main.fontDeathText;
							string text18 = text17;
							Vector2 position3 = new Vector2(num82 + num86, num81 + num87);
							Color color9 = color8;
							float rotation3 = 0f;
							spriteBatch3.DrawString(fontDeathText3, text18, position3, color9, rotation3, default(Vector2), 0.5f, SpriteEffects.None, 0f);
						}
					}
					bool flag13 = false;
					for (int num88 = 0; num88 < 2; num88++)
					{
						for (int num89 = 0; num89 < 1; num89++)
						{
							int num90 = num79 + num89 * 30 - 12;
							int num91 = 360 + Main.screenWidth / 2 - 400;
							float scale2 = 0.9f;
							if (num88 == 0)
							{
								num91 -= 70;
								num90 += 2;
							}
							else
							{
								num91 -= 40;
							}
							text17 = "-";
							if (num88 == 1)
							{
								text17 = "+";
							}
							Vector2 vector2 = new Vector2(24f, 24f);
							int num92 = 142;
							if (Main.mouseX > num91 && (float)Main.mouseX < (float)num91 + vector2.X && Main.mouseY > num90 + 13 && (float)Main.mouseY < (float)(num90 + 13) + vector2.Y)
							{
								if (main.focusColor != (num88 + 1) * (num89 + 10))
								{
									Main.PlaySound(12);
								}
								main.focusColor = (num88 + 1) * (num89 + 10);
								flag13 = true;
								num92 = 255;
								if (Main.mouseLeft)
								{
									if (main.colorDelay <= 1)
									{
										if (main.colorDelay == 0)
										{
											main.colorDelay = 40;
										}
										else
										{
											main.colorDelay = 3;
										}
										int num93 = (num88 == 0) ? (-1) : num88;
										if (num89 == 0)
										{
											main.bgScroll += num93;
											if (main.bgScroll > 100)
											{
												main.bgScroll = 100;
											}
											if (main.bgScroll < 0)
											{
												main.bgScroll = 0;
											}
										}
									}
									main.colorDelay--;
								}
								else
								{
									main.colorDelay = 0;
								}
							}
							for (int num94 = 0; num94 < 5; num94++)
							{
								Color color10 = Color.Black;
								if (num94 == 4)
								{
									color10 = color;
									color10.R = (byte)((255 + color10.R) / 2);
									color10.G = (byte)((255 + color10.R) / 2);
									color10.B = (byte)((255 + color10.R) / 2);
								}
								int num95 = color10.R - (255 - num92);
								if (num95 < 0)
								{
									num95 = 0;
								}
								color10 = new Color((byte)num95, (byte)num95, (byte)num95, (byte)num92);
								int num96 = 0;
								int num97 = 0;
								if (num94 == 0)
								{
									num96 = -2;
								}
								if (num94 == 1)
								{
									num96 = 2;
								}
								if (num94 == 2)
								{
									num97 = -2;
								}
								if (num94 == 3)
								{
									num97 = 2;
								}
								SpriteBatch spriteBatch4 = main.spriteBatch;
								SpriteFont fontDeathText4 = Main.fontDeathText;
								string text19 = text17;
								Vector2 position4 = new Vector2(num91 + num96, num90 + num97);
								Color color11 = color10;
								float rotation4 = 0f;
								spriteBatch4.DrawString(fontDeathText4, text19, position4, color11, rotation4, default(Vector2), scale2, SpriteEffects.None, 0f);
							}
						}
					}
					if (!flag13)
					{
						main.focusColor = 0;
						main.colorDelay = 0;
					}
				}
				if (flag2)
				{
					int num98 = 400;
					string text20 = "";
					for (int num99 = 0; num99 < 4; num99++)
					{
						int num100 = num98;
						int num101 = 370 + Main.screenWidth / 2 - 400;
						if (num99 == 0)
						{
							text20 = Lang.menu[98];
						}
						if (num99 == 1)
						{
							text20 = Lang.menu[99];
							num100 += 30;
						}
						if (num99 == 2)
						{
							text20 = Math.Round(Main.soundVolume * 100f) + "%";
							num101 += 90;
						}
						if (num99 == 3)
						{
							text20 = Math.Round(Main.musicVolume * 100f) + "%";
							num101 += 90;
							num100 += 30;
						}
						for (int num102 = 0; num102 < 5; num102++)
						{
							Color color12 = Color.Black;
							if (num102 == 4)
							{
								color12 = color;
								color12.R = (byte)((255 + color12.R) / 2);
								color12.G = (byte)((255 + color12.R) / 2);
								color12.B = (byte)((255 + color12.R) / 2);
							}
							int num103 = 255;
							int num104 = color12.R - (255 - num103);
							if (num104 < 0)
							{
								num104 = 0;
							}
							color12 = new Color((byte)num104, (byte)num104, (byte)num104, (byte)num103);
							int num105 = 0;
							int num106 = 0;
							if (num102 == 0)
							{
								num105 = -2;
							}
							if (num102 == 1)
							{
								num105 = 2;
							}
							if (num102 == 2)
							{
								num106 = -2;
							}
							if (num102 == 3)
							{
								num106 = 2;
							}
							SpriteBatch spriteBatch5 = main.spriteBatch;
							SpriteFont fontDeathText5 = Main.fontDeathText;
							string text21 = text20;
							Vector2 position5 = new Vector2(num101 + num105, num100 + num106);
							Color color13 = color12;
							float rotation5 = 0f;
							spriteBatch5.DrawString(fontDeathText5, text21, position5, color13, rotation5, default(Vector2), 0.5f, SpriteEffects.None, 0f);
						}
					}
					bool flag14 = false;
					for (int num107 = 0; num107 < 2; num107++)
					{
						for (int num108 = 0; num108 < 2; num108++)
						{
							int num109 = num98 + num108 * 30 - 12;
							int num110 = 360 + Main.screenWidth / 2 - 400;
							float scale3 = 0.9f;
							if (num107 == 0)
							{
								num110 -= 70;
								num109 += 2;
							}
							else
							{
								num110 -= 40;
							}
							text20 = "-";
							if (num107 == 1)
							{
								text20 = "+";
							}
							Vector2 vector3 = new Vector2(24f, 24f);
							int num111 = 142;
							if (Main.mouseX > num110 && (float)Main.mouseX < (float)num110 + vector3.X && Main.mouseY > num109 + 13 && (float)Main.mouseY < (float)(num109 + 13) + vector3.Y)
							{
								if (main.focusColor != (num107 + 1) * (num108 + 10))
								{
									Main.PlaySound(12);
								}
								main.focusColor = (num107 + 1) * (num108 + 10);
								flag14 = true;
								num111 = 255;
								if (Main.mouseLeft)
								{
									if (main.colorDelay <= 1)
									{
										if (main.colorDelay == 0)
										{
											main.colorDelay = 40;
										}
										else
										{
											main.colorDelay = 3;
										}
										int num112 = num107;
										if (num112 == 0)
										{
											num112 = -1;
										}
										if (num108 == 0)
										{
											Main.soundVolume += (float)num112 * 0.01f;
											if (Main.soundVolume > 1f)
											{
												Main.soundVolume = 1f;
											}
											if (Main.soundVolume < 0f)
											{
												Main.soundVolume = 0f;
											}
										}
										if (num108 == 1)
										{
											Main.musicVolume += (float)num112 * 0.01f;
											if (Main.musicVolume > 1f)
											{
												Main.musicVolume = 1f;
											}
											if (Main.musicVolume < 0f)
											{
												Main.musicVolume = 0f;
											}
											Audio.Player.GameVolume = Main.musicVolume;
										}
									}
									main.colorDelay--;
								}
								else
								{
									main.colorDelay = 0;
								}
							}
							for (int num113 = 0; num113 < 5; num113++)
							{
								Color color14 = Color.Black;
								if (num113 == 4)
								{
									color14 = color;
									color14.R = (byte)((255 + color14.R) / 2);
									color14.G = (byte)((255 + color14.R) / 2);
									color14.B = (byte)((255 + color14.R) / 2);
								}
								int num114 = color14.R - (255 - num111);
								if (num114 < 0)
								{
									num114 = 0;
								}
								color14 = new Color((byte)num114, (byte)num114, (byte)num114, (byte)num111);
								int num115 = 0;
								int num116 = 0;
								if (num113 == 0)
								{
									num115 = -2;
								}
								if (num113 == 1)
								{
									num115 = 2;
								}
								if (num113 == 2)
								{
									num116 = -2;
								}
								if (num113 == 3)
								{
									num116 = 2;
								}
								SpriteBatch spriteBatch6 = main.spriteBatch;
								SpriteFont fontDeathText6 = Main.fontDeathText;
								string text22 = text20;
								Vector2 position6 = new Vector2(num110 + num115, num109 + num116);
								Color color15 = color14;
								float rotation6 = 0f;
								spriteBatch6.DrawString(fontDeathText6, text22, position6, color15, rotation6, default(Vector2), scale3, SpriteEffects.None, 0f);
							}
						}
					}
					if (!flag14)
					{
						main.focusColor = 0;
						main.colorDelay = 0;
					}
				}
				for (int num117 = 0; num117 < 5; num117++)
				{
					Color color16 = Color.Black;
					if (num117 == 4)
					{
						color16 = color;
						if (array4[num60] == 2)
						{
							color16 = Main.hcColor;
						}
						else if (array4[num60] == 1)
						{
							color16 = Main.mcColor;
						}
						color16.R = (byte)((255 + color16.R) / 2);
						color16.G = (byte)((255 + color16.G) / 2);
						color16.B = (byte)((255 + color16.B) / 2);
					}
					int num118 = (int)(255f * (main.menuItemScale[num60] * 2f - 1f));
					if (MenuActive[num60])
					{
						num118 = 255;
					}
					int num119 = color16.R - (255 - num118);
					if (num119 < 0)
					{
						num119 = 0;
					}
					int num120 = color16.G - (255 - num118);
					if (num120 < 0)
					{
						num120 = 0;
					}
					int num121 = color16.B - (255 - num118);
					if (num121 < 0)
					{
						num121 = 0;
					}
					color16 = new Color((byte)num119, (byte)num120, (byte)num121, (byte)num118);
					int num122 = 0;
					int num123 = 0;
					if (num117 == 0)
					{
						num122 = -2;
					}
					if (num117 == 1)
					{
						num122 = 2;
					}
					if (num117 == 2)
					{
						num123 = -2;
					}
					if (num117 == 3)
					{
						num123 = 2;
					}
					Vector2 origin = Main.fontDeathText.MeasureString(array7[num60]);
					origin.X *= 0.5f;
					origin.Y *= 0.5f;
					float num124 = main.menuItemScale[num60];
					if (Main.menuMode == 15 && num60 == 0)
					{
						num124 *= 0.35f;
					}
					else if (Main.netMode == 2)
					{
						num124 *= 0.5f;
					}
					num124 *= array5[num60];
					if (!array6[num60])
					{
						main.spriteBatch.DrawString(Main.fontDeathText, array7[num60], new Vector2(MenuPosX + num122 + array3[num60], (float)(MenuPosY + num2 * num60 + num123) + origin.Y * array5[num60] + (float)array2[num60]), color16, 0f, origin, num124, SpriteEffects.None, 0f);
					}
					else
					{
						main.spriteBatch.DrawString(Main.fontDeathText, array7[num60], new Vector2(MenuPosX + num122 + array3[num60], (float)(MenuPosY + num2 * num60 + num123) + origin.Y * array5[num60] + (float)array2[num60]), color16, 0f, new Vector2(0f, origin.Y), num124, SpriteEffects.None, 0f);
					}
				}
				if (flag11)
				{
					continue;
				}
				if (!array6[num60])
				{
					if (!((float)Main.mouseX > (float)MenuPosX - (float)(array7[num60].Length * 10) * array5[num60] + (float)array3[num60]) || !((float)Main.mouseX < (float)MenuPosX + (float)(array7[num60].Length * 10) * array5[num60] + (float)array3[num60]) || Main.mouseY <= MenuPosY + num2 * num60 + array2[num60] || !((float)Main.mouseY < (float)(MenuPosY + num2 * num60 + array2[num60]) + 50f * array5[num60]) || !Main.hasFocus)
					{
						continue;
					}
					main.focusMenu = num60;
					if (MenuActive[num60] || array[num60])
					{
						main.focusMenu = -1;
						continue;
					}
					if (focusMenu != main.focusMenu)
					{
						Main.PlaySound(12);
					}
					if (Main.mouseLeftRelease && Main.mouseLeft)
					{
						main.selectedMenu = num60;
					}
					if (Main.mouseRightRelease && Main.mouseRight)
					{
						main.selectedMenu2 = num60;
					}
				}
				else
				{
					if (Main.mouseX <= MenuPosX + array3[num60] || !((float)Main.mouseX < (float)MenuPosX + (float)(array7[num60].Length * 20) * array5[num60] + (float)array3[num60]) || Main.mouseY <= MenuPosY + num2 * num60 + array2[num60] || !((float)Main.mouseY < (float)(MenuPosY + num2 * num60 + array2[num60]) + 50f * array5[num60]) || !Main.hasFocus)
					{
						continue;
					}
					main.focusMenu = num60;
					if (MenuActive[num60] || array[num60])
					{
						main.focusMenu = -1;
						continue;
					}
					if (focusMenu != main.focusMenu)
					{
						Main.PlaySound(12);
					}
					if (Main.mouseLeftRelease && Main.mouseLeft)
					{
						main.selectedMenu = num60;
					}
					if (Main.mouseRightRelease && Main.mouseRight)
					{
						main.selectedMenu2 = num60;
					}
				}
			}
			for (int num125 = 0; num125 < Main.maxMenuItems; num125++)
			{
				if (num125 == main.focusMenu)
				{
					if (main.menuItemScale[num125] < 1f)
					{
						main.menuItemScale[num125] += 0.02f;
					}
					if (main.menuItemScale[num125] > 1f)
					{
						main.menuItemScale[num125] = 1f;
					}
				}
				else if ((double)main.menuItemScale[num125] > 0.8)
				{
					main.menuItemScale[num125] -= 0.02f;
				}
			}
			Codable.RunGlobalMethod("ModGeneric", "PostDrawMenu", main, main.spriteBatch);
			if (num4 >= 0)
			{
				Main.loadPlayer[num4].PlayerFrame();
				Main.loadPlayer[num4].position.X = (float)num5 + Main.screenPosition.X;
				Main.loadPlayer[num4].position.Y = (float)num6 + Main.screenPosition.Y;
				main.DrawPlayer(Main.loadPlayer[num4]);
			}
			for (int num126 = 0; num126 < 5; num126++)
			{
				Color color17 = Color.Black;
				if (num126 == 4)
				{
					color17 = color;
					color17.R = (byte)((255 + color17.R) / 2);
					color17.G = (byte)((255 + color17.R) / 2);
					color17.B = (byte)((255 + color17.R) / 2);
				}
				color17.A = (byte)((float)(int)color17.A * 0.3f);
				int num127 = 0;
				int num128 = 0;
				if (num126 == 0)
				{
					num127 = -2;
				}
				if (num126 == 1)
				{
					num127 = 2;
				}
				if (num126 == 2)
				{
					num128 = -2;
				}
				if (num126 == 3)
				{
					num128 = 2;
				}
				string text23 = "Copyright © 2012 Re-Logic";
				Vector2 origin2 = Main.fontMouseText.MeasureString(text23);
				origin2.X *= 0.5f;
				origin2.Y *= 0.5f;
				main.spriteBatch.DrawString(Main.fontMouseText, text23, new Vector2((float)Main.screenWidth - origin2.X + (float)num127 - 10f, (float)Main.screenHeight - origin2.Y + (float)num128 - 2f), color17, 0f, origin2, 1f, SpriteEffects.None, 0f);
			}
			for (int num129 = 0; num129 < 5; num129++)
			{
				Color color18 = Color.Black;
				if (num129 == 4)
				{
					color18 = color;
					color18.R = (byte)((255 + color18.R) / 2);
					color18.G = (byte)((255 + color18.R) / 2);
					color18.B = (byte)((255 + color18.R) / 2);
				}
				color18.A = (byte)((float)(int)color18.A * 0.3f);
				int num130 = 0;
				int num131 = 0;
				if (num129 == 0)
				{
					num130 = -2;
				}
				if (num129 == 1)
				{
					num130 = 2;
				}
				if (num129 == 2)
				{
					num131 = -2;
				}
				if (num129 == 3)
				{
					num131 = 2;
				}
				Vector2 origin3 = Main.fontMouseText.MeasureString(Main.versionNumber);
				origin3.X *= 0.5f;
				origin3.Y *= 0.5f;
				main.spriteBatch.DrawString(Main.fontMouseText, Main.versionNumber, new Vector2(origin3.X + (float)num130 + 10f, (float)Main.screenHeight - origin3.Y + (float)num131 - 2f), color18, 0f, origin3, 1f, SpriteEffects.None, 0f);
			}
			SpriteBatch spriteBatch7 = main.spriteBatch;
			Texture2D cursorTexture = Main.cursorTexture;
			Vector2 position7 = new Vector2(Main.mouseX + 1, Main.mouseY + 1);
			Rectangle? sourceRectangle = new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height);
			Color color19 = new Color((int)((float)(int)Main.cursorColor.R * 0.2f), (int)((float)(int)Main.cursorColor.G * 0.2f), (int)((float)(int)Main.cursorColor.B * 0.2f), (int)((float)(int)Main.cursorColor.A * 0.5f));
			float rotation7 = 0f;
			spriteBatch7.Draw(cursorTexture, position7, sourceRectangle, color19, rotation7, default(Vector2), Main.cursorScale * 1.1f, SpriteEffects.None, 0f);
			SpriteBatch spriteBatch8 = main.spriteBatch;
			Texture2D cursorTexture2 = Main.cursorTexture;
			Vector2 position8 = new Vector2(Main.mouseX, Main.mouseY);
			Rectangle? sourceRectangle2 = new Rectangle(0, 0, Main.cursorTexture.Width, Main.cursorTexture.Height);
			Color cursorColor = Main.cursorColor;
			float rotation8 = 0f;
			spriteBatch8.Draw(cursorTexture2, position8, sourceRectangle2, cursorColor, rotation8, default(Vector2), Main.cursorScale, SpriteEffects.None, 0f);
			if (Main.fadeCounter > 0)
			{
				Color white = Color.White;
				Main.fadeCounter--;
				float num132 = (float)Main.fadeCounter / 75f * 255f;
				byte b2 = (byte)num132;
				white = new Color(b2, b2, b2, b2);
				main.spriteBatch.Draw(Main.fadeTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), white);
			}
			main.spriteBatch.End();
			if (Main.mouseLeft)
			{
				Main.mouseLeftRelease = false;
			}
			else
			{
				Main.mouseLeftRelease = true;
			}
			if (Main.mouseRight)
			{
				Main.mouseRightRelease = false;
			}
			else
			{
				Main.mouseRightRelease = true;
			}
		}
	}
}
