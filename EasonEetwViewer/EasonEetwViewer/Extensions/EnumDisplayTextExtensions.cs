using System.Diagnostics;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Accuracy;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Models.RealTimePage;

namespace EasonEetwViewer.Extensions;

/// <summary>
/// Provides extension methods for enums to have <c>ToDisplayString</c> method.
/// </summary>
internal static class EnumDisplayTextExtensions
{
    public static string ToDisplayString(this MeasurementType measurementType)
        => measurementType switch
        {
            MeasurementType.MeasuredIntensity
                => SettingPageResources.KmoniMeasurementTextMeasuredIntensity,
            MeasurementType.PeakGroundAcceleration
                => SettingPageResources.KmoniMeasurementTextPeakGroundAcceleration,
            MeasurementType.PeakGroundVelocity
                => SettingPageResources.KmoniMeasurementTextPeakGroundVelocity,
            MeasurementType.PeakGroundDisplacement
                => SettingPageResources.KmoniMeasurementTextPeakGroundDisplacement,
            MeasurementType.Response0125
                => SettingPageResources.KmoniMeasurementTextResponse0125,
            MeasurementType.Response0250
                => SettingPageResources.KmoniMeasurementTextResponse0250,
            MeasurementType.Response0500
                => SettingPageResources.KmoniMeasurementTextResponse0500,
            MeasurementType.Response1000
                => SettingPageResources.KmoniMeasurementTextResponse1000,
            MeasurementType.Response2000
                => SettingPageResources.KmoniMeasurementTextResponse2000,
            MeasurementType.Response4000
                => SettingPageResources.KmoniMeasurementTextResponse4000,
            _
                => throw new UnreachableException(),
        };

    public static string ToDisplayString(this SensorType sensorType)
        => sensorType switch
        {
            SensorType.Surface
                => SettingPageResources.KmoniSensorTextSurface,
            SensorType.Borehole
                => SettingPageResources.KmoniSensorTextBorehole,
            _
                => throw new UnreachableException(),
        };

    public static string ToDisplayString(this AuthenticationStatus authenticationStatus)
        => authenticationStatus switch
        {
            AuthenticationStatus.ApiKey
                => SettingPageResources.AuthenticationStatusTextApiKey,
            AuthenticationStatus.OAuth
                => SettingPageResources.AuthenticationStatusTextOAuth,
            AuthenticationStatus.Null
                => SettingPageResources.AuthenticationStatusTextNull,
            _
                => throw new UnreachableException(),
        };

    public static string ToDisplayString(this TsunamiWarningType tsunamiWarningType)
        => tsunamiWarningType switch
        {
            TsunamiWarningType.None
                => RealtimePageResources.TsunamiWarningTypeTextNone,
            TsunamiWarningType.Information
                => RealtimePageResources.TsunamiWarningTypeTextInformation,
            TsunamiWarningType.Caution
                => RealtimePageResources.TsunamiWarningTypeTextCaution,
            TsunamiWarningType.Warning
                => RealtimePageResources.TsunamiWarningTypeTextWarning,
            TsunamiWarningType.SpecialWarning
                => RealtimePageResources.TsunamiWarningTypeTextSpecialWarning,
            _
                => throw new UnreachableException()
        };

    public static string ToDisplayString(this Intensity intensity)
        => intensity switch
        {
            Intensity.One
                => Resources.EarthquakeIntensityOneText,
            Intensity.Two
                => Resources.EarthquakeIntensityTwoText,
            Intensity.Three
                => Resources.EarthquakeIntensityThreeText,
            Intensity.Four
                => Resources.EarthquakeIntensityFourText,
            Intensity.FiveWeak
                => Resources.EarthquakeIntensityFiveWeakText,
            Intensity.FiveStrong
                => Resources.EarthquakeIntensityFiveStrongText,
            Intensity.SixWeak
                => Resources.EarthquakeIntensitySixWeakText,
            Intensity.SixStrong
                => Resources.EarthquakeIntensitySixStrongText,
            Intensity.Seven
                => Resources.EarthquakeIntensitySevenText,
            _
                => throw new UnreachableException()
        };

    public static string ToDisplayString(this EpicentreDepth epicentreDepth)
#pragma warning disable IDE0072 // Add missing cases
        => epicentreDepth switch
        {
            EpicentreDepth.LevelIpf1Plum
                => RealtimePageResources.EpicentreDepthAccuracy1,
            EpicentreDepth.Ipf2
                => RealtimePageResources.EpicentreDepthAccuracy2,
            EpicentreDepth.Ipf3Or4
                => RealtimePageResources.EpicentreDepthAccuracy3,
            EpicentreDepth.Ipf5OrMore
                => RealtimePageResources.EpicentreDepthAccuracy4,
            EpicentreDepth.Final
                => RealtimePageResources.EpicentreDepthAccuracy9,
            EpicentreDepth.Unknown
                => Resources.UnknownText,
            _
                => throw new UnreachableException(),
        };
#pragma warning restore IDE0072 // Add missing cases

    public static string ToDisplayString(this Magnitude magnitude)
        => magnitude switch
        {
            Magnitude.SpeedMagnitude
                => RealtimePageResources.MagnitudeAccuracy2,
            Magnitude.FullPPhase
                => RealtimePageResources.MagnitudeAccuracy3,
            Magnitude.FullPPhaseMixed
                => RealtimePageResources.MagnitudeAccuracy4,
            Magnitude.FullPointPhase
                => RealtimePageResources.MagnitudeAccuracy5,
            Magnitude.Epos
                => RealtimePageResources.MagnitudeAccuracy6,
            Magnitude.LevelOrPlum
                => RealtimePageResources.MagnitudeAccuracy8,
            Magnitude.Unknown
                => Resources.UnknownText,
            _
                => throw new UnreachableException(),
        };

    public static string ToDisplayString(this MagnitudePoint magnitudePoint)
        => magnitudePoint switch
        {
            MagnitudePoint.OneOrLevelOrPlum
                => RealtimePageResources.Point1,
            MagnitudePoint.Two
                => RealtimePageResources.Point2,
            MagnitudePoint.Three
                => RealtimePageResources.Point3,
            MagnitudePoint.Four
                => RealtimePageResources.Point4,
            MagnitudePoint.FiveOrAbove
                => RealtimePageResources.Point5,
            MagnitudePoint.Unknown
                => Resources.UnknownText,
            _
                => throw new UnreachableException(),
        };
}
