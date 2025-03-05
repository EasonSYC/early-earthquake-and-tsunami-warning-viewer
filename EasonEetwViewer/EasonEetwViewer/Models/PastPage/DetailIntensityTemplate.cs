using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
/// <summary>
/// Represents the node for an intensity in the intensity display tree.
/// </summary>
internal record DetailIntensityTemplate
{
    /// <summary>
    /// The intensity of the node.
    /// </summary>
    public required Intensity Intensity { get; init; }
    /// <summary>
    /// The nodes that has this intensity.
    /// </summary>
    public required IEnumerable<DisplayNode> PositionNodes { get; init; }
}
