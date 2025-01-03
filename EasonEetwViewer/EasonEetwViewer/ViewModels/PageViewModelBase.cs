using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;
internal partial class PageViewModelBase(ApplicationOptions options) : ViewModelBase
{
    [ObservableProperty]
    private ApplicationOptions _options = options;
}
