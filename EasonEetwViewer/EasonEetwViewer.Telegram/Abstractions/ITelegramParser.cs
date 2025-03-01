using EasonEetwViewer.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Telegram.Abstractions;
/// <summary>
/// Represents a parser for JSON Telegrams.
/// </summary>
public interface ITelegramParser
{
    /// <summary>
    /// Parses a JSON Telegram.
    /// </summary>
    /// <param name="json">The JSON string to be parsed.</param>
    /// <returns>The telegram that was parsed.</returns>
    Head ParseJsonTelegram(string json);
}
