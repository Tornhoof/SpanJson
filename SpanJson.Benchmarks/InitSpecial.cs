using System;
using System.Collections.Generic;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks
{
    public static class InitSpecial
    {
        public static bool Init()
        {
            //ResolverBase<char, ExcludeNullsOriginalCaseResolver<char>>.RegisterFormatter(typeof(Answer),
            //    new AnswerUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>());
            //ResolverBase<char, ExcludeNullsOriginalCaseResolver<char>>.RegisterFormatter(typeof(User.BadgeCount),
            //    new BadgeCountUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>());
            //ResolverBase<char, ExcludeNullsOriginalCaseResolver<char>>.RegisterFormatter(typeof(Comment),
            //    new CommentUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>());
            //ResolverBase<char, ExcludeNullsOriginalCaseResolver<char>>.RegisterFormatter(typeof(ShallowUser),
            //    new ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>());

            //ResolverBase<byte, ExcludeNullsOriginalCaseResolver<byte>>.RegisterFormatter(typeof(Answer),
            //    new AnswerUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>());
            //ResolverBase<byte, ExcludeNullsOriginalCaseResolver<byte>>.RegisterFormatter(typeof(User.BadgeCount),
            //    new BadgeCountUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>());
            //ResolverBase<byte, ExcludeNullsOriginalCaseResolver<byte>>.RegisterFormatter(typeof(Comment),
            //    new CommentUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>());
            //ResolverBase<byte, ExcludeNullsOriginalCaseResolver<byte>>.RegisterFormatter(typeof(ShallowUser),
            //    new ShallowUserUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>());
            return true;
        }
    }
}
