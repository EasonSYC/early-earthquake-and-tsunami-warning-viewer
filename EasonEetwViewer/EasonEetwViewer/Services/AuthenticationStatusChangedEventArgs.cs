using EasonEetwViewer.Authentication;

namespace EasonEetwViewer.Services;
internal class AuthenticationStatusChangedEventArgs : EventArgs
{
    public string? NewAuthenticatorString { get; init; }
}
