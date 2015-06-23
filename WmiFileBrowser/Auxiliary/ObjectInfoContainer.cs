namespace WmiFileBrowser.Auxiliary
{
    public class ObjectInfoContainer
    {
        public readonly ObjectType Type;
        public readonly string ClassName;
        public readonly string[] Properties;

        public ObjectInfoContainer(ObjectType type, string className, string[] properties)
        {
            Type = type;
            ClassName = className;
            Properties = properties;
        }
    }
}
