
namespace TechNotes.Application.Notes.GetNoteById;

public class GetNoteQueryHandler : IQueryHandler<GetNoteByIdQuery, NoteResponse?>
{
    private readonly INoteRepository _noteRepository;

    public GetNoteQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Result<NoteResponse?>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetNoteAsync( request.Id );

        if( note is null )
            return Result.Fail<NoteResponse?>("Nota no encontrada!");

        return note.Adapt<NoteResponse>();
    }
}
