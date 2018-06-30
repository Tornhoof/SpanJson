namespace SpanJson.Shared.Models
{
    public enum EventType : byte
    {
        question_posted = 1,
        answer_posted = 2,
        comment_posted = 3,
        post_edited = 4,
        user_created = 5
    }
}