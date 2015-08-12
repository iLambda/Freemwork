using System;
using System.Collections.Generic;

namespace Freemwork.Primitives
{
    public sealed class EqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> EqualsFunc { get; set; }
        public Func<T, int> GetHashCodeFunc { get; set; }

        public EqualityComparer(Func<T, T, bool> EqualsFunc, Func<T, int> GetHashCodeFunc)
        {
            this.EqualsFunc = EqualsFunc;
            this.GetHashCodeFunc = GetHashCodeFunc;
        }

        public bool Equals(T X, T Y)
        {
            return EqualsFunc(X, Y);
        }

        public int GetHashCode(T Obj)
        {
            return GetHashCodeFunc(Obj);
        }
    }
}
