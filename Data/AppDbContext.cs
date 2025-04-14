using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //project - executor company
        modelBuilder.Entity<Project>()
            .HasOne(p => p.ContractorCompany)
            .WithMany(c => c.ProjectsAsExecutor)
            .HasForeignKey(p => p.ContractorCompanyId)
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
            .WithOne()
            .HasForeignKey<Project>(p => p.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //project relationships - end
        
        //employee relationships - start
        
        //employee - project
        modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeeProjects)
            .HasForeignKey(ep => ep.EmployeeId);

        modelBuilder.Entity<EmployeeProject>()
            .HasOne(ep => ep.Project)
            .WithMany(p => p.EmployeeProjects)
            .HasForeignKey(ep => ep.ProjectId);

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
            .OnDelete(DeleteBehavior.Cascade);
        
        //task - author
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.Author)
            .WithMany(p => p.TasksAsAuthor)
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //task - executor
        modelBuilder.Entity<ProjectTask>()
            .HasOne(t => t.Executor)
            .WithMany(p => p.TasksAsExecutor)
            .HasForeignKey(t => t.ExecutorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //task relationships - end
        
        //document - project
        
        modelBuilder.Entity<ProjectDocument>()
            .HasOne<Project>(d => d.Project)
            .WithMany(p => p.Documents)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //no need to include company relationships
    }
}