using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace App.Converters;

public class InitialsConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var name = value as string;
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return string.Empty;

        if (parts.Length == 1)
            return parts[0].Substring(0, 1).ToUpperInvariant();

        var first = parts[0][0];
        var last = parts[^1][0];
        return string.Concat(char.ToUpperInvariant(first), char.ToUpperInvariant(last));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
