using System;
using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Services;

namespace nas_FB10_MoodTracker2.Views;

public partial class SettingsPopupPage : ContentPage
{
    private readonly CacheService _cacheService;

    public SettingsPopupPage()
    {
        InitializeComponent();
        // Resolve via DI
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
    }
}
