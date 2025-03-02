using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.Services.Kmoni.Abstraction;
using EasonEetwViewer.Services.Kmoni.Dtos;
using EasonEetwViewer.Services.Kmoni.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.Services.Kmoni.Extension;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add <see cref="KmoniSettingsHelper"/>.
/// </summary>
public static class KmoniSettingsHelperServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="KmoniSettingsHelper"/> to the service collection.
    /// </summary>
    /// <param name="services">The services collection for which the service is added to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that calls can be chained.</returns>
    public static IServiceCollection AddKmoniOptionsHelper(this IServiceCollection services)
        => services.AddSingleton(sp
            =>
            {
                JsonSerializerOptions serializerOptions = sp.GetRequiredService<JsonSerializerOptions>();
                KmoniSettingsHelperOptions options = sp.GetRequiredService<IOptions<KmoniSettingsHelperOptions>>().Value;
                string filePath = options.FilePath;
                ILogger<KmoniSettingsHelper> logger = sp.GetRequiredService<ILogger<KmoniSettingsHelper>>();
                KmoniSettings? settings;
                try
                {
                    settings = JsonSerializer.Deserialize<KmoniSettings>(File.ReadAllText(filePath), serializerOptions);
                    logger.IOSucceeded();
                }
                catch
                {
                    settings = options.Default;
                    logger.IOFailed(filePath);
                }

                settings ??= options.Default;
                IKmoniSettingsHelper helper = new KmoniSettingsHelper(settings, logger);
                helper.KmoniSettingsChanged += (sender, e)
                    =>
                    {
                        try
                        {
                            File.WriteAllText(
                            filePath,
                            JsonSerializer.Serialize(
                                e.KmoniSettings,
                                serializerOptions));
                        }
                        catch
                        {
                            logger.IOFailed(filePath);
                        }
                    };
                return helper;
            });
}
