using System;
using System.Linq;
using Freemwork.World.Objects;
using Freemwork.World.Objects.Components.UI;

namespace Freemwork.Primitives.Input.Commands
{
    public enum VirtualButtonInputCommandType
    {
        Down, Up, PressedOnce, ReleasedOnce
    }

    public sealed class VirtualButtonInputCommand : IInputCommand
    {
        public VirtualButtonInputCommandType Type { get; private set; }
        public GameObject Object { get; private set; }

        public VirtualButtonInputCommand(GameObject Object, VirtualButtonInputCommandType Type)
        {
            this.Object = Object;
            this.Type = Type;

            if(!Object.Components.Any(Co => Co is VirtualButton))
                throw new Exception("The given GameObject doesn't have a VirtualButton component.");
        }

        public bool Evaluate()
        {
            var button = Object.QueryComponent<VirtualButton>();
            switch (Type)
            {
                case VirtualButtonInputCommandType.Down:
                    return button.IsDown;
                case VirtualButtonInputCommandType.Up:
                    return button.IsUp;
                case VirtualButtonInputCommandType.PressedOnce:
                    return button.IsPressedOnce;
                case VirtualButtonInputCommandType.ReleasedOnce:
                    return button.IsReleasedOnce;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    };
}
