using System;

namespace Freemwork.Primitives.Math
{
    public struct Rectangle<T> : IEquatable<Rectangle<T>> where T : struct, IEquatable<T>
    {
        public T X;
        public T Y;
        public T Width;
        public T Height;

        public T Top { get { return Y; } }
        public T Bottom 
        { 
            get
            {
                dynamic y = Y;
                dynamic h = Height;

                return y + h; 
            } 
        }
        public T Left { get { return X; } }
        public T Right 
        { 
            get
            {
                dynamic x = X;
                dynamic w = Width;

                return x + w;
            } 
        }
        public Size2D<T> Size
        {
            get { return new Size2D<T>(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }
        

        public Rectangle(T X, T Y, T Width, T Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        public bool Equals(Rectangle<T> Other)
        {
            return X.Equals(Other.X) && 
                   Y.Equals(Other.Y) && 
                   Width.Equals(Other.Width) && 
                   Height.Equals(Other.Height);
        }

        public bool Intersects(Rectangle<T> BoundingBox)
        {
            dynamic x1 = Left;
            dynamic x2 = Right;
            dynamic y1 = Top;
            dynamic y2 = Bottom;

            dynamic x1A = BoundingBox.Left;
            dynamic x2A = BoundingBox.Right;
            dynamic y1A = BoundingBox.Top;
            dynamic y2A = BoundingBox.Bottom;


            return (x2A >= x1 && x1A <= x2) && (y2A >= y1 && y1A <= y2);
        }

        public static Rectangle<T> Offset(Rectangle<T> Rectangle, T X, T Y)
        {
            dynamic x = Rectangle.X;
            dynamic y = Rectangle.Y;
            dynamic nX = X + x;
            dynamic nY = Y + y;

            return new Rectangle<T>(nX, nY, Rectangle.Width, Rectangle.Height);
        }

        public Rectangle<T> Offset(T OffsetX, T OffsetY)
        {
            dynamic x = X;
            dynamic y = Y;
            dynamic nX = x + OffsetX;
            dynamic nY = y + OffsetY;

            return new Rectangle<T>(nX, nY, Width, Height);
        }
    }
}
