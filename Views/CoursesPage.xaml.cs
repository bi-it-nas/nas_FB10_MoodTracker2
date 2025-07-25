using System.Linq;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.ViewModels;

namespace nas_FB10_MoodTracker2.Views;

public partial class CoursesPage : ContentPage
{
    private readonly CoursesViewModel _viewModel;

    // Default ctor for Shell/XAML instantiation
    public CoursesPage()
        : this(MauiProgram.Services.GetService<CoursesViewModel>()!)
    {
    }

    // DI ctor
    public CoursesPage(CoursesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
        {
            // Open as modal (popup style) instead of navigating away
            await Navigation.PushModalAsync(new CoursesPopupPage(selectedCourse));
        }

    // Clear selection so item doesn’t stay highlighted
    ((CollectionView)sender).SelectedItem = null;
    }

}
