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
    public class BudgetItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BudgetItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BudgetItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BudgetItems.Include(b => b.Wedding);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BudgetItems/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetItem = await _context.BudgetItems
                .Include(b => b.Wedding)
                .FirstOrDefaultAsync(m => m.BudgetItemId == id);
            if (budgetItem == null)
            {
                return NotFound();
            }

            return View(budgetItem);
        }

        // GET: BudgetItems/Create
        public IActionResult Create()
        {
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title");
            return View();
        }

        // POST: BudgetItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetItemId,Category,Description,AmountAllocated,AmountSpent,WeddingId")] BudgetItem budgetItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(budgetItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", budgetItem.WeddingId);
            return View(budgetItem);
        }

        // GET: BudgetItems/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetItem = await _context.BudgetItems.FindAsync(id);
            if (budgetItem == null)
            {
                return NotFound();
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", budgetItem.WeddingId);
            return View(budgetItem);
        }

        // POST: BudgetItems/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetItemId,Category,Description,AmountAllocated,AmountSpent,WeddingId")] BudgetItem budgetItem)
        {
            if (id != budgetItem.BudgetItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budgetItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetItemExists(budgetItem.BudgetItemId))
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
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "WeddingId", "Title", budgetItem.WeddingId);
            return View(budgetItem);
        }

        // GET: BudgetItems/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetItem = await _context.BudgetItems
                .Include(b => b.Wedding)
                .FirstOrDefaultAsync(m => m.BudgetItemId == id);
            if (budgetItem == null)
            {
                return NotFound();
            }

            return View(budgetItem);
        }

        // POST: BudgetItems/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budgetItem = await _context.BudgetItems.FindAsync(id);
            if (budgetItem != null)
            {
                _context.BudgetItems.Remove(budgetItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudgetItemExists(int id)
        {
            return _context.BudgetItems.Any(e => e.BudgetItemId == id);
        }
    }
}
