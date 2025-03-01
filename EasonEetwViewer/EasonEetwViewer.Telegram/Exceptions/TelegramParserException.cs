using EasonEetwViewer.Telegram.Services;

namespace EasonEetwViewer.Telegram.Exceptions;
/// <summary>
/// Represents errors that occurs in <see cref="TelegramParser"/>.
/// </summary>
public class TelegramParserException : Exception
{
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserException"/> class.
    /// </summary>
    public TelegramParserException() { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public TelegramParserException(string message)
        : base(message) { }
    /// <summary>
    /// Instantiates a new instance of the <see cref="TelegramParserException"/> class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="inner">The inner exception.</param>
    public TelegramParserException(string message, Exception inner)
        : base(message, inner) { }
}
