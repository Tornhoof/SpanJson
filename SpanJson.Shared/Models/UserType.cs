namespace SpanJson.Shared.Models
{
    public enum UserType : byte
    {
        unregistered = 2,
        registered = 3,
        moderator = 4,
        does_not_exist = 255
    }
}