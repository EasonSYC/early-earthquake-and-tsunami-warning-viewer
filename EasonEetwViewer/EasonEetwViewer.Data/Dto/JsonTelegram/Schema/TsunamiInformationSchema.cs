using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TsunamiInformation;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.Schema;
public record TsunamiInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
