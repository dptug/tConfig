using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria
{
	public class Projectile : Codable
	{
		public delegate void DamageNPC_Del(NPC npc, ref int damage, ref float knockback);

		public delegate void DealtNPC_Del(NPC npc, double damage, Player player);

		public delegate void DamagePlayer_Del(ref int damage, Player player);

		public delegate void DealtPlayer_Del(double damage, Player player);

		public Action AI_Custom;

		public Func<bool> PreAI;

		public Action PostAI;

		public DamageNPC_Del DamageNPC;

		public DealtNPC_Del DealtNPC;

		public DamagePlayer_Del DamagePlayer;

		public DealtPlayer_Del DealtPlayer;

		public Action Kill_Custom;

		public DamagePlayer_Del DamagePVP;

		public DealtPlayer_Del DealtPVP;

		public Func<SpriteBatch, bool> PreDraw;

		public Action<SpriteBatch> PostDraw;

		public Item CallbackItem;

		public int whoAmI;

		public static int maxAI = 2;

		public Vector2 lastPosition;

		public float scale = 1f;

		public float rotation;

		public int type;

		public int alpha;

		public int owner = 255;

		public float[] ai = new float[maxAI];

		public float[] localAI = new float[maxAI];

		public int aiStyle;

		public int timeLeft;

		public int soundDelay;

		public int damage;

		public int spriteDirection = 1;

		public bool hostile;

		public float knockBack;

		public bool friendly;

		public int penetrate = 1;

		public int identity;

		public float light;

		public bool netUpdate;

		public bool netUpdate2;

		public int netSpam;

		public Vector2[] oldPos = new Vector2[10];

		public int restrikeDelay;

		public bool tileCollide;

		public int maxUpdates;

		public int numUpdates;

		public bool ignoreWater;

		public bool hide;

		public bool ownerHitCheck;

		public int[] playerImmune = new int[255];

		public string miscText = "";

		public bool melee;

		public bool ranged;

		public bool magic;

		public int frameCounter;

		public int frame;

		public string displayName;

		public Color color;

		public bool hurtsTiles = true;

		public override void ResetEvents()
		{
			PreDraw = null;
			PostDraw = null;
			AI_Custom = null;
			PreAI = null;
			PostAI = null;
			DamageNPC = null;
			DealtNPC = null;
			DamagePlayer = null;
			DealtPlayer = null;
			Kill_Custom = null;
			DamagePVP = null;
			DealtPVP = null;
		}

		public override void SetDefaultEvents()
		{
		}

		public override void RegisterEvents(object code)
		{
			if (code != null)
			{
				Register(ref AI_Custom, code, "AI");
				Register(ref PreAI, code, "PreAI");
				Register(ref PostAI, code, "PostAI");
				Register(ref DamageNPC, code, "DamageNPC");
				Register(ref DealtNPC, code, "DealtNPC");
				Register(ref DamagePlayer, code, "DamagePlayer");
				Register(ref DealtPlayer, code, "DealtPlayer");
				Register(ref Kill_Custom, code, "Kill");
				Register(ref DamagePVP, code, "DamagePVP");
				Register(ref DealtPVP, code, "DealtPVP");
				Register(ref PreDraw, code, "PreDraw");
				Register(ref PostDraw, code, "PostDraw");
			}
		}

		public override string ToString()
		{
			return "Projectile " + whoAmI + " " + name + " [" + displayName + "]";
		}

		public new void Init(int type = -1)
		{
			className = "Projectile";
			base.Init(type);
		}

		public Projectile()
		{
			className = "Projectile";
		}

		public void SetDefaults(int Type)
		{
			Reset();
			className = "Projectile";
			CallbackItem = null;
			displayName = "";
			for (int i = 0; i < oldPos.Length; i++)
			{
				oldPos[i].X = 0f;
				oldPos[i].Y = 0f;
			}
			for (int j = 0; j < maxAI; j++)
			{
				ai[j] = 0f;
				localAI[j] = 0f;
			}
			for (int k = 0; k < 255; k++)
			{
				playerImmune[k] = 0;
			}
			color = default(Color);
			hurtsTiles = true;
			soundDelay = 0;
			spriteDirection = 1;
			melee = false;
			ranged = false;
			magic = false;
			ownerHitCheck = false;
			hide = false;
			lavaWet = false;
			wetCount = 0;
			wet = false;
			ignoreWater = false;
			hostile = false;
			netUpdate = false;
			netUpdate2 = false;
			netSpam = 0;
			numUpdates = 0;
			maxUpdates = 0;
			identity = 0;
			restrikeDelay = 0;
			light = 0f;
			penetrate = 1;
			tileCollide = true;
			position = default(Vector2);
			velocity = default(Vector2);
			aiStyle = 0;
			alpha = 0;
			type = Type;
			active = true;
			rotation = 0f;
			scale = 1f;
			owner = 255;
			timeLeft = 3600;
			name = "";
			friendly = false;
			damage = 0;
			knockBack = 0f;
			miscText = "";
			if (displayName == null || displayName == "")
			{
				displayName = name;
			}
			if (Config.generateItemObj || Config.generateINI || !Config.initialized)
			{
				if (type == 1)
				{
					name = "Wooden Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					ranged = true;
				}
				else if (type == 2)
				{
					name = "Fire Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					light = 1f;
					ranged = true;
				}
				else if (type == 3)
				{
					name = "Shuriken";
					width = 22;
					height = 22;
					aiStyle = 2;
					friendly = true;
					penetrate = 4;
					ranged = true;
				}
				else if (type == 4)
				{
					name = "Unholy Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					light = 0.35f;
					penetrate = 5;
					ranged = true;
				}
				else if (type == 5)
				{
					name = "Jester's Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					light = 0.4f;
					penetrate = -1;
					timeLeft = 40;
					alpha = 100;
					ignoreWater = true;
					ranged = true;
				}
				else if (type == 6)
				{
					name = "Enchanted Boomerang";
					width = 22;
					height = 22;
					aiStyle = 3;
					friendly = true;
					penetrate = -1;
					melee = true;
					light = 0.4f;
				}
				else if (type == 7 || type == 8)
				{
					name = "Vilethorn";
					width = 28;
					height = 28;
					aiStyle = 4;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					alpha = 255;
					ignoreWater = true;
					magic = true;
				}
				else if (type == 9)
				{
					name = "Starfury";
					width = 24;
					height = 24;
					aiStyle = 5;
					friendly = true;
					penetrate = 2;
					alpha = 50;
					scale = 0.8f;
					tileCollide = false;
					magic = true;
				}
				else if (type == 10)
				{
					name = "Purification Powder";
					width = 64;
					height = 64;
					aiStyle = 6;
					friendly = true;
					tileCollide = false;
					penetrate = -1;
					alpha = 255;
					ignoreWater = true;
				}
				else if (type == 11)
				{
					name = "Vile Powder";
					width = 48;
					height = 48;
					aiStyle = 6;
					friendly = true;
					tileCollide = false;
					penetrate = -1;
					alpha = 255;
					ignoreWater = true;
				}
				else if (type == 12)
				{
					name = "Falling Star";
					width = 16;
					height = 16;
					aiStyle = 5;
					friendly = true;
					penetrate = -1;
					alpha = 50;
					light = 1f;
				}
				else if (type == 13)
				{
					name = "Hook";
					width = 18;
					height = 18;
					aiStyle = 7;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					timeLeft *= 10;
				}
				else if (type == 14)
				{
					name = "Bullet";
					width = 4;
					height = 4;
					aiStyle = 1;
					friendly = true;
					penetrate = 1;
					light = 0.5f;
					alpha = 255;
					maxUpdates = 1;
					scale = 1.2f;
					timeLeft = 600;
					ranged = true;
				}
				else if (type == 15)
				{
					name = "Ball of Fire";
					width = 16;
					height = 16;
					aiStyle = 8;
					friendly = true;
					light = 0.8f;
					alpha = 100;
					magic = true;
				}
				else if (type == 16)
				{
					name = "Magic Missile";
					width = 10;
					height = 10;
					aiStyle = 9;
					friendly = true;
					light = 0.8f;
					alpha = 100;
					magic = true;
				}
				else if (type == 17)
				{
					name = "Dirt Ball";
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
				}
				else if (type == 18)
				{
					name = "Orb of Light";
					width = 32;
					height = 32;
					aiStyle = 11;
					friendly = true;
					light = 0.45f;
					alpha = 150;
					tileCollide = false;
					penetrate = -1;
					timeLeft *= 5;
					ignoreWater = true;
					scale = 0.8f;
				}
				else if (type == 19)
				{
					name = "Flamarang";
					width = 22;
					height = 22;
					aiStyle = 3;
					friendly = true;
					penetrate = -1;
					light = 1f;
					melee = true;
				}
				else if (type == 20)
				{
					name = "Green Laser";
					width = 4;
					height = 4;
					aiStyle = 1;
					friendly = true;
					penetrate = 3;
					light = 0.75f;
					alpha = 255;
					maxUpdates = 2;
					scale = 1.4f;
					timeLeft = 600;
					magic = true;
				}
				else if (type == 21)
				{
					name = "Bone";
					width = 16;
					height = 16;
					aiStyle = 2;
					scale = 1.2f;
					friendly = true;
					ranged = true;
				}
				else if (type == 22)
				{
					name = "Water Stream";
					width = 18;
					height = 18;
					aiStyle = 12;
					friendly = true;
					alpha = 255;
					penetrate = -1;
					maxUpdates = 2;
					ignoreWater = true;
					magic = true;
				}
				else if (type == 23)
				{
					name = "Harpoon";
					width = 4;
					height = 4;
					aiStyle = 13;
					friendly = true;
					penetrate = -1;
					alpha = 255;
					ranged = true;
				}
				else if (type == 24)
				{
					name = "Spiky Ball";
					width = 14;
					height = 14;
					aiStyle = 14;
					friendly = true;
					penetrate = 6;
					ranged = true;
				}
				else if (type == 25)
				{
					name = "Ball 'O Hurt";
					width = 22;
					height = 22;
					aiStyle = 15;
					friendly = true;
					penetrate = -1;
					melee = true;
					scale = 0.8f;
				}
				else if (type == 26)
				{
					name = "Blue Moon";
					width = 22;
					height = 22;
					aiStyle = 15;
					friendly = true;
					penetrate = -1;
					melee = true;
					scale = 0.8f;
				}
				else if (type == 27)
				{
					name = "Water Bolt";
					width = 16;
					height = 16;
					aiStyle = 8;
					friendly = true;
					light = 0.8f;
					alpha = 200;
					timeLeft /= 2;
					penetrate = 10;
					magic = true;
				}
				else if (type == 28)
				{
					name = "Bomb";
					width = 22;
					height = 22;
					aiStyle = 16;
					friendly = true;
					penetrate = -1;
				}
				else if (type == 29)
				{
					name = "Dynamite";
					width = 10;
					height = 10;
					aiStyle = 16;
					friendly = true;
					penetrate = -1;
				}
				else if (type == 30)
				{
					name = "Grenade";
					width = 14;
					height = 14;
					aiStyle = 16;
					friendly = true;
					penetrate = -1;
					ranged = true;
				}
				else if (type == 31)
				{
					name = "Sand Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 32)
				{
					name = "Ivy Whip";
					width = 18;
					height = 18;
					aiStyle = 7;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					timeLeft *= 10;
				}
				else if (type == 33)
				{
					name = "Thorn Chakrum";
					width = 28;
					height = 28;
					aiStyle = 3;
					friendly = true;
					scale = 0.9f;
					penetrate = -1;
					melee = true;
				}
				else if (type == 34)
				{
					name = "Flamelash";
					width = 14;
					height = 14;
					aiStyle = 9;
					friendly = true;
					light = 0.8f;
					alpha = 100;
					penetrate = 1;
					magic = true;
				}
				else if (type == 35)
				{
					name = "Sunfury";
					width = 22;
					height = 22;
					aiStyle = 15;
					friendly = true;
					penetrate = -1;
					melee = true;
					scale = 0.8f;
				}
				else if (type == 36)
				{
					name = "Meteor Shot";
					width = 4;
					height = 4;
					aiStyle = 1;
					friendly = true;
					penetrate = 2;
					light = 0.6f;
					alpha = 255;
					maxUpdates = 1;
					scale = 1.4f;
					timeLeft = 600;
					ranged = true;
				}
				else if (type == 37)
				{
					name = "Sticky Bomb";
					width = 22;
					height = 22;
					aiStyle = 16;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
				}
				else if (type == 38)
				{
					name = "Harpy Feather";
					width = 14;
					height = 14;
					aiStyle = 0;
					hostile = true;
					penetrate = -1;
					aiStyle = 1;
					tileCollide = true;
				}
				else if (type == 39)
				{
					name = "Mud Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 40)
				{
					name = "Ash Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 41)
				{
					name = "Hellfire Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					penetrate = -1;
					ranged = true;
					light = 0.3f;
				}
				else if (type == 42)
				{
					name = "Sand Ball";
					knockBack = 8f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					maxUpdates = 1;
				}
				else if (type == 43)
				{
					name = "Tombstone";
					knockBack = 12f;
					width = 24;
					height = 24;
					aiStyle = 17;
					penetrate = -1;
				}
				else if (type == 44)
				{
					name = "Demon Sickle";
					width = 48;
					height = 48;
					alpha = 100;
					light = 0.2f;
					aiStyle = 18;
					hostile = true;
					penetrate = -1;
					tileCollide = true;
					scale = 0.9f;
				}
				else if (type == 45)
				{
					name = "Demon Scythe";
					width = 48;
					height = 48;
					alpha = 100;
					light = 0.2f;
					aiStyle = 18;
					friendly = true;
					penetrate = 5;
					tileCollide = true;
					scale = 0.9f;
					magic = true;
				}
				else if (type == 46)
				{
					name = "Dark Lance";
					width = 20;
					height = 20;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.1f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 47)
				{
					name = "Trident";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.1f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 48)
				{
					name = "Throwing Knife";
					width = 12;
					height = 12;
					aiStyle = 2;
					friendly = true;
					penetrate = 2;
					ranged = true;
				}
				else if (type == 49)
				{
					name = "Spear";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.2f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 50)
				{
					name = "Glowstick";
					width = 6;
					height = 6;
					aiStyle = 14;
					penetrate = -1;
					alpha = 75;
					light = 1f;
					timeLeft *= 5;
				}
				else if (type == 51)
				{
					name = "Seed";
					width = 8;
					height = 8;
					aiStyle = 1;
					friendly = true;
				}
				else if (type == 52)
				{
					name = "Wooden Boomerang";
					width = 22;
					height = 22;
					aiStyle = 3;
					friendly = true;
					penetrate = -1;
					melee = true;
				}
				else if (type == 53)
				{
					name = "Sticky Glowstick";
					width = 6;
					height = 6;
					aiStyle = 14;
					penetrate = -1;
					alpha = 75;
					light = 1f;
					timeLeft *= 5;
					tileCollide = false;
				}
				else if (type == 54)
				{
					name = "Poisoned Knife";
					width = 12;
					height = 12;
					aiStyle = 2;
					friendly = true;
					penetrate = 2;
					ranged = true;
				}
				else if (type == 55)
				{
					name = "Stinger";
					width = 10;
					height = 10;
					aiStyle = 0;
					hostile = true;
					penetrate = -1;
					aiStyle = 1;
					tileCollide = true;
				}
				else if (type == 56)
				{
					name = "Ebonsand Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 57)
				{
					name = "Cobalt Chainsaw";
					width = 18;
					height = 18;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 58)
				{
					name = "Mythril Chainsaw";
					width = 18;
					height = 18;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 1.08f;
				}
				else if (type == 59)
				{
					name = "Cobalt Drill";
					width = 22;
					height = 22;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 0.9f;
				}
				else if (type == 60)
				{
					name = "Mythril Drill";
					width = 22;
					height = 22;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 0.9f;
				}
				else if (type == 61)
				{
					name = "Adamantite Chainsaw";
					width = 18;
					height = 18;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 1.16f;
				}
				else if (type == 62)
				{
					name = "Adamantite Drill";
					width = 22;
					height = 22;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 0.9f;
				}
				else if (type == 63)
				{
					name = "The Dao of Pow";
					width = 22;
					height = 22;
					aiStyle = 15;
					friendly = true;
					penetrate = -1;
					melee = true;
				}
				else if (type == 64)
				{
					name = "Mythril Halberd";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.25f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 65)
				{
					name = "Ebonsand Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					penetrate = -1;
					maxUpdates = 1;
				}
				else if (type == 66)
				{
					name = "Adamantite Glaive";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.27f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 67)
				{
					name = "Pearl Sand Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 68)
				{
					name = "Pearl Sand Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					penetrate = -1;
					maxUpdates = 1;
				}
				else if (type == 69)
				{
					name = "Holy Water";
					width = 14;
					height = 14;
					aiStyle = 2;
					friendly = true;
					penetrate = 1;
				}
				else if (type == 70)
				{
					name = "Unholy Water";
					width = 14;
					height = 14;
					aiStyle = 2;
					friendly = true;
					penetrate = 1;
				}
				else if (type == 71)
				{
					name = "Gravel Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					friendly = true;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 72)
				{
					name = "Blue Fairy";
					width = 18;
					height = 18;
					aiStyle = 11;
					friendly = true;
					light = 0.9f;
					tileCollide = false;
					penetrate = -1;
					timeLeft *= 5;
					ignoreWater = true;
					scale = 0.8f;
				}
				else if (type == 73 || type == 74)
				{
					name = "Hook";
					width = 18;
					height = 18;
					aiStyle = 7;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					timeLeft *= 10;
					light = 0.4f;
				}
				else if (type == 75)
				{
					name = "Happy Bomb";
					width = 22;
					height = 22;
					aiStyle = 16;
					hostile = true;
					penetrate = -1;
				}
				else if (type == 76 || type == 77 || type == 78)
				{
					if (type == 76)
					{
						width = 10;
						height = 22;
					}
					else if (type == 77)
					{
						width = 18;
						height = 24;
					}
					else
					{
						width = 22;
						height = 24;
					}
					name = "Note";
					aiStyle = 21;
					friendly = true;
					ranged = true;
					alpha = 100;
					light = 0.3f;
					penetrate = -1;
					timeLeft = 180;
				}
				else if (type == 79)
				{
					name = "Rainbow";
					width = 10;
					height = 10;
					aiStyle = 9;
					friendly = true;
					light = 0.8f;
					alpha = 255;
					magic = true;
				}
				else if (type == 80)
				{
					name = "Ice Block";
					width = 16;
					height = 16;
					aiStyle = 22;
					friendly = true;
					magic = true;
					tileCollide = false;
					light = 0.5f;
				}
				else if (type == 81)
				{
					name = "Wooden Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					hostile = true;
					ranged = true;
				}
				else if (type == 82)
				{
					name = "Flaming Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					hostile = true;
					ranged = true;
				}
				else if (type == 83)
				{
					name = "Eye Laser";
					width = 4;
					height = 4;
					aiStyle = 1;
					hostile = true;
					penetrate = 3;
					light = 0.75f;
					alpha = 255;
					maxUpdates = 2;
					scale = 1.7f;
					timeLeft = 600;
					magic = true;
				}
				else if (type == 84)
				{
					name = "Pink Laser";
					width = 4;
					height = 4;
					aiStyle = 1;
					hostile = true;
					penetrate = 3;
					light = 0.75f;
					alpha = 255;
					maxUpdates = 2;
					scale = 1.2f;
					timeLeft = 600;
					magic = true;
				}
				else if (type == 85)
				{
					name = "Flames";
					width = 6;
					height = 6;
					aiStyle = 23;
					friendly = true;
					alpha = 255;
					penetrate = 3;
					maxUpdates = 2;
					magic = true;
				}
				else if (type == 86)
				{
					name = "Pink Fairy";
					width = 18;
					height = 18;
					aiStyle = 11;
					friendly = true;
					light = 0.9f;
					tileCollide = false;
					penetrate = -1;
					timeLeft *= 5;
					ignoreWater = true;
					scale = 0.8f;
				}
				else if (type == 87)
				{
					name = "Pink Fairy";
					width = 18;
					height = 18;
					aiStyle = 11;
					friendly = true;
					light = 0.9f;
					tileCollide = false;
					penetrate = -1;
					timeLeft *= 5;
					ignoreWater = true;
					scale = 0.8f;
				}
				else if (type == 88)
				{
					name = "Purple Laser";
					width = 6;
					height = 6;
					aiStyle = 1;
					friendly = true;
					penetrate = 3;
					light = 0.75f;
					alpha = 255;
					maxUpdates = 4;
					scale = 1.4f;
					timeLeft = 600;
					magic = true;
				}
				else if (type == 89)
				{
					name = "Crystal Bullet";
					width = 4;
					height = 4;
					aiStyle = 1;
					friendly = true;
					penetrate = 1;
					light = 0.5f;
					alpha = 255;
					maxUpdates = 1;
					scale = 1.2f;
					timeLeft = 600;
					ranged = true;
				}
				else if (type == 90)
				{
					name = "Crystal Shard";
					width = 6;
					height = 6;
					aiStyle = 24;
					friendly = true;
					penetrate = 1;
					light = 0.5f;
					alpha = 50;
					scale = 1.2f;
					timeLeft = 600;
					ranged = true;
					tileCollide = false;
				}
				else if (type == 91)
				{
					name = "Holy Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					ranged = true;
				}
				else if (type == 92)
				{
					name = "Hallow Star";
					width = 24;
					height = 24;
					aiStyle = 5;
					friendly = true;
					penetrate = 2;
					alpha = 50;
					scale = 0.8f;
					tileCollide = false;
					magic = true;
				}
				else if (type == 93)
				{
					light = 0.15f;
					name = "Magic Dagger";
					width = 12;
					height = 12;
					aiStyle = 2;
					friendly = true;
					penetrate = 2;
					magic = true;
				}
				else if (type == 94)
				{
					ignoreWater = true;
					name = "Crystal Storm";
					width = 8;
					height = 8;
					aiStyle = 24;
					friendly = true;
					light = 0.5f;
					alpha = 50;
					scale = 1.2f;
					timeLeft = 600;
					magic = true;
					tileCollide = true;
					penetrate = 1;
				}
				else if (type == 95)
				{
					name = "Cursed Flame";
					width = 16;
					height = 16;
					aiStyle = 8;
					friendly = true;
					light = 0.8f;
					alpha = 100;
					magic = true;
					penetrate = 2;
				}
				else if (type == 96)
				{
					name = "Cursed Flame";
					width = 16;
					height = 16;
					aiStyle = 8;
					hostile = true;
					light = 0.8f;
					alpha = 100;
					magic = true;
					penetrate = -1;
					scale = 0.9f;
					scale = 1.3f;
				}
				else if (type == 97)
				{
					name = "Cobalt Naginata";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.1f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 98)
				{
					name = "Poison Dart";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					hostile = true;
					ranged = true;
					penetrate = -1;
				}
				else if (type == 99)
				{
					name = "Boulder";
					width = 31;
					height = 31;
					aiStyle = 25;
					friendly = true;
					hostile = true;
					ranged = true;
					penetrate = -1;
				}
				else if (type == 100)
				{
					name = "Death Laser";
					width = 4;
					height = 4;
					aiStyle = 1;
					hostile = true;
					penetrate = 3;
					light = 0.75f;
					alpha = 255;
					maxUpdates = 2;
					scale = 1.8f;
					timeLeft = 1200;
					magic = true;
				}
				else if (type == 101)
				{
					name = "Eye Fire";
					width = 6;
					height = 6;
					aiStyle = 23;
					hostile = true;
					alpha = 255;
					penetrate = -1;
					maxUpdates = 3;
					magic = true;
				}
				else if (type == 102)
				{
					name = "Bomb";
					width = 22;
					height = 22;
					aiStyle = 16;
					hostile = true;
					penetrate = -1;
					ranged = true;
				}
				else if (type == 103)
				{
					name = "Cursed Arrow";
					width = 10;
					height = 10;
					aiStyle = 1;
					friendly = true;
					light = 1f;
					ranged = true;
				}
				else if (type == 104)
				{
					name = "Cursed Bullet";
					width = 4;
					height = 4;
					aiStyle = 1;
					friendly = true;
					penetrate = 1;
					light = 0.5f;
					alpha = 255;
					maxUpdates = 1;
					scale = 1.2f;
					timeLeft = 600;
					ranged = true;
				}
				else if (type == 105)
				{
					name = "Gungnir";
					width = 18;
					height = 18;
					aiStyle = 19;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					scale = 1.3f;
					hide = true;
					ownerHitCheck = true;
					melee = true;
				}
				else if (type == 106)
				{
					name = "Light Disc";
					width = 32;
					height = 32;
					aiStyle = 3;
					friendly = true;
					penetrate = -1;
					melee = true;
					light = 0.4f;
				}
				else if (type == 107)
				{
					name = "Hamdrax";
					width = 22;
					height = 22;
					aiStyle = 20;
					friendly = true;
					penetrate = -1;
					tileCollide = false;
					hide = true;
					ownerHitCheck = true;
					melee = true;
					scale = 1.1f;
				}
				else if (type == 108)
				{
					name = "Explosives";
					width = 260;
					height = 260;
					aiStyle = 16;
					friendly = true;
					hostile = true;
					penetrate = -1;
					tileCollide = false;
					alpha = 255;
					timeLeft = 2;
				}
				else if (type == 109)
				{
					name = "Snow Ball";
					knockBack = 6f;
					width = 10;
					height = 10;
					aiStyle = 10;
					hostile = true;
					scale = 0.9f;
					penetrate = -1;
				}
				else if (type == 110)
				{
					name = "Bullet";
					width = 4;
					height = 4;
					aiStyle = 1;
					hostile = true;
					penetrate = -1;
					light = 0.5f;
					alpha = 255;
					maxUpdates = 1;
					scale = 1.2f;
					timeLeft = 600;
					ranged = true;
				}
				else if (type == 111)
				{
					name = "Bunny";
					width = 18;
					height = 18;
					aiStyle = 26;
					friendly = true;
					penetrate = -1;
					timeLeft *= 5;
				}
				else
				{
					active = false;
				}
				width = (int)((float)width * scale);
				height = (int)((float)height * scale);
				if (type != 0 && (Config.generateINI || Config.generateItemObj))
				{
					Config.ProcessINI(this, "Projectile", name, "Defaults");
				}
			}
			else if (Type > 0)
			{
				Config.CopyAttributes(this, Config.projDefs[Type]);
				Init(type);
			}
			else
			{
				Reset();
				color = default(Color);
				hurtsTiles = true;
				displayName = "";
				soundDelay = 0;
				spriteDirection = 1;
				melee = false;
				ranged = false;
				magic = false;
				ownerHitCheck = false;
				hide = false;
				lavaWet = false;
				wetCount = 0;
				wet = false;
				ignoreWater = false;
				hostile = false;
				netUpdate = false;
				netUpdate2 = false;
				netSpam = 0;
				numUpdates = 0;
				maxUpdates = 0;
				identity = 0;
				restrikeDelay = 0;
				light = 0f;
				penetrate = 1;
				tileCollide = true;
				position = default(Vector2);
				velocity = default(Vector2);
				aiStyle = 0;
				alpha = 0;
				type = Type;
				active = true;
				rotation = 0f;
				scale = 1f;
				owner = 255;
				timeLeft = 3600;
				name = "";
				friendly = false;
				damage = 0;
				knockBack = 0f;
				miscText = "";
				if (displayName == null || displayName == "")
				{
					displayName = name;
				}
			}
		}

		public static int NewProjectile(float X, float Y, float SpeedX, float SpeedY, string name, int Damage, float KnockBack, int Owner = 255, Item item = null)
		{
			return NewProjectile(X, Y, SpeedX, SpeedY, Config.projDefs.byName[name].type, Damage, KnockBack, Owner, item);
		}

		public static int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255, Item item = null)
		{
			int num = Main.projectile.Length;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				if (!Main.projectile[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == Main.projectile.Length)
			{
				return num;
			}
			Main.projectile[num].SetDefaults(Type);
			Main.projectile[num].position.X = X - (float)Main.projectile[num].width * 0.5f;
			Main.projectile[num].position.Y = Y - (float)Main.projectile[num].height * 0.5f;
			Main.projectile[num].owner = Owner;
			Main.projectile[num].velocity.X = SpeedX;
			Main.projectile[num].velocity.Y = SpeedY;
			Main.projectile[num].damage = Damage;
			Main.projectile[num].knockBack = KnockBack;
			Main.projectile[num].identity = num;
			Main.projectile[num].wet = Collision.WetCollision(Main.projectile[num].position, Main.projectile[num].width, Main.projectile[num].height);
			Main.projectile[num].CallbackItem = item;
			if (Main.netMode != 0 && Owner == Main.myPlayer)
			{
				NetMessage.SendData(27, -1, -1, "", num);
			}
			if (Owner == Main.myPlayer)
			{
				if (Type == 28)
				{
					Main.projectile[num].timeLeft = 180;
				}
				if (Type == 29)
				{
					Main.projectile[num].timeLeft = 300;
				}
				if (Type == 30)
				{
					Main.projectile[num].timeLeft = 180;
				}
				if (Type == 37)
				{
					Main.projectile[num].timeLeft = 180;
				}
				if (Type == 75)
				{
					Main.projectile[num].timeLeft = 180;
				}
			}
			if (item != null)
			{
				if (item.RunEvent("SpawnProjectile", Main.projectile[num]))
				{
					CustomProjectile code = (CustomProjectile)Codable.customMethodReturn;
					Main.projectile[num].RegisterEvents(code);
				}
				item.RunEvent("RegisterProjectile", Main.projectile[num]);
			}
			Main.projectile[num].RunMethod("Initialize");
			return num;
		}

		public void StatusNPC(int i)
		{
			if (type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
			}
			else if (type == 15)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(24, 300);
				}
			}
			else if (type == 19)
			{
				if (Main.rand.Next(5) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
			}
			else if (type == 33)
			{
				if (Main.rand.Next(5) == 0)
				{
					Main.npc[i].AddBuff(20, 420);
				}
			}
			else if (type == 34)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(24, 240);
				}
			}
			else if (type == 35)
			{
				if (Main.rand.Next(4) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
			}
			else if (type == 54)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(20, 600);
				}
			}
			else if (type == 63)
			{
				if (Main.rand.Next(3) != 0)
				{
					Main.npc[i].AddBuff(31, 120);
				}
			}
			else if (type == 85)
			{
				Main.npc[i].AddBuff(24, 1200);
			}
			else if (type == 95 || type == 103 || type == 104)
			{
				Main.npc[i].AddBuff(39, 420);
			}
			else if (type == 98)
			{
				Main.npc[i].AddBuff(20, 600);
			}
		}

		public void StatusPvP(int i)
		{
			if (type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
			}
			else if (type == 15)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(24, 300, quiet: false);
				}
			}
			else if (type == 19)
			{
				if (Main.rand.Next(5) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
			}
			else if (type == 33)
			{
				if (Main.rand.Next(5) == 0)
				{
					Main.player[i].AddBuff(20, 420, quiet: false);
				}
			}
			else if (type == 34)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(24, 240, quiet: false);
				}
			}
			else if (type == 35)
			{
				if (Main.rand.Next(4) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
			}
			else if (type == 54)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(20, 600, quiet: false);
				}
			}
			else if (type == 63)
			{
				if (Main.rand.Next(3) != 0)
				{
					Main.player[i].AddBuff(31, 120);
				}
			}
			else if (type == 85)
			{
				Main.player[i].AddBuff(24, 1200, quiet: false);
			}
			else if (type == 95 || type == 103 || type == 104)
			{
				Main.player[i].AddBuff(39, 420);
			}
		}

		public void StatusPlayer(int i)
		{
			if (type == 55 && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(20, 600);
			}
			if (type == 44 && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(22, 900);
			}
			if (type == 82 && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(24, 420);
			}
			if ((type == 96 || type == 101) && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(39, 480);
			}
			if (type == 98)
			{
				Main.player[i].AddBuff(20, 600);
			}
		}

		public void Damage()
		{
			if (type == 18 || type == 72 || type == 86 || type == 87 || type == 111)
			{
				return;
			}
			Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
			if (type == 85 || type == 101)
			{
				int num = 30;
				rectangle.X -= num;
				rectangle.Y -= num;
				rectangle.Width += num * 2;
				rectangle.Height += num * 2;
			}
			if (friendly && owner == Main.myPlayer)
			{
				if ((aiStyle == 16 || type == 41) && (timeLeft <= 1 || type == 108))
				{
					int myPlayer = Main.myPlayer;
					if (Main.player[myPlayer].active && !Main.player[myPlayer].dead && !Main.player[myPlayer].immune && (!ownerHitCheck || Collision.CanHit(Main.player[owner].position, Main.player[owner].width, Main.player[owner].height, Main.player[myPlayer].position, Main.player[myPlayer].width, Main.player[myPlayer].height)))
					{
						Rectangle value = new Rectangle((int)Main.player[myPlayer].position.X, (int)Main.player[myPlayer].position.Y, Main.player[myPlayer].width, Main.player[myPlayer].height);
						if (rectangle.Intersects(value))
						{
							if (Main.player[myPlayer].position.X + (float)(Main.player[myPlayer].width / 2) < position.X + (float)(width / 2))
							{
								base.direction = -1;
							}
							else
							{
								base.direction = 1;
							}
							int num2 = Main.DamageVar(damage);
							StatusPlayer(myPlayer);
							Main.player[myPlayer].Hurt(num2, base.direction, pvp: true, quiet: false, Lang.deathMsg(owner, -1, whoAmI));
							if (Main.netMode != 0)
							{
								NetMessage.SendData(26, -1, -1, Lang.deathMsg(owner, -1, whoAmI), myPlayer, base.direction, 1f, 1f, num2);
							}
						}
					}
				}
				if (type != 69 && type != 70 && type != 10 && type != 11 && hurtsTiles)
				{
					int num3 = (int)(position.X / 16f);
					int num4 = (int)((position.X + (float)width) / 16f) + 1;
					int num5 = (int)(position.Y / 16f);
					int num6 = (int)((position.Y + (float)height) / 16f) + 1;
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num4 > Main.maxTilesX)
					{
						num4 = Main.maxTilesX;
					}
					if (num5 < 0)
					{
						num5 = 0;
					}
					if (num6 > Main.maxTilesY)
					{
						num6 = Main.maxTilesY;
					}
					for (int i = num3; i < num4; i++)
					{
						for (int j = num5; j < num6; j++)
						{
							if (Main.tile[i, j] != null && Main.tileCut[Main.tile[i, j].type] && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].type != 78)
							{
								WorldGen.KillTile(i, j, fail: false, effectOnly: false, noItem: false, Main.player[Main.myPlayer]);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, "", 0, i, j);
								}
							}
						}
					}
				}
			}
			if (owner == Main.myPlayer)
			{
				if (damage > 0)
				{
					for (int k = 0; k < 200; k++)
					{
						if (!Main.npc[k].active || Main.npc[k].dontTakeDamage || (((Main.npc[k].friendly && (Main.npc[k].type != 22 || owner >= 255 || !Main.player[owner].killGuide)) || !friendly) && (!Main.npc[k].friendly || !hostile)) || (owner >= 0 && Main.npc[k].immune[owner] != 0))
						{
							continue;
						}
						bool flag = false;
						if (type == 11 && (Main.npc[k].type == 47 || Main.npc[k].type == 57))
						{
							flag = true;
						}
						else if (type == 31 && Main.npc[k].type == 69)
						{
							flag = true;
						}
						if (flag || (!Main.npc[k].noTileCollide && ownerHitCheck && !Collision.CanHit(Main.player[owner].position, Main.player[owner].width, Main.player[owner].height, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height)))
						{
							continue;
						}
						Rectangle value2 = new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height);
						if (!rectangle.Intersects(value2))
						{
							continue;
						}
						if (aiStyle == 3)
						{
							if (ai[0] == 0f)
							{
								velocity.X = 0f - velocity.X;
								velocity.Y = 0f - velocity.Y;
								netUpdate = true;
							}
							ai[0] = 1f;
						}
						else if (aiStyle == 16)
						{
							if (timeLeft > 3)
							{
								timeLeft = 3;
							}
							if (Main.npc[k].position.X + (float)(Main.npc[k].width / 2) < position.X + (float)(width / 2))
							{
								base.direction = -1;
							}
							else
							{
								base.direction = 1;
							}
						}
						if (type == 41 && timeLeft > 1)
						{
							timeLeft = 1;
						}
						bool flag2 = false;
						if (melee && Main.rand.Next(1, 101) <= Main.player[owner].meleeCrit)
						{
							flag2 = true;
						}
						if (ranged && Main.rand.Next(1, 101) <= Main.player[owner].rangedCrit)
						{
							flag2 = true;
						}
						if (magic && Main.rand.Next(1, 101) <= Main.player[owner].magicCrit)
						{
							flag2 = true;
						}
						int num7 = damage;
						float num8 = knockBack;
						if (RunMethodRef("DamageNPC", Main.npc[k], num7, num8) && Codable.customMethodRefReturn != null)
						{
							object[] customMethodRefReturn = Codable.customMethodRefReturn;
							num7 = (int)customMethodRefReturn[1];
							num8 = (float)customMethodRefReturn[2];
						}
						num7 = Main.DamageVar(num7);
						float num9 = 2f;
						if (melee)
						{
							num9 = Main.player[owner].meleeDamageCrit;
						}
						else if (ranged)
						{
							num9 = Main.player[owner].rangedDamageCrit;
						}
						else if (magic)
						{
							num9 = Main.player[owner].magicDamageCrit;
						}
						StatusNPC(k);
						double num10 = Main.npc[k].StrikeNPC(num7, num8, base.direction, flag2, noEffect: false, num9);
						RunMethodRef("DealtNPC", Main.npc[k], num10, Main.player[owner]);
						Main.npc[k].RunMethod("DealtNPC", num10, Main.player[owner]);
						if (Main.netMode != 0)
						{
							if (flag2)
							{
								NetMessage.SendData(28, -1, -1, "", k, num9, knockBack, base.direction, num7);
							}
							else
							{
								NetMessage.SendData(28, -1, -1, "", k, 1f, knockBack, base.direction, num7);
							}
						}
						if (penetrate != 1)
						{
							Main.npc[k].immune[owner] = 10;
						}
						if (penetrate > 0)
						{
							penetrate--;
							if (penetrate == 0)
							{
								break;
							}
						}
						if (aiStyle == 7)
						{
							ai[0] = 1f;
							damage = 0;
							netUpdate = true;
						}
						else if (aiStyle == 13)
						{
							ai[0] = 1f;
							netUpdate = true;
						}
					}
				}
				if (damage > 0 && Main.player[Main.myPlayer].hostile)
				{
					for (int l = 0; l < 255; l++)
					{
						if (l == owner || !Main.player[l].active || Main.player[l].dead || Main.player[l].immune || !Main.player[l].hostile || playerImmune[l] > 0 || (Main.player[Main.myPlayer].team != 0 && Main.player[Main.myPlayer].team == Main.player[l].team) || (ownerHitCheck && !Collision.CanHit(Main.player[owner].position, Main.player[owner].width, Main.player[owner].height, Main.player[l].position, Main.player[l].width, Main.player[l].height)))
						{
							continue;
						}
						Rectangle value3 = new Rectangle((int)Main.player[l].position.X, (int)Main.player[l].position.Y, Main.player[l].width, Main.player[l].height);
						if (!rectangle.Intersects(value3))
						{
							continue;
						}
						if (aiStyle == 3)
						{
							if (ai[0] == 0f)
							{
								velocity.X = 0f - velocity.X;
								velocity.Y = 0f - velocity.Y;
								netUpdate = true;
							}
							ai[0] = 1f;
						}
						else if (aiStyle == 16)
						{
							if (timeLeft > 3)
							{
								timeLeft = 3;
							}
							if (Main.player[l].position.X + (float)(Main.player[l].width / 2) < position.X + (float)(width / 2))
							{
								base.direction = -1;
							}
							else
							{
								base.direction = 1;
							}
						}
						if (type == 41 && timeLeft > 1)
						{
							timeLeft = 1;
						}
						bool flag3 = false;
						if (melee && Main.rand.Next(1, 101) <= Main.player[owner].meleeCrit)
						{
							flag3 = true;
						}
						float num11 = 2f;
						if (melee)
						{
							num11 = Main.player[owner].meleeDamageCrit;
						}
						else if (ranged)
						{
							num11 = Main.player[owner].rangedDamageCrit;
						}
						else if (magic)
						{
							num11 = Main.player[owner].magicDamageCrit;
						}
						int num12 = damage;
						if (RunMethod("DamagePVP", num12, Main.player[l]) && Codable.customMethodRefReturn != null)
						{
							object[] customMethodRefReturn2 = Codable.customMethodRefReturn;
							num12 = (int)customMethodRefReturn2[0];
						}
						int num13 = Main.DamageVar(num12);
						if (!Main.player[l].immune)
						{
							StatusPvP(l);
						}
						double num14 = Main.player[l].Hurt(num13, base.direction, pvp: true, quiet: false, Lang.deathMsg(owner, -1, whoAmI), flag3, num11);
						RunMethod("DealtPVP", num14, Main.player[l]);
						if (Main.netMode != 0)
						{
							if (flag3)
							{
								NetMessage.SendData(26, -1, -1, Lang.deathMsg(owner, -1, whoAmI), l, base.direction, num11, 1f, num13);
							}
							else
							{
								NetMessage.SendData(26, -1, -1, Lang.deathMsg(owner, -1, whoAmI), l, base.direction, 1f, 1f, num13);
							}
						}
						playerImmune[l] = 40;
						if (penetrate > 0)
						{
							penetrate--;
							if (penetrate == 0)
							{
								break;
							}
						}
						if (aiStyle == 7)
						{
							ai[0] = 1f;
							damage = 0;
							netUpdate = true;
						}
						else if (aiStyle == 13)
						{
							ai[0] = 1f;
							netUpdate = true;
						}
					}
				}
			}
			if (type == 11 && Main.netMode != 1)
			{
				for (int m = 0; m < 200; m++)
				{
					if (!Main.npc[m].active)
					{
						continue;
					}
					if (Main.npc[m].type == 46)
					{
						Rectangle value4 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
						if (rectangle.Intersects(value4))
						{
							Main.npc[m].Transform(47);
						}
					}
					else if (Main.npc[m].type == 55)
					{
						Rectangle value5 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
						if (rectangle.Intersects(value5))
						{
							Main.npc[m].Transform(57);
						}
					}
				}
			}
			if (Main.netMode == 2 || !hostile || Main.myPlayer >= 255 || damage <= 0)
			{
				return;
			}
			int myPlayer2 = Main.myPlayer;
			if (!Main.player[myPlayer2].active || Main.player[myPlayer2].dead || Main.player[myPlayer2].immune)
			{
				return;
			}
			Rectangle value6 = new Rectangle((int)Main.player[myPlayer2].position.X, (int)Main.player[myPlayer2].position.Y, Main.player[myPlayer2].width, Main.player[myPlayer2].height);
			if (rectangle.Intersects(value6))
			{
				int direction = base.direction;
				direction = ((!(Main.player[myPlayer2].position.X + (float)(Main.player[myPlayer2].width / 2) < position.X + (float)(width / 2))) ? 1 : (-1));
				int num15 = damage;
				if (RunMethod("DamagePlayer", num15, Main.player[myPlayer2]) && Codable.customMethodRefReturn != null)
				{
					object[] customMethodRefReturn3 = Codable.customMethodRefReturn;
					num15 = (int)customMethodRefReturn3[0];
				}
				int theDamage = Main.DamageVar(num15);
				if (!Main.player[myPlayer2].immune)
				{
					StatusPlayer(myPlayer2);
				}
				double num16 = Main.player[myPlayer2].Hurt(theDamage, direction, pvp: false, quiet: false, Lang.deathMsg(-1, -1, whoAmI));
				RunMethod("DealtPlayer", num16, Main.player[myPlayer2]);
			}
		}

		public void Update(int i)
		{
			int num = type;
			int value = type;
			if (name != null && Config.projDefs.pretendType.TryGetValue(name, out value))
			{
				type = value;
			}
			UpdateReal(i);
			if (type == value)
			{
				type = num;
			}
		}

		public void UpdateReal(int i)
		{
			if (!active)
			{
				return;
			}
			if (displayName == "" || displayName == null)
			{
				displayName = name;
			}
			Vector2 vector = base.velocity;
			if (base.position.X <= Main.leftWorld || base.position.X + (float)width >= Main.rightWorld || base.position.Y <= Main.topWorld || base.position.Y + (float)height >= Main.bottomWorld)
			{
				active = false;
				return;
			}
			whoAmI = i;
			if (soundDelay > 0)
			{
				soundDelay--;
			}
			netUpdate = false;
			for (int j = 0; j < 255; j++)
			{
				if (playerImmune[j] > 0)
				{
					playerImmune[j]--;
				}
			}
			if (!RunMethod("PreAI") || (bool)Codable.customMethodReturn)
			{
				AI();
			}
			RunMethod("PostAI");
			if (owner < 255 && !Main.player[owner].active)
			{
				Kill();
			}
			if (!ignoreWater)
			{
				bool flag;
				bool flag2;
				try
				{
					flag = Collision.LavaCollision(base.position, width, height);
					flag2 = Collision.WetCollision(base.position, width, height);
					if (flag)
					{
						lavaWet = true;
					}
				}
				catch
				{
					active = false;
					return;
				}
				if (wet && !lavaWet)
				{
					if (type == 85 || type == 15 || type == 34)
					{
						Kill();
					}
					if (type == 2)
					{
						type = 1;
						light = 0f;
					}
				}
				if (type == 80)
				{
					flag2 = false;
					wet = false;
					if (flag && ai[0] >= 0f)
					{
						Kill();
					}
				}
				if (flag2)
				{
					if (wetCount == 0)
					{
						wetCount = 10;
						if (!wet)
						{
							if (!flag)
							{
								for (int k = 0; k < 10; k++)
								{
									int num = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 33);
									Dust dust = Main.dust[num];
									dust.velocity.Y = dust.velocity.Y - 4f;
									Dust dust2 = Main.dust[num];
									dust2.velocity.X = dust2.velocity.X * 2.5f;
									Main.dust[num].scale = 1.3f;
									Main.dust[num].alpha = 100;
									Main.dust[num].noGravity = true;
								}
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
							}
							else
							{
								for (int l = 0; l < 10; l++)
								{
									int num2 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 35);
									Dust dust3 = Main.dust[num2];
									dust3.velocity.Y = dust3.velocity.Y - 1.5f;
									Dust dust4 = Main.dust[num2];
									dust4.velocity.X = dust4.velocity.X * 2.5f;
									Main.dust[num2].scale = 1.3f;
									Main.dust[num2].alpha = 100;
									Main.dust[num2].noGravity = true;
								}
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
							}
						}
						wet = true;
					}
				}
				else if (wet)
				{
					wet = false;
					if (wetCount == 0)
					{
						wetCount = 10;
						if (!lavaWet)
						{
							for (int m = 0; m < 10; m++)
							{
								int num3 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2)), width + 12, 24, 33);
								Dust dust5 = Main.dust[num3];
								dust5.velocity.Y = dust5.velocity.Y - 4f;
								Dust dust6 = Main.dust[num3];
								dust6.velocity.X = dust6.velocity.X * 2.5f;
								Main.dust[num3].scale = 1.3f;
								Main.dust[num3].alpha = 100;
								Main.dust[num3].noGravity = true;
							}
							Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
						}
						else
						{
							for (int n = 0; n < 10; n++)
							{
								int num4 = Dust.NewDust(new Vector2(base.position.X - 6f, base.position.Y + (float)(height / 2) - 8f), width + 12, 24, 35);
								Dust dust7 = Main.dust[num4];
								dust7.velocity.Y = dust7.velocity.Y - 1.5f;
								Dust dust8 = Main.dust[num4];
								dust8.velocity.X = dust8.velocity.X * 2.5f;
								Main.dust[num4].scale = 1.3f;
								Main.dust[num4].alpha = 100;
								Main.dust[num4].noGravity = true;
							}
							Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
						}
					}
				}
				if (!wet)
				{
					lavaWet = false;
				}
				if (wetCount > 0)
				{
					wetCount--;
				}
			}
			lastPosition = base.position;
			if (tileCollide)
			{
				Vector2 velocity = base.velocity;
				bool flag3 = true;
				if (type == 9 || type == 12 || type == 15 || type == 13 || type == 31 || type == 39 || type == 40)
				{
					flag3 = false;
				}
				if (aiStyle == 10)
				{
					if (type == 42 || type == 65 || type == 68 || (type == 31 && ai[0] == 2f))
					{
						base.velocity = Collision.TileCollision(base.position, base.velocity, width, height, flag3, flag3);
					}
					else
					{
						base.velocity = Collision.AnyCollision(base.position, base.velocity, width, height);
					}
				}
				else if (aiStyle == 18)
				{
					int num5 = width - 36;
					int num6 = height - 36;
					Vector2 position = new Vector2(base.position.X + (float)(width / 2) - (float)(num5 / 2), base.position.Y + (float)(height / 2) - (float)(num6 / 2));
					base.velocity = Collision.TileCollision(position, base.velocity, num5, num6, flag3, flag3);
				}
				else if (wet)
				{
					Vector2 velocity2 = base.velocity;
					base.velocity = Collision.TileCollision(base.position, base.velocity, width, height, flag3, flag3);
					vector = base.velocity * 0.5f;
					if (base.velocity.X != velocity2.X)
					{
						vector.X = base.velocity.X;
					}
					if (base.velocity.Y != velocity2.Y)
					{
						vector.Y = base.velocity.Y;
					}
				}
				else
				{
					base.velocity = Collision.TileCollision(base.position, base.velocity, width, height, flag3, flag3);
				}
				if (velocity != base.velocity && type != 111 && (!RunMethod("tileCollide", velocity) || (bool)Codable.customMethodReturn))
				{
					if (type == 94)
					{
						if (base.velocity.X != velocity.X)
						{
							base.velocity.X = 0f - velocity.X;
						}
						if (base.velocity.Y != velocity.Y)
						{
							base.velocity.Y = 0f - velocity.Y;
						}
					}
					else if (type == 99)
					{
						if (base.velocity.Y != velocity.Y && velocity.Y > 5f)
						{
							Collision.HitTiles(base.position, base.velocity, width, height);
							Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
							base.velocity.Y = (0f - velocity.Y) * 0.2f;
						}
						if (base.velocity.X != velocity.X)
						{
							Kill();
						}
					}
					else if (type == 36)
					{
						if (penetrate > 1)
						{
							Collision.HitTiles(base.position, base.velocity, width, height);
							Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
							penetrate--;
							if (base.velocity.X != velocity.X)
							{
								base.velocity.X = 0f - velocity.X;
							}
							if (base.velocity.Y != velocity.Y)
							{
								base.velocity.Y = 0f - velocity.Y;
							}
						}
						else
						{
							Kill();
						}
					}
					else if (aiStyle == 21)
					{
						if (base.velocity.X != velocity.X)
						{
							base.velocity.X = 0f - velocity.X;
						}
						if (base.velocity.Y != velocity.Y)
						{
							base.velocity.Y = 0f - velocity.Y;
						}
					}
					else if (aiStyle == 17)
					{
						if (base.velocity.X != velocity.X)
						{
							base.velocity.X = velocity.X * -0.75f;
						}
						if (base.velocity.Y != velocity.Y && (double)velocity.Y > 1.5)
						{
							base.velocity.Y = velocity.Y * -0.7f;
						}
					}
					else if (aiStyle == 15)
					{
						bool flag4 = false;
						if (velocity.X != base.velocity.X)
						{
							if (Math.Abs(velocity.X) > 4f)
							{
								flag4 = true;
							}
							base.position.X = base.position.X + base.velocity.X;
							base.velocity.X = (0f - velocity.X) * 0.2f;
						}
						if (velocity.Y != base.velocity.Y)
						{
							if (Math.Abs(velocity.Y) > 4f)
							{
								flag4 = true;
							}
							base.position.Y = base.position.Y + base.velocity.Y;
							base.velocity.Y = (0f - velocity.Y) * 0.2f;
						}
						ai[0] = 1f;
						if (flag4)
						{
							netUpdate = true;
							Collision.HitTiles(base.position, base.velocity, width, height);
							Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
						}
					}
					else if (aiStyle == 3 || aiStyle == 13)
					{
						Collision.HitTiles(base.position, base.velocity, width, height);
						if (type == 33 || type == 106)
						{
							if (base.velocity.X != velocity.X)
							{
								base.velocity.X = 0f - velocity.X;
							}
							if (base.velocity.Y != velocity.Y)
							{
								base.velocity.Y = 0f - velocity.Y;
							}
						}
						else
						{
							ai[0] = 1f;
							if (aiStyle == 3)
							{
								base.velocity.X = 0f - velocity.X;
								base.velocity.Y = 0f - velocity.Y;
							}
						}
						netUpdate = true;
						Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					}
					else if (aiStyle == 8 && type != 96)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
						ai[0] += 1f;
						if (ai[0] >= 5f)
						{
							base.position += base.velocity;
							Kill();
						}
						else
						{
							if (type == 15 && base.velocity.Y > 4f)
							{
								if (base.velocity.Y != velocity.Y)
								{
									base.velocity.Y = (0f - velocity.Y) * 0.8f;
								}
							}
							else if (base.velocity.Y != velocity.Y)
							{
								base.velocity.Y = 0f - velocity.Y;
							}
							if (base.velocity.X != velocity.X)
							{
								base.velocity.X = 0f - velocity.X;
							}
						}
					}
					else if (aiStyle == 14)
					{
						if (type == 50)
						{
							if (base.velocity.X != velocity.X)
							{
								base.velocity.X = velocity.X * -0.2f;
							}
							if (base.velocity.Y != velocity.Y && (double)velocity.Y > 1.5)
							{
								base.velocity.Y = velocity.Y * -0.2f;
							}
						}
						else
						{
							if (base.velocity.X != velocity.X)
							{
								base.velocity.X = velocity.X * -0.5f;
							}
							if (base.velocity.Y != velocity.Y && velocity.Y > 1f)
							{
								base.velocity.Y = velocity.Y * -0.5f;
							}
						}
					}
					else if (aiStyle == 16)
					{
						if (base.velocity.X != velocity.X)
						{
							base.velocity.X = velocity.X * -0.4f;
							if (type == 29)
							{
								base.velocity.X = base.velocity.X * 0.8f;
							}
						}
						if (base.velocity.Y != velocity.Y && (double)velocity.Y > 0.7 && type != 102)
						{
							base.velocity.Y = velocity.Y * -0.4f;
							if (type == 29)
							{
								base.velocity.Y = base.velocity.Y * 0.8f;
							}
						}
					}
					else if (aiStyle != 9 || owner == Main.myPlayer)
					{
						base.position += base.velocity;
						Kill();
					}
				}
			}
			if (type != 7 && type != 8)
			{
				if (wet)
				{
					base.position += vector;
				}
				else
				{
					base.position += base.velocity;
				}
			}
			if ((aiStyle != 3 || ai[0] != 1f) && (aiStyle != 7 || ai[0] != 1f) && (aiStyle != 13 || ai[0] != 1f) && (aiStyle != 15 || ai[0] != 1f) && aiStyle != 15 && aiStyle != 26)
			{
				if (base.velocity.X < 0f)
				{
					direction = -1;
				}
				else
				{
					direction = 1;
				}
			}
			if (!active)
			{
				return;
			}
			if (light > 0f)
			{
				float num7 = light;
				float num8 = light;
				float num9 = light;
				if (type == 2 || type == 82)
				{
					num8 *= 0.75f;
					num9 *= 0.55f;
				}
				else if (type == 94)
				{
					num7 *= 0.5f;
					num8 *= 0f;
				}
				else if (type == 95 || type == 96 || type == 103 || type == 104)
				{
					num7 *= 0.35f;
					num8 *= 1f;
					num9 *= 0f;
				}
				else if (type == 4)
				{
					num8 *= 0.1f;
					num7 *= 0.5f;
				}
				else if (type == 9)
				{
					num8 *= 0.1f;
					num9 *= 0.6f;
				}
				else if (type == 92)
				{
					num8 *= 0.6f;
					num7 *= 0.8f;
				}
				else if (type == 93)
				{
					num8 *= 1f;
					num7 *= 1f;
					num9 *= 0.01f;
				}
				else if (type == 12)
				{
					num7 *= 0.9f;
					num8 *= 0.8f;
					num9 *= 0.1f;
				}
				else if (type == 14 || type == 110)
				{
					num8 *= 0.7f;
					num9 *= 0.1f;
				}
				else if (type == 15)
				{
					num8 *= 0.4f;
					num9 *= 0.1f;
					num7 = 1f;
				}
				else if (type == 16)
				{
					num7 *= 0.1f;
					num8 *= 0.4f;
					num9 = 1f;
				}
				else if (type == 18)
				{
					num8 *= 0.7f;
					num9 *= 0.3f;
				}
				else if (type == 19)
				{
					num8 *= 0.5f;
					num9 *= 0.1f;
				}
				else if (type == 20)
				{
					num7 *= 0.1f;
					num9 *= 0.3f;
				}
				else if (type == 22)
				{
					num7 = 0f;
					num8 = 0f;
				}
				else if (type == 27)
				{
					num7 *= 0f;
					num8 *= 0.3f;
					num9 = 1f;
				}
				else if (type == 34)
				{
					num8 *= 0.1f;
					num9 *= 0.1f;
				}
				else if (type == 36)
				{
					num7 = 0.8f;
					num8 *= 0.2f;
					num9 *= 0.6f;
				}
				else if (type == 41)
				{
					num8 *= 0.8f;
					num9 *= 0.6f;
				}
				else if (type == 44 || type == 45)
				{
					num9 = 1f;
					num7 *= 0.6f;
					num8 *= 0.1f;
				}
				else if (type == 50)
				{
					num7 *= 0.7f;
					num9 *= 0.8f;
				}
				else if (type == 53)
				{
					num7 *= 0.7f;
					num8 *= 0.8f;
				}
				else if (type == 72)
				{
					num7 *= 0.45f;
					num8 *= 0.75f;
					num9 = 1f;
				}
				else if (type == 86)
				{
					num7 *= 1f;
					num8 *= 0.45f;
					num9 = 0.75f;
				}
				else if (type == 87)
				{
					num7 *= 0.45f;
					num8 = 1f;
					num9 *= 0.75f;
				}
				else if (type == 73)
				{
					num7 *= 0.4f;
					num8 *= 0.6f;
					num9 *= 1f;
				}
				else if (type == 74)
				{
					num7 *= 1f;
					num8 *= 0.4f;
					num9 *= 0.6f;
				}
				else if (type == 76 || type == 77 || type == 78)
				{
					num7 *= 1f;
					num8 *= 0.3f;
					num9 *= 0.6f;
				}
				else if (type == 79)
				{
					num7 = (float)Main.DiscoR / 255f;
					num8 = (float)Main.DiscoG / 255f;
					num9 = (float)Main.DiscoB / 255f;
				}
				else if (type == 80)
				{
					num7 *= 0f;
					num8 *= 0.8f;
					num9 *= 1f;
				}
				else if (type == 83 || type == 88)
				{
					num7 *= 0.7f;
					num8 *= 0f;
					num9 *= 1f;
				}
				else if (type == 100)
				{
					num7 *= 1f;
					num8 *= 0.5f;
					num9 *= 0f;
				}
				else if (type == 84)
				{
					num7 *= 0.8f;
					num8 *= 0f;
					num9 *= 0.5f;
				}
				else if (type == 89 || type == 90)
				{
					num8 *= 0.2f;
					num9 *= 1f;
					num7 *= 0.05f;
				}
				else if (type == 106)
				{
					num7 *= 0f;
					num8 *= 0.5f;
					num9 *= 1f;
				}
				Lighting.addLight((int)((base.position.X + (float)(width / 2)) / 16f), (int)((base.position.Y + (float)(height / 2)) / 16f), num7, num8, num9);
			}
			if (type == 2 || type == 82)
			{
				Dust.NewDust(new Vector2(base.position.X, base.position.Y), width, height, 6, 0f, 0f, 100);
			}
			else if (type == 103)
			{
				int num10 = Dust.NewDust(new Vector2(base.position.X, base.position.Y), width, height, 75, 0f, 0f, 100);
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num10].noGravity = true;
					Main.dust[num10].scale *= 2f;
				}
			}
			else if (type == 4)
			{
				if (Main.rand.Next(5) == 0)
				{
					Dust.NewDust(new Vector2(base.position.X, base.position.Y), width, height, 14, 0f, 0f, 150, default(Color), 1.1f);
				}
			}
			else if (type == 5)
			{
				int num11;
				switch (Main.rand.Next(3))
				{
				case 0:
					num11 = 15;
					break;
				case 1:
					num11 = 57;
					break;
				default:
					num11 = 58;
					break;
				}
				Dust.NewDust(base.position, width, height, num11, base.velocity.X * 0.5f, base.velocity.Y * 0.5f, 150, default(Color), 1.2f);
			}
			Damage();
			if (Main.netMode != 1 && type == 99)
			{
				Collision.SwitchTiles(base.position, width, height, lastPosition);
			}
			if (type == 94)
			{
				for (int num12 = oldPos.Length - 1; num12 > 0; num12--)
				{
					oldPos[num12] = oldPos[num12 - 1];
				}
				oldPos[0] = base.position;
			}
			timeLeft--;
			if (timeLeft <= 0)
			{
				Kill();
			}
			if (penetrate == 0)
			{
				Kill();
			}
			if (active && owner == Main.myPlayer)
			{
				if (netUpdate2)
				{
					netUpdate = true;
				}
				if (!active)
				{
					netSpam = 0;
				}
				if (netUpdate)
				{
					if (netSpam < 60)
					{
						netSpam += 5;
						NetMessage.SendData(27, -1, -1, "", i);
						netUpdate2 = false;
					}
					else
					{
						netUpdate2 = true;
					}
				}
				if (netSpam > 0)
				{
					netSpam--;
				}
			}
			if (active && maxUpdates > 0)
			{
				numUpdates--;
				if (numUpdates >= 0)
				{
					Update(i);
				}
				else
				{
					numUpdates = maxUpdates;
				}
			}
			netUpdate = false;
		}

		public void AI(bool ignoreCustomCode = false)
		{
			int num = type;
			int value = type;
			if (name != null && Config.projDefs.aiPretendType.TryGetValue(name, out value))
			{
				type = value;
			}
			AIReal(ignoreCustomCode);
			if (type == value)
			{
				type = num;
			}
		}

		public void AIReal(bool ignoreCustomCode = false)
		{
			if (!ignoreCustomCode && RunMethod("AI"))
			{
				return;
			}
			if (aiStyle == 1)
			{
				if (type == 83 && ai[1] == 0f)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 33);
				}
				if (type == 110 && ai[1] == 0f)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 11);
				}
				if (type == 84 && ai[1] == 0f)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 12);
				}
				if (type == 100 && ai[1] == 0f)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 33);
				}
				if (type == 98 && ai[1] == 0f)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 17);
				}
				if ((type == 81 || type == 82) && ai[1] == 0f)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 5);
					ai[1] = 1f;
				}
				if (type == 41)
				{
					Vector2 position = new Vector2(base.position.X, base.position.Y);
					int width = base.width;
					int height = base.height;
					int num = 31;
					float speedX = 0f;
					float speedY = 0f;
					int num2 = 100;
					int num3 = Dust.NewDust(position, width, height, num, speedX, speedY, num2, default(Color), 1.6f);
					Main.dust[num3].noGravity = true;
					Vector2 position2 = new Vector2(base.position.X, base.position.Y);
					int width2 = base.width;
					int height2 = base.height;
					int num4 = 6;
					float speedX2 = 0f;
					float speedY2 = 0f;
					int num5 = 100;
					num3 = Dust.NewDust(position2, width2, height2, num4, speedX2, speedY2, num5, default(Color), 2f);
					Main.dust[num3].noGravity = true;
				}
				else if (type == 55)
				{
					Vector2 position3 = new Vector2(base.position.X, base.position.Y);
					int width3 = base.width;
					int height3 = base.height;
					int num6 = 18;
					float speedX3 = 0f;
					float speedY3 = 0f;
					int num7 = 0;
					int num8 = Dust.NewDust(position3, width3, height3, num6, speedX3, speedY3, num7, default(Color), 0.9f);
					Main.dust[num8].noGravity = true;
				}
				else if (type == 91 && Main.rand.Next(2) == 0)
				{
					int num9 = (Main.rand.Next(2) != 0) ? 58 : 15;
					Vector2 position4 = base.position;
					int width4 = base.width;
					int height4 = base.height;
					int num10 = num9;
					float speedX4 = velocity.X * 0.25f;
					float speedY4 = velocity.Y * 0.25f;
					int num11 = 150;
					int num12 = Dust.NewDust(position4, width4, height4, num10, speedX4, speedY4, num11, default(Color), 0.9f);
					Dust dust = Main.dust[num12];
					dust.velocity *= 0.25f;
				}
				if (type == 20 || type == 14 || type == 36 || type == 83 || type == 84 || type == 89 || type == 100 || type == 104 || type == 110)
				{
					if (alpha > 0)
					{
						alpha -= 15;
					}
					if (alpha < 0)
					{
						alpha = 0;
					}
				}
				if (type == 88)
				{
					if (alpha > 0)
					{
						alpha -= 10;
					}
					if (alpha < 0)
					{
						alpha = 0;
					}
				}
				if (type != 5 && type != 14 && type != 20 && type != 36 && type != 38 && type != 55 && type != 83 && type != 84 && type != 88 && type != 89 && type != 98 && type != 100 && type != 104 && type != 110)
				{
					ai[0] += 1f;
				}
				if (type == 81 || type == 91)
				{
					if (ai[0] >= 20f)
					{
						ai[0] = 20f;
						velocity.Y += 0.07f;
					}
				}
				else if (ai[0] >= 15f)
				{
					ai[0] = 15f;
					velocity.Y += 0.1f;
				}
				rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
			}
			else if (aiStyle == 2)
			{
				if (type == 93 && Main.rand.Next(5) == 0)
				{
					Vector2 position5 = base.position;
					int width5 = base.width;
					int height5 = base.height;
					int num13 = 57;
					float speedX5 = velocity.X * 0.2f + (float)(direction * 3);
					float speedY5 = velocity.Y * 0.2f;
					int num14 = 100;
					int num15 = Dust.NewDust(position5, width5, height5, num13, speedX5, speedY5, num14, default(Color), 0.3f);
					Dust dust2 = Main.dust[num15];
					dust2.velocity.X = dust2.velocity.X * 0.3f;
					Dust dust3 = Main.dust[num15];
					dust3.velocity.Y = dust3.velocity.Y * 0.3f;
				}
				rotation += (Math.Abs(velocity.X) + Math.Abs(velocity.Y)) * 0.03f * (float)direction;
				if (type == 69 || type == 70)
				{
					ai[0] += 1f;
					if (ai[0] >= 10f)
					{
						velocity.Y += 0.25f;
						velocity.X *= 0.99f;
					}
				}
				else
				{
					ai[0] += 1f;
					if (ai[0] >= 20f)
					{
						velocity.Y += 0.4f;
						velocity.X *= 0.97f;
					}
					else if (type == 48 || type == 54 || type == 93)
					{
						rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
					}
				}
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
				if (type == 54 && Main.rand.Next(20) == 0)
				{
					Vector2 position6 = new Vector2(base.position.X, base.position.Y);
					int width6 = base.width;
					int height6 = base.height;
					int num16 = 40;
					float speedX6 = velocity.X * 0.1f;
					float speedY6 = velocity.Y * 0.1f;
					int num17 = 0;
					Dust.NewDust(position6, width6, height6, num16, speedX6, speedY6, num17, default(Color), 0.75f);
				}
			}
			else if (aiStyle == 3)
			{
				if (soundDelay == 0)
				{
					soundDelay = 8;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 7);
				}
				if (type == 19)
				{
					for (int i = 0; i < 2; i++)
					{
						Vector2 position7 = new Vector2(base.position.X, base.position.Y);
						int width7 = base.width;
						int height7 = base.height;
						int num18 = 6;
						float speedX7 = velocity.X * 0.2f;
						float speedY7 = velocity.Y * 0.2f;
						int num19 = 100;
						int num20 = Dust.NewDust(position7, width7, height7, num18, speedX7, speedY7, num19, default(Color), 2f);
						Main.dust[num20].noGravity = true;
						Dust dust4 = Main.dust[num20];
						dust4.velocity.X = dust4.velocity.X * 0.3f;
						Dust dust5 = Main.dust[num20];
						dust5.velocity.Y = dust5.velocity.Y * 0.3f;
					}
				}
				else if (type == 33)
				{
					if (Main.rand.Next(1) == 0)
					{
						Vector2 position8 = base.position;
						int width8 = base.width;
						int height8 = base.height;
						int num21 = 40;
						float speedX8 = velocity.X * 0.25f;
						float speedY8 = velocity.Y * 0.25f;
						int num22 = 0;
						int num23 = Dust.NewDust(position8, width8, height8, num21, speedX8, speedY8, num22, default(Color), 1.4f);
						Main.dust[num23].noGravity = true;
					}
				}
				else if (type == 6 && Main.rand.Next(5) == 0)
				{
					int num24;
					switch (Main.rand.Next(3))
					{
					case 0:
						num24 = 15;
						break;
					case 1:
						num24 = 57;
						break;
					default:
						num24 = 58;
						break;
					}
					Vector2 position9 = base.position;
					int width9 = base.width;
					int height9 = base.height;
					int num25 = num24;
					float speedX9 = velocity.X * 0.25f;
					float speedY9 = velocity.Y * 0.25f;
					int num26 = 150;
					Dust.NewDust(position9, width9, height9, num25, speedX9, speedY9, num26, default(Color), 0.7f);
				}
				if (ai[0] == 0f)
				{
					ai[1] += 1f;
					if (type == 106)
					{
						if (ai[1] >= 45f)
						{
							ai[0] = 1f;
							ai[1] = 0f;
							netUpdate = true;
						}
					}
					else if (ai[1] >= 30f)
					{
						ai[0] = 1f;
						ai[1] = 0f;
						netUpdate = true;
					}
				}
				else
				{
					tileCollide = false;
					float num27 = 9f;
					float num28 = 0.4f;
					if (type == 19)
					{
						num27 = 13f;
						num28 = 0.6f;
					}
					else if (type == 33)
					{
						num27 = 15f;
						num28 = 0.8f;
					}
					else if (type == 106)
					{
						num27 = 16f;
						num28 = 1.2f;
					}
					Vector2 vector = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num29 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector.X;
					float num30 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector.Y;
					float num31 = (float)Math.Sqrt(num29 * num29 + num30 * num30);
					if (num31 > 3000f)
					{
						Kill();
					}
					num31 = num27 / num31;
					num29 *= num31;
					num30 *= num31;
					if (velocity.X < num29)
					{
						velocity.X += num28;
						if (velocity.X < 0f && num29 > 0f)
						{
							velocity.X += num28;
						}
					}
					else if (velocity.X > num29)
					{
						velocity.X -= num28;
						if (velocity.X > 0f && num29 < 0f)
						{
							velocity.X -= num28;
						}
					}
					if (velocity.Y < num30)
					{
						velocity.Y += num28;
						if (velocity.Y < 0f && num30 > 0f)
						{
							velocity.Y += num28;
						}
					}
					else if (velocity.Y > num30)
					{
						velocity.Y -= num28;
						if (velocity.Y > 0f && num30 < 0f)
						{
							velocity.Y -= num28;
						}
					}
					if (Main.myPlayer == owner)
					{
						Rectangle rectangle = new Rectangle((int)base.position.X, (int)base.position.Y, base.width, base.height);
						Rectangle value = new Rectangle((int)Main.player[owner].position.X, (int)Main.player[owner].position.Y, Main.player[owner].width, Main.player[owner].height);
						if (rectangle.Intersects(value))
						{
							Kill();
						}
					}
				}
				if (type == 106)
				{
					rotation += 0.3f * (float)direction;
				}
				else
				{
					rotation += 0.4f * (float)direction;
				}
			}
			else if (aiStyle == 4)
			{
				rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
				if (ai[0] == 0f)
				{
					alpha -= 50;
					if (alpha > 0)
					{
						return;
					}
					alpha = 0;
					ai[0] = 1f;
					if (ai[1] == 0f)
					{
						ai[1] += 1f;
						base.position += velocity * 1f;
					}
					if (type == 7 && Main.myPlayer == owner)
					{
						int num32 = type;
						if (ai[1] >= 6f)
						{
							num32++;
						}
						int num33 = NewProjectile(base.position.X + velocity.X + (float)(base.width / 2), base.position.Y + velocity.Y + (float)(base.height / 2), velocity.X, velocity.Y, num32, damage, knockBack, owner);
						Main.projectile[num33].damage = damage;
						Main.projectile[num33].ai[1] = ai[1] + 1f;
						NetMessage.SendData(27, -1, -1, "", num33);
					}
					return;
				}
				if (alpha < 170 && alpha + 5 >= 170)
				{
					for (int j = 0; j < 3; j++)
					{
						Vector2 position10 = base.position;
						int width10 = base.width;
						int height10 = base.height;
						int num34 = 18;
						float speedX10 = velocity.X * 0.025f;
						float speedY10 = velocity.Y * 0.025f;
						int num35 = 170;
						Dust.NewDust(position10, width10, height10, num34, speedX10, speedY10, num35, default(Color), 1.2f);
					}
					Vector2 position11 = base.position;
					int width11 = base.width;
					int height11 = base.height;
					int num36 = 14;
					float speedX11 = 0f;
					float speedY11 = 0f;
					int num37 = 170;
					Dust.NewDust(position11, width11, height11, num36, speedX11, speedY11, num37, default(Color), 1.1f);
				}
				alpha += 5;
				if (alpha >= 255)
				{
					Kill();
				}
			}
			else if (aiStyle == 5)
			{
				if (type == 92)
				{
					if (base.position.Y > ai[1])
					{
						tileCollide = true;
					}
				}
				else
				{
					if (ai[1] == 0f && !Collision.SolidCollision(base.position, base.width, base.height))
					{
						ai[1] = 1f;
						netUpdate = true;
					}
					if (ai[1] != 0f)
					{
						tileCollide = true;
					}
				}
				if (soundDelay == 0)
				{
					soundDelay = 20 + Main.rand.Next(40);
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 9);
				}
				if (localAI[0] == 0f)
				{
					localAI[0] = 1f;
				}
				alpha += (int)(25f * localAI[0]);
				if (alpha > 200)
				{
					alpha = 200;
					localAI[0] = -1f;
				}
				if (alpha < 0)
				{
					alpha = 0;
					localAI[0] = 1f;
				}
				rotation += (Math.Abs(velocity.X) + Math.Abs(velocity.Y)) * 0.01f * (float)direction;
				if (ai[1] == 1f || type == 92)
				{
					light = 0.9f;
					if (Main.rand.Next(10) == 0)
					{
						Vector2 position12 = base.position;
						int width12 = base.width;
						int height12 = base.height;
						int num38 = 58;
						float speedX12 = velocity.X * 0.5f;
						float speedY12 = velocity.Y * 0.5f;
						int num39 = 150;
						Dust.NewDust(position12, width12, height12, num38, speedX12, speedY12, num39, default(Color), 1.2f);
					}
					if (Main.rand.Next(20) == 0)
					{
						Gore.NewGore(base.position, new Vector2(velocity.X * 0.2f, velocity.Y * 0.2f), Main.rand.Next(16, 18));
					}
				}
			}
			else if (aiStyle == 6)
			{
				velocity *= 0.95f;
				ai[0] += 1f;
				if (ai[0] == 180f)
				{
					Kill();
				}
				if (ai[1] == 0f)
				{
					ai[1] = 1f;
					for (int k = 0; k < 30; k++)
					{
						Vector2 position13 = base.position;
						int width13 = base.width;
						int height13 = base.height;
						int num40 = 10 + type;
						float x = velocity.X;
						float y = velocity.Y;
						int num41 = 50;
						Dust.NewDust(position13, width13, height13, num40, x, y, num41);
					}
				}
				if (type != 10 && type != 11)
				{
					return;
				}
				int num42 = (int)(base.position.X / 16f) - 1;
				int num43 = (int)((base.position.X + (float)base.width) / 16f) + 2;
				int num44 = (int)(base.position.Y / 16f) - 1;
				int num45 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
				if (num42 < 0)
				{
					num42 = 0;
				}
				if (num43 > Main.maxTilesX)
				{
					num43 = Main.maxTilesX;
				}
				if (num44 < 0)
				{
					num44 = 0;
				}
				if (num45 > Main.maxTilesY)
				{
					num45 = Main.maxTilesY;
				}
				Vector2 vector2 = default(Vector2);
				for (int l = num42; l < num43; l++)
				{
					for (int m = num44; m < num45; m++)
					{
						vector2.X = l * 16;
						vector2.Y = m * 16;
						if (!(base.position.X + (float)base.width > vector2.X) || !(base.position.X < vector2.X + 16f) || !(base.position.Y + (float)base.height > vector2.Y) || !(base.position.Y < vector2.Y + 16f) || Main.myPlayer != owner || !Main.tile[l, m].active)
						{
							continue;
						}
						if (type == 10)
						{
							if (Main.tile[l, m].type == 23)
							{
								Main.tile[l, m].type = 2;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
							if (Main.tile[l, m].type == 25)
							{
								Main.tile[l, m].type = 1;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
							if (Main.tile[l, m].type == 112)
							{
								Main.tile[l, m].type = 53;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
						}
						else
						{
							if (type != 11)
							{
								continue;
							}
							if (Main.tile[l, m].type == 109)
							{
								Main.tile[l, m].type = 2;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
							if (Main.tile[l, m].type == 116)
							{
								Main.tile[l, m].type = 53;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
							if (Main.tile[l, m].type == 117)
							{
								Main.tile[l, m].type = 1;
								WorldGen.SquareTileFrame(l, m);
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, l, m, 1);
								}
							}
						}
					}
				}
			}
			else if (aiStyle == 7)
			{
				if (Main.player[owner].dead)
				{
					Kill();
					return;
				}
				Vector2 vector3 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num46 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector3.X;
				float num47 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector3.Y;
				float num48 = (float)Math.Sqrt(num46 * num46 + num47 * num47);
				rotation = (float)Math.Atan2(num47, num46) - 1.57f;
				if (ai[0] == 0f)
				{
					if ((num48 > 300f && type == 13) || (num48 > 400f && type == 32) || (num48 > 440f && type == 73) || (num48 > 440f && type == 74))
					{
						ai[0] = 1f;
					}
					int num49 = (int)(base.position.X / 16f) - 1;
					int num50 = (int)((base.position.X + (float)base.width) / 16f) + 2;
					int num51 = (int)(base.position.Y / 16f) - 1;
					int num52 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
					if (num49 < 0)
					{
						num49 = 0;
					}
					if (num50 > Main.maxTilesX)
					{
						num50 = Main.maxTilesX;
					}
					if (num51 < 0)
					{
						num51 = 0;
					}
					if (num52 > Main.maxTilesY)
					{
						num52 = Main.maxTilesY;
					}
					Vector2 vector4 = default(Vector2);
					for (int n = num49; n < num50; n++)
					{
						for (int num53 = num51; num53 < num52; num53++)
						{
							if (Main.tile[n, num53] == null)
							{
								Main.tile[n, num53] = new Tile();
							}
							vector4.X = n * 16;
							vector4.Y = num53 * 16;
							if (!(base.position.X + (float)base.width > vector4.X) || !(base.position.X < vector4.X + 16f) || !(base.position.Y + (float)base.height > vector4.Y) || !(base.position.Y < vector4.Y + 16f) || !Main.tile[n, num53].active || !Main.tileSolid[Main.tile[n, num53].type])
							{
								continue;
							}
							if (Main.player[owner].grapCount < 10)
							{
								Main.player[owner].grappling[Main.player[owner].grapCount] = whoAmI;
								Main.player[owner].grapCount++;
							}
							if (Main.myPlayer == owner)
							{
								int num54 = 0;
								int num55 = -1;
								int num56 = 100000;
								if (type == 73 || type == 74)
								{
									for (int num57 = 0; num57 < 1000; num57++)
									{
										if (num57 != whoAmI && Main.projectile[num57].active && Main.projectile[num57].owner == owner && Main.projectile[num57].aiStyle == 7 && Main.projectile[num57].ai[0] == 2f)
										{
											Main.projectile[num57].Kill();
										}
									}
								}
								else
								{
									for (int num58 = 0; num58 < 1000; num58++)
									{
										if (Main.projectile[num58].active && Main.projectile[num58].owner == owner && Main.projectile[num58].aiStyle == 7)
										{
											if (Main.projectile[num58].timeLeft < num56)
											{
												num55 = num58;
												num56 = Main.projectile[num58].timeLeft;
											}
											num54++;
										}
									}
									if (num54 > 3)
									{
										Main.projectile[num55].Kill();
									}
								}
							}
							WorldGen.KillTile(n, num53, fail: true, effectOnly: true);
							Main.PlaySound(0, n * 16, num53 * 16);
							velocity.X = 0f;
							velocity.Y = 0f;
							ai[0] = 2f;
							base.position.X = n * 16 + 8 - base.width / 2;
							base.position.Y = num53 * 16 + 8 - base.height / 2;
							damage = 0;
							netUpdate = true;
							if (Main.myPlayer == owner)
							{
								NetMessage.SendData(13, -1, -1, "", owner);
							}
							break;
						}
						if (ai[0] == 2f)
						{
							break;
						}
					}
				}
				else if (ai[0] == 1f)
				{
					float num59 = 11f;
					if (type == 32)
					{
						num59 = 15f;
					}
					if (type == 73 || type == 74)
					{
						num59 = 17f;
					}
					if (num48 < 24f)
					{
						Kill();
					}
					num48 = num59 / num48;
					num46 *= num48;
					num47 *= num48;
					velocity.X = num46;
					velocity.Y = num47;
				}
				else
				{
					if (ai[0] != 2f)
					{
						return;
					}
					int num60 = (int)(base.position.X / 16f) - 1;
					int num61 = (int)((base.position.X + (float)base.width) / 16f) + 2;
					int num62 = (int)(base.position.Y / 16f) - 1;
					int num63 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
					if (num60 < 0)
					{
						num60 = 0;
					}
					if (num61 > Main.maxTilesX)
					{
						num61 = Main.maxTilesX;
					}
					if (num62 < 0)
					{
						num62 = 0;
					}
					if (num63 > Main.maxTilesY)
					{
						num63 = Main.maxTilesY;
					}
					bool flag = true;
					Vector2 vector5 = default(Vector2);
					for (int num64 = num60; num64 < num61; num64++)
					{
						for (int num65 = num62; num65 < num63; num65++)
						{
							if (Main.tile[num64, num65] == null)
							{
								Main.tile[num64, num65] = new Tile();
							}
							vector5.X = num64 * 16;
							vector5.Y = num65 * 16;
							if (base.position.X + (float)(base.width / 2) > vector5.X && base.position.X + (float)(base.width / 2) < vector5.X + 16f && base.position.Y + (float)(base.height / 2) > vector5.Y && base.position.Y + (float)(base.height / 2) < vector5.Y + 16f && Main.tile[num64, num65].active && Main.tileSolid[Main.tile[num64, num65].type])
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						ai[0] = 1f;
					}
					else if (Main.player[owner].grapCount < 10)
					{
						Main.player[owner].grappling[Main.player[owner].grapCount] = whoAmI;
						Main.player[owner].grapCount++;
					}
				}
			}
			else if (aiStyle == 8)
			{
				if (type == 96 && localAI[0] == 0f)
				{
					localAI[0] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 20);
				}
				if (type == 27)
				{
					Vector2 position14 = new Vector2(base.position.X + velocity.X, base.position.Y + velocity.Y);
					int width14 = base.width;
					int height14 = base.height;
					int num66 = 29;
					float x2 = velocity.X;
					float y2 = velocity.Y;
					int num67 = 100;
					int num68 = Dust.NewDust(position14, width14, height14, num66, x2, y2, num67, default(Color), 3f);
					Main.dust[num68].noGravity = true;
					if (Main.rand.Next(10) == 0)
					{
						Vector2 position15 = new Vector2(base.position.X, base.position.Y);
						int width15 = base.width;
						int height15 = base.height;
						int num69 = 29;
						float x3 = velocity.X;
						float y3 = velocity.Y;
						int num70 = 100;
						num68 = Dust.NewDust(position15, width15, height15, num69, x3, y3, num70, default(Color), 1.4f);
					}
				}
				else if (type == 95 || type == 96)
				{
					Vector2 position16 = new Vector2(base.position.X + velocity.X, base.position.Y + velocity.Y);
					int width16 = base.width;
					int height16 = base.height;
					int num71 = 75;
					float x4 = velocity.X;
					float y4 = velocity.Y;
					int num72 = 100;
					int num73 = Dust.NewDust(position16, width16, height16, num71, x4, y4, num72, default(Color), 3f * scale);
					Main.dust[num73].noGravity = true;
				}
				else
				{
					for (int num74 = 0; num74 < 2; num74++)
					{
						Vector2 position17 = new Vector2(base.position.X, base.position.Y);
						int width17 = base.width;
						int height17 = base.height;
						int num75 = 6;
						float speedX13 = velocity.X * 0.2f;
						float speedY13 = velocity.Y * 0.2f;
						int num76 = 100;
						int num77 = Dust.NewDust(position17, width17, height17, num75, speedX13, speedY13, num76, default(Color), 2f);
						Main.dust[num77].noGravity = true;
						Dust dust6 = Main.dust[num77];
						dust6.velocity.X = dust6.velocity.X * 0.3f;
						Dust dust7 = Main.dust[num77];
						dust7.velocity.Y = dust7.velocity.Y * 0.3f;
					}
				}
				if (type != 27 && type != 96)
				{
					ai[1] += 1f;
				}
				if (ai[1] >= 20f)
				{
					velocity.Y += 0.2f;
				}
				rotation += 0.3f * (float)direction;
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
			}
			else if (aiStyle == 9)
			{
				if (type == 34)
				{
					Vector2 position18 = new Vector2(base.position.X, base.position.Y);
					int width18 = base.width;
					int height18 = base.height;
					int num78 = 6;
					float speedX14 = velocity.X * 0.2f;
					float speedY14 = velocity.Y * 0.2f;
					int num79 = 100;
					int num80 = Dust.NewDust(position18, width18, height18, num78, speedX14, speedY14, num79, default(Color), 3.5f);
					Main.dust[num80].noGravity = true;
					Dust dust8 = Main.dust[num80];
					dust8.velocity *= 1.4f;
					Vector2 position19 = new Vector2(base.position.X, base.position.Y);
					int width19 = base.width;
					int height19 = base.height;
					int num81 = 6;
					float speedX15 = velocity.X * 0.2f;
					float speedY15 = velocity.Y * 0.2f;
					int num82 = 100;
					num80 = Dust.NewDust(position19, width19, height19, num81, speedX15, speedY15, num82, default(Color), 1.5f);
				}
				else if (type == 79)
				{
					if (soundDelay == 0 && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > 2f)
					{
						soundDelay = 10;
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 9);
					}
					for (int num83 = 0; num83 < 1; num83++)
					{
						int num84 = Dust.NewDust(new Vector2(base.position.X, base.position.Y), base.width, base.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
						Dust dust9 = Main.dust[num84];
						dust9.velocity *= 0.1f;
						Dust dust10 = Main.dust[num84];
						dust10.velocity += velocity * 0.2f;
						Main.dust[num84].position.X = base.position.X + (float)(base.width / 2) + 4f + (float)Main.rand.Next(-2, 3);
						Main.dust[num84].position.Y = base.position.Y + (float)(base.height / 2) + (float)Main.rand.Next(-2, 3);
						Main.dust[num84].noGravity = true;
					}
				}
				else
				{
					if (soundDelay == 0 && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > 2f)
					{
						soundDelay = 10;
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 9);
					}
					Vector2 position20 = new Vector2(base.position.X, base.position.Y);
					int width20 = base.width;
					int height20 = base.height;
					int num85 = 15;
					float speedX16 = 0f;
					float speedY16 = 0f;
					int num86 = 100;
					int num87 = Dust.NewDust(position20, width20, height20, num85, speedX16, speedY16, num86, default(Color), 2f);
					Dust dust11 = Main.dust[num87];
					dust11.velocity *= 0.3f;
					Main.dust[num87].position.X = base.position.X + (float)(base.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
					Main.dust[num87].position.Y = base.position.Y + (float)(base.height / 2) + (float)Main.rand.Next(-4, 5);
					Main.dust[num87].noGravity = true;
				}
				if (Main.myPlayer == owner && ai[0] == 0f)
				{
					if (Main.player[owner].channel)
					{
						float num88 = 12f;
						if (type == 16)
						{
							num88 = 15f;
						}
						Vector2 vector6 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num89 = (float)Main.mouseX + Main.screenPosition.X - vector6.X;
						float num90 = (float)Main.mouseY + Main.screenPosition.Y - vector6.Y;
						float num91 = (float)Math.Sqrt(num89 * num89 + num90 * num90);
						num91 = (float)Math.Sqrt(num89 * num89 + num90 * num90);
						if (num91 > num88)
						{
							num91 = num88 / num91;
							num89 *= num91;
							num90 *= num91;
							int num92 = (int)(num89 * 1000f);
							int num93 = (int)(velocity.X * 1000f);
							int num94 = (int)(num90 * 1000f);
							int num95 = (int)(velocity.Y * 1000f);
							if (num92 != num93 || num94 != num95)
							{
								netUpdate = true;
							}
							velocity.X = num89;
							velocity.Y = num90;
						}
						else
						{
							int num96 = (int)(num89 * 1000f);
							int num97 = (int)(velocity.X * 1000f);
							int num98 = (int)(num90 * 1000f);
							int num99 = (int)(velocity.Y * 1000f);
							if (num96 != num97 || num98 != num99)
							{
								netUpdate = true;
							}
							velocity.X = num89;
							velocity.Y = num90;
						}
					}
					else if (ai[0] == 0f)
					{
						ai[0] = 1f;
						netUpdate = true;
						float num100 = 12f;
						Vector2 vector7 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num101 = (float)Main.mouseX + Main.screenPosition.X - vector7.X;
						float num102 = (float)Main.mouseY + Main.screenPosition.Y - vector7.Y;
						float num103 = (float)Math.Sqrt(num101 * num101 + num102 * num102);
						if (num103 == 0f)
						{
							vector7 = new Vector2(Main.player[owner].position.X + (float)(Main.player[owner].width / 2), Main.player[owner].position.Y + (float)(Main.player[owner].height / 2));
							num101 = base.position.X + (float)base.width * 0.5f - vector7.X;
							num102 = base.position.Y + (float)base.height * 0.5f - vector7.Y;
							num103 = (float)Math.Sqrt(num101 * num101 + num102 * num102);
						}
						num103 = num100 / num103;
						num101 *= num103;
						num102 *= num103;
						velocity.X = num101;
						velocity.Y = num102;
						if (velocity.X == 0f && velocity.Y == 0f)
						{
							Kill();
						}
					}
				}
				if (type == 34)
				{
					rotation += 0.3f * (float)direction;
				}
				else if (velocity.X != 0f || velocity.Y != 0f)
				{
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) - 2.355f;
				}
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
			}
			else if (aiStyle == 10)
			{
				if (type == 31 && ai[0] != 2f)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position21 = new Vector2(base.position.X, base.position.Y);
						int width21 = base.width;
						int height21 = base.height;
						int num104 = 32;
						float speedX17 = 0f;
						float speedY17 = velocity.Y / 2f;
						int num105 = 0;
						int num106 = Dust.NewDust(position21, width21, height21, num104, speedX17, speedY17, num105);
						Dust dust12 = Main.dust[num106];
						dust12.velocity.X = dust12.velocity.X * 0.4f;
					}
				}
				else if (type == 39)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position22 = new Vector2(base.position.X, base.position.Y);
						int width22 = base.width;
						int height22 = base.height;
						int num107 = 38;
						float speedX18 = 0f;
						float speedY18 = velocity.Y / 2f;
						int num108 = 0;
						int num109 = Dust.NewDust(position22, width22, height22, num107, speedX18, speedY18, num108);
						Dust dust13 = Main.dust[num109];
						dust13.velocity.X = dust13.velocity.X * 0.4f;
					}
				}
				else if (type == 40)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position23 = new Vector2(base.position.X, base.position.Y);
						int width23 = base.width;
						int height23 = base.height;
						int num110 = 36;
						float speedX19 = 0f;
						float speedY19 = velocity.Y / 2f;
						int num111 = 0;
						int num112 = Dust.NewDust(position23, width23, height23, num110, speedX19, speedY19, num111);
						Dust dust14 = Main.dust[num112];
						dust14.velocity *= 0.4f;
					}
				}
				else if (type == 42 || type == 31)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position24 = new Vector2(base.position.X, base.position.Y);
						int width24 = base.width;
						int height24 = base.height;
						int num113 = 32;
						float speedX20 = 0f;
						float speedY20 = 0f;
						int num114 = 0;
						int num115 = Dust.NewDust(position24, width24, height24, num113, speedX20, speedY20, num114);
						Dust dust15 = Main.dust[num115];
						dust15.velocity.X = dust15.velocity.X * 0.4f;
					}
				}
				else if (type == 56 || type == 65)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position25 = new Vector2(base.position.X, base.position.Y);
						int width25 = base.width;
						int height25 = base.height;
						int num116 = 14;
						float speedX21 = 0f;
						float speedY21 = 0f;
						int num117 = 0;
						int num118 = Dust.NewDust(position25, width25, height25, num116, speedX21, speedY21, num117);
						Dust dust16 = Main.dust[num118];
						dust16.velocity.X = dust16.velocity.X * 0.4f;
					}
				}
				else if (type == 67 || type == 68)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position26 = new Vector2(base.position.X, base.position.Y);
						int width26 = base.width;
						int height26 = base.height;
						int num119 = 51;
						float speedX22 = 0f;
						float speedY22 = 0f;
						int num120 = 0;
						int num121 = Dust.NewDust(position26, width26, height26, num119, speedX22, speedY22, num120);
						Dust dust17 = Main.dust[num121];
						dust17.velocity.X = dust17.velocity.X * 0.4f;
					}
				}
				else if (type == 71)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 position27 = new Vector2(base.position.X, base.position.Y);
						int width27 = base.width;
						int height27 = base.height;
						int num122 = 53;
						float speedX23 = 0f;
						float speedY23 = 0f;
						int num123 = 0;
						int num124 = Dust.NewDust(position27, width27, height27, num122, speedX23, speedY23, num123);
						Dust dust18 = Main.dust[num124];
						dust18.velocity.X = dust18.velocity.X * 0.4f;
					}
				}
				else if (type != 109 && Main.rand.Next(20) == 0)
				{
					Vector2 position28 = new Vector2(base.position.X, base.position.Y);
					int width28 = base.width;
					int height28 = base.height;
					int num125 = 0;
					float speedX24 = 0f;
					float speedY24 = 0f;
					int num126 = 0;
					Dust.NewDust(position28, width28, height28, num125, speedX24, speedY24, num126);
				}
				if (Main.myPlayer == owner && ai[0] == 0f)
				{
					if (Main.player[owner].channel)
					{
						float num127 = 12f;
						Vector2 vector8 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
						float num128 = (float)Main.mouseX + Main.screenPosition.X - vector8.X;
						float num129 = (float)Main.mouseY + Main.screenPosition.Y - vector8.Y;
						float num130 = (float)Math.Sqrt(num128 * num128 + num129 * num129);
						num130 = (float)Math.Sqrt(num128 * num128 + num129 * num129);
						if (num130 > num127)
						{
							num130 = num127 / num130;
							num128 *= num130;
							num129 *= num130;
							if (num128 != velocity.X || num129 != velocity.Y)
							{
								netUpdate = true;
							}
							velocity.X = num128;
							velocity.Y = num129;
						}
						else
						{
							if (num128 != velocity.X || num129 != velocity.Y)
							{
								netUpdate = true;
							}
							velocity.X = num128;
							velocity.Y = num129;
						}
					}
					else
					{
						ai[0] = 1f;
						netUpdate = true;
					}
				}
				if (ai[0] == 1f && type != 109)
				{
					if (type == 42 || type == 65 || type == 68)
					{
						ai[1] += 1f;
						if (ai[1] >= 60f)
						{
							ai[1] = 60f;
							velocity.Y += 0.2f;
						}
					}
					else
					{
						velocity.Y += 0.41f;
					}
				}
				else if (ai[0] == 2f && type != 109)
				{
					velocity.Y += 0.2f;
					if ((double)velocity.X < -0.04)
					{
						velocity.X += 0.04f;
					}
					else if ((double)velocity.X > 0.04)
					{
						velocity.X -= 0.04f;
					}
					else
					{
						velocity.X = 0f;
					}
				}
				rotation += 0.1f;
				if (velocity.Y > 10f)
				{
					velocity.Y = 10f;
				}
			}
			else if (aiStyle == 11)
			{
				if (type == 72 || type == 86 || type == 87)
				{
					if (velocity.X > 0f)
					{
						spriteDirection = -1;
					}
					else if (velocity.X < 0f)
					{
						spriteDirection = 1;
					}
					rotation = velocity.X * 0.1f;
					frameCounter++;
					if (frameCounter >= 4)
					{
						frame++;
						frameCounter = 0;
					}
					if (frame >= 4)
					{
						frame = 0;
					}
					if (Main.rand.Next(6) == 0)
					{
						int num131 = 56;
						if (type == 86)
						{
							num131 = 73;
						}
						else if (type == 87)
						{
							num131 = 74;
						}
						Vector2 position29 = base.position;
						int width29 = base.width;
						int height29 = base.height;
						int num132 = num131;
						float speedX25 = 0f;
						float speedY25 = 0f;
						int num133 = 200;
						int num134 = Dust.NewDust(position29, width29, height29, num132, speedX25, speedY25, num133, default(Color), 0.8f);
						Dust dust19 = Main.dust[num134];
						dust19.velocity *= 0.3f;
					}
				}
				else
				{
					rotation += 0.02f;
				}
				if (Main.myPlayer == owner)
				{
					if (type == 72 || type == 86 || type == 87)
					{
						if (Main.player[Main.myPlayer].fairy)
						{
							timeLeft = 2;
						}
					}
					else if (Main.player[Main.myPlayer].lightOrb)
					{
						timeLeft = 2;
					}
				}
				if (Main.player[owner].dead)
				{
					Kill();
					return;
				}
				float num135 = 2.5f;
				if (type == 72 || type == 86 || type == 87)
				{
					num135 = 3.5f;
				}
				Vector2 vector9 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num136 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector9.X;
				float num137 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector9.Y;
				float num138 = (float)Math.Sqrt(num136 * num136 + num137 * num137);
				num138 = (float)Math.Sqrt(num136 * num136 + num137 * num137);
				int num139 = 70;
				if (type == 72 || type == 86 || type == 87)
				{
					num139 = 40;
				}
				if (num138 > 800f)
				{
					base.position.X = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - (float)(base.width / 2);
					base.position.Y = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - (float)(base.height / 2);
				}
				else if (num138 > (float)num139)
				{
					num138 = num135 / num138;
					num136 *= num138;
					num137 *= num138;
					velocity.X = num136;
					velocity.Y = num137;
				}
				else
				{
					velocity.X = 0f;
					velocity.Y = 0f;
				}
			}
			else if (aiStyle == 12)
			{
				scale -= 0.04f;
				if (scale <= 0f)
				{
					Kill();
				}
				if (ai[0] > 4f)
				{
					alpha = 150;
					light = 0.8f;
					Vector2 position30 = new Vector2(base.position.X, base.position.Y);
					int width30 = base.width;
					int height30 = base.height;
					int num140 = 29;
					float x5 = velocity.X;
					float y5 = velocity.Y;
					int num141 = 100;
					int num142 = Dust.NewDust(position30, width30, height30, num140, x5, y5, num141, default(Color), 2.5f);
					Main.dust[num142].noGravity = true;
					Vector2 position31 = new Vector2(base.position.X, base.position.Y);
					int width31 = base.width;
					int height31 = base.height;
					int num143 = 29;
					float x6 = velocity.X;
					float y6 = velocity.Y;
					int num144 = 100;
					Dust.NewDust(position31, width31, height31, num143, x6, y6, num144, default(Color), 1.5f);
				}
				else
				{
					ai[0] += 1f;
				}
				rotation += 0.3f * (float)direction;
			}
			else if (aiStyle == 13)
			{
				if (Main.player[owner].dead)
				{
					Kill();
					return;
				}
				Main.player[owner].itemAnimation = 5;
				Main.player[owner].itemTime = 5;
				if (base.position.X + (float)(base.width / 2) > Main.player[owner].position.X + (float)(Main.player[owner].width / 2))
				{
					Main.player[owner].direction = 1;
				}
				else
				{
					Main.player[owner].direction = -1;
				}
				Vector2 vector10 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num145 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector10.X;
				float num146 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector10.Y;
				float num147 = (float)Math.Sqrt(num145 * num145 + num146 * num146);
				if (ai[0] == 0f)
				{
					if (num147 > 700f)
					{
						ai[0] = 1f;
					}
					rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 1.57f;
					ai[1] += 1f;
					if (ai[1] > 2f)
					{
						alpha = 0;
					}
					if (ai[1] >= 10f)
					{
						ai[1] = 15f;
						velocity.Y += 0.3f;
					}
				}
				else if (ai[0] == 1f)
				{
					tileCollide = false;
					rotation = (float)Math.Atan2(num146, num145) - 1.57f;
					float num148 = 20f;
					if (num147 < 50f)
					{
						Kill();
					}
					num147 = num148 / num147;
					num145 *= num147;
					num146 *= num147;
					velocity.X = num145;
					velocity.Y = num146;
				}
			}
			else if (aiStyle == 14)
			{
				if (type == 53)
				{
					try
					{
						Collision.TileCollision(base.position, velocity, base.width, base.height);
						int num149 = (int)(base.position.X / 16f) - 1;
						int num150 = (int)((base.position.X + (float)base.width) / 16f) + 2;
						int num151 = (int)(base.position.Y / 16f) - 1;
						int num152 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
						if (num149 < 0)
						{
							num149 = 0;
						}
						if (num150 > Main.maxTilesX)
						{
							num150 = Main.maxTilesX;
						}
						if (num151 < 0)
						{
							num151 = 0;
						}
						if (num152 > Main.maxTilesY)
						{
							num152 = Main.maxTilesY;
						}
						Vector2 vector11 = default(Vector2);
						for (int num153 = num149; num153 < num150; num153++)
						{
							for (int num154 = num151; num154 < num152; num154++)
							{
								if (Main.tile[num153, num154] != null && Main.tile[num153, num154].active && (Main.tileSolid[Main.tile[num153, num154].type] || (Main.tileSolidTop[Main.tile[num153, num154].type] && Main.tile[num153, num154].frameY == 0)))
								{
									vector11.X = num153 * 16;
									vector11.Y = num154 * 16;
									if (base.position.X + (float)base.width > vector11.X && base.position.X < vector11.X + 16f && base.position.Y + (float)base.height > vector11.Y && base.position.Y < vector11.Y + 16f)
									{
										velocity.X = 0f;
										velocity.Y = -0.2f;
									}
								}
							}
						}
					}
					catch
					{
					}
				}
				ai[0] += 1f;
				if (ai[0] > 5f)
				{
					ai[0] = 5f;
					if (velocity.Y == 0f && velocity.X != 0f)
					{
						velocity.X *= 0.97f;
						if ((double)velocity.X > -0.01 && (double)velocity.X < 0.01)
						{
							velocity.X = 0f;
							netUpdate = true;
						}
					}
					velocity.Y += 0.2f;
				}
				rotation += velocity.X * 0.1f;
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
			}
			else if (aiStyle == 15)
			{
				if (type == 25)
				{
					if (Main.rand.Next(15) == 0)
					{
						Vector2 position32 = base.position;
						int width32 = base.width;
						int height32 = base.height;
						int num155 = 14;
						float speedX26 = 0f;
						float speedY26 = 0f;
						int num156 = 150;
						Dust.NewDust(position32, width32, height32, num155, speedX26, speedY26, num156, default(Color), 1.3f);
					}
				}
				else if (type == 26)
				{
					Vector2 position33 = base.position;
					int width33 = base.width;
					int height33 = base.height;
					int num157 = 29;
					float speedX27 = velocity.X * 0.4f;
					float speedY27 = velocity.Y * 0.4f;
					int num158 = 100;
					int num159 = Dust.NewDust(position33, width33, height33, num157, speedX27, speedY27, num158, default(Color), 2.5f);
					Main.dust[num159].noGravity = true;
					Dust dust20 = Main.dust[num159];
					dust20.velocity.X = dust20.velocity.X / 2f;
					Dust dust21 = Main.dust[num159];
					dust21.velocity.Y = dust21.velocity.Y / 2f;
				}
				else if (type == 35)
				{
					Vector2 position34 = base.position;
					int width34 = base.width;
					int height34 = base.height;
					int num160 = 6;
					float speedX28 = velocity.X * 0.4f;
					float speedY28 = velocity.Y * 0.4f;
					int num161 = 100;
					int num162 = Dust.NewDust(position34, width34, height34, num160, speedX28, speedY28, num161, default(Color), 3f);
					Main.dust[num162].noGravity = true;
					Dust dust22 = Main.dust[num162];
					dust22.velocity.X = dust22.velocity.X * 2f;
					Dust dust23 = Main.dust[num162];
					dust23.velocity.Y = dust23.velocity.Y * 2f;
				}
				if (Main.player[owner].dead)
				{
					Kill();
					return;
				}
				Main.player[owner].itemAnimation = 10;
				Main.player[owner].itemTime = 10;
				if (base.position.X + (float)(base.width / 2) > Main.player[owner].position.X + (float)(Main.player[owner].width / 2))
				{
					Main.player[owner].direction = 1;
					direction = 1;
				}
				else
				{
					Main.player[owner].direction = -1;
					direction = -1;
				}
				Vector2 vector12 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num163 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector12.X;
				float num164 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector12.Y;
				float num165 = (float)Math.Sqrt(num163 * num163 + num164 * num164);
				if (ai[0] == 0f)
				{
					float num166 = 160f;
					if (type == 63)
					{
						num166 *= 1.5f;
					}
					tileCollide = true;
					if (num165 > num166)
					{
						ai[0] = 1f;
						netUpdate = true;
					}
					else if (!Main.player[owner].channel)
					{
						if (velocity.Y < 0f)
						{
							velocity.Y *= 0.9f;
						}
						velocity.Y += 1f;
						velocity.X *= 0.9f;
					}
				}
				else if (ai[0] == 1f)
				{
					float num167 = 14f / Main.player[owner].meleeSpeed;
					float num168 = 0.9f / Main.player[owner].meleeSpeed;
					float num169 = 300f;
					if (type == 63)
					{
						num169 *= 1.5f;
						num167 *= 1.5f;
						num168 *= 1.5f;
					}
					Math.Abs(num163);
					Math.Abs(num164);
					if (ai[1] == 1f)
					{
						tileCollide = false;
					}
					if (!Main.player[owner].channel || num165 > num169 || !tileCollide)
					{
						ai[1] = 1f;
						if (tileCollide)
						{
							netUpdate = true;
						}
						tileCollide = false;
						if (num165 < 20f)
						{
							Kill();
						}
					}
					if (!tileCollide)
					{
						num168 *= 2f;
					}
					if (num165 > 60f || !tileCollide)
					{
						num165 = num167 / num165;
						num163 *= num165;
						num164 *= num165;
						new Vector2(velocity.X, velocity.Y);
						float num170 = num163 - velocity.X;
						float num171 = num164 - velocity.Y;
						float num172 = (float)Math.Sqrt(num170 * num170 + num171 * num171);
						num172 = num168 / num172;
						num170 *= num172;
						num171 *= num172;
						velocity.X *= 0.98f;
						velocity.Y *= 0.98f;
						velocity.X += num170;
						velocity.Y += num171;
					}
					else
					{
						if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < 6f)
						{
							velocity.X *= 0.96f;
							velocity.Y += 0.2f;
						}
						if (Main.player[owner].velocity.X == 0f)
						{
							velocity.X *= 0.96f;
						}
					}
				}
				rotation = (float)Math.Atan2(num164, num163) - velocity.X * 0.1f;
			}
			else if (aiStyle == 16)
			{
				if (type == 108)
				{
					ai[0] += 1f;
					if (ai[0] > 3f)
					{
						Kill();
					}
				}
				if (type == 37)
				{
					try
					{
						int num173 = (int)(base.position.X / 16f) - 1;
						int num174 = (int)((base.position.X + (float)base.width) / 16f) + 2;
						int num175 = (int)(base.position.Y / 16f) - 1;
						int num176 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
						if (num173 < 0)
						{
							num173 = 0;
						}
						if (num174 > Main.maxTilesX)
						{
							num174 = Main.maxTilesX;
						}
						if (num175 < 0)
						{
							num175 = 0;
						}
						if (num176 > Main.maxTilesY)
						{
							num176 = Main.maxTilesY;
						}
						Vector2 vector13 = default(Vector2);
						for (int num177 = num173; num177 < num174; num177++)
						{
							for (int num178 = num175; num178 < num176; num178++)
							{
								if (Main.tile[num177, num178] != null && Main.tile[num177, num178].active && (Main.tileSolid[Main.tile[num177, num178].type] || (Main.tileSolidTop[Main.tile[num177, num178].type] && Main.tile[num177, num178].frameY == 0)))
								{
									vector13.X = num177 * 16;
									vector13.Y = num178 * 16;
									if (base.position.X + (float)base.width - 4f > vector13.X && base.position.X + 4f < vector13.X + 16f && base.position.Y + (float)base.height - 4f > vector13.Y && base.position.Y + 4f < vector13.Y + 16f)
									{
										velocity.X = 0f;
										velocity.Y = -0.2f;
									}
								}
							}
						}
					}
					catch
					{
					}
				}
				if (type == 102)
				{
					if (velocity.Y > 10f)
					{
						velocity.Y = 10f;
					}
					if (localAI[0] == 0f)
					{
						localAI[0] = 1f;
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					}
					frameCounter++;
					if (frameCounter > 3)
					{
						frame++;
						frameCounter = 0;
					}
					if (frame > 1)
					{
						frame = 0;
					}
					if (velocity.Y == 0f)
					{
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 128;
						base.height = 128;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
						damage = 40;
						knockBack = 8f;
						timeLeft = 3;
						netUpdate = true;
					}
				}
				if (owner == Main.myPlayer && timeLeft <= 3)
				{
					ai[1] = 0f;
					alpha = 255;
					if (type == 28 || type == 37 || type == 75)
					{
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 128;
						base.height = 128;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
						damage = 100;
						knockBack = 8f;
					}
					else if (type == 29)
					{
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 250;
						base.height = 250;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
						damage = 250;
						knockBack = 10f;
					}
					else if (type == 30)
					{
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 128;
						base.height = 128;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
						knockBack = 8f;
					}
				}
				else
				{
					if (type != 30 && type != 108)
					{
						damage = 0;
					}
					if (type != 30 && Main.rand.Next(4) == 0)
					{
						Vector2 position35 = new Vector2(base.position.X, base.position.Y);
						int width35 = base.width;
						int height35 = base.height;
						int num179 = 6;
						float speedX29 = 0f;
						float speedY29 = 0f;
						int num180 = 100;
						Dust.NewDust(position35, width35, height35, num179, speedX29, speedY29, num180);
					}
				}
				ai[0] += 1f;
				if ((type == 30 && ai[0] > 10f) || (type != 30 && ai[0] > 5f))
				{
					ai[0] = 10f;
					if (velocity.Y == 0f && velocity.X != 0f)
					{
						velocity.X *= 0.97f;
						if (type == 29)
						{
							velocity.X *= 0.99f;
						}
						if ((double)velocity.X > -0.01 && (double)velocity.X < 0.01)
						{
							velocity.X = 0f;
							netUpdate = true;
						}
					}
					velocity.Y += 0.2f;
				}
				rotation += velocity.X * 0.1f;
			}
			else if (aiStyle == 17)
			{
				if (velocity.Y == 0f)
				{
					velocity.X *= 0.98f;
				}
				rotation += velocity.X * 0.1f;
				velocity.Y += 0.2f;
				if (owner != Main.myPlayer)
				{
					return;
				}
				int num181 = (int)((base.position.X + (float)(base.width / 2)) / 16f);
				int num182 = (int)((base.position.Y + (float)base.height - 4f) / 16f);
				if (Main.tile[num181, num182] == null || Main.tile[num181, num182].active)
				{
					return;
				}
				WorldGen.PlaceTile(num181, num182, 85);
				if (Main.tile[num181, num182].active)
				{
					if (Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, "", 1, num181, num182, 85f);
					}
					int num183 = Sign.ReadSign(num181, num182);
					if (num183 >= 0)
					{
						Sign.TextSign(num183, miscText);
					}
					Kill();
				}
			}
			else if (aiStyle == 18)
			{
				if (ai[1] == 0f && type == 44)
				{
					ai[1] = 1f;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
				}
				rotation += (float)direction * 0.8f;
				ai[0] += 1f;
				if (ai[0] >= 30f)
				{
					if (ai[0] < 100f)
					{
						velocity *= 1.06f;
					}
					else
					{
						ai[0] = 200f;
					}
				}
				for (int num184 = 0; num184 < 2; num184++)
				{
					Vector2 position36 = new Vector2(base.position.X, base.position.Y);
					int width36 = base.width;
					int height36 = base.height;
					int num185 = 27;
					float speedX30 = 0f;
					float speedY30 = 0f;
					int num186 = 100;
					int num187 = Dust.NewDust(position36, width36, height36, num185, speedX30, speedY30, num186);
					Main.dust[num187].noGravity = true;
				}
			}
			else if (aiStyle == 19)
			{
				direction = Main.player[owner].direction;
				Main.player[owner].heldProj = whoAmI;
				Main.player[owner].itemTime = Main.player[owner].itemAnimation;
				base.position.X = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - (float)(base.width / 2);
				base.position.Y = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - (float)(base.height / 2);
				if (type == 46)
				{
					if (ai[0] == 0f)
					{
						ai[0] = 3f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 1.6f;
					}
					else
					{
						ai[0] += 1.4f;
					}
				}
				else if (type == 105)
				{
					if (ai[0] == 0f)
					{
						ai[0] = 3f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 2.4f;
					}
					else
					{
						ai[0] += 2.1f;
					}
				}
				else if (type == 47)
				{
					if (ai[0] == 0f)
					{
						ai[0] = 4f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 1.2f;
					}
					else
					{
						ai[0] += 0.9f;
					}
				}
				else if (type == 49)
				{
					if (ai[0] == 0f)
					{
						ai[0] = 4f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 1.1f;
					}
					else
					{
						ai[0] += 0.85f;
					}
				}
				else if (type == 64)
				{
					spriteDirection = -direction;
					if (ai[0] == 0f)
					{
						ai[0] = 3f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 1.9f;
					}
					else
					{
						ai[0] += 1.7f;
					}
				}
				else if (type == 66 || type == 97)
				{
					spriteDirection = -direction;
					if (ai[0] == 0f)
					{
						ai[0] = 3f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 2.1f;
					}
					else
					{
						ai[0] += 1.9f;
					}
				}
				else if (type == 97)
				{
					spriteDirection = -direction;
					if (ai[0] == 0f)
					{
						ai[0] = 3f;
						netUpdate = true;
					}
					if (Main.player[owner].itemAnimation < Main.player[owner].itemAnimationMax / 3)
					{
						ai[0] -= 1.6f;
					}
					else
					{
						ai[0] += 1.4f;
					}
				}
				base.position += velocity * ai[0];
				if (Main.player[owner].itemAnimation == 0)
				{
					Kill();
				}
				rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 2.355f;
				if (spriteDirection == -1)
				{
					rotation -= 1.57f;
				}
				if (type == 46)
				{
					if (Main.rand.Next(5) == 0)
					{
						Vector2 position37 = base.position;
						int width37 = base.width;
						int height37 = base.height;
						int num188 = 14;
						float speedX31 = 0f;
						float speedY31 = 0f;
						int num189 = 150;
						Dust.NewDust(position37, width37, height37, num188, speedX31, speedY31, num189, default(Color), 1.4f);
					}
					Vector2 position38 = base.position;
					int width38 = base.width;
					int height38 = base.height;
					int num190 = 27;
					float speedX32 = velocity.X * 0.2f + (float)(direction * 3);
					float speedY32 = velocity.Y * 0.2f;
					int num191 = 100;
					int num192 = Dust.NewDust(position38, width38, height38, num190, speedX32, speedY32, num191, default(Color), 1.2f);
					Main.dust[num192].noGravity = true;
					Dust dust24 = Main.dust[num192];
					dust24.velocity.X = dust24.velocity.X / 2f;
					Dust dust25 = Main.dust[num192];
					dust25.velocity.Y = dust25.velocity.Y / 2f;
					Vector2 position39 = base.position - velocity * 2f;
					int width39 = base.width;
					int height39 = base.height;
					int num193 = 27;
					float speedX33 = 0f;
					float speedY33 = 0f;
					int num194 = 150;
					num192 = Dust.NewDust(position39, width39, height39, num193, speedX33, speedY33, num194, default(Color), 1.4f);
					Dust dust26 = Main.dust[num192];
					dust26.velocity.X = dust26.velocity.X / 5f;
					Dust dust27 = Main.dust[num192];
					dust27.velocity.Y = dust27.velocity.Y / 5f;
				}
				else if (type == 105)
				{
					if (Main.rand.Next(3) == 0)
					{
						Vector2 position40 = new Vector2(base.position.X, base.position.Y);
						int width40 = base.width;
						int height40 = base.height;
						int num195 = 57;
						float speedX34 = velocity.X * 0.2f;
						float speedY34 = velocity.Y * 0.2f;
						int num196 = 200;
						int num197 = Dust.NewDust(position40, width40, height40, num195, speedX34, speedY34, num196, default(Color), 1.2f);
						Dust dust28 = Main.dust[num197];
						dust28.velocity += velocity * 0.3f;
						Dust dust29 = Main.dust[num197];
						dust29.velocity *= 0.2f;
					}
					if (Main.rand.Next(4) == 0)
					{
						Vector2 position41 = new Vector2(base.position.X, base.position.Y);
						int width41 = base.width;
						int height41 = base.height;
						int num198 = 43;
						float speedX35 = 0f;
						float speedY35 = 0f;
						int num199 = 254;
						int num200 = Dust.NewDust(position41, width41, height41, num198, speedX35, speedY35, num199, default(Color), 0.3f);
						Dust dust30 = Main.dust[num200];
						dust30.velocity += velocity * 0.5f;
						Dust dust31 = Main.dust[num200];
						dust31.velocity *= 0.5f;
					}
				}
			}
			else if (aiStyle == 20)
			{
				if (soundDelay <= 0)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 22);
					soundDelay = 30;
				}
				if (Main.myPlayer == owner)
				{
					if (Main.player[owner].channel)
					{
						float num201 = Main.player[owner].inventory[Main.player[owner].selectedItem].shootSpeed * scale;
						Vector2 vector14 = new Vector2(Main.player[owner].position.X + (float)Main.player[owner].width * 0.5f, Main.player[owner].position.Y + (float)Main.player[owner].height * 0.5f);
						float num202 = (float)Main.mouseX + Main.screenPosition.X - vector14.X;
						float num203 = (float)Main.mouseY + Main.screenPosition.Y - vector14.Y;
						float num204 = (float)Math.Sqrt(num202 * num202 + num203 * num203);
						num204 = (float)Math.Sqrt(num202 * num202 + num203 * num203);
						num204 = num201 / num204;
						num202 *= num204;
						num203 *= num204;
						if (num202 != velocity.X || num203 != velocity.Y)
						{
							netUpdate = true;
						}
						velocity.X = num202;
						velocity.Y = num203;
					}
					else
					{
						Kill();
					}
				}
				if (velocity.X > 0f)
				{
					Main.player[owner].direction = 1;
				}
				else if (velocity.X < 0f)
				{
					Main.player[owner].direction = -1;
				}
				spriteDirection = direction;
				Main.player[owner].direction = direction;
				Main.player[owner].heldProj = whoAmI;
				Main.player[owner].itemTime = 2;
				Main.player[owner].itemAnimation = 2;
				base.position.X = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - (float)(base.width / 2);
				base.position.Y = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - (float)(base.height / 2);
				rotation = (float)(Math.Atan2(velocity.Y, velocity.X) + 1.5700000524520874);
				if (Main.player[owner].direction == 1)
				{
					Main.player[owner].itemRotation = (float)Math.Atan2(velocity.Y * (float)direction, velocity.X * (float)direction);
				}
				else
				{
					Main.player[owner].itemRotation = (float)Math.Atan2(velocity.Y * (float)direction, velocity.X * (float)direction);
				}
				velocity.X *= 1f + (float)Main.rand.Next(-3, 4) * 0.01f;
				if (Main.rand.Next(6) == 0)
				{
					Vector2 position42 = base.position + velocity * Main.rand.Next(6, 10) * 0.1f;
					int width42 = base.width;
					int height42 = base.height;
					int num205 = 31;
					float speedX36 = 0f;
					float speedY36 = 0f;
					int num206 = 80;
					int num207 = Dust.NewDust(position42, width42, height42, num205, speedX36, speedY36, num206, default(Color), 1.4f);
					Dust dust32 = Main.dust[num207];
					dust32.position.X = dust32.position.X - 4f;
					Main.dust[num207].noGravity = true;
					Dust dust33 = Main.dust[num207];
					dust33.velocity *= 0.2f;
					Main.dust[num207].velocity.Y = (0f - (float)Main.rand.Next(7, 13)) * 0.15f;
				}
			}
			else if (aiStyle == 21)
			{
				rotation = velocity.X * 0.1f;
				spriteDirection = -direction;
				if (Main.rand.Next(3) == 0)
				{
					Vector2 position43 = base.position;
					int width43 = base.width;
					int height43 = base.height;
					int num208 = 27;
					float speedX37 = 0f;
					float speedY37 = 0f;
					int num209 = 80;
					int num210 = Dust.NewDust(position43, width43, height43, num208, speedX37, speedY37, num209);
					Main.dust[num210].noGravity = true;
					Dust dust34 = Main.dust[num210];
					dust34.velocity *= 0.2f;
				}
				if (ai[1] == 1f)
				{
					ai[1] = 0f;
					Main.harpNote = ai[0];
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 26);
				}
			}
			else if (aiStyle == 22)
			{
				if (velocity.X == 0f && velocity.Y == 0f)
				{
					alpha = 255;
				}
				if (ai[1] < 0f)
				{
					if (velocity.X > 0f)
					{
						rotation += 0.3f;
					}
					else
					{
						rotation -= 0.3f;
					}
					int num211 = (int)(base.position.X / 16f) - 1;
					int num212 = (int)((base.position.X + (float)base.width) / 16f) + 2;
					int num213 = (int)(base.position.Y / 16f) - 1;
					int num214 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
					if (num211 < 0)
					{
						num211 = 0;
					}
					if (num212 > Main.maxTilesX)
					{
						num212 = Main.maxTilesX;
					}
					if (num213 < 0)
					{
						num213 = 0;
					}
					if (num214 > Main.maxTilesY)
					{
						num214 = Main.maxTilesY;
					}
					int num215 = (int)base.position.X + 4;
					int num216 = (int)base.position.Y + 4;
					Vector2 vector15 = default(Vector2);
					for (int num217 = num211; num217 < num212; num217++)
					{
						for (int num218 = num213; num218 < num214; num218++)
						{
							if (Main.tile[num217, num218] != null && Main.tile[num217, num218].active && Main.tile[num217, num218].type != 127 && Main.tileSolid[Main.tile[num217, num218].type] && !Main.tileSolidTop[Main.tile[num217, num218].type])
							{
								vector15.X = num217 * 16;
								vector15.Y = num218 * 16;
								if ((float)(num215 + 8) > vector15.X && (float)num215 < vector15.X + 16f && (float)(num216 + 8) > vector15.Y && (float)num216 < vector15.Y + 16f)
								{
									Kill();
								}
							}
						}
					}
					Vector2 position44 = new Vector2(base.position.X, base.position.Y);
					int width44 = base.width;
					int height44 = base.height;
					int num219 = 67;
					float speedX38 = 0f;
					float speedY38 = 0f;
					int num220 = 0;
					int num221 = Dust.NewDust(position44, width44, height44, num219, speedX38, speedY38, num220);
					Main.dust[num221].noGravity = true;
					Dust dust35 = Main.dust[num221];
					dust35.velocity *= 0.3f;
					return;
				}
				if (ai[0] < 0f)
				{
					if (ai[0] == -1f)
					{
						for (int num222 = 0; num222 < 10; num222++)
						{
							Vector2 position45 = new Vector2(base.position.X, base.position.Y);
							int width45 = base.width;
							int height45 = base.height;
							int num223 = 67;
							float speedX39 = 0f;
							float speedY39 = 0f;
							int num224 = 0;
							int num225 = Dust.NewDust(position45, width45, height45, num223, speedX39, speedY39, num224, default(Color), 1.1f);
							Main.dust[num225].noGravity = true;
							Dust dust36 = Main.dust[num225];
							dust36.velocity *= 1.3f;
						}
					}
					else if (Main.rand.Next(30) == 0)
					{
						Vector2 position46 = new Vector2(base.position.X, base.position.Y);
						int width46 = base.width;
						int height46 = base.height;
						int num226 = 67;
						float speedX40 = 0f;
						float speedY40 = 0f;
						int num227 = 100;
						int num228 = Dust.NewDust(position46, width46, height46, num226, speedX40, speedY40, num227);
						Dust dust37 = Main.dust[num228];
						dust37.velocity *= 0.2f;
					}
					int num229 = (int)base.position.X / 16;
					int num230 = (int)base.position.Y / 16;
					if (Main.tile[num229, num230] == null || !Main.tile[num229, num230].active)
					{
						Kill();
					}
					ai[0] -= 1f;
					if (ai[0] <= -300f && (Main.myPlayer == owner || Main.netMode == 2) && Main.tile[num229, num230].active && Main.tile[num229, num230].type == 127)
					{
						WorldGen.KillTile(num229, num230);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, "", 0, num229, num230);
						}
						Kill();
					}
					return;
				}
				int num231 = (int)(base.position.X / 16f) - 1;
				int num232 = (int)((base.position.X + (float)base.width) / 16f) + 2;
				int num233 = (int)(base.position.Y / 16f) - 1;
				int num234 = (int)((base.position.Y + (float)base.height) / 16f) + 2;
				if (num231 < 0)
				{
					num231 = 0;
				}
				if (num232 > Main.maxTilesX)
				{
					num232 = Main.maxTilesX;
				}
				if (num233 < 0)
				{
					num233 = 0;
				}
				if (num234 > Main.maxTilesY)
				{
					num234 = Main.maxTilesY;
				}
				int num235 = (int)base.position.X + 4;
				int num236 = (int)base.position.Y + 4;
				Vector2 vector16 = default(Vector2);
				for (int num237 = num231; num237 < num232; num237++)
				{
					for (int num238 = num233; num238 < num234; num238++)
					{
						if (Main.tile[num237, num238] != null && Main.tile[num237, num238].active && Main.tile[num237, num238].type != 127 && Main.tileSolid[Main.tile[num237, num238].type] && !Main.tileSolidTop[Main.tile[num237, num238].type])
						{
							vector16.X = num237 * 16;
							vector16.Y = num238 * 16;
							if ((float)(num235 + 8) > vector16.X && (float)num235 < vector16.X + 16f && (float)(num236 + 8) > vector16.Y && (float)num236 < vector16.Y + 16f)
							{
								Kill();
							}
						}
					}
				}
				if (lavaWet)
				{
					Kill();
				}
				if (!active)
				{
					return;
				}
				Vector2 position47 = new Vector2(base.position.X, base.position.Y);
				int width47 = base.width;
				int height47 = base.height;
				int num239 = 67;
				float speedX41 = 0f;
				float speedY41 = 0f;
				int num240 = 0;
				int num241 = Dust.NewDust(position47, width47, height47, num239, speedX41, speedY41, num240);
				Main.dust[num241].noGravity = true;
				Dust dust38 = Main.dust[num241];
				dust38.velocity *= 0.3f;
				int num242 = (int)ai[0];
				int num243 = (int)ai[1];
				if (velocity.X > 0f)
				{
					rotation += 0.3f;
				}
				else
				{
					rotation -= 0.3f;
				}
				if (Main.myPlayer != owner)
				{
					return;
				}
				int num244 = (int)((base.position.X + (float)(base.width / 2)) / 16f);
				int num245 = (int)((base.position.Y + (float)(base.height / 2)) / 16f);
				bool flag2 = false;
				if (num244 == num242 && num245 == num243)
				{
					flag2 = true;
				}
				if (((velocity.X <= 0f && num244 <= num242) || (velocity.X >= 0f && num244 >= num242)) && ((velocity.Y <= 0f && num245 <= num243) || (velocity.Y >= 0f && num245 >= num243)))
				{
					flag2 = true;
				}
				if (!flag2)
				{
					return;
				}
				if (WorldGen.PlaceTile(num242, num243, 127, mute: false, forced: false, owner))
				{
					if (Main.netMode == 1)
					{
						NetMessage.SendData(17, -1, -1, "", 1, (int)ai[0], (int)ai[1], 127f);
					}
					damage = 0;
					ai[0] = -1f;
					velocity *= 0f;
					alpha = 255;
					base.position.X = num242 * 16;
					base.position.Y = num243 * 16;
					netUpdate = true;
				}
				else
				{
					ai[1] = -1f;
				}
			}
			else if (aiStyle == 23)
			{
				if (timeLeft > 60)
				{
					timeLeft = 60;
				}
				if (ai[0] > 7f)
				{
					float num246 = 1f;
					if (ai[0] == 8f)
					{
						num246 = 0.25f;
					}
					else if (ai[0] == 9f)
					{
						num246 = 0.5f;
					}
					else if (ai[0] == 10f)
					{
						num246 = 0.75f;
					}
					ai[0] += 1f;
					int num247 = 6;
					if (type == 101)
					{
						num247 = 75;
					}
					if (num247 == 6 || Main.rand.Next(2) == 0)
					{
						for (int num248 = 0; num248 < 1; num248++)
						{
							Vector2 position48 = new Vector2(base.position.X, base.position.Y);
							int width48 = base.width;
							int height48 = base.height;
							int num249 = num247;
							float speedX42 = velocity.X * 0.2f;
							float speedY42 = velocity.Y * 0.2f;
							int num250 = 100;
							int num251 = Dust.NewDust(position48, width48, height48, num249, speedX42, speedY42, num250);
							if (Main.rand.Next(3) != 0 || (num247 == 75 && Main.rand.Next(3) == 0))
							{
								Main.dust[num251].noGravity = true;
								Main.dust[num251].scale *= 3f;
								Dust dust39 = Main.dust[num251];
								dust39.velocity.X = dust39.velocity.X * 2f;
								Dust dust40 = Main.dust[num251];
								dust40.velocity.Y = dust40.velocity.Y * 2f;
							}
							Main.dust[num251].scale *= 1.5f;
							Dust dust41 = Main.dust[num251];
							dust41.velocity.X = dust41.velocity.X * 1.2f;
							Dust dust42 = Main.dust[num251];
							dust42.velocity.Y = dust42.velocity.Y * 1.2f;
							Main.dust[num251].scale *= num246;
							if (num247 == 75)
							{
								Dust dust43 = Main.dust[num251];
								dust43.velocity += velocity;
								if (!Main.dust[num251].noGravity)
								{
									Dust dust44 = Main.dust[num251];
									dust44.velocity *= 0.5f;
								}
							}
						}
					}
				}
				else
				{
					ai[0] += 1f;
				}
				rotation += 0.3f * (float)direction;
			}
			else if (aiStyle == 24)
			{
				light = scale * 0.5f;
				rotation += velocity.X * 0.2f;
				ai[1] += 1f;
				if (type == 94)
				{
					if (Main.rand.Next(4) == 0)
					{
						Vector2 position49 = new Vector2(base.position.X, base.position.Y);
						int width49 = base.width;
						int height49 = base.height;
						int num252 = 70;
						float speedX43 = 0f;
						float speedY43 = 0f;
						int num253 = 0;
						int num254 = Dust.NewDust(position49, width49, height49, num252, speedX43, speedY43, num253);
						Main.dust[num254].noGravity = true;
						Dust dust45 = Main.dust[num254];
						dust45.velocity *= 0.5f;
						Main.dust[num254].scale *= 0.9f;
					}
					velocity *= 0.985f;
					if (ai[1] > 130f)
					{
						scale -= 0.05f;
						if ((double)scale <= 0.2)
						{
							scale = 0.2f;
							Kill();
						}
					}
					return;
				}
				velocity *= 0.96f;
				if (ai[1] > 15f)
				{
					scale -= 0.05f;
					if ((double)scale <= 0.2)
					{
						scale = 0.2f;
						Kill();
					}
				}
			}
			else if (aiStyle == 25)
			{
				if (ai[0] != 0f && velocity.Y <= 0f && velocity.X == 0f)
				{
					float num255 = 0.5f;
					int i2 = (int)((base.position.X - 8f) / 16f);
					int num256 = (int)(base.position.Y / 16f);
					bool flag3 = false;
					bool flag4 = false;
					if (WorldGen.SolidTile(i2, num256) || WorldGen.SolidTile(i2, num256 + 1))
					{
						flag3 = true;
					}
					i2 = (int)((base.position.X + (float)base.width + 8f) / 16f);
					if (WorldGen.SolidTile(i2, num256) || WorldGen.SolidTile(i2, num256 + 1))
					{
						flag4 = true;
					}
					if (flag3)
					{
						velocity.X = num255;
					}
					else if (flag4)
					{
						velocity.X = 0f - num255;
					}
					else
					{
						i2 = (int)((base.position.X - 8f - 16f) / 16f);
						num256 = (int)(base.position.Y / 16f);
						flag3 = false;
						flag4 = false;
						if (WorldGen.SolidTile(i2, num256) || WorldGen.SolidTile(i2, num256 + 1))
						{
							flag3 = true;
						}
						i2 = (int)((base.position.X + (float)base.width + 8f + 16f) / 16f);
						if (WorldGen.SolidTile(i2, num256) || WorldGen.SolidTile(i2, num256 + 1))
						{
							flag4 = true;
						}
						if (flag3)
						{
							velocity.X = num255;
						}
						else if (flag4)
						{
							velocity.X = 0f - num255;
						}
						else
						{
							i2 = (int)((base.position.X + 4f) / 16f);
							num256 = (int)((base.position.Y + (float)base.height + 8f) / 16f);
							if (WorldGen.SolidTile(i2, num256) || WorldGen.SolidTile(i2, num256 + 1))
							{
								flag3 = true;
							}
							if (!flag3)
							{
								velocity.X = num255;
							}
							else
							{
								velocity.X = 0f - num255;
							}
						}
					}
				}
				rotation += velocity.X * 0.06f;
				ai[0] = 1f;
				if (velocity.Y > 16f)
				{
					velocity.Y = 16f;
				}
				if (velocity.Y <= 6f)
				{
					if (velocity.X > 0f && velocity.X < 7f)
					{
						velocity.X += 0.05f;
					}
					if (velocity.X < 0f && velocity.X > -7f)
					{
						velocity.X -= 0.05f;
					}
				}
				velocity.Y += 0.3f;
			}
			else
			{
				if (aiStyle != 26)
				{
					return;
				}
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				int num257 = 60;
				if (Main.myPlayer == owner)
				{
					if (Main.player[Main.myPlayer].dead)
					{
						Main.player[Main.myPlayer].bunny = false;
					}
					if (Main.player[Main.myPlayer].bunny)
					{
						timeLeft = 2;
					}
				}
				if (Main.player[owner].position.X + (float)(Main.player[owner].width / 2) < base.position.X + (float)(base.width / 2) - (float)num257)
				{
					flag5 = true;
				}
				else if (Main.player[owner].position.X + (float)(Main.player[owner].width / 2) > base.position.X + (float)(base.width / 2) + (float)num257)
				{
					flag6 = true;
				}
				if (Main.player[owner].rocketDelay2 > 0)
				{
					ai[0] = 1f;
				}
				Vector2 vector17 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
				float num258 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector17.X;
				float num259 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector17.Y;
				float num260 = (float)Math.Sqrt(num258 * num258 + num259 * num259);
				if (num260 > 2000f)
				{
					base.position.X = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - (float)(base.width / 2);
					base.position.Y = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - (float)(base.height / 2);
				}
				else if (num260 > 500f || Math.Abs(num259) > 300f)
				{
					ai[0] = 1f;
					if (num259 > 0f && velocity.Y < 0f)
					{
						velocity.Y = 0f;
					}
					if (num259 < 0f && velocity.Y > 0f)
					{
						velocity.Y = 0f;
					}
				}
				if (ai[0] != 0f)
				{
					tileCollide = false;
					Vector2 vector18 = new Vector2(base.position.X + (float)base.width * 0.5f, base.position.Y + (float)base.height * 0.5f);
					float num261 = Main.player[owner].position.X + (float)(Main.player[owner].width / 2) - vector18.X;
					float num262 = Main.player[owner].position.Y + (float)(Main.player[owner].height / 2) - vector18.Y;
					float num263 = (float)Math.Sqrt(num261 * num261 + num262 * num262);
					float num264 = 10f;
					if (num263 < 200f && Main.player[owner].velocity.Y == 0f && base.position.Y + (float)base.height <= Main.player[owner].position.Y + (float)Main.player[owner].height && !Collision.SolidCollision(base.position, base.width, base.height))
					{
						ai[0] = 0f;
						if (velocity.Y < -6f)
						{
							velocity.Y = -6f;
						}
					}
					if (num263 < 60f)
					{
						num261 = velocity.X;
						num262 = velocity.Y;
					}
					else
					{
						num263 = num264 / num263;
						num261 *= num263;
						num262 *= num263;
					}
					if (velocity.X < num261)
					{
						velocity.X += 0.2f;
						if (velocity.X < 0f)
						{
							velocity.X += 0.3f;
						}
					}
					if (velocity.X > num261)
					{
						velocity.X -= 0.2f;
						if (velocity.X > 0f)
						{
							velocity.X -= 0.3f;
						}
					}
					if (velocity.Y < num262)
					{
						velocity.Y += 0.2f;
						if (velocity.Y < 0f)
						{
							velocity.Y += 0.3f;
						}
					}
					if (velocity.Y > num262)
					{
						velocity.Y -= 0.2f;
						if (velocity.Y > 0f)
						{
							velocity.Y -= 0.3f;
						}
					}
					frame = 7;
					if ((double)velocity.X > 0.5)
					{
						spriteDirection = -1;
					}
					else if ((double)velocity.X < -0.5)
					{
						spriteDirection = 1;
					}
					if (spriteDirection == -1)
					{
						rotation = (float)Math.Atan2(velocity.Y, velocity.X);
					}
					else
					{
						rotation = (float)Math.Atan2(velocity.Y, velocity.X) + 3.14f;
					}
					Vector2 position50 = new Vector2(base.position.X + (float)(base.width / 2) - 4f, base.position.Y + (float)(base.height / 2) - 4f) - velocity;
					int width50 = 8;
					int height50 = 8;
					int num265 = 16;
					float speedX44 = (0f - velocity.X) * 0.5f;
					float speedY44 = velocity.Y * 0.5f;
					int num266 = 50;
					int num267 = Dust.NewDust(position50, width50, height50, num265, speedX44, speedY44, num266, default(Color), 1.7f);
					Main.dust[num267].velocity.X = Main.dust[num267].velocity.X * 0.2f;
					Main.dust[num267].velocity.Y = Main.dust[num267].velocity.Y * 0.2f;
					Main.dust[num267].noGravity = true;
					return;
				}
				rotation = 0f;
				tileCollide = true;
				if (flag5)
				{
					if ((double)velocity.X > -3.5)
					{
						velocity.X -= 0.08f;
					}
					else
					{
						velocity.X -= 0.02f;
					}
				}
				else if (flag6)
				{
					if ((double)velocity.X < 3.5)
					{
						velocity.X += 0.08f;
					}
					else
					{
						velocity.X += 0.02f;
					}
				}
				else
				{
					velocity.X *= 0.9f;
					if ((double)velocity.X >= -0.08 && (double)velocity.X <= 0.08)
					{
						velocity.X = 0f;
					}
				}
				if (flag5 || flag6)
				{
					int num268 = (int)(base.position.X + (float)(base.width / 2)) / 16;
					int j2 = (int)(base.position.Y + (float)(base.width / 2)) / 16;
					if (flag5)
					{
						num268--;
					}
					if (flag6)
					{
						num268++;
					}
					num268 += (int)velocity.X;
					if (WorldGen.SolidTile(num268, j2))
					{
						flag8 = true;
					}
				}
				if (Main.player[owner].position.Y + (float)Main.player[owner].height > base.position.Y + (float)base.height)
				{
					flag7 = true;
				}
				if (velocity.Y == 0f)
				{
					if (!flag7 && (velocity.X < 0f || velocity.X > 0f))
					{
						int num269 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int j3 = (int)(base.position.Y + (float)(base.height / 2)) / 16 + 1;
						if (flag5)
						{
							num269--;
						}
						if (flag6)
						{
							num269++;
						}
						if (!WorldGen.SolidTile(num269, j3))
						{
							flag8 = true;
						}
					}
					if (flag8)
					{
						int i3 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int j4 = (int)(base.position.Y + (float)(base.height / 2)) / 16 + 1;
						if (WorldGen.SolidTile(i3, j4))
						{
							velocity.Y = -9.1f;
						}
					}
				}
				if (velocity.X > 6.5f)
				{
					velocity.X = 6.5f;
				}
				if (velocity.X < -6.5f)
				{
					velocity.X = -6.5f;
				}
				if ((double)velocity.X > 0.07 && flag6)
				{
					direction = 1;
				}
				if ((double)velocity.X < -0.07 && flag5)
				{
					direction = -1;
				}
				if (direction == -1)
				{
					spriteDirection = 1;
				}
				if (direction == 1)
				{
					spriteDirection = -1;
				}
				if (velocity.Y == 0f)
				{
					if (velocity.X == 0f)
					{
						frame = 0;
						frameCounter = 0;
					}
					else if ((double)velocity.X < -0.8 || (double)velocity.X > 0.8)
					{
						frameCounter += (int)Math.Abs(velocity.X);
						frameCounter++;
						if (frameCounter > 6)
						{
							frame++;
							frameCounter = 0;
						}
						if (frame >= 7)
						{
							frame = 0;
						}
					}
					else
					{
						frame = 0;
						frameCounter = 0;
					}
				}
				else if (velocity.Y < 0f)
				{
					frameCounter = 0;
					frame = 4;
				}
				else if (velocity.Y > 0f)
				{
					frameCounter = 0;
					frame = 6;
				}
				velocity.Y += 0.4f;
				if (velocity.Y > 10f)
				{
					velocity.Y = 10f;
				}
			}
		}

		public void Kill()
		{
			RunMethod("PreKill");
			int num = type;
			int value = type;
			if (name != null && Config.projDefs.killPretendType.TryGetValue(name, out value))
			{
				type = value;
			}
			KillReal();
			if (type == value)
			{
				type = num;
			}
			RunMethod("PostKill");
		}

		public void KillReal()
		{
			if (!active)
			{
				return;
			}
			timeLeft = 0;
			if (RunMethod("Kill"))
			{
				return;
			}
			if (type == 1 || type == 81 || type == 98)
			{
				Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
				for (int i = 0; i < 10; i++)
				{
					Vector2 position = new Vector2(base.position.X, base.position.Y);
					int width = base.width;
					int height = base.height;
					int num = 7;
					float speedX = 0f;
					float speedY = 0f;
					int num2 = 0;
					Dust.NewDust(position, width, height, num, speedX, speedY, num2);
				}
			}
			else
			{
				if (type == 111)
				{
					int num3 = Gore.NewGore(new Vector2(base.position.X - (float)(base.width / 2), base.position.Y - (float)(base.height / 2)), new Vector2(0f, 0f), Main.rand.Next(11, 14), scale);
					Gore gore = Main.gore[num3];
					gore.velocity *= 0.1f;
				}
				else if (type == 93)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int j = 0; j < 10; j++)
					{
						Vector2 position2 = base.position;
						int width2 = base.width;
						int height2 = base.height;
						int num4 = 57;
						float speedX2 = 0f;
						float speedY2 = 0f;
						int num5 = 100;
						int num6 = Dust.NewDust(position2, width2, height2, num4, speedX2, speedY2, num5, default(Color), 0.5f);
						Dust dust = Main.dust[num6];
						dust.velocity.X = dust.velocity.X * 2f;
						Dust dust2 = Main.dust[num6];
						dust2.velocity.Y = dust2.velocity.Y * 2f;
					}
				}
				else if (type == 99)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int k = 0; k < 30; k++)
					{
						Vector2 position3 = base.position;
						int width3 = base.width;
						int height3 = base.height;
						int num7 = 1;
						float speedX3 = 0f;
						float speedY3 = 0f;
						int num8 = 0;
						int num9 = Dust.NewDust(position3, width3, height3, num7, speedX3, speedY3, num8);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num9].scale *= 1.4f;
						}
						velocity *= 1.9f;
					}
				}
				else if (type == 91 || type == 92)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int l = 0; l < 10; l++)
					{
						Vector2 position4 = base.position;
						int width4 = base.width;
						int height4 = base.height;
						int num10 = 58;
						float speedX4 = velocity.X * 0.1f;
						float speedY4 = velocity.Y * 0.1f;
						int num11 = 150;
						Dust.NewDust(position4, width4, height4, num10, speedX4, speedY4, num11, default(Color), 1.2f);
					}
					for (int m = 0; m < 3; m++)
					{
						Gore.NewGore(base.position, new Vector2(velocity.X * 0.05f, velocity.Y * 0.05f), Main.rand.Next(16, 18));
					}
					if (type == 12 && damage < 500)
					{
						for (int n = 0; n < 10; n++)
						{
							Vector2 position5 = base.position;
							int width5 = base.width;
							int height5 = base.height;
							int num12 = 57;
							float speedX5 = velocity.X * 0.1f;
							float speedY5 = velocity.Y * 0.1f;
							int num13 = 150;
							Dust.NewDust(position5, width5, height5, num12, speedX5, speedY5, num13, default(Color), 1.2f);
						}
						for (int num14 = 0; num14 < 3; num14++)
						{
							Gore.NewGore(base.position, new Vector2(velocity.X * 0.05f, velocity.Y * 0.05f), Main.rand.Next(16, 18));
						}
					}
					if ((type == 91 || (type == 92 && ai[0] > 0f)) && owner == Main.myPlayer)
					{
						float x = base.position.X + (float)Main.rand.Next(-400, 400);
						float y = base.position.Y - (float)Main.rand.Next(600, 900);
						Vector2 vector = new Vector2(x, y);
						float num15 = base.position.X + (float)(base.width / 2) - vector.X;
						float num16 = base.position.Y + (float)(base.height / 2) - vector.Y;
						int num17 = 22;
						float num18 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
						num18 = (float)num17 / num18;
						num15 *= num18;
						num16 *= num18;
						int num19 = damage;
						if (type == 91)
						{
							num19 = (int)((float)num19 * 0.5f);
						}
						int num20 = NewProjectile(x, y, num15, num16, 92, num19, knockBack, owner);
						if (type == 91)
						{
							Main.projectile[num20].ai[1] = base.position.Y;
							Main.projectile[num20].ai[0] = 1f;
						}
						else
						{
							Main.projectile[num20].ai[1] = base.position.Y;
						}
					}
				}
				else if (type == 89)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num21 = 0; num21 < 5; num21++)
					{
						Vector2 position6 = new Vector2(base.position.X, base.position.Y);
						int width6 = base.width;
						int height6 = base.height;
						int num22 = 68;
						float speedX6 = 0f;
						float speedY6 = 0f;
						int num23 = 0;
						int num24 = Dust.NewDust(position6, width6, height6, num22, speedX6, speedY6, num23);
						Main.dust[num24].noGravity = true;
						Dust dust3 = Main.dust[num24];
						dust3.velocity *= 1.5f;
						Main.dust[num24].scale *= 0.9f;
					}
					if (type == 89 && owner == Main.myPlayer)
					{
						for (int num25 = 0; num25 < 3; num25++)
						{
							float num26 = (0f - velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
							float num27 = (0f - velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
							NewProjectile(base.position.X + num26, base.position.Y + num27, num26, num27, 90, (int)((double)damage * 0.6), 0f, owner);
						}
					}
				}
				else if (type == 80)
				{
					if (ai[0] >= 0f)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 27);
						for (int num28 = 0; num28 < 10; num28++)
						{
							Vector2 position7 = new Vector2(base.position.X, base.position.Y);
							int width7 = base.width;
							int height7 = base.height;
							int num29 = 67;
							float speedX7 = 0f;
							float speedY7 = 0f;
							int num30 = 0;
							Dust.NewDust(position7, width7, height7, num29, speedX7, speedY7, num30);
						}
					}
					int num31 = (int)base.position.X / 16;
					int num32 = (int)base.position.Y / 16;
					if (Main.tile[num31, num32] == null)
					{
						Main.tile[num31, num32] = new Tile();
					}
					if (Main.tile[num31, num32].type == 127 && Main.tile[num31, num32].active)
					{
						WorldGen.KillTile(num31, num32);
					}
				}
				else if (type == 76 || type == 77 || type == 78)
				{
					for (int num33 = 0; num33 < 5; num33++)
					{
						Vector2 position8 = base.position;
						int width8 = base.width;
						int height8 = base.height;
						int num34 = 27;
						float speedX8 = 0f;
						float speedY8 = 0f;
						int num35 = 80;
						int num36 = Dust.NewDust(position8, width8, height8, num34, speedX8, speedY8, num35, default(Color), 1.5f);
						Main.dust[num36].noGravity = true;
					}
				}
				else if (type == 55)
				{
					for (int num37 = 0; num37 < 5; num37++)
					{
						Vector2 position9 = new Vector2(base.position.X, base.position.Y);
						int width9 = base.width;
						int height9 = base.height;
						int num38 = 18;
						float speedX9 = 0f;
						float speedY9 = 0f;
						int num39 = 0;
						int num40 = Dust.NewDust(position9, width9, height9, num38, speedX9, speedY9, num39, default(Color), 1.5f);
						Main.dust[num40].noGravity = true;
					}
				}
				else if (type == 51)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num41 = 0; num41 < 5; num41++)
					{
						Vector2 position10 = new Vector2(base.position.X, base.position.Y);
						int width10 = base.width;
						int height10 = base.height;
						int num42 = 0;
						float speedX10 = 0f;
						float speedY10 = 0f;
						int num43 = 0;
						Dust.NewDust(position10, width10, height10, num42, speedX10, speedY10, num43, default(Color), 0.7f);
					}
				}
				else if (type == 2 || type == 82)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num44 = 0; num44 < 20; num44++)
					{
						Vector2 position11 = new Vector2(base.position.X, base.position.Y);
						int width11 = base.width;
						int height11 = base.height;
						int num45 = 6;
						float speedX11 = 0f;
						float speedY11 = 0f;
						int num46 = 100;
						Dust.NewDust(position11, width11, height11, num45, speedX11, speedY11, num46);
					}
				}
				else if (type == 103)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num47 = 0; num47 < 20; num47++)
					{
						Vector2 position12 = new Vector2(base.position.X, base.position.Y);
						int width12 = base.width;
						int height12 = base.height;
						int num48 = 75;
						float speedX12 = 0f;
						float speedY12 = 0f;
						int num49 = 100;
						int num50 = Dust.NewDust(position12, width12, height12, num48, speedX12, speedY12, num49);
						if (Main.rand.Next(2) == 0)
						{
							Main.dust[num50].scale *= 2.5f;
							Main.dust[num50].noGravity = true;
							Dust dust4 = Main.dust[num50];
							dust4.velocity *= 5f;
						}
					}
				}
				else if (type == 3 || type == 48 || type == 54)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num51 = 0; num51 < 10; num51++)
					{
						Vector2 position13 = new Vector2(base.position.X, base.position.Y);
						int width13 = base.width;
						int height13 = base.height;
						int num52 = 1;
						float speedX13 = velocity.X * 0.1f;
						float speedY13 = velocity.Y * 0.1f;
						int num53 = 0;
						Dust.NewDust(position13, width13, height13, num52, speedX13, speedY13, num53, default(Color), 0.75f);
					}
				}
				else if (type == 4)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num54 = 0; num54 < 10; num54++)
					{
						Vector2 position14 = new Vector2(base.position.X, base.position.Y);
						int width14 = base.width;
						int height14 = base.height;
						int num55 = 14;
						float speedX14 = 0f;
						float speedY14 = 0f;
						int num56 = 150;
						Dust.NewDust(position14, width14, height14, num55, speedX14, speedY14, num56, default(Color), 1.1f);
					}
				}
				else if (type == 5)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num57 = 0; num57 < 60; num57++)
					{
						int num58;
						switch (Main.rand.Next(3))
						{
						case 0:
							num58 = 15;
							break;
						case 1:
							num58 = 57;
							break;
						default:
							num58 = 58;
							break;
						}
						Vector2 position15 = base.position;
						int width15 = base.width;
						int height15 = base.height;
						int num59 = num58;
						float speedX15 = velocity.X * 0.5f;
						float speedY15 = velocity.Y * 0.5f;
						int num60 = 150;
						Dust.NewDust(position15, width15, height15, num59, speedX15, speedY15, num60, default(Color), 1.5f);
					}
				}
				else if (type == 9 || type == 12)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num61 = 0; num61 < 10; num61++)
					{
						Vector2 position16 = base.position;
						int width16 = base.width;
						int height16 = base.height;
						int num62 = 58;
						float speedX16 = velocity.X * 0.1f;
						float speedY16 = velocity.Y * 0.1f;
						int num63 = 150;
						Dust.NewDust(position16, width16, height16, num62, speedX16, speedY16, num63, default(Color), 1.2f);
					}
					for (int num64 = 0; num64 < 3; num64++)
					{
						Gore.NewGore(base.position, new Vector2(velocity.X * 0.05f, velocity.Y * 0.05f), Main.rand.Next(16, 18));
					}
					if (type == 12 && damage < 100)
					{
						for (int num65 = 0; num65 < 10; num65++)
						{
							Vector2 position17 = base.position;
							int width17 = base.width;
							int height17 = base.height;
							int num66 = 57;
							float speedX17 = velocity.X * 0.1f;
							float speedY17 = velocity.Y * 0.1f;
							int num67 = 150;
							Dust.NewDust(position17, width17, height17, num66, speedX17, speedY17, num67, default(Color), 1.2f);
						}
						for (int num68 = 0; num68 < 3; num68++)
						{
							Gore.NewGore(base.position, new Vector2(velocity.X * 0.05f, velocity.Y * 0.05f), Main.rand.Next(16, 18));
						}
					}
				}
				else if (type == 14 || type == 20 || type == 36 || type == 83 || type == 84 || type == 100 || type == 110)
				{
					Collision.HitTiles(base.position, velocity, base.width, base.height);
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
				}
				else if (type == 15 || type == 34)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num69 = 0; num69 < 20; num69++)
					{
						Vector2 position18 = new Vector2(base.position.X, base.position.Y);
						int width18 = base.width;
						int height18 = base.height;
						int num70 = 6;
						float speedX18 = (0f - velocity.X) * 0.2f;
						float speedY18 = (0f - velocity.Y) * 0.2f;
						int num71 = 100;
						int num72 = Dust.NewDust(position18, width18, height18, num70, speedX18, speedY18, num71, default(Color), 2f);
						Main.dust[num72].noGravity = true;
						Dust dust5 = Main.dust[num72];
						dust5.velocity *= 2f;
						Vector2 position19 = new Vector2(base.position.X, base.position.Y);
						int width19 = base.width;
						int height19 = base.height;
						int num73 = 6;
						float speedX19 = (0f - velocity.X) * 0.2f;
						float speedY19 = (0f - velocity.Y) * 0.2f;
						int num74 = 100;
						num72 = Dust.NewDust(position19, width19, height19, num73, speedX19, speedY19, num74);
						Dust dust6 = Main.dust[num72];
						dust6.velocity *= 2f;
					}
				}
				else if (type == 95 || type == 96)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num75 = 0; num75 < 20; num75++)
					{
						Vector2 position20 = new Vector2(base.position.X, base.position.Y);
						int width20 = base.width;
						int height20 = base.height;
						int num76 = 75;
						float speedX20 = (0f - velocity.X) * 0.2f;
						float speedY20 = (0f - velocity.Y) * 0.2f;
						int num77 = 100;
						int num78 = Dust.NewDust(position20, width20, height20, num76, speedX20, speedY20, num77, default(Color), 2f * scale);
						Main.dust[num78].noGravity = true;
						Dust dust7 = Main.dust[num78];
						dust7.velocity *= 2f;
						Vector2 position21 = new Vector2(base.position.X, base.position.Y);
						int width21 = base.width;
						int height21 = base.height;
						int num79 = 75;
						float speedX21 = (0f - velocity.X) * 0.2f;
						float speedY21 = (0f - velocity.Y) * 0.2f;
						int num80 = 100;
						num78 = Dust.NewDust(position21, width21, height21, num79, speedX21, speedY21, num80, default(Color), 1f * scale);
						Dust dust8 = Main.dust[num78];
						dust8.velocity *= 2f;
					}
				}
				else if (type == 79)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num81 = 0; num81 < 20; num81++)
					{
						int num82 = Dust.NewDust(new Vector2(base.position.X, base.position.Y), base.width, base.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2f);
						Main.dust[num82].noGravity = true;
						Dust dust9 = Main.dust[num82];
						dust9.velocity *= 4f;
					}
				}
				else if (type == 16)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num83 = 0; num83 < 20; num83++)
					{
						Vector2 position22 = new Vector2(base.position.X - velocity.X, base.position.Y - velocity.Y);
						int width22 = base.width;
						int height22 = base.height;
						int num84 = 15;
						float speedX22 = 0f;
						float speedY22 = 0f;
						int num85 = 100;
						int num86 = Dust.NewDust(position22, width22, height22, num84, speedX22, speedY22, num85, default(Color), 2f);
						Main.dust[num86].noGravity = true;
						Dust dust10 = Main.dust[num86];
						dust10.velocity *= 2f;
						Vector2 position23 = new Vector2(base.position.X - velocity.X, base.position.Y - velocity.Y);
						int width23 = base.width;
						int height23 = base.height;
						int num87 = 15;
						float speedX23 = 0f;
						float speedY23 = 0f;
						int num88 = 100;
						num86 = Dust.NewDust(position23, width23, height23, num87, speedX23, speedY23, num88);
					}
				}
				else if (type == 17)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num89 = 0; num89 < 5; num89++)
					{
						Vector2 position24 = new Vector2(base.position.X, base.position.Y);
						int width24 = base.width;
						int height24 = base.height;
						int num90 = 0;
						float speedX24 = 0f;
						float speedY24 = 0f;
						int num91 = 0;
						Dust.NewDust(position24, width24, height24, num90, speedX24, speedY24, num91);
					}
				}
				else if (type == 31 || type == 42)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num92 = 0; num92 < 5; num92++)
					{
						Vector2 position25 = new Vector2(base.position.X, base.position.Y);
						int width25 = base.width;
						int height25 = base.height;
						int num93 = 32;
						float speedX25 = 0f;
						float speedY25 = 0f;
						int num94 = 0;
						int num95 = Dust.NewDust(position25, width25, height25, num93, speedX25, speedY25, num94);
						Dust dust11 = Main.dust[num95];
						dust11.velocity *= 0.6f;
					}
				}
				else if (type == 109)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num96 = 0; num96 < 5; num96++)
					{
						Vector2 position26 = new Vector2(base.position.X, base.position.Y);
						int width26 = base.width;
						int height26 = base.height;
						int num97 = 51;
						float speedX26 = 0f;
						float speedY26 = 0f;
						int num98 = 0;
						int num99 = Dust.NewDust(position26, width26, height26, num97, speedX26, speedY26, num98, default(Color), 0.6f);
						Dust dust12 = Main.dust[num99];
						dust12.velocity *= 0.6f;
					}
				}
				else if (type == 39)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num100 = 0; num100 < 5; num100++)
					{
						Vector2 position27 = new Vector2(base.position.X, base.position.Y);
						int width27 = base.width;
						int height27 = base.height;
						int num101 = 38;
						float speedX27 = 0f;
						float speedY27 = 0f;
						int num102 = 0;
						int num103 = Dust.NewDust(position27, width27, height27, num101, speedX27, speedY27, num102);
						Dust dust13 = Main.dust[num103];
						dust13.velocity *= 0.6f;
					}
				}
				else if (type == 71)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num104 = 0; num104 < 5; num104++)
					{
						Vector2 position28 = new Vector2(base.position.X, base.position.Y);
						int width28 = base.width;
						int height28 = base.height;
						int num105 = 53;
						float speedX28 = 0f;
						float speedY28 = 0f;
						int num106 = 0;
						int num107 = Dust.NewDust(position28, width28, height28, num105, speedX28, speedY28, num106);
						Dust dust14 = Main.dust[num107];
						dust14.velocity *= 0.6f;
					}
				}
				else if (type == 40)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num108 = 0; num108 < 5; num108++)
					{
						Vector2 position29 = new Vector2(base.position.X, base.position.Y);
						int width29 = base.width;
						int height29 = base.height;
						int num109 = 36;
						float speedX29 = 0f;
						float speedY29 = 0f;
						int num110 = 0;
						int num111 = Dust.NewDust(position29, width29, height29, num109, speedX29, speedY29, num110);
						Dust dust15 = Main.dust[num111];
						dust15.velocity *= 0.6f;
					}
				}
				else if (type == 21)
				{
					Main.PlaySound(0, (int)base.position.X, (int)base.position.Y);
					for (int num112 = 0; num112 < 10; num112++)
					{
						Vector2 position30 = new Vector2(base.position.X, base.position.Y);
						int width30 = base.width;
						int height30 = base.height;
						int num113 = 26;
						float speedX30 = 0f;
						float speedY30 = 0f;
						int num114 = 0;
						Dust.NewDust(position30, width30, height30, num113, speedX30, speedY30, num114, default(Color), 0.8f);
					}
				}
				else if (type == 24)
				{
					for (int num115 = 0; num115 < 10; num115++)
					{
						Vector2 position31 = new Vector2(base.position.X, base.position.Y);
						int width31 = base.width;
						int height31 = base.height;
						int num116 = 1;
						float speedX31 = velocity.X * 0.1f;
						float speedY31 = velocity.Y * 0.1f;
						int num117 = 0;
						Dust.NewDust(position31, width31, height31, num116, speedX31, speedY31, num117, default(Color), 0.75f);
					}
				}
				else if (type == 27)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num118 = 0; num118 < 30; num118++)
					{
						Vector2 position32 = new Vector2(base.position.X, base.position.Y);
						int width32 = base.width;
						int height32 = base.height;
						int num119 = 29;
						float speedX32 = velocity.X * 0.1f;
						float speedY32 = velocity.Y * 0.1f;
						int num120 = 100;
						int num121 = Dust.NewDust(position32, width32, height32, num119, speedX32, speedY32, num120, default(Color), 3f);
						Main.dust[num121].noGravity = true;
						Vector2 position33 = new Vector2(base.position.X, base.position.Y);
						int width33 = base.width;
						int height33 = base.height;
						int num122 = 29;
						float speedX33 = velocity.X * 0.1f;
						float speedY33 = velocity.Y * 0.1f;
						int num123 = 100;
						Dust.NewDust(position33, width33, height33, num122, speedX33, speedY33, num123, default(Color), 2f);
					}
				}
				else if (type == 38)
				{
					for (int num124 = 0; num124 < 10; num124++)
					{
						Vector2 position34 = new Vector2(base.position.X, base.position.Y);
						int width34 = base.width;
						int height34 = base.height;
						int num125 = 42;
						float speedX34 = velocity.X * 0.1f;
						float speedY34 = velocity.Y * 0.1f;
						int num126 = 0;
						Dust.NewDust(position34, width34, height34, num125, speedX34, speedY34, num126);
					}
				}
				else if (type == 44 || type == 45)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 10);
					for (int num127 = 0; num127 < 30; num127++)
					{
						Vector2 position35 = new Vector2(base.position.X, base.position.Y);
						int width35 = base.width;
						int height35 = base.height;
						int num128 = 27;
						float x2 = velocity.X;
						float y2 = velocity.Y;
						int num129 = 100;
						int num130 = Dust.NewDust(position35, width35, height35, num128, x2, y2, num129, default(Color), 1.7f);
						Main.dust[num130].noGravity = true;
						Vector2 position36 = new Vector2(base.position.X, base.position.Y);
						int width36 = base.width;
						int height36 = base.height;
						int num131 = 27;
						float x3 = velocity.X;
						float y3 = velocity.Y;
						int num132 = 100;
						Dust.NewDust(position36, width36, height36, num131, x3, y3, num132);
					}
				}
				else if (type == 41)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 14);
					for (int num133 = 0; num133 < 10; num133++)
					{
						Vector2 position37 = new Vector2(base.position.X, base.position.Y);
						int width37 = base.width;
						int height37 = base.height;
						int num134 = 31;
						float speedX35 = 0f;
						float speedY35 = 0f;
						int num135 = 100;
						Dust.NewDust(position37, width37, height37, num134, speedX35, speedY35, num135, default(Color), 1.5f);
					}
					for (int num136 = 0; num136 < 5; num136++)
					{
						Vector2 position38 = new Vector2(base.position.X, base.position.Y);
						int width38 = base.width;
						int height38 = base.height;
						int num137 = 6;
						float speedX36 = 0f;
						float speedY36 = 0f;
						int num138 = 100;
						int num139 = Dust.NewDust(position38, width38, height38, num137, speedX36, speedY36, num138, default(Color), 2.5f);
						Main.dust[num139].noGravity = true;
						Dust dust16 = Main.dust[num139];
						dust16.velocity *= 3f;
						Vector2 position39 = new Vector2(base.position.X, base.position.Y);
						int width39 = base.width;
						int height39 = base.height;
						int num140 = 6;
						float speedX37 = 0f;
						float speedY37 = 0f;
						int num141 = 100;
						num139 = Dust.NewDust(position39, width39, height39, num140, speedX37, speedY37, num141, default(Color), 1.5f);
						Dust dust17 = Main.dust[num139];
						dust17.velocity *= 2f;
					}
					Vector2 position40 = new Vector2(base.position.X, base.position.Y);
					int num142 = Gore.NewGore(position40, default(Vector2), Main.rand.Next(61, 64));
					Gore gore2 = Main.gore[num142];
					gore2.velocity *= 0.4f;
					Gore gore3 = Main.gore[num142];
					gore3.velocity.X = gore3.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
					Gore gore4 = Main.gore[num142];
					gore4.velocity.Y = gore4.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
					Vector2 position41 = new Vector2(base.position.X, base.position.Y);
					num142 = Gore.NewGore(position41, default(Vector2), Main.rand.Next(61, 64));
					Gore gore5 = Main.gore[num142];
					gore5.velocity *= 0.4f;
					Gore gore6 = Main.gore[num142];
					gore6.velocity.X = gore6.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
					Gore gore7 = Main.gore[num142];
					gore7.velocity.Y = gore7.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
					if (owner == Main.myPlayer)
					{
						penetrate = -1;
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 64;
						base.height = 64;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
						Damage();
					}
				}
				else if (type == 28 || type == 30 || type == 37 || type == 75 || type == 102)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 14);
					base.position.X = base.position.X + (float)(base.width / 2);
					base.position.Y = base.position.Y + (float)(base.height / 2);
					base.width = 22;
					base.height = 22;
					base.position.X = base.position.X - (float)(base.width / 2);
					base.position.Y = base.position.Y - (float)(base.height / 2);
					for (int num143 = 0; num143 < 20; num143++)
					{
						Vector2 position42 = new Vector2(base.position.X, base.position.Y);
						int width40 = base.width;
						int height40 = base.height;
						int num144 = 31;
						float speedX38 = 0f;
						float speedY38 = 0f;
						int num145 = 100;
						int num146 = Dust.NewDust(position42, width40, height40, num144, speedX38, speedY38, num145, default(Color), 1.5f);
						Dust dust18 = Main.dust[num146];
						dust18.velocity *= 1.4f;
					}
					for (int num147 = 0; num147 < 10; num147++)
					{
						Vector2 position43 = new Vector2(base.position.X, base.position.Y);
						int width41 = base.width;
						int height41 = base.height;
						int num148 = 6;
						float speedX39 = 0f;
						float speedY39 = 0f;
						int num149 = 100;
						int num150 = Dust.NewDust(position43, width41, height41, num148, speedX39, speedY39, num149, default(Color), 2.5f);
						Main.dust[num150].noGravity = true;
						Dust dust19 = Main.dust[num150];
						dust19.velocity *= 5f;
						Vector2 position44 = new Vector2(base.position.X, base.position.Y);
						int width42 = base.width;
						int height42 = base.height;
						int num151 = 6;
						float speedX40 = 0f;
						float speedY40 = 0f;
						int num152 = 100;
						num150 = Dust.NewDust(position44, width42, height42, num151, speedX40, speedY40, num152, default(Color), 1.5f);
						Dust dust20 = Main.dust[num150];
						dust20.velocity *= 3f;
					}
					Vector2 position45 = new Vector2(base.position.X, base.position.Y);
					int num153 = Gore.NewGore(position45, default(Vector2), Main.rand.Next(61, 64));
					Gore gore8 = Main.gore[num153];
					gore8.velocity *= 0.4f;
					Gore gore9 = Main.gore[num153];
					gore9.velocity.X = gore9.velocity.X + 1f;
					Gore gore10 = Main.gore[num153];
					gore10.velocity.Y = gore10.velocity.Y + 1f;
					Vector2 position46 = new Vector2(base.position.X, base.position.Y);
					num153 = Gore.NewGore(position46, default(Vector2), Main.rand.Next(61, 64));
					Gore gore11 = Main.gore[num153];
					gore11.velocity *= 0.4f;
					Gore gore12 = Main.gore[num153];
					gore12.velocity.X = gore12.velocity.X - 1f;
					Gore gore13 = Main.gore[num153];
					gore13.velocity.Y = gore13.velocity.Y + 1f;
					Vector2 position47 = new Vector2(base.position.X, base.position.Y);
					num153 = Gore.NewGore(position47, default(Vector2), Main.rand.Next(61, 64));
					Gore gore14 = Main.gore[num153];
					gore14.velocity *= 0.4f;
					Gore gore15 = Main.gore[num153];
					gore15.velocity.X = gore15.velocity.X + 1f;
					Gore gore16 = Main.gore[num153];
					gore16.velocity.Y = gore16.velocity.Y - 1f;
					Vector2 position48 = new Vector2(base.position.X, base.position.Y);
					num153 = Gore.NewGore(position48, default(Vector2), Main.rand.Next(61, 64));
					Gore gore17 = Main.gore[num153];
					gore17.velocity *= 0.4f;
					Gore gore18 = Main.gore[num153];
					gore18.velocity.X = gore18.velocity.X - 1f;
					Gore gore19 = Main.gore[num153];
					gore19.velocity.Y = gore19.velocity.Y - 1f;
				}
				else if (type == 29 || type == 108)
				{
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 14);
					if (type == 29)
					{
						base.position.X = base.position.X + (float)(base.width / 2);
						base.position.Y = base.position.Y + (float)(base.height / 2);
						base.width = 200;
						base.height = 200;
						base.position.X = base.position.X - (float)(base.width / 2);
						base.position.Y = base.position.Y - (float)(base.height / 2);
					}
					for (int num154 = 0; num154 < 50; num154++)
					{
						Vector2 position49 = new Vector2(base.position.X, base.position.Y);
						int width43 = base.width;
						int height43 = base.height;
						int num155 = 31;
						float speedX41 = 0f;
						float speedY41 = 0f;
						int num156 = 100;
						int num157 = Dust.NewDust(position49, width43, height43, num155, speedX41, speedY41, num156, default(Color), 2f);
						Dust dust21 = Main.dust[num157];
						dust21.velocity *= 1.4f;
					}
					for (int num158 = 0; num158 < 80; num158++)
					{
						Vector2 position50 = new Vector2(base.position.X, base.position.Y);
						int width44 = base.width;
						int height44 = base.height;
						int num159 = 6;
						float speedX42 = 0f;
						float speedY42 = 0f;
						int num160 = 100;
						int num161 = Dust.NewDust(position50, width44, height44, num159, speedX42, speedY42, num160, default(Color), 3f);
						Main.dust[num161].noGravity = true;
						Dust dust22 = Main.dust[num161];
						dust22.velocity *= 5f;
						Vector2 position51 = new Vector2(base.position.X, base.position.Y);
						int width45 = base.width;
						int height45 = base.height;
						int num162 = 6;
						float speedX43 = 0f;
						float speedY43 = 0f;
						int num163 = 100;
						num161 = Dust.NewDust(position51, width45, height45, num162, speedX43, speedY43, num163, default(Color), 2f);
						Dust dust23 = Main.dust[num161];
						dust23.velocity *= 3f;
					}
					for (int num164 = 0; num164 < 2; num164++)
					{
						Vector2 position52 = new Vector2(base.position.X + (float)(base.width / 2) - 24f, base.position.Y + (float)(base.height / 2) - 24f);
						int num165 = Gore.NewGore(position52, default(Vector2), Main.rand.Next(61, 64));
						Main.gore[num165].scale = 1.5f;
						Gore gore20 = Main.gore[num165];
						gore20.velocity.X = gore20.velocity.X + 1.5f;
						Gore gore21 = Main.gore[num165];
						gore21.velocity.Y = gore21.velocity.Y + 1.5f;
						Vector2 position53 = new Vector2(base.position.X + (float)(base.width / 2) - 24f, base.position.Y + (float)(base.height / 2) - 24f);
						num165 = Gore.NewGore(position53, default(Vector2), Main.rand.Next(61, 64));
						Main.gore[num165].scale = 1.5f;
						Gore gore22 = Main.gore[num165];
						gore22.velocity.X = gore22.velocity.X - 1.5f;
						Gore gore23 = Main.gore[num165];
						gore23.velocity.Y = gore23.velocity.Y + 1.5f;
						Vector2 position54 = new Vector2(base.position.X + (float)(base.width / 2) - 24f, base.position.Y + (float)(base.height / 2) - 24f);
						num165 = Gore.NewGore(position54, default(Vector2), Main.rand.Next(61, 64));
						Main.gore[num165].scale = 1.5f;
						Gore gore24 = Main.gore[num165];
						gore24.velocity.X = gore24.velocity.X + 1.5f;
						Gore gore25 = Main.gore[num165];
						gore25.velocity.Y = gore25.velocity.Y - 1.5f;
						Vector2 position55 = new Vector2(base.position.X + (float)(base.width / 2) - 24f, base.position.Y + (float)(base.height / 2) - 24f);
						num165 = Gore.NewGore(position55, default(Vector2), Main.rand.Next(61, 64));
						Main.gore[num165].scale = 1.5f;
						Gore gore26 = Main.gore[num165];
						gore26.velocity.X = gore26.velocity.X - 1.5f;
						Gore gore27 = Main.gore[num165];
						gore27.velocity.Y = gore27.velocity.Y - 1.5f;
					}
					base.position.X = base.position.X + (float)(base.width / 2);
					base.position.Y = base.position.Y + (float)(base.height / 2);
					base.width = 10;
					base.height = 10;
					base.position.X = base.position.X - (float)(base.width / 2);
					base.position.Y = base.position.Y - (float)(base.height / 2);
				}
				else if (type == 69)
				{
					Main.PlaySound(13, (int)base.position.X, (int)base.position.Y);
					for (int num166 = 0; num166 < 5; num166++)
					{
						Vector2 position56 = new Vector2(base.position.X, base.position.Y);
						int width46 = base.width;
						int height46 = base.height;
						int num167 = 13;
						float speedX44 = 0f;
						float speedY44 = 0f;
						int num168 = 0;
						Dust.NewDust(position56, width46, height46, num167, speedX44, speedY44, num168);
					}
					for (int num169 = 0; num169 < 30; num169++)
					{
						Vector2 position57 = new Vector2(base.position.X, base.position.Y);
						int width47 = base.width;
						int height47 = base.height;
						int num170 = 33;
						float speedX45 = 0f;
						float speedY45 = -2f;
						int num171 = 0;
						int num172 = Dust.NewDust(position57, width47, height47, num170, speedX45, speedY45, num171, default(Color), 1.1f);
						Main.dust[num172].alpha = 100;
						Dust dust24 = Main.dust[num172];
						dust24.velocity.X = dust24.velocity.X * 1.5f;
						Dust dust25 = Main.dust[num172];
						dust25.velocity *= 3f;
					}
				}
				else if (type == 70)
				{
					Main.PlaySound(13, (int)base.position.X, (int)base.position.Y);
					for (int num173 = 0; num173 < 5; num173++)
					{
						Vector2 position58 = new Vector2(base.position.X, base.position.Y);
						int width48 = base.width;
						int height48 = base.height;
						int num174 = 13;
						float speedX46 = 0f;
						float speedY46 = 0f;
						int num175 = 0;
						Dust.NewDust(position58, width48, height48, num174, speedX46, speedY46, num175);
					}
					for (int num176 = 0; num176 < 30; num176++)
					{
						Vector2 position59 = new Vector2(base.position.X, base.position.Y);
						int width49 = base.width;
						int height49 = base.height;
						int num177 = 52;
						float speedX47 = 0f;
						float speedY47 = -2f;
						int num178 = 0;
						int num179 = Dust.NewDust(position59, width49, height49, num177, speedX47, speedY47, num178, default(Color), 1.1f);
						Main.dust[num179].alpha = 100;
						Dust dust26 = Main.dust[num179];
						dust26.velocity.X = dust26.velocity.X * 1.5f;
						Dust dust27 = Main.dust[num179];
						dust27.velocity *= 3f;
					}
				}
				if (owner == Main.myPlayer)
				{
					if (type == 28 || type == 29 || type == 37 || type == 75 || type == 108)
					{
						int num180 = 3;
						if (type == 29)
						{
							num180 = 7;
						}
						if (type == 108)
						{
							num180 = 10;
						}
						int num181 = (int)(base.position.X / 16f - (float)num180);
						int num182 = (int)(base.position.X / 16f + (float)num180);
						int num183 = (int)(base.position.Y / 16f - (float)num180);
						int num184 = (int)(base.position.Y / 16f + (float)num180);
						if (num181 < 0)
						{
							num181 = 0;
						}
						if (num182 > Main.maxTilesX)
						{
							num182 = Main.maxTilesX;
						}
						if (num183 < 0)
						{
							num183 = 0;
						}
						if (num184 > Main.maxTilesY)
						{
							num184 = Main.maxTilesY;
						}
						bool explodeWall = false;
						for (int num185 = num181; num185 <= num182; num185++)
						{
							for (int num186 = num183; num186 <= num184; num186++)
							{
								float num187 = Math.Abs((float)num185 - base.position.X / 16f);
								float num188 = Math.Abs((float)num186 - base.position.Y / 16f);
								double num189 = Math.Sqrt(num187 * num187 + num188 * num188);
								if (num189 < (double)num180 && Main.tile[num185, num186] != null && Main.tile[num185, num186].wall == 0)
								{
									explodeWall = true;
									break;
								}
							}
						}
						for (int num190 = num181; num190 <= num182; num190++)
						{
							for (int num191 = num183; num191 <= num184; num191++)
							{
								float num192 = Math.Abs((float)num190 - base.position.X / 16f);
								float num193 = Math.Abs((float)num191 - base.position.Y / 16f);
								double num194 = Math.Sqrt(num192 * num192 + num193 * num193);
								if (num194 < (double)num180)
								{
									ExplodeTile(num190, num191, type, explodeWall);
								}
							}
						}
					}
					if (Main.netMode != 0)
					{
						NetMessage.SendData(29, -1, -1, "", identity, owner);
					}
					int num195 = -1;
					if (aiStyle == 10)
					{
						int num196 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int num197 = (int)(base.position.Y + (float)(base.width / 2)) / 16;
						int num198 = 0;
						int num199 = 2;
						if (type == 109)
						{
							num198 = 147;
							num199 = 0;
						}
						if (type == 31)
						{
							num198 = 53;
							num199 = 0;
						}
						if (type == 42)
						{
							num198 = 53;
							num199 = 0;
						}
						if (type == 56)
						{
							num198 = 112;
							num199 = 0;
						}
						if (type == 65)
						{
							num198 = 112;
							num199 = 0;
						}
						if (type == 67)
						{
							num198 = 116;
							num199 = 0;
						}
						if (type == 68)
						{
							num198 = 116;
							num199 = 0;
						}
						if (type == 71)
						{
							num198 = 123;
							num199 = 0;
						}
						if (type == 39)
						{
							num198 = 59;
							num199 = 176;
						}
						if (type == 40)
						{
							num198 = 57;
							num199 = 172;
						}
						if (!Main.tile[num196, num197].active)
						{
							WorldGen.PlaceTile(num196, num197, num198, mute: false, forced: true);
							if (Main.tile[num196, num197].active && Main.tile[num196, num197].type == num198)
							{
								NetMessage.SendData(17, -1, -1, "", 1, num196, num197, num198);
							}
							else if (num199 > 0)
							{
								num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, num199);
							}
						}
						else if (num199 > 0)
						{
							num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, num199);
						}
					}
					if (type == 1 && Main.rand.Next(3) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 40);
					}
					if (type == 103 && Main.rand.Next(6) == 0)
					{
						num195 = ((Main.rand.Next(3) != 0) ? Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 40) : Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 545));
					}
					if (type == 2 && Main.rand.Next(3) == 0)
					{
						num195 = ((Main.rand.Next(3) != 0) ? Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 40) : Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 41));
					}
					if (type == 91 && Main.rand.Next(6) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 516);
					}
					if (type == 50 && Main.rand.Next(3) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 282);
					}
					if (type == 53 && Main.rand.Next(3) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 286);
					}
					if (type == 48 && Main.rand.Next(2) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 279);
					}
					if (type == 54 && Main.rand.Next(2) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 287);
					}
					if (type == 3 && Main.rand.Next(2) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 42);
					}
					if (type == 4 && Main.rand.Next(4) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 47);
					}
					if (type == 12 && damage > 100)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 75);
					}
					if (type == 69 || type == 70)
					{
						int num200 = (int)(base.position.X + (float)(base.width / 2)) / 16;
						int num201 = (int)(base.position.Y + (float)(base.height / 2)) / 16;
						for (int num202 = num200 - 4; num202 <= num200 + 4; num202++)
						{
							for (int num203 = num201 - 4; num203 <= num201 + 4; num203++)
							{
								if (Math.Abs(num202 - num200) + Math.Abs(num203 - num201) >= 6)
								{
									continue;
								}
								if (type == 69)
								{
									if (Main.tile[num202, num203].type == 2)
									{
										Main.tile[num202, num203].type = 109;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
									else if (Main.tile[num202, num203].type == 1)
									{
										Main.tile[num202, num203].type = 117;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
									else if (Main.tile[num202, num203].type == 53)
									{
										Main.tile[num202, num203].type = 116;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
									else if (Main.tile[num202, num203].type == 23)
									{
										Main.tile[num202, num203].type = 109;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
									else if (Main.tile[num202, num203].type == 25)
									{
										Main.tile[num202, num203].type = 117;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
									else if (Main.tile[num202, num203].type == 112)
									{
										Main.tile[num202, num203].type = 116;
										WorldGen.SquareTileFrame(num202, num203);
										NetMessage.SendTileSquare(-1, num202, num203, 1);
									}
								}
								else if (Main.tile[num202, num203].type == 2)
								{
									Main.tile[num202, num203].type = 23;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
								else if (Main.tile[num202, num203].type == 1)
								{
									Main.tile[num202, num203].type = 25;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
								else if (Main.tile[num202, num203].type == 53)
								{
									Main.tile[num202, num203].type = 112;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
								else if (Main.tile[num202, num203].type == 109)
								{
									Main.tile[num202, num203].type = 23;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
								else if (Main.tile[num202, num203].type == 117)
								{
									Main.tile[num202, num203].type = 25;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
								else if (Main.tile[num202, num203].type == 116)
								{
									Main.tile[num202, num203].type = 112;
									WorldGen.SquareTileFrame(num202, num203);
									NetMessage.SendTileSquare(-1, num202, num203, 1);
								}
							}
						}
					}
					if (type == 21 && Main.rand.Next(2) == 0)
					{
						num195 = Item.NewItem((int)base.position.X, (int)base.position.Y, base.width, base.height, 154);
					}
					if (Main.netMode == 1 && num195 >= 0)
					{
						NetMessage.SendData(21, -1, -1, "", num195);
					}
				}
			}
			active = false;
		}

		public static void ExplodeTile(int x, int y, int projtype, bool ExplodeWall = false)
		{
			bool flag = true;
			if (Main.tile[x, y] != null && Main.tile[x, y].active)
			{
				flag = true;
				if (Main.tileDungeon[Main.tile[x, y].type] || Main.tile[x, y].type == 21 || Main.tile[x, y].type == 26 || Main.tile[x, y].type == 107 || Main.tile[x, y].type == 108 || Main.tile[x, y].type == 111)
				{
					flag = false;
				}
				if (!Main.hardMode && Main.tile[x, y].type == 58)
				{
					flag = false;
				}
				if (Codable.RunTileMethod(false, new Vector2(x, y), Main.tile[x, y].type, "CanExplode", x, y, projtype, flag))
				{
					flag = (bool)Codable.customMethodReturn;
				}
				if (flag)
				{
					WorldGen.KillTile(x, y);
					if (!Main.tile[x, y].active && Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, "", 0, x, y);
					}
				}
			}
			if (!flag)
			{
				return;
			}
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 1; j <= y + 1; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].wall > 0 && ExplodeWall)
					{
						WorldGen.KillWall(i, j);
						if (Main.tile[i, j].wall == 0 && Main.netMode != 0)
						{
							NetMessage.SendData(17, -1, -1, "", 2, i, j);
						}
					}
				}
			}
		}

		public Color GetAlpha(Color newColor)
		{
			if (type == 34 || type == 15 || type == 93 || type == 94 || type == 95 || type == 96 || (type == 102 && alpha < 255))
			{
				return new Color(200, 200, 200, 25);
			}
			if (type == 83 || type == 88 || type == 89 || type == 90 || type == 100 || type == 104)
			{
				if (alpha < 200)
				{
					return new Color(255 - alpha, 255 - alpha, 255 - alpha, 0);
				}
				return new Color(0, 0, 0, 0);
			}
			if (type == 34 || type == 35 || type == 15 || type == 19 || type == 44 || type == 45)
			{
				return Color.White;
			}
			int discoR;
			int discoG;
			int discoB;
			if (type == 79)
			{
				discoR = Main.DiscoR;
				discoG = Main.DiscoG;
				discoB = Main.DiscoB;
				return default(Color);
			}
			if (type == 9 || type == 15 || type == 34 || type == 50 || type == 53 || type == 76 || type == 77 || type == 78 || type == 92 || type == 91)
			{
				discoR = newColor.R - alpha / 3;
				discoG = newColor.G - alpha / 3;
				discoB = newColor.B - alpha / 3;
			}
			else if (type == 16 || type == 18 || type == 44 || type == 45)
			{
				discoR = newColor.R;
				discoG = newColor.G;
				discoB = newColor.B;
			}
			else
			{
				if (type == 12 || type == 72 || type == 86 || type == 87)
				{
					return new Color(255, 255, 255, newColor.A - alpha);
				}
				discoR = newColor.R - alpha;
				discoG = newColor.G - alpha;
				discoB = newColor.B - alpha;
			}
			int num = newColor.A - alpha;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			return new Color(discoR, discoG, discoB, num);
		}

		public Color GetColor(Color newColor)
		{
			float value = color.R - (255 - newColor.R);
			float value2 = color.G - (255 - newColor.G);
			float value3 = color.B - (255 - newColor.B);
			float value4 = color.A - (255 - newColor.A);
			return new Color(MathHelper.Clamp(value, 0f, 255f), MathHelper.Clamp(value2, 0f, 255f), MathHelper.Clamp(value3, 0f, 255f), MathHelper.Clamp(value4, 0f, 255f));
		}
	}
}
