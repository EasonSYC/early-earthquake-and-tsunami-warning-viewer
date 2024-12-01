using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace EasonEetwViewer.Authentication;

public class ApiKey : IAuthenticator
{
    private readonly string _header;
    public Task<string> GetAuthenticationHeader() => Task.FromResult(_header);
    public Task<string> GetNewAuthenticationHeader() => Task.FromResult(_header);

    public ApiKey(string apiKey)
    {
        byte[]? plainTextBytes = Encoding.UTF8.GetBytes($"{apiKey}:");
        string val = Convert.ToBase64String(plainTextBytes);
        _header = $"Basic {val}";
    }
}
