using System;

namespace Freemwork.Primitives.Math
{
    public struct Size2D<T> : IEquatable<Size2D<T>> where T : struct, IEquatable<T>
    {
        public T Width;
        public T Height;

        public Size2D(T Width, T Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public bool Equals(Size2D<T> Other)
        {
            return Width.Equals(Other.Width) &&
                   Height.Equals(Other.Height);
        }
    }
}
