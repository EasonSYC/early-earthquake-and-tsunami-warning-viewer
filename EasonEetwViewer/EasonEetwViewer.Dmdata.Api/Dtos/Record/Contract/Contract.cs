using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Dmdata.Api.Dtos.Record.Contract;

/// <summary>
/// Represents a contract of subscription to <see href="dmdata.jp"/>.
/// </summary>
public record Contract
{
    /// <summary>
    /// The property <c>id</c>. The subscription ID.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> if not subscribed to the contract.
    /// </remarks>
    [JsonPropertyName("id")]
    public required int? SubscriptionId { get; init; }
    /// <summary>
    /// The property <c>planId</c>. The contract plan ID.
    /// </summary>
    [JsonPropertyName("planId")]
    public required int PlanId { get; init; }
    /// <summary>
    /// The property <c>planName</c>. The name of the contract.
    /// </summary>
    [JsonPropertyName("planName")]
    public required string PlanName { get; init; }
    /// <summary>
    /// The property <c>classifications</c>. The classification of the contract.
    /// </summary>
    [JsonPropertyName("classification")]
    public required Classification Classification { get; init; }
    /// <summary>
    /// The property <c>price</c>. The price of the contract.
    /// </summary>
    [JsonPropertyName("price")]
    public required UnitPrice Price { get; init; }
    /// <summary>
    /// The property <c>start</c>. The start time of the subscription.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> if not subscribed to the contract.
    /// </remarks>
    [JsonPropertyName("start")]
    public required DateTimeOffset? StartTime { get; init; }
    /// <summary>
    /// The property <c>isValid</c>. Whether the user is subscribed to the contract.
    /// </summary>
    [JsonPropertyName("isValid")]
    public required bool IsValid { get; init; }
    /// <summary>
    /// The property <c>connectionCounts</c>. The number of extra WebSocket connection this subscription gives.
    /// </summary>
    [JsonPropertyName("connectionCounts")]
    public required int ConnectionCounts { get; init; }
}