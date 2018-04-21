using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace SpanJson.Tests.JsonTestSuite
{
    /// <summary>
    /// https://github.com/nst/JSONTestSuite
    /// The json files are zipped, VS/Resharper wants to analyze the json content too much and half of them are invalid
    /// </summary>
    public class JsonTestSuite
    {
        private static  readonly string[] IgnoredErrors = new string[]
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
            "n_structure_trailing_#.json",
        };

        private readonly ITestOutputHelper _outputHelper;

        public JsonTestSuite(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Run(string name, string input, Result result, TestType type)
        {
            switch (result)
            {
                case Result.Accepted:
                {
                    Deserialize(input, type);
                    _outputHelper.WriteLine($"{name} was accepted.");
                    break;
                }
                case Result.Rejected:
                {
                    Assert.ThrowsAny<Exception>(() => Deserialize(input, type));
                    _outputHelper.WriteLine($"{name} was rejected.");
                    break;
                }
                case Result.Both:
                {
                    try
                    {
                        Deserialize(input, type);
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

        private object Deserialize(string input, TestType type)
        {
            var array = input[0] == JsonConstant.BeginArray;
            switch (type)
            {
                case TestType.String:
                {
                    if (array)
                    {
                        return JsonSerializer.Generic.Deserialize<string[]>(input);
                    }
                    else
                    {
                        return JsonSerializer.Generic.Deserialize<string>(input);
                    }
                }
                case TestType.Number:
                {
                    if (array)
                    {
                        return JsonSerializer.Generic.Deserialize<double[]>(input);
                    }
                    else
                    {
                        return JsonSerializer.Generic.Deserialize<double>(input);
                    }
                }
                case TestType.Array:
                {
                    return JsonSerializer.Generic.Deserialize<object[]>(input);
                }
                case TestType.Object:
                case TestType.Structure:
                {
                    if (array)
                    {
                        return JsonSerializer.Generic.Deserialize<object[]>(input);
                    }
                    else
                    {
                        return JsonSerializer.Generic.Deserialize<object>(input);
                    }
                }
                default:
                    throw new NotImplementedException();
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
                        if (name.StartsWith("y_"))
                        {
                            result.Add(new object[] {name, text, Result.Accepted, type});
                        }

                        if (name.StartsWith("n_"))
                        {
                            // We have a specific list of errors we currently do not parse as errors
                            if (IgnoredErrors.Contains(name))
                            {
                                result.Add(new object[] { name, text, Result.Accepted, type });
                            }
                            else
                            {
                                result.Add(new object[] {name, text, Result.Rejected, type});
                            }
                        }
                        else if (name.StartsWith("i_"))
                        {
                            result.Add(new object[] {name, text, Result.Both, type});
                        }
                    }
                }
            }

            return result;
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

        public enum TestType
        {
            String,
            Number,
            Array,
            Object,
            Structure,
        }

        public enum Result
        {
            Accepted,
            Rejected,
            Both,
        }

    }
}
