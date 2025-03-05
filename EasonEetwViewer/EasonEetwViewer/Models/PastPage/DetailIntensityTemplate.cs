using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Services;

namespace EasonEetwViewer.Models.PastPage;
internal record DetailIntensityTemplate
{
    public required Intensity Intensity { get; init; }
    public required IEnumerable<DisplayNode> PositionNodes { get; init; }
}
