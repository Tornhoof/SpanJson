using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly AccessToken AccessTokenInput = ExpressionTreeFixture.Create<AccessToken>();

        private static readonly MobileCommunityBulletin MobileCommunityBulletinInput =
            ExpressionTreeFixture.Create<MobileCommunityBulletin>();

        private static readonly Question.ClosedDetails QuestionClosedDetailsInput =
            ExpressionTreeFixture.Create<Question.ClosedDetails>();

        //[Benchmark]
        //public string SerializeAccessTokenWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(AccessTokenInput);
        //}

        [Benchmark]
        public string SerializeQuestionClosedDetailsSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(QuestionClosedDetailsInput);
        }

        //[Benchmark]
        //public byte[] SerializeAccessTokenWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(AccessTokenInput);
        //}
    }
}
