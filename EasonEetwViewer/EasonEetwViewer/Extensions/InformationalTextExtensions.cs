using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Telegram.Dtos.Schema;

namespace EasonEetwViewer.Extensions;

internal static class InformationalTextExtensions
{
    private static StringBuilder AppendLineIfVisible(this StringBuilder sb, string? value)
        => !string.IsNullOrWhiteSpace(value)
            ? sb.AppendLine(value)
            : sb;
    public static string ToInformationalString(this EarthquakeInformationSchema earthquake)
        => new StringBuilder()
            .AppendLineIfVisible(earthquake.Headline)
            .AppendLineIfVisible(earthquake.Body.Text)
            .AppendLineIfVisible(earthquake.Body.Comments?.FreeText)
            .AppendLineIfVisible(earthquake.Body.Comments?.Forecast?.Text)
            .AppendLineIfVisible(earthquake.Body.Comments?.Var?.Text)
            .ToString();
}
