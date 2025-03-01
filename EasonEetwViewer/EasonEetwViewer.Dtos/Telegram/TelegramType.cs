using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Telegram;
/// <summary>
/// Describes the type of a telegram.
/// </summary>

[JsonConverter(typeof(JsonStringEnumConverter<TelegramType>))]
public enum TelegramType
{
    /// <summary>
    /// The value <c>発表</c>, representing a new release.
    /// </summary>
    [JsonStringEnumMemberName("発表")]
    Release,
    /// <summary>
    /// The value <c>訂正</c>, representing a correction.
    /// </summary>
    [JsonStringEnumMemberName("訂正")]
    Correction,
    /// <summary>
    /// The value <c>遅延</c>, representing a delay.
    /// </summary>
    [JsonStringEnumMemberName("遅延")]
    Delay,
    /// <summary>
    /// The value <c>取消</c>, representing a cancellation.
    /// </summary>
    [JsonStringEnumMemberName("取消")]
    Cancel
}
