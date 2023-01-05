namespace TaskTracker.ViewModels;

public class TaskViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    
    public int ProjectId { get; set; }
}