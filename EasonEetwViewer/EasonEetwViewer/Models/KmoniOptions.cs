using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.KyoshinMonitor.Dtos;

namespace EasonEetwViewer.Services.KmoniOptions;
internal partial class KmoniOptions : ObservableObject
{
    [ObservableProperty]
    private SensorType _sensorChoice;

    [ObservableProperty]
    private MeasurementType _dataChoice;

    internal KmoniOptions(IKmoniDto kmoniDto)
    {
        SensorChoice = kmoniDto.SensorChoice;
        DataChoice = kmoniDto.DataChoice;
    }

    internal KmoniSerialisableOptions ToKmoniSerialisableOptions() => new(SensorChoice, DataChoice);
}
