using Gajatko.IniFiles;
using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace Terraria
{
	public class DropTable
	{
		public ArrayList drops = new ArrayList();

		public DropTable()
		{
		}

		public DropTable(BinaryReader reader, string version)
		{
			Load(reader, version);
		}

		public DropTable(IniFileSection section)
		{
			foreach (string key in section.GetKeys())
			{
				string name;
				int num;
				int max;
				if (key[0] != '*')
				{
					name = key.Substring(key.IndexOf(' ') + 1);
					if (key.IndexOf('-') > 0)
					{
						string[] array = key.Split()[0].Split('-');
						num = Convert.ToInt32(array[0]);
						max = Convert.ToInt32(array[1]);
					}
					else
					{
						string value = key.Substring(0, key.IndexOf(' '));
						num = Convert.ToInt32(value);
						max = num;
					}
				}
				else
				{
					name = key;
					num = 0;
					max = 0;
				}
				float chance = Convert.ToSingle(section[key], CultureInfo.InvariantCulture.NumberFormat);
				Drop value2 = new Drop(num, max, name, chance);
				drops.Add(value2);
			}
			drops.Sort();
		}

		public Item GetNextDrop(int luck = 0)
		{
			int num = 0;
			for (num = 0; num < drops.Count; num++)
			{
				Drop drop = (Drop)drops[num];
				if (drop.chance == 100f)
				{
					break;
				}
				Item item = drop.TryDrop();
				if (item != null)
				{
					return item;
				}
			}
			if (num < drops.Count)
			{
				int index = Config.rand.Next(num, drops.Count);
				Item item2 = ((Drop)drops[index]).TryDrop();
				if (item2 != null)
				{
					return item2;
				}
			}
			return null;
		}

		public void Save(BinaryWriter writer)
		{
			writer.Write(drops.Count);
			foreach (Drop drop in drops)
			{
				drop.Save(writer);
			}
		}

		public void Load(BinaryReader reader, string version)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				drops.Add(new Drop(reader, version));
			}
		}
	}
}
