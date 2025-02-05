using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;

namespace EasonEetwViewer.Models;
internal record DetailIntensityTemplate
{
    internal Intensity Intensity { get; init; }
    internal IEnumerable<PositionNode> PositionNodes { get; private init; }
    internal DetailIntensityTemplate(Intensity intensity, IEnumerable<PositionNode> positionNodes)
    {
        Intensity = intensity;
        PositionNodes = positionNodes;
    }
}
