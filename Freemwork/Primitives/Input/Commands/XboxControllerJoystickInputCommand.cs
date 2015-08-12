using System;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public sealed class XboxControllerJoystickInputCommand : IInputCommand
    {
        public Predicate<Vector2> Condition { get; private set; }
        public int PlayerID { get; set; }
        public ControllerJoystick Joystick { get; set; }

        public XboxControllerJoystickInputCommand(int PlayerID, ControllerJoystick Joystick, Predicate<Vector2> Condition)
        {
            this.PlayerID = PlayerID;
            this.Joystick = Joystick;
            this.Condition = Condition;
        }

        public bool Evaluate()
        {
            return Condition(ServiceLocator.InputService.GetXboxControllerJoystickValue(PlayerID, Joystick));
        }
    };
}
