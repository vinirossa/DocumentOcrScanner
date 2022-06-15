namespace DocumentOcrScanner.Services;

public interface IDocumentInfoReaderService
{
    Task ReadRgDocumentInfoAsync(IFormFile file);
}
