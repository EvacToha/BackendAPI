using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VebTech.Domain.Models.Entities;

namespace VebTech.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builderOptions = DbContextOptionsFactory.Get();
            return new DatabaseContext(builderOptions);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRole"));

        modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Age).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
        
        modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();
    }
}