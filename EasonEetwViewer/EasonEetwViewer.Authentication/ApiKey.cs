using System.Text;

namespace EasonEetwViewer.Authentication;

public class ApiKey : IAuthenticator
{
    private readonly string _header;
    public Task<string> GetAuthenticationHeader() => Task.FromResult(_header);
    public Task<string> GetNewAuthenticationHeader() => Task.FromResult(_header);

    public ApiKey(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Length <= 4 || apiKey.Substring(0, 4) != "AKe.")
        {
            throw new ArgumentException($"{apiKey} is not a valid API Key.", nameof(apiKey));
        }
        byte[]? plainTextBytes = Encoding.UTF8.GetBytes($"{apiKey}:");
        string val = Convert.ToBase64String(plainTextBytes);
        _header = $"Basic {val}";
    }
}