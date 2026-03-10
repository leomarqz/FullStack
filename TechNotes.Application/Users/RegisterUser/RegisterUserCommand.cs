
namespace TechNotes.Application.Users.RegisterUser;

public class RegisterUserCommand : ICommand
{
    public required string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
