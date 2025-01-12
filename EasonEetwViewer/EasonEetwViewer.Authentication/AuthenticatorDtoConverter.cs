using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication;
public class AuthenticatorDtoConverter : JsonConverter<AuthenticatorDto>
{
    public override AuthenticatorDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => AuthenticatorDto.FromJsonString(reader.GetString()!);
    public override void Write(Utf8JsonWriter writer, AuthenticatorDto value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToJsonString());
}
