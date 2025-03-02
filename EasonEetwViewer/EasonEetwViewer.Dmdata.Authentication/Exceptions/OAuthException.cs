namespace EasonEetwViewer.Dmdata.Authentication.Exceptions;
/// <summary>
/// Represents errors that occurs during OAuth2.
/// </summary>
public class OAuthException : Exception
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthException"/> class.
    /// </summary>
    public OAuthException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public OAuthException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="OAuthException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public OAuthException(string message, Exception inner)
        : base(message, inner) { }
}
