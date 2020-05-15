using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Terraria
{
	public class Item : Codable
	{
		public delegate bool CanEquip_Del(Player p, int i);

		public delegate void PreDrawItem_Del(SpriteBatch SP, Item I, ref Color DrawAlpha, ref float Scale);

		public delegate void DrawBeforeShooting_Del(Player P, SpriteBatch SP, ref int shootOff, ref Vector2 ShootCenter);

		public delegate void AffixName_Del(ref string str, bool pre_prefixing);

		public delegate void UpdateItem_Custom_Del(int I, ref bool LetUpdate, ref int MovementType, ref int LavaImmunity);

		public delegate bool OverrideItemGrab_Del(Player P, int PlayerID, int ItemIndex);

		public Events.Player playerEvents;

		public CanEquip_Del CanEquip;

		public Action<Player, int> OnEquip;

		public Action<Player, int> OnUnequip;

		public Func<Player, int, bool> AccCheck;

		public Action<Player> VanityEffects;

		public Func<Player, int, int, bool> InvRightClicked;

		public PreDrawItem_Del PreDrawItem;

		public DrawBeforeShooting_Del DrawBeforeShooting;

		public Action Initialize;

		public Action<Player> Effects;

		public Func<int, bool> OverridePrefix;

		public Action<int> PostPrefix;

		public AffixName_Del AffixName_Custom;

		public UpdateItem_Custom_Del UpdateItem_Custom;

		public Action<Player, int, int> PlayerGrab;

		public OverrideItemGrab_Del OverrideItemGrab;

		public OverrideItemGrab_Del OverrideItemVacuum;

		public Action<Player, int> PreItemCheck;

		public Func<Player, int, bool> CanUse;

		public Func<Projectile, CustomProjectile> SpawnProjectile;

		public Action<Projectile> RegisterProjectile;

		public string displayName;

		public string unloadedPrefix;

		public string toolTip3;

		public string toolTip4;

		public string toolTip5;

		public string toolTip6;

		public string toolTip7;

		public static int potionDelay = 3600;

		public static ArrayHandler<int> headType = new ArrayHandler<int>(45);

		public static ArrayHandler<int> bodyType = new ArrayHandler<int>(26);

		public static ArrayHandler<int> legType = new ArrayHandler<int>(25);

		public bool mech;

		public int noGrabDelay;

		public bool beingGrabbed;

		public int spawnTime;

		public bool wornArmor;

		public int ownIgnore = -1;

		public int ownTime;

		public int keepTime;

		public int type;

		public int holdStyle;

		public int useStyle;

		public bool channel;

		public bool accessory;

		public int useAnimation;

		public int useTime;

		public int stack;

		public int maxStack;

		public int pick;

		public int axe;

		public int hammer;

		public int tileBoost;

		public int createTile = -1;

		public int createWall = -1;

		public int placeStyle;

		public int damage;

		public float knockBack;

		public int healLife;

		public int healMana;

		public bool potion;

		public bool consumable;

		public bool autoReuse;

		public bool useTurn;

		public Color color;

		public int alpha;

		public float scale = 1f;

		public int useSound;

		public int defense;

		public int headSlot = -1;

		public int bodySlot = -1;

		public int legSlot = -1;

		public string toolTip;

		public string toolTip2;

		public int owner = 255;

		public int rare;

		public int shoot;

		public float shootSpeed;

		public int ammo;

		public int useAmmo;

		public int lifeRegen;

		public int manaIncrease;

		public bool buyOnce;

		public int mana;

		public bool noUseGraphic;

		public bool noMelee;

		public int release;

		public int value;

		public bool buy;

		public bool social;

		public bool vanity;

		public bool material;

		public bool noWet;

		public int buffType;

		public int buffTime;

		public int netID;

		public int crit;

		public byte prefix;

		public bool melee;

		public bool magic;

		public bool ranged;

		public int reuseDelay;

		public override void ResetEvents()
		{
			base.ResetEvents();
			CanEquip = null;
			OnEquip = null;
			OnUnequip = null;
			AccCheck = null;
			VanityEffects = null;
			InvRightClicked = null;
			PreDrawItem = null;
			DrawBeforeShooting = null;
			Initialize = null;
			Effects = null;
			OverridePrefix = null;
			PostPrefix = null;
			AffixName_Custom = null;
			UpdateItem_Custom = null;
			PlayerGrab = null;
			OverrideItemGrab = null;
			OverrideItemVacuum = null;
			PreItemCheck = null;
			CanUse = null;
			playerEvents = new Events.Player();
			SpawnProjectile = null;
			RegisterProjectile = null;
			SetDefaultEvents();
		}

		public override void RegisterEvents(object code)
		{
			base.RegisterEvents(code);
			if (code != null)
			{
				Register(ref CanEquip, code, "CanEquip");
				Register(ref OnEquip, code, "OnEquip");
				Register(ref OnUnequip, code, "OnUnequip");
				Register(ref AccCheck, code, "AccCheck");
				Register(ref VanityEffects, code, "VanityEffects");
				Register(ref InvRightClicked, code, "InvRightClicked");
				Register(ref PreDrawItem, code, "PreDrawItem");
				Register(ref DrawBeforeShooting, code, "DrawBeforeShooting");
				Register(ref Initialize, code, "Initialize");
				Register(ref Effects, code, "Effects");
				Register(ref OverridePrefix, code, "OverridePrefix");
				Register(ref PostPrefix, code, "PostPrefix");
				Register(ref AffixName_Custom, code, "AffixName");
				Register(ref UpdateItem_Custom, code, "UpdateItem");
				Register(ref PlayerGrab, code, "PlayerGrab");
				Register(ref OverrideItemGrab, code, "OverrideItemGrab");
				Register(ref OverrideItemVacuum, code, "OverrideItemVacuum");
				Register(ref PreItemCheck, code, "PreItemCheck");
				Register(ref CanUse, code, "CanUse");
				Register(ref SpawnProjectile, code, "SpawnProjectile");
				Register(ref RegisterProjectile, code, "RegisterProjectile");
				if (playerEvents == null)
				{
					playerEvents = new Events.Player();
				}
				playerEvents.RegisterEvents(code);
			}
		}

		public override void SetDefaultEvents()
		{
			base.SetDefaultEvents();
			if (CanEquip == null)
			{
				CanEquip = ((Player p, int s) => true);
			}
			if (OnEquip == null)
			{
				OnEquip = delegate
				{
				};
			}
			if (OnUnequip == null)
			{
				OnUnequip = delegate
				{
				};
			}
			if (VanityEffects == null)
			{
				VanityEffects = delegate
				{
				};
			}
			if (InvRightClicked == null)
			{
				InvRightClicked = ((Player p, int ip, int i) => true);
			}
			if (PreDrawItem == null)
			{
				PreDrawItem = delegate
				{
				};
			}
			if (DrawBeforeShooting == null)
			{
				DrawBeforeShooting = delegate
				{
				};
			}
			if (UpdateItem_Custom == null)
			{
				UpdateItem_Custom = delegate
				{
				};
			}
			if (Initialize == null)
			{
				Initialize = delegate
				{
				};
			}
			if (Effects == null)
			{
				Effects = delegate
				{
				};
			}
			if (PostPrefix == null)
			{
				PostPrefix = delegate
				{
				};
			}
		}

		public override void Init(int type = -1)
		{
			className = "Item";
			base.Init(type);
		}

		public override string ToString()
		{
			return "Item " + name;
		}

		public Item()
		{
			className = "Item";
			SetDefaultEvents();
		}

		public bool Prefix(int pre)
		{
			if (type == 0)
			{
				return false;
			}
			if (OverridePrefix != null)
			{
				return OverridePrefix(pre);
			}
			switch (pre)
			{
			case 0:
				PostPrefix(pre);
				return false;
			case -2:
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryWriter writer = new BinaryWriter(memoryStream);
				Codable.SaveCustomData(Main.reforgeItem, writer);
				memoryStream.Position = 0L;
				Main.reforgeItem.SetDefaults(Main.reforgeItem.name);
				bool result2 = RealPrefix(pre);
				PostPrefix(pre);
				Codable.LoadCustomData(Main.reforgeItem, new BinaryReader(memoryStream));
				return result2;
			}
			default:
			{
				bool result = RealPrefix(pre);
				PostPrefix(pre);
				return result;
			}
			}
		}

		public bool RealPrefix(int pre)
		{
			if (pre == 0 || type == 0)
			{
				return false;
			}
			if (pre <= -1)
			{
				WeightedRandom<int> weightedRandom = new WeightedRandom<int>(Main.rand);
				for (int i = (pre == -2 || pre == -3) ? 1 : 0; i < Terraria.Prefix.prefixes.Count; i++)
				{
					if (Terraria.Prefix.prefixes[i].Check(this))
					{
						weightedRandom.Add(i, Terraria.Prefix.prefixes[i].weight);
					}
				}
				RunMethod("ModifyPrefixList", weightedRandom);
				if (pre == -3)
				{
					return weightedRandom.list.Count > 0;
				}
				pre = weightedRandom.Get();
				if (pre == -1)
				{
					return false;
				}
			}
			if (pre < Terraria.Prefix.prefixes.Count)
			{
				Terraria.Prefix.prefixes[pre].Apply(this);
				prefix = (byte)pre;
				if (rare < Main.colorRarityMin)
				{
					rare = Main.colorRarityMin;
				}
				if (rare > Main.colorRarityMax)
				{
					rare = Main.colorRarityMax;
				}
				return true;
			}
			return false;
		}

		public string AffixName()
		{
			string text = "";
			text = name;
			if (!string.IsNullOrEmpty(displayName))
			{
				text = displayName;
			}
			if (AffixName_Custom != null)
			{
				AffixName_Custom(ref text, pre_prefixing: false);
			}
			if (prefix != 0)
			{
				text = Terraria.Prefix.prefixes[prefix].AffixName(this, text);
			}
			if (AffixName_Custom != null)
			{
				AffixName_Custom(ref text, pre_prefixing: true);
			}
			return text;
		}

		public void CheckTip()
		{
			if (toolTip != "")
			{
				toolTip = Lang.toolTip(netID);
			}
			if (toolTip2 != "")
			{
				toolTip2 = Lang.toolTip2(netID);
			}
		}

		public void SetDefaults(string ItemName, bool pointless = false, bool pointless2 = false)
		{
			Reset();
			className = "Item";
			name = "";
			bool flag = false;
			Item original;
			if (Config.generateItemObj || Config.generateINI || !Config.initialized)
			{
				switch (ItemName)
				{
				case "Gold Pickaxe":
					SetDefaults(1, noMatCheck: false, saveData: false);
					color = new Color(210, 190, 0, 100);
					useTime = 17;
					pick = 55;
					useAnimation = 20;
					scale = 1.05f;
					damage = 6;
					value = 10000;
					toolTip = "Can mine Meteorite";
					netID = -1;
					break;
				case "Gold Broadsword":
					SetDefaults(4, noMatCheck: false, saveData: false);
					color = new Color(210, 190, 0, 100);
					useAnimation = 20;
					damage = 13;
					scale = 1.05f;
					value = 9000;
					netID = -2;
					break;
				case "Gold Shortsword":
					SetDefaults(6, noMatCheck: false, saveData: false);
					color = new Color(210, 190, 0, 100);
					damage = 11;
					useAnimation = 11;
					scale = 0.95f;
					value = 7000;
					netID = -3;
					break;
				case "Gold Axe":
					SetDefaults(10, noMatCheck: false, saveData: false);
					color = new Color(210, 190, 0, 100);
					useTime = 18;
					axe = 11;
					useAnimation = 26;
					scale = 1.15f;
					damage = 7;
					value = 8000;
					netID = -4;
					break;
				case "Gold Hammer":
					SetDefaults(7, noMatCheck: false, saveData: false);
					color = new Color(210, 190, 0, 100);
					useAnimation = 28;
					useTime = 23;
					scale = 1.25f;
					damage = 9;
					hammer = 55;
					value = 8000;
					netID = -5;
					break;
				case "Gold Bow":
					SetDefaults(99, noMatCheck: false, saveData: false);
					useAnimation = 26;
					useTime = 26;
					color = new Color(210, 190, 0, 100);
					damage = 11;
					value = 7000;
					netID = -6;
					break;
				case "Silver Pickaxe":
					SetDefaults(1, noMatCheck: false, saveData: false);
					color = new Color(180, 180, 180, 100);
					useTime = 11;
					pick = 45;
					useAnimation = 19;
					scale = 1.05f;
					damage = 6;
					value = 5000;
					netID = -7;
					break;
				case "Silver Broadsword":
					SetDefaults(4, noMatCheck: false, saveData: false);
					color = new Color(180, 180, 180, 100);
					useAnimation = 21;
					damage = 11;
					value = 4500;
					netID = -8;
					break;
				case "Silver Shortsword":
					SetDefaults(6, noMatCheck: false, saveData: false);
					color = new Color(180, 180, 180, 100);
					damage = 9;
					useAnimation = 12;
					scale = 0.95f;
					value = 3500;
					netID = -9;
					break;
				case "Silver Axe":
					SetDefaults(10, noMatCheck: false, saveData: false);
					color = new Color(180, 180, 180, 100);
					useTime = 18;
					axe = 10;
					useAnimation = 26;
					scale = 1.15f;
					damage = 6;
					value = 4000;
					netID = -10;
					break;
				case "Silver Hammer":
					SetDefaults(7, noMatCheck: false, saveData: false);
					color = new Color(180, 180, 180, 100);
					useAnimation = 29;
					useTime = 19;
					scale = 1.25f;
					damage = 9;
					hammer = 45;
					value = 4000;
					netID = -11;
					break;
				case "Silver Bow":
					SetDefaults(99, noMatCheck: false, saveData: false);
					useAnimation = 27;
					useTime = 27;
					color = new Color(180, 180, 180, 100);
					damage = 9;
					value = 3500;
					netID = -12;
					break;
				case "Copper Pickaxe":
					SetDefaults(1, noMatCheck: false, saveData: false);
					color = new Color(180, 100, 45, 80);
					useTime = 15;
					pick = 35;
					useAnimation = 23;
					damage = 4;
					scale = 0.9f;
					tileBoost = -1;
					value = 500;
					netID = -13;
					break;
				case "Copper Broadsword":
					SetDefaults(4, noMatCheck: false, saveData: false);
					color = new Color(180, 100, 45, 80);
					useAnimation = 23;
					damage = 8;
					value = 450;
					netID = -14;
					break;
				case "Copper Shortsword":
					SetDefaults(6, noMatCheck: false, saveData: false);
					color = new Color(180, 100, 45, 80);
					damage = 5;
					useAnimation = 13;
					scale = 0.8f;
					value = 350;
					netID = -15;
					break;
				case "Copper Axe":
					SetDefaults(10, noMatCheck: false, saveData: false);
					color = new Color(180, 100, 45, 80);
					useTime = 21;
					axe = 7;
					useAnimation = 30;
					scale = 1f;
					damage = 3;
					tileBoost = -1;
					value = 400;
					netID = -16;
					break;
				case "Copper Hammer":
					SetDefaults(7, noMatCheck: false, saveData: false);
					color = new Color(180, 100, 45, 80);
					useAnimation = 33;
					useTime = 23;
					scale = 1.1f;
					damage = 4;
					hammer = 35;
					tileBoost = -1;
					value = 400;
					netID = -17;
					break;
				case "Copper Bow":
					SetDefaults(99, noMatCheck: false, saveData: false);
					useAnimation = 29;
					useTime = 29;
					color = new Color(180, 100, 45, 80);
					damage = 6;
					value = 350;
					netID = -18;
					break;
				case "Blue Phasesaber":
					SetDefaults(198, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -19;
					break;
				case "Red Phasesaber":
					SetDefaults(199, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -20;
					break;
				case "Green Phasesaber":
					SetDefaults(200, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -21;
					break;
				case "Purple Phasesaber":
					SetDefaults(201, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -22;
					break;
				case "White Phasesaber":
					SetDefaults(202, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -23;
					break;
				case "Yellow Phasesaber":
					SetDefaults(203, noMatCheck: false, saveData: false);
					damage = 41;
					scale = 1.15f;
					flag = true;
					autoReuse = true;
					useTurn = true;
					rare = 4;
					netID = -24;
					break;
				default:
				{
					if (!(ItemName != ""))
					{
						break;
					}
					for (int i = 0; i < 604; i++)
					{
						if (Main.itemName[i] == ItemName)
						{
							SetDefaults(i, noMatCheck: false, saveData: false);
							checkMat();
							return;
						}
					}
					name = "";
					stack = 0;
					type = 0;
					break;
				}
				}
				if (type != 0)
				{
					if (flag)
					{
						material = false;
					}
					else
					{
						checkMat();
					}
					name = ItemName;
					if ((Config.generateINI || Config.generateItemObj) && Config.initialized)
					{
						Config.ProcessINI(this, "Item", ItemName, "Defaults");
					}
				}
			}
			else if (ItemName != null && Config.itemDefs.byName.TryGetValue(ItemName, out original))
			{
				Config.CopyAttributes(this, original, "Item");
				Init();
				RunMethod("Initialize");
			}
			else if (ItemName != "" && ItemName != null)
			{
				byte b = prefix;
				Config.CopyAttributes(this, Config.itemDefs.byName["Unloaded Item"], "Item");
				Init();
				RunMethod("Initialize");
				RunMethod("InitItem", ItemName, b);
			}
		}

		public bool checkMat()
		{
			if (type >= 71 && type <= 74)
			{
				material = false;
				return false;
			}
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				for (int j = 0; Main.recipe[i].requiredItem[j].type > 0; j++)
				{
					if (name == Main.recipe[i].requiredItem[j].name)
					{
						material = true;
						return true;
					}
				}
			}
			material = false;
			return false;
		}

		public void netDefaults(int type)
		{
			if (type < 0)
			{
				switch (type)
				{
				case -1:
					SetDefaults("Gold Pickaxe");
					break;
				case -2:
					SetDefaults("Gold Broadsword");
					break;
				case -3:
					SetDefaults("Gold Shortsword");
					break;
				case -4:
					SetDefaults("Gold Axe");
					break;
				case -5:
					SetDefaults("Gold Hammer");
					break;
				case -6:
					SetDefaults("Gold Bow");
					break;
				case -7:
					SetDefaults("Silver Pickaxe");
					break;
				case -8:
					SetDefaults("Silver Broadsword");
					break;
				case -9:
					SetDefaults("Silver Shortsword");
					break;
				case -10:
					SetDefaults("Silver Axe");
					break;
				case -11:
					SetDefaults("Silver Hammer");
					break;
				case -12:
					SetDefaults("Silver Bow");
					break;
				case -13:
					SetDefaults("Copper Pickaxe");
					break;
				case -14:
					SetDefaults("Copper Broadsword");
					break;
				case -15:
					SetDefaults("Copper Shortsword");
					break;
				case -16:
					SetDefaults("Copper Axe");
					break;
				case -17:
					SetDefaults("Copper Hammer");
					break;
				case -18:
					SetDefaults("Copper Bow");
					break;
				case -19:
					SetDefaults("Blue Phasesaber");
					break;
				case -20:
					SetDefaults("Red Phasesaber");
					break;
				case -21:
					SetDefaults("Green Phasesaber");
					break;
				case -22:
					SetDefaults("Purple Phasesaber");
					break;
				case -23:
					SetDefaults("White Phasesaber");
					break;
				case -24:
					SetDefaults("Yellow Phasesaber");
					break;
				}
			}
			else
			{
				SetDefaults(type);
			}
		}

		public void SetDefaults(int Type, bool noMatCheck = false, bool saveData = true)
		{
			Reset();
			className = "Item";
			if (Main.netMode == 1 || Main.netMode == 2)
			{
				owner = 255;
			}
			else
			{
				owner = Main.myPlayer;
			}
			if (Config.generateItemObj || Config.generateINI || !Config.initialized)
			{
				netID = 0;
				displayName = "";
				prefix = 0;
				crit = 0;
				mech = false;
				reuseDelay = 0;
				melee = false;
				magic = false;
				ranged = false;
				placeStyle = 0;
				buffTime = 0;
				buffType = 0;
				material = false;
				noWet = false;
				vanity = false;
				mana = 0;
				wet = false;
				wetCount = 0;
				lavaWet = false;
				channel = false;
				manaIncrease = 0;
				release = 0;
				noMelee = false;
				noUseGraphic = false;
				lifeRegen = 0;
				shootSpeed = 0f;
				active = true;
				alpha = 0;
				ammo = 0;
				useAmmo = 0;
				autoReuse = false;
				accessory = false;
				axe = 0;
				healMana = 0;
				bodySlot = -1;
				legSlot = -1;
				headSlot = -1;
				potion = false;
				color = default(Color);
				consumable = false;
				createTile = -1;
				createWall = -1;
				damage = -1;
				defense = 0;
				hammer = 0;
				healLife = 0;
				holdStyle = 0;
				knockBack = 0f;
				maxStack = 1;
				pick = 0;
				rare = 0;
				scale = 1f;
				shoot = 0;
				stack = 1;
				toolTip = null;
				toolTip2 = null;
				tileBoost = 0;
				type = Type;
				useStyle = 0;
				useSound = 0;
				useTime = 100;
				useAnimation = 100;
				value = 0;
				useTurn = false;
				buy = false;
				if (type == 0)
				{
					name = "";
					stack = 0;
				}
				else if (type == 1)
				{
					name = "Iron Pickaxe";
					useStyle = 1;
					useTurn = true;
					useAnimation = 20;
					useTime = 13;
					autoReuse = true;
					width = 24;
					height = 28;
					damage = 5;
					pick = 40;
					useSound = 1;
					knockBack = 2f;
					value = 2000;
					melee = true;
				}
				else if (type == 2)
				{
					name = "Dirt Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 0;
					width = 12;
					height = 12;
				}
				else if (type == 3)
				{
					name = "Stone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 1;
					width = 12;
					height = 12;
				}
				else if (type == 4)
				{
					name = "Iron Broadsword";
					useStyle = 1;
					useTurn = false;
					useAnimation = 21;
					useTime = 21;
					width = 24;
					height = 28;
					damage = 10;
					knockBack = 5f;
					useSound = 1;
					scale = 1f;
					value = 1800;
					melee = true;
				}
				else if (type == 5)
				{
					name = "Mushroom";
					useStyle = 2;
					useSound = 2;
					useTurn = false;
					useAnimation = 17;
					useTime = 17;
					width = 16;
					height = 18;
					healLife = 15;
					maxStack = 99;
					consumable = true;
					potion = true;
					value = 25;
				}
				else if (type == 6)
				{
					name = "Iron Shortsword";
					useStyle = 3;
					useTurn = false;
					useAnimation = 12;
					useTime = 12;
					width = 24;
					height = 28;
					damage = 8;
					knockBack = 4f;
					scale = 0.9f;
					useSound = 1;
					useTurn = true;
					value = 1400;
					melee = true;
				}
				else if (type == 7)
				{
					name = "Iron Hammer";
					autoReuse = true;
					useStyle = 1;
					useTurn = true;
					useAnimation = 30;
					useTime = 20;
					hammer = 45;
					width = 24;
					height = 28;
					damage = 7;
					knockBack = 5.5f;
					scale = 1.2f;
					useSound = 1;
					value = 1600;
					melee = true;
				}
				else if (type == 8)
				{
					noWet = true;
					name = "Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					width = 10;
					height = 12;
					toolTip = "Provides light";
					value = 50;
				}
				else if (type == 9)
				{
					name = "Wood";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 30;
					width = 8;
					height = 10;
				}
				else if (type == 10)
				{
					name = "Iron Axe";
					useStyle = 1;
					useTurn = true;
					useAnimation = 27;
					knockBack = 4.5f;
					useTime = 19;
					autoReuse = true;
					width = 24;
					height = 28;
					damage = 5;
					axe = 9;
					scale = 1.1f;
					useSound = 1;
					value = 1600;
					melee = true;
				}
				else if (type == 11)
				{
					name = "Iron Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 6;
					width = 12;
					height = 12;
					value = 500;
				}
				else if (type == 12)
				{
					name = "Copper Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 7;
					width = 12;
					height = 12;
					value = 250;
				}
				else if (type == 13)
				{
					name = "Gold Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 8;
					width = 12;
					height = 12;
					value = 2000;
				}
				else if (type == 14)
				{
					name = "Silver Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 9;
					width = 12;
					height = 12;
					value = 1000;
				}
				else if (type == 15)
				{
					name = "Copper Watch";
					width = 24;
					height = 28;
					accessory = true;
					toolTip = "Tells the time";
					value = 1000;
				}
				else if (type == 16)
				{
					name = "Silver Watch";
					width = 24;
					height = 28;
					accessory = true;
					toolTip = "Tells the time";
					value = 5000;
				}
				else if (type == 17)
				{
					name = "Gold Watch";
					width = 24;
					height = 28;
					accessory = true;
					rare = 1;
					toolTip = "Tells the time";
					value = 10000;
				}
				else if (type == 18)
				{
					name = "Depth Meter";
					width = 24;
					height = 18;
					accessory = true;
					rare = 1;
					toolTip = "Shows depth";
					value = 10000;
				}
				else if (type == 19)
				{
					name = "Gold Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 6000;
				}
				else if (type == 20)
				{
					name = "Copper Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 750;
				}
				else if (type == 21)
				{
					name = "Silver Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 3000;
				}
				else if (type == 22)
				{
					name = "Iron Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 1500;
				}
				else if (type == 23)
				{
					name = "Gel";
					width = 10;
					height = 12;
					maxStack = 250;
					alpha = 175;
					ammo = 23;
					color = new Color(0, 80, 255, 100);
					toolTip = "'Both tasty and flammable'";
					value = 5;
				}
				else if (type == 24)
				{
					name = "Wooden Sword";
					useStyle = 1;
					useTurn = false;
					useAnimation = 25;
					width = 24;
					height = 28;
					damage = 7;
					knockBack = 4f;
					scale = 0.95f;
					useSound = 1;
					value = 100;
					melee = true;
				}
				else if (type == 25)
				{
					name = "Wooden Door";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 10;
					width = 14;
					height = 28;
					value = 200;
				}
				else if (type == 26)
				{
					name = "Stone Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 1;
					width = 12;
					height = 12;
				}
				else if (type == 27)
				{
					name = "Acorn";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 20;
					width = 18;
					height = 18;
					value = 10;
				}
				else if (type == 28)
				{
					name = "Lesser Healing Potion";
					useSound = 3;
					healLife = 50;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					potion = true;
					value = 300;
				}
				else if (type == 29)
				{
					name = "Life Crystal";
					maxStack = 99;
					consumable = true;
					width = 18;
					height = 18;
					useStyle = 4;
					useTime = 30;
					useSound = 4;
					useAnimation = 30;
					toolTip = "Permanently increases maximum life by 20";
					rare = 2;
					value = 75000;
				}
				else if (type == 30)
				{
					name = "Dirt Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 16;
					width = 12;
					height = 12;
				}
				else if (type == 31)
				{
					name = "Bottle";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 13;
					width = 16;
					height = 24;
					value = 20;
				}
				else if (type == 32)
				{
					name = "Wooden Table";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 14;
					width = 26;
					height = 20;
					value = 300;
				}
				else if (type == 33)
				{
					name = "Furnace";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 17;
					width = 26;
					height = 24;
					value = 300;
					toolTip = "Used for smelting ore";
				}
				else if (type == 34)
				{
					name = "Wooden Chair";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 15;
					width = 12;
					height = 30;
					value = 150;
				}
				else if (type == 35)
				{
					name = "Iron Anvil";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 16;
					width = 28;
					height = 14;
					value = 5000;
					toolTip = "Used to craft items from metal bars";
				}
				else if (type == 36)
				{
					name = "Work Bench";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 18;
					width = 28;
					height = 14;
					value = 150;
					toolTip = "Used for basic crafting";
				}
				else if (type == 37)
				{
					name = "Goggles";
					width = 28;
					height = 12;
					defense = 1;
					headSlot = 10;
					rare = 1;
					value = 1000;
				}
				else if (type == 38)
				{
					name = "Lens";
					width = 12;
					height = 20;
					maxStack = 99;
					value = 500;
				}
				else if (type == 39)
				{
					useStyle = 5;
					useAnimation = 30;
					useTime = 30;
					name = "Wooden Bow";
					width = 12;
					height = 28;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 4;
					shootSpeed = 6.1f;
					noMelee = true;
					value = 100;
					ranged = true;
				}
				else if (type == 40)
				{
					name = "Wooden Arrow";
					shootSpeed = 3f;
					shoot = 1;
					damage = 4;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 2f;
					value = 10;
					ranged = true;
				}
				else if (type == 41)
				{
					name = "Flaming Arrow";
					shootSpeed = 3.5f;
					shoot = 2;
					damage = 6;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 2f;
					value = 15;
					ranged = true;
				}
				else if (type == 42)
				{
					useStyle = 1;
					name = "Shuriken";
					shootSpeed = 9f;
					shoot = 3;
					damage = 10;
					width = 18;
					height = 20;
					maxStack = 250;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 20;
					ranged = true;
				}
				else if (type == 43)
				{
					useStyle = 4;
					name = "Suspicious Looking Eye";
					width = 22;
					height = 14;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					maxStack = 20;
					toolTip = "Summons the Eye of Cthulhu";
				}
				else if (type == 44)
				{
					useStyle = 5;
					useAnimation = 25;
					useTime = 25;
					name = "Demon Bow";
					width = 12;
					height = 28;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 14;
					shootSpeed = 6.7f;
					knockBack = 1f;
					alpha = 30;
					rare = 1;
					noMelee = true;
					value = 18000;
					ranged = true;
				}
				else if (type == 45)
				{
					name = "War Axe of the Night";
					autoReuse = true;
					useStyle = 1;
					useAnimation = 30;
					knockBack = 6f;
					useTime = 15;
					width = 24;
					height = 28;
					damage = 20;
					axe = 15;
					scale = 1.2f;
					useSound = 1;
					rare = 1;
					value = 13500;
					melee = true;
				}
				else if (type == 46)
				{
					name = "Light's Bane";
					useStyle = 1;
					useAnimation = 20;
					knockBack = 5f;
					width = 24;
					height = 28;
					damage = 17;
					scale = 1.1f;
					useSound = 1;
					rare = 1;
					value = 13500;
					melee = true;
				}
				else if (type == 47)
				{
					name = "Unholy Arrow";
					shootSpeed = 3.4f;
					shoot = 4;
					damage = 8;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 3f;
					alpha = 30;
					rare = 1;
					value = 40;
					ranged = true;
				}
				else if (type == 48)
				{
					name = "Chest";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 21;
					width = 26;
					height = 22;
					value = 500;
				}
				else if (type == 49)
				{
					name = "Band of Regeneration";
					width = 22;
					height = 22;
					accessory = true;
					lifeRegen = 1;
					rare = 1;
					toolTip = "Slowly regenerates life";
					value = 50000;
				}
				else if (type == 50)
				{
					mana = 20;
					name = "Magic Mirror";
					useTurn = true;
					width = 20;
					height = 20;
					useStyle = 4;
					useTime = 90;
					useSound = 6;
					useAnimation = 90;
					toolTip = "Gaze in the mirror to return home";
					rare = 1;
					value = 50000;
				}
				else if (type == 51)
				{
					name = "Jester's Arrow";
					shootSpeed = 0.5f;
					shoot = 5;
					damage = 9;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 4f;
					rare = 1;
					value = 100;
					ranged = true;
				}
				else if (type == 52)
				{
					name = "Angel Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 1;
				}
				else if (type == 53)
				{
					name = "Cloud in a Bottle";
					width = 16;
					height = 24;
					accessory = true;
					rare = 1;
					toolTip = "Allows the holder to double jump";
					value = 50000;
				}
				else if (type == 54)
				{
					name = "Hermes Boots";
					width = 28;
					height = 24;
					accessory = true;
					rare = 1;
					toolTip = "The wearer can run super fast";
					value = 50000;
				}
				else if (type == 55)
				{
					noMelee = true;
					useStyle = 1;
					name = "Enchanted Boomerang";
					shootSpeed = 10f;
					shoot = 6;
					damage = 13;
					knockBack = 8f;
					width = 14;
					height = 28;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					rare = 1;
					value = 50000;
					melee = true;
				}
				else if (type == 56)
				{
					name = "Demonite Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 22;
					width = 12;
					height = 12;
					rare = 1;
					toolTip = "'Pulsing with dark energy'";
					value = 4000;
				}
				else if (type == 57)
				{
					name = "Demonite Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					rare = 1;
					toolTip = "'Pulsing with dark energy'";
					value = 16000;
				}
				else if (type == 58)
				{
					name = "Heart";
					width = 12;
					height = 12;
				}
				else if (type == 59)
				{
					name = "Corrupt Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 23;
					width = 14;
					height = 14;
					value = 500;
				}
				else if (type == 60)
				{
					name = "Vile Mushroom";
					width = 16;
					height = 18;
					maxStack = 99;
					value = 50;
				}
				else if (type == 61)
				{
					name = "Ebonstone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 25;
					width = 12;
					height = 12;
				}
				else if (type == 62)
				{
					name = "Grass Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 2;
					width = 14;
					height = 14;
					value = 20;
				}
				else if (type == 63)
				{
					name = "Sunflower";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 27;
					width = 26;
					height = 26;
					value = 200;
				}
				else if (type == 64)
				{
					mana = 12;
					damage = 8;
					useStyle = 1;
					name = "Vilethorn";
					shootSpeed = 32f;
					shoot = 7;
					width = 26;
					height = 28;
					useSound = 8;
					useAnimation = 30;
					useTime = 30;
					rare = 1;
					noMelee = true;
					knockBack = 1f;
					toolTip = "Summons a vile thorn";
					value = 10000;
					magic = true;
				}
				else if (type == 65)
				{
					autoReuse = true;
					mana = 16;
					knockBack = 5f;
					alpha = 100;
					color = new Color(150, 150, 150, 0);
					damage = 16;
					useStyle = 1;
					scale = 1.15f;
					name = "Starfury";
					shootSpeed = 12f;
					shoot = 9;
					width = 14;
					height = 28;
					useSound = 9;
					useAnimation = 25;
					useTime = 10;
					rare = 1;
					toolTip = "Causes stars to rain from the sky";
					toolTip2 = "'Forged with the fury of heaven'";
					value = 50000;
					magic = true;
				}
				else if (type == 66)
				{
					useStyle = 1;
					name = "Purification Powder";
					shootSpeed = 4f;
					shoot = 10;
					width = 16;
					height = 24;
					maxStack = 99;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noMelee = true;
					toolTip = "Cleanses the corruption";
					value = 75;
				}
				else if (type == 67)
				{
					damage = 0;
					useStyle = 1;
					name = "Vile Powder";
					shootSpeed = 4f;
					shoot = 11;
					width = 16;
					height = 24;
					maxStack = 99;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noMelee = true;
					value = 100;
					toolTip = "Removes the Hallow";
				}
				else if (type == 68)
				{
					name = "Rotten Chunk";
					width = 18;
					height = 20;
					maxStack = 99;
					toolTip = "'Looks tasty!'";
					value = 10;
				}
				else if (type == 69)
				{
					name = "Worm Tooth";
					width = 8;
					height = 20;
					maxStack = 99;
					value = 100;
				}
				else if (type == 70)
				{
					useStyle = 4;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					name = "Worm Food";
					width = 28;
					height = 28;
					maxStack = 20;
					toolTip = "Summons the Eater of Worlds";
				}
				else if (type == 71)
				{
					name = "Copper Coin";
					width = 10;
					height = 12;
					maxStack = 100;
					value = 5;
				}
				else if (type == 72)
				{
					name = "Silver Coin";
					width = 10;
					height = 12;
					maxStack = 100;
					value = 500;
				}
				else if (type == 73)
				{
					name = "Gold Coin";
					width = 10;
					height = 12;
					maxStack = 100;
					value = 50000;
				}
				else if (type == 74)
				{
					name = "Platinum Coin";
					width = 10;
					height = 12;
					maxStack = 100;
					value = 5000000;
				}
				else if (type == 75)
				{
					name = "Fallen Star";
					width = 18;
					height = 20;
					maxStack = 100;
					alpha = 75;
					ammo = 15;
					toolTip = "Disappears after the sunrise";
					value = 500;
					useStyle = 4;
					useSound = 4;
					useTurn = false;
					useAnimation = 17;
					useTime = 17;
					consumable = true;
					rare = 1;
				}
				else if (type == 76)
				{
					name = "Copper Greaves";
					width = 18;
					height = 18;
					defense = 1;
					legSlot = 1;
					value = 750;
				}
				else if (type == 77)
				{
					name = "Iron Greaves";
					width = 18;
					height = 18;
					defense = 2;
					legSlot = 2;
					value = 3000;
				}
				else if (type == 78)
				{
					name = "Silver Greaves";
					width = 18;
					height = 18;
					defense = 3;
					legSlot = 3;
					value = 7500;
				}
				else if (type == 79)
				{
					name = "Gold Greaves";
					width = 18;
					height = 18;
					defense = 4;
					legSlot = 4;
					value = 15000;
				}
				else if (type == 80)
				{
					name = "Copper Chainmail";
					width = 18;
					height = 18;
					defense = 2;
					bodySlot = 1;
					value = 1000;
				}
				else if (type == 81)
				{
					name = "Iron Chainmail";
					width = 18;
					height = 18;
					defense = 3;
					bodySlot = 2;
					value = 4000;
				}
				else if (type == 82)
				{
					name = "Silver Chainmail";
					width = 18;
					height = 18;
					defense = 4;
					bodySlot = 3;
					value = 10000;
				}
				else if (type == 83)
				{
					name = "Gold Chainmail";
					width = 18;
					height = 18;
					defense = 5;
					bodySlot = 4;
					value = 20000;
				}
				else if (type == 84)
				{
					noUseGraphic = true;
					damage = 0;
					knockBack = 7f;
					useStyle = 5;
					name = "Grappling Hook";
					shootSpeed = 11f;
					shoot = 13;
					width = 18;
					height = 28;
					useSound = 1;
					useAnimation = 20;
					useTime = 20;
					rare = 1;
					noMelee = true;
					value = 20000;
					toolTip = "'Get over here!'";
				}
				else if (type == 85)
				{
					name = "Iron Chain";
					width = 14;
					height = 20;
					maxStack = 99;
					value = 1000;
				}
				else if (type == 86)
				{
					name = "Shadow Scale";
					width = 14;
					height = 18;
					maxStack = 99;
					rare = 1;
					value = 500;
				}
				else if (type == 87)
				{
					name = "Piggy Bank";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 29;
					width = 20;
					height = 12;
					value = 10000;
				}
				else if (type == 88)
				{
					name = "Mining Helmet";
					width = 22;
					height = 16;
					defense = 1;
					headSlot = 11;
					rare = 1;
					value = 80000;
					toolTip = "Provides light when worn";
				}
				else if (type == 89)
				{
					name = "Copper Helmet";
					width = 18;
					height = 18;
					defense = 1;
					headSlot = 1;
					value = 1250;
				}
				else if (type == 90)
				{
					name = "Iron Helmet";
					width = 18;
					height = 18;
					defense = 2;
					headSlot = 2;
					value = 5000;
				}
				else if (type == 91)
				{
					name = "Silver Helmet";
					width = 18;
					height = 18;
					defense = 3;
					headSlot = 3;
					value = 12500;
				}
				else if (type == 92)
				{
					name = "Gold Helmet";
					width = 18;
					height = 18;
					defense = 4;
					headSlot = 4;
					value = 25000;
				}
				else if (type == 93)
				{
					name = "Wood Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 4;
					width = 12;
					height = 12;
				}
				else if (type == 94)
				{
					name = "Wood Platform";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 19;
					width = 8;
					height = 10;
				}
				else if (type == 95)
				{
					useStyle = 5;
					useAnimation = 16;
					useTime = 16;
					name = "Flintlock Pistol";
					width = 24;
					height = 28;
					shoot = 14;
					useAmmo = 14;
					useSound = 11;
					damage = 10;
					shootSpeed = 5f;
					noMelee = true;
					value = 50000;
					scale = 0.9f;
					rare = 1;
					ranged = true;
				}
				else if (type == 96)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 43;
					useTime = 43;
					name = "Musket";
					width = 44;
					height = 14;
					shoot = 10;
					useAmmo = 14;
					useSound = 11;
					damage = 23;
					shootSpeed = 8f;
					noMelee = true;
					value = 100000;
					knockBack = 4f;
					rare = 1;
					ranged = true;
				}
				else if (type == 97)
				{
					name = "Musket Ball";
					shootSpeed = 4f;
					shoot = 14;
					damage = 7;
					width = 8;
					height = 8;
					maxStack = 250;
					consumable = true;
					ammo = 14;
					knockBack = 2f;
					value = 7;
					ranged = true;
				}
				else if (type == 98)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 8;
					useTime = 8;
					name = "Minishark";
					width = 50;
					height = 18;
					shoot = 10;
					useAmmo = 14;
					useSound = 11;
					damage = 6;
					shootSpeed = 7f;
					noMelee = true;
					value = 350000;
					rare = 2;
					toolTip = "33% chance to not consume ammo";
					toolTip2 = "'Half shark, half gun, completely awesome.'";
					ranged = true;
				}
				else if (type == 99)
				{
					useStyle = 5;
					useAnimation = 28;
					useTime = 28;
					name = "Iron Bow";
					width = 12;
					height = 28;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 8;
					shootSpeed = 6.6f;
					noMelee = true;
					value = 1400;
					ranged = true;
				}
				else if (type == 100)
				{
					name = "Shadow Greaves";
					width = 18;
					height = 18;
					defense = 6;
					legSlot = 5;
					rare = 1;
					value = 22500;
					toolTip = "7% increased melee speed";
				}
				else if (type == 101)
				{
					name = "Shadow Scalemail";
					width = 18;
					height = 18;
					defense = 7;
					bodySlot = 5;
					rare = 1;
					value = 30000;
					toolTip = "7% increased melee speed";
				}
				else if (type == 102)
				{
					name = "Shadow Helmet";
					width = 18;
					height = 18;
					defense = 6;
					headSlot = 5;
					rare = 1;
					value = 37500;
					toolTip = "7% increased melee speed";
				}
				else if (type == 103)
				{
					name = "Nightmare Pickaxe";
					useStyle = 1;
					useTurn = true;
					useAnimation = 20;
					useTime = 15;
					autoReuse = true;
					width = 24;
					height = 28;
					damage = 9;
					pick = 65;
					useSound = 1;
					knockBack = 3f;
					rare = 1;
					value = 18000;
					scale = 1.15f;
					toolTip = "Able to mine Hellstone";
					melee = true;
				}
				else if (type == 104)
				{
					name = "The Breaker";
					autoReuse = true;
					useStyle = 1;
					useAnimation = 45;
					useTime = 19;
					hammer = 55;
					width = 24;
					height = 28;
					damage = 24;
					knockBack = 6f;
					scale = 1.3f;
					useSound = 1;
					rare = 1;
					value = 15000;
					melee = true;
				}
				else if (type == 105)
				{
					noWet = true;
					name = "Candle";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 33;
					width = 8;
					height = 18;
					holdStyle = 1;
				}
				else if (type == 106)
				{
					name = "Copper Chandelier";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 34;
					width = 26;
					height = 26;
				}
				else if (type == 107)
				{
					name = "Silver Chandelier";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 35;
					width = 26;
					height = 26;
				}
				else if (type == 108)
				{
					name = "Gold Chandelier";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 36;
					width = 26;
					height = 26;
				}
				else if (type == 109)
				{
					name = "Mana Crystal";
					maxStack = 99;
					consumable = true;
					width = 18;
					height = 18;
					useStyle = 4;
					useTime = 30;
					useSound = 29;
					useAnimation = 30;
					toolTip = "Permanently increases maximum mana by 20";
					rare = 2;
				}
				else if (type == 110)
				{
					name = "Lesser Mana Potion";
					useSound = 3;
					healMana = 50;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 20;
					consumable = true;
					width = 14;
					height = 24;
					value = 100;
				}
				else if (type == 111)
				{
					name = "Band of Starpower";
					width = 22;
					height = 22;
					accessory = true;
					rare = 1;
					toolTip = "Increases maximum mana by 20";
					value = 50000;
				}
				else if (type == 112)
				{
					mana = 17;
					damage = 44;
					useStyle = 1;
					name = "Flower of Fire";
					shootSpeed = 6f;
					shoot = 15;
					width = 26;
					height = 28;
					useSound = 20;
					useAnimation = 20;
					useTime = 20;
					rare = 3;
					noMelee = true;
					knockBack = 5.5f;
					toolTip = "Throws balls of fire";
					value = 10000;
					magic = true;
				}
				else if (type == 113)
				{
					mana = 10;
					channel = true;
					damage = 22;
					useStyle = 1;
					name = "Magic Missile";
					shootSpeed = 6f;
					shoot = 16;
					width = 26;
					height = 28;
					useSound = 9;
					useAnimation = 17;
					useTime = 17;
					rare = 2;
					noMelee = true;
					knockBack = 5f;
					toolTip = "Casts a controllable missile";
					value = 10000;
					magic = true;
				}
				else if (type == 114)
				{
					mana = 5;
					channel = true;
					damage = 0;
					useStyle = 1;
					name = "Dirt Rod";
					shoot = 17;
					width = 26;
					height = 28;
					useSound = 8;
					useAnimation = 20;
					useTime = 20;
					rare = 1;
					noMelee = true;
					knockBack = 5f;
					toolTip = "Magically moves dirt";
					value = 200000;
				}
				else if (type == 115)
				{
					mana = 40;
					channel = true;
					damage = 0;
					useStyle = 4;
					name = "Orb of Light";
					shoot = 18;
					width = 24;
					height = 24;
					useSound = 8;
					useAnimation = 20;
					useTime = 20;
					rare = 1;
					noMelee = true;
					toolTip = "Creates a magical orb of light";
					value = 10000;
					buffType = 19;
					buffTime = 18000;
				}
				else if (type == 116)
				{
					name = "Meteorite";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 37;
					width = 12;
					height = 12;
					value = 1000;
				}
				else if (type == 117)
				{
					name = "Meteorite Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					rare = 1;
					toolTip = "'Warm to the touch'";
					value = 7000;
				}
				else if (type == 118)
				{
					name = "Hook";
					maxStack = 99;
					width = 18;
					height = 18;
					value = 1000;
					toolTip = "Sometimes dropped by Skeletons and Piranha";
				}
				else if (type == 119)
				{
					noMelee = true;
					useStyle = 1;
					name = "Flamarang";
					shootSpeed = 11f;
					shoot = 19;
					damage = 32;
					knockBack = 8f;
					width = 14;
					height = 28;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					rare = 3;
					value = 100000;
					melee = true;
				}
				else if (type == 120)
				{
					useStyle = 5;
					useAnimation = 25;
					useTime = 25;
					name = "Molten Fury";
					width = 14;
					height = 32;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 29;
					shootSpeed = 8f;
					knockBack = 2f;
					alpha = 30;
					rare = 3;
					noMelee = true;
					scale = 1.1f;
					value = 27000;
					toolTip = "Lights wooden arrows ablaze";
					ranged = true;
				}
				else if (type == 121)
				{
					name = "Fiery Greatsword";
					useStyle = 1;
					useAnimation = 34;
					knockBack = 6.5f;
					width = 24;
					height = 28;
					damage = 36;
					scale = 1.3f;
					useSound = 1;
					rare = 3;
					value = 27000;
					toolTip = "'It's made out of fire!'";
					melee = true;
				}
				if (type == 122)
				{
					name = "Molten Pickaxe";
					useStyle = 1;
					useTurn = true;
					useAnimation = 25;
					useTime = 25;
					autoReuse = true;
					width = 24;
					height = 28;
					damage = 12;
					pick = 100;
					scale = 1.15f;
					useSound = 1;
					knockBack = 2f;
					rare = 3;
					value = 27000;
					melee = true;
				}
				else if (type == 123)
				{
					name = "Meteor Helmet";
					width = 18;
					height = 18;
					defense = 3;
					headSlot = 6;
					rare = 1;
					value = 45000;
					toolTip = "5% increased magic damage";
				}
				else if (type == 124)
				{
					name = "Meteor Suit";
					width = 18;
					height = 18;
					defense = 4;
					bodySlot = 6;
					rare = 1;
					value = 30000;
					toolTip = "5% increased magic damage";
				}
				else if (type == 125)
				{
					name = "Meteor Leggings";
					width = 18;
					height = 18;
					defense = 3;
					legSlot = 6;
					rare = 1;
					value = 30000;
					toolTip = "5% increased magic damage";
				}
				else if (type == 126)
				{
					name = "Bottled Water";
					useSound = 3;
					healLife = 20;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					potion = true;
					value = 20;
				}
				else if (type == 127)
				{
					autoReuse = true;
					useStyle = 5;
					useAnimation = 19;
					useTime = 19;
					name = "Space Gun";
					width = 24;
					height = 28;
					shoot = 20;
					mana = 8;
					useSound = 12;
					knockBack = 0.5f;
					damage = 17;
					shootSpeed = 10f;
					noMelee = true;
					scale = 0.8f;
					rare = 1;
					magic = true;
					value = 20000;
				}
				else if (type == 128)
				{
					name = "Rocket Boots";
					width = 28;
					height = 24;
					accessory = true;
					rare = 3;
					toolTip = "Allows flight";
					value = 50000;
				}
				else if (type == 129)
				{
					name = "Gray Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 38;
					width = 12;
					height = 12;
				}
				else if (type == 130)
				{
					name = "Gray Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 5;
					width = 12;
					height = 12;
				}
				else if (type == 131)
				{
					name = "Red Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 39;
					width = 12;
					height = 12;
				}
				else if (type == 132)
				{
					name = "Red Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 6;
					width = 12;
					height = 12;
				}
				else if (type == 133)
				{
					name = "Clay Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 40;
					width = 12;
					height = 12;
				}
				else if (type == 134)
				{
					name = "Blue Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 41;
					width = 12;
					height = 12;
				}
				else if (type == 135)
				{
					name = "Blue Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 17;
					width = 12;
					height = 12;
				}
				else if (type == 136)
				{
					name = "Chain Lantern";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 42;
					width = 12;
					height = 28;
				}
				else if (type == 137)
				{
					name = "Green Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 43;
					width = 12;
					height = 12;
				}
				else if (type == 138)
				{
					name = "Green Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 18;
					width = 12;
					height = 12;
				}
				else if (type == 139)
				{
					name = "Pink Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 44;
					width = 12;
					height = 12;
				}
				else if (type == 140)
				{
					name = "Pink Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 19;
					width = 12;
					height = 12;
				}
				else if (type == 141)
				{
					name = "Gold Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 45;
					width = 12;
					height = 12;
				}
				else if (type == 142)
				{
					name = "Gold Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 10;
					width = 12;
					height = 12;
				}
				else if (type == 143)
				{
					name = "Silver Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 46;
					width = 12;
					height = 12;
				}
				else if (type == 144)
				{
					name = "Silver Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 11;
					width = 12;
					height = 12;
				}
				else if (type == 145)
				{
					name = "Copper Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 47;
					width = 12;
					height = 12;
				}
				else if (type == 146)
				{
					name = "Copper Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 12;
					width = 12;
					height = 12;
				}
				else if (type == 147)
				{
					name = "Spike";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 48;
					width = 12;
					height = 12;
				}
				else if (type == 148)
				{
					noWet = true;
					name = "Water Candle";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 49;
					width = 8;
					height = 18;
					holdStyle = 1;
					toolTip = "Holding this may attract unwanted attention";
				}
				else if (type == 149)
				{
					name = "Book";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 50;
					width = 24;
					height = 28;
					toolTip = "'It contains strange symbols'";
				}
				else if (type == 150)
				{
					name = "Cobweb";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 51;
					width = 20;
					height = 24;
					alpha = 100;
				}
				else if (type == 151)
				{
					name = "Necro Helmet";
					width = 18;
					height = 18;
					defense = 5;
					headSlot = 7;
					rare = 2;
					value = 45000;
					toolTip = "4% increased ranged damage.";
				}
				else if (type == 152)
				{
					name = "Necro Breastplate";
					width = 18;
					height = 18;
					defense = 6;
					bodySlot = 7;
					rare = 2;
					value = 30000;
					toolTip = "4% increased ranged damage.";
				}
				else if (type == 153)
				{
					name = "Necro Greaves";
					width = 18;
					height = 18;
					defense = 5;
					legSlot = 7;
					rare = 2;
					value = 30000;
					toolTip = "4% increased ranged damage.";
				}
				else if (type == 154)
				{
					name = "Bone";
					maxStack = 99;
					consumable = true;
					width = 12;
					height = 14;
					value = 50;
					useAnimation = 12;
					useTime = 12;
					useStyle = 1;
					useSound = 1;
					shootSpeed = 8f;
					noUseGraphic = true;
					damage = 22;
					knockBack = 4f;
					shoot = 21;
					ranged = true;
				}
				else if (type == 155)
				{
					autoReuse = true;
					useTurn = true;
					name = "Muramasa";
					useStyle = 1;
					useAnimation = 20;
					width = 40;
					height = 40;
					damage = 18;
					scale = 1.1f;
					useSound = 1;
					rare = 2;
					value = 27000;
					knockBack = 1f;
					melee = true;
				}
				else if (type == 156)
				{
					name = "Cobalt Shield";
					width = 24;
					height = 28;
					rare = 2;
					value = 27000;
					accessory = true;
					defense = 1;
					toolTip = "Grants immunity to knockback";
				}
				else if (type == 157)
				{
					mana = 7;
					autoReuse = true;
					name = "Aqua Scepter";
					useStyle = 5;
					useAnimation = 16;
					useTime = 8;
					knockBack = 5f;
					width = 38;
					height = 10;
					damage = 14;
					scale = 1f;
					shoot = 22;
					shootSpeed = 11f;
					useSound = 13;
					rare = 2;
					value = 27000;
					toolTip = "Sprays out a shower of water";
					magic = true;
				}
				else if (type == 158)
				{
					name = "Lucky Horseshoe";
					width = 20;
					height = 22;
					rare = 1;
					value = 27000;
					accessory = true;
					toolTip = "Negates fall damage";
				}
				else if (type == 159)
				{
					name = "Shiny Red Balloon";
					width = 14;
					height = 28;
					rare = 1;
					value = 27000;
					accessory = true;
					toolTip = "Increases jump height";
				}
				else if (type == 160)
				{
					autoReuse = true;
					name = "Harpoon";
					useStyle = 5;
					useAnimation = 30;
					useTime = 30;
					knockBack = 6f;
					width = 30;
					height = 10;
					damage = 25;
					scale = 1.1f;
					shoot = 23;
					shootSpeed = 11f;
					useSound = 10;
					rare = 2;
					value = 27000;
					ranged = true;
				}
				else if (type == 161)
				{
					useStyle = 1;
					name = "Spiky Ball";
					shootSpeed = 5f;
					shoot = 24;
					knockBack = 1f;
					damage = 15;
					width = 10;
					height = 10;
					maxStack = 250;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 80;
					ranged = true;
				}
				else if (type == 162)
				{
					name = "Ball O' Hurt";
					useStyle = 5;
					useAnimation = 45;
					useTime = 45;
					knockBack = 6.5f;
					width = 30;
					height = 10;
					damage = 15;
					scale = 1.1f;
					noUseGraphic = true;
					shoot = 25;
					shootSpeed = 12f;
					useSound = 1;
					rare = 1;
					value = 27000;
					melee = true;
					channel = true;
					noMelee = true;
				}
				else if (type == 163)
				{
					name = "Blue Moon";
					useStyle = 5;
					useAnimation = 45;
					useTime = 45;
					knockBack = 7f;
					width = 30;
					height = 10;
					damage = 23;
					scale = 1.1f;
					noUseGraphic = true;
					shoot = 26;
					shootSpeed = 12f;
					useSound = 1;
					rare = 2;
					value = 27000;
					melee = true;
					channel = true;
				}
				else if (type == 164)
				{
					autoReuse = false;
					useStyle = 5;
					useAnimation = 12;
					useTime = 12;
					name = "Handgun";
					width = 24;
					height = 24;
					shoot = 14;
					knockBack = 3f;
					useAmmo = 14;
					useSound = 11;
					damage = 14;
					shootSpeed = 10f;
					noMelee = true;
					value = 50000;
					scale = 0.75f;
					rare = 2;
					ranged = true;
				}
				else if (type == 165)
				{
					autoReuse = true;
					rare = 2;
					mana = 14;
					useSound = 21;
					name = "Water Bolt";
					useStyle = 5;
					damage = 17;
					useAnimation = 17;
					useTime = 17;
					width = 24;
					height = 28;
					shoot = 27;
					scale = 0.9f;
					shootSpeed = 4.5f;
					knockBack = 5f;
					toolTip = "Casts a slow moving bolt of water";
					magic = true;
					value = 50000;
				}
				else if (type == 166)
				{
					useStyle = 1;
					name = "Bomb";
					shootSpeed = 5f;
					shoot = 28;
					width = 20;
					height = 20;
					maxStack = 50;
					consumable = true;
					useSound = 1;
					useAnimation = 25;
					useTime = 25;
					noUseGraphic = true;
					noMelee = true;
					value = 500;
					damage = 0;
					toolTip = "A small explosion that will destroy some tiles";
				}
				else if (type == 167)
				{
					useStyle = 1;
					name = "Dynamite";
					shootSpeed = 4f;
					shoot = 29;
					width = 8;
					height = 28;
					maxStack = 5;
					consumable = true;
					useSound = 1;
					useAnimation = 40;
					useTime = 40;
					noUseGraphic = true;
					noMelee = true;
					value = 5000;
					rare = 1;
					toolTip = "A large explosion that will destroy most tiles";
				}
				else if (type == 168)
				{
					useStyle = 5;
					name = "Grenade";
					shootSpeed = 5.5f;
					shoot = 30;
					width = 20;
					height = 20;
					maxStack = 99;
					consumable = true;
					useSound = 1;
					useAnimation = 45;
					useTime = 45;
					noUseGraphic = true;
					noMelee = true;
					value = 400;
					damage = 60;
					knockBack = 8f;
					toolTip = "A small explosion that will not destroy tiles";
					ranged = true;
				}
				else if (type == 169)
				{
					name = "Sand Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 53;
					width = 12;
					height = 12;
					ammo = 42;
				}
				else if (type == 170)
				{
					name = "Glass";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 54;
					width = 12;
					height = 12;
				}
				else if (type == 171)
				{
					name = "Sign";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 55;
					width = 28;
					height = 28;
				}
				else if (type == 172)
				{
					name = "Ash Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 57;
					width = 12;
					height = 12;
				}
				else if (type == 173)
				{
					name = "Obsidian";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 56;
					width = 12;
					height = 12;
				}
				else if (type == 174)
				{
					name = "Hellstone";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 58;
					width = 12;
					height = 12;
					rare = 2;
				}
				else if (type == 175)
				{
					name = "Hellstone Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					rare = 2;
					toolTip = "'Hot to the touch'";
					value = 20000;
				}
				else if (type == 176)
				{
					name = "Mud Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 59;
					width = 12;
					height = 12;
				}
				else if (type == 181)
				{
					name = "Amethyst";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 1875;
				}
				else if (type == 180)
				{
					name = "Topaz";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 3750;
				}
				else if (type == 177)
				{
					name = "Sapphire";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 5625;
				}
				else if (type == 179)
				{
					name = "Emerald";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 7500;
				}
				else if (type == 178)
				{
					name = "Ruby";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 11250;
				}
				else if (type == 182)
				{
					name = "Diamond";
					maxStack = 99;
					alpha = 50;
					width = 10;
					height = 14;
					value = 15000;
				}
				else if (type == 183)
				{
					name = "Glowing Mushroom";
					useStyle = 2;
					useSound = 2;
					useTurn = false;
					useAnimation = 17;
					useTime = 17;
					width = 16;
					height = 18;
					healLife = 25;
					maxStack = 99;
					consumable = true;
					potion = true;
					value = 50;
				}
				else if (type == 184)
				{
					name = "Star";
					width = 12;
					height = 12;
				}
				else if (type == 185)
				{
					noUseGraphic = true;
					damage = 0;
					knockBack = 7f;
					useStyle = 5;
					name = "Ivy Whip";
					shootSpeed = 13f;
					shoot = 32;
					width = 18;
					height = 28;
					useSound = 1;
					useAnimation = 20;
					useTime = 20;
					rare = 3;
					noMelee = true;
					value = 20000;
				}
				else if (type == 186)
				{
					name = "Breathing Reed";
					width = 44;
					height = 44;
					rare = 1;
					value = 10000;
					holdStyle = 2;
					toolTip = "'Because not drowning is kinda nice'";
				}
				else if (type == 187)
				{
					name = "Flipper";
					width = 28;
					height = 28;
					rare = 1;
					value = 10000;
					accessory = true;
					toolTip = "Grants the ability to swim";
				}
				else if (type == 188)
				{
					name = "Healing Potion";
					useSound = 3;
					healLife = 100;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					rare = 1;
					potion = true;
					value = 1000;
				}
				else if (type == 189)
				{
					name = "Mana Potion";
					useSound = 3;
					healMana = 100;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 50;
					consumable = true;
					width = 14;
					height = 24;
					rare = 1;
					value = 250;
				}
				else if (type == 190)
				{
					name = "Blade of Grass";
					useStyle = 1;
					useAnimation = 30;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 28;
					scale = 1.4f;
					useSound = 1;
					rare = 3;
					value = 27000;
					melee = true;
				}
				else if (type == 191)
				{
					noMelee = true;
					useStyle = 1;
					name = "Thorn Chakram";
					shootSpeed = 11f;
					shoot = 33;
					damage = 25;
					knockBack = 8f;
					width = 14;
					height = 28;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					rare = 3;
					value = 50000;
					melee = true;
				}
				else if (type == 192)
				{
					name = "Obsidian Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 75;
					width = 12;
					height = 12;
				}
				else if (type == 193)
				{
					name = "Obsidian Skull";
					width = 20;
					height = 22;
					rare = 2;
					value = 27000;
					accessory = true;
					defense = 1;
					toolTip = "Grants immunity to fire blocks";
				}
				else if (type == 194)
				{
					name = "Mushroom Grass Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 70;
					width = 14;
					height = 14;
					value = 150;
				}
				else if (type == 195)
				{
					name = "Jungle Grass Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 60;
					width = 14;
					height = 14;
					value = 150;
				}
				else if (type == 196)
				{
					name = "Wooden Hammer";
					autoReuse = true;
					useStyle = 1;
					useTurn = true;
					useAnimation = 37;
					useTime = 25;
					hammer = 25;
					width = 24;
					height = 28;
					damage = 2;
					knockBack = 5.5f;
					scale = 1.2f;
					useSound = 1;
					tileBoost = -1;
					value = 50;
					melee = true;
				}
				else if (type == 197)
				{
					autoReuse = true;
					useStyle = 5;
					useAnimation = 12;
					useTime = 12;
					name = "Star Cannon";
					width = 50;
					height = 18;
					shoot = 12;
					useAmmo = 15;
					useSound = 9;
					damage = 55;
					shootSpeed = 14f;
					noMelee = true;
					value = 500000;
					rare = 2;
					toolTip = "Shoots fallen stars";
					ranged = true;
				}
				else if (type == 198)
				{
					name = "Blue Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 199)
				{
					name = "Red Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 200)
				{
					name = "Green Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 201)
				{
					name = "Purple Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 202)
				{
					name = "White Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 203)
				{
					name = "Yellow Phaseblade";
					useStyle = 1;
					useAnimation = 25;
					knockBack = 3f;
					width = 40;
					height = 40;
					damage = 21;
					scale = 1f;
					useSound = 15;
					rare = 1;
					value = 27000;
					melee = true;
				}
				else if (type == 204)
				{
					name = "Meteor Hamaxe";
					useTurn = true;
					autoReuse = true;
					useStyle = 1;
					useAnimation = 30;
					useTime = 16;
					hammer = 60;
					axe = 20;
					width = 24;
					height = 28;
					damage = 20;
					knockBack = 7f;
					scale = 1.2f;
					useSound = 1;
					rare = 1;
					value = 15000;
					melee = true;
				}
				else if (type == 205)
				{
					name = "Empty Bucket";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					width = 20;
					height = 20;
					headSlot = 13;
					defense = 1;
				}
				else if (type == 206)
				{
					name = "Water Bucket";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					width = 20;
					height = 20;
				}
				else if (type == 207)
				{
					name = "Lava Bucket";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					width = 20;
					height = 20;
				}
				else if (type == 208)
				{
					name = "Jungle Rose";
					width = 20;
					height = 20;
					value = 100;
					headSlot = 23;
					toolTip = "'It's pretty, oh so pretty'";
					vanity = true;
				}
				else if (type == 209)
				{
					name = "Stinger";
					width = 16;
					height = 18;
					maxStack = 99;
					value = 200;
				}
				else if (type == 210)
				{
					name = "Vine";
					width = 14;
					height = 20;
					maxStack = 99;
					value = 1000;
				}
				else if (type == 211)
				{
					name = "Feral Claws";
					width = 20;
					height = 20;
					accessory = true;
					rare = 3;
					toolTip = "12% increased melee speed";
					value = 50000;
				}
				else if (type == 212)
				{
					name = "Anklet of the Wind";
					width = 20;
					height = 20;
					accessory = true;
					rare = 3;
					toolTip = "10% increased movement speed";
					value = 50000;
				}
				else if (type == 213)
				{
					name = "Staff of Regrowth";
					useStyle = 1;
					useTurn = true;
					useAnimation = 25;
					useTime = 13;
					autoReuse = true;
					width = 24;
					height = 28;
					damage = 7;
					createTile = 2;
					scale = 1.2f;
					useSound = 1;
					knockBack = 3f;
					rare = 3;
					value = 2000;
					toolTip = "Creates grass on dirt";
					melee = true;
				}
				else if (type == 214)
				{
					name = "Hellstone Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 76;
					width = 12;
					height = 12;
				}
				else if (type == 215)
				{
					name = "Whoopie Cushion";
					width = 18;
					height = 18;
					useTurn = true;
					useTime = 30;
					useAnimation = 30;
					noUseGraphic = true;
					useStyle = 10;
					useSound = 16;
					rare = 2;
					toolTip = "'May annoy others'";
					value = 100;
				}
				else if (type == 216)
				{
					name = "Shackle";
					width = 20;
					height = 20;
					rare = 1;
					value = 1500;
					accessory = true;
					defense = 1;
				}
				else if (type == 217)
				{
					name = "Molten Hamaxe";
					useTurn = true;
					autoReuse = true;
					useStyle = 1;
					useAnimation = 27;
					useTime = 14;
					hammer = 70;
					axe = 30;
					width = 24;
					height = 28;
					damage = 20;
					knockBack = 7f;
					scale = 1.4f;
					useSound = 1;
					rare = 3;
					value = 15000;
					melee = true;
				}
				else if (type == 218)
				{
					mana = 16;
					channel = true;
					damage = 34;
					useStyle = 1;
					name = "Flamelash";
					shootSpeed = 6f;
					shoot = 34;
					width = 26;
					height = 28;
					useSound = 20;
					useAnimation = 20;
					useTime = 20;
					rare = 3;
					noMelee = true;
					knockBack = 6.5f;
					toolTip = "Summons a controllable ball of fire";
					value = 10000;
					magic = true;
				}
				else if (type == 219)
				{
					autoReuse = false;
					useStyle = 5;
					useAnimation = 11;
					useTime = 11;
					name = "Phoenix Blaster";
					width = 24;
					height = 22;
					shoot = 14;
					knockBack = 2f;
					useAmmo = 14;
					useSound = 11;
					damage = 23;
					shootSpeed = 13f;
					noMelee = true;
					value = 50000;
					scale = 0.75f;
					rare = 3;
					ranged = true;
				}
				else if (type == 220)
				{
					name = "Sunfury";
					noMelee = true;
					useStyle = 5;
					useAnimation = 45;
					useTime = 45;
					knockBack = 7f;
					width = 30;
					height = 10;
					damage = 33;
					scale = 1.1f;
					noUseGraphic = true;
					shoot = 35;
					shootSpeed = 12f;
					useSound = 1;
					rare = 3;
					value = 27000;
					melee = true;
					channel = true;
				}
				else if (type == 221)
				{
					name = "Hellforge";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 77;
					width = 26;
					height = 24;
					value = 3000;
				}
				else if (type == 222)
				{
					name = "Clay Pot";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 78;
					width = 14;
					height = 14;
					value = 100;
					toolTip = "Grows plants";
				}
				else if (type == 223)
				{
					name = "Nature's Gift";
					width = 20;
					height = 22;
					rare = 3;
					value = 27000;
					accessory = true;
					toolTip = "6% reduced mana usage";
				}
				else if (type == 224)
				{
					name = "Bed";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 79;
					width = 28;
					height = 20;
					value = 2000;
				}
				else if (type == 225)
				{
					name = "Silk";
					maxStack = 99;
					width = 22;
					height = 22;
					value = 1000;
				}
				else if (type == 226)
				{
					name = "Lesser Restoration Potion";
					useSound = 3;
					healMana = 50;
					healLife = 50;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 20;
					consumable = true;
					width = 14;
					height = 24;
					potion = true;
					value = 2000;
				}
				else if (type == 227)
				{
					name = "Restoration Potion";
					useSound = 3;
					healMana = 100;
					healLife = 100;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 20;
					consumable = true;
					width = 14;
					height = 24;
					potion = true;
					value = 4000;
				}
				else if (type == 228)
				{
					name = "Jungle Hat";
					width = 18;
					height = 18;
					defense = 4;
					headSlot = 8;
					rare = 3;
					value = 45000;
					toolTip = "Increases maximum mana by 20";
					toolTip2 = "3% increased magic critical strike chance";
				}
				else if (type == 229)
				{
					name = "Jungle Shirt";
					width = 18;
					height = 18;
					defense = 5;
					bodySlot = 8;
					rare = 3;
					value = 30000;
					toolTip = "Increases maximum mana by 20";
					toolTip2 = "3% increased magic critical strike chance";
				}
				else if (type == 230)
				{
					name = "Jungle Pants";
					width = 18;
					height = 18;
					defense = 4;
					legSlot = 8;
					rare = 3;
					value = 30000;
					toolTip = "Increases maximum mana by 20";
					toolTip2 = "3% increased magic critical strike chance";
				}
				else if (type == 231)
				{
					name = "Molten Helmet";
					width = 18;
					height = 18;
					defense = 8;
					headSlot = 9;
					rare = 3;
					value = 45000;
				}
				else if (type == 232)
				{
					name = "Molten Breastplate";
					width = 18;
					height = 18;
					defense = 9;
					bodySlot = 9;
					rare = 3;
					value = 30000;
				}
				else if (type == 233)
				{
					name = "Molten Greaves";
					width = 18;
					height = 18;
					defense = 8;
					legSlot = 9;
					rare = 3;
					value = 30000;
				}
				else if (type == 234)
				{
					name = "Meteor Shot";
					shootSpeed = 3f;
					shoot = 36;
					damage = 9;
					width = 8;
					height = 8;
					maxStack = 250;
					consumable = true;
					ammo = 14;
					knockBack = 1f;
					value = 8;
					rare = 1;
					ranged = true;
				}
				else if (type == 235)
				{
					useStyle = 1;
					name = "Sticky Bomb";
					shootSpeed = 5f;
					shoot = 37;
					width = 20;
					height = 20;
					maxStack = 50;
					consumable = true;
					useSound = 1;
					useAnimation = 25;
					useTime = 25;
					noUseGraphic = true;
					noMelee = true;
					value = 500;
					damage = 0;
					toolTip = "'Tossing may be difficult.'";
				}
				else if (type == 236)
				{
					name = "Black Lens";
					width = 12;
					height = 20;
					maxStack = 99;
					value = 5000;
				}
				else if (type == 237)
				{
					name = "Sunglasses";
					width = 28;
					height = 12;
					headSlot = 12;
					rare = 2;
					value = 10000;
					toolTip = "'Makes you look cool!'";
					vanity = true;
				}
				else if (type == 238)
				{
					name = "Wizard Hat";
					width = 28;
					height = 20;
					headSlot = 14;
					rare = 2;
					value = 10000;
					defense = 2;
					toolTip = "15% increased magic damage";
				}
				else if (type == 239)
				{
					name = "Top Hat";
					width = 18;
					height = 18;
					headSlot = 15;
					value = 10000;
					vanity = true;
				}
				else if (type == 240)
				{
					name = "Tuxedo Shirt";
					width = 18;
					height = 18;
					bodySlot = 10;
					value = 5000;
					vanity = true;
				}
				else if (type == 241)
				{
					name = "Tuxedo Pants";
					width = 18;
					height = 18;
					legSlot = 10;
					value = 5000;
					vanity = true;
				}
				else if (type == 242)
				{
					name = "Summer Hat";
					width = 18;
					height = 18;
					headSlot = 16;
					value = 10000;
					vanity = true;
				}
				else if (type == 243)
				{
					name = "Bunny Hood";
					width = 18;
					height = 18;
					headSlot = 17;
					value = 20000;
					vanity = true;
				}
				else if (type == 244)
				{
					name = "Plumber's Hat";
					width = 18;
					height = 12;
					headSlot = 18;
					value = 10000;
					vanity = true;
				}
				else if (type == 245)
				{
					name = "Plumber's Shirt";
					width = 18;
					height = 18;
					bodySlot = 11;
					value = 250000;
					vanity = true;
				}
				else if (type == 246)
				{
					name = "Plumber's Pants";
					width = 18;
					height = 18;
					legSlot = 11;
					value = 250000;
					vanity = true;
				}
				else if (type == 247)
				{
					name = "Hero's Hat";
					width = 18;
					height = 12;
					headSlot = 19;
					value = 10000;
					vanity = true;
				}
				else if (type == 248)
				{
					name = "Hero's Shirt";
					width = 18;
					height = 18;
					bodySlot = 12;
					value = 5000;
					vanity = true;
				}
				else if (type == 249)
				{
					name = "Hero's Pants";
					width = 18;
					height = 18;
					legSlot = 12;
					value = 5000;
					vanity = true;
				}
				else if (type == 250)
				{
					name = "Fish Bowl";
					width = 18;
					height = 18;
					headSlot = 20;
					value = 10000;
					vanity = true;
				}
				else if (type == 251)
				{
					name = "Archaeologist's Hat";
					width = 18;
					height = 12;
					headSlot = 21;
					value = 10000;
					vanity = true;
				}
				else if (type == 252)
				{
					name = "Archaeologist's Jacket";
					width = 18;
					height = 18;
					bodySlot = 13;
					value = 5000;
					vanity = true;
				}
				else if (type == 253)
				{
					name = "Archaeologist's Pants";
					width = 18;
					height = 18;
					legSlot = 13;
					value = 5000;
					vanity = true;
				}
				else if (type == 254)
				{
					name = "Black Dye";
					maxStack = 99;
					width = 12;
					height = 20;
					value = 10000;
				}
				else if (type == 255)
				{
					name = "Green Dye";
					maxStack = 99;
					width = 12;
					height = 20;
					value = 2000;
				}
				else if (type == 256)
				{
					name = "Ninja Hood";
					width = 18;
					height = 12;
					headSlot = 22;
					value = 10000;
					vanity = true;
				}
				else if (type == 257)
				{
					name = "Ninja Shirt";
					width = 18;
					height = 18;
					bodySlot = 14;
					value = 5000;
					vanity = true;
				}
				else if (type == 258)
				{
					name = "Ninja Pants";
					width = 18;
					height = 18;
					legSlot = 14;
					value = 5000;
					vanity = true;
				}
				else if (type == 259)
				{
					name = "Leather";
					width = 18;
					height = 20;
					maxStack = 99;
					value = 50;
				}
				else if (type == 260)
				{
					name = "Red Hat";
					width = 18;
					height = 14;
					headSlot = 24;
					value = 1000;
					vanity = true;
				}
				else if (type == 261)
				{
					name = "Goldfish";
					useStyle = 2;
					useSound = 2;
					useTurn = false;
					useAnimation = 17;
					useTime = 17;
					width = 20;
					height = 10;
					maxStack = 99;
					healLife = 20;
					consumable = true;
					value = 1000;
					potion = true;
					toolTip = "'It's smiling, might be a good snack'";
				}
				else if (type == 262)
				{
					name = "Robe";
					width = 18;
					height = 14;
					bodySlot = 15;
					value = 2000;
					vanity = true;
				}
				else if (type == 263)
				{
					name = "Robot Hat";
					width = 18;
					height = 18;
					headSlot = 25;
					value = 10000;
					vanity = true;
				}
				else if (type == 264)
				{
					name = "Gold Crown";
					width = 18;
					height = 18;
					headSlot = 26;
					value = 10000;
					vanity = true;
				}
				else if (type == 265)
				{
					name = "Hellfire Arrow";
					shootSpeed = 6.5f;
					shoot = 41;
					damage = 10;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 8f;
					value = 100;
					rare = 2;
					ranged = true;
				}
				else if (type == 266)
				{
					useStyle = 5;
					useAnimation = 16;
					useTime = 16;
					autoReuse = true;
					name = "Sandgun";
					width = 40;
					height = 20;
					shoot = 42;
					useAmmo = 42;
					useSound = 11;
					damage = 30;
					shootSpeed = 12f;
					noMelee = true;
					knockBack = 5f;
					value = 10000;
					rare = 2;
					toolTip = "'This is a good idea!'";
					ranged = true;
				}
				else if (type == 267)
				{
					accessory = true;
					name = "Guide Voodoo Doll";
					width = 14;
					height = 26;
					value = 1000;
					toolTip = "'You are a terrible person.'";
				}
				else if (type == 268)
				{
					headSlot = 27;
					defense = 2;
					name = "Diving Helmet";
					width = 20;
					height = 20;
					value = 1000;
					rare = 2;
					toolTip = "Greatly extends underwater breathing";
				}
				else if (type == 269)
				{
					name = "Familiar Shirt";
					bodySlot = 0;
					width = 20;
					height = 20;
					value = 10000;
					color = Main.player[Main.myPlayer].shirtColor;
				}
				else if (type == 270)
				{
					name = "Familiar Pants";
					legSlot = 0;
					width = 20;
					height = 20;
					value = 10000;
					color = Main.player[Main.myPlayer].pantsColor;
				}
				else if (type == 271)
				{
					name = "Familiar Wig";
					headSlot = 0;
					width = 20;
					height = 20;
					value = 10000;
					color = Main.player[Main.myPlayer].hairColor;
				}
				else if (type == 272)
				{
					mana = 14;
					damage = 35;
					useStyle = 5;
					name = "Demon Scythe";
					shootSpeed = 0.2f;
					shoot = 45;
					width = 26;
					height = 28;
					useSound = 8;
					useAnimation = 20;
					useTime = 20;
					rare = 3;
					noMelee = true;
					knockBack = 5f;
					scale = 0.9f;
					toolTip = "Casts a demon scythe";
					value = 10000;
					magic = true;
				}
				else if (type == 273)
				{
					name = "Night's Edge";
					useStyle = 1;
					useAnimation = 27;
					useTime = 27;
					knockBack = 4.5f;
					width = 40;
					height = 40;
					damage = 42;
					scale = 1.15f;
					useSound = 1;
					rare = 3;
					value = 27000;
					melee = true;
				}
				else if (type == 274)
				{
					name = "Dark Lance";
					useStyle = 5;
					useAnimation = 25;
					useTime = 25;
					shootSpeed = 5f;
					knockBack = 4f;
					width = 40;
					height = 40;
					damage = 27;
					scale = 1.1f;
					useSound = 1;
					shoot = 46;
					rare = 3;
					value = 27000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 275)
				{
					name = "Coral";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 81;
					width = 20;
					height = 22;
					value = 400;
				}
				else if (type == 276)
				{
					name = "Cactus";
					maxStack = 250;
					width = 12;
					height = 12;
					value = 10;
				}
				else if (type == 277)
				{
					name = "Trident";
					useStyle = 5;
					useAnimation = 31;
					useTime = 31;
					shootSpeed = 4f;
					knockBack = 5f;
					width = 40;
					height = 40;
					damage = 10;
					scale = 1.1f;
					useSound = 1;
					shoot = 47;
					rare = 1;
					value = 10000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 278)
				{
					name = "Silver Bullet";
					shootSpeed = 4.5f;
					shoot = 14;
					damage = 9;
					width = 8;
					height = 8;
					maxStack = 250;
					consumable = true;
					ammo = 14;
					knockBack = 3f;
					value = 15;
					ranged = true;
				}
				else if (type == 279)
				{
					useStyle = 1;
					name = "Throwing Knife";
					shootSpeed = 10f;
					shoot = 48;
					damage = 12;
					width = 18;
					height = 20;
					maxStack = 250;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 50;
					knockBack = 2f;
					ranged = true;
				}
				else if (type == 280)
				{
					name = "Spear";
					useStyle = 5;
					useAnimation = 31;
					useTime = 31;
					shootSpeed = 3.7f;
					knockBack = 6.5f;
					width = 32;
					height = 32;
					damage = 8;
					scale = 1f;
					useSound = 1;
					shoot = 49;
					value = 1000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 281)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 45;
					useTime = 45;
					name = "Blowpipe";
					width = 38;
					height = 6;
					shoot = 10;
					useAmmo = 51;
					useSound = 5;
					damage = 9;
					shootSpeed = 11f;
					noMelee = true;
					value = 10000;
					knockBack = 4f;
					useAmmo = 51;
					toolTip = "Allows the collection of seeds for ammo";
					ranged = true;
				}
				else if (type == 282)
				{
					useStyle = 1;
					name = "Glowstick";
					shootSpeed = 6f;
					shoot = 50;
					width = 12;
					height = 12;
					maxStack = 99;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noMelee = true;
					value = 10;
					holdStyle = 1;
					toolTip = "Works when wet";
				}
				else if (type == 283)
				{
					name = "Seed";
					shoot = 51;
					width = 8;
					height = 8;
					maxStack = 250;
					ammo = 51;
					toolTip = "For use with Blowpipe";
				}
				else if (type == 284)
				{
					noMelee = true;
					useStyle = 1;
					name = "Wooden Boomerang";
					shootSpeed = 6.5f;
					shoot = 52;
					damage = 7;
					knockBack = 5f;
					width = 14;
					height = 28;
					useSound = 1;
					useAnimation = 16;
					useTime = 16;
					noUseGraphic = true;
					value = 5000;
					melee = true;
				}
				else if (type == 285)
				{
					name = "Aglet";
					width = 24;
					height = 8;
					accessory = true;
					toolTip = "5% increased movement speed";
					value = 5000;
				}
				else if (type == 286)
				{
					useStyle = 1;
					name = "Sticky Glowstick";
					shootSpeed = 6f;
					shoot = 53;
					width = 12;
					height = 12;
					maxStack = 99;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noMelee = true;
					value = 20;
					holdStyle = 1;
				}
				else if (type == 287)
				{
					useStyle = 1;
					name = "Poisoned Knife";
					shootSpeed = 11f;
					shoot = 54;
					damage = 13;
					width = 18;
					height = 20;
					maxStack = 250;
					consumable = true;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 60;
					knockBack = 2f;
					ranged = true;
				}
				else if (type == 288)
				{
					name = "Obsidian Skin Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 1;
					buffTime = 14400;
					toolTip = "Provides immunity to lava";
					value = 1000;
					rare = 1;
				}
				else if (type == 289)
				{
					name = "Regeneration Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 2;
					buffTime = 18000;
					toolTip = "Provides life regeneration";
					value = 1000;
					rare = 1;
				}
				else if (type == 290)
				{
					name = "Swiftness Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 3;
					buffTime = 14400;
					toolTip = "25% increased movement speed";
					value = 1000;
					rare = 1;
				}
				else if (type == 291)
				{
					name = "Gills Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 4;
					buffTime = 7200;
					toolTip = "Breathe water instead of air";
					value = 1000;
					rare = 1;
				}
				else if (type == 292)
				{
					name = "Ironskin Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 5;
					buffTime = 18000;
					toolTip = "Increase defense by 8";
					value = 1000;
					rare = 1;
				}
				else if (type == 293)
				{
					name = "Mana Regeneration Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 6;
					buffTime = 7200;
					toolTip = "Increased mana regeneration";
					value = 1000;
					rare = 1;
				}
				else if (type == 294)
				{
					name = "Magic Power Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 7;
					buffTime = 7200;
					toolTip = "20% increased magic damage";
					value = 1000;
					rare = 1;
				}
				else if (type == 295)
				{
					name = "Featherfall Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 8;
					buffTime = 18000;
					toolTip = "Slows falling speed";
					value = 1000;
					rare = 1;
				}
				else if (type == 296)
				{
					name = "Spelunker Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 9;
					buffTime = 18000;
					toolTip = "Shows the location of treasure and ore";
					value = 1000;
					rare = 1;
				}
				else if (type == 297)
				{
					name = "Invisibility Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 10;
					buffTime = 7200;
					toolTip = "Grants invisibility";
					value = 1000;
					rare = 1;
				}
				else if (type == 298)
				{
					name = "Shine Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 11;
					buffTime = 18000;
					toolTip = "Emits an aura of light";
					value = 1000;
					rare = 1;
				}
				else if (type == 299)
				{
					name = "Night Owl Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 12;
					buffTime = 14400;
					toolTip = "Increases night vision";
					value = 1000;
					rare = 1;
				}
				else if (type == 300)
				{
					name = "Battle Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 13;
					buffTime = 25200;
					toolTip = "Increases enemy spawn rate";
					value = 1000;
					rare = 1;
				}
				else if (type == 301)
				{
					name = "Thorns Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 14;
					buffTime = 7200;
					toolTip = "Attackers also take damage";
					value = 1000;
					rare = 1;
				}
				else if (type == 302)
				{
					name = "Water Walking Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 15;
					buffTime = 18000;
					toolTip = "Allows the ability to walk on water";
					value = 1000;
					rare = 1;
				}
				else if (type == 303)
				{
					name = "Archery Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 16;
					buffTime = 14400;
					toolTip = "20% increased arrow speed and damage";
					value = 1000;
					rare = 1;
				}
				else if (type == 304)
				{
					name = "Hunter Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 17;
					buffTime = 18000;
					toolTip = "Shows the location of enemies";
					value = 1000;
					rare = 1;
				}
				else if (type == 305)
				{
					name = "Gravitation Potion";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					buffType = 18;
					buffTime = 10800;
					toolTip = "Allows the control of gravity";
					value = 1000;
					rare = 1;
				}
				else if (type == 306)
				{
					name = "Gold Chest";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 21;
					placeStyle = 1;
					width = 26;
					height = 22;
					value = 5000;
				}
				else if (type == 307)
				{
					name = "Daybloom Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 0;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 308)
				{
					name = "Moonglow Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 1;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 309)
				{
					name = "Blinkroot Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 2;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 310)
				{
					name = "Deathweed Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 3;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 311)
				{
					name = "Waterleaf Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 4;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 312)
				{
					name = "Fireblossom Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 82;
					placeStyle = 5;
					width = 12;
					height = 14;
					value = 80;
				}
				else if (type == 313)
				{
					name = "Daybloom";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 314)
				{
					name = "Moonglow";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 315)
				{
					name = "Blinkroot";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 316)
				{
					name = "Deathweed";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 317)
				{
					name = "Waterleaf";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 318)
				{
					name = "Fireblossom";
					maxStack = 99;
					width = 12;
					height = 14;
					value = 100;
				}
				else if (type == 319)
				{
					name = "Shark Fin";
					maxStack = 99;
					width = 16;
					height = 14;
					value = 200;
				}
				else if (type == 320)
				{
					name = "Feather";
					maxStack = 99;
					width = 16;
					height = 14;
					value = 50;
				}
				else if (type == 321)
				{
					name = "Tombstone";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 85;
					width = 20;
					height = 20;
				}
				else if (type == 322)
				{
					name = "Mime Mask";
					headSlot = 28;
					width = 20;
					height = 20;
					value = 20000;
				}
				else if (type == 323)
				{
					name = "Antlion Mandible";
					width = 10;
					height = 20;
					maxStack = 99;
					value = 50;
				}
				else if (type == 324)
				{
					name = "Illegal Gun Parts";
					width = 10;
					height = 20;
					maxStack = 99;
					value = 750000;
					toolTip = "'Banned in most places'";
				}
				else if (type == 325)
				{
					name = "The Doctor's Shirt";
					width = 18;
					height = 18;
					bodySlot = 16;
					value = 200000;
					vanity = true;
				}
				else if (type == 326)
				{
					name = "The Doctor's Pants";
					width = 18;
					height = 18;
					legSlot = 15;
					value = 200000;
					vanity = true;
				}
				else if (type == 327)
				{
					name = "Golden Key";
					width = 14;
					height = 20;
					maxStack = 99;
					toolTip = "Opens one Gold Chest";
				}
				else if (type == 328)
				{
					name = "Shadow Chest";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 21;
					placeStyle = 3;
					width = 26;
					height = 22;
					value = 5000;
				}
				else if (type == 329)
				{
					name = "Shadow Key";
					width = 14;
					height = 20;
					maxStack = 1;
					toolTip = "Opens all Shadow Chests";
					value = 75000;
				}
				else if (type == 330)
				{
					name = "Obsidian Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 20;
					width = 12;
					height = 12;
				}
				else if (type == 331)
				{
					name = "Jungle Spores";
					width = 18;
					height = 16;
					maxStack = 99;
					value = 100;
				}
				else if (type == 332)
				{
					name = "Loom";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 86;
					width = 20;
					height = 20;
					value = 300;
					toolTip = "Used for crafting cloth";
				}
				else if (type == 333)
				{
					name = "Piano";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 87;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 334)
				{
					name = "Dresser";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 88;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 335)
				{
					name = "Bench";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 89;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 336)
				{
					name = "Bathtub";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 90;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 337)
				{
					name = "Red Banner";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 91;
					placeStyle = 0;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 338)
				{
					name = "Green Banner";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 91;
					placeStyle = 1;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 339)
				{
					name = "Blue Banner";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 91;
					placeStyle = 2;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 340)
				{
					name = "Yellow Banner";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 91;
					placeStyle = 3;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 341)
				{
					name = "Lamp Post";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 92;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 342)
				{
					name = "Tiki Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 93;
					width = 10;
					height = 24;
					value = 500;
				}
				else if (type == 343)
				{
					name = "Barrel";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 21;
					placeStyle = 5;
					width = 20;
					height = 20;
					value = 500;
				}
				else if (type == 344)
				{
					name = "Chinese Lantern";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 95;
					width = 20;
					height = 20;
					value = 500;
				}
				else if (type == 345)
				{
					name = "Cooking Pot";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 96;
					width = 20;
					height = 20;
					value = 500;
				}
				else if (type == 346)
				{
					name = "Safe";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 97;
					width = 20;
					height = 20;
					value = 500000;
				}
				else if (type == 347)
				{
					name = "Skull Lantern";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 98;
					width = 20;
					height = 20;
					value = 500;
				}
				else if (type == 348)
				{
					name = "Trash Can";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 21;
					placeStyle = 6;
					width = 20;
					height = 20;
					value = 1000;
				}
				else if (type == 349)
				{
					name = "Candelabra";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 100;
					width = 20;
					height = 20;
					value = 1500;
				}
				else if (type == 350)
				{
					name = "Pink Vase";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 13;
					placeStyle = 3;
					width = 16;
					height = 24;
					value = 70;
				}
				else if (type == 351)
				{
					name = "Mug";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 13;
					placeStyle = 4;
					width = 16;
					height = 24;
					value = 20;
				}
				else if (type == 352)
				{
					name = "Keg";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 94;
					width = 24;
					height = 24;
					value = 600;
					toolTip = "Used for brewing ale";
				}
				else if (type == 353)
				{
					name = "Ale";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 10;
					height = 10;
					buffType = 25;
					buffTime = 7200;
					value = 100;
				}
				else if (type == 354)
				{
					name = "Bookcase";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 101;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 355)
				{
					name = "Throne";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 102;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 356)
				{
					name = "Bowl";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 103;
					width = 16;
					height = 24;
					value = 20;
				}
				else if (type == 357)
				{
					name = "Bowl of Soup";
					useSound = 3;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 10;
					height = 10;
					buffType = 26;
					buffTime = 36000;
					rare = 1;
					toolTip = "Minor improvements to all stats";
					value = 1000;
				}
				else if (type == 358)
				{
					name = "Toilet";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 15;
					placeStyle = 1;
					width = 12;
					height = 30;
					value = 150;
				}
				else if (type == 359)
				{
					name = "Grandfather Clock";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 104;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 360)
				{
					name = "Armor Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
				}
				else if (type == 361)
				{
					useStyle = 4;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					name = "Goblin Battle Standard";
					width = 28;
					height = 28;
					toolTip = "Summons a Goblin Army";
				}
				else if (type == 362)
				{
					name = "Tattered Cloth";
					maxStack = 99;
					width = 24;
					height = 24;
					value = 30;
				}
				else if (type == 363)
				{
					name = "Sawmill";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 106;
					width = 20;
					height = 20;
					value = 300;
					toolTip = "Used for advanced wood crafting";
				}
				else if (type == 364)
				{
					name = "Cobalt Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 107;
					width = 12;
					height = 12;
					value = 3500;
					rare = 3;
				}
				else if (type == 365)
				{
					name = "Mythril Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 108;
					width = 12;
					height = 12;
					value = 5500;
					rare = 3;
				}
				else if (type == 366)
				{
					name = "Adamantite Ore";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 111;
					width = 12;
					height = 12;
					value = 7500;
					rare = 3;
				}
				else if (type == 367)
				{
					name = "Pwnhammer";
					useTurn = true;
					autoReuse = true;
					useStyle = 1;
					useAnimation = 27;
					useTime = 14;
					hammer = 80;
					width = 24;
					height = 28;
					damage = 26;
					knockBack = 7.5f;
					scale = 1.2f;
					useSound = 1;
					rare = 4;
					value = 39000;
					melee = true;
					toolTip = "Strong enough to destroy Demon Altars";
				}
				else if (type == 368)
				{
					autoReuse = true;
					name = "Excalibur";
					useStyle = 1;
					useAnimation = 25;
					useTime = 25;
					knockBack = 4.5f;
					width = 40;
					height = 40;
					damage = 47;
					scale = 1.15f;
					useSound = 1;
					rare = 5;
					value = 230000;
					melee = true;
				}
				else if (type == 369)
				{
					name = "Hallowed Seeds";
					useTurn = true;
					useStyle = 1;
					useAnimation = 15;
					useTime = 10;
					maxStack = 99;
					consumable = true;
					createTile = 109;
					width = 14;
					height = 14;
					value = 2000;
					rare = 3;
				}
				else if (type == 370)
				{
					name = "Ebonsand Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 112;
					width = 12;
					height = 12;
					ammo = 42;
				}
				else if (type == 371)
				{
					name = "Cobalt Hat";
					width = 18;
					height = 18;
					defense = 2;
					headSlot = 29;
					rare = 4;
					value = 75000;
					toolTip = "Increases maximum mana by 40";
					toolTip2 = "9% increased magic critical strike chance";
				}
				else if (type == 372)
				{
					name = "Cobalt Helmet";
					width = 18;
					height = 18;
					defense = 11;
					headSlot = 30;
					rare = 4;
					value = 75000;
					toolTip = "7% increased movement speed";
					toolTip2 = "12% increased melee speed";
				}
				else if (type == 373)
				{
					name = "Cobalt Mask";
					width = 18;
					height = 18;
					defense = 4;
					headSlot = 31;
					rare = 4;
					value = 75000;
					toolTip = "10% increased ranged damage";
					toolTip2 = "6% increased ranged critical strike chance";
				}
				else if (type == 374)
				{
					name = "Cobalt Breastplate";
					width = 18;
					height = 18;
					defense = 8;
					bodySlot = 17;
					rare = 4;
					value = 60000;
					toolTip2 = "3% increased critical strike chance";
				}
				else if (type == 375)
				{
					name = "Cobalt Leggings";
					width = 18;
					height = 18;
					defense = 7;
					legSlot = 16;
					rare = 4;
					value = 45000;
					toolTip2 = "10% increased movement speed";
				}
				else if (type == 376)
				{
					name = "Mythril Hood";
					width = 18;
					height = 18;
					defense = 3;
					headSlot = 32;
					rare = 4;
					value = 112500;
					toolTip = "Increases maximum mana by 60";
					toolTip2 = "15% increased magic damage";
				}
				else if (type == 377)
				{
					name = "Mythril Helmet";
					width = 18;
					height = 18;
					defense = 16;
					headSlot = 33;
					rare = 4;
					value = 112500;
					toolTip = "5% increased melee critical strike chance";
					toolTip2 = "10% increased melee damage";
				}
				else if (type == 378)
				{
					name = "Mythril Hat";
					width = 18;
					height = 18;
					defense = 6;
					headSlot = 34;
					rare = 4;
					value = 112500;
					toolTip = "12% increased ranged damage";
					toolTip2 = "7% increased ranged critical strike chance";
				}
				else if (type == 379)
				{
					name = "Mythril Chainmail";
					width = 18;
					height = 18;
					defense = 12;
					bodySlot = 18;
					rare = 4;
					value = 90000;
					toolTip2 = "5% increased damage";
				}
				else if (type == 380)
				{
					name = "Mythril Greaves";
					width = 18;
					height = 18;
					defense = 9;
					legSlot = 17;
					rare = 4;
					value = 67500;
					toolTip2 = "3% increased critical strike chance";
				}
				else if (type == 381)
				{
					name = "Cobalt Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 10500;
					rare = 3;
				}
				else if (type == 382)
				{
					name = "Mythril Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 22000;
					rare = 3;
				}
				else if (type == 383)
				{
					name = "Cobalt Chainsaw";
					useStyle = 5;
					useAnimation = 25;
					useTime = 8;
					shootSpeed = 40f;
					knockBack = 2.75f;
					width = 20;
					height = 12;
					damage = 23;
					axe = 14;
					useSound = 23;
					shoot = 57;
					rare = 4;
					value = 54000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
				}
				else if (type == 384)
				{
					name = "Mythril Chainsaw";
					useStyle = 5;
					useAnimation = 25;
					useTime = 8;
					shootSpeed = 40f;
					knockBack = 3f;
					width = 20;
					height = 12;
					damage = 29;
					axe = 17;
					useSound = 23;
					shoot = 58;
					rare = 4;
					value = 81000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
				}
				else if (type == 385)
				{
					name = "Cobalt Drill";
					useStyle = 5;
					useAnimation = 25;
					useTime = 13;
					shootSpeed = 32f;
					knockBack = 0f;
					width = 20;
					height = 12;
					damage = 10;
					pick = 110;
					useSound = 23;
					shoot = 59;
					rare = 4;
					value = 54000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
					toolTip = "Can mine Mythril";
				}
				else if (type == 386)
				{
					name = "Mythril Drill";
					useStyle = 5;
					useAnimation = 25;
					useTime = 10;
					shootSpeed = 32f;
					knockBack = 0f;
					width = 20;
					height = 12;
					damage = 15;
					pick = 150;
					useSound = 23;
					shoot = 60;
					rare = 4;
					value = 81000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
					toolTip = "Can mine Adamantite";
				}
				else if (type == 387)
				{
					name = "Adamantite Chainsaw";
					useStyle = 5;
					useAnimation = 25;
					useTime = 6;
					shootSpeed = 40f;
					knockBack = 4.5f;
					width = 20;
					height = 12;
					damage = 33;
					axe = 20;
					useSound = 23;
					shoot = 61;
					rare = 4;
					value = 108000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
				}
				else if (type == 388)
				{
					name = "Adamantite Drill";
					useStyle = 5;
					useAnimation = 25;
					useTime = 7;
					shootSpeed = 32f;
					knockBack = 0f;
					width = 20;
					height = 12;
					damage = 20;
					pick = 180;
					useSound = 23;
					shoot = 62;
					rare = 4;
					value = 108000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
				}
				else if (type == 389)
				{
					name = "Dao of Pow";
					noMelee = true;
					useStyle = 5;
					useAnimation = 45;
					useTime = 45;
					knockBack = 7f;
					width = 30;
					height = 10;
					damage = 49;
					scale = 1.1f;
					noUseGraphic = true;
					shoot = 63;
					shootSpeed = 15f;
					useSound = 1;
					rare = 5;
					value = 144000;
					melee = true;
					channel = true;
					toolTip = "Has a chance to confuse";
					toolTip2 = "'Find your inner pieces'";
				}
				else if (type == 390)
				{
					name = "Mythril Halberd";
					useStyle = 5;
					useAnimation = 26;
					useTime = 26;
					shootSpeed = 4.5f;
					knockBack = 5f;
					width = 40;
					height = 40;
					damage = 35;
					scale = 1.1f;
					useSound = 1;
					shoot = 64;
					rare = 4;
					value = 67500;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 391)
				{
					name = "Adamantite Bar";
					width = 20;
					height = 20;
					maxStack = 99;
					value = 37500;
					rare = 3;
				}
				else if (type == 392)
				{
					name = "Glass Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 21;
					width = 12;
					height = 12;
				}
				else if (type == 393)
				{
					name = "Compass";
					width = 24;
					height = 28;
					rare = 3;
					value = 100000;
					accessory = true;
					toolTip = "Shows horizontal position";
				}
				else if (type == 394)
				{
					name = "Diving Gear";
					width = 24;
					height = 28;
					rare = 4;
					value = 100000;
					accessory = true;
					toolTip = "Grants the ability to swim";
					toolTip2 = "Greatly extends underwater breathing";
				}
				else if (type == 395)
				{
					name = "GPS";
					width = 24;
					height = 28;
					rare = 4;
					value = 150000;
					accessory = true;
					toolTip = "Shows position";
					toolTip2 = "Tells the time";
				}
				else if (type == 396)
				{
					name = "Obsidian Horseshoe";
					width = 24;
					height = 28;
					rare = 4;
					value = 100000;
					accessory = true;
					toolTip = "Negates fall damage";
					toolTip2 = "Grants immunity to fire blocks";
				}
				else if (type == 397)
				{
					name = "Obsidian Shield";
					width = 24;
					height = 28;
					rare = 4;
					value = 100000;
					accessory = true;
					defense = 2;
					toolTip = "Grants immunity to knockback";
					toolTip2 = "Grants immunity to fire blocks";
				}
				else if (type == 398)
				{
					name = "Tinkerer's Workshop";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 114;
					width = 26;
					height = 20;
					value = 100000;
					toolTip = "Allows the combining of some accessories";
				}
				else if (type == 399)
				{
					name = "Cloud in a Balloon";
					width = 14;
					height = 28;
					rare = 4;
					value = 150000;
					accessory = true;
					toolTip = "Allows the holder to double jump";
					toolTip2 = "Increases jump height";
				}
				else if (type == 400)
				{
					name = "Adamantite Headgear";
					width = 18;
					height = 18;
					defense = 4;
					headSlot = 35;
					rare = 4;
					value = 150000;
					toolTip = "Increases maximum mana by 80";
					toolTip2 = "11% increased magic damage and critical strike chance";
				}
				else if (type == 401)
				{
					name = "Adamantite Helmet";
					width = 18;
					height = 18;
					defense = 22;
					headSlot = 36;
					rare = 4;
					value = 150000;
					toolTip = "7% increased melee critical strike chance";
					toolTip2 = "14% increased melee damage";
				}
				else if (type == 402)
				{
					name = "Adamantite Mask";
					width = 18;
					height = 18;
					defense = 8;
					headSlot = 37;
					rare = 4;
					value = 150000;
					toolTip = "14% increased ranged damage";
					toolTip2 = "8% increased ranged critical strike chance";
				}
				else if (type == 403)
				{
					name = "Adamantite Breastplate";
					width = 18;
					height = 18;
					defense = 14;
					bodySlot = 19;
					rare = 4;
					value = 120000;
					toolTip = "6% increased damage";
				}
				else if (type == 404)
				{
					name = "Adamantite Leggings";
					width = 18;
					height = 18;
					defense = 10;
					legSlot = 18;
					rare = 4;
					value = 90000;
					toolTip = "4% increased critical strike chance";
					toolTip2 = "5% increased movement speed";
				}
				else if (type == 405)
				{
					name = "Spectre Boots";
					width = 28;
					height = 24;
					accessory = true;
					rare = 4;
					toolTip = "Allows flight";
					toolTip2 = "The wearer can run super fast";
					value = 100000;
				}
				else if (type == 406)
				{
					name = "Adamantite Glaive";
					useStyle = 5;
					useAnimation = 25;
					useTime = 25;
					shootSpeed = 5f;
					knockBack = 6f;
					width = 40;
					height = 40;
					damage = 38;
					scale = 1.1f;
					useSound = 1;
					shoot = 66;
					rare = 4;
					value = 90000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 407)
				{
					name = "Toolbelt";
					width = 28;
					height = 24;
					accessory = true;
					rare = 3;
					toolTip = "Increases block placement range";
					value = 100000;
				}
				else if (type == 408)
				{
					name = "Pearlsand Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 116;
					width = 12;
					height = 12;
					ammo = 42;
				}
				else if (type == 409)
				{
					name = "Pearlstone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 117;
					width = 12;
					height = 12;
				}
				else if (type == 410)
				{
					name = "Mining Shirt";
					width = 18;
					height = 18;
					defense = 1;
					bodySlot = 20;
					value = 5000;
					rare = 1;
				}
				else if (type == 411)
				{
					name = "Mining Pants";
					width = 18;
					height = 18;
					defense = 1;
					legSlot = 19;
					value = 5000;
					rare = 1;
				}
				else if (type == 412)
				{
					name = "Pearlstone Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 118;
					width = 12;
					height = 12;
				}
				else if (type == 413)
				{
					name = "Iridescent Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 119;
					width = 12;
					height = 12;
				}
				else if (type == 414)
				{
					name = "Mudstone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 120;
					width = 12;
					height = 12;
				}
				else if (type == 415)
				{
					name = "Cobalt Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 121;
					width = 12;
					height = 12;
				}
				else if (type == 416)
				{
					name = "Mythril Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 122;
					width = 12;
					height = 12;
				}
				else if (type == 417)
				{
					name = "Pearlstone Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 22;
					width = 12;
					height = 12;
				}
				else if (type == 418)
				{
					name = "Iridescent Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 23;
					width = 12;
					height = 12;
				}
				else if (type == 419)
				{
					name = "Mudstone Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 24;
					width = 12;
					height = 12;
				}
				else if (type == 420)
				{
					name = "Cobalt Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 25;
					width = 12;
					height = 12;
				}
				else if (type == 421)
				{
					name = "Mythril Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 26;
					width = 12;
					height = 12;
				}
				else if (type == 422)
				{
					useStyle = 1;
					name = "Holy Water";
					shootSpeed = 9f;
					rare = 3;
					damage = 20;
					shoot = 69;
					width = 18;
					height = 20;
					maxStack = 250;
					consumable = true;
					knockBack = 3f;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 200;
					toolTip = "Spreads the Hallow to some blocks";
				}
				else if (type == 423)
				{
					useStyle = 1;
					name = "Unholy Water";
					shootSpeed = 9f;
					rare = 3;
					damage = 20;
					shoot = 70;
					width = 18;
					height = 20;
					maxStack = 250;
					consumable = true;
					knockBack = 3f;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 200;
					toolTip = "Spreads the corruption to some blocks";
				}
				else if (type == 424)
				{
					name = "Silt Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 123;
					width = 12;
					height = 12;
				}
				else if (type == 425)
				{
					mana = 40;
					channel = true;
					damage = 0;
					useStyle = 1;
					name = "Fairy Bell";
					shoot = 72;
					width = 24;
					height = 24;
					useSound = 25;
					useAnimation = 20;
					useTime = 20;
					rare = 5;
					noMelee = true;
					toolTip = "Summons a magical fairy";
					value = (value = 250000);
					buffType = 27;
					buffTime = 18000;
				}
				else if (type == 426)
				{
					name = "Breaker Blade";
					useStyle = 1;
					useAnimation = 30;
					knockBack = 8f;
					width = 60;
					height = 70;
					damage = 39;
					scale = 1.05f;
					useSound = 1;
					rare = 4;
					value = 150000;
					melee = true;
				}
				else if (type == 427)
				{
					noWet = true;
					name = "Blue Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 1;
					width = 10;
					height = 12;
					value = 200;
				}
				else if (type == 428)
				{
					noWet = true;
					name = "Red Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 2;
					width = 10;
					height = 12;
					value = 200;
				}
				else if (type == 429)
				{
					noWet = true;
					name = "Green Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 3;
					width = 10;
					height = 12;
					value = 200;
				}
				else if (type == 430)
				{
					noWet = true;
					name = "Purple Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 4;
					width = 10;
					height = 12;
					value = 200;
				}
				else if (type == 431)
				{
					noWet = true;
					name = "White Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 5;
					width = 10;
					height = 12;
					value = 500;
				}
				else if (type == 432)
				{
					noWet = true;
					name = "Yellow Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 6;
					width = 10;
					height = 12;
					value = 200;
				}
				else if (type == 433)
				{
					noWet = true;
					name = "Demon Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 7;
					width = 10;
					height = 12;
					value = 300;
				}
				else if (type == 434)
				{
					autoReuse = true;
					useStyle = 5;
					useAnimation = 12;
					useTime = 4;
					reuseDelay = 14;
					name = "Clockwork Assault Rifle";
					width = 50;
					height = 18;
					shoot = 10;
					useAmmo = 14;
					useSound = 31;
					damage = 19;
					shootSpeed = 7.75f;
					noMelee = true;
					value = 150000;
					rare = 4;
					ranged = true;
					toolTip = "Three round burst";
					toolTip2 = "Only the first shot consumes ammo";
				}
				else if (type == 435)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 25;
					useTime = 25;
					name = "Cobalt Repeater";
					width = 50;
					height = 18;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 30;
					shootSpeed = 9f;
					noMelee = true;
					value = 60000;
					ranged = true;
					rare = 4;
					knockBack = 1.5f;
				}
				else if (type == 436)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 23;
					useTime = 23;
					name = "Mythril Repeater";
					width = 50;
					height = 18;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 34;
					shootSpeed = 9.5f;
					noMelee = true;
					value = 90000;
					ranged = true;
					rare = 4;
					knockBack = 2f;
				}
				else if (type == 437)
				{
					noUseGraphic = true;
					damage = 0;
					knockBack = 7f;
					useStyle = 5;
					name = "Dual Hook";
					shootSpeed = 14f;
					shoot = 73;
					width = 18;
					height = 28;
					useSound = 1;
					useAnimation = 20;
					useTime = 20;
					rare = 4;
					noMelee = true;
					value = 200000;
				}
				else if (type == 438)
				{
					name = "Star Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 2;
				}
				else if (type == 439)
				{
					name = "Sword Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 3;
				}
				else if (type == 440)
				{
					name = "Slime Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 4;
				}
				else if (type == 441)
				{
					name = "Goblin Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 5;
				}
				else if (type == 442)
				{
					name = "Shield Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 6;
				}
				else if (type == 443)
				{
					name = "Bat Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 7;
				}
				else if (type == 444)
				{
					name = "Fish Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 8;
				}
				else if (type == 445)
				{
					name = "Bunny Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 9;
				}
				else if (type == 446)
				{
					name = "Skeleton Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 10;
				}
				else if (type == 447)
				{
					name = "Reaper Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 11;
				}
				else if (type == 448)
				{
					name = "Woman Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 12;
				}
				else if (type == 449)
				{
					name = "Imp Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 13;
				}
				else if (type == 450)
				{
					name = "Gargoyle Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 14;
				}
				else if (type == 451)
				{
					name = "Gloom Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 15;
				}
				else if (type == 452)
				{
					name = "Hornet Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 16;
				}
				else if (type == 453)
				{
					name = "Bomb Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 17;
				}
				else if (type == 454)
				{
					name = "Crab Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 18;
				}
				else if (type == 455)
				{
					name = "Hammer Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 19;
				}
				else if (type == 456)
				{
					name = "Potion Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 20;
				}
				else if (type == 457)
				{
					name = "Spear Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 21;
				}
				else if (type == 458)
				{
					name = "Cross Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 22;
				}
				else if (type == 459)
				{
					name = "Jellyfish Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 23;
				}
				else if (type == 460)
				{
					name = "Bow Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 24;
				}
				else if (type == 461)
				{
					name = "Boomerang Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 25;
				}
				else if (type == 462)
				{
					name = "Boot Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 26;
				}
				else if (type == 463)
				{
					name = "Chest Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 27;
				}
				else if (type == 464)
				{
					name = "Bird Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 28;
				}
				else if (type == 465)
				{
					name = "Axe Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 29;
				}
				else if (type == 466)
				{
					name = "Corrupt Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 30;
				}
				else if (type == 467)
				{
					name = "Tree Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 31;
				}
				else if (type == 468)
				{
					name = "Anvil Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 32;
				}
				else if (type == 469)
				{
					name = "Pickaxe Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 33;
				}
				else if (type == 470)
				{
					name = "Mushroom Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 34;
				}
				else if (type == 471)
				{
					name = "Eyeball Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 35;
				}
				else if (type == 472)
				{
					name = "Pillar Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 36;
				}
				else if (type == 473)
				{
					name = "Heart Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 37;
				}
				else if (type == 474)
				{
					name = "Pot Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 38;
				}
				else if (type == 475)
				{
					name = "Sunflower Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 39;
				}
				else if (type == 476)
				{
					name = "King Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 40;
				}
				else if (type == 477)
				{
					name = "Queen Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 41;
				}
				else if (type == 478)
				{
					name = "Pirahna Statue";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 105;
					width = 20;
					height = 20;
					value = 300;
					placeStyle = 42;
				}
				else if (type == 479)
				{
					name = "Planked Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 7;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 27;
					width = 12;
					height = 12;
				}
				else if (type == 480)
				{
					name = "Wooden Beam";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 124;
					width = 12;
					height = 12;
				}
				else if (type == 481)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 20;
					useTime = 20;
					name = "Adamantite Repeater";
					width = 50;
					height = 18;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 37;
					shootSpeed = 10f;
					noMelee = true;
					value = 120000;
					ranged = true;
					rare = 4;
					knockBack = 2.5f;
				}
				else if (type == 482)
				{
					name = "Adamantite Sword";
					useStyle = 1;
					useAnimation = 27;
					useTime = 27;
					knockBack = 6f;
					width = 40;
					height = 40;
					damage = 44;
					scale = 1.2f;
					useSound = 1;
					rare = 4;
					value = 138000;
					melee = true;
				}
				else if (type == 483)
				{
					useTurn = true;
					autoReuse = true;
					name = "Cobalt Sword";
					useStyle = 1;
					useAnimation = 23;
					useTime = 23;
					knockBack = 3.85f;
					width = 40;
					height = 40;
					damage = 34;
					scale = 1.1f;
					useSound = 1;
					rare = 4;
					value = 69000;
					melee = true;
				}
				else if (type == 484)
				{
					name = "Mythril Sword";
					useStyle = 1;
					useAnimation = 26;
					useTime = 26;
					knockBack = 6f;
					width = 40;
					height = 40;
					damage = 39;
					scale = 1.15f;
					useSound = 1;
					rare = 4;
					value = 103500;
					melee = true;
				}
				else if (type == 485)
				{
					rare = 4;
					name = "Moon Charm";
					width = 24;
					height = 28;
					accessory = true;
					toolTip = "Turns the holder into a werewolf on full moons";
					value = 150000;
				}
				else if (type == 486)
				{
					name = "Ruler";
					width = 10;
					height = 26;
					accessory = true;
					toolTip = "Creates a grid on screen for block placement";
					value = 10000;
					rare = 1;
				}
				else if (type == 487)
				{
					name = "Crystal Ball";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 125;
					width = 22;
					height = 22;
					value = 100000;
					rare = 3;
				}
				else if (type == 488)
				{
					name = "Disco Ball";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 126;
					width = 22;
					height = 26;
					value = 10000;
				}
				else if (type == 489)
				{
					name = "Sorcerer Emblem";
					width = 24;
					height = 24;
					accessory = true;
					toolTip = "15% increased magic damage";
					value = 100000;
					rare = 4;
				}
				else if (type == 491)
				{
					name = "Ranger Emblem";
					width = 24;
					height = 24;
					accessory = true;
					toolTip = "15% increased ranged damage";
					value = 100000;
					rare = 4;
				}
				else if (type == 490)
				{
					name = "Warrior Emblem";
					width = 24;
					height = 24;
					accessory = true;
					toolTip = "15% increased melee damage";
					value = 100000;
					rare = 4;
				}
				else if (type == 492)
				{
					name = "Demon Wings";
					width = 24;
					height = 8;
					accessory = true;
					toolTip = "Allows flight and slow fall";
					value = 400000;
					rare = 5;
				}
				else if (type == 493)
				{
					name = "Angel Wings";
					width = 24;
					height = 8;
					accessory = true;
					toolTip = "Allows flight and slow fall";
					value = 400000;
					rare = 5;
				}
				else if (type == 494)
				{
					rare = 5;
					useStyle = 5;
					useAnimation = 12;
					useTime = 12;
					name = "Magical Harp";
					width = 12;
					height = 28;
					shoot = 76;
					holdStyle = 3;
					autoReuse = true;
					damage = 30;
					shootSpeed = 4.5f;
					noMelee = true;
					value = 200000;
					mana = 4;
					magic = true;
				}
				else if (type == 495)
				{
					rare = 5;
					mana = 10;
					channel = true;
					damage = 53;
					useStyle = 1;
					name = "Rainbow Rod";
					shootSpeed = 6f;
					shoot = 79;
					width = 26;
					height = 28;
					useSound = 28;
					useAnimation = 15;
					useTime = 15;
					noMelee = true;
					knockBack = 5f;
					toolTip = "Casts a controllable rainbow";
					value = 200000;
					magic = true;
				}
				else if (type == 496)
				{
					rare = 4;
					mana = 7;
					damage = 26;
					useStyle = 1;
					name = "Ice Rod";
					shootSpeed = 12f;
					shoot = 80;
					width = 26;
					height = 28;
					useSound = 28;
					useAnimation = 17;
					useTime = 17;
					rare = 4;
					autoReuse = true;
					noMelee = true;
					knockBack = 0f;
					toolTip = "Summons a block of ice";
					value = 1000000;
					magic = true;
					knockBack = 2f;
				}
				else if (type == 497)
				{
					name = "Neptune's Shell";
					width = 24;
					height = 28;
					accessory = true;
					toolTip = "Transforms the holder into merfolk when entering water";
					value = 150000;
					rare = 5;
				}
				else if (type == 498)
				{
					name = "Mannequin";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 128;
					width = 12;
					height = 12;
				}
				else if (type == 499)
				{
					name = "Greater Healing Potion";
					useSound = 3;
					healLife = 150;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 30;
					consumable = true;
					width = 14;
					height = 24;
					rare = 3;
					potion = true;
					value = 5000;
				}
				else if (type == 500)
				{
					name = "Greater Mana Potion";
					useSound = 3;
					healMana = 200;
					useStyle = 2;
					useTurn = true;
					useAnimation = 17;
					useTime = 17;
					maxStack = 99;
					consumable = true;
					width = 14;
					height = 24;
					rare = 3;
					value = 500;
				}
				else if (type == 501)
				{
					name = "Pixie Dust";
					width = 16;
					height = 14;
					maxStack = 99;
					value = 500;
					rare = 1;
				}
				else if (type == 502)
				{
					name = "Crystal Shard";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 129;
					width = 24;
					height = 24;
					value = 8000;
					rare = 1;
				}
				else if (type == 503)
				{
					name = "Clown Hat";
					width = 18;
					height = 18;
					headSlot = 40;
					value = 20000;
					vanity = true;
					rare = 2;
				}
				else if (type == 504)
				{
					name = "Clown Shirt";
					width = 18;
					height = 18;
					bodySlot = 23;
					value = 10000;
					vanity = true;
					rare = 2;
				}
				else if (type == 505)
				{
					name = "Clown Pants";
					width = 18;
					height = 18;
					legSlot = 22;
					value = 10000;
					vanity = true;
					rare = 2;
				}
				else if (type == 506)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 30;
					useTime = 6;
					name = "Flamethrower";
					width = 50;
					height = 18;
					shoot = 85;
					useAmmo = 23;
					useSound = 34;
					damage = 27;
					knockBack = 0.3f;
					shootSpeed = 7f;
					noMelee = true;
					value = 500000;
					rare = 5;
					ranged = true;
					toolTip = "Uses gel for ammo";
				}
				else if (type == 507)
				{
					rare = 3;
					useStyle = 1;
					useAnimation = 12;
					useTime = 12;
					name = "Bell";
					width = 12;
					height = 28;
					autoReuse = true;
					noMelee = true;
					value = 10000;
				}
				else if (type == 508)
				{
					rare = 3;
					useStyle = 5;
					useAnimation = 12;
					useTime = 12;
					name = "Harp";
					width = 12;
					height = 28;
					autoReuse = true;
					noMelee = true;
					value = 10000;
				}
				else if (type == 509)
				{
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					name = "Wrench";
					width = 24;
					height = 28;
					rare = 1;
					toolTip = "Places wire";
					value = 20000;
					mech = true;
				}
				else if (type == 510)
				{
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					name = "Wire Cutter";
					width = 24;
					height = 28;
					rare = 1;
					toolTip = "Removes wire";
					value = 20000;
					mech = true;
				}
				else if (type == 511)
				{
					name = "Active Stone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 130;
					width = 12;
					height = 12;
					value = 1000;
					mech = true;
				}
				else if (type == 512)
				{
					name = "Inactive Stone Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 131;
					width = 12;
					height = 12;
					value = 1000;
					mech = true;
				}
				else if (type == 513)
				{
					name = "Lever";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 132;
					width = 24;
					height = 24;
					value = 3000;
					mech = true;
				}
				else if (type == 514)
				{
					autoReuse = true;
					useStyle = 5;
					useAnimation = 12;
					useTime = 12;
					name = "Laser Rifle";
					width = 36;
					height = 22;
					shoot = 88;
					mana = 8;
					useSound = 12;
					knockBack = 2.5f;
					damage = 29;
					shootSpeed = 17f;
					noMelee = true;
					rare = 4;
					magic = true;
					value = 500000;
				}
				else if (type == 515)
				{
					name = "Crystal Bullet";
					shootSpeed = 5f;
					shoot = 89;
					damage = 9;
					width = 8;
					height = 8;
					maxStack = 250;
					consumable = true;
					ammo = 14;
					knockBack = 1f;
					value = 30;
					ranged = true;
					rare = 3;
					toolTip = "Creates several crystal shards on impact";
				}
				else if (type == 516)
				{
					name = "Holy Arrow";
					shootSpeed = 3.5f;
					shoot = 91;
					damage = 6;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 2f;
					value = 80;
					ranged = true;
					rare = 3;
					toolTip = "Summons falling stars on impact";
				}
				else if (type == 517)
				{
					useStyle = 1;
					name = "Magic Dagger";
					shootSpeed = 10f;
					shoot = 93;
					damage = 28;
					width = 18;
					height = 20;
					mana = 7;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					noMelee = true;
					value = 1000000;
					knockBack = 2f;
					magic = true;
					rare = 4;
					toolTip = "A magical returning dagger";
				}
				else if (type == 518)
				{
					autoReuse = true;
					rare = 4;
					mana = 5;
					useSound = 9;
					name = "Crystal Storm";
					useStyle = 5;
					damage = 26;
					useAnimation = 7;
					useTime = 7;
					width = 24;
					height = 28;
					shoot = 94;
					scale = 0.9f;
					shootSpeed = 16f;
					knockBack = 5f;
					toolTip = "Summons rapid fire crystal shards";
					magic = true;
					value = 500000;
				}
				else if (type == 519)
				{
					autoReuse = true;
					rare = 4;
					mana = 14;
					useSound = 20;
					name = "Cursed Flames";
					useStyle = 5;
					damage = 35;
					useAnimation = 20;
					useTime = 20;
					width = 24;
					height = 28;
					shoot = 95;
					scale = 0.9f;
					shootSpeed = 10f;
					knockBack = 6.5f;
					toolTip = "Summons unholy fire balls";
					magic = true;
					value = 500000;
				}
				else if (type == 520)
				{
					name = "Soul of Light";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 1000;
					rare = 3;
					toolTip = "'The essence of light creatures'";
				}
				else if (type == 521)
				{
					name = "Soul of Night";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 1000;
					rare = 3;
					toolTip = "'The essence of dark creatures'";
				}
				else if (type == 522)
				{
					name = "Cursed Flame";
					width = 12;
					height = 14;
					maxStack = 99;
					value = 4000;
					rare = 3;
					toolTip = "'Not even water can put the flame out'";
				}
				else if (type == 523)
				{
					name = "Cursed Torch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					holdStyle = 1;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 4;
					placeStyle = 8;
					width = 10;
					height = 12;
					value = 300;
					rare = 1;
					toolTip = "Can be placed in water";
				}
				else if (type == 524)
				{
					name = "Adamantite Forge";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 133;
					width = 44;
					height = 30;
					value = 50000;
					toolTip = "Used to smelt adamantite ore";
					rare = 3;
				}
				else if (type == 525)
				{
					name = "Mythril Anvil";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 134;
					width = 28;
					height = 14;
					value = 25000;
					toolTip = "Used to craft items from mythril and adamantite bars";
					rare = 3;
				}
				else if (type == 526)
				{
					name = "Unicorn Horn";
					width = 14;
					height = 14;
					maxStack = 99;
					value = 15000;
					rare = 1;
					toolTip = "'Sharp and magical!'";
				}
				else if (type == 527)
				{
					name = "Dark Shard";
					width = 14;
					height = 14;
					maxStack = 99;
					value = 4500;
					rare = 2;
					toolTip = "'Sometimes carried by creatures in corrupt deserts'";
				}
				else if (type == 528)
				{
					name = "Light Shard";
					width = 14;
					height = 14;
					maxStack = 99;
					value = 4500;
					rare = 2;
					toolTip = "'Sometimes carried by creatures in light deserts'";
				}
				else if (type == 529)
				{
					name = "Red Pressure Plate";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 135;
					width = 12;
					height = 12;
					placeStyle = 0;
					mech = true;
					value = 5000;
					mech = true;
					toolTip = "Activates when stepped on";
				}
				else if (type == 530)
				{
					name = "Wire";
					width = 12;
					height = 18;
					maxStack = 250;
					value = 500;
					mech = true;
				}
				else if (type == 531)
				{
					name = "Spell Tome";
					width = 12;
					height = 18;
					maxStack = 99;
					value = 50000;
					rare = 1;
					toolTip = "Can be enchanted";
				}
				else if (type == 532)
				{
					name = "Star Cloak";
					width = 20;
					height = 24;
					value = 100000;
					toolTip = "Causes stars to fall when injured";
					accessory = true;
					rare = 4;
				}
				else if (type == 533)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 7;
					useTime = 7;
					name = "Megashark";
					width = 50;
					height = 18;
					shoot = 10;
					useAmmo = 14;
					useSound = 11;
					damage = 23;
					shootSpeed = 10f;
					noMelee = true;
					value = 300000;
					rare = 5;
					toolTip = "50% chance to not consume ammo";
					toolTip2 = "'Minishark's older brother'";
					knockBack = 1f;
					ranged = true;
				}
				else if (type == 534)
				{
					knockBack = 6.5f;
					useStyle = 5;
					useAnimation = 45;
					useTime = 45;
					name = "Shotgun";
					width = 50;
					height = 14;
					shoot = 10;
					useAmmo = 14;
					useSound = 36;
					damage = 18;
					shootSpeed = 6f;
					noMelee = true;
					value = 700000;
					rare = 4;
					ranged = true;
					toolTip = "Fires a spread of bullets";
				}
				else if (type == 535)
				{
					name = "Philosopher's Stone";
					width = 12;
					height = 18;
					value = 100000;
					toolTip = "Reduces the cooldown of healing potions";
					accessory = true;
					rare = 4;
				}
				else if (type == 536)
				{
					name = "Titan Glove";
					width = 12;
					height = 18;
					value = 100000;
					toolTip = "Increases melee knockback";
					rare = 4;
					accessory = true;
				}
				else if (type == 537)
				{
					name = "Cobalt Naginata";
					useStyle = 5;
					useAnimation = 28;
					useTime = 28;
					shootSpeed = 4.3f;
					knockBack = 4f;
					width = 40;
					height = 40;
					damage = 29;
					scale = 1.1f;
					useSound = 1;
					shoot = 97;
					rare = 4;
					value = 45000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 538)
				{
					name = "Switch";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 136;
					width = 12;
					height = 12;
					value = 2000;
					mech = true;
				}
				else if (type == 539)
				{
					name = "Dart Trap";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 137;
					width = 12;
					height = 12;
					value = 10000;
					mech = true;
				}
				else if (type == 540)
				{
					name = "Boulder";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 138;
					width = 12;
					height = 12;
					mech = true;
				}
				else if (type == 541)
				{
					name = "Green Pressure Plate";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 135;
					width = 12;
					height = 12;
					placeStyle = 1;
					mech = true;
					value = 5000;
					toolTip = "Activates when stepped on";
				}
				else if (type == 542)
				{
					name = "Gray Pressure Plate";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 135;
					width = 12;
					height = 12;
					placeStyle = 2;
					mech = true;
					value = 5000;
					toolTip = "Activates when stepped on";
				}
				else if (type == 543)
				{
					name = "Brown Pressure Plate";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 135;
					width = 12;
					height = 12;
					placeStyle = 3;
					mech = true;
					value = 5000;
					toolTip = "Activates when stepped on";
				}
				else if (type == 544)
				{
					useStyle = 4;
					name = "Mechanical Eye";
					width = 22;
					height = 14;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					maxStack = 20;
					toolTip = "Summons The Twins";
					rare = 3;
				}
				else if (type == 545)
				{
					name = "Cursed Arrow";
					shootSpeed = 4f;
					shoot = 103;
					damage = 14;
					width = 10;
					height = 28;
					maxStack = 250;
					consumable = true;
					ammo = 1;
					knockBack = 3f;
					value = 80;
					ranged = true;
					rare = 3;
				}
				else if (type == 546)
				{
					name = "Cursed Bullet";
					shootSpeed = 5f;
					shoot = 104;
					damage = 12;
					width = 8;
					height = 8;
					maxStack = 250;
					consumable = true;
					ammo = 14;
					knockBack = 4f;
					value = 30;
					rare = 1;
					ranged = true;
					rare = 3;
				}
				else if (type == 547)
				{
					name = "Soul of Fright";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 100000;
					rare = 5;
					toolTip = "'The essence of pure terror'";
				}
				else if (type == 548)
				{
					name = "Soul of Might";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 100000;
					rare = 5;
					toolTip = "'The essence of the destroyer'";
				}
				else if (type == 549)
				{
					name = "Soul of Sight";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 100000;
					rare = 5;
					toolTip = "'The essence of omniscient watchers'";
				}
				else if (type == 550)
				{
					name = "Gungnir";
					useStyle = 5;
					useAnimation = 22;
					useTime = 22;
					shootSpeed = 5.6f;
					knockBack = 6.4f;
					width = 40;
					height = 40;
					damage = 42;
					scale = 1.1f;
					useSound = 1;
					shoot = 105;
					rare = 5;
					value = 1500000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
				}
				else if (type == 551)
				{
					name = "Hallowed Plate Mail";
					width = 18;
					height = 18;
					defense = 15;
					bodySlot = 24;
					rare = 5;
					value = 200000;
					toolTip = "7% increased critical strike chance";
				}
				else if (type == 552)
				{
					name = "Hallowed Greaves";
					width = 18;
					height = 18;
					defense = 11;
					legSlot = 23;
					rare = 5;
					value = 150000;
					toolTip = "7% increased damage";
					toolTip2 = "8% increased movement speed";
				}
				else if (type == 553)
				{
					name = "Hallowed Helmet";
					width = 18;
					height = 18;
					defense = 9;
					headSlot = 41;
					rare = 5;
					value = 250000;
					toolTip = "15% increased ranged damage";
					toolTip2 = "8% increased ranged critical strike chance";
				}
				else if (type == 558)
				{
					name = "Hallowed Headgear";
					width = 18;
					height = 18;
					defense = 5;
					headSlot = 42;
					rare = 5;
					value = 250000;
					toolTip = "Increases maximum mana by 100";
					toolTip2 = "12% increased magic damage and critical strike chance";
				}
				else if (type == 559)
				{
					name = "Hallowed Mask";
					width = 18;
					height = 18;
					defense = 24;
					headSlot = 43;
					rare = 5;
					value = 250000;
					toolTip = "10% increased melee damage and critical strike chance";
					toolTip2 = "10% increased melee haste";
				}
				else if (type == 554)
				{
					name = "Cross Necklace";
					width = 20;
					height = 24;
					value = 1500;
					toolTip = "Increases length of invincibility after taking damage";
					accessory = true;
					rare = 4;
				}
				else if (type == 555)
				{
					name = "Mana Flower";
					width = 20;
					height = 24;
					value = 50000;
					toolTip = "8% reduced mana usage";
					toolTip2 = "Automatically use mana potions when needed";
					accessory = true;
					rare = 4;
				}
				else if (type == 556)
				{
					useStyle = 4;
					name = "Mechanical Worm";
					width = 22;
					height = 14;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					maxStack = 20;
					toolTip = "Summons Destroyer";
					rare = 3;
				}
				else if (type == 557)
				{
					useStyle = 4;
					name = "Mechanical Skull";
					width = 22;
					height = 14;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					maxStack = 20;
					toolTip = "Summons Skeletron Prime";
					rare = 3;
				}
				else if (type == 560)
				{
					useStyle = 4;
					name = "Slime Crown";
					width = 22;
					height = 14;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					maxStack = 20;
					toolTip = "Summons King Slime";
					rare = 1;
				}
				else if (type == 561)
				{
					melee = true;
					autoReuse = true;
					noMelee = true;
					useStyle = 1;
					name = "Light Disc";
					shootSpeed = 13f;
					shoot = 106;
					damage = 35;
					knockBack = 8f;
					width = 24;
					height = 24;
					useSound = 1;
					useAnimation = 15;
					useTime = 15;
					noUseGraphic = true;
					rare = 5;
					maxStack = 5;
					value = 500000;
					toolTip = "Stacks up to 5";
				}
				else if (type == 562)
				{
					name = "Music Box (Overworld Day)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 0;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 563)
				{
					name = "Music Box (Eerie)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 1;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 564)
				{
					name = "Music Box (Night)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 2;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 565)
				{
					name = "Music Box (Title)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 3;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 566)
				{
					name = "Music Box (Underground)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 4;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 567)
				{
					name = "Music Box (Boss 1)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 5;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 568)
				{
					name = "Music Box (Jungle)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 6;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 569)
				{
					name = "Music Box (Corruption)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 7;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 570)
				{
					name = "Music Box (Underground Corruption)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 8;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 571)
				{
					name = "Music Box (The Hallow)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 9;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 572)
				{
					name = "Music Box (Boss 2)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 10;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 573)
				{
					name = "Music Box (Underground Hallow)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 11;
					width = 24;
					height = 24;
					rare = 4;
					value = 100000;
					accessory = true;
				}
				else if (type == 574)
				{
					name = "Music Box (Boss 3)";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					consumable = true;
					createTile = 139;
					placeStyle = 12;
					width = 24;
					height = 24;
					rare = 3;
					value = 100000;
					accessory = true;
				}
				else if (type == 575)
				{
					name = "Soul of Flight";
					width = 18;
					height = 18;
					maxStack = 250;
					value = 1000;
					rare = 3;
					toolTip = "'The essence of powerful flying creatures'";
				}
				else if (type == 576)
				{
					name = "Music Box";
					width = 24;
					height = 24;
					rare = 3;
					toolTip = "Has a chance to record songs";
					value = 100000;
					accessory = true;
				}
				else if (type == 577)
				{
					name = "Demonite Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 140;
					width = 12;
					height = 12;
				}
				else if (type == 578)
				{
					useStyle = 5;
					autoReuse = true;
					useAnimation = 19;
					useTime = 19;
					name = "Hallowed Repeater";
					width = 50;
					height = 18;
					shoot = 1;
					useAmmo = 1;
					useSound = 5;
					damage = 39;
					shootSpeed = 11f;
					noMelee = true;
					value = 200000;
					ranged = true;
					rare = 4;
					knockBack = 2.5f;
				}
				else if (type == 579)
				{
					name = "Hamdrax";
					useStyle = 5;
					useAnimation = 25;
					useTime = 7;
					shootSpeed = 36f;
					knockBack = 4.75f;
					width = 20;
					height = 12;
					damage = 35;
					pick = 200;
					axe = 22;
					hammer = 85;
					useSound = 23;
					shoot = 107;
					rare = 4;
					value = 220000;
					noMelee = true;
					noUseGraphic = true;
					melee = true;
					channel = true;
					toolTip = "'Not to be confused with a hamsaw'";
				}
				else if (type == 580)
				{
					mech = true;
					name = "Explosives";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 141;
					width = 12;
					height = 12;
					toolTip = "Explodes when activated";
				}
				else if (type == 581)
				{
					mech = true;
					name = "Inlet Pump";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 142;
					width = 12;
					height = 12;
					toolTip = "Sends water to outlet pumps";
				}
				else if (type == 582)
				{
					mech = true;
					name = "Outlet Pump";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 143;
					width = 12;
					height = 12;
					toolTip = "Receives water from inlet pumps";
				}
				else if (type == 583)
				{
					mech = true;
					noWet = true;
					name = "1 Second Timer";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 144;
					placeStyle = 0;
					width = 10;
					height = 12;
					value = 50;
					toolTip = "Activates every second";
				}
				else if (type == 584)
				{
					mech = true;
					noWet = true;
					name = "3 Second Timer";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 144;
					placeStyle = 1;
					width = 10;
					height = 12;
					value = 50;
					toolTip = "Activates every 3 seconds";
				}
				else if (type == 585)
				{
					mech = true;
					noWet = true;
					name = "5 Second Timer";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 99;
					consumable = true;
					createTile = 144;
					placeStyle = 2;
					width = 10;
					height = 12;
					value = 50;
					toolTip = "Activates every 5 seconds";
				}
				else if (type == 586)
				{
					name = "Candy Cane Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 145;
					width = 12;
					height = 12;
				}
				else if (type == 587)
				{
					name = "Candy Cane Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 29;
					width = 12;
					height = 12;
				}
				else if (type == 588)
				{
					name = "Santa Hat";
					width = 18;
					height = 12;
					headSlot = 44;
					value = 150000;
					vanity = true;
				}
				else if (type == 589)
				{
					name = "Santa Shirt";
					width = 18;
					height = 18;
					bodySlot = 25;
					value = 150000;
					vanity = true;
				}
				else if (type == 590)
				{
					name = "Santa Pants";
					width = 18;
					height = 18;
					legSlot = 24;
					value = 150000;
					vanity = true;
				}
				else if (type == 591)
				{
					name = "Green Candy Cane Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 146;
					width = 12;
					height = 12;
				}
				else if (type == 592)
				{
					name = "Green Candy Cane Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 30;
					width = 12;
					height = 12;
				}
				else if (type == 593)
				{
					name = "Snow Block";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 147;
					width = 12;
					height = 12;
				}
				else if (type == 594)
				{
					name = "Snow Brick";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 148;
					width = 12;
					height = 12;
				}
				else if (type == 595)
				{
					name = "Snow Brick Wall";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createWall = 31;
					width = 12;
					height = 12;
				}
				else if (type == 596)
				{
					name = "Blue Light";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 149;
					placeStyle = 0;
					width = 12;
					height = 12;
					value = 500;
				}
				else if (type == 597)
				{
					name = "Red Light";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 149;
					placeStyle = 1;
					width = 12;
					height = 12;
					value = 500;
				}
				else if (type == 598)
				{
					name = "Green Light";
					useStyle = 1;
					useTurn = true;
					useAnimation = 15;
					useTime = 10;
					autoReuse = true;
					maxStack = 250;
					consumable = true;
					createTile = 149;
					placeStyle = 2;
					width = 12;
					height = 12;
					value = 500;
				}
				else if (type == 599)
				{
					name = "Blue Present";
					width = 12;
					height = 12;
					rare = 1;
					toolTip = "Right click to open";
				}
				else if (type == 600)
				{
					name = "Green Present";
					width = 12;
					height = 12;
					rare = 1;
					toolTip = "Right click to open";
				}
				else if (type == 601)
				{
					name = "Yellow Present";
					width = 12;
					height = 12;
					rare = 1;
					toolTip = "Right click to open";
				}
				else if (type == 602)
				{
					name = "Snow Globe";
					useStyle = 4;
					consumable = true;
					useAnimation = 45;
					useTime = 45;
					width = 28;
					height = 28;
					toolTip = "Summons the Frost Legion";
					rare = 2;
				}
				else if (type == 603)
				{
					damage = 0;
					useStyle = 1;
					name = "Carrot";
					shoot = 111;
					width = 16;
					height = 30;
					useSound = 2;
					useAnimation = 20;
					useTime = 20;
					rare = 3;
					noMelee = true;
					toolTip = "Summons a pet bunny";
					value = 0;
					buffType = 40;
				}
				if (!noMatCheck)
				{
					checkMat();
				}
				netID = type;
				if (saveData && type != 0 && (Config.generateINI || Config.generateItemObj))
				{
					Config.ProcessINI(this, "Item", name, "Defaults");
				}
			}
			else if (Type > 0)
			{
				if (Config.itemDefs[Type] == null)
				{
					SetDefaults(0);
					return;
				}
				Config.CopyAttributes(this, Config.itemDefs[Type], "Item");
				Init(type);
				RunMethod("Initialize");
			}
			else
			{
				Reset();
				displayName = "";
				netID = 0;
				prefix = 0;
				crit = 0;
				mech = false;
				reuseDelay = 0;
				melee = false;
				magic = false;
				ranged = false;
				placeStyle = 0;
				buffTime = 0;
				buffType = 0;
				material = false;
				noWet = false;
				vanity = false;
				mana = 0;
				wet = false;
				wetCount = 0;
				lavaWet = false;
				channel = false;
				manaIncrease = 0;
				release = 0;
				noMelee = false;
				noUseGraphic = false;
				lifeRegen = 0;
				shootSpeed = 0f;
				active = true;
				alpha = 0;
				ammo = 0;
				useAmmo = 0;
				autoReuse = false;
				accessory = false;
				axe = 0;
				healMana = 0;
				bodySlot = -1;
				legSlot = -1;
				headSlot = -1;
				potion = false;
				color = default(Color);
				consumable = false;
				createTile = -1;
				createWall = -1;
				damage = -1;
				defense = 0;
				hammer = 0;
				healLife = 0;
				holdStyle = 0;
				knockBack = 0f;
				maxStack = 1;
				pick = 0;
				rare = 0;
				scale = 1f;
				shoot = 0;
				stack = 1;
				toolTip = null;
				toolTip2 = null;
				tileBoost = 0;
				type = Type;
				useStyle = 0;
				useSound = 0;
				useTime = 100;
				useAnimation = 100;
				value = 0;
				useTurn = false;
				buy = false;
				name = "";
				stack = 0;
			}
		}

		public static string VersionName(string oldName, int release)
		{
			string result = oldName;
			if (release <= 4)
			{
				switch (oldName)
				{
				case "Cobalt Helmet":
					result = "Jungle Hat";
					break;
				case "Cobalt Breastplate":
					result = "Jungle Shirt";
					break;
				case "Cobalt Greaves":
					result = "Jungle Pants";
					break;
				}
			}
			if (release <= 13 && oldName == "Jungle Rose")
			{
				result = "Jungle Spores";
			}
			if (release <= 20)
			{
				switch (oldName)
				{
				case "Gills potion":
					result = "Gills Potion";
					break;
				case "Thorn Chakrum":
					result = "Thorn Chakram";
					break;
				case "Ball 'O Hurt":
					result = "Ball O' Hurt";
					break;
				}
			}
			return result;
		}

		public Color GetAlpha(Color newColor)
		{
			if (type == 75)
			{
				return new Color(255, 255, 255, newColor.A - alpha);
			}
			if (type == 121 || type == 122 || type == 217 || type == 218 || type == 219 || type == 220 || type == 120 || type == 119)
			{
				return new Color(255, 255, 255, 255);
			}
			if (type == 501)
			{
				return new Color(200, 200, 200, 50);
			}
			if (type == 520 || type == 521 || type == 522 || type == 547 || type == 548 || type == 549 || type == 575)
			{
				return new Color(255, 255, 255, 50);
			}
			if (type == 58 || type == 184)
			{
				return new Color(200, 200, 200, 2000);
			}
			float num = (float)(255 - alpha) / 255f;
			int r = (int)((float)(int)newColor.R * num);
			int g = (int)((float)(int)newColor.G * num);
			int b = (int)((float)(int)newColor.B * num);
			int num2 = newColor.A - alpha;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (type >= 198 && type <= 203)
			{
				return Color.White;
			}
			return new Color(r, g, b, num2);
		}

		public Color GetColor(Color newColor)
		{
			int num = color.R - (255 - newColor.R);
			int num2 = color.G - (255 - newColor.G);
			int num3 = color.B - (255 - newColor.B);
			int num4 = color.A - (255 - newColor.A);
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			return new Color(num, num2, num3, num4);
		}

		public static bool MechSpawn(float x, float y, int type)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 200; i++)
			{
				if (Main.item[i].active && Main.item[i].type == type)
				{
					num++;
					Vector2 vector = new Vector2(x, y);
					float num4 = Main.item[i].position.X - vector.X;
					float num5 = Main.item[i].position.Y - vector.Y;
					float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
					if (num6 < 300f)
					{
						num2++;
					}
					if (num6 < 800f)
					{
						num3++;
					}
				}
			}
			if (num2 < 3 && num3 < 6)
			{
				return num < 10;
			}
			return false;
		}

		public void UpdateItem(int i)
		{
			if (!active)
			{
				return;
			}
			int num = type;
			int num2 = type;
			if (name != null && Config.itemDefs.pretendType.TryGetValue(name, out num2))
			{
				type = num2;
			}
			if (Main.netMode == 0)
			{
				owner = Main.myPlayer;
			}
			float num3 = 0.1f;
			float num4 = 7f;
			int num5 = (int)(position.X + (float)(width / 2)) / 16;
			int num6 = (int)(position.Y + (float)(height / 2)) / 16;
			if (Main.tile[num5, num6] == null)
			{
				num3 = 0f;
				base.velocity.X = 0f;
				base.velocity.Y = 0f;
			}
			if (wet)
			{
				num4 = 5f;
				num3 = 0.08f;
			}
			Vector2 vector = base.velocity * 0.5f;
			if (ownTime > 0)
			{
				ownTime--;
			}
			else
			{
				ownIgnore = -1;
			}
			if (keepTime > 0)
			{
				keepTime--;
			}
			if (!beingGrabbed)
			{
				bool LetUpdate = true;
				int LavaImmunity = 0;
				int MovementType = 0;
				UpdateItem_Custom(i, ref LetUpdate, ref MovementType, ref LavaImmunity);
				if (LetUpdate)
				{
					if (type == 520 || type == 521 || type == 547 || type == 548 || type == 549 || type == 575 || MovementType == 1)
					{
						base.velocity.X = base.velocity.X * 0.95f;
						if ((double)base.velocity.X < 0.1 && (double)base.velocity.X > -0.1)
						{
							base.velocity.X = 0f;
						}
						base.velocity.Y = base.velocity.Y * 0.95f;
						if ((double)base.velocity.Y < 0.1 && (double)base.velocity.Y > -0.1)
						{
							base.velocity.Y = 0f;
						}
					}
					else if (MovementType == 0)
					{
						base.velocity.Y = base.velocity.Y + num3;
						if (base.velocity.Y > num4)
						{
							base.velocity.Y = num4;
						}
						base.velocity.X = base.velocity.X * 0.95f;
						if ((double)base.velocity.X < 0.1 && (double)base.velocity.X > -0.1)
						{
							base.velocity.X = 0f;
						}
					}
					bool flag = Collision.LavaCollision(position, width, height);
					if (flag)
					{
						lavaWet = true;
					}
					if (Collision.WetCollision(position, width, height))
					{
						if (!wet)
						{
							if (wetCount == 0)
							{
								wetCount = 20;
								if (!flag)
								{
									for (int j = 0; j < 10; j++)
									{
										int num7 = Dust.NewDust(new Vector2(position.X - 6f, position.Y + (float)(height / 2) - 8f), width + 12, 24, 33);
										Dust dust = Main.dust[num7];
										dust.velocity.Y = dust.velocity.Y - 4f;
										Dust dust2 = Main.dust[num7];
										dust2.velocity.X = dust2.velocity.X * 2.5f;
										Main.dust[num7].scale = 1.3f;
										Main.dust[num7].alpha = 100;
										Main.dust[num7].noGravity = true;
									}
									Main.PlaySound(19, (int)position.X, (int)position.Y);
								}
								else
								{
									for (int k = 0; k < 5; k++)
									{
										int num8 = Dust.NewDust(new Vector2(position.X - 6f, position.Y + (float)(height / 2) - 8f), width + 12, 24, 35);
										Dust dust3 = Main.dust[num8];
										dust3.velocity.Y = dust3.velocity.Y - 1.5f;
										Dust dust4 = Main.dust[num8];
										dust4.velocity.X = dust4.velocity.X * 2.5f;
										Main.dust[num8].scale = 1.3f;
										Main.dust[num8].alpha = 100;
										Main.dust[num8].noGravity = true;
									}
									Main.PlaySound(19, (int)position.X, (int)position.Y);
								}
							}
							wet = true;
						}
					}
					else if (wet)
					{
						wet = false;
					}
					if (!wet)
					{
						lavaWet = false;
					}
					if (wetCount > 0)
					{
						wetCount--;
					}
					if (wet)
					{
						Vector2 velocity = base.velocity;
						base.velocity = Collision.TileCollision(position, base.velocity, width, height);
						if (base.velocity.X != velocity.X)
						{
							vector.X = base.velocity.X;
						}
						if (base.velocity.Y != velocity.Y)
						{
							vector.Y = base.velocity.Y;
						}
					}
					else
					{
						base.velocity = Collision.TileCollision(position, base.velocity, width, height);
					}
					if (lavaWet && LavaImmunity < 1)
					{
						if (type == 267)
						{
							if (Main.netMode != 1)
							{
								active = false;
								type = 0;
								name = "";
								stack = 0;
								for (int l = 0; l < 200; l++)
								{
									if (Main.npc[l].active && Main.npc[l].type == 22)
									{
										if (Main.netMode == 2)
										{
											NetMessage.SendData(28, -1, -1, "", l, 1f, 10f, 0f - (float)Main.npc[l].direction, 9999);
										}
										Main.npc[l].StrikeNPC(9999, 10f, -Main.npc[l].direction);
										NPC.SpawnWOF(position);
									}
								}
								NetMessage.SendData(21, -1, -1, "", i);
							}
						}
						else if (owner == Main.myPlayer && (LavaImmunity != 0 || (type != 312 && type != 318 && type != 173 && type != 174 && type != 175 && rare == 0)))
						{
							active = false;
							type = 0;
							name = "";
							stack = 0;
							if (Main.netMode != 0)
							{
								NetMessage.SendData(21, -1, -1, "", i);
							}
						}
					}
					if (type == 520)
					{
						float num9 = (float)Main.rand.Next(90, 111) * 0.01f;
						num9 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.5f * num9, 0.1f * num9, 0.25f * num9);
					}
					else if (type == 521)
					{
						float num10 = (float)Main.rand.Next(90, 111) * 0.01f;
						num10 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.25f * num10, 0.1f * num10, 0.5f * num10);
					}
					else if (type == 547)
					{
						float num11 = (float)Main.rand.Next(90, 111) * 0.01f;
						num11 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.5f * num11, 0.3f * num11, 0.05f * num11);
					}
					else if (type == 548)
					{
						float num12 = (float)Main.rand.Next(90, 111) * 0.01f;
						num12 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.1f * num12, 0.1f * num12, 0.6f * num12);
					}
					else if (type == 575)
					{
						float num13 = (float)Main.rand.Next(90, 111) * 0.01f;
						num13 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.1f * num13, 0.3f * num13, 0.5f * num13);
					}
					else if (type == 549)
					{
						float num14 = (float)Main.rand.Next(90, 111) * 0.01f;
						num14 *= Main.essScale;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.1f * num14, 0.5f * num14, 0.2f * num14);
					}
					else if (type == 58)
					{
						float num15 = (float)Main.rand.Next(90, 111) * 0.01f;
						num15 *= Main.essScale * 0.5f;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.5f * num15, 0.1f * num15, 0.1f * num15);
					}
					else if (type == 184)
					{
						float num16 = (float)Main.rand.Next(90, 111) * 0.01f;
						num16 *= Main.essScale * 0.5f;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.1f * num16, 0.1f * num16, 0.5f * num16);
					}
					else if (type == 522)
					{
						float num17 = (float)Main.rand.Next(90, 111) * 0.01f;
						num17 *= Main.essScale * 0.2f;
						Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.5f * num17, 1f * num17, 0.1f * num17);
					}
					if (type == 75 && Main.dayTime)
					{
						for (int m = 0; m < 10; m++)
						{
							Dust.NewDust(position, width, height, 15, base.velocity.X, base.velocity.Y, 150, default(Color), 1.2f);
						}
						for (int n = 0; n < 3; n++)
						{
							Gore.NewGore(position, new Vector2(base.velocity.X, base.velocity.Y), Main.rand.Next(16, 18));
						}
						active = false;
						type = 0;
						stack = 0;
						if (Main.netMode == 2)
						{
							NetMessage.SendData(21, -1, -1, "", i);
						}
					}
				}
			}
			else
			{
				beingGrabbed = false;
			}
			if (type == 501)
			{
				if (Main.rand.Next(6) == 0)
				{
					int num18 = Dust.NewDust(position, width, height, 55, 0f, 0f, 200, color);
					Dust dust5 = Main.dust[num18];
					dust5.velocity *= 0.3f;
					Main.dust[num18].scale *= 0.5f;
				}
			}
			else if (type == 8 || type == 105)
			{
				if (!wet)
				{
					Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 1f, 0.95f, 0.8f);
				}
			}
			else if (type == 523)
			{
				Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.85f, 1f, 0.7f);
			}
			else if (type >= 427 && type <= 432)
			{
				if (!wet)
				{
					float r = 0f;
					float g = 0f;
					float b = 0f;
					int num19 = type - 426;
					if (num19 == 1)
					{
						r = 0.1f;
						g = 0.2f;
						b = 1.1f;
					}
					if (num19 == 2)
					{
						r = 1f;
						g = 0.1f;
						b = 0.1f;
					}
					if (num19 == 3)
					{
						r = 0f;
						g = 1f;
						b = 0.1f;
					}
					if (num19 == 4)
					{
						r = 0.9f;
						g = 0f;
						b = 0.9f;
					}
					if (num19 == 5)
					{
						r = 1.3f;
						g = 1.3f;
						b = 1.3f;
					}
					if (num19 == 6)
					{
						r = 0.9f;
						g = 0.9f;
						b = 0f;
					}
					Lighting.addLight((int)((position.X + (float)(width / 2)) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), r, g, b);
				}
			}
			else if (type == 41)
			{
				if (!wet)
				{
					Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 1f, 0.75f, 0.55f);
				}
			}
			else if (type == 282)
			{
				Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.7f, 1f, 0.8f);
			}
			else if (type == 286)
			{
				Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.7f, 0.8f, 1f);
			}
			else if (type == 331)
			{
				Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.55f, 0.75f, 0.6f);
			}
			else if (type == 183)
			{
				Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.15f, 0.45f, 0.9f);
			}
			else if (type == 75)
			{
				Lighting.addLight((int)((position.X + (float)width) / 16f), (int)((position.Y + (float)(height / 2)) / 16f), 0.8f, 0.7f, 0.1f);
			}
			if (type == 75)
			{
				if (Main.rand.Next(25) == 0)
				{
					Dust.NewDust(position, width, height, 58, base.velocity.X * 0.5f, base.velocity.Y * 0.5f, 150, default(Color), 1.2f);
				}
				if (Main.rand.Next(50) == 0)
				{
					Gore.NewGore(position, new Vector2(base.velocity.X * 0.2f, base.velocity.Y * 0.2f), Main.rand.Next(16, 18));
				}
			}
			if (spawnTime < 2147483646)
			{
				spawnTime++;
			}
			if (Main.netMode == 2 && owner != Main.myPlayer)
			{
				release++;
				if (release >= 300)
				{
					release = 0;
					NetMessage.SendData(39, owner, -1, "", i);
				}
			}
			if (wet)
			{
				position += vector;
			}
			else
			{
				position += base.velocity;
			}
			if (noGrabDelay > 0)
			{
				noGrabDelay--;
			}
			if (type == num2)
			{
				type = num;
			}
		}

		public static int NewItem(int X, int Y, int Width, int Height, string name, int Stack = 1, bool noBroadcast = false, int pfix = 0)
		{
			return NewItem(X, Y, Width, Height, Config.itemDefs.byName[name].type, Stack, noBroadcast, pfix);
		}

		public static int NewItem(int X, int Y, int Width, int Height, int Type, int Stack = 1, bool noBroadcast = false, int pfix = 0)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random();
			}
			if (WorldGen.gen)
			{
				return 0;
			}
			int num = 200;
			Main.item[200] = new Item();
			if (Main.netMode != 1)
			{
				for (int i = 0; i < 200; i++)
				{
					if (!Main.item[i].active)
					{
						num = i;
						break;
					}
				}
			}
			if (num == 200 && Main.netMode != 1)
			{
				if (Codable.RunGlobalMethod("ModWorld", "OverrideItemRemove"))
				{
					int num2 = (int)Codable.customMethodReturn;
					if (num2 >= 0 && num2 <= 199)
					{
						num = num2;
					}
					else if (Main.netMode != 1)
					{
						for (int j = 0; j < 200; j++)
						{
							if (!Main.item[j].active)
							{
								num = j;
								break;
							}
						}
					}
				}
				else
				{
					int num3 = 0;
					for (int k = 0; k < 200; k++)
					{
						if (Main.item[k].spawnTime > num3)
						{
							num3 = Main.item[k].spawnTime;
							num = k;
						}
					}
				}
				TMod.RunMethod(TMod.WorldHooks.RemoveOldItemInWorld, Main.item[num], num);
			}
			Main.item[num] = new Item();
			Main.item[num].SetDefaults(Type);
			Main.item[num].Prefix(pfix);
			Main.item[num].position.X = X + Width / 2 - Main.item[num].width / 2;
			Main.item[num].position.Y = Y + Height / 2 - Main.item[num].height / 2;
			Main.item[num].wet = Collision.WetCollision(Main.item[num].position, Main.item[num].width, Main.item[num].height);
			Main.item[num].velocity.X = (float)Main.rand.Next(-30, 31) * 0.1f;
			Main.item[num].velocity.Y = (float)Main.rand.Next(-40, -15) * 0.1f;
			if (Type == 520 || Type == 521)
			{
				Main.item[num].velocity.X = (float)Main.rand.Next(-30, 31) * 0.1f;
				Main.item[num].velocity.Y = (float)Main.rand.Next(-30, 31) * 0.1f;
			}
			Main.item[num].active = true;
			Main.item[num].spawnTime = 0;
			Main.item[num].stack = Stack;
			if (Main.netMode == 2 && !noBroadcast)
			{
				NetMessage.SendData(21, -1, -1, "", num);
				Main.item[num].FindOwner(num);
			}
			else if (Main.netMode == 0)
			{
				Main.item[num].owner = Main.myPlayer;
			}
			return num;
		}

		public static int NewItem(int X, int Y, int Width, int Height, Item item, bool noBroadcast = false)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random();
			}
			if (WorldGen.gen)
			{
				return 0;
			}
			int num = 200;
			Main.item[200] = new Item();
			if (Main.netMode != 1)
			{
				for (int i = 0; i < 200; i++)
				{
					if (!Main.item[i].active)
					{
						num = i;
						break;
					}
				}
			}
			if (num == 200 && Main.netMode != 1)
			{
				int num2 = 0;
				for (int j = 0; j < 200; j++)
				{
					if (Main.item[j].spawnTime > num2)
					{
						num2 = Main.item[j].spawnTime;
						num = j;
					}
				}
				TMod.RunMethod(TMod.WorldHooks.RemoveOldItemInWorld, Main.item[num], num);
			}
			Main.item[num] = item;
			Main.item[num].position.X = X + Width / 2 - Main.item[num].width / 2;
			Main.item[num].position.Y = Y + Height / 2 - Main.item[num].height / 2;
			Main.item[num].wet = Collision.WetCollision(Main.item[num].position, Main.item[num].width, Main.item[num].height);
			Main.item[num].velocity.X = (float)Main.rand.Next(-30, 31) * 0.1f;
			Main.item[num].velocity.Y = (float)Main.rand.Next(-40, -15) * 0.1f;
			Main.item[num].active = true;
			Main.item[num].spawnTime = 0;
			if (Main.netMode == 2 && !noBroadcast)
			{
				NetMessage.SendData(21, -1, -1, "", num);
				Main.item[num].FindOwner(num);
			}
			else if (Main.netMode == 0)
			{
				Main.item[num].owner = Main.myPlayer;
			}
			return num;
		}

		public void FindOwner(int whoAmI)
		{
			if (keepTime > 0)
			{
				return;
			}
			int num = owner;
			owner = 255;
			float num2 = -1f;
			for (int i = 0; i < 255; i++)
			{
				if (ownIgnore != i && Main.player[i].active && Main.player[i].ItemSpace(Main.item[whoAmI]))
				{
					float num3 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - position.X - (float)(width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - position.Y - (float)height);
					if (num3 < (float)NPC.sWidth && (num2 == -1f || num3 < num2))
					{
						num2 = num3;
						owner = i;
					}
				}
			}
			if (owner != num && ((num == Main.myPlayer && Main.netMode == 1) || (num == 255 && Main.netMode == 2) || !Main.player[num].active))
			{
				NetMessage.SendData(21, -1, -1, "", whoAmI);
				if (active)
				{
					NetMessage.SendData(22, -1, -1, "", whoAmI);
				}
			}
		}

		public object ShallowClone()
		{
			return (Item)MemberwiseClone();
		}

		public object Clone()
		{
			return ShallowClone();
		}

		public Item CloneItem()
		{
			Item item = new Item();
			if (IsBlankItem())
			{
				return item;
			}
			item.SetDefaults(name);
			item.stack = stack;
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			Terraria.Prefix.SavePrefix(binaryWriter, this);
			Codable.SaveCustomData(this, binaryWriter);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			Terraria.Prefix.LoadPrefix(binaryReader, item, "player");
			Codable.LoadCustomData(item, binaryReader, 5, forceItemLoad: true);
			item.value = value;
			return item;
		}

		public bool IsBlankItem()
		{
			if (type != 0 && name != null && !(name == "") && !(name == "Unloaded Item"))
			{
				return stack <= 0;
			}
			return true;
		}

		public bool IsTheSameAs(Item compareItem)
		{
			return netID == compareItem.netID;
		}

		public bool IsNotTheSameAs(Item compareItem)
		{
			if (netID == compareItem.netID && stack == compareItem.stack)
			{
				return prefix != compareItem.prefix;
			}
			return true;
		}
	}
}
