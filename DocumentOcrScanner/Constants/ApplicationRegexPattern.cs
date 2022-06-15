using System.Text.RegularExpressions;

namespace DocumentOcrScanner.Constants
{
    public static class ApplicationRegexPattern
    {
        public static readonly Regex Formatted47 = new(@"[\d]{5}.[\d]{5}\s[\d]{5}.[\d]{6}\s[\d]{5}.[\d]{6}\s\d\s[\d]{14}", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        public static readonly Regex Simple47 = new(@"^[\d]{47}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        public static readonly Regex Formatted48 = new(@"[\d]{11}-\d [\d]{11}-\d [\d]{11}-\d [\d]{11}-\d", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        public static readonly Regex Formatted48_2 = new(@"[\d]{12} [\d]{12} [\d]{12} [\d]{12}", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        public static readonly Regex Simple48 = new(@"^[\d]{48}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
    }

}
