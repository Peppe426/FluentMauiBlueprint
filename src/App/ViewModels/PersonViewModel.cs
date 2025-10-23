using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels;

public partial class PersonViewModel : ObservableObject
{
    private static readonly Regex PersonalNumberRegex = new(@"^(\d{8}|\d{6})[-\s]\d{4}$|^\d{11}$|^[A-Za-z0-9]{9,12}$", RegexOptions.Compiled);

    private static readonly Regex UsPhoneRegex = new(@"^\(\d{3}\) \d{3}-\d{4}$", RegexOptions.Compiled);

    [ObservableProperty]
    private string? personalNumber;

    [ObservableProperty]
    private string? firstName;

    [ObservableProperty]
    private string? lastName;

    [ObservableProperty]
    private string? phoneNumber;

    private bool IsValidPersonalNumber(string? value) => !string.IsNullOrWhiteSpace(value) && PersonalNumberRegex.IsMatch(value.Trim());

    private static bool IsValidName(string? value) => !string.IsNullOrWhiteSpace(value) && value.Trim().Length >= 2;

    private bool IsValidPhone(string? value) => !string.IsNullOrWhiteSpace(value) && UsPhoneRegex.IsMatch(value.Trim());

    [RelayCommand]
    private async Task SubmitAsync()
    {
        var pnValid = IsValidPersonalNumber(PersonalNumber);
        var fnValid = IsValidName(FirstName);
        var lnValid = IsValidName(LastName);
        var phValid = IsValidPhone(PhoneNumber);

        if (pnValid && fnValid && lnValid && phValid)
        {
            var msg =
                $"Personal Number: {PersonalNumber}\n" +
                $"First Name: {FirstName}\n" +
                $"Last Name: {LastName}\n" +
                $"Phone Number: {PhoneNumber}";

            var page = Application.Current?.Windows?.FirstOrDefault()?.Page;
            if (page is not null)
                await page.DisplayAlert(Resources.Strings.LangResources.alertSubmitTitle, msg, "OK");
        }
        else
        {
            var errors = new List<string>();
            if (!pnValid) errors.Add("• Personal Number is invalid");
            if (!fnValid) errors.Add("• First Name must be at least 2 characters");
            if (!lnValid) errors.Add("• Last Name must be at least 2 characters");
            if (!phValid) errors.Add("• Phone Number must match (XXX) XXX-XXXX");

            var errorText = string.Join("\n", errors);
            var page = Application.Current?.Windows?.FirstOrDefault()?.Page;
            if (page is not null)
                await page.DisplayAlert(Resources.Strings.LangResources.alertValidationTitle, errorText, "OK");
        }
    }

    [RelayCommand]
    private void Clear()
    {
        PersonalNumber = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
    }
}