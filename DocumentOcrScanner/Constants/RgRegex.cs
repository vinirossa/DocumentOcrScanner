using System.Text.RegularExpressions;

namespace DocumentOcrScanner.Constants
{
    public static class RgRegex
    {
        public static readonly Regex AllBrazilianStates = new(@"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
    }
}
