using System;

namespace Freemwork.Primitives.Systems.Automaton
{
    public delegate void RequestTransitionEventHandler(IFiniteState Sender, String Label);

    public interface IFiniteState
    {
        String Identifier { get; }

        event RequestTransitionEventHandler RequestTransition;
    }
}
