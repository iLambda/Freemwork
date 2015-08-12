using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Windows.Foundation;
using Freemwork.Primitives.Math;

namespace Freemwork.Utilities
{
    public static class Extensions
    {
        public static String FormatString(this String Self, params Object[] Parameters)
        {
            return String.Format(Self, Parameters);
        }

        public static int IntegerPart(this float Self)
        {
            return (int)Math.Floor(Self);
        }

        public static float Floor(this float Self)
        {
            return (float)Math.Floor(Self);
        }

        public static int IntegerPart(this double Self)
        {
            return (int)Math.Floor(Self);
        }

        public static float Pow(this float Number, float Exponent)
        {
            return (float)Math.Pow(Number, Exponent);
        }

        public static bool IsInteger(this float Self)
        {
            return Self == Self.Floor();
        }

        public static float Floor(this double Self)
        {
            return (float)Math.Floor(Self);
        }

        public static void ForEach<T>(this IEnumerable<T> Self, Action<T> Predicate)
        {
            foreach (var item in Self)
                Predicate(item);
        }

        public static bool Contains(this Rectangle<int> Rectangle, Vector2 Vector2)
        {
            return Vector2.X >= Rectangle.X &&
                   Vector2.X <= Rectangle.X + Rectangle.Width &&
                   Vector2.Y >= Rectangle.Y &&
                   Vector2.Y <= Rectangle.Y + Rectangle.Height;

        }

        public static bool Contains(this Rectangle<float> Rectangle, Vector2 Vector2)
        {
            return Vector2.X >= Rectangle.X &&
                   Vector2.X <= Rectangle.X + Rectangle.Width &&
                   Vector2.Y >= Rectangle.Y &&
                   Vector2.Y <= Rectangle.Y + Rectangle.Height;

        }

        public static T GetSynchronously<T>(this Task<T> Self)
        {
            Self.Wait();
            return Self.Result;
        }

        public static T GetSynchronously<T>(this IAsyncOperation<T> Self)
        {
            var task = Self.AsTask();
            if (task.Status == TaskStatus.Faulted)
                throw task.Exception;

            task.Wait();
            return task.Result;
        }

        #if NETFX_CORE
            public static TypeInfo GetTypeInfo(this Type Type)
            {
                return System.Reflection.IntrospectionExtensions.GetTypeInfo(Type);
            }
        #else
            public static Type GetTypeInfo(this Type Type)
            {
                return Type;
            }
        #endif

    }
}
