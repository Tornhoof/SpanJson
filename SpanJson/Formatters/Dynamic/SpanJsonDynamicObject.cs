using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicObject : DynamicObject, IReadOnlyDictionary<string, object>
    {
        private readonly Dictionary<string, object> _dictionary;

        internal SpanJsonDynamicObject(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public int Count => _dictionary.Count;

        public object this[string key] => _dictionary[key];

        public IEnumerable<string> Keys => _dictionary.Keys;
        public IEnumerable<object> Values => _dictionary.Values;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public override string ToString()
        {
            return $"{{{string.Join(", ", _dictionary.Select(a => $"\"{a.Key}\": {a.Value}"))}}}";
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }


        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys;
        }
    }
}