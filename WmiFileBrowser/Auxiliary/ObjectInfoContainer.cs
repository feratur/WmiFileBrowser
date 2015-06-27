namespace WmiFileBrowser.Auxiliary
{
    class ObjectInfoContainer
    {
        public readonly ObjectType Type;
        public readonly string ClassName;
        public readonly string[] Properties;

        public ObjectInfoContainer(ObjectType type, string className, string[] sortedProperties)
        {
            Type = type;
            ClassName = className;
            Properties = sortedProperties;
        }
    }
}