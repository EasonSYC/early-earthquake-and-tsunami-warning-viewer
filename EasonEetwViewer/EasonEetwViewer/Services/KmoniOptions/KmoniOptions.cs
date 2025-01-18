using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;

namespace EasonEetwViewer.Services.KmoniOptions;
internal partial class KmoniOptions : ObservableObject
{
    [ObservableProperty]
    private SensorType _sensorChoice;

    [ObservableProperty]
    private KmoniDataType _dataChoice;

    internal KmoniOptions(IKmoniDto kmoniDto)
    {
        SensorChoice = kmoniDto.SensorChoice;
        DataChoice = kmoniDto.DataChoice;
    }

    internal KmoniSerialisableOptions ToKmoniSerialisableOptions() => new(SensorChoice, DataChoice);
}
