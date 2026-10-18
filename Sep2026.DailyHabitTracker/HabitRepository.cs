using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sep2026.DailyHabitTracker;

public sealed class HabitRepository
{
    private string filePath;

    private JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public HabitRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public List<HabitDto> LoadFile()
    {
        if (!File.Exists(filePath))
        {
            return [];
        }

        try
        {
            return JsonSerializer.Deserialize<List<HabitDto>>(File.ReadAllText(filePath), options) ?? [];
        }
        catch (JsonException exception)
        {
            throw new InvalidOperationException($"Please select a valid file path.", exception);
        }
    }

    public void SaveFile(IEnumerable<HabitDto> habits)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var temporaryFile = filePath + ".tmp";
        File.WriteAllText(temporaryFile, JsonSerializer.Serialize(habits, options));
        File.Move(temporaryFile, filePath, true);
    }

}
