using System;
using System.Collections.Generic;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Gameplay
{
    public struct DamageDealtEventArgs
    {
        public PlayState State;
        public Worldspawn Worldspawn;
        public GameObject Owner;
        public int GOID;

        public KeyValuePair<int, GameObject> DealtTo;
        public int Amount;
        public int EffectiveAmount;
    }

    public delegate void DamageDealtEventHandler(IGameComponent Sender, DamageDealtEventArgs Args);

    [NeededComponent(typeof(Hitbox))]
    public sealed class DamageDealer : HitboxDependentComponent
    {
        public event DamageDealtEventHandler DamageDealt;

        public int Damages { get; set; }

        public DamageDealer(int Damages, String Group = "Default")
        {
            this.Damages = Damages;
            this.Group = Group;
        }

        public override void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) 
        {
            var hitbox = Owner.QueryComponent<Hitbox>();
            var gobs = Worldspawn.SpacePartitionning 
                ? Worldspawn.GetNearObjects<Lifespan>(Owner) 
                : Worldspawn.GameObjects.Where(Gob => Gob.Value.HasComponent<Lifespan>() );
            foreach (var gob in gobs)
            {
                var lifeSpan = gob.Value.QueryComponent<Lifespan>();
                if(lifeSpan.Group != Group) continue;
                var boundingBox = gob.Value.QueryComponent<Hitbox>();
                if (!hitbox.Intersects(boundingBox)) continue;
                   
                var args = new DamageDealtEventArgs { State = State, Worldspawn = Worldspawn, DealtTo = gob, Owner = Owner, GOID = GOID, Amount = Damages };
                var oldLife = lifeSpan.Current;
                lifeSpan.Hurt(Damages);
                args.EffectiveAmount = lifeSpan.Current - oldLife;
                if (DamageDealt != null) DamageDealt(this, args);
            }
        }
        public override void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public override IGameComponent Clone() { return new DamageDealer(Damages, Group); }
    }
}
    