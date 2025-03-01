using EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;