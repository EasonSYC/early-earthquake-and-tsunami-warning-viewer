using EasonEetwViewer.HttpRequest.Dto.ApiPost;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.Responses;

namespace EasonEetwViewer.HttpRequest;
public interface IApiCaller
{
    public Task<ContractListResponse> GetContractListAsync();
    public Task<WebSocketListResponse> GetWebSocketListAsync(int id = -1, WebSocketConnectionStatus connectionStatus = WebSocketConnectionStatus.Unknown, string cursorToken = "", int limit = -1);
    public Task<WebSocketStartResponse> PostWebSocketStartAsync(WebSocketStartPost postData);
    public Task DeleteWebSocketAsync(int id);
    public Task<EarthquakeParameterResponse> GetEarthquakeParameterAsync();
    public Task<PastEarthquakeListResponse> GetPastEarthquakeListAsync(string hypocentreCode = "", EarthquakeIntensity maxInt = EarthquakeIntensity.Unknown, DateOnly date = new(), int limit = -1, string cursorToken = "");
    public Task<PastEarthquakeEventResponse> GetPathEarthquakeEventAsync(string eventId);
}
