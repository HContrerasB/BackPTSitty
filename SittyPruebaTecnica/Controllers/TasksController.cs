using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SittyPruebaTecnica.Data;
using System.IdentityModel.Tokens.Jwt;


namespace SittyPruebaTecnica.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] Models.Task task)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            task.UserId = userId;  // Asignar UserId al crear la tarea
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return Ok(tasks);
        }

        [HttpPut("complete/{id}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Completed = true;
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }


}
