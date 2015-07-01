using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class FileSystemObject : WmiObject, IFileDescriptor
    {
        public FileSystemObject(ObjectInfoContainer info) : base(info.Properties)
        {
            Type = ResolveType(info.ClassName);
        }

        public string ObjectPath
        {
            get { return WmiPath; }
        }

        public ObjectType Type { get; private set; }

        private static ObjectType ResolveType(string className)
        {
            switch (className)
            {
                case "cim_datafile":
                    return ObjectType.File;
                case "win32_directory":
                    return ObjectType.Directory;
                case "win32_volume":
                    return ObjectType.Drive;
                default:
                    return ObjectType.Unknown;
            }
        }
    }
}