using WmiFileBrowser.Auxiliary;

namespace WmiFileBrowser.Interfaces
{
    /// <summary>
    /// Describes the object of the filesystem.
    /// </summary>
    public interface IFileDescriptor : IPropertyContainer
    {
        /// <summary>
        /// The management path of the object.
        /// </summary>
        string ObjectPath { get; }
        
        /// <summary>
        /// The type of the object.
        /// </summary>
        ObjectType Type { get; }
    }
}
