﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeXmlControl
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }
    [JsonPropertyName("status")]
    public required TelegramStatus Status { get; init; }
    [JsonPropertyName("dateTime")]
    public required DateTime Time { get; init; }
    [JsonPropertyName("editorialOffice")]
    public required string EditorialOffice { get; init; }
    [JsonPropertyName("publishingOffice")]
    public required string PublishingOFfice { get; init; }
}
