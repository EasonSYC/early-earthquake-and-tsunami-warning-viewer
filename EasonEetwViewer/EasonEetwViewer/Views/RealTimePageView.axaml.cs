using Avalonia.Controls;
using EasonEetwViewer.ViewModels;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui
public partial class RealtimePageView : UserControl
{
    private readonly RealtimePageViewModel vm = new();
    public RealtimePageView()
    {
        InitializeComponent();
        DataContext = vm;
        MapControl.Map = ((RealtimePageViewModel)DataContext).Map;
    }
}