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

    [ObservableProperty]
    private bool _isBusy;

    public ObservableCollection<Course> Courses { get; } = new();

    public IAsyncRelayCommand LoadCoursesCommand { get; }

    public CoursesViewModel(CourseService courseService)
    {
        _courseService = courseService;
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
            Courses.Clear();
            foreach (var c in list)
                Courses.Add(c);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
