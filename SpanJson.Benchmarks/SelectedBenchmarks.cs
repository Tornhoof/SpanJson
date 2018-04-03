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

        private static readonly Comment CommentInput =
            ExpressionTreeFixture.Create<Comment>();

        [Benchmark]
        public string SerializeCommentWithJilSerializer()
        {
            return JilSerializer.Serialize(CommentInput);
        }

        [Benchmark]
        public string SerializeCommentSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(CommentInput);
        }

        [Benchmark]
        public byte[] SerializeCommentWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(CommentInput);
        }
    }
}
