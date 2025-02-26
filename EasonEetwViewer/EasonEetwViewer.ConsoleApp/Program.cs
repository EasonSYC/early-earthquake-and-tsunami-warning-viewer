using System.IO.Compression;
using System.Text;
using EasonEetwViewer.Api.Abstractions;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Dtos.Dto.ApiPost;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Response;
using EasonEetwViewer.Dtos.Dto.JsonTelegram.Schema;
using EasonEetwViewer.JmaTravelTime.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Abstractions;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using EasonEetwViewer.Telegram.Abstractions;
using SkiaSharp;
namespace EasonEetwViewer.ConsoleApp;

internal class Program
{
    private static void Main() =>
        //IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();

        //string apiKey = config["ApiKey"] ?? string.Empty;
        //IAuthenticator apiKeyAuth = new ApiKey(apiKey);

        // IConfigurationSection oAuthConfig = config.GetSection("oAuth");
        // string oAuthClientId = oAuthConfig["clientId"] ?? string.Empty;
        // string oAuthBaseUri = oAuthConfig["baseUri"] ?? string.Empty;
        // string oAuthHost = oAuthConfig["host"] ?? string.Empty;
        // HashSet<string> oAuthScopes = oAuthConfig.GetSection("scopes").Get<HashSet<string>>() ?? [];
        // IAuthenticator oAuth = new OAuth(oAuthClientId, oAuthBaseUri, oAuthHost, oAuthScopes);

        //string baseApi = config["BaseApi"] ?? string.Empty;

        //IApiCaller apiCaller = new ApiCaller(baseApi, new() { Authenticator = apiKeyAuth });
        // IApiCaller apiCaller = new(baseApi, oAuth);

        //string baseTelegram = config["BaseTelegram"] ?? string.Empty;

        //ITelegramRetriever telegramRetriever = new TelegramRetriever(baseTelegram, new() { Authenticator = apiKeyAuth });

        // await TestAuthenticator(apiKey);
        // await TestAuthenticator(oAuth);
        // await TestApiCaller(apiCaller);
        //await TestWebSocket(apiCaller);

        // await TestTelegramRetriever(telegramRetriever);

        // await TestKmoni();

        // TestDecode();

        TestTimeTableProvider();

    private static void TestTimeTableProvider()
    {
        int depth = 10;
        ITimeTable timeTable = TimeTableProvider.FromFile("tjma2001.txt");
        for (int i = 0; i <= 180; ++i)
        {
            Console.WriteLine($"{i}: {timeTable.DistanceFromDepthTime(depth, i)}");
        }
    }

    private static void TestDecode()
    {
        string decode = "H4sIAAAAAAAAAI2Sz2sTQRTH/5e5mpSZ2U2yu1e9FEUvwYMiYXbmTTKw2Q07k2opgaRFK2LxIKjFggbUVg8lPYnx4B+zbpr9L5ypifRQoTCHx3uf9+v7Zgd1slx1VcqSTYEiFFAfB+CJwMfYwx4n0gskbragQTjjMQl5IwYiQLCm72MquWz5fhM3gREWQ4C9RsjiwCOMkjhshZQEfsAkBQ4COAOBaqijeQ/6DEU7yGwPwDYFeFxXqczyPjMqSy2zBbl2VoTIBt7AaFRbs+ffXyzGn8ujWXW0X40/lB/PqqcHv39Ni71nxe6PYu+5zTbKJNdltWFmqC28/PqlOnltPW6S9qrZ4Xw5PbE+EMpYnVhyT0rFXaj8dFy9+3Z+Oi1fzhezt8uzaflqZsnBME6U7qm0u0YfolV8PkGPLJCD1reYgbbqu0IU00YdkzqlbYwj9+gDWyeHQZab/3DhX+7GheEWZnkXrgvDFqTm4tiOwoRSHGKMqRMD3I53M6f7SonbKhVXSnkpfv/ytTquRw+YSFRqR0mHSVJDcSa23cGVvsO02bR5KJIs0WCL6Jss5ZDYv7F2GXhiruxZTE7/3a56/3PxZr+YHBeTw2K8i0ajP8lMMtbMAgAA";
        byte[] bytes = Convert.FromBase64String(decode);
        using MemoryStream stream = new(bytes);
        using GZipStream gZipStream = new(stream, CompressionMode.Decompress);
        using MemoryStream outputStream = new();
        gZipStream.CopyTo(outputStream);
        byte[] uncompressed = outputStream.ToArray();
        string decompressed = Encoding.UTF8.GetString(uncompressed);

        Console.WriteLine(decompressed);

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
        Console.WriteLine(earthquakeParameter.ItemList.ToList()[0]);

        GdEarthquakeList pastEarthquakeList = await apiCaller.GetPastEarthquakeListAsync();
        Console.WriteLine(pastEarthquakeList.ResponseId);
        Console.WriteLine(pastEarthquakeList.ResponseStatus);
        Console.WriteLine(pastEarthquakeList.ResponseTime);
        Console.WriteLine(pastEarthquakeList.NextToken);
        Console.WriteLine(pastEarthquakeList.NextPooling);
        Console.WriteLine(pastEarthquakeList.NextPoolingInterval);
        Console.WriteLine(pastEarthquakeList.ItemList.ToList()[0]);

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
            //Types = ["VXSE42", "VXSE45", "VTSE41"],
            TestStatus = TestStatus.Include,
            //AppName = "Test",
            FormatMode = FormatMode.Json
        };
        WebSocketStart response = await apiCaller.PostWebSocketStartAsync(start);
        Console.WriteLine(response);

        IWebSocketClient ws = new WebSocketClient();
        Console.WriteLine("Created!");
        await ws.ConnectAsync(response.WebSockerUrl.Url);
        Console.WriteLine(value: "Connected!");
        await Task.Delay(60000000);
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
        List<(ObservationPoint point, SKColor colour)> colours = pointExtract.ExtractColours(bm).ToList();
        List<(ObservationPoint point, double intensity)> intensities = [];

        foreach ((ObservationPoint p, SKColor c) in colours)
        {
            intensities.Add((p, ColourConversionExtensions.HeightToIntensity(ColourConversionExtensions.ColourToHeight(c))));
        }

        for (int i = 0; i < 100; ++i)
        {
            Console.WriteLine($"{intensities[i].point.Name}, {intensities[i].intensity}, {intensities[i].point.Point.X}, {intensities[i].point.Point.Y}");
        }

        using SKBitmap newBm = new(image.Width, image.Height);

        foreach ((ObservationPoint p, double i) in intensities)
        {
            double height = ColourConversionExtensions.IntensityToHeight(i);
            double hue = ColourConversionExtensions.HeightToHue(height);
            double saturation = ColourConversionExtensions.HeightToSaturation(height) * 100;
            double value = ColourConversionExtensions.HeightToValue(height) * 100;
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