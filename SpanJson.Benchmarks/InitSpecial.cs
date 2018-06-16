using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks
{
    public static class InitSpecial
    {
        public static bool Init()
        {
            var types = Assembly.GetAssembly(typeof(MobileBadgeAward)).GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.Namespace.StartsWith("SpanJson.Benchmarks.Generated")).ToArray();
            foreach (var type in types)
            {
                if (type.TryGetTypeOfGenericInterface(typeof(IJsonFormatter<,,>), out var argumentTypes) && argumentTypes.Length == 3)
                {
                    var mi = typeof(ResolverBase<,>).MakeGenericType(argumentTypes[1], argumentTypes[2])
                        .GetMethod("RegisterFormatter", BindingFlags.Static | BindingFlags.Public);
                    mi.Invoke(null, new []{argumentTypes[0], Activator.CreateInstance(type)});
                }
            }

            return true;
        }
    }
}
