using System;

namespace Freemwork.Primitives.Resources
{
    public interface IResource
    {
        String Name { get; }
        bool Loaded { get; }

        IResource Clone();
    }
}
