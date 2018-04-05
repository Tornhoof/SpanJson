using System;
using System.Collections.Generic;
using SpanJson;
using SpanJson.Formatters;
// Autogenerate
// ReSharper disable BuiltInTypeReferenceStyle
namespace SpanJson.Formatters.Generated
{
    public sealed class SByteFormatter : IJsonFormatter<SByte>
    {
        public static readonly SByteFormatter Default = new SByteFormatter();

        public void Serialize(ref JsonWriter writer, SByte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSByte(value);
        }

        public SByte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSByte();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableSByteFormatter : NullableFormatter, IJsonFormatter<SByte?>
    {
        public static readonly NullableSByteFormatter Default = new NullableSByteFormatter();

        public void Serialize(ref JsonWriter writer, SByte? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, SByteFormatter.Default, formatterResolver);
        }

        public SByte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, SByteFormatter.Default, formatterResolver);
        }
	}

    public sealed class SByteArrayFormatter : ArrayFormatter, IJsonFormatter<SByte[]>
    {
        public static readonly SByteArrayFormatter Default = new SByteArrayFormatter();

        public void Serialize(ref JsonWriter writer, SByte[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, SByteFormatter.Default, formatterResolver);
        }

        public SByte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, SByteFormatter.Default, formatterResolver);
        }
	}

	public sealed class SByteListFormatter : ListFormatter, IJsonFormatter<List<SByte>>
    {
        public static readonly SByteListFormatter Default = new SByteListFormatter();

        public void Serialize(ref JsonWriter writer, List<SByte> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, SByteFormatter.Default, formatterResolver);
        }

        public List<SByte> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, SByteFormatter.Default, formatterResolver);
        }	
	}
    public sealed class Int16Formatter : IJsonFormatter<Int16>
    {
        public static readonly Int16Formatter Default = new Int16Formatter();

        public void Serialize(ref JsonWriter writer, Int16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt16(value);
        }

        public Int16 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt16();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableInt16Formatter : NullableFormatter, IJsonFormatter<Int16?>
    {
        public static readonly NullableInt16Formatter Default = new NullableInt16Formatter();

        public void Serialize(ref JsonWriter writer, Int16? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, Int16Formatter.Default, formatterResolver);
        }

        public Int16? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, Int16Formatter.Default, formatterResolver);
        }
	}

    public sealed class Int16ArrayFormatter : ArrayFormatter, IJsonFormatter<Int16[]>
    {
        public static readonly Int16ArrayFormatter Default = new Int16ArrayFormatter();

        public void Serialize(ref JsonWriter writer, Int16[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int16Formatter.Default, formatterResolver);
        }

        public Int16[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int16Formatter.Default, formatterResolver);
        }
	}

	public sealed class Int16ListFormatter : ListFormatter, IJsonFormatter<List<Int16>>
    {
        public static readonly Int16ListFormatter Default = new Int16ListFormatter();

        public void Serialize(ref JsonWriter writer, List<Int16> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int16Formatter.Default, formatterResolver);
        }

        public List<Int16> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int16Formatter.Default, formatterResolver);
        }	
	}
    public sealed class Int32Formatter : IJsonFormatter<Int32>
    {
        public static readonly Int32Formatter Default = new Int32Formatter();

        public void Serialize(ref JsonWriter writer, Int32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt32(value);
        }

        public Int32 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt32();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableInt32Formatter : NullableFormatter, IJsonFormatter<Int32?>
    {
        public static readonly NullableInt32Formatter Default = new NullableInt32Formatter();

        public void Serialize(ref JsonWriter writer, Int32? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, Int32Formatter.Default, formatterResolver);
        }

        public Int32? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, Int32Formatter.Default, formatterResolver);
        }
	}

    public sealed class Int32ArrayFormatter : ArrayFormatter, IJsonFormatter<Int32[]>
    {
        public static readonly Int32ArrayFormatter Default = new Int32ArrayFormatter();

        public void Serialize(ref JsonWriter writer, Int32[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int32Formatter.Default, formatterResolver);
        }

        public Int32[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int32Formatter.Default, formatterResolver);
        }
	}

	public sealed class Int32ListFormatter : ListFormatter, IJsonFormatter<List<Int32>>
    {
        public static readonly Int32ListFormatter Default = new Int32ListFormatter();

        public void Serialize(ref JsonWriter writer, List<Int32> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int32Formatter.Default, formatterResolver);
        }

        public List<Int32> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int32Formatter.Default, formatterResolver);
        }	
	}
    public sealed class Int64Formatter : IJsonFormatter<Int64>
    {
        public static readonly Int64Formatter Default = new Int64Formatter();

        public void Serialize(ref JsonWriter writer, Int64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt64(value);
        }

        public Int64 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt64();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableInt64Formatter : NullableFormatter, IJsonFormatter<Int64?>
    {
        public static readonly NullableInt64Formatter Default = new NullableInt64Formatter();

        public void Serialize(ref JsonWriter writer, Int64? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, Int64Formatter.Default, formatterResolver);
        }

        public Int64? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, Int64Formatter.Default, formatterResolver);
        }
	}

    public sealed class Int64ArrayFormatter : ArrayFormatter, IJsonFormatter<Int64[]>
    {
        public static readonly Int64ArrayFormatter Default = new Int64ArrayFormatter();

        public void Serialize(ref JsonWriter writer, Int64[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int64Formatter.Default, formatterResolver);
        }

        public Int64[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int64Formatter.Default, formatterResolver);
        }
	}

	public sealed class Int64ListFormatter : ListFormatter, IJsonFormatter<List<Int64>>
    {
        public static readonly Int64ListFormatter Default = new Int64ListFormatter();

        public void Serialize(ref JsonWriter writer, List<Int64> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, Int64Formatter.Default, formatterResolver);
        }

        public List<Int64> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, Int64Formatter.Default, formatterResolver);
        }	
	}
    public sealed class ByteFormatter : IJsonFormatter<Byte>
    {
        public static readonly ByteFormatter Default = new ByteFormatter();

        public void Serialize(ref JsonWriter writer, Byte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteByte(value);
        }

        public Byte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadByte();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableByteFormatter : NullableFormatter, IJsonFormatter<Byte?>
    {
        public static readonly NullableByteFormatter Default = new NullableByteFormatter();

        public void Serialize(ref JsonWriter writer, Byte? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, ByteFormatter.Default, formatterResolver);
        }

        public Byte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, ByteFormatter.Default, formatterResolver);
        }
	}

    public sealed class ByteArrayFormatter : ArrayFormatter, IJsonFormatter<Byte[]>
    {
        public static readonly ByteArrayFormatter Default = new ByteArrayFormatter();

        public void Serialize(ref JsonWriter writer, Byte[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, ByteFormatter.Default, formatterResolver);
        }

        public Byte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, ByteFormatter.Default, formatterResolver);
        }
	}

	public sealed class ByteListFormatter : ListFormatter, IJsonFormatter<List<Byte>>
    {
        public static readonly ByteListFormatter Default = new ByteListFormatter();

        public void Serialize(ref JsonWriter writer, List<Byte> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, ByteFormatter.Default, formatterResolver);
        }

        public List<Byte> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, ByteFormatter.Default, formatterResolver);
        }	
	}
    public sealed class UInt16Formatter : IJsonFormatter<UInt16>
    {
        public static readonly UInt16Formatter Default = new UInt16Formatter();

        public void Serialize(ref JsonWriter writer, UInt16 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt16(value);
        }

        public UInt16 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt16();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableUInt16Formatter : NullableFormatter, IJsonFormatter<UInt16?>
    {
        public static readonly NullableUInt16Formatter Default = new NullableUInt16Formatter();

        public void Serialize(ref JsonWriter writer, UInt16? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, UInt16Formatter.Default, formatterResolver);
        }

        public UInt16? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, UInt16Formatter.Default, formatterResolver);
        }
	}

    public sealed class UInt16ArrayFormatter : ArrayFormatter, IJsonFormatter<UInt16[]>
    {
        public static readonly UInt16ArrayFormatter Default = new UInt16ArrayFormatter();

        public void Serialize(ref JsonWriter writer, UInt16[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt16Formatter.Default, formatterResolver);
        }

        public UInt16[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt16Formatter.Default, formatterResolver);
        }
	}

	public sealed class UInt16ListFormatter : ListFormatter, IJsonFormatter<List<UInt16>>
    {
        public static readonly UInt16ListFormatter Default = new UInt16ListFormatter();

        public void Serialize(ref JsonWriter writer, List<UInt16> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt16Formatter.Default, formatterResolver);
        }

        public List<UInt16> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt16Formatter.Default, formatterResolver);
        }	
	}
    public sealed class UInt32Formatter : IJsonFormatter<UInt32>
    {
        public static readonly UInt32Formatter Default = new UInt32Formatter();

        public void Serialize(ref JsonWriter writer, UInt32 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt32(value);
        }

        public UInt32 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt32();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableUInt32Formatter : NullableFormatter, IJsonFormatter<UInt32?>
    {
        public static readonly NullableUInt32Formatter Default = new NullableUInt32Formatter();

        public void Serialize(ref JsonWriter writer, UInt32? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, UInt32Formatter.Default, formatterResolver);
        }

        public UInt32? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, UInt32Formatter.Default, formatterResolver);
        }
	}

    public sealed class UInt32ArrayFormatter : ArrayFormatter, IJsonFormatter<UInt32[]>
    {
        public static readonly UInt32ArrayFormatter Default = new UInt32ArrayFormatter();

        public void Serialize(ref JsonWriter writer, UInt32[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt32Formatter.Default, formatterResolver);
        }

        public UInt32[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt32Formatter.Default, formatterResolver);
        }
	}

	public sealed class UInt32ListFormatter : ListFormatter, IJsonFormatter<List<UInt32>>
    {
        public static readonly UInt32ListFormatter Default = new UInt32ListFormatter();

        public void Serialize(ref JsonWriter writer, List<UInt32> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt32Formatter.Default, formatterResolver);
        }

        public List<UInt32> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt32Formatter.Default, formatterResolver);
        }	
	}
    public sealed class UInt64Formatter : IJsonFormatter<UInt64>
    {
        public static readonly UInt64Formatter Default = new UInt64Formatter();

        public void Serialize(ref JsonWriter writer, UInt64 value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt64(value);
        }

        public UInt64 Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt64();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableUInt64Formatter : NullableFormatter, IJsonFormatter<UInt64?>
    {
        public static readonly NullableUInt64Formatter Default = new NullableUInt64Formatter();

        public void Serialize(ref JsonWriter writer, UInt64? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, UInt64Formatter.Default, formatterResolver);
        }

        public UInt64? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, UInt64Formatter.Default, formatterResolver);
        }
	}

    public sealed class UInt64ArrayFormatter : ArrayFormatter, IJsonFormatter<UInt64[]>
    {
        public static readonly UInt64ArrayFormatter Default = new UInt64ArrayFormatter();

        public void Serialize(ref JsonWriter writer, UInt64[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt64Formatter.Default, formatterResolver);
        }

        public UInt64[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt64Formatter.Default, formatterResolver);
        }
	}

	public sealed class UInt64ListFormatter : ListFormatter, IJsonFormatter<List<UInt64>>
    {
        public static readonly UInt64ListFormatter Default = new UInt64ListFormatter();

        public void Serialize(ref JsonWriter writer, List<UInt64> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, UInt64Formatter.Default, formatterResolver);
        }

        public List<UInt64> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, UInt64Formatter.Default, formatterResolver);
        }	
	}
    public sealed class SingleFormatter : IJsonFormatter<Single>
    {
        public static readonly SingleFormatter Default = new SingleFormatter();

        public void Serialize(ref JsonWriter writer, Single value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSingle(value);
        }

        public Single Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSingle();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableSingleFormatter : NullableFormatter, IJsonFormatter<Single?>
    {
        public static readonly NullableSingleFormatter Default = new NullableSingleFormatter();

        public void Serialize(ref JsonWriter writer, Single? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, SingleFormatter.Default, formatterResolver);
        }

        public Single? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, SingleFormatter.Default, formatterResolver);
        }
	}

    public sealed class SingleArrayFormatter : ArrayFormatter, IJsonFormatter<Single[]>
    {
        public static readonly SingleArrayFormatter Default = new SingleArrayFormatter();

        public void Serialize(ref JsonWriter writer, Single[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, SingleFormatter.Default, formatterResolver);
        }

        public Single[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, SingleFormatter.Default, formatterResolver);
        }
	}

	public sealed class SingleListFormatter : ListFormatter, IJsonFormatter<List<Single>>
    {
        public static readonly SingleListFormatter Default = new SingleListFormatter();

        public void Serialize(ref JsonWriter writer, List<Single> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, SingleFormatter.Default, formatterResolver);
        }

        public List<Single> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, SingleFormatter.Default, formatterResolver);
        }	
	}
    public sealed class DoubleFormatter : IJsonFormatter<Double>
    {
        public static readonly DoubleFormatter Default = new DoubleFormatter();

        public void Serialize(ref JsonWriter writer, Double value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDouble(value);
        }

        public Double Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDouble();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableDoubleFormatter : NullableFormatter, IJsonFormatter<Double?>
    {
        public static readonly NullableDoubleFormatter Default = new NullableDoubleFormatter();

        public void Serialize(ref JsonWriter writer, Double? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DoubleFormatter.Default, formatterResolver);
        }

        public Double? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, DoubleFormatter.Default, formatterResolver);
        }
	}

    public sealed class DoubleArrayFormatter : ArrayFormatter, IJsonFormatter<Double[]>
    {
        public static readonly DoubleArrayFormatter Default = new DoubleArrayFormatter();

        public void Serialize(ref JsonWriter writer, Double[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DoubleFormatter.Default, formatterResolver);
        }

        public Double[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DoubleFormatter.Default, formatterResolver);
        }
	}

	public sealed class DoubleListFormatter : ListFormatter, IJsonFormatter<List<Double>>
    {
        public static readonly DoubleListFormatter Default = new DoubleListFormatter();

        public void Serialize(ref JsonWriter writer, List<Double> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DoubleFormatter.Default, formatterResolver);
        }

        public List<Double> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DoubleFormatter.Default, formatterResolver);
        }	
	}
    public sealed class BooleanFormatter : IJsonFormatter<Boolean>
    {
        public static readonly BooleanFormatter Default = new BooleanFormatter();

        public void Serialize(ref JsonWriter writer, Boolean value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBoolean(value);
        }

        public Boolean Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadBoolean();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableBooleanFormatter : NullableFormatter, IJsonFormatter<Boolean?>
    {
        public static readonly NullableBooleanFormatter Default = new NullableBooleanFormatter();

        public void Serialize(ref JsonWriter writer, Boolean? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, BooleanFormatter.Default, formatterResolver);
        }

        public Boolean? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, BooleanFormatter.Default, formatterResolver);
        }
	}

    public sealed class BooleanArrayFormatter : ArrayFormatter, IJsonFormatter<Boolean[]>
    {
        public static readonly BooleanArrayFormatter Default = new BooleanArrayFormatter();

        public void Serialize(ref JsonWriter writer, Boolean[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, BooleanFormatter.Default, formatterResolver);
        }

        public Boolean[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, BooleanFormatter.Default, formatterResolver);
        }
	}

	public sealed class BooleanListFormatter : ListFormatter, IJsonFormatter<List<Boolean>>
    {
        public static readonly BooleanListFormatter Default = new BooleanListFormatter();

        public void Serialize(ref JsonWriter writer, List<Boolean> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, BooleanFormatter.Default, formatterResolver);
        }

        public List<Boolean> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, BooleanFormatter.Default, formatterResolver);
        }	
	}
    public sealed class CharFormatter : IJsonFormatter<Char>
    {
        public static readonly CharFormatter Default = new CharFormatter();

        public void Serialize(ref JsonWriter writer, Char value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteChar(value);
        }

        public Char Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadChar();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableCharFormatter : NullableFormatter, IJsonFormatter<Char?>
    {
        public static readonly NullableCharFormatter Default = new NullableCharFormatter();

        public void Serialize(ref JsonWriter writer, Char? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, CharFormatter.Default, formatterResolver);
        }

        public Char? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, CharFormatter.Default, formatterResolver);
        }
	}

    public sealed class CharArrayFormatter : ArrayFormatter, IJsonFormatter<Char[]>
    {
        public static readonly CharArrayFormatter Default = new CharArrayFormatter();

        public void Serialize(ref JsonWriter writer, Char[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, CharFormatter.Default, formatterResolver);
        }

        public Char[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, CharFormatter.Default, formatterResolver);
        }
	}

	public sealed class CharListFormatter : ListFormatter, IJsonFormatter<List<Char>>
    {
        public static readonly CharListFormatter Default = new CharListFormatter();

        public void Serialize(ref JsonWriter writer, List<Char> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, CharFormatter.Default, formatterResolver);
        }

        public List<Char> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, CharFormatter.Default, formatterResolver);
        }	
	}
    public sealed class DateTimeFormatter : IJsonFormatter<DateTime>
    {
        public static readonly DateTimeFormatter Default = new DateTimeFormatter();

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDateTime(value);
        }

        public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDateTime();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableDateTimeFormatter : NullableFormatter, IJsonFormatter<DateTime?>
    {
        public static readonly NullableDateTimeFormatter Default = new NullableDateTimeFormatter();

        public void Serialize(ref JsonWriter writer, DateTime? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DateTimeFormatter.Default, formatterResolver);
        }

        public DateTime? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, DateTimeFormatter.Default, formatterResolver);
        }
	}

    public sealed class DateTimeArrayFormatter : ArrayFormatter, IJsonFormatter<DateTime[]>
    {
        public static readonly DateTimeArrayFormatter Default = new DateTimeArrayFormatter();

        public void Serialize(ref JsonWriter writer, DateTime[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DateTimeFormatter.Default, formatterResolver);
        }

        public DateTime[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DateTimeFormatter.Default, formatterResolver);
        }
	}

	public sealed class DateTimeListFormatter : ListFormatter, IJsonFormatter<List<DateTime>>
    {
        public static readonly DateTimeListFormatter Default = new DateTimeListFormatter();

        public void Serialize(ref JsonWriter writer, List<DateTime> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DateTimeFormatter.Default, formatterResolver);
        }

        public List<DateTime> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DateTimeFormatter.Default, formatterResolver);
        }	
	}
    public sealed class DateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset>
    {
        public static readonly DateTimeOffsetFormatter Default = new DateTimeOffsetFormatter();

        public void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDateTimeOffset(value);
        }

        public DateTimeOffset Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDateTimeOffset();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableDateTimeOffsetFormatter : NullableFormatter, IJsonFormatter<DateTimeOffset?>
    {
        public static readonly NullableDateTimeOffsetFormatter Default = new NullableDateTimeOffsetFormatter();

        public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DateTimeOffsetFormatter.Default, formatterResolver);
        }

        public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, DateTimeOffsetFormatter.Default, formatterResolver);
        }
	}

    public sealed class DateTimeOffsetArrayFormatter : ArrayFormatter, IJsonFormatter<DateTimeOffset[]>
    {
        public static readonly DateTimeOffsetArrayFormatter Default = new DateTimeOffsetArrayFormatter();

        public void Serialize(ref JsonWriter writer, DateTimeOffset[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DateTimeOffsetFormatter.Default, formatterResolver);
        }

        public DateTimeOffset[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DateTimeOffsetFormatter.Default, formatterResolver);
        }
	}

	public sealed class DateTimeOffsetListFormatter : ListFormatter, IJsonFormatter<List<DateTimeOffset>>
    {
        public static readonly DateTimeOffsetListFormatter Default = new DateTimeOffsetListFormatter();

        public void Serialize(ref JsonWriter writer, List<DateTimeOffset> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DateTimeOffsetFormatter.Default, formatterResolver);
        }

        public List<DateTimeOffset> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DateTimeOffsetFormatter.Default, formatterResolver);
        }	
	}
    public sealed class TimeSpanFormatter : IJsonFormatter<TimeSpan>
    {
        public static readonly TimeSpanFormatter Default = new TimeSpanFormatter();

        public void Serialize(ref JsonWriter writer, TimeSpan value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteTimeSpan(value);
        }

        public TimeSpan Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadTimeSpan();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableTimeSpanFormatter : NullableFormatter, IJsonFormatter<TimeSpan?>
    {
        public static readonly NullableTimeSpanFormatter Default = new NullableTimeSpanFormatter();

        public void Serialize(ref JsonWriter writer, TimeSpan? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, TimeSpanFormatter.Default, formatterResolver);
        }

        public TimeSpan? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, TimeSpanFormatter.Default, formatterResolver);
        }
	}

    public sealed class TimeSpanArrayFormatter : ArrayFormatter, IJsonFormatter<TimeSpan[]>
    {
        public static readonly TimeSpanArrayFormatter Default = new TimeSpanArrayFormatter();

        public void Serialize(ref JsonWriter writer, TimeSpan[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, TimeSpanFormatter.Default, formatterResolver);
        }

        public TimeSpan[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, TimeSpanFormatter.Default, formatterResolver);
        }
	}

	public sealed class TimeSpanListFormatter : ListFormatter, IJsonFormatter<List<TimeSpan>>
    {
        public static readonly TimeSpanListFormatter Default = new TimeSpanListFormatter();

        public void Serialize(ref JsonWriter writer, List<TimeSpan> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, TimeSpanFormatter.Default, formatterResolver);
        }

        public List<TimeSpan> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, TimeSpanFormatter.Default, formatterResolver);
        }	
	}
    public sealed class GuidFormatter : IJsonFormatter<Guid>
    {
        public static readonly GuidFormatter Default = new GuidFormatter();

        public void Serialize(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteGuid(value);
        }

        public Guid Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadGuid();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableGuidFormatter : NullableFormatter, IJsonFormatter<Guid?>
    {
        public static readonly NullableGuidFormatter Default = new NullableGuidFormatter();

        public void Serialize(ref JsonWriter writer, Guid? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, GuidFormatter.Default, formatterResolver);
        }

        public Guid? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, GuidFormatter.Default, formatterResolver);
        }
	}

    public sealed class GuidArrayFormatter : ArrayFormatter, IJsonFormatter<Guid[]>
    {
        public static readonly GuidArrayFormatter Default = new GuidArrayFormatter();

        public void Serialize(ref JsonWriter writer, Guid[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, GuidFormatter.Default, formatterResolver);
        }

        public Guid[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, GuidFormatter.Default, formatterResolver);
        }
	}

	public sealed class GuidListFormatter : ListFormatter, IJsonFormatter<List<Guid>>
    {
        public static readonly GuidListFormatter Default = new GuidListFormatter();

        public void Serialize(ref JsonWriter writer, List<Guid> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, GuidFormatter.Default, formatterResolver);
        }

        public List<Guid> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, GuidFormatter.Default, formatterResolver);
        }	
	}
    public sealed class StringFormatter : IJsonFormatter<String>
    {
        public static readonly StringFormatter Default = new StringFormatter();

        public void Serialize(ref JsonWriter writer, String value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value);
        }

        public String Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadString();
        }

		public int AllocSize {get;} = 100;
	} 

    public sealed class StringArrayFormatter : ArrayFormatter, IJsonFormatter<String[]>
    {
        public static readonly StringArrayFormatter Default = new StringArrayFormatter();

        public void Serialize(ref JsonWriter writer, String[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, StringFormatter.Default, formatterResolver);
        }

        public String[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, StringFormatter.Default, formatterResolver);
        }
	}

	public sealed class StringListFormatter : ListFormatter, IJsonFormatter<List<String>>
    {
        public static readonly StringListFormatter Default = new StringListFormatter();

        public void Serialize(ref JsonWriter writer, List<String> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, StringFormatter.Default, formatterResolver);
        }

        public List<String> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, StringFormatter.Default, formatterResolver);
        }	
	}
    public sealed class DecimalFormatter : IJsonFormatter<Decimal>
    {
        public static readonly DecimalFormatter Default = new DecimalFormatter();

        public void Serialize(ref JsonWriter writer, Decimal value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDecimal(value);
        }

        public Decimal Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDecimal();
        }

		public int AllocSize {get;} = 100;
	} 
	public sealed class NullableDecimalFormatter : NullableFormatter, IJsonFormatter<Decimal?>
    {
        public static readonly NullableDecimalFormatter Default = new NullableDecimalFormatter();

        public void Serialize(ref JsonWriter writer, Decimal? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DecimalFormatter.Default, formatterResolver);
        }

        public Decimal? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
			return Deserialize(ref reader, DecimalFormatter.Default, formatterResolver);
        }
	}

    public sealed class DecimalArrayFormatter : ArrayFormatter, IJsonFormatter<Decimal[]>
    {
        public static readonly DecimalArrayFormatter Default = new DecimalArrayFormatter();

        public void Serialize(ref JsonWriter writer, Decimal[] value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DecimalFormatter.Default, formatterResolver);
        }

        public Decimal[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DecimalFormatter.Default, formatterResolver);
        }
	}

	public sealed class DecimalListFormatter : ListFormatter, IJsonFormatter<List<Decimal>>
    {
        public static readonly DecimalListFormatter Default = new DecimalListFormatter();

        public void Serialize(ref JsonWriter writer, List<Decimal> value, IJsonFormatterResolver formatterResolver)
        {
			Serialize(ref writer, value, DecimalFormatter.Default, formatterResolver);
        }

        public List<Decimal> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DecimalFormatter.Default, formatterResolver);
        }	
	}
}