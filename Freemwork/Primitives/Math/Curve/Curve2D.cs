namespace Freemwork.Primitives.Math.Curve
{
    public abstract class Curve2D
    {
        public abstract Vector2[] Points { get; }
        public Vector2 this[int PointID] { get { return Points[PointID]; } }
    }
}
