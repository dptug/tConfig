using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Terraria
{
	public class messageBuffer
	{
		public const int readBufferMax = 65535;

		public const int writeBufferMax = 65535;

		public bool broadcast;

		public byte[] readBuffer = new byte[65535];

		public byte[] writeBuffer = new byte[65535];

		public bool writeLocked;

		public int messageLength;

		public int totalData;

		public int whoAmI;

		public int spamCount;

		public int maxSpam;

		public bool checkBytes;

		public void Reset()
		{
			writeBuffer = new byte[65535];
			writeLocked = false;
			messageLength = 0;
			totalData = 0;
			spamCount = 0;
			broadcast = false;
			checkBytes = false;
		}

		public void ProcessMessage(int b, ref int num, int start, int length)
		{
			if (b == 1 && Main.netMode == 2)
			{
				if (Main.dedServ && Netplay.CheckBan(Netplay.serverSock[whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
				{
					NetMessage.SendData(2, whoAmI, -1, Lang.mp[3]);
				}
				else
				{
					if (Netplay.serverSock[whoAmI].state != 0)
					{
						return;
					}
					string @string = Encoding.UTF8.GetString(readBuffer, start + 1, length - 1);
					if (!@string.Contains($"Terraria{Main.curRelease} {Constants.version} "))
					{
						NetMessage.SendData(2, whoAmI, -1, Lang.mp[4]);
						return;
					}
					Config.modConfirmed[whoAmI] = false;
					if (@string == $"Terraria{Main.curRelease} {Constants.version} {Config.tConfigHash}")
					{
						Config.modConfirmed[whoAmI] = true;
					}
					if (string.IsNullOrEmpty(Netplay.password))
					{
						Netplay.serverSock[whoAmI].state = 1;
						Config.PerformModTransfer(whoAmI);
					}
					else
					{
						Netplay.serverSock[whoAmI].state = -1;
						NetMessage.SendData(37, whoAmI);
					}
				}
				return;
			}
			if (b == 2 && Main.netMode == 1)
			{
				Netplay.disconnect = true;
				Main.statusText = Encoding.UTF8.GetString(readBuffer, start + 1, length - 1);
				return;
			}
			if (b == 3 && Main.dedServ)
			{
				NetMessage.SendData(3, whoAmI);
				return;
			}
			if (b == 3 && Main.netMode == 1)
			{
				MemoryStream memoryStream = new MemoryStream(readBuffer, start + 1, length - 1);
				BinaryReader recvreader = new BinaryReader(memoryStream);
				if (Config.ReceiveModPart((int)memoryStream.Length, recvreader))
				{
					NetMessage.SendData(103, whoAmI);
					Main.statusText = "Next step..?";
					Config.modTransferStarted = false;
					return;
				}
				NetMessage.SendData(3, whoAmI);
				int num2 = 0;
				for (int i = 0; i < Config.modPartsTransferred.Length; i++)
				{
					num2 += Config.modPartsTransferred[i];
				}
				Main.statusText = "Receiving mod pieces " + num2 + "/" + Config.modPartsTotal;
				return;
			}
			if (b == 103 && Main.netMode == 2)
			{
				NetMessage.SendData(103, whoAmI);
				return;
			}
			if (b == 103 && Main.netMode == 1)
			{
				if (Netplay.clientSock.state == 1)
				{
					Netplay.clientSock.state = 2;
				}
				int num3 = readBuffer[start + 1];
				if (num3 != Main.myPlayer)
				{
					Main.player[num3] = (Player)Main.player[Main.myPlayer].Clone();
					Main.player[Main.myPlayer] = new Player();
					Main.player[num3].whoAmi = num3;
					Main.myPlayer = num3;
				}
				NetMessage.SendData(4, -1, -1, Main.player[Main.myPlayer].name, Main.myPlayer);
				NetMessage.SendData(16, -1, -1, "", Main.myPlayer);
				NetMessage.SendData(42, -1, -1, "", Main.myPlayer);
				NetMessage.SendData(50, -1, -1, "", Main.myPlayer);
				for (int j = 0; j < 49; j++)
				{
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[j].name, Main.myPlayer, j, (int)Main.player[Main.myPlayer].inventory[j].prefix);
				}
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 49f, (int)Main.player[Main.myPlayer].armor[0].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 50f, (int)Main.player[Main.myPlayer].armor[1].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 51f, (int)Main.player[Main.myPlayer].armor[2].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 52f, (int)Main.player[Main.myPlayer].armor[3].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 53f, (int)Main.player[Main.myPlayer].armor[4].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 54f, (int)Main.player[Main.myPlayer].armor[5].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 55f, (int)Main.player[Main.myPlayer].armor[6].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 56f, (int)Main.player[Main.myPlayer].armor[7].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 57f, (int)Main.player[Main.myPlayer].armor[8].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 58f, (int)Main.player[Main.myPlayer].armor[9].prefix);
				NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 59f, (int)Main.player[Main.myPlayer].armor[10].prefix);
				NetMessage.SendData(6);
				if (Netplay.clientSock.state == 2)
				{
					Netplay.clientSock.state = 3;
				}
				return;
			}
			switch (b)
			{
			case 4:
			{
				bool flag = false;
				int num13 = readBuffer[start + 1];
				if (Main.netMode == 2)
				{
					num13 = whoAmI;
				}
				if (num13 == Main.myPlayer)
				{
					return;
				}
				int num14 = readBuffer[start + 2];
				if (num14 >= 36)
				{
					num14 = 0;
				}
				Main.player[num13].hair = num14;
				Main.player[num13].whoAmi = num13;
				num += 2;
				byte b5 = readBuffer[num];
				num++;
				if (b5 == 1)
				{
					Main.player[num13].male = true;
				}
				else
				{
					Main.player[num13].male = false;
				}
				Main.player[num13].hairColor.R = readBuffer[num];
				num++;
				Main.player[num13].hairColor.G = readBuffer[num];
				num++;
				Main.player[num13].hairColor.B = readBuffer[num];
				num++;
				Main.player[num13].skinColor.R = readBuffer[num];
				num++;
				Main.player[num13].skinColor.G = readBuffer[num];
				num++;
				Main.player[num13].skinColor.B = readBuffer[num];
				num++;
				Main.player[num13].eyeColor.R = readBuffer[num];
				num++;
				Main.player[num13].eyeColor.G = readBuffer[num];
				num++;
				Main.player[num13].eyeColor.B = readBuffer[num];
				num++;
				Main.player[num13].shirtColor.R = readBuffer[num];
				num++;
				Main.player[num13].shirtColor.G = readBuffer[num];
				num++;
				Main.player[num13].shirtColor.B = readBuffer[num];
				num++;
				Main.player[num13].underShirtColor.R = readBuffer[num];
				num++;
				Main.player[num13].underShirtColor.G = readBuffer[num];
				num++;
				Main.player[num13].underShirtColor.B = readBuffer[num];
				num++;
				Main.player[num13].pantsColor.R = readBuffer[num];
				num++;
				Main.player[num13].pantsColor.G = readBuffer[num];
				num++;
				Main.player[num13].pantsColor.B = readBuffer[num];
				num++;
				Main.player[num13].shoeColor.R = readBuffer[num];
				num++;
				Main.player[num13].shoeColor.G = readBuffer[num];
				num++;
				Main.player[num13].shoeColor.B = readBuffer[num];
				num++;
				byte difficulty = readBuffer[num];
				Main.player[num13].difficulty = difficulty;
				num++;
				string string2 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
				string2 = string2.Trim();
				Main.player[num13].name = string2.Trim();
				if (Main.netMode != 2)
				{
					return;
				}
				if (Netplay.serverSock[whoAmI].state < 10)
				{
					for (int l = 0; l < 255; l++)
					{
						if (l != num13 && string2 == Main.player[l].name && Netplay.serverSock[l].active)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					NetMessage.SendData(2, whoAmI, -1, string2 + " " + Lang.mp[5]);
					return;
				}
				if (string2.Length > Player.nameLen)
				{
					NetMessage.SendData(2, whoAmI, -1, "Name is too long.");
					return;
				}
				if (string2 == "")
				{
					NetMessage.SendData(2, whoAmI, -1, "Empty name.");
					return;
				}
				Netplay.serverSock[whoAmI].oldName = string2;
				Netplay.serverSock[whoAmI].name = string2;
				NetMessage.SendData(4, -1, whoAmI, string2, num13);
				return;
			}
			case 5:
			{
				MemoryStream memoryStream3 = new MemoryStream(readBuffer);
				memoryStream3.Position = start + 1;
				BinaryReader binaryReader2 = new BinaryReader(memoryStream3);
				int num17 = binaryReader2.ReadByte();
				if (Main.netMode == 2)
				{
					num17 = whoAmI;
				}
				if (num17 != Main.myPlayer)
				{
					lock (Main.player[num17])
					{
						int num18 = binaryReader2.ReadByte();
						int stack = binaryReader2.ReadInt32();
						byte b6 = binaryReader2.ReadByte();
						int type2 = binaryReader2.ReadInt16();
						if (num18 < 49)
						{
							Main.player[num17].inventory[num18] = new Item();
							Main.player[num17].inventory[num18].netDefaults(type2);
							Main.player[num17].inventory[num18].stack = stack;
							Main.player[num17].inventory[num18].Prefix(b6);
							Item item = Main.player[num17].inventory[num18];
							if (!string.IsNullOrEmpty(item.name))
							{
								Codable.LoadCustomDataNew(item, binaryReader2, 4, Config.GetCurrentVersion(item));
							}
						}
						else
						{
							Main.player[num17].armor[num18 - 48 - 1] = new Item();
							Main.player[num17].armor[num18 - 48 - 1].netDefaults(type2);
							Main.player[num17].armor[num18 - 48 - 1].stack = stack;
							Main.player[num17].armor[num18 - 48 - 1].Prefix(b6);
							Item item2 = Main.player[num17].armor[num18 - 48 - 1];
							if (!string.IsNullOrEmpty(item2.name))
							{
								Codable.LoadCustomDataNew(item2, binaryReader2, 4, Config.GetCurrentVersion(item2));
							}
						}
						if (Main.netMode == 2 && num17 == whoAmI)
						{
							NetMessage.SendData(5, -1, whoAmI, "", num17, num18, (int)b6);
						}
						binaryReader2.Close();
					}
				}
				return;
			}
			case 6:
				if (Main.netMode == 2)
				{
					if (Netplay.serverSock[whoAmI].state == 1)
					{
						Netplay.serverSock[whoAmI].state = 2;
					}
					NetMessage.SendData(7, whoAmI);
				}
				return;
			case 7:
				if (Main.netMode == 1)
				{
					Main.time = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.dayTime = false;
					if (readBuffer[num] == 1)
					{
						Main.dayTime = true;
					}
					num++;
					Main.moonPhase = readBuffer[num];
					num++;
					int num10 = readBuffer[num];
					num++;
					if (num10 == 1)
					{
						Main.bloodMoon = true;
					}
					else
					{
						Main.bloodMoon = false;
					}
					Main.maxTilesX = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.maxTilesY = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.spawnTileX = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.spawnTileY = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.worldSurface = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.rockLayer = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.hellLayer = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					Main.worldID = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					byte b4 = readBuffer[num];
					if ((b4 & 1) == 1)
					{
						WorldGen.shadowOrbSmashed = true;
					}
					if ((b4 & 2) == 2)
					{
						NPC.downedBoss1 = true;
					}
					if ((b4 & 4) == 4)
					{
						NPC.downedBoss2 = true;
					}
					if ((b4 & 8) == 8)
					{
						NPC.downedBoss3 = true;
					}
					if ((b4 & 0x10) == 16)
					{
						Main.hardMode = true;
					}
					if ((b4 & 0x20) == 32)
					{
						NPC.downedClown = true;
					}
					num++;
					int num11 = readBuffer[num];
					num++;
					Main.worldName = Encoding.UTF8.GetString(readBuffer, num, num11);
					num += num11;
					Config.randSeed = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					using (MemoryStream memoryStream2 = new MemoryStream(readBuffer, num, readBuffer.Length - num))
					{
						using (BinaryReader binaryReader = new BinaryReader(memoryStream2))
						{
							long position = memoryStream2.Position;
							int num12 = binaryReader.ReadByte();
							for (int k = 0; k < num12; k++)
							{
								string key = binaryReader.ReadString();
								string json = binaryReader.ReadString();
								JsonData jsonData = JsonMapper.ToObject(json);
								foreach (KeyValuePair<string, JsonData> item5 in (IEnumerable)jsonData)
								{
									Config.jsonCurrent[key]["settings"][item5.Key]["value"] = item5.Value;
								}
							}
							long position2 = memoryStream2.Position;
							num += (int)(position2 - position);
						}
					}
					if (Netplay.clientSock.state == 3)
					{
						Config.syncedRand = new Random(Config.randSeed);
						Config.InitializeGlobalMod("ModWorld");
						Netplay.clientSock.state = 4;
					}
				}
				return;
			case 100:
			{
				int index = readBuffer[num];
				num++;
				int num15 = readBuffer[num];
				num++;
				object value = null;
				if (Config.globalMod["ModWorld"].TryGetValue(Config.mods[index], out value))
				{
					Codable.RunSpecifiedMethod("ModWorld " + Config.mods[index] + "NetReceive", value, "NetReceive", num15, new BinaryReader(new MemoryStream(readBuffer, num, readBuffer.Length - num)));
				}
				return;
			}
			case 8:
			{
				if (Main.netMode != 2)
				{
					return;
				}
				int num19 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				int num20 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				bool flag2 = true;
				if (num19 == -1 || num20 == -1)
				{
					flag2 = false;
				}
				else if (num19 < 10 || num19 > Main.maxTilesX - 10)
				{
					flag2 = false;
				}
				else if (num20 < 10 || num20 > Main.maxTilesY - 10)
				{
					flag2 = false;
				}
				int num21 = 1350;
				if (flag2)
				{
					num21 *= 2;
				}
				if (Netplay.serverSock[whoAmI].state == 2)
				{
					Netplay.serverSock[whoAmI].state = 3;
				}
				NetMessage.SendData(9, whoAmI, -1, Lang.inter[44], num21);
				Netplay.serverSock[whoAmI].statusText2 = "is receiving tile data";
				Netplay.serverSock[whoAmI].statusMax += num21;
				int sectionX = Netplay.GetSectionX(Main.spawnTileX);
				int sectionY = Netplay.GetSectionY(Main.spawnTileY);
				for (int m = sectionX - 2; m < sectionX + 3; m++)
				{
					for (int n = sectionY - 1; n < sectionY + 2; n++)
					{
						NetMessage.SendSection(whoAmI, m, n);
					}
				}
				if (flag2)
				{
					num19 = Netplay.GetSectionX(num19);
					num20 = Netplay.GetSectionY(num20);
					for (int num22 = num19 - 2; num22 < num19 + 3; num22++)
					{
						for (int num23 = num20 - 1; num23 < num20 + 2; num23++)
						{
							NetMessage.SendSection(whoAmI, num22, num23);
						}
					}
					NetMessage.SendData(11, whoAmI, -1, "", num19 - 2, num20 - 1, num19 + 2, num20 + 1);
				}
				NetMessage.SendData(11, whoAmI, -1, "", sectionX - 2, sectionY - 1, sectionX + 2, sectionY + 1);
				for (int num24 = 0; num24 < 200; num24++)
				{
					if (Main.item[num24].active)
					{
						NetMessage.SendData(21, whoAmI, -1, "", num24);
						NetMessage.SendData(22, whoAmI, -1, "", num24);
					}
				}
				for (int num25 = 0; num25 < 200; num25++)
				{
					if (Main.npc[num25].active)
					{
						NetMessage.SendData(23, whoAmI, -1, "", num25);
					}
				}
				NetMessage.SendData(49, whoAmI);
				NetMessage.SendData(57, whoAmI);
				NetMessage.SendData(56, whoAmI, -1, "", 17);
				NetMessage.SendData(56, whoAmI, -1, "", 18);
				NetMessage.SendData(56, whoAmI, -1, "", 19);
				NetMessage.SendData(56, whoAmI, -1, "", 20);
				NetMessage.SendData(56, whoAmI, -1, "", 22);
				NetMessage.SendData(56, whoAmI, -1, "", 38);
				NetMessage.SendData(56, whoAmI, -1, "", 54);
				NetMessage.SendData(56, whoAmI, -1, "", 107);
				NetMessage.SendData(56, whoAmI, -1, "", 108);
				NetMessage.SendData(56, whoAmI, -1, "", 124);
				for (int num26 = 147; num26 < 147 + Config.customNPCAmt; num26++)
				{
					if (!string.IsNullOrEmpty(Main.chrName[num26]))
					{
						NetMessage.SendData(56, whoAmI, -1, "", num26);
					}
				}
				return;
			}
			case 9:
				if (Main.netMode == 1)
				{
					int num16 = BitConverter.ToInt32(readBuffer, start + 1);
					string string3 = Encoding.UTF8.GetString(readBuffer, start + 5, length - 5);
					Netplay.clientSock.statusMax += num16;
					Netplay.clientSock.statusText = string3;
				}
				return;
			case 10:
			{
				if (Main.netMode != 1)
				{
					break;
				}
				short num4 = BitConverter.ToInt16(readBuffer, start + 1);
				int num5 = BitConverter.ToInt32(readBuffer, start + 3);
				int num6 = BitConverter.ToInt32(readBuffer, start + 7);
				num = start + 11;
				int num7;
				for (num7 = num5; num7 < num5 + num4; num7++)
				{
					if (Main.tile[num7, num6] == null)
					{
						Main.tile[num7, num6] = new Tile();
					}
					byte b2 = readBuffer[num];
					num++;
					bool active = Main.tile[num7, num6].active;
					if ((b2 & 1) == 1)
					{
						Main.tile[num7, num6].active = true;
					}
					else
					{
						Main.tile[num7, num6].active = false;
					}
					if ((b2 & 4) == 4)
					{
						Main.tile[num7, num6].wall = 1;
					}
					else
					{
						Main.tile[num7, num6].wall = 0;
					}
					if ((b2 & 8) == 8)
					{
						Main.tile[num7, num6].liquid = 1;
					}
					else
					{
						Main.tile[num7, num6].liquid = 0;
					}
					if ((b2 & 0x10) == 16)
					{
						Main.tile[num7, num6].wire = true;
					}
					else
					{
						Main.tile[num7, num6].wire = false;
					}
					if (Main.tile[num7, num6].active)
					{
						int type = Main.tile[num7, num6].type;
						Main.tile[num7, num6].type = (ushort)BitConverter.ToInt16(readBuffer, num);
						num += 2;
						if (Main.tileFrameImportant[Main.tile[num7, num6].type])
						{
							Main.tile[num7, num6].frameX = BitConverter.ToInt16(readBuffer, num);
							num += 2;
							Main.tile[num7, num6].frameY = BitConverter.ToInt16(readBuffer, num);
							num += 2;
							if (Main.tile[num7, num6].type >= 150)
							{
								Main.tile[num7, num6].frameNumber = readBuffer[num];
								num++;
							}
						}
						else if (!active || Main.tile[num7, num6].type != type)
						{
							Main.tile[num7, num6].frameX = -1;
							Main.tile[num7, num6].frameY = -1;
						}
					}
					if (Main.tile[num7, num6].wall > 0)
					{
						Main.tile[num7, num6].wall = (ushort)BitConverter.ToInt16(readBuffer, num);
						num += 2;
					}
					if (Main.tile[num7, num6].liquid > 0)
					{
						Main.tile[num7, num6].liquid = readBuffer[num];
						num++;
						byte b3 = readBuffer[num];
						num++;
						if (b3 == 1)
						{
							Main.tile[num7, num6].lava = true;
						}
						else
						{
							Main.tile[num7, num6].lava = false;
						}
					}
					if (Config.tileDefs.assemblyByType[Main.tile[num7, num6].type] != null)
					{
						Codable.InitTile(new Vector2(num7, num6), Main.tile[num7, num6].type);
					}
					short num8 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					int num9 = num7;
					while (num8 > 0)
					{
						num9++;
						num8 = (short)(num8 - 1);
						if (Main.tile[num9, num6] == null)
						{
							Main.tile[num9, num6] = new Tile();
						}
						Main.tile[num9, num6].active = Main.tile[num7, num6].active;
						Main.tile[num9, num6].type = Main.tile[num7, num6].type;
						Main.tile[num9, num6].wall = Main.tile[num7, num6].wall;
						Main.tile[num9, num6].wire = Main.tile[num7, num6].wire;
						if (Main.tileFrameImportant[Main.tile[num9, num6].type])
						{
							Main.tile[num9, num6].frameX = Main.tile[num7, num6].frameX;
							Main.tile[num9, num6].frameY = Main.tile[num7, num6].frameY;
							Main.tile[num9, num6].frameNumber = Main.tile[num7, num6].frameNumber;
						}
						else
						{
							Main.tile[num9, num6].frameX = -1;
							Main.tile[num9, num6].frameY = -1;
						}
						Main.tile[num9, num6].liquid = Main.tile[num7, num6].liquid;
						Main.tile[num9, num6].lava = Main.tile[num7, num6].lava;
					}
					num7 = num9;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(b, -1, whoAmI, "", num4, num5, num6);
				}
				return;
			}
			}
			if (b == 11)
			{
				if (Main.netMode == 1)
				{
					int startX = BitConverter.ToInt16(readBuffer, num);
					num += 4;
					int startY = BitConverter.ToInt16(readBuffer, num);
					num += 4;
					int endX = BitConverter.ToInt16(readBuffer, num);
					num += 4;
					int endY = BitConverter.ToInt16(readBuffer, num);
					num += 4;
					WorldGen.SectionTileFrame(startX, startY, endX, endY);
				}
			}
			else if (b == 12)
			{
				int num27 = readBuffer[num];
				if (Main.netMode == 2)
				{
					num27 = whoAmI;
				}
				num++;
				Main.player[num27].SpawnX = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				Main.player[num27].SpawnY = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				Main.player[num27].Spawn();
				if (Main.netMode == 2 && Netplay.serverSock[whoAmI].state >= 3)
				{
					if (Netplay.serverSock[whoAmI].state == 3)
					{
						Netplay.serverSock[whoAmI].state = 10;
						NetMessage.greetPlayer(whoAmI);
						NetMessage.buffer[whoAmI].broadcast = true;
						NetMessage.syncPlayers();
						NetMessage.SendData(12, -1, whoAmI, "", whoAmI);
						Codable.RunGlobalMethod("ModWorld", "PlayerConnected", num27);
					}
					else
					{
						NetMessage.SendData(12, -1, whoAmI, "", whoAmI);
					}
				}
			}
			else if (b == 13)
			{
				int num28 = readBuffer[num];
				if (num28 != Main.myPlayer)
				{
					if (Main.netMode == 1)
					{
						_ = Main.player[num28].active;
					}
					if (Main.netMode == 2)
					{
						num28 = whoAmI;
					}
					num++;
					int num29 = readBuffer[num];
					num++;
					int selectedItem = readBuffer[num];
					num++;
					float x = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float num30 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float x2 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float y = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					Main.player[num28].selectedItem = selectedItem;
					Main.player[num28].position.X = x;
					Main.player[num28].position.Y = num30;
					Main.player[num28].velocity.X = x2;
					Main.player[num28].velocity.Y = y;
					Main.player[num28].oldVelocity = Main.player[num28].velocity;
					Main.player[num28].fallStart = (int)(num30 / 16f);
					Main.player[num28].controlUp = false;
					Main.player[num28].controlDown = false;
					Main.player[num28].controlLeft = false;
					Main.player[num28].controlRight = false;
					Main.player[num28].controlJump = false;
					Main.player[num28].controlUseItem = false;
					Main.player[num28].direction = -1;
					if ((num29 & 1) == 1)
					{
						Main.player[num28].controlUp = true;
					}
					if ((num29 & 2) == 2)
					{
						Main.player[num28].controlDown = true;
					}
					if ((num29 & 4) == 4)
					{
						Main.player[num28].controlLeft = true;
					}
					if ((num29 & 8) == 8)
					{
						Main.player[num28].controlRight = true;
					}
					if ((num29 & 0x10) == 16)
					{
						Main.player[num28].controlJump = true;
					}
					if ((num29 & 0x20) == 32)
					{
						Main.player[num28].controlUseItem = true;
					}
					if ((num29 & 0x40) == 64)
					{
						Main.player[num28].direction = 1;
					}
					if (Main.netMode == 2 && Netplay.serverSock[whoAmI].state == 10)
					{
						NetMessage.SendData(13, -1, whoAmI, "", num28);
					}
				}
			}
			else if (b == 14)
			{
				if (Main.netMode != 1)
				{
					return;
				}
				int num31 = readBuffer[num];
				num++;
				int num32 = readBuffer[num];
				if (num32 == 1)
				{
					if (!Main.player[num31].active)
					{
						Main.player[num31] = new Player();
					}
					Main.player[num31].active = true;
				}
				else
				{
					Main.player[num31].active = false;
				}
			}
			else if (b == 15)
			{
				if (Main.netMode != 2)
				{
				}
			}
			else if (b == 16)
			{
				int num33 = readBuffer[num];
				num++;
				if (num33 != Main.myPlayer)
				{
					int statLife = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					int statLifeMax = BitConverter.ToInt16(readBuffer, num);
					if (Main.netMode == 2)
					{
						num33 = whoAmI;
					}
					Main.player[num33].statLife = statLife;
					Main.player[num33].statLifeMax = statLifeMax;
					if (Main.player[num33].statLife <= 0)
					{
						Main.player[num33].dead = true;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(16, -1, whoAmI, "", num33);
					}
				}
			}
			else if (b == 17)
			{
				byte b7 = readBuffer[num];
				num++;
				int num34 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				int num35 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				ushort num36 = BitConverter.ToUInt16(readBuffer, num);
				num += 2;
				int num37 = readBuffer[num];
				bool flag3 = false;
				if (num36 == 1)
				{
					flag3 = true;
				}
				if (Main.tile[num34, num35] == null)
				{
					Main.tile[num34, num35] = new Tile();
				}
				if (Main.netMode == 2)
				{
					if (!flag3)
					{
						switch (b7)
						{
						case 0:
						case 2:
						case 4:
							Netplay.serverSock[whoAmI].spamDelBlock += 1f;
							break;
						case 1:
						case 3:
							Netplay.serverSock[whoAmI].spamAddBlock += 1f;
							break;
						}
					}
					if (!Netplay.serverSock[whoAmI].tileSection[Netplay.GetSectionX(num34), Netplay.GetSectionY(num35)])
					{
						flag3 = true;
					}
				}
				switch (b7)
				{
				case 0:
					WorldGen.KillTile(num34, num35, flag3);
					break;
				case 1:
					WorldGen.PlaceTile(num34, num35, num36, mute: false, forced: true, -1, num37);
					break;
				case 2:
					WorldGen.KillWall(num34, num35, flag3);
					break;
				case 3:
					WorldGen.PlaceWall(num34, num35, num36);
					break;
				case 4:
					WorldGen.KillTile(num34, num35, flag3, effectOnly: false, noItem: true);
					break;
				case 5:
					WorldGen.PlaceWire(num34, num35);
					break;
				case 6:
					WorldGen.KillWire(num34, num35);
					break;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(17, -1, whoAmI, "", b7, num34, num35, (int)num36, num37);
					if (b7 == 1 && num36 == 53)
					{
						NetMessage.SendTileSquare(-1, num34, num35, 1);
					}
				}
			}
			else if (b == 18)
			{
				if (Main.netMode == 1)
				{
					byte b8 = readBuffer[num];
					num++;
					int num38 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					short sunModY = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					short moonModY = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (b8 == 1)
					{
						Main.dayTime = true;
					}
					else
					{
						Main.dayTime = false;
					}
					Main.time = num38;
					Main.sunModY = sunModY;
					Main.moonModY = moonModY;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(18, -1, whoAmI);
					}
				}
			}
			else if (b != 19)
			{
				switch (b)
				{
				case 20:
				{
					short num44 = BitConverter.ToInt16(readBuffer, start + 1);
					int num45 = BitConverter.ToInt32(readBuffer, start + 3);
					int num46 = BitConverter.ToInt32(readBuffer, start + 7);
					num = start + 11;
					if (Main.netMode == 2)
					{
						try
						{
							if (num44 > 10)
							{
								return;
							}
							int num47 = num45 + num44 / 2;
							int num48 = num46;
							int num49 = (int)(Main.player[whoAmI].position.X + (float)(Main.player[whoAmI].width / 2)) / 16;
							int num50 = (int)(Main.player[whoAmI].position.Y + (float)(Main.player[whoAmI].height / 2)) / 16;
							if (Math.Abs(num47 - num49) > 20 || Math.Abs(num48 - num50) > 20)
							{
								return;
							}
						}
						catch
						{
							return;
						}
					}
					for (int num51 = num45; num51 < num45 + num44; num51++)
					{
						for (int num52 = num46; num52 < num46 + num44; num52++)
						{
							if (Main.tile[num51, num52] == null)
							{
								Main.tile[num51, num52] = new Tile();
							}
							byte b9 = readBuffer[num];
							num++;
							bool active2 = Main.tile[num51, num52].active;
							if ((b9 & 1) == 1)
							{
								Main.tile[num51, num52].active = true;
							}
							else
							{
								Main.tile[num51, num52].active = false;
							}
							if ((b9 & 4) == 4)
							{
								Main.tile[num51, num52].wall = 1;
							}
							else
							{
								Main.tile[num51, num52].wall = 0;
							}
							bool flag4 = false;
							if ((b9 & 8) == 8)
							{
								flag4 = true;
							}
							if (Main.netMode != 2)
							{
								if (flag4)
								{
									Main.tile[num51, num52].liquid = 1;
								}
								else
								{
									Main.tile[num51, num52].liquid = 0;
								}
							}
							if ((b9 & 0x10) == 16)
							{
								Main.tile[num51, num52].wire = true;
							}
							else
							{
								Main.tile[num51, num52].wire = false;
							}
							if (Main.tile[num51, num52].active)
							{
								int type3 = Main.tile[num51, num52].type;
								Main.tile[num51, num52].type = (ushort)BitConverter.ToInt16(readBuffer, num);
								num += 2;
								if (Main.tileFrameImportant[Main.tile[num51, num52].type])
								{
									Main.tile[num51, num52].frameX = BitConverter.ToInt16(readBuffer, num);
									num += 2;
									Main.tile[num51, num52].frameY = BitConverter.ToInt16(readBuffer, num);
									num += 2;
									if (Main.tile[num51, num52].type >= 150)
									{
										Main.tile[num51, num52].frameNumber = readBuffer[num];
										num++;
									}
								}
								else if (!active2 || Main.tile[num51, num52].type != type3)
								{
									Main.tile[num51, num52].frameX = -1;
									Main.tile[num51, num52].frameY = -1;
								}
							}
							if (Main.tile[num51, num52].wall > 0)
							{
								Main.tile[num51, num52].wall = (ushort)BitConverter.ToInt16(readBuffer, num);
								num += 2;
							}
							if (!flag4)
							{
								continue;
							}
							Main.tile[num51, num52].liquid = readBuffer[num];
							num++;
							byte b10 = readBuffer[num];
							num++;
							if (Main.netMode != 2)
							{
								if (b10 == 1)
								{
									Main.tile[num51, num52].lava = true;
								}
								else
								{
									Main.tile[num51, num52].lava = false;
								}
							}
						}
					}
					WorldGen.RangeFrame(num45, num46, num45 + num44, num46 + num44);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(b, -1, whoAmI, "", num44, num45, num46);
					}
					return;
				}
				case 21:
				{
					short num54 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					float num55 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float num56 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float x5 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float y4 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					int stack2 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					byte pre = readBuffer[num];
					num++;
					short num57 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (Main.netMode == 1)
					{
						if (num57 == 0)
						{
							Main.item[num54].active = false;
							return;
						}
						Main.item[num54].netDefaults(num57);
						Main.item[num54].Prefix(pre);
						Main.item[num54].stack = stack2;
						Main.item[num54].position.X = num55;
						Main.item[num54].position.Y = num56;
						Main.item[num54].velocity.X = x5;
						Main.item[num54].velocity.Y = y4;
						Main.item[num54].active = true;
						Main.item[num54].wet = Collision.WetCollision(Main.item[num54].position, Main.item[num54].width, Main.item[num54].height);
						Item item3 = Main.item[num54];
						if (!string.IsNullOrEmpty(item3.name))
						{
							Codable.LoadCustomDataNew(item3, new BinaryReader(new MemoryStream(readBuffer, num, readBuffer.Length - num)), 4, Config.GetCurrentVersion(item3));
						}
						return;
					}
					if (num57 == 0)
					{
						if (num54 < 200)
						{
							Main.item[num54].active = false;
							NetMessage.SendData(21, -1, -1, "", num54);
						}
						return;
					}
					bool flag5 = false;
					if (num54 == 200)
					{
						flag5 = true;
					}
					if (flag5)
					{
						Item item4 = new Item();
						item4.netDefaults(num57);
						num54 = (short)Item.NewItem((int)num55, (int)num56, item4.width, item4.height, item4.type, stack2, noBroadcast: true);
					}
					Main.item[num54].netDefaults(num57);
					Main.item[num54].Prefix(pre);
					Main.item[num54].stack = stack2;
					Main.item[num54].position.X = num55;
					Main.item[num54].position.Y = num56;
					Main.item[num54].velocity.X = x5;
					Main.item[num54].velocity.Y = y4;
					Main.item[num54].active = true;
					Main.item[num54].owner = Main.myPlayer;
					if (!string.IsNullOrEmpty(Main.item[num54].name))
					{
						Codable.LoadCustomDataNew(Main.item[num54], new BinaryReader(new MemoryStream(readBuffer, num, readBuffer.Length - num)), 4, Config.GetCurrentVersion(Main.item[num54]));
					}
					if (flag5)
					{
						NetMessage.SendData(21, -1, -1, "", num54);
						Main.item[num54].ownIgnore = whoAmI;
						Main.item[num54].ownTime = 100;
						Main.item[num54].FindOwner(num54);
					}
					else
					{
						NetMessage.SendData(21, -1, whoAmI, "", num54);
					}
					return;
				}
				case 22:
				{
					short num53 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte b11 = readBuffer[num];
					if (Main.netMode != 2 || Main.item[num53].owner == whoAmI)
					{
						Main.item[num53].owner = b11;
						if (b11 == Main.myPlayer)
						{
							Main.item[num53].keepTime = 15;
						}
						else
						{
							Main.item[num53].keepTime = 0;
						}
						if (Main.netMode == 2)
						{
							Main.item[num53].owner = 255;
							Main.item[num53].keepTime = 15;
							NetMessage.SendData(22, -1, -1, "", num53);
						}
					}
					return;
				}
				case 23:
					if (Main.netMode == 1)
					{
						short num39 = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						float x3 = BitConverter.ToSingle(readBuffer, num);
						num += 4;
						float y2 = BitConverter.ToSingle(readBuffer, num);
						num += 4;
						float x4 = BitConverter.ToSingle(readBuffer, num);
						num += 4;
						float y3 = BitConverter.ToSingle(readBuffer, num);
						num += 4;
						int target = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						int direction = readBuffer[num] - 1;
						num++;
						int directionY = readBuffer[num] - 1;
						num++;
						int num40 = BitConverter.ToInt32(readBuffer, num);
						num += 4;
						float[] array = new float[NPC.maxAI];
						for (int num41 = 0; num41 < NPC.maxAI; num41++)
						{
							array[num41] = BitConverter.ToSingle(readBuffer, num);
							num += 4;
						}
						int num42 = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						if (!Main.npc[num39].active || Main.npc[num39].netID != num42)
						{
							Main.npc[num39].active = true;
							Main.npc[num39].netDefaults(num42);
						}
						Main.npc[num39].position.X = x3;
						Main.npc[num39].position.Y = y2;
						Main.npc[num39].velocity.X = x4;
						Main.npc[num39].velocity.Y = y3;
						Main.npc[num39].target = target;
						Main.npc[num39].direction = direction;
						Main.npc[num39].directionY = directionY;
						Main.npc[num39].life = num40;
						Main.npc[num39].whoAmI = num39;
						if (num40 <= 0)
						{
							Main.npc[num39].active = false;
						}
						for (int num43 = 0; num43 < NPC.maxAI; num43++)
						{
							Main.npc[num39].ai[num43] = array[num43];
						}
						return;
					}
					break;
				}
				switch (b)
				{
				case 24:
				{
					short num69 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte b13 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b13 = (byte)whoAmI;
					}
					Main.npc[num69].StrikeNPC(Main.player[b13].inventory[Main.player[b13].selectedItem].damage, Main.player[b13].inventory[Main.player[b13].selectedItem].knockBack, Main.player[b13].direction);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(24, -1, whoAmI, "", num69, (int)b13);
						NetMessage.SendData(23, -1, -1, "", num69);
					}
					return;
				}
				case 25:
				{
					int num72 = readBuffer[start + 1];
					if (Main.netMode == 2)
					{
						num72 = whoAmI;
					}
					byte b17 = readBuffer[start + 2];
					byte b18 = readBuffer[start + 3];
					byte b19 = readBuffer[start + 4];
					if (Main.netMode == 2)
					{
						b17 = byte.MaxValue;
						b18 = byte.MaxValue;
						b19 = byte.MaxValue;
					}
					string string5 = Encoding.UTF8.GetString(readBuffer, start + 5, length - 5);
					if (Main.netMode == 1)
					{
						string newText = string5;
						if (num72 < 255)
						{
							newText = "<" + Main.player[num72].name + "> " + string5;
							Main.player[num72].chatText = string5;
							Main.player[num72].chatShowTime = Main.chatLength / 2;
						}
						Main.NewText(newText, b17, b18, b19);
					}
					else
					{
						if (Main.netMode != 2)
						{
							return;
						}
						string text = string5.ToLower();
						if (text == Lang.mp[6])
						{
							string text2 = "";
							for (int num73 = 0; num73 < 255; num73++)
							{
								if (Main.player[num73].active)
								{
									text2 = ((!(text2 == "")) ? (text2 + ", " + Main.player[num73].name) : (text2 + Main.player[num73].name));
								}
							}
							NetMessage.SendData(25, whoAmI, -1, Lang.mp[7] + " " + text2 + ".", 255, 255f, 240f, 20f);
							return;
						}
						if (text.Length >= 4 && text.Substring(0, 4) == "/me ")
						{
							NetMessage.SendData(25, -1, -1, "*" + Main.player[whoAmI].name + " " + string5.Substring(4), 255, 200f, 100f);
							return;
						}
						if (text == Lang.mp[8])
						{
							NetMessage.SendData(25, -1, -1, "*" + Main.player[whoAmI].name + " " + Lang.mp[9] + " " + Main.rand.Next(1, 101), 255, 255f, 240f, 20f);
							return;
						}
						if (text.Length >= 3 && text.Substring(0, 3) == "/p ")
						{
							if (Main.player[whoAmI].team != 0)
							{
								for (int num74 = 0; num74 < 255; num74++)
								{
									if (Main.player[num74].team == Main.player[whoAmI].team)
									{
										NetMessage.SendData(25, num74, -1, string5.Substring(3), num72, (int)Main.teamColor[Main.player[whoAmI].team].R, (int)Main.teamColor[Main.player[whoAmI].team].G, (int)Main.teamColor[Main.player[whoAmI].team].B);
									}
								}
							}
							else
							{
								NetMessage.SendData(25, whoAmI, -1, Lang.mp[10], 255, 255f, 240f, 20f);
							}
							return;
						}
						if (Main.player[whoAmI].difficulty == 2)
						{
							b17 = Main.hcColor.R;
							b18 = Main.hcColor.G;
							b19 = Main.hcColor.B;
						}
						else if (Main.player[whoAmI].difficulty == 1)
						{
							b17 = Main.mcColor.R;
							b18 = Main.mcColor.G;
							b19 = Main.mcColor.B;
						}
						NetMessage.SendData(25, -1, -1, string5, num72, (int)b17, (int)b18, (int)b19);
						if (Main.dedServ)
						{
							Console.WriteLine("<" + Main.player[whoAmI].name + "> " + string5);
						}
					}
					return;
				}
				case 26:
				{
					byte b20 = readBuffer[num];
					if (Main.netMode != 2 || whoAmI == b20 || (Main.player[b20].hostile && Main.player[whoAmI].hostile))
					{
						num++;
						int num81 = readBuffer[num] - 1;
						num++;
						float num82 = BitConverter.ToSingle(readBuffer, num);
						num += 4;
						byte b21 = readBuffer[num];
						num++;
						bool pvp = false;
						int num83 = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						bool crit = false;
						string string6 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
						if (b21 != 0)
						{
							pvp = true;
						}
						if (num82 != 1f)
						{
							crit = true;
						}
						Main.player[b20].Hurt(num83, num81, pvp, quiet: true, string6, crit, num82);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(26, -1, whoAmI, string6, b20, num81, num82, (int)b21, (short)num83);
						}
					}
					return;
				}
				case 27:
				{
					short num62 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					float x6 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float y5 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float x7 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float y6 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float knockBack = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					short damage = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte b12 = readBuffer[num];
					num++;
					short num63 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					_ = readBuffer[num];
					num++;
					float[] array2 = new float[Projectile.maxAI];
					if (Main.netMode == 2)
					{
						b12 = (byte)whoAmI;
						if (Main.projHostile[num63])
						{
							return;
						}
					}
					for (int num64 = 0; num64 < Projectile.maxAI; num64++)
					{
						array2[num64] = BitConverter.ToSingle(readBuffer, num);
						num += 4;
					}
					int num65 = 1000;
					for (int num66 = 0; num66 < 1000; num66++)
					{
						if (Main.projectile[num66].owner == b12 && Main.projectile[num66].identity == num62 && Main.projectile[num66].active)
						{
							num65 = num66;
							break;
						}
					}
					if (num65 == 1000)
					{
						for (int num67 = 0; num67 < 1000; num67++)
						{
							if (!Main.projectile[num67].active)
							{
								num65 = num67;
								break;
							}
						}
					}
					if (!Main.projectile[num65].active || Main.projectile[num65].type != num63)
					{
						Main.projectile[num65].SetDefaults(num63);
						if (Main.netMode == 2)
						{
							Netplay.serverSock[whoAmI].spamProjectile += 1f;
						}
					}
					Main.projectile[num65].identity = num62;
					Main.projectile[num65].position.X = x6;
					Main.projectile[num65].position.Y = y5;
					Main.projectile[num65].velocity.X = x7;
					Main.projectile[num65].velocity.Y = y6;
					Main.projectile[num65].damage = damage;
					Main.projectile[num65].type = num63;
					Main.projectile[num65].owner = b12;
					Main.projectile[num65].knockBack = knockBack;
					for (int num68 = 0; num68 < Projectile.maxAI; num68++)
					{
						Main.projectile[num65].ai[num68] = array2[num68];
					}
					Main.projectile[num65].RunMethod("Initialize");
					if (Main.netMode == 2)
					{
						NetMessage.SendData(27, -1, whoAmI, "", num65);
					}
					return;
				}
				case 28:
				{
					short num90 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					float num91 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					float num92 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					int num93 = readBuffer[num] - 1;
					num++;
					short num94 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (num94 >= 0)
					{
						if (num91 != 1f)
						{
							Main.npc[num90].StrikeNPC(num94, num92, num93, crit: true, noEffect: false, num91);
						}
						else
						{
							Main.npc[num90].StrikeNPC(num94, num92, num93);
						}
					}
					else
					{
						Main.npc[num90].life = 0;
						Main.npc[num90].HitEffect();
						Main.npc[num90].active = false;
					}
					if (Main.netMode == 2)
					{
						if (Main.npc[num90].life <= 0)
						{
							NetMessage.SendData(28, -1, whoAmI, "", num90, num91, num92, num93, num94);
							NetMessage.SendData(23, -1, -1, "", num90);
						}
						else
						{
							NetMessage.SendData(28, -1, whoAmI, "", num90, num91, num92, num93, num94);
							Main.npc[num90].netUpdate = true;
						}
					}
					return;
				}
				case 29:
				{
					short num70 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte b14 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b14 = (byte)whoAmI;
					}
					for (int num71 = 0; num71 < 1000; num71++)
					{
						if (Main.projectile[num71].owner == b14 && Main.projectile[num71].identity == num70 && Main.projectile[num71].active)
						{
							Main.projectile[num71].Kill();
							break;
						}
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(29, -1, whoAmI, "", num70, (int)b14);
					}
					return;
				}
				case 30:
				{
					byte b15 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b15 = (byte)whoAmI;
					}
					num++;
					byte b16 = readBuffer[num];
					if (b16 == 1)
					{
						Main.player[b15].hostile = true;
					}
					else
					{
						Main.player[b15].hostile = false;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(30, -1, whoAmI, "", b15);
						string str = " " + Lang.mp[11];
						if (b16 == 0)
						{
							str = " " + Lang.mp[12];
						}
						NetMessage.SendData(25, -1, -1, Main.player[b15].name + str, 255, (int)Main.teamColor[Main.player[b15].team].R, (int)Main.teamColor[Main.player[b15].team].G, (int)Main.teamColor[Main.player[b15].team].B);
					}
					return;
				}
				case 31:
				{
					if (Main.netMode != 2)
					{
						return;
					}
					int x8 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int y7 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int num86 = Chest.FindChest(x8, y7);
					if (num86 > -1 && Chest.UsingChest(num86) == -1)
					{
						for (int num87 = 0; num87 < Chest.maxItems; num87++)
						{
							NetMessage.SendData(32, whoAmI, -1, "", num86, num87);
						}
						NetMessage.SendData(33, whoAmI, -1, "", num86);
						Main.player[whoAmI].chest = num86;
					}
					return;
				}
				case 32:
				{
					MemoryStream memoryStream4 = new MemoryStream(readBuffer);
					memoryStream4.Position = num;
					BinaryReader binaryReader3 = new BinaryReader(memoryStream4);
					int num88 = binaryReader3.ReadInt16();
					num += 2;
					int num89 = binaryReader3.ReadByte();
					num++;
					int stack3 = binaryReader3.ReadInt32();
					num++;
					int pre2 = binaryReader3.ReadByte();
					num++;
					int type4 = binaryReader3.ReadInt16();
					if (Main.chest[num88] == null)
					{
						Main.chest[num88] = new Chest();
					}
					if (Main.chest[num88].item[num89] == null)
					{
						Main.chest[num88].item[num89] = new Item();
					}
					Main.chest[num88].item[num89].netDefaults(type4);
					Main.chest[num88].item[num89].Prefix(pre2);
					Main.chest[num88].item[num89].stack = stack3;
					if (!string.IsNullOrEmpty(Main.chest[num88].item[num89].name))
					{
						Codable.LoadCustomDataNew(Main.chest[num88].item[num89], binaryReader3, 4, Config.GetCurrentVersion(Main.chest[num88].item[num89]));
					}
					binaryReader3.Close();
					return;
				}
				case 33:
				{
					int num59 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					int chestX = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int chestY = BitConverter.ToInt32(readBuffer, num);
					if (Main.netMode == 1)
					{
						if (Main.player[Main.myPlayer].chest == -1)
						{
							Main.playerInventory = true;
							Main.PlaySound(10);
						}
						else if (Main.player[Main.myPlayer].chest != num59 && num59 != -1)
						{
							Main.playerInventory = true;
							Main.PlaySound(12);
						}
						else if (Main.player[Main.myPlayer].chest != -1 && num59 == -1)
						{
							Main.PlaySound(11);
						}
						Main.player[Main.myPlayer].chest = num59;
						Main.player[Main.myPlayer].chestX = chestX;
						Main.player[Main.myPlayer].chestY = chestY;
					}
					else
					{
						Main.player[whoAmI].chest = num59;
					}
					return;
				}
				case 34:
					if (Main.netMode == 2)
					{
						int num84 = BitConverter.ToInt32(readBuffer, num);
						num += 4;
						int num85 = BitConverter.ToInt32(readBuffer, num);
						WorldGen.KillTile(num84, num85);
						if (!Main.tile[num84, num85].active)
						{
							NetMessage.SendData(17, -1, -1, "", 0, num84, num85);
						}
					}
					return;
				case 35:
				{
					int num60 = readBuffer[num];
					if (Main.netMode == 2)
					{
						num60 = whoAmI;
					}
					num++;
					int num61 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (num60 != Main.myPlayer)
					{
						Main.player[num60].HealEffect(num61);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(35, -1, whoAmI, "", num60, num61);
					}
					return;
				}
				case 36:
				{
					int num75 = readBuffer[num];
					if (Main.netMode == 2)
					{
						num75 = whoAmI;
					}
					num++;
					foreach (string key2 in Biome.Biomes.Keys)
					{
						Main.player[num75].zone[key2] = (1 == readBuffer[num]);
						num++;
					}
					int num76 = readBuffer[num];
					num++;
					int num77 = readBuffer[num];
					num++;
					int num78 = readBuffer[num];
					num++;
					int num79 = readBuffer[num];
					num++;
					int num80 = readBuffer[num];
					num++;
					if (num76 == 0)
					{
						Main.player[num75].zoneEvil = false;
					}
					else
					{
						Main.player[num75].zoneEvil = true;
					}
					if (num77 == 0)
					{
						Main.player[num75].zoneMeteor = false;
					}
					else
					{
						Main.player[num75].zoneMeteor = true;
					}
					if (num78 == 0)
					{
						Main.player[num75].zoneDungeon = false;
					}
					else
					{
						Main.player[num75].zoneDungeon = true;
					}
					if (num79 == 0)
					{
						Main.player[num75].zoneJungle = false;
					}
					else
					{
						Main.player[num75].zoneJungle = true;
					}
					if (num80 == 0)
					{
						Main.player[num75].zoneHoly = false;
					}
					else
					{
						Main.player[num75].zoneHoly = true;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(36, -1, whoAmI, "", num75);
					}
					return;
				}
				case 37:
					if (Main.netMode == 1)
					{
						if (Main.autoPass)
						{
							NetMessage.SendData(38, -1, -1, Netplay.password);
							Main.autoPass = false;
						}
						else
						{
							Netplay.password = "";
							Main.menuMode = 31;
						}
					}
					return;
				case 38:
					if (Main.netMode == 2)
					{
						string string4 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
						if (string4 == Netplay.password)
						{
							Netplay.serverSock[whoAmI].state = 1;
							NetMessage.SendData(3, whoAmI);
						}
						else
						{
							NetMessage.SendData(2, whoAmI, -1, Lang.mp[1]);
						}
					}
					return;
				case 39:
					if (Main.netMode == 1)
					{
						short num58 = BitConverter.ToInt16(readBuffer, num);
						Main.item[num58].owner = 255;
						NetMessage.SendData(22, -1, -1, "", num58);
						return;
					}
					break;
				}
				switch (b)
				{
				case 40:
				{
					byte b27 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b27 = (byte)whoAmI;
					}
					num++;
					int talkNPC = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					Main.player[b27].talkNPC = talkNPC;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(40, -1, whoAmI, "", b27);
					}
					break;
				}
				case 41:
				{
					byte b25 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b25 = (byte)whoAmI;
					}
					num++;
					float itemRotation = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					int itemAnimation = BitConverter.ToInt16(readBuffer, num);
					Main.player[b25].itemRotation = itemRotation;
					Main.player[b25].itemAnimation = itemAnimation;
					Main.player[b25].channel = Main.player[b25].inventory[Main.player[b25].selectedItem].channel;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(41, -1, whoAmI, "", b25);
					}
					break;
				}
				case 42:
				{
					int num120 = readBuffer[num];
					if (Main.netMode == 2)
					{
						num120 = whoAmI;
					}
					num++;
					int statMana = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					int statManaMax = BitConverter.ToInt16(readBuffer, num);
					if (Main.netMode == 2)
					{
						num120 = whoAmI;
					}
					Main.player[num120].statMana = statMana;
					Main.player[num120].statManaMax = statManaMax;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(42, -1, whoAmI, "", num120);
					}
					break;
				}
				case 43:
				{
					int num112 = readBuffer[num];
					if (Main.netMode == 2)
					{
						num112 = whoAmI;
					}
					num++;
					int num113 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (num112 != Main.myPlayer)
					{
						Main.player[num112].ManaEffect(num113);
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(43, -1, whoAmI, "", num112, num113);
					}
					break;
				}
				case 44:
				{
					byte b28 = readBuffer[num];
					if (b28 != Main.myPlayer)
					{
						if (Main.netMode == 2)
						{
							b28 = (byte)whoAmI;
						}
						num++;
						int num118 = readBuffer[num] - 1;
						num++;
						short num119 = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						byte b29 = readBuffer[num];
						num++;
						string string8 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
						bool pvp2 = false;
						if (b29 != 0)
						{
							pvp2 = true;
						}
						Main.player[b28].KillMe(num119, num118, pvp2, string8);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(44, -1, whoAmI, string8, b28, num118, num119, (int)b29);
						}
					}
					break;
				}
				case 45:
				{
					int num127 = readBuffer[num];
					if (Main.netMode == 2)
					{
						num127 = whoAmI;
					}
					num++;
					int num128 = readBuffer[num];
					num++;
					int team = Main.player[num127].team;
					Main.player[num127].team = num128;
					if (Main.netMode != 2)
					{
						break;
					}
					NetMessage.SendData(45, -1, whoAmI, "", num127);
					string str2 = "";
					switch (num128)
					{
					case 0:
						str2 = " " + Lang.mp[13];
						break;
					case 1:
						str2 = " " + Lang.mp[14];
						break;
					case 2:
						str2 = " " + Lang.mp[15];
						break;
					case 3:
						str2 = " " + Lang.mp[16];
						break;
					case 4:
						str2 = " " + Lang.mp[17];
						break;
					}
					for (int num129 = 0; num129 < 255; num129++)
					{
						if (num129 == whoAmI || (team > 0 && Main.player[num129].team == team) || (num128 > 0 && Main.player[num129].team == num128))
						{
							NetMessage.SendData(25, num129, -1, Main.player[num127].name + str2, 255, (int)Main.teamColor[num128].R, (int)Main.teamColor[num128].G, (int)Main.teamColor[num128].B);
						}
					}
					break;
				}
				case 46:
					if (Main.netMode == 2)
					{
						int i2 = BitConverter.ToInt32(readBuffer, num);
						num += 4;
						int j2 = BitConverter.ToInt32(readBuffer, num);
						num += 4;
						int num109 = Sign.ReadSign(i2, j2);
						if (num109 >= 0)
						{
							NetMessage.SendData(47, whoAmI, -1, "", num109);
						}
					}
					break;
				case 47:
				{
					int num98 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					int x9 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int y8 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					string string7 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
					Main.sign[num98] = new Sign();
					Main.sign[num98].x = x9;
					Main.sign[num98].y = y8;
					Sign.TextSign(num98, string7);
					if (Main.netMode == 1 && Main.sign[num98] != null && num98 != Main.player[Main.myPlayer].sign)
					{
						Main.playerInventory = false;
						Main.player[Main.myPlayer].talkNPC = -1;
						Main.editSign = false;
						Main.PlaySound(10);
						Main.player[Main.myPlayer].sign = num98;
						Main.npcChatText = Main.sign[num98].text;
					}
					break;
				}
				case 48:
				{
					int num99 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int num100 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					byte liquid = readBuffer[num];
					num++;
					byte b24 = readBuffer[num];
					num++;
					if (Main.netMode == 2 && Netplay.spamCheck)
					{
						int num101 = whoAmI;
						int num102 = (int)(Main.player[num101].position.X + (float)(Main.player[num101].width / 2));
						int num103 = (int)(Main.player[num101].position.Y + (float)(Main.player[num101].height / 2));
						int num104 = 10;
						int num105 = num102 - num104;
						int num106 = num102 + num104;
						int num107 = num103 - num104;
						int num108 = num103 + num104;
						if (num102 < num105 || num102 > num106 || num103 < num107 || num103 > num108)
						{
							NetMessage.BootPlayer(whoAmI, "Cheating attempt detected: Liquid spam");
							break;
						}
					}
					if (Main.tile[num99, num100] == null)
					{
						Main.tile[num99, num100] = new Tile();
					}
					lock (Main.tile[num99, num100])
					{
						Main.tile[num99, num100].liquid = liquid;
						if (b24 == 1)
						{
							Main.tile[num99, num100].lava = true;
						}
						else
						{
							Main.tile[num99, num100].lava = false;
						}
						if (Main.netMode == 2)
						{
							WorldGen.SquareTileFrame(num99, num100);
						}
					}
					break;
				}
				case 49:
					if (Netplay.clientSock.state == 6)
					{
						Netplay.clientSock.state = 10;
						Main.player[Main.myPlayer].Spawn();
					}
					break;
				case 50:
				{
					int num123 = readBuffer[num];
					num++;
					if (Main.netMode == 2)
					{
						num123 = whoAmI;
					}
					else if (num123 == Main.myPlayer)
					{
						break;
					}
					int num124 = readBuffer[num++];
					if (Main.player[num123].buffType.Length != num124)
					{
						Array.Resize(ref Main.player[num123].buffType, num124);
						Array.Resize(ref Main.player[num123].buffTime, num124);
						Array.Resize(ref Main.player[num123].buffCode, num124);
					}
					for (int num125 = 0; num125 < Main.player[num123].buffType.Length; num125++)
					{
						int num126 = readBuffer[num];
						Main.player[num123].buffType[num125] = num126;
						if (num126 > 0)
						{
							Main.player[num123].buffTime[num125] = 60;
							if (Config.buffDefs.assemblyByType[num126] != null)
							{
								Main.player[num123].buffCode[num125] = Config.buffDefs.assemblyByType[num126].CreateInstance("Terraria." + Codable.ParseName(Main.buffName[num126]) + "Buff");
							}
						}
						else
						{
							Main.player[num123].buffTime[num125] = 0;
							Main.player[num123].buffCode[num125] = null;
						}
						num++;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendData(50, -1, whoAmI, "", num123);
					}
					break;
				}
				case 51:
				{
					byte b22 = readBuffer[num];
					num++;
					byte b23 = readBuffer[num];
					switch (b23)
					{
					case 1:
						NPC.SpawnSkeletron();
						break;
					case 2:
						if (Main.netMode != 2)
						{
							Main.PlaySound(2, (int)Main.player[b22].position.X, (int)Main.player[b22].position.Y);
						}
						else if (Main.netMode == 2)
						{
							NetMessage.SendData(51, -1, whoAmI, "", b22, (int)b23);
						}
						break;
					}
					break;
				}
				case 52:
				{
					byte number = readBuffer[num];
					num++;
					byte b30 = readBuffer[num];
					num++;
					int num121 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int num122 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					if (b30 == 1)
					{
						Chest.Unlock(num121, num122);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(52, -1, whoAmI, "", number, (int)b30, num121, num122);
							NetMessage.SendTileSquare(-1, num121, num122, 2);
						}
					}
					break;
				}
				case 53:
				{
					short num117 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte type5 = readBuffer[num];
					num++;
					short time = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					Main.npc[num117].AddBuff(type5, time, quiet: true);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(54, -1, -1, "", num117);
					}
					break;
				}
				case 54:
				{
					if (Main.netMode != 1)
					{
						break;
					}
					short num115 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					for (int num116 = 0; num116 < 5; num116++)
					{
						Main.npc[num115].buffType[num116] = readBuffer[num];
						num++;
						Main.npc[num115].buffTime[num116] = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						if (Config.buffDefs.assemblyByType[Main.npc[num115].buffType[num116]] != null)
						{
							Assembly assembly = Config.buffDefs.assemblyByType[Main.npc[num115].buffType[num116]];
							Main.npc[num115].buffCode[num116] = assembly.CreateInstance(Config.namespaces[assembly.FullName] + "." + Codable.ParseName(Main.buffName[Main.npc[num115].buffType[num116]]) + "Buff");
						}
					}
					break;
				}
				case 55:
				{
					byte b31 = readBuffer[num];
					num++;
					byte b32 = readBuffer[num];
					num++;
					short num130 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					if (Main.netMode != 2 || b31 == whoAmI || Main.pvpBuff[b32])
					{
						if (Main.netMode == 1 && b31 == Main.myPlayer)
						{
							Main.player[b31].AddBuff(b32, num130);
						}
						else if (Main.netMode == 2)
						{
							NetMessage.SendData(55, b31, -1, "", b31, (int)b32, num130);
						}
					}
					break;
				}
				case 56:
					if (Main.netMode == 1)
					{
						short i3 = BitConverter.ToInt16(readBuffer, num);
						num += 2;
						string string9 = Encoding.UTF8.GetString(readBuffer, num, length - num + start);
						Main.chrName[i3] = string9;
					}
					break;
				case 57:
					if (Main.netMode == 1)
					{
						WorldGen.tGood = readBuffer[num];
						num++;
						WorldGen.tEvil = readBuffer[num];
					}
					break;
				case 58:
				{
					byte b26 = readBuffer[num];
					if (Main.netMode == 2)
					{
						b26 = (byte)whoAmI;
					}
					num++;
					float num114 = BitConverter.ToSingle(readBuffer, num);
					num += 4;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(58, -1, whoAmI, "", whoAmI, num114);
						break;
					}
					Main.harpNote = num114;
					int style = 26;
					if (Main.player[b26].inventory[Main.player[b26].selectedItem].type == 507)
					{
						style = 35;
					}
					Main.PlaySound(2, (int)Main.player[b26].position.X, (int)Main.player[b26].position.Y, style);
					break;
				}
				case 59:
				{
					int num110 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int num111 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					WorldGen.hitSwitch(num110, num111);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(59, -1, whoAmI, "", num110, num111);
					}
					break;
				}
				case 60:
				{
					short num131 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					short num132 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					short num133 = BitConverter.ToInt16(readBuffer, num);
					num += 2;
					byte b33 = readBuffer[num];
					num++;
					bool homeless = false;
					if (b33 == 1)
					{
						homeless = true;
					}
					if (Main.netMode == 1)
					{
						Main.npc[num131].homeless = homeless;
						Main.npc[num131].homeTileX = num132;
						Main.npc[num131].homeTileY = num133;
					}
					else if (b33 == 0)
					{
						WorldGen.kickOut(num131);
					}
					else
					{
						WorldGen.moveRoom(num132, num133, num131);
					}
					break;
				}
				case 61:
				{
					int plr = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					int num95 = BitConverter.ToInt32(readBuffer, num);
					num += 4;
					if (Main.netMode != 2)
					{
						break;
					}
					if (num95 == 4 || num95 == 13 || num95 == 50 || num95 == 125 || num95 == 126 || num95 == 134 || num95 == 127 || num95 == 128)
					{
						bool flag6 = true;
						for (int num96 = 0; num96 < 200; num96++)
						{
							if (Main.npc[num96].active && Main.npc[num96].type == num95)
							{
								flag6 = false;
							}
						}
						if (flag6)
						{
							NPC.SpawnOnPlayer(plr, num95);
						}
					}
					else if (num95 < 0)
					{
						int num97 = -1;
						if (num95 == -1)
						{
							num97 = 1;
						}
						if (num95 == -2)
						{
							num97 = 2;
						}
						if (num97 > 0 && Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion(num97);
						}
					}
					break;
				}
				}
			}
			else
			{
				byte b34 = readBuffer[num];
				num++;
				int num134 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				int num135 = BitConverter.ToInt32(readBuffer, num);
				num += 4;
				int num136 = readBuffer[num];
				int direction2 = 0;
				if (num136 == 0)
				{
					direction2 = -1;
				}
				switch (b34)
				{
				case 0:
					WorldGen.OpenDoor(num134, num135, direction2);
					break;
				case 1:
					WorldGen.CloseDoor(num134, num135, forced: true);
					break;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(19, -1, whoAmI, "", b34, num134, num135, num136);
				}
			}
		}

		public void GetData(int start, int length)
		{
			if (whoAmI < 256)
			{
				Netplay.serverSock[whoAmI].timeOut = 0;
			}
			else
			{
				Netplay.clientSock.timeOut = 0;
			}
			int num = 0;
			num = start + 1;
			byte b = readBuffer[start];
			Main.rxMsg++;
			Main.rxData += length;
			Main.rxMsgType[b]++;
			Main.rxDataType[b] += length;
			if (Main.netMode == 1 && Netplay.clientSock.statusMax > 0)
			{
				Netplay.clientSock.statusCount++;
			}
			if (Main.verboseNetplay)
			{
				for (int i = start; i < start + length; i++)
				{
				}
				for (int j = start; j < start + length; j++)
				{
					_ = readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.serverSock[whoAmI].state == -1)
			{
				NetMessage.SendData(2, whoAmI, -1, Lang.mp[1]);
				return;
			}
			if (Main.netMode == 2 && Netplay.serverSock[whoAmI].state < 10 && b > 12 && b != 16 && b != 42 && b != 50 && b != 38 && b != 103)
			{
				NetMessage.BootPlayer(whoAmI, Lang.mp[2]);
			}
			if (Events.world.PreNetReceiveIntercept == null || Events.world.PreNetReceiveIntercept(this, b, start, length, ref num))
			{
				ProcessMessage(b, ref num, start, length);
			}
			if (Events.world.NetReceiveIntercept != null)
			{
				Events.world.NetReceiveIntercept(this, b, start, length, ref num);
			}
		}
	}
}
