using System;
using System.Collections.Generic;

namespace SpanJson.Tests
{
    public static class ValueHelper
    {
        public static void RandomlySetValuesToNull(object input, int modValue)
        {
            Random prng = new Random();
            Queue<object> values = new Queue<object>();
            values.Enqueue(input);
            while (values.Count > 0)
            {
                var current = values.Dequeue();
                var type = current.GetType();

                var props = type.GetProperties();
                foreach (var propertyInfo in props)
                {
                    var randValue = prng.Next(modValue);
                    if (randValue == 0 && (propertyInfo.PropertyType.IsClass || Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null) &&
                        propertyInfo.CanWrite)
                    {
                        propertyInfo.SetValue(current, null);
                    }
                    else if (propertyInfo.PropertyType.IsClass && !propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType != typeof(string))
                    {
                        var value = propertyInfo.GetValue(current);
                        if (value != null)
                        {
                            values.Enqueue(value);
                        }
                    }
                }
            }
        }
    }
}