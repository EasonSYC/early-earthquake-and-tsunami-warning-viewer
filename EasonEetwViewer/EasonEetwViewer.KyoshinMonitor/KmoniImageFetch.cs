using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.KyoshinMonitor;

/// <summary>
/// Represents the functionality to fetch a kmoni image.
/// </summary>
public class KmoniImageFetch
{
    /// <summary>
    /// The base URI for the images.
    /// </summary>
    private const string _baseUri = "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/";
    /// <summary>
    /// The relative URI for the images, with data yet to be filled in.
    /// </summary>
    private const string _relativeUri = "[#1]_[#2]/[yyyyMMdd]/[yyyyMMdd][HHmmss].[#1]_[#2].gif";
    /// <summary>
    /// The hours that JST is ahead of UTC.
    /// </summary>
    private const int _jstAheadUtcHours = 9;
    /// <summary>
    /// The HTTP Client used to make GET requests.
    /// </summary>
    private readonly HttpClient _client = new()
    {
        BaseAddress = new(_baseUri)
    };
    /// <summary>
    /// Gets the GIF image as a byte array with the specified parameters.
    /// </summary>
    /// <param name="kmoniDataType">The data type plotted on the image.</param>
    /// <param name="sensorType">The sensor type included on the image.</param>
    /// <param name="utcDateTime">The date and time in UTC to be fetched.</param>
    /// <returns>The byte array obtained.</returns>
    public async Task<byte[]> GetByteArrayAsync(KmoniDataType kmoniDataType, SensorType sensorType, DateTime utcDateTime)
    {
        string kmoniDataTypeStr = kmoniDataType.ToUriString();
        string sensorTypeStr = sensorType.ToUriString();
        DateTime jstDateTime = utcDateTime.AddHours(_jstAheadUtcHours);
        string yearMonthDateStr = jstDateTime.ToString("yyyyMMdd");
        string hourMinuteSecondStr = jstDateTime.ToString("HHmmss");

        string relativeUri = _relativeUri
            .Replace("[#1]", kmoniDataTypeStr)
            .Replace("[#2]", sensorTypeStr)
            .Replace("[yyyyMMdd]", yearMonthDateStr)
            .Replace("[HHmmss]", hourMinuteSecondStr);

        using HttpRequestMessage request = new(HttpMethod.Get, relativeUri);
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();

        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

        return imageBytes;
    }
}
