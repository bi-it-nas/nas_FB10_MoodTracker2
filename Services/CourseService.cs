using nas_FB10_MoodTracker2.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace nas_FB10_MoodTracker2.Services;

public class CourseService
{
    public async Task<List<Course>> GetCoursesAsync()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("courses.json");
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer
            .Deserialize<CourseData>(json, options)?
            .Courses
            ?? new List<Course>();
    }

    private class CourseData
    {
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
