using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;
using TaskTracker.ViewModels;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : Controller
{
    private readonly ProjectContext _context;

    public TaskController(ProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Task>>> Get() => await _context.Tasks.Include(t => t.Project).ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Task>> Get(int id)
    {
        var task = await _context.Tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
        if (task is null)
            return NotFound("Такая задача не была найдена");
        return new ObjectResult(task);
    }

    [HttpPost]
    public async Task<ActionResult<Task>> Post(TaskViewModel taskModel)
    {
        if (!ModelState.IsValid)
            return BadRequest("Не правильно введенны данные");
        var task = new Task()
        {
            Name = taskModel.Name,
            Description = taskModel.Description,
            Priority = taskModel.Priority,
            StartDate = taskModel.StartDate,
            ProjectId = taskModel.ProjectId
        };
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpPut]
    public async Task<ActionResult<Task>> Put(TaskViewModel? model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Не правильно введены данные");

        try
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Name == model.Name && t.StartDate == model.StartDate);
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        catch (NullReferenceException)
        {
            return NotFound("Такая задача не найдена");
        }
        
        return Ok(model);
    }

    public Exception ModelNotValidException { get; set; }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Task>> Delete(int? id)
    {
        if (id is null)
            return BadRequest("Не правильный запрос");
        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
            return NotFound("Задача не найдена");
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return Ok(task);
    }
}