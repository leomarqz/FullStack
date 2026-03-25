
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechNotes.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{

/* * DESIGN-TIME DB CONTEXT FACTORY
 * -----------------------------------------------------------------------------------------
 * PURPOSE: 
 * This factory allows EF Core CLI tools (dotnet-ef) to create an instance of ApplicationDbContext 
 * without running the main application (Blazor). It serves as a standalone entry point for 
 * generating migrations when the UI project is unavailable or complex to initialize.
 * * WHY DISABLE IT LATER? 
 * In production/shared environments, we prefer 'MigrationsAssembly' configuration in Program.cs 
 * to use secure connection strings from 'appsettings.json' or Environment Variables.
 * * SECURITY WARNING: 
 * Do NOT use this in Production. The connection string below contains hardcoded credentials.
 * -----------------------------------------------------------------------------------------
 */

 #if DEBUG
 
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // var connStr = "Server=localhost;Database=TechNotesDb;User ID=SA;Password=MyStrongPass123;TrustServerCertificate=true;MultipleActiveResultSets=true";
        var connStr = "Server=XNERD;Database=TechNotesDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True";

        builder.UseSqlServer( connStr, (sqlBuilder) =>
        {
            sqlBuilder.MigrationsAssembly( typeof(ApplicationDbContext).Assembly.FullName );
        });

        return new ApplicationDbContext( builder.Options );
    }
}

#endif