namespace App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        // Apply persisted or system default settings on startup
        _ = new ViewModels.SettingsViewModel();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}