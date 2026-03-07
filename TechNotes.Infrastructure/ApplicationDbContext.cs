
using Microsoft.EntityFrameworkCore;
using TechNotes.Domain.Notes;

namespace TechNotes.Infrastructure;

public class ApplicationDbContext : DbContext
{
    /*
    * Constructor
    */
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : base(options)
    {}

    /*
    * Tables
    */
    public DbSet<Note> Notes { get; set; }


    /*
    * Conventions
    */
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    /*
    * Configurations
    */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    /*
    * Models configuration 
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
