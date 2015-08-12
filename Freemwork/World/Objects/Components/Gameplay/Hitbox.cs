using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Gameplay
{
    public abstract class Hitbox : IGameComponent
    {
        public abstract int Dimension { get; }
        public abstract bool Intersects(Hitbox Box);

        public abstract void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        public abstract void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        public abstract IGameComponent Clone();
    }

    [BoundsDefiningProperty("BoundingBox")]
    public sealed class Hitbox2D : Hitbox
    {
        public Rectangle<int> BoundingBox { get; set; }
        public override int Dimension { get { return 2; } }

        public Hitbox2D(Rectangle<int> BoundingBox)
        {
            this.BoundingBox = BoundingBox;
        }

        public override bool Intersects(Hitbox Box)
        {
            return Box.Dimension == Dimension && BoundingBox.Intersects(((Hitbox2D) Box).BoundingBox);
        }

        public override void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public override void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public override IGameComponent Clone() { return new Hitbox2D(BoundingBox); }
    }
}
