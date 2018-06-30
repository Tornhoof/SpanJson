using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;
using SpanJson.Shared.Models;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    /// <summary>
    ///     Slightly modified from:
    ///     Copyright (c) .NET Foundation. All rights reserved.
    ///     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
    ///     https://github.com/aspnet/Mvc/blob/dev/test/Microsoft.AspNetCore.Mvc.Formatters.Json.Test
    /// </summary>
    public abstract class TestBase
    {
        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface &&
                            !t.IsAbstract)
                .ToList();
            return models.Where(a => a != null).Select(a => new object[] {a});
        }

        protected InputFormatterContext CreateInputFormatterContext(
            Type modelType,
            HttpContext httpContext,
            string modelName = null,
            bool treatEmptyInputAsDefaultValue = false)
        {
            var provider = new EmptyModelMetadataProvider();
            var metadata = provider.GetMetadataForType(modelType);

            return new InputFormatterContext(
                httpContext,
                modelName ?? string.Empty,
                new ModelStateDictionary(),
                metadata,
                new TestHttpRequestStreamReaderFactory().CreateReader,
                treatEmptyInputAsDefaultValue);
        }

        protected OutputFormatterWriteContext GetOutputFormatterContext(
            object outputValue,
            Type outputType,
            string contentType = "application/json; charset=utf-8",
            MemoryStream responseStream = null)
        {
            var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);

            var actionContext = GetActionContext(mediaTypeHeaderValue, responseStream);
            return new OutputFormatterWriteContext(
                actionContext.HttpContext,
                new TestHttpResponseStreamWriterFactory().CreateWriter,
                outputType,
                outputValue)
            {
                ContentType = new StringSegment(contentType)
            };
        }

        protected ActionContext GetActionContext(
            MediaTypeHeaderValue contentType,
            MemoryStream responseStream = null)
        {
            var request = new Mock<HttpRequest>();
            var headers = new HeaderDictionary();
            request.Setup(r => r.ContentType).Returns(contentType.ToString());
            request.SetupGet(r => r.Headers).Returns(headers);
            headers[HeaderNames.AcceptCharset] = contentType.Charset.ToString();
            var response = new Mock<HttpResponse>();
            response.SetupGet(f => f.Body).Returns(responseStream ?? new MemoryStream());
            var httpContext = new Mock<HttpContext>();
            httpContext.SetupGet(c => c.Request).Returns(request.Object);
            httpContext.SetupGet(c => c.Response).Returns(response.Object);
            return new ActionContext(httpContext.Object, new RouteData(), new ActionDescriptor());
        }

        protected class TestResponseFeature : HttpResponseFeature
        {
            public override void OnCompleted(Func<object, Task> callback, object state)
            {
                // do not do anything
            }
        }

        public class TestHttpRequestStreamReaderFactory : IHttpRequestStreamReaderFactory
        {
            public TextReader CreateReader(Stream stream, Encoding encoding)
            {
                return new HttpRequestStreamReader(stream, encoding);
            }
        }

        public class TestHttpResponseStreamWriterFactory : IHttpResponseStreamWriterFactory
        {
            public TextWriter CreateWriter(Stream stream, Encoding encoding)
            {
                return new HttpResponseStreamWriter(stream, encoding);
            }
        }
    }
}