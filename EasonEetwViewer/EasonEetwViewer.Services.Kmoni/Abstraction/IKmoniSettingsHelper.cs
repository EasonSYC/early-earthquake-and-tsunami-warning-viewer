using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Services.Kmoni.Events;

namespace EasonEetwViewer.Services.Kmoni.Abstraction;

public interface IKmoniSettingsHelper
{
    MeasurementType MeasurementChoice { get; set; }
    SensorType SensorChoice { get; set; }

    event EventHandler<KmoniSettingsChangedEventArgs>? KmoniSettingsChanged;
}
