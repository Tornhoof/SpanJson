using Microsoft.AspNetCore.Mvc;

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