namespace WmiFileBrowser.Interfaces
{
    /// <summary>
    /// Provides access to the properties of the object.
    /// </summary>
    public interface IPropertyContainer
    {
        /// <summary>
        /// The names of all the properties of the object.
        /// </summary>
        string[] PropertyNames { get; }

        /// <summary>
        /// Returns the value of the property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns>The name of the desired property.</returns>
        object GetPropertyValue(string propertyName);
    }
}
