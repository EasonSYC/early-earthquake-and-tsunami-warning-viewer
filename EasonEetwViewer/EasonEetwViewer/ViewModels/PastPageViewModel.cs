using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [RelayCommand]
    private async Task RefreshEarthquakeList()
    {
        ;
    }
}
