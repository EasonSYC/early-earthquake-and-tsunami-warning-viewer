using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
internal class ViewModelBase : ObservableObject
{
    private protected readonly ILogger<ViewModelBase> _logger;
    public ViewModelBase(ILogger<ViewModelBase> logger)
    {
        _logger = logger;
        _logger.Instantiated();
    }
}