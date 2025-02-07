using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.JmaTravelTime;
/// <summary>
/// Provides static methods to create a JMA TimeTable from.
/// </summary>
internal static partial class JmaTimeTableBuilder
{

    /// <summary>
    /// Create a new instance of <see cref="JmaTimeTable"/> from a file.
    /// </summary>
    /// <param name="fileName">The file that the time table is stored in.</param>
    /// <param name="logger">The logger to be used.</param>
    /// <returns>The instance of <see cref="JmaTimeTable"/> created.</returns>
    /// <exception cref="FormatException">When a line does not have the correct format.</exception>
    /// <remarks>See <see href="https://www.data.jma.go.jp/eqev/data/bulletin/catalog/appendix/trtime/tttfmt_j.html">JMA Webpage</see> for format definition.</remarks>
    public static JmaTimeTable FromFile(string fileName, ILogger<JmaTimeTable> logger)
    {
        string[] rows = File.ReadAllLines(fileName);

        string? unmatchingRow = rows.FirstOrDefault(r => !Pattern().IsMatch(r));
        return unmatchingRow is not null
            ? throw new FormatException($"{unmatchingRow} does not fit format of time table")
            : new JmaTimeTable(rows.Select(row =>
            new TimeTableEntry()
            {
                Depth = int.Parse(row[22..25]),
                Radius = int.Parse(row[27..32]),
                Times = [double.Parse(row[2..10]),
                    double.Parse(row[13..21])]
            }), logger);
    }
    [GeneratedRegex(@"^P [\d\s]{4}.\d{3} S [\d\s]{4}.\d{3} [\d\s]{3}  [\d\s]{5}$")]
    private static partial Regex Pattern();
}
