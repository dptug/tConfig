using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Terraria
{
	public class Netplay
	{
		public const int bufferSize = 1024;

		public const int maxConnections = 256;

		public static bool stopListen = false;

		public static ServerSock[] serverSock = new ServerSock[256];

		public static ClientSock clientSock = new ClientSock();

		public static TcpListener tcpListener;

		public static IPAddress serverListenIP;

		public static IPAddress serverIP;

		public static int serverPort = 7777;

		public static bool disconnect = false;

		public static string password = "";

		public static string banFile = "banlist.txt";

		public static bool spamCheck = false;

		public static bool anyClients = false;

		public static bool ServerUp = false;

		public static void ResetNetDiag()
		{
			Main.rxMsg = 0;
			Main.rxData = 0;
			Main.txMsg = 0;
			Main.txData = 0;
			for (int i = 0; i < Main.maxMsg; i++)
			{
				Main.rxMsgType[i] = 0;
				Main.rxDataType[i] = 0;
				Main.txMsgType[i] = 0;
				Main.txDataType[i] = 0;
			}
		}

		public static void ResetSections()
		{
			for (int i = 0; i < 256; i++)
			{
				for (int j = 0; j < Main.maxSectionsX; j++)
				{
					for (int k = 0; k < Main.maxSectionsY; k++)
					{
						serverSock[i].tileSection[j, k] = false;
					}
				}
			}
		}

		public static void AddBan(int plr)
		{
			string text = serverSock[plr].tcpClient.Client.RemoteEndPoint.ToString();
			string value = text;
			for (int i = 0; i < text.Length; i++)
			{
				if (text.Substring(i, 1) == ":")
				{
					value = text.Substring(0, i);
				}
			}
			using (StreamWriter streamWriter = new StreamWriter(banFile, append: true))
			{
				streamWriter.WriteLine("//" + Main.player[plr].name);
				streamWriter.WriteLine(value);
			}
		}

		public static bool CheckBan(string ip)
		{
			try
			{
				string b = ip;
				for (int i = 0; i < ip.Length; i++)
				{
					if (ip.Substring(i, 1) == ":")
					{
						b = ip.Substring(0, i);
					}
				}
				if (File.Exists(banFile))
				{
					using (StreamReader streamReader = new StreamReader(banFile))
					{
						string a;
						while ((a = streamReader.ReadLine()) != null)
						{
							if (a == b)
							{
								return true;
							}
						}
					}
				}
			}
			catch
			{
			}
			return false;
		}

		public static void newRecent()
		{
			for (int i = 0; i < Main.maxMP; i++)
			{
				if (Main.recentIP[i] == serverIP.ToString() && Main.recentPort[i] == serverPort)
				{
					for (int j = i; j < Main.maxMP - 1; j++)
					{
						Main.recentIP[j] = Main.recentIP[j + 1];
						Main.recentPort[j] = Main.recentPort[j + 1];
						Main.recentWorld[j] = Main.recentWorld[j + 1];
					}
				}
			}
			for (int num = Main.maxMP - 1; num > 0; num--)
			{
				Main.recentIP[num] = Main.recentIP[num - 1];
				Main.recentPort[num] = Main.recentPort[num - 1];
				Main.recentWorld[num] = Main.recentWorld[num - 1];
			}
			Main.recentIP[0] = serverIP.ToString();
			Main.recentPort[0] = serverPort;
			Main.recentWorld[0] = Main.worldName;
			Main.SaveRecent();
		}

		public static void ClientLoop(object threadContext)
		{
			ResetNetDiag();
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			Main.player[Main.myPlayer].hostile = false;
			Main.clientPlayer = (Player)Main.player[Main.myPlayer].clientClone();
			for (int i = 0; i < 255; i++)
			{
				if (i != Main.myPlayer)
				{
					Main.player[i] = new Player();
				}
			}
			Main.menuMode = 10;
			Main.menuMode = 14;
			if (!Main.autoPass)
			{
				Main.statusText = "Connecting to " + serverIP;
			}
			Main.netMode = 1;
			disconnect = false;
			clientSock = new ClientSock();
			clientSock.tcpClient.NoDelay = true;
			clientSock.readBuffer = new byte[1024];
			clientSock.writeBuffer = new byte[1024];
			bool flag = true;
			while (flag)
			{
				flag = false;
				try
				{
					clientSock.tcpClient.Connect(serverIP, serverPort);
					clientSock.networkStream = clientSock.tcpClient.GetStream();
					flag = false;
				}
				catch
				{
					if (!disconnect && Main.gameMenu)
					{
						flag = true;
					}
				}
			}
			NetMessage.buffer[256].Reset();
			int num = -1;
			while (!disconnect)
			{
				if (clientSock.tcpClient.Connected)
				{
					if (NetMessage.buffer[256].checkBytes)
					{
						NetMessage.CheckBytes();
					}
					clientSock.active = true;
					if (clientSock.state == 0)
					{
						Main.statusText = "Found server";
						clientSock.state = 1;
						NetMessage.SendData(1);
					}
					if (clientSock.state == 2 && num != clientSock.state)
					{
						Main.statusText = "Sending player data...";
					}
					if (clientSock.state == 3 && num != clientSock.state)
					{
						Main.statusText = "Requesting world information";
					}
					if (clientSock.state == 4)
					{
						WorldGen.worldCleared = false;
						clientSock.state = 5;
						WorldGen.clearWorld();
					}
					if (clientSock.state == 5 && WorldGen.worldCleared)
					{
						clientSock.state = 6;
						Main.player[Main.myPlayer].FindSpawn();
						NetMessage.SendData(8, -1, -1, "", Main.player[Main.myPlayer].SpawnX, Main.player[Main.myPlayer].SpawnY);
					}
					if (clientSock.state == 6 && num != clientSock.state)
					{
						Main.statusText = "Requesting tile data";
					}
					if (!clientSock.locked && !disconnect && clientSock.networkStream.DataAvailable)
					{
						clientSock.locked = true;
						clientSock.networkStream.BeginRead(clientSock.readBuffer, 0, clientSock.readBuffer.Length, clientSock.ClientReadCallBack, clientSock.networkStream);
					}
					if (clientSock.statusMax > 0 && clientSock.statusText != "")
					{
						if (clientSock.statusCount >= clientSock.statusMax)
						{
							Main.statusText = clientSock.statusText + ": Complete!";
							clientSock.statusText = "";
							clientSock.statusMax = 0;
							clientSock.statusCount = 0;
						}
						else
						{
							Main.statusText = clientSock.statusText + ": " + (int)((float)clientSock.statusCount / (float)clientSock.statusMax * 100f) + "%";
						}
					}
					Thread.Sleep(1);
				}
				else if (clientSock.active)
				{
					Main.statusText = "Lost connection";
					disconnect = true;
				}
				num = clientSock.state;
			}
			try
			{
				clientSock.networkStream.Close();
				clientSock.networkStream = clientSock.tcpClient.GetStream();
			}
			catch
			{
			}
			if (!Main.gameMenu)
			{
				Main.netMode = 0;
				Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
				Main.gameMenu = true;
				Main.menuMode = 14;
			}
			NetMessage.buffer[256].Reset();
			if (Main.menuMode == 15 && Main.statusText == "Lost connection")
			{
				Main.menuMode = 14;
			}
			if (clientSock.statusText != "" && clientSock.statusText != null)
			{
				Main.statusText = "Lost connection";
			}
			clientSock.statusCount = 0;
			clientSock.statusMax = 0;
			clientSock.statusText = "";
			Main.netMode = 0;
		}

		public static void ServerLoop(object threadContext)
		{
			ResetNetDiag();
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			Main.myPlayer = 255;
			serverIP = IPAddress.Any;
			serverListenIP = serverIP;
			Main.menuMode = 14;
			Main.statusText = "Starting server...";
			Main.netMode = 2;
			disconnect = false;
			for (int i = 0; i < 256; i++)
			{
				serverSock[i] = new ServerSock();
				serverSock[i].Reset();
				serverSock[i].whoAmI = i;
				serverSock[i].tcpClient = new TcpClient();
				serverSock[i].tcpClient.NoDelay = true;
				serverSock[i].readBuffer = new byte[1024];
				serverSock[i].writeBuffer = new byte[1024];
			}
			tcpListener = new TcpListener(serverListenIP, serverPort);
			try
			{
				tcpListener.Start();
			}
			catch (Exception ex)
			{
				Main.menuMode = 15;
				Main.statusText = ex.ToString();
				disconnect = true;
			}
			if (!disconnect)
			{
				ThreadPool.QueueUserWorkItem(ListenForClients, 1);
				Main.statusText = "Server started";
			}
			int num = 0;
			while (!disconnect)
			{
				if (stopListen)
				{
					int num2 = -1;
					for (int j = 0; j < Main.maxNetPlayers; j++)
					{
						if (!serverSock[j].tcpClient.Connected)
						{
							num2 = j;
							break;
						}
					}
					if (num2 >= 0)
					{
						if (Main.ignoreErrors)
						{
							try
							{
								tcpListener.Start();
								stopListen = false;
								ThreadPool.QueueUserWorkItem(ListenForClients, 1);
							}
							catch
							{
							}
						}
						else
						{
							tcpListener.Start();
							stopListen = false;
							ThreadPool.QueueUserWorkItem(ListenForClients, 1);
						}
					}
				}
				int num3 = 0;
				for (int k = 0; k < 256; k++)
				{
					if (NetMessage.buffer[k].checkBytes)
					{
						NetMessage.CheckBytes(k);
					}
					if (serverSock[k].kill)
					{
						serverSock[k].Reset();
						NetMessage.syncPlayers();
					}
					else if (serverSock[k].tcpClient.Connected)
					{
						if (!serverSock[k].active)
						{
							serverSock[k].state = 0;
						}
						serverSock[k].active = true;
						num3++;
						if (!serverSock[k].locked)
						{
							try
							{
								serverSock[k].networkStream = serverSock[k].tcpClient.GetStream();
								if (serverSock[k].networkStream.DataAvailable)
								{
									serverSock[k].locked = true;
									serverSock[k].networkStream.BeginRead(serverSock[k].readBuffer, 0, serverSock[k].readBuffer.Length, serverSock[k].ServerReadCallBack, serverSock[k].networkStream);
								}
							}
							catch
							{
								serverSock[k].kill = true;
							}
						}
						if (serverSock[k].statusMax > 0 && serverSock[k].statusText2 != "")
						{
							if (serverSock[k].statusCount >= serverSock[k].statusMax)
							{
								serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " ", serverSock[k].statusText2, ": Complete!");
								serverSock[k].statusText2 = "";
								serverSock[k].statusMax = 0;
								serverSock[k].statusCount = 0;
							}
							else
							{
								serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " ", serverSock[k].statusText2, ": ", (int)((float)serverSock[k].statusCount / (float)serverSock[k].statusMax * 100f), "%");
							}
						}
						else if (serverSock[k].state == 0)
						{
							serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is connecting...");
						}
						else if (serverSock[k].state == 1)
						{
							serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is sending player data...");
						}
						else if (serverSock[k].state == 2)
						{
							serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " requested world information");
						}
						else if (serverSock[k].state != 3 && serverSock[k].state == 10)
						{
							serverSock[k].statusText = string.Concat("(", serverSock[k].tcpClient.Client.RemoteEndPoint, ") ", serverSock[k].name, " is playing");
						}
					}
					else if (serverSock[k].active)
					{
						serverSock[k].kill = true;
					}
					else
					{
						serverSock[k].statusText2 = "";
						if (k < 255)
						{
							Main.player[k].active = false;
						}
					}
				}
				num++;
				if (num > 10)
				{
					Thread.Sleep(1);
					num = 0;
				}
				else
				{
					Thread.Sleep(0);
				}
				if (!WorldGen.saveLock && !Main.dedServ)
				{
					if (num3 == 0)
					{
						Main.statusText = "Waiting for clients...";
					}
					else
					{
						Main.statusText = num3 + " clients connected";
					}
				}
				if (num3 == 0)
				{
					anyClients = false;
				}
				else
				{
					anyClients = true;
				}
				ServerUp = true;
			}
			tcpListener.Stop();
			for (int l = 0; l < 256; l++)
			{
				serverSock[l].Reset();
			}
			if (Main.menuMode != 15)
			{
				Main.netMode = 0;
				Main.menuMode = 10;
				WorldGen.saveWorld();
				while (WorldGen.saveLock)
				{
				}
				Main.menuMode = 0;
			}
			else
			{
				Main.netMode = 0;
			}
			Main.myPlayer = 0;
		}

		public static void ListenForClients(object threadContext)
		{
			while (!disconnect && !stopListen)
			{
				int num = -1;
				for (int i = 0; i < Main.maxNetPlayers; i++)
				{
					if (!serverSock[i].tcpClient.Connected)
					{
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					try
					{
						serverSock[num].tcpClient = tcpListener.AcceptTcpClient();
						serverSock[num].tcpClient.NoDelay = true;
						Console.WriteLine(string.Concat(serverSock[num].tcpClient.Client.RemoteEndPoint, " is connecting..."));
					}
					catch (Exception ex)
					{
						if (!disconnect)
						{
							Main.menuMode = 15;
							Main.statusText = ex.ToString();
							disconnect = true;
						}
					}
					continue;
				}
				stopListen = true;
				tcpListener.Stop();
			}
		}

		public static void StartClient()
		{
			ThreadPool.QueueUserWorkItem(ClientLoop, 1);
		}

		public static void StartServer()
		{
			ThreadPool.QueueUserWorkItem(ServerLoop, 1);
		}

		public static bool SetIP(string newIP)
		{
			try
			{
				serverIP = IPAddress.Parse(newIP);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool SetIP2(string newIP)
		{
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(newIP);
				IPAddress[] addressList = hostEntry.AddressList;
				for (int i = 0; i < addressList.Length; i++)
				{
					if (addressList[i].AddressFamily == AddressFamily.InterNetwork)
					{
						serverIP = addressList[i];
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static void Init()
		{
			for (int i = 0; i < 257; i++)
			{
				if (i < 256)
				{
					serverSock[i] = new ServerSock();
					serverSock[i].tcpClient.NoDelay = true;
				}
				NetMessage.buffer[i] = new messageBuffer();
				NetMessage.buffer[i].whoAmI = i;
			}
			clientSock.tcpClient.NoDelay = true;
		}

		public static int GetSectionX(int x)
		{
			return x / 200;
		}

		public static int GetSectionY(int y)
		{
			return y / 150;
		}
	}
}
