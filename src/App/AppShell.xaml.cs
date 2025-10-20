namespace App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Pages.RegisterPage), typeof(Pages.RegisterPage));
    }
}