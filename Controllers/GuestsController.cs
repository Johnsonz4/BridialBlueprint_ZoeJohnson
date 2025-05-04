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
    public class GuestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Guests.Include(g => g.Wedding);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Guests/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .Include(g => g.Wedding)
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title");
            return View();
        }

        // POST: Guests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,Name,Email,RSVPStatus,MealPreference,WeddingId")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", guest.WeddingId);
            return View(guest);
        }

        // GET: Guests/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", guest.WeddingId);
            return View(guest);
        }

        // POST: Guests/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuestId,Name,Email,RSVPStatus,MealPreference,WeddingId")] Guest guest)
        {
            if (id != guest.GuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestExists(guest.GuestId))
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
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", guest.WeddingId);
            return View(guest);
        }

        // GET: Guests/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests
                .Include(g => g.Wedding)
                .FirstOrDefaultAsync(m => m.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRSVP(int id, string rsvpStatus)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            guest.RSVPStatus = rsvpStatus;
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.GuestId == id);
        }
    }
}
