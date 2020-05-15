using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class npcDefHandler : DefHandler
	{
		public NPC[] itemDef;

		public Dictionary<int, NPC> customItemDefs;

		public Dictionary<string, NPC> byName;

		public Dictionary<string, string> modName;

		public Dictionary<string, int> animationType;

		public DictionaryHandler<int, int> townNPCCount;

		public ArrayList townNPCList;

		public Dictionary<int, List<DropTable>> dropTables;

		public Dictionary<string, int> aiPretendType;

		public Dictionary<string, int> drawPretendType;

		public NPC this[int i]
		{
			get
			{
				if (i < itemDef.Length)
				{
					return itemDef[i];
				}
				customItemDefs.TryGetValue(i, out NPC value);
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

		public npcDefHandler(int size, int custom = 0)
		{
			drawPretendType = new Dictionary<string, int>(custom);
			itemDef = new NPC[size + custom];
			customItemDefs = new Dictionary<int, NPC>();
			byName = new Dictionary<string, NPC>(23 + custom);
			modName = new Dictionary<string, string>(custom);
			assemblyByName = new DictionaryHandler<string, Assembly>(custom);
			assemblyByType = new DictionaryHandler<int, Assembly>(custom);
			animationType = new Dictionary<string, int>(custom);
			aiPretendType = new Dictionary<string, int>(custom);
			townNPCCount = new DictionaryHandler<int, int>(custom);
			dropTables = new Dictionary<int, List<DropTable>>(custom);
			townNPCList = new ArrayList(custom);
			globalAssembly = new ArrayList();
			globalModname = new ArrayList();
		}

		public int GetSize()
		{
			return itemDef.Length + customItemDefs.Count;
		}
	}
}
