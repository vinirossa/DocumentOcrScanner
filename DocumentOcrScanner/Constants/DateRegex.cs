using System.Text.RegularExpressions;

namespace DocumentOcrScanner.Constants
{
    public static class DateRegex
    {
        public static readonly Regex BrazilDefault = new(@"\d\d[/]\d\d[/]\d\d\d\d", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        public static readonly Regex BrazilAbbreviated = new(@"\d\d[/]\d\d[/]\d\d", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
    }
}
