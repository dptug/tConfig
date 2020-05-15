using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace Terraria
{
	public class TileBuilder : Builder
	{
		private Config.TileDefaults item;

		public TileBuilder(Config.TileDefaults item)
		{
			attrs = new ArrayList();
			this.item = item;
		}

		public TileBuilder()
		{
			attrs = new ArrayList();
			item = new Config.TileDefaults();
		}

		public override void BuildAttrs()
		{
			attrs.Add(new Attr("-1", "Deprecated data", (string name) => true, delegate
			{
				if (!Config.CheckVersion(version, "0.24"))
				{
					r.ReadBoolean();
				}
			}));
			attrs.Add(new Attr("0.18.1", "mechToggle", strReaderCheck(tileExists), delegate
			{
				string value2 = r.ReadString();
				Config.tileDefs.mechToggle[item.id] = value2;
			}));
			attrs.Add(new Attr("0.18.2", "Drops", delegate
			{
				if (ini["Drops"].GetKeys().Count > 0)
				{
					w.Write(value: true);
					DropTable dropTable = new DropTable(ini["Drops"]);
					foreach (Drop drop in dropTable.drops)
					{
						if (!itemExists(drop.name))
						{
							Console.WriteLine("Drop item " + drop.name + " does not exist!");
							return false;
						}
					}
					dropTable.Save(w);
				}
				else
				{
					w.Write(value: false);
				}
				return true;
			}, delegate
			{
				Config.tileDefs.dropTables[item.id] = new DropTable(r, version);
			}));
			attrs.Add(new Attr("0.18.5", "special", base.boolReader, delegate
			{
				if (r.ReadBoolean())
				{
					Config.tileDefs.special[item.id] = true;
				}
			}, boolCheck: false));
			attrs.Add(new Attr("0.20.6", "spawn", base.boolReader, delegate
			{
				if (r.ReadBoolean())
				{
					Config.tileDefs.spawnPoint[item.id] = true;
				}
			}, boolCheck: false));
			attrs.Add(new Attr("0.23", "hitSoundName", delegate
			{
				if (!string.IsNullOrEmpty(ini["Stats"]["hitSoundName"]))
				{
					string text = Path.Combine(modpackFolder, "Sound", ini["Stats"]["hitSoundName"] + ".wav");
					if (!File.Exists(text))
					{
						Console.WriteLine("Sound file " + text + " does not exist!");
						return false;
					}
					w.Write(value: true);
					w.Write(ini["Stats"]["hitSoundName"]);
					w.Write(value: false);
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["hitSound"]))
				{
					w.Write(value: false);
					w.Write(value: true);
					w.Write(Convert.ToInt32(ini["Stats"]["hitSound"]));
				}
				else
				{
					w.Write(value: false);
					w.Write(value: false);
				}
				return true;
			}, delegate
			{
				if (r.ReadBoolean())
				{
					Config.tileDefs.hitSound[item.id] = SoundHandler.soundID[r.ReadString()];
				}
				if (r.ReadBoolean())
				{
					Config.tileDefs.hitSound[item.id] = r.ReadInt32();
				}
			}, boolCheck: false));
			attrs.Add(new Attr("0.23.2", "hitSoundList", base.intReader, delegate
			{
				Config.tileDefs.hitSoundList[item.id] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.23.4", "doorType", delegate
			{
				int num = 0;
				if (!string.IsNullOrEmpty(ini["Stats"]["doorType"]))
				{
					if (ini["Stats"]["doorType"] == "closed" || ini["Stats"]["doorType"] == "close")
					{
						num = 1;
					}
					if (ini["Stats"]["doorType"] == "opened" || ini["Stats"]["doorType"] == "open")
					{
						num = 2;
					}
				}
				w.Write((byte)num);
				if (num > 0)
				{
					if (!string.IsNullOrEmpty(ini["Stats"]["doorToggle"]))
					{
						w.Write(value: true);
						w.Write(ini["Stats"]["doorToggle"]);
					}
					else
					{
						w.Write(value: false);
					}
				}
				return true;
			}, delegate
			{
				if (Config.CheckVersion(version, "0.23.4"))
				{
					Config.tileDefs.doorType[item.id] = r.ReadByte();
					if (Config.tileDefs.doorType[item.id] > 0 && r.ReadBoolean())
					{
						Config.tileDefs.doorToggle[item.id] = r.ReadString();
					}
				}
			}, boolCheck: false));
			attrs.Add(new Attr("0.23.4", "directional", base.boolReader, delegate
			{
				Config.tileDefs.directional[item.id] = r.ReadBoolean();
			}, boolCheck: false));
			attrs.Add(new Attr("0.23.7", "placeOn", base.strReader, delegate
			{
				Config.tileDefs.placeOn[item.id] = r.ReadString() + " ";
			}));
			attrs.Add(new Attr("0.27.1", "light", delegate
			{
				if (!string.IsNullOrEmpty(ini["Stats"]["light"]))
				{
					w.Write(value: true);
					string[] array = ini["Stats"]["light"].Split(',');
					if (array.Length != 3)
					{
						Console.WriteLine("light parameter is invalid");
						return false;
					}
					string[] array2 = array;
					foreach (string value in array2)
					{
						w.Write(Convert.ToSingle(value, CultureInfo.InvariantCulture.NumberFormat));
					}
				}
				else
				{
					w.Write(value: false);
				}
				return true;
			}, delegate
			{
				Config.tileDefs.light[item.id] = new float[3]
				{
					r.ReadSingle(),
					r.ReadSingle(),
					r.ReadSingle()
				};
			}));
			attrs.Add(new Attr("0.27.3", "treasure", base.boolReader, delegate
			{
				Config.tileDefs.treasure[item.id] = r.ReadBoolean();
			}, boolCheck: false));
			attrs.Add(new Attr("0.27.3", "dustType", base.intReader, delegate
			{
				Config.tileDefs.dustType[item.id] = r.ReadInt32();
			}));
		}
	}
}
