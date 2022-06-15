using Docnet.Core;
using Docnet.Core.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DocumentOcrScanner.Helpers;

public static class PdfToImageHelper
{
    public static async Task<IList<MemoryStream>> PdfToImageStreamListAsync(IFormFile file, ImageFormat format)
    {
        using var pdfStream = new MemoryStream();
        file.CopyTo(pdfStream);

        return await PdfToImageStreamListAsync(pdfStream.ToArray(), format);
    }

    public static async Task<IList<MemoryStream>> PdfToImageStreamListAsync(string filePath, ImageFormat format)
    {
        if (!File.Exists(filePath))
            throw new Exception("FileNotFound");

        try
        {
            var pdfBytes = File.ReadAllBytes(filePath);
            return await PdfToImageStreamListAsync(pdfBytes, format);
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToOpenFile", ex);
        }
    }

    public static async Task<IList<MemoryStream>> PdfToImageStreamListAsync(MemoryStream pdfStream, ImageFormat format)
    {
        return await PdfToImageStreamListAsync(pdfStream.ToArray(), format);
    }

    public static async Task<IList<MemoryStream>> PdfToImageStreamListAsync(byte[] pdfBytes, ImageFormat format)
    {
        return await Task.Run(() =>
        {
            try
            {
                using var docReader = DocLib.Instance.GetDocReader(pdfBytes, new PageDimensions(1440, 2560));

                var imageStreamList = new List<MemoryStream>();

                for (int i = 0; i < docReader.GetPageCount(); i++)
                {
                    using var pageReader = docReader.GetPageReader(i);

                    var rawBytes = pageReader.GetImage();
                    var width = pageReader.GetPageWidth();
                    var height = pageReader.GetPageHeight();

                    using var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                    using var tempBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

                    CopyBytesToBitmap(tempBmp, rawBytes);

                    // Prevents transparency
                    for (int y = 0; y < tempBmp.Height; y++)
                        tempBmp.SetPixel(tempBmp.Width - 1, y, tempBmp.GetPixel(tempBmp.Width - 2, y));

                    var g = Graphics.FromImage(bmp);
                    g.FillRegion(Brushes.White, new Region(new Rectangle(0, 0, width, height)));
                    g.DrawImage(tempBmp, new Point(0, 0));
                    g.Save();

                    using var imageStream = new MemoryStream();
                    bmp.Save(imageStream, format);

                    imageStreamList.Add(imageStream);
                }
                return imageStreamList;
            }
            catch (Exception ex)
            {
                throw new Exception("UnableToConvertFile PDF", ex);
            }
        });
    }


    public static async Task<IList<byte[]>> PdfToImageBytesListAsync(IFormFile file, ImageFormat format)
    {
        var imageBytesList = new List<byte[]>();
        var imageStreamList = await PdfToImageStreamListAsync(file, format);

        foreach (var imageStream in imageStreamList)
            imageBytesList.Add(imageStream.ToArray());

        return imageBytesList;
    }

    public static async Task<IList<byte[]>> PdfToImageBytesListAsync(string filePath, ImageFormat format)
    {
        var imageBytesList = new List<byte[]>();
        var imageStreamList = await PdfToImageStreamListAsync(filePath, format);

        foreach (var imageStream in imageStreamList)
            imageBytesList.Add(imageStream.ToArray());

        return imageBytesList;
    }

    public static async Task<IList<byte[]>> PdfToImageBytesListAsync(MemoryStream pdfStream, ImageFormat format)
    {
        var imageBytesList = new List<byte[]>();
        var imageStreamList = await PdfToImageStreamListAsync(pdfStream, format);

        foreach (var imageStream in imageStreamList)
            imageBytesList.Add(imageStream.ToArray());

        return imageBytesList;
    }

    public static async Task<IList<byte[]>> PdfToImageBytesListAsync(byte[] pdfBytes, ImageFormat format)
    {
        var imageBytesList = new List<byte[]>();
        var imageStreamList = await PdfToImageStreamListAsync(pdfBytes, format);

        foreach (var imageStream in imageStreamList)
            imageBytesList.Add(imageStream.ToArray());

        return imageBytesList;
    }


    public static async Task PdfToImageFilesAsync(IFormFile file, ImageFormat format, string outputPath)
    {
        var imagesBytes = await PdfToImageBytesListAsync(file, format);
        WriteImageBytesToDisk(imagesBytes, format, outputPath);
    }

    public static async Task PdfToImageFilesAsync(string filePath, ImageFormat format, string outputPath)
    {
        var imagesBytes = await PdfToImageBytesListAsync(filePath, format);
        WriteImageBytesToDisk(imagesBytes, format, outputPath);
    }

    public static async Task PdfToImageFilesAsync(MemoryStream pdfStream, ImageFormat format, string outputPath)
    {
        var imagesBytes = await PdfToImageBytesListAsync(pdfStream, format);
        WriteImageBytesToDisk(imagesBytes, format, outputPath);
    }

    public static async Task PdfToImageFilesAsync(byte[] pdfBytes, ImageFormat format, string outputPath)
    {
        var imagesBytes = await PdfToImageBytesListAsync(pdfBytes, format);
        WriteImageBytesToDisk(imagesBytes, format, outputPath);
    }


    /// <summary>
    /// Write image byte array to disk using <see cref="File.WriteAllBytes"/>.
    /// </summary>
    /// <param name="imagesBytes"></param>
    /// <param name="format"></param>
    /// <param name="outputPath"></param>
    private static void WriteImageBytesToDisk(IList<byte[]> imagesBytes, ImageFormat format, string outputPath)
    {
        try
        {
            foreach (var (imageByte, i) in imagesBytes.Select((x, i) => (x, i)))
                File.WriteAllBytes(
                    GenerateImageFilename(outputPath, i.ToString(), format),
                    imageByte
                );
        }
        catch (Exception ex)
        {
            throw new Exception("UnableToSaveFile", ex);
        }
    }

    /// <summary>
    /// Generates the image filename based on its format and allowing to add a suffix, like an index, by example.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    private static string GenerateImageFilename(string path, string suffix, ImageFormat format)
    {
        return Path.ChangeExtension(path, null) + suffix + "." + format.ToString().ToLower();
    }

    /// <summary>
    /// Copy byte array to <see cref="Bitmap"/> data using <see cref="Marshal.Copy"/>.
    /// </summary>
    /// <param name="bmp"></param>
    /// <param name="rawBytes"></param>
    private static void CopyBytesToBitmap(Bitmap bmp, byte[] rawBytes)
    {
        var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
        var pNative = bmpData.Scan0;

        Marshal.Copy(rawBytes, 0, pNative, rawBytes.Length);
        bmp.UnlockBits(bmpData);
    }
}

