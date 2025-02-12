namespace EasonEetwViewer.Authentication.Exceptions;
/// <summary>
/// Represents errors that occurs during OAuth2 JSON Parsing.
/// </summary>
public class OAuthJsonException : OAuthException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthJsonException"/> class.
    /// </summary>
    public OAuthJsonException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthJsonException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OAuthJsonException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthJsonException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public OAuthJsonException(string message, Exception inner)
        : base(message, inner) { }
}
