using System.Linq;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.ViewModels;
using nas_FB10_MoodTracker2.Views;

namespace nas_FB10_MoodTracker2.Views;

public partial class CoursesPage : ContentPage
{
    private readonly CoursesViewModel _viewModel;

    public CoursesPage()
        : this(MauiProgram.Services.GetService<CoursesViewModel>()!)
    {
    }

    public CoursesPage(CoursesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SettingsPopupPage());
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (!_viewModel.IsLoaded)
            {
                await DisplayAlert("Please wait", "Courses are still loading...", "OK");
                ((CollectionView)sender).SelectedItem = null;
                return;
            }

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
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
        Thread.CurrentThread.Abort();
#else
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
#endif
    }
}
