using System;
using System.Net.Sockets;

namespace Terraria
{
	public class ServerSock
	{
		public Socket clientSocket;

		public NetworkStream networkStream;

		public TcpClient tcpClient = new TcpClient();

		public int whoAmI;

		public string statusText2;

		public int statusCount;

		public int statusMax;

		public bool[,] tileSection = new bool[Main.maxTilesX / 200 + 1, Main.maxTilesY / 150 + 1];

		public string statusText = "";

		public bool active;

		public bool locked;

		public bool kill;

		public int timeOut;

		public bool announced;

		public string name = "Anonymous";

		public string oldName = "";

		public int state;

		public float spamProjectile;

		public float spamAddBlock;

		public float spamDelBlock;

		public float spamWater;

		public float spamProjectileMax = 100f;

		public float spamAddBlockMax = 100f;

		public float spamDelBlockMax = 500f;

		public float spamWaterMax = 50f;

		public byte[] readBuffer;

		public byte[] writeBuffer;

		public void SpamUpdate()
		{
			if (!Netplay.spamCheck)
			{
				spamProjectile = 0f;
				spamDelBlock = 0f;
				spamAddBlock = 0f;
				spamWater = 0f;
				return;
			}
			if (spamProjectile > spamProjectileMax)
			{
				NetMessage.BootPlayer(whoAmI, "Cheating attempt detected: Projectile spam");
			}
			if (spamAddBlock > spamAddBlockMax)
			{
				NetMessage.BootPlayer(whoAmI, "Cheating attempt detected: Add tile spam");
			}
			if (spamDelBlock > spamDelBlockMax)
			{
				NetMessage.BootPlayer(whoAmI, "Cheating attempt detected: Remove tile spam");
			}
			if (spamWater > spamWaterMax)
			{
				NetMessage.BootPlayer(whoAmI, "Cheating attempt detected: Liquid spam");
			}
			spamProjectile -= 0.4f;
			if (spamProjectile < 0f)
			{
				spamProjectile = 0f;
			}
			spamAddBlock -= 0.3f;
			if (spamAddBlock < 0f)
			{
				spamAddBlock = 0f;
			}
			spamDelBlock -= 5f;
			if (spamDelBlock < 0f)
			{
				spamDelBlock = 0f;
			}
			spamWater -= 0.2f;
			if (spamWater < 0f)
			{
				spamWater = 0f;
			}
		}

		public void SpamClear()
		{
			spamProjectile = 0f;
			spamAddBlock = 0f;
			spamDelBlock = 0f;
			spamWater = 0f;
		}

		public bool SectionRange(int size, int firstX, int firstY)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = firstX;
				int num2 = firstY;
				if (i == 1)
				{
					num += size;
				}
				if (i == 2)
				{
					num2 += size;
				}
				if (i == 3)
				{
					num += size;
					num2 += size;
				}
				int sectionX = Netplay.GetSectionX(num);
				int sectionY = Netplay.GetSectionY(num2);
				if (tileSection[sectionX, sectionY])
				{
					return true;
				}
			}
			return false;
		}

		public void Reset()
		{
			for (int i = 0; i < Main.maxSectionsX; i++)
			{
				for (int j = 0; j < Main.maxSectionsY; j++)
				{
					tileSection[i, j] = false;
				}
			}
			if (whoAmI < 255)
			{
				Main.player[whoAmI] = new Player();
			}
			timeOut = 0;
			statusCount = 0;
			statusMax = 0;
			statusText2 = "";
			statusText = "";
			name = "Anonymous";
			state = 0;
			locked = false;
			kill = false;
			SpamClear();
			active = false;
			NetMessage.buffer[whoAmI].Reset();
			if (networkStream != null)
			{
				networkStream.Close();
			}
			if (tcpClient != null)
			{
				tcpClient.Close();
			}
		}

		public void ServerWriteCallBack(IAsyncResult ar)
		{
			NetMessage.buffer[whoAmI].spamCount--;
			if (statusMax > 0)
			{
				statusCount++;
			}
		}

		public void ServerReadCallBack(IAsyncResult ar)
		{
			int num = 0;
			if (!Netplay.disconnect)
			{
				try
				{
					num = networkStream.EndRead(ar);
				}
				catch
				{
				}
				if (num == 0)
				{
					kill = true;
				}
				else if (Main.ignoreErrors)
				{
					try
					{
						NetMessage.RecieveBytes(readBuffer, num, whoAmI);
					}
					catch
					{
					}
				}
				else
				{
					NetMessage.RecieveBytes(readBuffer, num, whoAmI);
				}
			}
			locked = false;
		}
	}
}
