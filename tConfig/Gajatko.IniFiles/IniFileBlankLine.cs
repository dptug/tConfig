using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileBlankLine : IniFileElement
	{
		public int Amount
		{
			get
			{
				return base.Line.Length / Environment.NewLine.Length + 1;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Cannot set Amount to less than 1.");
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i < value; i++)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				base.Content = stringBuilder.ToString();
			}
		}

		public IniFileBlankLine(int amount)
			: base("")
		{
			Amount = amount;
		}

		public static bool IsLineValid(string testString)
		{
			return testString == "";
		}

		public override string ToString()
		{
			return Amount + " blank line(s)";
		}

		public override void FormatDefault()
		{
			Amount = 1;
			base.FormatDefault();
		}
	}
}
