using System.Text.Json.Serialization;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.Enum;

/// <summary>
/// Represents the status of the API response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ResponseStatus>))]
public enum ResponseStatus
{
    /// <summary>
    /// The value <c>ok</c>, representing a success.
    /// </summary>
    [JsonStringEnumMemberName("ok")]
    Success,
    /// <summary>
    /// The value <c>error</c>, representing an error.
    /// </summary>
    [JsonStringEnumMemberName("error")]
    Error
}