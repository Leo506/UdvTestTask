using Microsoft.EntityFrameworkCore;
using UdvTestTask.Abstractions;
using UdvTestTask.Models;

namespace UdvTestTask.Data;

public sealed partial class LettersCountDbContext : DbContext, IRepository<LettersCount>
{
    public LettersCountDbContext()
    {
    }

    public LettersCountDbContext(DbContextOptions<LettersCountDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    private DbSet<LettersCount> LettersCounts { get; set; } = null!;

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
    public async Task AddAsync(LettersCount entity)
    {
        await LettersCounts.AddAsync(entity);
        await SaveChangesAsync();
    }
}