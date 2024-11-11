using EasonEetwViewer.Dto.Http.Response.Model;
using EasonEetwViewer.Dto.Http.Response.Record;

namespace EasonEetwViewer.Dto.Http.Response;

/// <summary>
/// Represents the result of an API call on <c>gd.earthquake.list</c> API.
/// </summary>
public record PastEarthquakeList : PoolingResponse<EarthquakeInfo>;