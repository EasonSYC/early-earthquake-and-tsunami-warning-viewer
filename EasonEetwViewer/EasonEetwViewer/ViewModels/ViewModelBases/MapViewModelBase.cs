using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Dmdata.Api.Abstractions;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Telegram.Abstractions;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.TimeProvider;
using Mapsui;
using Mapsui.Limiting;
using Mapsui.Projections;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// The base view model for all view models that use a map.
/// </summary>
internal abstract partial class MapViewModelBase : PageViewModelBase
{
    /// <summary>
    /// The map to be used in the view.
    /// </summary>
    [ObservableProperty]
    private Map _map = new();

    // Adapted from https://mapsui.com/samples/ - Navigation - Keep within Extent
    /// <summary>
    /// Creates a new instance of the <see cref="MapViewModelBase"/> class.
    /// </summary>
    /// <param name="resources">The map resources to be used.</param>
    /// <param name="authenticatorWrapper">The authenticator to be used.</param>
    /// <param name="apiCaller">The API caller to be used.</param>
    /// <param name="telegramRetriever">The telegram retriever to be used.</param>
    /// <param name="telegramParser">The telegram parser to be used.</param>
    /// <param name="timeProvider">The time provider to be used.</param>
    /// <param name="logger">The logger to be used.</param>
    public MapViewModelBase(
        MapResourcesProvider resources,
        IAuthenticationHelper authenticatorWrapper,
        IApiCaller apiCaller,
        ITelegramRetriever telegramRetriever,
        ITelegramParser telegramParser,
        ITimeProvider timeProvider,
        ILogger<MapViewModelBase> logger)
        : base(
            authenticatorWrapper,
            apiCaller,
            telegramRetriever,
            telegramParser,
            timeProvider,
            logger)
    {
        _resources = resources;
        _logger = logger;
        InitMapView();
    }

    /// <summary>
    /// The logger to be used;
    /// </summary>
    private readonly ILogger<MapViewModelBase> _logger;
    /// <summary>
    /// The map resources to be used.
    /// </summary>
    private protected MapResourcesProvider _resources;

    /// <summary>
    /// Initialises the view of the map.
    /// </summary>
    private void InitMapView()
    {
        Map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        Map.Navigator.RotationLock = true;
        Map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
        Map.Navigator.OverridePanBounds = _mapBounds;
        Map.Navigator.ZoomToBox(_mainLimitsOfJapan);
        _logger.MapInitialised();
    }

    /// <summary>
    /// Gives a <see cref="MRect"/> of the limits of Japan.
    /// </summary>
    private protected readonly MRect _limitsOfJapan
        = GetMRectFromLonLat(122, 20, 154, 46);
    /// <summary>
    /// Gives a <see cref="MRect"/> of the main limits of Japan.
    /// </summary>
    private protected readonly MRect _mainLimitsOfJapan
        = GetMRectFromLonLat(122, 27, 154, 46);
    /// <summary>
    /// Gives a <see cref="MRect"/> of the map bounds.
    /// </summary>
    private readonly MRect _mapBounds
        = GetMRectFromLonLat(-180, -85, 180, 85);
    /// <summary>
    /// Creates a <see cref="MRect"/> object from pair of longitude and latitude.
    /// </summary>
    /// <param name="minLon">The minimal longitude.</param>
    /// <param name="minLat">The minimal latitude.</param>
    /// <param name="maxLon">The maximal longitude.</param>
    /// <param name="maxLat">The maximal latitude.</param>
    /// <returns>The <see cref="MRect"/> object created with the specific parameters.</returns>
    private static MRect GetMRectFromLonLat(double minLon, double minLat, double maxLon, double maxLat)
    {
        (double minX, double minY) = SphericalMercator.FromLonLat(minLon, minLat);
        (double maxX, double maxY) = SphericalMercator.FromLonLat(maxLon, maxLat);
        return new MRect(minX, minY, maxX, maxY);
    }
}