using DocumentOcrScanner.Data;

namespace DocumentOcrScanner.Services;

public class DocumentInfoReaderService : IDocumentInfoReaderService
{
    private readonly IApplicationFormInfoRepository _repository;
    public DocumentInfoReaderService(IApplicationFormInfoRepository repository)
    {
        _repository = repository;
    }

    public Task ReadRgDocumentInfoAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
