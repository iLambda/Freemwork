using System;
using System.Linq;
using Freemwork.Playstates;
using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;
using Freemwork.Primitives.Resources;
using Freemwork.Services;
using Freemwork.Utilities;
using Freemwork.World.Objects.Components.Graphics2D;

namespace Freemwork.World.Objects.Components.UI
{
    public sealed class VirtualJoystick : IGameComponent
    {
        private float deadzone = 0.2f;
        private CursorMode allowedMode = CursorMode.Touch;
        private bool isClicked = false;
        private Vector2 moverPosition = Vector2.Zero;
        private int lastId = -1;

        public CursorMode AllowedMode
        {
            get { return allowedMode; }
            set
            {
                var tmp = CursorMode.None;
                if ((value & CursorMode.Mouse) == CursorMode.Mouse) tmp |= (ServiceLocator.DeviceService.IsMouseSupported ? CursorMode.Mouse : CursorMode.None);
                if ((value & CursorMode.Touch) == CursorMode.Touch) tmp |= (ServiceLocator.DeviceService.IsTouchSupported ? CursorMode.Touch : CursorMode.None);
                allowedMode = tmp;
            }
        }
        public uint MaxRadius { get; set; }
        
        public ISprite MoverSprite { get; set; }
        public Vector2 Value { get; private set; }
        public float Deadzone { get { return deadzone; } set { deadzone = Maths.Clamp(value, 0f, 1f); } }

        public VirtualJoystick(String MoverSpriteName, uint MaxRadius, CursorMode AllowedMode = CursorMode.Mouse | CursorMode.Touch)
        {
            this.MaxRadius = MaxRadius;
            this.AllowedMode = AllowedMode;
            MoverSprite = (ISprite)ServiceLocator.ResourceService.GetOrLoad<ISprite>(MoverSpriteName).Clone();
        }

        public VirtualJoystick(ISprite MoverSprite, uint MaxRadius, CursorMode AllowedMode = CursorMode.Mouse | CursorMode.Touch)
        {
            this.MaxRadius = MaxRadius;
            this.MoverSprite = MoverSprite;
            this.AllowedMode = AllowedMode;
        }


        public void Update(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            var identity = Owner.QueryComponent<Identity2D>();
            var position = identity.CameraTransform.Position;
            var rect = new Rectangle<float>(position.X - MaxRadius, position.Y - MaxRadius, MaxRadius * 2, MaxRadius * 2);

            switch (AllowedMode)
            {
                case CursorMode.Touch:
                    if (lastId == -1)
                    {
                        var touches = ServiceLocator.InputService.GetTouches().Where(To => rect.Contains(To.Position) && To.State == TouchState.Pressed).ToList();
                        if (touches.Any()) lastId = touches.First().Id;
                    }

                    if (lastId != -1)
                    {
                        var touch = ServiceLocator.InputService.GetTouches().Where(To => To.Id == lastId).ToList();
                        if (!touch.Any()) lastId = -1;

                        var vect = touch.First().Position - position;
                        if (vect.LengthSquared() > MaxRadius * MaxRadius)
                            vect *= (MaxRadius / vect.Length());

                        moverPosition = position + vect;
                        Value = vect / MaxRadius;
                    }
                    else
                    {
                        moverPosition = position;
                        Value = Vector2.Zero;
                    }
                    break;
                
                case CursorMode.Mouse:
                    if (!isClicked)
                        if (rect.Contains(ServiceLocator.InputService.GetMousePosition()) && ServiceLocator.InputService.IsMouseButtonDown(MouseButton.LeftButton))
                            isClicked = true;
                    if(isClicked)
                        if (ServiceLocator.InputService.IsMouseButtonUp(MouseButton.LeftButton))
                            isClicked = false;

                    if (isClicked)
                    {
                        var vect = ServiceLocator.InputService.GetMousePosition() - position;
                        if (vect.LengthSquared() > MaxRadius*MaxRadius)
                            vect *= (MaxRadius/vect.Length());

                        moverPosition = position + vect;
                        Value = vect/MaxRadius;
                    }
                    else
                    {
                        moverPosition = position;
                        Value = Vector2.Zero;
                    }

                    break;


                case CursorMode.Mouse | CursorMode.Touch:
                    if (lastId == -1 && !isClicked)
                    {
                        var touches = ServiceLocator.InputService.GetTouches().Where(To => rect.Contains(To.Position) && To.State == TouchState.Pressed).ToList();
                        if (touches.Any()) lastId = touches.First().Id;
                    }

                    if (lastId != -1)
                    {
                        isClicked = false;
                        var touch = ServiceLocator.InputService.GetTouches().Where(To => To.Id == lastId).ToList();
                        if (!touch.Any()) lastId = -1;

                        var vect = touch.First().Position - position;
                        if (vect.LengthSquared() > MaxRadius * MaxRadius)
                            vect *= (MaxRadius / vect.Length());

                        moverPosition = position + vect;
                        Value = vect / MaxRadius;
                    }
                    else
                    {
                        if (!isClicked)
                        {
                            moverPosition = position;
                            Value = Vector2.Zero;
                        }
                    }

                    if (lastId == -1)
                    {
                        if (!isClicked)
                            if (rect.Contains(ServiceLocator.InputService.GetMousePosition()) && ServiceLocator.InputService.IsMouseButtonDown(MouseButton.LeftButton))
                                isClicked = true;
                        if (isClicked)
                            if (ServiceLocator.InputService.IsMouseButtonUp(MouseButton.LeftButton))
                                isClicked = false;

                        if (isClicked)
                        {
                            var vect = ServiceLocator.InputService.GetMousePosition() - position;
                            if (vect.LengthSquared() > MaxRadius * MaxRadius)
                                vect *= (MaxRadius / vect.Length());

                            moverPosition = position + vect;
                            Value = vect / MaxRadius;
                        }
                        else
                        {
                            moverPosition = position;
                            Value = Vector2.Zero;
                        }
                    }
                    break;
            }
        }

        public void Draw(PlayState State, Worldspawn Worldspawn, GameObject Owner, int GOID)
        {
            ServiceLocator.GraphicsService.Draw(MoverSprite, new Transform2D(moverPosition, 0f, Vector2.One));
        }

        public IGameComponent Clone()
        {
            return new VirtualJoystick(
                MoverSprite != null ? (ISprite)MoverSprite.Clone() : null, 
                MaxRadius)
            {
                Deadzone = Deadzone,
                AllowedMode = AllowedMode
            };
        }
    }
}
