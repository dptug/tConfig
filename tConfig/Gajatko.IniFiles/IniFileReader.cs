using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileReader : StreamReader
	{
		private IniFileElement current;

		public IniFileElement Current => current;

		public IniFileReader(Stream str)
			: base(str)
		{
		}

		public IniFileReader(Stream str, Encoding enc)
			: base(str, enc)
		{
		}

		public IniFileReader(string path)
			: base(path)
		{
		}

		public IniFileReader(string path, Encoding enc)
			: base(path, enc)
		{
		}

		public static IniFileElement ParseLine(string line)
		{
			if (line == null)
			{
				return null;
			}
			if (line.Contains("\n"))
			{
				throw new ArgumentException("String passed to the ParseLine method cannot contain more than one line.");
			}
			string testString = line.Trim();
			IniFileElement iniFileElement = null;
			if (IniFileBlankLine.IsLineValid(testString))
			{
				iniFileElement = new IniFileBlankLine(1);
			}
			else if (IniFileCommentary.IsLineValid(line))
			{
				iniFileElement = new IniFileCommentary(line);
			}
			else if (IniFileSectionStart.IsLineValid(testString))
			{
				iniFileElement = new IniFileSectionStart(line);
			}
			else if (IniFileValue.IsLineValid(testString))
			{
				iniFileElement = new IniFileValue(line);
			}
			return iniFileElement ?? new IniFileElement(line);
		}

		public static List<IniFileElement> ParseText(string text)
		{
			if (text == null)
			{
				return null;
			}
			List<IniFileElement> list = new List<IniFileElement>();
			IniFileElement iniFileElement = null;
			string[] array = text.Split(new string[1]
			{
				Environment.NewLine
			}, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				IniFileElement iniFileElement2 = ParseLine(array[i]);
				if (IniFileSettings.GroupElements)
				{
					if (iniFileElement != null)
					{
						if (iniFileElement2 is IniFileBlankLine && iniFileElement is IniFileBlankLine)
						{
							((IniFileBlankLine)iniFileElement).Amount++;
							continue;
						}
						if (iniFileElement2 is IniFileCommentary && iniFileElement is IniFileCommentary)
						{
							IniFileCommentary obj = (IniFileCommentary)iniFileElement;
							obj.Comment = obj.Comment + Environment.NewLine + ((IniFileCommentary)iniFileElement2).Comment;
							continue;
						}
					}
					else
					{
						iniFileElement = iniFileElement2;
					}
				}
				iniFileElement = iniFileElement2;
				list.Add(iniFileElement2);
			}
			return list;
		}

		public IniFileElement ReadElement()
		{
			current = ParseLine(ReadLine());
			return current;
		}

		public List<IniFileElement> ReadElementsToEnd()
		{
			return ParseText(ReadToEnd());
		}

		public IniFileSectionStart GotoSection(string sectionName)
		{
			IniFileSectionStart iniFileSectionStart = null;
			while (true)
			{
				string text = ReadLine();
				if (text == null)
				{
					current = null;
					return null;
				}
				if (IniFileSectionStart.IsLineValid(text))
				{
					iniFileSectionStart = (ParseLine(text) as IniFileSectionStart);
					if (iniFileSectionStart != null && (iniFileSectionStart.SectionName == sectionName || (!IniFileSettings.CaseSensitive && iniFileSectionStart.SectionName.ToLowerInvariant() == sectionName)))
					{
						break;
					}
				}
			}
			current = iniFileSectionStart;
			return iniFileSectionStart;
		}

		public List<IniFileElement> ReadSection()
		{
			if (current == null || !(current is IniFileSectionStart))
			{
				throw new InvalidOperationException("The current position of the reader must be at IniFileSectionStart. Use GotoSection method");
			}
			List<IniFileElement> list = new List<IniFileElement>();
			IniFileElement item = current;
			list.Add(item);
			string text = "";
			string text2;
			while ((text2 = ReadLine()) != null)
			{
				if (IniFileSectionStart.IsLineValid(text2.Trim()))
				{
					current = new IniFileSectionStart(text2);
					break;
				}
				text = text + text2 + Environment.NewLine;
			}
			if (text.EndsWith(Environment.NewLine) && text != Environment.NewLine)
			{
				text = text.Substring(0, text.Length - Environment.NewLine.Length);
			}
			list.AddRange(ParseText(text));
			return list;
		}

		public List<IniFileValue> ReadSectionValues()
		{
			List<IniFileElement> list = ReadSection();
			List<IniFileValue> list2 = new List<IniFileValue>();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] is IniFileValue)
				{
					list2.Add((IniFileValue)list[i]);
				}
			}
			return list2;
		}

		public IniFileValue GotoValue(string key)
		{
			return GotoValue(key, searchWholeFile: false);
		}

		public IniFileValue GotoValue(string key, bool searchWholeFile)
		{
			string text;
			do
			{
				text = ReadLine();
				if (text == null)
				{
					return null;
				}
				if (IniFileValue.IsLineValid(text.Trim()))
				{
					IniFileValue iniFileValue = ParseLine(text) as IniFileValue;
					if (iniFileValue != null && (iniFileValue.Key == key || (!IniFileSettings.CaseSensitive && iniFileValue.Key.ToLowerInvariant() == key.ToLowerInvariant())))
					{
						return iniFileValue;
					}
				}
			}
			while (searchWholeFile || !IniFileSectionStart.IsLineValid(text.Trim()));
			return null;
		}
	}
}
