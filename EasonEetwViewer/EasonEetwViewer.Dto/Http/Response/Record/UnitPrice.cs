using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// Represents the price of a contract.
/// </summary>
public record UnitPrice
{
    /// <summary>
    /// The property <c>day</c>. The daily price of the contract.
    /// </summary>
    [JsonPropertyName("day")]
    public int Day { get; init; }
    /// <summary>
    /// The property <c>month</c>. The monthly price of the contract.
    /// </summary>
    [JsonPropertyName("month")]
    public int Month { get; init; }
}