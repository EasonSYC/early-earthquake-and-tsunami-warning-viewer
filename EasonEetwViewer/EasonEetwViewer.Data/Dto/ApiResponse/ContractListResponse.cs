using EasonEetwViewer.HttpRequest.Dto.Model;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractListResponse : ListResponse<Contract>;