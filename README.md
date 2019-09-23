# SpanJson
[![NuGet](https://img.shields.io/nuget/v/SpanJson.svg)](https://www.nuget.org/packages/SpanJson)

See https://github.com/Tornhoof/SpanJson/wiki/Performance for Benchmarks

## What is supported ##
- Serialization and Deserialization into/from byte arrays, strings, TextWriter/TextReader and streams
- Serialization and Deserialization of Arrays, Lists, Complex types of the following Base Class Library types:

``sbyte``, ``Int16``, ``Int32``, ``Int64``, ``byte``,
``UInt16``, ``UInt32``, ``UInt64``, ``Single``, ``Double``,
``decimal``, ``bool``, ``char``, ``DateTime``, ``DateTimeOffset``,
``TimeSpan``, ``Guid``, ``string``, ``Version``, ``Uri``, ``Tuple<,>``,``ValueTuple<,>``, ``KeyValuePair<,>``
- Public Properties and Fields are considered for serialization/deserialization
- DateTime{Offset} is in ISO8601 mode with profile https://www.w3.org/TR/NOTE-datetime
- Dynamics
- Enums (string and integer, for integer see section Custom Resolver), incl. Flags
- Anonymous types
- ``Dictionary``, ``ConcurrentDictionary`` with string/int/enum as key, the enum is formatted as a string.
- Serialization/Deserialization of most IEnumerable<T> types (Stack and ConcurrentStack are not supported)
- Support for ``[DataMember(Name="MemberName")]`` to set field name
- Support for ``[IgnoreDataMember]`` to ignore a specific member
- Support for ``ShouldSerializeXXX`` pattern to decide at runtime if a member should be serialized
- Support for ``[EnumMember]`` to specify the string value of the enum value
- Support for Immutable Collections, full Serialization/Deserialization for ``ImmutableList``, ``ImmutableArray``, ``ImmutableDictionary``, ``ImmutableSortedDictionary``. ImmutableStack is not supported.
- Support for read-only collections, ``ReadOnlyCollection``, ``ReadOnlyDictionary``, they are deserialized into a writeable type (i.e. List or Dictionary), then the read-only version is created via an appropriate constructor overload.
- Support for tuples currently excludes the last type with 8 arguments (TRest)
- Support for annotating a constructor with ``[JsonConstructor]`` to use that one instead of assigning members during deserialization
- Support for custom serializers with ``[JsonCustomSerializer]`` to use that one instead of the normal formatter, see examples below
- Support for annotating a IDictionary<string,object> with ``[JsonExtensionData]``. Serialization will write all values from the dictionary as additional attributes. Deserialization will deserialize all unknown attributes into it.
  This does not work together with the Dynamic Language Runtime (DLR) support or the ``[JsonConstructor]`` attribute. See Example below. The Dictionary will also honor the Case Setting (i.e. CamelCase) and null behaviour for the dictionary keys.
- Pretty printing JSON
- Minify JSON
- Different 'Resolvers' to control general behaviour:
  - Exclude Nulls with Camel Case: ``ExcludeNullsCamelCaseResolver``
  - Exclude Nulls with Original Case (default): ``ExcludeNullsOriginalCaseResolver``
  - Include Nulls with Camel Case: ``IncludeNullsCamelCaseResolver``
  - Include Nulls with Original Case: ``IncludeNullsOriginalCaseResolver``
  
- Custom Resolvers to control behaviour much more detailed.
 
 
## How to use it ##
```csharp
Synchronous API:

var result = JsonSerializer.Generic.Utf16.Serialize(input);
var result = JsonSerializer.NonGeneric.Utf16.Serialize(input);
var result = JsonSerializer.Generic.Utf16.Deserialize<Input>(input);
var result = JsonSerializer.NonGeneric.Utf16.Deserialize(input, typeof(Input));

var result = JsonSerializer.Generic.Utf8.Serialize(input);
var result = JsonSerializer.NonGeneric.Utf8.Serialize(input);
var result = JsonSerializer.Generic.Utf8.Deserialize<Input>(input);
var result = JsonSerializer.NonGeneric.Utf8.Deserialize(input, typeof(Input));

// The following methods return an ArraySegment from the ArrayPool, you NEED to return it yourself after working with it.
var result = JsonSerializer.Generic.Utf16.SerializeToArrayPool(input);
var result = JsonSerializer.NonGeneric.Utf16.SerializeToArrayPool(input);
var result = JsonSerializer.Generic.Utf8.SerializeToArrayPool(input);
var result = JsonSerializer.NonGeneric.Utf8.SerializeToArrayPool(input);

Asynchronous API:

ValueTask result = JsonSerializer.Generic.Utf16.SerializeAsync(input, textWriter, cancellationToken);
ValueTask result = JsonSerializer.NonGeneric.Utf16.SerializeAsync(input, textWriter, cancellationToken);
ValueTask<Input> result = JsonSerializer.Generic.Utf16.DeserializeAsync<Input>(textReader,cancellationToken);
ValueTask<object> result = JsonSerializer.NonGeneric.Utf16.DeserializeAsync(textReader,typeof(Input),cancellationToken);
ValueTask result = JsonSerializer.Generic.Utf8.SerializeAsync(input, stream, cancellationToken);
ValueTask result = JsonSerializer.NonGeneric.Utf8.SerializeAsync(input, stream, cancellationToken);
ValueTask<Input> result = JsonSerializer.Generic.Utf8.DeserializeAsync<Input>(input, stream, cancellationToken);
ValueTask<object> result = JsonSerializer.NonGeneric.Utf8.DeserializeAsync(input, stream, typeof(Input) cancellationToken);

To use other resolvers use the appropriate overloads,e.g.:

var serialized = JsonSerializer.NonGeneric.Utf16.Serialize<Input, IncludeNullsOriginalCaseResolver<char>>(includeNull);

Pretty Printing:

var pretty = JsonSerializer.PrettyPrinter.Print(serialized); // this works by reading the JSON and writing it out again with spaces and line breaks

Minify:
var minified = JsonSerializer.Minifier.Minify(serialized); // this works by reading the JSON and writing it out again without spaces and line breaks

```

Full example:
```csharp
using System;
using SpanJson;

namespace Test
{
    public class Program
    {
        private static void Main(string[] args)
        {

            var input = new Input { Text = "Hello World" };

            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);

            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Input>(serialized);
        }
    }

    public class Input
    {
        public string Text { get; set; }
    }
}
```

```csharp
using System;
using SpanJson;

namespace Test
{
	// This JsonConstructorAttribute assumes that the constructor parameter names are the same as the member names (case insensitive comparison, order is not important)
	public class DefaultDO
	{
		[JsonConstructor]
		public DefaultDO(string key, int value)
		{
			Key = key;
			Value = value;
		}

		public string Key { get; }
		public int Value { get; }
	}

	// This JsonConstructorAttribute allows overwriting the matching names of the constructor parameter names to allow for different member names vs. constructor parameter names, order is important here
	public readonly struct NamedDO
	{
		[JsonConstructor(nameof(Key), nameof(Value))]
		public NamedDO(string first, int second)
		{
			Key = first;
			Value = second;
		}


		public string Key { get; }
		public int Value { get; }
	}
}
```

```csharp
// Type with a custom serializer to (de)serialize the long value into/from string
public class TestDTO
{
    [JsonCustomSerializer(typeof(LongAsStringFormatter), "Hello World")]
    public long Value { get; set; }
}

// Serializes the Long into a string
public sealed class LongAsStringFormatter : ICustomJsonFormatter<long>
{
    public static readonly LongAsStringFormatter Default = new LongAsStringFormatter();
	
	public object Arguments {get;set;} // the Argument from the attribute will be assigned

    public void Serialize(ref JsonWriter<char> writer, long value)
    {
        StringUtf16Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture));
    }

    public long Deserialize(ref JsonReader<char> reader)
    {
        var value = StringUtf16Formatter.Default.Deserialize(ref reader);
        if (long.TryParse(value, out long longValue))
        {
            return longValue;
        }

        throw new InvalidOperationException("Invalid value.");
    }

    public void Serialize(ref JsonWriter<byte> writer, long value)
    {
        StringUtf8Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture));
    }

    public long Deserialize(ref JsonReader<byte> reader)
    {
        var value = StringUtf8Formatter.Default.Deserialize(ref reader);
        if (long.TryParse(value, out long longValue))
        {
            return longValue;
        }

        throw new InvalidOperationException("Invalid value.");
    }
}
```

```csharp
// It's possible to annotate custom types a custom formatter to always use the custom formatter 
[JsonCustomSerializer(typeof(TwcsCustomSerializer))]
public class TypeWithCustomSerializer : IEquatable<TypeWithCustomSerializer>
{
    public long Value { get; set; }
}

// Instead of copying the implementation of for serialize/deserialize for utf8/utf16
// it is possible to use the writer/reader methods which support both, there is no or only a very minor performance difference
public sealed class TwcsCustomSerializer : ICustomJsonFormatter<TypeWithCustomSerializer>
{
    public static readonly TwcsCustomSerializer Default = new TwcsCustomSerializer();

    public object Arguments { get; set; }

    private void SerializeInternal<TSymbol>(ref JsonWriter<TSymbol> writer, TypeWithCustomSerializer value) where TSymbol : struct
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteBeginObject();

        writer.WriteName(nameof(TypeWithCustomSerializer.Value));

        writer.WriteInt64(value.Value);

        writer.WriteEndObject();
    }

    public void Serialize(ref JsonWriter<byte> writer, TypeWithCustomSerializer value)
    {
        SerializeInternal(ref writer, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TypeWithCustomSerializer DeserializeInternal<TSymbol>(ref JsonReader<TSymbol> reader) where TSymbol : struct
    {
        if (reader.ReadIsNull())
        {
            return null;
        }

        reader.ReadBeginObjectOrThrow();
        var result = new TypeWithCustomSerializer {Value = reader.ReadInt64()};
        reader.ReadEndObjectOrThrow();
        return result;
    }

    public TypeWithCustomSerializer Deserialize(ref JsonReader<byte> reader)
    {
        return DeserializeInternal(ref reader);
    }

    public void Serialize(ref JsonWriter<char> writer, TypeWithCustomSerializer value)
    {
        SerializeInternal(ref writer, value);
    }

    public TypeWithCustomSerializer Deserialize(ref JsonReader<char> reader)
    {
        return DeserializeInternal(ref reader);
    }
}
```

```csharp
// Below class will serialize Key and Value and any additional key-value-pair from the dictionary
public class ExtensionTest
{
    public string Key;
    public string Value;

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalValues { get; set; }
}
```

## ASP.NET Core 2.1 Formatter ##
You can enable SpanJson as the default JSON formatter in ASP.NET Core 2.1 by using the Nuget package [SpanJson.AspNetCore.Formatter](https://www.nuget.org/packages/SpanJson.AspNetCore.Formatter).
To enable it, add one of the following extension methods to the ``AddMvc()`` call in ``ConfigureServices``
* AddSpanJson for a resolver with ASP.NET Core 2.1 defaults: IncludeNull, CamelCase, Integer Enums
* AddSpanJsonCustom for a custom resolver (one of the default resolvers or custom)

```csharp
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddSpanJson().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
}
```

``AddSpanJson`` is the closest in behaviour compared to the default JSON.NET formatter, this used the AspNetCoreDefaultResolver type.
Note: This clears the Formatter list, if you have other formatters, e.g. JSON Patch or XML, you need to re-add them.

## Custom Resolver ##
As each option is a concrete class it is infeasible to supply concrete classes for each possible option combination.
To support a custom combination implement your own custom formatter resolvers
```csharp
public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
{
    public CustomResolver() : base(new SpanJsonOptions
    {
        NullOption = NullOptions.ExcludeNulls,
        NamingConvention = NamingConventions.CamelCase,
        EnumOption = EnumOptions.Integer
    })
    {
    }
}
```
and pass this type just the same as e.g. ``ExcludeNullsCamelCaseResolver``

## TODO ##
- Improve async deserialization/serialization: Find a way to do it streaming instead of buffering.
