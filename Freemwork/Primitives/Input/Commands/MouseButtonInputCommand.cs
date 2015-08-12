using System;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public enum MouseButtonInputCommandType
    {
        Up, Down, PressedOnce, ReleasedOnce
    };

    public sealed class MouseButtonInputCommand : IInputCommand
    {
        public MouseButton MouseButton { get; private set; }
        public MouseButtonInputCommandType CommandType { get; private set; }

        public MouseButtonInputCommand(MouseButton MouseButton, MouseButtonInputCommandType CommandType)
        {
            this.MouseButton = MouseButton;
            this.CommandType = CommandType;
        }

        public bool Evaluate()
        {
            switch (CommandType)
            {
                case MouseButtonInputCommandType.Up:
                    return ServiceLocator.InputService.IsMouseButtonUp(MouseButton);
                case MouseButtonInputCommandType.Down:
                    return ServiceLocator.InputService.IsMouseButtonDown(MouseButton);
                case MouseButtonInputCommandType.PressedOnce:
                    return ServiceLocator.InputService.IsMouseButtonPressedOnce(MouseButton);
                case MouseButtonInputCommandType.ReleasedOnce:
                    return ServiceLocator.InputService.IsMouseButtonReleasedOnce(MouseButton);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    };
}
