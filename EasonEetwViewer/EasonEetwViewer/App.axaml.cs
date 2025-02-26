using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using EasonEetwViewer.Authentication.Abstractions;
using EasonEetwViewer.Authentication.Extensions;
using EasonEetwViewer.Dtos.Caller.Interfaces;
using EasonEetwViewer.Dtos.Caller.Services;
using EasonEetwViewer.Dtos.Dto.ApiPost;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.JmaTravelTime.Extensions;
using EasonEetwViewer.KyoshinMonitor.Dtos;
using EasonEetwViewer.KyoshinMonitor.Extensions;
using EasonEetwViewer.Logging;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels;
using EasonEetwViewer.Views;
using EasonEetwViewer.WebSocket.Abstractions;
using EasonEetwViewer.WebSocket.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer;
public partial class App : Application
{
    /// <inheritdoc/> 
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public static IServiceProvider Service { get; private set; }

    private static KmoniOptions GetKmoniOptions(string kmoniOptionsPath)
    {
        IKmoniDto kmoniDto;
        try
        {
            IKmoniDto? serialisedDto = JsonSerializer.Deserialize<KmoniSerialisableOptions>(File.ReadAllText(kmoniOptionsPath));
            kmoniDto = serialisedDto is not null ? serialisedDto : new KmoniDefaultOptions();
        }
        catch
        {
            kmoniDto = new KmoniDefaultOptions();
        }

        // https://stackoverflow.com/a/5822249
        KmoniOptions kmoniOptions = new(kmoniDto);
        kmoniOptions.PropertyChanged += (s, e)
            => File.WriteAllText(kmoniOptionsPath, JsonSerializer.Serialize(kmoniOptions.ToKmoniSerialisableOptions()));

        return kmoniOptions;
    }

    private static CultureInfo GetLanguage(string languagePath)
    {
        CultureInfo culture;
        try
        {
            string languageOption = File.ReadAllText(languagePath);
            culture = new CultureInfo(languageOption);
        }
        catch
        {
            culture = CultureInfo.InvariantCulture;
        }

        return culture;
    }

    private static WebSocketStartPost WebSocketStartParam(IConfigurationSection webSocketConfig, string appName)
        => new()
        {
            AppName = appName,
            Classifications = webSocketConfig.GetSection("Classifications").Get<IEnumerable<Classification>>()!,
            Types = webSocketConfig.GetSection("Types").Get<IEnumerable<string>>(),
            FormatMode = webSocketConfig.GetSection("FormatMode").Get<FormatMode>(),
            TestStatus = webSocketConfig.GetSection("TestStatus").Get<TestStatus>()
        };

    private static void LanguageChange(CultureInfo language)
    {
        Lang.Resources.Culture = language;
        Lang.PastPageResources.Culture = language;
        Lang.SettingPageResources.Culture = language;
        Lang.RealtimePageResources.Culture = language;
    }

    /// <inheritdoc/>
    public override async void OnFrameworkInitializationCompleted()
    {
        IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        IConfigurationSection metaData = config.GetSection("AppMetaInfo");
        string appName = metaData["Name"]!;
        string appVersion = metaData["Version"]!;

        IConfigurationSection baseUrls = config.GetSection("BaseUrls");
        string baseApi = baseUrls["Api"]!;
        string baseTelegram = baseUrls["Telegram"]!;

        IConfigurationSection configPaths = config.GetSection("ConfigPaths");
        string kmoniOptionsPath = configPaths["KmoniOptions"]!;
        string authenticatorPath = configPaths["Authenticator"]!;
        string languagePath = configPaths["Language"]!;

        IConfigurationSection webSocketConfig = config.GetSection("WebSocketParams");

        string timeTablePath = config["TimeTablePath"]!;

        LanguageChange(GetLanguage(languagePath));

        IServiceCollection collection = new ServiceCollection()
            .AddLogging(loggingBuilder => loggingBuilder
                .AddFileLogger(new StreamWriter($"{DateTime.UtcNow:yyyyMMddHHmmss}.log"), LogLevel.Information)
                .AddDebug()
                .SetMinimumLevel(LogLevel.Information))
            .AddSingleton(sp => WebSocketStartParam(webSocketConfig, $"{appName} {appVersion}"))
            .AddSingleton(sp => GetKmoniOptions(kmoniOptionsPath))

            .AddSingleton<OnLanguageChanged>(sp => s
            =>
                {
                    LanguageChange(s);
                    File.WriteAllText(languagePath, s.Name);
                })
            .AddSingleton<ITimeProvider, DefaultTimeProvider>()
            .AddSingleton(sp => new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            })

            .AddSingleton<IApiCaller>(sp => new ApiCaller(baseApi, sp.GetRequiredService<AuthenticationWrapper>(), sp.GetRequiredService<JsonSerializerOptions>()))
            .AddSingleton<ITelegramRetriever>(sp => new TelegramRetriever(baseTelegram, sp.GetRequiredService<AuthenticationWrapper>(), sp.GetRequiredService<JsonSerializerOptions>()))

            .AddSingleton<IWebSocketClient, WebSocketClient>()

            .AddAuthenticationWrapper(authenticatorPath)
            .AddOptions<OAuth2Options>()
            .Bind(config.GetSection("OAuth2Options"))
            .Services
            .AddSingleton<EventHandler<AuthenticationStatusChangedEventArgs>>(sp
                => (o, e)
                => File.WriteAllText(authenticatorPath, e.NewAuthenticatorString))

            .AddKmoniHelpers()
            .AddOptions<KmoniHelperOptions>()
            .Bind(config.GetSection("KmoniHelperOptions"))
            .Services

            .AddJmaTimeTable()
            .AddOptions<JmaTimeTableOptions>()
            .Bind(config.GetSection("TimeTableOptions"))
            .Services

            .AddSingleton<StaticResources>()

            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<RealtimePageViewModel>()
            .AddSingleton<PastPageViewModel>()
            .AddSingleton<SettingPageViewModel>();

        Service = collection
            .BuildServiceProvider()
            .AttachMapsuiLogging();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = Service.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (DataAnnotationsValidationPlugin plugin in dataValidationPluginsToRemove)
        {
            _ = BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}