using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Maximize the window on startup (Windows only)
        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS
            events.AddWindows(windows => windows.OnWindowCreated(window =>
            {
                try
                {
                    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
                    var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                    if (appWindow?.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
                    {
                        presenter.Maximize();
                    }
                }
                catch
                {
                    // No-op: if maximizing fails, just continue with normal window
                }
            }));
#endif
        });

        // Register pages/viewmodels for navigation & DI
        builder.Services.AddSingleton<Pages.SettingsPage>();
        builder.Services.AddSingleton<ViewModels.SettingsViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Expose DI container for code-behind/XAML helpers
        ServiceHelper.Initialize(app.Services);

        return app;
    }
}