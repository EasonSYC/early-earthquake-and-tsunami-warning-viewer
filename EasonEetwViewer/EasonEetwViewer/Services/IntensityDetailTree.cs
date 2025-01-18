using Avalonia.Media;
using EasonEetwViewer.Converters.EnumExtensions;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Services;
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
