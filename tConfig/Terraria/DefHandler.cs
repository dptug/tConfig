using System.Collections;
using System.Reflection;

namespace Terraria
{
	public class DefHandler
	{
		public DictionaryHandler<string, Assembly> assemblyByName;

		public DictionaryHandler<int, Assembly> assemblyByType;

		public ArrayList globalAssembly;

		public ArrayList globalModname;
	}
}
