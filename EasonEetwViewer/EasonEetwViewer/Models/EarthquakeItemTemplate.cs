using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.HttpRequest.Dto.Enum;

namespace EasonEetwViewer.Models;
internal record EarthquakeItemTemplate
{
    internal EarthquakeItemTemplate(string eventId, EarthquakeIntensity? intensity, DateTimeOffset? originTime, Hypocentre? hypocentre, Magnitude? magnitude)
    {
        EventId = eventId;
        Intensity = intensity;
        OriginTime = originTime;
        Hypocentre = hypocentre;
        Magnitude = magnitude;
    }
    internal string EventId { get; private init; }
    internal EarthquakeIntensity? Intensity { get; private init; }
    internal DateTimeOffset? OriginTime { get; private init; }
    internal Hypocentre? Hypocentre { get; private init; }
    internal Magnitude? Magnitude { get; private init; }
}
