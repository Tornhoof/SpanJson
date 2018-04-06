using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SpanJson.Benchmarks.Models;

namespace SpanJson.Tests
{
    class PropertySwitcher
    {
        public void Generate()
        {
            var parameter = Expression.Parameter(typeof(ReadOnlySpan<char>), "span");
            var props = typeof(AccessToken).GetProperties().Select(a => a.Name).ToArray();

            var cases = new List<SwitchCase>();
            StringBuilder sb = new StringBuilder();
            Recursive(props, 0, sb);
            var output = sb.ToString();
        }

        private void Recursive(ICollection<string> props, int index, StringBuilder sb)
        {
            props = props.Select(a => a.Substring(1)).ToArray();
            var minLength = props.Min(a => a.Length);
            for (int i = 0; i < minLength; i++)
            {
                var grouped = props.GroupBy(a => a[i]).ToList();
                foreach (var group in grouped)
                {
                    sb.AppendLine($"{new string('\t', index)}: {group.Key}");
                    Recursive(group.ToArray(), index + 1, sb);
                }
            }
        }
    }
}
