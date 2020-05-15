namespace Terraria
{
	public class Attr : AttrInterface
	{
		public delegate bool IniReader(string name);

		public delegate void ObjReader();

		private string name;

		public string version;

		public bool boolCheck;

		public IniReader inireader;

		public ObjReader objreader;

		public Attr(string version, string name, IniReader inireader, ObjReader objreader, bool boolCheck = true)
		{
			this.name = name;
			this.version = version;
			this.inireader = inireader;
			this.objreader = objreader;
			this.boolCheck = boolCheck;
		}

		public bool ReadINI()
		{
			return inireader(name);
		}

		public void ReadObj()
		{
			objreader();
		}
	}
}
