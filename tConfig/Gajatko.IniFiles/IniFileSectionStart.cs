using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileSectionStart : IniFileElement
	{
		private string sectionName;

		private string textOnTheRight;

		private string inlineComment;

		private string inlineCommentChar;

		public string SectionName
		{
			get
			{
				return sectionName;
			}
			set
			{
				sectionName = value;
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
				inlineComment = value;
				Format();
			}
		}

		private IniFileSectionStart()
		{
		}

		public IniFileSectionStart(string content)
			: base(content)
		{
			formatting = ExtractFormat(content);
			content = content.TrimStart();
			if (IniFileSettings.AllowInlineComments)
			{
				IniFileSettings.indexOfAnyResult indexOfAnyResult = IniFileSettings.indexOfAny(content, IniFileSettings.CommentChars);
				if (indexOfAnyResult.index > content.IndexOf(IniFileSettings.SectionCloseBracket))
				{
					inlineComment = content.Substring(indexOfAnyResult.index + indexOfAnyResult.any.Length);
					inlineCommentChar = indexOfAnyResult.any;
					content = content.Substring(0, indexOfAnyResult.index);
				}
			}
			if (IniFileSettings.AllowTextOnTheRight)
			{
				int num = content.LastIndexOf(IniFileSettings.SectionCloseBracket);
				if (num != content.Length - 1)
				{
					textOnTheRight = content.Substring(num + 1);
					content = content.Substring(0, num);
				}
			}
			sectionName = content.Substring(IniFileSettings.SectionOpenBracket.Length, content.Length - IniFileSettings.SectionCloseBracket.Length - IniFileSettings.SectionOpenBracket.Length).Trim();
			base.Content = content;
			Format();
		}

		public static bool IsLineValid(string testString)
		{
			if (testString.StartsWith(IniFileSettings.SectionOpenBracket))
			{
				return testString.EndsWith(IniFileSettings.SectionCloseBracket);
			}
			return false;
		}

		public override string ToString()
		{
			return "Section: \"" + sectionName + "\"";
		}

		public IniFileSectionStart CreateNew(string sectName)
		{
			IniFileSectionStart iniFileSectionStart = new IniFileSectionStart();
			iniFileSectionStart.sectionName = sectName;
			if (IniFileSettings.PreserveFormatting)
			{
				iniFileSectionStart.formatting = formatting;
				iniFileSectionStart.Format();
			}
			else
			{
				iniFileSectionStart.Format();
			}
			return iniFileSectionStart;
		}

		public static string ExtractFormat(string content)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			string text = "";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				char c = content[i];
				if (char.IsLetterOrDigit(c) && flag)
				{
					flag2 = true;
					flag = false;
					stringBuilder.Append('$');
				}
				else if (flag2 && char.IsLetterOrDigit(c))
				{
					text = "";
				}
				else if (content.Length - i >= IniFileSettings.SectionOpenBracket.Length && content.Substring(i, IniFileSettings.SectionOpenBracket.Length) == IniFileSettings.SectionOpenBracket && flag3)
				{
					flag = true;
					flag3 = false;
					stringBuilder.Append('[');
				}
				else if (content.Length - i >= IniFileSettings.SectionCloseBracket.Length && content.Substring(i, IniFileSettings.SectionOpenBracket.Length) == IniFileSettings.SectionCloseBracket && flag2)
				{
					stringBuilder.Append(text);
					flag2 = false;
					stringBuilder.Append(IniFileSettings.SectionCloseBracket);
				}
				else if (IniFileSettings.ofAny(i, content, IniFileSettings.CommentChars) != null)
				{
					stringBuilder.Append(';');
				}
				else if (char.IsWhiteSpace(c))
				{
					if (flag2)
					{
						text += c;
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
			}
			string text2 = stringBuilder.ToString();
			if (text2.IndexOf(';') == -1)
			{
				text2 += "   ;";
			}
			return text2;
		}

		public override void FormatDefault()
		{
			base.Formatting = IniFileSettings.DefaultSectionFormatting;
			Format();
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
				switch (formatting[i])
				{
				case '$':
					stringBuilder.Append(sectionName);
					continue;
				case '[':
					stringBuilder.Append(IniFileSettings.SectionOpenBracket);
					continue;
				case ']':
					stringBuilder.Append(IniFileSettings.SectionCloseBracket);
					continue;
				case ';':
					if (IniFileSettings.CommentChars.Length > 0 && inlineComment != null)
					{
						stringBuilder.Append(IniFileSettings.CommentChars[0]).Append(inlineComment);
						continue;
					}
					break;
				}
				if (char.IsWhiteSpace(formatting[i]))
				{
					stringBuilder.Append(formatting[i]);
				}
			}
			base.Content = stringBuilder.ToString().TrimEnd() + (IniFileSettings.AllowTextOnTheRight ? textOnTheRight : "");
		}

		public static IniFileSectionStart FromName(string sectionName)
		{
			IniFileSectionStart iniFileSectionStart = new IniFileSectionStart();
			iniFileSectionStart.SectionName = sectionName;
			iniFileSectionStart.FormatDefault();
			return iniFileSectionStart;
		}
	}
}
