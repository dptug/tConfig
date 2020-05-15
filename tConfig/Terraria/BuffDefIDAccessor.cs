namespace Terraria
{
	public class BuffDefIDAccessor
	{
		public int this[string str]
		{
			get
			{
				return Config.buffDefs.ID[str];
			}
			set
			{
			}
		}

		public bool ContainsKey(string str)
		{
			return Config.buffDefs.ID.ContainsKey(str);
		}
	}
}
