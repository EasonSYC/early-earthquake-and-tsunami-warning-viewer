using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;

namespace EasonEetwViewer.Models;
internal record DetailIntensityTemplate
{
    internal Intensity Intensity { get; init; }
    internal List<PositionNode> PositionNodes { get; private init; }
    internal DetailIntensityTemplate(Intensity intensity, List<PositionNode> positionNodes)
    {
        Intensity = intensity;
        PositionNodes = positionNodes;
    }
}
