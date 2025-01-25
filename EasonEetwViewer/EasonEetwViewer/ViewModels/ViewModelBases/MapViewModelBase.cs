using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Services;
using Mapsui;
using Mapsui.Projections;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class MapViewModelBase : PageViewModelBase
{
    private protected StaticResources _resources;

    [ObservableProperty]
    private Map _map = new();

    // Adapted from https://mapsui.com/samples/ - Navigation - Keep within Extent
    internal MapViewModelBase(StaticResources resources, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, OnAuthenticatorChanged onChange)
        : base(authenticatorDto, apiCaller, telegramRetriever, onChange)
    {
        _resources = resources;
        InitMapView();
    }

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