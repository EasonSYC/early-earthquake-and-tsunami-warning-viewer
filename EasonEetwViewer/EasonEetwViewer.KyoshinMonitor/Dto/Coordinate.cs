﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dto;
/// <summary>
/// Represents the position of a pixel.
/// </summary>
public record Coordinate
{
    /// <summary>
    /// The property <c>x</c>, representing the position of the pixel on the width axis.
    /// </summary>
    [JsonPropertyName("x")]
    [JsonInclude]
    public required int X { get; init; }
    /// <summary>
    /// The property <c>y</c>, representing the position of the pixel on the height axis.
    /// </summary>
    [JsonPropertyName("y")]
    [JsonInclude]
    public required int Y { get; init; }
}
