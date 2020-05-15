using System;
using System.IO;

namespace Terraria
{
	public class Constants
	{
		public static bool betaRelease = false;

		public static string version = "0.35.0";

		public static string minRequiredVersion = "0.35.0";

		public static string subVersion = "";

		public static bool InTheWork = false;

		public static string tConfigFolder = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Storage";
	}
}
