namespace Freemwork.Services.File
{
    public sealed class NullFileService : IFileService, INullService
    {
        public void Save<T>(string Path, T Data, FileStorage Storage = FileStorage.Local)
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("BeginDraw()");
            #endif
        }

        public T Load<T>(string Path, FileStorage Storage = FileStorage.Local)
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("Load()");
            #endif

            return default(T);
        }

        public bool Exists(string Path, FileStorage Storage = FileStorage.Local)
        {
            #if DEBUG_VERBOSE
                if(Debugger.IsAttached)
                    Debug.WriteLine("Exists()");
            #endif

            return false;
        }
    }
}
