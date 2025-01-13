using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;

public static class MagnitudeUnitExtensions
{
    public static string ToReadableString(this MagnitudeUnit magnitudeUnit) => magnitudeUnit switch
    {
        MagnitudeUnit.JmaMagnitude => "Mj",
        MagnitudeUnit.NormalMagnitude => "Mw",
        MagnitudeUnit.Unknown or _ => "Unknown"
    };
}
