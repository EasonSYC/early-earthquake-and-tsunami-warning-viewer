using System.Globalization;

namespace EasonEetwViewer.Events;

/// <summary>
/// The event arguments for the language changed event.
/// </summary>
internal class LanguagedChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new language that has been set.
    /// </summary>
    public required CultureInfo Language { get; init; }
}
