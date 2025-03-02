using EasonEetwViewer.WebSocket.Services;

namespace EasonEetwViewer.WebSocket.Exceptions;
/// <summary>
/// Represents incorrected formatted data that occurs in <see cref="WebSocketClient"/>.
/// </summary>
public class WebSocketClientFormatException : WebSocketClientException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientFormatException"/> class.
    /// </summary>
    public WebSocketClientFormatException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientFormatException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public WebSocketClientFormatException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientFormatException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public WebSocketClientFormatException(string message, Exception inner)
        : base(message, inner) { }
}
