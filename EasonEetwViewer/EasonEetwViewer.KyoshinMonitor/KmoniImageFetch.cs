using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;

namespace EasonEetwViewer.KyoshinMonitor;

public class KmoniImageFetch
{
    private const string _baseUri = "http://www.kmoni.bosai.go.jp/data/map_img/RealTimeImg/";
    private const string _relativeUri = "[#1]_[#2]/[yyyyMMdd]/[yyyyMMdd][HHmmss].[#1]_[#2].gif";
    private const int _jstAheadUtcHours = 9;

    private readonly HttpClient _client;

    public KmoniImageFetch()
    {
        _client = new()
        {
            BaseAddress = new(_baseUri)
        };
    }

    public async Task<byte[]> GetByteArrayAsync(KmoniDataType kmoniDataType, SensorType sensorType, DateTime utcDateTime)
    {
        string kmoniDataTypeStr = kmoniDataType.ToUriString();
        string sensorTypeStr = sensorType.ToUriString();
        DateTime jstDateTime = utcDateTime.AddHours(_jstAheadUtcHours);
        string yearMonthDateStr = jstDateTime.ToString("yyyyMMdd");
        string hourMinuteSecondStr = jstDateTime.ToString("HHmmss");

        string relativeUri = _relativeUri
            .Replace("[#1]", kmoniDataTypeStr)
            .Replace("[#2]", sensorTypeStr)
            .Replace("[yyyyMMdd]", yearMonthDateStr)
            .Replace("[HHmmss]", hourMinuteSecondStr);
        
        using HttpRequestMessage request = new(HttpMethod.Get, relativeUri);
        using HttpResponseMessage response = await _client.SendAsync(request);

        _ = response.EnsureSuccessStatusCode();

        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

        return imageBytes;
    }
}
