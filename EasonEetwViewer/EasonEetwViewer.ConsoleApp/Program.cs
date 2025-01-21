using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest.Caller;
using EasonEetwViewer.HttpRequest.Dto.ApiPost;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.KyoshinMonitor.Dto;
using EasonEetwViewer.KyoshinMonitor.Dto.Enum;
using EasonEetwViewer.WebSocket;
using Microsoft.Extensions.Configuration;
using SkiaSharp;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    private static async Task Main()
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

        string apiKey = config["ApiKey"] ?? string.Empty;
        IAuthenticator apiKeyAuth = new ApiKey(apiKey);

        // IConfigurationSection oAuthConfig = config.GetSection("oAuth");
        // string oAuthClientId = oAuthConfig["clientId"] ?? string.Empty;
        // string oAuthBaseUri = oAuthConfig["baseUri"] ?? string.Empty;
        // string oAuthHost = oAuthConfig["host"] ?? string.Empty;
        // HashSet<string> oAuthScopes = oAuthConfig.GetSection("scopes").Get<HashSet<string>>() ?? [];
        // IAuthenticator oAuth = new OAuth(oAuthClientId, oAuthBaseUri, oAuthHost, oAuthScopes);

        string baseApi = config["BaseApi"] ?? string.Empty;

        IApiCaller apiCaller = new(baseApi, new() { Authenticator = apiKeyAuth });
        // IApiCaller apiCaller = new(baseApi, oAuth);

        string baseTelegram = config["BaseTelegram"] ?? string.Empty;

        ITelegramRetriever telegramRetriever = new(baseTelegram, new() { Authenticator = apiKeyAuth });

        // await TestAuthenticator(apiKey);
        // await TestAuthenticator(oAuth);
        await TestApiCaller(apiCaller);
        // await TestWebSocket(apiCaller);

        // await TestTelegramRetriever(telegramRetriever);

        // await TestKmoni();
    }

    private static async Task TestAuthenticator(IAuthenticator auth)
    {
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetNewAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetNewAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
        Console.WriteLine(await auth.GetAuthenticationHeader());
    }

    private static async Task TestApiCaller(IApiCaller apiCaller)
    {
        ContractList contractList = await apiCaller.GetContractListAsync();
        Console.WriteLine(contractList);

        WebSocketList webSocketList = await apiCaller.GetWebSocketListAsync();
        Console.WriteLine(webSocketList);

        WebSocketStartPost start = new()
        {
            Classifications = [Classification.EewForecast, Classification.TelegramEarthquake],
            Types = ["VXSE45", "VXSE51", "VXSE52", "VXSE53"],
            TestStatus = TestStatus.Include,
            AppName = "Test",
            FormatMode = FormatMode.Raw
        };
        WebSocketStart response = await apiCaller.PostWebSocketStartAsync(start);
        Console.WriteLine(response);

        await apiCaller.DeleteWebSocketAsync(int.Parse(Console.ReadLine() ?? string.Empty));
        Console.WriteLine("Successfully closed WebSocket connection.");

        EarthquakeParameter earthquakeParameter = await apiCaller.GetEarthquakeParameterAsync();
        Console.WriteLine(earthquakeParameter.ResponseId);
        Console.WriteLine(earthquakeParameter.ResponseStatus);
        Console.WriteLine(earthquakeParameter.ResponseTime);
        Console.WriteLine(earthquakeParameter.Version);
        Console.WriteLine(earthquakeParameter.ChangeTime);
        Console.WriteLine(earthquakeParameter.ItemList[0]);

        GdEarthquakeList pastEarthquakeList = await apiCaller.GetPastEarthquakeListAsync();
        Console.WriteLine(pastEarthquakeList.ResponseId);
        Console.WriteLine(pastEarthquakeList.ResponseStatus);
        Console.WriteLine(pastEarthquakeList.ResponseTime);
        Console.WriteLine(pastEarthquakeList.NextToken);
        Console.WriteLine(pastEarthquakeList.NextPooling);
        Console.WriteLine(pastEarthquakeList.NextPoolingInterval);
        Console.WriteLine(pastEarthquakeList.ItemList[0]);

        GdEarthquakeEvent pastEarthquakeEvent = await apiCaller.GetPathEarthquakeEventAsync("20250112232728");
        Console.WriteLine(pastEarthquakeEvent);
    }

    private static async Task TestTelegramRetriever(ITelegramRetriever telegramRetriever)
    {
        EarthquakeInformationSchema telegramVXSE53 = await telegramRetriever.GetTelegramJsonAsync<EarthquakeInformationSchema>("225612eb1353a21b9ec8585adf5a49252b54a07f4fa667d9f580d5b8ffbeca6de6a7e841674a5c99451b2d7025e6e3c4");
        Console.WriteLine(telegramVXSE53);
    }
    private static async Task TestWebSocket(IApiCaller apiCaller)
    {
        WebSocketStartPost start = new()
        {
            Classifications = [Classification.EewForecast, Classification.TelegramEarthquake],
            Types = ["VXSE45", "VXSE51", "VXSE52", "VXSE53"],
            TestStatus = TestStatus.Include,
            AppName = "Test",
            FormatMode = FormatMode.Raw
        };
        WebSocketStart response = await apiCaller.PostWebSocketStartAsync(start);
        Console.WriteLine(response);

        using WebSocketClient ws = new(response.WebSockerUrl.Url);
        Console.WriteLine("Created!");
        await ws.ConnectAsync();
        Console.WriteLine(value: "Connected!");
        await Task.Delay(60000);
        await ws.DisconnectAsync();
    }

    private static async Task TestKmoni()
    {
        ImageFetch imageFetch = new();
        byte[] imageBytes = await imageFetch.GetByteArrayAsync(MeasurementType.MeasuredIntensity, SensorType.Surface, DateTime.UtcNow);
        using SKImage image = SKImage.FromEncodedData(imageBytes);
        using SKBitmap bm = SKBitmap.FromImage(image);

        using SKData data = bm.Encode(SKEncodedImageFormat.Png, 100);
        File.WriteAllBytes("ObtainedImage.png", data.ToArray());

        SKColor color1 = bm.GetPixel(289, 42);
        color1.ToHsv(out float h, out float s, out float v);
        Console.WriteLine($"{h}, {s}, {v}");

        PointExtract pointExtract = new("ObservationPoints.json");
        pointExtract.WritePoints("OutputObservationPoints.json");
        List<(ObservationPoint point, SKColor colour)> colours = pointExtract.ExtractColours(bm);
        List<(ObservationPoint point, double intensity)> intensities = [];

        foreach ((ObservationPoint p, SKColor c) in colours)
        {
            intensities.Add((p, ColourConversion.HeightToIntensity(ColourConversion.ColourToHeight(c))));
        }

        for (int i = 0; i < 100; ++i)
        {
            Console.WriteLine($"{intensities[i].point.Name}, {intensities[i].intensity}, {intensities[i].point.Point.X}, {intensities[i].point.Point.Y}");
        }

        using SKBitmap newBm = new(image.Width, image.Height);

        foreach ((ObservationPoint p, double i) in intensities)
        {
            double height = ColourConversion.IntensityToHeight(i);
            double hue = ColourConversion.HeightToHue(height);
            double saturation = ColourConversion.HeightToSaturation(height) * 100;
            double value = ColourConversion.HeightToValue(height) * 100;
            Console.WriteLine($"{hue}, {saturation}, {value}");
            SKColor colour = SKColor.FromHsv((float)hue, (float)saturation, (float)value);

            for (int x = p.Point.X - 1; x <= p.Point.X + 1; ++x)
            {
                for (int y = p.Point.Y - 1; y <= p.Point.Y + 1; ++y)
                {
                    if (x >= 0 && x < image.Width && y >= 0 && y < image.Height)
                    {
                        newBm.SetPixel(x, y, colour);
                        Console.WriteLine($"{x}, {y}");
                    }
                }
            }
        }

        using SKData newData = newBm.Encode(SKEncodedImageFormat.Png, 100);
        File.WriteAllBytes("CalculatedImage.png", newData.ToArray());
    }
}