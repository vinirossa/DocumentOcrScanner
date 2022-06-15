using DocumentOcrScanner.Constants;
using DocumentOcrScanner.Dtos;
using System.Drawing;
using System.Reflection;
using Tesseract;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace DocumentOcrScanner.Helpers;

public static class OcrScannerHelper
{
    private static readonly string _tessDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "OcrTrainedData");
    private static readonly OcrAvailableLanguage _defaultLanguage = OcrAvailableLanguage.Portuguese;
    private static readonly EngineMode _defaultEngineMode = EngineMode.Default;

    public static async Task<OcrScannerDto> ReadTextInImageAsync(IFormFile file)
    {
        return await ReadTextInImageAsync(file, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(IFormFile file, OcrAvailableLanguage language)
    {
        using var imageStream = new MemoryStream();
        file.CopyTo(imageStream);

        return await ReadTextInImageAsync(imageStream.ToArray(), language);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(string filePath)
    {
        return await ReadTextInImageAsync(filePath, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(string filePath, OcrAvailableLanguage language)
    {
        if (!File.Exists(filePath))
            throw new Exception("FileNotFound");

        try
        {
            var imageBytes = File.ReadAllBytes(filePath);
            return await ReadTextInImageAsync(imageBytes, language);
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToOpenFile", ex);
        }
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(MemoryStream imageStream)
    {
        return await ReadTextInImageAsync(imageStream, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(MemoryStream imageStream, OcrAvailableLanguage language)
    {
        return await ReadTextInImageAsync(imageStream.ToArray(), language);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(byte[] imageBytes)
    {
        return await ReadTextInImageAsync(imageBytes, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageAsync(byte[] imageBytes, OcrAvailableLanguage language)
    {
        return await Task.Run(() =>
        {
            try
            {
                using var engine = new TesseractEngine(_tessDataPath, language.Value, _defaultEngineMode);
                using var image = Pix.LoadFromMemory(imageBytes);
                using var page = engine.Process(image);

                var meanConfidence = page.GetMeanConfidence();
                var readText = page.GetText();
                var readAltoText = page.GetAltoText(0);

                return new OcrScannerDto(
                    meanConfidence,
                    readText,
                    readAltoText
                );
            }
            catch (IOException ex)
            {
                throw new Exception("InvalidFileType", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("UnableToScanImageWithOcr", ex);
            }
        });
    }


    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<IFormFile> fileList)
    {
        return await ReadTextInImageListAsync(fileList, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<IFormFile> fileList, OcrAvailableLanguage language)
    {
        var imageBytesList = new List<byte[]>();

        foreach (var file in fileList)
        {
            using var imageStream = new MemoryStream();
            file.CopyTo(imageStream);

            imageBytesList.Add(imageStream.ToArray());
        }

        return await ReadTextInImageListAsync(imageBytesList, language);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<string> filePathList)
    {
        return await ReadTextInImageListAsync(filePathList, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<string> filePathList, OcrAvailableLanguage language)
    {
        try
        {
            var imageBytesList = new List<byte[]>();
            foreach (var filePath in filePathList)
            {
                if (!File.Exists(filePath))
                    throw new Exception("FileNotFound");

                imageBytesList.Add(File.ReadAllBytes(filePath));
            }

            return await ReadTextInImageListAsync(imageBytesList, language);
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToOpenFile", ex);
        }
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<MemoryStream> imageStreamList)
    {
        return await ReadTextInImageListAsync(imageStreamList, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<MemoryStream> imageStreamList, OcrAvailableLanguage language)
    {
        var imageBytesList = new List<byte[]>();

        foreach (var imageStream in imageStreamList)
            imageBytesList.Add(imageStream.ToArray());

        return await ReadTextInImageListAsync(imageBytesList, language);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<byte[]> imageBytesList)
    {
        return await ReadTextInImageListAsync(imageBytesList, _defaultLanguage);
    }

    public static async Task<OcrScannerDto> ReadTextInImageListAsync(IList<byte[]> imageBytesList, OcrAvailableLanguage language)
    {
        var fullOcrScannerDto = new OcrScannerDto();
        var meanConfidenceList = new List<float?>();

        foreach (var imageByte in imageBytesList)
        {
            var OcrScannerDto = await ReadTextInImageAsync(imageByte, language);

            meanConfidenceList.Add(OcrScannerDto.MeanConfidence);
            fullOcrScannerDto.Text += OcrScannerDto.Text;
            fullOcrScannerDto.AltoText += OcrScannerDto.AltoText;
        }

        fullOcrScannerDto.MeanConfidence = meanConfidenceList.Average();

        return fullOcrScannerDto;
    }


    public static async Task<byte[]> HighlightBlocksInImageAsync(IFormFile file, PageIteratorLevel level, ImageFormat format)
    {
        return await HighlightBlocksInImageAsync(file, level, format, new Pen(Color.Aqua, 3));
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(IFormFile file, PageIteratorLevel level, ImageFormat format, Pen pen)
    {
        using var imageStream = new MemoryStream();
        file.CopyTo(imageStream);

        return await HighlightBlocksInImageAsync(imageStream.ToArray(), level, format, pen);
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(string filePath, PageIteratorLevel level, ImageFormat format)
    {
        return await HighlightBlocksInImageAsync(filePath, level, format, new Pen(Color.Aqua, 3));
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(string filePath, PageIteratorLevel level, ImageFormat format, Pen pen)
    {
        if (!File.Exists(filePath))
            throw new Exception("FileNotFound");

        try
        {
            var imageBytes = File.ReadAllBytes(filePath);
            return await HighlightBlocksInImageAsync(imageBytes, level, format, pen);
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToOpenFile", ex);
        }
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(MemoryStream imageStream, PageIteratorLevel level, ImageFormat format)
    {
        return await HighlightBlocksInImageAsync(imageStream, level, format, new Pen(Color.Aqua, 3));
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(MemoryStream imageStream, PageIteratorLevel level, ImageFormat format, Pen pen)
    {
        return await HighlightBlocksInImageAsync(imageStream.ToArray(), level, format, pen);
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(byte[] imageBytes, PageIteratorLevel level, ImageFormat format)
    {
        return await HighlightBlocksInImageAsync(imageBytes, level, format, new Pen(Color.Aqua, 3));
    }

    public static async Task<byte[]> HighlightBlocksInImageAsync(byte[] imageBytes, PageIteratorLevel level, ImageFormat format, Pen pen)
    {
        return await Task.Run(() =>
        {
            try
            {
                using var engine = new TesseractEngine(_tessDataPath, _defaultLanguage.Value, _defaultEngineMode);
                using var image = Pix.LoadFromMemory(imageBytes);
                using var page = engine.Process(image);
                var rectList = page.GetSegmentedRegions(PageIteratorLevel.Block);

                using var drawableImage = Image.FromStream(new MemoryStream(imageBytes));

                using var graphics = Graphics.FromImage(drawableImage);
                foreach (var rect in rectList)
                    graphics.DrawRectangle(new Pen(Color.Aqua, 3), rect);

                using var newImageStream = new MemoryStream();
                drawableImage.Save(newImageStream, format);

                return newImageStream.ToArray();
            }
            catch (IOException ex)
            {
                throw new Exception("InvalidFileType", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("UnableToScanImageWithOcr", ex);
            }
        });
    }
}

