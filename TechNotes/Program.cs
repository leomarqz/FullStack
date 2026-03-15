
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
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
        
        // --- OAUTH CONTROLLER INTEGRATION ---
        // Enables MVC Controller support to act as a bridge for the OAuth2 flow.
        // While Blazor handles the UI, we need traditional endpoints to process 
        // the 'Challenge' and 'ExternalLoginCallback' redirects from Google.
        builder.Services.AddControllers();

        // --- CRYPTOGRAPHIC KEY PERSISTENCE ---
        // Configures the Data Protection API to ensure consistent cookie encryption.
        // By setting a static Application Name, we guarantee that authentication 
        // tokens remain valid across application restarts and horizontal scaling.
        builder.Services.AddDataProtection().SetApplicationName("TechNotes");

        // --- GLOBAL COOKIE POLICY CONFIGURATION ---
        // This block defines how the application handles cookies at a system-wide level,
        // ensuring compatibility with modern browser security and external providers like Google.
        builder.Services.Configure<CookiePolicyOptions>((options) =>
        {
            // Determines whether user consent is required for non-essential cookies.
            // Set to 'false' to allow immediate issuance of authentication cookies (GDPR compliance).
            options.CheckConsentNeeded = (context)=> false; 

            // Sets the minimum SameSite attribute for all cookies.
            // 'Unspecified' is critical for Google/External Auth; it prevents browsers from
            // blocking the correlation cookie during the redirect back from Google's servers.
            options.MinimumSameSitePolicy = SameSiteMode.Unspecified; 
        });

        // --- MAIN APPLICATION COOKIE SETTINGS ---
        // Configures the primary session cookie issued after a successful authentication.
        // This cookie represents the "logged-in" state of the user within TechNotes.
        builder.Services.ConfigureApplicationCookie((options) =>
        {
            // Prevents client-side scripts (JavaScript) from accessing the cookie.
            // This is a critical defense against Cross-Site Scripting (XSS) attacks.
            options.Cookie.HttpOnly = true;

            // Determines if the cookie requires an HTTPS connection.
            // 'SameAsRequest' allows HTTP during local development but enforces HTTPS in production.
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            // Controls cross-site cookie behavior. 
            // 'Lax' is the industry standard for OAuth, allowing the session to persist 
            // when a user is redirected back to the app from an external site (like Google).
            options.Cookie.SameSite = SameSiteMode.Lax;

            // Provides a recognizable name for the cookie in the browser's DevTools.
            // Useful for debugging and avoids using the default '.AspNetCore.Cookies' name.
            options.Cookie.Name = "TechNotes.Auth";
        });


        // --- EXTERNAL AUTHENTICATION COOKIE OVERRIDE ---
        // This block configures the temporary cookie used by Identity to store 
        // claims received from external providers (like Google) before the local user 
        // is signed in or created in our database.
        builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ExternalScheme, (options) =>
        {
            // Hardens the temporary external cookie against client-side script access.
            options.Cookie.HttpOnly = true;

            // Matches the security of the incoming request (HTTP/HTTPS).
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            // Ensures the external cookie is sent when Google redirects back to our site.
            // This prevents the "Correlation Failed" error during the OAuth callback.
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        // Project: TechNotes.Application
        builder.Services.AddApplication();
        
        // Project: TechNotes.Infrastructure
        builder.Services.AddInfrastructure( builder.Configuration );


        // --- GOOGLE EXTERNAL AUTHENTICATION HANDLER ---
        // Registers the Google OpenID Connect (OIDC) handler to enable social login.
        // This configuration defines how the app requests and validates user data from Google.
        builder.Services.AddAuthentication().AddGoogle((options) =>
        {
            // Credentials retrieved from secure configuration (appsettings.json or Secrets).
            // Get your Google API client ID (Browser)
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";

            // The endpoint where Google sends the auth code; handled automatically by the middleware.
            options.CallbackPath = "/signin-google";

            // Maps the Google identity to the temporary 'External' scheme we configured earlier.
            options.SignInScheme = IdentityConstants.ExternalScheme;

            // Persists the access_token and id_token for future API calls if needed.
            options.SaveTokens = true;

            // --- SECURITY: CORRELATION COOKIE ---
            // Hardens the cookie used to prevent Cross-Site Request Forgery (CSRF) 
            // during the redirection state.
            options.CorrelationCookie.HttpOnly = true;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.CorrelationCookie.SameSite = SameSiteMode.Lax;

            // --- PERMISSIONS: SCOPES ---
            // Defines the specific user data requested from the Google Account.
            options.Scope.Add("email");
            options.Scope.Add("profile");

        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Force all HTTP traffic to be redirected to HTTPS for secure communication.
        app.UseHttpsRedirection();

        app.UseStaticFiles(); // Serve files from wwwroot (CSS, JS, Images).

        app.UseCookiePolicy(); // Applies the global cookie rules

        app.UseAuthentication(); // Who the user is.
        app.UseAuthorization();  // What the user can do!

        app.UseAntiforgery(); // Protects against Cross-Site Request Forgery (CSRF)

        app.MapControllers(); // Map traditional MVC Controllers (Custom)

        // Map the Blazor Root component and enable Interactive Server rendering.
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Start the web host and listen for incoming requests.
        app.Run();
    }
}
