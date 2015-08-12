using System;
using Microsoft.Xna.Framework.Media;

namespace Freemwork.Primitives.Resources
{
    public sealed class XNASong : ISong
    {
        private Song song;

        public Song Song { get { return song; } }
        public string Name
        {
            get
            {
                if (song == null)
                    throw new NullReferenceException("The XNASong is not loaded.");
                return song.Name;
            }
        }
        public bool Loaded { get { return song != null; } }
        public TimeSpan Duration
        {
            get
            {
                if (song == null)
                    throw new NullReferenceException("The XNASong is not loaded.");
                return song.Duration;
            }
        }

        public XNASong(Song Song)
        {
            song = Song;
        }

        public IResource Clone()
        {
            return new XNASong(song);
        }

    }
}
