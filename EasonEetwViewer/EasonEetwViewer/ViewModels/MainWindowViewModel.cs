using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.ViewModels;

// Adapted from https://github.com/MammaMiaDev/avaloniaui-the-series Episode 3 with edits

internal partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty]
    private ViewModelBase _currentPage = new RealtimePageViewModel();

    [ObservableProperty]
    private ListItemTemplate _selectedListItem = new(typeof(RealtimePageViewModel), "LiveRegular");

    partial void OnSelectedListItemChanged(ListItemTemplate value)
    {
        object instance = Activator.CreateInstance(value.ModelType)!;
        CurrentPage = (ViewModelBase)instance;
    }

    internal ObservableCollection<ListItemTemplate> Items { get; } =
    [
        new ListItemTemplate(typeof(RealtimePageViewModel), "LiveRegular"),
        new ListItemTemplate(typeof(PastPageViewModel), "HistoryRegular"),
        new ListItemTemplate(typeof(SettingPageViewModel), "SettingsRegular"),
    ];

    [RelayCommand]
    private void TriggerPane() => IsPaneOpen ^= true;
}

internal record ListItemTemplate
{
    internal ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        _ = Application.Current!.TryFindResource(iconKey, out object? res);
        ListItemIcon = (StreamGeometry)res!;
    }

    internal string Label { get; init; }
    internal Type ModelType { get; init; }
    internal StreamGeometry ListItemIcon { get; init; }
}
