using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.Schema;
internal record EewInformationSchema : Head
{
    [JsonPropertyName("body")]
    public required Body Body { get; init; }
}
