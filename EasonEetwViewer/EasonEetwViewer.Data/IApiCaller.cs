using EasonEetwViewer.HttpRequest.Dto.ApiPost;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.HttpRequest;
public interface IApiCaller
{
    public Task<ContractList> GetContractListAsync();
    public Task<WebSocketList> GetWebSocketListAsync(int id = -1, WebSocketConnectionStatus connectionStatus = WebSocketConnectionStatus.Unknown, string cursorToken = "", int limit = -1);
    public Task<WebSocketStart> PostWebSocketStartAsync(WebSocketStartPost postData);
    public Task DeleteWebSocketAsync(int id);
    public Task<EarthquakeParameter> GetEarthquakeParameterAsync();
    public Task<GdEarthquakeList> GetPastEarthquakeListAsync(string hypocentreCode = "", EarthquakeIntensity maxInt = EarthquakeIntensity.Unknown, DateOnly date = new(), int limit = -1, string cursorToken = "");
    public Task<GdEarthquakeEvent> GetPathEarthquakeEventAsync(string eventId);
}
