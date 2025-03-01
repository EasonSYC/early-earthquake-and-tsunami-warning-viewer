using EasonEetwViewer.Dtos.ApiResponse.Record.WebSocket;
using EasonEetwViewer.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;