using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

/// <summary>
/// Represents the status of the API response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status
{
    /// <summary>
    /// The <c>ok</c> value, representing a success.
    /// </summary>
    [JsonStringEnumMemberName("ok")]
    Success,
    /// <summary>
    /// The <c>error</c> value, representing an error.
    /// </summary>
    [JsonStringEnumMemberName("error")]
    Error
}