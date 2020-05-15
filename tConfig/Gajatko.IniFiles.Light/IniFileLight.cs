using System.Collections.Generic;
using System.IO;

namespace Gajatko.IniFiles.Light
{
	public class IniFileLight
	{
		public static char EQUALITY_SIGN = '=';

		public static char SECTION_OPEN_BRACKET = '[';

		public static char SECTION_CLOSE_BRACKET = ']';

		public static char COMMENT_START_1 = ';';

		public static char COMMENT_START_2 = '#';

		private Dictionary<string, Dictionary<string, string>> sections = new Dictionary<string, Dictionary<string, string>>();

		private Dictionary<string, string> comments = new Dictionary<string, string>();

		public Dictionary<string, Dictionary<string, string>> Sections => sections;

		public Dictionary<string, string> Comments => comments;

		public IniFileLight(string path)
		{
			StreamReader streamReader = File.OpenText(path);
			load(streamReader);
			streamReader.Close();
		}

		public IniFileLight(Stream stream)
		{
			StreamReader reader = new StreamReader(stream);
			load(reader);
		}

		public IniFileLight()
		{
		}

		private void load(StreamReader reader)
		{
			string text = null;
			string text2 = null;
			while (reader.Peek() != -1)
			{
				string text3 = reader.ReadLine().Trim();
				if (text3.Length <= 0)
				{
					continue;
				}
				int num;
				if (text3[0] == COMMENT_START_1 || text3[0] == COMMENT_START_2)
				{
					if (text3.Length > 1)
					{
						text2 = text3.Substring(1);
					}
				}
				else if (text != null && (num = text3.IndexOf(EQUALITY_SIGN)) > 0 && num < text3.Length - 1)
				{
					string text4 = text3.Substring(0, num).TrimEnd();
					string value = text3.Substring(num + 1, text3.Length - num - 1).TrimStart();
					if (text2 != null)
					{
						string key = text + "." + text4;
						if (comments.ContainsKey(key))
						{
							comments[key] = text2;
						}
						else
						{
							comments.Add(key, text2);
						}
						text2 = null;
					}
					if (sections[text].ContainsKey(text4))
					{
						sections[text][text4] = value;
					}
					else
					{
						sections[text].Add(text4, value);
					}
				}
				else
				{
					if (text3.Length <= 2 || text3[0] != SECTION_OPEN_BRACKET || text3[text3.Length - 1] != SECTION_CLOSE_BRACKET)
					{
						continue;
					}
					text = text3.Substring(1, text3.Length - 2).Trim();
					if (text2 != null)
					{
						string key = text;
						if (comments.ContainsKey(key))
						{
							comments[key] = text2;
						}
						else
						{
							comments.Add(key, text2);
						}
						text2 = null;
					}
					if (!sections.ContainsKey(text))
					{
						sections.Add(text, new Dictionary<string, string>());
					}
				}
			}
			if (text2 != null && !comments.ContainsKey(""))
			{
				comments.Add("", text2);
			}
		}

		private void save(StreamWriter writer)
		{
			lock (sections)
			{
				foreach (KeyValuePair<string, Dictionary<string, string>> section in sections)
				{
					if (comments.ContainsKey(section.Key))
					{
						writer.WriteLine(COMMENT_START_1 + comments[section.Key]);
					}
					writer.WriteLine("[" + section.Key + "]");
					foreach (KeyValuePair<string, string> item in section.Value)
					{
						if (comments.ContainsKey(section.Key + "." + item.Key))
						{
							writer.WriteLine(COMMENT_START_1 + comments[section.Key + "." + item.Key]);
						}
						writer.WriteLine(item.Key + "=" + item.Value);
					}
				}
				if (comments.ContainsKey(""))
				{
					writer.WriteLine(COMMENT_START_1 + comments[""]);
				}
			}
		}

		public void Save(string path)
		{
			StreamWriter streamWriter = File.CreateText(path);
			save(streamWriter);
			streamWriter.Close();
		}

		public void Save(Stream stream)
		{
			StreamWriter writer = new StreamWriter(stream);
			save(writer);
		}
	}
}
