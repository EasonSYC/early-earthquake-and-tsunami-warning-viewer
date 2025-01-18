using System.Text.Json.Serialization;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services;
internal class KmoniSerialisableOptions : IKmoniDto
{
    [JsonInclude]
    [JsonPropertyName("sensorChoice")]
    public SensorType SensorChoice { get; private init; }

    [JsonInclude]
    [JsonPropertyName("dataChoice")]
    public KmoniDataType DataChoice { get; private init; }

    [JsonConstructor]
    internal KmoniSerialisableOptions(SensorType sensorChoice, KmoniDataType dataChoice)
    {
        SensorChoice = sensorChoice;
        DataChoice = dataChoice;
    }
}
