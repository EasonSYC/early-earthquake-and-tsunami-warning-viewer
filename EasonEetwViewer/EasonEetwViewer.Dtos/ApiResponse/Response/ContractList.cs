using EasonEetwViewer.Dtos.ApiResponse.Record.Contract;
using EasonEetwViewer.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListBase<Contract>;