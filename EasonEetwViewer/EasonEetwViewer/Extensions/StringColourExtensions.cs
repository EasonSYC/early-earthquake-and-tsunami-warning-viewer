using Mapsui.Styles;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods to convert a string to a colour.
/// </summary>
internal static class StringColourExtensions
{
    /// <summary>
    /// Converts a string to a colour.
    /// </summary>
    /// <param name="colourString">The string of the color to be converted.</param>
    /// <returns>The converted colour.</returns>
    public static Color ToColour(this string colourString)
        => Color.FromString(colourString);

    /// <summary>
    /// Converts a string to a colour with the specified opacity.
    /// </summary>
    /// <param name="colourString">The string of the color to be converted.</param>
    /// <param name="opacity">The specified opacity.</param>
    /// <returns>The converted colour.</returns>
    public static Color ToColour(this string colourString, float opacity)
        => Color.Opacity(colourString.ToColour(), opacity);
}
