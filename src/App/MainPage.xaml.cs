namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToRegister(object? sender, EventArgs e)
        {
            // Navigate to the flyout item's route so the left nav highlights the active page
            await Shell.Current.GoToAsync("//register", false);
        }
    }
}
