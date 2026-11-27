using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization; // NOWE

namespace BeFit.Controllers
{
    [Authorize] // Zabezpiecza cały kontroler
    public class ExerciseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ExerciseEntriesController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string GetUserId() => _userManager.GetUserId(User)!;

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var entries = _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.WorkoutSession)
                .Where(e => e.UserId == userId);
            return View(await entries.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.WorkoutSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (exerciseEntry == null) return NotFound();
            return View(exerciseEntry);
        }

        public IActionResult Create()
        {
            // Listy wyboru: tylko sesje użytkownika
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");
            ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSessions.Where(s => s.UserId == GetUserId()), "Id", "StartTime");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseEntryDto dto)
        {
            if (ModelState.IsValid)
            {
                var entry = new ExerciseEntry
                {
                    ExerciseTypeId = dto.ExerciseTypeId,
                    WorkoutSessionId = dto.WorkoutSessionId,
                    Weight = dto.Weight,
                    Sets = dto.Sets,
                    Reps = dto.Reps,
                    UserId = GetUserId()
                };
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", dto.ExerciseTypeId);
            ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSessions.Where(s => s.UserId == GetUserId()), "Id", "StartTime", dto.WorkoutSessionId);
            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var exerciseEntry = await _context.ExerciseEntries
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (exerciseEntry == null) return NotFound();
            
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSessions.Where(s => s.UserId == userId), "Id", "StartTime", exerciseEntry.WorkoutSessionId);
            return View(exerciseEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExerciseTypeId,WorkoutSessionId,Weight,Sets,Reps,UserId")] ExerciseEntry exerciseEntry)
        {
            if (id != exerciseEntry.Id) return NotFound();
            
            // Weryfikacja czy edytujemy swoje
            if (exerciseEntry.UserId != GetUserId()) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseEntryExists(exerciseEntry.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", exerciseEntry.ExerciseTypeId);
            ViewData["WorkoutSessionId"] = new SelectList(_context.WorkoutSessions.Where(s => s.UserId == GetUserId()), "Id", "StartTime", exerciseEntry.WorkoutSessionId);
            return View(exerciseEntry);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.ExerciseType)
                .Include(e => e.WorkoutSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (exerciseEntry == null) return NotFound();
            return View(exerciseEntry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            var exerciseEntry = await _context.ExerciseEntries
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (exerciseEntry != null)
            {
                _context.ExerciseEntries.Remove(exerciseEntry);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseEntryExists(int id) => _context.ExerciseEntries.Any(e => e.Id == id);
    }
}