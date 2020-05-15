using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Terraria
{
	public class BuffDefHandler : DefHandler
	{
		public Dictionary<int, string> modName;

		public DictionaryHandler<string, int> ID;

		public BuffDefHandler(int custom = 0)
		{
			modName = new Dictionary<int, string>(custom);
			assemblyByName = new DictionaryHandler<string, Assembly>(custom);
			assemblyByType = new DictionaryHandler<int, Assembly>(custom);
			ID = new DictionaryHandler<string, int>(custom);
			globalAssembly = new ArrayList();
			globalModname = new ArrayList();
		}
	}
}
