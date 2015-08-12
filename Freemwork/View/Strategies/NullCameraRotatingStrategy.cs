using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class NullCameraRotatingStrategy : ICameraRotatingStrategy
    {
        public float GetRotation(Worldspawn Worldspawn, Camera2D Owner)
        {
            return 0f;
        }
    }
}
