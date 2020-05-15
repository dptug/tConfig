using System.Collections;

namespace Terraria
{
	public class JSONArray
	{
		private ArrayList array;

		public JSONArray()
		{
			array = new ArrayList();
		}

		public JSONArray(params object[] arr)
		{
			array = new ArrayList();
			for (int i = 0; i < arr.Length; i++)
			{
				Add(arr[i]);
			}
		}

		public void Add(object arr, bool addQuotes = true)
		{
			string fullName = arr.GetType().FullName;
			if (fullName == "System.String" && addQuotes)
			{
				array.Add('"' + arr.ToString() + '"');
			}
			else if (fullName == "System.Boolean")
			{
				if ((bool)arr)
				{
					array.Add("1");
				}
				else
				{
					array.Add("0");
				}
			}
			else if (fullName.IndexOf("Single") > -1)
			{
				string value = arr.ToString().Replace(',', '.');
				array.Add(value);
			}
			else
			{
				array.Add(arr);
			}
		}

		public override string ToString()
		{
			string str = "[";
			for (int i = 0; i < array.Count; i++)
			{
				str += array[i].ToString();
				if (i < array.Count - 1)
				{
					str += ",";
				}
			}
			return str + "]";
		}
	}
}
