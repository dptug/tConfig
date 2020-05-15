using Microsoft.Xna.Framework.Audio;

namespace Terraria
{
	public class soundInstanceHandler
	{
		public SoundEffectInstance[] soundInstance;

		public SoundEffectInstance this[int i]
		{
			get
			{
				if (i >= soundInstance.Length)
				{
					return SoundHandler.customSoundInstance[i];
				}
				return soundInstance[i];
			}
			set
			{
				if (i >= soundInstance.Length)
				{
					SoundHandler.customSoundInstance[i] = value;
				}
				else
				{
					soundInstance[i] = value;
				}
			}
		}

		public soundInstanceHandler(int size)
		{
			soundInstance = new SoundEffectInstance[size];
		}
	}
}
