using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.Enum;

namespace EasonEetwViewer.Models.PastPage;
internal record EarthquakeDetailsTemplate
{
    public required string? EventId { get; init; }
    public required Intensity? Intensity { get; init; }
    public required DateTimeOffset? OriginTime { get; init; }
    public required DateTimeOffset? LastUpdated { get; init; }
    public required Hypocentre? Hypocentre { get; init; }
    public required Magnitude? Magnitude { get; init; }
    public required string? InformationalText { get; init; }
    public required IEnumerable<DetailIntensityTemplate> DetailDisplay { get; init; }
}
