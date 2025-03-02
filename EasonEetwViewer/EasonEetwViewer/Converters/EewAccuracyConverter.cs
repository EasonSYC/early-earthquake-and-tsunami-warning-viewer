using System.Globalization;
using Avalonia.Data.Converters;
using EasonEetwViewer.Lang;
using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Accuracy;

namespace EasonEetwViewer.Converters;
internal class EewAccuracyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is EpicentreDepth epicentreDepth)
        {
#pragma warning disable IDE0072 // Add missing cases
            return epicentreDepth switch
            {
                EpicentreDepth.LevelIpf1Plum => RealtimePageResources.EpicentreDepthAccuracy1,
                EpicentreDepth.Ipf2 => RealtimePageResources.EpicentreDepthAccuracy2,
                EpicentreDepth.Ipf3Or4 => RealtimePageResources.EpicentreDepthAccuracy3,
                EpicentreDepth.Ipf5OrMore => RealtimePageResources.EpicentreDepthAccuracy4,
                EpicentreDepth.Final => RealtimePageResources.EpicentreDepthAccuracy9,
                EpicentreDepth.Unknown or _ => Resources.UnknownText,
            };
#pragma warning restore IDE0072 // Add missing cases
        }
        else if (value is Magnitude magnitude)
        {
            return magnitude switch
            {
                Magnitude.SpeedMagnitude => RealtimePageResources.MagnitudeAccuracy2,
                Magnitude.FullPPhase => RealtimePageResources.MagnitudeAccuracy3,
                Magnitude.FullPPhaseMixed => RealtimePageResources.MagnitudeAccuracy4,
                Magnitude.FullPointPhase => RealtimePageResources.MagnitudeAccuracy5,
                Magnitude.Epos => RealtimePageResources.MagnitudeAccuracy6,
                Magnitude.LevelOrPlum => RealtimePageResources.MagnitudeAccuracy8,
                Magnitude.Unknown or _ => Resources.UnknownText,
            };
        }
        else if (value is MagnitudePoint magnitudePoint)
        {
            return magnitudePoint switch
            {
                MagnitudePoint.OneOrLevelOrPlum => RealtimePageResources.Point1,
                MagnitudePoint.Two => RealtimePageResources.Point2,
                MagnitudePoint.Three => RealtimePageResources.Point3,
                MagnitudePoint.Four => RealtimePageResources.Point4,
                MagnitudePoint.FiveOrAbove => RealtimePageResources.Point5,
                MagnitudePoint.Unknown or _ => Resources.UnknownText,
            };
        }

        return null;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
