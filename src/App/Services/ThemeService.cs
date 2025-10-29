using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using NativeAppTheme = Microsoft.Maui.ApplicationModel.AppTheme;

namespace App.Services;

public enum AppTheme
{
    Light,
    Dark,
    System
}

public static class ThemeService
{
    private const string ThemePreferenceKey = "AppTheme";

    public static AppTheme CurrentTheme { get; private set; } = AppTheme.System;

    public static void ApplyTheme(AppTheme theme)
    {
        if (Application.Current is null)
        {
            return;
        }

        if (CurrentTheme == theme && theme != AppTheme.System)
        {
            return;
        }

        Application.Current.UserAppTheme = theme switch
        {
            AppTheme.Light => NativeAppTheme.Light,
            AppTheme.Dark => NativeAppTheme.Dark,
            _ => NativeAppTheme.Unspecified
        };

        CurrentTheme = theme;
        Preferences.Default.Set(ThemePreferenceKey, theme.ToString());
    }

    public static void LoadSavedTheme()
    {
        if (Preferences.Default.ContainsKey(ThemePreferenceKey) &&
            Enum.TryParse(Preferences.Default.Get(ThemePreferenceKey, AppTheme.System.ToString()), out AppTheme saved))
        {
            ApplyTheme(saved);
        }
        else
        {
            ApplyTheme(AppTheme.System);
        }
    }

    public static void RegisterSystemThemeListener()
    {
        if (Application.Current is null)
        {
            return;
        }

        Application.Current.RequestedThemeChanged += (_, _) =>
        {
            var savedValue = Preferences.Default.Get(ThemePreferenceKey, AppTheme.System.ToString());
            if (!Enum.TryParse(savedValue, out AppTheme saved))
            {
                saved = AppTheme.System;
            }

            if (saved == AppTheme.System)
            {
                ApplyTheme(AppTheme.System);
            }
        };
    }
}
