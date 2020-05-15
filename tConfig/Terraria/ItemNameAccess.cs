namespace Terraria
{
	public class ItemNameAccess
	{
		public string this[int i]
		{
			get
			{
				if (Config.itemDefs[i] == null)
				{
					return null;
				}
				return Config.itemDefs[i].name;
			}
			set
			{
			}
		}

		public bool ContainsValue(string str)
		{
			return Config.itemDefs.byName.ContainsKey(str);
		}
	}
}
