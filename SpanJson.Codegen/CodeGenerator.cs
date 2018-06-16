using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using SpanJson.Formatters;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Codegen
{
    public static class CodeGenerator<TSymbol, TResolver> where TSymbol : struct where TResolver : class, IJsonFormatterResolver<TSymbol, TResolver>, new()
    {
        private static readonly string SymbolName = GetSymbolName();
        private static readonly IJsonFormatterResolver<TSymbol, TResolver> Resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
        public static string Generate(Type[] dataTypes)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using SpanJson.Benchmarks.Models;");
            sb.AppendLine("using SpanJson.Codegen;");
            sb.AppendLine("using SpanJson.Formatters;");
            sb.AppendLine("using SpanJson.Resolvers;");
            sb.AppendLine();
            sb.AppendLine("namespace SpanJson.Generated");
            sb.AppendLine("{");
            Queue<Type> queue = new Queue<Type>();
            foreach (var dataType in dataTypes)
            {
                queue.Enqueue(dataType);
            }
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            HashSet<Type> alreadyDone = new HashSet<Type>();
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (!alreadyDone.Add(current))
                {
                    continue;
                }

                var currentFullName = current.FullName.Replace('+', '.');
                var objectDescription = resolver.GetObjectDescription(current);
                sb.AppendLine(
                    $"public sealed class {current.Name}{SymbolName}Formatter : BaseGeneratedFormatter<{currentFullName},{typeof(TSymbol).Name},ExcludeNullsOriginalCaseResolver<{typeof(TSymbol).Name}>>, IJsonFormatter<{currentFullName}, {typeof(TSymbol).Name}, ExcludeNullsOriginalCaseResolver<{typeof(TSymbol).Name}>>");
                sb.AppendLine("{");
                GenerateSerializerNameFields(sb, current, objectDescription);
                sb.AppendLine(
                    $"public static readonly {current.Name}{SymbolName}Formatter Default = new {current.Name}{SymbolName}Formatter();");
                sb.AppendLine($"public {currentFullName} Deserialize(ref JsonReader<{typeof(TSymbol).Name}> reader)");
                sb.AppendLine("{");
                GenerateDeserializer(sb, current, objectDescription, queue);
                sb.AppendLine("}");
                sb.AppendLine($"public void Serialize(ref JsonWriter<{typeof(TSymbol).Name}> writer, {current.FullName.Replace('+', '.')} value, int nestingLimit)");
                sb.AppendLine("{");
                GenerateSerializer(sb, current, objectDescription, queue);
                sb.AppendLine("}");
                sb.AppendLine("}");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        private static void GenerateSerializerNameFields(StringBuilder sb, Type current, JsonObjectDescription objectDescription)
        {
            foreach (var memberInfo in objectDescription.Members)
            {
                if (memberInfo.CanWrite)
                {
                    if (typeof(TSymbol) == typeof(char))
                    {
                        sb.AppendLine($"private const string _{memberInfo.MemberName}Name = \"\\\"{memberInfo.Name}\\\":\";");
                    }
                    else if (typeof(TSymbol) == typeof(byte))
                    {
                        sb.AppendLine($"private readonly byte[] _{memberInfo.MemberName}Name = Encoding.UTF8.GetBytes(\"\\\"{memberInfo.Name}\\\":\");");
                    }
                }
            }
        }

        private static void GenerateSerializer(StringBuilder sb, Type current, JsonObjectDescription objectDescription, Queue<Type> queue)
        {
            sb.AppendLine("if(value == null)");
            sb.AppendLine("{");
            sb.AppendLine($"writer.Write{SymbolName}Null();");
            sb.AppendLine("return;");
            sb.AppendLine("}");
            sb.AppendLine($"writer.Write{SymbolName}BeginObject();");
            sb.AppendLine("var writeSeparator = false;");
            int counter = 0;
            foreach (var memberInfo in objectDescription.Members)
            {
                if (memberInfo.CanWrite)
                {
                    if (memberInfo.MemberType.IsNullable())
                    {
                        sb.AppendLine($"if(value.{memberInfo.MemberName} != null)");
                        sb.AppendLine("{");
                        GenerateWriteValue(sb, memberInfo, counter++, queue);
                        sb.AppendLine("}");
                    }
                    else
                    {
                        GenerateWriteValue(sb, memberInfo, counter++, queue);
                    }

                }
            }
            sb.AppendLine($"writer.Write{SymbolName}EndObject();");
        }

        private static void GenerateWriteValue(StringBuilder sb, JsonMemberInfo memberInfo, int counter, Queue<Type> queue)
        {
            if (counter > 0)
            {
                sb.AppendLine("if(writeSeparator)");
                sb.AppendLine("{");
                sb.AppendLine($"writer.Write{SymbolName}ValueSeparator();");
                sb.AppendLine("}");
            }

            sb.AppendLine($"writer.Write{SymbolName}Verbatim(_{memberInfo.MemberName}Name);");
            sb.AppendLine($"{GetPropertyFormatter(memberInfo, queue)}.Serialize(ref writer, value.{memberInfo.MemberName}, nestingLimit);");
            sb.AppendLine("writeSeparator = true;");         
        }

        private static void GenerateDeserializer(StringBuilder sb, Type current, JsonObjectDescription objectDescription, Queue<Type> queue)
        {
            sb.AppendLine($"if(reader.Read{SymbolName}IsNull())");
            sb.AppendLine("{");
            sb.AppendLine("return null;");
            sb.AppendLine("}");
            var currentFullName = current.FullName.Replace('+', '.');
            sb.AppendLine($"var result = new {currentFullName}();");
            sb.AppendLine("var count = 0;");
            sb.AppendLine($"reader.Read{SymbolName}BeginObjectOrThrow();");
            sb.AppendLine($"while (!reader.TryRead{SymbolName}IsEndObjectOrValueSeparator(ref count))");
            sb.AppendLine("{");
            sb.AppendLine($"var name = reader.Read{SymbolName}NameSpan();");
            sb.AppendLine("var length = name.Length;");
            if (typeof(TSymbol) == typeof(char))
            {
                sb.AppendLine("ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));");
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                sb.AppendLine("ref var b = ref MemoryMarshal.GetReference(name);");
            }
            GenerateIfStatements(objectDescription.Members, sb, queue, 0);
            sb.AppendLine($"reader.SkipNext{SymbolName}Segment();");
            sb.AppendLine("}");
            sb.AppendLine("return result;");
        }

        private static void GenerateIfStatements(ICollection<JsonMemberInfo> memberInfos, StringBuilder sb, Queue<Type> queue, int index)
        {
            var symbolSize = typeof(TSymbol) == typeof(char) ? 2 : 1;

            var grouping = memberInfos.Where(a => a.CanRead && a.Name.Length >= index).GroupBy(a => CalculateKey(a.Name, index))
                .OrderByDescending(a => a.Key.Key).ToList();
            if (!grouping.Any())
            {
                return;
            }

            foreach (var group in grouping)
            {
                (string readMethod, string keySuffix) = GetReadMethod(group.Key.intType);

                if (group.Count() == 1) // need to check remaining values too 
                {
                    var memberInfo = group.Single();
                    int length = index + group.Key.offset;
                    if (group.Key.Key == 0 && group.Key.offset == 0) // full direct match, no checks necessary anymore
                    {
                        sb.AppendLine(
                            $"if(length == {length}) {{result.{memberInfo.MemberName} = {GetPropertyFormatter(memberInfo, queue)}.Deserialize(ref reader);continue;}}");
                        continue;
                    }

                    var subSb = new StringBuilder();
                    subSb.Append($"{readMethod}(ref b, {index * symbolSize}) == {group.Key.Key}{keySuffix}");
                    while (length < memberInfo.Name.Length)
                    {
                        subSb.Append(" && ");
                        var subKey = CalculateKey(memberInfo.Name, length);
                        (string readSubMethod, string keySubSuffix) = GetReadMethod(subKey.intType);
                        subSb.Append($"{readSubMethod}(ref b, {length * symbolSize}) == {subKey.Key}{keySubSuffix}");
                        length += subKey.offset;
                    }

                    sb.AppendLine(
                        $"if(length == {length} && {subSb}) {{result.{memberInfo.MemberName} = {GetPropertyFormatter(memberInfo, queue)}.Deserialize(ref reader);continue;}}");
                }
                else
                {
                    var nextIndex = index + group.Key.offset;
                    sb.AppendLine(
                        $"if(length >= {nextIndex} && {readMethod}(ref b, {index * symbolSize}) == {group.Key.Key}{keySuffix})\r\n{{");
                    GenerateIfStatements(group.ToList(), sb, queue, nextIndex);
                    sb.AppendLine($"reader.SkipNext{SymbolName}Segment();");
                    sb.AppendLine("continue;\r\n}");
                }
            }
        }

        private static string GetPropertyFormatter(JsonMemberInfo memberInfo, Queue<Type> queue)
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var type = memberInfo.MemberType;
            var formatter = resolver.GetFormatter(type);
            if (formatter is ComplexFormatter)
            {
                queue.Enqueue(type);
                return $"{type.Name}{SymbolName}Formatter.Default";
            }

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var elementFormatter = resolver.GetFormatter(elementType);
                if (elementFormatter is ComplexFormatter)
                {
                    queue.Enqueue(elementType);
                }
            }
            else if (type.TryGetTypeOfGenericInterface(typeof(IList<>), out var listArgumentTypes))
            {
                var elementType = listArgumentTypes.First();
                var elementFormatter = resolver.GetFormatter(elementType);
                if (elementFormatter is ComplexFormatter)
                {
                    queue.Enqueue(elementType);
                }
            }
            var result = $"{GetFullName(formatter.GetType())}.Default";
            return result;
        }

        static string GetFullName(Type t)
        {
            if (!t.IsGenericType)
                return t.Name;
            StringBuilder sb = new StringBuilder();

            sb.Append(t.Name.Substring(0, t.Name.LastIndexOf("`")));
            sb.Append(t.GetGenericArguments().Aggregate("<",

                delegate (string aggregate, Type type)
                {
                    return aggregate + (aggregate == "<" ? "" : ",") + GetFullName(type);
                }
            ));
            sb.Append(">");

            return sb.ToString();
        }

        private static (string ReadMethod, string KeySuffix) GetReadMethod(Type intType)
        {
            string readMethod;
            string keySuffix = null;
            if (intType == typeof(ulong))
            {
                readMethod = "ReadUInt64";
                keySuffix = "UL";
            }
            else if (intType == typeof(uint))
            {
                readMethod = "ReadUInt32";
                keySuffix = "U";
            }
            else if (intType == typeof(ushort))
            {
                readMethod = "ReadUInt16";
            }
            else if (intType == typeof(byte))
            {
                readMethod = "ReadByte";
            }
            else
            {
                throw new NotSupportedException();
            }

            return (readMethod, keySuffix);
        }

        private static string GetSymbolName()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return "Utf16";
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return "Utf8";
            }
            
            throw new NotSupportedException();
        }


        private static (ulong Key, Type intType, int offset) CalculateKey(string memberName, int index)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return CalculateKeyUtf16(memberName, index);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return CalculateKeyUtf8(memberName, index); 
            }

            throw new NotSupportedException();
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf8(string memberName, int index)
        {
            int remaining = memberName.Length - index;
            if (remaining >= 8)
            {
                return (BitConverter.ToUInt64(Encoding.UTF8.GetBytes(memberName), index), typeof(ulong), 8);
            }

            if (remaining >= 4)
            {
                return (BitConverter.ToUInt32(Encoding.UTF8.GetBytes(memberName), index), typeof(uint), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt16(Encoding.UTF8.GetBytes(memberName), index), typeof(ushort), 2);
            }

            if (remaining >= 1)
            {
                return (Encoding.UTF8.GetBytes(memberName)[index], typeof(byte), 1);
            }

            return (0, typeof(uint), 0);
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf16(string memberName, int index)
        {
            int remaining = memberName.Length - index;
            if (remaining >= 4)
            {
                return (BitConverter.ToUInt64(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ulong), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt32(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(uint), 2);
            }

            if (remaining >= 1)
            {
                return (BitConverter.ToUInt16(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ushort), 1);
            }

            return (0, typeof(uint), 0);
        }
    }
}
