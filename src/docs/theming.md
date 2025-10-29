# Theming Overview

This application supports light, dark, and system-driven themes so that UI colors adapt dynamically at runtime. The approach mirrors Microsofts official .NET MAUI guidance while remaining tailored to this projects structure.

## Architecture

- **Theme resources** live in `App/Resources/Styles/Colors.xaml`. The file defines paired light and dark color keys such as `ButtonTextColorLight` and `ButtonTextColorDark`, plus matching brush resources (for example `PageContainerBackgroundBrushLight`).
- **Control styles** in `App/Resources/Styles/Styles.xaml` consume those theme keys via `AppThemeBinding`. Each setter selects the appropriate color or brush based on the active `UserAppTheme` while preserving existing style structure.
- **Theme orchestration** happens in `App/Services/ThemeService.cs`. The service exposes `ApplyTheme`, `LoadSavedTheme`, and `RegisterSystemThemeListener` to:
  - Persist the user's choice with `Preferences`.
  - Set `Application.Current.UserAppTheme` to `Light`, `Dark`, or `Unspecified` (system).
  - React to OS theme changes whenever the stored preference is `System`.
- **Settings UI** (`Pages/SettingsPage.xaml` + `ViewModels/SettingsViewModel.cs`) binds a picker to the theme options and calls `ThemeService.ApplyTheme` when the selection changes.

## Customization Checklist

1. Add new semantic keys (for example, `CardBackgroundLight/Dark`) to `Colors.xaml`.
2. Use those keys in `Styles.xaml` through `AppThemeBinding` so both light and dark values are referenced.
3. Run `ThemeService.ApplyTheme(AppTheme.System)` if you want to revert to the system appearance.
4. After editing theme resources, perform a `dotnet build` to ensure all referenced keys exist.

## Related Files

- `App/Resources/Styles/Colors.xaml`
- `App/Resources/Styles/Styles.xaml`
- `App/Services/ThemeService.cs`
- `App/ViewModels/SettingsViewModel.cs`
- `App/Pages/SettingsPage.xaml`

## References

- [.NET MAUI Theming sample ThemingDemo](https://github.com/dotnet/maui-samples/tree/main/10.0/UserInterface/ThemingDemo)
- [Theme a .NET MAUI app (Microsoft Learn)](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/theming?view=net-maui-9.0)
