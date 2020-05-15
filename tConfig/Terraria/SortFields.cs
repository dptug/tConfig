using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Terraria
{
	public class SortFields : IComparer<FieldInfo>
	{
		public int Compare(FieldInfo x, FieldInfo y)
		{
			return string.Compare(x.Name, y.Name, ignoreCase: false, CultureInfo.InvariantCulture);
		}
	}
}
