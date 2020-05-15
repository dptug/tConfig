using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
	public interface OnScreenInterfaceable
	{
		void DrawOnScreen(SpriteBatch sb, double layer);
	}
}
