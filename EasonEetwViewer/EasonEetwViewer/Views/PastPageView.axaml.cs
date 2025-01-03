using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Mapsui.UI.Avalonia;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui

public partial class PastPageView : UserControl
{
    private readonly PastPageViewModel vm = new();
    public PastPageView()
    {
        InitializeComponent();
        DataContext = vm;
        MapControl.Map = ((PastPageViewModel)DataContext).Map;
    }
}