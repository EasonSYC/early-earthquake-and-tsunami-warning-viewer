using EasonEetwViewer.Api.Dtos.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Api.Dtos.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;