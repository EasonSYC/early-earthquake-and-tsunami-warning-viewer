namespace EasonEetwViewer.Services;
internal sealed class AuthenticationStatusChangedEventArgs : EventArgs
{
    public required string? NewAuthenticatorString { get; init; }
}
