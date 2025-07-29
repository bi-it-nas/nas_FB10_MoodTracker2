using System.Text.Json;

namespace nas_FB10_MoodTracker2.Services;

public class CacheService
{
    private readonly string _cacheFilePath;

    public CacheService()
    {
        _cacheFilePath = Path.Combine(FileSystem.AppDataDirectory, "courses_cache.json");
    }

    public async Task<Dictionary<string, CachedCourseData>> LoadAsync()
    {
        if (!File.Exists(_cacheFilePath))
            return new Dictionary<string, CachedCourseData>();

        var json = await File.ReadAllTextAsync(_cacheFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, CachedCourseData>>(json)
               ?? new Dictionary<string, CachedCourseData>();
    }

    public async Task SaveAsync(Dictionary<string, CachedCourseData> cache)
    {
        var json = JsonSerializer.Serialize(cache);
        await File.WriteAllTextAsync(_cacheFilePath, json);
    }

    public async Task ClearCacheAsync()
    {
        if (File.Exists(_cacheFilePath))
            File.Delete(_cacheFilePath);
    }

    public class CachedCourseData
    {
        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
