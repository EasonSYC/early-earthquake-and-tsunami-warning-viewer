using EasonEetwViewer.Dmdata.Dto.ApiPost;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;

namespace EasonEetwViewer.Dmdata.Caller.Interfaces;
public interface IApiCaller
{
    public Task<ContractList> GetContractListAsync();
    public Task<WebSocketList> GetWebSocketListAsync(int id = -1, ConnectionStatus connectionStatus = ConnectionStatus.Unknown, string cursorToken = "", int limit = -1);
    public Task<WebSocketStart> PostWebSocketStartAsync(WebSocketStartPost postData);
    public Task DeleteWebSocketAsync(int id);
    public Task<EarthquakeParameter> GetEarthquakeParameterAsync();
    public Task<GdEarthquakeList> GetPastEarthquakeListAsync(string hypocentreCode = "", Intensity maxInt = Intensity.Unknown, DateOnly date = new(), int limit = -1, string cursorToken = "");
    public Task<GdEarthquakeEvent> GetPathEarthquakeEventAsync(string eventId);
}
