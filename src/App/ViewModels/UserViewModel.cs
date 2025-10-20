using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels;

public partial class UserViewModel : ObservableObject
{
    [ObservableProperty]
    private string userName = "John Doe";

    // Image source for the avatar; can be a file name or URI
    [ObservableProperty]
    private string image = "dotnet_bot.png";
}
