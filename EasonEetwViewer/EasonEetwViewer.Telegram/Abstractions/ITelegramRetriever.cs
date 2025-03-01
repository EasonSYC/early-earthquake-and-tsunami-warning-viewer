using EasonEetwViewer.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Telegram.Abstractions;
/// <summary>
/// Represents a retriever for JSON Telegrams.
/// </summary>
public interface ITelegramRetriever
{
    /// <summary>
    /// Retrieves a JSON Telegram with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the telegram.</param>
    /// <returns>The telegram that was parsed, or <see langword="null"/> if unsuccessful.</returns>
    Task<Head?> GetJsonTelegramAsync(string id);
}
