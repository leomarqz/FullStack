using System;
using TechNotes.Application.Authentication;

namespace TechNotes.Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        bool success = await _authenticationService.LoginUserAsync(request.UserName, request.Password);

        return success 
                ? Result.Ok() 
                : Result.Fail("Nombre de usuario o contraseña son invalidos!");
        
    }
}
