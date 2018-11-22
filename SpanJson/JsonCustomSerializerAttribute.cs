using System;
using System.Reflection;
using SpanJson.Helpers;

namespace SpanJson
{
    /// <summary>
    /// The serializer to use, needs to implement ICustomJsonFormatter and needs to have a public static field Default returning an instance of it;
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class)]
    public class JsonCustomSerializerAttribute : Attribute
    {
        public Type Type { get; }

        public object Arguments { get; }

        public JsonCustomSerializerAttribute(Type type, object arguments) : this(type)
        {
            Arguments = arguments;
        }

        public JsonCustomSerializerAttribute(Type type)
        {
            if (!type.TryGetTypeOfGenericInterface(typeof(ICustomJsonFormatter<>), out _))
            {
                throw new InvalidOperationException($"{type.FullName} must implement ICustomJsonFormatter<T>.");
            }

            if (type.GetField("Default", BindingFlags.Static | BindingFlags.Public) == null)
            {
                throw new InvalidOperationException($"{type.FullName} must have a public static field 'Default' returning an instance of it.");
            }

            Type = type;
        }
    }
}