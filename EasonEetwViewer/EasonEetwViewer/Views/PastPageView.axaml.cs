using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Mapsui.UI.Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui

public partial class PastPageView : UserControl
{
    private readonly PastPageViewModel vm;
    public PastPageView()
    {
        InitializeComponent();
        vm = App.Service.GetRequiredService<PastPageViewModel>();
        DataContext = vm;
        MapControl.Map = ((PastPageViewModel)DataContext).Map;
    }
}