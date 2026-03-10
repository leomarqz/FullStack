
using System.ComponentModel.DataAnnotations;

namespace TechNotes.Features.Users;

public class RegisterUserModel
{
    [Required(ErrorMessage = "El nombre de usuario es requerido!")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es requerido!")]
    [EmailAddress(ErrorMessage = "El email no es valido!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida!")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "La confirmacion de contraseña es requerida!")]
    [Compare( nameof(Password), ErrorMessage = "Las contraseñas no coinciden!" )]
    public string ConfirmPassword { get; set; } = string.Empty;
}
