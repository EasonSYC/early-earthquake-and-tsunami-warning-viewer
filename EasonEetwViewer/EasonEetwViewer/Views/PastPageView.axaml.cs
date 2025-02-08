using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui

public partial class PastPageView : UserControl
{
    public PastPageView()
    {
        InitializeComponent();
        MapControl.Map = App.Service.GetRequiredService<PastPageViewModel>().Map;
    }
}