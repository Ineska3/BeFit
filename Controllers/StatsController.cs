using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StatsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User)!;
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var stats = _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.WorkoutSession)
                .Where(e => e.UserId == userId && e.WorkoutSession!.StartTime >= fourWeeksAgo)
                .GroupBy(e => e.ExerciseType!.Name)
                .Select(g => new ExerciseStat
                {
                    ExerciseName = g.Key,
                    TimesPerformed = g.Count(), // Ilość wystąpień w tym przedziale czasowym
                    TotalReps = g.Sum(e => e.Sets * e.Reps),
                    AverageWeight = g.Average(e => e.Weight),
                    MaxWeight = g.Max(e => e.Weight)
                })
                .OrderByDescending(s => s.TimesPerformed)
                .ToList();

            return View(stats);
        }
    }
}