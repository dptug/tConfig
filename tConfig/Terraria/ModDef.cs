namespace Terraria
{
	public class ModDef
	{
		public string name;

		public string setting;

		public int index;

		public ModDef(string name, string setting, int index)
		{
			this.name = name;
			this.setting = setting;
			this.index = index;
		}
	}
}
