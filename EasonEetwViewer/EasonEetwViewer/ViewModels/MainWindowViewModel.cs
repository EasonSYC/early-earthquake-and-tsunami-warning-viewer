using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.MainWindow;
using EasonEetwViewer.ViewModels.ViewModelBases;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels;

// Adapted from https://github.com/MammaMiaDev/avaloniaui-the-series Episode 3 with edits
/// <summary>
/// The view model for the main window.
/// </summary>
internal sealed partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// The currently selected view model.
    /// </summary>
    public ViewModelBase CurrentPage
        => SelectedSidebarItem.Model;
    /// <summary>
    /// The currently selected sidebar item.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentPage))]
    private SidebarItemTemplate _selectedSidebarItem;
    /// <summary>
    /// When the selected sidebar item has changed.
    /// </summary>
    /// <param name="oldValue">The previously selected item.</param>
    /// <param name="newValue">The new selected item.</param>
    partial void OnSelectedSidebarItemChanged(SidebarItemTemplate? oldValue, SidebarItemTemplate newValue)
    {
        _logger.ViewModelSwitched(newValue.Model.GetType());
    }
    /// <summary>
    /// Event handler for when data is received on WebSocket.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void RealtimePageViewModel_WebSocketDataReceived(object? sender, EventArgs e)
    {
        SelectedSidebarItem = SidebarItems.ElementAt(0);
        _logger.SwitchingToRealTime();
    }

    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<MainWindowViewModel> _logger;

    /// <summary>
    /// Creates a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    /// <param name="realtimePageViewModel">The realtime page view model instance.</param>
    /// <param name="pastPageViewModel">The past page view model instance.</param>
    /// <param name="settingPageViewModel">The setting page view model instance.</param>
    /// <param name="logger">The logger to be used.</param>
    public MainWindowViewModel(
        RealtimePageViewModel realtimePageViewModel,
        PastPageViewModel pastPageViewModel,
        SettingPageViewModel settingPageViewModel,
        ILogger<MainWindowViewModel> logger)
        : base(logger)
    {
        _logger = logger;
        SidebarItems = [
            new SidebarItemTemplate(
                realtimePageViewModel,
                "LiveRegular",
                Resources.PageNameRealtime),
            new SidebarItemTemplate(
                pastPageViewModel,
                "HistoryRegular",
                Resources.PageNamePast),
            new SidebarItemTemplate(
                settingPageViewModel,
                "SettingsRegular",
                Resources.PageNameSettings),
        ];
        SelectedSidebarItem = SidebarItems.ElementAt(0);
        realtimePageViewModel.WebSocketDataReceived += RealtimePageViewModel_WebSocketDataReceived;
    }

    /// <summary>
    /// The sidebar items.
    /// </summary>
    public IEnumerable<SidebarItemTemplate> SidebarItems { get; init; }
    /// <summary>
    /// Whether the sidebar pane is open.
    /// </summary>
    [ObservableProperty]
    private bool _isPaneOpen = true;
    /// <summary>
    /// To open or close the sidebar pane.
    /// </summary>
    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen ^= true;
        _logger.PaneTriggered(IsPaneOpen);
    }
}