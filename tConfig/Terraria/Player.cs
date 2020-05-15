using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Terraria
{
	public class Player : Codable
	{
		public object[] buffCode = new object[10];

		public bool flapSound;

		public int wingTime;

		public int wings;

		public int wingFrame;

		public int wingFrameCounter;

		public bool male = true;

		public bool ghost;

		public int ghostFrame;

		public int ghostFrameCounter;

		public bool pvpDeath;

		public bool zoneDungeon;

		public bool zoneEvil;

		public bool zoneHoly;

		public bool zoneMeteor;

		public bool zoneJungle;

		public Dictionary<string, bool> zone = new Dictionary<string, bool>();

		public bool boneArmor;

		public float townNPCs;

		public Vector2 oldPosition;

		public Vector2 oldVelocity;

		public double headFrameCounter;

		public double bodyFrameCounter;

		public double legFrameCounter;

		public int netSkip;

		public int oldSelectItem;

		public bool immune;

		public int immuneTime;

		public int immuneAlphaDirection;

		public int immuneAlpha;

		public int team;

		public bool hbLocked;

		public static int nameLen = 20;

		public float maxRegenDelay;

		public string chatText = "";

		public int sign = -1;

		public int chatShowTime;

		public int reuseDelay;

		public float activeNPCs;

		public bool mouseInterface;

		public int noThrow;

		public int changeItem = -1;

		public int selectedItem;

		public Item[] armor = new Item[11];

		public int itemAnimation;

		public int itemAnimationMax;

		public int itemTime;

		public int toolTime;

		public float itemRotation;

		public int itemWidth;

		public int itemHeight;

		public Vector2 itemLocation;

		public float ghostFade;

		public float ghostDir = 1f;

		public int[] buffType = new int[10];

		public int[] buffTime = new int[10];

		public int heldProj = -1;

		public int breathCD;

		public int breathMax = 200;

		public int breath = 200;

		public bool socialShadow;

		public string setBonus = "";

		public Item[] inventory = new Item[49];

		public Item[] bank = new Item[Chest.maxItems];

		public Item[] bank2 = new Item[Chest.maxItems];

		public float headRotation;

		public float bodyRotation;

		public float legRotation;

		public Vector2 headPosition;

		public Vector2 bodyPosition;

		public Vector2 legPosition;

		public Vector2 headVelocity;

		public Vector2 bodyVelocity;

		public Vector2 legVelocity;

		public int nonTorch = -1;

		public static bool deadForGood = false;

		public bool dead;

		public int respawnTimer;

		public int attackCD;

		public int potionDelay;

		public byte difficulty;

		public int hitTile;

		public int hitTileX;

		public int hitTileY;

		public int jump;

		public int head = -1;

		public int body = -1;

		public int legs = -1;

		public Rectangle headFrame;

		public Rectangle bodyFrame;

		public Rectangle legFrame;

		public Rectangle hairFrame;

		public bool controlLeft;

		public bool controlRight;

		public bool controlUp;

		public bool controlDown;

		public bool controlJump;

		public bool controlUseItem;

		public bool controlUseTile;

		public bool controlThrow;

		public bool controlInv;

		public bool controlHook;

		public bool controlTorch;

		public bool releaseJump;

		public bool releaseUseItem;

		public bool releaseUseTile;

		public bool releaseInventory;

		public bool releaseHook;

		public bool releaseThrow;

		public bool releaseQuickMana;

		public bool releaseQuickHeal;

		public bool delayUseItem;

		public bool showItemIcon;

		public int showItemIcon2;

		public int whoAmi;

		public int runSoundDelay;

		public float shadow;

		public float manaCost = 1f;

		public bool fireWalk;

		public Vector2[] shadowPos = new Vector2[3];

		public bool ShadowTail;

		public bool ShadowAura;

		public int shadowCount;

		public bool channel;

		public int step = -1;

		public int statDefense;

		public int statAttack;

		public int statLifeMax = 100;

		public int statLifeMax2;

		public int statLife = 100;

		public int statMana;

		public int statManaMax;

		public int statManaMax2;

		public int lifeRegen;

		public int lifeRegenCount;

		public int lifeRegenTime;

		public int manaRegen;

		public int manaRegenCount;

		public int manaRegenDelay;

		public bool manaRegenBuff;

		public bool noKnockback;

		public bool spaceGun;

		public bool ammoCost80;

		public bool ammoCost75;

		public int stickyBreak;

		public bool lightOrb;

		public bool fairy;

		public bool bunny;

		public bool archery;

		public bool poisoned;

		public bool blind;

		public bool onFire;

		public bool onFire2;

		public bool noItems;

		public bool wereWolf;

		public bool wolfAcc;

		public bool rulerAcc;

		public bool bleed;

		public bool confused;

		public bool accMerman;

		public bool merman;

		public bool brokenArmor;

		public bool silence;

		public bool slow;

		public bool gross;

		public bool tongued;

		public bool kbGlove;

		public bool starCloak;

		public bool longInvince;

		public bool pStone;

		public bool manaFlower;

		public int meleeCrit = 4;

		public int rangedCrit = 4;

		public int magicCrit = 4;

		public float meleeDamage = 1f;

		public float rangedDamage = 1f;

		public float magicDamage = 1f;

		public float meleeSpeed = 1f;

		public float moveSpeed = 1f;

		public float pickSpeed = 1f;

		public float meleeDamageCrit = 2f;

		public float rangedDamageCrit = 2f;

		public float magicDamageCrit = 2f;

		public float baseGravity = 0.4f;

		public float maxGravity = 10f;

		public float baseSpeed = 3f;

		public float baseSpeedAcceleration = 0.08f;

		public float baseSlideFactor = 0.2f;

		public float baseMaxSpeed = 3f;

		public float maximumMaxSpeed = 1.4f;

		public int SpawnX = -1;

		public int SpawnY = -1;

		public int[] spX = new int[200];

		public int[] spY = new int[200];

		public string[] spN = new string[200];

		public int[] spI = new int[200];

		public static int tileRangeX = 5;

		public static int tileRangeY = 4;

		public static int itemGrabRange = 38;

		public static float itemGrabSpeed = 0.45f;

		public static float itemGrabSpeedMax = 4f;

		public static int tileTargetX;

		public static int tileTargetY;

		public static int jumpHeight = 15;

		public static float jumpSpeed = 5.01f;

		public bool adjWater;

		public bool oldAdjWater;

		public bool adjLava;

		public bool oldAdjLava;

		public ArrayHandler<bool> adjTile = new ArrayHandler<bool>(150);

		public ArrayHandler<bool> oldAdjTile = new ArrayHandler<bool>(150);

		public Color hairColor = new Color(215, 90, 55);

		public Color skinColor = new Color(255, 125, 90);

		public Color eyeColor = new Color(105, 90, 75);

		public Color shirtColor = new Color(175, 165, 140);

		public Color underShirtColor = new Color(160, 180, 215);

		public Color pantsColor = new Color(255, 230, 175);

		public Color shoeColor = new Color(160, 105, 60);

		public int hair;

		public bool hostile;

		public int accCompass;

		public int accWatch;

		public int accDepthMeter;

		public bool accDivingHelm;

		public bool accFlipper;

		public bool doubleJump;

		public bool jumpAgain;

		public bool spawnMax;

		public int blockRange;

		public int[] grappling = new int[20];

		public int grapCount;

		public int rocketTime;

		public int rocketTimeMax = 7;

		public int rocketDelay;

		public int rocketDelay2;

		public bool rocketRelease;

		public bool rocketFrame;

		public int rocketBoots;

		public bool canRocket;

		public bool jumpBoost;

		public bool noFallDmg;

		public int swimTime;

		public bool killGuide;

		public bool lavaImmune;

		public bool gills;

		public bool slowFall;

		public bool findTreasure;

		public bool invis;

		public bool detectCreature;

		public bool nightVision;

		public bool enemySpawns;

		public bool thorns;

		public bool waterWalk;

		public bool gravControl;

		public int chest = -1;

		public int chestX;

		public int chestY;

		public int talkNPC = -1;

		public int fallStart;

		public int slowCount;

		public int potionDelayTime = Item.potionDelay;

		public Rectangle PlayerRect;

		public static Color HealEffectColour = new Color(100, 255, 100, 255);

		public static Color ManaEffectColour = new Color(100, 100, 255, 255);

		public void CheckZones()
		{
			foreach (string key in Biome.Biomes.Keys)
			{
				zone[key] = Biome.Biomes[key].Check(this);
			}
			zoneEvil = zone["Corruption"];
			zoneHoly = zone["Hallow"];
			zoneMeteor = zone["Meteor"];
			zoneJungle = zone["Jungle"];
			zoneDungeon = zone["Dungeon"];
		}

		public void CheckNetPlayer()
		{
			bool flag = false;
			if (statLife != Main.clientPlayer.statLife || statLifeMax != Main.clientPlayer.statLifeMax)
			{
				NetMessage.SendData(16, -1, -1, "", Main.myPlayer);
				flag = true;
			}
			if (statMana != Main.clientPlayer.statMana || statManaMax != Main.clientPlayer.statManaMax)
			{
				NetMessage.SendData(42, -1, -1, "", Main.myPlayer);
				flag = true;
			}
			if (controlUp != Main.clientPlayer.controlUp)
			{
				flag = true;
			}
			else if (controlDown != Main.clientPlayer.controlDown)
			{
				flag = true;
			}
			else if (controlLeft != Main.clientPlayer.controlLeft)
			{
				flag = true;
			}
			else if (controlRight != Main.clientPlayer.controlRight)
			{
				flag = true;
			}
			else if (controlJump != Main.clientPlayer.controlJump)
			{
				flag = true;
			}
			else if (controlUseItem != Main.clientPlayer.controlUseItem)
			{
				flag = true;
			}
			else if (selectedItem != Main.clientPlayer.selectedItem)
			{
				flag = true;
			}
			if (flag)
			{
				NetMessage.SendData(13, -1, -1, "", Main.myPlayer);
			}
		}

		public void CheckKeyAndPadInput()
		{
			Keys[] pressedKeys = Main.keyState.GetPressedKeys();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < pressedKeys.Length; i++)
			{
				string a = pressedKeys[i].ToString();
				if (a == Main.cUp)
				{
					controlUp = true;
				}
				if (a == Main.cLeft)
				{
					controlLeft = true;
				}
				if (a == Main.cDown)
				{
					controlDown = true;
				}
				if (a == Main.cRight)
				{
					controlRight = true;
				}
				if (a == Main.cJump)
				{
					controlJump = true;
				}
				if (a == Main.cThrowItem)
				{
					controlThrow = true;
				}
				if (a == Main.cInv)
				{
					controlInv = true;
				}
				if (a == Main.cBuff)
				{
					QuickBuff();
				}
				if (a == Main.cHeal)
				{
					flag = true;
				}
				if (a == Main.cMana)
				{
					flag2 = true;
				}
				if (a == Main.cHook)
				{
					controlHook = true;
				}
				if (a == Main.cTorch)
				{
					controlTorch = true;
				}
			}
			if (Main.gamePad)
			{
				GamePadState state = GamePad.GetState(PlayerIndex.One);
				if (state.DPad.Up == ButtonState.Pressed)
				{
					controlUp = true;
				}
				if (state.DPad.Down == ButtonState.Pressed)
				{
					controlDown = true;
				}
				if (state.DPad.Left == ButtonState.Pressed)
				{
					controlLeft = true;
				}
				if (state.DPad.Right == ButtonState.Pressed)
				{
					controlRight = true;
				}
				if (state.Triggers.Left > 0f)
				{
					controlJump = true;
				}
				if (state.Triggers.Right > 0f)
				{
					controlUseItem = true;
				}
				Main.mouseX = (int)((float)Main.screenWidth * 0.5f + state.ThumbSticks.Right.X * (float)tileRangeX * 16f);
				Main.mouseY = (int)((float)Main.screenHeight * 0.5f - state.ThumbSticks.Right.Y * (float)tileRangeX * 16f);
				if (state.ThumbSticks.Right.X == 0f)
				{
					Main.mouseX = Main.screenWidth / 2 + direction * 2;
				}
			}
			if (flag)
			{
				if (releaseQuickHeal)
				{
					QuickHeal();
				}
				releaseQuickHeal = false;
			}
			else
			{
				releaseQuickHeal = true;
			}
			if (flag2)
			{
				if (releaseQuickMana)
				{
					QuickMana();
				}
				releaseQuickMana = false;
			}
			else
			{
				releaseQuickMana = true;
			}
			if (controlLeft && controlRight)
			{
				controlLeft = false;
				controlRight = false;
			}
		}

		public void PreCheckActive()
		{
			tileRangeX = 5;
			tileRangeY = 4;
			itemGrabRange = 38;
			itemGrabSpeed = 0.45f;
			itemGrabSpeedMax = 4f;
			maxGravity = 10f;
			baseGravity = 0.4f;
			jumpHeight = 15;
			jumpSpeed = 5.01f;
			if (wet)
			{
				if (merman)
				{
					baseGravity = 0.3f;
					maxGravity = 7f;
				}
				else
				{
					baseGravity = 0.2f;
					maxGravity = 5f;
					jumpHeight = 30;
					jumpSpeed = 6.01f;
				}
			}
			baseSpeed = 3f;
			baseSpeedAcceleration = 0.08f;
			baseSlideFactor = 0.2f;
			baseMaxSpeed = baseSpeed;
			maximumMaxSpeed = 1.4f;
			heldProj = -1;
			if (active)
			{
				maxRegenDelay = (1f - (float)statMana / (float)statManaMax2) * 60f * 4f + 45f;
				shadowCount++;
				ShadowTail = false;
				ShadowAura = false;
				if (shadowCount == 1)
				{
					shadowPos[2] = shadowPos[1];
				}
				else if (shadowCount == 2)
				{
					shadowPos[1] = shadowPos[0];
				}
				else if (shadowCount >= 3)
				{
					shadowCount = 0;
					shadowPos[0] = position;
				}
				if (runSoundDelay > 0)
				{
					runSoundDelay--;
				}
				if (attackCD > 0)
				{
					attackCD--;
				}
				if (chatShowTime > 0)
				{
					chatShowTime--;
				}
				if (potionDelay > 0)
				{
					potionDelay--;
				}
			}
		}

		public void CheckScrollItem()
		{
			int num = selectedItem;
			if (!Main.chatMode && selectedItem != 48)
			{
				if (Main.keyState.IsKeyDown(Keys.D1))
				{
					selectedItem = 0;
				}
				else if (Main.keyState.IsKeyDown(Keys.D2))
				{
					selectedItem = 1;
				}
				else if (Main.keyState.IsKeyDown(Keys.D3))
				{
					selectedItem = 2;
				}
				else if (Main.keyState.IsKeyDown(Keys.D4))
				{
					selectedItem = 3;
				}
				else if (Main.keyState.IsKeyDown(Keys.D5))
				{
					selectedItem = 4;
				}
				else if (Main.keyState.IsKeyDown(Keys.D6))
				{
					selectedItem = 5;
				}
				else if (Main.keyState.IsKeyDown(Keys.D7))
				{
					selectedItem = 6;
				}
				else if (Main.keyState.IsKeyDown(Keys.D8))
				{
					selectedItem = 7;
				}
				else if (Main.keyState.IsKeyDown(Keys.D9))
				{
					selectedItem = 8;
				}
				else if (Main.keyState.IsKeyDown(Keys.D0))
				{
					selectedItem = 9;
				}
			}
			if (num != selectedItem)
			{
				Main.PlaySound(12);
			}
			if (!Main.playerInventory)
			{
				int i;
				for (i = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120; i > 9; i -= 10)
				{
				}
				for (; i < 0; i += 10)
				{
				}
				selectedItem -= i;
				if (i != 0)
				{
					Main.PlaySound(12);
				}
				if (changeItem >= 0)
				{
					if (selectedItem != changeItem)
					{
						Main.PlaySound(12);
					}
					selectedItem = changeItem;
					changeItem = -1;
				}
				while (selectedItem > 9)
				{
					selectedItem -= 10;
				}
				while (selectedItem < 0)
				{
					selectedItem += 10;
				}
			}
			else
			{
				Main.focusRecipe += (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
				if (Main.focusRecipe > Main.numAvailableRecipes - 1)
				{
					Main.focusRecipe = Main.numAvailableRecipes - 1;
				}
				if (Main.focusRecipe < 0)
				{
					Main.focusRecipe = 0;
				}
			}
		}

		public void ResetEffects()
		{
			potionDelayTime = Item.potionDelay;
			statDefense = 0;
			accWatch = 0;
			accCompass = 0;
			accDepthMeter = 0;
			accDivingHelm = false;
			lifeRegen = 0;
			manaCost = 1f;
			meleeSpeed = 1f;
			meleeDamage = 1f;
			rangedDamage = 1f;
			magicDamage = 1f;
			moveSpeed = 1f;
			boneArmor = false;
			rocketBoots = 0;
			fireWalk = false;
			noKnockback = false;
			jumpBoost = false;
			noFallDmg = false;
			accFlipper = false;
			spawnMax = false;
			spaceGun = false;
			killGuide = false;
			lavaImmune = false;
			gills = false;
			slowFall = false;
			findTreasure = false;
			invis = false;
			nightVision = false;
			enemySpawns = false;
			thorns = false;
			waterWalk = false;
			detectCreature = false;
			gravControl = false;
			statManaMax2 = statManaMax;
			statLifeMax2 = statLifeMax;
			ammoCost80 = false;
			ammoCost75 = false;
			manaRegenBuff = false;
			meleeCrit = 4;
			rangedCrit = 4;
			magicCrit = 4;
			lightOrb = false;
			fairy = false;
			bunny = false;
			archery = false;
			poisoned = false;
			blind = false;
			onFire = false;
			onFire2 = false;
			noItems = false;
			blockRange = 0;
			pickSpeed = 1f;
			wereWolf = false;
			rulerAcc = false;
			bleed = false;
			confused = false;
			wings = 0;
			brokenArmor = false;
			silence = false;
			slow = false;
			gross = false;
			tongued = false;
			kbGlove = false;
			starCloak = false;
			longInvince = false;
			pStone = false;
			manaFlower = false;
			meleeDamageCrit = 2f;
			rangedDamageCrit = 2f;
			magicDamageCrit = 2f;
		}

		public void CheckTileReaction()
		{
			int num = 0;
			int num2 = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			int num3 = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (num2 >= 0 && num2 < Main.maxTilesX && num3 >= 0 && num3 < Main.maxTilesY)
			{
				if (position.X / 16f - (float)tileRangeX <= (float)num2 && (position.X + (float)width) / 16f + (float)tileRangeX - 1f >= (float)num2 && position.Y / 16f - (float)tileRangeY <= (float)num3 && (position.Y + (float)height) / 16f + (float)tileRangeY - 2f >= (float)num3)
				{
					if (Main.tile[num2, num3].active)
					{
						ushort type = Main.tile[num2, num3].type;
						num = (Main.tileHammer[type] ? 1 : ((!Main.tileAxe[type]) ? 3 : 2));
					}
					else if (Main.tile[num2, num3].liquid > 0 && wet)
					{
						num = 4;
					}
				}
			}
			else if (wet)
			{
				num = 4;
			}
			if (num == 0)
			{
				float num4 = Math.Abs((float)Main.mouseX + Main.screenPosition.X - (position.X + (float)width * 0.5f));
				float num5 = Math.Abs((float)Main.mouseY + Main.screenPosition.Y - (position.Y + (float)height * 0.5f)) * 1.3f;
				if (Math.Sqrt(num4 * num4 + num5 * num5) > 200.0)
				{
					num = 5;
				}
			}
			for (int i = 0; i < 40; i++)
			{
				int type2 = inventory[i].type;
				switch (num)
				{
				case 0:
					switch (type2)
					{
					case 8:
					case 427:
					case 428:
					case 429:
					case 430:
					case 431:
					case 432:
					case 433:
					case 523:
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					case 282:
					case 286:
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						break;
					}
					break;
				case 1:
					if (inventory[i].hammer > 0)
					{
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					}
					break;
				case 2:
					if (inventory[i].axe > 0)
					{
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					}
					break;
				case 3:
					if (inventory[i].pick > 0)
					{
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					}
					break;
				case 4:
					if (type2 == 282 || type2 == 286 || type2 == 523)
					{
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					}
					break;
				case 5:
					switch (type2)
					{
					case 8:
					case 427:
					case 428:
					case 429:
					case 430:
					case 431:
					case 432:
					case 433:
					case 523:
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						break;
					case 282:
					case 286:
						if (nonTorch == -1)
						{
							nonTorch = selectedItem;
						}
						selectedItem = i;
						return;
					}
					break;
				}
			}
		}

		public void CheckInput()
		{
			controlUp = false;
			controlLeft = false;
			controlDown = false;
			controlRight = false;
			controlJump = false;
			controlUseItem = false;
			controlUseTile = false;
			controlThrow = false;
			controlInv = false;
			controlHook = false;
			controlTorch = false;
			if (!Main.hasFocus)
			{
				return;
			}
			if (!Main.chatMode && !Main.editSign)
			{
				CheckKeyAndPadInput();
			}
			if (confused)
			{
				bool flag = controlLeft;
				controlLeft = controlRight;
				controlRight = flag;
				flag = controlUp;
				controlUp = controlRight;
				controlDown = flag;
			}
			if (Main.mouseLeft && !mouseInterface)
			{
				controlUseItem = true;
			}
			if (Main.mouseRight && !mouseInterface)
			{
				controlUseTile = true;
			}
			if (controlInv)
			{
				if (releaseInventory)
				{
					toggleInv();
				}
				releaseInventory = false;
			}
			else
			{
				releaseInventory = true;
			}
			if (delayUseItem)
			{
				if (!controlUseItem)
				{
					delayUseItem = false;
				}
				controlUseItem = false;
			}
			if (itemAnimation == 0 && itemTime == 0)
			{
				dropItemCheck();
				CheckScrollItem();
			}
		}

		public void UpdateBuffs()
		{
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] <= 0 || buffTime[i] <= 0)
				{
					continue;
				}
				if (whoAmi == Main.myPlayer && buffType[i] != 28)
				{
					buffTime[i]--;
				}
				if (Codable.RunSpecifiedMethod("Buff " + Main.buffName[buffType[i]], buffCode[i], "Effects", this, i, buffType[i], buffTime[i]))
				{
					continue;
				}
				switch (buffType[i])
				{
				case 1:
					lavaImmune = true;
					fireWalk = true;
					break;
				case 2:
					lifeRegen += 2;
					break;
				case 3:
					moveSpeed += 0.25f;
					break;
				case 4:
					gills = true;
					break;
				case 5:
					statDefense += 8;
					break;
				case 6:
					manaRegenBuff = true;
					break;
				case 7:
					magicDamage += 0.2f;
					break;
				case 8:
					slowFall = true;
					break;
				case 9:
					findTreasure = true;
					break;
				case 10:
					invis = true;
					break;
				case 11:
					Lighting.addLight((int)(position.X + (float)width * 0.5f) / 16, (int)(position.Y + (float)height * 0.5f) / 16, 0.8f, 0.95f, 1f);
					break;
				case 12:
					nightVision = true;
					break;
				case 13:
					enemySpawns = true;
					break;
				case 14:
					thorns = true;
					break;
				case 15:
					waterWalk = true;
					break;
				case 16:
					archery = true;
					break;
				case 17:
					detectCreature = true;
					break;
				case 18:
					gravControl = true;
					break;
				case 19:
				{
					lightOrb = true;
					bool flag3 = true;
					for (int l = 0; l < Main.projectile.Length; l++)
					{
						if (Main.projectile[l].active && Main.projectile[l].owner == whoAmi && Main.projectile[l].type == 18)
						{
							flag3 = false;
							break;
						}
					}
					if (flag3)
					{
						Projectile.NewProjectile(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f, 0f, 0f, 18, 0, 0f, whoAmi);
					}
					break;
				}
				case 20:
					poisoned = true;
					break;
				case 21:
					potionDelay = buffTime[i];
					break;
				case 22:
					blind = true;
					break;
				case 23:
					noItems = true;
					break;
				case 24:
					onFire = true;
					break;
				case 25:
					statDefense -= 4;
					meleeCrit += 2;
					meleeDamage += 0.1f;
					meleeSpeed += 0.1f;
					break;
				case 26:
					statDefense++;
					meleeCrit++;
					meleeDamage += 0.05f;
					meleeSpeed += 0.05f;
					magicCrit++;
					magicDamage += 0.05f;
					rangedCrit++;
					magicDamage += 0.05f;
					moveSpeed += 0.1f;
					break;
				case 27:
				{
					fairy = true;
					bool flag2 = true;
					for (int k = 0; k < Main.projectile.Length; k++)
					{
						if (Main.projectile[k].active && Main.projectile[k].owner == whoAmi && (Main.projectile[k].type == 72 || Main.projectile[k].type == 86 || Main.projectile[k].type == 87))
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						int type;
						switch (Main.rand.Next(3))
						{
						case 0:
							type = 72;
							break;
						case 1:
							type = 86;
							break;
						default:
							type = 87;
							break;
						}
						Projectile.NewProjectile(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f, 0f, 0f, type, 0, 0f, whoAmi);
					}
					break;
				}
				case 28:
					if (!Main.dayTime && Main.moonPhase == 0 && wolfAcc && !merman)
					{
						wereWolf = true;
						meleeCrit++;
						meleeDamage += 0.051f;
						meleeSpeed += 0.051f;
						statDefense++;
						moveSpeed += 0.05f;
					}
					else
					{
						DelBuff(i);
					}
					break;
				case 29:
					magicCrit += 2;
					magicDamage += 0.05f;
					statManaMax2 += 20;
					manaCost -= 0.02f;
					break;
				case 30:
					bleed = true;
					break;
				case 31:
					confused = true;
					break;
				case 32:
					slow = true;
					break;
				case 33:
					meleeDamage -= 0.051f;
					meleeSpeed -= 0.051f;
					statDefense -= 4;
					moveSpeed -= 0.1f;
					break;
				case 35:
					silence = true;
					break;
				case 36:
					brokenArmor = true;
					break;
				case 37:
					if (Main.wof >= 0 && Main.npc[Main.wof].type == 113)
					{
						gross = true;
						buffTime[i] = 10;
					}
					else
					{
						DelBuff(i);
					}
					break;
				case 38:
					buffTime[i] = 10;
					tongued = true;
					break;
				case 39:
					onFire2 = true;
					break;
				case 40:
				{
					buffTime[i] = 18000;
					bunny = true;
					bool flag = true;
					for (int j = 0; j < Main.projectile.Length; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == whoAmi && Main.projectile[j].type == 111)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						Projectile.NewProjectile(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f, 0f, 0f, 111, 0, 0f, whoAmi);
					}
					break;
				}
				}
			}
		}

		public void ApplyArmour()
		{
			for (int i = 0; i < 8; i++)
			{
				statDefense += armor[i].defense;
				lifeRegen += armor[i].lifeRegen;
				if (armor[i].prefix > 0 && armor[i].prefix < Prefix.prefixes.Count)
				{
					Prefix.prefixes[armor[i].prefix].Apply(this);
				}
				armor[i].Effects(this);
				switch (armor[i].type)
				{
				case 100:
				case 101:
				case 102:
					meleeSpeed += 0.07f;
					break;
				case 123:
				case 124:
				case 125:
					magicDamage += 0.05f;
					break;
				case 151:
				case 152:
				case 153:
					rangedDamage += 0.05f;
					break;
				case 111:
				case 228:
					statManaMax2 += 20;
					magicCrit += 3;
					break;
				case 229:
				case 230:
					statManaMax2 += 20;
					break;
				case 238:
					magicDamage += 0.15f;
					break;
				case 268:
					accDivingHelm = true;
					break;
				case 371:
					magicCrit += 9;
					statManaMax2 += 40;
					break;
				case 372:
					moveSpeed += 0.07f;
					meleeSpeed += 0.12f;
					break;
				case 373:
					rangedDamage += 0.1f;
					rangedCrit += 6;
					break;
				case 374:
					magicCrit += 3;
					meleeCrit += 3;
					rangedCrit += 3;
					break;
				case 375:
					moveSpeed += 0.1f;
					break;
				case 376:
					magicDamage += 0.15f;
					statManaMax2 += 60;
					break;
				case 377:
					meleeCrit += 5;
					meleeDamage += 0.1f;
					break;
				case 378:
					rangedDamage += 0.12f;
					rangedCrit += 7;
					break;
				case 379:
					rangedDamage += 0.05f;
					meleeDamage += 0.05f;
					magicDamage += 0.05f;
					break;
				case 380:
					magicCrit += 3;
					meleeCrit += 3;
					rangedCrit += 3;
					break;
				case 400:
					magicDamage += 0.11f;
					magicCrit += 11;
					statManaMax2 += 80;
					break;
				case 401:
					meleeCrit += 7;
					meleeDamage += 0.14f;
					break;
				case 402:
					rangedDamage += 0.14f;
					rangedCrit += 8;
					break;
				case 403:
					rangedDamage += 0.06f;
					meleeDamage += 0.06f;
					magicDamage += 0.06f;
					break;
				case 404:
					magicCrit += 4;
					meleeCrit += 4;
					rangedCrit += 4;
					moveSpeed += 0.05f;
					break;
				case 551:
					magicCrit += 7;
					meleeCrit += 7;
					rangedCrit += 7;
					break;
				case 552:
					rangedDamage += 0.07f;
					meleeDamage += 0.07f;
					magicDamage += 0.07f;
					moveSpeed += 0.08f;
					break;
				case 553:
					rangedDamage += 0.15f;
					rangedCrit += 8;
					break;
				case 558:
					magicDamage += 0.12f;
					magicCrit += 12;
					statManaMax2 += 100;
					break;
				case 559:
					meleeCrit += 10;
					meleeDamage += 0.1f;
					meleeSpeed += 0.1f;
					break;
				}
			}
			for (int j = 8; j < 11; j++)
			{
				armor[j].VanityEffects(this);
			}
			head = armor[0].headSlot;
			body = armor[1].bodySlot;
			legs = armor[2].legSlot;
			for (int k = 3; k < 8; k++)
			{
				switch (armor[k].type)
				{
				case 15:
					if (accWatch < 1)
					{
						accWatch = 1;
					}
					break;
				case 16:
					if (accWatch < 2)
					{
						accWatch = 2;
					}
					break;
				case 17:
					if (accWatch < 3)
					{
						accWatch = 3;
					}
					break;
				case 18:
					if (accDepthMeter < 1)
					{
						accDepthMeter = 1;
					}
					break;
				case 53:
					doubleJump = true;
					break;
				case 54:
					if (baseMaxSpeed < 6f)
					{
						baseMaxSpeed = 6f;
					}
					break;
				case 128:
					if (rocketBoots < 1)
					{
						rocketBoots = 1;
					}
					break;
				case 156:
					noKnockback = true;
					break;
				case 158:
					noFallDmg = true;
					break;
				case 159:
					jumpBoost = true;
					break;
				case 187:
					accFlipper = true;
					break;
				case 193:
					fireWalk = true;
					break;
				case 211:
					meleeSpeed += 0.12f;
					break;
				case 212:
					moveSpeed += 0.1f;
					break;
				case 223:
					manaCost -= 0.06f;
					break;
				case 267:
					killGuide = true;
					break;
				case 285:
					moveSpeed += 0.05f;
					break;
				case 393:
					accCompass = 1;
					break;
				case 394:
					accFlipper = true;
					accDivingHelm = true;
					break;
				case 395:
					accWatch = 3;
					accDepthMeter = 1;
					accCompass = 1;
					break;
				case 396:
					noFallDmg = true;
					fireWalk = true;
					break;
				case 397:
					noKnockback = true;
					fireWalk = true;
					break;
				case 399:
					jumpBoost = true;
					doubleJump = true;
					break;
				case 405:
					if (baseMaxSpeed < 6f)
					{
						baseMaxSpeed = 6f;
					}
					if (rocketBoots < 2)
					{
						rocketBoots = 2;
					}
					break;
				case 407:
					if (blockRange < 1)
					{
						blockRange = 1;
					}
					break;
				case 485:
					wolfAcc = true;
					break;
				case 486:
					rulerAcc = true;
					break;
				case 489:
					magicDamage += 0.15f;
					break;
				case 490:
					meleeDamage += 0.15f;
					break;
				case 491:
					rangedDamage += 0.15f;
					break;
				case 492:
					if (wings == 0)
					{
						wings = 1;
					}
					break;
				case 493:
					if (wings == 0)
					{
						wings = 2;
					}
					break;
				case 497:
					accMerman = true;
					break;
				case 532:
					starCloak = true;
					break;
				case 535:
					pStone = true;
					break;
				case 536:
					kbGlove = true;
					break;
				case 554:
					longInvince = true;
					break;
				case 555:
					manaFlower = true;
					manaCost -= 0.08f;
					break;
				case 576:
					if (Main.myPlayer == whoAmi && Main.rand.Next(18000) == 0 && Main.curMusic is SoundHandler.MusicVanilla && (!(Main.curMusic is SoundHandler.MusicVanilla) || ((SoundHandler.MusicVanilla)Main.curMusic).ID != 0))
					{
						int num = 0;
						switch (((SoundHandler.MusicVanilla)Main.curMusic).ID)
						{
						case 1:
							num = 0;
							break;
						case 2:
							num = 1;
							break;
						case 3:
							num = 2;
							break;
						case 4:
							num = 4;
							break;
						case 5:
							num = 5;
							break;
						case 7:
							num = 6;
							break;
						case 8:
							num = 7;
							break;
						case 9:
							num = 9;
							break;
						case 10:
							num = 8;
							break;
						case 11:
							num = 11;
							break;
						case 12:
							num = 10;
							break;
						case 13:
							num = 12;
							break;
						}
						armor[k].SetDefaults(num + 562);
					}
					break;
				}
				if (armor[k].type >= 562 && armor[k].type <= 574)
				{
					Main.musicBox2 = armor[k].type - 562;
				}
			}
		}

		public void ApplySetBonuses()
		{
			if ((head == 1 && body == 1 && legs == 1) || (head == 2 && body == 2 && legs == 2))
			{
				setBonus = Lang.setBonus(0);
				statDefense += 2;
			}
			else if ((head == 3 && body == 3 && legs == 3) || (head == 4 && body == 4 && legs == 4))
			{
				setBonus = Lang.setBonus(1);
				statDefense += 3;
			}
			else if (head == 5 && body == 5 && legs == 5)
			{
				setBonus = Lang.setBonus(2);
				moveSpeed += 0.15f;
			}
			else if (head == 6 && body == 6 && legs == 6)
			{
				setBonus = Lang.setBonus(3);
				spaceGun = true;
			}
			else if (head == 7 && body == 7 && legs == 7)
			{
				setBonus = Lang.setBonus(4);
				ammoCost80 = true;
			}
			else if (head == 8 && body == 8 && legs == 8)
			{
				setBonus = Lang.setBonus(5);
				manaCost -= 0.16f;
			}
			else if (head == 9 && body == 9 && legs == 9)
			{
				setBonus = Lang.setBonus(6);
				meleeDamage += 0.17f;
			}
			else if (head == 11 && body == 20 && legs == 19)
			{
				setBonus = Lang.setBonus(7);
				pickSpeed = 0.8f;
			}
			else if (body == 17 && legs == 16)
			{
				if (head == 29)
				{
					setBonus = Lang.setBonus(8);
					manaCost -= 0.14f;
				}
				else if (head == 30)
				{
					setBonus = Lang.setBonus(9);
					meleeSpeed += 0.15f;
				}
				else if (head == 31)
				{
					setBonus = Lang.setBonus(10);
					ammoCost80 = true;
				}
			}
			else if (body == 18 && legs == 17)
			{
				if (head == 32)
				{
					setBonus = Lang.setBonus(11);
					manaCost -= 0.17f;
				}
				else if (head == 33)
				{
					setBonus = Lang.setBonus(12);
					meleeCrit += 5;
				}
				else if (head == 34)
				{
					setBonus = Lang.setBonus(13);
					ammoCost80 = true;
				}
			}
			else if (body == 19 && legs == 18)
			{
				if (head == 35)
				{
					setBonus = Lang.setBonus(14);
					manaCost -= 0.19f;
				}
				else if (head == 36)
				{
					setBonus = Lang.setBonus(15);
					meleeSpeed += 0.18f;
					moveSpeed += 0.18f;
				}
				else if (head == 37)
				{
					setBonus = Lang.setBonus(16);
					ammoCost75 = true;
				}
			}
			else if (body == 24 && legs == 23)
			{
				if (head == 42)
				{
					setBonus = Lang.setBonus(17);
					manaCost -= 0.2f;
				}
				else if (head == 43)
				{
					setBonus = Lang.setBonus(18);
					meleeSpeed += 0.19f;
					moveSpeed += 0.19f;
				}
				else if (head == 41)
				{
					setBonus = Lang.setBonus(19);
					ammoCost75 = true;
				}
			}
		}

		public void CheckTileMouseOver(Tile T)
		{
			switch (T.type)
			{
			case 4:
			{
				noThrow = 2;
				showItemIcon = true;
				int num4 = T.frameY / 22;
				switch (num4)
				{
				case 0:
					showItemIcon2 = 8;
					break;
				case 8:
					showItemIcon2 = 523;
					break;
				default:
					showItemIcon2 = 426 + num4;
					break;
				}
				break;
			}
			case 10:
			case 11:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 25;
				break;
			case 13:
				noThrow = 2;
				showItemIcon = true;
				if (T.frameX == 72)
				{
					showItemIcon2 = 351;
				}
				else if (T.frameX == 54)
				{
					showItemIcon2 = 350;
				}
				else if (T.frameX == 18)
				{
					showItemIcon2 = 28;
				}
				else if (T.frameX == 36)
				{
					showItemIcon2 = 110;
				}
				else
				{
					showItemIcon2 = 31;
				}
				break;
			case 21:
				noThrow = 2;
				showItemIcon = true;
				if (T.frameX >= 216)
				{
					showItemIcon2 = 348;
				}
				else if (T.frameX >= 180)
				{
					showItemIcon2 = 343;
				}
				else if (T.frameX >= 144)
				{
					showItemIcon2 = 329;
				}
				else if (T.frameX >= 108)
				{
					showItemIcon2 = 328;
				}
				else if (T.frameX >= 72)
				{
					showItemIcon2 = 327;
				}
				else if (T.frameX >= 36)
				{
					showItemIcon2 = 306;
				}
				else
				{
					showItemIcon2 = 48;
				}
				break;
			case 29:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 87;
				break;
			case 33:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 105;
				break;
			case 49:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 148;
				break;
			case 50:
				noThrow = 2;
				if (T.frameX == 90)
				{
					showItemIcon = true;
					showItemIcon2 = 165;
				}
				break;
			case 55:
			case 85:
			{
				noThrow = 2;
				int num5 = tileTargetX - T.frameX / 18 % 2;
				int num6 = tileTargetY - T.frameY / 18;
				Main.signBubble = true;
				Main.signX = num5 * 16 + 16;
				Main.signY = num6 * 16;
				break;
			}
			case 79:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 224;
				break;
			case 97:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 346;
				break;
			case 104:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 359;
				break;
			case 125:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 487;
				break;
			case 128:
			{
				noThrow = 2;
				int num = tileTargetX;
				int num2 = tileTargetY;
				if (T.frameX % 100 % 36 == 18)
				{
					num--;
				}
				if (Main.tile[num, num2].frameX >= 100)
				{
					int i = Main.tile[num, num2].frameX / 100;
					showItemIcon = true;
					int num3 = Main.tile[num, num2].frameY / 18;
					if (num3 == 0)
					{
						showItemIcon2 = Item.headType[i];
					}
					if (num3 == 1)
					{
						showItemIcon2 = Item.bodyType[i];
					}
					if (num3 == 2)
					{
						showItemIcon2 = Item.legType[i];
					}
				}
				break;
			}
			case 132:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 513;
				break;
			case 136:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 538;
				break;
			case 139:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 562 + T.frameY / 36;
				break;
			case 144:
				noThrow = 2;
				showItemIcon = true;
				showItemIcon2 = 583 + T.frameX / 18;
				break;
			}
			if (T.type > 149)
			{
				Codable.RunTileMethod(false, new Vector2(tileTargetX, tileTargetY), T.type, "MouseOverTile", tileTargetX, tileTargetY, this);
			}
		}

		public void RocketDust()
		{
			bool flag = true;
			bool flag2 = true;
			if (Codable.RunPlayerMethodRef("RocketFrame", true, this, flag, flag2) && Codable.customMethodRefReturn != null)
			{
				flag = (bool)Codable.customMethodRefReturn[1];
				flag2 = (bool)Codable.customMethodRefReturn[2];
			}
			if (!flag)
			{
				return;
			}
			int num = height;
			if (base.gravDir == -1f)
			{
				num = 4;
			}
			rocketFrame = true;
			Vector2 position = default(Vector2);
			for (int i = 0; i < 2; i++)
			{
				if (!flag2)
				{
					break;
				}
				int type = 6;
				float scale = 2.5f;
				int alpha = 100;
				if (rocketBoots == 2)
				{
					type = 16;
					scale = 1.5f;
					alpha = 20;
				}
				else if (socialShadow)
				{
					type = 27;
					scale = 1.5f;
				}
				position.X = base.position.X - 4f;
				if (i == 1)
				{
					position.X += width;
				}
				position.Y = base.position.Y + (float)num - 10f;
				float speedX = (float)(i * 4 - 2) - velocity.X * 0.3f;
				float speedY = 2f * base.gravDir - velocity.Y * 0.3f;
				int num2 = Dust.NewDust(position, 8, 8, type, speedX, speedY, alpha, Color.Transparent, scale);
				if (rocketBoots == 1)
				{
					Main.dust[num2].noGravity = true;
				}
				else if (rocketBoots == 2)
				{
					Main.dust[num2].velocity *= 0.1f;
				}
			}
			if (rocketDelay == 0)
			{
				releaseJump = true;
			}
			rocketDelay--;
			velocity.Y -= 0.1f * base.gravDir;
			if (base.gravDir == 1f)
			{
				if (velocity.Y > 0f)
				{
					velocity.Y -= 0.5f;
				}
				else if (velocity.Y > (0f - jumpSpeed) * 0.5f)
				{
					velocity.Y -= 0.1f;
				}
				if (velocity.Y < (0f - jumpSpeed) * 1.5f)
				{
					velocity.Y = (0f - jumpSpeed) * 1.5f;
				}
			}
			else
			{
				if (velocity.Y < 0f)
				{
					velocity.Y += 0.5f;
				}
				else if (velocity.Y < jumpSpeed * 0.5f)
				{
					velocity.Y += 0.1f;
				}
				if (velocity.Y > jumpSpeed * 1.5f)
				{
					velocity.Y = jumpSpeed * 1.5f;
				}
			}
		}

		public void CheckRemoveTongued()
		{
			if (!tongued)
			{
				return;
			}
			bool flag = false;
			if (Main.wof >= 0)
			{
				NPC nPC = Main.npc[Main.wof];
				float num = nPC.position.X + (float)nPC.width * 0.5f + (float)(nPC.direction * 200);
				float num2 = nPC.position.Y + (float)nPC.height * 0.5f;
				Vector2 vector = default(Vector2);
				vector.X = position.X + (float)width * 0.5f;
				vector.Y = position.Y + (float)height * 0.5f;
				float num3 = num - vector.X;
				float num4 = num2 - vector.Y;
				float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
				float num6;
				if (num5 > 11f)
				{
					num6 = 11f / num5;
				}
				else
				{
					num6 = 1f;
					flag = true;
				}
				velocity.X = num3 * num6;
				velocity.Y = num4 * num6;
			}
			else
			{
				flag = true;
			}
			if (!flag)
			{
				return;
			}
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] == 38)
				{
					DelBuff(i);
				}
			}
		}

		public void CheckTongued()
		{
			NPC nPC = Main.npc[Main.wof];
			float num = nPC.position.X + 40f;
			if (nPC.direction > 0)
			{
				num -= 96f;
			}
			if (position.X + (float)width > num && position.X < num + 140f && gross)
			{
				noKnockback = false;
				Hurt(50, nPC.direction);
			}
			if (!gross && (double)position.Y > (Main.hellLayer - 50.0) * 16.0 && position.X > num - 1920f && position.X < num + 1920f)
			{
				AddBuff(37, 10);
				Main.PlaySound(4, (int)nPC.position.X, (int)nPC.position.Y, 10);
			}
			if (gross)
			{
				if ((double)position.Y < Main.hellLayer * 16.0)
				{
					AddBuff(38, 10);
				}
				if (nPC.direction < 0)
				{
					if (position.X + (float)width * 0.5f > nPC.position.X + (float)nPC.width * 0.5f + 40f)
					{
						AddBuff(38, 10);
					}
				}
				else if (position.X + (float)width * 0.5f < nPC.position.X + (float)nPC.width * 0.5f - 40f)
				{
					AddBuff(38, 10);
				}
			}
			if (!tongued)
			{
				return;
			}
			controlHook = false;
			controlUseItem = false;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].aiStyle == 7)
				{
					Main.projectile[i].Kill();
				}
			}
			Vector2 vector = default(Vector2);
			vector.X = position.X + (float)width * 0.5f;
			vector.Y = position.Y + (float)height * 0.5f;
			float num2 = nPC.position.X + (float)nPC.width * 0.5f - vector.X;
			float num3 = nPC.position.Y + (float)nPC.height * 0.5f - vector.Y;
			float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
			if (num4 > 3000f)
			{
				KillMe(1000.0, 0, pvp: false, " tried to escape.");
			}
			else if (nPC.position.X < 608f || nPC.position.X > (float)((Main.maxTilesX - 38) * 16))
			{
				KillMe(1000.0, 0, pvp: false, " was licked.");
			}
		}

		public void UpdatePlayerRect()
		{
			PlayerRect.X = (int)position.X;
			PlayerRect.Y = (int)position.Y;
			PlayerRect.Width = width;
			PlayerRect.Height = height;
		}

		public void CheckOpenedChest()
		{
			if (chest == -1)
			{
				return;
			}
			int num = (int)((position.X + (float)width * 0.5f) / 16f);
			int num2 = (int)((position.Y + (float)height * 0.5f) / 16f);
			if (num < chestX - 5 || num > chestX + 6 || num2 < chestY - 4 || num2 > chestY + 5)
			{
				if (chest != -1)
				{
					Main.PlaySound(11);
				}
				chest = -1;
			}
			if (!Main.tile[chestX, chestY].active)
			{
				Main.PlaySound(11);
				chest = -1;
			}
		}

		public void RegenLife()
		{
			if (poisoned)
			{
				lifeRegenTime = 0;
				lifeRegen = -4;
			}
			if (onFire || onFire2)
			{
				lifeRegenTime = 0;
				lifeRegen = -8;
			}
			lifeRegenTime++;
			if (bleed)
			{
				lifeRegenTime = 0;
			}
			float num = 0f;
			num = ((lifeRegenTime < 2400) ? ((float)(lifeRegenTime / 300)) : (6f + (float)((lifeRegenTime - 1800) / 600)));
			if (lifeRegenTime > 3600)
			{
				lifeRegenTime = 3600;
			}
			num = ((velocity.X != 0f && grappling[0] <= 0) ? (num * 0.5f) : (num * 1.25f));
			num = num * (float)statLifeMax2 / 400f * 0.85f + 0.15f;
			lifeRegen += (int)Math.Round(num);
			lifeRegenCount += lifeRegen;
			while (lifeRegenCount >= 120)
			{
				lifeRegenCount -= 120;
				if (statLife < statLifeMax2)
				{
					statLife++;
				}
				if (statLife > statLifeMax2)
				{
					statLife = statLifeMax2;
				}
			}
			while (lifeRegenCount <= -120)
			{
				lifeRegenCount += 120;
				statLife--;
				if (statLife <= 0 && whoAmi == Main.myPlayer)
				{
					if (poisoned)
					{
						KillMe(10.0, 0, pvp: false, " " + Lang.dt[0]);
					}
					else if (onFire)
					{
						KillMe(10.0, 0, pvp: false, " " + Lang.dt[1]);
					}
					else if (onFire2)
					{
						KillMe(10.0, 0, pvp: false, " " + Lang.dt[1]);
					}
				}
			}
		}

		public void RegenMana()
		{
			if (manaRegenDelay > 0 && !channel)
			{
				manaRegenDelay--;
				if ((velocity.X == 0f && velocity.Y == 0f) || grappling[0] >= 0 || manaRegenBuff)
				{
					manaRegenDelay--;
				}
			}
			if (manaRegenBuff && manaRegenDelay > 20)
			{
				manaRegenDelay = 20;
			}
			if (manaRegenDelay <= 0)
			{
				manaRegenDelay = 0;
				manaRegen = statManaMax2 / 7 + 1;
				if ((velocity.X == 0f && velocity.Y == 0f) || grappling[0] >= 0 || manaRegenBuff)
				{
					manaRegen += statManaMax2 / 2;
				}
				float num = 1f;
				if (!manaRegenBuff)
				{
					num = (float)statMana / (float)statManaMax2 * 0.8f + 0.2f;
				}
				manaRegen = (int)((float)manaRegen * num);
			}
			else
			{
				manaRegen = 0;
			}
			manaRegenCount += manaRegen;
			while (manaRegenCount >= 120)
			{
				bool flag = false;
				manaRegenCount -= 120;
				if (statMana < statManaMax2)
				{
					statMana++;
					flag = true;
				}
				if (statMana < statManaMax2)
				{
					continue;
				}
				if (whoAmi == Main.myPlayer && flag)
				{
					Main.PlaySound(25);
					for (int i = 0; i < 5; i++)
					{
						int num2 = Dust.NewDust(position, width, height, 45, 0f, 0f, 255, Color.Transparent, (float)Main.rand.Next(20, 26) * 0.1f);
						Main.dust[num2].noLight = true;
						Main.dust[num2].noGravity = true;
					}
				}
				statMana = statManaMax2;
			}
			if (manaRegenCount < 0)
			{
				manaRegenCount = 0;
			}
			if (statMana > statManaMax2)
			{
				statMana = statManaMax2;
			}
		}

		public void DirectionalMovement()
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			if (Codable.RunPlayerMethodRef("DirectionalMovement", true, this, flag, flag2, flag3) && Codable.customMethodRefReturn != null)
			{
				flag = (bool)Codable.customMethodRefReturn[1];
				flag2 = (bool)Codable.customMethodRefReturn[2];
				flag3 = (bool)Codable.customMethodRefReturn[3];
			}
			if (!flag)
			{
				return;
			}
			if (controlLeft && velocity.X > 0f - baseSpeed)
			{
				if (velocity.X > baseSlideFactor)
				{
					velocity.X -= baseSlideFactor;
				}
				velocity.X -= baseSpeedAcceleration;
				if (itemAnimation == 0 || inventory[selectedItem].useTurn)
				{
					direction = -1;
				}
			}
			else if (controlRight && velocity.X < baseSpeed)
			{
				if (velocity.X < 0f - baseSlideFactor)
				{
					velocity.X += baseSlideFactor;
				}
				velocity.X += baseSpeedAcceleration;
				if (itemAnimation == 0 || inventory[selectedItem].useTurn)
				{
					direction = 1;
				}
			}
			else if (controlLeft && velocity.X > 0f - baseMaxSpeed)
			{
				if (itemAnimation == 0 || inventory[selectedItem].useTurn)
				{
					direction = -1;
				}
				if (velocity.Y == 0f || wings > 0)
				{
					if (velocity.X > baseSlideFactor)
					{
						velocity.X -= baseSlideFactor;
					}
					velocity.X -= baseSpeedAcceleration * 0.2f;
				}
				if (velocity.X < (0f - (baseMaxSpeed + baseSpeed)) * 0.5f && velocity.Y == 0f)
				{
					int num = 0;
					if (base.gravDir == -1f)
					{
						num -= height;
					}
					if (runSoundDelay == 0 && velocity.Y == 0f && flag3)
					{
						Main.PlaySound(17, (int)base.position.X, (int)base.position.Y);
						runSoundDelay = 9;
					}
					if (flag2)
					{
						Vector2 position = default(Vector2);
						position.X = base.position.X - 4f;
						position.Y = base.position.Y + (float)height + (float)num;
						Dust.NewDust(position, width + 8, 4, 16, (0f - velocity.X) * 0.1f, velocity.Y * 0.1f, 50, Color.Transparent, 1.5f);
					}
				}
			}
			else if (controlRight && velocity.X < baseMaxSpeed)
			{
				if (itemAnimation == 0 || inventory[selectedItem].useTurn)
				{
					direction = 1;
				}
				if (velocity.Y == 0f || wings > 0)
				{
					if (velocity.X < 0f - baseSlideFactor)
					{
						velocity.X += baseSlideFactor;
					}
					velocity.X += baseSpeedAcceleration * 0.2f;
				}
				if (velocity.X > (baseMaxSpeed + baseSpeed) * 0.5f && velocity.Y == 0f)
				{
					int num2 = 0;
					if (base.gravDir == -1f)
					{
						num2 -= height;
					}
					if (runSoundDelay == 0 && velocity.Y == 0f && flag3)
					{
						Main.PlaySound(17, (int)base.position.X, (int)base.position.Y);
						runSoundDelay = 9;
					}
					if (flag2)
					{
						Vector2 position2 = default(Vector2);
						position2.X = base.position.X - 4f;
						position2.Y = base.position.Y + (float)height + (float)num2;
						Dust.NewDust(position2, width + 8, 4, 16, (0f - velocity.X) * 0.1f, velocity.Y * 0.1f, 50, Color.Transparent, 1.5f);
					}
				}
			}
			else if (velocity.Y == 0f)
			{
				if (velocity.X > baseSlideFactor)
				{
					velocity.X -= baseSlideFactor;
				}
				else if (velocity.X < 0f - baseSlideFactor)
				{
					velocity.X += baseSlideFactor;
				}
				else
				{
					velocity.X = 0f;
				}
			}
			else if (velocity.X > baseSlideFactor * 0.5f)
			{
				velocity.X -= baseSlideFactor * 0.5f;
			}
			else if (velocity.X < (0f - baseSlideFactor) * 0.5f)
			{
				velocity.X += baseSlideFactor * 0.5f;
			}
			else
			{
				velocity.X = 0f;
			}
			if (gravControl)
			{
				if (controlUp && base.gravDir == 1f)
				{
					base.gravDir = -1f;
					fallStart = (int)(base.position.Y / 16f);
					jump = 0;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
				}
				else if (controlDown && base.gravDir == -1f)
				{
					base.gravDir = 1f;
					fallStart = (int)(base.position.Y / 16f);
					jump = 0;
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, 8);
				}
			}
			else
			{
				base.gravDir = 1f;
			}
		}

		public void CheckJump()
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			if (Codable.RunPlayerMethodRef("CheckJump", true, this, flag, flag2, flag3) && Codable.customMethodRefReturn != null)
			{
				flag = (bool)Codable.customMethodRefReturn[1];
				flag2 = (bool)Codable.customMethodRefReturn[2];
				flag3 = (bool)Codable.customMethodRefReturn[3];
			}
			if (!flag)
			{
				return;
			}
			if (controlJump)
			{
				if (jump > 0)
				{
					if (velocity.Y == 0f)
					{
						if (merman)
						{
							jump = 10;
						}
						jump = 0;
					}
					else
					{
						velocity.Y = (0f - jumpSpeed) * base.gravDir;
						if (merman)
						{
							if (swimTime <= 10)
							{
								swimTime = 30;
							}
						}
						else
						{
							jump--;
						}
					}
				}
				else if ((velocity.Y == 0f || jumpAgain || (wet && accFlipper)) && releaseJump)
				{
					bool flag4 = false;
					if (wet && accFlipper)
					{
						if (swimTime == 0)
						{
							swimTime = 30;
						}
						flag4 = true;
					}
					jumpAgain = false;
					canRocket = false;
					rocketRelease = false;
					if (velocity.Y == 0f && doubleJump)
					{
						jumpAgain = true;
					}
					if (velocity.Y == 0f || flag4)
					{
						velocity.Y = (0f - jumpSpeed) * base.gravDir;
						jump = jumpHeight;
					}
					else
					{
						int num = height;
						if (base.gravDir == -1f)
						{
							num = 0;
						}
						if (flag3)
						{
							Main.PlaySound(16, (int)base.position.X, (int)base.position.Y);
						}
						velocity.Y = (0f - jumpSpeed) * base.gravDir;
						jump = jumpHeight / 2;
						if (flag2)
						{
							Vector2 position = default(Vector2);
							for (int i = 0; i < 10; i++)
							{
								position.X = base.position.X - 34f;
								position.Y = base.position.Y + (float)num - 16f;
								Dust.NewDust(position, 102, 32, 16, (0f - velocity.X) * 0.35f, (0f - velocity.Y) * 0.05f, 100, Color.Transparent, 1.5f);
							}
							Vector2 position2 = default(Vector2);
							position2.X = base.position.X + (float)width * 0.5f - 16f;
							position2.Y = base.position.Y + (float)num - 16f;
							int num2 = Gore.NewGore(position2, -velocity * 0.1f, Main.rand.Next(11, 14));
							Main.gore[num2].velocity.X -= velocity.X * 0.1f;
							Main.gore[num2].velocity.Y -= velocity.Y * 0.05f;
							position2.X = base.position.X - 36f;
							num2 = Gore.NewGore(position2, -velocity * 0.1f, Main.rand.Next(11, 14));
							Main.gore[num2].velocity.X -= velocity.X * 0.1f;
							Main.gore[num2].velocity.Y -= velocity.Y * 0.05f;
							position2.X = base.position.X + (float)width + 4f;
							num2 = Gore.NewGore(position2, -velocity * 0.1f, Main.rand.Next(11, 14));
							Main.gore[num2].velocity.X -= velocity.X * 0.1f;
							Main.gore[num2].velocity.Y -= velocity.Y * 0.05f;
						}
					}
				}
				releaseJump = false;
			}
			else
			{
				jump = 0;
				releaseJump = true;
				rocketRelease = true;
			}
			if (doubleJump && !jumpAgain && ((base.gravDir == 1f && velocity.Y < 0f) || (base.gravDir == -1f && velocity.Y > 0f)) && rocketBoots == 0 && !accFlipper && flag2)
			{
				int num3 = height;
				if (base.gravDir == -1f)
				{
					num3 = -6;
				}
				Vector2 position3 = default(Vector2);
				position3.X = base.position.X - 4f;
				position3.Y = base.position.Y + (float)num3;
				Dust.NewDust(position3, width + 8, 4, 16, (0f - velocity.X) * 0.35f, (0f - velocity.Y) * 0.05f, 100, Color.Transparent, 1.5f);
			}
		}

		public void CheckFlying()
		{
			bool flag = false;
			if (velocity.Y == 0f)
			{
				wingTime = 90;
			}
			if (wings > 0 && controlJump && wingTime > 0 && !jumpAgain && jump == 0 && velocity.Y != 0f)
			{
				flag = true;
			}
			if (flag)
			{
				velocity.Y -= 0.1f * base.gravDir;
				if (base.gravDir == 1f)
				{
					if (velocity.Y > 0f)
					{
						velocity.Y -= 0.5f;
					}
					else if (velocity.Y > (0f - jumpSpeed) * 0.5f)
					{
						velocity.Y -= 0.1f;
					}
					if (velocity.Y < (0f - jumpSpeed) * 1.5f)
					{
						velocity.Y = (0f - jumpSpeed) * 1.5f;
					}
				}
				else
				{
					if (velocity.Y < 0f)
					{
						velocity.Y += 0.5f;
					}
					else if (velocity.Y < jumpSpeed * 0.5f)
					{
						velocity.Y += 0.1f;
					}
					if (velocity.Y > jumpSpeed * 1.5f)
					{
						velocity.Y = jumpSpeed * 1.5f;
					}
				}
				wingTime--;
			}
			if (flag || jump > 0)
			{
				wingFrameCounter++;
				if (wingFrameCounter > 4)
				{
					wingFrame++;
					wingFrameCounter = 0;
					if (wingFrame >= 4)
					{
						wingFrame = 0;
					}
				}
			}
			else if (velocity.Y != 0f)
			{
				wingFrame = 1;
			}
			else
			{
				wingFrame = 0;
			}
			if (wings > 0 && rocketBoots > 0)
			{
				wingTime += rocketTime * 10;
				rocketTime = 0;
			}
			if (flag)
			{
				if (wingFrame == 3)
				{
					if (!flapSound)
					{
						Main.PlaySound(2, (int)position.X, (int)position.Y, 32);
					}
					flapSound = true;
				}
				else
				{
					flapSound = false;
				}
			}
			if (velocity.Y == 0f)
			{
				rocketTime = rocketTimeMax;
			}
			if ((wingTime == 0 || wings == 0) && rocketBoots > 0 && controlJump && rocketDelay == 0 && canRocket && rocketRelease && !jumpAgain)
			{
				if (rocketTime > 0)
				{
					rocketTime--;
					rocketDelay = 10;
					if (rocketDelay2 <= 0)
					{
						if (rocketBoots == 1)
						{
							Main.PlaySound(2, (int)position.X, (int)position.Y, 13);
							rocketDelay2 = 30;
						}
						else if (rocketBoots == 2)
						{
							Main.PlaySound(2, (int)position.X, (int)position.Y, 24);
							rocketDelay2 = 15;
						}
					}
				}
				else
				{
					canRocket = false;
				}
			}
			if (rocketDelay2 > 0)
			{
				rocketDelay2--;
			}
			if (rocketDelay == 0)
			{
				rocketFrame = false;
			}
			if (rocketDelay > 0)
			{
				RocketDust();
			}
			else
			{
				if (flag)
				{
					return;
				}
				if (slowFall && ((!controlDown && base.gravDir == 1f) || (!controlUp && base.gravDir == -1f)))
				{
					if ((controlUp && base.gravDir == 1f) || (controlDown && base.gravDir == -1f))
					{
						velocity.Y += baseGravity / 10f * base.gravDir;
					}
					else
					{
						velocity.Y += baseGravity / 3f * base.gravDir;
					}
				}
				else if (wings > 0 && controlJump && velocity.Y > 0f)
				{
					fallStart = (int)(position.Y / 16f);
					if (velocity.Y > 0f)
					{
						wingFrame = 2;
					}
					velocity.Y += baseGravity / 3f * base.gravDir;
					if (base.gravDir == 1f)
					{
						if (velocity.Y > maxGravity / 3f && !controlDown)
						{
							velocity.Y = maxGravity / 3f;
						}
					}
					else if (velocity.Y < (0f - maxGravity) / 3f && !controlUp)
					{
						velocity.Y = (0f - maxGravity) / 3f;
					}
				}
				else
				{
					velocity.Y += baseGravity * base.gravDir;
				}
			}
		}

		public void CheckPickupItems(int PID)
		{
			Rectangle rectangle = default(Rectangle);
			Rectangle value = default(Rectangle);
			for (int i = 0; i < Main.item.Length - 1; i++)
			{
				if (!Main.item[i].active || Main.item[i].noGrabDelay != 0 || Main.item[i].owner != PID)
				{
					continue;
				}
				rectangle.X = (int)position.X;
				rectangle.Y = (int)position.Y;
				rectangle.Width = width;
				rectangle.Height = height;
				value.X = (int)Main.item[i].position.X;
				value.Y = (int)Main.item[i].position.Y;
				value.Width = Main.item[i].width;
				value.Height = Main.item[i].height;
				if (rectangle.Intersects(value))
				{
					if (PID != Main.myPlayer || (inventory[selectedItem].type == 0 && itemAnimation > 0))
					{
						continue;
					}
					if (Main.item[i].type == 58)
					{
						Main.PlaySound(7, (int)position.X, (int)position.Y);
						statLife += 20;
						if (Main.myPlayer == whoAmi)
						{
							HealEffect(20);
						}
						if (statLife > statLifeMax2)
						{
							statLife = statLifeMax2;
						}
						Main.item[i] = new Item();
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", i);
						}
					}
					else if (Main.item[i].type == 184)
					{
						Main.PlaySound(7, (int)position.X, (int)position.Y);
						statMana += 100;
						if (Main.myPlayer == whoAmi)
						{
							ManaEffect(100);
						}
						if (statMana > statManaMax2)
						{
							statMana = statManaMax2;
						}
						Main.item[i] = new Item();
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", i);
						}
					}
					else if (Main.item[i].PlayerGrab != null)
					{
						Main.item[i].PlayerGrab(this, PID, i);
					}
					else
					{
						if (Main.item[i].OverrideItemGrab == null || !Main.item[i].OverrideItemGrab(this, PID, i))
						{
							Main.item[i] = GetItem(PID, Main.item[i]);
						}
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", i);
						}
					}
					continue;
				}
				rectangle.X = (int)position.X - itemGrabRange;
				rectangle.Y = (int)position.Y - itemGrabRange;
				rectangle.Width = width + itemGrabRange * 2;
				rectangle.Height = height + itemGrabRange * 2;
				if (!rectangle.Intersects(value) || !((Main.item[i].OverrideItemVacuum != null) ? Main.item[i].OverrideItemVacuum(this, PID, i) : ItemSpace(Main.item[i])))
				{
					continue;
				}
				Main.item[i].beingGrabbed = true;
				if (position.X + (float)width * 0.5f > Main.item[i].position.X + (float)Main.item[i].width * 0.5f)
				{
					if (Main.item[i].velocity.X < itemGrabSpeedMax + velocity.X)
					{
						Main.item[i].velocity.X += itemGrabSpeed;
					}
					if (Main.item[i].velocity.X < 0f)
					{
						Main.item[i].velocity.X += itemGrabSpeed * 0.75f;
					}
				}
				else
				{
					if (Main.item[i].velocity.X > 0f - itemGrabSpeedMax + velocity.X)
					{
						Main.item[i].velocity.X -= itemGrabSpeed;
					}
					if (Main.item[i].velocity.X > 0f)
					{
						Main.item[i].velocity.X -= itemGrabSpeed * 0.75f;
					}
				}
				if (position.Y + (float)height * 0.5f > Main.item[i].position.Y + (float)Main.item[i].height * 0.5f)
				{
					if (Main.item[i].velocity.Y < itemGrabSpeedMax)
					{
						Main.item[i].velocity.Y += itemGrabSpeed;
					}
					if (Main.item[i].velocity.Y < 0f)
					{
						Main.item[i].velocity.Y += itemGrabSpeed * 0.75f;
					}
				}
				else
				{
					if (Main.item[i].velocity.Y > 0f - itemGrabSpeedMax)
					{
						Main.item[i].velocity.Y -= itemGrabSpeed;
					}
					if (Main.item[i].velocity.Y > 0f)
					{
						Main.item[i].velocity.Y -= itemGrabSpeed * 0.75f;
					}
				}
			}
		}

		public void UseTile(Tile T)
		{
			Vector2 pos = default(Vector2);
			pos.X = tileTargetX;
			pos.Y = tileTargetY;
			if (Codable.RunTileMethod(false, pos, T.type, "UseTile", this, tileTargetX, tileTargetY))
			{
				return;
			}
			if (Config.tileDefs.doorToggle.ContainsKey(T.type))
			{
				if (Config.tileDefs.doorType[T.type] == 1)
				{
					Config.OpenCustomDoor(tileTargetX, tileTargetY, direction, Config.tileDefs.doorToggle[T.type]);
					NetMessage.SendData(19, -1, -1, "", 0, tileTargetX, tileTargetY, direction);
				}
				else if (Config.tileDefs.doorType[T.type] == 2 && Config.CloseCustomDoor(tileTargetX, tileTargetY, Config.tileDefs.doorToggle[T.type]))
				{
					NetMessage.SendData(19, -1, -1, "", 1, tileTargetX, tileTargetY, direction);
				}
			}
			else if (T.type == 132 || T.type == 136 || T.type == 144)
			{
				WorldGen.hitSwitch(tileTargetX, tileTargetY);
				NetMessage.SendData(59, -1, -1, "", tileTargetX, tileTargetY);
			}
			else if (T.type == 139)
			{
				Main.PlaySound(28, tileTargetX * 16, tileTargetY * 16, 0);
				WorldGen.SwitchMB(tileTargetX, tileTargetY);
			}
			else if (T.type == 128)
			{
				int num = tileTargetX;
				if (T.frameX % 100 % 36 == 18)
				{
					num--;
				}
				if (Main.tile[num, tileTargetY].frameX >= 100)
				{
					WorldGen.KillTile(num, tileTargetY, fail: true);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(17, -1, -1, "", 0, num, tileTargetY, 1f);
					}
				}
			}
			else if (T.type == 4 || T.type == 13 || T.type == 33 || T.type == 49 || (T.type == 50 && T.frameX == 90))
			{
				WorldGen.KillTile(tileTargetX, tileTargetY);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY);
				}
			}
			else if (T.type == 125)
			{
				AddBuff(29, 36000);
				Main.PlaySound(2, (int)position.X, (int)position.Y, 4);
			}
			else if (T.type == 79)
			{
				int num2 = tileTargetX;
				int y = tileTargetY + T.frameY / -18 + 2;
				num2 += T.frameX / -18;
				num2 = ((T.frameX < 72) ? (num2 + 2) : (num2 + 5));
				if (CheckSpawn(num2, y))
				{
					ChangeSpawn(num2, y);
					Main.NewText("Spawn point set!", byte.MaxValue, 240, 20);
				}
			}
			else if (T.type == 55 || T.type == 85)
			{
				bool flag = true;
				if (sign >= 0 && Sign.ReadSign(tileTargetX, tileTargetY) == sign)
				{
					sign = -1;
					Main.npcChatText = "";
					Main.editSign = false;
					Main.PlaySound(11);
					flag = false;
				}
				if (!flag)
				{
					return;
				}
				if (Main.netMode == 0)
				{
					talkNPC = -1;
					Main.playerInventory = false;
					Main.editSign = false;
					Main.PlaySound(10);
					sign = Sign.ReadSign(tileTargetX, tileTargetY);
					Main.npcChatText = Main.sign[sign].text;
				}
				else
				{
					int num3 = tileTargetX - T.frameX / 18 % 2;
					int num4 = tileTargetY - T.frameY / 18;
					if (Main.tile[num3, num4].type == 55 || Main.tile[num3, num4].type == 85)
					{
						NetMessage.SendData(46, -1, -1, "", num3, num4);
					}
				}
			}
			else if (T.type == 104)
			{
				string text = "AM";
				double num5 = Main.time;
				if (!Main.dayTime)
				{
					num5 += 54000.0;
				}
				num5 = num5 / 86400.0 * 24.0 - 19.5;
				if (num5 < 0.0)
				{
					num5 += 24.0;
				}
				if (num5 >= 12.0)
				{
					text = "PM";
				}
				int num6 = (int)num5;
				int num7 = (int)((num5 - (double)(int)num5) * 60.0);
				string arg = "";
				if (num7 < 10)
				{
					arg = "0";
				}
				arg += num7;
				if (num6 > 12)
				{
					num6 -= 12;
				}
				if (num6 == 0)
				{
					num6 = 12;
				}
				Main.NewText("Time: " + num6 + ":" + arg + " " + text, byte.MaxValue, 240, 20);
			}
			else if (T.type == 10)
			{
				WorldGen.OpenDoor(tileTargetX, tileTargetY, direction);
				NetMessage.SendData(19, -1, -1, "", 0, tileTargetX, tileTargetY, direction);
			}
			else if (T.type == 11)
			{
				if (WorldGen.CloseDoor(tileTargetX, tileTargetY))
				{
					NetMessage.SendData(19, -1, -1, "", 1, tileTargetX, tileTargetY, direction);
				}
			}
			else
			{
				if ((T.type != 21 && T.type != 29 && T.type != 97) || talkNPC != -1)
				{
					return;
				}
				int num8 = 0;
				int num9 = tileTargetX - T.frameX / 18 % 2;
				int num10 = tileTargetY - T.frameY / 18;
				if (T.type == 29)
				{
					num8 = 1;
				}
				else if (T.type == 97)
				{
					num8 = 2;
				}
				else if (T.frameX >= 216)
				{
					Main.chestText = "Trash Can";
				}
				else if (T.frameX >= 180)
				{
					Main.chestText = "Barrel";
				}
				else
				{
					Main.chestText = "Chest";
				}
				if (Main.netMode == 1 && num8 == 0 && (Main.tile[num9, num10].frameX < 72 || Main.tile[num9, num10].frameX > 106) && (Main.tile[num9, num10].frameX < 144 || Main.tile[num9, num10].frameX > 178))
				{
					if (num9 == chestX && num10 == chestY && chest != -1)
					{
						chest = -1;
						Main.PlaySound(11);
					}
					else
					{
						NetMessage.SendData(31, -1, -1, "", num9, num10);
					}
					return;
				}
				int num11 = -1;
				switch (num8)
				{
				case 1:
					num11 = -2;
					break;
				case 2:
					num11 = -3;
					break;
				default:
				{
					bool flag2 = false;
					if ((Main.tile[num9, num10].frameX >= 72 && Main.tile[num9, num10].frameX <= 106) || (Main.tile[num9, num10].frameX >= 144 && Main.tile[num9, num10].frameX <= 178))
					{
						int num12 = 327;
						if (Main.tile[num9, num10].frameX >= 144 && Main.tile[num9, num10].frameX <= 178)
						{
							num12 = 329;
						}
						flag2 = true;
						for (int i = 0; i < 48; i++)
						{
							if (inventory[i].type != num12 || inventory[i].stack <= 0)
							{
								continue;
							}
							if (num12 != 329)
							{
								inventory[i].stack--;
								if (inventory[i].stack <= 0)
								{
									inventory[i] = new Item();
								}
							}
							Chest.Unlock(num9, num10);
							if (Main.netMode == 1)
							{
								NetMessage.SendData(52, -1, -1, "", whoAmi, 1f, num9, num10);
							}
						}
					}
					if (!flag2)
					{
						num11 = Chest.FindChest(num9, num10);
					}
					break;
				}
				}
				if (num11 == -1)
				{
					return;
				}
				if (num11 == chest)
				{
					chest = -1;
					Main.PlaySound(11);
					return;
				}
				chest = num11;
				Main.playerInventory = true;
				chestX = num9;
				chestY = num10;
				if (num11 != chest && chest == -1)
				{
					Main.PlaySound(10);
				}
				else
				{
					Main.PlaySound(12);
				}
			}
		}

		public void CheckInteract()
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.X = (int)(position.X + (float)width * 0.5f - (float)(tileRangeX * 16));
			rectangle.Y = (int)(position.Y + (float)height * 0.5f - (float)(tileRangeY * 16));
			rectangle.Width = tileRangeX * 16 * 2;
			rectangle.Height = tileRangeY * 16 * 2;
			if (talkNPC >= 0)
			{
				Rectangle value = default(Rectangle);
				value.X = (int)Main.npc[talkNPC].position.X;
				value.Y = (int)Main.npc[talkNPC].position.Y;
				value.Width = Main.npc[talkNPC].width;
				value.Height = Main.npc[talkNPC].height;
				if (!Main.npc[talkNPC].active || chest != -1 || !rectangle.Intersects(value))
				{
					if (chest == -1)
					{
						Main.PlaySound(11);
					}
					talkNPC = -1;
					Main.npcChatText = "";
				}
			}
			if (sign >= 0)
			{
				try
				{
					Rectangle value2 = default(Rectangle);
					value2.X = Main.sign[sign].x * 16;
					value2.Y = Main.sign[sign].y * 16;
					value2.Width = 32;
					value2.Height = 32;
					if (!rectangle.Intersects(value2))
					{
						Main.PlaySound(11);
						sign = -1;
						Main.editSign = false;
						Main.npcChatText = "";
					}
				}
				catch
				{
					Main.PlaySound(11);
					sign = -1;
					Main.editSign = false;
					Main.npcChatText = "";
				}
			}
		}

		public void CheckCollideNPCs()
		{
			UpdatePlayerRect();
			Rectangle value = default(Rectangle);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC nPC = Main.npc[i];
				if (!nPC.active || nPC.friendly || nPC.damage <= 0)
				{
					continue;
				}
				value.X = (int)nPC.position.X;
				value.Y = (int)nPC.position.Y;
				value.Width = nPC.width;
				value.Height = nPC.height;
				if (!PlayerRect.Intersects(value))
				{
					continue;
				}
				int num = -1;
				if (nPC.position.X + (float)nPC.width * 0.5f < position.X + (float)width * 0.5f)
				{
					num = 1;
				}
				int damage = nPC.damage;
				if (nPC.DamagePlayer != null)
				{
					nPC.DamagePlayer(this, ref damage);
				}
				if (Codable.RunPlayerMethodRef("DamagePlayer", false, this, damage, nPC) && Codable.customMethodRefReturn != null)
				{
					damage = (int)Codable.customMethodRefReturn[1];
				}
				damage = Main.DamageVar(damage);
				if (whoAmi == Main.myPlayer && thorns && !immune && !nPC.dontTakeDamage)
				{
					int num2 = damage / 3;
					nPC.StrikeNPC(num2, 10f, -num);
					if (Main.netMode != 0)
					{
						NetMessage.SendData(28, -1, -1, "", i, 1f, 10f, 0f - (float)num, num2);
					}
				}
				switch (nPC.type)
				{
				case 1:
					if (Main.rand.Next(4) == 0 && nPC.name == "Black Slime")
					{
						AddBuff(22, 900);
					}
					break;
				case 23:
				case 25:
					if (Main.rand.Next(3) == 0)
					{
						AddBuff(24, 420);
					}
					break;
				case 34:
				case 83:
				case 84:
					if (Main.rand.Next(3) == 0)
					{
						AddBuff(23, 240);
					}
					break;
				case 75:
					if (Main.rand.Next(10) == 0)
					{
						AddBuff(35, 420);
					}
					if (Main.rand.Next(10) == 0)
					{
						AddBuff(32, 900);
					}
					break;
				case 77:
					if (Main.rand.Next(6) == 0)
					{
						AddBuff(36, 18000);
					}
					break;
				case 78:
				case 82:
					if (Main.rand.Next(8) == 0)
					{
						AddBuff(32, 900);
					}
					break;
				case 79:
					if (Main.rand.Next(4) == 0)
					{
						AddBuff(22, 900);
					}
					if (Main.rand.Next(5) == 0)
					{
						AddBuff(35, 420);
					}
					break;
				case 80:
				case 93:
				case 109:
					if (Main.rand.Next(12) == 0)
					{
						AddBuff(31, 420);
					}
					break;
				case 81:
					if (Main.rand.Next(4) == 0 && nPC.name == "Black Slime")
					{
						AddBuff(22, 900);
					}
					break;
				case 102:
				case 104:
					if (Main.rand.Next(8) == 0)
					{
						AddBuff(30, 2700);
					}
					break;
				case 103:
					if (Main.rand.Next(5) == 0)
					{
						AddBuff(35, 420);
					}
					break;
				case 112:
					if (Main.rand.Next(20) == 0)
					{
						AddBuff(33, 18000);
					}
					break;
				case 141:
					if (Main.rand.Next(2) == 0)
					{
						AddBuff(20, 600);
					}
					break;
				}
				bool crit = Main.rand.Next(1, 101) <= nPC.CritChance;
				double num3 = Hurt(damage, num, pvp: false, quiet: false, Lang.deathMsg(-1, i), crit, nPC.CritMult);
				Codable.RunPlayerMethod("DealtPlayer", false, this, num3, nPC);
			}
		}

		public void CheckGrapple(int PID)
		{
			wingFrame = 1;
			if (velocity.Y == 0f || (wet && velocity.Y > -0.02f && velocity.Y < 0.02f))
			{
				wingFrame = 0;
			}
			wingTime = 90;
			rocketTime = rocketTimeMax;
			rocketDelay = 0;
			rocketFrame = false;
			canRocket = false;
			rocketRelease = false;
			fallStart = (int)(position.Y / 16f);
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < grapCount; i++)
			{
				num += Main.projectile[grappling[i]].position.X + (float)Main.projectile[grappling[i]].width * 0.5f;
				num2 += Main.projectile[grappling[i]].position.Y + (float)Main.projectile[grappling[i]].height * 0.5f;
			}
			num /= (float)grapCount;
			num2 /= (float)grapCount;
			velocity.X = num - (position.X + (float)width * 0.5f);
			velocity.Y = num2 - (position.Y + (float)height * 0.5f);
			float num3 = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
			float num4 = 1f;
			if (num3 > 11f)
			{
				num4 = 11f / num3;
			}
			velocity.X *= num4;
			velocity.Y *= num4;
			if (itemAnimation == 0)
			{
				if (velocity.X > 0f)
				{
					direction = 1;
				}
				else if (velocity.X < 0f)
				{
					direction = -1;
				}
			}
			if (controlJump)
			{
				if (!releaseJump)
				{
					return;
				}
				if ((velocity.Y == 0f || (wet && velocity.Y > -0.02f && velocity.Y < 0.02f)) && !controlDown)
				{
					velocity.Y = 0f - jumpSpeed;
					jump = jumpHeight / 2;
					releaseJump = false;
				}
				else
				{
					velocity.Y += 0.01f;
					releaseJump = false;
				}
				if (doubleJump)
				{
					jumpAgain = true;
				}
				grappling[0] = 0;
				grapCount = 0;
				for (int j = 0; j < Main.projectile.Length; j++)
				{
					if (Main.projectile[j].active && Main.projectile[j].owner == PID && Main.projectile[j].aiStyle == 7)
					{
						Main.projectile[j].Kill();
					}
				}
			}
			else
			{
				releaseJump = true;
			}
		}

		public void CheckSticky()
		{
			Vector2 vector = Collision.StickyTiles(position, velocity, width, height);
			if (vector.Y != -1f && vector.X != -1f)
			{
				if (whoAmi == Main.myPlayer && (velocity.X != 0f || velocity.Y != 0f))
				{
					stickyBreak++;
					if (stickyBreak > Main.rand.Next(20, 100))
					{
						stickyBreak = 0;
						int num = (int)vector.X;
						int num2 = (int)vector.Y;
						WorldGen.KillTile(num, num2);
						if (Main.netMode == 1 && !Main.tile[num, num2].active && Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, "", 0, num, num2);
						}
					}
				}
				fallStart = (int)(position.Y / 16f);
				jump = 0;
				if (velocity.X > 1f)
				{
					velocity.X = 1f;
				}
				else if (velocity.X < -1f)
				{
					velocity.X = -1f;
				}
				if (velocity.Y > 1f)
				{
					velocity.Y = 1f;
				}
				else if (velocity.Y < -5f)
				{
					velocity.Y = -5f;
				}
				if (velocity.X > 0.75f || velocity.X < -0.75f)
				{
					velocity.X *= 0.85f;
				}
				else
				{
					velocity.X *= 0.6f;
				}
				if (velocity.Y < 0f)
				{
					velocity.Y *= 0.96f;
				}
				else
				{
					velocity.Y *= 0.3f;
				}
			}
			else
			{
				stickyBreak = 0;
			}
		}

		public void CheckBreath(int PID)
		{
			bool flag = Collision.DrownCollision(base.position, width, height, base.gravDir);
			if (armor[0].type == 250)
			{
				flag = true;
			}
			if (inventory[selectedItem].type == 186)
			{
				try
				{
					int num = (int)((base.position.X + (float)width * 0.5f + (float)(6 * direction)) / 16f);
					int num2 = 0;
					if (base.gravDir == -1f)
					{
						num2 = height;
					}
					int num3 = (int)((base.position.Y + (float)num2 - 44f * base.gravDir) / 16f);
					if (Main.tile[num, num3].liquid < 128)
					{
						if (Main.tile[num, num3] == null)
						{
							Main.tile[num, num3] = new Tile();
						}
						if (!Main.tile[num, num3].active || !Main.tileSolid[Main.tile[num, num3].type] || Main.tileSolidTop[Main.tile[num, num3].type])
						{
							flag = false;
						}
					}
				}
				catch
				{
				}
			}
			if (gills)
			{
				flag = !flag;
			}
			if (Main.myPlayer == PID)
			{
				if (merman)
				{
					flag = false;
				}
				if (flag)
				{
					breathCD++;
					int num4 = 7;
					if (inventory[selectedItem].type == 186)
					{
						num4 *= 2;
					}
					if (accDivingHelm)
					{
						num4 *= 4;
					}
					if (breathCD >= num4)
					{
						breathCD = 0;
						breath--;
						if (breath == 0)
						{
							Main.PlaySound(23);
						}
						if (breath <= 0)
						{
							lifeRegenTime = 0;
							breath = 0;
							statLife -= 2;
							if (statLife <= 0)
							{
								statLife = 0;
								KillMe(10.0, 0, pvp: false, Lang.deathMsg(-1, -1, -1, 1));
							}
						}
					}
				}
				else
				{
					breath += 3;
					if (breath > breathMax)
					{
						breath = breathMax;
					}
					breathCD = 0;
				}
			}
			if (flag && Main.rand.Next(20) == 0 && !lavaWet)
			{
				int num5 = 0;
				if (base.gravDir == -1f)
				{
					num5 += height - 12;
				}
				if (inventory[selectedItem].type == 186)
				{
					Vector2 position = default(Vector2);
					position.X = base.position.X + (float)(10 * direction) + 4f;
					position.Y = base.position.Y + (float)num5 - 54f * base.gravDir;
					Dust.NewDust(position, width - 8, 8, 34, 0f, 0f, 0, Color.Transparent, 1.2f);
				}
				else
				{
					Vector2 position2 = default(Vector2);
					position2.X = base.position.X + (float)(12 * direction);
					position2.Y = base.position.Y + (float)num5 + 4f * base.gravDir;
					Dust.NewDust(position2, width - 8, 8, 34, 0f, 0f, 0, Color.Transparent, 1.2f);
				}
			}
		}

		public void CheckWet(int PID)
		{
			int num = height;
			if (waterWalk)
			{
				num -= 6;
			}
			bool flag = Collision.LavaCollision(base.position, width, num);
			if (flag)
			{
				if (!lavaImmune && Main.myPlayer == PID && !immune)
				{
					AddBuff(24, 420);
					Hurt(80, 0, pvp: false, quiet: false, Lang.deathMsg(-1, -1, -1, 2));
				}
				lavaWet = true;
			}
			if (Collision.WetCollision(base.position, width, height))
			{
				if (onFire && !lavaWet)
				{
					for (int i = 0; i < buffType.Length; i++)
					{
						if (buffType[i] == 24)
						{
							DelBuff(i);
						}
					}
				}
				if (!wet)
				{
					if (wetCount == 0)
					{
						wetCount = 10;
						if (!flag)
						{
							for (int j = 0; j < 50; j++)
							{
								Vector2 position = new Vector2(base.position.X - 6f, base.position.Y + (float)height * 0.5f - 8f);
								int num2 = Dust.NewDust(position, width + 12, 24, 33, 0f, 0f, 0, Color.Transparent, 1.3f);
								Main.dust[num2].velocity.X *= 2.5f;
								Main.dust[num2].velocity.Y -= 4f;
								Main.dust[num2].alpha = 100;
								Main.dust[num2].noGravity = true;
							}
							Main.PlaySound(19, (int)base.position.X, (int)base.position.Y, 0);
						}
						else
						{
							for (int k = 0; k < 20; k++)
							{
								Vector2 position2 = new Vector2(base.position.X - 6f, base.position.Y + (float)height * 0.5f - 8f);
								int num3 = Dust.NewDust(position2, width + 12, 24, 35, 0f, 0f, 0, Color.Transparent, 1.3f);
								Main.dust[num3].velocity.X *= 2.5f;
								Main.dust[num3].velocity.Y -= 1.5f;
								Main.dust[num3].alpha = 100;
								Main.dust[num3].noGravity = true;
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
				if (jump > jumpHeight / 5)
				{
					jump = jumpHeight / 5;
				}
				if (wetCount == 0)
				{
					wetCount = 10;
					if (!lavaWet)
					{
						for (int l = 0; l < 50; l++)
						{
							Vector2 position3 = new Vector2(base.position.X - 6f, base.position.Y + (float)height * 0.5f);
							int num4 = Dust.NewDust(position3, width + 12, 24, 33, 0f, -4f, 100, Color.Transparent, 1.3f);
							Main.dust[num4].noGravity = true;
						}
						Main.PlaySound(19, (int)base.position.X, (int)base.position.Y, 0);
					}
					else
					{
						for (int m = 0; m < 20; m++)
						{
							Vector2 position4 = new Vector2(base.position.X - 6f, base.position.Y + (float)height * 0.5f - 8f);
							int num5 = Dust.NewDust(position4, width + 12, 24, 35, 0f, -1.5f, 100, Color.Transparent, 1.3f);
							Main.dust[num5].noGravity = true;
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

		public void ProcessDead(int PID)
		{
			wings = 0;
			poisoned = false;
			onFire = false;
			onFire2 = false;
			blind = false;
			base.gravDir = 1f;
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] > 0)
				{
					DelBuff(i);
				}
				buffTime[i] = 0;
			}
			if (PID == Main.myPlayer)
			{
				Main.npcChatText = "";
				Main.editSign = false;
			}
			grappling[0] = -1;
			grappling[1] = -1;
			grappling[2] = -1;
			sign = -1;
			talkNPC = -1;
			statLife = 0;
			channel = false;
			potionDelay = 0;
			chest = -1;
			changeItem = -1;
			itemAnimation = 0;
			immuneAlpha += 2;
			if (immuneAlpha > 255)
			{
				immuneAlpha = 255;
			}
			headPosition += headVelocity;
			bodyPosition += bodyVelocity;
			legPosition += legVelocity;
			headRotation += headVelocity.X * 0.1f;
			bodyRotation += bodyVelocity.X * 0.1f;
			legRotation += legVelocity.X * 0.1f;
			headVelocity.Y += 0.1f;
			bodyVelocity.Y += 0.1f;
			legVelocity.Y += 0.1f;
			headVelocity.X *= 0.99f;
			bodyVelocity.X *= 0.99f;
			legVelocity.X *= 0.99f;
			if (difficulty == 2)
			{
				if (respawnTimer > 0)
				{
					respawnTimer--;
					return;
				}
				if (whoAmi == Main.myPlayer || Main.netMode == 2)
				{
					ghost = true;
					return;
				}
			}
			else
			{
				respawnTimer--;
				if (respawnTimer <= 0 && Main.myPlayer == whoAmi)
				{
					if (Main.mouseItem.type > 0)
					{
						Main.playerInventory = true;
					}
					Spawn();
					Codable.RunPlayerMethod("OnSpawn", false, this, PID);
					return;
				}
			}
			Codable.RunPlayerMethod("ProcessDead", false, this);
		}

		public override string ToString()
		{
			return "Player " + whoAmi + " " + name;
		}

		public void HealEffect(int healAmount)
		{
			UpdatePlayerRect();
			CombatText.NewText(PlayerRect, HealEffectColour, healAmount.ToString());
			if (Main.netMode == 1 && whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(35, -1, -1, "", whoAmi, healAmount);
			}
		}

		public void ManaEffect(int manaAmount)
		{
			UpdatePlayerRect();
			CombatText.NewText(PlayerRect, ManaEffectColour, manaAmount.ToString());
			if (Main.netMode == 1 && whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(43, -1, -1, "", whoAmi, manaAmount);
			}
		}

		public static byte FindClosest(Vector2 Position, int Width, int Height)
		{
			byte result = 0;
			for (byte b = 0; b < byte.MaxValue; b = (byte)(b + 1))
			{
				if (Main.player[b].active)
				{
					result = b;
					break;
				}
			}
			float num = -1f;
			float num2 = -1f;
			for (byte b2 = 0; b2 < byte.MaxValue; b2 = (byte)(b2 + 1))
			{
				Player player = Main.player[b2];
				if (player.active && !player.dead)
				{
					num2 = Math.Abs(player.position.X + (float)player.width * 0.5f - Position.X - (float)Width * 0.5f) + Math.Abs(player.position.Y + (float)player.height * 0.5f - Position.Y - (float)Height * 0.5f);
					if (num == -1f || num2 < num)
					{
						num = num2;
						result = b2;
					}
				}
			}
			return result;
		}

		public void toggleInv()
		{
			if (talkNPC >= 0)
			{
				talkNPC = -1;
				Main.npcChatText = "";
				Main.PlaySound(11);
			}
			else if (sign >= 0)
			{
				sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
				Main.PlaySound(11);
			}
			else if (!Main.playerInventory)
			{
				Recipe.FindRecipes();
				Main.playerInventory = true;
				Main.PlaySound(10);
			}
			else
			{
				Main.playerInventory = false;
				Main.PlaySound(11);
			}
		}

		public void dropItemCheck()
		{
			if (!Main.playerInventory)
			{
				noThrow = 0;
			}
			if (noThrow > 0)
			{
				noThrow--;
			}
			if (!Main.craftGuide && Main.guideItem.type > 0)
			{
				int num = Item.NewItem((int)position.X, (int)position.Y, width, height, Main.guideItem.type);
				Main.guideItem.position = Main.item[num].position;
				Main.item[num] = Main.guideItem;
				Main.guideItem = new Item();
				if (Main.netMode == 0)
				{
					Main.item[num].noGrabDelay = 100;
				}
				Main.item[num].velocity.Y = -2f;
				Main.item[num].velocity.X = 4f * (float)direction + velocity.X;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num);
				}
			}
			if (!Main.reforge && Main.reforgeItem.type > 0)
			{
				int num2 = Item.NewItem((int)position.X, (int)position.Y, width, height, Main.reforgeItem.type);
				Main.reforgeItem.position = Main.item[num2].position;
				Main.item[num2] = Main.reforgeItem;
				Main.reforgeItem = new Item();
				if (Main.netMode == 0)
				{
					Main.item[num2].noGrabDelay = 100;
				}
				Main.item[num2].velocity.Y = -2f;
				Main.item[num2].velocity.X = 4f * (float)direction + velocity.X;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num2);
				}
			}
			if (Main.myPlayer == whoAmi)
			{
				inventory[48] = (Item)Main.mouseItem.Clone();
			}
			bool flag = true;
			if (Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
			{
				tileTargetX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
				tileTargetY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
				if (selectedItem != 48)
				{
					oldSelectItem = selectedItem;
				}
				selectedItem = 48;
				flag = false;
			}
			if (flag && selectedItem == 48)
			{
				selectedItem = oldSelectItem;
			}
			if (((controlThrow && releaseThrow && inventory[selectedItem].type > 0 && !Main.chatMode) || (((Main.mouseRight && !mouseInterface && Main.mouseRightRelease) || !Main.playerInventory) && Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)) && noThrow <= 0)
			{
				Item item = new Item();
				bool flag2 = false;
				if (((Main.mouseRight && !mouseInterface && Main.mouseRightRelease) || Main.playerInventory) && Main.mouseItem.type > 0 && Main.mouseItem.stack > 0)
				{
					item = inventory[selectedItem];
					inventory[selectedItem] = Main.mouseItem;
					delayUseItem = true;
					controlUseItem = false;
					flag2 = true;
				}
				int num3 = Item.NewItem((int)position.X, (int)position.Y, width, height, inventory[selectedItem].type);
				if (!flag2 && inventory[selectedItem].type == 8 && inventory[selectedItem].stack > 1)
				{
					inventory[selectedItem].stack--;
				}
				else
				{
					inventory[selectedItem].position = Main.item[num3].position;
					Main.item[num3] = inventory[selectedItem];
					inventory[selectedItem] = new Item();
				}
				if (Main.netMode == 0)
				{
					Main.item[num3].noGrabDelay = 100;
				}
				Main.item[num3].velocity.Y = -2f;
				Main.item[num3].velocity.X = 4f * (float)direction + velocity.X;
				if (((Main.mouseRight && !mouseInterface) || !Main.playerInventory) && Main.mouseItem.type > 0)
				{
					inventory[selectedItem] = item;
					Main.mouseItem = new Item();
				}
				else
				{
					itemAnimation = 10;
					itemAnimationMax = 10;
				}
				Recipe.FindRecipes();
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num3);
				}
			}
		}

		public void AddBuff(string buffName, int time, bool quiet = false)
		{
			AddBuff(Config.buffDefs.ID[buffName], time, quiet);
		}

		public int HasBuff(string buffName)
		{
			int num = Config.buffDefs.ID[buffName];
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] == num && buffTime[i] > 0)
				{
					return i;
				}
			}
			return -1;
		}

		public void AddBuff(int type, int time, bool quiet = true)
		{
			if (!quiet && Main.netMode == 1)
			{
				NetMessage.SendData(55, -1, -1, "", whoAmi, type, time);
			}
			int num = -1;
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[i] == type)
				{
					if (buffTime[i] < time)
					{
						buffTime[i] = time;
					}
					return;
				}
			}
			while (num == -1)
			{
				int num2 = -1;
				for (int j = 0; j < buffType.Length; j++)
				{
					if (!Main.debuff[buffType[j]])
					{
						num2 = j;
						break;
					}
				}
				if (num2 == -1)
				{
					return;
				}
				for (int k = num2; k < buffType.Length; k++)
				{
					if (buffType[k] == 0)
					{
						num = k;
						break;
					}
				}
				if (num == -1)
				{
					DelBuff(num2);
				}
			}
			buffType[num] = type;
			buffTime[num] = time;
			if (Config.buffDefs.assemblyByType[type] != null)
			{
				buffCode[num] = Config.buffDefs.assemblyByType[type].CreateInstance("Terraria." + Codable.ParseName(Main.buffName[type]) + "Buff");
			}
			Codable.RunSpecifiedMethod("Buff " + Main.buffName[buffType[num]], buffCode[num], "EffectsStart", this, num, type, time);
		}

		public void DelBuff(int b)
		{
			int num = buffType[b];
			Codable.RunSpecifiedMethod("Buff " + Main.buffName[num], buffCode[b], "EffectsEnd", this, b, num, buffTime[b]);
			buffCode[b] = null;
			buffTime[b] = 0;
			buffType[b] = 0;
			for (int i = 0; i < buffType.Length - 1; i++)
			{
				if (buffTime[i] == 0 || buffType[i] == 0)
				{
					for (int j = i + 1; j < buffType.Length; j++)
					{
						buffTime[j - 1] = buffTime[j];
						buffType[j - 1] = buffType[j];
						buffTime[j] = 0;
						buffType[j] = 0;
						buffCode[j - 1] = buffCode[j];
						buffCode[j] = null;
					}
				}
			}
		}

		public void QuickHeal()
		{
			if ((Codable.RunPlayerMethod("PreQuickHeal", false, this) && !(bool)Codable.customMethodReturn) || noItems || statLife == statLifeMax2 || potionDelay > 0)
			{
				return;
			}
			int num = 0;
			while (true)
			{
				if (num < 48)
				{
					if (inventory[num].stack > 0 && inventory[num].type > 0 && inventory[num].potion && inventory[num].healLife > 0)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			Main.PlaySound(2, (int)position.X, (int)position.Y, inventory[num].useSound);
			if (inventory[num].potion)
			{
				potionDelay = potionDelayTime;
				AddBuff(21, potionDelay);
			}
			statLife += inventory[num].healLife;
			statMana += inventory[num].healMana;
			if (statLife > statLifeMax2)
			{
				statLife = statLifeMax2;
			}
			if (statMana > statManaMax2)
			{
				statMana = statManaMax2;
			}
			if (inventory[num].healLife > 0 && Main.myPlayer == whoAmi)
			{
				HealEffect(inventory[num].healLife);
			}
			if (inventory[num].healMana > 0 && Main.myPlayer == whoAmi)
			{
				ManaEffect(inventory[num].healMana);
			}
			if (inventory[num].consumable)
			{
				inventory[num].stack--;
			}
			if (inventory[num].stack <= 0)
			{
				inventory[num].type = 0;
				inventory[num].name = "";
			}
			Recipe.FindRecipes();
		}

		public void QuickMana()
		{
			if ((Codable.RunPlayerMethod("PreQuickMana", false, this) && !(bool)Codable.customMethodReturn) || noItems || statMana == statManaMax2)
			{
				return;
			}
			int num = 0;
			while (true)
			{
				if (num < 48)
				{
					if (inventory[num].stack > 0 && inventory[num].type > 0 && inventory[num].healMana > 0 && (potionDelay == 0 || !inventory[num].potion))
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			Main.PlaySound(2, (int)position.X, (int)position.Y, inventory[num].useSound);
			if (inventory[num].potion)
			{
				potionDelay = potionDelayTime;
				AddBuff(21, potionDelay);
			}
			statLife += inventory[num].healLife;
			statMana += inventory[num].healMana;
			if (statLife > statLifeMax2)
			{
				statLife = statLifeMax2;
			}
			if (statMana > statManaMax2)
			{
				statMana = statManaMax2;
			}
			if (inventory[num].healLife > 0 && Main.myPlayer == whoAmi)
			{
				HealEffect(inventory[num].healLife);
			}
			if (inventory[num].healMana > 0 && Main.myPlayer == whoAmi)
			{
				ManaEffect(inventory[num].healMana);
			}
			if (inventory[num].consumable)
			{
				inventory[num].stack--;
			}
			if (inventory[num].stack <= 0)
			{
				inventory[num].type = 0;
				inventory[num].name = "";
			}
			Recipe.FindRecipes();
		}

		public int countBuffs()
		{
			int num = 0;
			for (int i = 0; i < buffType.Length; i++)
			{
				if (buffType[num] > 0)
				{
					num++;
				}
			}
			return num;
		}

		public void QuickBuff()
		{
			if ((Codable.RunPlayerMethod("PreQuickBuff", false, this) && !(bool)Codable.customMethodReturn) || noItems)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 48; i++)
			{
				if (countBuffs() == 10)
				{
					return;
				}
				if (inventory[i].stack <= 0 || inventory[i].type <= 0 || inventory[i].buffType <= 0)
				{
					continue;
				}
				bool flag = true;
				for (int j = 0; j < buffType.Length; j++)
				{
					if (buffType[j] == inventory[i].buffType)
					{
						flag = false;
						break;
					}
				}
				if (inventory[i].mana > 0 && flag)
				{
					if (statMana >= (int)((float)inventory[i].mana * manaCost))
					{
						manaRegenDelay = (int)maxRegenDelay;
						statMana -= (int)((float)inventory[i].mana * manaCost);
					}
					else
					{
						flag = false;
					}
				}
				if (whoAmi == Main.myPlayer && inventory[i].type == 603 && !Main.cEd)
				{
					flag = false;
				}
				if (!flag)
				{
					continue;
				}
				num = inventory[i].useSound;
				int num2 = inventory[i].buffTime;
				if (num2 == 0)
				{
					num2 = 3600;
				}
				AddBuff(inventory[i].buffType, num2);
				if (inventory[i].consumable)
				{
					inventory[i].stack--;
					if (inventory[i].stack <= 0)
					{
						inventory[i].type = 0;
						inventory[i].name = "";
					}
				}
			}
			if (num > 0)
			{
				Main.PlaySound(2, (int)position.X, (int)position.Y, num);
				Recipe.FindRecipes();
			}
		}

		public void StatusNPC(int type, int i)
		{
			switch (type)
			{
			case 121:
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
				break;
			case 122:
				if (Main.rand.Next(10) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
				break;
			case 190:
				if (Main.rand.Next(4) == 0)
				{
					Main.npc[i].AddBuff(20, 420);
				}
				break;
			case 217:
				if (Main.rand.Next(5) == 0)
				{
					Main.npc[i].AddBuff(24, 180);
				}
				break;
			}
		}

		public void StatusPvP(int type, int i)
		{
			switch (type)
			{
			case 121:
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
				break;
			case 122:
				if (Main.rand.Next(10) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
				break;
			case 190:
				if (Main.rand.Next(4) == 0)
				{
					Main.player[i].AddBuff(20, 420, quiet: false);
				}
				break;
			case 217:
				if (Main.rand.Next(5) == 0)
				{
					Main.player[i].AddBuff(24, 180, quiet: false);
				}
				break;
			}
		}

		public void Ghost()
		{
			immune = false;
			immuneAlpha = 0;
			controlUp = false;
			controlLeft = false;
			controlDown = false;
			controlRight = false;
			controlJump = false;
			if (Main.hasFocus && !Main.chatMode && !Main.editSign)
			{
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				for (int i = 0; i < pressedKeys.Length; i++)
				{
					string a = pressedKeys[i].ToString();
					if (a == Main.cUp)
					{
						controlUp = true;
					}
					else if (a == Main.cLeft)
					{
						controlLeft = true;
					}
					else if (a == Main.cDown)
					{
						controlDown = true;
					}
					else if (a == Main.cRight)
					{
						controlRight = true;
					}
					else if (a == Main.cJump)
					{
						controlJump = true;
					}
				}
			}
			if (controlUp || controlJump)
			{
				if (velocity.Y > 0f)
				{
					velocity.Y *= 0.9f;
				}
				velocity.Y -= 0.1f;
				if (velocity.Y < -3f)
				{
					velocity.Y = -3f;
				}
			}
			else if (controlDown)
			{
				if (velocity.Y < 0f)
				{
					velocity.Y *= 0.9f;
				}
				velocity.Y += 0.1f;
				if (velocity.Y > 3f)
				{
					velocity.Y = 3f;
				}
			}
			else if (velocity.Y < -0.1f || velocity.Y > 0.1f)
			{
				velocity.Y *= 0.9f;
			}
			else
			{
				velocity.Y = 0f;
			}
			if (controlLeft && !controlRight)
			{
				if (velocity.X > 0f)
				{
					velocity.X *= 0.9f;
				}
				velocity.X -= 0.1f;
				if (velocity.X < -3f)
				{
					velocity.X = -3f;
				}
			}
			else if (controlRight && !controlLeft)
			{
				if (velocity.X < 0f)
				{
					velocity.X *= 0.9f;
				}
				velocity.X += 0.1f;
				if (velocity.X > 3f)
				{
					velocity.X = 3f;
				}
			}
			else if (velocity.X < -0.1f || velocity.X > 0.1f)
			{
				velocity.X *= 0.9f;
			}
			else
			{
				velocity.X = 0f;
			}
			position += velocity;
			ghostFrameCounter++;
			if (velocity.X < 0f)
			{
				direction = -1;
			}
			else if (velocity.X > 0f)
			{
				direction = 1;
			}
			if (ghostFrameCounter >= 8)
			{
				ghostFrameCounter = 0;
				ghostFrame++;
				if (ghostFrame >= 4)
				{
					ghostFrame = 0;
				}
			}
			if (position.X < Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
			{
				position.X = Main.leftWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				velocity.X = 0f;
			}
			if (position.X + (float)width > Main.rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f)
			{
				position.X = Main.rightWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)width;
				velocity.X = 0f;
			}
			if (position.Y < Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f)
			{
				position.Y = Main.topWorld + (float)(Lighting.offScreenTiles * 16) + 16f;
				if (velocity.Y < -0.1f)
				{
					velocity.Y = -0.1f;
				}
			}
			if (position.Y > Main.bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)height)
			{
				position.Y = Main.bottomWorld - (float)(Lighting.offScreenTiles * 16) - 32f - (float)height;
				velocity.Y = 0f;
			}
		}

		public void UpdatePlayer(int PID)
		{
			PreCheckActive();
			if (!active)
			{
				return;
			}
			float num = Main.maxTilesX / 4200;
			num *= num;
			float num2 = (float)((double)(position.Y / 16f - 60f + 10f * num) / Main.worldSurface * 6.0);
			if (num2 < 0.25f)
			{
				num2 = 0.25f;
			}
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			baseGravity *= num2;
			whoAmi = PID;
			if (PID == Main.myPlayer)
			{
				CheckZones();
			}
			if (ghost)
			{
				Ghost();
				return;
			}
			if (dead)
			{
				ProcessDead(PID);
				return;
			}
			if (PID == Main.myPlayer)
			{
				CheckInput();
				if (selectedItem == 48)
				{
					nonTorch = -1;
				}
				else if (itemAnimation == 0)
				{
					if (controlTorch)
					{
						CheckTileReaction();
					}
					else if (nonTorch > -1)
					{
						selectedItem = nonTorch;
						nonTorch = -1;
					}
				}
				if (!controlThrow)
				{
					releaseThrow = true;
				}
				else
				{
					releaseThrow = false;
				}
				if (Main.netMode == 1)
				{
					CheckNetPlayer();
				}
				if (Main.playerInventory)
				{
					AdjTiles();
				}
				CheckOpenedChest();
				if (base.velocity.Y == 0f)
				{
					int num3 = (int)(position.Y / 16f) - fallStart;
					if (((base.gravDir == 1f && num3 > 25) || (base.gravDir == -1f && num3 < -25)) && !noFallDmg && wings == 0)
					{
						immune = false;
						Hurt((int)((float)num3 * base.gravDir - 25f) * 10, 0, pvp: false, quiet: false, Lang.deathMsg(-1, -1, -1, 0));
					}
					fallStart = (int)(position.Y / 16f);
				}
				if (jump > 0 || rocketDelay > 0 || wet || slowFall || num2 < 0.8f || tongued)
				{
					fallStart = (int)(position.Y / 16f);
				}
			}
			if (mouseInterface)
			{
				delayUseItem = true;
			}
			tileTargetX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			tileTargetY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (immune)
			{
				immuneTime--;
				if (immuneTime <= 0)
				{
					immune = false;
				}
				immuneAlpha += immuneAlphaDirection * 50;
				if (immuneAlpha <= 50)
				{
					immuneAlphaDirection = 1;
				}
				else if (immuneAlpha >= 205)
				{
					immuneAlphaDirection = -1;
				}
			}
			else
			{
				immuneAlpha = 0;
			}
			ResetEffects();
			meleeCrit += inventory[selectedItem].crit;
			magicCrit += inventory[selectedItem].crit;
			rangedCrit += inventory[selectedItem].crit;
			if (whoAmi == Main.myPlayer)
			{
				Main.musicBox2 = -1;
			}
			Codable.RunPlayerMethod("PreUpdatePlayer", true, this);
			UpdateBuffs();
			if (accMerman && wet && !lavaWet)
			{
				releaseJump = true;
				wings = 0;
				merman = true;
				accFlipper = true;
				AddBuff(34, 2);
			}
			else
			{
				merman = false;
			}
			accMerman = false;
			if (wolfAcc && !merman && !Main.dayTime && Main.moonPhase == 0 && !wereWolf)
			{
				AddBuff(28, 60);
			}
			wolfAcc = false;
			if (whoAmi == Main.myPlayer)
			{
				for (int i = 0; i < buffType.Length; i++)
				{
					if (buffType[i] > 0 && buffTime[i] <= 0)
					{
						DelBuff(i);
					}
				}
			}
			doubleJump = false;
			ApplyArmour();
			if (pStone)
			{
				potionDelayTime -= 900;
			}
			if (head == 11)
			{
				int i2 = (int)(position.X + (float)width * 0.5f + (float)(8 * direction)) / 16;
				int j = (int)(position.Y + 2f) / 16;
				Lighting.addLight(i2, j, 0.92f, 0.8f, 0.65f);
			}
			setBonus = "";
			Config.ArmorSetBonusEffects(this);
			ApplySetBonuses();
			Codable.RunPlayerMethod("UpdatePlayer", true, this);
			if (merman)
			{
				wings = 0;
			}
			if (meleeSpeed > 4f)
			{
				meleeSpeed = 4f;
			}
			if (moveSpeed > maximumMaxSpeed)
			{
				moveSpeed = maximumMaxSpeed;
			}
			if (slow)
			{
				moveSpeed *= 0.5f;
			}
			if (statDefense < 0)
			{
				statDefense = 0;
			}
			meleeSpeed = 1f / meleeSpeed;
			RegenLife();
			RegenMana();
			baseSpeedAcceleration *= moveSpeed;
			baseSpeed *= moveSpeed;
			if (jumpBoost)
			{
				jumpHeight = 20;
				jumpSpeed = 6.51f;
			}
			if (wereWolf)
			{
				jumpHeight += 2;
				jumpSpeed += 0.2f;
			}
			if (brokenArmor)
			{
				statDefense /= 2;
			}
			if (!doubleJump)
			{
				jumpAgain = false;
			}
			else if (base.velocity.Y == 0f)
			{
				jumpAgain = true;
			}
			if (grappling[0] == -1 && !tongued)
			{
				DirectionalMovement();
				CheckJump();
				if (((base.gravDir == 1f && base.velocity.Y > 0f - jumpSpeed) || (base.gravDir == -1f && base.velocity.Y < jumpSpeed)) && base.velocity.Y != 0f)
				{
					canRocket = true;
				}
				CheckFlying();
				if (base.gravDir == 1f)
				{
					if (base.velocity.Y > maxGravity)
					{
						base.velocity.Y = maxGravity;
					}
					if (slowFall && base.velocity.Y > maxGravity / 3f && !controlDown)
					{
						base.velocity.Y = maxGravity / 3f;
					}
					if (slowFall && base.velocity.Y > maxGravity / 5f && controlUp)
					{
						base.velocity.Y = maxGravity / 10f;
					}
				}
				else
				{
					if (base.velocity.Y < 0f - maxGravity)
					{
						base.velocity.Y = 0f - maxGravity;
					}
					if (slowFall && base.velocity.Y < (0f - maxGravity) / 3f && !controlUp)
					{
						base.velocity.Y = (0f - maxGravity) / 3f;
					}
					if (slowFall && base.velocity.Y < (0f - maxGravity) / 5f && controlDown)
					{
						base.velocity.Y = (0f - maxGravity) / 10f;
					}
				}
			}
			CheckPickupItems(PID);
			if (position.X / 16f - (float)tileRangeX <= (float)tileTargetX && (position.X + (float)width) / 16f + (float)tileRangeX - 1f >= (float)tileTargetX && position.Y / 16f - (float)tileRangeY <= (float)tileTargetY && (position.Y + (float)height) / 16f + (float)tileRangeY - 2f >= (float)tileTargetY)
			{
				if (Main.tile[tileTargetX, tileTargetY] == null)
				{
					Main.tile[tileTargetX, tileTargetY] = new Tile();
				}
				Tile tile = Main.tile[tileTargetX, tileTargetY];
				if (tile.active)
				{
					CheckTileMouseOver(tile);
					if (controlUseTile)
					{
						if (releaseUseTile)
						{
							UseTile(tile);
						}
						releaseUseTile = false;
					}
					else
					{
						releaseUseTile = true;
					}
				}
			}
			if (Main.myPlayer == whoAmi)
			{
				CheckRemoveTongued();
				if (Main.wof >= 0 && Main.npc[Main.wof].active)
				{
					CheckTongued();
				}
				if (controlHook)
				{
					if (releaseHook)
					{
						QuickGrapple();
					}
					releaseHook = false;
				}
				else
				{
					releaseHook = true;
				}
				CheckInteract();
				if (Main.editSign)
				{
					if (sign == -1)
					{
						Main.editSign = false;
					}
					else
					{
						Main.npcChatText = Main.GetInputText(Main.npcChatText);
						if (Main.inputTextEnter)
						{
							Main.npcChatText += Encoding.ASCII.GetString(new byte[1]
							{
								10
							});
						}
					}
				}
				if (!immune)
				{
					CheckCollideNPCs();
				}
				Vector2 vector = Collision.HurtTiles(position, base.velocity, width, height, fireWalk);
				if (vector.Y != 0f)
				{
					Hurt(Main.DamageVar(vector.Y), 0, pvp: false, quiet: false, Lang.deathMsg(-1, -1, -1, 3));
				}
			}
			if (grappling[0] >= 0)
			{
				CheckGrapple(PID);
			}
			CheckSticky();
			CheckBreath(PID);
			CheckWet(PID);
			oldPosition = position;
			if (tongued)
			{
				position += base.velocity;
			}
			else if (wet && !merman)
			{
				Vector2 velocity = base.velocity;
				base.velocity = Collision.TileCollision(position, base.velocity, width, height, controlDown);
				Vector2 vector2 = base.velocity * 0.5f;
				if (base.velocity.X != velocity.X)
				{
					vector2.X = base.velocity.X;
				}
				if (base.velocity.Y != velocity.Y)
				{
					vector2.Y = base.velocity.Y;
				}
				position += vector2;
			}
			else
			{
				base.velocity = Collision.TileCollision(position, base.velocity, width, height, controlDown);
				if (waterWalk)
				{
					base.velocity = Collision.WaterCollision(position, base.velocity, width, height, controlDown);
				}
				position += base.velocity;
			}
			if (base.velocity.Y == 0f)
			{
				if (base.gravDir == 1f && Collision.up)
				{
					base.velocity.Y = 0.01f;
				}
				else if (base.gravDir == -1f && Collision.down)
				{
					base.velocity.Y = -0.01f;
				}
				if (!merman)
				{
					jump = 0;
				}
			}
			if (whoAmi == Main.myPlayer)
			{
				Collision.SwitchTiles(position, width, height, oldPosition);
			}
			KeepInBounds();
			if (Main.ignoreErrors)
			{
				try
				{
					ItemCheck(PID);
				}
				catch
				{
				}
			}
			else
			{
				ItemCheck(PID);
			}
			PlayerFrame();
			if (statLife > statLifeMax2)
			{
				statLife = statLifeMax2;
			}
			grappling[0] = -1;
			grapCount = 0;
		}

		public void KeepInBounds()
		{
			int num = Lighting.offScreenTiles * 16 + 16;
			if (position.X < Main.leftWorld + (float)num)
			{
				position.X = Main.leftWorld + (float)num;
				velocity.X = 0f;
			}
			else if (position.X + (float)width > Main.rightWorld - (float)num - 16f)
			{
				position.X = Main.rightWorld - (float)num - 16f - (float)width;
				velocity.X = 0f;
			}
			if (position.Y < Main.topWorld + (float)num)
			{
				position.Y = Main.topWorld + (float)num;
				if (velocity.Y < 0.11f)
				{
					velocity.Y = 0.11f;
				}
			}
			else if (position.Y > Main.bottomWorld - (float)num - 16f - (float)height)
			{
				position.Y = Main.bottomWorld - (float)num - 16f - (float)height;
				velocity.Y = 0f;
			}
		}

		public bool SellItem(int price, int stack)
		{
			if (price <= 0)
			{
				return false;
			}
			Item[] array = new Item[48];
			for (int i = 0; i < 48; i++)
			{
				array[i] = (Item)inventory[i].Clone();
			}
			int num = price / 5;
			num *= stack;
			if (num < 1)
			{
				num = 1;
			}
			bool flag = false;
			while (num >= 1000000 && !flag)
			{
				int num2 = -1;
				for (int num3 = 43; num3 >= 0; num3--)
				{
					if (num2 == -1 && (inventory[num3].type == 0 || inventory[num3].stack == 0))
					{
						num2 = num3;
					}
					while (inventory[num3].type == 74 && inventory[num3].stack < inventory[num3].maxStack && num >= 1000000)
					{
						inventory[num3].stack++;
						num -= 1000000;
						DoCoins(num3);
						if (inventory[num3].stack == 0 && num2 == -1)
						{
							num2 = num3;
						}
					}
				}
				if (num >= 1000000)
				{
					if (num2 == -1)
					{
						flag = true;
						continue;
					}
					inventory[num2].SetDefaults(74);
					num -= 1000000;
				}
			}
			while (num >= 10000 && !flag)
			{
				int num4 = -1;
				for (int num5 = 43; num5 >= 0; num5--)
				{
					if (num4 == -1 && (inventory[num5].type == 0 || inventory[num5].stack == 0))
					{
						num4 = num5;
					}
					while (inventory[num5].type == 73 && inventory[num5].stack < inventory[num5].maxStack && num >= 10000)
					{
						inventory[num5].stack++;
						num -= 10000;
						DoCoins(num5);
						if (inventory[num5].stack == 0 && num4 == -1)
						{
							num4 = num5;
						}
					}
				}
				if (num >= 10000)
				{
					if (num4 == -1)
					{
						flag = true;
						continue;
					}
					inventory[num4].SetDefaults(73);
					num -= 10000;
				}
			}
			while (num >= 100 && !flag)
			{
				int num6 = -1;
				for (int num7 = 43; num7 >= 0; num7--)
				{
					if (num6 == -1 && (inventory[num7].type == 0 || inventory[num7].stack == 0))
					{
						num6 = num7;
					}
					while (inventory[num7].type == 72 && inventory[num7].stack < inventory[num7].maxStack && num >= 100)
					{
						inventory[num7].stack++;
						num -= 100;
						DoCoins(num7);
						if (inventory[num7].stack == 0 && num6 == -1)
						{
							num6 = num7;
						}
					}
				}
				if (num >= 100)
				{
					if (num6 == -1)
					{
						flag = true;
						continue;
					}
					inventory[num6].SetDefaults(72);
					num -= 100;
				}
			}
			while (num >= 1 && !flag)
			{
				int num8 = -1;
				for (int num9 = 43; num9 >= 0; num9--)
				{
					if (num8 == -1 && (inventory[num9].type == 0 || inventory[num9].stack == 0))
					{
						num8 = num9;
					}
					while (inventory[num9].type == 71 && inventory[num9].stack < inventory[num9].maxStack && num >= 1)
					{
						inventory[num9].stack++;
						num--;
						DoCoins(num9);
						if (inventory[num9].stack == 0 && num8 == -1)
						{
							num8 = num9;
						}
					}
				}
				if (num >= 1)
				{
					if (num8 == -1)
					{
						flag = true;
						continue;
					}
					inventory[num8].SetDefaults(71);
					num--;
				}
			}
			if (flag)
			{
				for (int j = 0; j < 48; j++)
				{
					inventory[j] = (Item)array[j].Clone();
				}
				return false;
			}
			return true;
		}

		public bool BuyItem(int price)
		{
			if (price <= 0)
			{
				return true;
			}
			int num = 0;
			int num2 = price;
			Item[] array = new Item[44];
			for (int i = 0; i < 44; i++)
			{
				array[i] = (Item)inventory[i].Clone();
				if (inventory[i].type == 71)
				{
					num += inventory[i].stack;
				}
				else if (inventory[i].type == 72)
				{
					num += inventory[i].stack * 100;
				}
				else if (inventory[i].type == 73)
				{
					num += inventory[i].stack * 10000;
				}
				else if (inventory[i].type == 74)
				{
					num += inventory[i].stack * 1000000;
				}
			}
			if (num >= price)
			{
				num2 = price;
				while (num2 > 0)
				{
					if (num2 >= 1000000)
					{
						for (int j = 0; j < 44; j++)
						{
							if (inventory[j].type != 74)
							{
								continue;
							}
							while (inventory[j].stack > 0 && num2 >= 1000000)
							{
								num2 -= 1000000;
								inventory[j].stack--;
								if (inventory[j].stack == 0)
								{
									inventory[j].type = 0;
								}
							}
						}
					}
					if (num2 >= 10000)
					{
						for (int k = 0; k < 44; k++)
						{
							if (inventory[k].type != 73)
							{
								continue;
							}
							while (inventory[k].stack > 0 && num2 >= 10000)
							{
								num2 -= 10000;
								inventory[k].stack--;
								if (inventory[k].stack == 0)
								{
									inventory[k].type = 0;
								}
							}
						}
					}
					if (num2 >= 100)
					{
						for (int l = 0; l < 44; l++)
						{
							if (inventory[l].type != 72)
							{
								continue;
							}
							while (inventory[l].stack > 0 && num2 >= 100)
							{
								num2 -= 100;
								inventory[l].stack--;
								if (inventory[l].stack == 0)
								{
									inventory[l].type = 0;
								}
							}
						}
					}
					if (num2 >= 1)
					{
						for (int m = 0; m < 44; m++)
						{
							if (inventory[m].type != 71)
							{
								continue;
							}
							while (inventory[m].stack > 0 && num2 >= 1)
							{
								num2--;
								inventory[m].stack--;
								if (inventory[m].stack == 0)
								{
									inventory[m].type = 0;
								}
							}
						}
					}
					if (num2 <= 0)
					{
						continue;
					}
					int num3 = -1;
					for (int num4 = 43; num4 >= 0; num4--)
					{
						if (inventory[num4].type == 0 || inventory[num4].stack == 0)
						{
							num3 = num4;
							break;
						}
					}
					if (num3 < 0)
					{
						for (int n = 0; n < 44; n++)
						{
							inventory[n] = (Item)array[n].Clone();
						}
						return false;
					}
					bool flag = true;
					if (num2 >= 10000)
					{
						for (int num5 = 0; num5 < 48; num5++)
						{
							if (inventory[num5].type == 74 && inventory[num5].stack >= 1)
							{
								inventory[num5].stack--;
								if (inventory[num5].stack == 0)
								{
									inventory[num5].type = 0;
								}
								inventory[num3].SetDefaults(73);
								inventory[num3].stack = 100;
								flag = false;
								break;
							}
						}
					}
					else if (num2 >= 100)
					{
						for (int num6 = 0; num6 < 44; num6++)
						{
							if (inventory[num6].type == 73 && inventory[num6].stack >= 1)
							{
								inventory[num6].stack--;
								if (inventory[num6].stack == 0)
								{
									inventory[num6].type = 0;
								}
								inventory[num3].SetDefaults(72);
								inventory[num3].stack = 100;
								flag = false;
								break;
							}
						}
					}
					else if (num2 >= 1)
					{
						for (int num7 = 0; num7 < 44; num7++)
						{
							if (inventory[num7].type == 72 && inventory[num7].stack >= 1)
							{
								inventory[num7].stack--;
								if (inventory[num7].stack == 0)
								{
									inventory[num7].type = 0;
								}
								inventory[num3].SetDefaults(71);
								inventory[num3].stack = 100;
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						continue;
					}
					if (num2 < 10000)
					{
						for (int num8 = 0; num8 < 44; num8++)
						{
							if (inventory[num8].type == 73 && inventory[num8].stack >= 1)
							{
								inventory[num8].stack--;
								if (inventory[num8].stack == 0)
								{
									inventory[num8].type = 0;
								}
								inventory[num3].SetDefaults(72);
								inventory[num3].stack = 100;
								flag = false;
								break;
							}
						}
					}
					if (!flag || num2 >= 1000000)
					{
						continue;
					}
					for (int num9 = 0; num9 < 44; num9++)
					{
						if (inventory[num9].type == 74 && inventory[num9].stack >= 1)
						{
							inventory[num9].stack--;
							if (inventory[num9].stack == 0)
							{
								inventory[num9].type = 0;
							}
							inventory[num3].SetDefaults(73);
							inventory[num3].stack = 100;
							flag = false;
							break;
						}
					}
				}
				return true;
			}
			return false;
		}

		public void AdjTiles()
		{
			for (int i = 0; i < 150 + Config.customTileAmt; i++)
			{
				oldAdjTile[i] = adjTile[i];
				adjTile[i] = false;
			}
			oldAdjWater = adjWater;
			adjWater = false;
			oldAdjLava = adjLava;
			adjLava = false;
			int num = (int)((position.X + (float)width * 0.5f) / 16f);
			int num2 = (int)((position.Y + (float)height) / 16f);
			for (int j = num - 4; j <= num + 4; j++)
			{
				for (int k = num2 - 3; k < num2 + 3; k++)
				{
					if (Main.tile[j, k].active)
					{
						adjTile[Main.tile[j, k].type] = true;
						if (Main.tile[j, k].type == 77)
						{
							adjTile[17] = true;
						}
						else if (Main.tile[j, k].type == 133)
						{
							adjTile[17] = true;
							adjTile[77] = true;
						}
						else if (Main.tile[j, k].type == 134)
						{
							adjTile[16] = true;
						}
					}
					if (Main.tile[j, k].liquid > 200)
					{
						if (Main.tile[j, k].lava)
						{
							adjLava = true;
						}
						else
						{
							adjWater = true;
						}
					}
				}
			}
			Codable.RunPlayerMethod("AdjTiles", true, this);
			if (!Main.playerInventory)
			{
				return;
			}
			bool flag = false;
			for (int l = 0; l < 150 + Config.customTileAmt; l++)
			{
				if (oldAdjTile[l] != adjTile[l])
				{
					flag = true;
					break;
				}
			}
			if (adjWater != oldAdjWater)
			{
				flag = true;
			}
			if (adjLava != oldAdjLava)
			{
				flag = true;
			}
			if (flag)
			{
				Recipe.FindRecipes();
			}
		}

		public void PlayerFrame()
		{
			if (merman)
			{
				headRotation = velocity.Y * (float)direction * 0.1f;
				if ((double)headRotation < -0.3)
				{
					headRotation = -0.3f;
				}
				if ((double)headRotation > 0.3)
				{
					headRotation = 0.3f;
				}
			}
			else if (!dead)
			{
				headRotation = 0f;
			}
			if (swimTime > 0)
			{
				swimTime--;
				if (!wet)
				{
					swimTime = 0;
				}
			}
			head = armor[0].headSlot;
			body = armor[1].bodySlot;
			legs = armor[2].legSlot;
			if (armor[8].headSlot >= 0)
			{
				head = armor[8].headSlot;
			}
			if (armor[9].bodySlot >= 0)
			{
				body = armor[9].bodySlot;
			}
			if (armor[10].legSlot >= 0)
			{
				legs = armor[10].legSlot;
			}
			if (wereWolf)
			{
				legs = 20;
				body = 21;
				head = 38;
			}
			if (merman)
			{
				head = 39;
				legs = 21;
				body = 22;
			}
			socialShadow = false;
			Config.ArmorVisualEffects(this);
			if (head == 5 && body == 5 && legs == 5)
			{
				socialShadow = true;
			}
			if (head == 5 && body == 5 && legs == 5 && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(position, width, height, 14, 0f, 0f, 200, Color.Transparent, 1.2f);
			}
			else if (head == 6 && body == 6 && legs == 6 && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > 1f && !rocketFrame)
			{
				for (int i = 0; i < 2; i++)
				{
					int num = Dust.NewDust(new Vector2(position.X - velocity.X * 2f, position.Y - 2f - velocity.Y * 2f), width, height, 6, (0f - velocity.X) * 0.5f, (0f - velocity.Y) * 0.5f, 100, Color.Transparent, 2f);
					Main.dust[num].noGravity = true;
					Main.dust[num].noLight = true;
				}
			}
			else if (head == 7 && body == 7 && legs == 7)
			{
				boneArmor = true;
			}
			else if (head == 8 && body == 8 && legs == 8 && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > 1f)
			{
				int num2 = Dust.NewDust(new Vector2(position.X - velocity.X * 2f, position.Y - 2f - velocity.Y * 2f), width, height, 40, velocity.X * 0.25f, velocity.Y * 0.25f, 50, Color.Transparent, 1.4f);
				Main.dust[num2].noGravity = true;
			}
			else if (head == 9 && body == 9 && legs == 9 && Math.Abs(velocity.X) + Math.Abs(velocity.Y) > 1f && !rocketFrame)
			{
				for (int j = 0; j < 2; j++)
				{
					int num3 = Dust.NewDust(new Vector2(position.X - velocity.X * 2f, position.Y - 2f - velocity.Y * 2f), width, height, 6, (0f - velocity.X) * 0.5f, (0f - velocity.Y) * 0.5f, 100, Color.Transparent, 2f);
					Main.dust[num3].noGravity = true;
					Main.dust[num3].noLight = true;
				}
			}
			else if (body == 18 && legs == 17 && (head == 32 || head == 33 || head == 34) && Main.rand.Next(10) == 0)
			{
				int num4 = Dust.NewDust(new Vector2(position.X - velocity.X * 2f, position.Y - 2f - velocity.Y * 2f), width, height, 43, 0f, 0f, 100, Color.Transparent, 0.3f);
				Main.dust[num4].fadeIn = 0.8f;
			}
			else if (body == 24 && legs == 23 && (head == 42 || head == 43 || head == 41) && velocity.X != 0f && velocity.Y != 0f && Main.rand.Next(10) == 0)
			{
				int num5 = Dust.NewDust(new Vector2(position.X - velocity.X * 2f, position.Y - 2f - velocity.Y * 2f), width, height, 43, 0f, 0f, 100, Color.Transparent, 0.3f);
				Main.dust[num5].fadeIn = 0.8f;
			}
			bodyFrame.Width = 40;
			bodyFrame.Height = 56;
			legFrame.Width = 40;
			legFrame.Height = 56;
			bodyFrame.X = 0;
			legFrame.X = 0;
			if (itemAnimation > 0 && inventory[selectedItem].useStyle != 10)
			{
				if (inventory[selectedItem].useStyle == 1 || inventory[selectedItem].type == 0)
				{
					if ((float)itemAnimation < (float)itemAnimationMax * 0.333f)
					{
						bodyFrame.Y = bodyFrame.Height * 3;
					}
					else if ((float)itemAnimation < (float)itemAnimationMax * 0.666f)
					{
						bodyFrame.Y = bodyFrame.Height * 2;
					}
					else
					{
						bodyFrame.Y = bodyFrame.Height;
					}
				}
				else if (inventory[selectedItem].useStyle == 2)
				{
					if ((float)itemAnimation > (float)itemAnimationMax * 0.5f)
					{
						bodyFrame.Y = bodyFrame.Height * 3;
					}
					else
					{
						bodyFrame.Y = bodyFrame.Height * 2;
					}
				}
				else if (inventory[selectedItem].useStyle == 3)
				{
					if ((float)itemAnimation > (float)itemAnimationMax * 0.666f)
					{
						bodyFrame.Y = bodyFrame.Height * 3;
					}
					else
					{
						bodyFrame.Y = bodyFrame.Height * 3;
					}
				}
				else if (inventory[selectedItem].useStyle == 4)
				{
					bodyFrame.Y = bodyFrame.Height * 2;
				}
				else if (inventory[selectedItem].useStyle == 5)
				{
					if (inventory[selectedItem].type == 281)
					{
						bodyFrame.Y = bodyFrame.Height * 2;
					}
					else
					{
						float num6 = itemRotation * (float)direction;
						bodyFrame.Y = bodyFrame.Height * 3;
						if (num6 < -0.75f)
						{
							bodyFrame.Y = bodyFrame.Height * 2;
							if (base.gravDir == -1f)
							{
								bodyFrame.Y = bodyFrame.Height * 4;
							}
						}
						if (num6 > 0.6f)
						{
							bodyFrame.Y = bodyFrame.Height * 4;
							if (base.gravDir == -1f)
							{
								bodyFrame.Y = bodyFrame.Height * 2;
							}
						}
					}
				}
				else if (inventory[selectedItem].useStyle > 10)
				{
					Config.useStyleDefs.SetFrame(this, inventory[selectedItem]);
				}
			}
			else if (inventory[selectedItem].holdStyle == 1 && (!wet || !inventory[selectedItem].noWet))
			{
				bodyFrame.Y = bodyFrame.Height * 3;
			}
			else if (inventory[selectedItem].holdStyle == 2 && (!wet || !inventory[selectedItem].noWet))
			{
				bodyFrame.Y = bodyFrame.Height * 2;
			}
			else if (inventory[selectedItem].holdStyle == 3)
			{
				bodyFrame.Y = bodyFrame.Height * 3;
			}
			else if (inventory[selectedItem].holdStyle > 3)
			{
				Config.holdStyleDefs.SetFrame(this, inventory[selectedItem]);
			}
			else if (grappling[0] >= 0)
			{
				Vector2 vector = new Vector2(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f);
				float num7 = 0f;
				float num8 = 0f;
				for (int k = 0; k < grapCount; k++)
				{
					num7 += Main.projectile[grappling[k]].position.X + (float)(Main.projectile[grappling[k]].width / 2);
					num8 += Main.projectile[grappling[k]].position.Y + (float)(Main.projectile[grappling[k]].height / 2);
				}
				num7 /= (float)grapCount;
				num8 /= (float)grapCount;
				num7 -= vector.X;
				num8 -= vector.Y;
				if (num8 < 0f && Math.Abs(num8) > Math.Abs(num7))
				{
					bodyFrame.Y = bodyFrame.Height * 2;
					if (base.gravDir == -1f)
					{
						bodyFrame.Y = bodyFrame.Height * 4;
					}
				}
				else if (num8 > 0f && Math.Abs(num8) > Math.Abs(num7))
				{
					bodyFrame.Y = bodyFrame.Height * 4;
					if (base.gravDir == -1f)
					{
						bodyFrame.Y = bodyFrame.Height * 2;
					}
				}
				else
				{
					bodyFrame.Y = bodyFrame.Height * 3;
				}
			}
			else if (swimTime > 0)
			{
				if (swimTime > 20)
				{
					bodyFrame.Y = 0;
				}
				else if (swimTime > 10)
				{
					bodyFrame.Y = bodyFrame.Height * 5;
				}
				else
				{
					bodyFrame.Y = 0;
				}
			}
			else if (velocity.Y != 0f)
			{
				if (wings > 0)
				{
					if (velocity.Y > 0f)
					{
						if (controlJump)
						{
							bodyFrame.Y = bodyFrame.Height * 6;
						}
						else
						{
							bodyFrame.Y = bodyFrame.Height * 5;
						}
					}
					else
					{
						bodyFrame.Y = bodyFrame.Height * 6;
					}
				}
				else
				{
					bodyFrame.Y = bodyFrame.Height * 5;
				}
				bodyFrameCounter = 0.0;
			}
			else if (velocity.X != 0f)
			{
				bodyFrameCounter += Math.Abs(velocity.X) * 1.5f;
				bodyFrame.Y = legFrame.Y;
			}
			else
			{
				bodyFrameCounter = 0.0;
				bodyFrame.Y = 0;
			}
			if (swimTime > 0)
			{
				legFrameCounter += 2.0;
				while (legFrameCounter > 8.0)
				{
					legFrameCounter -= 8.0;
					legFrame.Y += legFrame.Height;
				}
				if (legFrame.Y < legFrame.Height * 7)
				{
					legFrame.Y = legFrame.Height * 19;
				}
				else if (legFrame.Y > legFrame.Height * 19)
				{
					legFrame.Y = legFrame.Height * 7;
				}
			}
			else if (velocity.Y != 0f || grappling[0] > -1)
			{
				legFrameCounter = 0.0;
				legFrame.Y = legFrame.Height * 5;
			}
			else if (velocity.X != 0f)
			{
				legFrameCounter += Math.Abs(velocity.X) * 1.3f;
				while (legFrameCounter > 8.0)
				{
					legFrameCounter -= 8.0;
					legFrame.Y += legFrame.Height;
				}
				if (legFrame.Y < legFrame.Height * 7)
				{
					legFrame.Y = legFrame.Height * 19;
				}
				else if (legFrame.Y > legFrame.Height * 19)
				{
					legFrame.Y = legFrame.Height * 7;
				}
			}
			else
			{
				legFrameCounter = 0.0;
				legFrame.Y = 0;
			}
		}

		public void Spawn()
		{
			if (whoAmi == Main.myPlayer)
			{
				Main.quickBG = 10;
				FindSpawn();
				if (!CheckSpawn(SpawnX, SpawnY))
				{
					SpawnX = -1;
					SpawnY = -1;
				}
				Main.maxQ = true;
			}
			if (Main.netMode == 1 && whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(12, -1, -1, "", Main.myPlayer);
				Main.gameMenu = false;
				OnScreenInterface.Setup();
			}
			headPosition = default(Vector2);
			bodyPosition = default(Vector2);
			legPosition = default(Vector2);
			headRotation = 0f;
			bodyRotation = 0f;
			legRotation = 0f;
			if (statLife <= 0)
			{
				statLife = 100;
				breath = breathMax;
				if (spawnMax)
				{
					statLife = statLifeMax2;
					statMana = statManaMax2;
				}
			}
			if (SpawnX >= 0 && SpawnY >= 0)
			{
				position.X = (float)(SpawnX * 16 + 8) - (float)width * 0.5f;
				position.Y = SpawnY * 16 - height;
			}
			else
			{
				position.X = (float)(Main.spawnTileX * 16 + 8) - (float)width * 0.5f;
				position.Y = Main.spawnTileY * 16 - height;
				for (int i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; i++)
				{
					for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
					{
						if (Main.tileSolid[Main.tile[i, j].type] && !Main.tileSolidTop[Main.tile[i, j].type])
						{
							WorldGen.KillTile(i, j);
						}
						if (Main.tile[i, j].liquid > 0)
						{
							Main.tile[i, j].lava = false;
							Main.tile[i, j].liquid = 0;
							WorldGen.SquareTileFrame(i, j);
						}
					}
				}
			}
			wet = false;
			wetCount = 0;
			lavaWet = false;
			fallStart = (int)(position.Y / 16f);
			velocity.X = 0f;
			velocity.Y = 0f;
			talkNPC = -1;
			if (pvpDeath)
			{
				pvpDeath = false;
				immuneTime = 300;
				statLife = statLifeMax2;
			}
			else
			{
				immuneTime = 60;
			}
			immune = true;
			dead = false;
			immuneTime = 0;
			active = true;
			if (whoAmi == Main.myPlayer)
			{
				Main.renderNow = true;
				if (Main.netMode == 1)
				{
					Netplay.newRecent();
				}
				Main.screenPosition.X = position.X + (float)(width - Main.screenWidth) * 0.5f;
				Main.screenPosition.Y = position.Y + (float)(height - Main.screenHeight) * 0.5f;
			}
		}

		public double Hurt(int TheDamage, int hitDirection, bool pvp = false, bool quiet = false, string deathText = " was slain...", bool Crit = false, float CritMultiplier = 2f)
		{
			if (immune)
			{
				return 0.0;
			}
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			if (Codable.RunPlayerMethodRef("BeforeHurt", true, this, flag, flag2, flag3, TheDamage, hitDirection, pvp, quiet, deathText, Crit, CritMultiplier) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn = Codable.customMethodRefReturn;
				flag = (bool)customMethodRefReturn[1];
				flag2 = (bool)customMethodRefReturn[2];
				flag3 = (bool)customMethodRefReturn[3];
				TheDamage = (int)customMethodRefReturn[4];
				hitDirection = (int)customMethodRefReturn[5];
				deathText = (string)customMethodRefReturn[8];
				Crit = (bool)customMethodRefReturn[9];
				CritMultiplier = (float)customMethodRefReturn[10];
			}
			double num = 0.0;
			if (flag)
			{
				int num2 = TheDamage;
				if (pvp)
				{
					num2 *= 2;
				}
				num = Main.CalculateDamage(num2, statDefense);
				if (num >= 1.0)
				{
					if (Crit)
					{
						num2 = (int)((float)num2 * CritMultiplier);
						num *= (double)CritMultiplier;
					}
					if (Main.netMode == 1 && whoAmi == Main.myPlayer && !quiet)
					{
						NetMessage.SendData(13, -1, -1, "", whoAmi);
						NetMessage.SendData(16, -1, -1, "", whoAmi);
						NetMessage.SendData(26, -1, -1, "", whoAmi, hitDirection, Crit ? CritMultiplier : 1f, pvp ? 1 : 0, TheDamage);
					}
					UpdatePlayerRect();
					CombatText.NewText(PlayerRect, CombatText.PlayerHurtColor, ((int)num).ToString(), Crit);
					statLife -= (int)num;
					immune = true;
					immuneTime = 40;
					if (longInvince)
					{
						immuneTime += 40;
					}
					lifeRegenTime = 0;
					if (pvp)
					{
						immuneTime = 8;
					}
					if (whoAmi == Main.myPlayer && starCloak)
					{
						for (int i = 0; i < 3; i++)
						{
							float num3 = position.X + (float)Main.rand.Next(-400, 400);
							float num4 = position.Y - (float)Main.rand.Next(500, 800);
							float num5 = position.X + (float)width * 0.5f - num3 + (float)Main.rand.Next(-100, 101);
							float num6 = position.Y + (float)height * 0.5f - num4;
							float num7 = 23f / (float)Math.Sqrt(num5 * num5 + num6 * num6);
							num5 *= num7;
							num6 *= num7;
							int num8 = Projectile.NewProjectile(num3, num4, num5, num6, 92, 30, 5f, whoAmi);
							Main.projectile[num8].ai[1] = position.Y;
						}
					}
					if (!noKnockback && hitDirection != 0)
					{
						velocity.X = 4.5f * (float)hitDirection;
						velocity.Y = -3.5f;
					}
					if (flag2)
					{
						if (wereWolf)
						{
							Main.PlaySound(3, (int)position.X, (int)position.Y, 6);
						}
						else if (boneArmor)
						{
							Main.PlaySound(3, (int)position.X, (int)position.Y, 2);
						}
						else if (!male)
						{
							Main.PlaySound(20, (int)position.X, (int)position.Y);
						}
						else
						{
							Main.PlaySound(1, (int)position.X, (int)position.Y);
						}
					}
					if (statLife > 0)
					{
						if (flag3)
						{
							for (int j = 0; (double)j < num / (double)statLifeMax2 * 100.0; j++)
							{
								if (boneArmor)
								{
									Dust.NewDust(position, width, height, 26, 2 * hitDirection, -2f, 0, Color.Transparent);
								}
								else
								{
									Dust.NewDust(position, width, height, 5, 2 * hitDirection, -2f, 0, Color.Transparent);
								}
							}
						}
					}
					else
					{
						statLife = 0;
						if (whoAmi == Main.myPlayer)
						{
							KillMe(num, hitDirection, pvp, deathText);
						}
					}
				}
			}
			if (Codable.RunPlayerMethodRef("AfterHurt", true, this, num, hitDirection, pvp, quiet, deathText, Crit, CritMultiplier) && Codable.customMethodRefReturn != null)
			{
				object[] customMethodRefReturn2 = Codable.customMethodRefReturn;
				num = (double)customMethodRefReturn2[1];
			}
			return num;
		}

		public void KillMeForGood()
		{
			if (File.Exists(Main.playerPathName))
			{
				File.Delete(Main.playerPathName);
			}
			if (File.Exists(Main.playerPathName + ".bak"))
			{
				File.Delete(Main.playerPathName + ".bak");
			}
			if (File.Exists(Main.playerPathName + ".dat"))
			{
				File.Delete(Main.playerPathName + ".dat");
			}
			Main.playerPathName = "";
		}

		public void KillMe(double dmg, int hitDirection, bool pvp = false, string deathText = " was slain...")
		{
			if (dead || (Codable.RunPlayerMethod("PreKill", true, this, dmg, hitDirection, pvp, deathText) && !(bool)Codable.customMethodReturn))
			{
				return;
			}
			if (pvp)
			{
				pvpDeath = true;
			}
			if (Main.netMode != 1)
			{
				float num;
				for (num = (float)Main.rand.Next(-35, 36) * 0.1f; num < 2f && num > -2f; num += (float)Main.rand.Next(-30, 31) * 0.1f)
				{
				}
				int num2 = Projectile.NewProjectile(position.X + (float)width * 0.5f, position.Y + (float)head * 0.5f, (float)Main.rand.Next(10, 30) * 0.1f * (float)hitDirection + num, (float)Main.rand.Next(-40, -20) * 0.1f, 43, 0, 0f, Main.myPlayer);
				Main.projectile[num2].miscText = name + deathText;
			}
			if (difficulty != 0 && Main.myPlayer == whoAmi)
			{
				Main.trashItem.SetDefaults(0);
				if (difficulty == 1)
				{
					DropItems();
				}
				else if (difficulty == 2)
				{
					DropItems();
					KillMeForGood();
				}
			}
			Main.PlaySound(5, (int)position.X, (int)position.Y);
			headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			for (int i = 0; (double)i < 20.0 + dmg / (double)statLifeMax2 * 100.0; i++)
			{
				if (boneArmor)
				{
					Dust.NewDust(position, width, height, 26, 2f * (float)hitDirection, -2f, 0, Color.Transparent);
				}
				else
				{
					Dust.NewDust(position, width, height, 5, 2f * (float)hitDirection, -2f, 0, Color.Transparent);
				}
			}
			dead = true;
			respawnTimer = 600;
			immuneAlpha = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, name + deathText, 255, 225f, 25f, 25f);
			}
			else if (Main.netMode == 0)
			{
				Main.NewText(name + deathText, 225, 25, 25);
			}
			if (Main.netMode == 1 && whoAmi == Main.myPlayer)
			{
				int num3 = 0;
				if (pvp)
				{
					num3 = 1;
				}
				NetMessage.SendData(44, -1, -1, deathText, whoAmi, hitDirection, (int)dmg, num3);
			}
			if (!pvp && whoAmi == Main.myPlayer && difficulty == 0)
			{
				DropCoins();
			}
			Codable.RunPlayerMethod("PostKill", true, this, dmg, hitDirection, pvp, deathText);
			if (whoAmi == Main.myPlayer)
			{
				try
				{
					WorldGen.saveToonWhilePlaying();
				}
				catch
				{
				}
			}
		}

		public bool ItemSpace(Item newItem)
		{
			if (newItem.type == 58)
			{
				return true;
			}
			if (newItem.type == 184)
			{
				return true;
			}
			int num = 40;
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num = 44;
			}
			for (int i = 0; i < num; i++)
			{
				if (inventory[i].type == 0)
				{
					return true;
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (inventory[j].type > 0 && inventory[j].stack < inventory[j].maxStack && newItem.IsTheSameAs(inventory[j]))
				{
					return true;
				}
			}
			if (newItem.ammo > 0)
			{
				if (newItem.type != 75 && newItem.type != 169 && newItem.type != 23 && newItem.type != 408 && newItem.type != 370)
				{
					for (int k = 44; k < 48; k++)
					{
						if (inventory[k].type == 0)
						{
							return true;
						}
					}
				}
				for (int l = 44; l < 48; l++)
				{
					if (inventory[l].type > 0 && inventory[l].stack < inventory[l].maxStack && newItem.IsTheSameAs(inventory[l]))
					{
						return true;
					}
				}
			}
			return false;
		}

		public void DoCoins(int i)
		{
			if (inventory[i].stack != 100 || (inventory[i].type != 71 && inventory[i].type != 72 && inventory[i].type != 73))
			{
				return;
			}
			inventory[i].SetDefaults(inventory[i].type + 1);
			for (int j = 0; j < 44; j++)
			{
				if (inventory[j].IsTheSameAs(inventory[i]) && j != i && inventory[j].stack < inventory[j].maxStack)
				{
					inventory[j].stack++;
					inventory[i].SetDefaults(0);
					inventory[i].active = false;
					inventory[i].name = "";
					inventory[i].type = 0;
					inventory[i].stack = 0;
					DoCoins(j);
				}
			}
		}

		public Item FillAmmo(int plr, Item newItem)
		{
			for (int i = 44; i < 48; i++)
			{
				if (inventory[i].type <= 0 || inventory[i].stack >= inventory[i].maxStack || !newItem.IsTheSameAs(inventory[i]))
				{
					continue;
				}
				Main.PlaySound(7, (int)position.X, (int)position.Y);
				if (newItem.stack + inventory[i].stack <= inventory[i].maxStack)
				{
					inventory[i].stack += newItem.stack;
					ItemText.NewText(newItem, newItem.stack);
					DoCoins(i);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
					return new Item();
				}
				newItem.stack -= inventory[i].maxStack - inventory[i].stack;
				ItemText.NewText(newItem, inventory[i].maxStack - inventory[i].stack);
				inventory[i].stack = inventory[i].maxStack;
				DoCoins(i);
				if (plr == Main.myPlayer)
				{
					Recipe.FindRecipes();
				}
			}
			if (newItem.type != 169 && newItem.type != 75 && newItem.type != 23 && newItem.type != 408 && newItem.type != 370)
			{
				for (int j = 44; j < 48; j++)
				{
					if (inventory[j].type == 0)
					{
						inventory[j] = newItem;
						ItemText.NewText(newItem, newItem.stack);
						DoCoins(j);
						Main.PlaySound(7, (int)position.X, (int)position.Y);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
				}
			}
			return newItem;
		}

		public Item GetItem(int plr, Item newItem)
		{
			if (newItem.noGrabDelay > 0)
			{
				return newItem;
			}
			Item item = newItem;
			int num = 40;
			int num2 = 0;
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num2 = -4;
				num = 44;
			}
			if (item.ammo > 0)
			{
				item = FillAmmo(plr, item);
				if (item.type == 0 || item.stack == 0)
				{
					return new Item();
				}
			}
			for (int i = num2; i < 40; i++)
			{
				int num3 = i;
				if (num3 < 0)
				{
					num3 = 44 + i;
				}
				if (inventory[num3].type <= 0 || inventory[num3].stack >= inventory[num3].maxStack || !item.IsTheSameAs(inventory[num3]))
				{
					continue;
				}
				Main.PlaySound(7, (int)position.X, (int)position.Y);
				if (item.stack + inventory[num3].stack <= inventory[num3].maxStack)
				{
					inventory[num3].stack += item.stack;
					ItemText.NewText(newItem, item.stack);
					DoCoins(num3);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
					return new Item();
				}
				item.stack -= inventory[num3].maxStack - inventory[num3].stack;
				ItemText.NewText(newItem, inventory[num3].maxStack - inventory[num3].stack);
				inventory[num3].stack = inventory[num3].maxStack;
				DoCoins(num3);
				if (plr == Main.myPlayer)
				{
					Recipe.FindRecipes();
				}
			}
			if (newItem.type != 71 && newItem.type != 72 && newItem.type != 73 && newItem.type != 74 && newItem.useStyle > 0)
			{
				for (int j = 0; j < 10; j++)
				{
					if (inventory[j].type == 0)
					{
						inventory[j] = item;
						ItemText.NewText(newItem, newItem.stack);
						DoCoins(j);
						Main.PlaySound(7, (int)position.X, (int)position.Y);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
				}
			}
			for (int num4 = num - 1; num4 >= 0; num4--)
			{
				if (inventory[num4].type == 0)
				{
					inventory[num4] = item;
					ItemText.NewText(newItem, newItem.stack);
					DoCoins(num4);
					Main.PlaySound(7, (int)position.X, (int)position.Y);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
					return new Item();
				}
			}
			return item;
		}

		public void PlaceThing()
		{
			if (inventory[selectedItem].createTile >= 0 && position.X / 16f - (float)tileRangeX - (float)inventory[selectedItem].tileBoost - (float)blockRange <= (float)tileTargetX && (position.X + (float)width) / 16f + (float)tileRangeX + (float)inventory[selectedItem].tileBoost - 1f + (float)blockRange >= (float)tileTargetX && position.Y / 16f - (float)tileRangeY - (float)inventory[selectedItem].tileBoost - (float)blockRange <= (float)tileTargetY && (position.Y + (float)height) / 16f + (float)tileRangeY + (float)inventory[selectedItem].tileBoost - 2f + (float)blockRange >= (float)tileTargetY)
			{
				showItemIcon = true;
				if (Config.tileDefs.assemblyByType[inventory[selectedItem].createTile] != null)
				{
					int createTile = inventory[selectedItem].createTile;
					object code = Config.tileDefs.assemblyByType[createTile].CreateInstance("Terraria." + Codable.ParseName(Main.tileName[createTile]) + "Tile");
					if (Codable.RunSpecifiedMethod("Tile " + Main.tileName[createTile], code, "CanPlace", tileTargetX, tileTargetY) && !(bool)Codable.customMethodReturn)
					{
						return;
					}
				}
				bool flag = false;
				if (Main.tile[tileTargetX, tileTargetY].liquid > 0 && Main.tile[tileTargetX, tileTargetY].lava)
				{
					if (Main.tileSolid[inventory[selectedItem].createTile])
					{
						flag = true;
					}
					else if (Main.tileLavaDeath[inventory[selectedItem].createTile])
					{
						flag = true;
					}
				}
				if (((!Main.tile[tileTargetX, tileTargetY].active && !flag) || Main.tileCut[Main.tile[tileTargetX, tileTargetY].type] || inventory[selectedItem].createTile == 23 || inventory[selectedItem].createTile == 2 || inventory[selectedItem].createTile == 109 || inventory[selectedItem].createTile == 60 || inventory[selectedItem].createTile == 70) && itemTime == 0 && itemAnimation > 0 && controlUseItem)
				{
					bool flag2 = false;
					if (inventory[selectedItem].createTile >= 150)
					{
						flag2 = true;
					}
					else if (inventory[selectedItem].createTile == 23 || inventory[selectedItem].createTile == 2 || inventory[selectedItem].createTile == 109)
					{
						if (Main.tile[tileTargetX, tileTargetY].active && Main.tile[tileTargetX, tileTargetY].type == 0)
						{
							flag2 = true;
						}
					}
					else if (inventory[selectedItem].createTile == 60 || inventory[selectedItem].createTile == 70)
					{
						if (Main.tile[tileTargetX, tileTargetY].active && Main.tile[tileTargetX, tileTargetY].type == 59)
						{
							flag2 = true;
						}
					}
					else if (inventory[selectedItem].createTile == 4 || inventory[selectedItem].createTile == 136)
					{
						int num = Main.tile[tileTargetX, tileTargetY + 1].type;
						int num2 = Main.tile[tileTargetX - 1, tileTargetY].type;
						int num3 = Main.tile[tileTargetX + 1, tileTargetY].type;
						int num4 = Main.tile[tileTargetX - 1, tileTargetY - 1].type;
						int num5 = Main.tile[tileTargetX + 1, tileTargetY - 1].type;
						int num6 = Main.tile[tileTargetX - 1, tileTargetY - 1].type;
						int num7 = Main.tile[tileTargetX + 1, tileTargetY + 1].type;
						if (!Main.tile[tileTargetX, tileTargetY + 1].active)
						{
							num = -1;
						}
						if (!Main.tile[tileTargetX - 1, tileTargetY].active)
						{
							num2 = -1;
						}
						if (!Main.tile[tileTargetX + 1, tileTargetY].active)
						{
							num3 = -1;
						}
						if (!Main.tile[tileTargetX - 1, tileTargetY - 1].active)
						{
							num4 = -1;
						}
						if (!Main.tile[tileTargetX + 1, tileTargetY - 1].active)
						{
							num5 = -1;
						}
						if (!Main.tile[tileTargetX - 1, tileTargetY + 1].active)
						{
							num6 = -1;
						}
						if (!Main.tile[tileTargetX + 1, tileTargetY + 1].active)
						{
							num7 = -1;
						}
						if (num >= 0 && Main.tileSolid[num] && !Main.tileNoAttach[num])
						{
							flag2 = true;
						}
						else if ((num2 >= 0 && Main.tileSolid[num2] && !Main.tileNoAttach[num2]) || (num2 == 5 && num4 == 5 && num6 == 5) || num2 == 124)
						{
							flag2 = true;
						}
						else if ((num3 >= 0 && Main.tileSolid[num3] && !Main.tileNoAttach[num3]) || (num3 == 5 && num5 == 5 && num7 == 5) || num3 == 124)
						{
							flag2 = true;
						}
					}
					else if (inventory[selectedItem].createTile == 78 || inventory[selectedItem].createTile == 98 || inventory[selectedItem].createTile == 100)
					{
						if (Main.tile[tileTargetX, tileTargetY + 1].active && (Main.tileSolid[Main.tile[tileTargetX, tileTargetY + 1].type] || Main.tileTable[Main.tile[tileTargetX, tileTargetY + 1].type]))
						{
							flag2 = true;
						}
					}
					else if (inventory[selectedItem].createTile == 13 || inventory[selectedItem].createTile == 29 || inventory[selectedItem].createTile == 33 || inventory[selectedItem].createTile == 49 || inventory[selectedItem].createTile == 50 || inventory[selectedItem].createTile == 103)
					{
						if (Main.tile[tileTargetX, tileTargetY + 1].active && Main.tileTable[Main.tile[tileTargetX, tileTargetY + 1].type])
						{
							flag2 = true;
						}
					}
					else if (inventory[selectedItem].createTile == 51)
					{
						if (Main.tile[tileTargetX + 1, tileTargetY].active || Main.tile[tileTargetX + 1, tileTargetY].wall > 0 || Main.tile[tileTargetX - 1, tileTargetY].active || Main.tile[tileTargetX - 1, tileTargetY].wall > 0 || Main.tile[tileTargetX, tileTargetY + 1].active || Main.tile[tileTargetX, tileTargetY + 1].wall > 0 || Main.tile[tileTargetX, tileTargetY - 1].active || Main.tile[tileTargetX, tileTargetY - 1].wall > 0)
						{
							flag2 = true;
						}
					}
					else if ((Main.tile[tileTargetX + 1, tileTargetY].active && Main.tileSolid[Main.tile[tileTargetX + 1, tileTargetY].type]) || Main.tile[tileTargetX + 1, tileTargetY].wall > 0 || (Main.tile[tileTargetX - 1, tileTargetY].active && Main.tileSolid[Main.tile[tileTargetX - 1, tileTargetY].type]) || Main.tile[tileTargetX - 1, tileTargetY].wall > 0 || (Main.tile[tileTargetX, tileTargetY + 1].active && (Main.tileSolid[Main.tile[tileTargetX, tileTargetY + 1].type] || Main.tile[tileTargetX, tileTargetY + 1].type == 124)) || Main.tile[tileTargetX, tileTargetY + 1].wall > 0 || (Main.tile[tileTargetX, tileTargetY - 1].active && (Main.tileSolid[Main.tile[tileTargetX, tileTargetY - 1].type] || Main.tile[tileTargetX, tileTargetY - 1].type == 124)) || Main.tile[tileTargetX, tileTargetY - 1].wall > 0)
					{
						flag2 = true;
					}
					if (Main.tileAlch[inventory[selectedItem].createTile])
					{
						flag2 = true;
					}
					if (Main.tile[tileTargetX, tileTargetY].active && Main.tileCut[Main.tile[tileTargetX, tileTargetY].type])
					{
						if (Main.tile[tileTargetX, tileTargetY + 1].type != 78)
						{
							WorldGen.KillTile(tileTargetX, tileTargetY);
							if (!Main.tile[tileTargetX, tileTargetY].active && Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 4, tileTargetX, tileTargetY);
							}
						}
						else
						{
							flag2 = false;
						}
					}
					if (flag2)
					{
						int num8 = inventory[selectedItem].placeStyle;
						if (inventory[selectedItem].createTile == 141)
						{
							num8 = Main.rand.Next(2);
						}
						if (inventory[selectedItem].createTile == 128 || inventory[selectedItem].createTile == 137)
						{
							num8 = ((direction >= 0) ? 1 : (-1));
						}
						if (WorldGen.PlaceTile(tileTargetX, tileTargetY, inventory[selectedItem].createTile, mute: false, forced: false, whoAmi, num8))
						{
							itemTime = inventory[selectedItem].useTime;
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 1, tileTargetX, tileTargetY, inventory[selectedItem].createTile, num8);
							}
							if ((Config.tileDefs.directional[inventory[selectedItem].createTile] || Config.itemDefs.placeFrame[inventory[selectedItem].name] != 0) && Main.netMode == 1)
							{
								Vector2 pos = Codable.GetPos(new Vector2(tileTargetX, tileTargetY));
								int num9 = Config.tileDefs.width[inventory[selectedItem].createTile];
								int num10 = Config.tileDefs.height[inventory[selectedItem].createTile];
								for (int i = (int)pos.X; i < (int)pos.X + num9; i++)
								{
									for (int j = (int)pos.Y; j < (int)pos.Y + num10; j++)
									{
										NetMessage.SendTileSquare(-1, i, j, 1);
									}
								}
							}
							if (inventory[selectedItem].createTile == 15)
							{
								if (direction == 1)
								{
									Main.tile[tileTargetX, tileTargetY].frameX += 18;
									Main.tile[tileTargetX, tileTargetY - 1].frameX += 18;
								}
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, tileTargetX - 1, tileTargetY - 1, 3);
								}
							}
							else if ((inventory[selectedItem].createTile == 79 || inventory[selectedItem].createTile == 90) && Main.netMode == 1)
							{
								NetMessage.SendTileSquare(-1, tileTargetX, tileTargetY, 5);
							}
							inventory[selectedItem].RunMethod("PostPlaceThing", tileTargetX, tileTargetY, this);
						}
					}
				}
			}
			if (inventory[selectedItem].createWall < 0 || !(position.X / 16f - (float)tileRangeX - (float)inventory[selectedItem].tileBoost <= (float)tileTargetX) || !((position.X + (float)width) / 16f + (float)tileRangeX + (float)inventory[selectedItem].tileBoost - 1f >= (float)tileTargetX) || !(position.Y / 16f - (float)tileRangeY - (float)inventory[selectedItem].tileBoost <= (float)tileTargetY) || !((position.Y + (float)height) / 16f + (float)tileRangeY + (float)inventory[selectedItem].tileBoost - 2f >= (float)tileTargetY))
			{
				return;
			}
			showItemIcon = true;
			if (itemTime != 0 || itemAnimation <= 0 || !controlUseItem || (!Main.tile[tileTargetX + 1, tileTargetY].active && Main.tile[tileTargetX + 1, tileTargetY].wall <= 0 && !Main.tile[tileTargetX - 1, tileTargetY].active && Main.tile[tileTargetX - 1, tileTargetY].wall <= 0 && !Main.tile[tileTargetX, tileTargetY + 1].active && Main.tile[tileTargetX, tileTargetY + 1].wall <= 0 && !Main.tile[tileTargetX, tileTargetY - 1].active && Main.tile[tileTargetX, tileTargetY - 1].wall <= 0) || Main.tile[tileTargetX, tileTargetY].wall == inventory[selectedItem].createWall)
			{
				return;
			}
			WorldGen.PlaceWall(tileTargetX, tileTargetY, inventory[selectedItem].createWall);
			if (Main.tile[tileTargetX, tileTargetY].wall != inventory[selectedItem].createWall)
			{
				return;
			}
			itemTime = inventory[selectedItem].useTime;
			if (Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, "", 3, tileTargetX, tileTargetY, inventory[selectedItem].createWall);
			}
			if (inventory[selectedItem].stack <= 1)
			{
				return;
			}
			int createWall = inventory[selectedItem].createWall;
			for (int k = 0; k < 4; k++)
			{
				int num11 = tileTargetX;
				int num12 = tileTargetY;
				switch (k)
				{
				case 0:
					num11--;
					break;
				case 1:
					num11++;
					break;
				case 2:
					num12--;
					break;
				case 3:
					num12++;
					break;
				}
				if (Main.tile[num11, num12].wall != 0)
				{
					continue;
				}
				int num13 = 0;
				for (int l = 0; l < 4; l++)
				{
					int num14 = num11;
					int num15 = num12;
					switch (l)
					{
					case 0:
						num14--;
						break;
					case 1:
						num14++;
						break;
					case 2:
						num15--;
						break;
					case 3:
						num15++;
						break;
					}
					if (Main.tile[num14, num15].wall == createWall)
					{
						num13++;
					}
				}
				if (num13 != 4)
				{
					continue;
				}
				WorldGen.PlaceWall(num11, num12, createWall);
				if (Main.tile[num11, num12].wall == createWall)
				{
					inventory[selectedItem].stack--;
					if (inventory[selectedItem].stack == 0)
					{
						inventory[selectedItem].SetDefaults(0);
					}
					if (Main.netMode == 1)
					{
						NetMessage.SendData(17, -1, -1, "", 3, num11, num12, createWall);
					}
				}
			}
		}

		public void ItemCheck(int i)
		{
			Item item = inventory[selectedItem];
			if (item.PreItemCheck != null)
			{
				item.PreItemCheck(this, i);
			}
			int num = item.damage;
			if (num > 0)
			{
				if (item.melee)
				{
					num = (int)((float)num * meleeDamage);
				}
				else if (item.ranged)
				{
					num = (int)((float)num * rangedDamage);
				}
				else if (item.magic)
				{
					num = (int)((float)num * magicDamage);
				}
			}
			if (item.autoReuse && !noItems)
			{
				releaseUseItem = true;
				if (itemAnimation == 1 && item.stack > 0)
				{
					if (item.shoot > 0 && whoAmi != Main.myPlayer && controlUseItem)
					{
						itemAnimation = 2;
					}
					else
					{
						itemAnimation = 0;
					}
				}
			}
			if (itemAnimation == 0 && reuseDelay > 0)
			{
				itemAnimation = reuseDelay;
				itemTime = reuseDelay;
				reuseDelay = 0;
			}
			if (controlUseItem && releaseUseItem && item.headSlot <= 0 && item.bodySlot <= 0)
			{
				_ = item.legSlot;
			}
			if (controlUseItem && itemAnimation == 0 && releaseUseItem && item.useStyle > 0)
			{
				bool flag = true;
				if (item.CanUse != null)
				{
					flag = item.CanUse(this, i);
				}
				if (item.shoot == 0)
				{
					itemRotation = 0f;
				}
				if (wet && (item.shoot == 85 || item.shoot == 15 || item.shoot == 34))
				{
					flag = false;
				}
				if (whoAmi == Main.myPlayer && item.type == 603 && !Main.cEd)
				{
					flag = false;
				}
				if (noItems)
				{
					flag = false;
				}
				if (item.shoot == 6 || item.shoot == 19 || item.shoot == 33 || item.shoot == 52)
				{
					for (int j = 0; j < Main.projectile.Length; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == item.shoot)
						{
							flag = false;
						}
					}
				}
				else if (item.shoot == 106)
				{
					int num2 = 0;
					for (int k = 0; k < Main.projectile.Length; k++)
					{
						if (Main.projectile[k].active && Main.projectile[k].owner == Main.myPlayer && Main.projectile[k].type == item.shoot)
						{
							num2++;
						}
					}
					if (num2 >= item.stack)
					{
						flag = false;
					}
				}
				else if (item.shoot == 13 || item.shoot == 32)
				{
					for (int l = 0; l < Main.projectile.Length; l++)
					{
						if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == item.shoot && Main.projectile[l].ai[0] != 2f)
						{
							flag = false;
						}
					}
				}
				else if (item.shoot == 73)
				{
					for (int m = 0; m < Main.projectile.Length; m++)
					{
						if (Main.projectile[m].active && Main.projectile[m].owner == Main.myPlayer && Main.projectile[m].type == 74)
						{
							flag = false;
						}
					}
				}
				if (item.potion && flag)
				{
					if (potionDelay <= 0)
					{
						potionDelay = potionDelayTime;
						AddBuff(21, potionDelay);
					}
					else
					{
						flag = false;
					}
				}
				if (item.mana > 0 && silence)
				{
					flag = false;
				}
				if (item.mana > 0 && flag)
				{
					if (item.type != 127 || !spaceGun)
					{
						if (statMana >= (int)((float)item.mana * manaCost))
						{
							statMana -= (int)((float)item.mana * manaCost);
						}
						else if (manaFlower)
						{
							QuickMana();
							if (statMana >= (int)((float)item.mana * manaCost))
							{
								statMana -= (int)((float)item.mana * manaCost);
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							flag = false;
						}
					}
					if (whoAmi == Main.myPlayer && item.buffType != 0)
					{
						AddBuff(item.buffType, item.buffTime);
					}
				}
				if (whoAmi == Main.myPlayer && item.type == 603 && Main.cEd)
				{
					AddBuff(item.buffType, 3600);
				}
				if (Main.dayTime)
				{
					if (item.type == 43)
					{
						flag = false;
					}
					else if (item.type == 544)
					{
						flag = false;
					}
					else if (item.type == 556)
					{
						flag = false;
					}
					else if (item.type == 557)
					{
						flag = false;
					}
				}
				if (item.type == 70 && !zoneEvil)
				{
					flag = false;
				}
				if (item.shoot == 17 && flag && i == Main.myPlayer)
				{
					int num3 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
					int num4 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
					if (Main.tile[num3, num4].active && (Main.tile[num3, num4].type == 0 || Main.tile[num3, num4].type == 2 || Main.tile[num3, num4].type == 23))
					{
						WorldGen.KillTile(num3, num4, fail: false, effectOnly: false, noItem: true);
						if (!Main.tile[num3, num4].active)
						{
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 4, num3, num4);
							}
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				if (flag && item.useAmmo > 0)
				{
					flag = false;
					for (int n = 0; n < 48; n++)
					{
						if (inventory[n].ammo == item.useAmmo && inventory[n].stack > 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					if (item.pick > 0 || item.axe > 0 || item.hammer > 0)
					{
						toolTime = 1;
					}
					if (grappling[0] > -1)
					{
						if (controlRight)
						{
							direction = 1;
						}
						else if (controlLeft)
						{
							direction = -1;
						}
					}
					channel = item.channel;
					attackCD = 0;
					if (item.melee)
					{
						itemAnimation = (int)((float)item.useAnimation * meleeSpeed);
						itemAnimationMax = (int)((float)item.useAnimation * meleeSpeed);
					}
					else
					{
						itemAnimation = item.useAnimation;
						itemAnimationMax = item.useAnimation;
						reuseDelay = item.reuseDelay;
					}
					if (item.useSound > 0)
					{
						Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, item.useSound);
					}
				}
				if (flag && (item.shoot == 18 || item.shoot == 72 || item.shoot == 86 || item.shoot == 87 || item.shoot == 111))
				{
					for (int num5 = 0; num5 < Main.projectile.Length; num5++)
					{
						if (Main.projectile[num5].active && Main.projectile[num5].owner == i && Main.projectile[num5].type == item.shoot)
						{
							Main.projectile[num5].Kill();
						}
						if (item.shoot == 72 && Main.projectile[num5].active && Main.projectile[num5].owner == i && (Main.projectile[num5].type == 86 || Main.projectile[num5].type == 87))
						{
							Main.projectile[num5].Kill();
						}
					}
				}
			}
			if (!controlUseItem)
			{
				channel = false;
			}
			if (itemAnimation > 0)
			{
				if (item.melee)
				{
					itemAnimationMax = (int)((float)item.useAnimation * meleeSpeed);
				}
				else
				{
					itemAnimationMax = item.useAnimation;
				}
				if (item.mana > 0)
				{
					manaRegenDelay = (int)maxRegenDelay;
				}
				if (Main.dedServ)
				{
					itemHeight = item.height;
					itemWidth = item.width;
				}
				else
				{
					itemHeight = Main.itemTexture[item.type].Height;
					itemWidth = Main.itemTexture[item.type].Width;
				}
				itemAnimation--;
				if (!Main.dedServ)
				{
					if (item.useStyle == 1)
					{
						if ((float)itemAnimation < (float)itemAnimationMax * 0.333f)
						{
							float num6 = 10f;
							if (Main.itemTexture[item.type].Width > 64)
							{
								num6 = 28f;
							}
							else if (Main.itemTexture[item.type].Width > 32)
							{
								num6 = 14f;
							}
							itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - num6) * (float)direction;
							itemLocation.Y = base.position.Y + 24f;
						}
						else if ((float)itemAnimation < (float)itemAnimationMax * 0.666f)
						{
							float num7 = 10f;
							if (Main.itemTexture[item.type].Width > 64)
							{
								num7 = 28f;
							}
							else if (Main.itemTexture[item.type].Width > 32)
							{
								num7 = 18f;
							}
							itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - num7) * (float)direction;
							num7 = 10f;
							if (Main.itemTexture[item.type].Height > 64)
							{
								num7 = 14f;
							}
							else if (Main.itemTexture[item.type].Height > 32)
							{
								num7 = 8f;
							}
							itemLocation.Y = base.position.Y + num7;
						}
						else
						{
							float num8 = 6f;
							if (Main.itemTexture[item.type].Width > 64)
							{
								num8 = 28f;
							}
							else if (Main.itemTexture[item.type].Width > 32)
							{
								num8 = 14f;
							}
							itemLocation.X = base.position.X + (float)width * 0.5f - ((float)Main.itemTexture[item.type].Width * 0.5f - num8) * (float)direction;
							num8 = 10f;
							if (Main.itemTexture[item.type].Height > 64)
							{
								num8 = 14f;
							}
							itemLocation.Y = base.position.Y + num8;
						}
						itemRotation = ((float)itemAnimation / (float)itemAnimationMax - 0.5f) * (float)(-direction) * 3.5f - (float)direction * 0.3f;
						if (base.gravDir == -1f)
						{
							itemRotation = 0f - itemRotation;
							itemLocation.Y = base.position.Y + (float)height + (base.position.Y - itemLocation.Y);
						}
					}
					else if (item.useStyle == 2)
					{
						itemRotation = (float)itemAnimation / (float)itemAnimationMax * (float)direction * 2f - 1.4f * (float)direction;
						if ((float)itemAnimation < (float)itemAnimationMax * 0.5f)
						{
							itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - 9f - itemRotation * 12f * (float)direction) * (float)direction;
							itemLocation.Y = base.position.Y + 38f + itemRotation * (float)direction * 4f;
						}
						else
						{
							itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - 9f - itemRotation * 16f * (float)direction) * (float)direction;
							itemLocation.Y = base.position.Y + 38f + itemRotation * (float)direction;
						}
						if (base.gravDir == -1f)
						{
							itemRotation = 0f - itemRotation;
							itemLocation.Y = base.position.Y + (float)height + (base.position.Y - itemLocation.Y);
						}
					}
					else if (item.useStyle == 3)
					{
						if ((float)itemAnimation > (float)itemAnimationMax * 0.666f)
						{
							itemLocation.X = -1000f;
							itemLocation.Y = -1000f;
							itemRotation = -1.3f * (float)direction;
						}
						else
						{
							itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - 4f) * (float)direction;
							itemLocation.Y = base.position.Y + 24f;
							float num9 = (float)itemAnimation / (float)itemAnimationMax * (float)Main.itemTexture[item.type].Width * (float)direction * item.scale * 1.2f - (float)(10 * direction);
							if (num9 > -4f && direction == -1)
							{
								num9 = -8f;
							}
							if (num9 < 4f && direction == 1)
							{
								num9 = 8f;
							}
							itemLocation.X -= num9;
							itemRotation = 0.8f * (float)direction;
						}
						if (base.gravDir == -1f)
						{
							itemLocation.Y = base.position.Y + (float)height + base.position.Y - itemLocation.Y;
						}
					}
					else if (item.useStyle == 4)
					{
						itemRotation = 0f;
						itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f - 9f - itemRotation * 14f * (float)direction - 4f) * (float)direction;
						itemLocation.Y = base.position.Y + (float)Main.itemTexture[item.type].Height * 0.5f + 4f;
						if (base.gravDir == -1f)
						{
							itemLocation.Y = base.position.Y + (float)height + (base.position.Y - itemLocation.Y);
						}
					}
					else if (item.useStyle == 5)
					{
						itemLocation.X = base.position.X + (float)width * 0.5f - (float)Main.itemTexture[item.type].Width * 0.5f - (float)(direction * 2);
						itemLocation.Y = base.position.Y + (float)height * 0.5f - (float)Main.itemTexture[item.type].Height * 0.5f;
					}
					else if (item.useStyle > 10)
					{
						Config.useStyleDefs.SetStyle(this, item);
					}
					item.RunMethod("UseStyle", this);
				}
			}
			else
			{
				if (item.holdStyle == 1)
				{
					if (Main.dedServ)
					{
						itemLocation.X = base.position.X + (float)width * 0.5f + (float)(20 * direction);
					}
					else
					{
						itemLocation.X = base.position.X + (float)width * 0.5f + ((float)Main.itemTexture[item.type].Width * 0.5f + 2f) * (float)direction;
						if (item.type == 282 || item.type == 286)
						{
							itemLocation.X -= (float)(direction * 2);
							itemLocation.Y += 4f;
						}
					}
					itemLocation.Y = base.position.Y + 24f;
					itemRotation = 0f;
					if (base.gravDir == -1f)
					{
						itemLocation.Y = base.position.Y + (float)height + base.position.Y - itemLocation.Y;
					}
				}
				else if (item.holdStyle == 2)
				{
					itemLocation.X = base.position.X + (float)width * 0.5f + (float)(6 * direction);
					itemLocation.Y = base.position.Y + 16f;
					itemRotation = 0.79f * (float)(-direction);
					if (base.gravDir == -1f)
					{
						itemRotation = 0f - itemRotation;
						itemLocation.Y = base.position.Y + (float)height + (base.position.Y - itemLocation.Y);
					}
				}
				else if (item.holdStyle == 3 && !Main.dedServ)
				{
					itemLocation.X = base.position.X + (float)width * 0.5f - (float)Main.itemTexture[item.type].Width * 0.5f - (float)(direction * 2);
					itemLocation.Y = base.position.Y + (float)height * 0.5f - (float)Main.itemTexture[item.type].Height * 0.5f;
					itemRotation = 0f;
				}
				else if (item.holdStyle > 3)
				{
					Config.holdStyleDefs.SetStyle(this, item);
				}
				item.RunMethod("HoldStyle", this);
			}
			if (!Codable.RunPlayerMethod("HeldItemEffects", true, this) || (bool)Codable.customMethodReturn)
			{
				if (((item.type == 8 || (item.type >= 427 && item.type <= 433)) && !wet) || item.type == 523)
				{
					float r = 1f;
					float g = 0.95f;
					float b = 0.8f;
					int num10 = 0;
					if (item.type == 523)
					{
						num10 = 8;
					}
					else if (item.type >= 427)
					{
						num10 = item.type - 426;
					}
					switch (num10)
					{
					case 1:
						r = 0f;
						g = 0.1f;
						b = 1.3f;
						break;
					case 2:
						r = 1f;
						g = 0.1f;
						b = 0.1f;
						break;
					case 3:
						r = 0f;
						g = 1f;
						b = 0.1f;
						break;
					case 4:
						r = 0.9f;
						g = 0f;
						b = 0.9f;
						break;
					case 5:
						r = 1.3f;
						g = 1.3f;
						b = 1.3f;
						break;
					case 6:
						r = 0.9f;
						g = 0.9f;
						b = 0f;
						break;
					case 7:
						r = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
						g = 0.3f;
						b = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
						break;
					case 8:
						b = 0.7f;
						r = 0.85f;
						g = 1f;
						break;
					}
					int num11 = num10;
					switch (num11)
					{
					case 0:
						num11 = 6;
						break;
					case 8:
						num11 = 75;
						break;
					default:
						num11 = 58 + num11;
						break;
					}
					int maxValue = 20;
					if (itemAnimation > 0)
					{
						maxValue = 7;
					}
					if (direction == -1)
					{
						if (Main.rand.Next(maxValue) == 0)
						{
							Vector2 position = new Vector2(itemLocation.X - 16f, itemLocation.Y - 14f * base.gravDir);
							Dust.NewDust(position, 4, 4, num11, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X - 12f + velocity.X) / 16f), (int)((itemLocation.Y - 14f + velocity.Y) / 16f), r, g, b);
					}
					else
					{
						if (Main.rand.Next(maxValue) == 0)
						{
							Vector2 position2 = new Vector2(itemLocation.X + 6f, itemLocation.Y - 14f * base.gravDir);
							Dust.NewDust(position2, 4, 4, num11, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X + 12f + velocity.X) / 16f), (int)((itemLocation.Y - 14f + velocity.Y) / 16f), r, g, b);
					}
				}
				if (item.type == 105 && !wet)
				{
					int maxValue2 = 20;
					if (itemAnimation > 0)
					{
						maxValue2 = 7;
					}
					if (direction == -1)
					{
						if (Main.rand.Next(maxValue2) == 0)
						{
							Vector2 position3 = new Vector2(itemLocation.X - 12f, itemLocation.Y - 20f * base.gravDir);
							Dust.NewDust(position3, 4, 4, 6, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X - 16f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 1f, 0.95f, 0.8f);
					}
					else
					{
						if (Main.rand.Next(maxValue2) == 0)
						{
							Vector2 position4 = new Vector2(itemLocation.X + 4f, itemLocation.Y - 20f * base.gravDir);
							Dust.NewDust(position4, 4, 4, 6, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X + 6f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 1f, 0.95f, 0.8f);
					}
				}
				else if (item.type == 148 && !wet)
				{
					int maxValue3 = 10;
					if (itemAnimation > 0)
					{
						maxValue3 = 7;
					}
					if (direction == -1)
					{
						if (Main.rand.Next(maxValue3) == 0)
						{
							Vector2 position5 = new Vector2(itemLocation.X - 12f, itemLocation.Y - 20f * base.gravDir);
							Dust.NewDust(position5, 4, 4, 29, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X - 16f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.3f, 0.3f, 0.75f);
					}
					else
					{
						if (Main.rand.Next(maxValue3) == 0)
						{
							Vector2 position6 = new Vector2(itemLocation.X + 4f, itemLocation.Y - 20f * base.gravDir);
							Dust.NewDust(position6, 4, 4, 29, 0f, 0f, 100, Color.Transparent);
						}
						Lighting.addLight((int)((itemLocation.X + 6f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.3f, 0.3f, 0.75f);
					}
				}
				else if (item.type == 282)
				{
					if (direction == -1)
					{
						Lighting.addLight((int)((itemLocation.X - 16f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.7f, 1f, 0.8f);
					}
					else
					{
						Lighting.addLight((int)((itemLocation.X + 6f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.7f, 1f, 0.8f);
					}
				}
				else if (item.type == 286)
				{
					if (direction == -1)
					{
						Lighting.addLight((int)((itemLocation.X - 16f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.7f, 0.8f, 1f);
					}
					else
					{
						Lighting.addLight((int)((itemLocation.X + 6f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), 0.7f, 0.8f, 1f);
					}
				}
			}
			if (controlUseItem)
			{
				releaseUseItem = false;
			}
			else
			{
				releaseUseItem = true;
			}
			if (itemTime > 0)
			{
				itemTime--;
			}
			if (i == Main.myPlayer)
			{
				if (item.shoot > 0 && itemAnimation > 0 && itemTime == 0)
				{
					int num12 = item.shoot;
					float num13 = item.shootSpeed;
					if (item.melee && num12 != 25 && num12 != 26 && num12 != 35)
					{
						num13 /= meleeSpeed;
					}
					bool flag2 = false;
					int num14 = num;
					float num15 = item.knockBack;
					if (num12 == 13 || num12 == 32)
					{
						grappling[0] = -1;
						grapCount = 0;
						for (int num16 = 0; num16 < Main.projectile.Length; num16++)
						{
							if (Main.projectile[num16].active && Main.projectile[num16].owner == i && Main.projectile[num16].type == 13)
							{
								Main.projectile[num16].Kill();
							}
						}
					}
					if (item.useAmmo > 0)
					{
						Item item2 = new Item();
						bool flag3 = false;
						for (int num17 = 44; num17 < 48; num17++)
						{
							if (inventory[num17].ammo == item.useAmmo && inventory[num17].stack > 0)
							{
								item2 = inventory[num17];
								flag2 = true;
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							for (int num18 = 0; num18 < 44; num18++)
							{
								if (inventory[num18].ammo == item.useAmmo && inventory[num18].stack > 0)
								{
									item2 = inventory[num18];
									flag2 = true;
									break;
								}
							}
						}
						if (flag2)
						{
							if (item2.shoot > 0)
							{
								num12 = item2.shoot;
							}
							if (num12 == 42)
							{
								if (item2.type == 370)
								{
									num12 = 65;
									num14 += 5;
								}
								else if (item2.type == 408)
								{
									num12 = 68;
									num14 += 5;
								}
							}
							num13 += item2.shootSpeed;
							if (item2.ranged)
							{
								if (item2.damage > 0)
								{
									num14 += (int)((float)item2.damage * rangedDamage);
								}
							}
							else
							{
								num14 += item2.damage;
							}
							if (item.useAmmo == 1 && archery)
							{
								if (num13 < 20f)
								{
									num13 *= 1.2f;
									if (num13 > 20f)
									{
										num13 = 20f;
									}
								}
								num14 = (int)((float)num14 * 1.2f);
							}
							num15 += item2.knockBack;
							bool flag4 = false;
							if (item.type == 98 && Main.rand.Next(3) == 0)
							{
								flag4 = true;
							}
							else if (item.type == 533 && Main.rand.Next(2) == 0)
							{
								flag4 = true;
							}
							else if (item.type == 434 && itemAnimation < item.useAnimation - 2)
							{
								flag4 = true;
							}
							else if (ammoCost80 && Main.rand.Next(5) == 0)
							{
								flag4 = true;
							}
							else if (ammoCost75 && Main.rand.Next(4) == 0)
							{
								flag4 = true;
							}
							else if (num12 == 85 && itemAnimation < itemAnimationMax - 6)
							{
								flag4 = true;
							}
							if (item2.RunMethod("UseAmmo", this, !flag4, item))
							{
								flag4 = !(bool)Codable.customMethodReturn;
							}
							if (item.RunMethod("UseAmmo", this, !flag4, item2))
							{
								flag4 = !(bool)Codable.customMethodReturn;
							}
							if (!flag4)
							{
								item2.stack--;
								if (item2.stack <= 0)
								{
									item2.active = false;
									item2.name = "";
									item2.type = 0;
								}
							}
						}
					}
					else
					{
						flag2 = true;
					}
					switch (num12)
					{
					case 72:
						switch (Main.rand.Next(3))
						{
						case 0:
							num12 = 72;
							break;
						case 1:
							num12 = 86;
							break;
						case 2:
							num12 = 87;
							break;
						}
						break;
					case 73:
					{
						for (int num19 = 0; num19 < Main.projectile.Length; num19++)
						{
							if (Main.projectile[num19].active && Main.projectile[num19].owner == i)
							{
								if (Main.projectile[num19].type == 73)
								{
									num12 = 74;
								}
								else if (Main.projectile[num19].type == 74)
								{
									flag2 = false;
								}
							}
						}
						break;
					}
					}
					if (flag2)
					{
						if (item.mech && kbGlove)
						{
							num15 *= 1.7f;
						}
						if (num12 == 1 && item.type == 120)
						{
							num12 = 2;
						}
						itemTime = item.useTime;
						if ((float)Main.mouseX + Main.screenPosition.X > base.position.X + (float)width * 0.5f)
						{
							direction = 1;
						}
						else
						{
							direction = -1;
						}
						Vector2 vector = new Vector2(base.position.X + (float)width * 0.5f, base.position.Y + (float)height * 0.5f);
						switch (num12)
						{
						case 9:
							vector = new Vector2(base.position.X + (float)width * 0.5f + (float)(Main.rand.Next(601) * -direction), base.position.Y + (float)height * 0.5f - 300f - (float)Main.rand.Next(100));
							num15 = 0f;
							break;
						case 51:
							vector.Y -= 6f * base.gravDir;
							break;
						}
						float num20 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
						float num21 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
						float num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
						float num23 = num22;
						num22 = num13 / num22;
						num20 *= num22;
						num21 *= num22;
						if (item.useStyle == 5)
						{
							itemRotation = (float)Math.Atan2(num21 * (float)direction, num20 * (float)direction);
							NetMessage.SendData(13, -1, -1, "", whoAmi);
							NetMessage.SendData(41, -1, -1, "", whoAmi);
						}
						if (num12 == 12)
						{
							vector.X += num20 * 3f;
							vector.Y += num21 * 3f;
						}
						if (num12 == 17)
						{
							vector.X = (float)Main.mouseX + Main.screenPosition.X;
							vector.Y = (float)Main.mouseY + Main.screenPosition.Y;
						}
						if (num12 == 76)
						{
							num12 += Main.rand.Next(3);
							num23 /= (float)Main.screenHeight * 0.5f;
							if (num23 > 1f)
							{
								num23 = 1f;
							}
							float num24 = num20 + (float)Main.rand.Next(-40, 41) * 0.01f;
							float num25 = num21 + (float)Main.rand.Next(-40, 41) * 0.01f;
							num24 *= num23 + 0.25f;
							num25 *= num23 + 0.25f;
							int num26 = Projectile.NewProjectile(vector.X, vector.Y, num24, num25, num12, num14, num15, i);
							Main.projectile[num26].ai[1] = 1f;
							num23 = num23 * 2f - 1f;
							if (num23 < -1f)
							{
								num23 = -1f;
							}
							if (num23 > 1f)
							{
								num23 = 1f;
							}
							Main.projectile[num26].ai[0] = num23;
							NetMessage.SendData(27, -1, -1, "", num26);
						}
						else if (item.type == 98 || item.type == 533)
						{
							float speedX = num20 + (float)Main.rand.Next(-40, 41) * 0.01f;
							float speedY = num21 + (float)Main.rand.Next(-40, 41) * 0.01f;
							Projectile.NewProjectile(vector.X, vector.Y, speedX, speedY, num12, num14, num15, i, item);
						}
						else if (item.type == 518)
						{
							float speedX2 = num20 + (float)Main.rand.Next(-40, 41) * 0.04f;
							float speedY2 = num21 + (float)Main.rand.Next(-40, 41) * 0.04f;
							Projectile.NewProjectile(vector.X, vector.Y, speedX2, speedY2, num12, num14, num15, i, item);
						}
						else if (item.type == 534)
						{
							for (int num27 = 0; num27 < 4; num27++)
							{
								float speedX3 = num20 + (float)Main.rand.Next(-40, 41) * 0.05f;
								float speedY3 = num21 + (float)Main.rand.Next(-40, 41) * 0.05f;
								Projectile.NewProjectile(vector.X, vector.Y, speedX3, speedY3, num12, num14, num15, i, item);
							}
						}
						else if (item.type == 434)
						{
							float num28 = num20;
							float num29 = num21;
							if (itemAnimation < 5)
							{
								num28 += (float)Main.rand.Next(-40, 41) * 0.01f * 1.1f;
								num29 += (float)Main.rand.Next(-40, 41) * 0.01f * 1.1f;
							}
							else if (itemAnimation < 10)
							{
								num28 += (float)Main.rand.Next(-20, 21) * 0.01f * 1.05f;
								num29 += (float)Main.rand.Next(-20, 21) * 0.01f * 1.05f;
							}
							Projectile.NewProjectile(vector.X, vector.Y, num28, num29, num12, num14, num15, i, item);
						}
						else if (!item.RunMethod("PreShoot", this, vector, new Vector2(num20, num21), num12, num14, num15, i) || (bool)Codable.customMethodReturn)
						{
							int num30 = Projectile.NewProjectile(vector.X, vector.Y, num20, num21, num12, num14, num15, i, item);
							if (num12 == 80)
							{
								Main.projectile[num30].ai[0] = tileTargetX;
								Main.projectile[num30].ai[1] = tileTargetY;
							}
						}
					}
					else if (item.useStyle == 5)
					{
						itemRotation = 0f;
						NetMessage.SendData(41, -1, -1, "", whoAmi);
					}
				}
				if (whoAmi == Main.myPlayer && (item.type == 509 || item.type == 510) && base.position.X / 16f - (float)tileRangeX - (float)item.tileBoost - (float)blockRange <= (float)tileTargetX && (base.position.X + (float)width) / 16f + (float)tileRangeX + (float)item.tileBoost - 1f + (float)blockRange >= (float)tileTargetX && base.position.Y / 16f - (float)tileRangeY - (float)item.tileBoost - (float)blockRange <= (float)tileTargetY && (base.position.Y + (float)height) / 16f + (float)tileRangeY + (float)item.tileBoost - 2f + (float)blockRange >= (float)tileTargetY)
				{
					showItemIcon = true;
					if (itemAnimation > 0 && itemTime == 0 && controlUseItem)
					{
						int i2 = tileTargetX;
						int j2 = tileTargetY;
						if (item.type == 509)
						{
							int num31 = -1;
							for (int num32 = 0; num32 < 48; num32++)
							{
								if (inventory[num32].stack > 0 && inventory[num32].type == 530)
								{
									num31 = num32;
									break;
								}
							}
							if (num31 >= 0 && WorldGen.PlaceWire(i2, j2))
							{
								inventory[num31].stack--;
								if (inventory[num31].stack <= 0)
								{
									inventory[num31].SetDefaults(0);
								}
								itemTime = item.useTime;
								NetMessage.SendData(17, -1, -1, "", 5, tileTargetX, tileTargetY);
							}
						}
						else if (WorldGen.KillWire(i2, j2))
						{
							itemTime = item.useTime;
							NetMessage.SendData(17, -1, -1, "", 6, tileTargetX, tileTargetY);
						}
					}
				}
				if (itemAnimation > 0 && itemTime == 0 && (item.type == 507 || item.type == 508))
				{
					itemTime = item.useTime;
					Vector2 vector2 = new Vector2(base.position.X + (float)width * 0.5f, base.position.Y + (float)height * 0.5f);
					float num33 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
					float num34 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
					float num35 = (float)Math.Sqrt(num33 * num33 + num34 * num34);
					num35 /= (float)(Main.screenHeight / 2);
					if (num35 > 1f)
					{
						num35 = 1f;
					}
					num35 = num35 * 2f - 1f;
					if (num35 < -1f)
					{
						num35 = -1f;
					}
					if (num35 > 1f)
					{
						num35 = 1f;
					}
					Main.harpNote = num35;
					int style = 26;
					if (item.type == 507)
					{
						style = 35;
					}
					Main.PlaySound(2, (int)base.position.X, (int)base.position.Y, style);
					NetMessage.SendData(58, -1, -1, "", whoAmi, num35);
				}
				Tile tile = Main.tile[tileTargetX, tileTargetY];
				if (item.type >= 205 && item.type <= 207 && base.position.X / 16f - (float)tileRangeX - (float)item.tileBoost <= (float)tileTargetX && (base.position.X + (float)width) / 16f + (float)tileRangeX + (float)item.tileBoost - 1f >= (float)tileTargetX && base.position.Y / 16f - (float)tileRangeY - (float)item.tileBoost <= (float)tileTargetY && (base.position.Y + (float)height) / 16f + (float)tileRangeY + (float)item.tileBoost - 2f >= (float)tileTargetY)
				{
					showItemIcon = true;
					if (itemTime == 0 && itemAnimation > 0 && controlUseItem)
					{
						if (item.type == 205)
						{
							bool lava = tile.lava;
							int num36 = 0;
							for (int num37 = tileTargetX - 1; num37 <= tileTargetX + 1; num37++)
							{
								for (int num38 = tileTargetY - 1; num38 <= tileTargetY + 1; num38++)
								{
									if (Main.tile[num37, num38].lava == lava)
									{
										num36 += Main.tile[num37, num38].liquid;
									}
								}
							}
							if (tile.liquid > 0 && num36 > 100)
							{
								if (!lava)
								{
									item.SetDefaults(206);
								}
								else
								{
									item.SetDefaults(207);
								}
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
								itemTime = item.useTime;
								int num39 = tile.liquid;
								tile.liquid = 0;
								tile.lava = false;
								WorldGen.SquareTileFrame(tileTargetX, tileTargetY, resetFrame: false);
								if (Main.netMode == 1)
								{
									NetMessage.sendWater(tileTargetX, tileTargetY);
								}
								else
								{
									Liquid.AddWater(tileTargetX, tileTargetY);
								}
								for (int num40 = tileTargetX - 1; num40 <= tileTargetX + 1; num40++)
								{
									for (int num41 = tileTargetY - 1; num41 <= tileTargetY + 1; num41++)
									{
										if (num39 < 256 && Main.tile[num40, num41].lava == lava)
										{
											int num42 = Main.tile[num40, num41].liquid;
											if (num42 + num39 > 255)
											{
												num42 = 255 - num39;
											}
											num39 += num42;
											Main.tile[num40, num41].liquid -= (byte)num42;
											Main.tile[num40, num41].lava = lava;
											if (Main.tile[num40, num41].liquid == 0)
											{
												Main.tile[num40, num41].lava = false;
											}
											WorldGen.SquareTileFrame(num40, num41, resetFrame: false);
											if (Main.netMode == 1)
											{
												NetMessage.sendWater(num40, num41);
											}
											else
											{
												Liquid.AddWater(num40, num41);
											}
										}
									}
								}
							}
						}
						else if (tile.liquid < 200 && (!tile.active || !Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type]))
						{
							if (item.type == 207)
							{
								if (tile.liquid == 0 || tile.lava)
								{
									Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
									tile.lava = true;
									tile.liquid = byte.MaxValue;
									WorldGen.SquareTileFrame(tileTargetX, tileTargetY);
									item.SetDefaults(205);
									itemTime = item.useTime;
									if (Main.netMode == 1)
									{
										NetMessage.sendWater(tileTargetX, tileTargetY);
									}
								}
							}
							else if (tile.liquid == 0 || !tile.lava)
							{
								Main.PlaySound(19, (int)base.position.X, (int)base.position.Y);
								tile.lava = false;
								tile.liquid = byte.MaxValue;
								WorldGen.SquareTileFrame(tileTargetX, tileTargetY);
								item.SetDefaults(205);
								itemTime = item.useTime;
								if (Main.netMode == 1)
								{
									NetMessage.sendWater(tileTargetX, tileTargetY);
								}
							}
						}
					}
				}
				if (!channel)
				{
					toolTime = itemTime;
				}
				else
				{
					toolTime--;
					if (toolTime < 0)
					{
						if (item.pick > 0)
						{
							toolTime = item.useTime;
						}
						else
						{
							toolTime = (int)((float)item.useTime * pickSpeed);
						}
					}
				}
				if ((item.pick > 0 || item.axe > 0 || item.hammer > 0) && base.position.X / 16f - (float)tileRangeX - (float)item.tileBoost <= (float)tileTargetX && (base.position.X + (float)width) / 16f + (float)tileRangeX + (float)item.tileBoost - 1f >= (float)tileTargetX && base.position.Y / 16f - (float)tileRangeY - (float)item.tileBoost <= (float)tileTargetY && (base.position.Y + (float)height) / 16f + (float)tileRangeY + (float)item.tileBoost - 2f >= (float)tileTargetY)
				{
					bool flag5 = true;
					showItemIcon = true;
					if (tile.active)
					{
						if ((item.pick > 0 && !Main.tileAxe[tile.type] && !Main.tileHammer[tile.type]) || (item.axe > 0 && Main.tileAxe[tile.type]) || (item.hammer > 0 && Main.tileHammer[tile.type]))
						{
							flag5 = false;
						}
						if (toolTime == 0 && itemAnimation > 0 && controlUseItem)
						{
							if (hitTileX != tileTargetX || hitTileY != tileTargetY)
							{
								hitTile = 0;
								hitTileX = tileTargetX;
								hitTileY = tileTargetY;
							}
							if (Main.tileNoFail[tile.type])
							{
								hitTile = 100;
							}
							if (tile.type != 27)
							{
								if (Main.tileHammer[tile.type])
								{
									flag5 = false;
									float value = 0f;
									if (Config.tileDefs.hammer.TryGetValue(tile.type, out value))
									{
										int value2 = 0;
										Config.tileDefs.minHammer.TryGetValue(tile.type, out value2);
										if (item.hammer < value2)
										{
											hitTile = 0;
										}
										else
										{
											hitTile += (int)((float)item.hammer * value);
										}
									}
									else if (tile.type == 48)
									{
										hitTile += item.hammer / 2;
									}
									else if (tile.type == 129)
									{
										hitTile += item.hammer * 2;
									}
									else
									{
										hitTile += item.hammer;
									}
									if ((double)tileTargetY > Main.rockLayer && tile.type == 77 && item.hammer < 60)
									{
										hitTile = 0;
									}
									if (item.hammer > 0)
									{
										if (tile.type == 26 && (item.hammer < 80 || !Main.hardMode))
										{
											hitTile = 0;
											Hurt(statLife / 2, -direction, pvp: false, quiet: false, Lang.deathMsg(-1, -1, -1, 4));
											WorldGen.KillTile(tileTargetX, tileTargetY, fail: true);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY, 1f);
											}
										}
										if (hitTile >= 100)
										{
											if (Main.netMode == 1 && tile.type == 21)
											{
												WorldGen.KillTile(tileTargetX, tileTargetY, fail: true);
												NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY, 1f);
												NetMessage.SendData(34, -1, -1, "", tileTargetX, tileTargetY);
											}
											else
											{
												hitTile = 0;
												WorldGen.KillTile(tileTargetX, tileTargetY, fail: false, effectOnly: false, noItem: false, this);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY);
												}
											}
										}
										else
										{
											WorldGen.KillTile(tileTargetX, tileTargetY, fail: true);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY, 1f);
											}
										}
										itemTime = item.useTime;
									}
								}
								else if (Main.tileAxe[tile.type])
								{
									float value3 = 0f;
									if (Config.tileDefs.axe.TryGetValue(tile.type, out value3))
									{
										int value4 = 0;
										Config.tileDefs.minAxe.TryGetValue(tile.type, out value4);
										if (item.axe < value4)
										{
											hitTile = 0;
										}
										else
										{
											hitTile += (int)((float)item.axe * value3);
										}
									}
									else if (tile.type == 30 || tile.type == 124)
									{
										hitTile += item.axe * 5;
									}
									else if (tile.type == 80)
									{
										hitTile += item.axe * 3;
									}
									else
									{
										hitTile += item.axe;
									}
									if (item.axe > 0)
									{
										if (hitTile >= 100)
										{
											hitTile = 0;
											WorldGen.KillTile(tileTargetX, tileTargetY, fail: false, effectOnly: false, noItem: false, this);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY);
											}
										}
										else
										{
											WorldGen.KillTile(tileTargetX, tileTargetY, fail: true);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY, 1f);
											}
										}
										itemTime = item.useTime;
									}
								}
								else if (item.pick > 0)
								{
									float value5 = 0f;
									if (Config.tileDefs.pick.TryGetValue(tile.type, out value5))
									{
										int value6 = 0;
										Config.tileDefs.minPick.TryGetValue(tile.type, out value6);
										if (item.pick < value6)
										{
											hitTile = 0;
										}
										else
										{
											hitTile += (int)((float)item.pick * value5);
										}
									}
									else if (Main.tileDungeon[tile.type] || tile.type == 37 || tile.type == 25 || tile.type == 58 || tile.type == 117)
									{
										hitTile += item.pick / 2;
									}
									else if (tile.type == 107)
									{
										hitTile += item.pick / 2;
									}
									else if (tile.type == 108)
									{
										hitTile += item.pick / 3;
									}
									else if (tile.type == 111)
									{
										hitTile += item.pick / 4;
									}
									else
									{
										hitTile += item.pick;
									}
									if (tile.type == 25 && item.pick < 65)
									{
										hitTile = 0;
									}
									else if (tile.type == 117 && item.pick < 65)
									{
										hitTile = 0;
									}
									else if (tile.type == 37 && item.pick < 55)
									{
										hitTile = 0;
									}
									else if (tile.type == 22 && (double)tileTargetY > Main.worldSurface && item.pick < 55)
									{
										hitTile = 0;
									}
									else if (tile.type == 56 && item.pick < 65)
									{
										hitTile = 0;
									}
									else if (tile.type == 58 && item.pick < 65)
									{
										hitTile = 0;
									}
									else if (Main.tileDungeon[tile.type] && item.pick < 65)
									{
										if ((float)tileTargetX < (float)Main.maxTilesX * 0.25f || (float)tileTargetX > (float)Main.maxTilesX * 0.75f)
										{
											hitTile = 0;
										}
									}
									else if (tile.type == 107 && item.pick < 100)
									{
										hitTile = 0;
									}
									else if (tile.type == 108 && item.pick < 110)
									{
										hitTile = 0;
									}
									else if (tile.type == 111 && item.pick < 120)
									{
										hitTile = 0;
									}
									if (tile.type == 0 || tile.type == 40 || tile.type == 53 || tile.type == 57 || tile.type == 59 || tile.type == 123)
									{
										hitTile += item.pick;
									}
									if (hitTile >= 100)
									{
										hitTile = 0;
										WorldGen.KillTile(tileTargetX, tileTargetY, fail: false, effectOnly: false, noItem: false, this);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY);
										}
									}
									else
									{
										WorldGen.KillTile(tileTargetX, tileTargetY, fail: true);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, tileTargetX, tileTargetY, 1f);
										}
									}
									itemTime = (int)((float)item.useTime * pickSpeed);
								}
							}
						}
					}
					int num43 = tileTargetX;
					int num44 = tileTargetY;
					bool flag6 = true;
					if (Main.tile[num43, num44].wall > 0)
					{
						if (!Main.wallHouse[Main.tile[num43, num44].wall])
						{
							for (int num45 = num43 - 1; num45 < num43 + 2; num45++)
							{
								for (int num46 = num44 - 1; num46 < num44 + 2; num46++)
								{
									if (Main.tile[num45, num46].wall != Main.tile[num43, num44].wall)
									{
										flag6 = false;
										break;
									}
								}
							}
						}
						else
						{
							flag6 = false;
						}
					}
					if (flag6 && !Main.tile[num43, num44].active)
					{
						int num47 = -1;
						if ((double)(((float)Main.mouseX + Main.screenPosition.X) / 16f) < Math.Round(((float)Main.mouseX + Main.screenPosition.X) / 16f))
						{
							num47 = 0;
						}
						int num48 = -1;
						if ((double)(((float)Main.mouseY + Main.screenPosition.Y) / 16f) < Math.Round(((float)Main.mouseY + Main.screenPosition.Y) / 16f))
						{
							num48 = 0;
						}
						for (int num49 = tileTargetX + num47; num49 <= tileTargetX + num47 + 1; num49++)
						{
							for (int num50 = tileTargetY + num48; num50 <= tileTargetY + num48 + 1; num50++)
							{
								if (!flag6)
								{
									break;
								}
								num43 = num49;
								num44 = num50;
								if (Main.tile[num43, num44].wall <= 0)
								{
									continue;
								}
								if (!Main.wallHouse[Main.tile[num43, num44].wall])
								{
									for (int num51 = num43 - 1; num51 < num43 + 2; num51++)
									{
										for (int num52 = num44 - 1; num52 < num44 + 2; num52++)
										{
											if (Main.tile[num51, num52].wall != Main.tile[num43, num44].wall)
											{
												flag6 = false;
												break;
											}
										}
									}
								}
								else
								{
									flag6 = false;
								}
							}
						}
					}
					if (flag5 && Main.tile[num43, num44].wall > 0 && toolTime == 0 && itemAnimation > 0 && controlUseItem && item.hammer > 0)
					{
						bool flag7 = true;
						if (!Main.wallHouse[Main.tile[num43, num44].wall])
						{
							flag7 = false;
							for (int num53 = num43 - 1; num53 < num43 + 2; num53++)
							{
								for (int num54 = num44 - 1; num54 < num44 + 2; num54++)
								{
									if (Main.tile[num53, num54].wall != Main.tile[num43, num44].wall)
									{
										flag7 = true;
										break;
									}
								}
							}
						}
						if (flag7)
						{
							if (hitTileX != num43 || hitTileY != num44)
							{
								hitTile = 0;
								hitTileX = num43;
								hitTileY = num44;
							}
							hitTile += (int)((float)item.hammer * 1.5f);
							if (hitTile >= 100)
							{
								hitTile = 0;
								WorldGen.KillWall(num43, num44);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, num43, num44);
								}
							}
							else
							{
								WorldGen.KillWall(num43, num44, fail: true);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, num43, num44, 1f);
								}
							}
							itemTime = item.useTime / 2;
						}
					}
				}
				if (item.type == 29 && itemAnimation > 0 && statLifeMax < 400 && itemTime == 0)
				{
					itemTime = item.useTime;
					statLifeMax += 20;
					statLife += 20;
					if (Main.myPlayer == whoAmi)
					{
						HealEffect(20);
					}
				}
				else if (item.type == 109 && itemAnimation > 0 && statManaMax < 200 && itemTime == 0)
				{
					itemTime = item.useTime;
					statManaMax += 20;
					statMana += 20;
					if (Main.myPlayer == whoAmi)
					{
						ManaEffect(20);
					}
				}
				PlaceThing();
			}
			if (item.damage >= 0 && item.type > 0 && !item.noMelee && itemAnimation > 0)
			{
				bool flag8 = false;
				Rectangle rectangle = default(Rectangle);
				rectangle.X = (int)itemLocation.X;
				rectangle.Y = (int)itemLocation.Y;
				if (Main.dedServ)
				{
					rectangle.Width = (int)(32f * item.scale);
					rectangle.Height = (int)(32f * item.scale);
				}
				else
				{
					rectangle.Width = (int)((float)Main.itemTexture[item.type].Width * item.scale);
					rectangle.Height = (int)((float)Main.itemTexture[item.type].Height * item.scale);
				}
				if (direction == -1)
				{
					rectangle.X -= rectangle.Width;
				}
				if (base.gravDir == 1f)
				{
					rectangle.Y -= rectangle.Height;
				}
				if (item.useStyle == 1)
				{
					if ((float)itemAnimation < (float)itemAnimationMax * 0.333f)
					{
						if (direction == -1)
						{
							rectangle.X -= (int)((float)rectangle.Width * 1.4f - (float)rectangle.Width);
						}
						rectangle.Width = (int)((float)rectangle.Width * 1.4f);
						rectangle.Height = (int)((float)rectangle.Height * 1.1f);
						rectangle.Y += (int)((float)rectangle.Height * 0.5f * base.gravDir);
					}
					else if ((float)itemAnimation >= (float)itemAnimationMax * 0.666f)
					{
						if (direction == 1)
						{
							rectangle.X -= (int)((float)rectangle.Width * 1.2f);
						}
						rectangle.Y -= (int)(((float)rectangle.Height * 1.4f - (float)rectangle.Height) * base.gravDir);
						rectangle.Width *= 2;
						rectangle.Height = (int)((float)rectangle.Height * 1.4f);
					}
				}
				else if (item.useStyle == 3)
				{
					if ((float)itemAnimation > (float)itemAnimationMax * 0.666f)
					{
						flag8 = true;
					}
					else
					{
						if (direction == -1)
						{
							rectangle.X -= (int)((float)rectangle.Width * 1.4f - (float)rectangle.Width);
						}
						rectangle.Width = (int)((float)rectangle.Width * 1.4f);
						rectangle.Y += (int)((float)rectangle.Height * 0.6f);
						rectangle.Height = (int)((float)rectangle.Height * 0.6f);
					}
				}
				else if (item.useStyle > 10)
				{
					rectangle = Config.useStyleDefs.UpdateHitBox(this, item, rectangle);
				}
				if (item.RunMethodRef("UseStyleHitBox", this, rectangle) && Codable.customMethodRefReturn != null)
				{
					rectangle = (Rectangle)Codable.customMethodRefReturn[1];
				}
				_ = base.gravDir;
				if (!flag8)
				{
					item.RunMethod("UseItemEffect", this, rectangle);
					Vector2 position7 = new Vector2(rectangle.X, rectangle.Y);
					if ((item.type == 44 || item.type == 45 || item.type == 46 || item.type == 103 || item.type == 104) && Main.rand.Next(15) == 0)
					{
						Dust.NewDust(position7, rectangle.Width, rectangle.Height, 14, (float)direction * 2f, 0f, 150, Color.Transparent, 1.3f);
					}
					else if (item.type == 273)
					{
						if (Main.rand.Next(5) == 0)
						{
							Dust.NewDust(position7, rectangle.Width, rectangle.Height, 14, (float)direction * 2f, 0f, 150, Color.Transparent, 1.4f);
						}
						int num55 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 27, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 100, Color.Transparent, 1.2f);
						Main.dust[num55].noGravity = true;
						Main.dust[num55].velocity *= 0.5f;
					}
					else if (item.type == 65)
					{
						if (Main.rand.Next(5) == 0)
						{
							Dust.NewDust(position7, rectangle.Width, rectangle.Height, 58, 0f, 0f, 150, Color.Transparent, 1.2f);
						}
						if (Main.rand.Next(10) == 0)
						{
							Gore.NewGore(position7, Vector2.Zero, Main.rand.Next(16, 18));
						}
					}
					else if (item.type == 190 || item.type == 213)
					{
						int num56 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 40, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 0, Color.Transparent, 1.2f);
						Main.dust[num56].noGravity = true;
					}
					else if (item.type == 121)
					{
						for (int num57 = 0; num57 < 2; num57++)
						{
							int num58 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 6, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 100, Color.Transparent, 2.5f);
							Main.dust[num58].noGravity = true;
							Main.dust[num58].velocity *= 2f;
						}
					}
					else if (item.type == 122 || item.type == 217)
					{
						int num59 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 6, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 100, Color.Transparent, 1.9f);
						Main.dust[num59].noGravity = true;
					}
					else if (item.type == 155)
					{
						int num60 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 29, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 100, Color.Transparent, 2f);
						Main.dust[num60].noGravity = true;
						Main.dust[num60].velocity *= 0.5f;
					}
					else if (item.type == 367 || item.type == 368)
					{
						if (Main.rand.Next(3) == 0)
						{
							int num61 = Dust.NewDust(position7, rectangle.Width, rectangle.Height, 57, velocity.X * 0.2f + (float)(direction * 3), velocity.Y * 0.2f, 100, Color.Transparent, 1.1f);
							Main.dust[num61].noGravity = true;
							Main.dust[num61].velocity *= 0.5f;
							Main.dust[num61].velocity.X += direction * 2;
						}
						if (Main.rand.Next(4) == 0)
						{
							Dust.NewDust(position7, rectangle.Width, rectangle.Height, 43, 0f, 0f, 254, Color.Transparent, 0.3f);
						}
					}
					if (item.type >= 198 && item.type <= 203)
					{
						float num62 = 0.5f;
						float num63 = 0.5f;
						float num64 = 0.5f;
						if (item.type == 198)
						{
							num62 *= 0.1f;
							num63 *= 0.5f;
							num64 *= 1.2f;
						}
						else if (item.type == 199)
						{
							num62 *= 1f;
							num63 *= 0.2f;
							num64 *= 0.1f;
						}
						else if (item.type == 200)
						{
							num62 *= 0.1f;
							num63 *= 1f;
							num64 *= 0.2f;
						}
						else if (item.type == 201)
						{
							num62 *= 0.8f;
							num63 *= 0.1f;
							num64 *= 1f;
						}
						else if (item.type == 202)
						{
							num62 *= 0.8f;
							num63 *= 0.9f;
							num64 *= 1f;
						}
						else if (item.type == 203)
						{
							num62 *= 0.9f;
							num63 *= 0.9f;
							num64 *= 0.1f;
						}
						Lighting.addLight((int)((itemLocation.X + 6f + velocity.X) / 16f), (int)((itemLocation.Y - 14f) / 16f), num62, num63, num64);
					}
					if (Main.myPlayer == i)
					{
						int num65 = (int)((float)item.damage * meleeDamage);
						float num66 = item.knockBack;
						if (kbGlove)
						{
							num66 *= 2f;
						}
						Rectangle rectangle2 = default(Rectangle);
						rectangle2.X = rectangle.X / 16;
						rectangle2.Y = rectangle.Y / 16;
						rectangle2.Width = rectangle.Width / 16 + 1;
						rectangle2.Height = rectangle.Height / 16 + 1;
						for (int num67 = rectangle2.X; num67 < rectangle2.X + rectangle2.Width; num67++)
						{
							for (int num68 = rectangle2.Y; num68 < rectangle2.Y + rectangle2.Height; num68++)
							{
								if (Main.tile[num67, num68] != null && Main.tileCut[Main.tile[num67, num68].type] && Main.tile[num67, num68 + 1] != null && Main.tile[num67, num68 + 1].type != 78)
								{
									WorldGen.KillTile(num67, num68, fail: false, effectOnly: false, noItem: false, this);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(17, -1, -1, "", 0, num67, num68);
									}
								}
							}
						}
						Rectangle value7 = default(Rectangle);
						for (int num69 = 0; num69 < Main.npc.Length; num69++)
						{
							NPC nPC = Main.npc[num69];
							if (!nPC.active || nPC.immune[i] != 0 || attackCD != 0 || nPC.dontTakeDamage || (nPC.friendly && (nPC.type != 22 || !killGuide)))
							{
								continue;
							}
							value7.X = (int)nPC.position.X;
							value7.Y = (int)nPC.position.Y;
							value7.Width = nPC.width;
							value7.Height = nPC.height;
							if (!rectangle.Intersects(value7))
							{
								continue;
							}
							int num70 = 0;
							if (Codable.RunPlayerMethod("CanHitboxDamageNPC", true, this, rectangle, nPC) && Codable.customMethodReturn != null)
							{
								num70 = (((bool)Codable.customMethodReturn) ? 1 : 2);
							}
							if (num70 != 1 && (num70 == 2 || (!nPC.noTileCollide && !Collision.CanHit(base.position, width, height, nPC.position, nPC.width, nPC.height))))
							{
								continue;
							}
							bool flag9 = false;
							if (Main.rand.Next(1, 101) <= meleeCrit)
							{
								flag9 = true;
							}
							if (Codable.RunPlayerEvent("DamageNPC", true, this, nPC, num65, num66) && Codable.customMethodRefReturn != null)
							{
								num65 = (int)Codable.customMethodRefReturn[2];
								num66 = (float)Codable.customMethodRefReturn[3];
							}
							int num71 = Main.DamageVar(num65);
							StatusNPC(item.type, num69);
							double num72 = nPC.StrikeNPC(num71, num66, direction, flag9, noEffect: false, meleeDamageCrit);
							Codable.RunPlayerMethod("DealtNPC", true, this, nPC, num72);
							nPC.RunMethod("DealtNPC", num72, this);
							if (Main.netMode != 0)
							{
								if (flag9)
								{
									NetMessage.SendData(28, -1, -1, "", num69, meleeDamageCrit, num66, direction, num71);
								}
								else
								{
									NetMessage.SendData(28, -1, -1, "", num69, 1f, num66, direction, num71);
								}
							}
							nPC.immune[i] = itemAnimation;
							attackCD = (int)((float)itemAnimationMax * 0.33f);
						}
						if (hostile)
						{
							Rectangle value8 = default(Rectangle);
							for (int num73 = 0; num73 < 255; num73++)
							{
								Player player = Main.player[num73];
								if (attackCD != 0 || num73 == i || !player.active || !player.hostile || player.immune || player.dead || (Main.player[i].team != 0 && Main.player[i].team == player.team))
								{
									continue;
								}
								value8.X = (int)player.position.X;
								value8.Y = (int)player.position.Y;
								value8.Width = player.width;
								value8.Height = player.height;
								if (!rectangle.Intersects(value8))
								{
									continue;
								}
								int num74 = 0;
								if (Codable.RunPlayerMethod("CanHitboxDamagePlayer", true, this, rectangle, player) && Codable.customMethodReturn != null)
								{
									num74 = (((bool)Codable.customMethodReturn) ? 1 : 2);
								}
								if (num74 != 1 && (num74 == 2 || !Collision.CanHit(base.position, width, height, player.position, player.width, player.height)))
								{
									continue;
								}
								bool flag10 = false;
								if (Main.rand.Next(1, 101) <= 10)
								{
									flag10 = true;
								}
								if (Codable.RunPlayerMethodRef("DamagePVP", true, this, num65, player) && Codable.customMethodRefReturn != null)
								{
									num65 = (int)Codable.customMethodRefReturn[1];
								}
								int num75 = Main.DamageVar(num65);
								StatusPvP(item.type, num73);
								player.Hurt(num75, direction, pvp: true, quiet: false, "", flag10, meleeDamageCrit);
								Codable.RunPlayerMethod("DealtPVP", true, this, num75, player);
								if (Main.netMode != 0)
								{
									if (flag10)
									{
										NetMessage.SendData(26, -1, -1, Lang.deathMsg(whoAmi), num73, direction, meleeDamageCrit, 1f, num75);
									}
									else
									{
										NetMessage.SendData(26, -1, -1, Lang.deathMsg(whoAmi), num73, direction, 1f, 1f, num75);
									}
								}
								attackCD = (int)((float)itemAnimationMax * 0.33f);
							}
						}
					}
				}
			}
			if (itemTime == 0 && itemAnimation > 0)
			{
				if (item.RunMethod("UseItem", this, i))
				{
					itemTime = item.useTime;
				}
				if (item.healLife > 0)
				{
					statLife += item.healLife;
					itemTime = item.useTime;
					if (Main.myPlayer == whoAmi)
					{
						HealEffect(item.healLife);
					}
				}
				if (item.healMana > 0)
				{
					statMana += item.healMana;
					itemTime = item.useTime;
					if (Main.myPlayer == whoAmi)
					{
						ManaEffect(item.healMana);
					}
				}
				if (item.buffType > 0)
				{
					if (whoAmi == Main.myPlayer)
					{
						AddBuff(item.buffType, item.buffTime);
					}
					itemTime = item.useTime;
				}
			}
			if (whoAmi == Main.myPlayer && itemAnimation > 0 && itemTime == 0)
			{
				if (item.type == 361)
				{
					itemTime = item.useTime;
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					if (Main.netMode != 1)
					{
						if (Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion();
						}
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", whoAmi, -1f);
					}
				}
				else if (item.type == 602)
				{
					itemTime = item.useTime;
					Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
					if (Main.netMode != 1)
					{
						if (Main.invasionType == 0)
						{
							Main.invasionDelay = 0;
							Main.StartInvasion(2);
						}
					}
					else
					{
						NetMessage.SendData(61, -1, -1, "", whoAmi, -2f);
					}
				}
				else if (item.type == 43 || item.type == 70 || item.type == 544 || item.type == 556 || item.type == 557 || item.type == 560)
				{
					for (int num76 = 0; num76 < Main.npc.Length; num76++)
					{
						if (Main.npc[num76].active && ((item.type == 43 && Main.npc[num76].type == 4) || (item.type == 70 && Main.npc[num76].type == 13) || (item.type == 560 && Main.npc[num76].type == 50) || (item.type == 544 && Main.npc[num76].type == 125) || (item.type == 544 && Main.npc[num76].type == 126) || (item.type == 556 && Main.npc[num76].type == 134) || (item.type == 557 && Main.npc[num76].type == 128)))
						{
							itemTime = item.useTime;
							break;
						}
					}
					if (item.type == 560)
					{
						itemTime = item.useTime;
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						if (Main.netMode != 1)
						{
							NPC.SpawnOnPlayer(i, 50);
						}
						else
						{
							NetMessage.SendData(61, -1, -1, "", whoAmi, 50f);
						}
					}
					else if (item.type == 43)
					{
						if (!Main.dayTime)
						{
							itemTime = item.useTime;
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 4);
							}
							else
							{
								NetMessage.SendData(61, -1, -1, "", whoAmi, 4f);
							}
						}
					}
					else if (item.type == 70)
					{
						if (zoneEvil)
						{
							itemTime = item.useTime;
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 13);
							}
							else
							{
								NetMessage.SendData(61, -1, -1, "", whoAmi, 13f);
							}
						}
					}
					else if (item.type == 544)
					{
						if (!Main.dayTime)
						{
							itemTime = item.useTime;
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 125);
								NPC.SpawnOnPlayer(i, 126);
							}
							else
							{
								NetMessage.SendData(61, -1, -1, "", whoAmi, 125f);
								NetMessage.SendData(61, -1, -1, "", whoAmi, 126f);
							}
						}
					}
					else if (item.type == 556)
					{
						if (!Main.dayTime)
						{
							itemTime = item.useTime;
							Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 134);
							}
							else
							{
								NetMessage.SendData(61, -1, -1, "", whoAmi, 134f);
							}
						}
					}
					else if (item.type == 557 && !Main.dayTime)
					{
						itemTime = item.useTime;
						Main.PlaySound(15, (int)base.position.X, (int)base.position.Y, 0);
						if (Main.netMode != 1)
						{
							NPC.SpawnOnPlayer(i, 127);
						}
						else
						{
							NetMessage.SendData(61, -1, -1, "", whoAmi, 127f);
						}
					}
				}
			}
			if (item.type == 50 && itemAnimation > 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					Dust.NewDust(base.position, width, height, 15, 0f, 0f, 150, Color.Transparent, 1.1f);
				}
				if (itemTime == 0)
				{
					itemTime = item.useTime;
				}
				else if (itemTime == item.useTime / 2)
				{
					for (int num77 = 0; num77 < 70; num77++)
					{
						Dust.NewDust(base.position, width, height, 15, velocity.X * 0.5f, velocity.Y * 0.5f, 150, Color.Transparent, 1.5f);
					}
					grappling[0] = -1;
					grapCount = 0;
					for (int num78 = 0; num78 < 1000; num78++)
					{
						if (Main.projectile[num78].active && Main.projectile[num78].owner == i && Main.projectile[num78].aiStyle == 7)
						{
							Main.projectile[num78].Kill();
						}
					}
					Spawn();
					for (int num79 = 0; num79 < 70; num79++)
					{
						Dust.NewDust(base.position, width, height, 15, 0f, 0f, 150, Color.Transparent, 1.5f);
					}
				}
			}
			if (i == Main.myPlayer)
			{
				if (itemTime == item.useTime && item.consumable)
				{
					bool flag11 = true;
					if (item.ranged)
					{
						if (ammoCost80 && Main.rand.Next(5) == 0)
						{
							flag11 = false;
						}
						if (ammoCost75 && Main.rand.Next(4) == 0)
						{
							flag11 = false;
						}
					}
					if (flag11)
					{
						if (item.stack > 0)
						{
							item.stack--;
						}
						if (item.stack <= 0)
						{
							itemTime = itemAnimation;
						}
					}
				}
				if (item.stack <= 0 && itemAnimation == 0)
				{
					inventory[selectedItem] = new Item();
				}
				if (selectedItem == 48)
				{
					if (itemAnimation == 0)
					{
						item.RunMethod("PostItemCheck", this, i);
						return;
					}
					Main.mouseItem = (Item)item.Clone();
				}
			}
			item.RunMethod("PostItemCheck", this, i);
		}

		public Color GetImmuneAlpha(Color newColor)
		{
			if (immuneAlpha > 125)
			{
				return Color.Transparent;
			}
			return GetImmuneAlpha2(newColor);
		}

		public Color GetImmuneAlpha2(Color newColor)
		{
			float num = (255f - (float)immuneAlpha) / 255f;
			if (shadow > 0f)
			{
				num *= 1f - shadow;
			}
			if (num < 0f)
			{
				num = 0f;
			}
			else if (num > 255f)
			{
				num = 255f;
			}
			return newColor * num;
		}

		public Color GetDeathAlpha(Color newColor)
		{
			int r = newColor.R + (int)((float)immuneAlpha * 0.9f);
			int g = newColor.G + (int)((float)immuneAlpha * 0.5f);
			int b = newColor.B + (int)((float)immuneAlpha * 0.5f);
			int num = newColor.A + (int)((float)immuneAlpha * 0.4f);
			if (num < 0)
			{
				num = 0;
			}
			else if (num > 255)
			{
				num = 255;
			}
			return new Color(r, g, b, num);
		}

		public void DropCoins()
		{
			for (int i = 0; i < 49; i++)
			{
				if (inventory[i].type >= 71 && inventory[i].type <= 74)
				{
					int num = Item.NewItem((int)position.X, (int)position.Y, width, height, inventory[i].type);
					int num2 = inventory[i].stack - inventory[i].stack / 2;
					Main.item[num].stack = num2;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					inventory[i].stack -= num2;
					if (inventory[i].stack <= 0)
					{
						inventory[i] = new Item();
					}
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num);
					}
					if (i == 48)
					{
						Main.mouseItem = (Item)inventory[i].Clone();
					}
				}
			}
		}

		public void DropItems()
		{
			for (int i = 0; i < 49; i++)
			{
				if (inventory[i].stack > 0 && inventory[i].name != "Copper Pickaxe" && inventory[i].name != "Copper Axe" && inventory[i].name != "Copper Shortsword")
				{
					int num = Item.NewItem((int)position.X, (int)position.Y, width, height, inventory[i]);
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num);
					}
				}
				inventory[i] = new Item();
				if (i >= 11)
				{
					continue;
				}
				armor[i].RunMethod("OnUnequip", this, i);
				if (armor[i].stack > 0)
				{
					int num2 = Item.NewItem((int)position.X, (int)position.Y, width, height, armor[i]);
					Main.item[num2].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num2].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num2].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num2);
					}
				}
				armor[i] = new Item();
			}
			inventory[0].SetDefaults("Copper Shortsword");
			inventory[0].Prefix(-1);
			inventory[1].SetDefaults("Copper Pickaxe");
			inventory[1].Prefix(-1);
			inventory[2].SetDefaults("Copper Axe");
			inventory[2].Prefix(-1);
			Main.mouseItem = new Item();
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public object clientClone()
		{
			Player player = new Player();
			foreach (string key in Biome.Biomes.Keys)
			{
				player.zone[key] = zone[key];
			}
			player.zoneEvil = zoneEvil;
			player.zoneMeteor = zoneMeteor;
			player.zoneDungeon = zoneDungeon;
			player.zoneJungle = zoneJungle;
			player.zoneHoly = zoneHoly;
			player.direction = direction;
			player.selectedItem = selectedItem;
			player.controlUp = controlUp;
			player.controlDown = controlDown;
			player.controlLeft = controlLeft;
			player.controlRight = controlRight;
			player.controlJump = controlJump;
			player.controlUseItem = controlUseItem;
			player.statLife = statLife;
			player.statLifeMax = statLifeMax;
			player.statMana = statMana;
			player.statManaMax = statManaMax;
			player.position = position;
			player.chest = chest;
			player.talkNPC = talkNPC;
			for (int i = 0; i < 49; i++)
			{
				player.inventory[i] = (Item)inventory[i].Clone();
				if (i < 11)
				{
					player.armor[i] = (Item)armor[i].Clone();
				}
			}
			for (int j = 0; j < buffType.Length; j++)
			{
				player.buffType[j] = buffType[j];
				player.buffTime[j] = buffTime[j];
			}
			return player;
		}

		public static void EncryptFile(MemoryStream inputStream, MemoryStream outputStream)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			using (CryptoStream cryptoStream = new CryptoStream(outputStream, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write))
			{
				cryptoStream.Write(inputStream.ToArray(), 0, inputStream.ToArray().Length);
				cryptoStream.Close();
			}
		}

		public static void EncryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(outputFile, FileMode.Create);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
			FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
			int num;
			while ((num = fileStream2.ReadByte()) != -1)
			{
				cryptoStream.WriteByte((byte)num);
			}
			fileStream2.Close();
			cryptoStream.Close();
			fileStream.Close();
		}

		public static void DecryptFile(MemoryStream stream, MemoryStream outputStream)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			using (CryptoStream cryptoStream = new CryptoStream(stream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read))
			{
				outputStream.Position = 0L;
				int num;
				while ((num = cryptoStream.ReadByte()) != -1)
				{
					outputStream.WriteByte((byte)num);
				}
				cryptoStream.Close();
				outputStream.Position = 0L;
			}
		}

		public static bool DecryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(inputFile, FileMode.Open);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
			FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
			try
			{
				int num;
				while ((num = cryptoStream.ReadByte()) != -1)
				{
					fileStream2.WriteByte((byte)num);
				}
				fileStream2.Close();
				cryptoStream.Close();
				fileStream.Close();
			}
			catch
			{
				fileStream2.Close();
				fileStream.Close();
				File.Delete(outputFile);
				return true;
			}
			return false;
		}

		public static bool CheckSpawn(int x, int y)
		{
			if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesX - 10)
			{
				return false;
			}
			if (Main.tile[x, y - 1] == null)
			{
				return false;
			}
			if (!Main.tile[x, y - 1].active || (Main.tile[x, y - 1].type != 79 && !Config.tileDefs.spawnPoint[Main.tile[x, y - 1].type]))
			{
				return false;
			}
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 3; j < y; j++)
				{
					if (Main.tile[i, j] == null)
					{
						return false;
					}
					if (Main.tile[i, j].active && Main.tileSolid[Main.tile[i, j].type] && !Main.tileSolidTop[Main.tile[i, j].type])
					{
						return false;
					}
				}
			}
			return WorldGen.StartRoomCheck(x, y - 1);
		}

		public void FindSpawn()
		{
			int num = 0;
			while (true)
			{
				if (num < spN.Length)
				{
					if (spN[num] == null)
					{
						SpawnX = -1;
						SpawnY = -1;
						return;
					}
					if (spN[num] == Main.worldName && spI[num] == Main.worldID)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			SpawnX = spX[num];
			SpawnY = spY[num];
		}

		public void ChangeSpawn(int x, int y)
		{
			for (int i = 0; i < spN.Length && spN[i] != null; i++)
			{
				if (spN[i] == Main.worldName && spI[i] == Main.worldID)
				{
					for (int num = i; num > 0; num--)
					{
						spN[num] = spN[num - 1];
						spI[num] = spI[num - 1];
						spX[num] = spX[num - 1];
						spY[num] = spY[num - 1];
					}
					spN[0] = Main.worldName;
					spI[0] = Main.worldID;
					spX[0] = x;
					spY[0] = y;
					return;
				}
			}
			for (int num2 = spN.Length - 1; num2 > 0; num2--)
			{
				if (spN[num2 - 1] != null)
				{
					spN[num2] = spN[num2 - 1];
					spI[num2] = spI[num2 - 1];
					spX[num2] = spX[num2 - 1];
					spY[num2] = spY[num2 - 1];
				}
			}
			spN[0] = Main.worldName;
			spI[0] = Main.worldID;
			spX[0] = x;
			spY[0] = y;
		}

		public static void savePlayerZip(Player newPlayer, string playerPath)
		{
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Expected O, but got Unknown
			try
			{
				Directory.CreateDirectory(Main.PlayerPath);
			}
			catch
			{
			}
			if (playerPath == null || playerPath == "")
			{
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			MemoryStream memoryStream3 = new MemoryStream();
			MemoryStream memoryStream4 = new MemoryStream();
			MemoryStream memoryStream5 = new MemoryStream();
			MemoryStream memoryStream6 = new MemoryStream();
			MemoryStream memoryStream7 = new MemoryStream();
			ZipFile val = (ZipFile)(object)new ZipFile();
			Config.SaveBuffSaveData(newPlayer, memoryStream2);
			Config.SavePlayerItemNames(memoryStream4);
			Prefix.SavePrefixNames(memoryStream7);
			Config.SaveModVersion(memoryStream6);
			Config.SaveGlobalModData(memoryStream3, "ModPlayer");
			savePlayerStream(newPlayer, memoryStream, memoryStream5);
			val.AddEntry("player.plr", memoryStream.ToArray());
			val.AddEntry("player.plr.buff.moddata", memoryStream2.ToArray());
			val.AddEntry("player.plr.global.moddata", memoryStream3.ToArray());
			val.AddEntry("player.plr.items.ini", memoryStream4.ToArray());
			val.AddEntry("player.plr.items.moddata", memoryStream5.ToArray());
			val.AddEntry("player.plr.version.moddata", memoryStream6.ToArray());
			val.AddEntry("player.plr.prefixes.ini", memoryStream7.ToArray());
			memoryStream.Dispose();
			memoryStream2.Dispose();
			memoryStream3.Dispose();
			memoryStream4.Dispose();
			memoryStream5.Dispose();
			memoryStream6.Dispose();
			memoryStream7.Dispose();
			if (playerPath.IndexOf(".zip") == -1)
			{
				Directory.CreateDirectory(Path.Combine(Main.PlayerPath, "plrbackups"));
				string[] files = Directory.GetFiles(Main.PlayerPath, Path.GetFileName(playerPath) + "*");
				string[] array = files;
				foreach (string text in array)
				{
					string destFileName = Path.Combine(Main.PlayerPath, "plrbackups", Path.GetFileName(text));
					File.Copy(text, destFileName, overwrite: true);
				}
				File.Delete(playerPath);
				if (File.Exists(playerPath + ".bak"))
				{
					File.Delete(playerPath + ".bak");
				}
				Config.DeletePlayer(playerPath);
				Main.playerPathName = playerPath + ".zip";
			}
			else if (File.Exists(playerPath))
			{
				Main.statusText = Lang.gen[50];
				string destFileName2 = playerPath + ".bak";
				File.Copy(playerPath, destFileName2, overwrite: true);
			}
			string text2 = newPlayer.name + ".zip";
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			foreach (char oldChar in invalidFileNameChars)
			{
				text2 = text2.Replace(oldChar, '_');
			}
			val.Save(Path.Combine(Main.PlayerPath, text2));
			val.Dispose();
		}

		public static void savePlayerStream(Player newPlayer, MemoryStream stream, MemoryStream itemData)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(Main.curRelease);
			binaryWriter.Write(newPlayer.name);
			binaryWriter.Write(newPlayer.difficulty);
			binaryWriter.Write(newPlayer.hair);
			binaryWriter.Write(newPlayer.male);
			binaryWriter.Write(newPlayer.statLife);
			binaryWriter.Write(newPlayer.statLifeMax);
			binaryWriter.Write(newPlayer.statMana);
			binaryWriter.Write(newPlayer.statManaMax);
			binaryWriter.Write(newPlayer.hairColor.R);
			binaryWriter.Write(newPlayer.hairColor.G);
			binaryWriter.Write(newPlayer.hairColor.B);
			binaryWriter.Write(newPlayer.skinColor.R);
			binaryWriter.Write(newPlayer.skinColor.G);
			binaryWriter.Write(newPlayer.skinColor.B);
			binaryWriter.Write(newPlayer.eyeColor.R);
			binaryWriter.Write(newPlayer.eyeColor.G);
			binaryWriter.Write(newPlayer.eyeColor.B);
			binaryWriter.Write(newPlayer.shirtColor.R);
			binaryWriter.Write(newPlayer.shirtColor.G);
			binaryWriter.Write(newPlayer.shirtColor.B);
			binaryWriter.Write(newPlayer.underShirtColor.R);
			binaryWriter.Write(newPlayer.underShirtColor.G);
			binaryWriter.Write(newPlayer.underShirtColor.B);
			binaryWriter.Write(newPlayer.pantsColor.R);
			binaryWriter.Write(newPlayer.pantsColor.G);
			binaryWriter.Write(newPlayer.pantsColor.B);
			binaryWriter.Write(newPlayer.shoeColor.R);
			binaryWriter.Write(newPlayer.shoeColor.G);
			binaryWriter.Write(newPlayer.shoeColor.B);
			BinaryWriter binaryWriter2 = new BinaryWriter(itemData);
			for (int i = 0; i < 11; i++)
			{
				if (newPlayer.armor[i].name == null)
				{
					newPlayer.armor[i].name = "";
				}
				binaryWriter.Write(newPlayer.armor[i].netID);
				Prefix.SavePrefix(binaryWriter, newPlayer.armor[i]);
				Codable.SaveCustomData(newPlayer.armor[i], binaryWriter2);
			}
			for (int j = 0; j < 48; j++)
			{
				if (newPlayer.inventory[j].name == null)
				{
					newPlayer.inventory[j].name = "";
				}
				binaryWriter.Write(newPlayer.inventory[j].netID);
				binaryWriter.Write(newPlayer.inventory[j].stack);
				Prefix.SavePrefix(binaryWriter, newPlayer.inventory[j]);
				Codable.SaveCustomData(newPlayer.inventory[j], binaryWriter2);
			}
			for (int k = 0; k < Chest.maxItems; k++)
			{
				if (newPlayer.bank[k].name == null)
				{
					newPlayer.bank[k].name = "";
				}
				binaryWriter.Write(newPlayer.bank[k].netID);
				binaryWriter.Write(newPlayer.bank[k].stack);
				Prefix.SavePrefix(binaryWriter, newPlayer.bank[k]);
				Codable.SaveCustomData(newPlayer.bank[k], binaryWriter2);
			}
			for (int l = 0; l < Chest.maxItems; l++)
			{
				if (newPlayer.bank2[l].name == null)
				{
					newPlayer.bank2[l].name = "";
				}
				binaryWriter.Write(newPlayer.bank2[l].netID);
				binaryWriter.Write(newPlayer.bank2[l].stack);
				Prefix.SavePrefix(binaryWriter, newPlayer.bank2[l]);
				Codable.SaveCustomData(newPlayer.bank2[l], binaryWriter2);
			}
			binaryWriter2.Close();
			binaryWriter.Write(byte.MaxValue);
			binaryWriter.Write(Constants.version);
			binaryWriter.Write((byte)newPlayer.buffType.Length);
			for (int m = 0; m < newPlayer.buffType.Length; m++)
			{
				binaryWriter.Write(newPlayer.buffType[m]);
				binaryWriter.Write(newPlayer.buffTime[m]);
			}
			for (int n = 0; n < 200; n++)
			{
				if (newPlayer.spN[n] == null)
				{
					binaryWriter.Write(-1);
					break;
				}
				binaryWriter.Write(newPlayer.spX[n]);
				binaryWriter.Write(newPlayer.spY[n]);
				binaryWriter.Write(newPlayer.spI[n]);
				binaryWriter.Write(newPlayer.spN[n]);
			}
			binaryWriter.Write(newPlayer.hbLocked);
			memoryStream.Position = 0L;
			EncryptFile(memoryStream, stream);
			binaryWriter.Close();
		}

		public static void SavePlayer(Player newPlayer, string playerPath)
		{
			savePlayerZip(newPlayer, playerPath);
		}

		public static Player loadPlayerZip(string playerPath)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			MemoryStream memoryStream3 = new MemoryStream();
			MemoryStream memoryStream4 = new MemoryStream();
			MemoryStream memoryStream5 = new MemoryStream();
			MemoryStream memoryStream6 = new MemoryStream();
			MemoryStream memoryStream7 = new MemoryStream();
			ZipFile val = ZipFile.Read(playerPath);
			try
			{
				foreach (ZipEntry item in val)
				{
					switch (item.FileName.ToLower())
					{
					case "player.plr":
						item.Extract((Stream)memoryStream);
						break;
					case "player.plr.buff.moddata":
						item.Extract((Stream)memoryStream2);
						break;
					case "player.plr.global.moddata":
						item.Extract((Stream)memoryStream3);
						break;
					case "player.plr.items.ini":
						item.Extract((Stream)memoryStream4);
						break;
					case "player.plr.items.moddata":
						item.Extract((Stream)memoryStream5);
						break;
					case "player.plr.version.moddata":
						item.Extract((Stream)memoryStream6);
						break;
					case "player.plr.prefixes.ini":
						item.Extract((Stream)memoryStream7);
						break;
					}
				}
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
			memoryStream.Position = 0L;
			memoryStream2.Position = 0L;
			memoryStream3.Position = 0L;
			memoryStream4.Position = 0L;
			memoryStream5.Position = 0L;
			memoryStream6.Position = 0L;
			memoryStream7.Position = 0L;
			Config.LoadPlayerItemNames(memoryStream4);
			Prefix.LoadPrefixNames("player", memoryStream7);
			Config.LoadModVersion(memoryStream6);
			Config.LoadGlobalModData(memoryStream3, "ModPlayer");
			Player player = loadPlayerStream(memoryStream, memoryStream5, memoryStream2);
			Codable.RunGlobalMethod("ModPlayer", "PostLoad", player);
			for (int num = player.buffType.Length - 1; num >= 0; num--)
			{
				if (string.IsNullOrEmpty(Main.buffName[player.buffType[num]]))
				{
					player.DelBuff(num);
				}
			}
			return player;
		}

		public static Player loadPlayerStream(MemoryStream stream, MemoryStream itemData, MemoryStream buffData)
		{
			Player player = new Player();
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				DecryptFile(stream, memoryStream);
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					int num = binaryReader.ReadInt32();
					int num2 = 0;
					if (num >= 1376)
					{
						num2 = num - 1337 - 39 + 2;
						num = 39;
					}
					else
					{
						switch (num)
						{
						case -1:
							num = 37;
							num2 = 2;
							break;
						case -2:
							num = 37;
							num2 = 1;
							break;
						}
					}
					player.name = binaryReader.ReadString();
					if (num >= 10)
					{
						if (num >= 17)
						{
							player.difficulty = binaryReader.ReadByte();
						}
						else if (binaryReader.ReadBoolean())
						{
							player.difficulty = 2;
						}
					}
					player.hair = binaryReader.ReadInt32();
					if (num <= 17)
					{
						if (player.hair == 5 || player.hair == 6 || player.hair == 9 || player.hair == 11)
						{
							player.male = false;
						}
						else
						{
							player.male = true;
						}
					}
					else
					{
						player.male = binaryReader.ReadBoolean();
					}
					player.statLife = binaryReader.ReadInt32();
					player.statLifeMax = binaryReader.ReadInt32();
					player.statMana = binaryReader.ReadInt32();
					player.statManaMax = binaryReader.ReadInt32();
					player.hairColor.R = binaryReader.ReadByte();
					player.hairColor.G = binaryReader.ReadByte();
					player.hairColor.B = binaryReader.ReadByte();
					player.skinColor.R = binaryReader.ReadByte();
					player.skinColor.G = binaryReader.ReadByte();
					player.skinColor.B = binaryReader.ReadByte();
					player.eyeColor.R = binaryReader.ReadByte();
					player.eyeColor.G = binaryReader.ReadByte();
					player.eyeColor.B = binaryReader.ReadByte();
					player.shirtColor.R = binaryReader.ReadByte();
					player.shirtColor.G = binaryReader.ReadByte();
					player.shirtColor.B = binaryReader.ReadByte();
					player.underShirtColor.R = binaryReader.ReadByte();
					player.underShirtColor.G = binaryReader.ReadByte();
					player.underShirtColor.B = binaryReader.ReadByte();
					player.pantsColor.R = binaryReader.ReadByte();
					player.pantsColor.G = binaryReader.ReadByte();
					player.pantsColor.B = binaryReader.ReadByte();
					player.shoeColor.R = binaryReader.ReadByte();
					player.shoeColor.G = binaryReader.ReadByte();
					player.shoeColor.B = binaryReader.ReadByte();
					Main.player[Main.myPlayer].shirtColor = player.shirtColor;
					Main.player[Main.myPlayer].pantsColor = player.pantsColor;
					Main.player[Main.myPlayer].hairColor = player.hairColor;
					if (num >= 38)
					{
						BinaryReader sepReader = null;
						if (num2 >= 4 && itemData.Length > 0)
						{
							sepReader = new BinaryReader(itemData);
						}
						for (int i = 0; i < 11; i++)
						{
							Config.LoadItem(binaryReader, sepReader, num2, player.armor[i], loadStack: false);
						}
						for (int j = 0; j < 48; j++)
						{
							Config.LoadItem(binaryReader, sepReader, num2, player.inventory[j]);
						}
						for (int k = 0; k < Chest.maxItems; k++)
						{
							Config.LoadItem(binaryReader, sepReader, num2, player.bank[k]);
						}
						for (int l = 0; l < Chest.maxItems; l++)
						{
							Config.LoadItem(binaryReader, sepReader, num2, player.bank2[l]);
						}
					}
					else
					{
						for (int m = 0; m < 8; m++)
						{
							player.armor[m].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
							if (num >= 36)
							{
								player.armor[m].Prefix(binaryReader.ReadByte());
							}
							if (num2 >= 1)
							{
								Codable.LoadCustomData(player.armor[m], binaryReader, num2);
							}
						}
						if (num >= 6)
						{
							for (int n = 8; n < 11; n++)
							{
								player.armor[n].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								if (num >= 36)
								{
									player.armor[n].Prefix(binaryReader.ReadByte());
								}
								if (num2 >= 1)
								{
									Codable.LoadCustomData(player.armor[n], binaryReader, num2);
								}
							}
						}
						for (int num3 = 0; num3 < 44; num3++)
						{
							player.inventory[num3].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
							player.inventory[num3].stack = binaryReader.ReadInt32();
							if (num >= 36)
							{
								player.inventory[num3].Prefix(binaryReader.ReadByte());
							}
							if (num2 >= 1)
							{
								Codable.LoadCustomData(player.inventory[num3], binaryReader, num2);
							}
						}
						if (num >= 15)
						{
							for (int num4 = 44; num4 < 48; num4++)
							{
								player.inventory[num4].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.inventory[num4].stack = binaryReader.ReadInt32();
								if (num >= 36)
								{
									player.inventory[num4].Prefix(binaryReader.ReadByte());
								}
								if (num2 >= 1)
								{
									Codable.LoadCustomData(player.inventory[num4], binaryReader, num2);
								}
							}
						}
						for (int num5 = 0; num5 < Chest.maxItems; num5++)
						{
							player.bank[num5].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
							player.bank[num5].stack = binaryReader.ReadInt32();
							if (num >= 36)
							{
								player.bank[num5].Prefix(binaryReader.ReadByte());
							}
							if (num2 >= 1)
							{
								Codable.LoadCustomData(player.bank[num5], binaryReader, num2);
							}
						}
						if (num >= 20)
						{
							for (int num6 = 0; num6 < Chest.maxItems; num6++)
							{
								player.bank2[num6].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.bank2[num6].stack = binaryReader.ReadInt32();
								if (num >= 36)
								{
									player.bank2[num6].Prefix(binaryReader.ReadByte());
								}
								if (num2 >= 1)
								{
									Codable.LoadCustomData(player.bank2[num6], binaryReader, num2);
								}
							}
						}
					}
					if (num >= 11)
					{
						int num7 = 10;
						if (binaryReader.ReadByte() == byte.MaxValue)
						{
							binaryReader.ReadString();
							num7 = binaryReader.ReadByte();
						}
						else
						{
							binaryReader.BaseStream.Position--;
						}
						if (player.buffType.Length != num7)
						{
							Array.Resize(ref player.buffType, num7);
							Array.Resize(ref player.buffTime, num7);
							Array.Resize(ref player.buffCode, num7);
						}
						for (int num8 = 0; num8 < num7; num8++)
						{
							player.buffType[num8] = binaryReader.ReadInt32();
							player.buffTime[num8] = binaryReader.ReadInt32();
							if (!(Config.buffDefs.assemblyByType[player.buffType[num8]] == null))
							{
								player.buffCode[num8] = Config.buffDefs.assemblyByType[player.buffType[num8]].CreateInstance("Terraria." + Codable.ParseName(Main.buffName[player.buffType[num8]]) + "Buff");
							}
						}
					}
					Config.LoadBuffSaveData(player, buffData, num2);
					for (int num9 = 0; num9 < player.spN.Length; num9++)
					{
						int num10 = binaryReader.ReadInt32();
						if (num10 == -1)
						{
							break;
						}
						player.spX[num9] = num10;
						player.spY[num9] = binaryReader.ReadInt32();
						player.spI[num9] = binaryReader.ReadInt32();
						player.spN[num9] = binaryReader.ReadString();
					}
					if (num >= 16)
					{
						player.hbLocked = binaryReader.ReadBoolean();
					}
					binaryReader.Close();
				}
				player.PlayerFrame();
				return player;
			}
			catch (Exception)
			{
			}
			return new Player();
		}

		public static Player LoadPlayer(string playerPath)
		{
			if (playerPath.IndexOf(".zip") > -1)
			{
				return loadPlayerZip(playerPath);
			}
			bool flag = false;
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			Player player = new Player();
			Config.LoadPlayerItemNames(playerPath);
			Config.LoadModVersion(playerPath);
			Config.LoadGlobalModData(playerPath, "ModPlayer");
			try
			{
				string text = playerPath + ".dat";
				flag = DecryptFile(playerPath, text);
				if (!flag)
				{
					using (FileStream input = new FileStream(text, FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(input))
						{
							int num = binaryReader.ReadInt32();
							int num2 = 0;
							if (num >= 1376)
							{
								num2 = num - 1337 - 39 + 2;
								num = 39;
							}
							else
							{
								switch (num)
								{
								case -1:
									num = 37;
									num2 = 2;
									break;
								case -2:
									num = 37;
									num2 = 1;
									break;
								}
							}
							player.name = binaryReader.ReadString();
							if (num >= 10)
							{
								if (num >= 17)
								{
									player.difficulty = binaryReader.ReadByte();
								}
								else if (binaryReader.ReadBoolean())
								{
									player.difficulty = 2;
								}
							}
							player.hair = binaryReader.ReadInt32();
							if (num <= 17)
							{
								if (player.hair == 5 || player.hair == 6 || player.hair == 9 || player.hair == 11)
								{
									player.male = false;
								}
								else
								{
									player.male = true;
								}
							}
							else
							{
								player.male = binaryReader.ReadBoolean();
							}
							player.statLife = binaryReader.ReadInt32();
							player.statLifeMax = binaryReader.ReadInt32();
							player.statMana = binaryReader.ReadInt32();
							player.statManaMax = binaryReader.ReadInt32();
							player.hairColor.R = binaryReader.ReadByte();
							player.hairColor.G = binaryReader.ReadByte();
							player.hairColor.B = binaryReader.ReadByte();
							player.skinColor.R = binaryReader.ReadByte();
							player.skinColor.G = binaryReader.ReadByte();
							player.skinColor.B = binaryReader.ReadByte();
							player.eyeColor.R = binaryReader.ReadByte();
							player.eyeColor.G = binaryReader.ReadByte();
							player.eyeColor.B = binaryReader.ReadByte();
							player.shirtColor.R = binaryReader.ReadByte();
							player.shirtColor.G = binaryReader.ReadByte();
							player.shirtColor.B = binaryReader.ReadByte();
							player.underShirtColor.R = binaryReader.ReadByte();
							player.underShirtColor.G = binaryReader.ReadByte();
							player.underShirtColor.B = binaryReader.ReadByte();
							player.pantsColor.R = binaryReader.ReadByte();
							player.pantsColor.G = binaryReader.ReadByte();
							player.pantsColor.B = binaryReader.ReadByte();
							player.shoeColor.R = binaryReader.ReadByte();
							player.shoeColor.G = binaryReader.ReadByte();
							player.shoeColor.B = binaryReader.ReadByte();
							Main.player[Main.myPlayer].shirtColor = player.shirtColor;
							Main.player[Main.myPlayer].pantsColor = player.pantsColor;
							Main.player[Main.myPlayer].hairColor = player.hairColor;
							if (num >= 38)
							{
								string path = playerPath + ".items.moddata";
								BinaryReader binaryReader2 = null;
								if (num2 >= 4 && File.Exists(path))
								{
									binaryReader2 = new BinaryReader(new FileStream(path, FileMode.Open));
								}
								for (int i = 0; i < 11; i++)
								{
									Config.LoadItem(playerPath, binaryReader, binaryReader2, num2, player.armor[i], loadStack: false);
								}
								for (int j = 0; j < 48; j++)
								{
									Config.LoadItem(playerPath, binaryReader, binaryReader2, num2, player.inventory[j]);
								}
								for (int k = 0; k < Chest.maxItems; k++)
								{
									Config.LoadItem(playerPath, binaryReader, binaryReader2, num2, player.bank[k]);
								}
								for (int l = 0; l < Chest.maxItems; l++)
								{
									Config.LoadItem(playerPath, binaryReader, binaryReader2, num2, player.bank2[l]);
								}
								if (num2 >= 4)
								{
									binaryReader2?.Close();
								}
							}
							else
							{
								for (int m = 0; m < 8; m++)
								{
									player.armor[m].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									if (num >= 36)
									{
										player.armor[m].Prefix(binaryReader.ReadByte());
									}
									if (num2 >= 1)
									{
										Codable.LoadCustomData(player.armor[m], binaryReader, num2);
									}
								}
								if (num >= 6)
								{
									for (int n = 8; n < 11; n++)
									{
										player.armor[n].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
										if (num >= 36)
										{
											player.armor[n].Prefix(binaryReader.ReadByte());
										}
										if (num2 >= 1)
										{
											Codable.LoadCustomData(player.armor[n], binaryReader, num2);
										}
									}
								}
								for (int num3 = 0; num3 < 44; num3++)
								{
									player.inventory[num3].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.inventory[num3].stack = binaryReader.ReadInt32();
									if (num >= 36)
									{
										player.inventory[num3].Prefix(binaryReader.ReadByte());
									}
									if (num2 >= 1)
									{
										Codable.LoadCustomData(player.inventory[num3], binaryReader, num2);
									}
								}
								if (num >= 15)
								{
									for (int num4 = 44; num4 < 48; num4++)
									{
										player.inventory[num4].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
										player.inventory[num4].stack = binaryReader.ReadInt32();
										if (num >= 36)
										{
											player.inventory[num4].Prefix(binaryReader.ReadByte());
										}
										if (num2 >= 1)
										{
											Codable.LoadCustomData(player.inventory[num4], binaryReader, num2);
										}
									}
								}
								for (int num5 = 0; num5 < Chest.maxItems; num5++)
								{
									player.bank[num5].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.bank[num5].stack = binaryReader.ReadInt32();
									if (num >= 36)
									{
										player.bank[num5].Prefix(binaryReader.ReadByte());
									}
									if (num2 >= 1)
									{
										Codable.LoadCustomData(player.bank[num5], binaryReader, num2);
									}
								}
								if (num >= 20)
								{
									for (int num6 = 0; num6 < Chest.maxItems; num6++)
									{
										player.bank2[num6].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
										player.bank2[num6].stack = binaryReader.ReadInt32();
										if (num >= 36)
										{
											player.bank2[num6].Prefix(binaryReader.ReadByte());
										}
										if (num2 >= 1)
										{
											Codable.LoadCustomData(player.bank2[num6], binaryReader, num2);
										}
									}
								}
							}
							if (num >= 11)
							{
								for (int num7 = 0; num7 < player.buffType.Length; num7++)
								{
									player.buffType[num7] = binaryReader.ReadInt32();
									player.buffTime[num7] = binaryReader.ReadInt32();
								}
							}
							Config.LoadBuffSaveData(player, playerPath, num2);
							for (int num8 = 0; num8 < player.spN.Length; num8++)
							{
								int num9 = binaryReader.ReadInt32();
								if (num9 == -1)
								{
									break;
								}
								player.spX[num8] = num9;
								player.spY[num8] = binaryReader.ReadInt32();
								player.spI[num8] = binaryReader.ReadInt32();
								player.spN[num8] = binaryReader.ReadString();
							}
							if (num >= 16)
							{
								player.hbLocked = binaryReader.ReadBoolean();
							}
							binaryReader.Close();
						}
					}
					player.PlayerFrame();
					File.Delete(text);
					return player;
				}
			}
			catch
			{
				flag = true;
			}
			if (flag)
			{
				try
				{
					string text2 = playerPath + ".bak";
					if (File.Exists(text2))
					{
						File.Delete(playerPath);
						File.Move(text2, playerPath);
						return LoadPlayer(playerPath);
					}
					return new Player();
				}
				catch
				{
					return new Player();
				}
			}
			return new Player();
		}

		public bool HasItem(int netID, int stack = -1)
		{
			int num = 0;
			for (int i = 0; i < 48; i++)
			{
				if (netID == inventory[i].netID)
				{
					if (stack == -1)
					{
						return true;
					}
					num += inventory[i].stack;
				}
			}
			if (stack != -1 && num >= stack)
			{
				return true;
			}
			return false;
		}

		public bool HasItem(string name, int stack = -1)
		{
			int num = 0;
			int netID = Config.itemDefs.byName[name].netID;
			for (int i = 0; i < 48; i++)
			{
				if (netID == inventory[i].netID)
				{
					if (stack == -1)
					{
						return true;
					}
					num += inventory[i].stack;
				}
			}
			if (stack != -1 && num >= stack)
			{
				return true;
			}
			return false;
		}

		public void QuickGrapple()
		{
			if ((Codable.RunPlayerMethod("PreQuickGrapple", false, this) && !(bool)Codable.customMethodReturn) || noItems)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < 48; i++)
			{
				if (inventory[i].shoot == 13 || inventory[i].shoot == 32 || inventory[i].shoot == 73)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			if (inventory[num].shoot == 73)
			{
				int num2 = 0;
				if (num >= 0)
				{
					for (int j = 0; j < Main.projectile.Length; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && (Main.projectile[j].type == 73 || Main.projectile[j].type == 74))
						{
							num2++;
						}
					}
				}
				if (num2 > 1)
				{
					num = -1;
				}
			}
			else if (num >= 0)
			{
				for (int k = 0; k < Main.projectile.Length; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == Main.myPlayer && Main.projectile[k].type == inventory[num].shoot && Main.projectile[k].ai[0] != 2f)
					{
						num = -1;
						break;
					}
				}
			}
			if (num < 0)
			{
				return;
			}
			Main.PlaySound(2, (int)position.X, (int)position.Y, inventory[num].useSound);
			if (Main.netMode == 1 && whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(51, -1, -1, "", whoAmi, 2f);
			}
			int num3 = inventory[num].shoot;
			float shootSpeed = inventory[num].shootSpeed;
			int damage = inventory[num].damage;
			float knockBack = inventory[num].knockBack;
			if (num3 == 13 || num3 == 32)
			{
				grappling[0] = -1;
				grapCount = 0;
				for (int l = 0; l < Main.projectile.Length; l++)
				{
					if (Main.projectile[l].active && Main.projectile[l].owner == whoAmi && Main.projectile[l].type == 13)
					{
						Main.projectile[l].Kill();
					}
				}
			}
			if (num3 == 73)
			{
				for (int m = 0; m < Main.projectile.Length; m++)
				{
					if (Main.projectile[m].active && Main.projectile[m].owner == whoAmi && Main.projectile[m].type == 73)
					{
						num3 = 74;
					}
				}
			}
			Vector2 vector = new Vector2(position.X + (float)width * 0.5f, position.Y + (float)height * 0.5f);
			float num4 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
			float num5 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
			float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
			num6 = shootSpeed / num6;
			num4 *= num6;
			num5 *= num6;
			Projectile.NewProjectile(vector.X, vector.Y, num4, num5, num3, damage, knockBack, whoAmi);
		}

		public Player(bool modsLoaded = false)
		{
			width = 20;
			height = 42;
			base.gravDir = 1f;
			direction = 1;
			for (int i = 0; i < 49; i++)
			{
				if (i < 11)
				{
					armor[i] = new Item();
					armor[i].name = "";
				}
				inventory[i] = new Item();
				inventory[i].name = "";
			}
			for (int j = 0; j < Chest.maxItems; j++)
			{
				bank[j] = new Item();
				bank[j].name = "";
				bank2[j] = new Item();
				bank2[j].name = "";
			}
			grappling[0] = -1;
			inventory[0].SetDefaults("Copper Shortsword");
			inventory[1].SetDefaults("Copper Pickaxe");
			inventory[2].SetDefaults("Copper Axe");
			if (Main.cEd)
			{
				inventory[3].SetDefaults(603);
			}
			for (int k = 0; k < 150 + Config.customTileAmt; k++)
			{
				adjTile[k] = false;
				oldAdjTile[k] = false;
			}
			foreach (Biome value in Biome.Biomes.Values)
			{
				zone.Add(value.Name, value: false);
			}
		}
	}
}
