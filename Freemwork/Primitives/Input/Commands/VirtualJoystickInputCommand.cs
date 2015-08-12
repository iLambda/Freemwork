using System;
using Freemwork.Primitives.Math;
using Freemwork.World.Objects;
using Freemwork.World.Objects.Components.UI;

namespace Freemwork.Primitives.Input.Commands
{
    public sealed class VirtualJoystickInputCommand : IInputCommand
    {
        public Predicate<Vector2> Condition { get; private set; }
        public GameObject Joystick { get; set; }

        public VirtualJoystickInputCommand(GameObject Joystick, Predicate<Vector2> Condition)
        {
            this.Joystick = Joystick;
            this.Condition = Condition;
        }

        public bool Evaluate()
        {
            return Condition(Joystick.QueryComponent<VirtualJoystick>().Value);
        }
    };
}
