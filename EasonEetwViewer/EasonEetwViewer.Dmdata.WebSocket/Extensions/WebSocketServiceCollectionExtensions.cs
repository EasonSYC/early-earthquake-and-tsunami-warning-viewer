using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Services;
using EasonEetwViewer.Dmdata.WebSocket.Abstractions;
using EasonEetwViewer.Dmdata.WebSocket.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Dmdata.WebSocket.Extensions;
/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to add WebSocket.
/// </summary>
public static class WebSocketServiceCollectionExtensions
{
    /// <summary>
    /// Injects a <see cref="IWebSocketClient"/> with the given base URI.
    /// </summary>
    /// <param name="services">The instance of <see cref="IServiceCollection"/> for the service to be injected.</param>
    /// <returns>The <see cref="IServiceCollection"/> where the service is injected, for chained calls.</returns>
    public static IServiceCollection AddWebSocket(this IServiceCollection services)
        => services
            .AddSingleton<ITelegramParser, TelegramParser>()
            .AddSingleton<IWebSocketClient, WebSocketClient>();
}
