using nas_FB10_MoodTracker2.ViewModels;
using Microsoft.Maui.Controls;

namespace nas_FB10_MoodTracker2.Views;

public partial class CourseDetailPage : ContentPage
{
    public CourseDetailPage(CourseDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
