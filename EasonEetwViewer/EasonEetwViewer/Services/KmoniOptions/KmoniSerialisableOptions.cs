using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOptions;
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
