
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.UpdateNote;

public class UpdateNoteCommandHandler : ICommandHandler<UpdateNoteCommand, NoteResponse?>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;

    public UpdateNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService;
    }

    public async Task<Result<NoteResponse?>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteToUpdate = request.Adapt<Note>();

        var currentUserCanEdit = await _userService.CurrentUserCanEditNoteAsync( noteToUpdate.Id );

        if( !currentUserCanEdit )
            return Result.Fail<NoteResponse?>("No tienes permisos para editar esta nota!");

        var updatedNote = await _noteRepository.UpdateNoteAsync( noteToUpdate );

        if( updatedNote is null )
            return Result.Fail<NoteResponse?>("Nota no encontrada o algo salio mal al intentar actualizarla!");

        return updatedNote.Adapt<NoteResponse>();
    }
}
