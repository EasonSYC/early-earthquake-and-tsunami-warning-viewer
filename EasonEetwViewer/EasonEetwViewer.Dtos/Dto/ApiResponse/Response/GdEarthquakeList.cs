using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.ResponseBase;

namespace EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;
/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record GdEarthquakeList : PoolingBase<EarthquakeInfo>;