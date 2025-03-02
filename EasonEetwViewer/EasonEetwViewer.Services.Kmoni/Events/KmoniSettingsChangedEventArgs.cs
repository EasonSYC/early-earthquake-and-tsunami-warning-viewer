using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Services.Kmoni.Dtos;

namespace EasonEetwViewer.Services.Kmoni.Events;
/// <summary>
/// The event arguments for when kmoni settings are changed.
/// </summary>
public class KmoniSettingsChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new settings that are changed to.
    /// </summary>
    public required KmoniSettings KmoniSettings { get; init; }
}
