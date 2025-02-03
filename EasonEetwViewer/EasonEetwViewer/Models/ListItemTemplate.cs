using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.Models;
internal record ListItemTemplate
{
    internal ListItemTemplate(Type type, ViewModelBase instance, string iconKey, OnLoadString displayLabel)
    {
        ModelType = type;
        Label = displayLabel;
        Model = instance;

        _ = Application.Current!.TryFindResource(iconKey, out object? res);
        ListItemIcon = (StreamGeometry)res!;
    }

    internal OnLoadString Label { get; init; }
    internal Type ModelType { get; init; }
    internal ViewModelBase Model { get; init; }
    internal StreamGeometry ListItemIcon { get; init; }
}
