using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using EasonEetwViewer.Authentication;
using EasonEetwViewer.HttpRequest;
using EasonEetwViewer.Models;
using EasonEetwViewer.Services;
using EasonEetwViewer.Services.KmoniOptions;
using EasonEetwViewer.ViewModels;
using EasonEetwViewer.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer;
public partial class App : Application
{
    private const string _kmoniOptionsPath = "kmoniOptions.json";
    private const string _authenticatorPath = "authenticator.json";
    private const string _apiCallerBaseUri = "https://api.dmdata.jp/v2/";
    private const string _telegramRetrieverBaseUri = "https://data.api.dmdata.jp/v1/";

    /// <inheritdoc/> 
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public static IServiceProvider Service { get; private set; }

    private static KmoniOptions GetKmoniOptions()
    {
        IKmoniDto kmoniDto;
        try
        {
            IKmoniDto? serialisedDto = JsonSerializer.Deserialize<KmoniSerialisableOptions>(File.ReadAllText(_kmoniOptionsPath));
            kmoniDto = serialisedDto is not null ? serialisedDto : new KmoniDefaultOptions();
        }
        catch
        {
            kmoniDto = new KmoniDefaultOptions();
        }

        KmoniOptions kmoniOptions = new(kmoniDto);
        kmoniOptions.PropertyChanged += (s, e)
            => File.WriteAllText(_kmoniOptionsPath, JsonSerializer.Serialize(kmoniOptions.ToKmoniSerialisableOptions()));

        return kmoniOptions;
    }

    private static AuthenticatorDto GetAuthenticatorDto()
    {
        AuthenticatorDto authenticatorDto;

        try
        {
            AuthenticatorDto? serialisedDto = JsonSerializer.Deserialize<AuthenticatorDto>(File.ReadAllText(_authenticatorPath));
            authenticatorDto = serialisedDto is not null ? serialisedDto : new AuthenticatorDto();
        }
        catch
        {
            authenticatorDto = new AuthenticatorDto();
        }

        return authenticatorDto;
    }

    /// <inheritdoc/>
    public override void OnFrameworkInitializationCompleted()
    {
        IServiceCollection collection = new ServiceCollection();

        _ = collection.AddSingleton(GetKmoniOptions());
        _ = collection.AddSingleton(GetAuthenticatorDto());
        _ = collection.AddSingleton(s => new ApiCaller(_apiCallerBaseUri, s.GetRequiredService<AuthenticatorDto>()));
        _ = collection.AddSingleton(s => new TelegramRetriever(_telegramRetrieverBaseUri, s.GetRequiredService<AuthenticatorDto>()));
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