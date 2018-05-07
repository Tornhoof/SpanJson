# SpanJson
[![Build status](https://ci.appveyor.com/api/projects/status/h49loskhn09g03in/branch/master?svg=true)](https://ci.appveyor.com/project/Tornhoof/spanjson/branch/master)

## What is supported ##
- Serialization and Deserialization into/from Byte-Arrays and strings
- Serialization and Deserialization of Arrays, Lists, Complex types of the following Base Class Library types:
  - ``sbyte``
  - ``Int16``
  - ``Int32``
  - ``Int64``
  - ``byte`` 
  - ``UInt16``
  - ``UInt32``
  - ``UInt64``
  - ``Single``
  - ``Double``
  - ``decimal``
  - ``bool`` 
  - ``char``
  - ``DateTime``
  - ``DateTimeOffset`` 
  - ``TimeSpan``
  - ``Guid``
  - ``string``
  - ``Version``
  - ``Uri`` 
- DateTime/Offset is in ISO8601 mode  
- Dynamics
- Anonymous types
- Serialization of Enumerables
- Different behaviours:
  - Exclude Nulls with Camel Case
  - Exclude Nulls with Original Case (default)
  - Include Nulls with Camel Case
  - Include Nulls with Original Case
 
## How to use it ##
``var result = JsonSerializer.Generic.Utf16.Serialize(input);``
``var result = JsonSerializer.NonGeneric.Utf16.Serialize(input);``
``var result = JsonSerializer.Generic.Utf16.Deserialize<Input>(input);``
``var result = JsonSerializer.NonGeneric.Utf16.Deserialize(input, typeof(Input));``
``var result = JsonSerializer.Generic.Utf8.Serialize(input);``
``var result = JsonSerializer.NonGeneric.Utf8.Serialize(input);``
``var result = JsonSerializer.Generic.Utf8.Deserialize<Input>(input);``
``var result = JsonSerializer.NonGeneric.Utf8.Deserialize(input, typeof(Input));``

Async Examples:

``ValueTask result = JsonSerializer.Generic.Utf16.SerializeAsync(input, textWriter, cancellationToken);``
``ValueTask result = JsonSerializer.NonGeneric.Utf16.SerializeAsync(input, textWriter, cancellationToken);``
``ValueTask<Input> result = JsonSerializer.Generic.Utf16.DeserializeAsync<Input>(textReader,cancellationToken);``
``ValueTask<object> result = JsonSerializer.NonGeneric.Utf16.DeserializeAsync(textReader,typeof(Input),cancellationToken);``
``ValueTask result = JsonSerializer.Generic.Utf8.SerializeAsync(input, stream, cancellationToken);``
``ValueTask result = JsonSerializer.NonGeneric.Utf8.SerializeAsync(input, stream, cancellationToken);``
``ValueTask<Input> result = JsonSerializer.Generic.Utf8.DeserializeAsync<Input>(input, stream, cancellationToken);``
``ValueTask<object> result = JsonSerializer.NonGeneric.Utf8.DeserializeAsync(input, stream, typeof(Input) cancellationToken);``


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
```

See https://github.com/Tornhoof/SpanJson/wiki/Performance for Benchmarks
