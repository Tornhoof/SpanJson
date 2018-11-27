using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicObject : DynamicObject
    {
        private readonly Dictionary<string, object> _dictionary;

        internal SpanJsonDynamicObject(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public override string ToString()
        {
            return $"{{{string.Join(",", _dictionary.Select(a => $"\"{a.Key}\":{a.Value.ToJsonValue()}"))}}}";
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (typeof(IDictionary<string, object>).IsAssignableFrom(binder.ReturnType))
            {
                result = _dictionary;
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys;
        }
    }
}