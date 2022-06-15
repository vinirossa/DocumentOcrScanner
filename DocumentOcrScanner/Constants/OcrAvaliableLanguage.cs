namespace DocumentOcrScanner.Constants;

public sealed class OcrAvailableLanguage
{
    private OcrAvailableLanguage(string value) { Value = value; }

    public string Value { get; private set; }

    public static OcrAvailableLanguage English => new("eng");
    public static OcrAvailableLanguage Portuguese => new("por");
    public static OcrAvailableLanguage Spanish => new("spa");
}
