using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SpanJson.Tests.JsonTestSuite
{
    /// <summary>
    ///     https://github.com/nst/JSONTestSuite
    ///     The json files are zipped, VS/Resharper wants to analyze the json content too much and half of them are invalid
    /// </summary>
    public class JsonTestSuite
    {
        public enum Result
        {
            Accepted,
            Rejected,
            Both
        }

        public enum TestType
        {
            String,
            Number,
            Array,
            Object,
            Structure
        }

        private static readonly string[] IgnoredErrors =
        {
            "n_array_comma_after_close.json",
            "n_array_extra_close.json",
            "n_array_just_minus.json",
            "n_multidigit_number_then_00.json",
            "n_number_+1.json",
            "n_number_-01.json",
            "n_number_-2..json",
            "n_number_.2e-3.json",
            "n_number_0.e1.json",
            "n_number_2.e+3.json",
            "n_number_2.e-3.json",
            "n_number_2.e3.json",
            "n_number_neg_int_starting_with_zero.json",
            "n_number_neg_real_without_int_part.json",
            "n_number_real_without_fractional_part.json",
            "n_number_starting_with_dot.json",
            "n_number_with_leading_zero.json",
            "n_object_repeated_null_null.json",
            "n_object_trailing_comment.json",
            "n_object_trailing_comment_open.json",
            "n_object_trailing_comment_slash_open.json",
            "n_object_trailing_comment_slash_open_incomplete.json",
            "n_object_with_trailing_garbage.json",
            "n_string_unescaped_crtl_char.json",
            "n_string_unescaped_newline.json",
            "n_string_unescaped_tab.json",
            "n_string_unicode_CapitalU.json",
            "n_string_with_trailing_garbage.json",
            "n_structure_array_trailing_garbage.json",
            "n_structure_array_with_extra_array_close.json",
            "n_structure_close_unopened_array.json",
            "n_structure_double_array.json",
            "n_structure_number_with_trailing_garbage.json",
            "n_structure_object_followed_by_closing_object.json",
            "n_structure_object_with_trailing_garbage.json",
            "n_structure_trailing_#.json"
        };

        private readonly ITestOutputHelper _outputHelper;

        public JsonTestSuite(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Run(string name, string input, Result result, TestType type, SymbolType symbolType)
        {
            switch (result)
            {
                case Result.Accepted:
                {
                    Deserialize(input, type, symbolType);
                    _outputHelper.WriteLine($"{name} was accepted.");
                    break;
                }
                case Result.Rejected:
                {
                    Assert.ThrowsAny<Exception>(() => Deserialize(input, type, symbolType));
                    _outputHelper.WriteLine($"{name} was rejected.");
                    break;
                }
                case Result.Both:
                {
                    try
                    {
                        Deserialize(input, type, symbolType);
                        _outputHelper.WriteLine($"{name} was accepted.");
                    }
                    catch
                    {
                        _outputHelper.WriteLine($"{name} was rejected.");
                    }

                    break;
                }
            }
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            var result = new List<object[]>();
            using (var zip = new ZipArchive(typeof(JsonTestSuite).Assembly.GetManifestResourceStream(typeof(JsonTestSuite), "test_parsing.zip"),
                ZipArchiveMode.Read))
            {
                foreach (var zipArchiveEntry in zip.Entries)
                {
                    using (var reader = new StreamReader(zipArchiveEntry.Open()))
                    {
                        var name = zipArchiveEntry.Name;
                        var text = reader.ReadToEnd();
                        var type = GetTestType(name);
                        foreach (var symbolType in Enum.GetValues(typeof(SymbolType)))
                        {

                            if (name.StartsWith("y_"))
                            {
                                result.Add(new object[] {name, text, Result.Accepted, type, symbolType});
                            }

                            if (name.StartsWith("n_"))
                            {
                                // We have a specific list of errors we currently do not parse as errors
                                if (IgnoredErrors.Contains(name))
                                {
                                    result.Add(new object[] {name, text, Result.Accepted, type, symbolType });
                                }
                                else
                                {
                                    result.Add(new object[] {name, text, Result.Rejected, type, symbolType });
                                }
                            }
                            else if (name.StartsWith("i_"))
                            {
                                result.Add(new object[] {name, text, Result.Both, type, symbolType });
                            }
                        }
                    }
                }
            }

            return result;
        }

        private object Deserialize(string input, TestType type, SymbolType symbolType)
        {
            var array = input[0] == JsonUtf16Constant.BeginArray;
            switch (type)
            {
                case TestType.String:
                {
                    if (array)
                    {
                        return DeserializeBySymbolType<string[]>(input, symbolType);
                    }

                    return DeserializeBySymbolType<string>(input, symbolType);
                }
                case TestType.Number:
                {
                    if (array)
                    {
                        return DeserializeBySymbolType<double[]>(input, symbolType);
                    }

                    return DeserializeBySymbolType<double>(input, symbolType);
                }
                case TestType.Array:
                {
                    return DeserializeBySymbolType<object[]>(input, symbolType);
                }
                case TestType.Object:
                case TestType.Structure:
                {
                    if (array)
                    {
                        return DeserializeBySymbolType<object[]>(input, symbolType);
                    }

                    return DeserializeBySymbolType<object>(input, symbolType);
                }
                default:
                    throw new NotImplementedException();
            }
        }

        private static T DeserializeBySymbolType<T>(string input, SymbolType symbolType)
        {
            switch (symbolType)
            {
                case SymbolType.Utf8:
                    return JsonSerializer.Generic.Utf8.Deserialize<T>(Encoding.UTF8.GetBytes(input));
                case SymbolType.Utf16:
                    return JsonSerializer.Generic.Utf16.Deserialize<T>(input);
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbolType), symbolType, null);
            }
        }

        private static TestType GetTestType(string name)
        {
            if (name.Contains("object"))
            {
                return TestType.Object;
            }

            if (name.Contains("string_"))
            {
                return TestType.String;
            }

            if (name.Contains("number_"))
            {
                return TestType.Number;
            }

            if (name.Contains("array_"))
            {
                return TestType.Array;
            }

            return TestType.Structure;
        }

        public enum SymbolType
        {
            Utf8,
            Utf16
        }
    }
}