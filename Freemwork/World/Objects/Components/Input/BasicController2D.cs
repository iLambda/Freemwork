using System;
using Freemwork.Playstates;
using Freemwork.Primitives.Input;
using Freemwork.Primitives.Input.Commands;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;
using Freemwork.Utilities.Attributes;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.World.Objects.Components.Input
{
    [NeededComponent(typeof(Identity2D))]
    public sealed class BasicController2D : IGameComponent
    {
        private String up;
        private String down;
        private String left;
        private String right;
        private Vector2 oldPosition;
        private static CommandMap defaultMap = new CommandMap();

        public static CommandMap DefaultCommandMap { get { return defaultMap; } }

        public String UpAnimation { get; set; }
        public String DownAnimation { get; set; }
        public String LeftAnimation { get; set; }
        public String RightAnimation { get; set; }
        
        public bool Animate { get; set; }
        public CommandMap CommandMap { get; private set; }
        public float Speed { get; set; }
        public bool Bobbing { get; set; }
        public bool CanMove { get; set; }

        static BasicController2D()
        {
            defaultMap["Up"] = new InputAction(new KeyInputCommand(Key.W, KeyInputCommandType.Down));
            defaultMap["Down"] = new InputAction(new KeyInputCommand(Key.S, KeyInputCommandType.Down));
            defaultMap["Left"] = new InputAction(new KeyInputCommand(Key.A, KeyInputCommandType.Down));
            defaultMap["Right"] = new InputAction(new KeyInputCommand(Key.D, KeyInputCommandType.Down));
        }

        public BasicController2D(float Speed, CommandMap CommandMap, String UpCommandName = "Up", String DownCommandName = "Down", String LeftCommandName = "Left", String RightCommandName = "Right")
        {
            up = UpCommandName;
            down = DownCommandName;
            left = LeftCommandName;
            right = RightCommandName;

            UpAnimation = UpCommandName;
            DownAnimation = DownCommandName;
            LeftAnimation = LeftCommandName;
            RightAnimation = RightCommandName;

            Animate = false;
            Bobbing = false;

            this.Speed = Speed;
            this.CommandMap = CommandMap;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            if (CommandMap[up].Evaluate())
            {
                Owner.QueryComponent<Identity2D>().Transform.Position += new Vector2(0, -Speed);
                if (Owner.HasComponent<SpriteAnimator>() && Animate && UpAnimation != null)
                    Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = UpAnimation;
            }
            if (CommandMap[down].Evaluate())
            {
                Owner.QueryComponent<Identity2D>().Transform.Position += new Vector2(0, Speed);
                if (Owner.HasComponent<SpriteAnimator>() && Animate && DownAnimation != null)
                    Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = DownAnimation;
            }
            if (CommandMap[left].Evaluate())
            {
                Owner.QueryComponent<Identity2D>().Transform.Position += new Vector2(-Speed, 0);
                if (Owner.HasComponent<SpriteAnimator>() && Animate && LeftAnimation != null)
                    Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = LeftAnimation;
            }
            if (CommandMap[right].Evaluate())
            {
                Owner.QueryComponent<Identity2D>().Transform.Position += new Vector2(Speed, 0);
                if (Owner.HasComponent<SpriteAnimator>() && Animate && RightAnimation != null)
                    Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = RightAnimation;
            }

            var pos = Owner.QueryComponent<Identity2D>().Transform.Position;

            if(oldPosition == pos)
                if (Owner.HasComponent<SpriteAnimator>() && Animate)
                    Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = null;

            if (Owner.HasComponent<SpriteOscillator>() && Bobbing)
                Owner.QueryComponent<SpriteOscillator>().Oscillate = oldPosition != pos;
            oldPosition = pos;
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }

        public IGameComponent Clone()
        {
            return new BasicController2D(Speed, CommandMap.Clone(), up, down, left, right)
            {
                DownAnimation = DownAnimation,
                UpAnimation = UpAnimation,
                LeftAnimation = LeftAnimation,
                RightAnimation = RightAnimation,
                Bobbing = Bobbing,
                Animate = Animate
            };
        }
    }
}
