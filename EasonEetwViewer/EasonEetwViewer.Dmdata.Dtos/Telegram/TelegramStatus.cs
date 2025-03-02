using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.Telegram;

/// <summary>
/// Describes the status of a telegram.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<TelegramStatus>))]
public enum TelegramStatus
{
    /// <summary>
    /// The value <c>通常</c>, a normal telegram.
    /// </summary>
    [JsonStringEnumMemberName("通常")]
    Normal,
    /// <summary>
    /// The value <c>訓練</c>, a practise telegram.
    /// </summary>
    [JsonStringEnumMemberName("訓練")]
    Practise,
    /// <summary>
    /// The value <c>試験</c>, a test telegram.
    /// </summary>
    [JsonStringEnumMemberName("試験")]
    Test
}
