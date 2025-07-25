using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace nas_FB10_MoodTracker2.Models;

public class Course : INotifyPropertyChanged
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // ← Added Location, Date, Time
    public string Location { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;

    public Feedback Feedback { get; set; } = new Feedback();

    private int? _rating;
    public int? Rating
    {
        get => _rating;
        set => SetProperty(ref _rating, value, nameof(Rating), nameof(RatingEmoji));
    }

    private DateTime? _ratingTimestamp;
    public DateTime? RatingTimestamp
    {
        get => _ratingTimestamp;
        set => SetProperty(ref _ratingTimestamp, value, nameof(RatingTimestamp));
    }

    private string _comment = string.Empty;
    public string Comment
    {
        get => _comment;
        set => SetProperty(ref _comment, value, nameof(Comment));
    }

    // ← **New**: Details field for your extended JSON info
    private string _details = string.Empty;
    public string Details
    {
        get => _details;
        set => SetProperty(ref _details, value, nameof(Details));
    }

    public string RatingEmoji => Rating switch
    {
        1 => "😤",
        2 => "😕",
        3 => "😐",
        4 => "🙂",
        5 => "😍",
        _ => string.Empty
    };

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    protected void SetProperty<T>(ref T field, T value, params string[] notify)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            foreach (var prop in notify)
                OnPropertyChanged(prop);
        }
    }
}
