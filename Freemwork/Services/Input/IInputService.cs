using Freemwork.Primitives.Input.Devices;
using Freemwork.Primitives.Math;

namespace Freemwork.Services.Input
{
    public interface IInputService : IService
    {
        void BeginFetch();
        void EndFetch();
        
        bool IsKeyDown(Key Key);
        bool IsKeyUp(Key Key);
        bool IsKeyPressedOnce(Key Key);
        bool IsKeyReleasedOnce(Key Key);

        bool IsMouseButtonDown(MouseButton MouseButton);
        bool IsMouseButtonUp(MouseButton MouseButton);
        bool IsMouseButtonPressedOnce(MouseButton MouseButton);
        bool IsMouseButtonReleasedOnce(MouseButton MouseButton);
        bool IsMouseStatic();
        int GetMouseWheelValue();
        Vector2 GetMousePosition();

        bool IsXboxControllerConnected(int PlayerID);
        bool IsXboxControllerButtonDown(int PlayerID, ControllerButton Button);
        bool IsXboxControllerButtonUp(int PlayerID, ControllerButton Button);
        bool IsXboxControllerButtonPressedOnce(int PlayerID, ControllerButton Button);
        bool IsXboxControllerButtonReleasedOnce(int PlayerID, ControllerButton Button);
        Vector2 GetXboxControllerJoystickValue(int PlayerID, ControllerJoystick JoystickName);
            
        Touch[] GetTouches();
        Touch[] GetOldTouches();

        Vector3 GetRotationSpeed();
        Vector3 GetRotation();
        Vector3 GetAcceleration();
    }
}
