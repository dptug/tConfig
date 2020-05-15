using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public static class IniFileSettings
	{
		private enum iniFlags
		{
			PreserveFormatting = 1,
			AllowEmptyValues = 2,
			AllowTextOnTheRight = 4,
			GroupElements = 8,
			CaseSensitive = 0x10,
			SeparateHeader = 0x20,
			AllowBlankLines = 0x40,
			AllowInlineComments = 0x80
		}

		internal struct indexOfAnyResult
		{
			public int index;

			public string any;

			public indexOfAnyResult(int i, string _any)
			{
				any = _any;
				index = i;
			}
		}

		private static iniFlags flags = (iniFlags)255;

		private static string[] commentChars = new string[2]
		{
			";",
			"#"
		};

		private static char? quoteChar = null;

		private static string defaultValueFormatting = "?=$   ;";

		private static string defaultSectionFormatting = "[$]   ;";

		private static string sectionCloseBracket = "]";

		private static string equalsString = "=";

		private static string tabReplacement = "    ";

		private static string sectionOpenBracket = "[";

		public static bool PreserveFormatting
		{
			get
			{
				return (flags & iniFlags.PreserveFormatting) == iniFlags.PreserveFormatting;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.PreserveFormatting;
				}
				else
				{
					flags &= (iniFlags)(-2);
				}
			}
		}

		public static bool AllowEmptyValues
		{
			get
			{
				return (flags & iniFlags.AllowEmptyValues) == iniFlags.AllowEmptyValues;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.AllowEmptyValues;
				}
				else
				{
					flags &= (iniFlags)(-3);
				}
			}
		}

		public static bool AllowTextOnTheRight
		{
			get
			{
				return (flags & iniFlags.AllowTextOnTheRight) == iniFlags.AllowTextOnTheRight;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.AllowTextOnTheRight;
				}
				else
				{
					flags &= (iniFlags)(-5);
				}
			}
		}

		public static bool GroupElements
		{
			get
			{
				return (flags & iniFlags.GroupElements) == iniFlags.GroupElements;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.GroupElements;
				}
				else
				{
					flags &= (iniFlags)(-9);
				}
			}
		}

		public static bool CaseSensitive
		{
			get
			{
				return (flags & iniFlags.CaseSensitive) == iniFlags.CaseSensitive;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.CaseSensitive;
				}
				else
				{
					flags &= (iniFlags)(-17);
				}
			}
		}

		public static bool SeparateHeader
		{
			get
			{
				return (flags & iniFlags.SeparateHeader) == iniFlags.SeparateHeader;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.SeparateHeader;
				}
				else
				{
					flags &= (iniFlags)(-33);
				}
			}
		}

		public static bool AllowBlankLines
		{
			get
			{
				return (flags & iniFlags.AllowBlankLines) == iniFlags.AllowBlankLines;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.AllowBlankLines;
				}
				else
				{
					flags &= (iniFlags)(-65);
				}
			}
		}

		public static bool AllowInlineComments
		{
			get
			{
				return (flags & iniFlags.AllowInlineComments) != 0;
			}
			set
			{
				if (value)
				{
					flags |= iniFlags.AllowInlineComments;
				}
				else
				{
					flags &= (iniFlags)(-129);
				}
			}
		}

		public static string SectionCloseBracket
		{
			get
			{
				return sectionCloseBracket;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SectionCloseBracket");
				}
				sectionCloseBracket = value;
			}
		}

		public static string[] CommentChars
		{
			get
			{
				return commentChars;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CommentChars", "Use empty array to disable comments instead of null");
				}
				commentChars = value;
			}
		}

		public static char? QuoteChar
		{
			get
			{
				return quoteChar;
			}
			set
			{
				quoteChar = value;
			}
		}

		public static string DefaultSectionFormatting
		{
			get
			{
				return defaultSectionFormatting;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultSectionFormatting");
				}
				string text = value.Replace("$", "").Replace("[", "").Replace("]", "")
					.Replace(";", "");
				if (text.TrimStart().Length > 0)
				{
					throw new ArgumentException("DefaultValueFormatting property cannot contain other characters than [,$,] and white spaces.");
				}
				if (value.IndexOf('[') >= value.IndexOf('$') || value.IndexOf('$') >= value.IndexOf(']') || (value.IndexOf(';') != -1 && value.IndexOf(']') >= value.IndexOf(';')))
				{
					throw new ArgumentException("Special charcters in the formatting strings are in the incorrect order. The valid is: [, $, ].");
				}
				defaultSectionFormatting = value;
			}
		}

		public static string DefaultValueFormatting
		{
			get
			{
				return defaultValueFormatting;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultValueFormatting");
				}
				string text = value.Replace("?", "").Replace("$", "").Replace("=", "")
					.Replace(";", "");
				if (text.TrimStart().Length > 0)
				{
					throw new ArgumentException("DefaultValueFormatting property cannot contain other characters than ?,$,= and white spaces.");
				}
				if (((value.IndexOf('?') >= value.IndexOf('=') || value.IndexOf('=') >= value.IndexOf('$')) && (value.IndexOf('=') != -1 || text.IndexOf('?') >= value.IndexOf('$'))) || (value.IndexOf(';') != -1 && value.IndexOf('$') >= value.IndexOf(';')))
				{
					throw new ArgumentException("Special charcters in the formatting strings are in the incorrect order. The valid is: ?, =, $.");
				}
				defaultValueFormatting = value;
			}
		}

		public static string SectionOpenBracket
		{
			get
			{
				return sectionOpenBracket;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("SectionCloseBracket");
				}
				sectionOpenBracket = value;
			}
		}

		public static string EqualsString
		{
			get
			{
				return equalsString;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("EqualsString");
				}
				equalsString = value;
			}
		}

		public static string TabReplacement
		{
			get
			{
				return tabReplacement;
			}
			set
			{
				tabReplacement = value;
			}
		}

		internal static string trimLeft(ref string str)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (; i < str.Length && char.IsWhiteSpace(str, i); i++)
			{
				stringBuilder.Append(str[i]);
			}
			if (str.Length > i)
			{
				str = str.Substring(i);
			}
			else
			{
				str = "";
			}
			return stringBuilder.ToString();
		}

		internal static string trimRight(ref string str)
		{
			int num = str.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			while (num >= 0 && char.IsWhiteSpace(str, num))
			{
				stringBuilder.Append(str[num]);
				num--;
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int num2 = stringBuilder.Length - 1; num2 >= 0; num2--)
			{
				stringBuilder2.Append(stringBuilder[num2]);
			}
			if (str.Length - num > 0)
			{
				str = str.Substring(0, num + 1);
			}
			else
			{
				str = "";
			}
			return stringBuilder2.ToString();
		}

		internal static string startsWith(string line, string[] array)
		{
			if (array == null)
			{
				return null;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (line.StartsWith(array[i]))
				{
					return array[i];
				}
			}
			return null;
		}

		internal static indexOfAnyResult indexOfAny(string text, string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (text.Contains(array[i]))
				{
					return new indexOfAnyResult(text.IndexOf(array[i]), array[i]);
				}
			}
			return new indexOfAnyResult(-1, null);
		}

		internal static string ofAny(int index, string text, string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (text.Length - index >= array[i].Length && text.Substring(index, array[i].Length) == array[i])
				{
					return array[i];
				}
			}
			return null;
		}
	}
}
