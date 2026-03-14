
using TechNotes.Application;
using TechNotes.Infrastructure;

namespace TechNotes;

/**
 * @author: Leomarqz
 * @project: TechNotes
 * @date: 2026
 */

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        // Project: TechNotes.Application
        builder.Services.AddApplication();
        
        // Project: TechNotes.Infrastructure
        builder.Services.AddInfrastructure( builder.Configuration );
        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        // No se habian agregado, aun asi la app funcionaba bien
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
