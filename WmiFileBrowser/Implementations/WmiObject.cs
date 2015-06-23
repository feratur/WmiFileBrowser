using System;
using System.Management;
using WmiFileBrowser.Interfaces;

namespace WmiFileBrowser.Implementations
{
    class WmiObject : IPropertyContainer
    {
        private readonly string[] _propertyNames;
        private readonly object[] _propertyValues;

        public WmiObject(string[] sortedPropertyNames)
        {
            _propertyNames = sortedPropertyNames;
            _propertyValues = new object[_propertyNames.Length];
        }

        public string[] PropertyNames
        {
            get { return _propertyNames; }
        }

        public object GetPropertyValue(string propertyName)
        {
            return _propertyValues[Array.BinarySearch(_propertyNames, propertyName)];
        }

        public void PopulateProperties(ManagementObject wmiObject)
        {
            for (var i = 0; i < _propertyNames.Length; ++i)
            {
                var property = wmiObject.Properties[_propertyNames[i]];
                if (property.Value == null)
                    continue;
                _propertyValues[i] = TransformPropertyValue(property.Value, property.Type);
            }
        }

        private static object TransformPropertyValue(object value, CimType cimType)
        {
            switch (cimType)
            {
                case CimType.DateTime:
                    return ManagementDateTimeConverter.ToDateTime((string)value);
                default:
                    return value;
            }
        }
    }
}
