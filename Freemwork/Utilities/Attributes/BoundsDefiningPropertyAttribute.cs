using System;

namespace Freemwork.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public sealed class BoundsDefiningPropertyAttribute : Attribute
    {
        public String PropertyName { get; private set; }

        public BoundsDefiningPropertyAttribute(String PropertyName)
        {
            this.PropertyName = PropertyName;
        }
    }
}
