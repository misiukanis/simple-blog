using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Blog.Shared.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CommentStatus
    {
        [Description("New")]
        New = 1,

        [Description("Accepted")]
        Accepted = 2,

        [Description("Rejected")]
        Rejected = 3
    }
}
