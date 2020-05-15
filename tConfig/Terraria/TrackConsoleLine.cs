using Microsoft.Xna.Framework;

namespace Terraria
{
	public class TrackConsoleLine
	{
		public readonly string tag;

		public string print;

		public Color color;

		public int ticks;

		public TrackConsoleLine(string tag, string print, Color color, int ticks)
		{
			this.tag = tag;
			this.print = print;
			this.color = color;
			this.ticks = ticks;
		}
	}
}
