namespace Terraria
{
	public class UnloadedPrefix : Prefix
	{
		public UnloadedPrefix(string name, bool suffix = false, IPrefix code = null)
			: base(name, suffix, code)
		{
			affix = name;
		}

		public override void Apply(Item item)
		{
		}

		public override void Apply(Player player)
		{
		}

		public override bool Check(Item item)
		{
			return false;
		}
	}
}
