using System;
using Freemwork.Primitives.Math;

namespace Freemwork.Utilities
{
    public static class Maths
    {
        public const float E = (float)Math.E;
        public const float Pi = (float)Math.PI;
        public const float TwoPi = (float)(Math.PI * 2.0);
        public const float PiOver2 = (float)(Math.PI / 2.0);
        public const float PiOver3 = (float)(Math.PI / 3.0);
        public const float PiOver4 = (float)(Math.PI / 4.0);
        public const float PiOver6 = (float)(Math.PI / 6.0);

        public static float Ceil(float Value)
        {
            return (float)Math.Ceiling(Value);
        }

        public static Rectangle<float> Transform(this Rectangle<float> Self, Transform2D Transform)
        {
            var angle = Transform.Rotation;
            var width = Self.Width * Transform.Scale.X;
            var height = Self.Height * Transform.Scale.Y;

            var sin = Sin(angle);
            var cos = Cos(angle); //Could use sqrt(1 - sin²)
            var nWidth = height * Abs(sin) + width * Abs(cos);
            var nHeight = height * Abs(cos) + width * Abs(sin);

            var origin = Transform.Position + (new Vector2(Self.X, Self.Y) * Transform.Scale) + 0.5f * (new Vector2(width - nWidth, height - nHeight));

            return new Rectangle<float>(origin.X, origin.Y, nWidth, nHeight);
        }

        public static float Round(float Self)
        {
            return Self < 0 ? Floor(Self) : Ceil(Self);
        }

        public static Rectangle<float> Offset(this Rectangle<float> Self, Vector2 Offset)
        {
            return Self.Offset(Offset.X, Offset.Y);
        }

        public static float Barycentric(float Value1, float Value2, float Value3, float Amount1, float Amount2)
        {
            return Value1 + (Value2 - Value1) * Amount1 + (Value3 - Value1) * Amount2;
        }

        public static float Abs(float Value)
        {
            return Value > 0 ? Value : -Value;
        }

        public static T Abs<T>(T Value)
        {
            return (dynamic)(Value) > 0 ? Value : -((dynamic)Value);
        }

        public static T Lerp<T>(T Min, T Max, float Amount)
        {
            dynamic min = Min;
            dynamic max = Max;
            return (T)(min + ((max - min) * Amount));
        }
        
        public static float Lerp(float Value1, float Value2, float Amount) { return Value1 + (Value2 - Value1) * Amount; }
        
        public static float CatmullRom(float Value1, float Value2, float Value3, float Value4, float Amount)
        {        
            double amountSquared = Amount * Amount;
            double amountCubed = amountSquared * Amount;
            return (float)(0.5 * (2.0 * Value2 +
                (Value3 - Value1) * Amount +
                (2.0 * Value1 - 5.0 * Value2 + 4.0 * Value3 - Value4) * amountSquared +
                (3.0 * Value2 - Value1 - 3.0 * Value3 + Value4) * amountCubed));
        }

        public static int Sign(float Number)
        {
            return Number > 0 ? 1 : Number < 0 ? -1 : 0; //Math.Sign(Number);
        }

        public static float Pow(float Number, float Exponent)
        {
            return (float)Math.Pow(Number, Exponent);
        }

        public static bool IsAligned(Vector2 A, Vector2 B, Vector2 C)
        {
            return ((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) == 0;
        }

        public static bool In(this float Self, float Min, float Max, bool LeftExclusive = false, bool RightExclusive = false)
        {
            var min = Max > Min ? Max : Min;
            var max = Max > Min ? Max : Min;
            return (LeftExclusive ? Self > min : Self >= min) && (RightExclusive ? Self < max : Self <= max);
        }

        public static bool IsInsideSegment(Vector2 A, Vector2 C, Vector2 M)
        {
            var test = (M.X - A.X) / (C.X - A.X);
            return IsAligned(A, C, M) && test.In(0, 1);
        }

        public static int BinomialCoefficient(int K, int N)
        {
            if (K < 0 || K > N)
                return 0;
            if (K == 0 || K == N)
                return 1;

            var c = 1;
            var a = 1;

            for (int i = N - K; i != N + 1; i++)
                c *= i;
            for (int i = 1; i != K + 1; i++)
                a *= i;

            return c/a;
        }

        public static float Sqrt(float Value)
        {
            return (float)Math.Sqrt(Value);
        }

        public static double Sqrt(double Value)
        {
            return Math.Sqrt(Value);
        }

        public static float Cos(float Radians)
        {
            return (float)Math.Cos(Radians);
        }
        
        public static float Sin(float Radians)
        {
            return (float)Math.Sin(Radians);
        }

        public static float Clamp(float Value, float Min, float Max)
        {
            Value = (Value > Max) ? Max : Value;
            Value = (Value < Min) ? Min : Value;
            return Value;
        }

        public static int Clamp(int Value, int Min, int Max)
        {
            Value = (Value > Max) ? Max : Value;
            Value = (Value < Min) ? Min : Value;
            return Value;
        }

        public static T Clamp<T>(T Value, T Min, T Max)
        {
            dynamic min = Min;
            dynamic max = Max;

            Value = (Value > max) ? max : Value;
            Value = (Value < min) ? min : Value;
            return Value;
        }

        public static float Distance(float Value1, float Value2)
        {
            return Math.Abs(Value1 - Value2);
        }

        public static float Hermite(float Value1, float Tangent1, float Value2, float Tangent2, float Amount)
        {
            double v1 = Value1, v2 = Value2, t1 = Tangent1, t2 = Tangent2, s = Amount, result;
            double sCubed = s * s * s;
            double sSquared = s * s;

            if (Amount == 0f)
                result = Value1;
            else if (Amount == 1f)
                result = Value2;
            else
                result = (2 * v1 - 2 * v2 + t2 + t1) * sCubed +
                    (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared +
                    t1 * s +
                    v1;
            return (float)result;
        }

        public static float Max(float Value1, float Value2) { return Math.Max(Value1, Value2); }
        public static float Min(float Value1, float Value2) { return Math.Min(Value1, Value2); }

        public static T Max<T>(T Value1, T Value2) { return ((dynamic)Value1) > ((dynamic)Value2) ? Value1 : Value2; }
        public static T Min<T>(T Value1, T Value2) { return ((dynamic)Value1) < ((dynamic)Value2) ? Value1 : Value2; }



        public static float SmoothStep(float Value1, float Value2, float Amount) 
        {
            float result = Maths.Clamp(Amount, 0f, 1f);
            result = Maths.Hermite(Value1, 0f, Value2, 0f, result);
            return result;
        }

        public static float ToDegrees(float Radians) { return (float)(Radians * 57.295779513082320876798154814105); }
        public static float ToRadians(float Degrees) { return (float)(Degrees * 0.017453292519943295769236907684886); }

        public static float WrapAngle(float Angle)
        {
            Angle = (float)Math.IEEERemainder(Angle, 6.2831854820251465);
            if (Angle <= -3.14159274f)
                Angle += 6.28318548f;
            else
                if (Angle > 3.14159274f)
                    Angle -= 6.28318548f;
            return Angle;
        }

        public static bool IsPowerOfTwo(int Value)
        {
            return (Value > 0) && ((Value & (Value - 1)) == 0);
        }


        public static float Floor(float Self)
        {
            return (float)Math.Floor(Self);
        }
    }
}
