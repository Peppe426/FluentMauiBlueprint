using App.Services;

namespace App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        ThemeService.LoadSavedTheme();
        ThemeService.RegisterSystemThemeListener();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}