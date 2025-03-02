using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer.Models.MainWindow;
/// <summary>
/// Represents a sidebar item.
/// </summary>
internal record SidebarItemTemplate
{
    /// <summary>
    /// Creates a new instance of the <see cref="SidebarItemTemplate"/> record.
    /// </summary>
    /// <param name="instance">The instance for the view model of the page.</param>
    /// <param name="iconKey">The key to the icon.</param>
    /// <param name="displayLabel">The label to display the text for the sidebar.</param>
    public SidebarItemTemplate(
        PageViewModelBase instance,
        string iconKey,
        string displayLabel)
    {
        Label = displayLabel;
        Model = instance;
        ListItemIcon =
            Application.Current!.TryFindResource(iconKey, out object? res)
                ? res as StreamGeometry
                : null;
    }
    /// <summary>
    /// The label to display the text for the sidebar.
    /// </summary>
    public string Label { get; init; }
    /// <summary>
    /// The instance for the view model of the page.
    /// </summary>
    public PageViewModelBase Model { get; init; }
    /// <summary>
    /// The icon to display on the sidebar.
    /// </summary>
    public StreamGeometry? ListItemIcon { get; init; }
}
