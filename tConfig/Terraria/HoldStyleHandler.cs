using System.Collections.Generic;

namespace Terraria
{
	public class HoldStyleHandler
	{
		public DictionaryHandler<int, IHoldStyle> style;

		public Dictionary<string, int> ID;

		public static int numStart = 3;

		public HoldStyleHandler()
		{
			ID = new Dictionary<string, int>();
			style = new DictionaryHandler<int, IHoldStyle>();
		}

		public void SetStyle(Player player, Item item)
		{
			int holdStyle = item.holdStyle;
			if (style.ContainsKey(holdStyle))
			{
				style[holdStyle].SetStyle(player, item);
			}
		}

		public void SetFrame(Player player, Item item)
		{
			int holdStyle = item.holdStyle;
			if (style.ContainsKey(holdStyle))
			{
				style[holdStyle].SetFrame(player, item);
			}
		}
	}
}
