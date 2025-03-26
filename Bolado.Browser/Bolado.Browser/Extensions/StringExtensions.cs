using System.Text.RegularExpressions;

namespace Bolado.Browser.Extensions
{
    public static partial class StringExtensions
    {
        [GeneratedRegex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex UrlRegex();

        /// <summary>
        /// Checks if the string is a valid URL.
        /// </summary>
        /// <param name="url">string URL to validate</param>
        /// <returns>Returns true if string is a valid URL, otherwise returns false.</returns>
        public static bool IsValidUrl(this string? url)
        {
            if (url is null)
            {
                return false;
            }

            return UrlRegex().IsMatch(url);
        }

        /// <summary>
        /// Ensures string url starts with http:// or https://
        /// </summary>
        /// <param name="url">String URL to be formatted</param>
        /// <returns>Formatted URL that starts with http:// or https://.</returns>
        public static string FormatUrl(this string url)
        {
            if (url.StartsWith(@"http://") || url.StartsWith(@"https://"))
            {
                return url;
            }

            return $"https://{url}";
        }
    }
}
