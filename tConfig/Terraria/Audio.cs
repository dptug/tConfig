using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Terraria
{
	public abstract class Audio
	{
		public static class Player
		{
			public enum Priorities
			{
				None,
				Biome,
				Boss,
				Override
			}

			public static Song CurrentSong;

			public static float GameVolume = 0.75f;

			public static bool FadingIn = false;

			public static bool FadingOut = false;

			public static float MusicTimer = 0f;

			public static float MusicFadeDelay = 3f;

			public static string NextSong = null;

			public static Instantiater.Constructor<Song> LoadSong;

			public static float Delta = 0.0166666675f;

			internal static bool InactivePause = false;

			public static Priorities Priority = Priorities.None;

			private static bool IsPlaying = false;

			public static void Init()
			{
				if (!Main.dedServ && Main.engine != null)
				{
					GameVolume = Main.musicVolume;
					Stop(Priority);
					Priority = Priorities.None;
					FadingIn = false;
					FadingOut = false;
					ConstructorInfo constructor = typeof(Song).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[3]
					{
						typeof(string),
						typeof(string),
						typeof(int)
					}, null);
					LoadSong = Instantiater.GetActivator<Song>(constructor);
					if (CurrentSong != null)
					{
						CurrentSong.Dispose();
						CurrentSong = null;
					}
				}
			}

			public static void Shutdown()
			{
				if (!Main.dedServ && Main.engine != null)
				{
					Stop(Priority);
					if (CurrentSong != null)
					{
						CurrentSong.Dispose();
						CurrentSong = null;
					}
					NextSong = null;
				}
			}

			public static void Update()
			{
				if (!Main.dedServ && Main.engine != null)
				{
					if (FadingOut)
					{
						FadeOut();
					}
					if (FadingIn)
					{
						FadeIn();
					}
				}
			}

			public static void Play(string SongName, Priorities SongPriority = Priorities.None, bool Repeat = true)
			{
				if (Main.dedServ || Main.engine == null || SongPriority < Priority)
				{
					return;
				}
				if (FadingIn)
				{
					MediaPlayer.Volume = GameVolume;
				}
				FadingIn = false;
				FadingOut = false;
				Stop(Priority);
				Priority = SongPriority;
				if (CurrentSong != null)
				{
					CurrentSong.Dispose();
					CurrentSong = null;
				}
				if (SongName != null)
				{
					string text = (!Path.IsPathRooted(SongName)) ? (Config.tempModAssembly + "\\" + TMod.GetCurrentMod() + "\\" + SongName) : SongName;
					if (!Path.HasExtension(text))
					{
						string text2 = text + ".mp3";
						text = ((!File.Exists(text2)) ? (text + ".wav") : text2);
					}
					if (File.Exists(text) && GameVolume > 0f)
					{
						CurrentSong = LoadSong("", text, 0);
						MediaPlayer.IsRepeating = Repeat;
						MediaPlayer.Play(CurrentSong);
						IsPlaying = true;
						Main.musicVolume = 0f;
					}
					else
					{
						Priority = Priorities.None;
					}
				}
			}

			public static void Resume()
			{
				if (!Main.dedServ && Main.engine != null && !(CurrentSong == null) && !CurrentSong.IsDisposed)
				{
					MediaPlayer.Resume();
					IsPlaying = false;
				}
			}

			public static void Pause()
			{
				if (!Main.dedServ && Main.engine != null)
				{
					MediaPlayer.Pause();
					IsPlaying = false;
				}
			}

			public static void Stop(Priorities SongPriority = Priorities.None)
			{
				if (!Main.dedServ && Main.engine != null && SongPriority >= Priority)
				{
					Priority = Priorities.None;
					MediaPlayer.IsRepeating = false;
					MediaPlayer.Stop();
					IsPlaying = false;
					FadingIn = false;
					FadingOut = false;
					MusicTimer = 0f;
					Main.musicVolume = GameVolume;
					SetVolume(GameVolume);
				}
			}

			public static void FadeTo(string SongName, float FadeTime = 3f, Priorities SongPriority = Priorities.None)
			{
				if (Main.dedServ || Main.engine == null || SongPriority < Priority)
				{
					return;
				}
				Priority = SongPriority;
				string text = Config.tempModAssembly + "\\" + TMod.GetCurrentMod() + "\\" + SongName;
				if (!Path.HasExtension(text))
				{
					string text2 = text + ".mp3";
					text = ((!File.Exists(text2)) ? (text + ".wav") : text2);
				}
				if (SongName == null || File.Exists(text))
				{
					NextSong = text;
					MusicFadeDelay = FadeTime;
					FadingOut = true;
					if (FadingIn)
					{
						FadingIn = false;
						MusicTimer = MusicFadeDelay - MusicTimer;
					}
				}
			}

			private static void FadeIn()
			{
				if (Main.dedServ || Main.engine == null)
				{
					return;
				}
				float num = MathHelper.Lerp(0f, GameVolume, MusicTimer / MusicFadeDelay);
				if (CurrentSong != null)
				{
					SetVolume(num);
				}
				else
				{
					Main.musicVolume = num;
				}
				MusicTimer += Delta;
				if (MusicTimer >= MusicFadeDelay)
				{
					MusicTimer = 0f;
					if (CurrentSong != null)
					{
						SetVolume(GameVolume);
					}
					else
					{
						Main.musicVolume = GameVolume;
					}
					FadingIn = false;
				}
			}

			private static void FadeOut()
			{
				if (Main.dedServ || Main.engine == null)
				{
					return;
				}
				float num = MathHelper.Lerp(GameVolume, 0f, MusicTimer / MusicFadeDelay);
				if (CurrentSong != null)
				{
					SetVolume(num);
				}
				else
				{
					Main.musicVolume = num;
				}
				MusicTimer += Delta;
				if (MusicTimer >= MusicFadeDelay)
				{
					MusicTimer = 0f;
					if (CurrentSong != null)
					{
						SetVolume(0f);
					}
					else
					{
						Main.musicVolume = 0f;
					}
					FadingOut = false;
					Play(NextSong, Priority);
					FadingIn = true;
					NextSong = null;
				}
			}

			public static void SetVolume(float Volume)
			{
				if (!Main.dedServ && Main.engine != null)
				{
					MediaPlayer.Volume = Volume;
				}
			}

			public static bool Playing()
			{
				if (Main.dedServ || Main.engine == null)
				{
					return false;
				}
				return IsPlaying;
			}

			public static bool Paused()
			{
				if (Main.dedServ || Main.engine == null)
				{
					return false;
				}
				return MediaPlayer.State == MediaState.Paused;
			}
		}

		internal abstract class AudioBank
		{
			internal static Dictionary<string, IList<AudioBank>> BanksDict = new Dictionary<string, IList<AudioBank>>();

			internal List<Audio> AudioList = new List<Audio>();

			internal Dictionary<string, Audio> AudioDict = new Dictionary<string, Audio>();

			public int ID
			{
				get;
				protected set;
			}

			public string Name
			{
				get;
				protected set;
			}

			public Audio this[int El] => AudioList[El];

			public Audio this[string El] => AudioDict[El];

			internal abstract Audio GetTrack(string TrackName);

			internal abstract void Dispose();
		}

		internal const string LegacyName = "tConfig";

		internal AudioBank Bank;

		public int ID
		{
			get;
			protected set;
		}

		public int LocalID
		{
			get;
			protected set;
		}

		public string Name
		{
			get;
			protected set;
		}

		public abstract float Volume
		{
			get;
			set;
		}

		public abstract bool IsPlaying
		{
			get;
		}

		public static Music GetTrack(string TrackName, string ModName = null)
		{
			if (TrackName == null || Main.engine == null)
			{
				return null;
			}
			if (ModName == null)
			{
				ModName = TMod.GetCurrentMod();
			}
			if (!AudioBank.BanksDict.TryGetValue(ModName, out IList<AudioBank> value))
			{
				return null;
			}
			Audio audio = null;
			foreach (AudioBank item in value)
			{
				audio = item.GetTrack(TrackName);
				if (audio != null)
				{
					break;
				}
			}
			if (audio == null)
			{
				return GetLegacyTrack(TrackName);
			}
			return (Music)audio;
		}

		public static Music GetLegacyTrack(string TrackName)
		{
			if (TrackName == null || Main.engine == null)
			{
				return null;
			}
			IList<AudioBank> list = AudioBank.BanksDict["tConfig"];
			Audio audio = null;
			foreach (AudioBank item in list)
			{
				audio = item.GetTrack(TrackName);
				if (audio != null)
				{
					break;
				}
			}
			return (Music)audio;
		}

		internal static void ExtractMusic(SevenZipExtractor Ex, string FilePath, string ModName)
		{
			if (Main.dedServ || Main.engine == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < Ex.ArchiveFileNames.Count; i++)
			{
				if (Ex.ArchiveFileNames[i].EndsWith(".mp3") || Ex.ArchiveFileNames[i].EndsWith(".wma"))
				{
					Main.statusText = "Loading " + ModName + " music (" + (num + 1) + ")";
					num++;
					Ex.ExtractFiles(FilePath, new string[1]
					{
						Ex.ArchiveFileNames[i]
					});
				}
			}
		}

		public abstract void Stop();

		internal abstract void Dispose();
	}
}
