using System;
using System.IO;
using System.Runtime.Serialization;
using Windows.ApplicationModel;
using Windows.Storage;
using Freemwork.Utilities;

namespace Freemwork.Services.File
{
    public sealed class RTFileService : IFileService
    {
        public void Save<T>(string Path, T Data, FileStorage Storage = FileStorage.Local)
        {
            switch (Storage)
            {
                case FileStorage.Local:
                    var storageFile = ApplicationData.Current.LocalFolder.CreateFileAsync(Path, CreationCollisionOption.ReplaceExisting).GetSynchronously();
                    if (Data is String)
                        Windows.Storage.FileIO.WriteTextAsync(storageFile, Data as String).AsTask().Wait();
                    else
                    {
                        var stream = storageFile.OpenAsync(FileAccessMode.ReadWrite).GetSynchronously();
                        using (var outputStream = stream.AsStream())
                        {
                            var dcs = new DataContractSerializer(typeof(T));
                            dcs.WriteObject(outputStream, null);
                        }
                    }
                    break;
                case FileStorage.Project:
                    throw new Exception("Operation not supported");
                case FileStorage.Parameter:
                    ApplicationData.Current.RoamingSettings.Values[Path] = Data;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Storage");
            }
        }

        public T Load<T>(string Path, FileStorage Storage = FileStorage.Local)
        {
            switch (Storage)
            {
                case FileStorage.Local:
                    var storageFile = ApplicationData.Current.LocalFolder.GetFileAsync(Path).GetSynchronously();
                    if (typeof(T) == typeof(String))
                        return (T)(object)FileIO.ReadTextAsync(storageFile).GetSynchronously();

                    var stream = storageFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite).GetSynchronously();
                    using (var inputStream = stream.AsStream())
                    {
                        var dcs = new DataContractSerializer(typeof(T));
                        return (T)dcs.ReadObject(inputStream);
                    }

                case FileStorage.Project:
                    storageFile = Package.Current.InstalledLocation.GetFileAsync(Path).GetSynchronously();
                    if (typeof(T) == typeof(String))
                        return (T)(object)FileIO.ReadTextAsync(storageFile).GetSynchronously();

                    stream = storageFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite).GetSynchronously();
                    using (var inputStream = stream.AsStream())
                    {
                        var dcs = new DataContractSerializer(typeof(T));
                        return (T)dcs.ReadObject(inputStream);
                    }

                case FileStorage.Parameter:
                    return (T)ApplicationData.Current.RoamingSettings.Values[Path];

                default:
                    throw new ArgumentOutOfRangeException("Storage");
            }
        }

        public bool Exists(string Path, FileStorage Storage = FileStorage.Local)
        {
            switch (Storage)
            {
                case FileStorage.Local:
                    try
                    {
                        ApplicationData.Current.LocalFolder.GetFileAsync(Path).GetSynchronously();
                        return true;
                    }
                    catch { return false; }
                case FileStorage.Project:
                    try
                    {
                        Package.Current.InstalledLocation.GetFileAsync(Path).GetSynchronously();
                        return true;
                    }
                    catch { return false; }
                case FileStorage.Parameter:
                    return ApplicationData.Current.RoamingSettings.Values.ContainsKey(Path);
                default:
                    throw new ArgumentOutOfRangeException("Storage");
            }
        }
    }
}
