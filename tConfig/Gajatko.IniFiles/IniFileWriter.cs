using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gajatko.IniFiles
{
	public class IniFileWriter : StreamWriter
	{
		public IniFileWriter(Stream str)
			: base(str)
		{
		}

		public IniFileWriter(string str)
			: base(str)
		{
		}

		public IniFileWriter(Stream str, Encoding enc)
			: base(str, enc)
		{
		}

		public IniFileWriter(string str, bool append)
			: base(str, append)
		{
		}

		public void WriteElement(IniFileElement element)
		{
			if (!IniFileSettings.PreserveFormatting)
			{
				element.FormatDefault();
			}
			if ((!(element is IniFileBlankLine) || IniFileSettings.AllowBlankLines) && (IniFileSettings.AllowEmptyValues || !(element is IniFileValue) || !(((IniFileValue)element).Value == "")))
			{
				base.WriteLine(element.Line);
			}
		}

		public void WriteElements(IEnumerable<IniFileElement> elements)
		{
			lock (elements)
			{
				foreach (IniFileElement element in elements)
				{
					WriteElement(element);
				}
			}
		}

		public void WriteIniFile(IniFile file)
		{
			WriteElements(file.elements);
		}

		public void WriteSection(IniFileSection section)
		{
			WriteElement(section.sectionStart);
			for (int i = section.parent.elements.IndexOf(section.sectionStart) + 1; i < section.parent.elements.Count && !(section.parent.elements[i] is IniFileSectionStart); i++)
			{
				WriteElement(section.parent.elements[i]);
			}
		}
	}
}
