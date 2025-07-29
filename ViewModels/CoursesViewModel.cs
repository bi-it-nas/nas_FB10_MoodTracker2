using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nas_FB10_MoodTracker2.ViewModels;

public partial class CoursesViewModel : ObservableObject
{
    private readonly CourseService _courseService;
    private readonly CacheService _cacheService;

    [ObservableProperty]
    private bool _isBusy;

    public bool IsLoaded { get; private set; } = false;

    public ObservableCollection<Course> Courses { get; } = new();

    public IAsyncRelayCommand LoadCoursesCommand { get; }

    public CoursesViewModel(CourseService courseService, CacheService cacheService)
    {
        _courseService = courseService;
        _cacheService = cacheService;
        LoadCoursesCommand = new AsyncRelayCommand(LoadCoursesAsync);
        _ = LoadCoursesAsync();
    }

    public async Task LoadCoursesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var list = await _courseService.GetCoursesAsync();

            Dictionary<string, CacheService.CachedCourseData> cache;
            try
            {
                cache = await _cacheService.LoadAsync();
            }
            catch
            {
                await _cacheService.ClearCacheAsync();
                cache = new Dictionary<string, CacheService.CachedCourseData>();
            }

            Courses.Clear();
            foreach (var c in list)
            {
                if (cache.TryGetValue(c.Id, out var saved))
                {
                    c.Rating = saved.Rating;
                    c.Comment = saved.Comment;
                }
                else
                {
                    c.Rating = null;
                    c.Comment = null;
                }
                Courses.Add(c);
            }

            IsLoaded = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
