using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.ViewModels;

namespace nas_FB10_MoodTracker2.Views;

public partial class CoursesPopupPage : ContentPage
{
    // parameterless ctor for Shell
    public CoursesPopupPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // pull the VM from DI so BindingContext is set
        BindingContext = MauiProgram.Services.GetService<CourseDetailViewModel>();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        // close modal routing
        await Shell.Current.GoToAsync("..");
    }

    private void OnRateClicked(object sender, EventArgs e)
    {
        if (BindingContext is CourseDetailViewModel vm &&
            sender is Button btn &&
            int.TryParse(btn.CommandParameter?.ToString(), out var rating))
        {
            vm.RateCommand.Execute(rating);
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        if (BindingContext is CourseDetailViewModel vm &&
            vm.SubmitCommand.CanExecute(null))
        {
            vm.SubmitCommand.Execute(null);
            await Task.Delay(100);
        }
        // close the popup
        await Shell.Current.GoToAsync("..");
    }
}
