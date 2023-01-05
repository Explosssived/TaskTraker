using Microsoft.EntityFrameworkCore;
using TaskTracker.Models;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Data;

public class ProjectContext: DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    
    
    public ProjectContext(DbContextOptions options) : base(options)
    {
    }
}