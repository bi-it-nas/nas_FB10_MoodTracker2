using Microsoft.Maui.Controls;
using nas_FB10_MoodTracker2.Views;

namespace nas_FB10_MoodTracker2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register the popup page for Shell routing
        Routing.RegisterRoute(nameof(CoursePopupPage), typeof(CoursePopupPage));
    }
}
