using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using Mapsui;
using Mapsui.Projections;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class MapViewModelBase : PageViewModelBase
{
    [ObservableProperty]
    private Map _map = new();

    // Adapted from https://mapsui.com/samples/ - Navigation - Keep within Extent
    internal MapViewModelBase(StaticResources resources, KmoniOptions kmoniOptions, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, TelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange)
        : base(resources, kmoniOptions, authenticatorDto, apiCaller, telegramRetriever, onChange) => InitMapView();

    private void InitMapView()
    {
        //MRect bounds = GetLimitsOfJapan();
        MRect view = GetMainLimitsOfJapan();
        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        //Map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
        Map.Navigator.RotationLock = true;
        //Map.Navigator.OverridePanBounds = bounds;
        //Map.Navigator.ZoomToBox(view);
        Map.Home = n => n.ZoomToBox(view);
    }

    private static MRect GetLimitsOfJapan()
    {
        (double minX, double minY) = SphericalMercator.FromLonLat(122, 20);
        (double maxX, double maxY) = SphericalMercator.FromLonLat(154, 46);
        return new MRect(minX, minY, maxX, maxY);
    }

    private static MRect GetMainLimitsOfJapan()
    {
        (double minX, double minY) = SphericalMercator.FromLonLat(122, 27);
        (double maxX, double maxY) = SphericalMercator.FromLonLat(154, 46);
        return new MRect(minX, minY, maxX, maxY);
    }
}