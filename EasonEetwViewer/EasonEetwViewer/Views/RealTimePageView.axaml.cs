using Avalonia.Controls;
using EasonEetwViewer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer.Views;

// Adapted from https://github.com/dev-elian/DemoMapsui
public partial class RealtimePageView : UserControl
{
    private readonly RealtimePageViewModel vm;
    public RealtimePageView()
    {
        InitializeComponent();
        vm = App.Service.GetRequiredService<RealtimePageViewModel>();
        DataContext = vm;
        MapControl.Map = ((RealtimePageViewModel)DataContext).Map;
    }
}