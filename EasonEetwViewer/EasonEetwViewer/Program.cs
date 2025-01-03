using Avalonia;
using Avalonia.Logging;

namespace EasonEetwViewer;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => _ = BuildAvaloniaApp()
               .StartWithClassicDesktopLifetime(args);

    //public static void Main(string[] args)
    //{
    //    try
    //    {
    //        _ = BuildAvaloniaApp()
    //           .StartWithClassicDesktopLifetime(args);
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(ex, $"Unhandled Exception {ex.GetType().FullName}, {ex.Message}");
    //    }
    //}

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace(LogEventLevel.Warning);
}
