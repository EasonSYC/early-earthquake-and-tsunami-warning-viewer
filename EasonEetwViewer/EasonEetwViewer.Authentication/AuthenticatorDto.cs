using System.Text.Json.Serialization;
using EasonEetwViewer.Authentication;

namespace EasonEetwViewer.Authentication;

[JsonConverter(typeof(AuthenticatorDtoConverter))]
public class AuthenticatorDto
{
    public IAuthenticator Authenticator = new EmptyAuthenticator();
    internal string ToJsonString() =>
        Authenticator is EmptyAuthenticator
            ? $"none://{Authenticator.ToJsonString()}"
            : Authenticator is ApiKey
                ? $"apiKey://{Authenticator.ToJsonString()}"
                : $"oAuth://{Authenticator.ToJsonString()}";
    internal static AuthenticatorDto FromJsonString(string jsonString) => jsonString.StartsWith("apiKey://")
            ? new() { Authenticator = ApiKey.FromJsonString(jsonString[9..]) }
            : jsonString.StartsWith("oAuth://") ? new() { Authenticator = OAuth.FromJsonString(jsonString[8..]) } : new();
}
