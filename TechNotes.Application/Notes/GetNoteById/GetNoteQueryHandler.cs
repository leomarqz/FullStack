
using TechNotes.Domain.User;

namespace TechNotes.Application.Notes.GetNoteById;

public class GetNoteQueryHandler : IQueryHandler<GetNoteByIdQuery, NoteResponse?>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserRepository _userRepository;

    public GetNoteQueryHandler(INoteRepository noteRepository, IUserRepository userRepository)
    {
        _noteRepository = noteRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<NoteResponse?>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        Note? note = await _noteRepository.GetNoteAsync( request.Id );

        if( note is null )
            return Result.Fail<NoteResponse?>("Nota no encontrada!");

        var noteResponse = note.Adapt<NoteResponse>();

        if( note.UserId != null)
        {
           var noteAuthor = await _userRepository.GetUserByIdAsync( note.UserId! );
           noteResponse.UserName = noteAuthor?.UserName ?? "Desconocido";
        }
        else
        {
            noteResponse.UserName = "Desconocido";
        }

        return noteResponse;
    }
}
