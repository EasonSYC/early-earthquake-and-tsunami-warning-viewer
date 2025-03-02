using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Services.Kmoni.Dtos;

namespace EasonEetwViewer.Services.Kmoni.Events;

public class KmoniSettingsChangedEventArgs : EventArgs
{
    public required KmoniSettings KmoniSettings { get; init; }
}
