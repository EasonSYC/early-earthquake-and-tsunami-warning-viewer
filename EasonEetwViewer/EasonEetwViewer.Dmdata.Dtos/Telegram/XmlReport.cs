﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.Telegram;
/// <summary>
/// Describes the head and control of a XML report.
/// </summary>
public class XmlReport
{
    /// <summary>
    /// The property <c>head</c>, the head component of the XML report.
    /// </summary>
    [JsonPropertyName("head")]
    public required XmlHead Head { get; init; }
    /// <summary>
    /// The property <c>control</c>, the control component of the XML report.
    /// </summary>
    [JsonPropertyName("control")]
    public required XmlControl Control { get; init; }
}
