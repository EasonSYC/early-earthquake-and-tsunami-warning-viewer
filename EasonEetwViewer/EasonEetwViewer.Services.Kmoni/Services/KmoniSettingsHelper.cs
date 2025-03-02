using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.Services.Kmoni.Abstraction;
using EasonEetwViewer.Services.Kmoni.Dtos;
using EasonEetwViewer.Services.Kmoni.Events;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services.Kmoni;

internal sealed class KmoniSettingsHelper : IKmoniSettingsHelper
{
    private readonly ILogger<KmoniSettingsHelper> _logger;
    private readonly KmoniSettings _settings;
    public event EventHandler<KmoniSettingsChangedEventArgs>? KmoniSettingsChanged;

    public SensorType SensorChoice
    {
        get
            => _settings.SensorChoice;
        set
        {
            if (_settings.SensorChoice != value)
            {
                _settings.SensorChoice = value;
                KmoniSettingsChanged?.Invoke(this, new() { KmoniSettings = _settings });
                _logger.SensorChoiceChanged(value);
            }
        }
    }

    public MeasurementType MeasurementChoice
    {
        get
            => _settings.MeasurementChoice;
        set
        {
            if (_settings.MeasurementChoice != value)
            {
                _settings.MeasurementChoice = value;
                KmoniSettingsChanged?.Invoke(this, new() { KmoniSettings = _settings });
                _logger.MeasurementChoiceChanged(value);
            }
        }
    }

    public KmoniSettingsHelper(KmoniSettings settings, ILogger<KmoniSettingsHelper> logger)
    {
        _settings = settings;
        _logger = logger;
        _logger.Instantiated();
    }
}
