using System;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    // ReSharper disable once InconsistentNaming
    public struct Matrix3x3 : IEquatable<Matrix4x4>
    {
        #region Public Constructors

        public Matrix3x3(float M11, float M12, float M13,  
            float M21, float M22, float M23,
            float M31, float M32, float M33)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
        }

        #endregion Public Constructors


        #region Public Fields

        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;

        #endregion Public Fields


        #region Private Members

        private static Matrix3x3 identity = new Matrix3x3(1f, 0f, 0f, 
            0f, 1f, 0f, 
            0f, 0f, 1f);

        #endregion Private Members


        #region Public Properties

        
        
        public static Matrix3x3 Identity
        {
            get { return identity; }
        }


        public static float[] ToFloatArray(Matrix3x3 Matrix)
        {
            float[] matarray =
            {
                Matrix.M11, Matrix.M12, Matrix.M13, 
                Matrix.M21, Matrix.M22, Matrix.M23, 
                Matrix.M31, Matrix.M32, Matrix.M33
            };
            return matarray;
        }

        public Vector2 Translation
        {
            get { return new Vector2(this.M31, this.M32); }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
            }
        }


        public Vector2 Scale
        {
            get { return new Vector2(this.M11, this.M22); }
            set
            {
                this.M11 = value.X;
                this.M22 = value.Y;
            }
        }

        #endregion Public Properties


        #region Public Methods

        public static Matrix3x3 Add(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            Matrix1.M11 += Matrix2.M11;
            Matrix1.M12 += Matrix2.M12;
            Matrix1.M13 += Matrix2.M13;
            Matrix1.M21 += Matrix2.M21;
            Matrix1.M22 += Matrix2.M22;
            Matrix1.M23 += Matrix2.M23;
            Matrix1.M31 += Matrix2.M31;
            Matrix1.M32 += Matrix2.M32;
            Matrix1.M33 += Matrix2.M33;
            return Matrix1;
        }


        public static void Add(ref Matrix3x3 Matrix1, ref Matrix3x3 Matrix2, out Matrix3x3 Result)
        {
            Result.M11 = Matrix1.M11 + Matrix2.M11;
            Result.M12 = Matrix1.M12 + Matrix2.M12;
            Result.M13 = Matrix1.M13 + Matrix2.M13;
            Result.M21 = Matrix1.M21 + Matrix2.M21;
            Result.M22 = Matrix1.M22 + Matrix2.M22;
            Result.M23 = Matrix1.M23 + Matrix2.M23;
            Result.M31 = Matrix1.M31 + Matrix2.M31;
            Result.M32 = Matrix1.M32 + Matrix2.M32;
            Result.M33 = Matrix1.M33 + Matrix2.M33;

        }

        public static Matrix3x3 CreateRotation(float Radians)
        {
            var returnMatrix = Identity;

            returnMatrix.M11 = Maths.Cos(Radians);
            returnMatrix.M12 = Maths.Sin(Radians);
            returnMatrix.M21 = -returnMatrix.M12;
            returnMatrix.M22 = returnMatrix.M11;

            return returnMatrix;

        }



        public static Matrix3x3 CreateScale(float Scale)
        {
            Matrix3x3 m = Matrix3x3.Identity;
            m.M11 = m.M22 = Scale;
            return m;
        }


        public static void CreateScale(float Scale, out Matrix3x3 Result)
        {
            Result = CreateScale(Scale);
        }


        public static Matrix3x3 CreateScale(float XScale, float YScale)
        {
            Matrix3x3 returnMatrix;
            returnMatrix.M11 = XScale;
            returnMatrix.M12 = 0;
            returnMatrix.M13 = 0;
            returnMatrix.M21 = 0;
            returnMatrix.M22 = YScale;
            returnMatrix.M23 = 0;
            returnMatrix.M31 = 0;
            returnMatrix.M32 = 0;
            returnMatrix.M33 = 1;
            return returnMatrix;
        }


        public static void CreateScale(float XScale, float YScale, out Matrix3x3 Result)
        {
            Result = CreateScale(XScale, YScale);
        }


        public static Matrix3x3 CreateScale(Vector2 Scales)
        {
            return CreateScale(Scales.X, Scales.Y);
        }


        public static void CreateScale(ref Vector2 Scale, out Matrix3x3 Result)
        {
            Result = CreateScale(Scale.X, Scale.Y);
        }

        public static Matrix3x3 CreateTranslation(float XPosition, float YPosition)
        {
            var m = Matrix3x3.Identity;
            m.M31 = XPosition;
            m.M32 = YPosition;
            return m;
        }


        public static void CreateTranslation(ref Vector3 Position, out Matrix3x3 Result)
        {
            Result = CreateTranslation(Position.X, Position.Y);
        }


        public static Matrix3x3 CreateTranslation(Vector2 Position)
        {
            return CreateTranslation(Position.X, Position.Y);
        }


        public static void CreateTranslation(float XPosition, float YPosition, out Matrix3x3 Result)
        {
            Result = CreateTranslation(XPosition, YPosition);
        }

        public bool Equals(Matrix4x4 Other)
        {
            return M11.Equals(Other.M11) && M12.Equals(Other.M12) && M13.Equals(Other.M13)
                   && M21.Equals(Other.M21) && M22.Equals(Other.M22) && M23.Equals(Other.M23) && M31.Equals(Other.M31)
                   && M32.Equals(Other.M32) && M33.Equals(Other.M33);
        }

        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            return Obj is Matrix3x3 && Equals((Matrix3x3)Obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = M11.GetHashCode();
                hashCode = (hashCode * 397) ^ M12.GetHashCode();
                hashCode = (hashCode * 397) ^ M13.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                hashCode = (hashCode * 397) ^ M23.GetHashCode();
                hashCode = (hashCode * 397) ^ M31.GetHashCode();
                hashCode = (hashCode * 397) ^ M32.GetHashCode();
                hashCode = (hashCode * 397) ^ M33.GetHashCode();
                return hashCode;
            }
        }


        public static Matrix3x3 Invert(Matrix3x3 Matrix)
        {
            Invert(ref Matrix, out Matrix);
            return Matrix;

        }


        public static void Invert(ref Matrix3x3 Matrix, out Matrix3x3 Result)
        {
            var a = Matrix.M11;
            var b = Matrix.M12;
            var c = Matrix.M13;
            var d = Matrix.M21;
            var e = Matrix.M22;
            var f = Matrix.M23;
            var g = Matrix.M31;
            var h = Matrix.M32;
            var i = Matrix.M33;

            var det = a*e*i + b*f*g + c*d*h - c*e*g - f*h*a - i*b*d;
            var ret = new Matrix3x3(e*i - f*h, c*h - b*i, b*f - c*e, 
                f * g - d*i, a*i - c*g, c*d - a*f,
                d * h - e * g, b * g - a * h, a * e - b * d) * (1f / det);
            
            Result = ret;
        }

        public static Matrix3x3 Multiply(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            Matrix3x3 ret;
            Multiply(ref Matrix1, ref Matrix2, out ret);
            return ret;
        }


        public static void Multiply(ref Matrix3x3 Matrix1, ref Matrix3x3 Matrix2, out Matrix3x3 Result)
        {
            Result.M11 = Matrix1.M11 * Matrix2.M11 + Matrix1.M12 * Matrix2.M21 + Matrix1.M13 * Matrix2.M31;
            Result.M12 = Matrix1.M11 * Matrix2.M12 + Matrix1.M12 * Matrix2.M22 + Matrix1.M13 * Matrix2.M32;
            Result.M13 = Matrix1.M11 * Matrix2.M13 + Matrix1.M12 * Matrix2.M23 + Matrix1.M13 * Matrix2.M33;

            Result.M21 = Matrix1.M21 * Matrix2.M11 + Matrix1.M22 * Matrix2.M21 + Matrix1.M23 * Matrix2.M31;
            Result.M22 = Matrix1.M21 * Matrix2.M12 + Matrix1.M22 * Matrix2.M22 + Matrix1.M23 * Matrix2.M32;
            Result.M23 = Matrix1.M21 * Matrix2.M13 + Matrix1.M22 * Matrix2.M23 + Matrix1.M23 * Matrix2.M33;

            Result.M31 = Matrix1.M31 * Matrix2.M11 + Matrix1.M32 * Matrix2.M21 + Matrix1.M33 * Matrix2.M31;
            Result.M32 = Matrix1.M31 * Matrix2.M12 + Matrix1.M32 * Matrix2.M22 + Matrix1.M33 * Matrix2.M32;
            Result.M33 = Matrix1.M31 * Matrix2.M13 + Matrix1.M32 * Matrix2.M23 + Matrix1.M33 * Matrix2.M33;
        }


        public static Matrix3x3 Multiply(Matrix3x3 Matrix1, float Factor)
        {
            Matrix1.M11 *= Factor;
            Matrix1.M12 *= Factor;
            Matrix1.M13 *= Factor;
            Matrix1.M21 *= Factor;
            Matrix1.M22 *= Factor;
            Matrix1.M23 *= Factor;
            Matrix1.M31 *= Factor;
            Matrix1.M32 *= Factor;
            Matrix1.M33 *= Factor;
            return Matrix1;
        }


        public static void Multiply(ref Matrix3x3 Matrix1, float Factor, out Matrix3x3 Result)
        {
            Result.M11 = Matrix1.M11 * Factor;
            Result.M12 = Matrix1.M12 * Factor;
            Result.M13 = Matrix1.M13 * Factor;
            Result.M21 = Matrix1.M21 * Factor;
            Result.M22 = Matrix1.M22 * Factor;
            Result.M23 = Matrix1.M23 * Factor;
            Result.M31 = Matrix1.M31 * Factor;
            Result.M32 = Matrix1.M32 * Factor;
            Result.M33 = Matrix1.M33 * Factor;

        }

        public static Matrix3x3 operator +(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            Matrix3x3.Add(ref Matrix1, ref Matrix2, out Matrix1);
            return Matrix1;
        }

        public static Matrix3x3 operator /(Matrix3x3 Matrix1, float Divider)
        {
            return Multiply(Matrix1, 1f / Divider);
        }


        public static bool operator ==(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            return (
                Matrix1.M11 == Matrix2.M11 &&
                Matrix1.M12 == Matrix2.M12 &&
                Matrix1.M13 == Matrix2.M13 &&
                Matrix1.M21 == Matrix2.M21 &&
                Matrix1.M22 == Matrix2.M22 &&
                Matrix1.M23 == Matrix2.M23 &&
                Matrix1.M31 == Matrix2.M31 &&
                Matrix1.M32 == Matrix2.M32 &&
                Matrix1.M33 == Matrix2.M33
                );
        }


        public static bool operator !=(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            return (
                Matrix1.M11 != Matrix2.M11 ||
                Matrix1.M12 != Matrix2.M12 ||
                Matrix1.M13 != Matrix2.M13 ||
                Matrix1.M21 != Matrix2.M21 ||
                Matrix1.M22 != Matrix2.M22 ||
                Matrix1.M23 != Matrix2.M23 ||
                Matrix1.M31 != Matrix2.M31 ||
                Matrix1.M32 != Matrix2.M32 ||
                Matrix1.M33 != Matrix2.M33
                );
        }


        public static Matrix3x3 operator *(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            return Multiply(Matrix1, Matrix2);
        }


        public static Matrix3x3 operator *(Matrix3x3 Matrix, float ScaleFactor)
        {
            return Multiply(Matrix, ScaleFactor);
        }

        public static Matrix3x3 operator *(float ScaleFactor, Matrix3x3 Matrix)
        {
            return Multiply(Matrix, ScaleFactor);
        }


        public static Matrix3x3 operator -(Matrix3x3 Matrix1, Matrix3x3 Matrix2)
        {
            return Matrix1 + Matrix2 * (-1f);
        }


        public static Matrix3x3 operator -(Matrix3x3 Matrix1)
        {
            return Matrix1 * (-1);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}\n{3}{4}{5}\n{6}{7}{8}", M11, M12, M13, M21,
                M22, M23, M31, M32, M33);
        }

        public static Matrix3x3 Transpose(Matrix3x3 Matrix)
        {
            Matrix3x3 ret;
            Transpose(ref Matrix, out ret);
            return ret;
        }


        public static void Transpose(ref Matrix3x3 Matrix, out Matrix3x3 Result)
        {
            Result.M11 = Matrix.M11;
            Result.M12 = Matrix.M21;
            Result.M13 = Matrix.M31;

            Result.M21 = Matrix.M12;
            Result.M22 = Matrix.M22;
            Result.M23 = Matrix.M32;

            Result.M31 = Matrix.M13;
            Result.M32 = Matrix.M23;
            Result.M33 = Matrix.M33;
        }

        #endregion Public Methods

        #region Private Static Methods

       
        #endregion Private Static Methods
    }
}
