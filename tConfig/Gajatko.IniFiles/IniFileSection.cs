using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gajatko.IniFiles
{
	public class IniFileSection
	{
		internal List<IniFileElement> elements = new List<IniFileElement>();

		internal IniFileSectionStart sectionStart;

		internal IniFile parent;

		public string Name
		{
			get
			{
				return sectionStart.SectionName;
			}
			set
			{
				sectionStart.SectionName = value;
			}
		}

		public string Comment
		{
			get
			{
				if (!(Name == ""))
				{
					return getComment(sectionStart);
				}
				return "";
			}
			set
			{
				if (Name != "")
				{
					setComment(sectionStart, value);
				}
			}
		}

		public string InlineComment
		{
			get
			{
				return sectionStart.InlineComment;
			}
			set
			{
				sectionStart.InlineComment = value;
			}
		}

		public string this[string key]
		{
			get
			{
				return getValue(key)?.Value;
			}
			set
			{
				IniFileValue value2 = getValue(key);
				if (value2 != null)
				{
					value2.Value = value;
				}
				else
				{
					setValue(key, value);
				}
			}
		}

		public string this[string key, string defaultValue]
		{
			get
			{
				string text = this[key];
				if (text == "" || text == null)
				{
					return defaultValue;
				}
				return text;
			}
			set
			{
				this[key] = value;
			}
		}

		internal IniFileSection(IniFile _parent, IniFileSectionStart sect)
		{
			sectionStart = sect;
			parent = _parent;
		}

		private void setComment(IniFileElement el, string comment)
		{
			int num = parent.elements.IndexOf(el);
			if (IniFileSettings.CommentChars.Length == 0)
			{
				throw new NotSupportedException("Comments are currently disabled. Setup ConfigFileSettings.CommentChars property to enable them.");
			}
			if (num > 0 && parent.elements[num - 1] is IniFileCommentary)
			{
				IniFileCommentary iniFileCommentary = (IniFileCommentary)parent.elements[num - 1];
				if (comment == "")
				{
					parent.elements.Remove(iniFileCommentary);
					return;
				}
				iniFileCommentary.Comment = comment;
				iniFileCommentary.Intendation = el.Intendation;
			}
			else if (comment != "")
			{
				IniFileCommentary iniFileCommentary = IniFileCommentary.FromComment(comment);
				iniFileCommentary.Intendation = el.Intendation;
				parent.elements.Insert(num, iniFileCommentary);
			}
		}

		private string getComment(IniFileElement el)
		{
			int num = parent.elements.IndexOf(el);
			if (num != 0 && parent.elements[num - 1] is IniFileCommentary)
			{
				return ((IniFileCommentary)parent.elements[num - 1]).Comment;
			}
			return "";
		}

		private IniFileValue getValue(string key)
		{
			string b = key.ToLowerInvariant();
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i] is IniFileValue)
				{
					IniFileValue iniFileValue = (IniFileValue)elements[i];
					if (iniFileValue.Key == key || (!IniFileSettings.CaseSensitive && iniFileValue.Key.ToLowerInvariant() == b))
					{
						return iniFileValue;
					}
				}
			}
			return null;
		}

		public void SetComment(string key, string comment)
		{
			IniFileValue value = getValue(key);
			if (value != null)
			{
				setComment(value, comment);
			}
		}

		public void SetInlineComment(string key, string comment)
		{
			IniFileValue value = getValue(key);
			if (value != null)
			{
				value.InlineComment = comment;
			}
		}

		public string GetInlineComment(string key)
		{
			return getValue(key)?.InlineComment;
		}

		public string GetComment(string key)
		{
			IniFileValue value = getValue(key);
			if (value == null)
			{
				return null;
			}
			return getComment(value);
		}

		public void RenameKey(string key, string newName)
		{
			IniFileValue value = getValue(key);
			if (key != null)
			{
				value.Key = newName;
			}
		}

		public void DeleteKey(string key)
		{
			IniFileValue value = getValue(key);
			if (key != null)
			{
				parent.elements.Remove(value);
				elements.Remove(value);
			}
		}

		private void setValue(string key, string value)
		{
			IniFileValue iniFileValue = null;
			IniFileValue iniFileValue2 = lastValue();
			if (IniFileSettings.PreserveFormatting)
			{
				if (iniFileValue2 != null && iniFileValue2.Intendation.Length >= sectionStart.Intendation.Length)
				{
					iniFileValue = iniFileValue2.CreateNew(key, value);
				}
				else
				{
					bool flag = false;
					for (int num = parent.elements.IndexOf(sectionStart) - 1; num >= 0; num--)
					{
						IniFileElement iniFileElement = parent.elements[num];
						if (iniFileElement is IniFileValue)
						{
							iniFileValue = ((IniFileValue)iniFileElement).CreateNew(key, value);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						iniFileValue = IniFileValue.FromData(key, value);
					}
					if (iniFileValue.Intendation.Length < sectionStart.Intendation.Length)
					{
						iniFileValue.Intendation = sectionStart.Intendation;
					}
				}
			}
			else
			{
				iniFileValue = IniFileValue.FromData(key, value);
			}
			if (iniFileValue2 == null)
			{
				elements.Insert(elements.IndexOf(sectionStart) + 1, iniFileValue);
				parent.elements.Insert(parent.elements.IndexOf(sectionStart) + 1, iniFileValue);
			}
			else
			{
				elements.Insert(elements.IndexOf(iniFileValue2) + 1, iniFileValue);
				parent.elements.Insert(parent.elements.IndexOf(iniFileValue2) + 1, iniFileValue);
			}
		}

		internal IniFileValue lastValue()
		{
			for (int num = elements.Count - 1; num >= 0; num--)
			{
				if (elements[num] is IniFileValue)
				{
					return (IniFileValue)elements[num];
				}
			}
			return null;
		}

		internal IniFileValue firstValue()
		{
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i] is IniFileValue)
				{
					return (IniFileValue)elements[i];
				}
			}
			return null;
		}

		public ReadOnlyCollection<string> GetKeys()
		{
			List<string> list = new List<string>(elements.Count);
			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i] is IniFileValue)
				{
					list.Add(((IniFileValue)elements[i]).Key);
				}
			}
			return new ReadOnlyCollection<string>(list);
		}

		public override string ToString()
		{
			return sectionStart.ToString() + " (" + elements.Count + " elements)";
		}

		public void Format(bool preserveIntendation)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				IniFileElement iniFileElement = elements[i];
				string intendation = iniFileElement.Intendation;
				iniFileElement.FormatDefault();
				if (preserveIntendation)
				{
					iniFileElement.Intendation = intendation;
				}
			}
		}
	}
}
