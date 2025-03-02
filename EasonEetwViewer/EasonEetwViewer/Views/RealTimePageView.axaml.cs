using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui
/// <summary>
/// Code-behind for RealtimePageView.axaml
/// </summary>
public partial class RealtimePageView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RealtimePageView"/> class.
    /// </summary>
    public RealtimePageView()
    {
        InitializeComponent();
        MapControl.Map = App.Service!.GetRequiredService<RealtimePageViewModel>().Map;
    }
}