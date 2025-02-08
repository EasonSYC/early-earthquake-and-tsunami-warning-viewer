namespace EasonEetwViewer.Authentication.Abstractions;
/// <summary>
/// Represents the current state of authentication.
/// </summary>
public enum AuthenticationStatus
{
    /// <summary>
    /// With no authentication.
    /// </summary>
    Null = 0,
    /// <summary>
    /// Using an API Key.
    /// </summary>
    ApiKey = 1,
    /// <summary>
    /// Using OAuth.
    /// </summary>
    OAuth = 2
}
