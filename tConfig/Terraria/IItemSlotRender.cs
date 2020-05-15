using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
	public interface IItemSlotRender
	{
		void PreDrawItemInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos, float sc, ref bool letDraw);

		void PostDrawItemInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos, float sc, bool ranVanilla);
	}
}
