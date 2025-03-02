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

/// <summary>
/// The default implementation of <see cref="IKmoniSettingsHelper"/>.
/// </summary>
internal sealed class KmoniSettingsHelper : IKmoniSettingsHelper
{
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<KmoniSettingsHelper> _logger;
    /// <summary>
    /// The settings for kmoni.
    /// </summary>
    private readonly KmoniSettings _settings;

    /// <inheritdoc/>
    public event EventHandler<KmoniSettingsChangedEventArgs>? KmoniSettingsChanged;

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="KmoniSettingsHelper"/>.
    /// </summary>
    /// <param name="settings">The initial settings.</param>
    /// <param name="logger">The logger to be used.</param>
    public KmoniSettingsHelper(KmoniSettings settings, ILogger<KmoniSettingsHelper> logger)
    {
        _settings = settings;
        _logger = logger;
        _logger.Instantiated();
    }
}
