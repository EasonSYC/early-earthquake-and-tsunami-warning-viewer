using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.EarthquakeParameter;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Response;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation.Enum;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;

namespace EasonEetwViewer.ViewModels;
internal partial class PastPageViewModel(StaticResources resources, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, ITimeProvider timeProvider, OnAuthenticatorChanged onChange)
    : MapViewModelBase(resources, authenticatorDto, apiCaller, telegramRetriever, timeProvider, onChange)
{
    [ObservableProperty]
    private ObservableCollection<EarthquakeItemTemplate> _earthquakeList = [];

    [ObservableProperty]
    private EarthquakeItemTemplate? _selectedEarthquake;

    [ObservableProperty]
    private EarthquakeDetailsTemplate? _earthquakeDetails;

    private const double _extendFactor = 1.2;

    private const string _regionLayerName = "Regions";
    private const string _obsPointLayerName = "Point";
    private const string _hypocentreLayerName = "Hypocentre";
    private CancellationTokenSource _cts = new();

    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        _ = Map.Layers.Remove(x => x.Name == _regionLayerName);
        _ = Map.Layers.Remove(x => x.Name == _obsPointLayerName);
        _ = Map.Layers.Remove(x => x.Name == _hypocentreLayerName);

        Map.Navigator.ZoomToBox(GetMainLimitsOfJapan());

        // async code needs cancellation token to prevent different ones add layers
        _cts.Cancel();
        _cts.Dispose();
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;

        EarthquakeDetails = null;

        if (value is null)
        {
            return;
        }

        MRect? regionLimits = null;

        GdEarthquakeEvent rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
        IEnumerable<Telegram> telegrams = rsp.EarthquakeEvent.Telegrams;
        telegrams = telegrams.Where(x => x.TelegramHead.Type == "VXSE53");
        if (telegrams.Count() != 0)
        {
            if (!IsStationsRetrieved)
            {
                await UpdateEarthquakeObservationStations();
            }

            Telegram telegram = telegrams.MaxBy(x => x.Serial)!;
            EarthquakeInformationSchema telegramInfo = await _telegramRetriever.GetTelegramJsonAsync<EarthquakeInformationSchema>(telegram.Id);
            IntensityDetailTree tree = new();

            if (telegramInfo.Body.Intensity is not null)
            {
                ICollection<IFeature> stationFeature = [];
                IEnumerable<StationIntensity> stationData = telegramInfo.Body.Intensity.Stations;

                foreach (StationIntensity station in stationData)
                {
                    if (station.MaxInt is not IntensityWithUnreceived newInt || newInt.ToEarthquakeIntensity() == Intensity.Unknown)
                    {
                        continue;
                    }

                    Station? potnetialStation = _earthquakeObservationStations!.FirstOrDefault(x => x.XmlCode == station.Code);
                    if (potnetialStation is null)
                    {
                        continue;
                    }

                    PointFeature feature = new(SphericalMercator.FromLonLat(potnetialStation.Longitude, potnetialStation.Latitude).ToMPoint());
                    feature.Styles.Add(new SymbolStyle()
                    {
                        SymbolScale = 0.25,
                        Fill = new Brush(Color.FromString(newInt.ToEarthquakeIntensity().ToColourString())),
                        Outline = new Pen { Color = Color.Black }
                    });
                    stationFeature.Add(feature);

                    string prefectureCode = potnetialStation.City.Code[0..2];
                    PrefectureData? prefecture = _resources.Prefectures.FirstOrDefault(x => x.Code == prefectureCode);
                    if (prefecture is not null)
                    {
                        tree.AddPositionNode(newInt.ToEarthquakeIntensity(),
                            new(prefecture.Name, prefecture.Code,
                                new(potnetialStation.Region.KanjiName, potnetialStation.Region.Code,
                                    new(potnetialStation.City.KanjiName, potnetialStation.City.Code,
                                        new(station.Name, potnetialStation.XmlCode)))));
                    }
                }

                ILayer layer = new MemoryLayer()
                {
                    Name = _obsPointLayerName,
                    Features = stationFeature,
                    Style = null
                };

                if (!token.IsCancellationRequested)
                {
                    Map.Layers.Add(new RasterizingLayer(CreateRegionLayer(telegramInfo.Body.Intensity.Regions!)));
                    Map.Layers.Add(new RasterizingLayer(layer));
                }

                IEnumerable<IFeature> features = (await _resources.Region.GetFeaturesAsync(new(new MSection(GetLimitsOfJapan(), 1))));
                IEnumerable<RegionIntensity> regionData = telegramInfo.Body.Intensity.Regions;
                foreach (RegionIntensity region in regionData)
                {
                    IFeature? potentialFeature = features.FirstOrDefault(f => (f["code"]?.ToString()?.ToLower() == region.Code));
                    if (potentialFeature is not null)
                    {
                        regionLimits = regionLimits is null ? potentialFeature.Extent : regionLimits.Join(potentialFeature.Extent);
                    }
                }
            }

            string informationalText = ToInformationString(telegramInfo);

            if (!token.IsCancellationRequested)
            {
                EarthquakeDetails = new(value.EventId, value.Intensity, value.OriginTime, value.Hypocentre, value.Magnitude, informationalText, telegramInfo.ReportDateTime, tree.ToItemControlDisplay());
            }
        }

        // Mark Hypocentre
        MRect? hypocentreLimits = null;
        if (value.Hypocentre is not null
            && value.Hypocentre.Coordinates.Longitude is not null
            && value.Hypocentre.Coordinates.Latitude is not null)
        {
            MPoint coords = SphericalMercator.FromLonLat(value.Hypocentre.Coordinates.Longitude.DoubleValue, value.Hypocentre.Coordinates.Latitude.DoubleValue).ToMPoint();
            hypocentreLimits = coords.MRect;

            ILayer layer = new MemoryLayer()
            {
                Name = _hypocentreLayerName,
                Features = [new PointFeature(coords)],
                Style = _resources.HypocentreShapeStyle
            };

            if (!token.IsCancellationRequested)
            {
                Map.Layers.Add(layer);
            }
        }

        if (!token.IsCancellationRequested)
        {
            MRect limits = regionLimits is null ? GetMainLimitsOfJapan() : regionLimits;
            limits = hypocentreLimits is null ? limits : limits.Join(hypocentreLimits);

            if (regionLimits is not null || hypocentreLimits is not null)
            {
                limits = limits.Multiply(_extendFactor);
            }

            Map.Navigator.ZoomToBox(limits);
        }
    }

    private static string ToInformationString(EarthquakeInformationSchema earthquake)
    {
        StringBuilder sb = new();

        if (earthquake.Headline is not null)
        {
            _ = sb.AppendLine(earthquake.Headline);
        }

        if (earthquake.Body.Text is not null)
        {
            _ = sb.AppendLine(earthquake.Body.Text);
        }

        if (earthquake.Body.Comments is not null)
        {
            if (earthquake.Body.Comments.FreeText is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.FreeText);
            }

            if (earthquake.Body.Comments.Forecast is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.Forecast.Text);
            }

            if (earthquake.Body.Comments.Var is not null)
            {
                _ = sb.AppendLine(earthquake.Body.Comments.Var.Text);
            }
        }

        return sb.ToString();
    }

    private Layer CreateRegionLayer(IEnumerable<RegionIntensity> regions)
        => new()
        {
            Name = _regionLayerName,
            DataSource = _resources.Region,
            Style = CreateRegionThemeStyle(regions)
        };

    // Adapted from https://mapsui.com/v5/samples/ - Styles - ThemeStyle on ShapeFile
    private static ThemeStyle CreateRegionThemeStyle(IEnumerable<RegionIntensity> regions)
        => new(f =>
            {
                RegionIntensity? region = regions.FirstOrDefault(r => r.Code == f["code"]?.ToString()?.ToLower());
                return region is null || region.MaxInt is null
                    ? null
                    : new VectorStyle()
                    {
                        Fill = new Brush(Color.Opacity(Color.FromString(((Intensity)region.MaxInt).ToColourString()), 0.60f))
                    };
            });

    [RelayCommand]
    private void JumpYahooWebpage()
        => _ = Process.Start(new ProcessStartInfo // https://stackoverflow.com/a/61035650/
        {
            FileName = $"https://typhoon.yahoo.co.jp/weather/jp/earthquake/{EarthquakeDetails!.EventId}.html",
            UseShellExecute = true
        });

    #region earthquakeObservationStations
    private IEnumerable<Station>? _earthquakeObservationStations = null;
    private bool IsStationsRetrieved => _earthquakeObservationStations is not null;
    private async Task UpdateEarthquakeObservationStations()
    {
        EarthquakeParameter rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp.ItemList;
    }
    #endregion

    #region loadEarthquakeAction
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoadExtraEnabled))]
    private string _cursorToken = string.Empty;
    public bool IsLoadExtraEnabled => CursorToken != string.Empty;

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        GdEarthquakeList rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 50);
        IEnumerable<EarthquakeInfo> eqList = rsp.ItemList;
        CursorToken = rsp.NextToken ?? string.Empty;

        ObservableCollection<EarthquakeItemTemplate> currentEarthquake = [];

        currentEarthquake.AddRange(eqList.Select(x => new EarthquakeItemTemplate(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));

        SelectedEarthquake = null;
        EarthquakeList = currentEarthquake;
    }

    [RelayCommand]
    private async Task LoadExtraEarthquakes()
    {
        if (CursorToken != string.Empty)
        {
            GdEarthquakeList rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 10, cursorToken: CursorToken);
            IEnumerable<EarthquakeInfo> eqList = rsp.ItemList;
            CursorToken = rsp.NextToken ?? string.Empty;

            EarthquakeList.AddRange(eqList.Select(x => new EarthquakeItemTemplate(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));
        }
    }
    #endregion
}
