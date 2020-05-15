namespace Terraria
{
	public class MouseTip
	{
		public string text;

		public bool colored;

		public bool red;

		public MouseTip(string text, bool colored = true, bool red = false)
		{
			this.text = text;
			this.colored = colored;
			this.red = red;
		}
	}
}
