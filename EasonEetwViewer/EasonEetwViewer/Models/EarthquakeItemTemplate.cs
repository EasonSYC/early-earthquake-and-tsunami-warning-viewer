using Avalonia.Media;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;

namespace EasonEetwViewer.Models;
internal record EarthquakeItemTemplate
{
    public EarthquakeItemTemplate(string eventId, EarthquakeIntensity? intensity, DateTime? originTime, Hypocentre? hypocentre, Magnitude? magnitude)
    {
        EventId = eventId;
        Intensity = intensity;
        OriginTime = originTime;
        Hypocentre = hypocentre;
        Magnitude = magnitude;
    }
    internal string EventId { get; private init; }
    internal EarthquakeIntensity? Intensity { get; private init; }
    internal DateTime? OriginTime { get; private init; }
    internal Hypocentre? Hypocentre { get; private init; }
    internal Magnitude? Magnitude { get; private init; }
    public string IntensityDisplay => Intensity is EarthquakeIntensity intensity ? intensity.ToReadableString() : "-";
    public IBrush? IntensityColour => Intensity.ToColourString() is string str ? new SolidColorBrush(Color.Parse($"#{str}")) : null;
    public string OriginTimeDisplay => OriginTime is DateTime time ? time.ToString("MM/dd HH:mm") : "不明";
    public string HypocentreDisplay => Hypocentre is Hypocentre hypocentre ? hypocentre.Name : "不明";
    public string DepthValueDisplay => Hypocentre is Hypocentre hypocentre
        ? hypocentre.Depth.Condition is DepthCondition condition ? condition.ToReadableString() : $"{hypocentre.Depth.Value}"
        : "不明";
    public string DepthUnitDisplay => Hypocentre is Hypocentre hypocentre
        ? hypocentre.Depth.Condition is DepthCondition ? string.Empty : hypocentre.Depth.Unit
        : string.Empty;
    public string MagnitudeValueDisplay => Magnitude is Magnitude magnitude
        ? magnitude.Condition is MagnitudeCondition condition ? condition.ToReadableString() : $"{magnitude.Value}"
        : "不明";
    public string MagnitudeUnitDisplay => Magnitude is Magnitude magnitude
        ? magnitude.Condition is MagnitudeCondition ? "M" : magnitude.Unit.ToReadableString()
        : "M";
}
