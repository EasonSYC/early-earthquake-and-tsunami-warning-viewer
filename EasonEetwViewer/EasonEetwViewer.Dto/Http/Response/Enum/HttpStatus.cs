using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Enums;

/// <summary>
/// Represents the status of the API response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<HttpStatus>))]
public enum HttpStatus
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