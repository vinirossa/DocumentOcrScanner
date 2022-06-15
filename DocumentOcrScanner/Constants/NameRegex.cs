using System.Text.RegularExpressions;

namespace DocumentOcrScanner.Constants
{
    public static class NameRegex
    {
        public static readonly Regex With2To9Words = new(@"^[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ]+(\s[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ]+){1,9}$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
    }
}
