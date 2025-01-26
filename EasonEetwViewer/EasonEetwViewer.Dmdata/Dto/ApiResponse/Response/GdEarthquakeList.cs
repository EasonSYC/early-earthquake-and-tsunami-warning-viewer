using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.HttpRequest.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;