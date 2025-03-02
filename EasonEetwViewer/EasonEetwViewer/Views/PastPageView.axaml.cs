using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui
/// <summary>
/// Code-behind for PastPageView.axaml
/// </summary>
public partial class PastPageView : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PastPageView"/> class.
    /// </summary>
    public PastPageView()
    {
        InitializeComponent();
        MapControl.Map = App.Service!.GetRequiredService<PastPageViewModel>().Map;
    }
}