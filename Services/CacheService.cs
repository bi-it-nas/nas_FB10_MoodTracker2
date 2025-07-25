using Microsoft.Maui.Storage;

namespace nas_FB10_MoodTracker2.Services;

public static class CacheService
{
    private const string CacheKey = "CourseFeedbackCache";

    public static void ClearCache()
    {
        Preferences.Remove(CacheKey); // Clears cache stored in Preferences
        // Add additional cache-clearing logic if needed (e.g., file deletion)
    }
}