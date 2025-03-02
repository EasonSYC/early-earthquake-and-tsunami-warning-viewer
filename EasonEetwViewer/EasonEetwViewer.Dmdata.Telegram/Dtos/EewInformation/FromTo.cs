using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
public record FromTo<TFrom, TTo>
    where TFrom : struct, System.Enum
    where TTo : struct, System.Enum
{
    [JsonPropertyName("from")]
    public required TFrom From { get; init; }
    [JsonPropertyName(name: "to")]
    public required TTo To { get; init; }
}
