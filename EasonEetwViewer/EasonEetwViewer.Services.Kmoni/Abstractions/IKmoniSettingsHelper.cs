using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Services.Kmoni.Events;

namespace EasonEetwViewer.Services.Kmoni.Abstraction;
/// <summary>
/// Represents a helper for managing kmoni settings.
/// </summary>
public interface IKmoniSettingsHelper
{
    /// <summary>
    /// The choice for the measurement.
    /// </summary>
    MeasurementType MeasurementChoice { get; set; }
    /// <summary>
    /// The choice for the sensor.
    /// </summary>
    SensorType SensorChoice { get; set; }
    /// <summary>
    /// The event raised shen the settings changed.
    /// </summary>
    event EventHandler<KmoniSettingsChangedEventArgs>? KmoniSettingsChanged;
}
