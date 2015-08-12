using System;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;
using Freemwork.Services;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(Identity2D))]
    [UncompatibleComponent(typeof(SpriteHolder))]
    [BoundsDefiningProperty("Bounds")]
    public sealed class TextHolder : IGameComponent
    {
        public IText Text { get; set; }
        public Rectangle<float> Bounds { get { return new Rectangle<float>(-Text.Origin.X, -Text.Origin.Y, Text.FullSize.Width, Text.FullSize.Height); } }

        public TextHolder(String TextName, String DefaultText = null)
        {
            Text = (IText)ServiceLocator.ResourceService.GetOrLoad<IText>(TextName).Clone();
            if (DefaultText != null) Text.Text = DefaultText;
        }

        public TextHolder(IText Text)
        {
            this.Text = Text;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (Text != null)
                ServiceLocator.GraphicsService.Draw(Text, Owner.QueryComponent<Identity2D>().CameraTransform);
        }
        public IGameComponent Clone()
        {
            return new SpriteHolder(Text != null ? (ISprite)Text.Clone() : null);
        }
    }
}
