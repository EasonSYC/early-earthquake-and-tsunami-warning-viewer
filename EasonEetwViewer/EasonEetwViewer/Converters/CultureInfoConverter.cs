using System.Globalization;
using Avalonia.Data.Converters;

namespace EasonEetwViewer.Converters;
internal class CultureInfoConverter : FuncValueConverter<CultureInfo, string>
{
    public CultureInfoConverter() : base(value
        => value is CultureInfo c
            ? c.Equals(CultureInfo.InvariantCulture)
                ? "English"
                : c.Equals(new CultureInfo("ja-JP"))
                    ? "日本語"
                    : c.Equals(new CultureInfo("zh-CN"))
                        ? "简体中文"
                        : string.Empty
            : string.Empty)
    { }
}
