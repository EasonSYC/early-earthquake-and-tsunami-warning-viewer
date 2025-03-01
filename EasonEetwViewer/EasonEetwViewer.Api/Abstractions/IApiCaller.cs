using EasonEetwViewer.Api.Dtos.Enum.WebSocket;
using EasonEetwViewer.Api.Dtos.Response;
using EasonEetwViewer.Dtos.Enum;

namespace EasonEetwViewer.Api.Abstractions;
/// <summary>
/// Represents the interface for calling the <see href="dmdata.jp"/> API.
/// </summary>
public interface IApiCaller
{
    /// <summary>
    /// Gets the list of contracts. <c>contract.list</c> API.
    /// </summary>
    /// <returns>The contract list. <see langword="null"/> if unsuccessful.</returns>
    Task<ContractList?> GetContractListAsync();
    /// <summary>
    /// Gets the list of WebSockets. <c>socket.list</c> API.
    /// </summary>
    /// <param name="connectionStatus">The connection status of the WebSocket.</param>
    /// <param name="cursorToken">The cursor token for calling a list.</param>
    /// <param name="id">The ID of the WebSocket.</param>
    /// <param name="limit">The length limit of the list.</param>
    /// <returns>The WebSocket list. <see langword="null"/> if unsuccessful.</returns>
    Task<WebSocketList?> GetWebSocketListAsync(
        int? id = null,
        ConnectionStatus? connectionStatus = null,
        string? cursorToken = null,
        int? limit = null);
    /// <summary>
    /// Start a new WebSocket connection. <c>socket.start</c> API.
    /// </summary>
    /// <param name="postData">The data to be included in the POST rquest.</param>
    /// <returns>The details of the WebSocket. <see langword="null"/> if unsuccessful.</returns>
    Task<WebSocketStart?> PostWebSocketStartAsync(WebSocketStartPost postData);
    /// <summary>
    /// Deletes a WebSocket connection. <c>socket.close</c> API.
    /// </summary>
    /// <param name="id">The ID of the WebSocket.</param>
    /// <returns><see langword="true"/> if it was successful, <see langword="false"/> otherwise.</returns>
    Task<bool> DeleteWebSocketAsync(int id);
    /// <summary>
    /// Gets the list of earthquake observation points. <c>parameter.earthquake</c> API.
    /// </summary>
    /// <returns>The list of earthquake observation points. <see langword="null"/> if unsuccessful.</returns>
    Task<EarthquakeParameter?> GetEarthquakeParameterAsync();
    /// <summary>
    /// Get the list of path earthquakes. <c>gd.earthquake.list</c> API.
    /// </summary>
    /// <param name="hypocentreCode">The code for the hypocentre.</param>
    /// <param name="maxInt">The maximum intensity.</param>
    /// <param name="date">The date that the earthquake happened.</param>
    /// <param name="limit">The length limit of the list.</param>
    /// <param name="cursorToken">The cursor token for calling a list.</param>
    /// <returns>A list of past earthquakes. <see langword="null"/> if unsuccessful.</returns>
    Task<GdEarthquakeList?> GetPastEarthquakeListAsync(
        string? hypocentreCode = null,
        Intensity? maxInt = null,
        DateOnly? date = null,
        int? limit = null,
        string? cursorToken = null);
    /// <summary>
    /// Gets the details of a past earthquake event. <c>gd.earthquake.event</c> API.
    /// </summary>
    /// <param name="eventId">The event ID of the earthquake.</param>
    /// <returns>The details of the past earthquake event. <see langword="null"/> if unsuccessful.</returns>
    Task<GdEarthquakeEvent?> GetPathEarthquakeEventAsync(string eventId);
}
