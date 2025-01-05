using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;
internal partial class PageViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private ApplicationOptions _options;

    // https://stackoverflow.com/a/5822249

    internal PageViewModelBase(ApplicationOptions options)
    {
        _options = options;
        _options.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OptionPropertyChanged);
    }

    private protected virtual void OptionPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        ;
    }
}
