using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Record.WebSocket;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;