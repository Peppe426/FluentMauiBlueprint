using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Services;
using ThemePreference = App.Services.AppTheme;

namespace App.ViewModels;

public class LayoutsGalleryViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<OptionItem> ThemeOptions { get; }

    private OptionItem _selectedThemeOption;
    public OptionItem SelectedThemeOption
    {
        get => _selectedThemeOption;
        set
        {
            if (SetProperty(ref _selectedThemeOption, value))
            {
                ApplyThemePreference(value.Key);
            }
        }
    }

    public string CurrentThemeLabel
    {
        get => $"Current Theme: {_selectedThemeOption?.Display ?? "System"}";
    }

    public LayoutsGalleryViewModel()
    {
        // Initialize theme options
        ThemeOptions = new ObservableCollection<OptionItem>(new[]
        {
            new OptionItem("System", () => "System Default"),
            new OptionItem("Light", () => "Light Mode"),
            new OptionItem("Dark", () => "Dark Mode"),
        });

        // Get the current theme and select it
        var currentTheme = ThemeService.CurrentTheme.ToString();
        _selectedThemeOption = ThemeOptions.FirstOrDefault(o => o.Key == currentTheme) ?? ThemeOptions[0];
    }

    private static void ApplyThemePreference(string key)
    {
        if (!Enum.TryParse<ThemePreference>(key, out var theme))
        {
            theme = ThemePreference.System;
        }

        ThemeService.ApplyTheme(theme);
    }

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;
        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
