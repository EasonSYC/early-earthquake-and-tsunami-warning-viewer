﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.DmdataComponent.Enum;


public static class MagnitudeConditionExtensions
{
    public static string ToReadableString(this MagnitudeCondition magnitudeCondition) => magnitudeCondition switch
    {
        MagnitudeCondition.Huge => "M8+",
        MagnitudeCondition.Unclear => "不明",
        MagnitudeCondition.Unknown or _ => "Unknown"
    };
}
