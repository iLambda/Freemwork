using System;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;
using Freemwork.Services;
using Freemwork.Utilities;
using Freemwork.Utilities.Attributes;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.World.Objects.Components.UI
{
    public struct VirtualButtonEventArgs
    {
        public PlayState State;
        public Worldspawn Worldspawn;
        public GameObject Owner;
        public int GOID;

    };

    public delegate void ButtonPressEventHandler(IGameComponent Sender, VirtualButtonEventArgs Args);
    public delegate void ButtonReleaseEventHandler(IGameComponent Sender, VirtualButtonEventArgs Args);
    
    [NeededComponent(typeof(Identity2D))]
    public sealed class VirtualButton : IGameComponent
    {
        private bool wasUp = false;
        private bool wasDown = false;

        public Size2D<int>? CustomHitbox { get; set; }
        public bool IsUp { get { return !IsDown; } }
        public bool IsDown { get; private set; }
        public bool IsPressedOnce { get { return IsDown && wasUp; } }
        public bool IsReleasedOnce { get { return IsUp && wasDown; } }

        public bool Animate { get; set; }
        public String PressedAnimation { get; set; }
        public String ReleasedAnimation { get; set; }

        public event ButtonPressEventHandler Press;
        public event ButtonReleaseEventHandler Release;

        public VirtualButton(bool Animate = false) 
        {
            this.Animate = Animate;

            CustomHitbox = null;
            PressedAnimation = "Pressed";
            ReleasedAnimation = "Released";
        }
        public VirtualButton(Size2D<int> CustomHitbox, bool Animate = false)
        {
            this.CustomHitbox = CustomHitbox;
            this.Animate = Animate;

            PressedAnimation = "Pressed";
            ReleasedAnimation = "Released";
        }


        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var identity2D = Owner.QueryComponent<Identity2D>();
            var sprite = Owner.QueryComponent<SpriteHolder>();
            var hitbox = CustomHitbox.HasValue
                ? new Rectangle<int>((int)identity2D.CameraTransform.Position.X,
                    (int)identity2D.CameraTransform.Position.Y,
                    CustomHitbox.Value.Width,
                    CustomHitbox.Value.Height)
                : new Rectangle<int>((int)identity2D.CameraTransform.Position.X,
                    (int)identity2D.CameraTransform.Position.Y,
                    sprite.Sprite.Size.Width,
                    sprite.Sprite.Size.Height);

            var isDown = false;
            if (ServiceLocator.DeviceService.IsMouseSupported) isDown |= MouseTest(hitbox);
            if (ServiceLocator.DeviceService.IsTouchSupported) isDown |= TouchTest(hitbox);
            IsDown = isDown;

            if (IsPressedOnce && Press != null) Press(this, new VirtualButtonEventArgs { Owner = Owner, Worldspawn = Worldspawn, GOID = GOID, State = State });
            if (IsReleasedOnce && Release != null) Release(this, new VirtualButtonEventArgs { Owner = Owner, Worldspawn = Worldspawn, GOID = GOID, State = State });

            if (Animate) Owner.QueryComponent<SpriteAnimator>().CurrentAnimation = IsDown ? PressedAnimation : ReleasedAnimation;

            wasUp = IsUp;
            wasDown = IsDown;
        }

        private bool MouseTest(Rectangle<int> Hitbox)
        {
            return Hitbox.Contains(ServiceLocator.InputService.GetMousePosition()) &&
                   ServiceLocator.InputService.IsMouseButtonDown(MouseButton.LeftButton);
        }

        private bool TouchTest(Rectangle<int> Hitbox)
        {
            return ServiceLocator.InputService.GetTouches().Any(To => Hitbox.Contains(To.Position));
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }
        public IGameComponent Clone()
        {
            return new VirtualButton
            {
                CustomHitbox = CustomHitbox, 
                Animate = Animate,
                PressedAnimation = PressedAnimation,
                ReleasedAnimation = ReleasedAnimation
            };
        }
    }
}
