using System;
using Freemwork.Primitives.Math;
using Freemwork.Services;

namespace Freemwork.Primitives.Input.Commands
{
    public sealed class InclinometerInputCommand : IInputCommand
    {
        public Predicate<Vector3> Condition { get; private set; }

        public InclinometerInputCommand(Predicate<Vector3> Condition)
        {
            this.Condition = Condition;
        }

        public bool Evaluate()
        {
            return Condition(ServiceLocator.InputService.GetRotation());
        }
    };
}
