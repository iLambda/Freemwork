using System;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    public struct Segment2D : IEquatable<Segment2D>
    {
        public Vector2 Start;
        public Vector2 End;

        public Vector2 Direction { get { return End - Start; } }
        public Vector2 Normal { get { return new Vector2(-Direction.Y, Direction.X); } }

        public Line2D Line { get { return new Line2D(Direction, Start); } }

        public bool Contains(Vector2 M)
        {
            var supposed = (M - Start) / Direction;
            return supposed.X == supposed.Y && supposed.X.In(0, 1);
        }

        public float? GetIndice(Vector2 M)
        {
            var supposed = (M - Start) / Direction;
            return supposed.X == supposed.Y && supposed.X.In(0, 1) ? (float?)supposed.X : null;
        }

        public bool IsParallel(Line2D Other)
        {
            return Other.Direction.ColinearWith(Direction);
        }

        public bool Intersects(Rectangle<float> Rectangle)
        {
            return false;   
        }

        public bool IsOrthogonal(Line2D Other)
        {
            return Vector2.Dot(Other.Direction, Direction) == 0;
        }

        public bool IsParallel(Segment2D Other)
        {
            return Other.Direction.ColinearWith(Direction);
        }

        public bool IsOrthogonal(Segment2D Other)
        {
            return Vector2.Dot(Other.Direction, Direction) == 0;
        }

        public bool IsSame(Segment2D Other, bool OrientationRelevant = false)
        {
            return (Start.Equals(Other.Start) && End.Equals(Other.End)) || (!OrientationRelevant && (Start.Equals(Other.End) && End.Equals(Other.Start)));
        }

        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            return Obj is Segment2D && Equals((Segment2D)Obj);
        }

        public override int GetHashCode()
        {
            return unchecked((Start.GetHashCode() * 397) ^ End.GetHashCode());
        }

        public bool Equals(Segment2D Other)
        {
            return IsSame(Other, true);
        }

        public static bool operator ==(Segment2D Self, Segment2D Other)
        {
            return Self.Equals(Other);
        }

        public static bool operator !=(Segment2D Self, Segment2D Other)
        {
            return !Self.Equals(Other);
        }

    }
}

