using System.Text.RegularExpressions;

namespace iQuarc.SystemEx
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns true if the input matches the wildcard pattern
        ///     It is case sensitive
        /// </summary>
        public static bool MatchesWildcard(this string input, string wildcard)
        {
            string pattern = WildcardToRegex(wildcard);
            return Regex.IsMatch(input, pattern);
        }

        private static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).
                               Replace("\\*", ".*").
                               Replace("\\?", ".") + "$";
        }
    }
}