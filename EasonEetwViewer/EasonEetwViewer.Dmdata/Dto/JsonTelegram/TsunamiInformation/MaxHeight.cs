using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation;
public record MaxHeight
{
    [JsonPropertyName("height")]
    public required TsunamiHeight Height { get; init; }
    [JsonPropertyName("condition")]
    public MaxHeightCondition? Condition { get; init; }
    [JsonPropertyName("revise")]
    public Revise? Revise { get; init; }
}
