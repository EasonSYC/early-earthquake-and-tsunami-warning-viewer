using EasonEetwViewer.Authentication.Services;

namespace EasonEetwViewer.Authentication.Exceptions;
/// <summary>
/// Represents error by attempting to get authentication from a <see cref="NullAuthenticator"/>.
/// </summary>
public sealed class NullAuthenticatiorException : Exception
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="NullAuthenticatiorException"/> class.
    /// </summary>
    public NullAuthenticatiorException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="NullAuthenticatiorException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public NullAuthenticatiorException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="NullAuthenticatiorException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public NullAuthenticatiorException(string message, Exception inner)
        : base(message, inner) { }
}
