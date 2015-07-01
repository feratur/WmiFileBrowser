namespace WmiFileBrowser.Auxiliary
{
    class ObjectInfoContainer
    {
        public readonly string[] Properties;
        public readonly string ClassName;

        public ObjectInfoContainer(string className, string[] sortedProperties)
        {
            ClassName = className;
            Properties = sortedProperties;
        }
    }
}