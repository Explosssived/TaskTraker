using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    
    public int ProjectId { get; set; }
    public Project Project { get; set; }
}