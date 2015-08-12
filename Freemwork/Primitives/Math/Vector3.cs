using System;
using System.Globalization;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    public struct Vector3 : IEquatable<Vector3>
    {
        private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);
        private static readonly Vector3 unitVector = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 unitXVector = new Vector3(1f, 0f, 0f);
        private static readonly Vector3 unitYVector = new Vector3(0f, 1f, 0f);
        private static readonly Vector3 unitZVector = new Vector3(0f, 0f, 1f);

        public float X;
        public float Y;
        public float Z;

        public static Vector3 Zero { get { return zeroVector; } }
        public static Vector3 One { get { return unitVector; } }
        public static Vector3 UnitX { get { return unitXVector; } }
        public static Vector3 UnitY { get { return unitYVector; } }
        public static Vector3 UnitZ { get { return unitZVector; } }

        public Vector3(float X, float Y, float Z) { this.X = X; this.Y = Y; this.Z = Z; }
        public Vector3(float Value) { X = Y = Z = Value; }

       
        public static Vector3 Add(Vector3 Value1, Vector3 Value2)
        {
            Value1.X += Value2.X;
            Value1.Y += Value2.Y;
            Value1.Z += Value2.Z;
            return Value1;
        }

        public static Vector3 Clamp(Vector3 Value1, Vector3 Min, Vector3 Max)
        {
            return new Vector3(
                Maths.Clamp(Value1.X, Min.X, Max.X),
                Maths.Clamp(Value1.Y, Min.Y, Max.Y),
                Maths.Clamp(Value1.Z, Min.Z, Max.Z));
        }

        public static void Clamp(ref Vector3 Value1, ref Vector3 Min, ref Vector3 Max, out Vector3 Result)
        {
            Result.X = Maths.Clamp(Value1.X, Min.X, Max.X);
            Result.Y = Maths.Clamp(Value1.Y, Min.Y, Max.Y);
            Result.Z = Maths.Clamp(Value1.Z, Min.Z, Max.Z);
        }

        public static float Distance(Vector3 Value1, Vector3 Value2)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y, v3 = Value1.Z - Value2.Z;
            return Maths.Sqrt((v1 * v1) + (v2 * v2) + (v3 * v3));
        }

        public static void Distance(ref Vector3 Value1, ref Vector3 Value2, out float Result)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y, v3 = Value1.Z - Value2.Z;
            Result = Maths.Sqrt((v1 * v1) + (v2 * v2) + (v3 * v3));
        }

        public static float DistanceSquared(Vector3 Value1, Vector3 Value2)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y, v3 = Value1.Z - Value2.Z;
            return (v1 * v1) + (v2 * v2) + (v3 * v3);
        }

        public static void DistanceSquared(ref Vector3 Value1, ref Vector3 Value2, out float Result)
        {
            float v1 = Value1.X - Value2.X, v2 = Value1.Y - Value2.Y, v3 = Value1.Z - Value2.Z;
            Result = (v1 * v1) + (v2 * v2) + (v3 * v3);
        }

        public static Vector3 Divide(Vector3 Value1, Vector3 Value2)
        {
            Value1.X /= Value2.X;
            Value1.Y /= Value2.Y;
            Value1.Z /= Value2.Z;
            return Value1;
        }

        public static void Divide(ref Vector3 Value1, ref Vector3 Value2, out Vector3 Result)
        {
            Result.X = Value1.X / Value2.X;
            Result.Y = Value1.Y / Value2.Y;
            Result.Z = Value1.Z / Value2.Z;
        }

        public static Vector3 Divide(Vector3 Value1, float Divider)
        {
            float factor = 1 / Divider;
            Value1.X *= factor;
            Value1.Y *= factor;
            Value1.Z *= factor;
            return Value1;
        }

        public static void Divide(ref Vector3 Value1, float Divider, out Vector3 Result)
        {
            float factor = 1 / Divider;
            Result.X = Value1.X * factor;
            Result.Y = Value1.Y * factor;
            Result.Z = Value1.Z * factor;
        }

        public static float Dot(Vector3 Value1, Vector3 Value2)
        {
            return (Value1.X * Value2.X) + (Value1.Y * Value2.Y) + (Value1.Z * Value2.Z);
        }

        public static void Dot(ref Vector3 Value1, ref Vector3 Value2, out float Result)
        {
            Result = (Value1.X * Value2.X) + (Value1.Y * Value2.Y) + (Value1.Z * Value2.Z);
        }

        public override bool Equals(object Obj)
        {
            if (Obj is Vector3)
            {
                return Equals((Vector3)Obj);
            }

            return false;
        }

        public bool Equals(Vector3 Other)
        {
            return (X == Other.X) && (Y == Other.Y) && (Z == Other.Z);
        }

        public static Vector3 Reflect(Vector3 Vector, Vector3 Normal)
        {
            Vector3 result;
            float val = 2.0f * ((Vector.X * Normal.X) + (Vector.Y * Normal.Y) + (Vector.Z * Normal.Z));
            result.X = Vector.X - (Normal.X * val);
            result.Y = Vector.Y - (Normal.Y * val);
            result.Z = Vector.Z - (Normal.Z * val);
            return result;
        }

        public static void Reflect(ref Vector3 Vector, ref Vector3 Normal, out Vector3 Result)
        {
            float val = 2.0f * ((Vector.X * Normal.X) + (Vector.Y * Normal.Y) + (Vector.Z * Normal.Z));
            Result.X = Vector.X - (Normal.X * val);
            Result.Y = Vector.Y - (Normal.Y * val);
            Result.Z = Vector.Z - (Normal.Z * val);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        public float Length()
        {
            return Maths.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

        public float LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }

        public static Vector3 Lerp(Vector3 Value1, Vector3 Value2, float Amount)
        {
            return new Vector3(
                Maths.Lerp(Value1.X, Value2.X, Amount),
                Maths.Lerp(Value1.Y, Value2.Y, Amount),
                Maths.Lerp(Value1.Z, Value2.Z, Amount));
        }

        public static void Lerp(ref Vector3 Value1, ref Vector3 Value2, float Amount, out Vector3 Result)
        {
            Result.X = Maths.Lerp(Value1.X, Value2.X, Amount);
            Result.Y = Maths.Lerp(Value1.Y, Value2.Y, Amount);
            Result.Z = Maths.Lerp(Value1.Z, Value2.Z, Amount);
        }

        public static Vector3 Max(Vector3 Value1, Vector3 Value2)
        {
            return new Vector3(Value1.X > Value2.X ? Value1.X : Value2.X,
                               Value1.Y > Value2.Y ? Value1.Y : Value2.Y,
                               Value1.Z > Value2.Z ? Value1.Z : Value2.Z);
        }

        public static void Max(ref Vector3 Value1, ref Vector3 Value2, out Vector3 Result)
        {
            Result.X = Value1.X > Value2.X ? Value1.X : Value2.X;
            Result.Y = Value1.Y > Value2.Y ? Value1.Y : Value2.Y;
            Result.Z = Value1.Z > Value2.Z ? Value1.Z : Value2.Z;
        }

        public static Vector3 Min(Vector3 Value1, Vector3 Value2)
        {
            return new Vector3(Value1.X < Value2.X ? Value1.X : Value2.X,
                               Value1.Y < Value2.Y ? Value1.Y : Value2.Y,
                               Value1.Z < Value2.Z ? Value1.Z : Value2.Z);
        }

        public static void Min(ref Vector3 Value1, ref Vector3 Value2, out Vector3 Result)
        {
            Result.X = Value1.X < Value2.X ? Value1.X : Value2.X;
            Result.Y = Value1.Y < Value2.Y ? Value1.Y : Value2.Y;
            Result.Z = Value1.Z < Value2.Z ? Value1.Z : Value2.Z;
        }

        public static Vector3 Multiply(Vector3 Value1, Vector3 Value2)
        {
            Value1.X *= Value2.X;
            Value1.Y *= Value2.Y;
            Value1.Z *= Value2.Z;
            return Value1;
        }

        public static Vector3 Multiply(Vector3 Value1, float ScaleFactor)
        {
            Value1.X *= ScaleFactor;
            Value1.Y *= ScaleFactor;
            Value1.Z *= ScaleFactor;
            return Value1;
        }

        public static void Multiply(ref Vector3 Value1, float ScaleFactor, out Vector3 Result)
        {
            Result.X = Value1.X * ScaleFactor;
            Result.Y = Value1.Y * ScaleFactor;
            Result.Z = Value1.Z * ScaleFactor;
        }

        public static void Multiply(ref Vector3 Value1, ref Vector3 Value2, out Vector3 Result)
        {
            Result.X = Value1.X * Value2.X;
            Result.Y = Value1.Y * Value2.Y;
            Result.Z = Value1.Z * Value2.Z;
        }

        public static Vector3 Negate(Vector3 Value)
        {
            Value.X = -Value.X;
            Value.Y = -Value.Y;
            Value.Z = -Value.Z;
            return Value;
        }

        public static void Negate(ref Vector3 Value, out Vector3 Result)
        {
            Result.X = -Value.X;
            Result.Y = -Value.Y;
            Result.Z = -Value.Z;
        }

        public void Normalize()
        {
            float val = 1.0f / Maths.Sqrt((X * X) + (Y * Y) + (Z * Z));
            X *= val;
            Y *= val;
            Z *= val;
        }

        public static Vector3 Normalize(Vector3 Value)
        {
            float val = 1.0f / Maths.Sqrt((Value.X * Value.X) + (Value.Y * Value.Y) + (Value.Z * Value.Z));
            Value.X *= val;
            Value.Y *= val;
            Value.Z *= val;
            return Value;
        }

        public static void Normalize(ref Vector3 Value, out Vector3 Result)
        {
            float val = 1.0f / Maths.Sqrt((Value.X * Value.X) + (Value.Y * Value.Y) + (Value.Z * Value.Z));
            Result.X = Value.X * val;
            Result.Y = Value.Y * val;
            Result.Z = Value.Z * val;
        }

        public static Vector3 SmoothStep(Vector3 Value1, Vector3 Value2, float Amount)
        {
            return new Vector3(
                Maths.SmoothStep(Value1.X, Value2.X, Amount),
                Maths.SmoothStep(Value1.Y, Value2.Y, Amount),
                Maths.SmoothStep(Value1.Z, Value2.Z, Amount));
        }

        public static void SmoothStep(ref Vector3 Value1, ref Vector3 Value2, float Amount, out Vector3 Result)
        {
            Result.X = Maths.SmoothStep(Value1.X, Value2.X, Amount);
            Result.Y = Maths.SmoothStep(Value1.Y, Value2.Y, Amount);
            Result.Z = Maths.SmoothStep(Value1.Z, Value2.Z, Amount);
        }

        public static Vector3 Subtract(Vector3 Value1, Vector3 Value2)
        {
            Value1.X -= Value2.X;
            Value1.Y -= Value2.Y;
            Value1.Z -= Value2.Z;
            return Value1;
        }

        public static void Subtract(ref Vector3 Value1, ref Vector3 Value2, out Vector3 Result)
        {
            Result.X = Value1.X - Value2.X;
            Result.Y = Value1.Y - Value2.Y;
            Result.Z = Value1.Z - Value2.Z;
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}", new object[] { 
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture), this.Z.ToString(currentCulture) });
        }

        public static Vector3 operator -(Vector3 Value)
        {
            Value.X = -Value.X;
            Value.Y = -Value.Y;
            Value.Z = -Value.Z;
            return Value;
        }


        public static bool operator ==(Vector3 Value1, Vector3 Value2)
        {
            return Value1.X == Value2.X && Value1.Y == Value2.Y && Value1.Z == Value2.Z;
        }


        public static bool operator !=(Vector3 Value1, Vector3 Value2)
        {
            return Value1.X != Value2.X || Value1.Y != Value2.Y || Value1.Z != Value2.Z;
        }


        public static Vector3 operator +(Vector3 Value1, Vector3 Value2)
        {
            Value1.X += Value2.X;
            Value1.Y += Value2.Y;   
            Value1.Z += Value2.Z;
            return Value1;
        }


        public static Vector3 operator -(Vector3 Value1, Vector3 Value2)
        {
            Value1.X -= Value2.X;
            Value1.Y -= Value2.Y;
            Value1.Z -= Value2.Z;
            return Value1;
        }


        public static Vector3 operator *(Vector3 Value1, Vector3 Value2)
        {
            Value1.X *= Value2.X;
            Value1.Y *= Value2.Y;
            Value1.Z *= Value2.Z;
            return Value1;
        }


        public static Vector3 operator *(Vector3 Value, float ScaleFactor)
        {
            Value.X *= ScaleFactor;
            Value.Y *= ScaleFactor;
            Value.Z *= ScaleFactor;
            return Value;
        }


        public static Vector3 operator *(float ScaleFactor, Vector3 Value)
        {
            Value.X *= ScaleFactor;
            Value.Y *= ScaleFactor;
            Value.Z *= ScaleFactor;
            return Value;
        }


        public static Vector3 operator /(Vector3 Value1, Vector3 Value2)
        {
            Value1.X /= Value2.X;
            Value1.Y /= Value2.Y;
            Value1.Z /= Value2.Z;
            return Value1;
        }


        public static Vector3 operator /(Vector3 Value1, float Divider)
        {
            float factor = 1 / Divider;
            Value1.X *= factor;
            Value1.Y *= factor;
            Value1.Z *= factor;
            return Value1;
        }


        public bool ColinearWith(Vector3 Other)
        {
            return Other.X * Y == X * Other.Y && Other.Y * Z == Z * Other.Y;
        }


        public static Vector3 Cross(Vector3 VectM, Vector3 VectN)
        {
            throw new NotImplementedException();
        }
    }
}
