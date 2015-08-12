using Freemwork.Playstates;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Gameplay
{
    public struct TriggeredEventArgs
    {
        public PlayState State;
        public Worldspawn Worldspawn;
        public GameObject Owner;
        public int GOID;
    }

    public delegate void TriggeredEventHandler(IGameComponent Sender, TriggeredEventArgs Args);
    

    [NeededComponent(typeof(Hitbox))]
    public abstract class TriggerableHitboxDependentComponent : HitboxDependentComponent
    {
        public virtual event TriggeredEventHandler Triggering;
    }
}