namespace Terraria
{
	public class ArrayHandler<T>
	{
		public T[] array;

		public DictionaryHandler<int, T> customArray;

		public T this[int i]
		{
			get
			{
				if (i < array.Length)
				{
					return array[i];
				}
				return customArray[i];
			}
			set
			{
				if (i < array.Length)
				{
					array[i] = value;
				}
				else
				{
					customArray[i] = value;
				}
			}
		}

		public ArrayHandler(int size)
		{
			array = new T[size];
			customArray = new DictionaryHandler<int, T>();
		}

		public ArrayHandler(T[] array)
		{
			this.array = array;
			customArray = new DictionaryHandler<int, T>();
		}

		public bool ContainsKey(int val)
		{
			if (val >= array.Length)
			{
				return customArray.ContainsKey(val);
			}
			return true;
		}

		public int GetSize()
		{
			return array.Length + customArray.Count;
		}

		public bool ContainsValue(T val)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (val.Equals(array[i]))
				{
					return true;
				}
			}
			return customArray.ContainsValue(val);
		}

		public bool TryGetValue(int i, out T val2)
		{
			if (i < array.Length)
			{
				val2 = array[i];
				return true;
			}
			return customArray.TryGetValue(i, out val2);
		}
	}
}
