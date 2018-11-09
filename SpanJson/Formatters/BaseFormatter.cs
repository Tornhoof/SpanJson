using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SpanJson.Formatters
{
    public abstract class BaseFormatter
    {
        protected static MethodInfo FindPublicInstanceMethod(Type type, string name, params Type[] args)
        {
            return args?.Length > 0 ? type.GetMethod(name, args) : type.GetMethod(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer,
            T value, IJsonFormatter<T, TSymbol> formatter,
            int nextNestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            // The first check is get around the runtime check for primitive types an structs without references, i.e most of the blc types from the bclformatter.tt
            // Then we specifically check for string as it is very common
            // a null value can be serialized by both (doesn't matter, but we need to handle null for the runtime type check)
            // Checking for things like IsValueType and/or IsSealed is actually several times more expensive than the type comparison
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>() || typeof(T) == typeof(string) || value == null || value.GetType() == typeof(T))
            {
                formatter.Serialize(ref writer, value, nextNestingLimit);
            }
            else
            {
                RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, value, nextNestingLimit);
            }
        }
    }
}