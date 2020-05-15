using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileValue : IniFileElement
	{
		private enum feState
		{
			BeforeEvery,
			AfterKey,
			BeforeVal,
			AfterVal
		}

		private string key;

		private string value;

		private string textOnTheRight;

		private string inlineComment;

		private string inlineCommentChar;

		public string Key
		{
			get
			{
				return key;
			}
			set
			{
				key = value;
				Format();
			}
		}

		public string Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
				Format();
			}
		}

		public string InlineComment
		{
			get
			{
				return inlineComment;
			}
			set
			{
				if (!IniFileSettings.AllowInlineComments || IniFileSettings.CommentChars.Length == 0)
				{
					throw new NotSupportedException("Inline comments are disabled.");
				}
				if (inlineCommentChar == null)
				{
					inlineCommentChar = IniFileSettings.CommentChars[0];
				}
				inlineComment = value;
				Format();
			}
		}

		private IniFileValue()
		{
		}

		public IniFileValue(string content)
			: base(content)
		{
			string[] array = base.Content.Split(new string[1]
			{
				IniFileSettings.EqualsString
			}, StringSplitOptions.None);
			formatting = ExtractFormat(content);
			string text = array[0].Trim();
			string text2 = (array.Length >= 1) ? array[1].Trim() : "";
			if (text.Length > 0)
			{
				if (IniFileSettings.AllowInlineComments)
				{
					IniFileSettings.indexOfAnyResult indexOfAnyResult = IniFileSettings.indexOfAny(text2, IniFileSettings.CommentChars);
					if (indexOfAnyResult.index != -1)
					{
						inlineComment = text2.Substring(indexOfAnyResult.index + indexOfAnyResult.any.Length);
						text2 = text2.Substring(0, indexOfAnyResult.index).TrimEnd();
						inlineCommentChar = indexOfAnyResult.any;
					}
				}
				if (((int?)IniFileSettings.QuoteChar).HasValue && text2.Length >= 2)
				{
					char c = IniFileSettings.QuoteChar.Value;
					if (text2[0] == c)
					{
						int num;
						if (IniFileSettings.AllowTextOnTheRight)
						{
							num = text2.LastIndexOf(c);
							if (num != text2.Length - 1)
							{
								textOnTheRight = text2.Substring(num + 1);
							}
						}
						else
						{
							num = text2.Length - 1;
						}
						if (num > 0)
						{
							text2 = ((text2.Length != 2) ? text2.Substring(1, num - 1) : "");
						}
					}
				}
				key = text;
				value = text2;
			}
			Format();
		}

		public string ExtractFormat(string content)
		{
			feState feState = feState.BeforeEvery;
			string str = "";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				char c = content[i];
				if (char.IsLetterOrDigit(c))
				{
					switch (feState)
					{
					case feState.BeforeEvery:
						stringBuilder.Append('?');
						feState = feState.AfterKey;
						break;
					case feState.BeforeVal:
						stringBuilder.Append('$');
						feState = feState.AfterVal;
						break;
					}
				}
				else if (feState == feState.AfterKey && content.Length - i >= IniFileSettings.EqualsString.Length && content.Substring(i, IniFileSettings.EqualsString.Length) == IniFileSettings.EqualsString)
				{
					stringBuilder.Append(str);
					feState = feState.BeforeVal;
					stringBuilder.Append('=');
				}
				else if (IniFileSettings.ofAny(i, content, IniFileSettings.CommentChars) != null)
				{
					stringBuilder.Append(str);
					stringBuilder.Append(';');
				}
				else if (char.IsWhiteSpace(c))
				{
					string str2 = (c != '\t' || IniFileSettings.TabReplacement == null) ? c.ToString() : IniFileSettings.TabReplacement;
					if (feState == feState.AfterKey || feState == feState.AfterVal)
					{
						str += str2;
						continue;
					}
					stringBuilder.Append(str2);
				}
				str = "";
			}
			if (feState == feState.BeforeVal)
			{
				stringBuilder.Append('$');
				feState = feState.AfterVal;
			}
			string text = stringBuilder.ToString();
			if (text.IndexOf(';') == -1)
			{
				text += "   ;";
			}
			return text;
		}

		public void Format()
		{
			Format(formatting);
		}

		public void Format(string formatting)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < formatting.Length; i++)
			{
				char c = formatting[i];
				switch (c)
				{
				case '?':
					stringBuilder.Append(key);
					break;
				case '$':
					if (((int?)IniFileSettings.QuoteChar).HasValue)
					{
						char c2 = IniFileSettings.QuoteChar.Value;
						stringBuilder.Append(c2).Append(value).Append(c2);
					}
					else
					{
						stringBuilder.Append(value);
					}
					break;
				case '=':
					stringBuilder.Append(IniFileSettings.EqualsString);
					break;
				case ';':
					stringBuilder.Append(inlineCommentChar + inlineComment);
					break;
				default:
					if (char.IsWhiteSpace(formatting[i]))
					{
						stringBuilder.Append(c);
					}
					break;
				}
			}
			base.Content = stringBuilder.ToString().TrimEnd() + (IniFileSettings.AllowTextOnTheRight ? textOnTheRight : "");
		}

		public override void FormatDefault()
		{
			base.Formatting = IniFileSettings.DefaultValueFormatting;
			Format();
		}

		public IniFileValue CreateNew(string key, string value)
		{
			IniFileValue iniFileValue = new IniFileValue();
			iniFileValue.key = key;
			iniFileValue.value = value;
			if (IniFileSettings.PreserveFormatting)
			{
				iniFileValue.formatting = formatting;
				if (IniFileSettings.AllowInlineComments)
				{
					iniFileValue.inlineCommentChar = inlineCommentChar;
				}
				iniFileValue.Format();
			}
			else
			{
				iniFileValue.FormatDefault();
			}
			return iniFileValue;
		}

		public static bool IsLineValid(string testString)
		{
			int num = testString.IndexOf(IniFileSettings.EqualsString);
			return num > 0;
		}

		public void Set(string key, string value)
		{
			this.key = key;
			this.value = value;
			Format();
		}

		public override string ToString()
		{
			return "Value: \"" + key + " = " + value + "\"";
		}

		public static IniFileValue FromData(string key, string value)
		{
			IniFileValue iniFileValue = new IniFileValue();
			iniFileValue.key = key;
			iniFileValue.value = value;
			iniFileValue.FormatDefault();
			return iniFileValue;
		}
	}
}
