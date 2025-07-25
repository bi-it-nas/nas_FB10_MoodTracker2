using Microsoft.Extensions.Logging;
using nas_FB10_MoodTracker2.Services;
using nas_FB10_MoodTracker2.ViewModels;
using nas_FB10_MoodTracker2.Views;

namespace nas_FB10_MoodTracker2;

public static class MauiProgram
{
    public static IServiceProvider Services { get; private set; } = null!;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services
        builder.Services.AddSingleton<CourseService>();

        // Register viewmodels
        builder.Services.AddTransient<CoursesViewModel>();
        // Make CourseDetailViewModel a singleton so its Course property is preserved
        builder.Services.AddSingleton<CourseDetailViewModel>();

        // Register views
        builder.Services.AddTransient<CoursesPage>();
        builder.Services.AddTransient<CourseDetailPage>();
        // Register your popup page for DI resolution
        builder.Services.AddTransient<CoursesPopupPage>();

        var app = builder.Build();
        Services = app.Services;

        return app;
    }
}
