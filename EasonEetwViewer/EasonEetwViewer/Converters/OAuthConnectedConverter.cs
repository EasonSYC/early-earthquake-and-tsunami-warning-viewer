﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Converters;
internal class OAuthConnectedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool b
           ? (b ? Resources.SettingsOAuthConnectedButtonText : Resources.SettingsOAuthDisconnectedButtonText)
           : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
