using Api_Bitsion.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api_Bitsion.DataAccess;

public class UsersDbContext : IdentityDbContext<User>
{
    private const string Schema = "users";

    public UsersDbContext(DbContextOptions<UsersDbContext> options): base(options)
    {  
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {      
        base.OnModelCreating(modelBuilder);
	    modelBuilder.HasDefaultSchema(Schema);  

        // Property Configurations
        modelBuilder.Entity<User>()
            .Property(x => x.IsActive)
            .HasDefaultValue(true); 
    }
}
