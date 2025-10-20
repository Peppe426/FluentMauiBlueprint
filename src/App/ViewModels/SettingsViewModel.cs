using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Storage;
using App.Resources.Strings;

namespace App.ViewModels;

public class SettingsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private const string PrefTheme = "pref_theme"; // values: System, Light, Dark
    private const string PrefLanguage = "pref_language"; // values: System, en, sv, de

    public ObservableCollection<OptionItem> ThemeOptions { get; }
    public ObservableCollection<OptionItem> LanguageOptions { get; }

    private OptionItem _selectedThemeOption;
    public OptionItem SelectedThemeOption
    {
        get => _selectedThemeOption;
        set
        {
            if (SetProperty(ref _selectedThemeOption, value))
            {
                ApplyTheme(MapThemeKeyToSelection(value.Key));
                Preferences.Default.Set(PrefTheme, value.Key);
            }
        }
    }

    private OptionItem _selectedLanguageOption;
    public OptionItem SelectedLanguageOption
    {
        get => _selectedLanguageOption;
        set
        {
            if (SetProperty(ref _selectedLanguageOption, value))
            {
                Preferences.Default.Set(PrefLanguage, value.Key);
                ApplyLanguage(value.Key);
            }
        }
    }

    // Localized titles
    public string SettingsTitle => LangResources.settingsTitle ?? "Settings";
    public string ThemeTitle => LangResources.settingsTheme ?? "Theme";
    public string LanguageTitle => LangResources.settingsLanguage ?? "Language";

    public SettingsViewModel()
    {
        // Initialize options with keys and localized display
        ThemeOptions = new ObservableCollection<OptionItem>(new[]
        {
            new OptionItem("System", () => LangResources.optionSystemDefault ?? "System Default"),
            new OptionItem("Light", () => LangResources.optionLight ?? "Light"),
            new OptionItem("Dark", () => LangResources.optionDark ?? "Dark"),
        });

        LanguageOptions = new ObservableCollection<OptionItem>(new[]
        {
            new OptionItem("System", () => LangResources.optionSystemDefault ?? "System Default"),
            new OptionItem("en", () => LangResources.optionEnglish ?? "English"),
            new OptionItem("sv", () => LangResources.optionSwedish ?? "Swedish"),
            new OptionItem("de", () => LangResources.optionGerman ?? "German"),
        });

        // Load persisted settings (keys)
        var themeKey = Preferences.Default.Get(PrefTheme, "System");
        var langKey = Preferences.Default.Get(PrefLanguage, "System");

        _selectedThemeOption = ThemeOptions.FirstOrDefault(o => o.Key == themeKey) ?? ThemeOptions[0];
        _selectedLanguageOption = LanguageOptions.FirstOrDefault(o => o.Key == langKey) ?? LanguageOptions[0];

        // Apply on startup
        ApplyTheme(MapThemeKeyToSelection(_selectedThemeOption.Key));
        ApplyLanguage(_selectedLanguageOption.Key);
    }

    private static string MapThemeKeyToSelection(string key) => key switch
    {
        "Light" => "Light",
        "Dark" => "Dark",
        _ => "System Default"
    };

    private void ApplyTheme(string selection)
    {
        var app = Application.Current;
        if (app is null) return;
        app.UserAppTheme = selection switch
        {
            "Light" => AppTheme.Light,
            "Dark" => AppTheme.Dark,
            _ => AppTheme.Unspecified // System Default
        };
    }

    private void ApplyLanguage(string pref)
    {
        CultureInfo culture;
        if (pref == "System")
        {
            culture = CultureInfo.CurrentUICulture; // OS language
        }
        else
        {
            try { culture = new CultureInfo(pref); }
            catch { culture = new CultureInfo("en"); }
        }

        // Support only en, sv, de; fallback to en
        var lang = culture.TwoLetterISOLanguageName;
        if (lang is not ("en" or "sv" or "de"))
            lang = "en";

        LangResources.Culture = new CultureInfo(lang);
        // Notify UI that bindings could change
        OnPropertyChanged(nameof(SettingsTitle));
        OnPropertyChanged(nameof(ThemeTitle));
        OnPropertyChanged(nameof(LanguageTitle));

        // Refresh option item displays
        foreach (var o in ThemeOptions) o.NotifyDisplayChanged();
        foreach (var o in LanguageOptions) o.NotifyDisplayChanged();
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

public class OptionItem : INotifyPropertyChanged
{
    public string Key { get; }
    private readonly Func<string> _displayAccessor;
    public OptionItem(string key, Func<string> displayAccessor)
    {
        Key = key;
        _displayAccessor = displayAccessor;
    }

    public string Display => _displayAccessor();

    public event PropertyChangedEventHandler? PropertyChanged;
    public void NotifyDisplayChanged() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Display)));
}
