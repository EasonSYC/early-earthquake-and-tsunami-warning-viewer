using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Api.Dtos.Record.EarthquakeParameter;
using EasonEetwViewer.Api.Dtos.Record.GdEarthquake;
using EasonEetwViewer.Api.Dtos.Response;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dtos.Enum;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.Models;
using EasonEetwViewer.Models.RealTimePage;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.TimeProvider;
using EasonEetwViewer.Telegram.Abstractions;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation;
using EasonEetwViewer.Telegram.Dtos.EarthquakeInformation.Enum;
using EasonEetwViewer.Telegram.Dtos.Schema;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;
internal partial class PastPageViewModel(
    MapResourcesProvider resources,
    IAuthenticationHelper authenticatorWrapper,
    IApiCaller apiCaller,
    ITelegramRetriever telegramRetriever,
    ITimeProvider timeProvider,
    ILogger<PastPageViewModel> logger)
    : MapViewModelBase(
        resources,
        authenticatorWrapper,
        apiCaller,
        telegramRetriever,
        timeProvider,
        logger)
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

        Map.Navigator.ZoomToBox(_mainLimitsOfJapan);

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

        GdEarthquakeEvent? rsp = await _apiCaller.GetPathEarthquakeEventAsync(value.EventId);
        IEnumerable<TelegramItem> telegrams = rsp?.EarthquakeEvent.Telegrams ?? [];
        telegrams = telegrams.Where(x => x.TelegramHead.Type == "VXSE53");
        if (telegrams.Count() != 0)
        {
            if (!IsStationsRetrieved)
            {
                await UpdateEarthquakeObservationStations();
            }

            TelegramItem telegram = telegrams.MaxBy(x => x.Serial)!;
            EarthquakeInformationSchema? telegramInfo = await _telegramRetriever.GetJsonTelegramAsync(telegram.Id) as EarthquakeInformationSchema;
            IntensityDetailTree tree = new();

            if (telegramInfo is not null && telegramInfo.Body.Intensity is not null)
            {
                IEnumerable<(Station s, Intensity i)> stationData = telegramInfo.Body.Intensity.Stations
                    .Where(x => (x.MaxInt is IntensityWithUnreceived newInt && newInt.ToEarthquakeIntensity() is Intensity))
                    .Join(_earthquakeObservationStations!,
                    si => si.Code,
                    s => s.XmlCode,
                    (si, s) => (s, (Intensity)((IntensityWithUnreceived)si.MaxInt!).ToEarthquakeIntensity()!));

                IEnumerable<IFeature> stationFeatures = stationData
                    .Select(x =>
                        new PointFeature(SphericalMercator.FromLonLat(x.s.Longitude, x.s.Latitude).ToMPoint())
                        {
                            Styles = [new SymbolStyle()
                            {
                                SymbolScale = 0.25,
                                Fill = new Brush(Color.FromString(x.i.ToColourString())),
                                Outline = new Pen { Color = Color.Black }
                            }]
                        });

                IEnumerable<(Intensity, PositionNode)> intensityAndNodes = stationData
                    .Join(_resources.Prefectures,
                        x => x.s.City.Code[0..2],
                        p => p.Code,
                        (si, p) => (p, si.s, si.i))
                    .Select(x =>
                        (x.i, new PositionNode(x.p.Name, x.p.Code,
                                new(x.s.Region.KanjiName, x.s.Region.Code,
                                    new(x.s.City.KanjiName, x.s.City.Code,
                                        new(x.s.KanjiName, x.s.XmlCode))))));

                tree.AddPositionNode(intensityAndNodes);

                ILayer layer = new MemoryLayer()
                {
                    Name = _obsPointLayerName,
                    Features = stationFeatures,
                    Style = null
                };

                if (!token.IsCancellationRequested)
                {
                    Map.Layers.Add(new RasterizingLayer(CreateRegionLayer(telegramInfo.Body.Intensity.Regions!)));
                    Map.Layers.Add(new RasterizingLayer(layer));
                }

                IEnumerable<MRect?> regions = (await _resources.Region.GetFeaturesAsync(new(new MSection(_limitsOfJapan, 1))))
                    .Join(telegramInfo.Body.Intensity.Regions,
                    f => f["code"]?.ToString()?.ToLower(),
                    r => r.Code,
                    (f, r) => (f.Extent));

                foreach (MRect? region in regions)
                {
                    regionLimits = regionLimits is null ? region : regionLimits.Join(region);
                }
            }

            string informationalText = telegramInfo is null ? string.Empty : ToInformationString(telegramInfo);

            if (!token.IsCancellationRequested)
            {
                EarthquakeDetails = new(
                    value.EventId,
                    value.Intensity,
                    value.OriginTime,
                    value.Hypocentre,
                    value.Magnitude,
                    informationalText,
                    telegramInfo?.ReportDateTime,
                    tree.ToItemControlDisplay());
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
            MRect limits = regionLimits is null ? _mainLimitsOfJapan : regionLimits;
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
        EarthquakeParameter? rsp = await _apiCaller.GetEarthquakeParameterAsync();
        _earthquakeObservationStations = rsp?.ItemList ?? [];
    }
    #endregion

    #region loadEarthquakeAction
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsLoadExtraEnabled))]
    private string? _cursorToken = null;
    public bool IsLoadExtraEnabled
        => CursorToken is not null;

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        GdEarthquakeList? rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 50);

        IEnumerable<EarthquakeInfo> eqList = rsp?.ItemList ?? [];
        CursorToken = rsp?.NextToken ?? null;

        ObservableCollection<EarthquakeItemTemplate> currentEarthquake = [];

        currentEarthquake.AddRange(eqList.Select(x => new EarthquakeItemTemplate(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));

        SelectedEarthquake = null;
        EarthquakeList = currentEarthquake;
    }

    [RelayCommand]
    private async Task LoadExtraEarthquakes()
    {
        if (!string.IsNullOrEmpty(CursorToken))
        {
            GdEarthquakeList? rsp = await _apiCaller.GetPastEarthquakeListAsync(limit: 10, cursorToken: CursorToken);

            IEnumerable<EarthquakeInfo> eqList = rsp?.ItemList ?? [];
            CursorToken = rsp?.NextToken;

            EarthquakeList.AddRange(eqList.Select(x => new EarthquakeItemTemplate(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));
        }
    }
    #endregion
}
