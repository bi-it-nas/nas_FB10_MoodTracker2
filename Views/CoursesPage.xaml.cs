using System.Linq;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.ViewModels;
using nas_FB10_MoodTracker2.Views; 

namespace nas_FB10_MoodTracker2.Views;

public partial class CoursesPage : ContentPage
{
    private readonly CoursesViewModel _viewModel;

    // Default ctor for Shell/XAML instantiation
    public CoursesPage()
        : this(MauiProgram.Services.GetService<CoursesViewModel>()!)
    {
    }
    // Open the settings popup
    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        // Push your SettingsPopupPage as a modal
        await Navigation.PushModalAsync(new SettingsPopupPage());
    }


    // DI ctor
    public CoursesPage(CoursesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() is Course selectedCourse)
            {
                await Navigation.PushModalAsync(new CoursesPopupPage(selectedCourse));
            }
            ((CollectionView)sender).SelectedItem = null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[ERROR] {ex}");
            await DisplayAlert("Crash", ex.ToString(), "OK");
        }
    }
    private void OnCloseAppClicked(object sender, EventArgs e)
    {
#if ANDROID
    // immediate exit
    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#else
        // polite back navigation
        Shell.Current.GoToAsync("..");
#endif
    }


}
