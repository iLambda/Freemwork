using System;
using System.Collections.Generic;
using Freemwork.Services.Audio;
using Freemwork.Services.Device;
using Freemwork.Services.File;
using Freemwork.Services.Graphics;
using Freemwork.Services.Input;
using Freemwork.Services.Resource;

namespace Freemwork.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> services = new Dictionary<Type, IService>();
        private static Dictionary<Type, INullService> nullServices = new Dictionary<Type, INullService>();
        
        public static IAudioService AudioService { get { return Get<IAudioService>(); } set { Provide <IAudioService>(value); } }
        public static IDeviceService DeviceService { get { return Get<IDeviceService>(); } set { Provide<IDeviceService>(value); } }
        public static IGraphicsService GraphicsService { get { return Get<IGraphicsService>(); } set { Provide<IGraphicsService>(value); } }
        public static IResourceService ResourceService { get { return Get<IResourceService>(); } set { Provide<IResourceService>(value); } }
        public static IInputService InputService { get { return Get<IInputService>(); } set { Provide<IInputService>(value); } }
        public static IFileService FileService { get { return Get<IFileService>(); } set { Provide<IFileService>(value); } }

        static ServiceLocator()
        {
           nullServices.Add(typeof(IAudioService), new NullAudioService());
           nullServices.Add(typeof(IDeviceService), new NullDeviceService());
           nullServices.Add(typeof(IGraphicsService), new NullGraphicsService()); 
           nullServices.Add(typeof(IResourceService), new NullResourceService());
           nullServices.Add(typeof(IInputService), new NullInputService());
           nullServices.Add(typeof(IFileService), new NullFileService());
        }
        

        public static T Get<T>() where T : IService
        {
            return services.ContainsKey(typeof(T)) ? (T)services[typeof(T)] : (T)nullServices[typeof(T)];
        }

        public static void Provide<T>(T Service) where T : IService
        {
            if (Service == null || Service is INullService)
                services.Remove(typeof (T));
            else
                services[typeof (T)] = Service;
        }
    }
}
