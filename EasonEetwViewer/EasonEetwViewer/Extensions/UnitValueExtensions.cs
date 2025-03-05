using EasonEetwViewer.Dmdata.Dtos.DmdataComponent;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Extensions;
/// <summary>
/// Provides extension methods for enums to have <c>ToUnitString</c> and <c>ToValueString</c> method.
/// </summary>
internal static class UnitValueExtensions
{
    /// <summary>
    /// Converts a <see cref="Depth"/> to a unit string.
    /// </summary>
    /// <param name="depth">The value to be converted.</param>
    /// <returns>The converted unit.</returns>
    public static string ToUnitString(this Depth depth)
        => depth.Condition is DepthCondition
            ? string.Empty
            : depth.Unit;
    /// <summary>
    /// Converts a <see cref="Depth"/> to a value string.
    /// </summary>
    /// <param name="depth">The value to be converted.</param>
    /// <returns>The converted value.</returns>
    public static string ToValueString(this Depth depth)
        => depth.Condition is DepthCondition condition
            ? condition.ToDisplayString()
            : depth.Value?.ToString()
                ?? EarthquakeResources.UnknownText;
    /// <summary>
    /// Converts a <see cref="Magnitude"/> to a unit string.
    /// </summary>
    /// <param name="magnitude">The value to be converted.</param>
    /// <returns>The converted unit.</returns>
    public static string ToUnitString(this Magnitude magnitude)
        => magnitude.Condition is MagnitudeCondition
            ? EarthquakeResources.MagnitudeUnitDefault
            : magnitude.Unit.ToDisplayString();
    /// <summary>
    /// Converts a <see cref="Magnitude"/> to a value string.
    /// </summary>
    /// <param name="magnitude">The value to be converted.</param>
    /// <returns>The converted value.</returns>
    public static string ToValueString(this Magnitude magnitude)
        => magnitude.Condition is MagnitudeCondition condition
            ? condition.ToDisplayString()
            : magnitude.Value.ToString()
                ?? EarthquakeResources.UnknownText;

}