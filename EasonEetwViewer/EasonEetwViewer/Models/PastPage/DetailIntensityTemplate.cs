using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
internal record DetailIntensityTemplate
{
    public required Intensity Intensity { get; init; }
    public required IEnumerable<PositionNode> PositionNodes { get; init; }
}
