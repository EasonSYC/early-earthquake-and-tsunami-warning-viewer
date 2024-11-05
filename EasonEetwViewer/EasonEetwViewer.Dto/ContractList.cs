using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record ContractList : ResponseModels.ListResponse<Contract>;