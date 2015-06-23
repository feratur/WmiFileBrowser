using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class FileSystemObject : WmiObject, IFileDescriptor
    {
        public FileSystemObject(ObjectInfoContainer info) : base(info.Properties)
        {
            Type = info.Type;
        }

        public ObjectType Type { get; private set; }
    }
}
