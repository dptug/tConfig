namespace Terraria
{
	public class ProjDefIDAccessor
	{
		public int this[string str]
		{
			get
			{
				return Config.projDefs.byName[str].type;
			}
			set
			{
			}
		}

		public bool ContainsKey(string str)
		{
			return Config.projDefs.byName.ContainsKey(str);
		}
	}
}
