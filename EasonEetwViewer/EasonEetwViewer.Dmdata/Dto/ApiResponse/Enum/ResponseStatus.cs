using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;

/// <summary>
/// Represents the status of the API response.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ResponseStatus>))]
public enum ResponseStatus
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