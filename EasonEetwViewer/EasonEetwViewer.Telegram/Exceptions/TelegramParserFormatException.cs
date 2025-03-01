using EasonEetwViewer.Telegram.Services;

namespace EasonEetwViewer.Telegram.Exceptions;
/// <summary>
/// Represents format errors that occurs in <see cref="TelegramParser"/>.
/// </summary>
public class TelegramParserFormatException : TelegramParserException
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserFormatException"/> class.
    /// </summary>
    public TelegramParserFormatException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserFormatException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public TelegramParserFormatException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserFormatException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public TelegramParserFormatException(string message, Exception inner)
        : base(message, inner) { }
}
