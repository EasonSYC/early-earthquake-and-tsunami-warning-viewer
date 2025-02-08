namespace EasonEetwViewer.KyoshinMonitor.Abstractions;

/// <summary>
/// Represents the functionality to fetch a kmoni image.
/// </summary>
public interface IImageFetch
{
    /// <summary>
    /// Gets the GIF image as a byte array with the specified parameters.
    /// </summary>
    /// <param name="measurementType">The data type plotted on the image.</param>
    /// <param name="sensorType">The sensor type included on the image.</param>
    /// <param name="dateTime">The date and time to be fetched, in JST.</param>
    /// <returns>The byte array obtained, <see langword="null"/> if attempt is unsuccessful.</returns>
    public Task<byte[]?> GetByteArrayAsync(MeasurementType measurementType, SensorType sensorType, DateTime dateTime);
}
