namespace App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Ensure BindingContext for localized footer label using shared VM instance
        BindingContext = ServiceHelper.GetService<ViewModels.SettingsViewModel>();
        // Register routes
        Routing.RegisterRoute("settings", typeof(Pages.SettingsPage));
    }

    private async void OnUserHeaderTapped(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//user", false);
    }

    private void OnSettingsClicked(object? sender, EventArgs e)
    {
        // find the hidden Settings FlyoutItem
        var settingsItem = this.Items.FirstOrDefault(i => i.Route == "settings");
        if (settingsItem != null)
        {
            Shell.Current.CurrentItem = settingsItem; // directly switch top-level item
        }
    }


}