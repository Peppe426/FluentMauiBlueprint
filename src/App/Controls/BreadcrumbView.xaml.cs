using Microsoft.Maui.Controls;

namespace App.Controls;

public partial class BreadcrumbView : ContentView
{
    public static readonly BindableProperty CurrentProperty = BindableProperty.Create(
        nameof(Current), typeof(string), typeof(BreadcrumbView), default(string));

    public string? Current
    {
        get => (string?)GetValue(CurrentProperty);
        set => SetValue(CurrentProperty, value);
    }

    public BreadcrumbView()
    {
        InitializeComponent();
    }
}
