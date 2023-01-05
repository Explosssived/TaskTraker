using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : Controller
{
    private readonly ProjectContext _context;

    public ProjectController(ProjectContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> Get() => await _context.Projects.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> Get(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project is null)
            return NotFound("Такого проекта не существует");
        return new ObjectResult(project);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Post(Project? project)
    {
        if (project is null)
            return BadRequest("Не правильный запрос");
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return Ok(project);
    }

    [HttpPut]
    public async Task<ActionResult<Project>> Put(Project? project)
    {
        if (project is null)
            return BadRequest("Не правильный запрос");
        if (!_context.Projects.Any(p => p.Id == project.Id))
            return NotFound("Такой проект не найден");
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Project>> Delete(int? id)
    {
        if (id is null)
            return BadRequest("Не правильный запрос");
        var project = _context.Projects.FirstOrDefault(p => p.Id == id);
        if (project is null)
            return NotFound("Проект не найден");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return Ok(project);
    }
}