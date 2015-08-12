using System;
using System.Globalization;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    public struct Vector2 : IEquatable<Vector2>
    {
        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);
        private static readonly Vector2 unitVector = new Vector2(1f, 1f);
        private static readonly Vector2 unitXVector = new Vector2(1f, 0f);
        private static readonly Vector2 unitYVector = new Vector2(0f, 1f);

        public float X;
        public float Y;

        public static Vector2 Zero { get { return zeroVector; } }
        public static Vector2 One { get { return unitVector; } }
        public static Vector2 UnitX { get { return unitXVector; } }
        public static Vector2 UnitY { get { return unitYVector; } }

        public Vector2(float X, float Y) { this.X = X; this.Y = Y; }
        public Vector2(float Value) { X = Y = Value; }

        public void Rotate(float Angle)
        {
            var cos = Maths.Cos(Angle);
            var sin = Maths.Sin(Angle);

            X = X*cos - Y*sin;
            Y = X*sin + Y*cos;
        }

        public static Vector2 Rotate(Vector2 Vector, float Angle)
        {
            var cos = Maths.Cos(Angle);
            var sin = Maths.Sin(Angle);

            return new Vector2(Vector.X * cos - Vector.Y * sin, Vector.X * sin + Vector.Y * cos);
        }

        public static Vector2 Add(Vector2 Value1, Vector2 Value2)
        {
            Value1.X += Value2.X;
            Value1.Y += Value2.Y;
            return Value1;
        }

        public static Vector2 Barycentric(Vector2 Value1, Vector2 Value2, Vector2 Value3, float Amount1, float Amount2)
        {
            return new Vector2(
                Maths.Barycentric(Value1.X, Value2.X, Value3.X, Amount1, Amount2),
                Maths.Barycentric(Value1.Y, Value2.Y, Value3.Y, Amount1, Amount2));
        }

        public static Vector2 CatmullRom(Vector2 Value1, Vector2 Value2, Vector2 Value3, Vector2 Value4, float Amount)
        {
            return new Vector2(
                Maths.CatmullRom(Value1.X, Value2.X, Value3.X, Value4.X, Amount),
                Maths.CatmullRom(Value1.Y, Value2.Y, Value3.Y, Value4.Y, Amount));
        }

        public static void CatmullRom(ref Vector2 Value1, ref Vector2 Value2, ref Vector2 Value3, ref Vector2 Value4, float Amount, out Vector2 Result)
        {
            Result.X = Maths.CatmullRom(Value1.X, Value2.X, Value3.X, Value4.X, Amount);
            Result.Y = Maths.CatmullRom(Value1.Y, Value2.Y, Value3.Y, Value4.Y, Amount);
        }

        public static Vector2 Clamp(Vector2 Value1, Vector2 Min, Vector2 Max)
        {
            return new Vector2(
                Maths.Clamp(Value1.X, Min.X, Max.X),
                Maths.Clamp(Value1.Y, Min.Y, Max.Y));
        }

        public static void Clamp(ref Vector2 Value1, ref Vector2 Min, ref Vector2 Max, out Vector2 Result)
        {
            Result.X = Maths.Clamp(Value1.X, Min.X, Max.X);
            Result.Y = Maths.Clamp(Value1.Y, Min.Y, Max.Y);
        }

        public static float Distance(Vector2 Value1, Vector2 Value2)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y;
            return Maths.Sqrt((v1 * v1) + (v2 * v2));
        }

        public static void Distance(ref Vector2 Value1, ref Vector2 Value2, out float Result)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y;
            Result = Maths.Sqrt((v1 * v1) + (v2 * v2));
        }

        public static float DistanceSquared(Vector2 Value1, Vector2 Value2)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        public static void DistanceSquared(ref Vector2 Value1, ref Vector2 Value2, out float Result)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y;
            Result = (v1 * v1) + (v2 * v2);
        }

        public static Vector2 Divide(Vector2 Value1, Vector2 Value2)
        {
            Value1.X /= Value2.X;
            Value1.Y /= Value2.Y;
            return Value1;
        }

        public static void Divide(ref Vector2 Value1, ref Vector2 Value2, out Vector2 Result)
        {
            Result.X = Value1.X / Value2.X;
            Result.Y = Value1.Y / Value2.Y;
        }

        public static Vector2 Divide(Vector2 Value1, float Divider)
        {
            float factor = 1 / Divider;
            Value1.X *= factor;
            Value1.Y *= factor;
            return Value1;
        }

        public static void Divide(ref Vector2 Value1, float Divider, out Vector2 Result)
        {
            float factor = 1 / Divider;
            Result.X = Value1.X * factor;
            Result.Y = Value1.Y * factor;
        }

        public static float Dot(Vector2 Value1, Vector2 Value2)
        {
            return (Value1.X * Value2.X) + (Value1.Y * Value2.Y);
        }

        public static void Dot(ref Vector2 Value1, ref Vector2 Value2, out float Result)
        {
            Result = (Value1.X * Value2.X) + (Value1.Y * Value2.Y);
        }

        public override bool Equals(object Obj)
        {
            if (Obj is Vector2)
            {
                return Equals((Vector2)Obj);
            }

            return false;
        }

        public bool Equals(Vector2 Other)
        {
            return (X == Other.X) && (Y == Other.Y);
        }

        public static Vector2 Reflect(Vector2 Vector, Vector2 Normal)
        {
            Vector2 result;
            float val = 2.0f * ((Vector.X * Normal.X) + (Vector.Y * Normal.Y));
            result.X = Vector.X - (Normal.X * val);
            result.Y = Vector.Y - (Normal.Y * val);
            return result;
        }

        public static void Reflect(ref Vector2 Vector, ref Vector2 Normal, out Vector2 Result)
        {
            float val = 2.0f * ((Vector.X * Normal.X) + (Vector.Y * Normal.Y));
            Result.X = Vector.X - (Normal.X * val);
            Result.Y = Vector.Y - (Normal.Y * val);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public static Vector2 Hermite(Vector2 Value1, Vector2 Tangent1, Vector2 Value2, Vector2 Tangent2, float Amount)
        {
            Vector2 result;
            Hermite(ref Value1, ref Tangent1, ref Value2, ref Tangent2, Amount, out result);
            return result;
        }

        public static void Hermite(ref Vector2 Value1, ref Vector2 Tangent1, ref Vector2 Value2, ref Vector2 Tangent2, float Amount, out Vector2 Result)
        {
            Result.X = Maths.Hermite(Value1.X, Tangent1.X, Value2.X, Tangent2.X, Amount);
            Result.Y = Maths.Hermite(Value1.Y, Tangent1.Y, Value2.Y, Tangent2.Y, Amount);
        }

        public float Length()
        {
            return Maths.Sqrt((X * X) + (Y * Y));
        }

        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        public static Vector2 Lerp(Vector2 Value1, Vector2 Value2, float Amount)
        {
            return new Vector2(
                Maths.Lerp(Value1.X, Value2.X, Amount),
                Maths.Lerp(Value1.Y, Value2.Y, Amount));
        }

        public static void Lerp(ref Vector2 Value1, ref Vector2 Value2, float Amount, out Vector2 Result)
        {
            Result.X = Maths.Lerp(Value1.X, Value2.X, Amount);
            Result.Y = Maths.Lerp(Value1.Y, Value2.Y, Amount);
        }

        public static Vector2 Max(Vector2 Value1, Vector2 Value2)
        {
            return new Vector2(Value1.X > Value2.X ? Value1.X : Value2.X,
                               Value1.Y > Value2.Y ? Value1.Y : Value2.Y);
        }

        public static void Max(ref Vector2 Value1, ref Vector2 Value2, out Vector2 Result)
        {
            Result.X = Value1.X > Value2.X ? Value1.X : Value2.X;
            Result.Y = Value1.Y > Value2.Y ? Value1.Y : Value2.Y;
        }

        public static Vector2 Min(Vector2 Value1, Vector2 Value2)
        {
            return new Vector2(Value1.X < Value2.X ? Value1.X : Value2.X,
                               Value1.Y < Value2.Y ? Value1.Y : Value2.Y);
        }

        public static void Min(ref Vector2 Value1, ref Vector2 Value2, out Vector2 Result)
        {
            Result.X = Value1.X < Value2.X ? Value1.X : Value2.X;
            Result.Y = Value1.Y < Value2.Y ? Value1.Y : Value2.Y;
        }

        public static Vector2 Multiply(Vector2 Value1, Vector2 Value2)
        {
            Value1.X *= Value2.X;
            Value1.Y *= Value2.Y;
            return Value1;
        }

        public static Vector2 Multiply(Vector2 Value1, float ScaleFactor)
        {
            Value1.X *= ScaleFactor;
            Value1.Y *= ScaleFactor;
            return Value1;
        }

        public static void Multiply(ref Vector2 Value1, float ScaleFactor, out Vector2 Result)
        {
            Result.X = Value1.X * ScaleFactor;
            Result.Y = Value1.Y * ScaleFactor;
        }

        public static void Multiply(ref Vector2 Value1, ref Vector2 Value2, out Vector2 Result)
        {
            Result.X = Value1.X * Value2.X;
            Result.Y = Value1.Y * Value2.Y;
        }

        public static Vector2 Negate(Vector2 Value)
        {
            Value.X = -Value.X;
            Value.Y = -Value.Y;
            return Value;
        }

        public static void Negate(ref Vector2 Value, out Vector2 Result)
        {
            Result.X = -Value.X;
            Result.Y = -Value.Y;
        }

        public void Normalize()
        {
            float val = 1.0f / Maths.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
        }

        public static Vector2 Normalize(Vector2 Value)
        {
            float val = 1.0f / Maths.Sqrt((Value.X * Value.X) + (Value.Y * Value.Y));
            Value.X *= val;
            Value.Y *= val;
            return Value;
        }

        public static void Normalize(ref Vector2 Value, out Vector2 Result)
        {
            float val = 1.0f / Maths.Sqrt((Value.X * Value.X) + (Value.Y * Value.Y));
            Result.X = Value.X * val;
            Result.Y = Value.Y * val;
        }

        public static Vector2 SmoothStep(Vector2 Value1, Vector2 Value2, float Amount)
        {
            return new Vector2(
                Maths.SmoothStep(Value1.X, Value2.X, Amount),
                Maths.SmoothStep(Value1.Y, Value2.Y, Amount));
        }

        public static void SmoothStep(ref Vector2 Value1, ref Vector2 Value2, float Amount, out Vector2 Result)
        {
            Result.X = Maths.SmoothStep(Value1.X, Value2.X, Amount);
            Result.Y = Maths.SmoothStep(Value1.Y, Value2.Y, Amount);
        }

        public static Vector2 Subtract(Vector2 Value1, Vector2 Value2)
        {
            Value1.X -= Value2.X;
            Value1.Y -= Value2.Y;
            return Value1;
        }

        public static void Subtract(ref Vector2 Value1, ref Vector2 Value2, out Vector2 Result)
        {
            Result.X = Value1.X - Value2.X;
            Result.Y = Value1.Y - Value2.Y;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] { 
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture) });
        }
        
        public static Vector2 operator -(Vector2 Value)
        {
            Value.X = -Value.X;
            Value.Y = -Value.Y;
            return Value;
        }


        public static bool operator ==(Vector2 Value1, Vector2 Value2)
        {
            return Value1.X == Value2.X && Value1.Y == Value2.Y;
        }


        public static bool operator !=(Vector2 Value1, Vector2 Value2)
        {
            return Value1.X != Value2.X || Value1.Y != Value2.Y;
        }


        public static Vector2 operator +(Vector2 Value1, Vector2 Value2)
        {
            Value1.X += Value2.X;
            Value1.Y += Value2.Y;
            return Value1;
        }


        public static Vector2 operator -(Vector2 Value1, Vector2 Value2)
        {
            Value1.X -= Value2.X;
            Value1.Y -= Value2.Y;
            return Value1;
        }


        public static Vector2 operator *(Vector2 Value1, Vector2 Value2)
        {
            Value1.X *= Value2.X;
            Value1.Y *= Value2.Y;
            return Value1;
        }

        public static Vector2 operator *(Vector2 Value, Matrix3x3 Matrix)
        {
            var result = Vector2.Zero;
            var x = (Value.X * Matrix.M11) + (Value.Y * Matrix.M21) + Matrix.M31;
            var y = (Value.X * Matrix.M12) + (Value.Y * Matrix.M22) + Matrix.M32;
            result.X = x;
            result.Y = y;

            return result;
        }

        public static Vector2 operator *(Matrix3x3 Matrix, Vector2 Value)
        {
            var result = Vector2.Zero;
            var x = (Value.X * Matrix.M11) + (Value.Y * Matrix.M21) + Matrix.M31;
            var y = (Value.X * Matrix.M12) + (Value.Y * Matrix.M22) + Matrix.M32;
            result.X = x;
            result.Y = y;

            return result;
        }

        public static Vector2 operator *(Vector2 Value, float ScaleFactor)
        {
            Value.X *= ScaleFactor;
            Value.Y *= ScaleFactor;
            return Value;
        }


        public static Vector2 operator *(float ScaleFactor, Vector2 Value)
        {
            Value.X *= ScaleFactor;
            Value.Y *= ScaleFactor;
            return Value;
        }


        public static Vector2 operator /(Vector2 Value1, Vector2 Value2)
        {
            Value1.X /= Value2.X;
            Value1.Y /= Value2.Y;
            return Value1;
        }

        public static Vector2 operator /(Vector2 Value1, float Divider)
        {
            float factor = 1 / Divider;
            Value1.X *= factor;
            Value1.Y *= factor;
            return Value1;
        }

        public static Vector2 Invert(Vector2 Other)
        {
            return new Vector2(1f/Other.X, 1f/Other.Y);
        }

        public void Invert()
        {
            X = 1f/X;
            Y = 1f/Y;
        }

        public bool ColinearWith(Vector2 Other)
        {
            return Other.X * Y == X * Other.Y;
        }
    }
}
