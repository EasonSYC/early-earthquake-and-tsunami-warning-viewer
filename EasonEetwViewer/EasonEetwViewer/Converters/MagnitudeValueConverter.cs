using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;
using EasonEetwViewer.HttpRequest.DmdataComponent;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class MagnitudeValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Magnitude magnitude
            ? magnitude.Condition is MagnitudeCondition condition
                ? condition switch
                {
                    MagnitudeCondition.Huge => Resources.EarthquakeMagnitudeHuge,
                    MagnitudeCondition.Unclear or MagnitudeCondition.Unknown or _ => Resources.UnknownText
                }
                : magnitude.Value
            : value is null
                ? Resources.UnknownText
                : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
