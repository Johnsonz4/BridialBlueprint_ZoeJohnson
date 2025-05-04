using BridalBlueprint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridalBlueprint.Data;

namespace BridalBlueprint.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var userWeddingIds = await _context.WeddingUsers
                .Where(wu => wu.UserId == user.Id)
                .Select(wu => wu.WeddingId)
                .ToListAsync();

            var totalWeddings = userWeddingIds.Count;

            var totalGuests = await _context.Guests
                .CountAsync(g => userWeddingIds.Contains(g.WeddingId));

            var tasks = await _context.WeddingTasks
                .Where(t => userWeddingIds.Contains(t.WeddingId))
                .ToListAsync();

            var completedTasks = tasks.Count(t => t.IsCompleted);
            var taskCompletionRate = _context.WeddingTasks.Count() > 0

                ? (int)((double)completedTasks / tasks.Count * 100)
                : 0;

            var budgetItems = await _context.BudgetItems
                .Where(b => userWeddingIds.Contains(b.WeddingId))
                .ToListAsync();

            var totalBudget = budgetItems.Sum(b => b.AmountAllocated);
            var actualSpent = budgetItems.Sum(b => b.AmountSpent);

            ViewBag.TotalWeddings = totalWeddings;
            ViewBag.TotalGuests = totalGuests;
            ViewBag.TaskCompletion = taskCompletionRate;
            ViewBag.BudgetTotal = totalBudget;
            ViewBag.ActualSpent = actualSpent;

            return View();
        }
    }
}
