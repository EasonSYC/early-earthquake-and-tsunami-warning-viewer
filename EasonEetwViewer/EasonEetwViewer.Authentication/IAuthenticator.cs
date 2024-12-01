using System;

namespace EasonEetwViewer.Authentication;

public interface IAuthenticator
{
    public Task<string> GetAuthenticationHeader();
    public Task<string> GetNewAuthenticationHeader();
}
