using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.ViewModels;

// Adapted from https://github.com/MammaMiaDev/avaloniaui-the-series Episode 3 with edits

internal partial class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentPage => SelectedListItem.Model;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private ListItemTemplate _selectedListItem;

    public MainWindowViewModel() => SelectedListItem = Items[0];

    internal ObservableCollection<ListItemTemplate> Items { get; } =
    [
        new ListItemTemplate(typeof(RealtimePageViewModel), "LiveRegular"),
        new ListItemTemplate(typeof(PastPageViewModel), "HistoryRegular"),
        new ListItemTemplate(typeof(SettingPageViewModel), "SettingsRegular"),
    ];

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [RelayCommand]
    private void TriggerPane() => IsPaneOpen ^= true;
}