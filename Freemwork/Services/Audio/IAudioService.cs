using System;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Audio
{
    public enum AudioState
    {
        Paused,
        Playing,
        Stopped
    };

    public interface IAudioService : IService
    {
        bool AllowedToPlay { get; }
        bool IsMuted { get; set; }
        bool IsRepeating { get; set; }
        TimeSpan SongProgress { get; }
        AudioState State { get; }
        float Volume { get; set; }

        void Play(ISound Sound);
        void Play(ISong Song);
        void Pause();
        void Resume();
        void Stop();
    }
}
