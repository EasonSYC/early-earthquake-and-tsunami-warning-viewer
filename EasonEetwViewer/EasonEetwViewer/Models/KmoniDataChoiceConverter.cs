using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;

namespace EasonEetwViewer.Models;
internal class KmoniDataChoiceConverter : JsonConverter<Tuple<KmoniDataType, string>>
{
    public override Tuple<KmoniDataType, string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        KmoniDataType dataType = JsonSerializer.Deserialize<KmoniDataType>(reader.GetString()!);
        return new(dataType, dataType.ToReadableString());
    }
    public override void Write(Utf8JsonWriter writer, Tuple<KmoniDataType, string> value, JsonSerializerOptions options) => writer.WriteStringValue(JsonSerializer.Serialize(value.Item1));
}
