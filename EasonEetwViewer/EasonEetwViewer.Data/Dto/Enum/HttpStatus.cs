using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;

/// <summary>
/// Represents the status of the API response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<HttpStatus>))]
public enum HttpStatus
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>ok</c>, representing a success.
    /// </summary>
    [JsonStringEnumMemberName("ok")]
    Success = 1,
    /// <summary>
    /// The value <c>error</c>, representing an error.
    /// </summary>
    [JsonStringEnumMemberName("error")]
    Error = 2
}