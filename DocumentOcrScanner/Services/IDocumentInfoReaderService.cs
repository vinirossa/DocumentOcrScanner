using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Services;

public interface IDocumentInfoReaderService
{
    Task ReadRgDocumentInfoAsync(IFormFile file);

    Task TesteAsync(ApplicationFormInfo model);
}
