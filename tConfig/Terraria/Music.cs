using Microsoft.Xna.Framework.Audio;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria
{
	public class Music : Audio
	{
		internal class MusicBank : AudioBank
		{
			internal static List<WaveBank> WaveBanks = new List<WaveBank>();

			internal SoundBank Bank;

			internal MusicBank(string ModName, SoundBank SB)
			{
				base.Name = ModName;
				base.ID = AudioBank.BanksDict[ModName].Count;
				Bank = SB;
				AudioBank.BanksDict[ModName].Add(this);
			}

			internal static void Init()
			{
				if (Main.engine != null)
				{
					List<AudioBank> list = new List<AudioBank>();
					AudioBank.BanksDict.Add("tConfig", list);
					MusicBank musicBank = new MusicBank("tConfig", Main.soundBank);
					AudioBank.BanksDict["tConfig"] = list.AsReadOnly();
					foreach (string mod in Config.mods)
					{
						AudioBank.BanksDict.Add(mod, new List<AudioBank>());
					}
					for (int i = 1; i < Main.music.Length; i++)
					{
						musicBank.GetTrack(Main.music[i].Name);
					}
				}
			}

			internal static void Reset()
			{
				foreach (KeyValuePair<string, IList<AudioBank>> item in AudioBank.BanksDict)
				{
					foreach (MusicBank item2 in item.Value)
					{
						foreach (Audio audio in item2.AudioList)
						{
							audio.Stop();
						}
					}
				}
			}

			internal static void DisposeAll()
			{
				foreach (KeyValuePair<string, IList<AudioBank>> item in AudioBank.BanksDict)
				{
					if (!item.Value.IsReadOnly)
					{
						foreach (MusicBank item2 in item.Value)
						{
							item2.Dispose();
						}
					}
				}
				AudioBank.BanksDict.Clear();
			}

			internal override void Dispose()
			{
				foreach (Audio audio in AudioList)
				{
					audio.Dispose();
				}
				AudioList.Clear();
				AudioDict.Clear();
				Bank.Dispose();
			}

			internal override Audio GetTrack(string TrackName)
			{
				if (Main.engine == null)
				{
					return null;
				}
				if (AudioDict.TryGetValue(TrackName, out Audio value))
				{
					value.Stop();
					return value;
				}
				Cue cue = null;
				try
				{
					cue = Bank.GetCue(TrackName);
				}
				catch (Exception)
				{
					return null;
				}
				value = new Music(this, TrackName, cue);
				AudioList.Add(value);
				AudioDict.Add(TrackName, value);
				return value;
			}
		}

		public static int SongCount;

		public Cue Track;

		public override float Volume
		{
			get
			{
				return Track.GetVariable("Volume");
			}
			set
			{
				Track.SetVariable("Volume", value * Main.musicVolume);
			}
		}

		public float VolumeExact
		{
			set
			{
				Track.SetVariable("Volume", value);
			}
		}

		public override bool IsPlaying => Track.IsPlaying;

		public bool IsPaused => Track.IsPaused;

		public bool IsStopped => Track.IsStopped;

		public Cue GetCue()
		{
			return ((MusicBank)Bank).Bank.GetCue(base.Name);
		}

		internal Music(AudioBank MB, string MusicName, Cue MusSong)
		{
			base.ID = SongCount++;
			base.LocalID = MB.AudioList.Count;
			base.Name = MusicName;
			Bank = MB;
			Track = MusSong;
			Volume = 1f;
		}

		internal static void Init()
		{
			DisposeAll();
			MusicBank.Init();
		}

		internal static void Reset()
		{
			MusicBank.Reset();
		}

		internal static void DisposeAll()
		{
			foreach (WaveBank waveBank in MusicBank.WaveBanks)
			{
				waveBank.Dispose();
			}
			MusicBank.WaveBanks.Clear();
			MusicBank.DisposeAll();
		}

		internal override void Dispose()
		{
			Stop();
		}

		public static void SetGlobalVolume(float Vol)
		{
			foreach (KeyValuePair<string, IList<AudioBank>> item in AudioBank.BanksDict)
			{
				foreach (AudioBank item2 in item.Value)
				{
					foreach (Audio audio in item2.AudioList)
					{
						audio.Volume = Vol;
					}
				}
			}
		}

		internal static void ExtractBanks(SevenZipExtractor Ex, string FilePath, string ModName)
		{
			int num = 0;
			Dictionary<string, AudioEngine> dictionary = new Dictionary<string, AudioEngine>();
			for (int i = 0; i < Ex.ArchiveFileNames.Count; i++)
			{
				string text = Ex.ArchiveFileNames[i];
				if (text.EndsWith(".xgs"))
				{
					Main.statusText = "Loading " + ModName + " audio/soundbanks (" + (num + 1) + ")";
					num++;
					Ex.ExtractFiles(FilePath, new string[1]
					{
						text
					});
					dictionary[text.Substring(0, text.LastIndexOf('.'))] = new AudioEngine(FilePath + "\\" + text);
					string path = Path.Combine(FilePath, text);
					if (File.Exists(path))
					{
						File.Delete(path);
					}
				}
			}
			for (int j = 0; j < Ex.ArchiveFileNames.Count; j++)
			{
				if (Ex.ArchiveFileNames[j].EndsWith(".xsb"))
				{
					Main.statusText = "Loading " + ModName + " audio/soundbanks (" + (num + 1) + ")";
					num++;
					Ex.ExtractFiles(FilePath, new string[1]
					{
						Ex.ArchiveFileNames[j]
					});
					string key = Ex.ArchiveFileNames[j].Substring(0, Ex.ArchiveFileNames[j].LastIndexOf('.'));
					AudioEngine engine = dictionary.ContainsKey(key) ? dictionary[key] : Main.engine;
					ExtractSoundBank(engine, Path.Combine(FilePath, Ex.ArchiveFileNames[j]), ModName);
				}
				else if (Ex.ArchiveFileNames[j].EndsWith(".xwb"))
				{
					Main.statusText = "Loading " + ModName + " audio/soundbanks (" + (num + 1) + ")";
					num++;
					Ex.ExtractFiles(FilePath, new string[1]
					{
						Ex.ArchiveFileNames[j]
					});
					string key2 = Ex.ArchiveFileNames[j].Substring(0, Ex.ArchiveFileNames[j].LastIndexOf('.'));
					AudioEngine engine2 = dictionary.ContainsKey(key2) ? dictionary[key2] : Main.engine;
					ExtractWaveBank(engine2, Path.Combine(FilePath, Ex.ArchiveFileNames[j]));
				}
			}
		}

		internal static void ExtractSoundBank(AudioEngine engine, string FileName, string ModName)
		{
			new MusicBank(ModName, new SoundBank(engine, FileName));
			if (File.Exists(FileName))
			{
				File.Delete(FileName);
			}
		}

		internal static void ExtractWaveBank(AudioEngine engine, string FileName)
		{
			MusicBank.WaveBanks.Add(new WaveBank(engine, FileName));
			if (File.Exists(FileName))
			{
				File.Delete(FileName);
			}
		}

		public void Play()
		{
			if (Main.engine != null)
			{
				Stop();
				Track = ((MusicBank)Bank).Bank.GetCue(base.Name);
				if (Track != null)
				{
					Volume = 1f;
					Track.Play();
				}
			}
		}

		public override void Stop()
		{
			if (Track != null && !Track.IsDisposed)
			{
				Track.Stop(AudioStopOptions.Immediate);
			}
		}

		public void Resume()
		{
			Track.Resume();
		}

		public void Pause()
		{
			Track.Pause();
		}
	}
}
