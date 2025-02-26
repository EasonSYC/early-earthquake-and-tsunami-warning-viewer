using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<FirstHeightCondition>))]
public enum FirstHeightCondition
{
    [JsonStringEnumMemberName("津波到達中と推測")]
    Approaching,
    [JsonStringEnumMemberName("ただちに津波来襲と予測")]
    Striking,
    [JsonStringEnumMemberName("第１波の到達を確認")]
    FirstWaveConfirmed
}
