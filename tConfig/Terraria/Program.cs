using System;
using System.IO;
using System.Windows.Forms;

namespace Terraria
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			using (Main main = new Main())
			{
				try
				{
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i].ToLower() == "-port" || args[i].ToLower() == "-p")
						{
							i++;
							try
							{
								int num = Netplay.serverPort = Convert.ToInt32(args[i]);
							}
							catch
							{
							}
						}
						if (args[i].ToLower() == "-join" || args[i].ToLower() == "-j")
						{
							i++;
							try
							{
								main.AutoJoin(args[i]);
							}
							catch
							{
							}
						}
						if (args[i].ToLower() == "-pass" || args[i].ToLower() == "-password")
						{
							i++;
							Netplay.password = args[i];
							main.AutoPass();
						}
						if (args[i].ToLower() == "-host")
						{
							main.AutoHost();
						}
						if (args[i].ToLower() == "-loadlib")
						{
							i++;
							string path = args[i];
							main.loadLib(path);
						}
					}
					if (Constants.InTheWork)
					{
						main.Run();
					}
					else
					{
						main.Run();
					}
				}
				catch (Exception ex)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", append: true))
						{
							streamWriter.WriteLine(DateTime.Now);
							streamWriter.WriteLine(ex);
							streamWriter.WriteLine("");
						}
						MessageBox.Show(ex.ToString(), "Terraria: Error");
					}
					catch
					{
					}
				}
			}
		}
	}
}
