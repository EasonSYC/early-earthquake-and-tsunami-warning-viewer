using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.OpenGL.Surfaces;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;
using ShimSkiaSharp;

namespace EasonEetwViewer.Models;
internal record IntensityDetailTree
{
    internal Dictionary<EarthquakeIntensity, PositionNode> Intensities { get; private set; } = [];

    internal void AddPositionNode(EarthquakeIntensity intensity, PositionNode node)
    {
        if (!Intensities.TryGetValue(intensity, out PositionNode? value))
        {
            value = new PositionNode(intensity.ToReadableString(), intensity.ToString());
            Intensities.Add(intensity, value);
        }

        value.AddPositionNode(node);
    }
    internal List<DetailIntensityTemplate> ToItemControlDisplay
    {
        get
        {
            List<DetailIntensityTemplate> finalResult = [];
            foreach (EarthquakeIntensity intensity in Intensities.Keys)
            {
                finalResult.Add(new(intensity.ToReadableString(),
                    intensity.ToColourString() is string str ? new SolidColorBrush(Color.Parse($"#{str}")) : null!,
                    GetNodeWithFixedIntensity(intensity)!));
            }

            return finalResult;
        }
    }
    internal List<PositionNode>? GetNodeWithFixedIntensity(EarthquakeIntensity intensity)
        => !Intensities.TryGetValue(intensity, out PositionNode? value) ? null : value.SubNodes;
}
