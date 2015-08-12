using System;
using Freemwork.World.Objects.Components;

namespace Freemwork.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
    public sealed class UncompatibleComponentAttribute : Attribute
    {
        public Type UncompatibleType { get; private set; }

        public UncompatibleComponentAttribute(Type Type)
        {
            if (!typeof(IGameComponent).GetTypeInfo().IsAssignableFrom(Type.GetTypeInfo()))
                throw new ArgumentException("The type provided is not a game component. ({0})".FormatString(Type.ToString()));

            UncompatibleType = Type;
        }
    }
}
