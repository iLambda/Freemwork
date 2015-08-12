using System;
using System.Collections.Generic;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Primitives.Math;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;

namespace Freemwork.World.Objects.Components.Graphics2D
{
    [NeededComponent(typeof(SpriteHolder))]
    public sealed class SpriteAnimator : IGameComponent
    {
        private Dictionary<String, Rectangle<int>[]> animations = new Dictionary<string, Rectangle<int>[]>();
        private String currentAnimation = null;
        private String lastAnimation = null;
        private int i = 0;
        private int j = 0;
        private uint frameDuration = 1;

        public Dictionary<String, Rectangle<int>[]> Animations { get { return animations; } private set { animations = value; } }

        public String CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                if (currentAnimation != value)
                {
                    lastAnimation = currentAnimation;
                    currentAnimation = value;
                    if (currentAnimation != null)
                        i = Maths.Clamp(i, 0, Animations[currentAnimation].Length);
                }
            }
        }
        public Rectangle<int>[] CurrentFrames { get { return Animations[CurrentAnimation]; } }
        public uint FrameDuration { get { return frameDuration; } set { i = 0; frameDuration = value; } }
        public int CurrentFrame { get { return i; } }

        public SpriteAnimator() { }
        public SpriteAnimator(String CurrentAnimation, Dictionary<String, Rectangle<int>[]> Animations)
        {
            this.Animations = Animations;
            this.CurrentAnimation = CurrentAnimation;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var sprite = Owner.QueryComponent<SpriteHolder>();

            if (CurrentAnimation == null)
            {
                sprite.Sprite.Region = Animations[lastAnimation].First();
                return;
            }
            
            var animation = Animations[CurrentAnimation];

            sprite.Sprite.Region = animation[i];

            j = (j + 1) % (animation.Length * (int)frameDuration);
            i = (j / (float)frameDuration).IntegerPart();
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }

        public IGameComponent Clone()
        {
            return new SpriteAnimator
            {
                animations = new Dictionary<string, Rectangle<int>[]>(animations),
                currentAnimation = currentAnimation,
                i = i,
                j = j,
                frameDuration = frameDuration
            };
        }
    }
}
