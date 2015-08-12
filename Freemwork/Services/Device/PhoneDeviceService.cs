using System;
using Windows.Devices.Sensors;
using Freemwork.Primitives.Math;

namespace Freemwork.Services.Device
{
    public class PhoneDeviceService : IDeviceService
    {
        public Size2D<int> ScreenResolution { get; private set; }
        public bool IsKeyboardSupported { get { return false; } }
        public bool IsMouseSupported { get { return false; } }
        public bool IsTouchSupported { get { return true; } }
        public bool IsAccelerometerSupported { get { return Accelerometer.GetDefault() != null; } }
        public bool IsGyroscopeSupported { get { return Gyrometer.GetDefault() != null; } }
        public bool IsInclinometerSupported { get { return Inclinometer.GetDefault() != null; } }
        public bool IsXboxControllerSupported { get { return false; } }

        public PhoneDeviceService()
        {
            #if WINDOWS_PHONE
                ScreenResolution = new Size<int>(Application.Current.Host.Content.ActualWidth, Application.Current.Host.Content.ActualHeight);
            #else
                ScreenResolution = default(Size2D<int>);
                throw new Exception("Not a Windows Phone !");
            #endif
        }
    }
}
