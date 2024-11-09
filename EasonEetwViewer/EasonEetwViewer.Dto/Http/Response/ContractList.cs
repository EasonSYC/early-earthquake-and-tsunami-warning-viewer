using EasonEetwViewer.Dto.Http.Response.Model;
using EasonEetwViewer.Dto.Http.Response.Record;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListResponse<Contract>;