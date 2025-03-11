using Avalonia;
using Avalonia.Logging;
using EasonEetwViewer.Services.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace EasonEetwViewer;
/// <summary>
/// The entry point of the application.
/// </summary>
internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns>The exit code.</returns>
    [STAThread]
    public static int Main(string[] args)
        => BuildAvaloniaApp()
           .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    /// <summary>
    /// The Avalonia configuration.
    /// </summary>
    /// <returns>The <see cref="AppBuilder"/>.</returns>
    public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont();
}
