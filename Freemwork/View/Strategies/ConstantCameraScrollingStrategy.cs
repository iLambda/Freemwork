using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class ConstantCameraScrollingStrategy : ICameraScrollingStrategy
    {
        public Vector2 Displacement { get; set; }

        public ConstantCameraScrollingStrategy(Vector2 Displacement)
        {
            this.Displacement = Displacement;
        }

        public Vector2 GetPosition(Worldspawn Worldspawn, Camera2D Owner)
        {
            return Displacement;
        }
    }
}
