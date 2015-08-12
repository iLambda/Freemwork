using System;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Services;
using Freemwork.Utilities.Attributes;
using Freemwork.World.Objects.Components.Graphics2D;
using Freemwork.World.Objects.Components.Input;

namespace Freemwork.World.Objects.Components.UI
{
    [Flags]
    public enum CursorMode
    {
        None = 0, Touch = 1, Mouse = 2
    };

    [NeededComponent(typeof(Identity2D))]
    [UncompatibleComponent(typeof(VirtualButton))]
    [UncompatibleComponent(typeof(VirtualJoystick))]
    [UncompatibleComponent(typeof(BasicController2D))]
    public sealed class Cursor : IGameComponent
    {
        private CursorMode allowedMode = CursorMode.Touch;
        private int lastTouchId = -1;

        public CursorMode AllowedMode {
            get { return allowedMode; }
            set
            {
                var tmp = CursorMode.None;
                if ((value & CursorMode.Mouse) == CursorMode.Mouse) tmp |= (ServiceLocator.DeviceService.IsMouseSupported ? CursorMode.Mouse : CursorMode.None);
                if ((value & CursorMode.Touch) == CursorMode.Touch) tmp |= (ServiceLocator.DeviceService.IsTouchSupported ? CursorMode.Touch : CursorMode.None);
                allowedMode = tmp;
            } 
        }
        public bool HideWhenStatic { get; set; }

        public Cursor(CursorMode AllowedMode = CursorMode.Mouse | CursorMode.Touch, bool HideWhenStatic = true)
        {
            this.AllowedMode = AllowedMode;
            this.HideWhenStatic = HideWhenStatic;
        }

        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            switch (AllowedMode)
            {
                case CursorMode.None:
                    break;

                case CursorMode.Touch:
                    if (!ServiceLocator.DeviceService.IsTouchSupported)
                        return;

                    if (ServiceLocator.InputService.GetOldTouches().Length == 0 && ServiceLocator.InputService.GetTouches().Length == 1)
                        lastTouchId = ServiceLocator.InputService.GetTouches().Single().Id;
                    if (lastTouchId != -1 && ServiceLocator.InputService.GetTouches().Any(To => To.Id == lastTouchId && To.State == TouchState.Released))
                        lastTouchId = -1;
                    if(lastTouchId != -1)
                        Owner.QueryComponent<Identity2D>().Transform.Position = ServiceLocator.InputService.GetTouches().Single(To => To.Id == lastTouchId).Position;

                    if (HideWhenStatic)
                    {
                        if (Owner.HasComponent<SpriteHolder>())
                            Owner.QueryComponent<SpriteHolder>().Sprite.Visible = (lastTouchId != -1);
                        if (Owner.HasComponent<TextHolder>())
                            Owner.QueryComponent<TextHolder>().Text.Visible = (lastTouchId != -1);
                    }
                    break;

                case CursorMode.Mouse:
                    if (!ServiceLocator.DeviceService.IsMouseSupported)
                        return;
                    Owner.QueryComponent<Identity2D>().Transform.Position = ServiceLocator.InputService.GetMousePosition();

                    if (HideWhenStatic)
                    {
                        if (Owner.HasComponent<SpriteHolder>())
                            Owner.QueryComponent<SpriteHolder>().Sprite.Visible = (lastTouchId != -1);
                        if (Owner.HasComponent<TextHolder>())
                            Owner.QueryComponent<TextHolder>().Text.Visible = (lastTouchId != -1);
                    }
                    break;

                case CursorMode.Touch | CursorMode.Mouse:
                    if (ServiceLocator.InputService.GetTouches().Any())
                    {
                        if (ServiceLocator.InputService.GetOldTouches().Length == 0 && ServiceLocator.InputService.GetTouches().Length == 1)
                            lastTouchId = ServiceLocator.InputService.GetTouches().Single().Id;
                        if (lastTouchId != -1 && ServiceLocator.InputService.GetTouches().Any(To => To.Id == lastTouchId && To.State == TouchState.Released))
                            lastTouchId = -1;
                        if (lastTouchId != -1)
                            Owner.QueryComponent<Identity2D>().Transform.Position = ServiceLocator.InputService.GetTouches().Single(To => To.Id == lastTouchId).Position;

                        if (HideWhenStatic)
                        {
                            if (Owner.HasComponent<SpriteHolder>())
                                Owner.QueryComponent<SpriteHolder>().Sprite.Visible = (lastTouchId != -1);
                            if (Owner.HasComponent<TextHolder>())
                                Owner.QueryComponent<TextHolder>().Text.Visible = (lastTouchId != -1);
                        }
                    }
                    else
                    {
                        Owner.QueryComponent<Identity2D>().Transform.Position = ServiceLocator.InputService.GetMousePosition();
                        if (HideWhenStatic)
                        {
                            if (Owner.HasComponent<SpriteHolder>())
                                Owner.QueryComponent<SpriteHolder>().Sprite.Visible = (lastTouchId != -1);
                            if (Owner.HasComponent<TextHolder>())
                                Owner.QueryComponent<TextHolder>().Text.Visible = (lastTouchId != -1);
                        }
                    }
                    break;
            }
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID) { }

        public IGameComponent Clone()
        {
            return new Cursor {AllowedMode = AllowedMode, HideWhenStatic = HideWhenStatic };
        }
    }
}
