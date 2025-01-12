using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.Models;
internal class ListItemTemplate
{
    internal ListItemTemplate(Type type, ViewModelBase instance, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");
        Model = instance;

        _ = Application.Current!.TryFindResource(iconKey, out object? res);
        ListItemIcon = (StreamGeometry)res!;
    }

    internal string Label { get; init; }
    internal Type ModelType { get; init; }
    internal ViewModelBase Model { get; init; }
    internal StreamGeometry ListItemIcon { get; init; }
}
