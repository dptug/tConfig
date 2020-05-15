using System;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileCommentary : IniFileElement
	{
		private string comment;

		private string commentChar;

		public string CommentChar
		{
			get
			{
				return commentChar;
			}
			set
			{
				if (commentChar != value)
				{
					commentChar = value;
					rewrite();
				}
			}
		}

		public string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				if (comment != value)
				{
					comment = value;
					rewrite();
				}
			}
		}

		private IniFileCommentary()
		{
		}

		public IniFileCommentary(string content)
			: base(content)
		{
			if (IniFileSettings.CommentChars.Length == 0)
			{
				throw new NotSupportedException("Comments are disabled. Set the IniFileSettings.CommentChars property to turn them on.");
			}
			commentChar = IniFileSettings.startsWith(base.Content, IniFileSettings.CommentChars);
			if (base.Content.Length > commentChar.Length)
			{
				comment = base.Content.Substring(commentChar.Length);
			}
			else
			{
				comment = "";
			}
		}

		private void rewrite()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = comment.Split(new string[1]
			{
				Environment.NewLine
			}, StringSplitOptions.None);
			stringBuilder.Append(commentChar + array[0]);
			for (int i = 1; i < array.Length; i++)
			{
				stringBuilder.Append(Environment.NewLine + commentChar + array[i]);
			}
			base.Content = stringBuilder.ToString();
		}

		public static bool IsLineValid(string testString)
		{
			return IniFileSettings.startsWith(testString.TrimStart(), IniFileSettings.CommentChars) != null;
		}

		public override string ToString()
		{
			return "Comment: \"" + comment + "\"";
		}

		public static IniFileCommentary FromComment(string comment)
		{
			if (IniFileSettings.CommentChars.Length == 0)
			{
				throw new NotSupportedException("Comments are disabled. Set the IniFileSettings.CommentChars property to turn them on.");
			}
			IniFileCommentary iniFileCommentary = new IniFileCommentary();
			iniFileCommentary.comment = comment;
			iniFileCommentary.CommentChar = IniFileSettings.CommentChars[0];
			return iniFileCommentary;
		}

		public override void FormatDefault()
		{
			base.FormatDefault();
			CommentChar = IniFileSettings.CommentChars[0];
			rewrite();
		}
	}
}
