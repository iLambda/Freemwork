using Freemwork.Primitives.Math;

namespace Freemwork.Primitives.Input.Devices
{
    public enum TouchState
    {
        Invalid, Moved, Pressed, Released
    }

    public struct Touch
    {
        public int Id;
        public Vector2 Position;
        public float Pressure;
        public TouchState State;
    }
}
