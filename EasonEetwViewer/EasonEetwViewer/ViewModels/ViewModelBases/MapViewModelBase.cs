using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Services;
using Mapsui;
using Mapsui.Limiting;
using Mapsui.Projections;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal partial class MapViewModelBase : PageViewModelBase
{
    private protected StaticResources _resources;

    [ObservableProperty]
    private Map _map = new();

    // Adapted from https://mapsui.com/samples/ - Navigation - Keep within Extent
    internal MapViewModelBase(StaticResources resources, AuthenticatorDto authenticatorDto, IApiCaller apiCaller, ITelegramRetriever telegramRetriever, ITimeProvider timeProvider, ILogger<MapViewModelBase> logger, OnAuthenticatorChanged onChange)
        : base(authenticatorDto, apiCaller, telegramRetriever, timeProvider, logger, onChange)
    {
        _resources = resources;
        InitMapView();
    }
    private void InitMapView()
    {
        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        Map.Navigator.RotationLock = true;
        Map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
        Map.Navigator.OverridePanBounds = GetMapBounds();
        Map.Navigator.ZoomToBox(GetMainLimitsOfJapan());
    }

    private protected static MRect GetLimitsOfJapan()
    {
        (double minX, double minY) = SphericalMercator.FromLonLat(122, 20);
        (double maxX, double maxY) = SphericalMercator.FromLonLat(154, 46);
        return new MRect(minX, minY, maxX, maxY);
    }

    private protected static MRect GetMainLimitsOfJapan()
    {
        (double minX, double minY) = SphericalMercator.FromLonLat(122, 27);
        (double maxX, double maxY) = SphericalMercator.FromLonLat(154, 46);
        return new MRect(minX, minY, maxX, maxY);
    }

    private protected static MRect GetMapBounds() => new(
        SphericalMercator.FromLonLat(-180, 85).x,
        SphericalMercator.FromLonLat(-180, 85).y,
        SphericalMercator.FromLonLat(180, -85).x,
        SphericalMercator.FromLonLat(180, -85).y);

}