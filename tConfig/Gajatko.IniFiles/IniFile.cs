using System.Collections.Generic;
using System.IO;

namespace Gajatko.IniFiles
{
	public class IniFile
	{
		internal List<IniFileSection> sections = new List<IniFileSection>();

		internal List<IniFileElement> elements = new List<IniFileElement>();

		public IniFileSection this[string sectionName]
		{
			get
			{
				IniFileSection section = getSection(sectionName);
				if (section != null)
				{
					return section;
				}
				IniFileSectionStart iniFileSectionStart;
				if (sections.Count > 0)
				{
					IniFileSectionStart sectionStart = sections[sections.Count - 1].sectionStart;
					iniFileSectionStart = sectionStart.CreateNew(sectionName);
				}
				else
				{
					iniFileSectionStart = IniFileSectionStart.FromName(sectionName);
				}
				elements.Add(iniFileSectionStart);
				section = new IniFileSection(this, iniFileSectionStart);
				sections.Add(section);
				return section;
			}
		}

		public string Header
		{
			get
			{
				if (elements.Count > 0 && elements[0] is IniFileCommentary && (IniFileSettings.SeparateHeader || elements.Count <= 1 || elements[1] is IniFileBlankLine))
				{
					return ((IniFileCommentary)elements[0]).Comment;
				}
				return "";
			}
			set
			{
				if (elements.Count > 0 && elements[0] is IniFileCommentary && (IniFileSettings.SeparateHeader || elements.Count <= 1 || elements[1] is IniFileBlankLine))
				{
					if (value == "")
					{
						elements.RemoveAt(0);
						if (IniFileSettings.SeparateHeader && elements.Count > 0 && elements[0] is IniFileBlankLine)
						{
							elements.RemoveAt(0);
						}
					}
					else
					{
						((IniFileCommentary)elements[0]).Comment = value;
					}
				}
				else if (value != "")
				{
					if ((elements.Count == 0 || !(elements[0] is IniFileBlankLine)) && IniFileSettings.SeparateHeader)
					{
						elements.Insert(0, new IniFileBlankLine(1));
					}
					elements.Insert(0, IniFileCommentary.FromComment(value));
				}
			}
		}

		public string Foot
		{
			get
			{
				if (elements.Count > 0 && elements[elements.Count - 1] is IniFileCommentary)
				{
					return ((IniFileCommentary)elements[elements.Count - 1]).Comment;
				}
				return "";
			}
			set
			{
				if (value == "")
				{
					if (elements.Count > 0 && elements[elements.Count - 1] is IniFileCommentary)
					{
						elements.RemoveAt(elements.Count - 1);
						if (elements.Count > 0 && elements[elements.Count - 1] is IniFileBlankLine)
						{
							elements.RemoveAt(elements.Count - 1);
						}
					}
				}
				else if (elements.Count > 0)
				{
					if (elements[elements.Count - 1] is IniFileCommentary)
					{
						((IniFileCommentary)elements[elements.Count - 1]).Comment = value;
					}
					else
					{
						elements.Add(IniFileCommentary.FromComment(value));
					}
					if (elements.Count > 2)
					{
						if (!(elements[elements.Count - 2] is IniFileBlankLine) && IniFileSettings.SeparateHeader)
						{
							elements.Insert(elements.Count - 1, new IniFileBlankLine(1));
						}
						else if (value == "")
						{
							elements.RemoveAt(elements.Count - 2);
						}
					}
				}
				else
				{
					elements.Add(IniFileCommentary.FromComment(value));
				}
			}
		}

		private IniFileSection getSection(string name)
		{
			string b = name.ToLowerInvariant();
			for (int i = 0; i < sections.Count; i++)
			{
				if (sections[i].Name == name || (!IniFileSettings.CaseSensitive && sections[i].Name.ToLowerInvariant() == b))
				{
					return sections[i];
				}
			}
			return null;
		}

		public string[] GetSectionNames()
		{
			string[] array = new string[sections.Count];
			for (int i = 0; i < sections.Count; i++)
			{
				array[i] = sections[i].Name;
			}
			return array;
		}

		public static IniFile FromFile(string path)
		{
			if (!File.Exists(path))
			{
				File.Create(path).Close();
				return new IniFile();
			}
			IniFileReader iniFileReader = new IniFileReader(path);
			IniFile result = FromStream(iniFileReader);
			iniFileReader.Close();
			return result;
		}

		public static IniFile FromElements(IEnumerable<IniFileElement> elemes)
		{
			IniFile iniFile = new IniFile();
			iniFile.elements.AddRange(elemes);
			if (iniFile.elements.Count > 0)
			{
				IniFileSection iniFileSection = null;
				if (iniFile.elements[iniFile.elements.Count - 1] is IniFileBlankLine)
				{
					iniFile.elements.RemoveAt(iniFile.elements.Count - 1);
				}
				for (int i = 0; i < iniFile.elements.Count; i++)
				{
					IniFileElement iniFileElement = iniFile.elements[i];
					if (iniFileElement is IniFileSectionStart)
					{
						iniFileSection = new IniFileSection(iniFile, (IniFileSectionStart)iniFileElement);
						iniFile.sections.Add(iniFileSection);
					}
					else if (iniFileSection != null)
					{
						iniFileSection.elements.Add(iniFileElement);
					}
					else if (iniFile.sections.Exists((IniFileSection a) => a.Name == ""))
					{
						iniFile.sections[0].elements.Add(iniFileElement);
					}
					else if (iniFileElement is IniFileValue)
					{
						iniFileSection = new IniFileSection(iniFile, IniFileSectionStart.FromName(""));
						iniFileSection.elements.Add(iniFileElement);
						iniFile.sections.Add(iniFileSection);
					}
				}
			}
			return iniFile;
		}

		public static IniFile FromStream(IniFileReader reader)
		{
			return FromElements(reader.ReadElementsToEnd());
		}

		public void Save(string path)
		{
			IniFileWriter iniFileWriter = new IniFileWriter(path);
			Save(iniFileWriter);
			iniFileWriter.Close();
		}

		public void Save(IniFileWriter writer)
		{
			writer.WriteIniFile(this);
		}

		public void DeleteSection(string name)
		{
			IniFileSection section = getSection(name);
			if (section != null)
			{
				IniFileSectionStart sectionStart = section.sectionStart;
				elements.Remove(sectionStart);
				for (int i = elements.IndexOf(sectionStart) + 1; i < elements.Count && !(elements[i] is IniFileSectionStart); i++)
				{
					elements.RemoveAt(i);
				}
			}
		}

		public void Format(bool preserveIntendation)
		{
			string intendation = "";
			string intendation2 = "";
			for (int i = 0; i < elements.Count; i++)
			{
				IniFileElement iniFileElement = elements[i];
				if (preserveIntendation)
				{
					if (iniFileElement is IniFileSectionStart)
					{
						intendation2 = (intendation = iniFileElement.Intendation);
					}
					else if (iniFileElement is IniFileValue)
					{
						intendation2 = iniFileElement.Intendation;
					}
				}
				iniFileElement.FormatDefault();
				if (preserveIntendation)
				{
					if (iniFileElement is IniFileSectionStart)
					{
						iniFileElement.Intendation = intendation;
					}
					else if (iniFileElement is IniFileCommentary && i != elements.Count - 1 && !(elements[i + 1] is IniFileBlankLine))
					{
						iniFileElement.Intendation = elements[i + 1].Intendation;
					}
					else
					{
						iniFileElement.Intendation = intendation2;
					}
				}
			}
		}

		public void UnifySections()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			for (int i = 0; i < sections.Count; i++)
			{
				IniFileSection iniFileSection = sections[i];
				if (dictionary.ContainsKey(iniFileSection.Name))
				{
					int index = dictionary[iniFileSection.Name] + 1;
					elements.Remove(iniFileSection.sectionStart);
					sections.Remove(iniFileSection);
					for (int num = iniFileSection.elements.Count - 1; num >= 0; num--)
					{
						IniFileElement iniFileElement = iniFileSection.elements[num];
						if (num != iniFileSection.elements.Count - 1 || !(iniFileElement is IniFileCommentary))
						{
							elements.Remove(iniFileElement);
						}
						if (!(iniFileElement is IniFileBlankLine))
						{
							elements.Insert(index, iniFileElement);
							IniFileValue iniFileValue = this[iniFileSection.Name].firstValue();
							if (iniFileValue != null)
							{
								iniFileElement.Intendation = iniFileValue.Intendation;
							}
							else
							{
								iniFileElement.Intendation = this[iniFileSection.Name].sectionStart.Intendation;
							}
						}
					}
				}
				else
				{
					dictionary.Add(iniFileSection.Name, elements.IndexOf(iniFileSection.sectionStart));
				}
			}
		}
	}
}
