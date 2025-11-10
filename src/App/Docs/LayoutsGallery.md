# Layouts Gallery (MAUI)

This gallery demonstrates common .NET MAUI layouts and patterns that you can reuse in pages:

- Grid: star/auto/absolute sizing, column/row span, tile-like content with `CollectionView`.
- Stack: `VerticalStackLayout` and `HorizontalStackLayout` for simple linear layouts.
- FlexLayout: wrapping, justification, direction.
- AbsoluteLayout: fixed vs proportional positioning with `LayoutFlags`.
- RelativeLayout: constraints relative to other views.
- Panels: common page shells like vertical (header/body/footer) and left-nav/content split.
- Breadcrumbs: simple reusable `BreadcrumbView`.
- Tabbed demo: uses CommunityToolkit `TabView` to switch between layout samples.

## Navigation

- The gallery is available from the Flyout as "Layouts" (route `layouts`).
- You can also navigate via Shell route: `await Shell.Current.GoToAsync("//layouts");`

## Breadcrumbs

`Controls/BreadcrumbView` is a minimal reusable control with a `Current` bindable property to show the current leaf text. You can place it in a page or `TitleView`.

## Extend the Gallery

- Add a new `toolkit:TabViewItem` in `Pages/LayoutsGalleryPage.xaml`.
- Create more panel examples (e.g., three-column dashboard, master-detail, cards grid, form layout with labels/fields in a grid).

## Notes

- The gallery uses .NET MAUI Community Toolkit for `TabView`. Make sure `UseMauiCommunityToolkit()` is configured (already present in `MauiProgram.cs`).
- Styles in the page keep tiles uniform across themes using `AppThemeBinding`.
