using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Terraria
{
	internal class Installer_Builder
	{
		private static void Main(string[] args)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.obj");
			if (files.Length == 0)
			{
				Console.WriteLine("There are no .obj mod files in the current directory!");
				Console.WriteLine("Press Enter to exit.");
				Console.ReadLine();
			}
			else
			{
				Build("ModPack", files);
				Console.WriteLine("Press Enter to exit.");
				Console.ReadLine();
			}
		}

		public static void Build(string modname, string[] files, string destination = "")
		{
			string extractor = Code.Extractor;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("CompilerVersion", "v4.0");
			Dictionary<string, string> providerOptions = dictionary;
			CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(providerOptions);
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = false;
			compilerParameters.GenerateExecutable = true;
			compilerParameters.CompilerOptions = "/optimize";
			CompilerParameters compilerParameters2 = compilerParameters;
			compilerParameters2.OutputAssembly = destination + Path.DirectorySeparatorChar + modname + " Installer.exe";
			IResourceWriter resourceWriter = new ResourceWriter("ModFiles");
			for (int i = 0; i < files.Length; i++)
			{
				Console.WriteLine("Adding mod file: " + files[i]);
				resourceWriter.AddResource(Path.GetFileName(files[i]), File.ReadAllBytes(files[i]));
			}
			resourceWriter.Generate();
			resourceWriter.Close();
			compilerParameters2.EmbeddedResources.Add("ModFiles");
			CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters2, extractor);
			if (compilerResults.Errors.Count > 0)
			{
				string str = "";
				str = str + "Errors building source code into " + compilerResults.PathToAssembly;
				str = str + "\n" + extractor + "\n\n";
				foreach (CompilerError error in compilerResults.Errors)
				{
					str = str + "  " + error.ToString();
				}
				Console.WriteLine(str);
				Console.WriteLine("Press Enter to exit.");
				Console.ReadLine();
			}
			else
			{
				File.Delete("ModFiles");
				Console.WriteLine(modname + " Installer.exe generated successfully!");
			}
		}
	}
}
