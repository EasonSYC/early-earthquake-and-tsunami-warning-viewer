using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;

namespace EasonEetwViewer.Converters;
internal class CultureInfoConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is CultureInfo c
            ? c.Equals(CultureInfo.InvariantCulture)
                ? "English"
                : c.Equals(new CultureInfo("ja-JP"))
                    ? "日本語"
                    : c.Equals(new CultureInfo("zh-CN"))
                        ? "简体中文"
                        : null
            : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
