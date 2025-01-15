using Avalonia.Media;

namespace EasonEetwViewer.Models;
internal record DetailIntensityTemplate
{
    internal string DisplayText { get; private init; }
    internal IBrush ForegroundColour { get; private init; }
    internal List<PositionNode> PositionNodes { get; private init; }
    internal DetailIntensityTemplate(string displayText, IBrush foregroundColour, List<PositionNode> positionNodes)
    {
        DisplayText = displayText;
        ForegroundColour = foregroundColour;
        PositionNodes = positionNodes;
    }
}
