using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;
using EasonEetwViewer.Models.EnumExtensions;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using NetTopologySuite.Geometries;

namespace EasonEetwViewer.ViewModels;
internal partial class PastPageViewModel(StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, ApiCaller apiCaller, TelegramRetriever telegramRetriever) 
    : MapViewModelBase(resources, kmoniOptions, authenticatorDto, apiCaller, telegramRetriever)
{
    [ObservableProperty]
    private ObservableCollection<EarthquakeItemTemplate> _earthquakeList = [];

    [ObservableProperty]
    private EarthquakeItemTemplate? _selectedEarthquake;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEarthquakeDetailsEnabled))]
    private EarthquakeDetailsTemplate? _earthquakeDetails;
    public bool IsEarthquakeDetailsEnabled => EarthquakeDetails is not null;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoadExtraEnabled))]
    private string _cursorToken = string.Empty;
    public bool IsLoadExtraEnabled => CursorToken != string.Empty;

    private const string _regionLayerName = "Regions";
    private const string _obsPointLayerName = "Point";
    private const string _hypocentreLayerName = "Hypocentre";
    private CancellationTokenSource _cts = new();

    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        _ = Map.Layers.Remove((x => x.Name == _regionLayerName));
        _ = Map.Layers.Remove((x => x.Name == _obsPointLayerName));
        _ = Map.Layers.Remove((x => x.Name == _hypocentreLayerName));

        // TODO: ZOOM FEATURE

        // async code needs cancellation token to prevent different ones add layers
        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;

        EarthquakeDetails = null;

        if (value is not null)
        {
            PastEarthquakeEventResponse rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
            List<EarthquakeTelegram> telegrams = rsp.EarthquakeEvent.Telegrams;
            telegrams = telegrams.Where(x => x.TelegramHead.Type == "VXSE53").ToList();
            if (telegrams.Count != 0)
            {
                if (!_isStationsRetrieved)
                {
                    await UpdateEarthquakeObservationStations();
                }

                EarthquakeTelegram telegram = telegrams.MaxBy(x => x.Serial)!;
                EarthquakeInformationSchema telegramInfo = await _telegramRetriever.GetTelegramJsonAsync<EarthquakeInformationSchema>(telegram.Id);
                IntensityDetailTree tree = new();

                if (telegramInfo.Body.Intensity is not null)
                {
                    ICollection<IFeature> stationFeature = [];
                    List<EarthquakeInformationStationData> stationData = telegramInfo.Body.Intensity.Stations;
                    foreach (EarthquakeInformationStationData station in stationData)
                    {
                        if (station.MaxInt is EarthquakeIntensityWithUnreceived newInt && newInt.ToEarthquakeIntensity() != EarthquakeIntensity.Unknown)
                        {
                            List<Station> potnetialStations = _earthquakeObservationStations!.Where(x => x.XmlCode == station.Code).ToList();
                            if (potnetialStations.Count != 0)
                            {
                                Station stationDetails = potnetialStations[0];

                                PointFeature feature = new(SphericalMercator.FromLonLat(stationDetails.Longitude, stationDetails.Latitude).ToMPoint());
                                feature.Styles.Add(new SymbolStyle()
                                {
                                    SymbolScale = 0.25,
                                    Fill = new Brush(Color.FromString($"#{newInt.ToEarthquakeIntensity().ToColourString()}")),
                                    Outline = new Pen { Color = Color.Black }
                                });
                                stationFeature.Add(feature);

                                string prefectureCode = stationDetails.City.Code[0..2];
                                List<PrefectureData> potentialPrefectures = _resources.Prefectures.Where(x => x.Code == prefectureCode).ToList();
                                if (potentialPrefectures.Count != 0)
                                {
                                    PrefectureData prefectureData = potentialPrefectures[0];
                                    tree.AddPositionNode(newInt.ToEarthquakeIntensity(),
                                        new(prefectureData.Name, prefectureData.Code,
                                            new(stationDetails.Region.KanjiName, stationDetails.Region.Code,
                                                new(stationDetails.City.KanjiName, stationDetails.City.Code,
                                                    new(stationDetails.KanjiName, stationDetails.XmlCode)))));
                                }
                            }
                        }
                    }

                    ILayer layer = new MemoryLayer()
                    {
                        Name = _obsPointLayerName,
                        Features = stationFeature,
                        IsMapInfoLayer = true,
                        Style = null
                    };

                    if (!token.IsCancellationRequested)
                    {
                        Map.Layers.Add(new RasterizingLayer(CreateRegionLayer(telegramInfo.Body.Intensity.Regions!)));
                        Map.Layers.Add(new RasterizingLayer(layer));
                    }
                }

                if (!token.IsCancellationRequested)
                {
                    EarthquakeDetails = new(value.EventId, value.Intensity, value.OriginTime, value.Hypocentre, value.Magnitude, "Test", telegramInfo.ReportDateTime, tree);
                }
            }

            if (value.Hypocentre is not null
                && value.Hypocentre.Coordinates.Longitude is not null
                && value.Hypocentre.Coordinates.Latitude is not null)
            {
                MPoint coords = SphericalMercator.FromLonLat(value.Hypocentre.Coordinates.Longitude.DoubleValue, value.Hypocentre.Coordinates.Latitude.DoubleValue).ToMPoint();

                ILayer layer = new MemoryLayer()
                {
                    Name = _hypocentreLayerName,
                    Features = [new PointFeature(coords)],
                    Style = _resources.HypocentreShapeStyle,
                    IsMapInfoLayer = true
                };

                if (!token.IsCancellationRequested)
                {
                    Map.Layers.Add(layer);
                }
            }
        }
    }

    private Layer CreateRegionLayer(List<EarthquakeInformationRegionData> regions)
        => new()
        {
            Name = _regionLayerName,
            DataSource = _resources.PastRegion,
            Style = CreateRegionThemeStyle(regions),
            IsMapInfoLayer = true
        };

    // Adapted from https://mapsui.com/v5/samples/ - Styles - ThemeStyle on ShapeFile
    private static ThemeStyle CreateRegionThemeStyle(List<EarthquakeInformationRegionData> regions)
        => new(f =>
            {
                if (f is GeometryFeature geometryFeature)
                {
                    if (geometryFeature.Geometry is Point)
                    {
                        return null;
                    }
                }

                VectorStyle? style = null;

                foreach (EarthquakeInformationRegionData region in regions)
                {
                    if (region.MaxInt is not null && f["code"]?.ToString()?.ToLower() == region.Code)
                    {
                        style = new VectorStyle()
                        {
                            Fill = new Brush(Color.Opacity(Color.FromString($"#{region.MaxInt!.ToColourString()}"), 0.60f))
                        };
                    }
                }

                return style;
            });

    [RelayCommand]
    private void JumpYahooWebpage()
        => _ = Process.Start(new ProcessStartInfo // https://stackoverflow.com/a/61035650/
        {
            FileName = $"https://typhoon.yahoo.co.jp/weather/jp/earthquake/{EarthquakeDetails!.EventId}.html",
            UseShellExecute = true
        });

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        PastEarthquakeListResponse rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 50);
        List<EarthquakeInfo> eqList = rsp.ItemList;
        CursorToken = rsp.NextToken ?? string.Empty;

        ObservableCollection<EarthquakeItemTemplate> currentEarthquake = [];
        eqList.ForEach(x => currentEarthquake.Add(new(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));

        SelectedEarthquake = null;
        EarthquakeList = currentEarthquake;
    }

    [RelayCommand]
    private async Task LoadExtraEarthquakes()
    {
        if (CursorToken != string.Empty)
        {
            PastEarthquakeListResponse rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 10, cursorToken: CursorToken);
            List<EarthquakeInfo> eqList = rsp.ItemList;
            CursorToken = rsp.NextToken ?? string.Empty;

            eqList.ForEach(x => EarthquakeList.Add(new(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));
        }
    }
}
