using EasonEetwViewer.Api.Dtos.ApiResponse.Record.WebSocket;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;