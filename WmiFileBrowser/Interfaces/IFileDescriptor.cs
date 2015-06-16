using WmiFileBrowser.Auxiliary;

namespace WmiFileBrowser.Interfaces
{
    public interface IFileDescriptor : IPropertyContainer
    {
        ObjectType Type { get; }
    }
}
