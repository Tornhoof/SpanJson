using Microsoft.AspNetCore.Mvc;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;

namespace SpanJson.WebBenchmark.Controllers
{
    [Route("api/Benchmark")]
    public class BenchmarkController : Controller
    {
        private static readonly Answer Answer = BuildAnswer();

        private static Answer BuildAnswer()
        {
            var fixture = new ExpressionTreeFixture();
            return fixture.Create<Answer>();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Answer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Answer answer)
        {
            return NoContent();
        }
    }
}