using GymManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Context;

public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Member entity
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.FirstName)
                .IsRequired().HasMaxLength(100);
            entity.Property(m => m.LastName)
                .IsRequired().HasMaxLength(100);
            entity.Property(m => m.Document)
                .IsRequired();
            entity.Property(m => m.Email)
                .IsRequired().HasMaxLength(255);
            entity.Property(m => m.PhoneNumber)
                .HasMaxLength(20);
            entity.Property(m => m.Address)
                .HasMaxLength(500);
            entity.Property(m => m.RegistrateDate)
                .IsRequired();
            entity.Property(m => m.IsActive)
                .IsRequired();
            entity.Property(m => m.CreatedAt)
                .IsRequired();
            entity.Property(m => m.UpdatedAt)
                .IsRequired(false);
            entity.HasIndex(m => m.Document)
                .IsUnique();
        });
        // Additional configurations for other entities can be added here
    }
}
