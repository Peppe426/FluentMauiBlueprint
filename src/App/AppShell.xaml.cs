namespace App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Ensure BindingContext for localized footer label
        BindingContext = new ViewModels.SettingsViewModel();
        // Register routes
        Routing.RegisterRoute("settings", typeof(Pages.SettingsPage));
    }

    private async void OnUserHeaderTapped(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//user");
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("settings");
    }
}