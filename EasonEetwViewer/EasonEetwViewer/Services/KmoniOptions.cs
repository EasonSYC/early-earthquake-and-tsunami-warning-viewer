using CommunityToolkit.Mvvm.ComponentModel;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.Models.EnumExtensions;

namespace EasonEetwViewer.Services;
internal partial class KmoniOptions : ObservableObject
{
    [ObservableProperty]
    private Tuple<SensorType, string> _sensorChoice;

    [ObservableProperty]
    private Tuple<KmoniDataType, string> _dataChoice;

    internal KmoniOptions(IKmoniDto kmoniDto)
    {
        SensorChoice = new(kmoniDto.SensorChoice, kmoniDto.SensorChoice.ToReadableString());
        DataChoice = new(kmoniDto.DataChoice, kmoniDto.DataChoice.ToReadableString());
    }

    internal KmoniSerialisableOptions ToKmoniSerialisableOptions() => new(SensorChoice.Item1, DataChoice.Item1);
}
