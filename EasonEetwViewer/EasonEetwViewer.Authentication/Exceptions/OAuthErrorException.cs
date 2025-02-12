namespace EasonEetwViewer.Authentication.Exceptions;
/// <summary>
/// Represents errors thrown by online authenticators that occurs during OAuth2 key change.
/// </summary>
public class OAuthErrorException : OAuthException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthErrorException"/> class.
    /// </summary>
    public OAuthErrorException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthErrorException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OAuthErrorException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthErrorException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public OAuthErrorException(string message, Exception inner)
        : base(message, inner) { }
}
