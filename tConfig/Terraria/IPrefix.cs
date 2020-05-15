namespace Terraria
{
	public interface IPrefix
	{
		void Apply(Item item);

		bool Check(Item item);

		void Apply(Player player);
	}
}
