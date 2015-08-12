using System;
using Freemwork.Utilities;

namespace Freemwork.Primitives.Math
{
    // ReSharper disable once InconsistentNaming
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        #region Public Constructors

        public Matrix4x4(float M11, float M12, float M13, float M14,
            float M21, float M22, float M23, float M24,
            float M31, float M32, float M33, float M34,
            float M41, float M42, float M43, float M44)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;
            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;
            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;
            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
        }

        #endregion Public Constructors


        #region Public Fields

        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        #endregion Public Fields


        #region Private Members

        private static Matrix4x4 identity = new Matrix4x4(1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f);

        #endregion Private Members


        #region Public Properties

         public static Matrix4x4 Identity
        {
            get { return identity; }
        }


        public static float[] ToFloatArray(Matrix4x4 Matrix)
        {
            float[] matarray =
            {
                Matrix.M11, Matrix.M12, Matrix.M13, Matrix.M14,
                Matrix.M21, Matrix.M22, Matrix.M23, Matrix.M24,
                Matrix.M31, Matrix.M32, Matrix.M33, Matrix.M34,
                Matrix.M41, Matrix.M42, Matrix.M43, Matrix.M44
            };
            return matarray;
        }

       public Vector3 Translation
        {
            get { return new Vector3(this.M41, this.M42, this.M43); }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }


        public Vector3 Scale
        {
            get { return new Vector3(this.M11, this.M22, this.M33); }
            set
            {
                this.M11 = value.X;
                this.M22 = value.Y;
                this.M33 = value.Z;
            }
        }

        #endregion Public Properties


        #region Public Methods

        public static Matrix4x4 Add(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            Matrix1.M11 += Matrix2.M11;
            Matrix1.M12 += Matrix2.M12;
            Matrix1.M13 += Matrix2.M13;
            Matrix1.M14 += Matrix2.M14;
            Matrix1.M21 += Matrix2.M21;
            Matrix1.M22 += Matrix2.M22;
            Matrix1.M23 += Matrix2.M23;
            Matrix1.M24 += Matrix2.M24;
            Matrix1.M31 += Matrix2.M31;
            Matrix1.M32 += Matrix2.M32;
            Matrix1.M33 += Matrix2.M33;
            Matrix1.M34 += Matrix2.M34;
            Matrix1.M41 += Matrix2.M41;
            Matrix1.M42 += Matrix2.M42;
            Matrix1.M43 += Matrix2.M43;
            Matrix1.M44 += Matrix2.M44;
            return Matrix1;
        }


        public static void Add(ref Matrix4x4 Matrix1, ref Matrix4x4 Matrix2, out Matrix4x4 Result)
        {
            Result.M11 = Matrix1.M11 + Matrix2.M11;
            Result.M12 = Matrix1.M12 + Matrix2.M12;
            Result.M13 = Matrix1.M13 + Matrix2.M13;
            Result.M14 = Matrix1.M14 + Matrix2.M14;
            Result.M21 = Matrix1.M21 + Matrix2.M21;
            Result.M22 = Matrix1.M22 + Matrix2.M22;
            Result.M23 = Matrix1.M23 + Matrix2.M23;
            Result.M24 = Matrix1.M24 + Matrix2.M24;
            Result.M31 = Matrix1.M31 + Matrix2.M31;
            Result.M32 = Matrix1.M32 + Matrix2.M32;
            Result.M33 = Matrix1.M33 + Matrix2.M33;
            Result.M34 = Matrix1.M34 + Matrix2.M34;
            Result.M41 = Matrix1.M41 + Matrix2.M41;
            Result.M42 = Matrix1.M42 + Matrix2.M42;
            Result.M43 = Matrix1.M43 + Matrix2.M43;
            Result.M44 = Matrix1.M44 + Matrix2.M44;

        }


        public static Matrix4x4 CreateLookAt(Vector3 CameraPosition, Vector3 CameraTarget, Vector3 CameraUpVector)
        {

            Matrix4x4 m = identity;
            var x = new float[3];
            var y = new float[3];
            var z = new float[3];
            float mag;

            z[0] = CameraPosition.X - CameraTarget.X;
            z[1] = CameraPosition.Y - CameraTarget.Y;
            z[2] = CameraPosition.Z - CameraTarget.Z;
            mag = (float)System.Math.Sqrt((z[0] * z[0] + z[1] * z[1] + z[2] * z[2]));
            if (mag > 0)
            {
                z[0] /= mag;
                z[1] /= mag;
                z[2] /= mag;
            }

            // Y vector
            y[0] = CameraUpVector.X;
            y[1] = CameraUpVector.Y;
            y[2] = CameraUpVector.Z;

            // X vector = Y cross Z
            x[0] = y[1] * z[2] - y[2] * z[1];
            x[1] = -y[0] * z[2] + y[2] * z[0];
            x[2] = y[0] * z[1] - y[1] * z[0];

            // Recompute Y = Z cross X
            y[0] = z[1] * x[2] - z[2] * x[1];
            y[1] = -z[0] * x[2] + z[2] * x[0];
            y[2] = z[0] * x[1] - z[1] * x[0];

            mag = (float)System.Math.Sqrt(x[0] * x[0] + x[1] * x[1] + x[2] * x[2]);
            if (mag > 0)
            {
                x[0] /= mag;
                x[1] /= mag;
                x[2] /= mag;
            }

            mag = (float)System.Math.Sqrt(y[0] * y[0] + y[1] * y[1] + y[2] * y[2]);
            if (mag > 0)
            {
                y[0] /= mag;
                y[1] /= mag;
                y[2] /= mag;
            }


            m.M11 = x[0];
            m.M12 = x[1];
            m.M13 = x[2];
            m.M14 = 0.0f;
            m.M21 = y[0];
            m.M22 = y[1];
            m.M23 = y[2];
            m.M24 = 0.0f;
            m.M31 = z[0];
            m.M32 = z[1];
            m.M33 = z[2];
            m.M34 = 0.0f;
            m.M41 = 0.0f;
            m.M42 = 0.0f;
            m.M43 = 0.0f;
            m.M44 = 1.0f;

            return m;
        }

        public static Matrix4x4 CreateOrthographic(float Left, float Right, float Botton, float Top, float ZNear,
            float ZFar)
        {
            float tx = -(Right + Left) / (Right - Left);
            float ty = -(Top + Botton) / (Top - Botton);
            float tz = -(ZFar + ZNear) / (ZFar - ZNear);

            Matrix4x4 m = identity;

            m.M11 = 2.0f / (Right - Left);
            m.M12 = 0;
            m.M13 = 0;
            m.M14 = tx;

            m.M21 = 0;
            m.M22 = 2.0f / (Top - Botton);
            m.M23 = 0;
            m.M24 = ty;

            m.M31 = 0;
            m.M32 = 0;
            m.M33 = -2.0f / (ZFar - ZNear);
            m.M34 = tz;

            m.M41 = 0;
            m.M42 = 0;
            m.M43 = 0;
            m.M44 = 1;

            return m;
        }

        public static Matrix4x4 CreateRotationX(float Radians)
        {
            var returnMatrix = Identity;

            returnMatrix.M22 = Maths.Cos(Radians);
            returnMatrix.M23 = Maths.Sin(Radians);
            returnMatrix.M32 = -returnMatrix.M23;
            returnMatrix.M33 = returnMatrix.M22;

            return returnMatrix;

        }


        public static void CreateRotationX(float Radians, out Matrix4x4 Result)
        {
            Result = Matrix4x4.Identity;

            Result.M22 = Maths.Cos(Radians);
            Result.M23 = Maths.Sin(Radians);
            Result.M32 = -Result.M23;
            Result.M33 = Result.M22;

        }


        public static Matrix4x4 CreateRotationY(float Radians)
        {
            Matrix4x4 returnMatrix = Matrix4x4.Identity;

            returnMatrix.M11 = Maths.Cos(Radians);
            returnMatrix.M13 = Maths.Sin(Radians);
            returnMatrix.M31 = -returnMatrix.M13;
            returnMatrix.M33 = returnMatrix.M11;

            return returnMatrix;
        }


        public static void CreateRotationY(float Radians, out Matrix4x4 Result)
        {
            Result = Matrix4x4.Identity;

            Result.M11 = Maths.Cos(Radians);
            Result.M13 = Maths.Sin(Radians);
            Result.M31 = -Result.M13;
            Result.M33 = Result.M11;
        }


        public static Matrix4x4 CreateRotationZ(float Radians)
        {
            Matrix4x4 returnMatrix = Matrix4x4.Identity;

            returnMatrix.M11 = Maths.Cos(Radians);
            returnMatrix.M12 = Maths.Sin(Radians);
            returnMatrix.M21 = -returnMatrix.M12;
            returnMatrix.M22 = returnMatrix.M11;

            return returnMatrix;
        }


        public static void CreateRotationZ(float Radians, out Matrix4x4 Result)
        {
            Result = Matrix4x4.Identity;

            Result.M11 = Maths.Cos(Radians);
            Result.M12 = Maths.Sin(Radians);
            Result.M21 = -Result.M12;
            Result.M22 = Result.M11;
        }


        public static Matrix4x4 CreateScale(float Scale)
        {
            Matrix4x4 m = Matrix4x4.Identity;
            m.M11 = m.M22 = m.M33 = Scale;
            return m;
        }


        public static void CreateScale(float Scale, out Matrix4x4 Result)
        {
            Result = CreateScale(Scale);
        }


        public static Matrix4x4 CreateScale(float XScale, float YScale, float ZScale)
        {
            Matrix4x4 returnMatrix;
            returnMatrix.M11 = XScale;
            returnMatrix.M12 = 0;
            returnMatrix.M13 = 0;
            returnMatrix.M14 = 0;
            returnMatrix.M21 = 0;
            returnMatrix.M22 = YScale;
            returnMatrix.M23 = 0;
            returnMatrix.M24 = 0;
            returnMatrix.M31 = 0;
            returnMatrix.M32 = 0;
            returnMatrix.M33 = ZScale;
            returnMatrix.M34 = 0;
            returnMatrix.M41 = 0;
            returnMatrix.M42 = 0;
            returnMatrix.M43 = 0;
            returnMatrix.M44 = 1;
            return returnMatrix;
        }


        public static void CreateScale(float XScale, float YScale, float ZScale, out Matrix4x4 Result)
        {
            Result = CreateScale(XScale, YScale, ZScale);
        }


        public static Matrix4x4 CreateScale(Vector3 Scales)
        {
            return CreateScale(Scales.X, Scales.Y, Scales.Z);
        }


        public static void CreateScale(ref Vector3 Scale, out Matrix4x4 Result)
        {
            Result = CreateScale(Scale.X, Scale.Y, Scale.Z);
        }

        public static Matrix4x4 CreateTranslation(float XPosition, float YPosition, float ZPosition)
        {
            Matrix4x4 m = Matrix4x4.Identity;
            m.M41 = XPosition;
            m.M42 = YPosition;
            m.M43 = ZPosition;
            return m;
        }


        public static void CreateTranslation(ref Vector3 Position, out Matrix4x4 Result)
        {
            Result = CreateTranslation(Position.X, Position.Y, Position.Z);
        }


        public static Matrix4x4 CreateTranslation(Vector3 Position)
        {
            return CreateTranslation(Position.X, Position.Y, Position.Z);
        }


        public static void CreateTranslation(float XPosition, float YPosition, float ZPosition, out Matrix4x4 Result)
        {
            Result = CreateTranslation(XPosition, YPosition, ZPosition);
        }

        public bool Equals(Matrix4x4 Other)
        {
            return M11.Equals(Other.M11) && M12.Equals(Other.M12) && M13.Equals(Other.M13) && M14.Equals(Other.M14)
                   && M21.Equals(Other.M21) && M22.Equals(Other.M22) && M23.Equals(Other.M23) && M31.Equals(Other.M31)
                   && M32.Equals(Other.M32) && M24.Equals(Other.M24) && M33.Equals(Other.M33) && M34.Equals(Other.M34)
                   && M41.Equals(Other.M41) && M42.Equals(Other.M42) && M43.Equals(Other.M43) && M44.Equals(Other.M44);
        }

        public override bool Equals(object Obj)
        {
            if (ReferenceEquals(null, Obj)) return false;
            return Obj is Matrix4x4 && Equals((Matrix4x4)Obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = M11.GetHashCode();
                hashCode = (hashCode * 397) ^ M12.GetHashCode();
                hashCode = (hashCode * 397) ^ M13.GetHashCode();
                hashCode = (hashCode * 397) ^ M14.GetHashCode();
                hashCode = (hashCode * 397) ^ M21.GetHashCode();
                hashCode = (hashCode * 397) ^ M22.GetHashCode();
                hashCode = (hashCode * 397) ^ M23.GetHashCode();
                hashCode = (hashCode * 397) ^ M31.GetHashCode();
                hashCode = (hashCode * 397) ^ M32.GetHashCode();
                hashCode = (hashCode * 397) ^ M24.GetHashCode();
                hashCode = (hashCode * 397) ^ M33.GetHashCode();
                hashCode = (hashCode * 397) ^ M34.GetHashCode();
                hashCode = (hashCode * 397) ^ M41.GetHashCode();
                hashCode = (hashCode * 397) ^ M42.GetHashCode();
                hashCode = (hashCode * 397) ^ M43.GetHashCode();
                hashCode = (hashCode * 397) ^ M44.GetHashCode();
                return hashCode;
            }
        }


        public static Matrix4x4 Invert(Matrix4x4 Matrix)
        {
            Invert(ref Matrix, out Matrix);
            return Matrix;

        }


        public static void Invert(ref Matrix4x4 Matrix, out Matrix4x4 Result)
        {
            float det1, det2, det3, det4, det5, det6, det7, det8, det9, det10, det11, det12;
            float detMatrix;
            FindDeterminants(ref Matrix, out detMatrix, out det1, out det2, out det3, out det4, out det5, out det6,
                out det7, out det8, out det9, out det10, out det11, out det12);

            float invDetMatrix = 1f / detMatrix;

            Matrix4x4 ret; // Allow for matrix and Result to point to the same structure

            ret.M11 = (Matrix.M22 * det12 - Matrix.M23 * det11 + Matrix.M24 * det10) * invDetMatrix;
            ret.M12 = (-Matrix.M12 * det12 + Matrix.M13 * det11 - Matrix.M14 * det10) * invDetMatrix;
            ret.M13 = (Matrix.M42 * det6 - Matrix.M43 * det5 + Matrix.M44 * det4) * invDetMatrix;
            ret.M14 = (-Matrix.M32 * det6 + Matrix.M33 * det5 - Matrix.M34 * det4) * invDetMatrix;
            ret.M21 = (-Matrix.M21 * det12 + Matrix.M23 * det9 - Matrix.M24 * det8) * invDetMatrix;
            ret.M22 = (Matrix.M11 * det12 - Matrix.M13 * det9 + Matrix.M14 * det8) * invDetMatrix;
            ret.M23 = (-Matrix.M41 * det6 + Matrix.M43 * det3 - Matrix.M44 * det2) * invDetMatrix;
            ret.M24 = (Matrix.M31 * det6 - Matrix.M33 * det3 + Matrix.M34 * det2) * invDetMatrix;
            ret.M31 = (Matrix.M21 * det11 - Matrix.M22 * det9 + Matrix.M24 * det7) * invDetMatrix;
            ret.M32 = (-Matrix.M11 * det11 + Matrix.M12 * det9 - Matrix.M14 * det7) * invDetMatrix;
            ret.M33 = (Matrix.M41 * det5 - Matrix.M42 * det3 + Matrix.M44 * det1) * invDetMatrix;
            ret.M34 = (-Matrix.M31 * det5 + Matrix.M32 * det3 - Matrix.M34 * det1) * invDetMatrix;
            ret.M41 = (-Matrix.M21 * det10 + Matrix.M22 * det8 - Matrix.M23 * det7) * invDetMatrix;
            ret.M42 = (Matrix.M11 * det10 - Matrix.M12 * det8 + Matrix.M13 * det7) * invDetMatrix;
            ret.M43 = (-Matrix.M41 * det4 + Matrix.M42 * det2 - Matrix.M43 * det1) * invDetMatrix;
            ret.M44 = (Matrix.M31 * det4 - Matrix.M32 * det2 + Matrix.M33 * det1) * invDetMatrix;

            Result = ret;
        }

        public static Matrix4x4 Multiply(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            Matrix4x4 ret;
            Multiply(ref Matrix1, ref Matrix2, out ret);
            return ret;
        }


        public static void Multiply(ref Matrix4x4 Matrix1, ref Matrix4x4 Matrix2, out Matrix4x4 Result)
        {
            Result.M11 = Matrix1.M11 * Matrix2.M11 + Matrix1.M12 * Matrix2.M21 + Matrix1.M13 * Matrix2.M31 +
                         Matrix1.M14 * Matrix2.M41;
            Result.M12 = Matrix1.M11 * Matrix2.M12 + Matrix1.M12 * Matrix2.M22 + Matrix1.M13 * Matrix2.M32 +
                         Matrix1.M14 * Matrix2.M42;
            Result.M13 = Matrix1.M11 * Matrix2.M13 + Matrix1.M12 * Matrix2.M23 + Matrix1.M13 * Matrix2.M33 +
                         Matrix1.M14 * Matrix2.M43;
            Result.M14 = Matrix1.M11 * Matrix2.M14 + Matrix1.M12 * Matrix2.M24 + Matrix1.M13 * Matrix2.M34 +
                         Matrix1.M14 * Matrix2.M44;

            Result.M21 = Matrix1.M21 * Matrix2.M11 + Matrix1.M22 * Matrix2.M21 + Matrix1.M23 * Matrix2.M31 +
                         Matrix1.M24 * Matrix2.M41;
            Result.M22 = Matrix1.M21 * Matrix2.M12 + Matrix1.M22 * Matrix2.M22 + Matrix1.M23 * Matrix2.M32 +
                         Matrix1.M24 * Matrix2.M42;
            Result.M23 = Matrix1.M21 * Matrix2.M13 + Matrix1.M22 * Matrix2.M23 + Matrix1.M23 * Matrix2.M33 +
                         Matrix1.M24 * Matrix2.M43;
            Result.M24 = Matrix1.M21 * Matrix2.M14 + Matrix1.M22 * Matrix2.M24 + Matrix1.M23 * Matrix2.M34 +
                         Matrix1.M24 * Matrix2.M44;

            Result.M31 = Matrix1.M31 * Matrix2.M11 + Matrix1.M32 * Matrix2.M21 + Matrix1.M33 * Matrix2.M31 +
                         Matrix1.M34 * Matrix2.M41;
            Result.M32 = Matrix1.M31 * Matrix2.M12 + Matrix1.M32 * Matrix2.M22 + Matrix1.M33 * Matrix2.M32 +
                         Matrix1.M34 * Matrix2.M42;
            Result.M33 = Matrix1.M31 * Matrix2.M13 + Matrix1.M32 * Matrix2.M23 + Matrix1.M33 * Matrix2.M33 +
                         Matrix1.M34 * Matrix2.M43;
            Result.M34 = Matrix1.M31 * Matrix2.M14 + Matrix1.M32 * Matrix2.M24 + Matrix1.M33 * Matrix2.M34 +
                         Matrix1.M34 * Matrix2.M44;

            Result.M41 = Matrix1.M41 * Matrix2.M11 + Matrix1.M42 * Matrix2.M21 + Matrix1.M43 * Matrix2.M31 +
                         Matrix1.M44 * Matrix2.M41;
            Result.M42 = Matrix1.M41 * Matrix2.M12 + Matrix1.M42 * Matrix2.M22 + Matrix1.M43 * Matrix2.M32 +
                         Matrix1.M44 * Matrix2.M42;
            Result.M43 = Matrix1.M41 * Matrix2.M13 + Matrix1.M42 * Matrix2.M23 + Matrix1.M43 * Matrix2.M33 +
                         Matrix1.M44 * Matrix2.M43;
            Result.M44 = Matrix1.M41 * Matrix2.M14 + Matrix1.M42 * Matrix2.M24 + Matrix1.M43 * Matrix2.M34 +
                         Matrix1.M44 * Matrix2.M44;
        }


        public static Matrix4x4 Multiply(Matrix4x4 Matrix1, float Factor)
        {
            Matrix1.M11 *= Factor;
            Matrix1.M12 *= Factor;
            Matrix1.M13 *= Factor;
            Matrix1.M14 *= Factor;
            Matrix1.M21 *= Factor;
            Matrix1.M22 *= Factor;
            Matrix1.M23 *= Factor;
            Matrix1.M24 *= Factor;
            Matrix1.M31 *= Factor;
            Matrix1.M32 *= Factor;
            Matrix1.M33 *= Factor;
            Matrix1.M34 *= Factor;
            Matrix1.M41 *= Factor;
            Matrix1.M42 *= Factor;
            Matrix1.M43 *= Factor;
            Matrix1.M44 *= Factor;
            return Matrix1;
        }


        public static void Multiply(ref Matrix4x4 Matrix1, float Factor, out Matrix4x4 Result)
        {
            Result.M11 = Matrix1.M11 * Factor;
            Result.M12 = Matrix1.M12 * Factor;
            Result.M13 = Matrix1.M13 * Factor;
            Result.M14 = Matrix1.M14 * Factor;
            Result.M21 = Matrix1.M21 * Factor;
            Result.M22 = Matrix1.M22 * Factor;
            Result.M23 = Matrix1.M23 * Factor;
            Result.M24 = Matrix1.M24 * Factor;
            Result.M31 = Matrix1.M31 * Factor;
            Result.M32 = Matrix1.M32 * Factor;
            Result.M33 = Matrix1.M33 * Factor;
            Result.M34 = Matrix1.M34 * Factor;
            Result.M41 = Matrix1.M41 * Factor;
            Result.M42 = Matrix1.M42 * Factor;
            Result.M43 = Matrix1.M43 * Factor;
            Result.M44 = Matrix1.M44 * Factor;

        }

        public static Matrix4x4 operator +(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            Matrix4x4.Add(ref Matrix1, ref Matrix2, out Matrix1);
            return Matrix1;
        }

        public static Matrix4x4 operator /(Matrix4x4 Matrix1, float Divider)
        {
            return Multiply(Matrix1, 1f / Divider);
        }


        public static bool operator ==(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            return (
                Matrix1.M11 == Matrix2.M11 &&
                Matrix1.M12 == Matrix2.M12 &&
                Matrix1.M13 == Matrix2.M13 &&
                Matrix1.M14 == Matrix2.M14 &&
                Matrix1.M21 == Matrix2.M21 &&
                Matrix1.M22 == Matrix2.M22 &&
                Matrix1.M23 == Matrix2.M23 &&
                Matrix1.M24 == Matrix2.M24 &&
                Matrix1.M31 == Matrix2.M31 &&
                Matrix1.M32 == Matrix2.M32 &&
                Matrix1.M33 == Matrix2.M33 &&
                Matrix1.M34 == Matrix2.M34 &&
                Matrix1.M41 == Matrix2.M41 &&
                Matrix1.M42 == Matrix2.M42 &&
                Matrix1.M43 == Matrix2.M43 &&
                Matrix1.M44 == Matrix2.M44
                );
        }


        public static bool operator !=(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            return (
                Matrix1.M11 != Matrix2.M11 ||
                Matrix1.M12 != Matrix2.M12 ||
                Matrix1.M13 != Matrix2.M13 ||
                Matrix1.M14 != Matrix2.M14 ||
                Matrix1.M21 != Matrix2.M21 ||
                Matrix1.M22 != Matrix2.M22 ||
                Matrix1.M23 != Matrix2.M23 ||
                Matrix1.M24 != Matrix2.M24 ||
                Matrix1.M31 != Matrix2.M31 ||
                Matrix1.M32 != Matrix2.M32 ||
                Matrix1.M33 != Matrix2.M33 ||
                Matrix1.M34 != Matrix2.M34 ||
                Matrix1.M41 != Matrix2.M41 ||
                Matrix1.M42 != Matrix2.M42 ||
                Matrix1.M43 != Matrix2.M43 ||
                Matrix1.M44 != Matrix2.M44
                );
        }


        public static Matrix4x4 operator *(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            Matrix4x4 matrix;
            matrix.M11 = (((Matrix1.M11 * Matrix2.M11) + (Matrix1.M12 * Matrix2.M21)) + (Matrix1.M13 * Matrix2.M31)) +
                         (Matrix1.M14 * Matrix2.M41);
            matrix.M12 = (((Matrix1.M11 * Matrix2.M12) + (Matrix1.M12 * Matrix2.M22)) + (Matrix1.M13 * Matrix2.M32)) +
                         (Matrix1.M14 * Matrix2.M42);
            matrix.M13 = (((Matrix1.M11 * Matrix2.M13) + (Matrix1.M12 * Matrix2.M23)) + (Matrix1.M13 * Matrix2.M33)) +
                         (Matrix1.M14 * Matrix2.M43);
            matrix.M14 = (((Matrix1.M11 * Matrix2.M14) + (Matrix1.M12 * Matrix2.M24)) + (Matrix1.M13 * Matrix2.M34)) +
                         (Matrix1.M14 * Matrix2.M44);
            matrix.M21 = (((Matrix1.M21 * Matrix2.M11) + (Matrix1.M22 * Matrix2.M21)) + (Matrix1.M23 * Matrix2.M31)) +
                         (Matrix1.M24 * Matrix2.M41);
            matrix.M22 = (((Matrix1.M21 * Matrix2.M12) + (Matrix1.M22 * Matrix2.M22)) + (Matrix1.M23 * Matrix2.M32)) +
                         (Matrix1.M24 * Matrix2.M42);
            matrix.M23 = (((Matrix1.M21 * Matrix2.M13) + (Matrix1.M22 * Matrix2.M23)) + (Matrix1.M23 * Matrix2.M33)) +
                         (Matrix1.M24 * Matrix2.M43);
            matrix.M24 = (((Matrix1.M21 * Matrix2.M14) + (Matrix1.M22 * Matrix2.M24)) + (Matrix1.M23 * Matrix2.M34)) +
                         (Matrix1.M24 * Matrix2.M44);
            matrix.M31 = (((Matrix1.M31 * Matrix2.M11) + (Matrix1.M32 * Matrix2.M21)) + (Matrix1.M33 * Matrix2.M31)) +
                         (Matrix1.M34 * Matrix2.M41);
            matrix.M32 = (((Matrix1.M31 * Matrix2.M12) + (Matrix1.M32 * Matrix2.M22)) + (Matrix1.M33 * Matrix2.M32)) +
                         (Matrix1.M34 * Matrix2.M42);
            matrix.M33 = (((Matrix1.M31 * Matrix2.M13) + (Matrix1.M32 * Matrix2.M23)) + (Matrix1.M33 * Matrix2.M33)) +
                         (Matrix1.M34 * Matrix2.M43);
            matrix.M34 = (((Matrix1.M31 * Matrix2.M14) + (Matrix1.M32 * Matrix2.M24)) + (Matrix1.M33 * Matrix2.M34)) +
                         (Matrix1.M34 * Matrix2.M44);
            matrix.M41 = (((Matrix1.M41 * Matrix2.M11) + (Matrix1.M42 * Matrix2.M21)) + (Matrix1.M43 * Matrix2.M31)) +
                         (Matrix1.M44 * Matrix2.M41);
            matrix.M42 = (((Matrix1.M41 * Matrix2.M12) + (Matrix1.M42 * Matrix2.M22)) + (Matrix1.M43 * Matrix2.M32)) +
                         (Matrix1.M44 * Matrix2.M42);
            matrix.M43 = (((Matrix1.M41 * Matrix2.M13) + (Matrix1.M42 * Matrix2.M23)) + (Matrix1.M43 * Matrix2.M33)) +
                         (Matrix1.M44 * Matrix2.M43);
            matrix.M44 = (((Matrix1.M41 * Matrix2.M14) + (Matrix1.M42 * Matrix2.M24)) + (Matrix1.M43 * Matrix2.M34)) +
                         (Matrix1.M44 * Matrix2.M44);
            return matrix;
        }


        public static Matrix4x4 operator *(Matrix4x4 Matrix, float ScaleFactor)
        {
            return Multiply(Matrix, ScaleFactor);
        }

        public static Matrix4x4 operator *(float ScaleFactor, Matrix4x4 Matrix)
        {
            return Multiply(Matrix, ScaleFactor);
        }


        public static Matrix4x4 operator -(Matrix4x4 Matrix1, Matrix4x4 Matrix2)
        {
            return Matrix1 + Matrix2 * (-1f);
        }


        public static Matrix4x4 operator -(Matrix4x4 Matrix1)
        {
            return Matrix1 * (-1);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}\n{4}{5}{6}{7}\n{8}{9}{10}{11}\n{12}{13}{14}{15}", M11, M12, M13, M14, M21,
                M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }

        public static Matrix4x4 Transpose(Matrix4x4 Matrix)
        {
            Matrix4x4 ret;
            Transpose(ref Matrix, out ret);
            return ret;
        }


        public static void Transpose(ref Matrix4x4 Matrix, out Matrix4x4 Result)
        {
            Result.M11 = Matrix.M11;
            Result.M12 = Matrix.M21;
            Result.M13 = Matrix.M31;
            Result.M14 = Matrix.M41;

            Result.M21 = Matrix.M12;
            Result.M22 = Matrix.M22;
            Result.M23 = Matrix.M32;
            Result.M24 = Matrix.M42;

            Result.M31 = Matrix.M13;
            Result.M32 = Matrix.M23;
            Result.M33 = Matrix.M33;
            Result.M34 = Matrix.M43;

            Result.M41 = Matrix.M14;
            Result.M42 = Matrix.M24;
            Result.M43 = Matrix.M34;
            Result.M44 = Matrix.M44;
        }

        #endregion Public Methods

        #region Private Static Methods

        /// <summary>
        /// Helper method for using the Laplace expansion theorem using two rows expansions to calculate major and 
        /// minor determinants of a 4x4 matrix. This method is used for inverting a matrix.
        /// </summary>
        private static void FindDeterminants(ref Matrix4x4 Matrix, out float Major,
            out float Minor1, out float Minor2, out float Minor3, out float Minor4, out float Minor5, out float Minor6,
            out float Minor7, out float Minor8, out float Minor9, out float Minor10, out float Minor11,
            out float Minor12)
        {
            double det1 = Matrix.M11 * Matrix.M22 - Matrix.M12 * Matrix.M21;
            double det2 = Matrix.M11 * Matrix.M23 - Matrix.M13 * Matrix.M21;
            double det3 = Matrix.M11 * Matrix.M24 - Matrix.M14 * Matrix.M21;
            double det4 = Matrix.M12 * Matrix.M23 - Matrix.M13 * Matrix.M22;
            double det5 = Matrix.M12 * Matrix.M24 - Matrix.M14 * Matrix.M22;
            double det6 = Matrix.M13 * Matrix.M24 - Matrix.M14 * Matrix.M23;
            double det7 = Matrix.M31 * Matrix.M42 - Matrix.M32 * Matrix.M41;
            double det8 = Matrix.M31 * Matrix.M43 - Matrix.M33 * Matrix.M41;
            double det9 = Matrix.M31 * Matrix.M44 - Matrix.M34 * Matrix.M41;
            double det10 = Matrix.M32 * Matrix.M43 - Matrix.M33 * Matrix.M42;
            double det11 = Matrix.M32 * Matrix.M44 - Matrix.M34 * Matrix.M42;
            double det12 = Matrix.M33 * Matrix.M44 - Matrix.M34 * Matrix.M43;

            Major = (float)(det1 * det12 - det2 * det11 + det3 * det10 + det4 * det9 - det5 * det8 + det6 * det7);
            Minor1 = (float)det1;
            Minor2 = (float)det2;
            Minor3 = (float)det3;
            Minor4 = (float)det4;
            Minor5 = (float)det5;
            Minor6 = (float)det6;
            Minor7 = (float)det7;
            Minor8 = (float)det8;
            Minor9 = (float)det9;
            Minor10 = (float)det10;
            Minor11 = (float)det11;
            Minor12 = (float)det12;
        }

        #endregion Private Static Methods
    }
}
