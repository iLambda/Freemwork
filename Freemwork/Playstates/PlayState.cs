using System;
using Freemwork.Primitives.Systems.Automaton;
using Freemwork.World;

namespace Freemwork.Playstates
{
    public abstract class PlayState : IFiniteState
    {
        public abstract string Identifier { get; }
        
        public event RequestTransitionEventHandler RequestTransition;

        public abstract Worldspawn Worldspawn { get; }

        public virtual void Update()
        {
            Worldspawn.Update(this);
        }

        public virtual void Draw()
        {
            Worldspawn.Draw(this);
        }

        public void Transite(String Label)
        {
            if (RequestTransition != null)
                RequestTransition(this, Label);
        }
    }
}
