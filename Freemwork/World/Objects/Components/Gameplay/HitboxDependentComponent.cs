using System;
using Freemwork.Playstates;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Gameplay
{
    [NeededComponent(typeof(Hitbox))]
    public abstract class HitboxDependentComponent : IGameComponent 
    {
        public bool IsActive { get; set; }
        public String Group { get; set; }

        public abstract void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        public abstract void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID);
        public abstract IGameComponent Clone();
    }
}
