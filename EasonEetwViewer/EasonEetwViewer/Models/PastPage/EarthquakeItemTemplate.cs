using EasonEetwViewer.Dmdata.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
/// <summary>
/// Describes an earthquake item in the sidebar earthquake list.
/// </summary>
internal record EarthquakeItemTemplate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EarthquakeItemTemplate"/> class from a <see cref="EarthquakeInfo"/>.
    /// </summary>
    /// <param name="earthquakeInfo">The <see cref="EarthquakeInfo"/> used as the data source.</param>
    public EarthquakeItemTemplate(EarthquakeInfo earthquakeInfo)
    {
        EventId = earthquakeInfo.EventId;
        Intensity = earthquakeInfo.MaxIntensity;
        OriginTime = earthquakeInfo.OriginTime;
        Hypocentre = earthquakeInfo.Hypocentre;
        Magnitude = earthquakeInfo.Magnitude;
    }
    /// <summary>
    /// The Event ID of the earthquake.
    /// </summary>
    public string EventId { get; private init; }
    /// <summary>
    /// The maximum intensity observed for the earthquake.
    /// </summary>
    public Intensity? Intensity { get; private init; }
    /// <summary>
    /// The time at which the earthquake originated.
    /// </summary>
    public DateTimeOffset? OriginTime { get; private init; }
    /// <summary>
    /// The hypocentre of the earthquake.
    /// </summary>
    public Hypocentre? Hypocentre { get; private init; }
    /// <summary>
    /// The magnitude of the earthquake.
    /// </summary>
    public Magnitude? Magnitude { get; private init; }
}
