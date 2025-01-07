using System.Text.Json.Serialization;
using EasonEetwViewer.Authentication;

namespace EasonEetwViewer.Models;

[JsonConverter(typeof(AuthenticatorDtoConverter))]
internal record AuthenticatorDto
{
    internal IAuthenticator Authenticator { get; set; } = new EmptyAuthenticator();
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
