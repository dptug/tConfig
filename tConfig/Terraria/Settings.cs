using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria
{
	public class Settings
	{
		private static readonly Vector2[] shadowOffset = new Vector2[4]
		{
			new Vector2(-1f, -1f),
			new Vector2(1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, 1f)
		};

		public static string current = null;

		public static JsonData currentSetting = null;

		public static string currentInput = null;

		public static bool Initialized(string modpack = null)
		{
			if (modpack == null)
			{
				modpack = TMod.GetCurrentMod();
			}
			return Config.jsonCurrent[modpack] != null;
		}

		public static JsonData GetSetting(string setting, string modpack = null)
		{
			if (modpack == null)
			{
				modpack = TMod.GetCurrentMod();
			}
			if (Config.jsonCurrent[modpack] == null)
			{
				return null;
			}
			return Config.jsonCurrent[modpack]["settings"][setting];
		}

		public static JsonData Get(string setting, string modpack = null)
		{
			return GetSetting(setting, modpack)?["value"];
		}

		public static bool GetBool(string setting, string modpack = null)
		{
			return (bool)Get(setting, modpack);
		}

		public static int GetInt(string setting, string modpack = null)
		{
			return (int)Get(setting, modpack);
		}

		public static string GetChoice(string setting, string modpack = null)
		{
			return GetString(setting, modpack);
		}

		public static int GetChoiceIndex(string setting, string modpack = null)
		{
			JsonData setting2 = GetSetting(setting, modpack);
			if (setting2 == null)
			{
				return -1;
			}
			string a = (string)setting2["value"];
			for (int i = 0; i < setting2["choices"].Count; i++)
			{
				if (a == (string)setting2["choices"][i]["value"])
				{
					return i;
				}
			}
			return -1;
		}

		public static string GetString(string setting, string modpack = null)
		{
			return (string)Get(setting, modpack);
		}

		public static void draw(int x, int y, int w, float scale, string setting, string modpack = null)
		{
			draw(x, y, w, scale, GetSetting(setting, modpack));
		}

		public static void draw(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			DrawStringShadowed(spriteBatch, Main.fontDeathText, (string)setting["display"], new Vector2(x, y), color, colorShadow, default(Vector2), scale);
			switch ((string)setting["type"])
			{
			case "bool":
				drawSettingBool(x, y, w, scale, setting);
				break;
			case "int":
				drawSettingInt(x, y, w, scale, setting);
				break;
			case "choice":
				drawSettingChoice(x, y, w, scale, setting);
				break;
			case "string":
				drawSettingString(x, y, w, scale, setting);
				break;
			case "keybind":
				drawSettingKeybind(x, y, w, scale, setting);
				break;
			}
		}

		public static bool drawBackButton(int x, int y, int w, float scale)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			DrawStringShadowed(spriteBatch, Main.fontDeathText, "Back", new Vector2((float)(x + w / 2) - Main.fontDeathText.MeasureString("Back").X / 2f * scale, y), color, colorShadow, default(Vector2), scale);
			if (flag && Main.mouseLeft)
			{
				return Main.mouseLeftRelease;
			}
			return false;
		}

		private static void drawSettingBool(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			string text = (string)(((bool)setting["value"]) ? setting["displayTrue"] : setting["displayFalse"]);
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2((float)(x + w) - Main.fontDeathText.MeasureString(text).X * scale, y), color, colorShadow, default(Vector2), scale);
			if (flag && Main.mouseLeft && Main.mouseLeftRelease)
			{
				setting["value"] = !(bool)setting["value"];
				Main.PlaySound(12);
			}
		}

		private static void drawSettingInt(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			float num = x + w;
			string text = ">";
			Vector2 vector = Main.fontDeathText.MeasureString(text);
			num -= vector.X * scale;
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2(num, y), color, colorShadow, default(Vector2), scale);
			if (MouseRegion((int)num, y, (int)(vector.X * scale), (int)(60f * scale)) && Main.mouseLeft && Main.mouseLeftRelease)
			{
				setting["value"] = (int)setting["rangeMax"];
				Main.PlaySound(12);
			}
			num -= 24f * scale;
			text = "+";
			vector = Main.fontDeathText.MeasureString(text);
			num -= vector.X * scale;
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2(num, y), color, colorShadow, default(Vector2), scale);
			if (MouseRegion((int)num, y, (int)(vector.X * scale), (int)(60f * scale)))
			{
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					if ((int)setting["value"] != (int)setting["rangeMax"])
					{
						setting["value"] = (int)setting["value"] + 1;
					}
					Main.PlaySound(12);
				}
				if (Main.mouseRight && Main.mouseRightRelease)
				{
					int val = (int)((double)(int)setting["value"] + Math.Ceiling((float)Math.Abs((int)setting["rangeMax"] - (int)setting["rangeMin"]) / 10f));
					setting["value"] = Math.Min(Math.Max(val, (int)setting["rangeMin"]), (int)setting["rangeMax"]);
					Main.PlaySound(12);
				}
			}
			num -= 48f * scale;
			text = string.Concat((int)setting["value"]);
			vector = Main.fontDeathText.MeasureString(string.Concat((int)setting["rangeMin"]));
			Vector2 vector2 = Main.fontDeathText.MeasureString(string.Concat((int)setting["rangeMax"]));
			num -= Math.Max(vector.X, vector2.X) * scale;
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2(num, y), color, colorShadow, default(Vector2), scale);
			if (MouseRegion((int)num, y, (int)(Math.Max(vector.X, vector2.X) * scale), (int)(60f * scale)) && Main.mouseLeft && Main.mouseLeftRelease)
			{
				currentSetting = setting;
				currentInput = null;
				Main.menuMode = 1502;
				Main.PlaySound(10);
			}
			num -= 48f * scale;
			text = "-";
			vector = Main.fontDeathText.MeasureString(text);
			num -= vector.X * scale;
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2(num, y), color, colorShadow, default(Vector2), scale);
			if (MouseRegion((int)num, y, (int)(vector.X * scale), (int)(60f * scale)))
			{
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					if ((int)setting["value"] != (int)setting["rangeMin"])
					{
						setting["value"] = (int)setting["value"] - 1;
					}
					Main.PlaySound(12);
				}
				if (Main.mouseRight && Main.mouseRightRelease)
				{
					int val2 = (int)((double)(int)setting["value"] - Math.Ceiling((float)Math.Abs((int)setting["rangeMax"] - (int)setting["rangeMin"]) / 10f));
					setting["value"] = Math.Min(Math.Max(val2, (int)setting["rangeMin"]), (int)setting["rangeMax"]);
					Main.PlaySound(12);
				}
			}
			num -= 24f * scale;
			text = "<";
			vector = Main.fontDeathText.MeasureString(text);
			num -= vector.X * scale;
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2(num, y), color, colorShadow, default(Vector2), scale);
			if (MouseRegion((int)num, y, (int)(vector.X * scale), (int)(60f * scale)) && Main.mouseLeft && Main.mouseLeftRelease)
			{
				setting["value"] = (int)setting["rangeMin"];
				Main.PlaySound(12);
			}
			num -= 24f * scale;
		}

		private static void drawSettingChoice(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			string text = "";
			string b = (string)setting["value"];
			string data = "";
			string data2 = "";
			for (int i = 0; i < setting["choices"].Count; i++)
			{
				if ((string)setting["choices"][i]["value"] == b)
				{
					text = (string)setting["choices"][i]["display"];
					data = (string)setting["choices"][(i == 0) ? (setting["choices"].Count - 1) : (i - 1)]["value"];
					data2 = (string)setting["choices"][(i != setting["choices"].Count - 1) ? (i + 1) : 0]["value"];
					break;
				}
			}
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2((float)(x + w) - Main.fontDeathText.MeasureString(text).X * scale, y), color, colorShadow, default(Vector2), scale);
			if (flag)
			{
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					setting["value"] = data2;
					Main.PlaySound(12);
				}
				if (Main.mouseRight && Main.mouseRightRelease)
				{
					setting["value"] = data;
					Main.PlaySound(12);
				}
			}
		}

		private static void drawSettingString(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			string text = (string)setting["value"];
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2((float)(x + w) - Main.fontDeathText.MeasureString(text).X * scale, y), color, colorShadow, default(Vector2), scale);
			if (flag && Main.mouseLeft && Main.mouseLeftRelease)
			{
				currentSetting = setting;
				currentInput = null;
				Main.menuMode = 1503;
				Main.PlaySound(10);
			}
		}

		private static void drawSettingKeybind(int x, int y, int w, float scale, JsonData setting)
		{
			SpriteBatch spriteBatch = Config.mainInstance.spriteBatch;
			bool flag = MouseOnSetting(x, y, w, scale);
			Color color = new Color(1f, 1f, 1f, flag ? 1f : 0.5f);
			Color colorShadow = new Color(0f, 0f, 0f, flag ? 1f : 0.5f);
			string text = (string)setting["value"];
			DrawStringShadowed(spriteBatch, Main.fontDeathText, text, new Vector2((float)(x + w) - Main.fontDeathText.MeasureString(text).X * scale, y), color, colorShadow, default(Vector2), scale);
			if (flag && Main.mouseLeft && Main.mouseLeftRelease)
			{
				currentSetting = setting;
				currentInput = null;
				Main.menuMode = 1504;
				Main.PlaySound(10);
			}
		}

		private static bool MouseOnSetting(int x, int y, int w, float scale)
		{
			return MouseRegion(x, y, w, (int)(60f * scale));
		}

		private static bool MouseRegion(int x, int y, int w, int h)
		{
			if (Main.mouseX >= x && Main.mouseY >= y && Main.mouseX < x + w)
			{
				return Main.mouseY < y + h;
			}
			return false;
		}

		private static void DrawStringShadowed(SpriteBatch sb, SpriteFont font, string text, Vector2 pos, Color color, Color colorShadow, Vector2 vec = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None)
		{
			Vector2[] array = shadowOffset;
			for (int i = 0; i < array.Length; i++)
			{
				Vector2 vector = array[i];
				sb.DrawString(font, text, new Vector2(pos.X + vector.X, pos.Y + vector.Y), colorShadow, 0f, vec, scale, effects, 0f);
			}
			sb.DrawString(font, text, pos, color, 0f, vec, scale, effects, 0f);
		}
	}
}
