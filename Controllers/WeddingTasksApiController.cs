using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridalBlueprint.Data;
using BridalBlueprint.Models;

namespace BridalBlueprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingTasksApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WeddingTasksApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WeddingTasksApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeddingTask>>> GetWeddingTasks()
        {
            return await _context.WeddingTasks
                .Include(t => t.Wedding)
                .ToListAsync();
        }

        // GET: api/WeddingTasksApi
        [HttpGet("{id}")]
        public async Task<ActionResult<WeddingTask>> GetWeddingTask(int id)
        {
            var task = await _context.WeddingTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        // POST: api/WeddingTasksApi
        [HttpPost]
        public async Task<ActionResult<WeddingTask>> PostWeddingTask(WeddingTask task)
        {
            _context.WeddingTasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeddingTask), new { id = task.WeddingTaskId }, task);
        }

        // PUT: api/WeddingTasksApi
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeddingTask(int id, WeddingTask task)
        {
            if (id != task.WeddingTaskId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.WeddingTasks.Any(e => e.WeddingTaskId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/WeddingTasksApi
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeddingTask(int id)
        {
            var task = await _context.WeddingTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.WeddingTasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

