using System.Net.Http.Headers;

namespace EasonEetwViewer.Authentication;
public class EmptyAuthenticator : IAuthenticator
{
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader() => throw new NoAuthenticationException();
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader() => throw new NoAuthenticationException();
}
