using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Services; // Assuming CacheService is in Services

namespace nas_FB10_MoodTracker2.Views;

public partial class SettingsPopupPage : ContentPage
{
    public SettingsPopupPage()
    {
        InitializeComponent();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void OnDeleteCacheClicked(object sender, EventArgs e)
    {
        CacheService.ClearCache(); // Assuming you have a CacheService
        DisplayAlert("Cache Deleted", "The cache has been successfully deleted.", "OK");
    }
}