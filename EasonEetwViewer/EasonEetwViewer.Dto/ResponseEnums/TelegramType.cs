using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseEnums;

[JsonConverter(typeof(JsonStringEnumConverter<TelegramType>))]
public enum TelegramType
{
}