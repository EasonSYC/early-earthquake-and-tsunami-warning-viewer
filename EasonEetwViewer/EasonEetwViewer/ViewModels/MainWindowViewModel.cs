using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;

// Adapted from https://github.com/MammaMiaDev/avaloniaui-the-series Episode 3 with edits

internal sealed partial class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentPage
        => SelectedListItem.Model;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private ListItemTemplate _selectedListItem;

    private new readonly ILogger<MainWindowViewModel> _logger;
    public MainWindowViewModel(
        RealtimePageViewModel rtvm,
        PastPageViewModel ppvm,
        SettingPageViewModel spvm,
        ILogger<MainWindowViewModel> logger)
        : base(logger)
    {
        Items = [
            new ListItemTemplate(
                typeof(RealtimePageViewModel),
                rtvm,
                "LiveRegular",
                () => Resources.PageNameRealtime),
            new ListItemTemplate(
                typeof(PastPageViewModel),
                ppvm,
                "HistoryRegular",
                () => Resources.PageNamePast),
            new ListItemTemplate(
                typeof(SettingPageViewModel),
                spvm,
                "SettingsRegular",
                () => Resources.PageNameSettings),
        ];
        SelectedListItem = Items.ElementAt(0);
        rtvm.WebSocketDataReceived += Rtvm_WebSocketDataReceived;
        _logger = logger;
    }

    private void Rtvm_WebSocketDataReceived(object? sender, EventArgs e)
        => SelectedListItem = Items.ElementAt(0);

    public IEnumerable<ListItemTemplate> Items { get; init; }

    [ObservableProperty]
    private bool _isPaneOpen = true;

    [RelayCommand]
    private void TriggerPane() => IsPaneOpen ^= true;
}