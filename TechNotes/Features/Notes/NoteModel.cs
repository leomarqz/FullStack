
namespace TechNotes.Features.Notes;

public class NoteModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Content { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = false;
    public string? UserName { get; set; }
}
