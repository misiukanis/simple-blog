namespace Blog.Client.TinyMce
{
    public static class TinyMceConfiguration
    {
        public static Dictionary<string, object> TinyMceConf = new Dictionary<string, object>
        {
            {"toolbar", "undo redo | bold italic | image"},
            {"width", 600},
            {"height", 600},
            {"plugins", "image"}
        };
    }
}
