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
            await Shell.Current.GoToAsync(nameof(Pages.RegisterPage));
        }
    }
}
