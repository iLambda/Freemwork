using System.Linq;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math.Curve
{
    public sealed class BezierCurve2D : Curve2D
    {
        private Vector2[] points = null;
        private float resolution = 1f;
        private int pointsCount = 1;

        public Vector2[] ControlPoints { get; private set; }
        public override Vector2[] Points { get { return points; } }
        public float Resolution { get { return resolution; } }
        public int PointsCount { get { return pointsCount; } set { pointsCount = value; Recalc(); } }

        private void Recalc()
        {
            resolution = 1f / pointsCount;
            points = new Vector2[pointsCount];
            var n = ControlPoints.Length;
            var binomialCoeff = Enumerable.Range(0, n).Select(Nu => Maths.BinomialCoefficient(Nu, n-1)).ToArray();
            var t = 0f;

            for (int i = 0; i != pointsCount; i++)
            {
                t += resolution;
                for (int j = 0; j != n; j++)
                    points[i] = binomialCoeff[j]*Maths.Pow(t, j)*Maths.Pow(1 - t, n - 1 - i)*ControlPoints[j];
            }
        }   

        public BezierCurve2D(Vector2[] ControlPoints, int PointsCount)
        {
            this.ControlPoints = ControlPoints;
            this.PointsCount = PointsCount;
        }
    }
}
