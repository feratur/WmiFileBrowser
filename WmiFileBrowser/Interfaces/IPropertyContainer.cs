using System.Collections.Generic;

namespace WmiFileBrowser.Interfaces
{
    public interface IPropertyContainer
    {
        IEnumerable<string> PropertyNames { get; }

        object GetPropertyValue(string propertyName);
    }
}
