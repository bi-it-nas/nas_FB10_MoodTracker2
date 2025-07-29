using System;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Services;
using nas_FB10_MoodTracker2.ViewModels;

namespace nas_FB10_MoodTracker2.Views;

public partial class SettingsPopupPage : ContentPage
{
    private readonly CacheService _cacheService;

    public SettingsPopupPage()
    {
        InitializeComponent();
        _cacheService = MauiProgram.Services.GetService<CacheService>()
                        ?? throw new InvalidOperationException("CacheService not registered");
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnDeleteCacheClicked(object sender, EventArgs e)
    {
        await _cacheService.ClearCacheAsync();
        await DisplayAlert("Cache Cleared", "All saved ratings and comments have been removed.", "OK");

        // Refresh main course list
        if (Application.Current.MainPage is Shell shell &&
            shell.CurrentPage is CoursesPage cp &&
            cp.BindingContext is CoursesViewModel vm)
        {
            await vm.LoadCoursesAsync();
        }
    }

    private void OnExitClicked(object sender, EventArgs e)
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
