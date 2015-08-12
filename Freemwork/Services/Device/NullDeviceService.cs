using Freemwork.Primitives.Math;

namespace Freemwork.Services.Device
{
    public sealed class NullDeviceService : INullService, IDeviceService
    {
        public Size2D<int> ScreenResolution { get { return new Size2D<int>(0, 0); } }
        public bool IsKeyboardSupported { get { return false; } }
        public bool IsMouseSupported { get { return false; } }
        public bool IsTouchSupported { get { return false; } }
        public bool IsAccelerometerSupported { get { return false; } }
        public bool IsGyroscopeSupported { get { return false; } }
        public bool IsInclinometerSupported { get { return false; } }
        public bool IsXboxControllerSupported { get { return false; } }
    }
}
