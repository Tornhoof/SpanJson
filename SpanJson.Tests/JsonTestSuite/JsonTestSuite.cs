using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Text;
using Utf8Json;
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
        private readonly ITestOutputHelper _outputHelper;

        public JsonTestSuite(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void Run(string name, string input, Result result, TestType type)
        {
            File.AppendAllText(@"c:\temp\tests.txt", name+Environment.NewLine);
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
            bool array = input[0] == JsonConstant.BeginArray;
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
                            result.Add(new object[] { name, text, Result.Accepted, type });
                        }
                        if (name.StartsWith("n_"))
                        {
                            result.Add(new object[] { name, text, Result.Rejected, type });
                        }
                        else if (name.StartsWith("i_"))
                        {
                            result.Add(new object[] { name, text, Result.Both, type });
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
            if(name.Contains("number_"))
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
