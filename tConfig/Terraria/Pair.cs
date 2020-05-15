namespace Terraria
{
	public struct Pair<T, R>
	{
		public T A;

		public R B;

		public Pair(T first, R second)
		{
			A = first;
			B = second;
		}
	}
	public class Pair
	{
		public readonly OnScreenInterfaceable osi;

		public readonly double layer;

		public Pair(OnScreenInterfaceable osi, double layer)
		{
			this.osi = osi;
			this.layer = layer;
		}
	}
}
