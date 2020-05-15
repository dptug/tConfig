using Gajatko.IniFiles;
using System;
using System.Collections;
using System.IO;

namespace Terraria
{
	public class Builder
	{
		public delegate bool Checker(string itemname);

		public BinaryWriter w;

		public BinaryReader r;

		public string type;

		public IniFile ini;

		public ArrayList attrs;

		public string version;

		public string modpack;

		public string modpackFolder;

		public Checker itemExists;

		public Checker tileExists;

		public Checker wallExists;

		public Checker holdStyleExists;

		public Checker useStyleExists;

		public Builder()
		{
			attrs = new ArrayList();
		}

		public bool ReadINI(BinaryWriter w, IniFile ini, string modpackFolder)
		{
			this.w = w;
			this.ini = ini;
			this.modpackFolder = modpackFolder;
			BuildAttrs();
			foreach (AttrInterface attr in attrs)
			{
				if (!attr.ReadINI())
				{
					return false;
				}
			}
			return true;
		}

		public virtual void BuildAttrs()
		{
		}

		public virtual void ReadObj(string modpack, BinaryReader reader, string version)
		{
			r = reader;
			this.version = version;
			this.modpack = modpack;
			BuildAttrs();
			foreach (Attr attr in attrs)
			{
				if (attr.version != "-1")
				{
					if (Config.CheckVersion(version, attr.version) && (!attr.boolCheck || reader.ReadBoolean()))
					{
						attr.ReadObj();
					}
				}
				else
				{
					attr.ReadObj();
				}
			}
		}

		public bool strReader(string name)
		{
			if (!string.IsNullOrEmpty(ini["Stats"][name]))
			{
				w.Write(value: true);
				string value = ini["Stats"][name];
				w.Write(value);
			}
			else
			{
				w.Write(value: false);
			}
			return true;
		}

		public bool boolReader(string name)
		{
			if (ini["Stats"][name] == "True")
			{
				w.Write(value: true);
			}
			else
			{
				w.Write(value: false);
			}
			return true;
		}

		public bool soundReader(string name)
		{
			if (!string.IsNullOrEmpty(ini["Stats"][name]))
			{
				string text = Path.Combine(modpackFolder, "Sound", ini["Stats"][name] + ".wav");
				if (!File.Exists(text))
				{
					Console.WriteLine("Sound file " + text + " does not exist!");
					return false;
				}
				w.Write(value: true);
				w.Write(ini["Stats"][name]);
			}
			else
			{
				w.Write(value: false);
			}
			return true;
		}

		public Attr.IniReader strReaderCheck(Checker checker)
		{
			return delegate(string name)
			{
				if (!string.IsNullOrEmpty(ini["Stats"][name]))
				{
					w.Write(value: true);
					string text = ini["Stats"][name];
					if (!checker(text))
					{
						return false;
					}
					w.Write(text);
				}
				else
				{
					w.Write(value: false);
				}
				return true;
			};
		}

		public bool intReader(string name)
		{
			if (!string.IsNullOrEmpty(ini["Stats"][name]))
			{
				w.Write(value: true);
				int value = Convert.ToInt32(ini["Stats"][name]);
				w.Write(value);
			}
			else
			{
				w.Write(value: false);
			}
			return true;
		}
	}
}
