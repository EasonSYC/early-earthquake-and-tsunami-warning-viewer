using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui
public partial class RealtimePageView : UserControl
{
    public RealtimePageView()
    {
        InitializeComponent();
        MapControl.Map = App.Service.GetRequiredService<RealtimePageViewModel>().Map;
    }
}