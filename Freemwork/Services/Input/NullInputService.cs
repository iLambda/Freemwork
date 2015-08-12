using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;

namespace Freemwork.Services.Input
{
    public sealed class NullInputService : INullService, IInputService
    {
        public void BeginFetch()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("BeginFetch() called in NullInputState");
            #endif
        }

        public void EndFetch()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("EndFetch() called in NullInputState");
            #endif
        }

        public bool IsKeyDown(Key Key)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsKeyDown(Key) called in NullInputState");
            #endif

            return false;
        }

        public bool IsKeyUp(Key Key)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsKeyUp(Key) called in NullInputState");
            #endif

            return false;
        }

        public bool IsKeyPressedOnce(Key Key)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsKeyPressedOnce(Key) called in NullInputState");
            #endif

            return false;
        }
        public bool IsKeyReleasedOnce(Key Key)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsKeyReleasedOnce(Key) called in NullInputState");
            #endif

            return false;
        }

        public bool IsMouseButtonDown(MouseButton MouseButton)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsMouseButtonDown(MouseButton) called in NullInputState");
            #endif

            return false;
        }

        public bool IsMouseButtonUp(MouseButton MouseButton)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsMouseButtonUp(MouseButton) called in NullInputState");
            #endif

            return false;
        }

        public bool IsMouseButtonPressedOnce(MouseButton MouseButton)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsMouseButtonPressedOnce(MouseButton) called in NullInputState");
            #endif

            return false;
        }
        public bool IsMouseButtonReleasedOnce(MouseButton MouseButton)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsMouseButtonReleasedOnce(MouseButton) called in NullInputState");
            #endif

            return false;
        }

        public bool IsMouseStatic()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsMouseStatic() called in NullInputState");
            #endif

            return false;
        }

        public int GetMouseWheelValue()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetMouseWheelValue() called in NullInputState");
            #endif

            return 0;
        }

        public Vector2 GetMousePosition()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetMousePosition() called in NullInputState");
            #endif

            return Vector2.Zero;
        }

        public bool IsXboxControllerConnected(int PlayerID)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsXboxControllerConnected() called in NullInputState");
            #endif

            return false;
        }

        public bool IsXboxControllerButtonDown(int PlayerID, ControllerButton Button)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsXboxControllerButtonDown() called in NullInputState");
            #endif

            return false;
        }

        public bool IsXboxControllerButtonUp(int PlayerID, ControllerButton Button)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsXboxControllerButtonUp() called in NullInputState");
            #endif

            return false;
        }

        public bool IsXboxControllerButtonPressedOnce(int PlayerID, ControllerButton Button)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsXboxControllerButtonPressedOnce() called in NullInputState");
            #endif

            return false;
        }

        public bool IsXboxControllerButtonReleasedOnce(int PlayerID, ControllerButton Button)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("IsXboxControllerButtonReleasedOnce() called in NullInputState");
            #endif

            return false;
        }

        public Vector2 GetXboxControllerJoystickValue(int PlayerID, ControllerJoystick JoystickName)
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetXboxControllerJoystickValue() called in NullInputState");
            #endif

            return Vector2.Zero;
        }

        public Touch[] GetTouches()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetTouches() called in NullInputState");
            #endif

            return null;
        }

        public Touch[] GetOldTouches()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetOldTouches() called in NullInputState");
            #endif

            return null;
        }

        public Vector3 GetRotationSpeed()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetRotationSpeed() called in NullInputState");
            #endif

            return Vector3.Zero;
        }
        public Vector3 GetRotation()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetRotationSpeed() called in NullInputState");
            #endif

            return Vector3.Zero;
        }


        public Vector3 GetAcceleration()
        {
            #if DEBUG_VERBOSE
                Debug.WriteLine("GetAcceleration() called in NullInputState");
            #endif

            return Vector3.Zero;
        }
    }
}
