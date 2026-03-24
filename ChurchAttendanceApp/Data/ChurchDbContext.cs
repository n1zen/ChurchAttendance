using Microsoft.EntityFrameworkCore;

using ChurchAttendanceApp.Models;

namespace ChurchAttendanceApp.Data;

public class ChurchDbContext : DbContext
{
    public ChurchDbContext(DbContextOptions<ChurchDbContext> options) : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasIndex(m => m.MemberId).IsUnique();

            entity.HasMany(m => m.AttendanceRecords)
                .WithOne(a => a.Member)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
