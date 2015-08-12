using System;

namespace Freemwork.Services.File
{
    public enum FileStorage { Local, Project, Parameter }

    public interface IFileService : IService
    {
        void Save<T>(String Path, T Data, FileStorage Storage = FileStorage.Local);
        T Load<T>(String Path, FileStorage Storage = FileStorage.Local);
        bool Exists(String Path, FileStorage Storage = FileStorage.Local);
    }
}
