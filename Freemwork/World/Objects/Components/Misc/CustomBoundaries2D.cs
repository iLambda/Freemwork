using Freemwork.Primitives.Math;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Misc
{
    [BoundsDefiningProperty("Bounds")]
    public sealed class CustomBoundaries2D : IGameComponent
    {
        public Rectangle<float> Bounds { get; set; }

        public CustomBoundaries2D(Rectangle<float> Bounds)
        {
            this.Bounds = Bounds;
        }

        public void Update(Playstates.PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {

        }

        public void Draw(Playstates.PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {

        }

        public IGameComponent Clone()
        {
            return new CustomBoundaries2D(Bounds);
        }
    }
}
