using System;
using System.Linq;
using Windows.Devices.Sensors;
using Freemwork.Primitives.Input.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Vector2 = Freemwork.Primitives.Math.Vector2;
using Vector3 = Freemwork.Primitives.Math.Vector3;

namespace Freemwork.Services.Input
{
    public sealed class XNAInputService : IInputService
    {
        private KeyboardState oldKeyboardState;
        private KeyboardState keyboardState;

        private MouseState oldMouseState;
        private MouseState mouseState;

        private Touch[] oldTouchCollection;
        private Touch[] touchCollection;

        private GamePadState[] xboxStates;
        private GamePadState[] oldXboxStates;

        private Gyrometer gyrometer;
        private Inclinometer inclinometer;
        private Accelerometer accelerometer;

        private Vector2 mousePos;

        public XNAInputService()
        {
            accelerometer = Accelerometer.GetDefault();
            gyrometer = Gyrometer.GetDefault();
            inclinometer = Inclinometer.GetDefault();

            keyboardState = oldKeyboardState = new KeyboardState();
            mouseState = oldMouseState = new MouseState();
            oldTouchCollection = touchCollection = new Touch[0];
            xboxStates = oldXboxStates = new GamePadState[4];
        }

        public void BeginFetch()
        {
            if (ServiceLocator.DeviceService.IsKeyboardSupported) 
                keyboardState = Keyboard.GetState();
            if (ServiceLocator.DeviceService.IsMouseSupported)
            {
                mouseState = Mouse.GetState();
                mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y) * ServiceLocator.GraphicsService.InvertViewportTransform;
            }
            if (ServiceLocator.DeviceService.IsTouchSupported) 
                touchCollection = TouchPanel.GetState().Select(To => new Touch { Id = To.Id, Position = new Vector2(To.Position.X, To.Position.Y) * ServiceLocator.GraphicsService.InvertViewportTransform, Pressure = To.Pressure, State = (TouchState)To.State }).ToArray();
            if (ServiceLocator.DeviceService.IsXboxControllerSupported)
                xboxStates = Enumerable.Range(0, 4).Select(PId => GamePad.GetState((PlayerIndex)PId)).ToArray();
        }

        public void EndFetch()
        {
            oldKeyboardState = keyboardState;
            oldMouseState = mouseState;
            oldTouchCollection = touchCollection;
            oldXboxStates = xboxStates;
        }

        public bool IsKeyDown(Key Key)
        {
            return keyboardState.IsKeyDown((Keys) Key);
        }

        public bool IsKeyUp(Key Key)
        {
            return keyboardState.IsKeyUp((Keys) Key);
        }

        public bool IsKeyPressedOnce(Key Key)
        {
            return oldKeyboardState.IsKeyUp((Keys)Key)
                   && keyboardState.IsKeyDown((Keys)Key);
        }

        public bool IsKeyReleasedOnce(Key Key)
        {
            return oldKeyboardState.IsKeyDown((Keys)Key)
                   && keyboardState.IsKeyUp((Keys)Key);
        }

        public bool IsMouseButtonDown(MouseButton MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Pressed;
                default:
                    throw new ArgumentOutOfRangeException("MouseButton");
            }
        }

        public bool IsMouseButtonUp(MouseButton MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Released;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Released;
                case MouseButton.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Released;
                default:
                    throw new ArgumentOutOfRangeException("MouseButton");
            }
        }

        public bool IsMouseButtonPressedOnce(MouseButton MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButton.LeftButton:
                    return oldMouseState.LeftButton == ButtonState.Released &&
                           mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return oldMouseState.RightButton == ButtonState.Released &&
                           mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return oldMouseState.MiddleButton == ButtonState.Released &&
                           mouseState.MiddleButton == ButtonState.Pressed;
                default:
                    throw new ArgumentOutOfRangeException("MouseButton");
            }
        }

        public bool IsMouseButtonReleasedOnce(MouseButton MouseButton)
        {
            switch (MouseButton)
            {
                case MouseButton.LeftButton:
                    return oldMouseState.LeftButton == ButtonState.Pressed &&
                           mouseState.LeftButton == ButtonState.Released;
                case MouseButton.RightButton:
                    return oldMouseState.RightButton == ButtonState.Pressed &&
                           mouseState.RightButton == ButtonState.Released;
                case MouseButton.MiddleButton:
                    return oldMouseState.MiddleButton == ButtonState.Pressed &&
                           mouseState.MiddleButton == ButtonState.Released;
                default:
                    throw new ArgumentOutOfRangeException("MouseButton");
            }
        }

        public bool IsMouseStatic()
        {
            return oldMouseState.Position == mouseState.Position;
        }

        public int GetMouseWheelValue()
        {
            return mouseState.ScrollWheelValue;
        }

        public Vector2 GetMousePosition()
        {
            return mousePos;
        }

        private Buttons GetXboxButton(ControllerButton Button)
        {
            switch (Button)
            {
                case ControllerButton.A:
                    return Buttons.A;
                case ControllerButton.B:
                    return Buttons.B;
                case ControllerButton.X:
                    return Buttons.X;
                case ControllerButton.Y:
                    return Buttons.Y;
                case ControllerButton.Start:
                    return Buttons.Start;
                case ControllerButton.Select:
                    return Buttons.Back;
                case ControllerButton.Up:
                    return Buttons.DPadUp;
                case ControllerButton.Down:
                    return Buttons.DPadDown;
                case ControllerButton.Left:
                    return Buttons.DPadLeft;
                case ControllerButton.Right:
                    return Buttons.DPadRight;
                case ControllerButton.LB:
                    return Buttons.LeftShoulder;
                case ControllerButton.RB:
                    return Buttons.RightShoulder;
                case ControllerButton.LS:
                    return Buttons.LeftStick;
                case ControllerButton.RS:
                    return Buttons.RightStick;
                default:
                    throw new Exception("The button does not exist on the XBOX controller.");
            }
        }

        public bool IsXboxControllerConnected(int PlayerID)
        {
            return xboxStates[PlayerID].IsConnected;
        }

        public bool IsXboxControllerButtonDown(int PlayerID, ControllerButton Button)
        {
            return IsXboxControllerConnected(PlayerID) && 
                xboxStates[PlayerID].IsButtonDown(GetXboxButton(Button));
        }

        public bool IsXboxControllerButtonUp(int PlayerID, ControllerButton Button)
        {
            return IsXboxControllerConnected(PlayerID) &&
                xboxStates[PlayerID].IsButtonUp(GetXboxButton(Button));
        }

        public bool IsXboxControllerButtonPressedOnce(int PlayerID, ControllerButton Button)
        {
            var button = GetXboxButton(Button);
            return IsXboxControllerConnected(PlayerID) &&
                oldXboxStates[PlayerID].IsButtonUp(button) &&
                xboxStates[PlayerID].IsButtonDown(button);
        }

        public bool IsXboxControllerButtonReleasedOnce(int PlayerID, ControllerButton Button)
        {
            var button = GetXboxButton(Button);
            return IsXboxControllerConnected(PlayerID) &&
                oldXboxStates[PlayerID].IsButtonDown(button) &&
                xboxStates[PlayerID].IsButtonUp(button);
        }

        public Vector2 GetXboxControllerJoystickValue(int PlayerID, ControllerJoystick JoystickName)
        {
            if (!IsXboxControllerConnected(PlayerID))
                return Vector2.Zero;

            switch (JoystickName)
            {
                case ControllerJoystick.LT:
                    return new Vector2(xboxStates[PlayerID].Triggers.Left);
                case ControllerJoystick.RT:
                    return new Vector2(xboxStates[PlayerID].Triggers.Right);
                case ControllerJoystick.LS:
                    return new Vector2(xboxStates[PlayerID].ThumbSticks.Left.X, xboxStates[PlayerID].ThumbSticks.Left.Y);
                case ControllerJoystick.RS:
                    return new Vector2(xboxStates[PlayerID].ThumbSticks.Right.X, xboxStates[PlayerID].ThumbSticks.Right.Y);
                default:
                    throw new Exception("The given joystick does not exist on the XBOX controller.");
            }
        }

        public Touch[] GetTouches()
        {
            return touchCollection;
        }

        public Touch[] GetOldTouches()
        {
            return oldTouchCollection;
        }

        public Vector3 GetRotationSpeed()
        {
            #if WINDOWS_PHONE
                var data = gyrometer.GetCurrentReading();
                return new Vector3((float)data.AngularVelocityX, (float)data.AngularVelocityY, (float)data.AngularVelocityZ);
            #elif NETFX_CORE
                var data = gyrometer.GetCurrentReading();
                return new Vector3((float)data.AngularVelocityX, (float)data.AngularVelocityY, (float)data.AngularVelocityZ);
            #else
                return Vector3.Zero;
            #endif
        }

        public Vector3 GetRotation()
        {
            #if WINDOWS_PHONE
                var data = inclinometer.GetCurrentReading();
                return new Vector3(data.YawDegrees, data.PitchDegrees, data.RollDegrees);
            #elif NETFX_CORE
                var data = inclinometer.GetCurrentReading();
                return new Vector3(data.YawDegrees, data.PitchDegrees, data.RollDegrees);
            #else
                return Vector3.Zero;
            #endif
        }

        public Vector3 GetAcceleration()
        {
            #if WINDOWS_PHONE
                var data = accelerometer.GetCurrentReading();
                return new Vector3((float)data.AccelerationX, (float)data.AccelerationY, (float)data.AccelerationZ);
            #elif NETFX_CORE
                var data = accelerometer.GetCurrentReading();
                return new Vector3((float)data.AccelerationX, (float)data.AccelerationY, (float)data.AccelerationZ);
            #else
                return Vector3.Zero;
            #endif
        }
     }
}
