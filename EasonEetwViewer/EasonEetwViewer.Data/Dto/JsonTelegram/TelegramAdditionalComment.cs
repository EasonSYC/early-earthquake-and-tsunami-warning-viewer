using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record TelegramAdditionalComment
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }
    [JsonPropertyName("codes")]
    public required List<string> Codes { get; init; }
}
