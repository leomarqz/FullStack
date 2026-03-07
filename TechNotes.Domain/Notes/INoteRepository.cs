
namespace TechNotes.Domain.Notes;

public interface INoteRepository 
{
    Task<Note?> GetNoteAsync(int id);
    Task<List<Note>> GetAllNotesAsync();
    Task<Note> CreateNoteAsync(Note note);
    Task<Note?> UpdateNoteAsync(Note note);
    Task<bool> DeleteNoteAsync(int id);
}
