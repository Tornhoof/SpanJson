using System;
using System.Collections.Generic;
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
        public void Run(string name, string input, Result result)
        {
            switch (result)
            {
                case Result.Accepted:
                {
                    JsonSerializer.Generic.Deserialize<object>(input);
                    _outputHelper.WriteLine($"{name} was accepted.");
                    break;
                }
                case Result.Rejected:
                {
                    Assert.Throws<JsonParserException>(() => JsonSerializer.Generic.Deserialize<object>(input));
                    _outputHelper.WriteLine($"{name} was rejected.");
                    break;
                }
                case Result.Both:
                {
                    try
                    {
                        JsonSerializer.Generic.Deserialize<object>(input);
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
                        if (name == "n_structure_100000_opening_arrays.json") // not yet supproted
                        {
                            continue;
                        }

                        var text = reader.ReadToEnd();
                        if (name.StartsWith("y_"))
                        {
                            result.Add(new object[] {name, text, Result.Accepted});
                        }
                        else if (name.StartsWith("n_"))
                        {
                            result.Add(new object[] { name, text, Result.Rejected });
                        }
                        else if (name.StartsWith("i_"))
                        {
                            result.Add(new object[] { name, text, Result.Both});
                        }
                    }
                }
            }

            return result;
        }

        [Flags]
        public enum Result
        {
            Accepted,
            Rejected,
            Both,
        }
    }
}
