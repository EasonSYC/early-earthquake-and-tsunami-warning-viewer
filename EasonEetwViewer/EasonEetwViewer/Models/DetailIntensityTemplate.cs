﻿using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.Models;
internal record DetailIntensityTemplate
{
    internal EarthquakeIntensity Intensity { get; init; }
    internal List<PositionNode> PositionNodes { get; private init; }
    internal DetailIntensityTemplate(EarthquakeIntensity intensity, List<PositionNode> positionNodes)
    {
        Intensity = intensity;
        PositionNodes = positionNodes;
    }
}
