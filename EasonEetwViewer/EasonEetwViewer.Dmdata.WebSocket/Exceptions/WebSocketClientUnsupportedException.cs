using EasonEetwViewer.WebSocket.Services;

namespace EasonEetwViewer.WebSocket.Exceptions;
/// <summary>
/// Represents unsupported operations that occurs in <see cref="WebSocketClient"/>.
/// </summary>
public class WebSocketClientUnsupportedException : WebSocketClientException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientUnsupportedException"/> class.
    /// </summary>
    public WebSocketClientUnsupportedException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientUnsupportedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public WebSocketClientUnsupportedException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientUnsupportedException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public WebSocketClientUnsupportedException(string message, Exception inner)
        : base(message, inner) { }
}
