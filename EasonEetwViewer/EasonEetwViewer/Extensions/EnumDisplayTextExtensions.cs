using System.Diagnostics;
using EasonEetwViewer.Dmdata.Authentication.Abstractions;
using EasonEetwViewer.Dmdata.Dtos.DmdataComponent.Enum;
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
    /// <summary>
    /// Converts <see cref="MeasurementType"/> to display string.
    /// </summary>
    /// <param name="measurementType">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
    /// <summary>
    /// Converts <see cref="SensorType"/> to display string.
    /// </summary>
    /// <param name="sensorType">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
    /// <summary>
    /// Converts <see cref="AuthenticationStatus"/> to display string.
    /// </summary>
    /// <param name="authenticationStatus">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
    /// <summary>
    /// Converts <see cref="TsunamiWarningType"/> to display string.
    /// </summary>
    /// <param name="tsunamiWarningType">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
    /// <summary>
    /// Converts <see cref="Intensity"/> to display string.
    /// </summary>
    /// <param name="intensity">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToDisplayString(this Intensity intensity)
        => intensity switch
        {
            Intensity.One
                => EarthquakeResources.IntensityTextOne,
            Intensity.Two
                => EarthquakeResources.IntensityTextTwo,
            Intensity.Three
                => EarthquakeResources.IntensityTextThree,
            Intensity.Four
                => EarthquakeResources.IntensityTextFour,
            Intensity.FiveWeak
                => EarthquakeResources.IntensityTextFiveWeak,
            Intensity.FiveStrong
                => EarthquakeResources.IntensityTextFiveStrong,
            Intensity.SixWeak
                => EarthquakeResources.IntensityTextSixWeak,
            Intensity.SixStrong
                => EarthquakeResources.IntensityTextSixStrong,
            Intensity.Seven
                => EarthquakeResources.IntensityTextSeven,
            _
                => throw new UnreachableException()
        };
    /// <summary>
    /// Converts <see cref="EpicentreDepth"/> to display string.
    /// </summary>
    /// <param name="epicentreDepth">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
                => EarthquakeResources.UnknownText,
            _
                => throw new UnreachableException(),
        };
#pragma warning restore IDE0072 // Add missing cases
    /// <summary>
    /// Converts <see cref="Magnitude"/> to display string.
    /// </summary>
    /// <param name="magnitude">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
                => EarthquakeResources.UnknownText,
            _
                => throw new UnreachableException(),
        };
    /// <summary>
    /// Converts <see cref="MagnitudePoint"/> to display string.
    /// </summary>
    /// <param name="magnitudePoint">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
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
                => EarthquakeResources.UnknownText,
            _
                => throw new UnreachableException(),
        };
    /// <summary>
    /// Converts <see cref="DepthCondition"/> to display string.
    /// </summary>
    /// <param name="depthCondition">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToDisplayString(this DepthCondition depthCondition)
        => depthCondition switch
        {
            DepthCondition.Deep
                => EarthquakeResources.DepthDeep,
            DepthCondition.Shallow
                => EarthquakeResources.DepthShallow,
            DepthCondition.Unclear
                => EarthquakeResources.UnknownText,
            _
                => throw new UnreachableException()
        };
    /// <summary>
    /// Converts <see cref="MagnitudeCondition"/> to display string.
    /// </summary>
    /// <param name="magnitudeCondition">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToDisplayString(this MagnitudeCondition magnitudeCondition)
        => magnitudeCondition switch
        {
            MagnitudeCondition.Huge
                => EarthquakeResources.MagnitudeHuge,
            MagnitudeCondition.Unclear
                => EarthquakeResources.UnknownText,
            _
                => throw new UnreachableException()
        };
    /// <summary>
    /// Converts <see cref="MagnitudeUnit"/> to display string.
    /// </summary>
    /// <param name="magnitudeUnit">The enum to be converted.</param>
    /// <returns>The converted display string.</returns>
    /// <exception cref="UnreachableException">When the program reaches an unreachable state.</exception>
    public static string ToDisplayString(this MagnitudeUnit magnitudeUnit)
        => magnitudeUnit switch
        {
            MagnitudeUnit.JmaMagnitude
                => EarthquakeResources.MagnitudeUnitJma,
            MagnitudeUnit.NormalMagnitude
                => EarthquakeResources.MagnitudeUnitMoment,
            _
                => throw new UnreachableException()
        };
}
