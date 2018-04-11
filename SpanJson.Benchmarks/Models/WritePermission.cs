using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class WritePermission : IGenericEquality<WritePermission>
    {
        [ProtoMember(1)] public int? user_id { get; set; }

        [ProtoMember(2)] public string object_type { get; set; }

        [ProtoMember(3)] public bool? can_add { get; set; }

        [ProtoMember(4)] public bool? can_edit { get; set; }

        [ProtoMember(5)] public bool? can_delete { get; set; }

        [ProtoMember(6)] public int? max_daily_actions { get; set; }

        [ProtoMember(7)] public int? min_seconds_between_actions { get; set; }

        public bool Equals(WritePermission obj)
        {
            return
                can_add.TrueEquals(obj.can_add) &&
                can_delete.TrueEquals(obj.can_delete) &&
                can_edit.TrueEquals(obj.can_edit) &&
                max_daily_actions.TrueEquals(obj.max_daily_actions) &&
                min_seconds_between_actions.TrueEquals(obj.min_seconds_between_actions) &&
                object_type.TrueEqualsString(obj.object_type) &&
                user_id.TrueEquals(obj.user_id);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                can_add.TrueEquals((bool?) obj.can_add) &&
                can_delete.TrueEquals((bool?) obj.can_delete) &&
                can_edit.TrueEquals((bool?) obj.can_edit) &&
                max_daily_actions.TrueEquals((int?) obj.max_daily_actions) &&
                min_seconds_between_actions.TrueEquals((int?) obj.min_seconds_between_actions) &&
                object_type.TrueEqualsString((string) obj.object_type) &&
                user_id.TrueEquals((int?) obj.user_id);
        }
    }
}