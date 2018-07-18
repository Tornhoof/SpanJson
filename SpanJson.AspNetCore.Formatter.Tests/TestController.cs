using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SpanJson.Shared.Models;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    [Route("/api/test")]
    public class TestController
    {
        [HttpPost]
        [Route("PingPong")]
        public ActionResult<TestObject> PingPong([FromBody] TestObject to)
        {
            return to;
        }
    }
}