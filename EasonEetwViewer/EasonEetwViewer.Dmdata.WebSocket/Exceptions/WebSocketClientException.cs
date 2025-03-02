namespace EasonEetwViewer.Dmdata.WebSocket.Exceptions;
/// <summary>
/// Represents errors that occurs in <see cref="WebSocketClient"/>.
/// </summary>
public class WebSocketClientException : Exception
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientException"/> class.
    /// </summary>
    public WebSocketClientException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public WebSocketClientException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="WebSocketClientException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public WebSocketClientException(string message, Exception inner)
        : base(message, inner) { }
}
