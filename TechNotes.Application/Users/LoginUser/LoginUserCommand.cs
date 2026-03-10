using System;

namespace TechNotes.Application.Users.LoginUser;

public class LoginUserCommand : ICommand
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
