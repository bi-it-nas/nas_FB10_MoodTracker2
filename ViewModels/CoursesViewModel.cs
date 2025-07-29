using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace nas_FB10_MoodTracker2.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly CourseService _courseService;
    private readonly CacheService _cacheService;

    [ObservableProperty]
    private bool _isBusy;

    public ObservableCollection<Course> Courses { get; } = new();

    public IAsyncRelayCommand LoadCoursesCommand { get; }

    public CoursesViewModel(CourseService courseService, CacheService cacheService)
    {
        _courseService = courseService;
        _cacheService = cacheService;
        LoadCoursesCommand = new AsyncRelayCommand(LoadCoursesAsync);
        _ = LoadCoursesAsync();
    }

    private async Task LoadCoursesAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        try
        {
            var list = await _courseService.GetCoursesAsync();
            var cache = await _cacheService.LoadAsync();

            Courses.Clear();
            foreach (var c in list)
            {
                // Apply cached rating/comment
                if (cache.TryGetValue(c.Id, out var cached))
                {
                    c.Rating = cached.Rating;
                    c.Comment = cached.Comment;
                }
                Courses.Add(c);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
