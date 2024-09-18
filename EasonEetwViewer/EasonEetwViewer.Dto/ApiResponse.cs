namespace EasonEetwViewer.Dto;

public record ApiResponse
{
    public string ResponseId = string.Empty;
    public DateTime ResponseTime = new();
    public string Status = string.Empty;
}
