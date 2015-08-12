using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.View;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    public sealed class Identity2D : IGameComponent
    {
        private Camera2D camera = new Camera2D();
        public Transform2D Transform = Transform2D.Identity;

        public bool DependsOnCamera { get; set; }
        public Transform2D CameraTransform { get { return Parent == null ? (DependsOnCamera ? Transform2D.Compose(Transform, camera.Transform) : Transform) : Transform2D.Compose(Transform, Parent.QueryComponent<Identity2D>().CameraTransform); } }
        public Transform2D GlobalTransform 
        { 
            get
            {
                var tmp = Parent == null
                    ? Transform
                    : Transform2D.Compose(Transform, Parent.QueryComponent<Identity2D>().GlobalTransform);
                return DependsOnCamera ? tmp : Transform2D.Compose(tmp, Transform2D.Invert(camera.Transform));
            } 
        }
        public GameObject Parent { get; set; }
        public bool HasParent { get { return Parent == null; } }

        public Identity2D(bool DependsOnCamera)
        {
            Transform = Transform2D.Identity;
            Parent = null;

            this.DependsOnCamera = DependsOnCamera;
        }

        public Identity2D(Vector2? Position = null, float? Rotation = null, Vector2? Scale = null, GameObject Parent = null, bool DependsOnCamera = true)
        {
            Transform = new Transform2D(Position ?? Vector2.Zero, Rotation ?? 0f, Scale ?? Vector2.One);
            this.Parent = Parent;
            this.DependsOnCamera = DependsOnCamera;
        }
        public Identity2D(Transform2D Transform, GameObject Parent = null, bool DependsOnCamera = true)
        {
            this.Transform = Transform;
            this.Parent = Parent;
            this.DependsOnCamera = DependsOnCamera;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            camera = Worldspawn.Camera;
        }
        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public IGameComponent Clone() { return new Identity2D (DependsOnCamera) { Parent = Parent, Transform = Transform }; }
    }
}
