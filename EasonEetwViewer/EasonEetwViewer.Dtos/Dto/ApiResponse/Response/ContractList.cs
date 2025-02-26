using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.Contract;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListBase<Contract>;