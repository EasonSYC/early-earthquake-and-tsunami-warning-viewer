using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Accuracy;
public enum Magnitude
{
    Unknown = 0,
    SpeedMagnitude = 2,
    FullPPhase = 3,
    FullPPhaseMixed = 4,
    FullPointPhase = 5,
    Epos = 6,
    LevelOrPlum = 8
}
