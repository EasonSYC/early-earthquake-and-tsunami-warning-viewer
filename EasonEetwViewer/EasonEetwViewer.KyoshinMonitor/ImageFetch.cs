using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.KyoshinMonitor;

/// <summary>
/// Default implementation of <see cref="IImageFetch"/>.
/// </summary>
/// <param name="logger">The <see cref="ILogger{ImageFetch}">ILogger&lt;ImageFetch&gt;</see> to be used for logging.</param>
/// <param name="options">The options for the instance.</param>
public sealed class ImageFetch(ILogger<ImageFetch> logger, IOptions<ImageFetchOptions> options) : IImageFetch
{
    /// <summary>
    /// The <see cref="HttpClient"/> used to make GET requests.
    /// </summary>
    private readonly HttpClient _client = new()
    {
        BaseAddress = new(options.Value.BaseUri)
    };
    /// <inheritdoc/>
    public async Task<byte[]?> GetByteArrayAsync(MeasurementType measurementType, SensorType sensorType, DateTime dateTime)
    {
        string relativeUri = string.Format(options.Value.RelativeUri, measurementType.ToUriString(), sensorType.ToUriString(), dateTime);

        logger.SendingReqeust(options.Value.BaseUri, relativeUri);
        using HttpRequestMessage request = new(HttpMethod.Get, relativeUri);
        using HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            logger.RequestSuccessful();
            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return imageBytes;
        }
        else
        {
            logger.RequestUnsuccessful(options.Value.BaseUri, relativeUri);
            return null;
        }
    }
}
