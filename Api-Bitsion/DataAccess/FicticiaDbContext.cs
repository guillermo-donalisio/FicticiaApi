using Api_Bitsion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Bitsion.DataAccess;

public class FicticiaDbContext : DbContext
{
    public FicticiaDbContext(DbContextOptions<FicticiaDbContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("FicticiaConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {           
        // Fluent Api
        // Property Configurations    
        modelBuilder.Entity<Client>()
                    .Property(c => c.ID)
                    .HasColumnName("ClientID")
                    .IsRequired();   

        modelBuilder.Entity<Client>()
                    .Property(c => c.FullName)
                    .HasMaxLength(50)
                    .IsRequired();
        
        modelBuilder.Entity<Client>()
                    .Property(c => c.Gender)
                    .HasMaxLength(50);                    

        modelBuilder.Entity<Client>()
                    .Property(c => c.IsActive)
                    .HasDefaultValue(false);

        modelBuilder.Entity<Client>()
                    .Property(c => c.CreatedAt)
                    .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<Client>()
                    .Property(c => c.UpdatedAt)
                    .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<Client>()
                    .Property(c => c.WearGlasses)
                    .HasDefaultValue(false);

        modelBuilder.Entity<Client>()
                    .Property(c => c.IsDiabetic)
                    .HasDefaultValue(false);

        modelBuilder.Entity<Client>()
                    .Property(c => c.IsSick)
                    .HasDefaultValue(false);

        modelBuilder.Entity<Client>()
                    .Property(c => c.Illness)
                    .HasMaxLength(100);
    }

    public DbSet<Client> Clients {set;get;} 
    
}
