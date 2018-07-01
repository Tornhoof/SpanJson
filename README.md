# SpanJson
[![Build status](https://ci.appveyor.com/api/projects/status/h49loskhn09g03in/branch/master?svg=true)](https://ci.appveyor.com/project/Tornhoof/spanjson/branch/master)
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
- DateTime{Offset} is in ISO8601 mode  
- Dynamics
- Enums (string and integer, for integer see section Custom Resolver)
- Anonymous types
- Dictionary<,> with string as key
- Serialization of Enumerables
- Support for ``[DataMember(Name="MemberName")]`` to set field name
- Support for ``[IgnoreDataMember]`` to ignore a specific member
- Support for ``ShouldSerializeXXX`` pattern to decide at runtime if a member should be serialized
- Support for ``[EnumMember]`` to specify the string value of the enum value
- Pretty printing JSON
- Support for tuples currently excludes the last type with 8 arguments (TRest)
- Support for annotating a constructor with ``[JsonConstructor]`` to use that one instead of assigning members during deserialization
- Support for custom serializes with ``[JsonCustomSerializer]`` to use that one instead of the normal formatter, see example below

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
    [JsonCustomSerializer(typeof(LongAsStringFormatter))]
    public long Value { get; set; }
}

// Serializes the Long into a string
public sealed class LongAsStringFormatter : ICustomJsonFormatter<long>
{
    public static readonly LongAsStringFormatter Default = new LongAsStringFormatter();

    public void Serialize(ref JsonWriter<char> writer, long value, int nestingLimit)
    {
        StringUtf16Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);
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

    public void Serialize(ref JsonWriter<byte> writer, long value, int nestingLimit)
    {
        StringUtf8Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture), nestingLimit);
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

## ASP.NET Core 2.1 Formatter ##
You can enable SpanJson as the default JSON formatter in ASP.NET Core 2.1 by using the Nuget package [SpanJson.AspNetCore.Formatter](https://www.nuget.org/packages/SpanJson.AspNetCore.Formatter)
To enable it, add one of the following extension methods to the ``AddMvc()`` call in ``ConfigureServices``
* AddSpanJson for a resolver with ASP.NET Core 2.1 defaults: IncludeNull, CamelCase, Integer Enums
* AddSpanJsonCustom for a custom resolver (one of the default resolvers or custom)

```csharp
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().AddSpanJsonIncludeNullsCamelCase().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
}
```

``AddSpanJson`` is the closest in behaviour compared to the default JSON.NET formatter, this used the AspNetCoreDefaultResolver type.
Note: This clears the Formatter list, if you have other formatters, e.g. JSON Patch or XML, you need to re-add them.

## Custom Resolver ##
As each option is a concrete class it is infeasible to supply concrete classes for each possible option combination
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
