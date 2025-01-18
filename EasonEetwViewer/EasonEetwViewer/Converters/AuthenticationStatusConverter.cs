using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using EasonEetwViewer.Models;

namespace EasonEetwViewer.Converters;

internal class AuthenticationStatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is AuthenticationStatus authStatus
            ? authStatus switch
            {
                AuthenticationStatus.ApiKey => Lang.Resources.SettingsAuthStatusApiKeyText,
                AuthenticationStatus.OAuth => Lang.Resources.SettingsAuthStatusOAuthText,
                AuthenticationStatus.None => Lang.Resources.SettingsAuthStatusNoneText,
                _ => null
            }
            : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
