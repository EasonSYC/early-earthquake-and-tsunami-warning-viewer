using EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
internal record EarthquakeItemTemplate
{
    public EarthquakeItemTemplate(EarthquakeInfo earthquakeInfo)
    {
        EventId = earthquakeInfo.EventId;
        Intensity = earthquakeInfo.MaxIntensity;
        OriginTime = earthquakeInfo.OriginTime;
        Hypocentre = earthquakeInfo.Hypocentre;
        Magnitude = earthquakeInfo.Magnitude;
    }
    public string EventId { get; private init; }
    public Intensity? Intensity { get; private init; }
    public DateTimeOffset? OriginTime { get; private init; }
    public Hypocentre? Hypocentre { get; private init; }
    public Magnitude? Magnitude { get; private init; }
}
