using WmiFileBrowser.Auxiliary;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class WmiFile : WmiObject, IFileDescriptor
    {
        private static readonly string[] Properties =
        {
            "AccessMask",
            "Archive",
            "Caption",
            "Compressed",
            "CompressionMethod",
            "CreationClassName",
            "CreationDate",
            "CSCreationClassName",
            "CSName",
            "Description",
            "Drive",
            "EightDotThreeFileName",
            "Encrypted",
            "EncryptionMethod",
            "Extension",
            "FileName",
            "FileSize",
            "FileType",
            "FSCreationClassName",
            "FSName",
            "Hidden",
            "InstallDate",
            "InUseCount",
            "LastAccessed",
            "LastModified",
            "Manufacturer",
            "Name",
            "Path",
            "Readable",
            "Status",
            "System",
            "Version",
            "Writeable"
        };
        
        public WmiFile() : base(Properties)
        {
        }

        public ObjectType Type
        {
            get { return ObjectType.File; }
        }
    }
}
