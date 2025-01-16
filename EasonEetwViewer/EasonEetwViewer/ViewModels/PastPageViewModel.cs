using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;
using EasonEetwViewer.Models.EnumExtensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using NetTopologySuite.Geometries;

namespace EasonEetwViewer.ViewModels;
internal partial class PastPageViewModel(UserOptions options) : MapViewModelBase(options)
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

    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        _ = Map.Layers.Remove((x => x.Name == _regionLayerName));

        EarthquakeDetails = null;

        if (value is not null)
        {
            if (value.Hypocentre is not null)
            {
                // Add hypocentre display
            }

            PastEarthquakeEventResponse rsp = await Options.ApiClient.GetPathEarthquakeEventAsync(value.EventId);
            List<EarthquakeTelegram> telegrams = rsp.EarthquakeEvent.Telegrams;
            telegrams = telegrams.Where(x => x.TelegramHead.Type == "VXSE53").ToList();
            if (telegrams.Count != 0)
            {
                if (!Options.IsStationsRetrieved)
                {
                    await Options.UpdateEarthquakeObservationStations();
                }

                EarthquakeTelegram telegram = telegrams.MaxBy(x => x.Serial)!;
                EarthquakeInformationSchema telegramInfo = await Options.TelegramRetriever.GetTelegramJsonAsync<EarthquakeInformationSchema>(telegram.Id);
                IntensityDetailTree tree = new();

                if (telegramInfo.Body.Intensity is not null)
                {
                    List<EarthquakeInformationStationData> stationData = telegramInfo.Body.Intensity.Stations;
                    foreach (EarthquakeInformationStationData station in stationData)
                    {
                        if (station.MaxInt is EarthquakeIntensityWithUnreceived newInt && newInt.ToEarthquakeIntensity() != EarthquakeIntensity.Unknown)
                        {
                            List<Station> potnetialStations = Options.EarthquakeObservationStations!.Where(x => x.XmlCode == station.Code).ToList();
                            if (potnetialStations.Count != 0)
                            {
                                Station stationDetails = potnetialStations[0];
                                string prefectureCode = stationDetails.City.Code[0..2];
                                List<PrefectureData> potentialPrefectures = Options.Prefectures.Where(x => x.Code == prefectureCode).ToList();
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

                    Map.Layers.Add(new RasterizingLayer(CreateRegionLayer(telegramInfo.Body.Intensity.Regions!)));

                }

                EarthquakeDetails = new(value.EventId, value.Intensity, value.OriginTime, value.Hypocentre, value.Magnitude, "Test", telegramInfo.ReportDateTime, tree);
            }
        }
    }

    private ILayer CreateRegionLayer(List<EarthquakeInformationRegionData> regions)
        => new Layer()
        {
            Name = _regionLayerName,
            DataSource = Options.PastRegion,
            Style = CreateThemeStyle(regions),
            IsMapInfoLayer = true
        };


    // Adapted from https://mapsui.com/v5/samples/ - Styles - ThemeStyle on ShapeFile
    private IThemeStyle CreateThemeStyle(List<EarthquakeInformationRegionData> regions)
        => new ThemeStyle(f =>
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
    private void JumpYahooWebpage() => _ = Process.Start(new ProcessStartInfo // https://stackoverflow.com/a/61035650/
    {
        FileName = $"https://typhoon.yahoo.co.jp/weather/jp/earthquake/{EarthquakeDetails!.EventId}.html",
        UseShellExecute = true
    });

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        PastEarthquakeListResponse rsp = await Options.ApiClient.GetPastEarthquakeListAsync(limit: 50);
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
            PastEarthquakeListResponse rsp = await Options.ApiClient.GetPastEarthquakeListAsync(limit: 10, cursorToken: CursorToken);
            List<EarthquakeInfo> eqList = rsp.ItemList;
            CursorToken = rsp.NextToken ?? string.Empty;

            eqList.ForEach(x => EarthquakeList.Add(new(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));
        }
    }
}
