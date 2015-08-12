using Windows.Devices.Sensors;
using Windows.UI.Xaml;
using Freemwork.Primitives.Math;

namespace Freemwork.Services.Device
{
    public class RTDeviceService : IDeviceService
    {
        public Size2D<int> ScreenResolution { get; private set; }
        public bool IsKeyboardSupported { get; private set; }
        public bool IsMouseSupported { get; private set; }
        public bool IsTouchSupported { get; private set; }
        public bool IsAccelerometerSupported { get { return Accelerometer.GetDefault() != null; } }
        public bool IsGyroscopeSupported { get { return Gyrometer.GetDefault() != null; } }
        public bool IsInclinometerSupported { get { return Inclinometer.GetDefault() != null; } }
        public bool IsXboxControllerSupported { get { return true; } }

        public RTDeviceService()
        {
            #if NETFX_CORE
                var bounds = Window.Current.Bounds;
                ScreenResolution = new Size2D<int>((int)bounds.Width, (int)bounds.Height);

                IsKeyboardSupported = 0 != new Windows.Devices.Input.KeyboardCapabilities().KeyboardPresent;
                IsMouseSupported = 0 != new Windows.Devices.Input.MouseCapabilities().MousePresent;
                IsTouchSupported = 0 != new Windows.Devices.Input.TouchCapabilities().TouchPresent;
            #else
                throw new Exception("Not a WinRT device !");
            #endif
        }
    }
}
