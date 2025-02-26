using EasonEetwViewer.Dtos.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dtos.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dtos.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;