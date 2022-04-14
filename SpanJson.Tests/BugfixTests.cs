using Xunit;

namespace SpanJson.Tests
{
	public class BugfixTests
	{

		[Fact]
		public void EscapeGrowBug163()
		{
			for (var i = 0; i < 1000; i++)
			{
				var obj = new JsonObjectSmall { N = new string('"', i) };
				var result = JsonSerializer.Generic.Utf8.Serialize(obj);
			}
		}

		private class JsonObjectSmall
		{
			public string N { get; set; }
		}
	}
}
