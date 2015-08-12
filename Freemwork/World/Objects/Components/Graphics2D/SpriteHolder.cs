using System;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;
using Freemwork.Services;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(Identity2D))]
    [BoundsDefiningProperty("Bounds")]
    public sealed class SpriteHolder : IGameComponent
    {
        public ISprite Sprite { get; set; }
        public Rectangle<float> Bounds { get { return new Rectangle<float>(-Sprite.Origin.X, -Sprite.Origin.Y, Sprite.Size.Width, Sprite.Size.Height); } }

        public SpriteHolder(String SpriteName)
        {
            Sprite = (ISprite) ServiceLocator.ResourceService.GetOrLoad<ISprite>(SpriteName).Clone();
        }

        public SpriteHolder(ISprite Sprite)
        {
            this.Sprite = Sprite;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if(Sprite != null)
                ServiceLocator.GraphicsService.Draw(Sprite, Owner.QueryComponent<Identity2D>().CameraTransform);
        }
        public IGameComponent Clone()
        {
            return new SpriteHolder(Sprite != null ? (ISprite)Sprite.Clone() : null);   
        }
    }
}
