using EasonEetwViewer.Dtos.Dto.ApiResponse.Record.Contract;
using EasonEetwViewer.Dtos.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>contract.list</c> API.
/// </summary>
public record ContractList : ListBase<Contract>;