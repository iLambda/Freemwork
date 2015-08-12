using System;
using Freemwork.Primitives.Math;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public sealed class GyroscopeInputCommand : IInputCommand
    {
        public Predicate<Vector3> Condition { get; private set; }

        public GyroscopeInputCommand(Predicate<Vector3> Condition)
        {
            this.Condition = Condition;
        }

        public bool Evaluate()
        {
            return Condition(ServiceLocator.InputService.GetRotationSpeed());
        }
    };
}
