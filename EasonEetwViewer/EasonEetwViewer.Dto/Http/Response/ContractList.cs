using EasonEetwViewer.Dto.Http.Response.Models;
using EasonEetwViewer.Dto.Http.Response.Records;
using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// Inherits from <c>ResponseModels.ListResponse&lt;Contract&gt;</c>.
/// </summary>
public record ContractList : ListResponse<Contract>;