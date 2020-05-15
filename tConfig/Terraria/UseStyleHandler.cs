using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria
{
	public class UseStyleHandler
	{
		public DictionaryHandler<int, IUseStyle> style;

		public Dictionary<string, int> ID;

		public static int numStart = 10;

		public UseStyleHandler()
		{
			ID = new Dictionary<string, int>();
			style = new DictionaryHandler<int, IUseStyle>();
		}

		public void SetStyle(Player player, Item item)
		{
			int useStyle = item.useStyle;
			if (style.ContainsKey(useStyle))
			{
				style[useStyle].SetStyle(player, item);
			}
		}

		public void SetFrame(Player player, Item item)
		{
			int useStyle = item.useStyle;
			if (style.ContainsKey(useStyle))
			{
				style[useStyle].SetFrame(player, item);
			}
		}

		public Rectangle UpdateHitBox(Player player, Item item, Rectangle rectangle)
		{
			int useStyle = item.useStyle;
			if (style.ContainsKey(useStyle))
			{
				return style[useStyle].UpdateHitBox(player, item, rectangle);
			}
			return rectangle;
		}
	}
}
