using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class NullCameraScrollingStrategy : ICameraScrollingStrategy
    {
        public Vector2 GetPosition(Worldspawn Worldspawn, Camera2D Owner)
        {
            return Vector2.Zero;
        }
    }
}
