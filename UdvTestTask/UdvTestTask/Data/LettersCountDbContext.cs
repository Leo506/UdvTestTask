using Microsoft.EntityFrameworkCore;
using UdvTestTask.Models;

namespace UdvTestTask.Data;

public partial class LettersCountDbContext : DbContext
{
    public LettersCountDbContext()
    {
    }

    public LettersCountDbContext(DbContextOptions<LettersCountDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LettersCount> LettersCounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LettersCount>(entity =>
        {
            entity.ToTable("LettersCount");

            entity.Property(e => e.LettersData).HasColumnType("jsonb");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}