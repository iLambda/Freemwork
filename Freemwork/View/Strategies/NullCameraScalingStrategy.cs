using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class NullCameraScalingStrategy : ICameraScalingStrategy
    {
        public Vector2 GetScale(Worldspawn Worldspawn, Camera2D Owner)
        {
            return Vector2.One;
        }
    }
}
