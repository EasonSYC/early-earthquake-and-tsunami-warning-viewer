using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Responses;

/// <summary>
/// Represents the result of an API call on <c>socket.list</c> API.
/// Inherits from <c>ResponseModels.TokenResponse&lt;WebSocketConnectionDetails&gt;</c>.
/// </summary>
public record WebSocketList : ResponseModels.TokenResponse<WebSocketConnectionDetails>;