
using Microsoft.EntityFrameworkCore;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /*
    * Implementations
    */

    public async Task<Note?> GetNoteAsync(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<List<Note>> GetAllNotesAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note?> UpdateNoteAsync(Note note)
    {
        var noteToUpdate = await _context.Notes.FindAsync( note.Id );

        if( noteToUpdate is null)
            return null;

        noteToUpdate.Title = note.Title;
        noteToUpdate.Content = note.Content;
        noteToUpdate.IsPublished = note.IsPublished;
        noteToUpdate.PublishedAt = note.PublishedAt;
        noteToUpdate.UpdatedAt = DateTime.Now;

        // EF hace un seguimiento a menos que se indique lo contrario (AsNoTracking)
        // _context.Notes.Update( noteToUpdate );

        await _context.SaveChangesAsync();

        return noteToUpdate;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var noteToDelete = await _context.Notes.FindAsync( id );

        if( noteToDelete is null )
            return false;

        _context.Notes.Remove( noteToDelete );

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
