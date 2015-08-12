using Freemwork.Primitives.Math;
using Freemwork.Services;
using Freemwork.Utilities;
using Freemwork.View.Strategies;
using Freemwork.World;

namespace Freemwork.View
{
    public sealed class Camera2D
    {
        private Transform2D cameraTransform = Transform2D.Identity;

        public Transform2D Transform { get { return cameraTransform; } }

        public ICameraScrollingStrategy ScrollingStrategy { get; set; }
        public ICameraRotatingStrategy RotatingStrategy { get; set; }
        public ICameraScalingStrategy ScalingStrategy { get; set; }

        public Rectangle<float> Viewport
        {
            get
            {
                return new Rectangle<float>(0, 0, ServiceLocator.GraphicsService.ActualViewport.Width, ServiceLocator.GraphicsService.ActualViewport.Height)
                    .Transform(new Transform2D(-cameraTransform.Position, -cameraTransform.Rotation, Vector2.Invert(cameraTransform.Scale)));
            }
        }

        public Camera2D()
        {
            ScalingStrategy = new NullCameraScalingStrategy();
            ScrollingStrategy = new NullCameraScrollingStrategy();
            RotatingStrategy = new NullCameraRotatingStrategy();
        }

        public Camera2D(ICameraScrollingStrategy ScrollingStrategy, ICameraRotatingStrategy RotatingStrategy, ICameraScalingStrategy ScalingStrategy)
        {
            this.ScalingStrategy = ScalingStrategy;
            this.ScrollingStrategy = ScrollingStrategy;
            this.RotatingStrategy = RotatingStrategy;
        }

        public void Update(Worldspawn Owner)
        {
            cameraTransform.Position = -ScrollingStrategy.GetPosition(Owner, this);
            cameraTransform.Rotation = -RotatingStrategy.GetRotation(Owner, this);
            cameraTransform.Scale = Vector2.Invert(ScalingStrategy.GetScale(Owner, this));
        }

        public void Draw(Worldspawn Owner) { }
    }
}
