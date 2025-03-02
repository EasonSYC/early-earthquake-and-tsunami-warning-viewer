using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
internal record EarthquakeItemTemplate
{
    internal EarthquakeItemTemplate(string eventId, Intensity? intensity, DateTimeOffset? originTime, Hypocentre? hypocentre, Magnitude? magnitude)
    {
        EventId = eventId;
        Intensity = intensity;
        OriginTime = originTime;
        Hypocentre = hypocentre;
        Magnitude = magnitude;
    }
    internal string EventId { get; private init; }
    internal Intensity? Intensity { get; private init; }
    internal DateTimeOffset? OriginTime { get; private init; }
    internal Hypocentre? Hypocentre { get; private init; }
    internal Magnitude? Magnitude { get; private init; }
}
