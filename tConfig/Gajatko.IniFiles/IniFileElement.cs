using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileElement
	{
		private string line;

		protected string formatting = "";

		public string Formatting
		{
			get
			{
				return formatting;
			}
			set
			{
				formatting = value;
			}
		}

		public string Intendation
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < formatting.Length && char.IsWhiteSpace(formatting[i]); i++)
				{
					stringBuilder.Append(formatting[i]);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value.TrimStart().Length > 0)
				{
					throw new ArgumentException("Intendation property cannot contain any characters which are not condsidered as white ones.");
				}
				if (IniFileSettings.TabReplacement != null)
				{
					value = value.Replace("\t", IniFileSettings.TabReplacement);
				}
				formatting = value + formatting.TrimStart();
				line = value + line.TrimStart();
			}
		}

		public string Content
		{
			get
			{
				return line.TrimStart();
			}
			protected set
			{
				line = value;
			}
		}

		public string Line
		{
			get
			{
				string intendation = Intendation;
				if (line.Contains(Environment.NewLine))
				{
					string[] array = line.Split(new string[1]
					{
						Environment.NewLine
					}, StringSplitOptions.None);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(array[0]);
					for (int i = 1; i < array.Length; i++)
					{
						stringBuilder.Append(Environment.NewLine + intendation + array[i]);
					}
					return stringBuilder.ToString();
				}
				return line;
			}
		}

		protected IniFileElement()
		{
			line = "";
		}

		public IniFileElement(string _content)
		{
			line = _content.TrimEnd();
		}

		public override string ToString()
		{
			return "Line: \"" + line + "\"";
		}

		public virtual void FormatDefault()
		{
			Intendation = "";
		}
	}
}
