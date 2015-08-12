using Freemwork.Primitives.Math;
using Freemwork.World;

namespace Freemwork.View.Strategies
{
    public sealed class ConstantZoomCameraScalingStrategy : ICameraScalingStrategy
    {
        public Vector2 Zoom { get; set; }

        public ConstantZoomCameraScalingStrategy(Vector2 Zoom)
        {
            this.Zoom = Zoom;
        }

        public Vector2 GetScale(Worldspawn Worldspawn, Camera2D Owner)
        {
            return Zoom;
        }
    }
}
