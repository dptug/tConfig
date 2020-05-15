using System;
using System.Collections.Generic;

namespace Terraria
{
	public class WeightedRandom<T>
	{
		public List<Pair<T, double>> list = new List<Pair<T, double>>();

		public readonly Random rnd;

		public WeightedRandom()
		{
			rnd = new Random();
		}

		public WeightedRandom(int seed)
		{
			rnd = new Random(seed);
		}

		public WeightedRandom(Random rnd)
		{
			this.rnd = rnd;
		}

		public void Add(T element, double weight)
		{
			if (!(weight <= 0.0))
			{
				list.Add(new Pair<T, double>(element, weight));
			}
		}

		public T Get()
		{
			double num = rnd.NextDouble();
			double wholeWeight = GetWholeWeight();
			num *= wholeWeight;
			foreach (Pair<T, double> item in list)
			{
				if (!(num > item.B))
				{
					return item.A;
				}
				num -= item.B;
			}
			return default(T);
		}

		public void Clear()
		{
			list.Clear();
		}

		public double GetWholeWeight()
		{
			double num = 0.0;
			foreach (Pair<T, double> item in list)
			{
				num += item.B;
			}
			return num;
		}
	}
}
