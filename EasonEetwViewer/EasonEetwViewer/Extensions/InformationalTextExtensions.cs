using System.Text;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods for converting telegram to a information string.
/// </summary>
internal static class InformationalTextExtensions
{
    /// <summary>
    /// Appends a line to the string builder if the value is not null or whitespace.
    /// </summary>
    /// <param name="stringBuilder">The instance of the string builder.</param>
    /// <param name="value">The value to be appended.</param>
    /// <returns>The instance of the string builder, for calls to be chained.</returns>
    private static StringBuilder AppendLineIfVisible(this StringBuilder stringBuilder, string? value)
        => !string.IsNullOrWhiteSpace(value)
            ? stringBuilder.AppendLine(value)
            : stringBuilder;
    /// <summary>
    /// Converts the earthquake information schema telegram to a information string.
    /// </summary>
    /// <param name="earthquake">The <see cref="EarthquakeInformationSchema"/> to be converted.</param>
    /// <returns>The converted information string.</returns>
    public static string ToInformationalString(this EarthquakeInformationSchema earthquake)
        => new StringBuilder()
            .AppendLineIfVisible(earthquake.Headline)
            .AppendLineIfVisible(earthquake.Body.Text)
            .AppendLineIfVisible(earthquake.Body.Comments?.FreeText)
            .AppendLineIfVisible(earthquake.Body.Comments?.Forecast?.Text)
            .AppendLineIfVisible(earthquake.Body.Comments?.Var?.Text)
            .ToString();
    /// <summary>
    /// Converts the tsunami information schema telegram to a information string.
    /// </summary>
    /// <param name="tsunami">The <see cref="TsunamiInformationSchema"/> to be converted.</param>
    /// <returns>The converted information string.</returns>
    public static string ToInformationString(this TsunamiInformationSchema tsunami)
        => new StringBuilder()
            .AppendLineIfVisible(tsunami.Headline)
            .AppendLineIfVisible(tsunami.Body.Text)
            .AppendLineIfVisible(tsunami.Body.Comments?.FreeText)
            .AppendLineIfVisible(tsunami.Body.Comments?.Warning?.Text)
            .ToString();
    /// <summary>
    /// Converts the eew information schema telegram to a information string.
    /// </summary>
    /// <param name="eew">The <see cref="EewInformationSchema"/> to be converted.</param>
    /// <returns>The converted information string.</returns>
    public static string ToInformationString(this EewInformationSchema eew)
        => new StringBuilder()
            .AppendLineIfVisible(eew.Headline)
            .AppendLineIfVisible(eew.Body.Text)
            .AppendLineIfVisible(eew.Body.Comments?.FreeText)
            .AppendLineIfVisible(eew.Body.Comments?.Warning?.Text)
            .ToString();
}
