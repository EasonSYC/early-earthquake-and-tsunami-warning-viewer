using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using EasonEetwViewer.KyoshinMonitor.Interfaces;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.KyoshinMonitor.Services;

/// <summary>
/// Default implementation of <see cref="IImageFetch"/>.
/// </summary>
internal sealed class ImageFetch : IImageFetch
{
    /// <summary>
    /// The <see cref="HttpClient"/> used to make GET requests.
    /// </summary>
    private readonly HttpClient _client;
    /// <summary>
    /// The logger to be used for logging.
    /// </summary>
    private readonly ILogger<ImageFetch> _logger;
    /// <summary>
    /// The base URI for the images.
    /// </summary>
    private readonly string _baseUri;
    /// <summary>
    /// The relative URI for the images, with data yet to be filled in.
    /// </summary>
    private readonly string _relativeUri;

    /// <summary>
    /// Creates an instance of the class with the parameters specified.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{ImageFetch}">ILogger&lt;ImageFetch&gt;</see> to be used for logging.</param>
    /// <param name="baseUri">The base URI for the images.</param>
    /// <param name="relativeUri">The relative URI for the images, with data yet to be filled in.</param>
    public ImageFetch(ILogger<ImageFetch> logger, string baseUri, string relativeUri)
    {
        _logger = logger;
        _client = new()
        {
            BaseAddress = new(baseUri)
        };
        _baseUri = baseUri;
        _relativeUri = relativeUri;
        _logger.Instantiated();
    }

    /// <inheritdoc/>
    public async Task<byte[]?> GetByteArrayAsync(MeasurementType measurementType, SensorType sensorType, DateTime dateTime)
    {
        string relativeUri = string.Format(_relativeUri, measurementType.ToUriString(), sensorType.ToUriString(), dateTime);

        _logger.SendingReqeust(_baseUri, relativeUri);
        using HttpRequestMessage request = new(HttpMethod.Get, relativeUri);
        using HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            _logger.RequestSuccessful();
            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
            return imageBytes;
        }
        else
        {
            _logger.RequestUnsuccessful(_baseUri, relativeUri);
            return null;
        }
    }
}
