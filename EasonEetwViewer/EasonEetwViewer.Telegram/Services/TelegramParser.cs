using System.Text.Json;
using EasonEetwViewer.Dtos.Telegram;
using EasonEetwViewer.Telegram.Abstractions;
using EasonEetwViewer.Telegram.Dtos.Schema;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.Telegram.Exceptions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Telegram.Services;
/// <summary>
/// The default implementation of the <see cref="ITelegramParser"/> interface.
/// </summary>
public sealed class TelegramParser : ITelegramParser
{
    /// <summary>
    /// The JSON serialisation options to be used.
    /// </summary>
    private readonly JsonSerializerOptions _options;
    /// <summary>
    /// The logger to be used for logging.
    /// </summary>
    private readonly ILogger<TelegramParser> _logger;
    /// <summary>
    /// Creates a new instance of the class <see cref="TelegramParser"/>.
    /// </summary>
    /// <param name="options">The serialisation options to be used for JSON.</param>
    /// <param name="logger">The logger to be used.</param>
    public TelegramParser(JsonSerializerOptions options, ILogger<TelegramParser> logger)
    {
        _options = options;
        _logger = logger;
        _logger.Instantiated();
    }
    /// <summary>
    /// A list of supported schemas.
    /// </summary>
    private readonly Dictionary<SchemaVersionInformation, Type> _supportedSchemas = new()
    {
        {
            new SchemaVersionInformation {
                Type = "eew-information",
                Version = "1.0.0" },
            typeof(EewInformationSchema)
        },
        {
            new SchemaVersionInformation {
                Type = "earthquake-information",
                Version = "1.1.0" },
            typeof(EarthquakeInformationSchema)
        },
        {
            new SchemaVersionInformation {
                Type = "tsunami-information",
                Version = "1.0.0" },
            typeof(TsunamiInformationSchema)
        },
    };
    /// <inheritdoc/>
    /// <exception cref="TelegramParserFormatException">When there was an error in parsing the telegram.</exception>
    /// <exception cref="TelegramParserUnsupportedException">When the telegram has an unsupported schema.</exception>
    public Head ParseJsonTelegram(string json)
    {
        try
        {
            _logger.HandlingJson(json);
            Head headData = JsonSerializer.Deserialize<Head>(json, _options)
               ?? throw new TelegramParserFormatException($"Cannot deserialise: {json}");

            if (_supportedSchemas.TryGetValue(headData.Schema, out Type? type))
            {
                _logger.SupportedSchema(headData.Schema.Type, headData.Schema.Version);
                Head? data = JsonSerializer.Deserialize(json, type, _options) as Head;
                return data ?? throw new TelegramParserFormatException($"Cannot deserialise: {json}");
            }

            _logger.UnsupportedSchema(headData.Schema.Type, headData.Schema.Version);
            throw new TelegramParserUnsupportedException($"Schema is not supported: Type {headData.Schema.Type}, Version {headData.Schema.Version}");
        }
        catch (TelegramParserFormatException)
        {
            _logger.IncorrectJsonFormat(json);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.IncorrectJsonFormat(json);
            throw new TelegramParserFormatException($"Cannot deserialised: {json}", ex);
        }
    }
}
