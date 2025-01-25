﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;
internal record HeadInfo
{
    [JsonInclude]
    [JsonPropertyName("type")]
    internal required string Type { get; init; }

    [JsonInclude]
    [JsonPropertyName("author")]
    internal required string Author { get; init; }

    [JsonInclude]
    [JsonPropertyName("target")]
    internal string? Target { get; init; }

    [JsonInclude]
    [JsonPropertyName("time")]
    internal required DateTimeOffset Time { get; init; }

    [JsonInclude]
    [JsonPropertyName("designation")]
    internal required string? Designation { get; init; }

    [JsonInclude]
    [JsonPropertyName("test")]
    internal required bool IsTest { get; init; }

    [JsonInclude]
    [JsonPropertyName("xml")]
    internal bool? IsXml { get; init; }
}
