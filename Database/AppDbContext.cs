using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //project - executor company
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ExecutorCompany)
            .WithMany(c => c.ProjectsAsExecutor)
            .HasForeignKey(p => p.ExecutorCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //project - client company
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ClientCompany)
            .WithMany(c => c.ProjectsAsClient)
            .HasForeignKey(p => p.ClientCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //project - supervisor
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Supervisor)
            .WithOne(s => s.Project)
            .HasForeignKey<Project>(p => p.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //project relationships - end
        
        //employee relationships - start
        
        //employee - project
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Project)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        //employee - company
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //employee relationships - end
        
        //task relationships - start
        
        //task - project
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //task - author
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.Author)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //task - executor
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.Executor)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ExecutorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //task relationships - end
        
        //no need to include company relationships
    }
}