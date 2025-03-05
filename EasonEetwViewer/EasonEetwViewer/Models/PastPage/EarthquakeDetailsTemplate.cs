using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Models.PastPage;
/// <summary>
/// Describes the details of an earthquake.
/// </summary>
internal record EarthquakeDetailsTemplate
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EarthquakeDetailsTemplate"/> class.
    /// </summary>
    /// <param name="earthquakeItem">The <see cref="EarthquakeItemTemplate"/>, which contains information for a fallback.</param>
    /// <param name="telegram">The <see cref="EarthquakeInformationSchema"/>, which contains most of the information.</param>
    /// <param name="tree">A tree of intensities.</param>
    public EarthquakeDetailsTemplate(
        EarthquakeItemTemplate earthquakeItem,
        EarthquakeInformationSchema? telegram,
        IEnumerable<DetailIntensityTemplate>? tree)
    {
        EventId = telegram?.EventId ?? earthquakeItem.EventId;
        Intensity = telegram?.Body.Intensity?.MaxInt ?? earthquakeItem.Intensity;
        OriginTime = telegram?.Body.Earthquake?.OriginTime ?? earthquakeItem.OriginTime;
        Hypocentre = telegram?.Body.Earthquake?.Hypocentre ?? earthquakeItem.Hypocentre;
        Magnitude = telegram?.Body.Earthquake?.Magnitude ?? earthquakeItem.Magnitude;
        LastUpdated = telegram?.ReportDateTime;
        InformationalText = telegram?.ToInformationalString();
        IntensityTree = tree ?? [];
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
    /// The time at which the information is last updated.
    /// </summary>
    public DateTimeOffset? LastUpdated { get; private init; }
    /// <summary>
    /// The hypocentre of the earthquake.
    /// </summary>
    public Hypocentre? Hypocentre { get; private init; }
    /// <summary>
    /// The magnitude of the earthquake.
    /// </summary>
    public Magnitude? Magnitude { get; private init; }
    /// <summary>
    /// The informational text from the telegram.
    /// </summary>
    public string? InformationalText { get; private init; }
    /// <summary>
    /// The detailed intensity tree.
    /// </summary>
    public IEnumerable<DetailIntensityTemplate> IntensityTree { get; private init; }
}
