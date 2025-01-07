using System.Net.Http.Headers;

namespace EasonEetwViewer.Authentication;
public class EmptyAuthenticator : IAuthenticator
{
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader() => throw new NoAuthenticationException();
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader() => throw new NoAuthenticationException();
    public string ToJsonString() => string.Empty;
    public static IAuthenticator FromJsonString(string jsonString) => new EmptyAuthenticator();
}
