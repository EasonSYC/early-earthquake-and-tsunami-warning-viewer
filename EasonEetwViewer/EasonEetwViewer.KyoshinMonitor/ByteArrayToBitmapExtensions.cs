using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace EasonEetwViewer.KyoshinMonitor;
/// <summary>
/// Provides extension method to bridge between <see cref="IImageFetch"/> and <see cref="IPointExtract"/>.
/// </summary>
public static class ByteArrayToBitmapExtensions
{
    /// <summary>
    /// Converts a <see cref="byte"/> array to <see cref="SKBitmap"/>.
    /// </summary>
    /// <param name="bytes">The byte array to be converted.</param>
    /// <returns>The converted bitmap.</returns>
    public static SKBitmap ToBitmap(this byte[] bytes)
        => SKBitmap.FromImage(SKImage.FromEncodedData(bytes));
}
