using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
