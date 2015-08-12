using System;
using Freemwork.Playstates;

namespace Freemwork.World.Objects.Components.Misc
{
    public struct UnspawnedEventArgs
    {
        public int GOID;
        public GameObject Owner;
        public Worldspawn Worldspawn;
        public PlayState State;
    }

    public delegate void UnspawnedEventHandler(IGameComponent Sender, UnspawnedEventArgs Args);

    public sealed class Unspawner : IGameComponent
    {
        public Func<Worldspawn, GameObject, int, bool> Predicate { get; set; }

        public event UnspawnedEventHandler Unspawned;

        public Unspawner(Func<Worldspawn, GameObject, int, bool> Predicate)
        {
            this.Predicate = Predicate;
        }
        
        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (Predicate(Worldspawn, Owner, GOID))
            {
                if (Unspawned != null) Unspawned(this, new UnspawnedEventArgs { GOID = GOID, Owner = Owner, Worldspawn = Worldspawn, State = State });
                Worldspawn.GameObjects.Remove(GOID);
            }
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
        
        }

        public IGameComponent Clone()
        {
            return new Unspawner(Predicate);
        }
    }
}
