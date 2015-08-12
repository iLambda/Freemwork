using System;
using System.Collections.Generic;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Gameplay
{
    public struct InstantlyKilledEventArgs
    {
        public PlayState State;
        public Worldspawn Worldspawn;
        public GameObject Owner;
        public int GOID;

        public KeyValuePair<int, GameObject> DealtTo;
    }

    public delegate void InstantlyKilledEventHandler(IGameComponent Sender, InstantlyKilledEventArgs Args);

    [NeededComponent(typeof(Hitbox))]
    public sealed class InstantKiller : HitboxDependentComponent
    {
        public event InstantlyKilledEventHandler InstantlyKill;

        public InstantKiller(String Group = "Default")
        {
            this.Group = Group;
        }

        public override void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var hitbox = Owner.QueryComponent<Hitbox>();
            var gobs = Worldspawn.SpacePartitionning 
                ? Worldspawn.GetNearObjects<Lifespan>(Owner) 
                : Worldspawn.GameObjects.Where(Gob => Gob.Value.HasComponent<Lifespan>());
            foreach (var gob in gobs)
            {
                var lifeSpan = gob.Value.QueryComponent<Lifespan>();
                if (lifeSpan.Group != Group) continue;
                var boundingBox = gob.Value.QueryComponent<Hitbox>();
                if (hitbox.Intersects(boundingBox))
                {
                    lifeSpan.Kill();
                    if (lifeSpan.IsDead)
                        if (InstantlyKill != null) InstantlyKill(this, new InstantlyKilledEventArgs { State = State, GOID = GOID, Owner = Owner, Worldspawn = Worldspawn, DealtTo = gob });
                }
            }
        }
        public override void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public override IGameComponent Clone() { return new InstantKiller(Group); }
    }
}
