using Mapsui.Styles;

namespace EasonEetwViewer.Extensions;

internal static class StringColourExtensions
{
    public static Color ToColour(this string colourString)
        => Color.FromString(colourString);
}
