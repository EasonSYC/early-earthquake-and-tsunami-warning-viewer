using EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation;
using Mapsui;
using Mapsui.Nts.Providers.Shapefile;
using Mapsui.Providers;
using Mapsui.Styles;

namespace EasonEetwViewer.Services;

/// <summary>
/// Provides resources for the application.
/// </summary>
internal sealed class MapResourcesProvider
{
    /// <summary>
    /// The base path to the shape files.
    /// </summary>
    private const string _baseGisFile = "Content/GisFiles/";
    /// <summary>
    /// The shape files for earthquake forecast regions.
    /// </summary>
    public IProvider Region { get; private init; }
        = ShapeFileToProvider("Simp_20240520_AreaForecastLocalE_GIS/Regions.shp");
    /// <summary>
    /// The shape files for tsunami forecast regions.
    /// </summary>
    public IProvider Tsunami { get; private init; }
        = ShapeFileToProvider("Simp_20240520_AreaTsunami_GIS/Tsunamis.shp");

    // Adapted from https://mapsui.com/samples/ - Projection - Shapefile with Projection
    /// <summary>
    /// Reades the shape file from the path and converts to a provider.
    /// </summary>
    /// <param name="shapeFilePath">The path to the shapefile.</param>
    /// <returns>The <see cref="ProjectingProvider"/> that contains the <see cref="ShapeFile"/> read.</returns>
    private static ProjectingProvider ShapeFileToProvider(string shapeFilePath)
        => new(
            new ShapeFile(
            _baseGisFile + shapeFilePath,
            true,
            true)
            { CRS = "EPSG:4326" })
        { CRS = "EPSG:3857" };

    /// <summary>
    /// The style for the epicenter for normal hypocentres.
    /// </summary>
    public IStyle HypocentreShapeStyle { get; private init; }
        = new SymbolStyle
        {
            ImageSource = "embedded://EasonEetwViewer.Resources.cross.svg"
        };
    /// <summary>
    /// The style for the epicenter for assumed hypocentre methods.
    /// </summary>
    public IStyle PlumShapeStyle { get; private init; }
        = new SymbolStyle
        {
            ImageSource = "embedded://EasonEetwViewer.Resources.circle.svg"
        };

    public IEnumerable<PrefectureData> Prefectures { get; private init; } = [
        new PrefectureData() { Code = "01", Name = "北海道" },
        new PrefectureData() { Code = "02", Name = "青森県" },
        new PrefectureData() { Code = "03", Name = "岩手県" },
        new PrefectureData() { Code = "04", Name = "宮城県" },
        new PrefectureData() { Code = "05", Name = "秋田県" },
        new PrefectureData() { Code = "06", Name = "山形県" },
        new PrefectureData() { Code = "07", Name = "福島県" },
        new PrefectureData() { Code = "08", Name = "茨城県" },
        new PrefectureData() { Code = "09", Name = "栃木県" },
        new PrefectureData() { Code = "10", Name = "群馬県" },
        new PrefectureData() { Code = "11", Name = "埼玉県" },
        new PrefectureData() { Code = "12", Name = "千葉県" },
        new PrefectureData() { Code = "13", Name = "東京都" },
        new PrefectureData() { Code = "14", Name = "神奈川県" },
        new PrefectureData() { Code = "15", Name = "新潟県" },
        new PrefectureData() { Code = "16", Name = "富山県" },
        new PrefectureData() { Code = "17", Name = "石川県" },
        new PrefectureData() { Code = "18", Name = "福井県" },
        new PrefectureData() { Code = "19", Name = "山梨県" },
        new PrefectureData() { Code = "20", Name = "長野県" },
        new PrefectureData() { Code = "21", Name = "岐阜県" },
        new PrefectureData() { Code = "22", Name = "静岡県" },
        new PrefectureData() { Code = "23", Name = "愛知県" },
        new PrefectureData() { Code = "24", Name = "三重県" },
        new PrefectureData() { Code = "25", Name = "滋賀県" },
        new PrefectureData() { Code = "26", Name = "京都府" },
        new PrefectureData() { Code = "27", Name = "大阪府" },
        new PrefectureData() { Code = "28", Name = "兵庫県" },
        new PrefectureData() { Code = "29", Name = "奈良県" },
        new PrefectureData() { Code = "30", Name = "和歌山県" },
        new PrefectureData() { Code = "31", Name = "鳥取県" },
        new PrefectureData() { Code = "32", Name = "島根県" },
        new PrefectureData() { Code = "33", Name = "岡山県" },
        new PrefectureData() { Code = "34", Name = "広島県" },
        new PrefectureData() { Code = "35", Name = "山口県" },
        new PrefectureData() { Code = "36", Name = "徳島県" },
        new PrefectureData() { Code = "37", Name = "香川県" },
        new PrefectureData() { Code = "38", Name = "愛媛県" },
        new PrefectureData() { Code = "39", Name = "高知県" },
        new PrefectureData() { Code = "40", Name = "福岡県" },
        new PrefectureData() { Code = "41", Name = "佐賀県" },
        new PrefectureData() { Code = "42", Name = "長崎県" },
        new PrefectureData() { Code = "43", Name = "熊本県" },
        new PrefectureData() { Code = "44", Name = "大分県" },
        new PrefectureData() { Code = "45", Name = "宮崎県" },
        new PrefectureData() { Code = "46", Name = "鹿児島県" },
        new PrefectureData() { Code = "47", Name = "沖縄県" }
    ];
}