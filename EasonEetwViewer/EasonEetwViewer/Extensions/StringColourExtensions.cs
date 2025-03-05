using Mapsui.Styles;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods to convert a string to a colour.
/// </summary>
internal static class StringColourExtensions
{
    public static Color ToColour(this string colourString)
        => Color.FromString(colourString);
}
