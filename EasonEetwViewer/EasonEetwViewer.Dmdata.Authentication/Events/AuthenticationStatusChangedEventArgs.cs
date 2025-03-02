using EasonEetwViewer.Dmdata.Authentication.Abstractions;

namespace EasonEetwViewer.Dmdata.Authentication.Events;

/// <summary>
/// The event arguments for when the authentication status changes.
/// </summary>
public class AuthenticationStatusChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new authentication status.
    /// </summary>
    public required AuthenticationStatus AuthenticationStatus { get; init; }
    /// <summary>
    /// The new authentication.
    /// </summary>
    public required string? Authentication { get; init; }
}
