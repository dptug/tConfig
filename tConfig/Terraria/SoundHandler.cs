using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.IO;

namespace Terraria
{
	public class SoundHandler
	{
		public abstract class Music
		{
			public int ID
			{
				get;
				protected set;
			}

			public abstract void Play();

			public abstract void Pause();

			public abstract void Resume();

			public abstract void Stop();

			public abstract bool IsPlaying();

			public abstract bool IsPaused();

			public abstract bool IsStopped();

			public abstract float GetVolume();

			public abstract void SetVolume(float volume);

			public abstract bool Equals(Music music);
		}

		public class MusicVanilla : Music
		{
			public MusicVanilla(int id)
			{
				base.ID = id;
				if (id > 0 && Main.music[id] == null && Main.engine != null)
				{
					Main.music[id] = Main.soundBank.GetCue("Music_" + id);
				}
			}

			public override bool Equals(Music music)
			{
				if (music is MusicVanilla)
				{
					return music.ID == base.ID;
				}
				return false;
			}

			public override void Play()
			{
				if (base.ID > 0 && Main.engine != null)
				{
					Stop();
					Main.music[base.ID] = Main.soundBank.GetCue("Music_" + base.ID);
					Main.music[base.ID].Play();
				}
			}

			public override void Pause()
			{
				Main.music[base.ID].Pause();
			}

			public override void Resume()
			{
				Main.music[base.ID].Resume();
			}

			public override void Stop()
			{
				Main.music[base.ID].Stop(AudioStopOptions.Immediate);
			}

			public override bool IsPlaying()
			{
				return Main.music[base.ID].IsPlaying;
			}

			public override bool IsPaused()
			{
				return Main.music[base.ID].IsPaused;
			}

			public override bool IsStopped()
			{
				return Main.music[base.ID].IsStopped;
			}

			public override float GetVolume()
			{
				return Main.musicFade[base.ID];
			}

			public override void SetVolume(float volume)
			{
				Main.musicFade[base.ID] = volume;
				Main.music[base.ID].SetVariable("Volume", volume * Main.musicVolume);
			}
		}

		public class MusicCustom : Music
		{
			public int id;

			public MusicCustom(int id)
			{
				this.id = id;
			}

			public override bool Equals(Music music)
			{
				if (music is MusicCustom)
				{
					return ((MusicCustom)music).id == id;
				}
				return false;
			}

			public override void Play()
			{
				if (Main.engine != null)
				{
					customMusicInstance[id].Play();
				}
			}

			public override void Pause()
			{
				customMusicInstance[id].Pause();
			}

			public override void Resume()
			{
				customMusicInstance[id].Resume();
			}

			public override void Stop()
			{
				customMusicInstance[id].Stop();
			}

			public override bool IsPlaying()
			{
				return customMusicInstance[id].State == SoundState.Playing;
			}

			public override bool IsPaused()
			{
				return customMusicInstance[id].State == SoundState.Paused;
			}

			public override bool IsStopped()
			{
				return customMusicInstance[id].State == SoundState.Stopped;
			}

			public override float GetVolume()
			{
				return customMusicFade[id];
			}

			public override void SetVolume(float volume)
			{
				customMusicFade[id] = volume;
				customMusicInstance[id].Volume = volume * Main.musicVolume;
			}
		}

		public class MusicCustomBank : MusicCustom
		{
			public string trackName = "";

			public string modName = "";

			public MusicCustomBank(string audio, string mod)
				: base(-1)
			{
				if (!cueID.ContainsKey(mod + ":" + audio))
				{
					cueID[mod + ":" + audio] = cueCount;
					IDCue[cueCount] = mod + ":" + audio;
					cueCount++;
				}
				id = cueID[mod + ":" + audio];
				if (!customCueFade.ContainsKey(id))
				{
					customCueFade[id] = 0f;
				}
				trackName = audio;
				modName = mod;
				if (!customCueInstance.ContainsKey(id))
				{
					customCueInstance[id] = Audio.GetTrack(audio, mod).GetCue();
				}
			}

			public override bool Equals(Music music)
			{
				if (music is MusicCustomBank)
				{
					if (((MusicCustomBank)music).trackName == trackName)
					{
						return ((MusicCustomBank)music).modName == modName;
					}
					return false;
				}
				return false;
			}

			public override void Play()
			{
				if (Main.engine != null)
				{
					Stop();
					customCueInstance[id] = Audio.GetTrack(trackName, modName).GetCue();
					customCueInstance[id].SetVariable("Volume", MathHelper.Lerp(-90f, 0f, 1f) * Main.musicVolume);
					customCueInstance[id].Play();
				}
			}

			public override void Pause()
			{
				customCueInstance[id].Pause();
			}

			public override void Resume()
			{
				customCueInstance[id].Resume();
			}

			public override void Stop()
			{
				if (!customCueInstance[id].IsDisposed)
				{
					customCueInstance[id].Stop(AudioStopOptions.Immediate);
				}
			}

			public override bool IsPlaying()
			{
				return customCueInstance[id].IsPlaying;
			}

			public override bool IsPaused()
			{
				return customCueInstance[id].IsPaused;
			}

			public override bool IsStopped()
			{
				return customCueInstance[id].IsStopped;
			}

			public override float GetVolume()
			{
				return customCueFade[id];
			}

			public override void SetVolume(float volume)
			{
				customCueFade[id] = volume;
				customCueInstance[id].SetVariable("Volume", MathHelper.Lerp(-90f, 0f, volume) * Main.musicVolume);
			}
		}

		public static Dictionary<string, int> soundID;

		public static int soundCount = 300;

		public static Dictionary<int, SoundEffect> customSound;

		public static Dictionary<int, SoundEffectInstance> customSoundInstance;

		public static Music customMusic = null;

		public static Dictionary<int, SoundEffectInstance> customMusicInstance;

		public static Dictionary<int, float> customMusicFade;

		public SoundEffect[] sound;

		public static Dictionary<string, int> cueID;

		public static Dictionary<int, string> IDCue;

		public static int cueCount = 0;

		public static Dictionary<int, Cue> customCueInstance;

		public static Dictionary<int, float> customCueFade;

		public SoundEffect this[int i]
		{
			get
			{
				if (i >= sound.Length)
				{
					return customSound[i];
				}
				return sound[i];
			}
			set
			{
				if (i >= sound.Length)
				{
					customSound[i] = value;
				}
				else
				{
					sound[i] = value;
				}
			}
		}

		public static void Init()
		{
			soundID = new Dictionary<string, int>();
			soundCount = 300;
			customSound = new Dictionary<int, SoundEffect>();
			customSoundInstance = new Dictionary<int, SoundEffectInstance>();
			customMusic = null;
			customMusicInstance = new Dictionary<int, SoundEffectInstance>();
			customMusicFade = new Dictionary<int, float>();
			cueID = new Dictionary<string, int>();
			IDCue = new Dictionary<int, string>();
			customCueInstance = new Dictionary<int, Cue>();
			customCueFade = new Dictionary<int, float>();
		}

		public SoundHandler(int size)
		{
			sound = new SoundEffect[size];
		}

		public static void AddSound(string name, byte[] bytes)
		{
			soundCount++;
			int num = soundCount;
			soundID[name] = num;
			Stream stream = new MemoryStream(bytes);
			customSound[num] = SoundEffect.FromStream(stream);
			customSoundInstance[num] = customSound[num].CreateInstance();
		}

		public static void SetMusicVanilla(int music)
		{
			customMusic = new MusicVanilla(music);
		}

		public static void SetMusicCustom(int sound)
		{
			customMusic = new MusicCustom(sound);
		}

		public static void SetMusicCustom(string soundName)
		{
			SetMusicCustom(soundID[soundName]);
		}
	}
}
