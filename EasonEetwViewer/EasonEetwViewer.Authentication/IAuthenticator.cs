using System.Net.Http.Headers;

namespace EasonEetwViewer.Authentication;

public interface IAuthenticator
{
    public Task<AuthenticationHeaderValue> GetAuthenticationHeader();
    public Task<AuthenticationHeaderValue> GetNewAuthenticationHeader();
}
