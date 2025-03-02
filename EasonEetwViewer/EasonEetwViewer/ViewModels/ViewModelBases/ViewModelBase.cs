using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// The base class for all view models.
/// </summary>
internal abstract class ViewModelBase : ObservableObject
{
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<ViewModelBase> _logger;
    /// <summary>
    /// Creates a new instance of the <see cref="ViewModelBase"/> class.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    public ViewModelBase(ILogger<ViewModelBase> logger)
    {
        _logger = logger;
        _logger.Instantiated();
    }
}