
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.DeleteNote;

public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;


    public DeleteNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService;
    }
    
    public async Task<Result> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var currentUserCanDelete = await _userService.CurrentUserCanEditNoteAsync( request.Id );

        if( !currentUserCanDelete)
        {
            return Result.Fail("No tienes permisos para eliminar esta nota!");
        }

        var deleted = await _noteRepository.DeleteNoteAsync( request.Id );

        if( deleted ) 
            return Result.Ok();
        
        return Result.Fail("Nota no encontrada o algo salio mal al intentar eliminarla!");
    }
}
