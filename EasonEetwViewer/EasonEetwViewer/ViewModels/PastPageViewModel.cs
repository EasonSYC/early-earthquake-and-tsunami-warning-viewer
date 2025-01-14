using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.HttpRequest.Dto.Record;
using EasonEetwViewer.HttpRequest.Dto.Responses;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;
internal partial class PastPageViewModel : MapViewModelBase
{
    public PastPageViewModel(UserOptions options) : base(options)
    {
        ;
    }

    [ObservableProperty]
    private ObservableCollection<EarthquakeItemTemplate> _earthquakeList = [];

    [ObservableProperty]
    private EarthquakeItemTemplate? _selectedEarthquake;

    private string _cursorToken = string.Empty;

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        PastEarthquakeListResponse rsp = await Options.ApiClient.GetPastEarthquakeListAsync(limit: 50);
        List<EarthquakeInfo> eqList = rsp.ItemList;
        _cursorToken = rsp.NextToken ?? string.Empty;

        ObservableCollection<EarthquakeItemTemplate> currentEarthquake = [];
        eqList.ForEach(x => currentEarthquake.Add(new(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));

        SelectedEarthquake = null;
        EarthquakeList = currentEarthquake;
    }

    [RelayCommand]
    private async Task LoadExtraEarthquakes()
    {
        if (_cursorToken != string.Empty)
        {
            PastEarthquakeListResponse rsp = await Options.ApiClient.GetPastEarthquakeListAsync(limit: 10, cursorToken: _cursorToken);
            List<EarthquakeInfo> eqList = rsp.ItemList;
            _cursorToken = rsp.NextToken ?? string.Empty;

            eqList.ForEach(x => EarthquakeList.Add(new(x.EventId, x.MaxIntensity, x.OriginTime, x.Hypocentre, x.Magnitude)));
        }
    }
}
