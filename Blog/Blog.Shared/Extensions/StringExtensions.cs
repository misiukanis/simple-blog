using System.Text.RegularExpressions;

namespace Blog.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ToSeoUrl(this string url)
        {
            var encodedUrl = (url ?? "").ToLower();

            var polishChars = new Dictionary<string, string>
            {
                {"ą", "a"},
                {"ę", "e"},
                {"ó", "o"},
                {"ć", "c"},
                {"ł", "l"},
                {"ń", "n"},
                {"ś", "s"},
                {"ź", "z"},
                {"ż", "z"},
            };

            encodedUrl = Regex.Replace(encodedUrl, @"[ąęóćłńśźż]", match => polishChars[match.Value]);

            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9\- ]", "");

            encodedUrl = Regex.Replace(encodedUrl, @"[ ]", "-");

            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        public static string ReplaceNewLinesWithBr(this string text)
        {
            return text.Replace("\r\n", "<br/>")
                        .Replace("\n", "<br/>")
                        .Replace("\r", "<br/>"); 
        }

        public static string TextSubstring(this string text, int maxLength)
        {
            if (text.Length > maxLength)
            {
                var shortText = text.Substring(0, maxLength);
                var lastSpaceInShortText = shortText.LastIndexOf(" ");

                return text.Substring(0, lastSpaceInShortText) + "...";
            }

            return text;
        }

        public static string TrustedFileName(this string text)
        {
            var fileExtension = Path.GetExtension(text).ToLower();
            var newFileName = string.Concat(Path.GetRandomFileName(), fileExtension);

            return newFileName;
        }
    }
}
