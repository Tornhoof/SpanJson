using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicObject : DynamicObject, IDictionary<string, object>
    {
        private readonly Dictionary<string, object> _dictionary;

        internal SpanJsonDynamicObject(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            (_dictionary as IDictionary<string, object>).CopyTo(array, arrayIndex);
        }


        public int Count => _dictionary.Count;
        public bool IsReadOnly { get; } = true;

        public object this[string key]
        {
            get => _dictionary[key];
            set => throw new System.NotImplementedException();
        }

        public ICollection<string> Keys => _dictionary.Keys;
        public ICollection<object> Values => _dictionary.Values;

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

        public void Add(string key, object value)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new System.NotImplementedException();
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