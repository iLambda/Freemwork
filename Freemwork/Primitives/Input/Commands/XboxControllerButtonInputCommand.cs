using System;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public enum XboxControllerButtonInputCommandType
    {
        Up, Down, PressedOnce, ReleasedOnce
    };

    public sealed class XboxControllerButtonInputCommand : IInputCommand
    {
        public int PlayerID { get; set; }
        public ControllerButton Button { get; private set; }
        public XboxControllerButtonInputCommandType CommandType { get; private set; }

        public XboxControllerButtonInputCommand(int PlayerID, ControllerButton Button, XboxControllerButtonInputCommandType CommandType)
        {
            this.PlayerID = PlayerID;
            this.Button = Button;
            this.CommandType = CommandType;
        }

        public bool Evaluate()
        {
            switch (CommandType)
            {
                case XboxControllerButtonInputCommandType.Up:
                    return ServiceLocator.InputService.IsXboxControllerButtonUp(PlayerID, Button);
                case XboxControllerButtonInputCommandType.Down:
                    return ServiceLocator.InputService.IsXboxControllerButtonDown(PlayerID, Button);
                case XboxControllerButtonInputCommandType.PressedOnce:
                    return ServiceLocator.InputService.IsXboxControllerButtonPressedOnce(PlayerID, Button);
                case XboxControllerButtonInputCommandType.ReleasedOnce:
                    return ServiceLocator.InputService.IsXboxControllerButtonReleasedOnce(PlayerID, Button);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    };
}
