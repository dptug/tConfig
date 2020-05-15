using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Terraria
{
	public class ItemSlotRender
	{
		private static List<IItemSlotRender> list = new List<IItemSlotRender>();

		public static void Clear()
		{
			list.Clear();
		}

		public static void Register(IItemSlotRender iisr)
		{
			list.Add(iisr);
		}

		public static void DrawItemInSlot(SpriteBatch sb, Item item, Vector2 pos)
		{
			DrawItemInSlot(sb, item, pos, Main.inventoryScale);
		}

		public static void DrawItemInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos)
		{
			DrawItemInSlot(sb, Color.White, item, pos, Main.inventoryScale);
		}

		public static void DrawItemInSlot(SpriteBatch sb, Item item, Vector2 pos, float scale)
		{
			DrawItemInSlot(sb, Color.White, item, pos, scale);
		}

		public static void DrawItemInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos, float scale)
		{
			if (item.type > 0 && item.stack > 0)
			{
				bool letDraw = true;
				foreach (IItemSlotRender item2 in list)
				{
					item2.PreDrawItemInSlot(sb, color, item, pos, scale, ref letDraw);
				}
				if (letDraw)
				{
					VanillaDrawItemInSlot(sb, color, item, pos, scale);
				}
				foreach (IItemSlotRender item3 in list)
				{
					item3.PostDrawItemInSlot(sb, color, item, pos, scale, letDraw);
				}
			}
		}

		public static void VanillaDrawItemInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos, float sc)
		{
			if (item.type > 0 && item.stack > 0)
			{
				Texture2D texture2D = Main.itemTexture[item.type];
				float num = 1f;
				if (texture2D.Width > 32 || texture2D.Height > 32)
				{
					num = 32f / (float)((texture2D.Width > texture2D.Height) ? texture2D.Width : texture2D.Height);
				}
				num *= sc;
				VanillaDrawItemTextureInSlot(sb, color, item, new Vector2(pos.X + 26f * sc - (float)texture2D.Width / 2f * num, pos.Y + 26f * sc - (float)texture2D.Height / 2f * num), num);
				if (item.stack > 1)
				{
					sb.DrawString(Main.fontItemStack, string.Concat(item.stack), pos + new Vector2(sc * 10f, sc * 26f), color, 0f, default(Vector2), num, SpriteEffects.None, 0f);
				}
			}
		}

		public static void VanillaDrawItemTextureInSlot(SpriteBatch sb, Color color, Item item, Vector2 pos, float scale)
		{
			if (item.type > 0 && item.stack > 0)
			{
				Texture2D texture2D = Main.itemTexture[item.type];
				Rectangle? sourceRectangle = new Rectangle(0, 0, texture2D.Width, texture2D.Height);
				sb.Draw(texture2D, pos, sourceRectangle, item.GetAlpha(color), 0f, default(Vector2), scale, SpriteEffects.None, 0f);
				if (item.color != default(Color))
				{
					sb.Draw(texture2D, pos, sourceRectangle, item.GetColor(color), 0f, default(Vector2), scale, SpriteEffects.None, 0f);
				}
			}
		}
	}
}
