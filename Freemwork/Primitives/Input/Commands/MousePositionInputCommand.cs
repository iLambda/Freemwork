using System;
using Freemwork.Primitives.Math;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public sealed class MousePositionInputCommand : IInputCommand
    {
        public Predicate<Vector2> Condition { get; private set; }

        public MousePositionInputCommand(Predicate<Vector2> Condition)
        {
            this.Condition = Condition;
        }

        public bool Evaluate()
        {
            return Condition(ServiceLocator.InputService.GetMousePosition());
        }
    };
}
