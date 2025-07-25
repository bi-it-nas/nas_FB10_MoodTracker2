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
        if (e.CurrentSelection.FirstOrDefault() is not Course selectedCourse)
            return;

        // prepare detail VM
        var detailVm = MauiProgram.Services.GetService<CourseDetailViewModel>()!;
        detailVm.Course = selectedCourse;
        detailVm.Comment = string.Empty;

        // navigate via Shell routing
        await Shell.Current.GoToAsync(nameof(CoursePopupPage));

        ((CollectionView)sender).SelectedItem = null;
    }
}
