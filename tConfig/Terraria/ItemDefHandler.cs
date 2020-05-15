using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class ItemDefHandler<T> : DefHandler
	{
		public T[] itemDef;

		public Dictionary<int, T> customItemDefs;

		public Dictionary<string, T> byName;

		public Dictionary<string, string> modName;

		public Dictionary<string, int> pretendType;

		public Dictionary<string, int> prefixType;

		public Dictionary<int, string> playerLoad;

		public Dictionary<int, string> worldLoad;

		public DictionaryHandler<string, int> placeFrame;

		public Dictionary<string, int> drawPretendType;

		public T this[int i]
		{
			get
			{
				if (i < itemDef.Length)
				{
					return itemDef[i];
				}
				customItemDefs.TryGetValue(i, out T value);
				return value;
			}
			set
			{
				if (i < itemDef.Length)
				{
					itemDef[i] = value;
				}
				else
				{
					customItemDefs[i] = value;
				}
			}
		}

		public ItemDefHandler(int size, int custom = 0)
		{
			placeFrame = new DictionaryHandler<string, int>(custom);
			itemDef = new T[size + custom];
			customItemDefs = new Dictionary<int, T>();
			byName = new Dictionary<string, T>(23);
			modName = new Dictionary<string, string>(custom);
			assemblyByName = new DictionaryHandler<string, Assembly>(custom);
			assemblyByType = new DictionaryHandler<int, Assembly>(custom);
			pretendType = new Dictionary<string, int>(custom);
			prefixType = new Dictionary<string, int>(custom);
			globalAssembly = new ArrayList();
			globalModname = new ArrayList();
			playerLoad = new Dictionary<int, string>(custom);
			worldLoad = new Dictionary<int, string>(custom);
			drawPretendType = new Dictionary<string, int>(custom);
		}

		public int GetSize()
		{
			return itemDef.Length;
		}
	}
}
