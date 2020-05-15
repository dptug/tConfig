using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class ProjDefHandler<T> : DefHandler
	{
		public T[] itemDef;

		public Dictionary<int, T> customItemDefs;

		public Dictionary<string, T> byName;

		public Dictionary<string, string> modName;

		public Dictionary<string, int> pretendType;

		public Dictionary<string, int> aiPretendType;

		public Dictionary<string, int> killPretendType;

		public Dictionary<int, int> drawPretendType;

		public Dictionary<int, int> frameOffsetX;

		public Dictionary<int, int> frameOffsetY;

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

		public ProjDefHandler(int size, int custom = 0)
		{
			drawPretendType = new Dictionary<int, int>(custom);
			frameOffsetX = new Dictionary<int, int>(custom);
			frameOffsetY = new Dictionary<int, int>(custom);
			itemDef = new T[size + custom];
			customItemDefs = new Dictionary<int, T>();
			byName = new Dictionary<string, T>(23 + custom);
			modName = new Dictionary<string, string>(custom);
			assemblyByName = new DictionaryHandler<string, Assembly>(custom);
			assemblyByType = new DictionaryHandler<int, Assembly>(custom);
			pretendType = new Dictionary<string, int>(custom);
			aiPretendType = new Dictionary<string, int>(custom);
			killPretendType = new Dictionary<string, int>(custom);
			globalAssembly = new ArrayList();
			globalModname = new ArrayList();
		}
	}
}
