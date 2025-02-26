using EasonEetwViewer.Dtos.Dto.ApiResponse.Record.WebSocket;
using EasonEetwViewer.Dtos.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// </summary>
public record WebSocketList : TokenBase<WebSocketDetails>;