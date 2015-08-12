using System;
using Microsoft.Xna.Framework.Audio;

namespace Freemwork.Primitives.Resources
{
    public sealed class XNASound : ISound
    {
        private SoundEffect sound = null;

        public SoundEffect Sound { get { return sound; } }
        public string Name
        {
            get
            {
                if (sound == null)
                    throw new NullReferenceException("The XNASound is not loaded.");
                return sound.Name;
            }
        }
        public bool Loaded { get { return sound != null; } }
        public float Volume { get; set; }
        public float Pitch { get; set; }
        public float Pan { get; set; }

        public TimeSpan Duration
        {
            get
            {
                if (sound == null)
                    throw new NullReferenceException("The XNASong is not loaded.");
                return sound.Duration;
            }
        }

        public XNASound(SoundEffect Sound)
        {
            sound = Sound;
            Volume = 1f;
            Pitch = 0f;
            Pan = 0f;
        }

        public IResource Clone()
        {
            return new XNASound(Sound) { Pan = Pan, Pitch = Pitch, Volume = Volume };
        }

    }
}
