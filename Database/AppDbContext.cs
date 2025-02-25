using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ClientCompany)
            .WithMany()
            .HasForeignKey(p => p.ClientCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ExecutorCompany)
            .WithMany()
            .HasForeignKey(p => p.ExecutorCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Supervisor)
            .WithOne(p => p.Project)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Employees)
            .WithOne(e => e.Project)
            .HasForeignKey()
    }
}