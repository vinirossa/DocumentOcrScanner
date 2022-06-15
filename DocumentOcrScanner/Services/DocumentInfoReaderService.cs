using DocumentOcrScanner.Constants;
using DocumentOcrScanner.Data;
using DocumentOcrScanner.Helpers;
using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Services;

public class DocumentInfoReaderService : IDocumentInfoReaderService
{
    private readonly IApplicationFormInfoRepository _repository;

    public DocumentInfoReaderService(IApplicationFormInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task ReadRgDocumentInfoAsync(IFormFile file)
    {
        var fileBytesList = await HandleFileExtensionAndGetBytesListAsync(file);

        var ocrScannerDto = await OcrScannerHelper.ReadTextInImageListAsync(fileBytesList);

        if (string.IsNullOrWhiteSpace(ocrScannerDto.Text))
            throw new Exception("Não foi possível escanear o documento.");

        var model = new ApplicationFormInfo();
        var rgModel = new RgDocumentInfo();

        rgModel.WordCloud = ocrScannerDto.Text;

        rgModel.RgNumber = RgRegex.AllBrazilianStates
            .Matches(ocrScannerDto.Text.Replace("\n", string.Empty))
            .Select(x => x.Value)
            .Where(x => !string.IsNullOrWhiteSpace(x)) // Only for safety
            .Select(x => new string(x.Where(char.IsDigit).ToArray()))
            .FirstOrDefault();

        var foundNames = NameRegex.With2To9Words
            .Matches(ocrScannerDto.Text.Replace("\n", string.Empty))
            .Select(x => x.Value)
            .Where(x => !string.IsNullOrWhiteSpace(x)) // Only for safety
            .ToList();

        rgModel.Name = foundNames.FirstOrDefault();
        rgModel.MotherName = foundNames.FirstOrDefault();

        //var foundTypeableLines = new List<string>();
        //foreach (var format in _typeableLineFormats)
        //{
        //    var matches = format.Matches(ocrScannerDto.Text.Replace("\n", string.Empty)) // Ensure that the typeable line will be in the correct pattern
        //                            .Select(x => x.Value)
        //                            .Where(x => !string.IsNullOrWhiteSpace(x)) // Only for safety
        //                            .Select(x => new string(x.Where(char.IsDigit).ToArray()))
        //                            .ToList();

        //    foundTypeableLines = foundTypeableLines.Union(matches).ToList();
        //}

        //var typeableLine = foundTypeableLines.FirstOrDefault();
    }

    //private ApplicationFormInfo ExtractRgDocumentInfo

    /// <summary>
    /// Converts an file to a byte array and if it is a PDF, previously converts it to an image using <see cref="DocnetPdfToImageHelper"/>.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    private async Task<IList<byte[]>> HandleFileExtensionAndGetBytesListAsync(IFormFile file)
    {
        // When it is a PDF file, convert it to an image list
        if (Path.GetExtension(file.FileName).ToLower() == ".pdf")
            return await PdfToImageHelper.PdfToImageBytesListAsync(file, System.Drawing.Imaging.ImageFormat.Tiff);
        // When it is other file format, it is assumed to be an image
        else
        {
            using var fileStream = new MemoryStream();
            file.CopyTo(fileStream);

            return new List<byte[]> { fileStream.ToArray() };
        }
    }
}
