
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TechNotes.Application.Exceptions;
using TechNotes.Application.Users;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor httpContextAccessor; // 
    private readonly INoteRepository noteRepository;

    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, INoteRepository noteRepository)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.noteRepository = noteRepository;
    }

    public async Task<bool> CurrentUserCanCreateNoteAsync()
    {
        var user = await GetCurrentUserAsync();

        if( user is null )
        {
            return false;
        }

        var isAdmin = await this.userManager.IsInRoleAsync(user, "Admin");
        var isWriter = await this.userManager.IsInRoleAsync(user, "Writer");

        return isAdmin || isWriter;

    }

    public async Task<bool> CurrentUserCanEditNoteAsync(int noteId)
    {
        var user = await GetCurrentUserAsync();

        if( user is null )
        {
            return false;
        }

        var isAdmin = await this.userManager.IsInRoleAsync(user, "Admin");
        var isWriter = await this.userManager.IsInRoleAsync(user, "Writer");

        var note = await this.noteRepository.GetNoteAsync(noteId);

        if( note is null )
        {
            return false;
        }

        var isAuthorized = isAdmin || ( isWriter && note.UserId == user.Id);

        return isAuthorized;
    }

    public async Task<string> GetCurrentUserIdAsync()
    {
        var user = await GetCurrentUserAsync();

        if( user is null )
        {
            throw new NotAuthorizedException();
        }

        return user.Id;
    }

    public async Task<bool> IsCurrentUserInRoleAsync(string role)
    {
        var user = await GetCurrentUserAsync();

        if( user is null )
        {
            return false;
        }

        // Validamos si el usuario posee el rol que se pase por parametro!
        var isUserInRole = await this.userManager.IsInRoleAsync(user, role);

        return isUserInRole;

    }

    private async Task<User?> GetCurrentUserAsync()
    {
        var httpContext = this.httpContextAccessor.HttpContext;

        if( httpContext is null || httpContext.User is null)
        {
            return null;
        }

        return await userManager.GetUserAsync( httpContext.User );
    }
}
