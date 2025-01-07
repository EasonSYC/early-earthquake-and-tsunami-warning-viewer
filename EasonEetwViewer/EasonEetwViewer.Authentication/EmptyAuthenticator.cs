using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication;
public class EmptyAuthenticator : IAuthenticator
{
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader() => throw new NoAuthenticationException();
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader() => throw new NoAuthenticationException();
    public string ToJsonString() => string.Empty;
    public static IAuthenticator FromJsonString(string jsonString) => new EmptyAuthenticator();
}
