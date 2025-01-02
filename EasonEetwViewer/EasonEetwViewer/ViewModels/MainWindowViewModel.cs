using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;

namespace EasonEetwViewer.ViewModels;

// Adapted from https://github.com/MammaMiaDev/avaloniaui-the-series Episode 3 with edits

public partial class MainWindowViewModel : ViewModelBase
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
        CurrentPage = (ViewModelBase) instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } =
    [
        new ListItemTemplate(typeof(RealtimePageViewModel), "LiveRegular"),
        new ListItemTemplate(typeof(PastPageViewModel), "HistoryRegular"),
        new ListItemTemplate(typeof(SettingPageViewModel), "SettingsRegular"),
    ];

    [RelayCommand]
    private void TriggerPane() => IsPaneOpen ^= true;
}

public record ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        _ = Application.Current!.TryFindResource(iconKey, out object? res);
        ListItemIcon = (StreamGeometry)res!;
    }

    public string Label { get; init; }
    public Type ModelType { get; init; }
    public StreamGeometry ListItemIcon { get; init; }
}
