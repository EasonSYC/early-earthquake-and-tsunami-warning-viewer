using System.Net.Http.Headers;

namespace EasonEetwViewer.Dmdata.Authentication.Services;
/// <summary>
/// Provides shared methods where OAuth 2.0 requires.
/// </summary>
internal static class OAuth2SharedMethod
{
    /// <summary>
    /// Generates an HTTP POST Request using the specified URI and parameters, and with media type <c>application/x-www-form-urlencoded</c>.
    /// </summary>
    /// <param name="requestUri">The URI of the POST Request.</param>
    /// <param name="requestParams">The Parameters of the POST Request.-1</param>
    /// <param name="host">The host for the request.</param>
    /// <returns>The generated <see cref="HttpRequestMessage"/>.</returns>
    public static HttpRequestMessage GeneratePostRequest(string requestUri, Dictionary<string, string> requestParams, string host)
    {
        FormUrlEncodedContent content = new(requestParams);
        HttpRequestMessage request = new(HttpMethod.Post, requestUri)
        {
            Content = content
        };
        request.Headers.Host = host;
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        return request;
    }
}
