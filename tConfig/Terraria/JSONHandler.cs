using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Terraria
{
	public class JSONHandler
	{
		public static Dictionary<string, object> LoadDict(string text)
		{
			Regex.Replace(text, "\\s", "");
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Deserialize<Dictionary<string, object>>(text);
		}

		public static object[] LoadArray(string thetext)
		{
			ArrayList arrayList = new ArrayList();
			string[] array = thetext.Split(',');
			foreach (string text in array)
			{
				object obj = null;
				int index = 0;
				obj = ((text[index] != '[') ? ((text[index] != '{') ? ((IEnumerable)text.Trim('"')) : ((IEnumerable)LoadDict(text.Substring(1, text.Length - 1)))) : LoadArray(text.Substring(1, text.Length - 1)));
				arrayList.Add(obj);
			}
			return arrayList.ToArray();
		}
	}
}
