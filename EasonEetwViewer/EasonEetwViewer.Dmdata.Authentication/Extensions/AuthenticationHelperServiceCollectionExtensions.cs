using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Options;
using EasonEetwViewer.Dmdata.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EasonEetwViewer.Dmdata.Authentication.Extensions;
/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add authentication wrapper.
/// </summary>
public static class AuthenticationHelperServiceCollectionExtensions
{
    /// <summary>
    /// Injects a <see cref="AuthenticationHelper"/> with setup from provided filepath.
    /// </summary>
    /// <param name="services">The instance of <see cref="IServiceCollection"/> for the service to be injected.</param>
    /// <param name="filePath">The file path to where the string is stored.</param>
    /// <returns>The <see cref="IServiceCollection"/> where the service is injected, for chained calls.</returns>
    public static IServiceCollection AddAuthenticator(this IServiceCollection services, string filePath)
        => services.AddSingleton(sp
        =>
        {
            string fileContent;
            try
            {
                fileContent = File.ReadAllText(filePath);
            }
            catch
            {
                fileContent = string.Empty;
            }

            return AuthenticationHelper.FromString(
                filePath,
                fileContent,
                sp.GetRequiredService<ILogger<AuthenticationHelper>>(),
                sp.GetRequiredService<ILogger<OAuth2Helper>>(),
                sp.GetRequiredService<ILogger<OAuth2Authenticator>>(),
                sp.GetRequiredService<IOptions<OAuth2Options>>().Value);
        });
}
