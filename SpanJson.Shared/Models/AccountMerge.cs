using System;

namespace SpanJson.Shared.Models
{
    public class AccountMerge : IGenericEquality<AccountMerge>
    {
        public int? old_account_id { get; set; }


        public int? new_account_id { get; set; }


        public DateTime? merge_date { get; set; }

        public bool Equals(AccountMerge obj)
        {
            return
                old_account_id.TrueEquals(obj.old_account_id) &&
                new_account_id.TrueEquals(obj.new_account_id) &&
                merge_date.TrueEquals(obj.merge_date);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                old_account_id.TrueEquals((int?) obj.old_account_id) &&
                new_account_id.TrueEquals((int?) obj.new_account_id) &&
                merge_date.TrueEquals((DateTime?) obj.merge_date);
        }
    }
}