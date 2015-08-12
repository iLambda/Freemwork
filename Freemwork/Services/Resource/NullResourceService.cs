using System;
using System.Collections.Generic;
using Freemwork.Primitives.Resources;

namespace Freemwork.Services.Resource
{
    public sealed class NullResourceService : INullService, IResourceService
    {
        private Dictionary<Tuple<Type, string>, IResource> cache = new Dictionary<Tuple<Type, string>, IResource>();
        public Dictionary<Tuple<Type, string>, IResource> Cache { get { return cache; } }

        public T Load<T>(string Filename) where T : IResource
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("Load<{0}>({1})".FormatString(typeof(T), Filename));
            #endif

            return default(T);
        }

        public T Get<T>(string Filename) where T : IResource
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("Get<{0}>({1})".FormatString(typeof(T), Filename));
            #endif

            return default(T);
        }

        public T GetOrLoad<T>(string Filename) where T : IResource
        {
            #if DEBUG_VERBOSE
                if (Debugger.IsAttached)
                    Debug.WriteLine("GetOrLoad<{0}>({1})".FormatString(typeof(T), Filename));
            #endif

            return default(T);
        }

        public bool HasLoaded<T>(string Filename) where T : IResource
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("HasLoaded<{0}>({1})".FormatString(typeof(T), Filename));
            #endif

            return false;
        }
    }
}
