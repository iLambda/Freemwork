using System;
using System.Collections.Generic;
using Freemwork.Primitives.Resources;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Freemwork.Services.Resource
{
    public sealed class XNAResourceService : IResourceService
    {
        private ContentManager content = null;
        private Dictionary<Tuple<Type, string>, IResource> cache = new Dictionary<Tuple<Type, string>, IResource>();
        
        public Dictionary<Tuple<Type, string>, IResource> Cache { get { return cache; } }
        
        public XNAResourceService(ContentManager Content)
        {
            content = Content;
            content.RootDirectory = "Content";
        }
        
        public T Load<T>(string Filename) where T : IResource
        {
            if (typeof (T) == typeof (ISprite))
            {
                var texture = content.Load<Texture2D>(Filename);
                var sprite = new XNASprite(texture);
                cache.Add(Tuple.Create(typeof(ISprite), Filename), sprite);
                return (T)((IResource) sprite);
            }
            if (typeof(T) == typeof(ISong))
            {
                var music = content.Load<Song>(Filename);
                var song = new XNASong(music);
                cache.Add(Tuple.Create(typeof(ISong), Filename), song);
                return (T)((IResource)song);
            }
            if (typeof(T) == typeof(IText))
            {
                var font = content.Load<SpriteFont>(Filename);
                var text = new XNAText(font);
                cache.Add(Tuple.Create(typeof(IText), Filename), text);
                return (T)((IResource)text);
            }

            return (T) ((IResource) null);
        }

        public T Get<T>(string Filename) where T : IResource
        {
            if (typeof(T) == typeof(ISprite))
                return (T)cache[Tuple.Create(typeof(ISprite), Filename)];
            if (typeof(T) == typeof(ISong))
                return (T)cache[Tuple.Create(typeof(ISong), Filename)];
            if (typeof(T) == typeof(IText))
                return (T)cache[Tuple.Create(typeof(IText), Filename)];
            return (T)((IResource)null);
        }

        public T GetOrLoad<T>(string Filename) where T : IResource
        {
            if (typeof (T) == typeof (ISprite))
            {
                var tuple = Tuple.Create(typeof (ISprite), Filename);
                if(cache.ContainsKey(tuple))
                    return (T)cache[Tuple.Create(typeof(ISprite), Filename)];
                var texture = content.Load<Texture2D>(Filename);
                var sprite = new XNASprite(texture);
                cache.Add(Tuple.Create(typeof(ISprite), Filename), sprite);
                return (T)((IResource)sprite);
            }
            if (typeof(T) == typeof(ISong))
            {
                var tuple = Tuple.Create(typeof(ISong), Filename);
                if (cache.ContainsKey(tuple))
                    return (T)cache[Tuple.Create(typeof(ISong), Filename)];
                var music = content.Load<Song>(Filename);
                var song = new XNASong(music);
                cache.Add(Tuple.Create(typeof(Song), Filename), song);
                return (T)((IResource)song);
            }
            if (typeof(T) == typeof(IText))
            {
                var tuple = Tuple.Create(typeof(IText), Filename);
                if (cache.ContainsKey(tuple))
                    return (T)cache[Tuple.Create(typeof(IText), Filename)];
                var font = content.Load<SpriteFont>(Filename);
                var text = new XNAText(font);
                cache.Add(Tuple.Create(typeof(Song), Filename), text);
                return (T)((IResource)text);
            }

            return (T)((IResource)null);
        }

        public bool HasLoaded<T>(string Filename) where T : IResource
        {
            return cache.ContainsKey(Tuple.Create(typeof(T), Filename));
        }
    }
}
