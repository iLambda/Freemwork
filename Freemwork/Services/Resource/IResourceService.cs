using System;
using System.Collections.Generic;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Resource
{
    public interface IResourceService : IService
    {
        Dictionary<Tuple<Type, String>, IResource> Cache { get; }

        T Load<T>(String Filename) where T : IResource;
        T Get<T>(String Filename) where T : IResource;
        T GetOrLoad<T>(String Filename) where T : IResource;
        bool HasLoaded<T>(String Filename) where T : IResource;
    }
}
