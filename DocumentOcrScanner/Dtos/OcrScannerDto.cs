namespace DocumentOcrScanner.Dtos;

public class OcrScannerDto
{
    public OcrScannerDto() { }
    public OcrScannerDto(float? meanConfidence, string text, string altoText)
    {
        MeanConfidence = meanConfidence;
        Text = text;
        AltoText = altoText;
    }

    public float? MeanConfidence { get; set; }
    public string Text { get; set; }
    public string AltoText { get; set; }
}

