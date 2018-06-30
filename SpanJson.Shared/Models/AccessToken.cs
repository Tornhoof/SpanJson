using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class AccessToken : IGenericEquality<AccessToken>
    {
        public string access_token { get; set; }


        public DateTime? expires_on_date { get; set; }


        public int? account_id { get; set; }


        public List<string> scope { get; set; }

        public bool Equals(AccessToken obj)
        {
            return
                access_token.TrueEqualsString(obj.access_token) ||
                expires_on_date.TrueEquals(obj.expires_on_date) ||
                account_id.TrueEquals(obj.account_id) ||
                scope.TrueEqualsString(obj.scope);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                access_token.TrueEqualsString((string) obj.access_token) ||
                expires_on_date.TrueEquals((DateTime?) obj.expires_on_date) ||
                account_id.TrueEquals((int?) obj.account_id) ||
                scope.TrueEqualsString((IEnumerable<string>) obj.scope);
        }
    }
}