using System.Globalization;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.Dmdata.Caller;
using EasonEetwViewer.Dmdata.Caller.Interfaces;
using EasonEetwViewer.Dmdata.Dto.ApiPost;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.KyoshinMonitor;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels;
using EasonEetwViewer.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    private static AuthenticatorDto GetAuthenticatorDto(string authenticatorPath)
    {
        AuthenticatorDto authenticatorDto;

        try
        {
            AuthenticatorDto? serialisedDto = JsonSerializer.Deserialize<AuthenticatorDto>(File.ReadAllText(authenticatorPath));
            authenticatorDto = serialisedDto is not null ? serialisedDto : new AuthenticatorDto();
        }
        catch
        {
            authenticatorDto = new AuthenticatorDto();
        }

        return authenticatorDto;
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
            Classifications = webSocketConfig.GetSection("Classifications").Get<List<Classification>>()!,
            Types = webSocketConfig.GetSection("Types").Get<List<string>>(),
            FormatMode = webSocketConfig.GetSection("FormatMode").Get<FormatMode>(),
            TestStatus = webSocketConfig.GetSection("TestStatus").Get<TestStatus>()
        };

    private static void LanguageChange(CultureInfo language)
    {
        Lang.Resources.Culture = language;
        Lang.PastPageResources.Culture = language;
        Lang.SettingPageResources.Culture = language;
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
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
        string pointExtractPath = configPaths["PointExtract"]!;
        string languagePath = configPaths["Language"]!;

        IConfigurationSection webSocketConfig = config.GetSection("WebSocketParams");

        //IConfigurationSection oAuthConfig = config.GetSection("oAuth");
        //string oAuthClientId = oAuthConfig["clientId"] ?? string.Empty;
        //string oAuthBaseUri = oAuthConfig["baseUri"] ?? string.Empty;
        //string oAuthHost = oAuthConfig["host"] ?? string.Empty;
        //HashSet<string> oAuthScopes = oAuthConfig.GetSection("scopes").Get<HashSet<string>>() ?? [];
        //IAuthenticator oAuth = new OAuth(oAuthClientId, oAuthBaseUri, oAuthHost, oAuthScopes);

        LanguageChange(GetLanguage(languagePath));

        IServiceCollection collection = new ServiceCollection();

        _ = collection.AddSingleton(sp => WebSocketStartParam(webSocketConfig, $"{appName} {appVersion}"));
        _ = collection.AddSingleton(sp => GetKmoniOptions(kmoniOptionsPath));
        _ = collection.AddSingleton(sp => GetAuthenticatorDto(authenticatorPath));

        _ = collection.AddSingleton<OnAuthenticatorChanged>(sp => v => File.WriteAllText(authenticatorPath, JsonSerializer.Serialize(v)));
        _ = collection.AddSingleton<OnLanguageChanged>(sp => s
            =>
                {
                    LanguageChange(s);
                    File.WriteAllText(languagePath, s.Name);
                });

        _ = collection.AddSingleton<IApiCaller>(sp => new ApiCaller(baseApi, sp.GetRequiredService<AuthenticatorDto>()));
        _ = collection.AddSingleton<ITelegramRetriever>(sp => new TelegramRetriever(baseTelegram, sp.GetRequiredService<AuthenticatorDto>()));
        _ = collection.AddSingleton<IWebSocketClient, WebSocketClient>();

        _ = collection.AddSingleton<ImageFetch>();
        _ = collection.AddSingleton(sp => new PointExtract(pointExtractPath));

        _ = collection.AddSingleton<StaticResources>();

        _ = collection.AddSingleton<MainWindowViewModel>();
        _ = collection.AddSingleton<RealtimePageViewModel>();
        _ = collection.AddSingleton<PastPageViewModel>();
        _ = collection.AddSingleton<SettingPageViewModel>();

        Service = collection.BuildServiceProvider();

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