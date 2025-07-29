using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using nas_FB10_MoodTracker2.Models;
using nas_FB10_MoodTracker2.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace nas_FB10_MoodTracker2.ViewModels;

public partial class CourseDetailViewModel : ObservableObject
{
    private readonly CacheService _cacheService;

    public CourseDetailViewModel(CacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [ObservableProperty]
    private Course? _course;
    public double DetailHeightRequest { get; set; }
    public double RatingHeightRequest { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _comment = string.Empty;

    public bool CanSubmit => Course != null && Course.Rating.HasValue;

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private async Task Submit()
    {
        if (Course is null) return;

        Course.Comment = Comment;
        Course.RatingTimestamp = DateTime.Now;

        await SaveToCacheAsync();
        Comment = string.Empty;
    }

    [RelayCommand]
    private async Task Rate(int rating)
    {
        if (Course is null) return;

        Course.Rating = rating;
        Course.Feedback.Hint = rating switch
        {
            1 => "What frustrated you most?",
            2 => "What could be improved?",
            3 => "How was it neutral?",
            4 => "What did you like?",
            5 => "What stood out?",
            _ => string.Empty
        };

        await SaveToCacheAsync();
        SubmitCommand.NotifyCanExecuteChanged();
    }

    private async Task SaveToCacheAsync()
    {
        if (Course == null) return;

        var cache = await _cacheService.LoadAsync();
        cache[Course.Id] = new CacheService.CachedCourseData
        {
            Rating = Course.Rating,
            Comment = Course.Comment
        };
        await _cacheService.SaveAsync(cache);
    }
}
