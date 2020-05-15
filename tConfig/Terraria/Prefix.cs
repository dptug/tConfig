using Gajatko.IniFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Terraria
{
	public class Prefix : IPrefix
	{
		public delegate bool Requirement(Item item);

		public delegate void ItemMod(Item item);

		public delegate void PlayerMod(Player player);

		public class ItemVals<T>
		{
			public T damage;

			public T scale;

			public T speed;

			public T mana;

			public T knockback;

			public T shootSpeed;

			public T crit;

			public T defense;

			public bool melee;

			public bool accessory;

			public bool ranged;

			public bool magic;

			public bool armor;

			public bool legArmor;

			public bool bodyArmor;

			public bool headArmor;

			public bool notVanity;
		}

		public class PlayerVals<T>
		{
			public T defense;

			public T crit;

			public T mana;

			public T damage;

			public T moveSpeed;

			public T meleeSpeed;

			public T meleeDamage;

			public T rangedDamage;

			public T magicDamage;

			public T meleeCrit;

			public T rangedCrit;

			public T magicCrit;
		}

		public static List<Prefix> prefixes;

		public static Dictionary<string, int> ID;

		public static Dictionary<int, string> playerLoad;

		public static Dictionary<int, string> worldLoad;

		public static int saveVersion = 0;

		public string affix;

		public bool suffix;

		public string identifier;

		public IPrefix code;

		public string modname = "";

		public double weight = 1.0;

		public List<MouseTip> toolTips;

		public ItemVals<int> add;

		public ItemVals<float> multiply;

		public ItemVals<float> requirements;

		public PlayerVals<float> pAdd;

		public List<Requirement> customRequirements;

		public List<ItemMod> itemMods;

		public List<PlayerMod> playerMods;

		public static void LoadPrefixes(string modpack, BinaryReader r, string modversion)
		{
			if (!Config.CheckVersion(modversion, "0.27.4"))
			{
				return;
			}
			int version = r.ReadInt32();
			int num = r.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				Prefix prefix = new Prefix(r, version, modversion);
				prefix.modname = modpack;
				prefixes.Add(prefix);
				ID[modpack + ":" + prefix.identifier] = prefixes.Count - 1;
			}
			if (Config.codeExists)
			{
				Assembly assembly = Config.modDLL[modpack];
				IPrefixDefiner prefixDefiner = (IPrefixDefiner)assembly.CreateInstance(Config.namespaces[assembly.FullName] + ".Global_Prefix");
				if (prefixDefiner != null)
				{
					foreach (Prefix item in prefixDefiner.DefinePrefixes())
					{
						item.modname = modpack;
						prefixes.Add(item);
						ID[modpack + ":" + item.identifier] = prefixes.Count - 1;
					}
				}
			}
		}

		public static void LoadPrefixNames(string type, MemoryStream stream)
		{
			Dictionary<int, string> dictionary;
			if (type == "player")
			{
				playerLoad = new Dictionary<int, string>();
				dictionary = playerLoad;
			}
			else
			{
				if (!(type == "world"))
				{
					return;
				}
				worldLoad = new Dictionary<int, string>();
				dictionary = worldLoad;
			}
			try
			{
				IniFileReader reader = new IniFileReader(stream);
				IniFile iniFile = IniFile.FromStream(reader);
				ReadOnlyCollection<string> keys = iniFile["Prefixes"].GetKeys();
				foreach (string item in keys)
				{
					int key = Convert.ToInt32(item);
					string text2 = dictionary[key] = iniFile["Prefixes"][item];
				}
			}
			catch (Exception)
			{
			}
		}

		public static void SavePrefixNames(MemoryStream stream)
		{
			IniFileWriter iniFileWriter = new IniFileWriter(stream);
			IniFile iniFile = new IniFile();
			for (int i = 0; i < prefixes.Count; i++)
			{
				int num = i;
				iniFile["Prefixes"][num.ToString()] = prefixes[i].modname + ":" + prefixes[i].identifier;
			}
			iniFileWriter.WriteIniFile(iniFile);
			iniFileWriter.Flush();
		}

		public static void SavePrefix(BinaryWriter w, Item item)
		{
			w.Write(item.prefix);
			if (item.prefix == ID[":Mysterious"])
			{
				w.Write(item.unloadedPrefix);
			}
		}

		public static void LoadPrefix(BinaryReader r, Item item, string type)
		{
			Dictionary<int, string> dictionary;
			if (type == "player")
			{
				dictionary = playerLoad;
			}
			else
			{
				if (!(type == "world"))
				{
					return;
				}
				dictionary = worldLoad;
			}
			int num = r.ReadByte();
			if (num == 0)
			{
				item.Prefix(0);
				return;
			}
			if (num > ID[":Mysterious"])
			{
				if (!dictionary.ContainsKey(num))
				{
					num = 0;
					item.Prefix(0);
					return;
				}
				string text = dictionary[num];
				try
				{
					num = ID[text];
				}
				catch (Exception)
				{
					item.Prefix((byte)ID[":Mysterious"]);
					item.unloadedPrefix = text;
					return;
				}
			}
			if (num == ID[":Mysterious"])
			{
				string text2 = r.ReadString();
				try
				{
					num = ID[text2];
					item.Prefix(num);
				}
				catch (Exception)
				{
					item.unloadedPrefix = text2;
					item.Prefix((byte)ID[":Mysterious"]);
				}
			}
			else
			{
				item.Prefix(num);
			}
		}

		public void Initialize()
		{
			add = new ItemVals<int>();
			multiply = new ItemVals<float>();
			multiply.defense = 1f;
			multiply.crit = 1f;
			multiply.mana = 1f;
			multiply.damage = 1f;
			multiply.scale = 1f;
			multiply.knockback = 1f;
			multiply.shootSpeed = 1f;
			multiply.speed = 1f;
			requirements = new ItemVals<float>();
			pAdd = new PlayerVals<float>();
			customRequirements = new List<Requirement>();
			itemMods = new List<ItemMod>();
			playerMods = new List<PlayerMod>();
			toolTips = new List<MouseTip>();
		}

		public Prefix(string name, bool suffix = false, IPrefix code = null)
		{
			affix = name;
			this.suffix = suffix;
			this.code = code;
			identifier = name;
			Initialize();
		}

		public Prefix(string name, string id, bool suffix = false)
		{
			affix = name;
			this.suffix = suffix;
			identifier = id;
			Initialize();
		}

		public Prefix(BinaryReader r, int version, string modversion)
		{
			Initialize();
			Load(r, version, modversion);
		}

		public Prefix(IniFile ini, string identifier)
		{
			this.identifier = identifier;
			if (string.IsNullOrEmpty(ini["Stats"]["name"]))
			{
				throw new Exception("Name required for prefix!");
			}
			affix = ini["Stats"]["name"];
			if (!string.IsNullOrEmpty(ini["Stats"]["suffix"]))
			{
				suffix = Convert.ToBoolean(ini["Stats"]["suffix"]);
			}
			if (!string.IsNullOrEmpty(ini["Stats"]["weight"]))
			{
				weight = Convert.ToDouble(ini["Stats"]["weight"]);
			}
			add = new ItemVals<int>();
			multiply = new ItemVals<float>();
			multiply.defense = 1f;
			multiply.crit = 1f;
			multiply.mana = 1f;
			multiply.damage = 1f;
			multiply.scale = 1f;
			multiply.knockback = 1f;
			multiply.shootSpeed = 1f;
			multiply.speed = 1f;
			if (!string.IsNullOrEmpty(ini["Item"]["manaCost"]))
			{
				multiply.mana = Convert.ToSingle(ini["Item"]["manaCost"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["damage"]))
			{
				multiply.damage = Convert.ToSingle(ini["Item"]["damage"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["scale"]))
			{
				multiply.scale = Convert.ToSingle(ini["Item"]["scale"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["knockback"]))
			{
				multiply.knockback = Convert.ToSingle(ini["Item"]["knockback"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["shootSpeed"]))
			{
				multiply.shootSpeed = Convert.ToSingle(ini["Item"]["shootSpeed"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["speed"]))
			{
				MultiplySpeedInverted(Convert.ToSingle(ini["Item"]["speed"], CultureInfo.InvariantCulture.NumberFormat));
			}
			requirements = new ItemVals<float>();
			pAdd = new PlayerVals<float>();
			if (!string.IsNullOrEmpty(ini["Item"]["defense"]))
			{
				add.defense = Convert.ToInt32(ini["Item"]["defense"]);
			}
			if (!string.IsNullOrEmpty(ini["Item"]["crit"]))
			{
				add.crit = Convert.ToInt32(ini["Item"]["crit"]);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["defense"]))
			{
				pAdd.defense = Convert.ToInt32(ini["Player"]["defense"]);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["crit"]))
			{
				pAdd.crit = Convert.ToInt32(ini["Player"]["crit"]);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["mana"]))
			{
				pAdd.mana = Convert.ToInt32(ini["Player"]["mana"]);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["damage"]))
			{
				pAdd.damage = Convert.ToSingle(ini["Player"]["damage"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["moveSpeed"]))
			{
				pAdd.moveSpeed = Convert.ToSingle(ini["Player"]["moveSpeed"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Player"]["meleeSpeed"]))
			{
				pAdd.meleeSpeed = Convert.ToSingle(ini["Player"]["meleeSpeed"], CultureInfo.InvariantCulture.NumberFormat);
			}
			if (!string.IsNullOrEmpty(ini["Requirements"]["melee"]))
			{
				requirements.melee = Convert.ToBoolean(ini["Requirements"]["melee"]);
			}
			if (!string.IsNullOrEmpty(ini["Requirements"]["magic"]))
			{
				requirements.magic = Convert.ToBoolean(ini["Requirements"]["magic"]);
			}
			if (!string.IsNullOrEmpty(ini["Requirements"]["ranged"]))
			{
				requirements.ranged = Convert.ToBoolean(ini["Requirements"]["ranged"]);
			}
			if (!string.IsNullOrEmpty(ini["Requirements"]["accessory"]))
			{
				requirements.accessory = Convert.ToBoolean(ini["Requirements"]["accessory"]);
			}
		}

		public virtual void Save(BinaryWriter writer)
		{
			writer.Write(identifier);
			writer.Write(affix);
			writer.Write(suffix);
			writer.Write(weight);
			writer.Write(multiply.defense);
			writer.Write(multiply.crit);
			writer.Write(multiply.mana);
			writer.Write(multiply.damage);
			writer.Write(multiply.scale);
			writer.Write(multiply.knockback);
			writer.Write(multiply.shootSpeed);
			writer.Write(multiply.speed);
			writer.Write(add.defense);
			writer.Write(add.crit);
			writer.Write(add.mana);
			writer.Write(add.damage);
			writer.Write(add.scale);
			writer.Write(add.knockback);
			writer.Write(add.shootSpeed);
			writer.Write(add.speed);
			writer.Write(pAdd.defense);
			writer.Write((int)pAdd.crit);
			writer.Write((int)pAdd.mana);
			writer.Write(pAdd.damage);
			writer.Write(pAdd.moveSpeed);
			writer.Write(pAdd.meleeSpeed);
			writer.Write(pAdd.meleeDamage);
			writer.Write(pAdd.rangedDamage);
			writer.Write(pAdd.magicDamage);
			writer.Write((int)pAdd.meleeCrit);
			writer.Write((int)pAdd.rangedCrit);
			writer.Write((int)pAdd.magicCrit);
			writer.Write(requirements.accessory);
			writer.Write(requirements.melee);
			writer.Write(requirements.ranged);
			writer.Write(requirements.magic);
			writer.Write(requirements.armor);
			writer.Write(requirements.legArmor);
			writer.Write(requirements.bodyArmor);
			writer.Write(requirements.headArmor);
			writer.Write(requirements.notVanity);
		}

		public virtual void Load(BinaryReader r, int version, string modversion)
		{
			identifier = r.ReadString();
			affix = r.ReadString();
			suffix = r.ReadBoolean();
			if (Config.CheckVersion(modversion, "0.28.8"))
			{
				weight = r.ReadDouble();
			}
			multiply.defense = r.ReadSingle();
			multiply.crit = r.ReadSingle();
			multiply.mana = r.ReadSingle();
			multiply.damage = r.ReadSingle();
			multiply.scale = r.ReadSingle();
			multiply.knockback = r.ReadSingle();
			multiply.shootSpeed = r.ReadSingle();
			multiply.speed = r.ReadSingle();
			add.defense = r.ReadInt32();
			add.crit = r.ReadInt32();
			add.mana = r.ReadInt32();
			add.damage = r.ReadInt32();
			add.scale = r.ReadInt32();
			add.knockback = r.ReadInt32();
			add.shootSpeed = r.ReadInt32();
			add.speed = r.ReadInt32();
			pAdd.defense = r.ReadSingle();
			pAdd.crit = r.ReadInt32();
			pAdd.mana = r.ReadInt32();
			pAdd.damage = r.ReadSingle();
			pAdd.moveSpeed = r.ReadSingle();
			pAdd.meleeSpeed = r.ReadSingle();
			pAdd.meleeDamage = r.ReadSingle();
			pAdd.rangedDamage = r.ReadSingle();
			pAdd.magicDamage = r.ReadSingle();
			pAdd.meleeCrit = r.ReadInt32();
			pAdd.rangedCrit = r.ReadInt32();
			pAdd.magicCrit = r.ReadInt32();
			requirements.accessory = r.ReadBoolean();
			requirements.melee = r.ReadBoolean();
			requirements.ranged = r.ReadBoolean();
			requirements.magic = r.ReadBoolean();
			requirements.armor = r.ReadBoolean();
			requirements.legArmor = r.ReadBoolean();
			requirements.bodyArmor = r.ReadBoolean();
			requirements.headArmor = r.ReadBoolean();
			requirements.notVanity = r.ReadBoolean();
		}

		public Prefix Require(Requirement req)
		{
			customRequirements.Add(req);
			return this;
		}

		public Prefix Mod(ItemMod m)
		{
			itemMods.Add(m);
			return this;
		}

		public Prefix Mod(PlayerMod m)
		{
			playerMods.Add(m);
			return this;
		}

		public Prefix AddTip(MouseTip tip)
		{
			toolTips.Add(tip);
			return this;
		}

		public Prefix AddTip(string text)
		{
			toolTips.Add(new MouseTip(text));
			return this;
		}

		public Prefix SetName(string name)
		{
			affix = name;
			return this;
		}

		public Prefix SetWeight(double weight)
		{
			this.weight = weight;
			return this;
		}

		public Prefix SetCode(IPrefix code)
		{
			this.code = code;
			return this;
		}

		public Prefix RequireMelee()
		{
			requirements.melee = true;
			return this;
		}

		public Prefix RequireRanged()
		{
			requirements.ranged = true;
			return this;
		}

		public Prefix RequireMagic()
		{
			requirements.magic = true;
			return this;
		}

		public Prefix RequireAccessory()
		{
			requirements.accessory = true;
			return this;
		}

		public Prefix RequireDamage(int dmg)
		{
			requirements.damage = dmg;
			return this;
		}

		public Prefix AddDamage(int dmg)
		{
			add.damage += dmg;
			return this;
		}

		public Prefix AddPlayerDefense(int def)
		{
			pAdd.defense += def;
			return this;
		}

		public Prefix AddPlayerCrit(int crit)
		{
			pAdd.crit += crit;
			return this;
		}

		public Prefix AddPlayerMana(int mana)
		{
			pAdd.mana += mana;
			return this;
		}

		public Prefix AddPlayerDmg(float dmg)
		{
			pAdd.damage += dmg;
			return this;
		}

		public Prefix AddPlayerMeleeDmg(float dmg)
		{
			pAdd.meleeDamage += dmg;
			return this;
		}

		public Prefix AddPlayerRangedDmg(float dmg)
		{
			pAdd.rangedDamage += dmg;
			return this;
		}

		public Prefix AddPlayerMagicDmg(float dmg)
		{
			pAdd.magicDamage += dmg;
			return this;
		}

		public Prefix AddPlayerMeleeCrit(int amt)
		{
			pAdd.meleeCrit += amt;
			return this;
		}

		public Prefix AddPlayerRangedCrit(int amt)
		{
			pAdd.rangedCrit += amt;
			return this;
		}

		public Prefix AddPlayerMagicCrit(int amt)
		{
			pAdd.magicCrit += amt;
			return this;
		}

		public Prefix AddPlayerMovespeed(float speed)
		{
			pAdd.moveSpeed += speed;
			return this;
		}

		public Prefix AddPlayerMeleespeed(float speed)
		{
			pAdd.meleeSpeed += speed;
			return this;
		}

		public Prefix AddSpeed(int speed)
		{
			add.speed += speed;
			return this;
		}

		public Prefix AddManaCost(int mana)
		{
			add.mana += mana;
			return this;
		}

		public Prefix AddCrit(int crit)
		{
			add.crit += crit;
			return this;
		}

		public Prefix MultiplyManaCost(float mana)
		{
			multiply.mana = mana;
			return this;
		}

		public Prefix MultiplyDmg(float dmg)
		{
			multiply.damage = dmg;
			return this;
		}

		public Prefix MultiplyScale(float scale)
		{
			multiply.scale = scale;
			return this;
		}

		public Prefix MultiplySpeed(float speed)
		{
			multiply.speed = speed;
			return this;
		}

		public Prefix MultiplySpeedInverted(float speed)
		{
			IncreaseSpeed(speed - 1f);
			return this;
		}

		public Prefix IncreaseSpeed(float amt)
		{
			multiply.speed -= amt;
			return this;
		}

		public Prefix MultiplyKnockback(float kb)
		{
			multiply.knockback = kb;
			return this;
		}

		public Prefix MultiplyShootspeed(float s)
		{
			multiply.shootSpeed = s;
			return this;
		}

		public virtual void Apply(Item item)
		{
			item.damage = (int)Math.Round((float)item.damage * multiply.damage);
			item.useAnimation = (int)Math.Round((float)item.useAnimation * multiply.speed);
			item.useTime = (int)Math.Round((float)item.useTime * multiply.speed);
			item.reuseDelay = (int)Math.Round((float)item.reuseDelay * multiply.speed);
			item.mana = (int)Math.Round((float)item.mana * multiply.mana);
			item.knockBack *= multiply.knockback;
			item.scale *= multiply.scale;
			item.shootSpeed *= multiply.shootSpeed;
			item.crit += add.crit;
			float num = 1f * multiply.damage * (2f - multiply.speed) * (2f - multiply.mana) * multiply.scale * multiply.knockback * multiply.shootSpeed * (1f + (float)item.crit * 0.02f);
			num *= (1f + 0.05f * pAdd.defense) * (1f + pAdd.damage) * (1f + 5f * pAdd.moveSpeed) * (1f + 5f * pAdd.meleeSpeed);
			if ((double)num >= 1.2)
			{
				item.rare += 2;
			}
			else if ((double)num >= 1.05)
			{
				item.rare++;
			}
			else if ((double)num <= 0.8)
			{
				item.rare -= 2;
			}
			else if ((double)num <= 0.95)
			{
				item.rare--;
			}
			num *= num;
			item.value = (int)((float)item.value * num);
			foreach (ItemMod itemMod in itemMods)
			{
				itemMod(item);
			}
			if (code != null)
			{
				code.Apply(item);
			}
		}

		public virtual string AffixName(Item item, string name)
		{
			name = (suffix ? (name + " " + affix) : (affix + " " + name));
			return name;
		}

		public virtual bool Check(Item item)
		{
			if (item.maxStack > 1)
			{
				return false;
			}
			if (requirements.melee && !item.melee)
			{
				return false;
			}
			if (requirements.accessory && !item.accessory)
			{
				return false;
			}
			if (item.damage >= 0 && (float)item.damage < requirements.damage)
			{
				return false;
			}
			if (requirements.ranged && !item.ranged)
			{
				return false;
			}
			if (requirements.magic && !item.magic)
			{
				return false;
			}
			if ((multiply.damage != 1f || add.damage != 0 || add.crit != 0) && item.damage == -1)
			{
				return false;
			}
			if (multiply.mana != 1f && item.mana == 0)
			{
				return false;
			}
			if ((multiply.speed != 1f || add.crit > 0 || multiply.damage != 1f || multiply.knockback != 1f) && !item.magic && !item.ranged && !item.melee)
			{
				return false;
			}
			if (requirements.armor && item.bodySlot == -1 && item.legSlot == -1 && item.headSlot == -1)
			{
				return false;
			}
			if (requirements.legArmor && item.legSlot == -1)
			{
				return false;
			}
			if (requirements.bodyArmor && item.bodySlot == -1)
			{
				return false;
			}
			if (requirements.headArmor && item.headSlot == -1)
			{
				return false;
			}
			if (requirements.notVanity && item.vanity)
			{
				return false;
			}
			foreach (Requirement customRequirement in customRequirements)
			{
				if (!customRequirement(item))
				{
					return false;
				}
			}
			if (code != null)
			{
				return code.Check(item);
			}
			return true;
		}

		public virtual void Apply(Player player)
		{
			player.statDefense += (int)pAdd.defense;
			player.meleeCrit += (int)pAdd.crit;
			player.rangedCrit += (int)pAdd.crit;
			player.magicCrit += (int)pAdd.crit;
			player.statManaMax2 += (int)pAdd.mana;
			player.meleeDamage += pAdd.damage;
			player.rangedDamage += pAdd.damage;
			player.magicDamage += pAdd.damage;
			player.moveSpeed += pAdd.moveSpeed;
			player.meleeSpeed += pAdd.meleeSpeed;
			foreach (PlayerMod playerMod in playerMods)
			{
				playerMod(player);
			}
			if (code != null)
			{
				code.Apply(player);
			}
		}

		public MouseTip[] UpdateTooltip()
		{
			List<MouseTip> list = new List<MouseTip>();
			if (pAdd.defense != 0f)
			{
				string arg = "+";
				if (pAdd.defense < 0f)
				{
					arg = "-";
				}
				list.Add(new MouseTip(arg + pAdd.defense + " Defense"));
			}
			if (pAdd.crit != 0f)
			{
				string arg2 = "+";
				if (pAdd.crit < 0f)
				{
					arg2 = "-";
				}
				list.Add(new MouseTip(arg2 + pAdd.crit + "% Critical Hit Chance"));
			}
			if (pAdd.meleeCrit != 0f)
			{
				string arg3 = "+";
				if (pAdd.meleeCrit < 0f)
				{
					arg3 = "-";
				}
				list.Add(new MouseTip(arg3 + pAdd.meleeCrit + "% Melee Crit Chance"));
			}
			if (pAdd.rangedCrit != 0f)
			{
				string arg4 = "+";
				if (pAdd.rangedCrit < 0f)
				{
					arg4 = "-";
				}
				list.Add(new MouseTip(arg4 + pAdd.rangedCrit + "% Ranged Crit Chance"));
			}
			if (pAdd.magicCrit != 0f)
			{
				string arg5 = "+";
				if (pAdd.magicCrit < 0f)
				{
					arg5 = "-";
				}
				list.Add(new MouseTip(arg5 + pAdd.magicCrit + "% Magic Crit Chance"));
			}
			if (pAdd.mana != 0f)
			{
				string arg6 = "+";
				if (pAdd.mana < 0f)
				{
					arg6 = "-";
				}
				list.Add(new MouseTip(arg6 + pAdd.mana + " Mana"));
			}
			if (pAdd.damage != 0f)
			{
				string arg7 = "+";
				if (pAdd.damage < 0f)
				{
					arg7 = "";
				}
				list.Add(new MouseTip(arg7 + Math.Round(pAdd.damage * 100f, 2) + "% Damage"));
			}
			if (pAdd.meleeDamage != 0f)
			{
				string arg8 = "+";
				if (pAdd.meleeDamage < 0f)
				{
					arg8 = "";
				}
				list.Add(new MouseTip(arg8 + Math.Round(pAdd.meleeDamage * 100f, 2) + "% Melee Damage"));
			}
			if (pAdd.rangedDamage != 0f)
			{
				string arg9 = "+";
				if (pAdd.rangedDamage < 0f)
				{
					arg9 = "";
				}
				list.Add(new MouseTip(arg9 + Math.Round(pAdd.rangedDamage * 100f, 2) + "% Ranged Damage"));
			}
			if (pAdd.magicDamage != 0f)
			{
				string arg10 = "+";
				if (pAdd.magicDamage < 0f)
				{
					arg10 = "";
				}
				list.Add(new MouseTip(arg10 + Math.Round(pAdd.magicDamage * 100f, 2) + "% Magic Damage"));
			}
			if (pAdd.moveSpeed != 0f)
			{
				string arg11 = "+";
				if (pAdd.moveSpeed < 0f)
				{
					arg11 = "";
				}
				list.Add(new MouseTip(arg11 + Math.Round(pAdd.moveSpeed * 100f, 2) + "% Movement Speed"));
			}
			if (pAdd.meleeSpeed != 0f)
			{
				string arg12 = "+";
				if (pAdd.meleeSpeed < 0f)
				{
					arg12 = "";
				}
				list.Add(new MouseTip(arg12 + Math.Round(pAdd.meleeSpeed * 100f, 2) + "% Melee Speed"));
			}
			list.AddRange(toolTips);
			return list.ToArray();
		}

		public static void DefineDefaults()
		{
			ID = new Dictionary<string, int>();
			List<Prefix> list = new List<Prefix>();
			list.AddRange(new Prefix[85]
			{
				new Prefix("None"),
				new Prefix("Large").RequireMelee().MultiplyScale(1.12f),
				new Prefix("Massive").RequireMelee().MultiplyScale(1.18f),
				new Prefix("Dangerous").RequireMelee().MultiplyScale(1.05f).MultiplyDmg(1.05f)
					.AddCrit(2),
				new Prefix("Savage").RequireMelee().MultiplyScale(1.1f).MultiplyDmg(1.1f)
					.MultiplyKnockback(1.1f),
				new Prefix("Sharp").RequireMelee().MultiplyDmg(1.15f),
				new Prefix("Pointy").RequireMelee().MultiplyDmg(1.1f),
				new Prefix("Tiny").RequireMelee().MultiplyScale(0.82f),
				new Prefix("Terrible").RequireMelee().MultiplyScale(0.87f).MultiplyDmg(0.85f)
					.MultiplyKnockback(0.85f),
				new Prefix("Small").RequireMelee().MultiplyScale(0.9f),
				new Prefix("Dull").RequireMelee().MultiplyDmg(0.85f),
				new Prefix("Unhappy").RequireMelee().MultiplyScale(0.9f).IncreaseSpeed(-0.1f)
					.MultiplyKnockback(0.9f),
				new Prefix("Bulky").RequireMelee().MultiplyDmg(1.05f).IncreaseSpeed(-0.15f)
					.MultiplyScale(1.1f)
					.MultiplyKnockback(1.1f),
				new Prefix("Shameful").RequireMelee().MultiplyDmg(0.9f).MultiplyScale(1.1f)
					.MultiplyKnockback(0.8f),
				new Prefix("Heavy").RequireMelee().IncreaseSpeed(-0.1f).MultiplyKnockback(1.15f),
				new Prefix("Light").RequireMelee().IncreaseSpeed(0.15f).MultiplyKnockback(0.9f),
				new Prefix("Sighted").RequireRanged().MultiplyDmg(1.1f).AddCrit(3),
				new Prefix("Rapid").RequireRanged().IncreaseSpeed(0.15f).MultiplyShootspeed(1.1f),
				new Prefix("Hasty").RequireRanged().IncreaseSpeed(0.1f).MultiplyShootspeed(1.15f),
				new Prefix("Intimidating").RequireRanged().MultiplyShootspeed(1.05f).MultiplyKnockback(1.15f),
				new Prefix("Deadly").RequireRanged().MultiplyDmg(1.1f).IncreaseSpeed(0.05f)
					.AddCrit(2)
					.MultiplyShootspeed(1.05f)
					.MultiplyKnockback(1.05f),
				new Prefix("Staunch").RequireRanged().MultiplyDmg(1.1f).MultiplyKnockback(1.15f),
				new Prefix("Awful").RequireRanged().MultiplyDmg(0.9f).MultiplyShootspeed(0.9f)
					.MultiplyKnockback(0.9f),
				new Prefix("Lethargic").RequireRanged().IncreaseSpeed(-0.15f).MultiplyShootspeed(0.9f),
				new Prefix("Awkward").RequireRanged().IncreaseSpeed(-0.1f).MultiplyKnockback(0.8f),
				new Prefix("Powerful").RequireRanged().MultiplyDmg(1.15f).IncreaseSpeed(-0.1f)
					.AddCrit(1),
				new Prefix("Mystic").RequireMagic().MultiplyDmg(1.1f).MultiplyManaCost(0.85f),
				new Prefix("Adept").RequireMagic().MultiplyManaCost(0.85f),
				new Prefix("Masterful").RequireMagic().MultiplyDmg(1.15f).MultiplyManaCost(0.8f)
					.MultiplyKnockback(1.05f),
				new Prefix("Inept").RequireMagic().MultiplyManaCost(1.1f),
				new Prefix("Ignorant").RequireMagic().MultiplyDmg(0.9f).MultiplyManaCost(1.2f),
				new Prefix("Deranged").RequireMagic().MultiplyDmg(0.9f).MultiplyKnockback(0.9f),
				new Prefix("Intense").RequireMagic().MultiplyDmg(1.1f).MultiplyManaCost(1.15f),
				new Prefix("Taboo").RequireMagic().IncreaseSpeed(0.1f).MultiplyManaCost(1.1f)
					.MultiplyKnockback(1.1f),
				new Prefix("Celestial").RequireMagic().MultiplyDmg(1.1f).IncreaseSpeed(-0.1f)
					.MultiplyManaCost(0.9f)
					.MultiplyKnockback(1.1f),
				new Prefix("Furious").RequireMagic().MultiplyDmg(1.15f).MultiplyManaCost(1.2f)
					.MultiplyKnockback(1.15f),
				new Prefix("Keen").AddCrit(3),
				new Prefix("Superior").MultiplyDmg(1.1f).AddCrit(3).MultiplyKnockback(1.1f),
				new Prefix("Forceful").MultiplyKnockback(1.15f),
				new Prefix("Broken").MultiplyDmg(0.7f).MultiplyKnockback(0.8f),
				new Prefix("Damaged").MultiplyDmg(0.85f),
				new Prefix("Shoddy").MultiplyDmg(0.9f).MultiplyKnockback(0.85f),
				new Prefix("Quick").IncreaseSpeed(0.1f),
				new Prefix("Deadly").MultiplyDmg(1.1f).IncreaseSpeed(0.1f),
				new Prefix("Agile").IncreaseSpeed(0.1f).AddCrit(3),
				new Prefix("Nimble").IncreaseSpeed(0.05f),
				new Prefix("Murderous").MultiplyDmg(1.07f).IncreaseSpeed(0.06f).AddCrit(3),
				new Prefix("Slow").IncreaseSpeed(-0.15f),
				new Prefix("Sluggish").IncreaseSpeed(-0.2f),
				new Prefix("Lazy").IncreaseSpeed(-0.08f),
				new Prefix("Annoying").MultiplyDmg(0.8f).IncreaseSpeed(-0.15f),
				new Prefix("Nasty").MultiplyDmg(1.05f).IncreaseSpeed(0.1f).AddCrit(2)
					.MultiplyKnockback(0.9f),
				new Prefix("Manic").RequireMagic().MultiplyDmg(0.9f).IncreaseSpeed(0.1f)
					.MultiplyManaCost(0.9f),
				new Prefix("Hurtful").MultiplyDmg(1.1f),
				new Prefix("Strong").MultiplyKnockback(1.15f),
				new Prefix("Unpleasant").MultiplyDmg(1.05f).MultiplyKnockback(1.15f),
				new Prefix("Weak").MultiplyKnockback(0.8f),
				new Prefix("Ruthless").MultiplyDmg(1.18f).MultiplyKnockback(0.9f),
				new Prefix("Frenzying").RequireRanged().MultiplyDmg(0.85f).IncreaseSpeed(0.15f),
				new Prefix("Godly").MultiplyDmg(1.15f).AddCrit(5).MultiplyKnockback(1.15f),
				new Prefix("Demonic").MultiplyDmg(1.15f).AddCrit(5),
				new Prefix("Zealous").AddCrit(5),
				new Prefix("Hard").RequireAccessory().AddPlayerDefense(1),
				new Prefix("Guarding").RequireAccessory().AddPlayerDefense(2),
				new Prefix("Armored").RequireAccessory().AddPlayerDefense(3),
				new Prefix("Warding").RequireAccessory().AddPlayerDefense(4),
				new Prefix("Arcane").RequireAccessory().AddPlayerMana(20),
				new Prefix("Precise").RequireAccessory().AddPlayerCrit(1),
				new Prefix("Lucky").RequireAccessory().AddPlayerCrit(2),
				new Prefix("Jagged").RequireAccessory().AddPlayerDmg(0.01f),
				new Prefix("Spiked").RequireAccessory().AddPlayerDmg(0.02f),
				new Prefix("Angry").RequireAccessory().AddPlayerDmg(0.03f),
				new Prefix("Menacing").RequireAccessory().AddPlayerDmg(0.04f),
				new Prefix("Brisk").RequireAccessory().AddPlayerMovespeed(0.01f),
				new Prefix("Fleeting").RequireAccessory().AddPlayerMovespeed(0.02f),
				new Prefix("Hasty").RequireAccessory().AddPlayerMovespeed(0.03f),
				new Prefix("Quick").RequireAccessory().AddPlayerMovespeed(0.04f),
				new Prefix("Wild").RequireAccessory().AddPlayerMeleespeed(0.01f),
				new Prefix("Rash").RequireAccessory().AddPlayerMeleespeed(0.02f),
				new Prefix("Intrepid").RequireAccessory().AddPlayerMeleespeed(0.03f),
				new Prefix("Violent").RequireAccessory().AddPlayerMeleespeed(0.04f),
				new Prefix("Legendary").RequireMelee().MultiplyDmg(1.15f).IncreaseSpeed(0.1f)
					.AddCrit(5)
					.MultiplyScale(1.1f)
					.MultiplyKnockback(1.15f),
				new Prefix("Unreal").RequireRanged().MultiplyDmg(1.15f).IncreaseSpeed(0.1f)
					.AddCrit(5)
					.MultiplyShootspeed(1.1f)
					.MultiplyKnockback(1.15f),
				new Prefix("Mythical").RequireMagic().MultiplyDmg(1.15f).IncreaseSpeed(0.1f)
					.AddCrit(5)
					.MultiplyManaCost(0.9f)
					.MultiplyKnockback(1.15f),
				new UnloadedPrefix("Mysterious")
			});
			prefixes = list;
			for (int i = 0; i < prefixes.Count; i++)
			{
				ID[":" + prefixes[i].identifier] = i;
			}
		}
	}
}
