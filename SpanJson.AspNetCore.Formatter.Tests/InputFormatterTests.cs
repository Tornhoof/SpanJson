using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SpanJson.Resolvers;
using SpanJson.Shared;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    /// <summary>
    ///     Slightly modified from:
    ///     Copyright (c) .NET Foundation. All rights reserved.
    ///     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
    ///     https://github.com/aspnet/Mvc/blob/dev/test/Microsoft.AspNetCore.Mvc.Formatters.Json.Test/JsonInputFormatterTest.cs
    /// </summary>
    public class InputFormatterTests : TestBase
    {
        private readonly ExpressionTreeFixture _fixture = new ExpressionTreeFixture();

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task ExcludeNullsOriginalCase(Type modelType)
        {
            return TestInputFormatter<ExcludeNullsOriginalCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task IncludeNullsOriginalCase(Type modelType)
        {
            return TestInputFormatter<IncludeNullsOriginalCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task IncludeNullsCamelCase(Type modelType)
        {
            return TestInputFormatter<IncludeNullsCamelCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task ExcludeNullsCamelCase(Type modelType)
        {
            return TestInputFormatter<ExcludeNullsCamelCaseResolver<byte>>(modelType);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public Task CustomFormatter(Type modelType)
        {
            return TestInputFormatter<ExcludeNullsCamelCaseResolver<byte>>(modelType);
        }

        private async Task TestInputFormatter<TResolver>(Type modelType) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set<IHttpResponseFeature>(new TestResponseFeature());
            var model = _fixture.Create(modelType);
            var inputData = JsonSerializer.NonGeneric.Utf8.Serialize<TResolver>(model);
            using (httpContext.Request.Body = new MemoryStream(inputData))
            {
                httpContext.Request.ContentType = "application/json";

                var formatter = new SpanJsonInputFormatter<TResolver>();

                var formatterContext = CreateInputFormatterContext(modelType, httpContext);

                // Act
                var result = await formatter.ReadAsync(formatterContext).ConfigureAwait(false);

                // Assert
                Assert.False(result.HasError);
                Assert.NotNull(result.Model);
                Assert.IsType(modelType, result.Model);
                Assert.Equal(model, result.Model, GenericEqualityComparer.Default);
            }
        }


        [Theory]
        [InlineData("application/json")]
        [InlineData("text/json")]
        [InlineData("application/schema+json")]
        public void SupportedMediaTypes(string type)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set<IHttpResponseFeature>(new TestResponseFeature());
            httpContext.Request.ContentType = type;

            var formatter = new SpanJsonInputFormatter<ExcludeNullsOriginalCaseResolver<byte>>();
            var formatterContext = CreateInputFormatterContext(typeof(string), httpContext);
            Assert.True(formatter.CanRead(formatterContext));
        }
    }
}