using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using SpanJson.Resolvers;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    /// <summary>
    ///     Slightly modified from:
    ///     Copyright (c) .NET Foundation. All rights reserved.
    ///     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
    ///     https://github.com/aspnet/Mvc/blob/dev/test/Microsoft.AspNetCore.Mvc.Formatters.Json.Test/JsonOutputFormatterTest.cs
    /// </summary>
    public class OutputFormatterTests : TestBase
    {
        private readonly ExpressionTreeFixture _fixture = new ExpressionTreeFixture();

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task ExcludeNullsOriginalCase(Type modelType)
        {
            return TestOutputFormatter<ExcludeNullsOriginalCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task IncludeNullsOriginalCase(Type modelType)
        {
            return TestOutputFormatter<IncludeNullsOriginalCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task IncludeNullsCamelCase(Type modelType)
        {
            return TestOutputFormatter<IncludeNullsCamelCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task ExcludeNullsCamelCase(Type modelType)
        {
            return TestOutputFormatter<ExcludeNullsCamelCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task CustomFormatter(Type modelType)
        {
            return TestOutputFormatter<CustomResolver<byte>>(modelType);
        }

        private async Task TestOutputFormatter<TResolver>(Type modelType) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var model = _fixture.Create(modelType);
            var outputFormatterContext = GetOutputFormatterContext(model, modelType);
            var jsonFormatter = new SpanJsonOutputFormatter<TResolver>();

            await jsonFormatter.WriteAsync(outputFormatterContext).ConfigureAwait(false);

            using (var body = outputFormatterContext.HttpContext.Response.Body)
            {
                Assert.NotNull(body);
                body.Position = 0;
                var expectedOutput = JsonSerializer.NonGeneric.Utf8.Serialize<TResolver>(model);
                var content = new byte[body.Length];
                var length = await body.ReadAsync(content, 0, content.Length).ConfigureAwait(false);
                Assert.Equal(expectedOutput.Length, length);
                Assert.Equal(expectedOutput, content);
            }
        }

        [Theory]
        [InlineData("application/json")]
        [InlineData("text/json")]
        [InlineData("application/schema+json")]
        public void SupportedMediaTypes(string type)
        {
            var formatter = new SpanJsonOutputFormatter<ExcludeNullsOriginalCaseResolver<byte>>();
            var actionContext = GetActionContext(MediaTypeHeaderValue.Parse(type), new MemoryStream());
            var outputFormatterContext = new OutputFormatterWriteContext(
                actionContext.HttpContext,
                new TestHttpResponseStreamWriterFactory().CreateWriter,
                typeof(string),
                new object())
            {
                ContentType = new StringSegment(type),
                ContentTypeIsServerDefined = true
            };
            Assert.True(formatter.CanWriteResult(outputFormatterContext));
        }
    }
}