using System;
using System.IO;

namespace Terraria
{
	public class Drop : IComparable
	{
		public int minAmount;

		public int maxAmount;

		public string name;

		public float chance;

		public Drop(BinaryReader reader, string version)
		{
			Load(reader, version);
		}

		public Drop(int min, int max, string name, float chance)
		{
			minAmount = min;
			maxAmount = max;
			this.name = name;
			this.chance = chance;
		}

		public Drop(int amt, string name, float chance)
		{
			minAmount = amt;
			maxAmount = amt;
			this.name = name;
			this.chance = chance;
		}

		public int Compare(object d1, object d2)
		{
			return (int)(((Drop)d1).chance - ((Drop)d2).chance);
		}

		public int CompareTo(object d2)
		{
			return (int)(chance - ((Drop)d2).chance);
		}

		public Item TryDrop()
		{
			if (Config.rand.NextDouble() * 100.0 < (double)chance && name[0] != '*')
			{
				Item item = new Item();
				item.SetDefaults(name);
				item.stack = Config.rand.Next(minAmount, maxAmount + 1);
				return item;
			}
			return null;
		}

		public void Save(BinaryWriter writer)
		{
			writer.Write(minAmount);
			writer.Write(maxAmount);
			writer.Write(name);
			writer.Write(chance);
		}

		public void Load(BinaryReader reader, string version)
		{
			minAmount = reader.ReadInt32();
			maxAmount = reader.ReadInt32();
			name = reader.ReadString();
			if (Config.CheckVersion(version, "0.27"))
			{
				chance = reader.ReadSingle();
			}
			else
			{
				chance = reader.ReadInt32();
			}
		}
	}
}
