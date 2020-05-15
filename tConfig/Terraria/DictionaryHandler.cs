using System.Collections.Generic;

namespace Terraria
{
	public class DictionaryHandler<T, T2>
	{
		public Dictionary<T, T2> dict;

		public T2 this[T key]
		{
			get
			{
				if (key == null)
				{
					return default(T2);
				}
				if (dict.TryGetValue(key, out T2 value))
				{
					return value;
				}
				return default(T2);
			}
			set
			{
				dict[key] = value;
			}
		}

		public int Count
		{
			get
			{
				return dict.Count;
			}
			set
			{
			}
		}

		public ICollection<T> Keys => dict.Keys;

		public DictionaryHandler(int size)
		{
			dict = new Dictionary<T, T2>(size);
		}

		public DictionaryHandler()
		{
			dict = new Dictionary<T, T2>();
		}

		public bool ContainsValue(T2 val)
		{
			return dict.ContainsValue(val);
		}

		public bool ContainsKey(T val)
		{
			return dict.ContainsKey(val);
		}

		public bool TryGetValue(T val, out T2 val2)
		{
			return dict.TryGetValue(val, out val2);
		}
	}
}
