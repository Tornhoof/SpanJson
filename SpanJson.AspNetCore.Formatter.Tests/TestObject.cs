using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public class TestObject : IEquatable<TestObject>
    {
        public string Hello { get; set; }

        public string World { get; set; }

        public bool Equals(TestObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Hello, other.Hello) && string.Equals(World, other.World);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestObject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Hello != null ? Hello.GetHashCode() : 0) * 397) ^ (World != null ? World.GetHashCode() : 0);
            }
        }
    }
}
