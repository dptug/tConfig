using System;
using System.Collections;

namespace Terraria
{
	public class ItemBuilder : Builder
	{
		private Item item;

		public ItemBuilder(Item item)
		{
			attrs = new ArrayList();
			this.item = item;
		}

		public ItemBuilder()
		{
			attrs = new ArrayList();
			item = new Item();
		}

		public override void BuildAttrs()
		{
			attrs.Add(new Attr("0.0", "setName", base.strReader, delegate
			{
				Config.armorSets[item.name] = r.ReadString();
			}));
			attrs.Add(new Attr("0.0", "projectile", base.strReader, delegate
			{
				string key3 = r.ReadString();
				item.shoot = Config.projDefs.byName[key3].type;
			}));
			attrs.Add(new Attr("0.0", "useAmmoName", base.strReader, delegate
			{
				string key2 = r.ReadString();
				if (Config.ammoDef.ContainsKey(key2))
				{
					item.useAmmo = Config.ammoDef[key2];
				}
				else
				{
					Config.ammoDef.Add(key2, 1000 + Config.ammoDef.Count);
					item.useAmmo = Config.ammoDef[key2];
				}
			}));
			attrs.Add(new Attr("0.0", "ammoName", base.strReader, delegate
			{
				string key = r.ReadString();
				if (Config.ammoDef.ContainsKey(key))
				{
					item.ammo = Config.ammoDef[key];
				}
				else
				{
					Config.ammoDef.Add(key, 1000 + Config.ammoDef.Count);
					item.ammo = Config.ammoDef[key];
				}
			}));
			attrs.Add(new Attr("-1", "Deprecated data", (string name) => true, delegate
			{
				if (!Config.CheckVersion(version, "0.24"))
				{
					r.ReadBoolean();
				}
			}));
			attrs.Add(new Attr("0.13", "pretendType", delegate
			{
				if (!string.IsNullOrEmpty(ini["Stats"]["pretendName"]))
				{
					w.Write(value: true);
					w.Write(Config.itemDefs.byName[ini["Stats"]["pretendName"]].type);
				}
				else if (!string.IsNullOrEmpty(ini["Stats"]["pretendType"]) && ini["Stats"]["pretendType"] != "-1")
				{
					w.Write(value: true);
					w.Write(Convert.ToInt32(ini["Stats"]["pretendType"]));
				}
				else
				{
					w.Write(value: false);
				}
				return true;
			}, delegate
			{
				Config.itemDefs.pretendType[item.name] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.16.7", "prefixType", base.intReader, delegate
			{
				Config.itemDefs.prefixType[item.name] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.17", "useSoundName", base.soundReader, delegate
			{
				item.useSound = SoundHandler.soundID[r.ReadString()];
			}));
			attrs.Add(new Attr("0.17.1", "drawHair", base.intReader, delegate
			{
				Config.drawHair[item.headSlot] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.17.4", "createTileName", strReaderCheck(tileExists), delegate
			{
				item.createTile = Config.tileDefs.ID[r.ReadString()];
			}));
			attrs.Add(new Attr("0.18.2", "createWallName", strReaderCheck(wallExists), delegate
			{
				item.createWall = Config.wallDefs.ID[r.ReadString()];
			}));
			attrs.Add(new Attr("0.20", "holdStyleName", strReaderCheck(holdStyleExists), delegate
			{
				item.holdStyle = Config.holdStyleDefs.ID[r.ReadString()];
			}));
			attrs.Add(new Attr("0.20.1", "useStyleName", strReaderCheck(useStyleExists), delegate
			{
				item.useStyle = Config.useStyleDefs.ID[r.ReadString()];
			}));
			attrs.Add(new Attr("0.21.9", "drawPretendType", base.intReader, delegate
			{
				Config.itemDefs.drawPretendType[item.name] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.23.8", "placeFrame", base.intReader, delegate
			{
				Config.itemDefs.placeFrame[item.name] = r.ReadInt32();
			}));
			attrs.Add(new Attr("0.29.1", "drawHairAlt", base.boolReader, delegate
			{
				Config.drawHairAlt[item.headSlot] = r.ReadBoolean();
			}, boolCheck: false));
			attrs.Add(new Attr("0.34.0", "displayName", base.strReader, delegate
			{
				item.displayName = r.ReadString();
			}));
		}
	}
}
