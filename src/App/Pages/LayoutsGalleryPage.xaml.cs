using Microsoft.Maui.Controls;
using App.ViewModels;

namespace App.Pages;

public partial class LayoutsGalleryPage : ContentPage
{
    public LayoutsGalleryPage()
    {
        InitializeComponent();
        BindingContext = new LayoutsGalleryViewModel();
    }
}
