# SpanJson
[![Build status](https://ci.appveyor.com/api/projects/status/h49loskhn09g03in/branch/master?svg=true)](https://ci.appveyor.com/project/Tornhoof/spanjson/branch/master)

See https://github.com/Tornhoof/SpanJson/wiki/Performance for Benchmarks

## What is supported ##
- Serialization and Deserialization into/from byte arrays, strings, TextWriter/TextReader and streams
- Serialization and Deserialization of Arrays, Lists, Complex types of the following Base Class Library types:

``sbyte``, ``Int16``, ``Int32``, ``Int64``, ``byte``,
``UInt16``, ``UInt32``, ``UInt64``, ``Single``, ``Double``,
``decimal``, ``bool``, ``char``, ``DateTime``, ``DateTimeOffset``,
``TimeSpan``, ``Guid``, ``string``, ``Version``, ``Uri``

- Public Properties and Fields are considered for serialization/deserialization
- DateTime{Offset} is in ISO8601 mode  
- Dynamics
- Enums
- Anonymous types
- Dictionar<,> with string as key
- Serialization of Enumerables
- Support for ``[DataMember(Name="MemberName")]`` to set field name
- Support for ``[IgnoreDataMember]`` to ignore a specific member
- Support for ``ShouldSerializeXXX`` pattern to decide at runtime if a member should be serialized

- Different 'Resolvers' to control general behaviour:
  - Exclude Nulls with Camel Case: ``ExcludeNullCamelCaseResolver``
  - Exclude Nulls with Original Case (default): ``IncludeNullCamelCaseResolver``
  - Include Nulls with Camel Case: ``IncludeNullCamelCaseResolver``
  - Include Nulls with Original Case: ``IncludeNullOriginalCaseResolver``
 
 
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

var serialized = JsonSerializer.NonGeneric.Utf16.Serialize<IncludeNullsOriginalCaseResolver<char>>(includeNull);

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
