
using System.ComponentModel.DataAnnotations;

namespace TechNotes.Features.Users;

public class LoginUserModel
{
    [Required(ErrorMessage = "El nombre de usuario es requerido!")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida!")]
    public string Password { get; set; } = string.Empty;
}
