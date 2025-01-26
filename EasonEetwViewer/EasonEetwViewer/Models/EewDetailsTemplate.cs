using CommunityToolkit.Mvvm.ComponentModel;

namespace EasonEetwViewer.Models;
internal class EewDetailsTemplate : ObservableObject
{
    internal DateTimeOffset ExpiryTime { get; }
    internal string EventId { get; }
}
