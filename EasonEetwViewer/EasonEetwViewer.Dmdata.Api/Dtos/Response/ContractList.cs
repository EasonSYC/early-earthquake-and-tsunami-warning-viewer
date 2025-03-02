using EasonEetwViewer.Dmdata.Api.Dtos.Record.Contract;
using EasonEetwViewer.Dmdata.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Dmdata.Api.Dtos.Response;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListBase<Contract>;