using System;

namespace Freemwork.Primitives.Resources
{
    public interface ISound : IResource
    {
        float Volume { get; set; }
        float Pitch { get; set; }
        float Pan { get; set; }

        TimeSpan Duration { get; }
    }
}
