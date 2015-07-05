using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class FileSystemObject : WmiObject, IFileDescriptor
    {
        public FileSystemObject(ObjectType type, string[] sortedProperties) : base(sortedProperties)
        {
            Type = type;
        }

        public string ObjectPath
        {
            get { return WmiPath; }
        }

        public ObjectType Type { get; private set; }
    }
}