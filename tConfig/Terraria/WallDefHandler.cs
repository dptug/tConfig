using System.Collections.Generic;

namespace Terraria
{
	public class WallDefHandler
	{
		public Dictionary<string, string> modName;

		public Dictionary<string, int> ID;

		public DictionaryHandler<int, string> dropName;

		public DictionaryHandler<int, string> load;

		public DictionaryHandler<int, string> name;

		public Dictionary<int, string> loadModname;

		public Dictionary<int, int[]> color;

		public WallDefHandler(int size)
		{
			color = new Dictionary<int, int[]>();
			loadModname = new Dictionary<int, string>();
			load = new DictionaryHandler<int, string>();
			ID = new Dictionary<string, int>();
			modName = new Dictionary<string, string>();
			dropName = new DictionaryHandler<int, string>(size);
			name = new DictionaryHandler<int, string>();
		}
	}
}
