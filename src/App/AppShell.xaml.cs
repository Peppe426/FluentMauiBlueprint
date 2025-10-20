namespace App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void OnUserHeaderTapped(object? sender, EventArgs e)
    {
        // Navigate to the user page (absolute route to select flyout if present)
        await Shell.Current.GoToAsync("//user");
    }
}