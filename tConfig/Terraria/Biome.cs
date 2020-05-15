using System;
using System.Collections.Generic;

namespace Terraria
{
	public class Biome
	{
		public enum MusicPriority
		{
			None,
			Low,
			Med,
			High
		}

		public static Dictionary<string, Biome> Biomes = new Dictionary<string, Biome>();

		public static List<string> BiomeOrder = new List<string>();

		public static Dictionary<string, List<int>> SpawnList = new Dictionary<string, List<int>>();

		public List<int> typesIncrease;

		public List<int> typesReduce;

		public string Name;

		public int necessary;

		public Func<Player, int, int, bool> Validation;

		public Func<int, int, int, bool> TileValid;

		public Func<SoundHandler.Music> GetMusic;

		public MusicPriority biomeMusicPriority;

		public int biomeMusic;

		public bool isVanillaMusic = true;

		public Biome(string N, List<int> inc, List<int> dec, int nes)
		{
			Name = N;
			typesIncrease = inc;
			typesReduce = dec;
			necessary = nes;
			Func<Player, int, int, bool> func = Validation = ((Player P, int count, int nex) => Count());
			TileValid = ((int x, int y, int pid) => Main.player[pid].zone[Name]);
			GetMusic = (() => isVanillaMusic ? ((SoundHandler.Music)new SoundHandler.MusicVanilla(biomeMusic)) : ((SoundHandler.Music)new SoundHandler.MusicCustom(biomeMusic)));
		}

		public void SetBiomeMusic(MusicPriority priority, int musicID, bool isVanilla = false)
		{
			biomeMusicPriority = priority;
			biomeMusic = musicID;
			isVanillaMusic = isVanilla;
		}

		public static bool GetBiomeForMusic(MusicPriority priority, ref Biome biome)
		{
			foreach (string key in Main.player[Main.myPlayer].zone.Keys)
			{
				if (Main.player[Main.myPlayer].zone[key] && Biomes[key].biomeMusicPriority == priority)
				{
					biome = Biomes[key];
					return true;
				}
			}
			return false;
		}

		public bool Check(Player P)
		{
			return Validation(P, CountNum(), necessary);
		}

		public int CountNum()
		{
			int num = 0;
			foreach (int item in typesIncrease)
			{
				num += Main.tileCount[item];
			}
			foreach (int item2 in typesReduce)
			{
				num -= Main.tileCount[item2];
			}
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		public bool Count()
		{
			int num = 0;
			foreach (int item in typesIncrease)
			{
				num += Main.tileCount[item];
			}
			foreach (int item2 in typesReduce)
			{
				num -= Main.tileCount[item2];
			}
			if (num < 0)
			{
				num = 0;
			}
			if (num >= necessary)
			{
				return true;
			}
			return false;
		}

		public void AddToGame()
		{
			Biomes.Add(Name, this);
		}

		public static void InitBiomes()
		{
			Biomes = new Dictionary<string, Biome>();
			Biome biome = new Biome("Corruption", new List<int>
			{
				23,
				24,
				25,
				32
			}, new List<int>
			{
				27,
				27,
				27,
				27,
				27
			}, 200);
			biome.Validation = ((Player P, int count, int nex) => count - Biomes["Hallow"].CountNum() >= nex);
			biome.AddToGame();
			biome = new Biome("Hallow", new List<int>
			{
				109,
				110,
				113,
				116,
				117
			}, new List<int>(), 100);
			biome.Validation = ((Player P, int count, int nex) => count - Biomes["Corruption"].CountNum() >= nex);
			biome.AddToGame();
			biome = new Biome("Meteor", new List<int>
			{
				37
			}, new List<int>(), 50);
			biome.AddToGame();
			biome = new Biome("Jungle", new List<int>
			{
				60,
				61,
				62,
				84
			}, new List<int>(), 80);
			biome.AddToGame();
			biome = new Biome("Desert", new List<int>
			{
				53,
				112,
				116
			}, new List<int>(), 1000);
			biome.AddToGame();
			biome = new Biome("Snow", new List<int>
			{
				147,
				148
			}, new List<int>(), 400);
			biome.AddToGame();
			biome = new Biome("Dungeon", new List<int>
			{
				41,
				43,
				44
			}, new List<int>(), 250);
			biome.Validation = delegate(Player P, int count, int nex)
			{
				if (count >= nex && (double)P.position.Y > Main.worldSurface * 16.0)
				{
					int num = (int)P.position.X / 16;
					int num2 = (int)P.position.Y / 16;
					if (Main.tile[num, num2].wall > 0 && !Main.wallHouse[Main.tile[num, num2].wall])
					{
						return true;
					}
				}
				return false;
			};
			biome.AddToGame();
			biome = new Biome("Hell", new List<int>(), new List<int>(), 0);
			biome.Validation = ((Player P, int count, int nex) => (double)P.position.Y > Main.hellLayer * 16.0);
			biome.TileValid = ((int x, int y, int pid) => (double)y > Main.hellLayer);
			biome.AddToGame();
			biome = new Biome("RockLayer", new List<int>(), new List<int>(), 0);
			biome.Validation = ((Player P, int count, int nex) => (double)P.position.Y < Main.hellLayer * 16.0 && (double)P.position.Y > Main.rockLayer * 16.0);
			biome.TileValid = ((int x, int y, int pid) => (double)y < Main.hellLayer && (double)y > Main.rockLayer);
			biome.AddToGame();
			biome = new Biome("DirtLayer", new List<int>(), new List<int>(), 0);
			biome.Validation = ((Player P, int count, int nex) => (double)P.position.Y < Main.rockLayer * 16.0 && (double)P.position.Y > Main.worldSurface * 16.0);
			biome.TileValid = ((int x, int y, int pid) => (double)y < Main.rockLayer && (double)y > Main.worldSurface);
			biome.AddToGame();
			biome = new Biome("Glowshroom", new List<int>
			{
				70,
				71,
				72
			}, new List<int>(), 50);
			biome.AddToGame();
			biome = new Biome("Ocean", new List<int>(), new List<int>(), 0);
			biome.Validation = ((Player P, int count, int nex) => P.position.X < 4000f || P.position.X > (float)(Main.maxTilesX * 16 - 4000));
			biome.TileValid = ((int x, int y, int pid) => NPC.SpawnWater && (double)y < Main.rockLayer && (x < 250 || x > Main.maxTilesX - 250) && Main.tile[x, y].type == 53);
			biome.AddToGame();
			biome = new Biome("Sky", new List<int>(), new List<int>(), 0);
			biome.Validation = ((Player P, int count, int nex) => (double)(P.position.Y / 16f) < Main.worldSurface * 0.40000000596046448);
			biome.TileValid = delegate(int x, int y, int pid)
			{
				if ((double)y < Main.worldSurface * 0.34999999403953552 && ((float)x < (float)Main.maxTilesX * 0.45f || (float)x > (float)Main.maxTilesX * 0.55f || Main.hardMode))
				{
					return true;
				}
				return ((double)y < Main.worldSurface * 0.44999998807907104 && Main.hardMode && Main.rand.Next(10) == 0) ? true : false;
			};
			biome.AddToGame();
			biome = new Biome("Overworld", new List<int>(), new List<int>(), 0);
			biome.TileValid = delegate(int x, int y, int pid)
			{
				Player player = Main.player[pid];
				if (player.zone["Corruption"])
				{
					return false;
				}
				if (player.zone["Hallow"])
				{
					return false;
				}
				if (player.zone["Jungle"])
				{
					return false;
				}
				if (player.zone["Meteor"])
				{
					return false;
				}
				if (player.zone["Dungeon"])
				{
					return false;
				}
				if (player.zone["RockLayer"])
				{
					return false;
				}
				if (player.zone["DirtLayer"])
				{
					return false;
				}
				if (player.zone["Sky"])
				{
					return false;
				}
				if (player.zone["Ocean"])
				{
					return false;
				}
				if (player.zone["Hell"])
				{
					return false;
				}
				return (!player.zone["Glowshroom"]) ? true : false;
			};
			biome.AddToGame();
			BiomeOrder = new List<string>();
			BiomeOrder.Add("Overworld");
			SpawnList = new Dictionary<string, List<int>>();
			foreach (string key in Biomes.Keys)
			{
				SpawnList.Add(key, new List<int>());
			}
		}
	}
}
