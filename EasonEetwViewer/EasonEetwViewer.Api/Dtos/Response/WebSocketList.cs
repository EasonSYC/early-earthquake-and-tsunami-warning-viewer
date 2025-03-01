using EasonEetwViewer.Api.Dtos.Record.WebSocket;
using EasonEetwViewer.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;