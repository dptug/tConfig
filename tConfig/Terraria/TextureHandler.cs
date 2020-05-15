using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
	public class TextureHandler
	{
		public Texture2D[] itemTexture;

		public DictionaryHandler<int, Texture2D> customItemTexture;

		public Texture2D this[int i]
		{
			get
			{
				if (i >= itemTexture.Length)
				{
					return customItemTexture[i];
				}
				if (i < 0)
				{
					return null;
				}
				return itemTexture[i];
			}
			set
			{
				if (i >= itemTexture.Length)
				{
					customItemTexture[i] = value;
				}
				else
				{
					itemTexture[i] = value;
				}
			}
		}

		public int customAmt
		{
			get
			{
				return customItemTexture.Count;
			}
			set
			{
			}
		}

		public TextureHandler(int size)
		{
			itemTexture = new Texture2D[size];
			customItemTexture = new DictionaryHandler<int, Texture2D>(20);
		}

		public void AddSpace(int amt)
		{
			Texture2D[] array = (Texture2D[])itemTexture.Clone();
			itemTexture = new Texture2D[array.Length + amt];
			array.CopyTo(itemTexture, 0);
		}

		public void SetArray()
		{
			Texture2D[] array = (Texture2D[])itemTexture.Clone();
			itemTexture = new Texture2D[array.Length + customItemTexture.Count + 1];
			array.CopyTo(itemTexture, 0);
			for (int i = array.Length; i <= array.Length + customItemTexture.Count; i++)
			{
				itemTexture[i] = customItemTexture[i];
			}
			customItemTexture = null;
		}
	}
}
