using Microsoft.EntityFrameworkCore;

namespace DAL;

public class UserContext : DbContext
{
    #region Constructors
    public UserContext(DbContextOptions<UserContext> options): base(options)
    {

    }
    #endregion
    public DbSet<User>? Users { get; set; }
    public DbSet<Country>? Countries { get; set; }

    #region Fluent API 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    #endregion
}
