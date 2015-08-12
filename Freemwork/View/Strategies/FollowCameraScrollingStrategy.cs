using Freemwork.Primitives.Math;
using Freemwork.Services;
using Freemwork.World;
using Freemwork.World.Objects;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.View.Strategies
{
    public sealed class FollowCameraScrollingStrategy : ICameraScrollingStrategy
    {
        public GameObject Target { get; set; }
        public Vector2 Offset { get; set; }

        public FollowCameraScrollingStrategy(GameObject Target)
        {
            this.Target = Target;
        }

        public Vector2 GetPosition(Worldspawn Worldspawn, Camera2D Owner)
        {
            if (Target == null)
                return -new Vector2(ServiceLocator.GraphicsService.ViewportSize.Width / 2, ServiceLocator.GraphicsService.ViewportSize.Height / 2);
            return ((Target.QueryComponent<Identity2D>().Transform.Position - Offset) * Owner.Transform.Scale - new Vector2(ServiceLocator.GraphicsService.ViewportSize.Width / 2, ServiceLocator.GraphicsService.ViewportSize.Height / 2));
        }
    }
}
