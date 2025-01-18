using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;

namespace EasonEetwViewer.Models;
internal class EarthquakeDetailsTemplate : ObservableObject
{
    internal EarthquakeDetailsTemplate(string eventId, EarthquakeIntensity? intensity, DateTimeOffset? originTime, Hypocentre? hypocentre, Magnitude? magnitude, string? informationalText, DateTimeOffset? lastUpdated, IntensityDetailTree detailTree)
    {
        EventId = eventId;
        Intensity = intensity;
        OriginTime = originTime;
        Hypocentre = hypocentre;
        Magnitude = magnitude;
        InformationalText = informationalText;
        LastUpdated = lastUpdated;
        DetailTree = detailTree;
    }
    internal string? EventId { get; private init; }
    internal EarthquakeIntensity? Intensity { get; private init; }
    internal DateTimeOffset? OriginTime { get; private init; }
    internal DateTimeOffset? LastUpdated { get; private init; }
    internal Hypocentre? Hypocentre { get; private init; }
    internal Magnitude? Magnitude { get; private init; }
    internal string? InformationalText { get; private init; }
    internal IntensityDetailTree DetailTree { get; private init; }
    internal string IntensityDisplay => Intensity is EarthquakeIntensity intensity ? intensity.ToReadableString() : "-";
    internal IBrush? IntensityColour => Intensity.ToColourString() is string str ? new SolidColorBrush(Color.Parse($"#{str}")) : null;
    internal string OriginTimeDisplay => OriginTime is DateTimeOffset time ? time.ToString("yyyy/MM/dd HH:mm") : "不明";
    internal string HypocentreDisplay => Hypocentre is Hypocentre hypocentre ? hypocentre.Name : "不明";
    internal string DepthValueDisplay => Hypocentre is Hypocentre hypocentre
        ? hypocentre.Depth.Condition is DepthCondition condition ? condition.ToReadableString() : $"{hypocentre.Depth.Value}"
        : "不明";
    internal string DepthUnitDisplay => Hypocentre is Hypocentre hypocentre
        ? hypocentre.Depth.Condition is DepthCondition ? string.Empty : hypocentre.Depth.Unit
        : string.Empty;
    internal string MagnitudeValueDisplay => Magnitude is Magnitude magnitude
        ? magnitude.Condition is MagnitudeCondition condition ? condition.ToReadableString() : $"{magnitude.Value}"
        : "不明";
    internal string MagnitudeUnitDisplay => Magnitude is Magnitude magnitude
        ? magnitude.Condition is MagnitudeCondition ? "M" : magnitude.Unit.ToReadableString()
        : "M";
    internal string LastUpdatedDisplay => LastUpdated is DateTimeOffset time ? time.ToString("yyyy/MM/dd HH:mm") : "不明";
}
