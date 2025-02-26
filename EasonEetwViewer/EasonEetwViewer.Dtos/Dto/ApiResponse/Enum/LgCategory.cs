using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;

/// <summary>
/// Describes the category of the long period ground motion observed.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<LgCategory>))]
public enum LgCategory
{
    /// <summary>
    /// The value <c>1</c>, representing a maximum LPGM intensity 2 or less, and all areas observing LPGM intensity 1 or above has maximum intensity 5 weak or above.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One = 1,
    /// <summary>
    /// The value <c>2</c>, representing a maximum LPGM intensity 2 or less, but there are areas which have maximum intensity 4 or less observing LPGM intensity 1 or above.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two = 2,
    /// <summary>
    /// The value <c>3</c>, representing a maximum LPGM intensity 3 or above, and all areas observing LPGM intensity 3 or above has maximum intensity 5 weak or above.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three = 3,
    /// <summary>
    /// The value <c>4</c>, representing a maximum LPGM intensity 4 or above, but there are areas which have maximum intensity 4 or less observing LPGM intensity 3 or above.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four = 4,
}