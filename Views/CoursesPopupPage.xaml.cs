using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.ViewModels;

namespace nas_FB10_MoodTracker2.Views;

public partial class CoursesPopupPage : ContentPage
{
    public CoursesPopupPage(Course selectedCourse)
    {
        InitializeComponent();

        // Prepare the shared VM
        var vm = MauiProgram.Services.GetService<CourseDetailViewModel>()
                 ?? throw new InvalidOperationException("CourseDetailViewModel not registered");
        vm.Course = selectedCourse;
        vm.Comment = string.Empty;
        BindingContext = vm;

        // Start hidden/off‐screen for animation
        Opacity = 0;
        TranslationY = 100;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Animate in: slide up from +100 to 0 and fade from 0 to 1
        await Task.WhenAll(
            this.FadeTo(1, 300, Easing.CubicIn),
            this.TranslateTo(0, 0, 300, Easing.CubicOut)
        );
    }

    protected override async void OnDisappearing()
    {
        // Animate out: slide down to +100 and fade to 0
        await Task.WhenAll(
            this.FadeTo(0, 200, Easing.CubicOut),
            this.TranslateTo(0, 100, 200, Easing.CubicIn)
        );

        base.OnDisappearing();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync(animated: false); // skip default animation
    }

    private void OnRateClicked(object sender, EventArgs e)
    {
        if (BindingContext is CourseDetailViewModel vm
            && sender is Button btn
            && int.TryParse(btn.CommandParameter?.ToString(), out var rating))
        {
            vm.RateCommand.Execute(rating);
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        if (BindingContext is CourseDetailViewModel vm
            && vm.SubmitCommand.CanExecute(null))
        {
            vm.SubmitCommand.Execute(null);
            await Task.Delay(100);
        }
        await Navigation.PopModalAsync(animated: false);
    }
}
