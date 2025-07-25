using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using nas_FB10_MoodTracker2.Models;
using System;

namespace nas_FB10_MoodTracker2.ViewModels;

public partial class CourseDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private Course? _course;
    public double DetailHeightRequest { get; set; }
    public double RatingHeightRequest { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
    private string _comment = string.Empty;

    public bool CanSubmit => Course != null && Course.Rating.HasValue;

    [RelayCommand(CanExecute = nameof(CanSubmit))]
    private void Submit()
    {
        if (Course is null) return;

        Course.Comment = Comment;
        Course.RatingTimestamp = DateTime.Now;
        // clear after submission
        Comment = string.Empty;
    }

    [RelayCommand]
    private void Rate(int rating)
    {
        if (Course is null) return;

        Course.Rating = rating;
        // update hint based on rating type
        Course.Feedback.Hint = rating switch
        {
            1 => "What frustrated you most?",
            2 => "What could be improved?",
            3 => "How was it neutral?",
            4 => "What did you like?",
            5 => "What stood out?",
            _ => string.Empty
        };
        // allow submit now
        SubmitCommand.NotifyCanExecuteChanged();
    }
}
