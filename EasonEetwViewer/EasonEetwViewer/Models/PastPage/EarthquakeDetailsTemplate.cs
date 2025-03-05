using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.Services;

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
        DetailDisplay = tree ?? [];
    }
    public string EventId { get; private init; }
    public Intensity? Intensity { get; private init; }
    public DateTimeOffset? OriginTime { get; private init; }
    public DateTimeOffset? LastUpdated { get; private init; }
    public Hypocentre? Hypocentre { get; private init; }
    public Magnitude? Magnitude { get; private init; }
    public string? InformationalText { get; private init; }
    public IEnumerable<DetailIntensityTemplate> DetailDisplay { get; private init; }
}
