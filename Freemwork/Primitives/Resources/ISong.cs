using System;

namespace Freemwork.Primitives.Resources
{
    public interface ISong : IResource
    {
        TimeSpan Duration { get; }
    }
}
