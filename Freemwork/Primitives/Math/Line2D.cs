using System;

namespace Freemwork.Primitives.Math
{
    public struct Line2D : IEquatable<Line2D>
    {
        public Vector2 Direction;
        public Vector2 Point;

        public Vector2 Normal { get { return new Vector2(-Direction.Y, Direction.X); } }

        public Line2D(Vector2 Direction, Vector2 Point)
        {
            if(Direction == Vector2.Zero) throw new ArgumentException("Direction cannot be null.", "Direction");

            this.Direction = Direction;
            this.Point = Point;
        }

        public Line2D(Segment2D Segment)
        {
            if (Segment.Direction == Vector2.Zero) throw new ArgumentException("Direction cannot be null.", "Segment");

            this.Direction = Segment.Direction;
            this.Point = Segment.Start;
        }

        public bool Contains(Vector2 M)
        {
            var supposed = (M - Point) / Direction;
            return supposed.X == supposed.Y;
        }

        public float? GetIndice(Vector2 M)
        {
            var supposed = (M - Point) / Direction;
            return supposed.X == supposed.Y ? (float?)supposed.X : null;
        }

        public bool Contains(Segment2D Segment)
        {
            return Contains(Segment.Start) && Contains(Segment.Direction);
        }


        public bool IsParallel(Line2D Other)
        {
            return Other.Direction.ColinearWith(Direction);
        }

        public bool IsOrthogonal(Line2D Other)
        {
            return Vector2.Dot(Other.Direction, Direction) == 0;
        }


        public static bool operator ==(Line2D Self, Line2D Other)
        {
            return Self.Equals(Other);
        }

        public static bool operator !=(Line2D Self, Line2D Other)
        {
            return !Self.Equals(Other);
        }

        public bool IsSame(Line2D Line, bool OrientationRelevant = false)
        {
            return Direction.ColinearWith(Line.Direction) && Contains(Line.Point) 
                && (!OrientationRelevant || Direction.X / Line.Direction.X >= 0);

        }


        public bool Equals(Line2D Other)
        {
            return Direction.Equals(Other.Direction) && Point.Equals(Other.Point);
        }

        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            return Obj is Line2D && Equals((Line2D)Obj);
        }

        public override int GetHashCode()
        {
            return unchecked((Direction.GetHashCode() * 397) ^ Point.GetHashCode());
        }
    }
}
