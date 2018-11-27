using System.Collections.Generic;

namespace SpanJson.Formatters.Dynamic
{
    public interface ISpanJsonDynamicArray : ISpanJsonDynamic, IEnumerable<object>
    {       
    }
}