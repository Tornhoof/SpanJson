using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public sealed partial class ComplexClassFormatter<T, TSymbol, TResolver> : ComplexFormatter, IAsyncJsonFormatter<T, TSymbol>
        where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private static readonly SerializeAsyncDelegate<T, TSymbol> AsyncSerializer = BuildSerializeAsyncDelegate<T, TSymbol, TResolver>();

        public async ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, T value, CancellationToken cancellationToken = default)
        {
            if (value == null)
            {
                asyncWriter.WriteNull();
                return;
            }

            asyncWriter.WriteBeginObject();

            var state = -1;
            do
            {
                await AsyncSerializer(asyncWriter, ref state, value, cancellationToken).ConfigureAwait(false);

            } while (state != -1);

            asyncWriter.WriteEndObject();
        }

    }
}
