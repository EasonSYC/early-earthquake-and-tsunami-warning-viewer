using EasonEetwViewer.Dtos.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;