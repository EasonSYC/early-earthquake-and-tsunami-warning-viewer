using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Extensions;
using EasonEetwViewer.KyoshinMonitor.Abstractions;

namespace EasonEetwViewer.Converters;

internal class EnumDisplayStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }

        MethodInfo? method = typeof(EnumDisplayTextExtensions)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(method
                => method.Name is "ToDisplayString")
            .Where(method
                => method.GetParameters().Length is 1)
            .SingleOrDefault(method
                => method.GetParameters()[0].ParameterType == value.GetType());

        return method?.Invoke(null, [value]);
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => null;
}
