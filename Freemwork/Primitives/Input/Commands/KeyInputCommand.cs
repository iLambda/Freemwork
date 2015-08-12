using System;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public enum KeyInputCommandType
    {
        Up, Down, PressedOnce, ReleasedOnce
    };

    public sealed class KeyInputCommand : IInputCommand
    {
        public Key Key { get; private set; }
        public KeyInputCommandType CommandType { get; private set; }

        public KeyInputCommand(Key Key, KeyInputCommandType CommandType)
        {
            this.Key = Key;
            this.CommandType = CommandType;
        }

        public bool Evaluate()
        {
            switch (CommandType)
            {
                case KeyInputCommandType.Up:
                    return ServiceLocator.InputService.IsKeyUp(Key);
                case KeyInputCommandType.Down:
                    return ServiceLocator.InputService.IsKeyDown(Key);
                case KeyInputCommandType.PressedOnce:
                    return ServiceLocator.InputService.IsKeyPressedOnce(Key);
                case KeyInputCommandType.ReleasedOnce:
                    return ServiceLocator.InputService.IsKeyReleasedOnce(Key);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    };
}
