using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.WebSocket;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;