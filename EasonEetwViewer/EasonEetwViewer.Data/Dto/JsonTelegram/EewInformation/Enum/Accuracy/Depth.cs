namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Accuracy;
public enum Depth
{
    Unknown = 0,
    LevelIpf1Plum = 1,
    Ipf2 = 2,
    Ipf3Or4 = 3,
    Ipf5OrMore = 4,
    [Obsolete("No longer used after 2023/09/26 14:00 JST.")]
    Bosai4OrLess = 5,
    [Obsolete("No longer used after 2023/09/26 14:00 JST.")]
    Bosai5OrMoreHinet = 6,
    [Obsolete("No longer used after 2023/09/26 14:00 JST.")]
    EposSea = 7,
    [Obsolete("No longer used after 2023/09/26 14:00 JST.")]
    EposLand = 8
}
