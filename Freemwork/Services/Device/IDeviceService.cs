using Freemwork.Primitives.Math;

namespace Freemwork.Services.Device
{
    public interface IDeviceService : IService
    {
        Size2D<int> ScreenResolution { get; }

        bool IsKeyboardSupported { get; }
        bool IsMouseSupported { get; }
        bool IsTouchSupported { get; }
        bool IsAccelerometerSupported { get; }
        bool IsGyroscopeSupported { get; }
        bool IsInclinometerSupported { get; }
        bool IsXboxControllerSupported { get;  }
    }
}
