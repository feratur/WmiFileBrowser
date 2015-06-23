namespace WmiFileBrowser.Interfaces
{
    public interface IPropertyContainer
    {
        string[] PropertyNames { get; }

        object GetPropertyValue(string propertyName);
    }
}
