using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;
using EasonEetwViewer.HttpRequest.Dto.Enum;
using System.Diagnostics;

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
    async partial void OnSelectedEarthquakeChanged(EarthquakeItemTemplate? value)
    {
        // draw the epicentre mark on the map

        if (value is not null)
        {
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
                        if (station.MaxInt is EarthquakeIntensity newInt && station.MaxInt != EarthquakeIntensity.Unknown)
                        {
                            Station stationDetails = Options.EarthquakeObservationStations!.Where(x => x.XmlCode == station.Code).ToList()[0];
                            string prefectureCode = stationDetails.City.Code[0..2];
                            PrefectureData prefectureData = Options.Prefectures.Where(x => x.Code == prefectureCode).ToList()[0];
                            tree.AddPositionNode(newInt,
                                new(prefectureData.Name, prefectureData.Code,
                                    new(stationDetails.Region.KanjiName, stationDetails.Region.Code,
                                        new(stationDetails.City.KanjiName, stationDetails.City.Code,
                                            new(stationDetails.KanjiName, stationDetails.XmlCode)))));
                        }
                    }
                }

                // draw the primary area mark on the map

                EarthquakeDetails = new(value.Intensity, value.OriginTime, value.Hypocentre, value.Magnitude, "Test", telegramInfo.ReportDateTime, tree);
            }
            else
            {
                EarthquakeDetails = null;
            }
        }
        else
        {
            EarthquakeDetails = null;
        }
    }

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
