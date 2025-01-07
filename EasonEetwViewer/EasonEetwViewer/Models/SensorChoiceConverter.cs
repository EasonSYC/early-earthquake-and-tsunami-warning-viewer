using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Models;
internal class SensorChoiceConverter : JsonConverter<Tuple<SensorType, string>>
{
    public override Tuple<SensorType, string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        SensorType dataType = JsonSerializer.Deserialize<SensorType>(reader.GetString()!);
        return new(dataType, dataType.ToReadableString());
    }
    public override void Write(Utf8JsonWriter writer, Tuple<SensorType, string> value, JsonSerializerOptions options) => writer.WriteStringValue(JsonSerializer.Serialize(value.Item1));
}
