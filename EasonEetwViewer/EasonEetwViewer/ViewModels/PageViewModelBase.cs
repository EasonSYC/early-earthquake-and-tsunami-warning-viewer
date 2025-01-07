using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.ViewModels;
internal partial class PageViewModelBase : ViewModelBase
{
    [ObservableProperty]
    private UserOptions _options;

    // https://stackoverflow.com/a/5822249

    internal PageViewModelBase(UserOptions options)
    {
        _options = options;
        _options.PropertyChanged += new PropertyChangedEventHandler(OptionPropertyChanged);
    }

    private protected virtual void OptionPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        ;
    }
}
