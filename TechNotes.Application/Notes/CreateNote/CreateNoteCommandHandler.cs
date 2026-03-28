
using TechNotes.Application.Exceptions;
using TechNotes.Application.Users;

namespace TechNotes.Application.Notes.CreateNote;

public class CreateNoteCommandHandler : ICommandHandler<CreateNoteCommand, NoteResponse>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUserService _userService;

    public CreateNoteCommandHandler(INoteRepository noteRepository, IUserService userService)
    {
        _noteRepository = noteRepository;
        _userService = userService; //new
    }

    public async Task<Result<NoteResponse>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Note newNote = request.Adapt<Note>();
            var userId = await _userService.GetCurrentUserIdAsync();

            if( userId is null )
            {
                return FailNoteCreate();
            }

            var isCurrentUserCanCreateNote = await _userService.CurrentUserCanCreateNoteAsync();

            if( !isCurrentUserCanCreateNote )
            {
                return FailNoteCreate();
            }

            newNote.UserId = userId;

            Note note = await _noteRepository.CreateNoteAsync(newNote);

            return note.Adapt<NoteResponse>();
            
        }catch(NotAuthorizedException)
        {
            return FailNoteCreate();
        }
    }

    private static Result<NoteResponse> FailNoteCreate()
    {
        return Result.Fail<NoteResponse>("El usuario no esta autorizado para crear una nota!");
    }

}
