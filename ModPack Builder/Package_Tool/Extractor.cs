using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.Win32;

namespace Package_Tool
{
	internal class Extractor
	{
		private static void Main(string[] args)
		{
			string text = "tConfig.exe";
			string text2 = "";
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE").OpenSubKey("Valve").OpenSubKey("Steam");
				text2 = (string)registryKey.GetValue("SteamPath");
			}
			catch (Exception arg)
			{
				Console.WriteLine("Error accessing registry: " + arg);
			}
			string text3 = "";
			if (text2 != "")
			{
				text3 = text2 + "\\steamapps\\common\\terraria\\";
			}
			if (!File.Exists(text3 + text))
			{
				Console.WriteLine("Warning: " + text3 + text + " could not be found.\nYou will need to install tConfig in order to use this mod.");
				Console.WriteLine("Press Enter to continue.");
				Console.ReadLine();
			}
			string text4 = string.Concat(new object[]
			{
				AppDomain.CurrentDomain.BaseDirectory,
				Path.DirectorySeparatorChar,
				"Storage",
				Path.DirectorySeparatorChar,
				"ModPacks"
			});
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("ModFiles");
			IResourceReader resourceReader = new ResourceReader(manifestResourceStream);
			foreach (object obj in resourceReader)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text5 = (string)dictionaryEntry.Key;
				byte[] bytes = (byte[])dictionaryEntry.Value;
				Console.WriteLine(string.Concat(new object[]
				{
					"Writing out ",
					text4,
					Path.DirectorySeparatorChar,
					text5
				}));
				File.WriteAllBytes(text4 + Path.DirectorySeparatorChar + text5, bytes);
			}
			Console.WriteLine("Done installing!\n\nDon't forget to Enable the mods in the tConfig Settings menu!\n\nPress Enter to exit!");
			Console.ReadLine();
		}
	}
}
