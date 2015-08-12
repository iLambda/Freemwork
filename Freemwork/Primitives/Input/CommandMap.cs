using System;
using System.Collections.Generic;
using System.Linq;

namespace Freemwork.Primitives.Input
{
    public sealed class CommandMap
    {
        private Dictionary<String, InputAction> actions = new Dictionary<string, InputAction>();

        public Dictionary<String, InputAction> Actions { get { return actions; } }
        public InputAction this[String Name]
        {
            get { return Actions[Name]; }
            set { Actions[Name] = value; }
        }


        public CommandMap Clone()
        {
            return new CommandMap { actions = actions.ToDictionary(Action => Action.Key, Action => Action.Value.Clone()) };   
        }
    }
}
