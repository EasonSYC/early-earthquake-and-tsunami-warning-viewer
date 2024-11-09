using EasonEetwViewer.Dto.Http.Response.Models;
using EasonEetwViewer.Dto.Http.Response.Records;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// Inherits from <c>ResponseModels.TokenResponse&lt;WebSocketConnectionDetails&gt;</c>.
/// </summary>
public record WebSocketList : TokenResponse<WebSocketConnectionDetails>;