using System.Text.Json;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.KyoshinMonitor.Extensions;
/// <summary>
/// Provides extensions to inject services of <see cref="IImageFetch"/> and <see cref="IPointExtract"/> to the service collection.
/// </summary>
public static class KmoniServiceCollectionExtensions
{
    /// <summary>
    /// Add default implementations of <see cref="IImageFetch"/> and <see cref="IPointExtract"/> to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where the services is to be added.</param>
    /// <returns>The instance of <see cref="IServiceCollection"/> to be chained.</returns>
    public static IServiceCollection AddKmoniHelpers(this IServiceCollection services)
        => services
            .AddSingleton<IImageFetch, ImageFetch>(static provider
                =>
                {
                    KmoniHelperOptions options = provider.GetRequiredService<IOptions<KmoniHelperOptions>>().Value;
                    ILogger<ImageFetch> logger = provider.GetRequiredService<ILogger<ImageFetch>>();
                    return new(logger, options.BaseUri, options.RelativeUri);
                })
            .AddSingleton<IPointExtract, PointExtract>(static provider
                =>
                {
                    KmoniHelperOptions options = provider.GetRequiredService<IOptions<KmoniHelperOptions>>().Value;
                    return new(JsonSerializer.Deserialize<IEnumerable<ObservationPoint>>(File.ReadAllText(options.FilePath)) ?? []);
                });
}
