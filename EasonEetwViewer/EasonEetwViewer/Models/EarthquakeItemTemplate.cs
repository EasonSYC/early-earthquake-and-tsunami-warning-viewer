using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    private EarthquakeIntensity? _intensity;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(OriginTimeDisplay))]
    private DateTime? _originTime;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HypocentreDisplay))]
    [NotifyPropertyChangedFor(nameof(DepthDisplay))]
    private Hypocentre? _hypocentre;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MagnitudeDisplay))]
    private Magnitude? _magnitude;
    public string IntensityDisplay => Intensity is EarthquakeIntensity intensity ? intensity.ToReadableString() : "-";
    public string OriginTimeDisplay => OriginTime is DateTime time ? time.ToString("MM/dd HH:mm") : "不明";
    public string HypocentreDisplay => Hypocentre is Hypocentre hypocentre ? hypocentre.Name : "不明";
    public string DepthDisplay => Hypocentre is Hypocentre hypocentre 
        ? hypocentre.Depth.Condition is DepthCondition condition ? condition.ToReadableString() : $"{hypocentre.Depth.Value} {hypocentre.Depth.Unit}"
        : "不明";
    public string MagnitudeDisplay => Magnitude is Magnitude magnitude
        ? magnitude.Condition is MagnitudeCondition condition ? condition.ToReadableString() : $"{magnitude.Unit.ToReadableString()} {magnitude.Value}"
        : "不明";
}
