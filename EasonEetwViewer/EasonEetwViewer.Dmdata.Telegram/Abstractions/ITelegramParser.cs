using EasonEetwViewer.Dmdata.Dtos.Telegram;
using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;

namespace EasonEetwViewer.Dmdata.Telegram.Abstractions;
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
    /// <summary>
    /// Gives the corresponding to the <see cref="SchemaVersionInformation"/>.
    /// </summary>
    /// <param name="schema">The JSON schema version information.</param>
    /// <returns>The type of the JSON schema, <see langword="null"/> if unknown.</returns>
    Type? ParseSchemaInformation(SchemaVersionInformation schema);
}
