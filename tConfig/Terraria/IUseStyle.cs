using Microsoft.Xna.Framework;

namespace Terraria
{
	public interface IUseStyle
	{
		void SetStyle(Player player, Item item);

		void SetFrame(Player player, Item item);

		Rectangle UpdateHitBox(Player player, Item item, Rectangle rectangle);
	}
}
