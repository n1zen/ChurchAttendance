using System.Text.Json.Serialization;
using ChurchAttendanceApp.Services;

namespace ChurchAttendanceApp.Models;

public class BibleVerse
{
    [JsonPropertyName("translation")]
    public string? Translation { get; set; } = "";

    [JsonPropertyName("book")]
    public int Book { get; set; }

    [JsonPropertyName("chapter")]
    public int Chapter { get; set; }

    [JsonPropertyName("verse")]
    public int Verse { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; } = "";

    public string BookName => BibleVerseService.BookNames.TryGetValue(Book, out var name) ? name : $"Book {Book}";
}
