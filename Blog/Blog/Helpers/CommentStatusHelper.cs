using Blog.Domain.Enums;

namespace Blog.Helpers
{
    public static class CommentStatusHelper
    {
        public static string GetDescription(CommentStatus status) => status switch
        {
            CommentStatus.New => "New",
            CommentStatus.Accepted => "Accepted",
            CommentStatus.Rejected => "Rejected",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
