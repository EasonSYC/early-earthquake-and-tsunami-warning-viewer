using System.Text.Json;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Api.Services;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Api.Extensions;
/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add API Caller.
/// </summary>
public static class ApiCallerServiceCollectionExtensions
{
    /// <summary>
    /// Injects a <see cref="IApiCaller"/> with the given base URI.
    /// </summary>
    /// <param name="services">The instance of <see cref="IServiceCollection"/> for the service to be injected.</param>
    /// <param name="baseUri">The base URI for API Calls.</param>
    /// <returns>The <see cref="IServiceCollection"/> where the service is injected, for chained calls.</returns>
    public static IServiceCollection AddApiCaller(this IServiceCollection services, string baseUri)
        => services.AddSingleton<IApiCaller>(sp
            => new ApiCaller(
                    baseUri,
                    sp.GetRequiredService<ILogger<ApiCaller>>(),
                    sp.GetRequiredService<IAuthenticationHelper>(),
                    sp.GetRequiredService<JsonSerializerOptions>()));
}
