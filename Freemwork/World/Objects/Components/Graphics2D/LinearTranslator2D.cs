using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(Identity2D))]
    public sealed class LinearTranslator2D : IGameComponent
    {
        private uint frame = 0;
        private uint interval = 0;

        public Vector2 Direction { get; set; }
        public uint Interval { get { return interval; } set { if (interval != value) frame = 0; interval = value; } }
        public bool Activated { get; set; }

        public LinearTranslator2D(Vector2 Direction, int Interval = 1)
        {
            this.Direction = Direction;
            this.Interval = (uint)(Interval > 0 ? Interval : 0);
            this.Activated = true;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (!Activated)
                return;

            if (frame == 0) Owner.QueryComponent<Identity2D>().Transform.Position += Direction;
            frame = (frame + 1) % interval;
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
        }

        public IGameComponent Clone()
        {
            return new LinearTranslator2D(Direction, (int)Interval) { frame = frame, interval = interval, Activated = Activated };
        }
    }
}
