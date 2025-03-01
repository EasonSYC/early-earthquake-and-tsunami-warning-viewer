using EasonEetwViewer.Telegram.Services;

namespace EasonEetwViewer.WebSocket.Exceptions;
/// <summary>
/// Represents unsupported errors that occurs in <see cref="TelegramParser"/>.
/// </summary>
public class TelegramParserUnsupportedException : TelegramParserException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserUnsupportedException"/> class.
    /// </summary>
    public TelegramParserUnsupportedException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserUnsupportedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public TelegramParserUnsupportedException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserUnsupportedException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public TelegramParserUnsupportedException(string message, Exception inner)
        : base(message, inner) { }
}
