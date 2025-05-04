using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BridalBlueprint.Models;
using BridalBlueprint.Data;


namespace BridalBlueprint.Controllers
{
    [Authorize]
    public class WeddingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeddingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Weddings
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var weddings = await _context.WeddingUsers
                .Where(wu => wu.UserId == user.Id)
                .Include(wu => wu.Wedding)
                    .ThenInclude(w => w.Guests)
                .Include(wu => wu.Wedding)
                    .ThenInclude(w => w.Tasks)
                .Include(wu => wu.Wedding)
                    .ThenInclude(w => w.BudgetItems)
                .Select(wu => wu.Wedding)
                .ToListAsync();

            return View(weddings);
        }


        // GET: Weddings/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings
                .FirstOrDefaultAsync(m => m.WeddingId == id);
            if (wedding == null)
            {
                return NotFound();
            }

            return View(wedding);
        }

        // GET: Weddings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weddings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeddingId,Title,Date,Location")] Wedding wedding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wedding);
                await _context.SaveChangesAsync();

                
                var user = await _userManager.GetUserAsync(User);
                var weddingUser = new WeddingUser
                {
                    UserId = user.Id,
                    WeddingId = wedding.WeddingId
                };
                _context.WeddingUsers.Add(weddingUser);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(wedding);
        }

        // GET: Weddings/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings.FindAsync(id);
            if (wedding == null)
            {
                return NotFound();
            }
            return View(wedding);
        }

        // POST: Weddings/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeddingId,Title,Date,Location")] Wedding wedding)
        {
            if (id != wedding.WeddingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wedding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingExists(wedding.WeddingId))
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
            return View(wedding);
        }

        // GET: Weddings/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings
                .FirstOrDefaultAsync(m => m.WeddingId == id);
            if (wedding == null)
            {
                return NotFound();
            }

            return View(wedding);
        }

        // POST: Weddings/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wedding = await _context.Weddings.FindAsync(id);
            if (wedding != null)
            {
                _context.Weddings.Remove(wedding);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeddingExists(int id)
        {
            return _context.Weddings.Any(e => e.WeddingId == id);
        }
    }
}

