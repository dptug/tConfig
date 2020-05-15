using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Terraria
{
	public class NetMessage
	{
		public static messageBuffer[] buffer = new messageBuffer[257];

		public static void SendModData(int modIndex, int msgID, int remoteClient = -1, int ignoreClient = -1, params object[] parameters)
		{
			int num = 100;
			int num2 = 256;
			if (Main.netMode == 2 && remoteClient >= 0)
			{
				num2 = remoteClient;
			}
			lock (buffer[num2])
			{
				int num3 = 5;
				int num4 = num3;
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				for (int i = 0; i < parameters.Length; i++)
				{
					string text = parameters[i].GetType().ToString();
					if (text.Contains("Int32"))
					{
						binaryWriter.Write((int)parameters[i]);
					}
					else if (text.Contains("Int16"))
					{
						binaryWriter.Write((short)parameters[i]);
					}
					else if (text.Contains("UInt16"))
					{
						binaryWriter.Write((ushort)parameters[i]);
					}
					else if (text.Contains("Double"))
					{
						binaryWriter.Write((double)parameters[i]);
					}
					else if (text.Contains("String"))
					{
						binaryWriter.Write((string)parameters[i]);
					}
					else if (text.Contains("Single"))
					{
						binaryWriter.Write((float)parameters[i]);
					}
					else if (text.Contains("Boolean"))
					{
						binaryWriter.Write((bool)parameters[i]);
					}
					else if (text.Contains("Byte"))
					{
						binaryWriter.Write((byte)parameters[i]);
					}
					else if (text.Contains("MemoryStream"))
					{
						binaryWriter.Write(((MemoryStream)parameters[i]).ToArray());
					}
					else
					{
						Console.WriteLine("Unknown datatype " + text);
					}
				}
				num3 += memoryStream.ToArray().Length;
				num3 += 2;
				byte[] bytes = BitConverter.GetBytes(num);
				byte[] bytes2 = BitConverter.GetBytes(num3 - 4);
				Buffer.BlockCopy(bytes2, 0, buffer[num2].writeBuffer, 0, 4);
				Buffer.BlockCopy(bytes, 0, buffer[num2].writeBuffer, 4, 1);
				buffer[num2].writeBuffer[num4] = (byte)modIndex;
				num4++;
				buffer[num2].writeBuffer[num4] = (byte)msgID;
				num4++;
				if (memoryStream.ToArray().Length > 0)
				{
					Buffer.BlockCopy(memoryStream.ToArray(), 0, buffer[num2].writeBuffer, num4, memoryStream.ToArray().Length);
				}
				binaryWriter.Close();
				memoryStream.Close();
				if (Main.netMode == 1)
				{
					if (Netplay.clientSock.tcpClient.Connected)
					{
						try
						{
							buffer[num2].spamCount++;
							Main.txMsg++;
							Main.txData += num3;
							Main.txMsgType[num]++;
							Main.txDataType[num] += num3;
							Netplay.clientSock.networkStream.BeginWrite(buffer[num2].writeBuffer, 0, num3, Netplay.clientSock.ClientWriteCallBack, Netplay.clientSock.networkStream);
						}
						catch
						{
						}
					}
				}
				else if (remoteClient == -1)
				{
					for (int j = 0; j < 256; j++)
					{
						if (j != ignoreClient && (buffer[j].broadcast || (Netplay.serverSock[j].state >= 3 && num == 10)) && Netplay.serverSock[j].tcpClient.Connected)
						{
							try
							{
								buffer[j].spamCount++;
								Main.txMsg++;
								Main.txData += num3;
								Main.txMsgType[num]++;
								Main.txDataType[num] += num3;
								Netplay.serverSock[j].networkStream.BeginWrite(buffer[num2].writeBuffer, 0, num3, Netplay.serverSock[j].ServerWriteCallBack, Netplay.serverSock[j].networkStream);
							}
							catch
							{
							}
						}
					}
				}
				else if (Netplay.serverSock[remoteClient].tcpClient.Connected)
				{
					try
					{
						buffer[remoteClient].spamCount++;
						Main.txMsg++;
						Main.txData += num3;
						Main.txMsgType[num]++;
						Main.txDataType[num] += num3;
						Netplay.serverSock[remoteClient].networkStream.BeginWrite(buffer[num2].writeBuffer, 0, num3, Netplay.serverSock[remoteClient].ServerWriteCallBack, Netplay.serverSock[remoteClient].networkStream);
					}
					catch
					{
					}
				}
				if (Main.verboseNetplay)
				{
					for (int k = 0; k < num3; k++)
					{
					}
					for (int l = 0; l < num3; l++)
					{
						_ = buffer[num2].writeBuffer[l];
					}
				}
				buffer[num2].writeLocked = false;
			}
		}

		public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0)
		{
			int num = 256;
			if (Main.netMode == 2 && remoteClient >= 0)
			{
				num = remoteClient;
			}
			lock (buffer[num])
			{
				int num2 = 5;
				int num3 = num2;
				if (msgType == 1)
				{
					byte[] bytes = BitConverter.GetBytes(msgType);
					byte[] bytes2 = Encoding.UTF8.GetBytes("Terraria" + Main.curRelease + " " + Constants.version + " " + Config.tConfigHash);
					num2 += bytes2.Length;
					byte[] bytes3 = BitConverter.GetBytes(num2 - 4);
					Buffer.BlockCopy(bytes3, 0, buffer[num].writeBuffer, 0, 4);
					Buffer.BlockCopy(bytes, 0, buffer[num].writeBuffer, 4, 1);
					Buffer.BlockCopy(bytes2, 0, buffer[num].writeBuffer, 5, bytes2.Length);
				}
				else if (Events.world.PreNetSendIntercept == null || Events.world.PreNetSendIntercept(num, ref num2, ref num3, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5))
				{
					switch (msgType)
					{
					case 2:
					{
						byte[] bytes231 = BitConverter.GetBytes(msgType);
						byte[] bytes232 = Encoding.UTF8.GetBytes(text);
						num2 += bytes232.Length;
						byte[] bytes233 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes233, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes231, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes232, 0, buffer[num].writeBuffer, 5, bytes232.Length);
						if (Main.dedServ)
						{
							Console.WriteLine(Netplay.serverSock[num].tcpClient.Client.RemoteEndPoint.ToString() + " was booted: " + text);
						}
						break;
					}
					case 3:
						if (Main.dedServ)
						{
							if (Config.modPartsTransferred[remoteClient] == 0)
							{
								Console.WriteLine("Starting mod transfer...");
								Config.SetupModTransfer(remoteClient);
							}
							byte[] array = Config.TransferModPart(remoteClient);
							num2 += array.Length;
							byte[] bytes192 = BitConverter.GetBytes(msgType);
							byte[] bytes193 = BitConverter.GetBytes(num2 - 4);
							Buffer.BlockCopy(bytes193, 0, buffer[num].writeBuffer, 0, 4);
							Buffer.BlockCopy(bytes192, 0, buffer[num].writeBuffer, 4, 1);
							Buffer.BlockCopy(array, 0, buffer[num].writeBuffer, 5, array.Length);
						}
						else
						{
							Console.WriteLine("Telling server to send me another mod piece...");
							byte[] bytes194 = BitConverter.GetBytes(msgType);
							byte[] bytes195 = BitConverter.GetBytes(num2 - 4);
							Buffer.BlockCopy(bytes195, 0, buffer[num].writeBuffer, 0, 4);
							Buffer.BlockCopy(bytes194, 0, buffer[num].writeBuffer, 4, 1);
						}
						break;
					case 103:
						if (Main.dedServ)
						{
							byte[] bytes131 = BitConverter.GetBytes(msgType);
							byte[] bytes132 = BitConverter.GetBytes(remoteClient);
							num2 += bytes132.Length;
							byte[] bytes133 = BitConverter.GetBytes(num2 - 4);
							Buffer.BlockCopy(bytes133, 0, buffer[num].writeBuffer, 0, 4);
							Buffer.BlockCopy(bytes131, 0, buffer[num].writeBuffer, 4, 1);
							Buffer.BlockCopy(bytes132, 0, buffer[num].writeBuffer, 5, bytes132.Length);
						}
						else
						{
							byte[] bytes134 = BitConverter.GetBytes(msgType);
							byte[] bytes135 = BitConverter.GetBytes(num2 - 4);
							Buffer.BlockCopy(bytes135, 0, buffer[num].writeBuffer, 0, 4);
							Buffer.BlockCopy(bytes134, 0, buffer[num].writeBuffer, 4, 1);
						}
						break;
					case 4:
					{
						byte[] bytes213 = BitConverter.GetBytes(msgType);
						byte b53 = (byte)number;
						byte b54 = (byte)Main.player[b53].hair;
						byte b55 = 0;
						if (Main.player[b53].male)
						{
							b55 = 1;
						}
						byte[] bytes214 = Encoding.UTF8.GetBytes(text);
						num2 += 24 + bytes214.Length + 1;
						byte[] bytes215 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes215, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes213, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b53;
						num3++;
						buffer[num].writeBuffer[6] = b54;
						num3++;
						buffer[num].writeBuffer[7] = b55;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].hairColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].hairColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].hairColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].skinColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].skinColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].skinColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].eyeColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].eyeColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].eyeColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shirtColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shirtColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shirtColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].underShirtColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].underShirtColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].underShirtColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].pantsColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].pantsColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].pantsColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shoeColor.R;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shoeColor.G;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].shoeColor.B;
						num3++;
						buffer[num].writeBuffer[num3] = Main.player[b53].difficulty;
						num3++;
						Buffer.BlockCopy(bytes214, 0, buffer[num].writeBuffer, num3, bytes214.Length);
						break;
					}
					case 5:
					{
						byte[] bytes258 = BitConverter.GetBytes(msgType);
						byte b62 = (byte)number;
						byte b63 = (byte)number2;
						byte[] array3;
						byte[] bytes259;
						Item item2;
						if (number2 < 49f)
						{
							if (Main.player[number].inventory[(int)number2].name == "" || Main.player[number].inventory[(int)number2].stack == 0 || Main.player[number].inventory[(int)number2].type == 0)
							{
								Main.player[number].inventory[(int)number2].netID = 0;
							}
							array3 = BitConverter.GetBytes(Main.player[number].inventory[(int)number2].stack);
							bytes259 = BitConverter.GetBytes((short)Main.player[number].inventory[(int)number2].netID);
							if (Main.player[number].inventory[(int)number2].stack < 0)
							{
								array3 = new byte[4];
							}
							item2 = Main.player[number].inventory[(int)number2];
						}
						else
						{
							if (Main.player[number].armor[(int)number2 - 48 - 1].name == "" || Main.player[number].armor[(int)number2 - 48 - 1].stack == 0 || Main.player[number].armor[(int)number2 - 48 - 1].type == 0)
							{
								Main.player[number].armor[(int)number2 - 48 - 1].SetDefaults(0);
							}
							array3 = BitConverter.GetBytes(Main.player[number].armor[(int)number2 - 48 - 1].stack);
							bytes259 = BitConverter.GetBytes((short)Main.player[number].armor[(int)number2 - 48 - 1].netID);
							if (Main.player[number].armor[(int)number2 - 48 - 1].stack < 0)
							{
								array3 = new byte[4];
							}
							item2 = Main.player[number].armor[(int)number2 - 48 - 1];
						}
						byte b64 = (byte)number3;
						MemoryStream memoryStream4 = new MemoryStream();
						BinaryWriter binaryWriter3 = new BinaryWriter(memoryStream4);
						Codable.SaveCustomData(item2, binaryWriter3, net: true);
						num2 += memoryStream4.ToArray().Length;
						binaryWriter3.Close();
						num2 += 7 + bytes259.Length;
						byte[] bytes260 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes260, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes258, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b62;
						num3++;
						buffer[num].writeBuffer[6] = b63;
						num3++;
						Buffer.BlockCopy(array3, 0, buffer[num].writeBuffer, num3, array3.Length);
						num3 += 4;
						buffer[num].writeBuffer[8] = b64;
						num3++;
						Buffer.BlockCopy(bytes259, 0, buffer[num].writeBuffer, num3, bytes259.Length);
						num3 += bytes259.Length;
						if (memoryStream4.ToArray().Length > 0)
						{
							Buffer.BlockCopy(memoryStream4.ToArray(), 0, buffer[num].writeBuffer, num3, memoryStream4.ToArray().Length);
						}
						break;
					}
					case 6:
					{
						byte[] bytes153 = BitConverter.GetBytes(msgType);
						byte[] bytes154 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes154, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes153, 0, buffer[num].writeBuffer, 4, 1);
						break;
					}
					case 7:
					{
						byte[] bytes50 = BitConverter.GetBytes(msgType);
						byte[] bytes51 = BitConverter.GetBytes((int)Main.time);
						byte b12 = 0;
						if (Main.dayTime)
						{
							b12 = 1;
						}
						byte b13 = (byte)Main.moonPhase;
						byte b14 = 0;
						if (Main.bloodMoon)
						{
							b14 = 1;
						}
						byte[] bytes52 = BitConverter.GetBytes(Main.maxTilesX);
						byte[] bytes53 = BitConverter.GetBytes(Main.maxTilesY);
						byte[] bytes54 = BitConverter.GetBytes(Main.spawnTileX);
						byte[] bytes55 = BitConverter.GetBytes(Main.spawnTileY);
						byte[] bytes56 = BitConverter.GetBytes((int)Main.worldSurface);
						byte[] bytes57 = BitConverter.GetBytes((int)Main.rockLayer);
						byte[] bytes58 = BitConverter.GetBytes((int)Main.hellLayer);
						byte[] bytes59 = BitConverter.GetBytes(Main.worldID);
						byte[] bytes60 = Encoding.UTF8.GetBytes(Main.worldName);
						byte b15 = 0;
						if (WorldGen.shadowOrbSmashed)
						{
							b15 = (byte)(b15 + 1);
						}
						if (NPC.downedBoss1)
						{
							b15 = (byte)(b15 + 2);
						}
						if (NPC.downedBoss2)
						{
							b15 = (byte)(b15 + 4);
						}
						if (NPC.downedBoss3)
						{
							b15 = (byte)(b15 + 8);
						}
						if (Main.hardMode)
						{
							b15 = (byte)(b15 + 16);
						}
						if (NPC.downedClown)
						{
							b15 = (byte)(b15 + 32);
						}
						byte[] bytes61 = BitConverter.GetBytes(Config.randSeed);
						num2 += bytes51.Length + 1 + 1 + 1 + bytes52.Length + bytes53.Length + bytes54.Length + bytes55.Length + bytes56.Length + bytes57.Length + bytes58.Length + bytes59.Length + 1 + bytes60.Length + bytes61.Length;
						byte[] bytes62 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes62, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes50, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes51, 0, buffer[num].writeBuffer, 5, bytes51.Length);
						num3 += bytes51.Length;
						buffer[num].writeBuffer[num3] = b12;
						num3++;
						buffer[num].writeBuffer[num3] = b13;
						num3++;
						buffer[num].writeBuffer[num3] = b14;
						num3++;
						Buffer.BlockCopy(bytes52, 0, buffer[num].writeBuffer, num3, bytes52.Length);
						num3 += bytes52.Length;
						Buffer.BlockCopy(bytes53, 0, buffer[num].writeBuffer, num3, bytes53.Length);
						num3 += bytes53.Length;
						Buffer.BlockCopy(bytes54, 0, buffer[num].writeBuffer, num3, bytes54.Length);
						num3 += bytes54.Length;
						Buffer.BlockCopy(bytes55, 0, buffer[num].writeBuffer, num3, bytes55.Length);
						num3 += bytes55.Length;
						Buffer.BlockCopy(bytes56, 0, buffer[num].writeBuffer, num3, bytes56.Length);
						num3 += bytes56.Length;
						Buffer.BlockCopy(bytes57, 0, buffer[num].writeBuffer, num3, bytes57.Length);
						num3 += bytes57.Length;
						Buffer.BlockCopy(bytes58, 0, buffer[num].writeBuffer, num3, bytes58.Length);
						num3 += bytes58.Length;
						Buffer.BlockCopy(bytes59, 0, buffer[num].writeBuffer, num3, bytes59.Length);
						num3 += bytes59.Length;
						buffer[num].writeBuffer[num3] = b15;
						num3++;
						buffer[num].writeBuffer[num3] = (byte)bytes60.Length;
						num3++;
						Buffer.BlockCopy(bytes60, 0, buffer[num].writeBuffer, num3, bytes60.Length);
						num3 += bytes60.Length;
						Buffer.BlockCopy(bytes61, 0, buffer[num].writeBuffer, num3, bytes61.Length);
						num3 += bytes61.Length;
						using (MemoryStream memoryStream = new MemoryStream(buffer[num].writeBuffer, num3, buffer[num].writeBuffer.Length - num3))
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
							{
								long position = memoryStream.Position;
								binaryWriter.Write((byte)0);
								int num4 = 0;
								StringBuilder stringBuilder = new StringBuilder();
								JsonWriter jsonWriter = new JsonWriter(stringBuilder);
								jsonWriter.WriteObjectStart();
								jsonWriter.WriteObjectEnd();
								foreach (KeyValuePair<string, JsonData> item3 in Config.jsonCurrent.dict)
								{
									if (item3.Value != null)
									{
										StringBuilder stringBuilder2 = new StringBuilder();
										jsonWriter = new JsonWriter(stringBuilder2);
										jsonWriter.WriteObjectStart();
										foreach (KeyValuePair<string, JsonData> item4 in (IEnumerable)item3.Value["settings"])
										{
											if (item4.Value.Has("serverSynced") && (bool)item4.Value["serverSynced"])
											{
												jsonWriter.WritePropertyName(item4.Key);
												JsonData jsonData = item4.Value["value"];
												if (jsonData.IsBoolean)
												{
													jsonWriter.Write((bool)jsonData);
												}
												if (jsonData.IsInt)
												{
													jsonWriter.Write((int)jsonData);
												}
												if (jsonData.IsString)
												{
													jsonWriter.Write((string)jsonData);
												}
											}
										}
										jsonWriter.WriteObjectEnd();
										if (stringBuilder2.ToString() != stringBuilder.ToString())
										{
											num4++;
											binaryWriter.Write(item3.Key);
											binaryWriter.Write(stringBuilder2.ToString());
										}
									}
								}
								long position2 = memoryStream.Position;
								memoryStream.Position = position;
								binaryWriter.Write((byte)num4);
								num2 += (int)(position2 - position) + 1;
								num3 += (int)(position2 - position);
								Buffer.BlockCopy(BitConverter.GetBytes(num2 - 4), 0, buffer[num].writeBuffer, 0, 4);
							}
						}
						break;
					}
					case 100:
					{
						MemoryStream memoryStream5 = new MemoryStream();
						BinaryWriter binaryWriter4 = new BinaryWriter(memoryStream5);
						object value2 = null;
						if (Config.globalMod["ModWorld"].TryGetValue(Config.mods[number], out value2))
						{
							Codable.RunSpecifiedMethod("ModWorld " + Config.mods[number] + " NetSend", value2, "NetSend", (int)number2, binaryWriter4);
						}
						num2 += memoryStream5.ToArray().Length;
						num2 += 2;
						byte[] bytes261 = BitConverter.GetBytes(msgType);
						byte[] bytes262 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes262, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes261, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = (byte)number;
						num3++;
						buffer[num].writeBuffer[num3] = (byte)number2;
						num3++;
						if (memoryStream5.ToArray().Length > 0)
						{
							Buffer.BlockCopy(memoryStream5.ToArray(), 0, buffer[num].writeBuffer, num3, memoryStream5.ToArray().Length);
						}
						binaryWriter4.Close();
						memoryStream5.Close();
						break;
					}
					case 8:
					{
						byte[] bytes209 = BitConverter.GetBytes(msgType);
						byte[] bytes210 = BitConverter.GetBytes(number);
						byte[] bytes211 = BitConverter.GetBytes((int)number2);
						num2 += bytes210.Length + bytes211.Length;
						byte[] bytes212 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes212, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes209, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes210, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes211, 0, buffer[num].writeBuffer, num3, 4);
						break;
					}
					case 9:
					{
						byte[] bytes234 = BitConverter.GetBytes(msgType);
						byte[] bytes235 = BitConverter.GetBytes(number);
						byte[] bytes236 = Encoding.UTF8.GetBytes(text);
						num2 += bytes235.Length + bytes236.Length;
						byte[] bytes237 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes237, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes234, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes235, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes236, 0, buffer[num].writeBuffer, num3, bytes236.Length);
						break;
					}
					case 10:
					{
						short num5 = (short)number;
						int num6 = (int)number2;
						int num7 = (int)number3;
						byte[] bytes161 = BitConverter.GetBytes(msgType);
						Buffer.BlockCopy(bytes161, 0, buffer[num].writeBuffer, 4, 1);
						byte[] bytes162 = BitConverter.GetBytes(num5);
						Buffer.BlockCopy(bytes162, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						byte[] bytes163 = BitConverter.GetBytes(num6);
						Buffer.BlockCopy(bytes163, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						byte[] bytes164 = BitConverter.GetBytes(num7);
						Buffer.BlockCopy(bytes164, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						int num8;
						for (num8 = num6; num8 < num6 + num5; num8++)
						{
							byte b48 = 0;
							if (Main.tile[num8, num7].active)
							{
								b48 = (byte)(b48 + 1);
							}
							if (Main.tile[num8, num7].wall > 0)
							{
								b48 = (byte)(b48 + 4);
							}
							if (Main.tile[num8, num7].liquid > 0)
							{
								b48 = (byte)(b48 + 8);
							}
							if (Main.tile[num8, num7].wire)
							{
								b48 = (byte)(b48 + 16);
							}
							buffer[num].writeBuffer[num3] = b48;
							num3++;
							byte[] bytes165 = BitConverter.GetBytes(Main.tile[num8, num7].frameX);
							byte[] bytes166 = BitConverter.GetBytes(Main.tile[num8, num7].frameY);
							if (Main.tile[num8, num7].active)
							{
								byte[] bytes167 = BitConverter.GetBytes(Main.tile[num8, num7].type);
								Buffer.BlockCopy(bytes167, 0, buffer[num].writeBuffer, num3, 2);
								num3 += 2;
								if (Main.tileFrameImportant[Main.tile[num8, num7].type])
								{
									Buffer.BlockCopy(bytes165, 0, buffer[num].writeBuffer, num3, 2);
									num3 += 2;
									Buffer.BlockCopy(bytes166, 0, buffer[num].writeBuffer, num3, 2);
									num3 += 2;
									if (Main.tile[num8, num7].type >= 150)
									{
										buffer[num].writeBuffer[num3] = Main.tile[num8, num7].frameNumber;
										num3++;
									}
								}
							}
							if (Main.tile[num8, num7].wall > 0)
							{
								byte[] bytes168 = BitConverter.GetBytes(Main.tile[num8, num7].wall);
								Buffer.BlockCopy(bytes168, 0, buffer[num].writeBuffer, num3, 2);
								num3 += 2;
							}
							if (Main.tile[num8, num7].liquid > 0)
							{
								buffer[num].writeBuffer[num3] = Main.tile[num8, num7].liquid;
								num3++;
								byte b49 = 0;
								if (Main.tile[num8, num7].lava)
								{
									b49 = 1;
								}
								buffer[num].writeBuffer[num3] = b49;
								num3++;
							}
							short num9 = 1;
							while (num8 + num9 < num6 + num5 && Main.tile[num8, num7].isTheSameAs(Main.tile[num8 + num9, num7]))
							{
								num9 = (short)(num9 + 1);
							}
							num9 = (short)(num9 - 1);
							byte[] bytes169 = BitConverter.GetBytes(num9);
							Buffer.BlockCopy(bytes169, 0, buffer[num].writeBuffer, num3, 2);
							num3 += 2;
							num8 += num9;
						}
						byte[] bytes170 = BitConverter.GetBytes(num3 - 4);
						Buffer.BlockCopy(bytes170, 0, buffer[num].writeBuffer, 0, 4);
						num2 = num3;
						break;
					}
					case 11:
					{
						byte[] bytes203 = BitConverter.GetBytes(msgType);
						byte[] bytes204 = BitConverter.GetBytes(number);
						byte[] bytes205 = BitConverter.GetBytes((int)number2);
						byte[] bytes206 = BitConverter.GetBytes((int)number3);
						byte[] bytes207 = BitConverter.GetBytes((int)number4);
						num2 += bytes204.Length + bytes205.Length + bytes206.Length + bytes207.Length;
						byte[] bytes208 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes208, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes203, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes204, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes205, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes206, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes207, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						break;
					}
					case 12:
					{
						byte[] bytes145 = BitConverter.GetBytes(msgType);
						byte b43 = (byte)number;
						byte[] bytes146 = BitConverter.GetBytes(Main.player[b43].SpawnX);
						byte[] bytes147 = BitConverter.GetBytes(Main.player[b43].SpawnY);
						num2 += 1 + bytes146.Length + bytes147.Length;
						byte[] bytes148 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes148, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes145, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b43;
						num3++;
						Buffer.BlockCopy(bytes146, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes147, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						break;
					}
					case 13:
					{
						byte[] bytes155 = BitConverter.GetBytes(msgType);
						byte b45 = (byte)number;
						byte b46 = 0;
						if (Main.player[b45].controlUp)
						{
							b46 = (byte)(b46 + 1);
						}
						if (Main.player[b45].controlDown)
						{
							b46 = (byte)(b46 + 2);
						}
						if (Main.player[b45].controlLeft)
						{
							b46 = (byte)(b46 + 4);
						}
						if (Main.player[b45].controlRight)
						{
							b46 = (byte)(b46 + 8);
						}
						if (Main.player[b45].controlJump)
						{
							b46 = (byte)(b46 + 16);
						}
						if (Main.player[b45].controlUseItem)
						{
							b46 = (byte)(b46 + 32);
						}
						if (Main.player[b45].direction == 1)
						{
							b46 = (byte)(b46 + 64);
						}
						byte b47 = (byte)Main.player[b45].selectedItem;
						byte[] bytes156 = BitConverter.GetBytes(Main.player[number].position.X);
						byte[] bytes157 = BitConverter.GetBytes(Main.player[number].position.Y);
						byte[] bytes158 = BitConverter.GetBytes(Main.player[number].velocity.X);
						byte[] bytes159 = BitConverter.GetBytes(Main.player[number].velocity.Y);
						num2 += 3 + bytes156.Length + bytes157.Length + bytes158.Length + bytes159.Length;
						byte[] bytes160 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes160, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes155, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b45;
						num3++;
						buffer[num].writeBuffer[6] = b46;
						num3++;
						buffer[num].writeBuffer[7] = b47;
						num3++;
						Buffer.BlockCopy(bytes156, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes157, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes158, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes159, 0, buffer[num].writeBuffer, num3, 4);
						break;
					}
					case 14:
					{
						byte[] bytes143 = BitConverter.GetBytes(msgType);
						byte b41 = (byte)number;
						byte b42 = (byte)number2;
						num2 += 2;
						byte[] bytes144 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes144, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes143, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b41;
						buffer[num].writeBuffer[6] = b42;
						break;
					}
					case 15:
					{
						byte[] bytes141 = BitConverter.GetBytes(msgType);
						byte[] bytes142 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes142, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes141, 0, buffer[num].writeBuffer, 4, 1);
						break;
					}
					case 16:
					{
						byte[] bytes149 = BitConverter.GetBytes(msgType);
						byte b44 = (byte)number;
						byte[] bytes150 = BitConverter.GetBytes((short)Main.player[b44].statLife);
						byte[] bytes151 = BitConverter.GetBytes((short)Main.player[b44].statLifeMax);
						num2 += 1 + bytes150.Length + bytes151.Length;
						byte[] bytes152 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes152, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes149, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b44;
						num3++;
						Buffer.BlockCopy(bytes150, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						Buffer.BlockCopy(bytes151, 0, buffer[num].writeBuffer, num3, 2);
						break;
					}
					case 17:
					{
						byte[] bytes136 = BitConverter.GetBytes(msgType);
						byte b40 = (byte)number;
						byte[] bytes137 = BitConverter.GetBytes((int)number2);
						byte[] bytes138 = BitConverter.GetBytes((int)number3);
						num2 += 1 + bytes137.Length + bytes138.Length + 2 + 1;
						byte[] bytes139 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes139, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes136, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b40;
						num3++;
						Buffer.BlockCopy(bytes137, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes138, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						byte[] bytes140 = BitConverter.GetBytes((ushort)number4);
						Buffer.BlockCopy(bytes140, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						buffer[num].writeBuffer[num3] = (byte)number5;
						break;
					}
					case 18:
					{
						byte[] bytes33 = BitConverter.GetBytes(msgType);
						BitConverter.GetBytes((int)Main.time);
						byte b5 = 0;
						if (Main.dayTime)
						{
							b5 = 1;
						}
						byte[] bytes34 = BitConverter.GetBytes((int)Main.time);
						byte[] bytes35 = BitConverter.GetBytes(Main.sunModY);
						byte[] bytes36 = BitConverter.GetBytes(Main.moonModY);
						num2 += 1 + bytes34.Length + bytes35.Length + bytes36.Length;
						byte[] bytes37 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes37, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes33, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b5;
						num3++;
						Buffer.BlockCopy(bytes34, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes35, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						Buffer.BlockCopy(bytes36, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						break;
					}
					case 19:
					{
						byte[] bytes263 = BitConverter.GetBytes(msgType);
						byte b65 = (byte)number;
						byte[] bytes264 = BitConverter.GetBytes((int)number2);
						byte[] bytes265 = BitConverter.GetBytes((int)number3);
						byte b66 = 0;
						if (number4 == 1f)
						{
							b66 = 1;
						}
						num2 += 1 + bytes264.Length + bytes265.Length + 1;
						byte[] bytes266 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes266, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes263, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b65;
						num3++;
						Buffer.BlockCopy(bytes264, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes265, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						buffer[num].writeBuffer[num3] = b66;
						break;
					}
					case 20:
					{
						short num10 = (short)number;
						int num11 = (int)number2;
						int num12 = (int)number3;
						byte[] bytes216 = BitConverter.GetBytes(msgType);
						Buffer.BlockCopy(bytes216, 0, buffer[num].writeBuffer, 4, 1);
						byte[] bytes217 = BitConverter.GetBytes(num10);
						Buffer.BlockCopy(bytes217, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						byte[] bytes218 = BitConverter.GetBytes(num11);
						Buffer.BlockCopy(bytes218, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						byte[] bytes219 = BitConverter.GetBytes(num12);
						Buffer.BlockCopy(bytes219, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						for (int l = num11; l < num11 + num10; l++)
						{
							for (int m = num12; m < num12 + num10; m++)
							{
								byte b56 = 0;
								if (Main.tile[l, m].active)
								{
									b56 = (byte)(b56 + 1);
								}
								if (Main.tile[l, m].wall > 0)
								{
									b56 = (byte)(b56 + 4);
								}
								if (Main.tile[l, m].liquid > 0 && Main.netMode == 2)
								{
									b56 = (byte)(b56 + 8);
								}
								if (Main.tile[l, m].wire)
								{
									b56 = (byte)(b56 + 16);
								}
								buffer[num].writeBuffer[num3] = b56;
								num3++;
								byte[] bytes220 = BitConverter.GetBytes(Main.tile[l, m].frameX);
								byte[] bytes221 = BitConverter.GetBytes(Main.tile[l, m].frameY);
								if (Main.tile[l, m].active)
								{
									byte[] bytes222 = BitConverter.GetBytes(Main.tile[l, m].type);
									Buffer.BlockCopy(bytes222, 0, buffer[num].writeBuffer, num3, 2);
									num3 += 2;
									if (Main.tileFrameImportant[Main.tile[l, m].type])
									{
										Buffer.BlockCopy(bytes220, 0, buffer[num].writeBuffer, num3, 2);
										num3 += 2;
										Buffer.BlockCopy(bytes221, 0, buffer[num].writeBuffer, num3, 2);
										num3 += 2;
										if (Main.tile[l, m].type >= 150)
										{
											buffer[num].writeBuffer[num3] = Main.tile[l, m].frameNumber;
											num3++;
										}
									}
								}
								if (Main.tile[l, m].wall > 0)
								{
									byte[] bytes223 = BitConverter.GetBytes(Main.tile[l, m].wall);
									Buffer.BlockCopy(bytes223, 0, buffer[num].writeBuffer, num3, 2);
									num3 += 2;
								}
								if (Main.tile[l, m].liquid > 0 && Main.netMode == 2)
								{
									buffer[num].writeBuffer[num3] = Main.tile[l, m].liquid;
									num3++;
									byte b57 = 0;
									if (Main.tile[l, m].lava)
									{
										b57 = 1;
									}
									buffer[num].writeBuffer[num3] = b57;
									num3++;
								}
							}
						}
						byte[] bytes224 = BitConverter.GetBytes(num3 - 4);
						Buffer.BlockCopy(bytes224, 0, buffer[num].writeBuffer, 0, 4);
						num2 = num3;
						break;
					}
					case 21:
					{
						MemoryStream memoryStream2 = new MemoryStream();
						BinaryWriter writer = new BinaryWriter(memoryStream2);
						Codable.SaveCustomData(Main.item[number], writer, net: true);
						num2 += memoryStream2.ToArray().Length;
						byte[] bytes183 = BitConverter.GetBytes(msgType);
						byte[] bytes184 = BitConverter.GetBytes((short)number);
						byte[] bytes185 = BitConverter.GetBytes(Main.item[number].position.X);
						byte[] bytes186 = BitConverter.GetBytes(Main.item[number].position.Y);
						byte[] bytes187 = BitConverter.GetBytes(Main.item[number].velocity.X);
						byte[] bytes188 = BitConverter.GetBytes(Main.item[number].velocity.Y);
						byte[] bytes189 = BitConverter.GetBytes(Main.item[number].stack);
						byte prefix = Main.item[number].prefix;
						short value = 0;
						if (Main.item[number].active && Main.item[number].stack > 0)
						{
							value = (short)Main.item[number].netID;
						}
						byte[] bytes190 = BitConverter.GetBytes(value);
						num2 += bytes184.Length + bytes185.Length + bytes186.Length + bytes187.Length + bytes188.Length + 1 + bytes190.Length + bytes189.Length;
						byte[] bytes191 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes191, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes183, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes184, 0, buffer[num].writeBuffer, num3, bytes184.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes185, 0, buffer[num].writeBuffer, num3, bytes185.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes186, 0, buffer[num].writeBuffer, num3, bytes186.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes187, 0, buffer[num].writeBuffer, num3, bytes187.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes188, 0, buffer[num].writeBuffer, num3, bytes188.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes189, 0, buffer[num].writeBuffer, num3, bytes189.Length);
						num3 += 4;
						buffer[num].writeBuffer[num3] = prefix;
						num3++;
						Buffer.BlockCopy(bytes190, 0, buffer[num].writeBuffer, num3, bytes190.Length);
						num3 += bytes190.Length;
						if (memoryStream2.ToArray().Length > 0)
						{
							Buffer.BlockCopy(memoryStream2.ToArray(), 0, buffer[num].writeBuffer, num3, memoryStream2.ToArray().Length);
						}
						break;
					}
					case 22:
					{
						byte[] bytes196 = BitConverter.GetBytes(msgType);
						byte[] bytes197 = BitConverter.GetBytes((short)number);
						byte b51 = (byte)Main.item[number].owner;
						num2 += bytes197.Length + 1;
						byte[] bytes198 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes198, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes196, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes197, 0, buffer[num].writeBuffer, num3, bytes197.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b51;
						break;
					}
					case 23:
					{
						byte[] bytes117 = BitConverter.GetBytes(msgType);
						byte[] bytes118 = BitConverter.GetBytes((short)number);
						byte[] bytes119 = BitConverter.GetBytes(Main.npc[number].position.X);
						byte[] bytes120 = BitConverter.GetBytes(Main.npc[number].position.Y);
						byte[] bytes121 = BitConverter.GetBytes(Main.npc[number].velocity.X);
						byte[] bytes122 = BitConverter.GetBytes(Main.npc[number].velocity.Y);
						byte[] bytes123 = BitConverter.GetBytes((short)Main.npc[number].target);
						byte[] bytes124 = BitConverter.GetBytes(Main.npc[number].life);
						if (!Main.npc[number].active)
						{
							bytes124 = BitConverter.GetBytes(0);
						}
						if (!Main.npc[number].active || Main.npc[number].life <= 0)
						{
							Main.npc[number].netSkip = 0;
						}
						if (Main.npc[number].name == null)
						{
							Main.npc[number].name = "";
						}
						byte[] bytes125 = BitConverter.GetBytes((short)Main.npc[number].netID);
						num2 += bytes118.Length + bytes119.Length + bytes120.Length + bytes121.Length + bytes122.Length + bytes123.Length + bytes124.Length + NPC.maxAI * 4 + bytes125.Length + 1 + 1;
						byte[] bytes126 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes126, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes117, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes118, 0, buffer[num].writeBuffer, num3, bytes118.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes119, 0, buffer[num].writeBuffer, num3, bytes119.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes120, 0, buffer[num].writeBuffer, num3, bytes120.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes121, 0, buffer[num].writeBuffer, num3, bytes121.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes122, 0, buffer[num].writeBuffer, num3, bytes122.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes123, 0, buffer[num].writeBuffer, num3, bytes123.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].direction + 1);
						num3++;
						buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].directionY + 1);
						num3++;
						Buffer.BlockCopy(bytes124, 0, buffer[num].writeBuffer, num3, bytes124.Length);
						num3 += 4;
						for (int k = 0; k < NPC.maxAI; k++)
						{
							byte[] bytes127 = BitConverter.GetBytes(Main.npc[number].ai[k]);
							Buffer.BlockCopy(bytes127, 0, buffer[num].writeBuffer, num3, bytes127.Length);
							num3 += 4;
						}
						Buffer.BlockCopy(bytes125, 0, buffer[num].writeBuffer, num3, bytes125.Length);
						num3 += bytes125.Length;
						break;
					}
					case 24:
					{
						byte[] bytes128 = BitConverter.GetBytes(msgType);
						byte[] bytes129 = BitConverter.GetBytes((short)number);
						byte b39 = (byte)number2;
						num2 += bytes129.Length + 1;
						byte[] bytes130 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes130, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes128, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes129, 0, buffer[num].writeBuffer, num3, bytes129.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b39;
						break;
					}
					case 25:
					{
						byte[] bytes69 = BitConverter.GetBytes(msgType);
						byte b17 = (byte)number;
						byte[] bytes70 = Encoding.UTF8.GetBytes(text);
						byte b18 = (byte)number2;
						byte b19 = (byte)number3;
						byte b20 = (byte)number4;
						num2 += 1 + bytes70.Length + 3;
						byte[] bytes71 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes71, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes69, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b17;
						num3++;
						buffer[num].writeBuffer[num3] = b18;
						num3++;
						buffer[num].writeBuffer[num3] = b19;
						num3++;
						buffer[num].writeBuffer[num3] = b20;
						num3++;
						Buffer.BlockCopy(bytes70, 0, buffer[num].writeBuffer, num3, bytes70.Length);
						break;
					}
					case 26:
					{
						byte[] bytes72 = BitConverter.GetBytes(msgType);
						byte b21 = (byte)number;
						byte b22 = (byte)(number2 + 1f);
						byte[] bytes73 = BitConverter.GetBytes(number3);
						byte[] bytes74 = Encoding.UTF8.GetBytes(text);
						byte b23 = (byte)number4;
						byte[] bytes75 = BitConverter.GetBytes((short)number5);
						num2 += 2 + bytes73.Length + 1 + bytes74.Length + 2;
						byte[] bytes76 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes76, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes72, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b21;
						num3++;
						buffer[num].writeBuffer[num3] = b22;
						num3++;
						Buffer.BlockCopy(bytes73, 0, buffer[num].writeBuffer, num3, bytes73.Length);
						num3 += 4;
						buffer[num].writeBuffer[num3] = b23;
						num3++;
						Buffer.BlockCopy(bytes75, 0, buffer[num].writeBuffer, num3, bytes75.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes74, 0, buffer[num].writeBuffer, num3, bytes74.Length);
						break;
					}
					case 27:
					{
						byte[] bytes247 = BitConverter.GetBytes(msgType);
						byte[] bytes248 = BitConverter.GetBytes((short)Main.projectile[number].identity);
						byte[] bytes249 = BitConverter.GetBytes(Main.projectile[number].position.X);
						byte[] bytes250 = BitConverter.GetBytes(Main.projectile[number].position.Y);
						byte[] bytes251 = BitConverter.GetBytes(Main.projectile[number].velocity.X);
						byte[] bytes252 = BitConverter.GetBytes(Main.projectile[number].velocity.Y);
						byte[] bytes253 = BitConverter.GetBytes(Main.projectile[number].knockBack);
						byte[] bytes254 = BitConverter.GetBytes((short)Main.projectile[number].damage);
						byte[] bytes255 = BitConverter.GetBytes((short)Main.projectile[number].type);
						Buffer.BlockCopy(bytes247, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes248, 0, buffer[num].writeBuffer, num3, bytes248.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes249, 0, buffer[num].writeBuffer, num3, bytes249.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes250, 0, buffer[num].writeBuffer, num3, bytes250.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes251, 0, buffer[num].writeBuffer, num3, bytes251.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes252, 0, buffer[num].writeBuffer, num3, bytes252.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes253, 0, buffer[num].writeBuffer, num3, bytes253.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes254, 0, buffer[num].writeBuffer, num3, bytes254.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = (byte)Main.projectile[number].owner;
						num3++;
						Buffer.BlockCopy(bytes255, 0, buffer[num].writeBuffer, num3, bytes255.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = (byte)((Main.projectile[number].CallbackItem != null) ? 1 : 0);
						num3++;
						for (int n = 0; n < Projectile.maxAI; n++)
						{
							byte[] bytes256 = BitConverter.GetBytes(Main.projectile[number].ai[n]);
							Buffer.BlockCopy(bytes256, 0, buffer[num].writeBuffer, num3, bytes256.Length);
							num3 += 4;
						}
						num2 += num3;
						byte[] bytes257 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes257, 0, buffer[num].writeBuffer, 0, 4);
						break;
					}
					case 28:
					{
						byte[] bytes241 = BitConverter.GetBytes(msgType);
						byte[] bytes242 = BitConverter.GetBytes((short)number);
						byte[] bytes243 = BitConverter.GetBytes(number2);
						byte[] bytes244 = BitConverter.GetBytes(number3);
						byte b61 = (byte)(number4 + 1f);
						byte[] bytes245 = BitConverter.GetBytes((short)number5);
						num2 += bytes242.Length + bytes243.Length + bytes244.Length + 1 + 2;
						byte[] bytes246 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes246, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes241, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes242, 0, buffer[num].writeBuffer, num3, bytes242.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes243, 0, buffer[num].writeBuffer, num3, bytes243.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes244, 0, buffer[num].writeBuffer, num3, bytes244.Length);
						num3 += 4;
						buffer[num].writeBuffer[num3] = b61;
						num3++;
						Buffer.BlockCopy(bytes245, 0, buffer[num].writeBuffer, num3, bytes245.Length);
						break;
					}
					case 29:
					{
						byte[] bytes238 = BitConverter.GetBytes(msgType);
						byte[] bytes239 = BitConverter.GetBytes((short)number);
						byte b60 = (byte)number2;
						num2 += bytes239.Length + 1;
						byte[] bytes240 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes240, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes238, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes239, 0, buffer[num].writeBuffer, num3, bytes239.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b60;
						break;
					}
					case 30:
					{
						byte[] bytes229 = BitConverter.GetBytes(msgType);
						byte b58 = (byte)number;
						byte b59 = 0;
						if (Main.player[b58].hostile)
						{
							b59 = 1;
						}
						num2 += 2;
						byte[] bytes230 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes230, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes229, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b58;
						num3++;
						buffer[num].writeBuffer[num3] = b59;
						break;
					}
					case 31:
					{
						byte[] bytes225 = BitConverter.GetBytes(msgType);
						byte[] bytes226 = BitConverter.GetBytes(number);
						byte[] bytes227 = BitConverter.GetBytes((int)number2);
						num2 += bytes226.Length + bytes227.Length;
						byte[] bytes228 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes228, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes225, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes226, 0, buffer[num].writeBuffer, num3, bytes226.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes227, 0, buffer[num].writeBuffer, num3, bytes227.Length);
						break;
					}
					case 32:
					{
						byte[] bytes199 = BitConverter.GetBytes(msgType);
						byte[] bytes200 = BitConverter.GetBytes((short)number);
						byte b52 = (byte)number2;
						byte[] bytes201 = BitConverter.GetBytes(Main.chest[number].item[(int)number2].stack);
						byte prefix2 = Main.chest[number].item[(int)number2].prefix;
						byte[] array2 = (Main.chest[number].item[(int)number2].name != null) ? BitConverter.GetBytes((short)Main.chest[number].item[(int)number2].netID) : BitConverter.GetBytes(0);
						Item item = Main.chest[number].item[(int)number2];
						MemoryStream memoryStream3 = new MemoryStream();
						BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream3);
						Codable.SaveCustomData(item, binaryWriter2, net: true);
						num2 += memoryStream3.ToArray().Length;
						binaryWriter2.Close();
						num2 += bytes200.Length + 1 + 4 + 1 + array2.Length;
						byte[] bytes202 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes202, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes199, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes200, 0, buffer[num].writeBuffer, num3, bytes200.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b52;
						num3++;
						Buffer.BlockCopy(bytes201, 0, buffer[num].writeBuffer, num3, bytes201.Length);
						num3 += 4;
						buffer[num].writeBuffer[num3] = prefix2;
						num3++;
						Buffer.BlockCopy(array2, 0, buffer[num].writeBuffer, num3, array2.Length);
						num3 += array2.Length;
						if (memoryStream3.ToArray().Length > 0)
						{
							Buffer.BlockCopy(memoryStream3.ToArray(), 0, buffer[num].writeBuffer, num3, memoryStream3.ToArray().Length);
						}
						break;
					}
					case 33:
					{
						byte[] bytes178 = BitConverter.GetBytes(msgType);
						byte[] bytes179 = BitConverter.GetBytes((short)number);
						byte[] bytes180;
						byte[] bytes181;
						if (number > -1)
						{
							bytes180 = BitConverter.GetBytes(Main.chest[number].x);
							bytes181 = BitConverter.GetBytes(Main.chest[number].y);
						}
						else
						{
							bytes180 = BitConverter.GetBytes(0);
							bytes181 = BitConverter.GetBytes(0);
						}
						num2 += bytes179.Length + bytes180.Length + bytes181.Length;
						byte[] bytes182 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes182, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes178, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes179, 0, buffer[num].writeBuffer, num3, bytes179.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes180, 0, buffer[num].writeBuffer, num3, bytes180.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes181, 0, buffer[num].writeBuffer, num3, bytes181.Length);
						break;
					}
					case 34:
					{
						byte[] bytes174 = BitConverter.GetBytes(msgType);
						byte[] bytes175 = BitConverter.GetBytes(number);
						byte[] bytes176 = BitConverter.GetBytes((int)number2);
						num2 += bytes175.Length + bytes176.Length;
						byte[] bytes177 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes177, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes174, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes175, 0, buffer[num].writeBuffer, num3, bytes175.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes176, 0, buffer[num].writeBuffer, num3, bytes176.Length);
						break;
					}
					case 35:
					{
						byte[] bytes171 = BitConverter.GetBytes(msgType);
						byte b50 = (byte)number;
						byte[] bytes172 = BitConverter.GetBytes((short)number2);
						num2 += 1 + bytes172.Length;
						byte[] bytes173 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes173, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes171, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b50;
						num3++;
						Buffer.BlockCopy(bytes172, 0, buffer[num].writeBuffer, num3, 2);
						break;
					}
					case 36:
					{
						byte[] bytes115 = BitConverter.GetBytes(msgType);
						byte b33 = (byte)number;
						byte b34 = 0;
						if (Main.player[b33].zoneEvil)
						{
							b34 = 1;
						}
						byte b35 = 0;
						if (Main.player[b33].zoneMeteor)
						{
							b35 = 1;
						}
						byte b36 = 0;
						if (Main.player[b33].zoneDungeon)
						{
							b36 = 1;
						}
						byte b37 = 0;
						if (Main.player[b33].zoneJungle)
						{
							b37 = 1;
						}
						byte b38 = 0;
						if (Main.player[b33].zoneHoly)
						{
							b38 = 1;
						}
						num2 += 5;
						num2 += Main.player[b33].zone.Keys.Count + 1;
						byte[] bytes116 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes116, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes115, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b33;
						num3++;
						foreach (string key in Biome.Biomes.Keys)
						{
							buffer[num].writeBuffer[num3] = (byte)(Main.player[b33].zone[key] ? 1 : 0);
							num3++;
						}
						buffer[num].writeBuffer[num3] = b34;
						num3++;
						buffer[num].writeBuffer[num3] = b35;
						num3++;
						buffer[num].writeBuffer[num3] = b36;
						num3++;
						buffer[num].writeBuffer[num3] = b37;
						num3++;
						buffer[num].writeBuffer[num3] = b38;
						num3++;
						break;
					}
					case 37:
					{
						byte[] bytes113 = BitConverter.GetBytes(msgType);
						byte[] bytes114 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes114, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes113, 0, buffer[num].writeBuffer, 4, 1);
						break;
					}
					case 38:
					{
						byte[] bytes110 = BitConverter.GetBytes(msgType);
						byte[] bytes111 = Encoding.UTF8.GetBytes(text);
						num2 += bytes111.Length;
						byte[] bytes112 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes112, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes110, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes111, 0, buffer[num].writeBuffer, num3, bytes111.Length);
						break;
					}
					case 39:
					{
						byte[] bytes107 = BitConverter.GetBytes(msgType);
						byte[] bytes108 = BitConverter.GetBytes((short)number);
						num2 += bytes108.Length;
						byte[] bytes109 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes109, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes107, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes108, 0, buffer[num].writeBuffer, num3, bytes108.Length);
						break;
					}
					case 40:
					{
						byte[] bytes104 = BitConverter.GetBytes(msgType);
						byte b32 = (byte)number;
						byte[] bytes105 = BitConverter.GetBytes((short)Main.player[b32].talkNPC);
						num2 += 1 + bytes105.Length;
						byte[] bytes106 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes106, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes104, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b32;
						num3++;
						Buffer.BlockCopy(bytes105, 0, buffer[num].writeBuffer, num3, bytes105.Length);
						num3 += 2;
						break;
					}
					case 41:
					{
						byte[] bytes100 = BitConverter.GetBytes(msgType);
						byte b31 = (byte)number;
						byte[] bytes101 = BitConverter.GetBytes(Main.player[b31].itemRotation);
						byte[] bytes102 = BitConverter.GetBytes((short)Main.player[b31].itemAnimation);
						num2 += 1 + bytes101.Length + bytes102.Length;
						byte[] bytes103 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes103, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes100, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b31;
						num3++;
						Buffer.BlockCopy(bytes101, 0, buffer[num].writeBuffer, num3, bytes101.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes102, 0, buffer[num].writeBuffer, num3, bytes102.Length);
						break;
					}
					case 42:
					{
						byte[] bytes96 = BitConverter.GetBytes(msgType);
						byte b30 = (byte)number;
						byte[] bytes97 = BitConverter.GetBytes((short)Main.player[b30].statMana);
						byte[] bytes98 = BitConverter.GetBytes((short)Main.player[b30].statManaMax);
						num2 += 1 + bytes97.Length + bytes98.Length;
						byte[] bytes99 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes99, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes96, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b30;
						num3++;
						Buffer.BlockCopy(bytes97, 0, buffer[num].writeBuffer, num3, 2);
						num3 += 2;
						Buffer.BlockCopy(bytes98, 0, buffer[num].writeBuffer, num3, 2);
						break;
					}
					case 43:
					{
						byte[] bytes93 = BitConverter.GetBytes(msgType);
						byte b29 = (byte)number;
						byte[] bytes94 = BitConverter.GetBytes((short)number2);
						num2 += 1 + bytes94.Length;
						byte[] bytes95 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes95, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes93, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b29;
						num3++;
						Buffer.BlockCopy(bytes94, 0, buffer[num].writeBuffer, num3, 2);
						break;
					}
					case 44:
					{
						byte[] bytes89 = BitConverter.GetBytes(msgType);
						byte b26 = (byte)number;
						byte b27 = (byte)(number2 + 1f);
						byte[] bytes90 = BitConverter.GetBytes((short)number3);
						byte b28 = (byte)number4;
						byte[] bytes91 = Encoding.UTF8.GetBytes(text);
						num2 += 2 + bytes90.Length + 1 + bytes91.Length;
						byte[] bytes92 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes92, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes89, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b26;
						num3++;
						buffer[num].writeBuffer[num3] = b27;
						num3++;
						Buffer.BlockCopy(bytes90, 0, buffer[num].writeBuffer, num3, bytes90.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b28;
						num3++;
						Buffer.BlockCopy(bytes91, 0, buffer[num].writeBuffer, num3, bytes91.Length);
						num3 += bytes91.Length;
						break;
					}
					case 45:
					{
						byte[] bytes87 = BitConverter.GetBytes(msgType);
						byte b24 = (byte)number;
						byte b25 = (byte)Main.player[b24].team;
						num2 += 2;
						byte[] bytes88 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes88, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes87, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[5] = b24;
						num3++;
						buffer[num].writeBuffer[num3] = b25;
						break;
					}
					case 46:
					{
						byte[] bytes83 = BitConverter.GetBytes(msgType);
						byte[] bytes84 = BitConverter.GetBytes(number);
						byte[] bytes85 = BitConverter.GetBytes((int)number2);
						num2 += bytes84.Length + bytes85.Length;
						byte[] bytes86 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes86, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes83, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes84, 0, buffer[num].writeBuffer, num3, bytes84.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes85, 0, buffer[num].writeBuffer, num3, bytes85.Length);
						break;
					}
					case 47:
					{
						byte[] bytes77 = BitConverter.GetBytes(msgType);
						byte[] bytes78 = BitConverter.GetBytes((short)number);
						byte[] bytes79 = BitConverter.GetBytes(Main.sign[number].x);
						byte[] bytes80 = BitConverter.GetBytes(Main.sign[number].y);
						byte[] bytes81 = Encoding.UTF8.GetBytes(Main.sign[number].text);
						num2 += bytes78.Length + bytes79.Length + bytes80.Length + bytes81.Length;
						byte[] bytes82 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes82, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes77, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes78, 0, buffer[num].writeBuffer, num3, bytes78.Length);
						num3 += bytes78.Length;
						Buffer.BlockCopy(bytes79, 0, buffer[num].writeBuffer, num3, bytes79.Length);
						num3 += bytes79.Length;
						Buffer.BlockCopy(bytes80, 0, buffer[num].writeBuffer, num3, bytes80.Length);
						num3 += bytes80.Length;
						Buffer.BlockCopy(bytes81, 0, buffer[num].writeBuffer, num3, bytes81.Length);
						num3 += bytes81.Length;
						break;
					}
					case 48:
					{
						byte[] bytes65 = BitConverter.GetBytes(msgType);
						byte[] bytes66 = BitConverter.GetBytes(number);
						byte[] bytes67 = BitConverter.GetBytes((int)number2);
						byte liquid = Main.tile[number, (int)number2].liquid;
						byte b16 = 0;
						if (Main.tile[number, (int)number2].lava)
						{
							b16 = 1;
						}
						num2 += bytes66.Length + bytes67.Length + 1 + 1;
						byte[] bytes68 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes68, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes65, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes66, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes67, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						buffer[num].writeBuffer[num3] = liquid;
						num3++;
						buffer[num].writeBuffer[num3] = b16;
						num3++;
						break;
					}
					case 49:
					{
						byte[] bytes63 = BitConverter.GetBytes(msgType);
						byte[] bytes64 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes64, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes63, 0, buffer[num].writeBuffer, 4, 1);
						break;
					}
					case 50:
					{
						byte[] bytes48 = BitConverter.GetBytes(msgType);
						byte b11 = (byte)number;
						num2 += 2 + Main.player[b11].buffType.Length;
						byte[] bytes49 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes49, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes48, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b11;
						num3++;
						buffer[num].writeBuffer[num3++] = (byte)Main.player[b11].buffType.Length;
						for (int j = 0; j < Main.player[b11].buffType.Length; j++)
						{
							buffer[num].writeBuffer[num3] = (byte)Main.player[b11].buffType[j];
							num3++;
						}
						break;
					}
					case 51:
					{
						byte[] bytes46 = BitConverter.GetBytes(msgType);
						num2 += 2;
						byte b9 = (byte)number;
						byte b10 = (byte)number2;
						byte[] bytes47 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes47, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes46, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b9;
						num3++;
						buffer[num].writeBuffer[num3] = b10;
						break;
					}
					case 52:
					{
						byte[] bytes42 = BitConverter.GetBytes(msgType);
						byte b7 = (byte)number;
						byte b8 = (byte)number2;
						byte[] bytes43 = BitConverter.GetBytes((int)number3);
						byte[] bytes44 = BitConverter.GetBytes((int)number4);
						num2 += 2 + bytes43.Length + bytes44.Length;
						byte[] bytes45 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes45, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes42, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b7;
						num3++;
						buffer[num].writeBuffer[num3] = b8;
						num3++;
						Buffer.BlockCopy(bytes43, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						Buffer.BlockCopy(bytes44, 0, buffer[num].writeBuffer, num3, 4);
						num3 += 4;
						break;
					}
					case 53:
					{
						byte[] bytes38 = BitConverter.GetBytes(msgType);
						byte[] bytes39 = BitConverter.GetBytes((short)number);
						byte b6 = (byte)number2;
						byte[] bytes40 = BitConverter.GetBytes((short)number3);
						num2 += bytes39.Length + 1 + bytes40.Length;
						byte[] bytes41 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes41, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes38, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes39, 0, buffer[num].writeBuffer, num3, bytes39.Length);
						num3 += bytes39.Length;
						buffer[num].writeBuffer[num3] = b6;
						num3++;
						Buffer.BlockCopy(bytes40, 0, buffer[num].writeBuffer, num3, bytes40.Length);
						num3 += bytes40.Length;
						break;
					}
					case 54:
					{
						byte[] bytes29 = BitConverter.GetBytes(msgType);
						byte[] bytes30 = BitConverter.GetBytes((short)number);
						num2 += bytes30.Length + 15;
						byte[] bytes31 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes31, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes29, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes30, 0, buffer[num].writeBuffer, num3, bytes30.Length);
						num3 += bytes30.Length;
						for (int i = 0; i < 5; i++)
						{
							buffer[num].writeBuffer[num3] = (byte)Main.npc[(short)number].buffType[i];
							num3++;
							byte[] bytes32 = BitConverter.GetBytes(Main.npc[(short)number].buffTime[i]);
							Buffer.BlockCopy(bytes32, 0, buffer[num].writeBuffer, num3, bytes32.Length);
							num3 += bytes32.Length;
						}
						break;
					}
					case 55:
					{
						byte[] bytes26 = BitConverter.GetBytes(msgType);
						byte b3 = (byte)number;
						byte b4 = (byte)number2;
						byte[] bytes27 = BitConverter.GetBytes((short)number3);
						num2 += 2 + bytes27.Length;
						byte[] bytes28 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes28, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes26, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b3;
						num3++;
						buffer[num].writeBuffer[num3] = b4;
						num3++;
						Buffer.BlockCopy(bytes27, 0, buffer[num].writeBuffer, num3, bytes27.Length);
						num3 += bytes27.Length;
						break;
					}
					case 56:
					{
						byte[] bytes22 = BitConverter.GetBytes(msgType);
						byte[] bytes23 = BitConverter.GetBytes((short)number);
						byte[] bytes24 = Encoding.UTF8.GetBytes(Main.chrName[number]);
						num2 += bytes23.Length + bytes24.Length;
						byte[] bytes25 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes25, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes22, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes23, 0, buffer[num].writeBuffer, num3, bytes23.Length);
						num3 += bytes23.Length;
						Buffer.BlockCopy(bytes24, 0, buffer[num].writeBuffer, num3, bytes24.Length);
						break;
					}
					case 57:
					{
						byte[] bytes20 = BitConverter.GetBytes(msgType);
						num2 += 2;
						byte[] bytes21 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes21, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes20, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = WorldGen.tGood;
						num3++;
						buffer[num].writeBuffer[num3] = WorldGen.tEvil;
						break;
					}
					case 58:
					{
						byte[] bytes17 = BitConverter.GetBytes(msgType);
						byte b2 = (byte)number;
						byte[] bytes18 = BitConverter.GetBytes(number2);
						num2 += 1 + bytes18.Length;
						byte[] bytes19 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes19, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes17, 0, buffer[num].writeBuffer, 4, 1);
						buffer[num].writeBuffer[num3] = b2;
						num3++;
						Buffer.BlockCopy(bytes18, 0, buffer[num].writeBuffer, num3, bytes18.Length);
						break;
					}
					case 59:
					{
						byte[] bytes13 = BitConverter.GetBytes(msgType);
						byte[] bytes14 = BitConverter.GetBytes(number);
						byte[] bytes15 = BitConverter.GetBytes((int)number2);
						num2 += bytes14.Length + bytes15.Length;
						byte[] bytes16 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes16, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes13, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes14, 0, buffer[num].writeBuffer, num3, bytes14.Length);
						num3 += 4;
						Buffer.BlockCopy(bytes15, 0, buffer[num].writeBuffer, num3, bytes15.Length);
						break;
					}
					case 60:
					{
						byte[] bytes8 = BitConverter.GetBytes(msgType);
						byte[] bytes9 = BitConverter.GetBytes((short)number);
						byte[] bytes10 = BitConverter.GetBytes((short)number2);
						byte[] bytes11 = BitConverter.GetBytes((short)number3);
						byte b = (byte)number4;
						num2 += bytes9.Length + bytes10.Length + bytes11.Length + 1;
						byte[] bytes12 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes12, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes8, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes9, 0, buffer[num].writeBuffer, num3, bytes9.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes10, 0, buffer[num].writeBuffer, num3, bytes10.Length);
						num3 += 2;
						Buffer.BlockCopy(bytes11, 0, buffer[num].writeBuffer, num3, bytes11.Length);
						num3 += 2;
						buffer[num].writeBuffer[num3] = b;
						num3++;
						break;
					}
					case 61:
					{
						byte[] bytes4 = BitConverter.GetBytes(msgType);
						byte[] bytes5 = BitConverter.GetBytes(number);
						byte[] bytes6 = BitConverter.GetBytes((int)number2);
						num2 += bytes5.Length + bytes6.Length;
						byte[] bytes7 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes7, 0, buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes4, 0, buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes5, 0, buffer[num].writeBuffer, num3, bytes5.Length);
						num3 += bytes5.Length;
						Buffer.BlockCopy(bytes6, 0, buffer[num].writeBuffer, num3, bytes6.Length);
						break;
					}
					}
				}
				if (Events.world.NetSendIntercept != null)
				{
					Events.world.NetSendIntercept(num, ref num2, ref num3, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5);
					byte[] bytes267 = BitConverter.GetBytes(num2 - 4);
					Buffer.BlockCopy(bytes267, 0, buffer[num].writeBuffer, 0, 4);
				}
				if (Main.netMode == 1)
				{
					if (Netplay.clientSock.tcpClient.Connected)
					{
						try
						{
							buffer[num].spamCount++;
							Main.txMsg++;
							Main.txData += num2;
							Main.txMsgType[msgType]++;
							Main.txDataType[msgType] += num2;
							Netplay.clientSock.networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.clientSock.ClientWriteCallBack, Netplay.clientSock.networkStream);
						}
						catch
						{
						}
					}
				}
				else if (remoteClient == -1)
				{
					if (msgType == 20)
					{
						for (int num13 = 0; num13 < 256; num13++)
						{
							if (num13 != ignoreClient && (buffer[num13].broadcast || (Netplay.serverSock[num13].state >= 3 && msgType == 10)) && Netplay.serverSock[num13].tcpClient.Connected && Netplay.serverSock[num13].SectionRange(number, (int)number2, (int)number3))
							{
								try
								{
									buffer[num13].spamCount++;
									Main.txMsg++;
									Main.txData += num2;
									Main.txMsgType[msgType]++;
									Main.txDataType[msgType] += num2;
									Netplay.serverSock[num13].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[num13].ServerWriteCallBack, Netplay.serverSock[num13].networkStream);
								}
								catch
								{
								}
							}
						}
					}
					else if (msgType == 28)
					{
						for (int num14 = 0; num14 < 256; num14++)
						{
							if (num14 != ignoreClient && (buffer[num14].broadcast || (Netplay.serverSock[num14].state >= 3 && msgType == 10)) && Netplay.serverSock[num14].tcpClient.Connected)
							{
								bool flag = false;
								if (Main.npc[number].life <= 0)
								{
									flag = true;
								}
								else
								{
									Rectangle rectangle = new Rectangle((int)Main.player[num14].position.X, (int)Main.player[num14].position.Y, Main.player[num14].width, Main.player[num14].height);
									Rectangle value3 = new Rectangle((int)Main.npc[number].position.X, (int)Main.npc[number].position.Y, Main.npc[number].width, Main.npc[number].height);
									value3.X -= 3000;
									value3.Y -= 3000;
									value3.Width += 6000;
									value3.Height += 6000;
									if (rectangle.Intersects(value3))
									{
										flag = true;
									}
								}
								if (flag)
								{
									try
									{
										buffer[num14].spamCount++;
										Main.txMsg++;
										Main.txData += num2;
										Main.txMsgType[msgType]++;
										Main.txDataType[msgType] += num2;
										Netplay.serverSock[num14].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[num14].ServerWriteCallBack, Netplay.serverSock[num14].networkStream);
									}
									catch
									{
									}
								}
							}
						}
					}
					else if (msgType == 13)
					{
						for (int num15 = 0; num15 < 256; num15++)
						{
							if (num15 != ignoreClient && (buffer[num15].broadcast || (Netplay.serverSock[num15].state >= 3 && msgType == 10)) && Netplay.serverSock[num15].tcpClient.Connected)
							{
								bool flag2 = false;
								if (Main.player[number].netSkip > 0)
								{
									Rectangle rectangle2 = new Rectangle((int)Main.player[num15].position.X, (int)Main.player[num15].position.Y, Main.player[num15].width, Main.player[num15].height);
									Rectangle value4 = new Rectangle((int)Main.player[number].position.X, (int)Main.player[number].position.Y, Main.player[number].width, Main.player[number].height);
									value4.X -= 2500;
									value4.Y -= 2500;
									value4.Width += 5000;
									value4.Height += 5000;
									if (rectangle2.Intersects(value4))
									{
										flag2 = true;
									}
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									try
									{
										buffer[num15].spamCount++;
										Main.txMsg++;
										Main.txData += num2;
										Main.txMsgType[msgType]++;
										Main.txDataType[msgType] += num2;
										Netplay.serverSock[num15].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[num15].ServerWriteCallBack, Netplay.serverSock[num15].networkStream);
									}
									catch
									{
									}
								}
							}
						}
						Main.player[number].netSkip++;
						if (Main.player[number].netSkip > 2)
						{
							Main.player[number].netSkip = 0;
						}
					}
					else if (msgType == 27)
					{
						for (int num16 = 0; num16 < 256; num16++)
						{
							if (num16 != ignoreClient && (buffer[num16].broadcast || (Netplay.serverSock[num16].state >= 3 && msgType == 10)) && Netplay.serverSock[num16].tcpClient.Connected)
							{
								bool flag3 = false;
								if (Main.projectile[number].type == 12)
								{
									flag3 = true;
								}
								else
								{
									Rectangle rectangle3 = new Rectangle((int)Main.player[num16].position.X, (int)Main.player[num16].position.Y, Main.player[num16].width, Main.player[num16].height);
									Rectangle value5 = new Rectangle((int)Main.projectile[number].position.X, (int)Main.projectile[number].position.Y, Main.projectile[number].width, Main.projectile[number].height);
									value5.X -= 5000;
									value5.Y -= 5000;
									value5.Width += 10000;
									value5.Height += 10000;
									if (rectangle3.Intersects(value5))
									{
										flag3 = true;
									}
								}
								if (flag3)
								{
									try
									{
										buffer[num16].spamCount++;
										Main.txMsg++;
										Main.txData += num2;
										Main.txMsgType[msgType]++;
										Main.txDataType[msgType] += num2;
										Netplay.serverSock[num16].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[num16].ServerWriteCallBack, Netplay.serverSock[num16].networkStream);
									}
									catch
									{
									}
								}
							}
						}
					}
					else
					{
						for (int num17 = 0; num17 < 256; num17++)
						{
							if (num17 != ignoreClient && (buffer[num17].broadcast || (Netplay.serverSock[num17].state >= 3 && msgType == 10)) && Netplay.serverSock[num17].tcpClient.Connected)
							{
								try
								{
									buffer[num17].spamCount++;
									Main.txMsg++;
									Main.txData += num2;
									Main.txMsgType[msgType]++;
									Main.txDataType[msgType] += num2;
									Netplay.serverSock[num17].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[num17].ServerWriteCallBack, Netplay.serverSock[num17].networkStream);
								}
								catch
								{
								}
							}
						}
					}
				}
				else if (Netplay.serverSock[remoteClient].tcpClient.Connected)
				{
					try
					{
						buffer[remoteClient].spamCount++;
						Main.txMsg++;
						Main.txData += num2;
						Main.txMsgType[msgType]++;
						Main.txDataType[msgType] += num2;
						Netplay.serverSock[remoteClient].networkStream.BeginWrite(buffer[num].writeBuffer, 0, num2, Netplay.serverSock[remoteClient].ServerWriteCallBack, Netplay.serverSock[remoteClient].networkStream);
					}
					catch
					{
					}
				}
				if (Main.verboseNetplay)
				{
					for (int num18 = 0; num18 < num2; num18++)
					{
					}
					for (int num19 = 0; num19 < num2; num19++)
					{
						_ = buffer[num].writeBuffer[num19];
					}
				}
				buffer[num].writeLocked = false;
				if (msgType == 19 && Main.netMode == 1)
				{
					int size = 5;
					SendTileSquare(num, (int)number2, (int)number3, size);
				}
				if (msgType == 2 && Main.netMode == 2)
				{
					Netplay.serverSock[num].kill = true;
				}
			}
		}

		public static void RecieveBytes(byte[] bytes, int streamLength, int i = 256)
		{
			lock (buffer[i])
			{
				try
				{
					Buffer.BlockCopy(bytes, 0, buffer[i].readBuffer, buffer[i].totalData, streamLength);
					buffer[i].totalData += streamLength;
					buffer[i].checkBytes = true;
				}
				catch
				{
					if (Main.netMode == 1)
					{
						Main.menuMode = 15;
						Main.statusText = "Bad header lead to a read buffer overflow.";
						Netplay.disconnect = true;
					}
					else
					{
						Netplay.serverSock[i].kill = true;
					}
				}
			}
		}

		public static void CheckBytes(int i = 256)
		{
			lock (buffer[i])
			{
				int num = 0;
				if (buffer[i].totalData >= 4)
				{
					if (buffer[i].messageLength == 0)
					{
						buffer[i].messageLength = BitConverter.ToInt32(buffer[i].readBuffer, 0) + 4;
					}
					while (buffer[i].totalData >= buffer[i].messageLength + num && buffer[i].messageLength > 0)
					{
						if (!Main.ignoreErrors)
						{
							buffer[i].GetData(num + 4, buffer[i].messageLength - 4);
						}
						else
						{
							try
							{
								buffer[i].GetData(num + 4, buffer[i].messageLength - 4);
							}
							catch
							{
							}
						}
						num += buffer[i].messageLength;
						if (buffer[i].totalData - num >= 4)
						{
							buffer[i].messageLength = BitConverter.ToInt32(buffer[i].readBuffer, num) + 4;
						}
						else
						{
							buffer[i].messageLength = 0;
						}
					}
					if (num == buffer[i].totalData)
					{
						buffer[i].totalData = 0;
					}
					else if (num > 0)
					{
						Buffer.BlockCopy(buffer[i].readBuffer, num, buffer[i].readBuffer, 0, buffer[i].totalData - num);
						buffer[i].totalData -= num;
					}
					buffer[i].checkBytes = false;
				}
			}
		}

		public static void BootPlayer(int plr, string msg)
		{
			SendData(2, plr, -1, msg);
		}

		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int size)
		{
			int num = (size - 1) / 2;
			SendData(20, whoAmi, -1, "", size, tileX - num, tileY - num);
		}

		public static void SendSection(int whoAmi, int sectionX, int sectionY)
		{
			if (Main.netMode == 2)
			{
				try
				{
					if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
					{
						Netplay.serverSock[whoAmi].tileSection[sectionX, sectionY] = true;
						int num = sectionX * 200;
						int num2 = sectionY * 150;
						for (int i = num2; i < num2 + 150; i++)
						{
							SendData(10, whoAmi, -1, "", 200, num, i);
						}
					}
				}
				catch
				{
				}
			}
		}

		public static void greetPlayer(int plr)
		{
			if (Main.motd == "")
			{
				SendData(25, plr, -1, Lang.mp[18] + " " + Main.worldName + "!", 255, 255f, 240f, 20f);
			}
			else
			{
				SendData(25, plr, -1, Main.motd, 255, 255f, 240f, 20f);
			}
			string text = "";
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					text = ((!(text == "")) ? (text + ", " + Main.player[i].name) : (text + Main.player[i].name));
				}
			}
			SendData(25, plr, -1, "Current players: " + text + ".", 255, 255f, 240f, 20f);
		}

		public static void sendWater(int x, int y)
		{
			if (Main.netMode == 1)
			{
				SendData(48, -1, -1, "", x, y);
				return;
			}
			for (int i = 0; i < 256; i++)
			{
				if ((buffer[i].broadcast || Netplay.serverSock[i].state >= 3) && Netplay.serverSock[i].tcpClient.Connected)
				{
					int num = x / 200;
					int num2 = y / 150;
					if (Netplay.serverSock[i].tileSection[num, num2])
					{
						SendData(48, i, -1, "", x, y);
					}
				}
			}
		}

		public static void syncPlayers()
		{
			Codable.RunGlobalMethod("ModWorld", "SyncPlayers");
			bool flag = false;
			for (int i = 0; i < 255; i++)
			{
				int num = 0;
				if (Main.player[i].active)
				{
					num = 1;
				}
				if (Netplay.serverSock[i].state == 10)
				{
					if (Main.autoShutdown && !flag)
					{
						string text = Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint.ToString();
						string a = text;
						for (int j = 0; j < text.Length; j++)
						{
							if (text.Substring(j, 1) == ":")
							{
								a = text.Substring(0, j);
							}
						}
						if (a == "127.0.0.1")
						{
							flag = true;
						}
					}
					SendData(14, -1, i, "", i, num);
					SendData(4, -1, i, Main.player[i].name, i);
					SendData(13, -1, i, "", i);
					SendData(16, -1, i, "", i);
					SendData(30, -1, i, "", i);
					SendData(45, -1, i, "", i);
					SendData(42, -1, i, "", i);
					SendData(50, -1, i, "", i);
					for (int k = 0; k < 49; k++)
					{
						SendData(5, -1, i, Main.player[i].inventory[k].name, i, k, (int)Main.player[i].inventory[k].prefix);
					}
					SendData(5, -1, i, Main.player[i].armor[0].name, i, 49f, (int)Main.player[i].armor[0].prefix);
					SendData(5, -1, i, Main.player[i].armor[1].name, i, 50f, (int)Main.player[i].armor[1].prefix);
					SendData(5, -1, i, Main.player[i].armor[2].name, i, 51f, (int)Main.player[i].armor[2].prefix);
					SendData(5, -1, i, Main.player[i].armor[3].name, i, 52f, (int)Main.player[i].armor[3].prefix);
					SendData(5, -1, i, Main.player[i].armor[4].name, i, 53f, (int)Main.player[i].armor[4].prefix);
					SendData(5, -1, i, Main.player[i].armor[5].name, i, 54f, (int)Main.player[i].armor[5].prefix);
					SendData(5, -1, i, Main.player[i].armor[6].name, i, 55f, (int)Main.player[i].armor[6].prefix);
					SendData(5, -1, i, Main.player[i].armor[7].name, i, 56f, (int)Main.player[i].armor[7].prefix);
					SendData(5, -1, i, Main.player[i].armor[8].name, i, 57f, (int)Main.player[i].armor[8].prefix);
					SendData(5, -1, i, Main.player[i].armor[9].name, i, 58f, (int)Main.player[i].armor[9].prefix);
					SendData(5, -1, i, Main.player[i].armor[10].name, i, 59f, (int)Main.player[i].armor[10].prefix);
					if (!Netplay.serverSock[i].announced)
					{
						Netplay.serverSock[i].announced = true;
						SendData(25, -1, i, Main.player[i].name + " " + Lang.mp[19], 255, 255f, 240f, 20f);
						if (Main.dedServ)
						{
							Console.WriteLine(Main.player[i].name + " " + Lang.mp[19]);
						}
					}
					continue;
				}
				num = 0;
				SendData(14, -1, i, "", i, num);
				if (Netplay.serverSock[i].announced)
				{
					Netplay.serverSock[i].announced = false;
					SendData(25, -1, i, Netplay.serverSock[i].oldName + " " + Lang.mp[20], 255, 255f, 240f, 20f);
					if (Main.dedServ)
					{
						Console.WriteLine(Netplay.serverSock[i].oldName + " " + Lang.mp[20]);
					}
				}
			}
			for (int l = 0; l < 200; l++)
			{
				if (Main.npc[l].active && Main.npc[l].townNPC && NPC.TypeToNum(Main.npc[l].type) != -1)
				{
					int num2 = 0;
					if (Main.npc[l].homeless)
					{
						num2 = 1;
					}
					SendData(60, -1, -1, "", l, Main.npc[l].homeTileX, Main.npc[l].homeTileY, num2);
				}
			}
			if (Main.autoShutdown && !flag)
			{
				WorldGen.saveWorld();
				Netplay.disconnect = true;
			}
		}
	}
}
