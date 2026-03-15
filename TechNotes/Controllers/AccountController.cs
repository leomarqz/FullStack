
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechNotes.Infrastructure.Authentication;

namespace TechNotes.Controllers
{
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("external-login")]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectUrl = Url.Action( nameof(HandleExternalCallback) );
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        [HttpGet("external-callback")]
        public async Task<IActionResult> HandleExternalCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if(info == null)
                return RedirectWithError("Error al obtener información de Google.");
            
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (result.Succeeded)
                return Redirect("/notes");

            // Si llegamos aquí, el usuario no está vinculado. Buscamos por Email.
            var email = info.Principal.FindFirstValue( ClaimTypes.Email );

            if( string.IsNullOrEmpty(email))
                return RedirectWithError("No se pudo obtener el email de Google.");

            // Verificamos si existe el usuario
            var user = await _userManager.FindByEmailAsync(email); 
            
            if(user == null)
            {
                // Sino existe el usuario lo creamos
                user = new User
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                
                var createResult = await _userManager.CreateAsync(user);

                if (!createResult.Succeeded)
                    return RedirectWithError("Error al crear la cuenta local.");
            }

            // Vinculamos la cuenta de Google con el usuario de nuestra BD
            var addLoginResult = await _userManager.AddLoginAsync(user, info);

            if( addLoginResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Redirect("/notes");
            }

            return RedirectWithError("Error al vincular la cuenta de Google.");

        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }

        private IActionResult RedirectWithError(string message)
        {
            var encoded = Uri.EscapeDataString(message);

            return Redirect($"/register?error={encoded}");
        }

    }
}
