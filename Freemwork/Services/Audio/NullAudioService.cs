using System;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Audio
{
    public class NullAudioService : IAudioService, INullService
    {
        public bool AllowedToPlay { get { return true; } }
        public bool IsMuted { get; set; }
        public bool IsRepeating { get; set; }
        public TimeSpan SongProgress { get { return TimeSpan.Zero; } }
        public AudioState State { get { return AudioState.Stopped; } }
        public float Volume { get; set; }

        public void Pause()
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Pause() called from NullAudioService");
            #endif
        }
        public void Resume()
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Resume() called from NullAudioService");
            #endif
        }
        public void Stop()
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Stop() called from NullAudioService");
            #endif
        }
        public void Play(ISong Song)
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Play(Song) called from NullAudioService");
            #endif
        }

        public void Play(ISound Sound)
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("Play(Sound) called from NullAudioService");
            #endif
        }
    }
}
