using System.Collections;

namespace Terraria
{
	public class JSONDict
	{
		private ArrayList array;

		public JSONDict()
		{
			array = new ArrayList();
		}

		public void Add(string name, object contents, bool addQuotes = true)
		{
			string text = contents.ToString();
			string fullName = contents.GetType().FullName;
			if (fullName == "System.String" && addQuotes)
			{
				text = '"' + contents.ToString() + '"';
			}
			else if (fullName == "System.Boolean")
			{
				text = ((!(bool)contents) ? "0" : "1");
			}
			else if (fullName.IndexOf("Single") > -1)
			{
				text = text.Replace(',', '.');
			}
			array.Add('"' + name + '"' + ":" + text);
		}

		public override string ToString()
		{
			string str = "{";
			for (int i = 0; i < array.Count; i++)
			{
				str += array[i].ToString();
				if (i < array.Count - 1)
				{
					str += ",";
				}
			}
			return str + "}";
		}
	}
}
