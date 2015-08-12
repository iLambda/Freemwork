using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(Identity2D))]
    [NeededComponent(typeof(SpriteHolder))]
    public sealed class SpriteOscillator : IGameComponent
    {
        private int frame = 0;
        private bool oscillate = false;

        public Size2D<int> MinSize { get; set; }
        public Size2D<int>? IdleSize { get; set; }
        public Size2D<int> MaxSize { get; set; }
        public float Frequency { get; set; }
        public bool Oscillate { get { return oscillate; } set { if (oscillate != value && IdleSize.HasValue) frame = 0; oscillate = value; } }

        public SpriteOscillator(Size2D<int> MinSize, Size2D<int> MaxSize, float Frequency, Size2D<int>? IdleSize = null, bool Oscillate = true)
        {
            this.MinSize = MinSize;
            this.MaxSize = MaxSize;
            this.IdleSize = IdleSize;
            this.Frequency = Frequency;
            this.Oscillate = Oscillate;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var spriteHolder = Owner.QueryComponent<SpriteHolder>();

            var sX = (int)((MaxSize.Width - MinSize.Width) * Maths.Cos(frame * 2 * Maths.Pi / Frequency)) + MinSize.Width;
            var sY = (int)((MaxSize.Height - MinSize.Height) * Maths.Cos(frame * 2 * Maths.Pi / Frequency)) + MinSize.Height;

            spriteHolder.Sprite.Size = new Size2D<int>(IdleSize.HasValue ? (Oscillate ? sX : IdleSize.Value.Width) : sX, IdleSize.HasValue ? (Oscillate ? sY : IdleSize.Value.Height) : sY);

            if (Oscillate)
                frame = ((frame + 1) % 65536);
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {

        }

        public IGameComponent Clone()
        {
            return new SpriteOscillator(MinSize, MaxSize, Frequency, IdleSize, Oscillate);
        }
    }
}
