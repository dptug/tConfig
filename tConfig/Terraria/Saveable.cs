using System.IO;

namespace Terraria
{
	public interface Saveable
	{
		void Save(BinaryWriter writer);

		void Load(BinaryReader reader, int version);
	}
}
