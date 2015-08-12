using System;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    public struct Transform2D : IEquatable<Transform2D>
    {
        private static readonly Transform2D identity = new Transform2D(Vector2.Zero, 0f, Vector2.One);

        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;

        public static Transform2D Identity { get { return identity; } }

        public bool Equals(Transform2D Other)
        {
            return Position == Other.Position && Rotation == Other.Rotation && Scale == Other.Scale;
        }

        public Transform2D(Vector2 Position, float Rotation, Vector2 Scale)
        {
            this.Position = Position;
            this.Rotation = Rotation;
            this.Scale = Scale;
        }


        public void Compose(Transform2D Transform)
        {
            var tmp = Compose(this, Transform);
            Position = tmp.Position;
            Rotation = tmp.Rotation;
            Scale = tmp.Scale;
        }

        public static Transform2D Invert(Transform2D Self)
        {
            return new Transform2D(-Self.Position, -Self.Rotation, Vector2.Invert(Self.Scale));
        }

        public static Transform2D Compose(Transform2D Children, Transform2D Parent)
        {
            var tmp = new Transform2D();
            tmp.Position = Parent.Position + Vector2.Rotate(Parent.Scale * Children.Position, Parent.Rotation * (Maths.Pi / 180f));
            tmp.Rotation = Children.Rotation + Parent.Rotation;
            tmp.Scale = Children.Scale * Parent.Scale;
            return tmp;
        }

        public override string ToString()
        {
            return "{{Pos : ({0},{1}),  Rot : {2}, Sca : ({3},{4})}}".FormatString(Position.X, Position.Y, Rotation, Scale.X, Scale.Y);
        }
    }

    
}
