using EasonEetwViewer.Api.Dtos.Record.Contract;
using EasonEetwViewer.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.Response;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListBase<Contract>;