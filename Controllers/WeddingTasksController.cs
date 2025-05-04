using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BridalBlueprint.Models;
using BridalBlueprint.Data;

namespace BridalBlueprint.Controllers
{
    public class WeddingTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeddingTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WeddingTasks.Include(t => t.Wedding);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tasks/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.WeddingTasks
                .Include(t => t.Wedding)
                .FirstOrDefaultAsync(m => m.WeddingTaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title");
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeddingTaskId,Title,Description,DueDate,IsCompleted,WeddingId")] WeddingTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Model error: " + modelError.ErrorMessage);
            }
            Console.WriteLine("POST Create hit. Title: " + task.Title);
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", task.WeddingId);
            return View(task);
        }

        // GET: Tasks/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.WeddingTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", task.WeddingId);
            return View(task);
        }

        // POST: Tasks/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeddingTaskId,Title,Description,DueDate,IsCompleted,WeddingId")] WeddingTask task)
        {
            if (id != task.WeddingTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.WeddingTaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", task.WeddingId);
            return View(task);
        }

        // GET: Tasks/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.WeddingTasks
                .Include(t => t.Wedding)
                .FirstOrDefaultAsync(m => m.WeddingTaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.WeddingTasks.FindAsync(id);
            if (task != null)
            {
                _context.WeddingTasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [IgnoreAntiforgeryToken] 
        public async Task<IActionResult> ToggleComplete(int id, bool isCompleted)
        {
            var task = await _context.WeddingTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TaskExists(int id)
        {
            return _context.WeddingTasks.Any(e => e.WeddingTaskId == id);
        }
    }
}

