using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Models.Enums;

namespace EasonEetwViewer.Converters;

internal class AuthenticationStatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is AuthenticationStatus authStatus
            ? authStatus switch
            {
                AuthenticationStatus.ApiKey => Lang.SettingPageResources.AuthStatusApiKeyText,
                AuthenticationStatus.OAuth => Lang.SettingPageResources.AuthStatusOAuthText,
                AuthenticationStatus.None => Lang.SettingPageResources.AuthStatusNoneText,
                _ => null
            }
            : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
