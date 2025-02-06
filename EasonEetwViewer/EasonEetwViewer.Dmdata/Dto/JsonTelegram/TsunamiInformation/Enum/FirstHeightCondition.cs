using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<FirstHeightCondition>))]
public enum FirstHeightCondition
{
    Unknown = 0,
    [JsonStringEnumMemberName("津波到達中と推測")]
    Approaching = 1,
    [JsonStringEnumMemberName("ただちに津波来襲と予測")]
    Striking = 2,
    [JsonStringEnumMemberName("第１波の到達を確認")]
    FirstWaveConfirmed = 3
}
