using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class ConstantCameraRotatingStrategy : ICameraRotatingStrategy
    {
        public float Angle { get; set; }

        public ConstantCameraRotatingStrategy(float Angle)
        {
            this.Angle = Angle;
        }

        public float GetRotation(Worldspawn Worldspawn, Camera2D Owner)
        {
            return Angle;
        }
    }
}
