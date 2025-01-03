using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace EasonEetwViewer.ViewModels;
internal class ListItemTemplate
{
    internal ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("PageViewModel", "");

        object instance = Activator.CreateInstance(ModelType)!;
        Model = (ViewModelBase)instance;

        _ = Application.Current!.TryFindResource(iconKey, out object? res);
        ListItemIcon = (StreamGeometry)res!;
    }

    internal string Label { get; init; }
    internal Type ModelType { get; init; }
    internal ViewModelBase Model { get; init; }
    internal StreamGeometry ListItemIcon { get; init; }
}
