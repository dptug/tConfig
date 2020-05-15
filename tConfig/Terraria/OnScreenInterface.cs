using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Terraria
{
	public class OnScreenInterface
	{
		public const double LAYER_INTERFACE_SCREEN = -1000000.0;

		public const double LAYER_INTERFACE_WORLD = -500000.0;

		public const double LAYER_PLAYER = 0.0;

		public const double LAYER_TILE = 1000000.0;

		public static List<OnScreenInterfaceable> listOsi = new List<OnScreenInterfaceable>();

		public static List<List<double>> listOsiLayers = new List<List<double>>();

		public static List<Pair> depthSorted = new List<Pair>();

		public static void Setup()
		{
			listOsi.Clear();
			listOsiLayers.Clear();
			depthSorted.Clear();
			ItemSlotRender.Clear();
			Codable.RunGlobalMethod("ModWorld", "OnSetup");
			Codable.RunGlobalMethod("ModWorld", "RegisterOnScreenInterfaces");
			List<Pair> list = new List<Pair>();
			for (int i = 0; i < listOsi.Count; i++)
			{
				for (int j = 0; j < listOsiLayers[i].Count; j++)
				{
					list.Add(new Pair(listOsi[i], listOsiLayers[i][j]));
				}
			}
			while (list.Count > 0)
			{
				int num = -1;
				for (int k = 0; k < list.Count; k++)
				{
					Pair pair = list[k];
					if (num == -1 || pair.layer > list[num].layer)
					{
						num = k;
					}
				}
				depthSorted.Add(list[num]);
				list.RemoveAt(num);
			}
		}

		public static void Register(OnScreenInterfaceable osi, params double[] layers)
		{
			listOsi.Add(osi);
			listOsiLayers.Add(new List<double>(layers));
		}

		public static void DrawOnScreen(SpriteBatch sb)
		{
			foreach (Pair item in depthSorted)
			{
				item.osi.DrawOnScreen(sb, item.layer);
			}
		}
	}
}
