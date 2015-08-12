using System.Collections.Generic;
using System.Linq;
using Freemwork.Primitives.Input.Commands;

namespace Freemwork.Primitives.Input
{
    public sealed class InputAction
    {
        private List<IInputCommand> commands = new List<IInputCommand>();
        public IList<IInputCommand> Commands { get { return commands; } }

        public InputAction() { }
        public InputAction(params IInputCommand[] Commands)
        {
            commands.AddRange(Commands);   
        }
        public InputAction(IEnumerable<IInputCommand> Commands)
        {
            commands.AddRange(Commands);
        }

        public bool Evaluate()
        {
            return Commands.Aggregate(false, (Current, InputCommand) => Current || InputCommand.Evaluate());
        }
        public InputAction Clone()
        {
            return new InputAction { commands = new List<IInputCommand>(Commands) };
        }
    }
}
