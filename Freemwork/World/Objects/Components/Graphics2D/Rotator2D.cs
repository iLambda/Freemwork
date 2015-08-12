using Freemwork.Playstates;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(Identity2D))]
    public sealed class Rotator2D : IGameComponent
    {
        private uint frame = 0;
        private uint interval = 0;

        public float Increase { get; set; }
        public uint Interval { get { return interval; } set { if (interval != value) frame = 0; interval = value; } }
        public bool Activated { get; set; }

        public Rotator2D(float Increase, int Interval = 1)
        {
            this.Increase = Increase;
            this.Interval = (uint)(Interval > 0 ? Interval : 0);
            this.Activated = true;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (!Activated)
                return;

            if (frame == 0) Owner.QueryComponent<Identity2D>().Transform.Rotation += Increase;
            frame = (frame + 1) % interval;
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
        }

        public IGameComponent Clone()
        {
            return new Rotator2D(Increase, (int)Interval) { frame = frame, interval = interval, Activated = Activated };
        }
    }
}
