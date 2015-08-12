using System;
using Freemwork.Primitives.Resources;
using Microsoft.Xna.Framework.Media;

namespace Freemwork.Services.Audio
{
    public sealed class XNAAudioService : IAudioService
    {
        public bool AllowedToPlay { get { return MediaPlayer.GameHasControl; } }
        public bool IsMuted { get { return MediaPlayer.IsMuted; } set { MediaPlayer.IsMuted = value; } }
        public bool IsRepeating { get { return MediaPlayer.IsRepeating; } set { MediaPlayer.IsRepeating = value; } }
        public TimeSpan SongProgress { get { return MediaPlayer.PlayPosition; } }
        public AudioState State { get { return (AudioState) MediaPlayer.State; } }
        public float Volume { get { return MediaPlayer.Volume; } set { MediaPlayer.Volume = value; } }

        public XNAAudioService()
        {
            IsMuted = false;
            IsRepeating = true;
            Volume = 1f;
        }

        public void Play(ISound Sound)
        {
            var sound = (XNASound)Sound;
            if (AllowedToPlay)
            {
                var inst = sound.Sound.CreateInstance();
                inst.Volume = sound.Volume;
                inst.Pitch = sound.Pitch;
                inst.Pan = sound.Pan;
                inst.Play();
            }
        }

        public void Play(ISong Song)
        {
            var song = (XNASong) Song;
            if(AllowedToPlay)
                MediaPlayer.Play(song.Song);
        }

        public void Pause() { MediaPlayer.Pause(); }
        public void Resume() { MediaPlayer.Resume(); }
        public void Stop() { MediaPlayer.Stop(); }
    }
}
