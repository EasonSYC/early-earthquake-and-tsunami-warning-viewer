using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// Inherits from <c>ResponseModels.ListResponse&lt;Contract&gt;</c>.
/// </summary>
public record ContractList : ResponseModels.ListResponse<Contract>;