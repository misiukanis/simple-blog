namespace Blog.Common.Extensions
{
    public static class StringExtensions
    {
        public static string nl2br(this string text)
        {
            return text.Replace("&#xA;", "<br/>");
        }

        public static string TextSubstring(this string text, int maxLength)
        {
            if (text.Length > maxLength)
            {
                string shortText = text.Substring(0, maxLength);
                int lastSpaceInShortText = shortText.LastIndexOf(" ");

                return text.Substring(0, lastSpaceInShortText) + "...";
            }

            return text;
        }
    }
}
