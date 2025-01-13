using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;

namespace EasonEetwViewer.Models;
internal partial class EarthquakeItemTemplate : ObservableObject
{
    public EarthquakeItemTemplate(string eventId, EarthquakeIntensity? intensity, DateTime? originTime, Hypocentre? hypocentre, Magnitude? magnitude)
    {
        EventId = eventId;
        Intensity = intensity;
        OriginTime = originTime;
        Hypocentre = hypocentre;
        Magnitude = magnitude;
    }
    [ObservableProperty]
    private string _eventId;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IntensityDisplay))]
    [NotifyPropertyChangedFor(nameof(IntensityColour))]
    private EarthquakeIntensity? _intensity;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OriginTimeDisplay))]
    private DateTime? _originTime;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HypocentreDisplay))]
    [NotifyPropertyChangedFor(nameof(DepthValueDisplay))]
    [NotifyPropertyChangedFor(nameof(DepthUnitDisplay))]
    private Hypocentre? _hypocentre;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MagnitudeValueDisplay))]
    [NotifyPropertyChangedFor(nameof(MagnitudeUnitDisplay))]
    private Magnitude? _magnitude;
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
