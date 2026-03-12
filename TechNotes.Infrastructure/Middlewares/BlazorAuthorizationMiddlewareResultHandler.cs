
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace TechNotes.Infrastructure.Middlewares;

/*
* @author leomarqz 2026

* * MIDDLEWARE: BlazorAuthorizationMiddlewareResultHandler

* * DESCRIPCIÓN:
* Este manejador personaliza la respuesta del middleware de autorización de .NET 8+.
* Su función es interceptar y anular el comportamiento por defecto de ASP.NET Core Identity,
* el cual redirige automáticamente (Status 302) a los usuarios no autenticados hacia '/Account/Login'.

* * POR QUÉ ES NECESARIO:
* En aplicaciones Blazor modernas (SSR/Server), queremos que el control de la navegación permanezca 
* dentro del ecosistema de Blazor. Al permitir que la petición continúe (next), delegamos la 
* responsabilidad al Router de Blazor (<AuthorizeRouteView>). 
* * Esto permite que el usuario vea nuestra pantalla de "Acceso Restringido" personalizada con 
* Bootstrap 5 en lugar de ser expulsado abruptamente de la aplicación hacia una URL externa.
*/

public class BlazorAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    
    public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        return next(context);
    }
}
