using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

/// <summary>
/// Represents a contract.
/// </summary>
public record Contract
{
    /// <summary>
    /// The property <c>id</c>. The subscription ID.
    /// <c>null</c> if not subscribed to the contract.
    /// </summary>
    [JsonPropertyName("id")]
    public int? SubscriptionId { get; init; } = 0;
    /// <summary>
    /// The property <c>planId</c>. The contract plan ID.
    /// </summary>
    [JsonPropertyName("planId")]
    public int PlanId { get; init; } = 0;
    /// <summary>
    /// The property <c>planName</c>. The name of the contract.
    /// </summary>
    [JsonPropertyName("planName")]
    public string PlanName { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>classifications</c>. The classification of the contract.
    /// </summary>
    [JsonPropertyName("classification")]
    public ResponseEnums.Classification Classification { get; init; }
    /// <summary>
    /// The property <c>price</c>. The price of the contract.
    /// </summary>
    [JsonPropertyName("price")]
    public UnitPrice Price { get; init; } = new();
    /// <summary>
    /// The property <c>start</c>. The start time of the subscription.
    /// <c>null</c> if not subscribed to the contract.
    /// </summary>
    [JsonPropertyName("start")]
    public DateTime? StartTime { get; init; } = new();
    /// <summary>
    /// The property <c>isValid</c>. Whether the user is subscribed to the contract.
    /// </summary>
    [JsonPropertyName("isValid")]
    public bool IsValid { get; init; } = false;
    /// <summary>
    /// The property <c>connectionCounts</c>. The number of extra WebSocket connection this subscription gives.
    /// </summary>
    [JsonPropertyName("connectionCounts")]
    public int ConnectionCounts { get; init; } = 0;
}