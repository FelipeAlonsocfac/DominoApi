using Domino.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domino.Api.Infrastructure.DataAccess;

public class DominoDBContext : DbContext
{
    public DominoDBContext(DbContextOptions<DominoDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07321175C8");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserTable> User { get; set; }
}