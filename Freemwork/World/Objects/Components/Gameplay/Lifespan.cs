using System;
using Freemwork.Playstates;
using Freemwork.Utilities;

namespace Freemwork.World.Objects.Components.Gameplay
{
    public sealed class Lifespan : HitboxDependentComponent
    {
        public delegate void DeathEventHandler(int LastDamages);
        public delegate void DamagedEventHandler(int LastDamages);
        public delegate void HealedEventHandler(int HealValue);

        public event DeathEventHandler Death;
        public event DamagedEventHandler Damaged;
        public event HealedEventHandler Healed;

        private int current = 0;
        private int maximum = 0;
        private bool isInvulnerable = false;
        private int invencibilityFrameCounter = -1;

        public int Current { get { return current; } private set { current = value; } }
        public int Maximum { get { return maximum; } set { maximum = value; } }
        public float RelativeCurrent { get { return (((float)current) / maximum); } }
        public bool IsDead { get { return current <= 0; } }
        public bool IsInvulnerable { get { return isInvulnerable || invencibilityFrameCounter != -1; } set { isInvulnerable = value; } }
        public int InvencibilityDuration { get; set; }

        public Lifespan(int Current, int Maximum, int InvencibilityDuration, String Group = "Default")
        {
            this.Current = Maths.Clamp(Current, 0, Maximum);
            this.Maximum = Maximum;
            this.InvencibilityDuration = InvencibilityDuration;
            this.Group = Group;
        }

        public void Hurt(int Damage = 0)
        {
            if (IsInvulnerable)
                return;

            var damage = (Damage > 0) ? Damage : 0;
            current = Maths.Clamp(current - damage, 0, maximum);

            if (Damaged != null)
            {
                Damaged(damage);
                invencibilityFrameCounter = InvencibilityDuration;
            }

            if (current == 0 && Death != null)
            {
                Death(damage);
                invencibilityFrameCounter = -1;
            }
        }

        public void Heal(int Amount = 0)
        {
            var amount = (Amount > 0) ? Amount : 0;
            current = Maths.Clamp(current + amount, 0, maximum);

            if (Healed != null)
                Healed(amount);

            if (current == 0 && Death != null)
                Death(0);
        }

        public void Kill()
        {
            var damage = current;
            current = 0;
            if (Death != null)
                Death(damage);
        }

        public void Revive()
        {
            current = maximum;
        }

        public override void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (invencibilityFrameCounter > -1) invencibilityFrameCounter--;
        }
        public override void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public override IGameComponent Clone() { return new Lifespan(Current, Maximum, InvencibilityDuration, Group) { IsInvulnerable = IsInvulnerable }; }
    }
}
